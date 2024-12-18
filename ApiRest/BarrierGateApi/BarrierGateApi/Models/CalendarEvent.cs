using BarrierGateApi.Singleton;
using Newtonsoft.Json;

namespace BarrierGateApi.Models
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
        public DateTime? StartTime { get; set; }
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

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

        //public static CalendarEvent? JsonToObject(string json) 
        //{
        //    JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
        //    return JsonConvert.DeserializeObject<CalendarEvent>(json, settings);
        //}

        public bool IsValid()
        {
            if (this.Id is null || this.Id == String.Empty)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            if (this.Description is null || this.Description == String.Empty)
            { return false; }

            return true;
        }

        public override bool AddInJsonFile(object[] parents)
        {
            try
            {
                BarrierGate bg = new BarrierGate();
                List<BarrierGate> barrierGateFromJson = bg.GetAllFromJsonFile();

                BarrierGate currentBG = barrierGateFromJson.Where(x => x == parents[0] as BarrierGate).FirstOrDefault();
                currentBG.CalendarEvents.Add(this);
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

        public override bool RemoveInJsonFile(object[] parents = null)
        {
            try
            {
                BarrierGate bg = new BarrierGate();
                List<BarrierGate> barrierGateFromJson = bg.GetAllFromJsonFile();
                BarrierGate currentBarrierGate = barrierGateFromJson.Where(x => x == parents[0]).FirstOrDefault();
                CalendarEvent calendarEvent = currentBarrierGate.CalendarEvents.Where(x => x == this).FirstOrDefault();
                currentBarrierGate.CalendarEvents.Remove(calendarEvent);
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

        public override bool ModifyInJsonFile(CalendarEvent elementToUseToModified, object[] parents = null)
        {
            try
            {
                BarrierGate bg = new BarrierGate();
                List<BarrierGate> barrierGateFromJson = bg.GetAllFromJsonFile();
                BarrierGate barrierGateToEdit = barrierGateFromJson.Where(x => x == parents[0]).FirstOrDefault();
                CalendarEvent currentCalendarEvent = barrierGateToEdit.CalendarEvents.Where(x => x == this).FirstOrDefault();

                currentCalendarEvent.Id = elementToUseToModified.Id;
                currentCalendarEvent.StartDate = elementToUseToModified.StartDate;
                currentCalendarEvent.EndDate = elementToUseToModified.EndDate;
                currentCalendarEvent.Name = elementToUseToModified.Name;
                currentCalendarEvent.Description = elementToUseToModified.Description;
                currentCalendarEvent.StartTime = elementToUseToModified.StartTime;
                currentCalendarEvent.EndTime = elementToUseToModified.EndTime;

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

        public List<CalendarEvent>? GetAllFromJsonFile(BarrierGate barrierGate)
        {
            BarrierGate bg = new BarrierGate();
            List<BarrierGate> barrierGateFromJson = bg.GetAllFromJsonFile();
            BarrierGate currentBarrierGate = barrierGateFromJson.Where(x => x == barrierGate).FirstOrDefault();
            return currentBarrierGate.CalendarEvents;
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
