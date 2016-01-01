namespace Voiperinho
{
    partial class frmMessengerDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessengerDialog));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.voiperinhoItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addContactItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlContactsContainer = new System.Windows.Forms.Panel();
            this.lblContactsList = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.tooltipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.lblResponseDescription = new System.Windows.Forms.Label();
            this.pboxLogo = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.pnlContactsContainer.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.voiperinhoItem,
            this.contactsItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(934, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // voiperinhoItem
            // 
            this.voiperinhoItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectItem,
            this.disconnectItem});
            this.voiperinhoItem.Name = "voiperinhoItem";
            this.voiperinhoItem.Size = new System.Drawing.Size(76, 20);
            this.voiperinhoItem.Text = "Voiperinho";
            // 
            // connectItem
            // 
            this.connectItem.Name = "connectItem";
            this.connectItem.Size = new System.Drawing.Size(133, 22);
            this.connectItem.Text = "Connect";
            this.connectItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectItem
            // 
            this.disconnectItem.Enabled = false;
            this.disconnectItem.Name = "disconnectItem";
            this.disconnectItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectItem.Text = "Disconnect";
            this.disconnectItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // contactsItem
            // 
            this.contactsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addContactItem});
            this.contactsItem.Name = "contactsItem";
            this.contactsItem.Size = new System.Drawing.Size(66, 20);
            this.contactsItem.Text = "Contacts";
            // 
            // addContactItem
            // 
            this.addContactItem.Name = "addContactItem";
            this.addContactItem.Size = new System.Drawing.Size(139, 22);
            this.addContactItem.Text = "Add contact";
            this.addContactItem.Click += new System.EventHandler(this.addContactItem_Click);
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlDashboard.AutoScroll = true;
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(62)))), ((int)(((byte)(77)))));
            this.pnlDashboard.Controls.Add(this.pnlContactsContainer);
            this.pnlDashboard.Controls.Add(this.txtSearch);
            this.pnlDashboard.Location = new System.Drawing.Point(0, 24);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(250, 478);
            this.pnlDashboard.TabIndex = 8;
            // 
            // pnlContactsContainer
            // 
            this.pnlContactsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContactsContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(62)))), ((int)(((byte)(77)))));
            this.pnlContactsContainer.Controls.Add(this.lblContactsList);
            this.pnlContactsContainer.Controls.Add(this.lblSeparator);
            this.pnlContactsContainer.Location = new System.Drawing.Point(0, 29);
            this.pnlContactsContainer.Name = "pnlContactsContainer";
            this.pnlContactsContainer.Size = new System.Drawing.Size(250, 450);
            this.pnlContactsContainer.TabIndex = 4;
            // 
            // lblContactsList
            // 
            this.lblContactsList.AutoSize = true;
            this.lblContactsList.ForeColor = System.Drawing.Color.White;
            this.lblContactsList.Location = new System.Drawing.Point(3, 11);
            this.lblContactsList.Name = "lblContactsList";
            this.lblContactsList.Size = new System.Drawing.Size(65, 13);
            this.lblContactsList.TabIndex = 2;
            this.lblContactsList.Text = "CONTACTS";
            // 
            // lblSeparator
            // 
            this.lblSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.lblSeparator.Location = new System.Drawing.Point(1, 35);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(249, 2);
            this.lblSeparator.TabIndex = 3;
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.CustomButton.Image = null;
            this.txtSearch.CustomButton.Location = new System.Drawing.Point(219, 2);
            this.txtSearch.CustomButton.Name = "";
            this.txtSearch.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.CustomButton.TabIndex = 1;
            this.txtSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.CustomButton.UseSelectable = true;
            this.txtSearch.CustomButton.Visible = false;
            this.txtSearch.DisplayIcon = true;
            this.txtSearch.Icon = global::Voiperinho.Properties.Resources.SearchIcon;
            this.txtSearch.Lines = new string[0];
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShowClearButton = true;
            this.txtSearch.Size = new System.Drawing.Size(243, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.UseSelectable = true;
            this.txtSearch.WaterMark = "Search";
            this.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatusBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlStatusBar.BackgroundImage")));
            this.pnlStatusBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlStatusBar.Controls.Add(this.lblResponseDescription);
            this.pnlStatusBar.Location = new System.Drawing.Point(250, 472);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(685, 24);
            this.pnlStatusBar.TabIndex = 9;
            // 
            // lblResponseDescription
            // 
            this.lblResponseDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResponseDescription.AutoSize = true;
            this.lblResponseDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblResponseDescription.Location = new System.Drawing.Point(509, 5);
            this.lblResponseDescription.Name = "lblResponseDescription";
            this.lblResponseDescription.Size = new System.Drawing.Size(173, 13);
            this.lblResponseDescription.TabIndex = 3;
            this.lblResponseDescription.Text = "Server response - no connection";
            this.lblResponseDescription.TextChanged += new System.EventHandler(this.lblResponseDescription_TextChanged);
            // 
            // pboxLogo
            // 
            this.pboxLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pboxLogo.BackgroundImage")));
            this.pboxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pboxLogo.ErrorImage = null;
            this.pboxLogo.InitialImage = null;
            this.pboxLogo.Location = new System.Drawing.Point(289, 134);
            this.pboxLogo.Name = "pboxLogo";
            this.pboxLogo.Size = new System.Drawing.Size(599, 183);
            this.pboxLogo.TabIndex = 10;
            this.pboxLogo.TabStop = false;
            // 
            // frmMessengerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 496);
            this.Controls.Add(this.pboxLogo);
            this.Controls.Add(this.pnlStatusBar);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMessengerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Voiperinho";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMessengerDialog_FormClosing);
            this.Load += new System.EventHandler(this.frmMessengerDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMessengerDialog_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMessengerDialog_MouseClick);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlDashboard.ResumeLayout(false);
            this.pnlContactsContainer.ResumeLayout(false);
            this.pnlContactsContainer.PerformLayout();
            this.pnlStatusBar.ResumeLayout(false);
            this.pnlStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem voiperinhoItem;
        private System.Windows.Forms.ToolStripMenuItem connectItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectItem;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.ToolTip tooltipInfo;
        private System.Windows.Forms.ToolStripMenuItem contactsItem;
        private System.Windows.Forms.ToolStripMenuItem addContactItem;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private System.Windows.Forms.Label lblContactsList;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Panel pnlContactsContainer;
        private System.Windows.Forms.Label lblResponseDescription;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.PictureBox pboxLogo;
    }
}

