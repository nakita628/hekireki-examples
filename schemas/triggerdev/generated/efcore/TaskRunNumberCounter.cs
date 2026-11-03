#nullable enable

namespace Trigger.Models;

public partial class TaskRunNumberCounter
{
    public string Id { get; set; } = null!;

    public string TaskIdentifier { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public int LastNumber { get; set; } = 0;
}
