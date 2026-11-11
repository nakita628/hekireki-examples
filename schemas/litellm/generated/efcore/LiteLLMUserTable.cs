#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMUserTable
{
    public virtual ICollection<LiteLLMVerifiedSubject> VerifiedSubjects { get; set; } = new List<LiteLLMVerifiedSubject>();

    public string UserId { get; set; } = null!;

    public string? UserAlias { get; set; }

    public string? TeamId { get; set; }

    public string? SsoUserId { get; set; }

    public string? OrganizationId { get; set; }

    public string? ObjectPermissionId { get; set; }

    public string? Password { get; set; }

    public bool? PasswordResetRequired { get; set; }

    public DateTime? LastBreachCheckAt { get; set; }

    public List<string>? Teams { get; set; } = new List<string>();

    public string? UserRole { get; set; }

    public double? MaxBudget { get; set; }

    public double Spend { get; set; } = 0.0;

    public string? UserEmail { get; set; }

    public List<string>? Models { get; set; }

    public string Metadata { get; set; } = "{}";

    public int? MaxParallelRequests { get; set; }

    public long? TpmLimit { get; set; }

    public long? RpmLimit { get; set; }

    public string? BudgetDuration { get; set; }

    public DateTime? BudgetResetAt { get; set; }

    public List<string>? AllowedCacheControls { get; set; } = new List<string>();

    public List<string>? Policies { get; set; } = new List<string>();

    public string ModelSpend { get; set; } = "{}";

    public string ModelMaxBudget { get; set; } = "{}";

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual LiteLLMOrganizationTable? LitellmOrganizationTable { get; set; }

    public virtual ICollection<LiteLLMOrganizationMembership> OrganizationMemberships { get; set; } = new List<LiteLLMOrganizationMembership>();

    public virtual ICollection<LiteLLMInvitationLink> InvitationsCreated { get; set; } = new List<LiteLLMInvitationLink>();

    public virtual ICollection<LiteLLMInvitationLink> InvitationsUpdated { get; set; } = new List<LiteLLMInvitationLink>();

    public virtual ICollection<LiteLLMInvitationLink> InvitationsUser { get; set; } = new List<LiteLLMInvitationLink>();

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }
}
