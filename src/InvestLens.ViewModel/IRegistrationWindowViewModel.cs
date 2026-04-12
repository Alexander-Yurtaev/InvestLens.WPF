using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface IRegistrationWindowViewModel
{
    string Name { get; set; }
    string Surname { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    string ConfirmPassword { get; set; }
    ICommand RegisterCommand { get; }
    ICommand LoginCommand { get; }
    string ErrorMessage { get; set; }
    bool ShowErrorMessage { get; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}