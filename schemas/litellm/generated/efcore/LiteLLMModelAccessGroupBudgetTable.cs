#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMModelAccessGroupBudgetTable
{
    public string AccessGroupName { get; set; } = null!;

    public double Spend { get; set; } = 0.0;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
