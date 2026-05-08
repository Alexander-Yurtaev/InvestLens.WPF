using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;

namespace InvestLens.DataAccess.Resolvers;

public class SecurityGroupIdResolver : IMemberValueResolver<Model.MoexApi.Responses.Security, SecurityModel, string, int?>
{
    private readonly IMoexService _moexService;

    public SecurityGroupIdResolver(IMoexService moexService)
    {
        _moexService = moexService;
    }

    public int? Resolve(Model.MoexApi.Responses.Security source, SecurityModel destination, string sourceMember, int? destMember, ResolutionContext context)
    {
        var group = _moexService.MoexDictionaries.SecurityGroups
            .FirstOrDefault(s => s.Name == sourceMember);
        return group?.Id;
    }
}