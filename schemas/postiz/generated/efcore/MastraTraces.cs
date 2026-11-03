#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraTraces
{
    public string Id { get; set; } = null!;

    public string? ParentSpanId { get; set; }

    public string Name { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public int Kind { get; set; }

    public string? Attributes { get; set; }

    public string? Status { get; set; }

    public string? Events { get; set; }

    public string? Links { get; set; }

    public string? Other { get; set; }

    public long StartTime { get; set; }

    public long EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
