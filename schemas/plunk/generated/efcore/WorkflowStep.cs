#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class WorkflowStep
{
    public string Id { get; set; } = null!;

    public WorkflowStepType Type { get; set; }

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string Config { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public virtual Template? Template { get; set; }

    public string? TemplateId { get; set; }

    public virtual ICollection<WorkflowTransition> OutgoingTransitions { get; set; } = new List<WorkflowTransition>();

    public virtual ICollection<WorkflowTransition> IncomingTransitions { get; set; } = new List<WorkflowTransition>();

    public virtual ICollection<WorkflowStepExecution> Executions { get; set; } = new List<WorkflowStepExecution>();

    public virtual ICollection<WorkflowExecution> CurrentWorkflowExecutions { get; set; } = new List<WorkflowExecution>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
