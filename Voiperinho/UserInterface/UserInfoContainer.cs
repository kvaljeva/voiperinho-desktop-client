using System.Drawing;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho.UserInterface
{
    public partial class UserInfoContainer : UserControl
    {
        private bool isSelected;
        private bool isContact;
        private bool isRequest;
        private bool onlineStatus;
        private string username;
        private string requestNote;
        private int userId;
        private Image avatar;

        public delegate void AddUserEventHandler(string username, object sender);
        public event AddUserEventHandler AddUserPressed;

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
        
        public bool IsNotificationReceived
        {
            get { return this.pboxNotification.Visible; }
            set 
            { 
                this.pboxNotification.Visible = value;

                if (value)
                {
                    this.lblContactUsername.Font = new Font(this.lblContactUsername.Font, FontStyle.Bold);
                }
                else
                {
                    this.lblContactUsername.Font = new Font(this.lblContactUsername.Font, FontStyle.Regular);
                }
            }
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

        public string RequestNote
        {
            get { return this.requestNote; }
            set { this.requestNote = value; }
        }

        public UserInfoContainer(bool status, string username, int id, Image avatar, bool isContact = true)
        {
            InitializeComponent();

            this.onlineStatus = status;
            this.username = username;
            this.userId = id;
            this.avatar = avatar;
            this.isSelected = false;
            this.pboxNotification.Visible = false;
            this.pboxNotification.Parent = this.pboxAvatar;
            this.isContact = isContact;

            this.lblContactUsername.ForeColor = Color.White;
            this.lblContactUsername.Text = this.username;
            this.lblContactUsername.Font = MetroFramework.MetroFonts.Subtitle;

            if (isContact)
                this.pboxOptions2.Image = Resources.OfflineIcon;
            else
                this.pboxOptions2.Image = Resources.InactiveAddIcon;

            if (avatar == null)
            {
                this.pboxAvatar.Image = Resources.FallbackImage;
                this.pboxAvatar.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        public UserInfoContainer(string username, int id, string requestText)
        {
            InitializeComponent();

            this.username = username;
            this.userId = id;
            this.requestNote = requestText;

            this.pboxNotification.Visible = false;
            this.pboxNotification.Parent = this.pboxAvatar;
            this.lblContactUsername.ForeColor = Color.White;
            this.lblContactUsername.Text = this.username;

            this.pboxOptions1.Image = Resources.InactiveConfirmIcon;
            this.pboxOptions2.Image = Resources.InactiveDeleteIcon;

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
                    this.pboxOptions2.Image = Resources.ActiveAddIcon;
                }
                else
                {
                    this.informationTooltip.Show("Delete request", this.pboxOptions2, new Point(this.pboxOptions2.Location.X - this.Parent.Width + 45, this.pboxOptions2.Location.Y - this.pboxOptions2.Height));
                    this.pboxOptions2.Image = Resources.ActiveDeleteIcon;
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
                    this.pboxOptions2.Image = Resources.InactiveAddIcon;
                }
                else
                {
                    this.pboxOptions2.Image = Resources.InactiveDeleteIcon;
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
                this.informationTooltip.Show("Accept request", this.pboxOptions1, new Point(this.pboxOptions1.Location.X - this.Parent.Width + 75, this.pboxOptions1.Location.Y - this.pboxOptions1.Height));
                this.pboxOptions1.Image = Resources.ActiveConfirmIcon;
            }
        }

        private void pboxOptions1_MouseLeave(object sender, System.EventArgs e)
        {
            this.informationTooltip.RemoveAll();

            if (this.isRequest)
            {
                this.pboxOptions1.Image = Resources.InactiveConfirmIcon;
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
