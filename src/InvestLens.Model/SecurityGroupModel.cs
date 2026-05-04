namespace InvestLens.Model;

public class SecurityGroupModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsHidden { get; set; }
}