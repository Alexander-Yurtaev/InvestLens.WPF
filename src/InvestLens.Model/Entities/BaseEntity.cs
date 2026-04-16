namespace InvestLens.Model.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        
    }

    protected BaseEntity(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}
