#nullable enable

using System;

namespace Plunk.Models;

public partial class WorkflowTransition
{
    public string Id { get; set; } = null!;

    public virtual WorkflowStep FromStep { get; set; } = null!;

    public string FromStepId { get; set; } = null!;

    public virtual WorkflowStep ToStep { get; set; } = null!;

    public string ToStepId { get; set; } = null!;

    public string? Condition { get; set; }

    public int Priority { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
