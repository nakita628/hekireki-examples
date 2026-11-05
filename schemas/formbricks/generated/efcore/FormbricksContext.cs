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

namespace Formbricks.Models;

public partial class FormbricksContext : DbContext
{
    public FormbricksContext(DbContextOptions<FormbricksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Creative> Creatives { get; set; }

    public virtual DbSet<CreativeDailyStat> CreativeDailyStats { get; set; }

    public virtual DbSet<CreativeDailyPageStat> CreativeDailyPageStats { get; set; }

    public virtual DbSet<CreativeTrigger> CreativeTriggers { get; set; }

    public virtual DbSet<CreativeLanguage> CreativeLanguages { get; set; }

    public virtual DbSet<OutboundEmailAudit> OutboundEmailAudits { get; set; }

    public virtual DbSet<OutboundEmailAuditErasureIntent> OutboundEmailAuditErasureIntents { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<ContactAttribute> ContactAttributes { get; set; }

    public virtual DbSet<ContactAttributeKey> ContactAttributeKeys { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsOnResponses> TagsOnResponses { get; set; }

    public virtual DbSet<Display> Displays { get; set; }

    public virtual DbSet<SurveyTrigger> SurveyTriggers { get; set; }

    public virtual DbSet<SurveyAttributeFilter> SurveyAttributeFilters { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<SurveyQuota> SurveyQuota { get; set; }

    public virtual DbSet<ResponseQuotaLink> ResponseQuotaLinks { get; set; }

    public virtual DbSet<SurveyFollowUp> SurveyFollowUps { get; set; }

    public virtual DbSet<ActionClass> ActionClasses { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<DataMigration> DataMigrations { get; set; }

    public virtual DbSet<Workspace> Workspaces { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationBilling> OrganizationBillings { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<ApiKeyWorkspace> ApiKeyWorkspaces { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<TwoFactor> TwoFactors { get; set; }

    public virtual DbSet<Jwks> Jwks { get; set; }

    public virtual DbSet<OauthClient> OauthClients { get; set; }

    public virtual DbSet<OauthAccessToken> OauthAccessTokens { get; set; }

    public virtual DbSet<OauthRefreshToken> OauthRefreshTokens { get; set; }

    public virtual DbSet<OauthConsent> OauthConsents { get; set; }

    public virtual DbSet<OauthResource> OauthResources { get; set; }

    public virtual DbSet<OauthClientResource> OauthClientResources { get; set; }

    public virtual DbSet<OauthClientAssertion> OauthClientAssertions { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<SurveyLanguage> SurveyLanguages { get; set; }

    public virtual DbSet<EmbeddedData> EmbeddedData { get; set; }

    public virtual DbSet<SurveyEmbeddedData> SurveyEmbeddedData { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamUser> TeamUsers { get; set; }

    public virtual DbSet<WorkspaceTeam> WorkspaceTeams { get; set; }

    public virtual DbSet<Chart> Charts { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<DashboardWidget> DashboardWidgets { get; set; }

    public virtual DbSet<DeletionCleanup> DeletionCleanups { get; set; }

    public virtual DbSet<AttachmentExport> AttachmentExports { get; set; }

    public virtual DbSet<FeedbackSource> FeedbackSources { get; set; }

    public virtual DbSet<FeedbackSourceFormbricksMapping> FeedbackSourceFormbricksMappings { get; set; }

    public virtual DbSet<FeedbackSourceFieldMapping> FeedbackSourceFieldMappings { get; set; }

    public virtual DbSet<FeedbackDirectory> FeedbackDirectories { get; set; }

    public virtual DbSet<FeedbackDirectoryWorkspace> FeedbackDirectoryWorkspaces { get; set; }

    public virtual DbSet<AuthzedProjectionScopeState> AuthzedProjectionScopeStates { get; set; }

    public virtual DbSet<AuthzedProjectionOutbox> AuthzedProjectionOutboxes { get; set; }

    public virtual DbSet<OnlineUpgradeState> OnlineUpgradeStates { get; set; }

    public virtual DbSet<RetentionPolicy> RetentionPolicies { get; set; }

    public virtual DbSet<RetentionExemption> RetentionExemptions { get; set; }

    public virtual DbSet<RetentionRun> RetentionRuns { get; set; }

    public virtual DbSet<RetentionRunItem> RetentionRunItems { get; set; }

    public virtual DbSet<RetentionNotice> RetentionNotices { get; set; }

    public virtual DbSet<RetentionSurveyRestore> RetentionSurveyRestores { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowVersion> WorkflowVersions { get; set; }

    public virtual DbSet<WorkflowRun> WorkflowRuns { get; set; }

    public virtual DbSet<WorkflowRunLog> WorkflowRunLogs { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<CreativeType>("CreativeType");
        npgsql.MapEnum<CreativeStatus>("CreativeStatus");
        npgsql.MapEnum<CreativeShowOn>("CreativeShowOn");
        npgsql.MapEnum<CreativeDeviceType>("CreativeDeviceType");
        npgsql.MapEnum<PipelineTriggers>("PipelineTriggers");
        npgsql.MapEnum<WebhookSource>("WebhookSource");
        npgsql.MapEnum<ContactAttributeType>("ContactAttributeType");
        npgsql.MapEnum<ContactAttributeDataType>("ContactAttributeDataType");
        npgsql.MapEnum<ResponseOutcome>("ResponseOutcome");
        npgsql.MapEnum<SurveyStatus>("SurveyStatus");
        npgsql.MapEnum<SurveyAttributeFilterCondition>("SurveyAttributeFilterCondition");
        npgsql.MapEnum<SurveyQuotaAction>("SurveyQuotaAction");
        npgsql.MapEnum<ResponseQuotaLinkStatus>("ResponseQuotaLinkStatus");
        npgsql.MapEnum<SurveyType>("SurveyType");
        npgsql.MapEnum<DisplayOptions>("displayOptions");
        npgsql.MapEnum<SurveyVisibility>("SurveyVisibility");
        npgsql.MapEnum<SurveyScriptMode>("SurveyScriptMode");
        npgsql.MapEnum<ActionType>("ActionType");
        npgsql.MapEnum<IntegrationType>("IntegrationType");
        npgsql.MapEnum<DataMigrationStatus>("DataMigrationStatus");
        npgsql.MapEnum<WidgetPlacement>("WidgetPlacement");
        npgsql.MapEnum<SurveyOverlay>("SurveyOverlay");
        npgsql.MapEnum<BootstrapAdminMarker>("BootstrapAdminMarker");
        npgsql.MapEnum<OrganizationRole>("OrganizationRole");
        npgsql.MapEnum<ApiKeyPermission>("ApiKeyPermission");
        npgsql.MapEnum<IdentityProvider>("IdentityProvider");
        npgsql.MapEnum<EmbeddedDataSource>("EmbeddedDataSource");
        npgsql.MapEnum<EmbeddedDataType>("EmbeddedDataType");
        npgsql.MapEnum<TeamUserRole>("TeamUserRole");
        npgsql.MapEnum<WorkspaceTeamPermission>("WorkspaceTeamPermission");
        npgsql.MapEnum<ChartType>("ChartType");
        npgsql.MapEnum<FeedbackSourceType>("FeedbackSourceType");
        npgsql.MapEnum<FeedbackSourceStatus>("FeedbackSourceStatus");
        npgsql.MapEnum<FeedbackSourceImportMode>("FeedbackSourceImportMode");
        npgsql.MapEnum<FeedbackSourceElementScope>("FeedbackSourceElementScope");
        npgsql.MapEnum<HubFieldType>("HubFieldType");
        npgsql.MapEnum<DeletionCleanupKind>("DeletionCleanupKind");
        npgsql.MapEnum<AttachmentExportStatus>("AttachmentExportStatus");
        npgsql.MapEnum<RetentionEntity>("RetentionEntity");
        npgsql.MapEnum<RetentionSurveyCondition>("RetentionSurveyCondition");
        npgsql.MapEnum<RetentionRunKind>("RetentionRunKind");
        npgsql.MapEnum<RetentionExemptionSource>("RetentionExemptionSource");
        npgsql.MapEnum<RetentionTargetType>("RetentionTargetType");
        npgsql.MapEnum<RetentionRunItemAction>("RetentionRunItemAction");
        npgsql.MapEnum<RetentionSkipReason>("RetentionSkipReason");
        npgsql.MapEnum<WorkflowStatus>("WorkflowStatus");
        npgsql.MapEnum<WorkflowRunStatus>("WorkflowRunStatus");
        npgsql.MapEnum<WorkflowRunLogStatus>("WorkflowRunLogStatus");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<CreativeType>("CreativeType");
        dataSource.MapEnum<CreativeStatus>("CreativeStatus");
        dataSource.MapEnum<CreativeShowOn>("CreativeShowOn");
        dataSource.MapEnum<CreativeDeviceType>("CreativeDeviceType");
        dataSource.MapEnum<PipelineTriggers>("PipelineTriggers");
        dataSource.MapEnum<WebhookSource>("WebhookSource");
        dataSource.MapEnum<ContactAttributeType>("ContactAttributeType");
        dataSource.MapEnum<ContactAttributeDataType>("ContactAttributeDataType");
        dataSource.MapEnum<ResponseOutcome>("ResponseOutcome");
        dataSource.MapEnum<SurveyStatus>("SurveyStatus");
        dataSource.MapEnum<SurveyAttributeFilterCondition>("SurveyAttributeFilterCondition");
        dataSource.MapEnum<SurveyQuotaAction>("SurveyQuotaAction");
        dataSource.MapEnum<ResponseQuotaLinkStatus>("ResponseQuotaLinkStatus");
        dataSource.MapEnum<SurveyType>("SurveyType");
        dataSource.MapEnum<DisplayOptions>("displayOptions");
        dataSource.MapEnum<SurveyVisibility>("SurveyVisibility");
        dataSource.MapEnum<SurveyScriptMode>("SurveyScriptMode");
        dataSource.MapEnum<ActionType>("ActionType");
        dataSource.MapEnum<IntegrationType>("IntegrationType");
        dataSource.MapEnum<DataMigrationStatus>("DataMigrationStatus");
        dataSource.MapEnum<WidgetPlacement>("WidgetPlacement");
        dataSource.MapEnum<SurveyOverlay>("SurveyOverlay");
        dataSource.MapEnum<BootstrapAdminMarker>("BootstrapAdminMarker");
        dataSource.MapEnum<OrganizationRole>("OrganizationRole");
        dataSource.MapEnum<ApiKeyPermission>("ApiKeyPermission");
        dataSource.MapEnum<IdentityProvider>("IdentityProvider");
        dataSource.MapEnum<EmbeddedDataSource>("EmbeddedDataSource");
        dataSource.MapEnum<EmbeddedDataType>("EmbeddedDataType");
        dataSource.MapEnum<TeamUserRole>("TeamUserRole");
        dataSource.MapEnum<WorkspaceTeamPermission>("WorkspaceTeamPermission");
        dataSource.MapEnum<ChartType>("ChartType");
        dataSource.MapEnum<FeedbackSourceType>("FeedbackSourceType");
        dataSource.MapEnum<FeedbackSourceStatus>("FeedbackSourceStatus");
        dataSource.MapEnum<FeedbackSourceImportMode>("FeedbackSourceImportMode");
        dataSource.MapEnum<FeedbackSourceElementScope>("FeedbackSourceElementScope");
        dataSource.MapEnum<HubFieldType>("HubFieldType");
        dataSource.MapEnum<DeletionCleanupKind>("DeletionCleanupKind");
        dataSource.MapEnum<AttachmentExportStatus>("AttachmentExportStatus");
        dataSource.MapEnum<RetentionEntity>("RetentionEntity");
        dataSource.MapEnum<RetentionSurveyCondition>("RetentionSurveyCondition");
        dataSource.MapEnum<RetentionRunKind>("RetentionRunKind");
        dataSource.MapEnum<RetentionExemptionSource>("RetentionExemptionSource");
        dataSource.MapEnum<RetentionTargetType>("RetentionTargetType");
        dataSource.MapEnum<RetentionRunItemAction>("RetentionRunItemAction");
        dataSource.MapEnum<RetentionSkipReason>("RetentionSkipReason");
        dataSource.MapEnum<WorkflowStatus>("WorkflowStatus");
        dataSource.MapEnum<WorkflowRunStatus>("WorkflowRunStatus");
        dataSource.MapEnum<WorkflowRunLogStatus>("WorkflowRunLogStatus");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<CreativeType>(name: "CreativeType");
        modelBuilder.HasPostgresEnum<CreativeStatus>(name: "CreativeStatus");
        modelBuilder.HasPostgresEnum<CreativeShowOn>(name: "CreativeShowOn");
        modelBuilder.HasPostgresEnum<CreativeDeviceType>(name: "CreativeDeviceType");
        modelBuilder.HasPostgresEnum<PipelineTriggers>(name: "PipelineTriggers");
        modelBuilder.HasPostgresEnum<WebhookSource>(name: "WebhookSource");
        modelBuilder.HasPostgresEnum<ContactAttributeType>(name: "ContactAttributeType");
        modelBuilder.HasPostgresEnum<ContactAttributeDataType>(name: "ContactAttributeDataType");
        modelBuilder.HasPostgresEnum<ResponseOutcome>(name: "ResponseOutcome");
        modelBuilder.HasPostgresEnum<SurveyStatus>(name: "SurveyStatus");
        modelBuilder.HasPostgresEnum<SurveyAttributeFilterCondition>(name: "SurveyAttributeFilterCondition");
        modelBuilder.HasPostgresEnum<SurveyQuotaAction>(name: "SurveyQuotaAction");
        modelBuilder.HasPostgresEnum<ResponseQuotaLinkStatus>(name: "ResponseQuotaLinkStatus");
        modelBuilder.HasPostgresEnum<SurveyType>(name: "SurveyType");
        modelBuilder.HasPostgresEnum<DisplayOptions>(name: "displayOptions");
        modelBuilder.HasPostgresEnum<SurveyVisibility>(name: "SurveyVisibility");
        modelBuilder.HasPostgresEnum<SurveyScriptMode>(name: "SurveyScriptMode");
        modelBuilder.HasPostgresEnum<ActionType>(name: "ActionType");
        modelBuilder.HasPostgresEnum<IntegrationType>(name: "IntegrationType");
        modelBuilder.HasPostgresEnum<DataMigrationStatus>(name: "DataMigrationStatus");
        modelBuilder.HasPostgresEnum<WidgetPlacement>(name: "WidgetPlacement");
        modelBuilder.HasPostgresEnum<SurveyOverlay>(name: "SurveyOverlay");
        modelBuilder.HasPostgresEnum<BootstrapAdminMarker>(name: "BootstrapAdminMarker");
        modelBuilder.HasPostgresEnum<OrganizationRole>(name: "OrganizationRole");
        modelBuilder.HasPostgresEnum<ApiKeyPermission>(name: "ApiKeyPermission");
        modelBuilder.HasPostgresEnum<IdentityProvider>(name: "IdentityProvider");
        modelBuilder.HasPostgresEnum<EmbeddedDataSource>(name: "EmbeddedDataSource");
        modelBuilder.HasPostgresEnum<EmbeddedDataType>(name: "EmbeddedDataType");
        modelBuilder.HasPostgresEnum<TeamUserRole>(name: "TeamUserRole");
        modelBuilder.HasPostgresEnum<WorkspaceTeamPermission>(name: "WorkspaceTeamPermission");
        modelBuilder.HasPostgresEnum<ChartType>(name: "ChartType");
        modelBuilder.HasPostgresEnum<FeedbackSourceType>(name: "FeedbackSourceType");
        modelBuilder.HasPostgresEnum<FeedbackSourceStatus>(name: "FeedbackSourceStatus");
        modelBuilder.HasPostgresEnum<FeedbackSourceImportMode>(name: "FeedbackSourceImportMode");
        modelBuilder.HasPostgresEnum<FeedbackSourceElementScope>(name: "FeedbackSourceElementScope");
        modelBuilder.HasPostgresEnum<HubFieldType>(name: "HubFieldType");
        modelBuilder.HasPostgresEnum<DeletionCleanupKind>(name: "DeletionCleanupKind");
        modelBuilder.HasPostgresEnum<AttachmentExportStatus>(name: "AttachmentExportStatus");
        modelBuilder.HasPostgresEnum<RetentionEntity>(name: "RetentionEntity");
        modelBuilder.HasPostgresEnum<RetentionSurveyCondition>(name: "RetentionSurveyCondition");
        modelBuilder.HasPostgresEnum<RetentionRunKind>(name: "RetentionRunKind");
        modelBuilder.HasPostgresEnum<RetentionExemptionSource>(name: "RetentionExemptionSource");
        modelBuilder.HasPostgresEnum<RetentionTargetType>(name: "RetentionTargetType");
        modelBuilder.HasPostgresEnum<RetentionRunItemAction>(name: "RetentionRunItemAction");
        modelBuilder.HasPostgresEnum<RetentionSkipReason>(name: "RetentionSkipReason");
        modelBuilder.HasPostgresEnum<WorkflowStatus>(name: "WorkflowStatus");
        modelBuilder.HasPostgresEnum<WorkflowRunStatus>(name: "WorkflowRunStatus");
        modelBuilder.HasPostgresEnum<WorkflowRunLogStatus>(name: "WorkflowRunLogStatus");

        modelBuilder.Entity<Creative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Creative_pkey");

            entity.ToTable("Creative");

            entity.HasIndex(e => new { e.WorkspaceId, e.UpdatedAt }, "Creative_workspaceId_updated_at_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "Creative_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Name }, "Creative_workspaceId_name_idx");

            entity.HasIndex(e => e.SurveyId, "Creative_surveyId_idx");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(CreativeStatus.Draft)
                .HasColumnName("status");
            entity.Property(e => e.PublishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publishedAt");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Content)
                .HasColumnType("jsonb")
                .HasColumnName("content");
            entity.Property(e => e.Styling)
                .HasColumnType("jsonb")
                .HasColumnName("styling");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.LinkUrl).HasColumnName("linkUrl");
            entity.Property(e => e.ShowOn)
                .ValueGeneratedNever()
                .HasDefaultValue(CreativeShowOn.PageView)
                .HasColumnName("showOn");
            entity.Property(e => e.ShowConditions)
                .HasColumnType("jsonb")
                .HasColumnName("showConditions");
            entity.Property(e => e.DisplayPercentage).HasColumnName("displayPercentage");
            entity.Property(e => e.StartAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startAt");
            entity.Property(e => e.EndAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("endAt");
            entity.Property(e => e.AutoSelectLanguage).HasColumnName("autoSelectLanguage");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Creatives)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Creative_workspaceId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Creatives)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Creative_createdBy_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.Creatives)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.SurveyId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("Creative_surveyId_workspaceId_fkey");
        });

        modelBuilder.Entity<CreativeDailyStat>(entity =>
        {
            entity.HasKey(e => new { e.CreativeId, e.Date, e.DeviceType }).HasName("CreativeDailyStat_pkey");

            entity.ToTable("CreativeDailyStat");

            entity.Property(e => e.CreativeId).HasColumnName("creativeId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeviceType).HasColumnName("deviceType");
            entity.Property(e => e.Impressions)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("impressions");
            entity.Property(e => e.Clicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clicks");

            entity.HasOne(d => d.Creative).WithMany(p => p.DailyStats)
                .HasForeignKey(d => d.CreativeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeDailyStat_creativeId_fkey");
        });

        modelBuilder.Entity<CreativeDailyPageStat>(entity =>
        {
            entity.HasKey(e => new { e.CreativeId, e.Date, e.Page }).HasName("CreativeDailyPageStat_pkey");

            entity.ToTable("CreativeDailyPageStat");

            entity.Property(e => e.CreativeId).HasColumnName("creativeId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Page).HasColumnName("page");
            entity.Property(e => e.Impressions)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("impressions");
            entity.Property(e => e.Clicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clicks");

            entity.HasOne(d => d.Creative).WithMany(p => p.DailyPageStats)
                .HasForeignKey(d => d.CreativeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeDailyPageStat_creativeId_fkey");
        });

        modelBuilder.Entity<CreativeTrigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CreativeTrigger_pkey");

            entity.ToTable("CreativeTrigger");

            entity.HasIndex(e => e.ActionClassId, "CreativeTrigger_actionClassId_idx");

            entity.HasIndex(e => new { e.CreativeId, e.ActionClassId }, "CreativeTrigger_creativeId_actionClassId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.CreativeId).HasColumnName("creativeId");
            entity.Property(e => e.ActionClassId).HasColumnName("actionClassId");

            entity.HasOne(d => d.Creative).WithMany(p => p.Triggers)
                .HasForeignKey(d => d.CreativeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeTrigger_creativeId_fkey");

            entity.HasOne(d => d.ActionClass).WithMany(p => p.CreativeTriggers)
                .HasForeignKey(d => d.ActionClassId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeTrigger_actionClassId_fkey");
        });

        modelBuilder.Entity<CreativeLanguage>(entity =>
        {
            entity.HasKey(e => new { e.LanguageId, e.CreativeId }).HasName("CreativeLanguage_pkey");

            entity.ToTable("CreativeLanguage");

            entity.HasIndex(e => e.CreativeId, "CreativeLanguage_creativeId_idx");

            entity.Property(e => e.LanguageId).HasColumnName("languageId");
            entity.Property(e => e.CreativeId).HasColumnName("creativeId");
            entity.Property(e => e.Default)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("default");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");

            entity.HasOne(d => d.Language).WithMany(p => p.CreativeLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeLanguage_languageId_fkey");

            entity.HasOne(d => d.Creative).WithMany(p => p.Languages)
                .HasForeignKey(d => d.CreativeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CreativeLanguage_creativeId_fkey");
        });

        modelBuilder.Entity<OutboundEmailAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OutboundEmailAudit_pkey");

            entity.ToTable("OutboundEmailAudit");

            entity.HasIndex(e => new { e.Environment, e.CreatedAt, e.Id }, "OutboundEmailAudit_environment_createdAt_id_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt, e.Id }, "OutboundEmailAudit_organizationId_createdAt_id_idx");

            entity.HasIndex(e => new { e.SurveyId, e.CreatedAt, e.Id }, "OutboundEmailAudit_surveyId_createdAt_id_idx");

            entity.HasIndex(e => new { e.ResponseId, e.CreatedAt, e.Id }, "OutboundEmailAudit_responseId_createdAt_id_idx");

            entity.HasIndex(e => new { e.ActorUserId, e.CreatedAt, e.Id }, "OutboundEmailAudit_actorUserId_createdAt_id_idx");

            entity.HasIndex(e => new { e.ConfiguredByUserId, e.CreatedAt, e.Id }, "OutboundEmailAudit_configuredByUserId_createdAt_id_idx");

            entity.HasIndex(e => new { e.ActorUserHash, e.CreatedAt, e.Id }, "OutboundEmailAudit_actorUserHash_createdAt_id_idx");

            entity.HasIndex(e => new { e.ConfiguredByUserHash, e.CreatedAt, e.Id }, "OutboundEmailAudit_configuredByUserHash_createdAt_id_idx");

            entity.HasIndex(e => new { e.RecipientMac, e.CreatedAt, e.Id }, "OutboundEmailAudit_recipientMac_createdAt_id_idx");

            entity.HasIndex(e => e.SesMessageHash, "OutboundEmailAudit_sesMessageHash_idx");

            entity.HasIndex(e => new { e.Status, e.CreatedAt, e.Id }, "OutboundEmailAudit_status_createdAt_id_idx");

            entity.HasIndex(e => new { e.ContentExpiresAt, e.Id }, "OutboundEmailAudit_contentExpiresAt_id_idx");

            entity.HasIndex(e => new { e.ExpiresAt, e.Id }, "OutboundEmailAudit_expiresAt_id_idx");

            entity.HasIndex(e => new { e.ErasureRequestedAt, e.Id }, "OutboundEmailAudit_erasureRequestedAt_id_idx");

            entity.HasIndex(e => new { e.ContentDeleteRetryAt, e.Id }, "OutboundEmailAudit_contentDeleteRetryAt_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
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
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ContentExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("contentExpiresAt");
            entity.Property(e => e.Environment).HasColumnName("environment");
            entity.Property(e => e.EmailType).HasColumnName("emailType");
            entity.Property(e => e.Trigger).HasColumnName("trigger");
            entity.Property(e => e.ActorKind).HasColumnName("actorKind");
            entity.Property(e => e.ActorUserId).HasColumnName("actorUserId");
            entity.Property(e => e.ConfiguredByUserId).HasColumnName("configuredByUserId");
            entity.Property(e => e.ActorUserHash).HasColumnName("actorUserHash");
            entity.Property(e => e.ConfiguredByUserHash).HasColumnName("configuredByUserHash");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.ResponseId).HasColumnName("responseId");
            entity.Property(e => e.FollowUpId).HasColumnName("followUpId");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.WorkflowRunId).HasColumnName("workflowRunId");
            entity.Property(e => e.InviteId).HasColumnName("inviteId");
            entity.Property(e => e.RecipientMac).HasColumnName("recipientMac");
            entity.Property(e => e.SesMessageHash).HasColumnName("sesMessageHash");
            entity.Property(e => e.SnapshotKey).HasColumnName("snapshotKey");
            entity.Property(e => e.SnapshotSha256).HasColumnName("snapshotSha256");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("prepared")
                .HasColumnName("status");
            entity.Property(e => e.StatusRecordedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("statusRecordedAt");
            entity.Property(e => e.AuditGap)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("auditGap");
            entity.Property(e => e.ErasureRequestedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasureRequestedAt");
            entity.Property(e => e.ErasedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("erasedAt");
            entity.Property(e => e.ContentDeleteRetryAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("contentDeleteRetryAt");
            entity.Property(e => e.ContentDeleteFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("contentDeleteFailures");
        });

        modelBuilder.Entity<OutboundEmailAuditErasureIntent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OutboundEmailAuditErasureIntent_pkey");

            entity.ToTable("OutboundEmailAuditErasureIntent");

            entity.HasIndex(e => e.UserId, "OutboundEmailAuditErasureIntent_userId_idx");

            entity.HasIndex(e => new { e.RecipientMac, e.ActivatedAt }, "OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx");

            entity.HasIndex(e => new { e.UserHash, e.ActivatedAt }, "OutboundEmailAuditErasureIntent_userHash_activatedAt_idx");

            entity.HasIndex(e => new { e.CheckedAt, e.Id }, "OutboundEmailAuditErasureIntent_checkedAt_id_idx");

            entity.HasIndex(e => e.ExpiresAt, "OutboundEmailAuditErasureIntent_expiresAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserHash).HasColumnName("userHash");
            entity.Property(e => e.RecipientMac).HasColumnName("recipientMac");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ActivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("activatedAt");
            entity.Property(e => e.CheckedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("checkedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

            entity.HasIndex(e => e.WorkspaceId, "Webhook_workspaceId_idx");

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
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue(WebhookSource.User)
                .HasColumnName("source");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Triggers).HasColumnName("triggers");
            entity.Property(e => e.SurveyIds).HasColumnName("surveyIds");
            entity.Property(e => e.Secret).HasColumnName("secret");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_workspaceId_fkey");
        });

        modelBuilder.Entity<ContactAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactAttribute_pkey");

            entity.ToTable("ContactAttribute");

            entity.HasIndex(e => new { e.AttributeKeyId, e.Value }, "ContactAttribute_attributeKeyId_value_idx");

            entity.HasIndex(e => new { e.AttributeKeyId, e.ValueNumber }, "ContactAttribute_attributeKeyId_valueNumber_idx");

            entity.HasIndex(e => new { e.AttributeKeyId, e.ValueDate }, "ContactAttribute_attributeKeyId_valueDate_idx");

            entity.HasIndex(e => new { e.ContactId, e.AttributeKeyId }, "ContactAttribute_contactId_attributeKeyId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.AttributeKeyId).HasColumnName("attributeKeyId");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.ValueNumber).HasColumnName("valueNumber");
            entity.Property(e => e.ValueDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("valueDate");

            entity.HasOne(d => d.AttributeKey).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AttributeKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ContactAttribute_attributeKeyId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ContactAttribute_contactId_fkey");
        });

        modelBuilder.Entity<ContactAttributeKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactAttributeKey_pkey");

            entity.ToTable("ContactAttributeKey");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "ContactAttributeKey_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.Key, e.WorkspaceId }, "ContactAttributeKey_key_workspaceId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.IsUnique)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isUnique");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(ContactAttributeType.Custom)
                .HasColumnName("type");
            entity.Property(e => e.DataType)
                .ValueGeneratedNever()
                .HasDefaultValue(ContactAttributeDataType.String)
                .HasColumnName("dataType");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ContactAttributeKeys)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ContactAttributeKey_workspaceId_fkey");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contact_pkey");

            entity.ToTable("Contact");

            entity.HasIndex(e => e.WorkspaceId, "Contact_workspaceId_idx");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Contact_workspaceId_fkey");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Response_pkey");

            entity.ToTable("Response");

            entity.HasIndex(e => e.DisplayId, "Response_displayId_key").IsUnique();

            entity.HasIndex(e => e.CreatedAt, "Response_created_at_idx");

            entity.HasIndex(e => new { e.CreatedAt, e.Id }, "Response_created_at_id_idx");

            entity.HasIndex(e => new { e.SurveyId, e.CreatedAt }, "Response_surveyId_created_at_idx");

            entity.HasIndex(e => new { e.SurveyId, e.CreatedAt, e.Id }, "Response_surveyId_created_at_id_idx");

            entity.HasIndex(e => new { e.ContactId, e.CreatedAt }, "Response_contactId_created_at_idx");

            entity.HasIndex(e => new { e.ContactId, e.CreatedAt, e.Id }, "Response_contactId_created_at_id_idx");

            entity.HasIndex(e => new { e.SurveyId, e.Finished }, "Response_surveyId_finished_idx");

            entity.HasIndex(e => new { e.SurveyId, e.SingleUseId }, "Response_surveyId_singleUseId_key").IsUnique();

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
            entity.Property(e => e.Finished)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("finished");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.EndingId).HasColumnName("endingId");
            entity.Property(e => e.Data)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Variables)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.Ttc)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("ttc");
            entity.Property(e => e.Meta)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("meta");
            entity.Property(e => e.IngestFlags)
                .HasColumnType("jsonb")
                .HasColumnName("ingest_flags");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.ContactAttributes)
                .HasColumnType("jsonb")
                .HasColumnName("contactAttributes");
            entity.Property(e => e.SingleUseId).HasColumnName("singleUseId");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.DisplayId).HasColumnName("displayId");

            entity.HasOne(d => d.Survey).WithMany(p => p.Responses)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Response_surveyId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.Responses)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Response_contactId_fkey");

            entity.HasOne(d => d.Display).WithOne(p => p.Response)
                .HasForeignKey<Response>(d => d.DisplayId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Response_displayId_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.WorkspaceId, "Tag_workspaceId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Name }, "Tag_workspaceId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Tags)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tag_workspaceId_fkey");
        });

        modelBuilder.Entity<TagsOnResponses>(entity =>
        {
            entity.HasKey(e => new { e.ResponseId, e.TagId }).HasName("TagsOnResponses_pkey");

            entity.ToTable("TagsOnResponses");

            entity.Property(e => e.ResponseId).HasColumnName("responseId");
            entity.Property(e => e.TagId).HasColumnName("tagId");

            entity.HasOne(d => d.Response).WithMany(p => p.Tags)
                .HasForeignKey(d => d.ResponseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagsOnResponses_responseId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TagsOnResponses_tagId_fkey");
        });

        modelBuilder.Entity<Display>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Display_pkey");

            entity.ToTable("Display");

            entity.HasIndex(e => e.SurveyId, "Display_surveyId_idx");

            entity.HasIndex(e => new { e.ContactId, e.CreatedAt }, "Display_contactId_created_at_idx");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.CreativeId).HasColumnName("creativeId");

            entity.HasOne(d => d.Survey).WithMany(p => p.Displays)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Display_surveyId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.Displays)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Display_contactId_fkey");
        });

        modelBuilder.Entity<SurveyTrigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SurveyTrigger_pkey");

            entity.ToTable("SurveyTrigger");

            entity.HasIndex(e => new { e.SurveyId, e.ActionClassId }, "SurveyTrigger_surveyId_actionClassId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.ActionClassId).HasColumnName("actionClassId");

            entity.HasOne(d => d.Survey).WithMany(p => p.Triggers)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyTrigger_surveyId_fkey");

            entity.HasOne(d => d.ActionClass).WithMany(p => p.SurveyTriggers)
                .HasForeignKey(d => d.ActionClassId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyTrigger_actionClassId_fkey");
        });

        modelBuilder.Entity<SurveyAttributeFilter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SurveyAttributeFilter_pkey");

            entity.ToTable("SurveyAttributeFilter");

            entity.HasIndex(e => e.AttributeKeyId, "SurveyAttributeFilter_attributeKeyId_idx");

            entity.HasIndex(e => new { e.SurveyId, e.AttributeKeyId }, "SurveyAttributeFilter_surveyId_attributeKeyId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.AttributeKeyId).HasColumnName("attributeKeyId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.AttributeKey).WithMany(p => p.AttributeFilters)
                .HasForeignKey(d => d.AttributeKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyAttributeFilter_attributeKeyId_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.AttributeFilters)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyAttributeFilter_surveyId_fkey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Survey_pkey");

            entity.ToTable("Survey");

            entity.HasIndex(e => e.Slug, "Survey_slug_key").IsUnique();

            entity.HasIndex(e => new { e.WorkspaceId, e.UpdatedAt }, "Survey_workspaceId_updated_at_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.OwnerId }, "Survey_workspaceId_ownerId_idx");

            entity.HasIndex(e => e.SegmentId, "Survey_segmentId_idx");

            entity.HasIndex(e => new { e.Status, e.PublishOn }, "Survey_status_publishOn_idx");

            entity.HasIndex(e => new { e.Status, e.CloseOn }, "Survey_status_closeOn_idx");

            entity.HasIndex(e => e.ArchivedAt, "Survey_archivedAt_idx");

            entity.HasAlternateKey(e => new { e.Id, e.WorkspaceId }).HasName("Survey_id_workspaceId_key");

            entity.Property(e => e.HiddenFields)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"enabled\": false}")
                .HasColumnType("jsonb")
                .HasColumnName("hiddenFields");
            entity.Property(e => e.Variables)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.OwnershipInitialized)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("ownershipInitialized");
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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.RedirectUrl).HasColumnName("redirectUrl");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(SurveyType.App)
                .HasColumnName("type");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Visibility)
                .ValueGeneratedNever()
                .HasDefaultValue(SurveyVisibility.Workspace)
                .HasColumnName("visibility");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.VisibilityVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("visibilityVersion");
            entity.Property(e => e.VisibilityProjectedVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("visibilityProjectedVersion");
            entity.Property(e => e.VisibilityPending)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("visibilityPending");
            entity.Property(e => e.VisibilityChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("visibilityChangedAt");
            entity.Property(e => e.VisibilityChangedById).HasColumnName("visibilityChangedById");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(SurveyStatus.Draft)
                .HasColumnName("status");
            entity.Property(e => e.WelcomeCard)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"enabled\": false}")
                .HasColumnType("jsonb")
                .HasColumnName("welcomeCard");
            entity.Property(e => e.Questions)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("questions");
            entity.Property(e => e.Blocks)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnType("jsonb[]")
                .HasColumnName("blocks");
            entity.Property(e => e.Endings)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnType("jsonb[]")
                .HasColumnName("endings");
            entity.Property(e => e.DisplayOption)
                .ValueGeneratedNever()
                .HasDefaultValue(DisplayOptions.DisplayOnce)
                .HasColumnName("displayOption");
            entity.Property(e => e.RecontactDays).HasColumnName("recontactDays");
            entity.Property(e => e.DisplayLimit).HasColumnName("displayLimit");
            entity.Property(e => e.InlineTriggers)
                .HasColumnType("jsonb")
                .HasColumnName("inlineTriggers");
            entity.Property(e => e.AutoClose).HasColumnName("autoClose");
            entity.Property(e => e.KeepEndingCardOpen)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("keepEndingCardOpen");
            entity.Property(e => e.AutoComplete).HasColumnName("autoComplete");
            entity.Property(e => e.Delay)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("delay");
            entity.Property(e => e.PublishOn)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publishOn");
            entity.Property(e => e.CloseOn)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("closeOn");
            entity.Property(e => e.ArchivedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("archivedAt");
            entity.Property(e => e.SurveyClosedMessage)
                .HasColumnType("jsonb")
                .HasColumnName("surveyClosedMessage");
            entity.Property(e => e.SegmentId).HasColumnName("segmentId");
            entity.Property(e => e.WorkspaceOverwrites)
                .HasColumnType("jsonb")
                .HasColumnName("workspaceOverwrites");
            entity.Property(e => e.Styling)
                .HasColumnType("jsonb")
                .HasColumnName("styling");
            entity.Property(e => e.SingleUse)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"enabled\": false, \"isEncrypted\": true}")
                .HasColumnType("jsonb")
                .HasColumnName("singleUse");
            entity.Property(e => e.IsVerifyEmailEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isVerifyEmailEnabled");
            entity.Property(e => e.IsBackButtonHidden)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isBackButtonHidden");
            entity.Property(e => e.IsAutoProgressingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAutoProgressingEnabled");
            entity.Property(e => e.IsCaptureIpEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isCaptureIpEnabled");
            entity.Property(e => e.IsAnonymizeResponsesEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_anonymize_responses_enabled");
            entity.Property(e => e.Pin).HasColumnName("pin");
            entity.Property(e => e.DisplayPercentage)
                .HasPrecision(65, 30)
                .HasColumnName("displayPercentage");
            entity.Property(e => e.ShowLanguageSwitch).HasColumnName("showLanguageSwitch");
            entity.Property(e => e.AutoSelectLanguage).HasColumnName("autoSelectLanguage");
            entity.Property(e => e.Recaptcha)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"enabled\": false, \"threshold\":0.1}")
                .HasColumnType("jsonb")
                .HasColumnName("recaptcha");
            entity.Property(e => e.Metadata)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.CustomHeadScripts).HasColumnName("customHeadScripts");
            entity.Property(e => e.CustomHeadScriptsMode)
                .ValueGeneratedNever()
                .HasDefaultValue(SurveyScriptMode.Add)
                .HasColumnName("customHeadScriptsMode");
            entity.Property(e => e.CustomCss)
                .HasColumnType("jsonb")
                .HasColumnName("customCss");
            entity.Property(e => e.ShowConditions)
                .HasColumnType("jsonb")
                .HasColumnName("showConditions");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Survey_workspaceId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Survey_createdBy_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnedSurveys)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Survey_ownerId_fkey");

            entity.HasOne(d => d.Segment).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Survey_segmentId_fkey");
        });

        modelBuilder.Entity<SurveyQuota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SurveyQuota_pkey");

            entity.ToTable("SurveyQuota");

            entity.HasIndex(e => new { e.SurveyId, e.Name }, "SurveyQuota_surveyId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Limit).HasColumnName("limit");
            entity.Property(e => e.Logic)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("logic");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.EndingCardId).HasColumnName("endingCardId");
            entity.Property(e => e.CountPartialSubmissions)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("countPartialSubmissions");

            entity.HasOne(d => d.Survey).WithMany(p => p.Quotas)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyQuota_surveyId_fkey");
        });

        modelBuilder.Entity<ResponseQuotaLink>(entity =>
        {
            entity.HasKey(e => new { e.ResponseId, e.QuotaId }).HasName("ResponseQuotaLink_pkey");

            entity.ToTable("ResponseQuotaLink");

            entity.HasIndex(e => new { e.QuotaId, e.Status }, "ResponseQuotaLink_quotaId_status_idx");

            entity.Property(e => e.ResponseId).HasColumnName("responseId");
            entity.Property(e => e.QuotaId).HasColumnName("quotaId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Response).WithMany(p => p.QuotaLinks)
                .HasForeignKey(d => d.ResponseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResponseQuotaLink_responseId_fkey");

            entity.HasOne(d => d.Quota).WithMany(p => p.QuotaLinks)
                .HasForeignKey(d => d.QuotaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResponseQuotaLink_quotaId_fkey");
        });

        modelBuilder.Entity<SurveyFollowUp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SurveyFollowUp_pkey");

            entity.ToTable("SurveyFollowUp");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Trigger)
                .HasColumnType("jsonb")
                .HasColumnName("trigger");
            entity.Property(e => e.Action)
                .HasColumnType("jsonb")
                .HasColumnName("action");

            entity.HasOne(d => d.Survey).WithMany(p => p.FollowUps)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyFollowUp_surveyId_fkey");
        });

        modelBuilder.Entity<ActionClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ActionClass_pkey");

            entity.ToTable("ActionClass");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "ActionClass_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.Key, e.WorkspaceId }, "ActionClass_key_workspaceId_key").IsUnique();

            entity.HasIndex(e => new { e.Name, e.WorkspaceId }, "ActionClass_name_workspaceId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.NoCodeConfig)
                .HasColumnType("jsonb")
                .HasColumnName("noCodeConfig");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ActionClasses)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ActionClass_workspaceId_fkey");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Integration_pkey");

            entity.ToTable("Integration");

            entity.HasIndex(e => e.WorkspaceId, "Integration_workspaceId_idx");

            entity.HasIndex(e => new { e.Type, e.WorkspaceId }, "Integration_type_workspaceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Integration_workspaceId_fkey");
        });

        modelBuilder.Entity<DataMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataMigration_pkey");

            entity.ToTable("DataMigration");

            entity.HasIndex(e => e.Name, "DataMigration_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workspace_pkey");

            entity.ToTable("Workspace");

            entity.HasIndex(e => e.LegacyEnvironmentId, "Workspace_legacyEnvironmentId_key").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "Workspace_organizationId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LegacyEnvironmentId).HasColumnName("legacyEnvironmentId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Styling)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"allowStyleOverwrite\":true}")
                .HasColumnType("jsonb")
                .HasColumnName("styling");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.RecontactDays)
                .ValueGeneratedNever()
                .HasDefaultValue(7)
                .HasColumnName("recontactDays");
            entity.Property(e => e.LinkSurveyBranding)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("linkSurveyBranding");
            entity.Property(e => e.InAppSurveyBranding)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("inAppSurveyBranding");
            entity.Property(e => e.Placement)
                .ValueGeneratedNever()
                .HasDefaultValue(WidgetPlacement.BottomRight)
                .HasColumnName("placement");
            entity.Property(e => e.ClickOutsideClose)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("clickOutsideClose");
            entity.Property(e => e.Overlay)
                .ValueGeneratedNever()
                .HasDefaultValue(SurveyOverlay.None)
                .HasColumnName("overlay");
            entity.Property(e => e.OverlayColor).HasColumnName("overlayColor");
            entity.Property(e => e.OverlayOpacity).HasColumnName("overlayOpacity");
            entity.Property(e => e.Logo)
                .HasColumnType("jsonb")
                .HasColumnName("logo");
            entity.Property(e => e.AppSetupCompleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("appSetupCompleted");
            entity.Property(e => e.CustomHeadScripts).HasColumnName("customHeadScripts");
            entity.Property(e => e.CustomCss)
                .HasColumnType("jsonb")
                .HasColumnName("customCss");
            entity.Property(e => e.CustomCssPrevious)
                .HasColumnType("jsonb")
                .HasColumnName("customCssPrevious");

            entity.HasOne(d => d.Organization).WithMany(p => p.Workspaces)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Workspace_organizationId_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pkey");

            entity.ToTable("Organization");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Whitelabel)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("whitelabel");
            entity.Property(e => e.IsAISmartToolsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAISmartToolsEnabled");
            entity.Property(e => e.DisplayTimeZone).HasColumnName("displayTimeZone");
        });

        modelBuilder.Entity<OrganizationBilling>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("OrganizationBilling_pkey");

            entity.ToTable("OrganizationBilling");

            entity.HasIndex(e => e.StripeCustomerId, "OrganizationBilling_stripe_customer_id_key").IsUnique();

            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.StripeCustomerId).HasColumnName("stripe_customer_id");
            entity.Property(e => e.Limits)
                .HasColumnType("jsonb")
                .HasColumnName("limits");
            entity.Property(e => e.UsageCycleAnchor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("usage_cycle_anchor");
            entity.Property(e => e.Stripe)
                .HasColumnType("jsonb")
                .HasColumnName("stripe");
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

            entity.HasOne(d => d.Organization).WithOne(p => p.Billing)
                .HasForeignKey<OrganizationBilling>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganizationBilling_organization_id_fkey");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.OrganizationId }).HasName("Membership_pkey");

            entity.ToTable("Membership");

            entity.HasIndex(e => e.OrganizationId, "Membership_organizationId_idx");

            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Accepted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("accepted");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganizationRole.Member)
                .HasColumnName("role");

            entity.HasOne(d => d.Organization).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Membership_organizationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Membership_userId_fkey");
        });

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Invite_pkey");

            entity.ToTable("Invite");

            entity.HasIndex(e => new { e.Email, e.OrganizationId }, "Invite_email_organizationId_idx");

            entity.HasIndex(e => e.OrganizationId, "Invite_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.CreatorId).HasColumnName("creatorId");
            entity.Property(e => e.AcceptorId).HasColumnName("acceptorId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganizationRole.Member)
                .HasColumnName("role");
            entity.Property(e => e.TeamIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("teamIds");

            entity.HasOne(d => d.Organization).WithMany(p => p.Invites)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Invite_organizationId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.InvitesCreated)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Invite_creatorId_fkey");

            entity.HasOne(d => d.Acceptor).WithMany(p => p.InvitesAccepted)
                .HasForeignKey(d => d.AcceptorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Invite_acceptorId_fkey");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKey_pkey");

            entity.ToTable("ApiKey");

            entity.HasIndex(e => e.LookupHash, "ApiKey_lookupHash_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "ApiKey_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.HashedKey).HasColumnName("hashedKey");
            entity.Property(e => e.LookupHash).HasColumnName("lookupHash");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.OrganizationAccess)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("organizationAccess");

            entity.HasOne(d => d.Organization).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKey_organizationId_fkey");
        });

        modelBuilder.Entity<ApiKeyWorkspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiKeyWorkspace_pkey");

            entity.ToTable("ApiKeyWorkspace");

            entity.HasIndex(e => e.WorkspaceId, "ApiKeyWorkspace_workspaceId_idx");

            entity.HasIndex(e => new { e.ApiKeyId, e.WorkspaceId }, "ApiKeyWorkspace_apiKeyId_workspaceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
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
            entity.Property(e => e.ApiKeyId).HasColumnName("apiKeyId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Permission).HasColumnName("permission");

            entity.HasOne(d => d.ApiKey).WithMany(p => p.ApiKeyWorkspaces)
                .HasForeignKey(d => d.ApiKeyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKeyWorkspace_apiKeyId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ApiKeyWorkspaces)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiKeyWorkspace_workspaceId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserId, "Account_userId_idx");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.HasIndex(e => new { e.Issuer, e.ProviderAccountId }, "Account_issuer_providerAccountId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.ExtExpiresIn).HasColumnName("ext_expires_in");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accessTokenExpiresAt");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refreshTokenExpiresAt");
            entity.Property(e => e.Issuer).HasColumnName("issuer");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Session_pkey");

            entity.ToTable("Session");

            entity.HasIndex(e => e.SessionToken, "Session_sessionToken_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Session_userId_idx");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => new { e.Identifier, e.Token }).HasName("VerificationToken_identifier_token_key");

            entity.ToTable("VerificationToken");

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<TwoFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TwoFactor_pkey");

            entity.ToTable("TwoFactor");

            entity.HasIndex(e => e.UserId, "TwoFactor_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.BackupCodes).HasColumnName("backupCodes");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.User).WithOne(p => p.TwoFactor)
                .HasForeignKey<TwoFactor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TwoFactor_userId_fkey");
        });

        modelBuilder.Entity<Jwks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jwks_pkey");

            entity.ToTable("jwks");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
            entity.Property(e => e.PrivateKey).HasColumnName("privateKey");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.Alg).HasColumnName("alg");
            entity.Property(e => e.Crv).HasColumnName("crv");
        });

        modelBuilder.Entity<OauthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthClient_pkey");

            entity.ToTable("oauthClient");

            entity.HasAlternateKey(e => e.ClientId).HasName("oauthClient_clientId_key");

            entity.HasIndex(e => e.UserId, "oauthClient_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientSecret).HasColumnName("clientSecret");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.SkipConsent).HasColumnName("skipConsent");
            entity.Property(e => e.EnableEndSession).HasColumnName("enableEndSession");
            entity.Property(e => e.SubjectType).HasColumnName("subjectType");
            entity.Property(e => e.Scopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Uri).HasColumnName("uri");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Contacts)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("contacts");
            entity.Property(e => e.Tos).HasColumnName("tos");
            entity.Property(e => e.Policy).HasColumnName("policy");
            entity.Property(e => e.SoftwareId).HasColumnName("softwareId");
            entity.Property(e => e.SoftwareVersion).HasColumnName("softwareVersion");
            entity.Property(e => e.SoftwareStatement).HasColumnName("softwareStatement");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
            entity.Property(e => e.PostLogoutRedirectUris)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("postLogoutRedirectUris");
            entity.Property(e => e.TokenEndpointAuthMethod).HasColumnName("tokenEndpointAuthMethod");
            entity.Property(e => e.GrantTypes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("grantTypes");
            entity.Property(e => e.ResponseTypes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("responseTypes");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.RequirePKCE).HasColumnName("requirePKCE");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.ClientDiscoveryId).HasColumnName("clientDiscoveryId");
            entity.Property(e => e.ClientCredentialsScopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("clientCredentialsScopes");
            entity.Property(e => e.BackchannelLogoutUri).HasColumnName("backchannelLogoutUri");
            entity.Property(e => e.BackchannelLogoutSessionRequired).HasColumnName("backchannelLogoutSessionRequired");
            entity.Property(e => e.ApplicationType).HasColumnName("applicationType");
            entity.Property(e => e.Jwks).HasColumnName("jwks");
            entity.Property(e => e.JwksUri).HasColumnName("jwksUri");
            entity.Property(e => e.DpopBoundAccessTokens)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dpopBoundAccessTokens");

            entity.HasOne(d => d.User).WithMany(p => p.OauthClients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthClient_userId_fkey");
        });

        modelBuilder.Entity<OauthAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthAccessToken_pkey");

            entity.ToTable("oauthAccessToken");

            entity.HasIndex(e => e.Token, "oauthAccessToken_token_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "oauthAccessToken_clientId_idx");

            entity.HasIndex(e => e.SessionId, "oauthAccessToken_sessionId_idx");

            entity.HasIndex(e => e.UserId, "oauthAccessToken_userId_idx");

            entity.HasIndex(e => e.RefreshId, "oauthAccessToken_refreshId_idx");

            entity.HasIndex(e => e.AuthorizationCodeId, "oauthAccessToken_authorizationCodeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.RefreshId).HasColumnName("refreshId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.AuthorizationCodeId).HasColumnName("authorizationCodeId");
            entity.Property(e => e.Resources)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("requestedUserInfoClaims");
            entity.Property(e => e.Revoked)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked");
            entity.Property(e => e.Confirmation)
                .HasColumnType("jsonb")
                .HasColumnName("confirmation");

            entity.HasOne(d => d.Client).WithMany(p => p.AccessTokens)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthAccessToken_clientId_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.OauthAccessTokens)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("oauthAccessToken_sessionId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthAccessTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthAccessToken_userId_fkey");

            entity.HasOne(d => d.Refresh).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.RefreshId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthAccessToken_refreshId_fkey");
        });

        modelBuilder.Entity<OauthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthRefreshToken_pkey");

            entity.ToTable("oauthRefreshToken");

            entity.HasIndex(e => e.Token, "oauthRefreshToken_token_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "oauthRefreshToken_clientId_idx");

            entity.HasIndex(e => e.SessionId, "oauthRefreshToken_sessionId_idx");

            entity.HasIndex(e => e.UserId, "oauthRefreshToken_userId_idx");

            entity.HasIndex(e => e.AuthorizationCodeId, "oauthRefreshToken_authorizationCodeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Revoked)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revoked");
            entity.Property(e => e.AuthTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("authTime");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.AuthorizationCodeId).HasColumnName("authorizationCodeId");
            entity.Property(e => e.Resources)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("requestedUserInfoClaims");
            entity.Property(e => e.RotatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rotatedAt");
            entity.Property(e => e.RotationReplayResponse).HasColumnName("rotationReplayResponse");
            entity.Property(e => e.RotationReplayExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("rotationReplayExpiresAt");
            entity.Property(e => e.Confirmation)
                .HasColumnType("jsonb")
                .HasColumnName("confirmation");

            entity.HasOne(d => d.Client).WithMany(p => p.RefreshTokens)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthRefreshToken_clientId_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.OauthRefreshTokens)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("oauthRefreshToken_sessionId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthRefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthRefreshToken_userId_fkey");
        });

        modelBuilder.Entity<OauthConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthConsent_pkey");

            entity.ToTable("oauthConsent");

            entity.HasIndex(e => e.ClientId, "oauthConsent_clientId_idx");

            entity.HasIndex(e => e.UserId, "oauthConsent_userId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ReferenceId).HasColumnName("referenceId");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Resources)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("resources");
            entity.Property(e => e.RequestedUserInfoClaims)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("requestedUserInfoClaims");

            entity.HasOne(d => d.Client).WithMany(p => p.Consents)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthConsent_clientId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthConsents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthConsent_userId_fkey");
        });

        modelBuilder.Entity<OauthResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthResource_pkey");

            entity.ToTable("oauthResource");

            entity.HasAlternateKey(e => e.Identifier).HasName("oauthResource_identifier_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.AccessTokenTtl).HasColumnName("accessTokenTtl");
            entity.Property(e => e.RefreshTokenTtl).HasColumnName("refreshTokenTtl");
            entity.Property(e => e.SigningAlgorithm).HasColumnName("signingAlgorithm");
            entity.Property(e => e.SigningKeyId).HasColumnName("signingKeyId");
            entity.Property(e => e.AllowedScopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowedScopes");
            entity.Property(e => e.CustomClaims)
                .HasColumnType("jsonb")
                .HasColumnName("customClaims");
            entity.Property(e => e.DpopBoundAccessTokensRequired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dpopBoundAccessTokensRequired");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.PolicyVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("policyVersion");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<OauthClientResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthClientResource_pkey");

            entity.ToTable("oauthClientResource");

            entity.HasIndex(e => e.ResourceId, "oauthClientResource_resourceId_idx");

            entity.HasIndex(e => new { e.ClientId, e.ResourceId }, "oauthClientResource_clientId_resourceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Client).WithMany(p => p.Resources)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthClientResource_clientId_fkey");

            entity.HasOne(d => d.Resource).WithMany(p => p.Clients)
                .HasPrincipalKey(p => p.Identifier)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauthClientResource_resourceId_fkey");
        });

        modelBuilder.Entity<OauthClientAssertion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauthClientAssertion_pkey");

            entity.ToTable("oauthClientAssertion");

            entity.HasIndex(e => e.ExpiresAt, "oauthClientAssertion_expiresAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PasswordResetToken_pkey");

            entity.ToTable("PasswordResetToken");

            entity.HasIndex(e => e.TokenHash, "PasswordResetToken_token_hash_key").IsUnique();

            entity.HasIndex(e => e.UserId, "PasswordResetToken_userId_key").IsUnique();

            entity.HasIndex(e => e.ExpiresAt, "PasswordResetToken_expires_at_idx");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.TokenHash).HasColumnName("token_hash");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.PasswordResetToken)
                .HasForeignKey<PasswordResetToken>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PasswordResetToken_userId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.IsBootstrapAdmin, "User_isBootstrapAdmin_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("email_verified");
            entity.Property(e => e.TwoFactorSecret).HasColumnName("twoFactorSecret");
            entity.Property(e => e.TwoFactorEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("twoFactorEnabled");
            entity.Property(e => e.BackupCodes).HasColumnName("backupCodes");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.IdentityProvider)
                .ValueGeneratedNever()
                .HasDefaultValue(IdentityProvider.Email)
                .HasColumnName("identityProvider");
            entity.Property(e => e.IdentityProviderAccountId).HasColumnName("identityProviderAccountId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.NotificationSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("notificationSettings");
            entity.Property(e => e.Locale)
                .ValueGeneratedNever()
                .HasDefaultValue("en-US")
                .HasColumnName("locale");
            entity.Property(e => e.LastLoginAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastLoginAt");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.ReactivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("reactivatedAt");
            entity.Property(e => e.LastActiveAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActiveAt");
            entity.Property(e => e.IsBootstrapAdmin).HasColumnName("isBootstrapAdmin");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Segment_pkey");

            entity.ToTable("Segment");

            entity.HasIndex(e => e.WorkspaceId, "Segment_workspaceId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Title }, "Segment_workspaceId_title_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsPrivate)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isPrivate");
            entity.Property(e => e.Filters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Segments)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Segment_workspaceId_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Language_pkey");

            entity.ToTable("Language");

            entity.HasIndex(e => new { e.WorkspaceId, e.Code }, "Language_workspaceId_code_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Languages)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Language_workspaceId_fkey");
        });

        modelBuilder.Entity<SurveyLanguage>(entity =>
        {
            entity.HasKey(e => new { e.LanguageId, e.SurveyId }).HasName("SurveyLanguage_pkey");

            entity.ToTable("SurveyLanguage");

            entity.HasIndex(e => e.SurveyId, "SurveyLanguage_surveyId_idx");

            entity.Property(e => e.LanguageId).HasColumnName("languageId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.Default)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("default");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");

            entity.HasOne(d => d.Language).WithMany(p => p.SurveyLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyLanguage_languageId_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.Languages)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyLanguage_surveyId_fkey");
        });

        modelBuilder.Entity<EmbeddedData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmbeddedData_pkey");

            entity.ToTable("EmbeddedData");

            entity.HasIndex(e => e.SurveyId, "EmbeddedData_surveyId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Key }, "EmbeddedData_workspaceId_key_key").IsUnique();

            entity.HasAlternateKey(e => new { e.Id, e.WorkspaceId }).HasName("EmbeddedData_id_workspaceId_key");

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.DataType)
                .ValueGeneratedNever()
                .HasDefaultValue(EmbeddedDataType.String)
                .HasColumnName("dataType");
            entity.Property(e => e.DefaultValue)
                .HasColumnType("jsonb")
                .HasColumnName("defaultValue");
            entity.Property(e => e.Locked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("locked");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Survey).WithMany(p => p.EmbeddedData)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.SurveyId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmbeddedData_surveyId_workspaceId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.EmbeddedData)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmbeddedData_workspaceId_fkey");
        });

        modelBuilder.Entity<SurveyEmbeddedData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SurveyEmbeddedData_pkey");

            entity.ToTable("SurveyEmbeddedData");

            entity.HasIndex(e => e.EmbeddedDataId, "SurveyEmbeddedData_embeddedDataId_idx");

            entity.HasIndex(e => new { e.SurveyId, e.Order }, "SurveyEmbeddedData_surveyId_order_idx");

            entity.HasIndex(e => new { e.SurveyId, e.EmbeddedDataId }, "SurveyEmbeddedData_surveyId_embeddedDataId_key").IsUnique();

            entity.HasIndex(e => new { e.SurveyId, e.StorageKey }, "SurveyEmbeddedData_surveyId_storageKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.EmbeddedDataId).HasColumnName("embeddedDataId");
            entity.Property(e => e.StorageKey).HasColumnName("storageKey");
            entity.Property(e => e.Order).HasColumnName("order");

            entity.HasOne(d => d.Survey).WithMany(p => p.EmbeddedDataLinks)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.SurveyId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyEmbeddedData_surveyId_workspaceId_fkey");

            entity.HasOne(d => d.EmbeddedData).WithMany(p => p.SurveyLinks)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.EmbeddedDataId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SurveyEmbeddedData_embeddedDataId_workspaceId_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "Team_organizationId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

            entity.HasOne(d => d.Organization).WithMany(p => p.Teams)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Team_organizationId_fkey");
        });

        modelBuilder.Entity<TeamUser>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.UserId }).HasName("TeamUser_pkey");

            entity.ToTable("TeamUser");

            entity.HasIndex(e => e.UserId, "TeamUser_userId_idx");

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
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamUsers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamUser_teamId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TeamUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamUser_userId_fkey");
        });

        modelBuilder.Entity<WorkspaceTeam>(entity =>
        {
            entity.HasKey(e => new { e.WorkspaceId, e.TeamId }).HasName("WorkspaceTeam_pkey");

            entity.ToTable("WorkspaceTeam");

            entity.HasIndex(e => e.TeamId, "WorkspaceTeam_teamId_idx");

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
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Permission)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkspaceTeamPermission.Read)
                .HasColumnName("permission");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceTeams)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkspaceTeam_workspaceId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.WorkspaceTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkspaceTeam_teamId_fkey");
        });

        modelBuilder.Entity<Chart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chart_pkey");

            entity.ToTable("Chart");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "Chart_workspaceId_created_at_idx");

            entity.HasIndex(e => e.FeedbackDirectoryId, "Chart_feedbackDirectoryId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Name }, "Chart_workspaceId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Query)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("query");
            entity.Property(e => e.Config)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.FeedbackDirectoryId).HasColumnName("feedbackDirectoryId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Charts)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chart_workspaceId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Charts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Chart_createdBy_fkey");

            entity.HasOne(d => d.FeedbackDirectory).WithMany(p => p.Charts)
                .HasForeignKey(d => d.FeedbackDirectoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Chart_feedbackDirectoryId_fkey");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dashboard_pkey");

            entity.ToTable("Dashboard");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "Dashboard_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Name }, "Dashboard_workspaceId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Dashboard_workspaceId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Dashboard_createdBy_fkey");
        });

        modelBuilder.Entity<DashboardWidget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardWidget_pkey");

            entity.ToTable("DashboardWidget");

            entity.HasIndex(e => new { e.DashboardId, e.Order }, "DashboardWidget_dashboardId_order_idx");

            entity.HasIndex(e => new { e.DashboardId, e.ChartId }, "DashboardWidget_dashboardId_chartId_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
            entity.Property(e => e.ChartId).HasColumnName("chartId");
            entity.Property(e => e.Layout)
                .ValueGeneratedNever()
                .HasDefaultValue("{\"x\":0,\"y\":0,\"w\":4,\"h\":3}")
                .HasColumnType("jsonb")
                .HasColumnName("layout");
            entity.Property(e => e.Order)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("order");

            entity.HasOne(d => d.Dashboard).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.DashboardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DashboardWidget_dashboardId_fkey");

            entity.HasOne(d => d.Chart).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.ChartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DashboardWidget_chartId_fkey");
        });

        modelBuilder.Entity<DeletionCleanup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DeletionCleanup_pkey");

            entity.ToTable("DeletionCleanup");

            entity.HasIndex(e => e.NextAttemptAt, "DeletionCleanup_nextAttemptAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt }, "DeletionCleanup_organizationId_created_at_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
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
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.TenantIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("tenantIds");
            entity.Property(e => e.ResponseIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("responseIds");
            entity.Property(e => e.FileKeys)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("fileKeys");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.NextAttemptAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextAttemptAt");
            entity.Property(e => e.LastError).HasColumnName("lastError");
        });

        modelBuilder.Entity<AttachmentExport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AttachmentExport_pkey");

            entity.ToTable("AttachmentExport");

            entity.HasIndex(e => new { e.SurveyId, e.RequestedById, e.CreatedAt }, "AttachmentExport_surveyId_requestedById_created_at_idx");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "AttachmentExport_status_created_at_idx");

            entity.HasIndex(e => e.ExpiresAt, "AttachmentExport_expiresAt_idx");

            entity.HasIndex(e => e.WorkspaceId, "AttachmentExport_workspaceId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
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
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.RequestedById).HasColumnName("requestedById");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(AttachmentExportStatus.Queued)
                .HasColumnName("status");
            entity.Property(e => e.FilterCriteria)
                .HasColumnType("jsonb")
                .HasColumnName("filterCriteria");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");
            entity.Property(e => e.DispatchCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("dispatchCount");
            entity.Property(e => e.FileCount).HasColumnName("fileCount");
            entity.Property(e => e.ResponseCount).HasColumnName("responseCount");
            entity.Property(e => e.IncludedFileCount).HasColumnName("includedFileCount");
            entity.Property(e => e.TotalBytes).HasColumnName("totalBytes");
            entity.Property(e => e.Truncated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("truncated");
            entity.Property(e => e.StorageKey).HasColumnName("storageKey");
            entity.Property(e => e.ErrorCode).HasColumnName("errorCode");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finishedAt");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");

            entity.HasOne(d => d.Survey).WithMany(p => p.AttachmentExports)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.SurveyId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttachmentExport_surveyId_workspaceId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.AttachmentExports)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AttachmentExport_workspaceId_fkey");

            entity.HasOne(d => d.RequestedBy).WithMany(p => p.AttachmentExports)
                .HasForeignKey(d => d.RequestedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("AttachmentExport_requestedById_fkey");
        });

        modelBuilder.Entity<FeedbackSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedbackSource_pkey");

            entity.ToTable("FeedbackSource");

            entity.HasIndex(e => e.Type, "FeedbackSource_type_idx");

            entity.HasIndex(e => new { e.FeedbackDirectoryId, e.WorkspaceId }, "FeedbackSource_feedbackDirectoryId_workspaceId_idx");

            entity.HasAlternateKey(e => new { e.Id, e.WorkspaceId }).HasName("FeedbackSource_id_workspaceId_key");

            entity.HasIndex(e => new { e.WorkspaceId, e.Name }, "FeedbackSource_workspaceId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(FeedbackSourceStatus.Active)
                .HasColumnName("status");
            entity.Property(e => e.ImportMode)
                .ValueGeneratedNever()
                .HasDefaultValue(FeedbackSourceImportMode.CompletedOnly)
                .HasColumnName("import_mode");
            entity.Property(e => e.ElementScope)
                .ValueGeneratedNever()
                .HasDefaultValue(FeedbackSourceElementScope.Specific)
                .HasColumnName("elementScope");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.FeedbackDirectoryId).HasColumnName("feedbackDirectoryId");
            entity.Property(e => e.LastSyncAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_sync_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");

            entity.HasOne(d => d.Workspace).WithMany(p => p.FeedbackSources)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackSource_workspaceId_fkey");

            entity.HasOne(d => d.FeedbackDirectory).WithMany(p => p.FeedbackSources)
                .HasForeignKey(d => d.FeedbackDirectoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackSource_feedbackDirectoryId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.FeedbackSources)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FeedbackSource_created_by_fkey");

            entity.HasOne(d => d.FeedbackDirectoryWorkspace).WithMany(p => p.FeedbackSources)
                .HasForeignKey(d => new { d.FeedbackDirectoryId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FeedbackSource_feedbackDirectoryId_workspaceId_fkey");
        });

        modelBuilder.Entity<FeedbackSourceFormbricksMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedbackSourceFormbricksMapping_pkey");

            entity.ToTable("FeedbackSourceFormbricksMapping");

            entity.HasIndex(e => new { e.WorkspaceId, e.SurveyId }, "FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx");

            entity.HasIndex(e => e.SurveyId, "FeedbackSourceFormbricksMapping_surveyId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.FeedbackSourceId, e.SurveyId, e.ElementId }, "FeedbackSourceFormbricksMapping_workspaceId_feedbackSourceId_su").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FeedbackSourceId).HasColumnName("feedback_source_id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.ElementId).HasColumnName("elementId");
            entity.Property(e => e.HubFieldType).HasColumnName("hubFieldType");
            entity.Property(e => e.CustomFieldLabel).HasColumnName("custom_field_label");

            entity.HasOne(d => d.FeedbackSource).WithMany(p => p.FormbricksMappings)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.FeedbackSourceId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackSourceFormbricksMapping_feedback_source_id_workspa_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.FeedbackSourceMappings)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.SurveyId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackSourceFormbricksMapping_surveyId_workspaceId_fkey");
        });

        modelBuilder.Entity<FeedbackSourceFieldMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedbackSourceFieldMapping_pkey");

            entity.ToTable("FeedbackSourceFieldMapping");

            entity.HasIndex(e => new { e.WorkspaceId, e.FeedbackSourceId, e.SourceFieldId, e.TargetFieldId }, "FeedbackSourceFieldMapping_workspaceId_feedbackSourceId_sourceF").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FeedbackSourceId).HasColumnName("feedback_source_id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.SourceFieldId).HasColumnName("source_field_id");
            entity.Property(e => e.TargetFieldId).HasColumnName("target_field_id");
            entity.Property(e => e.StaticValue).HasColumnName("static_value");

            entity.HasOne(d => d.FeedbackSource).WithMany(p => p.FieldMappings)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.FeedbackSourceId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackSourceFieldMapping_feedback_source_id_workspaceId_fkey");
        });

        modelBuilder.Entity<FeedbackDirectory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedbackDirectory_pkey");

            entity.ToTable("FeedbackDirectory");

            entity.HasIndex(e => new { e.OrganizationId, e.Name }, "FeedbackDirectory_organizationId_name_key").IsUnique();

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
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

            entity.HasOne(d => d.Organization).WithMany(p => p.FeedbackDirectories)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackDirectory_organizationId_fkey");
        });

        modelBuilder.Entity<FeedbackDirectoryWorkspace>(entity =>
        {
            entity.HasKey(e => new { e.FeedbackDirectoryId, e.WorkspaceId }).HasName("FeedbackDirectoryWorkspace_pkey");

            entity.ToTable("FeedbackDirectoryWorkspace");

            entity.HasIndex(e => e.WorkspaceId, "FeedbackDirectoryWorkspace_workspaceId_idx");

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
            entity.Property(e => e.FeedbackDirectoryId).HasColumnName("feedbackDirectoryId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.FeedbackDirectory).WithMany(p => p.Workspaces)
                .HasForeignKey(d => d.FeedbackDirectoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackDirectoryWorkspace_feedbackDirectoryId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.FeedbackDirectoryWorkspaces)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FeedbackDirectoryWorkspace_workspaceId_fkey");
        });

        modelBuilder.Entity<AuthzedProjectionScopeState>(entity =>
        {
            entity.HasKey(e => e.Scope).HasName("AuthzedProjectionScopeState_pkey");

            entity.ToTable("AuthzedProjectionScopeState");

            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ReadyAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("readyAt");
            entity.Property(e => e.ReadyBy).HasColumnName("readyBy");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<AuthzedProjectionOutbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuthzedProjectionOutbox_pkey");

            entity.ToTable("AuthzedProjectionOutbox");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.PrimaryId).HasColumnName("primaryId");
            entity.Property(e => e.SecondaryId).HasColumnName("secondaryId");
            entity.Property(e => e.IsRevocation)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRevocation");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.PermanentFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("permanentFailures");
            entity.Property(e => e.AvailableAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("availableAt");
            entity.Property(e => e.LeasedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("leasedAt");
            entity.Property(e => e.LeaseExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("leaseExpiresAt");
            entity.Property(e => e.LeaseOwner).HasColumnName("leaseOwner");
            entity.Property(e => e.ProcessedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("processedAt");
            entity.Property(e => e.DeadLetteredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deadLetteredAt");
            entity.Property(e => e.LastAttemptAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastAttemptAt");
            entity.Property(e => e.LastErrorCode).HasColumnName("lastErrorCode");
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

        modelBuilder.Entity<OnlineUpgradeState>(entity =>
        {
            entity.HasKey(e => e.Release).HasName("OnlineUpgradeState_pkey");

            entity.ToTable("OnlineUpgradeState");

            entity.Property(e => e.Release).HasColumnName("release");
            entity.Property(e => e.TargetImage).HasColumnName("targetImage");
            entity.Property(e => e.Phase).HasColumnName("phase");
            entity.Property(e => e.CompletedPhases)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("completedPhases");
            entity.Property(e => e.SurveyCursor).HasColumnName("surveyCursor");
            entity.Property(e => e.SurveyCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("surveyCount");
            entity.Property(e => e.BaselineCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("baselineCompletedAt");
            entity.Property(e => e.ForwardOnlyAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("forwardOnlyAt");
            entity.Property(e => e.QueuePausedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queuePausedAt");
            entity.Property(e => e.QueuePauseDeadlineAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queuePauseDeadlineAt");
            entity.Property(e => e.QueueResumedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queueResumedAt");
            entity.Property(e => e.ActivatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("activatedAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<RetentionPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionPolicy_pkey");

            entity.ToTable("RetentionPolicy");

            entity.HasIndex(e => e.UpdatedById, "RetentionPolicy_updatedById_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.Entity }, "RetentionPolicy_organizationId_entity_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
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
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Entity).HasColumnName("entity");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.EnabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enabledAt");
            entity.Property(e => e.WarnDays)
                .ValueGeneratedNever()
                .HasDefaultValue(60)
                .HasColumnName("warnDays");
            entity.Property(e => e.PeriodDays).HasColumnName("periodDays");
            entity.Property(e => e.Conditions)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<RetentionSurveyCondition>())
                .HasColumnName("conditions");
            entity.Property(e => e.DeferredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deferredAt");
            entity.Property(e => e.UpdatedById).HasColumnName("updatedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.RetentionPolicies)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionPolicy_organizationId_fkey");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.RetentionPoliciesUpdated)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RetentionPolicy_updatedById_fkey");
        });

        modelBuilder.Entity<RetentionExemption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionExemption_pkey");

            entity.ToTable("RetentionExemption");

            entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt, e.Id }, "RetentionExemption_organizationId_created_at_id_idx");

            entity.HasIndex(e => e.SurveyId, "RetentionExemption_surveyId_idx");

            entity.HasIndex(e => e.CreatedById, "RetentionExemption_createdById_idx");

            entity.HasIndex(e => e.RevokedById, "RetentionExemption_revokedById_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Entity).HasColumnName("entity");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.Until)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("until");
            entity.Property(e => e.Source)
                .ValueGeneratedNever()
                .HasDefaultValue(RetentionExemptionSource.Manual)
                .HasColumnName("source");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
            entity.Property(e => e.RevokedById).HasColumnName("revokedById");

            entity.HasOne(d => d.Organization).WithMany(p => p.RetentionExemptions)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionExemption_organizationId_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.RetentionExemptions)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionExemption_surveyId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.ExemptionsCreated)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RetentionExemption_createdById_fkey");

            entity.HasOne(d => d.RevokedBy).WithMany(p => p.ExemptionsRevoked)
                .HasForeignKey(d => d.RevokedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RetentionExemption_revokedById_fkey");
        });

        modelBuilder.Entity<RetentionRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionRun_pkey");

            entity.ToTable("RetentionRun");

            entity.HasIndex(e => new { e.OrganizationId, e.StartedAt, e.Id }, "RetentionRun_organizationId_startedAt_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Entity).HasColumnName("entity");
            entity.Property(e => e.Kind)
                .ValueGeneratedNever()
                .HasDefaultValue(RetentionRunKind.Sweep)
                .HasColumnName("kind");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finishedAt");
            entity.Property(e => e.NotifiedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("notifiedCount");
            entity.Property(e => e.ArchivedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("archivedCount");
            entity.Property(e => e.DeletedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("deletedCount");
            entity.Property(e => e.SkippedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("skippedCount");
            entity.Property(e => e.HasChanges)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("hasChanges");
            entity.Property(e => e.ScanCursor).HasColumnName("scanCursor");

            entity.HasOne(d => d.Organization).WithMany(p => p.RetentionRuns)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionRun_organizationId_fkey");
        });

        modelBuilder.Entity<RetentionRunItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionRunItem_pkey");

            entity.ToTable("RetentionRunItem");

            entity.HasIndex(e => new { e.RunId, e.Id }, "RetentionRunItem_runId_id_idx");

            entity.HasIndex(e => e.TargetId, "RetentionRunItem_targetId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.TargetName).HasColumnName("targetName");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("count");
            entity.Property(e => e.Recipient).HasColumnName("recipient");
            entity.Property(e => e.SkipReason).HasColumnName("skipReason");

            entity.HasOne(d => d.Run).WithMany(p => p.Items)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionRunItem_runId_fkey");
        });

        modelBuilder.Entity<RetentionNotice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RetentionNotice_pkey");

            entity.ToTable("RetentionNotice");

            entity.HasIndex(e => e.OrganizationId, "RetentionNotice_organizationId_idx");

            entity.HasIndex(e => new { e.SurveyId, e.Entity }, "RetentionNotice_surveyId_entity_key").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.OrganizationId, e.Entity }, "RetentionNotice_userId_organizationId_entity_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Entity).HasColumnName("entity");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.SentAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.DeliveredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deliveredAt");
            entity.Property(e => e.EmailSent)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailSent");
            entity.Property(e => e.ClaimToken).HasColumnName("claimToken");
            entity.Property(e => e.ClockAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("clockAt");

            entity.HasOne(d => d.Organization).WithMany(p => p.RetentionNotices)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionNotice_organizationId_fkey");

            entity.HasOne(d => d.Survey).WithMany(p => p.RetentionNotices)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionNotice_surveyId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.RetentionNotices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionNotice_userId_fkey");
        });

        modelBuilder.Entity<RetentionSurveyRestore>(entity =>
        {
            entity.HasKey(e => e.SurveyId).HasName("RetentionSurveyRestore_pkey");

            entity.ToTable("RetentionSurveyRestore");

            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.RestoredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("restoredAt");

            entity.HasOne(d => d.Survey).WithOne(p => p.RetentionRestore)
                .HasForeignKey<RetentionSurveyRestore>(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RetentionSurveyRestore_surveyId_fkey");
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workflow_pkey");

            entity.ToTable("Workflow");

            entity.HasIndex(e => new { e.WorkspaceId, e.Status }, "Workflow_workspaceId_status_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.UpdatedAt }, "Workflow_workspaceId_updated_at_idx");

            entity.HasAlternateKey(e => new { e.Id, e.WorkspaceId }).HasName("Workflow_id_workspaceId_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
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
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkflowStatus.Draft)
                .HasColumnName("status");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Definition)
                .HasColumnType("jsonb")
                .HasColumnName("definition");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Workflow_workspaceId_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Workflow_createdBy_fkey");
        });

        modelBuilder.Entity<WorkflowVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowVersion_pkey");

            entity.ToTable("WorkflowVersion");

            entity.HasIndex(e => new { e.WorkflowId, e.WorkspaceId }, "WorkflowVersion_workflowId_workspaceId_idx");

            entity.HasIndex(e => e.PublishedBy, "WorkflowVersion_publishedBy_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.Version }, "WorkflowVersion_workflowId_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Definition)
                .HasColumnType("jsonb")
                .HasColumnName("definition");
            entity.Property(e => e.PublishedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publishedAt");
            entity.Property(e => e.PublishedBy).HasColumnName("publishedBy");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Versions)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.WorkflowId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowVersion_workflowId_workspaceId_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.WorkflowVersionsPublished)
                .HasForeignKey(d => d.PublishedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WorkflowVersion_publishedBy_fkey");
        });

        modelBuilder.Entity<WorkflowRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowRun_pkey");

            entity.ToTable("WorkflowRun");

            entity.HasIndex(e => new { e.WorkspaceId, e.CreatedAt }, "WorkflowRun_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.WorkspaceId, e.CreatedAt }, "WorkflowRun_workflowId_workspaceId_created_at_idx");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "WorkflowRun_status_created_at_idx");

            entity.HasIndex(e => e.ResponseId, "WorkflowRun_responseId_idx");

            entity.HasIndex(e => e.IsDryRun, "WorkflowRun_isDryRun_idx");

            entity.HasIndex(e => e.NextAttemptAt, "WorkflowRun_nextAttemptAt_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.IdempotencyKey }, "WorkflowRun_workflowId_idempotencyKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
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
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.WorkflowVersionId).HasColumnName("workflowVersionId");
            entity.Property(e => e.ResponseId).HasColumnName("responseId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkflowRunStatus.Queued)
                .HasColumnName("status");
            entity.Property(e => e.TriggerType).HasColumnName("triggerType");
            entity.Property(e => e.SurveyId).HasColumnName("surveyId");
            entity.Property(e => e.IsDryRun)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDryRun");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.Attempt)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempt");
            entity.Property(e => e.NextAttemptAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextAttemptAt");
            entity.Property(e => e.LastErrorAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastErrorAt");
            entity.Property(e => e.TriggerPayload)
                .HasColumnType("jsonb")
                .HasColumnName("triggerPayload");
            entity.Property(e => e.Data)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.DispatchedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dispatchedAt");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finishedAt");

            entity.HasOne(d => d.Workflow).WithMany(p => p.Runs)
                .HasPrincipalKey(p => new { p.Id, p.WorkspaceId })
                .HasForeignKey(d => new { d.WorkflowId, d.WorkspaceId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowRun_workflowId_workspaceId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkflowRuns)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowRun_workspaceId_fkey");

            entity.HasOne(d => d.WorkflowVersion).WithMany(p => p.Runs)
                .HasForeignKey(d => d.WorkflowVersionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("WorkflowRun_workflowVersionId_fkey");

            entity.HasOne(d => d.Response).WithMany(p => p.WorkflowRuns)
                .HasForeignKey(d => d.ResponseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowRun_responseId_fkey");
        });

        modelBuilder.Entity<WorkflowRunLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkflowRunLog_pkey");

            entity.ToTable("WorkflowRunLog");

            entity.HasIndex(e => new { e.RunId, e.Sequence }, "WorkflowRunLog_runId_sequence_idx");

            entity.HasIndex(e => new { e.RunId, e.StepId }, "WorkflowRunLog_runId_stepId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<Cuid2Generator>()
                .HasColumnName("id");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.StepId).HasColumnName("stepId");
            entity.Property(e => e.StepType).HasColumnName("stepType");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkflowRunLogStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Input)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.Output)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finishedAt");

            entity.HasOne(d => d.Run).WithMany(p => p.Logs)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkflowRunLog_runId_fkey");
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
        foreach (var entry in ChangeTracker.Entries<Creative>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CreativeTrigger>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OutboundEmailAudit>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Webhook>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ContactAttribute>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ContactAttributeKey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Contact>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Response>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Tag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Display>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SurveyTrigger>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SurveyAttributeFilter>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Survey>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SurveyQuota>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SurveyFollowUp>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ActionClass>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Workspace>())
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

        foreach (var entry in ChangeTracker.Entries<OrganizationBilling>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ApiKeyWorkspace>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Account>())
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

        foreach (var entry in ChangeTracker.Entries<PasswordResetToken>())
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

        foreach (var entry in ChangeTracker.Entries<Segment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Language>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmbeddedData>())
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

        foreach (var entry in ChangeTracker.Entries<TeamUser>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkspaceTeam>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Chart>())
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

        foreach (var entry in ChangeTracker.Entries<DeletionCleanup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttachmentExport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeedbackSource>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeedbackDirectory>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<FeedbackDirectoryWorkspace>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthzedProjectionScopeState>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuthzedProjectionOutbox>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OnlineUpgradeState>())
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

        foreach (var entry in ChangeTracker.Entries<Workflow>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowRun>())
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

file sealed class Cuid2Generator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => new Cuid2().ToString();
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
