using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("client - wait for service to be ready, please press return afterwards...");
                Console.ReadLine();
                var conn = new HubConnectionBuilder().WithUrl("http://localhost:7071/api/").Build();
                // var conn = new HubConnectionBuilder().WithUrl("https://techconclick.azurewebsites.net/api/").Build();

                Console.WriteLine("adding the handler");
                conn.On("buttonClicked", (string s) =>
                {
                    Console.WriteLine($"Buttton clicked with message {Environment.NewLine}{s}");
                });

                await conn.StartAsync();
                Console.WriteLine($"connection started, waiting for events {conn.State}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
