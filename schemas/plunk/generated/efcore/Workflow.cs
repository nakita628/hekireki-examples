#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Workflow
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Enabled { get; set; } = false;

    public WorkflowTriggerType TriggerType { get; set; }

    public string? TriggerConfig { get; set; }

    public bool AllowReentry { get; set; } = false;

    public int? ThrottleWindowSeconds { get; set; }

    public string? ThrottleKey { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();

    public virtual ICollection<WorkflowExecution> Executions { get; set; } = new List<WorkflowExecution>();

    public virtual ICollection<ThrottleClaim> ThrottleClaims { get; set; } = new List<ThrottleClaim>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
