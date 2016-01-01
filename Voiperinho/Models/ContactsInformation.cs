using Newtonsoft.Json;
using System.Collections.Generic;

namespace Voiperinho
{
    public class ContactsInformation
    {
        [JsonProperty(PropertyName = "message")]
        public List<AccountInformation> ContactsList { get; set; }
    }
}
