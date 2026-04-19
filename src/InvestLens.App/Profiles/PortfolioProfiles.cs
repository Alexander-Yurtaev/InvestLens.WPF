using AutoMapper;
using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Entities;

namespace InvestLens.App.Profiles;

public class PortfolioProfiles : Profile
{
    public PortfolioProfiles()
    {
        CreateMap<InvestLens.Model.Entities.Portfolio, InvestLens.Model.Crud.Portfolio.PortfolioModel>().ReverseMap();
        CreateMap< InvestLens.Model.Crud.Portfolio.CreateModel, InvestLens.Model.Entities.Portfolio>();
        CreateMap<InvestLens.Model.Crud.Portfolio.UpdateModel, InvestLens.Model.Entities.Portfolio>();
        CreateMap<TransactionModel, InvestLens.Model.Entities.Transaction>().ReverseMap();
    }
}