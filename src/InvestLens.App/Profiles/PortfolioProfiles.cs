using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Entities;
using InvestLens.Model.Entities.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace InvestLens.App.Profiles;

public class PortfolioProfiles : Profile
{
    public PortfolioProfiles()
    {
        CreateMap<User, InvestLens.Model.Crud.User.UserModel>().ReverseMap();
        CreateMap< InvestLens.Model.Crud.User.RegistrationModel, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Portfolios, opt => opt.Ignore())
            ;

        CreateMap< InvestLens.Model.Entities.Settings.Engine, InvestLens.Model.EngineModel>().ReverseMap();

        CreateMap<InvestLens.Model.Entities.Portfolio, InvestLens.Model.Crud.Portfolio.PortfolioModel>()
            .ForMember(dest => dest.Portfolios, dest => dest.MapFrom(src => src.ChildrenPortfolios.Select(cp => cp.Id).ToList()));
        CreateMap<InvestLens.Model.Crud.Portfolio.PortfolioModel, InvestLens.Model.Entities.Portfolio>()
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap< InvestLens.Model.Crud.Portfolio.CreateModel, InvestLens.Model.Entities.Portfolio>()
            .ForMember(dest => dest.ParentPortfolioId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentPortfolio, opt => opt.Ignore())
            .ForMember(dest => dest.ChildrenPortfolios, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        CreateMap<InvestLens.Model.Crud.Portfolio.UpdateModel, InvestLens.Model.Entities.Portfolio>()
            .ForMember(dest => dest.ParentPortfolioId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentPortfolio, opt => opt.Ignore())
            .ForMember(dest => dest.ChildrenPortfolios, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        CreateMap<TransactionModel, InvestLens.Model.Entities.Transaction>()
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

        CreateMap<SecurityType, InvestLens.Model.SecurityTypeModel>().ReverseMap();
        CreateMap<SecurityGroup, InvestLens.Model.SecurityGroupModel>().ReverseMap();

        CreateMap<Model.MoexApi.Responses.Security, InvestLens.Model.SecurityModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsTraded, opt => opt.MapFrom(src => src.IsTraded == "1"))
            .ForMember(dest => dest.SecTypeId, opt => opt.MapFrom(src => GetSecurityTypeIdByName(src.SecType)))
            .ForMember(dest => dest.SecType, opt => opt.MapFrom(src => GetSecurityTypeByName(src.SecType)))
            .ForMember(dest => dest.SecGroupId, opt => opt.MapFrom(src => GetSecurityGroupIdByName(src.SecGroup)))
            .ForMember(dest => dest.SecGroup, opt => opt.MapFrom(src => GetSecurityGroupByName(src.SecGroup)));

        //CreateMap<Model.MoexApi.Responses.Security, InvestLens.Model.Entities.Security>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.IsTraded, opt => opt.MapFrom(src => src.IsTraded == "1"))
        //    .ForMember(dest => dest.SecTypeId, opt => opt.MapFrom(src => GetSecurityTypeIdByName(src.SecType)))
        //    .ForMember(dest => dest.SecType, opt => opt.Ignore())
        //    .ForMember(dest => dest.SecGroupId, opt => opt.MapFrom(src => GetSecurityGroupIdByName(src.SecType)))
        //    .ForMember(dest => dest.SecGroup, opt => opt.Ignore())
        //    .ForMember(dest => dest.IsLoaded, opt => opt.MapFrom(src => false));

        CreateMap<InvestLens.Model.Entities.Security, InvestLens.Model.SecurityModel>()
            .ForMember(dest => dest.SecType, opt => opt.Ignore())
            .ForMember(dest => dest.SecGroup, opt => opt.Ignore());

        CreateMap<InvestLens.Model.SecurityModel, InvestLens.Model.Entities.Security>()
            .ForMember(dest => dest.SecType, opt => opt.Ignore())
            .ForMember(dest => dest.SecGroup, opt => opt.Ignore())
            .ForMember(dest => dest.IsLoaded, opt => opt.MapFrom(src => false));
    }

    // SecurityType

    private SecurityType GetSecurityTypeByName(string secType)
    {
        var serviceProvider = ((InvestLens.App.App)App.Current).ServiceProvider;
        var securityTypeRepository = serviceProvider.GetRequiredService<ISecurityTypeRepository>();
        var securityTypes = securityTypeRepository.GetAll();
        
        return securityTypes.First(s => s.SecurityTypeName == secType);
    }

    private int? GetSecurityTypeIdByName(string secType)
    {
        return GetSecurityTypeByName(secType)?.Id;
    }

    private string GetSecurityTypeName(SecurityTypeModel? model) => model?.SecurityTypeName ?? string.Empty;

    // SecurityGroup

    private SecurityGroup GetSecurityGroupByName(string name)
    {
        var serviceProvider = ((InvestLens.App.App)App.Current).ServiceProvider;
        var securityGroupRepository = serviceProvider.GetRequiredService<ISecurityGroupRepository>();
        var securityGroups = securityGroupRepository.GetAll();

        return securityGroups.First(s => s.Name == name);
    }

    private int? GetSecurityGroupIdByName(string name)
    {
        return GetSecurityGroupByName(name)?.Id;
    }

    private string GetSecurityGroupName(SecurityGroupModel? model) => model?.Name ?? string.Empty;
}