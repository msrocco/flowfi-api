using FlowFi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess;

public class FlowFiDbContext : DbContext
{
    public FlowFiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
        modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name");
        modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
        modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.ToTable("bank_accounts");

            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Name).HasColumnName("name");
            entity.Property(u => u.Color).HasColumnName("color");
            entity.Property(u => u.InitialBalance).HasColumnName("initial_balance");
            entity.Property(b => b.Type).HasColumnName("type");
            entity.Property(b => b.UserId).HasColumnName("user_id");
        });
    }
}
