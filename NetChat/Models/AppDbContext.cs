using Microsoft.EntityFrameworkCore;

namespace NetChat.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }
}
