using WebChat.Models;

namespace WebChat.Services;

public interface IMessageStorage
{
    public void NewMessage(Message message);

    public IEnumerable<Message> Messages();
}