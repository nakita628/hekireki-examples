#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerMemberTable
{
    public double Spend { get; set; } = 0.0;

    public DateTime? BudgetResetAt { get; set; }

    public string CustomerMemberId { get; set; } = null!;

    public string? Alias { get; set; }

    public string ParentTeamId { get; set; } = null!;

    public virtual LiteLLMCustomerTeamTable ParentTeam { get; set; } = null!;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public string? BudgetTemplateId { get; set; }

    public virtual LiteLLMBudgetTable? BudgetTemplate { get; set; }

    public DateTime? BudgetOverrideExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
