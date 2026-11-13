#nullable enable

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Visus.Cuid;

namespace SuperProductivity.Models;

public partial class SuperProductivityContext : DbContext
{
    public SuperProductivityContext(DbContextOptions<SuperProductivityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Passkey> Passkeys { get; set; }

    public virtual DbSet<PendingPasskeyRegistration> PendingPasskeyRegistrations { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<UserSyncState> UserSyncStates { get; set; }

    public virtual DbSet<SyncDevice> SyncDevices { get; set; }

    public virtual DbSet<CaptureKey> CaptureKeys { get; set; }

    public virtual DbSet<CaptureGrant> CaptureGrants { get; set; }

    public virtual DbSet<CaptureItem> CaptureItems { get; set; }

    public virtual DbSet<CaptureConnectCode> CaptureConnectCodes { get; set; }

    public virtual DbSet<CaptureAuth> CaptureAuths { get; set; }

    public virtual DbSet<ConnectRequest> ConnectRequests { get; set; }

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
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.VerificationToken, "users_verification_token_idx");

            entity.HasIndex(e => e.ResetPasswordToken, "users_reset_password_token_idx");

            entity.HasIndex(e => e.PasskeyRecoveryToken, "users_passkey_recovery_token_idx");

            entity.HasIndex(e => e.LoginToken, "users_login_token_idx");

            entity.Property(e => e.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.IsVerified)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("is_verified");
            entity.Property(e => e.VerificationToken).HasColumnName("verification_token");
            entity.Property(e => e.VerificationTokenExpiresAt).HasColumnName("verification_token_expires_at");
            entity.Property(e => e.VerificationResendCount)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("verification_resend_count");
            entity.Property(e => e.ResetPasswordToken).HasColumnName("reset_password_token");
            entity.Property(e => e.ResetPasswordTokenExpiresAt).HasColumnName("reset_password_token_expires_at");
            entity.Property(e => e.PasskeyRecoveryToken).HasColumnName("passkey_recovery_token");
            entity.Property(e => e.PasskeyRecoveryTokenExpiresAt).HasColumnName("passkey_recovery_token_expires_at");
            entity.Property(e => e.LoginToken).HasColumnName("login_token");
            entity.Property(e => e.LoginTokenExpiresAt).HasColumnName("login_token_expires_at");
            entity.Property(e => e.FailedLoginAttempts)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("failed_login_attempts");
            entity.Property(e => e.LockedUntil).HasColumnName("locked_until");
            entity.Property(e => e.TokenVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("token_version");
            entity.Property(e => e.TermsAcceptedAt).HasColumnName("terms_accepted_at");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.StorageQuotaBytes)
                .ValueGeneratedNever()
                .HasDefaultValue(104857600L)
                .HasColumnName("storage_quota_bytes");
            entity.Property(e => e.StorageUsedBytes)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("storage_used_bytes");
        });

        modelBuilder.Entity<Passkey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passkeys_pkey");

            entity.ToTable("passkeys");

            entity.HasIndex(e => e.CredentialId, "passkeys_credential_id_key").IsUnique();

            entity.HasIndex(e => e.UserId, "passkeys_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");
            entity.Property(e => e.PublicKey).HasColumnName("public_key");
            entity.Property(e => e.Counter)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("counter");
            entity.Property(e => e.Transports).HasColumnName("transports");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.LastUsedAt)
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Passkeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("passkeys_user_id_fkey");
        });

        modelBuilder.Entity<PendingPasskeyRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pending_passkey_registrations_pkey");

            entity.ToTable("pending_passkey_registrations");

            entity.HasIndex(e => e.VerificationToken, "pending_passkey_registrations_verification_token_key").IsUnique();

            entity.HasIndex(e => e.UserId, "pending_passkey_registrations_user_id_idx");

            entity.Property(e => e.Id)
                .HasValueGenerator<CuidGenerator>()
                .HasColumnName("id");
            entity.Property(e => e.VerificationToken).HasColumnName("verification_token");
            entity.Property(e => e.VerificationTokenExpiresAt).HasColumnName("verification_token_expires_at");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");
            entity.Property(e => e.PublicKey).HasColumnName("public_key");
            entity.Property(e => e.Counter)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("counter");
            entity.Property(e => e.Transports).HasColumnName("transports");
            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<UtcNowGenerator>()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion<UtcClockConverter>()
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PendingPasskeyRegistrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pending_passkey_registrations_user_id_fkey");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("operations_pkey");

            entity.ToTable("operations");

            entity.HasIndex(e => new { e.UserId, e.EntityType, e.EntityId, e.ServerSeq }, "operations_user_id_entity_type_entity_id_server_seq_idx");

            entity.HasIndex(e => new { e.UserId, e.ReceivedAt, e.ServerSeq }, "operations_user_id_received_at_server_seq_idx");

            entity.HasIndex(e => e.EntityIds, "operations_entity_ids_gin").HasMethod("gin");

            entity.HasIndex(e => new { e.UserId, e.ServerSeq }, "operations_user_id_server_seq_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ServerSeq).HasColumnName("server_seq");
            entity.Property(e => e.ActionType).HasColumnName("action_type");
            entity.Property(e => e.OpType).HasColumnName("op_type");
            entity.Property(e => e.EntityType).HasColumnName("entity_type");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityIds)
                .ValueGeneratedNever()
                .HasDefaultValue(new List<string>())
                .HasColumnName("entity_ids");
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.PayloadBytes)
                .ValueGeneratedNever()
                .HasDefaultValue(0L)
                .HasColumnName("payload_bytes");
            entity.Property(e => e.VectorClock)
                .HasColumnType("jsonb")
                .HasColumnName("vector_clock");
            entity.Property(e => e.SchemaVersion).HasColumnName("schema_version");
            entity.Property(e => e.ClientTimestamp).HasColumnName("client_timestamp");
            entity.Property(e => e.ReceivedAt).HasColumnName("received_at");
            entity.Property(e => e.IsPayloadEncrypted)
                .ValueGeneratedNever()
                .HasDefaultValue(false)
                .HasColumnName("is_payload_encrypted");
            entity.Property(e => e.SyncImportReason).HasColumnName("sync_import_reason");
            entity.Property(e => e.RepairBaseServerSeq).HasColumnName("repair_base_server_seq");

            entity.HasOne(d => d.User).WithMany(p => p.Operations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("operations_user_id_fkey");
        });

        modelBuilder.Entity<UserSyncState>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_sync_state_pkey");

            entity.ToTable("user_sync_state");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.LastSeq)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("last_seq");
            entity.Property(e => e.LastSnapshotSeq).HasColumnName("last_snapshot_seq");
            entity.Property(e => e.SnapshotData).HasColumnName("snapshot_data");
            entity.Property(e => e.SnapshotAt).HasColumnName("snapshot_at");
            entity.Property(e => e.SnapshotSchemaVersion)
                .ValueGeneratedNever()
                .HasDefaultValue(1)
                .HasColumnName("snapshot_schema_version");
            entity.Property(e => e.LatestFullStateSeq).HasColumnName("latest_full_state_seq");
            entity.Property(e => e.LatestFullStateVectorClock)
                .HasColumnType("jsonb")
                .HasColumnName("latest_full_state_vector_clock");
            entity.Property(e => e.LatestStateReplacementSeq).HasColumnName("latest_state_replacement_seq");

            entity.HasOne(d => d.User).WithOne(p => p.SyncState)
                .HasForeignKey<UserSyncState>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_sync_state_user_id_fkey");
        });

        modelBuilder.Entity<SyncDevice>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClientId }).HasName("sync_devices_pkey");

            entity.ToTable("sync_devices");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DeviceName).HasColumnName("device_name");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.AppVersion).HasColumnName("app_version");
            entity.Property(e => e.LastSeenAt).HasColumnName("last_seen_at");
            entity.Property(e => e.LastAckedSeq)
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .HasColumnName("last_acked_seq");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany(p => p.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sync_devices_user_id_fkey");
        });

        modelBuilder.Entity<CaptureKey>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("capture_keys_pkey");

            entity.ToTable("capture_keys");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.PublicKey).HasColumnName("public_key");
            entity.Property(e => e.Fingerprint).HasColumnName("fingerprint");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DisabledAt).HasColumnName("disabled_at");

            entity.HasOne(d => d.User).WithOne(p => p.CaptureKey)
                .HasForeignKey<CaptureKey>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capture_keys_user_id_fkey");
        });

        modelBuilder.Entity<CaptureGrant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("capture_grants_pkey");

            entity.ToTable("capture_grants");

            entity.HasIndex(e => e.UserId, "capture_grants_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.ClientName).HasColumnName("client_name");
            entity.Property(e => e.SecretHash).HasColumnName("secret_hash");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.LastUsedAt).HasColumnName("last_used_at");
            entity.Property(e => e.RevokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.RefreshHash).HasColumnName("refresh_hash");
            entity.Property(e => e.PrevRefreshHash).HasColumnName("prev_refresh_hash");
            entity.Property(e => e.RotatedAt).HasColumnName("rotated_at");
            entity.Property(e => e.RefreshExpiresAt).HasColumnName("refresh_expires_at");

            entity.HasOne(d => d.User).WithMany(p => p.CaptureGrants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capture_grants_user_id_fkey");
        });

        modelBuilder.Entity<CaptureItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("capture_items_pkey");

            entity.ToTable("capture_items");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "capture_items_user_id_created_at_idx");

            entity.HasIndex(e => e.GrantId, "capture_items_grant_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.GrantId).HasColumnName("grant_id");
            entity.Property(e => e.Ciphertext).HasColumnName("ciphertext");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.LeaseDeviceId).HasColumnName("lease_device_id");
            entity.Property(e => e.LeaseUntil).HasColumnName("lease_until");

            entity.HasOne(d => d.User).WithMany(p => p.CaptureItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capture_items_user_id_fkey");
        });

        modelBuilder.Entity<CaptureConnectCode>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("capture_connect_codes_pkey");

            entity.ToTable("capture_connect_codes");

            entity.HasIndex(e => e.CodeHash, "capture_connect_codes_code_hash_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CodeHash).HasColumnName("code_hash");
            entity.Property(e => e.TokenVersion).HasColumnName("token_version");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

            entity.HasOne(d => d.User).WithOne(p => p.CaptureConnectCode)
                .HasForeignKey<CaptureConnectCode>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capture_connect_codes_user_id_fkey");
        });

        modelBuilder.Entity<CaptureAuth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("capture_auth_pkey");

            entity.ToTable("capture_auth");

            entity.HasIndex(e => e.CodeHash, "capture_auth_code_hash_key").IsUnique();

            entity.HasIndex(e => e.UserId, "capture_auth_user_id_idx");

            entity.HasIndex(e => e.ExpiresAt, "capture_auth_expires_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientName).HasColumnName("client_name");
            entity.Property(e => e.RedirectUri).HasColumnName("redirect_uri");
            entity.Property(e => e.CodeChallenge).HasColumnName("code_challenge");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.CodeHash).HasColumnName("code_hash");
            entity.Property(e => e.TokenVersion).HasColumnName("token_version");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

            entity.HasOne(d => d.User).WithMany(p => p.CaptureAuthorizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capture_auth_user_id_fkey");
        });

        modelBuilder.Entity<ConnectRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("connect_requests_pkey");

            entity.ToTable("connect_requests");

            entity.HasIndex(e => e.UserId, "connect_requests_user_id_idx");

            entity.HasIndex(e => e.ExpiresAt, "connect_requests_expires_at_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PollSecretHash).HasColumnName("poll_secret_hash");
            entity.Property(e => e.MatchCode).HasColumnName("match_code");
            entity.Property(e => e.DeviceName).HasColumnName("device_name");
            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.Status)
                .ValueGeneratedNever()
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TokenVersion).HasColumnName("token_version");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

            entity.HasOne(d => d.User).WithMany(p => p.ConnectRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("connect_requests_user_id_fkey");
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
