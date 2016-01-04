using System.Windows.Forms;
using Voiperinho.Properties;

namespace Voiperinho
{
    public partial class frmAuthorizationDialog : Form
    {
        private bool isLoginClicked;
        public string Username { get; set; }
        public string Password { get; set; }

        public frmAuthorizationDialog()
        {
            InitializeComponent();

            this.Icon = Resources.LoginIcon;
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
    }
}
