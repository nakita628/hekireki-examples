from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.utils import timezone


class UtcDateTimeField(models.DateTimeField):  # type: ignore[type-arg]
    def get_db_prep_value(self, value: Any, connection: BaseDatabaseWrapper, prepared: bool = False) -> Any:
        if not prepared:
            value = self.get_prep_value(value)
        if not isinstance(value, datetime):
            return super().get_db_prep_value(value, connection, prepared=True)
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_default_timezone())
        value = value.astimezone(dt_timezone.utc)
        if connection.vendor == "sqlite":
            return value.isoformat(timespec="milliseconds")
        return connection.ops.adapt_datetimefield_value(value.replace(tzinfo=None))

    def from_db_value(self, value: datetime | None, expression: Any, connection: BaseDatabaseWrapper) -> datetime | None:
        if value is None:
            return None
        if timezone.is_naive(value):
            value = value.replace(tzinfo=dt_timezone.utc)
        return value if settings.USE_TZ else timezone.make_naive(value, timezone.get_default_timezone())


class User(models.Model):
    id = models.AutoField(primary_key=True)
    email = models.TextField(unique=True)
    password_hash = models.TextField(null=True)
    is_verified = models.IntegerField(default=0)
    verification_token = models.TextField(null=True)
    verification_token_expires_at = models.BigIntegerField(null=True)
    verification_resend_count = models.IntegerField(default=0)
    reset_password_token = models.TextField(null=True)
    reset_password_token_expires_at = models.BigIntegerField(null=True)
    passkey_recovery_token = models.TextField(null=True)
    passkey_recovery_token_expires_at = models.BigIntegerField(null=True)
    login_token = models.TextField(null=True)
    login_token_expires_at = models.BigIntegerField(null=True)
    failed_login_attempts = models.IntegerField(default=0)
    locked_until = models.BigIntegerField(null=True)
    token_version = models.IntegerField(default=0)
    terms_accepted_at = models.BigIntegerField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    storage_quota_bytes = models.BigIntegerField(default=104857600)
    storage_used_bytes = models.BigIntegerField(default=0)

    class Meta:
        db_table = "users"
        indexes = [
            models.Index(fields=["verification_token"]),
            models.Index(fields=["reset_password_token"]),
            models.Index(fields=["passkey_recovery_token"]),
            models.Index(fields=["login_token"]),
        ]


class Passkey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    credential_id = models.BinaryField(unique=True)
    public_key = models.BinaryField()
    counter = models.BigIntegerField(default=0)
    transports = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    last_used_at = UtcDateTimeField(null=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="passkeys", db_index=False)

    class Meta:
        db_table = "passkeys"
        indexes = [
            models.Index(fields=["user"]),
        ]


class PendingPasskeyRegistration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    verification_token = models.TextField(unique=True)
    verification_token_expires_at = models.BigIntegerField()
    credential_id = models.BinaryField()
    public_key = models.BinaryField()
    counter = models.BigIntegerField(default=0)
    transports = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="pending_passkey_registrations", db_index=False)

    class Meta:
        db_table = "pending_passkey_registrations"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Operation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="operations", db_index=False)
    client_id = models.TextField()
    server_seq = models.IntegerField()
    action_type = models.TextField()
    op_type = models.TextField()
    entity_type = models.TextField()
    entity_id = models.TextField(null=True)
    entity_ids = ArrayField(models.TextField(), default=list)
    payload = models.JSONField()
    payload_bytes = models.BigIntegerField(default=0)
    vector_clock = models.JSONField()
    schema_version = models.IntegerField()
    client_timestamp = models.BigIntegerField()
    received_at = models.BigIntegerField()
    is_payload_encrypted = models.BooleanField(default=False)
    sync_import_reason = models.TextField(null=True)
    repair_base_server_seq = models.IntegerField(null=True)

    class Meta:
        db_table = "operations"
        constraints = [
            models.UniqueConstraint(fields=["user", "server_seq"], name="operations_user_id_server_seq_key"),
        ]
        indexes = [
            models.Index(fields=["user", "entity_type", "entity_id", "server_seq"]),
            models.Index(fields=["user", "received_at", "server_seq"]),
            GinIndex(fields=["entity_ids"], name="operations_entity_ids_gin"),
        ]


class UserSyncState(models.Model):
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="sync_state", primary_key=True)
    last_seq = models.IntegerField(default=0)
    last_snapshot_seq = models.IntegerField(null=True)
    snapshot_data = models.BinaryField(null=True)
    snapshot_at = models.BigIntegerField(null=True)
    snapshot_schema_version = models.IntegerField(null=True, default=1)
    latest_full_state_seq = models.IntegerField(null=True)
    latest_full_state_vector_clock = models.JSONField(null=True)
    latest_state_replacement_seq = models.IntegerField(null=True)

    class Meta:
        db_table = "user_sync_state"


class SyncDevice(models.Model):
    pk = models.CompositePrimaryKey("user_id", "client_id")
    client_id = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="devices", db_index=False)
    device_name = models.TextField(null=True)
    user_agent = models.TextField(null=True)
    app_version = models.TextField(null=True)
    last_seen_at = models.BigIntegerField()
    last_acked_seq = models.IntegerField(default=0)
    created_at = models.BigIntegerField()

    class Meta:
        db_table = "sync_devices"


class CaptureKey(models.Model):
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="capture_key", primary_key=True)
    public_key = models.TextField()
    fingerprint = models.TextField()
    created_at = models.BigIntegerField()
    disabled_at = models.BigIntegerField(null=True)

    class Meta:
        db_table = "capture_keys"


class CaptureGrant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="capture_grants", db_index=False)
    kind = models.TextField()
    client_name = models.TextField()
    secret_hash = models.TextField()
    created_at = models.BigIntegerField()
    expires_at = models.BigIntegerField()
    last_used_at = models.BigIntegerField(null=True)
    revoked_at = models.BigIntegerField(null=True)
    client_id = models.TextField(null=True)
    resource = models.TextField(null=True)
    refresh_hash = models.TextField(null=True)
    prev_refresh_hash = models.TextField(null=True)
    rotated_at = models.BigIntegerField(null=True)
    refresh_expires_at = models.BigIntegerField(null=True)

    class Meta:
        db_table = "capture_grants"
        indexes = [
            models.Index(fields=["user"]),
        ]


class CaptureItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="capture_items", db_index=False)
    grant_id = models.TextField()
    ciphertext = models.TextField()
    task_id = models.TextField()
    created_at = models.BigIntegerField()
    lease_device_id = models.TextField(null=True)
    lease_until = models.BigIntegerField(null=True)

    class Meta:
        db_table = "capture_items"
        indexes = [
            models.Index(fields=["user", "created_at"]),
            models.Index(fields=["grant_id"]),
        ]


class CaptureConnectCode(models.Model):
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="capture_connect_code", primary_key=True)
    code_hash = models.TextField(unique=True)
    token_version = models.IntegerField()
    expires_at = models.BigIntegerField()

    class Meta:
        db_table = "capture_connect_codes"


class CaptureAuth(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="capture_authorizations", null=True, db_index=False)
    client_id = models.TextField()
    client_name = models.TextField()
    redirect_uri = models.TextField()
    code_challenge = models.TextField()
    resource = models.TextField()
    state = models.TextField()
    scope = models.TextField()
    code_hash = models.TextField(null=True, unique=True)
    token_version = models.IntegerField(null=True)
    expires_at = models.BigIntegerField()

    class Meta:
        db_table = "capture_auth"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["expires_at"]),
        ]


class ConnectRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    poll_secret_hash = models.TextField()
    match_code = models.TextField()
    device_name = models.TextField()
    platform = models.TextField()
    status = models.TextField(default="pending")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="connect_requests", null=True, db_index=False)
    token_version = models.IntegerField(null=True)
    expires_at = models.BigIntegerField()

    class Meta:
        db_table = "connect_requests"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["expires_at"]),
        ]
