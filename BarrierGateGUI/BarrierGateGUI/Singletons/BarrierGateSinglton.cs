using BarrierGateGUI.Model;

namespace BarrierGateGUI.Singletons
{
    public class BarrierGateSingleton : Singleton<BarrierGateSingleton>
    {
        private BarrierGateSingleton() { }

        public BarrierGate CurrentBarrierGate { get; set; } = new BarrierGate();
        public CalendarEvent CurrentCalendarEvent { get; set; } = new CalendarEvent();

        public List<BarrierGate> barrierGates { get; set; } = new List<BarrierGate>();

        protected HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7056"),
        };

        public HttpClient GetHttpClienInstance() 
        {
            return httpClient;
        }

        public void SetCurrentBarrierGate(BarrierGate barrierGate)
        {
            this.CurrentBarrierGate = barrierGate;
        }
    }
}
