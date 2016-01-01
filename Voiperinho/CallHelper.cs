using NAudio.Wave;

namespace Voiperinho
{
    public class CallHelper
    {
        private WaveIn waveIn;
        private DirectSoundOut waveOut;
        private WaveInProvider waveProvider;
        private volatile bool isCallEstablished;

        public delegate void CallHelperDataAvailable(object sender, WaveInEventArgs e);
        public event CallHelperDataAvailable DataAvailable;

        public bool IsCallEstablished
        {
            get { return this.isCallEstablished; }
        }

        public CallHelper()
        {
            this.isCallEstablished = false;
        }

        public void EstablishCall()
        {
            this.waveIn = new WaveIn();
            this.waveOut = new DirectSoundOut(DirectSoundOut.DSDEVID_DefaultPlayback, 50);

            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new WaveFormat(44100, 16, WaveIn.GetCapabilities(0).Channels);
            waveIn.DataAvailable += waveIn_DataAvailable;

            waveProvider = new WaveInProvider(waveIn);
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

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.DataAvailable != null) this.DataAvailable.Invoke(sender, e);
        }
    }
}
