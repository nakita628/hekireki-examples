from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, Integer, MetaData, Numeric, String, Text, TypeDecorator, text
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


class UtcDateTimeTz(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def __init__(self, precision: Optional[int] = None) -> None:
        super().__init__(timezone=True, precision=precision)

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return value if value is None or value.tzinfo else value.replace(tzinfo=timezone.utc)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.astimezone(timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str] = mapped_column(default="oauth", server_default=text("'oauth'"))
    provider: Mapped[str]
    issuer: Mapped[Optional[str]]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]]
    ext_expires_in: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_issuer_providerAccountId_idx", "issuer", "providerAccountId"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class AccountCredential(Base):
    __tablename__ = "AccountCredential"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="AccountCredential_userId_fkey"))
    provider: Mapped[str]
    password: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    id_token: Mapped[Optional[str]] = mapped_column("idToken")
    access_token_expires_at: Mapped[Optional[datetime]] = mapped_column("accessTokenExpiresAt")
    refresh_token_expires_at: Mapped[Optional[datetime]] = mapped_column("refreshTokenExpiresAt")
    scope: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("AccountCredential_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="account_credentials")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    identifier_id: Mapped[Optional[str]] = mapped_column("identifierId")
    amr: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    started_by_sign_up: Mapped[bool] = mapped_column("startedBySignUp", default=False, server_default=text("false"))
    impersonating: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    actor_user_id: Mapped[Optional[str]] = mapped_column("actorUserId")
    subject_user_id: Mapped[Optional[str]] = mapped_column("subjectUserId")
    impersonation_reason: Mapped[Optional[str]] = mapped_column("impersonationReason")
    impersonation_expires_at: Mapped[Optional[datetime]] = mapped_column("impersonationExpiresAt")
    last_seen_at: Mapped[Optional[datetime]] = mapped_column("lastSeenAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
        Index("Session_identifierId_idx", "identifierId"),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class UserErasure(Base):
    __tablename__ = "UserErasure"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_created_at: Mapped[datetime] = mapped_column("accountCreatedAt")
    erased_at: Mapped[datetime] = mapped_column("erasedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("UserErasure_accountCreatedAt_idx", "accountCreatedAt"),
    )

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[bool] = mapped_column("emailVerified", default=False, server_default=text("false"))
    signup_confirmation_pending: Mapped[bool] = mapped_column("signupConfirmationPending", default=False, server_default=text("false"))
    passkey_signup_claim_hash: Mapped[Optional[str]] = mapped_column("passkeySignupClaimHash")
    image: Mapped[Optional[str]]
    pending_sso_setup: Mapped[bool] = mapped_column("pendingSsoSetup", default=False, server_default=text("false"))
    user_hash_key: Mapped[Optional[str]] = mapped_column("userHashKey")
    two_factor_enabled: Mapped[bool] = mapped_column("twoFactorEnabled", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    last_login_at: Mapped[Optional[datetime]] = mapped_column("lastLoginAt")
    deactivated_at: Mapped[Optional[datetime]] = mapped_column("deactivatedAt")
    last_home_path: Mapped[Optional[str]] = mapped_column("lastHomePath")
    traces_explorer_tour_dismissed_at: Mapped[Optional[datetime]] = mapped_column("tracesExplorerTourDismissedAt")
    langy_code_access_preference: Mapped[Optional[str]] = mapped_column("langyCodeAccessPreference")
    passkey_nudge_dismissed_at: Mapped[Optional[datetime]] = mapped_column("passkeyNudgeDismissedAt")
    join_offer_dismissed_domains: Mapped[list[str]] = mapped_column("joinOfferDismissedDomains", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    notification_preferences: Mapped[dict[str, Any]] = mapped_column("notificationPreferences", JSONB, default=lambda: {}, server_default=text("'{}'"))
    password_reset_required_at: Mapped[Optional[datetime]] = mapped_column("passwordResetRequiredAt")

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    account_credentials: Mapped[list["AccountCredential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factors: Mapped[list["TwoFactor"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    passkeys: Mapped[list["Passkey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    team_memberships: Mapped[list["TeamUser"]] = relationship(passive_deletes="all", back_populates="user")
    org_memberships: Mapped[list["OrganizationUser"]] = relationship(passive_deletes="all", back_populates="user")
    scim_user_resources: Mapped[list["ScimUserResource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    annotation: Mapped[list["Annotation"]] = relationship(back_populates="user")
    share_links: Mapped[list["ShareLink"]] = relationship(back_populates="user")
    workflow: Mapped[list["Workflow"]] = relationship(back_populates="published_by")
    workflow_version: Mapped[list["WorkflowVersion"]] = relationship(passive_deletes="all", back_populates="author")
    annotation_queues: Mapped[list["AnnotationQueueMembers"]] = relationship(passive_deletes="all", back_populates="user")
    assigned_queue_items: Mapped[list["AnnotationQueueItem"]] = relationship(foreign_keys="AnnotationQueueItem.user_id", back_populates="user")
    created_queue_items: Mapped[list["AnnotationQueueItem"]] = relationship(foreign_keys="AnnotationQueueItem.created_by_user_id", back_populates="created_by_user")
    llm_prompt_config_versions: Mapped[list["LlmPromptConfigVersion"]] = relationship(back_populates="author")
    scenarios: Mapped[list["Scenario"]] = relationship(back_populates="last_updated_by")
    invite_requests: Mapped[list["OrganizationInvite"]] = relationship(back_populates="requested_by_user")
    secrets_created: Mapped[list["ProjectSecret"]] = relationship(foreign_keys="ProjectSecret.created_by_id", passive_deletes="all", back_populates="created_by")
    secrets_updated: Mapped[list["ProjectSecret"]] = relationship(foreign_keys="ProjectSecret.updated_by_id", passive_deletes="all", back_populates="updated_by")
    saved_views: Mapped[list["SavedView"]] = relationship(back_populates="user")
    version_tags_created: Mapped[list["PromptTagAssignment"]] = relationship(foreign_keys="PromptTagAssignment.created_by_id", back_populates="created_by")
    version_tags_updated: Mapped[list["PromptTagAssignment"]] = relationship(foreign_keys="PromptTagAssignment.updated_by_id", back_populates="updated_by")
    prompt_tags_created: Mapped[list["PromptTag"]] = relationship(foreign_keys="PromptTag.created_by_id", back_populates="created_by")
    prompt_tags_updated: Mapped[list["PromptTag"]] = relationship(foreign_keys="PromptTag.updated_by_id", back_populates="updated_by")
    group_memberships: Mapped[list["GroupMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    role_bindings: Mapped[list["RoleBinding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scim_external_ids: Mapped[list["ScimExternalId"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_keys: Mapped[list["ApiKey"]] = relationship(passive_deletes="all", back_populates="user")
    gateway_budgets_created: Mapped[list["GatewayBudget"]] = relationship(passive_deletes="all", back_populates="created_by")
    principal_virtual_keys: Mapped[list["VirtualKey"]] = relationship(back_populates="principal_user")
    personal_teams: Mapped[list["Team"]] = relationship(back_populates="owner_user")
    personal_projects: Mapped[list["Project"]] = relationship(back_populates="owner_user")
    routing_policies_created: Mapped[list["RoutingPolicy"]] = relationship(foreign_keys="RoutingPolicy.created_by_id", back_populates="created_by")
    routing_policies_updated: Mapped[list["RoutingPolicy"]] = relationship(foreign_keys="RoutingPolicy.updated_by_id", back_populates="updated_by")
    gateway_guardrails_created: Mapped[list["GatewayGuardrail"]] = relationship(foreign_keys="GatewayGuardrail.created_by_id", back_populates="created_by")
    gateway_guardrails_updated: Mapped[list["GatewayGuardrail"]] = relationship(foreign_keys="GatewayGuardrail.updated_by_id", back_populates="updated_by")
    ingestion_sources_created: Mapped[list["IngestionSource"]] = relationship(back_populates="created_by")
    anomaly_rules_created: Mapped[list["AnomalyRule"]] = relationship(back_populates="created_by")
    pinned_traces: Mapped[list["PinnedTrace"]] = relationship(back_populates="user")
    trace_edit_overlays_created: Mapped[list["TraceEditOverlay"]] = relationship(foreign_keys="TraceEditOverlay.created_by_id", back_populates="created_by")
    trace_edit_overlays_updated: Mapped[list["TraceEditOverlay"]] = relationship(foreign_keys="TraceEditOverlay.updated_by_id", back_populates="updated_by")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    token: Mapped[str]
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
    )

class Identifier(Base):
    __tablename__ = "Identifier"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    provider: Mapped[str]
    value: Mapped[Optional[str]]
    domain: Mapped[Optional[str]]
    identifier_hash: Mapped[Optional[str]] = mapped_column("identifierHash")
    account_id: Mapped[Optional[str]] = mapped_column("accountId")
    provider_id: Mapped[Optional[str]] = mapped_column("providerId")
    issuer: Mapped[Optional[str]]
    provider_account_id: Mapped[Optional[str]] = mapped_column("providerAccountId")
    state: Mapped[str]
    connection_id: Mapped[Optional[str]] = mapped_column("connectionId")
    verified_at: Mapped[Optional[datetime]] = mapped_column("verifiedAt")
    attached_at: Mapped[datetime] = mapped_column("attachedAt")
    detached_at: Mapped[Optional[datetime]] = mapped_column("detachedAt")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Identifier_userId_idx", "userId"),
        Index("Identifier_identifierHash_idx", "identifierHash"),
        Index("Identifier_value_idx", "value"),
        Index("Identifier_domain_idx", "domain"),
        Index("Identifier_provider_providerAccountId_idx", "provider", "providerAccountId"),
        Index("Identifier_issuer_providerAccountId_idx", "issuer", "providerAccountId"),
        Index("Identifier_accountId_idx", "accountId"),
    )

class IdentitySignInSwitch(Base):
    __tablename__ = "IdentitySignInSwitch"

    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    on_identifiers: Mapped[bool] = mapped_column("onIdentifiers")
    pinned: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    switched_at: Mapped[datetime] = mapped_column("switchedAt")
    last_applied_event_id: Mapped[Optional[str]] = mapped_column("lastAppliedEventId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class IdentifierReservation(Base):
    __tablename__ = "IdentifierReservation"

    normalized_value: Mapped[str] = mapped_column("normalizedValue", primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    identifier_id: Mapped[str] = mapped_column("identifierId")
    command_id: Mapped[str] = mapped_column("commandId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("IdentifierReservation_userId_idx", "userId"),
        Index("IdentifierReservation_identifierId_idx", "identifierId"),
    )

class IdentityProjectionCursor(Base):
    __tablename__ = "IdentityProjectionCursor"

    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    accepted_at: Mapped[datetime] = mapped_column("acceptedAt")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class TwoFactor(Base):
    __tablename__ = "TwoFactor"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="TwoFactor_userId_fkey"))
    secret: Mapped[str]
    backup_codes: Mapped[str] = mapped_column("backupCodes")
    verified: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    failed_verification_count: Mapped[int] = mapped_column("failedVerificationCount", default=0, server_default=text("0"))
    locked_until: Mapped[Optional[datetime]] = mapped_column("lockedUntil")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("TwoFactor_userId_idx", "userId"),
        Index("TwoFactor_secret_idx", "secret"),
    )

    user: Mapped["User"] = relationship(back_populates="two_factors")

class SignInAttemptLock(Base):
    __tablename__ = "SignInAttemptLock"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier_hash: Mapped[str] = mapped_column("identifierHash")
    failed_count: Mapped[int] = mapped_column("failedCount", default=0, server_default=text("0"))
    locked_until: Mapped[Optional[datetime]] = mapped_column("lockedUntil")
    consecutive_lockouts: Mapped[int] = mapped_column("consecutiveLockouts", default=0, server_default=text("0"))
    held_for_review: Mapped[bool] = mapped_column("heldForReview", default=False, server_default=text("false"))
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SignInAttemptLock_identifierHash_key", "identifierHash", unique=True),
        Index("SignInAttemptLock_lockedUntil_idx", "lockedUntil"),
        Index("SignInAttemptLock_userId_idx", "userId"),
    )

class Passkey(Base):
    __tablename__ = "Passkey"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    public_key: Mapped[str] = mapped_column("publicKey")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Passkey_userId_fkey"))
    credential_id: Mapped[str] = mapped_column("credentialID")
    counter: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    device_type: Mapped[str] = mapped_column("deviceType")
    backed_up: Mapped[bool] = mapped_column("backedUp", default=False, server_default=text("false"))
    transports: Mapped[Optional[str]]
    aaguid: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Passkey_userId_idx", "userId"),
        Index("Passkey_credentialID_idx", "credentialID"),
    )

    user: Mapped["User"] = relationship(back_populates="passkeys")

class MfaEnrollment(Base):
    __tablename__ = "MfaEnrollment"

    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    enrollment_id: Mapped[Optional[str]] = mapped_column("enrollmentId")
    method: Mapped[Optional[str]]
    state: Mapped[str]
    enrolled_at: Mapped[Optional[datetime]] = mapped_column("enrolledAt")
    confirmed_at: Mapped[Optional[datetime]] = mapped_column("confirmedAt")
    expired_at: Mapped[Optional[datetime]] = mapped_column("expiredAt")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    disabled_via: Mapped[Optional[str]] = mapped_column("disabledVia")
    backup_code_count: Mapped[int] = mapped_column("backupCodeCount", default=0, server_default=text("0"))
    consumed_backup_code_indexes: Mapped[list[int]] = mapped_column("consumedBackupCodeIndexes", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    failed_count: Mapped[int] = mapped_column("failedCount", default=0, server_default=text("0"))
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    accepted_at: Mapped[datetime] = mapped_column("acceptedAt")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("MfaEnrollment_state_idx", "state"),
    )

class SsoConnection(Base):
    __tablename__ = "SsoConnection"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    type: Mapped[str]
    state: Mapped[str]
    claimed_domains: Mapped[list[str]] = mapped_column("claimedDomains", ARRAY(Text), nullable=True)
    domain_claims: Mapped[dict[str, Any]] = mapped_column("domainClaims", JSONB, default=lambda: [], server_default=text("'[]'"))
    approved_domains: Mapped[list[str]] = mapped_column("approvedDomains", ARRAY(Text), nullable=True)
    verified_domains: Mapped[list[str]] = mapped_column("verifiedDomains", ARRAY(Text), nullable=True)
    lapsed_domains: Mapped[list[str]] = mapped_column("lapsedDomains", ARRAY(Text), nullable=True)
    domain_verifications: Mapped[dict[str, Any]] = mapped_column("domainVerifications", JSONB, default=lambda: [], server_default=text("'[]'"))
    pending_verification: Mapped[Optional[dict[str, Any]]] = mapped_column("pendingVerification", JSONB(none_as_null=True))
    idp_metadata: Mapped[dict[str, Any]] = mapped_column("idpMetadata", JSONB)
    arrival_policy: Mapped[str] = mapped_column("arrivalPolicy", default="refuse", server_default=text("'refuse'"))
    allows_jit: Mapped[bool] = mapped_column("allowsJit", default=False, server_default=text("false"))
    arrival_policy_decided_at: Mapped[Optional[datetime]] = mapped_column("arrivalPolicyDecidedAt")
    source: Mapped[str]
    test_login_account_id: Mapped[Optional[str]] = mapped_column("testLoginAccountId")
    replaces_connection_id: Mapped[Optional[str]] = mapped_column("replacesConnectionId")
    migration_phase: Mapped[Optional[str]] = mapped_column("migrationPhase", Enum("SETUP", "GRACE_LEGACY", "GRACE_DIRECT", "FINALIZING", "FINALIZED", name="SsoConnectionMigrationPhase"))
    grace_started_at: Mapped[Optional[datetime]] = mapped_column("graceStartedAt")
    route_changed_at: Mapped[Optional[datetime]] = mapped_column("routeChangedAt")
    finalization_requested_at: Mapped[Optional[datetime]] = mapped_column("finalizationRequestedAt")
    finalized_at: Mapped[Optional[datetime]] = mapped_column("finalizedAt")
    rejection: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy")
    tear_down_after: Mapped[Optional[datetime]] = mapped_column("tearDownAfter")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    accepted_at: Mapped[datetime] = mapped_column("acceptedAt")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("SsoConnection_organizationId_idx", "organizationId"),
        Index("SsoConnection_organizationId_migrationPhase_idx", "organizationId", "migrationPhase"),
        Index("SsoConnection_state_idx", "state"),
        Index("SsoConnection_verifiedDomains_idx", "verifiedDomains"),
        Index("SsoConnection_lapsedDomains_idx", "lapsedDomains"),
    )

    reproof_cursor: Mapped[Optional["SsoConnectionReproofCursor"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="connection")

class SsoConnectionRegistrationSlot(Base):
    __tablename__ = "SsoConnectionRegistrationSlot"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    kind: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    replaces_connection_id: Mapped[Optional[str]] = mapped_column("replacesConnectionId")
    command_id: Mapped[str] = mapped_column("commandId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SsoConnectionRegistrationSlot_connectionId_key", "connectionId", unique=True),
    )

class SsoConnectionReproofCursor(Base):
    __tablename__ = "SsoConnectionReproofCursor"

    connection_id: Mapped[str] = mapped_column("connectionId", ForeignKey("SsoConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="SsoConnectionReproofCursor_connectionId_fkey"), primary_key=True)
    last_reproof_at: Mapped[datetime] = mapped_column("lastReproofAt")

    __table_args__ = (
        Index("SsoConnectionReproofCursor_lastReproofAt_idx", "lastReproofAt"),
    )

    connection: Mapped["SsoConnection"] = relationship(back_populates="reproof_cursor")

class SsoCredential(Base):
    __tablename__ = "SsoCredential"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    connection_id: Mapped[str] = mapped_column("connectionId")
    kind: Mapped[str]
    ciphertext: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("SsoCredential_organizationId_connectionId_idx", "organizationId", "connectionId"),
    )

class SsoVerifiedDomain(Base):
    __tablename__ = "SsoVerifiedDomain"

    domain: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("SsoVerifiedDomain_organizationId_idx", "organizationId"),
        Index("SsoVerifiedDomain_domain_organizationId_key", "domain", "organizationId", unique=True),
    )

    holders: Mapped[list["SsoVerifiedDomainHolder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="ownership")

class SsoVerifiedDomainHolder(Base):
    __tablename__ = "SsoVerifiedDomainHolder"

    domain: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("SsoVerifiedDomainHolder_connectionId_idx", "connectionId"),
        Index("SsoVerifiedDomainHolder_organizationId_idx", "organizationId"),
        ForeignKeyConstraint(["domain", "organizationId"], ["SsoVerifiedDomain.domain", "SsoVerifiedDomain.organizationId"], ondelete="CASCADE", onupdate="CASCADE", name="SsoVerifiedDomainHolder_domain_organizationId_fkey"),
    )

    ownership: Mapped["SsoVerifiedDomain"] = relationship(back_populates="holders")

class SsoAuthenticationActivity(Base):
    __tablename__ = "SsoAuthenticationActivity"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    connection_id: Mapped[str] = mapped_column("connectionId")
    user_id: Mapped[str] = mapped_column("userId")
    authenticated_at: Mapped[datetime] = mapped_column("authenticatedAt")
    provider_account_id: Mapped[Optional[str]] = mapped_column("providerAccountId")

    __table_args__ = (
        Index("SsoAuthenticationActivity_organizationId_connectionId_authe_idx", "organizationId", "connectionId", "authenticatedAt"),
        Index("SsoAuthenticationActivity_organizationId_userId_idx", "organizationId", "userId"),
    )

class SsoProvider(Base):
    __tablename__ = "SsoProvider"

    id: Mapped[str] = mapped_column(primary_key=True)
    issuer: Mapped[str]
    oidc_config: Mapped[Optional[str]] = mapped_column("oidcConfig")
    saml_config: Mapped[Optional[str]] = mapped_column("samlConfig")
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    provider_id: Mapped[str] = mapped_column("providerId")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    domain: Mapped[str]

    __table_args__ = (
        Index("SsoProvider_providerId_key", "providerId", unique=True),
        Index("SsoProvider_organizationId_idx", "organizationId"),
        Index("SsoProvider_domain_idx", "domain"),
    )

class SsoBreakGlassBinding(Base):
    __tablename__ = "SsoBreakGlassBinding"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    user_id: Mapped[str] = mapped_column("userId")
    granted_by_user_id: Mapped[str] = mapped_column("grantedByUserId")
    granted_at: Mapped[datetime] = mapped_column("grantedAt")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    superseded_at: Mapped[Optional[datetime]] = mapped_column("supersededAt")
    renewed_from_id: Mapped[Optional[str]] = mapped_column("renewedFromId")
    warned_days: Mapped[list[int]] = mapped_column("warnedDays", ARRAY(Integer), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("SsoBreakGlassBinding_organizationId_expiresAt_idx", "organizationId", "expiresAt"),
        Index("SsoBreakGlassBinding_userId_idx", "userId"),
    )

class SsoActivationRecoveryReservation(Base):
    __tablename__ = "SsoActivationRecoveryReservation"

    command_id: Mapped[str] = mapped_column("commandId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    connection_id: Mapped[str] = mapped_column("connectionId")
    created_at: Mapped[datetime] = mapped_column("createdAt")

    __table_args__ = (
        Index("SsoActivationRecoveryReservation_organizationId_idx", "organizationId"),
        Index("SsoActivationRecoveryReservation_organizationId_connectionI_key", "organizationId", "connectionId", unique=True),
    )

class JoinRequest(Base):
    __tablename__ = "JoinRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    domain: Mapped[str]
    state: Mapped[str]
    matched_via: Mapped[str] = mapped_column("matchedVia")
    origin: Mapped[str] = mapped_column(default="web", server_default=text("'web'"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")
    resolved_by_type: Mapped[Optional[str]] = mapped_column("resolvedByType")
    resolved_by_id: Mapped[Optional[str]] = mapped_column("resolvedById")
    withdrawal_cause: Mapped[Optional[str]] = mapped_column("withdrawalCause")
    connection_id: Mapped[Optional[str]] = mapped_column("connectionId")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    accepted_at: Mapped[datetime] = mapped_column("acceptedAt")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("JoinRequest_organizationId_state_idx", "organizationId", "state"),
        Index("JoinRequest_userId_state_idx", "userId", "state"),
        Index("JoinRequest_userId_organizationId_idx", "userId", "organizationId"),
        Index("JoinRequest_domain_idx", "domain"),
    )

class TeamUser(Base):
    __tablename__ = "TeamUser"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="TeamUser_userId_fkey"), primary_key=True)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="RESTRICT", onupdate="CASCADE", name="TeamUser_teamId_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", "VIEWER", "CUSTOM", name="TeamUserRole"))
    assigned_role_id: Mapped[Optional[str]] = mapped_column("assignedRoleId", ForeignKey("CustomRole.id", ondelete="SET NULL", onupdate="CASCADE", name="TeamUser_assignedRoleId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("TeamUser_teamId_idx", "teamId"),
        Index("TeamUser_userId_idx", "userId"),
        Index("TeamUser_assignedRoleId_idx", "assignedRoleId"),
    )

    assigned_role: Mapped[Optional["CustomRole"]] = relationship(back_populates="assigned_users")
    user: Mapped["User"] = relationship(back_populates="team_memberships")
    team: Mapped["Team"] = relationship(back_populates="members")

class OrganizationUser(Base):
    __tablename__ = "OrganizationUser"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrganizationUser_userId_fkey"), primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrganizationUser_organizationId_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", "EXTERNAL", "DEVELOPER", name="OrganizationUserRole"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    department_id: Mapped[Optional[str]] = mapped_column("departmentId")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    pending_sso_grant_id: Mapped[Optional[str]] = mapped_column("pendingSsoGrantId")
    membership_stamp: Mapped[str] = mapped_column("membershipStamp", server_default=text("gen_random_uuid()::text"))
    full_seat_since: Mapped[Optional[datetime]] = mapped_column("fullSeatSince")
    seat_pending_since: Mapped[Optional[datetime]] = mapped_column("seatPendingSince")

    __table_args__ = (
        Index("OrganizationUser_organizationId_idx", "organizationId"),
        Index("OrganizationUser_userId_idx", "userId"),
        Index("OrganizationUser_departmentId_idx", "departmentId"),
    )

    user: Mapped["User"] = relationship(back_populates="org_memberships")
    organization: Mapped["Organization"] = relationship(back_populates="members")

class SeatHold(Base):
    __tablename__ = "SeatHold"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    user_id: Mapped[str] = mapped_column("userId")
    email: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    seat_taken_at: Mapped[datetime] = mapped_column("seatTakenAt")
    held_until: Mapped[datetime] = mapped_column("heldUntil")
    reason: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("SeatHold_heldUntil_idx", "heldUntil"),
        Index("SeatHold_organizationId_userId_key", "organizationId", "userId", unique=True),
    )

class SeatRequest(Base):
    __tablename__ = "SeatRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    email: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    name: Mapped[Optional[str]]
    status: Mapped[str]
    source: Mapped[str]
    idp_refused: Mapped[bool] = mapped_column("idpRefused", default=False, server_default=text("false"))
    idp_kind: Mapped[Optional[str]] = mapped_column("idpKind")
    sales_notified_at: Mapped[Optional[datetime]] = mapped_column("salesNotifiedAt")
    opened_at: Mapped[datetime] = mapped_column("openedAt")
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")
    resolved_by_user_id: Mapped[Optional[str]] = mapped_column("resolvedByUserId")
    seat_pending_since: Mapped[Optional[datetime]] = mapped_column("seatPendingSince")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SeatRequest_organizationId_status_idx", "organizationId", "status"),
        Index("SeatRequest_userId_idx", "userId"),
        Index("SeatRequest_organizationId_email_key", "organizationId", "email", unique=True),
    )

class OrganizationSeatLease(Base):
    __tablename__ = "OrganizationSeatLease"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    holder: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class OrganizationAccessNotice(Base):
    __tablename__ = "OrganizationAccessNotice"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    kind: Mapped[str]
    email: Mapped[str]
    name: Mapped[Optional[str]]
    idp: Mapped[Optional[str]]
    day_key: Mapped[str] = mapped_column("dayKey")
    idp_refused: Mapped[bool] = mapped_column("idpRefused", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    mailed_at: Mapped[Optional[datetime]] = mapped_column("mailedAt")

    __table_args__ = (
        Index("OrganizationAccessNotice_organizationId_kind_email_dayKey_key", "organizationId", "kind", "email", "dayKey", unique=True),
    )

class Team(Base):
    __tablename__ = "Team"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Team_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    is_personal: Mapped[bool] = mapped_column("isPersonal", default=False, server_default=text("false"))
    owner_user_id: Mapped[Optional[str]] = mapped_column("ownerUserId", String(255), ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Team_ownerUserId_fkey"))
    department_id: Mapped[Optional[str]] = mapped_column("departmentId")

    __table_args__ = (
        Index("Team_slug_key", "slug", unique=True),
        Index("Team_organizationId_idx", "organizationId"),
        Index("Team_isPersonal_idx", "isPersonal"),
        Index("Team_departmentId_idx", "departmentId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="teams")
    owner_user: Mapped[Optional["User"]] = relationship(back_populates="personal_teams")
    members: Mapped[list["TeamUser"]] = relationship(passive_deletes="all", back_populates="team")
    projects: Mapped[list["Project"]] = relationship(passive_deletes="all", back_populates="team")
    ingestion_sources: Mapped[list["IngestionSource"]] = relationship(back_populates="team")
    ai_tool_entry_teams: Mapped[list["AiToolEntryTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class Organization(Base):
    __tablename__ = "Organization"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    kind: Mapped[str] = mapped_column(default="customer", server_default=text("'customer'"))
    phone_number: Mapped[Optional[str]] = mapped_column("phoneNumber")
    slug: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    usage_spending_max_limit: Mapped[Optional[int]] = mapped_column("usageSpendingMaxLimit")
    dataset_attachment_max_mb: Mapped[Optional[int]] = mapped_column("datasetAttachmentMaxMb")
    private_click_house_label: Mapped[Optional[str]] = mapped_column("privateClickHouseLabel")
    private_s3_label: Mapped[Optional[str]] = mapped_column("privateS3Label")
    private_dataplane_seen_at: Mapped[Optional[datetime]] = mapped_column("privateDataplaneSeenAt")
    max_session_duration_days: Mapped[int] = mapped_column("maxSessionDurationDays", default=0, server_default=text("0"))
    mfa_required: Mapped[bool] = mapped_column("mfaRequired", default=False, server_default=text("false"))
    lockout_after_failed_attempts: Mapped[int] = mapped_column("lockoutAfterFailedAttempts", default=0, server_default=text("0"))
    lockout_minutes: Mapped[int] = mapped_column("lockoutMinutes", default=30, server_default=text("30"))
    session_idle_timeout_minutes: Mapped[int] = mapped_column("sessionIdleTimeoutMinutes", default=1440, server_default=text("1440"))
    session_max_lifetime_minutes: Mapped[int] = mapped_column("sessionMaxLifetimeMinutes", default=0, server_default=text("0"))
    signup_data: Mapped[Optional[dict[str, Any]]] = mapped_column("signupData", JSONB(none_as_null=True))
    signed_dpa: Mapped[bool] = mapped_column("signedDPA", default=False, server_default=text("false"))
    elasticsearch_node_url: Mapped[Optional[str]] = mapped_column("elasticsearchNodeUrl")
    elasticsearch_api_key: Mapped[Optional[str]] = mapped_column("elasticsearchApiKey")
    use_custom_elasticsearch: Mapped[bool] = mapped_column("useCustomElasticsearch", default=False, server_default=text("false"))
    s3_endpoint: Mapped[Optional[str]] = mapped_column("s3Endpoint")
    s3_access_key_id: Mapped[Optional[str]] = mapped_column("s3AccessKeyId")
    s3_secret_access_key: Mapped[Optional[str]] = mapped_column("s3SecretAccessKey")
    s3_bucket: Mapped[Optional[str]] = mapped_column("s3Bucket")
    use_custom_s3: Mapped[bool] = mapped_column("useCustomS3", default=False, server_default=text("false"))
    sent_plan_limit_alert: Mapped[Optional[datetime]] = mapped_column("sentPlanLimitAlert")
    sso_domain: Mapped[Optional[str]] = mapped_column("ssoDomain")
    sso_provider: Mapped[Optional[str]] = mapped_column("ssoProvider")
    domain_join: Mapped[str] = mapped_column("domainJoin", default="request", server_default=text("'request'"))
    joiner_role: Mapped[str] = mapped_column("joinerRole", Enum("ADMIN", "MEMBER", "EXTERNAL", "DEVELOPER", name="OrganizationUserRole"), default="MEMBER", server_default=text("'MEMBER'"))
    join_domains: Mapped[list[str]] = mapped_column("joinDomains", ARRAY(Text), nullable=True)
    presence_enabled: Mapped[bool] = mapped_column("presenceEnabled", default=True, server_default=text("true"))
    trace_sharing_enabled: Mapped[bool] = mapped_column("traceSharingEnabled", default=True, server_default=text("true"))
    support_contact: Mapped[Optional[str]] = mapped_column("supportContact")
    primary_intent: Mapped[Optional[str]] = mapped_column("primaryIntent", Enum("AGENT_GOVERNANCE", "LLM_OPS", name="OrganizationIntent"))
    promo_code: Mapped[Optional[str]] = mapped_column("promoCode")
    stripe_customer_id: Mapped[Optional[str]] = mapped_column("stripeCustomerId")
    currency: Mapped[str] = mapped_column(Enum("USD", "EUR", name="Currency"), default="EUR", server_default=text("'EUR'"))
    pricing_model: Mapped[str] = mapped_column("pricingModel", Enum("TIERED", "SEAT_EVENT", name="PricingModel"), default="TIERED", server_default=text("'TIERED'"))
    license: Mapped[Optional[str]]
    license_expires_at: Mapped[Optional[datetime]] = mapped_column("licenseExpiresAt")
    license_last_validated_at: Mapped[Optional[datetime]] = mapped_column("licenseLastValidatedAt")
    self_hosted_customer: Mapped[bool] = mapped_column("selfHostedCustomer", default=False, server_default=text("false"))
    connect_services_disabled: Mapped[list[str]] = mapped_column("connectServicesDisabled", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    instant_evals_enabled_at: Mapped[Optional[datetime]] = mapped_column("instantEvalsEnabledAt")
    instant_evals_enabled_by_user_id: Mapped[Optional[str]] = mapped_column("instantEvalsEnabledByUserId")
    connect_last_sync_at: Mapped[Optional[datetime]] = mapped_column("connectLastSyncAt")
    connect_last_sync_error: Mapped[Optional[str]] = mapped_column("connectLastSyncError")

    __table_args__ = (
        Index("Organization_slug_key", "slug", unique=True),
        Index("Organization_ssoDomain_key", "ssoDomain", unique=True),
        Index("Organization_stripeCustomerId_key", "stripeCustomerId", unique=True),
        Index("Organization_ssoProvider_idx", "ssoProvider"),
    )

    members: Mapped[list["OrganizationUser"]] = relationship(passive_deletes="all", back_populates="organization")
    teams: Mapped[list["Team"]] = relationship(passive_deletes="all", back_populates="organization")
    webhook_endpoints: Mapped[list["WebhookEndpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    organization_invite: Mapped[list["OrganizationInvite"]] = relationship(passive_deletes="all", back_populates="organization")
    custom_roles: Mapped[list["CustomRole"]] = relationship(passive_deletes="all", back_populates="organization")
    notifications: Mapped[list["Notification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    subscriptions: Mapped[list["Subscription"]] = relationship(passive_deletes="all", back_populates="organization")
    scim_tokens: Mapped[list["ScimToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    scim_request_log: Mapped[list["ScimRequestLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    prompt_tags: Mapped[list["PromptTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    groups: Mapped[list["Group"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    role_bindings: Mapped[list["RoleBinding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    api_keys: Mapped[list["ApiKey"]] = relationship(passive_deletes="all", back_populates="organization")
    issued_licenses: Mapped[list["IssuedLicense"]] = relationship(passive_deletes="all", back_populates="organization")
    gateway_change_events: Mapped[list["GatewayChangeEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    routing_policies: Mapped[list["RoutingPolicy"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    ingestion_sources: Mapped[list["IngestionSource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    anomaly_rules: Mapped[list["AnomalyRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    anomaly_alerts: Mapped[list["AnomalyAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    ai_tool_entries: Mapped[list["AiToolEntry"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    ingestion_templates: Mapped[list["IngestionTemplate"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    github_installations: Mapped[list["GithubInstallation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    github_pull_requests: Mapped[list["GithubPullRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    github_branch_pr_checks: Mapped[list["GithubBranchPullRequestCheck"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    connected_billing: Mapped[Optional["ConnectedBillingAccount"]] = relationship(passive_deletes="all", back_populates="organization")

class IssuedLicense(Base):
    __tablename__ = "IssuedLicense"

    id: Mapped[str] = mapped_column(primary_key=True)
    license_id: Mapped[str] = mapped_column("licenseId")
    token_hash: Mapped[str] = mapped_column("tokenHash")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="IssuedLicense_organizationId_fkey"))
    organization_name: Mapped[str] = mapped_column("organizationName")
    email: Mapped[str]
    plan_type: Mapped[str] = mapped_column("planType")
    max_members: Mapped[int] = mapped_column("maxMembers")
    max_members_lite: Mapped[Optional[int]] = mapped_column("maxMembersLite")
    lite_seats_unlimited: Mapped[bool] = mapped_column("liteSeatsUnlimited", default=False, server_default=text("false"))
    max_members_developer: Mapped[Optional[int]] = mapped_column("maxMembersDeveloper")
    issued_at: Mapped[datetime] = mapped_column("issuedAt")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    source: Mapped[str] = mapped_column(Enum("BACKOFFICE", "PURCHASE", "SCRIPT", "LEGACY_IMPORT", name="IssuedLicenseSource"))
    issued_by_id: Mapped[Optional[str]] = mapped_column("issuedById")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_by_id: Mapped[Optional[str]] = mapped_column("revokedById")
    revoked_reason: Mapped[Optional[str]] = mapped_column("revokedReason")
    superseded_at: Mapped[Optional[datetime]] = mapped_column("supersededAt")
    replaces_id: Mapped[Optional[str]] = mapped_column("replacesId")
    pending_delivery_license: Mapped[Optional[str]] = mapped_column("pendingDeliveryLicense")
    services: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    seat_rate_cents: Mapped[Optional[int]] = mapped_column("seatRateCents")
    seat_currency: Mapped[Optional[str]] = mapped_column("seatCurrency", Enum("USD", "EUR", name="Currency"))
    commit_usd_cents: Mapped[int] = mapped_column("commitUsdCents", default=0, server_default=text("0"))
    overage_enabled: Mapped[bool] = mapped_column("overageEnabled", default=False, server_default=text("false"))
    overage_max_usd_cents: Mapped[Optional[int]] = mapped_column("overageMaxUsdCents")
    instance_id: Mapped[Optional[str]] = mapped_column("instanceId")
    instance_bound_at: Mapped[Optional[datetime]] = mapped_column("instanceBoundAt")
    last_sync_at: Mapped[Optional[datetime]] = mapped_column("lastSyncAt")
    last_sync_version: Mapped[Optional[str]] = mapped_column("lastSyncVersion")
    reported_members: Mapped[Optional[int]] = mapped_column("reportedMembers")
    reported_members_lite: Mapped[Optional[int]] = mapped_column("reportedMembersLite")
    virtual_key_id: Mapped[Optional[str]] = mapped_column("virtualKeyId")
    seats_raised_from: Mapped[Optional[int]] = mapped_column("seatsRaisedFrom")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("IssuedLicense_licenseId_key", "licenseId", unique=True),
        Index("IssuedLicense_tokenHash_key", "tokenHash", unique=True),
        Index("IssuedLicense_replacesId_key", "replacesId", unique=True),
        Index("IssuedLicense_virtualKeyId_key", "virtualKeyId", unique=True),
        Index("IssuedLicense_organizationId_idx", "organizationId"),
        Index("IssuedLicense_expiresAt_idx", "expiresAt"),
        Index("IssuedLicense_seatsRaisedFrom_idx", "seatsRaisedFrom"),
    )

    organization: Mapped[Optional["Organization"]] = relationship(back_populates="issued_licenses")

class OrganizationLicense(Base):
    __tablename__ = "OrganizationLicense"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    license_key: Mapped[Optional[str]] = mapped_column("licenseKey")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    validated_at: Mapped[Optional[datetime]] = mapped_column("validatedAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class ActivationCode(Base):
    __tablename__ = "ActivationCode"

    id: Mapped[str] = mapped_column(primary_key=True)
    code_hash: Mapped[str] = mapped_column("codeHash")
    code_hint: Mapped[str] = mapped_column("codeHint")
    organization_id: Mapped[str] = mapped_column("organizationId")
    organization_name: Mapped[str] = mapped_column("organizationName")
    email: Mapped[str]
    plan_type: Mapped[str] = mapped_column("planType")
    max_members: Mapped[int] = mapped_column("maxMembers")
    max_members_lite: Mapped[Optional[int]] = mapped_column("maxMembersLite", default=0, server_default=text("0"))
    lite_seats_unlimited: Mapped[bool] = mapped_column("liteSeatsUnlimited", default=False, server_default=text("false"))
    max_members_developer: Mapped[Optional[int]] = mapped_column("maxMembersDeveloper")
    license_term_days: Mapped[int] = mapped_column("licenseTermDays")
    services: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    reusable: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    redeemed_at: Mapped[Optional[datetime]] = mapped_column("redeemedAt")
    redeemed_by_instance_id: Mapped[Optional[str]] = mapped_column("redeemedByInstanceId")
    issued_license_id: Mapped[Optional[str]] = mapped_column("issuedLicenseId")
    redemption_count: Mapped[int] = mapped_column("redemptionCount", default=0, server_default=text("0"))
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_by_id: Mapped[Optional[str]] = mapped_column("revokedById")
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ActivationCode_codeHash_key", "codeHash", unique=True),
        Index("ActivationCode_organizationId_idx", "organizationId"),
        Index("ActivationCode_expiresAt_idx", "expiresAt"),
    )

class ConnectedBillingAccount(Base):
    __tablename__ = "ConnectedBillingAccount"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="ConnectedBillingAccount_organizationId_fkey"))
    stripe_customer_id: Mapped[str] = mapped_column("stripeCustomerId")
    usage_subscription_id: Mapped[Optional[str]] = mapped_column("usageSubscriptionId")
    usage_subscription_item_id: Mapped[Optional[str]] = mapped_column("usageSubscriptionItemId")
    term_starts_at: Mapped[datetime] = mapped_column("termStartsAt")
    term_ends_at: Mapped[datetime] = mapped_column("termEndsAt")
    commit_usd_cents: Mapped[int] = mapped_column("commitUsdCents", default=0, server_default=text("0"))
    seat_currency: Mapped[str] = mapped_column("seatCurrency", Enum("USD", "EUR", name="Currency"))
    seat_rate_cents: Mapped[int] = mapped_column("seatRateCents")
    seats: Mapped[int]
    bank_transfer_type: Mapped[Optional[str]] = mapped_column("bankTransferType")
    bank_transfer_country: Mapped[Optional[str]] = mapped_column("bankTransferCountry")
    billing_email: Mapped[str] = mapped_column("billingEmail")
    pending_renewal: Mapped[Optional[dict[str, Any]]] = mapped_column("pendingRenewal", JSONB(none_as_null=True))
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ConnectedBillingAccount_organizationId_key", "organizationId", unique=True),
        Index("ConnectedBillingAccount_stripeCustomerId_key", "stripeCustomerId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="connected_billing")

class ConnectedCreditGrant(Base):
    __tablename__ = "ConnectedCreditGrant"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_id: Mapped[str] = mapped_column("accountId")
    stripe_credit_grant_id: Mapped[str] = mapped_column("stripeCreditGrantId")
    amount_usd_cents: Mapped[int] = mapped_column("amountUsdCents")
    kind: Mapped[str] = mapped_column(Enum("commit", "added", "renewal", name="ConnectedCreditGrantKind"))
    term_ends_at: Mapped[datetime] = mapped_column("termEndsAt")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConnectedCreditGrant_stripeCreditGrantId_key", "stripeCreditGrantId", unique=True),
        Index("ConnectedCreditGrant_accountId_idx", "accountId"),
    )

class ConnectedInvoice(Base):
    __tablename__ = "ConnectedInvoice"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_id: Mapped[str] = mapped_column("accountId")
    stripe_invoice_id: Mapped[str] = mapped_column("stripeInvoiceId")
    kind: Mapped[str] = mapped_column(Enum("annual", "seat_change", "usage", name="ConnectedInvoiceKind"))
    currency: Mapped[str] = mapped_column(Enum("USD", "EUR", name="Currency"))
    amount_cents: Mapped[int] = mapped_column("amountCents")
    status: Mapped[str]
    paid_out_of_band_at: Mapped[Optional[datetime]] = mapped_column("paidOutOfBandAt")
    term_starts_at: Mapped[Optional[datetime]] = mapped_column("termStartsAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ConnectedInvoice_stripeInvoiceId_key", "stripeInvoiceId", unique=True),
        Index("ConnectedInvoice_accountId_idx", "accountId"),
    )

class ScimRequestLog(Base):
    __tablename__ = "ScimRequestLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="ScimRequestLog_organizationId_fkey"))
    connection_id: Mapped[Optional[str]] = mapped_column("connectionId")
    method: Mapped[str]
    resource: Mapped[str]
    status: Mapped[int]
    reason: Mapped[Optional[str]]
    detail: Mapped[Optional[str]]
    occurred_at: Mapped[datetime] = mapped_column("occurredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ScimRequestLog_organizationId_occurredAt_idx", "organizationId", "occurredAt"),
        Index("ScimRequestLog_connectionId_occurredAt_idx", "connectionId", "occurredAt"),
        Index("ScimRequestLog_occurredAt_idx", "occurredAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="scim_request_log")

class ScimToken(Base):
    __tablename__ = "ScimToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="ScimToken_organizationId_fkey"))
    connection_id: Mapped[Optional[str]] = mapped_column("connectionId")
    hashed_token: Mapped[str] = mapped_column("hashedToken")
    hash_scheme: Mapped[str] = mapped_column("hashScheme", default="sha256", server_default=text("'sha256'"))
    description: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")

    __table_args__ = (
        Index("ScimToken_hashedToken_key", "hashedToken", unique=True),
        Index("ScimToken_organizationId_idx", "organizationId"),
        Index("ScimToken_connectionId_idx", "connectionId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="scim_tokens")

class ScimExternalId(Base):
    __tablename__ = "ScimExternalId"

    organization_id: Mapped[str] = mapped_column("organizationId")
    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    external_id: Mapped[str] = mapped_column("externalId")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ScimExternalId_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ScimExternalId_organizationId_userId_idx", "organizationId", "userId"),
        Index("ScimExternalId_userId_idx", "userId"),
        Index("ScimExternalId_connectionId_idx", "connectionId"),
        Index("ScimExternalId_connectionId_externalId_key", "connectionId", "externalId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="scim_external_ids")

class ScimUserResource(Base):
    __tablename__ = "ScimUserResource"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ScimUserResource_userId_fkey"), primary_key=True)
    user_name: Mapped[str] = mapped_column("userName")
    name: Mapped[Optional[str]]
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ScimUserResource_organizationId_userName_idx", "organizationId", "userName"),
        Index("ScimUserResource_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="scim_user_resources")

class ScimDirectoryUser(Base):
    __tablename__ = "ScimDirectoryUser"

    organization_id: Mapped[str] = mapped_column("organizationId")
    connection_id: Mapped[str] = mapped_column("connectionId", primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", primary_key=True)

    __table_args__ = (
        Index("ScimDirectoryUser_organizationId_userId_idx", "organizationId", "userId"),
    )

class ScimSyncState(Base):
    __tablename__ = "ScimSyncState"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    state: Mapped[str]
    last_pushed_at: Mapped[Optional[datetime]] = mapped_column("lastPushedAt")
    last_failure: Mapped[Optional[dict[str, Any]]] = mapped_column("lastFailure", JSONB(none_as_null=True))
    dead_letters: Mapped[dict[str, Any]] = mapped_column("deadLetters", JSONB, default=lambda: [], server_default=text("'[]'"))
    revoked_cause: Mapped[Optional[str]] = mapped_column("revokedCause")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    accepted_at: Mapped[datetime] = mapped_column("acceptedAt")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("ScimSyncState_organizationId_idx", "organizationId"),
        Index("ScimSyncState_state_idx", "state"),
        Index("ScimSyncState_connectionId_idx", "connectionId"),
    )

class ScimSsoConnectionView(Base):
    __tablename__ = "ScimSsoConnectionView"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    folded: Mapped[dict[str, Any]] = mapped_column(JSONB)
    applied_event_ids: Mapped[list[str]] = mapped_column("appliedEventIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("ScimSsoConnectionView_organizationId_createdAt_idx", "organizationId", "createdAt"),
    )

class OrganizationSeatPeak(Base):
    __tablename__ = "OrganizationSeatPeak"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    opened_at: Mapped[float] = mapped_column("openedAt")
    full_seats: Mapped[int] = mapped_column("fullSeats")
    lite_seats: Mapped[int] = mapped_column("liteSeats")
    developer_seats: Mapped[int] = mapped_column("developerSeats")
    latest_change_at: Mapped[float] = mapped_column("latestChangeAt")
    mismatches: Mapped[int]

class OrganizationSeatPeakHolder(Base):
    __tablename__ = "OrganizationSeatPeakHolder"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    holder_kind: Mapped[str] = mapped_column("holderKind", primary_key=True)
    holder_id: Mapped[str] = mapped_column("holderId", primary_key=True)
    seat: Mapped[str]
    last_changed_at: Mapped[float] = mapped_column("lastChangedAt")
    last_change_id: Mapped[Optional[str]] = mapped_column("lastChangeId")
    expires_at: Mapped[Optional[float]] = mapped_column("expiresAt")
    expired_by_fold: Mapped[bool] = mapped_column("expiredByFold", default=False, server_default=text("false"))
    suppressed_by: Mapped[Optional[str]] = mapped_column("suppressedBy")
    restored_by_fold: Mapped[bool] = mapped_column("restoredByFold", default=False, server_default=text("false"))

    __table_args__ = (
        Index("OrganizationSeatPeakHolder_organizationId_holderKind_seat_e_idx", "organizationId", "holderKind", "seat", "expiresAt"),
        Index("OrganizationSeatPeakHolder_organizationId_suppressedBy_idx", "organizationId", "suppressedBy"),
    )

class OrganizationSeatPeakMonth(Base):
    __tablename__ = "OrganizationSeatPeakMonth"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    month: Mapped[str] = mapped_column(primary_key=True)
    full_peak: Mapped[int] = mapped_column("fullPeak")
    full_peak_at: Mapped[float] = mapped_column("fullPeakAt")
    lite_peak: Mapped[int] = mapped_column("litePeak")
    lite_peak_at: Mapped[float] = mapped_column("litePeakAt")
    full_at_end: Mapped[int] = mapped_column("fullAtEnd")
    lite_at_end: Mapped[int] = mapped_column("liteAtEnd")

class Project(Base):
    __tablename__ = "Project"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    api_key: Mapped[str] = mapped_column("apiKey")
    legacy_api_key_last_used_at: Mapped[Optional[datetime]] = mapped_column("legacyApiKeyLastUsedAt")
    lwql_key: Mapped[str] = mapped_column("lwqlKey", server_default=text("(gen_random_uuid())::text"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="RESTRICT", onupdate="CASCADE", name="Project_teamId_fkey"))
    language: Mapped[str]
    framework: Mapped[str]
    kind: Mapped[str] = mapped_column(default="application", server_default=text("'application'"))
    aggregate_rule: Mapped[Optional[dict[str, Any]]] = mapped_column("aggregateRule", JSONB(none_as_null=True))
    first_message: Mapped[bool] = mapped_column("firstMessage", default=False, server_default=text("false"))
    integrated: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    user_link_template: Mapped[Optional[str]] = mapped_column("userLinkTemplate")
    trace_sharing_enabled: Mapped[bool] = mapped_column("traceSharingEnabled", default=True, server_default=text("true"))
    presence_enabled: Mapped[bool] = mapped_column("presenceEnabled", default=True, server_default=text("true"))
    s3_endpoint: Mapped[Optional[str]] = mapped_column("s3Endpoint")
    s3_access_key_id: Mapped[Optional[str]] = mapped_column("s3AccessKeyId")
    s3_secret_access_key: Mapped[Optional[str]] = mapped_column("s3SecretAccessKey")
    s3_bucket: Mapped[Optional[str]] = mapped_column("s3Bucket")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    is_personal: Mapped[bool] = mapped_column("isPersonal", default=False, server_default=text("false"))
    owner_user_id: Mapped[Optional[str]] = mapped_column("ownerUserId", String(255), ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Project_ownerUserId_fkey"))
    personal_features: Mapped[dict[str, Any]] = mapped_column("personalFeatures", JSONB, default=lambda: {}, server_default=text("'{}'"))
    department_id: Mapped[Optional[str]] = mapped_column("departmentId")
    langy_egress_allowlist: Mapped[Optional[dict[str, Any]]] = mapped_column("langyEgressAllowlist", JSONB(none_as_null=True))
    last_coding_agent_session_at: Mapped[Optional[datetime]] = mapped_column("lastCodingAgentSessionAt")
    last_coding_agent_pull_request_at: Mapped[Optional[datetime]] = mapped_column("lastCodingAgentPullRequestAt")

    __table_args__ = (
        Index("Project_slug_key", "slug", unique=True),
        Index("Project_apiKey_key", "apiKey", unique=True),
        Index("Project_lwqlKey_key", "lwqlKey", unique=True),
        Index("Project_teamId_idx", "teamId"),
        Index("Project_isPersonal_idx", "isPersonal"),
        Index("Project_ownerUserId_idx", "ownerUserId"),
        Index("Project_teamId_kind_idx", "teamId", "kind"),
        Index("Project_departmentId_idx", "departmentId"),
    )

    team: Mapped["Team"] = relationship(back_populates="projects")
    owner_user: Mapped[Optional["User"]] = relationship(back_populates="personal_projects")
    checks: Mapped[list["Monitor"]] = relationship(passive_deletes="all", back_populates="project")
    costs: Mapped[list["Cost"]] = relationship(passive_deletes="all", back_populates="project")
    topics: Mapped[list["Topic"]] = relationship(passive_deletes="all", back_populates="project")
    datasets: Mapped[list["Dataset"]] = relationship(passive_deletes="all", back_populates="project")
    dataset_records: Mapped[list["DatasetRecord"]] = relationship(passive_deletes="all", back_populates="project")
    custom_graphs: Mapped[list["CustomGraph"]] = relationship(passive_deletes="all", back_populates="project")
    dashboards: Mapped[list["Dashboard"]] = relationship(passive_deletes="all", back_populates="project")
    batch_evaluations: Mapped[list["BatchEvaluation"]] = relationship(passive_deletes="all", back_populates="project")
    triggers: Mapped[list["Trigger"]] = relationship(passive_deletes="all", back_populates="project")
    experiments: Mapped[list["Experiment"]] = relationship(passive_deletes="all", back_populates="project")
    annotations: Mapped[list["Annotation"]] = relationship(passive_deletes="all", back_populates="project")
    project_secrets: Mapped[list["ProjectSecret"]] = relationship(passive_deletes="all", back_populates="project")
    trigger_sent: Mapped[list["TriggerSent"]] = relationship(passive_deletes="all", back_populates="project")
    webhook_deliveries: Mapped[list["WebhookEndpointDelivery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    annotation_scores: Mapped[list["AnnotationScore"]] = relationship(passive_deletes="all", back_populates="project")
    share_links: Mapped[list["ShareLink"]] = relationship(passive_deletes="all", back_populates="project")
    workflows: Mapped[list["Workflow"]] = relationship(passive_deletes="all", back_populates="project")
    workflow_version: Mapped[list["WorkflowVersion"]] = relationship(passive_deletes="all", back_populates="project")
    annotation_queue: Mapped[list["AnnotationQueue"]] = relationship(passive_deletes="all", back_populates="project")
    annotation_queue_item: Mapped[list["AnnotationQueueItem"]] = relationship(passive_deletes="all", back_populates="project")
    llm_prompt_configs: Mapped[list["LlmPromptConfig"]] = relationship(passive_deletes="all", back_populates="project")
    analytics: Mapped[list["Analytics"]] = relationship(passive_deletes="all", back_populates="project")
    notifications: Mapped[list["Notification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    agents: Mapped[list["Agent"]] = relationship(passive_deletes="all", back_populates="project")
    evaluators: Mapped[list["Evaluator"]] = relationship(passive_deletes="all", back_populates="project")
    scenarios: Mapped[list["Scenario"]] = relationship(passive_deletes="all", back_populates="project")
    suite_configurations: Mapped[list["SimulationSuite"]] = relationship(passive_deletes="all", back_populates="project")
    saved_views: Mapped[list["SavedView"]] = relationship(passive_deletes="all", back_populates="project")
    gateway_guardrails: Mapped[list["GatewayGuardrail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    gateway_change_events: Mapped[list["GatewayChangeEvent"]] = relationship(back_populates="project")
    pinned_traces: Mapped[list["PinnedTrace"]] = relationship(passive_deletes="all", back_populates="project")
    trace_edit_overlays: Mapped[list["TraceEditOverlay"]] = relationship(passive_deletes="all", back_populates="project")
    email_suppressions: Mapped[list["EmailSuppression"]] = relationship(passive_deletes="all", back_populates="project")

class Department(Base):
    __tablename__ = "Department"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")

    __table_args__ = (
        Index("Department_organizationId_idx", "organizationId"),
    )

class DepartmentMembershipHistory(Base):
    __tablename__ = "DepartmentMembershipHistory"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    user_id: Mapped[str] = mapped_column("userId")
    department_id: Mapped[str] = mapped_column("departmentId")
    valid_from: Mapped[datetime] = mapped_column("validFrom")
    valid_to: Mapped[Optional[datetime]] = mapped_column("validTo")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("DepartmentMembershipHistory_organizationId_userId_validFrom_idx", "organizationId", "userId", "validFrom"),
    )

class PlatformToolPolicy(Base):
    __tablename__ = "PlatformToolPolicy"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    tool_slug: Mapped[str] = mapped_column("toolSlug")
    allow_vk: Mapped[bool] = mapped_column("allowVk", default=True, server_default=text("true"))
    allow_otel_direct: Mapped[bool] = mapped_column("allowOtelDirect", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("PlatformToolPolicy_organizationId_idx", "organizationId"),
        Index("PlatformToolPolicy_organizationId_toolSlug_key", "organizationId", "toolSlug", unique=True),
    )

class OrganizationInvite(Base):
    __tablename__ = "OrganizationInvite"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    invite_code: Mapped[str] = mapped_column("inviteCode")
    expiration: Mapped[Optional[datetime]]
    status: Mapped[str] = mapped_column(Enum("PENDING", "ACCEPTED", "WAITING_APPROVAL", "PAYMENT_PENDING", "REVOKED", name="INVITE_STATUS"), default="PENDING", server_default=text("'PENDING'"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrganizationInvite_organizationId_fkey"))
    team_ids: Mapped[str] = mapped_column("teamIds")
    team_assignments: Mapped[Optional[dict[str, Any]]] = mapped_column("teamAssignments", JSONB(none_as_null=True))
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", "EXTERNAL", "DEVELOPER", name="OrganizationUserRole"))
    requested_by: Mapped[Optional[str]] = mapped_column("requestedBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="OrganizationInvite_requestedBy_fkey"))
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    accepted_by_user_id: Mapped[Optional[str]] = mapped_column("acceptedByUserId")
    accepted_via_identifier_id: Mapped[Optional[str]] = mapped_column("acceptedViaIdentifierId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("OrganizationInvite_inviteCode_key", "inviteCode", unique=True),
        Index("OrganizationInvite_organizationId_idx", "organizationId"),
        Index("OrganizationInvite_requestedBy_idx", "requestedBy"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="organization_invite")
    requested_by_user: Mapped[Optional["User"]] = relationship(back_populates="invite_requests")

class Monitor(Base):
    __tablename__ = "Monitor"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Monitor_projectId_fkey"))
    experiment_id: Mapped[Optional[str]] = mapped_column("experimentId", ForeignKey("Experiment.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Monitor_experimentId_fkey"))
    evaluator_id: Mapped[Optional[str]] = mapped_column("evaluatorId", ForeignKey("Evaluator.id", ondelete="SET NULL", onupdate="CASCADE", name="Monitor_evaluatorId_fkey"))
    check_type: Mapped[str] = mapped_column("checkType")
    name: Mapped[str]
    slug: Mapped[str]
    execution_mode: Mapped[str] = mapped_column("executionMode", Enum("ON_MESSAGE", "AS_GUARDRAIL", "MANUALLY", name="EvaluationExecutionMode"), default="ON_MESSAGE", server_default=text("'ON_MESSAGE'"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    preconditions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    parameters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    mappings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    sample: Mapped[float] = mapped_column(default=1, server_default=text("1"))
    level: Mapped[str] = mapped_column(default="trace", server_default=text("'trace'"))
    thread_idle_timeout: Mapped[Optional[int]] = mapped_column("threadIdleTimeout")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    auto_paused_at: Mapped[Optional[datetime]] = mapped_column("autoPausedAt")
    auto_paused_reason: Mapped[Optional[str]] = mapped_column("autoPausedReason")
    evaluator_label: Mapped[Optional[str]] = mapped_column("evaluatorLabel")
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")

    __table_args__ = (
        Index("Monitor_experimentId_key", "experimentId", unique=True),
        Index("Monitor_projectId_idx", "projectId"),
        Index("Monitor_evaluatorId_idx", "evaluatorId"),
        Index("Monitor_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="checks")
    experiment: Mapped[Optional["Experiment"]] = relationship(back_populates="monitor")
    evaluator: Mapped[Optional["Evaluator"]] = relationship(back_populates="monitors")

class Cost(Base):
    __tablename__ = "Cost"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Cost_projectId_fkey"))
    cost_type: Mapped[str] = mapped_column("costType", Enum("TRACE_CHECK", "GUARDRAIL", "CLUSTERING", "BATCH_EVALUATION", name="CostType"))
    cost_name: Mapped[Optional[str]] = mapped_column("costName")
    reference_type: Mapped[str] = mapped_column("referenceType", Enum("CHECK", "TRACE", "PROJECT", "BATCH", name="CostReferenceType"))
    reference_id: Mapped[str] = mapped_column("referenceId")
    amount: Mapped[float]
    currency: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    extra_info: Mapped[Optional[dict[str, Any]]] = mapped_column("extraInfo", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Cost_referenceType_referenceId_idx", "referenceType", "referenceId"),
        Index("Cost_costType_idx", "costType"),
        Index("Cost_projectId_idx", "projectId"),
        Index("Cost_projectId_createdAt_idx", "projectId", "createdAt"),
    )

    project: Mapped["Project"] = relationship(back_populates="costs")

class Topic(Base):
    __tablename__ = "Topic"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Topic_projectId_fkey"))
    name: Mapped[str]
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("Topic.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Topic_parentId_fkey"))
    embeddings_model: Mapped[str]
    centroid: Mapped[dict[str, Any]] = mapped_column(JSONB)
    p95_distance: Mapped[float] = mapped_column("p95Distance")
    automatically_generated: Mapped[bool] = mapped_column("automaticallyGenerated", default=True, server_default=text("true"))
    last_event_id: Mapped[Optional[str]] = mapped_column("lastEventId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Topic_parentId_idx", "parentId"),
        Index("Topic_projectId_idx", "projectId"),
    )

    project: Mapped["Project"] = relationship(back_populates="topics")
    parent: Mapped[Optional["Topic"]] = relationship(remote_side=[id], back_populates="subtopics")
    subtopics: Mapped[list["Topic"]] = relationship(passive_deletes="all", back_populates="parent")

class Dataset(Base):
    __tablename__ = "Dataset"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Dataset_projectId_fkey"))
    name: Mapped[str]
    slug: Mapped[str]
    column_types: Mapped[dict[str, Any]] = mapped_column("columnTypes", JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    mapping: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    use_s3: Mapped[bool] = mapped_column("useS3", default=False, server_default=text("false"))
    s3_record_count: Mapped[Optional[int]] = mapped_column("s3RecordCount")
    content_layout: Mapped[str] = mapped_column("contentLayout", default="postgres", server_default=text("'postgres'"))
    status: Mapped[str] = mapped_column(default="ready", server_default=text("'ready'"))
    status_error: Mapped[Optional[str]] = mapped_column("statusError")
    staging_key: Mapped[Optional[str]] = mapped_column("stagingKey")
    source_stored_object_id: Mapped[Optional[str]] = mapped_column("sourceStoredObjectId")
    upload_filename: Mapped[Optional[str]] = mapped_column("uploadFilename")
    row_count: Mapped[Optional[int]] = mapped_column("rowCount")
    size_bytes: Mapped[Optional[int]] = mapped_column("sizeBytes", BigInteger)
    chunk_count: Mapped[Optional[int]] = mapped_column("chunkCount")
    chunk_offsets: Mapped[Optional[dict[str, Any]]] = mapped_column("chunkOffsets", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Dataset_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="datasets")
    dataset_records: Mapped[list["DatasetRecord"]] = relationship(passive_deletes="all", back_populates="dataset")
    batch_evaluations: Mapped[list["BatchEvaluation"]] = relationship(passive_deletes="all", back_populates="dataset")

class DatasetRecord(Base):
    __tablename__ = "DatasetRecord"

    id: Mapped[str] = mapped_column(primary_key=True)
    dataset_id: Mapped[str] = mapped_column("datasetId", ForeignKey("Dataset.id", ondelete="RESTRICT", onupdate="CASCADE", name="DatasetRecord_datasetId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="DatasetRecord_projectId_fkey"))
    entry: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DatasetRecord_datasetId_idx", "datasetId"),
        Index("DatasetRecord_projectId_idx", "projectId"),
        Index("DatasetRecord_createdAt_idx", "createdAt"),
        Index("DatasetRecord_projectId_datasetId_idx", "projectId", "datasetId"),
    )

    dataset: Mapped["Dataset"] = relationship(back_populates="dataset_records")
    project: Mapped["Project"] = relationship(back_populates="dataset_records")

class DatasetVersion(Base):
    __tablename__ = "DatasetVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    dataset_id: Mapped[str] = mapped_column("datasetId")
    version: Mapped[int]
    name: Mapped[Optional[str]]
    trigger: Mapped[str]
    content_hash: Mapped[str] = mapped_column("contentHash")
    column_types: Mapped[dict[str, Any]] = mapped_column("columnTypes", JSONB)
    row_count: Mapped[int] = mapped_column("rowCount")
    size_bytes: Mapped[int] = mapped_column("sizeBytes", BigInteger)
    chunk_count: Mapped[int] = mapped_column("chunkCount")
    restored_from_version: Mapped[Optional[int]] = mapped_column("restoredFromVersion")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    author_kind: Mapped[Optional[str]] = mapped_column("authorKind")
    pinned_at: Mapped[Optional[datetime]] = mapped_column("pinnedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("DatasetVersion_projectId_datasetId_idx", "projectId", "datasetId"),
        Index("DatasetVersion_datasetId_version_key", "datasetId", "version", unique=True),
    )

class DatasetVersionRun(Base):
    __tablename__ = "DatasetVersionRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    dataset_id: Mapped[str] = mapped_column("datasetId")
    version_id: Mapped[str] = mapped_column("versionId")
    run_id: Mapped[str] = mapped_column("runId")
    experiment_id: Mapped[Optional[str]] = mapped_column("experimentId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("DatasetVersionRun_versionId_idx", "versionId"),
        Index("DatasetVersionRun_projectId_runId_datasetId_key", "projectId", "runId", "datasetId", unique=True),
    )

class Dashboard(Base):
    __tablename__ = "Dashboard"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Dashboard_projectId_fkey"))
    name: Mapped[str]
    order: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    description: Mapped[Optional[str]]
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    scope: Mapped[str] = mapped_column(Enum("PRIVATE", "PROJECT", "ORGANIZATION", name="DashboardScope"), default="PROJECT", server_default=text("'PROJECT'"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Dashboard_projectId_idx", "projectId"),
        Index("Dashboard_projectId_order_idx", "projectId", "order"),
        Index("Dashboard_organizationId_scope_idx", "organizationId", "scope"),
    )

    project: Mapped["Project"] = relationship(back_populates="dashboards")
    graphs: Mapped[list["CustomGraph"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dashboard")

class DashboardFavourite(Base):
    __tablename__ = "DashboardFavourite"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    dashboard_id: Mapped[Optional[str]] = mapped_column("dashboardId")
    template_id: Mapped[Optional[str]] = mapped_column("templateId")
    project_id: Mapped[str] = mapped_column("projectId")
    position: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("DashboardFavourite_userId_projectId_position_idx", "userId", "projectId", "position"),
        Index("DashboardFavourite_dashboardId_idx", "dashboardId"),
        Index("DashboardFavourite_userId_dashboardId_key", "userId", "dashboardId", unique=True),
        Index("DashboardFavourite_userId_projectId_templateId_key", "userId", "projectId", "templateId", unique=True),
    )

class DashboardsV2(Base):
    __tablename__ = "DashboardsV2"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    scope: Mapped[str] = mapped_column(Enum("PRIVATE", "PROJECT", "ORGANIZATION", name="DashboardScope"), default="PROJECT", server_default=text("'PROJECT'"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    excluded_origins: Mapped[Optional[dict[str, Any]]] = mapped_column("excludedOrigins", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DashboardsV2_projectId_idx", "projectId"),
        Index("DashboardsV2_organizationId_scope_idx", "organizationId", "scope"),
    )

class DashboardWidget(Base):
    __tablename__ = "DashboardWidget"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    dashboard_id: Mapped[Optional[str]] = mapped_column("dashboardId")
    name: Mapped[str]
    definition: Mapped[dict[str, Any]] = mapped_column(JSONB)
    grid_column: Mapped[int] = mapped_column("gridColumn", default=0, server_default=text("0"))
    grid_row: Mapped[int] = mapped_column("gridRow", default=0, server_default=text("0"))
    col_span: Mapped[int] = mapped_column("colSpan", default=1, server_default=text("1"))
    row_span: Mapped[int] = mapped_column("rowSpan", default=1, server_default=text("1"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DashboardWidget_projectId_dashboardId_idx", "projectId", "dashboardId"),
    )

class InsightProjection(Base):
    __tablename__ = "InsightProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    title: Mapped[str]
    body: Mapped[str]
    tone: Mapped[str]
    topic: Mapped[Optional[str]]
    valid_days: Mapped[int] = mapped_column("validDays")
    lwql: Mapped[Optional[str]]
    source_conversation_id: Mapped[Optional[str]] = mapped_column("sourceConversationId")
    source_message_id: Mapped[Optional[str]] = mapped_column("sourceMessageId")
    filed_by_user_id: Mapped[Optional[str]] = mapped_column("filedByUserId")
    filed_at: Mapped[float] = mapped_column("filedAt")
    renewed_at: Mapped[Optional[float]] = mapped_column("renewedAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    board_id: Mapped[Optional[str]] = mapped_column("boardId")
    board_name: Mapped[Optional[str]] = mapped_column("boardName")
    widget_id: Mapped[Optional[str]] = mapped_column("widgetId")
    widget_name: Mapped[Optional[str]] = mapped_column("widgetName")
    filed_via: Mapped[str] = mapped_column("filedVia", default="chat", server_default=text("'chat'"))
    replay_start: Mapped[Optional[float]] = mapped_column("replayStart")
    replay_end: Mapped[Optional[float]] = mapped_column("replayEnd")
    replay_granularity_seconds: Mapped[Optional[int]] = mapped_column("replayGranularitySeconds")
    replay_context: Mapped[Optional[dict[str, Any]]] = mapped_column("replayContext", JSONB(none_as_null=True))
    owner_user_id: Mapped[Optional[str]] = mapped_column("ownerUserId")
    finding_key: Mapped[Optional[str]] = mapped_column("findingKey")

    __table_args__ = (
        Index("InsightProjection_projectId_filedAt_idx", "projectId", "filedAt"),
        Index("InsightProjection_projectId_ownerUserId_filedAt_idx", "projectId", "ownerUserId", "filedAt"),
    )

class InsightReaderProjection(Base):
    __tablename__ = "InsightReaderProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    insight_id: Mapped[str] = mapped_column("insightId")
    user_id: Mapped[str] = mapped_column("userId")
    seen_at: Mapped[Optional[float]] = mapped_column("seenAt")
    archived_at: Mapped[Optional[float]] = mapped_column("archivedAt")
    kept_at: Mapped[Optional[float]] = mapped_column("keptAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("InsightReaderProjection_projectId_userId_idx", "projectId", "userId"),
        Index("InsightReaderProjection_projectId_insightId_userId_key", "projectId", "insightId", "userId", unique=True),
    )

class InsightDailyScheduleProjection(Base):
    __tablename__ = "InsightDailyScheduleProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    user_id: Mapped[str] = mapped_column("userId")
    board_kind: Mapped[str] = mapped_column("boardKind")
    board_id: Mapped[str] = mapped_column("boardId")
    board_name: Mapped[str] = mapped_column("boardName")
    state: Mapped[str] = mapped_column(default="undecided", server_default=text("'undecided'"))
    off_reason: Mapped[Optional[str]] = mapped_column("offReason")
    hour: Mapped[Optional[int]]
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    max_insights: Mapped[Optional[int]] = mapped_column("maxInsights")
    last_run_id: Mapped[Optional[str]] = mapped_column("lastRunId")
    last_run_at: Mapped[Optional[float]] = mapped_column("lastRunAt")
    last_run_outcome: Mapped[Optional[str]] = mapped_column("lastRunOutcome")
    last_run_reason: Mapped[Optional[str]] = mapped_column("lastRunReason")
    last_run_filed: Mapped[Optional[int]] = mapped_column("lastRunFiled")
    last_run_renewed: Mapped[Optional[int]] = mapped_column("lastRunRenewed")
    last_run_conversation_id: Mapped[Optional[str]] = mapped_column("lastRunConversationId")
    consecutive_skips: Mapped[int] = mapped_column("consecutiveSkips", default=0, server_default=text("0"))
    skip_streak_slot: Mapped[Optional[float]] = mapped_column("skipStreakSlot")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("InsightDailySchedule_projectId_userId_board_key", "projectId", "userId", "boardKind", "boardId", unique=True),
    )

class SavedView(Base):
    __tablename__ = "SavedView"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="SavedView_projectId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="SavedView_userId_fkey"))
    name: Mapped[str]
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    query: Mapped[Optional[str]]
    period: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    order: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    kind: Mapped[str] = mapped_column(default="v1-traces-filter", server_default=text("'v1-traces-filter'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SavedView_projectId_idx", "projectId"),
        Index("SavedView_projectId_order_idx", "projectId", "order"),
        Index("SavedView_userId_idx", "userId"),
        Index("SavedView_projectId_kind_order_idx", "projectId", "kind", "order"),
    )

    project: Mapped["Project"] = relationship(back_populates="saved_views")
    user: Mapped[Optional["User"]] = relationship(back_populates="saved_views")

class CustomGraph(Base):
    __tablename__ = "CustomGraph"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="CustomGraph_projectId_fkey"))
    name: Mapped[str]
    graph: Mapped[dict[str, Any]] = mapped_column(JSONB)
    filters: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    kind: Mapped[str] = mapped_column(default="builder", server_default=text("'builder'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    dashboard_id: Mapped[Optional[str]] = mapped_column("dashboardId", ForeignKey("Dashboard.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomGraph_dashboardId_fkey"))
    grid_column: Mapped[int] = mapped_column("gridColumn", default=0, server_default=text("0"))
    grid_row: Mapped[int] = mapped_column("gridRow", default=0, server_default=text("0"))
    col_span: Mapped[int] = mapped_column("colSpan", default=1, server_default=text("1"))
    row_span: Mapped[int] = mapped_column("rowSpan", default=1, server_default=text("1"))

    __table_args__ = (
        Index("CustomGraph_projectId_idx", "projectId"),
        Index("CustomGraph_dashboardId_idx", "dashboardId"),
        Index("CustomGraph_projectId_kind_idx", "projectId", "kind"),
    )

    project: Mapped["Project"] = relationship(back_populates="custom_graphs")
    dashboard: Mapped[Optional["Dashboard"]] = relationship(back_populates="graphs")
    trigger_sent: Mapped[list["TriggerSent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="custom_graph")
    trigger: Mapped[Optional["Trigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="custom_graph")

class BatchEvaluation(Base):
    __tablename__ = "BatchEvaluation"

    id: Mapped[str] = mapped_column(primary_key=True)
    experiment_id: Mapped[str] = mapped_column("experimentId", ForeignKey("Experiment.id", ondelete="RESTRICT", onupdate="CASCADE", name="BatchEvaluation_experimentId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="BatchEvaluation_projectId_fkey"))
    data: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    status: Mapped[str]
    score: Mapped[float]
    label: Mapped[Optional[str]]
    passed: Mapped[bool]
    details: Mapped[str]
    cost: Mapped[float]
    dataset_slug: Mapped[str] = mapped_column("datasetSlug")
    dataset_id: Mapped[str] = mapped_column("datasetId", ForeignKey("Dataset.id", ondelete="RESTRICT", onupdate="CASCADE", name="BatchEvaluation_datasetId_fkey"))
    evaluation: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("BatchEvaluation_projectId_idx", "projectId"),
        Index("BatchEvaluation_datasetId_idx", "datasetId"),
        Index("BatchEvaluation_experimentId_idx", "experimentId"),
        Index("BatchEvaluation_id_key", "id", unique=True),
    )

    experiment: Mapped["Experiment"] = relationship(back_populates="batch_evaluations")
    project: Mapped["Project"] = relationship(back_populates="batch_evaluations")
    dataset: Mapped["Dataset"] = relationship(back_populates="batch_evaluations")

class Trigger(Base):
    __tablename__ = "Trigger"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Trigger_projectId_fkey"))
    action: Mapped[str] = mapped_column(Enum("SEND_EMAIL", "ADD_TO_DATASET", "ADD_TO_ANNOTATION_QUEUE", "SEND_SLACK_MESSAGE", "SEND_WEBHOOK", name="TriggerAction"))
    trigger_kind: Mapped[str] = mapped_column("triggerKind", Enum("AUTOMATION", "ALERT", "REPORT", name="TriggerKind"), default="AUTOMATION", server_default=text("'AUTOMATION'"))
    action_params: Mapped[dict[str, Any]] = mapped_column("actionParams", JSONB)
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    filter_query: Mapped[Optional[str]] = mapped_column("filterQuery")
    last_run_at: Mapped[float] = mapped_column("lastRunAt", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    paused_reason: Mapped[Optional[str]] = mapped_column("pausedReason")
    paused_at: Mapped[Optional[datetime]] = mapped_column("pausedAt", UtcDateTimeTz(precision=3))
    message: Mapped[Optional[str]]
    deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    alert_type: Mapped[Optional[str]] = mapped_column("alertType", Enum("CRITICAL", "WARNING", "INFO", name="AlertType"))
    slack_template_type: Mapped[Optional[str]] = mapped_column("slackTemplateType")
    slack_template: Mapped[Optional[str]] = mapped_column("slackTemplate")
    email_subject_template: Mapped[Optional[str]] = mapped_column("emailSubjectTemplate")
    email_body_template: Mapped[Optional[str]] = mapped_column("emailBodyTemplate")
    notification_cadence: Mapped[str] = mapped_column("notificationCadence", default="immediate", server_default=text("'immediate'"))
    trace_debounce_ms: Mapped[int] = mapped_column("traceDebounceMs", default=30000, server_default=text("30000"))
    custom_graph_id: Mapped[Optional[str]] = mapped_column("customGraphId", ForeignKey("CustomGraph.id", ondelete="CASCADE", onupdate="CASCADE", name="Trigger_customGraphId_fkey"))

    __table_args__ = (
        Index("Trigger_customGraphId_key", "customGraphId", unique=True),
        Index("Trigger_projectId_idx", "projectId"),
    )

    project: Mapped["Project"] = relationship(back_populates="triggers")
    custom_graph: Mapped[Optional["CustomGraph"]] = relationship(back_populates="trigger")
    trigger_sent: Mapped[list["TriggerSent"]] = relationship(passive_deletes="all", back_populates="trigger")
    webhook_deliveries: Mapped[list["WebhookEndpointDelivery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="trigger")
    latest_evaluation: Mapped[Optional["TriggerLatestEvaluation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="trigger")

class SlackIntegration(Base):
    __tablename__ = "SlackIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    kind: Mapped[str] = mapped_column(Enum("BOT", "INCOMING_WEBHOOK", name="SlackIntegrationKind"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "PROJECT", name="SlackIntegrationScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    bot_token_encrypted: Mapped[Optional[str]] = mapped_column("botTokenEncrypted")
    webhook_url_encrypted: Mapped[Optional[str]] = mapped_column("webhookUrlEncrypted")
    secret_fingerprint: Mapped[str] = mapped_column("secretFingerprint")
    secret_hint: Mapped[str] = mapped_column("secretHint")
    slack_team_id: Mapped[Optional[str]] = mapped_column("slackTeamId")
    slack_team_name: Mapped[Optional[str]] = mapped_column("slackTeamName")
    created_by_id: Mapped[str] = mapped_column("createdById")
    updated_by_id: Mapped[str] = mapped_column("updatedById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SlackIntegration_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("SlackIntegration_organizationId_scopeType_scopeId_secretFin_key", "organizationId", "scopeType", "scopeId", "secretFingerprint", unique=True),
    )

class SlackConnectionClaim(Base):
    __tablename__ = "slack_connection_claim"

    connection_id: Mapped[str] = mapped_column("connectionId", primary_key=True)
    claimant_id: Mapped[str] = mapped_column("claimantId", primary_key=True)
    claimant_label: Mapped[str] = mapped_column("claimantLabel")
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("slack_connection_claim_organizationId_idx", "organizationId"),
    )

class WebhookEndpoint(Base):
    __tablename__ = "WebhookEndpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookEndpoint_organizationId_fkey"))
    destination_kind: Mapped[str] = mapped_column("destinationKind", Enum("http", "sqs", name="WebhookDestinationKind"), default="http", server_default=text("'http'"))
    url: Mapped[Optional[str]]
    sqs_queue_url: Mapped[Optional[str]] = mapped_column("sqsQueueUrl")
    sqs_role_arn: Mapped[Optional[str]] = mapped_column("sqsRoleArn")
    sqs_external_id: Mapped[Optional[str]] = mapped_column("sqsExternalId")
    sqs_access_key_id: Mapped[Optional[str]] = mapped_column("sqsAccessKeyId")
    sqs_secret_access_key_encrypted: Mapped[Optional[str]] = mapped_column("sqsSecretAccessKeyEncrypted")
    secret_encrypted: Mapped[str] = mapped_column("secretEncrypted")
    signature_scheme: Mapped[Optional[str]] = mapped_column("signatureScheme")
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    previous_secret_encrypted: Mapped[Optional[str]] = mapped_column("previousSecretEncrypted")
    previous_secret_expires_at: Mapped[Optional[datetime]] = mapped_column("previousSecretExpiresAt")
    enabled_events: Mapped[list[str]] = mapped_column("enabledEvents", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "DISABLED", name="WebhookEndpointStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    disabled_reason: Mapped[Optional[str]] = mapped_column("disabledReason")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    failing_since: Mapped[Optional[datetime]] = mapped_column("failingSince")
    max_batch_size: Mapped[int] = mapped_column("maxBatchSize", default=100, server_default=text("100"))
    max_batch_delay_ms: Mapped[int] = mapped_column("maxBatchDelayMs", default=250, server_default=text("250"))
    max_in_flight: Mapped[int] = mapped_column("maxInFlight", default=4, server_default=text("4"))
    allow_self_signed_certificate: Mapped[bool] = mapped_column("allowSelfSignedCertificate", default=False, server_default=text("false"))
    last_success_at: Mapped[Optional[datetime]] = mapped_column("lastSuccessAt")
    last_failure_at: Mapped[Optional[datetime]] = mapped_column("lastFailureAt")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("WebhookEndpoint_organizationId_status_idx", "organizationId", "status"),
        Index("WebhookEndpoint_organizationId_idempotencyKey_key", "organizationId", "idempotencyKey", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="webhook_endpoints")
    deliveries: Mapped[list["WebhookEndpointDelivery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="endpoint")

class WebhookEndpointDelivery(Base):
    __tablename__ = "WebhookEndpointDelivery"

    id: Mapped[str] = mapped_column(primary_key=True)
    channel: Mapped[str] = mapped_column(Enum("platform", "automations", name="WebhookDeliveryChannel"), default="platform", server_default=text("'platform'"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    endpoint_id: Mapped[Optional[str]] = mapped_column("endpointId", ForeignKey("WebhookEndpoint.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookEndpointDelivery_endpointId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookEndpointDelivery_projectId_fkey"))
    trigger_id: Mapped[Optional[str]] = mapped_column("triggerId", ForeignKey("Trigger.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookEndpointDelivery_triggerId_fkey"))
    dispatch_id: Mapped[str] = mapped_column("dispatchId")
    attempt: Mapped[Optional[int]]
    event_count: Mapped[Optional[int]] = mapped_column("eventCount")
    response_status: Mapped[Optional[int]] = mapped_column("responseStatus")
    latency_ms: Mapped[Optional[int]] = mapped_column("latencyMs")
    error: Mapped[Optional[str]]
    response: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    outcome: Mapped[str] = mapped_column(Enum("success", "retryable", "terminal", "pending", name="WebhookDeliveryOutcome"))
    fired_at: Mapped[datetime] = mapped_column("firedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("WebhookEndpointDelivery_endpointId_firedAt_idx", "endpointId", "firedAt"),
        Index("WebhookEndpointDelivery_organizationId_firedAt_idx", "organizationId", "firedAt"),
        Index("WebhookEndpointDelivery_firedAt_idx", "firedAt"),
        Index("WebhookEndpointDelivery_endpointId_dispatchId_idx", "endpointId", "dispatchId"),
        Index("WebhookEndpointDelivery_projectId_triggerId_firedAt_idx", "projectId", "triggerId", "firedAt"),
        Index("WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx", "projectId", "triggerId", "dispatchId"),
    )

    endpoint: Mapped[Optional["WebhookEndpoint"]] = relationship(back_populates="deliveries")
    project: Mapped[Optional["Project"]] = relationship(back_populates="webhook_deliveries")
    trigger: Mapped[Optional["Trigger"]] = relationship(back_populates="webhook_deliveries")

class Experiment(Base):
    __tablename__ = "Experiment"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("DSPY", "BATCH_EVALUATION", "BATCH_EVALUATION_V2", "EVALUATIONS_V3", name="ExperimentType"))
    slug: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Experiment_projectId_fkey"))
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="SET NULL", onupdate="CASCADE", name="Experiment_workflowId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    workbench_state: Mapped[Optional[dict[str, Any]]] = mapped_column("workbenchState", JSONB(none_as_null=True))
    workbench_version: Mapped[int] = mapped_column("workbenchVersion", default=0, server_default=text("0"))

    __table_args__ = (
        Index("Experiment_projectId_idx", "projectId"),
        Index("Experiment_workflowId_idx", "workflowId"),
        Index("Experiment_projectId_archivedAt_idx", "projectId", "archivedAt"),
        Index("Experiment_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="experiments")
    workflow: Mapped[Optional["Workflow"]] = relationship(back_populates="experiments")
    batch_evaluations: Mapped[list["BatchEvaluation"]] = relationship(passive_deletes="all", back_populates="experiment")
    monitor: Mapped[Optional["Monitor"]] = relationship(passive_deletes="all", back_populates="experiment")

class ExperimentVersion(Base):
    __tablename__ = "ExperimentVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    experiment_id: Mapped[str] = mapped_column("experimentId")
    project_id: Mapped[str] = mapped_column("projectId")
    version: Mapped[int]
    counter_version: Mapped[int] = mapped_column("counterVersion")
    auto_saved: Mapped[bool] = mapped_column("autoSaved", default=False, server_default=text("false"))
    commit_message: Mapped[Optional[str]] = mapped_column("commitMessage")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    author_label: Mapped[str] = mapped_column("authorLabel")
    run_id: Mapped[Optional[str]] = mapped_column("runId")
    state: Mapped[dict[str, Any]] = mapped_column(JSONB)
    schema_version: Mapped[str] = mapped_column("schemaVersion", default="1", server_default=text("'1'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ExperimentVersion_projectId_experimentId_autoSaved_idx", "projectId", "experimentId", "autoSaved"),
        Index("ExperimentVersion_projectId_experimentId_version_idx", "projectId", "experimentId", "version"),
        Index("ExperimentVersion_projectId_experimentId_counterVersion_idx", "projectId", "experimentId", "counterVersion"),
        Index("ExperimentVersion_createdAt_idx", "createdAt"),
        Index("ExperimentVersion_experimentId_version_key", "experimentId", "version", unique=True),
    )

class Annotation(Base):
    __tablename__ = "Annotation"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Annotation_projectId_fkey"))
    trace_id: Mapped[str] = mapped_column("traceId")
    comment: Mapped[Optional[str]]
    is_thumbs_up: Mapped[Optional[bool]] = mapped_column("isThumbsUp")
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Annotation_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    email: Mapped[Optional[str]]
    score_options: Mapped[Optional[dict[str, Any]]] = mapped_column("scoreOptions", JSONB(none_as_null=True))
    expected_output: Mapped[Optional[str]] = mapped_column("expectedOutput")
    anchor_kind: Mapped[Optional[str]] = mapped_column("anchorKind")
    anchor_id: Mapped[Optional[str]] = mapped_column("anchorId")
    anchor_path: Mapped[Optional[str]] = mapped_column("anchorPath")

    __table_args__ = (
        Index("Annotation_projectId_idx", "projectId"),
        Index("Annotation_traceId_idx", "traceId"),
        Index("Annotation_userId_idx", "userId"),
        Index("Annotation_projectId_traceId_idx", "projectId", "traceId"),
        Index("Annotation_projectId_traceId_anchorKind_idx", "projectId", "traceId", "anchorKind"),
    )

    project: Mapped["Project"] = relationship(back_populates="annotations")
    user: Mapped[Optional["User"]] = relationship(back_populates="annotation")

class ModelProvider(Base):
    __tablename__ = "ModelProvider"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    provider: Mapped[str]
    routing_handle: Mapped[Optional[str]] = mapped_column("routingHandle")
    enabled: Mapped[bool]
    custom_keys: Mapped[Optional[dict[str, Any]]] = mapped_column("customKeys", JSONB(none_as_null=True))
    extra_headers: Mapped[Optional[dict[str, Any]]] = mapped_column("extraHeaders", JSONB(none_as_null=True))
    custom_models: Mapped[Optional[dict[str, Any]]] = mapped_column("customModels", JSONB(none_as_null=True))
    custom_embeddings_models: Mapped[Optional[dict[str, Any]]] = mapped_column("customEmbeddingsModels", JSONB(none_as_null=True))
    deployment_mapping: Mapped[Optional[dict[str, Any]]] = mapped_column("deploymentMapping", JSONB(none_as_null=True))
    rate_limit_rpm: Mapped[Optional[int]] = mapped_column("rateLimitRpm")
    rate_limit_tpm: Mapped[Optional[int]] = mapped_column("rateLimitTpm")
    rate_limit_rpd: Mapped[Optional[int]] = mapped_column("rateLimitRpd")
    rotation_policy: Mapped[str] = mapped_column("rotationPolicy", Enum("MANUAL", name="GatewayProviderRotationPolicy"), default="MANUAL", server_default=text("'MANUAL'"))
    provider_config: Mapped[Optional[dict[str, Any]]] = mapped_column("providerConfig", JSONB(none_as_null=True))
    fallback_priority_global: Mapped[Optional[int]] = mapped_column("fallbackPriorityGlobal")
    langy_skip_permissions_models: Mapped[Optional[dict[str, Any]]] = mapped_column("langySkipPermissionsModels", JSONB(none_as_null=True))
    health_status: Mapped[str] = mapped_column("healthStatus", Enum("UNKNOWN", "HEALTHY", "DEGRADED", "CIRCUIT_OPEN", name="GatewayProviderHealthStatus"), default="UNKNOWN", server_default=text("'UNKNOWN'"))
    circuit_opened_at: Mapped[Optional[datetime]] = mapped_column("circuitOpenedAt")
    last_health_check_at: Mapped[Optional[datetime]] = mapped_column("lastHealthCheckAt")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("ModelProvider_healthStatus_idx", "healthStatus"),
        Index("ModelProvider_fallbackPriorityGlobal_idx", "fallbackPriorityGlobal"),
        Index("ModelProvider_organizationId_idx", "organizationId"),
        Index("ModelProvider_organizationId_routingHandle_key", "organizationId", "routingHandle", unique=True),
    )

    scopes: Mapped[list["ModelProviderScope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="model_provider")

class ModelProviderScope(Base):
    __tablename__ = "ModelProviderScope"

    id: Mapped[str] = mapped_column(primary_key=True)
    model_provider_id: Mapped[str] = mapped_column("modelProviderId", ForeignKey("ModelProvider.id", ondelete="CASCADE", onupdate="CASCADE", name="ModelProviderScope_modelProviderId_fkey"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="ModelProviderScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ModelProviderScope_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("ModelProviderScope_modelProviderId_idx", "modelProviderId"),
        Index("ModelProviderScope_modelProviderId_scopeType_scopeId_key", "modelProviderId", "scopeType", "scopeId", unique=True),
    )

    model_provider: Mapped["ModelProvider"] = relationship(back_populates="scopes")

class ModelDefaultConfig(Base):
    __tablename__ = "ModelDefaultConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("ModelDefaultConfig_organizationId_idx", "organizationId"),
    )

    scopes: Mapped[list["ModelDefaultConfigScope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="config")

class ModelDefaultConfigScope(Base):
    __tablename__ = "ModelDefaultConfigScope"

    id: Mapped[str] = mapped_column(primary_key=True)
    config_id: Mapped[str] = mapped_column("configId", ForeignKey("ModelDefaultConfig.id", ondelete="CASCADE", onupdate="CASCADE", name="ModelDefaultConfigScope_configId_fkey"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="ModelDefaultScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ModelDefaultConfigScope_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("ModelDefaultConfigScope_configId_idx", "configId"),
        Index("ModelDefaultConfigScope_configId_scopeType_scopeId_key", "configId", "scopeType", "scopeId", unique=True),
    )

    config: Mapped["ModelDefaultConfig"] = relationship(back_populates="scopes")

class GithubInstallation(Base):
    __tablename__ = "GithubInstallation"

    id: Mapped[str] = mapped_column(primary_key=True)
    installation_id: Mapped[str] = mapped_column("installationId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="GithubInstallation_organizationId_fkey"))
    account_login: Mapped[str] = mapped_column("accountLogin")
    account_type: Mapped[str] = mapped_column("accountType")
    account_id: Mapped[str] = mapped_column("accountId")
    repository_selection: Mapped[str] = mapped_column("repositorySelection")
    repositories: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    suspended_at: Mapped[Optional[datetime]] = mapped_column("suspendedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GithubInstallation_installationId_key", "installationId", unique=True),
        Index("GithubInstallation_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="github_installations")

class GithubPullRequest(Base):
    __tablename__ = "GithubPullRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="GithubPullRequest_organizationId_fkey"))
    repository_host: Mapped[str] = mapped_column("repositoryHost")
    repository_full_name: Mapped[str] = mapped_column("repositoryFullName")
    head_branch: Mapped[str] = mapped_column("headBranch")
    pr_number: Mapped[int] = mapped_column("prNumber")
    html_url: Mapped[str] = mapped_column("htmlUrl")
    title: Mapped[str]
    state: Mapped[str]
    is_draft: Mapped[bool] = mapped_column("isDraft", default=False, server_default=text("false"))
    author_login: Mapped[Optional[str]] = mapped_column("authorLogin")
    pr_created_at: Mapped[datetime] = mapped_column("prCreatedAt")
    pr_closed_at: Mapped[Optional[datetime]] = mapped_column("prClosedAt")
    pr_merged_at: Mapped[Optional[datetime]] = mapped_column("prMergedAt")
    pr_updated_at: Mapped[Optional[datetime]] = mapped_column("prUpdatedAt")
    mapped_at: Mapped[datetime] = mapped_column("mappedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_checked_at: Mapped[datetime] = mapped_column("lastCheckedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GithubPullRequest_organizationId_repositoryHost_repositoryF_idx", "organizationId", "repositoryHost", "repositoryFullName", "headBranch"),
        Index("GithubPullRequest_organizationId_repositoryHost_repositoryF_key", "organizationId", "repositoryHost", "repositoryFullName", "prNumber", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="github_pull_requests")

class GithubBranchPullRequestCheck(Base):
    __tablename__ = "GithubBranchPullRequestCheck"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="GithubBranchPullRequestCheck_organizationId_fkey"))
    repository_host: Mapped[str] = mapped_column("repositoryHost")
    repository_full_name: Mapped[str] = mapped_column("repositoryFullName")
    head_branch: Mapped[str] = mapped_column("headBranch")
    last_checked_at: Mapped[datetime] = mapped_column("lastCheckedAt")
    pr_count: Mapped[int] = mapped_column("prCount", default=0, server_default=text("0"))
    not_found_at: Mapped[Optional[datetime]] = mapped_column("notFoundAt")
    recheck_after: Mapped[Optional[datetime]] = mapped_column("recheckAfter")
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_requested_at: Mapped[datetime] = mapped_column("lastRequestedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_n_idx", "recheckAfter", "lastRequestedAt", "notFoundAt"),
        Index("GithubBranchPullRequestCheck_organizationId_repositoryHost__key", "organizationId", "repositoryHost", "repositoryFullName", "headBranch", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="github_branch_pr_checks")

class LangyConversationProjection(Base):
    __tablename__ = "LangyConversationProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    conversation_id: Mapped[str] = mapped_column("conversationId")
    user_id: Mapped[str] = mapped_column("userId")
    title: Mapped[Optional[str]]
    title_source: Mapped[str] = mapped_column("titleSource", Enum("derived", "auto", "user", name="LangyProjectionTitleSource"))
    origin: Mapped[str] = mapped_column(default="interactive", server_default=text("'interactive'"))
    status: Mapped[str]
    is_shared: Mapped[bool] = mapped_column("isShared", default=False, server_default=text("false"))
    shared_at: Mapped[Optional[float]] = mapped_column("sharedAt")
    shared_by_id: Mapped[Optional[str]] = mapped_column("sharedById")
    message_count: Mapped[int] = mapped_column("messageCount", default=0, server_default=text("0"))
    last_activity_at: Mapped[Optional[float]] = mapped_column("lastActivityAt")
    current_turn_id: Mapped[Optional[str]] = mapped_column("currentTurnId")
    last_error: Mapped[Optional[str]] = mapped_column("lastError")
    last_model: Mapped[Optional[str]] = mapped_column("lastModel")
    pending_handoff_token: Mapped[Optional[str]] = mapped_column("pendingHandoffToken")
    pending_handoff_turn_id: Mapped[Optional[str]] = mapped_column("pendingHandoffTurnId")
    run_token: Mapped[Optional[str]] = mapped_column("runToken")
    archived_at: Mapped[Optional[float]] = mapped_column("archivedAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("LangyConversationProjection_projectId_userId_conversationId_idx", "projectId", "userId", "conversationId"),
        Index("LangyConversationProjection_projectId_userId_lastActivityAt_idx", "projectId", "userId", "lastActivityAt"),
        Index("LangyConversationProjection_projectId_conversationId_key", "projectId", "conversationId", unique=True),
    )

class LangyTurnRequest(Base):
    __tablename__ = "LangyTurnRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    user_id: Mapped[str] = mapped_column("userId")
    request_id: Mapped[str] = mapped_column("requestId")
    conversation_id: Mapped[str] = mapped_column("conversationId")
    turn_id: Mapped[str] = mapped_column("turnId")
    status: Mapped[str]
    lease_owner: Mapped[str] = mapped_column("leaseOwner")
    lease_expires_at: Mapped[datetime] = mapped_column("leaseExpiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LangyTurnRequest_projectId_conversationId_idx", "projectId", "conversationId"),
        Index("LangyTurnRequest_projectId_userId_requestId_key", "projectId", "userId", "requestId", unique=True),
    )

class LangyActiveTurn(Base):
    __tablename__ = "LangyActiveTurn"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    conversation_id: Mapped[str] = mapped_column("conversationId")
    turn_id: Mapped[str] = mapped_column("turnId")
    request_id: Mapped[str] = mapped_column("requestId")
    user_id: Mapped[str] = mapped_column("userId")
    status: Mapped[str]
    lease_owner: Mapped[str] = mapped_column("leaseOwner")
    lease_expires_at: Mapped[datetime] = mapped_column("leaseExpiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LangyActiveTurn_projectId_userId_idx", "projectId", "userId"),
        Index("LangyActiveTurn_projectId_conversationId_key", "projectId", "conversationId", unique=True),
    )

class LangyConversationTurnProjection(Base):
    __tablename__ = "LangyConversationTurnProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    conversation_id: Mapped[str] = mapped_column("conversationId")
    turn_id: Mapped[str] = mapped_column("turnId")
    status: Mapped[str]
    question_parts: Mapped[dict[str, Any]] = mapped_column("questionParts", JSONB)
    answer_parts: Mapped[dict[str, Any]] = mapped_column("answerParts", JSONB)
    tool_calls: Mapped[dict[str, Any]] = mapped_column("toolCalls", JSONB)
    plan: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[Optional[str]]
    started_at: Mapped[Optional[float]] = mapped_column("startedAt")
    ended_at: Mapped[Optional[float]] = mapped_column("endedAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("LangyConversationTurnProjection_projectId_conversationId_up_idx", "projectId", "conversationId", "updatedAt"),
        Index("LangyConversationTurnProjection_projectId_conversationId_tu_key", "projectId", "conversationId", "turnId", unique=True),
    )

class LangyMessageProjection(Base):
    __tablename__ = "LangyMessageProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    conversation_id: Mapped[str] = mapped_column("conversationId")
    message_id: Mapped[str] = mapped_column("messageId")
    role: Mapped[str]
    parts: Mapped[dict[str, Any]] = mapped_column(JSONB)
    source_event_id: Mapped[str] = mapped_column("sourceEventId")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")

    __table_args__ = (
        Index("LangyMessageProjection_projectId_conversationId_createdAt_idx", "projectId", "conversationId", "createdAt"),
        Index("LangyMessageProjection_projectId_conversationId_messageId_key", "projectId", "conversationId", "messageId", unique=True),
        Index("LangyMessageProjection_projectId_sourceEventId_key", "projectId", "sourceEventId", unique=True),
    )

class ProjectSecret(Base):
    __tablename__ = "ProjectSecret"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProjectSecret_projectId_fkey"))
    name: Mapped[str]
    encrypted_value: Mapped[str] = mapped_column("encryptedValue")
    created_by_id: Mapped[str] = mapped_column("createdById", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProjectSecret_createdById_fkey"))
    updated_by_id: Mapped[str] = mapped_column("updatedById", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProjectSecret_updatedById_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ProjectSecret_projectId_idx", "projectId"),
        Index("ProjectSecret_projectId_name_key", "projectId", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="project_secrets")
    created_by: Mapped["User"] = relationship(foreign_keys=[created_by_id], back_populates="secrets_created")
    updated_by: Mapped["User"] = relationship(foreign_keys=[updated_by_id], back_populates="secrets_updated")

class TriggerSent(Base):
    __tablename__ = "TriggerSent"

    id: Mapped[str] = mapped_column(primary_key=True)
    trigger_id: Mapped[str] = mapped_column("triggerId", ForeignKey("Trigger.id", ondelete="RESTRICT", onupdate="CASCADE", name="TriggerSent_triggerId_fkey"))
    trace_id: Mapped[Optional[str]] = mapped_column("traceId")
    custom_graph_id: Mapped[Optional[str]] = mapped_column("customGraphId", ForeignKey("CustomGraph.id", ondelete="CASCADE", onupdate="CASCADE", name="TriggerSent_customGraphId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="TriggerSent_projectId_fkey"))
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")
    open_incident_key: Mapped[Optional[str]] = mapped_column("openIncidentKey")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("TriggerSent_openIncidentKey_key", "openIncidentKey", unique=True),
        Index("TriggerSent_triggerId_idx", "triggerId"),
        Index("TriggerSent_projectId_idx", "projectId"),
        Index("TriggerSent_customGraphId_idx", "customGraphId"),
        Index("TriggerSent_resolvedAt_idx", "resolvedAt"),
        Index("TriggerSent_projectId_triggerId_createdAt_id_idx", "projectId", "triggerId", "createdAt", "id"),
        Index("TriggerSent_triggerId_traceId_key", "triggerId", "traceId", unique=True),
    )

    custom_graph: Mapped[Optional["CustomGraph"]] = relationship(back_populates="trigger_sent")
    project: Mapped["Project"] = relationship(back_populates="trigger_sent")
    trigger: Mapped["Trigger"] = relationship(back_populates="trigger_sent")

class TriggerLatestEvaluation(Base):
    __tablename__ = "TriggerLatestEvaluation"

    trigger_id: Mapped[str] = mapped_column("triggerId", ForeignKey("Trigger.id", ondelete="CASCADE", onupdate="CASCADE", name="TriggerLatestEvaluation_triggerId_fkey"), primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    evaluated_at: Mapped[datetime] = mapped_column("evaluatedAt", UtcDateTimeTz(precision=3))
    verdict: Mapped[str]
    observed_value: Mapped[Optional[float]] = mapped_column("observedValue")
    threshold: Mapped[Optional[float]]
    operator: Mapped[Optional[str]]
    time_period_minutes: Mapped[Optional[int]] = mapped_column("timePeriodMinutes")
    skip_code: Mapped[Optional[str]] = mapped_column("skipCode")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TriggerLatestEvaluation_projectId_idx", "projectId"),
    )

    trigger: Mapped["Trigger"] = relationship(back_populates="latest_evaluation")

class EmailSuppression(Base):
    __tablename__ = "EmailSuppression"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="EmailSuppression_projectId_fkey"))
    email: Mapped[str]
    trigger_id: Mapped[Optional[str]] = mapped_column("triggerId")
    reason: Mapped[str] = mapped_column(default="unsubscribe", server_default=text("'unsubscribe'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("EmailSuppression_projectId_idx", "projectId"),
        Index("EmailSuppression_projectId_email_triggerId_key", "projectId", "email", "triggerId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="email_suppressions")

class AnnotationScore(Base):
    __tablename__ = "AnnotationScore"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationScore_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    description: Mapped[Optional[str]]
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    data_type: Mapped[Optional[str]] = mapped_column("dataType", Enum("CATEGORICAL", "BOOLEAN", "LIKERT", "OPTION", "CHECKBOX", name="AnnotationScoreDataType"))
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    default_value: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultValue", JSONB(none_as_null=True))
    global_: Mapped[bool] = mapped_column("global", default=False, server_default=text("false"))

    __table_args__ = (
        Index("AnnotationScore_projectId_idx", "projectId"),
    )

    project: Mapped["Project"] = relationship(back_populates="annotation_scores")
    annotation_queue_scores: Mapped[list["AnnotationQueueScores"]] = relationship(passive_deletes="all", back_populates="annotation_score")

class AnnotationQueue(Base):
    __tablename__ = "AnnotationQueue"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueue_projectId_fkey"))
    description: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("AnnotationQueue_projectId_idx", "projectId"),
        Index("AnnotationQueue_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="annotation_queue")
    members: Mapped[list["AnnotationQueueMembers"]] = relationship(passive_deletes="all", back_populates="annotation_queue")
    annotation_queue_scores: Mapped[list["AnnotationQueueScores"]] = relationship(passive_deletes="all", back_populates="annotation_queue")
    annotation_queue_items: Mapped[list["AnnotationQueueItem"]] = relationship(back_populates="annotation_queue")

class AnnotationQueueMembers(Base):
    __tablename__ = "AnnotationQueueMembers"

    annotation_queue_id: Mapped[str] = mapped_column("annotationQueueId", ForeignKey("AnnotationQueue.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueueMembers_annotationQueueId_fkey"), primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueueMembers_userId_fkey"), primary_key=True)

    __table_args__ = (
        Index("AnnotationQueueMembers_annotationQueueId_idx", "annotationQueueId"),
        Index("AnnotationQueueMembers_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="annotation_queues")
    annotation_queue: Mapped["AnnotationQueue"] = relationship(back_populates="members")

class AnnotationQueueScores(Base):
    __tablename__ = "AnnotationQueueScores"

    annotation_queue_id: Mapped[str] = mapped_column("annotationQueueId", ForeignKey("AnnotationQueue.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueueScores_annotationQueueId_fkey"), primary_key=True)
    annotation_score_id: Mapped[str] = mapped_column("annotationScoreId", ForeignKey("AnnotationScore.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueueScores_annotationScoreId_fkey"), primary_key=True)

    __table_args__ = (
        Index("AnnotationQueueScores_annotationQueueId_idx", "annotationQueueId"),
        Index("AnnotationQueueScores_annotationScoreId_idx", "annotationScoreId"),
    )

    annotation_score: Mapped["AnnotationScore"] = relationship(back_populates="annotation_queue_scores")
    annotation_queue: Mapped["AnnotationQueue"] = relationship(back_populates="annotation_queue_scores")

class AnnotationQueueItem(Base):
    __tablename__ = "AnnotationQueueItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    annotation_queue_id: Mapped[Optional[str]] = mapped_column("annotationQueueId", ForeignKey("AnnotationQueue.id", ondelete="SET NULL", onupdate="CASCADE", name="AnnotationQueueItem_annotationQueueId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="AnnotationQueueItem_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="AnnotationQueueItem_createdByUserId_fkey"))
    trace_id: Mapped[str] = mapped_column("traceId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="AnnotationQueueItem_projectId_fkey"))
    done_at: Mapped[Optional[datetime]] = mapped_column("doneAt")
    marked_for_dataset_at: Mapped[Optional[datetime]] = mapped_column("markedForDatasetAt")

    __table_args__ = (
        Index("AnnotationQueueItem_annotationQueueId_idx", "annotationQueueId"),
        Index("AnnotationQueueItem_userId_idx", "userId"),
        Index("AnnotationQueueItem_projectId_idx", "projectId"),
        Index("AnnotationQueueItem_projectId_doneAt_idx", "projectId", "doneAt"),
        Index("AnnotationQueueItem_projectId_userId_doneAt_idx", "projectId", "userId", "doneAt"),
        Index("AnnotationQueueItem_annotationQueueId_doneAt_idx", "annotationQueueId", "doneAt"),
        Index("AnnotationQueueItem_traceId_userId_projectId_key", "traceId", "userId", "projectId", unique=True),
        Index("AnnotationQueueItem_traceId_annotationQueueId_projectId_key", "traceId", "annotationQueueId", "projectId", unique=True),
    )

    annotation_queue: Mapped[Optional["AnnotationQueue"]] = relationship(back_populates="annotation_queue_items")
    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="assigned_queue_items")
    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_user_id], back_populates="created_queue_items")
    project: Mapped["Project"] = relationship(back_populates="annotation_queue_item")

class ShareLink(Base):
    __tablename__ = "ShareLink"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    resource_type: Mapped[str] = mapped_column("resourceType", Enum("TRACE", "THREAD", name="ShareResourceType"))
    resource_id: Mapped[str] = mapped_column("resourceId")
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="ShareLink_projectId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="ShareLink_userId_fkey"))
    visibility: Mapped[str] = mapped_column(Enum("PUBLIC", "ORGANIZATION", "PROJECT", name="ShareVisibility"), default="PUBLIC", server_default=text("'PUBLIC'"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    max_views: Mapped[Optional[int]] = mapped_column("maxViews")
    view_count: Mapped[int] = mapped_column("viewCount", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ShareLink_token_key", "token", unique=True),
        Index("ShareLink_projectId_resourceType_resourceId_idx", "projectId", "resourceType", "resourceId"),
        Index("ShareLink_userId_idx", "userId"),
    )

    project: Mapped["Project"] = relationship(back_populates="share_links")
    user: Mapped[Optional["User"]] = relationship(back_populates="share_links")

class PinnedTrace(Base):
    __tablename__ = "PinnedTrace"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="PinnedTrace_projectId_fkey"))
    trace_id: Mapped[str] = mapped_column("traceId")
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="PinnedTrace_userId_fkey"))
    source: Mapped[str] = mapped_column(Enum("manual", "share", name="PinSource"), default="manual", server_default=text("'manual'"))
    reason: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("PinnedTrace_projectId_idx", "projectId"),
        Index("PinnedTrace_projectId_traceId_key", "projectId", "traceId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="pinned_traces")
    user: Mapped[Optional["User"]] = relationship(back_populates="pinned_traces")

class TraceEditOverlay(Base):
    __tablename__ = "TraceEditOverlay"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="TraceEditOverlay_projectId_fkey"))
    trace_id: Mapped[str] = mapped_column("traceId")
    patch: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="TraceEditOverlay_createdById_fkey"))
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="TraceEditOverlay_updatedById_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("TraceEditOverlay_projectId_idx", "projectId"),
        Index("TraceEditOverlay_projectId_traceId_key", "projectId", "traceId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="trace_edit_overlays")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="trace_edit_overlays_created")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="trace_edit_overlays_updated")

class TraceIngestSourceBilling(Base):
    __tablename__ = "TraceIngestSourceBilling"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    source_type: Mapped[str] = mapped_column("sourceType", primary_key=True)
    billed: Mapped[bool]
    recorded_at: Mapped[datetime] = mapped_column("recordedAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class RetentionPolicy(Base):
    __tablename__ = "RetentionPolicy"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="RetentionPolicyScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    category: Mapped[str]
    retention_days: Mapped[int] = mapped_column("retentionDays")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("RetentionPolicy_organizationId_idx", "organizationId"),
        Index("RetentionPolicy_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("RetentionPolicy_scopeType_scopeId_category_key", "scopeType", "scopeId", "category", unique=True),
    )

class DataPrivacyPolicy(Base):
    __tablename__ = "DataPrivacyPolicy"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "DEPARTMENT", "TEAM", "PROJECT", name="DataPrivacyScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    personal_only: Mapped[bool] = mapped_column("personalOnly", default=False, server_default=text("false"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DataPrivacyPolicy_organizationId_idx", "organizationId"),
        Index("DataPrivacyPolicy_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("DataPrivacyPolicy_scopeType_scopeId_personalOnly_key", "scopeType", "scopeId", "personalOnly", unique=True),
    )

class InstantEvalJudgeProject(Base):
    __tablename__ = "InstantEvalJudgeProject"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    created_at: Mapped[datetime] = mapped_column("createdAt")

class InstantEvalJudgeUsageBilling(Base):
    __tablename__ = "InstantEvalJudgeUsageBilling"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    usage_billed: Mapped[bool] = mapped_column("usageBilled")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    from_catch_up: Mapped[bool] = mapped_column("fromCatchUp")

class InstantEvalJudgeSpend(Base):
    __tablename__ = "InstantEvalJudgeSpend"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    request_id: Mapped[str] = mapped_column("requestId", primary_key=True)
    spend_nano_usd: Mapped[int] = mapped_column("spendNanoUsd", BigInteger)
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")

class CustomLLMModelCost(Base):
    __tablename__ = "CustomLLMModelCost"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="CustomLLMModelCostScopeType"), default="PROJECT", server_default=text("'PROJECT'"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    model: Mapped[str]
    regex: Mapped[str]
    input_cost_per_token: Mapped[Optional[float]] = mapped_column("inputCostPerToken")
    output_cost_per_token: Mapped[Optional[float]] = mapped_column("outputCostPerToken")
    cache_read_cost_per_token: Mapped[Optional[float]] = mapped_column("cacheReadCostPerToken")
    cache_creation_cost_per_token: Mapped[Optional[float]] = mapped_column("cacheCreationCostPerToken")
    cache_creation1h_cost_per_token: Mapped[Optional[float]] = mapped_column("cacheCreation1hCostPerToken")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("CustomLLMModelCost_organizationId_idx", "organizationId"),
        Index("CustomLLMModelCost_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("CustomLLMModelCost_projectId_idx", "projectId"),
    )

class Workflow(Base):
    __tablename__ = "Workflow"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Workflow_projectId_fkey"))
    name: Mapped[str]
    icon: Mapped[str]
    description: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    latest_version_id: Mapped[Optional[str]] = mapped_column("latestVersionId", ForeignKey("WorkflowVersion.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Workflow_latestVersionId_fkey"))
    current_version_id: Mapped[Optional[str]] = mapped_column("currentVersionId", ForeignKey("WorkflowVersion.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Workflow_currentVersionId_fkey"))
    published_id: Mapped[Optional[str]] = mapped_column("publishedId")
    published_by_id: Mapped[Optional[str]] = mapped_column("publishedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Workflow_publishedById_fkey"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    is_evaluator: Mapped[bool] = mapped_column("isEvaluator", default=False, server_default=text("false"))
    is_component: Mapped[bool] = mapped_column("isComponent", default=False, server_default=text("false"))
    copied_from_workflow_id: Mapped[Optional[str]] = mapped_column("copiedFromWorkflowId", ForeignKey("Workflow.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Workflow_copiedFromWorkflowId_fkey"))

    __table_args__ = (
        Index("Workflow_projectId_idx", "projectId"),
        Index("Workflow_publishedById_idx", "publishedById"),
        Index("Workflow_latestVersionId_idx", "latestVersionId"),
        Index("Workflow_currentVersionId_idx", "currentVersionId"),
        Index("Workflow_copiedFromWorkflowId_idx", "copiedFromWorkflowId"),
    )

    project: Mapped["Project"] = relationship(back_populates="workflows")
    latest_version: Mapped[Optional["WorkflowVersion"]] = relationship(foreign_keys=[latest_version_id], back_populates="workflow_as_latest_version")
    current_version: Mapped[Optional["WorkflowVersion"]] = relationship(foreign_keys=[current_version_id], back_populates="workflow_as_current_version")
    published_by: Mapped[Optional["User"]] = relationship(back_populates="workflow")
    copied_from: Mapped[Optional["Workflow"]] = relationship(remote_side=[id], back_populates="copied_workflows")
    versions: Mapped[list["WorkflowVersion"]] = relationship(foreign_keys="WorkflowVersion.workflow_id", passive_deletes="all", back_populates="workflow")
    experiments: Mapped[list["Experiment"]] = relationship(back_populates="workflow")
    copied_workflows: Mapped[list["Workflow"]] = relationship(passive_deletes="all", back_populates="copied_from")
    agents: Mapped[list["Agent"]] = relationship(back_populates="workflow")
    evaluators: Mapped[list["Evaluator"]] = relationship(back_populates="workflow")

class WorkflowVersion(Base):
    __tablename__ = "WorkflowVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[str]
    commit_message: Mapped[str] = mapped_column("commitMessage")
    author_id: Mapped[str] = mapped_column("authorId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkflowVersion_authorId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkflowVersion_projectId_fkey"))
    workflow_id: Mapped[str] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkflowVersion_workflowId_fkey"))
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("WorkflowVersion.id", ondelete="RESTRICT", onupdate="RESTRICT", name="WorkflowVersion_parentId_fkey"))
    auto_saved: Mapped[bool] = mapped_column("autoSaved", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    dsl: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("WorkflowVersion_workflowId_idx", "workflowId"),
        Index("WorkflowVersion_projectId_idx", "projectId"),
        Index("WorkflowVersion_parentId_idx", "parentId"),
        Index("WorkflowVersion_authorId_idx", "authorId"),
        Index("WorkflowVersion_createdAt_idx", "createdAt"),
        Index("WorkflowVersion_workflowId_version_key", "workflowId", "version", unique=True),
    )

    author: Mapped["User"] = relationship(back_populates="workflow_version")
    project: Mapped["Project"] = relationship(back_populates="workflow_version")
    workflow: Mapped["Workflow"] = relationship(foreign_keys=[workflow_id], back_populates="versions")
    parent: Mapped[Optional["WorkflowVersion"]] = relationship(remote_side=[id], back_populates="children")
    children: Mapped[list["WorkflowVersion"]] = relationship(passive_deletes="all", back_populates="parent")
    workflow_as_latest_version: Mapped[list["Workflow"]] = relationship(foreign_keys="Workflow.latest_version_id", passive_deletes="all", back_populates="latest_version")
    workflow_as_current_version: Mapped[list["Workflow"]] = relationship(foreign_keys="Workflow.current_version_id", passive_deletes="all", back_populates="current_version")

class AuditLog(Base):
    __tablename__ = "AuditLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    action: Mapped[str]
    args: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[Optional[str]]
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    actor_user_id: Mapped[Optional[str]] = mapped_column("actorUserId")
    target_kind: Mapped[Optional[str]] = mapped_column("targetKind")
    target_id: Mapped[Optional[str]] = mapped_column("targetId")
    before: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    after: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")

    __table_args__ = (
        Index("AuditLog_idempotencyKey_key", "idempotencyKey", unique=True),
        Index("AuditLog_createdAt_idx", "createdAt"),
        Index("AuditLog_userId_idx", "userId"),
        Index("AuditLog_actorUserId_idx", "actorUserId"),
        Index("AuditLog_projectId_idx", "projectId"),
        Index("AuditLog_action_idx", "action"),
        Index("AuditLog_ipAddress_idx", "ipAddress"),
        Index("AuditLog_organizationId_createdAt_idx", "organizationId", "createdAt"),
        Index("AuditLog_targetKind_targetId_idx", "targetKind", "targetId"),
    )

class LlmPromptConfig(Base):
    __tablename__ = "LlmPromptConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    handle: Mapped[Optional[str]]
    name: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="LlmPromptConfig_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    organization_id: Mapped[str] = mapped_column("organizationId")
    scope: Mapped[str] = mapped_column(Enum("ORGANIZATION", "PROJECT", name="PromptScope"), default="PROJECT", server_default=text("'PROJECT'"))
    copied_from_prompt_id: Mapped[Optional[str]] = mapped_column("copiedFromPromptId", ForeignKey("LlmPromptConfig.id", ondelete="RESTRICT", onupdate="RESTRICT", name="LlmPromptConfig_copiedFromPromptId_fkey"))

    __table_args__ = (
        Index("LlmPromptConfig_projectId_idx", "projectId"),
        Index("LlmPromptConfig_organizationId_scope_idx", "organizationId", "scope"),
        Index("LlmPromptConfig_copiedFromPromptId_idx", "copiedFromPromptId"),
        Index("LlmPromptConfig_handle_key", "handle", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="llm_prompt_configs")
    copied_from: Mapped[Optional["LlmPromptConfig"]] = relationship(remote_side=[id], back_populates="copied_prompts")
    copied_prompts: Mapped[list["LlmPromptConfig"]] = relationship(passive_deletes="all", back_populates="copied_from")
    versions: Mapped[list["LlmPromptConfigVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="config")
    version_tags: Mapped[list["PromptTagAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="config")

class LlmPromptConfigVersion(Base):
    __tablename__ = "LlmPromptConfigVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[int]
    commit_message: Mapped[Optional[str]] = mapped_column("commitMessage")
    author_id: Mapped[Optional[str]] = mapped_column("authorId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="LlmPromptConfigVersion_authorId_fkey"))
    config_id: Mapped[str] = mapped_column("configId", ForeignKey("LlmPromptConfig.id", ondelete="CASCADE", onupdate="CASCADE", name="LlmPromptConfigVersion_configId_fkey"))
    config_data: Mapped[dict[str, Any]] = mapped_column("config", JSONB)
    schema_version: Mapped[str] = mapped_column("schemaVersion")
    runtime_parameters: Mapped[dict[str, Any]] = mapped_column("runtimeParameters", JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    project_id: Mapped[str] = mapped_column("projectId")

    __table_args__ = (
        Index("LlmPromptConfigVersion_configId_idx", "configId"),
        Index("LlmPromptConfigVersion_authorId_idx", "authorId"),
        Index("LlmPromptConfigVersion_createdAt_idx", "createdAt"),
        Index("LlmPromptConfigVersion_configId_version_key", "configId", "version", unique=True),
    )

    author: Mapped[Optional["User"]] = relationship(back_populates="llm_prompt_config_versions")
    config: Mapped["LlmPromptConfig"] = relationship(back_populates="versions")
    version_tags: Mapped[list["PromptTagAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="version")

class PromptTagAssignment(Base):
    __tablename__ = "PromptTagAssignment"

    id: Mapped[str] = mapped_column(primary_key=True)
    config_id: Mapped[str] = mapped_column("configId", ForeignKey("LlmPromptConfig.id", ondelete="CASCADE", onupdate="CASCADE", name="PromptTagAssignment_configId_fkey"))
    version_id: Mapped[str] = mapped_column("versionId", ForeignKey("LlmPromptConfigVersion.id", ondelete="CASCADE", onupdate="CASCADE", name="PromptTagAssignment_versionId_fkey"))
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("PromptTag.id", ondelete="CASCADE", onupdate="CASCADE", name="PromptTagAssignment_tagId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="PromptTagAssignment_createdById_fkey"))
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="PromptTagAssignment_updatedById_fkey"))

    __table_args__ = (
        Index("PromptTagAssignment_configId_idx", "configId"),
        Index("PromptTagAssignment_versionId_idx", "versionId"),
        Index("PromptTagAssignment_projectId_idx", "projectId"),
        Index("PromptTagAssignment_tagId_idx", "tagId"),
        Index("PromptTagAssignment_configId_tagId_key", "configId", "tagId", unique=True),
    )

    config: Mapped["LlmPromptConfig"] = relationship(back_populates="version_tags")
    version: Mapped["LlmPromptConfigVersion"] = relationship(back_populates="version_tags")
    prompt_tag: Mapped["PromptTag"] = relationship(back_populates="assignments")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="version_tags_created")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="version_tags_updated")

class PromptTag(Base):
    __tablename__ = "PromptTag"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="PromptTag_organizationId_fkey"))
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="PromptTag_createdById_fkey"))
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="PromptTag_updatedById_fkey"))

    __table_args__ = (
        Index("PromptTag_organizationId_idx", "organizationId"),
        Index("PromptTag_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="prompt_tags")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="prompt_tags_created")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="prompt_tags_updated")
    assignments: Mapped[list["PromptTagAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="prompt_tag")

class Analytics(Base):
    __tablename__ = "Analytics"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Analytics_projectId_fkey"))
    key: Mapped[str]
    value: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    numeric_value: Mapped[Optional[float]] = mapped_column("numericValue")
    string_value: Mapped[Optional[str]] = mapped_column("stringValue")
    bool_value: Mapped[Optional[bool]] = mapped_column("boolValue")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("Analytics_createdAt_key_projectId_numericValue_idx", "createdAt", "key", "projectId", "numericValue"),
        Index("Analytics_projectId_idx", "projectId"),
    )

    project: Mapped["Project"] = relationship(back_populates="analytics")

class CustomRole(Base):
    __tablename__ = "CustomRole"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="CustomRole_organizationId_fkey"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    permissions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    kind: Mapped[str] = mapped_column(default="custom", server_default=text("'custom'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("CustomRole_organizationId_idx", "organizationId"),
        Index("CustomRole_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="custom_roles")
    assigned_users: Mapped[list["TeamUser"]] = relationship(back_populates="assigned_role")
    role_bindings: Mapped[list["RoleBinding"]] = relationship(back_populates="custom_role")

class Notification(Base):
    __tablename__ = "Notification"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Notification_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Notification_projectId_fkey"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    sent_at: Mapped[datetime] = mapped_column("sentAt")

    __table_args__ = (
        Index("Notification_organizationId_idx", "organizationId"),
        Index("Notification_projectId_idx", "projectId"),
        Index("Notification_createdAt_idx", "createdAt"),
        Index("Notification_sentAt_idx", "sentAt"),
    )

    organization: Mapped[Optional["Organization"]] = relationship(back_populates="notifications")
    project: Mapped[Optional["Project"]] = relationship(back_populates="notifications")

class Agent(Base):
    __tablename__ = "Agent"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Agent_projectId_fkey"))
    name: Mapped[str]
    type: Mapped[str]
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="SET NULL", onupdate="CASCADE", name="Agent_workflowId_fkey"))
    copied_from_agent_id: Mapped[Optional[str]] = mapped_column("copiedFromAgentId", ForeignKey("Agent.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Agent_copiedFromAgentId_fkey"))
    environment: Mapped[Optional[str]]
    owner_user_id: Mapped[Optional[str]] = mapped_column("ownerUserId")
    host_label: Mapped[Optional[str]] = mapped_column("hostLabel")
    identity_key: Mapped[Optional[str]] = mapped_column("identityKey")
    last_seen_at: Mapped[Optional[datetime]] = mapped_column("lastSeenAt")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")

    __table_args__ = (
        Index("Agent_projectId_idx", "projectId"),
        Index("Agent_workflowId_idx", "workflowId"),
        Index("Agent_copiedFromAgentId_idx", "copiedFromAgentId"),
        Index("Agent_projectId_lastSeenAt_idx", "projectId", "lastSeenAt"),
        Index("Agent_projectId_identityKey_key", "projectId", "identityKey", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="agents")
    workflow: Mapped[Optional["Workflow"]] = relationship(back_populates="agents")
    copied_from: Mapped[Optional["Agent"]] = relationship(remote_side=[id], back_populates="copied_agents")
    copied_agents: Mapped[list["Agent"]] = relationship(passive_deletes="all", back_populates="copied_from")

class AgentVersion(Base):
    __tablename__ = "AgentVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    agent_id: Mapped[str] = mapped_column("agentId")
    version: Mapped[int]
    name: Mapped[str]
    type: Mapped[str]
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId")
    commit_message: Mapped[Optional[str]] = mapped_column("commitMessage")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    author_kind: Mapped[Optional[str]] = mapped_column("authorKind")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AgentVersion_projectId_idx", "projectId"),
        Index("AgentVersion_agentId_version_key", "agentId", "version", unique=True),
    )

class AgentVersionLabel(Base):
    __tablename__ = "AgentVersionLabel"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    agent_id: Mapped[str] = mapped_column("agentId")
    label: Mapped[str]
    version_id: Mapped[str] = mapped_column("versionId")
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("AgentVersionLabel_projectId_idx", "projectId"),
        Index("AgentVersionLabel_versionId_idx", "versionId"),
        Index("AgentVersionLabel_agentId_label_key", "agentId", "label", unique=True),
    )

class Evaluator(Base):
    __tablename__ = "Evaluator"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Evaluator_projectId_fkey"))
    name: Mapped[str]
    slug: Mapped[Optional[str]]
    type: Mapped[str]
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="SET NULL", onupdate="CASCADE", name="Evaluator_workflowId_fkey"))
    copied_from_evaluator_id: Mapped[Optional[str]] = mapped_column("copiedFromEvaluatorId", ForeignKey("Evaluator.id", ondelete="RESTRICT", onupdate="RESTRICT", name="Evaluator_copiedFromEvaluatorId_fkey"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")

    __table_args__ = (
        Index("Evaluator_projectId_idx", "projectId"),
        Index("Evaluator_workflowId_idx", "workflowId"),
        Index("Evaluator_copiedFromEvaluatorId_idx", "copiedFromEvaluatorId"),
        Index("Evaluator_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="evaluators")
    workflow: Mapped[Optional["Workflow"]] = relationship(back_populates="evaluators")
    copied_from: Mapped[Optional["Evaluator"]] = relationship(remote_side=[id], back_populates="copied_evaluators")
    copied_evaluators: Mapped[list["Evaluator"]] = relationship(passive_deletes="all", back_populates="copied_from")
    monitors: Mapped[list["Monitor"]] = relationship(back_populates="evaluator")
    gateway_guardrails: Mapped[list["GatewayGuardrail"]] = relationship(passive_deletes="all", back_populates="evaluator")

class EvaluatorVersion(Base):
    __tablename__ = "EvaluatorVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    evaluator_id: Mapped[str] = mapped_column("evaluatorId")
    version: Mapped[int]
    name: Mapped[str]
    type: Mapped[str]
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId")
    commit_message: Mapped[Optional[str]] = mapped_column("commitMessage")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    author_kind: Mapped[Optional[str]] = mapped_column("authorKind")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("EvaluatorVersion_projectId_idx", "projectId"),
        Index("EvaluatorVersion_evaluatorId_version_key", "evaluatorId", "version", unique=True),
    )

class EvaluatorVersionLabel(Base):
    __tablename__ = "EvaluatorVersionLabel"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    evaluator_id: Mapped[str] = mapped_column("evaluatorId")
    label: Mapped[str]
    version_id: Mapped[str] = mapped_column("versionId")
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("EvaluatorVersionLabel_projectId_idx", "projectId"),
        Index("EvaluatorVersionLabel_versionId_idx", "versionId"),
        Index("EvaluatorVersionLabel_evaluatorId_label_key", "evaluatorId", "label", unique=True),
    )

class Scenario(Base):
    __tablename__ = "Scenario"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Scenario_projectId_fkey"))
    name: Mapped[str]
    situation: Mapped[str]
    criteria: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    labels: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    parameters: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    simulator_model: Mapped[Optional[str]] = mapped_column("simulatorModel")
    judge_model: Mapped[Optional[str]] = mapped_column("judgeModel")
    max_turns: Mapped[Optional[int]] = mapped_column("maxTurns")
    min_turns: Mapped[Optional[int]] = mapped_column("minTurns")
    fields: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    caller_voice: Mapped[Optional[dict[str, Any]]] = mapped_column("callerVoice", JSONB(none_as_null=True))
    test_suite_id: Mapped[Optional[str]] = mapped_column("testSuiteId", ForeignKey("SimulationSuite.id", ondelete="SET NULL", onupdate="CASCADE", name="Scenario_testSuiteId_fkey"))
    version: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    last_updated_by_id: Mapped[Optional[str]] = mapped_column("lastUpdatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Scenario_lastUpdatedById_fkey"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Scenario_projectId_idx", "projectId"),
        Index("Scenario_projectId_testSuiteId_idx", "projectId", "testSuiteId"),
    )

    project: Mapped["Project"] = relationship(back_populates="scenarios")
    test_suite: Mapped[Optional["SimulationSuite"]] = relationship(back_populates="test_suite_scenarios")
    last_updated_by: Mapped[Optional["User"]] = relationship(back_populates="scenarios")
    versions: Mapped[list["ScenarioVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scenario")

class ScenarioVersion(Base):
    __tablename__ = "ScenarioVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    scenario_id: Mapped[str] = mapped_column("scenarioId", ForeignKey("Scenario.id", ondelete="CASCADE", onupdate="CASCADE", name="ScenarioVersion_scenarioId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId")
    version: Mapped[int]
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    author_label: Mapped[str] = mapped_column("authorLabel")
    change_description: Mapped[Optional[str]] = mapped_column("changeDescription")
    snapshot: Mapped[dict[str, Any]] = mapped_column(JSONB)
    schema_version: Mapped[int] = mapped_column("schemaVersion", default=1, server_default=text("1"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ScenarioVersion_projectId_scenarioId_version_idx", "projectId", "scenarioId", "version"),
        Index("ScenarioVersion_createdAt_idx", "createdAt"),
        Index("ScenarioVersion_scenarioId_version_key", "scenarioId", "version", unique=True),
    )

    scenario: Mapped["Scenario"] = relationship(back_populates="versions")

class SimulationSuite(Base):
    __tablename__ = "SimulationSuite"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="SimulationSuite_projectId_fkey"))
    name: Mapped[str]
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    scenario_ids: Mapped[list[str]] = mapped_column("scenarioIds", ARRAY(Text), nullable=True)
    targets: Mapped[dict[str, Any]] = mapped_column(JSONB)
    repeat_count: Mapped[int] = mapped_column("repeatCount", default=1, server_default=text("1"))
    labels: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    simulator_model: Mapped[Optional[str]] = mapped_column("simulatorModel")
    judge_model: Mapped[Optional[str]] = mapped_column("judgeModel")
    kind: Mapped[str] = mapped_column(default="run_plan", server_default=text("'run_plan'"))
    scope: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    fields: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    evaluators: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SimulationSuite_projectId_idx", "projectId"),
        Index("SimulationSuite_projectId_kind_idx", "projectId", "kind"),
        Index("SimulationSuite_projectId_slug_key", "projectId", "slug", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="suite_configurations")
    test_suite_scenarios: Mapped[list["Scenario"]] = relationship(back_populates="test_suite")

class Subscription(Base):
    __tablename__ = "Subscription"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Subscription_organizationId_fkey"))
    plan: Mapped[str] = mapped_column(Enum("FREE", "PRO", "GROWTH", "GROWTH_SEAT_EUR_MONTHLY", "GROWTH_SEAT_EUR_ANNUAL", "GROWTH_SEAT_USD_MONTHLY", "GROWTH_SEAT_USD_ANNUAL", "ENTERPRISE", "LAUNCH", "ACCELERATE", "LAUNCH_ANNUAL", "ACCELERATE_ANNUAL", name="PlanTypes"))
    stripe_subscription_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionId")
    status: Mapped[str] = mapped_column(Enum("PENDING", "FAILED", "ACTIVE", "CANCELLED", name="SubscriptionStatus"))
    start_date: Mapped[Optional[datetime]] = mapped_column("startDate")
    end_date: Mapped[Optional[datetime]] = mapped_column("endDate")
    last_payment_failed_date: Mapped[Optional[datetime]] = mapped_column("lastPaymentFailedDate")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    max_members: Mapped[Optional[int]] = mapped_column("maxMembers")
    max_projects: Mapped[Optional[int]] = mapped_column("maxProjects")
    max_messages_per_month: Mapped[Optional[int]] = mapped_column("maxMessagesPerMonth")
    max_workflows: Mapped[Optional[int]] = mapped_column("maxWorkflows")
    max_retention_days: Mapped[Optional[int]] = mapped_column("maxRetentionDays")
    max_members_lite: Mapped[Optional[int]] = mapped_column("maxMembersLite")
    max_members_developer: Mapped[Optional[int]] = mapped_column("maxMembersDeveloper")
    max_teams: Mapped[Optional[int]] = mapped_column("maxTeams")
    max_prompts: Mapped[Optional[int]] = mapped_column("maxPrompts")
    max_evaluators: Mapped[Optional[int]] = mapped_column("maxEvaluators")
    max_scenarios: Mapped[Optional[int]] = mapped_column("maxScenarios")
    max_agents: Mapped[Optional[int]] = mapped_column("maxAgents")
    max_experiments: Mapped[Optional[int]] = mapped_column("maxExperiments")
    max_online_evaluations: Mapped[Optional[int]] = mapped_column("maxOnlineEvaluations")
    max_datasets: Mapped[Optional[int]] = mapped_column("maxDatasets")
    max_dashboards: Mapped[Optional[int]] = mapped_column("maxDashboards")
    max_custom_graphs: Mapped[Optional[int]] = mapped_column("maxCustomGraphs")
    max_automations: Mapped[Optional[int]] = mapped_column("maxAutomations")
    seats_kept: Mapped[Optional[int]] = mapped_column("seatsKept")
    seats_kept_until: Mapped[Optional[datetime]] = mapped_column("seatsKeptUntil")
    seat_line_quantity: Mapped[Optional[int]] = mapped_column("seatLineQuantity")
    seat_line_quantity_at: Mapped[Optional[datetime]] = mapped_column("seatLineQuantityAt")
    seat_line_write_quantities: Mapped[list[int]] = mapped_column("seatLineWriteQuantities", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    seat_line_write_since: Mapped[Optional[datetime]] = mapped_column("seatLineWriteSince")
    seat_line_write_at: Mapped[Optional[datetime]] = mapped_column("seatLineWriteAt")
    seat_line_write_attempts: Mapped[Optional[int]] = mapped_column("seatLineWriteAttempts")
    seat_line_write_flagged_at: Mapped[Optional[datetime]] = mapped_column("seatLineWriteFlaggedAt")
    seat_repair_quantity: Mapped[Optional[int]] = mapped_column("seatRepairQuantity")
    seat_repair_lifted_quantity: Mapped[Optional[int]] = mapped_column("seatRepairLiftedQuantity")
    seat_repair_since: Mapped[Optional[datetime]] = mapped_column("seatRepairSince")
    seat_repair_attempts: Mapped[Optional[int]] = mapped_column("seatRepairAttempts")
    seat_repair_flagged_at: Mapped[Optional[datetime]] = mapped_column("seatRepairFlaggedAt")
    seat_line_lease_token: Mapped[Optional[str]] = mapped_column("seatLineLeaseToken")
    seat_line_lease_until: Mapped[Optional[datetime]] = mapped_column("seatLineLeaseUntil")

    __table_args__ = (
        Index("Subscription_stripeSubscriptionId_key", "stripeSubscriptionId", unique=True),
        Index("Subscription_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="subscriptions")
    invoices: Mapped[list["Invoice"]] = relationship(passive_deletes="all", back_populates="subscription")

class Invoice(Base):
    __tablename__ = "Invoice"

    id: Mapped[str] = mapped_column(primary_key=True)
    subscription_id: Mapped[str] = mapped_column("subscriptionId", ForeignKey("Subscription.id", ondelete="RESTRICT", onupdate="CASCADE", name="Invoice_subscriptionId_fkey"))
    total_amount: Mapped[float] = mapped_column("totalAmount")
    currency: Mapped[str]
    due_date: Mapped[datetime] = mapped_column("dueDate")
    paid: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Invoice_subscriptionId_idx", "subscriptionId"),
    )

    subscription: Mapped["Subscription"] = relationship(back_populates="invoices")
    line_items: Mapped[list["InvoiceItem"]] = relationship(passive_deletes="all", back_populates="invoice")

class InvoiceItem(Base):
    __tablename__ = "InvoiceItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    invoice_id: Mapped[str] = mapped_column("invoiceId", ForeignKey("Invoice.id", ondelete="RESTRICT", onupdate="CASCADE", name="InvoiceItem_invoiceId_fkey"))
    description: Mapped[str]
    amount: Mapped[float]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("InvoiceItem_invoiceId_idx", "invoiceId"),
    )

    invoice: Mapped["Invoice"] = relationship(back_populates="line_items")

class Group(Base):
    __tablename__ = "Group"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Group_organizationId_fkey"))
    name: Mapped[str]
    slug: Mapped[str]
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    scim_source: Mapped[Optional[str]] = mapped_column("scimSource")
    scim_connection_id: Mapped[Optional[str]] = mapped_column("scimConnectionId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Group_organizationId_idx", "organizationId"),
        Index("Group_scimConnectionId_idx", "scimConnectionId"),
        Index("Group_organizationId_slug_key", "organizationId", "slug", unique=True),
        Index("Group_scimConnectionId_externalId_key", "scimConnectionId", "externalId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="groups")
    members: Mapped[list["GroupMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    role_bindings: Mapped[list["RoleBinding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")

class GroupMembership(Base):
    __tablename__ = "GroupMembership"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="GroupMembership_userId_fkey"), primary_key=True)
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("Group.id", ondelete="CASCADE", onupdate="CASCADE", name="GroupMembership_groupId_fkey"), primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("GroupMembership_userId_idx", "userId"),
        Index("GroupMembership_groupId_idx", "groupId"),
    )

    user: Mapped["User"] = relationship(back_populates="group_memberships")
    group: Mapped["Group"] = relationship(back_populates="members")

class AuthzUserStanding(Base):
    __tablename__ = "AuthzUserStanding"

    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    deactivated_at: Mapped[Optional[datetime]] = mapped_column("deactivatedAt")
    erased_at: Mapped[Optional[datetime]] = mapped_column("erasedAt")
    standing_changed_at: Mapped[datetime] = mapped_column("standingChangedAt")

class AuthzEngineSwitch(Base):
    __tablename__ = "AuthzEngineSwitch"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    on_engine: Mapped[bool] = mapped_column("onEngine")
    pinned: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    switched_at: Mapped[datetime] = mapped_column("switchedAt")
    last_applied_event_id: Mapped[Optional[str]] = mapped_column("lastAppliedEventId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class AuthzOrganizationPolicy(Base):
    __tablename__ = "AuthzOrganizationPolicy"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    braids_organization_admins_only: Mapped[bool] = mapped_column("braidsOrganizationAdminsOnly", default=False, server_default=text("false"))
    updated_by_user_id: Mapped[str] = mapped_column("updatedByUserId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class RoleBinding(Base):
    __tablename__ = "RoleBinding"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RoleBinding_organizationId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="RoleBinding_userId_fkey"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("Group.id", ondelete="CASCADE", onupdate="CASCADE", name="RoleBinding_groupId_fkey"))
    api_key_id: Mapped[Optional[str]] = mapped_column("apiKeyId", ForeignKey("ApiKey.id", ondelete="RESTRICT", onupdate="CASCADE", name="RoleBinding_apiKeyId_fkey"))
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", "VIEWER", "CUSTOM", name="TeamUserRole"))
    custom_role_id: Mapped[Optional[str]] = mapped_column("customRoleId", ForeignKey("CustomRole.id", ondelete="SET NULL", onupdate="CASCADE", name="RoleBinding_customRoleId_fkey"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="RoleBindingScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("RoleBinding_organizationId_idx", "organizationId"),
        Index("RoleBinding_userId_idx", "userId"),
        Index("RoleBinding_groupId_idx", "groupId"),
        Index("RoleBinding_apiKeyId_idx", "apiKeyId"),
        Index("RoleBinding_scopeType_scopeId_idx", "scopeType", "scopeId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="role_bindings")
    user: Mapped[Optional["User"]] = relationship(back_populates="role_bindings")
    group: Mapped[Optional["Group"]] = relationship(back_populates="role_bindings")
    api_key: Mapped[Optional["ApiKey"]] = relationship(back_populates="role_bindings")
    custom_role: Mapped[Optional["CustomRole"]] = relationship(back_populates="role_bindings")

class Grant(Base):
    __tablename__ = "Grant"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    principal_type: Mapped[str] = mapped_column("principalType", Enum("USER", "API_KEY", "GROUP", "TEAM", "ORGANIZATION", "PROJECT", "ANYONE", name="GrantPrincipalType"))
    principal_id: Mapped[Optional[str]] = mapped_column("principalId")
    role_key: Mapped[Optional[str]] = mapped_column("roleKey")
    legacy_role: Mapped[Optional[str]] = mapped_column("legacyRole")
    source: Mapped[str]
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", "RESOURCE", "PLATFORM", name="GrantScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    token: Mapped[Optional[str]]
    permission: Mapped[Optional[str]]
    resource_kind: Mapped[Optional[str]] = mapped_column("resourceKind")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    max_views: Mapped[Optional[int]] = mapped_column("maxViews")
    condition: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    replaces_grant_id: Mapped[Optional[str]] = mapped_column("replacesGrantId")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_reason: Mapped[Optional[str]] = mapped_column("revokedReason")

    __table_args__ = (
        Index("Grant_token_key", "token", unique=True),
        Index("Grant_organizationId_scopeType_scopeId_idx", "organizationId", "scopeType", "scopeId"),
        Index("Grant_projectId_resourceKind_scopeId_idx", "projectId", "resourceKind", "scopeId"),
    )

class GrantUsage(Base):
    __tablename__ = "GrantUsage"

    grant_id: Mapped[str] = mapped_column("grantId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    view_count: Mapped[int] = mapped_column("viewCount", default=0, server_default=text("0"))
    last_viewed_at: Mapped[Optional[datetime]] = mapped_column("lastViewedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GrantUsage_organizationId_idx", "organizationId"),
        Index("GrantUsage_projectId_idx", "projectId"),
    )

class Role(Base):
    __tablename__ = "Role"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    permissions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    kind: Mapped[str] = mapped_column(default="custom", server_default=text("'custom'"))
    occurred_at: Mapped[datetime] = mapped_column("occurredAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Role_organizationId_idx", "organizationId"),
    )

class ApiKey(Base):
    __tablename__ = "ApiKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    lookup_id: Mapped[str] = mapped_column("lookupId")
    hashed_secret: Mapped[str] = mapped_column("hashedSecret")
    permission_mode: Mapped[str] = mapped_column("permissionMode", default="all", server_default=text("'all'"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ApiKey_userId_fkey"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="ApiKey_organizationId_fkey"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revocation_cause: Mapped[Optional[str]] = mapped_column("revocationCause")
    parent_api_key_id: Mapped[Optional[str]] = mapped_column("parentApiKeyId")
    ingest_source_type: Mapped[Optional[str]] = mapped_column("ingestSourceType")
    ingestion_template_id: Mapped[Optional[str]] = mapped_column("ingestionTemplateId")
    created_by_device_label: Mapped[Optional[str]] = mapped_column("createdByDeviceLabel")
    is_system_managed: Mapped[bool] = mapped_column("isSystemManaged", default=False, server_default=text("false"))
    created_by_browser_session_id: Mapped[Optional[str]] = mapped_column("createdByBrowserSessionId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ApiKey_lookupId_key", "lookupId", unique=True),
        Index("ApiKey_userId_idx", "userId"),
        Index("ApiKey_organizationId_idx", "organizationId"),
        Index("ApiKey_lookupId_idx", "lookupId"),
        Index("ApiKey_ingestSourceType_idx", "ingestSourceType"),
        Index("ApiKey_ingestionTemplateId_idx", "ingestionTemplateId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="api_keys")
    organization: Mapped["Organization"] = relationship(back_populates="api_keys")
    role_bindings: Mapped[list["RoleBinding"]] = relationship(passive_deletes="all", back_populates="api_key")

class SigningKey(Base):
    __tablename__ = "SigningKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    name: Mapped[str]
    public_id: Mapped[str] = mapped_column("publicId")
    secret_ciphertext: Mapped[str] = mapped_column("secretCiphertext")
    created_by_user_id: Mapped[str] = mapped_column("createdByUserId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    rotated_at: Mapped[Optional[datetime]] = mapped_column("rotatedAt")
    verifies_until: Mapped[Optional[datetime]] = mapped_column("verifiesUntil")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")

    __table_args__ = (
        Index("SigningKey_publicId_key", "publicId", unique=True),
        Index("SigningKey_projectId_idx", "projectId"),
        Index("SigningKey_organizationId_idx", "organizationId"),
    )

class ProjectActiveDay(Base):
    __tablename__ = "ProjectActiveDay"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    day: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ProjectActiveDay_projectId_day_key", "projectId", "day", unique=True),
    )

class NurturingProject(Base):
    __tablename__ = "NurturingProject"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("NurturingProject_organizationId_idx", "organizationId"),
    )

class NurturingUncopiedProject(Base):
    __tablename__ = "NurturingUncopiedProject"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class NurturingOrganizationMember(Base):
    __tablename__ = "NurturingOrganizationMember"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    joined_at: Mapped[Optional[datetime]] = mapped_column("joinedAt")
    left_at: Mapped[Optional[datetime]] = mapped_column("leftAt")
    returned_at: Mapped[Optional[datetime]] = mapped_column("returnedAt")

    __table_args__ = (
        Index("NurturingOrganizationMember_userId_idx", "userId"),
    )

class NurturingOrganizationCreated(Base):
    __tablename__ = "NurturingOrganizationCreated"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt")

    __table_args__ = (
        Index("NurturingOrganizationCreated_createdAt_idx", "createdAt"),
    )

class NurturingOrganization(Base):
    __tablename__ = "NurturingOrganization"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    admin_user_id: Mapped[Optional[str]] = mapped_column("adminUserId")
    seeded: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    evaluation_count: Mapped[int] = mapped_column("evaluationCount", default=0, server_default=text("0"))
    simulation_run_count: Mapped[int] = mapped_column("simulationRunCount", default=0, server_default=text("0"))
    coding_first_session_at: Mapped[Optional[datetime]] = mapped_column("codingFirstSessionAt")
    coding_agent_types: Mapped[list[str]] = mapped_column("codingAgentTypes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    coding_active_days: Mapped[int] = mapped_column("codingActiveDays", default=0, server_default=text("0"))
    coding_last_active_day: Mapped[Optional[str]] = mapped_column("codingLastActiveDay")
    coding_terminal_types: Mapped[list[str]] = mapped_column("codingTerminalTypes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    coding_repository_keys: Mapped[list[str]] = mapped_column("codingRepositoryKeys", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    coding_first_days: Mapped[list[int]] = mapped_column("codingFirstDays", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    last_trace_at: Mapped[Optional[datetime]] = mapped_column("lastTraceAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class NurturingProjectUsageView(Base):
    __tablename__ = "NurturingProjectUsageView"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    owner: Mapped[str] = mapped_column(primary_key=True)
    folded: Mapped[dict[str, Any]] = mapped_column(JSONB)
    applied_event_ids: Mapped[list[str]] = mapped_column("appliedEventIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    projection_version: Mapped[str] = mapped_column("projectionVersion")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

class NurturingProjectTraceDay(Base):
    __tablename__ = "NurturingProjectTraceDay"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    day: Mapped[str] = mapped_column(primary_key=True)
    trace_count: Mapped[int] = mapped_column("traceCount", default=0, server_default=text("0"))
    error_count: Mapped[int] = mapped_column("errorCount", default=0, server_default=text("0"))
    last_trace_at: Mapped[Optional[datetime]] = mapped_column("lastTraceAt")
    last_failed_trace_at: Mapped[Optional[datetime]] = mapped_column("lastFailedTraceAt")

class NurturingPersonalProject(Base):
    __tablename__ = "NurturingPersonalProject"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    owner_user_id: Mapped[str] = mapped_column("ownerUserId")

class NurturingTracedProject(Base):
    __tablename__ = "NurturingTracedProject"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")

    __table_args__ = (
        Index("NurturingTracedProject_organizationId_idx", "organizationId"),
    )

class BillingMeterCheckpoint(Base):
    __tablename__ = "BillingMeterCheckpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    billing_month: Mapped[str] = mapped_column("billingMonth")
    meter: Mapped[str] = mapped_column(default="langwatch_billable_events", server_default=text("'langwatch_billable_events'"))
    last_reported_total: Mapped[int] = mapped_column("lastReportedTotal", default=0, server_default=text("0"))
    pending_reported_total: Mapped[Optional[int]] = mapped_column("pendingReportedTotal")
    consecutive_failures: Mapped[int] = mapped_column("consecutiveFailures", default=0, server_default=text("0"))
    last_counted_event_id: Mapped[Optional[str]] = mapped_column("lastCountedEventId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("BillingMeterCheckpoint_organizationId_billingMonth_meter_key", "organizationId", "billingMonth", "meter", unique=True),
    )

class VirtualKey(Base):
    __tablename__ = "VirtualKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "DISABLED", "REVOKED", name="VirtualKeyStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    purpose: Mapped[str] = mapped_column(Enum("USER", "LANGY", "CONNECT", name="VirtualKeyPurpose"), default="USER", server_default=text("'USER'"))
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    disabled_reason: Mapped[Optional[str]] = mapped_column("disabledReason")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    hashed_secret: Mapped[str] = mapped_column("hashedSecret")
    display_prefix: Mapped[str] = mapped_column("displayPrefix")
    principal_user_id: Mapped[Optional[str]] = mapped_column("principalUserId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="VirtualKey_principalUserId_fkey"))
    trace_project_id: Mapped[Optional[str]] = mapped_column("traceProjectId")
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    revision: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    previous_hashed_secret: Mapped[Optional[str]] = mapped_column("previousHashedSecret")
    previous_secret_valid_until: Mapped[Optional[datetime]] = mapped_column("previousSecretValidUntil")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_by_id: Mapped[Optional[str]] = mapped_column("revokedById")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[str] = mapped_column("createdById")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    routing_policy_id: Mapped[Optional[str]] = mapped_column("routingPolicyId", ForeignKey("RoutingPolicy.id", ondelete="SET NULL", onupdate="CASCADE", name="VirtualKey_routingPolicyId_fkey"))
    routing_mode: Mapped[str] = mapped_column("routingMode", Enum("NONE", "FALLBACK_ALL", "POLICY", name="VirtualKeyRoutingMode"), default="NONE", server_default=text("'NONE'"))
    connect_services: Mapped[list[str]] = mapped_column("connectServices", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    license_token_hash: Mapped[Optional[str]] = mapped_column("licenseTokenHash")
    license_instance_id: Mapped[Optional[str]] = mapped_column("licenseInstanceId")
    license_expires_at: Mapped[Optional[datetime]] = mapped_column("licenseExpiresAt")
    license_id: Mapped[Optional[str]] = mapped_column("licenseId")

    __table_args__ = (
        Index("VirtualKey_licenseTokenHash_key", "licenseTokenHash", unique=True),
        Index("VirtualKey_organizationId_idx", "organizationId"),
        Index("VirtualKey_displayPrefix_idx", "displayPrefix"),
        Index("VirtualKey_status_idx", "status"),
        Index("VirtualKey_principalUserId_idx", "principalUserId"),
        Index("VirtualKey_revision_idx", "revision"),
        Index("VirtualKey_routingPolicyId_idx", "routingPolicyId"),
        Index("VirtualKey_organizationId_purpose_idx", "organizationId", "purpose"),
        Index("VirtualKey_hashedSecret_key", "hashedSecret", unique=True),
        Index("VirtualKey_organizationId_externalId_key", "organizationId", "externalId", unique=True),
    )

    principal_user: Mapped[Optional["User"]] = relationship(back_populates="principal_virtual_keys")
    routing_policy: Mapped[Optional["RoutingPolicy"]] = relationship(back_populates="virtual_keys")
    scopes: Mapped[list["VirtualKeyScope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="virtual_key")
    ledger_entries: Mapped[list["GatewayBudgetLedger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="virtual_key")

class VirtualKeyScope(Base):
    __tablename__ = "VirtualKeyScope"

    id: Mapped[str] = mapped_column(primary_key=True)
    virtual_key_id: Mapped[str] = mapped_column("virtualKeyId", ForeignKey("VirtualKey.id", ondelete="CASCADE", onupdate="CASCADE", name="VirtualKeyScope_virtualKeyId_fkey"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="VirtualKeyScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("VirtualKeyScope_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("VirtualKeyScope_virtualKeyId_idx", "virtualKeyId"),
        Index("VirtualKeyScope_virtualKeyId_scopeType_scopeId_key", "virtualKeyId", "scopeType", "scopeId", unique=True),
    )

    virtual_key: Mapped["VirtualKey"] = relationship(back_populates="scopes")

class RoutingPolicy(Base):
    __tablename__ = "RoutingPolicy"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RoutingPolicy_organizationId_fkey"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    model_provider_ids: Mapped[dict[str, Any]] = mapped_column("modelProviderIds", JSONB, default=lambda: [], server_default=text("'[]'"))
    model_aliases: Mapped[dict[str, Any]] = mapped_column("modelAliases", JSONB, default=lambda: {}, server_default=text("'{}'"))
    default_model: Mapped[Optional[str]] = mapped_column("defaultModel")
    policy_rules: Mapped[dict[str, Any]] = mapped_column("policyRules", JSONB, default=lambda: {}, server_default=text("'{}'"))
    is_default: Mapped[bool] = mapped_column("isDefault", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="RoutingPolicy_createdById_fkey"))
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="RoutingPolicy_updatedById_fkey"))

    __table_args__ = (
        Index("RoutingPolicy_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="routing_policies")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="routing_policies_created")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="routing_policies_updated")
    virtual_keys: Mapped[list["VirtualKey"]] = relationship(back_populates="routing_policy")
    scopes: Mapped[list["RoutingPolicyScope"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="routing_policy")

class RoutingPolicyScope(Base):
    __tablename__ = "RoutingPolicyScope"

    id: Mapped[str] = mapped_column(primary_key=True)
    routing_policy_id: Mapped[str] = mapped_column("routingPolicyId", ForeignKey("RoutingPolicy.id", ondelete="CASCADE", onupdate="CASCADE", name="RoutingPolicyScope_routingPolicyId_fkey"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", name="RoutingPolicyScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("RoutingPolicyScope_scopeType_scopeId_idx", "scopeType", "scopeId"),
        Index("RoutingPolicyScope_routingPolicyId_idx", "routingPolicyId"),
        Index("RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key", "routingPolicyId", "scopeType", "scopeId", unique=True),
    )

    routing_policy: Mapped["RoutingPolicy"] = relationship(back_populates="scopes")

class GatewayGuardrail(Base):
    __tablename__ = "GatewayGuardrail"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="GatewayGuardrail_projectId_fkey"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    evaluator_id: Mapped[str] = mapped_column("evaluatorId", ForeignKey("Evaluator.id", ondelete="RESTRICT", onupdate="CASCADE", name="GatewayGuardrail_evaluatorId_fkey"))
    direction: Mapped[str] = mapped_column(Enum("PRE", "POST", "STREAM_CHUNK", name="GatewayGuardrailDirection"))
    failure_mode: Mapped[str] = mapped_column("failureMode", Enum("FAIL_OPEN", "FAIL_CLOSED", name="GatewayGuardrailFailureMode"), default="FAIL_CLOSED", server_default=text("'FAIL_CLOSED'"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="GatewayGuardrail_createdById_fkey"))
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="GatewayGuardrail_updatedById_fkey"))

    __table_args__ = (
        Index("GatewayGuardrail_projectId_idx", "projectId"),
        Index("GatewayGuardrail_evaluatorId_idx", "evaluatorId"),
        Index("GatewayGuardrail_projectId_archivedAt_idx", "projectId", "archivedAt"),
    )

    project: Mapped["Project"] = relationship(back_populates="gateway_guardrails")
    evaluator: Mapped["Evaluator"] = relationship(back_populates="gateway_guardrails")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="gateway_guardrails_created")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="gateway_guardrails_updated")

class IngestionSource(Base):
    __tablename__ = "IngestionSource"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="IngestionSource_organizationId_fkey"))
    team_id: Mapped[Optional[str]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="SET NULL", onupdate="CASCADE", name="IngestionSource_teamId_fkey"))
    source_type: Mapped[str] = mapped_column("sourceType")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    ingest_secret_hash: Mapped[str] = mapped_column("ingestSecretHash")
    parser_config: Mapped[dict[str, Any]] = mapped_column("parserConfig", JSONB, default=lambda: {}, server_default=text("'{}'"))
    poller_cursor: Mapped[Optional[dict[str, Any]]] = mapped_column("pollerCursor", JSONB(none_as_null=True))
    error_count: Mapped[int] = mapped_column("errorCount", default=0, server_default=text("0"))
    pull_schedule: Mapped[Optional[str]] = mapped_column("pullSchedule")
    status: Mapped[str] = mapped_column(default="awaiting_first_event", server_default=text("'awaiting_first_event'"))
    trace_project_id: Mapped[Optional[str]] = mapped_column("traceProjectId")
    provider_account_id: Mapped[Optional[str]] = mapped_column("providerAccountId")
    last_event_at: Mapped[Optional[datetime]] = mapped_column("lastEventAt")
    last_success_at: Mapped[Optional[datetime]] = mapped_column("lastSuccessAt")
    last_read_through_at: Mapped[Optional[datetime]] = mapped_column("lastReadThroughAt")
    last_run_completeness: Mapped[Optional[str]] = mapped_column("lastRunCompleteness")
    unpriced_usage_since: Mapped[Optional[datetime]] = mapped_column("unpricedUsageSince")
    unpriced_usage_through: Mapped[Optional[datetime]] = mapped_column("unpricedUsageThrough")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="IngestionSource_createdById_fkey"))

    __table_args__ = (
        Index("IngestionSource_organizationId_idx", "organizationId"),
        Index("IngestionSource_teamId_idx", "teamId"),
        Index("IngestionSource_sourceType_idx", "sourceType"),
        Index("IngestionSource_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="ingestion_sources")
    team: Mapped[Optional["Team"]] = relationship(back_populates="ingestion_sources")
    created_by: Mapped[Optional["User"]] = relationship(back_populates="ingestion_sources_created")

class AnomalyRule(Base):
    __tablename__ = "AnomalyRule"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="AnomalyRule_organizationId_fkey"))
    scope: Mapped[str]
    scope_id: Mapped[str] = mapped_column("scopeId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    severity: Mapped[str]
    rule_type: Mapped[str] = mapped_column("ruleType")
    threshold_config: Mapped[dict[str, Any]] = mapped_column("thresholdConfig", JSONB, default=lambda: {}, server_default=text("'{}'"))
    destination_config: Mapped[dict[str, Any]] = mapped_column("destinationConfig", JSONB, default=lambda: {}, server_default=text("'{}'"))
    status: Mapped[str] = mapped_column(default="active", server_default=text("'active'"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="AnomalyRule_createdById_fkey"))

    __table_args__ = (
        Index("AnomalyRule_organizationId_idx", "organizationId"),
        Index("AnomalyRule_scope_scopeId_idx", "scope", "scopeId"),
        Index("AnomalyRule_ruleType_idx", "ruleType"),
        Index("AnomalyRule_status_idx", "status"),
        Index("AnomalyRule_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="anomaly_rules")
    created_by: Mapped[Optional["User"]] = relationship(back_populates="anomaly_rules_created")
    alerts: Mapped[list["AnomalyAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="rule")

class AnomalyAlert(Base):
    __tablename__ = "AnomalyAlert"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="AnomalyAlert_organizationId_fkey"))
    rule_id: Mapped[str] = mapped_column("ruleId", ForeignKey("AnomalyRule.id", ondelete="CASCADE", onupdate="CASCADE", name="AnomalyAlert_ruleId_fkey"))
    severity: Mapped[str]
    rule_name: Mapped[str] = mapped_column("ruleName")
    rule_type: Mapped[str] = mapped_column("ruleType")
    trigger_window_start: Mapped[datetime] = mapped_column("triggerWindowStart")
    trigger_window_end: Mapped[datetime] = mapped_column("triggerWindowEnd")
    trigger_spend_usd: Mapped[Optional[DecimalType]] = mapped_column("triggerSpendUsd", Numeric(precision=18, scale=6))
    trigger_event_count: Mapped[Optional[int]] = mapped_column("triggerEventCount")
    detail: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    state: Mapped[str] = mapped_column(default="open", server_default=text("'open'"))
    destination_status: Mapped[dict[str, Any]] = mapped_column("destinationStatus", JSONB, default=lambda: {}, server_default=text("'{}'"))
    detected_at: Mapped[datetime] = mapped_column("detectedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    acknowledged_at: Mapped[Optional[datetime]] = mapped_column("acknowledgedAt")
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")

    __table_args__ = (
        Index("AnomalyAlert_organizationId_detectedAt_idx", "organizationId", "detectedAt"),
        Index("AnomalyAlert_ruleId_idx", "ruleId"),
        Index("AnomalyAlert_state_idx", "state"),
        Index("AnomalyAlert_ruleId_triggerWindowStart_key", "ruleId", "triggerWindowStart", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="anomaly_alerts")
    rule: Mapped["AnomalyRule"] = relationship(back_populates="alerts")

class AiToolEntry(Base):
    __tablename__ = "AiToolEntry"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="AiToolEntry_organizationId_fkey"))
    scope: Mapped[str]
    scope_id: Mapped[str] = mapped_column("scopeId")
    type: Mapped[str]
    display_name: Mapped[str] = mapped_column("displayName")
    slug: Mapped[str]
    icon_key: Mapped[Optional[str]] = mapped_column("iconKey")
    icon_asset: Mapped[Optional[str]] = mapped_column("iconAsset")
    order: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")

    __table_args__ = (
        Index("AiToolEntry_organizationId_scope_scopeId_idx", "organizationId", "scope", "scopeId"),
        Index("AiToolEntry_organizationId_enabled_archivedAt_idx", "organizationId", "enabled", "archivedAt"),
        Index("AiToolEntry_organizationId_type_idx", "organizationId", "type"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="ai_tool_entries")
    teams: Mapped[list["AiToolEntryTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="entry")
    departments: Mapped[list["AiToolEntryDepartment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="entry")

class AiToolEntryTeam(Base):
    __tablename__ = "AiToolEntryTeam"

    id: Mapped[str] = mapped_column(primary_key=True)
    entry_id: Mapped[str] = mapped_column("entryId", ForeignKey("AiToolEntry.id", ondelete="CASCADE", onupdate="CASCADE", name="AiToolEntryTeam_entryId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="AiToolEntryTeam_teamId_fkey"))

    __table_args__ = (
        Index("AiToolEntryTeam_teamId_idx", "teamId"),
        Index("AiToolEntryTeam_entryId_teamId_key", "entryId", "teamId", unique=True),
    )

    entry: Mapped["AiToolEntry"] = relationship(back_populates="teams")
    team: Mapped["Team"] = relationship(back_populates="ai_tool_entry_teams")

class AiToolEntryDepartment(Base):
    __tablename__ = "AiToolEntryDepartment"

    id: Mapped[str] = mapped_column(primary_key=True)
    entry_id: Mapped[str] = mapped_column("entryId", ForeignKey("AiToolEntry.id", ondelete="CASCADE", onupdate="CASCADE", name="AiToolEntryDepartment_entryId_fkey"))
    department_id: Mapped[str] = mapped_column("departmentId")

    __table_args__ = (
        Index("AiToolEntryDepartment_departmentId_idx", "departmentId"),
        Index("AiToolEntryDepartment_entryId_departmentId_key", "entryId", "departmentId", unique=True),
    )

    entry: Mapped["AiToolEntry"] = relationship(back_populates="departments")

class IngestionTemplate(Base):
    __tablename__ = "IngestionTemplate"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="IngestionTemplate_organizationId_fkey"))
    slug: Mapped[str]
    source_type: Mapped[str] = mapped_column("sourceType")
    display_name: Mapped[str] = mapped_column("displayName")
    description: Mapped[Optional[str]]
    icon_asset: Mapped[Optional[str]] = mapped_column("iconAsset")
    credential_schema: Mapped[Optional[str]] = mapped_column("credentialSchema")
    ottl_rules: Mapped[str] = mapped_column("ottlRules", default="", server_default=text("''"))
    platform_published: Mapped[bool] = mapped_column("platformPublished", default=False, server_default=text("false"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById")

    __table_args__ = (
        Index("IngestionTemplate_organizationId_archivedAt_idx", "organizationId", "archivedAt"),
        Index("IngestionTemplate_sourceType_idx", "sourceType"),
        Index("IngestionTemplate_organizationId_slug_key", "organizationId", "slug", unique=True),
    )

    organization: Mapped[Optional["Organization"]] = relationship(back_populates="ingestion_templates")

class GatewayConnectUpstream(Base):
    __tablename__ = "GatewayConnectUpstream"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    base_url: Mapped[str] = mapped_column("baseUrl")
    encrypted_token: Mapped[str] = mapped_column("encryptedToken")
    instance_id: Mapped[str] = mapped_column("instanceId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class GatewayTraceExportKey(Base):
    __tablename__ = "GatewayTraceExportKey"

    project_id: Mapped[str] = mapped_column("projectId", primary_key=True)
    api_key_id: Mapped[str] = mapped_column("apiKeyId")
    encrypted_token: Mapped[str] = mapped_column("encryptedToken")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class GatewayBudget(Base):
    __tablename__ = "GatewayBudget"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("ORGANIZATION", "TEAM", "PROJECT", "VIRTUAL_KEY", "PRINCIPAL", "GROUP", "ATTRIBUTED_USER", name="GatewayBudgetScopeType"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    provider_key: Mapped[Optional[str]] = mapped_column("providerKey")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    window: Mapped[str] = mapped_column(Enum("MINUTE", "HOUR", "DAY", "WEEK", "MONTH", "TOTAL", "MANUAL", name="GatewayBudgetWindow"))
    limit_usd: Mapped[DecimalType] = mapped_column("limitUsd", Numeric(precision=18, scale=6))
    on_breach: Mapped[str] = mapped_column("onBreach", Enum("BLOCK", "WARN", name="GatewayBudgetBreachAction"), default="BLOCK", server_default=text("'BLOCK'"))
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    spent_usd: Mapped[DecimalType] = mapped_column("spentUsd", Numeric(precision=18, scale=6), default=DecimalType("0"), server_default=text("0"))
    current_period_started_at: Mapped[datetime] = mapped_column("currentPeriodStartedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    resets_at: Mapped[datetime] = mapped_column("resetsAt")
    last_reset_at: Mapped[Optional[datetime]] = mapped_column("lastResetAt")
    cycle_anchor_at: Mapped[Optional[datetime]] = mapped_column("cycleAnchorAt")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[str] = mapped_column("createdById", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="GatewayBudget_createdById_fkey"))
    managed_by_virtual_key_id: Mapped[Optional[str]] = mapped_column("managedByVirtualKeyId")

    __table_args__ = (
        Index("GatewayBudget_organizationId_idx", "organizationId"),
        Index("GatewayBudget_scopeType_scopeId_providerKey_idx", "scopeType", "scopeId", "providerKey"),
        Index("GatewayBudget_managedByVirtualKeyId_idx", "managedByVirtualKeyId"),
        Index("GatewayBudget_resetsAt_idx", "resetsAt"),
        Index("GatewayBudget_organizationId_externalId_key", "organizationId", "externalId", unique=True),
    )

    created_by: Mapped["User"] = relationship(back_populates="gateway_budgets_created")
    ledger_entries: Mapped[list["GatewayBudgetLedger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="budget")
    bucket_boundaries: Mapped[list["GatewayBudgetBucketBoundary"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="budget")

class GatewayBudgetBucketBoundary(Base):
    __tablename__ = "GatewayBudgetBucketBoundary"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    budget_id: Mapped[str] = mapped_column("budgetId", ForeignKey("GatewayBudget.id", ondelete="CASCADE", onupdate="CASCADE", name="GatewayBudgetBucketBoundary_budgetId_fkey"))
    bucket_scope_id: Mapped[str] = mapped_column("bucketScopeId")
    period_started_at: Mapped[datetime] = mapped_column("periodStartedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GatewayBudgetBucketBoundary_organizationId_idx", "organizationId"),
        Index("GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key", "budgetId", "bucketScopeId", unique=True),
    )

    budget: Mapped["GatewayBudget"] = relationship(back_populates="bucket_boundaries")

class GatewayBudgetLedger(Base):
    __tablename__ = "GatewayBudgetLedger"

    id: Mapped[str] = mapped_column(primary_key=True)
    budget_id: Mapped[str] = mapped_column("budgetId", ForeignKey("GatewayBudget.id", ondelete="CASCADE", onupdate="CASCADE", name="GatewayBudgetLedger_budgetId_fkey"))
    virtual_key_id: Mapped[str] = mapped_column("virtualKeyId", ForeignKey("VirtualKey.id", ondelete="CASCADE", onupdate="CASCADE", name="GatewayBudgetLedger_virtualKeyId_fkey"))
    model_provider_id: Mapped[Optional[str]] = mapped_column("modelProviderId")
    provider_key: Mapped[Optional[str]] = mapped_column("providerKey")
    gateway_request_id: Mapped[str] = mapped_column("gatewayRequestId")
    amount_usd: Mapped[DecimalType] = mapped_column("amountUsd", Numeric(precision=18, scale=6))
    tokens_input: Mapped[int] = mapped_column("tokensInput", default=0, server_default=text("0"))
    tokens_output: Mapped[int] = mapped_column("tokensOutput", default=0, server_default=text("0"))
    tokens_cache_read: Mapped[int] = mapped_column("tokensCacheRead", default=0, server_default=text("0"))
    tokens_cache_write: Mapped[int] = mapped_column("tokensCacheWrite", default=0, server_default=text("0"))
    model: Mapped[str]
    duration_ms: Mapped[Optional[int]] = mapped_column("durationMs")
    status: Mapped[str] = mapped_column(Enum("SUCCESS", "PROVIDER_ERROR", "BLOCKED_BY_GUARDRAIL", "CANCELLED", name="GatewayBudgetLedgerStatus"), default="SUCCESS", server_default=text("'SUCCESS'"))
    occurred_at: Mapped[datetime] = mapped_column("occurredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("GatewayBudgetLedger_virtualKeyId_idx", "virtualKeyId"),
        Index("GatewayBudgetLedger_gatewayRequestId_idx", "gatewayRequestId"),
        Index("GatewayBudgetLedger_occurredAt_idx", "occurredAt"),
        Index("GatewayBudgetLedger_budgetId_gatewayRequestId_key", "budgetId", "gatewayRequestId", unique=True),
    )

    budget: Mapped["GatewayBudget"] = relationship(back_populates="ledger_entries")
    virtual_key: Mapped["VirtualKey"] = relationship(back_populates="ledger_entries")

class GatewayChangeEvent(Base):
    __tablename__ = "GatewayChangeEvent"

    revision: Mapped[int] = mapped_column(BigInteger, primary_key=True, autoincrement=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="GatewayChangeEvent_organizationId_fkey"))
    kind: Mapped[str] = mapped_column(Enum("VK_CREATED", "VK_CONFIG_UPDATED", "VK_REVOKED", "VK_ROTATED", "VK_DISABLED", "VK_ENABLED", "BUDGET_CREATED", "BUDGET_UPDATED", "BUDGET_DELETED", "MODEL_PROVIDER_UPDATED", "CACHE_RULE_CREATED", "CACHE_RULE_UPDATED", "CACHE_RULE_DELETED", "ROUTING_POLICY_UPDATED", "ROUTING_POLICY_DELETED", name="GatewayChangeEventKind"))
    virtual_key_id: Mapped[Optional[str]] = mapped_column("virtualKeyId")
    budget_id: Mapped[Optional[str]] = mapped_column("budgetId")
    model_provider_id: Mapped[Optional[str]] = mapped_column("modelProviderId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="SET NULL", onupdate="CASCADE", name="GatewayChangeEvent_projectId_fkey"))
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("GatewayChangeEvent_organizationId_revision_idx", "organizationId", "revision"),
        Index("GatewayChangeEvent_projectId_revision_idx", "projectId", "revision"),
        Index("GatewayChangeEvent_virtualKeyId_idx", "virtualKeyId"),
        Index("GatewayChangeEvent_budgetId_idx", "budgetId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="gateway_change_events")
    project: Mapped[Optional["Project"]] = relationship(back_populates="gateway_change_events")

class GatewayCacheRule(Base):
    __tablename__ = "GatewayCacheRule"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    priority: Mapped[int] = mapped_column(default=100, server_default=text("100"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    matchers: Mapped[dict[str, Any]] = mapped_column(JSONB)
    action: Mapped[dict[str, Any]] = mapped_column(JSONB)
    mode_enum: Mapped[str] = mapped_column("modeEnum", Enum("RESPECT", "FORCE", "DISABLE", name="GatewayCacheRuleMode"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by_id: Mapped[str] = mapped_column("createdById")

    __table_args__ = (
        Index("GatewayCacheRule_organizationId_archivedAt_priority_idx", "organizationId", "archivedAt", "priority"),
        Index("GatewayCacheRule_organizationId_enabled_priority_idx", "organizationId", "enabled", "priority"),
    )

class FeatureFlag(Base):
    __tablename__ = "FeatureFlag"

    key: Mapped[str] = mapped_column(primary_key=True)
    enabled: Mapped[bool]
    rules: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_edited_by: Mapped[Optional[str]] = mapped_column("lastEditedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class FeatureFlagExperimentSetting(Base):
    __tablename__ = "FeatureFlagExperimentSetting"

    flag_key: Mapped[str] = mapped_column("flagKey", primary_key=True)
    subject_type: Mapped[str] = mapped_column("subjectType", Enum("USER", "ORGANIZATION", "PROJECT", name="FeatureFlagExperimentSubjectType"), primary_key=True)
    subject_id: Mapped[str] = mapped_column("subjectId", primary_key=True)
    enabled: Mapped[bool]
    changed_by_user_id: Mapped[Optional[str]] = mapped_column("changedByUserId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("FeatureFlagExperimentSetting_subjectType_subjectId_idx", "subjectType", "subjectId"),
    )

class ProcessManagerInstance(Base):
    __tablename__ = "ProcessManagerInstance"

    id: Mapped[str] = mapped_column(primary_key=True)
    process_name: Mapped[str] = mapped_column("processName")
    project_id: Mapped[str] = mapped_column("projectId")
    process_key: Mapped[str] = mapped_column("processKey")
    tenant_id: Mapped[str] = mapped_column("tenantId")
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    state: Mapped[dict[str, Any]] = mapped_column(JSONB)
    revision: Mapped[int]
    next_wake_at: Mapped[Optional[datetime]] = mapped_column("nextWakeAt", UtcDateTimeTz(precision=3))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("ProcessManagerInstance_nextWakeAt_idx", "nextWakeAt"),
        Index("ProcessManagerInstance_processName_projectId_processKey_key", "processName", "projectId", "processKey", unique=True),
    )

class ProcessManagerInbox(Base):
    __tablename__ = "ProcessManagerInbox"

    id: Mapped[str] = mapped_column(primary_key=True)
    process_name: Mapped[str] = mapped_column("processName")
    project_id: Mapped[str] = mapped_column("projectId")
    process_key: Mapped[str] = mapped_column("processKey")
    tenant_id: Mapped[str] = mapped_column("tenantId")
    source_event_id: Mapped[str] = mapped_column("sourceEventId")
    source_event_key: Mapped[Optional[str]] = mapped_column("sourceEventKey")
    consumed_at: Mapped[datetime] = mapped_column("consumedAt", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("ProcessManagerInbox_processName_projectId_processKey_consum_idx", "processName", "projectId", "processKey", "consumedAt"),
        Index("ProcessManagerInbox_consumedAt_idx", "consumedAt"),
        Index("ProcessManagerInbox_processName_projectId_sourceEventKey_key", "processName", "projectId", "sourceEventKey", unique=True),
    )

class ProcessManagerOutbox(Base):
    __tablename__ = "ProcessManagerOutbox"

    id: Mapped[str] = mapped_column(primary_key=True)
    process_name: Mapped[str] = mapped_column("processName")
    project_id: Mapped[str] = mapped_column("projectId")
    process_key: Mapped[str] = mapped_column("processKey")
    tenant_id: Mapped[str] = mapped_column("tenantId")
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    message_key: Mapped[str] = mapped_column("messageKey")
    intent_type: Mapped[str] = mapped_column("intentType")
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    trace_carrier: Mapped[dict[str, Any]] = mapped_column("traceCarrier", JSONB)
    source_event_id: Mapped[Optional[str]] = mapped_column("sourceEventId")
    status: Mapped[str] = mapped_column(Enum("pending", "dispatched", "dead", "discarded", name="ProcessManagerOutboxStatus"), default="pending", server_default=text("'pending'"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    next_attempt_at: Mapped[datetime] = mapped_column("nextAttemptAt", UtcDateTimeTz(precision=3))
    leased_until: Mapped[Optional[datetime]] = mapped_column("leasedUntil", UtcDateTimeTz(precision=3))
    lease_token: Mapped[Optional[str]] = mapped_column("leaseToken")
    dispatched_at: Mapped[Optional[datetime]] = mapped_column("dispatchedAt", UtcDateTimeTz(precision=3))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=3))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("ProcessManagerOutbox_leaseToken_key", "leaseToken", unique=True),
        Index("ProcessManagerOutbox_processName_projectId_processKey_creat_idx", "processName", "projectId", "processKey", "createdAt"),
        Index("ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx", "status", "nextAttemptAt", "leasedUntil"),
        Index("ProcessManagerOutbox_status_processName_nextAttemptAt_idx", "status", "processName", "nextAttemptAt"),
        Index("ProcessManagerOutbox_processName_projectId_messageKey_key", "processName", "projectId", "messageKey", unique=True),
    )

    attempt_log: Mapped[list["ProcessManagerOutboxAttempt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="outbox")

class ProcessManagerOutboxAttempt(Base):
    __tablename__ = "ProcessManagerOutboxAttempt"

    id: Mapped[str] = mapped_column(primary_key=True)
    outbox_id: Mapped[str] = mapped_column("outboxId", ForeignKey("ProcessManagerOutbox.id", ondelete="CASCADE", onupdate="CASCADE", name="ProcessManagerOutboxAttempt_outboxId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId")
    attempt: Mapped[int]
    occurred_at: Mapped[datetime] = mapped_column("occurredAt", UtcDateTimeTz(precision=3))
    outcome: Mapped[str] = mapped_column(Enum("retry_scheduled", "dead", name="ProcessManagerOutboxAttemptOutcome"))
    error_type: Mapped[str] = mapped_column("errorType")
    error_message: Mapped[str] = mapped_column("errorMessage")
    retry_after_ms: Mapped[Optional[int]] = mapped_column("retryAfterMs")

    __table_args__ = (
        Index("ProcessManagerOutboxAttempt_outboxId_attempt_idx", "outboxId", "attempt"),
    )

    outbox: Mapped["ProcessManagerOutbox"] = relationship(back_populates="attempt_log")

class TopicClusteringRunProjection(Base):
    __tablename__ = "TopicClusteringRunProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    last_requested_at: Mapped[Optional[float]] = mapped_column("lastRequestedAt")
    last_request_trigger: Mapped[Optional[str]] = mapped_column("lastRequestTrigger")
    last_run_at: Mapped[Optional[float]] = mapped_column("lastRunAt")
    last_run_outcome: Mapped[Optional[str]] = mapped_column("lastRunOutcome")
    last_run_mode: Mapped[Optional[str]] = mapped_column("lastRunMode")
    last_run_skipped_reason: Mapped[Optional[str]] = mapped_column("lastRunSkippedReason")
    last_run_error: Mapped[Optional[str]] = mapped_column("lastRunError")
    last_run_error_code: Mapped[Optional[str]] = mapped_column("lastRunErrorCode")
    last_run_error_user_actionable: Mapped[bool] = mapped_column("lastRunErrorUserActionable", default=False, server_default=text("false"))
    last_run_traces_processed: Mapped[int] = mapped_column("lastRunTracesProcessed", default=0, server_default=text("0"))
    last_run_topics_count: Mapped[int] = mapped_column("lastRunTopicsCount", default=0, server_default=text("0"))
    last_run_subtopics_count: Mapped[int] = mapped_column("lastRunSubtopicsCount", default=0, server_default=text("0"))
    last_run_pages: Mapped[int] = mapped_column("lastRunPages", default=0, server_default=text("0"))
    in_progress_run_id: Mapped[Optional[str]] = mapped_column("inProgressRunId")
    in_progress_traces: Mapped[int] = mapped_column("inProgressTraces", default=0, server_default=text("0"))
    in_progress_pages: Mapped[int] = mapped_column("inProgressPages", default=0, server_default=text("0"))
    in_progress_started_at: Mapped[Optional[float]] = mapped_column("inProgressStartedAt")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("TopicClusteringRunProjection_projectId_key", "projectId", unique=True),
    )

class TopicModelProjection(Base):
    __tablename__ = "TopicModelProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("TopicModelProjection_projectId_key", "projectId", unique=True),
    )

class TopicClusteringRunHistoryProjection(Base):
    __tablename__ = "TopicClusteringRunHistoryProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    runs: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("TopicClusteringRunHistoryProjection_projectId_key", "projectId", unique=True),
    )

class IngestionPullRunProjection(Base):
    __tablename__ = "IngestionPullRunProjection"

    id: Mapped[str] = mapped_column(primary_key=True)
    source_id: Mapped[str] = mapped_column("sourceId")
    project_id: Mapped[str] = mapped_column("projectId")
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    cron: Mapped[Optional[str]]
    cursor: Mapped[Optional[str]]
    last_run_at: Mapped[Optional[float]] = mapped_column("lastRunAt")
    last_run_outcome: Mapped[Optional[str]] = mapped_column("lastRunOutcome")
    last_run_event_count: Mapped[int] = mapped_column("lastRunEventCount", default=0, server_default=text("0"))
    last_run_error: Mapped[Optional[str]] = mapped_column("lastRunError")
    last_run_error_code: Mapped[Optional[str]] = mapped_column("lastRunErrorCode")
    consecutive_errors: Mapped[int] = mapped_column("consecutiveErrors", default=0, server_default=text("0"))
    last_success_at: Mapped[Optional[float]] = mapped_column("lastSuccessAt")
    last_run_scheduled_for: Mapped[Optional[float]] = mapped_column("lastRunScheduledFor")
    last_read_through_at: Mapped[Optional[float]] = mapped_column("lastReadThroughAt")
    last_run_completeness: Mapped[Optional[str]] = mapped_column("lastRunCompleteness")
    last_agents_listing_at: Mapped[Optional[float]] = mapped_column("lastAgentsListingAt")
    last_agents_listing_outcome: Mapped[Optional[str]] = mapped_column("lastAgentsListingOutcome")
    last_agents_listing_count: Mapped[Optional[int]] = mapped_column("lastAgentsListingCount")
    last_agents_listing_reason: Mapped[Optional[str]] = mapped_column("lastAgentsListingReason")
    last_agents_listing_status: Mapped[Optional[int]] = mapped_column("lastAgentsListingStatus")
    last_people_listing_at: Mapped[Optional[float]] = mapped_column("lastPeopleListingAt")
    last_people_listing_outcome: Mapped[Optional[str]] = mapped_column("lastPeopleListingOutcome")
    last_people_directory_count: Mapped[Optional[int]] = mapped_column("lastPeopleDirectoryCount")
    last_people_withheld_count: Mapped[Optional[int]] = mapped_column("lastPeopleWithheldCount")
    last_people_listing_reason: Mapped[Optional[str]] = mapped_column("lastPeopleListingReason")
    last_people_listing_status: Mapped[Optional[int]] = mapped_column("lastPeopleListingStatus")
    created_at: Mapped[float] = mapped_column("createdAt")
    updated_at: Mapped[float] = mapped_column("updatedAt")
    occurred_at: Mapped[float] = mapped_column("occurredAt")
    accepted_at: Mapped[float] = mapped_column("acceptedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId")
    projection_version: Mapped[str] = mapped_column("projectionVersion")

    __table_args__ = (
        Index("IngestionPullRunProjection_sourceId_key", "sourceId", unique=True),
        Index("IngestionPullRunProjection_projectId_idx", "projectId"),
    )

class ScheduledJob(Base):
    __tablename__ = "ScheduledJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    target_type: Mapped[str] = mapped_column("targetType")
    target_id: Mapped[str] = mapped_column("targetId")
    cron: Mapped[str]
    timezone_: Mapped[str] = mapped_column("timezone")
    next_run_at: Mapped[datetime] = mapped_column("nextRunAt")
    last_slot: Mapped[Optional[datetime]] = mapped_column("lastSlot")
    current_slot: Mapped[Optional[datetime]] = mapped_column("currentSlot")
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_error: Mapped[Optional[str]] = mapped_column("lastError")
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("ScheduledJob_active_nextRunAt_idx", "active", "nextRunAt"),
        Index("ScheduledJob_projectId_idx", "projectId"),
        Index("ScheduledJob_targetType_targetId_key", "targetType", "targetId", unique=True),
    )

class BugReport(Base):
    __tablename__ = "BugReport"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    source: Mapped[str]
    kind: Mapped[str]
    title: Mapped[str]
    summary: Mapped[Optional[str]]
    session_data: Mapped[Optional[str]] = mapped_column("sessionData")
    session_truncated: Mapped[bool] = mapped_column("sessionTruncated", default=False, server_default=text("false"))
    agent: Mapped[Optional[str]]
    contact_email: Mapped[Optional[str]] = mapped_column("contactEmail")
    cli_version: Mapped[Optional[str]] = mapped_column("cliVersion")
    linked_project_id: Mapped[Optional[str]] = mapped_column("linkedProjectId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("BugReport_createdAt_idx", "createdAt"),
    )

class IdempotencyReceipt(Base):
    __tablename__ = "IdempotencyReceipt"

    id: Mapped[str] = mapped_column(primary_key=True)
    scope_id: Mapped[str] = mapped_column("scopeId")
    key: Mapped[str]
    claim_id: Mapped[str] = mapped_column("claimId")
    request_fingerprint: Mapped[str] = mapped_column("requestFingerprint")
    response_status: Mapped[Optional[int]] = mapped_column("responseStatus")
    response_body: Mapped[Optional[str]] = mapped_column("responseBody")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    heartbeat_at: Mapped[datetime] = mapped_column("heartbeatAt")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

    __table_args__ = (
        Index("IdempotencyReceipt_expiresAt_idx", "expiresAt"),
        Index("IdempotencyReceipt_scopeId_key_key", "scopeId", "key", unique=True),
    )

class GatewayRealtimeSession(Base):
    __tablename__ = "GatewayRealtimeSession"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    virtual_key_id: Mapped[str] = mapped_column("virtualKeyId")
    model_provider_id: Mapped[str] = mapped_column("modelProviderId")
    vendor: Mapped[str]
    agent_id: Mapped[Optional[str]] = mapped_column("agentId")
    model: Mapped[str]
    requested_model: Mapped[Optional[str]] = mapped_column("requestedModel")
    vendor_conversation_id: Mapped[Optional[str]] = mapped_column("vendorConversationId")
    status: Mapped[str] = mapped_column(Enum("OPEN", "CLOSED", "FAILED", "EXPIRED", name="GatewayRealtimeSessionStatus"), default="OPEN", server_default=text("'OPEN'"))
    minted_at: Mapped[datetime] = mapped_column("mintedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    closed_at: Mapped[Optional[datetime]] = mapped_column("closedAt")
    close_reason: Mapped[Optional[str]] = mapped_column("closeReason")
    trace_id: Mapped[Optional[str]] = mapped_column("traceId")
    vendor_cost_raw: Mapped[Optional[dict[str, Any]]] = mapped_column("vendorCostRaw", JSONB(none_as_null=True))
    kind: Mapped[Optional[str]]
    metering: Mapped[Optional[str]]
    credential_expires_at: Mapped[Optional[datetime]] = mapped_column("credentialExpiresAt")
    transcription_model: Mapped[Optional[str]] = mapped_column("transcriptionModel")
    end_user_id: Mapped[Optional[str]] = mapped_column("endUserId")
    last_report_at: Mapped[Optional[datetime]] = mapped_column("lastReportAt")
    reported_cost_nano_usd: Mapped[int] = mapped_column("reportedCostNanoUsd", BigInteger, default=0, server_default=text("0"))
    report_count: Mapped[int] = mapped_column("reportCount", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx", "virtualKeyId", "status", "mintedAt"),
        Index("GatewayRealtimeSession_projectId_status_mintedAt_idx", "projectId", "status", "mintedAt"),
        Index("GatewayRealtimeSession_status_metering_mintedAt_idx", "status", "metering", "mintedAt"),
        Index("GatewayRealtimeSession_vendor_vendorConversationId_key", "vendor", "vendorConversationId", unique=True),
    )

class GatewayRealtimeSessionReport(Base):
    __tablename__ = "GatewayRealtimeSessionReport"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_id: Mapped[str] = mapped_column("sessionId")
    report_key: Mapped[str] = mapped_column("reportKey")
    project_id: Mapped[str] = mapped_column("projectId")
    model: Mapped[str]
    usage: Mapped[dict[str, Any]] = mapped_column(JSONB)
    cost_nano_usd: Mapped[int] = mapped_column("costNanoUsd", BigInteger)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("GatewayRealtimeSessionReport_projectId_idx", "projectId"),
        Index("GatewayRealtimeSessionReport_sessionId_reportKey_key", "sessionId", "reportKey", unique=True),
    )

class SystemMigrationTenantState(Base):
    __tablename__ = "SystemMigrationTenantState"

    migration_name: Mapped[str] = mapped_column("migrationName", primary_key=True)
    tenant_id: Mapped[str] = mapped_column("tenantId", primary_key=True)
    status: Mapped[str]
    report: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    occurred_at: Mapped[datetime] = mapped_column("occurredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("SystemMigrationTenantState_migrationName_status_idx", "migrationName", "status"),
    )

class SystemMigrationEnrollment(Base):
    __tablename__ = "SystemMigrationEnrollment"

    organization_id: Mapped[str] = mapped_column("organizationId", primary_key=True)
    migration_name: Mapped[str] = mapped_column("migrationName", primary_key=True)
    enrolled_by_user_id: Mapped[str] = mapped_column("enrolledByUserId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("SystemMigrationEnrollment_migrationName_idx", "migrationName"),
    )

class StoredObject(Base):
    __tablename__ = "StoredObject"

    tenant_id: Mapped[str] = mapped_column("tenantId", primary_key=True)
    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    purpose: Mapped[str]
    owner_kind: Mapped[str] = mapped_column("ownerKind")
    owner_id: Mapped[str] = mapped_column("ownerId")
    filename: Mapped[str]
    sha256: Mapped[str]
    size_bytes: Mapped[int] = mapped_column("sizeBytes", BigInteger)
    media_type: Mapped[str] = mapped_column("mediaType")
    media_type_verified: Mapped[bool] = mapped_column("mediaTypeVerified", default=False, server_default=text("false"))
    storage_provider: Mapped[Optional[str]] = mapped_column("storageProvider")
    storage_destination_id: Mapped[Optional[str]] = mapped_column("storageDestinationId")
    storage_provider_relative_id: Mapped[Optional[str]] = mapped_column("storageProviderRelativeId")
    generation: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    audiences: Mapped[dict[str, Any]] = mapped_column(JSONB)
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    available_at: Mapped[Optional[datetime]] = mapped_column("availableAt")
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    source: Mapped[str]
    legacy_fingerprint: Mapped[Optional[str]] = mapped_column("legacyFingerprint")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("StoredObject_tenantId_status_id_idx", "tenantId", "status", "id"),
        Index("StoredObject_ownerKind_ownerId_status_idx", "ownerKind", "ownerId", "status"),
        Index("StoredObject_status_expiresAt_idx", "status", "expiresAt"),
    )

class DiscoveredPerson(Base):
    __tablename__ = "DiscoveredPerson"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    provider: Mapped[str]
    raw_actor_id: Mapped[str] = mapped_column("rawActorId")
    display_text: Mapped[str] = mapped_column("displayText")
    kind: Mapped[str]
    department: Mapped[Optional[str]]
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt")
    last_seen_at: Mapped[datetime] = mapped_column("lastSeenAt")
    erased_at: Mapped[Optional[datetime]] = mapped_column("erasedAt")
    money_rows_pending_at: Mapped[Optional[datetime]] = mapped_column("moneyRowsPendingAt")
    money_rebuild_since: Mapped[Optional[str]] = mapped_column("moneyRebuildSince")
    suspended_at: Mapped[Optional[datetime]] = mapped_column("suspendedAt")
    suspended_reason: Mapped[Optional[str]] = mapped_column("suspendedReason")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DiscoveredPerson_organizationId_lastSeenAt_idx", "organizationId", "lastSeenAt"),
        Index("DiscoveredPerson_organizationId_provider_rawActorId_key", "organizationId", "provider", "rawActorId", unique=True),
    )

class DiscoveredAgent(Base):
    __tablename__ = "DiscoveredAgent"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    provider: Mapped[str]
    raw_agent_id: Mapped[str] = mapped_column("rawAgentId")
    display_text: Mapped[str] = mapped_column("displayText")
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB)
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt")
    last_seen_at: Mapped[datetime] = mapped_column("lastSeenAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DiscoveredAgent_organizationId_lastSeenAt_idx", "organizationId", "lastSeenAt"),
        Index("DiscoveredAgent_organizationId_provider_rawAgentId_key", "organizationId", "provider", "rawAgentId", unique=True),
    )

class IdentityMatch(Base):
    __tablename__ = "IdentityMatch"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    discovered_person_id: Mapped[str] = mapped_column("discoveredPersonId")
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    evidence_kind: Mapped[str] = mapped_column("evidenceKind")
    valid_from: Mapped[datetime] = mapped_column("validFrom")
    valid_to: Mapped[Optional[datetime]] = mapped_column("validTo")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("IdentityMatch_organizationId_discoveredPersonId_idx", "organizationId", "discoveredPersonId"),
        Index("IdentityMatch_organizationId_userId_idx", "organizationId", "userId"),
    )

class GovernanceTenantHistory(Base):
    __tablename__ = "GovernanceTenantHistory"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    tenant_id: Mapped[str] = mapped_column("tenantId")
    first_used_at: Mapped[datetime] = mapped_column("firstUsedAt")
    last_used_at: Mapped[datetime] = mapped_column("lastUsedAt")

    __table_args__ = (
        Index("GovernanceTenantHistory_organizationId_idx", "organizationId"),
        Index("GovernanceTenantHistory_organizationId_tenantId_key", "organizationId", "tenantId", unique=True),
    )

class ErasedIdentifierSuppression(Base):
    __tablename__ = "ErasedIdentifierSuppression"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    provider: Mapped[str]
    identifier_hash: Mapped[str] = mapped_column("identifierHash")
    erased_at: Mapped[datetime] = mapped_column("erasedAt")

    __table_args__ = (
        Index("ErasedIdentifierSuppression_organizationId_idx", "organizationId"),
        Index("ErasedIdentifierSuppression_organizationId_provider_identif_key", "organizationId", "provider", "identifierHash", unique=True),
    )

class IdentityMatchSuggestion(Base):
    __tablename__ = "IdentityMatchSuggestion"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    discovered_person_id: Mapped[str] = mapped_column("discoveredPersonId")
    user_id: Mapped[str] = mapped_column("userId")
    score: Mapped[float]
    computed_at: Mapped[datetime] = mapped_column("computedAt")

    __table_args__ = (
        Index("IdentityMatchSuggestion_organizationId_score_idx", "organizationId", "score"),
        Index("IdentityMatchSuggestion_organizationId_discoveredPersonId_u_key", "organizationId", "discoveredPersonId", "userId", unique=True),
    )

class ConnectedSeatChange(Base):
    __tablename__ = "ConnectedSeatChange"

    id: Mapped[str] = mapped_column(primary_key=True)
    license_id: Mapped[str] = mapped_column("licenseId")
    account_id: Mapped[Optional[str]] = mapped_column("accountId")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    changed_at: Mapped[datetime] = mapped_column("changedAt")
    added_seats: Mapped[int] = mapped_column("addedSeats")
    unit_amount_cents: Mapped[int] = mapped_column("unitAmountCents")
    amount_cents: Mapped[int] = mapped_column("amountCents")
    currency: Mapped[Optional[str]] = mapped_column(Enum("USD", "EUR", name="Currency"))
    stripe_invoice_id: Mapped[Optional[str]] = mapped_column("stripeInvoiceId")
    state: Mapped[str] = mapped_column(Enum("intent", "invoiced", "nothing_to_invoice", "not_onboarded", name="ConnectedSeatChangeState"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ConnectedSeatChange_licenseId_key", "licenseId", unique=True),
        Index("ConnectedSeatChange_accountId_idx", "accountId"),
        Index("ConnectedSeatChange_organizationId_idx", "organizationId"),
        Index("ConnectedSeatChange_state_idx", "state"),
    )

class ConnectedStatement(Base):
    __tablename__ = "ConnectedStatement"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_id: Mapped[str] = mapped_column("accountId")
    month: Mapped[datetime]
    sent_at: Mapped[datetime] = mapped_column("sentAt")

    __table_args__ = (
        Index("ConnectedStatement_accountId_idx", "accountId"),
        Index("ConnectedStatement_accountId_month_key", "accountId", "month", unique=True),
    )

class LicenseRenewalClaim(Base):
    __tablename__ = "LicenseRenewalClaim"

    stripe_invoice_id: Mapped[str] = mapped_column("stripeInvoiceId", primary_key=True)
    stripe_subscription_id: Mapped[str] = mapped_column("stripeSubscriptionId")
    claim_token: Mapped[str] = mapped_column("claimToken")
    claimed_at: Mapped[datetime] = mapped_column("claimedAt")
    issued_license: Mapped[Optional[dict[str, Any]]] = mapped_column("issuedLicense", JSONB(none_as_null=True))
    delivered_at: Mapped[Optional[datetime]] = mapped_column("deliveredAt")
    license_id: Mapped[Optional[str]] = mapped_column("licenseId")

class InstanceIdentity(Base):
    __tablename__ = "InstanceIdentity"

    id: Mapped[str] = mapped_column(primary_key=True)
    instance_id: Mapped[str] = mapped_column("instanceId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_report_at: Mapped[Optional[datetime]] = mapped_column("lastReportAt")
    last_report_error: Mapped[Optional[str]] = mapped_column("lastReportError")
    optional_metrics_opt_out: Mapped[bool] = mapped_column("optionalMetricsOptOut", default=False, server_default=text("false"))
    hostname_opt_out: Mapped[bool] = mapped_column("hostnameOptOut", default=False, server_default=text("false"))
    upgrade_diagnostics_opt_out: Mapped[bool] = mapped_column("upgradeDiagnosticsOptOut", default=False, server_default=text("false"))

    __table_args__ = (
        Index("InstanceIdentity_instanceId_key", "instanceId", unique=True),
    )

class SelfHostedInstance(Base):
    __tablename__ = "SelfHostedInstance"

    id: Mapped[str] = mapped_column(primary_key=True)
    instance_id: Mapped[str] = mapped_column("instanceId")
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_seen_at: Mapped[datetime] = mapped_column("lastSeenAt")
    version: Mapped[Optional[str]]
    install_method: Mapped[Optional[str]] = mapped_column("installMethod")
    chart_version: Mapped[Optional[str]] = mapped_column("chartVersion")
    hostname: Mapped[Optional[str]]
    environment: Mapped[Optional[str]]
    installed_at: Mapped[Optional[datetime]] = mapped_column("installedAt")
    report_schema_version: Mapped[Optional[int]] = mapped_column("reportSchemaVersion")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    issued_license_id: Mapped[Optional[str]] = mapped_column("issuedLicenseId")
    user_email_domains: Mapped[Optional[dict[str, Any]]] = mapped_column("userEmailDomains", JSONB(none_as_null=True))
    user_domains: Mapped[list[str]] = mapped_column("userDomains", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    latest_report: Mapped[Optional[dict[str, Any]]] = mapped_column("latestReport", JSONB(none_as_null=True))
    optional_metrics_reported: Mapped[bool] = mapped_column("optionalMetricsReported", default=True, server_default=text("true"))
    hostname_reported: Mapped[bool] = mapped_column("hostnameReported", default=True, server_default=text("true"))
    report_count: Mapped[int] = mapped_column("reportCount", default=0, server_default=text("0"))
    last_unknown_fields: Mapped[int] = mapped_column("lastUnknownFields", default=0, server_default=text("0"))
    raised_signals: Mapped[list[str]] = mapped_column("raisedSignals", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("SelfHostedInstance_instanceId_key", "instanceId", unique=True),
        Index("SelfHostedInstance_lastSeenAt_idx", "lastSeenAt"),
        Index("SelfHostedInstance_organizationId_idx", "organizationId"),
        Index("SelfHostedInstance_issuedLicenseId_idx", "issuedLicenseId"),
    )

class SelfHostedInstanceDiagnostic(Base):
    __tablename__ = "SelfHostedInstanceDiagnostic"

    id: Mapped[str] = mapped_column(primary_key=True)
    instance_id: Mapped[str] = mapped_column("instanceId")
    step_id: Mapped[str] = mapped_column("stepId")
    step_description: Mapped[Optional[str]] = mapped_column("stepDescription")
    release: Mapped[Optional[str]]
    attempt: Mapped[int]
    trigger: Mapped[str]
    byte_length: Mapped[int] = mapped_column("byteLength")
    object_key: Mapped[str] = mapped_column("objectKey")
    received_at: Mapped[datetime] = mapped_column("receivedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    slack_announced_at: Mapped[Optional[datetime]] = mapped_column("slackAnnouncedAt")

    __table_args__ = (
        Index("SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx", "instanceId", "receivedAt"),
    )

class SelfHostedNotice(Base):
    __tablename__ = "SelfHostedNotice"

    id: Mapped[str] = mapped_column(primary_key=True)
    severity: Mapped[str]
    title: Mapped[str]
    body: Mapped[str]
    url: Mapped[Optional[str]]
    fixed_in: Mapped[Optional[str]] = mapped_column("fixedIn")
    status: Mapped[str]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    published_at: Mapped[Optional[datetime]] = mapped_column("publishedAt")
    published_by: Mapped[Optional[str]] = mapped_column("publishedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class SelfHostedInstanceReport(Base):
    __tablename__ = "SelfHostedInstanceReport"

    id: Mapped[str] = mapped_column(primary_key=True)
    instance_id: Mapped[str] = mapped_column("instanceId")
    received_at: Mapped[datetime] = mapped_column("receivedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    version: Mapped[Optional[str]]
    report_schema_version: Mapped[Optional[int]] = mapped_column("reportSchemaVersion")
    unknown_fields: Mapped[int] = mapped_column("unknownFields", default=0, server_default=text("0"))
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("SelfHostedInstanceReport_instanceId_receivedAt_idx", "instanceId", "receivedAt"),
        Index("SelfHostedInstanceReport_receivedAt_idx", "receivedAt"),
    )

class WebPushSubscription(Base):
    __tablename__ = "WebPushSubscription"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    endpoint: Mapped[str]
    p256dh: Mapped[str]
    auth: Mapped[str]
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_success_at: Mapped[Optional[datetime]] = mapped_column("lastSuccessAt")

    __table_args__ = (
        Index("WebPushSubscription_endpoint_key", "endpoint", unique=True),
        Index("WebPushSubscription_userId_idx", "userId"),
    )

class WebPushVapidKey(Base):
    __tablename__ = "WebPushVapidKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    public_key: Mapped[str] = mapped_column("publicKey")
    private_key_encrypted: Mapped[str] = mapped_column("privateKeyEncrypted")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class SignInProviderMount(Base):
    __tablename__ = "SignInProviderMount"

    id: Mapped[str] = mapped_column(primary_key=True)
    provider: Mapped[Optional[str]]
    mounted: Mapped[bool]
    last_applied_event_id: Mapped[str] = mapped_column("lastAppliedEventId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
