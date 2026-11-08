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

namespace Documenso.Models;

public partial class DocumensoContext : DbContext
{
    public DocumensoContext(DbContextOptions<DocumensoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<TeamProfile> TeamProfiles { get; set; }

    public virtual DbSet<UserSecurityAuditLog> UserSecurityAuditLogs { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<Passkey> Passkeys { get; set; }

    public virtual DbSet<AnonymousVerificationToken> AnonymousVerificationTokens { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<WebhookCall> WebhookCalls { get; set; }

    public virtual DbSet<ApiToken> ApiTokens { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionClaim> SubscriptionClaims { get; set; }

    public virtual DbSet<OrganisationClaim> OrganisationClaims { get; set; }

    public virtual DbSet<OrganisationMonthlyStat> OrganisationMonthlyStats { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<AuthHandoff> AuthHandoffs { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Envelope> Envelopes { get; set; }

    public virtual DbSet<EnvelopeItem> EnvelopeItems { get; set; }

    public virtual DbSet<DocumentAuditLog> DocumentAuditLogs { get; set; }

    public virtual DbSet<DocumentData> DocumentData { get; set; }

    public virtual DbSet<DocumentMeta> DocumentMeta { get; set; }

    public virtual DbSet<EnvelopeAttachment> EnvelopeAttachments { get; set; }

    public virtual DbSet<Recipient> Recipients { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<EnvelopeContent> EnvelopeContents { get; set; }

    public virtual DbSet<DataContent> DataContents { get; set; }

    public virtual DbSet<Signature> Signatures { get; set; }

    public virtual DbSet<CscCredential> CscCredentials { get; set; }

    public virtual DbSet<CscSession> CscSessions { get; set; }

    public virtual DbSet<DocumentShareLink> DocumentShareLinks { get; set; }

    public virtual DbSet<Organisation> Organisations { get; set; }

    public virtual DbSet<OrganisationMember> OrganisationMembers { get; set; }

    public virtual DbSet<OrganisationMemberInvite> OrganisationMemberInvites { get; set; }

    public virtual DbSet<OrganisationGroup> OrganisationGroups { get; set; }

    public virtual DbSet<OrganisationGroupMember> OrganisationGroupMembers { get; set; }

    public virtual DbSet<TeamGroup> TeamGroups { get; set; }

    public virtual DbSet<OrganisationGlobalSettings> OrganisationGlobalSettings { get; set; }

    public virtual DbSet<TeamGlobalSettings> TeamGlobalSettings { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamEmail> TeamEmails { get; set; }

    public virtual DbSet<TeamEmailVerification> TeamEmailVerifications { get; set; }

    public virtual DbSet<TemplateDirectLink> TemplateDirectLinks { get; set; }

    public virtual DbSet<SiteSettings> SiteSettings { get; set; }

    public virtual DbSet<BackgroundJob> BackgroundJobs { get; set; }

    public virtual DbSet<BackgroundJobTask> BackgroundJobTasks { get; set; }

    public virtual DbSet<AvatarImage> AvatarImages { get; set; }

    public virtual DbSet<EmailDomain> EmailDomains { get; set; }

    public virtual DbSet<OrganisationEmail> OrganisationEmails { get; set; }

    public virtual DbSet<EmailTransport> EmailTransports { get; set; }

    public virtual DbSet<OrganisationAuthenticationPortal> OrganisationAuthenticationPortals { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<EmbedSignature> EmbedSignatures { get; set; }

    public virtual DbSet<RateLimit> RateLimits { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<IdentityProvider>("IdentityProvider");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<UserSecurityAuditLogType>("UserSecurityAuditLogType");
        npgsql.MapEnum<WebhookTriggerEvents>("WebhookTriggerEvents");
        npgsql.MapEnum<WebhookCallStatus>("WebhookCallStatus");
        npgsql.MapEnum<ApiTokenAlgorithm>("ApiTokenAlgorithm");
        npgsql.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        npgsql.MapEnum<DocumentStatus>("DocumentStatus");
        npgsql.MapEnum<DocumentSource>("DocumentSource");
        npgsql.MapEnum<DocumentVisibility>("DocumentVisibility");
        npgsql.MapEnum<FolderType>("FolderType");
        npgsql.MapEnum<EnvelopeType>("EnvelopeType");
        npgsql.MapEnum<DocumentDataType>("DocumentDataType");
        npgsql.MapEnum<DocumentSigningOrder>("DocumentSigningOrder");
        npgsql.MapEnum<DocumentDistributionMethod>("DocumentDistributionMethod");
        npgsql.MapEnum<ReadStatus>("ReadStatus");
        npgsql.MapEnum<SendStatus>("SendStatus");
        npgsql.MapEnum<SigningStatus>("SigningStatus");
        npgsql.MapEnum<RecipientRole>("RecipientRole");
        npgsql.MapEnum<FieldType>("FieldType");
        npgsql.MapEnum<OrganisationType>("OrganisationType");
        npgsql.MapEnum<OrganisationGroupType>("OrganisationGroupType");
        npgsql.MapEnum<OrganisationMemberRole>("OrganisationMemberRole");
        npgsql.MapEnum<TeamMemberRole>("TeamMemberRole");
        npgsql.MapEnum<OrganisationMemberInviteStatus>("OrganisationMemberInviteStatus");
        npgsql.MapEnum<TemplateType>("TemplateType");
        npgsql.MapEnum<BackgroundJobStatus>("BackgroundJobStatus");
        npgsql.MapEnum<BackgroundJobTaskStatus>("BackgroundJobTaskStatus");
        npgsql.MapEnum<EmailDomainStatus>("EmailDomainStatus");
        npgsql.MapEnum<EmailTransportType>("EmailTransportType");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<IdentityProvider>("IdentityProvider");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<UserSecurityAuditLogType>("UserSecurityAuditLogType");
        dataSource.MapEnum<WebhookTriggerEvents>("WebhookTriggerEvents");
        dataSource.MapEnum<WebhookCallStatus>("WebhookCallStatus");
        dataSource.MapEnum<ApiTokenAlgorithm>("ApiTokenAlgorithm");
        dataSource.MapEnum<SubscriptionStatus>("SubscriptionStatus");
        dataSource.MapEnum<DocumentStatus>("DocumentStatus");
        dataSource.MapEnum<DocumentSource>("DocumentSource");
        dataSource.MapEnum<DocumentVisibility>("DocumentVisibility");
        dataSource.MapEnum<FolderType>("FolderType");
        dataSource.MapEnum<EnvelopeType>("EnvelopeType");
        dataSource.MapEnum<DocumentDataType>("DocumentDataType");
        dataSource.MapEnum<DocumentSigningOrder>("DocumentSigningOrder");
        dataSource.MapEnum<DocumentDistributionMethod>("DocumentDistributionMethod");
        dataSource.MapEnum<ReadStatus>("ReadStatus");
        dataSource.MapEnum<SendStatus>("SendStatus");
        dataSource.MapEnum<SigningStatus>("SigningStatus");
        dataSource.MapEnum<RecipientRole>("RecipientRole");
        dataSource.MapEnum<FieldType>("FieldType");
        dataSource.MapEnum<OrganisationType>("OrganisationType");
        dataSource.MapEnum<OrganisationGroupType>("OrganisationGroupType");
        dataSource.MapEnum<OrganisationMemberRole>("OrganisationMemberRole");
        dataSource.MapEnum<TeamMemberRole>("TeamMemberRole");
        dataSource.MapEnum<OrganisationMemberInviteStatus>("OrganisationMemberInviteStatus");
        dataSource.MapEnum<TemplateType>("TemplateType");
        dataSource.MapEnum<BackgroundJobStatus>("BackgroundJobStatus");
        dataSource.MapEnum<BackgroundJobTaskStatus>("BackgroundJobTaskStatus");
        dataSource.MapEnum<EmailDomainStatus>("EmailDomainStatus");
        dataSource.MapEnum<EmailTransportType>("EmailTransportType");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<IdentityProvider>(name: "IdentityProvider");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<UserSecurityAuditLogType>(name: "UserSecurityAuditLogType");
        modelBuilder.HasPostgresEnum<WebhookTriggerEvents>(name: "WebhookTriggerEvents");
        modelBuilder.HasPostgresEnum<WebhookCallStatus>(name: "WebhookCallStatus");
        modelBuilder.HasPostgresEnum<ApiTokenAlgorithm>(name: "ApiTokenAlgorithm");
        modelBuilder.HasPostgresEnum<SubscriptionStatus>(name: "SubscriptionStatus");
        modelBuilder.HasPostgresEnum<DocumentStatus>(name: "DocumentStatus");
        modelBuilder.HasPostgresEnum<DocumentSource>(name: "DocumentSource");
        modelBuilder.HasPostgresEnum<DocumentVisibility>(name: "DocumentVisibility");
        modelBuilder.HasPostgresEnum<FolderType>(name: "FolderType");
        modelBuilder.HasPostgresEnum<EnvelopeType>(name: "EnvelopeType");
        modelBuilder.HasPostgresEnum<DocumentDataType>(name: "DocumentDataType");
        modelBuilder.HasPostgresEnum<DocumentSigningOrder>(name: "DocumentSigningOrder");
        modelBuilder.HasPostgresEnum<DocumentDistributionMethod>(name: "DocumentDistributionMethod");
        modelBuilder.HasPostgresEnum<ReadStatus>(name: "ReadStatus");
        modelBuilder.HasPostgresEnum<SendStatus>(name: "SendStatus");
        modelBuilder.HasPostgresEnum<SigningStatus>(name: "SigningStatus");
        modelBuilder.HasPostgresEnum<RecipientRole>(name: "RecipientRole");
        modelBuilder.HasPostgresEnum<FieldType>(name: "FieldType");
        modelBuilder.HasPostgresEnum<OrganisationType>(name: "OrganisationType");
        modelBuilder.HasPostgresEnum<OrganisationGroupType>(name: "OrganisationGroupType");
        modelBuilder.HasPostgresEnum<OrganisationMemberRole>(name: "OrganisationMemberRole");
        modelBuilder.HasPostgresEnum<TeamMemberRole>(name: "TeamMemberRole");
        modelBuilder.HasPostgresEnum<OrganisationMemberInviteStatus>(name: "OrganisationMemberInviteStatus");
        modelBuilder.HasPostgresEnum<TemplateType>(name: "TemplateType");
        modelBuilder.HasPostgresEnum<BackgroundJobStatus>(name: "BackgroundJobStatus");
        modelBuilder.HasPostgresEnum<BackgroundJobTaskStatus>(name: "BackgroundJobTaskStatus");
        modelBuilder.HasPostgresEnum<EmailDomainStatus>(name: "EmailDomainStatus");
        modelBuilder.HasPostgresEnum<EmailTransportType>(name: "EmailTransportType");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Email, "User_email_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.Signature).HasColumnName("signature");
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
            entity.Property(e => e.LastSignedIn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastSignedIn");
            entity.Property(e => e.Roles)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<Role> { Role.User })
                .HasColumnName("roles");
            entity.Property(e => e.IdentityProvider)
                .ValueGeneratedNever()
                .HasDefaultValue(IdentityProvider.Documenso)
                .HasColumnName("identityProvider");
            entity.Property(e => e.AvatarImageId).HasColumnName("avatarImageId");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.TwoFactorSecret).HasColumnName("twoFactorSecret");
            entity.Property(e => e.TwoFactorEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("twoFactorEnabled");
            entity.Property(e => e.TwoFactorBackupCodes).HasColumnName("twoFactorBackupCodes");

            entity.HasOne(d => d.AvatarImage).WithMany(p => p.User)
                .HasForeignKey(d => d.AvatarImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_avatarImageId_fkey");
        });

        modelBuilder.Entity<TeamProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamProfile_pkey");

            entity.ToTable("TeamProfile");

            entity.HasIndex(e => e.TeamId, "TeamProfile_teamId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Bio).HasColumnName("bio");

            entity.HasOne(d => d.Team).WithOne(p => p.Profile)
                .HasForeignKey<TeamProfile>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamProfile_teamId_fkey");
        });

        modelBuilder.Entity<UserSecurityAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserSecurityAuditLog_pkey");

            entity.ToTable("UserSecurityAuditLog");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");

            entity.HasOne(d => d.User).WithMany(p => p.SecurityAuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserSecurityAuditLog_userId_fkey");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PasswordResetToken_pkey");

            entity.ToTable("PasswordResetToken");

            entity.HasIndex(e => e.Token, "PasswordResetToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Expiry)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiry");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PasswordResetToken_userId_fkey");
        });

        modelBuilder.Entity<Passkey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Passkey_pkey");

            entity.ToTable("Passkey");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
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
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CredentialPublicKey).HasColumnName("credentialPublicKey");
            entity.Property(e => e.Counter).HasColumnName("counter");
            entity.Property(e => e.CredentialDeviceType).HasColumnName("credentialDeviceType");
            entity.Property(e => e.CredentialBackedUp).HasColumnName("credentialBackedUp");
            entity.Property(e => e.Transports).HasColumnName("transports");

            entity.HasOne(d => d.User).WithMany(p => p.Passkeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Passkey_userId_fkey");
        });

        modelBuilder.Entity<AnonymousVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnonymousVerificationToken_pkey");

            entity.ToTable("AnonymousVerificationToken");

            entity.HasIndex(e => e.Id, "AnonymousVerificationToken_id_key").IsUnique();

            entity.HasIndex(e => e.Token, "AnonymousVerificationToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
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

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VerificationToken_pkey");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => e.SecondaryId, "VerificationToken_secondaryId_key").IsUnique();

            entity.HasIndex(e => e.Token, "VerificationToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.SecondaryId)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("secondaryId");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Completed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("completed");
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
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.VerificationTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerificationToken_userId_fkey");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WebhookUrl).HasColumnName("webhookUrl");
            entity.Property(e => e.EventTriggers).HasColumnName("eventTriggers");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
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
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.User).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_teamId_fkey");
        });

        modelBuilder.Entity<WebhookCall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WebhookCall_pkey");

            entity.ToTable("WebhookCall");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Event).HasColumnName("event");
            entity.Property(e => e.RequestBody)
                .HasColumnType("jsonb")
                .HasColumnName("requestBody");
            entity.Property(e => e.ResponseCode).HasColumnName("responseCode");
            entity.Property(e => e.ResponseHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("responseHeaders");
            entity.Property(e => e.ResponseBody)
                .HasColumnType("jsonb")
                .HasColumnName("responseBody");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");

            entity.HasOne(d => d.Webhook).WithMany(p => p.WebhookCalls)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WebhookCall_webhookId_fkey");
        });

        modelBuilder.Entity<ApiToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiToken_pkey");

            entity.ToTable("ApiToken");

            entity.HasIndex(e => e.Token, "ApiToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Algorithm)
                .ValueGeneratedNever()
                .HasDefaultValue(ApiTokenAlgorithm.Sha512)
                .HasColumnName("algorithm");
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
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.User).WithMany(p => p.ApiTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiToken_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.ApiTokens)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiToken_teamId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.PlanId, "Subscription_planId_key").IsUnique();

            entity.HasIndex(e => e.OrganisationId, "Subscription_organisationId_key").IsUnique();

            entity.HasIndex(e => e.OrganisationId, "Subscription_organisationId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(SubscriptionStatus.Inactive)
                .HasColumnName("status");
            entity.Property(e => e.PlanId).HasColumnName("planId");
            entity.Property(e => e.PriceId).HasColumnName("priceId");
            entity.Property(e => e.PeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("periodEnd");
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
            entity.Property(e => e.CancelAtPeriodEnd)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("cancelAtPeriodEnd");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");

            entity.HasOne(d => d.Organisation).WithOne(p => p.Subscription)
                .HasForeignKey<Subscription>(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Subscription_organisationId_fkey");
        });

        modelBuilder.Entity<SubscriptionClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SubscriptionClaim_pkey");

            entity.ToTable("SubscriptionClaim");

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
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Locked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("locked");
            entity.Property(e => e.TeamCount).HasColumnName("teamCount");
            entity.Property(e => e.MemberCount).HasColumnName("memberCount");
            entity.Property(e => e.EnvelopeItemCount).HasColumnName("envelopeItemCount");
            entity.Property(e => e.RecipientCount).HasColumnName("recipientCount");
            entity.Property(e => e.EnvelopeContentCount).HasColumnName("envelopeContentCount");
            entity.Property(e => e.EnvelopeContentImageCount).HasColumnName("envelopeContentImageCount");
            entity.Property(e => e.Flags)
                .HasColumnType("jsonb")
                .HasColumnName("flags");
            entity.Property(e => e.DocumentRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("documentRateLimits");
            entity.Property(e => e.DocumentQuota).HasColumnName("documentQuota");
            entity.Property(e => e.EmailRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("emailRateLimits");
            entity.Property(e => e.EmailQuota).HasColumnName("emailQuota");
            entity.Property(e => e.ApiRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("apiRateLimits");
            entity.Property(e => e.ApiQuota).HasColumnName("apiQuota");
            entity.Property(e => e.EmailTransportId).HasColumnName("emailTransportId");

            entity.HasOne(d => d.EmailTransport).WithMany(p => p.SubscriptionClaims)
                .HasForeignKey(d => d.EmailTransportId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SubscriptionClaim_emailTransportId_fkey");
        });

        modelBuilder.Entity<OrganisationClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationClaim_pkey");

            entity.ToTable("OrganisationClaim");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.OriginalSubscriptionClaimId).HasColumnName("originalSubscriptionClaimId");
            entity.Property(e => e.TeamCount).HasColumnName("teamCount");
            entity.Property(e => e.MemberCount).HasColumnName("memberCount");
            entity.Property(e => e.EnvelopeItemCount).HasColumnName("envelopeItemCount");
            entity.Property(e => e.RecipientCount).HasColumnName("recipientCount");
            entity.Property(e => e.EnvelopeContentCount).HasColumnName("envelopeContentCount");
            entity.Property(e => e.EnvelopeContentImageCount).HasColumnName("envelopeContentImageCount");
            entity.Property(e => e.Flags)
                .HasColumnType("jsonb")
                .HasColumnName("flags");
            entity.Property(e => e.DocumentRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("documentRateLimits");
            entity.Property(e => e.DocumentQuota).HasColumnName("documentQuota");
            entity.Property(e => e.EmailRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("emailRateLimits");
            entity.Property(e => e.EmailQuota).HasColumnName("emailQuota");
            entity.Property(e => e.ApiRateLimits)
                .HasColumnType("jsonb")
                .HasColumnName("apiRateLimits");
            entity.Property(e => e.ApiQuota).HasColumnName("apiQuota");
            entity.Property(e => e.EmailTransportId).HasColumnName("emailTransportId");

            entity.HasOne(d => d.EmailTransport).WithMany(p => p.OrganisationClaims)
                .HasForeignKey(d => d.EmailTransportId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OrganisationClaim_emailTransportId_fkey");
        });

        modelBuilder.Entity<OrganisationMonthlyStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationMonthlyStat_pkey");

            entity.ToTable("OrganisationMonthlyStat");

            entity.HasIndex(e => e.OrganisationId, "OrganisationMonthlyStat_organisationId_idx");

            entity.HasIndex(e => new { e.OrganisationId, e.Period }, "OrganisationMonthlyStat_organisationId_period_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.DocumentCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("documentCount");
            entity.Property(e => e.EmailCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("emailCount");
            entity.Property(e => e.ApiCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("apiCount");
            entity.Property(e => e.EmailReports)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("emailReports");

            entity.HasOne(d => d.Organisation).WithMany(p => p.MonthlyStats)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationMonthlyStat_organisationId_fkey");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

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
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.CreatedAt1).HasColumnName("created_at");
            entity.Property(e => e.ExtExpiresIn).HasColumnName("ext_expires_in");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");
            entity.Property(e => e.Password).HasColumnName("password");

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

            entity.HasIndex(e => e.SessionToken, "Session_sessionToken_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
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
            entity.Property(e => e.IsEmbed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isEmbed");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<AuthHandoff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuthHandoff_pkey");

            entity.ToTable("AuthHandoff");

            entity.HasIndex(e => e.ExpiresAt, "AuthHandoff_expiresAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Payload).HasColumnName("payload");
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
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Folder_pkey");

            entity.ToTable("Folder");

            entity.HasIndex(e => e.UserId, "Folder_userId_idx");

            entity.HasIndex(e => e.TeamId, "Folder_teamId_idx");

            entity.HasIndex(e => e.ParentId, "Folder_parentId_idx");

            entity.HasIndex(e => e.Type, "Folder_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Pinned)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("pinned");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
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
            entity.Property(e => e.Visibility)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentVisibility.Everyone)
                .HasColumnName("visibility");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Folder_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Folders)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Folder_teamId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Subfolders)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Folder_parentId_fkey");
        });

        modelBuilder.Entity<Envelope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Envelope_pkey");

            entity.ToTable("Envelope");

            entity.HasIndex(e => e.SecondaryId, "Envelope_secondaryId_key").IsUnique();

            entity.HasIndex(e => e.DocumentMetaId, "Envelope_documentMetaId_key").IsUnique();

            entity.HasIndex(e => e.Type, "Envelope_type_idx");

            entity.HasIndex(e => e.Status, "Envelope_status_idx");

            entity.HasIndex(e => e.UserId, "Envelope_userId_idx");

            entity.HasIndex(e => e.TeamId, "Envelope_teamId_idx");

            entity.HasIndex(e => e.FolderId, "Envelope_folderId_idx");

            entity.HasIndex(e => e.CreatedAt, "Envelope_createdAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SecondaryId).HasColumnName("secondaryId");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
            entity.Property(e => e.Type).HasColumnName("type");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentStatus.Draft)
                .HasColumnName("status");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.QrToken).HasColumnName("qrToken");
            entity.Property(e => e.SignatureLevel).HasColumnName("signatureLevel");
            entity.Property(e => e.InternalVersion).HasColumnName("internalVersion");
            entity.Property(e => e.UseLegacyFieldInsertion)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useLegacyFieldInsertion");
            entity.Property(e => e.AuthOptions)
                .HasColumnType("jsonb")
                .HasColumnName("authOptions");
            entity.Property(e => e.FormValues)
                .HasColumnType("jsonb")
                .HasColumnName("formValues");
            entity.Property(e => e.Visibility)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentVisibility.Everyone)
                .HasColumnName("visibility");
            entity.Property(e => e.TemplateType)
                .ValueGeneratedNever()
                .HasDefaultValue(TemplateType.Private)
                .HasColumnName("templateType");
            entity.Property(e => e.PublicTitle)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("publicTitle");
            entity.Property(e => e.PublicDescription)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("publicDescription");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.DocumentMetaId).HasColumnName("documentMetaId");

            entity.HasOne(d => d.User).WithMany(p => p.Envelopes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Envelope_userId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Envelopes)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Envelope_teamId_fkey");

            entity.HasOne(d => d.Folder).WithMany(p => p.Envelopes)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Envelope_folderId_fkey");

            entity.HasOne(d => d.DocumentMeta).WithOne(p => p.Envelope)
                .HasForeignKey<Envelope>(d => d.DocumentMetaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Envelope_documentMetaId_fkey");
        });

        modelBuilder.Entity<EnvelopeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvelopeItem_pkey");

            entity.ToTable("EnvelopeItem");

            entity.HasIndex(e => e.EnvelopeId, "EnvelopeItem_envelopeId_idx");

            entity.HasIndex(e => e.DocumentDataId, "EnvelopeItem_documentDataId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.DocumentDataId).HasColumnName("documentDataId");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");

            entity.HasOne(d => d.DocumentData).WithOne(p => p.EnvelopeItem)
                .HasForeignKey<EnvelopeItem>(d => d.DocumentDataId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvelopeItem_documentDataId_fkey");

            entity.HasOne(d => d.Envelope).WithMany(p => p.EnvelopeItems)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvelopeItem_envelopeId_fkey");
        });

        modelBuilder.Entity<DocumentAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentAuditLog_pkey");

            entity.ToTable("DocumentAuditLog");

            entity.HasIndex(e => e.EnvelopeId, "DocumentAuditLog_envelopeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");

            entity.HasOne(d => d.Envelope).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DocumentAuditLog_envelopeId_fkey");
        });

        modelBuilder.Entity<DocumentData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentData_pkey");

            entity.ToTable("DocumentData");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.InitialData).HasColumnName("initialData");
        });

        modelBuilder.Entity<DocumentMeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentMeta_pkey");

            entity.ToTable("DocumentMeta");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Timezone)
                .ValueGeneratedNever()
                .HasDefaultValue("Etc/UTC")
                .HasColumnName("timezone");
            entity.Property(e => e.DateFormat)
                .ValueGeneratedNever()
                .HasDefaultValue("yyyy-MM-dd hh:mm a")
                .HasColumnName("dateFormat");
            entity.Property(e => e.RedirectUrl).HasColumnName("redirectUrl");
            entity.Property(e => e.SigningOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentSigningOrder.Parallel)
                .HasColumnName("signingOrder");
            entity.Property(e => e.AllowDictateNextSigner)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowDictateNextSigner");
            entity.Property(e => e.TypedSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("typedSignatureEnabled");
            entity.Property(e => e.UploadSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("uploadSignatureEnabled");
            entity.Property(e => e.DrawSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("drawSignatureEnabled");
            entity.Property(e => e.Language)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("language");
            entity.Property(e => e.DistributionMethod)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentDistributionMethod.Email)
                .HasColumnName("distributionMethod");
            entity.Property(e => e.EmailSettings)
                .HasColumnType("jsonb")
                .HasColumnName("emailSettings");
            entity.Property(e => e.EmailReplyTo).HasColumnName("emailReplyTo");
            entity.Property(e => e.EmailId).HasColumnName("emailId");
            entity.Property(e => e.EnvelopeExpirationPeriod)
                .HasColumnType("jsonb")
                .HasColumnName("envelopeExpirationPeriod");
            entity.Property(e => e.ReminderSettings)
                .HasColumnType("jsonb")
                .HasColumnName("reminderSettings");
        });

        modelBuilder.Entity<EnvelopeAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvelopeAttachment_pkey");

            entity.ToTable("EnvelopeAttachment");

            entity.HasIndex(e => e.EnvelopeId, "EnvelopeAttachment_envelopeId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Data).HasColumnName("data");
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
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");

            entity.HasOne(d => d.Envelope).WithMany(p => p.EnvelopeAttachments)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvelopeAttachment_envelopeId_fkey");
        });

        modelBuilder.Entity<Recipient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Recipient_pkey");

            entity.ToTable("Recipient");

            entity.HasIndex(e => e.Token, "Recipient_token_idx");

            entity.HasIndex(e => e.Email, "Recipient_email_idx");

            entity.HasIndex(e => e.EnvelopeId, "Recipient_envelopeId_idx");

            entity.HasIndex(e => e.SignedAt, "Recipient_signedAt_idx");

            entity.HasIndex(e => e.ExpiresAt, "Recipient_expiresAt_idx");

            entity.HasIndex(e => new { e.Email, e.DocumentDeletedAt, e.EnvelopeId }, "Recipient_email_documentDeletedAt_envelopeId_idx");

            entity.HasIndex(e => new { e.Email, e.EnvelopeId }, "Recipient_email_envelopeId_idx");

            entity.HasIndex(e => new { e.Email, e.SigningStatus, e.EnvelopeId, e.Role }, "Recipient_email_signingStatus_envelopeId_role_idx");

            entity.HasIndex(e => e.NextReminderAt, "Recipient_nextReminderAt_idx");

            entity.HasIndex(e => e.Email, "Recipient_email_trgm_idx")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            entity.HasIndex(e => e.Name, "Recipient_name_trgm_idx")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.DocumentDeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("documentDeletedAt");
            entity.Property(e => e.Expired)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expired");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.ExpirationNotifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expirationNotifiedAt");
            entity.Property(e => e.SentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sentAt");
            entity.Property(e => e.SignedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("signedAt");
            entity.Property(e => e.LastReminderSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReminderSentAt");
            entity.Property(e => e.NextReminderAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("nextReminderAt");
            entity.Property(e => e.ReminderCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("reminderCount");
            entity.Property(e => e.AuthOptions)
                .HasColumnType("jsonb")
                .HasColumnName("authOptions");
            entity.Property(e => e.SigningOrder).HasColumnName("signingOrder");
            entity.Property(e => e.RejectionReason).HasColumnName("rejectionReason");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(RecipientRole.Signer)
                .HasColumnName("role");
            entity.Property(e => e.ReadStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(ReadStatus.NotOpened)
                .HasColumnName("readStatus");
            entity.Property(e => e.SigningStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(SigningStatus.NotSigned)
                .HasColumnName("signingStatus");
            entity.Property(e => e.SendStatus)
                .ValueGeneratedNever()
                .HasDefaultValue(SendStatus.NotSent)
                .HasColumnName("sendStatus");

            entity.HasOne(d => d.Envelope).WithMany(p => p.Recipients)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Recipient_envelopeId_fkey");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Field_pkey");

            entity.ToTable("Field");

            entity.HasIndex(e => e.SecondaryId, "Field_secondaryId_key").IsUnique();

            entity.HasIndex(e => e.EnvelopeId, "Field_envelopeId_idx");

            entity.HasIndex(e => e.EnvelopeItemId, "Field_envelopeItemId_idx");

            entity.HasIndex(e => e.RecipientId, "Field_recipientId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.SecondaryId)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("secondaryId");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.EnvelopeItemId).HasColumnName("envelopeItemId");
            entity.Property(e => e.RecipientId).HasColumnName("recipientId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Page).HasColumnName("page");
            entity.Property(e => e.PositionX)
                .ValueGeneratedNever()
                .HasPrecision(65, 30)
                .HasDefaultValue(0m)
                .HasColumnName("positionX");
            entity.Property(e => e.PositionY)
                .ValueGeneratedNever()
                .HasPrecision(65, 30)
                .HasDefaultValue(0m)
                .HasColumnName("positionY");
            entity.Property(e => e.Width)
                .ValueGeneratedNever()
                .HasPrecision(65, 30)
                .HasDefaultValue(-1m)
                .HasColumnName("width");
            entity.Property(e => e.Height)
                .ValueGeneratedNever()
                .HasPrecision(65, 30)
                .HasDefaultValue(-1m)
                .HasColumnName("height");
            entity.Property(e => e.CustomText).HasColumnName("customText");
            entity.Property(e => e.Inserted).HasColumnName("inserted");
            entity.Property(e => e.FieldMeta)
                .HasColumnType("jsonb")
                .HasColumnName("fieldMeta");

            entity.HasOne(d => d.EnvelopeItem).WithMany(p => p.Field)
                .HasForeignKey(d => d.EnvelopeItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Field_envelopeItemId_fkey");

            entity.HasOne(d => d.Envelope).WithMany(p => p.Fields)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Field_envelopeId_fkey");

            entity.HasOne(d => d.Recipient).WithMany(p => p.Fields)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Field_recipientId_fkey");
        });

        modelBuilder.Entity<EnvelopeContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EnvelopeContent_pkey");

            entity.ToTable("EnvelopeContent");

            entity.HasIndex(e => e.EnvelopeId, "EnvelopeContent_envelopeId_idx");

            entity.HasIndex(e => e.EnvelopeItemId, "EnvelopeContent_envelopeItemId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.EnvelopeItemId).HasColumnName("envelopeItemId");
            entity.Property(e => e.ContentMeta)
                .HasColumnType("jsonb")
                .HasColumnName("contentMeta");
            entity.Property(e => e.DataContentId).HasColumnName("dataContentId");

            entity.HasOne(d => d.EnvelopeItem).WithMany(p => p.Contents)
                .HasForeignKey(d => d.EnvelopeItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvelopeContent_envelopeItemId_fkey");

            entity.HasOne(d => d.Envelope).WithMany(p => p.Contents)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EnvelopeContent_envelopeId_fkey");

            entity.HasOne(d => d.DataContent).WithMany(p => p.EnvelopeContents)
                .HasForeignKey(d => d.DataContentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EnvelopeContent_dataContentId_fkey");
        });

        modelBuilder.Entity<DataContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DataContent_pkey");

            entity.ToTable("DataContent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<Signature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Signature_pkey");

            entity.ToTable("Signature");

            entity.HasIndex(e => e.FieldId, "Signature_fieldId_key").IsUnique();

            entity.HasIndex(e => e.RecipientId, "Signature_recipientId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created");
            entity.Property(e => e.RecipientId).HasColumnName("recipientId");
            entity.Property(e => e.FieldId).HasColumnName("fieldId");
            entity.Property(e => e.SignatureImageAsBase64).HasColumnName("signatureImageAsBase64");
            entity.Property(e => e.TypedSignature).HasColumnName("typedSignature");

            entity.HasOne(d => d.Recipient).WithMany(p => p.Signatures)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Signature_recipientId_fkey");

            entity.HasOne(d => d.Field).WithOne(p => p.Signature)
                .HasForeignKey<Signature>(d => d.FieldId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Signature_fieldId_fkey");
        });

        modelBuilder.Entity<CscCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CscCredential_pkey");

            entity.ToTable("CscCredential");

            entity.HasIndex(e => e.RecipientId, "CscCredential_recipientId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.CredentialId).HasColumnName("credentialId");
            entity.Property(e => e.CertCache).HasColumnName("certCache");
            entity.Property(e => e.SignatureAlgorithm).HasColumnName("signatureAlgorithm");
            entity.Property(e => e.KeyType).HasColumnName("keyType");
            entity.Property(e => e.DigestAlgorithm).HasColumnName("digestAlgorithm");
            entity.Property(e => e.KeyLenBits).HasColumnName("keyLenBits");
            entity.Property(e => e.SignAlgoParams).HasColumnName("signAlgoParams");
            entity.Property(e => e.ServiceTokenCiphertext).HasColumnName("serviceTokenCiphertext");
            entity.Property(e => e.ServiceTokenExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("serviceTokenExpiresAt");
            entity.Property(e => e.RecipientId).HasColumnName("recipientId");
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

            entity.HasOne(d => d.Recipient).WithOne(p => p.CscCredential)
                .HasForeignKey<CscCredential>(d => d.RecipientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CscCredential_recipientId_fkey");
        });

        modelBuilder.Entity<CscSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CscSession_pkey");

            entity.ToTable("CscSession");

            entity.HasIndex(e => e.RecipientId, "CscSession_recipientId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.SigningTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("signingTime");
            entity.Property(e => e.ItemsJson)
                .HasColumnType("jsonb")
                .HasColumnName("itemsJson");
            entity.Property(e => e.EncryptedSad).HasColumnName("encryptedSad");
            entity.Property(e => e.SadExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("sadExpiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.RecipientId).HasColumnName("recipientId");

            entity.HasOne(d => d.Recipient).WithOne(p => p.CscSession)
                .HasForeignKey<CscSession>(d => d.RecipientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CscSession_recipientId_fkey");
        });

        modelBuilder.Entity<DocumentShareLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DocumentShareLink_pkey");

            entity.ToTable("DocumentShareLink");

            entity.HasIndex(e => e.Slug, "DocumentShareLink_slug_key").IsUnique();

            entity.HasIndex(e => new { e.EnvelopeId, e.Email }, "DocumentShareLink_envelopeId_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
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

            entity.HasOne(d => d.Envelope).WithMany(p => p.ShareLinks)
                .HasForeignKey(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DocumentShareLink_envelopeId_fkey");
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organisation_pkey");

            entity.ToTable("Organisation");

            entity.HasIndex(e => e.Url, "Organisation_url_key").IsUnique();

            entity.HasIndex(e => e.CustomerId, "Organisation_customerId_key").IsUnique();

            entity.HasIndex(e => e.OrganisationClaimId, "Organisation_organisationClaimId_key").IsUnique();

            entity.HasIndex(e => e.OrganisationGlobalSettingsId, "Organisation_organisationGlobalSettingsId_key").IsUnique();

            entity.HasIndex(e => e.OrganisationAuthenticationPortalId, "Organisation_organisationAuthenticationPortalId_key").IsUnique();

            entity.HasIndex(e => e.Name, "Organisation_name_idx");

            entity.HasIndex(e => e.OwnerUserId, "Organisation_ownerUserId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.AvatarImageId).HasColumnName("avatarImageId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.OrganisationClaimId).HasColumnName("organisationClaimId");
            entity.Property(e => e.OwnerUserId).HasColumnName("ownerUserId");
            entity.Property(e => e.OrganisationGlobalSettingsId).HasColumnName("organisationGlobalSettingsId");
            entity.Property(e => e.OrganisationAuthenticationPortalId).HasColumnName("organisationAuthenticationPortalId");

            entity.HasOne(d => d.OrganisationClaim).WithOne(p => p.Organisation)
                .HasForeignKey<Organisation>(d => d.OrganisationClaimId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Organisation_organisationClaimId_fkey");

            entity.HasOne(d => d.AvatarImage).WithMany(p => p.Organisation)
                .HasForeignKey(d => d.AvatarImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Organisation_avatarImageId_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnedOrganisations)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Organisation_ownerUserId_fkey");

            entity.HasOne(d => d.OrganisationGlobalSettings).WithOne(p => p.Organisation)
                .HasForeignKey<Organisation>(d => d.OrganisationGlobalSettingsId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Organisation_organisationGlobalSettingsId_fkey");

            entity.HasOne(d => d.OrganisationAuthenticationPortal).WithOne(p => p.Organisation)
                .HasForeignKey<Organisation>(d => d.OrganisationAuthenticationPortalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Organisation_organisationAuthenticationPortalId_fkey");
        });

        modelBuilder.Entity<OrganisationMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationMember_pkey");

            entity.ToTable("OrganisationMember");

            entity.HasIndex(e => e.OrganisationId, "OrganisationMember_organisationId_idx");

            entity.HasIndex(e => new { e.UserId, e.OrganisationId }, "OrganisationMember_userId_organisationId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");

            entity.HasOne(d => d.User).WithMany(p => p.OrganisationMember)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationMember_userId_fkey");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Members)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationMember_organisationId_fkey");
        });

        modelBuilder.Entity<OrganisationMemberInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationMemberInvite_pkey");

            entity.ToTable("OrganisationMemberInvite");

            entity.HasIndex(e => e.Token, "OrganisationMemberInvite_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganisationMemberInviteStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");
            entity.Property(e => e.OrganisationRole).HasColumnName("organisationRole");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Invites)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationMemberInvite_organisationId_fkey");
        });

        modelBuilder.Entity<OrganisationGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationGroup_pkey");

            entity.ToTable("OrganisationGroup");

            entity.HasIndex(e => e.OrganisationId, "OrganisationGroup_organisationId_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.OrganisationRole).HasColumnName("organisationRole");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationGroup_organisationId_fkey");
        });

        modelBuilder.Entity<OrganisationGroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationGroupMember_pkey");

            entity.ToTable("OrganisationGroupMember");

            entity.HasIndex(e => e.GroupId, "OrganisationGroupMember_groupId_idx");

            entity.HasIndex(e => e.OrganisationMemberId, "OrganisationGroupMember_organisationMemberId_idx");

            entity.HasIndex(e => new { e.OrganisationMemberId, e.GroupId }, "OrganisationGroupMember_organisationMemberId_groupId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.OrganisationMemberId).HasColumnName("organisationMemberId");

            entity.HasOne(d => d.Group).WithMany(p => p.OrganisationGroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationGroupMember_groupId_fkey");

            entity.HasOne(d => d.OrganisationMember).WithMany(p => p.OrganisationGroupMembers)
                .HasForeignKey(d => d.OrganisationMemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationGroupMember_organisationMemberId_fkey");
        });

        modelBuilder.Entity<TeamGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamGroup_pkey");

            entity.ToTable("TeamGroup");

            entity.HasIndex(e => e.TeamId, "TeamGroup_teamId_idx");

            entity.HasIndex(e => e.OrganisationGroupId, "TeamGroup_organisationGroupId_idx");

            entity.HasIndex(e => new { e.TeamId, e.OrganisationGroupId }, "TeamGroup_teamId_organisationGroupId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganisationGroupId).HasColumnName("organisationGroupId");
            entity.Property(e => e.TeamRole).HasColumnName("teamRole");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.OrganisationGroup).WithMany(p => p.TeamGroups)
                .HasForeignKey(d => d.OrganisationGroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamGroup_organisationGroupId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamGroups)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamGroup_teamId_fkey");
        });

        modelBuilder.Entity<OrganisationGlobalSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationGlobalSettings_pkey");

            entity.ToTable("OrganisationGlobalSettings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentVisibility)
                .ValueGeneratedNever()
                .HasDefaultValue(DocumentVisibility.Everyone)
                .HasColumnName("documentVisibility");
            entity.Property(e => e.DocumentLanguage)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("documentLanguage");
            entity.Property(e => e.IncludeSenderDetails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("includeSenderDetails");
            entity.Property(e => e.IncludeSigningCertificate)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("includeSigningCertificate");
            entity.Property(e => e.IncludeAuditLog)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("includeAuditLog");
            entity.Property(e => e.DocumentTimezone).HasColumnName("documentTimezone");
            entity.Property(e => e.DocumentDateFormat)
                .ValueGeneratedNever()
                .HasDefaultValue("yyyy-MM-dd hh:mm a")
                .HasColumnName("documentDateFormat");
            entity.Property(e => e.DelegateDocumentOwnership)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("delegateDocumentOwnership");
            entity.Property(e => e.TypedSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("typedSignatureEnabled");
            entity.Property(e => e.UploadSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("uploadSignatureEnabled");
            entity.Property(e => e.DrawSignatureEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("drawSignatureEnabled");
            entity.Property(e => e.DefaultRecipients)
                .HasColumnType("jsonb")
                .HasColumnName("defaultRecipients");
            entity.Property(e => e.EmailId).HasColumnName("emailId");
            entity.Property(e => e.EmailReplyTo).HasColumnName("emailReplyTo");
            entity.Property(e => e.EmailDocumentSettings)
                .HasColumnType("jsonb")
                .HasColumnName("emailDocumentSettings");
            entity.Property(e => e.BrandingEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("brandingEnabled");
            entity.Property(e => e.BrandingLogo)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("brandingLogo");
            entity.Property(e => e.BrandingUrl)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("brandingUrl");
            entity.Property(e => e.BrandingCompanyDetails)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("brandingCompanyDetails");
            entity.Property(e => e.BrandingColors)
                .HasColumnType("jsonb")
                .HasColumnName("brandingColors");
            entity.Property(e => e.BrandingCss)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("brandingCss");
            entity.Property(e => e.EnvelopeExpirationPeriod)
                .HasColumnType("jsonb")
                .HasColumnName("envelopeExpirationPeriod");
            entity.Property(e => e.ReminderSettings)
                .HasColumnType("jsonb")
                .HasColumnName("reminderSettings");
            entity.Property(e => e.AiFeaturesEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("aiFeaturesEnabled");

            entity.HasOne(d => d.Email).WithMany(p => p.OrganisationGlobalSettings)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OrganisationGlobalSettings_emailId_fkey");
        });

        modelBuilder.Entity<TeamGlobalSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamGlobalSettings_pkey");

            entity.ToTable("TeamGlobalSettings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentVisibility).HasColumnName("documentVisibility");
            entity.Property(e => e.DocumentLanguage).HasColumnName("documentLanguage");
            entity.Property(e => e.DocumentTimezone).HasColumnName("documentTimezone");
            entity.Property(e => e.DocumentDateFormat).HasColumnName("documentDateFormat");
            entity.Property(e => e.DelegateDocumentOwnership).HasColumnName("delegateDocumentOwnership");
            entity.Property(e => e.IncludeSenderDetails).HasColumnName("includeSenderDetails");
            entity.Property(e => e.IncludeSigningCertificate).HasColumnName("includeSigningCertificate");
            entity.Property(e => e.IncludeAuditLog).HasColumnName("includeAuditLog");
            entity.Property(e => e.TypedSignatureEnabled).HasColumnName("typedSignatureEnabled");
            entity.Property(e => e.UploadSignatureEnabled).HasColumnName("uploadSignatureEnabled");
            entity.Property(e => e.DrawSignatureEnabled).HasColumnName("drawSignatureEnabled");
            entity.Property(e => e.DefaultRecipients)
                .HasColumnType("jsonb")
                .HasColumnName("defaultRecipients");
            entity.Property(e => e.EmailId).HasColumnName("emailId");
            entity.Property(e => e.EmailReplyTo).HasColumnName("emailReplyTo");
            entity.Property(e => e.EmailDocumentSettings)
                .HasColumnType("jsonb")
                .HasColumnName("emailDocumentSettings");
            entity.Property(e => e.BrandingEnabled).HasColumnName("brandingEnabled");
            entity.Property(e => e.BrandingLogo).HasColumnName("brandingLogo");
            entity.Property(e => e.BrandingUrl).HasColumnName("brandingUrl");
            entity.Property(e => e.BrandingCompanyDetails).HasColumnName("brandingCompanyDetails");
            entity.Property(e => e.BrandingColors)
                .HasColumnType("jsonb")
                .HasColumnName("brandingColors");
            entity.Property(e => e.BrandingCss).HasColumnName("brandingCss");
            entity.Property(e => e.EnvelopeExpirationPeriod)
                .HasColumnType("jsonb")
                .HasColumnName("envelopeExpirationPeriod");
            entity.Property(e => e.ReminderSettings)
                .HasColumnType("jsonb")
                .HasColumnName("reminderSettings");
            entity.Property(e => e.AiFeaturesEnabled).HasColumnName("aiFeaturesEnabled");

            entity.HasOne(d => d.Email).WithMany(p => p.TeamGlobalSettings)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TeamGlobalSettings_emailId_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.HasIndex(e => e.Url, "Team_url_key").IsUnique();

            entity.HasIndex(e => e.TeamGlobalSettingsId, "Team_teamGlobalSettingsId_key").IsUnique();

            entity.HasIndex(e => e.Name, "Team_name_idx");

            entity.HasIndex(e => e.OrganisationId, "Team_organisationId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.AvatarImageId).HasColumnName("avatarImageId");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");
            entity.Property(e => e.TeamGlobalSettingsId).HasColumnName("teamGlobalSettingsId");

            entity.HasOne(d => d.AvatarImage).WithMany(p => p.Team)
                .HasForeignKey(d => d.AvatarImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Team_avatarImageId_fkey");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Team_organisationId_fkey");

            entity.HasOne(d => d.TeamGlobalSettings).WithOne(p => p.Team)
                .HasForeignKey<Team>(d => d.TeamGlobalSettingsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Team_teamGlobalSettingsId_fkey");
        });

        modelBuilder.Entity<TeamEmail>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("TeamEmail_pkey");

            entity.ToTable("TeamEmail");

            entity.HasIndex(e => e.TeamId, "TeamEmail_teamId_key").IsUnique();

            entity.HasIndex(e => e.Email, "TeamEmail_email_key").IsUnique();

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("teamId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");

            entity.HasOne(d => d.Team).WithOne(p => p.TeamEmail)
                .HasForeignKey<TeamEmail>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamEmail_teamId_fkey");
        });

        modelBuilder.Entity<TeamEmailVerification>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("TeamEmailVerification_pkey");

            entity.ToTable("TeamEmailVerification");

            entity.HasIndex(e => e.TeamId, "TeamEmailVerification_teamId_key").IsUnique();

            entity.HasIndex(e => e.Token, "TeamEmailVerification_token_key").IsUnique();

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("teamId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Completed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("completed");
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

            entity.HasOne(d => d.Team).WithOne(p => p.EmailVerification)
                .HasForeignKey<TeamEmailVerification>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamEmailVerification_teamId_fkey");
        });

        modelBuilder.Entity<TemplateDirectLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TemplateDirectLink_pkey");

            entity.ToTable("TemplateDirectLink");

            entity.HasIndex(e => e.Id, "TemplateDirectLink_id_key").IsUnique();

            entity.HasIndex(e => e.EnvelopeId, "TemplateDirectLink_envelopeId_key").IsUnique();

            entity.HasIndex(e => e.Token, "TemplateDirectLink_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.EnvelopeId).HasColumnName("envelopeId");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.DirectTemplateRecipientId).HasColumnName("directTemplateRecipientId");

            entity.HasOne(d => d.Envelope).WithOne(p => p.DirectLink)
                .HasForeignKey<TemplateDirectLink>(d => d.EnvelopeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TemplateDirectLink_envelopeId_fkey");
        });

        modelBuilder.Entity<SiteSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SiteSettings_pkey");

            entity.ToTable("SiteSettings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.LastModifiedByUserId).HasColumnName("lastModifiedByUserId");
            entity.Property(e => e.LastModifiedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastModifiedAt");

            entity.HasOne(d => d.LastModifiedByUser).WithMany(p => p.SiteSettings)
                .HasForeignKey(d => d.LastModifiedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SiteSettings_lastModifiedByUserId_fkey");
        });

        modelBuilder.Entity<BackgroundJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BackgroundJob_pkey");

            entity.ToTable("BackgroundJob");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BackgroundJobStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Retried)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("retried");
            entity.Property(e => e.MaxRetries)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("maxRetries");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.SubmittedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("submittedAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.LastRetriedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastRetriedAt");
        });

        modelBuilder.Entity<BackgroundJobTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BackgroundJobTask_pkey");

            entity.ToTable("BackgroundJobTask");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(BackgroundJobTaskStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Result)
                .HasColumnType("jsonb")
                .HasColumnName("result");
            entity.Property(e => e.Retried)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("retried");
            entity.Property(e => e.MaxRetries)
                .ValueGeneratedNever()
                .HasDefaultValue(3)
                .HasColumnName("maxRetries");
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
            entity.Property(e => e.JobId).HasColumnName("jobId");

            entity.HasOne(d => d.BackgroundJob).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BackgroundJobTask_jobId_fkey");
        });

        modelBuilder.Entity<AvatarImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AvatarImage_pkey");

            entity.ToTable("AvatarImage");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Bytes).HasColumnName("bytes");
        });

        modelBuilder.Entity<EmailDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailDomain_pkey");

            entity.ToTable("EmailDomain");

            entity.HasIndex(e => e.Selector, "EmailDomain_selector_key").IsUnique();

            entity.HasIndex(e => e.Domain, "EmailDomain_domain_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(EmailDomainStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.Selector).HasColumnName("selector");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
            entity.Property(e => e.PrivateKey).HasColumnName("privateKey");
            entity.Property(e => e.LastVerifiedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastVerifiedAt");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");

            entity.HasOne(d => d.Organisation).WithMany(p => p.EmailDomains)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmailDomain_organisationId_fkey");
        });

        modelBuilder.Entity<OrganisationEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationEmail_pkey");

            entity.ToTable("OrganisationEmail");

            entity.HasIndex(e => e.Email, "OrganisationEmail_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailName).HasColumnName("emailName");
            entity.Property(e => e.EmailDomainId).HasColumnName("emailDomainId");
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");

            entity.HasOne(d => d.EmailDomain).WithMany(p => p.Emails)
                .HasForeignKey(d => d.EmailDomainId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationEmail_emailDomainId_fkey");

            entity.HasOne(d => d.Organisation).WithMany(p => p.OrganisationEmails)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrganisationEmail_organisationId_fkey");
        });

        modelBuilder.Entity<EmailTransport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailTransport_pkey");

            entity.ToTable("EmailTransport");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.FromName).HasColumnName("fromName");
            entity.Property(e => e.FromAddress).HasColumnName("fromAddress");
            entity.Property(e => e.Config).HasColumnName("config");
        });

        modelBuilder.Entity<OrganisationAuthenticationPortal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrganisationAuthenticationPortal_pkey");

            entity.ToTable("OrganisationAuthenticationPortal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enabled");
            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("clientId");
            entity.Property(e => e.ClientSecret)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("clientSecret");
            entity.Property(e => e.WellKnownUrl)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("wellKnownUrl");
            entity.Property(e => e.DefaultOrganisationRole)
                .ValueGeneratedNever()
                .HasDefaultValue(OrganisationMemberRole.Member)
                .HasColumnName("defaultOrganisationRole");
            entity.Property(e => e.AutoProvisionUsers)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("autoProvisionUsers");
            entity.Property(e => e.AllowedDomains)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("allowedDomains");
            entity.Property(e => e.AllowPersonalOrganisations)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowPersonalOrganisations");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Counter_pkey");

            entity.ToTable("Counter");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<EmbedSignature>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("EmbedSignature_pkey");

            entity.ToTable("EmbedSignature");

            entity.HasIndex(e => e.OrganisationId, "EmbedSignature_organisationId_idx");

            entity.HasIndex(e => e.RecipientId, "EmbedSignature_recipientId_idx");

            entity.Property(e => e.Key).HasColumnName("key");
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
            entity.Property(e => e.OrganisationId).HasColumnName("organisationId");
            entity.Property(e => e.RecipientId).HasColumnName("recipientId");
            entity.Property(e => e.Signature).HasColumnName("signature");

            entity.HasOne(d => d.Organisation).WithMany(p => p.EmbedSignatures)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EmbedSignature_organisationId_fkey");
        });

        modelBuilder.Entity<RateLimit>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Action, e.Bucket }).HasName("RateLimit_pkey");

            entity.ToTable("RateLimit");

            entity.HasIndex(e => e.CreatedAt, "RateLimit_createdAt_idx");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Bucket)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("bucket");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("count");
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
        foreach (var entry in ChangeTracker.Entries<User>())
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

        foreach (var entry in ChangeTracker.Entries<Subscription>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SubscriptionClaim>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganisationClaim>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganisationMonthlyStat>())
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

        foreach (var entry in ChangeTracker.Entries<Folder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Envelope>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EnvelopeAttachment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CscCredential>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DocumentShareLink>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Organisation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganisationMember>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BackgroundJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BackgroundJobTask>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailDomain>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OrganisationEmail>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmailTransport>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<EmbedSignature>())
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
