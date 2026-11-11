#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMOrganizationTable
{
    public string OrganizationId { get; set; } = null!;

    public string OrganizationAlias { get; set; } = null!;

    public string BudgetId { get; set; } = null!;

    public string Metadata { get; set; } = "{}";

    public List<string>? Models { get; set; }

    public double Spend { get; set; } = 0.0;

    public string ModelSpend { get; set; } = "{}";

    public string? ObjectPermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public virtual ICollection<LiteLLMTeamTable> Teams { get; set; } = new List<LiteLLMTeamTable>();

    public virtual ICollection<LiteLLMUserTable> Users { get; set; } = new List<LiteLLMUserTable>();

    public virtual ICollection<LiteLLMVerificationToken> Keys { get; set; } = new List<LiteLLMVerificationToken>();

    public virtual ICollection<LiteLLMOrganizationMembership> Members { get; set; } = new List<LiteLLMOrganizationMembership>();

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }
}
