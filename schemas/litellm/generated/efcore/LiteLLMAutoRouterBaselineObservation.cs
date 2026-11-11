#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMAutoRouterBaselineObservation
{
    public string RequestId { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public double StartedAt { get; set; }

    public long Revision { get; set; }

    public string Data { get; set; } = null!;

    public string? Publication { get; set; }

    public bool Conflicted { get; set; } = false;
}
