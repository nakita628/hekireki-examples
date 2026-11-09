#nullable enable

using System;

namespace Jitsu.Models;

public partial class GlobalProps
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Value { get; set; }
}
