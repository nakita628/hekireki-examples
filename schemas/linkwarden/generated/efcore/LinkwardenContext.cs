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

namespace Linkwarden.Models;

public partial class LinkwardenContext : DbContext
{
    public LinkwardenContext(DbContextOptions<LinkwardenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<UsersAndCollections> UsersAndCollections { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<RssSubscription> RssSubscriptions { get; set; }

    public virtual DbSet<Highlight> Highlights { get; set; }

    public virtual DbSet<DashboardSection> DashboardSections { get; set; }

    public virtual DbSet<AppMigration> AppMigrations { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<Theme>("Theme");
        npgsql.MapEnum<AiTaggingMethod>("AiTaggingMethod");
        npgsql.MapEnum<LinksRouteTo>("LinksRouteTo");
        npgsql.MapEnum<SubscriptionProvider>("SubscriptionProvider");
        npgsql.MapEnum<DashboardSectionType>("DashboardSectionType");
        npgsql.MapEnum<AppMigrationStatus>("AppMigrationStatus");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<Theme>("Theme");
        dataSource.MapEnum<AiTaggingMethod>("AiTaggingMethod");
        dataSource.MapEnum<LinksRouteTo>("LinksRouteTo");
        dataSource.MapEnum<SubscriptionProvider>("SubscriptionProvider");
        dataSource.MapEnum<DashboardSectionType>("DashboardSectionType");
        dataSource.MapEnum<AppMigrationStatus>("AppMigrationStatus");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Theme>(name: "Theme");
        modelBuilder.HasPostgresEnum<AiTaggingMethod>(name: "AiTaggingMethod");
        modelBuilder.HasPostgresEnum<LinksRouteTo>(name: "LinksRouteTo");
        modelBuilder.HasPostgresEnum<SubscriptionProvider>(name: "SubscriptionProvider");
        modelBuilder.HasPostgresEnum<DashboardSectionType>(name: "DashboardSectionType");
        modelBuilder.HasPostgresEnum<AppMigrationStatus>(name: "AppMigrationStatus");

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

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Account_userId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Uuid, "User_uuid_key").IsUnique();

            entity.HasIndex(e => e.Username, "User_username_key").IsUnique();

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Uuid)
                .HasValueGenerator<UuidV4GuidGenerator>()
                .HasColumnName("uuid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("emailVerified");
            entity.Property(e => e.UnverifiedNewEmail).HasColumnName("unverifiedNewEmail");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Locale)
                .ValueGeneratedNever()
                .HasDefaultValue("en")
                .HasColumnName("locale");
            entity.Property(e => e.ParentSubscriptionId).HasColumnName("parentSubscriptionId");
            entity.Property(e => e.CollectionOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<int>())
                .HasColumnName("collectionOrder");
            entity.Property(e => e.LinksRouteTo)
                .ValueGeneratedNever()
                .HasDefaultValue(LinksRouteTo.Original)
                .HasColumnName("linksRouteTo");
            entity.Property(e => e.AiTaggingMethod)
                .ValueGeneratedNever()
                .HasDefaultValue(AiTaggingMethod.Disabled)
                .HasColumnName("aiTaggingMethod");
            entity.Property(e => e.AiPredefinedTags)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("aiPredefinedTags");
            entity.Property(e => e.AiTagExistingLinks)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("aiTagExistingLinks");
            entity.Property(e => e.Theme)
                .ValueGeneratedNever()
                .HasDefaultValue(Theme.Dark)
                .HasColumnName("theme");
            entity.Property(e => e.DismissedAnnouncementId).HasColumnName("dismissedAnnouncementId");
            entity.Property(e => e.ReadableFontFamily)
                .ValueGeneratedNever()
                .HasDefaultValue("sans-serif")
                .HasColumnName("readableFontFamily");
            entity.Property(e => e.ReadableFontSize)
                .ValueGeneratedNever()
                .HasDefaultValue("20px")
                .HasColumnName("readableFontSize");
            entity.Property(e => e.ReadableLineHeight)
                .ValueGeneratedNever()
                .HasDefaultValue("1.8")
                .HasColumnName("readableLineHeight");
            entity.Property(e => e.ReadableLineWidth)
                .ValueGeneratedNever()
                .HasDefaultValue("normal")
                .HasColumnName("readableLineWidth");
            entity.Property(e => e.PreventDuplicateLinks)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("preventDuplicateLinks");
            entity.Property(e => e.ArchiveAsScreenshot)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("archiveAsScreenshot");
            entity.Property(e => e.ArchiveAsMonolith)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("archiveAsMonolith");
            entity.Property(e => e.ArchiveAsPDF)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("archiveAsPDF");
            entity.Property(e => e.ArchiveAsReadable)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("archiveAsReadable");
            entity.Property(e => e.ArchiveAsWaybackMachine)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("archiveAsWaybackMachine");
            entity.Property(e => e.ReferredBy).HasColumnName("referredBy");
            entity.Property(e => e.LastPickedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastPickedAt");
            entity.Property(e => e.AcceptPromotionalEmails)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("acceptPromotionalEmails");
            entity.Property(e => e.TrialEndEmailSent)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("trialEndEmailSent");
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

            entity.HasOne(d => d.ParentSubscription).WithMany(p => p.ChildUsers)
                .HasForeignKey(d => d.ParentSubscriptionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_parentSubscriptionId_fkey");
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("VerificationToken_token_key");

            entity.ToTable("VerificationToken");

            entity.HasIndex(e => new { e.Identifier, e.Token }, "VerificationToken_identifier_token_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
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
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PasswordResetToken_token_key");

            entity.ToTable("PasswordResetToken");

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Token).HasColumnName("token");
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
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Collection_pkey");

            entity.ToTable("Collection");

            entity.HasIndex(e => e.OwnerId, "Collection_ownerId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("description");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.IconWeight).HasColumnName("iconWeight");
            entity.Property(e => e.Color)
                .ValueGeneratedNever()
                .HasDefaultValue("#0ea5e9")
                .HasColumnName("color");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.IsPublic)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isPublic");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
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

            entity.HasOne(d => d.Parent).WithMany(p => p.SubCollections)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Collection_parentId_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.Collections)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Collection_ownerId_fkey");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedCollections)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Collection_createdById_fkey");
        });

        modelBuilder.Entity<UsersAndCollections>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CollectionId }).HasName("UsersAndCollections_pkey");

            entity.ToTable("UsersAndCollections");

            entity.HasIndex(e => e.UserId, "UsersAndCollections_userId_idx");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CollectionId).HasColumnName("collectionId");
            entity.Property(e => e.CanCreate).HasColumnName("canCreate");
            entity.Property(e => e.CanUpdate).HasColumnName("canUpdate");
            entity.Property(e => e.CanDelete).HasColumnName("canDelete");
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

            entity.HasOne(d => d.User).WithMany(p => p.CollectionsJoined)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UsersAndCollections_userId_fkey");

            entity.HasOne(d => d.Collection).WithMany(p => p.Members)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UsersAndCollections_collectionId_fkey");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Link_pkey");

            entity.ToTable("Link");

            entity.HasIndex(e => e.CollectionId, "Link_collectionId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("url")
                .HasColumnName("type");
            entity.Property(e => e.Description)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("description");
            entity.Property(e => e.CreatedById).HasColumnName("createdById");
            entity.Property(e => e.CollectionId).HasColumnName("collectionId");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.IconWeight).HasColumnName("iconWeight");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.TextContent).HasColumnName("textContent");
            entity.Property(e => e.Preview).HasColumnName("preview");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Pdf).HasColumnName("pdf");
            entity.Property(e => e.Readable).HasColumnName("readable");
            entity.Property(e => e.Monolith).HasColumnName("monolith");
            entity.Property(e => e.ClientSide)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("clientSide");
            entity.Property(e => e.AiTagged)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("aiTagged");
            entity.Property(e => e.MetaDescription).HasColumnName("metaDescription");
            entity.Property(e => e.IndexVersion).HasColumnName("indexVersion");
            entity.Property(e => e.LastPreserved)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastPreserved");
            entity.Property(e => e.ImportDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("importDate");
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

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CreatedLinks)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_createdById_fkey");

            entity.HasOne(d => d.Collection).WithMany(p => p.Links)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Link_collectionId_fkey");

            entity.HasMany(d => d.PinnedBy).WithMany(p => p.PinnedLinks)
                .UsingEntity<Dictionary<string, object>>(
                    "_PinnedLinks",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_PinnedLinks_B_fkey"),
                    l => l.HasOne<Link>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_PinnedLinks_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_PinnedLinks_AB_pkey");
                        j.ToTable("_PinnedLinks");
                        j.HasIndex(new[] { "B" }, "_PinnedLinks_B_index");
                    });

            entity.HasMany(d => d.Tags).WithMany(p => p.Links)
                .UsingEntity<Dictionary<string, object>>(
                    "_LinkToTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_LinkToTag_B_fkey"),
                    l => l.HasOne<Link>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_LinkToTag_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_LinkToTag_AB_pkey");
                        j.ToTable("_LinkToTag");
                        j.HasIndex(new[] { "B" }, "_LinkToTag_B_index");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.HasIndex(e => e.OwnerId, "Tag_ownerId_idx");

            entity.HasIndex(e => new { e.Name, e.OwnerId }, "Tag_name_ownerId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.ArchiveAsScreenshot).HasColumnName("archiveAsScreenshot");
            entity.Property(e => e.ArchiveAsMonolith).HasColumnName("archiveAsMonolith");
            entity.Property(e => e.ArchiveAsPDF).HasColumnName("archiveAsPDF");
            entity.Property(e => e.ArchiveAsReadable).HasColumnName("archiveAsReadable");
            entity.Property(e => e.ArchiveAsWaybackMachine).HasColumnName("archiveAsWaybackMachine");
            entity.Property(e => e.AiTag).HasColumnName("aiTag");
            entity.Property(e => e.AiGenerated)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("aiGenerated");
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

            entity.HasOne(d => d.Owner).WithMany(p => p.Tags)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tag_ownerId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.StoreOriginalTransactionId, "Subscription_storeOriginalTransactionId_key").IsUnique();

            entity.HasIndex(e => e.GooglePurchaseToken, "Subscription_googlePurchaseToken_key").IsUnique();

            entity.HasIndex(e => e.StripeSubscriptionId, "Subscription_stripeSubscriptionId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Subscription_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.CurrentPeriodStart)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentPeriodStart");
            entity.Property(e => e.CurrentPeriodEnd)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("currentPeriodEnd");
            entity.Property(e => e.Quantity)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.StoreOriginalTransactionId).HasColumnName("storeOriginalTransactionId");
            entity.Property(e => e.StoreProductId).HasColumnName("storeProductId");
            entity.Property(e => e.GooglePurchaseToken).HasColumnName("googlePurchaseToken");
            entity.Property(e => e.StoreMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("storeMetadata");
            entity.Property(e => e.StripeSubscriptionId).HasColumnName("stripeSubscriptionId");
            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasOne(d => d.User).WithOne(p => p.Subscriptions)
                .HasForeignKey<Subscription>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Subscription_userId_fkey");
        });

        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccessToken_pkey");

            entity.ToTable("AccessToken");

            entity.HasIndex(e => e.Token, "AccessToken_token_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Revoked)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("revoked");
            entity.Property(e => e.IsSession)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSession");
            entity.Property(e => e.Expires)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastUsedAt");
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

            entity.HasOne(d => d.User).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("AccessToken_userId_fkey");
        });

        modelBuilder.Entity<RssSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RssSubscription_pkey");

            entity.ToTable("RssSubscription");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LastBuildDate)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("lastBuildDate");
            entity.Property(e => e.CollectionId).HasColumnName("collectionId");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
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

            entity.HasOne(d => d.Collection).WithMany(p => p.RssSubscriptions)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RssSubscription_collectionId_fkey");
        });

        modelBuilder.Entity<Highlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Highlight_pkey");

            entity.ToTable("Highlight");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.LinkId).HasColumnName("linkId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.StartOffset).HasColumnName("startOffset");
            entity.Property(e => e.EndOffset).HasColumnName("endOffset");
            entity.Property(e => e.Text).HasColumnName("text");
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

            entity.HasOne(d => d.Link).WithMany(p => p.Highlight)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Highlight_linkId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Highlights)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Highlight_userId_fkey");
        });

        modelBuilder.Entity<DashboardSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DashboardSection_pkey");

            entity.ToTable("DashboardSection");

            entity.HasIndex(e => new { e.UserId, e.CollectionId }, "DashboardSection_userId_collectionId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CollectionId).HasColumnName("collectionId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Order).HasColumnName("order");
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

            entity.HasOne(d => d.User).WithMany(p => p.DashboardSections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DashboardSection_userId_fkey");

            entity.HasOne(d => d.Collection).WithMany(p => p.DashboardSection)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("DashboardSection_collectionId_fkey");
        });

        modelBuilder.Entity<AppMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppMigration_pkey");

            entity.ToTable("AppMigration");

            entity.HasIndex(e => e.Name, "AppMigration_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.FinishedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("finishedAt");
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

        foreach (var entry in ChangeTracker.Entries<VerificationToken>())
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

        foreach (var entry in ChangeTracker.Entries<Collection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UsersAndCollections>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Link>())
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

        foreach (var entry in ChangeTracker.Entries<Subscription>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AccessToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<RssSubscription>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Highlight>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DashboardSection>())
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

file sealed class UuidV4GuidGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.NewGuid();
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
