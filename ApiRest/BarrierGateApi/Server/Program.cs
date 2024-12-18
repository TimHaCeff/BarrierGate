using Server.Services;
using System.Net.WebSockets;

class Program
{
    static void Main(string[] args)
    {
        //WebSocketClient webSocketClient = new WebSocketClient();
        //webSocketClient.OpenTest();

        JsonFileClient client = new JsonFileClient();
        client.Launch();
    }
}