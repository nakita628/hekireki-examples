#nullable enable

namespace Postiz.Models;

public partial class MastraScheduleTriggers
{
    public string Id { get; set; } = null!;

    public string ScheduleId { get; set; } = null!;

    public string? RunId { get; set; }

    public long ScheduledFireAt { get; set; }

    public long ActualFireAt { get; set; }

    public string Outcome { get; set; } = null!;

    public string? Error { get; set; }

    public string TriggerKind { get; set; } = null!;

    public string? ParentTriggerId { get; set; }

    public string? Metadata { get; set; }
}
