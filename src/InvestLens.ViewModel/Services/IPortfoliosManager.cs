using InvestLens.Model;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<PortfolioInfo> PortfolioInfos { get; set; }
    List<MenuNode> GetPortfoliosMenuItems();
    PortfolioDetail GetPortfolio(string title);
}