#nullable enable

namespace Teable.Models;

public partial class ComputedUpdateOutboxSeed
{
    public string Id { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public virtual ComputedUpdateOutbox Task { get; set; } = null!;
}
