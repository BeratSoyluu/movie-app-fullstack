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
}