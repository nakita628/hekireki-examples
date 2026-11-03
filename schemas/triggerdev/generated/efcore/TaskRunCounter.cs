#nullable enable

namespace Trigger.Models;

public partial class TaskRunCounter
{
    public string TaskIdentifier { get; set; } = null!;

    public int LastNumber { get; set; } = 0;
}
