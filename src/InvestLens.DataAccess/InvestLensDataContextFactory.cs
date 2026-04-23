using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InvestLens.DataAccess;

public class InvestLensDataContextFactory : IDesignTimeDbContextFactory<InvestLensDataContext>
{
    public InvestLensDataContext CreateDbContext(string[] args)
    {
        return new InvestLensDataContext(GetDbContextOptions());
    }

    private static DbContextOptions<InvestLensDataContext> GetDbContextOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InvestLensDataContext>();

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "InvestLens",
#if DEBUG
            "DEBUG",
#endif
            "database.db");

        var dbDir = Path.GetDirectoryName(dbPath);
        if (!Directory.Exists(dbDir))
        {
            Directory.CreateDirectory(dbDir!);
        }

        optionsBuilder.UseSqlite($"Data Source={dbPath}", options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            options.CommandTimeout(30);
        });

        return optionsBuilder.Options;
    }
}
