using MetroFramework;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho
{
    public partial class MessagerContainer : UserControl
    {
        private CallHelper callHelper;
        private bool clearingText;
        private frmMessengerDialog parent;
        private AccountInformation accountInfo;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public MetroFramework.Controls.MetroTextBox inputContainer
        {
            get { return this.txtInputContainer; }
        }

        public ListBox commandsContainer
        {
            get { return this.infoBox; }
        }

        public MessagerContainer(frmMessengerDialog form, AccountInformation account)
        {
            InitializeComponent();

            this.parent = form;
            this.accountInfo = account;
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

            this.txtResponseContainer.Font = MetroFonts.Subtitle;
            this.txtInputContainer.WaterMarkFont = MetroFonts.Subtitle;
        }

        public void AppendText(string content)
        {
            this.txtResponseContainer.AppendText(content);
        }

        private void MessagerContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.infoBox != null && this.infoBox.Visible)
            {
                this.infoBox.Hide();
                this.txtInputContainer.Text = string.Empty;
            }
        }

        private void txtInputContainer_TextChanged(object sender, EventArgs e)
        {
            if (this.txtInputContainer.Text.Length > 1) return;

            if (this.txtInputContainer.Text != string.Empty && this.txtInputContainer.Text[0] == '/' && !clearingText)
            {
                this.infoBox.Show();
                this.infoBox.BringToFront();
                this.infoBox.Focus();
            }
            else
            {
                this.infoBox.Hide();
                this.txtInputContainer.Focus();
            }
        }

        private void infoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.infoBox.Hide();

                if (this.txtInputContainer.Text.Length == 1)
                {
                    if (this.infoBox.Text.Contains("["))
                        this.txtInputContainer.Text = this.infoBox.Text.Remove(this.infoBox.Text.IndexOf('['));
                    else
                        this.txtInputContainer.Text = this.infoBox.Text;
                }

                this.txtInputContainer.SelectionStart = this.txtInputContainer.Text.Length + 1;
                this.txtInputContainer.SelectionLength = 0;
                this.txtInputContainer.Focus();
            }
        }

        private void infoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.infoBox.Text.Contains("["))
            {
                this.txtInputContainer.Text = this.infoBox.Text.Remove(this.infoBox.Text.IndexOf('['));
                return;
            }

            this.txtInputContainer.Text = this.infoBox.Text;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (this.txtInputContainer.Text != string.Empty && this.parent.SocketHelper.IsConnectionEstablished)
            {
                string selectedContact = string.Empty;

                foreach (UserInfoContainer container in this.parent.ListContactContainer)
                {
                    if (container.IsSelected)
                    {
                        selectedContact = container.Username;
                        break;
                    }
                }

                if (selectedContact == string.Empty) return;

                this.parent.SocketHelper.SendMessage(selectedContact, this.accountInfo.Username, this.txtInputContainer.Text);
            }
        }

        private void btnSend_MouseEnter(object sender, EventArgs e)
        {
            this.btnSend.Image = Resources.SendIconActive;
            this.btnSend.Invalidate();
        }

        private void btnSend_MouseLeave(object sender, EventArgs e)
        {
            this.btnSend.Image = Resources.SendIcon;
            this.btnSend.Invalidate();
        }

        private string GetSelectedContact()
        {
            foreach (UserInfoContainer container in this.parent.ListContactContainer)
            {
                if (container.IsSelected)
                {
                    return container.Username;
                }
            }

            return string.Empty;
        }

        private void txtInputContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                this.clearingText = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (this.txtInputContainer.Text != string.Empty && parent.SocketHelper.IsConnectionEstablished)
                {
                    string selectedContact = GetSelectedContact();

                    if (selectedContact == string.Empty) return;

                    this.parent.SocketHelper.SendMessage(selectedContact, this.accountInfo.Username, this.txtInputContainer.Text);
                }
            }

            this.clearingText = false;
        }

        private void txtResponseContainer_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void txtInputContainer_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void lblInputDescription_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void txtResponseContainer_MouseEnter(object sender, EventArgs e)
        {
            HideCaret(txtResponseContainer.Handle);
        }

        private void txtResponseContainer_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(txtResponseContainer.Handle);
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (this.callHelper == null || !this.callHelper.IsCallEstablished)
            {
                if (!this.parent.SocketHelper.InititateCallConnection())
                {
                    MessageBox.Show("An error ocurred while trying to establish your call. Please try again.", "Error establishing call", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.btnCall.Image = Resources.EndCallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "End call");

                this.callHelper = new CallHelper();
                this.callHelper.DataAvailable += callHelper_DataAvailable;

                callHelper.EstablishCall();
            }
            else
            {
                this.callHelper.CloseCall();

                this.btnCall.Image = Resources.CallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "Initiate call");
            }
        }

        private void callHelper_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            string selectedContact = this.GetSelectedContact();

            if (selectedContact == string.Empty) return;
        }
    }
}
