#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Action
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public ActionType Type { get; set; }

    public string Config { get; set; } = null!;

    public virtual ICollection<Automation> Automations { get; set; } = new List<Automation>();

    public virtual ICollection<AutomationExecution> AutomationExecutions { get; set; } = new List<AutomationExecution>();
}
