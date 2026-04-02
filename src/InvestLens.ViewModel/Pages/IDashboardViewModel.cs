using InvestLens.Model;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : INotifyPropertyChanged
{
    List<MetricCard> MetricCards { get; }
    List<ActivityItem> ActivityItems { get; }
    IContentHeaderViewModel ContentHeaderVm { get; }
}