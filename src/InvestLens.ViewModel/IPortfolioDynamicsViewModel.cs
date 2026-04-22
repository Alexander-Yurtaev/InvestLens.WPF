using OxyPlot;

namespace InvestLens.ViewModel
{
    public interface IPortfolioDynamicsViewModel
    {
        PlotModel? ChartModel { get; set; }
        IAsyncCommand LoadPeriod1M { get; set; }
        IAsyncCommand LoadPeriod1Y { get; set; }
        IAsyncCommand LoadPeriod3M { get; set; }
        IAsyncCommand LoadPeriod6M { get; set; }
        bool Period1M { get; set; }
        bool Period1Y { get; set; }
        bool Period3M { get; set; }
        bool Period6M { get; set; }
    }
}