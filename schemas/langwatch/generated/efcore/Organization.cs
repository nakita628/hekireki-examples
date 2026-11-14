#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Kind { get; set; } = "customer";

    public string? PhoneNumber { get; set; }

    public string Slug { get; set; } = null!;

    public virtual ICollection<OrganizationUser> Members { get; set; } = new List<OrganizationUser>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<WebhookEndpoint> WebhookEndpoints { get; set; } = new List<WebhookEndpoint>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrganizationInvite> OrganizationInvite { get; set; } = new List<OrganizationInvite>();

    public int? UsageSpendingMaxLimit { get; set; }

    public int? DatasetAttachmentMaxMb { get; set; }

    public string? PrivateClickHouseLabel { get; set; }

    public string? PrivateS3Label { get; set; }

    public DateTime? PrivateDataplaneSeenAt { get; set; }

    public int MaxSessionDurationDays { get; set; } = 0;

    public bool MfaRequired { get; set; } = false;

    public int LockoutAfterFailedAttempts { get; set; } = 0;

    public int LockoutMinutes { get; set; } = 30;

    public int SessionIdleTimeoutMinutes { get; set; } = 1440;

    public int SessionMaxLifetimeMinutes { get; set; } = 0;

    public string? SignupData { get; set; }

    public bool SignedDPA { get; set; } = false;

    public string? ElasticsearchNodeUrl { get; set; }

    public string? ElasticsearchApiKey { get; set; }

    public bool UseCustomElasticsearch { get; set; } = false;

    public string? S3Endpoint { get; set; }

    public string? S3AccessKeyId { get; set; }

    public string? S3SecretAccessKey { get; set; }

    public string? S3Bucket { get; set; }

    public bool UseCustomS3 { get; set; } = false;

    public DateTime? SentPlanLimitAlert { get; set; }

    public string? SsoDomain { get; set; }

    public string? SsoProvider { get; set; }

    public string DomainJoin { get; set; } = "request";

    public OrganizationUserRole JoinerRole { get; set; } = OrganizationUserRole.Member;

    public List<string>? JoinDomains { get; set; }

    public bool PresenceEnabled { get; set; } = true;

    public bool TraceSharingEnabled { get; set; } = true;

    public string? SupportContact { get; set; }

    public OrganizationIntent? PrimaryIntent { get; set; }

    public string? PromoCode { get; set; }

    public string? StripeCustomerId { get; set; }

    public Currency Currency { get; set; } = Currency.Eur;

    public PricingModel PricingModel { get; set; } = PricingModel.Tiered;

    public virtual ICollection<CustomRole> CustomRoles { get; set; } = new List<CustomRole>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<ScimToken> ScimTokens { get; set; } = new List<ScimToken>();

    public virtual ICollection<ScimRequestLog> ScimRequestLog { get; set; } = new List<ScimRequestLog>();

    public virtual ICollection<PromptTag> PromptTags { get; set; } = new List<PromptTag>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<RoleBinding> RoleBindings { get; set; } = new List<RoleBinding>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public string? License { get; set; }

    public DateTime? LicenseExpiresAt { get; set; }

    public DateTime? LicenseLastValidatedAt { get; set; }

    public bool SelfHostedCustomer { get; set; } = false;

    public List<string>? ConnectServicesDisabled { get; set; } = new List<string>();

    public DateTime? InstantEvalsEnabledAt { get; set; }

    public string? InstantEvalsEnabledByUserId { get; set; }

    public DateTime? ConnectLastSyncAt { get; set; }

    public string? ConnectLastSyncError { get; set; }

    public virtual ICollection<IssuedLicense> IssuedLicenses { get; set; } = new List<IssuedLicense>();

    public virtual ConnectedBillingAccount? ConnectedBilling { get; set; }

    public virtual ICollection<GatewayChangeEvent> GatewayChangeEvents { get; set; } = new List<GatewayChangeEvent>();

    public virtual ICollection<RoutingPolicy> RoutingPolicies { get; set; } = new List<RoutingPolicy>();

    public virtual ICollection<IngestionSource> IngestionSources { get; set; } = new List<IngestionSource>();

    public virtual ICollection<AnomalyRule> AnomalyRules { get; set; } = new List<AnomalyRule>();

    public virtual ICollection<AnomalyAlert> AnomalyAlerts { get; set; } = new List<AnomalyAlert>();

    public virtual ICollection<AiToolEntry> AiToolEntries { get; set; } = new List<AiToolEntry>();

    public virtual ICollection<IngestionTemplate> IngestionTemplates { get; set; } = new List<IngestionTemplate>();

    public virtual ICollection<GithubInstallation> GithubInstallations { get; set; } = new List<GithubInstallation>();

    public virtual ICollection<GithubPullRequest> GithubPullRequests { get; set; } = new List<GithubPullRequest>();

    public virtual ICollection<GithubBranchPullRequestCheck> GithubBranchPrChecks { get; set; } = new List<GithubBranchPullRequestCheck>();
}
