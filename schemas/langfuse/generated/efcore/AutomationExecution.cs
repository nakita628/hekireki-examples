#nullable enable

using System;

namespace Langfuse.Models;

public partial class AutomationExecution
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string SourceId { get; set; } = null!;

    public string AutomationId { get; set; } = null!;

    public virtual Automation Automation { get; set; } = null!;

    public string TriggerId { get; set; } = null!;

    public virtual Trigger Trigger { get; set; } = null!;

    public string ActionId { get; set; } = null!;

    public virtual Action Action { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public ActionExecutionStatus Status { get; set; } = ActionExecutionStatus.Pending;

    public string Input { get; set; } = null!;

    public string? Output { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public string? Error { get; set; }
}
