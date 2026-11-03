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

namespace Postiz.Models;

public partial class PostizContext : DbContext
{
    public PostizContext(DbContextOptions<PostizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Tags> Tags { get; set; }

    public virtual DbSet<TagsPosts> TagsPosts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsedCodes> UsedCodes { get; set; }

    public virtual DbSet<UserOrganization> UserOrganizations { get; set; }

    public virtual DbSet<GitHub> GitHubs { get; set; }

    public virtual DbSet<Trending> Trendings { get; set; }

    public virtual DbSet<TrendingLog> TrendingLogs { get; set; }

    public virtual DbSet<ItemUser> ItemUsers { get; set; }

    public virtual DbSet<Star> Stars { get; set; }

    public virtual DbSet<Media> Media { get; set; }

    public virtual DbSet<SocialMediaAgency> SocialMediaAgencies { get; set; }

    public virtual DbSet<SocialMediaAgencyNiche> SocialMediaAgencyNiches { get; set; }

    public virtual DbSet<Credits> Credits { get; set; }

    public virtual DbSet<Clipping> Clippings { get; set; }

    public virtual DbSet<ClippingClip> ClippingClips { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<Signatures> Signatures { get; set; }

    public virtual DbSet<Comments> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<MessagesGroup> MessagesGroups { get; set; }

    public virtual DbSet<PayoutProblems> PayoutProblems { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<OrderItems> OrderItems { get; set; }

    public virtual DbSet<Messages> Messages { get; set; }

    public virtual DbSet<Plugs> Plugs { get; set; }

    public virtual DbSet<ExisingPlugData> ExisingPlugData { get; set; }

    public virtual DbSet<PopularPosts> PopularPosts { get; set; }

    public virtual DbSet<IntegrationsWebhooks> IntegrationsWebhooks { get; set; }

    public virtual DbSet<Webhooks> Webhooks { get; set; }

    public virtual DbSet<AutoPost> AutoPosts { get; set; }

    public virtual DbSet<Sets> Sets { get; set; }

    public virtual DbSet<ThirdParty> ThirdParties { get; set; }

    public virtual DbSet<Errors> Errors { get; set; }

    public virtual DbSet<Mentions> Mentions { get; set; }

    public virtual DbSet<MastraMessages> MastraMessages { get; set; }

    public virtual DbSet<MastraResources> MastraResources { get; set; }

    public virtual DbSet<MastraScorers> MastraScorers { get; set; }

    public virtual DbSet<MastraThreads> MastraThreads { get; set; }

    public virtual DbSet<MastraTraces> MastraTraces { get; set; }

    public virtual DbSet<MastraWorkflowSnapshot> MastraWorkflowSnapshots { get; set; }

    public virtual DbSet<MastraAgentVersions> MastraAgentVersions { get; set; }

    public virtual DbSet<MastraAgents> MastraAgents { get; set; }

    public virtual DbSet<MastraBackgroundTasks> MastraBackgroundTasks { get; set; }

    public virtual DbSet<MastraChannelConfig> MastraChannelConfigs { get; set; }

    public virtual DbSet<MastraChannelInstallations> MastraChannelInstallations { get; set; }

    public virtual DbSet<MastraDatasetItems> MastraDatasetItems { get; set; }

    public virtual DbSet<MastraDatasetVersions> MastraDatasetVersions { get; set; }

    public virtual DbSet<MastraDatasets> MastraDatasets { get; set; }

    public virtual DbSet<MastraExperimentResults> MastraExperimentResults { get; set; }

    public virtual DbSet<MastraExperiments> MastraExperiments { get; set; }

    public virtual DbSet<MastraFavorites> MastraFavorites { get; set; }

    public virtual DbSet<MastraKnowledgeActivity> MastraKnowledgeActivities { get; set; }

    public virtual DbSet<MastraKnowledgeCursors> MastraKnowledgeCursors { get; set; }

    public virtual DbSet<MastraKnowledgeMentions> MastraKnowledgeMentions { get; set; }

    public virtual DbSet<MastraKnowledgeNodes> MastraKnowledgeNodes { get; set; }

    public virtual DbSet<MastraKnowledgeRecords> MastraKnowledgeRecords { get; set; }

    public virtual DbSet<MastraKnowledgeSemanticOutbox> MastraKnowledgeSemanticOutboxes { get; set; }

    public virtual DbSet<MastraMcpClientVersions> MastraMcpClientVersions { get; set; }

    public virtual DbSet<MastraMcpClients> MastraMcpClients { get; set; }

    public virtual DbSet<MastraMcpServerVersions> MastraMcpServerVersions { get; set; }

    public virtual DbSet<MastraMcpServers> MastraMcpServers { get; set; }

    public virtual DbSet<MastraObservationalMemory> MastraObservationalMemories { get; set; }

    public virtual DbSet<MastraPromptBlockVersions> MastraPromptBlockVersions { get; set; }

    public virtual DbSet<MastraPromptBlocks> MastraPromptBlocks { get; set; }

    public virtual DbSet<MastraScheduleTriggers> MastraScheduleTriggers { get; set; }

    public virtual DbSet<MastraSchedules> MastraSchedules { get; set; }

    public virtual DbSet<MastraScorerDefinitionVersions> MastraScorerDefinitionVersions { get; set; }

    public virtual DbSet<MastraScorerDefinitions> MastraScorerDefinitions { get; set; }

    public virtual DbSet<MastraSkillBlobs> MastraSkillBlobs { get; set; }

    public virtual DbSet<MastraSkillVersions> MastraSkillVersions { get; set; }

    public virtual DbSet<MastraSkills> MastraSkills { get; set; }

    public virtual DbSet<MastraThreadState> MastraThreadStates { get; set; }

    public virtual DbSet<MastraToolProviderConnections> MastraToolProviderConnections { get; set; }

    public virtual DbSet<MastraWorkflowDefinitions> MastraWorkflowDefinitions { get; set; }

    public virtual DbSet<MastraWorkspaceVersions> MastraWorkspaceVersions { get; set; }

    public virtual DbSet<MastraWorkspaces> MastraWorkspaces { get; set; }

    public virtual DbSet<OAuthApp> OAuthApps { get; set; }

    public virtual DbSet<OAuthAuthorization> OAuthAuthorizations { get; set; }

    public virtual DbSet<OAuthSelfHostedAuthorization> OAuthSelfHostedAuthorizations { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<OrderStatus>("OrderStatus");
        npgsql.MapEnum<From>("From");
        npgsql.MapEnum<State>("State");
        npgsql.MapEnum<SubscriptionTier>("SubscriptionTier");
        npgsql.MapEnum<Period>("Period");
        npgsql.MapEnum<Provider>("Provider");
        npgsql.MapEnum<Role>("Role");
        npgsql.MapEnum<ApprovedSubmitForOrder>("APPROVED_SUBMIT_FOR_ORDER");
        npgsql.MapEnum<CreationMethod>("CreationMethod");
        npgsql.MapEnum<ShortLinkPreference>("ShortLinkPreference");
        npgsql.MapEnum<AnnouncementColor>("AnnouncementColor");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<OrderStatus>("OrderStatus");
        dataSource.MapEnum<From>("From");
        dataSource.MapEnum<State>("State");
        dataSource.MapEnum<SubscriptionTier>("SubscriptionTier");
        dataSource.MapEnum<Period>("Period");
        dataSource.MapEnum<Provider>("Provider");
        dataSource.MapEnum<Role>("Role");
        dataSource.MapEnum<ApprovedSubmitForOrder>("APPROVED_SUBMIT_FOR_ORDER");
        dataSource.MapEnum<CreationMethod>("CreationMethod");
        dataSource.MapEnum<ShortLinkPreference>("ShortLinkPreference");
        dataSource.MapEnum<AnnouncementColor>("AnnouncementColor");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<OrderStatus>(name: "OrderStatus");
        modelBuilder.HasPostgresEnum<From>(name: "From");
        modelBuilder.HasPostgresEnum<State>(name: "State");
        modelBuilder.HasPostgresEnum<SubscriptionTier>(name: "SubscriptionTier");
        modelBuilder.HasPostgresEnum<Period>(name: "Period");
        modelBuilder.HasPostgresEnum<Provider>(name: "Provider");
        modelBuilder.HasPostgresEnum<Role>(name: "Role");
        modelBuilder.HasPostgresEnum<ApprovedSubmitForOrder>(name: "APPROVED_SUBMIT_FOR_ORDER");
        modelBuilder.HasPostgresEnum<CreationMethod>(name: "CreationMethod");
        modelBuilder.HasPostgresEnum<ShortLinkPreference>(name: "ShortLinkPreference");
        modelBuilder.HasPostgresEnum<AnnouncementColor>(name: "AnnouncementColor");

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organization_pkey");

            entity.ToTable("Organization");

            entity.HasIndex(e => e.ApiKey, "Organization_apiKey_idx");

            entity.HasIndex(e => e.StreakSince, "Organization_streakSince_idx");

            entity.HasIndex(e => e.PaymentId, "Organization_paymentId_idx");

            entity.HasIndex(e => e.DeletedAt, "Organization_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.PaymentId).HasColumnName("paymentId");
            entity.Property(e => e.StreakSince)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("streakSince");
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
            entity.Property(e => e.AllowTrial)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allowTrial");
            entity.Property(e => e.IsTrailing)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTrailing");
            entity.Property(e => e.Shortlink)
                .ValueGeneratedNever()
                .HasDefaultValue(ShortLinkPreference.Ask)
                .HasColumnName("shortlink");
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tags_pkey");

            entity.ToTable("Tags");

            entity.HasIndex(e => e.OrgId, "Tags_orgId_idx");

            entity.HasIndex(e => e.DeletedAt, "Tags_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.OrgId).HasColumnName("orgId");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Tags)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tags_orgId_fkey");
        });

        modelBuilder.Entity<TagsPosts>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.TagId }).HasName("TagsPosts_pkey");

            entity.ToTable("TagsPosts");

            entity.HasIndex(e => new { e.PostId, e.TagId }, "TagsPosts_postId_tagId_key").IsUnique();

            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.TagId).HasColumnName("tagId");
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

            entity.HasOne(d => d.Post).WithMany(p => p.Tags)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TagsPosts_postId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.Posts)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("TagsPosts_tagId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.LastReadNotifications, "User_lastReadNotifications_idx");

            entity.HasIndex(e => e.InviteId, "User_inviteId_idx");

            entity.HasIndex(e => e.Account, "User_account_idx");

            entity.HasIndex(e => e.LastOnline, "User_lastOnline_idx");

            entity.HasIndex(e => e.PictureId, "User_pictureId_idx");

            entity.HasIndex(e => e.DeletedAt, "User_deletedAt_idx");

            entity.HasIndex(e => new { e.Email, e.ProviderName }, "User_email_providerName_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.ProviderName).HasColumnName("providerName");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.IsSuperAdmin)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSuperAdmin");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Audience)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("audience");
            entity.Property(e => e.PictureId).HasColumnName("pictureId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.TimezoneName).HasColumnName("timezoneName");
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
            entity.Property(e => e.LastReadNotifications)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastReadNotifications");
            entity.Property(e => e.InviteId).HasColumnName("inviteId");
            entity.Property(e => e.Activated)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("activated");
            entity.Property(e => e.Account).HasColumnName("account");
            entity.Property(e => e.ConnectedAccount)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("connectedAccount");
            entity.Property(e => e.LastOnline)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastOnline");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.Agent).HasColumnName("agent");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.SendSuccessEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("sendSuccessEmails");
            entity.Property(e => e.SendFailureEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("sendFailureEmails");
            entity.Property(e => e.SendStreakEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("sendStreakEmails");

            entity.HasOne(d => d.Picture).WithMany(p => p.UserPicture)
                .HasForeignKey(d => d.PictureId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_pictureId_fkey");
        });

        modelBuilder.Entity<UsedCodes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UsedCodes_pkey");

            entity.ToTable("UsedCodes");

            entity.HasIndex(e => e.Code, "UsedCodes_code_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.OrgId).HasColumnName("orgId");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.UsedCodes)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UsedCodes_orgId_fkey");
        });

        modelBuilder.Entity<UserOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserOrganization_pkey");

            entity.ToTable("UserOrganization");

            entity.HasIndex(e => e.OrganizationId, "UserOrganization_organizationId_idx");

            entity.HasIndex(e => e.Disabled, "UserOrganization_disabled_idx");

            entity.HasIndex(e => new { e.UserId, e.OrganizationId }, "UserOrganization_userId_organizationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue(Role.User)
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UserOrganization_organizationId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("UserOrganization_userId_fkey");
        });

        modelBuilder.Entity<GitHub>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GitHub_pkey");

            entity.ToTable("GitHub");

            entity.HasIndex(e => e.Login, "GitHub_login_idx");

            entity.HasIndex(e => e.OrganizationId, "GitHub_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.JobId).HasColumnName("jobId");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Github)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("GitHub_organizationId_fkey");
        });

        modelBuilder.Entity<Trending>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Trending_pkey");

            entity.ToTable("Trending");

            entity.HasIndex(e => e.Language, "Trending_language_key").IsUnique();

            entity.HasIndex(e => e.Hash, "Trending_hash_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TrendingList).HasColumnName("trendingList");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
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

        modelBuilder.Entity<TrendingLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrendingLog_pkey");

            entity.ToTable("TrendingLog");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Date)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("date");
        });

        modelBuilder.Entity<ItemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ItemUser_pkey");

            entity.ToTable("ItemUser");

            entity.HasIndex(e => e.UserId, "ItemUser_userId_idx");

            entity.HasIndex(e => e.Key, "ItemUser_key_idx");

            entity.HasIndex(e => new { e.UserId, e.Key }, "ItemUser_userId_key_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Key).HasColumnName("key");

            entity.HasOne(d => d.User).WithMany(p => p.Items)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ItemUser_userId_fkey");
        });

        modelBuilder.Entity<Star>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Star_pkey");

            entity.ToTable("Star");

            entity.HasIndex(e => new { e.Login, e.Date }, "Star_login_date_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.TotalStars).HasColumnName("totalStars");
            entity.Property(e => e.Forks).HasColumnName("forks");
            entity.Property(e => e.TotalForks).HasColumnName("totalForks");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Date)
                .HasValueGenerator<UtcTodayGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date");
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

        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Media_pkey");

            entity.ToTable("Media");

            entity.HasIndex(e => e.Name, "Media_name_idx");

            entity.HasIndex(e => e.OrganizationId, "Media_organizationId_idx");

            entity.HasIndex(e => e.Type, "Media_type_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OriginalName).HasColumnName("originalName");
            entity.Property(e => e.Path).HasColumnName("path");
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
            entity.Property(e => e.FileSize)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("fileSize");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("image")
                .HasColumnName("type");
            entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");
            entity.Property(e => e.Alt).HasColumnName("alt");
            entity.Property(e => e.ThumbnailTimestamp).HasColumnName("thumbnailTimestamp");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("ready")
                .HasColumnName("status");
            entity.Property(e => e.ProcessingError).HasColumnName("processingError");

            entity.HasOne(d => d.Organization).WithMany(p => p.Media)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Media_organizationId_fkey");
        });

        modelBuilder.Entity<SocialMediaAgency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SocialMediaAgency_pkey");

            entity.ToTable("SocialMediaAgency");

            entity.HasIndex(e => e.UserId, "SocialMediaAgency_userId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "SocialMediaAgency_userId_idx");

            entity.HasIndex(e => e.DeletedAt, "SocialMediaAgency_deletedAt_idx");

            entity.HasIndex(e => e.Id, "SocialMediaAgency_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LogoId).HasColumnName("logoId");
            entity.Property(e => e.Website).HasColumnName("website");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.Facebook).HasColumnName("facebook");
            entity.Property(e => e.Instagram).HasColumnName("instagram");
            entity.Property(e => e.Twitter).HasColumnName("twitter");
            entity.Property(e => e.LinkedIn).HasColumnName("linkedIn");
            entity.Property(e => e.Youtube).HasColumnName("youtube");
            entity.Property(e => e.Tiktok).HasColumnName("tiktok");
            entity.Property(e => e.OtherSocialMedia).HasColumnName("otherSocialMedia");
            entity.Property(e => e.ShortDescription).HasColumnName("shortDescription");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Approved)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("approved");
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

            entity.HasOne(d => d.Logo).WithMany(p => p.Agencies)
                .HasForeignKey(d => d.LogoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SocialMediaAgency_logoId_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.Agencies)
                .HasForeignKey<SocialMediaAgency>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SocialMediaAgency_userId_fkey");
        });

        modelBuilder.Entity<SocialMediaAgencyNiche>(entity =>
        {
            entity.HasKey(e => new { e.AgencyId, e.Niche }).HasName("SocialMediaAgencyNiche_pkey");

            entity.ToTable("SocialMediaAgencyNiche");

            entity.Property(e => e.AgencyId).HasColumnName("agencyId");
            entity.Property(e => e.Niche).HasColumnName("niche");

            entity.HasOne(d => d.Agency).WithMany(p => p.Niches)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SocialMediaAgencyNiche_agencyId_fkey");
        });

        modelBuilder.Entity<Credits>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Credits_pkey");

            entity.ToTable("Credits");

            entity.HasIndex(e => e.OrganizationId, "Credits_organizationId_idx");

            entity.HasIndex(e => e.CreatedAt, "Credits_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Credits1).HasColumnName("credits");
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
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("ai_images")
                .HasColumnName("type");

            entity.HasOne(d => d.Organization).WithMany(p => p.Credits)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Credits_organizationId_fkey");
        });

        modelBuilder.Entity<Clipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Clipping_pkey");

            entity.ToTable("Clipping");

            entity.HasIndex(e => e.OrganizationId, "Clipping_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "Clipping_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("analysing")
                .HasColumnName("status");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MaxClips)
                .ValueGeneratedNever()
                .HasDefaultValue(5)
                .HasColumnName("maxClips");
            entity.Property(e => e.Fit)
                .ValueGeneratedNever()
                .HasDefaultValue("blur")
                .HasColumnName("fit");
            entity.Property(e => e.Integrations).HasColumnName("integrations");
            entity.Property(e => e.CreditsId).HasColumnName("creditsId");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Clippings)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Clipping_organizationId_fkey");
        });

        modelBuilder.Entity<ClippingClip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClippingClip_pkey");

            entity.ToTable("ClippingClip");

            entity.HasIndex(e => e.ClippingId, "ClippingClip_clippingId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClippingId).HasColumnName("clippingId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.End).HasColumnName("end");
            entity.Property(e => e.TrimStart).HasColumnName("trimStart");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.MediaId).HasColumnName("mediaId");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");
            entity.Property(e => e.DraftedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("draftedAt");
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

            entity.HasOne(d => d.Clipping).WithMany(p => p.Clips)
                .HasForeignKey(d => d.ClippingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ClippingClip_clippingId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.OrganizationId, "Subscription_organizationId_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "Subscription_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "Subscription_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.SubscriptionTier).HasColumnName("subscriptionTier");
            entity.Property(e => e.Provider)
                .ValueGeneratedNever()
                .HasDefaultValue("stripe")
                .HasColumnName("provider");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.CancelAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("cancelAt");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.TotalChannels).HasColumnName("totalChannels");
            entity.Property(e => e.IsLifetime)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isLifetime");
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

            entity.HasOne(d => d.Organization).WithOne(p => p.Subscription)
                .HasForeignKey<Subscription>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Subscription_organizationId_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.ToTable("Customer");

            entity.HasIndex(e => new { e.OrgId, e.Name, e.DeletedAt }, "Customer_orgId_name_deletedAt_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrgId).HasColumnName("orgId");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Customers)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Customer_orgId_fkey");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Integration_pkey");

            entity.ToTable("Integration");

            entity.HasIndex(e => e.RootInternalId, "Integration_rootInternalId_idx");

            entity.HasIndex(e => e.OrganizationId, "Integration_organizationId_idx");

            entity.HasIndex(e => e.ProviderIdentifier, "Integration_providerIdentifier_idx");

            entity.HasIndex(e => e.UpdatedAt, "Integration_updatedAt_idx");

            entity.HasIndex(e => e.CreatedAt, "Integration_createdAt_idx");

            entity.HasIndex(e => e.DeletedAt, "Integration_deletedAt_idx");

            entity.HasIndex(e => e.CustomerId, "Integration_customerId_idx");

            entity.HasIndex(e => e.InBetweenSteps, "Integration_inBetweenSteps_idx");

            entity.HasIndex(e => e.RefreshNeeded, "Integration_refreshNeeded_idx");

            entity.HasIndex(e => e.Disabled, "Integration_disabled_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.InternalId }, "Integration_organizationId_internalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.InternalId).HasColumnName("internalId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CustomName).HasColumnName("customName");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.ProviderIdentifier).HasColumnName("providerIdentifier");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Disabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.TokenExpiration)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("tokenExpiration");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.Profile).HasColumnName("profile");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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
            entity.Property(e => e.InBetweenSteps)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("inBetweenSteps");
            entity.Property(e => e.RefreshNeeded)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("refreshNeeded");
            entity.Property(e => e.PostingTimes)
                .ValueGeneratedNever()
                .HasDefaultValue("[{\"time\":120}, {\"time\":400}, {\"time\":700}]")
                .HasColumnName("postingTimes");
            entity.Property(e => e.CustomInstanceDetails).HasColumnName("customInstanceDetails");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.RootInternalId).HasColumnName("rootInternalId");
            entity.Property(e => e.AdditionalSettings)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnName("additionalSettings");

            entity.HasOne(d => d.Customer).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Integration_customerId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Integration)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Integration_organizationId_fkey");
        });

        modelBuilder.Entity<Signatures>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Signatures_pkey");

            entity.ToTable("Signatures");

            entity.HasIndex(e => e.CreatedAt, "Signatures_createdAt_idx");

            entity.HasIndex(e => e.OrganizationId, "Signatures_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "Signatures_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.AutoAdd).HasColumnName("autoAdd");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Signatures)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Signatures_organizationId_fkey");
        });

        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comments_pkey");

            entity.ToTable("Comments");

            entity.HasIndex(e => e.CreatedAt, "Comments_createdAt_idx");

            entity.HasIndex(e => e.OrganizationId, "Comments_organizationId_idx");

            entity.HasIndex(e => e.UserId, "Comments_userId_idx");

            entity.HasIndex(e => e.PostId, "Comments_postId_idx");

            entity.HasIndex(e => e.ParentId, "Comments_parentId_idx");

            entity.HasIndex(e => e.ResolvedAt, "Comments_resolvedAt_idx");

            entity.HasIndex(e => e.DeletedAt, "Comments_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.AnchorStart).HasColumnName("anchorStart");
            entity.Property(e => e.AnchorEnd).HasColumnName("anchorEnd");
            entity.Property(e => e.AnchorQuote).HasColumnName("anchorQuote");
            entity.Property(e => e.ResolvedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resolvedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Comments)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Comments_organizationId_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Comments_postId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Comments_userId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Replies)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Comments_parentId_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_pkey");

            entity.ToTable("Post");

            entity.HasIndex(e => e.Group, "Post_group_idx");

            entity.HasIndex(e => e.BatchId, "Post_batchId_idx");

            entity.HasIndex(e => e.DeletedAt, "Post_deletedAt_idx");

            entity.HasIndex(e => e.PublishDate, "Post_publishDate_idx");

            entity.HasIndex(e => e.State, "Post_state_idx");

            entity.HasIndex(e => e.OrganizationId, "Post_organizationId_idx");

            entity.HasIndex(e => e.ParentPostId, "Post_parentPostId_idx");

            entity.HasIndex(e => e.SubmittedForOrderId, "Post_submittedForOrderId_idx");

            entity.HasIndex(e => e.IntervalInDays, "Post_intervalInDays_idx");

            entity.HasIndex(e => e.ApprovedSubmitForOrder, "Post_approvedSubmitForOrder_idx");

            entity.HasIndex(e => e.CreationMethod, "Post_creationMethod_idx");

            entity.HasIndex(e => e.LastMessageId, "Post_lastMessageId_idx");

            entity.HasIndex(e => e.CreatedAt, "Post_createdAt_idx");

            entity.HasIndex(e => e.UpdatedAt, "Post_updatedAt_idx");

            entity.HasIndex(e => e.ReleaseURL, "Post_releaseURL_idx");

            entity.HasIndex(e => e.IntegrationId, "Post_integrationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(State.Queue)
                .HasColumnName("state");
            entity.Property(e => e.PublishDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("publishDate");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Delay)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("delay");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ParentPostId).HasColumnName("parentPostId");
            entity.Property(e => e.ReleaseId).HasColumnName("releaseId");
            entity.Property(e => e.ReleaseURL).HasColumnName("releaseURL");
            entity.Property(e => e.Settings).HasColumnName("settings");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.SubmittedForOrderId).HasColumnName("submittedForOrderId");
            entity.Property(e => e.SubmittedForOrganizationId).HasColumnName("submittedForOrganizationId");
            entity.Property(e => e.ApprovedSubmitForOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(ApprovedSubmitForOrder.No)
                .HasColumnName("approvedSubmitForOrder");
            entity.Property(e => e.CreationMethod)
                .ValueGeneratedNever()
                .HasDefaultValue(CreationMethod.Unknown)
                .HasColumnName("creationMethod");
            entity.Property(e => e.LastMessageId).HasColumnName("lastMessageId");
            entity.Property(e => e.IntervalInDays).HasColumnName("intervalInDays");
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
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");

            entity.HasOne(d => d.Integration).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Post_integrationId_fkey");

            entity.HasOne(d => d.LastMessage).WithMany(p => p.Posts)
                .HasForeignKey(d => d.LastMessageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Post_lastMessageId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Post)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Post_organizationId_fkey");

            entity.HasOne(d => d.ParentPost).WithMany(p => p.ChildrenPost)
                .HasForeignKey(d => d.ParentPostId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Post_parentPostId_fkey");

            entity.HasOne(d => d.SubmittedForOrder).WithMany(p => p.Posts)
                .HasForeignKey(d => d.SubmittedForOrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Post_submittedForOrderId_fkey");

            entity.HasOne(d => d.SubmittedForOrganization).WithMany(p => p.SubmittedPost)
                .HasForeignKey(d => d.SubmittedForOrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Post_submittedForOrganizationId_fkey");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notifications_pkey");

            entity.ToTable("Notifications");

            entity.HasIndex(e => e.CreatedAt, "Notifications_createdAt_idx");

            entity.HasIndex(e => e.OrganizationId, "Notifications_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "Notifications_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Link).HasColumnName("link");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Notifications_organizationId_fkey");
        });

        modelBuilder.Entity<MessagesGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MessagesGroup_pkey");

            entity.ToTable("MessagesGroup");

            entity.HasIndex(e => e.CreatedAt, "MessagesGroup_createdAt_idx");

            entity.HasIndex(e => e.UpdatedAt, "MessagesGroup_updatedAt_idx");

            entity.HasIndex(e => e.BuyerOrganizationId, "MessagesGroup_buyerOrganizationId_idx");

            entity.HasIndex(e => new { e.BuyerId, e.SellerId }, "MessagesGroup_buyerId_sellerId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BuyerOrganizationId).HasColumnName("buyerOrganizationId");
            entity.Property(e => e.BuyerId).HasColumnName("buyerId");
            entity.Property(e => e.SellerId).HasColumnName("sellerId");
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

            entity.HasOne(d => d.Buyer).WithMany(p => p.GroupBuyer)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("MessagesGroup_buyerId_fkey");

            entity.HasOne(d => d.BuyerOrganization).WithMany(p => p.BuyerOrganization)
                .HasForeignKey(d => d.BuyerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("MessagesGroup_buyerOrganizationId_fkey");

            entity.HasOne(d => d.Seller).WithMany(p => p.GroupSeller)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("MessagesGroup_sellerId_fkey");
        });

        modelBuilder.Entity<PayoutProblems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PayoutProblems_pkey");

            entity.ToTable("PayoutProblems");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.Amount).HasColumnName("amount");
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

            entity.HasOne(d => d.Order).WithMany(p => p.PayoutProblems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("PayoutProblems_orderId_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.PayoutProblems)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("PayoutProblems_postId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PayoutProblems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("PayoutProblems_userId_fkey");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.ToTable("Orders");

            entity.HasIndex(e => e.BuyerId, "Orders_buyerId_idx");

            entity.HasIndex(e => e.SellerId, "Orders_sellerId_idx");

            entity.HasIndex(e => e.UpdatedAt, "Orders_updatedAt_idx");

            entity.HasIndex(e => e.CreatedAt, "Orders_createdAt_idx");

            entity.HasIndex(e => e.MessageGroupId, "Orders_messageGroupId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BuyerId).HasColumnName("buyerId");
            entity.Property(e => e.SellerId).HasColumnName("sellerId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.MessageGroupId).HasColumnName("messageGroupId");
            entity.Property(e => e.CaptureId).HasColumnName("captureId");
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

            entity.HasOne(d => d.Buyer).WithMany(p => p.OrderBuyer)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Orders_buyerId_fkey");

            entity.HasOne(d => d.MessageGroup).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MessageGroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Orders_messageGroupId_fkey");

            entity.HasOne(d => d.Seller).WithMany(p => p.OrderSeller)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Orders_sellerId_fkey");
        });

        modelBuilder.Entity<OrderItems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderItems_pkey");

            entity.ToTable("OrderItems");

            entity.HasIndex(e => e.OrderId, "OrderItems_orderId_idx");

            entity.HasIndex(e => e.IntegrationId, "OrderItems_integrationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Integration).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrderItems_integrationId_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OrderItems_orderId_fkey");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Messages_pkey");

            entity.ToTable("Messages");

            entity.HasIndex(e => e.GroupId, "Messages_groupId_idx");

            entity.HasIndex(e => e.CreatedAt, "Messages_createdAt_idx");

            entity.HasIndex(e => e.DeletedAt, "Messages_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Special).HasColumnName("special");
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

            entity.HasOne(d => d.Group).WithMany(p => p.Messages)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Messages_groupId_fkey");
        });

        modelBuilder.Entity<Plugs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Plugs_pkey");

            entity.ToTable("Plugs");

            entity.HasIndex(e => e.OrganizationId, "Plugs_organizationId_idx");

            entity.HasIndex(e => new { e.PlugFunction, e.IntegrationId }, "Plugs_plugFunction_integrationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.PlugFunction).HasColumnName("plugFunction");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.Activated)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("activated");

            entity.HasOne(d => d.Integration).WithMany(p => p.Plugs)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Plugs_integrationId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Plugs)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Plugs_organizationId_fkey");
        });

        modelBuilder.Entity<ExisingPlugData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ExisingPlugData_pkey");

            entity.ToTable("ExisingPlugData");

            entity.HasIndex(e => new { e.IntegrationId, e.MethodName, e.Value }, "ExisingPlugData_integrationId_methodName_value_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.MethodName).HasColumnName("methodName");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Integration).WithMany(p => p.ExisingPlugData)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ExisingPlugData_integrationId_fkey");
        });

        modelBuilder.Entity<PopularPosts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PopularPosts_pkey");

            entity.ToTable("PopularPosts");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Topic).HasColumnName("topic");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Hook).HasColumnName("hook");
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

        modelBuilder.Entity<IntegrationsWebhooks>(entity =>
        {
            entity.HasKey(e => new { e.IntegrationId, e.WebhookId }).HasName("IntegrationsWebhooks_pkey");

            entity.ToTable("IntegrationsWebhooks");

            entity.HasIndex(e => e.IntegrationId, "IntegrationsWebhooks_integrationId_idx");

            entity.HasIndex(e => e.WebhookId, "IntegrationsWebhooks_webhookId_idx");

            entity.HasIndex(e => new { e.IntegrationId, e.WebhookId }, "IntegrationsWebhooks_integrationId_webhookId_key").IsUnique();

            entity.Property(e => e.IntegrationId).HasColumnName("integrationId");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");

            entity.HasOne(d => d.Integration).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("IntegrationsWebhooks_integrationId_fkey");

            entity.HasOne(d => d.Webhook).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.WebhookId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("IntegrationsWebhooks_webhookId_fkey");
        });

        modelBuilder.Entity<Webhooks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Webhooks_pkey");

            entity.ToTable("Webhooks");

            entity.HasIndex(e => e.OrganizationId, "Webhooks_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "Webhooks_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Webhooks_organizationId_fkey");
        });

        modelBuilder.Entity<AutoPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AutoPost_pkey");

            entity.ToTable("AutoPost");

            entity.HasIndex(e => e.DeletedAt, "AutoPost_deletedAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.OnSlot).HasColumnName("onSlot");
            entity.Property(e => e.SyncLast).HasColumnName("syncLast");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.LastUrl).HasColumnName("lastUrl");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AddPicture).HasColumnName("addPicture");
            entity.Property(e => e.GenerateContent).HasColumnName("generateContent");
            entity.Property(e => e.Integrations).HasColumnName("integrations");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deletedAt");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.AutoPost)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("AutoPost_organizationId_fkey");
        });

        modelBuilder.Entity<Sets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sets_pkey");

            entity.ToTable("Sets");

            entity.HasIndex(e => e.OrganizationId, "Sets_organizationId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Content).HasColumnName("content");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.Sets)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Sets_organizationId_fkey");
        });

        modelBuilder.Entity<ThirdParty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ThirdParty_pkey");

            entity.ToTable("ThirdParty");

            entity.HasIndex(e => e.OrganizationId, "ThirdParty_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "ThirdParty_deletedAt_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.InternalId }, "ThirdParty_organizationId_internalId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.InternalId).HasColumnName("internalId");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.ThirdParty)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ThirdParty_organizationId_fkey");
        });

        modelBuilder.Entity<Errors>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Errors_pkey");

            entity.ToTable("Errors");

            entity.HasIndex(e => e.OrganizationId, "Errors_organizationId_idx");

            entity.HasIndex(e => e.CreatedAt, "Errors_createdAt_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Platform).HasColumnName("platform");
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
            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.Body)
                .ValueGeneratedNever()
                .HasDefaultValue("{}")
                .HasColumnName("body");

            entity.HasOne(d => d.Organization).WithMany(p => p.Errors)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Errors_organizationId_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.Errors)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Errors_postId_fkey");
        });

        modelBuilder.Entity<Mentions>(entity =>
        {
            entity.HasKey(e => new { e.Name, e.Username, e.Platform, e.Image }).HasName("Mentions_pkey");

            entity.ToTable("Mentions");

            entity.HasIndex(e => e.CreatedAt, "Mentions_createdAt_idx");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Platform).HasColumnName("platform");
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
            entity.Property(e => e.Image).HasColumnName("image");
        });

        modelBuilder.Entity<MastraMessages>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_messages_pkey");

            entity.ToTable("mastra_messages");

            entity.HasIndex(e => new { e.ThreadId, e.CreatedAt }, "public_mastra_messages_thread_id_createdat_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.ThreadId, e.CreatedAt }, "mastra_messages_thread_id_createdat_idx").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraResources>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_resources_pkey");

            entity.ToTable("mastra_resources");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkingMemory).HasColumnName("workingMemory");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraScorers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_scorers_pkey");

            entity.ToTable("mastra_scorers");

            entity.HasIndex(e => new { e.TraceId, e.SpanId, e.CreatedAt }, "public_mastra_scores_trace_id_span_id_created_at_idx").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.TraceId, e.SpanId, e.CreatedAt }, "mastra_scores_trace_id_span_id_created_at_idx").IsDescending(false, false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ScorerId).HasColumnName("scorerId");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.RunId).HasColumnName("runId");
            entity.Property(e => e.Scorer)
                .HasColumnType("jsonb")
                .HasColumnName("scorer");
            entity.Property(e => e.PreprocessStepResult)
                .HasColumnType("jsonb")
                .HasColumnName("preprocessStepResult");
            entity.Property(e => e.ExtractStepResult)
                .HasColumnType("jsonb")
                .HasColumnName("extractStepResult");
            entity.Property(e => e.AnalyzeStepResult)
                .HasColumnType("jsonb")
                .HasColumnName("analyzeStepResult");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.PreprocessPrompt).HasColumnName("preprocessPrompt");
            entity.Property(e => e.ExtractPrompt).HasColumnName("extractPrompt");
            entity.Property(e => e.GenerateScorePrompt).HasColumnName("generateScorePrompt");
            entity.Property(e => e.GenerateReasonPrompt).HasColumnName("generateReasonPrompt");
            entity.Property(e => e.AnalyzePrompt).HasColumnName("analyzePrompt");
            entity.Property(e => e.ReasonPrompt).HasColumnName("reasonPrompt");
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.AdditionalContext)
                .HasColumnType("jsonb")
                .HasColumnName("additionalContext");
            entity.Property(e => e.RuntimeContext)
                .HasColumnType("jsonb")
                .HasColumnName("runtimeContext");
            entity.Property(e => e.EntityType).HasColumnName("entityType");
            entity.Property(e => e.Entity)
                .HasColumnType("jsonb")
                .HasColumnName("entity");
            entity.Property(e => e.EntityId).HasColumnName("entityId");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.SpanId).HasColumnName("spanId");
            entity.Property(e => e.RequestContext)
                .HasColumnType("jsonb")
                .HasColumnName("requestContext");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.DatasetItemId).HasColumnName("datasetItemId");
        });

        modelBuilder.Entity<MastraThreads>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_threads_pkey");

            entity.ToTable("mastra_threads");

            entity.HasIndex(e => new { e.ResourceId, e.CreatedAt }, "public_mastra_threads_resourceid_createdat_idx").IsDescending(false, true);

            entity.HasIndex(e => new { e.ResourceId, e.CreatedAt }, "mastra_threads_resourceid_createdat_idx").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Metadata).HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraTraces>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_traces_pkey");

            entity.ToTable("mastra_traces");

            entity.HasIndex(e => new { e.Name, e.StartTime }, "public_mastra_traces_name_starttime_idx").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ParentSpanId).HasColumnName("parentSpanId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Attributes)
                .HasColumnType("jsonb")
                .HasColumnName("attributes");
            entity.Property(e => e.Status)
                .HasColumnType("jsonb")
                .HasColumnName("status");
            entity.Property(e => e.Events)
                .HasColumnType("jsonb")
                .HasColumnName("events");
            entity.Property(e => e.Links)
                .HasColumnType("jsonb")
                .HasColumnName("links");
            entity.Property(e => e.Other).HasColumnName("other");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraWorkflowSnapshot>(entity =>
        {
            entity.HasKey(e => new { e.WorkflowName, e.RunId }).HasName("public_mastra_workflow_snapshot_workflow_name_run_id_key");

            entity.ToTable("mastra_workflow_snapshot");

            entity.HasIndex(e => new { e.WorkflowName, e.CreatedAt }, "mastra_workflow_snapshot_name_createdat_idx").IsDescending(false, true);

            entity.Property(e => e.WorkflowName).HasColumnName("workflow_name");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraAgentVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_agent_versions_pkey");

            entity.ToTable("mastra_agent_versions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Model)
                .HasColumnType("jsonb")
                .HasColumnName("model");
            entity.Property(e => e.Tools)
                .HasColumnType("jsonb")
                .HasColumnName("tools");
            entity.Property(e => e.DefaultOptions)
                .HasColumnType("jsonb")
                .HasColumnName("defaultOptions");
            entity.Property(e => e.Workflows)
                .HasColumnType("jsonb")
                .HasColumnName("workflows");
            entity.Property(e => e.Agents)
                .HasColumnType("jsonb")
                .HasColumnName("agents");
            entity.Property(e => e.IntegrationTools)
                .HasColumnType("jsonb")
                .HasColumnName("integrationTools");
            entity.Property(e => e.InputProcessors)
                .HasColumnType("jsonb")
                .HasColumnName("inputProcessors");
            entity.Property(e => e.OutputProcessors)
                .HasColumnType("jsonb")
                .HasColumnName("outputProcessors");
            entity.Property(e => e.Memory)
                .HasColumnType("jsonb")
                .HasColumnName("memory");
            entity.Property(e => e.Scorers)
                .HasColumnType("jsonb")
                .HasColumnName("scorers");
            entity.Property(e => e.McpClients)
                .HasColumnType("jsonb")
                .HasColumnName("mcpClients");
            entity.Property(e => e.RequestContextSchema)
                .HasColumnType("jsonb")
                .HasColumnName("requestContextSchema");
            entity.Property(e => e.Workspace)
                .HasColumnType("jsonb")
                .HasColumnName("workspace");
            entity.Property(e => e.Skills)
                .HasColumnType("jsonb")
                .HasColumnName("skills");
            entity.Property(e => e.SkillsFormat).HasColumnName("skillsFormat");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.Durable)
                .HasColumnType("jsonb")
                .HasColumnName("durable");
            entity.Property(e => e.Browser)
                .HasColumnType("jsonb")
                .HasColumnName("browser");
            entity.Property(e => e.ToolProviders)
                .HasColumnType("jsonb")
                .HasColumnName("toolProviders");
        });

        modelBuilder.Entity<MastraAgents>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_agents_pkey");

            entity.ToTable("mastra_agents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.Visibility).HasColumnName("visibility");
            entity.Property(e => e.FavoriteCount).HasColumnName("favoriteCount");
        });

        modelBuilder.Entity<MastraBackgroundTasks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_background_tasks_pkey");

            entity.ToTable("mastra_background_tasks");

            entity.HasIndex(e => new { e.AgentId, e.Status }, "mastra_bg_tasks_agent_status_idx");

            entity.HasIndex(e => new { e.Status, e.CreatedAt }, "mastra_bg_tasks_status_created_at_idx");

            entity.HasIndex(e => new { e.ThreadId, e.CreatedAt }, "mastra_bg_tasks_thread_idx");

            entity.HasIndex(e => e.ToolCallId, "mastra_bg_tasks_tool_call_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ToolCallId).HasColumnName("tool_call_id");
            entity.Property(e => e.ToolName).HasColumnName("tool_name");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Args)
                .HasColumnType("jsonb")
                .HasColumnName("args");
            entity.Property(e => e.Result)
                .HasColumnType("jsonb")
                .HasColumnName("result");
            entity.Property(e => e.Error)
                .HasColumnType("jsonb")
                .HasColumnName("error");
            entity.Property(e => e.SuspendPayload)
                .HasColumnType("jsonb")
                .HasColumnName("suspend_payload");
            entity.Property(e => e.RetryCount).HasColumnName("retry_count");
            entity.Property(e => e.MaxRetries).HasColumnName("max_retries");
            entity.Property(e => e.TimeoutMs).HasColumnName("timeout_ms");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.SuspendedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("suspendedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.StartedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("startedAtZ");
            entity.Property(e => e.SuspendedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("suspendedAtZ");
            entity.Property(e => e.CompletedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("completedAtZ");
        });

        modelBuilder.Entity<MastraChannelConfig>(entity =>
        {
            entity.HasKey(e => e.Platform).HasName("mastra_channel_config_pkey");

            entity.ToTable("mastra_channel_config");

            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraChannelInstallations>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_channel_installations_pkey");

            entity.ToTable("mastra_channel_installations");

            entity.HasIndex(e => e.WebhookId, "idx_channel_installations_webhook").IsUnique();

            entity.HasIndex(e => new { e.Platform, e.AgentId }, "idx_channel_installations_platform_agent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.AgentId).HasColumnName("agentId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WebhookId).HasColumnName("webhookId");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.ConfigHash).HasColumnName("configHash");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraDatasetItems>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.DatasetVersion }).HasName("mastra_dataset_items_pkey");

            entity.ToTable("mastra_dataset_items");

            entity.HasIndex(e => new { e.DatasetId, e.ValidTo }, "idx_dataset_items_dataset_validto");

            entity.HasIndex(e => new { e.DatasetId, e.ValidTo, e.IsDeleted }, "idx_dataset_items_dataset_validto_deleted");

            entity.HasIndex(e => new { e.DatasetId, e.DatasetVersion }, "idx_dataset_items_dataset_version");

            entity.HasIndex(e => new { e.DatasetId, e.ExternalId, e.DatasetVersion }, "idx_dataset_items_external_id_history");

            entity.HasIndex(e => new { e.OrganizationId, e.ProjectId }, "idx_dataset_items_org_project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.DatasetVersion).HasColumnName("datasetVersion");
            entity.Property(e => e.ValidTo).HasColumnName("validTo");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.GroundTruth)
                .HasColumnType("jsonb")
                .HasColumnName("groundTruth");
            entity.Property(e => e.RequestContext)
                .HasColumnType("jsonb")
                .HasColumnName("requestContext");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Source)
                .HasColumnType("jsonb")
                .HasColumnName("source");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.ExpectedTrajectory)
                .HasColumnType("jsonb")
                .HasColumnName("expectedTrajectory");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.ToolMocks)
                .HasColumnType("jsonb")
                .HasColumnName("toolMocks");
            entity.Property(e => e.UnmockedToolPolicy).HasColumnName("unmockedToolPolicy");
            entity.Property(e => e.ScorerIds)
                .HasColumnType("jsonb")
                .HasColumnName("scorerIds");
            entity.Property(e => e.ExternalId).HasColumnName("externalId");
        });

        modelBuilder.Entity<MastraDatasetVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_dataset_versions_pkey");

            entity.ToTable("mastra_dataset_versions");

            entity.HasIndex(e => new { e.DatasetId, e.Version }, "idx_dataset_versions_dataset_version");

            entity.HasIndex(e => new { e.DatasetId, e.Version }, "idx_dataset_versions_dataset_version_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraDatasets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_datasets_pkey");

            entity.ToTable("mastra_datasets");

            entity.HasIndex(e => new { e.CandidateKey, e.CandidateId }, "idx_datasets_candidate");

            entity.HasIndex(e => new { e.OrganizationId, e.ProjectId }, "idx_datasets_org_project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.InputSchema)
                .HasColumnType("jsonb")
                .HasColumnName("inputSchema");
            entity.Property(e => e.GroundTruthSchema)
                .HasColumnType("jsonb")
                .HasColumnName("groundTruthSchema");
            entity.Property(e => e.RequestContextSchema)
                .HasColumnType("jsonb")
                .HasColumnName("requestContextSchema");
            entity.Property(e => e.Tags)
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.TargetIds)
                .HasColumnType("jsonb")
                .HasColumnName("targetIds");
            entity.Property(e => e.ScorerIds)
                .HasColumnType("jsonb")
                .HasColumnName("scorerIds");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.CandidateKey).HasColumnName("candidateKey");
            entity.Property(e => e.CandidateId).HasColumnName("candidateId");
        });

        modelBuilder.Entity<MastraExperimentResults>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_experiment_results_pkey");

            entity.ToTable("mastra_experiment_results");

            entity.HasIndex(e => e.ExperimentId, "idx_experiment_results_experimentid");

            entity.HasIndex(e => new { e.OrganizationId, e.ProjectId }, "idx_experiment_results_org_project");

            entity.HasIndex(e => e.Tags, "idx_experiment_results_tags_gin").HasMethod("gin");

            entity.HasIndex(e => new { e.ExperimentId, e.ItemId, e.Attempt }, "idx_experiment_results_exp_item_attempt").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExperimentId).HasColumnName("experimentId");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.ItemDatasetVersion).HasColumnName("itemDatasetVersion");
            entity.Property(e => e.Input)
                .HasColumnType("jsonb")
                .HasColumnName("input");
            entity.Property(e => e.Output)
                .HasColumnType("jsonb")
                .HasColumnName("output");
            entity.Property(e => e.GroundTruth)
                .HasColumnType("jsonb")
                .HasColumnName("groundTruth");
            entity.Property(e => e.Error)
                .HasColumnType("jsonb")
                .HasColumnName("error");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.RetryCount).HasColumnName("retryCount");
            entity.Property(e => e.TraceId).HasColumnName("traceId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Tags)
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.StartedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("startedAtZ");
            entity.Property(e => e.CompletedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("completedAtZ");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ToolMockReport)
                .HasColumnType("jsonb")
                .HasColumnName("toolMockReport");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Attempt).HasColumnName("attempt");
        });

        modelBuilder.Entity<MastraExperiments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_experiments_pkey");

            entity.ToTable("mastra_experiments");

            entity.HasIndex(e => e.DatasetId, "idx_experiments_datasetid");

            entity.HasIndex(e => new { e.ExperimentSetId, e.ComparisonId, e.VariantId, e.TrialIndex }, "idx_experiments_grouping");

            entity.HasIndex(e => new { e.OrganizationId, e.ProjectId }, "idx_experiments_org_project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.DatasetId).HasColumnName("datasetId");
            entity.Property(e => e.DatasetVersion).HasColumnName("datasetVersion");
            entity.Property(e => e.TargetType).HasColumnName("targetType");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalItems).HasColumnName("totalItems");
            entity.Property(e => e.SucceededCount).HasColumnName("succeededCount");
            entity.Property(e => e.FailedCount).HasColumnName("failedCount");
            entity.Property(e => e.SkippedCount).HasColumnName("skippedCount");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("startedAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.AgentVersion).HasColumnName("agentVersion");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.StartedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("startedAtZ");
            entity.Property(e => e.CompletedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("completedAtZ");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Provenance)
                .HasColumnType("jsonb")
                .HasColumnName("provenance");
            entity.Property(e => e.RunnerAttestation)
                .HasColumnType("jsonb")
                .HasColumnName("runnerAttestation");
            entity.Property(e => e.ExperimentSetId).HasColumnName("experimentSetId");
            entity.Property(e => e.ComparisonId).HasColumnName("comparisonId");
            entity.Property(e => e.VariantId).HasColumnName("variantId");
            entity.Property(e => e.TrialIndex).HasColumnName("trialIndex");
            entity.Property(e => e.ScorerIds)
                .HasColumnType("jsonb")
                .HasColumnName("scorerIds");
        });

        modelBuilder.Entity<MastraFavorites>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EntityType, e.EntityId }).HasName("mastra_favorites_pkey");

            entity.ToTable("mastra_favorites");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "idx_favorites_entity");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.EntityType).HasColumnName("entityType");
            entity.Property(e => e.EntityId).HasColumnName("entityId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraKnowledgeActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_knowledge_activity_pkey");

            entity.ToTable("mastra_knowledge_activity");

            entity.HasIndex(e => e.Id, "idx_knowledge_activity_latest").IsDescending(true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.RecordType).HasColumnName("recordType");
            entity.Property(e => e.RecordId).HasColumnName("recordId");
            entity.Property(e => e.Scope)
                .HasColumnType("jsonb")
                .HasColumnName("scope");
            entity.Property(e => e.ScopeKey).HasColumnName("scopeKey");
            entity.Property(e => e.SourceThreadId).HasColumnName("sourceThreadId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraKnowledgeCursors>(entity =>
        {
            entity.HasKey(e => new { e.SourceThreadId, e.Agent }).HasName("mastra_knowledge_cursors_pkey");

            entity.ToTable("mastra_knowledge_cursors");

            entity.Property(e => e.SourceThreadId).HasColumnName("sourceThreadId");
            entity.Property(e => e.Agent).HasColumnName("agent");
            entity.Property(e => e.LastKnowledgeId).HasColumnName("lastKnowledgeId");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraKnowledgeMentions>(entity =>
        {
            entity.HasKey(e => new { e.SourceType, e.SourceId, e.RecordId }).HasName("mastra_knowledge_mentions_pkey");

            entity.ToTable("mastra_knowledge_mentions");

            entity.HasIndex(e => new { e.RecordId, e.SourceType, e.SourceId }, "idx_knowledge_mentions_record");

            entity.Property(e => e.SourceType).HasColumnName("sourceType");
            entity.Property(e => e.SourceId).HasColumnName("sourceId");
            entity.Property(e => e.RecordId).HasColumnName("recordId");
        });

        modelBuilder.Entity<MastraKnowledgeNodes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_knowledge_nodes_pkey");

            entity.ToTable("mastra_knowledge_nodes");

            entity.HasIndex(e => new { e.ScopeKey, e.Type }, "idx_knowledge_nodes_scope");

            entity.HasIndex(e => new { e.Type, e.ScopeKey, e.CanonicalName }, "idx_knowledge_nodes_identity").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CanonicalName).HasColumnName("canonicalName");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Scope)
                .HasColumnType("jsonb")
                .HasColumnName("scope");
            entity.Property(e => e.ScopeKey).HasColumnName("scopeKey");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.MergedInto).HasColumnName("mergedInto");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraKnowledgeRecords>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_knowledge_records_pkey");

            entity.ToTable("mastra_knowledge_records");

            entity.HasIndex(e => new { e.Node, e.Id }, "idx_knowledge_records_node_latest").IsDescending(false, true);

            entity.HasIndex(e => new { e.SourceThreadId, e.Id }, "idx_knowledge_records_thread_latest").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Node).HasColumnName("node");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Scope)
                .HasColumnType("jsonb")
                .HasColumnName("scope");
            entity.Property(e => e.ScopeKey).HasColumnName("scopeKey");
            entity.Property(e => e.SourceThreadId).HasColumnName("sourceThreadId");
            entity.Property(e => e.CapturedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("capturedAt");
            entity.Property(e => e.When)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("when");
            entity.Property(e => e.MaxScope).HasColumnName("maxScope");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.DeletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("deletedAt");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");
            entity.Property(e => e.CapturedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("capturedAtZ");
            entity.Property(e => e.WhenZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("whenZ");
            entity.Property(e => e.DeletedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("deletedAtZ");
        });

        modelBuilder.Entity<MastraKnowledgeSemanticOutbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_knowledge_semantic_outbox_pkey");

            entity.ToTable("mastra_knowledge_semantic_outbox");

            entity.HasIndex(e => e.IdempotencyKey, "idx_knowledge_outbox_idempotency").IsUnique();

            entity.HasIndex(e => new { e.Status, e.AvailableAt, e.CreatedAt }, "idx_knowledge_outbox_claim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotencyKey");
            entity.Property(e => e.DocumentId).HasColumnName("documentId");
            entity.Property(e => e.DocumentType).HasColumnName("documentType");
            entity.Property(e => e.Operation).HasColumnName("operation");
            entity.Property(e => e.Scope)
                .HasColumnType("jsonb")
                .HasColumnName("scope");
            entity.Property(e => e.ScopeKey).HasColumnName("scopeKey");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Attempts).HasColumnName("attempts");
            entity.Property(e => e.AvailableAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("availableAt");
            entity.Property(e => e.ClaimedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("claimedAt");
            entity.Property(e => e.ClaimedBy).HasColumnName("claimedBy");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("completedAt");
            entity.Property(e => e.AvailableAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("availableAtZ");
            entity.Property(e => e.ClaimedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("claimedAtZ");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.CompletedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("completedAtZ");
        });

        modelBuilder.Entity<MastraMcpClientVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_mcp_client_versions_pkey");

            entity.ToTable("mastra_mcp_client_versions");

            entity.HasIndex(e => new { e.McpClientId, e.VersionNumber }, "idx_mcp_client_versions_client_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.McpClientId).HasColumnName("mcpClientId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Servers)
                .HasColumnType("jsonb")
                .HasColumnName("servers");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraMcpClients>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_mcp_clients_pkey");

            entity.ToTable("mastra_mcp_clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraMcpServerVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_mcp_server_versions_pkey");

            entity.ToTable("mastra_mcp_server_versions");

            entity.HasIndex(e => new { e.McpServerId, e.VersionNumber }, "idx_mcp_server_versions_server_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.McpServerId).HasColumnName("mcpServerId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Repository)
                .HasColumnType("jsonb")
                .HasColumnName("repository");
            entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");
            entity.Property(e => e.IsLatest).HasColumnName("isLatest");
            entity.Property(e => e.PackageCanonical).HasColumnName("packageCanonical");
            entity.Property(e => e.Tools)
                .HasColumnType("jsonb")
                .HasColumnName("tools");
            entity.Property(e => e.Agents)
                .HasColumnType("jsonb")
                .HasColumnName("agents");
            entity.Property(e => e.Workflows)
                .HasColumnType("jsonb")
                .HasColumnName("workflows");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraMcpServers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_mcp_servers_pkey");

            entity.ToTable("mastra_mcp_servers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraObservationalMemory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_observational_memory_pkey");

            entity.ToTable("mastra_observational_memory");

            entity.HasIndex(e => e.LookupKey, "idx_om_lookup_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LookupKey).HasColumnName("lookupKey");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.ActiveObservations).HasColumnName("activeObservations");
            entity.Property(e => e.ActiveObservationsPendingUpdate).HasColumnName("activeObservationsPendingUpdate");
            entity.Property(e => e.OriginType).HasColumnName("originType");
            entity.Property(e => e.Config).HasColumnName("config");
            entity.Property(e => e.GenerationCount).HasColumnName("generationCount");
            entity.Property(e => e.LastObservedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("lastObservedAt");
            entity.Property(e => e.LastReflectionAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("lastReflectionAt");
            entity.Property(e => e.PendingMessageTokens).HasColumnName("pendingMessageTokens");
            entity.Property(e => e.TotalTokensObserved).HasColumnName("totalTokensObserved");
            entity.Property(e => e.ObservationTokenCount).HasColumnName("observationTokenCount");
            entity.Property(e => e.IsObserving).HasColumnName("isObserving");
            entity.Property(e => e.IsReflecting).HasColumnName("isReflecting");
            entity.Property(e => e.ObservedMessageIds)
                .HasColumnType("jsonb")
                .HasColumnName("observedMessageIds");
            entity.Property(e => e.ObservedTimezone).HasColumnName("observedTimezone");
            entity.Property(e => e.BufferedObservations).HasColumnName("bufferedObservations");
            entity.Property(e => e.BufferedObservationTokens).HasColumnName("bufferedObservationTokens");
            entity.Property(e => e.BufferedMessageIds)
                .HasColumnType("jsonb")
                .HasColumnName("bufferedMessageIds");
            entity.Property(e => e.BufferedReflection).HasColumnName("bufferedReflection");
            entity.Property(e => e.BufferedReflectionTokens).HasColumnName("bufferedReflectionTokens");
            entity.Property(e => e.BufferedReflectionInputTokens).HasColumnName("bufferedReflectionInputTokens");
            entity.Property(e => e.ReflectedObservationLineCount).HasColumnName("reflectedObservationLineCount");
            entity.Property(e => e.BufferedObservationChunks)
                .HasColumnType("jsonb")
                .HasColumnName("bufferedObservationChunks");
            entity.Property(e => e.IsBufferingObservation).HasColumnName("isBufferingObservation");
            entity.Property(e => e.IsBufferingReflection).HasColumnName("isBufferingReflection");
            entity.Property(e => e.LastBufferedAtTokens).HasColumnName("lastBufferedAtTokens");
            entity.Property(e => e.LastBufferedAtTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("lastBufferedAtTime");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.LastObservedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("lastObservedAtZ");
            entity.Property(e => e.LastReflectionAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("lastReflectionAtZ");
            entity.Property(e => e.LastBufferedAtTimeZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("lastBufferedAtTimeZ");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraPromptBlockVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_prompt_block_versions_pkey");

            entity.ToTable("mastra_prompt_block_versions");

            entity.HasIndex(e => new { e.BlockId, e.VersionNumber }, "idx_prompt_block_versions_block_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlockId).HasColumnName("blockId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Rules)
                .HasColumnType("jsonb")
                .HasColumnName("rules");
            entity.Property(e => e.RequestContextSchema)
                .HasColumnType("jsonb")
                .HasColumnName("requestContextSchema");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraPromptBlocks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_prompt_blocks_pkey");

            entity.ToTable("mastra_prompt_blocks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraScheduleTriggers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_schedule_triggers_pkey");

            entity.ToTable("mastra_schedule_triggers");

            entity.HasIndex(e => new { e.ScheduleId, e.ActualFireAt }, "idx_mastra_schedule_triggers_schedule_fire").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.ScheduledFireAt).HasColumnName("scheduled_fire_at");
            entity.Property(e => e.ActualFireAt).HasColumnName("actual_fire_at");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.Error).HasColumnName("error");
            entity.Property(e => e.TriggerKind).HasColumnName("trigger_kind");
            entity.Property(e => e.ParentTriggerId).HasColumnName("parent_trigger_id");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });

        modelBuilder.Entity<MastraSchedules>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_schedules_pkey");

            entity.ToTable("mastra_schedules");

            entity.HasIndex(e => new { e.Status, e.NextFireAt }, "idx_mastra_schedules_status_next_fire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Target)
                .HasColumnType("jsonb")
                .HasColumnName("target");
            entity.Property(e => e.Cron).HasColumnName("cron");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.NextFireAt).HasColumnName("next_fire_at");
            entity.Property(e => e.LastFireAt).HasColumnName("last_fire_at");
            entity.Property(e => e.LastRunId).HasColumnName("last_run_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.OwnerType).HasColumnName("owner_type");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
        });

        modelBuilder.Entity<MastraScorerDefinitionVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_scorer_definition_versions_pkey");

            entity.ToTable("mastra_scorer_definition_versions");

            entity.HasIndex(e => new { e.ScorerDefinitionId, e.VersionNumber }, "idx_scorer_definition_versions_def_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ScorerDefinitionId).HasColumnName("scorerDefinitionId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Model)
                .HasColumnType("jsonb")
                .HasColumnName("model");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.ScoreRange)
                .HasColumnType("jsonb")
                .HasColumnName("scoreRange");
            entity.Property(e => e.PresetConfig)
                .HasColumnType("jsonb")
                .HasColumnName("presetConfig");
            entity.Property(e => e.DefaultSampling)
                .HasColumnType("jsonb")
                .HasColumnName("defaultSampling");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraScorerDefinitions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_scorer_definitions_pkey");

            entity.ToTable("mastra_scorer_definitions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
        });

        modelBuilder.Entity<MastraSkillBlobs>(entity =>
        {
            entity.HasKey(e => e.Hash).HasName("mastra_skill_blobs_pkey");

            entity.ToTable("mastra_skill_blobs");

            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.MimeType).HasColumnName("mimeType");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraSkillVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_skill_versions_pkey");

            entity.ToTable("mastra_skill_versions");

            entity.HasIndex(e => new { e.SkillId, e.VersionNumber }, "idx_skill_versions_skill_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SkillId).HasColumnName("skillId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.License).HasColumnName("license");
            entity.Property(e => e.Compatibility)
                .HasColumnType("jsonb")
                .HasColumnName("compatibility");
            entity.Property(e => e.Source)
                .HasColumnType("jsonb")
                .HasColumnName("source");
            entity.Property(e => e.References)
                .HasColumnType("jsonb")
                .HasColumnName("references");
            entity.Property(e => e.Scripts)
                .HasColumnType("jsonb")
                .HasColumnName("scripts");
            entity.Property(e => e.Assets)
                .HasColumnType("jsonb")
                .HasColumnName("assets");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Tree)
                .HasColumnType("jsonb")
                .HasColumnName("tree");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.Files)
                .HasColumnType("jsonb")
                .HasColumnName("files");
        });

        modelBuilder.Entity<MastraSkills>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_skills_pkey");

            entity.ToTable("mastra_skills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
            entity.Property(e => e.Visibility).HasColumnName("visibility");
            entity.Property(e => e.FavoriteCount).HasColumnName("favoriteCount");
        });

        modelBuilder.Entity<MastraThreadState>(entity =>
        {
            entity.HasKey(e => new { e.ThreadId, e.Type }).HasName("mastra_thread_state_pkey");

            entity.ToTable("mastra_thread_state");

            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Value)
                .HasColumnType("jsonb")
                .HasColumnName("value");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraToolProviderConnections>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.ProviderId, e.ConnectionId }).HasName("mastra_tool_provider_connections_pkey");

            entity.ToTable("mastra_tool_provider_connections");

            entity.HasIndex(e => new { e.AuthorId, e.ProviderId, e.Toolkit }, "idx_tool_provider_connections_author");

            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.ConnectionId).HasColumnName("connectionId");
            entity.Property(e => e.Toolkit).HasColumnName("toolkit");
            entity.Property(e => e.Label).HasColumnName("label");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraWorkflowDefinitions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_workflow_definitions_pkey");

            entity.ToTable("mastra_workflow_definitions");

            entity.HasIndex(e => e.Status, "idx_workflow_definitions_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.InputSchema)
                .HasColumnType("jsonb")
                .HasColumnName("inputSchema");
            entity.Property(e => e.OutputSchema)
                .HasColumnType("jsonb")
                .HasColumnName("outputSchema");
            entity.Property(e => e.StateSchema)
                .HasColumnType("jsonb")
                .HasColumnName("stateSchema");
            entity.Property(e => e.RequestContextSchema)
                .HasColumnType("jsonb")
                .HasColumnName("requestContextSchema");
            entity.Property(e => e.Graph)
                .HasColumnType("jsonb")
                .HasColumnName("graph");
            entity.Property(e => e.Schedule)
                .HasColumnType("jsonb")
                .HasColumnName("schedule");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<MastraWorkspaceVersions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_workspace_versions_pkey");

            entity.ToTable("mastra_workspace_versions");

            entity.HasIndex(e => new { e.WorkspaceId, e.VersionNumber }, "idx_workspace_versions_workspace_version").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.WorkspaceId).HasColumnName("workspaceId");
            entity.Property(e => e.VersionNumber).HasColumnName("versionNumber");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Filesystem)
                .HasColumnType("jsonb")
                .HasColumnName("filesystem");
            entity.Property(e => e.Sandbox)
                .HasColumnType("jsonb")
                .HasColumnName("sandbox");
            entity.Property(e => e.Mounts)
                .HasColumnType("jsonb")
                .HasColumnName("mounts");
            entity.Property(e => e.Search)
                .HasColumnType("jsonb")
                .HasColumnName("search");
            entity.Property(e => e.Skills)
                .HasColumnType("jsonb")
                .HasColumnName("skills");
            entity.Property(e => e.Tools)
                .HasColumnType("jsonb")
                .HasColumnName("tools");
            entity.Property(e => e.AutoSync).HasColumnName("autoSync");
            entity.Property(e => e.OperationTimeout).HasColumnName("operationTimeout");
            entity.Property(e => e.ChangedFields)
                .HasColumnType("jsonb")
                .HasColumnName("changedFields");
            entity.Property(e => e.ChangeMessage).HasColumnName("changeMessage");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
        });

        modelBuilder.Entity<MastraWorkspaces>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mastra_workspaces_pkey");

            entity.ToTable("mastra_workspaces");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveVersionId).HasColumnName("activeVersionId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.CreatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAtZ");
            entity.Property(e => e.UpdatedAtZ)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAtZ");
        });

        modelBuilder.Entity<OAuthApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthApp_pkey");

            entity.ToTable("OAuthApp");

            entity.HasIndex(e => e.ClientId, "OAuthApp_clientId_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "OAuthApp_clientId_idx");

            entity.HasIndex(e => e.OrganizationId, "OAuthApp_organizationId_idx");

            entity.HasIndex(e => e.DeletedAt, "OAuthApp_deletedAt_idx");

            entity.HasIndex(e => e.Dynamic, "OAuthApp_dynamic_idx");

            entity.HasIndex(e => new { e.OrganizationId, e.DeletedAt }, "OAuthApp_organizationId_deletedAt_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PictureId).HasColumnName("pictureId");
            entity.Property(e => e.RedirectUrl).HasColumnName("redirectUrl");
            entity.Property(e => e.RedirectUris).HasColumnName("redirectUris");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientSecret).HasColumnName("clientSecret");
            entity.Property(e => e.Dynamic)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dynamic");
            entity.Property(e => e.TokenEndpointAuthMethod).HasColumnName("tokenEndpointAuthMethod");
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

            entity.HasOne(d => d.Organization).WithMany(p => p.OauthApp)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OAuthApp_organizationId_fkey");

            entity.HasOne(d => d.Picture).WithMany(p => p.OauthApps)
                .HasForeignKey(d => d.PictureId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("OAuthApp_pictureId_fkey");
        });

        modelBuilder.Entity<OAuthAuthorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthAuthorization_pkey");

            entity.ToTable("OAuthAuthorization");

            entity.HasIndex(e => e.AccessToken, "OAuthAuthorization_accessToken_idx");

            entity.HasIndex(e => e.AuthorizationCode, "OAuthAuthorization_authorizationCode_idx");

            entity.HasIndex(e => e.OauthAppId, "OAuthAuthorization_oauthAppId_idx");

            entity.HasIndex(e => e.UserId, "OAuthAuthorization_userId_idx");

            entity.HasIndex(e => e.OrganizationId, "OAuthAuthorization_organizationId_idx");

            entity.HasIndex(e => e.RevokedAt, "OAuthAuthorization_revokedAt_idx");

            entity.HasIndex(e => new { e.OauthAppId, e.UserId, e.OrganizationId }, "OAuthAuthorization_oauthAppId_userId_organizationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OauthAppId).HasColumnName("oauthAppId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.AuthorizationCode).HasColumnName("authorizationCode");
            entity.Property(e => e.CodeExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("codeExpiresAt");
            entity.Property(e => e.CodeChallenge).HasColumnName("codeChallenge");
            entity.Property(e => e.CodeChallengeMethod).HasColumnName("codeChallengeMethod");
            entity.Property(e => e.RedirectUri).HasColumnName("redirectUri");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
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

            entity.HasOne(d => d.OauthApp).WithMany(p => p.Authorizations)
                .HasForeignKey(d => d.OauthAppId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OAuthAuthorization_oauthAppId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthAuthorizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OAuthAuthorization_userId_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.OauthAuthorizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OAuthAuthorization_organizationId_fkey");
        });

        modelBuilder.Entity<OAuthSelfHostedAuthorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OAuthSelfHostedAuthorization_pkey");

            entity.ToTable("OAuthSelfHostedAuthorization");

            entity.HasIndex(e => e.AccessToken, "OAuthSelfHostedAuthorization_accessToken_idx");

            entity.HasIndex(e => e.AuthorizationCode, "OAuthSelfHostedAuthorization_authorizationCode_idx");

            entity.HasIndex(e => e.OauthAppId, "OAuthSelfHostedAuthorization_oauthAppId_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.OauthAppId).HasColumnName("oauthAppId");
            entity.Property(e => e.McpUrl).HasColumnName("mcpUrl");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.AuthorizationCode).HasColumnName("authorizationCode");
            entity.Property(e => e.CodeExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("codeExpiresAt");
            entity.Property(e => e.CodeChallenge).HasColumnName("codeChallenge");
            entity.Property(e => e.CodeChallengeMethod).HasColumnName("codeChallengeMethod");
            entity.Property(e => e.RedirectUri).HasColumnName("redirectUri");
            entity.Property(e => e.RevokedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("revokedAt");
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

            entity.HasOne(d => d.OauthApp).WithMany(p => p.SelfHostedAuthorizations)
                .HasForeignKey(d => d.OauthAppId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("OAuthSelfHostedAuthorization_oauthAppId_fkey");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Announcement_pkey");

            entity.ToTable("Announcement");

            entity.Property(e => e.Id)
                .HasValueGenerator<UuidV4StringGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Color)
                .ValueGeneratedNever()
                .HasDefaultValue(AnnouncementColor.Info)
                .HasColumnName("color");
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
        foreach (var entry in ChangeTracker.Entries<Organization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Tags>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TagsPosts>())
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

        foreach (var entry in ChangeTracker.Entries<UsedCodes>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserOrganization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<GitHub>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Trending>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Star>())
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

        foreach (var entry in ChangeTracker.Entries<SocialMediaAgency>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Credits>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Clipping>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ClippingClip>())
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

        foreach (var entry in ChangeTracker.Entries<Customer>())
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

        foreach (var entry in ChangeTracker.Entries<Signatures>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Comments>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Post>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Notifications>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<MessagesGroup>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PayoutProblems>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Orders>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Messages>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PopularPosts>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Webhooks>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AutoPost>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Sets>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ThirdParty>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Errors>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Mentions>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthApp>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthAuthorization>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthSelfHostedAuthorization>())
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

file sealed class UtcTodayGenerator : ValueGenerator<DateOnly>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateOnly Next(EntityEntry entry) => DateOnly.FromDateTime(DateTime.UtcNow);
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
