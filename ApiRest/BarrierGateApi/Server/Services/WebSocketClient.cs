using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

internal class WebSocketClient
{
    protected HttpClient httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7056"),
    };

    //This dummy is for test the value send by the outlook further
    protected string dummyPath = "C:\\TIH\\BarrierGateGestion\\ApiRest\\BarrierGateApi\\BarrierGateApi\\DummyData\\EventCalendar.json";

    public async Task ConnectToServer(string serverUri)
    {
        ClientWebSocket clientWebSocket = new ClientWebSocket();
        await clientWebSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);

        Console.WriteLine("Connected to the server. Start sending messages...");

        // Send messages to the server
        string message = "Hello, WebSocket!";
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

        // Receive messages from the server
        byte[] receiveBuffer = new byte[1024];
        while (clientWebSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                Console.WriteLine($"Received message from server: {receivedMessage}");

                //TODO:
                //Send the reiceive to the API
            }
        }
    }

    public async void SendJsonToApi(string json, string endpoint) 
    {    
        HttpResponseMessage response = await httpClient.GetAsync($"{endpoint}?s={json}");
        string sResponse = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(sResponse);
    }

    public void OpenTest() 
    {
        Thread.Sleep(5000);
        while (true) 
        {
            using (StreamReader r = new StreamReader(dummyPath))
            {
                string json = r.ReadToEnd();
                Console.WriteLine(json);
                SendJsonToApi(endpoint: $"/EventCalendar/NewEventCalendar", json: json);
            }
            Thread.Sleep(10000);
        }
    }
}

