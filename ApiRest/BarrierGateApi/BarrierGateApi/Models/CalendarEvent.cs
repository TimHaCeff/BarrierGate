using BarrierGateApi.Singleton;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrierGateApi.Models
{
    [Table("calendar_event")]
    public class CalendarEvent : GestionnableElement
    {
        [JsonProperty("start_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("description")]
        [Column("description")]
        public string Description { get; set; }


        [JsonProperty("start_time")]
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        [Column("end_time")]
        public DateTime EndTime { get; set; }

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

        public float GetOpenTimeInSeconds()
        {
            TimeSpan dateDiffInSeconds = (this.EndDate - this.StartDate);
            TimeSpan timeDiffInSeconds = (this.EndTime - this.StartTime);

            return (float)(dateDiffInSeconds.TotalSeconds + timeDiffInSeconds.TotalSeconds);
        }

        public bool IsValid()
        {
            if (this.Id is null)
            { return false; }

            if (this.Name is null || this.Name == String.Empty)
            { return false; }

            if (this.Description is null || this.Description == String.Empty)
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
