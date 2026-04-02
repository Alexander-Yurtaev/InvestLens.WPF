using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    public PortfoliosManager()
    {
        PortfolioInfos = [];
        LoadPortfolioInfos();
    }

    public List<IPortfolioInfo> PortfolioInfos { get; set; }

    private void LoadPortfolioInfos()
    {
        var result = new List<IPortfolioInfo>
        {
            new PortfolioInfo("Составной инвестиционный", PortfolioType.Primary, "$84,320", "+15.2%", "12", "сегодня"),
            new PortfolioInfo("Портфель №1", PortfolioType.Dividend, "$24,150", "+8.4%", "8", "вчера"),
            new PortfolioInfo("Портфель №2", PortfolioType.Agressive, "$16,062", "+22.1%", "6", "2 дня назад"),
        };

        PortfolioInfos.Clear();
        PortfolioInfos.AddRange(result);
    }
}