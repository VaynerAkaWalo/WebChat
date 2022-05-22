namespace WebChat.Models;

public class Message
{
    public string Username { get; }
    public string Text { get; }
    
    public DateTime Date { get; }

    public Message(string username, string text)
    {
        Date = DateTime.Now;
        Username = username;
        Text = text;
    }
}