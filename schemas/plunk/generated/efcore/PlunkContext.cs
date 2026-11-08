#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Plunk.Models;

public partial class PlunkContext : DbContext
{
    public PlunkContext(DbContextOptions<PlunkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<SegmentMembership> SegmentMemberships { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public virtual DbSet<WorkflowTransition> WorkflowTransitions { get; set; }

    public virtual DbSet<WorkflowExecution> WorkflowExecutions { get; set; }

    public virtual DbSet<WorkflowStepExecution> WorkflowStepExecutions { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<IdempotencyKey> IdempotencyKeys { get; set; }

    public virtual DbSet<ThrottleClaim> ThrottleClaims { get; set; }

    public virtual DbSet<ApiRequest> ApiRequests { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ProjectDisabledReason>("ProjectDisabledReason");
        npgsql.MapEnum<CardVerificationStatus>("CardVerificationStatus");
        npgsql.MapEnum<AuthMethod>("AuthMethod");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<TemplateType>("TemplateType");
        npgsql.MapEnum<TrackingMode>("TrackingMode");
        npgsql.MapEnum<CampaignStatus>("CampaignStatus");
        npgsql.MapEnum<CampaignAudienceType>("CampaignAudienceType");
        npgsql.MapEnum<WorkflowTriggerType>("WorkflowTriggerType");
        npgsql.MapEnum<WorkflowStepType>("WorkflowStepType");
        npgsql.MapEnum<WorkflowExecutionStatus>("WorkflowExecutionStatus");
        npgsql.MapEnum<StepExecutionStatus>("StepExecutionStatus");
        npgsql.MapEnum<SegmentType>("SegmentType");
        npgsql.MapEnum<EmailSourceType>("EmailSourceType");
        npgsql.MapEnum<EmailStatus>("EmailStatus");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ProjectDisabledReason>("ProjectDisabledReason");
        dataSource.MapEnum<CardVerificationStatus>("CardVerificationStatus");
        dataSource.MapEnum<AuthMethod>("AuthMethod");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<TemplateType>("TemplateType");
        dataSource.MapEnum<TrackingMode>("TrackingMode");
        dataSource.MapEnum<CampaignStatus>("CampaignStatus");
        dataSource.MapEnum<CampaignAudienceType>("CampaignAudienceType");
        dataSource.MapEnum<WorkflowTriggerType>("WorkflowTriggerType");
        dataSource.MapEnum<WorkflowStepType>("WorkflowStepType");
        dataSource.MapEnum<WorkflowExecutionStatus>("WorkflowExecutionStatus");
        dataSource.MapEnum<StepExecutionStatus>("StepExecutionStatus");
        dataSource.MapEnum<SegmentType>("SegmentType");
        dataSource.MapEnum<EmailSourceType>("EmailSourceType");
        dataSource.MapEnum<EmailStatus>("EmailStatus");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProjectDisabledReason>(name: "ProjectDisabledReason");
        modelBuilder.HasPostgresEnum<CardVerificationStatus>(name: "CardVerificationStatus");
        modelBuilder.HasPostgresEnum<AuthMethod>(name: "AuthMethod");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<TemplateType>(name: "TemplateType");
        modelBuilder.HasPostgresEnum<TrackingMode>(name: "TrackingMode");
        modelBuilder.HasPostgresEnum<CampaignStatus>(name: "CampaignStatus");
        modelBuilder.HasPostgresEnum<CampaignAudienceType>(name: "CampaignAudienceType");
        modelBuilder.HasPostgresEnum<WorkflowTriggerType>(name: "WorkflowTriggerType");
        modelBuilder.HasPostgresEnum<WorkflowStepType>(name: "WorkflowStepType");
        modelBuilder.HasPostgresEnum<WorkflowExecutionStatus>(name: "WorkflowExecutionStatus");
        modelBuilder.HasPostgresEnum<StepExecutionStatus>(name: "StepExecutionStatus");
        modelBuilder.HasPostgresEnum<SegmentType>(name: "SegmentType");
        modelBuilder.HasPostgresEnum<EmailSourceType>(name: "EmailSourceType");
        modelBuilder.HasPostgresEnum<EmailStatus>(name: "EmailStatus");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.EmailVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailVerified");
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

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.HasIndex(e => e.Public, "projects_public_key").IsUnique();

            entity.HasIndex(e => e.Secret, "projects_secret_key").IsUnique();

            entity.HasIndex(e => e.Customer, "projects_customer_key").IsUnique();

            entity.HasIndex(e => e.Subscription, "projects_subscription_key").IsUnique();

            entity.HasIndex(e => new { e.CardVerification, e.CardVerificationAt }, "projects_cardVerification_cardVerificationAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.DisabledReason).HasColumnName("disabledReason");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Subscription).HasColumnName("subscription");
            entity.Property(e => e.CardVerification).HasColumnName("cardVerification");
            entity.Property(e => e.CardVerificationIntent).HasColumnName("cardVerificationIntent");
            entity.Property(e => e.CardVerificationAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cardVerificationAt");
            entity.Property(e => e.CardVerificationSession).HasColumnName("cardVerificationSession");
            entity.Property(e => e.CardVerificationPaymentMethod).HasColumnName("cardVerificationPaymentMethod");
            entity.Property(e => e.CardVerificationFingerprint).HasColumnName("cardVerificationFingerprint");
            entity.Property(e => e.CardVerificationAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("cardVerificationAttempts");
            entity.Property(e => e.BillingLimitWorkflows).HasColumnName("billingLimitWorkflows");
            entity.Property(e => e.BillingLimitCampaigns).HasColumnName("billingLimitCampaigns");
            entity.Property(e => e.BillingLimitTransactional).HasColumnName("billingLimitTransactional");
            entity.Property(e => e.BillingLimitInbound).HasColumnName("billingLimitInbound");
            entity.Property(e => e.Tracking)
                .ValueGeneratedNever()
                .HasDefaultValue(TrackingMode.Enabled)
                .HasColumnName("tracking");
            entity.Property(e => e.Language)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("language");
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

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProjectId }).HasName("memberships_pkey");

            entity.ToTable("memberships");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(Role.Member)
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

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("memberships_userId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Members)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("memberships_projectId_fkey");
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("domains_pkey");

            entity.ToTable("domains");

            entity.HasIndex(e => e.ProjectId, "domains_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Verified }, "domains_projectId_verified_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Domain1 }, "domains_projectId_domain_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Domain1).HasColumnName("domain");
            entity.Property(e => e.Verified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("verified");
            entity.Property(e => e.DkimTokens)
                .HasColumnType("jsonb")
                .HasColumnName("dkimTokens");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Domains)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("domains_projectId_fkey");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contacts_pkey");

            entity.ToTable("contacts");

            entity.HasIndex(e => e.ProjectId, "contacts_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Subscribed }, "contacts_projectId_subscribed_idx");

            entity.HasIndex(e => e.SnoozedUntil, "contacts_snoozedUntil_idx");

            entity.HasIndex(e => e.Data, "contacts_data_idx")
                .HasMethod("gin")
                .HasOperators("jsonb_ops");

            entity.HasIndex(e => new { e.ProjectId, e.Email }, "contacts_projectId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Subscribed)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("subscribed");
            entity.Property(e => e.SnoozedUntil)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("snoozedUntil");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("contacts_projectId_fkey");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("templates_pkey");

            entity.ToTable("templates");

            entity.HasIndex(e => e.ProjectId, "templates_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Type }, "templates_projectId_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.FromName).HasColumnName("fromName");
            entity.Property(e => e.ReplyTo).HasColumnName("replyTo");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(TemplateType.Marketing)
                .HasColumnName("type");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Templates)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("templates_projectId_fkey");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("segments_pkey");

            entity.ToTable("segments");

            entity.HasIndex(e => e.ProjectId, "segments_projectId_idx");

            entity.HasIndex(e => e.Condition, "segments_condition_idx")
                .HasMethod("gin")
                .HasOperators("jsonb_ops");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(SegmentType.Dynamic)
                .HasColumnName("type");
            entity.Property(e => e.Condition)
                .HasColumnType("jsonb")
                .HasColumnName("condition");
            entity.Property(e => e.TrackMembership)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("trackMembership");
            entity.Property(e => e.MemberCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("memberCount");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Segments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("segments_projectId_fkey");
        });

        modelBuilder.Entity<SegmentMembership>(entity =>
        {
            entity.HasKey(e => new { e.ContactId, e.SegmentId }).HasName("segment_memberships_pkey");

            entity.ToTable("segment_memberships");

            entity.HasIndex(e => new { e.SegmentId, e.ExitedAt }, "segment_memberships_segmentId_exitedAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.ExitedAt }, "segment_memberships_contactId_exitedAt_idx");

            entity.HasIndex(e => e.EnteredAt, "segment_memberships_enteredAt_idx");

            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.SegmentId).HasColumnName("segmentId");
            entity.Property(e => e.EnteredAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enteredAt");
            entity.Property(e => e.ExitedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("exitedAt");
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

            entity.HasOne(d => d.Contact).WithMany(p => p.SegmentMemberships)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("segment_memberships_contactId_fkey");

            entity.HasOne(d => d.Segment).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("segment_memberships_segmentId_fkey");
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("campaigns_pkey");

            entity.ToTable("campaigns");

            entity.HasIndex(e => new { e.ProjectId, e.Status }, "campaigns_projectId_status_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Archived, e.CreatedAt }, "campaigns_projectId_archived_createdAt_idx");

            entity.HasIndex(e => e.ScheduledFor, "campaigns_scheduledFor_idx");

            entity.HasIndex(e => e.SegmentId, "campaigns_segmentId_idx");

            entity.HasIndex(e => e.AudienceCondition, "campaigns_audienceCondition_idx")
                .HasMethod("gin")
                .HasOperators("jsonb_ops");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(CampaignStatus.Draft)
                .HasColumnName("status");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.FromName).HasColumnName("fromName");
            entity.Property(e => e.ReplyTo).HasColumnName("replyTo");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(TemplateType.Marketing)
                .HasColumnName("type");
            entity.Property(e => e.AudienceType)
                .ValueGeneratedNever()
                .HasDefaultValue(CampaignAudienceType.All)
                .HasColumnName("audienceType");
            entity.Property(e => e.AudienceCondition)
                .HasColumnType("jsonb")
                .HasColumnName("audienceCondition");
            entity.Property(e => e.SegmentId).HasColumnName("segmentId");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.TotalRecipients)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalRecipients");
            entity.Property(e => e.SentCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sentCount");
            entity.Property(e => e.DeliveredCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("deliveredCount");
            entity.Property(e => e.OpenedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("openedCount");
            entity.Property(e => e.ClickedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clickedCount");
            entity.Property(e => e.BouncedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("bouncedCount");
            entity.Property(e => e.ComplainedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("complainedCount");
            entity.Property(e => e.UnsubscribedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("unsubscribedCount");
            entity.Property(e => e.Archived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("archived");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
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

            entity.HasOne(d => d.Segment).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("campaigns_segmentId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("campaigns_projectId_fkey");
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflows_pkey");

            entity.ToTable("workflows");

            entity.HasIndex(e => e.ProjectId, "workflows_projectId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Enabled }, "workflows_projectId_enabled_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.TriggerType).HasColumnName("triggerType");
            entity.Property(e => e.TriggerConfig)
                .HasColumnType("jsonb")
                .HasColumnName("triggerConfig");
            entity.Property(e => e.AllowReentry)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowReentry");
            entity.Property(e => e.ThrottleWindowSeconds).HasColumnName("throttleWindowSeconds");
            entity.Property(e => e.ThrottleKey).HasColumnName("throttleKey");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflows_projectId_fkey");
        });

        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_steps_pkey");

            entity.ToTable("workflow_steps");

            entity.HasIndex(e => e.WorkflowId, "workflow_steps_workflowId_idx");

            entity.HasIndex(e => e.TemplateId, "workflow_steps_templateId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Position)
                .HasColumnType("jsonb")
                .HasColumnName("position");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");
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

            entity.HasOne(d => d.Workflow).WithMany(p => p.Steps)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_steps_workflowId_fkey");

            entity.HasOne(d => d.Template).WithMany(p => p.WorkflowSteps)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("workflow_steps_templateId_fkey");
        });

        modelBuilder.Entity<WorkflowTransition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_transitions_pkey");

            entity.ToTable("workflow_transitions");

            entity.HasIndex(e => e.FromStepId, "workflow_transitions_fromStepId_idx");

            entity.HasIndex(e => e.ToStepId, "workflow_transitions_toStepId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FromStepId).HasColumnName("fromStepId");
            entity.Property(e => e.ToStepId).HasColumnName("toStepId");
            entity.Property(e => e.Condition)
                .HasColumnType("jsonb")
                .HasColumnName("condition");
            entity.Property(e => e.Priority)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("priority");
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

            entity.HasOne(d => d.FromStep).WithMany(p => p.OutgoingTransitions)
                .HasForeignKey(d => d.FromStepId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_transitions_fromStepId_fkey");

            entity.HasOne(d => d.ToStep).WithMany(p => p.IncomingTransitions)
                .HasForeignKey(d => d.ToStepId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_transitions_toStepId_fkey");
        });

        modelBuilder.Entity<WorkflowExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_executions_pkey");

            entity.ToTable("workflow_executions");

            entity.HasIndex(e => new { e.WorkflowId, e.ContactId }, "workflow_executions_workflowId_contactId_idx");

            entity.HasIndex(e => new { e.WorkflowId, e.Status }, "workflow_executions_workflowId_status_idx");

            entity.HasIndex(e => new { e.ContactId, e.Status }, "workflow_executions_contactId_status_idx");

            entity.HasIndex(e => new { e.Status, e.CurrentStepId }, "workflow_executions_status_currentStepId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkflowExecutionStatus.Running)
                .HasColumnName("status");
            entity.Property(e => e.CurrentStepId).HasColumnName("currentStepId");
            entity.Property(e => e.ExitReason).HasColumnName("exitReason");
            entity.Property(e => e.Context)
                .HasColumnType("jsonb")
                .HasColumnName("context");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
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

            entity.HasOne(d => d.Workflow).WithMany(p => p.Executions)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_executions_workflowId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.WorkflowExecutions)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_executions_contactId_fkey");

            entity.HasOne(d => d.CurrentStep).WithMany(p => p.CurrentWorkflowExecutions)
                .HasForeignKey(d => d.CurrentStepId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("workflow_executions_currentStepId_fkey");
        });

        modelBuilder.Entity<WorkflowStepExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workflow_step_executions_pkey");

            entity.ToTable("workflow_step_executions");

            entity.HasIndex(e => new { e.ExecutionId, e.Status }, "workflow_step_executions_executionId_status_idx");

            entity.HasIndex(e => e.StepId, "workflow_step_executions_stepId_idx");

            entity.HasIndex(e => new { e.Status, e.ScheduledFor }, "workflow_step_executions_status_scheduledFor_idx");

            entity.HasIndex(e => e.ScheduledFor, "workflow_step_executions_scheduledFor_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ExecutionId).HasColumnName("executionId");
            entity.Property(e => e.StepId).HasColumnName("stepId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(StepExecutionStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.ExecuteAfter)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("executeAfter");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
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

            entity.HasOne(d => d.Execution).WithMany(p => p.StepExecutions)
                .HasForeignKey(d => d.ExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_step_executions_executionId_fkey");

            entity.HasOne(d => d.Step).WithMany(p => p.Executions)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workflow_step_executions_stepId_fkey");
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("emails_pkey");

            entity.ToTable("emails");

            entity.HasIndex(e => e.MessageId, "emails_messageId_key").IsUnique();

            entity.HasIndex(e => new { e.ProjectId, e.ContactId }, "emails_projectId_contactId_idx");

            entity.HasIndex(e => e.ContactId, "emails_contactId_idx");

            entity.HasIndex(e => e.CampaignId, "emails_campaignId_idx");

            entity.HasIndex(e => new { e.CampaignId, e.Status }, "emails_campaignId_status_idx");

            entity.HasIndex(e => e.WorkflowExecutionId, "emails_workflowExecutionId_idx");

            entity.HasIndex(e => e.WorkflowStepExecutionId, "emails_workflowStepExecutionId_idx");

            entity.HasIndex(e => e.Status, "emails_status_idx");

            entity.HasIndex(e => e.CreatedAt, "emails_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.SourceType, e.CreatedAt }, "emails_projectId_sourceType_createdAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.OpenedAt }, "emails_contactId_openedAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.ClickedAt }, "emails_contactId_clickedAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.BouncedAt }, "emails_contactId_bouncedAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.ComplainedAt }, "emails_contactId_complainedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.ToName).HasColumnName("toName");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.FromName).HasColumnName("fromName");
            entity.Property(e => e.ReplyTo).HasColumnName("replyTo");
            entity.Property(e => e.Headers)
                .HasColumnType("jsonb")
                .HasColumnName("headers");
            entity.Property(e => e.Attachments)
                .HasColumnType("jsonb")
                .HasColumnName("attachments");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");
            entity.Property(e => e.CampaignId).HasColumnName("campaignId");
            entity.Property(e => e.WorkflowExecutionId).HasColumnName("workflowExecutionId");
            entity.Property(e => e.WorkflowStepExecutionId).HasColumnName("workflowStepExecutionId");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(EmailStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.ScheduledFor)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("scheduledFor");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.DeliveredAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deliveredAt");
            entity.Property(e => e.OpenedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("openedAt");
            entity.Property(e => e.ClickedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("clickedAt");
            entity.Property(e => e.BouncedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("bouncedAt");
            entity.Property(e => e.ComplainedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("complainedAt");
            entity.Property(e => e.UnsubscribedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("unsubscribedAt");
            entity.Property(e => e.Opens)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("opens");
            entity.Property(e => e.Clicks)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("clicks");
            entity.Property(e => e.Simulated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("simulated");
            entity.Property(e => e.Error).HasColumnName("error");
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

            entity.HasOne(d => d.Contact).WithMany(p => p.Emails)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("emails_contactId_fkey");

            entity.HasOne(d => d.Template).WithMany(p => p.Emails)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("emails_templateId_fkey");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Emails)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("emails_campaignId_fkey");

            entity.HasOne(d => d.WorkflowExecution).WithMany(p => p.Emails)
                .HasForeignKey(d => d.WorkflowExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("emails_workflowExecutionId_fkey");

            entity.HasOne(d => d.WorkflowStepExecution).WithMany(p => p.Emails)
                .HasForeignKey(d => d.WorkflowStepExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("emails_workflowStepExecutionId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Emails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("emails_projectId_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "events_projectId_name_idx");

            entity.HasIndex(e => e.ContactId, "events_contactId_idx");

            entity.HasIndex(e => e.EmailId, "events_emailId_idx");

            entity.HasIndex(e => e.CreatedAt, "events_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.ContactId, e.Name, e.CreatedAt }, "events_projectId_contactId_name_createdAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.Name, e.CreatedAt }, "events_contactId_name_createdAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Name, e.CreatedAt }, "events_projectId_name_createdAt_idx");

            entity.HasIndex(e => new { e.ContactId, e.CreatedAt }, "events_contactId_createdAt_idx");

            entity.HasIndex(e => e.Data, "events_data_idx")
                .HasMethod("gin")
                .HasOperators("jsonb_ops");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.EmailId).HasColumnName("emailId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Events)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("events_projectId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.Events)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("events_contactId_fkey");

            entity.HasOne(d => d.Email).WithMany(p => p.Events)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("events_emailId_fkey");
        });

        modelBuilder.Entity<IdempotencyKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("idempotency_keys_pkey");

            entity.ToTable("idempotency_keys");

            entity.HasIndex(e => e.ExpiresAt, "idempotency_keys_expiresAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Key }, "idempotency_keys_projectId_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.StatusCode).HasColumnName("statusCode");
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

            entity.HasOne(d => d.Project).WithMany(p => p.IdempotencyKeys)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idempotency_keys_projectId_fkey");
        });

        modelBuilder.Entity<ThrottleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("throttle_claims_pkey");

            entity.ToTable("throttle_claims");

            entity.HasIndex(e => e.EmailId, "throttle_claims_emailId_idx");

            entity.HasIndex(e => e.WorkflowId, "throttle_claims_workflowId_idx");

            entity.HasIndex(e => e.ContactId, "throttle_claims_contactId_idx");

            entity.HasIndex(e => e.ExpiresAt, "throttle_claims_expiresAt_idx");

            entity.HasIndex(e => new { e.ProjectId, e.Key, e.RecipientKey }, "throttle_claims_projectId_key_recipientKey_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.RecipientKey).HasColumnName("recipientKey");
            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.EmailId).HasColumnName("emailId");
            entity.Property(e => e.WorkflowId).HasColumnName("workflowId");
            entity.Property(e => e.WindowSeconds).HasColumnName("windowSeconds");
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

            entity.HasOne(d => d.Project).WithMany(p => p.ThrottleClaims)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("throttle_claims_projectId_fkey");

            entity.HasOne(d => d.Contact).WithMany(p => p.ThrottleClaims)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("throttle_claims_contactId_fkey");

            entity.HasOne(d => d.Workflow).WithMany(p => p.ThrottleClaims)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("throttle_claims_workflowId_fkey");
        });

        modelBuilder.Entity<ApiRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("api_requests_pkey");

            entity.ToTable("api_requests");

            entity.HasIndex(e => new { e.ProjectId, e.CreatedAt }, "api_requests_projectId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "api_requests_userId_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.StatusCode, e.CreatedAt }, "api_requests_statusCode_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.Path, e.CreatedAt }, "api_requests_path_createdAt_idx").IsDescending(false, true);

            entity.HasIndex(e => e.CreatedAt, "api_requests_createdAt_idx").IsDescending(true);

            entity.HasIndex(e => new { e.ProjectId, e.StatusCode, e.CreatedAt }, "api_requests_projectId_statusCode_createdAt_idx").IsDescending(false, false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.StatusCode).HasColumnName("statusCode");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AuthType).HasColumnName("authType");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.ErrorCode).HasColumnName("errorCode");
            entity.Property(e => e.ErrorMessage).HasColumnName("errorMessage");
            entity.Property(e => e.RequestSize).HasColumnName("requestSize");
            entity.Property(e => e.ResponseSize).HasColumnName("responseSize");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Project).WithMany(p => p.ApiRequests)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("api_requests_projectId_fkey");
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

        foreach (var entry in ChangeTracker.Entries<Project>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Membership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Domain>())
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

        foreach (var entry in ChangeTracker.Entries<Template>())
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

        foreach (var entry in ChangeTracker.Entries<SegmentMembership>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Campaign>())
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

        foreach (var entry in ChangeTracker.Entries<WorkflowStep>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowTransition>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowExecution>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkflowStepExecution>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Email>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ThrottleClaim>())
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

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
