#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyPolicyMetrics
{
    public string PolicyId { get; set; } = null!;

    public string Date { get; set; } = null!;

    public long RequestsEvaluated { get; set; } = 0L;

    public long PassedCount { get; set; } = 0L;

    public long BlockedCount { get; set; } = 0L;

    public long FlaggedCount { get; set; } = 0L;

    public double? AvgScore { get; set; }

    public double? AvgLatencyMs { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
