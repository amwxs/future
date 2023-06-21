namespace Zoo.Domain.Core.Abstractions;
public abstract class AbsFullEntity : IEntity, IAuditableEntity, ISoftDeletableEntity
{
    public int Id { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public bool Deleted { get; set; }
}
