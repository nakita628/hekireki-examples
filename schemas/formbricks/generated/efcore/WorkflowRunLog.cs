#nullable enable

using System;

namespace Formbricks.Models;

public partial class WorkflowRunLog
{
    public string Id { get; set; } = null!;

    public virtual WorkflowRun Run { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public int Sequence { get; set; }

    public string StepId { get; set; } = null!;

    public string StepType { get; set; } = null!;

    public WorkflowRunLogStatus Status { get; set; } = WorkflowRunLogStatus.Pending;

    public string Input { get; set; } = "{}";

    public string Output { get; set; } = "{}";

    public string? Error { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }
}
