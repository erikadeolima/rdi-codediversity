using Microsoft.EntityFrameworkCore;
using AulaTeste20.Models;

namespace AulaTeste20.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Contact>(entity =>
      {
        entity.ToTable("Contacts");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).UseIdentityColumn();
        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
        entity.Property(e => e.CreatedAt).IsRequired();
        entity.HasIndex(e => e.Email).IsUnique();
      });
    }
  }
}

