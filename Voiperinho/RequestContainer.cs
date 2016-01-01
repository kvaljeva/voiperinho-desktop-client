using System.Drawing;
using System.Windows.Forms;
using Voiperinho.Properties;
using Voiperinho.Network;
using Voiperinho.Models;
using Newtonsoft.Json;

namespace Voiperinho
{
    public partial class RequestContainer : UserControl
    {
        private string username;
        private int requesterId;
        private int receiverId;
        private SocketConnectionHelper socketHelper;
        private AccountInformation sender;

        public RequestContainer(string username, int requester, int receiver, SocketConnectionHelper helper, AccountInformation accountInfo)
        {
            InitializeComponent();

            this.username = username;
            this.requesterId = requester;
            this.receiverId = receiver;
            this.socketHelper = helper;
            this.sender = accountInfo;
        }

        private void RequestContainer_Load(object sender, System.EventArgs e)
        {
            this.lblDescription.Text = this.lblDescription.Text.Replace("$username", this.username);
        }

        private void btnSendRequest_Click(object sender, System.EventArgs e)
        {
            if (WebConnector.SendRequest(this.txtRequestContent.Text, requesterId, receiverId))
            {
                MessageBox.Show("Your request was succesffully sent!", "Request sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RequestInformation request = new RequestInformation();
                request.Id = 0; // Fix ID -> get it as the server's response
                request.RequesterId = this.requesterId;
                request.Requester = this.sender;
                request.UserId = this.receiverId;
                request.State = 1;
                request.RequestText = this.txtRequestContent.Text;

                string jsonRequest = JsonConvert.SerializeObject(request);

                socketHelper.SendMessage(this.username, jsonRequest, this.sender.Username, @"/request");

                // Ask parent to remove oneself from it's controls list
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Whoops, there was an issue while sending your request. Please try again.", "Request sending failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
