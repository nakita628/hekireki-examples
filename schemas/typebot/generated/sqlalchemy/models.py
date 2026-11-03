from sqlalchemy import Dialect, Enum, ForeignKey, Index, MetaData, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
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
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]
    oauth_token_secret: Mapped[Optional[str]]
    oauth_token: Mapped[Optional[str]]
    refresh_token_expires_in: Mapped[Optional[int]]

    __table_args__ = (
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    last_activity_at: Mapped[datetime] = mapped_column("lastActivityAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    company: Mapped[Optional[str]]
    onboarding_categories: Mapped[dict[str, Any]] = mapped_column("onboardingCategories", JSONB)
    referral: Mapped[Optional[str]]
    graph_navigation: Mapped[Optional[str]] = mapped_column("graphNavigation", Enum("MOUSE", "TRACKPAD", name="GraphNavigation"))
    preferred_app_appearance: Mapped[Optional[str]] = mapped_column("preferredAppAppearance")
    displayed_in_app_notifications: Mapped[Optional[dict[str, Any]]] = mapped_column("displayedInAppNotifications", JSONB(none_as_null=True))
    group_titles_auto_generation: Mapped[Optional[dict[str, Any]]] = mapped_column("groupTitlesAutoGeneration", JSONB(none_as_null=True))
    preferred_language: Mapped[Optional[str]] = mapped_column("preferredLanguage")
    terms_accepted_at: Mapped[Optional[datetime]] = mapped_column("termsAcceptedAt")

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_tokens: Mapped[list["ApiToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    collaborators_on_typebots: Mapped[list["CollaboratorsOnTypebots"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    workspaces: Mapped[list["MemberInWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    banned_ips: Mapped[list["BannedIp"]] = relationship(passive_deletes="all", back_populates="user")
    credentials: Mapped[list["UserCredentials"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class ApiToken(Base):
    __tablename__ = "ApiToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    token: Mapped[str]
    name: Mapped[str]
    owner_id: Mapped[str] = mapped_column("ownerId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiToken_ownerId_fkey"))

    __table_args__ = (
        Index("ApiToken_token_key", "token", unique=True),
    )

    owner: Mapped["User"] = relationship(back_populates="api_tokens")

class Workspace(Base):
    __tablename__ = "Workspace"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    icon: Mapped[Optional[str]]
    plan: Mapped[str] = mapped_column(Enum("FREE", "STARTER", "PRO", "LIFETIME", "OFFERED", "CUSTOM", "UNLIMITED", "ENTERPRISE", name="Plan"), default="FREE", server_default=text("'FREE'"))
    stripe_id: Mapped[Optional[str]] = mapped_column("stripeId")
    additional_chats_index: Mapped[int] = mapped_column("additionalChatsIndex", default=0, server_default=text("0"))
    additional_storage_index: Mapped[int] = mapped_column("additionalStorageIndex", default=0, server_default=text("0"))
    chats_limit_first_email_sent_at: Mapped[Optional[datetime]] = mapped_column("chatsLimitFirstEmailSentAt")
    storage_limit_first_email_sent_at: Mapped[Optional[datetime]] = mapped_column("storageLimitFirstEmailSentAt")
    chats_limit_second_email_sent_at: Mapped[Optional[datetime]] = mapped_column("chatsLimitSecondEmailSentAt")
    storage_limit_second_email_sent_at: Mapped[Optional[datetime]] = mapped_column("storageLimitSecondEmailSentAt")
    custom_chats_limit: Mapped[Optional[int]] = mapped_column("customChatsLimit")
    custom_storage_limit: Mapped[Optional[int]] = mapped_column("customStorageLimit")
    custom_seats_limit: Mapped[Optional[int]] = mapped_column("customSeatsLimit")
    is_quarantined: Mapped[bool] = mapped_column("isQuarantined", default=False, server_default=text("false"))
    is_suspended: Mapped[bool] = mapped_column("isSuspended", default=False, server_default=text("false"))
    is_past_due: Mapped[bool] = mapped_column("isPastDue", default=False, server_default=text("false"))
    is_verified: Mapped[Optional[bool]] = mapped_column("isVerified")
    chats_hard_limit: Mapped[Optional[int]] = mapped_column("chatsHardLimit")
    settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_activity_at: Mapped[Optional[datetime]] = mapped_column("lastActivityAt")

    __table_args__ = (
        Index("Workspace_stripeId_key", "stripeId", unique=True),
    )

    credentials: Mapped[list["Credentials"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    custom_domains: Mapped[list["CustomDomain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    folders: Mapped[list["DashboardFolder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    members: Mapped[list["MemberInWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    typebots: Mapped[list["Typebot"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    invitations: Mapped[list["WorkspaceInvitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    theme_templates: Mapped[list["ThemeTemplate"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    spaces: Mapped[list["Space"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    claimable_custom_plan: Mapped[Optional["ClaimableCustomPlan"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")

class Space(Base):
    __tablename__ = "Space"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    icon: Mapped[Optional[str]]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Space_workspaceId_fkey"))

    __table_args__ = (
        Index("Space_id_workspaceId_key", "id", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="spaces")
    typebots: Mapped[list["Typebot"]] = relationship(back_populates="space")

class WorkspaceInvitation(Base):
    __tablename__ = "WorkspaceInvitation"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    email: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkspaceInvitation_workspaceId_fkey"))
    type: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", "GUEST", name="WorkspaceRole"))

    workspace: Mapped["Workspace"] = relationship(back_populates="invitations")

class CustomDomain(Base):
    __tablename__ = "CustomDomain"

    name: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomDomain_workspaceId_fkey"))

    workspace: Mapped["Workspace"] = relationship(back_populates="custom_domains")

class Credentials(Base):
    __tablename__ = "Credentials"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Credentials_workspaceId_fkey"))
    data: Mapped[str]
    name: Mapped[str]
    type: Mapped[str]
    iv: Mapped[str]

    workspace: Mapped["Workspace"] = relationship(back_populates="credentials")

class UserCredentials(Base):
    __tablename__ = "UserCredentials"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UserCredentials_userId_fkey"))
    data: Mapped[str]
    name: Mapped[str]
    type: Mapped[str]
    iv: Mapped[str]

    user: Mapped["User"] = relationship(back_populates="credentials")

class DashboardFolder(Base):
    __tablename__ = "DashboardFolder"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    parent_folder_id: Mapped[Optional[str]] = mapped_column("parentFolderId", ForeignKey("DashboardFolder.id", ondelete="SET NULL", onupdate="CASCADE", name="DashboardFolder_parentFolderId_fkey"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="DashboardFolder_workspaceId_fkey"))

    parent_folder: Mapped[Optional["DashboardFolder"]] = relationship(remote_side=[id], back_populates="children_folder")
    workspace: Mapped["Workspace"] = relationship(back_populates="folders")
    children_folder: Mapped[list["DashboardFolder"]] = relationship(back_populates="parent_folder")
    typebots: Mapped[list["Typebot"]] = relationship(back_populates="folder")

class Typebot(Base):
    __tablename__ = "Typebot"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    icon: Mapped[Optional[str]]
    name: Mapped[str]
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("DashboardFolder.id", ondelete="SET NULL", onupdate="CASCADE", name="Typebot_folderId_fkey"))
    groups: Mapped[dict[str, Any]] = mapped_column(JSONB)
    events: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB)
    edges: Mapped[dict[str, Any]] = mapped_column(JSONB)
    theme: Mapped[dict[str, Any]] = mapped_column(JSONB)
    selected_theme_template_id: Mapped[Optional[str]] = mapped_column("selectedThemeTemplateId")
    settings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    public_id: Mapped[Optional[str]] = mapped_column("publicId")
    custom_domain: Mapped[Optional[str]] = mapped_column("customDomain")
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Typebot_workspaceId_fkey"))
    results_table_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("resultsTablePreferences", JSONB(none_as_null=True))
    is_archived: Mapped[bool] = mapped_column("isArchived", default=False, server_default=text("false"))
    is_closed: Mapped[bool] = mapped_column("isClosed", default=False, server_default=text("false"))
    whats_app_credentials_id: Mapped[Optional[str]] = mapped_column("whatsAppCredentialsId")
    risk_level: Mapped[Optional[int]] = mapped_column("riskLevel")
    space_id: Mapped[Optional[str]] = mapped_column("spaceId", ForeignKey("Space.id", ondelete="SET NULL", onupdate="CASCADE", name="Typebot_spaceId_fkey"))

    __table_args__ = (
        Index("Typebot_publicId_key", "publicId", unique=True),
        Index("Typebot_customDomain_key", "customDomain", unique=True),
        Index("Typebot_workspaceId_idx", "workspaceId"),
        Index("Typebot_isArchived_createdAt_idx", "isArchived", "createdAt"),
    )

    folder: Mapped[Optional["DashboardFolder"]] = relationship(back_populates="typebots")
    workspace: Mapped["Workspace"] = relationship(back_populates="typebots")
    space: Mapped[Optional["Space"]] = relationship(back_populates="typebots")
    collaborators: Mapped[list["CollaboratorsOnTypebots"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="typebot")
    invitations: Mapped[list["Invitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="typebot")
    results: Mapped[list["Result"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="typebot")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="typebot")
    banned_ips: Mapped[list["BannedIp"]] = relationship(passive_deletes="all", back_populates="responsible_typebot")
    published_typebot: Mapped[Optional["PublicTypebot"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="typebot")

class PublicTypebot(Base):
    __tablename__ = "PublicTypebot"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_activity_at: Mapped[Optional[datetime]] = mapped_column("lastActivityAt")
    typebot_id: Mapped[str] = mapped_column("typebotId", ForeignKey("Typebot.id", ondelete="CASCADE", onupdate="CASCADE", name="PublicTypebot_typebotId_fkey"))
    groups: Mapped[dict[str, Any]] = mapped_column(JSONB)
    events: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB)
    edges: Mapped[dict[str, Any]] = mapped_column(JSONB)
    theme: Mapped[dict[str, Any]] = mapped_column(JSONB)
    settings: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("PublicTypebot_typebotId_key", "typebotId", unique=True),
        Index("PublicTypebot_lastActivityAt_idx", "lastActivityAt"),
    )

    typebot: Mapped["Typebot"] = relationship(back_populates="published_typebot")
    media_ids_cache: Mapped[list["RuntimeMediaIdCache"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="public_typebot")

class Result(Base):
    __tablename__ = "Result"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    typebot_id: Mapped[str] = mapped_column("typebotId", ForeignKey("Typebot.id", ondelete="CASCADE", onupdate="CASCADE", name="Result_typebotId_fkey"))
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB)
    is_completed: Mapped[bool] = mapped_column("isCompleted")
    has_started: Mapped[Optional[bool]] = mapped_column("hasStarted")
    is_archived: Mapped[Optional[bool]] = mapped_column("isArchived", default=False, server_default=text("false"))
    last_chat_session_id: Mapped[Optional[str]] = mapped_column("lastChatSessionId")

    __table_args__ = (
        Index("Result_typebotId_hasStarted_createdAt_idx", "typebotId", "hasStarted", "createdAt"),
        Index("Result_typebotId_isCompleted_idx", "typebotId", "isCompleted"),
    )

    typebot: Mapped["Typebot"] = relationship(back_populates="results")
    answers: Mapped[list["Answer"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="result")
    answers_v2: Mapped[list["AnswerV2"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="result")
    logs: Mapped[list["Log"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="result")
    edges: Mapped[list["VisitedEdge"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="result")
    set_variable_history: Mapped[list["SetVariableHistoryItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="result")

class Log(Base):
    __tablename__ = "Log"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    result_id: Mapped[str] = mapped_column("resultId", ForeignKey("Result.id", ondelete="CASCADE", onupdate="CASCADE", name="Log_resultId_fkey"))
    status: Mapped[str]
    context: Mapped[Optional[str]]
    description: Mapped[str]
    details: Mapped[Optional[str]]

    __table_args__ = (
        Index("Log_resultId_idx", "resultId"),
    )

    result: Mapped["Result"] = relationship(back_populates="logs")

class AnswerV2(Base):
    __tablename__ = "AnswerV2"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    block_id: Mapped[str] = mapped_column("blockId")
    content: Mapped[str]
    attached_file_urls: Mapped[Optional[dict[str, Any]]] = mapped_column("attachedFileUrls", JSONB(none_as_null=True))
    result_id: Mapped[str] = mapped_column("resultId", ForeignKey("Result.id", ondelete="CASCADE", onupdate="CASCADE", name="AnswerV2_resultId_fkey"))

    __table_args__ = (
        Index("AnswerV2_blockId_idx", "blockId"),
    )

    result: Mapped["Result"] = relationship(back_populates="answers_v2")

class Coupon(Base):
    __tablename__ = "Coupon"

    user_properties_to_update: Mapped[dict[str, Any]] = mapped_column("userPropertiesToUpdate", JSONB)
    code: Mapped[str] = mapped_column(primary_key=True)
    date_redeemed: Mapped[Optional[datetime]] = mapped_column("dateRedeemed")

    __table_args__ = (
        Index("Coupon_code_key", "code", unique=True),
    )

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    url: Mapped[Optional[str]]
    method: Mapped[str]
    query_params: Mapped[dict[str, Any]] = mapped_column("queryParams", JSONB)
    headers: Mapped[dict[str, Any]] = mapped_column(JSONB)
    body: Mapped[Optional[str]]
    typebot_id: Mapped[str] = mapped_column("typebotId", ForeignKey("Typebot.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_typebotId_fkey"))

    typebot: Mapped["Typebot"] = relationship(back_populates="webhooks")

class ClaimableCustomPlan(Base):
    __tablename__ = "ClaimableCustomPlan"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    claimed_at: Mapped[Optional[datetime]] = mapped_column("claimedAt")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    price: Mapped[int]
    currency: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="ClaimableCustomPlan_workspaceId_fkey"))
    chats_limit: Mapped[int] = mapped_column("chatsLimit")
    storage_limit: Mapped[int] = mapped_column("storageLimit")
    seats_limit: Mapped[int] = mapped_column("seatsLimit")
    is_yearly: Mapped[bool] = mapped_column("isYearly", default=False, server_default=text("false"))
    company_name: Mapped[Optional[str]] = mapped_column("companyName")
    vat_type: Mapped[Optional[str]] = mapped_column("vatType")
    vat_value: Mapped[Optional[str]] = mapped_column("vatValue")

    __table_args__ = (
        Index("ClaimableCustomPlan_workspaceId_key", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="claimable_custom_plan")

class ChatSession(Base):
    __tablename__ = "ChatSession"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    state: Mapped[dict[str, Any]] = mapped_column(JSONB)
    is_replying: Mapped[Optional[bool]] = mapped_column("isReplying")

class ThemeTemplate(Base):
    __tablename__ = "ThemeTemplate"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    theme: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="ThemeTemplate_workspaceId_fkey"))

    workspace: Mapped["Workspace"] = relationship(back_populates="theme_templates")

class BannedIp(Base):
    __tablename__ = "BannedIp"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    ip: Mapped[str]
    responsible_typebot_id: Mapped[str] = mapped_column("responsibleTypebotId", ForeignKey("Typebot.id", ondelete="RESTRICT", onupdate="CASCADE", name="BannedIp_responsibleTypebotId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="BannedIp_userId_fkey"))

    __table_args__ = (
        Index("BannedIp_ip_key", "ip", unique=True),
    )

    responsible_typebot: Mapped["Typebot"] = relationship(back_populates="banned_ips")
    user: Mapped["User"] = relationship(back_populates="banned_ips")

class SuppressedEmail(Base):
    __tablename__ = "SuppressedEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    email: Mapped[str]
    transient_general_bounce_count: Mapped[int] = mapped_column("transientGeneralBounceCount", default=0, server_default=text("0"))
    suppressed_at: Mapped[Optional[datetime]] = mapped_column("suppressedAt")
    last_webhook_id: Mapped[Optional[str]] = mapped_column("lastWebhookId")

    __table_args__ = (
        Index("SuppressedEmail_email_key", "email", unique=True),
    )
