using InvestLens.Model.Entities;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.MoexApi.Responses.ResponseItems;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public sealed class InvestLensDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Security> Securities { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    //Settings
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Market> Markets { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardGroup> BoardGroups { get; set; }
    public DbSet<Duration> Durations { get; set; }
    public DbSet<SecurityType> SecurityTypes { get; set; }
    public DbSet<SecurityGroup> SecurityGroups { get; set; }
    public DbSet<SecurityCollection> SecurityCollections { get; set; }

    public InvestLensDataContext(DbContextOptions<InvestLensDataContext> options) : base(options)
    {
    }

    #region Overrides of DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        InitSttingsTables(modelBuilder);

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

        modelBuilder.Entity<Security>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SecId).IsRequired();
            entity.Property(p => p.ShortName).IsRequired().HasMaxLength(189);
            entity.Property(p => p.RegNumber).HasMaxLength(189);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(765);
            entity.Property(p => p.Isin).IsRequired().HasMaxLength(51);
            entity.Property(p => p.IsTraded);
            entity.Property(p => p.EmitentTitle).HasMaxLength(765);
            entity.Property(p => p.SecTypeId).IsRequired();
            entity.Property(p => p.SecGroupId);
            entity.Property(p => p.PrimaryBoardid).IsRequired().HasMaxLength(12);
            entity.Property(p => p.MarketpriceBoardid).HasMaxLength(12);
            entity.Property(p => p.IsLoaded);

            entity.HasOne(s => s.SecType)
                  .WithMany()
                  .HasForeignKey(s => s.SecTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.SecGroup)
                  .WithMany()
                  .HasForeignKey(s => s.SecGroupId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Event).IsRequired();
            entity.Property(p => p.Date).IsRequired();
            entity.Property(p => p.Symbol).IsRequired();
            entity.Property(p => p.Price).IsRequired();
            entity.Property(p => p.Quantity).IsRequired();
            entity.Property(p => p.Currency).IsRequired();
            entity.Property(p => p.FeeTax).IsRequired();
            entity.Property(p => p.Exchange).IsRequired();
            entity.Property(p => p.NKD);
            entity.Property(p => p.FeeCurrency).HasDefaultValue("");
            entity.Property(p => p.DoNotAdjustCash);
            entity.Property(p => p.Note).HasDefaultValue("");
            entity.HasOne(t => t.Portfolio)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(t => t.PortfolioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    #endregion

    private void InitSttingsTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Engine>(entity =>
        {
            entity.ToTable("Settings.Engines");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(45);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(765);
        });

        modelBuilder.Entity<Market>(entity =>
        {
            entity.ToTable("Settings.Markets");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.TradeEngineId).IsRequired();
            entity.Property(p => p.TradeEngineName).IsRequired().HasMaxLength(45);
            entity.Property(p => p.TradeEngineTitle).IsRequired().HasMaxLength(765);
            entity.Property(p => p.MarketName).IsRequired().HasMaxLength(45);
            entity.Property(p => p.MarketTitle).IsRequired().HasMaxLength(765);
            entity.Property(p => p.MarketId).IsRequired();
            entity.Property(p => p.MarketPlace).IsRequired().HasMaxLength(48);
            entity.Property(p => p.IsOtc).IsRequired();
            entity.Property(p => p.HasHistoryFiles).IsRequired();
            entity.Property(p => p.HasHistoryTradesFiles).IsRequired();
            entity.Property(p => p.HasTrades).IsRequired();
            entity.Property(p => p.HasHistory).IsRequired();
            entity.Property(p => p.HasCandles).IsRequired();
            entity.Property(p => p.HasOrderbook).IsRequired();
            entity.Property(p => p.HasTradingsession).IsRequired();
            entity.Property(p => p.HasExtraYields).IsRequired();
            entity.Property(p => p.HasDelay).IsRequired();
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.ToTable("Settings.Boards");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.BoardGroupId).IsRequired();
            entity.Property(p => p.EngineId).IsRequired();
            entity.Property(p => p.MarketId).IsRequired();
            entity.Property(p => p.BoardId).HasMaxLength(12);
            entity.Property(p => p.BoardTitle).IsRequired().HasMaxLength(381);
            entity.Property(p => p.IsTraded).IsRequired();
            entity.Property(p => p.HasCandles).IsRequired();
            entity.Property(p => p.IsPrimary).IsRequired();
        });

        modelBuilder.Entity<BoardGroup>(entity =>
        {
            entity.ToTable("Settings.BoardGroups");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.TradeEngineId).IsRequired();
            entity.Property(p => p.TradeEngineName).IsRequired().HasMaxLength(45);
            entity.Property(p => p.TradeEngineTitle).IsRequired().HasMaxLength(765);
            entity.Property(p => p.MarketId).IsRequired();
            entity.Property(p => p.MarketName).IsRequired().HasMaxLength(45);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(192);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(765);
            entity.Property(p => p.IsDefault).IsRequired();
            entity.Property(p => p.board_group_id).IsRequired();
            entity.Property(p => p.IsTraded).IsRequired();
            entity.Property(p => p.IsOrderDriven).IsRequired();
            entity.Property(p => p.Category).IsRequired().HasMaxLength(45);
        });

        modelBuilder.Entity<Duration>(entity =>
        {
            entity.ToTable("Settings.Durations");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Interval).IsRequired();
            entity.Property(p => p.DurationCount).IsRequired();
            entity.Property(p => p.Days);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(765);
            entity.Property(p => p.Hint).IsRequired().HasMaxLength(765);

        });

        modelBuilder.Entity<SecurityType>(entity =>
        {
            entity.ToTable("Settings.SecurityTypes");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.TradeEngineId).IsRequired();
            entity.Property(p => p.TradeEngineName).IsRequired().HasMaxLength(45);
            entity.Property(p => p.TradeEngineTitle).IsRequired().HasMaxLength(765);
            entity.Property(p => p.SecurityTypeName).IsRequired().HasMaxLength(93);
            entity.Property(p => p.SecurityTypeTitle).IsRequired().HasMaxLength(765);
            entity.Property(p => p.SecurityGroupName).IsRequired().HasMaxLength(93);
            entity.Property(p => p.StockType).IsRequired().HasMaxLength(3);
        });

        modelBuilder.Entity<SecurityGroup>(entity =>
        {
            entity.ToTable("Settings.SecurityGroups");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(93);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(765);
            entity.Property(p => p.IsHidden).IsRequired();
        });

        modelBuilder.Entity<SecurityCollection>(entity =>
        {
            entity.ToTable("Settings.SecurityCollections");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(96);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(765);
            entity.Property(p => p.SecurityGroupId).IsRequired();
        });
    }
}