using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;

namespace InvestLens.ViewModel;

public class PortfolioDynamicsViewModel : BindableBase, IPortfolioDynamicsViewModel
{
    private readonly IMetricsService _metricsService;
    private readonly IWindowManager _windowManager;
    private bool _period1M;
    private bool _period3M;
    private bool _period6M;
    private bool _period1Y;
    private PlotModel? _chartModel;
    private PortfolioDynamicPeriod? _currentPeriod;
    private CancellationTokenSource? _currentCts;

    public PortfolioDynamicsViewModel(IMetricsService metricsService, IWindowManager windowManager)
    {
        _metricsService = metricsService;
        _windowManager = windowManager;

        LoadPeriod1M = new AsyncDelegateCommand(OnLoadPeriod1M);
        LoadPeriod3M = new AsyncDelegateCommand(OnLoadPeriod3M);
        LoadPeriod6M = new AsyncDelegateCommand(OnLoadPeriod6M);
        LoadPeriod1Y = new AsyncDelegateCommand(OnLoadPeriod1Y);

        _ = LoadPeriod1M.ExecuteAsync(null);
        Period1M = true;
    }

    public PlotModel? ChartModel
    {
        get => _chartModel;
        set => SetProperty(ref _chartModel, value);
    }

    public bool Period1M
    {
        get => _period1M;
        set => SetProperty(ref _period1M, value);
    }

    public bool Period3M
    {
        get => _period3M;
        set => SetProperty(ref _period3M, value);
    }

    public bool Period6M
    {
        get => _period6M;
        set => SetProperty(ref _period6M, value);
    }

    public bool Period1Y
    {
        get => _period1Y;
        set => SetProperty(ref _period1Y, value);
    }

    public IAsyncCommand LoadPeriod1M { get; set; }
    public IAsyncCommand LoadPeriod3M { get; set; }
    public IAsyncCommand LoadPeriod6M { get; set; }
    public IAsyncCommand LoadPeriod1Y { get; set; }

    private async Task OnLoadPeriod1M()
    {
        if (_currentPeriod == PortfolioDynamicPeriod.Period1M) return;
        await InitChartModel(PortfolioDynamicPeriod.Period1M);
    }

    private async Task OnLoadPeriod3M()
    {
        if (_currentPeriod == PortfolioDynamicPeriod.Period3M) return;
        await InitChartModel(PortfolioDynamicPeriod.Period3M);
    }

    private async Task OnLoadPeriod6M()
    {
        if (_currentPeriod == PortfolioDynamicPeriod.Period6M) return;
        await InitChartModel(PortfolioDynamicPeriod.Period6M);
    }

    private async Task OnLoadPeriod1Y()
    {
        if (_currentPeriod == PortfolioDynamicPeriod.Period1Y) return;
        await InitChartModel(PortfolioDynamicPeriod.Period1Y);
    }

    private async Task InitChartModel(PortfolioDynamicPeriod period)
    {
        _currentCts?.Cancel();
        _currentCts = new CancellationTokenSource();
        var ct = _currentCts.Token;

        try
        {
            var plotModel = new PlotModel();

            InitAxises(plotModel, period);
            await InitSeries(plotModel, period, ct);

            if(!ct.IsCancellationRequested)
            {
                ChartModel = plotModel;
                _currentPeriod = period;
            }
        }
        catch(OperationCanceledException)
        {
            System.Diagnostics.Debug.WriteLine($"Загрузка периода {period} отменена");
        }
        catch (Exception ex)
        {
            var message = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

            _windowManager.ShowErrorDialog(message);
        }
    }

    private void InitAxises(PlotModel model, PortfolioDynamicPeriod period)
    {
        model.Title = "";

        // Настройка оси X для работы с датами
        var dateTimeAxis = new DateTimeAxis
        {
            Title = "Дата",
            Position = AxisPosition.Bottom,
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot
        };
        model.Axes.Add(dateTimeAxis);

        // Настройка оси Y для числовых значений
        var linearAxis = new LinearAxis
        {
            Title = "Стоимость, ₽",
            Position = AxisPosition.Left,
            MajorGridlineStyle = LineStyle.Solid,
        };
        model.Axes.Add(linearAxis);
    }

    private async Task InitSeries(
        PlotModel model, 
        PortfolioDynamicPeriod period, 
        CancellationToken ct)
    {
        var areaSeries = new AreaSeries
        {
            Title = "Инвестировано",
            Color = OxyColor.FromRgb(200, 16, 46),
            StrokeThickness = 2,
            Fill = OxyColor.FromArgb(77, 200, 16, 46),
            TrackerFormatString = "{0}\n📅 Дата: {2:dd.MM.yyyy}\n" +
                                  "💰 Стоимость: {4:N0} ₽"
        };

        var dynamicMetrics = await _metricsService.DynamicMetrics(period, ct);

        foreach (var metric in dynamicMetrics.OrderBy(m => m.Key))
        {
            var xValue = DateTimeAxis.ToDouble(metric.Key);
            areaSeries.Points.Add(new DataPoint(xValue, (double)decimal.Round(metric.Value, 2)));
        }

        model.Series.Clear();
        model.Series.Add(areaSeries);
    }
}