#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PendingEmail { get; set; }

    public AuthenticationMethod AuthenticationMethod { get; set; }

    public string? AuthenticationProfile { get; set; }

    public string? AuthenticationExtraParams { get; set; }

    public string? AuthIdentifier { get; set; }

    public string? DisplayName { get; set; }

    public string? Name { get; set; }

    public string? AvatarUrl { get; set; }

    public bool Admin { get; set; } = false;

    public string? DashboardPreferences { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsOnCloudWaitlist { get; set; } = false;

    public bool FeatureCloud { get; set; } = false;

    public bool IsOnHostedRepoWaitlist { get; set; } = false;

    public bool MarketingEmails { get; set; } = true;

    public bool ConfirmedBasicDetails { get; set; } = false;

    public string? ReferralSource { get; set; }

    public string? OnboardingData { get; set; }

    public virtual ICollection<OrgMember> OrgMemberships { get; set; } = new List<OrgMember>();

    public virtual ICollection<OrgMemberInvite> SentInvites { get; set; } = new List<OrgMemberInvite>();

    public DateTime? MfaEnabledAt { get; set; }

    public virtual SecretReference? MfaSecretReference { get; set; }

    public string? MfaSecretReferenceId { get; set; }

    public string? MfaLastUsedCode { get; set; }

    public int SessionDuration { get; set; } = 31556952;

    public DateTime? NextSessionEnd { get; set; }

    public virtual InvitationCode? InvitationCode { get; set; }

    public string? InvitationCodeId { get; set; }

    public virtual ICollection<PersonalAccessToken> PersonalAccessTokens { get; set; } = new List<PersonalAccessToken>();

    public virtual ICollection<ApiKey> CreatedApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<WorkerDeployment> Deployments { get; set; } = new List<WorkerDeployment>();

    public virtual ICollection<MfaBackupCode> BackupCodes { get; set; } = new List<MfaBackupCode>();

    public virtual ICollection<BulkActionGroup> BulkActions { get; set; } = new List<BulkActionGroup>();

    public virtual ICollection<ImpersonationAuditLog> ImpersonationsPerformed { get; set; } = new List<ImpersonationAuditLog>();

    public virtual ICollection<ImpersonationAuditLog> ImpersonationsReceived { get; set; } = new List<ImpersonationAuditLog>();

    public virtual ICollection<SupportAccessRequest> SupportAccessRequestsMade { get; set; } = new List<SupportAccessRequest>();

    public virtual ICollection<SupportAccessRequest> SupportAccessRequestsApproved { get; set; } = new List<SupportAccessRequest>();

    public virtual ICollection<CustomerQuery> CustomerQueries { get; set; } = new List<CustomerQuery>();

    public virtual ICollection<MetricsDashboard> MetricsDashboards { get; set; } = new List<MetricsDashboard>();

    public virtual ICollection<PlatformNotification> PlatformNotifications { get; set; } = new List<PlatformNotification>();

    public virtual ICollection<PlatformNotificationInteraction> PlatformNotificationInteractions { get; set; } = new List<PlatformNotificationInteraction>();
}
