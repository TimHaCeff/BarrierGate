using System.Reflection.Metadata.Ecma335;

namespace BarrierGateGUI.Singletons
{
    public class FileSingleton : Singleton<FileSingleton>
    {
        private FileSingleton() { }

        public static FileSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (threadSafeLocker)
                    {
                        if (instance == null)
                        {
                            instance = new FileSingleton();
                        }
                    }
                }
                return instance;
            }
        }

        protected bool jsFileAlreadyUsed { get; set; } = false;

        protected string lastAvailableJsonFile { get; set; } = string.Empty;

        public string JsonFilePath { get;set; } = Config.BARRIER_GATE_JSON_FILE_PATH;

        public string JsonFile 
        {
            get
            {
                string json = "";
                try
                {
                    StreamReader reader = new StreamReader(JsonFilePath);
                    json = reader.ReadToEnd();
                    reader.Close();

                    lastAvailableJsonFile = json;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                    json = lastAvailableJsonFile;
                }
               
                return json;
            }

            set 
            {
                try
                {
                    File.WriteAllText(Config.BARRIER_GATE_JSON_FILE_PATH, value);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
