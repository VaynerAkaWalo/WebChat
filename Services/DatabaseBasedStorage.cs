using Microsoft.EntityFrameworkCore;
using WebChat.Models;

namespace WebChat.Services;


public class DatabaseBasedStorage : IMessageStorage
{
    private readonly MessageContext _context;

    public DatabaseBasedStorage(MessageContext messageContext)
    {
        _context = messageContext;
    }
    
    public void NewMessage(Message message)
    {
        _context.Messages.Add(message);
        _context.SaveChanges();
    }

    public IEnumerable<Message> Messages()
    {
        return _context.Messages.ToList();
    }
}