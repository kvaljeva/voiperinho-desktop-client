using NAudio.Wave;
using System;

namespace Voiperinho
{
    public class CallHelper
    {
        private WaveInEvent waveIn;
        private DirectSoundOut waveOut;
        private BufferedWaveProvider waveProvider;
        private SpeexCodec codec;
        private volatile bool isCallEstablished;
        private string callId;

        public delegate void CallHelperDataAvailable(byte[] encodedData);
        public event CallHelperDataAvailable DataAvailable;

        public bool IsCallEstablished
        {
            get { return this.isCallEstablished; }
        }

        public string CallId
        {
            get { return this.callId; }
        }

        public CallHelper()
        {
            this.isCallEstablished = true;
            this.codec = new NarrowBandSpeexCodec();
        }

        private string GenerateCallId()
        {
            // "n" parameter in ToString is used to remove dashes (-) between the values
            return Guid.NewGuid().ToString("n").Substring(0, 16);
        }

        public void EstablishCall(string callIdentificator = "")
        {
            this.waveIn = new WaveInEvent();

            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = this.codec.RecordFormat;
            waveIn.BufferMilliseconds = 50;
            waveIn.DataAvailable += waveIn_DataAvailable;

            waveOut = new DirectSoundOut();
            waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            waveProvider.DiscardOnBufferOverflow = true;
            waveProvider.BufferLength = 4096;
            waveOut.Init(waveProvider);

            waveIn.StartRecording();
            waveOut.Play();

            this.isCallEstablished = true;

            if (callIdentificator == null || callIdentificator == string.Empty)
                this.callId = GenerateCallId();
            else
                this.callId = callIdentificator;
        }

        public void CloseCall()
        {
            if (this.waveIn != null)
            {
                this.waveIn.StopRecording();
                this.waveIn.Dispose();
                this.waveIn = null;
            }
            if (this.waveOut != null)
            {
                this.waveOut.Stop();
                this.waveOut.Dispose();
                this.waveOut = null;
            }

            this.isCallEstablished = false;
        }

        public void AddSamples(byte[] data)
        {
            byte[] decodedData = this.codec.Decode(data, 0, data.Length);

            this.waveProvider.AddSamples(decodedData, 0, decodedData.Length);
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] encodedData = this.codec.Encode(e.Buffer, 0, e.BytesRecorded);

            if (this.DataAvailable != null) this.DataAvailable.Invoke(encodedData);
        }
    }
}
