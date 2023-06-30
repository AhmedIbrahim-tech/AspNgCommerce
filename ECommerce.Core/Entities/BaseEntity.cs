namespace ECommerce.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}