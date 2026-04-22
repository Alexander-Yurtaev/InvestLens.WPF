namespace InvestLens.Model.Crud.User;

public class RegistrationModel : LoginModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}