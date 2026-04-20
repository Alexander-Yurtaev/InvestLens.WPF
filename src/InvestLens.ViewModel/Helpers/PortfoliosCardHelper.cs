using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Helpers;

public static class PortfoliosCardHelper
{
    public static string PortfolioTypeToStringConverter(PortfolioType portfolioType)
    {
        switch (portfolioType)
        {
            case PortfolioType.Invest:
                return "Инвест";
            case PortfolioType.Complex:
                return "Составной";
            default:
                throw new ArgumentException($"Неизвестный тип портфеля {portfolioType}");
        }
    }

    public static string PortfolioTypeToForegroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Complex => "#FFC8102E",
            PortfolioType.Invest => "#FF2C8C6E",
            _ => "0xFFFF4500"
        };
    }

    public static string PortfolioTypeToBackgroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Complex => "#1AC8102E",
            PortfolioType.Invest => "#1A2C8C6E",
            _ => "0xFFFFA500"
        };
    }
}