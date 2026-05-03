using InvestLens.DataAccess;
using InvestLens.DataAccess.Services;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows;

public class SplashInitializationWindowViewModel : BindableBase, ISplashInitializationWindowViewModel
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IWindowManager _windowManager;
    private bool _showCancelButton;
    private CancellationTokenSource _cancellationTokenSource;
    private float _progressValue;
    private List<InitStepWrapper> _parentInitSteps = [];

    public SplashInitializationWindowViewModel(
        IServiceProvider serviceProvider,
        IWindowManager windowManager)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _serviceProvider = serviceProvider;
        _windowManager = windowManager;
        CloseCommand = new DelegateCommand(CloseWindow);
        CancelCommand = new DelegateCommand(OnCancel);
        XbuttonCommand = CancelCommand;

        InitSteps.CollectionChanged += InitSteps_CollectionChanged;
    }

    public string Header => "Инициализация";

    public float ProgressMax => InitSteps.Count();

    public float ProgressValue
    {
        get => _progressValue;
        set => SetProperty(ref _progressValue, value);
    }

    public bool ShowCancelButton
    {
        get => _showCancelButton;
        set => SetProperty(ref _showCancelButton, value);
    }

    public ObservableCollection<InitStepWrapper> InitSteps { get; set; } = [];

    public ICommand CloseCommand { get; private set; }
    public ICommand CancelCommand { get; private set; }
    public ICommand XbuttonCommand { get; private set; }

    protected void CloseWindow()
    {
        _windowManager.CloseWindow<SplashInitializationWindowViewModel>();
    }

    public async Task Load(bool? force = false)
    {
        var ct = _cancellationTokenSource.Token;

        ShowCancelButton = true;
        PrepareAndStartInitSteps(ct);
        
        if (ct.IsCancellationRequested)
        {
            _windowManager.CloseWindow<SplashInitializationWindowViewModel>();
        }

        foreach (var step in _parentInitSteps)
        {
            _ = step.ExecuteAsync();
        }

        await Task.CompletedTask;
    }

    private void PrepareAndStartInitSteps(CancellationToken ct)
    {
        var migrationInitStep = new InitStepWrapper(
            "Проверка и обновление базы данных",
            "Накатывание миграций Entity Framework",
            new AsyncDelegateCommand(async () =>
            {
                await ApplyMigrations(ct);
            }),
            ct);
        
        var moexInitStep = new InitStepWrapper("Загрузка данных MOEX",
            "Глобальные справочники",
            new AsyncDelegateCommand(async () =>
            {
                var provider = _serviceProvider.GetRequiredService<IMoexProvider>();
                await provider.LoadMoexIndex(ct);
            }),
            ct);
        migrationInitStep.AddNextStep(moexInitStep);

        AddInitStep(migrationInitStep);
        AddInitStep(moexInitStep);

        _parentInitSteps.Add(migrationInitStep);
    }

    private void AddInitStep(InitStepWrapper step)
    {
        InitSteps.Add(step);
        step.PropertyChanged += Step_PropertyChanged;
    }

    private void LoadingCompleted()
    {
        _windowManager.SetMainWindow<LoginWindowViewModel>();
        _windowManager.ShowWindow<LoginWindowViewModel>();
        _windowManager.CloseWindow<SplashInitializationWindowViewModel>();
    }

    private void OnCancel()
    {
        _cancellationTokenSource.Cancel();
        foreach (var step in InitSteps)
        {
            step.CalcelIsWait();
        }
    }

    private async Task ApplyMigrations(CancellationToken ct)
    {
        var context = _serviceProvider.GetRequiredService<InvestLensDataContext>();
        await context.Database.MigrateAsync(ct);
    }

    private void InitSteps_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RaisePropertyChanged(nameof(ProgressMax));
    }

    private void Step_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(InitStepWrapper.State)) return;
        if (sender is not InitStepWrapper step) return;
        if (step.State == InitStepState.Wait || step.State == InitStepState.Run) return;

        ProgressValue++;

        if (ProgressValue < ProgressMax) return;

        if (InitSteps.All(s => s.State == InitStepState.CompletedSuccessfully))
        {
            LoadingCompleted();
        }
        else
        {
            ShowCancelButton = false;
            XbuttonCommand = CloseCommand;
            RaisePropertyChanged(nameof(XbuttonCommand));
        }
    }
}