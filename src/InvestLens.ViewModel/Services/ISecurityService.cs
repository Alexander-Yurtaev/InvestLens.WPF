namespace InvestLens.ViewModel.Services;

public interface ISecurityService
{
    Task UpdateSecurities(List<string> secIdImportList);
}