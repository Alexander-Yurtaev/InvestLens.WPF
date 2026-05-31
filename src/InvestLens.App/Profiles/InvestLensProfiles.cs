using AutoMapper;
using InvestLens.Shared.DataAccess.Resolvers;
using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Crud.Transaction;
using InvestLens.Shared.Model.Entities;
using InvestLens.Shared.Model.MoexApi;
using InvestLens.Shared.Model.MoexApi.Settings;
using InvestLens.Shared.Models.Entities.Moex;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InvestLens.App.Profiles;

public class InvestLensProfiles : Profile
{
    public InvestLensProfiles()
    {
        CreateMap<User, InvestLens.Shared.Model.Crud.User.UserModel>().ReverseMap();
        CreateMap< InvestLens.Shared.Model.Crud.User.RegistrationModel, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Portfolios, opt => opt.Ignore())
            ;

        CreateMap< InvestLens.Shared.Model.Entities.Settings.Engine, EngineModel>().ReverseMap();
        CreateMap<InvestLens.Shared.Model.Entities.Settings.Market, MarketModel>().ReverseMap();
        CreateMap<InvestLens.Shared.Model.Entities.Settings.Duration, DurationModel>().ReverseMap();

        CreateMap<InvestLens.Shared.Model.Entities.Settings.Board, BoardModel>()
            .ForMember(dest => dest.Engine, opt => opt.MapFrom<EngineResolver, int>(src => src.EngineId))
            .ForMember(dest => dest.Market, opt => opt.MapFrom<MarketResolver, int>(src => src.MarketId));

        CreateMap<InvestLens.Shared.Model.Entities.Settings.BoardGroup, BoardGroupModel>().ReverseMap();
        CreateMap<InvestLens.Shared.Model.Entities.Settings.SecurityType, SecurityTypeModel>().ReverseMap();
        CreateMap<InvestLens.Shared.Model.Entities.Settings.SecurityGroup, SecurityGroupModel>().ReverseMap();
        CreateMap<InvestLens.Shared.Model.Entities.Settings.SecurityCollection, SecurityCollectionModel>().ReverseMap();

        CreateMap<History, HistoryModel>().ReverseMap();

        CreateMap<InvestLens.Shared.Model.Entities.Portfolio, InvestLens.Shared.Model.Entities.Portfolio>()
            .ForMember(dest => dest.ChildrenPortfolios, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        CreateMap<InvestLens.Shared.Model.Entities.Portfolio, InvestLens.Shared.Model.Crud.Portfolio.PortfolioModel>()
            .ForMember(dest => dest.Portfolios, dest => dest.MapFrom(src => src.ChildrenPortfolios.Select(cp => cp.Id).ToList()));
        CreateMap<InvestLens.Shared.Model.Crud.Portfolio.PortfolioModel, InvestLens.Shared.Model.Entities.Portfolio>()
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap< InvestLens.Shared.Model.Crud.Portfolio.CreateModel, InvestLens.Shared.Model.Entities.Portfolio>()
            .ForMember(dest => dest.ParentPortfolioId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentPortfolio, opt => opt.Ignore())
            .ForMember(dest => dest.ChildrenPortfolios, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        CreateMap<InvestLens.Shared.Model.Crud.Portfolio.UpdateModel, InvestLens.Shared.Model.Entities.Portfolio>()
            .ForMember(dest => dest.ParentPortfolioId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentPortfolio, opt => opt.Ignore())
            .ForMember(dest => dest.ChildrenPortfolios, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        CreateMap<TransactionModel, InvestLens.Shared.Model.Entities.Transaction>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<Transaction, SecurityOperation>()
            .ForMember(dest => dest.SecId, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.OperationType, opt => opt.MapFrom(src => src.Event))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity + src.FeeTax))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Note))
            ;

        CreateMap<Shared.Model.MoexApi.Responses.Security, InvestLens.Shared.Model.SecurityModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsTraded, opt => opt.MapFrom(src => src.IsTraded == "1"))
            .ForMember(dest => dest.SecTypeId, opt => opt.MapFrom<SecurityTypeIdResolver, string>(src => src.SecType))
            .ForMember(dest => dest.SecType, opt => opt.MapFrom<SecurityTypeResolver, string>(src => src.SecType))
            .ForMember(dest => dest.SecGroupId, opt => opt.MapFrom<SecurityGroupIdResolver, string>(src => src.SecGroup))
            .ForMember(dest => dest.SecGroup, opt => opt.MapFrom<SecurityGroupResolver, string>(src => src.SecGroup));

        //CreateMap<Model.MoexApi.Responses.Security, InvestLens.Model.Entities.Security>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.IsTraded, opt => opt.MapFrom(src => src.IsTraded == "1"))
        //    .ForMember(dest => dest.SecTypeId, opt => opt.MapFrom(src => GetSecurityTypeIdByName(src.SecType)))
        //    .ForMember(dest => dest.SecType, opt => opt.Ignore())
        //    .ForMember(dest => dest.SecGroupId, opt => opt.MapFrom(src => GetSecurityGroupIdByName(src.SecType)))
        //    .ForMember(dest => dest.SecGroup, opt => opt.Ignore())
        //    .ForMember(dest => dest.IsLoaded, opt => opt.MapFrom(src => false));

        CreateMap<InvestLens.Shared.Model.Entities.Security, InvestLens.Shared.Model.SecurityModel>()
            .ForMember(dest => dest.SecType, opt => opt.Ignore())
            .ForMember(dest => dest.SecGroup, opt => opt.Ignore());

        CreateMap<InvestLens.Shared.Model.SecurityModel, InvestLens.Shared.Model.Entities.Security>()
            .ForMember(dest => dest.SecType, opt => opt.Ignore())
            .ForMember(dest => dest.SecGroup, opt => opt.Ignore())
            .ForMember(dest => dest.IsLoaded, opt => opt.MapFrom(src => false));
    }
}