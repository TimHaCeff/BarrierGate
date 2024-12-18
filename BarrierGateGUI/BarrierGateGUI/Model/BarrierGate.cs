using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using BarrierGateGUI.Singletons;
using System.Collections.Generic;

namespace BarrierGateGUI.Model
{
    public class BarrierGate : JsonElement<BarrierGate>
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty("calendar_events")]
        public List<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();

        public BarrierGate() 
        {
            this.Id = DateTime.Now.ToString();
        }

        public BarrierGate(string ip, string name, bool isOpen = false) 
        {
            this.Id = DateTime.Now.ToString();
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
            if (this.Id is null || this.Id == String.Empty)
            { return false; }

            if (this.Ip is null || this.Ip == String.Empty)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            return true;
        }

        public async override Task<bool> AddInJsonFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/BarrierGate/AddInJsonFile?barrierGateToAdd={json}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async override Task<bool> RemoveInJsonFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/BarrierGate/RemoveInJsonFile?jsonOfBarrierGateToRemove={json}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async override Task<bool> ModifyInJsonFile(BarrierGate elementModified)
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                string jsonOfModified = JsonConvert.SerializeObject(elementModified);

                string endpoint = $"/BarrierGate/ModifyInJsonFile?jsonOfBarrierGateToEdit={json}&jsonOfBarrierGateEdited={jsonOfModified}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async Task<List<BarrierGate>?> GetAllFromJsonFile()
        {
            try
            {
                string endpoint = $"/BarrierGate/GetAllFromJsonFile";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                List<BarrierGate> a = JsonConvert.DeserializeObject<List<BarrierGate>?>(content);
                if (a is null) 
                {
                    a = new List<BarrierGate>();
                }
                return a;
            }
            catch (Exception e) 
            {
                List < BarrierGate > a = new List < BarrierGate >();
                return a;
            }
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
