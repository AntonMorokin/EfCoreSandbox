using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace EfCoreSandbox.EF
{
    internal sealed class DatabaseContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Client> Clients { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.OpeningDate)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.ClientId);

            modelBuilder.Entity<Account>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.ClientId)
                .IsRequired();
        }
    }
}
