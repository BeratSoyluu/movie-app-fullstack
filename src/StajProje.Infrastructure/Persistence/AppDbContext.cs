using Microsoft.EntityFrameworkCore;
using StajProje.Domain.Entities;

namespace StajProje.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>().HasData(
        new User { Id = 1, UserName = "user1", Password = "1234", Role = "Movie" },
        new User { Id = 2, UserName = "user2", Password = "1234", Role = "Actor" },
        new User { Id = 3, UserName = "user3", Password = "1234", Role = "Admin" }
    );
}
}