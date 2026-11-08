#nullable enable

using System;

namespace Plunk.Models;

public partial class SegmentMembership
{
    public virtual Contact Contact { get; set; } = null!;

    public string ContactId { get; set; } = null!;

    public virtual Segment Segment { get; set; } = null!;

    public string SegmentId { get; set; } = null!;

    public DateTime EnteredAt { get; set; }

    public DateTime? ExitedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
