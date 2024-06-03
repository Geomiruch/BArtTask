using BArtTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BArtTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
            .HasIndex(c => c.Email)
            .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Name)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Contact)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ContactId);

            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Account)
                .WithMany(a => a.Incidents)
                .HasForeignKey(i => i.AccountId);

            modelBuilder.Entity<Incident>()
                .Property(i => i.Name)
                .ValueGeneratedOnAdd();
        }
    }
}
