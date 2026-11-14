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

namespace LiteLLM.Models;

public partial class LiteLLMContext : DbContext
{
    public LiteLLMContext(DbContextOptions<LiteLLMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LiteLLMBudgetTable> LiteLLMBudgetTables { get; set; }

    public virtual DbSet<LiteLLMCustomerOrganizationTable> LiteLLMCustomerOrganizationTables { get; set; }

    public virtual DbSet<LiteLLMCustomerTeamTable> LiteLLMCustomerTeamTables { get; set; }

    public virtual DbSet<LiteLLMCustomerMemberTable> LiteLLMCustomerMemberTables { get; set; }

    public virtual DbSet<LiteLLMCustomerBudgetIncreaseRequest> LiteLLMCustomerBudgetIncreaseRequests { get; set; }

    public virtual DbSet<LiteLLMCredentialsTable> LiteLLMCredentialsTables { get; set; }

    public virtual DbSet<LiteLLMProxyModelTable> LiteLLMProxyModelTables { get; set; }

    public virtual DbSet<LiteLLMAgentsTable> LiteLLMAgentsTables { get; set; }

    public virtual DbSet<LiteLLMAgentIdentity> LiteLLMAgentIdentities { get; set; }

    public virtual DbSet<LiteLLMRetiredAgentIdentity> LiteLLMRetiredAgentIdentities { get; set; }

    public virtual DbSet<LiteLLMRetiredAgent> LiteLLMRetiredAgents { get; set; }

    public virtual DbSet<LiteLLMVerifiedSubject> LiteLLMVerifiedSubjects { get; set; }

    public virtual DbSet<LiteLLMOrganizationTable> LiteLLMOrganizationTables { get; set; }

    public virtual DbSet<LiteLLMModelTable> LiteLLMModelTables { get; set; }

    public virtual DbSet<LiteLLMTeamTable> LiteLLMTeamTables { get; set; }

    public virtual DbSet<LiteLLMProjectTable> LiteLLMProjectTables { get; set; }

    public virtual DbSet<LiteLLMDeletedTeamTable> LiteLLMDeletedTeamTables { get; set; }

    public virtual DbSet<LiteLLMUserTable> LiteLLMUserTables { get; set; }

    public virtual DbSet<LiteLLMObjectPermissionTable> LiteLLMObjectPermissionTables { get; set; }

    public virtual DbSet<LiteLLMMCPServerTable> LiteLLMMCPServerTables { get; set; }

    public virtual DbSet<LiteLLMMCPToolsetTable> LiteLLMMCPToolsetTables { get; set; }

    public virtual DbSet<LiteLLMMCPUserCredentials> LiteLLMMCPUserCredentials { get; set; }

    public virtual DbSet<LiteLLMUserProviderCredentials> LiteLLMUserProviderCredentials { get; set; }

    public virtual DbSet<LiteLLMMCPUserEnvVars> LiteLLMMCPUserEnvVars { get; set; }

    public virtual DbSet<LiteLLMMCPServerOAuthClient> LiteLLMMCPServerOAuthClients { get; set; }

    public virtual DbSet<LiteLLMSSOIdentityAssertion> LiteLLMSSOIdentityAssertions { get; set; }

    public virtual DbSet<LiteLLMVerificationToken> LiteLLMVerificationTokens { get; set; }

    public virtual DbSet<LiteLLMJWTKeyMapping> LiteLLMJWTKeyMappings { get; set; }

    public virtual DbSet<LiteLLMDeprecatedVerificationToken> LiteLLMDeprecatedVerificationTokens { get; set; }

    public virtual DbSet<LiteLLMDeletedVerificationToken> LiteLLMDeletedVerificationTokens { get; set; }

    public virtual DbSet<LiteLLMEndUserTable> LiteLLMEndUserTables { get; set; }

    public virtual DbSet<LiteLLMModelAccessGroupBudgetTable> LiteLLMModelAccessGroupBudgetTables { get; set; }

    public virtual DbSet<LiteLLMTagTable> LiteLLMTagTables { get; set; }

    public virtual DbSet<LiteLLMConfig> LiteLLMConfigs { get; set; }

    public virtual DbSet<LiteLLMSpendLogs> LiteLLMSpendLogs { get; set; }

    public virtual DbSet<LiteLLMBudgetWindowSpend> LiteLLMBudgetWindowSpends { get; set; }

    public virtual DbSet<LiteLLMErrorLogs> LiteLLMErrorLogs { get; set; }

    public virtual DbSet<LiteLLMUserNotifications> LiteLLMUserNotifications { get; set; }

    public virtual DbSet<LiteLLMTeamMembership> LiteLLMTeamMemberships { get; set; }

    public virtual DbSet<LiteLLMOrganizationMembership> LiteLLMOrganizationMemberships { get; set; }

    public virtual DbSet<LiteLLMInvitationLink> LiteLLMInvitationLinks { get; set; }

    public virtual DbSet<LiteLLMAuditLog> LiteLLMAuditLogs { get; set; }

    public virtual DbSet<LiteLLMDailyUserSpend> LiteLLMDailyUserSpends { get; set; }

    public virtual DbSet<LiteLLMDailyGlobalSpend> LiteLLMDailyGlobalSpends { get; set; }

    public virtual DbSet<LiteLLMDailyOrganizationSpend> LiteLLMDailyOrganizationSpends { get; set; }

    public virtual DbSet<LiteLLMDailyCustomerMemberSpend> LiteLLMDailyCustomerMemberSpends { get; set; }

    public virtual DbSet<LiteLLMDailyEndUserSpend> LiteLLMDailyEndUserSpends { get; set; }

    public virtual DbSet<LiteLLMDailyAgentSpend> LiteLLMDailyAgentSpends { get; set; }

    public virtual DbSet<LiteLLMDailyTeamSpend> LiteLLMDailyTeamSpends { get; set; }

    public virtual DbSet<LiteLLMDailyTagSpend> LiteLLMDailyTagSpends { get; set; }

    public virtual DbSet<LiteLLMProxyWorkerHeartbeat> LiteLLMProxyWorkerHeartbeats { get; set; }

    public virtual DbSet<LiteLLMCronJob> LiteLLMCronJobs { get; set; }

    public virtual DbSet<LiteLLMScheduledJobRun> LiteLLMScheduledJobRuns { get; set; }

    public virtual DbSet<LiteLLMManagedFileTable> LiteLLMManagedFileTables { get; set; }

    public virtual DbSet<LiteLLMManagedObjectTable> LiteLLMManagedObjectTables { get; set; }

    public virtual DbSet<LiteLLMManagedFileContentTable> LiteLLMManagedFileContentTables { get; set; }

    public virtual DbSet<LiteLLMManagedVectorStoreTable> LiteLLMManagedVectorStoreTables { get; set; }

    public virtual DbSet<LiteLLMManagedVectorStoresTable> LiteLLMManagedVectorStoresTables { get; set; }

    public virtual DbSet<LiteLLMGuardrailsTable> LiteLLMGuardrailsTables { get; set; }

    public virtual DbSet<LiteLLMDailyGuardrailMetrics> LiteLLMDailyGuardrailMetrics { get; set; }

    public virtual DbSet<LiteLLMDailyGuardrailUsageUnits> LiteLLMDailyGuardrailUsageUnits { get; set; }

    public virtual DbSet<LiteLLMDailyPolicyMetrics> LiteLLMDailyPolicyMetrics { get; set; }

    public virtual DbSet<LiteLLMSpendLogGuardrailIndex> LiteLLMSpendLogGuardrailIndices { get; set; }

    public virtual DbSet<LiteLLMSpendLogToolIndex> LiteLLMSpendLogToolIndices { get; set; }

    public virtual DbSet<LiteLLMDailyToolSpend> LiteLLMDailyToolSpends { get; set; }

    public virtual DbSet<LiteLLMDailyModelUsage> LiteLLMDailyModelUsages { get; set; }

    public virtual DbSet<LiteLLMDailyGatewayRequests> LiteLLMDailyGatewayRequests { get; set; }

    public virtual DbSet<LiteLLMDailyGatewayFailedRequests> LiteLLMDailyGatewayFailedRequests { get; set; }

    public virtual DbSet<LiteLLMDailyRequestErrors> LiteLLMDailyRequestErrors { get; set; }

    public virtual DbSet<LiteLLMPromptTable> LiteLLMPromptTables { get; set; }

    public virtual DbSet<LiteLLMHealthCheckTable> LiteLLMHealthCheckTables { get; set; }

    public virtual DbSet<LiteLLMSearchToolsTable> LiteLLMSearchToolsTables { get; set; }

    public virtual DbSet<LiteLLMSSOConfig> LiteLLMSSOConfigs { get; set; }

    public virtual DbSet<LiteLLMManagedVectorStoreIndexTable> LiteLLMManagedVectorStoreIndexTables { get; set; }

    public virtual DbSet<LiteLLMCacheConfig> LiteLLMCacheConfigs { get; set; }

    public virtual DbSet<LiteLLMUISettings> LiteLLMUISettings { get; set; }

    public virtual DbSet<LiteLLMConfigOverrides> LiteLLMConfigOverrides { get; set; }

    public virtual DbSet<LiteLLMSkillsTable> LiteLLMSkillsTables { get; set; }

    public virtual DbSet<LiteLLMPolicyTable> LiteLLMPolicyTables { get; set; }

    public virtual DbSet<LiteLLMPolicyAttachmentTable> LiteLLMPolicyAttachmentTables { get; set; }

    public virtual DbSet<LiteLLMToolTable> LiteLLMToolTables { get; set; }

    public virtual DbSet<LiteLLMAccessGroupTable> LiteLLMAccessGroupTables { get; set; }

    public virtual DbSet<LiteLLMClaudeCodePluginTable> LiteLLMClaudeCodePluginTables { get; set; }

    public virtual DbSet<LiteLLMClaudeCodePluginVersion> LiteLLMClaudeCodePluginVersions { get; set; }

    public virtual DbSet<LiteLLMSkillSubmission> LiteLLMSkillSubmissions { get; set; }

    public virtual DbSet<LiteLLMClaudeCodePluginDownload> LiteLLMClaudeCodePluginDownloads { get; set; }

    public virtual DbSet<LiteLLMClaudeCodePluginUpload> LiteLLMClaudeCodePluginUploads { get; set; }

    public virtual DbSet<LiteLLMMemoryTable> LiteLLMMemoryTables { get; set; }

    public virtual DbSet<LiteLLMAdaptiveRouterState> LiteLLMAdaptiveRouterStates { get; set; }

    public virtual DbSet<LiteLLMAdaptiveRouterSession> LiteLLMAdaptiveRouterSessions { get; set; }

    public virtual DbSet<LiteLLMAutoRouterBaselineComparison> LiteLLMAutoRouterBaselineComparisons { get; set; }

    public virtual DbSet<LiteLLMAutoRouterBaselineObservation> LiteLLMAutoRouterBaselineObservations { get; set; }

    public virtual DbSet<LiteLLMAutoRouterSession> LiteLLMAutoRouterSessions { get; set; }

    public virtual DbSet<LiteLLMAutoRouterUserSession> LiteLLMAutoRouterUserSessions { get; set; }

    public virtual DbSet<LiteLLMAutoRouterDailySpend> LiteLLMAutoRouterDailySpends { get; set; }

    public virtual DbSet<LiteLLMShadowEvalJob> LiteLLMShadowEvalJobs { get; set; }

    public virtual DbSet<LiteLLMShadowEvalAttempt> LiteLLMShadowEvalAttempts { get; set; }

    public virtual DbSet<LiteLLMShadowEvalFunnel> LiteLLMShadowEvalFunnels { get; set; }

    public virtual DbSet<LiteLLMWorkflowRun> LiteLLMWorkflowRuns { get; set; }

    public virtual DbSet<LiteLLMWorkflowEvent> LiteLLMWorkflowEvents { get; set; }

    public virtual DbSet<LiteLLMWorkflowMessage> LiteLLMWorkflowMessages { get; set; }

    public virtual DbSet<LiteLLMBackgroundInteractionSettlement> LiteLLMBackgroundInteractionSettlements { get; set; }

    public virtual DbSet<LiteLLMBackgroundResponseSettlement> LiteLLMBackgroundResponseSettlements { get; set; }

    public virtual DbSet<LiteLLMLens> LiteLLMLens { get; set; }

    public virtual DbSet<LiteLLMLensRun> LiteLLMLensRuns { get; set; }

    public virtual DbSet<LiteLLMLensReview> LiteLLMLensReviews { get; set; }

    public virtual DbSet<LiteLLMLensWorker> LiteLLMLensWorkers { get; set; }

    public virtual DbSet<LiteLLMLensIngestionKey> LiteLLMLensIngestionKeys { get; set; }

    public virtual DbSet<LiteLLMLensDataset> LiteLLMLensDatasets { get; set; }

    public virtual DbSet<LiteLLMLensSignalConfig> LiteLLMLensSignalConfigs { get; set; }

    public virtual DbSet<LiteLLMLensTraceSignal> LiteLLMLensTraceSignals { get; set; }

    public virtual DbSet<LiteLLMTelemetryReport> LiteLLMTelemetryReports { get; set; }

    public virtual DbSet<LiteLLMSpendLogDelivery> LiteLLMSpendLogDeliveries { get; set; }

    public virtual DbSet<LiteLLMSubscriptionAccountTable> LiteLLMSubscriptionAccountTables { get; set; }

    public virtual DbSet<LiteLLMAlert> LiteLLMAlerts { get; set; }

    public virtual DbSet<LiteLLMPerformanceAlertState> LiteLLMPerformanceAlertStates { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<JobStatus>("JobStatus");
        npgsql.MapEnum<ScheduledJobRunStatus>("ScheduledJobRunStatus");
        npgsql.MapEnum<AccountingDeliveryState>("AccountingDeliveryState");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<JobStatus>("JobStatus");
        dataSource.MapEnum<ScheduledJobRunStatus>("ScheduledJobRunStatus");
        dataSource.MapEnum<AccountingDeliveryState>("AccountingDeliveryState");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<JobStatus>(name: "JobStatus");
        modelBuilder.HasPostgresEnum<ScheduledJobRunStatus>(name: "ScheduledJobRunStatus");
        modelBuilder.HasPostgresEnum<AccountingDeliveryState>(name: "AccountingDeliveryState");

        modelBuilder.Entity<LiteLLMBudgetTable>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("LiteLLM_BudgetTable_pkey");

            entity.ToTable("LiteLLM_BudgetTable");

            entity.Property(e => e.BudgetId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("budget_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.SoftBudget).HasColumnName("soft_budget");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.TpdLimit).HasColumnName("tpd_limit");
            entity.Property(e => e.ModelMaxBudget)
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.ModelTpmLimit)
                .HasColumnType("jsonb")
                .HasColumnName("model_tpm_limit");
            entity.Property(e => e.ModelRpmLimit)
                .HasColumnType("jsonb")
                .HasColumnName("model_rpm_limit");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetLimits)
                .HasColumnType("jsonb")
                .HasColumnName("budget_limits");
            entity.Property(e => e.CapPolicy).HasColumnName("cap_policy");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.AllowedModels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_models");
            entity.Property(e => e.TempBudgetIncrease).HasColumnName("temp_budget_increase");
            entity.Property(e => e.TempBudgetExpiry)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("temp_budget_expiry");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMCustomerOrganizationTable>(entity =>
        {
            entity.HasKey(e => e.CustomerOrgId).HasName("LiteLLM_CustomerOrganizationTable_pkey");

            entity.ToTable("LiteLLM_CustomerOrganizationTable");

            entity.HasIndex(e => e.Spend, "LiteLLM_CustomerOrganizationTable_spend_idx");

            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.BudgetTemplateIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("budget_template_ids");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.CustomerOrgId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("customer_org_id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.DefaultBudgetTemplateId).HasColumnName("default_budget_template_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Budget).WithMany(p => p.CustomerOrganizations)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_CustomerOrganizationTable_budget_id_fkey");

            entity.HasOne(d => d.DefaultBudgetTemplate).WithMany(p => p.CustomerOrganizationDefaults)
                .HasForeignKey(d => d.DefaultBudgetTemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_CustomerOrganizationTable_default_budget_template__fkey");
        });

        modelBuilder.Entity<LiteLLMCustomerTeamTable>(entity =>
        {
            entity.HasKey(e => e.CustomerTeamId).HasName("LiteLLM_CustomerTeamTable_pkey");

            entity.ToTable("LiteLLM_CustomerTeamTable");

            entity.HasIndex(e => e.ParentOrgId, "LiteLLM_CustomerTeamTable_parent_org_id_idx");

            entity.HasIndex(e => e.Spend, "LiteLLM_CustomerTeamTable_spend_idx");

            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.CustomerTeamId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("customer_team_id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.ParentOrgId).HasColumnName("parent_org_id");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.BudgetOverrideId).HasColumnName("budget_override_id");
            entity.Property(e => e.BudgetOverrideExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_override_expires_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ParentOrg).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ParentOrgId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_CustomerTeamTable_parent_org_id_fkey");

            entity.HasOne(d => d.Budget).WithMany(p => p.CustomerTeams)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_CustomerTeamTable_budget_id_fkey");

            entity.HasOne(d => d.BudgetOverride).WithMany(p => p.CustomerTeamOverrides)
                .HasForeignKey(d => d.BudgetOverrideId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("LiteLLM_CustomerTeamTable_budget_override_id_fkey");
        });

        modelBuilder.Entity<LiteLLMCustomerMemberTable>(entity =>
        {
            entity.HasKey(e => e.CustomerMemberId).HasName("LiteLLM_CustomerMemberTable_pkey");

            entity.ToTable("LiteLLM_CustomerMemberTable");

            entity.HasIndex(e => e.ParentOrgId, "LiteLLM_CustomerMemberTable_parent_org_id_idx");

            entity.HasIndex(e => e.Spend, "LiteLLM_CustomerMemberTable_spend_idx");

            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.CustomerMemberId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("customer_member_id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.ParentOrgId).HasColumnName("parent_org_id");
            entity.Property(e => e.ParentTeamIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("parent_team_ids");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.BudgetOverrideId).HasColumnName("budget_override_id");
            entity.Property(e => e.BudgetOverrideExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_override_expires_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ParentOrg).WithMany(p => p.Members)
                .HasForeignKey(d => d.ParentOrgId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_CustomerMemberTable_parent_org_id_fkey");

            entity.HasOne(d => d.Budget).WithMany(p => p.CustomerMembers)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("LiteLLM_CustomerMemberTable_budget_id_fkey");

            entity.HasOne(d => d.BudgetOverride).WithMany(p => p.CustomerMemberOverrides)
                .HasForeignKey(d => d.BudgetOverrideId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("LiteLLM_CustomerMemberTable_budget_override_id_fkey");
        });

        modelBuilder.Entity<LiteLLMCustomerBudgetIncreaseRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("LiteLLM_CustomerBudgetIncreaseRequest_pkey");

            entity.ToTable("LiteLLM_CustomerBudgetIncreaseRequest");

            entity.HasIndex(e => e.Status, "LiteLLM_CustomerBudgetIncreaseRequest_status_idx");

            entity.HasIndex(e => e.CustomerMemberId, "LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_idx");

            entity.Property(e => e.RequestId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("request_id");
            entity.Property(e => e.CustomerMemberId).HasColumnName("customer_member_id");
            entity.Property(e => e.RequestedMaxBudget).HasColumnName("requested_max_budget");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending_review")
                .HasColumnName("status");
            entity.Property(e => e.SubmittedBy).HasColumnName("submitted_by");
            entity.Property(e => e.SubmittedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("submitted_at");
            entity.Property(e => e.ReviewedBy).HasColumnName("reviewed_by");
            entity.Property(e => e.ReviewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reviewed_at");
            entity.Property(e => e.ReviewNotes).HasColumnName("review_notes");

            entity.HasOne(d => d.CustomerMember).WithMany(p => p.BudgetIncreaseRequests)
                .HasForeignKey(d => d.CustomerMemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_fkey");
        });

        modelBuilder.Entity<LiteLLMCredentialsTable>(entity =>
        {
            entity.HasKey(e => e.CredentialId).HasName("LiteLLM_CredentialsTable_pkey");

            entity.ToTable("LiteLLM_CredentialsTable");

            entity.HasIndex(e => e.CredentialName, "LiteLLM_CredentialsTable_credential_name_key").IsUnique();

            entity.Property(e => e.CredentialId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("credential_id");
            entity.Property(e => e.CredentialName).HasColumnName("credential_name");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.CredentialValues)
                .HasColumnType("jsonb")
                .HasColumnName("credential_values");
            entity.Property(e => e.CredentialInfo)
                .HasColumnType("jsonb")
                .HasColumnName("credential_info");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMProxyModelTable>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("LiteLLM_ProxyModelTable_pkey");

            entity.ToTable("LiteLLM_ProxyModelTable");

            entity.Property(e => e.ModelId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("model_id");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.ModelInfo)
                .HasColumnType("jsonb")
                .HasColumnName("model_info");
            entity.Property(e => e.Blocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("blocked");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMAgentsTable>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("LiteLLM_AgentsTable_pkey");

            entity.ToTable("LiteLLM_AgentsTable");

            entity.HasIndex(e => e.AgentName, "LiteLLM_AgentsTable_agent_name_key").IsUnique();

            entity.Property(e => e.AgentId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("agent_id");
            entity.Property(e => e.AgentName).HasColumnName("agent_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.AgentCardParams)
                .HasColumnType("jsonb")
                .HasColumnName("agent_card_params");
            entity.Property(e => e.StaticHeaders)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("static_headers");
            entity.Property(e => e.ExtraHeaders)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("extra_headers");
            entity.Property(e => e.KillSwitch)
                .HasColumnType("jsonb")
                .HasColumnName("kill_switch");
            entity.Property(e => e.AgentAccessGroups)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("agent_access_groups");
            entity.Property(e => e.AccessGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_group_ids");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.IdentityManaged)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("identity_managed");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.ExecutionMode)
                .ValueGeneratedNever()
                .HasDefaultValue("autonomous")
                .HasColumnName("execution_mode");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.SessionTpmLimit).HasColumnName("session_tpm_limit");
            entity.Property(e => e.SessionRpmLimit).HasColumnName("session_rpm_limit");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.AgentsTable)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_AgentsTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMAgentIdentity>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("LiteLLM_AgentIdentity_pkey");

            entity.ToTable("LiteLLM_AgentIdentity");

            entity.HasIndex(e => new { e.Provider, e.TenantId, e.ClientId }, "LiteLLM_AgentIdentity_provider_tenant_id_client_id_key").IsUnique();

            entity.HasIndex(e => new { e.Issuer, e.ServicePrincipalId }, "LiteLLM_AgentIdentity_issuer_service_principal_id_key").IsUnique();

            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.Active)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ServicePrincipalId).HasColumnName("service_principal_id");
            entity.Property(e => e.RequiredRoles)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("required_roles");
            entity.Property(e => e.RequiredScopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string> { "user_impersonation" })
                .HasColumnName("required_scopes");
            entity.Property(e => e.Revision)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("revision");
            entity.Property(e => e.LastAuthenticatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_authenticated_at");

            entity.HasOne(d => d.Agent).WithOne(p => p.Identity)
                .HasForeignKey<LiteLLMAgentIdentity>(d => d.AgentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_AgentIdentity_agent_id_fkey");
        });

        modelBuilder.Entity<LiteLLMRetiredAgentIdentity>(entity =>
        {
            entity.HasKey(e => e.BindingId).HasName("LiteLLM_RetiredAgentIdentity_pkey");

            entity.ToTable("LiteLLM_RetiredAgentIdentity");

            entity.HasIndex(e => new { e.Provider, e.TenantId, e.ClientId }, "LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key").IsUnique();

            entity.Property(e => e.BindingId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("binding_id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");

            entity.HasOne(d => d.Agent).WithMany(p => p.RetiredIdentities)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_RetiredAgentIdentity_agent_id_fkey");
        });

        modelBuilder.Entity<LiteLLMRetiredAgent>(entity =>
        {
            entity.HasKey(e => e.OriginalAgentId).HasName("LiteLLM_RetiredAgent_pkey");

            entity.ToTable("LiteLLM_RetiredAgent");

            entity.Property(e => e.OriginalAgentId).HasColumnName("original_agent_id");
            entity.Property(e => e.RetiredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("retired_at");
        });

        modelBuilder.Entity<LiteLLMVerifiedSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("LiteLLM_VerifiedSubject_pkey");

            entity.ToTable("LiteLLM_VerifiedSubject");

            entity.HasIndex(e => e.UserId, "LiteLLM_VerifiedSubject_user_id_idx");

            entity.HasIndex(e => new { e.Issuer, e.TenantId, e.Oid }, "LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key").IsUnique();

            entity.Property(e => e.SubjectId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("subject_id");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.Oid).HasColumnName("oid");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue("human")
                .HasColumnName("kind");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VerifiedVia)
                .ValueGeneratedNever()
                .HasDefaultValue("sso_interactive")
                .HasColumnName("verified_via");
            entity.Property(e => e.VerifiedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("verified_at");

            entity.HasOne(d => d.User).WithMany(p => p.VerifiedSubjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_VerifiedSubject_user_id_fkey");
        });

        modelBuilder.Entity<LiteLLMOrganizationTable>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("LiteLLM_OrganizationTable_pkey");

            entity.ToTable("LiteLLM_OrganizationTable");

            entity.Property(e => e.OrganizationId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("organization_id");
            entity.Property(e => e.OrganizationAlias).HasColumnName("organization_alias");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.Organization)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_OrganizationTable_budget_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_OrganizationTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMModelTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ModelTable_pkey");

            entity.ToTable("LiteLLM_ModelTable");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.ModelAliases)
                .HasColumnType("jsonb")
                .HasColumnName("aliases");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMTeamTable>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("LiteLLM_TeamTable_pkey");

            entity.ToTable("LiteLLM_TeamTable");

            entity.HasIndex(e => e.ModelId, "LiteLLM_TeamTable_model_id_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "LiteLLM_TeamTable_organization_id_idx");

            entity.HasIndex(e => e.TeamAlias, "LiteLLM_TeamTable_team_alias_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_TeamTable_created_at_idx");

            entity.Property(e => e.TeamId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("team_id");
            entity.Property(e => e.TeamAlias).HasColumnName("team_alias");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.Admins).HasColumnName("admins");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(e => e.MembersWithRoles)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("members_with_roles");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.SoftBudget).HasColumnName("soft_budget");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.TpdLimit).HasColumnName("tpd_limit");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.Blocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("blocked");
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
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelMaxBudget)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.RouterSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("router_settings");
            entity.Property(e => e.TeamMemberPermissions)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("team_member_permissions");
            entity.Property(e => e.AccessGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_group_ids");
            entity.Property(e => e.Policies)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("policies");
            entity.Property(e => e.DefaultTeamMemberModels)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("default_team_member_models");
            entity.Property(e => e.BudgetLimits)
                .HasColumnType("jsonb")
                .HasColumnName("budget_limits");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.AllowTeamGuardrailConfig)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_team_guardrail_config");

            entity.HasOne(d => d.LitellmOrganizationTable).WithMany(p => p.Teams)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_TeamTable_organization_id_fkey");

            entity.HasOne(d => d.LitellmModelTable).WithOne(p => p.Team)
                .HasForeignKey<LiteLLMTeamTable>(d => d.ModelId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_TeamTable_model_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_TeamTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMProjectTable>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("LiteLLM_ProjectTable_pkey");

            entity.ToTable("LiteLLM_ProjectTable");

            entity.Property(e => e.ProjectId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("project_id");
            entity.Property(e => e.ProjectAlias).HasColumnName("project_alias");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelRpmLimit)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_rpm_limit");
            entity.Property(e => e.ModelTpmLimit)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_tpm_limit");
            entity.Property(e => e.Blocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("blocked");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.LitellmTeamTable).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_ProjectTable_team_id_fkey");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.Projects)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_ProjectTable_budget_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_ProjectTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMDeletedTeamTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DeletedTeamTable_pkey");

            entity.ToTable("LiteLLM_DeletedTeamTable");

            entity.HasIndex(e => e.TeamId, "LiteLLM_DeletedTeamTable_team_id_idx");

            entity.HasIndex(e => e.DeletedAt, "LiteLLM_DeletedTeamTable_deleted_at_idx");

            entity.HasIndex(e => e.OrganizationId, "LiteLLM_DeletedTeamTable_organization_id_idx");

            entity.HasIndex(e => e.TeamAlias, "LiteLLM_DeletedTeamTable_team_alias_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_DeletedTeamTable_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamAlias).HasColumnName("team_alias");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.Admins).HasColumnName("admins");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(e => e.MembersWithRoles)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("members_with_roles");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.SoftBudget).HasColumnName("soft_budget");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.TpdLimit).HasColumnName("tpd_limit");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.Blocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("blocked");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelMaxBudget)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.RouterSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("router_settings");
            entity.Property(e => e.TeamMemberPermissions)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("team_member_permissions");
            entity.Property(e => e.AccessGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_group_ids");
            entity.Property(e => e.Policies)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("policies");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.AllowTeamGuardrailConfig)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_team_guardrail_config");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DeletedByApiKey).HasColumnName("deleted_by_api_key");
            entity.Property(e => e.LitellmChangedBy).HasColumnName("litellm_changed_by");
        });

        modelBuilder.Entity<LiteLLMUserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("LiteLLM_UserTable_pkey");

            entity.ToTable("LiteLLM_UserTable");

            entity.HasIndex(e => e.SsoUserId, "LiteLLM_UserTable_sso_user_id_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserAlias).HasColumnName("user_alias");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.SsoUserId).HasColumnName("sso_user_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PasswordResetRequired).HasColumnName("password_reset_required");
            entity.Property(e => e.LastBreachCheckAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_breach_check_at");
            entity.Property(e => e.Teams)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("teams");
            entity.Property(e => e.UserRole).HasColumnName("user_role");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.AllowedCacheControls)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_cache_controls");
            entity.Property(e => e.Policies)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("policies");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelMaxBudget)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.LitellmOrganizationTable).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_UserTable_organization_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.Users)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_UserTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMObjectPermissionTable>(entity =>
        {
            entity.HasKey(e => e.ObjectPermissionId).HasName("LiteLLM_ObjectPermissionTable_pkey");

            entity.ToTable("LiteLLM_ObjectPermissionTable");

            entity.Property(e => e.ObjectPermissionId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("object_permission_id");
            entity.Property(e => e.McpServers)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("mcp_servers");
            entity.Property(e => e.McpAccessGroups)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("mcp_access_groups");
            entity.Property(e => e.McpToolPermissions)
                .HasColumnType("jsonb")
                .HasColumnName("mcp_tool_permissions");
            entity.Property(e => e.VectorStores)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("vector_stores");
            entity.Property(e => e.Agents)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("agents");
            entity.Property(e => e.AgentAccessGroups)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("agent_access_groups");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("models");
            entity.Property(e => e.BlockedTools)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("blocked_tools");
            entity.Property(e => e.McpToolsets)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("mcp_toolsets");
            entity.Property(e => e.SearchTools)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("search_tools");
            entity.Property(e => e.McpToolSearchEnabled).HasColumnName("mcp_tool_search_enabled");
            entity.Property(e => e.Skills)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("skills");
        });

        modelBuilder.Entity<LiteLLMMCPServerTable>(entity =>
        {
            entity.HasKey(e => e.ServerId).HasName("LiteLLM_MCPServerTable_pkey");

            entity.ToTable("LiteLLM_MCPServerTable");

            entity.HasIndex(e => e.ApprovalStatus, "LiteLLM_MCPServerTable_approval_status_idx");

            entity.Property(e => e.ServerId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("server_id");
            entity.Property(e => e.ServerName).HasColumnName("server_name");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.SpecPath).HasColumnName("spec_path");
            entity.Property(e => e.Transport)
                .ValueGeneratedNever()
                .HasDefaultValue("sse")
                .HasColumnName("transport");
            entity.Property(e => e.AuthType).HasColumnName("auth_type");
            entity.Property(e => e.Credentials)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("credentials");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.McpInfo)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("mcp_info");
            entity.Property(e => e.McpAccessGroups).HasColumnName("mcp_access_groups");
            entity.Property(e => e.AllowedTools)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_tools");
            entity.Property(e => e.ToolNameToDisplayName)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("tool_name_to_display_name");
            entity.Property(e => e.ToolNameToDescription)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("tool_name_to_description");
            entity.Property(e => e.PinnedTools)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("pinned_tools");
            entity.Property(e => e.ExtraHeaders)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("extra_headers");
            entity.Property(e => e.StaticHeaders)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("static_headers");
            entity.Property(e => e.EnvVars)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("env_vars");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("unknown")
                .HasColumnName("status");
            entity.Property(e => e.LastHealthCheck)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_health_check");
            entity.Property(e => e.HealthCheckError).HasColumnName("health_check_error");
            entity.Property(e => e.Command).HasColumnName("command");
            entity.Property(e => e.Args)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("args");
            entity.Property(e => e.Env)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("env");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.AuthorizationUrl).HasColumnName("authorization_url");
            entity.Property(e => e.TokenUrl).HasColumnName("token_url");
            entity.Property(e => e.RegistrationUrl).HasColumnName("registration_url");
            entity.Property(e => e.Oauth2Flow).HasColumnName("oauth2_flow");
            entity.Property(e => e.TokenExchangeEndpoint).HasColumnName("token_exchange_endpoint");
            entity.Property(e => e.Audience).HasColumnName("audience");
            entity.Property(e => e.SubjectTokenType).HasColumnName("subject_token_type");
            entity.Property(e => e.TokenExchangeProfile).HasColumnName("token_exchange_profile");
            entity.Property(e => e.AllowAllKeys)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_all_keys");
            entity.Property(e => e.AvailableOnPublicInternet)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("available_on_public_internet");
            entity.Property(e => e.DelegateAuthToUpstream)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("delegate_auth_to_upstream");
            entity.Property(e => e.OauthPassthrough)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("oauth_passthrough");
            entity.Property(e => e.DcrBridge).HasColumnName("dcr_bridge");
            entity.Property(e => e.PerServerOauthDiscovery)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("per_server_oauth_discovery");
            entity.Property(e => e.IsByok)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_byok");
            entity.Property(e => e.ByokDescription)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("byok_description");
            entity.Property(e => e.ByokApiKeyHelpUrl).HasColumnName("byok_api_key_help_url");
            entity.Property(e => e.SourceUrl).HasColumnName("source_url");
            entity.Property(e => e.Timeout).HasColumnName("timeout");
            entity.Property(e => e.MaxConcurrentRequests).HasColumnName("max_concurrent_requests");
            entity.Property(e => e.Rpm).HasColumnName("rpm");
            entity.Property(e => e.ApprovalStatus)
                .ValueGeneratedNever()
                .HasDefaultValue("active")
                .HasColumnName("approval_status");
            entity.Property(e => e.SubmittedBy).HasColumnName("submitted_by");
            entity.Property(e => e.SubmittedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("submitted_at");
            entity.Property(e => e.ReviewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reviewed_at");
            entity.Property(e => e.ReviewNotes).HasColumnName("review_notes");
        });

        modelBuilder.Entity<LiteLLMMCPToolsetTable>(entity =>
        {
            entity.HasKey(e => e.ToolsetId).HasName("LiteLLM_MCPToolsetTable_pkey");

            entity.ToTable("LiteLLM_MCPToolsetTable");

            entity.HasIndex(e => e.ToolsetName, "LiteLLM_MCPToolsetTable_toolset_name_key").IsUnique();

            entity.Property(e => e.ToolsetId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("toolset_id");
            entity.Property(e => e.ToolsetName).HasColumnName("toolset_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Tools)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("tools");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMMCPUserCredentials>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_MCPUserCredentials_pkey");

            entity.ToTable("LiteLLM_MCPUserCredentials");

            entity.HasIndex(e => new { e.UserId, e.ServerId }, "LiteLLM_MCPUserCredentials_user_id_server_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ServerId).HasColumnName("server_id");
            entity.Property(e => e.CredentialB64).HasColumnName("credential_b64");
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

        modelBuilder.Entity<LiteLLMUserProviderCredentials>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_UserProviderCredentials_pkey");

            entity.ToTable("LiteLLM_UserProviderCredentials");

            entity.HasIndex(e => e.CredentialName, "LiteLLM_UserProviderCredentials_credential_name_idx");

            entity.HasIndex(e => new { e.UserId, e.CredentialName }, "LiteLLM_UserProviderCredentials_user_id_credential_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CredentialName).HasColumnName("credential_name");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.CredentialB64).HasColumnName("credential_b64");
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

        modelBuilder.Entity<LiteLLMMCPUserEnvVars>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_MCPUserEnvVars_pkey");

            entity.ToTable("LiteLLM_MCPUserEnvVars");

            entity.HasIndex(e => e.UserId, "LiteLLM_MCPUserEnvVars_user_id_idx");

            entity.HasIndex(e => e.ServerId, "LiteLLM_MCPUserEnvVars_server_id_idx");

            entity.HasIndex(e => new { e.UserId, e.ServerId }, "LiteLLM_MCPUserEnvVars_user_id_server_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ServerId).HasColumnName("server_id");
            entity.Property(e => e.ValuesB64).HasColumnName("values_b64");
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

        modelBuilder.Entity<LiteLLMMCPServerOAuthClient>(entity =>
        {
            entity.HasKey(e => e.ServerId).HasName("LiteLLM_MCPServerOAuthClient_pkey");

            entity.ToTable("LiteLLM_MCPServerOAuthClient");

            entity.Property(e => e.ServerId).HasColumnName("server_id");
            entity.Property(e => e.Credentials)
                .HasColumnType("jsonb")
                .HasColumnName("credentials");
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

        modelBuilder.Entity<LiteLLMSSOIdentityAssertion>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("LiteLLM_SSOIdentityAssertion_pkey");

            entity.ToTable("LiteLLM_SSOIdentityAssertion");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AssertionB64).HasColumnName("assertion_b64");
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

        modelBuilder.Entity<LiteLLMVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("LiteLLM_VerificationToken_pkey");

            entity.ToTable("LiteLLM_VerificationToken");

            entity.HasIndex(e => new { e.UserId, e.TeamId }, "LiteLLM_VerificationToken_user_id_team_id_idx");

            entity.HasIndex(e => e.TeamId, "LiteLLM_VerificationToken_team_id_idx");

            entity.HasIndex(e => new { e.BudgetResetAt, e.Expires }, "LiteLLM_VerificationToken_budget_reset_at_expires_idx");

            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.KeyName).HasColumnName("key_name");
            entity.Property(e => e.KeyAlias).HasColumnName("key_alias");
            entity.Property(e => e.SoftBudgetCooldown)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("soft_budget_cooldown");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.TotalSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("total_spend");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Aliases)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("aliases");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.RouterSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("router_settings");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Permissions)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("permissions");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Blocked).HasColumnName("blocked");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.TpdLimit).HasColumnName("tpd_limit");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.AllowedCacheControls)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_cache_controls");
            entity.Property(e => e.AllowedRoutes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_routes");
            entity.Property(e => e.KeyType).HasColumnName("key_type");
            entity.Property(e => e.Policies)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("policies");
            entity.Property(e => e.AccessGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_group_ids");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelMaxBudget)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.BudgetFallbacks)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("budget_fallbacks");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.SettingsUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("settings_updated_at");
            entity.Property(e => e.LastActive)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_active");
            entity.Property(e => e.RotationCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("rotation_count");
            entity.Property(e => e.AutoRotate)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("auto_rotate");
            entity.Property(e => e.RotationInterval).HasColumnName("rotation_interval");
            entity.Property(e => e.LastRotationAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_rotation_at");
            entity.Property(e => e.KeyRotationAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("key_rotation_at");
            entity.Property(e => e.BudgetLimits)
                .HasColumnType("jsonb")
                .HasColumnName("budget_limits");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.Keys)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_VerificationToken_budget_id_fkey");

            entity.HasOne(d => d.LitellmOrganizationTable).WithMany(p => p.Keys)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_VerificationToken_organization_id_fkey");

            entity.HasOne(d => d.LitellmProjectTable).WithMany(p => p.Keys)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_VerificationToken_project_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.VerificationTokens)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_VerificationToken_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMJWTKeyMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_JWTKeyMapping_pkey");

            entity.ToTable("LiteLLM_JWTKeyMapping");

            entity.HasIndex(e => new { e.JwtIssuer, e.JwtClaimName, e.JwtClaimValue, e.IsActive }, "LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_idx");

            entity.HasIndex(e => new { e.JwtIssuer, e.JwtClaimName, e.JwtClaimValue }, "LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.JwtIssuer)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("jwt_issuer");
            entity.Property(e => e.JwtClaimName).HasColumnName("jwt_claim_name");
            entity.Property(e => e.JwtClaimValue).HasColumnName("jwt_claim_value");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.LitellmVerificationToken).WithMany(p => p.JwtKeyMappings)
                .HasForeignKey(d => d.Token)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_JWTKeyMapping_token_fkey");
        });

        modelBuilder.Entity<LiteLLMDeprecatedVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DeprecatedVerificationToken_pkey");

            entity.ToTable("LiteLLM_DeprecatedVerificationToken");

            entity.HasIndex(e => new { e.Token, e.RevokeAt }, "LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx");

            entity.HasIndex(e => e.RevokeAt, "LiteLLM_DeprecatedVerificationToken_revoke_at_idx");

            entity.HasIndex(e => e.Token, "LiteLLM_DeprecatedVerificationToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ActiveTokenId).HasColumnName("active_token_id");
            entity.Property(e => e.RevokeAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoke_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMDeletedVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DeletedVerificationToken_pkey");

            entity.ToTable("LiteLLM_DeletedVerificationToken");

            entity.HasIndex(e => e.Token, "LiteLLM_DeletedVerificationToken_token_idx");

            entity.HasIndex(e => e.DeletedAt, "LiteLLM_DeletedVerificationToken_deleted_at_idx");

            entity.HasIndex(e => e.UserId, "LiteLLM_DeletedVerificationToken_user_id_idx");

            entity.HasIndex(e => e.TeamId, "LiteLLM_DeletedVerificationToken_team_id_idx");

            entity.HasIndex(e => e.OrganizationId, "LiteLLM_DeletedVerificationToken_organization_id_idx");

            entity.HasIndex(e => e.KeyAlias, "LiteLLM_DeletedVerificationToken_key_alias_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_DeletedVerificationToken_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.KeyName).HasColumnName("key_name");
            entity.Property(e => e.KeyAlias).HasColumnName("key_alias");
            entity.Property(e => e.SoftBudgetCooldown)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("soft_budget_cooldown");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.TotalSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("total_spend");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Aliases)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("aliases");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Permissions)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("permissions");
            entity.Property(e => e.MaxParallelRequests).HasColumnName("max_parallel_requests");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Blocked).HasColumnName("blocked");
            entity.Property(e => e.TpmLimit).HasColumnName("tpm_limit");
            entity.Property(e => e.RpmLimit).HasColumnName("rpm_limit");
            entity.Property(e => e.TpdLimit).HasColumnName("tpd_limit");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.BudgetDuration).HasColumnName("budget_duration");
            entity.Property(e => e.BudgetResetAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("budget_reset_at");
            entity.Property(e => e.AllowedCacheControls)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_cache_controls");
            entity.Property(e => e.AllowedRoutes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowed_routes");
            entity.Property(e => e.KeyType).HasColumnName("key_type");
            entity.Property(e => e.Policies)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("policies");
            entity.Property(e => e.AccessGroupIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_group_ids");
            entity.Property(e => e.ModelSpend)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_spend");
            entity.Property(e => e.ModelMaxBudget)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("model_max_budget");
            entity.Property(e => e.BudgetFallbacks)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("budget_fallbacks");
            entity.Property(e => e.RouterSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("router_settings");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.SettingsUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("settings_updated_at");
            entity.Property(e => e.LastActive)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_active");
            entity.Property(e => e.RotationCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("rotation_count");
            entity.Property(e => e.AutoRotate)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("auto_rotate");
            entity.Property(e => e.RotationInterval).HasColumnName("rotation_interval");
            entity.Property(e => e.LastRotationAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_rotation_at");
            entity.Property(e => e.KeyRotationAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("key_rotation_at");
            entity.Property(e => e.DeletedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DeletedByApiKey).HasColumnName("deleted_by_api_key");
            entity.Property(e => e.LitellmChangedBy).HasColumnName("litellm_changed_by");
        });

        modelBuilder.Entity<LiteLLMEndUserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("LiteLLM_EndUserTable_pkey");

            entity.ToTable("LiteLLM_EndUserTable");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.AllowedModelRegion).HasColumnName("allowed_model_region");
            entity.Property(e => e.DefaultModel).HasColumnName("default_model");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("models");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.ObjectPermissionId).HasColumnName("object_permission_id");
            entity.Property(e => e.Blocked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("blocked");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.EndUsers)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_EndUserTable_budget_id_fkey");

            entity.HasOne(d => d.ObjectPermission).WithMany(p => p.EndUsers)
                .HasForeignKey(d => d.ObjectPermissionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_EndUserTable_object_permission_id_fkey");
        });

        modelBuilder.Entity<LiteLLMModelAccessGroupBudgetTable>(entity =>
        {
            entity.HasKey(e => e.AccessGroupName).HasName("LiteLLM_ModelAccessGroupBudgetTable_pkey");

            entity.ToTable("LiteLLM_ModelAccessGroupBudgetTable");

            entity.Property(e => e.AccessGroupName).HasColumnName("access_group_name");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.ModelAccessGroups)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_ModelAccessGroupBudgetTable_budget_id_fkey");
        });

        modelBuilder.Entity<LiteLLMTagTable>(entity =>
        {
            entity.HasKey(e => e.TagName).HasName("LiteLLM_TagTable_pkey");

            entity.ToTable("LiteLLM_TagTable");

            entity.Property(e => e.TagName).HasColumnName("tag_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.ModelInfo)
                .HasColumnType("jsonb")
                .HasColumnName("model_info");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.Tags)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_TagTable_budget_id_fkey");
        });

        modelBuilder.Entity<LiteLLMConfig>(entity =>
        {
            entity.HasKey(e => e.ParamName).HasName("LiteLLM_Config_pkey");

            entity.ToTable("LiteLLM_Config");

            entity.Property(e => e.ParamName).HasColumnName("param_name");
            entity.Property(e => e.ParamValue)
                .HasColumnType("jsonb")
                .HasColumnName("param_value");
            entity.Property(e => e.LastRunAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_run_at");
            entity.Property(e => e.ReloadRevision)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("reload_revision");
        });

        modelBuilder.Entity<LiteLLMSpendLogs>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("LiteLLM_SpendLogs_pkey");

            entity.ToTable("LiteLLM_SpendLogs");

            entity.HasIndex(e => e.StartTime, "LiteLLM_SpendLogs_startTime_idx");

            entity.HasIndex(e => new { e.StartTime, e.RequestId }, "LiteLLM_SpendLogs_startTime_request_id_idx");

            entity.HasIndex(e => e.EndUser, "LiteLLM_SpendLogs_end_user_idx");

            entity.HasIndex(e => e.SessionId, "LiteLLM_SpendLogs_session_id_idx");

            entity.HasIndex(e => e.LitellmCallId, "LiteLLM_SpendLogs_litellm_call_id_idx");

            entity.HasIndex(e => new { e.ApiKey, e.StartTime }, "LiteLLM_SpendLogs_api_key_startTime_idx");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.CallType).HasColumnName("call_type");
            entity.Property(e => e.ApiKey)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("api_key");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.TotalTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("total_tokens");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.RequestDurationMs).HasColumnName("request_duration_ms");
            entity.Property(e => e.CompletionStartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completionStartTime");
            entity.Property(e => e.Model)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model");
            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_id");
            entity.Property(e => e.ModelGroup)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("custom_llm_provider");
            entity.Property(e => e.ApiBase)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("api_base");
            entity.Property(e => e.User)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("user");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CacheHit)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("cache_hit");
            entity.Property(e => e.CacheKey)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("cache_key");
            entity.Property(e => e.RequestTags)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("request_tags");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.EndUser).HasColumnName("end_user");
            entity.Property(e => e.RequesterIpAddress).HasColumnName("requester_ip_address");
            entity.Property(e => e.Messages)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("messages");
            entity.Property(e => e.Response)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("response");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.BillingAgentId).HasColumnName("billing_agent_id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.ProxyServerRequest)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("proxy_server_request");
            entity.Property(e => e.LitellmCallId).HasColumnName("litellm_call_id");
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

        modelBuilder.Entity<LiteLLMBudgetWindowSpend>(entity =>
        {
            entity.HasKey(e => new { e.EntityType, e.EntityId, e.WindowDuration }).HasName("LiteLLM_BudgetWindowSpend_pkey");

            entity.ToTable("LiteLLM_BudgetWindowSpend");

            entity.Property(e => e.EntityType).HasColumnName("entity_type");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.WindowDuration).HasColumnName("window_duration");
            entity.Property(e => e.WindowStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("window_start");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
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
        });

        modelBuilder.Entity<LiteLLMErrorLogs>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("LiteLLM_ErrorLogs_pkey");

            entity.ToTable("LiteLLM_ErrorLogs");

            entity.Property(e => e.RequestId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("request_id");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startTime");
            entity.Property(e => e.EndTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endTime");
            entity.Property(e => e.ApiBase)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("api_base");
            entity.Property(e => e.ModelGroup)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_group");
            entity.Property(e => e.LitellmModelName)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("litellm_model_name");
            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_id");
            entity.Property(e => e.RequestKwargs)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("request_kwargs");
            entity.Property(e => e.ExceptionType)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("exception_type");
            entity.Property(e => e.ExceptionString)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("exception_string");
            entity.Property(e => e.StatusCode)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("status_code");
        });

        modelBuilder.Entity<LiteLLMUserNotifications>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("LiteLLM_UserNotifications_pkey");

            entity.ToTable("LiteLLM_UserNotifications");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Models).HasColumnName("models");
            entity.Property(e => e.Justification).HasColumnName("justification");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<LiteLLMTeamMembership>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TeamId }).HasName("LiteLLM_TeamMembership_pkey");

            entity.ToTable("LiteLLM_TeamMembership");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.TotalSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("total_spend");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.TeamMembership)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_TeamMembership_budget_id_fkey");
        });

        modelBuilder.Entity<LiteLLMOrganizationMembership>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.OrganizationId }).HasName("LiteLLM_OrganizationMembership_pkey");

            entity.ToTable("LiteLLM_OrganizationMembership");

            entity.HasIndex(e => new { e.UserId, e.OrganizationId }, "LiteLLM_OrganizationMembership_user_id_organization_id_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.UserRole).HasColumnName("user_role");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_OrganizationMembership_user_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_OrganizationMembership_organization_id_fkey");

            entity.HasOne(d => d.LitellmBudgetTable).WithMany(p => p.OrganizationMembership)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("LiteLLM_OrganizationMembership_budget_id_fkey");
        });

        modelBuilder.Entity<LiteLLMInvitationLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_InvitationLink_pkey");

            entity.ToTable("LiteLLM_InvitationLink");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.IsAccepted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_accepted");
            entity.Property(e => e.AcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accepted_at");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.LiteLLMUserTableUser).WithMany(p => p.InvitationsUser)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_InvitationLink_user_id_fkey");

            entity.HasOne(d => d.LiteLLMUserTableCreated).WithMany(p => p.InvitationsCreated)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_InvitationLink_created_by_fkey");

            entity.HasOne(d => d.LiteLLMUserTableUpdated).WithMany(p => p.InvitationsUpdated)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_InvitationLink_updated_by_fkey");
        });

        modelBuilder.Entity<LiteLLMAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_AuditLog_pkey");

            entity.ToTable("LiteLLM_AuditLog");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ChangedBy)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("changed_by");
            entity.Property(e => e.ChangedByApiKey)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("changed_by_api_key");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.TableName).HasColumnName("table_name");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.BeforeValue)
                .HasColumnType("jsonb")
                .HasColumnName("before_value");
            entity.Property(e => e.UpdatedValues)
                .HasColumnType("jsonb")
                .HasColumnName("updated_values");
        });

        modelBuilder.Entity<LiteLLMDailyUserSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyUserSpend_pkey");

            entity.ToTable("LiteLLM_DailyUserSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyUserSpend_date_idx");

            entity.HasIndex(e => new { e.UserId, e.Date }, "LiteLLM_DailyUserSpend_user_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyUserSpend_api_key_idx");

            entity.HasIndex(e => new { e.ApiKey, e.Date }, "LiteLLM_DailyUserSpend_api_key_date_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyUserSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyUserSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyUserSpend_endpoint_idx");

            entity.HasIndex(e => new { e.UserId, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_ll_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyGlobalSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyGlobalSpend_pkey");

            entity.ToTable("LiteLLM_DailyGlobalSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyGlobalSpend_date_idx");

            entity.HasIndex(e => new { e.Date, e.Model, e.ModelGroup, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm__key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyOrganizationSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyOrganizationSpend_pkey");

            entity.ToTable("LiteLLM_DailyOrganizationSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyOrganizationSpend_date_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Date }, "LiteLLM_DailyOrganizationSpend_organization_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyOrganizationSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyOrganizationSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyOrganizationSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyOrganizationSpend_endpoint_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyCustomerMemberSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyCustomerMemberSpend_pkey");

            entity.ToTable("LiteLLM_DailyCustomerMemberSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyCustomerMemberSpend_date_idx");

            entity.HasIndex(e => new { e.CustomerOrgId, e.Date }, "LiteLLM_DailyCustomerMemberSpend_customer_org_id_date_idx");

            entity.HasIndex(e => new { e.CustomerTeamId, e.Date }, "LiteLLM_DailyCustomerMemberSpend_customer_team_id_date_idx");

            entity.HasIndex(e => new { e.CustomerMemberId, e.Date }, "LiteLLM_DailyCustomerMemberSpend_customer_member_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyCustomerMemberSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyCustomerMemberSpend_model_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.CustomerOrgId).HasColumnName("customer_org_id");
            entity.Property(e => e.CustomerTeamId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("customer_team_id");
            entity.Property(e => e.CustomerMemberId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("customer_member_id");
            entity.Property(e => e.Model)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model");
            entity.Property(e => e.ModelGroup)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("endpoint");
            entity.Property(e => e.ApiKey)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("api_key");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyEndUserSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyEndUserSpend_pkey");

            entity.ToTable("LiteLLM_DailyEndUserSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyEndUserSpend_date_idx");

            entity.HasIndex(e => new { e.EndUserId, e.Date }, "LiteLLM_DailyEndUserSpend_end_user_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyEndUserSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyEndUserSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyEndUserSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyEndUserSpend_endpoint_idx");

            entity.HasIndex(e => new { e.EndUserId, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_cu_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EndUserId).HasColumnName("end_user_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyAgentSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyAgentSpend_pkey");

            entity.ToTable("LiteLLM_DailyAgentSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyAgentSpend_date_idx");

            entity.HasIndex(e => new { e.AgentId, e.Date }, "LiteLLM_DailyAgentSpend_agent_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyAgentSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyAgentSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyAgentSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyAgentSpend_endpoint_idx");

            entity.HasIndex(e => new { e.AgentId, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom__key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyTeamSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyTeamSpend_pkey");

            entity.ToTable("LiteLLM_DailyTeamSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyTeamSpend_date_idx");

            entity.HasIndex(e => new { e.TeamId, e.Date }, "LiteLLM_DailyTeamSpend_team_id_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyTeamSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyTeamSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyTeamSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyTeamSpend_endpoint_idx");

            entity.HasIndex(e => new { e.TeamId, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_ll_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
            entity.Property(e => e.PtuFlatCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("ptu_flat_cost");
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
        });

        modelBuilder.Entity<LiteLLMDailyTagSpend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_DailyTagSpend_pkey");

            entity.ToTable("LiteLLM_DailyTagSpend");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyTagSpend_date_idx");

            entity.HasIndex(e => new { e.Tag, e.Date }, "LiteLLM_DailyTagSpend_tag_date_idx");

            entity.HasIndex(e => e.ApiKey, "LiteLLM_DailyTagSpend_api_key_idx");

            entity.HasIndex(e => e.Model, "LiteLLM_DailyTagSpend_model_idx");

            entity.HasIndex(e => e.McpNamespacedToolName, "LiteLLM_DailyTagSpend_mcp_namespaced_tool_name_idx");

            entity.HasIndex(e => e.Endpoint, "LiteLLM_DailyTagSpend_endpoint_idx");

            entity.HasIndex(e => new { e.Tag, e.Date, e.ApiKey, e.Model, e.CustomLlmProvider, e.McpNamespacedToolName, e.Endpoint }, "LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_pro_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Tag).HasColumnName("tag");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.McpNamespacedToolName).HasColumnName("mcp_namespaced_tool_name");
            entity.Property(e => e.Endpoint).HasColumnName("endpoint");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.CacheReadInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_read_input_tokens");
            entity.Property(e => e.CacheCreationInputTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("cache_creation_input_tokens");
            entity.Property(e => e.CompressionSavedTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("compression_saved_tokens");
            entity.Property(e => e.CompressionSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("compression_savings_spend");
            entity.Property(e => e.PromptCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("prompt_caching_savings_spend");
            entity.Property(e => e.GatewayInjectedCachingSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("gateway_injected_caching_savings_spend");
            entity.Property(e => e.AutorouterSavingsSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("autorouter_savings_spend");
            entity.Property(e => e.GuardrailSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("guardrail_spend");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.ApiRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("api_requests");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMProxyWorkerHeartbeat>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("LiteLLM_ProxyWorkerHeartbeat_pkey");

            entity.ToTable("LiteLLM_ProxyWorkerHeartbeat");

            entity.Property(e => e.WorkerId).HasColumnName("worker_id");
            entity.Property(e => e.Hostname).HasColumnName("hostname");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.LastHeartbeatAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_heartbeat_at");
        });

        modelBuilder.Entity<LiteLLMCronJob>(entity =>
        {
            entity.HasKey(e => e.CronjobId).HasName("LiteLLM_CronJob_pkey");

            entity.ToTable("LiteLLM_CronJob");

            entity.Property(e => e.CronjobId)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("cronjob_id");
            entity.Property(e => e.PodId).HasColumnName("pod_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(JobStatus.Inactive)
                .HasColumnName("status");
            entity.Property(e => e.LastUpdated)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Ttl)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ttl");
        });

        modelBuilder.Entity<LiteLLMScheduledJobRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ScheduledJobRun_pkey");

            entity.ToTable("LiteLLM_ScheduledJobRun");

            entity.HasIndex(e => new { e.JobName, e.Status }, "LiteLLM_ScheduledJobRun_job_name_status_idx");

            entity.HasIndex(e => new { e.LockGroup, e.Status }, "LiteLLM_ScheduledJobRun_lock_group_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.JobName).HasColumnName("job_name");
            entity.Property(e => e.LockGroup).HasColumnName("lock_group");
            entity.Property(e => e.Schedule).HasColumnName("schedule");
            entity.Property(e => e.ScheduledFor)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("scheduled_for");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(ScheduledJobRunStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ClaimedBy).HasColumnName("claimed_by");
            entity.Property(e => e.ClaimedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("claimed_at");
            entity.Property(e => e.LeaseExpiresAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("lease_expires_at");
            entity.Property(e => e.FinishedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("finished_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMManagedFileTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ManagedFileTable_pkey");

            entity.ToTable("LiteLLM_ManagedFileTable");

            entity.HasIndex(e => e.UnifiedFileId, "LiteLLM_ManagedFileTable_unified_file_id_key").IsUnique();

            entity.HasIndex(e => e.UnifiedFileId, "LiteLLM_ManagedFileTable_unified_file_id_idx");

            entity.HasIndex(e => e.FlatModelFileIds, "LiteLLM_ManagedFileTable_flat_model_file_ids_idx").HasMethod("gin");

            entity.HasIndex(e => new { e.TeamId, e.CreatedAt }, "LiteLLM_ManagedFileTable_team_id_created_at_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedAt }, "LiteLLM_ManagedFileTable_created_by_created_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UnifiedFileId).HasColumnName("unified_file_id");
            entity.Property(e => e.FileObject)
                .HasColumnType("jsonb")
                .HasColumnName("file_object");
            entity.Property(e => e.ModelMappings)
                .HasColumnType("jsonb")
                .HasColumnName("model_mappings");
            entity.Property(e => e.FlatModelFileIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("flat_model_file_ids");
            entity.Property(e => e.StorageBackend).HasColumnName("storage_backend");
            entity.Property(e => e.StorageUrl).HasColumnName("storage_url");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMManagedObjectTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ManagedObjectTable_pkey");

            entity.ToTable("LiteLLM_ManagedObjectTable");

            entity.HasIndex(e => e.UnifiedObjectId, "LiteLLM_ManagedObjectTable_unified_object_id_key").IsUnique();

            entity.HasIndex(e => e.ModelObjectId, "LiteLLM_ManagedObjectTable_model_object_id_key").IsUnique();

            entity.HasIndex(e => e.UnifiedObjectId, "LiteLLM_ManagedObjectTable_unified_object_id_idx");

            entity.HasIndex(e => e.ModelObjectId, "LiteLLM_ManagedObjectTable_model_object_id_idx");

            entity.HasIndex(e => new { e.TeamId, e.CreatedAt }, "LiteLLM_ManagedObjectTable_team_id_created_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UnifiedObjectId).HasColumnName("unified_object_id");
            entity.Property(e => e.ModelObjectId).HasColumnName("model_object_id");
            entity.Property(e => e.FileObject)
                .HasColumnType("jsonb")
                .HasColumnName("file_object");
            entity.Property(e => e.FilePurpose).HasColumnName("file_purpose");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.BatchProcessed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("batch_processed");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.OrgId).HasColumnName("org_id");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.RequestTags)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("request_tags");
            entity.Property(e => e.EndUserId).HasColumnName("end_user_id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMManagedFileContentTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ManagedFileContentTable_pkey");

            entity.ToTable("LiteLLM_ManagedFileContentTable");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMManagedVectorStoreTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ManagedVectorStoreTable_pkey");

            entity.ToTable("LiteLLM_ManagedVectorStoreTable");

            entity.HasIndex(e => e.UnifiedResourceId, "LiteLLM_ManagedVectorStoreTable_unified_resource_id_key").IsUnique();

            entity.HasIndex(e => e.UnifiedResourceId, "LiteLLM_ManagedVectorStoreTable_unified_resource_id_idx");

            entity.HasIndex(e => new { e.TeamId, e.CreatedAt }, "LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UnifiedResourceId).HasColumnName("unified_resource_id");
            entity.Property(e => e.ResourceObject)
                .HasColumnType("jsonb")
                .HasColumnName("resource_object");
            entity.Property(e => e.ModelMappings)
                .HasColumnType("jsonb")
                .HasColumnName("model_mappings");
            entity.Property(e => e.FlatModelResourceIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("flat_model_resource_ids");
            entity.Property(e => e.StorageBackend).HasColumnName("storage_backend");
            entity.Property(e => e.StorageUrl).HasColumnName("storage_url");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMManagedVectorStoresTable>(entity =>
        {
            entity.HasKey(e => e.VectorStoreId).HasName("LiteLLM_ManagedVectorStoresTable_pkey");

            entity.ToTable("LiteLLM_ManagedVectorStoresTable");

            entity.HasIndex(e => e.TeamId, "LiteLLM_ManagedVectorStoresTable_team_id_idx");

            entity.HasIndex(e => e.UserId, "LiteLLM_ManagedVectorStoresTable_user_id_idx");

            entity.Property(e => e.VectorStoreId).HasColumnName("vector_store_id");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.VectorStoreName).HasColumnName("vector_store_name");
            entity.Property(e => e.VectorStoreDescription).HasColumnName("vector_store_description");
            entity.Property(e => e.VectorStoreMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("vector_store_metadata");
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
            entity.Property(e => e.LitellmCredentialName).HasColumnName("litellm_credential_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<LiteLLMGuardrailsTable>(entity =>
        {
            entity.HasKey(e => e.GuardrailId).HasName("LiteLLM_GuardrailsTable_pkey");

            entity.ToTable("LiteLLM_GuardrailsTable");

            entity.HasIndex(e => e.GuardrailName, "LiteLLM_GuardrailsTable_guardrail_name_key").IsUnique();

            entity.HasIndex(e => e.Status, "LiteLLM_GuardrailsTable_status_idx");

            entity.Property(e => e.GuardrailId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("guardrail_id");
            entity.Property(e => e.GuardrailName).HasColumnName("guardrail_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.GuardrailInfo)
                .HasColumnType("jsonb")
                .HasColumnName("guardrail_info");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
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
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.SubmittedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("submitted_at");
            entity.Property(e => e.ReviewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reviewed_at");
        });

        modelBuilder.Entity<LiteLLMDailyGuardrailMetrics>(entity =>
        {
            entity.HasKey(e => new { e.GuardrailId, e.Date }).HasName("LiteLLM_DailyGuardrailMetrics_pkey");

            entity.ToTable("LiteLLM_DailyGuardrailMetrics");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyGuardrailMetrics_date_idx");

            entity.HasIndex(e => e.GuardrailId, "LiteLLM_DailyGuardrailMetrics_guardrail_id_idx");

            entity.Property(e => e.GuardrailId).HasColumnName("guardrail_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RequestsEvaluated)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("requests_evaluated");
            entity.Property(e => e.PassedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("passed_count");
            entity.Property(e => e.BlockedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("blocked_count");
            entity.Property(e => e.FlaggedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("flagged_count");
            entity.Property(e => e.AvgScore).HasColumnName("avg_score");
            entity.Property(e => e.AvgLatencyMs).HasColumnName("avg_latency_ms");
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
        });

        modelBuilder.Entity<LiteLLMDailyGuardrailUsageUnits>(entity =>
        {
            entity.HasKey(e => new { e.GuardrailId, e.Date, e.TeamId, e.ApiKey, e.UsageUnit }).HasName("LiteLLM_DailyGuardrailUsageUnits_pkey");

            entity.ToTable("LiteLLM_DailyGuardrailUsageUnits");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyGuardrailUsageUnits_date_idx");

            entity.Property(e => e.GuardrailId).HasColumnName("guardrail_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.UsageUnit).HasColumnName("usage_unit");
            entity.Property(e => e.Units)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("units");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.UntrackedUnits)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("untracked_units");
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
        });

        modelBuilder.Entity<LiteLLMDailyPolicyMetrics>(entity =>
        {
            entity.HasKey(e => new { e.PolicyId, e.Date }).HasName("LiteLLM_DailyPolicyMetrics_pkey");

            entity.ToTable("LiteLLM_DailyPolicyMetrics");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyPolicyMetrics_date_idx");

            entity.HasIndex(e => e.PolicyId, "LiteLLM_DailyPolicyMetrics_policy_id_idx");

            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RequestsEvaluated)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("requests_evaluated");
            entity.Property(e => e.PassedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("passed_count");
            entity.Property(e => e.BlockedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("blocked_count");
            entity.Property(e => e.FlaggedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("flagged_count");
            entity.Property(e => e.AvgScore).HasColumnName("avg_score");
            entity.Property(e => e.AvgLatencyMs).HasColumnName("avg_latency_ms");
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
        });

        modelBuilder.Entity<LiteLLMSpendLogGuardrailIndex>(entity =>
        {
            entity.HasKey(e => new { e.RequestId, e.GuardrailId }).HasName("LiteLLM_SpendLogGuardrailIndex_pkey");

            entity.ToTable("LiteLLM_SpendLogGuardrailIndex");

            entity.HasIndex(e => new { e.GuardrailId, e.StartTime }, "LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx");

            entity.HasIndex(e => new { e.PolicyId, e.StartTime }, "LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.GuardrailId).HasColumnName("guardrail_id");
            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<LiteLLMSpendLogToolIndex>(entity =>
        {
            entity.HasKey(e => new { e.RequestId, e.ToolName }).HasName("LiteLLM_SpendLogToolIndex_pkey");

            entity.ToTable("LiteLLM_SpendLogToolIndex");

            entity.HasIndex(e => new { e.ToolName, e.StartTime }, "LiteLLM_SpendLogToolIndex_tool_name_start_time_idx");

            entity.HasIndex(e => e.StartTime, "LiteLLM_SpendLogToolIndex_start_time_idx");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.ToolName).HasColumnName("tool_name");
            entity.Property(e => e.StartTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<LiteLLMDailyToolSpend>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.ToolName }).HasName("LiteLLM_DailyToolSpend_pkey");

            entity.ToTable("LiteLLM_DailyToolSpend");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ToolName).HasColumnName("tool_name");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.TotalTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_tokens");
            entity.Property(e => e.RequestCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("request_count");
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
        });

        modelBuilder.Entity<LiteLLMDailyModelUsage>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.ModelGroup, e.Model, e.CustomLlmProvider, e.TaskType }).HasName("LiteLLM_DailyModelUsage_pkey");

            entity.ToTable("LiteLLM_DailyModelUsage");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyModelUsage_date_idx");

            entity.HasIndex(e => e.ModelGroup, "LiteLLM_DailyModelUsage_model_group_idx");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ModelGroup).HasColumnName("model_group");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.TaskType).HasColumnName("task_type");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.PromptTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("prompt_tokens");
            entity.Property(e => e.CompletionTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("completion_tokens");
            entity.Property(e => e.RequestCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("request_count");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
            entity.Property(e => e.TotalResponseTimeMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_response_time_ms");
            entity.Property(e => e.TimedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("timed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyGatewayRequests>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.Category, e.Route }).HasName("LiteLLM_DailyGatewayRequests_pkey");

            entity.ToTable("LiteLLM_DailyGatewayRequests");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyGatewayRequests_date_idx");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.SuccessfulRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("successful_requests");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyGatewayFailedRequests>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.Category, e.Route, e.StatusCode }).HasName("LiteLLM_DailyGatewayFailedRequests_pkey");

            entity.ToTable("LiteLLM_DailyGatewayFailedRequests");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyGatewayFailedRequests_date_idx");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.StatusCode).HasColumnName("status_code");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
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
        });

        modelBuilder.Entity<LiteLLMDailyRequestErrors>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.ApiKey, e.TeamId, e.UserId, e.ModelGroup, e.StatusCode }).HasName("LiteLLM_DailyRequestErrors_pkey");

            entity.ToTable("LiteLLM_DailyRequestErrors");

            entity.HasIndex(e => e.Date, "LiteLLM_DailyRequestErrors_date_idx");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("team_id");
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("user_id");
            entity.Property(e => e.ModelGroup)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("model_group");
            entity.Property(e => e.StatusCode).HasColumnName("status_code");
            entity.Property(e => e.FailedRequests)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("failed_requests");
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
        });

        modelBuilder.Entity<LiteLLMPromptTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_PromptTable_pkey");

            entity.ToTable("LiteLLM_PromptTable");

            entity.HasIndex(e => new { e.PromptId, e.Environment }, "LiteLLM_PromptTable_prompt_id_environment_idx");

            entity.HasIndex(e => e.PromptId, "LiteLLM_PromptTable_prompt_id_idx");

            entity.HasIndex(e => new { e.PromptId, e.Version, e.Environment }, "LiteLLM_PromptTable_prompt_id_version_environment_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PromptId).HasColumnName("prompt_id");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("version");
            entity.Property(e => e.Environment)
                .ValueGeneratedNever()
                .HasDefaultValue("development")
                .HasColumnName("environment");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.PromptInfo)
                .HasColumnType("jsonb")
                .HasColumnName("prompt_info");
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
        });

        modelBuilder.Entity<LiteLLMHealthCheckTable>(entity =>
        {
            entity.HasKey(e => e.HealthCheckId).HasName("LiteLLM_HealthCheckTable_pkey");

            entity.ToTable("LiteLLM_HealthCheckTable");

            entity.HasIndex(e => e.ModelName, "LiteLLM_HealthCheckTable_model_name_idx");

            entity.HasIndex(e => e.CheckedAt, "LiteLLM_HealthCheckTable_checked_at_idx");

            entity.HasIndex(e => e.Status, "LiteLLM_HealthCheckTable_status_idx");

            entity.HasIndex(e => new { e.ModelId, e.ModelName, e.CheckedAt }, "LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx").IsDescending(false, false, true);

            entity.Property(e => e.HealthCheckId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("health_check_id");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.HealthyCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("healthy_count");
            entity.Property(e => e.UnhealthyCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unhealthy_count");
            entity.Property(e => e.ErrorMessage).HasColumnName("error_message");
            entity.Property(e => e.ResponseTimeMs).HasColumnName("response_time_ms");
            entity.Property(e => e.Details)
                .HasColumnType("jsonb")
                .HasColumnName("details");
            entity.Property(e => e.CheckedBy).HasColumnName("checked_by");
            entity.Property(e => e.CheckedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("checked_at");
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
        });

        modelBuilder.Entity<LiteLLMSearchToolsTable>(entity =>
        {
            entity.HasKey(e => e.SearchToolId).HasName("LiteLLM_SearchToolsTable_pkey");

            entity.ToTable("LiteLLM_SearchToolsTable");

            entity.HasIndex(e => e.SearchToolName, "LiteLLM_SearchToolsTable_search_tool_name_key").IsUnique();

            entity.Property(e => e.SearchToolId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("search_tool_id");
            entity.Property(e => e.SearchToolName).HasColumnName("search_tool_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.SearchToolInfo)
                .HasColumnType("jsonb")
                .HasColumnName("search_tool_info");
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
        });

        modelBuilder.Entity<LiteLLMSSOConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_SSOConfig_pkey");

            entity.ToTable("LiteLLM_SSOConfig");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("sso_config")
                .HasColumnName("id");
            entity.Property(e => e.SsoSettings)
                .HasColumnType("jsonb")
                .HasColumnName("sso_settings");
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
        });

        modelBuilder.Entity<LiteLLMManagedVectorStoreIndexTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ManagedVectorStoreIndexTable_pkey");

            entity.ToTable("LiteLLM_ManagedVectorStoreIndexTable");

            entity.HasIndex(e => e.IndexName, "LiteLLM_ManagedVectorStoreIndexTable_index_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IndexName).HasColumnName("index_name");
            entity.Property(e => e.LitellmParams)
                .HasColumnType("jsonb")
                .HasColumnName("litellm_params");
            entity.Property(e => e.IndexInfo)
                .HasColumnType("jsonb")
                .HasColumnName("index_info");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMCacheConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_CacheConfig_pkey");

            entity.ToTable("LiteLLM_CacheConfig");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("cache_config")
                .HasColumnName("id");
            entity.Property(e => e.CacheSettings)
                .HasColumnType("jsonb")
                .HasColumnName("cache_settings");
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
        });

        modelBuilder.Entity<LiteLLMUISettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_UISettings_pkey");

            entity.ToTable("LiteLLM_UISettings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasDefaultValue("ui_settings")
                .HasColumnName("id");
            entity.Property(e => e.UiSettings)
                .HasColumnType("jsonb")
                .HasColumnName("ui_settings");
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
        });

        modelBuilder.Entity<LiteLLMConfigOverrides>(entity =>
        {
            entity.HasKey(e => e.ConfigType).HasName("LiteLLM_ConfigOverrides_pkey");

            entity.ToTable("LiteLLM_ConfigOverrides");

            entity.Property(e => e.ConfigType).HasColumnName("config_type");
            entity.Property(e => e.ConfigValue)
                .HasColumnType("jsonb")
                .HasColumnName("config_value");
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
        });

        modelBuilder.Entity<LiteLLMSkillsTable>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("LiteLLM_SkillsTable_pkey");

            entity.ToTable("LiteLLM_SkillsTable");

            entity.Property(e => e.SkillId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("skill_id");
            entity.Property(e => e.DisplayTitle).HasColumnName("display_title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue("custom")
                .HasColumnName("source");
            entity.Property(e => e.LatestVersion).HasColumnName("latest_version");
            entity.Property(e => e.FileContent).HasColumnName("file_content");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.FileType).HasColumnName("file_type");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMPolicyTable>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("LiteLLM_PolicyTable_pkey");

            entity.ToTable("LiteLLM_PolicyTable");

            entity.HasIndex(e => new { e.PolicyName, e.VersionStatus }, "LiteLLM_PolicyTable_policy_name_version_status_idx");

            entity.HasIndex(e => new { e.PolicyName, e.VersionNumber }, "LiteLLM_PolicyTable_policy_name_version_number_key").IsUnique();

            entity.Property(e => e.PolicyId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("policy_id");
            entity.Property(e => e.PolicyName).HasColumnName("policy_name");
            entity.Property(e => e.VersionNumber)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("version_number");
            entity.Property(e => e.VersionStatus)
                .ValueGeneratedNever()
                .HasDefaultValue("production")
                .HasColumnName("version_status");
            entity.Property(e => e.ParentVersionId).HasColumnName("parent_version_id");
            entity.Property(e => e.IsLatest)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("is_latest");
            entity.Property(e => e.PublishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.ProductionAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("production_at");
            entity.Property(e => e.Inherit).HasColumnName("inherit");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GuardrailsAdd)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("guardrails_add");
            entity.Property(e => e.GuardrailsRemove)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("guardrails_remove");
            entity.Property(e => e.Condition)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("condition");
            entity.Property(e => e.Pipeline)
                .HasColumnType("jsonb")
                .HasColumnName("pipeline");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMPolicyAttachmentTable>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("LiteLLM_PolicyAttachmentTable_pkey");

            entity.ToTable("LiteLLM_PolicyAttachmentTable");

            entity.Property(e => e.AttachmentId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("attachment_id");
            entity.Property(e => e.PolicyName).HasColumnName("policy_name");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Teams)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("teams");
            entity.Property(e => e.Keys)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("keys");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("models");
            entity.Property(e => e.Tags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tags");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.IsDefault)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMToolTable>(entity =>
        {
            entity.HasKey(e => e.ToolId).HasName("LiteLLM_ToolTable_pkey");

            entity.ToTable("LiteLLM_ToolTable");

            entity.HasIndex(e => e.ToolName, "LiteLLM_ToolTable_tool_name_key").IsUnique();

            entity.HasIndex(e => e.InputPolicy, "LiteLLM_ToolTable_input_policy_idx");

            entity.HasIndex(e => e.OutputPolicy, "LiteLLM_ToolTable_output_policy_idx");

            entity.HasIndex(e => e.TeamId, "LiteLLM_ToolTable_team_id_idx");

            entity.Property(e => e.ToolId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("tool_id");
            entity.Property(e => e.ToolName).HasColumnName("tool_name");
            entity.Property(e => e.Origin).HasColumnName("origin");
            entity.Property(e => e.InputPolicy)
                .ValueGeneratedNever()
                .HasDefaultValue("untrusted")
                .HasColumnName("input_policy");
            entity.Property(e => e.OutputPolicy)
                .ValueGeneratedNever()
                .HasDefaultValue("untrusted")
                .HasColumnName("output_policy");
            entity.Property(e => e.CallCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("call_count");
            entity.Property(e => e.Assignments)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("assignments");
            entity.Property(e => e.KeyHash).HasColumnName("key_hash");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.KeyAlias).HasColumnName("key_alias");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMAccessGroupTable>(entity =>
        {
            entity.HasKey(e => e.AccessGroupId).HasName("LiteLLM_AccessGroupTable_pkey");

            entity.ToTable("LiteLLM_AccessGroupTable");

            entity.HasIndex(e => e.AccessGroupName, "LiteLLM_AccessGroupTable_access_group_name_key").IsUnique();

            entity.Property(e => e.AccessGroupId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("access_group_id");
            entity.Property(e => e.AccessGroupName).HasColumnName("access_group_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.AccessModelNames)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_model_names");
            entity.Property(e => e.AccessMcpServerIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_mcp_server_ids");
            entity.Property(e => e.McpToolPermissions)
                .HasColumnType("jsonb")
                .HasColumnName("mcp_tool_permissions");
            entity.Property(e => e.AccessAgentIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_agent_ids");
            entity.Property(e => e.AccessSkillNames)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_skill_names");
            entity.Property(e => e.AccessVectorStoreIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("access_vector_store_ids");
            entity.Property(e => e.AssignedTeamIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("assigned_team_ids");
            entity.Property(e => e.AssignedKeyIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("assigned_key_ids");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMClaudeCodePluginTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ClaudeCodePluginTable_pkey");

            entity.ToTable("LiteLLM_ClaudeCodePluginTable");

            entity.HasAlternateKey(e => e.Name).HasName("LiteLLM_ClaudeCodePluginTable_name_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ManifestJson).HasColumnName("manifest_json");
            entity.Property(e => e.FilesJson)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnName("files_json");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedNever()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.AnonymousDownloadCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("anonymous_download_count");
        });

        modelBuilder.Entity<LiteLLMClaudeCodePluginVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ClaudeCodePluginVersion_pkey");

            entity.ToTable("LiteLLM_ClaudeCodePluginVersion");

            entity.HasIndex(e => new { e.PluginId, e.PublishedAt }, "LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx");

            entity.HasIndex(e => new { e.PluginId, e.Version }, "LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PluginId).HasColumnName("plugin_id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ManifestJson).HasColumnName("manifest_json");
            entity.Property(e => e.PublishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.PublishedBy).HasColumnName("published_by");
            entity.Property(e => e.ArchiveAvailable)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("archive_available");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.Plugin).WithMany(p => p.Versions)
                .HasForeignKey(d => d.PluginId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_ClaudeCodePluginVersion_plugin_id_fkey");
        });

        modelBuilder.Entity<LiteLLMSkillSubmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_SkillSubmission_pkey");

            entity.ToTable("LiteLLM_SkillSubmission");

            entity.HasIndex(e => new { e.SubmittedBy, e.CreatedAt, e.Id }, "LiteLLM_SkillSubmission_submitted_by_created_at_id_idx").IsDescending(false, true, true);

            entity.HasIndex(e => new { e.SubmittedBy, e.Status, e.CreatedAt, e.Id }, "LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx").IsDescending(false, false, true, true);

            entity.HasIndex(e => new { e.Status, e.CreatedAt, e.Id }, "LiteLLM_SkillSubmission_status_created_at_id_idx").IsDescending(false, true, true);

            entity.HasIndex(e => new { e.CreatedAt, e.Id }, "LiteLLM_SkillSubmission_created_at_id_idx").IsDescending(true, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PluginName).HasColumnName("plugin_name");
            entity.Property(e => e.Operation).HasColumnName("operation");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending_review")
                .HasColumnName("status");
            entity.Property(e => e.ProposalJson).HasColumnName("proposal_json");
            entity.Property(e => e.ArchiveContent).HasColumnName("archive_content");
            entity.Property(e => e.SubmittedBy).HasColumnName("submitted_by");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.BasePluginId).HasColumnName("base_plugin_id");
            entity.Property(e => e.BaseUpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("base_updated_at");
            entity.Property(e => e.ReviewedBy).HasColumnName("reviewed_by");
            entity.Property(e => e.ReviewedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reviewed_at");
            entity.Property(e => e.RejectionReason).HasColumnName("rejection_reason");
            entity.Property(e => e.ScanJson).HasColumnName("scan_json");
        });

        modelBuilder.Entity<LiteLLMClaudeCodePluginDownload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ClaudeCodePluginDownload_pkey");

            entity.ToTable("LiteLLM_ClaudeCodePluginDownload");

            entity.HasIndex(e => new { e.PluginId, e.Id }, "LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.PluginId).HasColumnName("plugin_id");
            entity.Property(e => e.DownloadedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("downloaded_at");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Digest).HasColumnName("digest");
            entity.Property(e => e.Authenticated).HasColumnName("authenticated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.KeyAlias).HasColumnName("key_alias");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamAlias).HasColumnName("team_alias");

            entity.HasOne(d => d.Plugin).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.PluginId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_ClaudeCodePluginDownload_plugin_id_fkey");
        });

        modelBuilder.Entity<LiteLLMClaudeCodePluginUpload>(entity =>
        {
            entity.HasKey(e => e.PluginName).HasName("LiteLLM_ClaudeCodePluginUpload_pkey");

            entity.ToTable("LiteLLM_ClaudeCodePluginUpload");

            entity.Property(e => e.PluginName).HasColumnName("plugin_name");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.Plugin).WithOne(p => p.UploadedArchive)
                .HasPrincipalKey<LiteLLMClaudeCodePluginTable>(p => p.Name)
                .HasForeignKey<LiteLLMClaudeCodePluginUpload>(d => d.PluginName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_ClaudeCodePluginUpload_plugin_name_fkey");
        });

        modelBuilder.Entity<LiteLLMMemoryTable>(entity =>
        {
            entity.HasKey(e => e.MemoryId).HasName("LiteLLM_MemoryTable_pkey");

            entity.ToTable("LiteLLM_MemoryTable");

            entity.HasIndex(e => e.Key, "LiteLLM_MemoryTable_key_key").IsUnique();

            entity.HasIndex(e => e.UserId, "LiteLLM_MemoryTable_user_id_idx");

            entity.HasIndex(e => e.TeamId, "LiteLLM_MemoryTable_team_id_idx");

            entity.Property(e => e.MemoryId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("memory_id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMAdaptiveRouterState>(entity =>
        {
            entity.HasKey(e => new { e.RouterName, e.RequestType, e.ModelName }).HasName("LiteLLM_AdaptiveRouterState_pkey");

            entity.ToTable("LiteLLM_AdaptiveRouterState");

            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.RequestType).HasColumnName("request_type");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.Alpha).HasColumnName("alpha");
            entity.Property(e => e.Beta).HasColumnName("beta");
            entity.Property(e => e.TotalSamples)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("total_samples");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_updated_at");
        });

        modelBuilder.Entity<LiteLLMAdaptiveRouterSession>(entity =>
        {
            entity.HasKey(e => new { e.SessionId, e.RouterName, e.ModelName }).HasName("LiteLLM_AdaptiveRouterSession_pkey");

            entity.ToTable("LiteLLM_AdaptiveRouterSession");

            entity.HasIndex(e => e.LastActivityAt, "idx_adaptive_router_session_activity");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.ModelName).HasColumnName("model_name");
            entity.Property(e => e.ClassifiedType).HasColumnName("classified_type");
            entity.Property(e => e.MisalignmentCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("misalignment_count");
            entity.Property(e => e.StagnationCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("stagnation_count");
            entity.Property(e => e.DisengagementCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("disengagement_count");
            entity.Property(e => e.SatisfactionCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("satisfaction_count");
            entity.Property(e => e.FailureCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failure_count");
            entity.Property(e => e.LoopCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("loop_count");
            entity.Property(e => e.ExhaustionCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("exhaustion_count");
            entity.Property(e => e.LastUserContent).HasColumnName("last_user_content");
            entity.Property(e => e.LastAssistantContent).HasColumnName("last_assistant_content");
            entity.Property(e => e.ToolCallHistory)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("tool_call_history");
            entity.Property(e => e.PendingToolCalls)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("pending_tool_calls");
            entity.Property(e => e.TurnCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("turn_count");
            entity.Property(e => e.LastProcessedTurn)
                .ValueGeneratedNever()
                .HasDefaultValue(-1)
                .HasColumnName("last_processed_turn");
            entity.Property(e => e.CleanCreditAwarded)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("clean_credit_awarded");
            entity.Property(e => e.TerminalStatus).HasColumnName("terminal_status");
            entity.Property(e => e.LastActivityAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_activity_at");
        });

        modelBuilder.Entity<LiteLLMAutoRouterBaselineComparison>(entity =>
        {
            entity.HasKey(e => e.Scope).HasName("LiteLLM_AutoRouterBaselineComparison_pkey");

            entity.ToTable("LiteLLM_AutoRouterBaselineComparison");

            entity.HasIndex(e => new { e.ApiKey, e.SessionId, e.RouterName }, "idx_autorouter_baseline_scope");

            entity.HasIndex(e => e.UpdatedAt, "idx_autorouter_baseline_updated");

            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.InitialEquivalent).HasColumnName("initial_equivalent");
            entity.Property(e => e.Revision)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("revision");
            entity.Property(e => e.PublishedRevision)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("published_revision");
            entity.Property(e => e.History).HasColumnName("history");
            entity.Property(e => e.AttemptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("attempted_at");
            entity.Property(e => e.Retired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("retired");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<LiteLLMAutoRouterBaselineObservation>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("LiteLLM_AutoRouterBaselineObservation_pkey");

            entity.ToTable("LiteLLM_AutoRouterBaselineObservation");

            entity.HasIndex(e => new { e.Scope, e.StartedAt, e.RequestId }, "idx_autorouter_baseline_event_order");

            entity.HasIndex(e => new { e.Scope, e.Revision, e.StartedAt }, "idx_autorouter_baseline_event_revision");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Publication).HasColumnName("publication");
            entity.Property(e => e.Conflicted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("conflicted");
        });

        modelBuilder.Entity<LiteLLMAutoRouterSession>(entity =>
        {
            entity.HasKey(e => new { e.ApiKey, e.SessionId, e.RouterName }).HasName("LiteLLM_AutoRouterSession_pkey");

            entity.ToTable("LiteLLM_AutoRouterSession");

            entity.HasIndex(e => e.LastTurnAt, "idx_autorouter_session_last_turn");

            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.RouterType).HasColumnName("router_type");
            entity.Property(e => e.FirstTurnAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("first_turn_at");
            entity.Property(e => e.LastTurnAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_turn_at");
            entity.Property(e => e.LastModel).HasColumnName("last_model");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("models");
            entity.Property(e => e.Turns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("turns");
            entity.Property(e => e.UnorderedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unordered_turns");
            entity.Property(e => e.CoveredTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("covered_turns");
            entity.Property(e => e.CacheHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("cache_hits");
            entity.Property(e => e.SameModelTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("same_model_turns");
            entity.Property(e => e.SameModelHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("same_model_hits");
            entity.Property(e => e.FirstVisitTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("first_visit_turns");
            entity.Property(e => e.FirstVisitHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("first_visit_hits");
            entity.Property(e => e.ReturnTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_turns");
            entity.Property(e => e.ReturnHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_hits");
            entity.Property(e => e.ReturnExpiredMisses)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_expired_misses");
            entity.Property(e => e.ReturnWithinTtlMisses)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_within_ttl_misses");
            entity.Property(e => e.Ttl5mTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("ttl_5m_turns");
            entity.Property(e => e.Ttl1hTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("ttl_1h_turns");
            entity.Property(e => e.TotalTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_tokens");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.SavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("saved_spend");
            entity.Property(e => e.SavingsEstimatedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("savings_estimated_turns");
            entity.Property(e => e.SavingsEstimatedActualSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_actual_spend");
            entity.Property(e => e.SavingsEstimatedSavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_saved_spend");
            entity.Property(e => e.SavingsEstimatedBaselineModels)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("savings_estimated_baseline_models");
            entity.Property(e => e.ClassifierCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("classifier_cost");
            entity.Property(e => e.ClassifierCostRecordedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("classifier_cost_recorded_turns");
            entity.Property(e => e.TierTurns)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("tier_turns");
            entity.Property(e => e.BaselineModels)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("baseline_models");
        });

        modelBuilder.Entity<LiteLLMAutoRouterUserSession>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ApiKey, e.SessionId, e.RouterName }).HasName("LiteLLM_AutoRouterUserSession_pkey");

            entity.ToTable("LiteLLM_AutoRouterUserSession");

            entity.HasIndex(e => e.LastTurnAt, "idx_autorouter_user_session_last_turn");

            entity.HasIndex(e => new { e.UserId, e.LastTurnAt }, "idx_autorouter_user_session_user_last_turn");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.RouterType).HasColumnName("router_type");
            entity.Property(e => e.FirstTurnAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("first_turn_at");
            entity.Property(e => e.LastTurnAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_turn_at");
            entity.Property(e => e.LastModel).HasColumnName("last_model");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("models");
            entity.Property(e => e.Turns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("turns");
            entity.Property(e => e.UnorderedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unordered_turns");
            entity.Property(e => e.CoveredTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("covered_turns");
            entity.Property(e => e.CacheHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("cache_hits");
            entity.Property(e => e.SameModelTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("same_model_turns");
            entity.Property(e => e.SameModelHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("same_model_hits");
            entity.Property(e => e.FirstVisitTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("first_visit_turns");
            entity.Property(e => e.FirstVisitHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("first_visit_hits");
            entity.Property(e => e.ReturnTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_turns");
            entity.Property(e => e.ReturnHits)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_hits");
            entity.Property(e => e.ReturnExpiredMisses)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_expired_misses");
            entity.Property(e => e.ReturnWithinTtlMisses)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("return_within_ttl_misses");
            entity.Property(e => e.Ttl5mTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("ttl_5m_turns");
            entity.Property(e => e.Ttl1hTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("ttl_1h_turns");
            entity.Property(e => e.TotalTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_tokens");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.SavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("saved_spend");
            entity.Property(e => e.SavingsEstimatedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("savings_estimated_turns");
            entity.Property(e => e.SavingsEstimatedActualSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_actual_spend");
            entity.Property(e => e.SavingsEstimatedSavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_saved_spend");
            entity.Property(e => e.SavingsEstimatedBaselineModels)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("savings_estimated_baseline_models");
            entity.Property(e => e.ClassifierCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("classifier_cost");
            entity.Property(e => e.ClassifierCostRecordedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("classifier_cost_recorded_turns");
            entity.Property(e => e.TierTurns)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("tier_turns");
            entity.Property(e => e.BaselineModels)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("baseline_models");
        });

        modelBuilder.Entity<LiteLLMAutoRouterDailySpend>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.ApiKey, e.UserId, e.RouterName, e.RouterType }).HasName("LiteLLM_AutoRouterDailySpend_pkey");

            entity.ToTable("LiteLLM_AutoRouterDailySpend");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.RouterType).HasColumnName("router_type");
            entity.Property(e => e.Turns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("turns");
            entity.Property(e => e.TotalTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_tokens");
            entity.Property(e => e.TokenRecordedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("token_recorded_turns");
            entity.Property(e => e.Spend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("spend");
            entity.Property(e => e.SavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("saved_spend");
            entity.Property(e => e.SavingsEstimatedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("savings_estimated_turns");
            entity.Property(e => e.SavingsEstimatedActualSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_actual_spend");
            entity.Property(e => e.SavingsEstimatedSavedSpend)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("savings_estimated_saved_spend");
            entity.Property(e => e.ClassifierCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("classifier_cost");
            entity.Property(e => e.ClassifierCostRecordedTurns)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("classifier_cost_recorded_turns");
        });

        modelBuilder.Entity<LiteLLMShadowEvalJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ShadowEvalJob_pkey");

            entity.ToTable("LiteLLM_ShadowEvalJob");

            entity.HasIndex(e => e.GroupId, "LiteLLM_ShadowEvalJob_group_id_idx");

            entity.HasIndex(e => new { e.TargetType, e.TargetId }, "LiteLLM_ShadowEvalJob_target_type_target_id_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_ShadowEvalJob_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.TargetType)
                .ValueGeneratedNever()
                .HasDefaultValue("key")
                .HasColumnName("target_type");
            entity.Property(e => e.TargetId).HasColumnName("target_id");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.RouterNames)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("router_names");
            entity.Property(e => e.Models)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("models");
            entity.Property(e => e.Direction)
                .ValueGeneratedNever()
                .HasDefaultValue("forward")
                .HasColumnName("direction");
            entity.Property(e => e.BaselineModel).HasColumnName("baseline_model");
            entity.Property(e => e.JudgeModel).HasColumnName("judge_model");
            entity.Property(e => e.ShadowPercentage).HasColumnName("shadow_percentage");
            entity.Property(e => e.MaxTurns).HasColumnName("max_turns");
            entity.Property(e => e.MaxBudget).HasColumnName("max_budget");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("ends_at");
            entity.Property(e => e.StoppedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("stopped_at");
            entity.Property(e => e.StoppedBy).HasColumnName("stopped_by");
        });

        modelBuilder.Entity<LiteLLMShadowEvalAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_ShadowEvalAttempt_pkey");

            entity.ToTable("LiteLLM_ShadowEvalAttempt");

            entity.HasIndex(e => e.JobId, "LiteLLM_ShadowEvalAttempt_job_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.RouterName).HasColumnName("router_name");
            entity.Property(e => e.Tier).HasColumnName("tier");
            entity.Property(e => e.RealModel).HasColumnName("real_model");
            entity.Property(e => e.ShadowModel).HasColumnName("shadow_model");
            entity.Property(e => e.Confidence).HasColumnName("confidence");
            entity.Property(e => e.JudgeCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("judge_cost");
            entity.Property(e => e.ShadowCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("shadow_cost");
            entity.Property(e => e.RealCost).HasColumnName("real_cost");
            entity.Property(e => e.RealClassifierCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("real_classifier_cost");
            entity.Property(e => e.ShadowClassifierCost)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("shadow_classifier_cost");
            entity.Property(e => e.RealCacheHit)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("real_cache_hit");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMShadowEvalFunnel>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("LiteLLM_ShadowEvalFunnel_pkey");

            entity.ToTable("LiteLLM_ShadowEvalFunnel");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.NotSampled)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("not_sampled");
            entity.Property(e => e.Unjudgeable)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unjudgeable");
            entity.Property(e => e.Shed)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("shed");
            entity.Property(e => e.Withheld)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("withheld");
        });

        modelBuilder.Entity<LiteLLMWorkflowRun>(entity =>
        {
            entity.HasKey(e => e.RunId).HasName("LiteLLM_WorkflowRun_pkey");

            entity.ToTable("LiteLLM_WorkflowRun");

            entity.HasIndex(e => e.SessionId, "LiteLLM_WorkflowRun_session_id_key").IsUnique();

            entity.HasIndex(e => new { e.WorkflowType, e.Status }, "LiteLLM_WorkflowRun_workflow_type_status_idx");

            entity.HasIndex(e => e.SessionId, "LiteLLM_WorkflowRun_session_id_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_WorkflowRun_created_at_idx");

            entity.HasIndex(e => e.CreatedBy, "LiteLLM_WorkflowRun_created_by_idx");

            entity.Property(e => e.RunId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("run_id");
            entity.Property(e => e.SessionId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("session_id");
            entity.Property(e => e.WorkflowType).HasColumnName("workflow_type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
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
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<LiteLLMWorkflowEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("LiteLLM_WorkflowEvent_pkey");

            entity.ToTable("LiteLLM_WorkflowEvent");

            entity.HasIndex(e => e.RunId, "LiteLLM_WorkflowEvent_run_id_idx");

            entity.HasIndex(e => new { e.RunId, e.SequenceNumber }, "LiteLLM_WorkflowEvent_run_id_sequence_number_key").IsUnique();

            entity.Property(e => e.EventId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("event_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.EventType).HasColumnName("event_type");
            entity.Property(e => e.StepName).HasColumnName("step_name");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequence_number");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Run).WithMany(p => p.Events)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_WorkflowEvent_run_id_fkey");
        });

        modelBuilder.Entity<LiteLLMWorkflowMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("LiteLLM_WorkflowMessage_pkey");

            entity.ToTable("LiteLLM_WorkflowMessage");

            entity.HasIndex(e => e.RunId, "LiteLLM_WorkflowMessage_run_id_idx");

            entity.HasIndex(e => new { e.RunId, e.SequenceNumber }, "LiteLLM_WorkflowMessage_run_id_sequence_number_key").IsUnique();

            entity.Property(e => e.MessageId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("message_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequence_number");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Run).WithMany(p => p.Messages)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("LiteLLM_WorkflowMessage_run_id_fkey");
        });

        modelBuilder.Entity<LiteLLMBackgroundInteractionSettlement>(entity =>
        {
            entity.HasKey(e => e.InteractionId).HasName("LiteLLM_BackgroundInteractionSettlement_pkey");

            entity.ToTable("LiteLLM_BackgroundInteractionSettlement");

            entity.HasIndex(e => e.ClaimedAt, "idx_background_interaction_settlement_claimed_at");

            entity.HasIndex(e => e.SettledAt, "idx_background_interaction_settlement_settled_at");

            entity.Property(e => e.InteractionId).HasColumnName("interaction_id");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.CreateContext)
                .HasColumnType("jsonb")
                .HasColumnName("create_context");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimed_at");
            entity.Property(e => e.ClaimedBy).HasColumnName("claimed_by");
            entity.Property(e => e.SettledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("settled_at");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
        });

        modelBuilder.Entity<LiteLLMBackgroundResponseSettlement>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("LiteLLM_BackgroundResponseSettlement_pkey");

            entity.ToTable("LiteLLM_BackgroundResponseSettlement");

            entity.Property(e => e.ResponseId).HasColumnName("response_id");
            entity.Property(e => e.SettledAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("settled_at");
        });

        modelBuilder.Entity<LiteLLMLens>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_Lens_pkey");

            entity.ToTable("LiteLLM_Lens");

            entity.HasIndex(e => e.DueAt, "LiteLLM_Lens_due_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.DueAt)
                .HasDefaultValueSql("'1970-01-01 00:00:00'::timestamp without time zone")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("due_at");
        });

        modelBuilder.Entity<LiteLLMLensRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_LensRun_pkey");

            entity.ToTable("LiteLLM_LensRun");

            entity.HasIndex(e => new { e.LensId, e.CreatedAt }, "LiteLLM_LensRun_lens_id_created_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LensId).HasColumnName("lens_id");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMLensReview>(entity =>
        {
            entity.HasKey(e => new { e.LensId, e.CriteriaKey, e.ExecutionId }).HasName("LiteLLM_LensReview_pkey");

            entity.ToTable("LiteLLM_LensReview");

            entity.Property(e => e.LensId).HasColumnName("lens_id");
            entity.Property(e => e.CriteriaKey).HasColumnName("criteria_key");
            entity.Property(e => e.ExecutionId).HasColumnName("execution_id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");

            entity.HasOne(d => d.Lens).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.LensId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LiteLLM_LensReview_lens_id_fkey");
        });

        modelBuilder.Entity<LiteLLMLensWorker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_LensWorker_pkey");

            entity.ToTable("LiteLLM_LensWorker");

            entity.HasIndex(e => e.TokenHash, "LiteLLM_LensWorker_token_hash_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TokenHash).HasColumnName("token_hash");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMLensIngestionKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_LensIngestionKey_pkey");

            entity.ToTable("LiteLLM_LensIngestionKey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMLensDataset>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Revision }).HasName("LiteLLM_LensDataset_pkey");

            entity.ToTable("LiteLLM_LensDataset");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMLensSignalConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_LensSignalConfig_pkey");

            entity.ToTable("LiteLLM_LensSignalConfig");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMLensTraceSignal>(entity =>
        {
            entity.HasKey(e => new { e.TraceId, e.TraceRef }).HasName("LiteLLM_LensTraceSignal_pkey");

            entity.ToTable("LiteLLM_LensTraceSignal");

            entity.Property(e => e.TraceId).HasColumnName("trace_id");
            entity.Property(e => e.TraceRef)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("trace_ref");
            entity.Property(e => e.ConfigKey).HasColumnName("config_key");
            entity.Property(e => e.SpanCount).HasColumnName("span_count");
            entity.Property(e => e.ClaimedUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimed_until");
            entity.Property(e => e.ClassifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("classified_at");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
        });

        modelBuilder.Entity<LiteLLMTelemetryReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_TelemetryReport_pkey");

            entity.ToTable("LiteLLM_TelemetryReport");

            entity.HasIndex(e => e.WindowEnd, "LiteLLM_TelemetryReport_window_end_idx");

            entity.HasIndex(e => e.CreatedAt, "LiteLLM_TelemetryReport_created_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WindowStart).HasColumnName("window_start");
            entity.Property(e => e.WindowEnd).HasColumnName("window_end");
            entity.Property(e => e.Report)
                .HasColumnType("jsonb")
                .HasColumnName("report");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMSpendLogDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("LiteLLM_SpendLogDelivery_pkey");

            entity.ToTable("LiteLLM_SpendLogDelivery");

            entity.HasIndex(e => new { e.State, e.NextAttemptAt }, "LiteLLM_SpendLogDelivery_state_next_attempt_at_idx");

            entity.HasIndex(e => new { e.State, e.AppliedAt }, "LiteLLM_SpendLogDelivery_state_applied_at_idx");

            entity.HasIndex(e => e.RequestId, "LiteLLM_SpendLogDelivery_request_id_idx");

            entity.Property(e => e.DeliveryId)
                .HasDefaultValueSql("(gen_random_uuid())::text")
                .HasColumnName("delivery_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(AccountingDeliveryState.Pending)
                .HasColumnName("state");
            entity.Property(e => e.AccountingPayload)
                .HasColumnType("jsonb")
                .HasColumnName("accounting_payload");
            entity.Property(e => e.IsBatch).HasColumnName("is_batch");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.NextAttemptAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_attempt_at");
            entity.Property(e => e.AppliedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("applied_at");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<LiteLLMSubscriptionAccountTable>(entity =>
        {
            entity.HasKey(e => e.SubscriptionAccountId).HasName("LiteLLM_SubscriptionAccountTable_pkey");

            entity.ToTable("LiteLLM_SubscriptionAccountTable");

            entity.HasIndex(e => new { e.CustomLlmProvider, e.AccountId }, "LiteLLM_SubscriptionAccountTable_provider_account_key").IsUnique();

            entity.Property(e => e.SubscriptionAccountId)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("subscription_account_id");
            entity.Property(e => e.CustomLlmProvider).HasColumnName("custom_llm_provider");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.MonthlyFee).HasColumnName("monthly_fee");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.BillingPeriodStart).HasColumnName("billing_period_start");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_Alert_pkey");

            entity.ToTable("LiteLLM_Alert");

            entity.HasIndex(e => e.DetectionKey, "LiteLLM_Alert_detection_key_key").IsUnique();

            entity.HasIndex(e => new { e.Status, e.Id }, "LiteLLM_Alert_status_id_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.DetectionKey)
                .HasMaxLength(64)
                .HasColumnName("detection_key");
            entity.Property(e => e.Type)
                .HasMaxLength(32)
                .HasColumnName("type");
            entity.Property(e => e.ResourceType)
                .HasMaxLength(16)
                .HasColumnName("resource_type");
            entity.Property(e => e.ResourceId)
                .HasMaxLength(1024)
                .HasColumnName("resource_id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasMaxLength(16)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.WindowStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("window_start");
            entity.Property(e => e.WindowEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("window_end");
            entity.Property(e => e.Evidence)
                .HasColumnType("jsonb")
                .HasColumnName("evidence");
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
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(1024)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<LiteLLMPerformanceAlertState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LiteLLM_PerformanceAlertState_pkey");

            entity.ToTable("LiteLLM_PerformanceAlertState");

            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.AlertsUsed).HasColumnName("alerts_used");
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Owner)
                .HasMaxLength(32)
                .HasColumnName("owner");
            entity.Property(e => e.NextRunAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("next_run_at");
            entity.Property(e => e.Day)
                .HasMaxLength(10)
                .HasColumnName("day");
            entity.Property(e => e.Scope)
                .HasMaxLength(16)
                .HasColumnName("scope");
            entity.Property(e => e.ResumeAfter)
                .ValueGeneratedNever()
                .HasMaxLength(1024)
                .HasDefaultValue("")
                .HasColumnName("resume_after");
            entity.Property(e => e.Wrapped)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("wrapped");
            entity.Property(e => e.Checked)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("checked");
            entity.Property(e => e.Skipped)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("skipped");
            entity.Property(e => e.StartScope)
                .HasMaxLength(16)
                .HasColumnName("start_scope");
            entity.Property(e => e.StartAfter)
                .ValueGeneratedNever()
                .HasMaxLength(1024)
                .HasDefaultValue("")
                .HasColumnName("start_after");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
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
        foreach (var entry in ChangeTracker.Entries<LiteLLMBudgetTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMCustomerOrganizationTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMCustomerTeamTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMCustomerMemberTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMCredentialsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMProxyModelTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMAgentsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMOrganizationTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMModelTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMTeamTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMProjectTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMUserTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMCPServerTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMCPToolsetTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMCPUserCredentials>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMUserProviderCredentials>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMCPUserEnvVars>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMCPServerOAuthClient>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSSOIdentityAssertion>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMVerificationToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMJWTKeyMapping>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMModelAccessGroupBudgetTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMTagTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSpendLogs>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMBudgetWindowSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMOrganizationMembership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyUserSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyGlobalSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyOrganizationSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyCustomerMemberSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyEndUserSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyAgentSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyTeamSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyTagSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMManagedFileTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMManagedObjectTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMManagedVectorStoreTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMManagedVectorStoresTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMGuardrailsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyGuardrailMetrics>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyGuardrailUsageUnits>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyPolicyMetrics>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyToolSpend>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyModelUsage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyGatewayRequests>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyGatewayFailedRequests>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMDailyRequestErrors>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMPromptTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMHealthCheckTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSearchToolsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSSOConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMManagedVectorStoreIndexTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMCacheConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMUISettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMConfigOverrides>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSkillsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMPolicyTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMPolicyAttachmentTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMToolTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMAccessGroupTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMClaudeCodePluginTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMMemoryTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMAdaptiveRouterState>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastUpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastUpdatedAt).IsModified))
            {
                entry.Entity.LastUpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMAdaptiveRouterSession>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastActivityAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastActivityAt).IsModified))
            {
                entry.Entity.LastActivityAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMWorkflowRun>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMSubscriptionAccountTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMAlert>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<LiteLLMPerformanceAlertState>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

file sealed class UuidV4StringGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => Guid.NewGuid().ToString();
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
