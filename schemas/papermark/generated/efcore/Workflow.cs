#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Workflow
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string EntryLinkId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Link EntryLink { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();

    public virtual ICollection<WorkflowExecution> Executions { get; set; } = new List<WorkflowExecution>();
}
