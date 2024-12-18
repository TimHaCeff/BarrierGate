namespace BarrierGateApi.Singleton
{
    public class FileSingleton : Singleton<FileSingleton>
    {
        private FileSingleton() { }

        protected string lastAvailableJsonFile { get; set; } = string.Empty;

        public string JsonFilePath { get; set; } = ConfigSingleton.Instance.ConfigParam.JsonPath;

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
                    File.WriteAllText(JsonFilePath, value);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
