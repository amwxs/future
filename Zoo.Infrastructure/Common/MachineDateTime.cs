using Zoo.Application.Core;

namespace Zoo.Infrastructure.Common;
internal class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
