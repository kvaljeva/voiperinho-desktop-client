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

        public UdpReceiver(IPEndPoint endPoint)
        {
            this.isConnectionOpen = true;
            this.endPoint = endPoint;

            this.udpReceiver = new UdpClient();
            this.udpReceiver.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.udpReceiver.Client.Bind(this.endPoint);

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
