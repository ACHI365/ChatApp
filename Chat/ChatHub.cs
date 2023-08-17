using ChatApp.Models;

namespace ChatApp.Chat;

using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(Message message, List<string> tags)
    {
        await Clients.All.SendAsync("ReceiveMessage", message, tags);
    }
}