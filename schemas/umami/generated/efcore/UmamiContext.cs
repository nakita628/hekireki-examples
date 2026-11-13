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

namespace Umami.Models;

public partial class UmamiContext : DbContext
{
    public UmamiContext(DbContextOptions<UmamiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionLink> SessionLinks { get; set; }

    public virtual DbSet<Website> Websites { get; set; }

    public virtual DbSet<WebsiteEvent> WebsiteEvents { get; set; }

    public virtual DbSet<EventData> EventData { get; set; }

    public virtual DbSet<SessionData> SessionData { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamUser> TeamUsers { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Annotation> Annotations { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Pixel> Pixels { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Share> Shares { get; set; }

    public virtual DbSet<SessionReplay> SessionReplays { get; set; }

    public virtual DbSet<SessionReplaySaved> SessionReplaySaveds { get; set; }

    public virtual DbSet<HeatmapEvent> HeatmapEvents { get; set; }

    public virtual DbSet<TwoFactorAuth> TwoFactorAuths { get; set; }

    public virtual DbSet<TwoFactorBackupCode> TwoFactorBackupCodes { get; set; }

    public virtual DbSet<TwoFactorOtpUsed> TwoFactorOtpUseds { get; set; }

    public virtual DbSet<TwoFactorRateLimit> TwoFactorRateLimits { get; set; }

    public virtual DbSet<AppSetting> AppSettings { get; set; }

    public virtual DbSet<ErrorIssue> ErrorIssues { get; set; }

    public virtual DbSet<ErrorEvent> ErrorEvents { get; set; }

    public virtual DbSet<ErrorRateLimit> ErrorRateLimits { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "user_username_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(2183)
                .HasColumnName("logo_url");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasColumnName("display_name");
            entity.Property(e => e.TwoFactorRequired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("two_factor_required");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("api_key_pkey");

            entity.ToTable("api_key");

            entity.HasIndex(e => e.KeyHash, "api_key_key_hash_key").IsUnique();

            entity.HasIndex(e => e.UserId, "api_key_user_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("api_key_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.KeyHash)
                .HasMaxLength(128)
                .HasColumnName("key_hash");
            entity.Property(e => e.KeyPrefix)
                .HasMaxLength(20)
                .HasColumnName("key_prefix");
            entity.Property(e => e.LastUsedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("last_used_at");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_key_user_id_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pkey");

            entity.ToTable("session");

            entity.HasIndex(e => e.CreatedAt, "session_created_at_idx");

            entity.HasIndex(e => e.WebsiteId, "session_website_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "session_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Browser }, "session_website_id_created_at_browser_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Os }, "session_website_id_created_at_os_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Device }, "session_website_id_created_at_device_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Screen }, "session_website_id_created_at_screen_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Language }, "session_website_id_created_at_language_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Country }, "session_website_id_created_at_country_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Region }, "session_website_id_created_at_region_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.City }, "session_website_id_created_at_city_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("session_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.Browser)
                .HasMaxLength(20)
                .HasColumnName("browser");
            entity.Property(e => e.Os)
                .HasMaxLength(20)
                .HasColumnName("os");
            entity.Property(e => e.Device)
                .HasMaxLength(20)
                .HasColumnName("device");
            entity.Property(e => e.Screen)
                .HasMaxLength(11)
                .HasColumnName("screen");
            entity.Property(e => e.Language)
                .HasMaxLength(35)
                .HasColumnName("language");
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country");
            entity.Property(e => e.Region)
                .HasMaxLength(20)
                .HasColumnName("region");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.DistinctId)
                .HasMaxLength(50)
                .HasColumnName("distinct_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<SessionLink>(entity =>
        {
            entity.HasKey(e => new { e.WebsiteId, e.DistinctId, e.SessionId }).HasName("session_link_pkey");

            entity.ToTable("session_link");

            entity.HasIndex(e => new { e.WebsiteId, e.SessionId }, "session_link_website_id_session_id_idx");

            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.DistinctId)
                .HasMaxLength(50)
                .HasColumnName("distinct_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Website>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("website_pkey");

            entity.ToTable("website");

            entity.HasIndex(e => e.UserId, "website_user_id_idx");

            entity.HasIndex(e => e.TeamId, "website_team_id_idx");

            entity.HasIndex(e => e.CreatedAt, "website_created_at_idx");

            entity.HasIndex(e => e.CreatedBy, "website_created_by_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("website_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Domain)
                .HasMaxLength(500)
                .HasColumnName("domain");
            entity.Property(e => e.ResetAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("reset_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deleted_at");
            entity.Property(e => e.RecorderEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("recorder_enabled");
            entity.Property(e => e.ReplayConfig)
                .HasColumnType("jsonb")
                .HasColumnName("replay_config");

            entity.HasOne(d => d.User).WithMany(p => p.Websites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("website_user_id_fkey");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.CreatedBy)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("website_created_by_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Websites)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("website_team_id_fkey");
        });

        modelBuilder.Entity<WebsiteEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("website_event_pkey");

            entity.ToTable("website_event");

            entity.HasIndex(e => e.CreatedAt, "website_event_created_at_idx");

            entity.HasIndex(e => e.SessionId, "website_event_session_id_idx");

            entity.HasIndex(e => e.VisitId, "website_event_visit_id_idx");

            entity.HasIndex(e => e.WebsiteId, "website_event_website_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "website_event_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.UrlPath }, "website_event_website_id_created_at_url_path_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.UrlQuery }, "website_event_website_id_created_at_url_query_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.ReferrerDomain }, "website_event_website_id_created_at_referrer_domain_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.PageTitle }, "website_event_website_id_created_at_page_title_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.EventName }, "website_event_website_id_created_at_event_name_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Tag }, "website_event_website_id_created_at_tag_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.SessionId, e.CreatedAt }, "website_event_website_id_session_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.VisitId, e.CreatedAt }, "website_event_website_id_visit_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.Hostname }, "website_event_website_id_created_at_hostname_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("event_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UrlPath)
                .HasMaxLength(500)
                .HasColumnName("url_path");
            entity.Property(e => e.UrlQuery)
                .HasMaxLength(500)
                .HasColumnName("url_query");
            entity.Property(e => e.UtmSource)
                .HasMaxLength(255)
                .HasColumnName("utm_source");
            entity.Property(e => e.UtmMedium)
                .HasMaxLength(255)
                .HasColumnName("utm_medium");
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(255)
                .HasColumnName("utm_campaign");
            entity.Property(e => e.UtmContent)
                .HasMaxLength(255)
                .HasColumnName("utm_content");
            entity.Property(e => e.UtmTerm)
                .HasMaxLength(255)
                .HasColumnName("utm_term");
            entity.Property(e => e.ReferrerPath)
                .HasMaxLength(500)
                .HasColumnName("referrer_path");
            entity.Property(e => e.ReferrerQuery)
                .HasMaxLength(500)
                .HasColumnName("referrer_query");
            entity.Property(e => e.ReferrerDomain)
                .HasMaxLength(500)
                .HasColumnName("referrer_domain");
            entity.Property(e => e.PageTitle)
                .HasMaxLength(500)
                .HasColumnName("page_title");
            entity.Property(e => e.Gclid)
                .HasMaxLength(255)
                .HasColumnName("gclid");
            entity.Property(e => e.Fbclid)
                .HasMaxLength(255)
                .HasColumnName("fbclid");
            entity.Property(e => e.Msclkid)
                .HasMaxLength(255)
                .HasColumnName("msclkid");
            entity.Property(e => e.Ttclid)
                .HasMaxLength(255)
                .HasColumnName("ttclid");
            entity.Property(e => e.Lifatid)
                .HasMaxLength(255)
                .HasColumnName("li_fat_id");
            entity.Property(e => e.Twclid)
                .HasMaxLength(255)
                .HasColumnName("twclid");
            entity.Property(e => e.EventType)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("event_type");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .HasColumnName("event_name");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("tag");
            entity.Property(e => e.Hostname)
                .HasMaxLength(100)
                .HasColumnName("hostname");
            entity.Property(e => e.Lcp)
                .HasPrecision(10, 1)
                .HasColumnName("lcp");
            entity.Property(e => e.Inp)
                .HasPrecision(10, 1)
                .HasColumnName("inp");
            entity.Property(e => e.Cls)
                .HasPrecision(10, 4)
                .HasColumnName("cls");
            entity.Property(e => e.Fcp)
                .HasPrecision(10, 1)
                .HasColumnName("fcp");
            entity.Property(e => e.Ttfb)
                .HasPrecision(10, 1)
                .HasColumnName("ttfb");

            entity.HasOne(d => d.Session).WithMany(p => p.WebsiteEvents)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("website_event_session_id_fkey");
        });

        modelBuilder.Entity<EventData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_data_pkey");

            entity.ToTable("event_data");

            entity.HasIndex(e => e.CreatedAt, "event_data_created_at_idx");

            entity.HasIndex(e => e.WebsiteId, "event_data_website_id_idx");

            entity.HasIndex(e => e.WebsiteEventId, "event_data_website_event_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "event_data_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.DataKey }, "event_data_website_id_created_at_data_key_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("event_data_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.WebsiteEventId).HasColumnName("website_event_id");
            entity.Property(e => e.DataKey)
                .HasMaxLength(500)
                .HasColumnName("data_key");
            entity.Property(e => e.StringValue)
                .HasMaxLength(500)
                .HasColumnName("string_value");
            entity.Property(e => e.NumberValue)
                .HasPrecision(19, 4)
                .HasColumnName("number_value");
            entity.Property(e => e.DateValue)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("date_value");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.Website).WithMany(p => p.EventData)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("event_data_website_id_fkey");

            entity.HasOne(d => d.WebsiteEvent).WithMany(p => p.EventData)
                .HasForeignKey(d => d.WebsiteEventId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("event_data_website_event_id_fkey");
        });

        modelBuilder.Entity<SessionData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_data_pkey");

            entity.ToTable("session_data");

            entity.HasIndex(e => e.CreatedAt, "session_data_created_at_idx");

            entity.HasIndex(e => e.WebsiteId, "session_data_website_id_idx");

            entity.HasIndex(e => e.SessionId, "session_data_session_id_idx");

            entity.HasIndex(e => new { e.SessionId, e.CreatedAt }, "session_data_session_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt, e.DataKey }, "session_data_website_id_created_at_data_key_idx");

            entity.HasIndex(e => new { e.SessionId, e.DataKey }, "session_data_session_id_data_key_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("session_data_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.DataKey)
                .HasMaxLength(500)
                .HasColumnName("data_key");
            entity.Property(e => e.StringValue)
                .HasMaxLength(500)
                .HasColumnName("string_value");
            entity.Property(e => e.NumberValue)
                .HasPrecision(19, 4)
                .HasColumnName("number_value");
            entity.Property(e => e.DateValue)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("date_value");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.DistinctId)
                .HasMaxLength(50)
                .HasColumnName("distinct_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.Website).WithMany(p => p.SessionData)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("session_data_website_id_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionData)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("session_data_session_id_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_pkey");

            entity.ToTable("team");

            entity.HasIndex(e => e.AccessCode, "team_access_code_key").IsUnique();

            entity.HasIndex(e => e.AccessCode, "team_access_code_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("team_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.AccessCode)
                .HasMaxLength(50)
                .HasColumnName("access_code");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(2183)
                .HasColumnName("logo_url");
            entity.Property(e => e.TwoFactorRequired)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("two_factor_required");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<TeamUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_user_pkey");

            entity.ToTable("team_user");

            entity.HasIndex(e => e.TeamId, "team_user_team_id_idx");

            entity.HasIndex(e => e.UserId, "team_user_user_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("team_user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Team).WithMany(p => p.Members)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("team_user_team_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Teams)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("team_user_user_id_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("report_pkey");

            entity.ToTable("report");

            entity.HasIndex(e => e.UserId, "report_user_id_idx");

            entity.HasIndex(e => e.WebsiteId, "report_website_id_idx");

            entity.HasIndex(e => e.Type, "report_type_idx");

            entity.HasIndex(e => e.Name, "report_name_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("report_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("report_user_id_fkey");

            entity.HasOne(d => d.Website).WithMany(p => p.Reports)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("report_website_id_fkey");
        });

        modelBuilder.Entity<Annotation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("annotation_pkey");

            entity.ToTable("annotation");

            entity.HasIndex(e => new { e.WebsiteId, e.Date }, "annotation_website_id_date_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("annotation_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Date)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("date");
            entity.Property(e => e.AllDay)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("all_day");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Website).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("annotation_website_id_fkey");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("segment_pkey");

            entity.ToTable("segment");

            entity.HasIndex(e => e.WebsiteId, "segment_website_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("segment_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Website).WithMany(p => p.Segments)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("segment_website_id_fkey");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("revenue_pkey");

            entity.ToTable("revenue");

            entity.HasIndex(e => e.WebsiteId, "revenue_website_id_idx");

            entity.HasIndex(e => e.SessionId, "revenue_session_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "revenue_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.SessionId, e.CreatedAt }, "revenue_website_id_session_id_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("revenue_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .HasColumnName("event_name");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.Revenue1)
                .HasPrecision(19, 4)
                .HasColumnName("revenue");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.Website).WithMany(p => p.Revenue)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("revenue_website_id_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.Revenue)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("revenue_session_id_fkey");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("link_pkey");

            entity.ToTable("link");

            entity.HasIndex(e => e.Slug, "link_slug_key").IsUnique();

            entity.HasIndex(e => e.Slug, "link_slug_idx");

            entity.HasIndex(e => e.UserId, "link_user_id_idx");

            entity.HasIndex(e => e.TeamId, "link_team_id_idx");

            entity.HasIndex(e => e.CreatedAt, "link_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("link_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deleted_at");

            entity.HasOne(d => d.User).WithMany(p => p.Links)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("link_user_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Links)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("link_team_id_fkey");
        });

        modelBuilder.Entity<Pixel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pixel_pkey");

            entity.ToTable("pixel");

            entity.HasIndex(e => e.Slug, "pixel_slug_key").IsUnique();

            entity.HasIndex(e => e.Slug, "pixel_slug_idx");

            entity.HasIndex(e => e.UserId, "pixel_user_id_idx");

            entity.HasIndex(e => e.TeamId, "pixel_team_id_idx");

            entity.HasIndex(e => e.CreatedAt, "pixel_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("pixel_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("deleted_at");

            entity.HasOne(d => d.User).WithMany(p => p.Pixels)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("pixel_user_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Pixels)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("pixel_team_id_fkey");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("board_pkey");

            entity.ToTable("board");

            entity.HasIndex(e => e.UserId, "board_user_id_idx");

            entity.HasIndex(e => e.TeamId, "board_team_id_idx");

            entity.HasIndex(e => e.CreatedAt, "board_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("board_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithMany(p => p.Boards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("board_user_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Boards)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("board_team_id_fkey");
        });

        modelBuilder.Entity<Share>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("share_pkey");

            entity.ToTable("share");

            entity.HasIndex(e => e.Slug, "share_slug_key").IsUnique();

            entity.HasIndex(e => e.EntityId, "share_entity_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("share_id");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ShareType).HasColumnName("share_type");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SessionReplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_replay_pkey");

            entity.ToTable("session_replay");

            entity.HasIndex(e => e.WebsiteId, "session_replay_website_id_idx");

            entity.HasIndex(e => e.SessionId, "session_replay_session_id_idx");

            entity.HasIndex(e => e.VisitId, "session_replay_visit_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.SessionId }, "session_replay_website_id_session_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.VisitId }, "session_replay_website_id_visit_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "session_replay_website_id_created_at_idx");

            entity.HasIndex(e => new { e.SessionId, e.ChunkIndex }, "session_replay_session_id_chunk_index_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("replay_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ChunkIndex).HasColumnName("chunk_index");
            entity.Property(e => e.Events).HasColumnName("events");
            entity.Property(e => e.EventCount).HasColumnName("event_count");
            entity.Property(e => e.StartedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("started_at");
            entity.Property(e => e.EndedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("ended_at");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.Website).WithMany(p => p.SessionReplays)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("session_replay_website_id_fkey");
        });

        modelBuilder.Entity<SessionReplaySaved>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_replay_saved_pkey");

            entity.ToTable("session_replay_saved");

            entity.HasIndex(e => e.WebsiteId, "session_replay_saved_website_id_idx");

            entity.HasIndex(e => e.VisitId, "session_replay_saved_visit_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "session_replay_saved_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.VisitId }, "session_replay_saved_website_id_visit_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("saved_replay_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedNever()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Website).WithMany(p => p.SessionReplaysSaved)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("session_replay_saved_website_id_fkey");
        });

        modelBuilder.Entity<HeatmapEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("heatmap_event_pkey");

            entity.ToTable("heatmap_event");

            entity.HasIndex(e => e.WebsiteId, "heatmap_event_website_id_idx");

            entity.HasIndex(e => e.VisitId, "heatmap_event_visit_id_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "heatmap_event_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.UrlPath, e.EventType, e.CreatedAt }, "heatmap_event_website_id_url_path_event_type_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("heatmap_event_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.UrlPath)
                .HasMaxLength(500)
                .HasColumnName("url_path");
            entity.Property(e => e.EventType).HasColumnName("event_type");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");
            entity.Property(e => e.PageX).HasColumnName("page_x");
            entity.Property(e => e.PageY).HasColumnName("page_y");
            entity.Property(e => e.PageW).HasColumnName("page_w");
            entity.Property(e => e.ViewportW).HasColumnName("viewport_w");
            entity.Property(e => e.ViewportH).HasColumnName("viewport_h");
            entity.Property(e => e.PageH).HasColumnName("page_h");
            entity.Property(e => e.ScrollPct).HasColumnName("scroll_pct");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.Website).WithMany(p => p.HeatmapEvents)
                .HasForeignKey(d => d.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("heatmap_event_website_id_fkey");
        });

        modelBuilder.Entity<TwoFactorAuth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_auth_pkey");

            entity.ToTable("two_factor_auth");

            entity.HasIndex(e => e.UserId, "two_factor_auth_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_enabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.TwoFactorAuth)
                .HasForeignKey<TwoFactorAuth>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_auth_user_id_fkey");
        });

        modelBuilder.Entity<TwoFactorBackupCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_backup_code_pkey");

            entity.ToTable("two_factor_backup_code");

            entity.HasIndex(e => e.UserId, "two_factor_backup_code_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CodeHash).HasColumnName("code_hash");
            entity.Property(e => e.Used)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("used");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany(p => p.TwoFactorBackupCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_backup_code_user_id_fkey");
        });

        modelBuilder.Entity<TwoFactorOtpUsed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_otp_used_pkey");

            entity.ToTable("two_factor_otp_used");

            entity.HasIndex(e => new { e.UserId, e.Otp }, "two_factor_otp_used_user_id_otp_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Otp).HasColumnName("otp");
            entity.Property(e => e.ExpiresAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("expires_at");

            entity.HasOne(d => d.User).WithMany(p => p.TwoFactorOtpUseds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_otp_used_user_id_fkey");
        });

        modelBuilder.Entity<TwoFactorRateLimit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("two_factor_rate_limit_pkey");

            entity.ToTable("two_factor_rate_limit");

            entity.HasIndex(e => e.UserId, "two_factor_rate_limit_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.LockedUntil)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("locked_until");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.TwoFactorRateLimit)
                .HasForeignKey<TwoFactorRateLimit>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("two_factor_rate_limit_user_id_fkey");
        });

        modelBuilder.Entity<AppSetting>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("app_setting_pkey");

            entity.ToTable("app_setting");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<ErrorIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("error_issue_pkey");

            entity.ToTable("error_issue");

            entity.HasIndex(e => new { e.WebsiteId, e.Status }, "error_issue_website_id_status_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.GroupingVersion, e.Fingerprint }, "error_issue_website_id_grouping_version_fingerprint_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("issue_id");
            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.Fingerprint)
                .HasMaxLength(64)
                .HasColumnName("fingerprint");
            entity.Property(e => e.GroupingVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("grouping_version");
            entity.Property(e => e.Title)
                .HasMaxLength(2200)
                .HasColumnName("title");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasMaxLength(20)
                .HasDefaultValue("unresolved")
                .HasColumnName("status");
            entity.Property(e => e.ResolvedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("resolved_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<ErrorEvent>(entity =>
        {
            entity.HasKey(e => new { e.WebsiteId, e.Id }).HasName("error_event_pkey");

            entity.ToTable("error_event");

            entity.HasIndex(e => new { e.WebsiteId, e.CreatedAt }, "error_event_website_id_created_at_idx");

            entity.HasIndex(e => new { e.WebsiteId, e.IssueId, e.CreatedAt }, "error_event_website_id_issue_id_created_at_idx");

            entity.Property(e => e.WebsiteId).HasColumnName("website_id");
            entity.Property(e => e.Id).HasColumnName("event_id");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("created_at");
            entity.Property(e => e.ReceivedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("received_at");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Message)
                .HasMaxLength(2000)
                .HasColumnName("message");
            entity.Property(e => e.Stack).HasColumnName("stack");
            entity.Property(e => e.Frames)
                .HasColumnType("jsonb")
                .HasColumnName("frames");
            entity.Property(e => e.Tags)
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.Handled).HasColumnName("handled");
            entity.Property(e => e.UrlPath)
                .HasMaxLength(500)
                .HasColumnName("url_path");
            entity.Property(e => e.Release)
                .HasMaxLength(100)
                .HasColumnName("release");
            entity.Property(e => e.Environment)
                .HasMaxLength(50)
                .HasColumnName("environment");
            entity.Property(e => e.Browser)
                .HasMaxLength(50)
                .HasColumnName("browser");
            entity.Property(e => e.Os)
                .HasMaxLength(50)
                .HasColumnName("os");
            entity.Property(e => e.Device)
                .HasMaxLength(50)
                .HasColumnName("device");
        });

        modelBuilder.Entity<ErrorRateLimit>(entity =>
        {
            entity.HasKey(e => e.WebsiteId).HasName("error_rate_limit_pkey");

            entity.ToTable("error_rate_limit");

            entity.Property(e => e.WebsiteId)
                .ValueGeneratedNever()
                .HasColumnName("website_id");
            entity.Property(e => e.Bucket)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("bucket");
            entity.Property(e => e.Count).HasColumnName("count");
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

        foreach (var entry in ChangeTracker.Entries<Website>())
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

        foreach (var entry in ChangeTracker.Entries<Report>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Annotation>())
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

        foreach (var entry in ChangeTracker.Entries<Link>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Pixel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Board>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Share>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SessionReplaySaved>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TwoFactorAuth>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TwoFactorRateLimit>())
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

file sealed class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Utc), v => v)
    {
    }
}
