using Newtonsoft.Json;

namespace BarrierGateGUI.Model
{
    public abstract class JsonElement<T> where T : class
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        private string _jsonFilePath = Config.BARRIER_GATE_JSON_FILE_PATH;
        protected virtual string jsonFilePath 
        {
            get => _jsonFilePath;
            set => this._jsonFilePath = value; 
        }

        public abstract Task<bool> AddInJsonFile();
        public abstract Task<bool> RemoveInJsonFile();
        public abstract Task<bool> ModifyInJsonFile(T elementToModified);

        //Need to implement yourself a Get method
    }
}
