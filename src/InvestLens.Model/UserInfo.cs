using InvestLens.Model.Crud.User;

namespace InvestLens.Model;

public class UserInfo
{
    public UserInfo(UserModel model)
    {
        Id = model.Id;
        UserAvatar = $"{model.FirstName[0]}{model.LastName[0]}";
        UserName = model.FirstName;
        UserFullNameInShortFormat = $"{model.FirstName} {model.LastName[0]}";
    }

    public int Id { get; init; }
    public string UserAvatar { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string UserFullNameInShortFormat { get; init; } = string.Empty;
}