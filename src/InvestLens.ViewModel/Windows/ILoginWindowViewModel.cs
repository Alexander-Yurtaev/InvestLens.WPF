using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows;

public interface ILoginWindowViewModel : IPasswordSupport
{
    string Login { get; set; }
    ICommand LoginCommand { get; }
    ICommand RegistrationCommand { get; }
    string ErrorMessage { get; set; }
    bool HasErrorMessage { get; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}