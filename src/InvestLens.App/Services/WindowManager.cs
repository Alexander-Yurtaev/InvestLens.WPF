using Autofac;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows.Dialogs;
using System.Windows;

namespace InvestLens.App.Services;

public class WindowManager : IWindowManager
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly Dictionary<Type, Window> _windows = [];
    
    public WindowManager(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public void ShowErrorDialog(string message)
    {
        var viewModel = new ErrorDialogViewModel(this, message);
        ShowModalDialog(viewModel);
    }

    public void ShowWarningDialog(string message, string actionContext)
    {
        var viewModel = new WarningDialogViewModel(this, message, actionContext);
        ShowModalDialog(viewModel);
    }

    public void ShowInformationDialog(string message, string actionContext)
    {
        var viewModel = new InformationDialogViewModel(this, message);
        ShowModalDialog(viewModel);
    }

    public void ShowSuccessDialog(string message, string actionContext)
    {
        var viewModel = new SuccessDialogViewModel(this, message);
        ShowModalDialog(viewModel);
    }

    public bool? ShowConfirmDialog(string message, string actionContext)
    {
        var viewModel = new ConfirmDialogViewModel(this, message);
        var result = ShowModalDialog(viewModel);
        return result;
    }

    public void ShowWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class
    {
        var window = GetWindow(typeof(TViewModel), viewModel);
        window.Show();
    }

    public bool? ShowDialogWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class
    {
        var window = GetWindow(typeof(TViewModel), viewModel);
        window.Owner = Application.Current.MainWindow;
        var result = window.ShowDialog();
        if (window.DataContext is IConfirmable confirmable)
        {
            return confirmable.IsConfirmed;
        }
        return result;
    }

    public void CloseWindow<TViewModel>() where TViewModel : class
    {
        var viewModelType = typeof(TViewModel);
        if (_windows.TryGetValue(viewModelType, out var window))
        {
            window.Close();
            _windows.Remove(viewModelType);
        }
    }

    public void SetMainWindow<TViewModel>() where TViewModel : class
    {
        var window = GetWindow(typeof(TViewModel));
        Application.Current.MainWindow = window;
    }

    private bool? ShowModalDialog<TViewModel>(TViewModel viewModel)
    {
        var window = GetWindow(typeof(TViewModel), viewModel);
        window.Owner = Application.Current.MainWindow;
        var result = window.ShowDialog();
        if (window.DataContext is IConfirmable confirmable)
        {
            return confirmable.IsConfirmed;
        }
        return result;
    }

    private Window GetWindow(Type viewModelType, object? viewModel = null)
    {
        if (viewModel is not null)
        {
            _windows.Remove(viewModelType);
        }

        Window? window = null;
        if (viewModel is IViewableViewModel viewable)
        {
            var viewName = viewable.ViewName;
            var viewFullName = $"InvestLens.App.Windows.Dialogs.{viewName}";
            window = ResolveWindowByViewFullName(viewFullName);
        }
        else if (!_windows.TryGetValue(viewModelType, out window))
        {
            var viewName = viewModelType.Name.Substring(0, viewModelType.Name.LastIndexOf("ViewModel", StringComparison.InvariantCulture));
            string viewFullName;
            if (viewName.EndsWith("Window"))
            {
                viewFullName = $"InvestLens.App.Windows.{viewName}";
            }
            else if (viewName.EndsWith("Dialog"))
            {
                viewFullName = $"InvestLens.App.Windows.Dialogs.{viewName}";
            }
            else
            {
                viewName = $"{viewName}View";
                viewFullName = $"InvestLens.App.Views.{viewName}";
            }

            window = ResolveWindowByViewFullName(viewFullName);
            
        }

        if (window is null)
        {
            throw new Exception($"Ошибка при построении окна для {viewModelType}");
        }

        _windows[viewModelType] = window;
        if (viewModel is not null)
        {
            window.DataContext = viewModel;
        }

        return window;
    }

    private Window? ResolveWindowByViewFullName(string viewFullName)
    {
        var viewType = Type.GetType(viewFullName);
        if (viewType is null)
        {
            ShowErrorDialog($"View {viewFullName} not found");
            return null;
        }
        else
        {
            return (Window)_lifetimeScope.Resolve(viewType);
        }   
    }
}