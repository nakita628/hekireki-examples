#nullable enable

using System;

namespace LangWatch.Models;

public partial class PinnedTrace
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public PinSource Source { get; set; } = PinSource.Manual;

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }
}
