namespace InvestLens.ViewModel.Services;

public interface IPortfolioDynamicsViewModel
{
    bool Period1M { get; set; }
    bool Period3M { get; set; }
    bool Period6M { get; set; }
    bool Period1Y { get; set; }
}