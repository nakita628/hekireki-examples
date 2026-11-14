#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMCustomerMemberTable
{
    public double Spend { get; set; } = 0.0;

    public string CustomerMemberId { get; set; } = null!;

    public string? Alias { get; set; }

    public string ParentOrgId { get; set; } = null!;

    public virtual LiteLLMCustomerOrganizationTable ParentOrg { get; set; } = null!;

    public List<string>? ParentTeamIds { get; set; } = new List<string>();

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? Budget { get; set; }

    public string? BudgetOverrideId { get; set; }

    public virtual LiteLLMBudgetTable? BudgetOverride { get; set; }

    public DateTime? BudgetOverrideExpiresAt { get; set; }

    public virtual ICollection<LiteLLMCustomerBudgetIncreaseRequest> BudgetIncreaseRequests { get; set; } = new List<LiteLLMCustomerBudgetIncreaseRequest>();

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
