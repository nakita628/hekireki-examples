#nullable enable

using System;

namespace Trigger.Models;

public partial class FeatureFlag
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
