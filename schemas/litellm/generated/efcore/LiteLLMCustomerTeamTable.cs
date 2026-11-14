#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerTeamTable
{
    public double Spend { get; set; } = 0.0;

    public string CustomerTeamId { get; set; } = null!;

    public string? Alias { get; set; }

    public string ParentOrgId { get; set; } = null!;

    public virtual LiteLLMCustomerOrganizationTable ParentOrg { get; set; } = null!;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public string? BudgetOverrideId { get; set; }

    public virtual LiteLLMBudgetTable? BudgetOverride { get; set; }

    public DateTime? BudgetOverrideExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
