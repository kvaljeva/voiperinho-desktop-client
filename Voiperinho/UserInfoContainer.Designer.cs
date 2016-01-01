namespace Voiperinho
{
    partial class UserInfoContainer
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
            this.components = new System.ComponentModel.Container();
            this.lblContactUsername = new System.Windows.Forms.Label();
            this.pboxOptions2 = new System.Windows.Forms.PictureBox();
            this.informationTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pboxOptions1 = new System.Windows.Forms.PictureBox();
            this.pboxAvatar = new Voiperinho.RoundedPicturebox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOptions2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOptions1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContactUsername
            // 
            this.lblContactUsername.AutoSize = true;
            this.lblContactUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblContactUsername.Location = new System.Drawing.Point(63, 22);
            this.lblContactUsername.Name = "lblContactUsername";
            this.lblContactUsername.Size = new System.Drawing.Size(70, 15);
            this.lblContactUsername.TabIndex = 1;
            this.lblContactUsername.Text = "$username";
            this.lblContactUsername.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblContactUsername_MouseClick);
            // 
            // pboxOptions2
            // 
            this.pboxOptions2.Location = new System.Drawing.Point(226, 22);
            this.pboxOptions2.Name = "pboxOptions2";
            this.pboxOptions2.Size = new System.Drawing.Size(16, 16);
            this.pboxOptions2.TabIndex = 2;
            this.pboxOptions2.TabStop = false;
            this.pboxOptions2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxStatus_MouseClick);
            this.pboxOptions2.MouseEnter += new System.EventHandler(this.pboxStatus_MouseEnter);
            this.pboxOptions2.MouseLeave += new System.EventHandler(this.pboxStatus_MouseLeave);
            // 
            // pboxOptions1
            // 
            this.pboxOptions1.Location = new System.Drawing.Point(197, 22);
            this.pboxOptions1.Name = "pboxOptions1";
            this.pboxOptions1.Size = new System.Drawing.Size(16, 16);
            this.pboxOptions1.TabIndex = 4;
            this.pboxOptions1.TabStop = false;
            this.pboxOptions1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxOptions1_MouseClick);
            this.pboxOptions1.MouseEnter += new System.EventHandler(this.pboxOptions1_MouseEnter);
            this.pboxOptions1.MouseLeave += new System.EventHandler(this.pboxOptions1_MouseLeave);
            // 
            // pboxAvatar
            // 
            this.pboxAvatar.Location = new System.Drawing.Point(9, 4);
            this.pboxAvatar.Name = "pboxAvatar";
            this.pboxAvatar.Size = new System.Drawing.Size(50, 50);
            this.pboxAvatar.TabIndex = 3;
            this.pboxAvatar.TabStop = false;
            this.pboxAvatar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxAvatar_MouseClick);
            // 
            // UserInfoContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pboxOptions1);
            this.Controls.Add(this.pboxAvatar);
            this.Controls.Add(this.pboxOptions2);
            this.Controls.Add(this.lblContactUsername);
            this.Name = "UserInfoContainer";
            this.Size = new System.Drawing.Size(250, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pboxOptions2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOptions1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblContactUsername;
        private System.Windows.Forms.PictureBox pboxOptions2;
        private RoundedPicturebox pboxAvatar;
        private System.Windows.Forms.ToolTip informationTooltip;
        private System.Windows.Forms.PictureBox pboxOptions1;
    }
}
