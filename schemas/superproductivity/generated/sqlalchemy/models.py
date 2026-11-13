from sqlalchemy import ARRAY, BigInteger, Dialect, ForeignKey, Index, MetaData, Text, TypeDecorator, text
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


class User(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    email: Mapped[str]
    password_hash: Mapped[Optional[str]]
    is_verified: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    verification_token: Mapped[Optional[str]]
    verification_token_expires_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    verification_resend_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    reset_password_token: Mapped[Optional[str]]
    reset_password_token_expires_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    passkey_recovery_token: Mapped[Optional[str]]
    passkey_recovery_token_expires_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    login_token: Mapped[Optional[str]]
    login_token_expires_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    failed_login_attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    locked_until: Mapped[Optional[int]] = mapped_column(BigInteger)
    token_version: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    terms_accepted_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    storage_quota_bytes: Mapped[int] = mapped_column(BigInteger, default=104857600, server_default=text("104857600"))
    storage_used_bytes: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
        Index("users_verification_token_idx", "verification_token"),
        Index("users_reset_password_token_idx", "reset_password_token"),
        Index("users_passkey_recovery_token_idx", "passkey_recovery_token"),
        Index("users_login_token_idx", "login_token"),
    )

    operations: Mapped[list["Operation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    devices: Mapped[list["SyncDevice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    passkeys: Mapped[list["Passkey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    pending_passkey_registrations: Mapped[list["PendingPasskeyRegistration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    capture_grants: Mapped[list["CaptureGrant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    capture_items: Mapped[list["CaptureItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    capture_authorizations: Mapped[list["CaptureAuth"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    connect_requests: Mapped[list["ConnectRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sync_state: Mapped[Optional["UserSyncState"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    capture_key: Mapped[Optional["CaptureKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    capture_connect_code: Mapped[Optional["CaptureConnectCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Passkey(Base):
    __tablename__ = "passkeys"

    id: Mapped[str] = mapped_column(primary_key=True)
    credential_id: Mapped[bytes]
    public_key: Mapped[bytes]
    counter: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    transports: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_used_at: Mapped[Optional[datetime]]
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="passkeys_user_id_fkey"))

    __table_args__ = (
        Index("passkeys_credential_id_key", "credential_id", unique=True),
        Index("passkeys_user_id_idx", "user_id"),
    )

    user: Mapped["User"] = relationship(back_populates="passkeys")

class PendingPasskeyRegistration(Base):
    __tablename__ = "pending_passkey_registrations"

    id: Mapped[str] = mapped_column(primary_key=True)
    verification_token: Mapped[str]
    verification_token_expires_at: Mapped[int] = mapped_column(BigInteger)
    credential_id: Mapped[bytes]
    public_key: Mapped[bytes]
    counter: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    transports: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="pending_passkey_registrations_user_id_fkey"))

    __table_args__ = (
        Index("pending_passkey_registrations_verification_token_key", "verification_token", unique=True),
        Index("pending_passkey_registrations_user_id_idx", "user_id"),
    )

    user: Mapped["User"] = relationship(back_populates="pending_passkey_registrations")

class Operation(Base):
    __tablename__ = "operations"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="operations_user_id_fkey"))
    client_id: Mapped[str]
    server_seq: Mapped[int]
    action_type: Mapped[str]
    op_type: Mapped[str]
    entity_type: Mapped[str]
    entity_id: Mapped[Optional[str]]
    entity_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    payload_bytes: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    vector_clock: Mapped[dict[str, Any]] = mapped_column(JSONB)
    schema_version: Mapped[int]
    client_timestamp: Mapped[int] = mapped_column(BigInteger)
    received_at: Mapped[int] = mapped_column(BigInteger)
    is_payload_encrypted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    sync_import_reason: Mapped[Optional[str]]
    repair_base_server_seq: Mapped[Optional[int]]

    __table_args__ = (
        Index("operations_user_id_entity_type_entity_id_server_seq_idx", "user_id", "entity_type", "entity_id", "server_seq"),
        Index("operations_user_id_received_at_server_seq_idx", "user_id", "received_at", "server_seq"),
        Index("operations_entity_ids_gin", "entity_ids"),
        Index("operations_user_id_server_seq_key", "user_id", "server_seq", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="operations")

class UserSyncState(Base):
    __tablename__ = "user_sync_state"

    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="user_sync_state_user_id_fkey"), primary_key=True, autoincrement=False)
    last_seq: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_snapshot_seq: Mapped[Optional[int]]
    snapshot_data: Mapped[Optional[bytes]]
    snapshot_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    snapshot_schema_version: Mapped[Optional[int]] = mapped_column(default=1, server_default=text("1"))
    latest_full_state_seq: Mapped[Optional[int]]
    latest_full_state_vector_clock: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    latest_state_replacement_seq: Mapped[Optional[int]]

    user: Mapped["User"] = relationship(back_populates="sync_state")

class SyncDevice(Base):
    __tablename__ = "sync_devices"

    client_id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="sync_devices_user_id_fkey"), primary_key=True)
    device_name: Mapped[Optional[str]]
    user_agent: Mapped[Optional[str]]
    app_version: Mapped[Optional[str]]
    last_seen_at: Mapped[int] = mapped_column(BigInteger)
    last_acked_seq: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[int] = mapped_column(BigInteger)

    user: Mapped["User"] = relationship(back_populates="devices")

class CaptureKey(Base):
    __tablename__ = "capture_keys"

    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="capture_keys_user_id_fkey"), primary_key=True, autoincrement=False)
    public_key: Mapped[str]
    fingerprint: Mapped[str]
    created_at: Mapped[int] = mapped_column(BigInteger)
    disabled_at: Mapped[Optional[int]] = mapped_column(BigInteger)

    user: Mapped["User"] = relationship(back_populates="capture_key")

class CaptureGrant(Base):
    __tablename__ = "capture_grants"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="capture_grants_user_id_fkey"))
    kind: Mapped[str]
    client_name: Mapped[str]
    secret_hash: Mapped[str]
    created_at: Mapped[int] = mapped_column(BigInteger)
    expires_at: Mapped[int] = mapped_column(BigInteger)
    last_used_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    revoked_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    client_id: Mapped[Optional[str]]
    resource: Mapped[Optional[str]]
    refresh_hash: Mapped[Optional[str]]
    prev_refresh_hash: Mapped[Optional[str]]
    rotated_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    refresh_expires_at: Mapped[Optional[int]] = mapped_column(BigInteger)

    __table_args__ = (
        Index("capture_grants_user_id_idx", "user_id"),
    )

    user: Mapped["User"] = relationship(back_populates="capture_grants")

class CaptureItem(Base):
    __tablename__ = "capture_items"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="capture_items_user_id_fkey"))
    grant_id: Mapped[str]
    ciphertext: Mapped[str]
    task_id: Mapped[str]
    created_at: Mapped[int] = mapped_column(BigInteger)
    lease_device_id: Mapped[Optional[str]]
    lease_until: Mapped[Optional[int]] = mapped_column(BigInteger)

    __table_args__ = (
        Index("capture_items_user_id_created_at_idx", "user_id", "created_at"),
        Index("capture_items_grant_id_idx", "grant_id"),
    )

    user: Mapped["User"] = relationship(back_populates="capture_items")

class CaptureConnectCode(Base):
    __tablename__ = "capture_connect_codes"

    user_id: Mapped[int] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="capture_connect_codes_user_id_fkey"), primary_key=True, autoincrement=False)
    code_hash: Mapped[str]
    token_version: Mapped[int]
    expires_at: Mapped[int] = mapped_column(BigInteger)

    __table_args__ = (
        Index("capture_connect_codes_code_hash_key", "code_hash", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="capture_connect_code")

class CaptureAuth(Base):
    __tablename__ = "capture_auth"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="capture_auth_user_id_fkey"))
    client_id: Mapped[str]
    client_name: Mapped[str]
    redirect_uri: Mapped[str]
    code_challenge: Mapped[str]
    resource: Mapped[str]
    state: Mapped[str]
    scope: Mapped[str]
    code_hash: Mapped[Optional[str]]
    token_version: Mapped[Optional[int]]
    expires_at: Mapped[int] = mapped_column(BigInteger)

    __table_args__ = (
        Index("capture_auth_code_hash_key", "code_hash", unique=True),
        Index("capture_auth_user_id_idx", "user_id"),
        Index("capture_auth_expires_at_idx", "expires_at"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="capture_authorizations")

class ConnectRequest(Base):
    __tablename__ = "connect_requests"

    id: Mapped[str] = mapped_column(primary_key=True)
    poll_secret_hash: Mapped[str]
    match_code: Mapped[str]
    device_name: Mapped[str]
    platform: Mapped[str]
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    user_id: Mapped[Optional[int]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="connect_requests_user_id_fkey"))
    token_version: Mapped[Optional[int]]
    expires_at: Mapped[int] = mapped_column(BigInteger)

    __table_args__ = (
        Index("connect_requests_user_id_idx", "user_id"),
        Index("connect_requests_expires_at_idx", "expires_at"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="connect_requests")
