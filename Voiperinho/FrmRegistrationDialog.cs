using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Voiperinho.Network;
using Voiperinho.Properties;
using Voiperinho.Helpers;

namespace Voiperinho
{
    public partial class FrmRegistrationDialog : MetroForm
    {
        private string avatarPath;

        public FrmRegistrationDialog()
        {
            InitializeComponent();

            this.errorProvider.Icon = Resources.errorIcon;

            foreach (Control control in this.Controls)
            {
                if (control is Label) control.Font = MetroFramework.MetroFonts.Subtitle;
            }
        }

        private bool ValidateTextboxes()
        {
            bool isValidated = true;

            foreach (Control control in this.Controls)
            {
                if (control is MetroTextBox)
                {
                    MetroTextBox textbox = control as MetroTextBox;

                    if (control.Text == string.Empty)
                    {
                        textbox.WithError = true;
                        textbox.BackColor = Color.FromArgb(255, 235, 238);
                        
                        isValidated = false;
                        ErrorTracker.SetError(textbox, this.errorProvider);
                    }
                    else
                    {
                        textbox.BackColor = Color.White;
                        textbox.WithError = false;

                        ErrorTracker.RemoveError(textbox);
                    }
                }
            }

            ErrorTracker.DisplayError("This field cannot be empty.");

            return isValidated;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateTextboxes()) return;

            if (WebConnector.Register(this.txtUsername.Text, this.txtPassword.Text, this.txtEmail.Text, CreateBase64Image(pboxAvatarContainer.Image)))
            {
                MessageBox.Show("You have been successfully registered!", "Registration successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while trying to register your account. Please try again.", "Registration error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private string CreateBase64Image(Image image)
        {
            if (image == null) return null;

            byte[] imageBytes = ImageToByteArray(image);

            return Convert.ToBase64String(imageBytes);
        }

        private static Size ExpandToBound(Size image, Size boundingBox)
        {
            double widthScale = 0, heightScale = 0;
            if (image.Width != 0)
                widthScale = (double)boundingBox.Width / (double)image.Width;
            if (image.Height != 0)
                heightScale = (double)boundingBox.Height / (double)image.Height;

            double scale = Math.Min(widthScale, heightScale);

            Size result = new Size((int)(image.Width * scale),
                                (int)(image.Height * scale));
            return result;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                Image image = Image.FromFile(filePath);
                Size scaledImage = ExpandToBound(image.Size, this.pboxAvatarContainer.Size);
                image = new Bitmap(image, scaledImage);

                this.pboxAvatarContainer.Image = image;
                this.pboxAvatarContainer.BorderStyle = System.Windows.Forms.BorderStyle.None; ;

                this.avatarPath = filePath;
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.FromArgb(255, 255, 255);
        }
    }
}
