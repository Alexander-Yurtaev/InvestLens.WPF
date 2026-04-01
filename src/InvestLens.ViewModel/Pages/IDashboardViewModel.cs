using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : INotifyPropertyChanged
{
    string WelcomeTitle { get; }
    string WelcomeDescription { get; }
}