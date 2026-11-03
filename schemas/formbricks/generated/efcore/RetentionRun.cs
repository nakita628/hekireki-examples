#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class RetentionRun
{
    public string Id { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public RetentionEntity Entity { get; set; }

    public RetentionRunKind Kind { get; set; } = RetentionRunKind.Sweep;

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public int NotifiedCount { get; set; } = 0;

    public int ArchivedCount { get; set; } = 0;

    public int DeletedCount { get; set; } = 0;

    public int SkippedCount { get; set; } = 0;

    public bool HasChanges { get; set; } = false;

    public string? ScanCursor { get; set; }

    public virtual ICollection<RetentionRunItem> Items { get; set; } = new List<RetentionRunItem>();
}
