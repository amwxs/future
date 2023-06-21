namespace Zoo.Domain.Core.Abstractions;
public interface IAuditableEntity
{

    DateTime CreatedOnUtc { get; }

    DateTime? ModifiedOnUtc { get; }
}
