from sqlalchemy import Dialect, ForeignKey, Index, REAL, String, Text, TypeDecorator, text
from sqlalchemy.ext.compiler import compiles
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl = String
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[str]:
        if value is None:
            return None
        aware = value if value.tzinfo else value.replace(tzinfo=timezone.utc)
        return aware.astimezone(timezone.utc).isoformat(timespec="milliseconds")

    def process_result_value(self, value: Optional[str], dialect: Dialect) -> Optional[datetime]:
        if value is None:
            return None
        read = datetime.fromisoformat(value)
        return read.replace(tzinfo=timezone.utc) if read.tzinfo is None else read.astimezone(timezone.utc)


@compiles(UtcDateTime)
def utc_date_time_ddl(type_: UtcDateTime, compiler: Any, **kw: Any) -> str:
    return "DATETIME"


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    type_annotation_map = {datetime: UtcDateTime, str: Text, float: REAL}


class ApiKeys(Base):
    __tablename__ = "api_keys"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[Optional[str]]
    secret: Mapped[Optional[str]]
    created_by: Mapped[Optional[int]] = mapped_column("createdBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("api_keys_secret_key", "secret", unique=True),
        {"sqlite_autoincrement": True},
    )

class WorkspaceDocuments(Base):
    __tablename__ = "workspace_documents"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    doc_id: Mapped[str] = mapped_column("docId")
    filename: Mapped[str]
    docpath: Mapped[str]
    workspace_id: Mapped[int] = mapped_column("workspaceId", ForeignKey("workspaces.id", ondelete="RESTRICT", onupdate="CASCADE", name="workspace_documents_workspaceId_fkey"))
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    pinned: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    watched: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("workspace_documents_docId_key", "docId", unique=True),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="documents")
    document_sync_queues: Mapped[Optional["DocumentSyncQueues"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace_doc")

class Invites(Base):
    __tablename__ = "invites"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    code: Mapped[str]
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    claimed_by: Mapped[Optional[int]] = mapped_column("claimedBy")
    workspace_ids: Mapped[Optional[str]] = mapped_column("workspaceIds")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[int] = mapped_column("createdBy")
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("invites_code_key", "code", unique=True),
        {"sqlite_autoincrement": True},
    )

class SystemSettings(Base):
    __tablename__ = "system_settings"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    label: Mapped[str]
    value: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("system_settings_label_key", "label", unique=True),
        {"sqlite_autoincrement": True},
    )

class Users(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    username: Mapped[Optional[str]]
    password: Mapped[str]
    pfp_filename: Mapped[Optional[str]] = mapped_column("pfpFilename")
    role: Mapped[str] = mapped_column(default="default", server_default=text("'default'"))
    suspended: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    seen_recovery_codes: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    daily_message_limit: Mapped[Optional[int]] = mapped_column("dailyMessageLimit")
    bio: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    web_push_subscription_config: Mapped[Optional[str]]

    __table_args__ = (
        Index("users_username_key", "username", unique=True),
        {"sqlite_autoincrement": True},
    )

    workspace_chats: Mapped[list["WorkspaceChats"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="users")
    workspace_users: Mapped[list["WorkspaceUsers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="users")
    embed_configs: Mapped[list["EmbedConfigs"]] = relationship(back_populates="users")
    embed_chats: Mapped[list["EmbedChats"]] = relationship(back_populates="users")
    threads: Mapped[list["WorkspaceThreads"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    recovery_codes: Mapped[list["RecoveryCodes"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    password_reset_tokens: Mapped[list["PasswordResetTokens"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    workspace_agent_invocations: Mapped[list["WorkspaceAgentInvocations"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    slash_command_presets: Mapped[list["SlashCommandPresets"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    browser_extension_api_keys: Mapped[list["BrowserExtensionApiKeys"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    temporary_auth_tokens: Mapped[list["TemporaryAuthTokens"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    system_prompt_variables: Mapped[list["SystemPromptVariables"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    prompt_history: Mapped[list["PromptHistory"]] = relationship(back_populates="user")
    desktop_mobile_devices: Mapped[list["DesktopMobileDevices"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    workspace_parsed_files: Mapped[list["WorkspaceParsedFiles"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    memories: Mapped[list["Memories"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class RecoveryCodes(Base):
    __tablename__ = "recovery_codes"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="recovery_codes_user_id_fkey"))
    code_hash: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("recovery_codes_user_id_idx", "user_id"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped["Users"] = relationship(back_populates="recovery_codes")

class PasswordResetTokens(Base):
    __tablename__ = "password_reset_tokens"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="password_reset_tokens_user_id_fkey"))
    token: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("password_reset_tokens_token_key", "token", unique=True),
        Index("password_reset_tokens_user_id_idx", "user_id"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped["Users"] = relationship(back_populates="password_reset_tokens")

class DocumentVectors(Base):
    __tablename__ = "document_vectors"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    doc_id: Mapped[str] = mapped_column("docId")
    vector_id: Mapped[str] = mapped_column("vectorId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

class Workspaces(Base):
    __tablename__ = "workspaces"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    slug: Mapped[str]
    vector_tag: Mapped[Optional[str]] = mapped_column("vectorTag")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    open_ai_temp: Mapped[Optional[float]] = mapped_column("openAiTemp")
    open_ai_history: Mapped[int] = mapped_column("openAiHistory", default=20, server_default=text("20"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    open_ai_prompt: Mapped[Optional[str]] = mapped_column("openAiPrompt")
    similarity_threshold: Mapped[Optional[float]] = mapped_column("similarityThreshold", default=0.25, server_default=text("0.25"))
    chat_provider: Mapped[Optional[str]] = mapped_column("chatProvider")
    chat_model: Mapped[Optional[str]] = mapped_column("chatModel")
    top_n: Mapped[Optional[int]] = mapped_column("topN", default=4, server_default=text("4"))
    chat_mode: Mapped[Optional[str]] = mapped_column("chatMode", default="chat", server_default=text("'chat'"))
    pfp_filename: Mapped[Optional[str]] = mapped_column("pfpFilename")
    agent_provider: Mapped[Optional[str]] = mapped_column("agentProvider")
    agent_model: Mapped[Optional[str]] = mapped_column("agentModel")
    query_refusal_response: Mapped[Optional[str]] = mapped_column("queryRefusalResponse")
    vector_search_mode: Mapped[Optional[str]] = mapped_column("vectorSearchMode", default="default", server_default=text("'default'"))
    router_id: Mapped[Optional[int]]

    __table_args__ = (
        Index("workspaces_slug_key", "slug", unique=True),
        {"sqlite_autoincrement": True},
    )

    workspace_users: Mapped[list["WorkspaceUsers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspaces")
    documents: Mapped[list["WorkspaceDocuments"]] = relationship(passive_deletes="all", back_populates="workspace")
    workspace_suggested_messages: Mapped[list["WorkspaceSuggestedMessages"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    embed_configs: Mapped[list["EmbedConfigs"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    threads: Mapped[list["WorkspaceThreads"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    workspace_agent_invocations: Mapped[list["WorkspaceAgentInvocations"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    prompt_history: Mapped[list["PromptHistory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    workspace_parsed_files: Mapped[list["WorkspaceParsedFiles"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    memories: Mapped[list["Memories"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")

class WorkspaceThreads(Base):
    __tablename__ = "workspace_threads"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    slug: Mapped[str]
    workspace_id: Mapped[int] = mapped_column(ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_threads_workspace_id_fkey"))
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_threads_user_id_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("workspace_threads_slug_key", "slug", unique=True),
        Index("workspace_threads_workspace_id_idx", "workspace_id"),
        Index("workspace_threads_user_id_idx", "user_id"),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="threads")
    user: Mapped[Optional["Users"]] = relationship(back_populates="threads")
    workspace_parsed_files: Mapped[list["WorkspaceParsedFiles"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="thread")

class WorkspaceSuggestedMessages(Base):
    __tablename__ = "workspace_suggested_messages"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    workspace_id: Mapped[int] = mapped_column("workspaceId", ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_suggested_messages_workspaceId_fkey"))
    heading: Mapped[str]
    message: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("workspace_suggested_messages_workspaceId_idx", "workspaceId"),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="workspace_suggested_messages")

class WorkspaceChats(Base):
    __tablename__ = "workspace_chats"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    workspace_id: Mapped[int] = mapped_column("workspaceId")
    prompt: Mapped[str]
    response: Mapped[str]
    include: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_chats_user_id_fkey"))
    thread_id: Mapped[Optional[int]]
    api_session_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    feedback_score: Mapped[Optional[bool]] = mapped_column("feedbackScore")
    memory_processed: Mapped[Optional[bool]]

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

    users: Mapped[Optional["Users"]] = relationship(back_populates="workspace_chats")

class WorkspaceAgentInvocations(Base):
    __tablename__ = "workspace_agent_invocations"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    prompt: Mapped[str]
    closed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_agent_invocations_user_id_fkey"))
    thread_id: Mapped[Optional[int]]
    workspace_id: Mapped[int] = mapped_column(ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_agent_invocations_workspace_id_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("workspace_agent_invocations_uuid_key", "uuid", unique=True),
        Index("workspace_agent_invocations_uuid_idx", "uuid"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="workspace_agent_invocations")
    workspace: Mapped["Workspaces"] = relationship(back_populates="workspace_agent_invocations")

class WorkspaceUsers(Base):
    __tablename__ = "workspace_users"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_users_user_id_fkey"))
    workspace_id: Mapped[int] = mapped_column(ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_users_workspace_id_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

    workspaces: Mapped["Workspaces"] = relationship(back_populates="workspace_users")
    users: Mapped["Users"] = relationship(back_populates="workspace_users")

class CacheData(Base):
    __tablename__ = "cache_data"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    data: Mapped[str]
    belongs_to: Mapped[Optional[str]] = mapped_column("belongsTo")
    by_id: Mapped[Optional[int]] = mapped_column("byId")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

class EmbedConfigs(Base):
    __tablename__ = "embed_configs"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    chat_mode: Mapped[str] = mapped_column(default="query", server_default=text("'query'"))
    allowlist_domains: Mapped[Optional[str]]
    allow_model_override: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    allow_temperature_override: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    allow_prompt_override: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    max_chats_per_day: Mapped[Optional[int]]
    max_chats_per_session: Mapped[Optional[int]]
    message_limit: Mapped[Optional[int]] = mapped_column(default=20, server_default=text("20"))
    workspace_id: Mapped[int] = mapped_column(ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="embed_configs_workspace_id_fkey"))
    created_by: Mapped[Optional[int]] = mapped_column("createdBy")
    users_id: Mapped[Optional[int]] = mapped_column("usersId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="embed_configs_usersId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("embed_configs_uuid_key", "uuid", unique=True),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="embed_configs")
    users: Mapped[Optional["Users"]] = relationship(back_populates="embed_configs")
    embed_chats: Mapped[list["EmbedChats"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="embed_config")

class EmbedChats(Base):
    __tablename__ = "embed_chats"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    prompt: Mapped[str]
    response: Mapped[str]
    session_id: Mapped[str]
    include: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    connection_information: Mapped[Optional[str]]
    embed_id: Mapped[int] = mapped_column(ForeignKey("embed_configs.id", ondelete="CASCADE", onupdate="CASCADE", name="embed_chats_embed_id_fkey"))
    users_id: Mapped[Optional[int]] = mapped_column("usersId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="embed_chats_usersId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

    embed_config: Mapped["EmbedConfigs"] = relationship(back_populates="embed_chats")
    users: Mapped[Optional["Users"]] = relationship(back_populates="embed_chats")

class EventLogs(Base):
    __tablename__ = "event_logs"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    event: Mapped[str]
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    user_id: Mapped[Optional[int]] = mapped_column("userId")
    occurred_at: Mapped[datetime] = mapped_column("occurredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("event_logs_event_idx", "event"),
        {"sqlite_autoincrement": True},
    )

class SlashCommandPresets(Base):
    __tablename__ = "slash_command_presets"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    command: Mapped[str]
    prompt: Mapped[str]
    description: Mapped[str]
    uid: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="slash_command_presets_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("slash_command_presets_uid_command_key", "uid", "command", unique=True),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="slash_command_presets")

class DocumentSyncQueues(Base):
    __tablename__ = "document_sync_queues"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    stale_after_ms: Mapped[int] = mapped_column("staleAfterMs", default=604800000, server_default=text("604800000"))
    next_sync_at: Mapped[datetime] = mapped_column("nextSyncAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_synced_at: Mapped[datetime] = mapped_column("lastSyncedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    workspace_doc_id: Mapped[int] = mapped_column("workspaceDocId", ForeignKey("workspace_documents.id", ondelete="CASCADE", onupdate="CASCADE", name="document_sync_queues_workspaceDocId_fkey"))

    __table_args__ = (
        Index("document_sync_queues_workspaceDocId_key", "workspaceDocId", unique=True),
        {"sqlite_autoincrement": True},
    )

    workspace_doc: Mapped[Optional["WorkspaceDocuments"]] = relationship(back_populates="document_sync_queues")
    runs: Mapped[list["DocumentSyncExecutions"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="queue")

class DocumentSyncExecutions(Base):
    __tablename__ = "document_sync_executions"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    queue_id: Mapped[int] = mapped_column("queueId", ForeignKey("document_sync_queues.id", ondelete="CASCADE", onupdate="CASCADE", name="document_sync_executions_queueId_fkey"))
    status: Mapped[str] = mapped_column(default="unknown", server_default=text("'unknown'"))
    result: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

    queue: Mapped["DocumentSyncQueues"] = relationship(back_populates="runs")

class BrowserExtensionApiKeys(Base):
    __tablename__ = "browser_extension_api_keys"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str]
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="browser_extension_api_keys_user_id_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("browser_extension_api_keys_key_key", "key", unique=True),
        Index("browser_extension_api_keys_user_id_idx", "user_id"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="browser_extension_api_keys")

class TemporaryAuthTokens(Base):
    __tablename__ = "temporary_auth_tokens"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    token: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="temporary_auth_tokens_userId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("temporary_auth_tokens_token_key", "token", unique=True),
        Index("temporary_auth_tokens_token_idx", "token"),
        Index("temporary_auth_tokens_userId_idx", "userId"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped["Users"] = relationship(back_populates="temporary_auth_tokens")

class SystemPromptVariables(Base):
    __tablename__ = "system_prompt_variables"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str]
    value: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(default="system", server_default=text("'system'"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="system_prompt_variables_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("system_prompt_variables_key_key", "key", unique=True),
        Index("system_prompt_variables_userId_idx", "userId"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="system_prompt_variables")

class PromptHistory(Base):
    __tablename__ = "prompt_history"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    workspace_id: Mapped[int] = mapped_column("workspaceId", ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="prompt_history_workspaceId_fkey"))
    prompt: Mapped[str]
    modified_by: Mapped[Optional[int]] = mapped_column("modifiedBy", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="prompt_history_modifiedBy_fkey"))
    modified_at: Mapped[datetime] = mapped_column("modifiedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("prompt_history_workspaceId_idx", "workspaceId"),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="prompt_history")
    user: Mapped[Optional["Users"]] = relationship(back_populates="prompt_history")

class DesktopMobileDevices(Base):
    __tablename__ = "desktop_mobile_devices"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    device_os: Mapped[str] = mapped_column("deviceOs")
    device_name: Mapped[str] = mapped_column("deviceName")
    token: Mapped[str]
    approved: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="desktop_mobile_devices_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("desktop_mobile_devices_token_key", "token", unique=True),
        Index("desktop_mobile_devices_userId_idx", "userId"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="desktop_mobile_devices")

class WorkspaceParsedFiles(Base):
    __tablename__ = "workspace_parsed_files"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    filename: Mapped[str]
    workspace_id: Mapped[int] = mapped_column("workspaceId", ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_parsed_files_workspaceId_fkey"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_parsed_files_userId_fkey"))
    thread_id: Mapped[Optional[int]] = mapped_column("threadId", ForeignKey("workspace_threads.id", ondelete="CASCADE", onupdate="CASCADE", name="workspace_parsed_files_threadId_fkey"))
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    token_count_estimate: Mapped[Optional[int]] = mapped_column("tokenCountEstimate", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("workspace_parsed_files_filename_key", "filename", unique=True),
        Index("workspace_parsed_files_workspaceId_idx", "workspaceId"),
        Index("workspace_parsed_files_userId_idx", "userId"),
        {"sqlite_autoincrement": True},
    )

    workspace: Mapped["Workspaces"] = relationship(back_populates="workspace_parsed_files")
    user: Mapped[Optional["Users"]] = relationship(back_populates="workspace_parsed_files")
    thread: Mapped[Optional["WorkspaceThreads"]] = relationship(back_populates="workspace_parsed_files")

class ExternalCommunicationConnectors(Base):
    __tablename__ = "external_communication_connectors"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    type: Mapped[str]
    config: Mapped[str] = mapped_column(default="{}", server_default=text("'{}'"))
    active: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("external_communication_connectors_type_key", "type", unique=True),
        {"sqlite_autoincrement": True},
    )

class ScheduledJobs(Base):
    __tablename__ = "scheduled_jobs"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    prompt: Mapped[str]
    tools: Mapped[Optional[str]]
    schedule: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    last_run_at: Mapped[Optional[datetime]] = mapped_column("lastRunAt")
    next_run_at: Mapped[Optional[datetime]] = mapped_column("nextRunAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

    runs: Mapped[list["ScheduledJobRuns"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="job")

class ScheduledJobRuns(Base):
    __tablename__ = "scheduled_job_runs"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    job_id: Mapped[int] = mapped_column("jobId", ForeignKey("scheduled_jobs.id", ondelete="CASCADE", onupdate="CASCADE", name="scheduled_job_runs_jobId_fkey"))
    status: Mapped[str] = mapped_column(default="queued", server_default=text("'queued'"))
    result: Mapped[Optional[str]]
    error: Mapped[Optional[str]]
    started_at: Mapped[datetime] = mapped_column("startedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    read_at: Mapped[Optional[datetime]] = mapped_column("readAt")

    __table_args__ = (
        Index("scheduled_job_runs_jobId_idx", "jobId"),
        {"sqlite_autoincrement": True},
    )

    job: Mapped["ScheduledJobs"] = relationship(back_populates="runs")

class Memories(Base):
    __tablename__ = "memories"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="memories_user_id_fkey"))
    workspace_id: Mapped[Optional[int]] = mapped_column(ForeignKey("workspaces.id", ondelete="CASCADE", onupdate="CASCADE", name="memories_workspace_id_fkey"))
    scope: Mapped[str] = mapped_column(default="workspace", server_default=text("'workspace'"))
    content: Mapped[str]
    last_used_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("memories_user_id_workspace_id_idx", "user_id", "workspace_id"),
        Index("memories_user_id_scope_idx", "user_id", "scope"),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="memories")
    workspace: Mapped[Optional["Workspaces"]] = relationship(back_populates="memories")

class ModelRouters(Base):
    __tablename__ = "model_routers"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    fallback_provider: Mapped[str]
    fallback_model: Mapped[str]
    cooldown_seconds: Mapped[int] = mapped_column(default=30, server_default=text("30"))
    created_by: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("model_routers_name_key", "name", unique=True),
        {"sqlite_autoincrement": True},
    )

    rules: Mapped[list["ModelRouterRules"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="router")

class ModelRouterRules(Base):
    __tablename__ = "model_router_rules"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    router_id: Mapped[int] = mapped_column(ForeignKey("model_routers.id", ondelete="CASCADE", onupdate="CASCADE", name="model_router_rules_router_id_fkey"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    priority: Mapped[int]
    type: Mapped[str] = mapped_column(default="calculated", server_default=text("'calculated'"))
    title: Mapped[str]
    description: Mapped[Optional[str]]
    condition_logic: Mapped[Optional[str]]
    conditions: Mapped[Optional[str]]
    route_provider: Mapped[str]
    route_model: Mapped[str]
    created_by: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("model_router_rules_router_id_idx", "router_id"),
        Index("model_router_rules_router_id_enabled_priority_idx", "router_id", "enabled", "priority"),
        Index("model_router_rules_router_id_title_key", "router_id", "title", unique=True),
        {"sqlite_autoincrement": True},
    )

    router: Mapped["ModelRouters"] = relationship(back_populates="rules")
