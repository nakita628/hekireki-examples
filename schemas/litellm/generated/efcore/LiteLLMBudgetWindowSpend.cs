#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMBudgetWindowSpend
{
    public string EntityType { get; set; } = null!;

    public string EntityId { get; set; } = null!;

    public string WindowDuration { get; set; } = null!;

    public DateTime WindowStart { get; set; }

    public double Spend { get; set; } = 0.0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
