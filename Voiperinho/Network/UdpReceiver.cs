using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Voiperinho.Network
{
    public class UdpReceiver
    {
        private UdpClient udpReceiver;
        private Thread recvThread;
        private IPEndPoint endPoint;
        private bool isConnectionOpen;

        public delegate void DataReceivedEventHandler(byte[] data);
        public event DataReceivedEventHandler SocketDataReceived;

        public UdpReceiver(UdpClient client)
        {
            this.isConnectionOpen = true;

            this.udpReceiver = client;
            this.endPoint = new IPEndPoint(IPAddress.Any, 9999);

            this.recvThread = new Thread(Run);
            this.recvThread.IsBackground = true;
            this.recvThread.Start();
        }

        private void Run()
        {
            try
            {
                while (this.isConnectionOpen)
                {
                    byte[] data = this.udpReceiver.Receive(ref this.endPoint);

                    Console.WriteLine("Data received.");

                    if (this.SocketDataReceived != null) this.SocketDataReceived.Invoke(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UDP receiver raised an exception: " + ex.Message);

                this.isConnectionOpen = false;
            }
            finally
            {
                this.udpReceiver.Close();
            }
        }

        public void StopReceiver()
        {
            this.isConnectionOpen = false;
        }
    }
}
