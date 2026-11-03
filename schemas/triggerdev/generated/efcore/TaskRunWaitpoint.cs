#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskRunWaitpoint
{
    public string Id { get; set; } = null!;

    public virtual TaskRun TaskRun { get; set; } = null!;

    public string TaskRunId { get; set; } = null!;

    public virtual Waitpoint Waitpoint { get; set; } = null!;

    public string WaitpointId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? SpanIdToComplete { get; set; }

    public string? BatchId { get; set; }

    public virtual BatchTaskRun? Batch { get; set; }

    public int? BatchIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
