#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMShadowEvalAttempt
{
    public string Id { get; set; } = null!;

    public string JobId { get; set; } = null!;

    public string RequestId { get; set; } = null!;

    public string Outcome { get; set; } = null!;

    public string? RouterName { get; set; }

    public string? Tier { get; set; }

    public string? RealModel { get; set; }

    public string? ShadowModel { get; set; }

    public double? Confidence { get; set; }

    public double JudgeCost { get; set; } = 0.0;

    public double ShadowCost { get; set; } = 0.0;

    public double? RealCost { get; set; }

    public double RealClassifierCost { get; set; } = 0.0;

    public double ShadowClassifierCost { get; set; } = 0.0;

    public bool RealCacheHit { get; set; } = false;

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }
}
