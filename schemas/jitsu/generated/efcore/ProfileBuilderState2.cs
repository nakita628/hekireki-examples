#nullable enable

using System;

namespace Jitsu.Models;

public partial class ProfileBuilderState2
{
    public string ProfileBuilderId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? FullRebuildInfo { get; set; }

    public string? QueuesInfo { get; set; }

    public string? Metrics { get; set; }
}
