using Newtonsoft.Json;
using System.Collections.Generic;

namespace Voiperinho.Models
{
    class BaseResponseList<T>
    {
        public int Status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public List<T> Data { get; set; }
        [JsonProperty(PropertyName = "error_message")]
        public string ErrorMessage { get; set; }
    }
}
