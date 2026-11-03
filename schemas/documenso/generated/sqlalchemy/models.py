from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, Index, MetaData, Numeric, String, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from decimal import Decimal as DecimalType
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text, DecimalType: Numeric(precision=65, scale=30)}


class User(Base):
    __tablename__ = "User"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[Optional[str]]
    email: Mapped[str]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    password: Mapped[Optional[str]]
    source: Mapped[Optional[str]]
    signature: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    last_signed_in: Mapped[datetime] = mapped_column("lastSignedIn", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    roles: Mapped[list[str]] = mapped_column(ARRAY(Enum("ADMIN", "USER", name="Role")), nullable=True, default=lambda: ["USER"], server_default=text("ARRAY['USER']::\"Role\"[]"))
    identity_provider: Mapped[str] = mapped_column("identityProvider", Enum("DOCUMENSO", "GOOGLE", "OIDC", name="IdentityProvider"), default="DOCUMENSO", server_default=text("'DOCUMENSO'"))
    avatar_image_id: Mapped[Optional[str]] = mapped_column("avatarImageId", ForeignKey("AvatarImage.id", ondelete="SET NULL", onupdate="CASCADE", name="User_avatarImageId_fkey"))
    disabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    two_factor_secret: Mapped[Optional[str]] = mapped_column("twoFactorSecret")
    two_factor_enabled: Mapped[bool] = mapped_column("twoFactorEnabled", default=False, server_default=text("false"))
    two_factor_backup_codes: Mapped[Optional[str]] = mapped_column("twoFactorBackupCodes")

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_email_idx", "email"),
    )

    avatar_image: Mapped[Optional["AvatarImage"]] = relationship(back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    password_reset_tokens: Mapped[list["PasswordResetToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    owned_organisations: Mapped[list["Organisation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    organisation_member: Mapped[list["OrganisationMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    folders: Mapped[list["Folder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    envelopes: Mapped[list["Envelope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    verification_tokens: Mapped[list["VerificationToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_tokens: Mapped[list["ApiToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    security_audit_logs: Mapped[list["UserSecurityAuditLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    site_settings: Mapped[list["SiteSettings"]] = relationship(back_populates="last_modified_by_user")
    passkeys: Mapped[list["Passkey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class TeamProfile(Base):
    __tablename__ = "TeamProfile"

    id: Mapped[str] = mapped_column(primary_key=True)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamProfile_teamId_fkey"))
    bio: Mapped[Optional[str]]

    __table_args__ = (
        Index("TeamProfile_teamId_key", "teamId", unique=True),
    )

    team: Mapped[Optional["Team"]] = relationship(back_populates="profile")

class UserSecurityAuditLog(Base):
    __tablename__ = "UserSecurityAuditLog"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UserSecurityAuditLog_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    type: Mapped[str] = mapped_column(Enum("ACCOUNT_PROFILE_UPDATE", "ACCOUNT_SSO_LINK", "ACCOUNT_SSO_UNLINK", "ORGANISATION_SSO_LINK", "ORGANISATION_SSO_UNLINK", "AUTH_2FA_DISABLE", "AUTH_2FA_ENABLE", "PASSKEY_CREATED", "PASSKEY_DELETED", "PASSKEY_UPDATED", "PASSWORD_RESET", "PASSWORD_UPDATE", "SESSION_REVOKED", "SIGN_OUT", "SIGN_IN", "SIGN_IN_FAIL", "SIGN_IN_2FA_FAIL", "SIGN_IN_PASSKEY_FAIL", name="UserSecurityAuditLogType"))
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")

    user: Mapped["User"] = relationship(back_populates="security_audit_logs")

class PasswordResetToken(Base):
    __tablename__ = "PasswordResetToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    token: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expiry: Mapped[datetime]
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="PasswordResetToken_userId_fkey"))

    __table_args__ = (
        Index("PasswordResetToken_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="password_reset_tokens")

class Passkey(Base):
    __tablename__ = "Passkey"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Passkey_userId_fkey"))
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    credential_id: Mapped[bytes] = mapped_column("credentialId")
    credential_public_key: Mapped[bytes] = mapped_column("credentialPublicKey")
    counter: Mapped[int] = mapped_column(BigInteger)
    credential_device_type: Mapped[str] = mapped_column("credentialDeviceType")
    credential_backed_up: Mapped[bool] = mapped_column("credentialBackedUp")
    transports: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)

    user: Mapped["User"] = relationship(back_populates="passkeys")

class AnonymousVerificationToken(Base):
    __tablename__ = "AnonymousVerificationToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AnonymousVerificationToken_id_key", "id", unique=True),
        Index("AnonymousVerificationToken_token_key", "token", unique=True),
    )

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    secondary_id: Mapped[str] = mapped_column("secondaryId")
    identifier: Mapped[str]
    token: Mapped[str]
    completed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="VerificationToken_userId_fkey"))

    __table_args__ = (
        Index("VerificationToken_secondaryId_key", "secondaryId", unique=True),
        Index("VerificationToken_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="verification_tokens")

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    webhook_url: Mapped[str] = mapped_column("webhookUrl")
    event_triggers: Mapped[list[str]] = mapped_column("eventTriggers", ARRAY(Enum("DOCUMENT_CREATED", "DOCUMENT_SENT", "DOCUMENT_OPENED", "DOCUMENT_SIGNED", "DOCUMENT_COMPLETED", "DOCUMENT_REJECTED", "DOCUMENT_CANCELLED", "RECIPIENT_EXPIRED", "DOCUMENT_RECIPIENT_COMPLETED", "DOCUMENT_REMINDER_SENT", "TEMPLATE_CREATED", "TEMPLATE_UPDATED", "TEMPLATE_DELETED", "TEMPLATE_USED", name="WebhookTriggerEvents")), nullable=True)
    secret: Mapped[Optional[str]]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_userId_fkey"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_teamId_fkey"))

    user: Mapped["User"] = relationship(back_populates="webhooks")
    team: Mapped["Team"] = relationship(back_populates="webhooks")
    webhook_calls: Mapped[list["WebhookCall"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="webhook")

class WebhookCall(Base):
    __tablename__ = "WebhookCall"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str] = mapped_column(Enum("SUCCESS", "FAILED", name="WebhookCallStatus"))
    url: Mapped[str]
    event: Mapped[str] = mapped_column(Enum("DOCUMENT_CREATED", "DOCUMENT_SENT", "DOCUMENT_OPENED", "DOCUMENT_SIGNED", "DOCUMENT_COMPLETED", "DOCUMENT_REJECTED", "DOCUMENT_CANCELLED", "RECIPIENT_EXPIRED", "DOCUMENT_RECIPIENT_COMPLETED", "DOCUMENT_REMINDER_SENT", "TEMPLATE_CREATED", "TEMPLATE_UPDATED", "TEMPLATE_DELETED", "TEMPLATE_USED", name="WebhookTriggerEvents"))
    request_body: Mapped[dict[str, Any]] = mapped_column("requestBody", JSONB)
    response_code: Mapped[int] = mapped_column("responseCode")
    response_headers: Mapped[Optional[dict[str, Any]]] = mapped_column("responseHeaders", JSONB(none_as_null=True))
    response_body: Mapped[Optional[dict[str, Any]]] = mapped_column("responseBody", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    webhook_id: Mapped[str] = mapped_column("webhookId", ForeignKey("Webhook.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookCall_webhookId_fkey"))

    webhook: Mapped["Webhook"] = relationship(back_populates="webhook_calls")

class ApiToken(Base):
    __tablename__ = "ApiToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    token: Mapped[str]
    algorithm: Mapped[str] = mapped_column(Enum("SHA512", name="ApiTokenAlgorithm"), default="SHA512", server_default=text("'SHA512'"))
    expires: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiToken_userId_fkey"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiToken_teamId_fkey"))

    __table_args__ = (
        Index("ApiToken_token_key", "token", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="api_tokens")
    team: Mapped["Team"] = relationship(back_populates="api_tokens")

class Subscription(Base):
    __tablename__ = "Subscription"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "PAST_DUE", "INACTIVE", name="SubscriptionStatus"), default="INACTIVE", server_default=text("'INACTIVE'"))
    plan_id: Mapped[str] = mapped_column("planId")
    price_id: Mapped[str] = mapped_column("priceId")
    period_end: Mapped[Optional[datetime]] = mapped_column("periodEnd")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    cancel_at_period_end: Mapped[bool] = mapped_column("cancelAtPeriodEnd", default=False, server_default=text("false"))
    customer_id: Mapped[str] = mapped_column("customerId")
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="Subscription_organisationId_fkey"))

    __table_args__ = (
        Index("Subscription_planId_key", "planId", unique=True),
        Index("Subscription_organisationId_key", "organisationId", unique=True),
        Index("Subscription_organisationId_idx", "organisationId"),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="subscription")

class SubscriptionClaim(Base):
    __tablename__ = "SubscriptionClaim"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    locked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    team_count: Mapped[int] = mapped_column("teamCount")
    member_count: Mapped[int] = mapped_column("memberCount")
    envelope_item_count: Mapped[int] = mapped_column("envelopeItemCount")
    recipient_count: Mapped[int] = mapped_column("recipientCount")
    flags: Mapped[dict[str, Any]] = mapped_column(JSONB)
    document_rate_limits: Mapped[dict[str, Any]] = mapped_column("documentRateLimits", JSONB)
    document_quota: Mapped[Optional[int]] = mapped_column("documentQuota")
    email_rate_limits: Mapped[dict[str, Any]] = mapped_column("emailRateLimits", JSONB)
    email_quota: Mapped[Optional[int]] = mapped_column("emailQuota")
    api_rate_limits: Mapped[dict[str, Any]] = mapped_column("apiRateLimits", JSONB)
    api_quota: Mapped[Optional[int]] = mapped_column("apiQuota")
    email_transport_id: Mapped[Optional[str]] = mapped_column("emailTransportId", ForeignKey("EmailTransport.id", ondelete="SET NULL", onupdate="CASCADE", name="SubscriptionClaim_emailTransportId_fkey"))

    email_transport: Mapped[Optional["EmailTransport"]] = relationship(back_populates="subscription_claims")

class OrganisationClaim(Base):
    __tablename__ = "OrganisationClaim"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    original_subscription_claim_id: Mapped[Optional[str]] = mapped_column("originalSubscriptionClaimId")
    team_count: Mapped[int] = mapped_column("teamCount")
    member_count: Mapped[int] = mapped_column("memberCount")
    envelope_item_count: Mapped[int] = mapped_column("envelopeItemCount")
    recipient_count: Mapped[int] = mapped_column("recipientCount")
    flags: Mapped[dict[str, Any]] = mapped_column(JSONB)
    document_rate_limits: Mapped[dict[str, Any]] = mapped_column("documentRateLimits", JSONB)
    document_quota: Mapped[Optional[int]] = mapped_column("documentQuota")
    email_rate_limits: Mapped[dict[str, Any]] = mapped_column("emailRateLimits", JSONB)
    email_quota: Mapped[Optional[int]] = mapped_column("emailQuota")
    api_rate_limits: Mapped[dict[str, Any]] = mapped_column("apiRateLimits", JSONB)
    api_quota: Mapped[Optional[int]] = mapped_column("apiQuota")
    email_transport_id: Mapped[Optional[str]] = mapped_column("emailTransportId", ForeignKey("EmailTransport.id", ondelete="SET NULL", onupdate="CASCADE", name="OrganisationClaim_emailTransportId_fkey"))

    email_transport: Mapped[Optional["EmailTransport"]] = relationship(back_populates="organisation_claims")
    organisation: Mapped[Optional["Organisation"]] = relationship(passive_deletes="all", back_populates="organisation_claim")

class OrganisationMonthlyStat(Base):
    __tablename__ = "OrganisationMonthlyStat"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationMonthlyStat_organisationId_fkey"))
    period: Mapped[str]
    document_count: Mapped[int] = mapped_column("documentCount", default=0, server_default=text("0"))
    email_count: Mapped[int] = mapped_column("emailCount", default=0, server_default=text("0"))
    api_count: Mapped[int] = mapped_column("apiCount", default=0, server_default=text("0"))
    email_reports: Mapped[int] = mapped_column("emailReports", default=0, server_default=text("0"))

    __table_args__ = (
        Index("OrganisationMonthlyStat_organisationId_idx", "organisationId"),
        Index("OrganisationMonthlyStat_organisationId_period_key", "organisationId", "period", unique=True),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="monthly_stats")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    created_at: Mapped[Optional[int]]
    ext_expires_in: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]
    password: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    is_embed: Mapped[bool] = mapped_column("isEmbed", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
        Index("Session_sessionToken_idx", "sessionToken"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="sessions")

class AuthHandoff(Base):
    __tablename__ = "AuthHandoff"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    payload: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

    __table_args__ = (
        Index("AuthHandoff_expiresAt_idx", "expiresAt"),
    )

class Folder(Base):
    __tablename__ = "Folder"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Folder_userId_fkey"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Folder_teamId_fkey"))
    pinned: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("Folder.id", ondelete="CASCADE", onupdate="CASCADE", name="Folder_parentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    visibility: Mapped[str] = mapped_column(Enum("EVERYONE", "MANAGER_AND_ABOVE", "ADMIN", name="DocumentVisibility"), default="EVERYONE", server_default=text("'EVERYONE'"))
    type: Mapped[str] = mapped_column(Enum("DOCUMENT", "TEMPLATE", name="FolderType"))

    __table_args__ = (
        Index("Folder_userId_idx", "userId"),
        Index("Folder_teamId_idx", "teamId"),
        Index("Folder_parentId_idx", "parentId"),
        Index("Folder_type_idx", "type"),
    )

    user: Mapped["User"] = relationship(back_populates="folders")
    team: Mapped["Team"] = relationship(back_populates="folders")
    parent: Mapped[Optional["Folder"]] = relationship(remote_side=[id], back_populates="subfolders")
    envelopes: Mapped[list["Envelope"]] = relationship(back_populates="folder")
    subfolders: Mapped[list["Folder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")

class Envelope(Base):
    __tablename__ = "Envelope"

    id: Mapped[str] = mapped_column(primary_key=True)
    secondary_id: Mapped[str] = mapped_column("secondaryId")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    type: Mapped[str] = mapped_column(Enum("DOCUMENT", "TEMPLATE", name="EnvelopeType"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    title: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("DRAFT", "PENDING", "COMPLETED", "REJECTED", "CANCELLED", name="DocumentStatus"), default="DRAFT", server_default=text("'DRAFT'"))
    source: Mapped[str] = mapped_column(Enum("DOCUMENT", "TEMPLATE", "TEMPLATE_DIRECT_LINK", name="DocumentSource"))
    qr_token: Mapped[Optional[str]] = mapped_column("qrToken")
    signature_level: Mapped[str] = mapped_column("signatureLevel")
    internal_version: Mapped[int] = mapped_column("internalVersion")
    use_legacy_field_insertion: Mapped[bool] = mapped_column("useLegacyFieldInsertion", default=False, server_default=text("false"))
    auth_options: Mapped[Optional[dict[str, Any]]] = mapped_column("authOptions", JSONB(none_as_null=True))
    form_values: Mapped[Optional[dict[str, Any]]] = mapped_column("formValues", JSONB(none_as_null=True))
    visibility: Mapped[str] = mapped_column(Enum("EVERYONE", "MANAGER_AND_ABOVE", "ADMIN", name="DocumentVisibility"), default="EVERYONE", server_default=text("'EVERYONE'"))
    template_type: Mapped[str] = mapped_column("templateType", Enum("PUBLIC", "PRIVATE", "ORGANISATION", name="TemplateType"), default="PRIVATE", server_default=text("'PRIVATE'"))
    public_title: Mapped[str] = mapped_column("publicTitle", default="", server_default=text("''"))
    public_description: Mapped[str] = mapped_column("publicDescription", default="", server_default=text("''"))
    template_id: Mapped[Optional[int]] = mapped_column("templateId")
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Envelope_userId_fkey"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Envelope_teamId_fkey"))
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="SET NULL", onupdate="CASCADE", name="Envelope_folderId_fkey"))
    document_meta_id: Mapped[str] = mapped_column("documentMetaId", ForeignKey("DocumentMeta.id", ondelete="RESTRICT", onupdate="CASCADE", name="Envelope_documentMetaId_fkey"))

    __table_args__ = (
        Index("Envelope_secondaryId_key", "secondaryId", unique=True),
        Index("Envelope_documentMetaId_key", "documentMetaId", unique=True),
        Index("Envelope_type_idx", "type"),
        Index("Envelope_status_idx", "status"),
        Index("Envelope_userId_idx", "userId"),
        Index("Envelope_teamId_idx", "teamId"),
        Index("Envelope_folderId_idx", "folderId"),
        Index("Envelope_createdAt_idx", "createdAt"),
    )

    user: Mapped["User"] = relationship(back_populates="envelopes")
    team: Mapped["Team"] = relationship(back_populates="envelopes")
    folder: Mapped[Optional["Folder"]] = relationship(back_populates="envelopes")
    document_meta: Mapped["DocumentMeta"] = relationship(back_populates="envelope")
    envelope_items: Mapped[list["EnvelopeItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")
    recipients: Mapped[list["Recipient"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")
    fields: Mapped[list["Field"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")
    share_links: Mapped[list["DocumentShareLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")
    audit_logs: Mapped[list["DocumentAuditLog"]] = relationship(back_populates="envelope")
    envelope_attachments: Mapped[list["EnvelopeAttachment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")
    direct_link: Mapped[Optional["TemplateDirectLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope")

class EnvelopeItem(Base):
    __tablename__ = "EnvelopeItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[str]
    order: Mapped[int]
    document_data_id: Mapped[str] = mapped_column("documentDataId", ForeignKey("DocumentData.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvelopeItem_documentDataId_fkey"))
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvelopeItem_envelopeId_fkey"))

    __table_args__ = (
        Index("EnvelopeItem_envelopeId_idx", "envelopeId"),
        Index("EnvelopeItem_documentDataId_key", "documentDataId", unique=True),
    )

    document_data: Mapped["DocumentData"] = relationship(back_populates="envelope_item")
    envelope: Mapped["Envelope"] = relationship(back_populates="envelope_items")
    field: Mapped[list["Field"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="envelope_item")

class DocumentAuditLog(Base):
    __tablename__ = "DocumentAuditLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    envelope_id: Mapped[Optional[str]] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentAuditLog_envelopeId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    type: Mapped[str]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    user_id: Mapped[Optional[int]] = mapped_column("userId")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")

    __table_args__ = (
        Index("DocumentAuditLog_envelopeId_idx", "envelopeId"),
    )

    envelope: Mapped[Optional["Envelope"]] = relationship(back_populates="audit_logs")

class DocumentData(Base):
    __tablename__ = "DocumentData"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("S3_PATH", "BYTES", "BYTES_64", name="DocumentDataType"))
    data: Mapped[str]
    initial_data: Mapped[str] = mapped_column("initialData")

    envelope_item: Mapped[Optional["EnvelopeItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document_data")

class DocumentMeta(Base):
    __tablename__ = "DocumentMeta"

    id: Mapped[str] = mapped_column(primary_key=True)
    subject: Mapped[Optional[str]]
    message: Mapped[Optional[str]]
    timezone: Mapped[Optional[str]] = mapped_column(default="Etc/UTC", server_default=text("'Etc/UTC'"))
    date_format: Mapped[Optional[str]] = mapped_column("dateFormat", default="yyyy-MM-dd hh:mm a", server_default=text("'yyyy-MM-dd hh:mm a'"))
    redirect_url: Mapped[Optional[str]] = mapped_column("redirectUrl")
    signing_order: Mapped[str] = mapped_column("signingOrder", Enum("PARALLEL", "SEQUENTIAL", name="DocumentSigningOrder"), default="PARALLEL", server_default=text("'PARALLEL'"))
    allow_dictate_next_signer: Mapped[bool] = mapped_column("allowDictateNextSigner", default=False, server_default=text("false"))
    typed_signature_enabled: Mapped[bool] = mapped_column("typedSignatureEnabled", default=True, server_default=text("true"))
    upload_signature_enabled: Mapped[bool] = mapped_column("uploadSignatureEnabled", default=True, server_default=text("true"))
    draw_signature_enabled: Mapped[bool] = mapped_column("drawSignatureEnabled", default=True, server_default=text("true"))
    language: Mapped[str] = mapped_column(default="en", server_default=text("'en'"))
    distribution_method: Mapped[str] = mapped_column("distributionMethod", Enum("EMAIL", "NONE", name="DocumentDistributionMethod"), default="EMAIL", server_default=text("'EMAIL'"))
    email_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("emailSettings", JSONB(none_as_null=True))
    email_reply_to: Mapped[Optional[str]] = mapped_column("emailReplyTo")
    email_id: Mapped[Optional[str]] = mapped_column("emailId")
    envelope_expiration_period: Mapped[Optional[dict[str, Any]]] = mapped_column("envelopeExpirationPeriod", JSONB(none_as_null=True))
    reminder_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("reminderSettings", JSONB(none_as_null=True))

    envelope: Mapped[Optional["Envelope"]] = relationship(passive_deletes="all", back_populates="document_meta")

class EnvelopeAttachment(Base):
    __tablename__ = "EnvelopeAttachment"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    label: Mapped[str]
    data: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvelopeAttachment_envelopeId_fkey"))

    __table_args__ = (
        Index("EnvelopeAttachment_envelopeId_idx", "envelopeId"),
    )

    envelope: Mapped["Envelope"] = relationship(back_populates="envelope_attachments")

class Recipient(Base):
    __tablename__ = "Recipient"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="Recipient_envelopeId_fkey"))
    email: Mapped[str] = mapped_column(String(255))
    name: Mapped[str] = mapped_column(String(255), default="", server_default=text("''"))
    token: Mapped[str]
    document_deleted_at: Mapped[Optional[datetime]] = mapped_column("documentDeletedAt")
    expired: Mapped[Optional[datetime]]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    expiration_notified_at: Mapped[Optional[datetime]] = mapped_column("expirationNotifiedAt")
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")
    signed_at: Mapped[Optional[datetime]] = mapped_column("signedAt")
    last_reminder_sent_at: Mapped[Optional[datetime]] = mapped_column("lastReminderSentAt")
    next_reminder_at: Mapped[Optional[datetime]] = mapped_column("nextReminderAt")
    reminder_count: Mapped[int] = mapped_column("reminderCount", default=0, server_default=text("0"))
    auth_options: Mapped[Optional[dict[str, Any]]] = mapped_column("authOptions", JSONB(none_as_null=True))
    signing_order: Mapped[Optional[int]] = mapped_column("signingOrder")
    rejection_reason: Mapped[Optional[str]] = mapped_column("rejectionReason")
    role: Mapped[str] = mapped_column(Enum("CC", "SIGNER", "VIEWER", "APPROVER", "ASSISTANT", name="RecipientRole"), default="SIGNER", server_default=text("'SIGNER'"))
    read_status: Mapped[str] = mapped_column("readStatus", Enum("NOT_OPENED", "OPENED", name="ReadStatus"), default="NOT_OPENED", server_default=text("'NOT_OPENED'"))
    signing_status: Mapped[str] = mapped_column("signingStatus", Enum("NOT_SIGNED", "SIGNED", "REJECTED", name="SigningStatus"), default="NOT_SIGNED", server_default=text("'NOT_SIGNED'"))
    send_status: Mapped[str] = mapped_column("sendStatus", Enum("NOT_SENT", "SENT", name="SendStatus"), default="NOT_SENT", server_default=text("'NOT_SENT'"))

    __table_args__ = (
        Index("Recipient_token_idx", "token"),
        Index("Recipient_email_idx", "email"),
        Index("Recipient_envelopeId_idx", "envelopeId"),
        Index("Recipient_signedAt_idx", "signedAt"),
        Index("Recipient_expiresAt_idx", "expiresAt"),
        Index("Recipient_email_documentDeletedAt_envelopeId_idx", "email", "documentDeletedAt", "envelopeId"),
        Index("Recipient_email_envelopeId_idx", "email", "envelopeId"),
        Index("Recipient_email_signingStatus_envelopeId_role_idx", "email", "signingStatus", "envelopeId", "role"),
        Index("Recipient_nextReminderAt_idx", "nextReminderAt"),
        Index("Recipient_email_trgm_idx", "email"),
        Index("Recipient_name_trgm_idx", "name"),
    )

    envelope: Mapped["Envelope"] = relationship(back_populates="recipients")
    fields: Mapped[list["Field"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="recipient")
    signatures: Mapped[list["Signature"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="recipient")
    csc_credential: Mapped[Optional["CscCredential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="recipient")
    csc_session: Mapped[Optional["CscSession"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="recipient")

class Field(Base):
    __tablename__ = "Field"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    secondary_id: Mapped[str] = mapped_column("secondaryId")
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="Field_envelopeId_fkey"))
    envelope_item_id: Mapped[str] = mapped_column("envelopeItemId", ForeignKey("EnvelopeItem.id", ondelete="CASCADE", onupdate="CASCADE", name="Field_envelopeItemId_fkey"))
    recipient_id: Mapped[int] = mapped_column("recipientId", ForeignKey("Recipient.id", ondelete="CASCADE", onupdate="CASCADE", name="Field_recipientId_fkey"))
    type: Mapped[str] = mapped_column(Enum("SIGNATURE", "FREE_SIGNATURE", "INITIALS", "NAME", "EMAIL", "DATE", "TEXT", "NUMBER", "RADIO", "CHECKBOX", "DROPDOWN", name="FieldType"))
    page: Mapped[int]
    position_x: Mapped[DecimalType] = mapped_column("positionX", default=DecimalType("0"), server_default=text("0"))
    position_y: Mapped[DecimalType] = mapped_column("positionY", default=DecimalType("0"), server_default=text("0"))
    width: Mapped[DecimalType] = mapped_column(default=DecimalType("-1"), server_default=text("-1"))
    height: Mapped[DecimalType] = mapped_column(default=DecimalType("-1"), server_default=text("-1"))
    custom_text: Mapped[str] = mapped_column("customText")
    inserted: Mapped[bool]
    field_meta: Mapped[Optional[dict[str, Any]]] = mapped_column("fieldMeta", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Field_secondaryId_key", "secondaryId", unique=True),
        Index("Field_envelopeId_idx", "envelopeId"),
        Index("Field_envelopeItemId_idx", "envelopeItemId"),
        Index("Field_recipientId_idx", "recipientId"),
    )

    envelope_item: Mapped["EnvelopeItem"] = relationship(back_populates="field")
    envelope: Mapped["Envelope"] = relationship(back_populates="fields")
    recipient: Mapped["Recipient"] = relationship(back_populates="fields")
    signature: Mapped[Optional["Signature"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="field")

class Signature(Base):
    __tablename__ = "Signature"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    recipient_id: Mapped[int] = mapped_column("recipientId", ForeignKey("Recipient.id", ondelete="CASCADE", onupdate="CASCADE", name="Signature_recipientId_fkey"))
    field_id: Mapped[int] = mapped_column("fieldId", ForeignKey("Field.id", ondelete="CASCADE", onupdate="CASCADE", name="Signature_fieldId_fkey"))
    signature_image_as_base64: Mapped[Optional[str]] = mapped_column("signatureImageAsBase64")
    typed_signature: Mapped[Optional[str]] = mapped_column("typedSignature")

    __table_args__ = (
        Index("Signature_fieldId_key", "fieldId", unique=True),
        Index("Signature_recipientId_idx", "recipientId"),
    )

    recipient: Mapped["Recipient"] = relationship(back_populates="signatures")
    field: Mapped["Field"] = relationship(back_populates="signature")

class CscCredential(Base):
    __tablename__ = "CscCredential"

    id: Mapped[str] = mapped_column(primary_key=True)
    provider_id: Mapped[str] = mapped_column("providerId")
    credential_id: Mapped[str] = mapped_column("credentialId")
    cert_cache: Mapped[Optional[bytes]] = mapped_column("certCache")
    signature_algorithm: Mapped[str] = mapped_column("signatureAlgorithm")
    key_type: Mapped[str] = mapped_column("keyType")
    digest_algorithm: Mapped[str] = mapped_column("digestAlgorithm")
    key_len_bits: Mapped[Optional[int]] = mapped_column("keyLenBits")
    sign_algo_params: Mapped[Optional[str]] = mapped_column("signAlgoParams")
    service_token_ciphertext: Mapped[Optional[bytes]] = mapped_column("serviceTokenCiphertext")
    service_token_expires_at: Mapped[Optional[datetime]] = mapped_column("serviceTokenExpiresAt")
    recipient_id: Mapped[int] = mapped_column("recipientId", ForeignKey("Recipient.id", ondelete="CASCADE", onupdate="CASCADE", name="CscCredential_recipientId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("CscCredential_recipientId_key", "recipientId", unique=True),
    )

    recipient: Mapped["Recipient"] = relationship(back_populates="csc_credential")

class CscSession(Base):
    __tablename__ = "CscSession"

    id: Mapped[str] = mapped_column(primary_key=True)
    envelope_id: Mapped[str] = mapped_column("envelopeId")
    signing_time: Mapped[datetime] = mapped_column("signingTime")
    items_json: Mapped[dict[str, Any]] = mapped_column("itemsJson", JSONB)
    encrypted_sad: Mapped[Optional[bytes]] = mapped_column("encryptedSad")
    sad_expires_at: Mapped[Optional[datetime]] = mapped_column("sadExpiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    recipient_id: Mapped[int] = mapped_column("recipientId", ForeignKey("Recipient.id", ondelete="CASCADE", onupdate="CASCADE", name="CscSession_recipientId_fkey"))

    __table_args__ = (
        Index("CscSession_recipientId_key", "recipientId", unique=True),
    )

    recipient: Mapped["Recipient"] = relationship(back_populates="csc_session")

class DocumentShareLink(Base):
    __tablename__ = "DocumentShareLink"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    email: Mapped[str]
    slug: Mapped[str]
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentShareLink_envelopeId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentShareLink_slug_key", "slug", unique=True),
        Index("DocumentShareLink_envelopeId_email_key", "envelopeId", "email", unique=True),
    )

    envelope: Mapped["Envelope"] = relationship(back_populates="share_links")

class Organisation(Base):
    __tablename__ = "Organisation"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("PERSONAL", "ORGANISATION", name="OrganisationType"))
    name: Mapped[str]
    url: Mapped[str]
    avatar_image_id: Mapped[Optional[str]] = mapped_column("avatarImageId", ForeignKey("AvatarImage.id", ondelete="SET NULL", onupdate="CASCADE", name="Organisation_avatarImageId_fkey"))
    customer_id: Mapped[Optional[str]] = mapped_column("customerId")
    organisation_claim_id: Mapped[str] = mapped_column("organisationClaimId", ForeignKey("OrganisationClaim.id", ondelete="RESTRICT", onupdate="CASCADE", name="Organisation_organisationClaimId_fkey"))
    owner_user_id: Mapped[int] = mapped_column("ownerUserId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Organisation_ownerUserId_fkey"))
    organisation_global_settings_id: Mapped[str] = mapped_column("organisationGlobalSettingsId", ForeignKey("OrganisationGlobalSettings.id", ondelete="RESTRICT", onupdate="CASCADE", name="Organisation_organisationGlobalSettingsId_fkey"))
    organisation_authentication_portal_id: Mapped[str] = mapped_column("organisationAuthenticationPortalId", ForeignKey("OrganisationAuthenticationPortal.id", ondelete="RESTRICT", onupdate="CASCADE", name="Organisation_organisationAuthenticationPortalId_fkey"))

    __table_args__ = (
        Index("Organisation_url_key", "url", unique=True),
        Index("Organisation_customerId_key", "customerId", unique=True),
        Index("Organisation_organisationClaimId_key", "organisationClaimId", unique=True),
        Index("Organisation_organisationGlobalSettingsId_key", "organisationGlobalSettingsId", unique=True),
        Index("Organisation_organisationAuthenticationPortalId_key", "organisationAuthenticationPortalId", unique=True),
        Index("Organisation_name_idx", "name"),
        Index("Organisation_ownerUserId_idx", "ownerUserId"),
    )

    organisation_claim: Mapped["OrganisationClaim"] = relationship(back_populates="organisation")
    avatar_image: Mapped[Optional["AvatarImage"]] = relationship(back_populates="organisation")
    owner: Mapped["User"] = relationship(back_populates="owned_organisations")
    organisation_global_settings: Mapped["OrganisationGlobalSettings"] = relationship(back_populates="organisation")
    organisation_authentication_portal: Mapped["OrganisationAuthenticationPortal"] = relationship(back_populates="organisation")
    members: Mapped[list["OrganisationMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    invites: Mapped[list["OrganisationMemberInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    groups: Mapped[list["OrganisationGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    teams: Mapped[list["Team"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    email_domains: Mapped[list["EmailDomain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    organisation_emails: Mapped[list["OrganisationEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    monthly_stats: Mapped[list["OrganisationMonthlyStat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    embed_signatures: Mapped[list["EmbedSignature"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")
    subscription: Mapped[Optional["Subscription"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation")

class OrganisationMember(Base):
    __tablename__ = "OrganisationMember"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationMember_userId_fkey"))
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationMember_organisationId_fkey"))

    __table_args__ = (
        Index("OrganisationMember_organisationId_idx", "organisationId"),
        Index("OrganisationMember_userId_organisationId_key", "userId", "organisationId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="organisation_member")
    organisation: Mapped["Organisation"] = relationship(back_populates="members")
    organisation_group_members: Mapped[list["OrganisationGroupMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation_member")

class OrganisationMemberInvite(Base):
    __tablename__ = "OrganisationMemberInvite"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    email: Mapped[str]
    token: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("ACCEPTED", "PENDING", "DECLINED", name="OrganisationMemberInviteStatus"), default="PENDING", server_default=text("'PENDING'"))
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationMemberInvite_organisationId_fkey"))
    organisation_role: Mapped[str] = mapped_column("organisationRole", Enum("ADMIN", "MANAGER", "MEMBER", name="OrganisationMemberRole"))

    __table_args__ = (
        Index("OrganisationMemberInvite_token_key", "token", unique=True),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="invites")

class OrganisationGroup(Base):
    __tablename__ = "OrganisationGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("INTERNAL_ORGANISATION", "INTERNAL_TEAM", "CUSTOM", name="OrganisationGroupType"))
    organisation_role: Mapped[str] = mapped_column("organisationRole", Enum("ADMIN", "MANAGER", "MEMBER", name="OrganisationMemberRole"))
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationGroup_organisationId_fkey"))

    __table_args__ = (
        Index("OrganisationGroup_organisationId_idx", "organisationId"),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="groups")
    organisation_group_members: Mapped[list["OrganisationGroupMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    team_groups: Mapped[list["TeamGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organisation_group")

class OrganisationGroupMember(Base):
    __tablename__ = "OrganisationGroupMember"

    id: Mapped[str] = mapped_column(primary_key=True)
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("OrganisationGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationGroupMember_groupId_fkey"))
    organisation_member_id: Mapped[str] = mapped_column("organisationMemberId", ForeignKey("OrganisationMember.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationGroupMember_organisationMemberId_fkey"))

    __table_args__ = (
        Index("OrganisationGroupMember_groupId_idx", "groupId"),
        Index("OrganisationGroupMember_organisationMemberId_idx", "organisationMemberId"),
        Index("OrganisationGroupMember_organisationMemberId_groupId_key", "organisationMemberId", "groupId", unique=True),
    )

    group: Mapped["OrganisationGroup"] = relationship(back_populates="organisation_group_members")
    organisation_member: Mapped["OrganisationMember"] = relationship(back_populates="organisation_group_members")

class TeamGroup(Base):
    __tablename__ = "TeamGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    organisation_group_id: Mapped[str] = mapped_column("organisationGroupId", ForeignKey("OrganisationGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamGroup_organisationGroupId_fkey"))
    team_role: Mapped[str] = mapped_column("teamRole", Enum("ADMIN", "MANAGER", "MEMBER", name="TeamMemberRole"))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamGroup_teamId_fkey"))

    __table_args__ = (
        Index("TeamGroup_teamId_idx", "teamId"),
        Index("TeamGroup_organisationGroupId_idx", "organisationGroupId"),
        Index("TeamGroup_teamId_organisationGroupId_key", "teamId", "organisationGroupId", unique=True),
    )

    organisation_group: Mapped["OrganisationGroup"] = relationship(back_populates="team_groups")
    team: Mapped["Team"] = relationship(back_populates="team_groups")

class OrganisationGlobalSettings(Base):
    __tablename__ = "OrganisationGlobalSettings"

    id: Mapped[str] = mapped_column(primary_key=True)
    document_visibility: Mapped[str] = mapped_column("documentVisibility", Enum("EVERYONE", "MANAGER_AND_ABOVE", "ADMIN", name="DocumentVisibility"), default="EVERYONE", server_default=text("'EVERYONE'"))
    document_language: Mapped[str] = mapped_column("documentLanguage", default="en", server_default=text("'en'"))
    include_sender_details: Mapped[bool] = mapped_column("includeSenderDetails", default=True, server_default=text("true"))
    include_signing_certificate: Mapped[bool] = mapped_column("includeSigningCertificate", default=True, server_default=text("true"))
    include_audit_log: Mapped[bool] = mapped_column("includeAuditLog", default=False, server_default=text("false"))
    document_timezone: Mapped[Optional[str]] = mapped_column("documentTimezone")
    document_date_format: Mapped[str] = mapped_column("documentDateFormat", default="yyyy-MM-dd hh:mm a", server_default=text("'yyyy-MM-dd hh:mm a'"))
    delegate_document_ownership: Mapped[bool] = mapped_column("delegateDocumentOwnership", default=False, server_default=text("false"))
    typed_signature_enabled: Mapped[bool] = mapped_column("typedSignatureEnabled", default=True, server_default=text("true"))
    upload_signature_enabled: Mapped[bool] = mapped_column("uploadSignatureEnabled", default=True, server_default=text("true"))
    draw_signature_enabled: Mapped[bool] = mapped_column("drawSignatureEnabled", default=True, server_default=text("true"))
    default_recipients: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultRecipients", JSONB(none_as_null=True))
    email_id: Mapped[Optional[str]] = mapped_column("emailId", ForeignKey("OrganisationEmail.id", ondelete="SET NULL", onupdate="CASCADE", name="OrganisationGlobalSettings_emailId_fkey"))
    email_reply_to: Mapped[Optional[str]] = mapped_column("emailReplyTo")
    email_document_settings: Mapped[dict[str, Any]] = mapped_column("emailDocumentSettings", JSONB)
    branding_enabled: Mapped[bool] = mapped_column("brandingEnabled", default=False, server_default=text("false"))
    branding_logo: Mapped[str] = mapped_column("brandingLogo", default="", server_default=text("''"))
    branding_url: Mapped[str] = mapped_column("brandingUrl", default="", server_default=text("''"))
    branding_company_details: Mapped[str] = mapped_column("brandingCompanyDetails", default="", server_default=text("''"))
    branding_colors: Mapped[Optional[dict[str, Any]]] = mapped_column("brandingColors", JSONB(none_as_null=True))
    branding_css: Mapped[str] = mapped_column("brandingCss", default="", server_default=text("''"))
    envelope_expiration_period: Mapped[Optional[dict[str, Any]]] = mapped_column("envelopeExpirationPeriod", JSONB(none_as_null=True))
    reminder_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("reminderSettings", JSONB(none_as_null=True))
    ai_features_enabled: Mapped[bool] = mapped_column("aiFeaturesEnabled", default=False, server_default=text("false"))

    email: Mapped[Optional["OrganisationEmail"]] = relationship(back_populates="organisation_global_settings")
    organisation: Mapped[Optional["Organisation"]] = relationship(passive_deletes="all", back_populates="organisation_global_settings")

class TeamGlobalSettings(Base):
    __tablename__ = "TeamGlobalSettings"

    id: Mapped[str] = mapped_column(primary_key=True)
    document_visibility: Mapped[Optional[str]] = mapped_column("documentVisibility", Enum("EVERYONE", "MANAGER_AND_ABOVE", "ADMIN", name="DocumentVisibility"))
    document_language: Mapped[Optional[str]] = mapped_column("documentLanguage")
    document_timezone: Mapped[Optional[str]] = mapped_column("documentTimezone")
    document_date_format: Mapped[Optional[str]] = mapped_column("documentDateFormat")
    delegate_document_ownership: Mapped[Optional[bool]] = mapped_column("delegateDocumentOwnership")
    include_sender_details: Mapped[Optional[bool]] = mapped_column("includeSenderDetails")
    include_signing_certificate: Mapped[Optional[bool]] = mapped_column("includeSigningCertificate")
    include_audit_log: Mapped[Optional[bool]] = mapped_column("includeAuditLog")
    typed_signature_enabled: Mapped[Optional[bool]] = mapped_column("typedSignatureEnabled")
    upload_signature_enabled: Mapped[Optional[bool]] = mapped_column("uploadSignatureEnabled")
    draw_signature_enabled: Mapped[Optional[bool]] = mapped_column("drawSignatureEnabled")
    default_recipients: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultRecipients", JSONB(none_as_null=True))
    email_id: Mapped[Optional[str]] = mapped_column("emailId", ForeignKey("OrganisationEmail.id", ondelete="SET NULL", onupdate="CASCADE", name="TeamGlobalSettings_emailId_fkey"))
    email_reply_to: Mapped[Optional[str]] = mapped_column("emailReplyTo")
    email_document_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("emailDocumentSettings", JSONB(none_as_null=True))
    branding_enabled: Mapped[Optional[bool]] = mapped_column("brandingEnabled")
    branding_logo: Mapped[Optional[str]] = mapped_column("brandingLogo")
    branding_url: Mapped[Optional[str]] = mapped_column("brandingUrl")
    branding_company_details: Mapped[Optional[str]] = mapped_column("brandingCompanyDetails")
    branding_colors: Mapped[Optional[dict[str, Any]]] = mapped_column("brandingColors", JSONB(none_as_null=True))
    branding_css: Mapped[Optional[str]] = mapped_column("brandingCss")
    envelope_expiration_period: Mapped[Optional[dict[str, Any]]] = mapped_column("envelopeExpirationPeriod", JSONB(none_as_null=True))
    reminder_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("reminderSettings", JSONB(none_as_null=True))
    ai_features_enabled: Mapped[Optional[bool]] = mapped_column("aiFeaturesEnabled")

    email: Mapped[Optional["OrganisationEmail"]] = relationship(back_populates="team_global_settings")
    team: Mapped[Optional["Team"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team_global_settings")

class Team(Base):
    __tablename__ = "Team"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    url: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    avatar_image_id: Mapped[Optional[str]] = mapped_column("avatarImageId", ForeignKey("AvatarImage.id", ondelete="SET NULL", onupdate="CASCADE", name="Team_avatarImageId_fkey"))
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="Team_organisationId_fkey"))
    team_global_settings_id: Mapped[str] = mapped_column("teamGlobalSettingsId", ForeignKey("TeamGlobalSettings.id", ondelete="CASCADE", onupdate="CASCADE", name="Team_teamGlobalSettingsId_fkey"))

    __table_args__ = (
        Index("Team_url_key", "url", unique=True),
        Index("Team_teamGlobalSettingsId_key", "teamGlobalSettingsId", unique=True),
        Index("Team_name_idx", "name"),
        Index("Team_organisationId_idx", "organisationId"),
    )

    avatar_image: Mapped[Optional["AvatarImage"]] = relationship(back_populates="team")
    organisation: Mapped["Organisation"] = relationship(back_populates="teams")
    team_global_settings: Mapped["TeamGlobalSettings"] = relationship(back_populates="team")
    envelopes: Mapped[list["Envelope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    folders: Mapped[list["Folder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    api_tokens: Mapped[list["ApiToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    team_groups: Mapped[list["TeamGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    team_email: Mapped[Optional["TeamEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    email_verification: Mapped[Optional["TeamEmailVerification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    profile: Mapped[Optional["TeamProfile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class TeamEmail(Base):
    __tablename__ = "TeamEmail"

    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamEmail_teamId_fkey"), primary_key=True, autoincrement=False)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    name: Mapped[str]
    email: Mapped[str]

    __table_args__ = (
        Index("TeamEmail_teamId_key", "teamId", unique=True),
        Index("TeamEmail_email_key", "email", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="team_email")

class TeamEmailVerification(Base):
    __tablename__ = "TeamEmailVerification"

    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamEmailVerification_teamId_fkey"), primary_key=True, autoincrement=False)
    name: Mapped[str]
    email: Mapped[str]
    token: Mapped[str]
    completed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("TeamEmailVerification_teamId_key", "teamId", unique=True),
        Index("TeamEmailVerification_token_key", "token", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="email_verification")

class TemplateDirectLink(Base):
    __tablename__ = "TemplateDirectLink"

    id: Mapped[str] = mapped_column(primary_key=True)
    envelope_id: Mapped[str] = mapped_column("envelopeId", ForeignKey("Envelope.id", ondelete="CASCADE", onupdate="CASCADE", name="TemplateDirectLink_envelopeId_fkey"))
    token: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    enabled: Mapped[bool]
    direct_template_recipient_id: Mapped[int] = mapped_column("directTemplateRecipientId")

    __table_args__ = (
        Index("TemplateDirectLink_id_key", "id", unique=True),
        Index("TemplateDirectLink_envelopeId_key", "envelopeId", unique=True),
        Index("TemplateDirectLink_token_key", "token", unique=True),
    )

    envelope: Mapped["Envelope"] = relationship(back_populates="direct_link")

class SiteSettings(Base):
    __tablename__ = "SiteSettings"

    id: Mapped[str] = mapped_column(primary_key=True)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    last_modified_by_user_id: Mapped[Optional[int]] = mapped_column("lastModifiedByUserId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="SiteSettings_lastModifiedByUserId_fkey"))
    last_modified_at: Mapped[datetime] = mapped_column("lastModifiedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    last_modified_by_user: Mapped[Optional["User"]] = relationship(back_populates="site_settings")

class BackgroundJob(Base):
    __tablename__ = "BackgroundJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str] = mapped_column(Enum("PENDING", "PROCESSING", "COMPLETED", "FAILED", name="BackgroundJobStatus"), default="PENDING", server_default=text("'PENDING'"))
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    retried: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_retries: Mapped[int] = mapped_column("maxRetries", default=3, server_default=text("3"))
    job_id: Mapped[str] = mapped_column("jobId")
    name: Mapped[str]
    version: Mapped[str]
    submitted_at: Mapped[datetime] = mapped_column("submittedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    last_retried_at: Mapped[Optional[datetime]] = mapped_column("lastRetriedAt")

    tasks: Mapped[list["BackgroundJobTask"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="background_job")

class BackgroundJobTask(Base):
    __tablename__ = "BackgroundJobTask"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", "FAILED", name="BackgroundJobTaskStatus"), default="PENDING", server_default=text("'PENDING'"))
    result: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    retried: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_retries: Mapped[int] = mapped_column("maxRetries", default=3, server_default=text("3"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    job_id: Mapped[str] = mapped_column("jobId", ForeignKey("BackgroundJob.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundJobTask_jobId_fkey"))

    background_job: Mapped["BackgroundJob"] = relationship(back_populates="tasks")

class AvatarImage(Base):
    __tablename__ = "AvatarImage"

    id: Mapped[str] = mapped_column(primary_key=True)
    bytes: Mapped[str]

    team: Mapped[list["Team"]] = relationship(back_populates="avatar_image")
    user: Mapped[list["User"]] = relationship(back_populates="avatar_image")
    organisation: Mapped[list["Organisation"]] = relationship(back_populates="avatar_image")

class EmailDomain(Base):
    __tablename__ = "EmailDomain"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(Enum("PENDING", "ACTIVE", name="EmailDomainStatus"), default="PENDING", server_default=text("'PENDING'"))
    selector: Mapped[str]
    domain: Mapped[str]
    public_key: Mapped[str] = mapped_column("publicKey")
    private_key: Mapped[str] = mapped_column("privateKey")
    last_verified_at: Mapped[Optional[datetime]] = mapped_column("lastVerifiedAt")
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailDomain_organisationId_fkey"))

    __table_args__ = (
        Index("EmailDomain_selector_key", "selector", unique=True),
        Index("EmailDomain_domain_key", "domain", unique=True),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="email_domains")
    emails: Mapped[list["OrganisationEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_domain")

class OrganisationEmail(Base):
    __tablename__ = "OrganisationEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email: Mapped[str]
    email_name: Mapped[str] = mapped_column("emailName")
    email_domain_id: Mapped[str] = mapped_column("emailDomainId", ForeignKey("EmailDomain.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationEmail_emailDomainId_fkey"))
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganisationEmail_organisationId_fkey"))

    __table_args__ = (
        Index("OrganisationEmail_email_key", "email", unique=True),
    )

    email_domain: Mapped["EmailDomain"] = relationship(back_populates="emails")
    organisation: Mapped["Organisation"] = relationship(back_populates="organisation_emails")
    organisation_global_settings: Mapped[list["OrganisationGlobalSettings"]] = relationship(back_populates="email")
    team_global_settings: Mapped[list["TeamGlobalSettings"]] = relationship(back_populates="email")

class EmailTransport(Base):
    __tablename__ = "EmailTransport"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("SMTP_AUTH", "SMTP_API", "RESEND", "MAILCHANNELS", name="EmailTransportType"))
    from_name: Mapped[str] = mapped_column("fromName")
    from_address: Mapped[str] = mapped_column("fromAddress")
    config: Mapped[str]

    subscription_claims: Mapped[list["SubscriptionClaim"]] = relationship(back_populates="email_transport")
    organisation_claims: Mapped[list["OrganisationClaim"]] = relationship(back_populates="email_transport")

class OrganisationAuthenticationPortal(Base):
    __tablename__ = "OrganisationAuthenticationPortal"

    id: Mapped[str] = mapped_column(primary_key=True)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    client_id: Mapped[str] = mapped_column("clientId", default="", server_default=text("''"))
    client_secret: Mapped[str] = mapped_column("clientSecret", default="", server_default=text("''"))
    well_known_url: Mapped[str] = mapped_column("wellKnownUrl", default="", server_default=text("''"))
    default_organisation_role: Mapped[str] = mapped_column("defaultOrganisationRole", Enum("ADMIN", "MANAGER", "MEMBER", name="OrganisationMemberRole"), default="MEMBER", server_default=text("'MEMBER'"))
    auto_provision_users: Mapped[bool] = mapped_column("autoProvisionUsers", default=True, server_default=text("true"))
    allowed_domains: Mapped[list[str]] = mapped_column("allowedDomains", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    allow_personal_organisations: Mapped[bool] = mapped_column("allowPersonalOrganisations", default=False, server_default=text("false"))

    organisation: Mapped[Optional["Organisation"]] = relationship(passive_deletes="all", back_populates="organisation_authentication_portal")

class Counter(Base):
    __tablename__ = "Counter"

    id: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[int]

class EmbedSignature(Base):
    __tablename__ = "EmbedSignature"

    key: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organisation_id: Mapped[str] = mapped_column("organisationId", ForeignKey("Organisation.id", ondelete="CASCADE", onupdate="CASCADE", name="EmbedSignature_organisationId_fkey"))
    recipient_id: Mapped[int] = mapped_column("recipientId")
    signature: Mapped[str]

    __table_args__ = (
        Index("EmbedSignature_organisationId_idx", "organisationId"),
        Index("EmbedSignature_recipientId_idx", "recipientId"),
    )

    organisation: Mapped["Organisation"] = relationship(back_populates="embed_signatures")

class RateLimit(Base):
    __tablename__ = "RateLimit"

    key: Mapped[str] = mapped_column(primary_key=True)
    action: Mapped[str] = mapped_column(primary_key=True)
    bucket: Mapped[datetime] = mapped_column(primary_key=True)
    count: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("RateLimit_createdAt_idx", "createdAt"),
    )
