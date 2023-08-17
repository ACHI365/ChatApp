using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class DataContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Message>()
            .Property(m => m.TagsJson)
            .HasColumnName("Tags");

        base.OnModelCreating(modelBuilder);
    }
}