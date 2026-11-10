import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex, HashIndex
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


def uuid4_str() -> str:
    return str(uuid.uuid4())


def job_configuration_time_scope_default() -> list[str]:
    return ["NEW"]


def evaluation_rule_time_scope_default() -> list[str]:
    return ["NEW"]


def blob_storage_integration_export_field_groups_default() -> list[str]:
    return ["core", "basic", "time", "io", "metadata", "model", "usage", "prompt", "metrics", "tools", "trace_context"]


class ApiKeyScope(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"
    USER = "USER"


class SystemRole(models.TextChoices):
    OWNER = "OWNER"
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"
    VIEWER = "VIEWER"
    NONE = "NONE"
    LEGACY_PROJECT_API_KEY = "LEGACY_PROJECT_API_KEY"
    LEGACY_ORGANIZATION_API_KEY = "LEGACY_ORGANIZATION_API_KEY"
    SCORES_INGEST = "SCORES_INGEST"
    INGEST = "INGEST"
    AI_GATEWAY = "AI_GATEWAY"


class GatewayIngestionMode(models.TextChoices):
    NONE = "none"
    USAGE = "usage"
    FULL = "full"


class GatewayProvider(models.TextChoices):
    OPENAI = "openai"
    ANTHROPIC = "anthropic"
    BEDROCK = "bedrock"


class GatewayConnectionStatus(models.TextChoices):
    ENABLED = "enabled"
    DISABLED = "disabled"
    ERROR = "error"


class InAppAgentConversationVisibilityScope(models.TextChoices):
    PERSONAL = "PERSONAL"
    PROJECT = "PROJECT"


class Role(models.TextChoices):
    OWNER = "OWNER"
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"
    VIEWER = "VIEWER"
    NONE = "NONE"
    AI_GATEWAY = "AI_GATEWAY"


class ScoreConfigDataType(models.TextChoices):
    CATEGORICAL = "CATEGORICAL"
    NUMERIC = "NUMERIC"
    BOOLEAN = "BOOLEAN"
    TEXT = "TEXT"


class AnnotationQueueStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"


class AnnotationQueueObjectType(models.TextChoices):
    TRACE = "TRACE"
    OBSERVATION = "OBSERVATION"
    SESSION = "SESSION"


class DatasetStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    ARCHIVED = "ARCHIVED"


class CommentObjectType(models.TextChoices):
    TRACE = "TRACE"
    OBSERVATION = "OBSERVATION"
    SESSION = "SESSION"
    PROMPT = "PROMPT"


class NotificationChannel(models.TextChoices):
    EMAIL = "EMAIL"


class NotificationType(models.TextChoices):
    COMMENT_MENTION = "COMMENT_MENTION"


class AuditLogRecordType(models.TextChoices):
    USER = "USER"
    API_KEY = "API_KEY"


class EvalTemplateType(models.TextChoices):
    LLM_AS_JUDGE = "LLM_AS_JUDGE"
    CODE = "CODE"
    DECISION_MODEL = "DECISION_MODEL"
    FACET = "FACET"


class EvalTemplateSourceCodeLanguage(models.TextChoices):
    PYTHON = "PYTHON"
    TYPESCRIPT = "TYPESCRIPT"


class EvaluatorSourceCodeLanguage(models.TextChoices):
    PYTHON = "PYTHON"
    TYPESCRIPT = "TYPESCRIPT"


class JobType(models.TextChoices):
    EVAL = "EVAL"


class JobConfigState(models.TextChoices):
    ACTIVE = "ACTIVE"
    INACTIVE = "INACTIVE"


class EvaluatorBlockReason(models.TextChoices):
    LLM_CONNECTION_AUTH_INVALID = "LLM_CONNECTION_AUTH_INVALID"
    LLM_CONNECTION_BILLING_EXHAUSTED = "LLM_CONNECTION_BILLING_EXHAUSTED"
    LLM_CONNECTION_ENDPOINT_UNREACHABLE = "LLM_CONNECTION_ENDPOINT_UNREACHABLE"
    LLM_CONNECTION_MISSING = "LLM_CONNECTION_MISSING"
    DEFAULT_EVAL_MODEL_MISSING = "DEFAULT_EVAL_MODEL_MISSING"
    EVAL_MODEL_CONFIG_INVALID = "EVAL_MODEL_CONFIG_INVALID"
    EVAL_MODEL_UNAVAILABLE = "EVAL_MODEL_UNAVAILABLE"
    PROVIDER_ACCOUNT_NOT_READY = "PROVIDER_ACCOUNT_NOT_READY"


class JobExecutionStatus(models.TextChoices):
    COMPLETED = "COMPLETED"
    ERROR = "ERROR"
    PENDING = "PENDING"
    CANCELLED = "CANCELLED"
    DELAYED = "DELAYED"


class BlobStorageIntegrationFileType(models.TextChoices):
    JSON = "JSON"
    CSV = "CSV"
    JSONL = "JSONL"
    PARQUET = "PARQUET"


class BlobStorageIntegrationType(models.TextChoices):
    S3 = "S3"
    S3_COMPATIBLE = "S3_COMPATIBLE"
    AZURE_BLOB_STORAGE = "AZURE_BLOB_STORAGE"
    GOOGLE_CLOUD_STORAGE = "GOOGLE_CLOUD_STORAGE"


class ExternalMediaStorageIntegrationType(models.TextChoices):
    S3 = "S3"
    S3_COMPATIBLE = "S3_COMPATIBLE"


class StorageIntegrationAuthType(models.TextChoices):
    ACCESS_KEYS = "ACCESS_KEYS"
    ROLE_ARN = "ROLE_ARN"


class BlobStorageExportMode(models.TextChoices):
    FULL_HISTORY = "FULL_HISTORY"
    FROM_TODAY = "FROM_TODAY"
    FROM_CUSTOM_DATE = "FROM_CUSTOM_DATE"


class AnalyticsIntegrationExportSource(models.TextChoices):
    TRACES_OBSERVATIONS = "TRACES_OBSERVATIONS"
    TRACES_OBSERVATIONS_EVENTS = "TRACES_OBSERVATIONS_EVENTS"
    EVENTS = "EVENTS"


class MediaAssociationOrigin(models.TextChoices):
    UNKNOWN = "UNKNOWN"
    CLIENT_UPLOAD = "CLIENT_UPLOAD"
    INGESTION_MEDIA_EXTRACTION = "INGESTION_MEDIA_EXTRACTION"
    INGESTION_FIELD_OVERFLOW = "INGESTION_FIELD_OVERFLOW"


class DashboardWidgetViews(models.TextChoices):
    TRACES = "TRACES"
    OBSERVATIONS = "OBSERVATIONS"
    SCORES_NUMERIC = "SCORES_NUMERIC"
    SCORES_CATEGORICAL = "SCORES_CATEGORICAL"
    SCORES_BOOLEAN = "SCORES_BOOLEAN"


class DashboardWidgetChartType(models.TextChoices):
    LINE_TIME_SERIES = "LINE_TIME_SERIES"
    AREA_TIME_SERIES = "AREA_TIME_SERIES"
    BAR_TIME_SERIES = "BAR_TIME_SERIES"
    HORIZONTAL_BAR = "HORIZONTAL_BAR"
    VERTICAL_BAR = "VERTICAL_BAR"
    PIE = "PIE"
    NUMBER = "NUMBER"
    HISTOGRAM = "HISTOGRAM"
    PIVOT_TABLE = "PIVOT_TABLE"


class ActionType(models.TextChoices):
    WEBHOOK = "WEBHOOK"
    SLACK = "SLACK"
    GITHUB_DISPATCH = "GITHUB_DISPATCH"
    PAGERDUTY = "PAGERDUTY"


class ActionExecutionStatus(models.TextChoices):
    COMPLETED = "COMPLETED"
    ERROR = "ERROR"
    PENDING = "PENDING"
    CANCELLED = "CANCELLED"


class MonitorThresholdOperator(models.TextChoices):
    GT = "GT"
    GTE = "GTE"
    LT = "LT"
    LTE = "LTE"
    EQ = "EQ"
    NEQ = "NEQ"


class MonitorView(models.TextChoices):
    OBSERVATIONS = "OBSERVATIONS"
    SCORES_NUMERIC = "SCORES_NUMERIC"
    SCORES_CATEGORICAL = "SCORES_CATEGORICAL"
    SCORES_BOOLEAN = "SCORES_BOOLEAN"


class MonitorSeverity(models.TextChoices):
    PAUSED = "PAUSED"
    UNKNOWN = "UNKNOWN"
    NO_DATA = "NO_DATA"
    OK = "OK"
    WARNING = "WARNING"
    ALERT = "ALERT"


class MonitorStatus(models.TextChoices):
    PAUSED = "PAUSED"
    ACTIVE = "ACTIVE"
    ERROR_BAD_QUERY = "ERROR_BAD_QUERY"


class SurveyName(models.TextChoices):
    ORG_ONBOARDING = "org_onboarding"
    USER_ONBOARDING = "user_onboarding"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    expires_in = models.IntegerField(null=True)
    ext_expires_in = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    refresh_token_expires_in = models.IntegerField(null=True)
    created_at = models.IntegerField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_index=False)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "Session"


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(null=True, unique=True)
    email_verified = UtcDateTimeField(null=True)
    password = models.TextField(null=True)
    sessions_expired_at = UtcDateTimeField(null=True)
    image = models.TextField(null=True)
    admin = models.BooleanField(default=False)
    v4_beta_enabled = models.BooleanField(default=False)
    support_access = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    feature_flags = ArrayField(models.TextField(), default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "users"


class VerificationToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "verification_tokens"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="verification_tokens_identifier_token_key"),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    cloud_config = models.JSONField(null=True)
    metadata = models.JSONField(null=True)
    cloud_billing_cycle_anchor = UtcDateTimeField(null=True, default=timezone.now)
    cloud_billing_cycle_updated_at = UtcDateTimeField(null=True)
    cloud_current_cycle_usage = models.IntegerField(null=True)
    cloud_free_tier_usage_threshold_state = models.TextField(null=True)
    ai_features_enabled = models.BooleanField(default=False)
    ai_telemetry_enabled = models.BooleanField(default=True)
    access_audit_log_enabled = models.BooleanField(default=False)
    feature_flag_org_defaults = ArrayField(models.TextField(), default=list)
    sfdc_org_id = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "organizations"


class Project(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="projects", db_column="org_id", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    deleted_at = UtcDateTimeField(null=True)
    name = models.TextField()
    retention_days = models.IntegerField(null=True)
    has_traces = models.BooleanField(default=False)
    metadata = models.JSONField(null=True)
    home_dashboard = models.ForeignKey("Dashboard", on_delete=models.SET_NULL, related_name="home_dashboard_projects", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "projects"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    note = models.TextField(null=True)
    public_key = models.TextField(unique=True)
    hashed_secret_key = models.TextField(unique=True)
    fast_hashed_secret_key = models.TextField(null=True, unique=True)
    display_secret_key = models.TextField()
    last_used_at = UtcDateTimeField(null=True)
    expires_at = UtcDateTimeField(null=True)
    is_in_app_agent_key = models.BooleanField(default=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="api_keys", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="api_key", null=True, db_index=False)
    scope = models.TextField(choices=ApiKeyScope.choices, default=ApiKeyScope.PROJECT)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="api_keys_created", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_keys", null=True, db_index=False)
    created_by_api_key = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="api_keys_created", null=True, db_index=False)

    class Meta:
        db_table = "api_keys"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["project"]),
            models.Index(fields=["created_by_api_key"]),
            models.Index(fields=["user", "organization"]),
            models.Index(fields=["public_key"]),
            models.Index(fields=["hashed_secret_key"]),
            models.Index(fields=["fast_hashed_secret_key"]),
        ]


class RoleAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    owner_id = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="role_assignments", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="role_assignments", db_column="org_id", db_index=False)
    principal_id = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="role_assignments", null=True, db_index=False)
    api_key = models.ForeignKey("ApiKey", on_delete=models.CASCADE, related_name="role_assignments", null=True, db_index=False)
    role_id = models.TextField()
    system_role = models.TextField(choices=SystemRole.choices)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "role_assignments"
        constraints = [
            models.UniqueConstraint(fields=["owner_id", "principal_id", "role_id"], name="role_assignments_owner_id_principal_id_role_id_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["project"]),
            models.Index(fields=["principal_id"]),
            models.Index(fields=["user"]),
            models.Index(fields=["api_key"]),
        ]


class GatewayConfig(models.Model):
    organization = models.OneToOneField("Organization", on_delete=models.CASCADE, related_name="gateway_config", primary_key=True)
    default_ingestion_project = models.ForeignKey("Project", on_delete=models.SET_NULL, related_name="gateway_configs", null=True, db_index=False)
    ingestion_mode = models.TextField(choices=GatewayIngestionMode.choices, default=GatewayIngestionMode.USAGE)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "gateway_configs"


class GatewayAiConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="gateway_ai_connections", db_index=False)
    name = models.TextField()
    provider = models.TextField(choices=GatewayProvider.choices)
    encrypted_credential = models.TextField(db_column="encrypted_credentials")
    display_secret = models.TextField(db_column="display_secret_key")
    created_by_id = models.TextField(null=True)
    status = models.TextField(choices=GatewayConnectionStatus.choices, default=GatewayConnectionStatus.ENABLED)
    allowed_models = models.JSONField(null=True)
    custom_models = models.JSONField(default=list)
    config = models.JSONField(null=True)
    base_url = models.TextField(null=True)
    extra_headers = models.TextField(null=True)
    extra_header_keys = ArrayField(models.TextField(), default=list)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "gateway_ai_connections"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="gateway_ai_connections_organization_id_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "created_at", "id"]),
            models.Index(fields=["organization", "status", "created_at", "id"]),
        ]


class GatewayApiKeyAssociation(models.Model):
    api_key = models.OneToOneField("ApiKey", on_delete=models.CASCADE, related_name="gateway_association", primary_key=True)
    metadata = models.JSONField(default=dict)

    class Meta:
        db_table = "gateway_api_key_associations"


class InAppAgentConversation(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id")
    id = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="in_app_agent_conversation", db_index=False)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="in_app_agent_conversations", null=True, db_index=False)
    title = models.TextField(null=True)
    renamed_by_user_at = UtcDateTimeField(null=True)
    visibility_scope = models.TextField(choices=InAppAgentConversationVisibilityScope.choices, default=InAppAgentConversationVisibilityScope.PERSONAL)
    provider_session_id = models.TextField(null=True)
    always_allowed_tools = ArrayField(models.TextField(), default=list)
    deleted_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "in_app_agent_conversations"
        indexes = [
            models.Index(fields=["project", "created_by_user", "deleted_at", "updated_at", "id"]),
            models.Index(fields=["project", "updated_at"]),
        ]


class InAppAgentEvent(models.Model):
    pk = models.CompositePrimaryKey("project_id", "conversation_id", "sequence_number")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="in_app_agent_event", db_index=False)
    conversation_id = models.TextField()
    run_id = models.TextField()
    sequence_number = models.IntegerField()
    type = models.TextField()
    event = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "in_app_agent_events"
        indexes = [
            models.Index(fields=["project", "run_id"]),
        ]


class InAppAgentRun(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id")
    id = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="in_app_agent_run", db_index=False)
    conversation_id = models.TextField()
    triggered_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="in_app_agent_runs", null=True, db_index=False)
    model = models.TextField(null=True)
    mcp_api_key_id = models.TextField(null=True)
    error_code = models.TextField(null=True)
    error_message = models.TextField(null=True)
    finished_at = UtcDateTimeField(null=True)
    status = models.TextField(null=True)
    request = models.JSONField(null=True)
    claimed_at = UtcDateTimeField(null=True)
    heartbeat_at = UtcDateTimeField(null=True)
    cancel_requested_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "in_app_agent_runs"
        indexes = [
            models.Index(fields=["project", "conversation_id", "created_at"]),
        ]


class InAppAgentPendingToolApproval(models.Model):
    pk = models.CompositePrimaryKey("project_id", "conversation_id", "tool_call_id")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="in_app_agent_pending_tool_approval", db_index=False)
    conversation_id = models.TextField()
    tool_call_id = models.TextField()
    approval_fingerprint = models.TextField()
    expires_at = UtcDateTimeField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "in_app_agent_pending_tool_approvals"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class BackgroundMigration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(unique=True)
    script = models.TextField()
    args = models.JSONField()
    state = models.JSONField(default=dict)
    finished_at = UtcDateTimeField(null=True)
    failed_at = UtcDateTimeField(null=True)
    failed_reason = models.TextField(null=True)
    worker_id = models.TextField(null=True)
    locked_at = UtcDateTimeField(null=True)

    class Meta:
        db_table = "background_migrations"


class LlmApiKeys(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    provider = models.TextField()
    adapter = models.TextField()
    display_secret_key = models.TextField()
    secret_key = models.TextField()
    base_url = models.TextField(null=True)
    custom_models = ArrayField(models.TextField(), default=list)
    with_default_models = models.BooleanField(default=True)
    extra_headers = models.TextField(null=True)
    extra_header_keys = ArrayField(models.TextField(), default=list)
    config = models.JSONField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="llm_api_keys", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="organization_llm_api_keys", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "llm_api_keys"
        constraints = [
            models.UniqueConstraint(fields=["project", "provider"], name="llm_api_keys_project_id_provider_key"),
            models.UniqueConstraint(fields=["organization", "provider"], name="llm_api_keys_organization_id_provider_key"),
        ]


class OrganizationMembership(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="organization_memberships", db_column="org_id", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="organization_memberships", db_index=False)
    role = models.TextField(choices=Role.choices)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "organization_memberships"
        constraints = [
            models.UniqueConstraint(fields=["organization", "user"], name="organization_memberships_org_id_user_id_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class ProjectMembership(models.Model):
    pk = models.CompositePrimaryKey("project_id", "user_id")
    organization_membership = models.ForeignKey("OrganizationMembership", on_delete=models.CASCADE, related_name="project_memberships", db_column="org_membership_id", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="project_members", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="project_memberships", db_index=False)
    role = models.TextField(choices=Role.choices)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "project_memberships"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["project"]),
            models.Index(fields=["organization_membership"]),
        ]


class MembershipInvitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="membership_invitation", db_column="org_id", db_index=False)
    org_role = models.TextField(choices=Role.choices)
    project = models.ForeignKey("Project", on_delete=models.SET_NULL, related_name="invitations", null=True, db_index=False)
    project_role = models.TextField(choices=Role.choices, null=True)
    invited_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="invitations", null=True, db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "membership_invitations"
        constraints = [
            models.UniqueConstraint(fields=["email", "organization"], name="membership_invitations_email_org_id_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["email"]),
        ]


class TraceSession(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id")
    id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="sessions", db_index=False)
    bookmarked = models.BooleanField(default=False)
    public = models.BooleanField(default=False)
    environment = models.TextField(default="default")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "trace_sessions"
        indexes = [
            models.Index(fields=["project", "-created_at"]),
        ]


class ScoreConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="score_config", db_index=False)
    name = models.TextField()
    data_type = models.TextField(choices=ScoreConfigDataType.choices)
    is_archived = models.BooleanField(default=False)
    min_value = models.FloatField(null=True)
    max_value = models.FloatField(null=True)
    categories = models.JSONField(null=True)
    description = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "score_configs"
        constraints = [
            models.UniqueConstraint(fields=["id", "project"], name="score_configs_id_project_id_key"),
        ]
        indexes = [
            models.Index(fields=["data_type"]),
            models.Index(fields=["is_archived"]),
            models.Index(fields=["project"]),
            models.Index(fields=["categories"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
        ]


class AnnotationQueue(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    score_config_ids = ArrayField(models.TextField(), default=list)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="annotation_queue", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "annotation_queues"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="annotation_queues_project_id_name_key"),
        ]
        indexes = [
            models.Index(fields=["id", "project"]),
            models.Index(fields=["project", "created_at"]),
        ]


class AnnotationQueueItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    queue = models.ForeignKey("AnnotationQueue", on_delete=models.CASCADE, related_name="annotation_queue_item", db_index=False)
    object_id = models.TextField()
    object_type = models.TextField(choices=AnnotationQueueObjectType.choices)
    status = models.TextField(choices=AnnotationQueueStatus.choices, default=AnnotationQueueStatus.PENDING)
    locked_at = UtcDateTimeField(null=True)
    locked_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="annotated_locked_item", null=True, db_index=False)
    annotator_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="annotated_completed_item", null=True, db_index=False)
    completed_at = UtcDateTimeField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="annotation_queue_item", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "annotation_queue_items"
        indexes = [
            models.Index(fields=["id", "project"]),
            models.Index(fields=["project", "queue", "status"]),
            models.Index(fields=["object_id", "object_type", "project", "queue"]),
            models.Index(fields=["annotator_user"]),
            models.Index(fields=["created_at"]),
        ]


class AnnotationQueueAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="annotation_queue_assignment", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="annotation_queue_assignment", db_index=False)
    queue = models.ForeignKey("AnnotationQueue", on_delete=models.CASCADE, related_name="annotation_queue_assignment", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "annotation_queue_assignments"
        constraints = [
            models.UniqueConstraint(fields=["project", "queue", "user"], name="annotation_queue_assignments_project_id_queue_id_user_id_key"),
        ]


class CronJobs(models.Model):
    name = models.TextField(primary_key=True, default=None)
    last_run = UtcDateTimeField(null=True)
    job_started_at = UtcDateTimeField(null=True)
    state = models.TextField(null=True)

    class Meta:
        db_table = "cron_jobs"


class Dataset(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id")
    id = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="dataset", db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    remote_experiment_url = models.TextField(null=True)
    remote_experiment_payload = models.JSONField(null=True)
    remote_experiment_enabled = models.BooleanField(default=True)
    remote_experiment_secret_key = models.TextField(null=True)
    remote_experiment_display_secret_key = models.TextField(null=True)
    remote_experiment_request_headers = models.JSONField(null=True)
    remote_experiment_display_headers = models.JSONField(null=True)
    input_schema = models.JSONField(null=True)
    expected_output_schema = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "datasets"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="datasets_project_id_name_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
        ]


class DatasetItem(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id", "valid_from")
    id = models.TextField()
    project_id = models.TextField()
    status = models.TextField(choices=DatasetStatus.choices, null=True, default=DatasetStatus.ACTIVE)
    input = models.JSONField(null=True)
    expected_output = models.JSONField(null=True)
    metadata = models.JSONField(null=True)
    source_trace_id = models.TextField(null=True)
    source_observation_id = models.TextField(null=True)
    dataset_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    valid_from = UtcDateTimeField(default=timezone.now)
    valid_to = UtcDateTimeField(null=True)
    is_deleted = models.BooleanField(default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dataset_items"
        indexes = [
            models.Index(fields=["project_id", "valid_to"]),
            models.Index(fields=["project_id", "id", "valid_from"]),
            HashIndex(fields=["source_trace_id"]),
            HashIndex(fields=["source_observation_id"]),
            HashIndex(fields=["dataset_id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
        ]


class DatasetRuns(models.Model):
    pk = models.CompositePrimaryKey("id", "project_id")
    id = models.TextField()
    project_id = models.TextField()
    name = models.TextField()
    description = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    dataset_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dataset_runs"
        constraints = [
            models.UniqueConstraint(fields=["dataset_id", "project_id", "name"], name="dataset_runs_dataset_id_project_id_name_key"),
        ]
        indexes = [
            HashIndex(fields=["dataset_id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
        ]


class Comment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="comment", db_index=False)
    object_type = models.TextField(choices=CommentObjectType.choices)
    object_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    content = models.TextField()
    author_user_id = models.TextField(null=True)
    data_field = models.TextField(null=True)
    path = ArrayField(models.TextField(), default=list)
    range_start = ArrayField(models.IntegerField(), default=list)
    range_end = ArrayField(models.IntegerField(), default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "comments"
        indexes = [
            models.Index(fields=["project", "object_type", "object_id"]),
        ]


class CommentReaction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="comment_reactions", db_index=False)
    comment = models.ForeignKey("Comment", on_delete=models.CASCADE, related_name="reactions", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="comment_reactions", db_index=False)
    emoji = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "comment_reactions"
        constraints = [
            models.UniqueConstraint(fields=["comment", "user", "emoji"], name="comment_reactions_comment_id_user_id_emoji_key"),
        ]


class NotificationPreference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="notification_preferences", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="notification_preference", db_index=False)
    channel = models.TextField(choices=NotificationChannel.choices)
    type = models.TextField(choices=NotificationType.choices)
    enabled = models.BooleanField(default=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "notification_preferences"
        constraints = [
            models.UniqueConstraint(fields=["user", "project", "channel", "type"], name="notification_preferences_user_id_project_id_channel_type_key"),
        ]


class Prompt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="prompt", db_index=False)
    created_by = models.TextField()
    prompt = models.JSONField()
    name = models.TextField()
    version = models.IntegerField()
    type = models.TextField(default="text")
    is_active = models.BooleanField(null=True)
    config = models.JSONField(default=dict)
    tags = ArrayField(models.TextField(), default=list)
    labels = ArrayField(models.TextField(), default=list)
    commit_message = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "prompts"
        constraints = [
            models.UniqueConstraint(fields=["project", "name", "version"], name="prompts_project_id_name_version_key"),
        ]
        indexes = [
            models.Index(fields=["project", "id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
            GinIndex(fields=["tags"]),
        ]


class PromptDependency(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="prompt_dependency", db_index=False)
    parent = models.ForeignKey("Prompt", on_delete=models.CASCADE, related_name="prompt_dependency", db_index=False)
    child_name = models.TextField()
    child_label = models.TextField(null=True)
    child_version = models.IntegerField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "prompt_dependencies"
        indexes = [
            models.Index(fields=["project", "parent"]),
            models.Index(fields=["project", "child_name"]),
        ]


class PromptProtectedLabels(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="prompt_protected_labels", db_index=False)
    label = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "prompt_protected_labels"
        constraints = [
            models.UniqueConstraint(fields=["project", "label"], name="prompt_protected_labels_project_id_label_key"),
        ]


class Skill(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="skill", db_index=False)
    created_by = models.TextField()
    name = models.TextField()
    description = models.TextField()
    frontmatter = models.JSONField(default=dict)
    version = models.IntegerField()
    tags = ArrayField(models.TextField(), default=list)
    labels = ArrayField(models.TextField(), default=list)
    commit_message = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "skills"
        constraints = [
            models.UniqueConstraint(fields=["project", "name", "version"], name="skills_project_id_name_version_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
            GinIndex(fields=["tags"]),
            GinIndex(fields=["labels"]),
        ]


class SkillFile(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="skill_file", db_index=False)
    skill = models.ForeignKey("Skill", on_delete=models.CASCADE, related_name="files", db_index=False)
    blob = models.ForeignKey("SkillBlob", on_delete=models.RESTRICT, related_name="files", db_index=False)
    path = models.TextField()

    class Meta:
        db_table = "skill_files"
        constraints = [
            models.UniqueConstraint(fields=["project", "skill", "path"], name="skill_files_project_id_skill_id_path_key"),
        ]
        indexes = [
            models.Index(fields=["project", "blob"]),
        ]


class SkillBlob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="skill_blob", db_index=False)
    sha256_hash = models.CharField(max_length=44, db_column="sha_256_hash")
    content_type = models.TextField()
    content_length = models.IntegerField()
    content = models.TextField()

    class Meta:
        db_table = "skill_blobs"
        constraints = [
            models.UniqueConstraint(fields=["project", "sha256_hash"], name="skill_blobs_project_id_sha_256_hash_key"),
        ]


class Model(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="model", null=True, db_index=False)
    model_name = models.TextField()
    match_pattern = models.TextField()
    start_date = UtcDateTimeField(null=True)
    input_price = models.DecimalField(max_digits=65, decimal_places=30, null=True)
    output_price = models.DecimalField(max_digits=65, decimal_places=30, null=True)
    total_price = models.DecimalField(max_digits=65, decimal_places=30, null=True)
    unit = models.TextField(null=True)
    tokenizer_id = models.TextField(null=True)
    tokenizer_config = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "models"
        constraints = [
            models.UniqueConstraint(fields=["project", "model_name", "start_date", "unit"], name="models_project_id_model_name_start_date_unit_key"),
        ]
        indexes = [
            models.Index(fields=["model_name"]),
        ]


class Price(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    model = models.ForeignKey("Model", on_delete=models.CASCADE, related_name="price", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="price", null=True, db_index=False)
    pricing_tier = models.ForeignKey("PricingTier", on_delete=models.CASCADE, related_name="prices", db_index=False)
    usage_type = models.TextField()
    price = models.DecimalField(max_digits=65, decimal_places=30)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "prices"
        constraints = [
            models.UniqueConstraint(fields=["model", "usage_type", "pricing_tier"], name="prices_model_id_usage_type_pricing_tier_id_key"),
        ]
        indexes = [
            models.Index(fields=["pricing_tier"]),
        ]


class PricingTier(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    model = models.ForeignKey("Model", on_delete=models.CASCADE, related_name="pricing_tiers", db_index=False)
    name = models.TextField()
    is_default = models.BooleanField(default=False)
    priority = models.IntegerField()
    conditions = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "pricing_tiers"
        constraints = [
            models.UniqueConstraint(fields=["model", "priority"], name="pricing_tiers_model_id_priority_key"),
            models.UniqueConstraint(fields=["model", "name"], name="pricing_tiers_model_id_name_key"),
        ]


class AuditLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    type = models.TextField(choices=AuditLogRecordType.choices, default=AuditLogRecordType.USER)
    api_key_id = models.TextField(null=True)
    user_id = models.TextField(null=True)
    org_id = models.TextField()
    user_org_role = models.TextField(null=True)
    project_id = models.TextField(null=True)
    user_project_role = models.TextField(null=True)
    resource_type = models.TextField()
    resource_id = models.TextField()
    action = models.TextField()
    before = models.TextField(null=True)
    after = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "audit_logs"
        indexes = [
            models.Index(fields=["project_id"]),
            models.Index(fields=["api_key_id"]),
            models.Index(fields=["user_id"]),
            models.Index(fields=["org_id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
        ]


class AuditLogStaging(models.Model):
    id = models.UUIDField(primary_key=True)
    created_at = UtcDateTimeField(default=timezone.now)
    payload = models.JSONField()
    failed_at = UtcDateTimeField(null=True)
    last_error = models.TextField(null=True)

    class Meta:
        db_table = "audit_log_staging"


class EvalTemplate(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="eval_template", null=True, db_index=False)
    name = models.TextField()
    version = models.IntegerField()
    prompt = models.TextField(null=True)
    type = models.TextField(choices=EvalTemplateType.choices, default=EvalTemplateType.LLM_AS_JUDGE)
    partner = models.TextField(null=True)
    model = models.TextField(null=True)
    provider = models.TextField(null=True)
    model_params = models.JSONField(null=True)
    vars = ArrayField(models.TextField(), default=list)
    output_definition = models.JSONField(null=True, db_column="output_schema")
    source_code = models.CharField(max_length=262144, null=True)
    source_code_language = models.TextField(choices=EvalTemplateSourceCodeLanguage.choices, null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "eval_templates"
        constraints = [
            models.UniqueConstraint(fields=["project", "name", "version"], name="eval_templates_project_id_name_version_key"),
        ]
        indexes = [
            models.Index(fields=["project", "id"]),
        ]


class Evaluator(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="evaluator", db_index=False)
    name = models.TextField()
    type = models.TextField(choices=EvalTemplateType.choices)
    is_built_in = models.BooleanField(default=False)
    description = models.TextField(null=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="evaluators_created", null=True, db_index=False)
    blocked_at = UtcDateTimeField(null=True)
    block_reason = models.TextField(choices=EvaluatorBlockReason.choices, null=True)
    block_message = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "evaluators"
        indexes = [
            models.Index(fields=["project", "-created_at"]),
        ]


class EvaluatorVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    evaluator = models.ForeignKey("Evaluator", on_delete=models.CASCADE, related_name="versions", db_index=False)
    version = models.IntegerField()
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="evaluator_versions_created", null=True, db_index=False)
    prompt = models.TextField(null=True)
    prompt_messages = models.JSONField(null=True)
    partner = models.TextField(null=True)
    model = models.TextField(null=True)
    provider = models.TextField(null=True)
    model_params = models.JSONField(null=True)
    vars = ArrayField(models.TextField(), default=list)
    variable_mapping = models.JSONField(null=True)
    output_definition = models.JSONField(null=True)
    source_code = models.CharField(max_length=262144, null=True)
    source_code_language = models.TextField(choices=EvaluatorSourceCodeLanguage.choices, null=True)
    questions = models.JSONField(null=True)

    class Meta:
        db_table = "evaluator_versions"
        constraints = [
            models.UniqueConstraint(fields=["evaluator", "version"], name="evaluator_versions_evaluator_id_version_key"),
        ]


class JobConfiguration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="job_configuration", db_index=False)
    job_type = models.TextField(choices=JobType.choices)
    status = models.TextField(choices=JobConfigState.choices, default=JobConfigState.ACTIVE)
    blocked_at = UtcDateTimeField(null=True)
    block_reason = models.TextField(choices=EvaluatorBlockReason.choices, null=True)
    block_message = models.TextField(null=True)
    eval_template = models.ForeignKey("EvalTemplate", on_delete=models.SET_NULL, related_name="job_configuration", null=True, db_index=False)
    score_name = models.TextField()
    filter = models.JSONField()
    target_object = models.TextField()
    variable_mapping = models.JSONField()
    sampling = models.DecimalField(max_digits=65, decimal_places=30)
    delay = models.IntegerField()
    time_scope = ArrayField(models.TextField(), default=job_configuration_time_scope_default)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "job_configurations"
        indexes = [
            models.Index(fields=["project", "id"]),
        ]


class EvaluationRule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="evaluation_rule", db_index=False)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="evaluation_rules_created", null=True, db_index=False)
    name = models.TextField()
    status = models.TextField(choices=JobConfigState.choices, default=JobConfigState.ACTIVE)
    target_object = models.TextField()
    filter = models.JSONField()
    sampling = models.DecimalField(max_digits=65, decimal_places=30)
    delay = models.IntegerField()
    idle_time = models.IntegerField(null=True)
    time_scope = ArrayField(models.TextField(), default=evaluation_rule_time_scope_default)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "evaluation_rules"
        indexes = [
            models.Index(fields=["project", "-updated_at"]),
        ]


class TracingReadinessSettings(models.Model):
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="tracing_readiness_settings", primary_key=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="tracing_readiness_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="tracing_readiness_updated", db_column="updated_by", null=True, db_index=False)
    trace_idle_ms = models.IntegerField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "tracing_readiness_settings"


class EvaluationRuleEvaluatorAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="evaluation_rule_evaluator_assignment", db_index=False)
    evaluation_rule = models.ForeignKey("EvaluationRule", on_delete=models.CASCADE, related_name="assignments", db_index=False)
    evaluator = models.ForeignKey("Evaluator", on_delete=models.CASCADE, related_name="assignments", db_index=False)
    variable_mapping = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "evaluation_rule_evaluator_assignments"
        constraints = [
            models.UniqueConstraint(fields=["evaluation_rule", "evaluator"], name="evaluation_rule_assignments_rule_evaluator_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["evaluator"]),
        ]


class JobExecution(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="job_execution", db_index=False)
    job_configuration_id = models.TextField()
    job_template_id = models.TextField(null=True)
    status = models.TextField(choices=JobExecutionStatus.choices)
    start_time = UtcDateTimeField(null=True)
    end_time = UtcDateTimeField(null=True)
    error = models.TextField(null=True)
    job_input_trace_id = models.TextField(null=True)
    job_input_trace_timestamp = UtcDateTimeField(null=True)
    job_input_observation_id = models.TextField(null=True)
    job_input_dataset_item_id = models.TextField(null=True)
    job_input_dataset_item_valid_from = UtcDateTimeField(null=True)
    job_output_score_id = models.TextField(null=True)
    execution_trace_id = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "job_executions"
        indexes = [
            models.Index(fields=["project", "job_configuration_id", "job_input_trace_id"]),
            models.Index(fields=["project", "job_output_score_id"]),
            models.Index(fields=["job_configuration_id"]),
        ]


class DefaultLlmModel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="default_llm_model", db_index=False)
    llm_api_key = models.ForeignKey("LlmApiKeys", on_delete=models.CASCADE, related_name="default_llm_model", db_index=False)
    provider = models.TextField()
    adapter = models.TextField()
    model = models.TextField()
    model_params = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "default_llm_models"
        constraints = [
            models.UniqueConstraint(fields=["project"], name="default_llm_models_project_id_key"),
        ]


class TopicConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="topic_configs_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="topic_configs_updated", db_column="updated_by", null=True, db_index=False)
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="topic_config")
    enabled = models.BooleanField(default=False)
    blocked_at = UtcDateTimeField(null=True)
    block_reason = models.TextField(choices=EvaluatorBlockReason.choices, null=True)
    block_message = models.TextField(null=True)
    summary_llm_api_key = models.ForeignKey("LlmApiKeys", on_delete=models.SET_NULL, related_name="topic_summary_configs", null=True, db_index=False)
    summary_model = models.TextField(null=True)
    embedding_llm_api_key = models.ForeignKey("LlmApiKeys", on_delete=models.SET_NULL, related_name="topic_embedding_configs", null=True, db_index=False)
    embedding_model = models.TextField(null=True)
    embedding_dimensions = models.IntegerField(default=1024)
    clustering_llm_api_key = models.ForeignKey("LlmApiKeys", on_delete=models.SET_NULL, related_name="topic_clustering_configs", null=True, db_index=False)
    clustering_model = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "topic_configs"
        indexes = [
            models.Index(fields=["summary_llm_api_key"]),
            models.Index(fields=["embedding_llm_api_key"]),
            models.Index(fields=["clustering_llm_api_key"]),
        ]


class SsoConfig(models.Model):
    domain = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    auth_provider = models.TextField()
    auth_config = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "sso_configs"


class VerifiedDomain(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="verified_domains", db_index=False)
    domain = models.TextField()
    verification_token = models.TextField(unique=True, default=uuid4_str)
    verified_at = UtcDateTimeField(null=True)
    created_by_user_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "verified_domains"
        constraints = [
            models.UniqueConstraint(fields=["organization", "domain"], name="verified_domains_organization_id_domain_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class PosthogIntegration(models.Model):
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="posthog_integration", primary_key=True, db_index=False)
    encrypted_posthog_api_key = models.TextField()
    posthog_host_name = models.TextField()
    last_sync_at = UtcDateTimeField(null=True)
    enabled = models.BooleanField()
    created_at = UtcDateTimeField(default=timezone.now)
    export_source = models.TextField(choices=AnalyticsIntegrationExportSource.choices, default=AnalyticsIntegrationExportSource.TRACES_OBSERVATIONS)
    backfill = models.BooleanField(default=True)
    last_error = models.TextField(null=True)
    last_error_at = UtcDateTimeField(null=True)

    class Meta:
        db_table = "posthog_integrations"


class MixpanelIntegration(models.Model):
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="mixpanel_integration", primary_key=True, db_index=False)
    encrypted_mixpanel_project_token = models.TextField()
    mixpanel_region = models.TextField()
    last_sync_at = UtcDateTimeField(null=True)
    enabled = models.BooleanField()
    created_at = UtcDateTimeField(default=timezone.now)
    export_source = models.TextField(choices=AnalyticsIntegrationExportSource.choices, default=AnalyticsIntegrationExportSource.TRACES_OBSERVATIONS)
    backfill = models.BooleanField(default=True)

    class Meta:
        db_table = "mixpanel_integrations"


class BlobStorageIntegration(models.Model):
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="blob_storage_integration", primary_key=True, db_index=False)
    type = models.TextField(choices=BlobStorageIntegrationType.choices)
    bucket_name = models.TextField()
    prefix = models.TextField()
    auth_type = models.TextField(choices=StorageIntegrationAuthType.choices, default=StorageIntegrationAuthType.ACCESS_KEYS)
    access_key_id = models.TextField(null=True)
    secret_access_key = models.TextField(null=True)
    role_arn = models.TextField(null=True)
    external_id = models.TextField(null=True)
    region = models.TextField()
    endpoint = models.TextField(null=True)
    force_path_style = models.BooleanField()
    next_sync_at = UtcDateTimeField(null=True)
    last_sync_at = UtcDateTimeField(null=True)
    enabled = models.BooleanField()
    export_frequency = models.TextField()
    file_type = models.TextField(choices=BlobStorageIntegrationFileType.choices, default=BlobStorageIntegrationFileType.CSV)
    export_mode = models.TextField(choices=BlobStorageExportMode.choices, default=BlobStorageExportMode.FULL_HISTORY)
    export_start_date = UtcDateTimeField(null=True)
    export_source = models.TextField(choices=AnalyticsIntegrationExportSource.choices, default=AnalyticsIntegrationExportSource.TRACES_OBSERVATIONS)
    export_field_groups = ArrayField(models.TextField(), default=blob_storage_integration_export_field_groups_default)
    compressed = models.BooleanField(default=True)
    export_tuning = models.JSONField(null=True)
    run_started_at = UtcDateTimeField(null=True)
    backfill = models.BooleanField(default=True)
    last_error = models.TextField(null=True)
    last_error_at = UtcDateTimeField(null=True)
    last_failure_notification_sent_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "blob_storage_integrations"


class ExternalMediaStorageIntegration(models.Model):
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="external_media_storage_integration", primary_key=True)
    type = models.TextField(choices=ExternalMediaStorageIntegrationType.choices)
    bucket_name = models.TextField()
    prefix = models.TextField(null=True)
    auth_type = models.TextField(choices=StorageIntegrationAuthType.choices, default=StorageIntegrationAuthType.ACCESS_KEYS)
    access_key_id = models.TextField(null=True)
    secret_access_key = models.TextField(null=True)
    role_arn = models.TextField(null=True)
    external_id = models.TextField(null=True)
    region = models.TextField()
    endpoint = models.TextField(null=True)
    force_path_style = models.BooleanField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "external_media_storage_integrations"


class WebCalloutEndpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="web_callout_endpoints", db_index=False)
    name = models.TextField(default="Default")
    url = models.TextField()
    enabled = models.BooleanField(default=True)
    toast_message = models.TextField(default="Callout sent")
    request_headers = models.TextField(null=True)
    request_header_keys = ArrayField(models.TextField(), default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "web_callout_endpoints"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "enabled"]),
        ]


class BatchExport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="batch_export", db_index=False)
    user_id = models.TextField()
    finished_at = UtcDateTimeField(null=True)
    expires_at = UtcDateTimeField(null=True)
    name = models.TextField()
    status = models.TextField()
    query = models.JSONField()
    format = models.TextField()
    url = models.TextField(null=True)
    log = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "batch_exports"
        indexes = [
            models.Index(fields=["project", "user_id"]),
            models.Index(fields=["status"]),
        ]


class BatchAction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="batch_action", db_index=False)
    user_id = models.TextField()
    action_type = models.TextField()
    table_name = models.TextField()
    status = models.TextField()
    finished_at = UtcDateTimeField(null=True)
    query = models.JSONField()
    config = models.JSONField(null=True)
    total_count = models.IntegerField(null=True)
    processed_count = models.IntegerField(null=True)
    failed_count = models.IntegerField(null=True)
    log = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "batch_actions"
        indexes = [
            models.Index(fields=["project", "user_id"]),
            models.Index(fields=["status"]),
            models.Index(fields=["project", "action_type"]),
        ]


class Media(models.Model):
    pk = models.CompositePrimaryKey("project_id", "id")
    id = models.TextField()
    sha256_hash = models.CharField(max_length=44, db_column="sha_256_hash")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="media", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    uploaded_at = UtcDateTimeField(null=True)
    upload_http_status = models.IntegerField(null=True)
    upload_http_error = models.TextField(null=True)
    bucket_path = models.TextField()
    bucket_name = models.TextField()
    content_type = models.TextField()
    content_length = models.BigIntegerField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "media"
        constraints = [
            models.UniqueConstraint(fields=["project", "id"], name="media_project_id_id_key"),
            models.UniqueConstraint(fields=["project", "sha256_hash"], name="media_project_id_sha_256_hash_key"),
        ]
        indexes = [
            models.Index(fields=["project", "created_at"]),
        ]


class TraceMedia(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    media_id = models.TextField()
    trace_id = models.TextField()
    field = models.TextField()
    origin = models.TextField(choices=MediaAssociationOrigin.choices, default=MediaAssociationOrigin.UNKNOWN)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "trace_media"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "trace_id", "media_id", "field"], name="trace_media_project_id_trace_id_media_id_field_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "media_id"]),
        ]


class ObservationMedia(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    media_id = models.TextField()
    trace_id = models.TextField()
    observation_id = models.TextField()
    field = models.TextField()
    origin = models.TextField(choices=MediaAssociationOrigin.choices, default=MediaAssociationOrigin.UNKNOWN)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "observation_media"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "trace_id", "observation_id", "media_id", "field"], name="observation_media_project_id_trace_id_observation_id_media_id_field_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "media_id"]),
        ]


class DatasetItemMedia(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    media_id = models.TextField()
    dataset_id = models.TextField()
    dataset_item_id = models.TextField()
    dataset_item_valid_from = UtcDateTimeField(null=True)
    field = models.TextField()
    json_path = models.TextField(null=True)
    reference_string = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dataset_item_media"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "dataset_item_id", "dataset_item_valid_from", "field", "json_path"], name="dataset_item_media_item_version_field_path_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "media_id"]),
            models.Index(fields=["project_id", "dataset_id"]),
        ]


class BillingMeterBackup(models.Model):
    pk = models.CompositePrimaryKey("stripe_customer_id", "meter_id", "start_time", "end_time")
    stripe_customer_id = models.TextField()
    meter_id = models.TextField()
    start_time = UtcDateTimeField()
    end_time = UtcDateTimeField()
    aggregated_value = models.IntegerField()
    event_name = models.TextField()
    org_id = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "billing_meter_backups"
        constraints = [
            models.UniqueConstraint(fields=["stripe_customer_id", "meter_id", "start_time", "end_time"], name="billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_key"),
        ]
        indexes = [
            models.Index(fields=["stripe_customer_id", "meter_id", "start_time", "end_time"]),
        ]


class LlmSchema(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="llm_schema", db_index=False)
    name = models.TextField()
    description = models.TextField()
    schema = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "llm_schemas"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="llm_schemas_project_id_name_key"),
        ]


class LlmTool(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="llm_tool", db_index=False)
    name = models.TextField()
    description = models.TextField()
    parameters = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "llm_tools"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="llm_tools_project_id_name_key"),
        ]


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="dashboard_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="dashboard_updated", db_column="updated_by", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="dashboard", null=True, db_index=False)
    name = models.TextField()
    description = models.TextField()
    definition = models.JSONField()
    filters = models.JSONField(default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dashboards"


class DashboardWidget(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="dashboard_widgets_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="dashboard_widgets_updated", db_column="updated_by", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="dashboard_widget", null=True, db_index=False)
    name = models.TextField()
    description = models.TextField()
    view = models.TextField(choices=DashboardWidgetViews.choices)
    dimensions = models.JSONField()
    metrics = models.JSONField()
    filters = models.JSONField()
    chart_type = models.TextField(choices=DashboardWidgetChartType.choices)
    chart_config = models.JSONField()
    min_version = models.IntegerField(default=1)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dashboard_widgets"


class TableViewPreset(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="table_view_preset", db_index=False)
    name = models.TextField()
    table_name = models.TextField()
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="table_view_preset_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="table_view_preset_updated", db_column="updated_by", null=True, db_index=False)
    filters = models.JSONField()
    column_order = models.JSONField()
    column_visibility = models.JSONField()
    search_query = models.TextField(null=True)
    order_by = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "table_view_presets"
        constraints = [
            models.UniqueConstraint(fields=["project", "table_name", "name"], name="table_view_presets_project_id_table_name_name_key"),
        ]


class DefaultView(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="default_view", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="default_views", null=True, db_index=False)
    view_name = models.TextField()
    view_id = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "default_views"
        indexes = [
            models.Index(fields=["project", "view_name"]),
        ]


class Action(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="actions", db_index=False)
    type = models.TextField(choices=ActionType.choices)
    config = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "actions"
        indexes = [
            models.Index(fields=["project"]),
        ]


class Trigger(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="triggers", db_index=False)
    event_source = models.TextField(db_column="eventSource")
    event_actions = ArrayField(models.TextField(), db_column="eventActions")
    filter = models.JSONField(null=True)
    status = models.TextField(choices=JobConfigState.choices, default=JobConfigState.ACTIVE)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "triggers"
        indexes = [
            models.Index(fields=["project"]),
        ]


class Automation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    trigger = models.ForeignKey("Trigger", on_delete=models.CASCADE, related_name="automations", db_index=False)
    action = models.ForeignKey("Action", on_delete=models.CASCADE, related_name="automations", db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="automation", db_index=False)

    class Meta:
        db_table = "automations"
        indexes = [
            models.Index(fields=["project", "action", "trigger"]),
            models.Index(fields=["project", "name"]),
        ]


class AutomationExecution(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    source_id = models.TextField()
    automation = models.ForeignKey("Automation", on_delete=models.CASCADE, related_name="automation_execution", db_index=False)
    trigger = models.ForeignKey("Trigger", on_delete=models.CASCADE, related_name="automation_executions", db_index=False)
    action = models.ForeignKey("Action", on_delete=models.CASCADE, related_name="automation_executions", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="automation_executions", db_index=False)
    status = models.TextField(choices=ActionExecutionStatus.choices, default=ActionExecutionStatus.PENDING)
    input = models.JSONField()
    output = models.JSONField(null=True)
    started_at = UtcDateTimeField(null=True)
    finished_at = UtcDateTimeField(null=True)
    error = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "automation_executions"
        indexes = [
            models.Index(fields=["trigger"]),
            models.Index(fields=["action"]),
            models.Index(fields=["project"]),
        ]


class Monitor(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="monitors_created", db_column="created_by", null=True, db_index=False)
    updated_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="monitors_updated", db_column="updated_by", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="monitors", db_index=False)
    view = models.TextField(choices=MonitorView.choices)
    filters = models.JSONField()
    metric = models.JSONField()
    window_ms = models.BigIntegerField()
    cadence_ms = models.BigIntegerField()
    threshold_operator = models.TextField(choices=MonitorThresholdOperator.choices)
    alert_threshold = models.DecimalField(max_digits=65, decimal_places=30)
    warning_threshold = models.DecimalField(max_digits=65, decimal_places=30, null=True)
    severity = models.TextField(choices=MonitorSeverity.choices, default=MonitorSeverity.UNKNOWN)
    severity_changed_at = UtcDateTimeField(null=True)
    no_data = models.JSONField()
    renotify = models.JSONField()
    status = models.TextField(choices=MonitorStatus.choices, default=MonitorStatus.ACTIVE)
    scheduler_batch_id = models.BigIntegerField()
    next_run_at = UtcDateTimeField(null=True)
    last_published_at = UtcDateTimeField(null=True)
    last_claimed_at = UtcDateTimeField(null=True)
    last_completed_at = UtcDateTimeField(null=True)
    name = models.TextField()
    tags = ArrayField(models.TextField(), default=list)
    trigger_ids = ArrayField(models.TextField(), default=list)
    alerted_at = UtcDateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "monitors"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["scheduler_batch_id"]),
            models.Index(fields=["next_run_at", "scheduler_batch_id"], name="monitors_scheduler_tick_idx"),
        ]


class SlackIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="slack_integration")
    team_id = models.TextField()
    team_name = models.TextField()
    bot_token = models.TextField()
    bot_user_id = models.TextField()
    installer_user_id = models.TextField(null=True)
    installer_slack_user_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "slack_integrations"
        indexes = [
            models.Index(fields=["team_id"]),
        ]


class PendingDeletion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="pending_deletion", db_index=False)
    object = models.TextField()
    object_id = models.TextField()
    is_deleted = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "pending_deletions"
        indexes = [
            models.Index(fields=["project", "object", "is_deleted", "object_id", "id"]),
            models.Index(fields=["object_id", "object"]),
        ]


class Survey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    survey_name = models.TextField(choices=SurveyName.choices)
    response = models.JSONField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="surveys", null=True, db_index=False)
    user_email = models.TextField(null=True)
    org = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="surveys", null=True, db_index=False)

    class Meta:
        db_table = "surveys"


class CloudSpendAlert(models.Model):
    id = models.TextField(primary_key=True, default=None)
    org = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="cloud_spend_alerts", db_index=False)
    title = models.TextField()
    threshold = models.DecimalField(max_digits=65, decimal_places=30)
    triggered_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "cloud_spend_alerts"
        indexes = [
            models.Index(fields=["org"]),
        ]


class TopicClusteringRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="topic_clustering_runs", db_index=False)
    facet_id = models.TextField()
    facet_version = models.IntegerField()
    status = models.TextField(default="pending")
    config = models.JSONField(default=dict)
    error = models.TextField(null=True)
    created_at = models.DateTimeField(default=timezone.now)
    started_at = models.DateTimeField(null=True)
    finished_at = models.DateTimeField(null=True)
    topic_version_ids = ArrayField(models.TextField(), default=list)
    rule_id = models.TextField(null=True)
    batch_action_id = models.TextField(null=True)

    class Meta:
        db_table = "topic_clustering_runs"
        indexes = [
            models.Index(fields=["project", "facet_id", "status", "-facet_version", "-created_at", "-id"]),
            models.Index(fields=["project", "rule_id", "facet_id", "status"]),
            models.Index(fields=["project", "batch_action_id"]),
        ]
