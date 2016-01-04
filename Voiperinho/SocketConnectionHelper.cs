using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Voiperinho.Network;

namespace Voiperinho
{
    public class SocketConnectionHelper
    {
        private NetworkStream serverStream;
        private TcpClient clientSocket;
        private UdpClient udpSender;
        private UdpReceiver udpReceiver;
        private CallHelper callHelper;
        private Receiver receiver;
        private Sender sender;
        private bool isAuthorized;
        private bool isConnectionEstablished;
        private bool isHandshake;

        private List<string> commandsList;

        public delegate void SocketDataReceivedEventHandler(string data);
        public SocketDataReceivedEventHandler SocketDataReceived;

        public delegate void ServerSocketCloseEventHandler();
        public ServerSocketCloseEventHandler ServerSocketClosed;

        public bool IsAuthorized
        {
            get { return this.isAuthorized; }
            set { this.isAuthorized = value; }
        }

        public bool IsConnectionEstablished
        {
            get { return this.isConnectionEstablished; }
            set { this.isConnectionEstablished = value; }
        }

        public bool IsHandshake
        {
            get { return this.isHandshake; }
            set { this.isHandshake = value; }
        }

        public CallHelper CallHelper
        {
            get { return this.callHelper; }
        }

        // Constructor
        public SocketConnectionHelper()
        {
            this.isAuthorized = false;
            this.isConnectionEstablished = false;

            commandsList = new List<string>();
            commandsList.Add(@"/disconnect");
            commandsList.Add(@"/ping");
            commandsList.Add(@"/call");
            commandsList.Add(@"/request");
            commandsList.Add(@"/accept");
        }

        public void CloseConnection()
        {
            this.isAuthorized = false;
            this.isConnectionEstablished = false;

            this.receiver.OnDataReceived -= receiver_onDataReceived;
            this.sender.SenderStateChanged -= sender_onStateChanged;

            this.receiver.StopReceiver();
            this.sender.StopSender();

            this.clientSocket.Close();

            if (this.callHelper != null)
                this.callHelper.CloseCall();
        }

        private void SendToSocket(string message)
        {
            sender.SendToSocket(message);
        }

        public bool TryConnect()
        {
            try
            {
                this.clientSocket = new TcpClient();
                this.clientSocket.Connect("127.0.0.1", 9999);
                this.serverStream = clientSocket.GetStream();

                this.isConnectionEstablished = true;
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);

                this.isConnectionEstablished = false;
                return false;
            }

            this.sender = new Sender(serverStream);
            this.receiver = new Receiver(serverStream, (int)clientSocket.ReceiveBufferSize);
            this.receiver.OnDataReceived += receiver_onDataReceived;
            this.sender.SenderStateChanged += sender_onStateChanged;

            return true;
        }

        private void sender_onStateChanged(int state)
        {
            if (state == -1)
            {
                if (this.ServerSocketClosed != null) this.ServerSocketClosed.Invoke();

                this.CloseConnection();
            }
        }

        private void receiver_onDataReceived(string data)
        {
            if (this.SocketDataReceived != null && data != string.Empty) this.SocketDataReceived.Invoke(data);
        }

        public bool TryAuthorize(string username, string password)
        {
            string authorizeMessage = "{ \"username\": \"" + username + "\", \"password\": \"" + password + "\" }\n";
            string rcvMessage = string.Empty;

            this.SendToSocket(authorizeMessage);

            this.isHandshake = true;
            return true;
        }

        public void Disconnect()
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                this.SendToSocket(Message.FormatMessageString("", "", "", "/disconnect"));
            }
        }

        public void SendMessage(string contact, string sender, string content)
        {
            if (!this.isAuthorized)
                return;

            string command = string.Empty;
            foreach (string cmd in this.commandsList)
            {
                if (content.Contains(cmd))
                {
                    command = cmd;
                    content.Replace(cmd, "");
                    break;
                }
            }

            this.SendToSocket(Message.FormatMessageString(contact, content, sender, command));
        }

        public void SendMessage(string contact, string content, string sender, string command)
        {
            if (!this.isAuthorized)
                return;

            this.SendToSocket(Message.FormatMessageString(contact, content, sender, command));
        }

        private bool InititateCallConnection()
        {
            if (!this.isConnectionEstablished) return false;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);

            udpReceiver = new UdpReceiver(endPoint);
            udpReceiver.SocketDataReceived += udpReceiver_SocketDataReceived;

            udpSender = new UdpClient();
            udpSender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                udpSender.Connect(endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to establish call connection: " + ex.Message);
                this.udpSender.Close();

                return false;
            }

            return true;
        }

        private void udpReceiver_SocketDataReceived(byte[] data)
        {
            this.callHelper.AddSamples(data);
        }

        public void EstablishCall(string id = "")
        {
            if (this.InititateCallConnection())
            {
                this.callHelper = new CallHelper();
                this.callHelper.DataAvailable += callHelper_DataAvailable;
                
                // Start recording voice to byte array
                this.callHelper.EstablishCall(id);
            }
        }
    
        public void CloseCall(string receiver, string sender, string id)
        {
            if (this.callHelper != null && this.callHelper.IsCallEstablished)
            {
                this.udpReceiver.StopReceiver();
                this.udpReceiver.SocketDataReceived -= udpReceiver_SocketDataReceived;

                this.callHelper.CloseCall();
                this.callHelper.DataAvailable -= callHelper_DataAvailable;

                this.SendMessage(receiver, id, sender, "/call/close");
            }
        }

        public void ForceCloseCall(string receiver, string sender, string id)
        {
            this.SendMessage(receiver, id, sender, "/call/close");
        }

        private void callHelper_DataAvailable(byte[] encodedData)
        {
            this.SendUdpPacket(encodedData);
        }

        private void SendUdpPacket(byte[] bytesArray)
        {
            udpSender.Send(bytesArray, bytesArray.Length);
        }
    }
}
