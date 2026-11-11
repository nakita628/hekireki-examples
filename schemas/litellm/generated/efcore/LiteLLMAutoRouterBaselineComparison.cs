#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAutoRouterBaselineComparison
{
    public string Scope { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string RouterName { get; set; } = null!;

    public bool InitialEquivalent { get; set; }

    public long Revision { get; set; } = 0L;

    public long PublishedRevision { get; set; } = 0L;

    public string? History { get; set; }

    public DateTime? AttemptedAt { get; set; }

    public bool Retired { get; set; } = false;

    public DateTime UpdatedAt { get; set; }
}
