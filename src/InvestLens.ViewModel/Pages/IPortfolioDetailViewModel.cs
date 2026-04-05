using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
    string Title { get; }
    List<PortfolioStatsWrapper> PortfolioStats { get; }
    List<SecurityInfoWrapper> Securities { get; }
    List<SecurityOperation> Operations { get; }
}