using Microsoft.Graph.Models;
using Newtonsoft.Json;

namespace BarrierGateApi.Models
{
    public class EventCalendar
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("start")]
        protected DateTimeTimeZone startAt { get; set; }
        [JsonProperty("end")]
        protected DateTimeTimeZone endAt { get; set; }

        public double TimeToGoOpenInSeconds 
        {
            get => (GetEndTime() - GetStartTime()).TotalSeconds;
        }

        public DateTime GetStartTime() 
        {
            DateTime.TryParse(startAt.DateTime, out DateTime result);
            return result;
        }

        public DateTime GetEndTime()
        {
            DateTime.TryParse(endAt.DateTime, out DateTime result);
            return result;
        }

        public bool IsActuallyActive() 
        {
            DateTime now = DateTime.Now; 
            SetDebugDateTime(now);

            if(now.CompareTo(GetStartTime()) >= 0 && now.CompareTo(GetEndTime()) <= 0) 
            {
                return true;
            }
            return false;
        }

        public static EventCalendar? ToObject(string json) 
        {
            return JsonConvert.DeserializeObject<EventCalendar>(json);
        }

        public void SetDebugDateTime(DateTime now) 
        {
            startAt.DateTime = now.ToString();
            endAt.DateTime = now.AddSeconds(2).ToString();
        }
    }
}
