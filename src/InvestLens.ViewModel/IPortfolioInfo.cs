using System.ComponentModel;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel;

public interface IPortfolioInfo
{
    string Title { get; set; }
    PortfolioType PortfolioType { get; set; }
    string Cost { get; set; }
    string Profitability { get; set; }
    string Amount { get; set; }
    string RefreshDate { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
}