using WebChat.Models;
namespace WebChat.Services;

public class QueueBasedStorage : IMessageStorage
{
    private readonly static int Size = 30;

    private readonly Queue<Message> _database;

    public QueueBasedStorage()
    {
        _database = new Queue<Message>();
    }
    
    public void NewMessage(Message message)
    {
        if (_database.Count == 30)
        {
            _database.Dequeue();
        }

        _database.Enqueue(message);
    }

    public IEnumerable<Message> Messages()
    {
        return _database;
    }
}