from datetime import datetime
from datetime import timezone as dt_timezone
from decimal import Decimal
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.db.models.expressions import RawSQL
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


class SsoConnectionMigrationPhase(models.TextChoices):
    SETUP = "SETUP"
    GRACE_LEGACY = "GRACE_LEGACY"
    GRACE_DIRECT = "GRACE_DIRECT"
    FINALIZING = "FINALIZING"
    FINALIZED = "FINALIZED"


class TeamUserRole(models.TextChoices):
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"
    VIEWER = "VIEWER"
    CUSTOM = "CUSTOM"


class RoleBindingScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class GrantScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"
    RESOURCE = "RESOURCE"
    PLATFORM = "PLATFORM"


class GrantPrincipalType(models.TextChoices):
    USER = "USER"
    API_KEY = "API_KEY"
    GROUP = "GROUP"
    TEAM = "TEAM"
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"
    ANYONE = "ANYONE"


class OrganizationUserRole(models.TextChoices):
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"
    EXTERNAL = "EXTERNAL"
    DEVELOPER = "DEVELOPER"


class IssuedLicenseSource(models.TextChoices):
    BACKOFFICE = "BACKOFFICE"
    PURCHASE = "PURCHASE"
    SCRIPT = "SCRIPT"
    LEGACY_IMPORT = "LEGACY_IMPORT"


class ConnectedCreditGrantKind(models.TextChoices):
    commit = "commit"
    added = "added"
    renewal = "renewal"


class ConnectedInvoiceKind(models.TextChoices):
    annual = "annual"
    seat_change = "seat_change"
    usage = "usage"


class PlanTypes(models.TextChoices):
    FREE = "FREE"
    PRO = "PRO"
    GROWTH = "GROWTH"
    GROWTH_SEAT_EUR_MONTHLY = "GROWTH_SEAT_EUR_MONTHLY"
    GROWTH_SEAT_EUR_ANNUAL = "GROWTH_SEAT_EUR_ANNUAL"
    GROWTH_SEAT_USD_MONTHLY = "GROWTH_SEAT_USD_MONTHLY"
    GROWTH_SEAT_USD_ANNUAL = "GROWTH_SEAT_USD_ANNUAL"
    ENTERPRISE = "ENTERPRISE"
    LAUNCH = "LAUNCH"
    ACCELERATE = "ACCELERATE"
    LAUNCH_ANNUAL = "LAUNCH_ANNUAL"
    ACCELERATE_ANNUAL = "ACCELERATE_ANNUAL"


class PricingModel(models.TextChoices):
    TIERED = "TIERED"
    SEAT_EVENT = "SEAT_EVENT"


class OrganizationIntent(models.TextChoices):
    AGENT_GOVERNANCE = "AGENT_GOVERNANCE"
    LLM_OPS = "LLM_OPS"


class SubscriptionStatus(models.TextChoices):
    PENDING = "PENDING"
    FAILED = "FAILED"
    ACTIVE = "ACTIVE"
    CANCELLED = "CANCELLED"


class Currency(models.TextChoices):
    USD = "USD"
    EUR = "EUR"


class INVITESTATUS(models.TextChoices):
    PENDING = "PENDING"
    ACCEPTED = "ACCEPTED"
    WAITING_APPROVAL = "WAITING_APPROVAL"
    PAYMENT_PENDING = "PAYMENT_PENDING"
    REVOKED = "REVOKED"


class EvaluationExecutionMode(models.TextChoices):
    ON_MESSAGE = "ON_MESSAGE"
    AS_GUARDRAIL = "AS_GUARDRAIL"
    MANUALLY = "MANUALLY"


class CostType(models.TextChoices):
    TRACE_CHECK = "TRACE_CHECK"
    GUARDRAIL = "GUARDRAIL"
    CLUSTERING = "CLUSTERING"
    BATCH_EVALUATION = "BATCH_EVALUATION"


class CostReferenceType(models.TextChoices):
    CHECK = "CHECK"
    TRACE = "TRACE"
    PROJECT = "PROJECT"
    BATCH = "BATCH"


class DashboardScope(models.TextChoices):
    PRIVATE = "PRIVATE"
    PROJECT = "PROJECT"
    ORGANIZATION = "ORGANIZATION"


class TriggerAction(models.TextChoices):
    SEND_EMAIL = "SEND_EMAIL"
    ADD_TO_DATASET = "ADD_TO_DATASET"
    ADD_TO_ANNOTATION_QUEUE = "ADD_TO_ANNOTATION_QUEUE"
    SEND_SLACK_MESSAGE = "SEND_SLACK_MESSAGE"
    SEND_WEBHOOK = "SEND_WEBHOOK"


class TriggerKind(models.TextChoices):
    AUTOMATION = "AUTOMATION"
    ALERT = "ALERT"
    REPORT = "REPORT"


class AlertType(models.TextChoices):
    CRITICAL = "CRITICAL"
    WARNING = "WARNING"
    INFO = "INFO"


class SlackIntegrationScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"


class SlackIntegrationKind(models.TextChoices):
    BOT = "BOT"
    INCOMING_WEBHOOK = "INCOMING_WEBHOOK"


class WebhookDeliveryOutcome(models.TextChoices):
    success = "success"
    retryable = "retryable"
    terminal = "terminal"
    pending = "pending"


class WebhookDestinationKind(models.TextChoices):
    http = "http"
    sqs = "sqs"


class WebhookEndpointStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    DISABLED = "DISABLED"


class WebhookDeliveryChannel(models.TextChoices):
    platform = "platform"
    automations = "automations"


class ExperimentType(models.TextChoices):
    DSPY = "DSPY"
    BATCH_EVALUATION = "BATCH_EVALUATION"
    BATCH_EVALUATION_V2 = "BATCH_EVALUATION_V2"
    EVALUATIONS_V3 = "EVALUATIONS_V3"


class ModelProviderScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class ModelDefaultScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class LangyProjectionTitleSource(models.TextChoices):
    derived = "derived"
    auto = "auto"
    user = "user"


class AnnotationScoreDataType(models.TextChoices):
    CATEGORICAL = "CATEGORICAL"
    BOOLEAN = "BOOLEAN"
    LIKERT = "LIKERT"
    OPTION = "OPTION"
    CHECKBOX = "CHECKBOX"


class ShareResourceType(models.TextChoices):
    TRACE = "TRACE"
    THREAD = "THREAD"


class ShareVisibility(models.TextChoices):
    PUBLIC = "PUBLIC"
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"


class PinSource(models.TextChoices):
    manual = "manual"
    share = "share"


class RetentionPolicyScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class DataPrivacyScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    DEPARTMENT = "DEPARTMENT"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class CustomLLMModelCostScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class PromptScope(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"


class VirtualKeyStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    DISABLED = "DISABLED"
    REVOKED = "REVOKED"


class VirtualKeyPurpose(models.TextChoices):
    USER = "USER"
    LANGY = "LANGY"
    CONNECT = "CONNECT"


class GatewayProviderRotationPolicy(models.TextChoices):
    MANUAL = "MANUAL"


class GatewayProviderHealthStatus(models.TextChoices):
    UNKNOWN = "UNKNOWN"
    HEALTHY = "HEALTHY"
    DEGRADED = "DEGRADED"
    CIRCUIT_OPEN = "CIRCUIT_OPEN"


class GatewayBudgetScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"
    VIRTUAL_KEY = "VIRTUAL_KEY"
    PRINCIPAL = "PRINCIPAL"
    GROUP = "GROUP"
    ATTRIBUTED_USER = "ATTRIBUTED_USER"


class GatewayBudgetWindow(models.TextChoices):
    MINUTE = "MINUTE"
    HOUR = "HOUR"
    DAY = "DAY"
    WEEK = "WEEK"
    MONTH = "MONTH"
    TOTAL = "TOTAL"
    MANUAL = "MANUAL"


class GatewayBudgetBreachAction(models.TextChoices):
    BLOCK = "BLOCK"
    WARN = "WARN"


class GatewayBudgetLedgerStatus(models.TextChoices):
    SUCCESS = "SUCCESS"
    PROVIDER_ERROR = "PROVIDER_ERROR"
    BLOCKED_BY_GUARDRAIL = "BLOCKED_BY_GUARDRAIL"
    CANCELLED = "CANCELLED"


class GatewayChangeEventKind(models.TextChoices):
    VK_CREATED = "VK_CREATED"
    VK_CONFIG_UPDATED = "VK_CONFIG_UPDATED"
    VK_REVOKED = "VK_REVOKED"
    VK_ROTATED = "VK_ROTATED"
    VK_DISABLED = "VK_DISABLED"
    VK_ENABLED = "VK_ENABLED"
    BUDGET_CREATED = "BUDGET_CREATED"
    BUDGET_UPDATED = "BUDGET_UPDATED"
    BUDGET_DELETED = "BUDGET_DELETED"
    MODEL_PROVIDER_UPDATED = "MODEL_PROVIDER_UPDATED"
    CACHE_RULE_CREATED = "CACHE_RULE_CREATED"
    CACHE_RULE_UPDATED = "CACHE_RULE_UPDATED"
    CACHE_RULE_DELETED = "CACHE_RULE_DELETED"
    ROUTING_POLICY_UPDATED = "ROUTING_POLICY_UPDATED"
    ROUTING_POLICY_DELETED = "ROUTING_POLICY_DELETED"


class GatewayCacheRuleMode(models.TextChoices):
    RESPECT = "RESPECT"
    FORCE = "FORCE"
    DISABLE = "DISABLE"


class VirtualKeyRoutingMode(models.TextChoices):
    NONE = "NONE"
    FALLBACK_ALL = "FALLBACK_ALL"
    POLICY = "POLICY"


class VirtualKeyScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class RoutingPolicyScopeType(models.TextChoices):
    ORGANIZATION = "ORGANIZATION"
    TEAM = "TEAM"
    PROJECT = "PROJECT"


class GatewayGuardrailDirection(models.TextChoices):
    PRE = "PRE"
    POST = "POST"
    STREAM_CHUNK = "STREAM_CHUNK"


class GatewayGuardrailFailureMode(models.TextChoices):
    FAIL_OPEN = "FAIL_OPEN"
    FAIL_CLOSED = "FAIL_CLOSED"


class FeatureFlagExperimentSubjectType(models.TextChoices):
    USER = "USER"
    ORGANIZATION = "ORGANIZATION"
    PROJECT = "PROJECT"


class ProcessManagerOutboxStatus(models.TextChoices):
    pending = "pending"
    dispatched = "dispatched"
    dead = "dead"
    discarded = "discarded"


class ProcessManagerOutboxAttemptOutcome(models.TextChoices):
    retry_scheduled = "retry_scheduled"
    dead = "dead"


class GatewayRealtimeSessionStatus(models.TextChoices):
    OPEN = "OPEN"
    CLOSED = "CLOSED"
    FAILED = "FAILED"
    EXPIRED = "EXPIRED"


class ConnectedSeatChangeState(models.TextChoices):
    intent = "intent"
    invoiced = "invoiced"
    nothing_to_invoice = "nothing_to_invoice"
    not_onboarded = "not_onboarded"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField(default="oauth")
    provider = models.TextField()
    issuer = models.TextField(null=True)
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = UtcDateTimeField(null=True)
    ext_expires_in = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["issuer", "provider_account_id"]),
        ]


class AccountCredential(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="account_credentials", db_column="userId", db_index=False)
    provider = models.TextField()
    password = models.TextField(null=True)
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    id_token = models.TextField(null=True, db_column="idToken")
    access_token_expires_at = UtcDateTimeField(null=True, db_column="accessTokenExpiresAt")
    refresh_token_expires_at = UtcDateTimeField(null=True, db_column="refreshTokenExpiresAt")
    scope = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AccountCredential"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    identifier_id = models.TextField(null=True, db_column="identifierId")
    amr = ArrayField(models.TextField(), default=list)
    started_by_sign_up = models.BooleanField(db_column="startedBySignUp", default=False)
    impersonating = models.JSONField(null=True)
    actor_user_id = models.TextField(null=True, db_column="actorUserId")
    subject_user_id = models.TextField(null=True, db_column="subjectUserId")
    impersonation_reason = models.TextField(null=True, db_column="impersonationReason")
    impersonation_expires_at = UtcDateTimeField(null=True, db_column="impersonationExpiresAt")
    last_seen_at = UtcDateTimeField(null=True, db_column="lastSeenAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["identifier_id"]),
        ]


class UserErasure(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account_created_at = UtcDateTimeField(db_column="accountCreatedAt")
    erased_at = UtcDateTimeField(db_column="erasedAt", default=timezone.now)

    class Meta:
        db_table = "UserErasure"
        indexes = [
            models.Index(fields=["account_created_at"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(null=True, unique=True)
    email_verified = models.BooleanField(db_column="emailVerified", default=False)
    signup_confirmation_pending = models.BooleanField(db_column="signupConfirmationPending", default=False)
    passkey_signup_claim_hash = models.TextField(null=True, db_column="passkeySignupClaimHash")
    image = models.TextField(null=True)
    pending_sso_setup = models.BooleanField(db_column="pendingSsoSetup", default=False)
    user_hash_key = models.TextField(null=True, db_column="userHashKey")
    two_factor_enabled = models.BooleanField(db_column="twoFactorEnabled", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_login_at = UtcDateTimeField(null=True, db_column="lastLoginAt")
    deactivated_at = UtcDateTimeField(null=True, db_column="deactivatedAt")
    last_home_path = models.TextField(null=True, db_column="lastHomePath")
    traces_explorer_tour_dismissed_at = UtcDateTimeField(null=True, db_column="tracesExplorerTourDismissedAt")
    langy_code_access_preference = models.TextField(null=True, db_column="langyCodeAccessPreference")
    passkey_nudge_dismissed_at = UtcDateTimeField(null=True, db_column="passkeyNudgeDismissedAt")
    join_offer_dismissed_domains = ArrayField(models.TextField(), db_column="joinOfferDismissedDomains", default=list)
    notification_preferences = models.JSONField(db_column="notificationPreferences", default=dict)
    password_reset_required_at = UtcDateTimeField(null=True, db_column="passwordResetRequiredAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class VerificationToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField()
    token = models.TextField()
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class Identifier(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField(db_column="userId")
    provider = models.TextField()
    value = models.TextField(null=True)
    domain = models.TextField(null=True)
    identifier_hash = models.TextField(null=True, db_column="identifierHash")
    account_id = models.TextField(null=True, db_column="accountId")
    provider_id = models.TextField(null=True, db_column="providerId")
    issuer = models.TextField(null=True)
    provider_account_id = models.TextField(null=True, db_column="providerAccountId")
    state = models.TextField()
    connection_id = models.TextField(null=True, db_column="connectionId")
    verified_at = UtcDateTimeField(null=True, db_column="verifiedAt")
    attached_at = UtcDateTimeField(db_column="attachedAt")
    detached_at = UtcDateTimeField(null=True, db_column="detachedAt")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Identifier"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["identifier_hash"]),
            models.Index(fields=["value"]),
            models.Index(fields=["domain"]),
            models.Index(fields=["provider", "provider_account_id"]),
            models.Index(fields=["issuer", "provider_account_id"]),
            models.Index(fields=["account_id"]),
        ]


class IdentitySignInSwitch(models.Model):
    user_id = models.TextField(primary_key=True, db_column="userId", default=None)
    on_identifiers = models.BooleanField(db_column="onIdentifiers")
    pinned = models.BooleanField(default=False)
    switched_at = UtcDateTimeField(db_column="switchedAt")
    last_applied_event_id = models.TextField(null=True, db_column="lastAppliedEventId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IdentitySignInSwitch"


class IdentifierReservation(models.Model):
    normalized_value = models.TextField(primary_key=True, db_column="normalizedValue", default=None)
    user_id = models.TextField(db_column="userId")
    identifier_id = models.TextField(db_column="identifierId")
    command_id = models.TextField(db_column="commandId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "IdentifierReservation"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["identifier_id"]),
        ]


class IdentityProjectionCursor(models.Model):
    user_id = models.TextField(primary_key=True, db_column="userId", default=None)
    last_event_id = models.TextField(db_column="lastEventId")
    accepted_at = UtcDateTimeField(db_column="acceptedAt")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IdentityProjectionCursor"


class TwoFactor(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="two_factors", db_column="userId", db_index=False)
    secret = models.TextField()
    backup_codes = models.TextField(db_column="backupCodes")
    verified = models.BooleanField(default=True)
    failed_verification_count = models.IntegerField(db_column="failedVerificationCount", default=0)
    locked_until = UtcDateTimeField(null=True, db_column="lockedUntil")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TwoFactor"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["secret"]),
        ]


class SignInAttemptLock(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier_hash = models.TextField(unique=True, db_column="identifierHash")
    failed_count = models.IntegerField(db_column="failedCount", default=0)
    locked_until = UtcDateTimeField(null=True, db_column="lockedUntil")
    consecutive_lockouts = models.IntegerField(db_column="consecutiveLockouts", default=0)
    held_for_review = models.BooleanField(db_column="heldForReview", default=False)
    user_id = models.TextField(null=True, db_column="userId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SignInAttemptLock"
        indexes = [
            models.Index(fields=["locked_until"]),
            models.Index(fields=["user_id"]),
        ]


class Passkey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    public_key = models.TextField(db_column="publicKey")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="passkeys", db_column="userId", db_index=False)
    credential_id = models.TextField(db_column="credentialID")
    counter = models.IntegerField(default=0)
    device_type = models.TextField(db_column="deviceType")
    backed_up = models.BooleanField(db_column="backedUp", default=False)
    transports = models.TextField(null=True)
    aaguid = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Passkey"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["credential_id"]),
        ]


class MfaEnrollment(models.Model):
    user_id = models.TextField(primary_key=True, db_column="userId", default=None)
    enrollment_id = models.TextField(null=True, db_column="enrollmentId")
    method = models.TextField(null=True)
    state = models.TextField()
    enrolled_at = UtcDateTimeField(null=True, db_column="enrolledAt")
    confirmed_at = UtcDateTimeField(null=True, db_column="confirmedAt")
    expired_at = UtcDateTimeField(null=True, db_column="expiredAt")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    disabled_via = models.TextField(null=True, db_column="disabledVia")
    backup_code_count = models.IntegerField(db_column="backupCodeCount", default=0)
    consumed_backup_code_indexes = ArrayField(models.IntegerField(), db_column="consumedBackupCodeIndexes", default=list)
    failed_count = models.IntegerField(db_column="failedCount", default=0)
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    last_event_id = models.TextField(db_column="lastEventId")
    accepted_at = UtcDateTimeField(db_column="acceptedAt")
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MfaEnrollment"
        indexes = [
            models.Index(fields=["state"]),
        ]


class SsoConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    type = models.TextField()
    state = models.TextField()
    claimed_domains = ArrayField(models.TextField(), db_column="claimedDomains")
    domain_claims = models.JSONField(db_column="domainClaims", default=list)
    approved_domains = ArrayField(models.TextField(), db_column="approvedDomains")
    verified_domains = ArrayField(models.TextField(), db_column="verifiedDomains")
    lapsed_domains = ArrayField(models.TextField(), db_column="lapsedDomains")
    domain_verifications = models.JSONField(db_column="domainVerifications", default=list)
    pending_verification = models.JSONField(null=True, db_column="pendingVerification")
    idp_metadata = models.JSONField(db_column="idpMetadata")
    arrival_policy = models.TextField(db_column="arrivalPolicy", default="refuse")
    allows_jit = models.BooleanField(db_column="allowsJit", default=False)
    arrival_policy_decided_at = UtcDateTimeField(null=True, db_column="arrivalPolicyDecidedAt")
    source = models.TextField()
    test_login_account_id = models.TextField(null=True, db_column="testLoginAccountId")
    replaces_connection_id = models.TextField(null=True, db_column="replacesConnectionId")
    migration_phase = models.TextField(choices=SsoConnectionMigrationPhase.choices, null=True, db_column="migrationPhase")
    grace_started_at = UtcDateTimeField(null=True, db_column="graceStartedAt")
    route_changed_at = UtcDateTimeField(null=True, db_column="routeChangedAt")
    finalization_requested_at = UtcDateTimeField(null=True, db_column="finalizationRequestedAt")
    finalized_at = UtcDateTimeField(null=True, db_column="finalizedAt")
    rejection = models.JSONField(null=True)
    created_by = models.TextField(null=True, db_column="createdBy")
    tear_down_after = UtcDateTimeField(null=True, db_column="tearDownAfter")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    last_event_id = models.TextField(db_column="lastEventId")
    accepted_at = UtcDateTimeField(db_column="acceptedAt")
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "SsoConnection"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["organization_id", "migration_phase"]),
            models.Index(fields=["state"]),
            GinIndex(fields=["verified_domains"]),
            GinIndex(fields=["lapsed_domains"]),
        ]


class SsoConnectionRegistrationSlot(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "kind")
    organization_id = models.TextField(db_column="organizationId")
    kind = models.TextField()
    connection_id = models.TextField(unique=True, db_column="connectionId")
    replaces_connection_id = models.TextField(null=True, db_column="replacesConnectionId")
    command_id = models.TextField(db_column="commandId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SsoConnectionRegistrationSlot"


class SsoConnectionReproofCursor(models.Model):
    connection = models.OneToOneField("SsoConnection", on_delete=models.CASCADE, related_name="reproof_cursor", primary_key=True, db_column="connectionId")
    last_reproof_at = UtcDateTimeField(db_column="lastReproofAt")

    class Meta:
        db_table = "SsoConnectionReproofCursor"
        indexes = [
            models.Index(fields=["last_reproof_at"]),
        ]


class SsoCredential(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    connection_id = models.TextField(db_column="connectionId")
    kind = models.TextField()
    ciphertext = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SsoCredential"
        indexes = [
            models.Index(fields=["organization_id", "connection_id"]),
        ]


class SsoVerifiedDomain(models.Model):
    domain = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")

    class Meta:
        db_table = "SsoVerifiedDomain"
        constraints = [
            models.UniqueConstraint(fields=["domain", "organization_id"], name="SsoVerifiedDomain_domain_organizationId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class SsoVerifiedDomainHolder(models.Model):
    pk = models.CompositePrimaryKey("domain", "connection_id")
    domain = models.TextField()
    connection_id = models.TextField(db_column="connectionId")
    organization_id = models.TextField(db_column="organizationId")

    class Meta:
        db_table = "SsoVerifiedDomainHolder"
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["organization_id"]),
        ]


class SsoAuthenticationActivity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    connection_id = models.TextField(db_column="connectionId")
    user_id = models.TextField(db_column="userId")
    authenticated_at = UtcDateTimeField(db_column="authenticatedAt")
    provider_account_id = models.TextField(null=True, db_column="providerAccountId")

    class Meta:
        db_table = "SsoAuthenticationActivity"
        indexes = [
            models.Index(fields=["organization_id", "connection_id", "authenticated_at"]),
            models.Index(fields=["organization_id", "user_id"]),
        ]


class SsoProvider(models.Model):
    id = models.TextField(primary_key=True, default=None)
    issuer = models.TextField()
    oidc_config = models.TextField(null=True, db_column="oidcConfig")
    saml_config = models.TextField(null=True, db_column="samlConfig")
    user_id = models.TextField(null=True, db_column="userId")
    provider_id = models.TextField(unique=True, db_column="providerId")
    organization_id = models.TextField(null=True, db_column="organizationId")
    domain = models.TextField()

    class Meta:
        db_table = "SsoProvider"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["domain"]),
        ]


class SsoBreakGlassBinding(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    user_id = models.TextField(db_column="userId")
    granted_by_user_id = models.TextField(db_column="grantedByUserId")
    granted_at = UtcDateTimeField(db_column="grantedAt")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    superseded_at = UtcDateTimeField(null=True, db_column="supersededAt")
    renewed_from_id = models.TextField(null=True, db_column="renewedFromId")
    warned_days = ArrayField(models.IntegerField(), db_column="warnedDays")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "SsoBreakGlassBinding"
        indexes = [
            models.Index(fields=["organization_id", "expires_at"]),
            models.Index(fields=["user_id"]),
        ]


class SsoActivationRecoveryReservation(models.Model):
    command_id = models.TextField(primary_key=True, db_column="commandId", default=None)
    organization_id = models.TextField(db_column="organizationId")
    connection_id = models.TextField(db_column="connectionId")
    created_at = UtcDateTimeField(db_column="createdAt")

    class Meta:
        db_table = "SsoActivationRecoveryReservation"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "connection_id"], name="SsoActivationRecoveryReservation_organizationId_connectionId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class JoinRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField(db_column="userId")
    organization_id = models.TextField(db_column="organizationId")
    domain = models.TextField()
    state = models.TextField()
    matched_via = models.TextField(db_column="matchedVia")
    origin = models.TextField(default="web")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")
    resolved_by_type = models.TextField(null=True, db_column="resolvedByType")
    resolved_by_id = models.TextField(null=True, db_column="resolvedById")
    withdrawal_cause = models.TextField(null=True, db_column="withdrawalCause")
    connection_id = models.TextField(null=True, db_column="connectionId")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    last_event_id = models.TextField(db_column="lastEventId")
    accepted_at = UtcDateTimeField(db_column="acceptedAt")
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "JoinRequest"
        indexes = [
            models.Index(fields=["organization_id", "state"]),
            models.Index(fields=["user_id", "state"]),
            models.Index(fields=["user_id", "organization_id"]),
            models.Index(fields=["domain"]),
        ]


class TeamUser(models.Model):
    pk = models.CompositePrimaryKey("user_id", "team_id")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="team_memberships", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.RESTRICT, related_name="members", db_column="teamId", db_index=False)
    role = models.TextField(choices=TeamUserRole.choices)
    assigned_role = models.ForeignKey("CustomRole", on_delete=models.SET_NULL, related_name="assigned_users", db_column="assignedRoleId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamUser"
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["user"]),
            models.Index(fields=["assigned_role"]),
        ]


class OrganizationUser(models.Model):
    pk = models.CompositePrimaryKey("user_id", "organization_id")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="org_memberships", db_column="userId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="members", db_column="organizationId", db_index=False)
    role = models.TextField(choices=OrganizationUserRole.choices)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    department_id = models.TextField(null=True, db_column="departmentId")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    pending_sso_grant_id = models.TextField(null=True, db_column="pendingSsoGrantId")
    membership_stamp = models.TextField(db_column="membershipStamp", db_default=RawSQL("gen_random_uuid()::text", []))
    full_seat_since = UtcDateTimeField(null=True, db_column="fullSeatSince")
    seat_pending_since = UtcDateTimeField(null=True, db_column="seatPendingSince")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationUser"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["user"]),
            models.Index(fields=["department_id"]),
        ]


class SeatHold(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    user_id = models.TextField(db_column="userId")
    email = models.TextField(null=True)
    name = models.TextField(null=True)
    seat_taken_at = UtcDateTimeField(db_column="seatTakenAt")
    held_until = UtcDateTimeField(db_column="heldUntil")
    reason = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "SeatHold"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "user_id"], name="SeatHold_organizationId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["held_until"]),
        ]


class SeatRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    email = models.TextField()
    user_id = models.TextField(null=True, db_column="userId")
    name = models.TextField(null=True)
    status = models.TextField()
    source = models.TextField()
    idp_refused = models.BooleanField(db_column="idpRefused", default=False)
    idp_kind = models.TextField(null=True, db_column="idpKind")
    sales_notified_at = UtcDateTimeField(null=True, db_column="salesNotifiedAt")
    opened_at = UtcDateTimeField(db_column="openedAt")
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")
    resolved_by_user_id = models.TextField(null=True, db_column="resolvedByUserId")
    seat_pending_since = UtcDateTimeField(null=True, db_column="seatPendingSince")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SeatRequest"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "email"], name="SeatRequest_organizationId_email_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id", "status"]),
            models.Index(fields=["user_id"]),
        ]


class OrganizationSeatLease(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    holder = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OrganizationSeatLease"


class OrganizationAccessNotice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    kind = models.TextField()
    email = models.TextField()
    name = models.TextField(null=True)
    idp = models.TextField(null=True)
    day_key = models.TextField(db_column="dayKey")
    idp_refused = models.BooleanField(db_column="idpRefused", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    mailed_at = UtcDateTimeField(null=True, db_column="mailedAt")

    class Meta:
        db_table = "OrganizationAccessNotice"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "kind", "email", "day_key"], name="OrganizationAccessNotice_organizationId_kind_email_dayKey_key"),
        ]


class Team(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(unique=True)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="teams", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    is_personal = models.BooleanField(db_column="isPersonal", default=False)
    owner_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="personal_teams", db_column="ownerUserId", null=True, db_index=False)
    department_id = models.TextField(null=True, db_column="departmentId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Team"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["is_personal"]),
            models.Index(fields=["department_id"]),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    kind = models.TextField(default="customer")
    phone_number = models.TextField(null=True, db_column="phoneNumber")
    slug = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    usage_spending_max_limit = models.IntegerField(null=True, db_column="usageSpendingMaxLimit")
    dataset_attachment_max_mb = models.IntegerField(null=True, db_column="datasetAttachmentMaxMb")
    private_click_house_label = models.TextField(null=True, db_column="privateClickHouseLabel")
    private_s3_label = models.TextField(null=True, db_column="privateS3Label")
    private_dataplane_seen_at = UtcDateTimeField(null=True, db_column="privateDataplaneSeenAt")
    max_session_duration_days = models.IntegerField(db_column="maxSessionDurationDays", default=0)
    mfa_required = models.BooleanField(db_column="mfaRequired", default=False)
    lockout_after_failed_attempts = models.IntegerField(db_column="lockoutAfterFailedAttempts", default=0)
    lockout_minutes = models.IntegerField(db_column="lockoutMinutes", default=30)
    session_idle_timeout_minutes = models.IntegerField(db_column="sessionIdleTimeoutMinutes", default=1440)
    session_max_lifetime_minutes = models.IntegerField(db_column="sessionMaxLifetimeMinutes", default=0)
    signup_data = models.JSONField(null=True, db_column="signupData")
    signed_dpa = models.BooleanField(db_column="signedDPA", default=False)
    elasticsearch_node_url = models.TextField(null=True, db_column="elasticsearchNodeUrl")
    elasticsearch_api_key = models.TextField(null=True, db_column="elasticsearchApiKey")
    use_custom_elasticsearch = models.BooleanField(db_column="useCustomElasticsearch", default=False)
    s3_endpoint = models.TextField(null=True, db_column="s3Endpoint")
    s3_access_key_id = models.TextField(null=True, db_column="s3AccessKeyId")
    s3_secret_access_key = models.TextField(null=True, db_column="s3SecretAccessKey")
    s3_bucket = models.TextField(null=True, db_column="s3Bucket")
    use_custom_s3 = models.BooleanField(db_column="useCustomS3", default=False)
    sent_plan_limit_alert = UtcDateTimeField(null=True, db_column="sentPlanLimitAlert")
    sso_domain = models.TextField(null=True, unique=True, db_column="ssoDomain")
    sso_provider = models.TextField(null=True, db_column="ssoProvider")
    domain_join = models.TextField(db_column="domainJoin", default="request")
    joiner_role = models.TextField(choices=OrganizationUserRole.choices, db_column="joinerRole", default=OrganizationUserRole.MEMBER)
    join_domains = ArrayField(models.TextField(), db_column="joinDomains")
    presence_enabled = models.BooleanField(db_column="presenceEnabled", default=True)
    trace_sharing_enabled = models.BooleanField(db_column="traceSharingEnabled", default=True)
    support_contact = models.TextField(null=True, db_column="supportContact")
    primary_intent = models.TextField(choices=OrganizationIntent.choices, null=True, db_column="primaryIntent")
    promo_code = models.TextField(null=True, db_column="promoCode")
    stripe_customer_id = models.TextField(null=True, unique=True, db_column="stripeCustomerId")
    currency = models.TextField(choices=Currency.choices, default=Currency.EUR)
    pricing_model = models.TextField(choices=PricingModel.choices, db_column="pricingModel", default=PricingModel.TIERED)
    license = models.TextField(null=True)
    license_expires_at = UtcDateTimeField(null=True, db_column="licenseExpiresAt")
    license_last_validated_at = UtcDateTimeField(null=True, db_column="licenseLastValidatedAt")
    self_hosted_customer = models.BooleanField(db_column="selfHostedCustomer", default=False)
    connect_services_disabled = ArrayField(models.TextField(), db_column="connectServicesDisabled", default=list)
    instant_evals_enabled_at = UtcDateTimeField(null=True, db_column="instantEvalsEnabledAt")
    instant_evals_enabled_by_user_id = models.TextField(null=True, db_column="instantEvalsEnabledByUserId")
    connect_last_sync_at = UtcDateTimeField(null=True, db_column="connectLastSyncAt")
    connect_last_sync_error = models.TextField(null=True, db_column="connectLastSyncError")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organization"
        indexes = [
            models.Index(fields=["sso_provider"]),
        ]


class IssuedLicense(models.Model):
    id = models.TextField(primary_key=True, default=None)
    license_id = models.TextField(unique=True, db_column="licenseId")
    token_hash = models.TextField(unique=True, db_column="tokenHash")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="issued_licenses", db_column="organizationId", null=True, db_index=False)
    organization_name = models.TextField(db_column="organizationName")
    email = models.TextField()
    plan_type = models.TextField(db_column="planType")
    max_members = models.IntegerField(db_column="maxMembers")
    max_members_lite = models.IntegerField(null=True, db_column="maxMembersLite")
    lite_seats_unlimited = models.BooleanField(db_column="liteSeatsUnlimited", default=False)
    max_members_developer = models.IntegerField(null=True, db_column="maxMembersDeveloper")
    issued_at = UtcDateTimeField(db_column="issuedAt")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    source = models.TextField(choices=IssuedLicenseSource.choices)
    issued_by_id = models.TextField(null=True, db_column="issuedById")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_by_id = models.TextField(null=True, db_column="revokedById")
    revoked_reason = models.TextField(null=True, db_column="revokedReason")
    superseded_at = UtcDateTimeField(null=True, db_column="supersededAt")
    replaces_id = models.TextField(null=True, unique=True, db_column="replacesId")
    pending_delivery_license = models.TextField(null=True, db_column="pendingDeliveryLicense")
    services = ArrayField(models.TextField(), default=list)
    seat_rate_cents = models.IntegerField(null=True, db_column="seatRateCents")
    seat_currency = models.TextField(choices=Currency.choices, null=True, db_column="seatCurrency")
    commit_usd_cents = models.IntegerField(db_column="commitUsdCents", default=0)
    overage_enabled = models.BooleanField(db_column="overageEnabled", default=False)
    overage_max_usd_cents = models.IntegerField(null=True, db_column="overageMaxUsdCents")
    instance_id = models.TextField(null=True, db_column="instanceId")
    instance_bound_at = UtcDateTimeField(null=True, db_column="instanceBoundAt")
    last_sync_at = UtcDateTimeField(null=True, db_column="lastSyncAt")
    last_sync_version = models.TextField(null=True, db_column="lastSyncVersion")
    reported_members = models.IntegerField(null=True, db_column="reportedMembers")
    reported_members_lite = models.IntegerField(null=True, db_column="reportedMembersLite")
    virtual_key_id = models.TextField(null=True, unique=True, db_column="virtualKeyId")
    seats_raised_from = models.IntegerField(null=True, db_column="seatsRaisedFrom")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IssuedLicense"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["expires_at"]),
            models.Index(fields=["seats_raised_from"]),
        ]


class OrganizationLicense(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    license_key = models.TextField(null=True, db_column="licenseKey")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    validated_at = UtcDateTimeField(null=True, db_column="validatedAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationLicense"


class ActivationCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    code_hash = models.TextField(unique=True, db_column="codeHash")
    code_hint = models.TextField(db_column="codeHint")
    organization_id = models.TextField(db_column="organizationId")
    organization_name = models.TextField(db_column="organizationName")
    email = models.TextField()
    plan_type = models.TextField(db_column="planType")
    max_members = models.IntegerField(db_column="maxMembers")
    max_members_lite = models.IntegerField(null=True, db_column="maxMembersLite", default=0)
    lite_seats_unlimited = models.BooleanField(db_column="liteSeatsUnlimited", default=False)
    max_members_developer = models.IntegerField(null=True, db_column="maxMembersDeveloper")
    license_term_days = models.IntegerField(db_column="licenseTermDays")
    services = ArrayField(models.TextField(), default=list)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    reusable = models.BooleanField(default=False)
    redeemed_at = UtcDateTimeField(null=True, db_column="redeemedAt")
    redeemed_by_instance_id = models.TextField(null=True, db_column="redeemedByInstanceId")
    issued_license_id = models.TextField(null=True, db_column="issuedLicenseId")
    redemption_count = models.IntegerField(db_column="redemptionCount", default=0)
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_by_id = models.TextField(null=True, db_column="revokedById")
    created_by_id = models.TextField(null=True, db_column="createdById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ActivationCode"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["expires_at"]),
        ]


class ConnectedBillingAccount(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.OneToOneField("Organization", on_delete=models.RESTRICT, related_name="connected_billing", db_column="organizationId")
    stripe_customer_id = models.TextField(unique=True, db_column="stripeCustomerId")
    usage_subscription_id = models.TextField(null=True, db_column="usageSubscriptionId")
    usage_subscription_item_id = models.TextField(null=True, db_column="usageSubscriptionItemId")
    term_starts_at = UtcDateTimeField(db_column="termStartsAt")
    term_ends_at = UtcDateTimeField(db_column="termEndsAt")
    commit_usd_cents = models.IntegerField(db_column="commitUsdCents", default=0)
    seat_currency = models.TextField(choices=Currency.choices, db_column="seatCurrency")
    seat_rate_cents = models.IntegerField(db_column="seatRateCents")
    seats = models.IntegerField()
    bank_transfer_type = models.TextField(null=True, db_column="bankTransferType")
    bank_transfer_country = models.TextField(null=True, db_column="bankTransferCountry")
    billing_email = models.TextField(db_column="billingEmail")
    pending_renewal = models.JSONField(null=True, db_column="pendingRenewal")
    created_by_id = models.TextField(null=True, db_column="createdById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConnectedBillingAccount"


class ConnectedCreditGrant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account_id = models.TextField(db_column="accountId")
    stripe_credit_grant_id = models.TextField(unique=True, db_column="stripeCreditGrantId")
    amount_usd_cents = models.IntegerField(db_column="amountUsdCents")
    kind = models.TextField(choices=ConnectedCreditGrantKind.choices)
    term_ends_at = UtcDateTimeField(db_column="termEndsAt")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConnectedCreditGrant"
        indexes = [
            models.Index(fields=["account_id"]),
        ]


class ConnectedInvoice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account_id = models.TextField(db_column="accountId")
    stripe_invoice_id = models.TextField(unique=True, db_column="stripeInvoiceId")
    kind = models.TextField(choices=ConnectedInvoiceKind.choices)
    currency = models.TextField(choices=Currency.choices)
    amount_cents = models.IntegerField(db_column="amountCents")
    status = models.TextField()
    paid_out_of_band_at = UtcDateTimeField(null=True, db_column="paidOutOfBandAt")
    term_starts_at = UtcDateTimeField(null=True, db_column="termStartsAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConnectedInvoice"
        indexes = [
            models.Index(fields=["account_id"]),
        ]


class ScimRequestLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="scim_request_log", db_column="organizationId", db_index=False)
    connection_id = models.TextField(null=True, db_column="connectionId")
    method = models.TextField()
    resource = models.TextField()
    status = models.IntegerField()
    reason = models.TextField(null=True)
    detail = models.TextField(null=True)
    occurred_at = UtcDateTimeField(db_column="occurredAt", default=timezone.now)

    class Meta:
        db_table = "ScimRequestLog"
        indexes = [
            models.Index(fields=["organization", "occurred_at"]),
            models.Index(fields=["connection_id", "occurred_at"]),
            models.Index(fields=["occurred_at"]),
        ]


class ScimToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="scim_tokens", db_column="organizationId", db_index=False)
    connection_id = models.TextField(null=True, db_column="connectionId")
    hashed_token = models.TextField(unique=True, db_column="hashedToken")
    hash_scheme = models.TextField(db_column="hashScheme", default="sha256")
    description = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")

    class Meta:
        db_table = "ScimToken"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["connection_id"]),
        ]


class ScimExternalId(models.Model):
    organization_id = models.TextField(db_column="organizationId")
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    external_id = models.TextField(db_column="externalId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_external_ids", db_column="userId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ScimExternalId"
        constraints = [
            models.UniqueConstraint(fields=["connection_id", "external_id"], name="ScimExternalId_connectionId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id", "user"]),
            models.Index(fields=["user"]),
            models.Index(fields=["connection_id"]),
        ]


class ScimUserResource(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "user_id")
    organization_id = models.TextField(db_column="organizationId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_user_resources", db_column="userId", db_index=False)
    user_name = models.TextField(db_column="userName")
    name = models.TextField(null=True)
    active = models.BooleanField(default=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ScimUserResource"
        indexes = [
            models.Index(fields=["organization_id", "user_name"]),
            models.Index(fields=["user"]),
        ]


class ScimDirectoryUser(models.Model):
    pk = models.CompositePrimaryKey("connection_id", "user_id")
    organization_id = models.TextField(db_column="organizationId")
    connection_id = models.TextField(db_column="connectionId")
    user_id = models.TextField(db_column="userId")

    class Meta:
        db_table = "ScimDirectoryUser"
        indexes = [
            models.Index(fields=["organization_id", "user_id"]),
        ]


class ScimSyncState(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    organization_id = models.TextField(db_column="organizationId")
    state = models.TextField()
    last_pushed_at = UtcDateTimeField(null=True, db_column="lastPushedAt")
    last_failure = models.JSONField(null=True, db_column="lastFailure")
    dead_letters = models.JSONField(db_column="deadLetters", default=list)
    revoked_cause = models.TextField(null=True, db_column="revokedCause")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    last_event_id = models.TextField(db_column="lastEventId")
    accepted_at = UtcDateTimeField(db_column="acceptedAt")
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "ScimSyncState"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["state"]),
            models.Index(fields=["connection_id"]),
        ]


class ScimSsoConnectionView(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    folded = models.JSONField()
    applied_event_ids = ArrayField(models.TextField(), db_column="appliedEventIds", default=list)
    projection_version = models.TextField(db_column="projectionVersion")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "ScimSsoConnectionView"
        indexes = [
            models.Index(fields=["organization_id", "created_at"]),
        ]


class OrganizationSeatPeak(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    opened_at = models.FloatField(db_column="openedAt")
    full_seats = models.IntegerField(db_column="fullSeats")
    lite_seats = models.IntegerField(db_column="liteSeats")
    developer_seats = models.IntegerField(db_column="developerSeats")
    latest_change_at = models.FloatField(db_column="latestChangeAt")
    mismatches = models.IntegerField()

    class Meta:
        db_table = "OrganizationSeatPeak"


class OrganizationSeatPeakHolder(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "holder_kind", "holder_id")
    organization_id = models.TextField(db_column="organizationId")
    holder_kind = models.TextField(db_column="holderKind")
    holder_id = models.TextField(db_column="holderId")
    seat = models.TextField()
    last_changed_at = models.FloatField(db_column="lastChangedAt")
    last_change_id = models.TextField(null=True, db_column="lastChangeId")
    expires_at = models.FloatField(null=True, db_column="expiresAt")
    expired_by_fold = models.BooleanField(db_column="expiredByFold", default=False)
    suppressed_by = models.TextField(null=True, db_column="suppressedBy")
    restored_by_fold = models.BooleanField(db_column="restoredByFold", default=False)

    class Meta:
        db_table = "OrganizationSeatPeakHolder"
        indexes = [
            models.Index(fields=["organization_id", "holder_kind", "seat", "expires_at"]),
            models.Index(fields=["organization_id", "suppressed_by"]),
        ]


class OrganizationSeatPeakMonth(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "month")
    organization_id = models.TextField(db_column="organizationId")
    month = models.TextField()
    full_peak = models.IntegerField(db_column="fullPeak")
    full_peak_at = models.FloatField(db_column="fullPeakAt")
    lite_peak = models.IntegerField(db_column="litePeak")
    lite_peak_at = models.FloatField(db_column="litePeakAt")
    full_at_end = models.IntegerField(db_column="fullAtEnd")
    lite_at_end = models.IntegerField(db_column="liteAtEnd")

    class Meta:
        db_table = "OrganizationSeatPeakMonth"


class Project(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(unique=True)
    api_key = models.TextField(unique=True, db_column="apiKey")
    legacy_api_key_last_used_at = UtcDateTimeField(null=True, db_column="legacyApiKeyLastUsedAt")
    lwql_key = models.TextField(unique=True, db_column="lwqlKey", db_default=RawSQL("(gen_random_uuid())::text", []))
    team = models.ForeignKey("Team", on_delete=models.RESTRICT, related_name="projects", db_column="teamId", db_index=False)
    language = models.TextField()
    framework = models.TextField()
    kind = models.TextField(default="application")
    aggregate_rule = models.JSONField(null=True, db_column="aggregateRule")
    first_message = models.BooleanField(db_column="firstMessage", default=False)
    integrated = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user_link_template = models.TextField(null=True, db_column="userLinkTemplate")
    trace_sharing_enabled = models.BooleanField(db_column="traceSharingEnabled", default=True)
    presence_enabled = models.BooleanField(db_column="presenceEnabled", default=True)
    s3_endpoint = models.TextField(null=True, db_column="s3Endpoint")
    s3_access_key_id = models.TextField(null=True, db_column="s3AccessKeyId")
    s3_secret_access_key = models.TextField(null=True, db_column="s3SecretAccessKey")
    s3_bucket = models.TextField(null=True, db_column="s3Bucket")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    is_personal = models.BooleanField(db_column="isPersonal", default=False)
    owner_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="personal_projects", db_column="ownerUserId", null=True, db_index=False)
    personal_features = models.JSONField(db_column="personalFeatures", default=dict)
    department_id = models.TextField(null=True, db_column="departmentId")
    langy_egress_allowlist = models.JSONField(null=True, db_column="langyEgressAllowlist")
    last_coding_agent_session_at = UtcDateTimeField(null=True, db_column="lastCodingAgentSessionAt")
    last_coding_agent_pull_request_at = UtcDateTimeField(null=True, db_column="lastCodingAgentPullRequestAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Project"
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["is_personal"]),
            models.Index(fields=["owner_user"]),
            models.Index(fields=["team", "kind"]),
            models.Index(fields=["department_id"]),
        ]


class Department(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    name = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Department"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class DepartmentMembershipHistory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    user_id = models.TextField(db_column="userId")
    department_id = models.TextField(db_column="departmentId")
    valid_from = UtcDateTimeField(db_column="validFrom")
    valid_to = UtcDateTimeField(null=True, db_column="validTo")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "DepartmentMembershipHistory"
        indexes = [
            models.Index(fields=["organization_id", "user_id", "valid_from"]),
        ]


class PlatformToolPolicy(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    tool_slug = models.TextField(db_column="toolSlug")
    allow_vk = models.BooleanField(db_column="allowVk", default=True)
    allow_otel_direct = models.BooleanField(db_column="allowOtelDirect", default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PlatformToolPolicy"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "tool_slug"], name="PlatformToolPolicy_organizationId_toolSlug_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class OrganizationInvite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField()
    invite_code = models.TextField(unique=True, db_column="inviteCode")
    expiration = UtcDateTimeField(null=True)
    status = models.TextField(choices=INVITESTATUS.choices, default=INVITESTATUS.PENDING)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="organization_invite", db_column="organizationId", db_index=False)
    team_ids = models.TextField(db_column="teamIds")
    team_assignments = models.JSONField(null=True, db_column="teamAssignments")
    role = models.TextField(choices=OrganizationUserRole.choices)
    requested_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="invite_requests", db_column="requestedBy", null=True, db_index=False)
    subscription_id = models.TextField(null=True, db_column="subscriptionId")
    accepted_by_user_id = models.TextField(null=True, db_column="acceptedByUserId")
    accepted_via_identifier_id = models.TextField(null=True, db_column="acceptedViaIdentifierId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationInvite"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["requested_by_user"]),
        ]


class Monitor(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="checks", db_column="projectId", db_index=False)
    experiment = models.OneToOneField("Experiment", on_delete=models.RESTRICT, related_name="monitor", db_column="experimentId", null=True)
    evaluator = models.ForeignKey("Evaluator", on_delete=models.SET_NULL, related_name="monitors", db_column="evaluatorId", null=True, db_index=False)
    check_type = models.TextField(db_column="checkType")
    name = models.TextField()
    slug = models.TextField()
    execution_mode = models.TextField(choices=EvaluationExecutionMode.choices, db_column="executionMode", default=EvaluationExecutionMode.ON_MESSAGE)
    enabled = models.BooleanField(default=True)
    preconditions = models.JSONField()
    parameters = models.JSONField()
    mappings = models.JSONField(null=True)
    sample = models.FloatField(default=1)
    level = models.TextField(default="trace")
    thread_idle_timeout = models.IntegerField(null=True, db_column="threadIdleTimeout")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    auto_paused_at = UtcDateTimeField(null=True, db_column="autoPausedAt")
    auto_paused_reason = models.TextField(null=True, db_column="autoPausedReason")
    evaluator_label = models.TextField(null=True, db_column="evaluatorLabel")
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Monitor"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="Monitor_projectId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["evaluator"]),
        ]


class Cost(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="costs", db_column="projectId", db_index=False)
    cost_type = models.TextField(choices=CostType.choices, db_column="costType")
    cost_name = models.TextField(null=True, db_column="costName")
    reference_type = models.TextField(choices=CostReferenceType.choices, db_column="referenceType")
    reference_id = models.TextField(db_column="referenceId")
    amount = models.FloatField()
    currency = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    extra_info = models.JSONField(null=True, db_column="extraInfo")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Cost"
        indexes = [
            models.Index(fields=["reference_type", "reference_id"]),
            models.Index(fields=["cost_type"]),
            models.Index(fields=["project"]),
            models.Index(fields=["project", "created_at"]),
        ]


class Topic(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="topics", db_column="projectId", db_index=False)
    name = models.TextField()
    parent = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="subtopics", db_column="parentId", null=True, db_index=False)
    embeddings_model = models.TextField()
    centroid = models.JSONField()
    p95_distance = models.FloatField(db_column="p95Distance")
    automatically_generated = models.BooleanField(db_column="automaticallyGenerated", default=True)
    last_event_id = models.TextField(null=True, db_column="lastEventId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Topic"
        indexes = [
            models.Index(fields=["parent"]),
            models.Index(fields=["project"]),
        ]


class Dataset(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="datasets", db_column="projectId", db_index=False)
    name = models.TextField()
    slug = models.TextField()
    column_types = models.JSONField(db_column="columnTypes")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    mapping = models.JSONField(null=True)
    use_s3 = models.BooleanField(db_column="useS3", default=False)
    s3_record_count = models.IntegerField(null=True, db_column="s3RecordCount")
    content_layout = models.TextField(db_column="contentLayout", default="postgres")
    status = models.TextField(default="ready")
    status_error = models.TextField(null=True, db_column="statusError")
    staging_key = models.TextField(null=True, db_column="stagingKey")
    source_stored_object_id = models.TextField(null=True, db_column="sourceStoredObjectId")
    upload_filename = models.TextField(null=True, db_column="uploadFilename")
    row_count = models.IntegerField(null=True, db_column="rowCount")
    size_bytes = models.BigIntegerField(null=True, db_column="sizeBytes")
    chunk_count = models.IntegerField(null=True, db_column="chunkCount")
    chunk_offsets = models.JSONField(null=True, db_column="chunkOffsets")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Dataset"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="Dataset_projectId_slug_key"),
        ]


class DatasetRecord(models.Model):
    id = models.TextField(primary_key=True, default=None)
    dataset = models.ForeignKey("Dataset", on_delete=models.RESTRICT, related_name="dataset_records", db_column="datasetId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="dataset_records", db_column="projectId", db_index=False)
    entry = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DatasetRecord"
        indexes = [
            models.Index(fields=["dataset"]),
            models.Index(fields=["project"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["project", "dataset"]),
        ]


class DatasetVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    dataset_id = models.TextField(db_column="datasetId")
    version = models.IntegerField()
    name = models.TextField(null=True)
    trigger = models.TextField()
    content_hash = models.TextField(db_column="contentHash")
    column_types = models.JSONField(db_column="columnTypes")
    row_count = models.IntegerField(db_column="rowCount")
    size_bytes = models.BigIntegerField(db_column="sizeBytes")
    chunk_count = models.IntegerField(db_column="chunkCount")
    restored_from_version = models.IntegerField(null=True, db_column="restoredFromVersion")
    author_id = models.TextField(null=True, db_column="authorId")
    author_kind = models.TextField(null=True, db_column="authorKind")
    pinned_at = UtcDateTimeField(null=True, db_column="pinnedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "DatasetVersion"
        constraints = [
            models.UniqueConstraint(fields=["dataset_id", "version"], name="DatasetVersion_datasetId_version_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "dataset_id"]),
        ]


class DatasetVersionRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    dataset_id = models.TextField(db_column="datasetId")
    version_id = models.TextField(db_column="versionId")
    run_id = models.TextField(db_column="runId")
    experiment_id = models.TextField(null=True, db_column="experimentId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "DatasetVersionRun"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "run_id", "dataset_id"], name="DatasetVersionRun_projectId_runId_datasetId_key"),
        ]
        indexes = [
            models.Index(fields=["version_id"]),
        ]


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="dashboards", db_column="projectId", db_index=False)
    name = models.TextField()
    order = models.IntegerField(default=0)
    description = models.TextField(null=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    scope = models.TextField(choices=DashboardScope.choices, default=DashboardScope.PROJECT)
    organization_id = models.TextField(null=True, db_column="organizationId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Dashboard"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "order"]),
            models.Index(fields=["organization_id", "scope"]),
        ]


class DashboardFavourite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField(db_column="userId")
    dashboard_id = models.TextField(null=True, db_column="dashboardId")
    template_id = models.TextField(null=True, db_column="templateId")
    project_id = models.TextField(db_column="projectId")
    position = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "DashboardFavourite"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "dashboard_id"], name="DashboardFavourite_userId_dashboardId_key"),
            models.UniqueConstraint(fields=["user_id", "project_id", "template_id"], name="DashboardFavourite_userId_projectId_templateId_key"),
        ]
        indexes = [
            models.Index(fields=["user_id", "project_id", "position"]),
            models.Index(fields=["dashboard_id"]),
        ]


class DashboardsV2(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    name = models.TextField()
    description = models.TextField(null=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    scope = models.TextField(choices=DashboardScope.choices, default=DashboardScope.PROJECT)
    organization_id = models.TextField(null=True, db_column="organizationId")
    excluded_origins = models.JSONField(null=True, db_column="excludedOrigins")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DashboardsV2"
        indexes = [
            models.Index(fields=["project_id"]),
            models.Index(fields=["organization_id", "scope"]),
        ]


class DashboardWidget(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    dashboard_id = models.TextField(null=True, db_column="dashboardId")
    name = models.TextField()
    definition = models.JSONField()
    grid_column = models.IntegerField(db_column="gridColumn", default=0)
    grid_row = models.IntegerField(db_column="gridRow", default=0)
    col_span = models.IntegerField(db_column="colSpan", default=1)
    row_span = models.IntegerField(db_column="rowSpan", default=1)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DashboardWidget"
        indexes = [
            models.Index(fields=["project_id", "dashboard_id"]),
        ]


class InsightProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    title = models.TextField()
    body = models.TextField()
    tone = models.TextField()
    topic = models.TextField(null=True)
    valid_days = models.IntegerField(db_column="validDays")
    lwql = models.TextField(null=True)
    source_conversation_id = models.TextField(null=True, db_column="sourceConversationId")
    source_message_id = models.TextField(null=True, db_column="sourceMessageId")
    filed_by_user_id = models.TextField(null=True, db_column="filedByUserId")
    filed_at = models.FloatField(db_column="filedAt")
    renewed_at = models.FloatField(null=True, db_column="renewedAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")
    board_id = models.TextField(null=True, db_column="boardId")
    board_name = models.TextField(null=True, db_column="boardName")
    widget_id = models.TextField(null=True, db_column="widgetId")
    widget_name = models.TextField(null=True, db_column="widgetName")
    filed_via = models.TextField(db_column="filedVia", default="chat")
    replay_start = models.FloatField(null=True, db_column="replayStart")
    replay_end = models.FloatField(null=True, db_column="replayEnd")
    replay_granularity_seconds = models.IntegerField(null=True, db_column="replayGranularitySeconds")
    replay_context = models.JSONField(null=True, db_column="replayContext")
    owner_user_id = models.TextField(null=True, db_column="ownerUserId")
    finding_key = models.TextField(null=True, db_column="findingKey")

    class Meta:
        db_table = "InsightProjection"
        indexes = [
            models.Index(fields=["project_id", "filed_at"]),
            models.Index(fields=["project_id", "owner_user_id", "filed_at"]),
        ]


class InsightReaderProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    insight_id = models.TextField(db_column="insightId")
    user_id = models.TextField(db_column="userId")
    seen_at = models.FloatField(null=True, db_column="seenAt")
    archived_at = models.FloatField(null=True, db_column="archivedAt")
    kept_at = models.FloatField(null=True, db_column="keptAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "InsightReaderProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "insight_id", "user_id"], name="InsightReaderProjection_projectId_insightId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "user_id"]),
        ]


class InsightDailyScheduleProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    user_id = models.TextField(db_column="userId")
    board_kind = models.TextField(db_column="boardKind")
    board_id = models.TextField(db_column="boardId")
    board_name = models.TextField(db_column="boardName")
    state = models.TextField(default="undecided")
    off_reason = models.TextField(null=True, db_column="offReason")
    hour = models.IntegerField(null=True)
    timezone = models.TextField(null=True)
    max_insights = models.IntegerField(null=True, db_column="maxInsights")
    last_run_id = models.TextField(null=True, db_column="lastRunId")
    last_run_at = models.FloatField(null=True, db_column="lastRunAt")
    last_run_outcome = models.TextField(null=True, db_column="lastRunOutcome")
    last_run_reason = models.TextField(null=True, db_column="lastRunReason")
    last_run_filed = models.IntegerField(null=True, db_column="lastRunFiled")
    last_run_renewed = models.IntegerField(null=True, db_column="lastRunRenewed")
    last_run_conversation_id = models.TextField(null=True, db_column="lastRunConversationId")
    consecutive_skips = models.IntegerField(db_column="consecutiveSkips", default=0)
    skip_streak_slot = models.FloatField(null=True, db_column="skipStreakSlot")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "InsightDailyScheduleProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "user_id", "board_kind", "board_id"], name="InsightDailySchedule_projectId_userId_board_key"),
        ]


class SavedView(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="saved_views", db_column="projectId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="saved_views", db_column="userId", null=True, db_index=False)
    name = models.TextField()
    filters = models.JSONField(default=dict)
    query = models.TextField(null=True)
    period = models.JSONField(null=True)
    order = models.IntegerField(default=0)
    kind = models.TextField(default="v1-traces-filter")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SavedView"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "order"]),
            models.Index(fields=["user"]),
            models.Index(fields=["project", "kind", "order"]),
        ]


class CustomGraph(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="custom_graphs", db_column="projectId", db_index=False)
    name = models.TextField()
    graph = models.JSONField()
    filters = models.JSONField(null=True)
    kind = models.TextField(default="builder")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    dashboard = models.ForeignKey("Dashboard", on_delete=models.CASCADE, related_name="graphs", db_column="dashboardId", null=True, db_index=False)
    grid_column = models.IntegerField(db_column="gridColumn", default=0)
    grid_row = models.IntegerField(db_column="gridRow", default=0)
    col_span = models.IntegerField(db_column="colSpan", default=1)
    row_span = models.IntegerField(db_column="rowSpan", default=1)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CustomGraph"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["dashboard"]),
            models.Index(fields=["project", "kind"]),
        ]


class BatchEvaluation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    experiment = models.ForeignKey("Experiment", on_delete=models.RESTRICT, related_name="batch_evaluations", db_column="experimentId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="batch_evaluations", db_column="projectId", db_index=False)
    data = models.JSONField(default=dict)
    status = models.TextField()
    score = models.FloatField()
    label = models.TextField(null=True)
    passed = models.BooleanField()
    details = models.TextField()
    cost = models.FloatField()
    dataset_slug = models.TextField(db_column="datasetSlug")
    dataset = models.ForeignKey("Dataset", on_delete=models.RESTRICT, related_name="batch_evaluations", db_column="datasetId", db_index=False)
    evaluation = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BatchEvaluation"
        constraints = [
            models.UniqueConstraint(fields=["id"], name="BatchEvaluation_id_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["dataset"]),
            models.Index(fields=["experiment"]),
        ]


class Trigger(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="triggers", db_column="projectId", db_index=False)
    action = models.TextField(choices=TriggerAction.choices)
    trigger_kind = models.TextField(choices=TriggerKind.choices, db_column="triggerKind", default=TriggerKind.AUTOMATION)
    action_params = models.JSONField(db_column="actionParams")
    filters = models.JSONField()
    filter_query = models.TextField(null=True, db_column="filterQuery")
    last_run_at = models.FloatField(db_column="lastRunAt", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    active = models.BooleanField(default=True)
    paused_reason = models.TextField(null=True, db_column="pausedReason")
    paused_at = models.DateTimeField(null=True, db_column="pausedAt")
    message = models.TextField(null=True)
    deleted = models.BooleanField(default=False)
    alert_type = models.TextField(choices=AlertType.choices, null=True, db_column="alertType")
    slack_template_type = models.TextField(null=True, db_column="slackTemplateType")
    slack_template = models.TextField(null=True, db_column="slackTemplate")
    email_subject_template = models.TextField(null=True, db_column="emailSubjectTemplate")
    email_body_template = models.TextField(null=True, db_column="emailBodyTemplate")
    notification_cadence = models.TextField(db_column="notificationCadence", default="immediate")
    trace_debounce_ms = models.IntegerField(db_column="traceDebounceMs", default=30000)
    custom_graph = models.OneToOneField("CustomGraph", on_delete=models.CASCADE, related_name="trigger", db_column="customGraphId", null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Trigger"
        indexes = [
            models.Index(fields=["project"]),
        ]


class SlackIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    kind = models.TextField(choices=SlackIntegrationKind.choices)
    scope_type = models.TextField(choices=SlackIntegrationScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    organization_id = models.TextField(db_column="organizationId")
    bot_token_encrypted = models.TextField(null=True, db_column="botTokenEncrypted")
    webhook_url_encrypted = models.TextField(null=True, db_column="webhookUrlEncrypted")
    secret_fingerprint = models.TextField(db_column="secretFingerprint")
    secret_hint = models.TextField(db_column="secretHint")
    slack_team_id = models.TextField(null=True, db_column="slackTeamId")
    slack_team_name = models.TextField(null=True, db_column="slackTeamName")
    created_by_id = models.TextField(db_column="createdById")
    updated_by_id = models.TextField(db_column="updatedById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SlackIntegration"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "scope_type", "scope_id", "secret_fingerprint"], name="SlackIntegration_organizationId_scopeType_scopeId_secretFingerprint_key"),
        ]
        indexes = [
            models.Index(fields=["scope_type", "scope_id"]),
        ]


class SlackConnectionClaim(models.Model):
    pk = models.CompositePrimaryKey("connection_id", "claimant_id")
    connection_id = models.TextField(db_column="connectionId")
    claimant_id = models.TextField(db_column="claimantId")
    claimant_label = models.TextField(db_column="claimantLabel")
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "slack_connection_claim"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class WebhookEndpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="webhook_endpoints", db_column="organizationId", db_index=False)
    destination_kind = models.TextField(choices=WebhookDestinationKind.choices, db_column="destinationKind", default=WebhookDestinationKind.http)
    url = models.TextField(null=True)
    sqs_queue_url = models.TextField(null=True, db_column="sqsQueueUrl")
    sqs_role_arn = models.TextField(null=True, db_column="sqsRoleArn")
    sqs_external_id = models.TextField(null=True, db_column="sqsExternalId")
    sqs_access_key_id = models.TextField(null=True, db_column="sqsAccessKeyId")
    sqs_secret_access_key_encrypted = models.TextField(null=True, db_column="sqsSecretAccessKeyEncrypted")
    secret_encrypted = models.TextField(db_column="secretEncrypted")
    signature_scheme = models.TextField(null=True, db_column="signatureScheme")
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    previous_secret_encrypted = models.TextField(null=True, db_column="previousSecretEncrypted")
    previous_secret_expires_at = UtcDateTimeField(null=True, db_column="previousSecretExpiresAt")
    enabled_events = ArrayField(models.TextField(), db_column="enabledEvents", default=list)
    status = models.TextField(choices=WebhookEndpointStatus.choices, default=WebhookEndpointStatus.ACTIVE)
    disabled_reason = models.TextField(null=True, db_column="disabledReason")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    failing_since = UtcDateTimeField(null=True, db_column="failingSince")
    max_batch_size = models.IntegerField(db_column="maxBatchSize", default=100)
    max_batch_delay_ms = models.IntegerField(db_column="maxBatchDelayMs", default=250)
    max_in_flight = models.IntegerField(db_column="maxInFlight", default=4)
    allow_self_signed_certificate = models.BooleanField(db_column="allowSelfSignedCertificate", default=False)
    last_success_at = UtcDateTimeField(null=True, db_column="lastSuccessAt")
    last_failure_at = UtcDateTimeField(null=True, db_column="lastFailureAt")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WebhookEndpoint"
        constraints = [
            models.UniqueConstraint(fields=["organization", "idempotency_key"], name="WebhookEndpoint_organizationId_idempotencyKey_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "status"]),
        ]


class WebhookEndpointDelivery(models.Model):
    id = models.TextField(primary_key=True, default=None)
    channel = models.TextField(choices=WebhookDeliveryChannel.choices, default=WebhookDeliveryChannel.platform)
    organization_id = models.TextField(null=True, db_column="organizationId")
    endpoint = models.ForeignKey("WebhookEndpoint", on_delete=models.CASCADE, related_name="deliveries", db_column="endpointId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="webhook_deliveries", db_column="projectId", null=True, db_index=False)
    trigger = models.ForeignKey("Trigger", on_delete=models.CASCADE, related_name="webhook_deliveries", db_column="triggerId", null=True, db_index=False)
    dispatch_id = models.TextField(db_column="dispatchId")
    attempt = models.IntegerField(null=True)
    event_count = models.IntegerField(null=True, db_column="eventCount")
    response_status = models.IntegerField(null=True, db_column="responseStatus")
    latency_ms = models.IntegerField(null=True, db_column="latencyMs")
    error = models.TextField(null=True)
    response = models.JSONField(null=True)
    outcome = models.TextField(choices=WebhookDeliveryOutcome.choices)
    fired_at = UtcDateTimeField(db_column="firedAt", default=timezone.now)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "WebhookEndpointDelivery"
        indexes = [
            models.Index(fields=["endpoint", "fired_at"]),
            models.Index(fields=["organization_id", "fired_at"]),
            models.Index(fields=["fired_at"]),
            models.Index(fields=["endpoint", "dispatch_id"]),
            models.Index(fields=["project", "trigger", "fired_at"]),
            models.Index(fields=["project", "trigger", "dispatch_id"]),
        ]


class Experiment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    type = models.TextField(choices=ExperimentType.choices)
    slug = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="experiments", db_column="projectId", db_index=False)
    workflow = models.ForeignKey("Workflow", on_delete=models.SET_NULL, related_name="experiments", db_column="workflowId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    workbench_state = models.JSONField(null=True, db_column="workbenchState")
    workbench_version = models.IntegerField(db_column="workbenchVersion", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Experiment"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="Experiment_projectId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["workflow"]),
            models.Index(fields=["project", "archived_at"]),
        ]


class ExperimentVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    experiment_id = models.TextField(db_column="experimentId")
    project_id = models.TextField(db_column="projectId")
    version = models.IntegerField()
    counter_version = models.IntegerField(db_column="counterVersion")
    auto_saved = models.BooleanField(db_column="autoSaved", default=False)
    commit_message = models.TextField(null=True, db_column="commitMessage")
    author_id = models.TextField(null=True, db_column="authorId")
    author_label = models.TextField(db_column="authorLabel")
    run_id = models.TextField(null=True, db_column="runId")
    state = models.JSONField()
    schema_version = models.TextField(db_column="schemaVersion", default="1")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ExperimentVersion"
        constraints = [
            models.UniqueConstraint(fields=["experiment_id", "version"], name="ExperimentVersion_experimentId_version_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "experiment_id", "auto_saved"]),
            models.Index(fields=["project_id", "experiment_id", "version"]),
            models.Index(fields=["project_id", "experiment_id", "counter_version"]),
            models.Index(fields=["created_at"]),
        ]


class Annotation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="annotations", db_column="projectId", db_index=False)
    trace_id = models.TextField(db_column="traceId")
    comment = models.TextField(null=True)
    is_thumbs_up = models.BooleanField(null=True, db_column="isThumbsUp")
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="annotation", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField(null=True)
    score_options = models.JSONField(null=True, db_column="scoreOptions")
    expected_output = models.TextField(null=True, db_column="expectedOutput")
    anchor_kind = models.TextField(null=True, db_column="anchorKind")
    anchor_id = models.TextField(null=True, db_column="anchorId")
    anchor_path = models.TextField(null=True, db_column="anchorPath")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Annotation"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["trace_id"]),
            models.Index(fields=["user"]),
            models.Index(fields=["project", "trace_id"]),
            models.Index(fields=["project", "trace_id", "anchor_kind"]),
        ]


class ModelProvider(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    provider = models.TextField()
    routing_handle = models.TextField(null=True, db_column="routingHandle")
    enabled = models.BooleanField()
    custom_keys = models.JSONField(null=True, db_column="customKeys")
    extra_headers = models.JSONField(null=True, db_column="extraHeaders")
    custom_models = models.JSONField(null=True, db_column="customModels")
    custom_embeddings_models = models.JSONField(null=True, db_column="customEmbeddingsModels")
    deployment_mapping = models.JSONField(null=True, db_column="deploymentMapping")
    rate_limit_rpm = models.IntegerField(null=True, db_column="rateLimitRpm")
    rate_limit_tpm = models.IntegerField(null=True, db_column="rateLimitTpm")
    rate_limit_rpd = models.IntegerField(null=True, db_column="rateLimitRpd")
    rotation_policy = models.TextField(choices=GatewayProviderRotationPolicy.choices, db_column="rotationPolicy", default=GatewayProviderRotationPolicy.MANUAL)
    provider_config = models.JSONField(null=True, db_column="providerConfig")
    fallback_priority_global = models.IntegerField(null=True, db_column="fallbackPriorityGlobal")
    langy_skip_permissions_models = models.JSONField(null=True, db_column="langySkipPermissionsModels")
    health_status = models.TextField(choices=GatewayProviderHealthStatus.choices, db_column="healthStatus", default=GatewayProviderHealthStatus.UNKNOWN)
    circuit_opened_at = UtcDateTimeField(null=True, db_column="circuitOpenedAt")
    last_health_check_at = UtcDateTimeField(null=True, db_column="lastHealthCheckAt")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization_id = models.TextField(db_column="organizationId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ModelProvider"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "routing_handle"], name="ModelProvider_organizationId_routingHandle_key"),
        ]
        indexes = [
            models.Index(fields=["health_status"]),
            models.Index(fields=["fallback_priority_global"]),
            models.Index(fields=["organization_id"]),
        ]


class ModelProviderScope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    model_provider = models.ForeignKey("ModelProvider", on_delete=models.CASCADE, related_name="scopes", db_column="modelProviderId", db_index=False)
    scope_type = models.TextField(choices=ModelProviderScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ModelProviderScope"
        constraints = [
            models.UniqueConstraint(fields=["model_provider", "scope_type", "scope_id"], name="ModelProviderScope_modelProviderId_scopeType_scopeId_key"),
        ]
        indexes = [
            models.Index(fields=["scope_type", "scope_id"]),
            models.Index(fields=["model_provider"]),
        ]


class ModelDefaultConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    config = models.JSONField()
    author_id = models.TextField(null=True, db_column="authorId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization_id = models.TextField(db_column="organizationId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ModelDefaultConfig"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class ModelDefaultConfigScope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    config = models.ForeignKey("ModelDefaultConfig", on_delete=models.CASCADE, related_name="scopes", db_column="configId", db_index=False)
    scope_type = models.TextField(choices=ModelDefaultScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ModelDefaultConfigScope"
        constraints = [
            models.UniqueConstraint(fields=["config", "scope_type", "scope_id"], name="ModelDefaultConfigScope_configId_scopeType_scopeId_key"),
        ]
        indexes = [
            models.Index(fields=["scope_type", "scope_id"]),
            models.Index(fields=["config"]),
        ]


class GithubInstallation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    installation_id = models.TextField(unique=True, db_column="installationId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="github_installations", db_column="organizationId", db_index=False)
    account_login = models.TextField(db_column="accountLogin")
    account_type = models.TextField(db_column="accountType")
    account_id = models.TextField(db_column="accountId")
    repository_selection = models.TextField(db_column="repositorySelection")
    repositories = models.JSONField(null=True)
    suspended_at = UtcDateTimeField(null=True, db_column="suspendedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GithubInstallation"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class GithubPullRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="github_pull_requests", db_column="organizationId", db_index=False)
    repository_host = models.TextField(db_column="repositoryHost")
    repository_full_name = models.TextField(db_column="repositoryFullName")
    head_branch = models.TextField(db_column="headBranch")
    pr_number = models.IntegerField(db_column="prNumber")
    html_url = models.TextField(db_column="htmlUrl")
    title = models.TextField()
    state = models.TextField()
    is_draft = models.BooleanField(db_column="isDraft", default=False)
    author_login = models.TextField(null=True, db_column="authorLogin")
    pr_created_at = UtcDateTimeField(db_column="prCreatedAt")
    pr_closed_at = UtcDateTimeField(null=True, db_column="prClosedAt")
    pr_merged_at = UtcDateTimeField(null=True, db_column="prMergedAt")
    pr_updated_at = UtcDateTimeField(null=True, db_column="prUpdatedAt")
    mapped_at = UtcDateTimeField(db_column="mappedAt", default=timezone.now)
    last_checked_at = UtcDateTimeField(db_column="lastCheckedAt", default=timezone.now)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GithubPullRequest"
        constraints = [
            models.UniqueConstraint(fields=["organization", "repository_host", "repository_full_name", "pr_number"], name="GithubPullRequest_organizationId_repositoryHost_repositoryFullName_prNumber_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "repository_host", "repository_full_name", "head_branch"]),
        ]


class GithubBranchPullRequestCheck(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="github_branch_pr_checks", db_column="organizationId", db_index=False)
    repository_host = models.TextField(db_column="repositoryHost")
    repository_full_name = models.TextField(db_column="repositoryFullName")
    head_branch = models.TextField(db_column="headBranch")
    last_checked_at = UtcDateTimeField(db_column="lastCheckedAt")
    pr_count = models.IntegerField(db_column="prCount", default=0)
    not_found_at = UtcDateTimeField(null=True, db_column="notFoundAt")
    recheck_after = UtcDateTimeField(null=True, db_column="recheckAfter")
    attempts = models.IntegerField(default=0)
    last_requested_at = UtcDateTimeField(db_column="lastRequestedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GithubBranchPullRequestCheck"
        constraints = [
            models.UniqueConstraint(fields=["organization", "repository_host", "repository_full_name", "head_branch"], name="GithubBranchPullRequestCheck_organizationId_repositoryHost_repositoryFullName_headBranch_key"),
        ]
        indexes = [
            models.Index(fields=["recheck_after", "last_requested_at", "not_found_at"]),
        ]


class LangyConversationProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    conversation_id = models.TextField(db_column="conversationId")
    user_id = models.TextField(db_column="userId")
    title = models.TextField(null=True)
    title_source = models.TextField(choices=LangyProjectionTitleSource.choices, db_column="titleSource")
    origin = models.TextField(default="interactive")
    status = models.TextField()
    is_shared = models.BooleanField(db_column="isShared", default=False)
    shared_at = models.FloatField(null=True, db_column="sharedAt")
    shared_by_id = models.TextField(null=True, db_column="sharedById")
    message_count = models.IntegerField(db_column="messageCount", default=0)
    last_activity_at = models.FloatField(null=True, db_column="lastActivityAt")
    current_turn_id = models.TextField(null=True, db_column="currentTurnId")
    last_error = models.TextField(null=True, db_column="lastError")
    last_model = models.TextField(null=True, db_column="lastModel")
    pending_handoff_token = models.TextField(null=True, db_column="pendingHandoffToken")
    pending_handoff_turn_id = models.TextField(null=True, db_column="pendingHandoffTurnId")
    run_token = models.TextField(null=True, db_column="runToken")
    archived_at = models.FloatField(null=True, db_column="archivedAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "LangyConversationProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "conversation_id"], name="LangyConversationProjection_projectId_conversationId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "user_id", "conversation_id"]),
            models.Index(fields=["project_id", "user_id", "last_activity_at"]),
        ]


class LangyTurnRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    user_id = models.TextField(db_column="userId")
    request_id = models.TextField(db_column="requestId")
    conversation_id = models.TextField(db_column="conversationId")
    turn_id = models.TextField(db_column="turnId")
    status = models.TextField()
    lease_owner = models.TextField(db_column="leaseOwner")
    lease_expires_at = UtcDateTimeField(db_column="leaseExpiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LangyTurnRequest"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "user_id", "request_id"], name="LangyTurnRequest_projectId_userId_requestId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "conversation_id"]),
        ]


class LangyActiveTurn(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    conversation_id = models.TextField(db_column="conversationId")
    turn_id = models.TextField(db_column="turnId")
    request_id = models.TextField(db_column="requestId")
    user_id = models.TextField(db_column="userId")
    status = models.TextField()
    lease_owner = models.TextField(db_column="leaseOwner")
    lease_expires_at = UtcDateTimeField(db_column="leaseExpiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LangyActiveTurn"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "conversation_id"], name="LangyActiveTurn_projectId_conversationId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "user_id"]),
        ]


class LangyConversationTurnProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    conversation_id = models.TextField(db_column="conversationId")
    turn_id = models.TextField(db_column="turnId")
    status = models.TextField()
    question_parts = models.JSONField(db_column="questionParts")
    answer_parts = models.JSONField(db_column="answerParts")
    tool_calls = models.JSONField(db_column="toolCalls")
    plan = models.JSONField(null=True)
    error = models.TextField(null=True)
    started_at = models.FloatField(null=True, db_column="startedAt")
    ended_at = models.FloatField(null=True, db_column="endedAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "LangyConversationTurnProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "conversation_id", "turn_id"], name="LangyConversationTurnProjection_projectId_conversationId_turnId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "conversation_id", "updated_at"]),
        ]


class LangyMessageProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    conversation_id = models.TextField(db_column="conversationId")
    message_id = models.TextField(db_column="messageId")
    role = models.TextField()
    parts = models.JSONField()
    source_event_id = models.TextField(db_column="sourceEventId")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")

    class Meta:
        db_table = "LangyMessageProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "conversation_id", "message_id"], name="LangyMessageProjection_projectId_conversationId_messageId_key"),
            models.UniqueConstraint(fields=["project_id", "source_event_id"], name="LangyMessageProjection_projectId_sourceEventId_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "conversation_id", "created_at"]),
        ]


class ProjectSecret(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="project_secrets", db_column="projectId", db_index=False)
    name = models.TextField()
    encrypted_value = models.TextField(db_column="encryptedValue")
    created_by = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="secrets_created", db_column="createdById", db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="secrets_updated", db_column="updatedById", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProjectSecret"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="ProjectSecret_projectId_name_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class TriggerSent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    trigger = models.ForeignKey("Trigger", on_delete=models.RESTRICT, related_name="trigger_sent", db_column="triggerId", db_index=False)
    trace_id = models.TextField(null=True, db_column="traceId")
    custom_graph = models.ForeignKey("CustomGraph", on_delete=models.CASCADE, related_name="trigger_sent", db_column="customGraphId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="trigger_sent", db_column="projectId", db_index=False)
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")
    open_incident_key = models.TextField(null=True, unique=True, db_column="openIncidentKey")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TriggerSent"
        constraints = [
            models.UniqueConstraint(fields=["trigger", "trace_id"], name="TriggerSent_triggerId_traceId_key"),
        ]
        indexes = [
            models.Index(fields=["trigger"]),
            models.Index(fields=["project"]),
            models.Index(fields=["custom_graph"]),
            models.Index(fields=["resolved_at"]),
            models.Index(fields=["project", "trigger", "created_at", "id"]),
        ]


class TriggerLatestEvaluation(models.Model):
    trigger = models.OneToOneField("Trigger", on_delete=models.CASCADE, related_name="latest_evaluation", primary_key=True, db_column="triggerId")
    project_id = models.TextField(db_column="projectId")
    evaluated_at = models.DateTimeField(db_column="evaluatedAt")
    verdict = models.TextField()
    observed_value = models.FloatField(null=True, db_column="observedValue")
    threshold = models.FloatField(null=True)
    operator = models.TextField(null=True)
    time_period_minutes = models.IntegerField(null=True, db_column="timePeriodMinutes")
    skip_code = models.TextField(null=True, db_column="skipCode")
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TriggerLatestEvaluation"
        indexes = [
            models.Index(fields=["project_id"]),
        ]


class EmailSuppression(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="email_suppressions", db_column="projectId", db_index=False)
    email = models.TextField()
    trigger_id = models.TextField(null=True, db_column="triggerId")
    reason = models.TextField(default="unsubscribe")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "EmailSuppression"
        constraints = [
            models.UniqueConstraint(fields=["project", "email", "trigger_id"], name="EmailSuppression_projectId_email_triggerId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class AnnotationScore(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="annotation_scores", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    description = models.TextField(null=True)
    active = models.BooleanField(default=True)
    data_type = models.TextField(choices=AnnotationScoreDataType.choices, null=True, db_column="dataType")
    options = models.JSONField(null=True)
    default_value = models.JSONField(null=True, db_column="defaultValue")
    global_field = models.BooleanField(db_column="global", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AnnotationScore"
        indexes = [
            models.Index(fields=["project"]),
        ]


class AnnotationQueue(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="annotation_queue", db_column="projectId", db_index=False)
    description = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AnnotationQueue"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="AnnotationQueue_projectId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class AnnotationQueueMembers(models.Model):
    pk = models.CompositePrimaryKey("annotation_queue_id", "user_id")
    annotation_queue = models.ForeignKey("AnnotationQueue", on_delete=models.RESTRICT, related_name="members", db_column="annotationQueueId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="annotation_queues", db_column="userId", db_index=False)

    class Meta:
        db_table = "AnnotationQueueMembers"
        indexes = [
            models.Index(fields=["annotation_queue"]),
            models.Index(fields=["user"]),
        ]


class AnnotationQueueScores(models.Model):
    pk = models.CompositePrimaryKey("annotation_queue_id", "annotation_score_id")
    annotation_queue = models.ForeignKey("AnnotationQueue", on_delete=models.RESTRICT, related_name="annotation_queue_scores", db_column="annotationQueueId", db_index=False)
    annotation_score = models.ForeignKey("AnnotationScore", on_delete=models.RESTRICT, related_name="annotation_queue_scores", db_column="annotationScoreId", db_index=False)

    class Meta:
        db_table = "AnnotationQueueScores"
        indexes = [
            models.Index(fields=["annotation_queue"]),
            models.Index(fields=["annotation_score"]),
        ]


class AnnotationQueueItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    annotation_queue = models.ForeignKey("AnnotationQueue", on_delete=models.SET_NULL, related_name="annotation_queue_items", db_column="annotationQueueId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="assigned_queue_items", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_queue_items", db_column="createdByUserId", null=True, db_index=False)
    trace_id = models.TextField(db_column="traceId")
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="annotation_queue_item", db_column="projectId", db_index=False)
    done_at = UtcDateTimeField(null=True, db_column="doneAt")
    marked_for_dataset_at = UtcDateTimeField(null=True, db_column="markedForDatasetAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AnnotationQueueItem"
        constraints = [
            models.UniqueConstraint(fields=["trace_id", "user", "project"], name="AnnotationQueueItem_traceId_userId_projectId_key"),
            models.UniqueConstraint(fields=["trace_id", "annotation_queue", "project"], name="AnnotationQueueItem_traceId_annotationQueueId_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["annotation_queue"]),
            models.Index(fields=["user"]),
            models.Index(fields=["project"]),
            models.Index(fields=["project", "done_at"]),
            models.Index(fields=["project", "user", "done_at"]),
            models.Index(fields=["annotation_queue", "done_at"]),
        ]


class ShareLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    resource_type = models.TextField(choices=ShareResourceType.choices, db_column="resourceType")
    resource_id = models.TextField(db_column="resourceId")
    thread_id = models.TextField(null=True, db_column="threadId")
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="share_links", db_column="projectId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="share_links", db_column="userId", null=True, db_index=False)
    visibility = models.TextField(choices=ShareVisibility.choices, default=ShareVisibility.PUBLIC)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    max_views = models.IntegerField(null=True, db_column="maxViews")
    view_count = models.IntegerField(db_column="viewCount", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ShareLink"
        indexes = [
            models.Index(fields=["project", "resource_type", "resource_id"]),
            models.Index(fields=["user"]),
        ]


class PinnedTrace(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="pinned_traces", db_column="projectId", db_index=False)
    trace_id = models.TextField(db_column="traceId")
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="pinned_traces", db_column="userId", null=True, db_index=False)
    source = models.TextField(choices=PinSource.choices, default=PinSource.manual)
    reason = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "PinnedTrace"
        constraints = [
            models.UniqueConstraint(fields=["project", "trace_id"], name="PinnedTrace_projectId_traceId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class TraceEditOverlay(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="trace_edit_overlays", db_column="projectId", db_index=False)
    trace_id = models.TextField(db_column="traceId")
    patch = models.JSONField()
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="trace_edit_overlays_created", db_column="createdById", null=True, db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="trace_edit_overlays_updated", db_column="updatedById", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TraceEditOverlay"
        constraints = [
            models.UniqueConstraint(fields=["project", "trace_id"], name="TraceEditOverlay_projectId_traceId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class TraceIngestSourceBilling(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "source_type")
    organization_id = models.TextField(db_column="organizationId")
    source_type = models.TextField(db_column="sourceType")
    billed = models.BooleanField()
    recorded_at = UtcDateTimeField(db_column="recordedAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TraceIngestSourceBilling"


class RetentionPolicy(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    scope_type = models.TextField(choices=RetentionPolicyScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    category = models.TextField()
    retention_days = models.IntegerField(db_column="retentionDays")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RetentionPolicy"
        constraints = [
            models.UniqueConstraint(fields=["scope_type", "scope_id", "category"], name="RetentionPolicy_scopeType_scopeId_category_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["scope_type", "scope_id"]),
        ]


class DataPrivacyPolicy(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    scope_type = models.TextField(choices=DataPrivacyScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    personal_only = models.BooleanField(db_column="personalOnly", default=False)
    config = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataPrivacyPolicy"
        constraints = [
            models.UniqueConstraint(fields=["scope_type", "scope_id", "personal_only"], name="DataPrivacyPolicy_scopeType_scopeId_personalOnly_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["scope_type", "scope_id"]),
        ]


class InstantEvalJudgeProject(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    organization_id = models.TextField(db_column="organizationId")
    created_at = UtcDateTimeField(db_column="createdAt")

    class Meta:
        db_table = "InstantEvalJudgeProject"


class InstantEvalJudgeUsageBilling(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    usage_billed = models.BooleanField(db_column="usageBilled")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    from_catch_up = models.BooleanField(db_column="fromCatchUp")

    class Meta:
        db_table = "InstantEvalJudgeUsageBilling"


class InstantEvalJudgeSpend(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "request_id")
    organization_id = models.TextField(db_column="organizationId")
    request_id = models.TextField(db_column="requestId")
    spend_nano_usd = models.BigIntegerField(db_column="spendNanoUsd")
    occurred_at = UtcDateTimeField(db_column="occurredAt")

    class Meta:
        db_table = "InstantEvalJudgeSpend"


class CustomLLMModelCost(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    scope_type = models.TextField(choices=CustomLLMModelCostScopeType.choices, db_column="scopeType", default=CustomLLMModelCostScopeType.PROJECT)
    scope_id = models.TextField(db_column="scopeId")
    project_id = models.TextField(null=True, db_column="projectId")
    model = models.TextField()
    regex = models.TextField()
    input_cost_per_token = models.FloatField(null=True, db_column="inputCostPerToken")
    output_cost_per_token = models.FloatField(null=True, db_column="outputCostPerToken")
    cache_read_cost_per_token = models.FloatField(null=True, db_column="cacheReadCostPerToken")
    cache_creation_cost_per_token = models.FloatField(null=True, db_column="cacheCreationCostPerToken")
    cache_creation1h_cost_per_token = models.FloatField(null=True, db_column="cacheCreation1hCostPerToken")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CustomLLMModelCost"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["scope_type", "scope_id"]),
            models.Index(fields=["project_id"]),
        ]


class Workflow(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="workflows", db_column="projectId", db_index=False)
    name = models.TextField()
    icon = models.TextField()
    description = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    latest_version = models.ForeignKey("WorkflowVersion", on_delete=models.RESTRICT, related_name="workflow_as_latest_version", db_column="latestVersionId", null=True, db_index=False)
    current_version = models.ForeignKey("WorkflowVersion", on_delete=models.RESTRICT, related_name="workflow_as_current_version", db_column="currentVersionId", null=True, db_index=False)
    published_id = models.TextField(null=True, db_column="publishedId")
    published_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="workflow", db_column="publishedById", null=True, db_index=False)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    is_evaluator = models.BooleanField(db_column="isEvaluator", default=False)
    is_component = models.BooleanField(db_column="isComponent", default=False)
    copied_from = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="copied_workflows", db_column="copiedFromWorkflowId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workflow"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["published_by"]),
            models.Index(fields=["latest_version"]),
            models.Index(fields=["current_version"]),
            models.Index(fields=["copied_from"]),
        ]


class WorkflowVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.TextField()
    commit_message = models.TextField(db_column="commitMessage")
    author = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="workflow_version", db_column="authorId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="workflow_version", db_column="projectId", db_index=False)
    workflow = models.ForeignKey("Workflow", on_delete=models.RESTRICT, related_name="versions", db_column="workflowId", db_index=False)
    parent = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="children", db_column="parentId", null=True, db_index=False)
    auto_saved = models.BooleanField(db_column="autoSaved", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    dsl = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkflowVersion"
        constraints = [
            models.UniqueConstraint(fields=["workflow", "version"], name="WorkflowVersion_workflowId_version_key"),
        ]
        indexes = [
            models.Index(fields=["workflow"]),
            models.Index(fields=["project"]),
            models.Index(fields=["parent"]),
            models.Index(fields=["author"]),
            models.Index(fields=["created_at"]),
        ]


class AuditLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user_id = models.TextField(null=True, db_column="userId")
    project_id = models.TextField(null=True, db_column="projectId")
    organization_id = models.TextField(null=True, db_column="organizationId")
    action = models.TextField()
    args = models.JSONField(null=True)
    error = models.TextField(null=True)
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    metadata = models.JSONField(null=True)
    actor_user_id = models.TextField(null=True, db_column="actorUserId")
    target_kind = models.TextField(null=True, db_column="targetKind")
    target_id = models.TextField(null=True, db_column="targetId")
    before = models.JSONField(null=True)
    after = models.JSONField(null=True)
    idempotency_key = models.TextField(null=True, unique=True, db_column="idempotencyKey")

    class Meta:
        db_table = "AuditLog"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["user_id"]),
            models.Index(fields=["actor_user_id"]),
            models.Index(fields=["project_id"]),
            models.Index(fields=["action"]),
            models.Index(fields=["ip_address"]),
            models.Index(fields=["organization_id", "created_at"]),
            models.Index(fields=["target_kind", "target_id"]),
        ]


class LlmPromptConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    handle = models.TextField(null=True, unique=True)
    name = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="llm_prompt_configs", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    organization_id = models.TextField(db_column="organizationId")
    scope = models.TextField(choices=PromptScope.choices, default=PromptScope.PROJECT)
    copied_from = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="copied_prompts", db_column="copiedFromPromptId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LlmPromptConfig"
        constraints = [
            models.UniqueConstraint(fields=["handle"], name="LlmPromptConfig_handle_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["organization_id", "scope"]),
            models.Index(fields=["copied_from"]),
        ]


class LlmPromptConfigVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.IntegerField()
    commit_message = models.TextField(null=True, db_column="commitMessage")
    author = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="llm_prompt_config_versions", db_column="authorId", null=True, db_index=False)
    config = models.ForeignKey("LlmPromptConfig", on_delete=models.CASCADE, related_name="versions", db_column="configId", db_index=False)
    config_data = models.JSONField(db_column="config")
    schema_version = models.TextField(db_column="schemaVersion")
    runtime_parameters = models.JSONField(db_column="runtimeParameters", default=dict)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    project_id = models.TextField(db_column="projectId")

    class Meta:
        db_table = "LlmPromptConfigVersion"
        constraints = [
            models.UniqueConstraint(fields=["config", "version"], name="LlmPromptConfigVersion_configId_version_key"),
        ]
        indexes = [
            models.Index(fields=["config"]),
            models.Index(fields=["author"]),
            models.Index(fields=["created_at"]),
        ]


class PromptTagAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    config = models.ForeignKey("LlmPromptConfig", on_delete=models.CASCADE, related_name="version_tags", db_column="configId", db_index=False)
    version = models.ForeignKey("LlmPromptConfigVersion", on_delete=models.CASCADE, related_name="version_tags", db_column="versionId", db_index=False)
    prompt_tag = models.ForeignKey("PromptTag", on_delete=models.CASCADE, related_name="assignments", db_column="tagId", db_index=False)
    project_id = models.TextField(db_column="projectId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="version_tags_created", db_column="createdById", null=True, db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="version_tags_updated", db_column="updatedById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PromptTagAssignment"
        constraints = [
            models.UniqueConstraint(fields=["config", "prompt_tag"], name="PromptTagAssignment_configId_tagId_key"),
        ]
        indexes = [
            models.Index(fields=["config"]),
            models.Index(fields=["version"]),
            models.Index(fields=["project_id"]),
            models.Index(fields=["prompt_tag"]),
        ]


class PromptTag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="prompt_tags", db_column="organizationId", db_index=False)
    name = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="prompt_tags_created", db_column="createdById", null=True, db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="prompt_tags_updated", db_column="updatedById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PromptTag"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="PromptTag_organizationId_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class Analytics(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="analytics", db_column="projectId", db_index=False)
    key = models.TextField()
    value = models.JSONField(null=True)
    numeric_value = models.FloatField(null=True, db_column="numericValue")
    string_value = models.TextField(null=True, db_column="stringValue")
    bool_value = models.BooleanField(null=True, db_column="boolValue")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "Analytics"
        indexes = [
            models.Index(fields=["created_at", "key", "project", "numeric_value"]),
            models.Index(fields=["project"]),
        ]


class CustomRole(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="custom_roles", db_column="organizationId", db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    permissions = models.JSONField()
    kind = models.TextField(default="custom")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CustomRole"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="CustomRole_organizationId_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class Notification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="notifications", db_column="organizationId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="notifications", db_column="projectId", null=True, db_index=False)
    metadata = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    sent_at = UtcDateTimeField(db_column="sentAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Notification"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["project"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["sent_at"]),
        ]


class Agent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="agents", db_column="projectId", db_index=False)
    name = models.TextField()
    type = models.TextField()
    config = models.JSONField()
    workflow = models.ForeignKey("Workflow", on_delete=models.SET_NULL, related_name="agents", db_column="workflowId", null=True, db_index=False)
    copied_from = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="copied_agents", db_column="copiedFromAgentId", null=True, db_index=False)
    environment = models.TextField(null=True)
    owner_user_id = models.TextField(null=True, db_column="ownerUserId")
    host_label = models.TextField(null=True, db_column="hostLabel")
    identity_key = models.TextField(null=True, db_column="identityKey")
    last_seen_at = UtcDateTimeField(null=True, db_column="lastSeenAt")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Agent"
        constraints = [
            models.UniqueConstraint(fields=["project", "identity_key"], name="Agent_projectId_identityKey_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["workflow"]),
            models.Index(fields=["copied_from"]),
            models.Index(fields=["project", "last_seen_at"]),
        ]


class AgentVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    agent_id = models.TextField(db_column="agentId")
    version = models.IntegerField()
    name = models.TextField()
    type = models.TextField()
    config = models.JSONField()
    workflow_id = models.TextField(null=True, db_column="workflowId")
    commit_message = models.TextField(null=True, db_column="commitMessage")
    author_id = models.TextField(null=True, db_column="authorId")
    author_kind = models.TextField(null=True, db_column="authorKind")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AgentVersion"
        constraints = [
            models.UniqueConstraint(fields=["agent_id", "version"], name="AgentVersion_agentId_version_key"),
        ]
        indexes = [
            models.Index(fields=["project_id"]),
        ]


class AgentVersionLabel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    agent_id = models.TextField(db_column="agentId")
    label = models.TextField()
    version_id = models.TextField(db_column="versionId")
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AgentVersionLabel"
        constraints = [
            models.UniqueConstraint(fields=["agent_id", "label"], name="AgentVersionLabel_agentId_label_key"),
        ]
        indexes = [
            models.Index(fields=["project_id"]),
            models.Index(fields=["version_id"]),
        ]


class Evaluator(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="evaluators", db_column="projectId", db_index=False)
    name = models.TextField()
    slug = models.TextField(null=True)
    type = models.TextField()
    config = models.JSONField()
    workflow = models.ForeignKey("Workflow", on_delete=models.SET_NULL, related_name="evaluators", db_column="workflowId", null=True, db_index=False)
    copied_from = models.ForeignKey("self", on_delete=models.RESTRICT, related_name="copied_evaluators", db_column="copiedFromEvaluatorId", null=True, db_index=False)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Evaluator"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="Evaluator_projectId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["workflow"]),
            models.Index(fields=["copied_from"]),
        ]


class EvaluatorVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    evaluator_id = models.TextField(db_column="evaluatorId")
    version = models.IntegerField()
    name = models.TextField()
    type = models.TextField()
    config = models.JSONField()
    workflow_id = models.TextField(null=True, db_column="workflowId")
    commit_message = models.TextField(null=True, db_column="commitMessage")
    author_id = models.TextField(null=True, db_column="authorId")
    author_kind = models.TextField(null=True, db_column="authorKind")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "EvaluatorVersion"
        constraints = [
            models.UniqueConstraint(fields=["evaluator_id", "version"], name="EvaluatorVersion_evaluatorId_version_key"),
        ]
        indexes = [
            models.Index(fields=["project_id"]),
        ]


class EvaluatorVersionLabel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    evaluator_id = models.TextField(db_column="evaluatorId")
    label = models.TextField()
    version_id = models.TextField(db_column="versionId")
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EvaluatorVersionLabel"
        constraints = [
            models.UniqueConstraint(fields=["evaluator_id", "label"], name="EvaluatorVersionLabel_evaluatorId_label_key"),
        ]
        indexes = [
            models.Index(fields=["project_id"]),
            models.Index(fields=["version_id"]),
        ]


class Scenario(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="scenarios", db_column="projectId", db_index=False)
    name = models.TextField()
    situation = models.TextField()
    criteria = ArrayField(models.TextField())
    labels = ArrayField(models.TextField())
    parameters = models.JSONField(null=True)
    simulator_model = models.TextField(null=True, db_column="simulatorModel")
    judge_model = models.TextField(null=True, db_column="judgeModel")
    max_turns = models.IntegerField(null=True, db_column="maxTurns")
    min_turns = models.IntegerField(null=True, db_column="minTurns")
    fields = models.JSONField(null=True)
    caller_voice = models.JSONField(null=True, db_column="callerVoice")
    test_suite = models.ForeignKey("SimulationSuite", on_delete=models.SET_NULL, related_name="test_suite_scenarios", db_column="testSuiteId", null=True, db_index=False)
    version = models.IntegerField(default=1)
    last_updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="scenarios", db_column="lastUpdatedById", null=True, db_index=False)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Scenario"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "test_suite"]),
        ]


class ScenarioVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scenario = models.ForeignKey("Scenario", on_delete=models.CASCADE, related_name="versions", db_column="scenarioId", db_index=False)
    project_id = models.TextField(db_column="projectId")
    version = models.IntegerField()
    author_id = models.TextField(null=True, db_column="authorId")
    author_label = models.TextField(db_column="authorLabel")
    change_description = models.TextField(null=True, db_column="changeDescription")
    snapshot = models.JSONField()
    schema_version = models.IntegerField(db_column="schemaVersion", default=1)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ScenarioVersion"
        constraints = [
            models.UniqueConstraint(fields=["scenario", "version"], name="ScenarioVersion_scenarioId_version_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "scenario", "version"]),
            models.Index(fields=["created_at"]),
        ]


class SimulationSuite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="suite_configurations", db_column="projectId", db_index=False)
    name = models.TextField()
    slug = models.TextField()
    description = models.TextField(null=True)
    scenario_ids = ArrayField(models.TextField(), db_column="scenarioIds")
    targets = models.JSONField()
    repeat_count = models.IntegerField(db_column="repeatCount", default=1)
    labels = ArrayField(models.TextField())
    simulator_model = models.TextField(null=True, db_column="simulatorModel")
    judge_model = models.TextField(null=True, db_column="judgeModel")
    kind = models.TextField(default="run_plan")
    scope = models.JSONField(null=True)
    fields = models.JSONField(null=True)
    evaluators = models.JSONField(null=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SimulationSuite"
        constraints = [
            models.UniqueConstraint(fields=["project", "slug"], name="SimulationSuite_projectId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "kind"]),
        ]


class Subscription(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="subscriptions", db_column="organizationId", db_index=False)
    plan = models.TextField(choices=PlanTypes.choices)
    stripe_subscription_id = models.TextField(null=True, unique=True, db_column="stripeSubscriptionId")
    status = models.TextField(choices=SubscriptionStatus.choices)
    start_date = UtcDateTimeField(null=True, db_column="startDate")
    end_date = UtcDateTimeField(null=True, db_column="endDate")
    last_payment_failed_date = UtcDateTimeField(null=True, db_column="lastPaymentFailedDate")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    max_members = models.IntegerField(null=True, db_column="maxMembers")
    max_projects = models.IntegerField(null=True, db_column="maxProjects")
    max_messages_per_month = models.IntegerField(null=True, db_column="maxMessagesPerMonth")
    max_workflows = models.IntegerField(null=True, db_column="maxWorkflows")
    max_retention_days = models.IntegerField(null=True, db_column="maxRetentionDays")
    max_members_lite = models.IntegerField(null=True, db_column="maxMembersLite")
    max_members_developer = models.IntegerField(null=True, db_column="maxMembersDeveloper")
    max_teams = models.IntegerField(null=True, db_column="maxTeams")
    max_prompts = models.IntegerField(null=True, db_column="maxPrompts")
    max_evaluators = models.IntegerField(null=True, db_column="maxEvaluators")
    max_scenarios = models.IntegerField(null=True, db_column="maxScenarios")
    max_agents = models.IntegerField(null=True, db_column="maxAgents")
    max_experiments = models.IntegerField(null=True, db_column="maxExperiments")
    max_online_evaluations = models.IntegerField(null=True, db_column="maxOnlineEvaluations")
    max_datasets = models.IntegerField(null=True, db_column="maxDatasets")
    max_dashboards = models.IntegerField(null=True, db_column="maxDashboards")
    max_custom_graphs = models.IntegerField(null=True, db_column="maxCustomGraphs")
    max_automations = models.IntegerField(null=True, db_column="maxAutomations")
    seats_kept = models.IntegerField(null=True, db_column="seatsKept")
    seats_kept_until = UtcDateTimeField(null=True, db_column="seatsKeptUntil")
    seat_line_quantity = models.IntegerField(null=True, db_column="seatLineQuantity")
    seat_line_quantity_at = UtcDateTimeField(null=True, db_column="seatLineQuantityAt")
    seat_line_write_quantities = ArrayField(models.IntegerField(), db_column="seatLineWriteQuantities", default=list)
    seat_line_write_since = UtcDateTimeField(null=True, db_column="seatLineWriteSince")
    seat_line_write_at = UtcDateTimeField(null=True, db_column="seatLineWriteAt")
    seat_line_write_attempts = models.IntegerField(null=True, db_column="seatLineWriteAttempts")
    seat_line_write_flagged_at = UtcDateTimeField(null=True, db_column="seatLineWriteFlaggedAt")
    seat_repair_quantity = models.IntegerField(null=True, db_column="seatRepairQuantity")
    seat_repair_lifted_quantity = models.IntegerField(null=True, db_column="seatRepairLiftedQuantity")
    seat_repair_since = UtcDateTimeField(null=True, db_column="seatRepairSince")
    seat_repair_attempts = models.IntegerField(null=True, db_column="seatRepairAttempts")
    seat_repair_flagged_at = UtcDateTimeField(null=True, db_column="seatRepairFlaggedAt")
    seat_line_lease_token = models.TextField(null=True, db_column="seatLineLeaseToken")
    seat_line_lease_until = UtcDateTimeField(null=True, db_column="seatLineLeaseUntil")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Subscription"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class Invoice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    subscription = models.ForeignKey("Subscription", on_delete=models.RESTRICT, related_name="invoices", db_column="subscriptionId", db_index=False)
    total_amount = models.FloatField(db_column="totalAmount")
    currency = models.TextField()
    due_date = UtcDateTimeField(db_column="dueDate")
    paid = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Invoice"
        indexes = [
            models.Index(fields=["subscription"]),
        ]


class InvoiceItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    invoice = models.ForeignKey("Invoice", on_delete=models.RESTRICT, related_name="line_items", db_column="invoiceId", db_index=False)
    description = models.TextField()
    amount = models.FloatField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InvoiceItem"
        indexes = [
            models.Index(fields=["invoice"]),
        ]


class Group(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="groups", db_column="organizationId", db_index=False)
    name = models.TextField()
    slug = models.TextField()
    external_id = models.TextField(null=True, db_column="externalId")
    scim_source = models.TextField(null=True, db_column="scimSource")
    scim_connection_id = models.TextField(null=True, db_column="scimConnectionId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Group"
        constraints = [
            models.UniqueConstraint(fields=["organization", "slug"], name="Group_organizationId_slug_key"),
            models.UniqueConstraint(fields=["scim_connection_id", "external_id"], name="Group_scimConnectionId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["scim_connection_id"]),
        ]


class GroupMembership(models.Model):
    pk = models.CompositePrimaryKey("user_id", "group_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="group_memberships", db_column="userId", db_index=False)
    group = models.ForeignKey("Group", on_delete=models.CASCADE, related_name="members", db_column="groupId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "GroupMembership"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["group"]),
        ]


class AuthzUserStanding(models.Model):
    user_id = models.TextField(primary_key=True, db_column="userId", default=None)
    deactivated_at = UtcDateTimeField(null=True, db_column="deactivatedAt")
    erased_at = UtcDateTimeField(null=True, db_column="erasedAt")
    standing_changed_at = UtcDateTimeField(db_column="standingChangedAt")

    class Meta:
        db_table = "AuthzUserStanding"


class AuthzEngineSwitch(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    on_engine = models.BooleanField(db_column="onEngine")
    pinned = models.BooleanField(default=False)
    switched_at = UtcDateTimeField(db_column="switchedAt")
    last_applied_event_id = models.TextField(null=True, db_column="lastAppliedEventId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthzEngineSwitch"


class AuthzOrganizationPolicy(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    braids_organization_admins_only = models.BooleanField(db_column="braidsOrganizationAdminsOnly", default=False)
    updated_by_user_id = models.TextField(db_column="updatedByUserId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthzOrganizationPolicy"


class RoleBinding(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="role_bindings", db_column="organizationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="role_bindings", db_column="userId", null=True, db_index=False)
    group = models.ForeignKey("Group", on_delete=models.CASCADE, related_name="role_bindings", db_column="groupId", null=True, db_index=False)
    api_key = models.ForeignKey("ApiKey", on_delete=models.RESTRICT, related_name="role_bindings", db_column="apiKeyId", null=True, db_index=False)
    role = models.TextField(choices=TeamUserRole.choices)
    custom_role = models.ForeignKey("CustomRole", on_delete=models.SET_NULL, related_name="role_bindings", db_column="customRoleId", null=True, db_index=False)
    scope_type = models.TextField(choices=RoleBindingScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RoleBinding"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["user"]),
            models.Index(fields=["group"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["scope_type", "scope_id"]),
        ]


class Grant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    principal_type = models.TextField(choices=GrantPrincipalType.choices, db_column="principalType")
    principal_id = models.TextField(null=True, db_column="principalId")
    role_key = models.TextField(null=True, db_column="roleKey")
    legacy_role = models.TextField(null=True, db_column="legacyRole")
    source = models.TextField()
    scope_type = models.TextField(choices=GrantScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    token = models.TextField(null=True, unique=True)
    permission = models.TextField(null=True)
    resource_kind = models.TextField(null=True, db_column="resourceKind")
    project_id = models.TextField(null=True, db_column="projectId")
    created_by_user_id = models.TextField(null=True, db_column="createdByUserId")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    max_views = models.IntegerField(null=True, db_column="maxViews")
    condition = models.JSONField(null=True)
    replaces_grant_id = models.TextField(null=True, db_column="replacesGrantId")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_reason = models.TextField(null=True, db_column="revokedReason")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Grant"
        indexes = [
            models.Index(fields=["organization_id", "scope_type", "scope_id"]),
            models.Index(fields=["project_id", "resource_kind", "scope_id"]),
        ]


class GrantUsage(models.Model):
    grant_id = models.TextField(primary_key=True, db_column="grantId", default=None)
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    view_count = models.IntegerField(db_column="viewCount", default=0)
    last_viewed_at = UtcDateTimeField(null=True, db_column="lastViewedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GrantUsage"
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["project_id"]),
        ]


class Role(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    name = models.TextField()
    description = models.TextField(null=True)
    permissions = models.JSONField()
    kind = models.TextField(default="custom")
    occurred_at = UtcDateTimeField(db_column="occurredAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Role"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    lookup_id = models.TextField(unique=True, db_column="lookupId")
    hashed_secret = models.TextField(db_column="hashedSecret")
    permission_mode = models.TextField(db_column="permissionMode", default="all")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="api_keys", db_column="userId", null=True, db_index=False)
    created_by_user_id = models.TextField(null=True, db_column="createdByUserId")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="api_keys", db_column="organizationId", db_index=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revocation_cause = models.TextField(null=True, db_column="revocationCause")
    parent_api_key_id = models.TextField(null=True, db_column="parentApiKeyId")
    ingest_source_type = models.TextField(null=True, db_column="ingestSourceType")
    ingestion_template_id = models.TextField(null=True, db_column="ingestionTemplateId")
    created_by_device_label = models.TextField(null=True, db_column="createdByDeviceLabel")
    is_system_managed = models.BooleanField(db_column="isSystemManaged", default=False)
    created_by_browser_session_id = models.TextField(null=True, db_column="createdByBrowserSessionId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ApiKey"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["lookup_id"]),
            models.Index(fields=["ingest_source_type"]),
            models.Index(fields=["ingestion_template_id"]),
        ]


class SigningKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    project_id = models.TextField(db_column="projectId")
    name = models.TextField()
    public_id = models.TextField(unique=True, db_column="publicId")
    secret_ciphertext = models.TextField(db_column="secretCiphertext")
    created_by_user_id = models.TextField(db_column="createdByUserId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    rotated_at = UtcDateTimeField(null=True, db_column="rotatedAt")
    verifies_until = UtcDateTimeField(null=True, db_column="verifiesUntil")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")

    class Meta:
        db_table = "SigningKey"
        indexes = [
            models.Index(fields=["project_id"]),
            models.Index(fields=["organization_id"]),
        ]


class ProjectActiveDay(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    day = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ProjectActiveDay"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "day"], name="ProjectActiveDay_projectId_day_key"),
        ]


class NurturingProject(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    organization_id = models.TextField(db_column="organizationId")

    class Meta:
        db_table = "NurturingProject"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class NurturingUncopiedProject(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt", default=timezone.now)

    class Meta:
        db_table = "NurturingUncopiedProject"


class NurturingOrganizationMember(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "user_id")
    organization_id = models.TextField(db_column="organizationId")
    user_id = models.TextField(db_column="userId")
    joined_at = UtcDateTimeField(null=True, db_column="joinedAt")
    left_at = UtcDateTimeField(null=True, db_column="leftAt")
    returned_at = UtcDateTimeField(null=True, db_column="returnedAt")

    class Meta:
        db_table = "NurturingOrganizationMember"
        indexes = [
            models.Index(fields=["user_id"]),
        ]


class NurturingOrganizationCreated(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    created_at = UtcDateTimeField(db_column="createdAt")

    class Meta:
        db_table = "NurturingOrganizationCreated"
        indexes = [
            models.Index(fields=["created_at"]),
        ]


class NurturingOrganization(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    admin_user_id = models.TextField(null=True, db_column="adminUserId")
    seeded = models.BooleanField(default=False)
    evaluation_count = models.IntegerField(db_column="evaluationCount", default=0)
    simulation_run_count = models.IntegerField(db_column="simulationRunCount", default=0)
    coding_first_session_at = UtcDateTimeField(null=True, db_column="codingFirstSessionAt")
    coding_agent_types = ArrayField(models.TextField(), db_column="codingAgentTypes", default=list)
    coding_active_days = models.IntegerField(db_column="codingActiveDays", default=0)
    coding_last_active_day = models.TextField(null=True, db_column="codingLastActiveDay")
    coding_terminal_types = ArrayField(models.TextField(), db_column="codingTerminalTypes", default=list)
    coding_repository_keys = ArrayField(models.TextField(), db_column="codingRepositoryKeys", default=list)
    coding_first_days = ArrayField(models.IntegerField(), db_column="codingFirstDays", default=list)
    last_trace_at = UtcDateTimeField(null=True, db_column="lastTraceAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "NurturingOrganization"


class NurturingProjectUsageView(models.Model):
    pk = models.CompositePrimaryKey("project_id", "owner")
    project_id = models.TextField(db_column="projectId")
    owner = models.TextField()
    folded = models.JSONField()
    applied_event_ids = ArrayField(models.TextField(), db_column="appliedEventIds", default=list)
    projection_version = models.TextField(db_column="projectionVersion")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "NurturingProjectUsageView"


class NurturingProjectTraceDay(models.Model):
    pk = models.CompositePrimaryKey("project_id", "day")
    project_id = models.TextField(db_column="projectId")
    day = models.TextField()
    trace_count = models.IntegerField(db_column="traceCount", default=0)
    error_count = models.IntegerField(db_column="errorCount", default=0)
    last_trace_at = UtcDateTimeField(null=True, db_column="lastTraceAt")
    last_failed_trace_at = UtcDateTimeField(null=True, db_column="lastFailedTraceAt")

    class Meta:
        db_table = "NurturingProjectTraceDay"


class NurturingPersonalProject(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    organization_id = models.TextField(db_column="organizationId")
    owner_user_id = models.TextField(db_column="ownerUserId")

    class Meta:
        db_table = "NurturingPersonalProject"


class NurturingTracedProject(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    organization_id = models.TextField(db_column="organizationId")

    class Meta:
        db_table = "NurturingTracedProject"
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class BillingMeterCheckpoint(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    billing_month = models.TextField(db_column="billingMonth")
    meter = models.TextField(default="langwatch_billable_events")
    last_reported_total = models.IntegerField(db_column="lastReportedTotal", default=0)
    pending_reported_total = models.IntegerField(null=True, db_column="pendingReportedTotal")
    consecutive_failures = models.IntegerField(db_column="consecutiveFailures", default=0)
    last_counted_event_id = models.TextField(null=True, db_column="lastCountedEventId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BillingMeterCheckpoint"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "billing_month", "meter"], name="BillingMeterCheckpoint_organizationId_billingMonth_meter_key"),
        ]


class VirtualKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    name = models.TextField()
    description = models.TextField(null=True)
    status = models.TextField(choices=VirtualKeyStatus.choices, default=VirtualKeyStatus.ACTIVE)
    purpose = models.TextField(choices=VirtualKeyPurpose.choices, default=VirtualKeyPurpose.USER)
    external_id = models.TextField(null=True, db_column="externalId")
    metadata = models.JSONField(default=dict)
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    disabled_reason = models.TextField(null=True, db_column="disabledReason")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    hashed_secret = models.TextField(unique=True, db_column="hashedSecret")
    display_prefix = models.TextField(db_column="displayPrefix")
    principal_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="principal_virtual_keys", db_column="principalUserId", null=True, db_index=False)
    trace_project_id = models.TextField(null=True, db_column="traceProjectId")
    config = models.JSONField(default=dict)
    revision = models.BigIntegerField(default=0)
    previous_hashed_secret = models.TextField(null=True, db_column="previousHashedSecret")
    previous_secret_valid_until = UtcDateTimeField(null=True, db_column="previousSecretValidUntil")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_by_id = models.TextField(null=True, db_column="revokedById")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(db_column="createdById")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    routing_policy = models.ForeignKey("RoutingPolicy", on_delete=models.SET_NULL, related_name="virtual_keys", db_column="routingPolicyId", null=True, db_index=False)
    routing_mode = models.TextField(choices=VirtualKeyRoutingMode.choices, db_column="routingMode", default=VirtualKeyRoutingMode.NONE)
    connect_services = ArrayField(models.TextField(), db_column="connectServices", default=list)
    license_token_hash = models.TextField(null=True, unique=True, db_column="licenseTokenHash")
    license_instance_id = models.TextField(null=True, db_column="licenseInstanceId")
    license_expires_at = UtcDateTimeField(null=True, db_column="licenseExpiresAt")
    license_id = models.TextField(null=True, db_column="licenseId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VirtualKey"
        constraints = [
            models.UniqueConstraint(fields=["hashed_secret"], name="VirtualKey_hashedSecret_key"),
            models.UniqueConstraint(fields=["organization_id", "external_id"], name="VirtualKey_organizationId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["display_prefix"]),
            models.Index(fields=["status"]),
            models.Index(fields=["principal_user"]),
            models.Index(fields=["revision"]),
            models.Index(fields=["routing_policy"]),
            models.Index(fields=["organization_id", "purpose"]),
        ]


class VirtualKeyScope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    virtual_key = models.ForeignKey("VirtualKey", on_delete=models.CASCADE, related_name="scopes", db_column="virtualKeyId", db_index=False)
    scope_type = models.TextField(choices=VirtualKeyScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "VirtualKeyScope"
        constraints = [
            models.UniqueConstraint(fields=["virtual_key", "scope_type", "scope_id"], name="VirtualKeyScope_virtualKeyId_scopeType_scopeId_key"),
        ]
        indexes = [
            models.Index(fields=["scope_type", "scope_id"]),
            models.Index(fields=["virtual_key"]),
        ]


class RoutingPolicy(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="routing_policies", db_column="organizationId", db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    model_provider_ids = models.JSONField(db_column="modelProviderIds", default=list)
    model_aliases = models.JSONField(db_column="modelAliases", default=dict)
    default_model = models.TextField(null=True, db_column="defaultModel")
    policy_rules = models.JSONField(db_column="policyRules", default=dict)
    is_default = models.BooleanField(db_column="isDefault", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="routing_policies_created", db_column="createdById", null=True, db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="routing_policies_updated", db_column="updatedById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RoutingPolicy"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class RoutingPolicyScope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    routing_policy = models.ForeignKey("RoutingPolicy", on_delete=models.CASCADE, related_name="scopes", db_column="routingPolicyId", db_index=False)
    scope_type = models.TextField(choices=RoutingPolicyScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "RoutingPolicyScope"
        constraints = [
            models.UniqueConstraint(fields=["routing_policy", "scope_type", "scope_id"], name="RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key"),
        ]
        indexes = [
            models.Index(fields=["scope_type", "scope_id"]),
            models.Index(fields=["routing_policy"]),
        ]


class GatewayGuardrail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="gateway_guardrails", db_column="projectId", db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    evaluator = models.ForeignKey("Evaluator", on_delete=models.RESTRICT, related_name="gateway_guardrails", db_column="evaluatorId", db_index=False)
    direction = models.TextField(choices=GatewayGuardrailDirection.choices)
    failure_mode = models.TextField(choices=GatewayGuardrailFailureMode.choices, db_column="failureMode", default=GatewayGuardrailFailureMode.FAIL_CLOSED)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="gateway_guardrails_created", db_column="createdById", null=True, db_index=False)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="gateway_guardrails_updated", db_column="updatedById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayGuardrail"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["evaluator"]),
            models.Index(fields=["project", "archived_at"]),
        ]


class IngestionSource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="ingestion_sources", db_column="organizationId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="ingestion_sources", db_column="teamId", null=True, db_index=False)
    source_type = models.TextField(db_column="sourceType")
    name = models.TextField()
    description = models.TextField(null=True)
    ingest_secret_hash = models.TextField(db_column="ingestSecretHash")
    parser_config = models.JSONField(db_column="parserConfig", default=dict)
    poller_cursor = models.JSONField(null=True, db_column="pollerCursor")
    error_count = models.IntegerField(db_column="errorCount", default=0)
    pull_schedule = models.TextField(null=True, db_column="pullSchedule")
    status = models.TextField(default="awaiting_first_event")
    trace_project_id = models.TextField(null=True, db_column="traceProjectId")
    provider_account_id = models.TextField(null=True, db_column="providerAccountId")
    last_event_at = UtcDateTimeField(null=True, db_column="lastEventAt")
    last_success_at = UtcDateTimeField(null=True, db_column="lastSuccessAt")
    last_read_through_at = UtcDateTimeField(null=True, db_column="lastReadThroughAt")
    last_run_completeness = models.TextField(null=True, db_column="lastRunCompleteness")
    unpriced_usage_since = UtcDateTimeField(null=True, db_column="unpricedUsageSince")
    unpriced_usage_through = UtcDateTimeField(null=True, db_column="unpricedUsageThrough")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="ingestion_sources_created", db_column="createdById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IngestionSource"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="IngestionSource_organizationId_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["team"]),
            models.Index(fields=["source_type"]),
        ]


class AnomalyRule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="anomaly_rules", db_column="organizationId", db_index=False)
    scope = models.TextField()
    scope_id = models.TextField(db_column="scopeId")
    name = models.TextField()
    description = models.TextField(null=True)
    severity = models.TextField()
    rule_type = models.TextField(db_column="ruleType")
    threshold_config = models.JSONField(db_column="thresholdConfig", default=dict)
    destination_config = models.JSONField(db_column="destinationConfig", default=dict)
    status = models.TextField(default="active")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="anomaly_rules_created", db_column="createdById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AnomalyRule"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="AnomalyRule_organizationId_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["scope", "scope_id"]),
            models.Index(fields=["rule_type"]),
            models.Index(fields=["status"]),
        ]


class AnomalyAlert(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="anomaly_alerts", db_column="organizationId", db_index=False)
    rule = models.ForeignKey("AnomalyRule", on_delete=models.CASCADE, related_name="alerts", db_column="ruleId", db_index=False)
    severity = models.TextField()
    rule_name = models.TextField(db_column="ruleName")
    rule_type = models.TextField(db_column="ruleType")
    trigger_window_start = UtcDateTimeField(db_column="triggerWindowStart")
    trigger_window_end = UtcDateTimeField(db_column="triggerWindowEnd")
    trigger_spend_usd = models.DecimalField(max_digits=18, decimal_places=6, null=True, db_column="triggerSpendUsd")
    trigger_event_count = models.IntegerField(null=True, db_column="triggerEventCount")
    detail = models.JSONField(default=dict)
    state = models.TextField(default="open")
    destination_status = models.JSONField(db_column="destinationStatus", default=dict)
    detected_at = UtcDateTimeField(db_column="detectedAt", default=timezone.now)
    acknowledged_at = UtcDateTimeField(null=True, db_column="acknowledgedAt")
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")

    class Meta:
        db_table = "AnomalyAlert"
        constraints = [
            models.UniqueConstraint(fields=["rule", "trigger_window_start"], name="AnomalyAlert_ruleId_triggerWindowStart_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "detected_at"]),
            models.Index(fields=["rule"]),
            models.Index(fields=["state"]),
        ]


class AiToolEntry(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="ai_tool_entries", db_column="organizationId", db_index=False)
    scope = models.TextField()
    scope_id = models.TextField(db_column="scopeId")
    type = models.TextField()
    display_name = models.TextField(db_column="displayName")
    slug = models.TextField()
    icon_key = models.TextField(null=True, db_column="iconKey")
    icon_asset = models.TextField(null=True, db_column="iconAsset")
    order = models.IntegerField(default=0)
    enabled = models.BooleanField(default=True)
    config = models.JSONField(default=dict)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AiToolEntry"
        indexes = [
            models.Index(fields=["organization", "scope", "scope_id"]),
            models.Index(fields=["organization", "enabled", "archived_at"]),
            models.Index(fields=["organization", "type"]),
        ]


class AiToolEntryTeam(models.Model):
    id = models.TextField(primary_key=True, default=None)
    entry = models.ForeignKey("AiToolEntry", on_delete=models.CASCADE, related_name="teams", db_column="entryId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="ai_tool_entry_teams", db_column="teamId", db_index=False)

    class Meta:
        db_table = "AiToolEntryTeam"
        constraints = [
            models.UniqueConstraint(fields=["entry", "team"], name="AiToolEntryTeam_entryId_teamId_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class AiToolEntryDepartment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    entry = models.ForeignKey("AiToolEntry", on_delete=models.CASCADE, related_name="departments", db_column="entryId", db_index=False)
    department_id = models.TextField(db_column="departmentId")

    class Meta:
        db_table = "AiToolEntryDepartment"
        constraints = [
            models.UniqueConstraint(fields=["entry", "department_id"], name="AiToolEntryDepartment_entryId_departmentId_key"),
        ]
        indexes = [
            models.Index(fields=["department_id"]),
        ]


class IngestionTemplate(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="ingestion_templates", db_column="organizationId", null=True, db_index=False)
    slug = models.TextField()
    source_type = models.TextField(db_column="sourceType")
    display_name = models.TextField(db_column="displayName")
    description = models.TextField(null=True)
    icon_asset = models.TextField(null=True, db_column="iconAsset")
    credential_schema = models.TextField(null=True, db_column="credentialSchema")
    ottl_rules = models.TextField(db_column="ottlRules", default="")
    platform_published = models.BooleanField(db_column="platformPublished", default=False)
    enabled = models.BooleanField(default=True)
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(null=True, db_column="createdById")
    updated_by_id = models.TextField(null=True, db_column="updatedById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IngestionTemplate"
        constraints = [
            models.UniqueConstraint(fields=["organization", "slug"], name="IngestionTemplate_organizationId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "archived_at"]),
            models.Index(fields=["source_type"]),
        ]


class GatewayConnectUpstream(models.Model):
    organization_id = models.TextField(primary_key=True, db_column="organizationId", default=None)
    base_url = models.TextField(db_column="baseUrl")
    encrypted_token = models.TextField(db_column="encryptedToken")
    instance_id = models.TextField(db_column="instanceId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayConnectUpstream"


class GatewayTraceExportKey(models.Model):
    project_id = models.TextField(primary_key=True, db_column="projectId", default=None)
    api_key_id = models.TextField(db_column="apiKeyId")
    encrypted_token = models.TextField(db_column="encryptedToken")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "GatewayTraceExportKey"


class GatewayBudget(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    scope_type = models.TextField(choices=GatewayBudgetScopeType.choices, db_column="scopeType")
    scope_id = models.TextField(db_column="scopeId")
    provider_key = models.TextField(null=True, db_column="providerKey")
    name = models.TextField()
    description = models.TextField(null=True)
    window = models.TextField(choices=GatewayBudgetWindow.choices)
    limit_usd = models.DecimalField(max_digits=18, decimal_places=6, db_column="limitUsd")
    on_breach = models.TextField(choices=GatewayBudgetBreachAction.choices, db_column="onBreach", default=GatewayBudgetBreachAction.BLOCK)
    timezone_field = models.TextField(null=True, db_column="timezone")
    external_id = models.TextField(null=True, db_column="externalId")
    metadata = models.JSONField(default=dict)
    spent_usd = models.DecimalField(max_digits=18, decimal_places=6, db_column="spentUsd", default=Decimal("0"))
    current_period_started_at = UtcDateTimeField(db_column="currentPeriodStartedAt", default=timezone.now)
    resets_at = UtcDateTimeField(db_column="resetsAt")
    last_reset_at = UtcDateTimeField(null=True, db_column="lastResetAt")
    cycle_anchor_at = UtcDateTimeField(null=True, db_column="cycleAnchorAt")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="gateway_budgets_created", db_column="createdById", db_index=False)
    managed_by_virtual_key_id = models.TextField(null=True, db_column="managedByVirtualKeyId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayBudget"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "external_id"], name="GatewayBudget_organizationId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
            models.Index(fields=["scope_type", "scope_id", "provider_key"]),
            models.Index(fields=["managed_by_virtual_key_id"]),
            models.Index(fields=["resets_at"]),
        ]


class GatewayBudgetBucketBoundary(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    budget = models.ForeignKey("GatewayBudget", on_delete=models.CASCADE, related_name="bucket_boundaries", db_column="budgetId", db_index=False)
    bucket_scope_id = models.TextField(db_column="bucketScopeId")
    period_started_at = UtcDateTimeField(db_column="periodStartedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayBudgetBucketBoundary"
        constraints = [
            models.UniqueConstraint(fields=["budget", "bucket_scope_id"], name="GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class GatewayBudgetLedger(models.Model):
    id = models.TextField(primary_key=True, default=None)
    budget = models.ForeignKey("GatewayBudget", on_delete=models.CASCADE, related_name="ledger_entries", db_column="budgetId", db_index=False)
    virtual_key = models.ForeignKey("VirtualKey", on_delete=models.CASCADE, related_name="ledger_entries", db_column="virtualKeyId", db_index=False)
    model_provider_id = models.TextField(null=True, db_column="modelProviderId")
    provider_key = models.TextField(null=True, db_column="providerKey")
    gateway_request_id = models.TextField(db_column="gatewayRequestId")
    amount_usd = models.DecimalField(max_digits=18, decimal_places=6, db_column="amountUsd")
    tokens_input = models.IntegerField(db_column="tokensInput", default=0)
    tokens_output = models.IntegerField(db_column="tokensOutput", default=0)
    tokens_cache_read = models.IntegerField(db_column="tokensCacheRead", default=0)
    tokens_cache_write = models.IntegerField(db_column="tokensCacheWrite", default=0)
    model = models.TextField()
    duration_ms = models.IntegerField(null=True, db_column="durationMs")
    status = models.TextField(choices=GatewayBudgetLedgerStatus.choices, default=GatewayBudgetLedgerStatus.SUCCESS)
    occurred_at = UtcDateTimeField(db_column="occurredAt", default=timezone.now)

    class Meta:
        db_table = "GatewayBudgetLedger"
        constraints = [
            models.UniqueConstraint(fields=["budget", "gateway_request_id"], name="GatewayBudgetLedger_budgetId_gatewayRequestId_key"),
        ]
        indexes = [
            models.Index(fields=["virtual_key"]),
            models.Index(fields=["gateway_request_id"]),
            models.Index(fields=["occurred_at"]),
        ]


class GatewayChangeEvent(models.Model):
    revision = models.BigAutoField(primary_key=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="gateway_change_events", db_column="organizationId", db_index=False)
    kind = models.TextField(choices=GatewayChangeEventKind.choices)
    virtual_key_id = models.TextField(null=True, db_column="virtualKeyId")
    budget_id = models.TextField(null=True, db_column="budgetId")
    model_provider_id = models.TextField(null=True, db_column="modelProviderId")
    project = models.ForeignKey("Project", on_delete=models.SET_NULL, related_name="gateway_change_events", db_column="projectId", null=True, db_index=False)
    payload = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "GatewayChangeEvent"
        indexes = [
            models.Index(fields=["organization", "revision"]),
            models.Index(fields=["project", "revision"]),
            models.Index(fields=["virtual_key_id"]),
            models.Index(fields=["budget_id"]),
        ]


class GatewayCacheRule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    name = models.TextField()
    description = models.TextField(null=True)
    priority = models.IntegerField(default=100)
    enabled = models.BooleanField(default=True)
    matchers = models.JSONField()
    action = models.JSONField()
    mode_enum = models.TextField(choices=GatewayCacheRuleMode.choices, db_column="modeEnum")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    created_by_id = models.TextField(db_column="createdById")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayCacheRule"
        indexes = [
            models.Index(fields=["organization_id", "archived_at", "priority"]),
            models.Index(fields=["organization_id", "enabled", "priority"]),
        ]


class FeatureFlag(models.Model):
    key = models.TextField(primary_key=True, default=None)
    enabled = models.BooleanField()
    rules = models.JSONField(null=True)
    last_edited_by = models.TextField(null=True, db_column="lastEditedBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeatureFlag"


class FeatureFlagExperimentSetting(models.Model):
    pk = models.CompositePrimaryKey("flag_key", "subject_type", "subject_id")
    flag_key = models.TextField(db_column="flagKey")
    subject_type = models.TextField(choices=FeatureFlagExperimentSubjectType.choices, db_column="subjectType")
    subject_id = models.TextField(db_column="subjectId")
    enabled = models.BooleanField()
    changed_by_user_id = models.TextField(null=True, db_column="changedByUserId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeatureFlagExperimentSetting"
        indexes = [
            models.Index(fields=["subject_type", "subject_id"]),
        ]


class ProcessManagerInstance(models.Model):
    id = models.TextField(primary_key=True, default=None)
    process_name = models.TextField(db_column="processName")
    project_id = models.TextField(db_column="projectId")
    process_key = models.TextField(db_column="processKey")
    tenant_id = models.TextField(db_column="tenantId")
    user_id = models.TextField(null=True, db_column="userId")
    state = models.JSONField()
    revision = models.IntegerField()
    next_wake_at = models.DateTimeField(null=True, db_column="nextWakeAt")
    updated_at = models.DateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "ProcessManagerInstance"
        constraints = [
            models.UniqueConstraint(fields=["process_name", "project_id", "process_key"], name="ProcessManagerInstance_processName_projectId_processKey_key"),
        ]
        indexes = [
            models.Index(fields=["next_wake_at"]),
        ]


class ProcessManagerInbox(models.Model):
    id = models.TextField(primary_key=True, default=None)
    process_name = models.TextField(db_column="processName")
    project_id = models.TextField(db_column="projectId")
    process_key = models.TextField(db_column="processKey")
    tenant_id = models.TextField(db_column="tenantId")
    source_event_id = models.TextField(db_column="sourceEventId")
    source_event_key = models.TextField(null=True, db_column="sourceEventKey")
    consumed_at = models.DateTimeField(db_column="consumedAt")

    class Meta:
        db_table = "ProcessManagerInbox"
        constraints = [
            models.UniqueConstraint(fields=["process_name", "project_id", "source_event_key"], name="ProcessManagerInbox_processName_projectId_sourceEventKey_key"),
        ]
        indexes = [
            models.Index(fields=["process_name", "project_id", "process_key", "consumed_at"]),
            models.Index(fields=["consumed_at"]),
        ]


class ProcessManagerOutbox(models.Model):
    id = models.TextField(primary_key=True, default=None)
    process_name = models.TextField(db_column="processName")
    project_id = models.TextField(db_column="projectId")
    process_key = models.TextField(db_column="processKey")
    tenant_id = models.TextField(db_column="tenantId")
    user_id = models.TextField(null=True, db_column="userId")
    message_key = models.TextField(db_column="messageKey")
    intent_type = models.TextField(db_column="intentType")
    payload = models.JSONField()
    trace_carrier = models.JSONField(db_column="traceCarrier")
    source_event_id = models.TextField(null=True, db_column="sourceEventId")
    status = models.TextField(choices=ProcessManagerOutboxStatus.choices, default=ProcessManagerOutboxStatus.pending)
    attempts = models.IntegerField(default=0)
    next_attempt_at = models.DateTimeField(db_column="nextAttemptAt")
    leased_until = models.DateTimeField(null=True, db_column="leasedUntil")
    lease_token = models.TextField(null=True, unique=True, db_column="leaseToken")
    dispatched_at = models.DateTimeField(null=True, db_column="dispatchedAt")
    created_at = models.DateTimeField(db_column="createdAt")
    updated_at = models.DateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "ProcessManagerOutbox"
        constraints = [
            models.UniqueConstraint(fields=["process_name", "project_id", "message_key"], name="ProcessManagerOutbox_processName_projectId_messageKey_key"),
        ]
        indexes = [
            models.Index(fields=["process_name", "project_id", "process_key", "created_at"]),
            models.Index(fields=["status", "next_attempt_at", "leased_until"]),
            models.Index(fields=["status", "process_name", "next_attempt_at"]),
        ]


class ProcessManagerOutboxAttempt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    outbox = models.ForeignKey("ProcessManagerOutbox", on_delete=models.CASCADE, related_name="attempt_log", db_column="outboxId", db_index=False)
    project_id = models.TextField(db_column="projectId")
    attempt = models.IntegerField()
    occurred_at = models.DateTimeField(db_column="occurredAt")
    outcome = models.TextField(choices=ProcessManagerOutboxAttemptOutcome.choices)
    error_type = models.TextField(db_column="errorType")
    error_message = models.TextField(db_column="errorMessage")
    retry_after_ms = models.IntegerField(null=True, db_column="retryAfterMs")

    class Meta:
        db_table = "ProcessManagerOutboxAttempt"
        indexes = [
            models.Index(fields=["outbox", "attempt"]),
        ]


class TopicClusteringRunProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(unique=True, db_column="projectId")
    last_requested_at = models.FloatField(null=True, db_column="lastRequestedAt")
    last_request_trigger = models.TextField(null=True, db_column="lastRequestTrigger")
    last_run_at = models.FloatField(null=True, db_column="lastRunAt")
    last_run_outcome = models.TextField(null=True, db_column="lastRunOutcome")
    last_run_mode = models.TextField(null=True, db_column="lastRunMode")
    last_run_skipped_reason = models.TextField(null=True, db_column="lastRunSkippedReason")
    last_run_error = models.TextField(null=True, db_column="lastRunError")
    last_run_error_code = models.TextField(null=True, db_column="lastRunErrorCode")
    last_run_error_user_actionable = models.BooleanField(db_column="lastRunErrorUserActionable", default=False)
    last_run_traces_processed = models.IntegerField(db_column="lastRunTracesProcessed", default=0)
    last_run_topics_count = models.IntegerField(db_column="lastRunTopicsCount", default=0)
    last_run_subtopics_count = models.IntegerField(db_column="lastRunSubtopicsCount", default=0)
    last_run_pages = models.IntegerField(db_column="lastRunPages", default=0)
    in_progress_run_id = models.TextField(null=True, db_column="inProgressRunId")
    in_progress_traces = models.IntegerField(db_column="inProgressTraces", default=0)
    in_progress_pages = models.IntegerField(db_column="inProgressPages", default=0)
    in_progress_started_at = models.FloatField(null=True, db_column="inProgressStartedAt")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "TopicClusteringRunProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id"], name="TopicClusteringRunProjection_projectId_key"),
        ]


class TopicModelProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(unique=True, db_column="projectId")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "TopicModelProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id"], name="TopicModelProjection_projectId_key"),
        ]


class TopicClusteringRunHistoryProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(unique=True, db_column="projectId")
    runs = models.JSONField()
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "TopicClusteringRunHistoryProjection"
        constraints = [
            models.UniqueConstraint(fields=["project_id"], name="TopicClusteringRunHistoryProjection_projectId_key"),
        ]


class IngestionPullRunProjection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    source_id = models.TextField(unique=True, db_column="sourceId")
    project_id = models.TextField(db_column="projectId")
    enabled = models.BooleanField(default=False)
    cron = models.TextField(null=True)
    cursor = models.TextField(null=True)
    last_run_at = models.FloatField(null=True, db_column="lastRunAt")
    last_run_outcome = models.TextField(null=True, db_column="lastRunOutcome")
    last_run_event_count = models.IntegerField(db_column="lastRunEventCount", default=0)
    last_run_error = models.TextField(null=True, db_column="lastRunError")
    last_run_error_code = models.TextField(null=True, db_column="lastRunErrorCode")
    consecutive_errors = models.IntegerField(db_column="consecutiveErrors", default=0)
    last_success_at = models.FloatField(null=True, db_column="lastSuccessAt")
    last_run_scheduled_for = models.FloatField(null=True, db_column="lastRunScheduledFor")
    last_read_through_at = models.FloatField(null=True, db_column="lastReadThroughAt")
    last_run_completeness = models.TextField(null=True, db_column="lastRunCompleteness")
    last_agents_listing_at = models.FloatField(null=True, db_column="lastAgentsListingAt")
    last_agents_listing_outcome = models.TextField(null=True, db_column="lastAgentsListingOutcome")
    last_agents_listing_count = models.IntegerField(null=True, db_column="lastAgentsListingCount")
    last_agents_listing_reason = models.TextField(null=True, db_column="lastAgentsListingReason")
    last_agents_listing_status = models.IntegerField(null=True, db_column="lastAgentsListingStatus")
    last_people_listing_at = models.FloatField(null=True, db_column="lastPeopleListingAt")
    last_people_listing_outcome = models.TextField(null=True, db_column="lastPeopleListingOutcome")
    last_people_directory_count = models.IntegerField(null=True, db_column="lastPeopleDirectoryCount")
    last_people_withheld_count = models.IntegerField(null=True, db_column="lastPeopleWithheldCount")
    last_people_listing_reason = models.TextField(null=True, db_column="lastPeopleListingReason")
    last_people_listing_status = models.IntegerField(null=True, db_column="lastPeopleListingStatus")
    created_at = models.FloatField(db_column="createdAt")
    updated_at = models.FloatField(db_column="updatedAt")
    occurred_at = models.FloatField(db_column="occurredAt")
    accepted_at = models.FloatField(db_column="acceptedAt")
    last_event_id = models.TextField(db_column="lastEventId")
    projection_version = models.TextField(db_column="projectionVersion")

    class Meta:
        db_table = "IngestionPullRunProjection"
        indexes = [
            models.Index(fields=["project_id"]),
        ]


class ScheduledJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    target_type = models.TextField(db_column="targetType")
    target_id = models.TextField(db_column="targetId")
    cron = models.TextField()
    timezone_field = models.TextField(db_column="timezone")
    next_run_at = UtcDateTimeField(db_column="nextRunAt")
    last_slot = UtcDateTimeField(null=True, db_column="lastSlot")
    current_slot = UtcDateTimeField(null=True, db_column="currentSlot")
    attempts = models.IntegerField(default=0)
    last_error = models.TextField(null=True, db_column="lastError")
    active = models.BooleanField(default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ScheduledJob"
        constraints = [
            models.UniqueConstraint(fields=["target_type", "target_id"], name="ScheduledJob_targetType_targetId_key"),
        ]
        indexes = [
            models.Index(fields=["active", "next_run_at"]),
            models.Index(fields=["project_id"]),
        ]


class BugReport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    source = models.TextField()
    kind = models.TextField()
    title = models.TextField()
    summary = models.TextField(null=True)
    session_data = models.TextField(null=True, db_column="sessionData")
    session_truncated = models.BooleanField(db_column="sessionTruncated", default=False)
    agent = models.TextField(null=True)
    contact_email = models.TextField(null=True, db_column="contactEmail")
    cli_version = models.TextField(null=True, db_column="cliVersion")
    linked_project_id = models.TextField(null=True, db_column="linkedProjectId")
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "BugReport"
        indexes = [
            models.Index(fields=["created_at"]),
        ]


class IdempotencyReceipt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scope_id = models.TextField(db_column="scopeId")
    key = models.TextField()
    claim_id = models.TextField(db_column="claimId")
    request_fingerprint = models.TextField(db_column="requestFingerprint")
    response_status = models.IntegerField(null=True, db_column="responseStatus")
    response_body = models.TextField(null=True, db_column="responseBody")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    heartbeat_at = UtcDateTimeField(db_column="heartbeatAt")
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "IdempotencyReceipt"
        constraints = [
            models.UniqueConstraint(fields=["scope_id", "key"], name="IdempotencyReceipt_scopeId_key_key"),
        ]
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class GatewayRealtimeSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_id = models.TextField(db_column="projectId")
    organization_id = models.TextField(db_column="organizationId")
    virtual_key_id = models.TextField(db_column="virtualKeyId")
    model_provider_id = models.TextField(db_column="modelProviderId")
    vendor = models.TextField()
    agent_id = models.TextField(null=True, db_column="agentId")
    model = models.TextField()
    requested_model = models.TextField(null=True, db_column="requestedModel")
    vendor_conversation_id = models.TextField(null=True, db_column="vendorConversationId")
    status = models.TextField(choices=GatewayRealtimeSessionStatus.choices, default=GatewayRealtimeSessionStatus.OPEN)
    minted_at = UtcDateTimeField(db_column="mintedAt", default=timezone.now)
    closed_at = UtcDateTimeField(null=True, db_column="closedAt")
    close_reason = models.TextField(null=True, db_column="closeReason")
    trace_id = models.TextField(null=True, db_column="traceId")
    vendor_cost_raw = models.JSONField(null=True, db_column="vendorCostRaw")
    kind = models.TextField(null=True)
    metering = models.TextField(null=True)
    credential_expires_at = UtcDateTimeField(null=True, db_column="credentialExpiresAt")
    transcription_model = models.TextField(null=True, db_column="transcriptionModel")
    end_user_id = models.TextField(null=True, db_column="endUserId")
    last_report_at = UtcDateTimeField(null=True, db_column="lastReportAt")
    reported_cost_nano_usd = models.BigIntegerField(db_column="reportedCostNanoUsd", default=0)
    report_count = models.IntegerField(db_column="reportCount", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GatewayRealtimeSession"
        constraints = [
            models.UniqueConstraint(fields=["vendor", "vendor_conversation_id"], name="GatewayRealtimeSession_vendor_vendorConversationId_key"),
        ]
        indexes = [
            models.Index(fields=["virtual_key_id", "status", "minted_at"]),
            models.Index(fields=["project_id", "status", "minted_at"]),
            models.Index(fields=["status", "metering", "minted_at"]),
        ]


class GatewayRealtimeSessionReport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_id = models.TextField(db_column="sessionId")
    report_key = models.TextField(db_column="reportKey")
    project_id = models.TextField(db_column="projectId")
    model = models.TextField()
    usage = models.JSONField()
    cost_nano_usd = models.BigIntegerField(db_column="costNanoUsd")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "GatewayRealtimeSessionReport"
        constraints = [
            models.UniqueConstraint(fields=["session_id", "report_key"], name="GatewayRealtimeSessionReport_sessionId_reportKey_key"),
        ]
        indexes = [
            models.Index(fields=["project_id"]),
        ]


class SystemMigrationTenantState(models.Model):
    pk = models.CompositePrimaryKey("migration_name", "tenant_id")
    migration_name = models.TextField(db_column="migrationName")
    tenant_id = models.TextField(db_column="tenantId")
    status = models.TextField()
    report = models.JSONField(null=True)
    occurred_at = UtcDateTimeField(db_column="occurredAt", default=timezone.now)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SystemMigrationTenantState"
        indexes = [
            models.Index(fields=["migration_name", "status"]),
        ]


class SystemMigrationEnrollment(models.Model):
    pk = models.CompositePrimaryKey("organization_id", "migration_name")
    organization_id = models.TextField(db_column="organizationId")
    migration_name = models.TextField(db_column="migrationName")
    enrolled_by_user_id = models.TextField(db_column="enrolledByUserId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "SystemMigrationEnrollment"
        indexes = [
            models.Index(fields=["migration_name"]),
        ]


class StoredObject(models.Model):
    pk = models.CompositePrimaryKey("tenant_id", "id")
    tenant_id = models.TextField(db_column="tenantId")
    id = models.TextField()
    status = models.TextField()
    purpose = models.TextField()
    owner_kind = models.TextField(db_column="ownerKind")
    owner_id = models.TextField(db_column="ownerId")
    filename = models.TextField()
    sha256 = models.TextField()
    size_bytes = models.BigIntegerField(db_column="sizeBytes")
    media_type = models.TextField(db_column="mediaType")
    media_type_verified = models.BooleanField(db_column="mediaTypeVerified", default=False)
    storage_provider = models.TextField(null=True, db_column="storageProvider")
    storage_destination_id = models.TextField(null=True, db_column="storageDestinationId")
    storage_provider_relative_id = models.TextField(null=True, db_column="storageProviderRelativeId")
    generation = models.IntegerField(default=0)
    audiences = models.JSONField()
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    available_at = UtcDateTimeField(null=True, db_column="availableAt")
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    source = models.TextField()
    legacy_fingerprint = models.TextField(null=True, db_column="legacyFingerprint")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "StoredObject"
        indexes = [
            models.Index(fields=["tenant_id", "status", "id"]),
            models.Index(fields=["owner_kind", "owner_id", "status"]),
            models.Index(fields=["status", "expires_at"]),
        ]


class DiscoveredPerson(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    provider = models.TextField()
    raw_actor_id = models.TextField(db_column="rawActorId")
    display_text = models.TextField(db_column="displayText")
    kind = models.TextField()
    department = models.TextField(null=True)
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt")
    last_seen_at = UtcDateTimeField(db_column="lastSeenAt")
    erased_at = UtcDateTimeField(null=True, db_column="erasedAt")
    money_rows_pending_at = UtcDateTimeField(null=True, db_column="moneyRowsPendingAt")
    money_rebuild_since = models.TextField(null=True, db_column="moneyRebuildSince")
    suspended_at = UtcDateTimeField(null=True, db_column="suspendedAt")
    suspended_reason = models.TextField(null=True, db_column="suspendedReason")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DiscoveredPerson"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "provider", "raw_actor_id"], name="DiscoveredPerson_organizationId_provider_rawActorId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id", "last_seen_at"]),
        ]


class DiscoveredAgent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    provider = models.TextField()
    raw_agent_id = models.TextField(db_column="rawAgentId")
    display_text = models.TextField(db_column="displayText")
    metadata = models.JSONField()
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt")
    last_seen_at = UtcDateTimeField(db_column="lastSeenAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DiscoveredAgent"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "provider", "raw_agent_id"], name="DiscoveredAgent_organizationId_provider_rawAgentId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id", "last_seen_at"]),
        ]


class IdentityMatch(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    discovered_person_id = models.TextField(db_column="discoveredPersonId")
    user_id = models.TextField(null=True, db_column="userId")
    evidence_kind = models.TextField(db_column="evidenceKind")
    valid_from = UtcDateTimeField(db_column="validFrom")
    valid_to = UtcDateTimeField(null=True, db_column="validTo")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "IdentityMatch"
        indexes = [
            models.Index(fields=["organization_id", "discovered_person_id"]),
            models.Index(fields=["organization_id", "user_id"]),
        ]


class GovernanceTenantHistory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    tenant_id = models.TextField(db_column="tenantId")
    first_used_at = UtcDateTimeField(db_column="firstUsedAt")
    last_used_at = UtcDateTimeField(db_column="lastUsedAt")

    class Meta:
        db_table = "GovernanceTenantHistory"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "tenant_id"], name="GovernanceTenantHistory_organizationId_tenantId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class ErasedIdentifierSuppression(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    provider = models.TextField()
    identifier_hash = models.TextField(db_column="identifierHash")
    erased_at = UtcDateTimeField(db_column="erasedAt")

    class Meta:
        db_table = "ErasedIdentifierSuppression"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "provider", "identifier_hash"], name="ErasedIdentifierSuppression_organizationId_provider_identifierHash_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id"]),
        ]


class IdentityMatchSuggestion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization_id = models.TextField(db_column="organizationId")
    discovered_person_id = models.TextField(db_column="discoveredPersonId")
    user_id = models.TextField(db_column="userId")
    score = models.FloatField()
    computed_at = UtcDateTimeField(db_column="computedAt")

    class Meta:
        db_table = "IdentityMatchSuggestion"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "discovered_person_id", "user_id"], name="IdentityMatchSuggestion_organizationId_discoveredPersonId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["organization_id", "score"]),
        ]


class ConnectedSeatChange(models.Model):
    id = models.TextField(primary_key=True, default=None)
    license_id = models.TextField(unique=True, db_column="licenseId")
    account_id = models.TextField(null=True, db_column="accountId")
    organization_id = models.TextField(null=True, db_column="organizationId")
    changed_at = UtcDateTimeField(db_column="changedAt")
    added_seats = models.IntegerField(db_column="addedSeats")
    unit_amount_cents = models.IntegerField(db_column="unitAmountCents")
    amount_cents = models.IntegerField(db_column="amountCents")
    currency = models.TextField(choices=Currency.choices, null=True)
    stripe_invoice_id = models.TextField(null=True, db_column="stripeInvoiceId")
    state = models.TextField(choices=ConnectedSeatChangeState.choices)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ConnectedSeatChange"
        indexes = [
            models.Index(fields=["account_id"]),
            models.Index(fields=["organization_id"]),
            models.Index(fields=["state"]),
        ]


class ConnectedStatement(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account_id = models.TextField(db_column="accountId")
    month = UtcDateTimeField()
    sent_at = UtcDateTimeField(db_column="sentAt")

    class Meta:
        db_table = "ConnectedStatement"
        constraints = [
            models.UniqueConstraint(fields=["account_id", "month"], name="ConnectedStatement_accountId_month_key"),
        ]
        indexes = [
            models.Index(fields=["account_id"]),
        ]


class LicenseRenewalClaim(models.Model):
    stripe_invoice_id = models.TextField(primary_key=True, db_column="stripeInvoiceId", default=None)
    stripe_subscription_id = models.TextField(db_column="stripeSubscriptionId")
    claim_token = models.TextField(db_column="claimToken")
    claimed_at = UtcDateTimeField(db_column="claimedAt")
    issued_license = models.JSONField(null=True, db_column="issuedLicense")
    delivered_at = UtcDateTimeField(null=True, db_column="deliveredAt")
    license_id = models.TextField(null=True, db_column="licenseId")

    class Meta:
        db_table = "LicenseRenewalClaim"


class InstanceIdentity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    instance_id = models.TextField(unique=True, db_column="instanceId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_report_at = UtcDateTimeField(null=True, db_column="lastReportAt")
    last_report_error = models.TextField(null=True, db_column="lastReportError")
    optional_metrics_opt_out = models.BooleanField(db_column="optionalMetricsOptOut", default=False)
    hostname_opt_out = models.BooleanField(db_column="hostnameOptOut", default=False)
    upgrade_diagnostics_opt_out = models.BooleanField(db_column="upgradeDiagnosticsOptOut", default=False)

    class Meta:
        db_table = "InstanceIdentity"


class SelfHostedInstance(models.Model):
    id = models.TextField(primary_key=True, default=None)
    instance_id = models.TextField(unique=True, db_column="instanceId")
    first_seen_at = UtcDateTimeField(db_column="firstSeenAt", default=timezone.now)
    last_seen_at = UtcDateTimeField(db_column="lastSeenAt")
    version = models.TextField(null=True)
    install_method = models.TextField(null=True, db_column="installMethod")
    chart_version = models.TextField(null=True, db_column="chartVersion")
    hostname = models.TextField(null=True)
    environment = models.TextField(null=True)
    installed_at = UtcDateTimeField(null=True, db_column="installedAt")
    report_schema_version = models.IntegerField(null=True, db_column="reportSchemaVersion")
    organization_id = models.TextField(null=True, db_column="organizationId")
    issued_license_id = models.TextField(null=True, db_column="issuedLicenseId")
    user_email_domains = models.JSONField(null=True, db_column="userEmailDomains")
    user_domains = ArrayField(models.TextField(), db_column="userDomains", default=list)
    latest_report = models.JSONField(null=True, db_column="latestReport")
    optional_metrics_reported = models.BooleanField(db_column="optionalMetricsReported", default=True)
    hostname_reported = models.BooleanField(db_column="hostnameReported", default=True)
    report_count = models.IntegerField(db_column="reportCount", default=0)
    last_unknown_fields = models.IntegerField(db_column="lastUnknownFields", default=0)
    raised_signals = ArrayField(models.TextField(), db_column="raisedSignals", default=list)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SelfHostedInstance"
        indexes = [
            models.Index(fields=["last_seen_at"]),
            models.Index(fields=["organization_id"]),
            models.Index(fields=["issued_license_id"]),
        ]


class SelfHostedInstanceDiagnostic(models.Model):
    id = models.TextField(primary_key=True, default=None)
    instance_id = models.TextField(db_column="instanceId")
    step_id = models.TextField(db_column="stepId")
    step_description = models.TextField(null=True, db_column="stepDescription")
    release = models.TextField(null=True)
    attempt = models.IntegerField()
    trigger = models.TextField()
    byte_length = models.IntegerField(db_column="byteLength")
    object_key = models.TextField(db_column="objectKey")
    received_at = UtcDateTimeField(db_column="receivedAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    slack_announced_at = UtcDateTimeField(null=True, db_column="slackAnnouncedAt")

    class Meta:
        db_table = "SelfHostedInstanceDiagnostic"
        indexes = [
            models.Index(fields=["instance_id", "received_at"]),
        ]


class SelfHostedNotice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    severity = models.TextField()
    title = models.TextField()
    body = models.TextField()
    url = models.TextField(null=True)
    fixed_in = models.TextField(null=True, db_column="fixedIn")
    status = models.TextField()
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    published_at = UtcDateTimeField(null=True, db_column="publishedAt")
    published_by = models.TextField(null=True, db_column="publishedBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SelfHostedNotice"


class SelfHostedInstanceReport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    instance_id = models.TextField(db_column="instanceId")
    received_at = UtcDateTimeField(db_column="receivedAt", default=timezone.now)
    version = models.TextField(null=True)
    report_schema_version = models.IntegerField(null=True, db_column="reportSchemaVersion")
    unknown_fields = models.IntegerField(db_column="unknownFields", default=0)
    payload = models.JSONField()

    class Meta:
        db_table = "SelfHostedInstanceReport"
        indexes = [
            models.Index(fields=["instance_id", "received_at"]),
            models.Index(fields=["received_at"]),
        ]


class WebPushSubscription(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField(db_column="userId")
    endpoint = models.TextField(unique=True)
    p256dh = models.TextField()
    auth = models.TextField()
    user_agent = models.TextField(null=True, db_column="userAgent")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_success_at = UtcDateTimeField(null=True, db_column="lastSuccessAt")

    class Meta:
        db_table = "WebPushSubscription"
        indexes = [
            models.Index(fields=["user_id"]),
        ]


class WebPushVapidKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    public_key = models.TextField(db_column="publicKey")
    private_key_encrypted = models.TextField(db_column="privateKeyEncrypted")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "WebPushVapidKey"


class SignInProviderMount(models.Model):
    id = models.TextField(primary_key=True, default=None)
    provider = models.TextField(null=True)
    mounted = models.BooleanField()
    last_applied_event_id = models.TextField(db_column="lastAppliedEventId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SignInProviderMount"
