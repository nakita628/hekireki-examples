#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyGuardrailUsageUnits
{
    public string GuardrailId { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string UsageUnit { get; set; } = null!;

    public long Units { get; set; } = 0L;

    public double? Cost { get; set; }

    public long UntrackedUnits { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
