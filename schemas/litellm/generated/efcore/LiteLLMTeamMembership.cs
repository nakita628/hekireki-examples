#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMTeamMembership
{
    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public double Spend { get; set; } = 0.0;

    public double TotalSpend { get; set; } = 0.0;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }
}
