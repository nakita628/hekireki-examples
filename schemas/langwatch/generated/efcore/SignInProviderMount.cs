#nullable enable

using System;

namespace LangWatch.Models;

public partial class SignInProviderMount
{
    public string Id { get; set; } = "self";

    public string? Provider { get; set; }

    public bool Mounted { get; set; }

    public string LastAppliedEventId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
