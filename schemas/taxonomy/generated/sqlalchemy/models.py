from sqlalchemy import Dialect, ForeignKey, Index, JSON, String, Text, TypeDecorator, text
from sqlalchemy.dialects.mysql import DATETIME
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl = DATETIME
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
    type_annotation_map = {datetime: UtcDateTime(fsp=3), str: String(191)}


class Account(Base):
    __tablename__ = "accounts"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="accounts_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]] = mapped_column(Text)
    access_token: Mapped[Optional[str]] = mapped_column(Text)
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]] = mapped_column(Text)
    session_state: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("accounts_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="sessions_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("sessions_sessionToken_key", "sessionToken", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    stripe_customer_id: Mapped[Optional[str]]
    stripe_subscription_id: Mapped[Optional[str]]
    stripe_price_id: Mapped[Optional[str]]
    stripe_current_period_end: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
        Index("users_stripe_customer_id_key", "stripe_customer_id", unique=True),
        Index("users_stripe_subscription_id_key", "stripe_subscription_id", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    post: Mapped[list["Post"]] = relationship(passive_deletes="all", back_populates="author")

class Post(Base):
    __tablename__ = "posts"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[str]
    content: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    published: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    author_id: Mapped[str] = mapped_column("authorId", ForeignKey("users.id", ondelete="RESTRICT", onupdate="CASCADE", name="posts_authorId_fkey"))

    __table_args__ = (
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    author: Mapped["User"] = relationship(back_populates="post")
