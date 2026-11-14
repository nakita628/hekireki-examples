#nullable enable

using System;

namespace LangWatch.Models;

public partial class TriggerSent
{
    public string Id { get; set; } = null!;

    public string TriggerId { get; set; } = null!;

    public string? TraceId { get; set; }

    public string? CustomGraphId { get; set; }

    public virtual CustomGraph? CustomGraph { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Trigger Trigger { get; set; } = null!;

    public DateTime? ResolvedAt { get; set; }

    public string? OpenIncidentKey { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
