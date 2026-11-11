#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMTeamTable
{
    public string TeamId { get; set; } = null!;

    public string? TeamAlias { get; set; }

    public string? OrganizationId { get; set; }

    public string? ObjectPermissionId { get; set; }

    public List<string>? Admins { get; set; }

    public List<string>? Members { get; set; }

    public string MembersWithRoles { get; set; } = "{}";

    public string Metadata { get; set; } = "{}";

    public double? MaxBudget { get; set; }

    public double? SoftBudget { get; set; }

    public double Spend { get; set; } = 0.0;

    public List<string>? Models { get; set; }

    public int? MaxParallelRequests { get; set; }

    public long? TpmLimit { get; set; }

    public long? RpmLimit { get; set; }

    public long? TpdLimit { get; set; }

    public string? BudgetDuration { get; set; }

    public DateTime? BudgetResetAt { get; set; }

    public bool Blocked { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ModelSpend { get; set; } = "{}";

    public string ModelMaxBudget { get; set; } = "{}";

    public string? RouterSettings { get; set; } = "{}";

    public List<string>? TeamMemberPermissions { get; set; } = new List<string>();

    public List<string>? AccessGroupIds { get; set; } = new List<string>();

    public List<string>? Policies { get; set; } = new List<string>();

    public List<string>? DefaultTeamMemberModels { get; set; } = new List<string>();

    public string? BudgetLimits { get; set; }

    public int? ModelId { get; set; }

    public bool AllowTeamGuardrailConfig { get; set; } = false;

    public virtual LiteLLMOrganizationTable? LitellmOrganizationTable { get; set; }

    public virtual LiteLLMModelTable? LitellmModelTable { get; set; }

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }

    public virtual ICollection<LiteLLMProjectTable> Projects { get; set; } = new List<LiteLLMProjectTable>();
}
