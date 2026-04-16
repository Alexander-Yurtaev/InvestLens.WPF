namespace InvestLens.Model.Entities;

public class User : BaseEntity
{
    public User(string firstName, string lastName, string login, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Login = login;
        Password = password;
    }

    public User(int id, string firstName, string lastName, string login, string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Login = login;
        Password = password;
    }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public List<Portfolio> Portfolios { get; set; } = [];
}