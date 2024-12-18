using BarrierGateGUI.Singletons;
using Newtonsoft.Json;
using System.Globalization;

namespace BarrierGateGUI.Model
{
    public class CalendarEvent : JsonElement<CalendarEvent>
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("start_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [JsonProperty("end_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime EndDate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }


        public CalendarEvent() 
        {
            DateTime now = DateTime.Now;
            this.Id = DateTime.Now.ToString();
            this.StartDate = now;
            this.EndDate = now;
        }

        public CalendarEvent(string name, DateTime startDate, DateTime endDate, string description = "") 
        {
            this.Id = DateTime.Now.ToString();
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Description = description;
        }

        public bool IsValid()
        {
            if (this.Id is null || this.Id == String.Empty)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            return true;
        }

        public async override Task<bool> AddInJsonFile()
        {
            try
            {
                string barrierGateParent = JsonConvert.SerializeObject(BarrierGateSingleton.Instance.CurrentBarrierGate);
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/CalendarEvent/AddInJsonFile?barrierGateParent={barrierGateParent}&calendarEventToAdd={json}";
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
                string barrierGateParent = JsonConvert.SerializeObject(BarrierGateSingleton.Instance.CurrentBarrierGate);
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/CalendarEvent/RemoveInJsonFile?barrierGateParent={barrierGateParent}&calendarEventToRemove={json}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public async override Task<bool> ModifyInJsonFile(CalendarEvent elementModified)
        {
            try
            {
                string barrierGateParent = JsonConvert.SerializeObject(BarrierGateSingleton.Instance.CurrentBarrierGate);
                string json = JsonConvert.SerializeObject(this);
                string jsonOfEdited = JsonConvert.SerializeObject(elementModified);

                string endpoint =
                $"/CalendarEvent/EditInJsonFile?barrierGateParent=" +
                $"{barrierGateParent}&calendarEventToEdit={json}&calendarEventEdited={jsonOfEdited}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public async Task<List<CalendarEvent>?> GetAllFromJsonFile(BarrierGate barrierGate)
        {
            string barrierGateParent = JsonConvert.SerializeObject(barrierGate);
            string json = JsonConvert.SerializeObject(this);

            string endpoint = $"/CalendarEvent/GetAllFromOneBarrierGateFromJsonFile?barrierGateParent={barrierGateParent}";
            HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            string content = await response.Content.ReadAsStringAsync();
            List<CalendarEvent>? calendarEvents = JsonConvert.DeserializeObject<List<CalendarEvent>>(content);
            return calendarEvents;
        }

        public static bool operator ==(CalendarEvent CA1, CalendarEvent CA2)
        {
            if (CA2 is null)
            {
                return false;
            }

            if (CA1.Id == CA2.Id && CA2.Name == CA2.Name && CA2.StartDate == CA2.StartDate && CA1.EndDate == 
                CA2.EndDate && CA1.Description == CA2.Description)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(CalendarEvent CA1, CalendarEvent CA2)
        {
            if (CA2 is null)
            {
                return true;
            }

            if (CA1.Id != CA2.Id && CA2.Name != CA2.Name && CA2.StartDate != CA2.StartDate && CA1.EndDate !=
                CA2.EndDate && CA1.Description != CA2.Description)
            {
                return true;
            }
            return false;
        }
    }
}
