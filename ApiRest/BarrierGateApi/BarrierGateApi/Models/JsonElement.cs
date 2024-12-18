using BarrierGateApi.Singleton;
using Newtonsoft.Json;

namespace BarrierGateApi.Models
{
    public abstract class JsonElement<T> where T : class
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        private string _jsonFilePath = ConfigSingleton.Instance.ConfigParam.JsonPath;
        protected virtual string jsonFilePath
        {
            get => _jsonFilePath;
            set => this._jsonFilePath = value;
        }

        public abstract bool AddInJsonFile(object[] parents = null);
        public abstract bool RemoveInJsonFile(object[] parents = null);
        public abstract bool ModifyInJsonFile(T elementToModified, object[] parents = null);

        //Need to implement yourself a Get method
    }
}
