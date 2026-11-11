#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyToolSpend
{
    public string Date { get; set; } = null!;

    public string ToolName { get; set; } = null!;

    public double Spend { get; set; } = 0.0;

    public long TotalTokens { get; set; } = 0L;

    public long RequestCount { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
