#nullable enable

using System;
using System.Collections.Generic;
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

namespace Trigger.Models;

public partial class TriggerContext : DbContext
{
    public TriggerContext(DbContextOptions<TriggerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<MfaBackupCode> MfaBackupCodes { get; set; }

    public virtual DbSet<InvitationCode> InvitationCodes { get; set; }

    public virtual DbSet<AuthorizationCode> AuthorizationCodes { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<OrganizationAccessToken> OrganizationAccessTokens { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrgMember> OrgMembers { get; set; }

    public virtual DbSet<OrgMemberInvite> OrgMemberInvites { get; set; }

    public virtual DbSet<RuntimeEnvironment> RuntimeEnvironments { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<RevokedApiKey> RevokedApiKeys { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<EnvironmentEncryptionPolicy> EnvironmentEncryptionPolicies { get; set; }

    public virtual DbSet<EnvironmentEncryptionKey> EnvironmentEncryptionKeys { get; set; }

    public virtual DbSet<SecretReference> SecretReferences { get; set; }

    public virtual DbSet<SecretStore> SecretStores { get; set; }

    public virtual DbSet<OidcSigningKey> OidcSigningKeys { get; set; }

    public virtual DbSet<DataMigration> DataMigrations { get; set; }

    public virtual DbSet<BackgroundWorker> BackgroundWorkers { get; set; }

    public virtual DbSet<BackgroundWorkerFile> BackgroundWorkerFiles { get; set; }

    public virtual DbSet<Prompt> Prompts { get; set; }

    public virtual DbSet<PromptVersion> PromptVersions { get; set; }

    public virtual DbSet<BackgroundWorkerTask> BackgroundWorkerTasks { get; set; }

    public virtual DbSet<WebhookEndpoint> WebhookEndpoints { get; set; }

    public virtual DbSet<WebhookDelivery> WebhookDeliveries { get; set; }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<AlertEvent> AlertEvents { get; set; }

    public virtual DbSet<AlertOccurrenceSeen> AlertOccurrenceSeens { get; set; }

    public virtual DbSet<AlertDelivery> AlertDeliveries { get; set; }

    public virtual DbSet<PlaygroundConversation> PlaygroundConversations { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionRun> SessionRuns { get; set; }

    public virtual DbSet<TaskRun> TaskRuns { get; set; }

    public virtual DbSet<TaskRunTemplate> TaskRunTemplates { get; set; }

    public virtual DbSet<TaskRunExecutionSnapshot> TaskRunExecutionSnapshots { get; set; }

    public virtual DbSet<TaskRunCheckpoint> TaskRunCheckpoints { get; set; }

    public virtual DbSet<Waitpoint> Waitpoints { get; set; }

    public virtual DbSet<TaskRunWaitpoint> TaskRunWaitpoints { get; set; }

    public virtual DbSet<WaitpointTag> WaitpointTags { get; set; }

    public virtual DbSet<FeatureFlag> FeatureFlags { get; set; }

    public virtual DbSet<WorkerInstance> WorkerInstances { get; set; }

    public virtual DbSet<WorkerInstanceGroup> WorkerInstanceGroups { get; set; }

    public virtual DbSet<WorkerGroupToken> WorkerGroupTokens { get; set; }

    public virtual DbSet<TaskRunTag> TaskRunTags { get; set; }

    public virtual DbSet<TaskRunDependency> TaskRunDependencies { get; set; }

    public virtual DbSet<TaskRunCounter> TaskRunCounters { get; set; }

    public virtual DbSet<TaskRunNumberCounter> TaskRunNumberCounters { get; set; }

    public virtual DbSet<TaskRunAttempt> TaskRunAttempts { get; set; }

    public virtual DbSet<TaskEvent> TaskEvents { get; set; }

    public virtual DbSet<TaskQueue> TaskQueues { get; set; }

    public virtual DbSet<BatchTaskRun> BatchTaskRuns { get; set; }

    public virtual DbSet<BatchTaskRunItem> BatchTaskRunItems { get; set; }

    public virtual DbSet<BatchTaskRunError> BatchTaskRunErrors { get; set; }

    public virtual DbSet<EnvironmentVariable> EnvironmentVariables { get; set; }

    public virtual DbSet<EnvironmentVariableValue> EnvironmentVariableValues { get; set; }

    public virtual DbSet<Checkpoint> Checkpoints { get; set; }

    public virtual DbSet<CheckpointRestoreEvent> CheckpointRestoreEvents { get; set; }

    public virtual DbSet<WorkerDeployment> WorkerDeployments { get; set; }

    public virtual DbSet<WorkerDeploymentPromotion> WorkerDeploymentPromotions { get; set; }

    public virtual DbSet<TaskSchedule> TaskSchedules { get; set; }

    public virtual DbSet<TaskScheduleInstance> TaskScheduleInstances { get; set; }

    public virtual DbSet<RuntimeEnvironmentSession> RuntimeEnvironmentSessions { get; set; }

    public virtual DbSet<ProjectAlertChannel> ProjectAlertChannels { get; set; }

    public virtual DbSet<ProjectAlert> ProjectAlerts { get; set; }

    public virtual DbSet<ProjectAlertStorage> ProjectAlertStorages { get; set; }

    public virtual DbSet<OrganizationIntegration> OrganizationIntegrations { get; set; }

    public virtual DbSet<OrganizationProjectIntegration> OrganizationProjectIntegrations { get; set; }

    public virtual DbSet<BulkActionGroup> BulkActionGroups { get; set; }

    public virtual DbSet<BulkActionItem> BulkActionItems { get; set; }

    public virtual DbSet<RealtimeStreamChunk> RealtimeStreamChunks { get; set; }

    public virtual DbSet<TaskEventPartitioned> TaskEventPartitioneds { get; set; }

    public virtual DbSet<GithubAppInstallation> GithubAppInstallations { get; set; }

    public virtual DbSet<GithubRepository> GithubRepositories { get; set; }

    public virtual DbSet<TrustedDeployRepository> TrustedDeployRepositories { get; set; }

    public virtual DbSet<TrustedDeployEnvironment> TrustedDeployEnvironments { get; set; }

    public virtual DbSet<ConnectedGithubRepository> ConnectedGithubRepositories { get; set; }

    public virtual DbSet<ImpersonationAuditLog> ImpersonationAuditLogs { get; set; }

    public virtual DbSet<SupportAccessRequest> SupportAccessRequests { get; set; }

    public virtual DbSet<CustomerQuery> CustomerQueries { get; set; }

    public virtual DbSet<IntegrationDeployment> IntegrationDeployments { get; set; }

    public virtual DbSet<MetricsDashboard> MetricsDashboards { get; set; }

    public virtual DbSet<LlmModel> LlmModels { get; set; }

    public virtual DbSet<LlmPricingTier> LlmPricingTiers { get; set; }

    public virtual DbSet<LlmPrice> LlmPrices { get; set; }

    public virtual DbSet<PlatformNotification> PlatformNotifications { get; set; }

    public virtual DbSet<PlatformNotificationInteraction> PlatformNotificationInteractions { get; set; }

    public virtual DbSet<ErrorGroupState> ErrorGroupStates { get; set; }

    public virtual DbSet<TaskIdentifier> TaskIdentifiers { get; set; }

    public virtual DbSet<OrganizationDataStore> OrganizationDataStores { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<AuthenticationMethod>("AuthenticationMethod");
        npgsql.MapEnum<OrganizationAccessTokenType>("OrganizationAccessTokenType");
        npgsql.MapEnum<OrgMemberRole>("OrgMemberRole");
        npgsql.MapEnum<RuntimeEnvironmentType>("RuntimeEnvironmentType");
        npgsql.MapEnum<EnvironmentPauseSource>("EnvironmentPauseSource");
        npgsql.MapEnum<ProjectVersion>("ProjectVersion");
        npgsql.MapEnum<EnvironmentKeyWrappingProvider>("EnvironmentKeyWrappingProvider");
        npgsql.MapEnum<SecretStoreProvider>("SecretStoreProvider");
        npgsql.MapEnum<OidcSigningKeyStatus>("OidcSigningKeyStatus");
        npgsql.MapEnum<OidcSigningKeyPurpose>("OidcSigningKeyPurpose");
        npgsql.MapEnum<TaskTriggerSource>("TaskTriggerSource");
        npgsql.MapEnum<WebhookEndpointStatus>("WebhookEndpointStatus");
        npgsql.MapEnum<WebhookDeliveryStatus>("WebhookDeliveryStatus");
        npgsql.MapEnum<TaskRunStatus>("TaskRunStatus");
        npgsql.MapEnum<RunEngineVersion>("RunEngineVersion");
        npgsql.MapEnum<TaskRunExecutionStatus>("TaskRunExecutionStatus");
        npgsql.MapEnum<TaskRunCheckpointType>("TaskRunCheckpointType");
        npgsql.MapEnum<WaitpointType>("WaitpointType");
        npgsql.MapEnum<WaitpointStatus>("WaitpointStatus");
        npgsql.MapEnum<WorkerInstanceGroupType>("WorkerInstanceGroupType");
        npgsql.MapEnum<WorkloadType>("WorkloadType");
        npgsql.MapEnum<TaskRunAttemptStatus>("TaskRunAttemptStatus");
        npgsql.MapEnum<TaskEventLevel>("TaskEventLevel");
        npgsql.MapEnum<TaskEventKind>("TaskEventKind");
        npgsql.MapEnum<TaskEventStatus>("TaskEventStatus");
        npgsql.MapEnum<TaskQueueType>("TaskQueueType");
        npgsql.MapEnum<TaskQueueVersion>("TaskQueueVersion");
        npgsql.MapEnum<TaskQueueConcurrencyVersion>("TaskQueueConcurrencyVersion");
        npgsql.MapEnum<TaskQueueRole>("TaskQueueRole");
        npgsql.MapEnum<BatchTaskRunStatus>("BatchTaskRunStatus");
        npgsql.MapEnum<BatchTaskRunItemStatus>("BatchTaskRunItemStatus");
        npgsql.MapEnum<CheckpointType>("CheckpointType");
        npgsql.MapEnum<CheckpointRestoreEventType>("CheckpointRestoreEventType");
        npgsql.MapEnum<WorkerDeploymentType>("WorkerDeploymentType");
        npgsql.MapEnum<WorkerDeploymentStatus>("WorkerDeploymentStatus");
        npgsql.MapEnum<ScheduleType>("ScheduleType");
        npgsql.MapEnum<ScheduleGeneratorType>("ScheduleGeneratorType");
        npgsql.MapEnum<ProjectAlertChannelType>("ProjectAlertChannelType");
        npgsql.MapEnum<ProjectAlertType>("ProjectAlertType");
        npgsql.MapEnum<ProjectAlertStatus>("ProjectAlertStatus");
        npgsql.MapEnum<IntegrationService>("IntegrationService");
        npgsql.MapEnum<BulkActionType>("BulkActionType");
        npgsql.MapEnum<BulkActionStatus>("BulkActionStatus");
        npgsql.MapEnum<BulkActionNotificationType>("BulkActionNotificationType");
        npgsql.MapEnum<BulkActionItemStatus>("BulkActionItemStatus");
        npgsql.MapEnum<GithubRepositorySelection>("GithubRepositorySelection");
        npgsql.MapEnum<TrustedDeployScope>("TrustedDeployScope");
        npgsql.MapEnum<ImpersonationAuditLogAction>("ImpersonationAuditLogAction");
        npgsql.MapEnum<SupportAccessMode>("SupportAccessMode");
        npgsql.MapEnum<SupportAccessRequestStatus>("SupportAccessRequestStatus");
        npgsql.MapEnum<CustomerQuerySource>("CustomerQuerySource");
        npgsql.MapEnum<CustomerQueryScope>("CustomerQueryScope");
        npgsql.MapEnum<PlatformNotificationSurface>("PlatformNotificationSurface");
        npgsql.MapEnum<PlatformNotificationScope>("PlatformNotificationScope");
        npgsql.MapEnum<ErrorGroupStatus>("ErrorGroupStatus");
        npgsql.MapEnum<DataStoreKind>("DataStoreKind");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<AuthenticationMethod>("AuthenticationMethod");
        dataSource.MapEnum<OrganizationAccessTokenType>("OrganizationAccessTokenType");
        dataSource.MapEnum<OrgMemberRole>("OrgMemberRole");
        dataSource.MapEnum<RuntimeEnvironmentType>("RuntimeEnvironmentType");
        dataSource.MapEnum<EnvironmentPauseSource>("EnvironmentPauseSource");
        dataSource.MapEnum<ProjectVersion>("ProjectVersion");
        dataSource.MapEnum<EnvironmentKeyWrappingProvider>("EnvironmentKeyWrappingProvider");
        dataSource.MapEnum<SecretStoreProvider>("SecretStoreProvider");
        dataSource.MapEnum<OidcSigningKeyStatus>("OidcSigningKeyStatus");
        dataSource.MapEnum<OidcSigningKeyPurpose>("OidcSigningKeyPurpose");
        dataSource.MapEnum<TaskTriggerSource>("TaskTriggerSource");
        dataSource.MapEnum<WebhookEndpointStatus>("WebhookEndpointStatus");
        dataSource.MapEnum<WebhookDeliveryStatus>("WebhookDeliveryStatus");
        dataSource.MapEnum<TaskRunStatus>("TaskRunStatus");
        dataSource.MapEnum<RunEngineVersion>("RunEngineVersion");
        dataSource.MapEnum<TaskRunExecutionStatus>("TaskRunExecutionStatus");
        dataSource.MapEnum<TaskRunCheckpointType>("TaskRunCheckpointType");
        dataSource.MapEnum<WaitpointType>("WaitpointType");
        dataSource.MapEnum<WaitpointStatus>("WaitpointStatus");
        dataSource.MapEnum<WorkerInstanceGroupType>("WorkerInstanceGroupType");
        dataSource.MapEnum<WorkloadType>("WorkloadType");
        dataSource.MapEnum<TaskRunAttemptStatus>("TaskRunAttemptStatus");
        dataSource.MapEnum<TaskEventLevel>("TaskEventLevel");
        dataSource.MapEnum<TaskEventKind>("TaskEventKind");
        dataSource.MapEnum<TaskEventStatus>("TaskEventStatus");
        dataSource.MapEnum<TaskQueueType>("TaskQueueType");
        dataSource.MapEnum<TaskQueueVersion>("TaskQueueVersion");
        dataSource.MapEnum<TaskQueueConcurrencyVersion>("TaskQueueConcurrencyVersion");
        dataSource.MapEnum<TaskQueueRole>("TaskQueueRole");
        dataSource.MapEnum<BatchTaskRunStatus>("BatchTaskRunStatus");
        dataSource.MapEnum<BatchTaskRunItemStatus>("BatchTaskRunItemStatus");
        dataSource.MapEnum<CheckpointType>("CheckpointType");
        dataSource.MapEnum<CheckpointRestoreEventType>("CheckpointRestoreEventType");
        dataSource.MapEnum<WorkerDeploymentType>("WorkerDeploymentType");
        dataSource.MapEnum<WorkerDeploymentStatus>("WorkerDeploymentStatus");
        dataSource.MapEnum<ScheduleType>("ScheduleType");
        dataSource.MapEnum<ScheduleGeneratorType>("ScheduleGeneratorType");
        dataSource.MapEnum<ProjectAlertChannelType>("ProjectAlertChannelType");
        dataSource.MapEnum<ProjectAlertType>("ProjectAlertType");
        dataSource.MapEnum<ProjectAlertStatus>("ProjectAlertStatus");
        dataSource.MapEnum<IntegrationService>("IntegrationService");
        dataSource.MapEnum<BulkActionType>("BulkActionType");
        dataSource.MapEnum<BulkActionStatus>("BulkActionStatus");
        dataSource.MapEnum<BulkActionNotificationType>("BulkActionNotificationType");
        dataSource.MapEnum<BulkActionItemStatus>("BulkActionItemStatus");
        dataSource.MapEnum<GithubRepositorySelection>("GithubRepositorySelection");
        dataSource.MapEnum<TrustedDeployScope>("TrustedDeployScope");
        dataSource.MapEnum<ImpersonationAuditLogAction>("ImpersonationAuditLogAction");
        dataSource.MapEnum<SupportAccessMode>("SupportAccessMode");
        dataSource.MapEnum<SupportAccessRequestStatus>("SupportAccessRequestStatus");
        dataSource.MapEnum<CustomerQuerySource>("CustomerQuerySource");
        dataSource.MapEnum<CustomerQueryScope>("CustomerQueryScope");
        dataSource.MapEnum<PlatformNotificationSurface>("PlatformNotificationSurface");
        dataSource.MapEnum<PlatformNotificationScope>("PlatformNotificationScope");
        dataSource.MapEnum<ErrorGroupStatus>("ErrorGroupStatus");
        dataSource.MapEnum<DataStoreKind>("DataStoreKind");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<AuthenticationMethod>(name: "AuthenticationMethod");
        modelBuilder.HasPostgresEnum<OrganizationAccessTokenType>(name: "OrganizationAccessTokenType");
        modelBuilder.HasPostgresEnum<OrgMemberRole>(name: "OrgMemberRole");
        modelBuilder.HasPostgresEnum<RuntimeEnvironmentType>(name: "RuntimeEnvironmentType");
        modelBuilder.HasPostgresEnum<EnvironmentPauseSource>(name: "EnvironmentPauseSource");
        modelBuilder.HasPostgresEnum<ProjectVersion>(name: "ProjectVersion");
        modelBuilder.HasPostgresEnum<EnvironmentKeyWrappingProvider>(name: "EnvironmentKeyWrappingProvider");
        modelBuilder.HasPostgresEnum<SecretStoreProvider>(name: "SecretStoreProvider");
        modelBuilder.HasPostgresEnum<OidcSigningKeyStatus>(name: "OidcSigningKeyStatus");
        modelBuilder.HasPostgresEnum<OidcSigningKeyPurpose>(name: "OidcSigningKeyPurpose");
        modelBuilder.HasPostgresEnum<TaskTriggerSource>(name: "TaskTriggerSource");
        modelBuilder.HasPostgresEnum<WebhookEndpointStatus>(name: "WebhookEndpointStatus");
        modelBuilder.HasPostgresEnum<WebhookDeliveryStatus>(name: "WebhookDeliveryStatus");
        modelBuilder.HasPostgresEnum<TaskRunStatus>(name: "TaskRunStatus");
        modelBuilder.HasPostgresEnum<RunEngineVersion>(name: "RunEngineVersion");
        modelBuilder.HasPostgresEnum<TaskRunExecutionStatus>(name: "TaskRunExecutionStatus");
        modelBuilder.HasPostgresEnum<TaskRunCheckpointType>(name: "TaskRunCheckpointType");
        modelBuilder.HasPostgresEnum<WaitpointType>(name: "WaitpointType");
        modelBuilder.HasPostgresEnum<WaitpointStatus>(name: "WaitpointStatus");
        modelBuilder.HasPostgresEnum<WorkerInstanceGroupType>(name: "WorkerInstanceGroupType");
        modelBuilder.HasPostgresEnum<WorkloadType>(name: "WorkloadType");
        modelBuilder.HasPostgresEnum<TaskRunAttemptStatus>(name: "TaskRunAttemptStatus");
        modelBuilder.HasPostgresEnum<TaskEventLevel>(name: "TaskEventLevel");
        modelBuilder.HasPostgresEnum<TaskEventKind>(name: "TaskEventKind");
        modelBuilder.HasPostgresEnum<TaskEventStatus>(name: "TaskEventStatus");
        modelBuilder.HasPostgresEnum<TaskQueueType>(name: "TaskQueueType");
        modelBuilder.HasPostgresEnum<TaskQueueVersion>(name: "TaskQueueVersion");
        modelBuilder.HasPostgresEnum<TaskQueueConcurrencyVersion>(name: "TaskQueueConcurrencyVersion");
        modelBuilder.HasPostgresEnum<TaskQueueRole>(name: "TaskQueueRole");
        modelBuilder.HasPostgresEnum<BatchTaskRunStatus>(name: "BatchTaskRunStatus");
        modelBuilder.HasPostgresEnum<BatchTaskRunItemStatus>(name: "BatchTaskRunItemStatus");
        modelBuilder.HasPostgresEnum<CheckpointType>(name: "CheckpointType");
        modelBuilder.HasPostgresEnum<CheckpointRestoreEventType>(name: "CheckpointRestoreEventType");
        modelBuilder.HasPostgresEnum<WorkerDeploymentType>(name: "WorkerDeploymentType");
        modelBuilder.HasPostgresEnum<WorkerDeploymentStatus>(name: "WorkerDeploymentStatus");
        modelBuilder.HasPostgresEnum<ScheduleType>(name: "ScheduleType");
        modelBuilder.HasPostgresEnum<ScheduleGeneratorType>(name: "ScheduleGeneratorType");
        modelBuilder.HasPostgresEnum<ProjectAlertChannelType>(name: "ProjectAlertChannelType");
        modelBuilder.HasPostgresEnum<ProjectAlertType>(name: "ProjectAlertType");
        modelBuilder.HasPostgresEnum<ProjectAlertStatus>(name: "ProjectAlertStatus");
        modelBuilder.HasPostgresEnum<IntegrationService>(name: "IntegrationService");
        modelBuilder.HasPostgresEnum<BulkActionType>(name: "BulkActionType");
        modelBuilder.HasPostgresEnum<BulkActionStatus>(name: "BulkActionStatus");
        modelBuilder.HasPostgresEnum<BulkActionNotificationType>(name: "BulkActionNotificationType");
        modelBuilder.HasPostgresEnum<BulkActionItemStatus>(name: "BulkActionItemStatus");
        modelBuilder.HasPostgresEnum<GithubRepositorySelection>(name: "GithubRepositorySelection");
        modelBuilder.HasPostgresEnum<TrustedDeployScope>(name: "TrustedDeployScope");
        modelBuilder.HasPostgresEnum<ImpersonationAuditLogAction>(name: "ImpersonationAuditLogAction");
        modelBuilder.HasPostgresEnum<SupportAccessMode>(name: "SupportAccessMode");
        modelBuilder.HasPostgresEnum<SupportAccessRequestStatus>(name: "SupportAccessRequestStatus");
        modelBuilder.HasPostgresEnum<CustomerQuerySource>(name: "CustomerQuerySource");
        modelBuilder.HasPostgresEnum<CustomerQueryScope>(name: "CustomerQueryScope");
        modelBuilder.HasPostgresEnum<PlatformNotificationSurface>(name: "PlatformNotificationSurface");
        modelBuilder.HasPostgresEnum<PlatformNotificationScope>(name: "PlatformNotificationScope");
        modelBuilder.HasPostgresEnum<ErrorGroupStatus>(name: "ErrorGroupStatus");
        modelBuilder.HasPostgresEnum<DataStoreKind>(name: "DataStoreKind");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.AuthIdentifier, "User_authIdentifier_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PendingEmail).HasColumnName("pendingEmail");
            entity.Property(e => e.AuthenticationMethod).HasColumnName("authenticationMethod");
            entity.Property(e => e.AuthenticationProfile)
                .HasColumnType("jsonb")
                .HasColumnName("authenticationProfile");
            entity.Property(e => e.AuthenticationExtraParams)
                .HasColumnType("jsonb")
                .HasColumnName("authenticationExtraParams");
            entity.Property(e => e.AuthIdentifier).HasColumnName("authIdentifier");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatarUrl");
            entity.Property(e => e.Admin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("admin");
            entity.Property(e => e.DashboardPreferences)
                .HasColumnType("jsonb")
                .HasColumnName("dashboardPreferences");
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
            entity.Property(e => e.IsOnCloudWaitlist)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isOnCloudWaitlist");
            entity.Property(e => e.FeatureCloud)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("featureCloud");
            entity.Property(e => e.IsOnHostedRepoWaitlist)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isOnHostedRepoWaitlist");
            entity.Property(e => e.MarketingEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("marketingEmails");
            entity.Property(e => e.ConfirmedBasicDetails)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("confirmedBasicDetails");
            entity.Property(e => e.ReferralSource).HasColumnName("referralSource");
            entity.Property(e => e.OnboardingData)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingData");
            entity.Property(e => e.MfaEnabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("mfaEnabledAt");
            entity.Property(e => e.MfaSecretReferenceId).HasColumnName("mfaSecretReferenceId");
            entity.Property(e => e.MfaLastUsedCode).HasColumnName("mfaLastUsedCode");
            entity.Property(e => e.SessionDuration)
                .ValueGeneratedNever()
                .HasDefaultValue(31556952)
                .HasColumnName("sessionDuration");
            entity.Property(e => e.NextSessionEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextSessionEnd");
            entity.Property(e => e.InvitationCodeId).HasColumnName("invitationCodeId");

            entity.HasOne(d => d.MfaSecretReference).WithMany(p => p.User)
                .HasForeignKey(d => d.MfaSecretReferenceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_mfaSecretReferenceId_fkey");

            entity.HasOne(d => d.InvitationCode).WithMany(p => p.Users)
                .HasForeignKey(d => d.InvitationCodeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_invitationCodeId_fkey");
        });

        modelBuilder.Entity<MfaBackupCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MfaBackupCode_pkey");

            entity.ToTable("MfaBackupCode");

            entity.HasIndex(e => new { e.UserId, e.Code }, "MfaBackupCode_userId_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("usedAt");
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

            entity.HasOne(d => d.User).WithMany(p => p.BackupCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("MfaBackupCode_userId_fkey");
        });

        modelBuilder.Entity<InvitationCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InvitationCode_pkey");

            entity.ToTable("InvitationCode");

            entity.HasIndex(e => e.Code, "InvitationCode_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<AuthorizationCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuthorizationCode_pkey");

            entity.ToTable("AuthorizationCode");

            entity.HasIndex(e => e.Code, "AuthorizationCode_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.PersonalAccessTokenId).HasColumnName("personalAccessTokenId");
            entity.Property(e => e.RequesterIp).HasColumnName("requesterIp");
            entity.Property(e => e.CodeChallenge).HasColumnName("codeChallenge");
            entity.Property(e => e.CallbackPort).HasColumnName("callbackPort");
            entity.Property(e => e.ApprovalCodeHash).HasColumnName("approvalCodeHash");
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

            entity.HasOne(d => d.PersonalAccessToken).WithMany(p => p.AuthorizationCodes)
                .HasForeignKey(d => d.PersonalAccessTokenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AuthorizationCode_personalAccessTokenId_fkey");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PersonalAccessToken_pkey");

            entity.ToTable("PersonalAccessToken");

            entity.HasIndex(e => e.HashedToken, "PersonalAccessToken_hashedToken_key").IsUnique();

            entity.HasIndex(e => e.UserId, "PersonalAccessToken_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.EncryptedToken)
                .HasColumnType("jsonb")
                .HasColumnName("encryptedToken");
            entity.Property(e => e.ObfuscatedToken).HasColumnName("obfuscatedToken");
            entity.Property(e => e.HashedToken).HasColumnName("hashedToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.LastAccessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastAccessedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IdleTimeoutSeconds).HasColumnName("idleTimeoutSeconds");
            entity.Property(e => e.ExpiryReminderSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiryReminderSentAt");
            entity.Property(e => e.ExpiredNoticeSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiredNoticeSentAt");
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

            entity.HasOne(d => d.User).WithMany(p => p.PersonalAccessTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("PersonalAccessToken_userId_fkey");
        });

        modelBuilder.Entity<OrganizationAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationAccessToken_pkey");

            entity.ToTable("OrganizationAccessToken");

            entity.HasIndex(e => e.HashedToken, "OrganizationAccessToken_hashedToken_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.Type, e.CreatedAt }, "OrganizationAccessToken_organizationId_type_createdAt_idx").IsDescending(false, false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganizationAccessTokenType.User)
                .HasColumnName("type");
            entity.Property(e => e.HashedToken).HasColumnName("hashedToken");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.LastAccessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastAccessedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationAccessTokens)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrganizationAccessToken_organizationId_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pkey");

            entity.ToTable("Organization");

            entity.HasIndex(e => e.Slug, "Organization_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.MaximumExecutionTimePerRunInMs)
                .ValueGeneratedNever()
                .HasDefaultValue(900000)
                .HasColumnName("maximumExecutionTimePerRunInMs");
            entity.Property(e => e.MaximumConcurrencyLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(10)
                .HasColumnName("maximumConcurrencyLimit");
            entity.Property(e => e.MaximumSchedulesLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(5)
                .HasColumnName("maximumSchedulesLimit");
            entity.Property(e => e.MaximumDevQueueSize).HasColumnName("maximumDevQueueSize");
            entity.Property(e => e.MaximumDeployedQueueSize).HasColumnName("maximumDeployedQueueSize");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.CompanySize).HasColumnName("companySize");
            entity.Property(e => e.OnboardingData)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingData");
            entity.Property(e => e.Avatar)
                .HasColumnType("jsonb")
                .HasColumnName("avatar");
            entity.Property(e => e.RunsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("runsEnabled");
            entity.Property(e => e.IsActivated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("v3Enabled");
            entity.Property(e => e.V2Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("v2Enabled");
            entity.Property(e => e.V2MarqsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("v2MarqsEnabled");
            entity.Property(e => e.HasRequestedV3)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hasRequestedV3");
            entity.Property(e => e.ApiRateLimiterConfig)
                .HasColumnType("jsonb")
                .HasColumnName("apiRateLimiterConfig");
            entity.Property(e => e.RealtimeRateLimiterConfig)
                .HasColumnType("jsonb")
                .HasColumnName("realtimeRateLimiterConfig");
            entity.Property(e => e.BatchRateLimitConfig)
                .HasColumnType("jsonb")
                .HasColumnName("batchRateLimitConfig");
            entity.Property(e => e.BatchQueueConcurrencyConfig)
                .HasColumnType("jsonb")
                .HasColumnName("batchQueueConcurrencyConfig");
            entity.Property(e => e.WebhookLimitsConfig)
                .HasColumnType("jsonb")
                .HasColumnName("webhookLimitsConfig");
            entity.Property(e => e.FeatureFlags)
                .HasColumnType("jsonb")
                .HasColumnName("featureFlags");
            entity.Property(e => e.MaximumProjectCount)
                .ValueGeneratedNever()
                .HasDefaultValue(25)
                .HasColumnName("maximumProjectCount");
            entity.Property(e => e.MaxSessionDuration).HasColumnName("maxSessionDuration");
            entity.Property(e => e.StreamBasinName).HasColumnName("streamBasinName");
            entity.Property(e => e.SupportAccessMode)
                .ValueGeneratedNever()
                .HasDefaultValue(SupportAccessMode.Allow)
                .HasColumnName("supportAccessMode");
            entity.Property(e => e.SupportAccessModeUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("supportAccessModeUpdatedAt");
            entity.Property(e => e.SupportAccessModeUpdatedById).HasColumnName("supportAccessModeUpdatedById");
        });

        modelBuilder.Entity<OrgMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrgMember_pkey");

            entity.ToTable("OrgMember");

            entity.HasIndex(e => e.UserId, "OrgMember_userId_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "OrgMember_organizationId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(OrgMemberRole.Member)
                .HasColumnName("role");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrgMember_organizationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OrgMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrgMember_userId_fkey");
        });

        modelBuilder.Entity<OrgMemberInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrgMemberInvite_pkey");

            entity.ToTable("OrgMemberInvite");

            entity.HasIndex(e => e.Token, "OrgMemberInvite_token_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.Email }, "OrgMemberInvite_organizationId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("token");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(OrgMemberRole.Member)
                .HasColumnName("role");
            entity.Property(e => e.RbacRoleId).HasColumnName("rbacRoleId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.InviterId).HasColumnName("inviterId");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Invites)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrgMemberInvite_organizationId_fkey");

            entity.HasOne(d => d.Inviter).WithMany(p => p.SentInvites)
                .HasForeignKey(d => d.InviterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrgMemberInvite_inviterId_fkey");
        });

        modelBuilder.Entity<RuntimeEnvironment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RuntimeEnvironment_pkey");

            entity.ToTable("RuntimeEnvironment");

            entity.HasIndex(e => e.ApiKey, "RuntimeEnvironment_apiKey_key").IsUnique();

            entity.HasIndex(e => e.PkApiKey, "RuntimeEnvironment_pkApiKey_key").IsUnique();

            entity.HasIndex(e => e.ParentEnvironmentId, "RuntimeEnvironment_parentEnvironmentId_idx");

            entity.HasIndex(e => e.ProjectId, "RuntimeEnvironment_projectId_idx");

            entity.HasIndex(e => e.OrganizationId, "RuntimeEnvironment_organizationId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug, e.OrgMemberId }, "RuntimeEnvironment_projectId_slug_orgMemberId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.Shortcode }, "RuntimeEnvironment_projectId_shortcode_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.RootApiKeyHiddenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rootApiKeyHiddenAt");
            entity.Property(e => e.PkApiKey).HasColumnName("pkApiKey");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(RuntimeEnvironmentType.Development)
                .HasColumnName("type");
            entity.Property(e => e.IsBranchableEnvironment)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isBranchableEnvironment");
            entity.Property(e => e.BranchName).HasColumnName("branchName");
            entity.Property(e => e.ParentEnvironmentId).HasColumnName("parentEnvironmentId");
            entity.Property(e => e.PreviewAutoArchiveAfterDays).HasColumnName("previewAutoArchiveAfterDays");
            entity.Property(e => e.PreviewAutoArchiveExcludedBranches)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("previewAutoArchiveExcludedBranches");
            entity.Property(e => e.PreviewAutoArchiveNextCheckAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("previewAutoArchiveNextCheckAt");
            entity.Property(e => e.PreviewAutoArchiveCursorCreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("previewAutoArchiveCursorCreatedAt");
            entity.Property(e => e.PreviewAutoArchiveCursorId).HasColumnName("previewAutoArchiveCursorId");
            entity.Property(e => e.Git)
                .HasColumnType("jsonb")
                .HasColumnName("git");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.Shortcode).HasColumnName("shortcode");
            entity.Property(e => e.MaximumConcurrencyLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(5)
                .HasColumnName("maximumConcurrencyLimit");
            entity.Property(e => e.ConcurrencyLimitBurstFactor)
                .ValueGeneratedNever()
                .HasPrecision(4, 2)
                .HasDefaultValue(2m)
                .HasColumnName("concurrencyLimitBurstFactor");
            entity.Property(e => e.Paused)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("paused");
            entity.Property(e => e.PauseSource).HasColumnName("pauseSource");
            entity.Property(e => e.AutoEnableInternalSources)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("autoEnableInternalSources");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrgMemberId).HasColumnName("orgMemberId");
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
            entity.Property(e => e.BuiltInEnvironmentVariableOverrides)
                .HasColumnType("jsonb")
                .HasColumnName("builtInEnvironmentVariableOverrides");
            entity.Property(e => e.TunnelId).HasColumnName("tunnelId");
            entity.Property(e => e.CurrentSessionId).HasColumnName("currentSessionId");

            entity.HasOne(d => d.ParentEnvironment).WithMany(p => p.ChildEnvironments)
                .HasForeignKey(d => d.ParentEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuntimeEnvironment_parentEnvironmentId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Environments)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuntimeEnvironment_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Environments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuntimeEnvironment_projectId_fkey");

            entity.HasOne(d => d.OrgMember).WithMany(p => p.Environments)
                .HasForeignKey(d => d.OrgMemberId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RuntimeEnvironment_orgMemberId_fkey");

            entity.HasOne(d => d.CurrentSession).WithMany(p => p.CurrentEnvironments)
                .HasForeignKey(d => d.CurrentSessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RuntimeEnvironment_currentSessionId_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("api_keys_pkey");

            entity.ToTable("api_keys");

            entity.HasIndex(e => e.KeyHash, "api_keys_key_hash_key").IsUnique();

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.RevokedAt, e.CreatedAt }, "api_keys_runtime_environment_id_revoked_at_created_at_idx").IsDescending(false, false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.KeyHash).HasColumnName("key_hash");
            entity.Property(e => e.LastFour).HasColumnName("last_four");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtime_environment_id");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.PresetId).HasColumnName("preset_id");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.AllowedIpRanges)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_ip_ranges");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.LastUsedIp).HasColumnName("last_used_ip");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_keys_runtime_environment_id_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedApiKeys)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("api_keys_created_by_user_id_fkey");
        });

        modelBuilder.Entity<RevokedApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RevokedApiKey_pkey");

            entity.ToTable("RevokedApiKey");

            entity.HasIndex(e => e.ApiKey, "RevokedApiKey_apiKey_idx");

            entity.HasIndex(e => e.RuntimeEnvironmentId, "RevokedApiKey_runtimeEnvironmentId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.RevokedApiKeys)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RevokedApiKey_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pkey");

            entity.ToTable("Project");

            entity.HasIndex(e => e.Slug, "Project_slug_key").IsUnique();

            entity.HasIndex(e => e.ExternalRef, "Project_externalRef_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ExternalRef).HasColumnName("externalRef");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.InitializedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("initializedAt");
            entity.Property(e => e.DefaultRuntime).HasColumnName("defaultRuntime");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(ProjectVersion.V2)
                .HasColumnName("version");
            entity.Property(e => e.Engine)
                .ValueGeneratedNever()
                .HasDefaultValue(RunEngineVersion.V1)
                .HasColumnName("engine");
            entity.Property(e => e.BuilderProjectId).HasColumnName("builderProjectId");
            entity.Property(e => e.DefaultWorkerGroupId).HasColumnName("defaultWorkerGroupId");
            entity.Property(e => e.AllowedWorkerQueues)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowedMasterQueues");
            entity.Property(e => e.BuildSettings)
                .HasColumnType("jsonb")
                .HasColumnName("buildSettings");
            entity.Property(e => e.OnboardingData)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingData");

            entity.HasOne(d => d.Organization).WithMany(p => p.Projects)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Project_organizationId_fkey");

            entity.HasOne(d => d.DefaultWorkerGroup).WithMany(p => p.DefaultForProjects)
                .HasForeignKey(d => d.DefaultWorkerGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Project_defaultWorkerGroupId_fkey");
        });

        modelBuilder.Entity<EnvironmentEncryptionPolicy>(entity =>
        {
            entity.HasKey(e => e.EnvironmentId).HasName("EnvironmentEncryptionPolicy_pkey");

            entity.ToTable("EnvironmentEncryptionPolicy");

            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("revision");
            entity.Property(e => e.WrappingProvider).HasColumnName("wrappingProvider");
            entity.Property(e => e.WrappingKeyReference).HasColumnName("wrappingKeyReference");
            entity.Property(e => e.ActiveKeyId).HasColumnName("activeKeyId");

            entity.HasOne(d => d.Environment).WithOne(p => p.EncryptionPolicy)
                .HasForeignKey<EnvironmentEncryptionPolicy>(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvironmentEncryptionPolicy_environmentId_fkey");

            entity.HasOne(d => d.ActiveKey).WithMany(p => p.ActiveFor)
                .HasPrincipalKey(p => new { p.Id, p.EnvironmentId })
                .HasForeignKey(d => new { d.ActiveKeyId, d.EnvironmentId })
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("EnvironmentEncryptionPolicy_activeKeyId_environmentId_fkey");
        });

        modelBuilder.Entity<EnvironmentEncryptionKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvironmentEncryptionKey_pkey");

            entity.ToTable("EnvironmentEncryptionKey");

            entity.HasIndex(e => e.EnvironmentId, "EnvironmentEncryptionKey_environmentId_idx");

            entity.HasAlternateKey(e => new { e.Id, e.EnvironmentId }).HasName("EnvironmentEncryptionKey_id_environmentId_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.WrappedKey)
                .HasColumnType("jsonb")
                .HasColumnName("wrappedKey");
            entity.Property(e => e.WrappingProvider).HasColumnName("wrappingProvider");
            entity.Property(e => e.WrappingKeyReference).HasColumnName("wrappingKeyReference");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("revision");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Environment).WithMany(p => p.EncryptionKeys)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvironmentEncryptionKey_environmentId_fkey");
        });

        modelBuilder.Entity<SecretReference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SecretReference_pkey");

            entity.ToTable("SecretReference");

            entity.HasIndex(e => e.Key, "SecretReference_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Provider)
                .ValueGeneratedNever()
                .HasDefaultValue(SecretStoreProvider.Database)
                .HasColumnName("provider");
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

        modelBuilder.Entity<SecretStore>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("SecretStore_key_key");

            entity.ToTable("SecretStore");

            entity.HasIndex(e => e.Key, "SecretStore_key_idx").HasOperators("text_pattern_ops");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue("1")
                .HasColumnName("version");
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

        modelBuilder.Entity<OidcSigningKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OidcSigningKey_pkey");

            entity.ToTable("OidcSigningKey");

            entity.HasIndex(e => e.Kid, "OidcSigningKey_kid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Kid).HasColumnName("kid");
            entity.Property(e => e.Purpose)
                .ValueGeneratedNever()
                .HasDefaultValue(OidcSigningKeyPurpose.ExternalOidc)
                .HasColumnName("purpose");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PublicJwk)
                .HasColumnType("jsonb")
                .HasColumnName("publicJwk");
            entity.Property(e => e.ReadyAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("readyAt");
            entity.Property(e => e.PublicUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publicUntil");
            entity.Property(e => e.GeneratedByUserId).HasColumnName("generatedByUserId");
            entity.Property(e => e.ActivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("activatedAt");
            entity.Property(e => e.ActivatedByUserId).HasColumnName("activatedByUserId");
            entity.Property(e => e.RetiredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("retiredAt");
            entity.Property(e => e.RetiredByUserId).HasColumnName("retiredByUserId");
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

        modelBuilder.Entity<DataMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataMigration_pkey");

            entity.ToTable("DataMigration");

            entity.HasIndex(e => e.Name, "DataMigration_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
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
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
        });

        modelBuilder.Entity<BackgroundWorker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BackgroundWorker_pkey");

            entity.ToTable("BackgroundWorker");

            entity.HasIndex(e => e.FriendlyId, "BackgroundWorker_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.RuntimeEnvironmentId, "BackgroundWorker_runtimeEnvironmentId_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.CreatedAt }, "BackgroundWorker_runtimeEnvironmentId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.ProjectId, e.RuntimeEnvironmentId, e.Version }, "BackgroundWorker_projectId_runtimeEnvironmentId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Engine)
                .ValueGeneratedNever()
                .HasDefaultValue(RunEngineVersion.V1)
                .HasColumnName("engine");
            entity.Property(e => e.ContentHash).HasColumnName("contentHash");
            entity.Property(e => e.SdkVersion)
                .ValueGeneratedNever()
                .HasDefaultValue("unknown")
                .HasColumnName("sdkVersion");
            entity.Property(e => e.CliVersion)
                .ValueGeneratedNever()
                .HasDefaultValue("unknown")
                .HasColumnName("cliVersion");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.RuntimeVersion).HasColumnName("runtimeVersion");
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
            entity.Property(e => e.WorkerGroupId).HasColumnName("workerGroupId");
            entity.Property(e => e.SupportsLazyAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("supportsLazyAttempts");

            entity.HasOne(d => d.Project).WithMany(p => p.BackgroundWorkers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorker_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.BackgroundWorkers)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorker_runtimeEnvironmentId_fkey");

            entity.HasOne(d => d.WorkerGroup).WithMany(p => p.BackgroundWorkers)
                .HasForeignKey(d => d.WorkerGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BackgroundWorker_workerGroupId_fkey");

            entity.HasMany(d => d.Files).WithMany(p => p.BackgroundWorkers)
                .UsingEntity<Dictionary<string, object>>(
                    "_BackgroundWorkerToBackgroundWorkerFile",
                    r => r.HasOne<BackgroundWorkerFile>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_BackgroundWorkerToBackgroundWorkerFile_B_fkey"),
                    l => l.HasOne<BackgroundWorker>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_BackgroundWorkerToBackgroundWorkerFile_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_BackgroundWorkerToBackgroundWorkerFile_AB_pkey");
                        j.ToTable("_BackgroundWorkerToBackgroundWorkerFile");
                        j.HasIndex(new[] { "B" }, "_BackgroundWorkerToBackgroundWorkerFile_B_index");
                    });

            entity.HasMany(d => d.Queues).WithMany(p => p.Workers)
                .UsingEntity<Dictionary<string, object>>(
                    "_BackgroundWorkerToTaskQueue",
                    r => r.HasOne<TaskQueue>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_BackgroundWorkerToTaskQueue_B_fkey"),
                    l => l.HasOne<BackgroundWorker>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_BackgroundWorkerToTaskQueue_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_BackgroundWorkerToTaskQueue_AB_pkey");
                        j.ToTable("_BackgroundWorkerToTaskQueue");
                        j.HasIndex(new[] { "B" }, "_BackgroundWorkerToTaskQueue_B_index");
                    });
        });

        modelBuilder.Entity<BackgroundWorkerFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BackgroundWorkerFile_pkey");

            entity.ToTable("BackgroundWorkerFile");

            entity.HasIndex(e => e.FriendlyId, "BackgroundWorkerFile_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.ContentHash }, "BackgroundWorkerFile_projectId_contentHash_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.FilePath).HasColumnName("filePath");
            entity.Property(e => e.ContentHash).HasColumnName("contentHash");
            entity.Property(e => e.Contents).HasColumnName("contents");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.BackgroundWorkerFile)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorkerFile_projectId_fkey");
        });

        modelBuilder.Entity<Prompt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompts_pkey");

            entity.ToTable("prompts");

            entity.HasIndex(e => e.FriendlyId, "prompts_friendly_id_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "prompts_projectId_idx");

            entity.HasIndex(e => e.RuntimeEnvironmentId, "prompts_runtimeEnvironmentId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.RuntimeEnvironmentId, e.Slug }, "prompts_projectId_runtimeEnvironmentId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendly_id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("text")
                .HasColumnName("type");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.FilePath).HasColumnName("filePath");
            entity.Property(e => e.ExportName).HasColumnName("exportName");
            entity.Property(e => e.VariableSchema)
                .HasColumnType("jsonb")
                .HasColumnName("variableSchema");
            entity.Property(e => e.DefaultModel).HasColumnName("defaultModel");
            entity.Property(e => e.DefaultConfig)
                .HasColumnType("jsonb")
                .HasColumnName("defaultConfig");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.Prompts)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompts_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Prompts)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompts_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.Prompts)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompts_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<PromptVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompt_versions_pkey");

            entity.ToTable("prompt_versions");

            entity.HasIndex(e => e.PromptId, "prompt_versions_promptId_idx");

            entity.HasIndex(e => e.ContentHash, "prompt_versions_contentHash_idx");

            entity.HasIndex(e => new { e.PromptId, e.Version }, "prompt_versions_promptId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.PromptId).HasColumnName("promptId");
            entity.Property(e => e.TextContent).HasColumnName("textContent");
            entity.Property(e => e.ChatContent)
                .HasColumnType("jsonb")
                .HasColumnName("chatContent");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.CommitMessage).HasColumnName("commitMessage");
            entity.Property(e => e.ContentHash).HasColumnName("contentHash");
            entity.Property(e => e.Labels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("labels");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Prompt).WithMany(p => p.Versions)
                .HasForeignKey(d => d.PromptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompt_versions_promptId_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.PromptVersions)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("prompt_versions_workerId_fkey");
        });

        modelBuilder.Entity<BackgroundWorkerTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BackgroundWorkerTask_pkey");

            entity.ToTable("BackgroundWorkerTask");

            entity.HasIndex(e => e.FriendlyId, "BackgroundWorkerTask_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.Slug }, "BackgroundWorkerTask_projectId_slug_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Slug, e.CreatedAt }, "BackgroundWorkerTask_projectId_slug_createdAt_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.ProjectId }, "BackgroundWorkerTask_runtimeEnvironmentId_projectId_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.Slug, e.TriggerSource }, "BackgroundWorkerTask_runtimeEnvironmentId_slug_triggerSourc_idx");

            entity.HasIndex(e => new { e.WorkerId, e.Slug }, "BackgroundWorkerTask_workerId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.FilePath).HasColumnName("filePath");
            entity.Property(e => e.ExportName).HasColumnName("exportName");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.FileId).HasColumnName("fileId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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
            entity.Property(e => e.QueueConfig)
                .HasColumnType("jsonb")
                .HasColumnName("queueConfig");
            entity.Property(e => e.RetryConfig)
                .HasColumnType("jsonb")
                .HasColumnName("retryConfig");
            entity.Property(e => e.MachineConfig)
                .HasColumnType("jsonb")
                .HasColumnName("machineConfig");
            entity.Property(e => e.Gates)
                .HasColumnType("jsonb")
                .HasColumnName("gates");
            entity.Property(e => e.QueueId).HasColumnName("queueId");
            entity.Property(e => e.MaxDurationInSeconds).HasColumnName("maxDurationInSeconds");
            entity.Property(e => e.Ttl).HasColumnName("ttl");
            entity.Property(e => e.TriggerSource)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskTriggerSource.Standard)
                .HasColumnName("triggerSource");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.PayloadSchema)
                .HasColumnType("jsonb")
                .HasColumnName("payloadSchema");

            entity.HasOne(d => d.Worker).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorkerTask_workerId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.BackgroundWorkerTasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorkerTask_projectId_fkey");

            entity.HasOne(d => d.File).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorkerTask_fileId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.BackgroundWorkerTasks)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundWorkerTask_runtimeEnvironmentId_fkey");

            entity.HasOne(d => d.Queue).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BackgroundWorkerTask_queueId_fkey");
        });

        modelBuilder.Entity<WebhookEndpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebhookEndpoint_pkey");

            entity.ToTable("WebhookEndpoint");

            entity.HasIndex(e => e.FriendlyId, "WebhookEndpoint_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.OpaqueId, "WebhookEndpoint_opaqueId_key").IsUnique();

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.Source }, "WebhookEndpoint_runtimeEnvironmentId_source_idx");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "WebhookEndpoint_projectId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.DeclaredId, e.EndpointTenantId, e.EndpointExternalRef }, "WebhookEndpoint_runtimeEnvironmentId_declaredId_endpointTen_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.OpaqueId).HasColumnName("opaqueId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.EndpointTenantId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("endpointTenantId");
            entity.Property(e => e.EndpointExternalRef)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("endpointExternalRef");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.DeclaredId).HasColumnName("declaredId");
            entity.Property(e => e.RoutingTargets)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("routingTargets");
            entity.Property(e => e.VerifierArtifact)
                .HasColumnType("jsonb")
                .HasColumnName("verifierArtifact");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.SecretProvisioning)
                .ValueGeneratedNever()
                .HasDefaultValue("either")
                .HasColumnName("secretProvisioning");
            entity.Property(e => e.SetupPrompt).HasColumnName("setupPrompt");
            entity.Property(e => e.SigningSecretKey).HasColumnName("signingSecretKey");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookEndpointStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.ManuallyDeactivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("manuallyDeactivatedAt");
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

        modelBuilder.Entity<WebhookDelivery>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedAt, e.RetentionDays }).HasName("WebhookDelivery_pkey");

            entity.ToTable("WebhookDelivery");

            entity.HasIndex(e => new { e.WebhookEndpointId, e.CreatedAt }, "WebhookDelivery_webhookEndpointId_createdAt_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.WebhookEndpointId).HasColumnName("webhookEndpointId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.ExternalDeliveryId).HasColumnName("externalDeliveryId");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookDeliveryStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.IsTest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTest");
            entity.Property(e => e.ParsedEvent)
                .HasColumnType("jsonb")
                .HasColumnName("parsedEvent");
            entity.Property(e => e.Headers)
                .HasColumnType("jsonb")
                .HasColumnName("headers");
            entity.Property(e => e.RawBodyHash).HasColumnName("rawBodyHash");
            entity.Property(e => e.ErrorMessage).HasColumnName("errorMessage");
            entity.Property(e => e.FilterReason).HasColumnName("filterReason");
            entity.Property(e => e.Targets)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("targets");
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
            entity.Property(e => e.ProcessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processedAt");
            entity.Property(e => e.RetentionDays)
                .ValueGeneratedNever()
                .HasDefaultValue(30)
                .HasColumnName("retentionDays");
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Alert_pkey");

            entity.ToTable("Alert");

            entity.HasIndex(e => new { e.EnvironmentId, e.Lifecycle }, "Alert_environmentId_lifecycle_idx");

            entity.HasIndex(e => new { e.Lifecycle, e.EvaluationLeaseUntil }, "Alert_lifecycle_evaluationLeaseUntil_idx");

            entity.HasIndex(e => e.ProjectId, "Alert_projectId_idx");

            entity.HasIndex(e => e.OrganizationId, "Alert_organizationId_idx");

            entity.HasIndex(e => e.GroupId, "Alert_groupId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.CapabilityKey).HasColumnName("capabilityKey");
            entity.Property(e => e.Params)
                .HasColumnType("jsonb")
                .HasColumnName("params");
            entity.Property(e => e.Condition)
                .HasColumnType("jsonb")
                .HasColumnName("condition");
            entity.Property(e => e.Query).HasColumnName("query");
            entity.Property(e => e.ExpiresAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresAt");
            entity.Property(e => e.Lifecycle)
                .ValueGeneratedNever()
                .HasDefaultValue("active")
                .HasColumnName("lifecycle");
            entity.Property(e => e.Attention)
                .ValueGeneratedNever()
                .HasDefaultValue("clear")
                .HasColumnName("attention");
            entity.Property(e => e.ObservedCondition)
                .ValueGeneratedNever()
                .HasDefaultValue("unknown")
                .HasColumnName("observedCondition");
            entity.Property(e => e.ObservedConditionChangedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("observedConditionChangedAt");
            entity.Property(e => e.PendingMetSince)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("pendingMetSince");
            entity.Property(e => e.EvaluatorState)
                .HasColumnType("jsonb")
                .HasColumnName("evaluatorState");
            entity.Property(e => e.EvaluationLeaseId).HasColumnName("evaluationLeaseId");
            entity.Property(e => e.EvaluationLeaseUntil)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("evaluationLeaseUntil");
            entity.Property(e => e.LastEvaluatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastEvaluatedAt");
            entity.Property(e => e.NextEvaluationAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("nextEvaluationAt");
            entity.Property(e => e.Notifiers)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("notifiers");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
            entity.Property(e => e.ResolvedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("resolvedAt");
            entity.Property(e => e.ResolvedBy).HasColumnName("resolvedBy");
            entity.Property(e => e.StoppedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("stoppedAt");
            entity.Property(e => e.StoppedBy).HasColumnName("stoppedBy");
            entity.Property(e => e.LastOccurrenceAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastOccurrenceAt");
            entity.Property(e => e.OccurrencesSinceRaise)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("occurrencesSinceRaise");
            entity.Property(e => e.OccurrencesTotal)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("occurrencesTotal");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
        });

        modelBuilder.Entity<AlertEvent>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.At }).HasName("AlertEvent_pkey");

            entity.ToTable("AlertEvent");

            entity.HasIndex(e => new { e.AlertId, e.At }, "AlertEvent_alertId_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlertId).HasColumnName("alertId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.At)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("at");
            entity.Property(e => e.Facts)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("facts");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.SourceEventId).HasColumnName("sourceEventId");
        });

        modelBuilder.Entity<AlertOccurrenceSeen>(entity =>
        {
            entity.HasKey(e => new { e.AlertId, e.SourceEventId }).HasName("AlertOccurrenceSeen_pkey");

            entity.ToTable("AlertOccurrenceSeen");

            entity.HasIndex(e => e.ExpiresAt, "AlertOccurrenceSeen_expiresAt_idx");

            entity.Property(e => e.AlertId).HasColumnName("alertId");
            entity.Property(e => e.SourceEventId).HasColumnName("sourceEventId");
            entity.Property(e => e.RowTime)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("rowTime");
            entity.Property(e => e.SeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("seenAt");
            entity.Property(e => e.ExpiresAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresAt");
        });

        modelBuilder.Entity<AlertDelivery>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.EventAt }).HasName("AlertDelivery_pkey");

            entity.ToTable("AlertDelivery");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "AlertDelivery_status_createdAt_idx");

            entity.HasIndex(e => new { e.Status, e.ClaimedAt }, "AlertDelivery_status_claimedAt_idx");

            entity.HasIndex(e => new { e.AlertEventId, e.NotifierId, e.EventAt }, "AlertDelivery_alertEventId_notifierId_eventAt_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlertEventId).HasColumnName("alertEventId");
            entity.Property(e => e.EventAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("eventAt");
            entity.Property(e => e.NotifierId).HasColumnName("notifierId");
            entity.Property(e => e.Notifier)
                .HasColumnType("jsonb")
                .HasColumnName("notifier");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.ClaimId).HasColumnName("claimId");
            entity.Property(e => e.ClaimedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("claimedAt");
            entity.Property(e => e.DeliveredAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deliveredAt");
            entity.Property(e => e.FailureReason).HasColumnName("failureReason");
            entity.Property(e => e.ProviderMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("providerMetadata");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.FirstAttemptAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("firstAttemptAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<PlaygroundConversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlaygroundConversation_pkey");

            entity.ToTable("PlaygroundConversation");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.AgentSlug, e.UpdatedAt }, "PlaygroundConversation_runtimeEnvironmentId_agentSlug_updat_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.UserId, e.RuntimeEnvironmentId }, "PlaygroundConversation_userId_runtimeEnvironmentId_idx");

            entity.HasIndex(e => new { e.ChatId, e.RuntimeEnvironmentId }, "PlaygroundConversation_chatId_runtimeEnvironmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ChatId).HasColumnName("chatId");
            entity.Property(e => e.Title)
                .ValueGeneratedNever()
                .HasDefaultValue("New conversation")
                .HasColumnName("title");
            entity.Property(e => e.AgentSlug).HasColumnName("agentSlug");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.ClientData)
                .HasColumnType("jsonb")
                .HasColumnName("clientData");
            entity.Property(e => e.Messages)
                .HasColumnType("jsonb")
                .HasColumnName("messages");
            entity.Property(e => e.LastEventId).HasColumnName("lastEventId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.Run).WithMany(p => p.PlaygroundConversations)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PlaygroundConversation_runId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.PlaygroundConversations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlaygroundConversation_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.PlaygroundConversations)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlaygroundConversation_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Session_pkey");

            entity.ToTable("Session");

            entity.HasIndex(e => e.FriendlyId, "Session_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.ExpiresAt, "Session_expiresAt_idx");

            entity.HasIndex(e => e.CurrentRunId, "Session_currentRunId_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.ExternalId }, "Session_runtimeEnvironmentId_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.TriggerConfig)
                .HasColumnType("jsonb")
                .HasColumnName("triggerConfig");
            entity.Property(e => e.IsTest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTest");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CurrentRunId).HasColumnName("currentRunId");
            entity.Property(e => e.CurrentRunVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("currentRunVersion");
            entity.Property(e => e.ClosedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("closedAt");
            entity.Property(e => e.ClosedReason).HasColumnName("closedReason");
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
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.StreamBasinName).HasColumnName("streamBasinName");
            entity.Property(e => e.ChatSnapshotStoragePath).HasColumnName("chatSnapshotStoragePath");
        });

        modelBuilder.Entity<SessionRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SessionRun_pkey");

            entity.ToTable("SessionRun");

            entity.HasIndex(e => e.RunId, "SessionRun_runId_key").IsUnique();

            entity.HasIndex(e => e.SessionId, "SessionRun_sessionId_idx");

            entity.HasIndex(e => new { e.SessionId, e.TriggeredAt, e.Id }, "SessionRun_sessionId_triggeredAt_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.TriggeredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("triggeredAt");

            entity.HasOne(d => d.Session).WithMany(p => p.Runs)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SessionRun_sessionId_fkey");
        });

        modelBuilder.Entity<TaskRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRun_pkey");

            entity.ToTable("TaskRun");

            entity.HasIndex(e => e.FriendlyId, "TaskRun_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.ParentTaskRunId, "TaskRun_parentTaskRunId_idx");

            entity.HasIndex(e => e.SpanId, "TaskRun_spanId_idx");

            entity.HasIndex(e => e.ParentSpanId, "TaskRun_parentSpanId_idx");

            entity.HasIndex(e => e.RunTags, "TaskRun_runTags_idx")
                .HasMethod("gin")
                .HasOperators("array_ops");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.BatchId }, "TaskRun_runtimeEnvironmentId_batchId_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.CreatedAt }, "TaskRun_runtimeEnvironmentId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => e.CreatedAt, "TaskRun_createdAt_idx").HasMethod("brin");

            entity.HasIndex(e => e.OneTimeUseToken, "TaskRun_oneTimeUseToken_key").IsUnique();

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.TaskIdentifier, e.IdempotencyKey }, "TaskRun_runtimeEnvironmentId_taskIdentifier_idempotencyKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Number)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("number");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Engine)
                .ValueGeneratedNever()
                .HasDefaultValue(RunEngineVersion.V1)
                .HasColumnName("engine");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskRunStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.StatusReason).HasColumnName("statusReason");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.IdempotencyKeyExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("idempotencyKeyExpiresAt");
            entity.Property(e => e.IdempotencyKeyOptions)
                .HasColumnType("jsonb")
                .HasColumnName("idempotencyKeyOptions");
            entity.Property(e => e.Debounce)
                .HasColumnType("jsonb")
                .HasColumnName("debounce");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.IsTest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTest");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.PayloadType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("payloadType");
            entity.Property(e => e.Context)
                .HasColumnType("jsonb")
                .HasColumnName("context");
            entity.Property(e => e.TraceContext)
                .HasColumnType("jsonb")
                .HasColumnName("traceContext");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.SpanId).HasColumnName("spanId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Queue).HasColumnName("queue");
            entity.Property(e => e.LockedQueueId).HasColumnName("lockedQueueId");
            entity.Property(e => e.WorkerQueue)
                .ValueGeneratedNever()
                .HasDefaultValue("main")
                .HasColumnName("masterQueue");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.SecondaryMasterQueue).HasColumnName("secondaryMasterQueue");
            entity.Property(e => e.AttemptNumber).HasColumnName("attemptNumber");
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
            entity.Property(e => e.RunTags).HasColumnName("runTags");
            entity.Property(e => e.TaskVersion).HasColumnName("taskVersion");
            entity.Property(e => e.SdkVersion).HasColumnName("sdkVersion");
            entity.Property(e => e.CliVersion).HasColumnName("cliVersion");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.ExecutedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.MachinePreset).HasColumnName("machinePreset");
            entity.Property(e => e.UsageDurationMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usageDurationMs");
            entity.Property(e => e.CostInCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("costInCents");
            entity.Property(e => e.BaseCostInCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("baseCostInCents");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lockedAt");
            entity.Property(e => e.LockedById).HasColumnName("lockedById");
            entity.Property(e => e.LockedToVersionId).HasColumnName("lockedToVersionId");
            entity.Property(e => e.PriorityMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("priorityMs");
            entity.Property(e => e.ConcurrencyKey).HasColumnName("concurrencyKey");
            entity.Property(e => e.Gates)
                .HasColumnType("jsonb")
                .HasColumnName("gates");
            entity.Property(e => e.DelayUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("delayUntil");
            entity.Property(e => e.QueuedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queuedAt");
            entity.Property(e => e.Ttl).HasColumnName("ttl");
            entity.Property(e => e.ExpiredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiredAt");
            entity.Property(e => e.MaxAttempts).HasColumnName("maxAttempts");
            entity.Property(e => e.LockedRetryConfig)
                .HasColumnType("jsonb")
                .HasColumnName("lockedRetryConfig");
            entity.Property(e => e.OneTimeUseToken).HasColumnName("oneTimeUseToken");
            entity.Property(e => e.TaskEventStore)
                .ValueGeneratedNever()
                .HasDefaultValue("taskEvent")
                .HasColumnName("taskEventStore");
            entity.Property(e => e.QueueTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queueTimestamp");
            entity.Property(e => e.ScheduleInstanceId).HasColumnName("scheduleInstanceId");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.BulkActionGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("bulkActionGroupIds");
            entity.Property(e => e.LogsDeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("logsDeletedAt");
            entity.Property(e => e.ReplayedFromTaskRunFriendlyId).HasColumnName("replayedFromTaskRunFriendlyId");
            entity.Property(e => e.RootTaskRunId).HasColumnName("rootTaskRunId");
            entity.Property(e => e.ParentTaskRunId).HasColumnName("parentTaskRunId");
            entity.Property(e => e.ParentTaskRunAttemptId).HasColumnName("parentTaskRunAttemptId");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.ResumeParentOnCompletion)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("resumeParentOnCompletion");
            entity.Property(e => e.Depth)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("depth");
            entity.Property(e => e.ParentSpanId).HasColumnName("parentSpanId");
            entity.Property(e => e.RunChainState)
                .HasColumnType("jsonb")
                .HasColumnName("runChainState");
            entity.Property(e => e.SeedMetadata).HasColumnName("seedMetadata");
            entity.Property(e => e.SeedMetadataType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("seedMetadataType");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.MetadataType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("metadataType");
            entity.Property(e => e.MetadataVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("metadataVersion");
            entity.Property(e => e.Annotations)
                .HasColumnType("jsonb")
                .HasColumnName("annotations");
            entity.Property(e => e.IsWarmStart).HasColumnName("isWarmStart");
            entity.Property(e => e.Output).HasColumnName("output");
            entity.Property(e => e.OutputType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("outputType");
            entity.Property(e => e.Error)
                .HasColumnType("jsonb")
                .HasColumnName("error");
            entity.Property(e => e.PlanType).HasColumnName("planType");
            entity.Property(e => e.MaxDurationInSeconds).HasColumnName("maxDurationInSeconds");
            entity.Property(e => e.RealtimeStreamsVersion)
                .ValueGeneratedNever()
                .HasDefaultValue("v1")
                .HasColumnName("realtimeStreamsVersion");
            entity.Property(e => e.RealtimeStreams)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("realtimeStreams");
            entity.Property(e => e.StreamBasinName).HasColumnName("streamBasinName");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.TaskRuns)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRun_runtimeEnvironmentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskRuns)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRun_projectId_fkey");

            entity.HasOne(d => d.LockedBy).WithMany(p => p.Runs)
                .HasForeignKey(d => d.LockedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_lockedById_fkey");

            entity.HasOne(d => d.LockedToVersion).WithMany(p => p.LockedRuns)
                .HasForeignKey(d => d.LockedToVersionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_lockedToVersionId_fkey");

            entity.HasOne(d => d.RootTaskRun).WithMany(p => p.DescendantRuns)
                .HasForeignKey(d => d.RootTaskRunId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_rootTaskRunId_fkey");

            entity.HasOne(d => d.ParentTaskRun).WithMany(p => p.ChildRuns)
                .HasForeignKey(d => d.ParentTaskRunId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_parentTaskRunId_fkey");

            entity.HasOne(d => d.ParentTaskRunAttempt).WithMany(p => p.ChildRuns)
                .HasForeignKey(d => d.ParentTaskRunAttemptId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_parentTaskRunAttemptId_fkey");

            entity.HasOne(d => d.Batch).WithMany(p => p.Runs)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRun_batchId_fkey");

            entity.HasMany(d => d.ConnectedWaitpoints).WithMany(p => p.ConnectedRuns)
                .UsingEntity<Dictionary<string, object>>(
                    "_WaitpointRunConnections",
                    r => r.HasOne<Waitpoint>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_WaitpointRunConnections_B_fkey"),
                    l => l.HasOne<TaskRun>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_WaitpointRunConnections_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_WaitpointRunConnections_AB_pkey");
                        j.ToTable("_WaitpointRunConnections");
                        j.HasIndex(new[] { "B" }, "_WaitpointRunConnections_B_index");
                    });
        });

        modelBuilder.Entity<TaskRunTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunTemplate_pkey");

            entity.ToTable("TaskRunTemplate");

            entity.HasIndex(e => new { e.ProjectId, e.TaskSlug, e.TriggerSource, e.CreatedAt }, "TaskRunTemplate_projectId_taskSlug_triggerSource_createdAt_idx").IsDescending(false, false, false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskSlug).HasColumnName("taskSlug");
            entity.Property(e => e.TriggerSource).HasColumnName("triggerSource");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.PayloadType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("payloadType");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.MetadataType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("metadataType");
            entity.Property(e => e.Queue).HasColumnName("queue");
            entity.Property(e => e.ConcurrencyKey).HasColumnName("concurrencyKey");
            entity.Property(e => e.TtlSeconds).HasColumnName("ttlSeconds");
            entity.Property(e => e.MaxAttempts).HasColumnName("maxAttempts");
            entity.Property(e => e.MaxDurationSeconds).HasColumnName("maxDurationSeconds");
            entity.Property(e => e.Tags).HasColumnName("tags");
            entity.Property(e => e.MachinePreset).HasColumnName("machinePreset");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
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

        modelBuilder.Entity<TaskRunExecutionSnapshot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunExecutionSnapshot_pkey");

            entity.ToTable("TaskRunExecutionSnapshot");

            entity.HasIndex(e => new { e.RunId, e.IsValid, e.CreatedAt }, "TaskRunExecutionSnapshot_runId_isValid_createdAt_idx").IsDescending(false, false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Engine)
                .ValueGeneratedNever()
                .HasDefaultValue(RunEngineVersion.V2)
                .HasColumnName("engine");
            entity.Property(e => e.ExecutionStatus).HasColumnName("executionStatus");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsValid)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isValid");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.PreviousSnapshotId).HasColumnName("previousSnapshotId");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.RunStatus).HasColumnName("runStatus");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.AttemptNumber).HasColumnName("attemptNumber");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CompletedWaitpointOrder).HasColumnName("completedWaitpointOrder");
            entity.Property(e => e.CheckpointId).HasColumnName("checkpointId");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.RunnerId).HasColumnName("runnerId");
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
            entity.Property(e => e.LastHeartbeatAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastHeartbeatAt");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");

            entity.HasOne(d => d.Run).WithMany(p => p.ExecutionSnapshots)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TaskRunExecutionSnapshot_runId_fkey");

            entity.HasOne(d => d.Checkpoint).WithMany(p => p.ExecutionSnapshot)
                .HasForeignKey(d => d.CheckpointId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRunExecutionSnapshot_checkpointId_fkey");

            entity.HasMany(d => d.CompletedWaitpoints).WithMany(p => p.CompletedExecutionSnapshots)
                .UsingEntity<Dictionary<string, object>>(
                    "_completedWaitpoints",
                    r => r.HasOne<Waitpoint>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_completedWaitpoints_B_fkey"),
                    l => l.HasOne<TaskRunExecutionSnapshot>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_completedWaitpoints_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_completedWaitpoints_AB_pkey");
                        j.ToTable("_completedWaitpoints");
                        j.HasIndex(new[] { "B" }, "_completedWaitpoints_B_index");
                    });
        });

        modelBuilder.Entity<TaskRunCheckpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunCheckpoint_pkey");

            entity.ToTable("TaskRunCheckpoint");

            entity.HasIndex(e => e.FriendlyId, "TaskRunCheckpoint_friendlyId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.ImageRef).HasColumnName("imageRef");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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

            entity.HasOne(d => d.Project).WithMany(p => p.TaskRunCheckpoints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunCheckpoint_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.TaskRunCheckpoints)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunCheckpoint_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<Waitpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Waitpoint_pkey");

            entity.ToTable("Waitpoint");

            entity.HasIndex(e => e.FriendlyId, "Waitpoint_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.CompletedByTaskRunId, "Waitpoint_completedByTaskRunId_key").IsUnique();

            entity.HasIndex(e => e.CompletedByBatchId, "Waitpoint_completedByBatchId_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.Type, e.CreatedAt }, "Waitpoint_environmentId_type_createdAt_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.EnvironmentId, e.Type, e.Status }, "Waitpoint_environmentId_type_status_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.Type, e.Id }, "Waitpoint_environmentId_type_id_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.EnvironmentId, e.IdempotencyKey }, "Waitpoint_environmentId_idempotencyKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WaitpointStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.UserProvidedIdempotencyKey).HasColumnName("userProvidedIdempotencyKey");
            entity.Property(e => e.IdempotencyKeyExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("idempotencyKeyExpiresAt");
            entity.Property(e => e.InactiveIdempotencyKey).HasColumnName("inactiveIdempotencyKey");
            entity.Property(e => e.CompletedByTaskRunId).HasColumnName("completedByTaskRunId");
            entity.Property(e => e.CompletedAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAfter");
            entity.Property(e => e.CompletedByBatchId).HasColumnName("completedByBatchId");
            entity.Property(e => e.Output).HasColumnName("output");
            entity.Property(e => e.OutputType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("outputType");
            entity.Property(e => e.OutputIsError)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("outputIsError");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
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
            entity.Property(e => e.Tags).HasColumnName("tags");

            entity.HasOne(d => d.CompletedByTaskRun).WithOne(p => p.AssociatedWaitpoint)
                .HasForeignKey<Waitpoint>(d => d.CompletedByTaskRunId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Waitpoint_completedByTaskRunId_fkey");

            entity.HasOne(d => d.CompletedByBatch).WithMany(p => p.Waitpoints)
                .HasForeignKey(d => d.CompletedByBatchId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Waitpoint_completedByBatchId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Waitpoints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Waitpoint_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.Waitpoints)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Waitpoint_environmentId_fkey");
        });

        modelBuilder.Entity<TaskRunWaitpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunWaitpoint_pkey");

            entity.ToTable("TaskRunWaitpoint");

            entity.HasIndex(e => e.TaskRunId, "TaskRunWaitpoint_taskRunId_idx");

            entity.HasIndex(e => e.WaitpointId, "TaskRunWaitpoint_waitpointId_idx");

            entity.HasIndex(e => new { e.TaskRunId, e.WaitpointId, e.BatchIndex }, "TaskRunWaitpoint_taskRunId_waitpointId_batchIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskRunId).HasColumnName("taskRunId");
            entity.Property(e => e.WaitpointId).HasColumnName("waitpointId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.SpanIdToComplete).HasColumnName("spanIdToComplete");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.BatchIndex).HasColumnName("batchIndex");
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

            entity.HasOne(d => d.TaskRun).WithMany(p => p.BlockedByWaitpoints)
                .HasForeignKey(d => d.TaskRunId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TaskRunWaitpoint_taskRunId_fkey");

            entity.HasOne(d => d.Waitpoint).WithMany(p => p.BlockingTaskRuns)
                .HasForeignKey(d => d.WaitpointId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TaskRunWaitpoint_waitpointId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskRunWaitpoints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunWaitpoint_projectId_fkey");

            entity.HasOne(d => d.Batch).WithMany(p => p.RunsBlocked)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRunWaitpoint_batchId_fkey");
        });

        modelBuilder.Entity<WaitpointTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WaitpointTag_pkey");

            entity.ToTable("WaitpointTag");

            entity.HasIndex(e => new { e.EnvironmentId, e.Name }, "WaitpointTag_environmentId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Environment).WithMany(p => p.WaitpointTags)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WaitpointTag_environmentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.WaitpointTags)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WaitpointTag_projectId_fkey");
        });

        modelBuilder.Entity<FeatureFlag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeatureFlag_pkey");

            entity.ToTable("FeatureFlag");

            entity.HasIndex(e => e.Key, "FeatureFlag_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
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

        modelBuilder.Entity<WorkerInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkerInstance_pkey");

            entity.ToTable("WorkerInstance");

            entity.HasIndex(e => new { e.WorkerGroupId, e.ResourceIdentifier }, "WorkerInstance_workerGroupId_resourceIdentifier_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ResourceIdentifier).HasColumnName("resourceIdentifier");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.WorkerGroupId).HasColumnName("workerGroupId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.DeploymentId).HasColumnName("deploymentId");
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
            entity.Property(e => e.LastDequeueAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastDequeueAt");
            entity.Property(e => e.LastHeartbeatAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastHeartbeatAt");

            entity.HasOne(d => d.WorkerGroup).WithMany(p => p.Workers)
                .HasForeignKey(d => d.WorkerGroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkerInstance_workerGroupId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.WorkerInstances)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstance_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Workers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstance_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.WorkerInstances)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstance_environmentId_fkey");

            entity.HasOne(d => d.Deployment).WithMany(p => p.WorkerInstance)
                .HasForeignKey(d => d.DeploymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WorkerInstance_deploymentId_fkey");
        });

        modelBuilder.Entity<WorkerInstanceGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkerInstanceGroup_pkey");

            entity.ToTable("WorkerInstanceGroup");

            entity.HasIndex(e => e.MasterQueue, "WorkerInstanceGroup_masterQueue_key").IsUnique();

            entity.HasIndex(e => e.TokenId, "WorkerInstanceGroup_tokenId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.MasterQueue).HasColumnName("masterQueue");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Hidden)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hidden");
            entity.Property(e => e.TokenId).HasColumnName("tokenId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CloudProvider).HasColumnName("cloudProvider");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.StaticIPs).HasColumnName("staticIPs");
            entity.Property(e => e.WorkloadType)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkloadType.Container)
                .HasColumnName("workloadType");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.EnableFastPath)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableFastPath");
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

            entity.HasOne(d => d.Token).WithOne(p => p.WorkerGroup)
                .HasForeignKey<WorkerInstanceGroup>(d => d.TokenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstanceGroup_tokenId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.WorkerGroups)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstanceGroup_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.WorkerGroups)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerInstanceGroup_projectId_fkey");
        });

        modelBuilder.Entity<WorkerGroupToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkerGroupToken_pkey");

            entity.ToTable("WorkerGroupToken");

            entity.HasIndex(e => e.TokenHash, "WorkerGroupToken_tokenHash_key").IsUnique();

            entity.HasIndex(e => e.PreviousTokenHash, "WorkerGroupToken_previousTokenHash_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TokenHash).HasColumnName("tokenHash");
            entity.Property(e => e.PreviousTokenHash).HasColumnName("previousTokenHash");
            entity.Property(e => e.PreviousTokenValidUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("previousTokenValidUntil");
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

        modelBuilder.Entity<TaskRunTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunTag_pkey");

            entity.ToTable("TaskRunTag");

            entity.HasIndex(e => e.FriendlyId, "TaskRunTag_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.Name, e.Id }, "TaskRunTag_name_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "TaskRunTag_projectId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.RunTags)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunTag_projectId_fkey");
        });

        modelBuilder.Entity<TaskRunDependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunDependency_pkey");

            entity.ToTable("TaskRunDependency");

            entity.HasIndex(e => e.TaskRunId, "TaskRunDependency_taskRunId_key").IsUnique();

            entity.HasIndex(e => e.CheckpointEventId, "TaskRunDependency_checkpointEventId_key").IsUnique();

            entity.HasIndex(e => e.DependentAttemptId, "TaskRunDependency_dependentAttemptId_idx");

            entity.HasIndex(e => e.DependentBatchRunId, "TaskRunDependency_dependentBatchRunId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskRunId).HasColumnName("taskRunId");
            entity.Property(e => e.CheckpointEventId).HasColumnName("checkpointEventId");
            entity.Property(e => e.DependentAttemptId).HasColumnName("dependentAttemptId");
            entity.Property(e => e.DependentBatchRunId).HasColumnName("dependentBatchRunId");
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
            entity.Property(e => e.ResumedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resumedAt");

            entity.HasOne(d => d.TaskRun).WithOne(p => p.Dependency)
                .HasForeignKey<TaskRunDependency>(d => d.TaskRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunDependency_taskRunId_fkey");

            entity.HasOne(d => d.CheckpointEvent).WithOne(p => p.TaskRunDependency)
                .HasForeignKey<TaskRunDependency>(d => d.CheckpointEventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunDependency_checkpointEventId_fkey");

            entity.HasOne(d => d.DependentAttempt).WithMany(p => p.Dependencies)
                .HasForeignKey(d => d.DependentAttemptId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRunDependency_dependentAttemptId_fkey");

            entity.HasOne(d => d.DependentBatchRun).WithMany(p => p.RunDependencies)
                .HasForeignKey(d => d.DependentBatchRunId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskRunDependency_dependentBatchRunId_fkey");
        });

        modelBuilder.Entity<TaskRunCounter>(entity =>
        {
            entity.HasKey(e => e.TaskIdentifier).HasName("TaskRunCounter_pkey");

            entity.ToTable("TaskRunCounter");

            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.LastNumber)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastNumber");
        });

        modelBuilder.Entity<TaskRunNumberCounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunNumberCounter_pkey");

            entity.ToTable("TaskRunNumberCounter");

            entity.HasIndex(e => new { e.TaskIdentifier, e.EnvironmentId }, "TaskRunNumberCounter_taskIdentifier_environmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.LastNumber)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("lastNumber");

            entity.HasOne(d => d.Environment).WithMany(p => p.TaskRunNumberCounter)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunNumberCounter_environmentId_fkey");
        });

        modelBuilder.Entity<TaskRunAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskRunAttempt_pkey");

            entity.ToTable("TaskRunAttempt");

            entity.HasIndex(e => e.FriendlyId, "TaskRunAttempt_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.TaskRunId, "TaskRunAttempt_taskRunId_idx");

            entity.HasIndex(e => new { e.TaskRunId, e.Number }, "TaskRunAttempt_taskRunId_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Number)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("number");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.TaskRunId).HasColumnName("taskRunId");
            entity.Property(e => e.BackgroundWorkerId).HasColumnName("backgroundWorkerId");
            entity.Property(e => e.BackgroundWorkerTaskId).HasColumnName("backgroundWorkerTaskId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.QueueId).HasColumnName("queueId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskRunAttemptStatus.Pending)
                .HasColumnName("status");
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
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.UsageDurationMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usageDurationMs");
            entity.Property(e => e.Error)
                .HasColumnType("jsonb")
                .HasColumnName("error");
            entity.Property(e => e.Output).HasColumnName("output");
            entity.Property(e => e.OutputType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("outputType");

            entity.HasOne(d => d.TaskRun).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.TaskRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunAttempt_taskRunId_fkey");

            entity.HasOne(d => d.BackgroundWorker).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.BackgroundWorkerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunAttempt_backgroundWorkerId_fkey");

            entity.HasOne(d => d.BackgroundWorkerTask).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.BackgroundWorkerTaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunAttempt_backgroundWorkerTaskId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.TaskRunAttempts)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunAttempt_runtimeEnvironmentId_fkey");

            entity.HasOne(d => d.Queue).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskRunAttempt_queueId_fkey");
        });

        modelBuilder.Entity<TaskEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskEvent_pkey");

            entity.ToTable("TaskEvent");

            entity.HasIndex(e => e.TraceId, "TaskEvent_traceId_idx");

            entity.HasIndex(e => e.SpanId, "TaskEvent_spanId_idx");

            entity.HasIndex(e => e.RunId, "TaskEvent_runId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.SpanId).HasColumnName("spanId");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Tracestate).HasColumnName("tracestate");
            entity.Property(e => e.IsError)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isError");
            entity.Property(e => e.IsPartial)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPartial");
            entity.Property(e => e.IsCancelled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isCancelled");
            entity.Property(e => e.IsDebug)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDebug");
            entity.Property(e => e.ServiceName).HasColumnName("serviceName");
            entity.Property(e => e.ServiceNamespace).HasColumnName("serviceNamespace");
            entity.Property(e => e.Level)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventLevel.Trace)
                .HasColumnName("level");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventKind.Internal)
                .HasColumnName("kind");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventStatus.Unset)
                .HasColumnName("status");
            entity.Property(e => e.Links)
                .HasColumnType("jsonb")
                .HasColumnName("links");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.Duration)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("duration");
            entity.Property(e => e.AttemptId).HasColumnName("attemptId");
            entity.Property(e => e.AttemptNumber).HasColumnName("attemptNumber");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProjectRef).HasColumnName("projectRef");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.RunIsTest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("runIsTest");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.TaskSlug).HasColumnName("taskSlug");
            entity.Property(e => e.TaskPath).HasColumnName("taskPath");
            entity.Property(e => e.TaskExportName).HasColumnName("taskExportName");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.WorkerVersion).HasColumnName("workerVersion");
            entity.Property(e => e.QueueId).HasColumnName("queueId");
            entity.Property(e => e.QueueName).HasColumnName("queueName");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.Properties)
                .HasColumnType("jsonb")
                .HasColumnName("properties");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Style)
                .HasColumnType("jsonb")
                .HasColumnName("style");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.OutputType).HasColumnName("outputType");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.PayloadType).HasColumnName("payloadType");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UsageDurationMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usageDurationMs");
            entity.Property(e => e.UsageCostInCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("usageCostInCents");
            entity.Property(e => e.MachinePreset).HasColumnName("machinePreset");
            entity.Property(e => e.MachinePresetCpu).HasColumnName("machinePresetCpu");
            entity.Property(e => e.MachinePresetMemory).HasColumnName("machinePresetMemory");
            entity.Property(e => e.MachinePresetCentsPerMs).HasColumnName("machinePresetCentsPerMs");
        });

        modelBuilder.Entity<TaskQueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskQueue_pkey");

            entity.ToTable("TaskQueue");

            entity.HasIndex(e => e.FriendlyId, "TaskQueue_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.Name }, "TaskQueue_runtimeEnvironmentId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskQueueType.Virtual)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskQueueVersion.V1)
                .HasColumnName("version");
            entity.Property(e => e.OrderableName).HasColumnName("orderableName");
            entity.Property(e => e.ConcurrencyVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskQueueConcurrencyVersion.V1)
                .HasColumnName("concurrencyVersion");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskQueueRole.Queue)
                .HasColumnName("role");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.ConcurrencyLimit).HasColumnName("concurrencyLimit");
            entity.Property(e => e.ConcurrencyLimitOverriddenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("concurrencyLimitOverriddenAt");
            entity.Property(e => e.ConcurrencyLimitOverriddenBy).HasColumnName("concurrencyLimitOverriddenBy");
            entity.Property(e => e.ConcurrencyLimitBase).HasColumnName("concurrencyLimitBase");
            entity.Property(e => e.ConcurrencyLimitOverridePercent)
                .HasPrecision(5, 2)
                .HasColumnName("concurrencyLimitOverridePercent");
            entity.Property(e => e.TotalConcurrencyLimit).HasColumnName("totalConcurrencyLimit");
            entity.Property(e => e.TotalConcurrencyLimitOverriddenAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("totalConcurrencyLimitOverriddenAt");
            entity.Property(e => e.TotalConcurrencyLimitOverriddenBy).HasColumnName("totalConcurrencyLimitOverriddenBy");
            entity.Property(e => e.TotalConcurrencyLimitBase).HasColumnName("totalConcurrencyLimitBase");
            entity.Property(e => e.RateLimit)
                .HasColumnType("jsonb")
                .HasColumnName("rateLimit");
            entity.Property(e => e.Paused)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("paused");
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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskQueues)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskQueue_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.TaskQueues)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskQueue_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<BatchTaskRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchTaskRun_pkey");

            entity.ToTable("BatchTaskRun");

            entity.HasIndex(e => e.FriendlyId, "BatchTaskRun_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.CheckpointEventId, "BatchTaskRun_checkpointEventId_key").IsUnique();

            entity.HasIndex(e => e.DependentTaskAttemptId, "BatchTaskRun_dependentTaskAttemptId_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.Id }, "BatchTaskRun_runtimeEnvironmentId_id_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.CreatedAt, e.Id }, "BatchTaskRun_runtimeEnvironmentId_createdAt_id_idx").IsDescending(false, true, true);

            entity.HasIndex(e => e.OneTimeUseToken, "BatchTaskRun_oneTimeUseToken_key").IsUnique();

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.IdempotencyKey }, "BatchTaskRun_runtimeEnvironmentId_idempotencyKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.IdempotencyKeyExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("idempotencyKeyExpiresAt");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BatchTaskRunStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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
            entity.Property(e => e.RunIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("runIds");
            entity.Property(e => e.RunCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("runCount");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.PayloadType)
                .ValueGeneratedNever()
                .HasDefaultValue("application/json")
                .HasColumnName("payloadType");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
            entity.Property(e => e.BatchVersion)
                .ValueGeneratedNever()
                .HasDefaultValue("v1")
                .HasColumnName("batchVersion");
            entity.Property(e => e.Sealed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sealed");
            entity.Property(e => e.SealedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sealedAt");
            entity.Property(e => e.ExpectedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("expectedCount");
            entity.Property(e => e.CompletedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("completedCount");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.ResumedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resumedAt");
            entity.Property(e => e.ProcessingJobsCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processingJobsCount");
            entity.Property(e => e.ProcessingJobsExpectedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processingJobsExpectedCount");
            entity.Property(e => e.OneTimeUseToken).HasColumnName("oneTimeUseToken");
            entity.Property(e => e.ProcessingStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processingStartedAt");
            entity.Property(e => e.ProcessingCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processingCompletedAt");
            entity.Property(e => e.SuccessfulRunCount).HasColumnName("successfulRunCount");
            entity.Property(e => e.FailedRunCount).HasColumnName("failedRunCount");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.CheckpointEventId).HasColumnName("checkpointEventId");
            entity.Property(e => e.DependentTaskAttemptId).HasColumnName("dependentTaskAttemptId");

            entity.HasOne(d => d.CheckpointEvent).WithOne(p => p.BatchTaskRunDependency)
                .HasForeignKey<BatchTaskRun>(d => d.CheckpointEventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BatchTaskRun_checkpointEventId_fkey");

            entity.HasOne(d => d.DependentTaskAttempt).WithMany(p => p.BatchDependencies)
                .HasForeignKey(d => d.DependentTaskAttemptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BatchTaskRun_dependentTaskAttemptId_fkey");
        });

        modelBuilder.Entity<BatchTaskRunItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchTaskRunItem_pkey");

            entity.ToTable("BatchTaskRunItem");

            entity.HasIndex(e => e.TaskRunAttemptId, "idx_batchtaskrunitem_taskrunattempt");

            entity.HasIndex(e => e.TaskRunId, "idx_batchtaskrunitem_taskrun");

            entity.HasIndex(e => new { e.BatchTaskRunId, e.TaskRunId }, "BatchTaskRunItem_batchTaskRunId_taskRunId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BatchTaskRunItemStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.BatchTaskRunId).HasColumnName("batchTaskRunId");
            entity.Property(e => e.TaskRunId).HasColumnName("taskRunId");
            entity.Property(e => e.TaskRunAttemptId).HasColumnName("taskRunAttemptId");
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
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");

            entity.HasOne(d => d.BatchTaskRun).WithMany(p => p.Items)
                .HasForeignKey(d => d.BatchTaskRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BatchTaskRunItem_batchTaskRunId_fkey");

            entity.HasOne(d => d.TaskRun).WithMany(p => p.BatchItems)
                .HasForeignKey(d => d.TaskRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BatchTaskRunItem_taskRunId_fkey");

            entity.HasOne(d => d.TaskRunAttempt).WithMany(p => p.BatchTaskRunItems)
                .HasForeignKey(d => d.TaskRunAttemptId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BatchTaskRunItem_taskRunAttemptId_fkey");
        });

        modelBuilder.Entity<BatchTaskRunError>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchTaskRunError_pkey");

            entity.ToTable("BatchTaskRunError");

            entity.HasIndex(e => e.BatchTaskRunId, "BatchTaskRunError_batchTaskRunId_idx");

            entity.HasIndex(e => new { e.BatchTaskRunId, e.Index }, "BatchTaskRunError_batchTaskRunId_index_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BatchTaskRunId).HasColumnName("batchTaskRunId");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.ErrorCode).HasColumnName("errorCode");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.BatchTaskRun).WithMany(p => p.Errors)
                .HasForeignKey(d => d.BatchTaskRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BatchTaskRunError_batchTaskRunId_fkey");
        });

        modelBuilder.Entity<EnvironmentVariable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvironmentVariable_pkey");

            entity.ToTable("EnvironmentVariable");

            entity.HasIndex(e => e.FriendlyId, "EnvironmentVariable_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.Key }, "EnvironmentVariable_projectId_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
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

            entity.HasOne(d => d.Project).WithMany(p => p.EnvironmentVariables)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvironmentVariable_projectId_fkey");
        });

        modelBuilder.Entity<EnvironmentVariableValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvironmentVariableValue_pkey");

            entity.ToTable("EnvironmentVariableValue");

            entity.HasIndex(e => e.EnvironmentId, "EnvironmentVariableValue_environmentId_idx");

            entity.HasIndex(e => e.ValueReferenceId, "EnvironmentVariableValue_valueReferenceId_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.UpdatedAt, e.VariableId }, "EnvironmentVariableValue_environmentId_updatedAt_variableId_idx").IsDescending(false, true, false);

            entity.HasIndex(e => new { e.VariableId, e.EnvironmentId }, "EnvironmentVariableValue_variableId_environmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ValueReferenceId).HasColumnName("valueReferenceId");
            entity.Property(e => e.VariableId).HasColumnName("variableId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.IsSecret)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSecret");
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
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("version");
            entity.Property(e => e.LastUpdatedBy)
                .HasColumnType("jsonb")
                .HasColumnName("lastUpdatedBy");

            entity.HasOne(d => d.ValueReference).WithMany(p => p.EnvironmentVariableValues)
                .HasForeignKey(d => d.ValueReferenceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EnvironmentVariableValue_valueReferenceId_fkey");

            entity.HasOne(d => d.Variable).WithMany(p => p.Values)
                .HasForeignKey(d => d.VariableId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvironmentVariableValue_variableId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.EnvironmentVariableValues)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvironmentVariableValue_environmentId_fkey");
        });

        modelBuilder.Entity<Checkpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Checkpoint_pkey");

            entity.ToTable("Checkpoint");

            entity.HasIndex(e => e.FriendlyId, "Checkpoint_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.AttemptId, "Checkpoint_attemptId_idx");

            entity.HasIndex(e => e.RunId, "Checkpoint_runId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.ImageRef).HasColumnName("imageRef");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.AttemptId).HasColumnName("attemptId");
            entity.Property(e => e.AttemptNumber).HasColumnName("attemptNumber");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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

            entity.HasOne(d => d.Run).WithMany(p => p.Checkpoints)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Checkpoint_runId_fkey");

            entity.HasOne(d => d.Attempt).WithMany(p => p.Checkpoints)
                .HasForeignKey(d => d.AttemptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Checkpoint_attemptId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Checkpoints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Checkpoint_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.Checkpoints)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Checkpoint_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<CheckpointRestoreEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CheckpointRestoreEvent_pkey");

            entity.ToTable("CheckpointRestoreEvent");

            entity.HasIndex(e => e.CheckpointId, "CheckpointRestoreEvent_checkpointId_idx");

            entity.HasIndex(e => e.RunId, "CheckpointRestoreEvent_runId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.CheckpointId).HasColumnName("checkpointId");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.AttemptId).HasColumnName("attemptId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
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

            entity.HasOne(d => d.Checkpoint).WithMany(p => p.Events)
                .HasForeignKey(d => d.CheckpointId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CheckpointRestoreEvent_checkpointId_fkey");

            entity.HasOne(d => d.Run).WithMany(p => p.CheckpointRestoreEvent)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CheckpointRestoreEvent_runId_fkey");

            entity.HasOne(d => d.Attempt).WithMany(p => p.CheckpointRestoreEvent)
                .HasForeignKey(d => d.AttemptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CheckpointRestoreEvent_attemptId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.CheckpointRestoreEvent)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CheckpointRestoreEvent_projectId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.CheckpointRestoreEvent)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CheckpointRestoreEvent_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<WorkerDeployment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkerDeployment_pkey");

            entity.ToTable("WorkerDeployment");

            entity.HasIndex(e => e.FriendlyId, "WorkerDeployment_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.WorkerId, "WorkerDeployment_workerId_key").IsUnique();

            entity.HasIndex(e => e.CommitSHA, "WorkerDeployment_commitSHA_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.CreatedAt }, "WorkerDeployment_environmentId_createdAt_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.Status, e.Id }, "WorkerDeployment_environmentId_status_id_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.ExternalId }, "WorkerDeployment_environmentId_externalId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ShortCode }, "WorkerDeployment_projectId_shortCode_key").IsUnique();

            entity.HasIndex(e => new { e.EnvironmentId, e.Version }, "WorkerDeployment_environmentId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ContentHash).HasColumnName("contentHash");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.ShortCode).HasColumnName("shortCode");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.RuntimeVersion).HasColumnName("runtimeVersion");
            entity.Property(e => e.CliVersion).HasColumnName("cliVersion");
            entity.Property(e => e.ImageReference).HasColumnName("imageReference");
            entity.Property(e => e.ImagePlatform)
                .ValueGeneratedNever()
                .HasDefaultValue("linux/amd64")
                .HasColumnName("imagePlatform");
            entity.Property(e => e.TemplateKey).HasColumnName("templateKey");
            entity.Property(e => e.IndexObjectKey).HasColumnName("indexObjectKey");
            entity.Property(e => e.IndexSha256).HasColumnName("indexSha256");
            entity.Property(e => e.IndexFormatVersion).HasColumnName("indexFormatVersion");
            entity.Property(e => e.ExternalBuildData)
                .HasColumnType("jsonb")
                .HasColumnName("externalBuildData");
            entity.Property(e => e.BuildServerMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("buildServerMetadata");
            entity.Property(e => e.BuildEnvVars)
                .HasColumnType("jsonb")
                .HasColumnName("buildEnvVars");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkerDeploymentStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkerDeploymentType.V1)
                .HasColumnName("type");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.TriggeredById).HasColumnName("triggeredById");
            entity.Property(e => e.TriggeredVia).HasColumnName("triggeredVia");
            entity.Property(e => e.CommitSHA).HasColumnName("commitSHA");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.InstalledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("installedAt");
            entity.Property(e => e.BuiltAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("builtAt");
            entity.Property(e => e.DeployedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deployedAt");
            entity.Property(e => e.FailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failedAt");
            entity.Property(e => e.ErrorData)
                .HasColumnType("jsonb")
                .HasColumnName("errorData");
            entity.Property(e => e.CanceledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("canceledAt");
            entity.Property(e => e.CanceledReason).HasColumnName("canceledReason");
            entity.Property(e => e.Git)
                .HasColumnType("jsonb")
                .HasColumnName("git");
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

            entity.HasOne(d => d.Project).WithMany(p => p.WorkerDeployment)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerDeployment_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.WorkerDeployments)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerDeployment_environmentId_fkey");

            entity.HasOne(d => d.Worker).WithOne(p => p.Deployment)
                .HasForeignKey<WorkerDeployment>(d => d.WorkerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerDeployment_workerId_fkey");

            entity.HasOne(d => d.TriggeredBy).WithMany(p => p.Deployments)
                .HasForeignKey(d => d.TriggeredById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WorkerDeployment_triggeredById_fkey");
        });

        modelBuilder.Entity<WorkerDeploymentPromotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkerDeploymentPromotion_pkey");

            entity.ToTable("WorkerDeploymentPromotion");

            entity.HasIndex(e => new { e.EnvironmentId, e.Label }, "WorkerDeploymentPromotion_environmentId_label_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.DeploymentId).HasColumnName("deploymentId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");

            entity.HasOne(d => d.Deployment).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.DeploymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerDeploymentPromotion_deploymentId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.WorkerDeploymentPromotions)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkerDeploymentPromotion_environmentId_fkey");
        });

        modelBuilder.Entity<TaskSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskSchedule_pkey");

            entity.ToTable("TaskSchedule");

            entity.HasIndex(e => e.FriendlyId, "TaskSchedule_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "TaskSchedule_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "TaskSchedule_projectId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.ProjectId, e.DeduplicationKey }, "TaskSchedule_projectId_deduplicationKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduleType.Imperative)
                .HasColumnName("type");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.DeduplicationKey)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("deduplicationKey");
            entity.Property(e => e.UserProvidedDeduplicationKey)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("userProvidedDeduplicationKey");
            entity.Property(e => e.GeneratorExpression).HasColumnName("generatorExpression");
            entity.Property(e => e.GeneratorDescription)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("generatorDescription");
            entity.Property(e => e.GeneratorType)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduleGeneratorType.Cron)
                .HasColumnName("generatorType");
            entity.Property(e => e.Timezone)
                .ValueGeneratedNever()
                .HasDefaultValue("UTC")
                .HasColumnName("timezone");
            entity.Property(e => e.WindowDurationSeconds).HasColumnName("windowDurationSeconds");
            entity.Property(e => e.WindowPercentage).HasColumnName("windowPercentage");
            entity.Property(e => e.DefaultWindowDurationSeconds).HasColumnName("defaultWindowDurationSeconds");
            entity.Property(e => e.MinimumWindowDurationSeconds).HasColumnName("minimumWindowDurationSeconds");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.LastRunTriggeredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastRunTriggeredAt");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
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
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskSchedules)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskSchedule_projectId_fkey");
        });

        modelBuilder.Entity<TaskScheduleInstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskScheduleInstance_pkey");

            entity.ToTable("TaskScheduleInstance");

            entity.HasIndex(e => e.EnvironmentId, "TaskScheduleInstance_environmentId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Active }, "TaskScheduleInstance_projectId_active_idx");

            entity.HasIndex(e => new { e.TaskScheduleId, e.EnvironmentId }, "TaskScheduleInstance_taskScheduleId_environmentId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskScheduleId).HasColumnName("taskScheduleId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.SchedulePhase).HasColumnName("schedulePhase");
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
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.LastScheduledTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastScheduledTimestamp");
            entity.Property(e => e.NextScheduledTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextScheduledTimestamp");

            entity.HasOne(d => d.TaskSchedule).WithMany(p => p.Instances)
                .HasForeignKey(d => d.TaskScheduleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskScheduleInstance_taskScheduleId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.TaskScheduleInstances)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskScheduleInstance_environmentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskScheduleInstances)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskScheduleInstance_projectId_fkey");
        });

        modelBuilder.Entity<RuntimeEnvironmentSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RuntimeEnvironmentSession_pkey");

            entity.ToTable("RuntimeEnvironmentSession");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
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
            entity.Property(e => e.DisconnectedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("disconnectedAt");

            entity.HasOne(d => d.Environment).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuntimeEnvironmentSession_environmentId_fkey");
        });

        modelBuilder.Entity<ProjectAlertChannel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectAlertChannel_pkey");

            entity.ToTable("ProjectAlertChannel");

            entity.HasIndex(e => e.FriendlyId, "ProjectAlertChannel_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.DeduplicationKey }, "ProjectAlertChannel_projectId_deduplicationKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.DeduplicationKey)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("deduplicationKey");
            entity.Property(e => e.UserProvidedDeduplicationKey)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("userProvidedDeduplicationKey");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Properties)
                .HasColumnType("jsonb")
                .HasColumnName("properties");
            entity.Property(e => e.AlertTypes).HasColumnName("alertTypes");
            entity.Property(e => e.EnvironmentTypes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<RuntimeEnvironmentType> { RuntimeEnvironmentType.Staging, RuntimeEnvironmentType.Production })
                .HasColumnName("environmentTypes");
            entity.Property(e => e.ErrorAlertConfig)
                .HasColumnType("jsonb")
                .HasColumnName("errorAlertConfig");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
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

            entity.HasOne(d => d.Integration).WithMany(p => p.AlertChannels)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ProjectAlertChannel_integrationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.AlertChannels)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlertChannel_projectId_fkey");
        });

        modelBuilder.Entity<ProjectAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectAlert_pkey");

            entity.ToTable("ProjectAlert");

            entity.HasIndex(e => e.FriendlyId, "ProjectAlert_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.ChannelId, "ProjectAlert_channelId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.ChannelId).HasColumnName("channelId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ProjectAlertStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.TaskRunAttemptId).HasColumnName("taskRunAttemptId");
            entity.Property(e => e.TaskRunId).HasColumnName("taskRunId");
            entity.Property(e => e.WorkerDeploymentId).HasColumnName("workerDeploymentId");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlert_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlert_environmentId_fkey");

            entity.HasOne(d => d.Channel).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlert_channelId_fkey");

            entity.HasOne(d => d.WorkerDeployment).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.WorkerDeploymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlert_workerDeploymentId_fkey");
        });

        modelBuilder.Entity<ProjectAlertStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectAlertStorage_pkey");

            entity.ToTable("ProjectAlertStorage");

            entity.HasIndex(e => new { e.AlertChannelId, e.AlertType, e.StorageId }, "ProjectAlertStorage_alertChannelId_alertType_storageId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.AlertChannelId).HasColumnName("alertChannelId");
            entity.Property(e => e.AlertType).HasColumnName("alertType");
            entity.Property(e => e.StorageId).HasColumnName("storageId");
            entity.Property(e => e.StorageData)
                .HasColumnType("jsonb")
                .HasColumnName("storageData");
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

            entity.HasOne(d => d.Project).WithMany(p => p.AlertStorages)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlertStorage_projectId_fkey");

            entity.HasOne(d => d.AlertChannel).WithMany(p => p.AlertStorages)
                .HasForeignKey(d => d.AlertChannelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProjectAlertStorage_alertChannelId_fkey");
        });

        modelBuilder.Entity<OrganizationIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationIntegration_pkey");

            entity.ToTable("OrganizationIntegration");

            entity.HasIndex(e => e.FriendlyId, "OrganizationIntegration_friendlyId_key").IsUnique();

            entity.HasIndex(e => e.ExternalOrganizationId, "OrganizationIntegration_externalOrganizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Service).HasColumnName("service");
            entity.Property(e => e.ExternalOrganizationId).HasColumnName("externalOrganizationId");
            entity.Property(e => e.IntegrationData)
                .HasColumnType("jsonb")
                .HasColumnName("integrationData");
            entity.Property(e => e.TokenReferenceId).HasColumnName("tokenReferenceId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");

            entity.HasOne(d => d.TokenReference).WithMany(p => p.OrganizationIntegration)
                .HasForeignKey(d => d.TokenReferenceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationIntegration_tokenReferenceId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationIntegrations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationIntegration_organizationId_fkey");
        });

        modelBuilder.Entity<OrganizationProjectIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationProjectIntegration_pkey");

            entity.ToTable("OrganizationProjectIntegration");

            entity.HasIndex(e => e.ProjectId, "OrganizationProjectIntegration_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.OrganizationIntegrationId }, "OrganizationProjectIntegration_projectId_organizationIntegr_idx");

            entity.HasIndex(e => e.ExternalEntityId, "OrganizationProjectIntegration_externalEntityId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationIntegrationId).HasColumnName("organizationIntegrationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ExternalEntityId).HasColumnName("externalEntityId");
            entity.Property(e => e.IntegrationData)
                .HasColumnType("jsonb")
                .HasColumnName("integrationData");
            entity.Property(e => e.InstalledBy).HasColumnName("installedBy");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");

            entity.HasOne(d => d.OrganizationIntegration).WithMany(p => p.OrganizationProjectIntegration)
                .HasForeignKey(d => d.OrganizationIntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationProjectIntegration_organizationIntegrationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.OrganizationProjectIntegration)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationProjectIntegration_projectId_fkey");
        });

        modelBuilder.Entity<BulkActionGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BulkActionGroup_pkey");

            entity.ToTable("BulkActionGroup");

            entity.HasIndex(e => e.FriendlyId, "BulkActionGroup_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.EnvironmentId, e.CreatedAt }, "BulkActionGroup_environmentId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.EnvironmentId, e.Type, e.DedupeKey }, "BulkActionGroup_environmentId_type_dedupeKey_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.Status, e.Type }, "BulkActionGroup_environmentId_status_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BulkActionStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.QueryName).HasColumnName("queryName");
            entity.Property(e => e.Params)
                .HasColumnType("jsonb")
                .HasColumnName("params");
            entity.Property(e => e.DedupeKey).HasColumnName("dedupeKey");
            entity.Property(e => e.Cursor)
                .HasColumnType("jsonb")
                .HasColumnName("cursor");
            entity.Property(e => e.SuccessCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("successCount");
            entity.Property(e => e.FailureCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failureCount");
            entity.Property(e => e.TotalCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalCount");
            entity.Property(e => e.CompletionNotification)
                .ValueGeneratedNever()
                .HasDefaultValue(BulkActionNotificationType.None)
                .HasColumnName("completionNotification");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
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

            entity.HasOne(d => d.Project).WithMany(p => p.BulkActionGroups)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BulkActionGroup_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.BulkActionGroup)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BulkActionGroup_environmentId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.BulkActions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("BulkActionGroup_userId_fkey");
        });

        modelBuilder.Entity<BulkActionItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BulkActionItem_pkey");

            entity.ToTable("BulkActionItem");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BulkActionItemStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.SourceRunId).HasColumnName("sourceRunId");
            entity.Property(e => e.DestinationRunId).HasColumnName("destinationRunId");
            entity.Property(e => e.Error).HasColumnName("error");
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

            entity.HasOne(d => d.SourceRun).WithMany(p => p.SourceBulkActionItems)
                .HasForeignKey(d => d.SourceRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BulkActionItem_sourceRunId_fkey");

            entity.HasOne(d => d.DestinationRun).WithMany(p => p.DestinationBulkActionItems)
                .HasForeignKey(d => d.DestinationRunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BulkActionItem_destinationRunId_fkey");
        });

        modelBuilder.Entity<RealtimeStreamChunk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RealtimeStreamChunk_pkey");

            entity.ToTable("RealtimeStreamChunk");

            entity.HasIndex(e => e.RunId, "RealtimeStreamChunk_runId_idx");

            entity.HasIndex(e => e.CreatedAt, "RealtimeStreamChunk_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<TaskEventPartitioned>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedAt }).HasName("TaskEventPartitioned_pkey");

            entity.ToTable("TaskEventPartitioned");

            entity.HasIndex(e => e.TraceId, "TaskEventPartitioned_traceId_idx");

            entity.HasIndex(e => e.SpanId, "TaskEventPartitioned_spanId_idx");

            entity.HasIndex(e => e.RunId, "TaskEventPartitioned_runId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.SpanId).HasColumnName("spanId");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Tracestate).HasColumnName("tracestate");
            entity.Property(e => e.IsError)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isError");
            entity.Property(e => e.IsPartial)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPartial");
            entity.Property(e => e.IsCancelled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isCancelled");
            entity.Property(e => e.ServiceName).HasColumnName("serviceName");
            entity.Property(e => e.ServiceNamespace).HasColumnName("serviceNamespace");
            entity.Property(e => e.Level)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventLevel.Trace)
                .HasColumnName("level");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventKind.Internal)
                .HasColumnName("kind");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskEventStatus.Unset)
                .HasColumnName("status");
            entity.Property(e => e.Links)
                .HasColumnType("jsonb")
                .HasColumnName("links");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.Duration)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("duration");
            entity.Property(e => e.AttemptId).HasColumnName("attemptId");
            entity.Property(e => e.AttemptNumber).HasColumnName("attemptNumber");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.EnvironmentType).HasColumnName("environmentType");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ProjectRef).HasColumnName("projectRef");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.RunIsTest)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("runIsTest");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.TaskSlug).HasColumnName("taskSlug");
            entity.Property(e => e.TaskPath).HasColumnName("taskPath");
            entity.Property(e => e.TaskExportName).HasColumnName("taskExportName");
            entity.Property(e => e.WorkerId).HasColumnName("workerId");
            entity.Property(e => e.WorkerVersion).HasColumnName("workerVersion");
            entity.Property(e => e.QueueId).HasColumnName("queueId");
            entity.Property(e => e.QueueName).HasColumnName("queueName");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.Properties)
                .HasColumnType("jsonb")
                .HasColumnName("properties");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Style)
                .HasColumnType("jsonb")
                .HasColumnName("style");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.OutputType).HasColumnName("outputType");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.PayloadType).HasColumnName("payloadType");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UsageDurationMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usageDurationMs");
            entity.Property(e => e.UsageCostInCents)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("usageCostInCents");
            entity.Property(e => e.MachinePreset).HasColumnName("machinePreset");
            entity.Property(e => e.MachinePresetCpu).HasColumnName("machinePresetCpu");
            entity.Property(e => e.MachinePresetMemory).HasColumnName("machinePresetMemory");
            entity.Property(e => e.MachinePresetCentsPerMs).HasColumnName("machinePresetCentsPerMs");
        });

        modelBuilder.Entity<GithubAppInstallation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GithubAppInstallation_pkey");

            entity.ToTable("GithubAppInstallation");

            entity.HasIndex(e => e.AppInstallationId, "GithubAppInstallation_appInstallationId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "GithubAppInstallation_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AppInstallationId).HasColumnName("appInstallationId");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.AccountHandle).HasColumnName("accountHandle");
            entity.Property(e => e.Permissions)
                .HasColumnType("jsonb")
                .HasColumnName("permissions");
            entity.Property(e => e.RepositorySelection).HasColumnName("repositorySelection");
            entity.Property(e => e.InstalledBy).HasColumnName("installedBy");
            entity.Property(e => e.InstalledByUserId).HasColumnName("installedByUserId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.GithubAppInstallations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("GithubAppInstallation_organizationId_fkey");
        });

        modelBuilder.Entity<GithubRepository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GithubRepository_pkey");

            entity.ToTable("GithubRepository");

            entity.HasIndex(e => e.InstallationId, "GithubRepository_installationId_idx");

            entity.HasIndex(e => new { e.InstallationId, e.GithubId }, "GithubRepository_installationId_githubId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.GithubId).HasColumnName("githubId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.FullName).HasColumnName("fullName");
            entity.Property(e => e.HtmlUrl).HasColumnName("htmlUrl");
            entity.Property(e => e.Private).HasColumnName("private");
            entity.Property(e => e.DefaultBranch).HasColumnName("defaultBranch");
            entity.Property(e => e.InstallationId).HasColumnName("installationId");
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

            entity.HasOne(d => d.Installation).WithMany(p => p.Repositories)
                .HasForeignKey(d => d.InstallationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GithubRepository_installationId_fkey");
        });

        modelBuilder.Entity<TrustedDeployRepository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrustedDeployRepository_pkey");

            entity.ToTable("TrustedDeployRepository");

            entity.HasIndex(e => e.ProjectId, "TrustedDeployRepository_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Issuer)
                .ValueGeneratedNever()
                .HasDefaultValue("https://token.actions.githubusercontent.com")
                .HasColumnName("issuer");
            entity.Property(e => e.RepositoryOwner).HasColumnName("repositoryOwner");
            entity.Property(e => e.RepositoryName).HasColumnName("repositoryName");
            entity.Property(e => e.RepositoryId).HasColumnName("repositoryId");
            entity.Property(e => e.RepositoryOwnerId).HasColumnName("repositoryOwnerId");
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

            entity.HasOne(d => d.Project).WithOne(p => p.TrustedDeployRepository)
                .HasForeignKey<TrustedDeployRepository>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TrustedDeployRepository_projectId_fkey");
        });

        modelBuilder.Entity<TrustedDeployEnvironment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrustedDeployEnvironment_pkey");

            entity.ToTable("TrustedDeployEnvironment");

            entity.HasIndex(e => e.RuntimeEnvironmentId, "TrustedDeployEnvironment_runtimeEnvironmentId_key").IsUnique();

            entity.HasIndex(e => e.TrustedDeployRepositoryId, "TrustedDeployEnvironment_trustedDeployRepositoryId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TrustedDeployRepositoryId).HasColumnName("trustedDeployRepositoryId");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ScopeValue).HasColumnName("scopeValue");
            entity.Property(e => e.AdditionalClaims)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("additionalClaims");
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

            entity.HasOne(d => d.TrustedDeployRepository).WithMany(p => p.Environments)
                .HasForeignKey(d => d.TrustedDeployRepositoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TrustedDeployEnvironment_trustedDeployRepositoryId_fkey");

            entity.HasOne(d => d.RuntimeEnvironment).WithOne(p => p.TrustedDeployGrant)
                .HasForeignKey<TrustedDeployEnvironment>(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TrustedDeployEnvironment_runtimeEnvironmentId_fkey");
        });

        modelBuilder.Entity<ConnectedGithubRepository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectedGithubRepository_pkey");

            entity.ToTable("ConnectedGithubRepository");

            entity.HasIndex(e => e.RepositoryId, "ConnectedGithubRepository_repositoryId_idx");

            entity.HasIndex(e => e.ProjectId, "ConnectedGithubRepository_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.RepositoryId).HasColumnName("repositoryId");
            entity.Property(e => e.BranchTracking)
                .HasColumnType("jsonb")
                .HasColumnName("branchTracking");
            entity.Property(e => e.PreviewDeploymentsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("previewDeploymentsEnabled");
            entity.Property(e => e.BuildFilters)
                .HasColumnType("jsonb")
                .HasColumnName("buildFilters");
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

            entity.HasOne(d => d.Project).WithOne(p => p.ConnectedGithubRepository)
                .HasForeignKey<ConnectedGithubRepository>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConnectedGithubRepository_projectId_fkey");

            entity.HasOne(d => d.Repository).WithMany(p => p.ConnectedGithubRepository)
                .HasForeignKey(d => d.RepositoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ConnectedGithubRepository_repositoryId_fkey");
        });

        modelBuilder.Entity<ImpersonationAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImpersonationAuditLog_pkey");

            entity.ToTable("ImpersonationAuditLog");

            entity.HasIndex(e => e.AdminId, "ImpersonationAuditLog_adminId_idx");

            entity.HasIndex(e => e.TargetId, "ImpersonationAuditLog_targetId_idx");

            entity.HasIndex(e => e.CreatedAt, "ImpersonationAuditLog_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.AdminId).HasColumnName("adminId");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Admin).WithMany(p => p.ImpersonationsPerformed)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ImpersonationAuditLog_adminId_fkey");

            entity.HasOne(d => d.Target).WithMany(p => p.ImpersonationsReceived)
                .HasForeignKey(d => d.TargetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ImpersonationAuditLog_targetId_fkey");
        });

        modelBuilder.Entity<SupportAccessRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SupportAccessRequest_pkey");

            entity.ToTable("SupportAccessRequest");

            entity.HasIndex(e => new { e.OrganizationId, e.Status, e.ExpiresAt }, "SupportAccessRequest_organizationId_status_expiresAt_idx");

            entity.HasIndex(e => e.RequestedById, "SupportAccessRequest_requestedById_idx");

            entity.HasIndex(e => e.ApprovedById, "SupportAccessRequest_approvedById_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.RequestedById).HasColumnName("requestedById");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(SupportAccessRequestStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ApprovedById).HasColumnName("approvedById");
            entity.Property(e => e.ApprovedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("approvedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CancelledById).HasColumnName("cancelledById");
            entity.Property(e => e.CancelledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cancelledAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.SupportAccessRequests)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SupportAccessRequest_organizationId_fkey");

            entity.HasOne(d => d.RequestedBy).WithMany(p => p.SupportAccessRequestsMade)
                .HasForeignKey(d => d.RequestedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SupportAccessRequest_requestedById_fkey");

            entity.HasOne(d => d.ApprovedBy).WithMany(p => p.SupportAccessRequestsApproved)
                .HasForeignKey(d => d.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SupportAccessRequest_approvedById_fkey");
        });

        modelBuilder.Entity<CustomerQuery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomerQuery_pkey");

            entity.ToTable("CustomerQuery");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt }, "CustomerQuery_organizationId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => e.CreatedAt, "CustomerQuery_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Query).HasColumnName("query");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Stats)
                .HasColumnType("jsonb")
                .HasColumnName("stats");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue(CustomerQuerySource.Dashboard)
                .HasColumnName("source");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FilterPeriod).HasColumnName("filterPeriod");
            entity.Property(e => e.FilterFrom)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("filterFrom");
            entity.Property(e => e.FilterTo)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("filterTo");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.CustomerQueries)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomerQuery_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.CustomerQueries)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomerQuery_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.CustomerQueries)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomerQuery_environmentId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerQueries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("CustomerQuery_userId_fkey");
        });

        modelBuilder.Entity<IntegrationDeployment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("IntegrationDeployment_pkey");

            entity.ToTable("IntegrationDeployment");

            entity.HasIndex(e => e.CommitSHA, "IntegrationDeployment_commitSHA_idx");

            entity.HasIndex(e => e.DeploymentId, "IntegrationDeployment_deploymentId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IntegrationName).HasColumnName("integrationName");
            entity.Property(e => e.IntegrationDeploymentId).HasColumnName("integrationDeploymentId");
            entity.Property(e => e.CommitSHA).HasColumnName("commitSHA");
            entity.Property(e => e.DeploymentId).HasColumnName("deploymentId");
            entity.Property(e => e.Status).HasColumnName("status");
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

            entity.HasOne(d => d.WorkerDeployment).WithMany(p => p.IntegrationDeployments)
                .HasForeignKey(d => d.DeploymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("IntegrationDeployment_deploymentId_fkey");
        });

        modelBuilder.Entity<MetricsDashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MetricsDashboard_pkey");

            entity.ToTable("MetricsDashboard");

            entity.HasIndex(e => e.FriendlyId, "MetricsDashboard_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "MetricsDashboard_projectId_createdAt_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendlyId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("description");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
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
            entity.Property(e => e.Layout).HasColumnName("layout");

            entity.HasOne(d => d.Organization).WithMany(p => p.MetricsDashboards)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MetricsDashboard_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.MetricsDashboards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MetricsDashboard_projectId_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.MetricsDashboards)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("MetricsDashboard_ownerId_fkey");
        });

        modelBuilder.Entity<LlmModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_models_pkey");

            entity.ToTable("llm_models");

            entity.HasIndex(e => e.FriendlyId, "llm_models_friendly_id_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "llm_models_project_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ModelName, e.StartDate }, "llm_models_project_id_model_name_start_date_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId).HasColumnName("friendly_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.MatchPattern).HasColumnName("match_pattern");
            entity.Property(e => e.StartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue("default")
                .HasColumnName("source");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ContextWindow).HasColumnName("context_window");
            entity.Property(e => e.MaxOutputTokens).HasColumnName("max_output_tokens");
            entity.Property(e => e.Capabilities)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("capabilities");
            entity.Property(e => e.IsHidden)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_hidden");
            entity.Property(e => e.BaseModelName).HasColumnName("base_model_name");
            entity.Property(e => e.PricingUnit).HasColumnName("pricing_unit");

            entity.HasOne(d => d.Project).WithMany(p => p.LlmModels)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_models_project_id_fkey");
        });

        modelBuilder.Entity<LlmPricingTier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_pricing_tiers_pkey");

            entity.ToTable("llm_pricing_tiers");

            entity.HasIndex(e => new { e.ModelId, e.Priority }, "llm_pricing_tiers_model_id_priority_key").IsUnique();

            entity.HasIndex(e => new { e.ModelId, e.Name }, "llm_pricing_tiers_model_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("is_default");
            entity.Property(e => e.Priority)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("priority");
            entity.Property(e => e.Conditions)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("conditions");

            entity.HasOne(d => d.Model).WithMany(p => p.PricingTiers)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_pricing_tiers_model_id_fkey");
        });

        modelBuilder.Entity<LlmPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_prices_pkey");

            entity.ToTable("llm_prices");

            entity.HasIndex(e => new { e.ModelId, e.UsageType, e.PricingTierId }, "llm_prices_model_id_usage_type_pricing_tier_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.PricingTierId).HasColumnName("pricing_tier_id");
            entity.Property(e => e.UsageType).HasColumnName("usage_type");
            entity.Property(e => e.Price)
                .HasPrecision(20, 12)
                .HasColumnName("price");

            entity.HasOne(d => d.Model).WithMany(p => p.Prices)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_prices_model_id_fkey");

            entity.HasOne(d => d.PricingTier).WithMany(p => p.Prices)
                .HasForeignKey(d => d.PricingTierId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_prices_pricing_tier_id_fkey");
        });

        modelBuilder.Entity<PlatformNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformNotification_pkey");

            entity.ToTable("PlatformNotification");

            entity.HasIndex(e => e.FriendlyId, "PlatformNotification_friendlyId_key").IsUnique();

            entity.HasIndex(e => new { e.Surface, e.Scope, e.StartsAt }, "PlatformNotification_surface_scope_startsAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FriendlyId)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("friendlyId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.StartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startsAt");
            entity.Property(e => e.EndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endsAt");
            entity.Property(e => e.CliMaxDaysAfterFirstSeen).HasColumnName("cliMaxDaysAfterFirstSeen");
            entity.Property(e => e.CliMaxShowCount).HasColumnName("cliMaxShowCount");
            entity.Property(e => e.CliShowEvery).HasColumnName("cliShowEvery");
            entity.Property(e => e.Priority)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("priority");
            entity.Property(e => e.IsDraft)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDraft");
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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.User).WithMany(p => p.PlatformNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformNotification_userId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.PlatformNotifications)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformNotification_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.PlatformNotifications)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformNotification_projectId_fkey");
        });

        modelBuilder.Entity<PlatformNotificationInteraction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PlatformNotificationInteraction_pkey");

            entity.ToTable("PlatformNotificationInteraction");

            entity.HasIndex(e => new { e.NotificationId, e.UserId }, "PlatformNotificationInteraction_notificationId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.NotificationId).HasColumnName("notificationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FirstSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
            entity.Property(e => e.ShowCount)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("showCount");
            entity.Property(e => e.WebappDismissedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("webappDismissedAt");
            entity.Property(e => e.WebappClickedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("webappClickedAt");
            entity.Property(e => e.CliDismissedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cliDismissedAt");
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

            entity.HasOne(d => d.Notification).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformNotificationInteraction_notificationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PlatformNotificationInteractions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PlatformNotificationInteraction_userId_fkey");
        });

        modelBuilder.Entity<ErrorGroupState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ErrorGroupState_pkey");

            entity.ToTable("ErrorGroupState");

            entity.HasIndex(e => new { e.EnvironmentId, e.Status }, "ErrorGroupState_environmentId_status_idx");

            entity.HasIndex(e => new { e.EnvironmentId, e.TaskIdentifier, e.ErrorFingerprint }, "ErrorGroupState_environmentId_taskIdentifier_errorFingerpri_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EnvironmentId).HasColumnName("environmentId");
            entity.Property(e => e.TaskIdentifier).HasColumnName("taskIdentifier");
            entity.Property(e => e.ErrorFingerprint).HasColumnName("errorFingerprint");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ErrorGroupStatus.Unresolved)
                .HasColumnName("status");
            entity.Property(e => e.IgnoredUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ignoredUntil");
            entity.Property(e => e.IgnoredUntilOccurrenceRate).HasColumnName("ignoredUntilOccurrenceRate");
            entity.Property(e => e.IgnoredUntilTotalOccurrences).HasColumnName("ignoredUntilTotalOccurrences");
            entity.Property(e => e.IgnoredAtOccurrenceCount).HasColumnName("ignoredAtOccurrenceCount");
            entity.Property(e => e.IgnoredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ignoredAt");
            entity.Property(e => e.IgnoredReason).HasColumnName("ignoredReason");
            entity.Property(e => e.IgnoredByUserId).HasColumnName("ignoredByUserId");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");
            entity.Property(e => e.ResolvedInVersion).HasColumnName("resolvedInVersion");
            entity.Property(e => e.ResolvedBy).HasColumnName("resolvedBy");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.ErrorGroupStates)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ErrorGroupState_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ErrorGroupStates)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ErrorGroupState_projectId_fkey");

            entity.HasOne(d => d.Environment).WithMany(p => p.ErrorGroupStates)
                .HasForeignKey(d => d.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ErrorGroupState_environmentId_fkey");
        });

        modelBuilder.Entity<TaskIdentifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskIdentifier_pkey");

            entity.ToTable("TaskIdentifier");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.IsInLatestDeployment }, "TaskIdentifier_runtimeEnvironmentId_isInLatestDeployment_idx");

            entity.HasIndex(e => new { e.RuntimeEnvironmentId, e.Slug }, "TaskIdentifier_runtimeEnvironmentId_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.RuntimeEnvironmentId).HasColumnName("runtimeEnvironmentId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.CurrentTriggerSource)
                .ValueGeneratedNever()
                .HasDefaultValue(TaskTriggerSource.Standard)
                .HasColumnName("currentTriggerSource");
            entity.Property(e => e.CurrentWorkerId).HasColumnName("currentWorkerId");
            entity.Property(e => e.FirstSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstSeenAt");
            entity.Property(e => e.LastSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
            entity.Property(e => e.IsInLatestDeployment)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isInLatestDeployment");

            entity.HasOne(d => d.RuntimeEnvironment).WithMany(p => p.TaskIdentifiers)
                .HasForeignKey(d => d.RuntimeEnvironmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskIdentifier_runtimeEnvironmentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskIdentifiers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskIdentifier_projectId_fkey");

            entity.HasOne(d => d.CurrentWorker).WithMany(p => p.TaskIdentifiers)
                .HasForeignKey(d => d.CurrentWorkerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskIdentifier_currentWorkerId_fkey");
        });

        modelBuilder.Entity<OrganizationDataStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganizationDataStore_pkey");

            entity.ToTable("OrganizationDataStore");

            entity.HasIndex(e => e.Key, "OrganizationDataStore_key_key").IsUnique();

            entity.HasIndex(e => e.Kind, "OrganizationDataStore_kind_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.OrganizationIds).HasColumnName("organizationIds");
            entity.Property(e => e.Kind).HasColumnName("kind");
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
        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MfaBackupCode>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthorizationCode>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PersonalAccessToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationAccessToken>())
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

        foreach (var entry in ChangeTracker.Entries<OrgMember>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrgMemberInvite>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RuntimeEnvironment>())
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

        foreach (var entry in ChangeTracker.Entries<Project>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SecretReference>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SecretStore>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OidcSigningKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataMigration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BackgroundWorker>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Prompt>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BackgroundWorkerTask>())
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

        foreach (var entry in ChangeTracker.Entries<WebhookDelivery>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PlaygroundConversation>())
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

        foreach (var entry in ChangeTracker.Entries<TaskRun>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunTemplate>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunExecutionSnapshot>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunCheckpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Waitpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunWaitpoint>())
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

        foreach (var entry in ChangeTracker.Entries<WorkerInstance>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkerInstanceGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkerGroupToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunDependency>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRunAttempt>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskQueue>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BatchTaskRun>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BatchTaskRunItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EnvironmentVariable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EnvironmentVariableValue>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Checkpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CheckpointRestoreEvent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkerDeployment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskSchedule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskScheduleInstance>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RuntimeEnvironmentSession>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectAlertChannel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectAlert>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectAlertStorage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationProjectIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BulkActionGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BulkActionItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GithubAppInstallation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GithubRepository>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TrustedDeployRepository>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TrustedDeployEnvironment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConnectedGithubRepository>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SupportAccessRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IntegrationDeployment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MetricsDashboard>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LlmModel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PlatformNotification>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PlatformNotificationInteraction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ErrorGroupState>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationDataStore>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class CuidGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

#pragma warning disable VISLIB0001
    public override string Next(EntityEntry entry) => Cuid.NewCuid().ToString();
#pragma warning restore VISLIB0001
}

file sealed class UtcNowGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
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
