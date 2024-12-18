using Newtonsoft.Json;
using System.Globalization;

namespace BarrierGateApi.Models
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = "yyyy-MM-ddTHH:mm:ss";  // Format to use

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(_dateFormat)); // Convert DateTime to the required format
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string dateString = reader.Value.ToString();
            DateTime dateTime = DateTime.Parse(dateString);
            return DateTime.ParseExact(dateTime.ToString(_dateFormat), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
