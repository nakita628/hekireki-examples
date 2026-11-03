#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Trigger
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string EventSource { get; set; } = null!;

    public List<string>? EventActions { get; set; }

    public string? Filter { get; set; }

    public JobConfigState Status { get; set; } = JobConfigState.Active;

    public virtual ICollection<AutomationExecution> AutomationExecutions { get; set; } = new List<AutomationExecution>();

    public virtual ICollection<Automation> Automations { get; set; } = new List<Automation>();
}
