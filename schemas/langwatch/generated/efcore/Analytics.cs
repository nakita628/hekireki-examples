#nullable enable

using System;

namespace LangWatch.Models;

public partial class Analytics
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? Value { get; set; }

    public double? NumericValue { get; set; }

    public string? StringValue { get; set; }

    public bool? BoolValue { get; set; }

    public DateTime CreatedAt { get; set; }
}
