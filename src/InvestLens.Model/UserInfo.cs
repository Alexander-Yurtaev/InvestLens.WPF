namespace InvestLens.Model;

public class UserInfo
{
    public string UserAvatar { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string UserFullNameInShortFormat { get; private set; } = string.Empty;

    public void Load()
    {
        UserAvatar = "АЮ";
        UserName = "Александр";
        UserFullNameInShortFormat = "Александр Ю.";
    }
}