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
using Visus.Cuid;

namespace Typebot.Models;

public partial class TypebotContext : DbContext
{
    public TypebotContext(DbContextOptions<TypebotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ApiToken> ApiTokens { get; set; }

    public virtual DbSet<Workspace> Workspaces { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<MemberInWorkspace> MemberInWorkspaces { get; set; }

    public virtual DbSet<WorkspaceInvitation> WorkspaceInvitations { get; set; }

    public virtual DbSet<CustomDomain> CustomDomains { get; set; }

    public virtual DbSet<Credentials> Credentials { get; set; }

    public virtual DbSet<UserCredentials> UserCredentials { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<DashboardFolder> DashboardFolders { get; set; }

    public virtual DbSet<Typebot> Typebots { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<CollaboratorsOnTypebots> CollaboratorsOnTypebots { get; set; }

    public virtual DbSet<PublicTypebot> PublicTypebots { get; set; }

    public virtual DbSet<RuntimeMediaIdCache> RuntimeMediaIdCaches { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<SetVariableHistoryItem> SetVariableHistoryItems { get; set; }

    public virtual DbSet<VisitedEdge> VisitedEdges { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<AnswerV2> AnswerV2s { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<ClaimableCustomPlan> ClaimableCustomPlans { get; set; }

    public virtual DbSet<ChatSession> ChatSessions { get; set; }

    public virtual DbSet<ThemeTemplate> ThemeTemplates { get; set; }

    public virtual DbSet<BannedIp> BannedIps { get; set; }

    public virtual DbSet<SuppressedEmail> SuppressedEmails { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ChatProvider>("ChatProvider");
        npgsql.MapEnum<WorkspaceRole>("WorkspaceRole");
        npgsql.MapEnum<GraphNavigation>("GraphNavigation");
        npgsql.MapEnum<Plan>("Plan");
        npgsql.MapEnum<CollaborationType>("CollaborationType");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ChatProvider>("ChatProvider");
        dataSource.MapEnum<WorkspaceRole>("WorkspaceRole");
        dataSource.MapEnum<GraphNavigation>("GraphNavigation");
        dataSource.MapEnum<Plan>("Plan");
        dataSource.MapEnum<CollaborationType>("CollaborationType");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ChatProvider>(name: "ChatProvider");
        modelBuilder.HasPostgresEnum<WorkspaceRole>(name: "WorkspaceRole");
        modelBuilder.HasPostgresEnum<GraphNavigation>(name: "GraphNavigation");
        modelBuilder.HasPostgresEnum<Plan>(name: "Plan");
        modelBuilder.HasPostgresEnum<CollaborationType>(name: "CollaborationType");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.TokenType).HasColumnName("token_type");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.SessionState).HasColumnName("session_state");
            entity.Property(e => e.OauthTokenSecret).HasColumnName("oauth_token_secret");
            entity.Property(e => e.OauthToken).HasColumnName("oauth_token");
            entity.Property(e => e.RefreshTokenExpiresIn).HasColumnName("refresh_token_expires_in");

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

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SessionToken).HasColumnName("sessionToken");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Session_userId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

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
            entity.Property(e => e.LastActivityAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActivityAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Company).HasColumnName("company");
            entity.Property(e => e.OnboardingCategories)
                .HasColumnType("jsonb")
                .HasColumnName("onboardingCategories");
            entity.Property(e => e.Referral).HasColumnName("referral");
            entity.Property(e => e.GraphNavigation).HasColumnName("graphNavigation");
            entity.Property(e => e.PreferredAppAppearance).HasColumnName("preferredAppAppearance");
            entity.Property(e => e.DisplayedInAppNotifications)
                .HasColumnType("jsonb")
                .HasColumnName("displayedInAppNotifications");
            entity.Property(e => e.GroupTitlesAutoGeneration)
                .HasColumnType("jsonb")
                .HasColumnName("groupTitlesAutoGeneration");
            entity.Property(e => e.PreferredLanguage).HasColumnName("preferredLanguage");
            entity.Property(e => e.TermsAcceptedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("termsAcceptedAt");
        });

        modelBuilder.Entity<ApiToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiToken_pkey");

            entity.ToTable("ApiToken");

            entity.HasIndex(e => e.Token, "ApiToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.ApiTokens)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApiToken_ownerId_fkey");
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workspace_pkey");

            entity.ToTable("Workspace");

            entity.HasIndex(e => e.StripeId, "Workspace_stripeId_key").IsUnique();

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
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Plan)
                .ValueGeneratedNever()
                .HasDefaultValue(Plan.Free)
                .HasColumnName("plan");
            entity.Property(e => e.StripeId).HasColumnName("stripeId");
            entity.Property(e => e.AdditionalChatsIndex)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("additionalChatsIndex");
            entity.Property(e => e.AdditionalStorageIndex)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("additionalStorageIndex");
            entity.Property(e => e.ChatsLimitFirstEmailSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("chatsLimitFirstEmailSentAt");
            entity.Property(e => e.StorageLimitFirstEmailSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("storageLimitFirstEmailSentAt");
            entity.Property(e => e.ChatsLimitSecondEmailSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("chatsLimitSecondEmailSentAt");
            entity.Property(e => e.StorageLimitSecondEmailSentAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("storageLimitSecondEmailSentAt");
            entity.Property(e => e.CustomChatsLimit).HasColumnName("customChatsLimit");
            entity.Property(e => e.CustomStorageLimit).HasColumnName("customStorageLimit");
            entity.Property(e => e.CustomSeatsLimit).HasColumnName("customSeatsLimit");
            entity.Property(e => e.IsQuarantined)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isQuarantined");
            entity.Property(e => e.IsSuspended)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSuspended");
            entity.Property(e => e.IsPastDue)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPastDue");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.ChatsHardLimit).HasColumnName("chatsHardLimit");
            entity.Property(e => e.Settings)
                .HasColumnType("jsonb")
                .HasColumnName("settings");
            entity.Property(e => e.LastActivityAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActivityAt");
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Space_pkey");

            entity.ToTable("Space");

            entity.HasIndex(e => new { e.Id, e.WorkspaceId }, "Space_id_workspaceId_key").IsUnique();

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
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Spaces)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Space_workspaceId_fkey");
        });

        modelBuilder.Entity<MemberInWorkspace>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.WorkspaceId }).HasName("MemberInWorkspace_userId_workspaceId_key");

            entity.ToTable("MemberInWorkspace");

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
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.User).WithMany(p => p.Workspaces)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MemberInWorkspace_userId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Members)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MemberInWorkspace_workspaceId_fkey");
        });

        modelBuilder.Entity<WorkspaceInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkspaceInvitation_pkey");

            entity.ToTable("WorkspaceInvitation");

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
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("WorkspaceInvitation_workspaceId_fkey");
        });

        modelBuilder.Entity<CustomDomain>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("CustomDomain_pkey");

            entity.ToTable("CustomDomain");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.CustomDomains)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CustomDomain_workspaceId_fkey");
        });

        modelBuilder.Entity<Credentials>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Credentials_pkey");

            entity.ToTable("Credentials");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Iv).HasColumnName("iv");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Credentials_workspaceId_fkey");
        });

        modelBuilder.Entity<UserCredentials>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserCredentials_pkey");

            entity.ToTable("UserCredentials");

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
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Iv).HasColumnName("iv");

            entity.HasOne(d => d.User).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserCredentials_userId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("VerificationToken_token_key");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.FailedAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failedAttempts");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
        });

        modelBuilder.Entity<DashboardFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardFolder_pkey");

            entity.ToTable("DashboardFolder");

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
            entity.Property(e => e.ParentFolderId).HasColumnName("parentFolderId");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.ParentFolder).WithMany(p => p.ChildrenFolder)
                .HasForeignKey(d => d.ParentFolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("DashboardFolder_parentFolderId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Folders)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DashboardFolder_workspaceId_fkey");
        });

        modelBuilder.Entity<Typebot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Typebot_pkey");

            entity.ToTable("Typebot");

            entity.HasIndex(e => e.PublicId, "Typebot_publicId_key").IsUnique();

            entity.HasIndex(e => e.CustomDomain, "Typebot_customDomain_key").IsUnique();

            entity.HasIndex(e => e.WorkspaceId, "Typebot_workspaceId_idx");

            entity.HasIndex(e => new { e.IsArchived, e.CreatedAt }, "Typebot_isArchived_createdAt_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Version).HasColumnName("version");
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
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.Groups)
                .HasColumnType("jsonb")
                .HasColumnName("groups");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.Variables)
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.Edges)
                .HasColumnType("jsonb")
                .HasColumnName("edges");
            entity.Property(e => e.Theme)
                .HasColumnType("jsonb")
                .HasColumnName("theme");
            entity.Property(e => e.SelectedThemeTemplateId).HasColumnName("selectedThemeTemplateId");
            entity.Property(e => e.Settings)
                .HasColumnType("jsonb")
                .HasColumnName("settings");
            entity.Property(e => e.PublicId).HasColumnName("publicId");
            entity.Property(e => e.CustomDomain).HasColumnName("customDomain");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ResultsTablePreferences)
                .HasColumnType("jsonb")
                .HasColumnName("resultsTablePreferences");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.IsClosed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isClosed");
            entity.Property(e => e.WhatsAppCredentialsId).HasColumnName("whatsAppCredentialsId");
            entity.Property(e => e.RiskLevel).HasColumnName("riskLevel");
            entity.Property(e => e.SpaceId).HasColumnName("spaceId");

            entity.HasOne(d => d.Folder).WithMany(p => p.Typebots)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Typebot_folderId_fkey");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Typebots)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Typebot_workspaceId_fkey");

            entity.HasOne(d => d.Space).WithMany(p => p.Typebots)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Typebot_spaceId_fkey");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.TypebotId }).HasName("Invitation_email_typebotId_key");

            entity.ToTable("Invitation");

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
            entity.Property(e => e.TypebotId).HasColumnName("typebotId");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Typebot).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.TypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Invitation_typebotId_fkey");
        });

        modelBuilder.Entity<CollaboratorsOnTypebots>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TypebotId }).HasName("CollaboratorsOnTypebots_userId_typebotId_key");

            entity.ToTable("CollaboratorsOnTypebots");

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
            entity.Property(e => e.TypebotId).HasColumnName("typebotId");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Typebot).WithMany(p => p.Collaborators)
                .HasForeignKey(d => d.TypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CollaboratorsOnTypebots_typebotId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CollaboratorsOnTypebots)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CollaboratorsOnTypebots_userId_fkey");
        });

        modelBuilder.Entity<PublicTypebot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PublicTypebot_pkey");

            entity.ToTable("PublicTypebot");

            entity.HasIndex(e => e.TypebotId, "PublicTypebot_typebotId_key").IsUnique();

            entity.HasIndex(e => e.LastActivityAt, "PublicTypebot_lastActivityAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Version).HasColumnName("version");
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
            entity.Property(e => e.LastActivityAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastActivityAt");
            entity.Property(e => e.TypebotId).HasColumnName("typebotId");
            entity.Property(e => e.Groups)
                .HasColumnType("jsonb")
                .HasColumnName("groups");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.Variables)
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.Edges)
                .HasColumnType("jsonb")
                .HasColumnName("edges");
            entity.Property(e => e.Theme)
                .HasColumnType("jsonb")
                .HasColumnName("theme");
            entity.Property(e => e.Settings)
                .HasColumnType("jsonb")
                .HasColumnName("settings");

            entity.HasOne(d => d.Typebot).WithOne(p => p.PublishedTypebot)
                .HasForeignKey<PublicTypebot>(d => d.TypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PublicTypebot_typebotId_fkey");
        });

        modelBuilder.Entity<RuntimeMediaIdCache>(entity =>
        {
            entity.HasKey(e => new { e.PublicTypebotId, e.Provider, e.Url }).HasName("RuntimeMediaIdCache_publicTypebotId_provider_url_key");

            entity.ToTable("RuntimeMediaIdCache");

            entity.HasIndex(e => e.ExpiresAt, "RuntimeMediaIdCache_expiresAt_idx");

            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.MediaId).HasColumnName("mediaId");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.PublicTypebotId).HasColumnName("publicTypebotId");

            entity.HasOne(d => d.PublicTypebot).WithMany(p => p.MediaIdsCache)
                .HasForeignKey(d => d.PublicTypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RuntimeMediaIdCache_publicTypebotId_fkey");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Result_pkey");

            entity.ToTable("Result");

            entity.HasIndex(e => new { e.TypebotId, e.HasStarted, e.CreatedAt }, "Result_typebotId_hasStarted_createdAt_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.TypebotId, e.IsCompleted }, "Result_typebotId_isCompleted_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.TypebotId).HasColumnName("typebotId");
            entity.Property(e => e.Variables)
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
            entity.Property(e => e.HasStarted).HasColumnName("hasStarted");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.LastChatSessionId).HasColumnName("lastChatSessionId");

            entity.HasOne(d => d.Typebot).WithMany(p => p.Results)
                .HasForeignKey(d => d.TypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Result_typebotId_fkey");
        });

        modelBuilder.Entity<SetVariableHistoryItem>(entity =>
        {
            entity.HasKey(e => new { e.ResultId, e.Index }).HasName("SetVariableHistoryItem_resultId_index_key");

            entity.ToTable("SetVariableHistoryItem");

            entity.Property(e => e.ResultId).HasColumnName("resultId");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.VariableId).HasColumnName("variableId");
            entity.Property(e => e.BlockId).HasColumnName("blockId");
            entity.Property(e => e.BlockIndex).HasColumnName("blockIndex");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");

            entity.HasOne(d => d.Result).WithMany(p => p.SetVariableHistory)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SetVariableHistoryItem_resultId_fkey");
        });

        modelBuilder.Entity<VisitedEdge>(entity =>
        {
            entity.HasKey(e => new { e.ResultId, e.Index }).HasName("VisitedEdge_resultId_index_key");

            entity.ToTable("VisitedEdge");

            entity.Property(e => e.ResultId).HasColumnName("resultId");
            entity.Property(e => e.EdgeId).HasColumnName("edgeId");
            entity.Property(e => e.Index).HasColumnName("index");

            entity.HasOne(d => d.Result).WithMany(p => p.Edges)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VisitedEdge_resultId_fkey");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Log_pkey");

            entity.ToTable("Log");

            entity.HasIndex(e => e.ResultId, "Log_resultId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ResultId).HasColumnName("resultId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Context).HasColumnName("context");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Details).HasColumnName("details");

            entity.HasOne(d => d.Result).WithMany(p => p.Logs)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Log_resultId_fkey");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => new { e.ResultId, e.BlockId, e.GroupId }).HasName("Answer_resultId_blockId_groupId_key");

            entity.ToTable("Answer");

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ResultId).HasColumnName("resultId");
            entity.Property(e => e.BlockId).HasColumnName("blockId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.VariableId).HasColumnName("variableId");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.Result).WithMany(p => p.Answers)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Answer_resultId_fkey");
        });

        modelBuilder.Entity<AnswerV2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnswerV2_pkey");

            entity.ToTable("AnswerV2");

            entity.HasIndex(e => e.BlockId, "AnswerV2_blockId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.BlockId).HasColumnName("blockId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.AttachedFileUrls)
                .HasColumnType("jsonb")
                .HasColumnName("attachedFileUrls");
            entity.Property(e => e.ResultId).HasColumnName("resultId");

            entity.HasOne(d => d.Result).WithMany(p => p.AnswersV2)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AnswerV2_resultId_fkey");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("Coupon_pkey");

            entity.ToTable("Coupon");

            entity.HasIndex(e => e.Code, "Coupon_code_key").IsUnique();

            entity.Property(e => e.UserPropertiesToUpdate)
                .HasColumnType("jsonb")
                .HasColumnName("userPropertiesToUpdate");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.DateRedeemed)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("dateRedeemed");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhook_pkey");

            entity.ToTable("Webhook");

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
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.QueryParams)
                .HasColumnType("jsonb")
                .HasColumnName("queryParams");
            entity.Property(e => e.Headers)
                .HasColumnType("jsonb")
                .HasColumnName("headers");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.TypebotId).HasColumnName("typebotId");

            entity.HasOne(d => d.Typebot).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.TypebotId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Webhook_typebotId_fkey");
        });

        modelBuilder.Entity<ClaimableCustomPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClaimableCustomPlan_pkey");

            entity.ToTable("ClaimableCustomPlan");

            entity.HasIndex(e => e.WorkspaceId, "ClaimableCustomPlan_workspaceId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("claimedAt");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.ChatsLimit).HasColumnName("chatsLimit");
            entity.Property(e => e.StorageLimit).HasColumnName("storageLimit");
            entity.Property(e => e.SeatsLimit).HasColumnName("seatsLimit");
            entity.Property(e => e.IsYearly)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isYearly");
            entity.Property(e => e.CompanyName).HasColumnName("companyName");
            entity.Property(e => e.VatType).HasColumnName("vatType");
            entity.Property(e => e.VatValue).HasColumnName("vatValue");

            entity.HasOne(d => d.Workspace).WithOne(p => p.ClaimableCustomPlan)
                .HasForeignKey<ClaimableCustomPlan>(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ClaimableCustomPlan_workspaceId_fkey");
        });

        modelBuilder.Entity<ChatSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChatSession_pkey");

            entity.ToTable("ChatSession");

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
            entity.Property(e => e.State)
                .HasColumnType("jsonb")
                .HasColumnName("state");
            entity.Property(e => e.IsReplying).HasColumnName("isReplying");
        });

        modelBuilder.Entity<ThemeTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ThemeTemplate_pkey");

            entity.ToTable("ThemeTemplate");

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
            entity.Property(e => e.Theme)
                .HasColumnType("jsonb")
                .HasColumnName("theme");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");

            entity.HasOne(d => d.Workspace).WithMany(p => p.ThemeTemplates)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ThemeTemplate_workspaceId_fkey");
        });

        modelBuilder.Entity<BannedIp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BannedIp_pkey");

            entity.ToTable("BannedIp");

            entity.HasIndex(e => e.Ip, "BannedIp_ip_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.ResponsibleTypebotId).HasColumnName("responsibleTypebotId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ResponsibleTypebot).WithMany(p => p.BannedIps)
                .HasForeignKey(d => d.ResponsibleTypebotId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BannedIp_responsibleTypebotId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.BannedIps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("BannedIp_userId_fkey");
        });

        modelBuilder.Entity<SuppressedEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SuppressedEmail_pkey");

            entity.ToTable("SuppressedEmail");

            entity.HasIndex(e => e.Email, "SuppressedEmail_email_key").IsUnique();

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
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.TransientGeneralBounceCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("transientGeneralBounceCount");
            entity.Property(e => e.SuppressedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("suppressedAt");
            entity.Property(e => e.LastWebhookId).HasColumnName("lastWebhookId");
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

        foreach (var entry in ChangeTracker.Entries<Workspace>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Space>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MemberInWorkspace>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<WorkspaceInvitation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DashboardFolder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Typebot>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Invitation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<CollaboratorsOnTypebots>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Answer>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.CreatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.CreatedAt).IsModified))
            {
                entry.Entity.CreatedAt = now;
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

        foreach (var entry in ChangeTracker.Entries<ChatSession>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ThemeTemplate>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SuppressedEmail>())
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
