namespace InvestLens.Model;

public class UserInfo
{
    public int Id { get; init; }
    public string UserAvatar { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string UserFullNameInShortFormat { get; init; } = string.Empty;
}