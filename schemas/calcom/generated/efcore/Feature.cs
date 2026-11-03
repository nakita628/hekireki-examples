#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Feature
{
    public string Slug { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public string? Description { get; set; }

    public FeatureType? Type { get; set; } = FeatureType.Release;

    public bool? Stale { get; set; } = false;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public int? UpdatedBy { get; set; }

    public virtual ICollection<UserFeatures> Users { get; set; } = new List<UserFeatures>();

    public virtual ICollection<TeamFeatures> Teams { get; set; } = new List<TeamFeatures>();
}
