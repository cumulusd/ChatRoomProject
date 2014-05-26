using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatRoomSharpClient.ChatRoomService;
using System.ServiceModel;

namespace ChatRoomSharpClient
{
    public partial class Form1 : Form, IChatRoomCallback
    {
        private string _ClientName = String.Empty;
        private ChatRoomClient _ChatRoomClient;


        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            InstanceContext instContext = new InstanceContext(this);

            _ChatRoomClient = new ChatRoomClient(instContext);

            while (_ClientName == string.Empty)
            {
                _ClientName = Microsoft.VisualBasic.Interaction.InputBox("What is your name?", "Name", "");
            }

            _ChatRoomClient.Open();
            _ChatRoomClient.JoinRoom();
        }
        
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            _ChatRoomClient.Close();
            _ChatRoomClient = null;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // send string to service
            this._ChatRoomClient.SendMessage(this._ClientName, this.txtMessage.Text);

            txtMessage.Text = string.Empty;
        }

        public void didReceiveMessage(string fromClientName, string message)
        {
            this.txtChatRoom.Text += "(" + System.DateTime.Now.ToString() + ") - " + fromClientName + ": " + message + Environment.NewLine + Environment.NewLine;
        }
    }
}
