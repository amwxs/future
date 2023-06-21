using Zoo.Application.Core.Abstractions;

namespace Zoo.Infrastructure.Common;
internal class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
