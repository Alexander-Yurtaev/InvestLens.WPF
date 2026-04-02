using InvestLens.Model;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : INotifyPropertyChanged
{
    string WelcomeTitle { get; }
    string WelcomeDescription { get; }
    List<MetricCard> MetricCards { get; }
    List<ActivityItem> ActivityItems { get; }
}