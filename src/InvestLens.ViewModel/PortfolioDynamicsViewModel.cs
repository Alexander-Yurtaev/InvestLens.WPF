using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class PortfolioDynamicsViewModel : BindableBase, IPortfolioDynamicsService
{
    private readonly IMetricsService _metricsService;
    private bool _period1M;
    private bool _period3M;
    private bool _period6M;
    private bool _period1Y;
    
    public PortfolioDynamicsViewModel(IMetricsService metricsService)
    {
        _metricsService = metricsService;
        Period1M = true;
    }

    public PortfolioDynamicPeriod Period 
    {
        set
        {
            var dynamicMetrics = _metricsService.DynamicMetrics(value);
        }
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
                    Period = PortfolioDynamicPeriod.Period1M;
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
                    Period = PortfolioDynamicPeriod.Period3M;
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
                    Period = PortfolioDynamicPeriod.Period6M;
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
                    Period = PortfolioDynamicPeriod.Period1Y;
                }
            }
        }
    }
}