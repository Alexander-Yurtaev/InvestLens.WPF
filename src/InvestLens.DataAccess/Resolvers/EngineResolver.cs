using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.MoexApi.Settings;

namespace InvestLens.DataAccess.Resolvers;

public class EngineResolver : IMemberValueResolver<Model.Entities.Settings.Board, BoardModel, int, EngineModel?>
{
    private readonly IMoexService _moexService;

    public EngineResolver(IMoexService moexService)
    {
        _moexService = moexService;
    }

    public EngineModel? Resolve(Model.Entities.Settings.Board source, BoardModel destination, int engineId, EngineModel? destMember, ResolutionContext context)
    {
        return _moexService.MoexDictionaries.Engines
            .FirstOrDefault(s => s.Id == engineId);
    }
}