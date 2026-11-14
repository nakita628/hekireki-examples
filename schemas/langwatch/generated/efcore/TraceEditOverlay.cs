#nullable enable

using System;

namespace LangWatch.Models;

public partial class TraceEditOverlay
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string Patch { get; set; } = null!;

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
