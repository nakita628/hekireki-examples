from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
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


class ApiKeys(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(null=True)
    secret = models.TextField(null=True, unique=True)
    created_by = models.IntegerField(null=True, db_column="createdBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "api_keys"


class WorkspaceDocuments(models.Model):
    id = models.AutoField(primary_key=True)
    doc_id = models.TextField(unique=True, db_column="docId")
    filename = models.TextField()
    docpath = models.TextField()
    workspace = models.ForeignKey("Workspaces", on_delete=models.RESTRICT, related_name="documents", db_column="workspaceId", db_index=False)
    metadata = models.TextField(null=True)
    pinned = models.BooleanField(null=True, default=False)
    watched = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "workspace_documents"


class Invites(models.Model):
    id = models.AutoField(primary_key=True)
    code = models.TextField(unique=True)
    status = models.TextField(default="pending")
    claimed_by = models.IntegerField(null=True, db_column="claimedBy")
    workspace_ids = models.TextField(null=True, db_column="workspaceIds")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    created_by = models.IntegerField(db_column="createdBy")
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "invites"


class SystemSettings(models.Model):
    id = models.AutoField(primary_key=True)
    label = models.TextField(unique=True)
    value = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "system_settings"


class Users(models.Model):
    id = models.AutoField(primary_key=True)
    username = models.TextField(null=True, unique=True)
    password = models.TextField()
    pfp_filename = models.TextField(null=True, db_column="pfpFilename")
    role = models.TextField(default="default")
    suspended = models.IntegerField(default=0)
    seen_recovery_codes = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)
    daily_message_limit = models.IntegerField(null=True, db_column="dailyMessageLimit")
    bio = models.TextField(null=True, default="")
    web_push_subscription_config = models.TextField(null=True)

    class Meta:
        db_table = "users"


class RecoveryCodes(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="recovery_codes", db_index=False)
    code_hash = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "recovery_codes"
        indexes = [
            models.Index(fields=["user"]),
        ]


class PasswordResetTokens(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="password_reset_tokens", db_index=False)
    token = models.TextField(unique=True)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "password_reset_tokens"
        indexes = [
            models.Index(fields=["user"]),
        ]


class DocumentVectors(models.Model):
    id = models.AutoField(primary_key=True)
    doc_id = models.TextField(db_column="docId")
    vector_id = models.TextField(db_column="vectorId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "document_vectors"


class Workspaces(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    slug = models.TextField(unique=True)
    vector_tag = models.TextField(null=True, db_column="vectorTag")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    open_ai_temp = models.FloatField(null=True, db_column="openAiTemp")
    open_ai_history = models.IntegerField(db_column="openAiHistory", default=20)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)
    open_ai_prompt = models.TextField(null=True, db_column="openAiPrompt")
    similarity_threshold = models.FloatField(null=True, db_column="similarityThreshold", default=0.25)
    chat_provider = models.TextField(null=True, db_column="chatProvider")
    chat_model = models.TextField(null=True, db_column="chatModel")
    top_n = models.IntegerField(null=True, db_column="topN", default=4)
    chat_mode = models.TextField(null=True, db_column="chatMode", default="chat")
    pfp_filename = models.TextField(null=True, db_column="pfpFilename")
    agent_provider = models.TextField(null=True, db_column="agentProvider")
    agent_model = models.TextField(null=True, db_column="agentModel")
    query_refusal_response = models.TextField(null=True, db_column="queryRefusalResponse")
    vector_search_mode = models.TextField(null=True, db_column="vectorSearchMode", default="default")
    router_id = models.IntegerField(null=True)

    class Meta:
        db_table = "workspaces"


class WorkspaceThreads(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    slug = models.TextField(unique=True)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="threads", db_index=False)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="threads", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "workspace_threads"
        indexes = [
            models.Index(fields=["workspace"]),
            models.Index(fields=["user"]),
        ]


class WorkspaceSuggestedMessages(models.Model):
    id = models.AutoField(primary_key=True)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="workspace_suggested_messages", db_column="workspaceId", db_index=False)
    heading = models.TextField()
    message = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "workspace_suggested_messages"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class WorkspaceChats(models.Model):
    id = models.AutoField(primary_key=True)
    workspace_id = models.IntegerField(db_column="workspaceId")
    prompt = models.TextField()
    response = models.TextField()
    include = models.BooleanField(default=True)
    users = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="workspace_chats", db_column="user_id", null=True, db_index=False)
    thread_id = models.IntegerField(null=True)
    api_session_id = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)
    feedback_score = models.BooleanField(null=True, db_column="feedbackScore")
    memory_processed = models.BooleanField(null=True)

    class Meta:
        db_table = "workspace_chats"


class WorkspaceAgentInvocations(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    prompt = models.TextField()
    closed = models.BooleanField(default=False)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="workspace_agent_invocations", null=True, db_index=False)
    thread_id = models.IntegerField(null=True)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="workspace_agent_invocations", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "workspace_agent_invocations"
        indexes = [
            models.Index(fields=["uuid"]),
        ]


class WorkspaceUsers(models.Model):
    id = models.AutoField(primary_key=True)
    users = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="workspace_users", db_column="user_id", db_index=False)
    workspaces = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="workspace_users", db_column="workspace_id", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "workspace_users"


class CacheData(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    data = models.TextField()
    belongs_to = models.TextField(null=True, db_column="belongsTo")
    by_id = models.IntegerField(null=True, db_column="byId")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "cache_data"


class EmbedConfigs(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    enabled = models.BooleanField(default=False)
    chat_mode = models.TextField(default="query")
    allowlist_domains = models.TextField(null=True)
    allow_model_override = models.BooleanField(default=False)
    allow_temperature_override = models.BooleanField(default=False)
    allow_prompt_override = models.BooleanField(default=False)
    max_chats_per_day = models.IntegerField(null=True)
    max_chats_per_session = models.IntegerField(null=True)
    message_limit = models.IntegerField(null=True, default=20)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="embed_configs", db_index=False)
    created_by = models.IntegerField(null=True, db_column="createdBy")
    users = models.ForeignKey("Users", on_delete=models.SET_NULL, related_name="embed_configs", db_column="usersId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "embed_configs"


class EmbedChats(models.Model):
    id = models.AutoField(primary_key=True)
    prompt = models.TextField()
    response = models.TextField()
    session_id = models.TextField()
    include = models.BooleanField(default=True)
    connection_information = models.TextField(null=True)
    embed_config = models.ForeignKey("EmbedConfigs", on_delete=models.CASCADE, related_name="embed_chats", db_column="embed_id", db_index=False)
    users = models.ForeignKey("Users", on_delete=models.SET_NULL, related_name="embed_chats", db_column="usersId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "embed_chats"


class EventLogs(models.Model):
    id = models.AutoField(primary_key=True)
    event = models.TextField()
    metadata = models.TextField(null=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    occurred_at = UtcDateTimeField(db_column="occurredAt", default=timezone.now)

    class Meta:
        db_table = "event_logs"
        indexes = [
            models.Index(fields=["event"]),
        ]


class SlashCommandPresets(models.Model):
    id = models.AutoField(primary_key=True)
    command = models.TextField()
    prompt = models.TextField()
    description = models.TextField()
    uid = models.IntegerField(default=0)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="slash_command_presets", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "slash_command_presets"
        constraints = [
            models.UniqueConstraint(fields=["uid", "command"], name="slash_command_presets_uid_command_key"),
        ]


class DocumentSyncQueues(models.Model):
    id = models.AutoField(primary_key=True)
    stale_after_ms = models.IntegerField(db_column="staleAfterMs", default=604800000)
    next_sync_at = UtcDateTimeField(db_column="nextSyncAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_synced_at = UtcDateTimeField(db_column="lastSyncedAt", default=timezone.now)
    workspace_doc = models.OneToOneField("WorkspaceDocuments", on_delete=models.CASCADE, related_name="document_sync_queues", db_column="workspaceDocId")

    class Meta:
        db_table = "document_sync_queues"


class DocumentSyncExecutions(models.Model):
    id = models.AutoField(primary_key=True)
    queue = models.ForeignKey("DocumentSyncQueues", on_delete=models.CASCADE, related_name="runs", db_column="queueId", db_index=False)
    status = models.TextField(default="unknown")
    result = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "document_sync_executions"


class BrowserExtensionApiKeys(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.TextField(unique=True)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="browser_extension_api_keys", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "browser_extension_api_keys"
        indexes = [
            models.Index(fields=["user"]),
        ]


class TemporaryAuthTokens(models.Model):
    id = models.AutoField(primary_key=True)
    token = models.TextField(unique=True)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="temporary_auth_tokens", db_column="userId", db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "temporary_auth_tokens"
        indexes = [
            models.Index(fields=["token"]),
            models.Index(fields=["user"]),
        ]


class SystemPromptVariables(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.TextField(unique=True)
    value = models.TextField(null=True)
    description = models.TextField(null=True)
    type = models.TextField(default="system")
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="system_prompt_variables", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "system_prompt_variables"
        indexes = [
            models.Index(fields=["user"]),
        ]


class PromptHistory(models.Model):
    id = models.AutoField(primary_key=True)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="prompt_history", db_column="workspaceId", db_index=False)
    prompt = models.TextField()
    user = models.ForeignKey("Users", on_delete=models.SET_NULL, related_name="prompt_history", db_column="modifiedBy", null=True, db_index=False)
    modified_at = UtcDateTimeField(db_column="modifiedAt", default=timezone.now)

    class Meta:
        db_table = "prompt_history"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class DesktopMobileDevices(models.Model):
    id = models.AutoField(primary_key=True)
    device_os = models.TextField(db_column="deviceOs")
    device_name = models.TextField(db_column="deviceName")
    token = models.TextField(unique=True)
    approved = models.BooleanField(default=False)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="desktop_mobile_devices", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "desktop_mobile_devices"
        indexes = [
            models.Index(fields=["user"]),
        ]


class WorkspaceParsedFiles(models.Model):
    id = models.AutoField(primary_key=True)
    filename = models.TextField(unique=True)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="workspace_parsed_files", db_column="workspaceId", db_index=False)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="workspace_parsed_files", db_column="userId", null=True, db_index=False)
    thread = models.ForeignKey("WorkspaceThreads", on_delete=models.CASCADE, related_name="workspace_parsed_files", db_column="threadId", null=True, db_index=False)
    metadata = models.TextField(null=True)
    token_count_estimate = models.IntegerField(null=True, db_column="tokenCountEstimate", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "workspace_parsed_files"
        indexes = [
            models.Index(fields=["workspace"]),
            models.Index(fields=["user"]),
        ]


class ExternalCommunicationConnectors(models.Model):
    id = models.AutoField(primary_key=True)
    type = models.TextField(unique=True)
    config = models.TextField(default="{}")
    active = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "external_communication_connectors"


class ScheduledJobs(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    prompt = models.TextField()
    tools = models.TextField(null=True)
    schedule = models.TextField()
    enabled = models.BooleanField(default=True)
    last_run_at = UtcDateTimeField(null=True, db_column="lastRunAt")
    next_run_at = UtcDateTimeField(null=True, db_column="nextRunAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", default=timezone.now)

    class Meta:
        db_table = "scheduled_jobs"


class ScheduledJobRuns(models.Model):
    id = models.AutoField(primary_key=True)
    job = models.ForeignKey("ScheduledJobs", on_delete=models.CASCADE, related_name="runs", db_column="jobId", db_index=False)
    status = models.TextField(default="queued")
    result = models.TextField(null=True)
    error = models.TextField(null=True)
    started_at = UtcDateTimeField(db_column="startedAt", default=timezone.now)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    read_at = UtcDateTimeField(null=True, db_column="readAt")

    class Meta:
        db_table = "scheduled_job_runs"
        indexes = [
            models.Index(fields=["job"]),
        ]


class Memories(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="memories", null=True, db_index=False)
    workspace = models.ForeignKey("Workspaces", on_delete=models.CASCADE, related_name="memories", null=True, db_index=False)
    scope = models.TextField(default="workspace")
    content = models.TextField()
    last_used_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "memories"
        indexes = [
            models.Index(fields=["user", "workspace"]),
            models.Index(fields=["user", "scope"]),
        ]


class ModelRouters(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(unique=True)
    description = models.TextField(null=True)
    fallback_provider = models.TextField()
    fallback_model = models.TextField()
    cooldown_seconds = models.IntegerField(default=30)
    created_by = models.IntegerField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "model_routers"


class ModelRouterRules(models.Model):
    id = models.AutoField(primary_key=True)
    router = models.ForeignKey("ModelRouters", on_delete=models.CASCADE, related_name="rules", db_index=False)
    enabled = models.BooleanField(default=True)
    priority = models.IntegerField()
    type = models.TextField(default="calculated")
    title = models.TextField()
    description = models.TextField(null=True)
    condition_logic = models.TextField(null=True)
    conditions = models.TextField(null=True)
    route_provider = models.TextField()
    route_model = models.TextField()
    created_by = models.IntegerField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "model_router_rules"
        constraints = [
            models.UniqueConstraint(fields=["router", "title"], name="model_router_rules_router_id_title_key"),
        ]
        indexes = [
            models.Index(fields=["router"]),
            models.Index(fields=["router", "enabled", "priority"]),
        ]
