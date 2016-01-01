using System.Drawing;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho
{
    public partial class UserInfoContainer : UserControl
    {
        private bool isSelected;
        private bool isContact;
        private bool isRequest;
        private bool onlineStatus;
        private string username;
        private int userId;
        private Image avatar;

        public delegate void AddUserEventHandler(string username, object sender);
        public static event AddUserEventHandler AddUserPressed;

        public delegate void RequestStateEventHandler(object sender);
        public event RequestStateEventHandler RequestRejected;
        public event RequestStateEventHandler RequestAccepted;

        public bool OnlineStatus
        {
            get { return this.onlineStatus; }
            set 
            { 
                this.onlineStatus = value;
                this.SetOnlineStatus();
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.isSelected = value; }
        }
        
        public bool IsRequest
        {
            get { return this.isRequest; }
            set { this.isRequest = value; }
        }

        public string Username
        {
            get { return this.username; }
        }

        public int UserId
        {
            get { return this.userId; }
        }

        public UserInfoContainer(bool status, string username, int id, Image avatar, bool isContact = true)
        {
            InitializeComponent();

            this.onlineStatus = status;
            this.username = username;
            this.userId = id;
            this.avatar = avatar;
            this.isSelected = false;
            this.isContact = isContact;

            this.lblContactUsername.ForeColor = Color.White;
            this.lblContactUsername.Text = this.username;

            if (isContact)
                this.pboxOptions2.Image = Resources.OfflineIcon;
            else
                this.pboxOptions2.Image = Resources.inactiveAddIcon;

            if (avatar == null)
            {
                this.pboxAvatar.Image = Resources.FallbackImage;
                this.pboxAvatar.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        public UserInfoContainer(string username, int id)
        {
            InitializeComponent();

            this.username = username;
            this.userId = id;

            this.lblContactUsername.ForeColor = Color.White;
            this.lblContactUsername.Text = this.username;

            this.pboxOptions1.Image = Resources.inactiveConfirmIcon;
            this.pboxOptions2.Image = Resources.inactiveDeleteIcon;

            this.isRequest = true;

            if (avatar == null)
            {
                this.pboxAvatar.Image = Resources.FallbackImage;
                this.pboxAvatar.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        public void UpdateContacts()
        {
            this.isContact = true;
            this.isRequest = false;

            this.pboxOptions2.Image = Resources.OfflineIcon;
            this.pboxOptions1.Image = null;

            this.pboxOptions1.Invalidate();
            this.pboxOptions2.Invalidate();
        }

        private void SetOnlineStatus()
        {
            if (this.onlineStatus)
                this.pboxOptions2.Image = Resources.OnlineIcon;
            else
                this.pboxOptions2.Image = Resources.OfflineIcon;

            this.pboxOptions2.Invalidate();
        }

        public void HighlightContainer()
        {
            if (this.isSelected)
            {
                this.BackColor = Color.FromArgb(68, 88, 110);
            }
            else
            {
                this.BackColor = this.Parent.BackColor;
            }
        }

        private void pboxStatus_MouseEnter(object sender, System.EventArgs e)
        {
            if (this.isContact)
            {
                if (this.onlineStatus)
                {
                    this.informationTooltip.Show("Online", this.pboxOptions2);
                }
                else
                {
                    this.informationTooltip.Show("Offline", this.pboxOptions2);
                }
            }
            else
            {
                if (!this.isRequest)
                {
                    this.informationTooltip.Show("Send request", this.pboxOptions2);
                    this.pboxOptions2.Image = Resources.activeAddIcon;
                }
                else
                {
                    this.informationTooltip.Show("Delete request", this.pboxOptions2);
                    this.pboxOptions2.Image = Resources.activeDeleteIcon;
                }
            }
        }

        private void pboxStatus_MouseLeave(object sender, System.EventArgs e)
        {
            this.informationTooltip.RemoveAll();

            if (!this.isContact)
            {
                if (!this.isRequest)
                {
                    this.pboxOptions2.Image = Resources.inactiveAddIcon;
                }
                else
                {
                    this.pboxOptions2.Image = Resources.inactiveDeleteIcon;
                }
            }
        }

        private void pboxAvatar_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void lblContactUsername_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void pboxStatus_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.isRequest)
            {
                if (AddUserPressed != null) AddUserPressed.Invoke(this.username, this);

                this.OnMouseClick(e);
            }
            else
            {
                if (RequestRejected != null) RequestRejected.Invoke(this);
            }
        }

        private void pboxOptions1_MouseEnter(object sender, System.EventArgs e)
        {
            if (this.isRequest)
            {
                this.pboxOptions1.Image = Resources.activeConfirmIcon;
            }
        }

        private void pboxOptions1_MouseLeave(object sender, System.EventArgs e)
        {
            if (this.isRequest)
            {
                this.pboxOptions1.Image = Resources.inactiveConfirmIcon;
            }
        }

        private void pboxOptions1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.isRequest)
            {
                if (RequestAccepted != null) RequestAccepted.Invoke(this);
            }
        }
    }
}
