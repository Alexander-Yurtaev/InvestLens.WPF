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
    private bool _period1M;
    private bool _period3M;
    private bool _period6M;
    private bool _period1Y;
    private PlotModel? _chartModel;

    public PortfolioDynamicsViewModel(IMetricsService metricsService)
    {
        _metricsService = metricsService;
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
        set
        {
            if (SetProperty(ref _period1M, value))
            {
                if(value)
                {
                    InitChartModel(PortfolioDynamicPeriod.Period1M);
                }
            }
        }
    }

    public bool Period3M
    {
        get => _period3M;
        set
        {
            if (SetProperty(ref _period3M, value))
            {
                if(value)
                {
                    InitChartModel(PortfolioDynamicPeriod.Period3M);
                }
            }
        }
    }

    public bool Period6M
    {
        get => _period6M;
        set
        {
            if (SetProperty(ref _period6M, value))
            {
                if(value)
                {
                    InitChartModel(PortfolioDynamicPeriod.Period6M);
                }
            }
        }
    }

    public bool Period1Y
    {
        get => _period1Y;
        set
        {
            if (SetProperty(ref _period1Y, value))
            {
                if(value)
                {
                    InitChartModel(PortfolioDynamicPeriod.Period1Y);
                }
            }
        }
    }

    private async void InitChartModel(PortfolioDynamicPeriod period)
    {
        var plotModel = new PlotModel();

        InitAxises(plotModel, period);
        await InitSeries(plotModel, period);

        ChartModel = plotModel;
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

    private async Task InitSeries(PlotModel model, PortfolioDynamicPeriod period)
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

        var dynamicMetrics = await _metricsService.DynamicMetrics(period);

        foreach (var metric in dynamicMetrics.OrderBy(m => m.Key))
        {
            var xValue = DateTimeAxis.ToDouble(metric.Key);
            areaSeries.Points.Add(new DataPoint(xValue, (double)decimal.Round(metric.Value, 2)));
        }

        model.Series.Clear();
        model.Series.Add(areaSeries);
    }
}