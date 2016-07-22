using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace RpiLaserHost
{
    [HubName("StopwatchHub")]
    public class StopwatchHub : Hub
    {
        private static readonly IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<StopwatchHub>();

        public static void Send(string command)
        {
            // Call the broadcastMessage method to update clients.
            hubContext.Clients.All.controlStopwatch(command);
        }
    }
}
