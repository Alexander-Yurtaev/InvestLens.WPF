using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.MoexApi.Settings;

namespace InvestLens.ViewModel.Wrappers;

public class SecurityWrapper
{
    private readonly SecurityModel _model;

    public SecurityWrapper(SecurityModel model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public string SecTypeDisplay => GetSecTypeDisplay(_model.SecType);

    private string GetSecTypeDisplay(SecurityTypeModel? secType)
    {
        return secType?.SecurityTypeTitle ?? string.Empty;
    }
}