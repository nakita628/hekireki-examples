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

namespace OpenPanel.Models;

public partial class OpenPanelContext : DbContext
{
    public OpenPanelContext(DbContextOptions<OpenPanelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CodeMigration> CodeMigrations { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTotp> UserTotps { get; set; }

    public virtual DbSet<TwoFactor> TwoFactors { get; set; }

    public virtual DbSet<TwoFactorChallenge> TwoFactorChallenges { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberProject> MemberProjects { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Cohort> Cohorts { get; set; }

    public virtual DbSet<Salt> Salts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportLayout> ReportLayouts { get; set; }

    public virtual DbSet<ShareOverview> ShareOverviews { get; set; }

    public virtual DbSet<ShareDashboard> ShareDashboards { get; set; }

    public virtual DbSet<ShareReport> ShareReports { get; set; }

    public virtual DbSet<ShareWidget> ShareWidgets { get; set; }

    public virtual DbSet<EventMeta> EventMeta { get; set; }

    public virtual DbSet<CustomEvent> CustomEvents { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<NotificationRule> NotificationRules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<ExportWatermark> ExportWatermarks { get; set; }

    public virtual DbSet<ResetPassword> ResetPasswords { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<ProjectInsight> ProjectInsights { get; set; }

    public virtual DbSet<InsightEvent> InsightEvents { get; set; }

    public virtual DbSet<GscConnection> GscConnections { get; set; }

    public virtual DbSet<EmailUnsubscribe> EmailUnsubscribes { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ProjectType>("ProjectType");
        npgsql.MapEnum<ProjectScope>("ProjectScope");
        npgsql.MapEnum<AccessLevel>("AccessLevel");
        npgsql.MapEnum<ClientType>("ClientType");
        npgsql.MapEnum<Interval>("Interval");
        npgsql.MapEnum<ChartType>("ChartType");
        npgsql.MapEnum<Metric>("Metric");
        npgsql.MapEnum<IntegrationType>("IntegrationType");
        npgsql.MapEnum<ImportStatus>("ImportStatus");
        npgsql.MapEnum<InsightState>("InsightState");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ProjectType>("ProjectType");
        dataSource.MapEnum<ProjectScope>("ProjectScope");
        dataSource.MapEnum<AccessLevel>("AccessLevel");
        dataSource.MapEnum<ClientType>("ClientType");
        dataSource.MapEnum<Interval>("Interval");
        dataSource.MapEnum<ChartType>("ChartType");
        dataSource.MapEnum<Metric>("Metric");
        dataSource.MapEnum<IntegrationType>("IntegrationType");
        dataSource.MapEnum<ImportStatus>("ImportStatus");
        dataSource.MapEnum<InsightState>("InsightState");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProjectType>(name: "ProjectType");
        modelBuilder.HasPostgresEnum<ProjectScope>(name: "ProjectScope");
        modelBuilder.HasPostgresEnum<AccessLevel>(name: "AccessLevel");
        modelBuilder.HasPostgresEnum<ClientType>(name: "ClientType");
        modelBuilder.HasPostgresEnum<Interval>(name: "Interval");
        modelBuilder.HasPostgresEnum<ChartType>(name: "ChartType");
        modelBuilder.HasPostgresEnum<Metric>(name: "Metric");
        modelBuilder.HasPostgresEnum<IntegrationType>(name: "IntegrationType");
        modelBuilder.HasPostgresEnum<ImportStatus>(name: "ImportStatus");
        modelBuilder.HasPostgresEnum<InsightState>(name: "InsightState");

        modelBuilder.Entity<CodeMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("__code_migrations_pkey");

            entity.ToTable("__code_migrations");

            entity.HasIndex(e => e.Name, "__code_migrations_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
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
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conversations_pkey");

            entity.ToTable("conversations");

            entity.HasIndex(e => new { e.ProjectId, e.UpdatedAt }, "conversations_projectId_updatedAt_idx");

            entity.HasIndex(e => new { e.UserId, e.UpdatedAt }, "conversations_userId_updatedAt_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Title).HasColumnName("title");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conversations_projectId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conversations_organizationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conversations_userId_fkey");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_messages_pkey");

            entity.ToTable("chat_messages");

            entity.HasIndex(e => new { e.ConversationId, e.CreatedAt }, "chat_messages_conversationId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Parts)
                .HasColumnType("json")
                .HasColumnName("parts");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chat_messages_conversationId_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organizations_pkey");

            entity.ToTable("organizations");

            entity.HasIndex(e => e.Slug, "organizations_slug_key").IsUnique();

            entity.HasIndex(e => e.WindDownStartedAt, "organizations_windDownStartedAt_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.CreatedByUserId).HasColumnName("createdByUserId");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.Onboarding)
                .ValueGeneratedNever()
                .HasDefaultValue("completed")
                .HasColumnName("onboarding");
            entity.Property(e => e.Attribution)
                .HasColumnType("jsonb")
                .HasColumnName("attribution");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
            entity.Property(e => e.SubscriptionCustomerId).HasColumnName("subscriptionCustomerId");
            entity.Property(e => e.SubscriptionPriceId).HasColumnName("subscriptionPriceId");
            entity.Property(e => e.SubscriptionProductId).HasColumnName("subscriptionProductId");
            entity.Property(e => e.SubscriptionStatus).HasColumnName("subscriptionStatus");
            entity.Property(e => e.SubscriptionStartsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionStartsAt");
            entity.Property(e => e.SubscriptionEndsAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionEndsAt");
            entity.Property(e => e.SubscriptionCanceledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionCanceledAt");
            entity.Property(e => e.SubscriptionCreatedByUserId).HasColumnName("subscriptionCreatedByUserId");
            entity.Property(e => e.SubscriptionPeriodEventsCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("subscriptionPeriodEventsCount");
            entity.Property(e => e.SubscriptionPeriodEventsCountExceededAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionPeriodEventsCountExceededAt");
            entity.Property(e => e.SubscriptionPeriodEventsLimit)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("subscriptionPeriodEventsLimit");
            entity.Property(e => e.SubscriptionInterval).HasColumnName("subscriptionInterval");
            entity.Property(e => e.SubscriptionCancelReason).HasColumnName("subscriptionCancelReason");
            entity.Property(e => e.SubscriptionCancelComment).HasColumnName("subscriptionCancelComment");
            entity.Property(e => e.SubscriptionSaveDiscountAppliedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionSaveDiscountAppliedAt");
            entity.Property(e => e.SubscriptionPauseAtPeriodEnd)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("subscriptionPauseAtPeriodEnd");
            entity.Property(e => e.SubscriptionResumesAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionResumesAt");
            entity.Property(e => e.SubscriptionFirstStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("subscriptionFirstStartedAt");
            entity.Property(e => e.UsageWarningSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("usageWarningSentAt");
            entity.Property(e => e.UsageExceededSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("usageExceededSentAt");
            entity.Property(e => e.SubscriptionDiscount)
                .HasColumnType("jsonb")
                .HasColumnName("subscriptionDiscount");
            entity.Property(e => e.WindDownStartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("windDownStartedAt");
            entity.Property(e => e.WindDownStep).HasColumnName("windDownStep");
            entity.Property(e => e.DeleteAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleteAt");
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

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedOrganizations)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("organizations_createdByUserId_fkey");

            entity.HasOne(d => d.SubscriptionCreatedBy).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SubscriptionCreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("organizations_subscriptionCreatedByUserId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.EmailVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
        });

        modelBuilder.Entity<UserTotp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_totp_pkey");

            entity.ToTable("user_totp");

            entity.HasIndex(e => e.UserId, "user_totp_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.RecoveryCodes).HasColumnName("recoveryCodes");
            entity.Property(e => e.EnabledAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enabledAt");
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

            entity.HasOne(d => d.User).WithOne(p => p.Totp)
                .HasForeignKey<UserTotp>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_totp_userId_fkey");
        });

        modelBuilder.Entity<TwoFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_pkey");

            entity.ToTable("two_factor");

            entity.HasIndex(e => e.UserId, "two_factor_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
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

            entity.HasOne(d => d.User).WithOne(p => p.TwoFactor)
                .HasForeignKey<TwoFactor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_userId_fkey");
        });

        modelBuilder.Entity<TwoFactorChallenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_challenges_pkey");

            entity.ToTable("two_factor_challenges");

            entity.HasIndex(e => e.UserId, "two_factor_challenges_userId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.User).WithMany(p => p.TwoFactorChallenges)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_challenges_userId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.IdToken).HasColumnName("idToken");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accessTokenExpiresAt");
            entity.Property(e => e.RefreshTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("refreshTokenExpiresAt");
            entity.Property(e => e.Scope).HasColumnName("scope");
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
                .HasConstraintName("accounts_userId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.Token, "sessions_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
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
                .HasConstraintName("sessions_userId_fkey");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verifications_pkey");

            entity.ToTable("verifications");

            entity.HasIndex(e => e.Identifier, "verifications_identifier_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Value).HasColumnName("value");
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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("members_pkey");

            entity.ToTable("members");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "members_organizationId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.InvitedById).HasColumnName("invitedById");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectScope)
                .ValueGeneratedNever()
                .HasDefaultValue(ProjectScope.All)
                .HasColumnName("projectScope");
            entity.Property(e => e.Meta)
                .HasColumnType("jsonb")
                .HasColumnName("meta");
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

            entity.HasOne(d => d.User).WithMany(p => p.Membership)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("members_userId_fkey");

            entity.HasOne(d => d.InvitedBy).WithMany(p => p.SentInvites)
                .HasForeignKey(d => d.InvitedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("members_invitedById_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("members_organizationId_fkey");
        });

        modelBuilder.Entity<MemberProject>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.ProjectId }).HasName("member_projects_pkey");

            entity.ToTable("member_projects");

            entity.HasIndex(e => e.ProjectId, "member_projects_projectId_idx");

            entity.Property(e => e.MemberId).HasColumnName("memberId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Member).WithMany(p => p.Projects)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("member_projects_memberId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.MemberProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("member_projects_projectId_fkey");
        });

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invites_pkey");

            entity.ToTable("invites");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectAccess)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("projectAccess");
            entity.Property(e => e.ProjectScope)
                .ValueGeneratedNever()
                .HasDefaultValue(ProjectScope.All)
                .HasColumnName("projectScope");
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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Invites)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invites_createdById_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Invites)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invites_organizationId_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.EventsCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("eventsCount");
            entity.Property(e => e.Types)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<ProjectType>())
                .HasColumnName("types");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.Cors)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("cors");
            entity.Property(e => e.CrossDomain)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("crossDomain");
            entity.Property(e => e.AllowUnsafeRevenueTracking)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowUnsafeRevenueTracking");
            entity.Property(e => e.AiStoreContent)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("aiStoreContent");
            entity.Property(e => e.Filters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.FirstEventAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstEventAt");
            entity.Property(e => e.NoDataNotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("noDataNotifiedAt");
            entity.Property(e => e.DataStoppedNotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dataStoppedNotifiedAt");
            entity.Property(e => e.DeleteAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleteAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Projects)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projects_organizationId_fkey");
        });

        modelBuilder.Entity<Cohort>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cohorts_pkey");

            entity.ToTable("cohorts");

            entity.HasIndex(e => e.ProjectId, "cohorts_projectId_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Definition)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("definition");
            entity.Property(e => e.IsStatic)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isStatic");
            entity.Property(e => e.ProfileCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("profileCount");
            entity.Property(e => e.LastComputedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastComputedAt");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Cohorts)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cohorts_projectId_fkey");
        });

        modelBuilder.Entity<Salt>(entity =>
        {
            entity.HasKey(e => e.Salt1).HasName("salts_pkey");

            entity.ToTable("salts");

            entity.Property(e => e.Salt1).HasColumnName("salt");
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

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(ClientType.Write)
                .HasColumnName("type");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.IgnoreCorsAndSecret)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("ignoreCorsAndSecret");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clients_projectId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Client)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clients_organizationId_fkey");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dashboards_pkey");

            entity.ToTable("dashboards");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Filters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("filters");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Dashboard)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dashboards_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Dashboards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dashboards_projectId_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Interval).HasColumnName("interval");
            entity.Property(e => e.Range)
                .ValueGeneratedNever()
                .HasDefaultValue("30d")
                .HasColumnName("range");
            entity.Property(e => e.ChartType).HasColumnName("chartType");
            entity.Property(e => e.LineType)
                .ValueGeneratedNever()
                .HasDefaultValue("monotone")
                .HasColumnName("lineType");
            entity.Property(e => e.Breakdowns)
                .HasColumnType("jsonb")
                .HasColumnName("breakdowns");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.GlobalFilters)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("globalFilters");
            entity.Property(e => e.Formula).HasColumnName("formula");
            entity.Property(e => e.Unit).HasColumnName("unit");
            entity.Property(e => e.Metric)
                .ValueGeneratedNever()
                .HasDefaultValue(Metric.Sum)
                .HasColumnName("metric");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Previous)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("previous");
            entity.Property(e => e.Criteria).HasColumnName("criteria");
            entity.Property(e => e.FunnelGroup).HasColumnName("funnelGroup");
            entity.Property(e => e.FunnelWindow).HasColumnName("funnelWindow");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
            entity.Property(e => e.Display)
                .HasColumnType("jsonb")
                .HasColumnName("display");
            entity.Property(e => e.VisibleSeries).HasColumnName("visibleSeries");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reports_projectId_fkey");

            entity.HasOne(d => d.Dashboard).WithMany(p => p.Reports)
                .HasForeignKey(d => d.DashboardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reports_dashboardId_fkey");
        });

        modelBuilder.Entity<ReportLayout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("report_layouts_pkey");

            entity.ToTable("report_layouts");

            entity.HasIndex(e => e.ReportId, "report_layouts_reportId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ReportId).HasColumnName("reportId");
            entity.Property(e => e.GridColumns)
                .ValueGeneratedNever()
                .HasDefaultValue(12)
                .HasColumnName("gridColumns");
            entity.Property(e => e.X)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("x");
            entity.Property(e => e.Y)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("y");
            entity.Property(e => e.W)
                .ValueGeneratedNever()
                .HasDefaultValue(8)
                .HasColumnName("w");
            entity.Property(e => e.H)
                .ValueGeneratedNever()
                .HasDefaultValue(6)
                .HasColumnName("h");
            entity.Property(e => e.MinW)
                .ValueGeneratedNever()
                .HasDefaultValue(4)
                .HasColumnName("minW");
            entity.Property(e => e.MinH)
                .ValueGeneratedNever()
                .HasDefaultValue(4)
                .HasColumnName("minH");
            entity.Property(e => e.MaxW).HasColumnName("maxW");
            entity.Property(e => e.MaxH).HasColumnName("maxH");
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

            entity.HasOne(d => d.Report).WithOne(p => p.Layout)
                .HasForeignKey<ReportLayout>(d => d.ReportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("report_layouts_reportId_fkey");
        });

        modelBuilder.Entity<ShareOverview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shares_id_key");

            entity.ToTable("shares");

            entity.HasIndex(e => e.ProjectId, "shares_projectId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Public)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("public");
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

            entity.HasOne(d => d.Project).WithOne(p => p.Share)
                .HasForeignKey<ShareOverview>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("shares_projectId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ShareOverview)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("shares_organizationId_fkey");
        });

        modelBuilder.Entity<ShareDashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("share_dashboards_id_key");

            entity.ToTable("share_dashboards");

            entity.HasIndex(e => e.DashboardId, "share_dashboards_dashboardId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DashboardId).HasColumnName("dashboardId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Public)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("public");
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

            entity.HasOne(d => d.Dashboard).WithOne(p => p.Share)
                .HasForeignKey<ShareDashboard>(d => d.DashboardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_dashboards_dashboardId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ShareDashboard)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_dashboards_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ShareDashboards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_dashboards_projectId_fkey");
        });

        modelBuilder.Entity<ShareReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("share_reports_id_key");

            entity.ToTable("share_reports");

            entity.HasIndex(e => e.ReportId, "share_reports_reportId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ReportId).HasColumnName("reportId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Public)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("public");
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

            entity.HasOne(d => d.Report).WithOne(p => p.Share)
                .HasForeignKey<ShareReport>(d => d.ReportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_reports_reportId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ShareReport)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_reports_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ShareReports)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_reports_projectId_fkey");
        });

        modelBuilder.Entity<ShareWidget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("share_widgets_id_key");

            entity.ToTable("share_widgets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Public)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("public");
            entity.Property(e => e.Options)
                .HasColumnType("jsonb")
                .HasColumnName("options");
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

            entity.HasOne(d => d.Project).WithMany(p => p.ShareWidgets)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_widgets_projectId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.ShareWidget)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("share_widgets_organizationId_fkey");
        });

        modelBuilder.Entity<EventMeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_meta_pkey");

            entity.ToTable("event_meta");

            entity.HasIndex(e => new { e.Name, e.ProjectId }, "event_meta_name_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Conversion).HasColumnName("conversion");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Icon).HasColumnName("icon");
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

            entity.HasOne(d => d.Project).WithMany(p => p.Meta)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("event_meta_projectId_fkey");
        });

        modelBuilder.Entity<CustomEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("custom_events_pkey");

            entity.ToTable("custom_events");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "custom_events_projectId_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Items)
                .HasColumnType("jsonb")
                .HasColumnName("items");
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

            entity.HasOne(d => d.Project).WithMany(p => p.CustomEvents)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("custom_events_projectId_fkey");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("references_pkey");

            entity.ToTable("references");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
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

            entity.HasOne(d => d.Project).WithMany(p => p.References)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("references_projectId_fkey");
        });

        modelBuilder.Entity<NotificationRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_rules_pkey");

            entity.ToTable("notification_rules");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.SendToApp)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sendToApp");
            entity.Property(e => e.SendToEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sendToEmail");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.Template).HasColumnName("template");
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

            entity.HasOne(d => d.Project).WithMany(p => p.NotificationRules)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notification_rules_projectId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.IsReadAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("isReadAt");
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
            entity.Property(e => e.SendToApp)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sendToApp");
            entity.Property(e => e.SendToEmail)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("sendToEmail");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.NotificationRuleId).HasColumnName("notificationRuleId");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");

            entity.HasOne(d => d.Project).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_projectId_fkey");

            entity.HasOne(d => d.Integration).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_integrationId_fkey");

            entity.HasOne(d => d.NotificationRule).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationRuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_notificationRuleId_fkey");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("integrations_pkey");

            entity.ToTable("integrations");

            entity.HasIndex(e => e.OrganizationId, "integrations_organizationId_idx");

            entity.HasIndex(e => e.ProjectId, "integrations_projectId_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("integrations_organizationId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("integrations_projectId_fkey");

            entity.HasMany(d => d.NotificationRules).WithMany(p => p.Integrations)
                .UsingEntity<Dictionary<string, object>>(
                    "_IntegrationToNotificationRule",
                    r => r.HasOne<NotificationRule>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_IntegrationToNotificationRule_B_fkey"),
                    l => l.HasOne<Integration>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_IntegrationToNotificationRule_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_IntegrationToNotificationRule_AB_pkey");
                        j.ToTable("_IntegrationToNotificationRule");
                        j.HasIndex(new[] { "B" }, "_IntegrationToNotificationRule_B_index");
                    });
        });

        modelBuilder.Entity<ExportWatermark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("export_watermarks_pkey");

            entity.ToTable("export_watermarks");

            entity.HasIndex(e => e.IntegrationId, "export_watermarks_integrationId_idx");

            entity.HasIndex(e => new { e.ProjectId, e.IntegrationId }, "export_watermarks_projectId_integrationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.LastInsertedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastInsertedAt");
            entity.Property(e => e.LastEventId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("lastEventId");
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

            entity.HasOne(d => d.Project).WithMany(p => p.ExportWatermarks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("export_watermarks_projectId_fkey");

            entity.HasOne(d => d.Integration).WithMany(p => p.ExportWatermarks)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("export_watermarks_integrationId_fkey");
        });

        modelBuilder.Entity<ResetPassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reset_password_pkey");

            entity.ToTable("reset_password");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Account).WithMany(p => p.ResetPasswords)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reset_password_accountId_fkey");
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("imports_pkey");

            entity.ToTable("imports");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StatusMessage).HasColumnName("statusMessage");
            entity.Property(e => e.ErrorMessage).HasColumnName("errorMessage");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.TotalEvents)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("totalEvents");
            entity.Property(e => e.ProcessedEvents)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processedEvents");
            entity.Property(e => e.CurrentStep).HasColumnName("currentStep");
            entity.Property(e => e.CurrentBatch).HasColumnName("currentBatch");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Project).WithMany(p => p.Imports)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("imports_projectId_fkey");
        });

        modelBuilder.Entity<ProjectInsight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("project_insights_pkey");

            entity.ToTable("project_insights");

            entity.HasIndex(e => new { e.ProjectId, e.ImpactScore }, "project_insights_projectId_impactScore_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.ProjectId, e.ModuleKey, e.WindowKind, e.State }, "project_insights_projectId_moduleKey_windowKind_state_idx");

            entity.HasIndex(e => new { e.ProjectId, e.State, e.RelevanceScore }, "project_insights_projectId_state_relevanceScore_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.ProjectId, e.ModuleKey, e.DimensionKey, e.WindowKind, e.State }, "project_insights_projectId_moduleKey_dimensionKey_windowKin_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ModuleKey).HasColumnName("moduleKey");
            entity.Property(e => e.DimensionKey).HasColumnName("dimensionKey");
            entity.Property(e => e.WindowKind).HasColumnName("windowKind");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(InsightState.Active)
                .HasColumnName("state");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.DisplayName)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("displayName");
            entity.Property(e => e.Payload)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Direction).HasColumnName("direction");
            entity.Property(e => e.ImpactScore)
                .ValueGeneratedNever()
                .HasDefaultValue(0.0)
                .HasColumnName("impactScore");
            entity.Property(e => e.SeverityBand).HasColumnName("severityBand");
            entity.Property(e => e.RelevanceScore).HasColumnName("relevanceScore");
            entity.Property(e => e.AiSummary).HasColumnName("aiSummary");
            entity.Property(e => e.AiCategory).HasColumnName("aiCategory");
            entity.Property(e => e.EmailWorthy)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("emailWorthy");
            entity.Property(e => e.ReferenceWorthy)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("referenceWorthy");
            entity.Property(e => e.EnrichedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enrichedAt");
            entity.Property(e => e.EnrichVersion).HasColumnName("enrichVersion");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("version");
            entity.Property(e => e.ThreadId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("threadId");
            entity.Property(e => e.WindowStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("windowStart");
            entity.Property(e => e.WindowEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("windowEnd");
            entity.Property(e => e.FirstDetectedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("firstDetectedAt");
            entity.Property(e => e.LastUpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUpdatedAt");
            entity.Property(e => e.LastSeenAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSeenAt");
        });

        modelBuilder.Entity<InsightEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("insight_events_pkey");

            entity.ToTable("insight_events");

            entity.HasIndex(e => new { e.InsightId, e.CreatedAt }, "insight_events_insightId_createdAt_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.InsightId).HasColumnName("insightId");
            entity.Property(e => e.EventKind).HasColumnName("eventKind");
            entity.Property(e => e.ChangeFrom)
                .HasColumnType("jsonb")
                .HasColumnName("changeFrom");
            entity.Property(e => e.ChangeTo)
                .HasColumnType("jsonb")
                .HasColumnName("changeTo");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Insight).WithMany(p => p.Events)
                .HasForeignKey(d => d.InsightId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("insight_events_insightId_fkey");
        });

        modelBuilder.Entity<GscConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gsc_connections_pkey");

            entity.ToTable("gsc_connections");

            entity.HasIndex(e => e.ProjectId, "gsc_connections_projectId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.SiteUrl)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("siteUrl");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.AccessTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("accessTokenExpiresAt");
            entity.Property(e => e.LastSyncedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSyncedAt");
            entity.Property(e => e.LastSyncStatus).HasColumnName("lastSyncStatus");
            entity.Property(e => e.LastSyncError).HasColumnName("lastSyncError");
            entity.Property(e => e.BackfillStatus).HasColumnName("backfillStatus");
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

            entity.HasOne(d => d.Project).WithOne(p => p.GscConnection)
                .HasForeignKey<GscConnection>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("gsc_connections_projectId_fkey");
        });

        modelBuilder.Entity<EmailUnsubscribe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("email_unsubscribes_pkey");

            entity.ToTable("email_unsubscribes");

            entity.HasIndex(e => new { e.Email, e.Category }, "email_unsubscribes_email_category_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
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
        foreach (var entry in ChangeTracker.Entries<CodeMigration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Conversation>())
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

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserTotp>())
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

        foreach (var entry in ChangeTracker.Entries<Verification>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Member>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Invite>())
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

        foreach (var entry in ChangeTracker.Entries<Cohort>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Salt>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Client>())
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

        foreach (var entry in ChangeTracker.Entries<Report>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ReportLayout>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ShareOverview>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ShareDashboard>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ShareReport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ShareWidget>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EventMeta>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CustomEvent>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Reference>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<NotificationRule>())
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

        foreach (var entry in ChangeTracker.Entries<Integration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ExportWatermark>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ResetPassword>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Import>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProjectInsight>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastUpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastUpdatedAt).IsModified))
            {
                entry.Entity.LastUpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GscConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
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
