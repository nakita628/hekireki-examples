from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, Index, MetaData, String, Text, TypeDecorator, Uuid, text
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


class EmailLog(Base):
    __tablename__ = "EmailLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    email: Mapped[dict[str, Any]] = mapped_column(JSONB)
    error: Mapped[Optional[str]]
    preview_url: Mapped[Optional[str]] = mapped_column("previewUrl")
    message_id: Mapped[Optional[str]] = mapped_column("messageId")
    status: Mapped[str]

class UserPreferences(Base):
    __tablename__ = "UserPreferences"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("UserProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="UserPreferences_userId_fkey"))
    workspace_id: Mapped[Optional[str]] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="SET NULL", onupdate="CASCADE", name="UserPreferences_workspaceId_fkey"))
    scope: Mapped[Optional[str]]
    preferences: Mapped[dict[str, Any]] = mapped_column(JSONB)

    user: Mapped["UserProfile"] = relationship(back_populates="user_preferences")
    workspace: Mapped[Optional["Workspace"]] = relationship(back_populates="preferences")

class UserProfile(Base):
    __tablename__ = "UserProfile"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    email: Mapped[str]
    admin: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    login_provider: Mapped[str] = mapped_column("loginProvider")
    external_username: Mapped[Optional[str]] = mapped_column("externalUsername")
    external_id: Mapped[str] = mapped_column("externalId")

    __table_args__ = (
        Index("UserProfile_externalId_idx", "externalId"),
        Index("UserProfile_loginProvider_externalId_key", "loginProvider", "externalId", unique=True),
    )

    tokens: Mapped[list["UserApiToken"]] = relationship(passive_deletes="all", back_populates="user")
    workspace_access: Mapped[list["WorkspaceAccess"]] = relationship(passive_deletes="all", back_populates="user")
    user_preferences: Mapped[list["UserPreferences"]] = relationship(passive_deletes="all", back_populates="user")
    workspace_user_properties: Mapped[list["WorkspaceUserProperties"]] = relationship(passive_deletes="all", back_populates="user")
    password: Mapped[Optional["UserPassword"]] = relationship(passive_deletes="all", back_populates="user")

class UserPassword(Base):
    __tablename__ = "UserPassword"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("UserProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="UserPassword_userId_fkey"))
    hash: Mapped[str]
    change_at_next_login: Mapped[bool] = mapped_column("changeAtNextLogin", default=False, server_default=text("false"))

    __table_args__ = (
        Index("UserPassword_userId_key", "userId", unique=True),
    )

    user: Mapped["UserProfile"] = relationship(back_populates="password")

class Workspace(Base):
    __tablename__ = "Workspace"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    slug: Mapped[Optional[str]]
    features_enabled: Mapped[list[str]] = mapped_column("featuresEnabled", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("Workspace_slug_idx", "slug"),
        Index("Workspace_updatedAt_idx", "updatedAt"),
        Index("Workspace_slug_key", "slug", unique=True),
    )

    workspace_access: Mapped[list["WorkspaceAccess"]] = relationship(passive_deletes="all", back_populates="workspace")
    workspace_options: Mapped[list["WorkspaceOptions"]] = relationship(passive_deletes="all", back_populates="workspace")
    configuration_object: Mapped[list["ConfigurationObject"]] = relationship(passive_deletes="all", back_populates="workspace")
    configuration_object_link: Mapped[list["ConfigurationObjectLink"]] = relationship(passive_deletes="all", back_populates="workspace")
    preferences: Mapped[list["UserPreferences"]] = relationship(back_populates="workspace")
    workspace_user_properties: Mapped[list["WorkspaceUserProperties"]] = relationship(passive_deletes="all", back_populates="workspace")
    profile_builders: Mapped[list["ProfileBuilder"]] = relationship(passive_deletes="all", back_populates="workspace")
    oidc_login_groups: Mapped[list["OidcLoginGroup"]] = relationship(passive_deletes="all", back_populates="workspace")

class WorkspaceOptions(Base):
    __tablename__ = "WorkspaceOptions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkspaceOptions_workspaceId_fkey"))
    namespace: Mapped[str]
    value: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("WorkspaceOptions_workspaceId_namespace_idx", "workspaceId", "namespace"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="workspace_options")

class WorkspaceAccess(Base):
    __tablename__ = "WorkspaceAccess"

    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkspaceAccess_workspaceId_fkey"), primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("UserProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkspaceAccess_userId_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("owner", "editor", "analyst", name="WorkspaceRole"), default="owner", server_default=text("'owner'"))

    workspace: Mapped["Workspace"] = relationship(back_populates="workspace_access")
    user: Mapped["UserProfile"] = relationship(back_populates="workspace_access")

class WorkspaceUserProperties(Base):
    __tablename__ = "WorkspaceUserProperties"

    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkspaceUserProperties_workspaceId_fkey"), primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("UserProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="WorkspaceUserProperties_userId_fkey"), primary_key=True)
    last_used: Mapped[datetime] = mapped_column("lastUsed", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    workspace: Mapped["Workspace"] = relationship(back_populates="workspace_user_properties")
    user: Mapped["UserProfile"] = relationship(back_populates="workspace_user_properties")

class UserApiToken(Base):
    __tablename__ = "UserApiToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    last_used: Mapped[Optional[datetime]] = mapped_column("lastUsed")
    hint: Mapped[str]
    hash: Mapped[str]
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("UserProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="UserApiToken_userId_fkey"))
    type: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    oauth_client_id: Mapped[Optional[str]] = mapped_column("oauthClientId", ForeignKey("OAuthClient.id", ondelete="SET NULL", onupdate="CASCADE", name="UserApiToken_oauthClientId_fkey"))

    user: Mapped["UserProfile"] = relationship(back_populates="tokens")
    oauth_client: Mapped[Optional["OAuthClient"]] = relationship(back_populates="tokens")
    access_tokens: Mapped[list["OAuthAccessToken"]] = relationship(passive_deletes="all", back_populates="refresh_token")

class OAuthClient(Base):
    __tablename__ = "OAuthClient"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    client_secret_hash: Mapped[str] = mapped_column("clientSecretHash")
    name: Mapped[str]
    redirect_uris: Mapped[list[str]] = mapped_column("redirectUris", ARRAY(Text), nullable=True)

    tokens: Mapped[list["UserApiToken"]] = relationship(back_populates="oauth_client")

class KvStoreEntry(Base):
    __tablename__ = "kv"

    key: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[dict[str, Any]] = mapped_column(JSONB)
    expires_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("kv_expires_at_idx", "expires_at"),
        Index("kv_key_pattern_idx", "key"),
    )

class OAuthAccessToken(Base):
    __tablename__ = "OAuthAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_used: Mapped[Optional[datetime]] = mapped_column("lastUsed")
    hash: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    refresh_token_id: Mapped[str] = mapped_column("refreshTokenId", ForeignKey("UserApiToken.id", ondelete="RESTRICT", onupdate="CASCADE", name="OAuthAccessToken_refreshTokenId_fkey"))

    __table_args__ = (
        Index("OAuthAccessToken_expiresAt_idx", "expiresAt"),
        Index("OAuthAccessToken_refreshTokenId_idx", "refreshTokenId"),
    )

    refresh_token: Mapped["UserApiToken"] = relationship(back_populates="access_tokens")

class InvitationToken(Base):
    __tablename__ = "InvitationToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    email: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    token: Mapped[str]
    used_by: Mapped[Optional[str]] = mapped_column("usedBy")
    role: Mapped[str] = mapped_column(Enum("owner", "editor", "analyst", name="WorkspaceRole"), default="owner", server_default=text("'owner'"))

class ConfigurationObject(Base):
    __tablename__ = "ConfigurationObject"

    id: Mapped[str] = mapped_column(primary_key=True)
    deleted: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    type: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="ConfigurationObject_workspaceId_fkey"))
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("ConfigurationObject_updatedAt_idx", "updatedAt"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="configuration_object")
    from_links: Mapped[list["ConfigurationObjectLink"]] = relationship(foreign_keys="ConfigurationObjectLink.to_id", passive_deletes="all", back_populates="to")
    to_links: Mapped[list["ConfigurationObjectLink"]] = relationship(foreign_keys="ConfigurationObjectLink.from_id", passive_deletes="all", back_populates="from_")
    profile_builder: Mapped[list["ProfileBuilder"]] = relationship(back_populates="destination")
    profile_builder_function: Mapped[list["ProfileBuilderFunction"]] = relationship(passive_deletes="all", back_populates="function")

class ConfigurationObjectLink(Base):
    __tablename__ = "ConfigurationObjectLink"

    id: Mapped[str] = mapped_column(primary_key=True)
    deleted: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    type: Mapped[Optional[str]] = mapped_column(default="push", server_default=text("'push'"))
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="ConfigurationObjectLink_workspaceId_fkey"))
    from_id: Mapped[str] = mapped_column("fromId", ForeignKey("ConfigurationObject.id", ondelete="RESTRICT", onupdate="CASCADE", name="ConfigurationObjectLink_fromId_fkey"))
    to_id: Mapped[str] = mapped_column("toId", ForeignKey("ConfigurationObject.id", ondelete="RESTRICT", onupdate="CASCADE", name="ConfigurationObjectLink_toId_fkey"))

    __table_args__ = (
        Index("ConfigurationObjectLink_updatedAt_idx", "updatedAt"),
        Index("ConfigurationObjectLink_workspaceId_idx", "workspaceId"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="configuration_object_link")
    from_: Mapped["ConfigurationObject"] = relationship(foreign_keys=[from_id], back_populates="to_links")
    to: Mapped["ConfigurationObject"] = relationship(foreign_keys=[to_id], back_populates="from_links")

class ConnectorPackage(Base):
    __tablename__ = "ConnectorPackage"

    id: Mapped[str] = mapped_column(primary_key=True)
    package_id: Mapped[str] = mapped_column("packageId")
    package_type: Mapped[str] = mapped_column("packageType", default="airbyte", server_default=text("'airbyte'"))
    meta: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    logo_svg: Mapped[Optional[bytes]] = mapped_column("logoSvg")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class AuditLog(Base):
    __tablename__ = "AuditLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    timestamp: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    type: Mapped[str]
    severity: Mapped[Optional[str]] = mapped_column(default="info", server_default=text("'info'"))
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    workspace_id: Mapped[Optional[str]] = mapped_column("workspaceId")
    object_id: Mapped[Optional[str]] = mapped_column("objectId")
    changes: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    auth_type: Mapped[Optional[str]] = mapped_column("authType")
    token_id: Mapped[Optional[str]] = mapped_column("tokenId")
    ip: Mapped[Optional[str]]
    request_headers: Mapped[Optional[dict[str, Any]]] = mapped_column("requestHeaders", JSONB(none_as_null=True))

    __table_args__ = (
        Index("AuditLog_timestamp_idx", "timestamp"),
        Index("AuditLog_type_idx", "type"),
        Index("AuditLog_workspaceId_timestamp_idx", "workspaceId", "timestamp"),
    )

class GlobalProps(Base):
    __tablename__ = "GlobalProps"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    value: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

class SourceSpec(Base):
    __tablename__ = "source_spec"

    package: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[str] = mapped_column(primary_key=True)
    specs: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    error: Mapped[Optional[str]]

class SourceCheck(Base):
    __tablename__ = "source_check"

    package: Mapped[str]
    version: Mapped[str]
    key: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    description: Mapped[Optional[str]]
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class SourceCatalog(Base):
    __tablename__ = "source_catalog"

    package: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str] = mapped_column(primary_key=True)
    catalog: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    status: Mapped[str]
    description: Mapped[Optional[str]]

class ReverseSyncControl(Base):
    __tablename__ = "reverse_sync_control"

    workspace_id: Mapped[str] = mapped_column(primary_key=True)
    sync_id: Mapped[str] = mapped_column(primary_key=True)
    run_id: Mapped[str] = mapped_column(primary_key=True)
    run_order: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    detached: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    revision: Mapped[str]
    target_hash: Mapped[str]
    phase: Mapped[str] = mapped_column(default="new", server_default=text("'new'"))
    mode: Mapped[str] = mapped_column(Enum("upsert", "mirror", name="reverse_sync_mode"))
    extraction: Mapped[str] = mapped_column(Enum("cursor", "full", name="reverse_sync_extraction"))
    artifact_head: Mapped[Optional[bytes]]
    base_sequence: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    next_sequence: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    checkpoint_sequence: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    finish_sequence: Mapped[Optional[int]] = mapped_column(BigInteger)
    store: Mapped[Optional[bytes]]
    provider_state: Mapped[Optional[bytes]]
    finish_result: Mapped[Optional[bytes]]
    committed_generation: Mapped[Optional[str]]

    __table_args__ = (
        Index("reverse_sync_control_sync_id_idx", "sync_id"),
        Index("reverse_sync_control_workspace_id_sync_id_run_order_key", "workspace_id", "sync_id", "run_order", unique=True),
    )

class ReverseSyncTargetOwner(Base):
    __tablename__ = "reverse_sync_target_owner"

    target_hash: Mapped[str] = mapped_column(primary_key=True)
    workspace_id: Mapped[str]
    sync_id: Mapped[str]

    __table_args__ = (
        Index("reverse_sync_target_owner_workspace_id_sync_id_key", "workspace_id", "sync_id", unique=True),
    )

class SourceState(Base):
    __tablename__ = "source_state"

    sync_id: Mapped[str] = mapped_column(primary_key=True)
    stream: Mapped[str] = mapped_column(primary_key=True)
    state: Mapped[dict[str, Any]] = mapped_column(JSONB)
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class SourceTask(Base):
    __tablename__ = "source_task"

    sync_id: Mapped[str]
    task_id: Mapped[str] = mapped_column(primary_key=True)
    package: Mapped[str]
    version: Mapped[str]
    started_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    started_by: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[str]
    description: Mapped[Optional[str]]
    metrics: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("source_task_sync_id_idx", "sync_id"),
        Index("source_task_started_at_idx", "started_at"),
        Index("source_task_sync_id_started_at_idx", "sync_id", "started_at"),
    )

class TaskLog(Base):
    __tablename__ = "task_log"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid_mod.uuid4)
    level: Mapped[str]
    logger: Mapped[str]
    message: Mapped[str]
    sync_id: Mapped[str]
    task_id: Mapped[str]
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("task_log_sync_id_idx", "sync_id"),
        Index("task_log_task_id_idx", "task_id"),
        Index("task_log_timestamp_idx", "timestamp"),
    )

class ProfileBuilder(Base):
    __tablename__ = "ProfileBuilder"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    deleted: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProfileBuilder_workspaceId_fkey"))
    intermediate_storage_credentials: Mapped[dict[str, Any]] = mapped_column("intermediateStorageCredentials", JSONB)
    destination_id: Mapped[Optional[str]] = mapped_column("destinationId", ForeignKey("ConfigurationObject.id", ondelete="SET NULL", onupdate="CASCADE", name="ProfileBuilder_destinationId_fkey"))
    connection_options: Mapped[Optional[dict[str, Any]]] = mapped_column("connectionOptions", JSONB(none_as_null=True))
    version: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    workspace: Mapped["Workspace"] = relationship(back_populates="profile_builders")
    destination: Mapped[Optional["ConfigurationObject"]] = relationship(back_populates="profile_builder")
    functions: Mapped[list["ProfileBuilderFunction"]] = relationship(passive_deletes="all", back_populates="profile_builder")

class ProfileBuilderFunction(Base):
    __tablename__ = "ProfileBuilderFunction"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    profile_builder_id: Mapped[str] = mapped_column("profileBuilderId", ForeignKey("ProfileBuilder.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProfileBuilderFunction_profileBuilderId_fkey"))
    function_id: Mapped[str] = mapped_column("functionId", ForeignKey("ConfigurationObject.id", ondelete="RESTRICT", onupdate="CASCADE", name="ProfileBuilderFunction_functionId_fkey"))

    profile_builder: Mapped["ProfileBuilder"] = relationship(back_populates="functions")
    function: Mapped["ConfigurationObject"] = relationship(back_populates="profile_builder_function")

class ProfileBuilderState(Base):
    __tablename__ = "ProfileBuilderState"

    profile_builder_id: Mapped[str] = mapped_column("profileBuilderId", primary_key=True)
    profile_builder_version: Mapped[int] = mapped_column("profileBuilderVersion", primary_key=True)
    instance_index: Mapped[int] = mapped_column("instanceIndex", primary_key=True)
    total_instances: Mapped[int] = mapped_column("totalInstances", primary_key=True)
    started_at: Mapped[datetime] = mapped_column("startedAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    last_timestamp: Mapped[Optional[datetime]] = mapped_column("lastTimestamp")
    processed_users: Mapped[int] = mapped_column("processedUsers")
    total_users: Mapped[int] = mapped_column("totalUsers")
    error_users: Mapped[int] = mapped_column("errorUsers")
    speed: Mapped[float]

class ProfileBuilderState2(Base):
    __tablename__ = "ProfileBuilderState2"

    profile_builder_id: Mapped[str] = mapped_column("profileBuilderId", primary_key=True)
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    full_rebuild_info: Mapped[Optional[dict[str, Any]]] = mapped_column("fullRebuildInfo", JSONB(none_as_null=True))
    queues_info: Mapped[Optional[dict[str, Any]]] = mapped_column("queuesInfo", JSONB(none_as_null=True))
    metrics: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

class StatusChange(Base):
    __tablename__ = "StatusChange"

    id: Mapped[int] = mapped_column(BigInteger, primary_key=True, autoincrement=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    actor_id: Mapped[str] = mapped_column("actorId")
    type: Mapped[str]
    table_name: Mapped[str] = mapped_column("tableName")
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    status: Mapped[str]
    started_at: Mapped[datetime] = mapped_column("startedAt")
    counts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    description: Mapped[Optional[str]]
    queue_size: Mapped[int] = mapped_column("queueSize", default=0, server_default=text("0"))

    __table_args__ = (
        Index("StatusChange_actorId_type_tableName_idx", "actorId", "type", "tableName"),
        Index("StatusChange_timestamp_idx", "timestamp"),
        Index("StatusChange_startedAt_idx", "startedAt"),
    )

class NotificationState(Base):
    __tablename__ = "NotificationState"

    workspace_id: Mapped[str] = mapped_column("workspaceId")
    actor_id: Mapped[str] = mapped_column("actorId", primary_key=True)
    type: Mapped[str] = mapped_column(primary_key=True)
    table_name: Mapped[str] = mapped_column("tableName", primary_key=True)
    channel_id: Mapped[str] = mapped_column("channelId", primary_key=True)
    flapping_since: Mapped[Optional[datetime]] = mapped_column("flappingSince", UtcDateTimeTz(precision=3))
    last_notification: Mapped[datetime] = mapped_column("lastNotification", UtcDateTimeTz(precision=3))
    status_change_id: Mapped[int] = mapped_column("statusChangeId", BigInteger)
    error: Mapped[Optional[str]]

class Notification(Base):
    __tablename__ = "Notification"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid_mod.uuid4)
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    actor_id: Mapped[str] = mapped_column("actorId")
    type: Mapped[str]
    table_name: Mapped[str] = mapped_column("tableName")
    channel_id: Mapped[str] = mapped_column("channelId")
    status_change_id: Mapped[int] = mapped_column("statusChangeId", BigInteger)
    timestamp: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    status: Mapped[str]
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("Notification_actorId_type_tableName_idx", "actorId", "type", "tableName"),
        Index("Notification_timestamp_idx", "timestamp"),
    )

class OidcProvider(Base):
    __tablename__ = "OidcProvider"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    name: Mapped[str]
    issuer: Mapped[str]
    client_id: Mapped[str] = mapped_column("clientId")
    client_secret: Mapped[str] = mapped_column("clientSecret")
    authorization_endpoint: Mapped[Optional[str]] = mapped_column("authorizationEndpoint")
    token_endpoint: Mapped[Optional[str]] = mapped_column("tokenEndpoint")
    userinfo_endpoint: Mapped[Optional[str]] = mapped_column("userinfoEndpoint")
    jwks_uri: Mapped[Optional[str]] = mapped_column("jwksUri")
    introspection_endpoint: Mapped[Optional[str]] = mapped_column("introspectionEndpoint")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: ["openid", "profile", "email", "offline_access"], server_default=text("ARRAY['openid', 'profile', 'email', 'offline_access']"))
    audience: Mapped[Optional[str]]
    prompt: Mapped[Optional[str]]
    username_claim: Mapped[str] = mapped_column("usernameClaim", default="email", server_default=text("'email'"))
    email_claim: Mapped[str] = mapped_column("emailClaim", default="email", server_default=text("'email'"))
    name_claim: Mapped[str] = mapped_column("nameClaim", default="name", server_default=text("'name'"))
    group_claim: Mapped[str] = mapped_column("groupClaim", default="groups", server_default=text("'groups'"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    auto_discovery: Mapped[bool] = mapped_column("autoDiscovery", default=True, server_default=text("true"))

    __table_args__ = (
        Index("OidcProvider_issuer_clientId_key", "issuer", "clientId", unique=True),
    )

    oidc_login_groups: Mapped[list["OidcLoginGroup"]] = relationship(passive_deletes="all", back_populates="oidc_provider")

class OidcLoginGroup(Base):
    __tablename__ = "OidcLoginGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="RESTRICT", onupdate="CASCADE", name="OidcLoginGroup_workspaceId_fkey"))
    oidc_provider_id: Mapped[str] = mapped_column("oidcProviderId", ForeignKey("OidcProvider.id", ondelete="RESTRICT", onupdate="CASCADE", name="OidcLoginGroup_oidcProviderId_fkey"))
    group_value: Mapped[Optional[str]] = mapped_column("groupValue")
    allow_all_users: Mapped[bool] = mapped_column("allowAllUsers", default=False, server_default=text("false"))

    __table_args__ = (
        Index("OidcLoginGroup_workspaceId_idx", "workspaceId"),
        Index("OidcLoginGroup_oidcProviderId_idx", "oidcProviderId"),
        Index("OidcLoginGroup_workspaceId_oidcProviderId_key", "workspaceId", "oidcProviderId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="oidc_login_groups")
    oidc_provider: Mapped["OidcProvider"] = relationship(back_populates="oidc_login_groups")

class ReprocessingJobs(Base):
    __tablename__ = "reprocessing_jobs"

    id: Mapped[str] = mapped_column(String(64), primary_key=True)
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    status: Mapped[str] = mapped_column(String(32))
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6))
    started_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    completed_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    k8s_job_name: Mapped[Optional[str]] = mapped_column(String(255))
    total_files: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    total_workers: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("reprocessing_jobs_created_at_idx", "created_at"),
    )

    reprocessing_workers: Mapped[list["ReprocessingWorkers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="job")

class ReprocessingWorkers(Base):
    __tablename__ = "reprocessing_workers"

    job_id: Mapped[str] = mapped_column(String(64), ForeignKey("reprocessing_jobs.id", ondelete="CASCADE", onupdate="NO ACTION", name="reprocessing_workers_job_id_fkey"), primary_key=True)
    worker_index: Mapped[int] = mapped_column(primary_key=True)
    status: Mapped[str] = mapped_column(String(32))
    started_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    updated_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    current_file: Mapped[Optional[str]]
    current_line: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    current_ts: Mapped[Optional[str]]
    assigned_files: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    processed_files: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    total_lines: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    success_count: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    error_count: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    skipped_count: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_bytes: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    processed_bytes: Mapped[Optional[int]] = mapped_column(BigInteger, default=0, server_default=text("0"))
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("reprocessing_workers_job_id_idx", "job_id"),
        Index("reprocessing_workers_status_idx", "job_id", "status"),
    )

    job: Mapped["ReprocessingJobs"] = relationship(back_populates="reprocessing_workers")

class FunctionsServer(Base):
    __tablename__ = "FunctionsServer"

    workspace_id: Mapped[str] = mapped_column("workspaceId", primary_key=True)
    class_: Mapped[str] = mapped_column("class", primary_key=True)
    deployment_id: Mapped[str] = mapped_column("deploymentId")
    connections: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    empty_connections: Mapped[list[str]] = mapped_column("emptyConnections", ARRAY(Text), nullable=True)
    profile_builders: Mapped[list[str]] = mapped_column("profileBuilders", ARRAY(Text), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", UtcDateTimeTz(precision=3))

class RateLimitCounter(Base):
    __tablename__ = "RateLimitCounter"

    bucket_key: Mapped[str] = mapped_column("bucketKey", primary_key=True)
    window_start: Mapped[datetime] = mapped_column("windowStart", UtcDateTimeTz(precision=3), primary_key=True)
    count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("RateLimitCounter_expiresAt_idx", "expiresAt"),
    )
