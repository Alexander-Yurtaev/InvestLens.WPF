using InvestLens.Model.Entities;

namespace InvestLens.Model;

public class UserInfo
{
    public UserInfo(User user)
    {
        Id = user.Id;
        UserAvatar = $"{user.FirstName[0]}{user.LastName[0]}";
        UserName = user.FirstName;
        UserFullNameInShortFormat = $"{user.FirstName} {user.LastName[0]}";
    }

    public int Id { get; init; }
    public string UserAvatar { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string UserFullNameInShortFormat { get; init; } = string.Empty;
}