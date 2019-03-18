using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreSignalRService.Hubs
{
    public class IOTButtonHub : Hub
    {
        public IOTButtonHub()
        {

        }

        public override Task OnConnectedAsync() => base.OnConnectedAsync();

        public Task ButtonClicked(object data)
        {
            return base.Clients.All.SendAsync("Waiter", data);
        }
    }
}
