using BarrierGateGUI.Model;

namespace BarrierGateGUI.Singletons
{
    public class CalendarEventSingleton : Singleton<CalendarEventSingleton>
    {
        private CalendarEventSingleton() { }

        public List<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();
        public CalendarEvent CurrentCalendarEvent { get; set; } = new CalendarEvent();

        protected HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7056"),
        };

        public HttpClient GetHttpClienInstance()
        {
            return httpClient;
        }

        public void SetCurrentCalendarEvent(CalendarEvent calendarEvent)
        {
            this.CurrentCalendarEvent = calendarEvent;
        }
    }
}
