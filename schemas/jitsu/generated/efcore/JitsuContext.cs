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

namespace Jitsu.Models;

public partial class JitsuContext : DbContext
{
    public JitsuContext(DbContextOptions<JitsuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<UserPreferences> UserPreferences { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserPassword> UserPasswords { get; set; }

    public virtual DbSet<Workspace> Workspaces { get; set; }

    public virtual DbSet<WorkspaceOptions> WorkspaceOptions { get; set; }

    public virtual DbSet<WorkspaceAccess> WorkspaceAccesses { get; set; }

    public virtual DbSet<WorkspaceUserProperties> WorkspaceUserProperties { get; set; }

    public virtual DbSet<UserApiToken> UserApiTokens { get; set; }

    public virtual DbSet<OAuthClient> OAuthClients { get; set; }

    public virtual DbSet<KvStoreEntry> KvStoreEntries { get; set; }

    public virtual DbSet<OAuthAccessToken> OAuthAccessTokens { get; set; }

    public virtual DbSet<InvitationToken> InvitationTokens { get; set; }

    public virtual DbSet<ConfigurationObject> ConfigurationObjects { get; set; }

    public virtual DbSet<ConfigurationObjectLink> ConfigurationObjectLinks { get; set; }

    public virtual DbSet<ConnectorPackage> ConnectorPackages { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<GlobalProps> GlobalProps { get; set; }

    public virtual DbSet<SourceSpec> SourceSpecs { get; set; }

    public virtual DbSet<SourceCheck> SourceChecks { get; set; }

    public virtual DbSet<SourceCatalog> SourceCatalogs { get; set; }

    public virtual DbSet<ReverseSyncControl> ReverseSyncControls { get; set; }

    public virtual DbSet<ReverseSyncTargetOwner> ReverseSyncTargetOwners { get; set; }

    public virtual DbSet<SourceState> SourceStates { get; set; }

    public virtual DbSet<SourceTask> SourceTasks { get; set; }

    public virtual DbSet<TaskLog> TaskLogs { get; set; }

    public virtual DbSet<ProfileBuilder> ProfileBuilders { get; set; }

    public virtual DbSet<ProfileBuilderFunction> ProfileBuilderFunctions { get; set; }

    public virtual DbSet<ProfileBuilderState> ProfileBuilderStates { get; set; }

    public virtual DbSet<ProfileBuilderState2> ProfileBuilderState2s { get; set; }

    public virtual DbSet<StatusChange> StatusChanges { get; set; }

    public virtual DbSet<NotificationState> NotificationStates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OidcProvider> OidcProviders { get; set; }

    public virtual DbSet<OidcLoginGroup> OidcLoginGroups { get; set; }

    public virtual DbSet<ReprocessingJobs> ReprocessingJobs { get; set; }

    public virtual DbSet<ReprocessingWorkers> ReprocessingWorkers { get; set; }

    public virtual DbSet<FunctionsServer> FunctionsServers { get; set; }

    public virtual DbSet<RateLimitCounter> RateLimitCounters { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<WorkspaceRole>("WorkspaceRole");
        npgsql.MapEnum<ReverseSyncMode>("reverse_sync_mode");
        npgsql.MapEnum<ReverseSyncExtraction>("reverse_sync_extraction");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<WorkspaceRole>("WorkspaceRole");
        dataSource.MapEnum<ReverseSyncMode>("reverse_sync_mode");
        dataSource.MapEnum<ReverseSyncExtraction>("reverse_sync_extraction");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<WorkspaceRole>(name: "WorkspaceRole");
        modelBuilder.HasPostgresEnum<ReverseSyncMode>(name: "reverse_sync_mode");
        modelBuilder.HasPostgresEnum<ReverseSyncExtraction>(name: "reverse_sync_extraction");

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmailLog_pkey");

            entity.ToTable("EmailLog");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Time)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("time");
            entity.Property(e => e.Email)
                .HasColumnType("jsonb")
                .HasColumnName("email");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.PreviewUrl).HasColumnName("previewUrl");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<UserPreferences>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserPreferences_pkey");

            entity.ToTable("UserPreferences");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Preferences)
                .HasColumnType("jsonb")
                .HasColumnName("preferences");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UserPreferences_userId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserPreferences_workspaceId_fkey");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserProfile_pkey");

            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.ExternalId, "UserProfile_externalId_idx");

            entity.HasIndex(e => new { e.LoginProvider, e.ExternalId }, "UserProfile_loginProvider_externalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Admin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("admin");
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
            entity.Property(e => e.LoginProvider).HasColumnName("loginProvider");
            entity.Property(e => e.ExternalUsername).HasColumnName("externalUsername");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
        });

        modelBuilder.Entity<UserPassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserPassword_pkey");

            entity.ToTable("UserPassword");

            entity.HasIndex(e => e.UserId, "UserPassword_userId_key").IsUnique();

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.ChangeAtNextLogin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("changeAtNextLogin");

            entity.HasOne(d => d.User).WithOne(p => p.Password)
                .HasForeignKey<UserPassword>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UserPassword_userId_fkey");
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workspace_pkey");

            entity.ToTable("Workspace");

            entity.HasIndex(e => e.Slug, "Workspace_slug_idx");

            entity.HasIndex(e => e.UpdatedAt, "Workspace_updatedAt_idx").IsDescending(true);

            entity.HasIndex(e => e.Slug, "Workspace_slug_key").IsUnique();

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.FeaturesEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("featuresEnabled");
        });

        modelBuilder.Entity<WorkspaceOptions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkspaceOptions_pkey");

            entity.ToTable("WorkspaceOptions");

            entity.HasIndex(e => new { e.WorkspaceId, e.Namespace }, "WorkspaceOptions_workspaceId_namespace_idx");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Namespace).HasColumnName("namespace");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceOptions)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkspaceOptions_workspaceId_fkey");
        });

        modelBuilder.Entity<WorkspaceAccess>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.WorkspaceId }).HasName("WorkspaceAccess_pkey");

            entity.ToTable("WorkspaceAccess");

            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
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
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkspaceRole.Owner)
                .HasColumnName("role");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceAccess)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkspaceAccess_workspaceId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.WorkspaceAccess)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkspaceAccess_userId_fkey");
        });

        modelBuilder.Entity<WorkspaceUserProperties>(entity =>
        {
            entity.HasKey(e => new { e.WorkspaceId, e.UserId }).HasName("WorkspaceUserProperties_pkey");

            entity.ToTable("WorkspaceUserProperties");

            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.LastUsed)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsed");

            entity.HasOne(d => d.Workspace).WithMany(p => p.WorkspaceUserProperties)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkspaceUserProperties_workspaceId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.WorkspaceUserProperties)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("WorkspaceUserProperties_userId_fkey");
        });

        modelBuilder.Entity<UserApiToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserApiToken_pkey");

            entity.ToTable("UserApiToken");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LastUsed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsed");
            entity.Property(e => e.Hint).HasColumnName("hint");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.OauthClientId).HasColumnName("oauthClientId");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UserApiToken_userId_fkey");

            entity.HasOne(d => d.OauthClient).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.OauthClientId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserApiToken_oauthClientId_fkey");
        });

        modelBuilder.Entity<OAuthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthClient_pkey");

            entity.ToTable("OAuthClient");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ClientSecretHash).HasColumnName("clientSecretHash");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
        });

        modelBuilder.Entity<KvStoreEntry>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("kv_pkey");

            entity.ToTable("kv");

            entity.HasIndex(e => e.ExpiresAt, "kv_expires_at_idx");

            entity.HasIndex(e => e.Key, "kv_key_pattern_idx").HasOperators("text_pattern_ops");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires_at");
        });

        modelBuilder.Entity<OAuthAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthAccessToken_pkey");

            entity.ToTable("OAuthAccessToken");

            entity.HasIndex(e => e.ExpiresAt, "OAuthAccessToken_expiresAt_idx");

            entity.HasIndex(e => e.RefreshTokenId, "OAuthAccessToken_refreshTokenId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.LastUsed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsed");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.RefreshTokenId).HasColumnName("refreshTokenId");

            entity.HasOne(d => d.RefreshToken).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.RefreshTokenId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OAuthAccessToken_refreshTokenId_fkey");
        });

        modelBuilder.Entity<InvitationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InvitationToken_pkey");

            entity.ToTable("InvitationToken");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Email).HasColumnName("email");
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
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UsedBy).HasColumnName("usedBy");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(WorkspaceRole.Owner)
                .HasColumnName("role");
        });

        modelBuilder.Entity<ConfigurationObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConfigurationObject_pkey");

            entity.ToTable("ConfigurationObject");

            entity.HasIndex(e => e.UpdatedAt, "ConfigurationObject_updatedAt_idx").IsDescending(true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
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
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ConfigurationObject)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ConfigurationObject_workspaceId_fkey");
        });

        modelBuilder.Entity<ConfigurationObjectLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConfigurationObjectLink_pkey");

            entity.ToTable("ConfigurationObjectLink");

            entity.HasIndex(e => e.UpdatedAt, "ConfigurationObjectLink_updatedAt_idx").IsDescending(true);

            entity.HasIndex(e => e.WorkspaceId, "ConfigurationObjectLink_workspaceId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
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
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("push")
                .HasColumnName("type");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.FromId).HasColumnName("fromId");
            entity.Property(e => e.ToId).HasColumnName("toId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ConfigurationObjectLink)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ConfigurationObjectLink_workspaceId_fkey");

            entity.HasOne(d => d.From).WithMany(p => p.ToLinks)
                .HasForeignKey(d => d.FromId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ConfigurationObjectLink_fromId_fkey");

            entity.HasOne(d => d.To).WithMany(p => p.FromLinks)
                .HasForeignKey(d => d.ToId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ConfigurationObjectLink_toId_fkey");
        });

        modelBuilder.Entity<ConnectorPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConnectorPackage_pkey");

            entity.ToTable("ConnectorPackage");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PackageId).HasColumnName("packageId");
            entity.Property(e => e.PackageType)
                .ValueGeneratedNever()
                .HasDefaultValue("airbyte")
                .HasColumnName("packageType");
            entity.Property(e => e.Meta)
                .HasColumnType("jsonb")
                .HasColumnName("meta");
            entity.Property(e => e.LogoSvg).HasColumnName("logoSvg");
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

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AuditLog_pkey");

            entity.ToTable("AuditLog");

            entity.HasIndex(e => e.Timestamp, "AuditLog_timestamp_idx");

            entity.HasIndex(e => e.Type, "AuditLog_type_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.Timestamp }, "AuditLog_workspaceId_timestamp_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Severity)
                .ValueGeneratedNever()
                .HasDefaultValue("info")
                .HasColumnName("severity");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.Changes)
                .HasColumnType("jsonb")
                .HasColumnName("changes");
            entity.Property(e => e.AuthType).HasColumnName("authType");
            entity.Property(e => e.TokenId).HasColumnName("tokenId");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.RequestHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("requestHeaders");
        });

        modelBuilder.Entity<GlobalProps>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GlobalProps_pkey");

            entity.ToTable("GlobalProps");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
        });

        modelBuilder.Entity<SourceSpec>(entity =>
        {
            entity.HasKey(e => new { e.Package, e.Version }).HasName("source_spec_pkey");

            entity.ToTable("source_spec");

            entity.Property(e => e.Package).HasColumnName("package");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Specs)
                .HasColumnType("jsonb")
                .HasColumnName("specs");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
            entity.Property(e => e.Error).HasColumnName("error");
        });

        modelBuilder.Entity<SourceCheck>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("source_check_pkey");

            entity.ToTable("source_check");

            entity.Property(e => e.Package).HasColumnName("package");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<SourceCatalog>(entity =>
        {
            entity.HasKey(e => new { e.Package, e.Version, e.Key }).HasName("source_catalog_pkey");

            entity.ToTable("source_catalog");

            entity.Property(e => e.Package).HasColumnName("package");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Catalog)
                .HasColumnType("jsonb")
                .HasColumnName("catalog");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<ReverseSyncControl>(entity =>
        {
            entity.HasKey(e => new { e.WorkspaceId, e.SyncId, e.RunId }).HasName("reverse_sync_control_pkey");

            entity.ToTable("reverse_sync_control");

            entity.HasIndex(e => e.SyncId, "reverse_sync_control_sync_id_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.SyncId, e.RunOrder }, "reverse_sync_control_workspace_id_sync_id_run_order_key").IsUnique();

            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.SyncId).HasColumnName("sync_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.RunOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("run_order");
            entity.Property(e => e.Detached)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("detached");
            entity.Property(e => e.Revision).HasColumnName("revision");
            entity.Property(e => e.TargetHash).HasColumnName("target_hash");
            entity.Property(e => e.Phase)
                .ValueGeneratedNever()
                .HasDefaultValue("new")
                .HasColumnName("phase");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.Extraction).HasColumnName("extraction");
            entity.Property(e => e.ArtifactHead).HasColumnName("artifact_head");
            entity.Property(e => e.BaseSequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("base_sequence");
            entity.Property(e => e.NextSequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("next_sequence");
            entity.Property(e => e.CheckpointSequence)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("checkpoint_sequence");
            entity.Property(e => e.FinishSequence).HasColumnName("finish_sequence");
            entity.Property(e => e.Store).HasColumnName("store");
            entity.Property(e => e.ProviderState).HasColumnName("provider_state");
            entity.Property(e => e.FinishResult).HasColumnName("finish_result");
            entity.Property(e => e.CommittedGeneration).HasColumnName("committed_generation");
        });

        modelBuilder.Entity<ReverseSyncTargetOwner>(entity =>
        {
            entity.HasKey(e => e.TargetHash).HasName("reverse_sync_target_owner_pkey");

            entity.ToTable("reverse_sync_target_owner");

            entity.HasIndex(e => new { e.WorkspaceId, e.SyncId }, "reverse_sync_target_owner_workspace_id_sync_id_key").IsUnique();

            entity.Property(e => e.TargetHash).HasColumnName("target_hash");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspace_id");
            entity.Property(e => e.SyncId).HasColumnName("sync_id");
        });

        modelBuilder.Entity<SourceState>(entity =>
        {
            entity.HasKey(e => new { e.SyncId, e.Stream }).HasName("source_state_pkey");

            entity.ToTable("source_state");

            entity.Property(e => e.SyncId).HasColumnName("sync_id");
            entity.Property(e => e.Stream).HasColumnName("stream");
            entity.Property(e => e.State)
                .HasColumnType("jsonb")
                .HasColumnName("state");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<SourceTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("source_task_pkey");

            entity.ToTable("source_task");

            entity.HasIndex(e => e.SyncId, "source_task_sync_id_idx");

            entity.HasIndex(e => e.StartedAt, "source_task_started_at_idx");

            entity.HasIndex(e => new { e.SyncId, e.StartedAt }, "source_task_sync_id_started_at_idx").IsDescending(false, true);

            entity.Property(e => e.SyncId).HasColumnName("sync_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.Package).HasColumnName("package");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.StartedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("started_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.StartedBy)
                .HasColumnType("jsonb")
                .HasColumnName("started_by");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metrics)
                .HasColumnType("jsonb")
                .HasColumnName("metrics");
            entity.Property(e => e.Error).HasColumnName("error");
        });

        modelBuilder.Entity<TaskLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_log_pkey");

            entity.ToTable("task_log");

            entity.HasIndex(e => e.SyncId, "task_log_sync_id_idx");

            entity.HasIndex(e => e.TaskId, "task_log_task_id_idx");

            entity.HasIndex(e => e.Timestamp, "task_log_timestamp_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Logger).HasColumnName("logger");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.SyncId).HasColumnName("sync_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<ProfileBuilder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProfileBuilder_pkey");

            entity.ToTable("ProfileBuilder");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Deleted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("deleted");
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
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.IntermediateStorageCredentials)
                .HasColumnType("jsonb")
                .HasColumnName("intermediateStorageCredentials");
            entity.Property(e => e.DestinationId).HasColumnName("destinationId");
            entity.Property(e => e.ConnectionOptions)
                .HasColumnType("jsonb")
                .HasColumnName("connectionOptions");
            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("version");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ProfileBuilders)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProfileBuilder_workspaceId_fkey");

            entity.HasOne(d => d.Destination).WithMany(p => p.ProfileBuilder)
                .HasForeignKey(d => d.DestinationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ProfileBuilder_destinationId_fkey");
        });

        modelBuilder.Entity<ProfileBuilderFunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProfileBuilderFunction_pkey");

            entity.ToTable("ProfileBuilderFunction");

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.ProfileBuilderId).HasColumnName("profileBuilderId");
            entity.Property(e => e.FunctionId).HasColumnName("functionId");

            entity.HasOne(d => d.ProfileBuilder).WithMany(p => p.Functions)
                .HasForeignKey(d => d.ProfileBuilderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProfileBuilderFunction_profileBuilderId_fkey");

            entity.HasOne(d => d.Function).WithMany(p => p.ProfileBuilderFunction)
                .HasForeignKey(d => d.FunctionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ProfileBuilderFunction_functionId_fkey");
        });

        modelBuilder.Entity<ProfileBuilderState>(entity =>
        {
            entity.HasKey(e => new { e.ProfileBuilderId, e.ProfileBuilderVersion, e.TotalInstances, e.InstanceIndex }).HasName("ProfileBuilderState_pkey");

            entity.ToTable("ProfileBuilderState");

            entity.Property(e => e.ProfileBuilderId).HasColumnName("profileBuilderId");
            entity.Property(e => e.ProfileBuilderVersion).HasColumnName("profileBuilderVersion");
            entity.Property(e => e.InstanceIndex).HasColumnName("instanceIndex");
            entity.Property(e => e.TotalInstances).HasColumnName("totalInstances");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LastTimestamp)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastTimestamp");
            entity.Property(e => e.ProcessedUsers).HasColumnName("processedUsers");
            entity.Property(e => e.TotalUsers).HasColumnName("totalUsers");
            entity.Property(e => e.ErrorUsers).HasColumnName("errorUsers");
            entity.Property(e => e.Speed).HasColumnName("speed");
        });

        modelBuilder.Entity<ProfileBuilderState2>(entity =>
        {
            entity.HasKey(e => e.ProfileBuilderId).HasName("ProfileBuilderState2_pkey");

            entity.ToTable("ProfileBuilderState2");

            entity.Property(e => e.ProfileBuilderId).HasColumnName("profileBuilderId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.FullRebuildInfo)
                .HasColumnType("jsonb")
                .HasColumnName("fullRebuildInfo");
            entity.Property(e => e.QueuesInfo)
                .HasColumnType("jsonb")
                .HasColumnName("queuesInfo");
            entity.Property(e => e.Metrics)
                .HasColumnType("jsonb")
                .HasColumnName("metrics");
        });

        modelBuilder.Entity<StatusChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StatusChange_pkey");

            entity.ToTable("StatusChange");

            entity.HasIndex(e => new { e.ActorId, e.Type, e.TableName }, "StatusChange_actorId_type_tableName_idx");

            entity.HasIndex(e => e.Timestamp, "StatusChange_timestamp_idx");

            entity.HasIndex(e => e.StartedAt, "StatusChange_startedAt_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.TableName).HasColumnName("tableName");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.Counts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("counts");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.QueueSize)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("queueSize");
        });

        modelBuilder.Entity<NotificationState>(entity =>
        {
            entity.HasKey(e => new { e.ChannelId, e.ActorId, e.Type, e.TableName }).HasName("NotificationState_pkey");

            entity.ToTable("NotificationState");

            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.TableName).HasColumnName("tableName");
            entity.Property(e => e.ChannelId).HasColumnName("channelId");
            entity.Property(e => e.FlappingSince)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("flappingSince");
            entity.Property(e => e.LastNotification)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastNotification");
            entity.Property(e => e.StatusChangeId).HasColumnName("statusChangeId");
            entity.Property(e => e.Error).HasColumnName("error");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.HasIndex(e => new { e.ActorId, e.Type, e.TableName }, "Notification_actorId_type_tableName_idx");

            entity.HasIndex(e => e.Timestamp, "Notification_timestamp_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.TableName).HasColumnName("tableName");
            entity.Property(e => e.ChannelId).HasColumnName("channelId");
            entity.Property(e => e.StatusChangeId).HasColumnName("statusChangeId");
            entity.Property(e => e.Timestamp)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("timestamp");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Error).HasColumnName("error");
        });

        modelBuilder.Entity<OidcProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OidcProvider_pkey");

            entity.ToTable("OidcProvider");

            entity.HasIndex(e => new { e.Issuer, e.ClientId }, "OidcProvider_issuer_clientId_key").IsUnique();

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Issuer).HasColumnName("issuer");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientSecret).HasColumnName("clientSecret");
            entity.Property(e => e.AuthorizationEndpoint).HasColumnName("authorizationEndpoint");
            entity.Property(e => e.TokenEndpoint).HasColumnName("tokenEndpoint");
            entity.Property(e => e.UserinfoEndpoint).HasColumnName("userinfoEndpoint");
            entity.Property(e => e.JwksUri).HasColumnName("jwksUri");
            entity.Property(e => e.IntrospectionEndpoint).HasColumnName("introspectionEndpoint");
            entity.Property(e => e.Scopes)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string> { "openid", "profile", "email", "offline_access" })
                .HasColumnName("scopes");
            entity.Property(e => e.Audience).HasColumnName("audience");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.UsernameClaim)
                .ValueGeneratedNever()
                .HasDefaultValue("email")
                .HasColumnName("usernameClaim");
            entity.Property(e => e.EmailClaim)
                .ValueGeneratedNever()
                .HasDefaultValue("email")
                .HasColumnName("emailClaim");
            entity.Property(e => e.NameClaim)
                .ValueGeneratedNever()
                .HasDefaultValue("name")
                .HasColumnName("nameClaim");
            entity.Property(e => e.GroupClaim)
                .ValueGeneratedNever()
                .HasDefaultValue("groups")
                .HasColumnName("groupClaim");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.AutoDiscovery)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("autoDiscovery");
        });

        modelBuilder.Entity<OidcLoginGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OidcLoginGroup_pkey");

            entity.ToTable("OidcLoginGroup");

            entity.HasIndex(e => e.WorkspaceId, "OidcLoginGroup_workspaceId_idx");

            entity.HasIndex(e => e.OidcProviderId, "OidcLoginGroup_oidcProviderId_idx");

            entity.HasIndex(e => new { e.WorkspaceId, e.OidcProviderId }, "OidcLoginGroup_workspaceId_oidcProviderId_key").IsUnique();

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
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.OidcProviderId).HasColumnName("oidcProviderId");
            entity.Property(e => e.GroupValue).HasColumnName("groupValue");
            entity.Property(e => e.AllowAllUsers)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowAllUsers");

            entity.HasOne(d => d.Workspace).WithMany(p => p.OidcLoginGroups)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OidcLoginGroup_workspaceId_fkey");

            entity.HasOne(d => d.OidcProvider).WithMany(p => p.OidcLoginGroups)
                .HasForeignKey(d => d.OidcProviderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OidcLoginGroup_oidcProviderId_fkey");
        });

        modelBuilder.Entity<ReprocessingJobs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reprocessing_jobs_pkey");

            entity.ToTable("reprocessing_jobs");

            entity.HasIndex(e => e.CreatedAt, "reprocessing_jobs_created_at_idx").IsDescending(true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasMaxLength(64)
                .HasColumnName("id");
            entity.Property(e => e.Config)
                .HasColumnType("jsonb")
                .HasColumnName("config");
            entity.Property(e => e.Status)
                .HasMaxLength(32)
                .HasColumnName("status");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.StartedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("started_at");
            entity.Property(e => e.CompletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("completed_at");
            entity.Property(e => e.K8sJobName)
                .HasMaxLength(255)
                .HasColumnName("k8s_job_name");
            entity.Property(e => e.TotalFiles)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("total_files");
            entity.Property(e => e.TotalWorkers)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("total_workers");
            entity.Property(e => e.Error).HasColumnName("error");
        });

        modelBuilder.Entity<ReprocessingWorkers>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.WorkerIndex }).HasName("reprocessing_workers_pkey");

            entity.ToTable("reprocessing_workers");

            entity.HasIndex(e => e.JobId, "reprocessing_workers_job_id_idx");

            entity.HasIndex(e => new { e.JobId, e.Status }, "reprocessing_workers_status_idx");

            entity.Property(e => e.JobId)
                .HasMaxLength(64)
                .HasColumnName("job_id");
            entity.Property(e => e.WorkerIndex).HasColumnName("worker_index");
            entity.Property(e => e.Status)
                .HasMaxLength(32)
                .HasColumnName("status");
            entity.Property(e => e.StartedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("started_at");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.CompletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("completed_at");
            entity.Property(e => e.CurrentFile).HasColumnName("current_file");
            entity.Property(e => e.CurrentLine)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("current_line");
            entity.Property(e => e.CurrentTs).HasColumnName("current_ts");
            entity.Property(e => e.AssignedFiles)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("assigned_files");
            entity.Property(e => e.ProcessedFiles)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processed_files");
            entity.Property(e => e.TotalLines)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_lines");
            entity.Property(e => e.SuccessCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("success_count");
            entity.Property(e => e.ErrorCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("error_count");
            entity.Property(e => e.SkippedCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("skipped_count");
            entity.Property(e => e.TotalBytes)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("total_bytes");
            entity.Property(e => e.ProcessedBytes)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("processed_bytes");
            entity.Property(e => e.Error).HasColumnName("error");

            entity.HasOne(d => d.Job).WithMany(p => p.ReprocessingWorkers)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reprocessing_workers_job_id_fkey");
        });

        modelBuilder.Entity<FunctionsServer>(entity =>
        {
            entity.HasKey(e => new { e.WorkspaceId, e.Class }).HasName("FunctionsServer_pkey");

            entity.ToTable("FunctionsServer");

            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.DeploymentId).HasColumnName("deploymentId");
            entity.Property(e => e.Connections).HasColumnName("connections");
            entity.Property(e => e.EmptyConnections).HasColumnName("emptyConnections");
            entity.Property(e => e.ProfileBuilders).HasColumnName("profileBuilders");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<RateLimitCounter>(entity =>
        {
            entity.HasKey(e => new { e.BucketKey, e.WindowStart }).HasName("RateLimitCounter_pkey");

            entity.ToTable("RateLimitCounter");

            entity.HasIndex(e => e.ExpiresAt, "RateLimitCounter_expiresAt_idx");

            entity.Property(e => e.BucketKey).HasColumnName("bucketKey");
            entity.Property(e => e.WindowStart)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("windowStart");
            entity.Property(e => e.Count)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("count");
            entity.Property(e => e.ExpiresAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresAt");
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
        foreach (var entry in ChangeTracker.Entries<UserPreferences>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserProfile>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserPassword>())
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

        foreach (var entry in ChangeTracker.Entries<WorkspaceOptions>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkspaceAccess>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserApiToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthClient>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InvitationToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConfigurationObject>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConfigurationObjectLink>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ConnectorPackage>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GlobalProps>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProfileBuilder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ProfileBuilderFunction>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OidcProvider>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OidcLoginGroup>())
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

file sealed class UuidV4GuidGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.NewGuid();
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
