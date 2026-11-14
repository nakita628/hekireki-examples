#nullable enable

using System;

namespace LangWatch.Models;

public partial class FeatureFlag
{
    public string Key { get; set; } = null!;

    public bool Enabled { get; set; }

    public string? Rules { get; set; }

    public string? LastEditedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
