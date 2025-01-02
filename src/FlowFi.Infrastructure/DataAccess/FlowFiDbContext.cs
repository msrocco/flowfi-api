using FlowFi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess;

public class FlowFiDbContext : DbContext
{
    public FlowFiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

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

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");

            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.UserId).HasColumnName("user_id");
            entity.Property(t => t.BankAccountId).HasColumnName("bank_account_id");
            entity.Property(t => t.CategoryId).HasColumnName("category_id");
            entity.Property(t => t.Name).HasColumnName("name");
            entity.Property(t => t.Value).HasColumnName("value");
            entity.Property(t => t.Date).HasColumnName("date");
            entity.Property(t => t.Type).HasColumnName("type");

            entity.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.BankAccount)
                .WithMany()
                .HasForeignKey(t => t.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.UserId).HasColumnName("user_id");
            entity.Property(c => c.Name).HasColumnName("name");
            entity.Property(c => c.Icon).HasColumnName("icon");
            entity.Property(c => c.Type).HasColumnName("type");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
