using Microsoft.EntityFrameworkCore;
using GameService.Entities;
using MassTransit;

namespace GameService.Data;

public class GameDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameImage> GameImages { get; set; }
    public DbSet<MyGame> MyGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}