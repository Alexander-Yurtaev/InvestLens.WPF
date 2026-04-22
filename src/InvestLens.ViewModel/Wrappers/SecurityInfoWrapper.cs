using InvestLens.Common.Helpers;
using InvestLens.Model;

namespace InvestLens.ViewModel.Wrappers;

public class SecurityInfoWrapper
{
    private readonly SecurityInfo _model;

    public SecurityInfoWrapper(SecurityInfo model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public decimal Count => _model.Quantity;
    public decimal AveragePrice => _model.AveragePrice;
    public decimal CurrentPrice => _model.CurrentPrice;
    public decimal TotalPrice => _model.TotalPrice;
    public decimal DividendCount => _model.Dividends;
    public decimal Profit => _model.Profit;
}