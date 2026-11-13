#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerTeamTable
{
    public double Spend { get; set; } = 0.0;

    public DateTime? BudgetResetAt { get; set; }

    public string CustomerTeamId { get; set; } = null!;

    public string? Alias { get; set; }

    public string ParentOrgId { get; set; } = null!;

    public virtual LiteLLMCustomerOrganizationTable ParentOrg { get; set; } = null!;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public virtual ICollection<LiteLLMCustomerMemberTable> Members { get; set; } = new List<LiteLLMCustomerMemberTable>();

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
