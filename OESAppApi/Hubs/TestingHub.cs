using Microsoft.AspNetCore.SignalR;

namespace OESAppApi.Hubs;

public class TestingHub : Hub
{
    public async Task SendMessage()
    {
        await Clients.All.SendAsync("test", "aaaaa");
    }
}
