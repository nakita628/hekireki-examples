#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class SystemSettings
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string? Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
