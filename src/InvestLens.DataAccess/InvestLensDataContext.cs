using InvestLens.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public sealed class InvestLensDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public InvestLensDataContext(DbContextOptions<InvestLensDataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    #region Overrides of DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Login).IsUnique();
            entity.Property(u => u.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(200);
            entity.Property(p => p.PortfolioType).IsRequired();
            entity.HasOne(p => p.Owner)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.ChildrenPortfolios)
                  .WithOne(p => p.ParentPortfolio)
                  .HasForeignKey(p => p.ParentPortfolioId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(p => p.Event).IsRequired();
            entity.Property(p => p.Date).IsRequired();
            entity.Property(p => p.Symbol).IsRequired();
            entity.Property(p => p.Price).IsRequired();
            entity.Property(p => p.Quantity).IsRequired();
            entity.Property(p => p.Currency).IsRequired();
            entity.Property(p => p.FeeTax).IsRequired();
            entity.Property(p => p.Exchange).IsRequired();
            entity.Property(p => p.NKD).IsRequired();
            entity.Property(p => p.FeeCurrency).IsRequired();
            entity.Property(p => p.DoNotAdjustCash).IsRequired();
            entity.Property(p => p.Note).HasDefaultValue("");
            entity.HasOne(t => t.Portfolio)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(t => t.PortfolioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    #endregion
}