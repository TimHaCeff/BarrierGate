using BarrierGateApi.Models;

namespace BarrierGateApi.Singleton
{
    public class BarrierGateSingleton : Singleton<BarrierGateSingleton>
    {
        private BarrierGateSingleton() { }

        public HttpClient HttpClient { get; protected set; } = new HttpClient
        {
            BaseAddress = new Uri("http://157.26.121.88"),
        };

        protected const float DEFAULT_OPEN_TIME = 20;


        public async void OpenBarrierGate(bool stayOpen = false)
        {
            string request;
            if (stayOpen) 
            {
                request = $"/rpc/Switch.SetConfig?id=0&config={{auto_off:false}}";
            }
            else 
            {
                request = $"/rpc/Switch.SetConfig?id=0&config={{auto_off:true, auto_off_delay:{DEFAULT_OPEN_TIME}}}";
            }

            HttpResponseMessage? setConfigResponse = await TryGetAsync(request);
            HttpResponseMessage? repsonse = await TryGetAsync($"/rpc/Switch.Set?id=0&on=true");
        }
        public async void OpenBarrierGate(float seconds)
        {
            string request = $"/rpc/Switch.SetConfig?id=0&config={{auto_off:true, auto_off_delay:{seconds}}}";
            HttpResponseMessage? setConfigResponse = await TryGetAsync(request);
            HttpResponseMessage? repsonse = await TryGetAsync($"/rpc/Switch.Set?id=0&on=true");
        }

        public async void CloseBarrierGate() 
        {
            HttpResponseMessage? repsonse = await TryGetAsync($"/rpc/Switch.Set?id=0&on=false");
        }

        public async void CreateSchedule(CalendarEvent calendarEvent) 
        {
            string endPointPrefix = "/rpc/Schedule.Create?";
            string sParams = "* * * * * *";
        }

        protected async Task<HttpResponseMessage?>? TryGetAsync(string endPoint)
        {
            try
            {
                return await HttpClient.GetAsync(endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void ChangeHttpClientBaseAdress(Uri baseAdress)
        {
            this.HttpClient.Dispose();
            this.HttpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1.5),
                BaseAddress = baseAdress,
            };
        }
    }
}
