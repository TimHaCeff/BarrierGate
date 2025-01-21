namespace BarrierGateApi.Controllers
{
    public class BarrierGateController
    {
        protected const float DEFAULT_OPEN_TIME = 20;
        public async void OpenBarrierGate(Func<string, Task<HttpResponseMessage?>> tryProcessFunction, bool stayOpen = false)
        {
            string request;
            if (stayOpen)
            {
                request = $"rpc/Switch.SetConfig?id=0&config={{auto_off:false}}";
            }
            else
            {
                request = $"rpc/Switch.SetConfig?id=0&config={{auto_off:true, auto_off_delay:{DEFAULT_OPEN_TIME}}}";
            }

            HttpResponseMessage? setConfigResponse = await tryProcessFunction(request);
            HttpResponseMessage? repsonse = await tryProcessFunction($"rpc/Switch.Set?id=0&on=true");
        }
        public async void OpenBarrierGate(Func<string, Task<HttpResponseMessage?>> tryProcessFunction, float seconds)
        {
            string request = $"rpc/Switch.SetConfig?id=0&config={{auto_off:true, auto_off_delay:{seconds}}}";
            HttpResponseMessage? setConfigResponse = await tryProcessFunction(request);
            HttpResponseMessage? repsonse = await tryProcessFunction($"rpc/Switch.Set?id=0&on=true");
        }

        public async void CloseBarrierGate(Func<string, Task<HttpResponseMessage?>> tryProcessFunction)
        {
            HttpResponseMessage? repsonse = await tryProcessFunction($"rpc/Switch.Set?id=0&on=false");
        }
    }
}
