import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
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


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def oidc_provider_scopes_default() -> list[str]:
    return ["openid", "profile", "email", "offline_access"]


class WorkspaceRole(models.TextChoices):
    owner = "owner"
    editor = "editor"
    analyst = "analyst"


class ReverseSyncMode(models.TextChoices):
    upsert = "upsert"
    mirror = "mirror"


class ReverseSyncExtraction(models.TextChoices):
    cursor = "cursor"
    full = "full"


class EmailLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    time = UtcDateTimeField(default=timezone.now)
    email = models.JSONField()
    error = models.TextField(null=True)
    preview_url = models.TextField(null=True, db_column="previewUrl")
    message_id = models.TextField(null=True, db_column="messageId")
    status = models.TextField()

    class Meta:
        db_table = "EmailLog"


class UserPreferences(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("UserProfile", on_delete=models.RESTRICT, related_name="user_preferences", db_column="userId", db_index=False)
    workspace = models.ForeignKey("Workspace", on_delete=models.SET_NULL, related_name="preferences", db_column="workspaceId", null=True, db_index=False)
    scope = models.TextField(null=True)
    preferences = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserPreferences"


class UserProfile(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    email = models.TextField()
    admin = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    login_provider = models.TextField(db_column="loginProvider")
    external_username = models.TextField(null=True, db_column="externalUsername")
    external_id = models.TextField(db_column="externalId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserProfile"
        constraints = [
            models.UniqueConstraint(fields=["login_provider", "external_id"], name="UserProfile_loginProvider_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["external_id"]),
        ]


class UserPassword(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.OneToOneField("UserProfile", on_delete=models.RESTRICT, related_name="password", db_column="userId")
    hash = models.TextField()
    change_at_next_login = models.BooleanField(db_column="changeAtNextLogin", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserPassword"


class Workspace(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    deleted = models.BooleanField(default=False)
    slug = models.TextField(null=True, unique=True)
    features_enabled = ArrayField(models.TextField(), db_column="featuresEnabled", default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workspace"
        constraints = [
            models.UniqueConstraint(fields=["slug"], name="Workspace_slug_key"),
        ]
        indexes = [
            models.Index(fields=["slug"]),
            models.Index(fields=["-updated_at"]),
        ]


class WorkspaceOptions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="workspace_options", db_column="workspaceId", db_index=False)
    namespace = models.TextField()
    value = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkspaceOptions"
        indexes = [
            models.Index(fields=["workspace", "namespace"]),
        ]


class WorkspaceAccess(models.Model):
    pk = models.CompositePrimaryKey("user_id", "workspace_id")
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="workspace_access", db_column="workspaceId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("UserProfile", on_delete=models.RESTRICT, related_name="workspace_access", db_column="userId", db_index=False)
    role = models.TextField(choices=WorkspaceRole.choices, default=WorkspaceRole.owner)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkspaceAccess"


class WorkspaceUserProperties(models.Model):
    pk = models.CompositePrimaryKey("workspace_id", "user_id")
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="workspace_user_properties", db_column="workspaceId", db_index=False)
    user = models.ForeignKey("UserProfile", on_delete=models.RESTRICT, related_name="workspace_user_properties", db_column="userId", db_index=False)
    last_used = UtcDateTimeField(db_column="lastUsed", default=timezone.now)

    class Meta:
        db_table = "WorkspaceUserProperties"


class UserApiToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_used = UtcDateTimeField(null=True, db_column="lastUsed")
    hint = models.TextField()
    hash = models.TextField()
    user = models.ForeignKey("UserProfile", on_delete=models.RESTRICT, related_name="tokens", db_column="userId", db_index=False)
    type = models.TextField(null=True)
    name = models.TextField(null=True)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    oauth_client = models.ForeignKey("OAuthClient", on_delete=models.SET_NULL, related_name="tokens", db_column="oauthClientId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserApiToken"


class OAuthClient(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    client_secret_hash = models.TextField(db_column="clientSecretHash")
    name = models.TextField()
    redirect_uris = ArrayField(models.TextField(), db_column="redirectUris")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OAuthClient"


class KvStoreEntry(models.Model):
    key = models.TextField(primary_key=True, default=None)
    value = models.JSONField()
    expires_at = UtcDateTimeField(null=True)

    class Meta:
        db_table = "kv"
        indexes = [
            models.Index(fields=["expires_at"]),
            models.Index(fields=["key"], name="kv_key_pattern_idx"),
        ]


class OAuthAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_used = UtcDateTimeField(null=True, db_column="lastUsed")
    hash = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    refresh_token = models.ForeignKey("UserApiToken", on_delete=models.RESTRICT, related_name="access_tokens", db_column="refreshTokenId", db_index=False)

    class Meta:
        db_table = "OAuthAccessToken"
        indexes = [
            models.Index(fields=["expires_at"]),
            models.Index(fields=["refresh_token"]),
        ]


class InvitationToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workspace_id = models.TextField(db_column="workspaceId")
    email = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    token = models.TextField()
    used_by = models.TextField(null=True, db_column="usedBy")
    role = models.TextField(choices=WorkspaceRole.choices, default=WorkspaceRole.owner)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InvitationToken"


class ConfigurationObject(models.Model):
    id = models.TextField(primary_key=True, default=None)
    deleted = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField()
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="configuration_object", db_column="workspaceId", db_index=False)
    config = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConfigurationObject"
        indexes = [
            models.Index(fields=["-updated_at"]),
        ]


class ConfigurationObjectLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    deleted = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(null=True, default="push")
    data = models.JSONField(null=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="configuration_object_link", db_column="workspaceId", db_index=False)
    from_field = models.ForeignKey("ConfigurationObject", on_delete=models.RESTRICT, related_name="to_links", db_column="fromId", db_index=False)
    to = models.ForeignKey("ConfigurationObject", on_delete=models.RESTRICT, related_name="from_links", db_column="toId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConfigurationObjectLink"
        indexes = [
            models.Index(fields=["-updated_at"]),
            models.Index(fields=["workspace"]),
        ]


class ConnectorPackage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    package_id = models.TextField(db_column="packageId")
    package_type = models.TextField(db_column="packageType", default="airbyte")
    meta = models.JSONField(null=True)
    logo_svg = models.BinaryField(null=True, db_column="logoSvg")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConnectorPackage"


class AuditLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    timestamp = UtcDateTimeField(default=timezone.now)
    type = models.TextField()
    severity = models.TextField(null=True, default="info")
    user_id = models.TextField(null=True, db_column="userId")
    workspace_id = models.TextField(null=True, db_column="workspaceId")
    object_id = models.TextField(null=True, db_column="objectId")
    changes = models.JSONField(null=True)
    auth_type = models.TextField(null=True, db_column="authType")
    token_id = models.TextField(null=True, db_column="tokenId")
    ip = models.TextField(null=True)
    request_headers = models.JSONField(null=True, db_column="requestHeaders")

    class Meta:
        db_table = "AuditLog"
        indexes = [
            models.Index(fields=["timestamp"]),
            models.Index(fields=["type"]),
            models.Index(fields=["workspace_id", "timestamp"]),
        ]


class GlobalProps(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    value = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GlobalProps"


class SourceSpec(models.Model):
    pk = models.CompositePrimaryKey("package", "version")
    package = models.TextField()
    version = models.TextField()
    specs = models.JSONField(null=True)
    timestamp = models.DateTimeField(default=timezone.now)
    error = models.TextField(null=True)

    class Meta:
        db_table = "source_spec"


class SourceCheck(models.Model):
    package = models.TextField()
    version = models.TextField()
    key = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    description = models.TextField(null=True)
    timestamp = models.DateTimeField(default=timezone.now)

    class Meta:
        db_table = "source_check"


class SourceCatalog(models.Model):
    pk = models.CompositePrimaryKey("package", "version", "key")
    package = models.TextField()
    version = models.TextField()
    key = models.TextField()
    catalog = models.JSONField(null=True)
    timestamp = models.DateTimeField(default=timezone.now)
    status = models.TextField()
    description = models.TextField(null=True)

    class Meta:
        db_table = "source_catalog"


class ReverseSyncControl(models.Model):
    pk = models.CompositePrimaryKey("workspace_id", "sync_id", "run_id")
    workspace_id = models.TextField()
    sync_id = models.TextField()
    run_id = models.TextField()
    run_order = models.BigIntegerField(default=0)
    detached = models.BooleanField(default=False)
    revision = models.TextField()
    target_hash = models.TextField()
    phase = models.TextField(default="new")
    mode = models.TextField(choices=ReverseSyncMode.choices)
    extraction = models.TextField(choices=ReverseSyncExtraction.choices)
    artifact_head = models.BinaryField(null=True)
    base_sequence = models.BigIntegerField(default=0)
    next_sequence = models.BigIntegerField(default=0)
    checkpoint_sequence = models.BigIntegerField(default=0)
    finish_sequence = models.BigIntegerField(null=True)
    store = models.BinaryField(null=True)
    provider_state = models.BinaryField(null=True)
    finish_result = models.BinaryField(null=True)
    committed_generation = models.TextField(null=True)

    class Meta:
        db_table = "reverse_sync_control"
        constraints = [
            models.UniqueConstraint(fields=["workspace_id", "sync_id", "run_order"], name="reverse_sync_control_workspace_id_sync_id_run_order_key"),
        ]
        indexes = [
            models.Index(fields=["sync_id"]),
        ]


class ReverseSyncTargetOwner(models.Model):
    target_hash = models.TextField(primary_key=True, default=None)
    workspace_id = models.TextField()
    sync_id = models.TextField()

    class Meta:
        db_table = "reverse_sync_target_owner"
        constraints = [
            models.UniqueConstraint(fields=["workspace_id", "sync_id"], name="reverse_sync_target_owner_workspace_id_sync_id_key"),
        ]


class SourceState(models.Model):
    pk = models.CompositePrimaryKey("sync_id", "stream")
    sync_id = models.TextField()
    stream = models.TextField()
    state = models.JSONField()
    timestamp = models.DateTimeField(default=timezone.now)

    class Meta:
        db_table = "source_state"


class SourceTask(models.Model):
    sync_id = models.TextField()
    task_id = models.TextField(primary_key=True, default=None)
    package = models.TextField()
    version = models.TextField()
    started_at = models.DateTimeField(default=timezone.now)
    updated_at = models.DateTimeField(default=timezone.now)
    started_by = models.JSONField(null=True)
    status = models.TextField()
    description = models.TextField(null=True)
    metrics = models.JSONField(null=True)
    error = models.TextField(null=True)

    class Meta:
        db_table = "source_task"
        indexes = [
            models.Index(fields=["sync_id"]),
            models.Index(fields=["started_at"]),
            models.Index(fields=["sync_id", "-started_at"]),
        ]


class TaskLog(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    level = models.TextField()
    logger = models.TextField()
    message = models.TextField()
    sync_id = models.TextField()
    task_id = models.TextField()
    timestamp = models.DateTimeField(default=timezone.now)

    class Meta:
        db_table = "task_log"
        indexes = [
            models.Index(fields=["sync_id"]),
            models.Index(fields=["task_id"]),
            models.Index(fields=["timestamp"]),
        ]


class ProfileBuilder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    deleted = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="profile_builders", db_column="workspaceId", db_index=False)
    intermediate_storage_credentials = models.JSONField(db_column="intermediateStorageCredentials")
    destination = models.ForeignKey("ConfigurationObject", on_delete=models.SET_NULL, related_name="profile_builder", db_column="destinationId", null=True, db_index=False)
    connection_options = models.JSONField(null=True, db_column="connectionOptions")
    version = models.IntegerField(default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProfileBuilder"


class ProfileBuilderFunction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    profile_builder = models.ForeignKey("ProfileBuilder", on_delete=models.RESTRICT, related_name="functions", db_column="profileBuilderId", db_index=False)
    function = models.ForeignKey("ConfigurationObject", on_delete=models.RESTRICT, related_name="profile_builder_function", db_column="functionId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProfileBuilderFunction"


class ProfileBuilderState(models.Model):
    pk = models.CompositePrimaryKey("profile_builder_id", "profile_builder_version", "total_instances", "instance_index")
    profile_builder_id = models.TextField(db_column="profileBuilderId")
    profile_builder_version = models.IntegerField(db_column="profileBuilderVersion")
    instance_index = models.IntegerField(db_column="instanceIndex")
    total_instances = models.IntegerField(db_column="totalInstances")
    started_at = UtcDateTimeField(db_column="startedAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    last_timestamp = UtcDateTimeField(null=True, db_column="lastTimestamp")
    processed_users = models.IntegerField(db_column="processedUsers")
    total_users = models.IntegerField(db_column="totalUsers")
    error_users = models.IntegerField(db_column="errorUsers")
    speed = models.FloatField()

    class Meta:
        db_table = "ProfileBuilderState"


class ProfileBuilderState2(models.Model):
    profile_builder_id = models.TextField(primary_key=True, db_column="profileBuilderId", default=None)
    updated_at = UtcDateTimeField(db_column="updatedAt")
    full_rebuild_info = models.JSONField(null=True, db_column="fullRebuildInfo")
    queues_info = models.JSONField(null=True, db_column="queuesInfo")
    metrics = models.JSONField(null=True)

    class Meta:
        db_table = "ProfileBuilderState2"


class StatusChange(models.Model):
    id = models.BigAutoField(primary_key=True)
    workspace_id = models.TextField(db_column="workspaceId")
    actor_id = models.TextField(db_column="actorId")
    type = models.TextField()
    table_name = models.TextField(db_column="tableName")
    timestamp = models.DateTimeField(default=timezone.now)
    status = models.TextField()
    started_at = UtcDateTimeField(db_column="startedAt")
    counts = models.IntegerField(default=0)
    description = models.TextField(null=True)
    queue_size = models.IntegerField(db_column="queueSize", default=0)

    class Meta:
        db_table = "StatusChange"
        indexes = [
            models.Index(fields=["actor_id", "type", "table_name"]),
            models.Index(fields=["timestamp"]),
            models.Index(fields=["started_at"]),
        ]


class NotificationState(models.Model):
    pk = models.CompositePrimaryKey("channel_id", "actor_id", "type", "table_name")
    workspace_id = models.TextField(db_column="workspaceId")
    actor_id = models.TextField(db_column="actorId")
    type = models.TextField()
    table_name = models.TextField(db_column="tableName")
    channel_id = models.TextField(db_column="channelId")
    flapping_since = models.DateTimeField(null=True, db_column="flappingSince")
    last_notification = models.DateTimeField(db_column="lastNotification")
    status_change_id = models.BigIntegerField(db_column="statusChangeId")
    error = models.TextField(null=True)

    class Meta:
        db_table = "NotificationState"


class Notification(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    workspace_id = models.TextField(db_column="workspaceId")
    actor_id = models.TextField(db_column="actorId")
    type = models.TextField()
    table_name = models.TextField(db_column="tableName")
    channel_id = models.TextField(db_column="channelId")
    status_change_id = models.BigIntegerField(db_column="statusChangeId")
    timestamp = models.DateTimeField(default=timezone.now)
    status = models.TextField()
    error = models.TextField(null=True)

    class Meta:
        db_table = "Notification"
        indexes = [
            models.Index(fields=["actor_id", "type", "table_name"]),
            models.Index(fields=["timestamp"]),
        ]


class OidcProvider(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    issuer = models.TextField()
    client_id = models.TextField(db_column="clientId")
    client_secret = models.TextField(db_column="clientSecret")
    authorization_endpoint = models.TextField(null=True, db_column="authorizationEndpoint")
    token_endpoint = models.TextField(null=True, db_column="tokenEndpoint")
    userinfo_endpoint = models.TextField(null=True, db_column="userinfoEndpoint")
    jwks_uri = models.TextField(null=True, db_column="jwksUri")
    introspection_endpoint = models.TextField(null=True, db_column="introspectionEndpoint")
    scopes = ArrayField(models.TextField(), default=oidc_provider_scopes_default)
    audience = models.TextField(null=True)
    prompt = models.TextField(null=True)
    username_claim = models.TextField(db_column="usernameClaim", default="email")
    email_claim = models.TextField(db_column="emailClaim", default="email")
    name_claim = models.TextField(db_column="nameClaim", default="name")
    group_claim = models.TextField(db_column="groupClaim", default="groups")
    enabled = models.BooleanField(default=True)
    auto_discovery = models.BooleanField(db_column="autoDiscovery", default=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OidcProvider"
        constraints = [
            models.UniqueConstraint(fields=["issuer", "client_id"], name="OidcProvider_issuer_clientId_key"),
        ]


class OidcLoginGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.RESTRICT, related_name="oidc_login_groups", db_column="workspaceId", db_index=False)
    oidc_provider = models.ForeignKey("OidcProvider", on_delete=models.RESTRICT, related_name="oidc_login_groups", db_column="oidcProviderId", db_index=False)
    group_value = models.TextField(null=True, db_column="groupValue")
    allow_all_users = models.BooleanField(db_column="allowAllUsers", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OidcLoginGroup"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "oidc_provider"], name="OidcLoginGroup_workspaceId_oidcProviderId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace"]),
            models.Index(fields=["oidc_provider"]),
        ]


class ReprocessingJobs(models.Model):
    id = models.CharField(max_length=64, primary_key=True, default=None)
    config = models.JSONField()
    status = models.CharField(max_length=32)
    created_at = models.DateTimeField()
    started_at = models.DateTimeField(null=True)
    completed_at = models.DateTimeField(null=True)
    k8s_job_name = models.CharField(max_length=255, null=True)
    total_files = models.IntegerField(null=True, default=0)
    total_workers = models.IntegerField(null=True, default=0)
    error = models.TextField(null=True)

    class Meta:
        db_table = "reprocessing_jobs"
        indexes = [
            models.Index(fields=["-created_at"]),
        ]


class ReprocessingWorkers(models.Model):
    pk = models.CompositePrimaryKey("job_id", "worker_index")
    job = models.ForeignKey("ReprocessingJobs", on_delete=models.CASCADE, related_name="reprocessing_workers", db_index=False)
    worker_index = models.IntegerField()
    status = models.CharField(max_length=32)
    started_at = models.DateTimeField(null=True)
    updated_at = models.DateTimeField(default=timezone.now)
    completed_at = models.DateTimeField(null=True)
    current_file = models.TextField(null=True)
    current_line = models.BigIntegerField(null=True, default=0)
    current_ts = models.TextField(null=True)
    assigned_files = models.IntegerField(null=True, default=0)
    processed_files = models.IntegerField(null=True, default=0)
    total_lines = models.BigIntegerField(null=True, default=0)
    success_count = models.BigIntegerField(null=True, default=0)
    error_count = models.BigIntegerField(null=True, default=0)
    skipped_count = models.BigIntegerField(null=True, default=0)
    total_bytes = models.BigIntegerField(null=True, default=0)
    processed_bytes = models.BigIntegerField(null=True, default=0)
    error = models.TextField(null=True)

    class Meta:
        db_table = "reprocessing_workers"
        indexes = [
            models.Index(fields=["job"]),
            models.Index(fields=["job", "status"]),
        ]


class FunctionsServer(models.Model):
    pk = models.CompositePrimaryKey("workspace_id", "class_field")
    workspace_id = models.TextField(db_column="workspaceId")
    class_field = models.TextField(db_column="class")
    deployment_id = models.TextField(db_column="deploymentId")
    connections = ArrayField(models.TextField())
    empty_connections = ArrayField(models.TextField(), db_column="emptyConnections")
    profile_builders = ArrayField(models.TextField(), db_column="profileBuilders")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(null=True, db_column="updatedAt")

    class Meta:
        db_table = "FunctionsServer"


class RateLimitCounter(models.Model):
    pk = models.CompositePrimaryKey("bucket_key", "window_start")
    bucket_key = models.TextField(db_column="bucketKey")
    window_start = models.DateTimeField(db_column="windowStart")
    count = models.IntegerField(default=0)
    expires_at = models.DateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "RateLimitCounter"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]
