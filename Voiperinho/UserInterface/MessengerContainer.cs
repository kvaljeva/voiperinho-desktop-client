using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho.UserInterface
{
    public partial class MessengerContainer : UserControl
    {
        private bool clearingText;
        private bool initiatingCall;
        private FrmMessengerDialog parent;
        private AccountInformation accountInfo;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public MetroTextBox inputContainer
        {
            get { return this.txtInputContainer; }
        }

        public bool InitiatingCall
        {
            get { return this.initiatingCall; }
            set { this.initiatingCall = value; }
        }

        public ListBox commandsContainer
        {
            get { return this.infoBox; }
        }

        public IncomingCallDialog callDialog
        {
            get { return this.incomingCallDialog; }
        }

        public delegate void InputBoxFocusedEventHandler();
        public event InputBoxFocusedEventHandler InputBoxFocused;

        public MessengerContainer(FrmMessengerDialog form, AccountInformation account)
        {
            InitializeComponent();

            this.initiatingCall = false;
            this.parent = form;
            this.accountInfo = account;
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

            this.txtResponseContainer.Font = MetroFonts.Subtitle;
            this.txtInputContainer.WaterMarkFont = MetroFonts.Subtitle;
        }

        public void CreateCallDialog(string caller)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => CreateCallDialog(caller)));
                return;
            }

            this.incomingCallDialog.Visible = true;
            this.incomingCallDialog.Caller = caller;
        }

        public void UpdateCallState()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateCallState()));
                return;
            }

            if (this.parent.SocketHelper.IsUdpConnectionMapped)
            {
                this.btnCall.Image = Resources.EndCallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "End call");
            }
            else
            {
                this.btnCall.Image = Resources.CallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "Initiate call");

                if (this.incomingCallDialog.Visible)
                    this.incomingCallDialog.Visible = false;
            }

            this.btnCall.Invalidate();
        }

        public void AppendText(string content)
        {
            this.txtResponseContainer.AppendText(content);
        }

        public void AppendText(Message message)
        {
            if (message.Sender != null && message.Sender != string.Empty)
            {
                FormatText(this.txtResponseContainer, "\t" + message.Sender + " ", Color.Black, new Font(this.txtResponseContainer.Font, FontStyle.Bold));
                FormatText(this.txtResponseContainer, message.Timestamp, Color.Silver, new Font("Segoe UI", 9, FontStyle.Regular));
                FormatText(this.txtResponseContainer, Environment.NewLine + "\t" + message.Content + Environment.NewLine, Color.Black, new Font(this.txtResponseContainer.Font, FontStyle.Regular));
            }
            else
            {
                txtResponseContainer.AppendText("\t" + message.Content + Environment.NewLine);
            }
        }

        private static void FormatText(RichTextBox container, string content, Color color, Font font)
        {
            container.SelectionStart = container.TextLength;
            container.SelectionLength = 0;

            container.SelectionColor = color;
            container.SelectionFont = font;
            container.AppendText(content);
            container.SelectionColor = container.ForeColor;
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
                this.inputContainer.Text = string.Empty;
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
            else if (e.Control && e.KeyCode == Keys.A)
            {
                txtInputContainer.SelectAll();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (this.txtInputContainer.Text != string.Empty && parent.SocketHelper.IsConnectionEstablished)
                {
                    string selectedContact = GetSelectedContact();

                    if (selectedContact == string.Empty) return;

                    this.parent.SocketHelper.SendMessage(selectedContact, this.accountInfo.Username, this.txtInputContainer.Text);
                    this.inputContainer.Text = string.Empty;
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
            string selectedContact = this.GetSelectedContact();

            if (this.initiatingCall)
            {
                this.parent.CallSoundPlayer.Stop();
                this.parent.SocketHelper.ForceCloseCall(selectedContact, this.accountInfo.Username, "");

                this.initiatingCall = false;
                this.btnCall.Image = Resources.CallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "Initiate call");

                return;
            }

            if (this.parent.SocketHelper.CallHelper != null && this.parent.SocketHelper.CallHelper.IsCallEstablished)
            {
                this.initiatingCall = false;
                this.btnCall.Image = Resources.CallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "Initiate call");

                this.parent.SocketHelper.CloseCall(selectedContact, this.accountInfo.Username, this.parent.SocketHelper.CallHelper.CallId);

                return;
            }

            if (selectedContact != string.Empty)
            {
                this.parent.SocketHelper.SendMessage(selectedContact, this.accountInfo.Username, this.accountInfo.Username, "/call");

                this.initiatingCall = true;
                this.btnCall.Image = Resources.EndCallIcon;
                this.toolTipInfo.SetToolTip(btnCall, "End call");
            }
        }

        private void btnCall_MouseEnter(object sender, EventArgs e)
        {
            // In case that the call is being established, just set the icon to end the call
            if (this.initiatingCall)
            {
                this.btnCall.Image = Resources.ActiveEndCallIcon;
                return;
            }

            if (this.parent.SocketHelper.IsUdpConnectionMapped)
            {
                this.btnCall.Image = Resources.ActiveEndCallIcon;
            }
            else
            {
                this.btnCall.Image = Resources.ActiveCallIcon;
            }

            this.btnCall.Invalidate();
        }

        private void btnCall_MouseLeave(object sender, EventArgs e)
        {
            // In case that the call is being established, just set the icon to end the call
            if (this.initiatingCall)
            {
                this.btnCall.Image = Resources.EndCallIcon;
                return;
            }

            if (this.parent.SocketHelper.IsUdpConnectionMapped)
            {
                this.btnCall.Image = Resources.EndCallIcon;
            }
            else
            {
                this.btnCall.Image = Resources.CallIcon;
            }

            this.btnCall.Invalidate();
        }

        private void txtResponseContainer_TextChanged(object sender, EventArgs e)
        {
            txtResponseContainer.SelectionStart = txtResponseContainer.Text.Length;
            txtResponseContainer.ScrollToCaret();
        }

        private void txtInputContainer_Click(object sender, EventArgs e)
        {
            if (this.InputBoxFocused != null) this.InputBoxFocused.Invoke();
        }
    }
}
