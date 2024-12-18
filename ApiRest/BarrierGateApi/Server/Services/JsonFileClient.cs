using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    
    public class JsonFileClient
    {
        public string Path { get; set; } = "C:\\TIH\\BarrierGate\\ApiRest\\Json\\data.json";
    
        public void Launch() 
        {
            

            while (true)
            {
                Thread.Sleep(1000);
                string json = "";
                try
                {
                    StreamReader reader = new StreamReader(Path);
                    json = reader.ReadToEnd();
                    reader.Close();

                    Console.WriteLine(json);
                }
                catch (Exception ex) 
                {
                    CheckAgain();
                }

                //if()
            }
        }

        public void CheckAgain() 
        {
            try
            {
                StreamReader reader = new StreamReader(Path);
                string json = reader.ReadToEnd();
                reader.Close();

                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                CheckAgain();
            }
        }
    }
}
