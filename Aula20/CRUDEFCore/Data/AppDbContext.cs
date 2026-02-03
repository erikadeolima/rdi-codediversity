using CRUDEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts => Set<Contact>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var contact = modelBuilder.Entity<Contact>();

            contact.ToTable("Contacts");

            contact.HasKey(x => x.Id);

            contact.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            contact.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            contact.HasIndex(x => x.Email)
                .IsUnique();

            contact.Property(x => x.CreatedAt)
                .IsRequired();
        }

    }
}
