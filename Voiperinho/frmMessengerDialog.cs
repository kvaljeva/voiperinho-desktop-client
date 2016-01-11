using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Voiperinho.Properties;
using Voiperinho.Models;
using Voiperinho.Network;
using Voiperinho.Helpers;
using Voiperinho.UserInterface;
using System.Media;

namespace Voiperinho
{
    public partial class FrmMessengerDialog : Form
    {
        #region Private Fields

        private FrmAuthorizationDialog frmAuthDialog;
        private SocketConnectionHelper socketHelper;
        private ContactsInformation contacts;
        private List<RequestInformation> requests;
        private AccountInformation accountInfo;
        private List<UserInfoContainer> listContactContainer;
        private List<MessengerContainer> listMessengerContainer;
        private Panel pnlAvailableUsers;
        private Panel pnlAvailableRequests;
        private SoundPlayer notificationSoundPlayer;
        private SoundPlayer callSoundPlayer;
        private bool isApplicationClosing;
        private bool isSearchViewLoaded;

        #endregion

        #region Public Properties

        public SocketConnectionHelper SocketHelper
        {
            get { return this.socketHelper; }
        }

        public List<UserInfoContainer> ListContactContainer
        {
            get { return this.listContactContainer; }
        }

        public SoundPlayer CallSoundPlayer
        {
            get { return this.callSoundPlayer; }
        }

        #endregion

        #region Constructors

        public FrmMessengerDialog()
        {
            InitializeComponent();

            socketHelper = new SocketConnectionHelper();
            socketHelper.SocketDataReceived += socketHelper_SocketDataReceived;
            socketHelper.ServerSocketClosed += socketHelper_ServerSocketClosed;
            socketHelper.CallEstablished += socketHelper_CallEstablished;

            this.notificationSoundPlayer = new SoundPlayer(Resources.NotificationSound);
            this.callSoundPlayer = new SoundPlayer(Resources.CallSound);
        }

        #endregion

        #region Event Handlers

        private void frmMessengerDialog_Load(object sender, EventArgs e)
        {
            this.listContactContainer = new List<UserInfoContainer>();
            this.listMessengerContainer = new List<MessengerContainer>();

            this.isApplicationClosing = false;
            this.isSearchViewLoaded = false;

            this.lblContactsList.Font = MetroFramework.MetroFonts.Subtitle;
        }

        private void socketHelper_ServerSocketClosed()
        {
            this.UpdateClientOnDisconnect();
        }

        private void socketHelper_CallEstablished()
        {
            MessengerContainer msgWindow = GetActiveMessengerContainer();

            if (msgWindow != null) msgWindow.UpdateCallState();
        }

        private void socketHelper_SocketDataReceived(string data)
        {
            if (socketHelper.IsHandshake)
            {
                BaseResponse<AccountInformation> response = JsonConvert.DeserializeObject<BaseResponse<AccountInformation>>(data);

                if (response != null && response.Status == 200)
                {
                    this.accountInfo = response.Data;

                    this.contacts = WebConnector.GetContactsData(accountInfo.Id);
                    this.requests = WebConnector.GetAvailableRequests(accountInfo.Id);

                    this.DisplayContactsAndRequests();

                    this.socketHelper.IsHandshake = false;
                    this.socketHelper.IsAuthorized = true;

                    this.UpdateClientMenu(true);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + response.ErrorMessage, "Error logging in", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.socketHelper.CloseConnection();
                    this.UpdateClientMenu(false);
                }

                return;
            }

            Message message = Message.CreateMessageObject(data);
            bool isCallContent = false;

            if (message == null || (message.Command.Equals("/disconnect") && message.Content.Equals("OK")))
            {
                socketHelper.CloseConnection();

                UpdateClientOnDisconnect();
                return;
            }
            else if (message.Command.Equals(@"/online"))
            {
                string username = message.Content;
                ChangeOnlineStatus(username);

                return;
            }
            else if (message.Command.Equals(@"/request"))
            {
                RequestInformation request = null;

                try
                {
                    //Console.WriteLine(message.Content);
                    request = JsonConvert.DeserializeObject<RequestInformation>(message.Content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while parsing request command: " + ex.Message);
                }

                if (request != null)
                {
                    this.requests.Add(request);
                    this.FillRequestsList();
                }

                return;
            }
            else if (message.Command.Equals(@"/accept"))
            {
                AccountInformation contact = null;

                try
                {
                    contact = JsonConvert.DeserializeObject<AccountInformation>(message.Content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while parsing accept command: " + ex.Message);
                }

                if (contact != null)
                {
                    this.contacts.ContactsList.Add(contact);
                    this.AppendContact(contact);

                    this.ChangeOnlineStatus(contact.Username);

                    socketHelper.SendMessage(contact.Username, this.accountInfo.Username, this.accountInfo.Username, @"/online");
                }

                return;
            }
            else if (message.Command.Equals(@"/offline"))
            {
                string username = message.Content;
                ChangeOnlineStatus(username);

                return;
            }
            else if (message.Command.Contains("call"))
            {
                isCallContent = true; // We set it to true to avoid dumping call ID's into the message box

                if (message.Command.Equals("/call"))
                {
                    this.callSoundPlayer.PlayLooping();

                    MessengerContainer msgWindow = GetActiveMessengerContainer(message.Sender);
                    if (msgWindow != null)
                    {                        
                        msgWindow.CreateCallDialog(message.Sender);
                        msgWindow.callDialog.CallStateChanged += callDialog_CallStateChanged;
                    }
                }
                else if (message.Command.Contains("/accept"))
                {
                    MessengerContainer msgWindow = GetActiveMessengerContainer(message.Sender);

                    if (msgWindow != null) msgWindow.InitiatingCall = false;

                    this.socketHelper.EstablishCall(this.accountInfo.Username);
                }
                else if (message.Command.Contains("/close"))
                {
                    if (this.socketHelper.CallHelper != null)
                    {
                        this.socketHelper.CloseCall();
                    }

                    MessengerContainer msgWindow = GetActiveMessengerContainer(message.Sender);
                    if (msgWindow != null)
                    {
                        msgWindow.InitiatingCall = false;

                        msgWindow.UpdateCallState();
                        msgWindow.callDialog.CallStateChanged -= callDialog_CallStateChanged;
                    }
                }
            }

            if (message.Content != string.Empty && !isCallContent)
            {
                if (message.Sender == this.accountInfo.Username)
                    this.DumpMessage(message, true);
                else
                {
                    this.DumpMessage(message);

                    this.notificationSoundPlayer.Play();
                    SetContainerNotification(message.Sender);
                }
            }
        }

        private void callDialog_CallStateChanged(bool state, string caller)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => callDialog_CallStateChanged(state, caller)));
                return;
            }

            // Stop the calling sound if user either accepts or declines the call
            this.callSoundPlayer.Stop();

            MessengerContainer msgWindow = GetActiveMessengerContainer(caller);

            if (msgWindow != null)
            {
                if (state)
                {
                    this.socketHelper.EstablishCall(this.accountInfo.Username);
                    this.socketHelper.SendMessage(caller, "", this.accountInfo.Username, @"/call/accept");
                }
                else
                {
                    this.socketHelper.SendMessage(caller, "", this.accountInfo.Username, @"/call/close");
                }

                if (msgWindow.InitiatingCall)
                    msgWindow.InitiatingCall = false;

                msgWindow.callDialog.CallStateChanged -= callDialog_CallStateChanged;
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (socketHelper.IsAuthorized)
                return;

            if (!socketHelper.TryConnect())
            {
                MessageBox.Show("An error occurred while trying to establish connection with the server, please try again!", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.lblResponseDescription.Text = "Status - Connection error.";
                return;
            }

            this.lblResponseDescription.Text = "Status - Connected to server";

            if (this.frmAuthDialog == null)
            {
                this.frmAuthDialog = new FrmAuthorizationDialog();
                this.frmAuthDialog.ShowDialog(this);

                if (this.frmAuthDialog.DialogResult == DialogResult.OK)
                {
                    if (socketHelper.TryAuthorize(frmAuthDialog.Username, frmAuthDialog.Password))
                    {
                        this.lblResponseDescription.Text = "Status - Authenticating...";
                    }
                    else
                    {
                        this.lblResponseDescription.Text = "Status - Authentication error. Either the data was not correct or user doesn't exist.";
                    }

                    this.frmAuthDialog = null;
                }
                else
                {
                    socketHelper.Disconnect();
                    socketHelper.CloseConnection();

                    this.lblResponseDescription.Text = "Status - No connection.";
                    this.frmAuthDialog = null;
                }
            }
            else
            {
                this.frmAuthDialog.Focus();
            }
        }

        private void messagerContainer_InputBoxFocused()
        {
            RemoveContainerNotification();
        }

        private void UserInfoContainer_RequestRejected(object sender)
        {
            int requestId = 0;
            UserInfoContainer requestContainer = sender as UserInfoContainer;
            Point containerLocation = requestContainer.Location;

            this.pnlAvailableRequests.Controls.Remove(requestContainer);
            this.pnlAvailableRequests.Invalidate();

            foreach (Control control in this.pnlAvailableRequests.Controls)
            {
                if (control is UserInfoContainer)
                {
                    if (control.Location.Y > containerLocation.Y)
                        control.Location = new Point(control.Location.X, (control.Location.Y - control.Height));
                }
            }

            // Get the ID of the request itself
            foreach (RequestInformation request in this.requests)
            {
                if (request.Requester.Id == requestContainer.UserId)
                {
                    requestId = request.Id;
                    break;
                }
            }

            // Delete request from the database
            WebConnector.DeleteRequest(requestId);

            // Remove the current request both from the panel and the list containing the requests
            this.requests.RemoveAll(request => request.Requester.Id == requestContainer.UserId);

            // If there's no requests - no need to keep the header up anymore.
            if (requests.Count == 0)
            {
                this.pnlContactsContainer.Controls.Remove(this.pnlAvailableRequests);
            }
        }

        private void UserInfoContainer_RequestAccepted(object sender)
        {
            UserInfoContainer requestContainer = sender as UserInfoContainer;

            AddRequestToContacts(requestContainer);
            requestContainer.UpdateContacts();

            // After we're done with the request, remove it from the list
            this.requests.RemoveAll(request => request.Requester.Id == requestContainer.UserId);

            if (requests.Count == 0)
            {
                this.pnlContactsContainer.Controls.Remove(this.pnlAvailableRequests);
            }
        }

        private void container_AddUserPressed(string username, object sender)
        {
            UserInfoContainer activeContainer = sender as UserInfoContainer;

            RequestContainer requestContainer = new RequestContainer(username, this.accountInfo.Id, activeContainer.UserId, this.socketHelper, this.accountInfo);
            requestContainer.Name = "controlRequestContainer_" + username;
            requestContainer.Location = new Point(pnlDashboard.Right, ((activeContainer.Top + activeContainer.Height / 2) - 10));

            // Add the control and set it atop on all of the existing controls
            this.Controls.Add(requestContainer);
            requestContainer.BringToFront();
        }

        private void contactBox_MouseClick(object sender, MouseEventArgs e)
        {
            UserInfoContainer contactContainer = sender as UserInfoContainer;

            if ((contactContainer.Parent == this.pnlContactsContainer) || (contactContainer.Parent == this.pnlAvailableRequests))
            {
                this.LoadMessengerLayout(contactContainer.Username, contactContainer.RequestNote); // fix
            }

            if (e.Button == MouseButtons.Right && !contactContainer.IsRequest)
            {
                this.menuContactOptions.Show(Cursor.Position);
            }

            if (this.isSearchViewLoaded)
            {
                foreach (Control control in this.pnlAvailableUsers.Controls)
                {
                    if (control is UserInfoContainer)
                    {
                        UserInfoContainer container = control as UserInfoContainer;

                        if (!(container.Name.Equals(contactContainer.Name)))
                        {
                            container.IsSelected = false;
                            container.HighlightContainer();
                        }
                    }
                }
            }
            else
            {
                foreach (UserInfoContainer container in this.listContactContainer)
                {
                    if (!(container.Name.Equals(contactContainer.Name)))
                    {
                        container.IsSelected = false;
                        container.HighlightContainer();
                    }
                }
            }

            contactContainer.IsNotificationReceived = false;
            contactContainer.IsSelected = true;
            contactContainer.HighlightContainer();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                this.pnlContactsContainer.Show();
                this.ClearAvailableUsersList();

                this.isSearchViewLoaded = false;
            }
        }

        private void frmMessengerDialog_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is RequestContainer)
                {
                    RequestContainer requestContainer = control as RequestContainer;

                    if (this.Controls.Contains(requestContainer))
                    {
                        this.Controls.Remove(requestContainer);

                        requestContainer.Dispose();
                        requestContainer = null;

                        this.Invalidate();
                    }
                }
            }
        }

        private void addContactItem_Click(object sender, EventArgs e)
        {
            this.txtSearch.Focus();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socketHelper.Disconnect();
        }

        private void frmMessengerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isApplicationClosing = true;

            this.socketHelper.Disconnect();
            if (this.notificationSoundPlayer != null) this.notificationSoundPlayer.Dispose();
            if (this.callSoundPlayer != null) this.callSoundPlayer.Dispose();

            this.lblResponseDescription.Text = "Status - Disconnected.";
        }

        private void frmMessengerDialog_KeyDown(object sender, KeyEventArgs e)
        {
            MessengerContainer msgWindow = GetActiveMessengerContainer();

            if (msgWindow != null)
            {
                if (e.KeyCode == Keys.Escape && msgWindow.commandsContainer != null && msgWindow.commandsContainer.Visible)
                {
                    msgWindow.commandsContainer.Hide();
                    msgWindow.inputContainer.Text = string.Empty;
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.txtSearch.Text != string.Empty)
            {
                if (!this.socketHelper.IsAuthorized) return;

                if (this.pnlAvailableUsers == null)
                {
                    this.pnlAvailableUsers = new Panel();

                    this.CreateGroupHeader("AVAILABLE USERS", this.pnlAvailableUsers, lblContactsList.Location, lblSeparator.Location);

                    this.pnlAvailableUsers.Name = "pnlAvailableUsers";
                    this.pnlAvailableUsers.Location = pnlContactsContainer.Location;
                    this.pnlAvailableUsers.Size = new Size(pnlContactsContainer.Size.Width, pnlContactsContainer.Size.Height - txtSearch.Height);
                }
                else this.ClearAvailableUsersList();

                List<AccountInformation> users = WebConnector.GetAvailableUsers(this.txtSearch.Text, this.accountInfo.Id);

                foreach (AccountInformation contact in this.contacts.ContactsList)
                {
                    users.RemoveAll(user => user.Id == contact.Id);
                }

                this.pnlDashboard.Controls.Add(pnlAvailableUsers);
                this.pnlContactsContainer.Hide();

                // Add user controls to the available users container
                this.ListAvailableUsers(users);
                this.isSearchViewLoaded = true;
            }
        }

        private void messagerContainer_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        #endregion

        #region UI Helper Methods

        private void CreateMessengerLayout(string messengerIdentifier, string requestText = "")
        {
            // Hide the picturebox with the logo
            this.pboxLogo.Hide();

            MessengerContainer messagerContainer = new MessengerContainer(this, this.accountInfo);          
            messagerContainer.Location = new Point(250, 24);
            messagerContainer.Name = "messenger_" + messengerIdentifier;

            messagerContainer.MouseClick += messagerContainer_MouseClick;
            messagerContainer.InputBoxFocused += messagerContainer_InputBoxFocused;

            Controls.Add(messagerContainer);

            messagerContainer.inputContainer.Focus();

            this.listMessengerContainer.Add(messagerContainer);
            this.DumpMessage(Environment.NewLine + "\tClient started.");

            if (requestText != string.Empty) this.DumpMessage(requestText);
        }

        private void ClearMessengerLayouts()
        {
            for (int i = this.Controls.Count - 1; i > 0; i--)
            {
                if (this.Controls[i] is MessengerContainer)
                {
                    this.Controls.Remove(this.Controls[i]);
                }
            }

            this.Invalidate();
        }

        private MessengerContainer GetActiveMessengerContainer()
        {
            foreach (Control control in this.Controls)
            {
                if (control is MessengerContainer)
                {
                    foreach (MessengerContainer container in this.listMessengerContainer)
                    {
                        if (container.Name == control.Name && container.Visible) return container;
                    }
                }
            }

            return null;
        }

        private MessengerContainer GetActiveMessengerContainer(string contact)
        {
            foreach (Control control in this.Controls)
            {
                if (control is MessengerContainer)
                {
                    foreach (MessengerContainer container in this.listMessengerContainer)
                    {
                        if (container.Name.Contains(contact)) return container;
                    }
                }
            }

            return null;
        }

        private void LoadMessengerLayout(string identifier, string requestText = "")
        {
            bool windowExists = false;
            MessengerContainer msgWindow = null;

            foreach (MessengerContainer window in this.listMessengerContainer)
            {
                if (window.Name.Contains(identifier.ToString()))
                {
                    msgWindow = window;
                    windowExists = true;
                    break;
                }

                window.Hide();
            }

            if (windowExists)
            {
                msgWindow.Show();
            }
            else
            {
                this.CreateMessengerLayout(identifier, requestText);
            }
        }

        private void CreateGroupHeader(string headerText, Panel activePanel, Point labelLocation, Point separatorLocation)
        {
            Label label = new Label();
            Label separator = new Label();

            label.Name = "labelRequests";
            label.AutoSize = true;
            label.Text = headerText;
            label.Font = lblContactsList.Font;
            label.ForeColor = lblContactsList.ForeColor;
            label.Location = labelLocation;
            separator.Name = "separatorRequests";
            separator.AutoSize = false;
            separator.Size = lblSeparator.Size;
            separator.Location = separatorLocation;
            separator.BackColor = lblSeparator.BackColor;
            separator.BorderStyle = BorderStyle.None;

            activePanel.Controls.Add(label);
            activePanel.Controls.Add(separator);
        }

        private void AppendContact(AccountInformation user)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AppendContact(user)));
                return;
            }

            int offset = GetContactsListLength();

            UserInfoContainer contactBox = new UserInfoContainer(false, user.Username, user.Id, null);
            contactBox.Parent = this.pnlContactsContainer;
            contactBox.Name = "contact_" + user.Username;
            contactBox.Location = new Point(0, offset);
            contactBox.MouseClick += contactBox_MouseClick;

            this.listContactContainer.Add(contactBox);
            this.pnlContactsContainer.Controls.Add(contactBox);

            this.pnlContactsContainer.Invalidate();
        }

        private void FillRequestsList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => FillRequestsList()));
                return;
            }

            if (this.requests != null && this.requests.Count > 0)
            {
                // Add small offset between contacts and new requests
                int contactControlOffset = this.GetContactsListLength();
                contactControlOffset += 10;

                // Panel to store all of the requests
                this.pnlAvailableRequests = new Panel();

                this.pnlAvailableRequests.Location = new Point(0, contactControlOffset);
                this.pnlAvailableRequests.Size = new Size(this.pnlContactsContainer.Width, this.pnlContactsContainer.Height - contactControlOffset);
                this.pnlContactsContainer.Controls.Add(this.pnlAvailableRequests);

                this.CreateGroupHeader("RECEIVED REQUESTS", this.pnlAvailableRequests, new Point(3, 0), new Point(0, this.lblContactsList.Height + 5));
                contactControlOffset = this.lblContactsList.Height + 15;

                foreach (RequestInformation request in this.requests)
                {
                    UserInfoContainer requestBox = new UserInfoContainer(request.Requester.Username, request.RequesterId, request.RequestText);
                    requestBox.Name = "request_" + request.Requester.Username;
                    requestBox.Parent = this.pnlAvailableRequests;
                    requestBox.Location = new Point(0, contactControlOffset);

                    this.pnlAvailableRequests.Controls.Add(requestBox);

                    contactControlOffset += requestBox.Size.Height;

                    requestBox.MouseClick += contactBox_MouseClick;
                    requestBox.RequestAccepted += UserInfoContainer_RequestAccepted;
                    requestBox.RequestRejected += UserInfoContainer_RequestRejected;
                }
            }
        }

        private void FillContactsList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => FillContactsList()));
                return;
            }

            this.Text = "Voiperinho - " + accountInfo.Username;

            int contactControlOffset = 45;
            foreach (AccountInformation user in this.contacts.ContactsList)
            {
                UserInfoContainer contactBox = new UserInfoContainer(false, user.Username, user.Id, null);
                contactBox.Name = "contact_" + user.Username;
                contactBox.Parent = this.pnlContactsContainer;
                contactBox.Location = new Point(0, contactControlOffset);
                contactBox.MouseClick += contactBox_MouseClick;

                this.listContactContainer.Add(contactBox);
                this.pnlContactsContainer.Controls.Add(contactBox);

                contactControlOffset += contactBox.Size.Height;
            }

            this.pnlContactsContainer.Invalidate();
        }

        private void ListAvailableUsers(List<AccountInformation> users)
        {
            int contactControlOffset = 45;
            foreach (AccountInformation user in users)
            {
                UserInfoContainer contactBox = new UserInfoContainer(false, user.Username, user.Id, null, false);
                contactBox.Name = "contact_" + user.Username;
                contactBox.Parent = this.pnlAvailableRequests;
                contactBox.Location = new Point(0, contactControlOffset);

                this.pnlAvailableUsers.Controls.Add(contactBox);

                contactControlOffset += contactBox.Size.Height;

                // Add click handler for each consecutive container so that they fire independently of one another (per object)
                contactBox.MouseClick += contactBox_MouseClick;
                contactBox.AddUserPressed += container_AddUserPressed;
            }

            this.pnlAvailableUsers.Invalidate();
        }

        private void RepositionUserContainers(Panel activePanel, UserInfoContainer container)
        {
            // If there are any requests that are positioned lower on the panel than the current request, reposition them
            for (int i = activePanel.Controls.Count - 1; i > 0; i--)
            {
                if (activePanel.Controls[i] is UserInfoContainer)
                {
                    UserInfoContainer control = activePanel.Controls[i] as UserInfoContainer;

                    if (control.Location.Y > container.Location.Y)
                        control.Location = new Point(control.Location.X, (control.Location.Y - control.Height));
                }
            }
        }

        private void RespositionContacts(UserInfoContainer container)
        {
            this.RepositionUserContainers(this.pnlContactsContainer, container);
        }

        private void RepositionRemainingRequests(UserInfoContainer container)
        {
            this.RepositionUserContainers(this.pnlAvailableRequests, container);
        }

        private void AddRequestToContacts(UserInfoContainer container)
        {
            int contactListBottom = GetContactsListLength();
            int requestId = 0;

            // Get the ID of the request itself
            foreach (RequestInformation request in this.requests)
            {
                if (request.Requester.Id == container.UserId)
                {
                    requestId = request.Id;
                    break;
                }
            }

            RepositionRemainingRequests(container);

            container.Location = new Point(0, contactListBottom);
            contactListBottom += container.Height;

            container.Parent = this.pnlContactsContainer;
            this.pnlContactsContainer.Controls.Add(container);
            this.listContactContainer.Add(container);

            int offset = GetContactsListLength();
            this.pnlAvailableRequests.Location = new Point(this.pnlAvailableRequests.Location.X, offset);

            // Send query to the API so that we actually add the user to the database
            WebConnector.AddContact(this.accountInfo.Id, container.UserId);
            // Remove the request
            WebConnector.DeleteRequest(requestId);

            // Tell server to propagate the request to the accepted client if he's currently available
            string jsonClientInfo = JsonConvert.SerializeObject(this.accountInfo);
            // Send as message
            socketHelper.SendMessage(container.Username, jsonClientInfo, this.accountInfo.Username, @"/accept");
        }

        private void ChangeOnlineStatus(string username)
        {
            foreach (Control control in this.pnlContactsContainer.Controls)
            {
                if (control is UserInfoContainer)
                {
                    UserInfoContainer contact = control as UserInfoContainer;

                    if (contact.Username == username)
                    {
                        if (contact.OnlineStatus)
                            contact.OnlineStatus = false;
                        else
                            contact.OnlineStatus = true;

                        break;
                    }
                }
            }
        }

        private void SetContainerNotification(string username)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetContainerNotification(username)));
                return;
            }

            foreach (UserInfoContainer contactContainer in this.listContactContainer)
            {
                if (username != null && username != string.Empty)
                {
                    if (contactContainer.Username == username)
                    {
                        contactContainer.IsNotificationReceived = true;
                        contactContainer.Invalidate();
                        break;
                    }
                }
            }
        }

        private UserInfoContainer GetActiveUserContainer()
        {
            foreach (UserInfoContainer contactContainer in this.listContactContainer)
            {
                if (contactContainer.IsSelected)
                {
                    return contactContainer;
                }
            }

            return null;
        }

        private void RemoveContainerNotification()
        {
            foreach (UserInfoContainer contactContainer in this.listContactContainer)
            {
                if (contactContainer.IsSelected)
                {
                    contactContainer.IsNotificationReceived = false;
                    contactContainer.Invalidate();
                    break;
                }
            }
        }

        private void ClearContactsContainer()
        {
            for (int i = pnlContactsContainer.Controls.Count - 1; i > 0; i--)
                if (pnlContactsContainer.Controls[i] is UserInfoContainer)
                {
                    this.pnlContactsContainer.Controls[i].MouseClick -= contactBox_MouseClick;
                    this.pnlContactsContainer.Controls.RemoveAt(i);
                }

            if (this.pnlAvailableRequests != null) this.pnlContactsContainer.Controls.Remove(this.pnlAvailableRequests);

            this.listContactContainer.Clear();
            this.listMessengerContainer.Clear();
        }

        private void ClearAvailableUsersList()
        {
            if (this.pnlAvailableUsers == null || this.pnlAvailableUsers.Controls.Count <= 2) return;

            for (int i = this.pnlAvailableUsers.Controls.Count - 1; i > 0; i--)
                if (this.pnlAvailableUsers.Controls[i] is UserInfoContainer)
                {
                    this.pnlAvailableUsers.Controls[i].MouseClick -= contactBox_MouseClick;
                    this.pnlAvailableUsers.Controls.RemoveAt(i);
                }

            this.pnlAvailableUsers.Invalidate();
        }

        #endregion

        #region Utility Methods

        private void DumpMessage(Message message, bool isSender = false)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DumpMessage(message, isSender)));
                return;
            }

            MessengerContainer msgWindow;

            msgWindow = (message.Sender != null && message.Sender != this.accountInfo.Username) ? GetActiveMessengerContainer(message.Sender) : GetActiveMessengerContainer();

            if (msgWindow != null)
            {
                msgWindow.AppendText(message);
                msgWindow.inputContainer.Focus();
            }
        }

        private void DumpMessage(string message)
        {
            MessengerContainer msgWindow;

            msgWindow = GetActiveMessengerContainer();

            if (msgWindow != null)
            {
                msgWindow.AppendText("\t" + message + Environment.NewLine);

                msgWindow.inputContainer.Text = string.Empty;
                msgWindow.inputContainer.Focus();
            }
        }

        private void DisplayContactsAndRequests()
        {
            this.FillContactsList();
            this.FillRequestsList();

            this.pnlContactsContainer.Invalidate();

            if (this.pnlAvailableRequests != null)
                this.pnlAvailableRequests.Invalidate();
        }

        private int GetContactsListLength()
        {
            int offset = 0;

            foreach (Control control in this.pnlContactsContainer.Controls)
            {
                if (control is UserInfoContainer)
                {
                    if (offset == 0) offset = control.Top;
                    
                    offset += control.Height;
                }
            }

            // If offset is equal to zero even after the given calculation, it means that there are no contacts yet - so we set it the initial offset of 45
            if (offset == 0) offset = 45;

            return offset;
        }

        private void UpdateClientMenu(bool isConnect)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateClientMenu(isConnect)));
                return;
            }

            if (isConnect)
            {
                this.voiperinhoItem.DropDownItems["connectItem"].Enabled = false;
                this.voiperinhoItem.DropDownItems["disconnectItem"].Enabled = true;
                this.lblResponseDescription.Text = "Status - Succesfully authenticated.";
            }
            else
            {
                // Update the menustrip connection items
                this.voiperinhoItem.DropDownItems["connectItem"].Enabled = true;
                this.voiperinhoItem.DropDownItems["disconnectItem"].Enabled = false;
                this.lblResponseDescription.Text = "Status - Disconnected.";
            }
        }

        private void UpdateClientOnDisconnect()
        {
            if (this.isApplicationClosing) return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateClientOnDisconnect()));
                return;
            }

            // Remove contacts from the dashboard
            ClearContactsContainer();
            // Remove all of the messenger containers
            ClearMessengerLayouts();
            // Update menu accordingly
            UpdateClientMenu(false);

            // Print it to the user
            this.Text = "Voiperinho";
            this.pboxLogo.Show();
        }

        #endregion

        private void lblResponseDescription_TextChanged(object sender, EventArgs e)
        {
            // Determine the new text size to move the label accordingly
            Size length = TextRenderer.MeasureText(this.lblResponseDescription.Text, this.lblResponseDescription.Font);

            lblResponseDescription.Location = new Point(this.pnlStatusBar.Width - length.Width - 5, this.lblResponseDescription.Location.Y);
        }

        private void sendIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessengerContainer msgWindow = GetActiveMessengerContainer();
            msgWindow.inputContainer.Focus();
        }

        private void removeFromContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfoContainer activeContainer = GetActiveUserContainer();

            if (activeContainer != null)
            {
                int userId = activeContainer.UserId;

                // Add connector line
                WebConnector.RemoveContact(userId.ToString(), this.accountInfo.Id.ToString());

                this.RespositionContacts(activeContainer);
                this.pnlContactsContainer.Controls.Remove(activeContainer);
            }
        }
    }
}