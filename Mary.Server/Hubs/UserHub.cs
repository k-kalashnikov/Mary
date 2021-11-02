using Microsoft.AspNetCore.SignalR;

namespace Mary.Server.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
