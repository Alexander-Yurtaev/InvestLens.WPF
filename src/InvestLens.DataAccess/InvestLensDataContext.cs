using InvestLens.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public sealed class InvestLensDataContext : DbContext
{
    public DbSet<User> Users { get; set; }

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
    }

    #endregion
}