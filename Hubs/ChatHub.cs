using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using WebChat.Models;

namespace WebChat.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageStorage _database;

    public ChatHub(IMessageStorage database)
    {
        _database = database;
    }
    
    public async Task SendMessage(string user, string message)
    {
        var newMessage = new Message(user, message);
        _database.NewMessage(newMessage);
        string json = JsonSerializer.Serialize(newMessage);
        await Clients.All.SendAsync("ReceiveMessage", json);
    }

    public async Task Connected()
    {
        string json = JsonSerializer.Serialize(_database.Messages());
        await Clients.Client(Context.ConnectionId).SendAsync("LoadMessages", json);
    }
}