using BarrierGateApi.Singleton;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrierGateApi.Models
{
    public class GestionnableElement
    {
        [JsonProperty("id")]
        [Column("id")]
        public int? Id { get; set; } = null;
        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }
    }
}
