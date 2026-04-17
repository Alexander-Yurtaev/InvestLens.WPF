using AutoMapper;

namespace InvestLens.App.Profiles;

public class PortfolioProfiles : Profile
{
    public PortfolioProfiles()
    {
        CreateMap<InvestLens.Model.Entities.Portfolio, InvestLens.Model.Crud.Portfolio.PortfolioModel>();
        CreateMap< InvestLens.Model.Crud.Portfolio.CreateModel, InvestLens.Model.Entities.Portfolio>();
        CreateMap<InvestLens.Model.Crud.Portfolio.UpdateModel, InvestLens.Model.Entities.Portfolio>();
    }
}