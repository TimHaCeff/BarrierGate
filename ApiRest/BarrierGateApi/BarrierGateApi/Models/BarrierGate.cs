using BarrierGateApi.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrierGateApi.Models
{
    [Table("barrier_gate")]
    public class BarrierGate : GestionnableElement
    {
        [JsonProperty("ip")]
        [Column("ip")]
        public string Ip { get; set; }
        [JsonProperty("is_open")]
        [Column("is_open")]
        public bool IsOpen { get; set; }


        public enum BARRIERE_ENUM_STATE
        {
            OPEN,
            CLOSE,
        }


        public BarrierGate() { }

        public BarrierGate(string ip, string name, bool isOpen = false)
        {
            this.Ip = ip;
            this.Name = name;
            this.IsOpen = isOpen;
        }

        public bool IsValid()
        {
            if (this.Id is null)
            { return false; }

            if (this.Ip is null || this.Ip == String.Empty)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            return true;
        }

        public static bool operator ==(BarrierGate BG1, BarrierGate BG2)
        {
            if (BG1.Id == BG2.Id && BG1.Ip == BG2.Ip && BG1.Name == BG2.Name)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(BarrierGate BG1, BarrierGate BG2)
        {
            if (BG1.Id != BG2.Id && BG1.Ip != BG2.Ip || BG1.Name != BG2.Name)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"" +
                $"Id : {Id}\n" +
                $"Ip : {Ip}\n" +
                $"Name : {Name}\n";
        }
    }
}
