#nullable enable

using System;

namespace Calcom.Models;

public partial class UserFilterSegmentPreference
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string TableIdentifier { get; set; } = null!;

    public int? SegmentId { get; set; }

    public string? SystemSegmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual FilterSegment? Segment { get; set; }
}
