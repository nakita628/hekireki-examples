#nullable enable

using System;

namespace Jitsu.Models;

public partial class SourceCheck
{
    public string Package { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Timestamp { get; set; }
}
