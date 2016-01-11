using NAudio.Wave;
using System;

namespace Voiperinho.Helpers
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

        public CallHelper(string id)
        {
            this.isCallEstablished = true;
            this.callId = id;

            this.codec = new NarrowBandSpeexCodec();
        }

        public void EstablishCall()
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
            //byte[] decodedData = this.codec.Decode(data, 0, data.Length);

            this.waveProvider.AddSamples(data, 0, data.Length);
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            //byte[] encodedData = this.codec.Encode(e.Buffer, 0, e.BytesRecorded);

            if (this.DataAvailable != null) this.DataAvailable.Invoke(e.Buffer);
        }
    }
}
