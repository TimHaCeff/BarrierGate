using BarrierGateApi.DB;
using BarrierGateApi.DB.Context;
using BarrierGateApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarrierGateApi.Controllers
{
    public abstract class ControllerGestionnableElements<T> : ControllerBase where T : GestionnableElement
    {
        protected abstract Context<T> database { get; set; }

        [HttpGet(nameof(this.Get))]
        public async Task<string> Get(int id)
        {
            try
            {
                T bg = await database.Get(id);
                return JsonConvert.SerializeObject(bg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [HttpGet(nameof(this.GetAll))]
        public async Task<string> GetAll()
        {
            List<T> bg = await database.GetAll();
            return JsonConvert.SerializeObject(bg);
        }

        [HttpGet(nameof(this.Add))]
        public async Task<bool> Add(string json_to_add)
        {
            try
            {
                if (json_to_add[0] == '[')
                {
                    List<T> listOfBG = JsonConvert.DeserializeObject<List<T>>(json_to_add);
                    await database.Add(listOfBG);
                    return true;
                }
                else
                {
                    T baarrierGate = JsonConvert.DeserializeObject<T>(json_to_add);
                    await database.Add(baarrierGate);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        [HttpGet(nameof(this.Delete))]
        public async Task<bool> Delete(string json_to_delete)
        {
            try
            {
                if (json_to_delete[0] == '[')
                {
                    List<T> listOfBG = JsonConvert.DeserializeObject<List<T>>(json_to_delete);
                    await database.Delete(listOfBG);
                    return true;
                }
                else
                {
                    T baarrierGate = JsonConvert.DeserializeObject<T>(json_to_delete);
                    await database.Delete(baarrierGate);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        [HttpGet(nameof(this.DeleteAll))]
        public async Task<bool> DeleteAll()
        {
            try
            {
                await database.DeleteAll();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        [HttpGet(nameof(this.Update))]
        public async Task<bool> Update(string json_edited)
        {
            try
            {
                if (json_edited[0] == '[')
                {
                    List<T> listOfBG = JsonConvert.DeserializeObject<List<T>>(json_edited);
                    await database.Update(listOfBG);
                    return true;
                }
                else
                {
                    T baarrierGate = JsonConvert.DeserializeObject<T>(json_edited);
                    await database.Update(baarrierGate);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
