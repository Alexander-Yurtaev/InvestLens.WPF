namespace InvestLens.ViewModel.Services;

public class PortfolioDynamicsService : BindableBase, IPortfolioDynamicsService
{
    private bool _period1M;
    private bool _period3M;
    private bool _period6M;
    private bool _period1Y;

    public PortfolioDynamicsService()
    {
        Period1M = true;
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
}