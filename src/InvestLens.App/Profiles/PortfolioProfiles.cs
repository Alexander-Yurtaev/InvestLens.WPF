using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Entities;

namespace InvestLens.App.Profiles;

public class PortfolioProfiles : Profile
{
    public PortfolioProfiles()
    {
        CreateMap<User, InvestLens.Model.Crud.User.UserModel>().ReverseMap();
        CreateMap< InvestLens.Model.Crud.User.RegistrationModel, User>();

        CreateMap<InvestLens.Model.Entities.Portfolio, InvestLens.Model.Crud.Portfolio.PortfolioModel>().ReverseMap();
        CreateMap< InvestLens.Model.Crud.Portfolio.CreateModel, InvestLens.Model.Entities.Portfolio>();
        CreateMap<InvestLens.Model.Crud.Portfolio.UpdateModel, InvestLens.Model.Entities.Portfolio>();
        CreateMap<TransactionModel, InvestLens.Model.Entities.Transaction>().ReverseMap();
        CreateMap<Transaction, SecurityOperation>()
            .ForMember(dest => dest.SecId, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.OperationType, opt => opt.MapFrom(src => src.Event))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity + src.FeeTax))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Note))
            ;
    }
}