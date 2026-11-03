#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Automation
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual Trigger Trigger { get; set; } = null!;

    public string TriggerId { get; set; } = null!;

    public virtual Action Action { get; set; } = null!;

    public string ActionId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<AutomationExecution> AutomationExecution { get; set; } = new List<AutomationExecution>();
}
