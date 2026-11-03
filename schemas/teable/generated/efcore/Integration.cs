#nullable enable

using System;

namespace Teable.Models;

public partial class Integration
{
    public string Id { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool? Enable { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }
}
