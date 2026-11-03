#nullable enable

using System;

namespace Teable.Models;

public partial class TableQueryObservationShard
{
    public string? SpaceId { get; set; }

    public string BaseId { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string QueryKind { get; set; } = null!;

    public string ShapeHash { get; set; } = null!;

    public DateTime WindowStart { get; set; }

    public string WriterId { get; set; } = null!;

    public int WindowSizeSeconds { get; set; }

    public int RequestCount { get; set; }

    public int SlowCount { get; set; }

    public int TimeoutCount { get; set; }

    public int DbErrorCount { get; set; }

    public double TotalDurationMs { get; set; }

    public double MaxDurationMs { get; set; }

    public double? TotalDbDurationMs { get; set; }

    public double? MaxDbDurationMs { get; set; }

    public string Shape { get; set; } = null!;

    public string? SqlDiagnostics { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }
}
