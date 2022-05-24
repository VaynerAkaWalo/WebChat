using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.Models;

[Table("messages")]
public class Message
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    
    public Message(string username, string text)
    {
        Date = DateTime.Now;
        Username = username;
        Text = text;
    }
}