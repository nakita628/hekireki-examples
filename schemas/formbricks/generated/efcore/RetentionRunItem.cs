#nullable enable

namespace Formbricks.Models;

public partial class RetentionRunItem
{
    public string Id { get; set; } = null!;

    public virtual RetentionRun Run { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public RetentionTargetType TargetType { get; set; }

    public string TargetId { get; set; } = null!;

    public string? TargetName { get; set; }

    public RetentionRunItemAction Action { get; set; }

    public int Count { get; set; } = 1;

    public string? Recipient { get; set; }

    public RetentionSkipReason? SkipReason { get; set; }
}
