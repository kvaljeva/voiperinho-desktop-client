using System.Drawing;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho.UserInterface
{
    public partial class IncomingCallDialog : UserControl
    {
        private string caller;
        private bool isCallAccepted;

        public string Caller
        {
            set 
            { 
                this.caller = value;
                this.lblDescription.Text = this.lblDescription.Text.Replace("$caller", value);
            }
        }

        public bool IsCallAccepted
        {
            get { return this.isCallAccepted; }
        }

        public delegate void CallDialogStateChanged(bool state, string caller);
        public event CallDialogStateChanged CallStateChanged;

        public IncomingCallDialog()
        {
            InitializeComponent();

            this.lblDescription.Font = MetroFramework.MetroFonts.Title;
            this.lblDescription.Location = new Point(this.lblDescription.Location.X, this.Height / 2 - this.lblDescription.Height / 2);

            // Just hide it from the user, until the someone initiates a call
            this.Visible = false;
        }

        private void HandleClick(bool value)
        {
            this.isCallAccepted = value;
            this.Parent.Controls[this.Name].Hide();

            if (this.CallStateChanged != null) CallStateChanged.Invoke(value, this.caller);
        }

        private void pboxDeclineCall_MouseEnter(object sender, System.EventArgs e)
        {
            this.pboxDeclineCall.Image = Resources.ActiveDeclineCallIcon;
            this.pboxDeclineCall.Invalidate();
        }

        private void pboxDeclineCall_MouseLeave(object sender, System.EventArgs e)
        {
            this.pboxDeclineCall.Image = Resources.DeclineCallIcon;
            this.pboxDeclineCall.Invalidate();
        }

        private void pboxDeclineCall_MouseClick(object sender, MouseEventArgs e)
        {
            this.HandleClick(false);
        }

        private void pboxAcceptCall_MouseEnter(object sender, System.EventArgs e)
        {
            this.pboxAcceptCall.Image = Resources.ActiveAcceptCallIcon;
            this.pboxAcceptCall.Invalidate();
        }

        private void pboxAcceptCall_MouseLeave(object sender, System.EventArgs e)
        {
            this.pboxAcceptCall.Image = Resources.AcceptCallIcon;
            this.pboxAcceptCall.Invalidate();
        }

        private void pboxAcceptCall_MouseClick(object sender, MouseEventArgs e)
        {
            this.HandleClick(true);
        }
    }
}
