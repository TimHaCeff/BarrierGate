using BarrierGateApi.DB;
using BarrierGateApi.DB.Context;
using BarrierGateApi.Interfaces;
using BarrierGateApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarrierGateApi.Controllers
{
    public abstract class ControllerGestionnableElements<T> : ControllerBase where T : GestionnableElement
    {
        private readonly Context _context;

        public ControllerGestionnableElements(Context context)
        {
            _context = context;
        }

        [HttpGet(nameof(this.Get))]
        public async Task<string> Get(int id)
        {
            try
            {
                T bg = await ((IDbBasicActions<T>)_context).Get(_context.ReturnDBSet<T>(), id);
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
            List<T> bg = await ((IDbBasicActions<T>)_context).GetAll(_context.ReturnDBSet<T>());
            return JsonConvert.SerializeObject(bg);
        }

        [HttpGet(nameof(this.Add))]
        public async Task<bool> Add(string json_to_add)
        {
            try
            {
                if (json_to_add[0] == '[')
                {
                    List<T> listOfObj = JsonConvert.DeserializeObject<List<T>>(json_to_add);
                    await ((IDbBasicActions<T>)_context).Add(_context.ReturnDBSet<T>(), listOfObj);
                    await _context.Save();
                    return true;
                }
                else
                {
                    T obj = JsonConvert.DeserializeObject<T>(json_to_add);
                    await ((IDbBasicActions<T>)_context).Add(_context.ReturnDBSet<T>(), obj);
                    await _context.Save();
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
                    List<T> listOfObj = JsonConvert.DeserializeObject<List<T>>(json_to_delete);
                    await ((IDbBasicActions<T>)_context).Delete(_context.ReturnDBSet<T>(), listOfObj);
                    await _context.Save();
                    return true;
                }
                else
                {
                    T obj = JsonConvert.DeserializeObject<T>(json_to_delete);
                    await ((IDbBasicActions<T>)_context).Delete(_context.ReturnDBSet<T>(), obj);
                    await _context.Save();
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
                await ((IDbBasicActions<T>)_context).DeleteAll(_context.ReturnDBSet<T>());
                await _context.Save();
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
                    List<T> listOfObj = JsonConvert.DeserializeObject<List<T>>(json_edited);
                    await ((IDbBasicActions<T>)_context).Update(_context.ReturnDBSet<T>(), listOfObj);
                    await _context.Save();
                    return true;
                }
                else
                {
                    T obj = JsonConvert.DeserializeObject<T>(json_edited);
                    await ((IDbBasicActions<T>)_context).Update(_context.ReturnDBSet<T>(), obj);
                    await _context.Save();
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
