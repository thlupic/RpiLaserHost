using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace RpiLaserHostWpf
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
