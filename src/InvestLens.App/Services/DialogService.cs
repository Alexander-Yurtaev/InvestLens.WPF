using Autofac;
using System.Windows;
using IDialogService = InvestLens.ViewModel.Services.IDialogService;

namespace InvestLens.App.Services;

public class DialogService : IDialogService
{
    private readonly ILifetimeScope _lifetimeScope;
    private Window? _currentDialog;

    public DialogService(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public bool? ShowDialog(Type viewModelType)
    {
        _currentDialog = GetWindow(viewModelType);
        var result = _currentDialog.ShowDialog();
        _currentDialog = null;
        return result;
    }

    public void CloseDialog(bool? result)
    {
        if (_currentDialog is null) return;
        _currentDialog.DialogResult = result;
        _currentDialog.Close();
        _currentDialog = null;
    }

    private Window GetWindow(Type viewModelType)
    {
        var viewName = viewModelType.Name.Replace("ViewModel", "Window");
        var viewFullName = $"InvestLens.App.UserControls.{viewName}";
        var viewType = Type.GetType(viewFullName) ?? throw new TypeLoadException($"View {viewFullName} not found");
        return (Window)_lifetimeScope.Resolve(viewType);
    }
}