using MetroFramework.Forms;
using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho
{
    public partial class FrmAuthorizationDialog : MetroForm
    {
        private bool isLoginClicked;
        private FrmRegistrationDialog registrationDialog;
        public string Username { get; set; }
        public string Password { get; set; }

        public FrmAuthorizationDialog()
        {
            InitializeComponent();

            this.Icon = Resources.LoginIcon;
            this.lblLinkRegistration.Font = MetroFramework.MetroFonts.Subtitle;
            this.lblUsername.Font = MetroFramework.MetroFonts.Subtitle;
            this.lblPassword.Font = MetroFramework.MetroFonts.Subtitle;
            this.isLoginClicked = false;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (this.txtUsername.Text == string.Empty || this.txtPassword.Text == string.Empty)
            {
                return;
            }

            this.Username = this.txtUsername.Text;
            this.Password = this.txtPassword.Text;

            this.isLoginClicked = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmAuthorizationDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.txtUsername.Text == string.Empty || this.txtPassword.Text == string.Empty || !this.isLoginClicked)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void lblLinkRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.registrationDialog == null)
            {
                this.registrationDialog = new FrmRegistrationDialog();
                this.registrationDialog.ShowDialog();

                if (this.registrationDialog.DialogResult == DialogResult.OK || this.registrationDialog.DialogResult == DialogResult.Cancel)
                {
                    this.registrationDialog = null;
                }
            }
            else
            {
                this.registrationDialog.Focus();
            }
        }
    }
}
