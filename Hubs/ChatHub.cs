using Microsoft.AspNetCore.SignalR;
using WebChat.Models;

namespace WebChat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        var newMessage = new Message(user, message);
        await Clients.All.SendAsync("ReceiveMessage", newMessage.Username, newMessage.Text, newMessage.Date);
    }
}