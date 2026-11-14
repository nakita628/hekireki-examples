#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerOrganizationTable
{
    public double Spend { get; set; } = 0.0;

    public List<string>? BudgetTemplateIds { get; set; } = new List<string>();

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public string CustomerOrgId { get; set; } = null!;

    public string? Alias { get; set; }

    public string? DefaultBudgetTemplateId { get; set; }

    public virtual LiteLLMBudgetTable? DefaultBudgetTemplate { get; set; }

    public virtual ICollection<LiteLLMCustomerTeamTable> Teams { get; set; } = new List<LiteLLMCustomerTeamTable>();

    public virtual ICollection<LiteLLMCustomerMemberTable> Members { get; set; } = new List<LiteLLMCustomerMemberTable>();

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
