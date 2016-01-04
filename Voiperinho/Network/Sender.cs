using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Voiperinho.Network
{
    public class Sender
    {
        private Thread senderThread;
        private NetworkStream stream;
        private bool isStreamOpen;
        private object monitor;
        private string message;

        public delegate void SenderStateChangedEventHandler(int state);
        public SenderStateChangedEventHandler SenderStateChanged;

        public Sender(NetworkStream netStream)
        {
            this.stream = netStream;
            this.isStreamOpen = true;
            this.monitor = new object();

            senderThread = new Thread(Run);
            senderThread.IsBackground = true;
            senderThread.Start();
        }

        private void Run()
        {
            lock (monitor)
            {
                while (this.isStreamOpen)
                {
                    Monitor.Wait(monitor);

                    try
                    {
                        if (!this.isStreamOpen) break;

                        if (message != string.Empty)
                        {
                            byte[] outputStream = System.Text.Encoding.UTF8.GetBytes(message);
                            string socketStringData = string.Empty;

                            stream.Write(outputStream, 0, outputStream.Length);
                            stream.Flush();
                        }
                    }
                    catch (IOException iex)
                    {
                        Console.WriteLine("Sender raised an exception: " + iex.Message);

                        if (this.SenderStateChanged != null) this.SenderStateChanged.Invoke(-1);
                    }

                    Monitor.PulseAll(monitor);
                }
            }
        }

        public void SendToSocket(string data)
        {
            lock (monitor)
            {
                message = data;
                
                Monitor.PulseAll(monitor);
                Monitor.Wait(monitor);
            }
        }

        public void StopSender()
        {
            lock (monitor)
            {
                if (this.isStreamOpen)
                    this.isStreamOpen = false;

                Monitor.PulseAll(monitor);
            }
        }
    }
}
