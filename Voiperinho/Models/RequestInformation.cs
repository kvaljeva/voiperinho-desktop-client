using Newtonsoft.Json;

namespace Voiperinho.Models
{
    public class RequestInformation
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "requester_id")]
        public int RequesterId { get; set; }
        public int State { get; set; }
        [JsonProperty(PropertyName = "request_text")]
        public string RequestText { get; set; }
        [JsonProperty(PropertyName  = "requester")]
        public AccountInformation Requester { get; set; }
    }
}
