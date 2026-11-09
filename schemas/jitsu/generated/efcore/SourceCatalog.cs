#nullable enable

using System;

namespace Jitsu.Models;

public partial class SourceCatalog
{
    public string Package { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? Catalog { get; set; }

    public DateTime Timestamp { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }
}
