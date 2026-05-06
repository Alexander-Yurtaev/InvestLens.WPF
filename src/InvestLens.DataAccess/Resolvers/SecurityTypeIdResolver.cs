using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;

namespace InvestLens.DataAccess.Resolvers;

public class SecurityTypeIdResolver : IMemberValueResolver<Model.MoexApi.Responses.Security, SecurityModel, string, int?>
{
    private readonly IMoexService _moexService;

    public SecurityTypeIdResolver(IMoexService moexService)
    {
        _moexService = moexService;
    }

    public int? Resolve(Model.MoexApi.Responses.Security source, SecurityModel destination, string sourceMember, int? destMember, ResolutionContext context)
    {
        var type = _moexService.MoexDictionaries.SecurityTypes
            .FirstOrDefault(s => s.SecurityTypeName == sourceMember);
        return type?.Id;
    }
}
