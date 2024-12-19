using BarrierGateGUI.Singletons;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrierGateGUI.Model
{
    public abstract class GestionnableElement<T> where T : class
    {
        [JsonProperty("id")]
        public int? Id { get; set; } = null;
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        protected abstract string gestionnableElementName { get; set; }


        public async Task<bool> Add()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/{gestionnableElementName}/Add?json_to_add={json}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public async Task<bool> Delete()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);

                string endpoint = $"/{gestionnableElementName}/Delete?json_to_delete={json}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public async Task<bool> Update(T elementModified)
        {
            try
            {
                string jsonOfEdited = JsonConvert.SerializeObject(elementModified);

                string endpoint = $"/{gestionnableElementName}/Update?json_edited={jsonOfEdited}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public async Task<T?> Get(int id) 
        {
            try
            {
                string endpoint = $"/{gestionnableElementName}/Get?id={id}";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                T? element = JsonConvert.DeserializeObject<T>(content);
                return element;
            }catch(Exception ex) 
            {
                Console.Write(ex.Message);
            }
            return default(T?);
        }

        public async Task<List<T>?> GetAll()
        {
            try
            {
                string endpoint = $"/{gestionnableElementName}/GetAll";
                HttpResponseMessage response = await BarrierGateSingleton.Instance.GetHttpClienInstance().GetAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                List<T>? elements = JsonConvert.DeserializeObject<List<T>>(content);
                return elements;
            }
            catch (Exception ex) 
            {
                Console.Write(ex.Message);
            }
            return default(List<T>);
        }
    }
}
