using System.Collections.Generic;
using Newtonsoft.Json;

namespace RedisCacheConsole.Shared.Criterials.Domain
{
    public class FilterRequest
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("operator")]
        public string Operator { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }


    }
}