namespace Voiperinho
{
    partial class IncomingCallDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncomingCallDialog));
            this.lblDescription = new System.Windows.Forms.Label();
            this.pboxAcceptCall = new System.Windows.Forms.PictureBox();
            this.pboxDeclineCall = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxAcceptCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxDeclineCall)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.ForeColor = System.Drawing.Color.LightGray;
            this.lblDescription.Location = new System.Drawing.Point(0, 43);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(129, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Incoming call from: $caller";
            // 
            // pboxAcceptCall
            // 
            this.pboxAcceptCall.Image = ((System.Drawing.Image)(resources.GetObject("pboxAcceptCall.Image")));
            this.pboxAcceptCall.Location = new System.Drawing.Point(592, 35);
            this.pboxAcceptCall.Name = "pboxAcceptCall";
            this.pboxAcceptCall.Size = new System.Drawing.Size(32, 32);
            this.pboxAcceptCall.TabIndex = 2;
            this.pboxAcceptCall.TabStop = false;
            this.pboxAcceptCall.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxAcceptCall_MouseClick);
            this.pboxAcceptCall.MouseEnter += new System.EventHandler(this.pboxAcceptCall_MouseEnter);
            this.pboxAcceptCall.MouseLeave += new System.EventHandler(this.pboxAcceptCall_MouseLeave);
            // 
            // pboxDeclineCall
            // 
            this.pboxDeclineCall.Image = global::Voiperinho.Properties.Resources.DeclineCallIcon;
            this.pboxDeclineCall.Location = new System.Drawing.Point(636, 35);
            this.pboxDeclineCall.Name = "pboxDeclineCall";
            this.pboxDeclineCall.Size = new System.Drawing.Size(32, 32);
            this.pboxDeclineCall.TabIndex = 1;
            this.pboxDeclineCall.TabStop = false;
            this.pboxDeclineCall.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pboxDeclineCall_MouseClick);
            this.pboxDeclineCall.MouseEnter += new System.EventHandler(this.pboxDeclineCall_MouseEnter);
            this.pboxDeclineCall.MouseLeave += new System.EventHandler(this.pboxDeclineCall_MouseLeave);
            // 
            // IncomingCallDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.Controls.Add(this.pboxAcceptCall);
            this.Controls.Add(this.pboxDeclineCall);
            this.Controls.Add(this.lblDescription);
            this.Name = "IncomingCallDialog";
            this.Size = new System.Drawing.Size(685, 100);
            ((System.ComponentModel.ISupportInitialize)(this.pboxAcceptCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxDeclineCall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.PictureBox pboxDeclineCall;
        private System.Windows.Forms.PictureBox pboxAcceptCall;
    }
}
