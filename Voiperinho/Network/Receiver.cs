using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Voiperinho.Network
{
    public class Receiver
    {
        private Thread recvThread;
        private NetworkStream stream;
        private int bufferSize;
        private bool isStreamOpen;

        public delegate void DataReceivedEventHandler(string data);
        public DataReceivedEventHandler OnDataReceived;

        public Receiver(NetworkStream stream, int buffSize)
        {
            this.isStreamOpen = true;
            this.stream = stream;
            this.bufferSize = buffSize;

            recvThread = new Thread(Run);
            recvThread.Start();
        }

        private void Run()
        {
            try
            {
                while (this.isStreamOpen)
                {
                    try
                    {
                        if (!stream.DataAvailable)
                        {
                            Thread.Sleep(1);
                        }
                        else
                        {
                            string socketStringData = string.Empty;
                            byte[] inputStream = new byte[65536];

                            stream.Read(inputStream, 0, this.bufferSize);
                            socketStringData = System.Text.Encoding.UTF8.GetString(inputStream);

                            if (OnDataReceived != null) this.OnDataReceived.Invoke(socketStringData);
                        }

                    }
                    catch (IOException iex)
                    {
                        Console.WriteLine("Receiver raised an exception: " + iex.Message);

                        this.isStreamOpen = false;

                        if (OnDataReceived != null) this.OnDataReceived.Invoke(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receiver raised an exception: " + ex.Message);

                this.isStreamOpen = false;

                if (OnDataReceived != null) this.OnDataReceived.Invoke(string.Empty);
            }
            finally
            {
                this.stream.Close();
            }
        }

        public void StopReceiver()
        {
            this.isStreamOpen = false;
        }
    }
}
