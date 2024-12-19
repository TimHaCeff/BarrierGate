using BarrierGateGUI.Singletons;
using Newtonsoft.Json;
using System.Globalization;

namespace BarrierGateGUI.Model
{
    public class CalendarEvent : GestionnableElement<CalendarEvent>
    {

        [JsonProperty("start_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [JsonProperty("end_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime EndDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("strt_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }


        [JsonIgnore]
        protected override string gestionnableElementName { get; set; } = nameof(CalendarEvent);

        public CalendarEvent() 
        {
            DateTime now = DateTime.Now;
            this.StartDate = now;
            this.EndDate = now;
        }

        public CalendarEvent(string name, DateTime startDate, DateTime endDate, string description = "") 
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Description = description;
        }

        public bool IsValid()
        {
            if (this.Id is null)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            return true;
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
