using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public interface IPortfoliosManager
{
    List<PortfolioInfo> PortfolioInfos { get; set; }
    List<MenuNode> GetPortfoliosMenuItems();
}