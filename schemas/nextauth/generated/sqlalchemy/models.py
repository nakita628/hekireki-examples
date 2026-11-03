from sqlalchemy import Dialect, ForeignKey, Index, String, Text, TypeDecorator
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


class Base(DeclarativeBase):
    type_annotation_map = {datetime: UtcDateTime, str: Text}


class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[str]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    authenticator: Mapped[list["Authenticator"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Account(Base):
    __tablename__ = "Account"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str] = mapped_column(primary_key=True)
    provider_account_id: Mapped[str] = mapped_column("providerAccountId", primary_key=True)
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    user: Mapped["User"] = relationship(back_populates="accounts")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    identifier: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str] = mapped_column(primary_key=True)
    expires: Mapped[datetime]

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
    )

class Authenticator(Base):
    __tablename__ = "Authenticator"

    credential_id: Mapped[str] = mapped_column("credentialID", primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Authenticator_userId_fkey"), primary_key=True)
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    credential_public_key: Mapped[str] = mapped_column("credentialPublicKey")
    counter: Mapped[int]
    credential_device_type: Mapped[str] = mapped_column("credentialDeviceType")
    credential_backed_up: Mapped[bool] = mapped_column("credentialBackedUp")
    transports: Mapped[Optional[str]]

    __table_args__ = (
        Index("Authenticator_credentialID_key", "credentialID", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="authenticator")
