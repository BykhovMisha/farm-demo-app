using FarmDemoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmDemoApp.DataAccess;

public class FarmContext : DbContext
{
    public FarmContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{

    //    optionsBuilder.UseInMemoryDatabase(databaseName: "FarmDb");
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Animal>(entity => 
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Name)
                .IsUnique();
            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();
        });
    }
}

