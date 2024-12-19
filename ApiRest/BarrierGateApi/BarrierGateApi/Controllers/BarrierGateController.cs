using BarrierGateApi.DB;
using BarrierGateApi.DB.Context;
using BarrierGateApi.Models;
using BarrierGateApi.Singleton;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace BarrierGateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarrierGateController : ControllerGestionnableElements<BarrierGate>
    {
        protected override Context<BarrierGate> database { get; set; } = Database.Instance.BarrierGateDB;

        [HttpGet(nameof(this.TurnOn))]
        public async void TurnOn(string baseAdress, double timeAlive = 0) 
        {
            try
            {
                Uri UriBaseAdress = new Uri($"http://{baseAdress}");
                if (UriBaseAdress != BarrierGateSingleton.Instance.HttpClient.BaseAddress)
                {
                    BarrierGateSingleton.Instance.ChangeHttpClientBaseAdress(UriBaseAdress);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex);
            }


            if (timeAlive > 0)
            {
                BarrierGateSingleton.Instance.OpenBarrierGate((float)timeAlive);
            }
            else 
            {
                BarrierGateSingleton.Instance.OpenBarrierGate(stayOpen: true);
            }
            //HttpResponseMessage repsonse = await httpClient.GetAsync($"/rpc/Switch.Set?id=0&on=true");
            //TODO:
            //Insert in futur DB state of Shelly
        }

        /// <summary>
        /// Turn off the barrier gate immediately
        /// </summary>
        /// <param name="baseAdresse">
        /// It's the IP adresse of the barriergate you want to close
        /// </param>
        [HttpGet(nameof(this.TurnOff))]
        public async void TurnOff(string baseAdress)
        {
            try
            {
                Uri UriBaseAdress = new Uri($"http://{baseAdress}");
                if (UriBaseAdress != BarrierGateSingleton.Instance.HttpClient.BaseAddress)
                {
                    BarrierGateSingleton.Instance.ChangeHttpClientBaseAdress(UriBaseAdress);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex);
            }
            BarrierGateSingleton.Instance.CloseBarrierGate();
        }
    }
}
