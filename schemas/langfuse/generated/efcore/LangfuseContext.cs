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

namespace Langfuse.Models;

public partial class LangfuseContext : DbContext
{
    public LangfuseContext(DbContextOptions<LangfuseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<RoleAssignment> RoleAssignments { get; set; }

    public virtual DbSet<GatewayConfig> GatewayConfigs { get; set; }

    public virtual DbSet<GatewayAiConnection> GatewayAiConnections { get; set; }

    public virtual DbSet<GatewayApiKeyAssociation> GatewayApiKeyAssociations { get; set; }

    public virtual DbSet<InAppAgentConversation> InAppAgentConversations { get; set; }

    public virtual DbSet<InAppAgentEvent> InAppAgentEvents { get; set; }

    public virtual DbSet<InAppAgentRun> InAppAgentRuns { get; set; }

    public virtual DbSet<InAppAgentPendingToolApproval> InAppAgentPendingToolApprovals { get; set; }

    public virtual DbSet<BackgroundMigration> BackgroundMigrations { get; set; }

    public virtual DbSet<LlmApiKeys> LlmApiKeys { get; set; }

    public virtual DbSet<OrganizationMembership> OrganizationMemberships { get; set; }

    public virtual DbSet<ProjectMembership> ProjectMemberships { get; set; }

    public virtual DbSet<MembershipInvitation> MembershipInvitations { get; set; }

    public virtual DbSet<TraceSession> TraceSessions { get; set; }

    public virtual DbSet<ScoreConfig> ScoreConfigs { get; set; }

    public virtual DbSet<AnnotationQueue> AnnotationQueues { get; set; }

    public virtual DbSet<AnnotationQueueItem> AnnotationQueueItems { get; set; }

    public virtual DbSet<AnnotationQueueAssignment> AnnotationQueueAssignments { get; set; }

    public virtual DbSet<CronJobs> CronJobs { get; set; }

    public virtual DbSet<Dataset> Datasets { get; set; }

    public virtual DbSet<DatasetItem> DatasetItems { get; set; }

    public virtual DbSet<DatasetRuns> DatasetRuns { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReaction> CommentReactions { get; set; }

    public virtual DbSet<NotificationPreference> NotificationPreferences { get; set; }

    public virtual DbSet<Prompt> Prompts { get; set; }

    public virtual DbSet<PromptDependency> PromptDependencies { get; set; }

    public virtual DbSet<PromptProtectedLabels> PromptProtectedLabels { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillFile> SkillFiles { get; set; }

    public virtual DbSet<SkillBlob> SkillBlobs { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<PricingTier> PricingTiers { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<AuditLogStaging> AuditLogStagings { get; set; }

    public virtual DbSet<EvalTemplate> EvalTemplates { get; set; }

    public virtual DbSet<Evaluator> Evaluators { get; set; }

    public virtual DbSet<EvaluatorVersion> EvaluatorVersions { get; set; }

    public virtual DbSet<JobConfiguration> JobConfigurations { get; set; }

    public virtual DbSet<EvaluationRule> EvaluationRules { get; set; }

    public virtual DbSet<TracingReadinessSettings> TracingReadinessSettings { get; set; }

    public virtual DbSet<EvaluationRuleEvaluatorAssignment> EvaluationRuleEvaluatorAssignments { get; set; }

    public virtual DbSet<JobExecution> JobExecutions { get; set; }

    public virtual DbSet<DefaultLlmModel> DefaultLlmModels { get; set; }

    public virtual DbSet<TopicConfig> TopicConfigs { get; set; }

    public virtual DbSet<SsoConfig> SsoConfigs { get; set; }

    public virtual DbSet<VerifiedDomain> VerifiedDomains { get; set; }

    public virtual DbSet<PosthogIntegration> PosthogIntegrations { get; set; }

    public virtual DbSet<MixpanelIntegration> MixpanelIntegrations { get; set; }

    public virtual DbSet<BlobStorageIntegration> BlobStorageIntegrations { get; set; }

    public virtual DbSet<ExternalMediaStorageIntegration> ExternalMediaStorageIntegrations { get; set; }

    public virtual DbSet<WebCalloutEndpoint> WebCalloutEndpoints { get; set; }

    public virtual DbSet<BatchExport> BatchExports { get; set; }

    public virtual DbSet<BatchAction> BatchActions { get; set; }

    public virtual DbSet<Media> Media { get; set; }

    public virtual DbSet<TraceMedia> TraceMedia { get; set; }

    public virtual DbSet<ObservationMedia> ObservationMedia { get; set; }

    public virtual DbSet<DatasetItemMedia> DatasetItemMedia { get; set; }

    public virtual DbSet<BillingMeterBackup> BillingMeterBackups { get; set; }

    public virtual DbSet<LlmSchema> LlmSchemas { get; set; }

    public virtual DbSet<LlmTool> LlmTools { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<DashboardWidget> DashboardWidgets { get; set; }

    public virtual DbSet<TableViewPreset> TableViewPresets { get; set; }

    public virtual DbSet<DefaultView> DefaultViews { get; set; }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Trigger> Triggers { get; set; }

    public virtual DbSet<Automation> Automations { get; set; }

    public virtual DbSet<AutomationExecution> AutomationExecutions { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<SlackIntegration> SlackIntegrations { get; set; }

    public virtual DbSet<PendingDeletion> PendingDeletions { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<CloudSpendAlert> CloudSpendAlerts { get; set; }

    public virtual DbSet<TopicClusteringRun> TopicClusteringRuns { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ApiKeyScope>("ApiKeyScope");
        npgsql.MapEnum<SystemRole>("SystemRole");
        npgsql.MapEnum<GatewayIngestionMode>("GatewayIngestionMode");
        npgsql.MapEnum<GatewayProvider>("GatewayProvider");
        npgsql.MapEnum<GatewayConnectionStatus>("GatewayConnectionStatus");
        npgsql.MapEnum<InAppAgentConversationVisibilityScope>("InAppAgentConversationVisibilityScope");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<ScoreConfigDataType>("ScoreConfigDataType");
        npgsql.MapEnum<AnnotationQueueStatus>("AnnotationQueueStatus");
        npgsql.MapEnum<AnnotationQueueObjectType>("AnnotationQueueObjectType");
        npgsql.MapEnum<DatasetStatus>("DatasetStatus");
        npgsql.MapEnum<CommentObjectType>("CommentObjectType");
        npgsql.MapEnum<NotificationChannel>("NotificationChannel");
        npgsql.MapEnum<NotificationType>("NotificationType");
        npgsql.MapEnum<AuditLogRecordType>("AuditLogRecordType");
        npgsql.MapEnum<EvalTemplateType>("EvalTemplateType");
        npgsql.MapEnum<EvalTemplateSourceCodeLanguage>("EvalTemplateSourceCodeLanguage");
        npgsql.MapEnum<EvaluatorSourceCodeLanguage>("EvaluatorSourceCodeLanguage");
        npgsql.MapEnum<JobType>("JobType");
        npgsql.MapEnum<JobConfigState>("JobConfigState");
        npgsql.MapEnum<EvaluatorBlockReason>("EvaluatorBlockReason");
        npgsql.MapEnum<JobExecutionStatus>("JobExecutionStatus");
        npgsql.MapEnum<BlobStorageIntegrationFileType>("BlobStorageIntegrationFileType");
        npgsql.MapEnum<BlobStorageIntegrationType>("BlobStorageIntegrationType");
        npgsql.MapEnum<ExternalMediaStorageIntegrationType>("ExternalMediaStorageIntegrationType");
        npgsql.MapEnum<BlobStorageExportMode>("BlobStorageExportMode");
        npgsql.MapEnum<AnalyticsIntegrationExportSource>("AnalyticsIntegrationExportSource");
        npgsql.MapEnum<MediaAssociationOrigin>("MediaAssociationOrigin");
        npgsql.MapEnum<DashboardWidgetViews>("DashboardWidgetViews");
        npgsql.MapEnum<DashboardWidgetChartType>("DashboardWidgetChartType");
        npgsql.MapEnum<ActionType>("ActionType");
        npgsql.MapEnum<ActionExecutionStatus>("ActionExecutionStatus");
        npgsql.MapEnum<MonitorThresholdOperator>("MonitorThresholdOperator");
        npgsql.MapEnum<MonitorView>("MonitorView");
        npgsql.MapEnum<MonitorSeverity>("MonitorSeverity");
        npgsql.MapEnum<MonitorStatus>("MonitorStatus");
        npgsql.MapEnum<SurveyName>("SurveyName");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ApiKeyScope>("ApiKeyScope");
        dataSource.MapEnum<SystemRole>("SystemRole");
        dataSource.MapEnum<GatewayIngestionMode>("GatewayIngestionMode");
        dataSource.MapEnum<GatewayProvider>("GatewayProvider");
        dataSource.MapEnum<GatewayConnectionStatus>("GatewayConnectionStatus");
        dataSource.MapEnum<InAppAgentConversationVisibilityScope>("InAppAgentConversationVisibilityScope");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<ScoreConfigDataType>("ScoreConfigDataType");
        dataSource.MapEnum<AnnotationQueueStatus>("AnnotationQueueStatus");
        dataSource.MapEnum<AnnotationQueueObjectType>("AnnotationQueueObjectType");
        dataSource.MapEnum<DatasetStatus>("DatasetStatus");
        dataSource.MapEnum<CommentObjectType>("CommentObjectType");
        dataSource.MapEnum<NotificationChannel>("NotificationChannel");
        dataSource.MapEnum<NotificationType>("NotificationType");
        dataSource.MapEnum<AuditLogRecordType>("AuditLogRecordType");
        dataSource.MapEnum<EvalTemplateType>("EvalTemplateType");
        dataSource.MapEnum<EvalTemplateSourceCodeLanguage>("EvalTemplateSourceCodeLanguage");
        dataSource.MapEnum<EvaluatorSourceCodeLanguage>("EvaluatorSourceCodeLanguage");
        dataSource.MapEnum<JobType>("JobType");
        dataSource.MapEnum<JobConfigState>("JobConfigState");
        dataSource.MapEnum<EvaluatorBlockReason>("EvaluatorBlockReason");
        dataSource.MapEnum<JobExecutionStatus>("JobExecutionStatus");
        dataSource.MapEnum<BlobStorageIntegrationFileType>("BlobStorageIntegrationFileType");
        dataSource.MapEnum<BlobStorageIntegrationType>("BlobStorageIntegrationType");
        dataSource.MapEnum<ExternalMediaStorageIntegrationType>("ExternalMediaStorageIntegrationType");
        dataSource.MapEnum<BlobStorageExportMode>("BlobStorageExportMode");
        dataSource.MapEnum<AnalyticsIntegrationExportSource>("AnalyticsIntegrationExportSource");
        dataSource.MapEnum<MediaAssociationOrigin>("MediaAssociationOrigin");
        dataSource.MapEnum<DashboardWidgetViews>("DashboardWidgetViews");
        dataSource.MapEnum<DashboardWidgetChartType>("DashboardWidgetChartType");
        dataSource.MapEnum<ActionType>("ActionType");
        dataSource.MapEnum<ActionExecutionStatus>("ActionExecutionStatus");
        dataSource.MapEnum<MonitorThresholdOperator>("MonitorThresholdOperator");
        dataSource.MapEnum<MonitorView>("MonitorView");
        dataSource.MapEnum<MonitorSeverity>("MonitorSeverity");
        dataSource.MapEnum<MonitorStatus>("MonitorStatus");
        dataSource.MapEnum<SurveyName>("SurveyName");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ApiKeyScope>(name: "ApiKeyScope");
        modelBuilder.HasPostgresEnum<SystemRole>(name: "SystemRole");
        modelBuilder.HasPostgresEnum<GatewayIngestionMode>(name: "GatewayIngestionMode");
        modelBuilder.HasPostgresEnum<GatewayProvider>(name: "GatewayProvider");
        modelBuilder.HasPostgresEnum<GatewayConnectionStatus>(name: "GatewayConnectionStatus");
        modelBuilder.HasPostgresEnum<InAppAgentConversationVisibilityScope>(name: "InAppAgentConversationVisibilityScope");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<ScoreConfigDataType>(name: "ScoreConfigDataType");
        modelBuilder.HasPostgresEnum<AnnotationQueueStatus>(name: "AnnotationQueueStatus");
        modelBuilder.HasPostgresEnum<AnnotationQueueObjectType>(name: "AnnotationQueueObjectType");
        modelBuilder.HasPostgresEnum<DatasetStatus>(name: "DatasetStatus");
        modelBuilder.HasPostgresEnum<CommentObjectType>(name: "CommentObjectType");
        modelBuilder.HasPostgresEnum<NotificationChannel>(name: "NotificationChannel");
        modelBuilder.HasPostgresEnum<NotificationType>(name: "NotificationType");
        modelBuilder.HasPostgresEnum<AuditLogRecordType>(name: "AuditLogRecordType");
        modelBuilder.HasPostgresEnum<EvalTemplateType>(name: "EvalTemplateType");
        modelBuilder.HasPostgresEnum<EvalTemplateSourceCodeLanguage>(name: "EvalTemplateSourceCodeLanguage");
        modelBuilder.HasPostgresEnum<EvaluatorSourceCodeLanguage>(name: "EvaluatorSourceCodeLanguage");
        modelBuilder.HasPostgresEnum<JobType>(name: "JobType");
        modelBuilder.HasPostgresEnum<JobConfigState>(name: "JobConfigState");
        modelBuilder.HasPostgresEnum<EvaluatorBlockReason>(name: "EvaluatorBlockReason");
        modelBuilder.HasPostgresEnum<JobExecutionStatus>(name: "JobExecutionStatus");
        modelBuilder.HasPostgresEnum<BlobStorageIntegrationFileType>(name: "BlobStorageIntegrationFileType");
        modelBuilder.HasPostgresEnum<BlobStorageIntegrationType>(name: "BlobStorageIntegrationType");
        modelBuilder.HasPostgresEnum<ExternalMediaStorageIntegrationType>(name: "ExternalMediaStorageIntegrationType");
        modelBuilder.HasPostgresEnum<BlobStorageExportMode>(name: "BlobStorageExportMode");
        modelBuilder.HasPostgresEnum<AnalyticsIntegrationExportSource>(name: "AnalyticsIntegrationExportSource");
        modelBuilder.HasPostgresEnum<MediaAssociationOrigin>(name: "MediaAssociationOrigin");
        modelBuilder.HasPostgresEnum<DashboardWidgetViews>(name: "DashboardWidgetViews");
        modelBuilder.HasPostgresEnum<DashboardWidgetChartType>(name: "DashboardWidgetChartType");
        modelBuilder.HasPostgresEnum<ActionType>(name: "ActionType");
        modelBuilder.HasPostgresEnum<ActionExecutionStatus>(name: "ActionExecutionStatus");
        modelBuilder.HasPostgresEnum<MonitorThresholdOperator>(name: "MonitorThresholdOperator");
        modelBuilder.HasPostgresEnum<MonitorView>(name: "MonitorView");
        modelBuilder.HasPostgresEnum<MonitorSeverity>(name: "MonitorSeverity");
        modelBuilder.HasPostgresEnum<MonitorStatus>(name: "MonitorStatus");
        modelBuilder.HasPostgresEnum<SurveyName>(name: "SurveyName");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_user_id_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.ExpiresIn).HasColumnName("expires_in");
            entity.Property(e => e.ExtExpiresIn).HasColumnName("ext_expires_in");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");
            entity.Property(e => e.RefreshTokenExpiresIn).HasColumnName("refresh_token_expires_in");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_user_id_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Session_pkey");

            entity.ToTable("Session");

            entity.HasIndex(e => e.SessionToken, "Session_session_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("session_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("email_verified");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.SessionsExpiredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sessions_expired_at");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Admin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("admin");
            entity.Property(e => e.V4BetaEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("v4_beta_enabled");
            entity.Property(e => e.SupportAccess)
                .HasColumnType("jsonb")
                .HasColumnName("support_access");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.FeatureFlags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("feature_flags");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("verification_tokens_token_key");

            entity.ToTable("verification_tokens");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "verification_tokens_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organizations_pkey");

            entity.ToTable("organizations");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CloudConfig)
                .HasColumnType("jsonb")
                .HasColumnName("cloud_config");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CloudBillingCycleAnchor)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cloud_billing_cycle_anchor");
            entity.Property(e => e.CloudBillingCycleUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cloud_billing_cycle_updated_at");
            entity.Property(e => e.CloudCurrentCycleUsage).HasColumnName("cloud_current_cycle_usage");
            entity.Property(e => e.CloudFreeTierUsageThresholdState).HasColumnName("cloud_free_tier_usage_threshold_state");
            entity.Property(e => e.AiFeaturesEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("ai_features_enabled");
            entity.Property(e => e.AiTelemetryEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("ai_telemetry_enabled");
            entity.Property(e => e.FeatureFlagOrgDefaults)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("feature_flag_org_defaults");
            entity.Property(e => e.SfdcOrgId).HasColumnName("sfdc_org_id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.HasIndex(e => e.OrgId, "projects_org_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.RetentionDays).HasColumnName("retention_days");
            entity.Property(e => e.HasTraces)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("has_traces");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.HomeDashboardId).HasColumnName("home_dashboard_id");

            entity.HasOne(d => d.HomeDashboard).WithMany(p => p.HomeDashboardProjects)
                .HasForeignKey(d => d.HomeDashboardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("projects_home_dashboard_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Projects)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projects_org_id_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("api_keys_pkey");

            entity.ToTable("api_keys");

            entity.HasIndex(e => e.Id, "api_keys_id_key").IsUnique();

            entity.HasIndex(e => e.PublicKey, "api_keys_public_key_key").IsUnique();

            entity.HasIndex(e => e.HashedSecretKey, "api_keys_hashed_secret_key_key").IsUnique();

            entity.HasIndex(e => e.FastHashedSecretKey, "api_keys_fast_hashed_secret_key_key").IsUnique();

            entity.HasIndex(e => e.OrgId, "api_keys_organization_id_idx");

            entity.HasIndex(e => e.ProjectId, "api_keys_project_id_idx");

            entity.HasIndex(e => e.CreatedByApiKeyId, "api_keys_created_by_api_key_id_idx");

            entity.HasIndex(e => new { e.UserId, e.OrgId }, "api_keys_user_id_organization_id_idx");

            entity.HasIndex(e => e.PublicKey, "api_keys_public_key_idx");

            entity.HasIndex(e => e.HashedSecretKey, "api_keys_hashed_secret_key_idx");

            entity.HasIndex(e => e.FastHashedSecretKey, "api_keys_fast_hashed_secret_key_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PublicKey).HasColumnName("public_key");
            entity.Property(e => e.HashedSecretKey).HasColumnName("hashed_secret_key");
            entity.Property(e => e.FastHashedSecretKey).HasColumnName("fast_hashed_secret_key");
            entity.Property(e => e.DisplaySecretKey).HasColumnName("display_secret_key");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.IsInAppAgentKey)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_in_app_agent_key");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.OrgId).HasColumnName("organization_id");
            entity.Property(e => e.Scope)
                .ValueGeneratedNever()
                .HasDefaultValue(ApiKeyScope.Project)
                .HasColumnName("scope");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedByApiKeyId).HasColumnName("created_by_api_key_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_keys_project_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ApiKey)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_keys_organization_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ApiKeysCreated)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("api_keys_created_by_user_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_keys_user_id_fkey");

            entity.HasOne(d => d.CreatedByApiKey).WithMany(p => p.ApiKeysCreated)
                .HasForeignKey(d => d.CreatedByApiKeyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("api_keys_created_by_api_key_id_fkey");
        });

        modelBuilder.Entity<RoleAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_assignments_pkey");

            entity.ToTable("role_assignments");

            entity.HasIndex(e => e.deprecatedOwnerOrgId, "role_assignments_owner_org_id_idx");

            entity.HasIndex(e => e.deprecatedOwnerProjectId, "role_assignments_owner_project_id_idx");

            entity.HasIndex(e => e.OrgId, "role_assignments_org_id_idx");

            entity.HasIndex(e => e.ProjectId, "role_assignments_project_id_idx");

            entity.HasIndex(e => e.PrincipalId, "role_assignments_principal_id_idx");

            entity.HasIndex(e => e.UserId, "role_assignments_user_id_idx");

            entity.HasIndex(e => e.ApiKeyId, "role_assignments_api_key_id_idx");

            entity.HasIndex(e => new { e.deprecatedPrincipalUserId, e.deprecatedOwnerOrgId, e.SystemRole }, "role_assignments_user_org_role_key").IsUnique();

            entity.HasIndex(e => new { e.deprecatedPrincipalUserId, e.deprecatedOwnerProjectId, e.SystemRole }, "role_assignments_user_project_role_key").IsUnique();

            entity.HasIndex(e => new { e.deprecatedPrincipalApiKeyId, e.deprecatedOwnerOrgId, e.SystemRole }, "role_assignments_api_key_org_role_key").IsUnique();

            entity.HasIndex(e => new { e.deprecatedPrincipalApiKeyId, e.deprecatedOwnerProjectId, e.SystemRole }, "role_assignments_api_key_project_role_key").IsUnique();

            entity.HasIndex(e => new { e.OwnerId, e.PrincipalId, e.RoleId }, "role_assignments_owner_id_principal_id_role_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.PrincipalId).HasColumnName("principal_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ApiKeyId).HasColumnName("api_key_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SystemRole).HasColumnName("system_role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.RoleAssignments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("role_assignments_project_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.RoleAssignments)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("role_assignments_org_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.RoleAssignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("role_assignments_user_id_fkey");

            entity.HasOne(d => d.ApiKey).WithMany(p => p.RoleAssignments)
                .HasForeignKey(d => d.ApiKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("role_assignments_api_key_id_fkey");
        });

        modelBuilder.Entity<GatewayConfig>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("gateway_configs_pkey");

            entity.ToTable("gateway_configs");

            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.DefaultIngestionProjectId).HasColumnName("default_ingestion_project_id");
            entity.Property(e => e.IngestionMode)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayIngestionMode.Usage)
                .HasColumnName("ingestion_mode");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithOne(p => p.GatewayConfig)
                .HasForeignKey<GatewayConfig>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("gateway_configs_organization_id_fkey");

            entity.HasOne(d => d.DefaultIngestionProject).WithMany(p => p.GatewayConfigs)
                .HasForeignKey(d => d.DefaultIngestionProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("gateway_configs_default_ingestion_project_id_fkey");
        });

        modelBuilder.Entity<GatewayAiConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gateway_ai_connections_pkey");

            entity.ToTable("gateway_ai_connections");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt, e.Id }, "gateway_ai_conn_org_created_at_id_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Status, e.CreatedAt, e.Id }, "gateway_ai_conn_org_status_created_at_id_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "gateway_ai_connections_organization_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.EncryptedCredential).HasColumnName("encrypted_credentials");
            entity.Property(e => e.DisplaySecret).HasColumnName("display_secret_key");
            entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(GatewayConnectionStatus.Enabled)
                .HasColumnName("status");
            entity.Property(e => e.AllowedModels)
                .HasColumnType("jsonb")
                .HasColumnName("allowed_models");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithMany(p => p.GatewayAiConnections)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("gateway_ai_connections_organization_id_fkey");
        });

        modelBuilder.Entity<GatewayApiKeyAssociation>(entity =>
        {
            entity.HasKey(e => e.ApiKeyId).HasName("gateway_api_key_associations_pkey");

            entity.ToTable("gateway_api_key_associations");

            entity.Property(e => e.ApiKeyId).HasColumnName("api_key_id");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");

            entity.HasOne(d => d.ApiKey).WithOne(p => p.GatewayAssociation)
                .HasForeignKey<GatewayApiKeyAssociation>(d => d.ApiKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("gateway_api_key_associations_api_key_id_fkey");
        });

        modelBuilder.Entity<InAppAgentConversation>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId }).HasName("in_app_agent_conversations_pkey");

            entity.ToTable("in_app_agent_conversations");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedByUserId, e.DeletedAt, e.UpdatedAt, e.Id }, "in_app_agent_conversations_project_user_list_idx");

            entity.HasIndex(e => new { e.ProjectId, e.UpdatedAt }, "in_app_agent_conversations_project_updated_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.RenamedByUserAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("renamed_by_user_at");
            entity.Property(e => e.VisibilityScope)
                .ValueGeneratedNever()
                .HasDefaultValue(InAppAgentConversationVisibilityScope.Personal)
                .HasColumnName("visibility_scope");
            entity.Property(e => e.ProviderSessionId).HasColumnName("provider_session_id");
            entity.Property(e => e.AlwaysAllowedTools)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("always_allowed_tools");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.InAppAgentConversation)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_conversations_project_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.InAppAgentConversations)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("in_app_agent_conversations_created_by_user_id_fkey");
        });

        modelBuilder.Entity<InAppAgentEvent>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.ConversationId, e.SequenceNumber }).HasName("in_app_agent_events_pkey");

            entity.ToTable("in_app_agent_events");

            entity.HasIndex(e => new { e.ProjectId, e.RunId }, "in_app_agent_events_project_run_idx");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequence_number");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Event)
                .HasColumnType("jsonb")
                .HasColumnName("event");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Project).WithMany(p => p.InAppAgentEvent)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_events_project_id_fkey");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Events)
                .HasForeignKey(d => new { d.ConversationId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_events_conversation_id_project_id_fkey");

            entity.HasOne(d => d.Run).WithMany(p => p.Events)
                .HasForeignKey(d => new { d.RunId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_events_run_id_project_id_fkey");
        });

        modelBuilder.Entity<InAppAgentRun>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId }).HasName("in_app_agent_runs_pkey");

            entity.ToTable("in_app_agent_runs");

            entity.HasIndex(e => new { e.ProjectId, e.ConversationId, e.CreatedAt }, "in_app_agent_runs_project_conversation_created_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.TriggeredByUserId).HasColumnName("triggered_by_user_id");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.McpApiKeyId).HasColumnName("mcp_api_key_id");
            entity.Property(e => e.ErrorCode).HasColumnName("error_code");
            entity.Property(e => e.ErrorMessage).HasColumnName("error_message");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimed_at");
            entity.Property(e => e.HeartbeatAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("heartbeat_at");
            entity.Property(e => e.CancelRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cancel_requested_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.InAppAgentRun)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_runs_project_id_fkey");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Runs)
                .HasForeignKey(d => new { d.ConversationId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_runs_conversation_id_project_id_fkey");

            entity.HasOne(d => d.TriggeredByUser).WithMany(p => p.InAppAgentRuns)
                .HasForeignKey(d => d.TriggeredByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("in_app_agent_runs_triggered_by_user_id_fkey");
        });

        modelBuilder.Entity<InAppAgentPendingToolApproval>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.ConversationId, e.ToolCallId }).HasName("in_app_agent_pending_tool_approvals_pkey");

            entity.ToTable("in_app_agent_pending_tool_approvals");

            entity.HasIndex(e => e.ExpiresAt, "in_app_agent_pending_tool_approvals_expires_at_idx");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.ToolCallId).HasColumnName("tool_call_id");
            entity.Property(e => e.ApprovalFingerprint).HasColumnName("approval_fingerprint");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.InAppAgentPendingToolApproval)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_pending_tool_approvals_project_id_fkey");

            entity.HasOne(d => d.Conversation).WithMany(p => p.PendingToolApprovals)
                .HasForeignKey(d => new { d.ConversationId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("in_app_agent_pending_tool_approvals_conversation_id_projec_fkey");
        });

        modelBuilder.Entity<BackgroundMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("background_migrations_pkey");

            entity.ToTable("background_migrations");

            entity.HasIndex(e => e.Name, "background_migrations_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Script).HasColumnName("script");
            entity.Property(e => e.Args)
                .HasColumnType("jsonb")
                .HasColumnName("args");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("state");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.FailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failed_at");
            entity.Property(e => e.FailedReason).HasColumnName("failed_reason");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("locked_at");
        });

        modelBuilder.Entity<LlmApiKeys>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_api_keys_pkey");

            entity.ToTable("llm_api_keys");

            entity.HasIndex(e => e.Id, "llm_api_keys_id_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.Provider }, "llm_api_keys_project_id_provider_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.Provider }, "llm_api_keys_organization_id_provider_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Adapter).HasColumnName("adapter");
            entity.Property(e => e.DisplaySecretKey).HasColumnName("display_secret_key");
            entity.Property(e => e.SecretKey).HasColumnName("secret_key");
            entity.Property(e => e.BaseURL).HasColumnName("base_url");
            entity.Property(e => e.CustomModels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("custom_models");
            entity.Property(e => e.WithDefaultModels)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("with_default_models");
            entity.Property(e => e.ExtraHeaders).HasColumnName("extra_headers");
            entity.Property(e => e.ExtraHeaderKeys)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("extra_header_keys");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

            entity.HasOne(d => d.Project).WithMany(p => p.LlmApiKeys)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_api_keys_project_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationLlmApiKeys)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_api_keys_organization_id_fkey");
        });

        modelBuilder.Entity<OrganizationMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_memberships_pkey");

            entity.ToTable("organization_memberships");

            entity.HasIndex(e => e.UserId, "organization_memberships_user_id_idx");

            entity.HasIndex(e => new { e.OrgId, e.UserId }, "organization_memberships_org_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationMemberships)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("organization_memberships_org_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("organization_memberships_user_id_fkey");
        });

        modelBuilder.Entity<ProjectMembership>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.UserId }).HasName("project_memberships_pkey");

            entity.ToTable("project_memberships");

            entity.HasIndex(e => e.UserId, "project_memberships_user_id_idx");

            entity.HasIndex(e => e.ProjectId, "project_memberships_project_id_idx");

            entity.HasIndex(e => e.OrgMembershipId, "project_memberships_org_membership_id_idx");

            entity.Property(e => e.OrgMembershipId).HasColumnName("org_membership_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.OrganizationMembership).WithMany(p => p.ProjectMemberships)
                .HasForeignKey(d => d.OrgMembershipId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("project_memberships_org_membership_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("project_memberships_project_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("project_memberships_user_id_fkey");
        });

        modelBuilder.Entity<MembershipInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("membership_invitations_pkey");

            entity.ToTable("membership_invitations");

            entity.HasIndex(e => e.Id, "membership_invitations_id_key").IsUnique();

            entity.HasIndex(e => e.ProjectId, "membership_invitations_project_id_idx");

            entity.HasIndex(e => e.OrgId, "membership_invitations_org_id_idx");

            entity.HasIndex(e => e.Email, "membership_invitations_email_idx");

            entity.HasIndex(e => new { e.Email, e.OrgId }, "membership_invitations_email_org_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.OrgRole).HasColumnName("org_role");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectRole).HasColumnName("project_role");
            entity.Property(e => e.InvitedByUserId).HasColumnName("invited_by_user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithMany(p => p.MembershipInvitation)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("membership_invitations_org_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("membership_invitations_project_id_fkey");

            entity.HasOne(d => d.InvitedByUser).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.InvitedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("membership_invitations_invited_by_user_id_fkey");
        });

        modelBuilder.Entity<TraceSession>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId }).HasName("trace_sessions_pkey");

            entity.ToTable("trace_sessions");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "trace_sessions_project_id_created_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Bookmarked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("bookmarked");
            entity.Property(e => e.Public)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("public");
            entity.Property(e => e.Environment)
                .ValueGeneratedNever()
                .HasDefaultValue("default")
                .HasColumnName("environment");

            entity.HasOne(d => d.Project).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("trace_sessions_project_id_fkey");
        });

        modelBuilder.Entity<ScoreConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("score_configs_pkey");

            entity.ToTable("score_configs");

            entity.HasIndex(e => e.DataType, "score_configs_data_type_idx");

            entity.HasIndex(e => e.IsArchived, "score_configs_is_archived_idx");

            entity.HasIndex(e => e.ProjectId, "score_configs_project_id_idx");

            entity.HasIndex(e => e.Categories, "score_configs_categories_idx");

            entity.HasIndex(e => e.CreatedAt, "score_configs_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "score_configs_updated_at_idx");

            entity.HasIndex(e => new { e.Id, e.ProjectId }, "score_configs_id_project_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_archived");
            entity.Property(e => e.MinValue).HasColumnName("min_value");
            entity.Property(e => e.MaxValue).HasColumnName("max_value");
            entity.Property(e => e.Categories)
                .HasColumnType("jsonb")
                .HasColumnName("categories");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.Project).WithMany(p => p.ScoreConfig)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("score_configs_project_id_fkey");
        });

        modelBuilder.Entity<AnnotationQueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("annotation_queues_pkey");

            entity.ToTable("annotation_queues");

            entity.HasIndex(e => new { e.Id, e.ProjectId }, "annotation_queues_id_project_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "annotation_queues_project_id_created_at_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "annotation_queues_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ScoreConfigIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("score_config_ids");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationQueue)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queues_project_id_fkey");
        });

        modelBuilder.Entity<AnnotationQueueItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("annotation_queue_items_pkey");

            entity.ToTable("annotation_queue_items");

            entity.HasIndex(e => new { e.Id, e.ProjectId }, "annotation_queue_items_id_project_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.QueueId, e.Status }, "annotation_queue_items_project_id_queue_id_status_idx");

            entity.HasIndex(e => new { e.ObjectId, e.ObjectType, e.ProjectId, e.QueueId }, "annotation_queue_items_object_id_object_type_project_id_que_idx");

            entity.HasIndex(e => e.AnnotatorUserId, "annotation_queue_items_annotator_user_id_idx");

            entity.HasIndex(e => e.CreatedAt, "annotation_queue_items_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.QueueId).HasColumnName("queue_id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.ObjectType).HasColumnName("object_type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(AnnotationQueueStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("locked_at");
            entity.Property(e => e.LockedByUserId).HasColumnName("locked_by_user_id");
            entity.Property(e => e.AnnotatorUserId).HasColumnName("annotator_user_id");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Queue).WithMany(p => p.AnnotationQueueItem)
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queue_items_queue_id_fkey");

            entity.HasOne(d => d.LockedByUser).WithMany(p => p.AnnotatedLockedItem)
                .HasForeignKey(d => d.LockedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("annotation_queue_items_locked_by_user_id_fkey");

            entity.HasOne(d => d.AnnotatorUser).WithMany(p => p.AnnotatedCompletedItem)
                .HasForeignKey(d => d.AnnotatorUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("annotation_queue_items_annotator_user_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationQueueItem)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queue_items_project_id_fkey");
        });

        modelBuilder.Entity<AnnotationQueueAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("annotation_queue_assignments_pkey");

            entity.ToTable("annotation_queue_assignments");

            entity.HasIndex(e => new { e.ProjectId, e.QueueId, e.UserId }, "annotation_queue_assignments_project_id_queue_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.QueueId).HasColumnName("queue_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.AnnotationQueueAssignment)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queue_assignments_project_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AnnotationQueueAssignment)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queue_assignments_user_id_fkey");

            entity.HasOne(d => d.Queue).WithMany(p => p.AnnotationQueueAssignment)
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("annotation_queue_assignments_queue_id_fkey");
        });

        modelBuilder.Entity<CronJobs>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("cron_jobs_pkey");

            entity.ToTable("cron_jobs");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LastRun)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_run");
            entity.Property(e => e.JobStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("job_started_at");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId }).HasName("datasets_pkey");

            entity.ToTable("datasets");

            entity.HasIndex(e => e.CreatedAt, "datasets_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "datasets_updated_at_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "datasets_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.RemoteExperimentUrl).HasColumnName("remote_experiment_url");
            entity.Property(e => e.RemoteExperimentPayload)
                .HasColumnType("jsonb")
                .HasColumnName("remote_experiment_payload");
            entity.Property(e => e.RemoteExperimentEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("remote_experiment_enabled");
            entity.Property(e => e.RemoteExperimentSecretKey).HasColumnName("remote_experiment_secret_key");
            entity.Property(e => e.RemoteExperimentDisplaySecretKey).HasColumnName("remote_experiment_display_secret_key");
            entity.Property(e => e.RemoteExperimentRequestHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("remote_experiment_request_headers");
            entity.Property(e => e.RemoteExperimentDisplayHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("remote_experiment_display_headers");
            entity.Property(e => e.InputSchema)
                .HasColumnType("json")
                .HasColumnName("input_schema");
            entity.Property(e => e.ExpectedOutputSchema)
                .HasColumnType("json")
                .HasColumnName("expected_output_schema");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.Dataset)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("datasets_project_id_fkey");
        });

        modelBuilder.Entity<DatasetItem>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId, e.ValidFrom }).HasName("dataset_items_pkey");

            entity.ToTable("dataset_items");

            entity.HasIndex(e => new { e.ProjectId, e.ValidTo }, "dataset_items_project_id_valid_to_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Id, e.ValidFrom }, "dataset_items_project_id_id_valid_from_idx");

            entity.HasIndex(e => e.SourceTraceId, "dataset_items_source_trace_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.SourceObservationId, "dataset_items_source_observation_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.DatasetId, "dataset_items_dataset_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.CreatedAt, "dataset_items_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "dataset_items_updated_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(DatasetStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.ExpectedOutput)
                .HasColumnType("jsonb")
                .HasColumnName("expected_output");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.SourceTraceId).HasColumnName("source_trace_id");
            entity.Property(e => e.SourceObservationId).HasColumnName("source_observation_id");
            entity.Property(e => e.DatasetId).HasColumnName("dataset_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ValidFrom)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidTo)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("valid_to");
            entity.Property(e => e.IsDeleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.HasOne(d => d.Dataset).WithMany(p => p.DatasetItems)
                .HasForeignKey(d => new { d.DatasetId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dataset_items_dataset_id_project_id_fkey");
        });

        modelBuilder.Entity<DatasetRuns>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProjectId }).HasName("dataset_runs_pkey");

            entity.ToTable("dataset_runs");

            entity.HasIndex(e => e.DatasetId, "dataset_runs_dataset_id_idx").HasMethod("hash");

            entity.HasIndex(e => e.CreatedAt, "dataset_runs_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "dataset_runs_updated_at_idx");

            entity.HasIndex(e => new { e.DatasetId, e.ProjectId, e.Name }, "dataset_runs_dataset_id_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.DatasetId).HasColumnName("dataset_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Dataset).WithMany(p => p.DatasetRuns)
                .HasForeignKey(d => new { d.DatasetId, d.ProjectId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dataset_runs_dataset_id_project_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.HasIndex(e => new { e.ProjectId, e.ObjectType, e.ObjectId }, "comments_project_id_object_type_object_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ObjectType).HasColumnName("object_type");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.AuthorUserId).HasColumnName("author_user_id");
            entity.Property(e => e.DataField).HasColumnName("data_field");
            entity.Property(e => e.Path)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("path");
            entity.Property(e => e.RangeStart)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("range_start");
            entity.Property(e => e.RangeEnd)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("range_end");

            entity.HasOne(d => d.Project).WithMany(p => p.Comment)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_project_id_fkey");
        });

        modelBuilder.Entity<CommentReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_reactions_pkey");

            entity.ToTable("comment_reactions");

            entity.HasIndex(e => new { e.CommentId, e.UserId, e.Emoji }, "comment_reactions_comment_id_user_id_emoji_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Emoji).HasColumnName("emoji");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Project).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comment_reactions_project_id_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comment_reactions_comment_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comment_reactions_user_id_fkey");
        });

        modelBuilder.Entity<NotificationPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_preferences_pkey");

            entity.ToTable("notification_preferences");

            entity.HasIndex(e => new { e.UserId, e.ProjectId, e.Channel, e.Type }, "notification_preferences_user_id_project_id_channel_type_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Channel).HasColumnName("channel");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notification_preferences_user_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.NotificationPreference)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notification_preferences_project_id_fkey");
        });

        modelBuilder.Entity<Prompt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompts_pkey");

            entity.ToTable("prompts");

            entity.HasIndex(e => new { e.ProjectId, e.Id }, "prompts_project_id_id_idx");

            entity.HasIndex(e => e.CreatedAt, "prompts_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "prompts_updated_at_idx");

            entity.HasIndex(e => e.Tags, "prompts_tags_idx")
                .HasMethod("gin")
                .HasOperators("array_ops");

            entity.HasIndex(e => new { e.ProjectId, e.Name, e.Version }, "prompts_project_id_name_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Prompt1)
                .HasColumnType("jsonb")
                .HasColumnName("prompt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("text")
                .HasColumnName("type");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("json")
                .HasColumnName("config");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.Labels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("labels");
            entity.Property(e => e.CommitMessage).HasColumnName("commit_message");

            entity.HasOne(d => d.Project).WithMany(p => p.Prompt)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompts_project_id_fkey");
        });

        modelBuilder.Entity<PromptDependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompt_dependencies_pkey");

            entity.ToTable("prompt_dependencies");

            entity.HasIndex(e => new { e.ProjectId, e.ParentId }, "prompt_dependencies_project_id_parent_id");

            entity.HasIndex(e => new { e.ProjectId, e.ChildName }, "prompt_dependencies_project_id_child_name");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.ChildName).HasColumnName("child_name");
            entity.Property(e => e.ChildLabel).HasColumnName("child_label");
            entity.Property(e => e.ChildVersion).HasColumnName("child_version");

            entity.HasOne(d => d.Project).WithMany(p => p.PromptDependency)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompt_dependencies_project_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.PromptDependency)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompt_dependencies_parent_id_fkey");
        });

        modelBuilder.Entity<PromptProtectedLabels>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prompt_protected_labels_pkey");

            entity.ToTable("prompt_protected_labels");

            entity.HasIndex(e => new { e.ProjectId, e.Label }, "prompt_protected_labels_project_id_label_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Label).HasColumnName("label");

            entity.HasOne(d => d.Project).WithMany(p => p.PromptProtectedLabels)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prompt_protected_labels_project_id_fkey");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("skills_pkey");

            entity.ToTable("skills");

            entity.HasIndex(e => e.CreatedAt, "skills_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "skills_updated_at_idx");

            entity.HasIndex(e => e.Tags, "skills_tags_idx")
                .HasMethod("gin")
                .HasOperators("array_ops");

            entity.HasIndex(e => e.Labels, "skills_labels_idx")
                .HasMethod("gin")
                .HasOperators("array_ops");

            entity.HasIndex(e => new { e.ProjectId, e.Name, e.Version }, "skills_project_id_name_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Frontmatter)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("frontmatter");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.Labels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("labels");
            entity.Property(e => e.CommitMessage).HasColumnName("commit_message");

            entity.HasOne(d => d.Project).WithMany(p => p.Skill)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("skills_project_id_fkey");
        });

        modelBuilder.Entity<SkillFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("skill_files_pkey");

            entity.ToTable("skill_files");

            entity.HasIndex(e => new { e.ProjectId, e.BlobId }, "skill_files_project_id_blob_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.SkillId, e.Path }, "skill_files_project_id_skill_id_path_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.BlobId).HasColumnName("blob_id");
            entity.Property(e => e.Path).HasColumnName("path");

            entity.HasOne(d => d.Project).WithMany(p => p.SkillFile)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("skill_files_project_id_fkey");

            entity.HasOne(d => d.Skill).WithMany(p => p.Files)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("skill_files_skill_id_fkey");

            entity.HasOne(d => d.Blob).WithMany(p => p.Files)
                .HasForeignKey(d => d.BlobId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("skill_files_blob_id_fkey");
        });

        modelBuilder.Entity<SkillBlob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("skill_blobs_pkey");

            entity.ToTable("skill_blobs");

            entity.HasIndex(e => new { e.ProjectId, e.Sha256Hash }, "skill_blobs_project_id_sha_256_hash_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Sha256Hash)
                .HasMaxLength(44)
                .IsFixedLength()
                .HasColumnName("sha_256_hash");
            entity.Property(e => e.ContentType).HasColumnName("content_type");
            entity.Property(e => e.ContentLength).HasColumnName("content_length");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.Project).WithMany(p => p.SkillBlob)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("skill_blobs_project_id_fkey");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("models_pkey");

            entity.ToTable("models");

            entity.HasIndex(e => e.ModelName, "models_model_name_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ModelName, e.StartDate, e.Unit }, "models_project_id_model_name_start_date_unit_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.MatchPattern).HasColumnName("match_pattern");
            entity.Property(e => e.StartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.InputPrice)
                .HasPrecision(65, 30)
                .HasColumnName("input_price");
            entity.Property(e => e.OutputPrice)
                .HasPrecision(65, 30)
                .HasColumnName("output_price");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(65, 30)
                .HasColumnName("total_price");
            entity.Property(e => e.Unit).HasColumnName("unit");
            entity.Property(e => e.TokenizerId).HasColumnName("tokenizer_id");
            entity.Property(e => e.TokenizerConfig)
                .HasColumnType("jsonb")
                .HasColumnName("tokenizer_config");

            entity.HasOne(d => d.Project).WithMany(p => p.Model)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("models_project_id_fkey");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prices_pkey");

            entity.ToTable("prices");

            entity.HasIndex(e => e.PricingTierId, "prices_pricing_tier_id_idx");

            entity.HasIndex(e => new { e.ModelId, e.UsageType, e.PricingTierId }, "prices_model_id_usage_type_pricing_tier_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.PricingTierId).HasColumnName("pricing_tier_id");
            entity.Property(e => e.UsageType).HasColumnName("usage_type");
            entity.Property(e => e.Price1)
                .HasPrecision(65, 30)
                .HasColumnName("price");

            entity.HasOne(d => d.Model).WithMany(p => p.Price)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prices_model_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Price)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prices_project_id_fkey");

            entity.HasOne(d => d.PricingTier).WithMany(p => p.Prices)
                .HasForeignKey(d => d.PricingTierId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prices_pricing_tier_id_fkey");
        });

        modelBuilder.Entity<PricingTier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pricing_tiers_pkey");

            entity.ToTable("pricing_tiers");

            entity.HasIndex(e => new { e.ModelId, e.Priority }, "pricing_tiers_model_id_priority_key").IsUnique();

            entity.HasIndex(e => new { e.ModelId, e.Name }, "pricing_tiers_model_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Conditions)
                .HasColumnType("jsonb")
                .HasColumnName("conditions");

            entity.HasOne(d => d.Model).WithMany(p => p.PricingTiers)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pricing_tiers_model_id_fkey");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs");

            entity.HasIndex(e => e.ProjectId, "audit_logs_project_id_idx");

            entity.HasIndex(e => e.ApiKeyId, "audit_logs_api_key_id_idx");

            entity.HasIndex(e => e.UserId, "audit_logs_user_id_idx");

            entity.HasIndex(e => e.OrgId, "audit_logs_org_id_idx");

            entity.HasIndex(e => e.CreatedAt, "audit_logs_created_at_idx");

            entity.HasIndex(e => e.UpdatedAt, "audit_logs_updated_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(AuditLogRecordType.User)
                .HasColumnName("type");
            entity.Property(e => e.ApiKeyId).HasColumnName("api_key_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.UserOrgRole).HasColumnName("user_org_role");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserProjectRole).HasColumnName("user_project_role");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Before).HasColumnName("before");
            entity.Property(e => e.After).HasColumnName("after");
        });

        modelBuilder.Entity<AuditLogStaging>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_log_staging_pkey");

            entity.ToTable("audit_log_staging");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.FailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failed_at");
            entity.Property(e => e.LastError).HasColumnName("last_error");
        });

        modelBuilder.Entity<EvalTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eval_templates_pkey");

            entity.ToTable("eval_templates");

            entity.HasIndex(e => new { e.ProjectId, e.Id }, "eval_templates_project_id_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name, e.Version }, "eval_templates_project_id_name_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(EvalTemplateType.LlmAsJudge)
                .HasColumnName("type");
            entity.Property(e => e.Partner).HasColumnName("partner");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ModelParams)
                .HasColumnType("jsonb")
                .HasColumnName("model_params");
            entity.Property(e => e.Vars)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("vars");
            entity.Property(e => e.OutputDefinition)
                .HasColumnType("jsonb")
                .HasColumnName("output_schema");
            entity.Property(e => e.SourceCode)
                .HasMaxLength(262144)
                .HasColumnName("source_code");
            entity.Property(e => e.SourceCodeLanguage).HasColumnName("source_code_language");

            entity.HasOne(d => d.Project).WithMany(p => p.EvalTemplate)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("eval_templates_project_id_fkey");
        });

        modelBuilder.Entity<Evaluator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("evaluators_pkey");

            entity.ToTable("evaluators");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "evaluators_project_id_created_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.IsBuiltIn)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_built_in");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.BlockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("blocked_at");
            entity.Property(e => e.BlockReason).HasColumnName("block_reason");
            entity.Property(e => e.BlockMessage).HasColumnName("block_message");

            entity.HasOne(d => d.Project).WithMany(p => p.Evaluator)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluators_project_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.EvaluatorsCreated)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("evaluators_created_by_user_id_fkey");
        });

        modelBuilder.Entity<EvaluatorVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("evaluator_versions_pkey");

            entity.ToTable("evaluator_versions");

            entity.HasAlternateKey(e => new { e.EvaluatorId, e.Version }).HasName("evaluator_versions_evaluator_id_version_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluator_id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.PromptMessages)
                .HasColumnType("jsonb")
                .HasColumnName("prompt_messages");
            entity.Property(e => e.Partner).HasColumnName("partner");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ModelParams)
                .HasColumnType("jsonb")
                .HasColumnName("model_params");
            entity.Property(e => e.Vars)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("vars");
            entity.Property(e => e.VariableMapping)
                .HasColumnType("jsonb")
                .HasColumnName("variable_mapping");
            entity.Property(e => e.OutputDefinition)
                .HasColumnType("jsonb")
                .HasColumnName("output_definition");
            entity.Property(e => e.SourceCode)
                .HasMaxLength(262144)
                .HasColumnName("source_code");
            entity.Property(e => e.SourceCodeLanguage).HasColumnName("source_code_language");
            entity.Property(e => e.Questions)
                .HasColumnType("jsonb")
                .HasColumnName("questions");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.Versions)
                .HasForeignKey(d => d.EvaluatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluator_versions_evaluator_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.EvaluatorVersionsCreated)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("evaluator_versions_created_by_user_id_fkey");
        });

        modelBuilder.Entity<JobConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_configurations_pkey");

            entity.ToTable("job_configurations");

            entity.HasIndex(e => new { e.ProjectId, e.Id }, "job_configurations_project_id_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.JobType).HasColumnName("job_type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(JobConfigState.Active)
                .HasColumnName("status");
            entity.Property(e => e.BlockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("blocked_at");
            entity.Property(e => e.BlockReason).HasColumnName("block_reason");
            entity.Property(e => e.BlockMessage).HasColumnName("block_message");
            entity.Property(e => e.EvalTemplateId).HasColumnName("eval_template_id");
            entity.Property(e => e.ScoreName).HasColumnName("score_name");
            entity.Property(e => e.Filter)
                .HasColumnType("jsonb")
                .HasColumnName("filter");
            entity.Property(e => e.TargetObject).HasColumnName("target_object");
            entity.Property(e => e.VariableMapping)
                .HasColumnType("jsonb")
                .HasColumnName("variable_mapping");
            entity.Property(e => e.Sampling)
                .HasPrecision(65, 30)
                .HasColumnName("sampling");
            entity.Property(e => e.Delay).HasColumnName("delay");
            entity.Property(e => e.TimeScope)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string> { "NEW" })
                .HasColumnName("time_scope");

            entity.HasOne(d => d.Project).WithMany(p => p.JobConfiguration)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("job_configurations_project_id_fkey");

            entity.HasOne(d => d.EvalTemplate).WithMany(p => p.JobConfiguration)
                .HasForeignKey(d => d.EvalTemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("job_configurations_eval_template_id_fkey");
        });

        modelBuilder.Entity<EvaluationRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("evaluation_rules_pkey");

            entity.ToTable("evaluation_rules");

            entity.HasIndex(e => new { e.ProjectId, e.UpdatedAt }, "evaluation_rules_project_id_updated_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(JobConfigState.Active)
                .HasColumnName("status");
            entity.Property(e => e.TargetObject).HasColumnName("target_object");
            entity.Property(e => e.Filter)
                .HasColumnType("jsonb")
                .HasColumnName("filter");
            entity.Property(e => e.Sampling)
                .HasPrecision(65, 30)
                .HasColumnName("sampling");
            entity.Property(e => e.Delay).HasColumnName("delay");
            entity.Property(e => e.IdleTime).HasColumnName("idle_time");
            entity.Property(e => e.TimeScope)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string> { "NEW" })
                .HasColumnName("time_scope");

            entity.HasOne(d => d.Project).WithMany(p => p.EvaluationRule)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluation_rules_project_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.EvaluationRulesCreated)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("evaluation_rules_created_by_user_id_fkey");
        });

        modelBuilder.Entity<TracingReadinessSettings>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("tracing_readiness_settings_pkey");

            entity.ToTable("tracing_readiness_settings");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.TraceIdleMs).HasColumnName("trace_idle_ms");

            entity.HasOne(d => d.Project).WithOne(p => p.TracingReadinessSettings)
                .HasForeignKey<TracingReadinessSettings>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tracing_readiness_settings_project_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TracingReadinessCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tracing_readiness_settings_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.TracingReadinessUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tracing_readiness_settings_updated_by_fkey");
        });

        modelBuilder.Entity<EvaluationRuleEvaluatorAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("evaluation_rule_evaluator_assignments_pkey");

            entity.ToTable("evaluation_rule_evaluator_assignments");

            entity.HasIndex(e => e.ProjectId, "evaluation_rule_assignments_project_id_idx");

            entity.HasIndex(e => e.EvaluatorId, "evaluation_rule_assignments_evaluator_id_idx");

            entity.HasIndex(e => new { e.EvaluationRuleId, e.EvaluatorId }, "evaluation_rule_assignments_rule_evaluator_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.EvaluationRuleId).HasColumnName("evaluation_rule_id");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluator_id");
            entity.Property(e => e.VariableMapping)
                .HasColumnType("jsonb")
                .HasColumnName("variable_mapping");

            entity.HasOne(d => d.Project).WithMany(p => p.EvaluationRuleEvaluatorAssignment)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluation_rule_evaluator_assignments_project_id_fkey");

            entity.HasOne(d => d.EvaluationRule).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.EvaluationRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluation_rule_evaluator_assignments_evaluation_rule_id_fkey");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.EvaluatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("evaluation_rule_evaluator_assignments_evaluator_id_fkey");
        });

        modelBuilder.Entity<JobExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_executions_pkey");

            entity.ToTable("job_executions");

            entity.HasIndex(e => new { e.ProjectId, e.JobConfigurationId, e.JobInputTraceId }, "job_executions_project_id_job_configuration_id_job_input_tr_idx");

            entity.HasIndex(e => new { e.ProjectId, e.JobOutputScoreId }, "job_executions_project_id_job_output_score_id_idx");

            entity.HasIndex(e => e.JobConfigurationId, "job_executions_job_configuration_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.JobConfigurationId).HasColumnName("job_configuration_id");
            entity.Property(e => e.JobTemplateId).HasColumnName("job_template_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.JobInputTraceId).HasColumnName("job_input_trace_id");
            entity.Property(e => e.JobInputTraceTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("job_input_trace_timestamp");
            entity.Property(e => e.JobInputObservationId).HasColumnName("job_input_observation_id");
            entity.Property(e => e.JobInputDatasetItemId).HasColumnName("job_input_dataset_item_id");
            entity.Property(e => e.JobInputDatasetItemValidFrom)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("job_input_dataset_item_valid_from");
            entity.Property(e => e.JobOutputScoreId).HasColumnName("job_output_score_id");
            entity.Property(e => e.ExecutionTraceId).HasColumnName("execution_trace_id");

            entity.HasOne(d => d.Project).WithMany(p => p.JobExecution)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("job_executions_project_id_fkey");
        });

        modelBuilder.Entity<DefaultLlmModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("default_llm_models_pkey");

            entity.ToTable("default_llm_models");

            entity.HasIndex(e => e.ProjectId, "default_llm_models_project_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.LlmApiKeyId).HasColumnName("llm_api_key_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Adapter).HasColumnName("adapter");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelParams)
                .HasColumnType("jsonb")
                .HasColumnName("model_params");

            entity.HasOne(d => d.Project).WithMany(p => p.DefaultLlmModel)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_llm_models_project_id_fkey");

            entity.HasOne(d => d.LlmApiKey).WithMany(p => p.DefaultLlmModel)
                .HasForeignKey(d => d.LlmApiKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_llm_models_llm_api_key_id_fkey");
        });

        modelBuilder.Entity<TopicConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("topic_configs_pkey");

            entity.ToTable("topic_configs");

            entity.HasIndex(e => e.ProjectId, "topic_configs_project_id_key").IsUnique();

            entity.HasIndex(e => e.SummaryLlmApiKeyId, "topic_configs_summary_llm_api_key_id_idx");

            entity.HasIndex(e => e.EmbeddingLlmApiKeyId, "topic_configs_embedding_llm_api_key_id_idx");

            entity.HasIndex(e => e.ClusteringLlmApiKeyId, "topic_configs_clustering_llm_api_key_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.BlockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("blocked_at");
            entity.Property(e => e.BlockReason).HasColumnName("block_reason");
            entity.Property(e => e.BlockMessage).HasColumnName("block_message");
            entity.Property(e => e.SummaryLlmApiKeyId).HasColumnName("summary_llm_api_key_id");
            entity.Property(e => e.SummaryModel).HasColumnName("summary_model");
            entity.Property(e => e.EmbeddingLlmApiKeyId).HasColumnName("embedding_llm_api_key_id");
            entity.Property(e => e.EmbeddingModel).HasColumnName("embedding_model");
            entity.Property(e => e.EmbeddingDimensions)
                .ValueGeneratedNever()
                .HasDefaultValue(1024)
                .HasColumnName("embedding_dimensions");
            entity.Property(e => e.ClusteringLlmApiKeyId).HasColumnName("clustering_llm_api_key_id");
            entity.Property(e => e.ClusteringModel).HasColumnName("clustering_model");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TopicConfigsCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topic_configs_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.TopicConfigsUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topic_configs_updated_by_fkey");

            entity.HasOne(d => d.Project).WithOne(p => p.TopicConfig)
                .HasForeignKey<TopicConfig>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("topic_configs_project_id_fkey");

            entity.HasOne(d => d.SummaryLlmApiKey).WithMany(p => p.TopicSummaryConfigs)
                .HasForeignKey(d => d.SummaryLlmApiKeyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topic_configs_summary_llm_api_key_id_fkey");

            entity.HasOne(d => d.EmbeddingLlmApiKey).WithMany(p => p.TopicEmbeddingConfigs)
                .HasForeignKey(d => d.EmbeddingLlmApiKeyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topic_configs_embedding_llm_api_key_id_fkey");

            entity.HasOne(d => d.ClusteringLlmApiKey).WithMany(p => p.TopicClusteringConfigs)
                .HasForeignKey(d => d.ClusteringLlmApiKeyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("topic_configs_clustering_llm_api_key_id_fkey");
        });

        modelBuilder.Entity<SsoConfig>(entity =>
        {
            entity.HasKey(e => e.Domain).HasName("sso_configs_pkey");

            entity.ToTable("sso_configs");

            entity.Property(e => e.Domain)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("domain");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.AuthProvider).HasColumnName("auth_provider");
            entity.Property(e => e.AuthConfig)
                .HasColumnType("jsonb")
                .HasColumnName("auth_config");
        });

        modelBuilder.Entity<VerifiedDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verified_domains_pkey");

            entity.ToTable("verified_domains");

            entity.HasIndex(e => e.VerificationToken, "verified_domains_verification_token_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "verified_domains_organization_id_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Domain }, "verified_domains_organization_id_domain_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.VerificationToken)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("verification_token");
            entity.Property(e => e.VerifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("verified_at");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithMany(p => p.VerifiedDomains)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("verified_domains_organization_id_fkey");
        });

        modelBuilder.Entity<PosthogIntegration>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("posthog_integrations_pkey");

            entity.ToTable("posthog_integrations");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.EncryptedPosthogApiKey).HasColumnName("encrypted_posthog_api_key");
            entity.Property(e => e.PosthogHostName).HasColumnName("posthog_host_name");
            entity.Property(e => e.LastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_sync_at");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExportSource)
                .ValueGeneratedNever()
                .HasDefaultValue(AnalyticsIntegrationExportSource.TracesObservations)
                .HasColumnName("export_source");
            entity.Property(e => e.Backfill)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("backfill");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.LastErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_error_at");

            entity.HasOne(d => d.Project).WithMany(p => p.PosthogIntegration)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("posthog_integrations_project_id_fkey");
        });

        modelBuilder.Entity<MixpanelIntegration>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("mixpanel_integrations_pkey");

            entity.ToTable("mixpanel_integrations");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.EncryptedMixpanelProjectToken).HasColumnName("encrypted_mixpanel_project_token");
            entity.Property(e => e.MixpanelRegion).HasColumnName("mixpanel_region");
            entity.Property(e => e.LastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_sync_at");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExportSource)
                .ValueGeneratedNever()
                .HasDefaultValue(AnalyticsIntegrationExportSource.TracesObservations)
                .HasColumnName("export_source");
            entity.Property(e => e.Backfill)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("backfill");

            entity.HasOne(d => d.Project).WithMany(p => p.MixpanelIntegration)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("mixpanel_integrations_project_id_fkey");
        });

        modelBuilder.Entity<BlobStorageIntegration>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("blob_storage_integrations_pkey");

            entity.ToTable("blob_storage_integrations");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.BucketName).HasColumnName("bucket_name");
            entity.Property(e => e.Prefix).HasColumnName("prefix");
            entity.Property(e => e.AccessKeyId).HasColumnName("access_key_id");
            entity.Property(e => e.SecretAccessKey).HasColumnName("secret_access_key");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.ForcePathStyle).HasColumnName("force_path_style");
            entity.Property(e => e.NextSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_sync_at");
            entity.Property(e => e.LastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_sync_at");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.ExportFrequency).HasColumnName("export_frequency");
            entity.Property(e => e.FileType)
                .ValueGeneratedNever()
                .HasDefaultValue(BlobStorageIntegrationFileType.Csv)
                .HasColumnName("file_type");
            entity.Property(e => e.ExportMode)
                .ValueGeneratedNever()
                .HasDefaultValue(BlobStorageExportMode.FullHistory)
                .HasColumnName("export_mode");
            entity.Property(e => e.ExportStartDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("export_start_date");
            entity.Property(e => e.ExportSource)
                .ValueGeneratedNever()
                .HasDefaultValue(AnalyticsIntegrationExportSource.TracesObservations)
                .HasColumnName("export_source");
            entity.Property(e => e.ExportFieldGroups)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string> { "core", "basic", "time", "io", "metadata", "model", "usage", "prompt", "metrics", "tools", "trace_context" })
                .HasColumnName("export_field_groups");
            entity.Property(e => e.Compressed)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("compressed");
            entity.Property(e => e.ExportTuning)
                .HasColumnType("jsonb")
                .HasColumnName("export_tuning");
            entity.Property(e => e.RunStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("run_started_at");
            entity.Property(e => e.Backfill)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("backfill");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.LastErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_error_at");
            entity.Property(e => e.LastFailureNotificationSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_failure_notification_sent_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.BlobStorageIntegration)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("blob_storage_integrations_project_id_fkey");
        });

        modelBuilder.Entity<ExternalMediaStorageIntegration>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("external_media_storage_integrations_pkey");

            entity.ToTable("external_media_storage_integrations");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.BucketName).HasColumnName("bucket_name");
            entity.Property(e => e.Prefix).HasColumnName("prefix");
            entity.Property(e => e.AccessKeyId).HasColumnName("access_key_id");
            entity.Property(e => e.SecretAccessKey).HasColumnName("secret_access_key");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.ForcePathStyle).HasColumnName("force_path_style");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithOne(p => p.ExternalMediaStorageIntegration)
                .HasForeignKey<ExternalMediaStorageIntegration>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("external_media_storage_integrations_project_id_fkey");
        });

        modelBuilder.Entity<WebCalloutEndpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("web_callout_endpoints_pkey");

            entity.ToTable("web_callout_endpoints");

            entity.HasIndex(e => e.ProjectId, "web_callout_endpoints_project_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Enabled }, "web_callout_endpoints_project_id_enabled_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("Default")
                .HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.ToastMessage)
                .ValueGeneratedNever()
                .HasDefaultValue("Callout sent")
                .HasColumnName("toast_message");
            entity.Property(e => e.RequestHeaders).HasColumnName("request_headers");
            entity.Property(e => e.RequestHeaderKeys)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("request_header_keys");

            entity.HasOne(d => d.Project).WithMany(p => p.WebCalloutEndpoints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("web_callout_endpoints_project_id_fkey");
        });

        modelBuilder.Entity<BatchExport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("batch_exports_pkey");

            entity.ToTable("batch_exports");

            entity.HasIndex(e => new { e.ProjectId, e.UserId }, "batch_exports_project_id_user_id_idx");

            entity.HasIndex(e => e.Status, "batch_exports_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Query)
                .HasColumnType("jsonb")
                .HasColumnName("query");
            entity.Property(e => e.Format).HasColumnName("format");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Log).HasColumnName("log");

            entity.HasOne(d => d.Project).WithMany(p => p.BatchExport)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("batch_exports_project_id_fkey");
        });

        modelBuilder.Entity<BatchAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("batch_actions_pkey");

            entity.ToTable("batch_actions");

            entity.HasIndex(e => new { e.ProjectId, e.UserId }, "batch_actions_project_id_user_id_idx");

            entity.HasIndex(e => e.Status, "batch_actions_status_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ActionType }, "batch_actions_project_id_action_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ActionType).HasColumnName("action_type");
            entity.Property(e => e.TableName).HasColumnName("table_name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.Query)
                .HasColumnType("jsonb")
                .HasColumnName("query");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.TotalCount).HasColumnName("total_count");
            entity.Property(e => e.ProcessedCount).HasColumnName("processed_count");
            entity.Property(e => e.FailedCount).HasColumnName("failed_count");
            entity.Property(e => e.Log).HasColumnName("log");

            entity.HasOne(d => d.Project).WithMany(p => p.BatchAction)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("batch_actions_project_id_fkey");
        });

        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.Id }).HasName("media_project_id_id_key");

            entity.ToTable("media");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "media_project_id_created_at_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Sha256Hash }, "media_project_id_sha_256_hash_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sha256Hash)
                .HasMaxLength(44)
                .IsFixedLength()
                .HasColumnName("sha_256_hash");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UploadedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("uploaded_at");
            entity.Property(e => e.UploadHttpStatus).HasColumnName("upload_http_status");
            entity.Property(e => e.UploadHttpError).HasColumnName("upload_http_error");
            entity.Property(e => e.BucketPath).HasColumnName("bucket_path");
            entity.Property(e => e.BucketName).HasColumnName("bucket_name");
            entity.Property(e => e.ContentType).HasColumnName("content_type");
            entity.Property(e => e.ContentLength).HasColumnName("content_length");

            entity.HasOne(d => d.Project).WithMany(p => p.Media)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("media_project_id_fkey");
        });

        modelBuilder.Entity<TraceMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trace_media_pkey");

            entity.ToTable("trace_media");

            entity.HasIndex(e => new { e.ProjectId, e.MediaId }, "trace_media_project_id_media_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId, e.MediaId, e.Field }, "trace_media_project_id_trace_id_media_id_field_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.TraceId).HasColumnName("trace_id");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.Origin)
                .ValueGeneratedNever()
                .HasDefaultValue(MediaAssociationOrigin.Unknown)
                .HasColumnName("origin");
        });

        modelBuilder.Entity<ObservationMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("observation_media_pkey");

            entity.ToTable("observation_media");

            entity.HasIndex(e => new { e.ProjectId, e.MediaId }, "observation_media_project_id_media_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.TraceId, e.ObservationId, e.MediaId, e.Field }, "observation_media_project_id_trace_id_observation_id_media__key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.TraceId).HasColumnName("trace_id");
            entity.Property(e => e.ObservationId).HasColumnName("observation_id");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.Origin)
                .ValueGeneratedNever()
                .HasDefaultValue(MediaAssociationOrigin.Unknown)
                .HasColumnName("origin");
        });

        modelBuilder.Entity<DatasetItemMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dataset_item_media_pkey");

            entity.ToTable("dataset_item_media");

            entity.HasIndex(e => new { e.ProjectId, e.MediaId }, "dataset_item_media_project_id_media_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.DatasetId }, "dataset_item_media_project_id_dataset_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.DatasetItemId, e.DatasetItemValidFrom, e.Field, e.JsonPath }, "dataset_item_media_item_version_field_path_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.DatasetId).HasColumnName("dataset_id");
            entity.Property(e => e.DatasetItemId).HasColumnName("dataset_item_id");
            entity.Property(e => e.DatasetItemValidFrom)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dataset_item_valid_from");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.JsonPath).HasColumnName("json_path");
            entity.Property(e => e.ReferenceString).HasColumnName("reference_string");
        });

        modelBuilder.Entity<BillingMeterBackup>(entity =>
        {
            entity.HasKey(e => new { e.StripeCustomerId, e.MeterId, e.StartTime, e.EndTime }).HasName("billing_meter_backups_stripe_customer_id_meter_id_start_tim_key");

            entity.ToTable("billing_meter_backups");

            entity.HasIndex(e => new { e.StripeCustomerId, e.MeterId, e.StartTime, e.EndTime }, "billing_meter_backups_stripe_customer_id_meter_id_start_tim_idx");

            entity.Property(e => e.StripeCustomerId).HasColumnName("stripe_customer_id");
            entity.Property(e => e.MeterId).HasColumnName("meter_id");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.AggregatedValue).HasColumnName("aggregated_value");
            entity.Property(e => e.EventName).HasColumnName("event_name");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LlmSchema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_schemas_pkey");

            entity.ToTable("llm_schemas");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "llm_schemas_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Schema)
                .HasColumnType("json")
                .HasColumnName("schema");

            entity.HasOne(d => d.Project).WithMany(p => p.LlmSchema)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_schemas_project_id_fkey");
        });

        modelBuilder.Entity<LlmTool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("llm_tools_pkey");

            entity.ToTable("llm_tools");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "llm_tools_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Parameters)
                .HasColumnType("json")
                .HasColumnName("parameters");

            entity.HasOne(d => d.Project).WithMany(p => p.LlmTool)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("llm_tools_project_id_fkey");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dashboards_pkey");

            entity.ToTable("dashboards");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Definition)
                .HasColumnType("jsonb")
                .HasColumnName("definition");
            entity.Property(e => e.Filters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("filters");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.DashboardCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("dashboards_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.DashboardUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("dashboards_updated_by_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Dashboard)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dashboards_project_id_fkey");
        });

        modelBuilder.Entity<DashboardWidget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dashboard_widgets_pkey");

            entity.ToTable("dashboard_widgets");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.View).HasColumnName("view");
            entity.Property(e => e.Dimensions)
                .HasColumnType("jsonb")
                .HasColumnName("dimensions");
            entity.Property(e => e.Metrics)
                .HasColumnType("jsonb")
                .HasColumnName("metrics");
            entity.Property(e => e.Filters)
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.ChartType).HasColumnName("chart_type");
            entity.Property(e => e.ChartConfig)
                .HasColumnType("jsonb")
                .HasColumnName("chart_config");
            entity.Property(e => e.MinVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("min_version");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.DashboardWidgetsCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("dashboard_widgets_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.DashboardWidgetsUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("dashboard_widgets_updated_by_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.DashboardWidget)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dashboard_widgets_project_id_fkey");
        });

        modelBuilder.Entity<TableViewPreset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("table_view_presets_pkey");

            entity.ToTable("table_view_presets");

            entity.HasIndex(e => new { e.ProjectId, e.TableName, e.Name }, "table_view_presets_project_id_table_name_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TableName).HasColumnName("table_name");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Filters)
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.ColumnOrder)
                .HasColumnType("jsonb")
                .HasColumnName("column_order");
            entity.Property(e => e.ColumnVisibility)
                .HasColumnType("jsonb")
                .HasColumnName("column_visibility");
            entity.Property(e => e.SearchQuery).HasColumnName("search_query");
            entity.Property(e => e.OrderBy)
                .HasColumnType("jsonb")
                .HasColumnName("order_by");

            entity.HasOne(d => d.Project).WithMany(p => p.TableViewPreset)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("table_view_presets_project_id_fkey");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TableViewPresetCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("table_view_presets_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.TableViewPresetUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("table_view_presets_updated_by_fkey");
        });

        modelBuilder.Entity<DefaultView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("default_views_pkey");

            entity.ToTable("default_views");

            entity.HasIndex(e => new { e.ProjectId, e.ViewName }, "default_views_project_id_view_name_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ViewName).HasColumnName("view_name");
            entity.Property(e => e.ViewId).HasColumnName("view_id");

            entity.HasOne(d => d.Project).WithMany(p => p.DefaultView)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_views_project_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.DefaultViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_views_user_id_fkey");
        });

        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actions_pkey");

            entity.ToTable("actions");

            entity.HasIndex(e => e.ProjectId, "actions_project_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");

            entity.HasOne(d => d.Project).WithMany(p => p.Actions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("actions_project_id_fkey");
        });

        modelBuilder.Entity<Trigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("triggers_pkey");

            entity.ToTable("triggers");

            entity.HasIndex(e => e.ProjectId, "triggers_project_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.EventSource).HasColumnName("eventSource");
            entity.Property(e => e.EventActions).HasColumnName("eventActions");
            entity.Property(e => e.Filter)
                .HasColumnType("jsonb")
                .HasColumnName("filter");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(JobConfigState.Active)
                .HasColumnName("status");

            entity.HasOne(d => d.Project).WithMany(p => p.Triggers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("triggers_project_id_fkey");
        });

        modelBuilder.Entity<Automation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("automations_pkey");

            entity.ToTable("automations");

            entity.HasIndex(e => new { e.ProjectId, e.ActionId, e.TriggerId }, "automations_project_id_action_id_trigger_id_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "automations_project_id_name_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TriggerId).HasColumnName("trigger_id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Trigger).WithMany(p => p.Automations)
                .HasForeignKey(d => d.TriggerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automations_trigger_id_fkey");

            entity.HasOne(d => d.Action).WithMany(p => p.Automations)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automations_action_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Automation)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automations_project_id_fkey");
        });

        modelBuilder.Entity<AutomationExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("automation_executions_pkey");

            entity.ToTable("automation_executions");

            entity.HasIndex(e => e.TriggerId, "automation_executions_trigger_id_idx");

            entity.HasIndex(e => e.ActionId, "automation_executions_action_id_idx");

            entity.HasIndex(e => e.ProjectId, "automation_executions_project_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SourceId).HasColumnName("source_id");
            entity.Property(e => e.AutomationId).HasColumnName("automation_id");
            entity.Property(e => e.TriggerId).HasColumnName("trigger_id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ActionExecutionStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.Error).HasColumnName("error");

            entity.HasOne(d => d.Automation).WithMany(p => p.AutomationExecution)
                .HasForeignKey(d => d.AutomationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automation_executions_automation_id_fkey");

            entity.HasOne(d => d.Trigger).WithMany(p => p.AutomationExecutions)
                .HasForeignKey(d => d.TriggerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automation_executions_trigger_id_fkey");

            entity.HasOne(d => d.Action).WithMany(p => p.AutomationExecutions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automation_executions_action_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.AutomationExecutions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("automation_executions_project_id_fkey");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("monitors_pkey");

            entity.ToTable("monitors");

            entity.HasIndex(e => e.ProjectId, "monitors_project_id_idx");

            entity.HasIndex(e => e.SchedulerBatchId, "monitors_scheduler_batch_id_idx");

            entity.HasIndex(e => new { e.NextRunAt, e.SchedulerBatchId }, "monitors_scheduler_tick_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.View).HasColumnName("view");
            entity.Property(e => e.Filters)
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.Metric)
                .HasColumnType("jsonb")
                .HasColumnName("metric");
            entity.Property(e => e.WindowMs).HasColumnName("window_ms");
            entity.Property(e => e.CadenceMs).HasColumnName("cadence_ms");
            entity.Property(e => e.ThresholdOperator).HasColumnName("threshold_operator");
            entity.Property(e => e.AlertThreshold)
                .HasPrecision(65, 30)
                .HasColumnName("alert_threshold");
            entity.Property(e => e.WarningThreshold)
                .HasPrecision(65, 30)
                .HasColumnName("warning_threshold");
            entity.Property(e => e.Severity)
                .ValueGeneratedNever()
                .HasDefaultValue(MonitorSeverity.Unknown)
                .HasColumnName("severity");
            entity.Property(e => e.SeverityChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("severity_changed_at");
            entity.Property(e => e.NoData)
                .HasColumnType("jsonb")
                .HasColumnName("no_data");
            entity.Property(e => e.Renotify)
                .HasColumnType("jsonb")
                .HasColumnName("renotify");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(MonitorStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.SchedulerBatchId).HasColumnName("scheduler_batch_id");
            entity.Property(e => e.NextRunAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_run_at");
            entity.Property(e => e.LastPublishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_published_at");
            entity.Property(e => e.LastClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_claimed_at");
            entity.Property(e => e.LastCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_completed_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.TriggerIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("trigger_ids");
            entity.Property(e => e.AlertedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("alerted_at");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.MonitorsCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("monitors_created_by_fkey");

            entity.HasOne(d => d.UpdatedByUser).WithMany(p => p.MonitorsUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("monitors_updated_by_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Monitors)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("monitors_project_id_fkey");
        });

        modelBuilder.Entity<SlackIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("slack_integrations_pkey");

            entity.ToTable("slack_integrations");

            entity.HasIndex(e => e.ProjectId, "slack_integrations_project_id_key").IsUnique();

            entity.HasIndex(e => e.TeamId, "slack_integrations_team_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamName).HasColumnName("team_name");
            entity.Property(e => e.BotToken).HasColumnName("bot_token");
            entity.Property(e => e.BotUserId).HasColumnName("bot_user_id");
            entity.Property(e => e.InstallerUserId).HasColumnName("installer_user_id");
            entity.Property(e => e.InstallerSlackUserId).HasColumnName("installer_slack_user_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithOne(p => p.SlackIntegration)
                .HasForeignKey<SlackIntegration>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("slack_integrations_project_id_fkey");
        });

        modelBuilder.Entity<PendingDeletion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pending_deletions_pkey");

            entity.ToTable("pending_deletions");

            entity.HasIndex(e => new { e.ProjectId, e.Object, e.IsDeleted, e.ObjectId, e.Id }, "pending_deletions_project_id_object_is_deleted_object_id_id_idx");

            entity.HasIndex(e => new { e.ObjectId, e.Object }, "pending_deletions_object_id_object_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Object).HasColumnName("object");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.IsDeleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.PendingDeletion)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pending_deletions_project_id_fkey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("surveys_pkey");

            entity.ToTable("surveys");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SurveyName).HasColumnName("survey_name");
            entity.Property(e => e.Response)
                .HasColumnType("jsonb")
                .HasColumnName("response");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.OrgId).HasColumnName("org_id");

            entity.HasOne(d => d.Org).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("surveys_org_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("surveys_user_id_fkey");
        });

        modelBuilder.Entity<CloudSpendAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cloud_spend_alerts_pkey");

            entity.ToTable("cloud_spend_alerts");

            entity.HasIndex(e => e.OrgId, "cloud_spend_alerts_org_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Threshold)
                .HasPrecision(65, 30)
                .HasColumnName("threshold");
            entity.Property(e => e.TriggeredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("triggered_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Org).WithMany(p => p.CloudSpendAlerts)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cloud_spend_alerts_org_id_fkey");
        });

        modelBuilder.Entity<TopicClusteringRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("topic_clustering_runs_pkey");

            entity.ToTable("topic_clustering_runs");

            entity.HasIndex(e => new { e.ProjectId, e.FacetId, e.Status, e.FacetVersion, e.CreatedAt, e.Id }, "topic_clustering_runs_serving_map_idx").IsDescending(false, false, false, true, true, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.FacetId).HasColumnName("facet_id");
            entity.Property(e => e.FacetVersion).HasColumnName("facet_version");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.StartedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("started_at");
            entity.Property(e => e.FinishedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("finished_at");
            entity.Property(e => e.TopicVersionIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("topic_version_ids");

            entity.HasOne(d => d.Project).WithMany(p => p.TopicClusteringRuns)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("topic_clustering_runs_project_id_fkey");

            entity.HasOne(d => d.Facet).WithMany(p => p.TopicClusteringRuns)
                .HasPrincipalKey(p => new { p.EvaluatorId, p.Version })
                .HasForeignKey(d => new { d.FacetId, d.FacetVersion })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("topic_clustering_runs_facet_id_facet_version_fkey");
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

        foreach (var entry in ChangeTracker.Entries<Organization>())
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

        foreach (var entry in ChangeTracker.Entries<RoleAssignment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GatewayAiConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InAppAgentConversation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InAppAgentRun>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InAppAgentPendingToolApproval>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LlmApiKeys>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganizationMembership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectMembership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MembershipInvitation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TraceSession>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ScoreConfig>())
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

        foreach (var entry in ChangeTracker.Entries<AnnotationQueueAssignment>())
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

        foreach (var entry in ChangeTracker.Entries<DatasetItem>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DatasetRuns>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Comment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<NotificationPreference>())
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

        foreach (var entry in ChangeTracker.Entries<PromptDependency>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PromptProtectedLabels>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Skill>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Model>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Price>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PricingTier>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuditLog>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EvalTemplate>())
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

        foreach (var entry in ChangeTracker.Entries<JobConfiguration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EvaluationRule>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TracingReadinessSettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EvaluationRuleEvaluatorAssignment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<JobExecution>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DefaultLlmModel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TopicConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SsoConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<VerifiedDomain>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BlobStorageIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExternalMediaStorageIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WebCalloutEndpoint>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BatchExport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BatchAction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Media>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TraceMedia>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ObservationMedia>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DatasetItemMedia>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BillingMeterBackup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LlmSchema>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LlmTool>())
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

        foreach (var entry in ChangeTracker.Entries<DashboardWidget>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TableViewPreset>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DefaultView>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Action>())
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

        foreach (var entry in ChangeTracker.Entries<AutomationExecution>())
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

        foreach (var entry in ChangeTracker.Entries<SlackIntegration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PendingDeletion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CloudSpendAlert>())
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

file sealed class UuidV4StringGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => Guid.NewGuid().ToString();
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
