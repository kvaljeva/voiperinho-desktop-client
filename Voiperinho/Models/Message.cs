using Newtonsoft.Json;

namespace Voiperinho
{
    public class Message
    {
        private string sender;
        private string receiver;
        private string command;
        private string content;

        [JsonProperty(PropertyName = "sender")]
        public string Sender
        {
            get { return this.sender; }
            set { this.sender = value; }
        }
        [JsonProperty(PropertyName = "receiver")]
        public string Receiver
        {
            get { return this.receiver; }
            set { this.receiver = value; }
        }
        [JsonProperty(PropertyName = "command")]
        public string Command
        {
            get { return this.command; }
            set { this.command = value; }
        }
        [JsonProperty(PropertyName = "content")]
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public Message(string receiver, string command, string message, string sender)
        {
            this.receiver = receiver;
            this.command = command;
            this.content = message;
            this.sender = sender;
        }

        public static string FormatMessageString(string receiver, string message, string sender, string command = "")
        {
            return JsonConvert.SerializeObject(new Message(receiver, command, message, sender)) + "\n";
        }

        public static Message CreateMessageObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<Message>(jsonString);
        }
    }
}
