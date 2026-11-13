from sqlalchemy import ARRAY, BigInteger, Column, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, MetaData, Numeric, PrimaryKeyConstraint, Table, Text, TypeDecorator, text
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

background_worker_to_background_worker_file = Table(
    "_BackgroundWorkerToBackgroundWorkerFile",
    Base.metadata,
    Column("A", Text, ForeignKey("BackgroundWorker.id", ondelete="CASCADE", onupdate="CASCADE", name="_BackgroundWorkerToBackgroundWorkerFile_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("BackgroundWorkerFile.id", ondelete="CASCADE", onupdate="CASCADE", name="_BackgroundWorkerToBackgroundWorkerFile_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_BackgroundWorkerToBackgroundWorkerFile_AB_pkey"),
    Index("_BackgroundWorkerToBackgroundWorkerFile_B_index", "B"),
)

background_worker_to_task_queue = Table(
    "_BackgroundWorkerToTaskQueue",
    Base.metadata,
    Column("A", Text, ForeignKey("BackgroundWorker.id", ondelete="CASCADE", onupdate="CASCADE", name="_BackgroundWorkerToTaskQueue_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("TaskQueue.id", ondelete="CASCADE", onupdate="CASCADE", name="_BackgroundWorkerToTaskQueue_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_BackgroundWorkerToTaskQueue_AB_pkey"),
    Index("_BackgroundWorkerToTaskQueue_B_index", "B"),
)

waitpoint_run_connections = Table(
    "_WaitpointRunConnections",
    Base.metadata,
    Column("A", Text, ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="_WaitpointRunConnections_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("Waitpoint.id", ondelete="CASCADE", onupdate="CASCADE", name="_WaitpointRunConnections_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_WaitpointRunConnections_AB_pkey"),
    Index("_WaitpointRunConnections_B_index", "B"),
)

completed_waitpoints = Table(
    "_completedWaitpoints",
    Base.metadata,
    Column("A", Text, ForeignKey("TaskRunExecutionSnapshot.id", ondelete="CASCADE", onupdate="CASCADE", name="_completedWaitpoints_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("Waitpoint.id", ondelete="CASCADE", onupdate="CASCADE", name="_completedWaitpoints_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_completedWaitpoints_AB_pkey"),
    Index("_completedWaitpoints_B_index", "B"),
)


class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    pending_email: Mapped[Optional[str]] = mapped_column("pendingEmail")
    authentication_method: Mapped[str] = mapped_column("authenticationMethod", Enum("GITHUB", "MAGIC_LINK", "GOOGLE", "SSO", name="AuthenticationMethod"))
    authentication_profile: Mapped[Optional[dict[str, Any]]] = mapped_column("authenticationProfile", JSONB(none_as_null=True))
    authentication_extra_params: Mapped[Optional[dict[str, Any]]] = mapped_column("authenticationExtraParams", JSONB(none_as_null=True))
    auth_identifier: Mapped[Optional[str]] = mapped_column("authIdentifier")
    display_name: Mapped[Optional[str]] = mapped_column("displayName")
    name: Mapped[Optional[str]]
    avatar_url: Mapped[Optional[str]] = mapped_column("avatarUrl")
    admin: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    dashboard_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("dashboardPreferences", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    is_on_cloud_waitlist: Mapped[bool] = mapped_column("isOnCloudWaitlist", default=False, server_default=text("false"))
    feature_cloud: Mapped[bool] = mapped_column("featureCloud", default=False, server_default=text("false"))
    is_on_hosted_repo_waitlist: Mapped[bool] = mapped_column("isOnHostedRepoWaitlist", default=False, server_default=text("false"))
    marketing_emails: Mapped[bool] = mapped_column("marketingEmails", default=True, server_default=text("true"))
    confirmed_basic_details: Mapped[bool] = mapped_column("confirmedBasicDetails", default=False, server_default=text("false"))
    referral_source: Mapped[Optional[str]] = mapped_column("referralSource")
    onboarding_data: Mapped[Optional[dict[str, Any]]] = mapped_column("onboardingData", JSONB(none_as_null=True))
    mfa_enabled_at: Mapped[Optional[datetime]] = mapped_column("mfaEnabledAt")
    mfa_secret_reference_id: Mapped[Optional[str]] = mapped_column("mfaSecretReferenceId", ForeignKey("SecretReference.id", ondelete="SET NULL", onupdate="CASCADE", name="User_mfaSecretReferenceId_fkey"))
    mfa_last_used_code: Mapped[Optional[str]] = mapped_column("mfaLastUsedCode")
    session_duration: Mapped[int] = mapped_column("sessionDuration", default=31556952, server_default=text("31556952"))
    next_session_end: Mapped[Optional[datetime]] = mapped_column("nextSessionEnd")
    invitation_code_id: Mapped[Optional[str]] = mapped_column("invitationCodeId", ForeignKey("InvitationCode.id", ondelete="SET NULL", onupdate="CASCADE", name="User_invitationCodeId_fkey"))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_authIdentifier_key", "authIdentifier", unique=True),
    )

    mfa_secret_reference: Mapped[Optional["SecretReference"]] = relationship(back_populates="user")
    invitation_code: Mapped[Optional["InvitationCode"]] = relationship(back_populates="users")
    org_memberships: Mapped[list["OrgMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sent_invites: Mapped[list["OrgMemberInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="inviter")
    personal_access_tokens: Mapped[list["PersonalAccessToken"]] = relationship(passive_deletes="all", back_populates="user")
    created_api_keys: Mapped[list["ApiKey"]] = relationship(back_populates="created_by")
    deployments: Mapped[list["WorkerDeployment"]] = relationship(back_populates="triggered_by")
    backup_codes: Mapped[list["MfaBackupCode"]] = relationship(passive_deletes="all", back_populates="user")
    bulk_actions: Mapped[list["BulkActionGroup"]] = relationship(back_populates="user")
    impersonations_performed: Mapped[list["ImpersonationAuditLog"]] = relationship(foreign_keys="ImpersonationAuditLog.admin_id", cascade="all, delete", passive_deletes=True, back_populates="admin")
    impersonations_received: Mapped[list["ImpersonationAuditLog"]] = relationship(foreign_keys="ImpersonationAuditLog.target_id", cascade="all, delete", passive_deletes=True, back_populates="target")
    support_access_requests_made: Mapped[list["SupportAccessRequest"]] = relationship(foreign_keys="SupportAccessRequest.requested_by_id", back_populates="requested_by")
    support_access_requests_approved: Mapped[list["SupportAccessRequest"]] = relationship(foreign_keys="SupportAccessRequest.approved_by_id", back_populates="approved_by")
    customer_queries: Mapped[list["CustomerQuery"]] = relationship(back_populates="user")
    metrics_dashboards: Mapped[list["MetricsDashboard"]] = relationship(back_populates="owner")
    platform_notifications: Mapped[list["PlatformNotification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    platform_notification_interactions: Mapped[list["PlatformNotificationInteraction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class MfaBackupCode(Base):
    __tablename__ = "MfaBackupCode"

    id: Mapped[str] = mapped_column(primary_key=True)
    code: Mapped[str]
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="MfaBackupCode_userId_fkey"))
    used_at: Mapped[Optional[datetime]] = mapped_column("usedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("MfaBackupCode_userId_code_key", "userId", "code", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="backup_codes")

class InvitationCode(Base):
    __tablename__ = "InvitationCode"

    id: Mapped[str] = mapped_column(primary_key=True)
    code: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("InvitationCode_code_key", "code", unique=True),
    )

    users: Mapped[list["User"]] = relationship(back_populates="invitation_code")

class AuthorizationCode(Base):
    __tablename__ = "AuthorizationCode"

    id: Mapped[str] = mapped_column(primary_key=True)
    code: Mapped[str]
    personal_access_token_id: Mapped[Optional[str]] = mapped_column("personalAccessTokenId", ForeignKey("PersonalAccessToken.id", ondelete="CASCADE", onupdate="CASCADE", name="AuthorizationCode_personalAccessTokenId_fkey"))
    requester_ip: Mapped[Optional[str]] = mapped_column("requesterIp")
    code_challenge: Mapped[Optional[str]] = mapped_column("codeChallenge")
    callback_port: Mapped[Optional[int]] = mapped_column("callbackPort")
    approval_code_hash: Mapped[Optional[str]] = mapped_column("approvalCodeHash")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AuthorizationCode_code_key", "code", unique=True),
    )

    personal_access_token: Mapped[Optional["PersonalAccessToken"]] = relationship(back_populates="authorization_codes")

class PersonalAccessToken(Base):
    __tablename__ = "PersonalAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    encrypted_token: Mapped[dict[str, Any]] = mapped_column("encryptedToken", JSONB)
    obfuscated_token: Mapped[str] = mapped_column("obfuscatedToken")
    hashed_token: Mapped[str] = mapped_column("hashedToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="PersonalAccessToken_userId_fkey"))
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    last_accessed_at: Mapped[Optional[datetime]] = mapped_column("lastAccessedAt")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    idle_timeout_seconds: Mapped[Optional[int]] = mapped_column("idleTimeoutSeconds")
    expiry_reminder_sent_at: Mapped[Optional[datetime]] = mapped_column("expiryReminderSentAt")
    expired_notice_sent_at: Mapped[Optional[datetime]] = mapped_column("expiredNoticeSentAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PersonalAccessToken_hashedToken_key", "hashedToken", unique=True),
        Index("PersonalAccessToken_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="personal_access_tokens")
    authorization_codes: Mapped[list["AuthorizationCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="personal_access_token")

class OrganizationAccessToken(Base):
    __tablename__ = "OrganizationAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("USER", "SYSTEM", name="OrganizationAccessTokenType"), default="USER", server_default=text("'USER'"))
    hashed_token: Mapped[str] = mapped_column("hashedToken")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrganizationAccessToken_organizationId_fkey"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    last_accessed_at: Mapped[Optional[datetime]] = mapped_column("lastAccessedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrganizationAccessToken_hashedToken_key", "hashedToken", unique=True),
        Index("OrganizationAccessToken_organizationId_type_createdAt_idx", "organizationId", "type", "createdAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="organization_access_tokens")

class Organization(Base):
    __tablename__ = "Organization"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    title: Mapped[str]
    maximum_execution_time_per_run_in_ms: Mapped[int] = mapped_column("maximumExecutionTimePerRunInMs", default=900000, server_default=text("900000"))
    maximum_concurrency_limit: Mapped[int] = mapped_column("maximumConcurrencyLimit", default=10, server_default=text("10"))
    maximum_schedules_limit: Mapped[int] = mapped_column("maximumSchedulesLimit", default=5, server_default=text("5"))
    maximum_dev_queue_size: Mapped[Optional[int]] = mapped_column("maximumDevQueueSize")
    maximum_deployed_queue_size: Mapped[Optional[int]] = mapped_column("maximumDeployedQueueSize")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    company_size: Mapped[Optional[str]] = mapped_column("companySize")
    onboarding_data: Mapped[Optional[dict[str, Any]]] = mapped_column("onboardingData", JSONB(none_as_null=True))
    avatar: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    runs_enabled: Mapped[bool] = mapped_column("runsEnabled", default=True, server_default=text("true"))
    is_activated: Mapped[bool] = mapped_column("v3Enabled", default=False, server_default=text("false"))
    v2_enabled: Mapped[bool] = mapped_column("v2Enabled", default=False, server_default=text("false"))
    v2_marqs_enabled: Mapped[bool] = mapped_column("v2MarqsEnabled", default=False, server_default=text("false"))
    has_requested_v3: Mapped[bool] = mapped_column("hasRequestedV3", default=False, server_default=text("false"))
    api_rate_limiter_config: Mapped[Optional[dict[str, Any]]] = mapped_column("apiRateLimiterConfig", JSONB(none_as_null=True))
    realtime_rate_limiter_config: Mapped[Optional[dict[str, Any]]] = mapped_column("realtimeRateLimiterConfig", JSONB(none_as_null=True))
    batch_rate_limit_config: Mapped[Optional[dict[str, Any]]] = mapped_column("batchRateLimitConfig", JSONB(none_as_null=True))
    batch_queue_concurrency_config: Mapped[Optional[dict[str, Any]]] = mapped_column("batchQueueConcurrencyConfig", JSONB(none_as_null=True))
    webhook_limits_config: Mapped[Optional[dict[str, Any]]] = mapped_column("webhookLimitsConfig", JSONB(none_as_null=True))
    feature_flags: Mapped[Optional[dict[str, Any]]] = mapped_column("featureFlags", JSONB(none_as_null=True))
    maximum_project_count: Mapped[int] = mapped_column("maximumProjectCount", default=25, server_default=text("25"))
    max_session_duration: Mapped[Optional[int]] = mapped_column("maxSessionDuration")
    stream_basin_name: Mapped[Optional[str]] = mapped_column("streamBasinName")
    support_access_mode: Mapped[str] = mapped_column("supportAccessMode", Enum("ALLOW", "REQUIRES_REQUEST", name="SupportAccessMode"), default="ALLOW", server_default=text("'ALLOW'"))
    support_access_mode_updated_at: Mapped[Optional[datetime]] = mapped_column("supportAccessModeUpdatedAt")
    support_access_mode_updated_by_id: Mapped[Optional[str]] = mapped_column("supportAccessModeUpdatedById")

    __table_args__ = (
        Index("Organization_slug_key", "slug", unique=True),
    )

    environments: Mapped[list["RuntimeEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    projects: Mapped[list["Project"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    members: Mapped[list["OrgMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    invites: Mapped[list["OrgMemberInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    organization_integrations: Mapped[list["OrganizationIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    organization_access_tokens: Mapped[list["OrganizationAccessToken"]] = relationship(passive_deletes="all", back_populates="organization")
    worker_groups: Mapped[list["WorkerInstanceGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    worker_instances: Mapped[list["WorkerInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    github_app_installations: Mapped[list["GithubAppInstallation"]] = relationship(passive_deletes="all", back_populates="organization")
    customer_queries: Mapped[list["CustomerQuery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    metrics_dashboards: Mapped[list["MetricsDashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    prompts: Mapped[list["Prompt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    platform_notifications: Mapped[list["PlatformNotification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    error_group_states: Mapped[list["ErrorGroupState"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    support_access_requests: Mapped[list["SupportAccessRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")

class OrgMember(Base):
    __tablename__ = "OrgMember"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="OrgMember_organizationId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OrgMember_userId_fkey"))
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", name="OrgMemberRole"), default="MEMBER", server_default=text("'MEMBER'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrgMember_userId_idx", "userId"),
        Index("OrgMember_organizationId_userId_key", "organizationId", "userId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="members")
    user: Mapped["User"] = relationship(back_populates="org_memberships")
    environments: Mapped[list["RuntimeEnvironment"]] = relationship(back_populates="org_member")

class OrgMemberInvite(Base):
    __tablename__ = "OrgMemberInvite"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    email: Mapped[str]
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", name="OrgMemberRole"), default="MEMBER", server_default=text("'MEMBER'"))
    rbac_role_id: Mapped[Optional[str]] = mapped_column("rbacRoleId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="OrgMemberInvite_organizationId_fkey"))
    inviter_id: Mapped[str] = mapped_column("inviterId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OrgMemberInvite_inviterId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrgMemberInvite_token_key", "token", unique=True),
        Index("OrgMemberInvite_organizationId_email_key", "organizationId", "email", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="invites")
    inviter: Mapped["User"] = relationship(back_populates="sent_invites")

class RuntimeEnvironment(Base):
    __tablename__ = "RuntimeEnvironment"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    api_key: Mapped[str] = mapped_column("apiKey")
    root_api_key_hidden_at: Mapped[Optional[datetime]] = mapped_column("rootApiKeyHiddenAt")
    pk_api_key: Mapped[str] = mapped_column("pkApiKey")
    type: Mapped[str] = mapped_column(Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"), default="DEVELOPMENT", server_default=text("'DEVELOPMENT'"))
    is_branchable_environment: Mapped[bool] = mapped_column("isBranchableEnvironment", default=False, server_default=text("false"))
    branch_name: Mapped[Optional[str]] = mapped_column("branchName")
    parent_environment_id: Mapped[Optional[str]] = mapped_column("parentEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="RuntimeEnvironment_parentEnvironmentId_fkey"))
    preview_auto_archive_after_days: Mapped[Optional[int]] = mapped_column("previewAutoArchiveAfterDays")
    preview_auto_archive_excluded_branches: Mapped[list[str]] = mapped_column("previewAutoArchiveExcludedBranches", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    preview_auto_archive_next_check_at: Mapped[Optional[datetime]] = mapped_column("previewAutoArchiveNextCheckAt")
    preview_auto_archive_cursor_created_at: Mapped[Optional[datetime]] = mapped_column("previewAutoArchiveCursorCreatedAt")
    preview_auto_archive_cursor_id: Mapped[Optional[str]] = mapped_column("previewAutoArchiveCursorId")
    git: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    shortcode: Mapped[str]
    maximum_concurrency_limit: Mapped[int] = mapped_column("maximumConcurrencyLimit", default=5, server_default=text("5"))
    concurrency_limit_burst_factor: Mapped[DecimalType] = mapped_column("concurrencyLimitBurstFactor", Numeric(precision=4, scale=2), default=DecimalType("2"), server_default=text("2"))
    paused: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    pause_source: Mapped[Optional[str]] = mapped_column("pauseSource", Enum("BILLING_LIMIT", "ADMIN_BAN", name="EnvironmentPauseSource"))
    auto_enable_internal_sources: Mapped[bool] = mapped_column("autoEnableInternalSources", default=True, server_default=text("true"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RuntimeEnvironment_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="RuntimeEnvironment_projectId_fkey"))
    org_member_id: Mapped[Optional[str]] = mapped_column("orgMemberId", ForeignKey("OrgMember.id", ondelete="SET NULL", onupdate="CASCADE", name="RuntimeEnvironment_orgMemberId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    built_in_environment_variable_overrides: Mapped[Optional[dict[str, Any]]] = mapped_column("builtInEnvironmentVariableOverrides", JSONB(none_as_null=True))
    tunnel_id: Mapped[Optional[str]] = mapped_column("tunnelId")
    current_session_id: Mapped[Optional[str]] = mapped_column("currentSessionId", ForeignKey("RuntimeEnvironmentSession.id", ondelete="SET NULL", onupdate="CASCADE", name="RuntimeEnvironment_currentSessionId_fkey"))

    __table_args__ = (
        Index("RuntimeEnvironment_apiKey_key", "apiKey", unique=True),
        Index("RuntimeEnvironment_pkApiKey_key", "pkApiKey", unique=True),
        Index("RuntimeEnvironment_parentEnvironmentId_idx", "parentEnvironmentId"),
        Index("RuntimeEnvironment_projectId_idx", "projectId"),
        Index("RuntimeEnvironment_organizationId_idx", "organizationId"),
        Index("RuntimeEnvironment_projectId_slug_orgMemberId_key", "projectId", "slug", "orgMemberId", unique=True),
        Index("RuntimeEnvironment_projectId_shortcode_key", "projectId", "shortcode", unique=True),
    )

    parent_environment: Mapped[Optional["RuntimeEnvironment"]] = relationship(remote_side=[id], back_populates="child_environments")
    organization: Mapped["Organization"] = relationship(back_populates="environments")
    project: Mapped["Project"] = relationship(back_populates="environments")
    org_member: Mapped[Optional["OrgMember"]] = relationship(back_populates="environments")
    current_session: Mapped[Optional["RuntimeEnvironmentSession"]] = relationship(foreign_keys=[current_session_id], back_populates="current_environments")
    encryption_keys: Mapped[list["EnvironmentEncryptionKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    child_environments: Mapped[list["RuntimeEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent_environment")
    background_workers: Mapped[list["BackgroundWorker"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    background_worker_tasks: Mapped[list["BackgroundWorkerTask"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    task_runs: Mapped[list["TaskRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    task_queues: Mapped[list["TaskQueue"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    environment_variable_values: Mapped[list["EnvironmentVariableValue"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    checkpoints: Mapped[list["Checkpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    worker_deployments: Mapped[list["WorkerDeployment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    worker_deployment_promotions: Mapped[list["WorkerDeploymentPromotion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    task_run_attempts: Mapped[list["TaskRunAttempt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    checkpoint_restore_event: Mapped[list["CheckpointRestoreEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    task_schedule_instances: Mapped[list["TaskScheduleInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    alerts: Mapped[list["ProjectAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    sessions: Mapped[list["RuntimeEnvironmentSession"]] = relationship(foreign_keys="RuntimeEnvironmentSession.environment_id", cascade="all, delete", passive_deletes=True, back_populates="environment")
    task_run_number_counter: Mapped[list["TaskRunNumberCounter"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    task_run_checkpoints: Mapped[list["TaskRunCheckpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    waitpoints: Mapped[list["Waitpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    worker_instances: Mapped[list["WorkerInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    waitpoint_tags: Mapped[list["WaitpointTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    bulk_action_group: Mapped[list["BulkActionGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    customer_queries: Mapped[list["CustomerQuery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    prompts: Mapped[list["Prompt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    playground_conversations: Mapped[list["PlaygroundConversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    error_group_states: Mapped[list["ErrorGroupState"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="environment")
    task_identifiers: Mapped[list["TaskIdentifier"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    revoked_api_keys: Mapped[list["RevokedApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")
    encryption_policy: Mapped[Optional["EnvironmentEncryptionPolicy"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="active_for,active_key", back_populates="environment")
    trusted_deploy_grant: Mapped[Optional["TrustedDeployEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="runtime_environment")

class ApiKey(Base):
    __tablename__ = "api_keys"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    key_hash: Mapped[str]
    last_four: Mapped[str]
    description: Mapped[Optional[str]]
    runtime_environment_id: Mapped[str] = mapped_column(ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="api_keys_runtime_environment_id_fkey"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="api_keys_created_by_user_id_fkey"))
    issued_by: Mapped[str] = mapped_column(Enum("USER", "SYSTEM", name="ApiKeyIssuer"), default="USER", server_default=text("'USER'"))
    preset_id: Mapped[Optional[str]]
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    allowed_ip_ranges: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    last_used_at: Mapped[Optional[datetime]]
    last_used_ip: Mapped[Optional[str]]
    revoked_at: Mapped[Optional[datetime]]
    expires_at: Mapped[Optional[datetime]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("api_keys_key_hash_key", "key_hash", unique=True),
        Index("api_keys_runtime_environment_id_revoked_at_created_at_idx", "runtime_environment_id", "revoked_at", "created_at"),
        Index("api_keys_env_issued_by_revoked_at_created_at_idx", "runtime_environment_id", "issued_by", "revoked_at", "created_at"),
    )

    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="api_keys")
    created_by: Mapped[Optional["User"]] = relationship(back_populates="created_api_keys")

class RevokedApiKey(Base):
    __tablename__ = "RevokedApiKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    api_key: Mapped[str] = mapped_column("apiKey")
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="RevokedApiKey_runtimeEnvironmentId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("RevokedApiKey_apiKey_idx", "apiKey"),
        Index("RevokedApiKey_runtimeEnvironmentId_idx", "runtimeEnvironmentId"),
    )

    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="revoked_api_keys")

class Project(Base):
    __tablename__ = "Project"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    name: Mapped[str]
    external_ref: Mapped[str] = mapped_column("externalRef")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Project_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    initialized_at: Mapped[Optional[datetime]] = mapped_column("initializedAt")
    default_runtime: Mapped[Optional[str]] = mapped_column("defaultRuntime")
    version: Mapped[str] = mapped_column(Enum("V2", "V3", name="ProjectVersion"), default="V2", server_default=text("'V2'"))
    engine: Mapped[str] = mapped_column(Enum("V1", "V2", name="RunEngineVersion"), default="V1", server_default=text("'V1'"))
    builder_project_id: Mapped[Optional[str]] = mapped_column("builderProjectId")
    default_worker_group_id: Mapped[Optional[str]] = mapped_column("defaultWorkerGroupId", ForeignKey("WorkerInstanceGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="Project_defaultWorkerGroupId_fkey"))
    allowed_worker_queues: Mapped[list[str]] = mapped_column("allowedMasterQueues", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    build_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("buildSettings", JSONB(none_as_null=True))
    onboarding_data: Mapped[Optional[dict[str, Any]]] = mapped_column("onboardingData", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Project_slug_key", "slug", unique=True),
        Index("Project_externalRef_key", "externalRef", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="projects")
    default_worker_group: Mapped[Optional["WorkerInstanceGroup"]] = relationship(foreign_keys=[default_worker_group_id], back_populates="default_for_projects")
    worker_groups: Mapped[list["WorkerInstanceGroup"]] = relationship(foreign_keys="WorkerInstanceGroup.project_id", cascade="all, delete", passive_deletes=True, back_populates="project")
    workers: Mapped[list["WorkerInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    environments: Mapped[list["RuntimeEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    background_workers: Mapped[list["BackgroundWorker"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    background_worker_tasks: Mapped[list["BackgroundWorkerTask"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_runs: Mapped[list["TaskRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    run_tags: Mapped[list["TaskRunTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_queues: Mapped[list["TaskQueue"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    environment_variables: Mapped[list["EnvironmentVariable"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    checkpoints: Mapped[list["Checkpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    worker_deployment: Mapped[list["WorkerDeployment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    checkpoint_restore_event: Mapped[list["CheckpointRestoreEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_schedules: Mapped[list["TaskSchedule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    alert_channels: Mapped[list["ProjectAlertChannel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    alerts: Mapped[list["ProjectAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    alert_storages: Mapped[list["ProjectAlertStorage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    bulk_action_groups: Mapped[list["BulkActionGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    background_worker_file: Mapped[list["BackgroundWorkerFile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    waitpoints: Mapped[list["Waitpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_run_waitpoints: Mapped[list["TaskRunWaitpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_run_checkpoints: Mapped[list["TaskRunCheckpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    waitpoint_tags: Mapped[list["WaitpointTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    organization_project_integration: Mapped[list["OrganizationProjectIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    customer_queries: Mapped[list["CustomerQuery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    playground_conversations: Mapped[list["PlaygroundConversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_schedule_instances: Mapped[list["TaskScheduleInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    metrics_dashboards: Mapped[list["MetricsDashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    llm_models: Mapped[list["LlmModel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    prompts: Mapped[list["Prompt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    platform_notifications: Mapped[list["PlatformNotification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    error_group_states: Mapped[list["ErrorGroupState"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    task_identifiers: Mapped[list["TaskIdentifier"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    connected_github_repository: Mapped[Optional["ConnectedGithubRepository"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    trusted_deploy_repository: Mapped[Optional["TrustedDeployRepository"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")

class EnvironmentEncryptionPolicy(Base):
    __tablename__ = "EnvironmentEncryptionPolicy"

    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvironmentEncryptionPolicy_environmentId_fkey"), primary_key=True)
    revision: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    wrapping_provider: Mapped[Optional[str]] = mapped_column("wrappingProvider", Enum("PLATFORM_KEYRING", "AWS_KMS", name="EnvironmentKeyWrappingProvider"))
    wrapping_key_reference: Mapped[Optional[str]] = mapped_column("wrappingKeyReference")
    active_key_id: Mapped[Optional[str]] = mapped_column("activeKeyId")

    __table_args__ = (
        ForeignKeyConstraint(["activeKeyId", "environmentId"], ["EnvironmentEncryptionKey.id", "EnvironmentEncryptionKey.environmentId"], ondelete="NO ACTION", onupdate="NO ACTION", name="EnvironmentEncryptionPolicy_activeKeyId_environmentId_fkey"),
    )

    environment: Mapped["RuntimeEnvironment"] = relationship(overlaps="active_for,active_key", back_populates="encryption_policy")
    active_key: Mapped[Optional["EnvironmentEncryptionKey"]] = relationship(overlaps="encryption_policy,environment", back_populates="active_for")

class EnvironmentEncryptionKey(Base):
    __tablename__ = "EnvironmentEncryptionKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvironmentEncryptionKey_environmentId_fkey"))
    wrapped_key: Mapped[dict[str, Any]] = mapped_column("wrappedKey", JSONB)
    wrapping_provider: Mapped[str] = mapped_column("wrappingProvider", Enum("PLATFORM_KEYRING", "AWS_KMS", name="EnvironmentKeyWrappingProvider"))
    wrapping_key_reference: Mapped[str] = mapped_column("wrappingKeyReference")
    revision: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("EnvironmentEncryptionKey_environmentId_idx", "environmentId"),
        Index("EnvironmentEncryptionKey_id_environmentId_key", "id", "environmentId", unique=True),
    )

    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="encryption_keys")
    active_for: Mapped[list["EnvironmentEncryptionPolicy"]] = relationship(passive_deletes="all", overlaps="encryption_policy,environment", back_populates="active_key")

class SecretReference(Base):
    __tablename__ = "SecretReference"

    id: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str]
    provider: Mapped[str] = mapped_column(Enum("DATABASE", "AWS_PARAM_STORE", name="SecretStoreProvider"), default="DATABASE", server_default=text("'DATABASE'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("SecretReference_key_key", "key", unique=True),
    )

    environment_variable_values: Mapped[list["EnvironmentVariableValue"]] = relationship(back_populates="value_reference")
    organization_integration: Mapped[list["OrganizationIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="token_reference")
    user: Mapped[list["User"]] = relationship(back_populates="mfa_secret_reference")

class SecretStore(Base):
    __tablename__ = "SecretStore"

    key: Mapped[str] = mapped_column()
    value: Mapped[dict[str, Any]] = mapped_column(JSONB)
    version: Mapped[str] = mapped_column(default="1", server_default=text("'1'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("SecretStore_key_key", "key", unique=True),
        Index("SecretStore_key_idx", "key"),
    )

    __mapper_args__ = {"primary_key": [key]}

class OidcSigningKey(Base):
    __tablename__ = "OidcSigningKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    kid: Mapped[str]
    purpose: Mapped[str] = mapped_column(Enum("EXTERNAL_OIDC", "INTERNAL_RUN", name="OidcSigningKeyPurpose"), default="EXTERNAL_OIDC", server_default=text("'EXTERNAL_OIDC'"))
    status: Mapped[str] = mapped_column(Enum("STAGED", "ACTIVE", "RETIRED", name="OidcSigningKeyStatus"))
    public_jwk: Mapped[dict[str, Any]] = mapped_column("publicJwk", JSONB)
    ready_at: Mapped[datetime] = mapped_column("readyAt")
    public_until: Mapped[Optional[datetime]] = mapped_column("publicUntil")
    generated_by_user_id: Mapped[str] = mapped_column("generatedByUserId")
    activated_at: Mapped[Optional[datetime]] = mapped_column("activatedAt")
    activated_by_user_id: Mapped[Optional[str]] = mapped_column("activatedByUserId")
    retired_at: Mapped[Optional[datetime]] = mapped_column("retiredAt")
    retired_by_user_id: Mapped[Optional[str]] = mapped_column("retiredByUserId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OidcSigningKey_kid_key", "kid", unique=True),
    )

class DataMigration(Base):
    __tablename__ = "DataMigration"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")

    __table_args__ = (
        Index("DataMigration_name_key", "name", unique=True),
    )

class BackgroundWorker(Base):
    __tablename__ = "BackgroundWorker"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    engine: Mapped[str] = mapped_column(Enum("V1", "V2", name="RunEngineVersion"), default="V1", server_default=text("'V1'"))
    content_hash: Mapped[str] = mapped_column("contentHash")
    sdk_version: Mapped[str] = mapped_column("sdkVersion", default="unknown", server_default=text("'unknown'"))
    cli_version: Mapped[str] = mapped_column("cliVersion", default="unknown", server_default=text("'unknown'"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorker_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorker_runtimeEnvironmentId_fkey"))
    version: Mapped[str]
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB)
    runtime: Mapped[Optional[str]]
    runtime_version: Mapped[Optional[str]] = mapped_column("runtimeVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    worker_group_id: Mapped[Optional[str]] = mapped_column("workerGroupId", ForeignKey("WorkerInstanceGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="BackgroundWorker_workerGroupId_fkey"))
    supports_lazy_attempts: Mapped[bool] = mapped_column("supportsLazyAttempts", default=False, server_default=text("false"))

    __table_args__ = (
        Index("BackgroundWorker_friendlyId_key", "friendlyId", unique=True),
        Index("BackgroundWorker_runtimeEnvironmentId_idx", "runtimeEnvironmentId"),
        Index("BackgroundWorker_runtimeEnvironmentId_createdAt_idx", "runtimeEnvironmentId", "createdAt"),
        Index("BackgroundWorker_projectId_runtimeEnvironmentId_version_key", "projectId", "runtimeEnvironmentId", "version", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="background_workers")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="background_workers")
    worker_group: Mapped[Optional["WorkerInstanceGroup"]] = relationship(back_populates="background_workers")
    tasks: Mapped[list["BackgroundWorkerTask"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="worker")
    attempts: Mapped[list["TaskRunAttempt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="background_worker")
    locked_runs: Mapped[list["TaskRun"]] = relationship(back_populates="locked_to_version")
    prompt_versions: Mapped[list["PromptVersion"]] = relationship(back_populates="worker")
    task_identifiers: Mapped[list["TaskIdentifier"]] = relationship(back_populates="current_worker")
    deployment: Mapped[Optional["WorkerDeployment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="worker")
    files: Mapped[list["BackgroundWorkerFile"]] = relationship(secondary=background_worker_to_background_worker_file, back_populates="background_workers")
    queues: Mapped[list["TaskQueue"]] = relationship(secondary=background_worker_to_task_queue, back_populates="workers")

class BackgroundWorkerFile(Base):
    __tablename__ = "BackgroundWorkerFile"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    file_path: Mapped[str] = mapped_column("filePath")
    content_hash: Mapped[str] = mapped_column("contentHash")
    contents: Mapped[bytes]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorkerFile_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("BackgroundWorkerFile_friendlyId_key", "friendlyId", unique=True),
        Index("BackgroundWorkerFile_projectId_contentHash_key", "projectId", "contentHash", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="background_worker_file")
    tasks: Mapped[list["BackgroundWorkerTask"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="file")
    background_workers: Mapped[list["BackgroundWorker"]] = relationship(secondary=background_worker_to_background_worker_file, back_populates="files")

class Prompt(Base):
    __tablename__ = "prompts"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str]
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(default="text", server_default=text("'text'"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="prompts_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="prompts_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="prompts_runtimeEnvironmentId_fkey"))
    file_path: Mapped[Optional[str]] = mapped_column("filePath")
    export_name: Mapped[Optional[str]] = mapped_column("exportName")
    variable_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("variableSchema", JSONB(none_as_null=True))
    default_model: Mapped[Optional[str]] = mapped_column("defaultModel")
    default_config: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultConfig", JSONB(none_as_null=True))
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("prompts_friendly_id_key", "friendly_id", unique=True),
        Index("prompts_projectId_idx", "projectId"),
        Index("prompts_runtimeEnvironmentId_idx", "runtimeEnvironmentId"),
        Index("prompts_projectId_runtimeEnvironmentId_slug_key", "projectId", "runtimeEnvironmentId", "slug", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="prompts")
    project: Mapped["Project"] = relationship(back_populates="prompts")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="prompts")
    versions: Mapped[list["PromptVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="prompt")

class PromptVersion(Base):
    __tablename__ = "prompt_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[int]
    prompt_id: Mapped[str] = mapped_column("promptId", ForeignKey("prompts.id", ondelete="CASCADE", onupdate="CASCADE", name="prompt_versions_promptId_fkey"))
    text_content: Mapped[Optional[str]] = mapped_column("textContent")
    chat_content: Mapped[Optional[dict[str, Any]]] = mapped_column("chatContent", JSONB(none_as_null=True))
    model: Mapped[Optional[str]]
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    source: Mapped[str]
    commit_message: Mapped[Optional[str]] = mapped_column("commitMessage")
    content_hash: Mapped[str] = mapped_column("contentHash")
    labels: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    worker_id: Mapped[Optional[str]] = mapped_column("workerId", ForeignKey("BackgroundWorker.id", ondelete="SET NULL", onupdate="CASCADE", name="prompt_versions_workerId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("prompt_versions_promptId_idx", "promptId"),
        Index("prompt_versions_contentHash_idx", "contentHash"),
        Index("prompt_versions_promptId_version_key", "promptId", "version", unique=True),
    )

    prompt: Mapped["Prompt"] = relationship(back_populates="versions")
    worker: Mapped[Optional["BackgroundWorker"]] = relationship(back_populates="prompt_versions")

class BackgroundWorkerTask(Base):
    __tablename__ = "BackgroundWorkerTask"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    file_path: Mapped[str] = mapped_column("filePath")
    export_name: Mapped[Optional[str]] = mapped_column("exportName")
    worker_id: Mapped[str] = mapped_column("workerId", ForeignKey("BackgroundWorker.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorkerTask_workerId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorkerTask_projectId_fkey"))
    file_id: Mapped[Optional[str]] = mapped_column("fileId", ForeignKey("BackgroundWorkerFile.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorkerTask_fileId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="BackgroundWorkerTask_runtimeEnvironmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    queue_config: Mapped[Optional[dict[str, Any]]] = mapped_column("queueConfig", JSONB(none_as_null=True))
    retry_config: Mapped[Optional[dict[str, Any]]] = mapped_column("retryConfig", JSONB(none_as_null=True))
    machine_config: Mapped[Optional[dict[str, Any]]] = mapped_column("machineConfig", JSONB(none_as_null=True))
    gates: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    queue_id: Mapped[Optional[str]] = mapped_column("queueId", ForeignKey("TaskQueue.id", ondelete="SET NULL", onupdate="CASCADE", name="BackgroundWorkerTask_queueId_fkey"))
    max_duration_in_seconds: Mapped[Optional[int]] = mapped_column("maxDurationInSeconds")
    ttl: Mapped[Optional[str]]
    trigger_source: Mapped[str] = mapped_column("triggerSource", Enum("STANDARD", "SCHEDULED", "AGENT", "WEBHOOK", name="TaskTriggerSource"), default="STANDARD", server_default=text("'STANDARD'"))
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    payload_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("payloadSchema", JSONB(none_as_null=True))

    __table_args__ = (
        Index("BackgroundWorkerTask_friendlyId_key", "friendlyId", unique=True),
        Index("BackgroundWorkerTask_projectId_slug_idx", "projectId", "slug"),
        Index("BackgroundWorkerTask_projectId_slug_createdAt_idx", "projectId", "slug", "createdAt"),
        Index("BackgroundWorkerTask_runtimeEnvironmentId_projectId_idx", "runtimeEnvironmentId", "projectId"),
        Index("BackgroundWorkerTask_runtimeEnvironmentId_slug_triggerSourc_idx", "runtimeEnvironmentId", "slug", "triggerSource"),
        Index("BackgroundWorkerTask_workerId_slug_key", "workerId", "slug", unique=True),
    )

    worker: Mapped["BackgroundWorker"] = relationship(back_populates="tasks")
    project: Mapped["Project"] = relationship(back_populates="background_worker_tasks")
    file: Mapped[Optional["BackgroundWorkerFile"]] = relationship(back_populates="tasks")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="background_worker_tasks")
    queue: Mapped[Optional["TaskQueue"]] = relationship(back_populates="tasks")
    attempts: Mapped[list["TaskRunAttempt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="background_worker_task")
    runs: Mapped[list["TaskRun"]] = relationship(back_populates="locked_by")

class WebhookEndpoint(Base):
    __tablename__ = "WebhookEndpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    opaque_id: Mapped[str] = mapped_column("opaqueId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    endpoint_tenant_id: Mapped[str] = mapped_column("endpointTenantId", default="", server_default=text("''"))
    endpoint_external_ref: Mapped[str] = mapped_column("endpointExternalRef", default="", server_default=text("''"))
    source: Mapped[str]
    declared_id: Mapped[str] = mapped_column("declaredId")
    routing_targets: Mapped[dict[str, Any]] = mapped_column("routingTargets", JSONB, default=lambda: [], server_default=text("'[]'"))
    verifier_artifact: Mapped[dict[str, Any]] = mapped_column("verifierArtifact", JSONB)
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    secret_provisioning: Mapped[str] = mapped_column("secretProvisioning", default="either", server_default=text("'either'"))
    setup_prompt: Mapped[Optional[str]] = mapped_column("setupPrompt")
    signing_secret_key: Mapped[Optional[str]] = mapped_column("signingSecretKey")
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "INACTIVE", "DELETING", name="WebhookEndpointStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    manually_deactivated_at: Mapped[Optional[datetime]] = mapped_column("manuallyDeactivatedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WebhookEndpoint_friendlyId_key", "friendlyId", unique=True),
        Index("WebhookEndpoint_opaqueId_key", "opaqueId", unique=True),
        Index("WebhookEndpoint_runtimeEnvironmentId_source_idx", "runtimeEnvironmentId", "source"),
        Index("WebhookEndpoint_projectId_createdAt_idx", "projectId", "createdAt"),
        Index("WebhookEndpoint_runtimeEnvironmentId_declaredId_endpointTen_key", "runtimeEnvironmentId", "declaredId", "endpointTenantId", "endpointExternalRef", unique=True),
    )

class WebhookDelivery(Base):
    __tablename__ = "WebhookDelivery"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    webhook_endpoint_id: Mapped[str] = mapped_column("webhookEndpointId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    external_delivery_id: Mapped[str] = mapped_column("externalDeliveryId")
    idempotency_key: Mapped[str] = mapped_column("idempotencyKey")
    run_id: Mapped[Optional[str]] = mapped_column("runId")
    status: Mapped[str] = mapped_column(Enum("PENDING", "PROCESSING", "SUCCEEDED", "FAILED", "FILTERED", "UNMATCHED", name="WebhookDeliveryStatus"), default="PENDING", server_default=text("'PENDING'"))
    is_test: Mapped[bool] = mapped_column("isTest", default=False, server_default=text("false"))
    parsed_event: Mapped[Optional[dict[str, Any]]] = mapped_column("parsedEvent", JSONB(none_as_null=True))
    headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    raw_body_hash: Mapped[Optional[str]] = mapped_column("rawBodyHash")
    error_message: Mapped[Optional[str]] = mapped_column("errorMessage")
    filter_reason: Mapped[Optional[str]] = mapped_column("filterReason")
    targets: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", primary_key=True, default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    processed_at: Mapped[Optional[datetime]] = mapped_column("processedAt")
    retention_days: Mapped[int] = mapped_column("retentionDays", primary_key=True)

    __table_args__ = (
        Index("WebhookDelivery_webhookEndpointId_createdAt_idx", "webhookEndpointId", "createdAt"),
    )

class Alert(Base):
    __tablename__ = "Alert"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    environment_id: Mapped[str] = mapped_column("environmentId")
    capability_key: Mapped[str] = mapped_column("capabilityKey")
    params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    condition: Mapped[dict[str, Any]] = mapped_column(JSONB)
    query: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt", UtcDateTimeTz(precision=6))
    lifecycle: Mapped[str] = mapped_column(default="active", server_default=text("'active'"))
    attention: Mapped[str] = mapped_column(default="clear", server_default=text("'clear'"))
    observed_condition: Mapped[str] = mapped_column("observedCondition", default="unknown", server_default=text("'unknown'"))
    observed_condition_changed_at: Mapped[Optional[datetime]] = mapped_column("observedConditionChangedAt", UtcDateTimeTz(precision=6))
    pending_met_since: Mapped[Optional[datetime]] = mapped_column("pendingMetSince", UtcDateTimeTz(precision=6))
    evaluator_state: Mapped[Optional[dict[str, Any]]] = mapped_column("evaluatorState", JSONB(none_as_null=True))
    evaluation_lease_id: Mapped[Optional[str]] = mapped_column("evaluationLeaseId")
    evaluation_lease_until: Mapped[Optional[datetime]] = mapped_column("evaluationLeaseUntil", UtcDateTimeTz(precision=6))
    last_evaluated_at: Mapped[Optional[datetime]] = mapped_column("lastEvaluatedAt", UtcDateTimeTz(precision=6))
    next_evaluation_at: Mapped[Optional[datetime]] = mapped_column("nextEvaluationAt", UtcDateTimeTz(precision=6))
    notifiers: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    name: Mapped[Optional[str]]
    created_by: Mapped[str] = mapped_column("createdBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt", UtcDateTimeTz(precision=6))
    resolved_by: Mapped[Optional[str]] = mapped_column("resolvedBy")
    stopped_at: Mapped[Optional[datetime]] = mapped_column("stoppedAt", UtcDateTimeTz(precision=6))
    stopped_by: Mapped[Optional[str]] = mapped_column("stoppedBy")
    last_occurrence_at: Mapped[Optional[datetime]] = mapped_column("lastOccurrenceAt", UtcDateTimeTz(precision=6))
    occurrences_since_raise: Mapped[int] = mapped_column("occurrencesSinceRaise", default=0, server_default=text("0"))
    occurrences_total: Mapped[int] = mapped_column("occurrencesTotal", BigInteger, default=0, server_default=text("0"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId")

    __table_args__ = (
        Index("Alert_environmentId_lifecycle_idx", "environmentId", "lifecycle"),
        Index("Alert_lifecycle_evaluationLeaseUntil_idx", "lifecycle", "evaluationLeaseUntil"),
        Index("Alert_projectId_idx", "projectId"),
        Index("Alert_organizationId_idx", "organizationId"),
        Index("Alert_groupId_idx", "groupId"),
    )

class AlertEvent(Base):
    __tablename__ = "AlertEvent"

    id: Mapped[str] = mapped_column(primary_key=True)
    alert_id: Mapped[str] = mapped_column("alertId")
    type: Mapped[str]
    at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), primary_key=True, default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    facts: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    actor_id: Mapped[Optional[str]] = mapped_column("actorId")
    source_event_id: Mapped[Optional[str]] = mapped_column("sourceEventId")

    __table_args__ = (
        Index("AlertEvent_alertId_at_idx", "alertId", "at"),
    )

class AlertOccurrenceSeen(Base):
    __tablename__ = "AlertOccurrenceSeen"

    alert_id: Mapped[str] = mapped_column("alertId", primary_key=True)
    source_event_id: Mapped[str] = mapped_column("sourceEventId", primary_key=True)
    row_time: Mapped[datetime] = mapped_column("rowTime", UtcDateTimeTz(precision=6))
    seen_at: Mapped[datetime] = mapped_column("seenAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt", UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("AlertOccurrenceSeen_expiresAt_idx", "expiresAt"),
    )

class AlertDelivery(Base):
    __tablename__ = "AlertDelivery"

    id: Mapped[str] = mapped_column(primary_key=True)
    alert_event_id: Mapped[str] = mapped_column("alertEventId")
    event_at: Mapped[datetime] = mapped_column("eventAt", UtcDateTimeTz(precision=6), primary_key=True)
    notifier_id: Mapped[str] = mapped_column("notifierId")
    notifier: Mapped[dict[str, Any]] = mapped_column(JSONB)
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    claim_id: Mapped[Optional[str]] = mapped_column("claimId")
    claimed_at: Mapped[Optional[datetime]] = mapped_column("claimedAt", UtcDateTimeTz(precision=6))
    delivered_at: Mapped[Optional[datetime]] = mapped_column("deliveredAt", UtcDateTimeTz(precision=6))
    failure_reason: Mapped[Optional[str]] = mapped_column("failureReason")
    provider_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("providerMetadata", JSONB(none_as_null=True))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    first_attempt_at: Mapped[Optional[datetime]] = mapped_column("firstAttemptAt", UtcDateTimeTz(precision=6))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AlertDelivery_status_createdAt_idx", "status", "createdAt"),
        Index("AlertDelivery_status_claimedAt_idx", "status", "claimedAt"),
        Index("AlertDelivery_alertEventId_notifierId_eventAt_key", "alertEventId", "notifierId", "eventAt", unique=True),
    )

class PlaygroundConversation(Base):
    __tablename__ = "PlaygroundConversation"

    id: Mapped[str] = mapped_column(primary_key=True)
    chat_id: Mapped[str] = mapped_column("chatId")
    title: Mapped[str] = mapped_column(default="New conversation", server_default=text("'New conversation'"))
    agent_slug: Mapped[str] = mapped_column("agentSlug")
    run_id: Mapped[Optional[str]] = mapped_column("runId", ForeignKey("TaskRun.id", ondelete="SET NULL", onupdate="CASCADE", name="PlaygroundConversation_runId_fkey"))
    client_data: Mapped[Optional[dict[str, Any]]] = mapped_column("clientData", JSONB(none_as_null=True))
    messages: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_event_id: Mapped[Optional[str]] = mapped_column("lastEventId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="PlaygroundConversation_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="PlaygroundConversation_runtimeEnvironmentId_fkey"))
    user_id: Mapped[str] = mapped_column("userId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PlaygroundConversation_runtimeEnvironmentId_agentSlug_updat_idx", "runtimeEnvironmentId", "agentSlug", "updatedAt"),
        Index("PlaygroundConversation_userId_runtimeEnvironmentId_idx", "userId", "runtimeEnvironmentId"),
        Index("PlaygroundConversation_chatId_runtimeEnvironmentId_key", "chatId", "runtimeEnvironmentId", unique=True),
    )

    run: Mapped[Optional["TaskRun"]] = relationship(back_populates="playground_conversations")
    project: Mapped["Project"] = relationship(back_populates="playground_conversations")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="playground_conversations")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    type: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId")
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    organization_id: Mapped[str] = mapped_column("organizationId")
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    trigger_config: Mapped[dict[str, Any]] = mapped_column("triggerConfig", JSONB)
    is_test: Mapped[bool] = mapped_column("isTest", default=False, server_default=text("false"))
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    current_run_id: Mapped[Optional[str]] = mapped_column("currentRunId")
    current_run_version: Mapped[int] = mapped_column("currentRunVersion", default=0, server_default=text("0"))
    closed_at: Mapped[Optional[datetime]] = mapped_column("closedAt")
    closed_reason: Mapped[Optional[str]] = mapped_column("closedReason")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    stream_basin_name: Mapped[Optional[str]] = mapped_column("streamBasinName")
    chat_snapshot_storage_path: Mapped[Optional[str]] = mapped_column("chatSnapshotStoragePath")

    __table_args__ = (
        Index("Session_friendlyId_key", "friendlyId", unique=True),
        Index("Session_expiresAt_idx", "expiresAt"),
        Index("Session_currentRunId_idx", "currentRunId"),
        Index("Session_runtimeEnvironmentId_externalId_key", "runtimeEnvironmentId", "externalId", unique=True),
    )

    runs: Mapped[list["SessionRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="session")

class SessionRun(Base):
    __tablename__ = "SessionRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_id: Mapped[str] = mapped_column("sessionId", ForeignKey("Session.id", ondelete="CASCADE", onupdate="CASCADE", name="SessionRun_sessionId_fkey"))
    run_id: Mapped[str] = mapped_column("runId")
    reason: Mapped[str]
    triggered_at: Mapped[datetime] = mapped_column("triggeredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("SessionRun_runId_key", "runId", unique=True),
        Index("SessionRun_sessionId_idx", "sessionId"),
        Index("SessionRun_sessionId_triggeredAt_id_idx", "sessionId", "triggeredAt", "id"),
    )

    session: Mapped["Session"] = relationship(back_populates="runs")

class TaskRun(Base):
    __tablename__ = "TaskRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    number: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    engine: Mapped[str] = mapped_column(Enum("V1", "V2", name="RunEngineVersion"), default="V1", server_default=text("'V1'"))
    status: Mapped[str] = mapped_column(Enum("DELAYED", "PENDING", "PENDING_VERSION", "WAITING_FOR_DEPLOY", "DEQUEUED", "EXECUTING", "WAITING_TO_RESUME", "RETRYING_AFTER_FAILURE", "PAUSED", "CANCELED", "INTERRUPTED", "COMPLETED_SUCCESSFULLY", "COMPLETED_WITH_ERRORS", "SYSTEM_FAILURE", "CRASHED", "EXPIRED", "TIMED_OUT", name="TaskRunStatus"), default="PENDING", server_default=text("'PENDING'"))
    status_reason: Mapped[Optional[str]] = mapped_column("statusReason")
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    idempotency_key_expires_at: Mapped[Optional[datetime]] = mapped_column("idempotencyKeyExpiresAt")
    idempotency_key_options: Mapped[Optional[dict[str, Any]]] = mapped_column("idempotencyKeyOptions", JSONB(none_as_null=True))
    debounce: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    is_test: Mapped[bool] = mapped_column("isTest", default=False, server_default=text("false"))
    payload: Mapped[str]
    payload_type: Mapped[str] = mapped_column("payloadType", default="application/json", server_default=text("'application/json'"))
    context: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    trace_context: Mapped[Optional[dict[str, Any]]] = mapped_column("traceContext", JSONB(none_as_null=True))
    trace_id: Mapped[str] = mapped_column("traceId")
    span_id: Mapped[str] = mapped_column("spanId")
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRun_runtimeEnvironmentId_fkey"))
    environment_type: Mapped[Optional[str]] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRun_projectId_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    queue: Mapped[str]
    locked_queue_id: Mapped[Optional[str]] = mapped_column("lockedQueueId")
    worker_queue: Mapped[str] = mapped_column("masterQueue", default="main", server_default=text("'main'"))
    region: Mapped[Optional[str]]
    secondary_master_queue: Mapped[Optional[str]] = mapped_column("secondaryMasterQueue")
    attempt_number: Mapped[Optional[int]] = mapped_column("attemptNumber")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    run_tags: Mapped[list[str]] = mapped_column("runTags", ARRAY(Text), nullable=True)
    task_version: Mapped[Optional[str]] = mapped_column("taskVersion")
    sdk_version: Mapped[Optional[str]] = mapped_column("sdkVersion")
    cli_version: Mapped[Optional[str]] = mapped_column("cliVersion")
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    executed_at: Mapped[Optional[datetime]] = mapped_column("executedAt")
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    machine_preset: Mapped[Optional[str]] = mapped_column("machinePreset")
    usage_duration_ms: Mapped[int] = mapped_column("usageDurationMs", default=0, server_default=text("0"))
    cost_in_cents: Mapped[float] = mapped_column("costInCents", default=0, server_default=text("0"))
    base_cost_in_cents: Mapped[float] = mapped_column("baseCostInCents", default=0, server_default=text("0"))
    locked_at: Mapped[Optional[datetime]] = mapped_column("lockedAt")
    locked_by_id: Mapped[Optional[str]] = mapped_column("lockedById", ForeignKey("BackgroundWorkerTask.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRun_lockedById_fkey"))
    locked_to_version_id: Mapped[Optional[str]] = mapped_column("lockedToVersionId", ForeignKey("BackgroundWorker.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRun_lockedToVersionId_fkey"))
    priority_ms: Mapped[int] = mapped_column("priorityMs", default=0, server_default=text("0"))
    concurrency_key: Mapped[Optional[str]] = mapped_column("concurrencyKey")
    gates: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    delay_until: Mapped[Optional[datetime]] = mapped_column("delayUntil")
    queued_at: Mapped[Optional[datetime]] = mapped_column("queuedAt")
    ttl: Mapped[Optional[str]]
    expired_at: Mapped[Optional[datetime]] = mapped_column("expiredAt")
    max_attempts: Mapped[Optional[int]] = mapped_column("maxAttempts")
    locked_retry_config: Mapped[Optional[dict[str, Any]]] = mapped_column("lockedRetryConfig", JSONB(none_as_null=True))
    one_time_use_token: Mapped[Optional[str]] = mapped_column("oneTimeUseToken")
    task_event_store: Mapped[str] = mapped_column("taskEventStore", default="taskEvent", server_default=text("'taskEvent'"))
    queue_timestamp: Mapped[Optional[datetime]] = mapped_column("queueTimestamp")
    schedule_instance_id: Mapped[Optional[str]] = mapped_column("scheduleInstanceId")
    schedule_id: Mapped[Optional[str]] = mapped_column("scheduleId")
    bulk_action_group_ids: Mapped[list[str]] = mapped_column("bulkActionGroupIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    logs_deleted_at: Mapped[Optional[datetime]] = mapped_column("logsDeletedAt")
    replayed_from_task_run_friendly_id: Mapped[Optional[str]] = mapped_column("replayedFromTaskRunFriendlyId")
    root_task_run_id: Mapped[Optional[str]] = mapped_column("rootTaskRunId", ForeignKey("TaskRun.id", ondelete="SET NULL", onupdate="NO ACTION", name="TaskRun_rootTaskRunId_fkey"))
    parent_task_run_id: Mapped[Optional[str]] = mapped_column("parentTaskRunId", ForeignKey("TaskRun.id", ondelete="SET NULL", onupdate="NO ACTION", name="TaskRun_parentTaskRunId_fkey"))
    parent_task_run_attempt_id: Mapped[Optional[str]] = mapped_column("parentTaskRunAttemptId", ForeignKey("TaskRunAttempt.id", ondelete="SET NULL", onupdate="NO ACTION", name="TaskRun_parentTaskRunAttemptId_fkey"))
    batch_id: Mapped[Optional[str]] = mapped_column("batchId", ForeignKey("BatchTaskRun.id", ondelete="SET NULL", onupdate="NO ACTION", name="TaskRun_batchId_fkey"))
    resume_parent_on_completion: Mapped[bool] = mapped_column("resumeParentOnCompletion", default=False, server_default=text("false"))
    depth: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    parent_span_id: Mapped[Optional[str]] = mapped_column("parentSpanId")
    run_chain_state: Mapped[Optional[dict[str, Any]]] = mapped_column("runChainState", JSONB(none_as_null=True))
    seed_metadata: Mapped[Optional[str]] = mapped_column("seedMetadata")
    seed_metadata_type: Mapped[str] = mapped_column("seedMetadataType", default="application/json", server_default=text("'application/json'"))
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    metadata_type: Mapped[str] = mapped_column("metadataType", default="application/json", server_default=text("'application/json'"))
    metadata_version: Mapped[int] = mapped_column("metadataVersion", default=1, server_default=text("1"))
    annotations: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    is_warm_start: Mapped[Optional[bool]] = mapped_column("isWarmStart")
    output: Mapped[Optional[str]]
    output_type: Mapped[str] = mapped_column("outputType", default="application/json", server_default=text("'application/json'"))
    error: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    plan_type: Mapped[Optional[str]] = mapped_column("planType")
    max_duration_in_seconds: Mapped[Optional[int]] = mapped_column("maxDurationInSeconds")
    realtime_streams_version: Mapped[str] = mapped_column("realtimeStreamsVersion", default="v1", server_default=text("'v1'"))
    realtime_streams: Mapped[list[str]] = mapped_column("realtimeStreams", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    stream_basin_name: Mapped[Optional[str]] = mapped_column("streamBasinName")

    __table_args__ = (
        Index("TaskRun_friendlyId_key", "friendlyId", unique=True),
        Index("TaskRun_parentTaskRunId_idx", "parentTaskRunId"),
        Index("TaskRun_spanId_idx", "spanId"),
        Index("TaskRun_parentSpanId_idx", "parentSpanId"),
        Index("TaskRun_runTags_idx", "runTags"),
        Index("TaskRun_runtimeEnvironmentId_batchId_idx", "runtimeEnvironmentId", "batchId"),
        Index("TaskRun_runtimeEnvironmentId_createdAt_idx", "runtimeEnvironmentId", "createdAt"),
        Index("TaskRun_createdAt_idx", "createdAt"),
        Index("TaskRun_oneTimeUseToken_key", "oneTimeUseToken", unique=True),
        Index("TaskRun_runtimeEnvironmentId_taskIdentifier_idempotencyKey_key", "runtimeEnvironmentId", "taskIdentifier", "idempotencyKey", unique=True),
    )

    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_runs")
    project: Mapped["Project"] = relationship(back_populates="task_runs")
    locked_by: Mapped[Optional["BackgroundWorkerTask"]] = relationship(back_populates="runs")
    locked_to_version: Mapped[Optional["BackgroundWorker"]] = relationship(back_populates="locked_runs")
    root_task_run: Mapped[Optional["TaskRun"]] = relationship(remote_side=[id], foreign_keys=[root_task_run_id], back_populates="descendant_runs")
    parent_task_run: Mapped[Optional["TaskRun"]] = relationship(remote_side=[id], foreign_keys=[parent_task_run_id], back_populates="child_runs")
    parent_task_run_attempt: Mapped[Optional["TaskRunAttempt"]] = relationship(foreign_keys=[parent_task_run_attempt_id], back_populates="child_runs")
    batch: Mapped[Optional["BatchTaskRun"]] = relationship(back_populates="runs")
    attempts: Mapped[list["TaskRunAttempt"]] = relationship(foreign_keys="TaskRunAttempt.task_run_id", cascade="all, delete", passive_deletes=True, back_populates="task_run")
    checkpoints: Mapped[list["Checkpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="run")
    blocked_by_waitpoints: Mapped[list["TaskRunWaitpoint"]] = relationship(passive_deletes="all", back_populates="task_run")
    batch_items: Mapped[list["BatchTaskRunItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task_run")
    checkpoint_restore_event: Mapped[list["CheckpointRestoreEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="run")
    execution_snapshots: Mapped[list["TaskRunExecutionSnapshot"]] = relationship(passive_deletes="all", back_populates="run")
    source_bulk_action_items: Mapped[list["BulkActionItem"]] = relationship(foreign_keys="BulkActionItem.source_run_id", cascade="all, delete", passive_deletes=True, back_populates="source_run")
    destination_bulk_action_items: Mapped[list["BulkActionItem"]] = relationship(foreign_keys="BulkActionItem.destination_run_id", cascade="all, delete", passive_deletes=True, back_populates="destination_run")
    descendant_runs: Mapped[list["TaskRun"]] = relationship(foreign_keys="TaskRun.root_task_run_id", back_populates="root_task_run")
    child_runs: Mapped[list["TaskRun"]] = relationship(foreign_keys="TaskRun.parent_task_run_id", back_populates="parent_task_run")
    playground_conversations: Mapped[list["PlaygroundConversation"]] = relationship(back_populates="run")
    associated_waitpoint: Mapped[Optional["Waitpoint"]] = relationship(back_populates="completed_by_task_run")
    dependency: Mapped[Optional["TaskRunDependency"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task_run")
    connected_waitpoints: Mapped[list["Waitpoint"]] = relationship(secondary=waitpoint_run_connections, back_populates="connected_runs")

class TaskRunTemplate(Base):
    __tablename__ = "TaskRunTemplate"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_slug: Mapped[str] = mapped_column("taskSlug")
    trigger_source: Mapped[str] = mapped_column("triggerSource", Enum("STANDARD", "SCHEDULED", "AGENT", "WEBHOOK", name="TaskTriggerSource"))
    label: Mapped[str]
    payload: Mapped[Optional[str]]
    payload_type: Mapped[str] = mapped_column("payloadType", default="application/json", server_default=text("'application/json'"))
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    metadata_type: Mapped[str] = mapped_column("metadataType", default="application/json", server_default=text("'application/json'"))
    queue: Mapped[Optional[str]]
    concurrency_key: Mapped[Optional[str]] = mapped_column("concurrencyKey")
    ttl_seconds: Mapped[Optional[int]] = mapped_column("ttlSeconds")
    max_attempts: Mapped[Optional[int]] = mapped_column("maxAttempts")
    max_duration_seconds: Mapped[Optional[int]] = mapped_column("maxDurationSeconds")
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    machine_preset: Mapped[Optional[str]] = mapped_column("machinePreset")
    project_id: Mapped[str] = mapped_column("projectId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskRunTemplate_projectId_taskSlug_triggerSource_createdAt_idx", "projectId", "taskSlug", "triggerSource", "createdAt"),
    )

class TaskRunExecutionSnapshot(Base):
    __tablename__ = "TaskRunExecutionSnapshot"

    id: Mapped[str] = mapped_column(primary_key=True)
    engine: Mapped[str] = mapped_column(Enum("V1", "V2", name="RunEngineVersion"), default="V2", server_default=text("'V2'"))
    execution_status: Mapped[str] = mapped_column("executionStatus", Enum("RUN_CREATED", "DELAYED", "QUEUED", "QUEUED_EXECUTING", "PENDING_EXECUTING", "EXECUTING", "EXECUTING_WITH_WAITPOINTS", "SUSPENDED", "PENDING_CANCEL", "FINISHED", name="TaskRunExecutionStatus"))
    description: Mapped[str]
    is_valid: Mapped[bool] = mapped_column("isValid", default=True, server_default=text("true"))
    error: Mapped[Optional[str]]
    previous_snapshot_id: Mapped[Optional[str]] = mapped_column("previousSnapshotId")
    run_id: Mapped[str] = mapped_column("runId", ForeignKey("TaskRun.id", ondelete="RESTRICT", onupdate="CASCADE", name="TaskRunExecutionSnapshot_runId_fkey"))
    run_status: Mapped[str] = mapped_column("runStatus", Enum("DELAYED", "PENDING", "PENDING_VERSION", "WAITING_FOR_DEPLOY", "DEQUEUED", "EXECUTING", "WAITING_TO_RESUME", "RETRYING_AFTER_FAILURE", "PAUSED", "CANCELED", "INTERRUPTED", "COMPLETED_SUCCESSFULLY", "COMPLETED_WITH_ERRORS", "SYSTEM_FAILURE", "CRASHED", "EXPIRED", "TIMED_OUT", name="TaskRunStatus"))
    batch_id: Mapped[Optional[str]] = mapped_column("batchId")
    attempt_number: Mapped[Optional[int]] = mapped_column("attemptNumber")
    environment_id: Mapped[str] = mapped_column("environmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    project_id: Mapped[str] = mapped_column("projectId")
    organization_id: Mapped[str] = mapped_column("organizationId")
    completed_waitpoint_order: Mapped[list[str]] = mapped_column("completedWaitpointOrder", ARRAY(Text), nullable=True)
    checkpoint_id: Mapped[Optional[str]] = mapped_column("checkpointId", ForeignKey("TaskRunCheckpoint.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRunExecutionSnapshot_checkpointId_fkey"))
    worker_id: Mapped[Optional[str]] = mapped_column("workerId")
    runner_id: Mapped[Optional[str]] = mapped_column("runnerId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    last_heartbeat_at: Mapped[Optional[datetime]] = mapped_column("lastHeartbeatAt")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("TaskRunExecutionSnapshot_runId_isValid_createdAt_idx", "runId", "isValid", "createdAt"),
    )

    run: Mapped["TaskRun"] = relationship(back_populates="execution_snapshots")
    checkpoint: Mapped[Optional["TaskRunCheckpoint"]] = relationship(back_populates="execution_snapshot")
    completed_waitpoints: Mapped[list["Waitpoint"]] = relationship(secondary=completed_waitpoints, back_populates="completed_execution_snapshots")

class TaskRunCheckpoint(Base):
    __tablename__ = "TaskRunCheckpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    type: Mapped[str] = mapped_column(Enum("DOCKER", "KUBERNETES", "COMPUTE", name="TaskRunCheckpointType"))
    location: Mapped[str]
    image_ref: Mapped[Optional[str]] = mapped_column("imageRef")
    reason: Mapped[Optional[str]]
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunCheckpoint_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunCheckpoint_runtimeEnvironmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskRunCheckpoint_friendlyId_key", "friendlyId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="task_run_checkpoints")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_run_checkpoints")
    execution_snapshot: Mapped[list["TaskRunExecutionSnapshot"]] = relationship(back_populates="checkpoint")

class Waitpoint(Base):
    __tablename__ = "Waitpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    type: Mapped[str] = mapped_column(Enum("RUN", "DATETIME", "MANUAL", "BATCH", name="WaitpointType"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", name="WaitpointStatus"), default="PENDING", server_default=text("'PENDING'"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    idempotency_key: Mapped[str] = mapped_column("idempotencyKey")
    user_provided_idempotency_key: Mapped[bool] = mapped_column("userProvidedIdempotencyKey")
    idempotency_key_expires_at: Mapped[Optional[datetime]] = mapped_column("idempotencyKeyExpiresAt")
    inactive_idempotency_key: Mapped[Optional[str]] = mapped_column("inactiveIdempotencyKey")
    completed_by_task_run_id: Mapped[Optional[str]] = mapped_column("completedByTaskRunId", ForeignKey("TaskRun.id", ondelete="SET NULL", onupdate="CASCADE", name="Waitpoint_completedByTaskRunId_fkey"))
    completed_after: Mapped[Optional[datetime]] = mapped_column("completedAfter")
    completed_by_batch_id: Mapped[Optional[str]] = mapped_column("completedByBatchId", ForeignKey("BatchTaskRun.id", ondelete="SET NULL", onupdate="CASCADE", name="Waitpoint_completedByBatchId_fkey"))
    output: Mapped[Optional[str]]
    output_type: Mapped[str] = mapped_column("outputType", default="application/json", server_default=text("'application/json'"))
    output_is_error: Mapped[bool] = mapped_column("outputIsError", default=False, server_default=text("false"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Waitpoint_projectId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="Waitpoint_environmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)

    __table_args__ = (
        Index("Waitpoint_friendlyId_key", "friendlyId", unique=True),
        Index("Waitpoint_completedByTaskRunId_key", "completedByTaskRunId", unique=True),
        Index("Waitpoint_completedByBatchId_idx", "completedByBatchId"),
        Index("Waitpoint_environmentId_type_createdAt_idx", "environmentId", "type", "createdAt"),
        Index("Waitpoint_environmentId_type_status_idx", "environmentId", "type", "status"),
        Index("Waitpoint_environmentId_type_id_idx", "environmentId", "type", "id"),
        Index("Waitpoint_environmentId_idempotencyKey_key", "environmentId", "idempotencyKey", unique=True),
    )

    completed_by_task_run: Mapped[Optional["TaskRun"]] = relationship(back_populates="associated_waitpoint")
    completed_by_batch: Mapped[Optional["BatchTaskRun"]] = relationship(back_populates="waitpoints")
    project: Mapped["Project"] = relationship(back_populates="waitpoints")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="waitpoints")
    blocking_task_runs: Mapped[list["TaskRunWaitpoint"]] = relationship(passive_deletes="all", back_populates="waitpoint")
    connected_runs: Mapped[list["TaskRun"]] = relationship(secondary=waitpoint_run_connections, back_populates="connected_waitpoints")
    completed_execution_snapshots: Mapped[list["TaskRunExecutionSnapshot"]] = relationship(secondary=completed_waitpoints, back_populates="completed_waitpoints")

class TaskRunWaitpoint(Base):
    __tablename__ = "TaskRunWaitpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_run_id: Mapped[str] = mapped_column("taskRunId", ForeignKey("TaskRun.id", ondelete="RESTRICT", onupdate="CASCADE", name="TaskRunWaitpoint_taskRunId_fkey"))
    waitpoint_id: Mapped[str] = mapped_column("waitpointId", ForeignKey("Waitpoint.id", ondelete="RESTRICT", onupdate="CASCADE", name="TaskRunWaitpoint_waitpointId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunWaitpoint_projectId_fkey"))
    span_id_to_complete: Mapped[Optional[str]] = mapped_column("spanIdToComplete")
    batch_id: Mapped[Optional[str]] = mapped_column("batchId", ForeignKey("BatchTaskRun.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRunWaitpoint_batchId_fkey"))
    batch_index: Mapped[Optional[int]] = mapped_column("batchIndex")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskRunWaitpoint_taskRunId_idx", "taskRunId"),
        Index("TaskRunWaitpoint_waitpointId_idx", "waitpointId"),
        Index("TaskRunWaitpoint_taskRunId_waitpointId_batchIndex_key", "taskRunId", "waitpointId", "batchIndex", unique=True),
    )

    task_run: Mapped["TaskRun"] = relationship(back_populates="blocked_by_waitpoints")
    waitpoint: Mapped["Waitpoint"] = relationship(back_populates="blocking_task_runs")
    project: Mapped["Project"] = relationship(back_populates="task_run_waitpoints")
    batch: Mapped[Optional["BatchTaskRun"]] = relationship(back_populates="runs_blocked")

class WaitpointTag(Base):
    __tablename__ = "WaitpointTag"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="WaitpointTag_environmentId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="WaitpointTag_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("WaitpointTag_environmentId_name_key", "environmentId", "name", unique=True),
    )

    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="waitpoint_tags")
    project: Mapped["Project"] = relationship(back_populates="waitpoint_tags")

class FeatureFlag(Base):
    __tablename__ = "FeatureFlag"

    id: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str]
    value: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("FeatureFlag_key_key", "key", unique=True),
    )

class WorkerInstance(Base):
    __tablename__ = "WorkerInstance"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    resource_identifier: Mapped[str] = mapped_column("resourceIdentifier")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    worker_group_id: Mapped[str] = mapped_column("workerGroupId", ForeignKey("WorkerInstanceGroup.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkerInstance_workerGroupId_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstance_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstance_projectId_fkey"))
    environment_id: Mapped[Optional[str]] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstance_environmentId_fkey"))
    deployment_id: Mapped[Optional[str]] = mapped_column("deploymentId", ForeignKey("WorkerDeployment.id", ondelete="SET NULL", onupdate="CASCADE", name="WorkerInstance_deploymentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    last_dequeue_at: Mapped[Optional[datetime]] = mapped_column("lastDequeueAt")
    last_heartbeat_at: Mapped[Optional[datetime]] = mapped_column("lastHeartbeatAt")

    __table_args__ = (
        Index("WorkerInstance_workerGroupId_resourceIdentifier_key", "workerGroupId", "resourceIdentifier", unique=True),
    )

    worker_group: Mapped["WorkerInstanceGroup"] = relationship(back_populates="workers")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="worker_instances")
    project: Mapped[Optional["Project"]] = relationship(back_populates="workers")
    environment: Mapped[Optional["RuntimeEnvironment"]] = relationship(back_populates="worker_instances")
    deployment: Mapped[Optional["WorkerDeployment"]] = relationship(back_populates="worker_instance")

class WorkerInstanceGroup(Base):
    __tablename__ = "WorkerInstanceGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("MANAGED", "UNMANAGED", name="WorkerInstanceGroupType"))
    name: Mapped[str]
    master_queue: Mapped[str] = mapped_column("masterQueue")
    description: Mapped[Optional[str]]
    hidden: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    token_id: Mapped[str] = mapped_column("tokenId", ForeignKey("WorkerGroupToken.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstanceGroup_tokenId_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstanceGroup_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerInstanceGroup_projectId_fkey"))
    cloud_provider: Mapped[Optional[str]] = mapped_column("cloudProvider")
    location: Mapped[Optional[str]]
    static_ips: Mapped[Optional[str]] = mapped_column("staticIPs")
    workload_type: Mapped[str] = mapped_column("workloadType", Enum("CONTAINER", "MICROVM", name="WorkloadType"), default="CONTAINER", server_default=text("'CONTAINER'"))
    region: Mapped[Optional[str]]
    enable_fast_path: Mapped[bool] = mapped_column("enableFastPath", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WorkerInstanceGroup_masterQueue_key", "masterQueue", unique=True),
        Index("WorkerInstanceGroup_tokenId_key", "tokenId", unique=True),
    )

    token: Mapped["WorkerGroupToken"] = relationship(back_populates="worker_group")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="worker_groups")
    project: Mapped[Optional["Project"]] = relationship(foreign_keys=[project_id], back_populates="worker_groups")
    workers: Mapped[list["WorkerInstance"]] = relationship(passive_deletes="all", back_populates="worker_group")
    background_workers: Mapped[list["BackgroundWorker"]] = relationship(back_populates="worker_group")
    default_for_projects: Mapped[list["Project"]] = relationship(foreign_keys="Project.default_worker_group_id", back_populates="default_worker_group")

class WorkerGroupToken(Base):
    __tablename__ = "WorkerGroupToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token_hash: Mapped[str] = mapped_column("tokenHash")
    previous_token_hash: Mapped[Optional[str]] = mapped_column("previousTokenHash")
    previous_token_valid_until: Mapped[Optional[datetime]] = mapped_column("previousTokenValidUntil")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WorkerGroupToken_tokenHash_key", "tokenHash", unique=True),
        Index("WorkerGroupToken_previousTokenHash_key", "previousTokenHash", unique=True),
    )

    worker_group: Mapped[Optional["WorkerInstanceGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="token")

class TaskRunTag(Base):
    __tablename__ = "TaskRunTag"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunTag_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("TaskRunTag_friendlyId_key", "friendlyId", unique=True),
        Index("TaskRunTag_name_id_idx", "name", "id"),
        Index("TaskRunTag_projectId_name_key", "projectId", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="run_tags")

class TaskRunDependency(Base):
    __tablename__ = "TaskRunDependency"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_run_id: Mapped[str] = mapped_column("taskRunId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunDependency_taskRunId_fkey"))
    checkpoint_event_id: Mapped[Optional[str]] = mapped_column("checkpointEventId", ForeignKey("CheckpointRestoreEvent.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunDependency_checkpointEventId_fkey"))
    dependent_attempt_id: Mapped[Optional[str]] = mapped_column("dependentAttemptId", ForeignKey("TaskRunAttempt.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRunDependency_dependentAttemptId_fkey"))
    dependent_batch_run_id: Mapped[Optional[str]] = mapped_column("dependentBatchRunId", ForeignKey("BatchTaskRun.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskRunDependency_dependentBatchRunId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    resumed_at: Mapped[Optional[datetime]] = mapped_column("resumedAt")

    __table_args__ = (
        Index("TaskRunDependency_taskRunId_key", "taskRunId", unique=True),
        Index("TaskRunDependency_checkpointEventId_key", "checkpointEventId", unique=True),
        Index("TaskRunDependency_dependentAttemptId_idx", "dependentAttemptId"),
        Index("TaskRunDependency_dependentBatchRunId_idx", "dependentBatchRunId"),
    )

    task_run: Mapped["TaskRun"] = relationship(back_populates="dependency")
    checkpoint_event: Mapped[Optional["CheckpointRestoreEvent"]] = relationship(back_populates="task_run_dependency")
    dependent_attempt: Mapped[Optional["TaskRunAttempt"]] = relationship(back_populates="dependencies")
    dependent_batch_run: Mapped[Optional["BatchTaskRun"]] = relationship(back_populates="run_dependencies")

class TaskRunCounter(Base):
    __tablename__ = "TaskRunCounter"

    task_identifier: Mapped[str] = mapped_column("taskIdentifier", primary_key=True)
    last_number: Mapped[int] = mapped_column("lastNumber", default=0, server_default=text("0"))

class TaskRunNumberCounter(Base):
    __tablename__ = "TaskRunNumberCounter"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunNumberCounter_environmentId_fkey"))
    last_number: Mapped[int] = mapped_column("lastNumber", default=0, server_default=text("0"))

    __table_args__ = (
        Index("TaskRunNumberCounter_taskIdentifier_environmentId_key", "taskIdentifier", "environmentId", unique=True),
    )

    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_run_number_counter")

class TaskRunAttempt(Base):
    __tablename__ = "TaskRunAttempt"

    id: Mapped[str] = mapped_column(primary_key=True)
    number: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    task_run_id: Mapped[str] = mapped_column("taskRunId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunAttempt_taskRunId_fkey"))
    background_worker_id: Mapped[str] = mapped_column("backgroundWorkerId", ForeignKey("BackgroundWorker.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunAttempt_backgroundWorkerId_fkey"))
    background_worker_task_id: Mapped[str] = mapped_column("backgroundWorkerTaskId", ForeignKey("BackgroundWorkerTask.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunAttempt_backgroundWorkerTaskId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunAttempt_runtimeEnvironmentId_fkey"))
    queue_id: Mapped[str] = mapped_column("queueId", ForeignKey("TaskQueue.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskRunAttempt_queueId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "EXECUTING", "PAUSED", "FAILED", "CANCELED", "COMPLETED", name="TaskRunAttemptStatus"), default="PENDING", server_default=text("'PENDING'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    usage_duration_ms: Mapped[int] = mapped_column("usageDurationMs", default=0, server_default=text("0"))
    error: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output: Mapped[Optional[str]]
    output_type: Mapped[str] = mapped_column("outputType", default="application/json", server_default=text("'application/json'"))

    __table_args__ = (
        Index("TaskRunAttempt_friendlyId_key", "friendlyId", unique=True),
        Index("TaskRunAttempt_taskRunId_idx", "taskRunId"),
        Index("TaskRunAttempt_taskRunId_number_key", "taskRunId", "number", unique=True),
    )

    task_run: Mapped["TaskRun"] = relationship(foreign_keys=[task_run_id], back_populates="attempts")
    background_worker: Mapped["BackgroundWorker"] = relationship(back_populates="attempts")
    background_worker_task: Mapped["BackgroundWorkerTask"] = relationship(back_populates="attempts")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_run_attempts")
    queue: Mapped["TaskQueue"] = relationship(back_populates="attempts")
    dependencies: Mapped[list["TaskRunDependency"]] = relationship(back_populates="dependent_attempt")
    batch_dependencies: Mapped[list["BatchTaskRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dependent_task_attempt")
    checkpoints: Mapped[list["Checkpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attempt")
    batch_task_run_items: Mapped[list["BatchTaskRunItem"]] = relationship(back_populates="task_run_attempt")
    checkpoint_restore_event: Mapped[list["CheckpointRestoreEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attempt")
    child_runs: Mapped[list["TaskRun"]] = relationship(foreign_keys="TaskRun.parent_task_run_attempt_id", back_populates="parent_task_run_attempt")

class TaskEvent(Base):
    __tablename__ = "TaskEvent"

    id: Mapped[str] = mapped_column(primary_key=True)
    message: Mapped[str]
    trace_id: Mapped[str] = mapped_column("traceId")
    span_id: Mapped[str] = mapped_column("spanId")
    parent_id: Mapped[Optional[str]] = mapped_column("parentId")
    tracestate: Mapped[Optional[str]]
    is_error: Mapped[bool] = mapped_column("isError", default=False, server_default=text("false"))
    is_partial: Mapped[bool] = mapped_column("isPartial", default=False, server_default=text("false"))
    is_cancelled: Mapped[bool] = mapped_column("isCancelled", default=False, server_default=text("false"))
    is_debug: Mapped[bool] = mapped_column("isDebug", default=False, server_default=text("false"))
    service_name: Mapped[str] = mapped_column("serviceName")
    service_namespace: Mapped[str] = mapped_column("serviceNamespace")
    level: Mapped[str] = mapped_column(Enum("TRACE", "DEBUG", "LOG", "INFO", "WARN", "ERROR", name="TaskEventLevel"), default="TRACE", server_default=text("'TRACE'"))
    kind: Mapped[str] = mapped_column(Enum("UNSPECIFIED", "INTERNAL", "SERVER", "CLIENT", "PRODUCER", "CONSUMER", "UNRECOGNIZED", "LOG", name="TaskEventKind"), default="INTERNAL", server_default=text("'INTERNAL'"))
    status: Mapped[str] = mapped_column(Enum("UNSET", "OK", "ERROR", "UNRECOGNIZED", name="TaskEventStatus"), default="UNSET", server_default=text("'UNSET'"))
    links: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    events: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    start_time: Mapped[int] = mapped_column("startTime", BigInteger)
    duration: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    attempt_id: Mapped[Optional[str]] = mapped_column("attemptId")
    attempt_number: Mapped[Optional[int]] = mapped_column("attemptNumber")
    environment_id: Mapped[str] = mapped_column("environmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    project_ref: Mapped[str] = mapped_column("projectRef")
    run_id: Mapped[str] = mapped_column("runId")
    run_is_test: Mapped[bool] = mapped_column("runIsTest", default=False, server_default=text("false"))
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    task_slug: Mapped[str] = mapped_column("taskSlug")
    task_path: Mapped[Optional[str]] = mapped_column("taskPath")
    task_export_name: Mapped[Optional[str]] = mapped_column("taskExportName")
    worker_id: Mapped[Optional[str]] = mapped_column("workerId")
    worker_version: Mapped[Optional[str]] = mapped_column("workerVersion")
    queue_id: Mapped[Optional[str]] = mapped_column("queueId")
    queue_name: Mapped[Optional[str]] = mapped_column("queueName")
    batch_id: Mapped[Optional[str]] = mapped_column("batchId")
    properties: Mapped[dict[str, Any]] = mapped_column(JSONB)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    style: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output_type: Mapped[Optional[str]] = mapped_column("outputType")
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    payload_type: Mapped[Optional[str]] = mapped_column("payloadType")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    usage_duration_ms: Mapped[int] = mapped_column("usageDurationMs", default=0, server_default=text("0"))
    usage_cost_in_cents: Mapped[float] = mapped_column("usageCostInCents", default=0, server_default=text("0"))
    machine_preset: Mapped[Optional[str]] = mapped_column("machinePreset")
    machine_preset_cpu: Mapped[Optional[float]] = mapped_column("machinePresetCpu")
    machine_preset_memory: Mapped[Optional[float]] = mapped_column("machinePresetMemory")
    machine_preset_cents_per_ms: Mapped[Optional[float]] = mapped_column("machinePresetCentsPerMs")

    __table_args__ = (
        Index("TaskEvent_traceId_idx", "traceId"),
        Index("TaskEvent_spanId_idx", "spanId"),
        Index("TaskEvent_runId_idx", "runId"),
    )

class TaskQueue(Base):
    __tablename__ = "TaskQueue"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("VIRTUAL", "NAMED", name="TaskQueueType"), default="VIRTUAL", server_default=text("'VIRTUAL'"))
    version: Mapped[str] = mapped_column(Enum("V1", "V2", name="TaskQueueVersion"), default="V1", server_default=text("'V1'"))
    orderable_name: Mapped[Optional[str]] = mapped_column("orderableName")
    concurrency_version: Mapped[str] = mapped_column("concurrencyVersion", Enum("V1", "V2", name="TaskQueueConcurrencyVersion"), default="V1", server_default=text("'V1'"))
    role: Mapped[str] = mapped_column(Enum("QUEUE", "LIMIT", name="TaskQueueRole"), default="QUEUE", server_default=text("'QUEUE'"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskQueue_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskQueue_runtimeEnvironmentId_fkey"))
    concurrency_limit: Mapped[Optional[int]] = mapped_column("concurrencyLimit")
    concurrency_limit_overridden_at: Mapped[Optional[datetime]] = mapped_column("concurrencyLimitOverriddenAt")
    concurrency_limit_overridden_by: Mapped[Optional[str]] = mapped_column("concurrencyLimitOverriddenBy")
    concurrency_limit_base: Mapped[Optional[int]] = mapped_column("concurrencyLimitBase")
    concurrency_limit_override_percent: Mapped[Optional[DecimalType]] = mapped_column("concurrencyLimitOverridePercent", Numeric(precision=5, scale=2))
    total_concurrency_limit: Mapped[Optional[int]] = mapped_column("totalConcurrencyLimit")
    total_concurrency_limit_overridden_at: Mapped[Optional[datetime]] = mapped_column("totalConcurrencyLimitOverriddenAt")
    total_concurrency_limit_overridden_by: Mapped[Optional[str]] = mapped_column("totalConcurrencyLimitOverriddenBy")
    total_concurrency_limit_base: Mapped[Optional[int]] = mapped_column("totalConcurrencyLimitBase")
    rate_limit: Mapped[Optional[dict[str, Any]]] = mapped_column("rateLimit", JSONB(none_as_null=True))
    paused: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskQueue_friendlyId_key", "friendlyId", unique=True),
        Index("TaskQueue_runtimeEnvironmentId_name_key", "runtimeEnvironmentId", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="task_queues")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_queues")
    attempts: Mapped[list["TaskRunAttempt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="queue")
    tasks: Mapped[list["BackgroundWorkerTask"]] = relationship(back_populates="queue")
    workers: Mapped[list["BackgroundWorker"]] = relationship(secondary=background_worker_to_task_queue, back_populates="queues")

class BatchTaskRun(Base):
    __tablename__ = "BatchTaskRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    idempotency_key_expires_at: Mapped[Optional[datetime]] = mapped_column("idempotencyKeyExpiresAt")
    status: Mapped[str] = mapped_column(Enum("PENDING", "PROCESSING", "COMPLETED", "PARTIAL_FAILED", "ABORTED", name="BatchTaskRunStatus"), default="PENDING", server_default=text("'PENDING'"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    run_ids: Mapped[list[str]] = mapped_column("runIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    run_count: Mapped[int] = mapped_column("runCount", default=0, server_default=text("0"))
    payload: Mapped[Optional[str]]
    payload_type: Mapped[str] = mapped_column("payloadType", default="application/json", server_default=text("'application/json'"))
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    batch_version: Mapped[str] = mapped_column("batchVersion", default="v1", server_default=text("'v1'"))
    sealed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    sealed_at: Mapped[Optional[datetime]] = mapped_column("sealedAt")
    expected_count: Mapped[int] = mapped_column("expectedCount", default=0, server_default=text("0"))
    completed_count: Mapped[int] = mapped_column("completedCount", default=0, server_default=text("0"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    resumed_at: Mapped[Optional[datetime]] = mapped_column("resumedAt")
    processing_jobs_count: Mapped[int] = mapped_column("processingJobsCount", default=0, server_default=text("0"))
    processing_jobs_expected_count: Mapped[int] = mapped_column("processingJobsExpectedCount", default=0, server_default=text("0"))
    one_time_use_token: Mapped[Optional[str]] = mapped_column("oneTimeUseToken")
    processing_started_at: Mapped[Optional[datetime]] = mapped_column("processingStartedAt")
    processing_completed_at: Mapped[Optional[datetime]] = mapped_column("processingCompletedAt")
    successful_run_count: Mapped[Optional[int]] = mapped_column("successfulRunCount")
    failed_run_count: Mapped[Optional[int]] = mapped_column("failedRunCount")
    task_identifier: Mapped[Optional[str]] = mapped_column("taskIdentifier")
    checkpoint_event_id: Mapped[Optional[str]] = mapped_column("checkpointEventId", ForeignKey("CheckpointRestoreEvent.id", ondelete="CASCADE", onupdate="CASCADE", name="BatchTaskRun_checkpointEventId_fkey"))
    dependent_task_attempt_id: Mapped[Optional[str]] = mapped_column("dependentTaskAttemptId", ForeignKey("TaskRunAttempt.id", ondelete="CASCADE", onupdate="CASCADE", name="BatchTaskRun_dependentTaskAttemptId_fkey"))

    __table_args__ = (
        Index("BatchTaskRun_friendlyId_key", "friendlyId", unique=True),
        Index("BatchTaskRun_checkpointEventId_key", "checkpointEventId", unique=True),
        Index("BatchTaskRun_dependentTaskAttemptId_idx", "dependentTaskAttemptId"),
        Index("BatchTaskRun_runtimeEnvironmentId_id_idx", "runtimeEnvironmentId", "id"),
        Index("BatchTaskRun_runtimeEnvironmentId_createdAt_id_idx", "runtimeEnvironmentId", "createdAt", "id"),
        Index("BatchTaskRun_oneTimeUseToken_key", "oneTimeUseToken", unique=True),
        Index("BatchTaskRun_runtimeEnvironmentId_idempotencyKey_key", "runtimeEnvironmentId", "idempotencyKey", unique=True),
    )

    checkpoint_event: Mapped[Optional["CheckpointRestoreEvent"]] = relationship(back_populates="batch_task_run_dependency")
    dependent_task_attempt: Mapped[Optional["TaskRunAttempt"]] = relationship(back_populates="batch_dependencies")
    runs: Mapped[list["TaskRun"]] = relationship(back_populates="batch")
    runs_blocked: Mapped[list["TaskRunWaitpoint"]] = relationship(back_populates="batch")
    waitpoints: Mapped[list["Waitpoint"]] = relationship(back_populates="completed_by_batch")
    errors: Mapped[list["BatchTaskRunError"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="batch_task_run")
    items: Mapped[list["BatchTaskRunItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="batch_task_run")
    run_dependencies: Mapped[list["TaskRunDependency"]] = relationship(back_populates="dependent_batch_run")

class BatchTaskRunItem(Base):
    __tablename__ = "BatchTaskRunItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str] = mapped_column(Enum("PENDING", "FAILED", "CANCELED", "COMPLETED", name="BatchTaskRunItemStatus"), default="PENDING", server_default=text("'PENDING'"))
    batch_task_run_id: Mapped[str] = mapped_column("batchTaskRunId", ForeignKey("BatchTaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="BatchTaskRunItem_batchTaskRunId_fkey"))
    task_run_id: Mapped[str] = mapped_column("taskRunId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="BatchTaskRunItem_taskRunId_fkey"))
    task_run_attempt_id: Mapped[Optional[str]] = mapped_column("taskRunAttemptId", ForeignKey("TaskRunAttempt.id", ondelete="SET NULL", onupdate="CASCADE", name="BatchTaskRunItem_taskRunAttemptId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")

    __table_args__ = (
        Index("idx_batchtaskrunitem_taskrunattempt", "taskRunAttemptId"),
        Index("idx_batchtaskrunitem_taskrun", "taskRunId"),
        Index("BatchTaskRunItem_batchTaskRunId_taskRunId_key", "batchTaskRunId", "taskRunId", unique=True),
    )

    batch_task_run: Mapped["BatchTaskRun"] = relationship(back_populates="items")
    task_run: Mapped["TaskRun"] = relationship(back_populates="batch_items")
    task_run_attempt: Mapped[Optional["TaskRunAttempt"]] = relationship(back_populates="batch_task_run_items")

class BatchTaskRunError(Base):
    __tablename__ = "BatchTaskRunError"

    id: Mapped[str] = mapped_column(primary_key=True)
    batch_task_run_id: Mapped[str] = mapped_column("batchTaskRunId", ForeignKey("BatchTaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="BatchTaskRunError_batchTaskRunId_fkey"))
    index: Mapped[int]
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    payload: Mapped[Optional[str]]
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[str]
    error_code: Mapped[Optional[str]] = mapped_column("errorCode")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("BatchTaskRunError_batchTaskRunId_idx", "batchTaskRunId"),
        Index("BatchTaskRunError_batchTaskRunId_index_key", "batchTaskRunId", "index", unique=True),
    )

    batch_task_run: Mapped["BatchTaskRun"] = relationship(back_populates="errors")

class EnvironmentVariable(Base):
    __tablename__ = "EnvironmentVariable"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    key: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvironmentVariable_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("EnvironmentVariable_friendlyId_key", "friendlyId", unique=True),
        Index("EnvironmentVariable_projectId_key_key", "projectId", "key", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="environment_variables")
    values: Mapped[list["EnvironmentVariableValue"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="variable")

class EnvironmentVariableValue(Base):
    __tablename__ = "EnvironmentVariableValue"

    id: Mapped[str] = mapped_column(primary_key=True)
    value_reference_id: Mapped[Optional[str]] = mapped_column("valueReferenceId", ForeignKey("SecretReference.id", ondelete="SET NULL", onupdate="CASCADE", name="EnvironmentVariableValue_valueReferenceId_fkey"))
    variable_id: Mapped[str] = mapped_column("variableId", ForeignKey("EnvironmentVariable.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvironmentVariableValue_variableId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="EnvironmentVariableValue_environmentId_fkey"))
    is_secret: Mapped[bool] = mapped_column("isSecret", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    version: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    last_updated_by: Mapped[Optional[dict[str, Any]]] = mapped_column("lastUpdatedBy", JSONB(none_as_null=True))

    __table_args__ = (
        Index("EnvironmentVariableValue_environmentId_idx", "environmentId"),
        Index("EnvironmentVariableValue_valueReferenceId_idx", "valueReferenceId"),
        Index("EnvironmentVariableValue_environmentId_updatedAt_variableId_idx", "environmentId", "updatedAt", "variableId"),
        Index("EnvironmentVariableValue_variableId_environmentId_key", "variableId", "environmentId", unique=True),
    )

    value_reference: Mapped[Optional["SecretReference"]] = relationship(back_populates="environment_variable_values")
    variable: Mapped["EnvironmentVariable"] = relationship(back_populates="values")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="environment_variable_values")

class Checkpoint(Base):
    __tablename__ = "Checkpoint"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    type: Mapped[str] = mapped_column(Enum("DOCKER", "KUBERNETES", name="CheckpointType"))
    location: Mapped[str]
    image_ref: Mapped[str] = mapped_column("imageRef")
    reason: Mapped[Optional[str]]
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    run_id: Mapped[str] = mapped_column("runId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="Checkpoint_runId_fkey"))
    attempt_id: Mapped[str] = mapped_column("attemptId", ForeignKey("TaskRunAttempt.id", ondelete="CASCADE", onupdate="CASCADE", name="Checkpoint_attemptId_fkey"))
    attempt_number: Mapped[Optional[int]] = mapped_column("attemptNumber")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Checkpoint_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="Checkpoint_runtimeEnvironmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Checkpoint_friendlyId_key", "friendlyId", unique=True),
        Index("Checkpoint_attemptId_idx", "attemptId"),
        Index("Checkpoint_runId_idx", "runId"),
    )

    run: Mapped["TaskRun"] = relationship(back_populates="checkpoints")
    attempt: Mapped["TaskRunAttempt"] = relationship(back_populates="checkpoints")
    project: Mapped["Project"] = relationship(back_populates="checkpoints")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="checkpoints")
    events: Mapped[list["CheckpointRestoreEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="checkpoint")

class CheckpointRestoreEvent(Base):
    __tablename__ = "CheckpointRestoreEvent"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("CHECKPOINT", "RESTORE", name="CheckpointRestoreEventType"))
    reason: Mapped[Optional[str]]
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    checkpoint_id: Mapped[str] = mapped_column("checkpointId", ForeignKey("Checkpoint.id", ondelete="CASCADE", onupdate="CASCADE", name="CheckpointRestoreEvent_checkpointId_fkey"))
    run_id: Mapped[str] = mapped_column("runId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="CheckpointRestoreEvent_runId_fkey"))
    attempt_id: Mapped[str] = mapped_column("attemptId", ForeignKey("TaskRunAttempt.id", ondelete="CASCADE", onupdate="CASCADE", name="CheckpointRestoreEvent_attemptId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="CheckpointRestoreEvent_projectId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="CheckpointRestoreEvent_runtimeEnvironmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("CheckpointRestoreEvent_checkpointId_idx", "checkpointId"),
        Index("CheckpointRestoreEvent_runId_idx", "runId"),
    )

    checkpoint: Mapped["Checkpoint"] = relationship(back_populates="events")
    run: Mapped["TaskRun"] = relationship(back_populates="checkpoint_restore_event")
    attempt: Mapped["TaskRunAttempt"] = relationship(back_populates="checkpoint_restore_event")
    project: Mapped["Project"] = relationship(back_populates="checkpoint_restore_event")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="checkpoint_restore_event")
    task_run_dependency: Mapped[Optional["TaskRunDependency"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="checkpoint_event")
    batch_task_run_dependency: Mapped[Optional["BatchTaskRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="checkpoint_event")

class WorkerDeployment(Base):
    __tablename__ = "WorkerDeployment"

    id: Mapped[str] = mapped_column(primary_key=True)
    content_hash: Mapped[str] = mapped_column("contentHash")
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    short_code: Mapped[str] = mapped_column("shortCode")
    version: Mapped[str]
    runtime: Mapped[Optional[str]]
    runtime_version: Mapped[Optional[str]] = mapped_column("runtimeVersion")
    cli_version: Mapped[Optional[str]] = mapped_column("cliVersion")
    image_reference: Mapped[Optional[str]] = mapped_column("imageReference")
    image_platform: Mapped[str] = mapped_column("imagePlatform", default="linux/amd64", server_default=text("'linux/amd64'"))
    template_key: Mapped[Optional[str]] = mapped_column("templateKey")
    index_object_key: Mapped[Optional[str]] = mapped_column("indexObjectKey")
    index_sha256: Mapped[Optional[str]] = mapped_column("indexSha256")
    index_format_version: Mapped[Optional[int]] = mapped_column("indexFormatVersion")
    external_build_data: Mapped[Optional[dict[str, Any]]] = mapped_column("externalBuildData", JSONB(none_as_null=True))
    build_server_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("buildServerMetadata", JSONB(none_as_null=True))
    build_env_vars: Mapped[Optional[dict[str, Any]]] = mapped_column("buildEnvVars", JSONB(none_as_null=True))
    status: Mapped[str] = mapped_column(Enum("PENDING", "INSTALLING", "BUILDING", "DEPLOYING", "DEPLOYED", "FAILED", "CANCELED", "TIMED_OUT", name="WorkerDeploymentStatus"), default="PENDING", server_default=text("'PENDING'"))
    type: Mapped[str] = mapped_column(Enum("MANAGED", "UNMANAGED", "V1", name="WorkerDeploymentType"), default="V1", server_default=text("'V1'"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerDeployment_projectId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerDeployment_environmentId_fkey"))
    worker_id: Mapped[Optional[str]] = mapped_column("workerId", ForeignKey("BackgroundWorker.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerDeployment_workerId_fkey"))
    triggered_by_id: Mapped[Optional[str]] = mapped_column("triggeredById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="WorkerDeployment_triggeredById_fkey"))
    triggered_via: Mapped[Optional[str]] = mapped_column("triggeredVia")
    commit_sha: Mapped[Optional[str]] = mapped_column("commitSHA")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    installed_at: Mapped[Optional[datetime]] = mapped_column("installedAt")
    built_at: Mapped[Optional[datetime]] = mapped_column("builtAt")
    deployed_at: Mapped[Optional[datetime]] = mapped_column("deployedAt")
    failed_at: Mapped[Optional[datetime]] = mapped_column("failedAt")
    error_data: Mapped[Optional[dict[str, Any]]] = mapped_column("errorData", JSONB(none_as_null=True))
    canceled_at: Mapped[Optional[datetime]] = mapped_column("canceledAt")
    canceled_reason: Mapped[Optional[str]] = mapped_column("canceledReason")
    git: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WorkerDeployment_friendlyId_key", "friendlyId", unique=True),
        Index("WorkerDeployment_workerId_key", "workerId", unique=True),
        Index("WorkerDeployment_commitSHA_idx", "commitSHA"),
        Index("WorkerDeployment_environmentId_createdAt_idx", "environmentId", "createdAt"),
        Index("WorkerDeployment_environmentId_status_id_idx", "environmentId", "status", "id"),
        Index("WorkerDeployment_environmentId_externalId_idx", "environmentId", "externalId"),
        Index("WorkerDeployment_projectId_shortCode_key", "projectId", "shortCode", unique=True),
        Index("WorkerDeployment_environmentId_version_key", "environmentId", "version", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="worker_deployment")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="worker_deployments")
    worker: Mapped[Optional["BackgroundWorker"]] = relationship(back_populates="deployment")
    triggered_by: Mapped[Optional["User"]] = relationship(back_populates="deployments")
    promotions: Mapped[list["WorkerDeploymentPromotion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="deployment")
    alerts: Mapped[list["ProjectAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="worker_deployment")
    worker_instance: Mapped[list["WorkerInstance"]] = relationship(back_populates="deployment")
    integration_deployments: Mapped[list["IntegrationDeployment"]] = relationship(back_populates="worker_deployment")

class WorkerDeploymentPromotion(Base):
    __tablename__ = "WorkerDeploymentPromotion"

    id: Mapped[str] = mapped_column(primary_key=True)
    label: Mapped[str]
    deployment_id: Mapped[str] = mapped_column("deploymentId", ForeignKey("WorkerDeployment.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerDeploymentPromotion_deploymentId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkerDeploymentPromotion_environmentId_fkey"))

    __table_args__ = (
        Index("WorkerDeploymentPromotion_environmentId_label_key", "environmentId", "label", unique=True),
    )

    deployment: Mapped["WorkerDeployment"] = relationship(back_populates="promotions")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="worker_deployment_promotions")

class TaskSchedule(Base):
    __tablename__ = "TaskSchedule"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("DECLARATIVE", "IMPERATIVE", name="ScheduleType"), default="IMPERATIVE", server_default=text("'IMPERATIVE'"))
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    deduplication_key: Mapped[str] = mapped_column("deduplicationKey")
    user_provided_deduplication_key: Mapped[bool] = mapped_column("userProvidedDeduplicationKey", default=False, server_default=text("false"))
    generator_expression: Mapped[str] = mapped_column("generatorExpression")
    generator_description: Mapped[str] = mapped_column("generatorDescription", default="", server_default=text("''"))
    generator_type: Mapped[str] = mapped_column("generatorType", Enum("CRON", name="ScheduleGeneratorType"), default="CRON", server_default=text("'CRON'"))
    timezone_: Mapped[str] = mapped_column("timezone", default="UTC", server_default=text("'UTC'"))
    window_duration_seconds: Mapped[Optional[int]] = mapped_column("windowDurationSeconds")
    window_percentage: Mapped[Optional[int]] = mapped_column("windowPercentage")
    default_window_duration_seconds: Mapped[Optional[int]] = mapped_column("defaultWindowDurationSeconds")
    minimum_window_duration_seconds: Mapped[Optional[int]] = mapped_column("minimumWindowDurationSeconds")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    last_run_triggered_at: Mapped[Optional[datetime]] = mapped_column("lastRunTriggeredAt")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskSchedule_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))

    __table_args__ = (
        Index("TaskSchedule_friendlyId_key", "friendlyId", unique=True),
        Index("TaskSchedule_projectId_idx", "projectId"),
        Index("TaskSchedule_projectId_createdAt_idx", "projectId", "createdAt"),
        Index("TaskSchedule_projectId_deduplicationKey_key", "projectId", "deduplicationKey", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="task_schedules")
    instances: Mapped[list["TaskScheduleInstance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task_schedule")

class TaskScheduleInstance(Base):
    __tablename__ = "TaskScheduleInstance"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_schedule_id: Mapped[str] = mapped_column("taskScheduleId", ForeignKey("TaskSchedule.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskScheduleInstance_taskScheduleId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskScheduleInstance_environmentId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskScheduleInstance_projectId_fkey"))
    schedule_phase: Mapped[Optional[int]] = mapped_column("schedulePhase")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    last_scheduled_timestamp: Mapped[Optional[datetime]] = mapped_column("lastScheduledTimestamp")
    next_scheduled_timestamp: Mapped[Optional[datetime]] = mapped_column("nextScheduledTimestamp")

    __table_args__ = (
        Index("TaskScheduleInstance_environmentId_idx", "environmentId"),
        Index("TaskScheduleInstance_projectId_active_idx", "projectId", "active"),
        Index("TaskScheduleInstance_taskScheduleId_environmentId_key", "taskScheduleId", "environmentId", unique=True),
    )

    task_schedule: Mapped["TaskSchedule"] = relationship(back_populates="instances")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_schedule_instances")
    project: Mapped["Project"] = relationship(back_populates="task_schedule_instances")

class RuntimeEnvironmentSession(Base):
    __tablename__ = "RuntimeEnvironmentSession"

    id: Mapped[str] = mapped_column(primary_key=True)
    ip_address: Mapped[str] = mapped_column("ipAddress")
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="RuntimeEnvironmentSession_environmentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    disconnected_at: Mapped[Optional[datetime]] = mapped_column("disconnectedAt")

    environment: Mapped["RuntimeEnvironment"] = relationship(foreign_keys=[environment_id], back_populates="sessions")
    current_environments: Mapped[list["RuntimeEnvironment"]] = relationship(foreign_keys="RuntimeEnvironment.current_session_id", back_populates="current_session")

class ProjectAlertChannel(Base):
    __tablename__ = "ProjectAlertChannel"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    deduplication_key: Mapped[str] = mapped_column("deduplicationKey")
    user_provided_deduplication_key: Mapped[bool] = mapped_column("userProvidedDeduplicationKey", default=False, server_default=text("false"))
    integration_id: Mapped[Optional[str]] = mapped_column("integrationId", ForeignKey("OrganizationIntegration.id", ondelete="SET NULL", onupdate="CASCADE", name="ProjectAlertChannel_integrationId_fkey"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    type: Mapped[str] = mapped_column(Enum("EMAIL", "SLACK", "WEBHOOK", name="ProjectAlertChannelType"))
    name: Mapped[str]
    properties: Mapped[dict[str, Any]] = mapped_column(JSONB)
    alert_types: Mapped[list[str]] = mapped_column("alertTypes", ARRAY(Enum("TASK_RUN", "TASK_RUN_ATTEMPT", "DEPLOYMENT_FAILURE", "DEPLOYMENT_SUCCESS", "ERROR_GROUP", "DASHBOARD_AGENT_WATCH", name="ProjectAlertType")), nullable=True)
    environment_types: Mapped[list[str]] = mapped_column("environmentTypes", ARRAY(Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType")), nullable=True, default=lambda: ["STAGING", "PRODUCTION"], server_default=text("ARRAY['STAGING', 'PRODUCTION']::\"RuntimeEnvironmentType\"[]"))
    error_alert_config: Mapped[Optional[dict[str, Any]]] = mapped_column("errorAlertConfig", JSONB(none_as_null=True))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlertChannel_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ProjectAlertChannel_friendlyId_key", "friendlyId", unique=True),
        Index("ProjectAlertChannel_projectId_deduplicationKey_key", "projectId", "deduplicationKey", unique=True),
    )

    integration: Mapped[Optional["OrganizationIntegration"]] = relationship(back_populates="alert_channels")
    project: Mapped["Project"] = relationship(back_populates="alert_channels")
    alerts: Mapped[list["ProjectAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="channel")
    alert_storages: Mapped[list["ProjectAlertStorage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="alert_channel")

class ProjectAlert(Base):
    __tablename__ = "ProjectAlert"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlert_projectId_fkey"))
    environment_id: Mapped[str] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlert_environmentId_fkey"))
    channel_id: Mapped[str] = mapped_column("channelId", ForeignKey("ProjectAlertChannel.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlert_channelId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "SENT", "FAILED", name="ProjectAlertStatus"), default="PENDING", server_default=text("'PENDING'"))
    type: Mapped[str] = mapped_column(Enum("TASK_RUN", "TASK_RUN_ATTEMPT", "DEPLOYMENT_FAILURE", "DEPLOYMENT_SUCCESS", "ERROR_GROUP", "DASHBOARD_AGENT_WATCH", name="ProjectAlertType"))
    task_run_attempt_id: Mapped[Optional[str]] = mapped_column("taskRunAttemptId")
    task_run_id: Mapped[Optional[str]] = mapped_column("taskRunId")
    worker_deployment_id: Mapped[Optional[str]] = mapped_column("workerDeploymentId", ForeignKey("WorkerDeployment.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlert_workerDeploymentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ProjectAlert_friendlyId_key", "friendlyId", unique=True),
        Index("ProjectAlert_channelId_idx", "channelId"),
    )

    project: Mapped["Project"] = relationship(back_populates="alerts")
    environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="alerts")
    channel: Mapped["ProjectAlertChannel"] = relationship(back_populates="alerts")
    worker_deployment: Mapped[Optional["WorkerDeployment"]] = relationship(back_populates="alerts")

class ProjectAlertStorage(Base):
    __tablename__ = "ProjectAlertStorage"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlertStorage_projectId_fkey"))
    alert_channel_id: Mapped[str] = mapped_column("alertChannelId", ForeignKey("ProjectAlertChannel.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectAlertStorage_alertChannelId_fkey"))
    alert_type: Mapped[str] = mapped_column("alertType", Enum("TASK_RUN", "TASK_RUN_ATTEMPT", "DEPLOYMENT_FAILURE", "DEPLOYMENT_SUCCESS", "ERROR_GROUP", "DASHBOARD_AGENT_WATCH", name="ProjectAlertType"))
    storage_id: Mapped[str] = mapped_column("storageId")
    storage_data: Mapped[dict[str, Any]] = mapped_column("storageData", JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ProjectAlertStorage_alertChannelId_alertType_storageId_idx", "alertChannelId", "alertType", "storageId"),
    )

    project: Mapped["Project"] = relationship(back_populates="alert_storages")
    alert_channel: Mapped["ProjectAlertChannel"] = relationship(back_populates="alert_storages")

class OrganizationIntegration(Base):
    __tablename__ = "OrganizationIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    service: Mapped[str] = mapped_column(Enum("SLACK", "VERCEL", name="IntegrationService"))
    external_organization_id: Mapped[Optional[str]] = mapped_column("externalOrganizationId")
    integration_data: Mapped[dict[str, Any]] = mapped_column("integrationData", JSONB)
    token_reference_id: Mapped[str] = mapped_column("tokenReferenceId", ForeignKey("SecretReference.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationIntegration_tokenReferenceId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationIntegration_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("OrganizationIntegration_friendlyId_key", "friendlyId", unique=True),
        Index("OrganizationIntegration_externalOrganizationId_idx", "externalOrganizationId"),
    )

    token_reference: Mapped["SecretReference"] = relationship(back_populates="organization_integration")
    organization: Mapped["Organization"] = relationship(back_populates="organization_integrations")
    alert_channels: Mapped[list["ProjectAlertChannel"]] = relationship(back_populates="integration")
    organization_project_integration: Mapped[list["OrganizationProjectIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization_integration")

class OrganizationProjectIntegration(Base):
    __tablename__ = "OrganizationProjectIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_integration_id: Mapped[str] = mapped_column("organizationIntegrationId", ForeignKey("OrganizationIntegration.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationProjectIntegration_organizationIntegrationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationProjectIntegration_projectId_fkey"))
    external_entity_id: Mapped[str] = mapped_column("externalEntityId")
    integration_data: Mapped[dict[str, Any]] = mapped_column("integrationData", JSONB)
    installed_by: Mapped[Optional[str]] = mapped_column("installedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("OrganizationProjectIntegration_projectId_idx", "projectId"),
        Index("OrganizationProjectIntegration_projectId_organizationIntegr_idx", "projectId", "organizationIntegrationId"),
        Index("OrganizationProjectIntegration_externalEntityId_idx", "externalEntityId"),
    )

    organization_integration: Mapped["OrganizationIntegration"] = relationship(back_populates="organization_project_integration")
    project: Mapped["Project"] = relationship(back_populates="organization_project_integration")

class BulkActionGroup(Base):
    __tablename__ = "BulkActionGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkActionGroup_projectId_fkey"))
    environment_id: Mapped[Optional[str]] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkActionGroup_environmentId_fkey"))
    type: Mapped[str] = mapped_column(Enum("CANCEL", "REPLAY", name="BulkActionType"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", "ABORTED", name="BulkActionStatus"), default="PENDING", server_default=text("'PENDING'"))
    query_name: Mapped[Optional[str]] = mapped_column("queryName")
    params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    dedupe_key: Mapped[Optional[str]] = mapped_column("dedupeKey")
    cursor: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    success_count: Mapped[int] = mapped_column("successCount", default=0, server_default=text("0"))
    failure_count: Mapped[int] = mapped_column("failureCount", default=0, server_default=text("0"))
    total_count: Mapped[int] = mapped_column("totalCount", default=0, server_default=text("0"))
    completion_notification: Mapped[str] = mapped_column("completionNotification", Enum("NONE", "EMAIL", name="BulkActionNotificationType"), default="NONE", server_default=text("'NONE'"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="BulkActionGroup_userId_fkey"))
    name: Mapped[Optional[str]]
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("BulkActionGroup_friendlyId_key", "friendlyId", unique=True),
        Index("BulkActionGroup_environmentId_createdAt_idx", "environmentId", "createdAt"),
        Index("BulkActionGroup_environmentId_type_dedupeKey_idx", "environmentId", "type", "dedupeKey"),
        Index("BulkActionGroup_environmentId_status_type_idx", "environmentId", "status", "type"),
    )

    project: Mapped["Project"] = relationship(back_populates="bulk_action_groups")
    environment: Mapped[Optional["RuntimeEnvironment"]] = relationship(back_populates="bulk_action_group")
    user: Mapped[Optional["User"]] = relationship(back_populates="bulk_actions")

class BulkActionItem(Base):
    __tablename__ = "BulkActionItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[Optional[str]] = mapped_column("friendlyId")
    group_id: Mapped[str] = mapped_column("groupId")
    type: Mapped[str] = mapped_column(Enum("CANCEL", "REPLAY", name="BulkActionType"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", "FAILED", name="BulkActionItemStatus"), default="PENDING", server_default=text("'PENDING'"))
    source_run_id: Mapped[str] = mapped_column("sourceRunId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkActionItem_sourceRunId_fkey"))
    destination_run_id: Mapped[Optional[str]] = mapped_column("destinationRunId", ForeignKey("TaskRun.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkActionItem_destinationRunId_fkey"))
    error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    source_run: Mapped["TaskRun"] = relationship(foreign_keys=[source_run_id], back_populates="source_bulk_action_items")
    destination_run: Mapped[Optional["TaskRun"]] = relationship(foreign_keys=[destination_run_id], back_populates="destination_bulk_action_items")

class RealtimeStreamChunk(Base):
    __tablename__ = "RealtimeStreamChunk"

    id: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str]
    value: Mapped[str]
    sequence: Mapped[int]
    run_id: Mapped[str] = mapped_column("runId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("RealtimeStreamChunk_runId_idx", "runId"),
        Index("RealtimeStreamChunk_createdAt_idx", "createdAt"),
    )

class TaskEventPartitioned(Base):
    __tablename__ = "TaskEventPartitioned"

    id: Mapped[str] = mapped_column(primary_key=True)
    message: Mapped[str]
    trace_id: Mapped[str] = mapped_column("traceId")
    span_id: Mapped[str] = mapped_column("spanId")
    parent_id: Mapped[Optional[str]] = mapped_column("parentId")
    tracestate: Mapped[Optional[str]]
    is_error: Mapped[bool] = mapped_column("isError", default=False, server_default=text("false"))
    is_partial: Mapped[bool] = mapped_column("isPartial", default=False, server_default=text("false"))
    is_cancelled: Mapped[bool] = mapped_column("isCancelled", default=False, server_default=text("false"))
    service_name: Mapped[str] = mapped_column("serviceName")
    service_namespace: Mapped[str] = mapped_column("serviceNamespace")
    level: Mapped[str] = mapped_column(Enum("TRACE", "DEBUG", "LOG", "INFO", "WARN", "ERROR", name="TaskEventLevel"), default="TRACE", server_default=text("'TRACE'"))
    kind: Mapped[str] = mapped_column(Enum("UNSPECIFIED", "INTERNAL", "SERVER", "CLIENT", "PRODUCER", "CONSUMER", "UNRECOGNIZED", "LOG", name="TaskEventKind"), default="INTERNAL", server_default=text("'INTERNAL'"))
    status: Mapped[str] = mapped_column(Enum("UNSET", "OK", "ERROR", "UNRECOGNIZED", name="TaskEventStatus"), default="UNSET", server_default=text("'UNSET'"))
    links: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    events: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    start_time: Mapped[int] = mapped_column("startTime", BigInteger)
    duration: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    attempt_id: Mapped[Optional[str]] = mapped_column("attemptId")
    attempt_number: Mapped[Optional[int]] = mapped_column("attemptNumber")
    environment_id: Mapped[str] = mapped_column("environmentId")
    environment_type: Mapped[str] = mapped_column("environmentType", Enum("PRODUCTION", "STAGING", "DEVELOPMENT", "PREVIEW", name="RuntimeEnvironmentType"))
    organization_id: Mapped[str] = mapped_column("organizationId")
    project_id: Mapped[str] = mapped_column("projectId")
    project_ref: Mapped[str] = mapped_column("projectRef")
    run_id: Mapped[str] = mapped_column("runId")
    run_is_test: Mapped[bool] = mapped_column("runIsTest", default=False, server_default=text("false"))
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    task_slug: Mapped[str] = mapped_column("taskSlug")
    task_path: Mapped[Optional[str]] = mapped_column("taskPath")
    task_export_name: Mapped[Optional[str]] = mapped_column("taskExportName")
    worker_id: Mapped[Optional[str]] = mapped_column("workerId")
    worker_version: Mapped[Optional[str]] = mapped_column("workerVersion")
    queue_id: Mapped[Optional[str]] = mapped_column("queueId")
    queue_name: Mapped[Optional[str]] = mapped_column("queueName")
    batch_id: Mapped[Optional[str]] = mapped_column("batchId")
    properties: Mapped[dict[str, Any]] = mapped_column(JSONB)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    style: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output_type: Mapped[Optional[str]] = mapped_column("outputType")
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    payload_type: Mapped[Optional[str]] = mapped_column("payloadType")
    created_at: Mapped[datetime] = mapped_column("createdAt", primary_key=True, default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    usage_duration_ms: Mapped[int] = mapped_column("usageDurationMs", default=0, server_default=text("0"))
    usage_cost_in_cents: Mapped[float] = mapped_column("usageCostInCents", default=0, server_default=text("0"))
    machine_preset: Mapped[Optional[str]] = mapped_column("machinePreset")
    machine_preset_cpu: Mapped[Optional[float]] = mapped_column("machinePresetCpu")
    machine_preset_memory: Mapped[Optional[float]] = mapped_column("machinePresetMemory")
    machine_preset_cents_per_ms: Mapped[Optional[float]] = mapped_column("machinePresetCentsPerMs")

    __table_args__ = (
        Index("TaskEventPartitioned_traceId_idx", "traceId"),
        Index("TaskEventPartitioned_spanId_idx", "spanId"),
        Index("TaskEventPartitioned_runId_idx", "runId"),
    )

class GithubAppInstallation(Base):
    __tablename__ = "GithubAppInstallation"

    id: Mapped[str] = mapped_column(primary_key=True)
    app_installation_id: Mapped[int] = mapped_column("appInstallationId", BigInteger)
    target_id: Mapped[int] = mapped_column("targetId", BigInteger)
    target_type: Mapped[str] = mapped_column("targetType")
    account_handle: Mapped[str] = mapped_column("accountHandle")
    permissions: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    repository_selection: Mapped[str] = mapped_column("repositorySelection", Enum("ALL", "SELECTED", name="GithubRepositorySelection"))
    installed_by: Mapped[Optional[str]] = mapped_column("installedBy")
    installed_by_user_id: Mapped[Optional[str]] = mapped_column("installedByUserId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="GithubAppInstallation_organizationId_fkey"))
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    suspended_at: Mapped[Optional[datetime]] = mapped_column("suspendedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("GithubAppInstallation_appInstallationId_key", "appInstallationId", unique=True),
        Index("GithubAppInstallation_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="github_app_installations")
    repositories: Mapped[list["GithubRepository"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="installation")

class GithubRepository(Base):
    __tablename__ = "GithubRepository"

    id: Mapped[str] = mapped_column(primary_key=True)
    github_id: Mapped[int] = mapped_column("githubId", BigInteger)
    name: Mapped[str]
    full_name: Mapped[str] = mapped_column("fullName")
    html_url: Mapped[str] = mapped_column("htmlUrl")
    private: Mapped[bool]
    default_branch: Mapped[str] = mapped_column("defaultBranch")
    installation_id: Mapped[str] = mapped_column("installationId", ForeignKey("GithubAppInstallation.id", ondelete="CASCADE", onupdate="CASCADE", name="GithubRepository_installationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("GithubRepository_installationId_idx", "installationId"),
        Index("GithubRepository_installationId_githubId_key", "installationId", "githubId", unique=True),
    )

    installation: Mapped["GithubAppInstallation"] = relationship(back_populates="repositories")
    connected_github_repository: Mapped[list["ConnectedGithubRepository"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="repository")

class TrustedDeployRepository(Base):
    __tablename__ = "TrustedDeployRepository"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TrustedDeployRepository_projectId_fkey"))
    issuer: Mapped[str] = mapped_column(default="https://token.actions.githubusercontent.com", server_default=text("'https://token.actions.githubusercontent.com'"))
    repository_owner: Mapped[str] = mapped_column("repositoryOwner")
    repository_name: Mapped[str] = mapped_column("repositoryName")
    repository_id: Mapped[Optional[str]] = mapped_column("repositoryId")
    repository_owner_id: Mapped[Optional[str]] = mapped_column("repositoryOwnerId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TrustedDeployRepository_projectId_key", "projectId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="trusted_deploy_repository")
    environments: Mapped[list["TrustedDeployEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="trusted_deploy_repository")

class TrustedDeployEnvironment(Base):
    __tablename__ = "TrustedDeployEnvironment"

    id: Mapped[str] = mapped_column(primary_key=True)
    trusted_deploy_repository_id: Mapped[str] = mapped_column("trustedDeployRepositoryId", ForeignKey("TrustedDeployRepository.id", ondelete="CASCADE", onupdate="CASCADE", name="TrustedDeployEnvironment_trustedDeployRepositoryId_fkey"))
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TrustedDeployEnvironment_runtimeEnvironmentId_fkey"))
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    scope: Mapped[str] = mapped_column(Enum("ANY", "BRANCH", "TAG", "ENVIRONMENT", name="TrustedDeployScope"))
    scope_value: Mapped[Optional[str]] = mapped_column("scopeValue")
    additional_claims: Mapped[dict[str, Any]] = mapped_column("additionalClaims", JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TrustedDeployEnvironment_runtimeEnvironmentId_key", "runtimeEnvironmentId", unique=True),
        Index("TrustedDeployEnvironment_trustedDeployRepositoryId_idx", "trustedDeployRepositoryId"),
    )

    trusted_deploy_repository: Mapped["TrustedDeployRepository"] = relationship(back_populates="environments")
    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="trusted_deploy_grant")

class ConnectedGithubRepository(Base):
    __tablename__ = "ConnectedGithubRepository"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ConnectedGithubRepository_projectId_fkey"))
    repository_id: Mapped[str] = mapped_column("repositoryId", ForeignKey("GithubRepository.id", ondelete="CASCADE", onupdate="CASCADE", name="ConnectedGithubRepository_repositoryId_fkey"))
    branch_tracking: Mapped[dict[str, Any]] = mapped_column("branchTracking", JSONB)
    preview_deployments_enabled: Mapped[bool] = mapped_column("previewDeploymentsEnabled", default=True, server_default=text("true"))
    build_filters: Mapped[Optional[dict[str, Any]]] = mapped_column("buildFilters", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ConnectedGithubRepository_repositoryId_idx", "repositoryId"),
        Index("ConnectedGithubRepository_projectId_key", "projectId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="connected_github_repository")
    repository: Mapped["GithubRepository"] = relationship(back_populates="connected_github_repository")

class ImpersonationAuditLog(Base):
    __tablename__ = "ImpersonationAuditLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    action: Mapped[str] = mapped_column(Enum("START", "STOP", name="ImpersonationAuditLogAction"))
    admin_id: Mapped[str] = mapped_column("adminId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ImpersonationAuditLog_adminId_fkey"))
    target_id: Mapped[str] = mapped_column("targetId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ImpersonationAuditLog_targetId_fkey"))
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ImpersonationAuditLog_adminId_idx", "adminId"),
        Index("ImpersonationAuditLog_targetId_idx", "targetId"),
        Index("ImpersonationAuditLog_createdAt_idx", "createdAt"),
    )

    admin: Mapped["User"] = relationship(foreign_keys=[admin_id], back_populates="impersonations_performed")
    target: Mapped["User"] = relationship(foreign_keys=[target_id], back_populates="impersonations_received")

class SupportAccessRequest(Base):
    __tablename__ = "SupportAccessRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="SupportAccessRequest_organizationId_fkey"))
    requested_by_id: Mapped[Optional[str]] = mapped_column("requestedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="SupportAccessRequest_requestedById_fkey"))
    reason: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("PENDING", "APPROVED", "CANCELLED", name="SupportAccessRequestStatus"), default="PENDING", server_default=text("'PENDING'"))
    approved_by_id: Mapped[Optional[str]] = mapped_column("approvedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="SupportAccessRequest_approvedById_fkey"))
    approved_at: Mapped[Optional[datetime]] = mapped_column("approvedAt")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    cancelled_by_id: Mapped[Optional[str]] = mapped_column("cancelledById")
    cancelled_at: Mapped[Optional[datetime]] = mapped_column("cancelledAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("SupportAccessRequest_organizationId_status_expiresAt_idx", "organizationId", "status", "expiresAt"),
        Index("SupportAccessRequest_requestedById_idx", "requestedById"),
        Index("SupportAccessRequest_approvedById_idx", "approvedById"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="support_access_requests")
    requested_by: Mapped[Optional["User"]] = relationship(foreign_keys=[requested_by_id], back_populates="support_access_requests_made")
    approved_by: Mapped[Optional["User"]] = relationship(foreign_keys=[approved_by_id], back_populates="support_access_requests_approved")

class CustomerQuery(Base):
    __tablename__ = "CustomerQuery"

    id: Mapped[str] = mapped_column(primary_key=True)
    query: Mapped[str]
    scope: Mapped[str] = mapped_column(Enum("ORGANIZATION", "PROJECT", "ENVIRONMENT", name="CustomerQueryScope"))
    stats: Mapped[dict[str, Any]] = mapped_column(JSONB)
    title: Mapped[Optional[str]]
    source: Mapped[str] = mapped_column(Enum("DASHBOARD", "API", name="CustomerQuerySource"), default="DASHBOARD", server_default=text("'DASHBOARD'"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomerQuery_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomerQuery_projectId_fkey"))
    environment_id: Mapped[Optional[str]] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomerQuery_environmentId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="CustomerQuery_userId_fkey"))
    filter_period: Mapped[Optional[str]] = mapped_column("filterPeriod")
    filter_from: Mapped[Optional[datetime]] = mapped_column("filterFrom")
    filter_to: Mapped[Optional[datetime]] = mapped_column("filterTo")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("CustomerQuery_organizationId_createdAt_idx", "organizationId", "createdAt"),
        Index("CustomerQuery_createdAt_idx", "createdAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="customer_queries")
    project: Mapped[Optional["Project"]] = relationship(back_populates="customer_queries")
    environment: Mapped[Optional["RuntimeEnvironment"]] = relationship(back_populates="customer_queries")
    user: Mapped[Optional["User"]] = relationship(back_populates="customer_queries")

class IntegrationDeployment(Base):
    __tablename__ = "IntegrationDeployment"

    id: Mapped[str] = mapped_column(primary_key=True)
    integration_name: Mapped[str] = mapped_column("integrationName")
    integration_deployment_id: Mapped[str] = mapped_column("integrationDeploymentId")
    commit_sha: Mapped[str] = mapped_column("commitSHA")
    deployment_id: Mapped[Optional[str]] = mapped_column("deploymentId", ForeignKey("WorkerDeployment.id", ondelete="SET NULL", onupdate="CASCADE", name="IntegrationDeployment_deploymentId_fkey"))
    status: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("IntegrationDeployment_commitSHA_idx", "commitSHA"),
        Index("IntegrationDeployment_deploymentId_idx", "deploymentId"),
    )

    worker_deployment: Mapped[Optional["WorkerDeployment"]] = relationship(back_populates="integration_deployments")

class MetricsDashboard(Base):
    __tablename__ = "MetricsDashboard"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    title: Mapped[str]
    description: Mapped[str] = mapped_column(default="", server_default=text("''"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="MetricsDashboard_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="MetricsDashboard_projectId_fkey"))
    owner_id: Mapped[Optional[str]] = mapped_column("ownerId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="MetricsDashboard_ownerId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    layout: Mapped[str]

    __table_args__ = (
        Index("MetricsDashboard_friendlyId_key", "friendlyId", unique=True),
        Index("MetricsDashboard_projectId_createdAt_idx", "projectId", "createdAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="metrics_dashboards")
    project: Mapped[Optional["Project"]] = relationship(back_populates="metrics_dashboards")
    owner: Mapped[Optional["User"]] = relationship(back_populates="metrics_dashboards")

class LlmModel(Base):
    __tablename__ = "llm_models"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str]
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_models_project_id_fkey"))
    model_name: Mapped[str]
    match_pattern: Mapped[str]
    start_date: Mapped[Optional[datetime]]
    source: Mapped[str] = mapped_column(default="default", server_default=text("'default'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    provider: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    context_window: Mapped[Optional[int]]
    max_output_tokens: Mapped[Optional[int]]
    capabilities: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    is_hidden: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    base_model_name: Mapped[Optional[str]]
    pricing_unit: Mapped[Optional[str]]

    __table_args__ = (
        Index("llm_models_friendly_id_key", "friendly_id", unique=True),
        Index("llm_models_project_id_idx", "project_id"),
        Index("llm_models_project_id_model_name_start_date_key", "project_id", "model_name", "start_date", unique=True),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="llm_models")
    pricing_tiers: Mapped[list["LlmPricingTier"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="model")
    prices: Mapped[list["LlmPrice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="model")

class LlmPricingTier(Base):
    __tablename__ = "llm_pricing_tiers"

    id: Mapped[str] = mapped_column(primary_key=True)
    model_id: Mapped[str] = mapped_column(ForeignKey("llm_models.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_pricing_tiers_model_id_fkey"))
    name: Mapped[str]
    is_default: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    priority: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    conditions: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))

    __table_args__ = (
        Index("llm_pricing_tiers_model_id_priority_key", "model_id", "priority", unique=True),
        Index("llm_pricing_tiers_model_id_name_key", "model_id", "name", unique=True),
    )

    model: Mapped["LlmModel"] = relationship(back_populates="pricing_tiers")
    prices: Mapped[list["LlmPrice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="pricing_tier")

class LlmPrice(Base):
    __tablename__ = "llm_prices"

    id: Mapped[str] = mapped_column(primary_key=True)
    model_id: Mapped[str] = mapped_column(ForeignKey("llm_models.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_prices_model_id_fkey"))
    pricing_tier_id: Mapped[str] = mapped_column(ForeignKey("llm_pricing_tiers.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_prices_pricing_tier_id_fkey"))
    usage_type: Mapped[str]
    price: Mapped[DecimalType] = mapped_column(Numeric(precision=20, scale=12))

    __table_args__ = (
        Index("llm_prices_model_id_usage_type_pricing_tier_id_key", "model_id", "usage_type", "pricing_tier_id", unique=True),
    )

    model: Mapped["LlmModel"] = relationship(back_populates="prices")
    pricing_tier: Mapped["LlmPricingTier"] = relationship(back_populates="prices")

class PlatformNotification(Base):
    __tablename__ = "PlatformNotification"

    id: Mapped[str] = mapped_column(primary_key=True)
    friendly_id: Mapped[str] = mapped_column("friendlyId")
    title: Mapped[str]
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    surface: Mapped[str] = mapped_column(Enum("WEBAPP", "CLI", name="PlatformNotificationSurface"))
    scope: Mapped[str] = mapped_column(Enum("USER", "PROJECT", "ORGANIZATION", "GLOBAL", name="PlatformNotificationScope"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformNotification_userId_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformNotification_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformNotification_projectId_fkey"))
    starts_at: Mapped[datetime] = mapped_column("startsAt")
    ends_at: Mapped[datetime] = mapped_column("endsAt")
    cli_max_days_after_first_seen: Mapped[Optional[int]] = mapped_column("cliMaxDaysAfterFirstSeen")
    cli_max_show_count: Mapped[Optional[int]] = mapped_column("cliMaxShowCount")
    cli_show_every: Mapped[Optional[int]] = mapped_column("cliShowEvery")
    priority: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    is_draft: Mapped[bool] = mapped_column("isDraft", default=False, server_default=text("false"))
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PlatformNotification_friendlyId_key", "friendlyId", unique=True),
        Index("PlatformNotification_surface_scope_startsAt_idx", "surface", "scope", "startsAt"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="platform_notifications")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="platform_notifications")
    project: Mapped[Optional["Project"]] = relationship(back_populates="platform_notifications")
    interactions: Mapped[list["PlatformNotificationInteraction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="notification")

class PlatformNotificationInteraction(Base):
    __tablename__ = "PlatformNotificationInteraction"

    id: Mapped[str] = mapped_column(primary_key=True)
    notification_id: Mapped[str] = mapped_column("notificationId", ForeignKey("PlatformNotification.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformNotificationInteraction_notificationId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformNotificationInteraction_userId_fkey"))
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    show_count: Mapped[int] = mapped_column("showCount", default=1, server_default=text("1"))
    webapp_dismissed_at: Mapped[Optional[datetime]] = mapped_column("webappDismissedAt")
    webapp_clicked_at: Mapped[Optional[datetime]] = mapped_column("webappClickedAt")
    cli_dismissed_at: Mapped[Optional[datetime]] = mapped_column("cliDismissedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PlatformNotificationInteraction_notificationId_userId_key", "notificationId", "userId", unique=True),
    )

    notification: Mapped["PlatformNotification"] = relationship(back_populates="interactions")
    user: Mapped["User"] = relationship(back_populates="platform_notification_interactions")

class ErrorGroupState(Base):
    __tablename__ = "ErrorGroupState"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="ErrorGroupState_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ErrorGroupState_projectId_fkey"))
    environment_id: Mapped[Optional[str]] = mapped_column("environmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="ErrorGroupState_environmentId_fkey"))
    task_identifier: Mapped[str] = mapped_column("taskIdentifier")
    error_fingerprint: Mapped[str] = mapped_column("errorFingerprint")
    status: Mapped[str] = mapped_column(Enum("UNRESOLVED", "RESOLVED", "IGNORED", name="ErrorGroupStatus"), default="UNRESOLVED", server_default=text("'UNRESOLVED'"))
    ignored_until: Mapped[Optional[datetime]] = mapped_column("ignoredUntil")
    ignored_until_occurrence_rate: Mapped[Optional[int]] = mapped_column("ignoredUntilOccurrenceRate")
    ignored_until_total_occurrences: Mapped[Optional[int]] = mapped_column("ignoredUntilTotalOccurrences")
    ignored_at_occurrence_count: Mapped[Optional[int]] = mapped_column("ignoredAtOccurrenceCount", BigInteger)
    ignored_at: Mapped[Optional[datetime]] = mapped_column("ignoredAt")
    ignored_reason: Mapped[Optional[str]] = mapped_column("ignoredReason")
    ignored_by_user_id: Mapped[Optional[str]] = mapped_column("ignoredByUserId")
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")
    resolved_in_version: Mapped[Optional[str]] = mapped_column("resolvedInVersion")
    resolved_by: Mapped[Optional[str]] = mapped_column("resolvedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ErrorGroupState_environmentId_status_idx", "environmentId", "status"),
        Index("ErrorGroupState_environmentId_taskIdentifier_errorFingerpri_key", "environmentId", "taskIdentifier", "errorFingerprint", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="error_group_states")
    project: Mapped["Project"] = relationship(back_populates="error_group_states")
    environment: Mapped[Optional["RuntimeEnvironment"]] = relationship(back_populates="error_group_states")

class TaskIdentifier(Base):
    __tablename__ = "TaskIdentifier"

    id: Mapped[str] = mapped_column(primary_key=True)
    runtime_environment_id: Mapped[str] = mapped_column("runtimeEnvironmentId", ForeignKey("RuntimeEnvironment.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskIdentifier_runtimeEnvironmentId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskIdentifier_projectId_fkey"))
    slug: Mapped[str]
    current_trigger_source: Mapped[str] = mapped_column("currentTriggerSource", Enum("STANDARD", "SCHEDULED", "AGENT", "WEBHOOK", name="TaskTriggerSource"), default="STANDARD", server_default=text("'STANDARD'"))
    current_worker_id: Mapped[Optional[str]] = mapped_column("currentWorkerId", ForeignKey("BackgroundWorker.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskIdentifier_currentWorkerId_fkey"))
    first_seen_at: Mapped[datetime] = mapped_column("firstSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_seen_at: Mapped[datetime] = mapped_column("lastSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    is_in_latest_deployment: Mapped[bool] = mapped_column("isInLatestDeployment", default=True, server_default=text("true"))

    __table_args__ = (
        Index("TaskIdentifier_runtimeEnvironmentId_isInLatestDeployment_idx", "runtimeEnvironmentId", "isInLatestDeployment"),
        Index("TaskIdentifier_runtimeEnvironmentId_slug_key", "runtimeEnvironmentId", "slug", unique=True),
    )

    runtime_environment: Mapped["RuntimeEnvironment"] = relationship(back_populates="task_identifiers")
    project: Mapped["Project"] = relationship(back_populates="task_identifiers")
    current_worker: Mapped[Optional["BackgroundWorker"]] = relationship(back_populates="task_identifiers")

class OrganizationDataStore(Base):
    __tablename__ = "OrganizationDataStore"

    id: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str]
    organization_ids: Mapped[list[str]] = mapped_column("organizationIds", ARRAY(Text), nullable=True)
    kind: Mapped[str] = mapped_column(Enum("CLICKHOUSE", name="DataStoreKind"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrganizationDataStore_key_key", "key", unique=True),
        Index("OrganizationDataStore_kind_idx", "kind"),
    )
