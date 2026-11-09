#nullable enable

using System;

namespace Jitsu.Models;

public partial class ProfileBuilderState
{
    public string ProfileBuilderId { get; set; } = null!;

    public int ProfileBuilderVersion { get; set; }

    public int InstanceIndex { get; set; }

    public int TotalInstances { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastTimestamp { get; set; }

    public int ProcessedUsers { get; set; }

    public int TotalUsers { get; set; }

    public int ErrorUsers { get; set; }

    public double Speed { get; set; }
}
