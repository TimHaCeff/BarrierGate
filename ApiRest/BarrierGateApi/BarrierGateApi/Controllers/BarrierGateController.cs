using BarrierGateApi.Models;
using BarrierGateApi.Singleton;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace BarrierGateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarrierGateController : ControllerBase
    {
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

        [HttpGet(nameof(this.GetAllFromJsonFile))]
        public string GetAllFromJsonFile() 
        {
            return FileSingleton.Instance.JsonFile;
        }

        [HttpGet(nameof(this.AddInJsonFile))]
        public bool AddInJsonFile(string barrierGateToAdd)
        {
            try
            {
                BarrierGate barrierGate = JsonConvert.DeserializeObject<BarrierGate>(barrierGateToAdd);
                return barrierGate.AddInJsonFile();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpGet(nameof(this.RemoveInJsonFile))]
        public bool RemoveInJsonFile(string jsonOfBarrierGateToRemove)
        {
            try
            {
                BarrierGate barrierGate = JsonConvert.DeserializeObject<BarrierGate>(jsonOfBarrierGateToRemove);
                return barrierGate.RemoveInJsonFile();
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        [HttpGet(nameof(this.ModifyInJsonFile))]
        public bool ModifyInJsonFile(string jsonOfBarrierGateToEdit, string jsonOfBarrierGateEdited)
        {
            try
            {
                BarrierGate barrierGate = JsonConvert.DeserializeObject<BarrierGate>(jsonOfBarrierGateToEdit);
                BarrierGate modifiedBarrierGate = JsonConvert.DeserializeObject<BarrierGate>(jsonOfBarrierGateEdited);
                return barrierGate.ModifyInJsonFile(modifiedBarrierGate);
            }
            catch (Exception ex) 
            {
                return false ;
            }
        }
    }
}
