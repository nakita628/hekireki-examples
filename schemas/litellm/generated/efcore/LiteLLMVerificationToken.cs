#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMVerificationToken
{
    public string Token { get; set; } = null!;

    public string? KeyName { get; set; }

    public string? KeyAlias { get; set; }

    public bool SoftBudgetCooldown { get; set; } = false;

    public double Spend { get; set; } = 0.0;

    public double TotalSpend { get; set; } = 0.0;

    public DateTime? Expires { get; set; }

    public List<string>? Models { get; set; }

    public string Aliases { get; set; } = "{}";

    public string Config { get; set; } = "{}";

    public string? RouterSettings { get; set; } = "{}";

    public string? UserId { get; set; }

    public string? TeamId { get; set; }

    public string? AgentId { get; set; }

    public string? ProjectId { get; set; }

    public string Permissions { get; set; } = "{}";

    public int? MaxParallelRequests { get; set; }

    public string Metadata { get; set; } = "{}";

    public bool? Blocked { get; set; }

    public long? TpmLimit { get; set; }

    public long? RpmLimit { get; set; }

    public long? TpdLimit { get; set; }

    public double? MaxBudget { get; set; }

    public string? BudgetDuration { get; set; }

    public DateTime? BudgetResetAt { get; set; }

    public List<string>? AllowedCacheControls { get; set; } = new List<string>();

    public List<string>? AllowedRoutes { get; set; } = new List<string>();

    public string? KeyType { get; set; }

    public List<string>? Policies { get; set; } = new List<string>();

    public List<string>? AccessGroupIds { get; set; } = new List<string>();

    public string ModelSpend { get; set; } = "{}";

    public string ModelMaxBudget { get; set; } = "{}";

    public string BudgetFallbacks { get; set; } = "{}";

    public string? BudgetId { get; set; }

    public string? OrganizationId { get; set; }

    public string? ObjectPermissionId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? UpdatedBy { get; set; }

    public DateTime? SettingsUpdatedAt { get; set; }

    public DateTime? LastActive { get; set; }

    public int? RotationCount { get; set; } = 0;

    public bool? AutoRotate { get; set; } = false;

    public string? RotationInterval { get; set; }

    public DateTime? LastRotationAt { get; set; }

    public DateTime? KeyRotationAt { get; set; }

    public string? BudgetLimits { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public virtual LiteLLMOrganizationTable? LitellmOrganizationTable { get; set; }

    public virtual LiteLLMProjectTable? LitellmProjectTable { get; set; }

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }

    public virtual ICollection<LiteLLMJWTKeyMapping> JwtKeyMappings { get; set; } = new List<LiteLLMJWTKeyMapping>();
}
