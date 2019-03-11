using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("client - wait for service");
            Console.ReadLine();
            var conn = new HubConnectionBuilder().WithUrl("http://localhost:7071/api/").Build();

            conn.On("buttonClicked", (string s) =>
            {
                Console.WriteLine($"Buttton clicked with message {s}");
            });

            await conn.StartAsync();
            Console.ReadLine();
        }
    }
}
