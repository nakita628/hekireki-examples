#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Visus.Cuid;

namespace Teable.Models;

public partial class TeableContext : DbContext
{
    public TeableContext(DbContextOptions<TeableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<DataDbConnection> DataDbConnections { get; set; }

    public virtual DbSet<SpaceDataDbBinding> SpaceDataDbBindings { get; set; }

    public virtual DbSet<SpaceDataDbMigrationJob> SpaceDataDbMigrationJobs { get; set; }

    public virtual DbSet<BaseDataDbMoveJob> BaseDataDbMoveJobs { get; set; }

    public virtual DbSet<PinResource> PinResources { get; set; }

    public virtual DbSet<Base> Bases { get; set; }

    public virtual DbSet<TableMeta> TableMeta { get; set; }

    public virtual DbSet<TableQueryObservationShard> TableQueryObservationShards { get; set; }

    public virtual DbSet<SchemaOperation> SchemaOperations { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<ComputedUpdateOutbox> ComputedUpdateOutboxes { get; set; }

    public virtual DbSet<ComputedUpdateOutboxSeed> ComputedUpdateOutboxSeeds { get; set; }

    public virtual DbSet<ComputedUpdateChangeFrontier> ComputedUpdateChangeFrontiers { get; set; }

    public virtual DbSet<ComputedUpdateStageLedger> ComputedUpdateStageLedgers { get; set; }

    public virtual DbSet<ComputedUpdateDeadLetter> ComputedUpdateDeadLetters { get; set; }

    public virtual DbSet<ComputedUpdateRunHistory> ComputedUpdateRunHistories { get; set; }

    public virtual DbSet<ComputedUpdatePauseScope> ComputedUpdatePauseScopes { get; set; }

    public virtual DbSet<ComputedFieldActivity> ComputedFieldActivities { get; set; }

    public virtual DbSet<ComputedTableActivity> ComputedTableActivities { get; set; }

    public virtual DbSet<ComputedTaskFieldRef> ComputedTaskFieldRefs { get; set; }

    public virtual DbSet<View> Views { get; set; }

    public virtual DbSet<Ops> Ops { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Attachments> Attachments { get; set; }

    public virtual DbSet<AttachmentsTable> AttachmentsTables { get; set; }

    public virtual DbSet<Collaborator> Collaborators { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<InvitationRecord> InvitationRecords { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<OAuthApp> OAuthApps { get; set; }

    public virtual DbSet<OAuthAppAuthorized> OAuthAppAuthorizeds { get; set; }

    public virtual DbSet<OAuthAppSecret> OAuthAppSecrets { get; set; }

    public virtual DbSet<OAuthAppToken> OAuthAppTokens { get; set; }

    public virtual DbSet<RecordHistory> RecordHistories { get; set; }

    public virtual DbSet<Trash> Trashes { get; set; }

    public virtual DbSet<TableTrash> TableTrashes { get; set; }

    public virtual DbSet<RecordTrash> RecordTrashes { get; set; }

    public virtual DbSet<RecordRemovalTombstone> RecordRemovalTombstones { get; set; }

    public virtual DbSet<Plugin> Plugins { get; set; }

    public virtual DbSet<PluginInstall> PluginInstalls { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentSubscription> CommentSubscriptions { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<PluginPanel> PluginPanels { get; set; }

    public virtual DbSet<PluginContextMenu> PluginContextMenus { get; set; }

    public virtual DbSet<UserLastVisit> UserLastVisits { get; set; }

    public virtual DbSet<UserBaseOrder> UserBaseOrders { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<TemplateCategory> TemplateCategories { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskRun> TaskRuns { get; set; }

    public virtual DbSet<TaskReference> TaskReferences { get; set; }

    public virtual DbSet<Waitlist> Waitlists { get; set; }

    public virtual DbSet<BaseNode> BaseNodes { get; set; }

    public virtual DbSet<BaseNodeFolder> BaseNodeFolders { get; set; }

    public virtual DbSet<BaseShare> BaseShares { get; set; }

    public virtual DbSet<ShortLink> ShortLinks { get; set; }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder npgsql)
    {
        npgsql.MapEnum<ProvisionState>("ProvisionState");
        npgsql.MapEnum<DataDbProvider>("DataDbProvider");
        npgsql.MapEnum<DataDbConnectionStatus>("DataDbConnectionStatus");
        npgsql.MapEnum<DataDbConnectionHealthState>("DataDbConnectionHealthState");
        npgsql.MapEnum<SpaceDataDbBindingMode>("SpaceDataDbBindingMode");
        npgsql.MapEnum<SpaceDataDbBindingState>("SpaceDataDbBindingState");
        npgsql.MapEnum<SpaceDataDbMigrationJobState>("SpaceDataDbMigrationJobState");
        npgsql.MapEnum<BaseDataDbMoveJobState>("BaseDataDbMoveJobState");
    }

    public static void MapEnums(NpgsqlDataSourceBuilder dataSource)
    {
        dataSource.MapEnum<ProvisionState>("ProvisionState");
        dataSource.MapEnum<DataDbProvider>("DataDbProvider");
        dataSource.MapEnum<DataDbConnectionStatus>("DataDbConnectionStatus");
        dataSource.MapEnum<DataDbConnectionHealthState>("DataDbConnectionHealthState");
        dataSource.MapEnum<SpaceDataDbBindingMode>("SpaceDataDbBindingMode");
        dataSource.MapEnum<SpaceDataDbBindingState>("SpaceDataDbBindingState");
        dataSource.MapEnum<SpaceDataDbMigrationJobState>("SpaceDataDbMigrationJobState");
        dataSource.MapEnum<BaseDataDbMoveJobState>("BaseDataDbMoveJobState");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        ConfigureConventionsPartial(configurationBuilder);
    }

    partial void ConfigureConventionsPartial(ModelConfigurationBuilder configurationBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProvisionState>(name: "ProvisionState");
        modelBuilder.HasPostgresEnum<DataDbProvider>(name: "DataDbProvider");
        modelBuilder.HasPostgresEnum<DataDbConnectionStatus>(name: "DataDbConnectionStatus");
        modelBuilder.HasPostgresEnum<DataDbConnectionHealthState>(name: "DataDbConnectionHealthState");
        modelBuilder.HasPostgresEnum<SpaceDataDbBindingMode>(name: "SpaceDataDbBindingMode");
        modelBuilder.HasPostgresEnum<SpaceDataDbBindingState>(name: "SpaceDataDbBindingState");
        modelBuilder.HasPostgresEnum<SpaceDataDbMigrationJobState>(name: "SpaceDataDbMigrationJobState");
        modelBuilder.HasPostgresEnum<BaseDataDbMoveJobState>(name: "BaseDataDbMoveJobState");

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_pkey");

            entity.ToTable("space");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.IsTemplate).HasColumnName("is_template");
        });

        modelBuilder.Entity<DataDbConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("data_db_connection_pkey");

            entity.ToTable("data_db_connection");

            entity.HasIndex(e => e.UrlFingerprint, "data_db_connection_url_fingerprint_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Provider)
                .ValueGeneratedNever()
                .HasDefaultValue(DataDbProvider.Postgres)
                .HasColumnName("provider");
            entity.Property(e => e.EncryptedUrl).HasColumnName("encrypted_url");
            entity.Property(e => e.UrlFingerprint).HasColumnName("url_fingerprint");
            entity.Property(e => e.DisplayHost).HasColumnName("display_host");
            entity.Property(e => e.DisplayDatabase).HasColumnName("display_database");
            entity.Property(e => e.InternalSchema).HasColumnName("internal_schema");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue(DataDbConnectionStatus.Pending)
                .HasColumnName("status");
            entity.Property(e => e.SchemaVersion).HasColumnName("schema_version");
            entity.Property(e => e.Capabilities)
                .HasColumnType("jsonb")
                .HasColumnName("capabilities");
            entity.Property(e => e.LastValidatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_validated_at");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.HealthState)
                .ValueGeneratedNever()
                .HasDefaultValue(DataDbConnectionHealthState.Healthy)
                .HasColumnName("health_state");
            entity.Property(e => e.HealthReason).HasColumnName("health_reason");
            entity.Property(e => e.HealthChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("health_changed_at");
            entity.Property(e => e.LastHealthCheckAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_health_check_at");
            entity.Property(e => e.ConsecutiveHealthFailures)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("consecutive_health_failures");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<SpaceDataDbBinding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_data_db_binding_pkey");

            entity.ToTable("space_data_db_binding");

            entity.HasIndex(e => e.SpaceId, "space_data_db_binding_space_id_key").IsUnique();

            entity.HasIndex(e => e.DataDbConnectionId, "space_data_db_binding_data_db_connection_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.DataDbConnectionId).HasColumnName("data_db_connection_id");
            entity.Property(e => e.Mode)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceDataDbBindingMode.Default)
                .HasColumnName("mode");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceDataDbBindingState.Ready)
                .HasColumnName("state");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastHistoryFlushedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_history_flushed_at");
            entity.Property(e => e.LastRemovalFlushedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_removal_flushed_at");

            entity.HasOne(d => d.Space).WithOne(p => p.DataDbBinding)
                .HasForeignKey<SpaceDataDbBinding>(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_data_db_binding_space_id_fkey");

            entity.HasOne(d => d.DataDbConnection).WithMany(p => p.SpaceBindings)
                .HasForeignKey(d => d.DataDbConnectionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("space_data_db_binding_data_db_connection_id_fkey");
        });

        modelBuilder.Entity<SpaceDataDbMigrationJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("space_data_db_migration_job_pkey");

            entity.ToTable("space_data_db_migration_job");

            entity.HasIndex(e => new { e.SpaceId, e.State }, "space_data_db_migration_job_space_id_state_idx");

            entity.HasIndex(e => e.SourceConnectionId, "space_data_db_migration_job_source_connection_id_idx");

            entity.HasIndex(e => e.TargetConnectionId, "space_data_db_migration_job_target_connection_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.SourceConnectionId).HasColumnName("source_connection_id");
            entity.Property(e => e.TargetConnectionId).HasColumnName("target_connection_id");
            entity.Property(e => e.TargetMode)
                .ValueGeneratedNever()
                .HasDefaultValue("migrate-space")
                .HasColumnName("target_mode");
            entity.Property(e => e.SwitchOnCompletion)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("switch_on_completion");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(SpaceDataDbMigrationJobState.Pending)
                .HasColumnName("state");
            entity.Property(e => e.TargetUrlFingerprint).HasColumnName("target_url_fingerprint");
            entity.Property(e => e.TargetInternalSchema).HasColumnName("target_internal_schema");
            entity.Property(e => e.Inventory)
                .HasColumnType("jsonb")
                .HasColumnName("inventory");
            entity.Property(e => e.CopyStats)
                .HasColumnType("jsonb")
                .HasColumnName("copy_stats");
            entity.Property(e => e.ValidationStats)
                .HasColumnType("jsonb")
                .HasColumnName("validation_stats");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.Space).WithMany(p => p.DataDbMigrationJobs)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("space_data_db_migration_job_space_id_fkey");

            entity.HasOne(d => d.SourceConnection).WithMany(p => p.SourceMigrationJobs)
                .HasForeignKey(d => d.SourceConnectionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("space_data_db_migration_job_source_connection_id_fkey");

            entity.HasOne(d => d.TargetConnection).WithMany(p => p.TargetMigrationJobs)
                .HasForeignKey(d => d.TargetConnectionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("space_data_db_migration_job_target_connection_id_fkey");
        });

        modelBuilder.Entity<BaseDataDbMoveJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_data_db_move_job_pkey");

            entity.ToTable("base_data_db_move_job");

            entity.HasIndex(e => new { e.BaseId, e.State }, "base_data_db_move_job_base_id_state_idx");

            entity.HasIndex(e => e.State, "base_data_db_move_job_state_idx");

            entity.HasIndex(e => new { e.SourceSpaceId, e.State }, "base_data_db_move_job_source_space_id_state_idx");

            entity.HasIndex(e => new { e.TargetSpaceId, e.State }, "base_data_db_move_job_target_space_id_state_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SourceSpaceId).HasColumnName("source_space_id");
            entity.Property(e => e.TargetSpaceId).HasColumnName("target_space_id");
            entity.Property(e => e.SourceConnectionId).HasColumnName("source_connection_id");
            entity.Property(e => e.TargetConnectionId).HasColumnName("target_connection_id");
            entity.Property(e => e.State)
                .ValueGeneratedNever()
                .HasDefaultValue(BaseDataDbMoveJobState.Pending)
                .HasColumnName("state");
            entity.Property(e => e.Inventory)
                .HasColumnType("jsonb")
                .HasColumnName("inventory");
            entity.Property(e => e.CopyStats)
                .HasColumnType("jsonb")
                .HasColumnName("copy_stats");
            entity.Property(e => e.ValidationStats)
                .HasColumnType("jsonb")
                .HasColumnName("validation_stats");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.Base).WithMany(p => p.DataDbMoveJobs)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("base_data_db_move_job_base_id_fkey");

            entity.HasOne(d => d.SourceSpace).WithMany(p => p.BaseDataDbMoveJobsAsSource)
                .HasForeignKey(d => d.SourceSpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("base_data_db_move_job_source_space_id_fkey");

            entity.HasOne(d => d.TargetSpace).WithMany(p => p.BaseDataDbMoveJobsAsTarget)
                .HasForeignKey(d => d.TargetSpaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("base_data_db_move_job_target_space_id_fkey");
        });

        modelBuilder.Entity<PinResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pin_resource_pkey");

            entity.ToTable("pin_resource");

            entity.HasIndex(e => e.Order, "pin_resource_order_idx");

            entity.HasIndex(e => new { e.CreatedBy, e.ResourceId }, "pin_resource_created_by_resource_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Base>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_pkey");

            entity.ToTable("base");

            entity.HasIndex(e => e.Order, "base_order_idx");

            entity.HasIndex(e => e.SpaceId, "base_space_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.SchemaPass).HasColumnName("schema_pass");
            entity.Property(e => e.V2Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("v2_enabled");
            entity.Property(e => e.ProvisionState)
                .ValueGeneratedNever()
                .HasDefaultValue(ProvisionState.Ready)
                .HasColumnName("provision_state");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.Space).WithMany(p => p.BaseGroup)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("base_space_id_fkey");
        });

        modelBuilder.Entity<TableMeta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("table_meta_pkey");

            entity.ToTable("table_meta");

            entity.HasIndex(e => e.Order, "table_meta_order_idx");

            entity.HasIndex(e => e.DbTableName, "table_meta_db_table_name_idx");

            entity.HasIndex(e => new { e.BaseId, e.DeletedTime }, "table_meta_base_id_deleted_time_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.DbTableName).HasColumnName("db_table_name");
            entity.Property(e => e.DbViewName).HasColumnName("db_view_name");
            entity.Property(e => e.SearchIndex)
                .HasColumnType("jsonb")
                .HasColumnName("search_index");
            entity.Property(e => e.SearchIndexPolicy)
                .HasColumnType("jsonb")
                .HasColumnName("search_index_policy");
            entity.Property(e => e.QueryIndexPolicy)
                .HasColumnType("jsonb")
                .HasColumnName("query_index_policy");
            entity.Property(e => e.ProvisionState)
                .ValueGeneratedNever()
                .HasDefaultValue(ProvisionState.Ready)
                .HasColumnName("provision_state");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Base).WithMany(p => p.Tables)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("table_meta_base_id_fkey");
        });

        modelBuilder.Entity<TableQueryObservationShard>(entity =>
        {
            entity.HasKey(e => new { e.TableId, e.QueryKind, e.ShapeHash, e.WindowStart, e.WriterId }).HasName("table_query_observation_shard_pkey");

            entity.ToTable("table_query_observation_shard");

            entity.HasIndex(e => e.WindowStart, "table_query_observation_shard_window_start_idx");

            entity.HasIndex(e => new { e.TableId, e.WindowStart }, "table_query_observation_shard_table_start_idx");

            entity.HasIndex(e => new { e.QueryKind, e.TableId, e.WindowStart }, "table_query_observation_shard_search_activity_idx");

            entity.HasIndex(e => new { e.BaseId, e.WindowStart }, "table_query_observation_shard_base_start_idx");

            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.QueryKind).HasColumnName("query_kind");
            entity.Property(e => e.ShapeHash).HasColumnName("shape_hash");
            entity.Property(e => e.WindowStart)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("window_start");
            entity.Property(e => e.WriterId).HasColumnName("writer_id");
            entity.Property(e => e.WindowSizeSeconds).HasColumnName("window_size_seconds");
            entity.Property(e => e.RequestCount).HasColumnName("request_count");
            entity.Property(e => e.SlowCount).HasColumnName("slow_count");
            entity.Property(e => e.TimeoutCount).HasColumnName("timeout_count");
            entity.Property(e => e.DbErrorCount).HasColumnName("db_error_count");
            entity.Property(e => e.TotalDurationMs).HasColumnName("total_duration_ms");
            entity.Property(e => e.MaxDurationMs).HasColumnName("max_duration_ms");
            entity.Property(e => e.TotalDbDurationMs).HasColumnName("total_db_duration_ms");
            entity.Property(e => e.MaxDbDurationMs).HasColumnName("max_db_duration_ms");
            entity.Property(e => e.Shape)
                .HasColumnType("jsonb")
                .HasColumnName("shape");
            entity.Property(e => e.SqlDiagnostics)
                .HasColumnType("jsonb")
                .HasColumnName("sql_diagnostics");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<SchemaOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("schema_operation_pkey");

            entity.ToTable("schema_operation");

            entity.HasIndex(e => e.IdempotencyKey, "schema_operation_idempotency_key_key").IsUnique();

            entity.HasIndex(e => new { e.Status, e.NextRunAt }, "schema_operation_status_next_run_at_idx");

            entity.HasIndex(e => new { e.ResourceType, e.ResourceId, e.Status }, "schema_operation_resource_status_idx");

            entity.HasIndex(e => new { e.BaseId, e.Status }, "schema_operation_base_status_idx");

            entity.HasIndex(e => new { e.TableId, e.Status }, "schema_operation_table_status_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Phase).HasColumnName("phase");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.IdempotencyKey).HasColumnName("idempotency_key");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Result)
                .HasColumnType("jsonb")
                .HasColumnName("result");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.MaxAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(8)
                .HasColumnName("max_attempts");
            entity.Property(e => e.NextRunAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("next_run_at");
            entity.Property(e => e.LockedAt)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("locked_at");
            entity.Property(e => e.LockedBy).HasColumnName("locked_by");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcConverter>()
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasPrecision(6)
                .HasConversion<UtcConverter>()
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("field_pkey");

            entity.ToTable("field");

            entity.HasIndex(e => e.LookupLinkedFieldId, "field_lookup_linked_field_id_idx");

            entity.HasIndex(e => new { e.TableId, e.DeletedTime }, "field_table_id_deleted_time_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Options).HasColumnName("options");
            entity.Property(e => e.Meta).HasColumnName("meta");
            entity.Property(e => e.AiConfig).HasColumnName("ai_config");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CellValueType).HasColumnName("cell_value_type");
            entity.Property(e => e.IsMultipleCellValue).HasColumnName("is_multiple_cell_value");
            entity.Property(e => e.DbFieldType).HasColumnName("db_field_type");
            entity.Property(e => e.DbFieldName).HasColumnName("db_field_name");
            entity.Property(e => e.ProvisionState)
                .ValueGeneratedNever()
                .HasDefaultValue(ProvisionState.Ready)
                .HasColumnName("provision_state");
            entity.Property(e => e.NotNull).HasColumnName("not_null");
            entity.Property(e => e.Unique).HasColumnName("unique");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
            entity.Property(e => e.IsComputed).HasColumnName("is_computed");
            entity.Property(e => e.IsLookup).HasColumnName("is_lookup");
            entity.Property(e => e.IsConditionalLookup).HasColumnName("is_conditional_lookup");
            entity.Property(e => e.IsPending).HasColumnName("is_pending");
            entity.Property(e => e.HasError).HasColumnName("has_error");
            entity.Property(e => e.LookupLinkedFieldId).HasColumnName("lookup_linked_field_id");
            entity.Property(e => e.LookupOptions).HasColumnName("lookup_options");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Table).WithMany(p => p.Fields)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("field_table_id_fkey");
        });

        modelBuilder.Entity<ComputedUpdateOutbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computed_update_outbox_pkey");

            entity.ToTable("computed_update_outbox");

            entity.HasIndex(e => new { e.Status, e.NextRunAt }, "computed_update_outbox_status_next_run_at_idx");

            entity.HasIndex(e => new { e.BaseId, e.SeedTableId }, "computed_update_outbox_base_id_seed_table_id_idx");

            entity.HasIndex(e => e.PlanHash, "computed_update_outbox_plan_hash_idx");

            entity.HasIndex(e => e.RunId, "computed_update_outbox_run_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SeedTableId).HasColumnName("seed_table_id");
            entity.Property(e => e.SeedRecordIds)
                .HasColumnType("jsonb")
                .HasColumnName("seed_record_ids");
            entity.Property(e => e.ChangeType).HasColumnName("change_type");
            entity.Property(e => e.Steps)
                .HasColumnType("jsonb")
                .HasColumnName("steps");
            entity.Property(e => e.Edges)
                .HasColumnType("jsonb")
                .HasColumnName("edges");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.MaxAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(8)
                .HasColumnName("max_attempts");
            entity.Property(e => e.NextRunAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_run_at");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("locked_at");
            entity.Property(e => e.LockedBy).HasColumnName("locked_by");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.EstimatedComplexity)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("estimated_complexity");
            entity.Property(e => e.PlanHash).HasColumnName("plan_hash");
            entity.Property(e => e.DirtyStats)
                .HasColumnType("jsonb")
                .HasColumnName("dirty_stats");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.OriginRunIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("origin_run_ids");
            entity.Property(e => e.RunTotalSteps)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_total_steps");
            entity.Property(e => e.RunCompletedStepsBefore)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_completed_steps_before");
            entity.Property(e => e.AffectedTableIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_table_ids");
            entity.Property(e => e.AffectedFieldIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_field_ids");
            entity.Property(e => e.SyncMaxLevel).HasColumnName("sync_max_level");
            entity.Property(e => e.SourceChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("source_changed_at");
            entity.Property(e => e.StageDepth)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("stage_depth");
            entity.Property(e => e.PredecessorTaskId).HasColumnName("predecessor_task_id");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ComputedUpdateOutboxSeed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computed_update_outbox_seed_pkey");

            entity.ToTable("computed_update_outbox_seed");

            entity.HasIndex(e => e.TaskId, "computed_update_outbox_seed_task_id_idx");

            entity.HasIndex(e => new { e.TaskId, e.TableId, e.RecordId }, "computed_update_outbox_seed_task_id_table_id_record_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Seeds)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("computed_update_outbox_seed_task_id_fkey");
        });

        modelBuilder.Entity<ComputedUpdateChangeFrontier>(entity =>
        {
            entity.HasKey(e => new { e.ScopeId, e.Kind, e.TableId, e.RecordId, e.FieldId }).HasName("computed_update_change_frontier_pkey");

            entity.ToTable("computed_update_change_frontier");

            entity.Property(e => e.ScopeId).HasColumnName("scope_id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
        });

        modelBuilder.Entity<ComputedUpdateStageLedger>(entity =>
        {
            entity.HasKey(e => new { e.ScopeId, e.Kind, e.TableId, e.RecordId }).HasName("computed_update_stage_ledger_pkey");

            entity.ToTable("computed_update_stage_ledger");

            entity.HasIndex(e => new { e.ScopeId, e.Kind, e.Seq }, "computed_update_stage_ledger_scope_id_kind_seq_idx");

            entity.Property(e => e.ScopeId).HasColumnName("scope_id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Seq)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("seq");
        });

        modelBuilder.Entity<ComputedUpdateDeadLetter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computed_update_dead_letter_pkey");

            entity.ToTable("computed_update_dead_letter");

            entity.HasIndex(e => new { e.BaseId, e.SeedTableId }, "computed_update_dead_letter_base_id_seed_table_id_idx");

            entity.HasIndex(e => e.PlanHash, "computed_update_dead_letter_plan_hash_idx");

            entity.HasIndex(e => e.RunId, "computed_update_dead_letter_run_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SeedTableId).HasColumnName("seed_table_id");
            entity.Property(e => e.SeedRecordIds)
                .HasColumnType("jsonb")
                .HasColumnName("seed_record_ids");
            entity.Property(e => e.ChangeType).HasColumnName("change_type");
            entity.Property(e => e.Steps)
                .HasColumnType("jsonb")
                .HasColumnName("steps");
            entity.Property(e => e.Edges)
                .HasColumnType("jsonb")
                .HasColumnName("edges");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.MaxAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(8)
                .HasColumnName("max_attempts");
            entity.Property(e => e.NextRunAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("next_run_at");
            entity.Property(e => e.LockedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("locked_at");
            entity.Property(e => e.LockedBy).HasColumnName("locked_by");
            entity.Property(e => e.LastError).HasColumnName("last_error");
            entity.Property(e => e.EstimatedComplexity)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("estimated_complexity");
            entity.Property(e => e.PlanHash).HasColumnName("plan_hash");
            entity.Property(e => e.DirtyStats)
                .HasColumnType("jsonb")
                .HasColumnName("dirty_stats");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.OriginRunIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("origin_run_ids");
            entity.Property(e => e.RunTotalSteps)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_total_steps");
            entity.Property(e => e.RunCompletedStepsBefore)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_completed_steps_before");
            entity.Property(e => e.AffectedTableIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_table_ids");
            entity.Property(e => e.AffectedFieldIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_field_ids");
            entity.Property(e => e.SyncMaxLevel).HasColumnName("sync_max_level");
            entity.Property(e => e.SourceChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("source_changed_at");
            entity.Property(e => e.StageDepth)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("stage_depth");
            entity.Property(e => e.PredecessorTaskId).HasColumnName("predecessor_task_id");
            entity.Property(e => e.TraceData)
                .HasColumnType("jsonb")
                .HasColumnName("trace_data");
            entity.Property(e => e.FailedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("failed_at");
            entity.Property(e => e.CreatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ComputedUpdateRunHistory>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("computed_update_run_history_pkey");

            entity.ToTable("computed_update_run_history");

            entity.HasIndex(e => e.RunId, "computed_update_run_history_run_id_idx");

            entity.HasIndex(e => new { e.BaseId, e.CompletedAt }, "computed_update_run_history_base_id_completed_at_idx");

            entity.HasIndex(e => e.CompletedAt, "computed_update_run_history_completed_at_idx");

            entity.HasIndex(e => e.OriginRunIds, "computed_update_run_history_origin_run_ids_gin").HasMethod("gin");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SeedTableId).HasColumnName("seed_table_id");
            entity.Property(e => e.ChangeType).HasColumnName("change_type");
            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.OriginRunIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("origin_run_ids");
            entity.Property(e => e.Steps)
                .HasColumnType("jsonb")
                .HasColumnName("steps");
            entity.Property(e => e.Edges)
                .HasColumnType("jsonb")
                .HasColumnName("edges");
            entity.Property(e => e.AffectedTableIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_table_ids");
            entity.Property(e => e.AffectedFieldIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("affected_field_ids");
            entity.Property(e => e.SourceFieldIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("source_field_ids");
            entity.Property(e => e.SeedRecordCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("seed_record_count");
            entity.Property(e => e.StageDepth)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("stage_depth");
            entity.Property(e => e.PredecessorTaskId).HasColumnName("predecessor_task_id");
            entity.Property(e => e.RunTotalSteps)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_total_steps");
            entity.Property(e => e.RunCompletedStepsBefore)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("run_completed_steps_before");
            entity.Property(e => e.SyncMaxLevel).HasColumnName("sync_max_level");
            entity.Property(e => e.EstimatedComplexity)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("estimated_complexity");
            entity.Property(e => e.Attempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("attempts");
            entity.Property(e => e.Outcome).HasColumnName("outcome");
            entity.Property(e => e.SourceChangedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("source_changed_at");
            entity.Property(e => e.EnqueuedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("enqueued_at");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.CompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.DurationMs)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("duration_ms");
        });

        modelBuilder.Entity<ComputedUpdatePauseScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("computed_update_pause_scope_pkey");

            entity.ToTable("computed_update_pause_scope");

            entity.HasIndex(e => e.ResumeAt, "computed_update_pause_scope_resume_at_idx");

            entity.HasIndex(e => new { e.ScopeType, e.ScopeId }, "computed_update_pause_scope_scope_type_scope_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ScopeType).HasColumnName("scope_type");
            entity.Property(e => e.ScopeId).HasColumnName("scope_id");
            entity.Property(e => e.PausedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("paused_at");
            entity.Property(e => e.PausedBy).HasColumnName("paused_by");
            entity.Property(e => e.ResumeAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("resume_at");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.WritePolicy)
                .ValueGeneratedNever()
                .HasDefaultValue("allow_bounded")
                .HasColumnName("write_policy");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<ComputedFieldActivity>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("computed_field_activity_pkey");

            entity.ToTable("computed_field_activity");

            entity.HasIndex(e => new { e.TableId, e.Status }, "computed_field_activity_table_id_status_idx");

            entity.HasIndex(e => new { e.BaseId, e.Status }, "computed_field_activity_base_id_status_idx");

            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ActiveTaskCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("active_task_count");
            entity.Property(e => e.ProcessingTaskCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("processing_task_count");
            entity.Property(e => e.Generation)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("generation");
            entity.Property(e => e.EstimatedComplexity)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("estimated_complexity");
            entity.Property(e => e.EstimatedDirtyRecords)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("estimated_dirty_records");
            entity.Property(e => e.HasAllTargetRecords)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("has_all_target_records");
            entity.Property(e => e.QueuedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("queued_at");
            entity.Property(e => e.StartedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.LastCompletedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_completed_at");
            entity.Property(e => e.LastDurationMs).HasColumnName("last_duration_ms");
            entity.Property(e => e.LastError)
                .HasColumnType("jsonb")
                .HasColumnName("last_error");
            entity.Property(e => e.Extensions)
                .HasColumnType("jsonb")
                .HasColumnName("extensions");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ComputedTableActivity>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("computed_table_activity_pkey");

            entity.ToTable("computed_table_activity");

            entity.HasIndex(e => new { e.BaseId, e.Status }, "computed_table_activity_base_id_status_idx");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CalculatingFieldCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("calculating_field_count");
            entity.Property(e => e.QueuedFieldCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("queued_field_count");
            entity.Property(e => e.EstimatedComplexity)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("estimated_complexity");
            entity.Property(e => e.RecentCompletions)
                .ValueGeneratedNever()
                .HasDefaultValue("[]")
                .HasColumnType("jsonb")
                .HasColumnName("recent_completions");
            entity.Property(e => e.Generation)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("generation");
            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ComputedTaskFieldRef>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.FieldId }).HasName("computed_task_field_ref_pkey");

            entity.ToTable("computed_task_field_ref");

            entity.HasIndex(e => e.FieldId, "computed_task_field_ref_field_id_idx");

            entity.HasIndex(e => e.TableId, "computed_task_field_ref_table_id_idx");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.WasProcessing)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("was_processing");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("view_pkey");

            entity.ToTable("view");

            entity.HasIndex(e => e.ShareId, "view_share_id_key").IsUnique();

            entity.HasIndex(e => e.Order, "view_order_idx");

            entity.HasIndex(e => new { e.TableId, e.DeletedTime }, "view_table_id_deleted_time_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Filter).HasColumnName("filter");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.Options).HasColumnName("options");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.ColumnMeta).HasColumnName("column_meta");
            entity.Property(e => e.IsLocked).HasColumnName("is_locked");
            entity.Property(e => e.EnableShare).HasColumnName("enable_share");
            entity.Property(e => e.ShareId).HasColumnName("share_id");
            entity.Property(e => e.ShareMeta).HasColumnName("share_meta");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Table).WithMany(p => p.Views)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("view_table_id_fkey");
        });

        modelBuilder.Entity<Ops>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ops_pkey");

            entity.ToTable("ops");

            entity.HasIndex(e => new { e.Collection, e.CreatedTime }, "ops_collection_created_time_idx");

            entity.HasIndex(e => new { e.Collection, e.DocId, e.Version }, "ops_collection_doc_id_version_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Collection).HasColumnName("collection");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.DocType).HasColumnName("doc_type");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Operation).HasColumnName("operation");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reference_pkey");

            entity.ToTable("reference");

            entity.HasIndex(e => e.FromFieldId, "reference_from_field_id_idx");

            entity.HasIndex(e => e.ToFieldId, "reference_to_field_id_idx");

            entity.HasIndex(e => new { e.ToFieldId, e.FromFieldId }, "reference_to_field_id_from_field_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FromFieldId).HasColumnName("from_field_id");
            entity.Property(e => e.ToFieldId).HasColumnName("to_field_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Phone, "users_phone_key").IsUnique();

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.IsSystem).HasColumnName("is_system");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.IsTrialUsed).HasColumnName("is_trial_used");
            entity.Property(e => e.Lang).HasColumnName("lang");
            entity.Property(e => e.NotifyMeta).HasColumnName("notify_meta");
            entity.Property(e => e.LastSignTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_sign_time");
            entity.Property(e => e.DeactivatedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deactivated_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.PermanentDeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("permanent_deleted_time");
            entity.Property(e => e.RefMeta).HasColumnName("ref_meta");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => new { e.Provider, e.ProviderId, e.ConnectionId }, "account_provider_provider_id_connection_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.ConnectionId)
                .ValueGeneratedNever()
                .HasDefaultValue("")
                .HasColumnName("connection_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("account_user_id_fkey");
        });

        modelBuilder.Entity<Attachments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.HasIndex(e => e.Token, "attachments_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Mimetype).HasColumnName("mimetype");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.ThumbnailPath).HasColumnName("thumbnail_path");
        });

        modelBuilder.Entity<AttachmentsTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attachments_table_pkey");

            entity.ToTable("attachments_table");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "attachments_table_table_id_record_id_idx");

            entity.HasIndex(e => new { e.TableId, e.FieldId }, "attachments_table_table_id_field_id_idx");

            entity.HasIndex(e => e.AttachmentId, "attachments_table_attachment_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<Collaborator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("collaborator_pkey");

            entity.ToTable("collaborator");

            entity.HasIndex(e => e.ResourceId, "collaborator_resource_id_idx");

            entity.HasIndex(e => e.PrincipalId, "collaborator_principal_id_idx");

            entity.HasIndex(e => new { e.ResourceType, e.ResourceId, e.PrincipalId, e.PrincipalType }, "collaborator_resource_type_resource_id_principal_id_princip_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.PrincipalId).HasColumnName("principal_id");
            entity.Property(e => e.PrincipalType).HasColumnName("principal_type");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invitation_pkey");

            entity.ToTable("invitation");

            entity.HasIndex(e => e.BaseId, "invitation_base_id_idx");

            entity.HasIndex(e => e.SpaceId, "invitation_space_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.InvitationCode).HasColumnName("invitation_code");
            entity.Property(e => e.ExpiredTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expired_time");
            entity.Property(e => e.CreatedBy).HasColumnName("create_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
        });

        modelBuilder.Entity<InvitationRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invitation_record_pkey");

            entity.ToTable("invitation_record");

            entity.HasIndex(e => e.InvitationId, "invitation_record_invitation_id_idx");

            entity.HasIndex(e => e.BaseId, "invitation_record_base_id_idx");

            entity.HasIndex(e => e.SpaceId, "invitation_record_space_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.InvitationId).HasColumnName("invitation_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Inviter).HasColumnName("inviter");
            entity.Property(e => e.Accepter).HasColumnName("accepter");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_pkey");

            entity.ToTable("notification");

            entity.HasIndex(e => new { e.ToUserId, e.IsRead, e.CreatedTime }, "notification_to_user_id_is_read_created_time_idx");

            entity.HasIndex(e => e.CreatedTime, "notification_created_time_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FromUserId).HasColumnName("from_user_id");
            entity.Property(e => e.ToUserId).HasColumnName("to_user_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageI18n).HasColumnName("message_i18n");
            entity.Property(e => e.Severity)
                .ValueGeneratedNever()
                .HasDefaultValue("info")
                .HasColumnName("severity");
            entity.Property(e => e.UrlPath).HasColumnName("url_path");
            entity.Property(e => e.IsRead)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        });

        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("access_token_pkey");

            entity.ToTable("access_token");

            entity.HasIndex(e => e.UserId, "access_token_user_id_idx");

            entity.HasIndex(e => e.ClientId, "access_token_client_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.SpaceIds).HasColumnName("space_ids");
            entity.Property(e => e.BaseIds).HasColumnName("base_ids");
            entity.Property(e => e.Sign).HasColumnName("sign");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.HasFullAccess).HasColumnName("has_full_access");
            entity.Property(e => e.ExpiredTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expired_time");
            entity.Property(e => e.LastUsedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("setting_name_key");

            entity.ToTable("setting");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<OAuthApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_app_pkey");

            entity.ToTable("oauth_app");

            entity.HasAlternateKey(e => e.ClientId).HasName("oauth_app_client_id_key");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Homepage).HasColumnName("homepage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.RedirectUris).HasColumnName("redirect_uris");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.AllowDeviceFlow)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("allow_device_flow");
            entity.Property(e => e.DynamicRegistration)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("dynamic_registration");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        });

        modelBuilder.Entity<OAuthAppAuthorized>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_app_authorized_pkey");

            entity.ToTable("oauth_app_authorized");

            entity.HasIndex(e => new { e.ClientId, e.UserId }, "oauth_app_authorized_client_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AuthorizedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("authorized_time");

            entity.HasOne(d => d.OAuthApp).WithMany(p => p.Authorized)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_app_authorized_client_id_fkey");
        });

        modelBuilder.Entity<OAuthAppSecret>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_app_secret_pkey");

            entity.ToTable("oauth_app_secret");

            entity.HasIndex(e => e.Secret, "oauth_app_secret_secret_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.MaskedSecret).HasColumnName("masked_secret");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastUsedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_time");

            entity.HasOne(d => d.OAuthApp).WithMany(p => p.Secrets)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_app_secret_client_id_fkey");
        });

        modelBuilder.Entity<OAuthAppToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_app_token_pkey");

            entity.ToTable("oauth_app_token");

            entity.HasIndex(e => e.RefreshTokenSign, "oauth_app_token_refresh_token_sign_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "oauth_app_token_client_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.AppSecretId).HasColumnName("app_secret_id");
            entity.Property(e => e.RefreshTokenSign).HasColumnName("refresh_token_sign");
            entity.Property(e => e.ExpiredTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expired_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");

            entity.HasOne(d => d.OAuthAppSecret).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.AppSecretId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_app_token_app_secret_id_fkey");

            entity.HasOne(d => d.OAuthApp).WithMany(p => p.Tokens)
                .HasPrincipalKey(p => p.ClientId)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_app_token_client_id_fkey");
        });

        modelBuilder.Entity<RecordHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_history_pkey");

            entity.ToTable("record_history");

            entity.HasIndex(e => new { e.TableId, e.RecordId, e.CreatedTime }, "record_history_table_id_record_id_created_time_idx");

            entity.HasIndex(e => new { e.TableId, e.CreatedTime }, "record_history_table_id_created_time_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.Before).HasColumnName("before");
            entity.Property(e => e.After).HasColumnName("after");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        });

        modelBuilder.Entity<Trash>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trash_pkey");

            entity.ToTable("trash");

            entity.HasIndex(e => new { e.ResourceType, e.ResourceId }, "trash_resource_type_resource_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.DeletedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
        });

        modelBuilder.Entity<TableTrash>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("table_trash_pkey");

            entity.ToTable("table_trash");

            entity.HasIndex(e => e.TableId, "table_trash_table_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        });

        modelBuilder.Entity<RecordTrash>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_trash_pkey");

            entity.ToTable("record_trash");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "record_trash_table_id_record_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Reason)
                .ValueGeneratedNever()
                .HasDefaultValue("deleted")
                .HasColumnName("reason");
            entity.Property(e => e.RecordCreatedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("record_created_time");
            entity.Property(e => e.RecordCreatedBy).HasColumnName("record_created_by");
            entity.Property(e => e.RecordLastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("record_last_modified_time");
            entity.Property(e => e.RecordLastModifiedBy).HasColumnName("record_last_modified_by");
            entity.Property(e => e.OperationId).HasColumnName("operation_id");
        });

        modelBuilder.Entity<RecordRemovalTombstone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("record_removal_tombstone_pkey");

            entity.ToTable("record_removal_tombstone");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "record_removal_tombstone_table_id_record_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<Plugin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plugin_pkey");

            entity.ToTable("plugin");

            entity.HasIndex(e => e.Secret, "plugin_secret_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DetailDesc).HasColumnName("detail_desc");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.HelpUrl).HasColumnName("help_url");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Positions).HasColumnName("positions");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.MaskedSecret).HasColumnName("masked_secret");
            entity.Property(e => e.I18n).HasColumnName("i18n");
            entity.Property(e => e.Config).HasColumnName("config");
            entity.Property(e => e.PluginUser).HasColumnName("plugin_user");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<PluginInstall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plugin_install_pkey");

            entity.ToTable("plugin_install");

            entity.HasIndex(e => e.PositionId, "plugin_install_position_id_idx");

            entity.HasIndex(e => e.BaseId, "plugin_install_base_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.PluginId).HasColumnName("plugin_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.Storage).HasColumnName("storage");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Plugin).WithMany(p => p.PluginInstall)
                .HasForeignKey(d => d.PluginId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plugin_install_plugin_id_fkey");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dashboard_pkey");

            entity.ToTable("dashboard");

            entity.HasIndex(e => e.BaseId, "dashboard_base_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Layout).HasColumnName("layout");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pkey");

            entity.ToTable("comment");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "comment_table_id_record_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.QuoteId).HasColumnName("quote_Id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Reaction).HasColumnName("reaction");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<CommentSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_subscription_pkey");

            entity.ToTable("comment_subscription");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "comment_subscription_table_id_record_id_idx");

            entity.HasIndex(e => new { e.TableId, e.RecordId }, "comment_subscription_table_id_record_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("integration_pkey");

            entity.ToTable("integration");

            entity.HasIndex(e => e.ResourceId, "integration_resource_id_key").IsUnique();

            entity.HasIndex(e => e.ResourceId, "integration_resource_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Config).HasColumnName("config");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Enable).HasColumnName("enable");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<PluginPanel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plugin_panel_pkey");

            entity.ToTable("plugin_panel");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Layout).HasColumnName("layout");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Table).WithMany(p => p.PluginPanel)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plugin_panel_table_id_fkey");
        });

        modelBuilder.Entity<PluginContextMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plugin_context_menu_pkey");

            entity.ToTable("plugin_context_menu");

            entity.HasIndex(e => e.PluginInstallId, "plugin_context_menu_plugin_install_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.PluginInstallId).HasColumnName("plugin_install_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Table).WithMany(p => p.PluginContextMenu)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plugin_context_menu_table_id_fkey");
        });

        modelBuilder.Entity<UserLastVisit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_last_visit_pkey");

            entity.ToTable("user_last_visit");

            entity.HasIndex(e => new { e.UserId, e.ResourceType, e.ParentResourceId }, "user_last_visit_user_id_resource_type_parent_resource_id_idx");

            entity.HasIndex(e => new { e.UserId, e.ResourceType, e.ResourceId }, "user_last_visit_user_id_resource_type_resource_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.ParentResourceId).HasColumnName("parent_resource_id");
            entity.Property(e => e.LastVisitTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_visit_time");
        });

        modelBuilder.Entity<UserBaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_base_order_pkey");

            entity.ToTable("user_base_order");

            entity.HasIndex(e => new { e.UserId, e.Order }, "user_base_order_user_id_order_idx");

            entity.HasIndex(e => e.BaseId, "user_base_order_base_id_idx");

            entity.HasIndex(e => new { e.UserId, e.BaseId }, "user_base_order_user_id_base_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.User).WithMany(p => p.BaseOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_base_order_user_id_fkey");

            entity.HasOne(d => d.Base).WithMany(p => p.UserOrders)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_base_order_base_id_fkey");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("template_pkey");

            entity.ToTable("template");

            entity.HasIndex(e => e.BaseId, "template_base_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Cover).HasColumnName("cover");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MarkdownDescription).HasColumnName("markdown_description");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.IsSystem).HasColumnName("is_system");
            entity.Property(e => e.IsPublished).HasColumnName("is_published");
            entity.Property(e => e.Featured).HasColumnName("featured");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.UsageCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("usage_count");
            entity.Property(e => e.PublishInfo)
                .HasColumnType("jsonb")
                .HasColumnName("publish_info");
            entity.Property(e => e.VisitCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("visit_count");
            entity.Property(e => e.Kind).HasColumnName("kind");
        });

        modelBuilder.Entity<TemplateCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("template_category_pkey");

            entity.ToTable("template_category");

            entity.HasIndex(e => e.Name, "template_category_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_pkey");

            entity.ToTable("task");

            entity.HasIndex(e => new { e.Type, e.Status }, "task_type_status_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<TaskRun>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_run_pkey");

            entity.ToTable("task_run");

            entity.HasIndex(e => new { e.TaskId, e.Status }, "task_run_task_id_status_idx");

            entity.HasIndex(e => new { e.Status, e.BaseId, e.CreatedTime }, "task_run_status_base_id_created_time_idx");

            entity.HasIndex(e => new { e.Status, e.LastModifiedTime }, "task_run_status_last_modified_time_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Snapshot).HasColumnName("snapshot");
            entity.Property(e => e.DependsOnRunIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("depends_on_run_ids");
            entity.Property(e => e.Spent).HasColumnName("spent");
            entity.Property(e => e.Log).HasColumnName("log");
            entity.Property(e => e.ErrorMsg).HasColumnName("error_msg");
            entity.Property(e => e.StartedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("started_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.Task).WithMany(p => p.Runs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("task_run_task_id_fkey");
        });

        modelBuilder.Entity<TaskReference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_reference_pkey");

            entity.ToTable("task_reference");

            entity.HasIndex(e => e.FromFieldId, "task_reference_from_field_id_idx");

            entity.HasIndex(e => e.ToFieldId, "task_reference_to_field_id_idx");

            entity.HasIndex(e => new { e.ToFieldId, e.FromFieldId }, "task_reference_to_field_id_from_field_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.FromFieldId).HasColumnName("from_field_id");
            entity.Property(e => e.ToFieldId).HasColumnName("to_field_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<Waitlist>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("waitlist_email_key");

            entity.ToTable("waitlist");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Invite).HasColumnName("invite");
            entity.Property(e => e.InviteTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("invite_time");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
        });

        modelBuilder.Entity<BaseNode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_node_pkey");

            entity.ToTable("base_node");

            entity.HasIndex(e => new { e.BaseId, e.ResourceType, e.ResourceId }, "base_node_base_id_resource_type_resource_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.ResourceType).HasColumnName("resource_type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("base_node_parent_id_fkey");
        });

        modelBuilder.Entity<BaseNodeFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_node_folder_pkey");

            entity.ToTable("base_node_folder");

            entity.HasIndex(e => new { e.BaseId, e.Name }, "base_node_folder_base_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<BaseShare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_share_pkey");

            entity.ToTable("base_share");

            entity.HasIndex(e => e.ShareId, "base_share_share_id_key").IsUnique();

            entity.HasIndex(e => e.BaseId, "base_share_base_id_idx");

            entity.HasIndex(e => new { e.BaseId, e.NodeId }, "base_share_base_id_node_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.ShareId).HasColumnName("share_id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.NodeId).HasColumnName("node_id");
            entity.Property(e => e.AllowSave).HasColumnName("allow_save");
            entity.Property(e => e.AllowCopy).HasColumnName("allow_copy");
            entity.Property(e => e.AllowEdit).HasColumnName("allow_edit");
            entity.Property(e => e.Enabled)
                .ValueGeneratedNever()
                .HasDefaultValue(true)
                .HasColumnName("enabled");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LastModifiedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<ShortLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("short_link_pkey");

            entity.ToTable("short_link");

            entity.HasIndex(e => e.Code, "short_link_code_key").IsUnique();

            entity.HasIndex(e => e.DeletedTime, "short_link_deleted_time_idx");

            entity.HasIndex(e => new { e.Type, e.ResourceId }, "short_link_type_resource_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(32)
                .HasColumnName("code");
            entity.Property(e => e.Type)
                .HasMaxLength(32)
                .HasColumnName("type");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.CreatedTime)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedTime)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("deleted_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        StampUpdatedAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override global::System.Threading.Tasks.Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        StampUpdatedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void StampUpdatedAt()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<Space>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<DataDbConnection>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceDataDbBinding>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SpaceDataDbMigrationJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BaseDataDbMoveJob>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Base>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TableMeta>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SchemaOperation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Field>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ComputedUpdateOutbox>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ComputedUpdatePauseScope>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.UpdatedAt == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.UpdatedAt).IsModified))
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<View>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AttachmentsTable>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Collaborator>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Invitation>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AccessToken>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Setting>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<OAuthApp>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Plugin>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PluginInstall>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Dashboard>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Comment>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Integration>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PluginPanel>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<PluginContextMenu>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserBaseOrder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Template>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TemplateCategory>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Task>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<TaskRun>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BaseNode>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BaseNodeFolder>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BaseShare>())
        {
            if ((entry.State == EntityState.Added && entry.Entity.LastModifiedTime == default)
                || (entry.State == EntityState.Modified && !entry.Property(e => e.LastModifiedTime).IsModified))
            {
                entry.Entity.LastModifiedTime = now;
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

file sealed class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v, DateTimeKind.Utc), v => v)
    {
    }
}
