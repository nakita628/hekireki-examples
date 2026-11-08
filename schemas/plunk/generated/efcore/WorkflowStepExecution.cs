#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class WorkflowStepExecution
{
    public string Id { get; set; } = null!;

    public virtual WorkflowExecution Execution { get; set; } = null!;

    public string ExecutionId { get; set; } = null!;

    public virtual WorkflowStep Step { get; set; } = null!;

    public string StepId { get; set; } = null!;

    public StepExecutionStatus Status { get; set; } = StepExecutionStatus.Pending;

    public DateTime? ScheduledFor { get; set; }

    public DateTime? ExecuteAfter { get; set; }

    public string? Output { get; set; }

    public string? Error { get; set; }

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
