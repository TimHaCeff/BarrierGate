using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using BarrierGateGUI.Singletons;
using System.Collections.Generic;

namespace BarrierGateGUI.Model
{
    public class BarrierGate : GestionnableElement<BarrierGate>
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty("calendar_events")]
        public List<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();


        [JsonIgnore]
        protected override string gestionnableElementName { get; set; } = nameof(BarrierGate);

        public BarrierGate() { }

        public BarrierGate(int? id, string ip, string name, bool isOpen = false) 
        {
            this.Id = id;
            this.Ip = ip;
            this.Name = name;
            this.IsOpen = isOpen;
        }

        public enum BARRIERE_ENUM_STATE
        {
            OPEN,
            CLOSE,
        }

        public async void OpenBarriere()
        {
            try
            {
                string endpoint = $"/BarrierGate/TurnOn?baseAdress={this.Ip}&timeAlive=0";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void CloseBarrierGate()
        {
            try
            {
                string endpoint = $"/BarrierGate/TurnOff?baseAdress={this.Ip}";
                HttpResponseMessage response = await Singletons.BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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

        public static bool operator == (BarrierGate BG1, BarrierGate BG2) 
        {
            if(BG1.Id == BG2.Id && BG1.Ip == BG2.Ip && BG1.Name == BG2.Name) 
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
    }
}
