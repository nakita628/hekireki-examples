from datetime import datetime
from datetime import timezone as dt_timezone
from decimal import Decimal
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import BrinIndex, GinIndex
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


def project_alert_channel_environment_types_default() -> list[str]:
    return [RuntimeEnvironmentType.STAGING, RuntimeEnvironmentType.PRODUCTION]


class AuthenticationMethod(models.TextChoices):
    GITHUB = "GITHUB"
    MAGIC_LINK = "MAGIC_LINK"
    GOOGLE = "GOOGLE"
    SSO = "SSO"


class OrganizationAccessTokenType(models.TextChoices):
    USER = "USER"
    SYSTEM = "SYSTEM"


class OrgMemberRole(models.TextChoices):
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"


class RuntimeEnvironmentType(models.TextChoices):
    PRODUCTION = "PRODUCTION"
    STAGING = "STAGING"
    DEVELOPMENT = "DEVELOPMENT"
    PREVIEW = "PREVIEW"


class EnvironmentPauseSource(models.TextChoices):
    BILLING_LIMIT = "BILLING_LIMIT"


class ProjectVersion(models.TextChoices):
    V2 = "V2"
    V3 = "V3"


class SecretStoreProvider(models.TextChoices):
    DATABASE = "DATABASE"
    AWS_PARAM_STORE = "AWS_PARAM_STORE"


class OidcSigningKeyStatus(models.TextChoices):
    STAGED = "STAGED"
    ACTIVE = "ACTIVE"
    RETIRED = "RETIRED"


class OidcSigningKeyPurpose(models.TextChoices):
    EXTERNAL_OIDC = "EXTERNAL_OIDC"
    INTERNAL_RUN = "INTERNAL_RUN"


class TaskTriggerSource(models.TextChoices):
    STANDARD = "STANDARD"
    SCHEDULED = "SCHEDULED"
    AGENT = "AGENT"
    WEBHOOK = "WEBHOOK"


class WebhookEndpointStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    INACTIVE = "INACTIVE"
    DELETING = "DELETING"


class WebhookDeliveryStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    SUCCEEDED = "SUCCEEDED"
    FAILED = "FAILED"
    FILTERED = "FILTERED"
    UNMATCHED = "UNMATCHED"


class TaskRunStatus(models.TextChoices):
    DELAYED = "DELAYED"
    PENDING = "PENDING"
    PENDING_VERSION = "PENDING_VERSION"
    WAITING_FOR_DEPLOY = "WAITING_FOR_DEPLOY"
    DEQUEUED = "DEQUEUED"
    EXECUTING = "EXECUTING"
    WAITING_TO_RESUME = "WAITING_TO_RESUME"
    RETRYING_AFTER_FAILURE = "RETRYING_AFTER_FAILURE"
    PAUSED = "PAUSED"
    CANCELED = "CANCELED"
    INTERRUPTED = "INTERRUPTED"
    COMPLETED_SUCCESSFULLY = "COMPLETED_SUCCESSFULLY"
    COMPLETED_WITH_ERRORS = "COMPLETED_WITH_ERRORS"
    SYSTEM_FAILURE = "SYSTEM_FAILURE"
    CRASHED = "CRASHED"
    EXPIRED = "EXPIRED"
    TIMED_OUT = "TIMED_OUT"


class RunEngineVersion(models.TextChoices):
    V1 = "V1"
    V2 = "V2"


class TaskRunExecutionStatus(models.TextChoices):
    RUN_CREATED = "RUN_CREATED"
    DELAYED = "DELAYED"
    QUEUED = "QUEUED"
    QUEUED_EXECUTING = "QUEUED_EXECUTING"
    PENDING_EXECUTING = "PENDING_EXECUTING"
    EXECUTING = "EXECUTING"
    EXECUTING_WITH_WAITPOINTS = "EXECUTING_WITH_WAITPOINTS"
    SUSPENDED = "SUSPENDED"
    PENDING_CANCEL = "PENDING_CANCEL"
    FINISHED = "FINISHED"


class TaskRunCheckpointType(models.TextChoices):
    DOCKER = "DOCKER"
    KUBERNETES = "KUBERNETES"
    COMPUTE = "COMPUTE"


class WaitpointType(models.TextChoices):
    RUN = "RUN"
    DATETIME = "DATETIME"
    MANUAL = "MANUAL"
    BATCH = "BATCH"


class WaitpointStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"


class WorkerInstanceGroupType(models.TextChoices):
    MANAGED = "MANAGED"
    UNMANAGED = "UNMANAGED"


class WorkloadType(models.TextChoices):
    CONTAINER = "CONTAINER"
    MICROVM = "MICROVM"


class TaskRunAttemptStatus(models.TextChoices):
    PENDING = "PENDING"
    EXECUTING = "EXECUTING"
    PAUSED = "PAUSED"
    FAILED = "FAILED"
    CANCELED = "CANCELED"
    COMPLETED = "COMPLETED"


class TaskEventLevel(models.TextChoices):
    TRACE = "TRACE"
    DEBUG = "DEBUG"
    LOG = "LOG"
    INFO = "INFO"
    WARN = "WARN"
    ERROR = "ERROR"


class TaskEventKind(models.TextChoices):
    UNSPECIFIED = "UNSPECIFIED"
    INTERNAL = "INTERNAL"
    SERVER = "SERVER"
    CLIENT = "CLIENT"
    PRODUCER = "PRODUCER"
    CONSUMER = "CONSUMER"
    UNRECOGNIZED = "UNRECOGNIZED"
    LOG = "LOG"


class TaskEventStatus(models.TextChoices):
    UNSET = "UNSET"
    OK = "OK"
    ERROR = "ERROR"
    UNRECOGNIZED = "UNRECOGNIZED"


class TaskQueueType(models.TextChoices):
    VIRTUAL = "VIRTUAL"
    NAMED = "NAMED"


class TaskQueueVersion(models.TextChoices):
    V1 = "V1"
    V2 = "V2"


class TaskQueueConcurrencyVersion(models.TextChoices):
    V1 = "V1"
    V2 = "V2"


class TaskQueueRole(models.TextChoices):
    QUEUE = "QUEUE"
    LIMIT = "LIMIT"


class BatchTaskRunStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    COMPLETED = "COMPLETED"
    PARTIAL_FAILED = "PARTIAL_FAILED"
    ABORTED = "ABORTED"


class BatchTaskRunItemStatus(models.TextChoices):
    PENDING = "PENDING"
    FAILED = "FAILED"
    CANCELED = "CANCELED"
    COMPLETED = "COMPLETED"


class CheckpointType(models.TextChoices):
    DOCKER = "DOCKER"
    KUBERNETES = "KUBERNETES"


class CheckpointRestoreEventType(models.TextChoices):
    CHECKPOINT = "CHECKPOINT"
    RESTORE = "RESTORE"


class WorkerDeploymentType(models.TextChoices):
    MANAGED = "MANAGED"
    UNMANAGED = "UNMANAGED"
    V1 = "V1"


class WorkerDeploymentStatus(models.TextChoices):
    PENDING = "PENDING"
    INSTALLING = "INSTALLING"
    BUILDING = "BUILDING"
    DEPLOYING = "DEPLOYING"
    DEPLOYED = "DEPLOYED"
    FAILED = "FAILED"
    CANCELED = "CANCELED"
    TIMED_OUT = "TIMED_OUT"


class ScheduleType(models.TextChoices):
    DECLARATIVE = "DECLARATIVE"
    IMPERATIVE = "IMPERATIVE"


class ScheduleGeneratorType(models.TextChoices):
    CRON = "CRON"


class ProjectAlertChannelType(models.TextChoices):
    EMAIL = "EMAIL"
    SLACK = "SLACK"
    WEBHOOK = "WEBHOOK"


class ProjectAlertType(models.TextChoices):
    TASK_RUN = "TASK_RUN"
    TASK_RUN_ATTEMPT = "TASK_RUN_ATTEMPT"
    DEPLOYMENT_FAILURE = "DEPLOYMENT_FAILURE"
    DEPLOYMENT_SUCCESS = "DEPLOYMENT_SUCCESS"
    ERROR_GROUP = "ERROR_GROUP"
    DASHBOARD_AGENT_WATCH = "DASHBOARD_AGENT_WATCH"


class ProjectAlertStatus(models.TextChoices):
    PENDING = "PENDING"
    SENT = "SENT"
    FAILED = "FAILED"


class IntegrationService(models.TextChoices):
    SLACK = "SLACK"
    VERCEL = "VERCEL"


class BulkActionType(models.TextChoices):
    CANCEL = "CANCEL"
    REPLAY = "REPLAY"


class BulkActionStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"
    ABORTED = "ABORTED"


class BulkActionNotificationType(models.TextChoices):
    NONE = "NONE"
    EMAIL = "EMAIL"


class BulkActionItemStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"


class GithubRepositorySelection(models.TextChoices):
    ALL = "ALL"
    SELECTED = "SELECTED"


class ImpersonationAuditLogAction(models.TextChoices):
    START = "START"
    STOP = "STOP"


class SupportAccessMode(models.TextChoices):
    ALLOW = "ALLOW"
    REQUIRES_REQUEST = "REQUIRES_REQUEST"


class SupportAccessRequestStatus(models.TextChoices):
    PENDING = "PENDING"
    APPROVED = "APPROVED"
    CANCELLED = "CANCELLED"


class CustomerQuerySource(models.TextChoices):
    DASHBOARD = "DASHBOARD"
    API = "API"


class CustomerQueryScope(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"
    ENVIRONMENT = "ENVIRONMENT"


class PlatformNotificationSurface(models.TextChoices):
    WEBAPP = "WEBAPP"
    CLI = "CLI"


class PlatformNotificationScope(models.TextChoices):
    USER = "USER"
    PROJECT = "PROJECT"
    ORGANIZATION = "ORGANIZATION"
    GLOBAL = "GLOBAL"


class ErrorGroupStatus(models.TextChoices):
    UNRESOLVED = "UNRESOLVED"
    RESOLVED = "RESOLVED"
    IGNORED = "IGNORED"


class DataStoreKind(models.TextChoices):
    CLICKHOUSE = "CLICKHOUSE"


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField(unique=True)
    pending_email = models.TextField(null=True, db_column="pendingEmail")
    authentication_method = models.TextField(choices=AuthenticationMethod.choices, db_column="authenticationMethod")
    authentication_profile = models.JSONField(null=True, db_column="authenticationProfile")
    authentication_extra_params = models.JSONField(null=True, db_column="authenticationExtraParams")
    auth_identifier = models.TextField(null=True, unique=True, db_column="authIdentifier")
    display_name = models.TextField(null=True, db_column="displayName")
    name = models.TextField(null=True)
    avatar_url = models.TextField(null=True, db_column="avatarUrl")
    admin = models.BooleanField(default=False)
    dashboard_preferences = models.JSONField(null=True, db_column="dashboardPreferences")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    is_on_cloud_waitlist = models.BooleanField(db_column="isOnCloudWaitlist", default=False)
    feature_cloud = models.BooleanField(db_column="featureCloud", default=False)
    is_on_hosted_repo_waitlist = models.BooleanField(db_column="isOnHostedRepoWaitlist", default=False)
    marketing_emails = models.BooleanField(db_column="marketingEmails", default=True)
    confirmed_basic_details = models.BooleanField(db_column="confirmedBasicDetails", default=False)
    referral_source = models.TextField(null=True, db_column="referralSource")
    onboarding_data = models.JSONField(null=True, db_column="onboardingData")
    mfa_enabled_at = UtcDateTimeField(null=True, db_column="mfaEnabledAt")
    mfa_secret_reference = models.ForeignKey("SecretReference", on_delete=models.SET_NULL, related_name="user", db_column="mfaSecretReferenceId", null=True, db_index=False)
    mfa_last_used_code = models.TextField(null=True, db_column="mfaLastUsedCode")
    session_duration = models.IntegerField(db_column="sessionDuration", default=31556952)
    next_session_end = UtcDateTimeField(null=True, db_column="nextSessionEnd")
    invitation_code = models.ForeignKey("InvitationCode", on_delete=models.SET_NULL, related_name="users", db_column="invitationCodeId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class MfaBackupCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    code = models.TextField()
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="backup_codes", db_column="userId", db_index=False)
    used_at = UtcDateTimeField(null=True, db_column="usedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MfaBackupCode"
        constraints = [
            models.UniqueConstraint(fields=["user", "code"], name="MfaBackupCode_userId_code_key"),
        ]


class InvitationCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    code = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "InvitationCode"


class AuthorizationCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    code = models.TextField(unique=True)
    personal_access_token = models.ForeignKey("PersonalAccessToken", on_delete=models.CASCADE, related_name="authorization_codes", db_column="personalAccessTokenId", null=True, db_index=False)
    requester_ip = models.TextField(null=True, db_column="requesterIp")
    code_challenge = models.TextField(null=True, db_column="codeChallenge")
    callback_port = models.IntegerField(null=True, db_column="callbackPort")
    approval_code_hash = models.TextField(null=True, db_column="approvalCodeHash")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthorizationCode"


class PersonalAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    encrypted_token = models.JSONField(db_column="encryptedToken")
    obfuscated_token = models.TextField(db_column="obfuscatedToken")
    hashed_token = models.TextField(unique=True, db_column="hashedToken")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="personal_access_tokens", db_column="userId", db_index=False)
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    last_accessed_at = UtcDateTimeField(null=True, db_column="lastAccessedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PersonalAccessToken"
        indexes = [
            models.Index(fields=["user"]),
        ]


class OrganizationAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    type = models.TextField(choices=OrganizationAccessTokenType.choices, default=OrganizationAccessTokenType.USER)
    hashed_token = models.TextField(unique=True, db_column="hashedToken")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="organization_access_tokens", db_column="organizationId", db_index=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    last_accessed_at = UtcDateTimeField(null=True, db_column="lastAccessedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationAccessToken"
        indexes = [
            models.Index(fields=["organization", "type", "-created_at"]),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField(unique=True)
    title = models.TextField()
    maximum_execution_time_per_run_in_ms = models.IntegerField(db_column="maximumExecutionTimePerRunInMs", default=900000)
    maximum_concurrency_limit = models.IntegerField(db_column="maximumConcurrencyLimit", default=10)
    maximum_schedules_limit = models.IntegerField(db_column="maximumSchedulesLimit", default=5)
    maximum_dev_queue_size = models.IntegerField(null=True, db_column="maximumDevQueueSize")
    maximum_deployed_queue_size = models.IntegerField(null=True, db_column="maximumDeployedQueueSize")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    company_size = models.TextField(null=True, db_column="companySize")
    onboarding_data = models.JSONField(null=True, db_column="onboardingData")
    avatar = models.JSONField(null=True)
    runs_enabled = models.BooleanField(db_column="runsEnabled", default=True)
    is_activated = models.BooleanField(db_column="v3Enabled", default=False)
    v2_enabled = models.BooleanField(db_column="v2Enabled", default=False)
    v2_marqs_enabled = models.BooleanField(db_column="v2MarqsEnabled", default=False)
    has_requested_v3 = models.BooleanField(db_column="hasRequestedV3", default=False)
    api_rate_limiter_config = models.JSONField(null=True, db_column="apiRateLimiterConfig")
    realtime_rate_limiter_config = models.JSONField(null=True, db_column="realtimeRateLimiterConfig")
    batch_rate_limit_config = models.JSONField(null=True, db_column="batchRateLimitConfig")
    batch_queue_concurrency_config = models.JSONField(null=True, db_column="batchQueueConcurrencyConfig")
    webhook_limits_config = models.JSONField(null=True, db_column="webhookLimitsConfig")
    feature_flags = models.JSONField(null=True, db_column="featureFlags")
    maximum_project_count = models.IntegerField(db_column="maximumProjectCount", default=25)
    max_session_duration = models.IntegerField(null=True, db_column="maxSessionDuration")
    stream_basin_name = models.TextField(null=True, db_column="streamBasinName")
    support_access_mode = models.TextField(choices=SupportAccessMode.choices, db_column="supportAccessMode", default=SupportAccessMode.ALLOW)
    support_access_mode_updated_at = UtcDateTimeField(null=True, db_column="supportAccessModeUpdatedAt")
    support_access_mode_updated_by_id = models.TextField(null=True, db_column="supportAccessModeUpdatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organization"


class OrgMember(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="members", db_column="organizationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="org_memberships", db_column="userId", db_index=False)
    role = models.TextField(choices=OrgMemberRole.choices, default=OrgMemberRole.MEMBER)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrgMember"
        constraints = [
            models.UniqueConstraint(fields=["organization", "user"], name="OrgMember_organizationId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class OrgMemberInvite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    email = models.TextField()
    role = models.TextField(choices=OrgMemberRole.choices, default=OrgMemberRole.MEMBER)
    rbac_role_id = models.TextField(null=True, db_column="rbacRoleId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="invites", db_column="organizationId", db_index=False)
    inviter = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sent_invites", db_column="inviterId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrgMemberInvite"
        constraints = [
            models.UniqueConstraint(fields=["organization", "email"], name="OrgMemberInvite_organizationId_email_key"),
        ]


class RuntimeEnvironment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField()
    api_key = models.TextField(unique=True, db_column="apiKey")
    root_api_key_hidden_at = UtcDateTimeField(null=True, db_column="rootApiKeyHiddenAt")
    pk_api_key = models.TextField(unique=True, db_column="pkApiKey")
    type = models.TextField(choices=RuntimeEnvironmentType.choices, default=RuntimeEnvironmentType.DEVELOPMENT)
    is_branchable_environment = models.BooleanField(db_column="isBranchableEnvironment", default=False)
    branch_name = models.TextField(null=True, db_column="branchName")
    parent_environment = models.ForeignKey("self", on_delete=models.CASCADE, related_name="child_environments", db_column="parentEnvironmentId", null=True, db_index=False)
    preview_auto_archive_after_days = models.IntegerField(null=True, db_column="previewAutoArchiveAfterDays")
    preview_auto_archive_excluded_branches = ArrayField(models.TextField(), db_column="previewAutoArchiveExcludedBranches", default=list)
    preview_auto_archive_next_check_at = UtcDateTimeField(null=True, db_column="previewAutoArchiveNextCheckAt")
    preview_auto_archive_cursor_created_at = UtcDateTimeField(null=True, db_column="previewAutoArchiveCursorCreatedAt")
    preview_auto_archive_cursor_id = models.TextField(null=True, db_column="previewAutoArchiveCursorId")
    git = models.JSONField(null=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    shortcode = models.TextField()
    maximum_concurrency_limit = models.IntegerField(db_column="maximumConcurrencyLimit", default=5)
    concurrency_limit_burst_factor = models.DecimalField(max_digits=4, decimal_places=2, db_column="concurrencyLimitBurstFactor", default=Decimal("2"))
    paused = models.BooleanField(default=False)
    pause_source = models.TextField(choices=EnvironmentPauseSource.choices, null=True, db_column="pauseSource")
    auto_enable_internal_sources = models.BooleanField(db_column="autoEnableInternalSources", default=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="environments", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="environments", db_column="projectId", db_index=False)
    org_member = models.ForeignKey("OrgMember", on_delete=models.SET_NULL, related_name="environments", db_column="orgMemberId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    built_in_environment_variable_overrides = models.JSONField(null=True, db_column="builtInEnvironmentVariableOverrides")
    tunnel_id = models.TextField(null=True, db_column="tunnelId")
    current_session = models.ForeignKey("RuntimeEnvironmentSession", on_delete=models.SET_NULL, related_name="current_environments", db_column="currentSessionId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RuntimeEnvironment"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug", "org_member"], name="RuntimeEnvironment_projectId_slug_orgMemberId_key"),
            models.UniqueConstraint(fields=["project", "shortcode"], name="RuntimeEnvironment_projectId_shortcode_key"),
        ]
        indexes = [
            models.Index(fields=["parent_environment"]),
            models.Index(fields=["project"]),
            models.Index(fields=["organization"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    key_hash = models.TextField(unique=True)
    last_four = models.TextField()
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="api_keys", db_index=False)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_api_keys", db_column="created_by_user_id", null=True, db_index=False)
    preset_id = models.TextField(null=True)
    scopes = ArrayField(models.TextField())
    last_used_at = UtcDateTimeField(null=True)
    revoked_at = UtcDateTimeField(null=True)
    expires_at = UtcDateTimeField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    created_at = UtcDateTimeField(default=timezone.now)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "api_keys"
        indexes = [
            models.Index(fields=["runtime_environment", "revoked_at", "-created_at"]),
        ]


class RevokedApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    api_key = models.TextField(db_column="apiKey")
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="revoked_api_keys", db_column="runtimeEnvironmentId", db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "RevokedApiKey"
        indexes = [
            models.Index(fields=["api_key"]),
            models.Index(fields=["runtime_environment"]),
        ]


class Project(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField(unique=True)
    name = models.TextField()
    external_ref = models.TextField(unique=True, db_column="externalRef")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="projects", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    initialized_at = UtcDateTimeField(null=True, db_column="initializedAt")
    default_runtime = models.TextField(null=True, db_column="defaultRuntime")
    version = models.TextField(choices=ProjectVersion.choices, default=ProjectVersion.V2)
    engine = models.TextField(choices=RunEngineVersion.choices, default=RunEngineVersion.V1)
    builder_project_id = models.TextField(null=True, db_column="builderProjectId")
    default_worker_group = models.ForeignKey("WorkerInstanceGroup", on_delete=models.SET_NULL, related_name="default_for_projects", db_column="defaultWorkerGroupId", null=True, db_index=False)
    allowed_worker_queues = ArrayField(models.TextField(), db_column="allowedMasterQueues", default=list)
    build_settings = models.JSONField(null=True, db_column="buildSettings")
    onboarding_data = models.JSONField(null=True, db_column="onboardingData")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Project"


class SecretReference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    key = models.TextField(unique=True)
    provider = models.TextField(choices=SecretStoreProvider.choices, default=SecretStoreProvider.DATABASE)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SecretReference"


class OidcSigningKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    kid = models.TextField(unique=True)
    purpose = models.TextField(choices=OidcSigningKeyPurpose.choices, default=OidcSigningKeyPurpose.EXTERNAL_OIDC)
    status = models.TextField(choices=OidcSigningKeyStatus.choices)
    public_jwk = models.JSONField(db_column="publicJwk")
    ready_at = UtcDateTimeField(db_column="readyAt")
    public_until = UtcDateTimeField(null=True, db_column="publicUntil")
    generated_by_user_id = models.TextField(db_column="generatedByUserId")
    activated_at = UtcDateTimeField(null=True, db_column="activatedAt")
    activated_by_user_id = models.TextField(null=True, db_column="activatedByUserId")
    retired_at = UtcDateTimeField(null=True, db_column="retiredAt")
    retired_by_user_id = models.TextField(null=True, db_column="retiredByUserId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OidcSigningKey"


class DataMigration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataMigration"


class BackgroundWorker(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    engine = models.TextField(choices=RunEngineVersion.choices, default=RunEngineVersion.V1)
    content_hash = models.TextField(db_column="contentHash")
    sdk_version = models.TextField(db_column="sdkVersion", default="unknown")
    cli_version = models.TextField(db_column="cliVersion", default="unknown")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="background_workers", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="background_workers", db_column="runtimeEnvironmentId", db_index=False)
    version = models.TextField()
    metadata = models.JSONField()
    runtime = models.TextField(null=True)
    runtime_version = models.TextField(null=True, db_column="runtimeVersion")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    files: "models.ManyToManyField[BackgroundWorkerFile, BackgroundWorkerToBackgroundWorkerFile]" = models.ManyToManyField("BackgroundWorkerFile", through="BackgroundWorkerToBackgroundWorkerFile", related_name="background_workers")
    queues: "models.ManyToManyField[TaskQueue, BackgroundWorkerToTaskQueue]" = models.ManyToManyField("TaskQueue", through="BackgroundWorkerToTaskQueue", related_name="workers")
    worker_group = models.ForeignKey("WorkerInstanceGroup", on_delete=models.SET_NULL, related_name="background_workers", db_column="workerGroupId", null=True, db_index=False)
    supports_lazy_attempts = models.BooleanField(db_column="supportsLazyAttempts", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BackgroundWorker"
        constraints = [
            models.UniqueConstraint(fields=["project", "runtime_environment", "version"], name="BackgroundWorker_projectId_runtimeEnvironmentId_version_key"),
        ]
        indexes = [
            models.Index(fields=["runtime_environment"]),
            models.Index(fields=["runtime_environment", "-created_at"]),
        ]


class BackgroundWorkerFile(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    file_path = models.TextField(db_column="filePath")
    content_hash = models.TextField(db_column="contentHash")
    contents = models.BinaryField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="background_worker_file", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "BackgroundWorkerFile"
        constraints = [
            models.UniqueConstraint(fields=["project", "content_hash"], name="BackgroundWorkerFile_projectId_contentHash_key"),
        ]


class Prompt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True)
    slug = models.TextField()
    description = models.TextField(null=True)
    type = models.TextField(default="text")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="prompts", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="prompts", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="prompts", db_column="runtimeEnvironmentId", db_index=False)
    file_path = models.TextField(null=True, db_column="filePath")
    export_name = models.TextField(null=True, db_column="exportName")
    variable_schema = models.JSONField(null=True, db_column="variableSchema")
    default_model = models.TextField(null=True, db_column="defaultModel")
    default_config = models.JSONField(null=True, db_column="defaultConfig")
    tags = ArrayField(models.TextField(), default=list)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "prompts"
        constraints = [
            models.UniqueConstraint(fields=["project", "runtime_environment", "slug"], name="prompts_projectId_runtimeEnvironmentId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["runtime_environment"]),
        ]


class PromptVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.IntegerField()
    prompt = models.ForeignKey("Prompt", on_delete=models.CASCADE, related_name="versions", db_column="promptId", db_index=False)
    text_content = models.TextField(null=True, db_column="textContent")
    chat_content = models.JSONField(null=True, db_column="chatContent")
    model = models.TextField(null=True)
    config = models.JSONField(null=True)
    source = models.TextField()
    commit_message = models.TextField(null=True, db_column="commitMessage")
    content_hash = models.TextField(db_column="contentHash")
    labels = ArrayField(models.TextField(), default=list)
    worker = models.ForeignKey("BackgroundWorker", on_delete=models.SET_NULL, related_name="prompt_versions", db_column="workerId", null=True, db_index=False)
    created_by = models.TextField(null=True, db_column="createdBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "prompt_versions"
        constraints = [
            models.UniqueConstraint(fields=["prompt", "version"], name="prompt_versions_promptId_version_key"),
        ]
        indexes = [
            models.Index(fields=["prompt"]),
            models.Index(fields=["content_hash"]),
        ]


class BackgroundWorkerTask(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField()
    description = models.TextField(null=True)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    file_path = models.TextField(db_column="filePath")
    export_name = models.TextField(null=True, db_column="exportName")
    worker = models.ForeignKey("BackgroundWorker", on_delete=models.CASCADE, related_name="tasks", db_column="workerId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="background_worker_tasks", db_column="projectId", db_index=False)
    file = models.ForeignKey("BackgroundWorkerFile", on_delete=models.CASCADE, related_name="tasks", db_column="fileId", null=True, db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="background_worker_tasks", db_column="runtimeEnvironmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    queue_config = models.JSONField(null=True, db_column="queueConfig")
    retry_config = models.JSONField(null=True, db_column="retryConfig")
    machine_config = models.JSONField(null=True, db_column="machineConfig")
    gates = models.JSONField(null=True)
    queue = models.ForeignKey("TaskQueue", on_delete=models.SET_NULL, related_name="tasks", db_column="queueId", null=True, db_index=False)
    max_duration_in_seconds = models.IntegerField(null=True, db_column="maxDurationInSeconds")
    ttl = models.TextField(null=True)
    trigger_source = models.TextField(choices=TaskTriggerSource.choices, db_column="triggerSource", default=TaskTriggerSource.STANDARD)
    config = models.JSONField(null=True)
    payload_schema = models.JSONField(null=True, db_column="payloadSchema")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BackgroundWorkerTask"
        constraints = [
            models.UniqueConstraint(fields=["worker", "slug"], name="BackgroundWorkerTask_workerId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project", "slug"]),
            models.Index(fields=["project", "slug", "created_at"]),
            models.Index(fields=["runtime_environment", "project"]),
            models.Index(fields=["runtime_environment", "slug", "trigger_source"]),
        ]


class WebhookEndpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    opaque_id = models.TextField(unique=True, db_column="opaqueId")
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    runtime_environment_id = models.TextField(db_column="runtimeEnvironmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    endpoint_tenant_id = models.TextField(db_column="endpointTenantId", default="")
    endpoint_external_ref = models.TextField(db_column="endpointExternalRef", default="")
    source = models.TextField()
    declared_id = models.TextField(db_column="declaredId")
    routing_targets = models.JSONField(db_column="routingTargets", default=list)
    verifier_artifact = models.JSONField(db_column="verifierArtifact")
    metadata = models.JSONField(default=dict)
    secret_provisioning = models.TextField(db_column="secretProvisioning", default="either")
    setup_prompt = models.TextField(null=True, db_column="setupPrompt")
    signing_secret_key = models.TextField(null=True, db_column="signingSecretKey")
    status = models.TextField(choices=WebhookEndpointStatus.choices, default=WebhookEndpointStatus.ACTIVE)
    manually_deactivated_at = UtcDateTimeField(null=True, db_column="manuallyDeactivatedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WebhookEndpoint"
        constraints = [
            models.UniqueConstraint(fields=["runtime_environment_id", "declared_id", "endpoint_tenant_id", "endpoint_external_ref"], name="WebhookEndpoint_runtimeEnvironmentId_declaredId_endpointTenantId_endpointExternalRef_key"),
        ]
        indexes = [
            models.Index(fields=["runtime_environment_id", "source"]),
            models.Index(fields=["project_id", "-created_at"]),
        ]


class WebhookDelivery(models.Model):
    pk = models.CompositePrimaryKey("id", "created_at", "retention_days")
    id = models.TextField()
    friendly_id = models.TextField(db_column="friendlyId")
    webhook_endpoint_id = models.TextField(db_column="webhookEndpointId")
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    runtime_environment_id = models.TextField(db_column="runtimeEnvironmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    external_delivery_id = models.TextField(db_column="externalDeliveryId")
    idempotency_key = models.TextField(db_column="idempotencyKey")
    run_id = models.TextField(null=True, db_column="runId")
    status = models.TextField(choices=WebhookDeliveryStatus.choices, default=WebhookDeliveryStatus.PENDING)
    is_test = models.BooleanField(db_column="isTest", default=False)
    parsed_event = models.JSONField(null=True, db_column="parsedEvent")
    headers = models.JSONField(null=True)
    raw_body_hash = models.TextField(null=True, db_column="rawBodyHash")
    error_message = models.TextField(null=True, db_column="errorMessage")
    filter_reason = models.TextField(null=True, db_column="filterReason")
    targets = models.JSONField(default=list)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    processed_at = UtcDateTimeField(null=True, db_column="processedAt")
    retention_days = models.IntegerField(db_column="retentionDays", default=30)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WebhookDelivery"
        indexes = [
            models.Index(fields=["webhook_endpoint_id", "-created_at"]),
        ]


class Alert(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    environment_id = models.TextField(db_column="environmentId")
    capability_key = models.TextField(db_column="capabilityKey")
    params = models.JSONField()
    condition = models.JSONField()
    query = models.TextField(null=True)
    expires_at = models.DateTimeField(null=True, db_column="expiresAt")
    lifecycle = models.TextField(default="active")
    attention = models.TextField(default="clear")
    observed_condition = models.TextField(db_column="observedCondition", default="unknown")
    observed_condition_changed_at = models.DateTimeField(null=True, db_column="observedConditionChangedAt")
    pending_met_since = models.DateTimeField(null=True, db_column="pendingMetSince")
    evaluator_state = models.JSONField(null=True, db_column="evaluatorState")
    evaluation_lease_id = models.TextField(null=True, db_column="evaluationLeaseId")
    evaluation_lease_until = models.DateTimeField(null=True, db_column="evaluationLeaseUntil")
    last_evaluated_at = models.DateTimeField(null=True, db_column="lastEvaluatedAt")
    next_evaluation_at = models.DateTimeField(null=True, db_column="nextEvaluationAt")
    notifiers = models.JSONField(default=list)
    name = models.TextField(null=True)
    created_by = models.TextField(db_column="createdBy")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", default=timezone.now)
    resolved_at = models.DateTimeField(null=True, db_column="resolvedAt")
    resolved_by = models.TextField(null=True, db_column="resolvedBy")
    stopped_at = models.DateTimeField(null=True, db_column="stoppedAt")
    stopped_by = models.TextField(null=True, db_column="stoppedBy")
    last_occurrence_at = models.DateTimeField(null=True, db_column="lastOccurrenceAt")
    occurrences_since_raise = models.IntegerField(db_column="occurrencesSinceRaise", default=0)
    occurrences_total = models.BigIntegerField(db_column="occurrencesTotal", default=0)

    class Meta:
        db_table = "Alert"
        indexes = [
            models.Index(fields=["environment_id", "lifecycle"]),
            models.Index(fields=["lifecycle", "evaluation_lease_until"]),
            models.Index(fields=["project_id"]),
            models.Index(fields=["organization_id"]),
        ]


class AlertEvent(models.Model):
    pk = models.CompositePrimaryKey("id", "at")
    id = models.TextField()
    alert_id = models.TextField(db_column="alertId")
    type = models.TextField()
    at = models.DateTimeField(default=timezone.now)
    facts = models.JSONField(default=dict)
    actor_id = models.TextField(null=True, db_column="actorId")
    source_event_id = models.TextField(null=True, db_column="sourceEventId")

    class Meta:
        db_table = "AlertEvent"
        indexes = [
            models.Index(fields=["alert_id", "-at"]),
        ]


class AlertOccurrenceSeen(models.Model):
    pk = models.CompositePrimaryKey("alert_id", "source_event_id")
    alert_id = models.TextField(db_column="alertId")
    source_event_id = models.TextField(db_column="sourceEventId")
    row_time = models.DateTimeField(db_column="rowTime")
    seen_at = models.DateTimeField(db_column="seenAt", default=timezone.now)
    expires_at = models.DateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "AlertOccurrenceSeen"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class AlertDelivery(models.Model):
    pk = models.CompositePrimaryKey("id", "event_at")
    id = models.TextField()
    alert_event_id = models.TextField(db_column="alertEventId")
    event_at = models.DateTimeField(db_column="eventAt")
    notifier_id = models.TextField(db_column="notifierId")
    notifier = models.JSONField()
    status = models.TextField(default="pending")
    claim_id = models.TextField(null=True, db_column="claimId")
    claimed_at = models.DateTimeField(null=True, db_column="claimedAt")
    delivered_at = models.DateTimeField(null=True, db_column="deliveredAt")
    failure_reason = models.TextField(null=True, db_column="failureReason")
    provider_metadata = models.JSONField(null=True, db_column="providerMetadata")
    attempts = models.IntegerField(default=0)
    first_attempt_at = models.DateTimeField(null=True, db_column="firstAttemptAt")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AlertDelivery"
        constraints = [
            models.UniqueConstraint(fields=["alert_event_id", "notifier_id", "event_at"], name="AlertDelivery_alertEventId_notifierId_eventAt_key"),
        ]
        indexes = [
            models.Index(fields=["status", "created_at"]),
            models.Index(fields=["status", "claimed_at"]),
        ]


class PlaygroundConversation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    chat_id = models.TextField(db_column="chatId")
    title = models.TextField(default="New conversation")
    agent_slug = models.TextField(db_column="agentSlug")
    run = models.ForeignKey("TaskRun", on_delete=models.SET_NULL, related_name="playground_conversations", db_column="runId", null=True, db_index=False)
    client_data = models.JSONField(null=True, db_column="clientData")
    messages = models.JSONField(null=True)
    last_event_id = models.TextField(null=True, db_column="lastEventId")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="playground_conversations", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="playground_conversations", db_column="runtimeEnvironmentId", db_index=False)
    user_id = models.TextField(db_column="userId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PlaygroundConversation"
        constraints = [
            models.UniqueConstraint(fields=["chat_id", "runtime_environment"], name="PlaygroundConversation_chatId_runtimeEnvironmentId_key"),
        ]
        indexes = [
            models.Index(fields=["runtime_environment", "agent_slug", "-updated_at"]),
            models.Index(fields=["user_id", "runtime_environment"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    external_id = models.TextField(null=True, db_column="externalId")
    type = models.TextField()
    project_id = models.TextField(db_column="projectId")
    runtime_environment_id = models.TextField(db_column="runtimeEnvironmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    organization_id = models.TextField(db_column="organizationId")
    task_identifier = models.TextField(db_column="taskIdentifier")
    trigger_config = models.JSONField(db_column="triggerConfig")
    is_test = models.BooleanField(db_column="isTest", default=False)
    tags = ArrayField(models.TextField(), default=list)
    metadata = models.JSONField(null=True)
    current_run_id = models.TextField(null=True, db_column="currentRunId")
    current_run_version = models.IntegerField(db_column="currentRunVersion", default=0)
    closed_at = UtcDateTimeField(null=True, db_column="closedAt")
    closed_reason = models.TextField(null=True, db_column="closedReason")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    stream_basin_name = models.TextField(null=True, db_column="streamBasinName")
    chat_snapshot_storage_path = models.TextField(null=True, db_column="chatSnapshotStoragePath")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Session"
        constraints = [
            models.UniqueConstraint(fields=["runtime_environment_id", "external_id"], name="Session_runtimeEnvironmentId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["expires_at"]),
            models.Index(fields=["current_run_id"]),
        ]


class SessionRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session = models.ForeignKey("Session", on_delete=models.CASCADE, related_name="runs", db_column="sessionId", db_index=False)
    run_id = models.TextField(unique=True, db_column="runId")
    reason = models.TextField()
    triggered_at = UtcDateTimeField(db_column="triggeredAt", default=timezone.now)

    class Meta:
        db_table = "SessionRun"
        indexes = [
            models.Index(fields=["session"]),
            models.Index(fields=["session", "triggered_at", "id"]),
        ]


class TaskRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    number = models.IntegerField(default=0)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    engine = models.TextField(choices=RunEngineVersion.choices, default=RunEngineVersion.V1)
    status = models.TextField(choices=TaskRunStatus.choices, default=TaskRunStatus.PENDING)
    status_reason = models.TextField(null=True, db_column="statusReason")
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    idempotency_key_expires_at = UtcDateTimeField(null=True, db_column="idempotencyKeyExpiresAt")
    idempotency_key_options = models.JSONField(null=True, db_column="idempotencyKeyOptions")
    debounce = models.JSONField(null=True)
    task_identifier = models.TextField(db_column="taskIdentifier")
    is_test = models.BooleanField(db_column="isTest", default=False)
    payload = models.TextField()
    payload_type = models.TextField(db_column="payloadType", default="application/json")
    context = models.JSONField(null=True)
    trace_context = models.JSONField(null=True, db_column="traceContext")
    trace_id = models.TextField(db_column="traceId")
    span_id = models.TextField(db_column="spanId")
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_runs", db_column="runtimeEnvironmentId", db_index=False)
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, null=True, db_column="environmentType")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_runs", db_column="projectId", db_index=False)
    organization_id = models.TextField(null=True, db_column="organizationId")
    queue = models.TextField()
    locked_queue_id = models.TextField(null=True, db_column="lockedQueueId")
    worker_queue = models.TextField(db_column="masterQueue", default="main")
    region = models.TextField(null=True)
    secondary_master_queue = models.TextField(null=True, db_column="secondaryMasterQueue")
    attempt_number = models.IntegerField(null=True, db_column="attemptNumber")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    run_tags = ArrayField(models.TextField(), db_column="runTags")
    task_version = models.TextField(null=True, db_column="taskVersion")
    sdk_version = models.TextField(null=True, db_column="sdkVersion")
    cli_version = models.TextField(null=True, db_column="cliVersion")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    executed_at = UtcDateTimeField(null=True, db_column="executedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    machine_preset = models.TextField(null=True, db_column="machinePreset")
    usage_duration_ms = models.IntegerField(db_column="usageDurationMs", default=0)
    cost_in_cents = models.FloatField(db_column="costInCents", default=0)
    base_cost_in_cents = models.FloatField(db_column="baseCostInCents", default=0)
    locked_at = UtcDateTimeField(null=True, db_column="lockedAt")
    locked_by = models.ForeignKey("BackgroundWorkerTask", on_delete=models.SET_NULL, related_name="runs", db_column="lockedById", null=True, db_index=False)
    locked_to_version = models.ForeignKey("BackgroundWorker", on_delete=models.SET_NULL, related_name="locked_runs", db_column="lockedToVersionId", null=True, db_index=False)
    priority_ms = models.IntegerField(db_column="priorityMs", default=0)
    concurrency_key = models.TextField(null=True, db_column="concurrencyKey")
    gates = models.JSONField(null=True)
    delay_until = UtcDateTimeField(null=True, db_column="delayUntil")
    queued_at = UtcDateTimeField(null=True, db_column="queuedAt")
    ttl = models.TextField(null=True)
    expired_at = UtcDateTimeField(null=True, db_column="expiredAt")
    max_attempts = models.IntegerField(null=True, db_column="maxAttempts")
    locked_retry_config = models.JSONField(null=True, db_column="lockedRetryConfig")
    one_time_use_token = models.TextField(null=True, unique=True, db_column="oneTimeUseToken")
    connected_waitpoints: "models.ManyToManyField[Waitpoint, WaitpointRunConnections]" = models.ManyToManyField("Waitpoint", through="WaitpointRunConnections", related_name="connected_runs")
    task_event_store = models.TextField(db_column="taskEventStore", default="taskEvent")
    queue_timestamp = UtcDateTimeField(null=True, db_column="queueTimestamp")
    schedule_instance_id = models.TextField(null=True, db_column="scheduleInstanceId")
    schedule_id = models.TextField(null=True, db_column="scheduleId")
    bulk_action_group_ids = ArrayField(models.TextField(), db_column="bulkActionGroupIds", default=list)
    logs_deleted_at = UtcDateTimeField(null=True, db_column="logsDeletedAt")
    replayed_from_task_run_friendly_id = models.TextField(null=True, db_column="replayedFromTaskRunFriendlyId")
    root_task_run = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="descendant_runs", db_column="rootTaskRunId", null=True, db_index=False)
    parent_task_run = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="child_runs", db_column="parentTaskRunId", null=True, db_index=False)
    parent_task_run_attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.SET_NULL, related_name="child_runs", db_column="parentTaskRunAttemptId", null=True, db_index=False)
    batch = models.ForeignKey("BatchTaskRun", on_delete=models.SET_NULL, related_name="runs", db_column="batchId", null=True, db_index=False)
    resume_parent_on_completion = models.BooleanField(db_column="resumeParentOnCompletion", default=False)
    depth = models.IntegerField(default=0)
    parent_span_id = models.TextField(null=True, db_column="parentSpanId")
    run_chain_state = models.JSONField(null=True, db_column="runChainState")
    seed_metadata = models.TextField(null=True, db_column="seedMetadata")
    seed_metadata_type = models.TextField(db_column="seedMetadataType", default="application/json")
    metadata = models.TextField(null=True)
    metadata_type = models.TextField(db_column="metadataType", default="application/json")
    metadata_version = models.IntegerField(db_column="metadataVersion", default=1)
    annotations = models.JSONField(null=True)
    is_warm_start = models.BooleanField(null=True, db_column="isWarmStart")
    output = models.TextField(null=True)
    output_type = models.TextField(db_column="outputType", default="application/json")
    error = models.JSONField(null=True)
    plan_type = models.TextField(null=True, db_column="planType")
    max_duration_in_seconds = models.IntegerField(null=True, db_column="maxDurationInSeconds")
    realtime_streams_version = models.TextField(db_column="realtimeStreamsVersion", default="v1")
    realtime_streams = ArrayField(models.TextField(), db_column="realtimeStreams", default=list)
    stream_basin_name = models.TextField(null=True, db_column="streamBasinName")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRun"
        constraints = [
            models.UniqueConstraint(fields=["one_time_use_token"], name="TaskRun_oneTimeUseToken_key"),
            models.UniqueConstraint(fields=["runtime_environment", "task_identifier", "idempotency_key"], name="TaskRun_runtimeEnvironmentId_taskIdentifier_idempotencyKey_key"),
        ]
        indexes = [
            models.Index(fields=["parent_task_run"]),
            models.Index(fields=["span_id"]),
            models.Index(fields=["parent_span_id"]),
            GinIndex(fields=["run_tags"]),
            models.Index(fields=["runtime_environment", "batch"]),
            models.Index(fields=["runtime_environment", "-created_at"]),
            BrinIndex(fields=["created_at"]),
        ]


class TaskRunTemplate(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task_slug = models.TextField(db_column="taskSlug")
    trigger_source = models.TextField(choices=TaskTriggerSource.choices, db_column="triggerSource")
    label = models.TextField()
    payload = models.TextField(null=True)
    payload_type = models.TextField(db_column="payloadType", default="application/json")
    metadata = models.TextField(null=True)
    metadata_type = models.TextField(db_column="metadataType", default="application/json")
    queue = models.TextField(null=True)
    concurrency_key = models.TextField(null=True, db_column="concurrencyKey")
    ttl_seconds = models.IntegerField(null=True, db_column="ttlSeconds")
    max_attempts = models.IntegerField(null=True, db_column="maxAttempts")
    max_duration_seconds = models.IntegerField(null=True, db_column="maxDurationSeconds")
    tags = ArrayField(models.TextField())
    machine_preset = models.TextField(null=True, db_column="machinePreset")
    project_id = models.TextField(db_column="projectId")
    organization_id = models.TextField(db_column="organizationId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunTemplate"
        indexes = [
            models.Index(fields=["project_id", "task_slug", "trigger_source", "-created_at"]),
        ]


class TaskRunExecutionSnapshot(models.Model):
    id = models.TextField(primary_key=True, default=None)
    engine = models.TextField(choices=RunEngineVersion.choices, default=RunEngineVersion.V2)
    execution_status = models.TextField(choices=TaskRunExecutionStatus.choices, db_column="executionStatus")
    description = models.TextField()
    is_valid = models.BooleanField(db_column="isValid", default=True)
    error = models.TextField(null=True)
    previous_snapshot_id = models.TextField(null=True, db_column="previousSnapshotId")
    run = models.ForeignKey("TaskRun", on_delete=models.RESTRICT, related_name="execution_snapshots", db_column="runId", db_index=False)
    run_status = models.TextField(choices=TaskRunStatus.choices, db_column="runStatus")
    batch_id = models.TextField(null=True, db_column="batchId")
    attempt_number = models.IntegerField(null=True, db_column="attemptNumber")
    environment_id = models.TextField(db_column="environmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    project_id = models.TextField(db_column="projectId")
    organization_id = models.TextField(db_column="organizationId")
    completed_waitpoints: "models.ManyToManyField[Waitpoint, CompletedWaitpoints]" = models.ManyToManyField("Waitpoint", through="CompletedWaitpoints", related_name="completed_execution_snapshots")
    completed_waitpoint_order = ArrayField(models.TextField(), db_column="completedWaitpointOrder")
    checkpoint = models.ForeignKey("TaskRunCheckpoint", on_delete=models.SET_NULL, related_name="execution_snapshot", db_column="checkpointId", null=True, db_index=False)
    worker_id = models.TextField(null=True, db_column="workerId")
    runner_id = models.TextField(null=True, db_column="runnerId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_heartbeat_at = UtcDateTimeField(null=True, db_column="lastHeartbeatAt")
    metadata = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunExecutionSnapshot"
        indexes = [
            models.Index(fields=["run", "is_valid", "-created_at"]),
        ]


class TaskRunCheckpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    type = models.TextField(choices=TaskRunCheckpointType.choices)
    location = models.TextField()
    image_ref = models.TextField(null=True, db_column="imageRef")
    reason = models.TextField(null=True)
    metadata = models.TextField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_run_checkpoints", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_run_checkpoints", db_column="runtimeEnvironmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunCheckpoint"


class Waitpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    type = models.TextField(choices=WaitpointType.choices)
    status = models.TextField(choices=WaitpointStatus.choices, default=WaitpointStatus.PENDING)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    idempotency_key = models.TextField(db_column="idempotencyKey")
    user_provided_idempotency_key = models.BooleanField(db_column="userProvidedIdempotencyKey")
    idempotency_key_expires_at = UtcDateTimeField(null=True, db_column="idempotencyKeyExpiresAt")
    inactive_idempotency_key = models.TextField(null=True, db_column="inactiveIdempotencyKey")
    completed_by_task_run = models.OneToOneField("TaskRun", on_delete=models.SET_NULL, related_name="associated_waitpoint", db_column="completedByTaskRunId", null=True)
    completed_after = UtcDateTimeField(null=True, db_column="completedAfter")
    completed_by_batch = models.ForeignKey("BatchTaskRun", on_delete=models.SET_NULL, related_name="waitpoints", db_column="completedByBatchId", null=True, db_index=False)
    output = models.TextField(null=True)
    output_type = models.TextField(db_column="outputType", default="application/json")
    output_is_error = models.BooleanField(db_column="outputIsError", default=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="waitpoints", db_column="projectId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="waitpoints", db_column="environmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    tags = ArrayField(models.TextField())
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Waitpoint"
        constraints = [
            models.UniqueConstraint(fields=["environment", "idempotency_key"], name="Waitpoint_environmentId_idempotencyKey_key"),
        ]
        indexes = [
            models.Index(fields=["completed_by_batch"]),
            models.Index(fields=["environment", "type", "-created_at"]),
            models.Index(fields=["environment", "type", "status"]),
            models.Index(fields=["environment", "type", "-id"]),
        ]


class TaskRunWaitpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task_run = models.ForeignKey("TaskRun", on_delete=models.RESTRICT, related_name="blocked_by_waitpoints", db_column="taskRunId", db_index=False)
    waitpoint = models.ForeignKey("Waitpoint", on_delete=models.RESTRICT, related_name="blocking_task_runs", db_column="waitpointId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_run_waitpoints", db_column="projectId", db_index=False)
    span_id_to_complete = models.TextField(null=True, db_column="spanIdToComplete")
    batch = models.ForeignKey("BatchTaskRun", on_delete=models.SET_NULL, related_name="runs_blocked", db_column="batchId", null=True, db_index=False)
    batch_index = models.IntegerField(null=True, db_column="batchIndex")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunWaitpoint"
        constraints = [
            models.UniqueConstraint(fields=["task_run", "waitpoint", "batch_index"], name="TaskRunWaitpoint_taskRunId_waitpointId_batchIndex_key"),
        ]
        indexes = [
            models.Index(fields=["task_run"]),
            models.Index(fields=["waitpoint"]),
        ]


class WaitpointTag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="waitpoint_tags", db_column="environmentId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="waitpoint_tags", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "WaitpointTag"
        constraints = [
            models.UniqueConstraint(fields=["environment", "name"], name="WaitpointTag_environmentId_name_key"),
        ]


class FeatureFlag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    key = models.TextField(unique=True)
    value = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeatureFlag"


class WorkerInstance(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    resource_identifier = models.TextField(db_column="resourceIdentifier")
    metadata = models.JSONField(null=True)
    worker_group = models.ForeignKey("WorkerInstanceGroup", on_delete=models.RESTRICT, related_name="workers", db_column="workerGroupId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="worker_instances", db_column="organizationId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="workers", db_column="projectId", null=True, db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="worker_instances", db_column="environmentId", null=True, db_index=False)
    deployment = models.ForeignKey("WorkerDeployment", on_delete=models.SET_NULL, related_name="worker_instance", db_column="deploymentId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_dequeue_at = UtcDateTimeField(null=True, db_column="lastDequeueAt")
    last_heartbeat_at = UtcDateTimeField(null=True, db_column="lastHeartbeatAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkerInstance"
        constraints = [
            models.UniqueConstraint(fields=["worker_group", "resource_identifier"], name="WorkerInstance_workerGroupId_resourceIdentifier_key"),
        ]


class WorkerInstanceGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=WorkerInstanceGroupType.choices)
    name = models.TextField()
    master_queue = models.TextField(unique=True, db_column="masterQueue")
    description = models.TextField(null=True)
    hidden = models.BooleanField(default=False)
    token = models.OneToOneField("WorkerGroupToken", on_delete=models.CASCADE, related_name="worker_group", db_column="tokenId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="worker_groups", db_column="organizationId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="worker_groups", db_column="projectId", null=True, db_index=False)
    cloud_provider = models.TextField(null=True, db_column="cloudProvider")
    location = models.TextField(null=True)
    static_ips = models.TextField(null=True, db_column="staticIPs")
    workload_type = models.TextField(choices=WorkloadType.choices, db_column="workloadType", default=WorkloadType.CONTAINER)
    region = models.TextField(null=True)
    enable_fast_path = models.BooleanField(db_column="enableFastPath", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkerInstanceGroup"


class WorkerGroupToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token_hash = models.TextField(unique=True, db_column="tokenHash")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkerGroupToken"


class TaskRunTag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="run_tags", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "TaskRunTag"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="TaskRunTag_projectId_name_key"),
        ]
        indexes = [
            models.Index(fields=["name", "id"]),
        ]


class TaskRunDependency(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task_run = models.OneToOneField("TaskRun", on_delete=models.CASCADE, related_name="dependency", db_column="taskRunId")
    checkpoint_event = models.OneToOneField("CheckpointRestoreEvent", on_delete=models.CASCADE, related_name="task_run_dependency", db_column="checkpointEventId", null=True)
    dependent_attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.SET_NULL, related_name="dependencies", db_column="dependentAttemptId", null=True, db_index=False)
    dependent_batch_run = models.ForeignKey("BatchTaskRun", on_delete=models.SET_NULL, related_name="run_dependencies", db_column="dependentBatchRunId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    resumed_at = UtcDateTimeField(null=True, db_column="resumedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunDependency"
        indexes = [
            models.Index(fields=["dependent_attempt"]),
            models.Index(fields=["dependent_batch_run"]),
        ]


class TaskRunCounter(models.Model):
    task_identifier = models.TextField(primary_key=True, db_column="taskIdentifier", default=None)
    last_number = models.IntegerField(db_column="lastNumber", default=0)

    class Meta:
        db_table = "TaskRunCounter"


class TaskRunNumberCounter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task_identifier = models.TextField(db_column="taskIdentifier")
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_run_number_counter", db_column="environmentId", db_index=False)
    last_number = models.IntegerField(db_column="lastNumber", default=0)

    class Meta:
        db_table = "TaskRunNumberCounter"
        constraints = [
            models.UniqueConstraint(fields=["task_identifier", "environment"], name="TaskRunNumberCounter_taskIdentifier_environmentId_key"),
        ]


class TaskRunAttempt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    number = models.IntegerField(default=0)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    task_run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="attempts", db_column="taskRunId", db_index=False)
    background_worker = models.ForeignKey("BackgroundWorker", on_delete=models.CASCADE, related_name="attempts", db_column="backgroundWorkerId", db_index=False)
    background_worker_task = models.ForeignKey("BackgroundWorkerTask", on_delete=models.CASCADE, related_name="attempts", db_column="backgroundWorkerTaskId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_run_attempts", db_column="runtimeEnvironmentId", db_index=False)
    queue = models.ForeignKey("TaskQueue", on_delete=models.CASCADE, related_name="attempts", db_column="queueId", db_index=False)
    status = models.TextField(choices=TaskRunAttemptStatus.choices, default=TaskRunAttemptStatus.PENDING)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    usage_duration_ms = models.IntegerField(db_column="usageDurationMs", default=0)
    error = models.JSONField(null=True)
    output = models.TextField(null=True)
    output_type = models.TextField(db_column="outputType", default="application/json")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskRunAttempt"
        constraints = [
            models.UniqueConstraint(fields=["task_run", "number"], name="TaskRunAttempt_taskRunId_number_key"),
        ]
        indexes = [
            models.Index(fields=["task_run"]),
        ]


class TaskEvent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    message = models.TextField()
    trace_id = models.TextField(db_column="traceId")
    span_id = models.TextField(db_column="spanId")
    parent_id = models.TextField(null=True, db_column="parentId")
    tracestate = models.TextField(null=True)
    is_error = models.BooleanField(db_column="isError", default=False)
    is_partial = models.BooleanField(db_column="isPartial", default=False)
    is_cancelled = models.BooleanField(db_column="isCancelled", default=False)
    is_debug = models.BooleanField(db_column="isDebug", default=False)
    service_name = models.TextField(db_column="serviceName")
    service_namespace = models.TextField(db_column="serviceNamespace")
    level = models.TextField(choices=TaskEventLevel.choices, default=TaskEventLevel.TRACE)
    kind = models.TextField(choices=TaskEventKind.choices, default=TaskEventKind.INTERNAL)
    status = models.TextField(choices=TaskEventStatus.choices, default=TaskEventStatus.UNSET)
    links = models.JSONField(null=True)
    events = models.JSONField(null=True)
    start_time = models.BigIntegerField(db_column="startTime")
    duration = models.BigIntegerField(default=0)
    attempt_id = models.TextField(null=True, db_column="attemptId")
    attempt_number = models.IntegerField(null=True, db_column="attemptNumber")
    environment_id = models.TextField(db_column="environmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    project_ref = models.TextField(db_column="projectRef")
    run_id = models.TextField(db_column="runId")
    run_is_test = models.BooleanField(db_column="runIsTest", default=False)
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    task_slug = models.TextField(db_column="taskSlug")
    task_path = models.TextField(null=True, db_column="taskPath")
    task_export_name = models.TextField(null=True, db_column="taskExportName")
    worker_id = models.TextField(null=True, db_column="workerId")
    worker_version = models.TextField(null=True, db_column="workerVersion")
    queue_id = models.TextField(null=True, db_column="queueId")
    queue_name = models.TextField(null=True, db_column="queueName")
    batch_id = models.TextField(null=True, db_column="batchId")
    properties = models.JSONField()
    metadata = models.JSONField(null=True)
    style = models.JSONField(null=True)
    output = models.JSONField(null=True)
    output_type = models.TextField(null=True, db_column="outputType")
    payload = models.JSONField(null=True)
    payload_type = models.TextField(null=True, db_column="payloadType")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    usage_duration_ms = models.IntegerField(db_column="usageDurationMs", default=0)
    usage_cost_in_cents = models.FloatField(db_column="usageCostInCents", default=0)
    machine_preset = models.TextField(null=True, db_column="machinePreset")
    machine_preset_cpu = models.FloatField(null=True, db_column="machinePresetCpu")
    machine_preset_memory = models.FloatField(null=True, db_column="machinePresetMemory")
    machine_preset_cents_per_ms = models.FloatField(null=True, db_column="machinePresetCentsPerMs")

    class Meta:
        db_table = "TaskEvent"
        indexes = [
            models.Index(fields=["trace_id"]),
            models.Index(fields=["span_id"]),
            models.Index(fields=["run_id"]),
        ]


class TaskQueue(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    name = models.TextField()
    type = models.TextField(choices=TaskQueueType.choices, default=TaskQueueType.VIRTUAL)
    version = models.TextField(choices=TaskQueueVersion.choices, default=TaskQueueVersion.V1)
    orderable_name = models.TextField(null=True, db_column="orderableName")
    concurrency_version = models.TextField(choices=TaskQueueConcurrencyVersion.choices, db_column="concurrencyVersion", default=TaskQueueConcurrencyVersion.V1)
    role = models.TextField(choices=TaskQueueRole.choices, default=TaskQueueRole.QUEUE)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_queues", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_queues", db_column="runtimeEnvironmentId", db_index=False)
    concurrency_limit = models.IntegerField(null=True, db_column="concurrencyLimit")
    concurrency_limit_overridden_at = UtcDateTimeField(null=True, db_column="concurrencyLimitOverriddenAt")
    concurrency_limit_overridden_by = models.TextField(null=True, db_column="concurrencyLimitOverriddenBy")
    concurrency_limit_base = models.IntegerField(null=True, db_column="concurrencyLimitBase")
    concurrency_limit_override_percent = models.DecimalField(max_digits=5, decimal_places=2, null=True, db_column="concurrencyLimitOverridePercent")
    total_concurrency_limit = models.IntegerField(null=True, db_column="totalConcurrencyLimit")
    total_concurrency_limit_overridden_at = UtcDateTimeField(null=True, db_column="totalConcurrencyLimitOverriddenAt")
    total_concurrency_limit_overridden_by = models.TextField(null=True, db_column="totalConcurrencyLimitOverriddenBy")
    total_concurrency_limit_base = models.IntegerField(null=True, db_column="totalConcurrencyLimitBase")
    rate_limit = models.JSONField(null=True, db_column="rateLimit")
    paused = models.BooleanField(default=False)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskQueue"
        constraints = [
            models.UniqueConstraint(fields=["runtime_environment", "name"], name="TaskQueue_runtimeEnvironmentId_name_key"),
        ]


class BatchTaskRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    idempotency_key_expires_at = UtcDateTimeField(null=True, db_column="idempotencyKeyExpiresAt")
    status = models.TextField(choices=BatchTaskRunStatus.choices, default=BatchTaskRunStatus.PENDING)
    runtime_environment_id = models.TextField(db_column="runtimeEnvironmentId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    run_ids = ArrayField(models.TextField(), db_column="runIds", default=list)
    run_count = models.IntegerField(db_column="runCount", default=0)
    payload = models.TextField(null=True)
    payload_type = models.TextField(db_column="payloadType", default="application/json")
    options = models.JSONField(null=True)
    batch_version = models.TextField(db_column="batchVersion", default="v1")
    sealed = models.BooleanField(default=False)
    sealed_at = UtcDateTimeField(null=True, db_column="sealedAt")
    expected_count = models.IntegerField(db_column="expectedCount", default=0)
    completed_count = models.IntegerField(db_column="completedCount", default=0)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    resumed_at = UtcDateTimeField(null=True, db_column="resumedAt")
    processing_jobs_count = models.IntegerField(db_column="processingJobsCount", default=0)
    processing_jobs_expected_count = models.IntegerField(db_column="processingJobsExpectedCount", default=0)
    one_time_use_token = models.TextField(null=True, unique=True, db_column="oneTimeUseToken")
    processing_started_at = UtcDateTimeField(null=True, db_column="processingStartedAt")
    processing_completed_at = UtcDateTimeField(null=True, db_column="processingCompletedAt")
    successful_run_count = models.IntegerField(null=True, db_column="successfulRunCount")
    failed_run_count = models.IntegerField(null=True, db_column="failedRunCount")
    task_identifier = models.TextField(null=True, db_column="taskIdentifier")
    checkpoint_event = models.OneToOneField("CheckpointRestoreEvent", on_delete=models.CASCADE, related_name="batch_task_run_dependency", db_column="checkpointEventId", null=True)
    dependent_task_attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.CASCADE, related_name="batch_dependencies", db_column="dependentTaskAttemptId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BatchTaskRun"
        constraints = [
            models.UniqueConstraint(fields=["one_time_use_token"], name="BatchTaskRun_oneTimeUseToken_key"),
            models.UniqueConstraint(fields=["runtime_environment_id", "idempotency_key"], name="BatchTaskRun_runtimeEnvironmentId_idempotencyKey_key"),
        ]
        indexes = [
            models.Index(fields=["dependent_task_attempt"]),
            models.Index(fields=["runtime_environment_id", "-id"]),
            models.Index(fields=["runtime_environment_id", "-created_at", "-id"]),
        ]


class BatchTaskRunItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField(choices=BatchTaskRunItemStatus.choices, default=BatchTaskRunItemStatus.PENDING)
    batch_task_run = models.ForeignKey("BatchTaskRun", on_delete=models.CASCADE, related_name="items", db_column="batchTaskRunId", db_index=False)
    task_run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="batch_items", db_column="taskRunId", db_index=False)
    task_run_attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.SET_NULL, related_name="batch_task_run_items", db_column="taskRunAttemptId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BatchTaskRunItem"
        constraints = [
            models.UniqueConstraint(fields=["batch_task_run", "task_run"], name="BatchTaskRunItem_batchTaskRunId_taskRunId_key"),
        ]
        indexes = [
            models.Index(fields=["task_run_attempt"]),
            models.Index(fields=["task_run"], name="idx_batchtaskrunitem_taskrun"),
        ]


class BatchTaskRunError(models.Model):
    id = models.TextField(primary_key=True, default=None)
    batch_task_run = models.ForeignKey("BatchTaskRun", on_delete=models.CASCADE, related_name="errors", db_column="batchTaskRunId", db_index=False)
    index = models.IntegerField()
    task_identifier = models.TextField(db_column="taskIdentifier")
    payload = models.TextField(null=True)
    options = models.JSONField(null=True)
    error = models.TextField()
    error_code = models.TextField(null=True, db_column="errorCode")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "BatchTaskRunError"
        constraints = [
            models.UniqueConstraint(fields=["batch_task_run", "index"], name="BatchTaskRunError_batchTaskRunId_index_key"),
        ]
        indexes = [
            models.Index(fields=["batch_task_run"]),
        ]


class EnvironmentVariable(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    key = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="environment_variables", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EnvironmentVariable"
        constraints = [
            models.UniqueConstraint(fields=["project", "key"], name="EnvironmentVariable_projectId_key_key"),
        ]


class EnvironmentVariableValue(models.Model):
    id = models.TextField(primary_key=True, default=None)
    value_reference = models.ForeignKey("SecretReference", on_delete=models.SET_NULL, related_name="environment_variable_values", db_column="valueReferenceId", null=True, db_index=False)
    variable = models.ForeignKey("EnvironmentVariable", on_delete=models.CASCADE, related_name="values", db_column="variableId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="environment_variable_values", db_column="environmentId", db_index=False)
    is_secret = models.BooleanField(db_column="isSecret", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    version = models.IntegerField(default=1)
    last_updated_by = models.JSONField(null=True, db_column="lastUpdatedBy")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EnvironmentVariableValue"
        constraints = [
            models.UniqueConstraint(fields=["variable", "environment"], name="EnvironmentVariableValue_variableId_environmentId_key"),
        ]
        indexes = [
            models.Index(fields=["environment"]),
            models.Index(fields=["value_reference"]),
        ]


class Checkpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    type = models.TextField(choices=CheckpointType.choices)
    location = models.TextField()
    image_ref = models.TextField(db_column="imageRef")
    reason = models.TextField(null=True)
    metadata = models.TextField(null=True)
    run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="checkpoints", db_column="runId", db_index=False)
    attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.CASCADE, related_name="checkpoints", db_column="attemptId", db_index=False)
    attempt_number = models.IntegerField(null=True, db_column="attemptNumber")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="checkpoints", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="checkpoints", db_column="runtimeEnvironmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Checkpoint"
        indexes = [
            models.Index(fields=["attempt"]),
            models.Index(fields=["run"]),
        ]


class CheckpointRestoreEvent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=CheckpointRestoreEventType.choices)
    reason = models.TextField(null=True)
    metadata = models.TextField(null=True)
    checkpoint = models.ForeignKey("Checkpoint", on_delete=models.CASCADE, related_name="events", db_column="checkpointId", db_index=False)
    run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="checkpoint_restore_event", db_column="runId", db_index=False)
    attempt = models.ForeignKey("TaskRunAttempt", on_delete=models.CASCADE, related_name="checkpoint_restore_event", db_column="attemptId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="checkpoint_restore_event", db_column="projectId", db_index=False)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="checkpoint_restore_event", db_column="runtimeEnvironmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CheckpointRestoreEvent"
        indexes = [
            models.Index(fields=["checkpoint"]),
            models.Index(fields=["run"]),
        ]


class WorkerDeployment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    content_hash = models.TextField(db_column="contentHash")
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    short_code = models.TextField(db_column="shortCode")
    version = models.TextField()
    runtime = models.TextField(null=True)
    runtime_version = models.TextField(null=True, db_column="runtimeVersion")
    cli_version = models.TextField(null=True, db_column="cliVersion")
    image_reference = models.TextField(null=True, db_column="imageReference")
    image_platform = models.TextField(db_column="imagePlatform", default="linux/amd64")
    template_key = models.TextField(null=True, db_column="templateKey")
    index_object_key = models.TextField(null=True, db_column="indexObjectKey")
    index_sha256 = models.TextField(null=True, db_column="indexSha256")
    index_format_version = models.IntegerField(null=True, db_column="indexFormatVersion")
    external_build_data = models.JSONField(null=True, db_column="externalBuildData")
    build_server_metadata = models.JSONField(null=True, db_column="buildServerMetadata")
    build_env_vars = models.JSONField(null=True, db_column="buildEnvVars")
    status = models.TextField(choices=WorkerDeploymentStatus.choices, default=WorkerDeploymentStatus.PENDING)
    type = models.TextField(choices=WorkerDeploymentType.choices, default=WorkerDeploymentType.V1)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="worker_deployment", db_column="projectId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="worker_deployments", db_column="environmentId", db_index=False)
    worker = models.OneToOneField("BackgroundWorker", on_delete=models.CASCADE, related_name="deployment", db_column="workerId", null=True)
    triggered_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="deployments", db_column="triggeredById", null=True, db_index=False)
    triggered_via = models.TextField(null=True, db_column="triggeredVia")
    commit_sha = models.TextField(null=True, db_column="commitSHA")
    external_id = models.TextField(null=True, db_column="externalId")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    installed_at = UtcDateTimeField(null=True, db_column="installedAt")
    built_at = UtcDateTimeField(null=True, db_column="builtAt")
    deployed_at = UtcDateTimeField(null=True, db_column="deployedAt")
    failed_at = UtcDateTimeField(null=True, db_column="failedAt")
    error_data = models.JSONField(null=True, db_column="errorData")
    canceled_at = UtcDateTimeField(null=True, db_column="canceledAt")
    canceled_reason = models.TextField(null=True, db_column="canceledReason")
    git = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkerDeployment"
        constraints = [
            models.UniqueConstraint(fields=["project", "short_code"], name="WorkerDeployment_projectId_shortCode_key"),
            models.UniqueConstraint(fields=["environment", "version"], name="WorkerDeployment_environmentId_version_key"),
        ]
        indexes = [
            models.Index(fields=["commit_sha"]),
            models.Index(fields=["environment", "created_at"]),
            models.Index(fields=["environment", "status", "id"]),
            models.Index(fields=["environment", "external_id"]),
        ]


class WorkerDeploymentPromotion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    label = models.TextField()
    deployment = models.ForeignKey("WorkerDeployment", on_delete=models.CASCADE, related_name="promotions", db_column="deploymentId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="worker_deployment_promotions", db_column="environmentId", db_index=False)

    class Meta:
        db_table = "WorkerDeploymentPromotion"
        constraints = [
            models.UniqueConstraint(fields=["environment", "label"], name="WorkerDeploymentPromotion_environmentId_label_key"),
        ]


class TaskSchedule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=ScheduleType.choices, default=ScheduleType.IMPERATIVE)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    task_identifier = models.TextField(db_column="taskIdentifier")
    deduplication_key = models.TextField(db_column="deduplicationKey")
    user_provided_deduplication_key = models.BooleanField(db_column="userProvidedDeduplicationKey", default=False)
    generator_expression = models.TextField(db_column="generatorExpression")
    generator_description = models.TextField(db_column="generatorDescription", default="")
    generator_type = models.TextField(choices=ScheduleGeneratorType.choices, db_column="generatorType", default=ScheduleGeneratorType.CRON)
    timezone = models.TextField(default="UTC")
    window_duration_seconds = models.IntegerField(null=True, db_column="windowDurationSeconds")
    window_percentage = models.IntegerField(null=True, db_column="windowPercentage")
    default_window_duration_seconds = models.IntegerField(null=True, db_column="defaultWindowDurationSeconds")
    minimum_window_duration_seconds = models.IntegerField(null=True, db_column="minimumWindowDurationSeconds")
    external_id = models.TextField(null=True, db_column="externalId")
    last_run_triggered_at = UtcDateTimeField(null=True, db_column="lastRunTriggeredAt")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_schedules", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    active = models.BooleanField(default=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskSchedule"
        constraints = [
            models.UniqueConstraint(fields=["project", "deduplication_key"], name="TaskSchedule_projectId_deduplicationKey_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "-created_at"]),
        ]


class TaskScheduleInstance(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task_schedule = models.ForeignKey("TaskSchedule", on_delete=models.CASCADE, related_name="instances", db_column="taskScheduleId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_schedule_instances", db_column="environmentId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_schedule_instances", db_column="projectId", db_index=False)
    schedule_phase = models.IntegerField(null=True, db_column="schedulePhase")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    active = models.BooleanField(default=True)
    last_scheduled_timestamp = UtcDateTimeField(null=True, db_column="lastScheduledTimestamp")
    next_scheduled_timestamp = UtcDateTimeField(null=True, db_column="nextScheduledTimestamp")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskScheduleInstance"
        constraints = [
            models.UniqueConstraint(fields=["task_schedule", "environment"], name="TaskScheduleInstance_taskScheduleId_environmentId_key"),
        ]
        indexes = [
            models.Index(fields=["environment"]),
            models.Index(fields=["project", "active"]),
        ]


class RuntimeEnvironmentSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    ip_address = models.TextField(db_column="ipAddress")
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="sessions", db_column="environmentId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    disconnected_at = UtcDateTimeField(null=True, db_column="disconnectedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RuntimeEnvironmentSession"


class ProjectAlertChannel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    deduplication_key = models.TextField(db_column="deduplicationKey")
    user_provided_deduplication_key = models.BooleanField(db_column="userProvidedDeduplicationKey", default=False)
    integration = models.ForeignKey("OrganizationIntegration", on_delete=models.SET_NULL, related_name="alert_channels", db_column="integrationId", null=True, db_index=False)
    enabled = models.BooleanField(default=True)
    type = models.TextField(choices=ProjectAlertChannelType.choices)
    name = models.TextField()
    properties = models.JSONField()
    alert_types = ArrayField(models.TextField(choices=ProjectAlertType.choices), db_column="alertTypes")
    environment_types = ArrayField(models.TextField(choices=RuntimeEnvironmentType.choices), db_column="environmentTypes", default=project_alert_channel_environment_types_default)
    error_alert_config = models.JSONField(null=True, db_column="errorAlertConfig")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="alert_channels", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProjectAlertChannel"
        constraints = [
            models.UniqueConstraint(fields=["project", "deduplication_key"], name="ProjectAlertChannel_projectId_deduplicationKey_key"),
        ]


class ProjectAlert(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="alerts", db_column="projectId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="alerts", db_column="environmentId", db_index=False)
    channel = models.ForeignKey("ProjectAlertChannel", on_delete=models.CASCADE, related_name="alerts", db_column="channelId", db_index=False)
    status = models.TextField(choices=ProjectAlertStatus.choices, default=ProjectAlertStatus.PENDING)
    type = models.TextField(choices=ProjectAlertType.choices)
    task_run_attempt_id = models.TextField(null=True, db_column="taskRunAttemptId")
    task_run_id = models.TextField(null=True, db_column="taskRunId")
    worker_deployment = models.ForeignKey("WorkerDeployment", on_delete=models.CASCADE, related_name="alerts", db_column="workerDeploymentId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProjectAlert"
        indexes = [
            models.Index(fields=["channel"]),
        ]


class ProjectAlertStorage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="alert_storages", db_column="projectId", db_index=False)
    alert_channel = models.ForeignKey("ProjectAlertChannel", on_delete=models.CASCADE, related_name="alert_storages", db_column="alertChannelId", db_index=False)
    alert_type = models.TextField(choices=ProjectAlertType.choices, db_column="alertType")
    storage_id = models.TextField(db_column="storageId")
    storage_data = models.JSONField(db_column="storageData")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProjectAlertStorage"
        indexes = [
            models.Index(fields=["alert_channel", "alert_type", "storage_id"]),
        ]


class OrganizationIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    service = models.TextField(choices=IntegrationService.choices)
    external_organization_id = models.TextField(null=True, db_column="externalOrganizationId")
    integration_data = models.JSONField(db_column="integrationData")
    token_reference = models.ForeignKey("SecretReference", on_delete=models.CASCADE, related_name="organization_integration", db_column="tokenReferenceId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="organization_integrations", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationIntegration"
        indexes = [
            models.Index(fields=["external_organization_id"]),
        ]


class OrganizationProjectIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_integration = models.ForeignKey("OrganizationIntegration", on_delete=models.CASCADE, related_name="organization_project_integration", db_column="organizationIntegrationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="organization_project_integration", db_column="projectId", db_index=False)
    external_entity_id = models.TextField(db_column="externalEntityId")
    integration_data = models.JSONField(db_column="integrationData")
    installed_by = models.TextField(null=True, db_column="installedBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationProjectIntegration"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "organization_integration"]),
            models.Index(fields=["external_entity_id"]),
        ]


class BulkActionGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="bulk_action_groups", db_column="projectId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="bulk_action_group", db_column="environmentId", null=True, db_index=False)
    type = models.TextField(choices=BulkActionType.choices)
    status = models.TextField(choices=BulkActionStatus.choices, default=BulkActionStatus.PENDING)
    query_name = models.TextField(null=True, db_column="queryName")
    params = models.JSONField(null=True)
    dedupe_key = models.TextField(null=True, db_column="dedupeKey")
    cursor = models.JSONField(null=True)
    success_count = models.IntegerField(db_column="successCount", default=0)
    failure_count = models.IntegerField(db_column="failureCount", default=0)
    total_count = models.IntegerField(db_column="totalCount", default=0)
    completion_notification = models.TextField(choices=BulkActionNotificationType.choices, db_column="completionNotification", default=BulkActionNotificationType.NONE)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="bulk_actions", db_column="userId", null=True, db_index=False)
    name = models.TextField(null=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BulkActionGroup"
        indexes = [
            models.Index(fields=["environment", "-created_at"]),
            models.Index(fields=["environment", "type", "dedupe_key"]),
            models.Index(fields=["environment", "status", "type"]),
        ]


class BulkActionItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(null=True, db_column="friendlyId")
    group_id = models.TextField(db_column="groupId")
    type = models.TextField(choices=BulkActionType.choices)
    status = models.TextField(choices=BulkActionItemStatus.choices, default=BulkActionItemStatus.PENDING)
    source_run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="source_bulk_action_items", db_column="sourceRunId", db_index=False)
    destination_run = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="destination_bulk_action_items", db_column="destinationRunId", null=True, db_index=False)
    error = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BulkActionItem"


class RealtimeStreamChunk(models.Model):
    id = models.TextField(primary_key=True, default=None)
    key = models.TextField()
    value = models.TextField()
    sequence = models.IntegerField()
    run_id = models.TextField(db_column="runId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "RealtimeStreamChunk"
        indexes = [
            models.Index(fields=["run_id"]),
            models.Index(fields=["created_at"]),
        ]


class TaskEventPartitioned(models.Model):
    pk = models.CompositePrimaryKey("id", "created_at")
    id = models.TextField()
    message = models.TextField()
    trace_id = models.TextField(db_column="traceId")
    span_id = models.TextField(db_column="spanId")
    parent_id = models.TextField(null=True, db_column="parentId")
    tracestate = models.TextField(null=True)
    is_error = models.BooleanField(db_column="isError", default=False)
    is_partial = models.BooleanField(db_column="isPartial", default=False)
    is_cancelled = models.BooleanField(db_column="isCancelled", default=False)
    service_name = models.TextField(db_column="serviceName")
    service_namespace = models.TextField(db_column="serviceNamespace")
    level = models.TextField(choices=TaskEventLevel.choices, default=TaskEventLevel.TRACE)
    kind = models.TextField(choices=TaskEventKind.choices, default=TaskEventKind.INTERNAL)
    status = models.TextField(choices=TaskEventStatus.choices, default=TaskEventStatus.UNSET)
    links = models.JSONField(null=True)
    events = models.JSONField(null=True)
    start_time = models.BigIntegerField(db_column="startTime")
    duration = models.BigIntegerField(default=0)
    attempt_id = models.TextField(null=True, db_column="attemptId")
    attempt_number = models.IntegerField(null=True, db_column="attemptNumber")
    environment_id = models.TextField(db_column="environmentId")
    environment_type = models.TextField(choices=RuntimeEnvironmentType.choices, db_column="environmentType")
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    project_ref = models.TextField(db_column="projectRef")
    run_id = models.TextField(db_column="runId")
    run_is_test = models.BooleanField(db_column="runIsTest", default=False)
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    task_slug = models.TextField(db_column="taskSlug")
    task_path = models.TextField(null=True, db_column="taskPath")
    task_export_name = models.TextField(null=True, db_column="taskExportName")
    worker_id = models.TextField(null=True, db_column="workerId")
    worker_version = models.TextField(null=True, db_column="workerVersion")
    queue_id = models.TextField(null=True, db_column="queueId")
    queue_name = models.TextField(null=True, db_column="queueName")
    batch_id = models.TextField(null=True, db_column="batchId")
    properties = models.JSONField()
    metadata = models.JSONField(null=True)
    style = models.JSONField(null=True)
    output = models.JSONField(null=True)
    output_type = models.TextField(null=True, db_column="outputType")
    payload = models.JSONField(null=True)
    payload_type = models.TextField(null=True, db_column="payloadType")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    usage_duration_ms = models.IntegerField(db_column="usageDurationMs", default=0)
    usage_cost_in_cents = models.FloatField(db_column="usageCostInCents", default=0)
    machine_preset = models.TextField(null=True, db_column="machinePreset")
    machine_preset_cpu = models.FloatField(null=True, db_column="machinePresetCpu")
    machine_preset_memory = models.FloatField(null=True, db_column="machinePresetMemory")
    machine_preset_cents_per_ms = models.FloatField(null=True, db_column="machinePresetCentsPerMs")

    class Meta:
        db_table = "TaskEventPartitioned"
        indexes = [
            models.Index(fields=["trace_id"]),
            models.Index(fields=["span_id"]),
            models.Index(fields=["run_id"]),
        ]


class GithubAppInstallation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    app_installation_id = models.BigIntegerField(unique=True, db_column="appInstallationId")
    target_id = models.BigIntegerField(db_column="targetId")
    target_type = models.TextField(db_column="targetType")
    account_handle = models.TextField(db_column="accountHandle")
    permissions = models.JSONField(null=True)
    repository_selection = models.TextField(choices=GithubRepositorySelection.choices, db_column="repositorySelection")
    installed_by = models.TextField(null=True, db_column="installedBy")
    installed_by_user_id = models.TextField(null=True, db_column="installedByUserId")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="github_app_installations", db_column="organizationId", db_index=False)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    suspended_at = UtcDateTimeField(null=True, db_column="suspendedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GithubAppInstallation"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class GithubRepository(models.Model):
    id = models.TextField(primary_key=True, default=None)
    github_id = models.BigIntegerField(db_column="githubId")
    name = models.TextField()
    full_name = models.TextField(db_column="fullName")
    html_url = models.TextField(db_column="htmlUrl")
    private = models.BooleanField()
    default_branch = models.TextField(db_column="defaultBranch")
    installation = models.ForeignKey("GithubAppInstallation", on_delete=models.CASCADE, related_name="repositories", db_column="installationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GithubRepository"
        constraints = [
            models.UniqueConstraint(fields=["installation", "github_id"], name="GithubRepository_installationId_githubId_key"),
        ]
        indexes = [
            models.Index(fields=["installation"]),
        ]


class ConnectedGithubRepository(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="connected_github_repository", db_column="projectId")
    repository = models.ForeignKey("GithubRepository", on_delete=models.CASCADE, related_name="connected_github_repository", db_column="repositoryId", db_index=False)
    branch_tracking = models.JSONField(db_column="branchTracking")
    preview_deployments_enabled = models.BooleanField(db_column="previewDeploymentsEnabled", default=True)
    build_filters = models.JSONField(null=True, db_column="buildFilters")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConnectedGithubRepository"
        constraints = [
            models.UniqueConstraint(fields=["project"], name="ConnectedGithubRepository_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["repository"]),
        ]


class ImpersonationAuditLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    action = models.TextField(choices=ImpersonationAuditLogAction.choices)
    admin = models.ForeignKey("User", on_delete=models.CASCADE, related_name="impersonations_performed", db_column="adminId", db_index=False)
    target = models.ForeignKey("User", on_delete=models.CASCADE, related_name="impersonations_received", db_column="targetId", db_index=False)
    ip_address = models.TextField(null=True, db_column="ipAddress")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ImpersonationAuditLog"
        indexes = [
            models.Index(fields=["admin"]),
            models.Index(fields=["target"]),
            models.Index(fields=["created_at"]),
        ]


class SupportAccessRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="support_access_requests", db_column="organizationId", db_index=False)
    requested_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="support_access_requests_made", db_column="requestedById", null=True, db_index=False)
    reason = models.TextField()
    status = models.TextField(choices=SupportAccessRequestStatus.choices, default=SupportAccessRequestStatus.PENDING)
    approved_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="support_access_requests_approved", db_column="approvedById", null=True, db_index=False)
    approved_at = UtcDateTimeField(null=True, db_column="approvedAt")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    cancelled_by_id = models.TextField(null=True, db_column="cancelledById")
    cancelled_at = UtcDateTimeField(null=True, db_column="cancelledAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SupportAccessRequest"
        indexes = [
            models.Index(fields=["organization", "status", "expires_at"]),
            models.Index(fields=["requested_by"]),
            models.Index(fields=["approved_by"]),
        ]


class CustomerQuery(models.Model):
    id = models.TextField(primary_key=True, default=None)
    query = models.TextField()
    scope = models.TextField(choices=CustomerQueryScope.choices)
    stats = models.JSONField()
    title = models.TextField(null=True)
    source = models.TextField(choices=CustomerQuerySource.choices, default=CustomerQuerySource.DASHBOARD)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="customer_queries", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="customer_queries", db_column="projectId", null=True, db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="customer_queries", db_column="environmentId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="customer_queries", db_column="userId", null=True, db_index=False)
    filter_period = models.TextField(null=True, db_column="filterPeriod")
    filter_from = UtcDateTimeField(null=True, db_column="filterFrom")
    filter_to = UtcDateTimeField(null=True, db_column="filterTo")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "CustomerQuery"
        indexes = [
            models.Index(fields=["organization", "-created_at"]),
            models.Index(fields=["created_at"]),
        ]


class IntegrationDeployment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    integration_name = models.TextField(db_column="integrationName")
    integration_deployment_id = models.TextField(db_column="integrationDeploymentId")
    commit_sha = models.TextField(db_column="commitSHA")
    worker_deployment = models.ForeignKey("WorkerDeployment", on_delete=models.SET_NULL, related_name="integration_deployments", db_column="deploymentId", null=True, db_index=False)
    status = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IntegrationDeployment"
        indexes = [
            models.Index(fields=["commit_sha"]),
            models.Index(fields=["worker_deployment"]),
        ]


class MetricsDashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    title = models.TextField()
    description = models.TextField(default="")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="metrics_dashboards", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="metrics_dashboards", db_column="projectId", null=True, db_index=False)
    owner = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="metrics_dashboards", db_column="ownerId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    layout = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MetricsDashboard"
        indexes = [
            models.Index(fields=["project", "-created_at"]),
        ]


class LlmModel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="llm_models", null=True, db_index=False)
    model_name = models.TextField()
    match_pattern = models.TextField()
    start_date = UtcDateTimeField(null=True)
    source = models.TextField(default="default")
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    provider = models.TextField(null=True)
    description = models.TextField(null=True)
    context_window = models.IntegerField(null=True)
    max_output_tokens = models.IntegerField(null=True)
    capabilities = ArrayField(models.TextField(), default=list)
    is_hidden = models.BooleanField(default=False)
    base_model_name = models.TextField(null=True)
    pricing_unit = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "llm_models"
        constraints = [
            models.UniqueConstraint(fields=["project", "model_name", "start_date"], name="llm_models_project_id_model_name_start_date_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class LlmPricingTier(models.Model):
    id = models.TextField(primary_key=True, default=None)
    model = models.ForeignKey("LlmModel", on_delete=models.CASCADE, related_name="pricing_tiers", db_index=False)
    name = models.TextField()
    is_default = models.BooleanField(default=True)
    priority = models.IntegerField(default=0)
    conditions = models.JSONField(default=list)

    class Meta:
        db_table = "llm_pricing_tiers"
        constraints = [
            models.UniqueConstraint(fields=["model", "priority"], name="llm_pricing_tiers_model_id_priority_key"),
            models.UniqueConstraint(fields=["model", "name"], name="llm_pricing_tiers_model_id_name_key"),
        ]


class LlmPrice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    model = models.ForeignKey("LlmModel", on_delete=models.CASCADE, related_name="prices", db_index=False)
    pricing_tier = models.ForeignKey("LlmPricingTier", on_delete=models.CASCADE, related_name="prices", db_index=False)
    usage_type = models.TextField()
    price = models.DecimalField(max_digits=20, decimal_places=12)

    class Meta:
        db_table = "llm_prices"
        constraints = [
            models.UniqueConstraint(fields=["model", "usage_type", "pricing_tier"], name="llm_prices_model_id_usage_type_pricing_tier_id_key"),
        ]


class PlatformNotification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    friendly_id = models.TextField(unique=True, db_column="friendlyId")
    title = models.TextField()
    payload = models.JSONField()
    surface = models.TextField(choices=PlatformNotificationSurface.choices)
    scope = models.TextField(choices=PlatformNotificationScope.choices)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="platform_notifications", db_column="userId", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="platform_notifications", db_column="organizationId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="platform_notifications", db_column="projectId", null=True, db_index=False)
    starts_at = UtcDateTimeField(db_column="startsAt")
    ends_at = UtcDateTimeField(db_column="endsAt")
    cli_max_days_after_first_seen = models.IntegerField(null=True, db_column="cliMaxDaysAfterFirstSeen")
    cli_max_show_count = models.IntegerField(null=True, db_column="cliMaxShowCount")
    cli_show_every = models.IntegerField(null=True, db_column="cliShowEvery")
    priority = models.IntegerField(default=0)
    is_draft = models.BooleanField(db_column="isDraft", default=False)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PlatformNotification"
        indexes = [
            models.Index(fields=["surface", "scope", "starts_at"]),
        ]


class PlatformNotificationInteraction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    notification = models.ForeignKey("PlatformNotification", on_delete=models.CASCADE, related_name="interactions", db_column="notificationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="platform_notification_interactions", db_column="userId", db_index=False)
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt", default=timezone.now)
    show_count = models.IntegerField(db_column="showCount", default=1)
    webapp_dismissed_at = UtcDateTimeField(null=True, db_column="webappDismissedAt")
    webapp_clicked_at = UtcDateTimeField(null=True, db_column="webappClickedAt")
    cli_dismissed_at = UtcDateTimeField(null=True, db_column="cliDismissedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PlatformNotificationInteraction"
        constraints = [
            models.UniqueConstraint(fields=["notification", "user"], name="PlatformNotificationInteraction_notificationId_userId_key"),
        ]


class ErrorGroupState(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="error_group_states", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="error_group_states", db_column="projectId", db_index=False)
    environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="error_group_states", db_column="environmentId", null=True, db_index=False)
    task_identifier = models.TextField(db_column="taskIdentifier")
    error_fingerprint = models.TextField(db_column="errorFingerprint")
    status = models.TextField(choices=ErrorGroupStatus.choices, default=ErrorGroupStatus.UNRESOLVED)
    ignored_until = UtcDateTimeField(null=True, db_column="ignoredUntil")
    ignored_until_occurrence_rate = models.IntegerField(null=True, db_column="ignoredUntilOccurrenceRate")
    ignored_until_total_occurrences = models.IntegerField(null=True, db_column="ignoredUntilTotalOccurrences")
    ignored_at_occurrence_count = models.BigIntegerField(null=True, db_column="ignoredAtOccurrenceCount")
    ignored_at = UtcDateTimeField(null=True, db_column="ignoredAt")
    ignored_reason = models.TextField(null=True, db_column="ignoredReason")
    ignored_by_user_id = models.TextField(null=True, db_column="ignoredByUserId")
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")
    resolved_in_version = models.TextField(null=True, db_column="resolvedInVersion")
    resolved_by = models.TextField(null=True, db_column="resolvedBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ErrorGroupState"
        constraints = [
            models.UniqueConstraint(fields=["environment", "task_identifier", "error_fingerprint"], name="ErrorGroupState_environmentId_taskIdentifier_errorFingerprint_key"),
        ]
        indexes = [
            models.Index(fields=["environment", "status"]),
        ]


class TaskIdentifier(models.Model):
    id = models.TextField(primary_key=True, default=None)
    runtime_environment = models.ForeignKey("RuntimeEnvironment", on_delete=models.CASCADE, related_name="task_identifiers", db_column="runtimeEnvironmentId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="task_identifiers", db_column="projectId", db_index=False)
    slug = models.TextField()
    current_trigger_source = models.TextField(choices=TaskTriggerSource.choices, db_column="currentTriggerSource", default=TaskTriggerSource.STANDARD)
    current_worker = models.ForeignKey("BackgroundWorker", on_delete=models.SET_NULL, related_name="task_identifiers", db_column="currentWorkerId", null=True, db_index=False)
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt", default=timezone.now)
    last_seen_at = UtcDateTimeField(db_column="lastSeenAt", default=timezone.now)
    is_in_latest_deployment = models.BooleanField(db_column="isInLatestDeployment", default=True)

    class Meta:
        db_table = "TaskIdentifier"
        constraints = [
            models.UniqueConstraint(fields=["runtime_environment", "slug"], name="TaskIdentifier_runtimeEnvironmentId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["runtime_environment", "is_in_latest_deployment"]),
        ]


class OrganizationDataStore(models.Model):
    id = models.TextField(primary_key=True, default=None)
    key = models.TextField(unique=True)
    organization_ids = ArrayField(models.TextField(), db_column="organizationIds")
    kind = models.TextField(choices=DataStoreKind.choices)
    config = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationDataStore"
        indexes = [
            models.Index(fields=["kind"]),
        ]


class BackgroundWorkerToBackgroundWorkerFile(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("BackgroundWorker", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("BackgroundWorkerFile", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_BackgroundWorkerToBackgroundWorkerFile"
        indexes = [
            models.Index(fields=["b"]),
        ]


class BackgroundWorkerToTaskQueue(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("BackgroundWorker", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("TaskQueue", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_BackgroundWorkerToTaskQueue"
        indexes = [
            models.Index(fields=["b"]),
        ]


class WaitpointRunConnections(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("TaskRun", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Waitpoint", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_WaitpointRunConnections"
        indexes = [
            models.Index(fields=["b"]),
        ]


class CompletedWaitpoints(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("TaskRunExecutionSnapshot", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Waitpoint", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_completedWaitpoints"
        indexes = [
            models.Index(fields=["b"]),
        ]
