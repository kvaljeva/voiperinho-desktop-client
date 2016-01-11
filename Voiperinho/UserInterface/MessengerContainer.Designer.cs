namespace Voiperinho.UserInterface
{
    partial class MessengerContainer
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
            this.infoBox = new System.Windows.Forms.ListBox();
            this.lblInputDescription = new System.Windows.Forms.Label();
            this.txtInputContainer = new MetroFramework.Controls.MetroTextBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.btnCall = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtResponseContainer = new System.Windows.Forms.RichTextBox();
            this.incomingCallDialog = new Voiperinho.UserInterface.IncomingCallDialog();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.FormattingEnabled = true;
            this.infoBox.Items.AddRange(new object[] {
            "/ping",
            "/call",
            "/disconnect "});
            this.infoBox.Location = new System.Drawing.Point(244, 333);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(120, 56);
            this.infoBox.TabIndex = 12;
            this.infoBox.Visible = false;
            this.infoBox.SelectedIndexChanged += new System.EventHandler(this.infoBox_SelectedIndexChanged);
            this.infoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.infoBox_KeyDown);
            // 
            // lblInputDescription
            // 
            this.lblInputDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInputDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(62)))), ((int)(((byte)(77)))));
            this.lblInputDescription.Location = new System.Drawing.Point(0, 388);
            this.lblInputDescription.Name = "lblInputDescription";
            this.lblInputDescription.Size = new System.Drawing.Size(683, 1);
            this.lblInputDescription.TabIndex = 10;
            this.lblInputDescription.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblInputDescription_MouseClick);
            // 
            // txtInputContainer
            // 
            this.txtInputContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputContainer.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtInputContainer.CustomButton.Image = null;
            this.txtInputContainer.CustomButton.Location = new System.Drawing.Point(548, 1);
            this.txtInputContainer.CustomButton.Name = "";
            this.txtInputContainer.CustomButton.Size = new System.Drawing.Size(41, 41);
            this.txtInputContainer.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtInputContainer.CustomButton.TabIndex = 1;
            this.txtInputContainer.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtInputContainer.CustomButton.UseSelectable = true;
            this.txtInputContainer.CustomButton.Visible = false;
            this.txtInputContainer.Lines = new string[0];
            this.txtInputContainer.Location = new System.Drawing.Point(7, 397);
            this.txtInputContainer.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtInputContainer.MaxLength = 32767;
            this.txtInputContainer.Multiline = true;
            this.txtInputContainer.Name = "txtInputContainer";
            this.txtInputContainer.PasswordChar = '\0';
            this.txtInputContainer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtInputContainer.SelectedText = "";
            this.txtInputContainer.SelectionLength = 0;
            this.txtInputContainer.SelectionStart = 0;
            this.txtInputContainer.Size = new System.Drawing.Size(590, 43);
            this.txtInputContainer.TabIndex = 0;
            this.txtInputContainer.UseSelectable = true;
            this.txtInputContainer.WaterMark = "Type your message here";
            this.txtInputContainer.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtInputContainer.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtInputContainer.TextChanged += new System.EventHandler(this.txtInputContainer_TextChanged);
            this.txtInputContainer.Click += new System.EventHandler(this.txtInputContainer_Click);
            this.txtInputContainer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputContainer_KeyDown);
            this.txtInputContainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtInputContainer_MouseClick);
            // 
            // btnCall
            // 
            this.btnCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCall.FlatAppearance.BorderSize = 0;
            this.btnCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCall.Image = global::Voiperinho.Properties.Resources.CallIcon;
            this.btnCall.Location = new System.Drawing.Point(637, 394);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(36, 43);
            this.btnCall.TabIndex = 13;
            this.toolTipInfo.SetToolTip(this.btnCall, "Initiate call");
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            this.btnCall.MouseEnter += new System.EventHandler(this.btnCall_MouseEnter);
            this.btnCall.MouseLeave += new System.EventHandler(this.btnCall_MouseLeave);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Image = global::Voiperinho.Properties.Resources.SendIcon;
            this.btnSend.Location = new System.Drawing.Point(599, 394);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(36, 43);
            this.btnSend.TabIndex = 9;
            this.toolTipInfo.SetToolTip(this.btnSend, "Send message");
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.MouseEnter += new System.EventHandler(this.btnSend_MouseEnter);
            this.btnSend.MouseLeave += new System.EventHandler(this.btnSend_MouseLeave);
            // 
            // txtResponseContainer
            // 
            this.txtResponseContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponseContainer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponseContainer.Location = new System.Drawing.Point(0, 0);
            this.txtResponseContainer.Name = "txtResponseContainer";
            this.txtResponseContainer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtResponseContainer.Size = new System.Drawing.Size(683, 377);
            this.txtResponseContainer.TabIndex = 14;
            this.txtResponseContainer.Text = "";
            this.txtResponseContainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtResponseContainer_MouseClick);
            this.txtResponseContainer.TextChanged += new System.EventHandler(this.txtResponseContainer_TextChanged);
            this.txtResponseContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtResponseContainer_MouseDown);
            this.txtResponseContainer.MouseEnter += new System.EventHandler(this.txtResponseContainer_MouseEnter);
            // 
            // incomingCallDialog
            // 
            this.incomingCallDialog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.incomingCallDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(58)))));
            this.incomingCallDialog.Location = new System.Drawing.Point(0, 0);
            this.incomingCallDialog.Name = "incomingCallDialog";
            this.incomingCallDialog.Size = new System.Drawing.Size(683, 100);
            this.incomingCallDialog.TabIndex = 15;
            this.incomingCallDialog.Visible = false;
            // 
            // MessengerContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.incomingCallDialog);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.txtResponseContainer);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.txtInputContainer);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblInputDescription);
            this.Name = "MessengerContainer";
            this.Size = new System.Drawing.Size(683, 440);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessagerContainer_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox infoBox;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblInputDescription;
        private MetroFramework.Controls.MetroTextBox txtInputContainer;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.RichTextBox txtResponseContainer;
        private IncomingCallDialog incomingCallDialog;
    }
}
