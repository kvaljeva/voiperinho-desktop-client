using Newtonsoft.Json;
using System.Collections.Generic;

namespace Voiperinho.Models
{
    public class BaseResponse<T>
    {
        public int Status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "error_message")]
        public string ErrorMessage { get; set; }
    }
}
