#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerOrganizationTable
{
    public double Spend { get; set; } = 0.0;

    public DateTime? BudgetResetAt { get; set; }

    public List<string>? BudgetIds { get; set; } = new List<string>();

    public string BudgetNames { get; set; } = "{}";

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public string CustomerOrgId { get; set; } = null!;

    public string? Alias { get; set; }

    public string? DefaultBudgetId { get; set; }

    public virtual LiteLLMBudgetTable? DefaultBudget { get; set; }

    public virtual ICollection<LiteLLMCustomerTeamTable> Teams { get; set; } = new List<LiteLLMCustomerTeamTable>();

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
