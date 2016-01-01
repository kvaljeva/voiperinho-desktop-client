namespace Voiperinho
{
    partial class RequestContainer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSendRequest = new MetroFramework.Controls.MetroButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtRequestContent = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(110, 93);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(133, 23);
            this.btnSendRequest.TabIndex = 2;
            this.btnSendRequest.Text = "Send contact request";
            this.btnSendRequest.UseSelectable = true;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 11);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(305, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Send $username a contact request with the following message:";
            // 
            // txtRequestContent
            // 
            // 
            // 
            // 
            this.txtRequestContent.CustomButton.Image = null;
            this.txtRequestContent.CustomButton.Location = new System.Drawing.Point(240, 2);
            this.txtRequestContent.CustomButton.Name = "";
            this.txtRequestContent.CustomButton.Size = new System.Drawing.Size(51, 51);
            this.txtRequestContent.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRequestContent.CustomButton.TabIndex = 1;
            this.txtRequestContent.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRequestContent.CustomButton.UseSelectable = true;
            this.txtRequestContent.CustomButton.Visible = false;
            this.txtRequestContent.Lines = new string[0];
            this.txtRequestContent.Location = new System.Drawing.Point(29, 31);
            this.txtRequestContent.MaxLength = 32767;
            this.txtRequestContent.Multiline = true;
            this.txtRequestContent.Name = "txtRequestContent";
            this.txtRequestContent.PasswordChar = '\0';
            this.txtRequestContent.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRequestContent.SelectedText = "";
            this.txtRequestContent.SelectionLength = 0;
            this.txtRequestContent.SelectionStart = 0;
            this.txtRequestContent.Size = new System.Drawing.Size(294, 56);
            this.txtRequestContent.TabIndex = 0;
            this.txtRequestContent.UseSelectable = true;
            this.txtRequestContent.WaterMark = "Enter your desired message or leave blank!";
            this.txtRequestContent.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRequestContent.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // RequestContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtRequestContent);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnSendRequest);
            this.Name = "RequestContainer";
            this.Size = new System.Drawing.Size(330, 123);
            this.Load += new System.EventHandler(this.RequestContainer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnSendRequest;
        private System.Windows.Forms.Label lblDescription;
        private MetroFramework.Controls.MetroTextBox txtRequestContent;
    }
}
