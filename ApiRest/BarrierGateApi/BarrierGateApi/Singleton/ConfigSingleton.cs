using Newtonsoft.Json;
using BarrierGateApi.Models;

namespace BarrierGateApi.Singleton
{
    public class ConfigSingleton : Singleton<ConfigSingleton>
    {
        private ConfigSingleton() { }

        protected static string CONFIG_FILE_PATH = "./Json/Config.json";


        private Config _configParam = null;
        public Config ConfigParam 
        {
            get 
            {
                if(_configParam is null) 
                {
                    StreamReader reader = new StreamReader(CONFIG_FILE_PATH);
                    string json = reader.ReadToEnd();
                    reader.Close();
                    _configParam = JsonConvert.DeserializeObject<Config>(json);
                }
                return _configParam;
            }
        }
    }
}
