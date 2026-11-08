from datetime import datetime
from datetime import timezone as dt_timezone
from decimal import Decimal
from typing import Any, TypeVar

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


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def user_roles_default() -> list[str]:
    return [Role.USER]


class IdentityProvider(models.TextChoices):
    DOCUMENSO = "DOCUMENSO"
    GOOGLE = "GOOGLE"
    OIDC = "OIDC"


class Role(models.TextChoices):
    ADMIN = "ADMIN"
    USER = "USER"


class UserSecurityAuditLogType(models.TextChoices):
    ACCOUNT_PROFILE_UPDATE = "ACCOUNT_PROFILE_UPDATE"
    ACCOUNT_SSO_LINK = "ACCOUNT_SSO_LINK"
    ACCOUNT_SSO_UNLINK = "ACCOUNT_SSO_UNLINK"
    ORGANISATION_SSO_LINK = "ORGANISATION_SSO_LINK"
    ORGANISATION_SSO_UNLINK = "ORGANISATION_SSO_UNLINK"
    AUTH_2FA_DISABLE = "AUTH_2FA_DISABLE"
    AUTH_2FA_ENABLE = "AUTH_2FA_ENABLE"
    PASSKEY_CREATED = "PASSKEY_CREATED"
    PASSKEY_DELETED = "PASSKEY_DELETED"
    PASSKEY_UPDATED = "PASSKEY_UPDATED"
    PASSWORD_RESET = "PASSWORD_RESET"
    PASSWORD_UPDATE = "PASSWORD_UPDATE"
    SESSION_REVOKED = "SESSION_REVOKED"
    SIGN_OUT = "SIGN_OUT"
    SIGN_IN = "SIGN_IN"
    SIGN_IN_FAIL = "SIGN_IN_FAIL"
    SIGN_IN_2FA_FAIL = "SIGN_IN_2FA_FAIL"
    SIGN_IN_PASSKEY_FAIL = "SIGN_IN_PASSKEY_FAIL"


class WebhookTriggerEvents(models.TextChoices):
    DOCUMENT_CREATED = "DOCUMENT_CREATED"
    DOCUMENT_SENT = "DOCUMENT_SENT"
    DOCUMENT_OPENED = "DOCUMENT_OPENED"
    DOCUMENT_SIGNED = "DOCUMENT_SIGNED"
    DOCUMENT_COMPLETED = "DOCUMENT_COMPLETED"
    DOCUMENT_REJECTED = "DOCUMENT_REJECTED"
    DOCUMENT_CANCELLED = "DOCUMENT_CANCELLED"
    RECIPIENT_EXPIRED = "RECIPIENT_EXPIRED"
    DOCUMENT_RECIPIENT_COMPLETED = "DOCUMENT_RECIPIENT_COMPLETED"
    DOCUMENT_REMINDER_SENT = "DOCUMENT_REMINDER_SENT"
    TEMPLATE_CREATED = "TEMPLATE_CREATED"
    TEMPLATE_UPDATED = "TEMPLATE_UPDATED"
    TEMPLATE_DELETED = "TEMPLATE_DELETED"
    TEMPLATE_USED = "TEMPLATE_USED"


class WebhookCallStatus(models.TextChoices):
    SUCCESS = "SUCCESS"
    FAILED = "FAILED"


class ApiTokenAlgorithm(models.TextChoices):
    SHA512 = "SHA512"


class SubscriptionStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    PAST_DUE = "PAST_DUE"
    INACTIVE = "INACTIVE"


class DocumentStatus(models.TextChoices):
    DRAFT = "DRAFT"
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"
    REJECTED = "REJECTED"
    CANCELLED = "CANCELLED"


class DocumentSource(models.TextChoices):
    DOCUMENT = "DOCUMENT"
    TEMPLATE = "TEMPLATE"
    TEMPLATE_DIRECT_LINK = "TEMPLATE_DIRECT_LINK"


class DocumentVisibility(models.TextChoices):
    EVERYONE = "EVERYONE"
    MANAGER_AND_ABOVE = "MANAGER_AND_ABOVE"
    ADMIN = "ADMIN"


class FolderType(models.TextChoices):
    DOCUMENT = "DOCUMENT"
    TEMPLATE = "TEMPLATE"


class EnvelopeType(models.TextChoices):
    DOCUMENT = "DOCUMENT"
    TEMPLATE = "TEMPLATE"


class DocumentDataType(models.TextChoices):
    S3_PATH = "S3_PATH"
    BYTES = "BYTES"
    BYTES_64 = "BYTES_64"


class DocumentSigningOrder(models.TextChoices):
    PARALLEL = "PARALLEL"
    SEQUENTIAL = "SEQUENTIAL"


class DocumentDistributionMethod(models.TextChoices):
    EMAIL = "EMAIL"
    NONE = "NONE"


class ReadStatus(models.TextChoices):
    NOT_OPENED = "NOT_OPENED"
    OPENED = "OPENED"


class SendStatus(models.TextChoices):
    NOT_SENT = "NOT_SENT"
    SENT = "SENT"


class SigningStatus(models.TextChoices):
    NOT_SIGNED = "NOT_SIGNED"
    SIGNED = "SIGNED"
    REJECTED = "REJECTED"


class RecipientRole(models.TextChoices):
    CC = "CC"
    SIGNER = "SIGNER"
    VIEWER = "VIEWER"
    APPROVER = "APPROVER"
    ASSISTANT = "ASSISTANT"


class FieldType(models.TextChoices):
    SIGNATURE = "SIGNATURE"
    FREE_SIGNATURE = "FREE_SIGNATURE"
    INITIALS = "INITIALS"
    NAME = "NAME"
    EMAIL = "EMAIL"
    DATE = "DATE"
    TEXT = "TEXT"
    NUMBER = "NUMBER"
    RADIO = "RADIO"
    CHECKBOX = "CHECKBOX"
    DROPDOWN = "DROPDOWN"


class OrganisationType(models.TextChoices):
    PERSONAL = "PERSONAL"
    ORGANISATION = "ORGANISATION"


class OrganisationGroupType(models.TextChoices):
    INTERNAL_ORGANISATION = "INTERNAL_ORGANISATION"
    INTERNAL_TEAM = "INTERNAL_TEAM"
    CUSTOM = "CUSTOM"


class OrganisationMemberRole(models.TextChoices):
    ADMIN = "ADMIN"
    MANAGER = "MANAGER"
    MEMBER = "MEMBER"


class TeamMemberRole(models.TextChoices):
    ADMIN = "ADMIN"
    MANAGER = "MANAGER"
    MEMBER = "MEMBER"


class OrganisationMemberInviteStatus(models.TextChoices):
    ACCEPTED = "ACCEPTED"
    PENDING = "PENDING"
    DECLINED = "DECLINED"


class TemplateType(models.TextChoices):
    PUBLIC = "PUBLIC"
    PRIVATE = "PRIVATE"
    ORGANISATION = "ORGANISATION"


class BackgroundJobStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"


class BackgroundJobTaskStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"


class EmailDomainStatus(models.TextChoices):
    PENDING = "PENDING"
    ACTIVE = "ACTIVE"


class EmailTransportType(models.TextChoices):
    SMTP_AUTH = "SMTP_AUTH"
    SMTP_API = "SMTP_API"
    RESEND = "RESEND"
    MAILCHANNELS = "MAILCHANNELS"


class User(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(null=True)
    email = models.TextField(unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    password = models.TextField(null=True)
    source = models.TextField(null=True)
    signature = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_signed_in = UtcDateTimeField(db_column="lastSignedIn", default=timezone.now)
    roles = ArrayField(models.TextField(choices=Role.choices), default=user_roles_default)
    identity_provider = models.TextField(choices=IdentityProvider.choices, db_column="identityProvider", default=IdentityProvider.DOCUMENSO)
    avatar_image = models.ForeignKey("AvatarImage", on_delete=models.SET_NULL, related_name="user", db_column="avatarImageId", null=True, db_index=False)
    disabled = models.BooleanField(default=False)
    two_factor_secret = models.TextField(null=True, db_column="twoFactorSecret")
    two_factor_enabled = models.BooleanField(db_column="twoFactorEnabled", default=False)
    two_factor_backup_codes = models.TextField(null=True, db_column="twoFactorBackupCodes")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"
        indexes = [
            models.Index(fields=["email"]),
        ]


class TeamProfile(models.Model):
    id = models.TextField(primary_key=True, default=None)
    enabled = models.BooleanField(default=False)
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="profile", db_column="teamId")
    bio = models.TextField(null=True)

    class Meta:
        db_table = "TeamProfile"


class UserSecurityAuditLog(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="security_audit_logs", db_column="userId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    type = models.TextField(choices=UserSecurityAuditLogType.choices)
    user_agent = models.TextField(null=True, db_column="userAgent")
    ip_address = models.TextField(null=True, db_column="ipAddress")

    class Meta:
        db_table = "UserSecurityAuditLog"


class PasswordResetToken(models.Model):
    id = models.AutoField(primary_key=True)
    token = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expiry = UtcDateTimeField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="password_reset_tokens", db_column="userId", db_index=False)

    class Meta:
        db_table = "PasswordResetToken"


class Passkey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="passkeys", db_column="userId", db_index=False)
    name = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", default=timezone.now)
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    credential_id = models.BinaryField(db_column="credentialId")
    credential_public_key = models.BinaryField(db_column="credentialPublicKey")
    counter = models.BigIntegerField()
    credential_device_type = models.TextField(db_column="credentialDeviceType")
    credential_backed_up = models.BooleanField(db_column="credentialBackedUp")
    transports = ArrayField(models.TextField())

    class Meta:
        db_table = "Passkey"


class AnonymousVerificationToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AnonymousVerificationToken"


class VerificationToken(models.Model):
    id = models.AutoField(primary_key=True)
    secondary_id = models.TextField(unique=True, db_column="secondaryId")
    identifier = models.TextField()
    token = models.TextField(unique=True)
    completed = models.BooleanField(default=False)
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    metadata = models.JSONField(null=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="verification_tokens", db_column="userId", db_index=False)

    class Meta:
        db_table = "VerificationToken"


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    webhook_url = models.TextField(db_column="webhookUrl")
    event_triggers = ArrayField(models.TextField(choices=WebhookTriggerEvents.choices), db_column="eventTriggers")
    secret = models.TextField(null=True)
    enabled = models.BooleanField(default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="webhooks", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="webhooks", db_column="teamId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhook"


class WebhookCall(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField(choices=WebhookCallStatus.choices)
    url = models.TextField()
    event = models.TextField(choices=WebhookTriggerEvents.choices)
    request_body = models.JSONField(db_column="requestBody")
    response_code = models.IntegerField(db_column="responseCode")
    response_headers = models.JSONField(null=True, db_column="responseHeaders")
    response_body = models.JSONField(null=True, db_column="responseBody")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    webhook = models.ForeignKey("Webhook", on_delete=models.CASCADE, related_name="webhook_calls", db_column="webhookId", db_index=False)

    class Meta:
        db_table = "WebhookCall"


class ApiToken(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    token = models.TextField(unique=True)
    algorithm = models.TextField(choices=ApiTokenAlgorithm.choices, default=ApiTokenAlgorithm.SHA512)
    expires = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_tokens", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="api_tokens", db_column="teamId", db_index=False)

    class Meta:
        db_table = "ApiToken"


class Subscription(models.Model):
    id = models.AutoField(primary_key=True)
    status = models.TextField(choices=SubscriptionStatus.choices, default=SubscriptionStatus.INACTIVE)
    plan_id = models.TextField(unique=True, db_column="planId")
    price_id = models.TextField(db_column="priceId")
    period_end = UtcDateTimeField(null=True, db_column="periodEnd")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    cancel_at_period_end = models.BooleanField(db_column="cancelAtPeriodEnd", default=False)
    customer_id = models.TextField(db_column="customerId")
    organisation = models.OneToOneField("Organisation", on_delete=models.CASCADE, related_name="subscription", db_column="organisationId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Subscription"
        indexes = [
            models.Index(fields=["organisation"]),
        ]


class SubscriptionClaim(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    locked = models.BooleanField(default=False)
    team_count = models.IntegerField(db_column="teamCount")
    member_count = models.IntegerField(db_column="memberCount")
    envelope_item_count = models.IntegerField(db_column="envelopeItemCount")
    recipient_count = models.IntegerField(db_column="recipientCount")
    envelope_content_count = models.IntegerField(db_column="envelopeContentCount")
    envelope_content_image_count = models.IntegerField(db_column="envelopeContentImageCount")
    flags = models.JSONField()
    document_rate_limits = models.JSONField(db_column="documentRateLimits")
    document_quota = models.IntegerField(null=True, db_column="documentQuota")
    email_rate_limits = models.JSONField(db_column="emailRateLimits")
    email_quota = models.IntegerField(null=True, db_column="emailQuota")
    api_rate_limits = models.JSONField(db_column="apiRateLimits")
    api_quota = models.IntegerField(null=True, db_column="apiQuota")
    email_transport = models.ForeignKey("EmailTransport", on_delete=models.SET_NULL, related_name="subscription_claims", db_column="emailTransportId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SubscriptionClaim"


class OrganisationClaim(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    original_subscription_claim_id = models.TextField(null=True, db_column="originalSubscriptionClaimId")
    team_count = models.IntegerField(db_column="teamCount")
    member_count = models.IntegerField(db_column="memberCount")
    envelope_item_count = models.IntegerField(db_column="envelopeItemCount")
    recipient_count = models.IntegerField(db_column="recipientCount")
    envelope_content_count = models.IntegerField(db_column="envelopeContentCount")
    envelope_content_image_count = models.IntegerField(db_column="envelopeContentImageCount")
    flags = models.JSONField()
    document_rate_limits = models.JSONField(db_column="documentRateLimits")
    document_quota = models.IntegerField(null=True, db_column="documentQuota")
    email_rate_limits = models.JSONField(db_column="emailRateLimits")
    email_quota = models.IntegerField(null=True, db_column="emailQuota")
    api_rate_limits = models.JSONField(db_column="apiRateLimits")
    api_quota = models.IntegerField(null=True, db_column="apiQuota")
    email_transport = models.ForeignKey("EmailTransport", on_delete=models.SET_NULL, related_name="organisation_claims", db_column="emailTransportId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganisationClaim"


class OrganisationMonthlyStat(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="monthly_stats", db_column="organisationId", db_index=False)
    period = models.TextField()
    document_count = models.IntegerField(db_column="documentCount", default=0)
    email_count = models.IntegerField(db_column="emailCount", default=0)
    api_count = models.IntegerField(db_column="apiCount", default=0)
    email_reports = models.IntegerField(db_column="emailReports", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganisationMonthlyStat"
        constraints = [
            models.UniqueConstraint(fields=["organisation", "period"], name="OrganisationMonthlyStat_organisationId_period_key"),
        ]
        indexes = [
            models.Index(fields=["organisation"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    created_at_2 = models.IntegerField(null=True, db_column="created_at")
    ext_expires_in = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    password = models.TextField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    is_embed = models.BooleanField(db_column="isEmbed", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["session_token"]),
        ]


class AuthHandoff(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    payload = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "AuthHandoff"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class Folder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="folders", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="folders", db_column="teamId", db_index=False)
    pinned = models.BooleanField(default=False)
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="subfolders", db_column="parentId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    visibility = models.TextField(choices=DocumentVisibility.choices, default=DocumentVisibility.EVERYONE)
    type = models.TextField(choices=FolderType.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Folder"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["parent"]),
            models.Index(fields=["type"]),
        ]


class Envelope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    secondary_id = models.TextField(unique=True, db_column="secondaryId")
    external_id = models.TextField(null=True, db_column="externalId")
    type = models.TextField(choices=EnvelopeType.choices)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    title = models.TextField()
    status = models.TextField(choices=DocumentStatus.choices, default=DocumentStatus.DRAFT)
    source = models.TextField(choices=DocumentSource.choices)
    qr_token = models.TextField(null=True, db_column="qrToken")
    signature_level = models.TextField(db_column="signatureLevel")
    internal_version = models.IntegerField(db_column="internalVersion")
    use_legacy_field_insertion = models.BooleanField(db_column="useLegacyFieldInsertion", default=False)
    auth_options = models.JSONField(null=True, db_column="authOptions")
    form_values = models.JSONField(null=True, db_column="formValues")
    visibility = models.TextField(choices=DocumentVisibility.choices, default=DocumentVisibility.EVERYONE)
    template_type = models.TextField(choices=TemplateType.choices, db_column="templateType", default=TemplateType.PRIVATE)
    public_title = models.TextField(db_column="publicTitle", default="")
    public_description = models.TextField(db_column="publicDescription", default="")
    template_id = models.IntegerField(null=True, db_column="templateId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="envelopes", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="envelopes", db_column="teamId", db_index=False)
    folder = models.ForeignKey("Folder", on_delete=models.SET_NULL, related_name="envelopes", db_column="folderId", null=True, db_index=False)
    document_meta = models.OneToOneField("DocumentMeta", on_delete=models.RESTRICT, related_name="envelope", db_column="documentMetaId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Envelope"
        indexes = [
            models.Index(fields=["type"]),
            models.Index(fields=["status"]),
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["folder"]),
            models.Index(fields=["created_at"]),
        ]


class EnvelopeItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField()
    order = models.IntegerField()
    document_data = models.OneToOneField("DocumentData", on_delete=models.CASCADE, related_name="envelope_item", db_column="documentDataId")
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="envelope_items", db_column="envelopeId", db_index=False)

    class Meta:
        db_table = "EnvelopeItem"
        constraints = [
            models.UniqueConstraint(fields=["document_data"], name="EnvelopeItem_documentDataId_key"),
        ]
        indexes = [
            models.Index(fields=["envelope"]),
        ]


class DocumentAuditLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    envelope = models.ForeignKey("Envelope", on_delete=models.SET_NULL, related_name="audit_logs", db_column="envelopeId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    type = models.TextField()
    data = models.JSONField()
    name = models.TextField(null=True)
    email = models.TextField(null=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    user_agent = models.TextField(null=True, db_column="userAgent")
    ip_address = models.TextField(null=True, db_column="ipAddress")

    class Meta:
        db_table = "DocumentAuditLog"
        indexes = [
            models.Index(fields=["envelope"]),
        ]


class DocumentData(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=DocumentDataType.choices)
    data = models.TextField()
    initial_data = models.TextField(db_column="initialData")

    class Meta:
        db_table = "DocumentData"


class DocumentMeta(models.Model):
    id = models.TextField(primary_key=True, default=None)
    subject = models.TextField(null=True)
    message = models.TextField(null=True)
    timezone = models.TextField(null=True, default="Etc/UTC")
    date_format = models.TextField(null=True, db_column="dateFormat", default="yyyy-MM-dd hh:mm a")
    redirect_url = models.TextField(null=True, db_column="redirectUrl")
    signing_order = models.TextField(choices=DocumentSigningOrder.choices, db_column="signingOrder", default=DocumentSigningOrder.PARALLEL)
    allow_dictate_next_signer = models.BooleanField(db_column="allowDictateNextSigner", default=False)
    typed_signature_enabled = models.BooleanField(db_column="typedSignatureEnabled", default=True)
    upload_signature_enabled = models.BooleanField(db_column="uploadSignatureEnabled", default=True)
    draw_signature_enabled = models.BooleanField(db_column="drawSignatureEnabled", default=True)
    language = models.TextField(default="en")
    distribution_method = models.TextField(choices=DocumentDistributionMethod.choices, db_column="distributionMethod", default=DocumentDistributionMethod.EMAIL)
    email_settings = models.JSONField(null=True, db_column="emailSettings")
    email_reply_to = models.TextField(null=True, db_column="emailReplyTo")
    email_id = models.TextField(null=True, db_column="emailId")
    envelope_expiration_period = models.JSONField(null=True, db_column="envelopeExpirationPeriod")
    reminder_settings = models.JSONField(null=True, db_column="reminderSettings")

    class Meta:
        db_table = "DocumentMeta"


class EnvelopeAttachment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    label = models.TextField()
    data = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="envelope_attachments", db_column="envelopeId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EnvelopeAttachment"
        indexes = [
            models.Index(fields=["envelope"]),
        ]


class Recipient(models.Model):
    id = models.AutoField(primary_key=True)
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="recipients", db_column="envelopeId", db_index=False)
    email = models.CharField(max_length=255)
    name = models.CharField(max_length=255, default="")
    token = models.TextField()
    document_deleted_at = UtcDateTimeField(null=True, db_column="documentDeletedAt")
    expired = UtcDateTimeField(null=True)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    expiration_notified_at = UtcDateTimeField(null=True, db_column="expirationNotifiedAt")
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")
    signed_at = UtcDateTimeField(null=True, db_column="signedAt")
    last_reminder_sent_at = UtcDateTimeField(null=True, db_column="lastReminderSentAt")
    next_reminder_at = UtcDateTimeField(null=True, db_column="nextReminderAt")
    reminder_count = models.IntegerField(db_column="reminderCount", default=0)
    auth_options = models.JSONField(null=True, db_column="authOptions")
    signing_order = models.IntegerField(null=True, db_column="signingOrder")
    rejection_reason = models.TextField(null=True, db_column="rejectionReason")
    role = models.TextField(choices=RecipientRole.choices, default=RecipientRole.SIGNER)
    read_status = models.TextField(choices=ReadStatus.choices, db_column="readStatus", default=ReadStatus.NOT_OPENED)
    signing_status = models.TextField(choices=SigningStatus.choices, db_column="signingStatus", default=SigningStatus.NOT_SIGNED)
    send_status = models.TextField(choices=SendStatus.choices, db_column="sendStatus", default=SendStatus.NOT_SENT)

    class Meta:
        db_table = "Recipient"
        indexes = [
            models.Index(fields=["token"]),
            models.Index(fields=["email"]),
            models.Index(fields=["envelope"]),
            models.Index(fields=["signed_at"]),
            models.Index(fields=["expires_at"]),
            models.Index(fields=["email", "document_deleted_at", "envelope"]),
            models.Index(fields=["email", "envelope"], name="Recipient_email_envelopeId_idx"),
            models.Index(fields=["email", "signing_status", "envelope", "role"]),
            models.Index(fields=["next_reminder_at"]),
            GinIndex(fields=["email"], name="Recipient_email_trgm_idx"),
            GinIndex(fields=["name"], name="Recipient_name_trgm_idx"),
        ]


class Field(models.Model):
    id = models.AutoField(primary_key=True)
    secondary_id = models.TextField(unique=True, db_column="secondaryId")
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="fields", db_column="envelopeId", db_index=False)
    envelope_item = models.ForeignKey("EnvelopeItem", on_delete=models.CASCADE, related_name="field", db_column="envelopeItemId", db_index=False)
    recipient = models.ForeignKey("Recipient", on_delete=models.CASCADE, related_name="fields", db_column="recipientId", db_index=False)
    type = models.TextField(choices=FieldType.choices)
    page = models.IntegerField()
    position_x = models.DecimalField(max_digits=65, decimal_places=30, db_column="positionX", default=Decimal("0"))
    position_y = models.DecimalField(max_digits=65, decimal_places=30, db_column="positionY", default=Decimal("0"))
    width = models.DecimalField(max_digits=65, decimal_places=30, default=Decimal("-1"))
    height = models.DecimalField(max_digits=65, decimal_places=30, default=Decimal("-1"))
    custom_text = models.TextField(db_column="customText")
    inserted = models.BooleanField()
    field_meta = models.JSONField(null=True, db_column="fieldMeta")

    class Meta:
        db_table = "Field"
        indexes = [
            models.Index(fields=["envelope"]),
            models.Index(fields=["envelope_item"]),
            models.Index(fields=["recipient"]),
        ]


class EnvelopeContent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="contents", db_column="envelopeId", db_index=False)
    envelope_item = models.ForeignKey("EnvelopeItem", on_delete=models.CASCADE, related_name="contents", db_column="envelopeItemId", db_index=False)
    content_meta = models.JSONField(db_column="contentMeta")
    data_content = models.ForeignKey("DataContent", on_delete=models.SET_NULL, related_name="envelope_contents", db_column="dataContentId", null=True, db_index=False)

    class Meta:
        db_table = "EnvelopeContent"
        indexes = [
            models.Index(fields=["envelope"]),
            models.Index(fields=["envelope_item"]),
        ]


class DataContent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=DocumentDataType.choices)
    data = models.TextField()
    metadata = models.JSONField()

    class Meta:
        db_table = "DataContent"


class Signature(models.Model):
    id = models.AutoField(primary_key=True)
    created = UtcDateTimeField(default=timezone.now)
    recipient = models.ForeignKey("Recipient", on_delete=models.CASCADE, related_name="signatures", db_column="recipientId", db_index=False)
    field = models.OneToOneField("Field", on_delete=models.CASCADE, related_name="signature", db_column="fieldId")
    signature_image_as_base64 = models.TextField(null=True, db_column="signatureImageAsBase64")
    typed_signature = models.TextField(null=True, db_column="typedSignature")

    class Meta:
        db_table = "Signature"
        indexes = [
            models.Index(fields=["recipient"]),
        ]


class CscCredential(models.Model):
    id = models.TextField(primary_key=True, default=None)
    provider_id = models.TextField(db_column="providerId")
    credential_id = models.TextField(db_column="credentialId")
    cert_cache = models.BinaryField(null=True, db_column="certCache")
    signature_algorithm = models.TextField(db_column="signatureAlgorithm")
    key_type = models.TextField(db_column="keyType")
    digest_algorithm = models.TextField(db_column="digestAlgorithm")
    key_len_bits = models.IntegerField(null=True, db_column="keyLenBits")
    sign_algo_params = models.TextField(null=True, db_column="signAlgoParams")
    service_token_ciphertext = models.BinaryField(null=True, db_column="serviceTokenCiphertext")
    service_token_expires_at = UtcDateTimeField(null=True, db_column="serviceTokenExpiresAt")
    recipient = models.OneToOneField("Recipient", on_delete=models.CASCADE, related_name="csc_credential", db_column="recipientId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CscCredential"


class CscSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    envelope_id = models.TextField(db_column="envelopeId")
    signing_time = UtcDateTimeField(db_column="signingTime")
    items_json = models.JSONField(db_column="itemsJson")
    encrypted_sad = models.BinaryField(null=True, db_column="encryptedSad")
    sad_expires_at = UtcDateTimeField(null=True, db_column="sadExpiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    recipient = models.OneToOneField("Recipient", on_delete=models.CASCADE, related_name="csc_session", db_column="recipientId")

    class Meta:
        db_table = "CscSession"


class DocumentShareLink(models.Model):
    id = models.AutoField(primary_key=True)
    email = models.TextField()
    slug = models.TextField(unique=True)
    envelope = models.ForeignKey("Envelope", on_delete=models.CASCADE, related_name="share_links", db_column="envelopeId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentShareLink"
        constraints = [
            models.UniqueConstraint(fields=["envelope", "email"], name="DocumentShareLink_envelopeId_email_key"),
        ]


class Organisation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(choices=OrganisationType.choices)
    name = models.TextField()
    url = models.TextField(unique=True)
    avatar_image = models.ForeignKey("AvatarImage", on_delete=models.SET_NULL, related_name="organisation", db_column="avatarImageId", null=True, db_index=False)
    customer_id = models.TextField(null=True, unique=True, db_column="customerId")
    organisation_claim = models.OneToOneField("OrganisationClaim", on_delete=models.RESTRICT, related_name="organisation", db_column="organisationClaimId")
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="owned_organisations", db_column="ownerUserId", db_index=False)
    organisation_global_settings = models.OneToOneField("OrganisationGlobalSettings", on_delete=models.RESTRICT, related_name="organisation", db_column="organisationGlobalSettingsId")
    organisation_authentication_portal = models.OneToOneField("OrganisationAuthenticationPortal", on_delete=models.RESTRICT, related_name="organisation", db_column="organisationAuthenticationPortalId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organisation"
        indexes = [
            models.Index(fields=["name"]),
            models.Index(fields=["owner"]),
        ]


class OrganisationMember(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="organisation_member", db_column="userId", db_index=False)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="members", db_column="organisationId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganisationMember"
        constraints = [
            models.UniqueConstraint(fields=["user", "organisation"], name="OrganisationMember_userId_organisationId_key"),
        ]
        indexes = [
            models.Index(fields=["organisation"]),
        ]


class OrganisationMemberInvite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    email = models.TextField()
    token = models.TextField(unique=True)
    status = models.TextField(choices=OrganisationMemberInviteStatus.choices, default=OrganisationMemberInviteStatus.PENDING)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="invites", db_column="organisationId", db_index=False)
    organisation_role = models.TextField(choices=OrganisationMemberRole.choices, db_column="organisationRole")

    class Meta:
        db_table = "OrganisationMemberInvite"


class OrganisationGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    type = models.TextField(choices=OrganisationGroupType.choices)
    organisation_role = models.TextField(choices=OrganisationMemberRole.choices, db_column="organisationRole")
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="groups", db_column="organisationId", db_index=False)

    class Meta:
        db_table = "OrganisationGroup"
        indexes = [
            models.Index(fields=["organisation"]),
        ]


class OrganisationGroupMember(models.Model):
    id = models.TextField(primary_key=True, default=None)
    group = models.ForeignKey("OrganisationGroup", on_delete=models.CASCADE, related_name="organisation_group_members", db_column="groupId", db_index=False)
    organisation_member = models.ForeignKey("OrganisationMember", on_delete=models.CASCADE, related_name="organisation_group_members", db_column="organisationMemberId", db_index=False)

    class Meta:
        db_table = "OrganisationGroupMember"
        constraints = [
            models.UniqueConstraint(fields=["organisation_member", "group"], name="OrganisationGroupMember_organisationMemberId_groupId_key"),
        ]
        indexes = [
            models.Index(fields=["group"]),
            models.Index(fields=["organisation_member"]),
        ]


class TeamGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organisation_group = models.ForeignKey("OrganisationGroup", on_delete=models.CASCADE, related_name="team_groups", db_column="organisationGroupId", db_index=False)
    team_role = models.TextField(choices=TeamMemberRole.choices, db_column="teamRole")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="team_groups", db_column="teamId", db_index=False)

    class Meta:
        db_table = "TeamGroup"
        constraints = [
            models.UniqueConstraint(fields=["team", "organisation_group"], name="TeamGroup_teamId_organisationGroupId_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["organisation_group"]),
        ]


class OrganisationGlobalSettings(models.Model):
    id = models.TextField(primary_key=True, default=None)
    document_visibility = models.TextField(choices=DocumentVisibility.choices, db_column="documentVisibility", default=DocumentVisibility.EVERYONE)
    document_language = models.TextField(db_column="documentLanguage", default="en")
    include_sender_details = models.BooleanField(db_column="includeSenderDetails", default=True)
    include_signing_certificate = models.BooleanField(db_column="includeSigningCertificate", default=True)
    include_audit_log = models.BooleanField(db_column="includeAuditLog", default=False)
    document_timezone = models.TextField(null=True, db_column="documentTimezone")
    document_date_format = models.TextField(db_column="documentDateFormat", default="yyyy-MM-dd hh:mm a")
    delegate_document_ownership = models.BooleanField(db_column="delegateDocumentOwnership", default=False)
    typed_signature_enabled = models.BooleanField(db_column="typedSignatureEnabled", default=True)
    upload_signature_enabled = models.BooleanField(db_column="uploadSignatureEnabled", default=True)
    draw_signature_enabled = models.BooleanField(db_column="drawSignatureEnabled", default=True)
    default_recipients = models.JSONField(null=True, db_column="defaultRecipients")
    email = models.ForeignKey("OrganisationEmail", on_delete=models.SET_NULL, related_name="organisation_global_settings", db_column="emailId", null=True, db_index=False)
    email_reply_to = models.TextField(null=True, db_column="emailReplyTo")
    email_document_settings = models.JSONField(db_column="emailDocumentSettings")
    branding_enabled = models.BooleanField(db_column="brandingEnabled", default=False)
    branding_logo = models.TextField(db_column="brandingLogo", default="")
    branding_url = models.TextField(db_column="brandingUrl", default="")
    branding_company_details = models.TextField(db_column="brandingCompanyDetails", default="")
    branding_colors = models.JSONField(null=True, db_column="brandingColors")
    branding_css = models.TextField(db_column="brandingCss", default="")
    envelope_expiration_period = models.JSONField(null=True, db_column="envelopeExpirationPeriod")
    reminder_settings = models.JSONField(null=True, db_column="reminderSettings")
    ai_features_enabled = models.BooleanField(db_column="aiFeaturesEnabled", default=False)

    class Meta:
        db_table = "OrganisationGlobalSettings"


class TeamGlobalSettings(models.Model):
    id = models.TextField(primary_key=True, default=None)
    document_visibility = models.TextField(choices=DocumentVisibility.choices, null=True, db_column="documentVisibility")
    document_language = models.TextField(null=True, db_column="documentLanguage")
    document_timezone = models.TextField(null=True, db_column="documentTimezone")
    document_date_format = models.TextField(null=True, db_column="documentDateFormat")
    delegate_document_ownership = models.BooleanField(null=True, db_column="delegateDocumentOwnership")
    include_sender_details = models.BooleanField(null=True, db_column="includeSenderDetails")
    include_signing_certificate = models.BooleanField(null=True, db_column="includeSigningCertificate")
    include_audit_log = models.BooleanField(null=True, db_column="includeAuditLog")
    typed_signature_enabled = models.BooleanField(null=True, db_column="typedSignatureEnabled")
    upload_signature_enabled = models.BooleanField(null=True, db_column="uploadSignatureEnabled")
    draw_signature_enabled = models.BooleanField(null=True, db_column="drawSignatureEnabled")
    default_recipients = models.JSONField(null=True, db_column="defaultRecipients")
    email = models.ForeignKey("OrganisationEmail", on_delete=models.SET_NULL, related_name="team_global_settings", db_column="emailId", null=True, db_index=False)
    email_reply_to = models.TextField(null=True, db_column="emailReplyTo")
    email_document_settings = models.JSONField(null=True, db_column="emailDocumentSettings")
    branding_enabled = models.BooleanField(null=True, db_column="brandingEnabled")
    branding_logo = models.TextField(null=True, db_column="brandingLogo")
    branding_url = models.TextField(null=True, db_column="brandingUrl")
    branding_company_details = models.TextField(null=True, db_column="brandingCompanyDetails")
    branding_colors = models.JSONField(null=True, db_column="brandingColors")
    branding_css = models.TextField(null=True, db_column="brandingCss")
    envelope_expiration_period = models.JSONField(null=True, db_column="envelopeExpirationPeriod")
    reminder_settings = models.JSONField(null=True, db_column="reminderSettings")
    ai_features_enabled = models.BooleanField(null=True, db_column="aiFeaturesEnabled")

    class Meta:
        db_table = "TeamGlobalSettings"


class Team(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    url = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    avatar_image = models.ForeignKey("AvatarImage", on_delete=models.SET_NULL, related_name="team", db_column="avatarImageId", null=True, db_index=False)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="teams", db_column="organisationId", db_index=False)
    team_global_settings = models.OneToOneField("TeamGlobalSettings", on_delete=models.CASCADE, related_name="team", db_column="teamGlobalSettingsId")

    class Meta:
        db_table = "Team"
        indexes = [
            models.Index(fields=["name"]),
            models.Index(fields=["organisation"]),
        ]


class TeamEmail(models.Model):
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="team_email", primary_key=True, db_column="teamId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    name = models.TextField()
    email = models.TextField(unique=True)

    class Meta:
        db_table = "TeamEmail"


class TeamEmailVerification(models.Model):
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="email_verification", primary_key=True, db_column="teamId")
    name = models.TextField()
    email = models.TextField()
    token = models.TextField(unique=True)
    completed = models.BooleanField(default=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "TeamEmailVerification"


class TemplateDirectLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    envelope = models.OneToOneField("Envelope", on_delete=models.CASCADE, related_name="direct_link", db_column="envelopeId")
    token = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    enabled = models.BooleanField()
    direct_template_recipient_id = models.IntegerField(db_column="directTemplateRecipientId")

    class Meta:
        db_table = "TemplateDirectLink"


class SiteSettings(models.Model):
    id = models.TextField(primary_key=True, default=None)
    enabled = models.BooleanField(default=False)
    data = models.JSONField()
    last_modified_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="site_settings", db_column="lastModifiedByUserId", null=True, db_index=False)
    last_modified_at = UtcDateTimeField(db_column="lastModifiedAt", default=timezone.now)

    class Meta:
        db_table = "SiteSettings"


class BackgroundJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField(choices=BackgroundJobStatus.choices, default=BackgroundJobStatus.PENDING)
    payload = models.JSONField(null=True)
    retried = models.IntegerField(default=0)
    max_retries = models.IntegerField(db_column="maxRetries", default=3)
    job_id = models.TextField(db_column="jobId")
    name = models.TextField()
    version = models.TextField()
    submitted_at = UtcDateTimeField(db_column="submittedAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    last_retried_at = UtcDateTimeField(null=True, db_column="lastRetriedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BackgroundJob"


class BackgroundJobTask(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    status = models.TextField(choices=BackgroundJobTaskStatus.choices, default=BackgroundJobTaskStatus.PENDING)
    result = models.JSONField(null=True)
    retried = models.IntegerField(default=0)
    max_retries = models.IntegerField(db_column="maxRetries", default=3)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    background_job = models.ForeignKey("BackgroundJob", on_delete=models.CASCADE, related_name="tasks", db_column="jobId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BackgroundJobTask"


class AvatarImage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    bytes = models.TextField()

    class Meta:
        db_table = "AvatarImage"


class EmailDomain(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    status = models.TextField(choices=EmailDomainStatus.choices, default=EmailDomainStatus.PENDING)
    selector = models.TextField(unique=True)
    domain = models.TextField(unique=True)
    public_key = models.TextField(db_column="publicKey")
    private_key = models.TextField(db_column="privateKey")
    last_verified_at = UtcDateTimeField(null=True, db_column="lastVerifiedAt")
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="email_domains", db_column="organisationId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailDomain"


class OrganisationEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField(unique=True)
    email_name = models.TextField(db_column="emailName")
    email_domain = models.ForeignKey("EmailDomain", on_delete=models.CASCADE, related_name="emails", db_column="emailDomainId", db_index=False)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="organisation_emails", db_column="organisationId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganisationEmail"


class EmailTransport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    type = models.TextField(choices=EmailTransportType.choices)
    from_name = models.TextField(db_column="fromName")
    from_address = models.TextField(db_column="fromAddress")
    config = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailTransport"


class OrganisationAuthenticationPortal(models.Model):
    id = models.TextField(primary_key=True, default=None)
    enabled = models.BooleanField(default=False)
    client_id = models.TextField(db_column="clientId", default="")
    client_secret = models.TextField(db_column="clientSecret", default="")
    well_known_url = models.TextField(db_column="wellKnownUrl", default="")
    default_organisation_role = models.TextField(choices=OrganisationMemberRole.choices, db_column="defaultOrganisationRole", default=OrganisationMemberRole.MEMBER)
    auto_provision_users = models.BooleanField(db_column="autoProvisionUsers", default=True)
    allowed_domains = ArrayField(models.TextField(), db_column="allowedDomains", default=list)
    allow_personal_organisations = models.BooleanField(db_column="allowPersonalOrganisations", default=False)

    class Meta:
        db_table = "OrganisationAuthenticationPortal"


class Counter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    value = models.IntegerField()

    class Meta:
        db_table = "Counter"


class EmbedSignature(models.Model):
    key = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organisation = models.ForeignKey("Organisation", on_delete=models.CASCADE, related_name="embed_signatures", db_column="organisationId", db_index=False)
    recipient_id = models.IntegerField(db_column="recipientId")
    signature = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmbedSignature"
        indexes = [
            models.Index(fields=["organisation"]),
            models.Index(fields=["recipient_id"]),
        ]


class RateLimit(models.Model):
    pk = models.CompositePrimaryKey("key", "action", "bucket")
    key = models.TextField()
    action = models.TextField()
    bucket = UtcDateTimeField()
    count = models.IntegerField(default=1)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "RateLimit"
        indexes = [
            models.Index(fields=["created_at"]),
        ]
