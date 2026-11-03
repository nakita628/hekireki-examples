#nullable enable

using System;

namespace Teable.Models;

public partial class ComputedTableActivity
{
    public string TableId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int CalculatingFieldCount { get; set; } = 0;

    public int QueuedFieldCount { get; set; } = 0;

    public long EstimatedComplexity { get; set; } = 0L;

    public string RecentCompletions { get; set; } = "[]";

    public long Generation { get; set; } = 0L;

    public DateTime UpdatedAt { get; set; }
}
