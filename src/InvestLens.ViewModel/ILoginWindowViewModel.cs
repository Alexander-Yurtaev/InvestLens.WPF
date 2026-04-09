using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface ILoginWindowViewModel
{
    string Email { get; set; }
    string Password { get; set; }
    ICommand LoginCommand { get; }
    ICommand RegistrationCommand { get; }
    string ErrorMessage { get; set; }
    bool ShowErrorMessage { get; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}