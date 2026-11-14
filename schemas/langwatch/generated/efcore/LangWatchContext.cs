#nullable enable

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Visus.Cuid;

namespace LangWatch.Models;

public partial class LangWatchContext : DbContext
{
    public LangWatchContext(DbContextOptions<LangWatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountCredential> AccountCredentials { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<UserErasure> UserErasures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Identifier> Identifiers { get; set; }

    public virtual DbSet<IdentitySignInSwitch> IdentitySignInSwitches { get; set; }

    public virtual DbSet<IdentifierReservation> IdentifierReservations { get; set; }

    public virtual DbSet<IdentityProjectionCursor> IdentityProjectionCursors { get; set; }

    public virtual DbSet<TwoFactor> TwoFactors { get; set; }

    public virtual DbSet<SignInAttemptLock> SignInAttemptLocks { get; set; }

    public virtual DbSet<Passkey> Passkeys { get; set; }

    public virtual DbSet<MfaEnrollment> MfaEnrollments { get; set; }

    public virtual DbSet<SsoConnection> SsoConnections { get; set; }

    public virtual DbSet<SsoConnectionRegistrationSlot> SsoConnectionRegistrationSlots { get; set; }

    public virtual DbSet<SsoConnectionReproofCursor> SsoConnectionReproofCursors { get; set; }

    public virtual DbSet<SsoCredential> SsoCredentials { get; set; }

    public virtual DbSet<SsoVerifiedDomain> SsoVerifiedDomains { get; set; }

    public virtual DbSet<SsoVerifiedDomainHolder> SsoVerifiedDomainHolders { get; set; }

    public virtual DbSet<SsoAuthenticationActivity> SsoAuthenticationActivities { get; set; }

    public virtual DbSet<SsoProvider> SsoProviders { get; set; }

    public virtual DbSet<SsoBreakGlassBinding> SsoBreakGlassBindings { get; set; }

    public virtual DbSet<SsoActivationRecoveryReservation> SsoActivationRecoveryReservations { get; set; }

    public virtual DbSet<JoinRequest> JoinRequests { get; set; }

    public virtual DbSet<TeamUser> TeamUsers { get; set; }

    public virtual DbSet<OrganizationUser> OrganizationUsers { get; set; }

    public virtual DbSet<SeatHold> SeatHolds { get; set; }

    public virtual DbSet<SeatRequest> SeatRequests { get; set; }

    public virtual DbSet<OrganizationSeatLease> OrganizationSeatLeases { get; set; }

    public virtual DbSet<OrganizationAccessNotice> OrganizationAccessNotices { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<IssuedLicense> IssuedLicenses { get; set; }

    public virtual DbSet<OrganizationLicense> OrganizationLicenses { get; set; }

    public virtual DbSet<ActivationCode> ActivationCodes { get; set; }

    public virtual DbSet<ConnectedBillingAccount> ConnectedBillingAccounts { get; set; }

    public virtual DbSet<ConnectedCreditGrant> ConnectedCreditGrants { get; set; }

    public virtual DbSet<ConnectedInvoice> ConnectedInvoices { get; set; }

    public virtual DbSet<ScimRequestLog> ScimRequestLogs { get; set; }

    public virtual DbSet<ScimToken> ScimTokens { get; set; }

    public virtual DbSet<ScimExternalId> ScimExternalIds { get; set; }

    public virtual DbSet<ScimUserResource> ScimUserResources { get; set; }

    public virtual DbSet<ScimDirectoryUser> ScimDirectoryUsers { get; set; }

    public virtual DbSet<ScimSyncState> ScimSyncStates { get; set; }

    public virtual DbSet<ScimSsoConnectionView> ScimSsoConnectionViews { get; set; }

    public virtual DbSet<OrganizationSeatPeak> OrganizationSeatPeaks { get; set; }

    public virtual DbSet<OrganizationSeatPeakHolder> OrganizationSeatPeakHolders { get; set; }

    public virtual DbSet<OrganizationSeatPeakMonth> OrganizationSeatPeakMonths { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentMembershipHistory> DepartmentMembershipHistories { get; set; }

    public virtual DbSet<PlatformToolPolicy> PlatformToolPolicies { get; set; }

    public virtual DbSet<OrganizationInvite> OrganizationInvites { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Cost> Costs { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Dataset> Datasets { get; set; }

    public virtual DbSet<DatasetRecord> DatasetRecords { get; set; }

    public virtual DbSet<DatasetVersion> DatasetVersions { get; set; }

    public virtual DbSet<DatasetVersionRun> DatasetVersionRuns { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<DashboardFavourite> DashboardFavourites { get; set; }

    public virtual DbSet<DashboardsV2> DashboardsV2s { get; set; }

    public virtual DbSet<DashboardWidget> DashboardWidgets { get; set; }

    public virtual DbSet<InsightProjection> InsightProjections { get; set; }

    public virtual DbSet<InsightReaderProjection> InsightReaderProjections { get; set; }

    public virtual DbSet<InsightDailyScheduleProjection> InsightDailyScheduleProjections { get; set; }

    public virtual DbSet<SavedView> SavedViews { get; set; }

    public virtual DbSet<CustomGraph> CustomGraphs { get; set; }

    public virtual DbSet<BatchEvaluation> BatchEvaluations { get; set; }

    public virtual DbSet<Trigger> Triggers { get; set; }

    public virtual DbSet<SlackIntegration> SlackIntegrations { get; set; }

    public virtual DbSet<SlackConnectionClaim> SlackConnectionClaims { get; set; }

    public virtual DbSet<WebhookEndpoint> WebhookEndpoints { get; set; }

    public virtual DbSet<WebhookEndpointDelivery> WebhookEndpointDeliveries { get; set; }

    public virtual DbSet<Experiment> Experiments { get; set; }

    public virtual DbSet<ExperimentVersion> ExperimentVersions { get; set; }

    public virtual DbSet<Annotation> Annotations { get; set; }

    public virtual DbSet<ModelProvider> ModelProviders { get; set; }

    public virtual DbSet<ModelProviderScope> ModelProviderScopes { get; set; }

    public virtual DbSet<ModelDefaultConfig> ModelDefaultConfigs { get; set; }

    public virtual DbSet<ModelDefaultConfigScope> ModelDefaultConfigScopes { get; set; }

    public virtual DbSet<GithubInstallation> GithubInstallations { get; set; }

    public virtual DbSet<GithubPullRequest> GithubPullRequests { get; set; }

    public virtual DbSet<GithubBranchPullRequestCheck> GithubBranchPullRequestChecks { get; set; }

    public virtual DbSet<LangyConversationProjection> LangyConversationProjections { get; set; }

    public virtual DbSet<LangyTurnRequest> LangyTurnRequests { get; set; }

    public virtual DbSet<LangyActiveTurn> LangyActiveTurns { get; set; }

    public virtual DbSet<LangyConversationTurnProjection> LangyConversationTurnProjections { get; set; }

    public virtual DbSet<LangyMessageProjection> LangyMessageProjections { get; set; }

    public virtual DbSet<ProjectSecret> ProjectSecrets { get; set; }

    public virtual DbSet<TriggerSent> TriggerSents { get; set; }

    public virtual DbSet<TriggerLatestEvaluation> TriggerLatestEvaluations { get; set; }

    public virtual DbSet<EmailSuppression> EmailSuppressions { get; set; }

    public virtual DbSet<AnnotationScore> AnnotationScores { get; set; }

    public virtual DbSet<AnnotationQueue> AnnotationQueues { get; set; }

    public virtual DbSet<AnnotationQueueMembers> AnnotationQueueMembers { get; set; }

    public virtual DbSet<AnnotationQueueScores> AnnotationQueueScores { get; set; }

    public virtual DbSet<AnnotationQueueItem> AnnotationQueueItems { get; set; }

    public virtual DbSet<ShareLink> ShareLinks { get; set; }

    public virtual DbSet<PinnedTrace> PinnedTraces { get; set; }

    public virtual DbSet<TraceEditOverlay> TraceEditOverlays { get; set; }

    public virtual DbSet<TraceIngestSourceBilling> TraceIngestSourceBillings { get; set; }

    public virtual DbSet<RetentionPolicy> RetentionPolicies { get; set; }

    public virtual DbSet<DataPrivacyPolicy> DataPrivacyPolicies { get; set; }

    public virtual DbSet<InstantEvalJudgeProject> InstantEvalJudgeProjects { get; set; }

    public virtual DbSet<InstantEvalJudgeUsageBilling> InstantEvalJudgeUsageBillings { get; set; }

    public virtual DbSet<InstantEvalJudgeSpend> InstantEvalJudgeSpends { get; set; }

    public virtual DbSet<CustomLLMModelCost> CustomLLMModelCosts { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowVersion> WorkflowVersions { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<LlmPromptConfig> LlmPromptConfigs { get; set; }

    public virtual DbSet<LlmPromptConfigVersion> LlmPromptConfigVersions { get; set; }

    public virtual DbSet<PromptTagAssignment> PromptTagAssignments { get; set; }

    public virtual DbSet<PromptTag> PromptTags { get; set; }

    public virtual DbSet<Analytics> Analytics { get; set; }

    public virtual DbSet<CustomRole> CustomRoles { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<AgentVersion> AgentVersions { get; set; }

    public virtual DbSet<AgentVersionLabel> AgentVersionLabels { get; set; }

    public virtual DbSet<Evaluator> Evaluators { get; set; }

    public virtual DbSet<EvaluatorVersion> EvaluatorVersions { get; set; }

    public virtual DbSet<EvaluatorVersionLabel> EvaluatorVersionLabels { get; set; }

    public virtual DbSet<Scenario> Scenarios { get; set; }

    public virtual DbSet<ScenarioVersion> ScenarioVersions { get; set; }

    public virtual DbSet<SimulationSuite> SimulationSuites { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMembership> GroupMemberships { get; set; }

    public virtual DbSet<AuthzUserStanding> AuthzUserStandings { get; set; }

    public virtual DbSet<AuthzEngineSwitch> AuthzEngineSwitches { get; set; }

    public virtual DbSet<AuthzOrganizationPolicy> AuthzOrganizationPolicies { get; set; }

    public virtual DbSet<RoleBinding> RoleBindings { get; set; }

    public virtual DbSet<Grant> Grants { get; set; }

    public virtual DbSet<GrantUsage> GrantUsages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<SigningKey> SigningKeys { get; set; }

    public virtual DbSet<ProjectActiveDay> ProjectActiveDays { get; set; }

    public virtual DbSet<NurturingProject> NurturingProjects { get; set; }

    public virtual DbSet<NurturingUncopiedProject> NurturingUncopiedProjects { get; set; }

    public virtual DbSet<NurturingOrganizationMember> NurturingOrganizationMembers { get; set; }

    public virtual DbSet<NurturingOrganizationCreated> NurturingOrganizationCreateds { get; set; }

    public virtual DbSet<NurturingOrganization> NurturingOrganizations { get; set; }

    public virtual DbSet<NurturingProjectUsageView> NurturingProjectUsageViews { get; set; }

    public virtual DbSet<NurturingProjectTraceDay> NurturingProjectTraceDays { get; set; }

    public virtual DbSet<NurturingPersonalProject> NurturingPersonalProjects { get; set; }

    public virtual DbSet<NurturingTracedProject> NurturingTracedProjects { get; set; }

    public virtual DbSet<BillingMeterCheckpoint> BillingMeterCheckpoints { get; set; }

    public virtual DbSet<VirtualKey> VirtualKeys { get; set; }

    public virtual DbSet<VirtualKeyScope> VirtualKeyScopes { get; set; }

    public virtual DbSet<RoutingPolicy> RoutingPolicies { get; set; }

    public virtual DbSet<RoutingPolicyScope> RoutingPolicyScopes { get; set; }

    public virtual DbSet<GatewayGuardrail> GatewayGuardrails { get; set; }

    public virtual DbSet<IngestionSource> IngestionSources { get; set; }

    public virtual DbSet<AnomalyRule> AnomalyRules { get; set; }

    public virtual DbSet<AnomalyAlert> AnomalyAlerts { get; set; }

    public virtual DbSet<AiToolEntry> AiToolEntries { get; set; }

    public virtual DbSet<AiToolEntryTeam> AiToolEntryTeams { get; set; }

    public virtual DbSet<AiToolEntryDepartment> AiToolEntryDepartments { get; set; }

    public virtual DbSet<IngestionTemplate> IngestionTemplates { get; set; }

    public virtual DbSet<GatewayConnectUpstream> GatewayConnectUpstreams { get; set; }

    public virtual DbSet<GatewayTraceExportKey> GatewayTraceExportKeys { get; set; }

    public virtual DbSet<GatewayBudget> GatewayBudgets { get; set; }

    public virtual DbSet<GatewayBudgetBucketBoundary> GatewayBudgetBucketBoundaries { get; set; }

    public virtual DbSet<GatewayBudgetLedger> GatewayBudgetLedgers { get; set; }

    public virtual DbSet<GatewayChangeEvent> GatewayChangeEvents { get; set; }

    public virtual DbSet<GatewayCacheRule> GatewayCacheRules { get; set; }

    public virtual DbSet<FeatureFlag> FeatureFlags { get; set; }

    public virtual DbSet<FeatureFlagExperimentSetting> FeatureFlagExperimentSettings { get; set; }

    public virtual DbSet<ProcessManagerInstance> ProcessManagerInstances { get; set; }

    public virtual DbSet<ProcessManagerInbox> ProcessManagerInboxes { get; set; }

    public virtual DbSet<ProcessManagerOutbox> ProcessManagerOutboxes { get; set; }

    public virtual DbSet<ProcessManagerOutboxAttempt> ProcessManagerOutboxAttempts { get; set; }

    public virtual DbSet<TopicClusteringRunProjection> TopicClusteringRunProjections { get; set; }

    public virtual DbSet<TopicModelProjection> TopicModelProjections { get; set; }

    public virtual DbSet<TopicClusteringRunHistoryProjection> TopicClusteringRunHistoryProjections { get; set; }

    public virtual DbSet<IngestionPullRunProjection> IngestionPullRunProjections { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJobs { get; set; }

    public virtual DbSet<BugReport> BugReports { get; set; }

    public virtual DbSet<IdempotencyReceipt> IdempotencyReceipts { get; set; }

    public virtual DbSet<GatewayRealtimeSession> GatewayRealtimeSessions { get; set; }

    public virtual DbSet<GatewayRealtimeSessionReport> GatewayRealtimeSessionReports { get; set; }

    public virtual DbSet<SystemMigrationTenantState> SystemMigrationTenantStates { get; set; }

    public virtual DbSet<SystemMigrationEnrollment> SystemMigrationEnrollments { get; set; }

    public virtual DbSet<StoredObject> StoredObjects { get; set; }

    public virtual DbSet<DiscoveredPerson> DiscoveredPeople { get; set; }

    public virtual DbSet<DiscoveredAgent> DiscoveredAgents { get; set; }

    public virtual DbSet<IdentityMatch> IdentityMatches { get; set; }

    public virtual DbSet<GovernanceTenantHistory> GovernanceTenantHistories { get; set; }

    public virtual DbSet<ErasedIdentifierSuppression> ErasedIdentifierSuppressions { get; set; }

    public virtual DbSet<IdentityMatchSuggestion> IdentityMatchSuggestions { get; set; }

    public virtual DbSet<ConnectedSeatChange> ConnectedSeatChanges { get; set; }

    public virtual DbSet<ConnectedStatement> ConnectedStatements { get; set; }

    public virtual DbSet<LicenseRenewalClaim> LicenseRenewalClaims { get; set; }

    public virtual DbSet<InstanceIdentity> InstanceIdentities { get; set; }

    public virtual DbSet<SelfHostedInstance> SelfHostedInstances { get; set; }

    public virtual DbSet<SelfHostedInstanceDiagnostic> SelfHostedInstanceDiagnostics { get; set; }

    public virtual DbSet<SelfHostedNotice> SelfHostedNotices { get; set; }

    public virtual DbSet<SelfHostedInstanceReport> SelfHostedInstanceReports { get; set; }

    public virtual DbSet<WebPushSubscription> WebPushSubscriptions { get; set; }

    public virtual DbSet<WebPushVapidKey> WebPushVapidKeys { get; set; }

    public virtual DbSet<SignInProviderMount> SignInProviderMounts { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<SsoConnectionMigrationPhase>("SsoConnectionMigrationPhase");
        npgsql.MapEnum<TeamUserRole>("TeamUserRole");
        npgsql.MapEnum<RoleBindingScopeType>("RoleBindingScopeType");
        npgsql.MapEnum<GrantScopeType>("GrantScopeType");
        npgsql.MapEnum<GrantPrincipalType>("GrantPrincipalType");
        npgsql.MapEnum<OrganizationUserRole>("OrganizationUserRole");
        npgsql.MapEnum<IssuedLicenseSource>("IssuedLicenseSource");
        npgsql.MapEnum<ConnectedCreditGrantKind>("ConnectedCreditGrantKind");
        npgsql.MapEnum<ConnectedInvoiceKind>("ConnectedInvoiceKind");
        npgsql.MapEnum<PlanTypes>("PlanTypes");
        npgsql.MapEnum<PricingModel>("PricingModel");
        npgsql.MapEnum<OrganizationIntent>("OrganizationIntent");
        npgsql.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        npgsql.MapEnum<Currency>("Currency");
        npgsql.MapEnum<InviteStatus>("INVITE_STATUS");
        npgsql.MapEnum<EvaluationExecutionMode>("EvaluationExecutionMode");
        npgsql.MapEnum<CostType>("CostType");
        npgsql.MapEnum<CostReferenceType>("CostReferenceType");
        npgsql.MapEnum<DashboardScope>("DashboardScope");
        npgsql.MapEnum<TriggerAction>("TriggerAction");
        npgsql.MapEnum<TriggerKind>("TriggerKind");
        npgsql.MapEnum<AlertType>("AlertType");
        npgsql.MapEnum<SlackIntegrationScopeType>("SlackIntegrationScopeType");
        npgsql.MapEnum<SlackIntegrationKind>("SlackIntegrationKind");
        npgsql.MapEnum<WebhookDeliveryOutcome>("WebhookDeliveryOutcome");
        npgsql.MapEnum<WebhookDestinationKind>("WebhookDestinationKind");
        npgsql.MapEnum<WebhookEndpointStatus>("WebhookEndpointStatus");
        npgsql.MapEnum<WebhookDeliveryChannel>("WebhookDeliveryChannel");
        npgsql.MapEnum<ExperimentType>("ExperimentType");
        npgsql.MapEnum<ModelProviderScopeType>("ModelProviderScopeType");
        npgsql.MapEnum<ModelDefaultScopeType>("ModelDefaultScopeType");
        npgsql.MapEnum<LangyProjectionTitleSource>("LangyProjectionTitleSource");
        npgsql.MapEnum<AnnotationScoreDataType>("AnnotationScoreDataType");
        npgsql.MapEnum<ShareResourceType>("ShareResourceType");
        npgsql.MapEnum<ShareVisibility>("ShareVisibility");
        npgsql.MapEnum<PinSource>("PinSource");
        npgsql.MapEnum<RetentionPolicyScopeType>("RetentionPolicyScopeType");
        npgsql.MapEnum<DataPrivacyScopeType>("DataPrivacyScopeType");
        npgsql.MapEnum<CustomLLMModelCostScopeType>("CustomLLMModelCostScopeType");
        npgsql.MapEnum<PromptScope>("PromptScope");
        npgsql.MapEnum<VirtualKeyStatus>("VirtualKeyStatus");
        npgsql.MapEnum<VirtualKeyPurpose>("VirtualKeyPurpose");
        npgsql.MapEnum<GatewayProviderRotationPolicy>("GatewayProviderRotationPolicy");
        npgsql.MapEnum<GatewayProviderHealthStatus>("GatewayProviderHealthStatus");
        npgsql.MapEnum<GatewayBudgetScopeType>("GatewayBudgetScopeType");
        npgsql.MapEnum<GatewayBudgetWindow>("GatewayBudgetWindow");
        npgsql.MapEnum<GatewayBudgetBreachAction>("GatewayBudgetBreachAction");
        npgsql.MapEnum<GatewayBudgetLedgerStatus>("GatewayBudgetLedgerStatus");
        npgsql.MapEnum<GatewayChangeEventKind>("GatewayChangeEventKind");
        npgsql.MapEnum<GatewayCacheRuleMode>("GatewayCacheRuleMode");
        npgsql.MapEnum<VirtualKeyRoutingMode>("VirtualKeyRoutingMode");
        npgsql.MapEnum<VirtualKeyScopeType>("VirtualKeyScopeType");
        npgsql.MapEnum<RoutingPolicyScopeType>("RoutingPolicyScopeType");
        npgsql.MapEnum<GatewayGuardrailDirection>("GatewayGuardrailDirection");
        npgsql.MapEnum<GatewayGuardrailFailureMode>("GatewayGuardrailFailureMode");
        npgsql.MapEnum<FeatureFlagExperimentSubjectType>("FeatureFlagExperimentSubjectType");
        npgsql.MapEnum<ProcessManagerOutboxStatus>("ProcessManagerOutboxStatus");
        npgsql.MapEnum<ProcessManagerOutboxAttemptOutcome>("ProcessManagerOutboxAttemptOutcome");
        npgsql.MapEnum<GatewayRealtimeSessionStatus>("GatewayRealtimeSessionStatus");
        npgsql.MapEnum<ConnectedSeatChangeState>("ConnectedSeatChangeState");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<SsoConnectionMigrationPhase>("SsoConnectionMigrationPhase");
        dataSource.MapEnum<TeamUserRole>("TeamUserRole");
        dataSource.MapEnum<RoleBindingScopeType>("RoleBindingScopeType");
        dataSource.MapEnum<GrantScopeType>("GrantScopeType");
        dataSource.MapEnum<GrantPrincipalType>("GrantPrincipalType");
        dataSource.MapEnum<OrganizationUserRole>("OrganizationUserRole");
        dataSource.MapEnum<IssuedLicenseSource>("IssuedLicenseSource");
        dataSource.MapEnum<ConnectedCreditGrantKind>("ConnectedCreditGrantKind");
        dataSource.MapEnum<ConnectedInvoiceKind>("ConnectedInvoiceKind");
        dataSource.MapEnum<PlanTypes>("PlanTypes");
        dataSource.MapEnum<PricingModel>("PricingModel");
        dataSource.MapEnum<OrganizationIntent>("OrganizationIntent");
        dataSource.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        dataSource.MapEnum<Currency>("Currency");
        dataSource.MapEnum<InviteStatus>("INVITE_STATUS");
        dataSource.MapEnum<EvaluationExecutionMode>("EvaluationExecutionMode");
        dataSource.MapEnum<CostType>("CostType");
        dataSource.MapEnum<CostReferenceType>("CostReferenceType");
        dataSource.MapEnum<DashboardScope>("DashboardScope");
        dataSource.MapEnum<TriggerAction>("TriggerAction");
        dataSource.MapEnum<TriggerKind>("TriggerKind");
        dataSource.MapEnum<AlertType>("AlertType");
        dataSource.MapEnum<SlackIntegrationScopeType>("SlackIntegrationScopeType");
        dataSource.MapEnum<SlackIntegrationKind>("SlackIntegrationKind");
        dataSource.MapEnum<WebhookDeliveryOutcome>("WebhookDeliveryOutcome");
        dataSource.MapEnum<WebhookDestinationKind>("WebhookDestinationKind");
        dataSource.MapEnum<WebhookEndpointStatus>("WebhookEndpointStatus");
        dataSource.MapEnum<WebhookDeliveryChannel>("WebhookDeliveryChannel");
        dataSource.MapEnum<ExperimentType>("ExperimentType");
        dataSource.MapEnum<ModelProviderScopeType>("ModelProviderScopeType");
        dataSource.MapEnum<ModelDefaultScopeType>("ModelDefaultScopeType");
        dataSource.MapEnum<LangyProjectionTitleSource>("LangyProjectionTitleSource");
        dataSource.MapEnum<AnnotationScoreDataType>("AnnotationScoreDataType");
        dataSource.MapEnum<ShareResourceType>("ShareResourceType");
        dataSource.MapEnum<ShareVisibility>("ShareVisibility");
        dataSource.MapEnum<PinSource>("PinSource");
        dataSource.MapEnum<RetentionPolicyScopeType>("RetentionPolicyScopeType");
        dataSource.MapEnum<DataPrivacyScopeType>("DataPrivacyScopeType");
        dataSource.MapEnum<CustomLLMModelCostScopeType>("CustomLLMModelCostScopeType");
        dataSource.MapEnum<PromptScope>("PromptScope");
        dataSource.MapEnum<VirtualKeyStatus>("VirtualKeyStatus");
        dataSource.MapEnum<VirtualKeyPurpose>("VirtualKeyPurpose");
        dataSource.MapEnum<GatewayProviderRotationPolicy>("GatewayProviderRotationPolicy");
        dataSource.MapEnum<GatewayProviderHealthStatus>("GatewayProviderHealthStatus");
        dataSource.MapEnum<GatewayBudgetScopeType>("GatewayBudgetScopeType");
        dataSource.MapEnum<GatewayBudgetWindow>("GatewayBudgetWindow");
        dataSource.MapEnum<GatewayBudgetBreachAction>("GatewayBudgetBreachAction");
        dataSource.MapEnum<GatewayBudgetLedgerStatus>("GatewayBudgetLedgerStatus");
        dataSource.MapEnum<GatewayChangeEventKind>("GatewayChangeEventKind");
        dataSource.MapEnum<GatewayCacheRuleMode>("GatewayCacheRuleMode");
        dataSource.MapEnum<VirtualKeyRoutingMode>("VirtualKeyRoutingMode");
        dataSource.MapEnum<VirtualKeyScopeType>("VirtualKeyScopeType");
        dataSource.MapEnum<RoutingPolicyScopeType>("RoutingPolicyScopeType");
        dataSource.MapEnum<GatewayGuardrailDirection>("GatewayGuardrailDirection");
        dataSource.MapEnum<GatewayGuardrailFailureMode>("GatewayGuardrailFailureMode");
        dataSource.MapEnum<FeatureFlagExperimentSubjectType>("FeatureFlagExperimentSubjectType");
        dataSource.MapEnum<ProcessManagerOutboxStatus>("ProcessManagerOutboxStatus");
        dataSource.MapEnum<ProcessManagerOutboxAttemptOutcome>("ProcessManagerOutboxAttemptOutcome");
        dataSource.MapEnum<GatewayRealtimeSessionStatus>("GatewayRealtimeSessionStatus");
        dataSource.MapEnum<ConnectedSeatChangeState>("ConnectedSeatChangeState");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<SsoConnectionMigrationPhase>(name: "SsoConnectionMigrationPhase");
        modelBuilder.HasPostgresEnum<TeamUserRole>(name: "TeamUserRole");
        modelBuilder.HasPostgresEnum<RoleBindingScopeType>(name: "RoleBindingScopeType");
        modelBuilder.HasPostgresEnum<GrantScopeType>(name: "GrantScopeType");
        modelBuilder.HasPostgresEnum<GrantPrincipalType>(name: "GrantPrincipalType");
        modelBuilder.HasPostgresEnum<OrganizationUserRole>(name: "OrganizationUserRole");
        modelBuilder.HasPostgresEnum<IssuedLicenseSource>(name: "IssuedLicenseSource");
        modelBuilder.HasPostgresEnum<ConnectedCreditGrantKind>(name: "ConnectedCreditGrantKind");
        modelBuilder.HasPostgresEnum<ConnectedInvoiceKind>(name: "ConnectedInvoiceKind");
        modelBuilder.HasPostgresEnum<PlanTypes>(name: "PlanTypes");
        modelBuilder.HasPostgresEnum<PricingModel>(name: "PricingModel");
        modelBuilder.HasPostgresEnum<OrganizationIntent>(name: "OrganizationIntent");
        modelBuilder.HasPostgresEnum<SubscriptionStatus>(name: "SubscriptionStatus");
        modelBuilder.HasPostgresEnum<Currency>(name: "Currency");
        modelBuilder.HasPostgresEnum<InviteStatus>(name: "INVITE_STATUS");
        modelBuilder.HasPostgresEnum<EvaluationExecutionMode>(name: "EvaluationExecutionMode");
        modelBuilder.HasPostgresEnum<CostType>(name: "CostType");
        modelBuilder.HasPostgresEnum<CostReferenceType>(name: "CostReferenceType");
        modelBuilder.HasPostgresEnum<DashboardScope>(name: "DashboardScope");
        modelBuilder.HasPostgresEnum<TriggerAction>(name: "TriggerAction");
        modelBuilder.HasPostgresEnum<TriggerKind>(name: "TriggerKind");
        modelBuilder.HasPostgresEnum<AlertType>(name: "AlertType");
        modelBuilder.HasPostgresEnum<SlackIntegrationScopeType>(name: "SlackIntegrationScopeType");
        modelBuilder.HasPostgresEnum<SlackIntegrationKind>(name: "SlackIntegrationKind");
        modelBuilder.HasPostgresEnum<WebhookDeliveryOutcome>(name: "WebhookDeliveryOutcome");
        modelBuilder.HasPostgresEnum<WebhookDestinationKind>(name: "WebhookDestinationKind");
        modelBuilder.HasPostgresEnum<WebhookEndpointStatus>(name: "WebhookEndpointStatus");
        modelBuilder.HasPostgresEnum<WebhookDeliveryChannel>(name: "WebhookDeliveryChannel");
        modelBuilder.HasPostgresEnum<ExperimentType>(name: "ExperimentType");
        modelBuilder.HasPostgresEnum<ModelProviderScopeType>(name: "ModelProviderScopeType");
        modelBuilder.HasPostgresEnum<ModelDefaultScopeType>(name: "ModelDefaultScopeType");
        modelBuilder.HasPostgresEnum<LangyProjectionTitleSource>(name: "LangyProjectionTitleSource");
        modelBuilder.HasPostgresEnum<AnnotationScoreDataType>(name: "AnnotationScoreDataType");
        modelBuilder.HasPostgresEnum<ShareResourceType>(name: "ShareResourceType");
        modelBuilder.HasPostgresEnum<ShareVisibility>(name: "ShareVisibility");
        modelBuilder.HasPostgresEnum<PinSource>(name: "PinSource");
        modelBuilder.HasPostgresEnum<RetentionPolicyScopeType>(name: "RetentionPolicyScopeType");
        modelBuilder.HasPostgresEnum<DataPrivacyScopeType>(name: "DataPrivacyScopeType");
        modelBuilder.HasPostgresEnum<CustomLLMModelCostScopeType>(name: "CustomLLMModelCostScopeType");
        modelBuilder.HasPostgresEnum<PromptScope>(name: "PromptScope");
        modelBuilder.HasPostgresEnum<VirtualKeyStatus>(name: "VirtualKeyStatus");
        modelBuilder.HasPostgresEnum<VirtualKeyPurpose>(name: "VirtualKeyPurpose");
        modelBuilder.HasPostgresEnum<GatewayProviderRotationPolicy>(name: "GatewayProviderRotationPolicy");
        modelBuilder.HasPostgresEnum<GatewayProviderHealthStatus>(name: "GatewayProviderHealthStatus");
        modelBuilder.HasPostgresEnum<GatewayBudgetScopeType>(name: "GatewayBudgetScopeType");
        modelBuilder.HasPostgresEnum<GatewayBudgetWindow>(name: "GatewayBudgetWindow");
        modelBuilder.HasPostgresEnum<GatewayBudgetBreachAction>(name: "GatewayBudgetBreachAction");
        modelBuilder.HasPostgresEnum<GatewayBudgetLedgerStatus>(name: "GatewayBudgetLedgerStatus");
        modelBuilder.HasPostgresEnum<GatewayChangeEventKind>(name: "GatewayChangeEventKind");
        modelBuilder.HasPostgresEnum<GatewayCacheRuleMode>(name: "GatewayCacheRuleMode");
        modelBuilder.HasPostgresEnum<VirtualKeyRoutingMode>(name: "VirtualKeyRoutingMode");
        modelBuilder.HasPostgresEnum<VirtualKeyScopeType>(name: "VirtualKeyScopeType");
        modelBuilder.HasPostgresEnum<RoutingPolicyScopeType>(name: "RoutingPolicyScopeType");
        modelBuilder.HasPostgresEnum<GatewayGuardrailDirection>(name: "GatewayGuardrailDirection");
        modelBuilder.HasPostgresEnum<GatewayGuardrailFailureMode>(name: "GatewayGuardrailFailureMode");
        modelBuilder.HasPostgresEnum<FeatureFlagExperimentSubjectType>(name: "FeatureFlagExperimentSubjectType");
        modelBuilder.HasPostgresEnum<ProcessManagerOutboxStatus>(name: "ProcessManagerOutboxStatus");
        modelBuilder.HasPostgresEnum<ProcessManagerOutboxAttemptOutcome>(name: "ProcessManagerOutboxAttemptOutcome");
        modelBuilder.HasPostgresEnum<GatewayRealtimeSessionStatus>(name: "GatewayRealtimeSessionStatus");
        modelBuilder.HasPostgresEnum<ConnectedSeatChangeState>(name: "ConnectedSeatChangeState");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.HasIndex(e => new { e.Issuer, e.ProviderAccountId }, "Account_issuer_providerAccountId_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("oauth")
                .HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.ExtExpiresIn).HasColumnName("ext_expires_in");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<AccountCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccountCredential_pkey");

            entity.ToTable("AccountCredential");

            entity.HasIndex(e => e.UserId, "AccountCredential_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.IdToken).HasColumnName("idToken");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accessTokenExpiresAt");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refreshTokenExpiresAt");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.AccountCredentials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccountCredential_userId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Session_pkey");

            entity.ToTable("Session");

            entity.HasIndex(e => e.SessionToken, "Session_sessionToken_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Session_userId_idx");

            entity.HasIndex(e => e.IdentifierId, "Session_identifierId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.IdentifierId).HasColumnName("identifierId");
            entity.Property(e => e.Amr)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("amr");
            entity.Property(e => e.StartedBySignUp)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("startedBySignUp");
            entity.Property(e => e.Impersonating)
                .HasColumnType("jsonb")
                .HasColumnName("impersonating");
            entity.Property(e => e.ActorUserId).HasColumnName("actorUserId");
            entity.Property(e => e.SubjectUserId).HasColumnName("subjectUserId");
            entity.Property(e => e.ImpersonationReason).HasColumnName("impersonationReason");
            entity.Property(e => e.ImpersonationExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("impersonationExpiresAt");
            entity.Property(e => e.LastSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<UserErasure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserErasure_pkey");

            entity.ToTable("UserErasure");

            entity.HasIndex(e => e.AccountCreatedAt, "UserErasure_accountCreatedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.AccountCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accountCreatedAt");
            entity.Property(e => e.ErasedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasedAt");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailVerified");
            entity.Property(e => e.SignupConfirmationPending)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("signupConfirmationPending");
            entity.Property(e => e.PasskeySignupClaimHash).HasColumnName("passkeySignupClaimHash");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.PendingSsoSetup)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pendingSsoSetup");
            entity.Property(e => e.UserHashKey).HasColumnName("userHashKey");
            entity.Property(e => e.TwoFactorEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("twoFactorEnabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LastLoginAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastLoginAt");
            entity.Property(e => e.DeactivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deactivatedAt");
            entity.Property(e => e.LastHomePath).HasColumnName("lastHomePath");
            entity.Property(e => e.TracesExplorerTourDismissedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("tracesExplorerTourDismissedAt");
            entity.Property(e => e.LangyCodeAccessPreference).HasColumnName("langyCodeAccessPreference");
            entity.Property(e => e.PasskeyNudgeDismissedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("passkeyNudgeDismissedAt");
            entity.Property(e => e.JoinOfferDismissedDomains)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("joinOfferDismissedDomains");
            entity.Property(e => e.NotificationPreferences)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("notificationPreferences");
            entity.Property(e => e.PasswordResetRequiredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("passwordResetRequiredAt");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerificationToken_pkey");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Identifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Identifier_pkey");

            entity.ToTable("Identifier");

            entity.HasIndex(e => e.UserId, "Identifier_userId_idx");

            entity.HasIndex(e => e.IdentifierHash, "Identifier_identifierHash_idx");

            entity.HasIndex(e => e.Value, "Identifier_value_idx");

            entity.HasIndex(e => e.Domain, "Identifier_domain_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Identifier_provider_providerAccountId_idx");

            entity.HasIndex(e => new { e.Issuer, e.ProviderAccountId }, "Identifier_issuer_providerAccountId_idx");

            entity.HasIndex(e => e.AccountId, "Identifier_accountId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.IdentifierHash).HasColumnName("identifierHash");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.VerifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("verifiedAt");
            entity.Property(e => e.AttachedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("attachedAt");
            entity.Property(e => e.DetachedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("detachedAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<IdentitySignInSwitch>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("IdentitySignInSwitch_pkey");

            entity.ToTable("IdentitySignInSwitch");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OnIdentifiers).HasColumnName("onIdentifiers");
            entity.Property(e => e.Pinned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pinned");
            entity.Property(e => e.SwitchedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("switchedAt");
            entity.Property(e => e.LastAppliedEventId).HasColumnName("lastAppliedEventId");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<IdentifierReservation>(entity =>
        {
            entity.HasKey(e => e.NormalizedValue).HasName("IdentifierReservation_pkey");

            entity.ToTable("IdentifierReservation");

            entity.HasIndex(e => e.UserId, "IdentifierReservation_userId_idx");

            entity.HasIndex(e => e.IdentifierId, "IdentifierReservation_identifierId_idx");

            entity.Property(e => e.NormalizedValue).HasColumnName("normalizedValue");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.IdentifierId).HasColumnName("identifierId");
            entity.Property(e => e.CommandId).HasColumnName("commandId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<IdentityProjectionCursor>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("IdentityProjectionCursor_pkey");

            entity.ToTable("IdentityProjectionCursor");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acceptedAt");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<TwoFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TwoFactor_pkey");

            entity.ToTable("TwoFactor");

            entity.HasIndex(e => e.UserId, "TwoFactor_userId_idx");

            entity.HasIndex(e => e.Secret, "TwoFactor_secret_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.BackupCodes).HasColumnName("backupCodes");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("verified");
            entity.Property(e => e.FailedVerificationCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failedVerificationCount");
            entity.Property(e => e.LockedUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lockedUntil");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.TwoFactors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TwoFactor_userId_fkey");
        });

        modelBuilder.Entity<SignInAttemptLock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SignInAttemptLock_pkey");

            entity.ToTable("SignInAttemptLock");

            entity.HasIndex(e => e.IdentifierHash, "SignInAttemptLock_identifierHash_key").IsUnique();

            entity.HasIndex(e => e.LockedUntil, "SignInAttemptLock_lockedUntil_idx");

            entity.HasIndex(e => e.UserId, "SignInAttemptLock_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.IdentifierHash).HasColumnName("identifierHash");
            entity.Property(e => e.FailedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failedCount");
            entity.Property(e => e.LockedUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lockedUntil");
            entity.Property(e => e.ConsecutiveLockouts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveLockouts");
            entity.Property(e => e.HeldForReview)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("heldForReview");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Passkey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Passkey_pkey");

            entity.ToTable("Passkey");

            entity.HasIndex(e => e.UserId, "Passkey_userId_idx");

            entity.HasIndex(e => e.CredentialID, "Passkey_credentialID_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CredentialID).HasColumnName("credentialID");
            entity.Property(e => e.Counter)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("counter");
            entity.Property(e => e.DeviceType).HasColumnName("deviceType");
            entity.Property(e => e.BackedUp)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("backedUp");
            entity.Property(e => e.Transports).HasColumnName("transports");
            entity.Property(e => e.Aaguid).HasColumnName("aaguid");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.Passkeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Passkey_userId_fkey");
        });

        modelBuilder.Entity<MfaEnrollment>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("MfaEnrollment_pkey");

            entity.ToTable("MfaEnrollment");

            entity.HasIndex(e => e.State, "MfaEnrollment_state_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollmentId");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.EnrolledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enrolledAt");
            entity.Property(e => e.ConfirmedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("confirmedAt");
            entity.Property(e => e.ExpiredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiredAt");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
            entity.Property(e => e.DisabledVia).HasColumnName("disabledVia");
            entity.Property(e => e.BackupCodeCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("backupCodeCount");
            entity.Property(e => e.ConsumedBackupCodeIndexes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("consumedBackupCodeIndexes");
            entity.Property(e => e.FailedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failedCount");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acceptedAt");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SsoConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SsoConnection_pkey");

            entity.ToTable("SsoConnection");

            entity.HasIndex(e => e.OrganizationId, "SsoConnection_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.MigrationPhase }, "SsoConnection_organizationId_migrationPhase_idx");

            entity.HasIndex(e => e.State, "SsoConnection_state_idx");

            entity.HasIndex(e => e.VerifiedDomains, "SsoConnection_verifiedDomains_idx").HasMethod("gin");

            entity.HasIndex(e => e.LapsedDomains, "SsoConnection_lapsedDomains_idx").HasMethod("gin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.ClaimedDomains).HasColumnName("claimedDomains");
            entity.Property(e => e.DomainClaims)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("domainClaims");
            entity.Property(e => e.ApprovedDomains).HasColumnName("approvedDomains");
            entity.Property(e => e.VerifiedDomains).HasColumnName("verifiedDomains");
            entity.Property(e => e.LapsedDomains).HasColumnName("lapsedDomains");
            entity.Property(e => e.DomainVerifications)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("domainVerifications");
            entity.Property(e => e.PendingVerification)
                .HasColumnType("jsonb")
                .HasColumnName("pendingVerification");
            entity.Property(e => e.IdpMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("idpMetadata");
            entity.Property(e => e.ArrivalPolicy)
                .ValueGeneratedNever()
                .HasDefaultValue("refuse")
                .HasColumnName("arrivalPolicy");
            entity.Property(e => e.AllowsJit)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowsJit");
            entity.Property(e => e.ArrivalPolicyDecidedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("arrivalPolicyDecidedAt");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.TestLoginAccountId).HasColumnName("testLoginAccountId");
            entity.Property(e => e.ReplacesConnectionId).HasColumnName("replacesConnectionId");
            entity.Property(e => e.MigrationPhase).HasColumnName("migrationPhase");
            entity.Property(e => e.GraceStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("graceStartedAt");
            entity.Property(e => e.RouteChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("routeChangedAt");
            entity.Property(e => e.FinalizationRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finalizationRequestedAt");
            entity.Property(e => e.FinalizedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finalizedAt");
            entity.Property(e => e.Rejection)
                .HasColumnType("jsonb")
                .HasColumnName("rejection");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.TearDownAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("tearDownAfter");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acceptedAt");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SsoConnectionRegistrationSlot>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.Kind }).HasName("SsoConnectionRegistrationSlot_pkey");

            entity.ToTable("SsoConnectionRegistrationSlot");

            entity.HasIndex(e => e.ConnectionId, "SsoConnectionRegistrationSlot_connectionId_key").IsUnique();

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ReplacesConnectionId).HasColumnName("replacesConnectionId");
            entity.Property(e => e.CommandId).HasColumnName("commandId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SsoConnectionReproofCursor>(entity =>
        {
            entity.HasKey(e => e.ConnectionId).HasName("SsoConnectionReproofCursor_pkey");

            entity.ToTable("SsoConnectionReproofCursor");

            entity.HasIndex(e => e.LastReproofAt, "SsoConnectionReproofCursor_lastReproofAt_idx");

            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.LastReproofAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReproofAt");

            entity.HasOne(d => d.Connection).WithOne(p => p.ReproofCursor)
                .HasForeignKey<SsoConnectionReproofCursor>(d => d.ConnectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SsoConnectionReproofCursor_connectionId_fkey");
        });

        modelBuilder.Entity<SsoCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SsoCredential_pkey");

            entity.ToTable("SsoCredential");

            entity.HasIndex(e => new { e.OrganizationId, e.ConnectionId }, "SsoCredential_organizationId_connectionId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Ciphertext).HasColumnName("ciphertext");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SsoVerifiedDomain>(entity =>
        {
            entity.HasKey(e => e.Domain).HasName("SsoVerifiedDomain_pkey");

            entity.ToTable("SsoVerifiedDomain");

            entity.HasIndex(e => e.OrganizationId, "SsoVerifiedDomain_organizationId_idx");

            entity.HasAlternateKey(e => new { e.Domain, e.OrganizationId }).HasName("SsoVerifiedDomain_domain_organizationId_key");

            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
        });

        modelBuilder.Entity<SsoVerifiedDomainHolder>(entity =>
        {
            entity.HasKey(e => new { e.Domain, e.ConnectionId }).HasName("SsoVerifiedDomainHolder_pkey");

            entity.ToTable("SsoVerifiedDomainHolder");

            entity.HasIndex(e => e.ConnectionId, "SsoVerifiedDomainHolder_connectionId_idx");

            entity.HasIndex(e => e.OrganizationId, "SsoVerifiedDomainHolder_organizationId_idx");

            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

            entity.HasOne(d => d.Ownership).WithMany(p => p.Holders)
                .HasPrincipalKey(p => new { p.Domain, p.OrganizationId })
                .HasForeignKey(d => new { d.Domain, d.OrganizationId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SsoVerifiedDomainHolder_domain_organizationId_fkey");
        });

        modelBuilder.Entity<SsoAuthenticationActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SsoAuthenticationActivity_pkey");

            entity.ToTable("SsoAuthenticationActivity");

            entity.HasIndex(e => new { e.OrganizationId, e.ConnectionId, e.AuthenticatedAt }, "SsoAuthenticationActivity_organizationId_connectionId_authe_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "SsoAuthenticationActivity_organizationId_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AuthenticatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("authenticatedAt");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
        });

        modelBuilder.Entity<SsoProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SsoProvider_pkey");

            entity.ToTable("SsoProvider");

            entity.HasIndex(e => e.ProviderId, "SsoProvider_providerId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "SsoProvider_organizationId_idx");

            entity.HasIndex(e => e.Domain, "SsoProvider_domain_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.OidcConfig).HasColumnName("oidcConfig");
            entity.Property(e => e.SamlConfig).HasColumnName("samlConfig");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Domain).HasColumnName("domain");
        });

        modelBuilder.Entity<SsoBreakGlassBinding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SsoBreakGlassBinding_pkey");

            entity.ToTable("SsoBreakGlassBinding");

            entity.HasIndex(e => new { e.OrganizationId, e.ExpiresAt }, "SsoBreakGlassBinding_organizationId_expiresAt_idx");

            entity.HasIndex(e => e.UserId, "SsoBreakGlassBinding_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.GrantedByUserId).HasColumnName("grantedByUserId");
            entity.Property(e => e.GrantedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("grantedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.SupersededAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("supersededAt");
            entity.Property(e => e.RenewedFromId).HasColumnName("renewedFromId");
            entity.Property(e => e.WarnedDays).HasColumnName("warnedDays");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<SsoActivationRecoveryReservation>(entity =>
        {
            entity.HasKey(e => e.CommandId).HasName("SsoActivationRecoveryReservation_pkey");

            entity.ToTable("SsoActivationRecoveryReservation");

            entity.HasIndex(e => e.OrganizationId, "SsoActivationRecoveryReservation_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.ConnectionId }, "SsoActivationRecoveryReservation_organizationId_connectionI_key").IsUnique();

            entity.Property(e => e.CommandId).HasColumnName("commandId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<JoinRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("JoinRequest_pkey");

            entity.ToTable("JoinRequest");

            entity.HasIndex(e => new { e.OrganizationId, e.State }, "JoinRequest_organizationId_state_idx");

            entity.HasIndex(e => new { e.UserId, e.State }, "JoinRequest_userId_state_idx");

            entity.HasIndex(e => new { e.UserId, e.OrganizationId }, "JoinRequest_userId_organizationId_idx");

            entity.HasIndex(e => e.Domain, "JoinRequest_domain_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.MatchedVia).HasColumnName("matchedVia");
            entity.Property(e => e.Origin)
                .ValueGeneratedNever()
                .HasDefaultValue("web")
                .HasColumnName("origin");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");
            entity.Property(e => e.ResolvedByType).HasColumnName("resolvedByType");
            entity.Property(e => e.ResolvedById).HasColumnName("resolvedById");
            entity.Property(e => e.WithdrawalCause).HasColumnName("withdrawalCause");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acceptedAt");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<TeamUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TeamId }).HasName("TeamUser_pkey");

            entity.ToTable("TeamUser");

            entity.HasIndex(e => e.TeamId, "TeamUser_teamId_idx");

            entity.HasIndex(e => e.UserId, "TeamUser_userId_idx");

            entity.HasIndex(e => e.AssignedRoleId, "TeamUser_assignedRoleId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.AssignedRoleId).HasColumnName("assignedRoleId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.AssignedRole).WithMany(p => p.AssignedUsers)
                .HasForeignKey(d => d.AssignedRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TeamUser_assignedRoleId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TeamUser_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Members)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TeamUser_teamId_fkey");
        });

        modelBuilder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.OrganizationId }).HasName("OrganizationUser_pkey");

            entity.ToTable("OrganizationUser");

            entity.HasIndex(e => e.OrganizationId, "OrganizationUser_organizationId_idx");

            entity.HasIndex(e => e.UserId, "OrganizationUser_userId_idx");

            entity.HasIndex(e => e.DepartmentId, "OrganizationUser_departmentId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
            entity.Property(e => e.PendingSsoGrantId).HasColumnName("pendingSsoGrantId");
            entity.Property(e => e.MembershipStamp)
                .HasDefaultValueSql("gen_random_uuid()::text")
                .HasColumnName("membershipStamp");
            entity.Property(e => e.FullSeatSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("fullSeatSince");
            entity.Property(e => e.SeatPendingSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatPendingSince");

            entity.HasOne(d => d.User).WithMany(p => p.OrgMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrganizationUser_userId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrganizationUser_organizationId_fkey");
        });

        modelBuilder.Entity<SeatHold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SeatHold_pkey");

            entity.ToTable("SeatHold");

            entity.HasIndex(e => e.HeldUntil, "SeatHold_heldUntil_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "SeatHold_organizationId_userId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SeatTakenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatTakenAt");
            entity.Property(e => e.HeldUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("heldUntil");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<SeatRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SeatRequest_pkey");

            entity.ToTable("SeatRequest");

            entity.HasIndex(e => new { e.OrganizationId, e.Status }, "SeatRequest_organizationId_status_idx");

            entity.HasIndex(e => e.UserId, "SeatRequest_userId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Email }, "SeatRequest_organizationId_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.IdpRefused)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("idpRefused");
            entity.Property(e => e.IdpKind).HasColumnName("idpKind");
            entity.Property(e => e.SalesNotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("salesNotifiedAt");
            entity.Property(e => e.OpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("openedAt");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");
            entity.Property(e => e.ResolvedByUserId).HasColumnName("resolvedByUserId");
            entity.Property(e => e.SeatPendingSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatPendingSince");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<OrganizationSeatLease>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("OrganizationSeatLease_pkey");

            entity.ToTable("OrganizationSeatLease");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Holder).HasColumnName("holder");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<OrganizationAccessNotice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationAccessNotice_pkey");

            entity.ToTable("OrganizationAccessNotice");

            entity.HasIndex(e => new { e.OrganizationId, e.Kind, e.Email, e.DayKey }, "OrganizationAccessNotice_organizationId_kind_email_dayKey_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Idp).HasColumnName("idp");
            entity.Property(e => e.DayKey).HasColumnName("dayKey");
            entity.Property(e => e.IdpRefused)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("idpRefused");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.MailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("mailedAt");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.HasIndex(e => e.Slug, "Team_slug_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "Team_organizationId_idx");

            entity.HasIndex(e => e.IsPersonal, "Team_isPersonal_idx");

            entity.HasIndex(e => e.DepartmentId, "Team_departmentId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.IsPersonal)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPersonal");
            entity.Property(e => e.OwnerUserId)
                .HasMaxLength(255)
                .HasColumnName("ownerUserId");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

            entity.HasOne(d => d.Organization).WithMany(p => p.Teams)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Team_organizationId_fkey");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.PersonalTeams)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Team_ownerUserId_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pkey");

            entity.ToTable("Organization");

            entity.HasIndex(e => e.Slug, "Organization_slug_key").IsUnique();

            entity.HasIndex(e => e.SsoDomain, "Organization_ssoDomain_key").IsUnique();

            entity.HasIndex(e => e.StripeCustomerId, "Organization_stripeCustomerId_key").IsUnique();

            entity.HasIndex(e => e.SsoProvider, "Organization_ssoProvider_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("customer")
                .HasColumnName("kind");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UsageSpendingMaxLimit).HasColumnName("usageSpendingMaxLimit");
            entity.Property(e => e.DatasetAttachmentMaxMb).HasColumnName("datasetAttachmentMaxMb");
            entity.Property(e => e.PrivateClickHouseLabel).HasColumnName("privateClickHouseLabel");
            entity.Property(e => e.PrivateS3Label).HasColumnName("privateS3Label");
            entity.Property(e => e.PrivateDataplaneSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("privateDataplaneSeenAt");
            entity.Property(e => e.MaxSessionDurationDays)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("maxSessionDurationDays");
            entity.Property(e => e.MfaRequired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("mfaRequired");
            entity.Property(e => e.LockoutAfterFailedAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lockoutAfterFailedAttempts");
            entity.Property(e => e.LockoutMinutes)
                .ValueGeneratedNever()
                .HasDefaultValue(30)
                .HasColumnName("lockoutMinutes");
            entity.Property(e => e.SessionIdleTimeoutMinutes)
                .ValueGeneratedNever()
                .HasDefaultValue(1440)
                .HasColumnName("sessionIdleTimeoutMinutes");
            entity.Property(e => e.SessionMaxLifetimeMinutes)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sessionMaxLifetimeMinutes");
            entity.Property(e => e.SignupData)
                .HasColumnType("jsonb")
                .HasColumnName("signupData");
            entity.Property(e => e.SignedDPA)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("signedDPA");
            entity.Property(e => e.ElasticsearchNodeUrl).HasColumnName("elasticsearchNodeUrl");
            entity.Property(e => e.ElasticsearchApiKey).HasColumnName("elasticsearchApiKey");
            entity.Property(e => e.UseCustomElasticsearch)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useCustomElasticsearch");
            entity.Property(e => e.S3Endpoint).HasColumnName("s3Endpoint");
            entity.Property(e => e.S3AccessKeyId).HasColumnName("s3AccessKeyId");
            entity.Property(e => e.S3SecretAccessKey).HasColumnName("s3SecretAccessKey");
            entity.Property(e => e.S3Bucket).HasColumnName("s3Bucket");
            entity.Property(e => e.UseCustomS3)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useCustomS3");
            entity.Property(e => e.SentPlanLimitAlert)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentPlanLimitAlert");
            entity.Property(e => e.SsoDomain).HasColumnName("ssoDomain");
            entity.Property(e => e.SsoProvider).HasColumnName("ssoProvider");
            entity.Property(e => e.DomainJoin)
                .ValueGeneratedNever()
                .HasDefaultValue("request")
                .HasColumnName("domainJoin");
            entity.Property(e => e.JoinerRole)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganizationUserRole.Member)
                .HasColumnName("joinerRole");
            entity.Property(e => e.JoinDomains).HasColumnName("joinDomains");
            entity.Property(e => e.PresenceEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("presenceEnabled");
            entity.Property(e => e.TraceSharingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("traceSharingEnabled");
            entity.Property(e => e.SupportContact).HasColumnName("supportContact");
            entity.Property(e => e.PrimaryIntent).HasColumnName("primaryIntent");
            entity.Property(e => e.PromoCode).HasColumnName("promoCode");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.Currency)
                .ValueGeneratedNever()
                .HasDefaultValue(Currency.Eur)
                .HasColumnName("currency");
            entity.Property(e => e.PricingModel)
                .ValueGeneratedNever()
                .HasDefaultValue(PricingModel.Tiered)
                .HasColumnName("pricingModel");
            entity.Property(e => e.License).HasColumnName("license");
            entity.Property(e => e.LicenseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("licenseExpiresAt");
            entity.Property(e => e.LicenseLastValidatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("licenseLastValidatedAt");
            entity.Property(e => e.SelfHostedCustomer)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("selfHostedCustomer");
            entity.Property(e => e.ConnectServicesDisabled)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("connectServicesDisabled");
            entity.Property(e => e.InstantEvalsEnabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("instantEvalsEnabledAt");
            entity.Property(e => e.InstantEvalsEnabledByUserId).HasColumnName("instantEvalsEnabledByUserId");
            entity.Property(e => e.ConnectLastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("connectLastSyncAt");
            entity.Property(e => e.ConnectLastSyncError).HasColumnName("connectLastSyncError");
        });

        modelBuilder.Entity<IssuedLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IssuedLicense_pkey");

            entity.ToTable("IssuedLicense");

            entity.HasIndex(e => e.LicenseId, "IssuedLicense_licenseId_key").IsUnique();

            entity.HasIndex(e => e.TokenHash, "IssuedLicense_tokenHash_key").IsUnique();

            entity.HasIndex(e => e.ReplacesId, "IssuedLicense_replacesId_key").IsUnique();

            entity.HasIndex(e => e.VirtualKeyId, "IssuedLicense_virtualKeyId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "IssuedLicense_organizationId_idx");

            entity.HasIndex(e => e.ExpiresAt, "IssuedLicense_expiresAt_idx");

            entity.HasIndex(e => e.SeatsRaisedFrom, "IssuedLicense_seatsRaisedFrom_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.LicenseId).HasColumnName("licenseId");
            entity.Property(e => e.TokenHash).HasColumnName("tokenHash");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OrganizationName).HasColumnName("organizationName");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PlanType).HasColumnName("planType");
            entity.Property(e => e.MaxMembers).HasColumnName("maxMembers");
            entity.Property(e => e.MaxMembersLite).HasColumnName("maxMembersLite");
            entity.Property(e => e.LiteSeatsUnlimited)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("liteSeatsUnlimited");
            entity.Property(e => e.MaxMembersDeveloper).HasColumnName("maxMembersDeveloper");
            entity.Property(e => e.IssuedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("issuedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.IssuedById).HasColumnName("issuedById");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedById).HasColumnName("revokedById");
            entity.Property(e => e.RevokedReason).HasColumnName("revokedReason");
            entity.Property(e => e.SupersededAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("supersededAt");
            entity.Property(e => e.ReplacesId).HasColumnName("replacesId");
            entity.Property(e => e.PendingDeliveryLicense).HasColumnName("pendingDeliveryLicense");
            entity.Property(e => e.Services)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("services");
            entity.Property(e => e.SeatRateCents).HasColumnName("seatRateCents");
            entity.Property(e => e.SeatCurrency).HasColumnName("seatCurrency");
            entity.Property(e => e.CommitUsdCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("commitUsdCents");
            entity.Property(e => e.OverageEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("overageEnabled");
            entity.Property(e => e.OverageMaxUsdCents).HasColumnName("overageMaxUsdCents");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.InstanceBoundAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("instanceBoundAt");
            entity.Property(e => e.LastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSyncAt");
            entity.Property(e => e.LastSyncVersion).HasColumnName("lastSyncVersion");
            entity.Property(e => e.ReportedMembers).HasColumnName("reportedMembers");
            entity.Property(e => e.ReportedMembersLite).HasColumnName("reportedMembersLite");
            entity.Property(e => e.VirtualKeyId).HasColumnName("virtualKeyId");
            entity.Property(e => e.SeatsRaisedFrom).HasColumnName("seatsRaisedFrom");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.IssuedLicenses)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("IssuedLicense_organizationId_fkey");
        });

        modelBuilder.Entity<OrganizationLicense>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("OrganizationLicense_pkey");

            entity.ToTable("OrganizationLicense");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.LicenseKey).HasColumnName("licenseKey");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ValidatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validatedAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ActivationCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ActivationCode_pkey");

            entity.ToTable("ActivationCode");

            entity.HasIndex(e => e.CodeHash, "ActivationCode_codeHash_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "ActivationCode_organizationId_idx");

            entity.HasIndex(e => e.ExpiresAt, "ActivationCode_expiresAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.CodeHash).HasColumnName("codeHash");
            entity.Property(e => e.CodeHint).HasColumnName("codeHint");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OrganizationName).HasColumnName("organizationName");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PlanType).HasColumnName("planType");
            entity.Property(e => e.MaxMembers).HasColumnName("maxMembers");
            entity.Property(e => e.MaxMembersLite)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("maxMembersLite");
            entity.Property(e => e.LiteSeatsUnlimited)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("liteSeatsUnlimited");
            entity.Property(e => e.MaxMembersDeveloper).HasColumnName("maxMembersDeveloper");
            entity.Property(e => e.LicenseTermDays).HasColumnName("licenseTermDays");
            entity.Property(e => e.Services)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("services");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Reusable)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("reusable");
            entity.Property(e => e.RedeemedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("redeemedAt");
            entity.Property(e => e.RedeemedByInstanceId).HasColumnName("redeemedByInstanceId");
            entity.Property(e => e.IssuedLicenseId).HasColumnName("issuedLicenseId");
            entity.Property(e => e.RedemptionCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("redemptionCount");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedById).HasColumnName("revokedById");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ConnectedBillingAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedBillingAccount_pkey");

            entity.ToTable("ConnectedBillingAccount");

            entity.HasIndex(e => e.OrganizationId, "ConnectedBillingAccount_organizationId_key").IsUnique();

            entity.HasIndex(e => e.StripeCustomerId, "ConnectedBillingAccount_stripeCustomerId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripeCustomerId");
            entity.Property(e => e.UsageSubscriptionId).HasColumnName("usageSubscriptionId");
            entity.Property(e => e.UsageSubscriptionItemId).HasColumnName("usageSubscriptionItemId");
            entity.Property(e => e.TermStartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("termStartsAt");
            entity.Property(e => e.TermEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("termEndsAt");
            entity.Property(e => e.CommitUsdCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("commitUsdCents");
            entity.Property(e => e.SeatCurrency).HasColumnName("seatCurrency");
            entity.Property(e => e.SeatRateCents).HasColumnName("seatRateCents");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.BankTransferType).HasColumnName("bankTransferType");
            entity.Property(e => e.BankTransferCountry).HasColumnName("bankTransferCountry");
            entity.Property(e => e.BillingEmail).HasColumnName("billingEmail");
            entity.Property(e => e.PendingRenewal)
                .HasColumnType("jsonb")
                .HasColumnName("pendingRenewal");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithOne(p => p.ConnectedBilling)
                .HasForeignKey<ConnectedBillingAccount>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ConnectedBillingAccount_organizationId_fkey");
        });

        modelBuilder.Entity<ConnectedCreditGrant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedCreditGrant_pkey");

            entity.ToTable("ConnectedCreditGrant");

            entity.HasIndex(e => e.StripeCreditGrantId, "ConnectedCreditGrant_stripeCreditGrantId_key").IsUnique();

            entity.HasIndex(e => e.AccountId, "ConnectedCreditGrant_accountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.StripeCreditGrantId).HasColumnName("stripeCreditGrantId");
            entity.Property(e => e.AmountUsdCents).HasColumnName("amountUsdCents");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.TermEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("termEndsAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<ConnectedInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedInvoice_pkey");

            entity.ToTable("ConnectedInvoice");

            entity.HasIndex(e => e.StripeInvoiceId, "ConnectedInvoice_stripeInvoiceId_key").IsUnique();

            entity.HasIndex(e => e.AccountId, "ConnectedInvoice_accountId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.StripeInvoiceId).HasColumnName("stripeInvoiceId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.AmountCents).HasColumnName("amountCents");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PaidOutOfBandAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("paidOutOfBandAt");
            entity.Property(e => e.TermStartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("termStartsAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ScimRequestLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimRequestLog_pkey");

            entity.ToTable("ScimRequestLog");

            entity.HasIndex(e => new { e.OrganizationId, e.OccurredAt }, "ScimRequestLog_organizationId_occurredAt_idx");

            entity.HasIndex(e => new { e.ConnectionId, e.OccurredAt }, "ScimRequestLog_connectionId_occurredAt_idx");

            entity.HasIndex(e => e.OccurredAt, "ScimRequestLog_occurredAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.OccurredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.ScimRequestLog)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScimRequestLog_organizationId_fkey");
        });

        modelBuilder.Entity<ScimToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimToken_pkey");

            entity.ToTable("ScimToken");

            entity.HasIndex(e => e.HashedToken, "ScimToken_hashedToken_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "ScimToken_organizationId_idx");

            entity.HasIndex(e => e.ConnectionId, "ScimToken_connectionId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.HashedToken).HasColumnName("hashedToken");
            entity.Property(e => e.HashScheme)
                .ValueGeneratedNever()
                .HasDefaultValue("sha256")
                .HasColumnName("hashScheme");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.ScimTokens)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScimToken_organizationId_fkey");
        });

        modelBuilder.Entity<ScimExternalId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimExternalId_pkey");

            entity.ToTable("ScimExternalId");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "ScimExternalId_organizationId_userId_idx");

            entity.HasIndex(e => e.UserId, "ScimExternalId_userId_idx");

            entity.HasIndex(e => e.ConnectionId, "ScimExternalId_connectionId_idx");

            entity.HasIndex(e => new { e.ConnectionId, e.ExternalId }, "ScimExternalId_connectionId_externalId_key").IsUnique();

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ScimExternalIds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScimExternalId_userId_fkey");
        });

        modelBuilder.Entity<ScimUserResource>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.UserId }).HasName("ScimUserResource_pkey");

            entity.ToTable("ScimUserResource");

            entity.HasIndex(e => new { e.OrganizationId, e.UserName }, "ScimUserResource_organizationId_userName_idx");

            entity.HasIndex(e => e.UserId, "ScimUserResource_userId_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserName).HasColumnName("userName");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ScimUserResources)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScimUserResource_userId_fkey");
        });

        modelBuilder.Entity<ScimDirectoryUser>(entity =>
        {
            entity.HasKey(e => new { e.ConnectionId, e.UserId }).HasName("ScimDirectoryUser_pkey");

            entity.ToTable("ScimDirectoryUser");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "ScimDirectoryUser_organizationId_userId_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<ScimSyncState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimSyncState_pkey");

            entity.ToTable("ScimSyncState");

            entity.HasIndex(e => e.OrganizationId, "ScimSyncState_organizationId_idx");

            entity.HasIndex(e => e.State, "ScimSyncState_state_idx");

            entity.HasIndex(e => e.ConnectionId, "ScimSyncState_connectionId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.LastPushedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastPushedAt");
            entity.Property(e => e.LastFailure)
                .HasColumnType("jsonb")
                .HasColumnName("lastFailure");
            entity.Property(e => e.DeadLetters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("deadLetters");
            entity.Property(e => e.RevokedCause).HasColumnName("revokedCause");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acceptedAt");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ScimSsoConnectionView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScimSsoConnectionView_pkey");

            entity.ToTable("ScimSsoConnectionView");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt }, "ScimSsoConnectionView_organizationId_createdAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Folded)
                .HasColumnType("jsonb")
                .HasColumnName("folded");
            entity.Property(e => e.AppliedEventIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("appliedEventIds");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<OrganizationSeatPeak>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("OrganizationSeatPeak_pkey");

            entity.ToTable("OrganizationSeatPeak");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OpenedAt).HasColumnName("openedAt");
            entity.Property(e => e.FullSeats).HasColumnName("fullSeats");
            entity.Property(e => e.LiteSeats).HasColumnName("liteSeats");
            entity.Property(e => e.DeveloperSeats).HasColumnName("developerSeats");
            entity.Property(e => e.LatestChangeAt).HasColumnName("latestChangeAt");
            entity.Property(e => e.Mismatches).HasColumnName("mismatches");
        });

        modelBuilder.Entity<OrganizationSeatPeakHolder>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.HolderKind, e.HolderId }).HasName("OrganizationSeatPeakHolder_pkey");

            entity.ToTable("OrganizationSeatPeakHolder");

            entity.HasIndex(e => new { e.OrganizationId, e.HolderKind, e.Seat, e.ExpiresAt }, "OrganizationSeatPeakHolder_organizationId_holderKind_seat_e_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.SuppressedBy }, "OrganizationSeatPeakHolder_organizationId_suppressedBy_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.HolderKind).HasColumnName("holderKind");
            entity.Property(e => e.HolderId).HasColumnName("holderId");
            entity.Property(e => e.Seat).HasColumnName("seat");
            entity.Property(e => e.LastChangedAt).HasColumnName("lastChangedAt");
            entity.Property(e => e.LastChangeId).HasColumnName("lastChangeId");
            entity.Property(e => e.ExpiresAt).HasColumnName("expiresAt");
            entity.Property(e => e.ExpiredByFold)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("expiredByFold");
            entity.Property(e => e.SuppressedBy).HasColumnName("suppressedBy");
            entity.Property(e => e.RestoredByFold)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("restoredByFold");
        });

        modelBuilder.Entity<OrganizationSeatPeakMonth>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.Month }).HasName("OrganizationSeatPeakMonth_pkey");

            entity.ToTable("OrganizationSeatPeakMonth");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.FullPeak).HasColumnName("fullPeak");
            entity.Property(e => e.FullPeakAt).HasColumnName("fullPeakAt");
            entity.Property(e => e.LitePeak).HasColumnName("litePeak");
            entity.Property(e => e.LitePeakAt).HasColumnName("litePeakAt");
            entity.Property(e => e.FullAtEnd).HasColumnName("fullAtEnd");
            entity.Property(e => e.LiteAtEnd).HasColumnName("liteAtEnd");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pkey");

            entity.ToTable("Project");

            entity.HasIndex(e => e.Slug, "Project_slug_key").IsUnique();

            entity.HasIndex(e => e.ApiKey, "Project_apiKey_key").IsUnique();

            entity.HasIndex(e => e.LwqlKey, "Project_lwqlKey_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "Project_teamId_idx");

            entity.HasIndex(e => e.IsPersonal, "Project_isPersonal_idx");

            entity.HasIndex(e => e.OwnerUserId, "Project_ownerUserId_idx");

            entity.HasIndex(e => new { e.TeamId, e.Kind }, "Project_teamId_kind_idx");

            entity.HasIndex(e => e.DepartmentId, "Project_departmentId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.LegacyApiKeyLastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("legacyApiKeyLastUsedAt");
            entity.Property(e => e.LwqlKey)
                .HasDefaultValueSql("(gen_random_uuid())::text")
                .HasColumnName("lwqlKey");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Framework).HasColumnName("framework");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("application")
                .HasColumnName("kind");
            entity.Property(e => e.AggregateRule)
                .HasColumnType("jsonb")
                .HasColumnName("aggregateRule");
            entity.Property(e => e.FirstMessage)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("firstMessage");
            entity.Property(e => e.Integrated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("integrated");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserLinkTemplate).HasColumnName("userLinkTemplate");
            entity.Property(e => e.TraceSharingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("traceSharingEnabled");
            entity.Property(e => e.PresenceEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("presenceEnabled");
            entity.Property(e => e.S3Endpoint).HasColumnName("s3Endpoint");
            entity.Property(e => e.S3AccessKeyId).HasColumnName("s3AccessKeyId");
            entity.Property(e => e.S3SecretAccessKey).HasColumnName("s3SecretAccessKey");
            entity.Property(e => e.S3Bucket).HasColumnName("s3Bucket");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.IsPersonal)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPersonal");
            entity.Property(e => e.OwnerUserId)
                .HasMaxLength(255)
                .HasColumnName("ownerUserId");
            entity.Property(e => e.PersonalFeatures)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("personalFeatures");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.LangyEgressAllowlist)
                .HasColumnType("jsonb")
                .HasColumnName("langyEgressAllowlist");
            entity.Property(e => e.LastCodingAgentSessionAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastCodingAgentSessionAt");
            entity.Property(e => e.LastCodingAgentPullRequestAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastCodingAgentPullRequestAt");

            entity.HasOne(d => d.Team).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Project_teamId_fkey");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.PersonalProjects)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Project_ownerUserId_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Department_pkey");

            entity.ToTable("Department");

            entity.HasIndex(e => e.OrganizationId, "Department_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
        });

        modelBuilder.Entity<DepartmentMembershipHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DepartmentMembershipHistory_pkey");

            entity.ToTable("DepartmentMembershipHistory");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId, e.ValidFrom }, "DepartmentMembershipHistory_organizationId_userId_validFrom_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.ValidFrom)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validFrom");
            entity.Property(e => e.ValidTo)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validTo");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<PlatformToolPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformToolPolicy_pkey");

            entity.ToTable("PlatformToolPolicy");

            entity.HasIndex(e => e.OrganizationId, "PlatformToolPolicy_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.ToolSlug }, "PlatformToolPolicy_organizationId_toolSlug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ToolSlug).HasColumnName("toolSlug");
            entity.Property(e => e.AllowVk)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allowVk");
            entity.Property(e => e.AllowOtelDirect)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("allowOtelDirect");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<OrganizationInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationInvite_pkey");

            entity.ToTable("OrganizationInvite");

            entity.HasIndex(e => e.InviteCode, "OrganizationInvite_inviteCode_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "OrganizationInvite_organizationId_idx");

            entity.HasIndex(e => e.RequestedBy, "OrganizationInvite_requestedBy_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.InviteCode).HasColumnName("inviteCode");
            entity.Property(e => e.Expiration)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiration");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(InviteStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.TeamIds).HasColumnName("teamIds");
            entity.Property(e => e.TeamAssignments)
                .HasColumnType("jsonb")
                .HasColumnName("teamAssignments");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.RequestedBy).HasColumnName("requestedBy");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.AcceptedByUserId).HasColumnName("acceptedByUserId");
            entity.Property(e => e.AcceptedViaIdentifierId).HasColumnName("acceptedViaIdentifierId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationInvite)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrganizationInvite_organizationId_fkey");

            entity.HasOne(d => d.RequestedByUser).WithMany(p => p.InviteRequests)
                .HasForeignKey(d => d.RequestedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OrganizationInvite_requestedBy_fkey");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Monitor_pkey");

            entity.ToTable("Monitor");

            entity.HasIndex(e => e.ExperimentId, "Monitor_experimentId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "Monitor_projectId_idx");

            entity.HasIndex(e => e.EvaluatorId, "Monitor_evaluatorId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "Monitor_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ExperimentId).HasColumnName("experimentId");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.CheckType).HasColumnName("checkType");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ExecutionMode)
                .ValueGeneratedNever()
                .HasDefaultValue(EvaluationExecutionMode.OnMessage)
                .HasColumnName("executionMode");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Preconditions)
                .HasColumnType("jsonb")
                .HasColumnName("preconditions");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.Mappings)
                .HasColumnType("jsonb")
                .HasColumnName("mappings");
            entity.Property(e => e.Sample)
                .ValueGeneratedNever()
                .HasDefaultValue(1.0)
                .HasColumnName("sample");
            entity.Property(e => e.Level)
                .ValueGeneratedNever()
                .HasDefaultValue("trace")
                .HasColumnName("level");
            entity.Property(e => e.ThreadIdleTimeout).HasColumnName("threadIdleTimeout");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.AutoPausedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("autoPausedAt");
            entity.Property(e => e.AutoPausedReason).HasColumnName("autoPausedReason");
            entity.Property(e => e.EvaluatorLabel).HasColumnName("evaluatorLabel");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Project).WithMany(p => p.Checks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Monitor_projectId_fkey");

            entity.HasOne(d => d.Experiment).WithOne(p => p.Monitor)
                .HasForeignKey<Monitor>(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Monitor_experimentId_fkey");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.Monitors)
                .HasForeignKey(d => d.EvaluatorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Monitor_evaluatorId_fkey");
        });

        modelBuilder.Entity<Cost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cost_pkey");

            entity.ToTable("Cost");

            entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId }, "Cost_referenceType_referenceId_idx");

            entity.HasIndex(e => e.CostType, "Cost_costType_idx");

            entity.HasIndex(e => e.ProjectId, "Cost_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "Cost_projectId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CostType).HasColumnName("costType");
            entity.Property(e => e.CostName).HasColumnName("costName");
            entity.Property(e => e.ReferenceType).HasColumnName("referenceType");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ExtraInfo)
                .HasColumnType("jsonb")
                .HasColumnName("extraInfo");

            entity.HasOne(d => d.Project).WithMany(p => p.Costs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Cost_projectId_fkey");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Topic_pkey");

            entity.ToTable("Topic");

            entity.HasIndex(e => e.ParentId, "Topic_parentId_idx");

            entity.HasIndex(e => e.ProjectId, "Topic_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.EmbeddingsModel).HasColumnName("embeddings_model");
            entity.Property(e => e.Centroid)
                .HasColumnType("jsonb")
                .HasColumnName("centroid");
            entity.Property(e => e.P95Distance).HasColumnName("p95Distance");
            entity.Property(e => e.AutomaticallyGenerated)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("automaticallyGenerated");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Topics)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Topic_projectId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Subtopics)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Topic_parentId_fkey");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dataset_pkey");

            entity.ToTable("Dataset");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "Dataset_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ColumnTypes)
                .HasColumnType("jsonb")
                .HasColumnName("columnTypes");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.Mapping)
                .HasColumnType("jsonb")
                .HasColumnName("mapping");
            entity.Property(e => e.UseS3)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useS3");
            entity.Property(e => e.S3RecordCount).HasColumnName("s3RecordCount");
            entity.Property(e => e.ContentLayout)
                .ValueGeneratedNever()
                .HasDefaultValue("postgres")
                .HasColumnName("contentLayout");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("ready")
                .HasColumnName("status");
            entity.Property(e => e.StatusError).HasColumnName("statusError");
            entity.Property(e => e.StagingKey).HasColumnName("stagingKey");
            entity.Property(e => e.SourceStoredObjectId).HasColumnName("sourceStoredObjectId");
            entity.Property(e => e.UploadFilename).HasColumnName("uploadFilename");
            entity.Property(e => e.RowCount).HasColumnName("rowCount");
            entity.Property(e => e.SizeBytes).HasColumnName("sizeBytes");
            entity.Property(e => e.ChunkCount).HasColumnName("chunkCount");
            entity.Property(e => e.ChunkOffsets)
                .HasColumnType("jsonb")
                .HasColumnName("chunkOffsets");

            entity.HasOne(d => d.Project).WithMany(p => p.Datasets)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Dataset_projectId_fkey");
        });

        modelBuilder.Entity<DatasetRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DatasetRecord_pkey");

            entity.ToTable("DatasetRecord");

            entity.HasIndex(e => e.DatasetId, "DatasetRecord_datasetId_idx");

            entity.HasIndex(e => e.ProjectId, "DatasetRecord_projectId_idx");

            entity.HasIndex(e => e.CreatedAt, "DatasetRecord_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.DatasetId }, "DatasetRecord_projectId_datasetId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Entry)
                .HasColumnType("jsonb")
                .HasColumnName("entry");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Dataset).WithMany(p => p.DatasetRecords)
                .HasForeignKey(d => d.DatasetId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DatasetRecord_datasetId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.DatasetRecords)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DatasetRecord_projectId_fkey");
        });

        modelBuilder.Entity<DatasetVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DatasetVersion_pkey");

            entity.ToTable("DatasetVersion");

            entity.HasIndex(e => new { e.ProjectId, e.DatasetId }, "DatasetVersion_projectId_datasetId_idx");

            entity.HasIndex(e => new { e.DatasetId, e.Version }, "DatasetVersion_datasetId_version_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Trigger).HasColumnName("trigger");
            entity.Property(e => e.ContentHash).HasColumnName("contentHash");
            entity.Property(e => e.ColumnTypes)
                .HasColumnType("jsonb")
                .HasColumnName("columnTypes");
            entity.Property(e => e.RowCount).HasColumnName("rowCount");
            entity.Property(e => e.SizeBytes).HasColumnName("sizeBytes");
            entity.Property(e => e.ChunkCount).HasColumnName("chunkCount");
            entity.Property(e => e.RestoredFromVersion).HasColumnName("restoredFromVersion");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.AuthorKind).HasColumnName("authorKind");
            entity.Property(e => e.PinnedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("pinnedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<DatasetVersionRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DatasetVersionRun_pkey");

            entity.ToTable("DatasetVersionRun");

            entity.HasIndex(e => e.VersionId, "DatasetVersionRun_versionId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.RunId, e.DatasetId }, "DatasetVersionRun_projectId_runId_datasetId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.VersionId).HasColumnName("versionId");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.ExperimentId).HasColumnName("experimentId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dashboard_pkey");

            entity.ToTable("Dashboard");

            entity.HasIndex(e => e.ProjectId, "Dashboard_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Order }, "Dashboard_projectId_order_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Scope }, "Dashboard_organizationId_scope_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Order)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("order");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue(DashboardScope.Project)
                .HasColumnName("scope");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Dashboard_projectId_fkey");
        });

        modelBuilder.Entity<DashboardFavourite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardFavourite_pkey");

            entity.ToTable("DashboardFavourite");

            entity.HasIndex(e => new { e.UserId, e.ProjectId, e.Position }, "DashboardFavourite_userId_projectId_position_idx");

            entity.HasIndex(e => e.DashboardId, "DashboardFavourite_dashboardId_idx");

            entity.HasIndex(e => new { e.UserId, e.DashboardId }, "DashboardFavourite_userId_dashboardId_key").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.ProjectId, e.TemplateId }, "DashboardFavourite_userId_projectId_templateId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<DashboardsV2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardsV2_pkey");

            entity.ToTable("DashboardsV2");

            entity.HasIndex(e => e.ProjectId, "DashboardsV2_projectId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Scope }, "DashboardsV2_organizationId_scope_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue(DashboardScope.Project)
                .HasColumnName("scope");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ExcludedOrigins)
                .HasColumnType("jsonb")
                .HasColumnName("excludedOrigins");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<DashboardWidget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardWidget_pkey");

            entity.ToTable("DashboardWidget");

            entity.HasIndex(e => new { e.ProjectId, e.DashboardId }, "DashboardWidget_projectId_dashboardId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Definition)
                .HasColumnType("jsonb")
                .HasColumnName("definition");
            entity.Property(e => e.GridColumn)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("gridColumn");
            entity.Property(e => e.GridRow)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("gridRow");
            entity.Property(e => e.ColSpan)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("colSpan");
            entity.Property(e => e.RowSpan)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("rowSpan");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<InsightProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InsightProjection_pkey");

            entity.ToTable("InsightProjection");

            entity.HasIndex(e => new { e.ProjectId, e.FiledAt }, "InsightProjection_projectId_filedAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.OwnerUserId, e.FiledAt }, "InsightProjection_projectId_ownerUserId_filedAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Tone).HasColumnName("tone");
            entity.Property(e => e.Topic).HasColumnName("topic");
            entity.Property(e => e.ValidDays).HasColumnName("validDays");
            entity.Property(e => e.Lwql).HasColumnName("lwql");
            entity.Property(e => e.SourceConversationId).HasColumnName("sourceConversationId");
            entity.Property(e => e.SourceMessageId).HasColumnName("sourceMessageId");
            entity.Property(e => e.FiledByUserId).HasColumnName("filedByUserId");
            entity.Property(e => e.FiledAt).HasColumnName("filedAt");
            entity.Property(e => e.RenewedAt).HasColumnName("renewedAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.BoardId).HasColumnName("boardId");
            entity.Property(e => e.BoardName).HasColumnName("boardName");
            entity.Property(e => e.WidgetId).HasColumnName("widgetId");
            entity.Property(e => e.WidgetName).HasColumnName("widgetName");
            entity.Property(e => e.FiledVia)
                .ValueGeneratedNever()
                .HasDefaultValue("chat")
                .HasColumnName("filedVia");
            entity.Property(e => e.ReplayStart).HasColumnName("replayStart");
            entity.Property(e => e.ReplayEnd).HasColumnName("replayEnd");
            entity.Property(e => e.ReplayGranularitySeconds).HasColumnName("replayGranularitySeconds");
            entity.Property(e => e.ReplayContext)
                .HasColumnType("jsonb")
                .HasColumnName("replayContext");
            entity.Property(e => e.OwnerUserId).HasColumnName("ownerUserId");
            entity.Property(e => e.FindingKey).HasColumnName("findingKey");
        });

        modelBuilder.Entity<InsightReaderProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InsightReaderProjection_pkey");

            entity.ToTable("InsightReaderProjection");

            entity.HasIndex(e => new { e.ProjectId, e.UserId }, "InsightReaderProjection_projectId_userId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.InsightId, e.UserId }, "InsightReaderProjection_projectId_insightId_userId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.InsightId).HasColumnName("insightId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.SeenAt).HasColumnName("seenAt");
            entity.Property(e => e.ArchivedAt).HasColumnName("archivedAt");
            entity.Property(e => e.KeptAt).HasColumnName("keptAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<InsightDailyScheduleProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InsightDailyScheduleProjection_pkey");

            entity.ToTable("InsightDailyScheduleProjection");

            entity.HasIndex(e => new { e.ProjectId, e.UserId, e.BoardKind, e.BoardId }, "InsightDailySchedule_projectId_userId_board_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.BoardKind).HasColumnName("boardKind");
            entity.Property(e => e.BoardId).HasColumnName("boardId");
            entity.Property(e => e.BoardName).HasColumnName("boardName");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue("undecided")
                .HasColumnName("state");
            entity.Property(e => e.OffReason).HasColumnName("offReason");
            entity.Property(e => e.Hour).HasColumnName("hour");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.MaxInsights).HasColumnName("maxInsights");
            entity.Property(e => e.LastRunId).HasColumnName("lastRunId");
            entity.Property(e => e.LastRunAt).HasColumnName("lastRunAt");
            entity.Property(e => e.LastRunOutcome).HasColumnName("lastRunOutcome");
            entity.Property(e => e.LastRunReason).HasColumnName("lastRunReason");
            entity.Property(e => e.LastRunFiled).HasColumnName("lastRunFiled");
            entity.Property(e => e.LastRunRenewed).HasColumnName("lastRunRenewed");
            entity.Property(e => e.LastRunConversationId).HasColumnName("lastRunConversationId");
            entity.Property(e => e.ConsecutiveSkips)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveSkips");
            entity.Property(e => e.SkipStreakSlot).HasColumnName("skipStreakSlot");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<SavedView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SavedView_pkey");

            entity.ToTable("SavedView");

            entity.HasIndex(e => e.ProjectId, "SavedView_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Order }, "SavedView_projectId_order_idx");

            entity.HasIndex(e => e.UserId, "SavedView_userId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Kind, e.Order }, "SavedView_projectId_kind_order_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Filters)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.Query).HasColumnName("query");
            entity.Property(e => e.Period)
                .HasColumnType("jsonb")
                .HasColumnName("period");
            entity.Property(e => e.Order)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("order");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("v1-traces-filter")
                .HasColumnName("kind");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.SavedViews)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SavedView_projectId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.SavedViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SavedView_userId_fkey");
        });

        modelBuilder.Entity<CustomGraph>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomGraph_pkey");

            entity.ToTable("CustomGraph");

            entity.HasIndex(e => e.ProjectId, "CustomGraph_projectId_idx");

            entity.HasIndex(e => e.DashboardId, "CustomGraph_dashboardId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Kind }, "CustomGraph_projectId_kind_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Graph)
                .HasColumnType("jsonb")
                .HasColumnName("graph");
            entity.Property(e => e.Filters)
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("builder")
                .HasColumnName("kind");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
            entity.Property(e => e.GridColumn)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("gridColumn");
            entity.Property(e => e.GridRow)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("gridRow");
            entity.Property(e => e.ColSpan)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("colSpan");
            entity.Property(e => e.RowSpan)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("rowSpan");

            entity.HasOne(d => d.Project).WithMany(p => p.CustomGraphs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("CustomGraph_projectId_fkey");

            entity.HasOne(d => d.Dashboard).WithMany(p => p.Graphs)
                .HasForeignKey(d => d.DashboardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomGraph_dashboardId_fkey");
        });

        modelBuilder.Entity<BatchEvaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchEvaluation_pkey");

            entity.ToTable("BatchEvaluation");

            entity.HasIndex(e => e.ProjectId, "BatchEvaluation_projectId_idx");

            entity.HasIndex(e => e.DatasetId, "BatchEvaluation_datasetId_idx");

            entity.HasIndex(e => e.ExperimentId, "BatchEvaluation_experimentId_idx");

            entity.HasIndex(e => e.Id, "BatchEvaluation_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ExperimentId).HasColumnName("experimentId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Data)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Passed).HasColumnName("passed");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.DatasetSlug).HasColumnName("datasetSlug");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.Evaluation).HasColumnName("evaluation");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Experiment).WithMany(p => p.BatchEvaluations)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BatchEvaluation_experimentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.BatchEvaluations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BatchEvaluation_projectId_fkey");

            entity.HasOne(d => d.Dataset).WithMany(p => p.BatchEvaluations)
                .HasForeignKey(d => d.DatasetId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BatchEvaluation_datasetId_fkey");
        });

        modelBuilder.Entity<Trigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Trigger_pkey");

            entity.ToTable("Trigger");

            entity.HasIndex(e => e.CustomGraphId, "Trigger_customGraphId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "Trigger_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.TriggerKind)
                .ValueGeneratedNever()
                .HasDefaultValue(TriggerKind.Automation)
                .HasColumnName("triggerKind");
            entity.Property(e => e.ActionParams)
                .HasColumnType("jsonb")
                .HasColumnName("actionParams");
            entity.Property(e => e.Filters)
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.FilterQuery).HasColumnName("filterQuery");
            entity.Property(e => e.LastRunAt)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("lastRunAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.PausedReason).HasColumnName("pausedReason");
            entity.Property(e => e.PausedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("pausedAt");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.AlertType).HasColumnName("alertType");
            entity.Property(e => e.SlackTemplateType).HasColumnName("slackTemplateType");
            entity.Property(e => e.SlackTemplate).HasColumnName("slackTemplate");
            entity.Property(e => e.EmailSubjectTemplate).HasColumnName("emailSubjectTemplate");
            entity.Property(e => e.EmailBodyTemplate).HasColumnName("emailBodyTemplate");
            entity.Property(e => e.NotificationCadence)
                .ValueGeneratedNever()
                .HasDefaultValue("immediate")
                .HasColumnName("notificationCadence");
            entity.Property(e => e.TraceDebounceMs)
                .ValueGeneratedNever()
                .HasDefaultValue(30000)
                .HasColumnName("traceDebounceMs");
            entity.Property(e => e.CustomGraphId).HasColumnName("customGraphId");

            entity.HasOne(d => d.Project).WithMany(p => p.Triggers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Trigger_projectId_fkey");

            entity.HasOne(d => d.CustomGraph).WithOne(p => p.Trigger)
                .HasForeignKey<Trigger>(d => d.CustomGraphId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Trigger_customGraphId_fkey");
        });

        modelBuilder.Entity<SlackIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SlackIntegration_pkey");

            entity.ToTable("SlackIntegration");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "SlackIntegration_scopeType_scopeId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.ScopeType, e.ScopeId, e.SecretFingerprint }, "SlackIntegration_organizationId_scopeType_scopeId_secretFin_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BotTokenEncrypted).HasColumnName("botTokenEncrypted");
            entity.Property(e => e.WebhookUrlEncrypted).HasColumnName("webhookUrlEncrypted");
            entity.Property(e => e.SecretFingerprint).HasColumnName("secretFingerprint");
            entity.Property(e => e.SecretHint).HasColumnName("secretHint");
            entity.Property(e => e.SlackTeamId).HasColumnName("slackTeamId");
            entity.Property(e => e.SlackTeamName).HasColumnName("slackTeamName");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SlackConnectionClaim>(entity =>
        {
            entity.HasKey(e => new { e.ConnectionId, e.ClaimantId }).HasName("slack_connection_claim_pkey");

            entity.ToTable("slack_connection_claim");

            entity.HasIndex(e => e.OrganizationId, "slack_connection_claim_organizationId_idx");

            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.ClaimantId).HasColumnName("claimantId");
            entity.Property(e => e.ClaimantLabel).HasColumnName("claimantLabel");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<WebhookEndpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebhookEndpoint_pkey");

            entity.ToTable("WebhookEndpoint");

            entity.HasIndex(e => new { e.OrganizationId, e.Status }, "WebhookEndpoint_organizationId_status_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.IdempotencyKey }, "WebhookEndpoint_organizationId_idempotencyKey_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.DestinationKind)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookDestinationKind.Http)
                .HasColumnName("destinationKind");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.SqsQueueUrl).HasColumnName("sqsQueueUrl");
            entity.Property(e => e.SqsRoleArn).HasColumnName("sqsRoleArn");
            entity.Property(e => e.SqsExternalId).HasColumnName("sqsExternalId");
            entity.Property(e => e.SqsAccessKeyId).HasColumnName("sqsAccessKeyId");
            entity.Property(e => e.SqsSecretAccessKeyEncrypted).HasColumnName("sqsSecretAccessKeyEncrypted");
            entity.Property(e => e.SecretEncrypted).HasColumnName("secretEncrypted");
            entity.Property(e => e.SignatureScheme).HasColumnName("signatureScheme");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.PreviousSecretEncrypted).HasColumnName("previousSecretEncrypted");
            entity.Property(e => e.PreviousSecretExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("previousSecretExpiresAt");
            entity.Property(e => e.EnabledEvents)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("enabledEvents");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookEndpointStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.DisabledReason).HasColumnName("disabledReason");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
            entity.Property(e => e.FailingSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failingSince");
            entity.Property(e => e.MaxBatchSize)
                .ValueGeneratedNever()
                .HasDefaultValue(100)
                .HasColumnName("maxBatchSize");
            entity.Property(e => e.MaxBatchDelayMs)
                .ValueGeneratedNever()
                .HasDefaultValue(250)
                .HasColumnName("maxBatchDelayMs");
            entity.Property(e => e.MaxInFlight)
                .ValueGeneratedNever()
                .HasDefaultValue(4)
                .HasColumnName("maxInFlight");
            entity.Property(e => e.AllowSelfSignedCertificate)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowSelfSignedCertificate");
            entity.Property(e => e.LastSuccessAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSuccessAt");
            entity.Property(e => e.LastFailureAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastFailureAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.WebhookEndpoints)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookEndpoint_organizationId_fkey");
        });

        modelBuilder.Entity<WebhookEndpointDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebhookEndpointDelivery_pkey");

            entity.ToTable("WebhookEndpointDelivery");

            entity.HasIndex(e => new { e.EndpointId, e.FiredAt }, "WebhookEndpointDelivery_endpointId_firedAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.FiredAt }, "WebhookEndpointDelivery_organizationId_firedAt_idx");

            entity.HasIndex(e => e.FiredAt, "WebhookEndpointDelivery_firedAt_idx");

            entity.HasIndex(e => new { e.EndpointId, e.DispatchId }, "WebhookEndpointDelivery_endpointId_dispatchId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TriggerId, e.FiredAt }, "WebhookEndpointDelivery_projectId_triggerId_firedAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TriggerId, e.DispatchId }, "WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Channel)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookDeliveryChannel.Platform)
                .HasColumnName("channel");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.EndpointId).HasColumnName("endpointId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TriggerId).HasColumnName("triggerId");
            entity.Property(e => e.DispatchId).HasColumnName("dispatchId");
            entity.Property(e => e.Attempt).HasColumnName("attempt");
            entity.Property(e => e.EventCount).HasColumnName("eventCount");
            entity.Property(e => e.ResponseStatus).HasColumnName("responseStatus");
            entity.Property(e => e.LatencyMs).HasColumnName("latencyMs");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.Response)
                .HasColumnType("jsonb")
                .HasColumnName("response");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.FiredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Endpoint).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.EndpointId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookEndpointDelivery_endpointId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.WebhookDeliveries)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookEndpointDelivery_projectId_fkey");

            entity.HasOne(d => d.Trigger).WithMany(p => p.WebhookDeliveries)
                .HasForeignKey(d => d.TriggerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookEndpointDelivery_triggerId_fkey");
        });

        modelBuilder.Entity<Experiment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Experiment_pkey");

            entity.ToTable("Experiment");

            entity.HasIndex(e => e.ProjectId, "Experiment_projectId_idx");

            entity.HasIndex(e => e.WorkflowId, "Experiment_workflowId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ArchivedAt }, "Experiment_projectId_archivedAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "Experiment_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.WorkbenchState)
                .HasColumnType("jsonb")
                .HasColumnName("workbenchState");
            entity.Property(e => e.WorkbenchVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("workbenchVersion");

            entity.HasOne(d => d.Project).WithMany(p => p.Experiments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Experiment_projectId_fkey");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Experiments)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Experiment_workflowId_fkey");
        });

        modelBuilder.Entity<ExperimentVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ExperimentVersion_pkey");

            entity.ToTable("ExperimentVersion");

            entity.HasIndex(e => new { e.ProjectId, e.ExperimentId, e.AutoSaved }, "ExperimentVersion_projectId_experimentId_autoSaved_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ExperimentId, e.Version }, "ExperimentVersion_projectId_experimentId_version_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ExperimentId, e.CounterVersion }, "ExperimentVersion_projectId_experimentId_counterVersion_idx");

            entity.HasIndex(e => e.CreatedAt, "ExperimentVersion_createdAt_idx");

            entity.HasIndex(e => new { e.ExperimentId, e.Version }, "ExperimentVersion_experimentId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ExperimentId).HasColumnName("experimentId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CounterVersion).HasColumnName("counterVersion");
            entity.Property(e => e.AutoSaved)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoSaved");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.AuthorLabel).HasColumnName("authorLabel");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.State)
                .HasColumnType("jsonb")
                .HasColumnName("state");
            entity.Property(e => e.SchemaVersion)
                .ValueGeneratedNever()
                .HasDefaultValue("1")
                .HasColumnName("schemaVersion");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Annotation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Annotation_pkey");

            entity.ToTable("Annotation");

            entity.HasIndex(e => e.ProjectId, "Annotation_projectId_idx");

            entity.HasIndex(e => e.TraceId, "Annotation_traceId_idx");

            entity.HasIndex(e => e.UserId, "Annotation_userId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId }, "Annotation_projectId_traceId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId, e.AnchorKind }, "Annotation_projectId_traceId_anchorKind_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.IsThumbsUp).HasColumnName("isThumbsUp");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ScoreOptions)
                .HasColumnType("jsonb")
                .HasColumnName("scoreOptions");
            entity.Property(e => e.ExpectedOutput).HasColumnName("expectedOutput");
            entity.Property(e => e.AnchorKind).HasColumnName("anchorKind");
            entity.Property(e => e.AnchorId).HasColumnName("anchorId");
            entity.Property(e => e.AnchorPath).HasColumnName("anchorPath");

            entity.HasOne(d => d.Project).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Annotation_projectId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Annotation)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Annotation_userId_fkey");
        });

        modelBuilder.Entity<ModelProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModelProvider_pkey");

            entity.ToTable("ModelProvider");

            entity.HasIndex(e => e.HealthStatus, "ModelProvider_healthStatus_idx");

            entity.HasIndex(e => e.FallbackPriorityGlobal, "ModelProvider_fallbackPriorityGlobal_idx");

            entity.HasIndex(e => e.OrganizationId, "ModelProvider_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.RoutingHandle }, "ModelProvider_organizationId_routingHandle_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.RoutingHandle).HasColumnName("routingHandle");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.CustomKeys)
                .HasColumnType("jsonb")
                .HasColumnName("customKeys");
            entity.Property(e => e.ExtraHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("extraHeaders");
            entity.Property(e => e.CustomModels)
                .HasColumnType("jsonb")
                .HasColumnName("customModels");
            entity.Property(e => e.CustomEmbeddingsModels)
                .HasColumnType("jsonb")
                .HasColumnName("customEmbeddingsModels");
            entity.Property(e => e.DeploymentMapping)
                .HasColumnType("jsonb")
                .HasColumnName("deploymentMapping");
            entity.Property(e => e.RateLimitRpm).HasColumnName("rateLimitRpm");
            entity.Property(e => e.RateLimitTpm).HasColumnName("rateLimitTpm");
            entity.Property(e => e.RateLimitRpd).HasColumnName("rateLimitRpd");
            entity.Property(e => e.RotationPolicy)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayProviderRotationPolicy.Manual)
                .HasColumnName("rotationPolicy");
            entity.Property(e => e.ProviderConfig)
                .HasColumnType("jsonb")
                .HasColumnName("providerConfig");
            entity.Property(e => e.FallbackPriorityGlobal).HasColumnName("fallbackPriorityGlobal");
            entity.Property(e => e.LangySkipPermissionsModels)
                .HasColumnType("jsonb")
                .HasColumnName("langySkipPermissionsModels");
            entity.Property(e => e.HealthStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayProviderHealthStatus.Unknown)
                .HasColumnName("healthStatus");
            entity.Property(e => e.CircuitOpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("circuitOpenedAt");
            entity.Property(e => e.LastHealthCheckAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastHealthCheckAt");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
        });

        modelBuilder.Entity<ModelProviderScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModelProviderScope_pkey");

            entity.ToTable("ModelProviderScope");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "ModelProviderScope_scopeType_scopeId_idx");

            entity.HasIndex(e => e.ModelProviderId, "ModelProviderScope_modelProviderId_idx");

            entity.HasIndex(e => new { e.ModelProviderId, e.ScopeType, e.ScopeId }, "ModelProviderScope_modelProviderId_scopeType_scopeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ModelProviderId).HasColumnName("modelProviderId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.ModelProvider).WithMany(p => p.Scopes)
                .HasForeignKey(d => d.ModelProviderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ModelProviderScope_modelProviderId_fkey");
        });

        modelBuilder.Entity<ModelDefaultConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModelDefaultConfig_pkey");

            entity.ToTable("ModelDefaultConfig");

            entity.HasIndex(e => e.OrganizationId, "ModelDefaultConfig_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
        });

        modelBuilder.Entity<ModelDefaultConfigScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModelDefaultConfigScope_pkey");

            entity.ToTable("ModelDefaultConfigScope");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "ModelDefaultConfigScope_scopeType_scopeId_idx");

            entity.HasIndex(e => e.ConfigId, "ModelDefaultConfigScope_configId_idx");

            entity.HasIndex(e => new { e.ConfigId, e.ScopeType, e.ScopeId }, "ModelDefaultConfigScope_configId_scopeType_scopeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ConfigId).HasColumnName("configId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Config).WithMany(p => p.Scopes)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ModelDefaultConfigScope_configId_fkey");
        });

        modelBuilder.Entity<GithubInstallation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GithubInstallation_pkey");

            entity.ToTable("GithubInstallation");

            entity.HasIndex(e => e.InstallationId, "GithubInstallation_installationId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "GithubInstallation_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.InstallationId).HasColumnName("installationId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.AccountLogin).HasColumnName("accountLogin");
            entity.Property(e => e.AccountType).HasColumnName("accountType");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.RepositorySelection).HasColumnName("repositorySelection");
            entity.Property(e => e.Repositories)
                .HasColumnType("jsonb")
                .HasColumnName("repositories");
            entity.Property(e => e.SuspendedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("suspendedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.GithubInstallations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GithubInstallation_organizationId_fkey");
        });

        modelBuilder.Entity<GithubPullRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GithubPullRequest_pkey");

            entity.ToTable("GithubPullRequest");

            entity.HasIndex(e => new { e.OrganizationId, e.RepositoryHost, e.RepositoryFullName, e.HeadBranch }, "GithubPullRequest_organizationId_repositoryHost_repositoryF_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.RepositoryHost, e.RepositoryFullName, e.PrNumber }, "GithubPullRequest_organizationId_repositoryHost_repositoryF_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.RepositoryHost).HasColumnName("repositoryHost");
            entity.Property(e => e.RepositoryFullName).HasColumnName("repositoryFullName");
            entity.Property(e => e.HeadBranch).HasColumnName("headBranch");
            entity.Property(e => e.PrNumber).HasColumnName("prNumber");
            entity.Property(e => e.HtmlUrl).HasColumnName("htmlUrl");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.IsDraft)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDraft");
            entity.Property(e => e.AuthorLogin).HasColumnName("authorLogin");
            entity.Property(e => e.PrCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("prCreatedAt");
            entity.Property(e => e.PrClosedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("prClosedAt");
            entity.Property(e => e.PrMergedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("prMergedAt");
            entity.Property(e => e.PrUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("prUpdatedAt");
            entity.Property(e => e.MappedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("mappedAt");
            entity.Property(e => e.LastCheckedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastCheckedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.GithubPullRequests)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GithubPullRequest_organizationId_fkey");
        });

        modelBuilder.Entity<GithubBranchPullRequestCheck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GithubBranchPullRequestCheck_pkey");

            entity.ToTable("GithubBranchPullRequestCheck");

            entity.HasIndex(e => new { e.RecheckAfter, e.LastRequestedAt, e.NotFoundAt }, "GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_n_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.RepositoryHost, e.RepositoryFullName, e.HeadBranch }, "GithubBranchPullRequestCheck_organizationId_repositoryHost__key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.RepositoryHost).HasColumnName("repositoryHost");
            entity.Property(e => e.RepositoryFullName).HasColumnName("repositoryFullName");
            entity.Property(e => e.HeadBranch).HasColumnName("headBranch");
            entity.Property(e => e.LastCheckedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastCheckedAt");
            entity.Property(e => e.PrCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("prCount");
            entity.Property(e => e.NotFoundAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("notFoundAt");
            entity.Property(e => e.RecheckAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("recheckAfter");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.LastRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastRequestedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.GithubBranchPrChecks)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GithubBranchPullRequestCheck_organizationId_fkey");
        });

        modelBuilder.Entity<LangyConversationProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LangyConversationProjection_pkey");

            entity.ToTable("LangyConversationProjection");

            entity.HasIndex(e => new { e.ProjectId, e.UserId, e.ConversationId }, "LangyConversationProjection_projectId_userId_conversationId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.UserId, e.LastActivityAt }, "LangyConversationProjection_projectId_userId_lastActivityAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId }, "LangyConversationProjection_projectId_conversationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TitleSource).HasColumnName("titleSource");
            entity.Property(e => e.Origin)
                .ValueGeneratedNever()
                .HasDefaultValue("interactive")
                .HasColumnName("origin");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.IsShared)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isShared");
            entity.Property(e => e.SharedAt).HasColumnName("sharedAt");
            entity.Property(e => e.SharedById).HasColumnName("sharedById");
            entity.Property(e => e.MessageCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("messageCount");
            entity.Property(e => e.LastActivityAt).HasColumnName("lastActivityAt");
            entity.Property(e => e.CurrentTurnId).HasColumnName("currentTurnId");
            entity.Property(e => e.LastError).HasColumnName("lastError");
            entity.Property(e => e.LastModel).HasColumnName("lastModel");
            entity.Property(e => e.PendingHandoffToken).HasColumnName("pendingHandoffToken");
            entity.Property(e => e.PendingHandoffTurnId).HasColumnName("pendingHandoffTurnId");
            entity.Property(e => e.RunToken).HasColumnName("runToken");
            entity.Property(e => e.ArchivedAt).HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<LangyTurnRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LangyTurnRequest_pkey");

            entity.ToTable("LangyTurnRequest");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId }, "LangyTurnRequest_projectId_conversationId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.UserId, e.RequestId }, "LangyTurnRequest_projectId_userId_requestId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.TurnId).HasColumnName("turnId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.LeaseOwner).HasColumnName("leaseOwner");
            entity.Property(e => e.LeaseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("leaseExpiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<LangyActiveTurn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LangyActiveTurn_pkey");

            entity.ToTable("LangyActiveTurn");

            entity.HasIndex(e => new { e.ProjectId, e.UserId }, "LangyActiveTurn_projectId_userId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId }, "LangyActiveTurn_projectId_conversationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.TurnId).HasColumnName("turnId");
            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.LeaseOwner).HasColumnName("leaseOwner");
            entity.Property(e => e.LeaseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("leaseExpiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<LangyConversationTurnProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LangyConversationTurnProjection_pkey");

            entity.ToTable("LangyConversationTurnProjection");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId, e.UpdatedAt }, "LangyConversationTurnProjection_projectId_conversationId_up_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId, e.TurnId }, "LangyConversationTurnProjection_projectId_conversationId_tu_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.TurnId).HasColumnName("turnId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.QuestionParts)
                .HasColumnType("jsonb")
                .HasColumnName("questionParts");
            entity.Property(e => e.AnswerParts)
                .HasColumnType("jsonb")
                .HasColumnName("answerParts");
            entity.Property(e => e.ToolCalls)
                .HasColumnType("jsonb")
                .HasColumnName("toolCalls");
            entity.Property(e => e.Plan)
                .HasColumnType("jsonb")
                .HasColumnName("plan");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.StartedAt).HasColumnName("startedAt");
            entity.Property(e => e.EndedAt).HasColumnName("endedAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<LangyMessageProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LangyMessageProjection_pkey");

            entity.ToTable("LangyMessageProjection");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId, e.CreatedAt }, "LangyMessageProjection_projectId_conversationId_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId, e.MessageId }, "LangyMessageProjection_projectId_conversationId_messageId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.SourceEventId }, "LangyMessageProjection_projectId_sourceEventId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Parts)
                .HasColumnType("jsonb")
                .HasColumnName("parts");
            entity.Property(e => e.SourceEventId).HasColumnName("sourceEventId");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ProjectSecret>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectSecret_pkey");

            entity.ToTable("ProjectSecret");

            entity.HasIndex(e => e.ProjectId, "ProjectSecret_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "ProjectSecret_projectId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.EncryptedValue).HasColumnName("encryptedValue");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectSecrets)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProjectSecret_projectId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.SecretsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProjectSecret_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.SecretsUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProjectSecret_updatedById_fkey");
        });

        modelBuilder.Entity<TriggerSent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TriggerSent_pkey");

            entity.ToTable("TriggerSent");

            entity.HasIndex(e => e.OpenIncidentKey, "TriggerSent_openIncidentKey_key").IsUnique();

            entity.HasIndex(e => e.TriggerId, "TriggerSent_triggerId_idx");

            entity.HasIndex(e => e.ProjectId, "TriggerSent_projectId_idx");

            entity.HasIndex(e => e.CustomGraphId, "TriggerSent_customGraphId_idx");

            entity.HasIndex(e => e.ResolvedAt, "TriggerSent_resolvedAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TriggerId, e.CreatedAt, e.Id }, "TriggerSent_projectId_triggerId_createdAt_id_idx");

            entity.HasIndex(e => new { e.TriggerId, e.TraceId }, "TriggerSent_triggerId_traceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.TriggerId).HasColumnName("triggerId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.CustomGraphId).HasColumnName("customGraphId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");
            entity.Property(e => e.OpenIncidentKey).HasColumnName("openIncidentKey");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.CustomGraph).WithMany(p => p.TriggerSent)
                .HasForeignKey(d => d.CustomGraphId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TriggerSent_customGraphId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.TriggerSent)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TriggerSent_projectId_fkey");

            entity.HasOne(d => d.Trigger).WithMany(p => p.TriggerSent)
                .HasForeignKey(d => d.TriggerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TriggerSent_triggerId_fkey");
        });

        modelBuilder.Entity<TriggerLatestEvaluation>(entity =>
        {
            entity.HasKey(e => e.TriggerId).HasName("TriggerLatestEvaluation_pkey");

            entity.ToTable("TriggerLatestEvaluation");

            entity.HasIndex(e => e.ProjectId, "TriggerLatestEvaluation_projectId_idx");

            entity.Property(e => e.TriggerId).HasColumnName("triggerId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EvaluatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("evaluatedAt");
            entity.Property(e => e.Verdict).HasColumnName("verdict");
            entity.Property(e => e.ObservedValue).HasColumnName("observedValue");
            entity.Property(e => e.Threshold).HasColumnName("threshold");
            entity.Property(e => e.Operator).HasColumnName("operator");
            entity.Property(e => e.TimePeriodMinutes).HasColumnName("timePeriodMinutes");
            entity.Property(e => e.SkipCode).HasColumnName("skipCode");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Trigger).WithOne(p => p.LatestEvaluation)
                .HasForeignKey<TriggerLatestEvaluation>(d => d.TriggerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TriggerLatestEvaluation_triggerId_fkey");
        });

        modelBuilder.Entity<EmailSuppression>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailSuppression_pkey");

            entity.ToTable("EmailSuppression");

            entity.HasIndex(e => e.ProjectId, "EmailSuppression_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Email, e.TriggerId }, "EmailSuppression_projectId_email_triggerId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.TriggerId).HasColumnName("triggerId");
            entity.Property(e => e.Reason)
                .ValueGeneratedNever()
                .HasDefaultValue("unsubscribe")
                .HasColumnName("reason");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.EmailSuppressions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("EmailSuppression_projectId_fkey");
        });

        modelBuilder.Entity<AnnotationScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnnotationScore_pkey");

            entity.ToTable("AnnotationScore");

            entity.HasIndex(e => e.ProjectId, "AnnotationScore_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.DataType).HasColumnName("dataType");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
            entity.Property(e => e.DefaultValue)
                .HasColumnType("jsonb")
                .HasColumnName("defaultValue");
            entity.Property(e => e.Global)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("global");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationScores)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationScore_projectId_fkey");
        });

        modelBuilder.Entity<AnnotationQueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnnotationQueue_pkey");

            entity.ToTable("AnnotationQueue");

            entity.HasIndex(e => e.ProjectId, "AnnotationQueue_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "AnnotationQueue_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationQueue)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueue_projectId_fkey");
        });

        modelBuilder.Entity<AnnotationQueueMembers>(entity =>
        {
            entity.HasKey(e => new { e.AnnotationQueueId, e.UserId }).HasName("AnnotationQueueMembers_pkey");

            entity.ToTable("AnnotationQueueMembers");

            entity.HasIndex(e => e.AnnotationQueueId, "AnnotationQueueMembers_annotationQueueId_idx");

            entity.HasIndex(e => e.UserId, "AnnotationQueueMembers_userId_idx");

            entity.Property(e => e.AnnotationQueueId).HasColumnName("annotationQueueId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.AnnotationQueues)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueueMembers_userId_fkey");

            entity.HasOne(d => d.AnnotationQueue).WithMany(p => p.Members)
                .HasForeignKey(d => d.AnnotationQueueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueueMembers_annotationQueueId_fkey");
        });

        modelBuilder.Entity<AnnotationQueueScores>(entity =>
        {
            entity.HasKey(e => new { e.AnnotationQueueId, e.AnnotationScoreId }).HasName("AnnotationQueueScores_pkey");

            entity.ToTable("AnnotationQueueScores");

            entity.HasIndex(e => e.AnnotationQueueId, "AnnotationQueueScores_annotationQueueId_idx");

            entity.HasIndex(e => e.AnnotationScoreId, "AnnotationQueueScores_annotationScoreId_idx");

            entity.Property(e => e.AnnotationQueueId).HasColumnName("annotationQueueId");
            entity.Property(e => e.AnnotationScoreId).HasColumnName("annotationScoreId");

            entity.HasOne(d => d.AnnotationScore).WithMany(p => p.AnnotationQueueScores)
                .HasForeignKey(d => d.AnnotationScoreId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueueScores_annotationScoreId_fkey");

            entity.HasOne(d => d.AnnotationQueue).WithMany(p => p.AnnotationQueueScores)
                .HasForeignKey(d => d.AnnotationQueueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueueScores_annotationQueueId_fkey");
        });

        modelBuilder.Entity<AnnotationQueueItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnnotationQueueItem_pkey");

            entity.ToTable("AnnotationQueueItem");

            entity.HasIndex(e => e.AnnotationQueueId, "AnnotationQueueItem_annotationQueueId_idx");

            entity.HasIndex(e => e.UserId, "AnnotationQueueItem_userId_idx");

            entity.HasIndex(e => e.ProjectId, "AnnotationQueueItem_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.DoneAt }, "AnnotationQueueItem_projectId_doneAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.UserId, e.DoneAt }, "AnnotationQueueItem_projectId_userId_doneAt_idx");

            entity.HasIndex(e => new { e.AnnotationQueueId, e.DoneAt }, "AnnotationQueueItem_annotationQueueId_doneAt_idx");

            entity.HasIndex(e => new { e.TraceId, e.UserId, e.ProjectId }, "AnnotationQueueItem_traceId_userId_projectId_key").IsUnique();

            entity.HasIndex(e => new { e.TraceId, e.AnnotationQueueId, e.ProjectId }, "AnnotationQueueItem_traceId_annotationQueueId_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.AnnotationQueueId).HasColumnName("annotationQueueId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.DoneAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("doneAt");
            entity.Property(e => e.MarkedForDatasetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("markedForDatasetAt");

            entity.HasOne(d => d.AnnotationQueue).WithMany(p => p.AnnotationQueueItems)
                .HasForeignKey(d => d.AnnotationQueueId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AnnotationQueueItem_annotationQueueId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AssignedQueueItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AnnotationQueueItem_userId_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.CreatedQueueItems)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AnnotationQueueItem_createdByUserId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationQueueItem)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AnnotationQueueItem_projectId_fkey");
        });

        modelBuilder.Entity<ShareLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ShareLink_pkey");

            entity.ToTable("ShareLink");

            entity.HasIndex(e => e.Token, "ShareLink_token_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.ResourceType, e.ResourceId }, "ShareLink_projectId_resourceType_resourceId_idx");

            entity.HasIndex(e => e.UserId, "ShareLink_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ResourceType).HasColumnName("resourceType");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Visibility)
                .ValueGeneratedNever()
                .HasDefaultValue(ShareVisibility.Public)
                .HasColumnName("visibility");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.MaxViews).HasColumnName("maxViews");
            entity.Property(e => e.ViewCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("viewCount");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.ShareLinks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ShareLink_projectId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ShareLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ShareLink_userId_fkey");
        });

        modelBuilder.Entity<PinnedTrace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PinnedTrace_pkey");

            entity.ToTable("PinnedTrace");

            entity.HasIndex(e => e.ProjectId, "PinnedTrace_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId }, "PinnedTrace_projectId_traceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue(PinSource.Manual)
                .HasColumnName("source");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.PinnedTraces)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("PinnedTrace_projectId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PinnedTraces)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PinnedTrace_userId_fkey");
        });

        modelBuilder.Entity<TraceEditOverlay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TraceEditOverlay_pkey");

            entity.ToTable("TraceEditOverlay");

            entity.HasIndex(e => e.ProjectId, "TraceEditOverlay_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId }, "TraceEditOverlay_projectId_traceId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.Patch)
                .HasColumnType("jsonb")
                .HasColumnName("patch");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.TraceEditOverlays)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TraceEditOverlay_projectId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.TraceEditOverlaysCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TraceEditOverlay_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.TraceEditOverlaysUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TraceEditOverlay_updatedById_fkey");
        });

        modelBuilder.Entity<TraceIngestSourceBilling>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.SourceType }).HasName("TraceIngestSourceBilling_pkey");

            entity.ToTable("TraceIngestSourceBilling");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.Billed).HasColumnName("billed");
            entity.Property(e => e.RecordedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("recordedAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<RetentionPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionPolicy_pkey");

            entity.ToTable("RetentionPolicy");

            entity.HasIndex(e => e.OrganizationId, "RetentionPolicy_organizationId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "RetentionPolicy_scopeType_scopeId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId, e.Category }, "RetentionPolicy_scopeType_scopeId_category_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.RetentionDays).HasColumnName("retentionDays");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<DataPrivacyPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataPrivacyPolicy_pkey");

            entity.ToTable("DataPrivacyPolicy");

            entity.HasIndex(e => e.OrganizationId, "DataPrivacyPolicy_organizationId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "DataPrivacyPolicy_scopeType_scopeId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId, e.PersonalOnly }, "DataPrivacyPolicy_scopeType_scopeId_personalOnly_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.PersonalOnly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("personalOnly");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<InstantEvalJudgeProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("InstantEvalJudgeProject_pkey");

            entity.ToTable("InstantEvalJudgeProject");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<InstantEvalJudgeUsageBilling>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("InstantEvalJudgeUsageBilling_pkey");

            entity.ToTable("InstantEvalJudgeUsageBilling");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UsageBilled).HasColumnName("usageBilled");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.FromCatchUp).HasColumnName("fromCatchUp");
        });

        modelBuilder.Entity<InstantEvalJudgeSpend>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.RequestId }).HasName("InstantEvalJudgeSpend_pkey");

            entity.ToTable("InstantEvalJudgeSpend");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.SpendNanoUsd).HasColumnName("spendNanoUsd");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
        });

        modelBuilder.Entity<CustomLLMModelCost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomLLMModelCost_pkey");

            entity.ToTable("CustomLLMModelCost");

            entity.HasIndex(e => e.OrganizationId, "CustomLLMModelCost_organizationId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "CustomLLMModelCost_scopeType_scopeId_idx");

            entity.HasIndex(e => e.ProjectId, "CustomLLMModelCost_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ScopeType)
                .ValueGeneratedNever()
                .HasDefaultValue(CustomLLMModelCostScopeType.Project)
                .HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Regex).HasColumnName("regex");
            entity.Property(e => e.InputCostPerToken).HasColumnName("inputCostPerToken");
            entity.Property(e => e.OutputCostPerToken).HasColumnName("outputCostPerToken");
            entity.Property(e => e.CacheReadCostPerToken).HasColumnName("cacheReadCostPerToken");
            entity.Property(e => e.CacheCreationCostPerToken).HasColumnName("cacheCreationCostPerToken");
            entity.Property(e => e.CacheCreation1hCostPerToken).HasColumnName("cacheCreation1hCostPerToken");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workflow_pkey");

            entity.ToTable("Workflow");

            entity.HasIndex(e => e.ProjectId, "Workflow_projectId_idx");

            entity.HasIndex(e => e.PublishedById, "Workflow_publishedById_idx");

            entity.HasIndex(e => e.LatestVersionId, "Workflow_latestVersionId_idx");

            entity.HasIndex(e => e.CurrentVersionId, "Workflow_currentVersionId_idx");

            entity.HasIndex(e => e.CopiedFromWorkflowId, "Workflow_copiedFromWorkflowId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LatestVersionId).HasColumnName("latestVersionId");
            entity.Property(e => e.CurrentVersionId).HasColumnName("currentVersionId");
            entity.Property(e => e.PublishedId).HasColumnName("publishedId");
            entity.Property(e => e.PublishedById).HasColumnName("publishedById");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.IsEvaluator)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isEvaluator");
            entity.Property(e => e.IsComponent)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isComponent");
            entity.Property(e => e.CopiedFromWorkflowId).HasColumnName("copiedFromWorkflowId");

            entity.HasOne(d => d.Project).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Workflow_projectId_fkey");

            entity.HasOne(d => d.LatestVersion).WithMany(p => p.WorkflowAsLatestVersion)
                .HasForeignKey(d => d.LatestVersionId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Workflow_latestVersionId_fkey");

            entity.HasOne(d => d.CurrentVersion).WithMany(p => p.WorkflowAsCurrentVersion)
                .HasForeignKey(d => d.CurrentVersionId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Workflow_currentVersionId_fkey");

            entity.HasOne(d => d.PublishedBy).WithMany(p => p.Workflow)
                .HasForeignKey(d => d.PublishedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Workflow_publishedById_fkey");

            entity.HasOne(d => d.CopiedFrom).WithMany(p => p.CopiedWorkflows)
                .HasForeignKey(d => d.CopiedFromWorkflowId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Workflow_copiedFromWorkflowId_fkey");
        });

        modelBuilder.Entity<WorkflowVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowVersion_pkey");

            entity.ToTable("WorkflowVersion");

            entity.HasIndex(e => e.WorkflowId, "WorkflowVersion_workflowId_idx");

            entity.HasIndex(e => e.ProjectId, "WorkflowVersion_projectId_idx");

            entity.HasIndex(e => e.ParentId, "WorkflowVersion_parentId_idx");

            entity.HasIndex(e => e.AuthorId, "WorkflowVersion_authorId_idx");

            entity.HasIndex(e => e.CreatedAt, "WorkflowVersion_createdAt_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.Version }, "WorkflowVersion_workflowId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.AutoSaved)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("autoSaved");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Dsl)
                .HasColumnType("jsonb")
                .HasColumnName("dsl");

            entity.HasOne(d => d.Author).WithMany(p => p.WorkflowVersion)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkflowVersion_authorId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.WorkflowVersion)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkflowVersion_projectId_fkey");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Versions)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkflowVersion_workflowId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("WorkflowVersion_parentId_fkey");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuditLog_pkey");

            entity.ToTable("AuditLog");

            entity.HasIndex(e => e.IdempotencyKey, "AuditLog_idempotencyKey_key").IsUnique();

            entity.HasIndex(e => e.CreatedAt, "AuditLog_createdAt_idx");

            entity.HasIndex(e => e.UserId, "AuditLog_userId_idx");

            entity.HasIndex(e => e.ActorUserId, "AuditLog_actorUserId_idx");

            entity.HasIndex(e => e.ProjectId, "AuditLog_projectId_idx");

            entity.HasIndex(e => e.Action, "AuditLog_action_idx");

            entity.HasIndex(e => e.IpAddress, "AuditLog_ipAddress_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt }, "AuditLog_organizationId_createdAt_idx");

            entity.HasIndex(e => new { e.TargetKind, e.TargetId }, "AuditLog_targetKind_targetId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Args)
                .HasColumnType("jsonb")
                .HasColumnName("args");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.ActorUserId).HasColumnName("actorUserId");
            entity.Property(e => e.TargetKind).HasColumnName("targetKind");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.Before)
                .HasColumnType("jsonb")
                .HasColumnName("before");
            entity.Property(e => e.After)
                .HasColumnType("jsonb")
                .HasColumnName("after");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
        });

        modelBuilder.Entity<LlmPromptConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LlmPromptConfig_pkey");

            entity.ToTable("LlmPromptConfig");

            entity.HasIndex(e => e.ProjectId, "LlmPromptConfig_projectId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Scope }, "LlmPromptConfig_organizationId_scope_idx");

            entity.HasIndex(e => e.CopiedFromPromptId, "LlmPromptConfig_copiedFromPromptId_idx");

            entity.HasIndex(e => e.Handle, "LlmPromptConfig_handle_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Handle).HasColumnName("handle");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue(PromptScope.Project)
                .HasColumnName("scope");
            entity.Property(e => e.CopiedFromPromptId).HasColumnName("copiedFromPromptId");

            entity.HasOne(d => d.Project).WithMany(p => p.LlmPromptConfigs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LlmPromptConfig_projectId_fkey");

            entity.HasOne(d => d.CopiedFrom).WithMany(p => p.CopiedPrompts)
                .HasForeignKey(d => d.CopiedFromPromptId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("LlmPromptConfig_copiedFromPromptId_fkey");
        });

        modelBuilder.Entity<LlmPromptConfigVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LlmPromptConfigVersion_pkey");

            entity.ToTable("LlmPromptConfigVersion");

            entity.HasIndex(e => e.ConfigId, "LlmPromptConfigVersion_configId_idx");

            entity.HasIndex(e => e.AuthorId, "LlmPromptConfigVersion_authorId_idx");

            entity.HasIndex(e => e.CreatedAt, "LlmPromptConfigVersion_createdAt_idx");

            entity.HasIndex(e => new { e.ConfigId, e.Version }, "LlmPromptConfigVersion_configId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.ConfigId).HasColumnName("configId");
            entity.Property(e => e.ConfigData)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.SchemaVersion).HasColumnName("schemaVersion");
            entity.Property(e => e.RuntimeParameters)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("runtimeParameters");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");

            entity.HasOne(d => d.Author).WithMany(p => p.LlmPromptConfigVersions)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LlmPromptConfigVersion_authorId_fkey");

            entity.HasOne(d => d.Config).WithMany(p => p.Versions)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LlmPromptConfigVersion_configId_fkey");
        });

        modelBuilder.Entity<PromptTagAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PromptTagAssignment_pkey");

            entity.ToTable("PromptTagAssignment");

            entity.HasIndex(e => e.ConfigId, "PromptTagAssignment_configId_idx");

            entity.HasIndex(e => e.VersionId, "PromptTagAssignment_versionId_idx");

            entity.HasIndex(e => e.ProjectId, "PromptTagAssignment_projectId_idx");

            entity.HasIndex(e => e.TagId, "PromptTagAssignment_tagId_idx");

            entity.HasIndex(e => new { e.ConfigId, e.TagId }, "PromptTagAssignment_configId_tagId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ConfigId).HasColumnName("configId");
            entity.Property(e => e.VersionId).HasColumnName("versionId");
            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Config).WithMany(p => p.VersionTags)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PromptTagAssignment_configId_fkey");

            entity.HasOne(d => d.Version).WithMany(p => p.VersionTags)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PromptTagAssignment_versionId_fkey");

            entity.HasOne(d => d.PromptTag).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PromptTagAssignment_tagId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.VersionTagsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PromptTagAssignment_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.VersionTagsUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PromptTagAssignment_updatedById_fkey");
        });

        modelBuilder.Entity<PromptTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PromptTag_pkey");

            entity.ToTable("PromptTag");

            entity.HasIndex(e => e.OrganizationId, "PromptTag_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "PromptTag_organizationId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.PromptTags)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PromptTag_organizationId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.PromptTagsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PromptTag_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.PromptTagsUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PromptTag_updatedById_fkey");
        });

        modelBuilder.Entity<Analytics>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Analytics_pkey");

            entity.ToTable("Analytics");

            entity.HasIndex(e => new { e.CreatedAt, e.Key, e.ProjectId, e.NumericValue }, "Analytics_createdAt_key_projectId_numericValue_idx");

            entity.HasIndex(e => e.ProjectId, "Analytics_projectId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
            entity.Property(e => e.NumericValue).HasColumnName("numericValue");
            entity.Property(e => e.StringValue).HasColumnName("stringValue");
            entity.Property(e => e.BoolValue).HasColumnName("boolValue");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Analytics)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Analytics_projectId_fkey");
        });

        modelBuilder.Entity<CustomRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomRole_pkey");

            entity.ToTable("CustomRole");

            entity.HasIndex(e => e.OrganizationId, "CustomRole_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "CustomRole_organizationId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Permissions)
                .HasColumnType("jsonb")
                .HasColumnName("permissions");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("custom")
                .HasColumnName("kind");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.CustomRoles)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("CustomRole_organizationId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.HasIndex(e => e.OrganizationId, "Notification_organizationId_idx");

            entity.HasIndex(e => e.ProjectId, "Notification_projectId_idx");

            entity.HasIndex(e => e.CreatedAt, "Notification_createdAt_idx");

            entity.HasIndex(e => e.SentAt, "Notification_sentAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Notification_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Notification_projectId_fkey");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Agent_pkey");

            entity.ToTable("Agent");

            entity.HasIndex(e => e.ProjectId, "Agent_projectId_idx");

            entity.HasIndex(e => e.WorkflowId, "Agent_workflowId_idx");

            entity.HasIndex(e => e.CopiedFromAgentId, "Agent_copiedFromAgentId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.LastSeenAt }, "Agent_projectId_lastSeenAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.IdentityKey }, "Agent_projectId_identityKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.CopiedFromAgentId).HasColumnName("copiedFromAgentId");
            entity.Property(e => e.Environment).HasColumnName("environment");
            entity.Property(e => e.OwnerUserId).HasColumnName("ownerUserId");
            entity.Property(e => e.HostLabel).HasColumnName("hostLabel");
            entity.Property(e => e.IdentityKey).HasColumnName("identityKey");
            entity.Property(e => e.LastSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Project).WithMany(p => p.Agents)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Agent_projectId_fkey");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Agents)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Agent_workflowId_fkey");

            entity.HasOne(d => d.CopiedFrom).WithMany(p => p.CopiedAgents)
                .HasForeignKey(d => d.CopiedFromAgentId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Agent_copiedFromAgentId_fkey");
        });

        modelBuilder.Entity<AgentVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AgentVersion_pkey");

            entity.ToTable("AgentVersion");

            entity.HasIndex(e => e.ProjectId, "AgentVersion_projectId_idx");

            entity.HasIndex(e => new { e.AgentId, e.Version }, "AgentVersion_agentId_version_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.AuthorKind).HasColumnName("authorKind");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<AgentVersionLabel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AgentVersionLabel_pkey");

            entity.ToTable("AgentVersionLabel");

            entity.HasIndex(e => e.ProjectId, "AgentVersionLabel_projectId_idx");

            entity.HasIndex(e => e.VersionId, "AgentVersionLabel_versionId_idx");

            entity.HasIndex(e => new { e.AgentId, e.Label }, "AgentVersionLabel_agentId_label_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.VersionId).HasColumnName("versionId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Evaluator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Evaluator_pkey");

            entity.ToTable("Evaluator");

            entity.HasIndex(e => e.ProjectId, "Evaluator_projectId_idx");

            entity.HasIndex(e => e.WorkflowId, "Evaluator_workflowId_idx");

            entity.HasIndex(e => e.CopiedFromEvaluatorId, "Evaluator_copiedFromEvaluatorId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "Evaluator_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.CopiedFromEvaluatorId).HasColumnName("copiedFromEvaluatorId");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Project).WithMany(p => p.Evaluators)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Evaluator_projectId_fkey");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Evaluators)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Evaluator_workflowId_fkey");

            entity.HasOne(d => d.CopiedFrom).WithMany(p => p.CopiedEvaluators)
                .HasForeignKey(d => d.CopiedFromEvaluatorId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Evaluator_copiedFromEvaluatorId_fkey");
        });

        modelBuilder.Entity<EvaluatorVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EvaluatorVersion_pkey");

            entity.ToTable("EvaluatorVersion");

            entity.HasIndex(e => e.ProjectId, "EvaluatorVersion_projectId_idx");

            entity.HasIndex(e => new { e.EvaluatorId, e.Version }, "EvaluatorVersion_evaluatorId_version_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.AuthorKind).HasColumnName("authorKind");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<EvaluatorVersionLabel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EvaluatorVersionLabel_pkey");

            entity.ToTable("EvaluatorVersionLabel");

            entity.HasIndex(e => e.ProjectId, "EvaluatorVersionLabel_projectId_idx");

            entity.HasIndex(e => e.VersionId, "EvaluatorVersionLabel_versionId_idx");

            entity.HasIndex(e => new { e.EvaluatorId, e.Label }, "EvaluatorVersionLabel_evaluatorId_label_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.VersionId).HasColumnName("versionId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Scenario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Scenario_pkey");

            entity.ToTable("Scenario");

            entity.HasIndex(e => e.ProjectId, "Scenario_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TestSuiteId }, "Scenario_projectId_testSuiteId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Situation).HasColumnName("situation");
            entity.Property(e => e.Criteria).HasColumnName("criteria");
            entity.Property(e => e.Labels).HasColumnName("labels");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.SimulatorModel).HasColumnName("simulatorModel");
            entity.Property(e => e.JudgeModel).HasColumnName("judgeModel");
            entity.Property(e => e.MaxTurns).HasColumnName("maxTurns");
            entity.Property(e => e.MinTurns).HasColumnName("minTurns");
            entity.Property(e => e.Fields)
                .HasColumnType("jsonb")
                .HasColumnName("fields");
            entity.Property(e => e.CallerVoice)
                .HasColumnType("jsonb")
                .HasColumnName("callerVoice");
            entity.Property(e => e.TestSuiteId).HasColumnName("testSuiteId");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("version");
            entity.Property(e => e.LastUpdatedById).HasColumnName("lastUpdatedById");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Scenarios)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Scenario_projectId_fkey");

            entity.HasOne(d => d.TestSuite).WithMany(p => p.TestSuiteScenarios)
                .HasForeignKey(d => d.TestSuiteId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Scenario_testSuiteId_fkey");

            entity.HasOne(d => d.LastUpdatedBy).WithMany(p => p.Scenarios)
                .HasForeignKey(d => d.LastUpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Scenario_lastUpdatedById_fkey");
        });

        modelBuilder.Entity<ScenarioVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScenarioVersion_pkey");

            entity.ToTable("ScenarioVersion");

            entity.HasIndex(e => new { e.ProjectId, e.ScenarioId, e.Version }, "ScenarioVersion_projectId_scenarioId_version_idx");

            entity.HasIndex(e => e.CreatedAt, "ScenarioVersion_createdAt_idx");

            entity.HasIndex(e => new { e.ScenarioId, e.Version }, "ScenarioVersion_scenarioId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ScenarioId).HasColumnName("scenarioId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.AuthorLabel).HasColumnName("authorLabel");
            entity.Property(e => e.ChangeDescription).HasColumnName("changeDescription");
            entity.Property(e => e.Snapshot)
                .HasColumnType("jsonb")
                .HasColumnName("snapshot");
            entity.Property(e => e.SchemaVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("schemaVersion");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Scenario).WithMany(p => p.Versions)
                .HasForeignKey(d => d.ScenarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ScenarioVersion_scenarioId_fkey");
        });

        modelBuilder.Entity<SimulationSuite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SimulationSuite_pkey");

            entity.ToTable("SimulationSuite");

            entity.HasIndex(e => e.ProjectId, "SimulationSuite_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Kind }, "SimulationSuite_projectId_kind_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "SimulationSuite_projectId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ScenarioIds).HasColumnName("scenarioIds");
            entity.Property(e => e.Targets)
                .HasColumnType("jsonb")
                .HasColumnName("targets");
            entity.Property(e => e.RepeatCount)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("repeatCount");
            entity.Property(e => e.Labels).HasColumnName("labels");
            entity.Property(e => e.SimulatorModel).HasColumnName("simulatorModel");
            entity.Property(e => e.JudgeModel).HasColumnName("judgeModel");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("run_plan")
                .HasColumnName("kind");
            entity.Property(e => e.Scope)
                .HasColumnType("jsonb")
                .HasColumnName("scope");
            entity.Property(e => e.Fields)
                .HasColumnType("jsonb")
                .HasColumnName("fields");
            entity.Property(e => e.Evaluators)
                .HasColumnType("jsonb")
                .HasColumnName("evaluators");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.SuiteConfigurations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SimulationSuite_projectId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.StripeSubscriptionId, "Subscription_stripeSubscriptionId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "Subscription_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Plan).HasColumnName("plan");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.EndDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.LastPaymentFailedDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastPaymentFailedDate");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.MaxMembers).HasColumnName("maxMembers");
            entity.Property(e => e.MaxProjects).HasColumnName("maxProjects");
            entity.Property(e => e.MaxMessagesPerMonth).HasColumnName("maxMessagesPerMonth");
            entity.Property(e => e.MaxWorkflows).HasColumnName("maxWorkflows");
            entity.Property(e => e.MaxRetentionDays).HasColumnName("maxRetentionDays");
            entity.Property(e => e.MaxMembersLite).HasColumnName("maxMembersLite");
            entity.Property(e => e.MaxMembersDeveloper).HasColumnName("maxMembersDeveloper");
            entity.Property(e => e.MaxTeams).HasColumnName("maxTeams");
            entity.Property(e => e.MaxPrompts).HasColumnName("maxPrompts");
            entity.Property(e => e.MaxEvaluators).HasColumnName("maxEvaluators");
            entity.Property(e => e.MaxScenarios).HasColumnName("maxScenarios");
            entity.Property(e => e.MaxAgents).HasColumnName("maxAgents");
            entity.Property(e => e.MaxExperiments).HasColumnName("maxExperiments");
            entity.Property(e => e.MaxOnlineEvaluations).HasColumnName("maxOnlineEvaluations");
            entity.Property(e => e.MaxDatasets).HasColumnName("maxDatasets");
            entity.Property(e => e.MaxDashboards).HasColumnName("maxDashboards");
            entity.Property(e => e.MaxCustomGraphs).HasColumnName("maxCustomGraphs");
            entity.Property(e => e.MaxAutomations).HasColumnName("maxAutomations");
            entity.Property(e => e.SeatsKept).HasColumnName("seatsKept");
            entity.Property(e => e.SeatsKeptUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatsKeptUntil");
            entity.Property(e => e.SeatLineQuantity).HasColumnName("seatLineQuantity");
            entity.Property(e => e.SeatLineQuantityAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatLineQuantityAt");
            entity.Property(e => e.SeatLineWriteQuantities)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("seatLineWriteQuantities");
            entity.Property(e => e.SeatLineWriteSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatLineWriteSince");
            entity.Property(e => e.SeatLineWriteAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatLineWriteAt");
            entity.Property(e => e.SeatLineWriteAttempts).HasColumnName("seatLineWriteAttempts");
            entity.Property(e => e.SeatLineWriteFlaggedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatLineWriteFlaggedAt");
            entity.Property(e => e.SeatRepairQuantity).HasColumnName("seatRepairQuantity");
            entity.Property(e => e.SeatRepairLiftedQuantity).HasColumnName("seatRepairLiftedQuantity");
            entity.Property(e => e.SeatRepairSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatRepairSince");
            entity.Property(e => e.SeatRepairAttempts).HasColumnName("seatRepairAttempts");
            entity.Property(e => e.SeatRepairFlaggedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatRepairFlaggedAt");
            entity.Property(e => e.SeatLineLeaseToken).HasColumnName("seatLineLeaseToken");
            entity.Property(e => e.SeatLineLeaseUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("seatLineLeaseUntil");

            entity.HasOne(d => d.Organization).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Subscription_organizationId_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Invoice_pkey");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.SubscriptionId, "Invoice_subscriptionId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.DueDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dueDate");
            entity.Property(e => e.Paid)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("paid");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Invoice_subscriptionId_fkey");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InvoiceItem_pkey");

            entity.ToTable("InvoiceItem");

            entity.HasIndex(e => e.InvoiceId, "InvoiceItem_invoiceId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Invoice).WithMany(p => p.LineItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("InvoiceItem_invoiceId_fkey");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Group_pkey");

            entity.ToTable("Group");

            entity.HasIndex(e => e.OrganizationId, "Group_organizationId_idx");

            entity.HasIndex(e => e.ScimConnectionId, "Group_scimConnectionId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Slug }, "Group_organizationId_slug_key").IsUnique();

            entity.HasIndex(e => new { e.ScimConnectionId, e.ExternalId }, "Group_scimConnectionId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.ScimSource).HasColumnName("scimSource");
            entity.Property(e => e.ScimConnectionId).HasColumnName("scimConnectionId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.Groups)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Group_organizationId_fkey");
        });

        modelBuilder.Entity<GroupMembership>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GroupId }).HasName("GroupMembership_pkey");

            entity.ToTable("GroupMembership");

            entity.HasIndex(e => e.UserId, "GroupMembership_userId_idx");

            entity.HasIndex(e => e.GroupId, "GroupMembership_groupId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GroupMembership_userId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Members)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GroupMembership_groupId_fkey");
        });

        modelBuilder.Entity<AuthzUserStanding>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("AuthzUserStanding_pkey");

            entity.ToTable("AuthzUserStanding");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DeactivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deactivatedAt");
            entity.Property(e => e.ErasedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasedAt");
            entity.Property(e => e.StandingChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("standingChangedAt");
        });

        modelBuilder.Entity<AuthzEngineSwitch>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("AuthzEngineSwitch_pkey");

            entity.ToTable("AuthzEngineSwitch");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OnEngine).HasColumnName("onEngine");
            entity.Property(e => e.Pinned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pinned");
            entity.Property(e => e.SwitchedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("switchedAt");
            entity.Property(e => e.LastAppliedEventId).HasColumnName("lastAppliedEventId");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<AuthzOrganizationPolicy>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("AuthzOrganizationPolicy_pkey");

            entity.ToTable("AuthzOrganizationPolicy");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BraidsOrganizationAdminsOnly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("braidsOrganizationAdminsOnly");
            entity.Property(e => e.UpdatedByUserId).HasColumnName("updatedByUserId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<RoleBinding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoleBinding_pkey");

            entity.ToTable("RoleBinding");

            entity.HasIndex(e => e.OrganizationId, "RoleBinding_organizationId_idx");

            entity.HasIndex(e => e.UserId, "RoleBinding_userId_idx");

            entity.HasIndex(e => e.GroupId, "RoleBinding_groupId_idx");

            entity.HasIndex(e => e.ApiKeyId, "RoleBinding_apiKeyId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "RoleBinding_scopeType_scopeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ApiKeyId).HasColumnName("apiKeyId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CustomRoleId).HasColumnName("customRoleId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.RoleBindings)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RoleBinding_organizationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.RoleBindings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RoleBinding_userId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.RoleBindings)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RoleBinding_groupId_fkey");

            entity.HasOne(d => d.ApiKey).WithMany(p => p.RoleBindings)
                .HasForeignKey(d => d.ApiKeyId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("RoleBinding_apiKeyId_fkey");

            entity.HasOne(d => d.CustomRole).WithMany(p => p.RoleBindings)
                .HasForeignKey(d => d.CustomRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RoleBinding_customRoleId_fkey");
        });

        modelBuilder.Entity<Grant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Grant_pkey");

            entity.ToTable("Grant");

            entity.HasIndex(e => e.Token, "Grant_token_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.ScopeType, e.ScopeId }, "Grant_organizationId_scopeType_scopeId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ResourceKind, e.ScopeId }, "Grant_projectId_resourceKind_scopeId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.PrincipalType).HasColumnName("principalType");
            entity.Property(e => e.PrincipalId).HasColumnName("principalId");
            entity.Property(e => e.RoleKey).HasColumnName("roleKey");
            entity.Property(e => e.LegacyRole).HasColumnName("legacyRole");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Permission).HasColumnName("permission");
            entity.Property(e => e.ResourceKind).HasColumnName("resourceKind");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.MaxViews).HasColumnName("maxViews");
            entity.Property(e => e.Condition)
                .HasColumnType("jsonb")
                .HasColumnName("condition");
            entity.Property(e => e.ReplacesGrantId).HasColumnName("replacesGrantId");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedReason).HasColumnName("revokedReason");
        });

        modelBuilder.Entity<GrantUsage>(entity =>
        {
            entity.HasKey(e => e.GrantId).HasName("GrantUsage_pkey");

            entity.ToTable("GrantUsage");

            entity.HasIndex(e => e.OrganizationId, "GrantUsage_organizationId_idx");

            entity.HasIndex(e => e.ProjectId, "GrantUsage_projectId_idx");

            entity.Property(e => e.GrantId).HasColumnName("grantId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ViewCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("viewCount");
            entity.Property(e => e.LastViewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastViewedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.HasIndex(e => e.OrganizationId, "Role_organizationId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Permissions)
                .HasColumnType("jsonb")
                .HasColumnName("permissions");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("custom")
                .HasColumnName("kind");
            entity.Property(e => e.OccurredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKey_pkey");

            entity.ToTable("ApiKey");

            entity.HasIndex(e => e.LookupId, "ApiKey_lookupId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "ApiKey_userId_idx");

            entity.HasIndex(e => e.OrganizationId, "ApiKey_organizationId_idx");

            entity.HasIndex(e => e.LookupId, "ApiKey_lookupId_idx");

            entity.HasIndex(e => e.IngestSourceType, "ApiKey_ingestSourceType_idx");

            entity.HasIndex(e => e.IngestionTemplateId, "ApiKey_ingestionTemplateId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LookupId).HasColumnName("lookupId");
            entity.Property(e => e.HashedSecret).HasColumnName("hashedSecret");
            entity.Property(e => e.PermissionMode)
                .ValueGeneratedNever()
                .HasDefaultValue("all")
                .HasColumnName("permissionMode");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevocationCause).HasColumnName("revocationCause");
            entity.Property(e => e.ParentApiKeyId).HasColumnName("parentApiKeyId");
            entity.Property(e => e.IngestSourceType).HasColumnName("ingestSourceType");
            entity.Property(e => e.IngestionTemplateId).HasColumnName("ingestionTemplateId");
            entity.Property(e => e.CreatedByDeviceLabel).HasColumnName("createdByDeviceLabel");
            entity.Property(e => e.IsSystemManaged)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSystemManaged");
            entity.Property(e => e.CreatedByBrowserSessionId).HasColumnName("createdByBrowserSessionId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("ApiKey_userId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ApiKey_organizationId_fkey");
        });

        modelBuilder.Entity<SigningKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SigningKey_pkey");

            entity.ToTable("SigningKey");

            entity.HasIndex(e => e.PublicId, "SigningKey_publicId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "SigningKey_projectId_idx");

            entity.HasIndex(e => e.OrganizationId, "SigningKey_organizationId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PublicId).HasColumnName("publicId");
            entity.Property(e => e.SecretCiphertext).HasColumnName("secretCiphertext");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.RotatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rotatedAt");
            entity.Property(e => e.VerifiesUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("verifiesUntil");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
        });

        modelBuilder.Entity<ProjectActiveDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectActiveDay_pkey");

            entity.ToTable("ProjectActiveDay");

            entity.HasIndex(e => new { e.ProjectId, e.Day }, "ProjectActiveDay_projectId_day_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<NurturingProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("NurturingProject_pkey");

            entity.ToTable("NurturingProject");

            entity.HasIndex(e => e.OrganizationId, "NurturingProject_organizationId_idx");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
        });

        modelBuilder.Entity<NurturingUncopiedProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("NurturingUncopiedProject_pkey");

            entity.ToTable("NurturingUncopiedProject");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.FirstSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
        });

        modelBuilder.Entity<NurturingOrganizationMember>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.UserId }).HasName("NurturingOrganizationMember_pkey");

            entity.ToTable("NurturingOrganizationMember");

            entity.HasIndex(e => e.UserId, "NurturingOrganizationMember_userId_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.JoinedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("joinedAt");
            entity.Property(e => e.LeftAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("leftAt");
            entity.Property(e => e.ReturnedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("returnedAt");
        });

        modelBuilder.Entity<NurturingOrganizationCreated>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("NurturingOrganizationCreated_pkey");

            entity.ToTable("NurturingOrganizationCreated");

            entity.HasIndex(e => e.CreatedAt, "NurturingOrganizationCreated_createdAt_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<NurturingOrganization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("NurturingOrganization_pkey");

            entity.ToTable("NurturingOrganization");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.AdminUserId).HasColumnName("adminUserId");
            entity.Property(e => e.Seeded)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("seeded");
            entity.Property(e => e.EvaluationCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("evaluationCount");
            entity.Property(e => e.SimulationRunCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("simulationRunCount");
            entity.Property(e => e.CodingFirstSessionAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("codingFirstSessionAt");
            entity.Property(e => e.CodingAgentTypes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("codingAgentTypes");
            entity.Property(e => e.CodingActiveDays)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("codingActiveDays");
            entity.Property(e => e.CodingLastActiveDay).HasColumnName("codingLastActiveDay");
            entity.Property(e => e.CodingTerminalTypes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("codingTerminalTypes");
            entity.Property(e => e.CodingRepositoryKeys)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("codingRepositoryKeys");
            entity.Property(e => e.CodingFirstDays)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("codingFirstDays");
            entity.Property(e => e.LastTraceAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastTraceAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NurturingProjectUsageView>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.Owner }).HasName("NurturingProjectUsageView_pkey");

            entity.ToTable("NurturingProjectUsageView");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Owner).HasColumnName("owner");
            entity.Property(e => e.Folded)
                .HasColumnType("jsonb")
                .HasColumnName("folded");
            entity.Property(e => e.AppliedEventIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("appliedEventIds");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NurturingProjectTraceDay>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.Day }).HasName("NurturingProjectTraceDay_pkey");

            entity.ToTable("NurturingProjectTraceDay");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.TraceCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("traceCount");
            entity.Property(e => e.ErrorCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("errorCount");
            entity.Property(e => e.LastTraceAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastTraceAt");
            entity.Property(e => e.LastFailedTraceAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastFailedTraceAt");
        });

        modelBuilder.Entity<NurturingPersonalProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("NurturingPersonalProject_pkey");

            entity.ToTable("NurturingPersonalProject");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OwnerUserId).HasColumnName("ownerUserId");
        });

        modelBuilder.Entity<NurturingTracedProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("NurturingTracedProject_pkey");

            entity.ToTable("NurturingTracedProject");

            entity.HasIndex(e => e.OrganizationId, "NurturingTracedProject_organizationId_idx");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
        });

        modelBuilder.Entity<BillingMeterCheckpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BillingMeterCheckpoint_pkey");

            entity.ToTable("BillingMeterCheckpoint");

            entity.HasIndex(e => new { e.OrganizationId, e.BillingMonth, e.Meter }, "BillingMeterCheckpoint_organizationId_billingMonth_meter_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BillingMonth).HasColumnName("billingMonth");
            entity.Property(e => e.Meter)
                .ValueGeneratedNever()
                .HasDefaultValue("langwatch_billable_events")
                .HasColumnName("meter");
            entity.Property(e => e.LastReportedTotal)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastReportedTotal");
            entity.Property(e => e.PendingReportedTotal).HasColumnName("pendingReportedTotal");
            entity.Property(e => e.ConsecutiveFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveFailures");
            entity.Property(e => e.LastCountedEventId).HasColumnName("lastCountedEventId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<VirtualKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VirtualKey_pkey");

            entity.ToTable("VirtualKey");

            entity.HasIndex(e => e.LicenseTokenHash, "VirtualKey_licenseTokenHash_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "VirtualKey_organizationId_idx");

            entity.HasIndex(e => e.DisplayPrefix, "VirtualKey_displayPrefix_idx");

            entity.HasIndex(e => e.Status, "VirtualKey_status_idx");

            entity.HasIndex(e => e.PrincipalUserId, "VirtualKey_principalUserId_idx");

            entity.HasIndex(e => e.Revision, "VirtualKey_revision_idx");

            entity.HasIndex(e => e.RoutingPolicyId, "VirtualKey_routingPolicyId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Purpose }, "VirtualKey_organizationId_purpose_idx");

            entity.HasIndex(e => e.HashedSecret, "VirtualKey_hashedSecret_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.ExternalId }, "VirtualKey_organizationId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(VirtualKeyStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.Purpose)
                .ValueGeneratedNever()
                .HasDefaultValue(VirtualKeyPurpose.User)
                .HasColumnName("purpose");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.DisabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disabledAt");
            entity.Property(e => e.DisabledReason).HasColumnName("disabledReason");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.HashedSecret).HasColumnName("hashedSecret");
            entity.Property(e => e.DisplayPrefix).HasColumnName("displayPrefix");
            entity.Property(e => e.PrincipalUserId).HasColumnName("principalUserId");
            entity.Property(e => e.TraceProjectId).HasColumnName("traceProjectId");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("revision");
            entity.Property(e => e.PreviousHashedSecret).HasColumnName("previousHashedSecret");
            entity.Property(e => e.PreviousSecretValidUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("previousSecretValidUntil");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedById).HasColumnName("revokedById");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.RoutingPolicyId).HasColumnName("routingPolicyId");
            entity.Property(e => e.RoutingMode)
                .ValueGeneratedNever()
                .HasDefaultValue(VirtualKeyRoutingMode.None)
                .HasColumnName("routingMode");
            entity.Property(e => e.ConnectServices)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("connectServices");
            entity.Property(e => e.LicenseTokenHash).HasColumnName("licenseTokenHash");
            entity.Property(e => e.LicenseInstanceId).HasColumnName("licenseInstanceId");
            entity.Property(e => e.LicenseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("licenseExpiresAt");
            entity.Property(e => e.LicenseId).HasColumnName("licenseId");

            entity.HasOne(d => d.PrincipalUser).WithMany(p => p.PrincipalVirtualKeys)
                .HasForeignKey(d => d.PrincipalUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("VirtualKey_principalUserId_fkey");

            entity.HasOne(d => d.RoutingPolicy).WithMany(p => p.VirtualKeys)
                .HasForeignKey(d => d.RoutingPolicyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("VirtualKey_routingPolicyId_fkey");
        });

        modelBuilder.Entity<VirtualKeyScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VirtualKeyScope_pkey");

            entity.ToTable("VirtualKeyScope");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "VirtualKeyScope_scopeType_scopeId_idx");

            entity.HasIndex(e => e.VirtualKeyId, "VirtualKeyScope_virtualKeyId_idx");

            entity.HasIndex(e => new { e.VirtualKeyId, e.ScopeType, e.ScopeId }, "VirtualKeyScope_virtualKeyId_scopeType_scopeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.VirtualKeyId).HasColumnName("virtualKeyId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.VirtualKey).WithMany(p => p.Scopes)
                .HasForeignKey(d => d.VirtualKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VirtualKeyScope_virtualKeyId_fkey");
        });

        modelBuilder.Entity<RoutingPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoutingPolicy_pkey");

            entity.ToTable("RoutingPolicy");

            entity.HasIndex(e => e.OrganizationId, "RoutingPolicy_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ModelProviderIds)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("modelProviderIds");
            entity.Property(e => e.ModelAliases)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("modelAliases");
            entity.Property(e => e.DefaultModel).HasColumnName("defaultModel");
            entity.Property(e => e.PolicyRules)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("policyRules");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.RoutingPolicies)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RoutingPolicy_organizationId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.RoutingPoliciesCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RoutingPolicy_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.RoutingPoliciesUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RoutingPolicy_updatedById_fkey");
        });

        modelBuilder.Entity<RoutingPolicyScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoutingPolicyScope_pkey");

            entity.ToTable("RoutingPolicyScope");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "RoutingPolicyScope_scopeType_scopeId_idx");

            entity.HasIndex(e => e.RoutingPolicyId, "RoutingPolicyScope_routingPolicyId_idx");

            entity.HasIndex(e => new { e.RoutingPolicyId, e.ScopeType, e.ScopeId }, "RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.RoutingPolicyId).HasColumnName("routingPolicyId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.RoutingPolicy).WithMany(p => p.Scopes)
                .HasForeignKey(d => d.RoutingPolicyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RoutingPolicyScope_routingPolicyId_fkey");
        });

        modelBuilder.Entity<GatewayGuardrail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayGuardrail_pkey");

            entity.ToTable("GatewayGuardrail");

            entity.HasIndex(e => e.ProjectId, "GatewayGuardrail_projectId_idx");

            entity.HasIndex(e => e.EvaluatorId, "GatewayGuardrail_evaluatorId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ArchivedAt }, "GatewayGuardrail_projectId_archivedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluatorId");
            entity.Property(e => e.Direction).HasColumnName("direction");
            entity.Property(e => e.FailureMode)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayGuardrailFailureMode.FailClosed)
                .HasColumnName("failureMode");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Project).WithMany(p => p.GatewayGuardrails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GatewayGuardrail_projectId_fkey");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.GatewayGuardrails)
                .HasForeignKey(d => d.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("GatewayGuardrail_evaluatorId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.GatewayGuardrailsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("GatewayGuardrail_createdById_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.GatewayGuardrailsUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("GatewayGuardrail_updatedById_fkey");
        });

        modelBuilder.Entity<IngestionSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IngestionSource_pkey");

            entity.ToTable("IngestionSource");

            entity.HasIndex(e => e.OrganizationId, "IngestionSource_organizationId_idx");

            entity.HasIndex(e => e.TeamId, "IngestionSource_teamId_idx");

            entity.HasIndex(e => e.SourceType, "IngestionSource_sourceType_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "IngestionSource_organizationId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IngestSecretHash).HasColumnName("ingestSecretHash");
            entity.Property(e => e.ParserConfig)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("parserConfig");
            entity.Property(e => e.PollerCursor)
                .HasColumnType("jsonb")
                .HasColumnName("pollerCursor");
            entity.Property(e => e.ErrorCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("errorCount");
            entity.Property(e => e.PullSchedule).HasColumnName("pullSchedule");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("awaiting_first_event")
                .HasColumnName("status");
            entity.Property(e => e.TraceProjectId).HasColumnName("traceProjectId");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.LastEventAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastEventAt");
            entity.Property(e => e.LastSuccessAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSuccessAt");
            entity.Property(e => e.LastReadThroughAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReadThroughAt");
            entity.Property(e => e.LastRunCompleteness).HasColumnName("lastRunCompleteness");
            entity.Property(e => e.UnpricedUsageSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("unpricedUsageSince");
            entity.Property(e => e.UnpricedUsageThrough)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("unpricedUsageThrough");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");

            entity.HasOne(d => d.Organization).WithMany(p => p.IngestionSources)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IngestionSource_organizationId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.IngestionSources)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("IngestionSource_teamId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.IngestionSourcesCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("IngestionSource_createdById_fkey");
        });

        modelBuilder.Entity<AnomalyRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnomalyRule_pkey");

            entity.ToTable("AnomalyRule");

            entity.HasIndex(e => e.OrganizationId, "AnomalyRule_organizationId_idx");

            entity.HasIndex(e => new { e.Scope, e.ScopeId }, "AnomalyRule_scope_scopeId_idx");

            entity.HasIndex(e => e.RuleType, "AnomalyRule_ruleType_idx");

            entity.HasIndex(e => e.Status, "AnomalyRule_status_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "AnomalyRule_organizationId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Severity).HasColumnName("severity");
            entity.Property(e => e.RuleType).HasColumnName("ruleType");
            entity.Property(e => e.ThresholdConfig)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("thresholdConfig");
            entity.Property(e => e.DestinationConfig)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("destinationConfig");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");

            entity.HasOne(d => d.Organization).WithMany(p => p.AnomalyRules)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AnomalyRule_organizationId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.AnomalyRulesCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AnomalyRule_createdById_fkey");
        });

        modelBuilder.Entity<AnomalyAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnomalyAlert_pkey");

            entity.ToTable("AnomalyAlert");

            entity.HasIndex(e => new { e.OrganizationId, e.DetectedAt }, "AnomalyAlert_organizationId_detectedAt_idx");

            entity.HasIndex(e => e.RuleId, "AnomalyAlert_ruleId_idx");

            entity.HasIndex(e => e.State, "AnomalyAlert_state_idx");

            entity.HasIndex(e => new { e.RuleId, e.TriggerWindowStart }, "AnomalyAlert_ruleId_triggerWindowStart_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.RuleId).HasColumnName("ruleId");
            entity.Property(e => e.Severity).HasColumnName("severity");
            entity.Property(e => e.RuleName).HasColumnName("ruleName");
            entity.Property(e => e.RuleType).HasColumnName("ruleType");
            entity.Property(e => e.TriggerWindowStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("triggerWindowStart");
            entity.Property(e => e.TriggerWindowEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("triggerWindowEnd");
            entity.Property(e => e.TriggerSpendUsd)
                .HasPrecision(18, 6)
                .HasColumnName("triggerSpendUsd");
            entity.Property(e => e.TriggerEventCount).HasColumnName("triggerEventCount");
            entity.Property(e => e.Detail)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("detail");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue("open")
                .HasColumnName("state");
            entity.Property(e => e.DestinationStatus)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("destinationStatus");
            entity.Property(e => e.DetectedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("detectedAt");
            entity.Property(e => e.AcknowledgedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("acknowledgedAt");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.AnomalyAlerts)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AnomalyAlert_organizationId_fkey");

            entity.HasOne(d => d.Rule).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AnomalyAlert_ruleId_fkey");
        });

        modelBuilder.Entity<AiToolEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AiToolEntry_pkey");

            entity.ToTable("AiToolEntry");

            entity.HasIndex(e => new { e.OrganizationId, e.Scope, e.ScopeId }, "AiToolEntry_organizationId_scope_scopeId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Enabled, e.ArchivedAt }, "AiToolEntry_organizationId_enabled_archivedAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Type }, "AiToolEntry_organizationId_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.IconKey).HasColumnName("iconKey");
            entity.Property(e => e.IconAsset).HasColumnName("iconAsset");
            entity.Property(e => e.Order)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("order");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.AiToolEntries)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AiToolEntry_organizationId_fkey");
        });

        modelBuilder.Entity<AiToolEntryTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AiToolEntryTeam_pkey");

            entity.ToTable("AiToolEntryTeam");

            entity.HasIndex(e => e.TeamId, "AiToolEntryTeam_teamId_idx");

            entity.HasIndex(e => new { e.EntryId, e.TeamId }, "AiToolEntryTeam_entryId_teamId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.EntryId).HasColumnName("entryId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.Entry).WithMany(p => p.Teams)
                .HasForeignKey(d => d.EntryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AiToolEntryTeam_entryId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.AiToolEntryTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AiToolEntryTeam_teamId_fkey");
        });

        modelBuilder.Entity<AiToolEntryDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AiToolEntryDepartment_pkey");

            entity.ToTable("AiToolEntryDepartment");

            entity.HasIndex(e => e.DepartmentId, "AiToolEntryDepartment_departmentId_idx");

            entity.HasIndex(e => new { e.EntryId, e.DepartmentId }, "AiToolEntryDepartment_entryId_departmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.EntryId).HasColumnName("entryId");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

            entity.HasOne(d => d.Entry).WithMany(p => p.Departments)
                .HasForeignKey(d => d.EntryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AiToolEntryDepartment_entryId_fkey");
        });

        modelBuilder.Entity<IngestionTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IngestionTemplate_pkey");

            entity.ToTable("IngestionTemplate");

            entity.HasIndex(e => new { e.OrganizationId, e.ArchivedAt }, "IngestionTemplate_organizationId_archivedAt_idx");

            entity.HasIndex(e => e.SourceType, "IngestionTemplate_sourceType_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Slug }, "IngestionTemplate_organizationId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IconAsset).HasColumnName("iconAsset");
            entity.Property(e => e.CredentialSchema).HasColumnName("credentialSchema");
            entity.Property(e => e.OttlRules)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("ottlRules");
            entity.Property(e => e.PlatformPublished)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("platformPublished");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.IngestionTemplates)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("IngestionTemplate_organizationId_fkey");
        });

        modelBuilder.Entity<GatewayConnectUpstream>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("GatewayConnectUpstream_pkey");

            entity.ToTable("GatewayConnectUpstream");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BaseUrl).HasColumnName("baseUrl");
            entity.Property(e => e.EncryptedToken).HasColumnName("encryptedToken");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<GatewayTraceExportKey>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("GatewayTraceExportKey_pkey");

            entity.ToTable("GatewayTraceExportKey");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ApiKeyId).HasColumnName("apiKeyId");
            entity.Property(e => e.EncryptedToken).HasColumnName("encryptedToken");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<GatewayBudget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayBudget_pkey");

            entity.ToTable("GatewayBudget");

            entity.HasIndex(e => e.OrganizationId, "GatewayBudget_organizationId_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId, e.ProviderKey }, "GatewayBudget_scopeType_scopeId_providerKey_idx");

            entity.HasIndex(e => e.ManagedByVirtualKeyId, "GatewayBudget_managedByVirtualKeyId_idx");

            entity.HasIndex(e => e.ResetsAt, "GatewayBudget_resetsAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.ExternalId }, "GatewayBudget_organizationId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ScopeType).HasColumnName("scopeType");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.ProviderKey).HasColumnName("providerKey");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Window).HasColumnName("window");
            entity.Property(e => e.LimitUsd)
                .HasPrecision(18, 6)
                .HasColumnName("limitUsd");
            entity.Property(e => e.OnBreach)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayBudgetBreachAction.Block)
                .HasColumnName("onBreach");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.SpentUsd)
                .ValueGeneratedNever()
                .HasPrecision(18, 6)
                .HasDefaultValue(0m)
                .HasColumnName("spentUsd");
            entity.Property(e => e.CurrentPeriodStartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentPeriodStartedAt");
            entity.Property(e => e.ResetsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resetsAt");
            entity.Property(e => e.LastResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastResetAt");
            entity.Property(e => e.CycleAnchorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cycleAnchorAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.ManagedByVirtualKeyId).HasColumnName("managedByVirtualKeyId");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.GatewayBudgetsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("GatewayBudget_createdById_fkey");
        });

        modelBuilder.Entity<GatewayBudgetBucketBoundary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayBudgetBucketBoundary_pkey");

            entity.ToTable("GatewayBudgetBucketBoundary");

            entity.HasIndex(e => e.OrganizationId, "GatewayBudgetBucketBoundary_organizationId_idx");

            entity.HasIndex(e => new { e.BudgetId, e.BucketScopeId }, "GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.BudgetId).HasColumnName("budgetId");
            entity.Property(e => e.BucketScopeId).HasColumnName("bucketScopeId");
            entity.Property(e => e.PeriodStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodStartedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Budget).WithMany(p => p.BucketBoundaries)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GatewayBudgetBucketBoundary_budgetId_fkey");
        });

        modelBuilder.Entity<GatewayBudgetLedger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayBudgetLedger_pkey");

            entity.ToTable("GatewayBudgetLedger");

            entity.HasIndex(e => e.VirtualKeyId, "GatewayBudgetLedger_virtualKeyId_idx");

            entity.HasIndex(e => e.GatewayRequestId, "GatewayBudgetLedger_gatewayRequestId_idx");

            entity.HasIndex(e => e.OccurredAt, "GatewayBudgetLedger_occurredAt_idx");

            entity.HasIndex(e => new { e.BudgetId, e.GatewayRequestId }, "GatewayBudgetLedger_budgetId_gatewayRequestId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.BudgetId).HasColumnName("budgetId");
            entity.Property(e => e.VirtualKeyId).HasColumnName("virtualKeyId");
            entity.Property(e => e.ModelProviderId).HasColumnName("modelProviderId");
            entity.Property(e => e.ProviderKey).HasColumnName("providerKey");
            entity.Property(e => e.GatewayRequestId).HasColumnName("gatewayRequestId");
            entity.Property(e => e.AmountUsd)
                .HasPrecision(18, 6)
                .HasColumnName("amountUsd");
            entity.Property(e => e.TokensInput)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tokensInput");
            entity.Property(e => e.TokensOutput)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tokensOutput");
            entity.Property(e => e.TokensCacheRead)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tokensCacheRead");
            entity.Property(e => e.TokensCacheWrite)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tokensCacheWrite");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.DurationMs).HasColumnName("durationMs");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayBudgetLedgerStatus.Success)
                .HasColumnName("status");
            entity.Property(e => e.OccurredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");

            entity.HasOne(d => d.Budget).WithMany(p => p.LedgerEntries)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GatewayBudgetLedger_budgetId_fkey");

            entity.HasOne(d => d.VirtualKey).WithMany(p => p.LedgerEntries)
                .HasForeignKey(d => d.VirtualKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GatewayBudgetLedger_virtualKeyId_fkey");
        });

        modelBuilder.Entity<GatewayChangeEvent>(entity =>
        {
            entity.HasKey(e => e.Revision).HasName("GatewayChangeEvent_pkey");

            entity.ToTable("GatewayChangeEvent");

            entity.HasIndex(e => new { e.OrganizationId, e.Revision }, "GatewayChangeEvent_organizationId_revision_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Revision }, "GatewayChangeEvent_projectId_revision_idx");

            entity.HasIndex(e => e.VirtualKeyId, "GatewayChangeEvent_virtualKeyId_idx");

            entity.HasIndex(e => e.BudgetId, "GatewayChangeEvent_budgetId_idx");

            entity.Property(e => e.Revision)
                .UseSerialColumn()
                .HasColumnName("revision");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.VirtualKeyId).HasColumnName("virtualKeyId");
            entity.Property(e => e.BudgetId).HasColumnName("budgetId");
            entity.Property(e => e.ModelProviderId).HasColumnName("modelProviderId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.GatewayChangeEvents)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GatewayChangeEvent_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.GatewayChangeEvents)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("GatewayChangeEvent_projectId_fkey");
        });

        modelBuilder.Entity<GatewayCacheRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayCacheRule_pkey");

            entity.ToTable("GatewayCacheRule");

            entity.HasIndex(e => new { e.OrganizationId, e.ArchivedAt, e.Priority }, "GatewayCacheRule_organizationId_archivedAt_priority_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Enabled, e.Priority }, "GatewayCacheRule_organizationId_enabled_priority_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Priority)
                .ValueGeneratedNever()
                .HasDefaultValue(100)
                .HasColumnName("priority");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Matchers)
                .HasColumnType("jsonb")
                .HasColumnName("matchers");
            entity.Property(e => e.Action)
                .HasColumnType("jsonb")
                .HasColumnName("action");
            entity.Property(e => e.ModeEnum).HasColumnName("modeEnum");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
        });

        modelBuilder.Entity<FeatureFlag>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("FeatureFlag_pkey");

            entity.ToTable("FeatureFlag");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.Rules)
                .HasColumnType("jsonb")
                .HasColumnName("rules");
            entity.Property(e => e.LastEditedBy).HasColumnName("lastEditedBy");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<FeatureFlagExperimentSetting>(entity =>
        {
            entity.HasKey(e => new { e.FlagKey, e.SubjectType, e.SubjectId }).HasName("FeatureFlagExperimentSetting_pkey");

            entity.ToTable("FeatureFlagExperimentSetting");

            entity.HasIndex(e => new { e.SubjectType, e.SubjectId }, "FeatureFlagExperimentSetting_subjectType_subjectId_idx");

            entity.Property(e => e.FlagKey).HasColumnName("flagKey");
            entity.Property(e => e.SubjectType).HasColumnName("subjectType");
            entity.Property(e => e.SubjectId).HasColumnName("subjectId");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.ChangedByUserId).HasColumnName("changedByUserId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ProcessManagerInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProcessManagerInstance_pkey");

            entity.ToTable("ProcessManagerInstance");

            entity.HasIndex(e => e.NextWakeAt, "ProcessManagerInstance_nextWakeAt_idx");

            entity.HasIndex(e => new { e.ProcessName, e.ProjectId, e.ProcessKey }, "ProcessManagerInstance_processName_projectId_processKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProcessName).HasColumnName("processName");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProcessKey).HasColumnName("processKey");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.State)
                .HasColumnType("jsonb")
                .HasColumnName("state");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.NextWakeAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("nextWakeAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ProcessManagerInbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProcessManagerInbox_pkey");

            entity.ToTable("ProcessManagerInbox");

            entity.HasIndex(e => new { e.ProcessName, e.ProjectId, e.ProcessKey, e.ConsumedAt }, "ProcessManagerInbox_processName_projectId_processKey_consum_idx");

            entity.HasIndex(e => e.ConsumedAt, "ProcessManagerInbox_consumedAt_idx");

            entity.HasIndex(e => new { e.ProcessName, e.ProjectId, e.SourceEventKey }, "ProcessManagerInbox_processName_projectId_sourceEventKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProcessName).HasColumnName("processName");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProcessKey).HasColumnName("processKey");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.SourceEventId).HasColumnName("sourceEventId");
            entity.Property(e => e.SourceEventKey).HasColumnName("sourceEventKey");
            entity.Property(e => e.ConsumedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("consumedAt");
        });

        modelBuilder.Entity<ProcessManagerOutbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProcessManagerOutbox_pkey");

            entity.ToTable("ProcessManagerOutbox");

            entity.HasIndex(e => e.LeaseToken, "ProcessManagerOutbox_leaseToken_key").IsUnique();

            entity.HasIndex(e => new { e.ProcessName, e.ProjectId, e.ProcessKey, e.CreatedAt }, "ProcessManagerOutbox_processName_projectId_processKey_creat_idx");

            entity.HasIndex(e => new { e.Status, e.NextAttemptAt, e.LeasedUntil }, "ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx");

            entity.HasIndex(e => new { e.Status, e.ProcessName, e.NextAttemptAt }, "ProcessManagerOutbox_status_processName_nextAttemptAt_idx");

            entity.HasIndex(e => new { e.ProcessName, e.ProjectId, e.MessageKey }, "ProcessManagerOutbox_processName_projectId_messageKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProcessName).HasColumnName("processName");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProcessKey).HasColumnName("processKey");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.MessageKey).HasColumnName("messageKey");
            entity.Property(e => e.IntentType).HasColumnName("intentType");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.TraceCarrier)
                .HasColumnType("jsonb")
                .HasColumnName("traceCarrier");
            entity.Property(e => e.SourceEventId).HasColumnName("sourceEventId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ProcessManagerOutboxStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.NextAttemptAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("nextAttemptAt");
            entity.Property(e => e.LeasedUntil)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("leasedUntil");
            entity.Property(e => e.LeaseToken).HasColumnName("leaseToken");
            entity.Property(e => e.DispatchedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("dispatchedAt");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ProcessManagerOutboxAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProcessManagerOutboxAttempt_pkey");

            entity.ToTable("ProcessManagerOutboxAttempt");

            entity.HasIndex(e => new { e.OutboxId, e.Attempt }, "ProcessManagerOutboxAttempt_outboxId_attempt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OutboxId).HasColumnName("outboxId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Attempt).HasColumnName("attempt");
            entity.Property(e => e.OccurredAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("occurredAt");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.ErrorType).HasColumnName("errorType");
            entity.Property(e => e.ErrorMessage).HasColumnName("errorMessage");
            entity.Property(e => e.RetryAfterMs).HasColumnName("retryAfterMs");

            entity.HasOne(d => d.Outbox).WithMany(p => p.AttemptLog)
                .HasForeignKey(d => d.OutboxId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProcessManagerOutboxAttempt_outboxId_fkey");
        });

        modelBuilder.Entity<TopicClusteringRunProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TopicClusteringRunProjection_pkey");

            entity.ToTable("TopicClusteringRunProjection");

            entity.HasIndex(e => e.ProjectId, "TopicClusteringRunProjection_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.LastRequestedAt).HasColumnName("lastRequestedAt");
            entity.Property(e => e.LastRequestTrigger).HasColumnName("lastRequestTrigger");
            entity.Property(e => e.LastRunAt).HasColumnName("lastRunAt");
            entity.Property(e => e.LastRunOutcome).HasColumnName("lastRunOutcome");
            entity.Property(e => e.LastRunMode).HasColumnName("lastRunMode");
            entity.Property(e => e.LastRunSkippedReason).HasColumnName("lastRunSkippedReason");
            entity.Property(e => e.LastRunError).HasColumnName("lastRunError");
            entity.Property(e => e.LastRunErrorCode).HasColumnName("lastRunErrorCode");
            entity.Property(e => e.LastRunErrorUserActionable)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("lastRunErrorUserActionable");
            entity.Property(e => e.LastRunTracesProcessed)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastRunTracesProcessed");
            entity.Property(e => e.LastRunTopicsCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastRunTopicsCount");
            entity.Property(e => e.LastRunSubtopicsCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastRunSubtopicsCount");
            entity.Property(e => e.LastRunPages)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastRunPages");
            entity.Property(e => e.InProgressRunId).HasColumnName("inProgressRunId");
            entity.Property(e => e.InProgressTraces)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("inProgressTraces");
            entity.Property(e => e.InProgressPages)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("inProgressPages");
            entity.Property(e => e.InProgressStartedAt).HasColumnName("inProgressStartedAt");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<TopicModelProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TopicModelProjection_pkey");

            entity.ToTable("TopicModelProjection");

            entity.HasIndex(e => e.ProjectId, "TopicModelProjection_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<TopicClusteringRunHistoryProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TopicClusteringRunHistoryProjection_pkey");

            entity.ToTable("TopicClusteringRunHistoryProjection");

            entity.HasIndex(e => e.ProjectId, "TopicClusteringRunHistoryProjection_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Runs)
                .HasColumnType("jsonb")
                .HasColumnName("runs");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<IngestionPullRunProjection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IngestionPullRunProjection_pkey");

            entity.ToTable("IngestionPullRunProjection");

            entity.HasIndex(e => e.SourceId, "IngestionPullRunProjection_sourceId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "IngestionPullRunProjection_projectId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SourceId).HasColumnName("sourceId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.Cron).HasColumnName("cron");
            entity.Property(e => e.Cursor).HasColumnName("cursor");
            entity.Property(e => e.LastRunAt).HasColumnName("lastRunAt");
            entity.Property(e => e.LastRunOutcome).HasColumnName("lastRunOutcome");
            entity.Property(e => e.LastRunEventCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastRunEventCount");
            entity.Property(e => e.LastRunError).HasColumnName("lastRunError");
            entity.Property(e => e.LastRunErrorCode).HasColumnName("lastRunErrorCode");
            entity.Property(e => e.ConsecutiveErrors)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutiveErrors");
            entity.Property(e => e.LastSuccessAt).HasColumnName("lastSuccessAt");
            entity.Property(e => e.LastRunScheduledFor).HasColumnName("lastRunScheduledFor");
            entity.Property(e => e.LastReadThroughAt).HasColumnName("lastReadThroughAt");
            entity.Property(e => e.LastRunCompleteness).HasColumnName("lastRunCompleteness");
            entity.Property(e => e.LastAgentsListingAt).HasColumnName("lastAgentsListingAt");
            entity.Property(e => e.LastAgentsListingOutcome).HasColumnName("lastAgentsListingOutcome");
            entity.Property(e => e.LastAgentsListingCount).HasColumnName("lastAgentsListingCount");
            entity.Property(e => e.LastAgentsListingReason).HasColumnName("lastAgentsListingReason");
            entity.Property(e => e.LastAgentsListingStatus).HasColumnName("lastAgentsListingStatus");
            entity.Property(e => e.LastPeopleListingAt).HasColumnName("lastPeopleListingAt");
            entity.Property(e => e.LastPeopleListingOutcome).HasColumnName("lastPeopleListingOutcome");
            entity.Property(e => e.LastPeopleDirectoryCount).HasColumnName("lastPeopleDirectoryCount");
            entity.Property(e => e.LastPeopleWithheldCount).HasColumnName("lastPeopleWithheldCount");
            entity.Property(e => e.LastPeopleListingReason).HasColumnName("lastPeopleListingReason");
            entity.Property(e => e.LastPeopleListingStatus).HasColumnName("lastPeopleListingStatus");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.OccurredAt).HasColumnName("occurredAt");
            entity.Property(e => e.AcceptedAt).HasColumnName("acceptedAt");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectionVersion).HasColumnName("projectionVersion");
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ScheduledJob_pkey");

            entity.ToTable("ScheduledJob");

            entity.HasIndex(e => new { e.Active, e.NextRunAt }, "ScheduledJob_active_nextRunAt_idx");

            entity.HasIndex(e => e.ProjectId, "ScheduledJob_projectId_idx");

            entity.HasIndex(e => new { e.TargetType, e.TargetId }, "ScheduledJob_targetType_targetId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.Cron).HasColumnName("cron");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.NextRunAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextRunAt");
            entity.Property(e => e.LastSlot)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSlot");
            entity.Property(e => e.CurrentSlot)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentSlot");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.LastError).HasColumnName("lastError");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<BugReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BugReport_pkey");

            entity.ToTable("BugReport");

            entity.HasIndex(e => e.CreatedAt, "BugReport_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.SessionData).HasColumnName("sessionData");
            entity.Property(e => e.SessionTruncated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sessionTruncated");
            entity.Property(e => e.Agent).HasColumnName("agent");
            entity.Property(e => e.ContactEmail).HasColumnName("contactEmail");
            entity.Property(e => e.CliVersion).HasColumnName("cliVersion");
            entity.Property(e => e.LinkedProjectId).HasColumnName("linkedProjectId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<IdempotencyReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IdempotencyReceipt_pkey");

            entity.ToTable("IdempotencyReceipt");

            entity.HasIndex(e => e.ExpiresAt, "IdempotencyReceipt_expiresAt_idx");

            entity.HasIndex(e => new { e.ScopeId, e.Key }, "IdempotencyReceipt_scopeId_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.ScopeId).HasColumnName("scopeId");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.ClaimId).HasColumnName("claimId");
            entity.Property(e => e.RequestFingerprint).HasColumnName("requestFingerprint");
            entity.Property(e => e.ResponseStatus).HasColumnName("responseStatus");
            entity.Property(e => e.ResponseBody).HasColumnName("responseBody");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.HeartbeatAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("heartbeatAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
        });

        modelBuilder.Entity<GatewayRealtimeSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayRealtimeSession_pkey");

            entity.ToTable("GatewayRealtimeSession");

            entity.HasIndex(e => new { e.VirtualKeyId, e.Status, e.MintedAt }, "GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Status, e.MintedAt }, "GatewayRealtimeSession_projectId_status_mintedAt_idx");

            entity.HasIndex(e => new { e.Status, e.Metering, e.MintedAt }, "GatewayRealtimeSession_status_metering_mintedAt_idx");

            entity.HasIndex(e => new { e.Vendor, e.VendorConversationId }, "GatewayRealtimeSession_vendor_vendorConversationId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.VirtualKeyId).HasColumnName("virtualKeyId");
            entity.Property(e => e.ModelProviderId).HasColumnName("modelProviderId");
            entity.Property(e => e.Vendor).HasColumnName("vendor");
            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.RequestedModel).HasColumnName("requestedModel");
            entity.Property(e => e.VendorConversationId).HasColumnName("vendorConversationId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayRealtimeSessionStatus.Open)
                .HasColumnName("status");
            entity.Property(e => e.MintedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("mintedAt");
            entity.Property(e => e.ClosedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("closedAt");
            entity.Property(e => e.CloseReason).HasColumnName("closeReason");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.VendorCostRaw)
                .HasColumnType("jsonb")
                .HasColumnName("vendorCostRaw");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Metering).HasColumnName("metering");
            entity.Property(e => e.CredentialExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("credentialExpiresAt");
            entity.Property(e => e.TranscriptionModel).HasColumnName("transcriptionModel");
            entity.Property(e => e.EndUserId).HasColumnName("endUserId");
            entity.Property(e => e.LastReportAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReportAt");
            entity.Property(e => e.ReportedCostNanoUsd)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("reportedCostNanoUsd");
            entity.Property(e => e.ReportCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("reportCount");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<GatewayRealtimeSessionReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GatewayRealtimeSessionReport_pkey");

            entity.ToTable("GatewayRealtimeSessionReport");

            entity.HasIndex(e => e.ProjectId, "GatewayRealtimeSessionReport_projectId_idx");

            entity.HasIndex(e => new { e.SessionId, e.ReportKey }, "GatewayRealtimeSessionReport_sessionId_reportKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.ReportKey).HasColumnName("reportKey");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Usage)
                .HasColumnType("jsonb")
                .HasColumnName("usage");
            entity.Property(e => e.CostNanoUsd).HasColumnName("costNanoUsd");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<SystemMigrationTenantState>(entity =>
        {
            entity.HasKey(e => new { e.MigrationName, e.TenantId }).HasName("SystemMigrationTenantState_pkey");

            entity.ToTable("SystemMigrationTenantState");

            entity.HasIndex(e => new { e.MigrationName, e.Status }, "SystemMigrationTenantState_migrationName_status_idx");

            entity.Property(e => e.MigrationName).HasColumnName("migrationName");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Report)
                .HasColumnType("jsonb")
                .HasColumnName("report");
            entity.Property(e => e.OccurredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("occurredAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SystemMigrationEnrollment>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationId, e.MigrationName }).HasName("SystemMigrationEnrollment_pkey");

            entity.ToTable("SystemMigrationEnrollment");

            entity.HasIndex(e => e.MigrationName, "SystemMigrationEnrollment_migrationName_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.MigrationName).HasColumnName("migrationName");
            entity.Property(e => e.EnrolledByUserId).HasColumnName("enrolledByUserId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<StoredObject>(entity =>
        {
            entity.HasKey(e => new { e.TenantId, e.Id }).HasName("StoredObject_pkey");

            entity.ToTable("StoredObject");

            entity.HasIndex(e => new { e.TenantId, e.Status, e.Id }, "StoredObject_tenantId_status_id_idx");

            entity.HasIndex(e => new { e.OwnerKind, e.OwnerId, e.Status }, "StoredObject_ownerKind_ownerId_status_idx");

            entity.HasIndex(e => new { e.Status, e.ExpiresAt }, "StoredObject_status_expiresAt_idx");

            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.OwnerKind).HasColumnName("ownerKind");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.Sha256).HasColumnName("sha256");
            entity.Property(e => e.SizeBytes).HasColumnName("sizeBytes");
            entity.Property(e => e.MediaType).HasColumnName("mediaType");
            entity.Property(e => e.MediaTypeVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("mediaTypeVerified");
            entity.Property(e => e.StorageProvider).HasColumnName("storageProvider");
            entity.Property(e => e.StorageDestinationId).HasColumnName("storageDestinationId");
            entity.Property(e => e.StorageProviderRelativeId).HasColumnName("storageProviderRelativeId");
            entity.Property(e => e.Generation)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("generation");
            entity.Property(e => e.Audiences)
                .HasColumnType("jsonb")
                .HasColumnName("audiences");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.AvailableAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("availableAt");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.LegacyFingerprint).HasColumnName("legacyFingerprint");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<DiscoveredPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DiscoveredPerson_pkey");

            entity.ToTable("DiscoveredPerson");

            entity.HasIndex(e => new { e.OrganizationId, e.LastSeenAt }, "DiscoveredPerson_organizationId_lastSeenAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Provider, e.RawActorId }, "DiscoveredPerson_organizationId_provider_rawActorId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.RawActorId).HasColumnName("rawActorId");
            entity.Property(e => e.DisplayText).HasColumnName("displayText");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Department).HasColumnName("department");
            entity.Property(e => e.FirstSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
            entity.Property(e => e.LastSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.ErasedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasedAt");
            entity.Property(e => e.MoneyRowsPendingAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("moneyRowsPendingAt");
            entity.Property(e => e.MoneyRebuildSince).HasColumnName("moneyRebuildSince");
            entity.Property(e => e.SuspendedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("suspendedAt");
            entity.Property(e => e.SuspendedReason).HasColumnName("suspendedReason");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<DiscoveredAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DiscoveredAgent_pkey");

            entity.ToTable("DiscoveredAgent");

            entity.HasIndex(e => new { e.OrganizationId, e.LastSeenAt }, "DiscoveredAgent_organizationId_lastSeenAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Provider, e.RawAgentId }, "DiscoveredAgent_organizationId_provider_rawAgentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.RawAgentId).HasColumnName("rawAgentId");
            entity.Property(e => e.DisplayText).HasColumnName("displayText");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.FirstSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
            entity.Property(e => e.LastSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<IdentityMatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IdentityMatch_pkey");

            entity.ToTable("IdentityMatch");

            entity.HasIndex(e => new { e.OrganizationId, e.DiscoveredPersonId }, "IdentityMatch_organizationId_discoveredPersonId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "IdentityMatch_organizationId_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.DiscoveredPersonId).HasColumnName("discoveredPersonId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EvidenceKind).HasColumnName("evidenceKind");
            entity.Property(e => e.ValidFrom)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validFrom");
            entity.Property(e => e.ValidTo)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("validTo");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<GovernanceTenantHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GovernanceTenantHistory_pkey");

            entity.ToTable("GovernanceTenantHistory");

            entity.HasIndex(e => e.OrganizationId, "GovernanceTenantHistory_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.TenantId }, "GovernanceTenantHistory_organizationId_tenantId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.TenantId).HasColumnName("tenantId");
            entity.Property(e => e.FirstUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstUsedAt");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
        });

        modelBuilder.Entity<ErasedIdentifierSuppression>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ErasedIdentifierSuppression_pkey");

            entity.ToTable("ErasedIdentifierSuppression");

            entity.HasIndex(e => e.OrganizationId, "ErasedIdentifierSuppression_organizationId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Provider, e.IdentifierHash }, "ErasedIdentifierSuppression_organizationId_provider_identif_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.IdentifierHash).HasColumnName("identifierHash");
            entity.Property(e => e.ErasedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasedAt");
        });

        modelBuilder.Entity<IdentityMatchSuggestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IdentityMatchSuggestion_pkey");

            entity.ToTable("IdentityMatchSuggestion");

            entity.HasIndex(e => new { e.OrganizationId, e.Score }, "IdentityMatchSuggestion_organizationId_score_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.DiscoveredPersonId, e.UserId }, "IdentityMatchSuggestion_organizationId_discoveredPersonId_u_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.DiscoveredPersonId).HasColumnName("discoveredPersonId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.ComputedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("computedAt");
        });

        modelBuilder.Entity<ConnectedSeatChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedSeatChange_pkey");

            entity.ToTable("ConnectedSeatChange");

            entity.HasIndex(e => e.LicenseId, "ConnectedSeatChange_licenseId_key").IsUnique();

            entity.HasIndex(e => e.AccountId, "ConnectedSeatChange_accountId_idx");

            entity.HasIndex(e => e.OrganizationId, "ConnectedSeatChange_organizationId_idx");

            entity.HasIndex(e => e.State, "ConnectedSeatChange_state_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.LicenseId).HasColumnName("licenseId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("changedAt");
            entity.Property(e => e.AddedSeats).HasColumnName("addedSeats");
            entity.Property(e => e.UnitAmountCents).HasColumnName("unitAmountCents");
            entity.Property(e => e.AmountCents).HasColumnName("amountCents");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.StripeInvoiceId).HasColumnName("stripeInvoiceId");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ConnectedStatement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedStatement_pkey");

            entity.ToTable("ConnectedStatement");

            entity.HasIndex(e => e.AccountId, "ConnectedStatement_accountId_idx");

            entity.HasIndex(e => new { e.AccountId, e.Month }, "ConnectedStatement_accountId_month_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Month)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("month");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
        });

        modelBuilder.Entity<LicenseRenewalClaim>(entity =>
        {
            entity.HasKey(e => e.StripeInvoiceId).HasName("LicenseRenewalClaim_pkey");

            entity.ToTable("LicenseRenewalClaim");

            entity.Property(e => e.StripeInvoiceId).HasColumnName("stripeInvoiceId");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.ClaimToken).HasColumnName("claimToken");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimedAt");
            entity.Property(e => e.IssuedLicense)
                .HasColumnType("jsonb")
                .HasColumnName("issuedLicense");
            entity.Property(e => e.DeliveredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deliveredAt");
            entity.Property(e => e.LicenseId).HasColumnName("licenseId");
        });

        modelBuilder.Entity<InstanceIdentity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InstanceIdentity_pkey");

            entity.ToTable("InstanceIdentity");

            entity.HasIndex(e => e.InstanceId, "InstanceIdentity_instanceId_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("self")
                .HasColumnName("id");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.LastReportAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReportAt");
            entity.Property(e => e.LastReportError).HasColumnName("lastReportError");
            entity.Property(e => e.OptionalMetricsOptOut)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("optionalMetricsOptOut");
            entity.Property(e => e.HostnameOptOut)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hostnameOptOut");
            entity.Property(e => e.UpgradeDiagnosticsOptOut)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("upgradeDiagnosticsOptOut");
        });

        modelBuilder.Entity<SelfHostedInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelfHostedInstance_pkey");

            entity.ToTable("SelfHostedInstance");

            entity.HasIndex(e => e.InstanceId, "SelfHostedInstance_instanceId_key").IsUnique();

            entity.HasIndex(e => e.LastSeenAt, "SelfHostedInstance_lastSeenAt_idx");

            entity.HasIndex(e => e.OrganizationId, "SelfHostedInstance_organizationId_idx");

            entity.HasIndex(e => e.IssuedLicenseId, "SelfHostedInstance_issuedLicenseId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.FirstSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
            entity.Property(e => e.LastSeenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.InstallMethod).HasColumnName("installMethod");
            entity.Property(e => e.ChartVersion).HasColumnName("chartVersion");
            entity.Property(e => e.Hostname).HasColumnName("hostname");
            entity.Property(e => e.Environment).HasColumnName("environment");
            entity.Property(e => e.InstalledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("installedAt");
            entity.Property(e => e.ReportSchemaVersion).HasColumnName("reportSchemaVersion");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.IssuedLicenseId).HasColumnName("issuedLicenseId");
            entity.Property(e => e.UserEmailDomains)
                .HasColumnType("jsonb")
                .HasColumnName("userEmailDomains");
            entity.Property(e => e.UserDomains)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("userDomains");
            entity.Property(e => e.LatestReport)
                .HasColumnType("jsonb")
                .HasColumnName("latestReport");
            entity.Property(e => e.OptionalMetricsReported)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("optionalMetricsReported");
            entity.Property(e => e.HostnameReported)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("hostnameReported");
            entity.Property(e => e.ReportCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("reportCount");
            entity.Property(e => e.LastUnknownFields)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastUnknownFields");
            entity.Property(e => e.RaisedSignals)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("raisedSignals");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SelfHostedInstanceDiagnostic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelfHostedInstanceDiagnostic_pkey");

            entity.ToTable("SelfHostedInstanceDiagnostic");

            entity.HasIndex(e => new { e.InstanceId, e.ReceivedAt }, "SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.StepId).HasColumnName("stepId");
            entity.Property(e => e.StepDescription).HasColumnName("stepDescription");
            entity.Property(e => e.Release).HasColumnName("release");
            entity.Property(e => e.Attempt).HasColumnName("attempt");
            entity.Property(e => e.Trigger).HasColumnName("trigger");
            entity.Property(e => e.ByteLength).HasColumnName("byteLength");
            entity.Property(e => e.ObjectKey).HasColumnName("objectKey");
            entity.Property(e => e.ReceivedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("receivedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.SlackAnnouncedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("slackAnnouncedAt");
        });

        modelBuilder.Entity<SelfHostedNotice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelfHostedNotice_pkey");

            entity.ToTable("SelfHostedNotice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Severity).HasColumnName("severity");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FixedIn).HasColumnName("fixedIn");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.PublishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publishedAt");
            entity.Property(e => e.PublishedBy).HasColumnName("publishedBy");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<SelfHostedInstanceReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SelfHostedInstanceReport_pkey");

            entity.ToTable("SelfHostedInstanceReport");

            entity.HasIndex(e => new { e.InstanceId, e.ReceivedAt }, "SelfHostedInstanceReport_instanceId_receivedAt_idx");

            entity.HasIndex(e => e.ReceivedAt, "SelfHostedInstanceReport_receivedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.InstanceId).HasColumnName("instanceId");
            entity.Property(e => e.ReceivedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("receivedAt");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.ReportSchemaVersion).HasColumnName("reportSchemaVersion");
            entity.Property(e => e.UnknownFields)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unknownFields");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
        });

        modelBuilder.Entity<WebPushSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebPushSubscription_pkey");

            entity.ToTable("WebPushSubscription");

            entity.HasIndex(e => e.Endpoint, "WebPushSubscription_endpoint_key").IsUnique();

            entity.HasIndex(e => e.UserId, "WebPushSubscription_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Nanoid21Generator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.P256dh).HasColumnName("p256dh");
            entity.Property(e => e.Auth).HasColumnName("auth");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.LastSuccessAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSuccessAt");
        });

        modelBuilder.Entity<WebPushVapidKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebPushVapidKey_pkey");

            entity.ToTable("WebPushVapidKey");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("self")
                .HasColumnName("id");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
            entity.Property(e => e.PrivateKeyEncrypted).HasColumnName("privateKeyEncrypted");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<SignInProviderMount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SignInProviderMount_pkey");

            entity.ToTable("SignInProviderMount");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("self")
                .HasColumnName("id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Mounted).HasColumnName("mounted");
            entity.Property(e => e.LastAppliedEventId).HasColumnName("lastAppliedEventId");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        StampUpdatedAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        StampUpdatedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void StampUpdatedAt()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<Account>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AccountCredential>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Session>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VerificationToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Identifier>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IdentitySignInSwitch>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IdentityProjectionCursor>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TwoFactor>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SignInAttemptLock>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Passkey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MfaEnrollment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SsoConnectionRegistrationSlot>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SsoCredential>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TeamUser>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationUser>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SeatRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Team>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Organization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IssuedLicense>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationLicense>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ActivationCode>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConnectedBillingAccount>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConnectedInvoice>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScimExternalId>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScimUserResource>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Project>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Department>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PlatformToolPolicy>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationInvite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Monitor>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Cost>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Topic>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Dataset>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DatasetRecord>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Dashboard>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DashboardsV2>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DashboardWidget>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SavedView>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomGraph>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BatchEvaluation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Trigger>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SlackIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SlackConnectionClaim>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WebhookEndpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Experiment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExperimentVersion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Annotation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ModelProvider>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ModelDefaultConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GithubInstallation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GithubPullRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GithubBranchPullRequestCheck>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LangyTurnRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LangyActiveTurn>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectSecret>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TriggerSent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TriggerLatestEvaluation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AnnotationScore>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AnnotationQueue>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AnnotationQueueItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ShareLink>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TraceEditOverlay>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TraceIngestSourceBilling>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RetentionPolicy>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataPrivacyPolicy>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomLLMModelCost>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Workflow>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowVersion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LlmPromptConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PromptTagAssignment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PromptTag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomRole>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Notification>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Agent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AgentVersionLabel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Evaluator>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EvaluatorVersionLabel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Scenario>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SimulationSuite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Subscription>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Invoice>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InvoiceItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Group>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthzEngineSwitch>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthzOrganizationPolicy>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RoleBinding>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Grant>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GrantUsage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Role>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ApiKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<NurturingOrganization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BillingMeterCheckpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VirtualKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RoutingPolicy>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayGuardrail>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IngestionSource>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AnomalyRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AiToolEntry>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IngestionTemplate>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayConnectUpstream>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayBudget>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayBudgetBucketBoundary>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayCacheRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeatureFlag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeatureFlagExperimentSetting>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScheduledJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayRealtimeSession>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SystemMigrationTenantState>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<StoredObject>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DiscoveredPerson>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DiscoveredAgent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConnectedSeatChange>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SelfHostedInstance>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SelfHostedNotice>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SignInProviderMount>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class Nanoid21Generator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => RandomNumberGenerator.GetString("_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", 21);
}

file sealed class UtcNowGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
}

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}

file sealed class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Utc), v => v)
    {
    }
}
