#nullable enable

using System;

namespace Jitsu.Models;

public partial class SourceSpec
{
    public string Package { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string? Specs { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Error { get; set; }
}
