#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAdaptiveRouterState
{
    public string RouterName { get; set; } = null!;

    public string RequestType { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public double Alpha { get; set; }

    public double Beta { get; set; }

    public int TotalSamples { get; set; } = 0;

    public DateTime LastUpdatedAt { get; set; }
}
