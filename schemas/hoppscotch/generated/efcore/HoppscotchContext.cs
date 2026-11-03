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

namespace Hoppscotch.Models;

public partial class HoppscotchContext : DbContext
{
    public HoppscotchContext(DbContextOptions<HoppscotchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<TeamInvitation> TeamInvitations { get; set; }

    public virtual DbSet<TeamCollection> TeamCollections { get; set; }

    public virtual DbSet<TeamRequest> TeamRequests { get; set; }

    public virtual DbSet<TeamEnvironment> TeamEnvironments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<UserSettings> UserSettings { get; set; }

    public virtual DbSet<UserHistory> UserHistories { get; set; }

    public virtual DbSet<UserEnvironment> UserEnvironments { get; set; }

    public virtual DbSet<InvitedUsers> InvitedUsers { get; set; }

    public virtual DbSet<UserRequest> UserRequests { get; set; }

    public virtual DbSet<UserCollection> UserCollections { get; set; }

    public virtual DbSet<MockServer> MockServers { get; set; }

    public virtual DbSet<MockServerLog> MockServerLogs { get; set; }

    public virtual DbSet<MockServerActivity> MockServerActivities { get; set; }

    public virtual DbSet<Shortcode> Shortcodes { get; set; }

    public virtual DbSet<InfraConfig> InfraConfigs { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<InfraToken> InfraTokens { get; set; }

    public virtual DbSet<PublishedDocs> PublishedDocs { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<WorkspaceType>("WorkspaceType");
        npgsql.MapEnum<ReqType>("ReqType");
        npgsql.MapEnum<TeamAccessRole>("TeamAccessRole");
        npgsql.MapEnum<MockServerAction>("MockServerAction");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<WorkspaceType>("WorkspaceType");
        dataSource.MapEnum<ReqType>("ReqType");
        dataSource.MapEnum<TeamAccessRole>("TeamAccessRole");
        dataSource.MapEnum<MockServerAction>("MockServerAction");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<WorkspaceType>(name: "WorkspaceType");
        modelBuilder.HasPostgresEnum<ReqType>(name: "ReqType");
        modelBuilder.HasPostgresEnum<TeamAccessRole>(name: "TeamAccessRole");
        modelBuilder.HasPostgresEnum<MockServerAction>(name: "MockServerAction");

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Team_pkey");

            entity.ToTable("Team");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamMember_pkey");

            entity.ToTable("TeamMember");

            entity.HasIndex(e => new { e.TeamID, e.UserUid }, "TeamMember_teamID_userUid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.TeamID).HasColumnName("teamID");

            entity.HasOne(d => d.Team).WithMany(p => p.Members)
                .HasForeignKey(d => d.TeamID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamMember_teamID_fkey");
        });

        modelBuilder.Entity<TeamInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamInvitation_pkey");

            entity.ToTable("TeamInvitation");

            entity.HasIndex(e => e.TeamID, "TeamInvitation_teamID_idx");

            entity.HasIndex(e => new { e.TeamID, e.InviteeEmail }, "TeamInvitation_teamID_inviteeEmail_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamID).HasColumnName("teamID");
            entity.Property(e => e.CreatorUid).HasColumnName("creatorUid");
            entity.Property(e => e.InviteeEmail).HasColumnName("inviteeEmail");
            entity.Property(e => e.InviteeRole).HasColumnName("inviteeRole");

            entity.HasOne(d => d.Team).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.TeamID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamInvitation_teamID_fkey");
        });

        modelBuilder.Entity<TeamCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamCollection_pkey");

            entity.ToTable("TeamCollection");

            entity.HasIndex(e => new { e.TeamID, e.ParentID, e.OrderIndex }, "TeamCollection_teamID_parentID_orderIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ParentID).HasColumnName("parentID");
            entity.Property(e => e.TeamID).HasColumnName("teamID");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamCollection_parentID_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Collections)
                .HasForeignKey(d => d.TeamID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamCollection_teamID_fkey");
        });

        modelBuilder.Entity<TeamRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamRequest_pkey");

            entity.ToTable("TeamRequest");

            entity.HasIndex(e => new { e.TeamID, e.CollectionID, e.OrderIndex }, "TeamRequest_teamID_collectionID_orderIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CollectionID).HasColumnName("collectionID");
            entity.Property(e => e.TeamID).HasColumnName("teamID");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.MockExamples)
                .HasColumnType("jsonb")
                .HasColumnName("mockExamples");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.Collection).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CollectionID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamRequest_collectionID_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Requests)
                .HasForeignKey(d => d.TeamID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamRequest_teamID_fkey");
        });

        modelBuilder.Entity<TeamEnvironment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TeamEnvironment_pkey");

            entity.ToTable("TeamEnvironment");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TeamID).HasColumnName("teamID");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Variables)
                .HasColumnType("jsonb")
                .HasColumnName("variables");

            entity.HasOne(d => d.Team).WithMany(p => p.Environments)
                .HasForeignKey(d => d.TeamID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TeamEnvironment_teamID_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Uid)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("uid");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PhotoURL).HasColumnName("photoURL");
            entity.Property(e => e.IsAdmin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isAdmin");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.CurrentRESTSession)
                .HasColumnType("jsonb")
                .HasColumnName("currentRESTSession");
            entity.Property(e => e.CurrentGQLSession)
                .HasColumnType("jsonb")
                .HasColumnName("currentGQLSession");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.LastLoggedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastLoggedOn");
            entity.Property(e => e.LastActiveOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastActiveOn");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "Account_provider_providerAccountId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderAccountId).HasColumnName("providerAccountId");
            entity.Property(e => e.ProviderRefreshToken).HasColumnName("providerRefreshToken");
            entity.Property(e => e.ProviderAccessToken).HasColumnName("providerAccessToken");
            entity.Property(e => e.ProviderScope).HasColumnName("providerScope");
            entity.Property(e => e.LoggedIn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("loggedIn");

            entity.HasOne(d => d.User).WithMany(p => p.ProviderAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("VerificationToken_token_key");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => new { e.DeviceIdentifier, e.Token }, "VerificationToken_deviceIdentifier_token_key").IsUnique();

            entity.Property(e => e.DeviceIdentifier).HasColumnName("deviceIdentifier");
            entity.Property(e => e.Token)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("token");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.ExpiresOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresOn");

            entity.HasOne(d => d.User).WithMany(p => p.VerificationTokens)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VerificationToken_userUid_fkey");
        });

        modelBuilder.Entity<UserSettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserSettings_pkey");

            entity.ToTable("UserSettings");

            entity.HasIndex(e => e.UserUid, "UserSettings_userUid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.Properties)
                .HasColumnType("jsonb")
                .HasColumnName("properties");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.User).WithOne(p => p.Settings)
                .HasForeignKey<UserSettings>(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserSettings_userUid_fkey");
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserHistory_pkey");

            entity.ToTable("UserHistory");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.ReqType).HasColumnName("reqType");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.ResponseMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("responseMetadata");
            entity.Property(e => e.IsStarred).HasColumnName("isStarred");
            entity.Property(e => e.ExecutedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("executedOn");

            entity.HasOne(d => d.User).WithMany(p => p.History)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserHistory_userUid_fkey");
        });

        modelBuilder.Entity<UserEnvironment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserEnvironment_pkey");

            entity.ToTable("UserEnvironment");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Variables)
                .HasColumnType("jsonb")
                .HasColumnName("variables");
            entity.Property(e => e.IsGlobal).HasColumnName("isGlobal");

            entity.HasOne(d => d.User).WithMany(p => p.Environments)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserEnvironment_userUid_fkey");
        });

        modelBuilder.Entity<InvitedUsers>(entity =>
        {
            entity.HasKey(e => e.InviteeEmail).HasName("InvitedUsers_inviteeEmail_key");

            entity.ToTable("InvitedUsers");

            entity.Property(e => e.AdminUid).HasColumnName("adminUid");
            entity.Property(e => e.AdminEmail).HasColumnName("adminEmail");
            entity.Property(e => e.InviteeEmail).HasColumnName("inviteeEmail");
            entity.Property(e => e.InvitedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("invitedOn");

            entity.HasOne(d => d.User).WithMany(p => p.InvitedUsers)
                .HasForeignKey(d => d.AdminUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InvitedUsers_adminUid_fkey");
        });

        modelBuilder.Entity<UserRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRequest_pkey");

            entity.ToTable("UserRequest");

            entity.HasIndex(e => new { e.UserUid, e.CollectionID, e.OrderIndex }, "UserRequest_userUid_collectionID_orderIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CollectionID).HasColumnName("collectionID");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.MockExamples)
                .HasColumnType("jsonb")
                .HasColumnName("mockExamples");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.UserCollection).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CollectionID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserRequest_collectionID_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRequests)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserRequest_userUid_fkey");
        });

        modelBuilder.Entity<UserCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserCollection_pkey");

            entity.ToTable("UserCollection");

            entity.HasIndex(e => new { e.UserUid, e.ParentID, e.OrderIndex }, "UserCollection_userUid_parentID_orderIndex_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ParentID).HasColumnName("parentID");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.OrderIndex).HasColumnName("orderIndex");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserCollection_parentID_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserCollections)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserCollection_userUid_fkey");
        });

        modelBuilder.Entity<MockServer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MockServer_pkey");

            entity.ToTable("MockServer");

            entity.HasIndex(e => e.Subdomain, "MockServer_subdomain_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Subdomain).HasColumnName("subdomain");
            entity.Property(e => e.CreatorUid).HasColumnName("creatorUid");
            entity.Property(e => e.CollectionID).HasColumnName("collectionID");
            entity.Property(e => e.WorkspaceType).HasColumnName("workspaceType");
            entity.Property(e => e.WorkspaceID).HasColumnName("workspaceID");
            entity.Property(e => e.DelayInMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("delayInMs");
            entity.Property(e => e.IsPublic)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isPublic");
            entity.Property(e => e.IsActive)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.HitCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("hitCount");
            entity.Property(e => e.LastHitAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastHitAt");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("deletedAt");

            entity.HasOne(d => d.User).WithMany(p => p.MockServers)
                .HasForeignKey(d => d.CreatorUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("MockServer_creatorUid_fkey");
        });

        modelBuilder.Entity<MockServerLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MockServerLog_pkey");

            entity.ToTable("MockServerLog");

            entity.HasIndex(e => e.MockServerID, "MockServerLog_mockServerID_idx");

            entity.HasIndex(e => new { e.MockServerID, e.ExecutedAt }, "MockServerLog_mockServerID_executedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.MockServerID).HasColumnName("mockServerID");
            entity.Property(e => e.RequestMethod).HasColumnName("requestMethod");
            entity.Property(e => e.RequestPath).HasColumnName("requestPath");
            entity.Property(e => e.RequestHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("requestHeaders");
            entity.Property(e => e.RequestBody)
                .HasColumnType("jsonb")
                .HasColumnName("requestBody");
            entity.Property(e => e.RequestQuery)
                .HasColumnType("jsonb")
                .HasColumnName("requestQuery");
            entity.Property(e => e.ResponseStatus).HasColumnName("responseStatus");
            entity.Property(e => e.ResponseHeaders)
                .HasColumnType("jsonb")
                .HasColumnName("responseHeaders");
            entity.Property(e => e.ResponseBody)
                .HasColumnType("jsonb")
                .HasColumnName("responseBody");
            entity.Property(e => e.ResponseTime).HasColumnName("responseTime");
            entity.Property(e => e.IpAddress).HasColumnName("ipAddress");
            entity.Property(e => e.UserAgent).HasColumnName("userAgent");
            entity.Property(e => e.ExecutedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("executedAt");

            entity.HasOne(d => d.MockServer).WithMany(p => p.RequestLogs)
                .HasForeignKey(d => d.MockServerID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MockServerLog_mockServerID_fkey");
        });

        modelBuilder.Entity<MockServerActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MockServerActivity_pkey");

            entity.ToTable("MockServerActivity");

            entity.HasIndex(e => e.MockServerID, "MockServerActivity_mockServerID_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.MockServerID).HasColumnName("mockServerID");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.PerformedBy).HasColumnName("performedBy");
            entity.Property(e => e.PerformedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("performedAt");

            entity.HasOne(d => d.MockServer).WithMany(p => p.ActivityHistory)
                .HasForeignKey(d => d.MockServerID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MockServerActivity_mockServerID_fkey");
        });

        modelBuilder.Entity<Shortcode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Shortcode_pkey");

            entity.ToTable("Shortcode");

            entity.HasIndex(e => e.Id, "Shortcode_id_key").IsUnique();

            entity.HasIndex(e => new { e.Id, e.CreatorUid }, "Shortcode_id_creatorUid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.CreatorUid).HasColumnName("creatorUid");
            entity.Property(e => e.EmbedProperties)
                .HasColumnType("jsonb")
                .HasColumnName("embedProperties");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.User).WithMany(p => p.Shortcodes)
                .HasForeignKey(d => d.CreatorUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Shortcode_creatorUid_fkey");
        });

        modelBuilder.Entity<InfraConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InfraConfig_pkey");

            entity.ToTable("InfraConfig");

            entity.HasIndex(e => e.Name, "InfraConfig_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.IsEncrypted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isEncrypted");
            entity.Property(e => e.LastSyncedEnvFileValue).HasColumnName("lastSyncedEnvFileValue");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PersonalAccessToken_pkey");

            entity.ToTable("PersonalAccessToken");

            entity.HasIndex(e => e.Token, "PersonalAccessToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserUid).HasColumnName("userUid");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Token)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("token");
            entity.Property(e => e.ExpiresOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresOn");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");

            entity.HasOne(d => d.User).WithMany(p => p.PersonalAccessTokens)
                .HasForeignKey(d => d.UserUid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PersonalAccessToken_userUid_fkey");
        });

        modelBuilder.Entity<InfraToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InfraToken_pkey");

            entity.ToTable("InfraToken");

            entity.HasIndex(e => e.Token, "InfraToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CreatorUid).HasColumnName("creatorUid");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Token)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("token");
            entity.Property(e => e.ExpiresOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("expiresOn");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");
        });

        modelBuilder.Entity<PublishedDocs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PublishedDocs_pkey");

            entity.ToTable("PublishedDocs");

            entity.HasIndex(e => e.CollectionID, "PublishedDocs_collectionID_idx");

            entity.HasIndex(e => new { e.Slug, e.Version }, "PublishedDocs_slug_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.CollectionID).HasColumnName("collectionID");
            entity.Property(e => e.CreatorUid).HasColumnName("creatorUid");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.AutoSync).HasColumnName("autoSync");
            entity.Property(e => e.DocumentTree)
                .HasColumnType("jsonb")
                .HasColumnName("documentTree");
            entity.Property(e => e.WorkspaceType).HasColumnName("workspaceType");
            entity.Property(e => e.WorkspaceID).HasColumnName("workspaceID");
            entity.Property(e => e.EnvironmentID).HasColumnName("environmentID");
            entity.Property(e => e.EnvironmentName).HasColumnName("environmentName");
            entity.Property(e => e.EnvironmentVariables)
                .HasColumnType("jsonb")
                .HasColumnName("environmentVariables");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedOn)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(3)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdOn");
            entity.Property(e => e.UpdatedOn)
                .HasPrecision(3)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedOn");
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
        foreach (var entry in ChangeTracker.Entries<TeamCollection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TeamRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserSettings>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserRequest>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserCollection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MockServer>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Shortcode>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<InfraConfig>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PersonalAccessToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PublishedDocs>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedOn == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedOn).IsModified))
            {
                entry.Entity.UpdatedOn = now;
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

file sealed class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Utc), v => v)
    {
    }
}
