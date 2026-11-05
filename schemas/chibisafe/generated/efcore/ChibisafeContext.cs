#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Chibisafe.Models;

public partial class ChibisafeContext : DbContext
{
    public ChibisafeContext(DbContextOptions<ChibisafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Files> Files { get; set; }

    public virtual DbSet<QuarantineFiles> QuarantineFiles { get; set; }

    public virtual DbSet<Snippets> Snippets { get; set; }

    public virtual DbSet<ShortenedLinks> ShortenedLinks { get; set; }

    public virtual DbSet<Albums> Albums { get; set; }

    public virtual DbSet<Bans> Bans { get; set; }

    public virtual DbSet<Links> Links { get; set; }

    public virtual DbSet<Settings> Settings { get; set; }

    public virtual DbSet<Statistics> Statistics { get; set; }

    public virtual DbSet<Tags> Tags { get; set; }

    public virtual DbSet<Invites> Invites { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Uuid, "users_uuid_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.HasIndex(e => e.ApiKey, "users_apiKey_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.StorageQuota)
                .ValueGeneratedNever()
                .HasDefaultValue("0")
                .HasColumnName("storageQuota");
            entity.Property(e => e.ApiKey).HasColumnName("apiKey");
            entity.Property(e => e.PasswordEditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("passwordEditedAt");
            entity.Property(e => e.ApiKeyEditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("apiKeyEditedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "_rolesTousers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_rolesTousers_B_fkey"),
                    l => l.HasOne<Roles>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_rolesTousers_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_rolesTousers_AB_pkey");
                        j.ToTable("_rolesTousers");
                        j.HasIndex(new[] { "B" }, "_rolesTousers_B_index");
                    });
        });

        modelBuilder.Entity<Files>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.HasAlternateKey(e => e.Uuid).HasName("files_uuid_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Original).HasColumnName("original");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.SourceUrl).HasColumnName("sourceUrl");
            entity.Property(e => e.IsS3)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isS3");
            entity.Property(e => e.IsWatched)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isWatched");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
            entity.Property(e => e.Quarantine)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("quarantine");

            entity.HasOne(d => d.User).WithMany(p => p.Files)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("files_userId_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Files)
                .UsingEntity<Dictionary<string, object>>(
                    "_filesTotags",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_filesTotags_B_fkey"),
                    l => l.HasOne<Files>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_filesTotags_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_filesTotags_AB_pkey");
                        j.ToTable("_filesTotags");
                        j.HasIndex(new[] { "B" }, "_filesTotags_B_index");
                    });
        });

        modelBuilder.Entity<QuarantineFiles>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("quarantineFiles_fileId_key");

            entity.ToTable("quarantineFiles");

            entity.Property(e => e.FileId).HasColumnName("fileId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Reason).HasColumnName("reason");

            entity.HasOne(d => d.File).WithOne(p => p.QuarantineFile)
                .HasPrincipalKey<Files>(p => p.Uuid)
                .HasForeignKey<QuarantineFiles>(d => d.FileId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("quarantineFiles_fileId_fkey");
        });

        modelBuilder.Entity<Snippets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("snippets_pkey");

            entity.ToTable("snippets");

            entity.HasIndex(e => e.Uuid, "snippets_uuid_key").IsUnique();

            entity.HasIndex(e => e.Identifier, "snippets_identifier_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.ParentUuid).HasColumnName("parentUuid");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
        });

        modelBuilder.Entity<ShortenedLinks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shortenedLinks_pkey");

            entity.ToTable("shortenedLinks");

            entity.HasIndex(e => e.Uuid, "shortenedLinks_uuid_key").IsUnique();

            entity.HasIndex(e => e.Identifier, "shortenedLinks_identifier_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Destination).HasColumnName("destination");
            entity.Property(e => e.Views)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("views");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.User).WithMany(p => p.Links)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("shortenedLinks_userId_fkey");
        });

        modelBuilder.Entity<Albums>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("albums_pkey");

            entity.ToTable("albums");

            entity.HasIndex(e => e.Uuid, "albums_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ZippedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("zippedAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
            entity.Property(e => e.Nsfw)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("nsfw");
            entity.Property(e => e.SortOrder).HasColumnName("sortOrder");

            entity.HasMany(d => d.Files).WithMany(p => p.Albums)
                .UsingEntity<Dictionary<string, object>>(
                    "_albumsTofiles",
                    r => r.HasOne<Files>().WithMany()
                        .HasForeignKey("B")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_albumsTofiles_B_fkey"),
                    l => l.HasOne<Albums>().WithMany()
                        .HasForeignKey("A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("_albumsTofiles_A_fkey"),
                    j =>
                    {
                        j.HasKey("A", "B").HasName("_albumsTofiles_AB_pkey");
                        j.ToTable("_albumsTofiles");
                        j.HasIndex(new[] { "B" }, "_albumsTofiles_B_index");
                    });
        });

        modelBuilder.Entity<Bans>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bans_pkey");

            entity.ToTable("bans");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Links>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("links_pkey");

            entity.ToTable("links");

            entity.HasIndex(e => e.Uuid, "links_uuid_key").IsUnique();

            entity.HasIndex(e => e.Identifier, "links_identifier_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AlbumId).HasColumnName("albumId");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Views)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("views");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.EnableDownload)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("enableDownload");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
        });

        modelBuilder.Entity<Settings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("settings_pkey");

            entity.ToTable("settings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RateLimitWindow).HasColumnName("rateLimitWindow");
            entity.Property(e => e.RateLimitMax).HasColumnName("rateLimitMax");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.ServiceName).HasColumnName("serviceName");
            entity.Property(e => e.ChunkSize).HasColumnName("chunkSize");
            entity.Property(e => e.ChunkedUploadsTimeout).HasColumnName("chunkedUploadsTimeout");
            entity.Property(e => e.MaxSize).HasColumnName("maxSize");
            entity.Property(e => e.GenerateZips).HasColumnName("generateZips");
            entity.Property(e => e.GenerateOriginalFileNameWithIdentifier)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("generateOriginalFileNameWithIdentifier");
            entity.Property(e => e.GeneratedFilenameLength).HasColumnName("generatedFilenameLength");
            entity.Property(e => e.GeneratedAlbumLength).HasColumnName("generatedAlbumLength");
            entity.Property(e => e.GeneratedLinksLength)
                .ValueGeneratedNever()
                .HasDefaultValue(8)
                .HasColumnName("generatedLinksLength");
            entity.Property(e => e.SaveDuplicatesToAlbum)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("saveDuplicatesToAlbum");
            entity.Property(e => e.BlockedExtensions).HasColumnName("blockedExtensions");
            entity.Property(e => e.BlockNoExtension).HasColumnName("blockNoExtension");
            entity.Property(e => e.PublicMode).HasColumnName("publicMode");
            entity.Property(e => e.UserAccounts).HasColumnName("userAccounts");
            entity.Property(e => e.DisableStatisticsCron).HasColumnName("disableStatisticsCron");
            entity.Property(e => e.DisableUpdateCheck)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("disableUpdateCheck");
            entity.Property(e => e.BackgroundImageURL).HasColumnName("backgroundImageURL");
            entity.Property(e => e.LogoURL).HasColumnName("logoURL");
            entity.Property(e => e.MetaDescription).HasColumnName("metaDescription");
            entity.Property(e => e.MetaKeywords).HasColumnName("metaKeywords");
            entity.Property(e => e.MetaTwitterHandle).HasColumnName("metaTwitterHandle");
            entity.Property(e => e.MetaDomain)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("metaDomain");
            entity.Property(e => e.ServeUploadsFrom)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("serveUploadsFrom");
            entity.Property(e => e.EnableMixedCaseFilenames)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enableMixedCaseFilenames");
            entity.Property(e => e.UsersStorageQuota)
                .ValueGeneratedNever()
                .HasDefaultValue("0")
                .HasColumnName("usersStorageQuota");
            entity.Property(e => e.UseNetworkStorage)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useNetworkStorage");
            entity.Property(e => e.UseMinimalHomepage)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useMinimalHomepage");
            entity.Property(e => e.UseUrlShortener)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("useUrlShortener");
            entity.Property(e => e.GenerateThumbnails)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("generateThumbnails");
            entity.Property(e => e.PrivacyPolicyPageContent)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("privacyPolicyPageContent");
            entity.Property(e => e.TermsOfServicePageContent)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("termsOfServicePageContent");
            entity.Property(e => e.RulesPageContent)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("rulesPageContent");
            entity.Property(e => e.S3Region)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.S3Bucket)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.S3AccessKey)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.S3SecretKey)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.S3Endpoint)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.S3PathStyle)
                .ValueGeneratedNever()
                .HasDefaultValue(false);
            entity.Property(e => e.S3PublicUrl)
                .ValueGeneratedNever()
                .HasDefaultValue("");
            entity.Property(e => e.DefaultSortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue("createdAt:desc")
                .HasColumnName("defaultSortOrder");
        });

        modelBuilder.Entity<Statistics>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statistics_pkey");

            entity.ToTable("statistics");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batchId");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.HasIndex(e => e.Uuid, "tags_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
        });

        modelBuilder.Entity<Invites>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invites_pkey");

            entity.ToTable("invites");

            entity.HasIndex(e => e.Code, "invites_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.UsedBy).HasColumnName("usedBy");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Used)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("used");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcTextConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.EditedAt)
                .HasConversion<UtcTextConverter>()
                .HasColumnName("editedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

file sealed class UtcNowGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
}

file sealed class UtcTextConverter : ValueConverter<DateTime, string>
{
    public UtcTextConverter()
        : base(v => (v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'+00:00'", CultureInfo.InvariantCulture), v => DateTime.Parse(v, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal))
    {
    }
}
