using Newtonsoft.Json;
namespace Voiperinho
{
    public class AccountInformation
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonProperty(PropertyName = "email_address")]
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
