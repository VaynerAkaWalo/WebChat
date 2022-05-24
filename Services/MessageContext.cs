using Microsoft.EntityFrameworkCore;
using WebChat.Models;
namespace WebChat.Services;

public class MessageContext : DbContext
{
    public MessageContext(DbContextOptions<MessageContext> options) : base(options)
    {
        
    }
    
    public DbSet<Message> Messages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("webchat");
        modelBuilder.Entity<Message>().ToTable("messages");
    }
}