using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;

namespace InvestLens.DataAccess.Resolvers;

public class SecurityGroupResolver : IMemberValueResolver<Model.MoexApi.Responses.Security, SecurityModel, string, SecurityGroupModel?>
{
    private readonly IMoexService _moexService;

    public SecurityGroupResolver(IMoexService moexService)
    {
        _moexService = moexService;
    }

    public SecurityGroupModel? Resolve(Model.MoexApi.Responses.Security source, SecurityModel destination, string sourceMember, SecurityGroupModel? destMember, ResolutionContext context)
    {
        return _moexService.MoexDictionaries.SecurityGroups
            .FirstOrDefault(s => s.Name == sourceMember);
    }
}