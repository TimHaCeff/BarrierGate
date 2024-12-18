using Newtonsoft.Json;

namespace BarrierGateApi.Models
{
    public class Config
    {
        [JsonProperty("json_path")]
        public string JsonPath { get; set; }
    }
}
