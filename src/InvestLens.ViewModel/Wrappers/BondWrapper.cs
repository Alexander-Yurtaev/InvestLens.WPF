using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Wrappers;

public class BondWrapper
{
    private readonly Bond _model;

    public BondWrapper(Bond model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public string Issuer => _model.Issuer;
    public PeriodType PeriodType => _model.PeriodType;
}