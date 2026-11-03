#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class WorkflowStep
{
    public string Id { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int StepOrder { get; set; }

    public WorkflowStepType StepType { get; set; } = WorkflowStepType.Router;

    public string Conditions { get; set; } = null!;

    public string Actions { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowStepLog> ExecutionLogs { get; set; } = new List<WorkflowStepLog>();
}
