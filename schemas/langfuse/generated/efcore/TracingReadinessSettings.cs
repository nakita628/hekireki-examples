#nullable enable

using System;

namespace Langfuse.Models;

public partial class TracingReadinessSettings
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? UpdatedByUser { get; set; }

    public int? TraceIdleMs { get; set; }
}
