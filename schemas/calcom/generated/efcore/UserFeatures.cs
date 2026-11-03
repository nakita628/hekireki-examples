#nullable enable

using System;

namespace Calcom.Models;

public partial class UserFeatures
{
    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public string FeatureId { get; set; } = null!;

    public bool Enabled { get; set; }

    public DateTime AssignedAt { get; set; }

    public string AssignedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
