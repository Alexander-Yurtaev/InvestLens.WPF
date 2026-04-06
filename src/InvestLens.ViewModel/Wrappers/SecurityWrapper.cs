using InvestLens.Model;

namespace InvestLens.ViewModel.Wrappers;

public class SecurityWrapper
{
    private readonly Security _model;

    public SecurityWrapper(Security model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public string SecType => _model.SecType;
}