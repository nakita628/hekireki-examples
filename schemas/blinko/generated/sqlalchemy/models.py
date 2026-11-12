from sqlalchemy import Dialect, ForeignKey, Index, JSON, MetaData, Numeric, String, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import TIMESTAMP
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


class Accounts(Base):
    __tablename__ = "accounts"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    nickname: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    password: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    image: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    api_token: Mapped[str] = mapped_column("apiToken", String, default="", server_default=text("''"))
    description: Mapped[str] = mapped_column(default="", server_default=text("''"))
    note: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    role: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    login_type: Mapped[str] = mapped_column("loginType", String, default="", server_default=text("''"))
    link_account_id: Mapped[Optional[int]] = mapped_column("linkAccountId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    notes: Mapped[list["Notes"]] = relationship(back_populates="account")
    configs: Mapped[list["Config"]] = relationship(back_populates="user")
    tags: Mapped[list["Tag"]] = relationship(back_populates="account")
    comments: Mapped[list["Comments"]] = relationship(back_populates="account")
    attachments: Mapped[list["Attachments"]] = relationship(back_populates="account")
    follows: Mapped[list["Follows"]] = relationship(passive_deletes="all", back_populates="account")
    notifications: Mapped[list["Notifications"]] = relationship(passive_deletes="all", back_populates="account")
    conversations: Mapped[list["Conversation"]] = relationship(passive_deletes="all", back_populates="account")
    shared_notes: Mapped[list["NoteInternalShare"]] = relationship(passive_deletes="all", back_populates="account")
    ai_scheduled_tasks: Mapped[list["AiScheduledTask"]] = relationship(passive_deletes="all", back_populates="account")

class Attachments(Base):
    __tablename__ = "attachments"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    is_share: Mapped[bool] = mapped_column("isShare", default=False, server_default=text("false"))
    share_password: Mapped[str] = mapped_column("sharePassword", String, default="", server_default=text("''"))
    name: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    path: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    size: Mapped[DecimalType] = mapped_column(Numeric, default=DecimalType("0"), server_default=text("0"))
    type: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    note_id: Mapped[Optional[int]] = mapped_column("noteId", ForeignKey("notes.id", ondelete="SET NULL", onupdate="CASCADE", name="attachments_noteId_fkey"))
    account_id: Mapped[Optional[int]] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="SET NULL", onupdate="CASCADE", name="attachments_accountId_fkey"))
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    perfix_path: Mapped[Optional[str]] = mapped_column("perfixPath", String, default="", server_default=text("''"))
    depth: Mapped[Optional[int]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSON(none_as_null=True))

    note: Mapped[Optional["Notes"]] = relationship(back_populates="attachments")
    account: Mapped[Optional["Accounts"]] = relationship(back_populates="attachments")

class Config(Base):
    __tablename__ = "config"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("accounts.id", ondelete="SET NULL", onupdate="CASCADE", name="config_userId_fkey"))

    user: Mapped[Optional["Accounts"]] = relationship(back_populates="configs")

class Notes(Base):
    __tablename__ = "notes"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    type: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    content: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    is_archived: Mapped[bool] = mapped_column("isArchived", default=False, server_default=text("false"))
    is_recycle: Mapped[bool] = mapped_column("isRecycle", default=False, server_default=text("false"))
    is_share: Mapped[bool] = mapped_column("isShare", default=False, server_default=text("false"))
    is_top: Mapped[bool] = mapped_column("isTop", default=False, server_default=text("false"))
    is_reviewed: Mapped[bool] = mapped_column("isReviewed", default=False, server_default=text("false"))
    share_password: Mapped[str] = mapped_column("sharePassword", String, default="", server_default=text("''"))
    share_encrypted_url: Mapped[Optional[str]] = mapped_column("shareEncryptedUrl", String)
    share_expiry_date: Mapped[Optional[datetime]] = mapped_column("shareExpiryDate", UtcDateTimeTz(precision=6))
    share_max_view: Mapped[Optional[int]] = mapped_column("shareMaxView", default=0, server_default=text("0"))
    share_view_count: Mapped[Optional[int]] = mapped_column("shareViewCount", default=0, server_default=text("0"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSON(none_as_null=True))
    account_id: Mapped[Optional[int]] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="SET NULL", onupdate="CASCADE", name="notes_accountId_fkey"))
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    account: Mapped[Optional["Accounts"]] = relationship(back_populates="notes")
    attachments: Mapped[list["Attachments"]] = relationship(back_populates="note")
    tags: Mapped[list["TagsToNote"]] = relationship(passive_deletes="all", back_populates="note")
    referenced_by: Mapped[list["NoteReference"]] = relationship(foreign_keys="NoteReference.to_note_id", passive_deletes="all", back_populates="to_note")
    references: Mapped[list["NoteReference"]] = relationship(foreign_keys="NoteReference.from_note_id", passive_deletes="all", back_populates="from_note")
    comments: Mapped[list["Comments"]] = relationship(passive_deletes="all", back_populates="note")
    histories: Mapped[list["NoteHistory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="note")
    internal_shares: Mapped[list["NoteInternalShare"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="note")

class Comments(Base):
    __tablename__ = "comments"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    content: Mapped[str]
    account_id: Mapped[Optional[int]] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="SET NULL", onupdate="CASCADE", name="comments_accountId_fkey"))
    guest_name: Mapped[Optional[str]] = mapped_column("guestName", String)
    guest_ip: Mapped[Optional[str]] = mapped_column("guestIP", String)
    guest_ua: Mapped[Optional[str]] = mapped_column("guestUA", String)
    note_id: Mapped[int] = mapped_column("noteId", ForeignKey("notes.id", ondelete="RESTRICT", onupdate="CASCADE", name="comments_noteId_fkey"))
    parent_id: Mapped[Optional[int]] = mapped_column("parentId", ForeignKey("comments.id", ondelete="SET NULL", onupdate="CASCADE", name="comments_parentId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    note: Mapped["Notes"] = relationship(back_populates="comments")
    account: Mapped[Optional["Accounts"]] = relationship(back_populates="comments")
    parent: Mapped[Optional["Comments"]] = relationship(remote_side=[id], back_populates="replies")
    replies: Mapped[list["Comments"]] = relationship(back_populates="parent")

class Tag(Base):
    __tablename__ = "tag"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    icon: Mapped[str] = mapped_column(String, default="", server_default=text("''"))
    parent: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    account_id: Mapped[Optional[int]] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="SET NULL", onupdate="CASCADE", name="tag_accountId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))

    account: Mapped[Optional["Accounts"]] = relationship(back_populates="tags")
    tags_to_note: Mapped[list["TagsToNote"]] = relationship(passive_deletes="all", back_populates="tag")

class TagsToNote(Base):
    __tablename__ = "tagsToNote"

    id: Mapped[int]
    note_id: Mapped[int] = mapped_column("noteId", ForeignKey("notes.id", ondelete="RESTRICT", onupdate="CASCADE", name="tagsToNote_noteId_fkey"), primary_key=True)
    tag_id: Mapped[int] = mapped_column("tagId", ForeignKey("tag.id", ondelete="RESTRICT", onupdate="CASCADE", name="tagsToNote_tagId_fkey"), primary_key=True)

    note: Mapped["Notes"] = relationship(back_populates="tags")
    tag: Mapped["Tag"] = relationship(back_populates="tags_to_note")

class NoteReference(Base):
    __tablename__ = "noteReference"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    from_note_id: Mapped[int] = mapped_column("fromNoteId", ForeignKey("notes.id", ondelete="RESTRICT", onupdate="CASCADE", name="noteReference_fromNoteId_fkey"))
    to_note_id: Mapped[int] = mapped_column("toNoteId", ForeignKey("notes.id", ondelete="RESTRICT", onupdate="CASCADE", name="noteReference_toNoteId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("noteReference_fromNoteId_toNoteId_key", "fromNoteId", "toNoteId", unique=True),
    )

    from_note: Mapped["Notes"] = relationship(foreign_keys=[from_note_id], back_populates="references")
    to_note: Mapped["Notes"] = relationship(foreign_keys=[to_note_id], back_populates="referenced_by")

class Follows(Base):
    __tablename__ = "follows"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    site_name: Mapped[Optional[str]] = mapped_column("siteName", String)
    site_url: Mapped[str] = mapped_column("siteUrl", String)
    site_avatar: Mapped[Optional[str]] = mapped_column("siteAvatar", String)
    description: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    follow_type: Mapped[str] = mapped_column("followType", String, default="following", server_default=text("'following'"))
    account_id: Mapped[int] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="RESTRICT", onupdate="CASCADE", name="follows_accountId_fkey"))

    account: Mapped["Accounts"] = relationship(back_populates="follows")

class Notifications(Base):
    __tablename__ = "notifications"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    type: Mapped[str] = mapped_column(String)
    title: Mapped[str] = mapped_column(String)
    content: Mapped[str]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSON(none_as_null=True))
    is_read: Mapped[bool] = mapped_column("isRead", default=False, server_default=text("false"))
    account_id: Mapped[int] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="RESTRICT", onupdate="CASCADE", name="notifications_accountId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    account: Mapped["Accounts"] = relationship(back_populates="notifications")

class Cache(Base):
    __tablename__ = "cache"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str] = mapped_column(String)
    value: Mapped[dict[str, Any]] = mapped_column(JSON)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("cache_key_key", "key", unique=True),
    )

class Plugin(Base):
    __tablename__ = "plugin"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSON)
    path: Mapped[str] = mapped_column(String)
    is_use: Mapped[bool] = mapped_column("isUse", default=True, server_default=text("true"))
    is_dev: Mapped[bool] = mapped_column("isDev", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

class Conversation(Base):
    __tablename__ = "conversation"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    title: Mapped[str] = mapped_column(String(255), default="", server_default=text("''"))
    is_share: Mapped[bool] = mapped_column("isShare", default=False, server_default=text("false"))
    account_id: Mapped[int] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="RESTRICT", onupdate="CASCADE", name="conversation_accountId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("conversation_accountId_idx", "accountId"),
    )

    account: Mapped["Accounts"] = relationship(back_populates="conversations")
    messages: Mapped[list["Message"]] = relationship(passive_deletes="all", back_populates="conversation")

class Message(Base):
    __tablename__ = "message"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    content: Mapped[str]
    role: Mapped[str] = mapped_column(String(50))
    conversation_id: Mapped[int] = mapped_column("conversationId", ForeignKey("conversation.id", ondelete="RESTRICT", onupdate="CASCADE", name="message_conversationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSON(none_as_null=True))

    __table_args__ = (
        Index("message_conversationId_idx", "conversationId"),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="messages")

class NoteHistory(Base):
    __tablename__ = "noteHistory"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    note_id: Mapped[int] = mapped_column("noteId", ForeignKey("notes.id", ondelete="CASCADE", onupdate="CASCADE", name="noteHistory_noteId_fkey"))
    content: Mapped[str]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSON(none_as_null=True))
    version: Mapped[int]
    account_id: Mapped[Optional[int]] = mapped_column("accountId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("noteHistory_noteId_idx", "noteId"),
        Index("noteHistory_accountId_idx", "accountId"),
    )

    note: Mapped["Notes"] = relationship(back_populates="histories")

class NoteInternalShare(Base):
    __tablename__ = "noteInternalShare"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    note_id: Mapped[int] = mapped_column("noteId", ForeignKey("notes.id", ondelete="CASCADE", onupdate="CASCADE", name="noteInternalShare_noteId_fkey"))
    account_id: Mapped[int] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="RESTRICT", onupdate="CASCADE", name="noteInternalShare_accountId_fkey"))
    can_edit: Mapped[bool] = mapped_column("canEdit", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("noteInternalShare_noteId_idx", "noteId"),
        Index("noteInternalShare_accountId_idx", "accountId"),
        Index("noteInternalShare_noteId_accountId_key", "noteId", "accountId", unique=True),
    )

    note: Mapped["Notes"] = relationship(back_populates="internal_shares")
    account: Mapped["Accounts"] = relationship(back_populates="shared_notes")

class Session(Base):
    __tablename__ = "session"

    id: Mapped[str] = mapped_column(primary_key=True)
    sid: Mapped[str]
    data: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("session_sid_key", "sid", unique=True),
        Index("session_expiresAt_idx", "expiresAt"),
    )

class AiProviders(Base):
    __tablename__ = "aiProviders"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    title: Mapped[str] = mapped_column(String(255))
    provider: Mapped[str] = mapped_column(String(50))
    base_url: Mapped[Optional[str]] = mapped_column("baseURL", String(500))
    api_key: Mapped[Optional[str]] = mapped_column("apiKey", String(500))
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    models: Mapped[list["AiModels"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="provider")

class AiModels(Base):
    __tablename__ = "aiModels"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    provider_id: Mapped[int] = mapped_column("providerId", ForeignKey("aiProviders.id", ondelete="CASCADE", onupdate="CASCADE", name="aiModels_providerId_fkey"))
    title: Mapped[str] = mapped_column(String(255))
    model_key: Mapped[str] = mapped_column("modelKey", String(255))
    capabilities: Mapped[dict[str, Any]] = mapped_column(JSON)
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("aiModels_providerId_idx", "providerId"),
    )

    provider: Mapped["AiProviders"] = relationship(back_populates="models")

class AiScheduledTask(Base):
    __tablename__ = "aiScheduledTask"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(String(255))
    prompt: Mapped[str]
    schedule: Mapped[str] = mapped_column(String(100))
    is_enabled: Mapped[bool] = mapped_column("isEnabled", default=True, server_default=text("true"))
    last_run: Mapped[Optional[datetime]] = mapped_column("lastRun", UtcDateTimeTz(precision=6))
    last_result: Mapped[Optional[dict[str, Any]]] = mapped_column("lastResult", JSON(none_as_null=True))
    account_id: Mapped[int] = mapped_column("accountId", ForeignKey("accounts.id", ondelete="RESTRICT", onupdate="CASCADE", name="aiScheduledTask_accountId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("aiScheduledTask_accountId_idx", "accountId"),
    )

    account: Mapped["Accounts"] = relationship(back_populates="ai_scheduled_tasks")

class McpServers(Base):
    __tablename__ = "mcpServers"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(String(255))
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(String(50))
    command: Mapped[Optional[str]]
    args: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    url: Mapped[Optional[str]] = mapped_column(String(500))
    env: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    is_enabled: Mapped[bool] = mapped_column("isEnabled", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

class Fonts(Base):
    __tablename__ = "fonts"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(String(255))
    display_name: Mapped[str] = mapped_column("displayName", String(255))
    url: Mapped[Optional[str]]
    file_data: Mapped[Optional[bytes]] = mapped_column("fileData")
    is_local: Mapped[bool] = mapped_column("isLocal", default=False, server_default=text("false"))
    is_system: Mapped[bool] = mapped_column("isSystem", default=False, server_default=text("false"))
    weights: Mapped[dict[str, Any]] = mapped_column(JSON)
    category: Mapped[str] = mapped_column(String(50), default="sans-serif", server_default=text("'sans-serif'"))
    sort_order: Mapped[int] = mapped_column("sortOrder", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("fonts_name_key", "name", unique=True),
    )
