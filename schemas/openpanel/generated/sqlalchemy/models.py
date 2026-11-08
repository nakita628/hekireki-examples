from sqlalchemy import ARRAY, Column, Dialect, Enum, ForeignKey, Index, JSON, MetaData, PrimaryKeyConstraint, Table, Text, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone
import uuid as uuid_mod


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
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}

integration_to_notification_rule = Table(
    "_IntegrationToNotificationRule",
    Base.metadata,
    Column("A", Uuid, ForeignKey("integrations.id", ondelete="CASCADE", onupdate="CASCADE", name="_IntegrationToNotificationRule_A_fkey"), nullable=False),
    Column("B", Uuid, ForeignKey("notification_rules.id", ondelete="CASCADE", onupdate="CASCADE", name="_IntegrationToNotificationRule_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_IntegrationToNotificationRule_AB_pkey"),
    Index("_IntegrationToNotificationRule_B_index", "B"),
)


class CodeMigration(Base):
    __tablename__ = "__code_migrations"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("__code_migrations_name_key", "name", unique=True),
    )

class Conversation(Base):
    __tablename__ = "conversations"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="conversations_projectId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="conversations_organizationId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="conversations_userId_fkey"))
    title: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("conversations_projectId_updatedAt_idx", "projectId", "updatedAt"),
        Index("conversations_userId_updatedAt_idx", "userId", "updatedAt"),
    )

    project: Mapped["Project"] = relationship(back_populates="conversations")
    organization: Mapped["Organization"] = relationship(back_populates="conversations")
    user: Mapped["User"] = relationship(back_populates="conversations")
    messages: Mapped[list["ChatMessage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")

class ChatMessage(Base):
    __tablename__ = "chat_messages"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("conversations.id", ondelete="CASCADE", onupdate="CASCADE", name="chat_messages_conversationId_fkey"))
    role: Mapped[str]
    parts: Mapped[dict[str, Any]] = mapped_column(JSON)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("chat_messages_conversationId_createdAt_idx", "conversationId", "createdAt"),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="messages")

class Organization(Base):
    __tablename__ = "organizations"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    slug: Mapped[Optional[str]]
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="organizations_createdByUserId_fkey"))
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    onboarding: Mapped[Optional[str]] = mapped_column(default="completed", server_default=text("'completed'"))
    attribution: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    subscription_customer_id: Mapped[Optional[str]] = mapped_column("subscriptionCustomerId")
    subscription_price_id: Mapped[Optional[str]] = mapped_column("subscriptionPriceId")
    subscription_product_id: Mapped[Optional[str]] = mapped_column("subscriptionProductId")
    subscription_status: Mapped[Optional[str]] = mapped_column("subscriptionStatus")
    subscription_starts_at: Mapped[Optional[datetime]] = mapped_column("subscriptionStartsAt")
    subscription_ends_at: Mapped[Optional[datetime]] = mapped_column("subscriptionEndsAt")
    subscription_canceled_at: Mapped[Optional[datetime]] = mapped_column("subscriptionCanceledAt")
    subscription_created_by_user_id: Mapped[Optional[str]] = mapped_column("subscriptionCreatedByUserId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="organizations_subscriptionCreatedByUserId_fkey"))
    subscription_period_events_count: Mapped[int] = mapped_column("subscriptionPeriodEventsCount", default=0, server_default=text("0"))
    subscription_period_events_count_exceeded_at: Mapped[Optional[datetime]] = mapped_column("subscriptionPeriodEventsCountExceededAt")
    subscription_period_events_limit: Mapped[int] = mapped_column("subscriptionPeriodEventsLimit", default=0, server_default=text("0"))
    subscription_interval: Mapped[Optional[str]] = mapped_column("subscriptionInterval")
    subscription_cancel_reason: Mapped[Optional[str]] = mapped_column("subscriptionCancelReason")
    subscription_cancel_comment: Mapped[Optional[str]] = mapped_column("subscriptionCancelComment")
    subscription_save_discount_applied_at: Mapped[Optional[datetime]] = mapped_column("subscriptionSaveDiscountAppliedAt")
    subscription_pause_at_period_end: Mapped[bool] = mapped_column("subscriptionPauseAtPeriodEnd", default=False, server_default=text("false"))
    subscription_resumes_at: Mapped[Optional[datetime]] = mapped_column("subscriptionResumesAt")
    subscription_first_started_at: Mapped[Optional[datetime]] = mapped_column("subscriptionFirstStartedAt")
    usage_warning_sent_at: Mapped[Optional[datetime]] = mapped_column("usageWarningSentAt")
    usage_exceeded_sent_at: Mapped[Optional[datetime]] = mapped_column("usageExceededSentAt")
    subscription_discount: Mapped[Optional[dict[str, Any]]] = mapped_column("subscriptionDiscount", JSONB(none_as_null=True))
    wind_down_started_at: Mapped[Optional[datetime]] = mapped_column("windDownStartedAt")
    wind_down_step: Mapped[Optional[str]] = mapped_column("windDownStep")
    delete_at: Mapped[Optional[datetime]] = mapped_column("deleteAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("organizations_slug_key", "slug", unique=True),
        Index("organizations_windDownStartedAt_idx", "windDownStartedAt"),
    )

    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_user_id], back_populates="created_organizations")
    subscription_created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[subscription_created_by_user_id], back_populates="subscriptions")
    projects: Mapped[list["Project"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    members: Mapped[list["Member"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    client: Mapped[list["Client"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    dashboard: Mapped[list["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    share_overview: Mapped[list["ShareOverview"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    share_dashboard: Mapped[list["ShareDashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    share_report: Mapped[list["ShareReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    share_widget: Mapped[list["ShareWidget"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    integrations: Mapped[list["Integration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    invites: Mapped[list["Invite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")

class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    email: Mapped[str]
    first_name: Mapped[Optional[str]] = mapped_column("firstName")
    last_name: Mapped[Optional[str]] = mapped_column("lastName")
    name: Mapped[Optional[str]]
    email_verified: Mapped[bool] = mapped_column("emailVerified", default=False, server_default=text("false"))
    image: Mapped[Optional[str]]
    two_factor_enabled: Mapped[bool] = mapped_column("twoFactorEnabled", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
    )

    created_organizations: Mapped[list["Organization"]] = relationship(foreign_keys="Organization.created_by_user_id", back_populates="created_by")
    subscriptions: Mapped[list["Organization"]] = relationship(foreign_keys="Organization.subscription_created_by_user_id", back_populates="subscription_created_by")
    membership: Mapped[list["Member"]] = relationship(foreign_keys="Member.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    sent_invites: Mapped[list["Member"]] = relationship(foreign_keys="Member.invited_by_id", back_populates="invited_by")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    invites: Mapped[list["Invite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="created_by")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factor_challenges: Mapped[list["TwoFactorChallenge"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factor: Mapped[Optional["TwoFactor"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    totp: Mapped[Optional["UserTotp"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class UserTotp(Base):
    __tablename__ = "user_totp"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="user_totp_userId_fkey"))
    secret: Mapped[str]
    recovery_codes: Mapped[list[str]] = mapped_column("recoveryCodes", ARRAY(Text), nullable=True)
    enabled_at: Mapped[Optional[datetime]] = mapped_column("enabledAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("user_totp_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="totp")

class TwoFactor(Base):
    __tablename__ = "two_factor"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_userId_fkey"))
    secret: Mapped[str]
    backup_codes: Mapped[str] = mapped_column("backupCodes")
    verified: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    failed_verification_count: Mapped[int] = mapped_column("failedVerificationCount", default=0, server_default=text("0"))
    locked_until: Mapped[Optional[datetime]] = mapped_column("lockedUntil")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("two_factor_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor")

class TwoFactorChallenge(Base):
    __tablename__ = "two_factor_challenges"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_challenges_userId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("two_factor_challenges_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor_challenges")

class Account(Base):
    __tablename__ = "accounts"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="accounts_userId_fkey"))
    email: Mapped[Optional[str]]
    provider: Mapped[str]
    provider_id: Mapped[Optional[str]] = mapped_column("providerId")
    account_id: Mapped[Optional[str]] = mapped_column("accountId")
    id_token: Mapped[Optional[str]] = mapped_column("idToken")
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    access_token_expires_at: Mapped[Optional[datetime]] = mapped_column("accessTokenExpiresAt")
    refresh_token_expires_at: Mapped[Optional[datetime]] = mapped_column("refreshTokenExpiresAt")
    scope: Mapped[Optional[str]]
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    user: Mapped["User"] = relationship(back_populates="accounts")
    reset_passwords: Mapped[list["ResetPassword"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="account")

class Session(Base):
    __tablename__ = "sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[Optional[str]]
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="sessions_userId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("sessions_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class Verification(Base):
    __tablename__ = "verifications"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    value: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("verifications_identifier_idx", "identifier"),
    )

class Member(Base):
    __tablename__ = "members"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    role: Mapped[str]
    email: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="members_userId_fkey"))
    invited_by_id: Mapped[Optional[str]] = mapped_column("invitedById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="members_invitedById_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="members_organizationId_fkey"))
    project_scope: Mapped[str] = mapped_column("projectScope", Enum("all", "selected", name="ProjectScope"), default="all", server_default=text("'all'"))
    meta: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("members_organizationId_userId_key", "organizationId", "userId", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="membership")
    invited_by: Mapped[Optional["User"]] = relationship(foreign_keys=[invited_by_id], back_populates="sent_invites")
    organization: Mapped["Organization"] = relationship(back_populates="members")
    projects: Mapped[list["MemberProject"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="member")

class MemberProject(Base):
    __tablename__ = "member_projects"

    member_id: Mapped[uuid_mod.UUID] = mapped_column("memberId", Uuid, ForeignKey("members.id", ondelete="CASCADE", onupdate="CASCADE", name="member_projects_memberId_fkey"), primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="member_projects_projectId_fkey"), primary_key=True)
    level: Mapped[Optional[str]] = mapped_column(Enum("read", "write", "admin", name="AccessLevel"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("member_projects_projectId_idx", "projectId"),
    )

    member: Mapped["Member"] = relationship(back_populates="projects")
    project: Mapped["Project"] = relationship(back_populates="member_projects")

class Invite(Base):
    __tablename__ = "invites"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    created_by_id: Mapped[str] = mapped_column("createdById", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="invites_createdById_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="invites_organizationId_fkey"))
    project_access: Mapped[dict[str, Any]] = mapped_column("projectAccess", JSONB, default=lambda: [], server_default=text("'[]'"))
    project_scope: Mapped[str] = mapped_column("projectScope", Enum("all", "selected", name="ProjectScope"), default="all", server_default=text("'all'"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    role: Mapped[str]

    created_by: Mapped["User"] = relationship(back_populates="invites")
    organization: Mapped["Organization"] = relationship(back_populates="invites")

class Project(Base):
    __tablename__ = "projects"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="projects_organizationId_fkey"))
    events_count: Mapped[int] = mapped_column("eventsCount", default=0, server_default=text("0"))
    types: Mapped[list[str]] = mapped_column(ARRAY(Enum("website", "app", "backend", name="ProjectType")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"ProjectType\"[]"))
    domain: Mapped[Optional[str]]
    cors: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    cross_domain: Mapped[bool] = mapped_column("crossDomain", default=False, server_default=text("false"))
    allow_unsafe_revenue_tracking: Mapped[bool] = mapped_column("allowUnsafeRevenueTracking", default=False, server_default=text("false"))
    ai_store_content: Mapped[bool] = mapped_column("aiStoreContent", default=True, server_default=text("true"))
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    first_event_at: Mapped[Optional[datetime]] = mapped_column("firstEventAt")
    no_data_notified_at: Mapped[Optional[datetime]] = mapped_column("noDataNotifiedAt")
    data_stopped_notified_at: Mapped[Optional[datetime]] = mapped_column("dataStoppedNotifiedAt")
    delete_at: Mapped[Optional[datetime]] = mapped_column("deleteAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    organization: Mapped["Organization"] = relationship(back_populates="projects")
    clients: Mapped[list["Client"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    reports: Mapped[list["Report"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    dashboards: Mapped[list["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    share_dashboards: Mapped[list["ShareDashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    share_reports: Mapped[list["ShareReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    share_widgets: Mapped[list["ShareWidget"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    meta: Mapped[list["EventMeta"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    custom_events: Mapped[list["CustomEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    references: Mapped[list["Reference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    member_projects: Mapped[list["MemberProject"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    notification_rules: Mapped[list["NotificationRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    notifications: Mapped[list["Notification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    imports: Mapped[list["Import"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    cohorts: Mapped[list["Cohort"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    export_watermarks: Mapped[list["ExportWatermark"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    integrations: Mapped[list["Integration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    share: Mapped[Optional["ShareOverview"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    gsc_connection: Mapped[Optional["GscConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")

class Cohort(Base):
    __tablename__ = "cohorts"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="cohorts_projectId_fkey"))
    definition: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    is_static: Mapped[bool] = mapped_column("isStatic", default=False, server_default=text("false"))
    profile_count: Mapped[int] = mapped_column("profileCount", default=0, server_default=text("0"))
    last_computed_at: Mapped[Optional[datetime]] = mapped_column("lastComputedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("cohorts_projectId_idx", "projectId"),
    )

    project: Mapped["Project"] = relationship(back_populates="cohorts")

class Salt(Base):
    __tablename__ = "salts"

    salt: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class Client(Base):
    __tablename__ = "clients"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    secret: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("read", "write", "root", name="ClientType"), default="write", server_default=text("'write'"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="clients_projectId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="clients_organizationId_fkey"))
    ignore_cors_and_secret: Mapped[bool] = mapped_column("ignoreCorsAndSecret", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped[Optional["Project"]] = relationship(back_populates="clients")
    organization: Mapped["Organization"] = relationship(back_populates="client")

class Dashboard(Base):
    __tablename__ = "dashboards"

    id: Mapped[str] = mapped_column(primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="dashboards_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="dashboards_projectId_fkey"))
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    organization: Mapped["Organization"] = relationship(back_populates="dashboard")
    project: Mapped["Project"] = relationship(back_populates="dashboards")
    reports: Mapped[list["Report"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dashboard")
    share: Mapped[Optional["ShareDashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dashboard")

class Report(Base):
    __tablename__ = "reports"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    interval: Mapped[str] = mapped_column(Enum("hour", "day", "month", "minute", "week", name="Interval"))
    range: Mapped[str] = mapped_column(default="30d", server_default=text("'30d'"))
    chart_type: Mapped[str] = mapped_column("chartType", Enum("linear", "bar", "histogram", "pie", "metric", "area", "map", "funnel", "retention", "conversion", "sankey", "distribution", name="ChartType"))
    line_type: Mapped[str] = mapped_column("lineType", default="monotone", server_default=text("'monotone'"))
    breakdowns: Mapped[dict[str, Any]] = mapped_column(JSONB)
    events: Mapped[dict[str, Any]] = mapped_column(JSONB)
    global_filters: Mapped[dict[str, Any]] = mapped_column("globalFilters", JSONB, default=lambda: [], server_default=text("'[]'"))
    formula: Mapped[Optional[str]]
    unit: Mapped[Optional[str]]
    metric: Mapped[str] = mapped_column(Enum("sum", "average", "min", "max", "count", name="Metric"), default="sum", server_default=text("'sum'"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="reports_projectId_fkey"))
    previous: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    criteria: Mapped[Optional[str]]
    funnel_group: Mapped[Optional[str]] = mapped_column("funnelGroup")
    funnel_window: Mapped[Optional[float]] = mapped_column("funnelWindow")
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    display: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    visible_series: Mapped[list[str]] = mapped_column("visibleSeries", ARRAY(Text), nullable=True)
    start_date: Mapped[Optional[str]] = mapped_column("startDate")
    end_date: Mapped[Optional[str]] = mapped_column("endDate")
    dashboard_id: Mapped[str] = mapped_column("dashboardId", ForeignKey("dashboards.id", ondelete="CASCADE", onupdate="CASCADE", name="reports_dashboardId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="reports")
    dashboard: Mapped["Dashboard"] = relationship(back_populates="reports")
    layout: Mapped[Optional["ReportLayout"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="report")
    share: Mapped[Optional["ShareReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="report")

class ReportLayout(Base):
    __tablename__ = "report_layouts"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    report_id: Mapped[uuid_mod.UUID] = mapped_column("reportId", Uuid, ForeignKey("reports.id", ondelete="CASCADE", onupdate="CASCADE", name="report_layouts_reportId_fkey"))
    grid_columns: Mapped[int] = mapped_column("gridColumns", default=12, server_default=text("12"))
    x: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    y: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    w: Mapped[int] = mapped_column(default=8, server_default=text("8"))
    h: Mapped[int] = mapped_column(default=6, server_default=text("6"))
    min_w: Mapped[Optional[int]] = mapped_column("minW", default=4, server_default=text("4"))
    min_h: Mapped[Optional[int]] = mapped_column("minH", default=4, server_default=text("4"))
    max_w: Mapped[Optional[int]] = mapped_column("maxW")
    max_h: Mapped[Optional[int]] = mapped_column("maxH")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("report_layouts_reportId_key", "reportId", unique=True),
    )

    report: Mapped["Report"] = relationship(back_populates="layout")

class ShareOverview(Base):
    __tablename__ = "shares"

    id: Mapped[str] = mapped_column()
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="shares_projectId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="shares_organizationId_fkey"))
    public: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("shares_id_key", "id", unique=True),
        Index("shares_projectId_key", "projectId", unique=True),
    )

    __mapper_args__ = {"primary_key": [id]}

    project: Mapped["Project"] = relationship(back_populates="share")
    organization: Mapped["Organization"] = relationship(back_populates="share_overview")

class ShareDashboard(Base):
    __tablename__ = "share_dashboards"

    id: Mapped[str] = mapped_column()
    dashboard_id: Mapped[str] = mapped_column("dashboardId", ForeignKey("dashboards.id", ondelete="CASCADE", onupdate="CASCADE", name="share_dashboards_dashboardId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="share_dashboards_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="share_dashboards_projectId_fkey"))
    public: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("share_dashboards_id_key", "id", unique=True),
        Index("share_dashboards_dashboardId_key", "dashboardId", unique=True),
    )

    __mapper_args__ = {"primary_key": [id]}

    dashboard: Mapped["Dashboard"] = relationship(back_populates="share")
    organization: Mapped["Organization"] = relationship(back_populates="share_dashboard")
    project: Mapped["Project"] = relationship(back_populates="share_dashboards")

class ShareReport(Base):
    __tablename__ = "share_reports"

    id: Mapped[str] = mapped_column()
    report_id: Mapped[uuid_mod.UUID] = mapped_column("reportId", Uuid, ForeignKey("reports.id", ondelete="CASCADE", onupdate="CASCADE", name="share_reports_reportId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="share_reports_organizationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="share_reports_projectId_fkey"))
    public: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("share_reports_id_key", "id", unique=True),
        Index("share_reports_reportId_key", "reportId", unique=True),
    )

    __mapper_args__ = {"primary_key": [id]}

    report: Mapped["Report"] = relationship(back_populates="share")
    organization: Mapped["Organization"] = relationship(back_populates="share_report")
    project: Mapped["Project"] = relationship(back_populates="share_reports")

class ShareWidget(Base):
    __tablename__ = "share_widgets"

    id: Mapped[str] = mapped_column()
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="share_widgets_projectId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="share_widgets_organizationId_fkey"))
    public: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    options: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("share_widgets_id_key", "id", unique=True),
    )

    __mapper_args__ = {"primary_key": [id]}

    project: Mapped["Project"] = relationship(back_populates="share_widgets")
    organization: Mapped["Organization"] = relationship(back_populates="share_widget")

class EventMeta(Base):
    __tablename__ = "event_meta"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    conversion: Mapped[Optional[bool]]
    color: Mapped[Optional[str]]
    icon: Mapped[Optional[str]]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="event_meta_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("event_meta_name_projectId_key", "name", "projectId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="meta")

class CustomEvent(Base):
    __tablename__ = "custom_events"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    items: Mapped[dict[str, Any]] = mapped_column(JSONB)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="custom_events_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("custom_events_projectId_name_key", "projectId", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="custom_events")

class Reference(Base):
    __tablename__ = "references"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    title: Mapped[str]
    description: Mapped[Optional[str]]
    date_: Mapped[datetime] = mapped_column("date", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="references_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="references")

class NotificationRule(Base):
    __tablename__ = "notification_rules"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="notification_rules_projectId_fkey"))
    send_to_app: Mapped[bool] = mapped_column("sendToApp", default=False, server_default=text("false"))
    send_to_email: Mapped[bool] = mapped_column("sendToEmail", default=False, server_default=text("false"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    template: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="notification_rules")
    notifications: Mapped[list["Notification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="notification_rule")
    integrations: Mapped[list["Integration"]] = relationship(secondary=integration_to_notification_rule, back_populates="notification_rules")

class Notification(Base):
    __tablename__ = "notifications"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="notifications_projectId_fkey"))
    title: Mapped[str]
    message: Mapped[str]
    is_read_at: Mapped[Optional[datetime]] = mapped_column("isReadAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    send_to_app: Mapped[bool] = mapped_column("sendToApp", default=False, server_default=text("false"))
    send_to_email: Mapped[bool] = mapped_column("sendToEmail", default=False, server_default=text("false"))
    integration_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column("integrationId", Uuid, ForeignKey("integrations.id", ondelete="CASCADE", onupdate="CASCADE", name="notifications_integrationId_fkey"))
    notification_rule_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column("notificationRuleId", Uuid, ForeignKey("notification_rules.id", ondelete="CASCADE", onupdate="CASCADE", name="notifications_notificationRuleId_fkey"))
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    project: Mapped["Project"] = relationship(back_populates="notifications")
    integration: Mapped[Optional["Integration"]] = relationship(back_populates="notifications")
    notification_rule: Mapped[Optional["NotificationRule"]] = relationship(back_populates="notifications")

class Integration(Base):
    __tablename__ = "integrations"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    name: Mapped[str]
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="integrations_organizationId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="integrations_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("integrations_organizationId_idx", "organizationId"),
        Index("integrations_projectId_idx", "projectId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="integrations")
    project: Mapped[Optional["Project"]] = relationship(back_populates="integrations")
    notifications: Mapped[list["Notification"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")
    export_watermarks: Mapped[list["ExportWatermark"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")
    notification_rules: Mapped[list["NotificationRule"]] = relationship(secondary=integration_to_notification_rule, back_populates="integrations")

class ExportWatermark(Base):
    __tablename__ = "export_watermarks"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="export_watermarks_projectId_fkey"))
    integration_id: Mapped[uuid_mod.UUID] = mapped_column("integrationId", Uuid, ForeignKey("integrations.id", ondelete="CASCADE", onupdate="CASCADE", name="export_watermarks_integrationId_fkey"))
    last_inserted_at: Mapped[datetime] = mapped_column("lastInsertedAt")
    last_event_id: Mapped[str] = mapped_column("lastEventId", default="", server_default=text("''"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("export_watermarks_integrationId_idx", "integrationId"),
        Index("export_watermarks_projectId_integrationId_key", "projectId", "integrationId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="export_watermarks")
    integration: Mapped["Integration"] = relationship(back_populates="export_watermarks")

class ResetPassword(Base):
    __tablename__ = "reset_password"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_id: Mapped[str] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="CASCADE", onupdate="CASCADE", name="reset_password_accountId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    account: Mapped["Account"] = relationship(back_populates="reset_passwords")

class Import(Base):
    __tablename__ = "imports"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="imports_projectId_fkey"))
    job_id: Mapped[Optional[str]] = mapped_column("jobId")
    status: Mapped[str] = mapped_column(Enum("pending", "processing", "completed", "failed", name="ImportStatus"))
    status_message: Mapped[Optional[str]] = mapped_column("statusMessage")
    error_message: Mapped[Optional[str]] = mapped_column("errorMessage")
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    total_events: Mapped[int] = mapped_column("totalEvents", default=0, server_default=text("0"))
    processed_events: Mapped[int] = mapped_column("processedEvents", default=0, server_default=text("0"))
    current_step: Mapped[Optional[str]] = mapped_column("currentStep")
    current_batch: Mapped[Optional[str]] = mapped_column("currentBatch")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="imports")

class ProjectInsight(Base):
    __tablename__ = "project_insights"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId")
    module_key: Mapped[str] = mapped_column("moduleKey")
    dimension_key: Mapped[str] = mapped_column("dimensionKey")
    window_kind: Mapped[str] = mapped_column("windowKind")
    state: Mapped[str] = mapped_column(Enum("active", "suppressed", "closed", name="InsightState"), default="active", server_default=text("'active'"))
    title: Mapped[str]
    summary: Mapped[Optional[str]]
    display_name: Mapped[str] = mapped_column("displayName", default="", server_default=text("''"))
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    direction: Mapped[Optional[str]]
    impact_score: Mapped[float] = mapped_column("impactScore", default=0, server_default=text("0"))
    severity_band: Mapped[Optional[str]] = mapped_column("severityBand")
    relevance_score: Mapped[Optional[float]] = mapped_column("relevanceScore")
    ai_summary: Mapped[Optional[str]] = mapped_column("aiSummary")
    ai_category: Mapped[Optional[str]] = mapped_column("aiCategory")
    email_worthy: Mapped[bool] = mapped_column("emailWorthy", default=False, server_default=text("false"))
    reference_worthy: Mapped[bool] = mapped_column("referenceWorthy", default=False, server_default=text("false"))
    enriched_at: Mapped[Optional[datetime]] = mapped_column("enrichedAt")
    enrich_version: Mapped[Optional[int]] = mapped_column("enrichVersion")
    version: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    thread_id: Mapped[uuid_mod.UUID] = mapped_column("threadId", Uuid, server_default=text("gen_random_uuid()"))
    window_start: Mapped[Optional[datetime]] = mapped_column("windowStart")
    window_end: Mapped[Optional[datetime]] = mapped_column("windowEnd")
    first_detected_at: Mapped[datetime] = mapped_column("firstDetectedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    last_seen_at: Mapped[datetime] = mapped_column("lastSeenAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("project_insights_projectId_impactScore_idx", "projectId", "impactScore"),
        Index("project_insights_projectId_moduleKey_windowKind_state_idx", "projectId", "moduleKey", "windowKind", "state"),
        Index("project_insights_projectId_state_relevanceScore_idx", "projectId", "state", "relevanceScore"),
        Index("project_insights_projectId_moduleKey_dimensionKey_windowKin_key", "projectId", "moduleKey", "dimensionKey", "windowKind", "state", unique=True),
    )

    events: Mapped[list["InsightEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="insight")

class InsightEvent(Base):
    __tablename__ = "insight_events"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    insight_id: Mapped[uuid_mod.UUID] = mapped_column("insightId", Uuid, ForeignKey("project_insights.id", ondelete="CASCADE", onupdate="CASCADE", name="insight_events_insightId_fkey"))
    event_kind: Mapped[str] = mapped_column("eventKind")
    change_from: Mapped[Optional[dict[str, Any]]] = mapped_column("changeFrom", JSONB(none_as_null=True))
    change_to: Mapped[Optional[dict[str, Any]]] = mapped_column("changeTo", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("insight_events_insightId_createdAt_idx", "insightId", "createdAt"),
    )

    insight: Mapped["ProjectInsight"] = relationship(back_populates="events")

class GscConnection(Base):
    __tablename__ = "gsc_connections"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="gsc_connections_projectId_fkey"))
    site_url: Mapped[str] = mapped_column("siteUrl", default="", server_default=text("''"))
    access_token: Mapped[str] = mapped_column("accessToken")
    refresh_token: Mapped[str] = mapped_column("refreshToken")
    access_token_expires_at: Mapped[Optional[datetime]] = mapped_column("accessTokenExpiresAt")
    last_synced_at: Mapped[Optional[datetime]] = mapped_column("lastSyncedAt")
    last_sync_status: Mapped[Optional[str]] = mapped_column("lastSyncStatus")
    last_sync_error: Mapped[Optional[str]] = mapped_column("lastSyncError")
    backfill_status: Mapped[Optional[str]] = mapped_column("backfillStatus")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("gsc_connections_projectId_key", "projectId", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="gsc_connection")

class EmailUnsubscribe(Base):
    __tablename__ = "email_unsubscribes"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, server_default=text("gen_random_uuid()"))
    email: Mapped[str]
    category: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("email_unsubscribes_email_category_key", "email", "category", unique=True),
    )
