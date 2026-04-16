using Autofac;
using InvestLens.ViewModel.Dialogs;
using InvestLens.ViewModel.Services;
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
            return confirmable.IsConfermed;
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

    private Window GetWindow(Type viewModelType, object? viewModel = null)
    {
        if (viewModel is not null)
        {
            _windows.Remove(viewModelType);
        }

        if (!_windows.TryGetValue(viewModelType, out var window))
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

            var viewType = Type.GetType(viewFullName) ?? throw new TypeLoadException($"View {viewFullName} not found");
            window = (Window)_lifetimeScope.Resolve(viewType);
            _windows[viewModelType] = window;
        }

        if (viewModel is not null)
        {
            window.DataContext = viewModel;
        }

        return window;
    }
}