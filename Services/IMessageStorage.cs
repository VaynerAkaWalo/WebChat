namespace WebChat.Models;

public interface IMessageStorage
{
    public void NewMessage(Message message);

    public IEnumerable<Message> Messages();
}