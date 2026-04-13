namespace InvestLens.Model.Entities;

public class User(string firstName, string lastName, string login, string password)
{
    public int Id { get; set; }

    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName;

    public string Login { get; set; } = login;

    public string Password { get; set; } = password;

    public List<Portfolio> Portfolios { get; set; } = [];
}