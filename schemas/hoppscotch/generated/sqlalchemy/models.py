from sqlalchemy import Dialect, Enum, ForeignKey, Index, MetaData, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone
import uuid as uuid_mod


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
    type_annotation_map = {str: Text}


class Team(Base):
    __tablename__ = "Team"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]

    collections: Mapped[list["TeamCollection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    environments: Mapped[list["TeamEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    invitations: Mapped[list["TeamInvitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    members: Mapped[list["TeamMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    requests: Mapped[list["TeamRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class TeamMember(Base):
    __tablename__ = "TeamMember"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    role: Mapped[str] = mapped_column(Enum("OWNER", "VIEWER", "EDITOR", name="TeamAccessRole"))
    user_uid: Mapped[str] = mapped_column("userUid")
    team_id: Mapped[str] = mapped_column("teamID", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamMember_teamID_fkey"))

    __table_args__ = (
        Index("TeamMember_teamID_userUid_key", "teamID", "userUid", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="members")

class TeamInvitation(Base):
    __tablename__ = "TeamInvitation"

    id: Mapped[str] = mapped_column(primary_key=True)
    team_id: Mapped[str] = mapped_column("teamID", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamInvitation_teamID_fkey"))
    creator_uid: Mapped[str] = mapped_column("creatorUid")
    invitee_email: Mapped[str] = mapped_column("inviteeEmail")
    invitee_role: Mapped[str] = mapped_column("inviteeRole", Enum("OWNER", "VIEWER", "EDITOR", name="TeamAccessRole"))

    __table_args__ = (
        Index("TeamInvitation_teamID_idx", "teamID"),
        Index("TeamInvitation_teamID_inviteeEmail_key", "teamID", "inviteeEmail", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="invitations")

class TeamCollection(Base):
    __tablename__ = "TeamCollection"

    id: Mapped[str] = mapped_column(primary_key=True)
    parent_id: Mapped[Optional[str]] = mapped_column("parentID", ForeignKey("TeamCollection.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamCollection_parentID_fkey"))
    team_id: Mapped[str] = mapped_column("teamID", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamCollection_teamID_fkey"))
    title: Mapped[str]
    order_index: Mapped[int] = mapped_column("orderIndex")
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TeamCollection_teamID_parentID_orderIndex_key", "teamID", "parentID", "orderIndex", unique=True),
    )

    parent: Mapped[Optional["TeamCollection"]] = relationship(remote_side=[id], back_populates="children")
    team: Mapped["Team"] = relationship(back_populates="collections")
    children: Mapped[list["TeamCollection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")
    requests: Mapped[list["TeamRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="collection")

class TeamRequest(Base):
    __tablename__ = "TeamRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    collection_id: Mapped[str] = mapped_column("collectionID", ForeignKey("TeamCollection.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamRequest_collectionID_fkey"))
    team_id: Mapped[str] = mapped_column("teamID", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamRequest_teamID_fkey"))
    title: Mapped[str]
    request: Mapped[dict[str, Any]] = mapped_column(JSONB)
    mock_examples: Mapped[Optional[dict[str, Any]]] = mapped_column("mockExamples", JSONB(none_as_null=True))
    order_index: Mapped[int] = mapped_column("orderIndex")
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TeamRequest_teamID_collectionID_orderIndex_key", "teamID", "collectionID", "orderIndex", unique=True),
    )

    collection: Mapped["TeamCollection"] = relationship(back_populates="requests")
    team: Mapped["Team"] = relationship(back_populates="requests")

class TeamEnvironment(Base):
    __tablename__ = "TeamEnvironment"

    id: Mapped[str] = mapped_column(primary_key=True)
    team_id: Mapped[str] = mapped_column("teamID", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamEnvironment_teamID_fkey"))
    name: Mapped[str]
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB)

    team: Mapped["Team"] = relationship(back_populates="environments")

class User(Base):
    __tablename__ = "User"

    uid: Mapped[str] = mapped_column(primary_key=True)
    display_name: Mapped[Optional[str]] = mapped_column("displayName")
    email: Mapped[Optional[str]]
    photo_url: Mapped[Optional[str]] = mapped_column("photoURL")
    is_admin: Mapped[bool] = mapped_column("isAdmin", default=False, server_default=text("false"))
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    current_restsession: Mapped[Optional[dict[str, Any]]] = mapped_column("currentRESTSession", JSONB(none_as_null=True))
    current_gqlsession: Mapped[Optional[dict[str, Any]]] = mapped_column("currentGQLSession", JSONB(none_as_null=True))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_logged_on: Mapped[Optional[datetime]] = mapped_column("lastLoggedOn", UtcDateTimeTz(precision=3))
    last_active_on: Mapped[Optional[datetime]] = mapped_column("lastActiveOn", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
    )

    provider_accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    invited_users: Mapped[list["InvitedUsers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    mock_servers: Mapped[list["MockServer"]] = relationship(back_populates="user")
    personal_access_tokens: Mapped[list["PersonalAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    shortcodes: Mapped[list["Shortcode"]] = relationship(back_populates="user")
    user_collections: Mapped[list["UserCollection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    environments: Mapped[list["UserEnvironment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    history: Mapped[list["UserHistory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    user_requests: Mapped[list["UserRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    verification_tokens: Mapped[list["VerificationToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    settings: Mapped[Optional["UserSettings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    provider_refresh_token: Mapped[Optional[str]] = mapped_column("providerRefreshToken")
    provider_access_token: Mapped[Optional[str]] = mapped_column("providerAccessToken")
    provider_scope: Mapped[Optional[str]] = mapped_column("providerScope")
    logged_in: Mapped[datetime] = mapped_column("loggedIn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="provider_accounts")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    device_identifier: Mapped[str] = mapped_column("deviceIdentifier")
    token: Mapped[str] = mapped_column()
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="VerificationToken_userUid_fkey"))
    expires_on: Mapped[datetime] = mapped_column("expiresOn", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
        Index("VerificationToken_deviceIdentifier_token_key", "deviceIdentifier", "token", unique=True),
    )

    __mapper_args__ = {"primary_key": [token]}

    user: Mapped["User"] = relationship(back_populates="verification_tokens")

class UserSettings(Base):
    __tablename__ = "UserSettings"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="UserSettings_userUid_fkey"))
    properties: Mapped[dict[str, Any]] = mapped_column(JSONB)
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserSettings_userUid_key", "userUid", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="settings")

class UserHistory(Base):
    __tablename__ = "UserHistory"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="UserHistory_userUid_fkey"))
    req_type: Mapped[str] = mapped_column("reqType", Enum("REST", "GQL", name="ReqType"))
    request: Mapped[dict[str, Any]] = mapped_column(JSONB)
    response_metadata: Mapped[dict[str, Any]] = mapped_column("responseMetadata", JSONB)
    is_starred: Mapped[bool] = mapped_column("isStarred")
    executed_on: Mapped[datetime] = mapped_column("executedOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    user: Mapped["User"] = relationship(back_populates="history")

class UserEnvironment(Base):
    __tablename__ = "UserEnvironment"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="UserEnvironment_userUid_fkey"))
    name: Mapped[Optional[str]]
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB)
    is_global: Mapped[bool] = mapped_column("isGlobal")

    user: Mapped["User"] = relationship(back_populates="environments")

class InvitedUsers(Base):
    __tablename__ = "InvitedUsers"

    admin_uid: Mapped[str] = mapped_column("adminUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="InvitedUsers_adminUid_fkey"))
    admin_email: Mapped[str] = mapped_column("adminEmail")
    invitee_email: Mapped[str] = mapped_column("inviteeEmail")
    invited_on: Mapped[datetime] = mapped_column("invitedOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("InvitedUsers_inviteeEmail_key", "inviteeEmail", unique=True),
    )

    __mapper_args__ = {"primary_key": [invitee_email]}

    user: Mapped["User"] = relationship(back_populates="invited_users")

class UserRequest(Base):
    __tablename__ = "UserRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    collection_id: Mapped[str] = mapped_column("collectionID", ForeignKey("UserCollection.id", ondelete="CASCADE", onupdate="CASCADE", name="UserRequest_collectionID_fkey"))
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="UserRequest_userUid_fkey"))
    title: Mapped[str]
    request: Mapped[dict[str, Any]] = mapped_column(JSONB)
    type: Mapped[str] = mapped_column(Enum("REST", "GQL", name="ReqType"))
    mock_examples: Mapped[Optional[dict[str, Any]]] = mapped_column("mockExamples", JSONB(none_as_null=True))
    order_index: Mapped[int] = mapped_column("orderIndex")
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserRequest_userUid_collectionID_orderIndex_key", "userUid", "collectionID", "orderIndex", unique=True),
    )

    user_collection: Mapped["UserCollection"] = relationship(back_populates="requests")
    user: Mapped["User"] = relationship(back_populates="user_requests")

class UserCollection(Base):
    __tablename__ = "UserCollection"

    id: Mapped[str] = mapped_column(primary_key=True)
    parent_id: Mapped[Optional[str]] = mapped_column("parentID", ForeignKey("UserCollection.id", ondelete="CASCADE", onupdate="CASCADE", name="UserCollection_parentID_fkey"))
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="UserCollection_userUid_fkey"))
    title: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("REST", "GQL", name="ReqType"))
    order_index: Mapped[int] = mapped_column("orderIndex")
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserCollection_userUid_parentID_orderIndex_key", "userUid", "parentID", "orderIndex", unique=True),
    )

    parent: Mapped[Optional["UserCollection"]] = relationship(remote_side=[id], back_populates="children")
    user: Mapped["User"] = relationship(back_populates="user_collections")
    children: Mapped[list["UserCollection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")
    requests: Mapped[list["UserRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user_collection")

class MockServer(Base):
    __tablename__ = "MockServer"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    subdomain: Mapped[str]
    creator_uid: Mapped[Optional[str]] = mapped_column("creatorUid", ForeignKey("User.uid", ondelete="SET NULL", onupdate="CASCADE", name="MockServer_creatorUid_fkey"))
    collection_id: Mapped[str] = mapped_column("collectionID")
    workspace_type: Mapped[str] = mapped_column("workspaceType", Enum("USER", "TEAM", name="WorkspaceType"))
    workspace_id: Mapped[str] = mapped_column("workspaceID")
    delay_in_ms: Mapped[int] = mapped_column("delayInMs", default=0, server_default=text("0"))
    is_public: Mapped[bool] = mapped_column("isPublic", default=True, server_default=text("true"))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    hit_count: Mapped[int] = mapped_column("hitCount", default=0, server_default=text("0"))
    last_hit_at: Mapped[Optional[datetime]] = mapped_column("lastHitAt", UtcDateTimeTz(precision=3))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt", UtcDateTimeTz(precision=3))

    __table_args__ = (
        Index("MockServer_subdomain_key", "subdomain", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="mock_servers")
    request_logs: Mapped[list["MockServerLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="mock_server")
    activity_history: Mapped[list["MockServerActivity"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="mock_server")

class MockServerLog(Base):
    __tablename__ = "MockServerLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    mock_server_id: Mapped[str] = mapped_column("mockServerID", ForeignKey("MockServer.id", ondelete="CASCADE", onupdate="CASCADE", name="MockServerLog_mockServerID_fkey"))
    request_method: Mapped[str] = mapped_column("requestMethod")
    request_path: Mapped[str] = mapped_column("requestPath")
    request_headers: Mapped[dict[str, Any]] = mapped_column("requestHeaders", JSONB)
    request_body: Mapped[Optional[dict[str, Any]]] = mapped_column("requestBody", JSONB(none_as_null=True))
    request_query: Mapped[Optional[dict[str, Any]]] = mapped_column("requestQuery", JSONB(none_as_null=True))
    response_status: Mapped[int] = mapped_column("responseStatus")
    response_headers: Mapped[dict[str, Any]] = mapped_column("responseHeaders", JSONB)
    response_body: Mapped[Optional[dict[str, Any]]] = mapped_column("responseBody", JSONB(none_as_null=True))
    response_time: Mapped[int] = mapped_column("responseTime")
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    executed_at: Mapped[datetime] = mapped_column("executedAt", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("MockServerLog_mockServerID_idx", "mockServerID"),
        Index("MockServerLog_mockServerID_executedAt_idx", "mockServerID", "executedAt"),
    )

    mock_server: Mapped["MockServer"] = relationship(back_populates="request_logs")

class MockServerActivity(Base):
    __tablename__ = "MockServerActivity"

    id: Mapped[str] = mapped_column(primary_key=True)
    mock_server_id: Mapped[str] = mapped_column("mockServerID", ForeignKey("MockServer.id", ondelete="CASCADE", onupdate="CASCADE", name="MockServerActivity_mockServerID_fkey"))
    action: Mapped[str] = mapped_column(Enum("CREATED", "DELETED", "ACTIVATED", "DEACTIVATED", name="MockServerAction"))
    performed_by: Mapped[Optional[str]] = mapped_column("performedBy")
    performed_at: Mapped[datetime] = mapped_column("performedAt", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("MockServerActivity_mockServerID_idx", "mockServerID"),
    )

    mock_server: Mapped["MockServer"] = relationship(back_populates="activity_history")

class Shortcode(Base):
    __tablename__ = "Shortcode"

    id: Mapped[str] = mapped_column(primary_key=True)
    request: Mapped[dict[str, Any]] = mapped_column(JSONB)
    creator_uid: Mapped[Optional[str]] = mapped_column("creatorUid", ForeignKey("User.uid", ondelete="SET NULL", onupdate="CASCADE", name="Shortcode_creatorUid_fkey"))
    embed_properties: Mapped[Optional[dict[str, Any]]] = mapped_column("embedProperties", JSONB(none_as_null=True))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Shortcode_id_key", "id", unique=True),
        Index("Shortcode_id_creatorUid_key", "id", "creatorUid", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="shortcodes")

class InfraConfig(Base):
    __tablename__ = "InfraConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    value: Mapped[Optional[str]]
    is_encrypted: Mapped[bool] = mapped_column("isEncrypted", default=False, server_default=text("false"))
    last_synced_env_file_value: Mapped[Optional[str]] = mapped_column("lastSyncedEnvFileValue")
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("InfraConfig_name_key", "name", unique=True),
    )

class PersonalAccessToken(Base):
    __tablename__ = "PersonalAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_uid: Mapped[str] = mapped_column("userUid", ForeignKey("User.uid", ondelete="CASCADE", onupdate="CASCADE", name="PersonalAccessToken_userUid_fkey"))
    label: Mapped[str]
    token: Mapped[str] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    expires_on: Mapped[Optional[datetime]] = mapped_column("expiresOn", UtcDateTimeTz(precision=3))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PersonalAccessToken_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="personal_access_tokens")

class InfraToken(Base):
    __tablename__ = "InfraToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    creator_uid: Mapped[str] = mapped_column("creatorUid")
    label: Mapped[str]
    token: Mapped[str] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    expires_on: Mapped[Optional[datetime]] = mapped_column("expiresOn", UtcDateTimeTz(precision=3))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("InfraToken_token_key", "token", unique=True),
    )

class PublishedDocs(Base):
    __tablename__ = "PublishedDocs"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    title: Mapped[str]
    collection_id: Mapped[str] = mapped_column("collectionID")
    creator_uid: Mapped[str] = mapped_column("creatorUid")
    version: Mapped[str]
    auto_sync: Mapped[bool] = mapped_column("autoSync")
    document_tree: Mapped[Optional[dict[str, Any]]] = mapped_column("documentTree", JSONB(none_as_null=True))
    workspace_type: Mapped[str] = mapped_column("workspaceType", Enum("USER", "TEAM", name="WorkspaceType"))
    workspace_id: Mapped[str] = mapped_column("workspaceID")
    environment_id: Mapped[Optional[str]] = mapped_column("environmentID")
    environment_name: Mapped[Optional[str]] = mapped_column("environmentName")
    environment_variables: Mapped[Optional[dict[str, Any]]] = mapped_column("environmentVariables", JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_on: Mapped[datetime] = mapped_column("createdOn", UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_on: Mapped[datetime] = mapped_column("updatedOn", UtcDateTimeTz(precision=3), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PublishedDocs_collectionID_idx", "collectionID"),
        Index("PublishedDocs_slug_version_key", "slug", "version", unique=True),
    )
