#nullable enable

namespace Teable.Models;

public partial class ComputedUpdateDeadLetterSeed
{
    public string TaskId { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public virtual ComputedUpdateDeadLetter Task { get; set; } = null!;
}
