using BarrierGateApi.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;

namespace BarrierGateApi.Models
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

        public override bool AddInJsonFile(object[] parents = null)
        {
            try
            {
                if (FileSingleton.Instance.JsonFile.Length > 0)
                {
                    List<BarrierGate> barrierGateFromJson = JsonConvert.DeserializeObject<List<BarrierGate>>(FileSingleton.Instance.JsonFile);
                    barrierGateFromJson.Add(this);
                    string json = JsonConvert.SerializeObject(barrierGateFromJson);
                    FileSingleton.Instance.JsonFile = json;
                }
                else
                {
                    List<BarrierGate> list = new List<BarrierGate>();
                    list.Add(this);
                    string json = JsonConvert.SerializeObject(list);
                    FileSingleton.Instance.JsonFile = json;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(FileSingleton.Instance.JsonFile);
                return false;
            }
            return true;
        }

        public override bool RemoveInJsonFile(object[] parents = null)
        {
            try
            {
                FileSingleton.Instance.JsonFilePath = this.jsonFilePath;
                List<BarrierGate> barrierGateFromJson = JsonConvert.DeserializeObject<List<BarrierGate>>(FileSingleton.Instance.JsonFile);
                BarrierGate b = barrierGateFromJson.Where(x => x == this).FirstOrDefault();
                barrierGateFromJson.Remove(b);
                string json = JsonConvert.SerializeObject(barrierGateFromJson);
                FileSingleton.Instance.JsonFile = json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public override bool ModifyInJsonFile(BarrierGate elementToModified, object[] parents = null)
        {
            try
            {
                FileSingleton.Instance.JsonFilePath = this.jsonFilePath;
                List<BarrierGate> barrierGateFromJson = JsonConvert.DeserializeObject<List<BarrierGate>>(FileSingleton.Instance.JsonFile);
                BarrierGate barrierGateToEdit = barrierGateFromJson.Where(x => x == this).FirstOrDefault();

                barrierGateToEdit.Ip = elementToModified.Ip;
                barrierGateToEdit.Name = elementToModified.Name;
                barrierGateToEdit.IsOpen = elementToModified.IsOpen;

                string json = JsonConvert.SerializeObject(barrierGateFromJson);
                FileSingleton.Instance.JsonFile = json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public List<BarrierGate>? GetAllFromJsonFile()
        {
            FileSingleton.Instance.JsonFilePath = this.jsonFilePath;
            return JsonConvert.DeserializeObject<List<BarrierGate>>(FileSingleton.Instance.JsonFile);
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
