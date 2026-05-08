using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.MoexApi.Settings;

namespace InvestLens.DataAccess.Resolvers;

public class MarketResolver : IMemberValueResolver<Model.Entities.Settings.Board, BoardModel, int, MarketModel?>
{
    private readonly IMoexService _moexService;

    public MarketResolver(IMoexService moexService)
    {
        _moexService = moexService;
    }

    public MarketModel? Resolve(Model.Entities.Settings.Board source, BoardModel destination, int marketId, MarketModel? destMember, ResolutionContext context)
    {
        return _moexService.MoexDictionaries.Markets
            .FirstOrDefault(s => s.Id == marketId);
    }
}