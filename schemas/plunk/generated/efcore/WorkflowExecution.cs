#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class WorkflowExecution
{
    public string Id { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public virtual Contact Contact { get; set; } = null!;

    public string ContactId { get; set; } = null!;

    public WorkflowExecutionStatus Status { get; set; } = WorkflowExecutionStatus.Running;

    public virtual WorkflowStep? CurrentStep { get; set; }

    public string? CurrentStepId { get; set; }

    public string? ExitReason { get; set; }

    public string? Context { get; set; }

    public virtual ICollection<WorkflowStepExecution> StepExecutions { get; set; } = new List<WorkflowStepExecution>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
