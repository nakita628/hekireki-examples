#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Segment
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public SegmentType Type { get; set; } = SegmentType.Dynamic;

    public string? Condition { get; set; }

    public bool TrackMembership { get; set; } = false;

    public int MemberCount { get; set; } = 0;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<SegmentMembership> Memberships { get; set; } = new List<SegmentMembership>();

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
