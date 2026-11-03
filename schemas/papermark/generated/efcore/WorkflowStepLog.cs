#nullable enable

using System;

namespace Papermark.Models;

public partial class WorkflowStepLog
{
    public string Id { get; set; } = null!;

    public string ExecutionId { get; set; } = null!;

    public string WorkflowStepId { get; set; } = null!;

    public bool ConditionsMatched { get; set; }

    public string? ConditionResults { get; set; }

    public string? ActionsExecuted { get; set; }

    public DateTime ExecutedAt { get; set; }

    public int? Duration { get; set; }

    public string? Error { get; set; }

    public virtual WorkflowExecution Execution { get; set; } = null!;

    public virtual WorkflowStep Step { get; set; } = null!;
}
