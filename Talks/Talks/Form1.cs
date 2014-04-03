using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;
using RestSharp;

namespace Talks
{
    public partial class Form1 : Form
    {
        public static Skype skype = new Skype();

        public Form1()
        {
            InitializeComponent();
            skype.Attach();
            try
            {
                // Try setting our custom event handler while avoiding any ambiguity.
                ((_ISkypeEvents_Event)skype).MessageStatus += OurMessageStatus;
            }
            catch (Exception e)
            {
                // Write Application Receiving Event Handler Failed to Window.
                Console.WriteLine(DateTime.Now.ToLocalTime() + ": " +
                 "Application Receiving Event Handler Failed" +
                 " - Exception Source: " + e.Source + " - Exception Message: " + e.Message +
                 "\r\n");

                // If the "Use Auto Debug" check box is checked and we are in debug, drop into debug here when retry, otherwise, prompt for action.
             
            }
        }

        public void OurMessageStatus(ChatMessage chatmessage, TChatMessageStatus status)
        {
            // Always use try/catch with ANY Skype calls.
            try
            {
                
                // Write Message Status to Window.
                Console.WriteLine(DateTime.Now.ToLocalTime() + ": " +
                 "Message Status - Message Id: " + chatmessage.Id +
                 " - Chat Friendly Name: " + chatmessage.Chat.FriendlyName +
                 " - Chat Name: " + chatmessage.Chat.Name +
                 " - Converted Message Type: " + skype.Convert.ChatMessageTypeToText(chatmessage.Type) +
                 " - Message Type: " + chatmessage.Type +
                 " - Converted TChatMessageStatus Status: " + skype.Convert.ChatMessageStatusToText(status) +
                 " - TChatMessageStatus Status: " + status +
                 " - From Display Name: " + chatmessage.FromDisplayName +
                 " - From Handle: " + chatmessage.FromHandle);

                // Examples of checking lengths before adding to Window.
                if (chatmessage.Chat.Topic.Length > 0) Console.WriteLine(" - Topic: " + chatmessage.Chat.Topic);
                if (chatmessage.Body.Length > 0) Console.WriteLine(" - Body: " + chatmessage.Body);

                var client = new RestClient("http://localhost:8080/");
                var request = new RestRequest("/api/messages", Method.POST);
                request.AddParameter("user", chatmessage.FromDisplayName);
                if (chatmessage.Body.Length > 0)
                    request.AddParameter("body", chatmessage.Body);

                IRestResponse response = client.Execute(request);

            }
            catch (Exception e)
            {
                // Possibly old Skype4COM version, log an error, drop into debug if wanted.
                Console.WriteLine(DateTime.Now.ToLocalTime() + ": " +
                 "Message Status Event Fired - Bad Text" +
                 " - Exception Source: " + e.Source + " - Exception Message: " + e.Message +
                 "\r\n");

                // If the "Use Auto Debug" check box is checked and we are in debug, drop into debug here when retry, otherwise, prompt for action.

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
