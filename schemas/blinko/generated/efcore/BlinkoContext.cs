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

namespace Blinko.Models;

public partial class BlinkoContext : DbContext
{
    public BlinkoContext(DbContextOptions<BlinkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accounts> Accounts { get; set; }

    public virtual DbSet<Attachments> Attachments { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Notes> Notes { get; set; }

    public virtual DbSet<Comments> Comments { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsToNote> TagsToNotes { get; set; }

    public virtual DbSet<NoteReference> NoteReferences { get; set; }

    public virtual DbSet<Follows> Follows { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Cache> Caches { get; set; }

    public virtual DbSet<Plugin> Plugins { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<NoteHistory> NoteHistories { get; set; }

    public virtual DbSet<NoteInternalShare> NoteInternalShares { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<AiProviders> AiProviders { get; set; }

    public virtual DbSet<AiModels> AiModels { get; set; }

    public virtual DbSet<AiScheduledTask> AiScheduledTasks { get; set; }

    public virtual DbSet<McpServers> McpServers { get; set; }

    public virtual DbSet<Fonts> Fonts { get; set; }

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
        modelBuilder.Entity<Accounts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Nickname)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Image)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.ApiToken)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("apiToken");
            entity.Property(e => e.Description)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("description");
            entity.Property(e => e.Note)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("note");
            entity.Property(e => e.Role)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("role");
            entity.Property(e => e.LoginType)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("loginType");
            entity.Property(e => e.LinkAccountId).HasColumnName("linkAccountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Attachments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsShare)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isShare");
            entity.Property(e => e.SharePassword)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("sharePassword");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("path");
            entity.Property(e => e.Size)
                .ValueGeneratedNever()
                .HasDefaultValue(0m)
                .HasColumnName("size");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.NoteId).HasColumnName("noteId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
            entity.Property(e => e.PerfixPath)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("perfixPath");
            entity.Property(e => e.Depth).HasColumnName("depth");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");

            entity.HasOne(d => d.Note).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attachments_noteId_fkey");

            entity.HasOne(d => d.Account).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attachments_accountId_fkey");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("config_pkey");

            entity.ToTable("config");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Key)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("key");
            entity.Property(e => e.Config1)
                .HasColumnType("json")
                .HasColumnName("config");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Configs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("config_userId_fkey");
        });

        modelBuilder.Entity<Notes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notes_pkey");

            entity.ToTable("notes");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("type");
            entity.Property(e => e.Content)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("content");
            entity.Property(e => e.IsArchived)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.IsRecycle)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRecycle");
            entity.Property(e => e.IsShare)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isShare");
            entity.Property(e => e.IsTop)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isTop");
            entity.Property(e => e.IsReviewed)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isReviewed");
            entity.Property(e => e.SharePassword)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("sharePassword");
            entity.Property(e => e.ShareEncryptedUrl)
                .HasColumnType("character varying")
                .HasColumnName("shareEncryptedUrl");
            entity.Property(e => e.ShareExpiryDate)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("shareExpiryDate");
            entity.Property(e => e.ShareMaxView)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("shareMaxView");
            entity.Property(e => e.ShareViewCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("shareViewCount");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Account).WithMany(p => p.Notes)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("notes_accountId_fkey");
        });

        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.GuestName)
                .HasColumnType("character varying")
                .HasColumnName("guestName");
            entity.Property(e => e.GuestIP)
                .HasColumnType("character varying")
                .HasColumnName("guestIP");
            entity.Property(e => e.GuestUA)
                .HasColumnType("character varying")
                .HasColumnName("guestUA");
            entity.Property(e => e.NoteId).HasColumnName("noteId");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Note).WithMany(p => p.Comments)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("comments_noteId_fkey");

            entity.HasOne(d => d.Account).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("comments_accountId_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.Replies)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("comments_parentId_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Icon)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnType("character varying")
                .HasColumnName("icon");
            entity.Property(e => e.Parent)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("parent");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");

            entity.HasOne(d => d.Account).WithMany(p => p.Tags)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tag_accountId_fkey");
        });

        modelBuilder.Entity<TagsToNote>(entity =>
        {
            entity.HasKey(e => new { e.NoteId, e.TagId }).HasName("tagsToNote_pkey");

            entity.ToTable("tagsToNote");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.NoteId)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("noteId");
            entity.Property(e => e.TagId)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("tagId");

            entity.HasOne(d => d.Note).WithMany(p => p.Tags)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tagsToNote_noteId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagsToNote)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tagsToNote_tagId_fkey");
        });

        modelBuilder.Entity<NoteReference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("noteReference_pkey");

            entity.ToTable("noteReference");

            entity.HasIndex(e => new { e.FromNoteId, e.ToNoteId }, "noteReference_fromNoteId_toNoteId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.FromNoteId).HasColumnName("fromNoteId");
            entity.Property(e => e.ToNoteId).HasColumnName("toNoteId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.FromNote).WithMany(p => p.References)
                .HasForeignKey(d => d.FromNoteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("noteReference_fromNoteId_fkey");

            entity.HasOne(d => d.ToNote).WithMany(p => p.ReferencedBy)
                .HasForeignKey(d => d.ToNoteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("noteReference_toNoteId_fkey");
        });

        modelBuilder.Entity<Follows>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("follows_pkey");

            entity.ToTable("follows");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.SiteName)
                .HasColumnType("character varying")
                .HasColumnName("siteName");
            entity.Property(e => e.SiteUrl)
                .HasColumnType("character varying")
                .HasColumnName("siteUrl");
            entity.Property(e => e.SiteAvatar)
                .HasColumnType("character varying")
                .HasColumnName("siteAvatar");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
            entity.Property(e => e.FollowType)
                .ValueGeneratedNever()
                .HasDefaultValue("following")
                .HasColumnType("character varying")
                .HasColumnName("followType");
            entity.Property(e => e.AccountId).HasColumnName("accountId");

            entity.HasOne(d => d.Account).WithMany(p => p.Follows)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("follows_accountId_fkey");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");
            entity.Property(e => e.IsRead)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isRead");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("notifications_accountId_fkey");
        });

        modelBuilder.Entity<Cache>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cache_pkey");

            entity.ToTable("cache");

            entity.HasIndex(e => e.Key, "cache_key_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Key)
                .HasColumnType("character varying")
                .HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("json")
                .HasColumnName("value");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Plugin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plugin_pkey");

            entity.ToTable("plugin");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");
            entity.Property(e => e.Path)
                .HasColumnType("character varying")
                .HasColumnName("path");
            entity.Property(e => e.IsUse)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isUse");
            entity.Property(e => e.IsDev)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isDev");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conversation_pkey");

            entity.ToTable("conversation");

            entity.HasIndex(e => e.AccountId, "conversation_accountId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .ValueGeneratedNever()
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("title");
            entity.Property(e => e.IsShare)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isShare");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Account).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("conversation_accountId_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("message_pkey");

            entity.ToTable("message");

            entity.HasIndex(e => e.ConversationId, "message_conversationId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.ConversationId).HasColumnName("conversationId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("message_conversationId_fkey");
        });

        modelBuilder.Entity<NoteHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("noteHistory_pkey");

            entity.ToTable("noteHistory");

            entity.HasIndex(e => e.NoteId, "noteHistory_noteId_idx");

            entity.HasIndex(e => e.AccountId, "noteHistory_accountId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.NoteId).HasColumnName("noteId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Note).WithMany(p => p.Histories)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("noteHistory_noteId_fkey");
        });

        modelBuilder.Entity<NoteInternalShare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("noteInternalShare_pkey");

            entity.ToTable("noteInternalShare");

            entity.HasIndex(e => e.NoteId, "noteInternalShare_noteId_idx");

            entity.HasIndex(e => e.AccountId, "noteInternalShare_accountId_idx");

            entity.HasIndex(e => new { e.NoteId, e.AccountId }, "noteInternalShare_noteId_accountId_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.NoteId).HasColumnName("noteId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CanEdit)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("canEdit");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Note).WithMany(p => p.InternalShares)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("noteInternalShare_noteId_fkey");

            entity.HasOne(d => d.Account).WithMany(p => p.SharedNotes)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("noteInternalShare_accountId_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pkey");

            entity.ToTable("session");

            entity.HasIndex(e => e.Sid, "session_sid_key").IsUnique();

            entity.HasIndex(e => e.ExpiresAt, "session_expiresAt_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.ExpiresAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<AiProviders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aiProviders_pkey");

            entity.ToTable("aiProviders");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasColumnName("provider");
            entity.Property(e => e.BaseURL)
                .HasMaxLength(500)
                .HasColumnName("baseURL");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(500)
                .HasColumnName("apiKey");
            entity.Property(e => e.Config)
                .HasColumnType("json")
                .HasColumnName("config");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<AiModels>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aiModels_pkey");

            entity.ToTable("aiModels");

            entity.HasIndex(e => e.ProviderId, "aiModels_providerId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.ProviderId).HasColumnName("providerId");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.ModelKey)
                .HasMaxLength(255)
                .HasColumnName("modelKey");
            entity.Property(e => e.Capabilities)
                .HasColumnType("json")
                .HasColumnName("capabilities");
            entity.Property(e => e.Config)
                .HasColumnType("json")
                .HasColumnName("config");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Provider).WithMany(p => p.Models)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("aiModels_providerId_fkey");
        });

        modelBuilder.Entity<AiScheduledTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aiScheduledTask_pkey");

            entity.ToTable("aiScheduledTask");

            entity.HasIndex(e => e.AccountId, "aiScheduledTask_accountId_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Prompt).HasColumnName("prompt");
            entity.Property(e => e.Schedule)
                .HasMaxLength(100)
                .HasColumnName("schedule");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.LastRun)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("lastRun");
            entity.Property(e => e.LastResult)
                .HasColumnType("json")
                .HasColumnName("lastResult");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Account).WithMany(p => p.AiScheduledTasks)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("aiScheduledTask_accountId_fkey");
        });

        modelBuilder.Entity<McpServers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mcpServers_pkey");

            entity.ToTable("mcpServers");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Command).HasColumnName("command");
            entity.Property(e => e.Args)
                .HasColumnType("json")
                .HasColumnName("args");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");
            entity.Property(e => e.Env)
                .HasColumnType("json")
                .HasColumnName("env");
            entity.Property(e => e.Headers)
                .HasColumnType("json")
                .HasColumnName("headers");
            entity.Property(e => e.IsEnabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("isEnabled");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Fonts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fonts_pkey");

            entity.ToTable("fonts");

            entity.HasIndex(e => e.Name, "fonts_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasColumnName("displayName");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.FileData).HasColumnName("fileData");
            entity.Property(e => e.IsLocal)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isLocal");
            entity.Property(e => e.IsSystem)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("isSystem");
            entity.Property(e => e.Weights)
                .HasColumnType("json")
                .HasColumnName("weights");
            entity.Property(e => e.Category)
                .ValueGeneratedNever()
                .HasMaxLength(50)
                .HasDefaultValue("sans-serif")
                .HasColumnName("category");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("sortOrder");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("updatedAt");
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
        foreach (var entry in ChangeTracker.Entries<Accounts>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Attachments>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Notes>())
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

        foreach (var entry in ChangeTracker.Entries<Tag>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Follows>())
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

        foreach (var entry in ChangeTracker.Entries<Cache>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Plugin>())
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

        foreach (var entry in ChangeTracker.Entries<Message>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<NoteInternalShare>())
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

        foreach (var entry in ChangeTracker.Entries<AiProviders>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AiModels>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AiScheduledTask>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<McpServers>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Fonts>())
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

file sealed class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Utc), v => v)
    {
    }
}

file sealed class UtcClockConverter : ValueConverter<DateTime, DateTime>
{
    public UtcClockConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
