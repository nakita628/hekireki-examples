#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class WorkflowExecution
{
    public string Id { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string? VisitorEmail { get; set; }

    public string? VisitorIp { get; set; }

    public ExecutionStatus Status { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Result { get; set; }

    public string? Metadata { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowStepLog> StepLogs { get; set; } = new List<WorkflowStepLog>();
}
