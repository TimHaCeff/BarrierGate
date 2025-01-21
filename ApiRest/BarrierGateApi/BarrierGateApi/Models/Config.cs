using Newtonsoft.Json;

namespace BarrierGateApi.Models
{
    public class Config
    {
        [JsonProperty("sqlite_path")]
        public string SQLitePath { get; set; }
    }
}
