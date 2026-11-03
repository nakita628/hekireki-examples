#nullable enable

using System;

namespace Calcom.Models;

public partial class TeamFeatures
{
    public virtual Team Team { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public string FeatureId { get; set; } = null!;

    public bool Enabled { get; set; }

    public DateTime AssignedAt { get; set; }

    public string AssignedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
