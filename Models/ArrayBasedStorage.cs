namespace WebChat.Models;

public class ArrayBasedStorage : IMessageStorage
{
    private readonly static int Size = 30;

    private readonly Message[] _database;

    public ArrayBasedStorage()
    {
        _database = new Message[Size];
    }

    public void NewMessage(Message message)
    {
        ArrayShift();

        _database[0] = message;
    }

    public IEnumerable<Message> Messages()
    {
        return _database;
    }

    private void ArrayShift()
    {
        for (int i = _database.Length - 1; i > 0; i--)
        {
            _database[i] = _database[i - 1];
        }
    }

}