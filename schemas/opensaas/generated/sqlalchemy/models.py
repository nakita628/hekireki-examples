from sqlalchemy import Dialect, ForeignKey, Index, MetaData, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Optional
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


class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    email: Mapped[Optional[str]]
    username: Mapped[Optional[str]]
    is_admin: Mapped[bool] = mapped_column("isAdmin", default=False, server_default=text("false"))
    payment_processor_user_id: Mapped[Optional[str]] = mapped_column("paymentProcessorUserId")
    subscription_status: Mapped[Optional[str]] = mapped_column("subscriptionStatus")
    subscription_plan: Mapped[Optional[str]] = mapped_column("subscriptionPlan")
    date_paid: Mapped[Optional[datetime]] = mapped_column("datePaid")
    credits: Mapped[int] = mapped_column(default=3, server_default=text("3"))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_username_key", "username", unique=True),
        Index("User_paymentProcessorUserId_key", "paymentProcessorUserId", unique=True),
    )

    gpt_responses: Mapped[list["GptResponse"]] = relationship(passive_deletes="all", back_populates="user")
    contact_form_messages: Mapped[list["ContactFormMessage"]] = relationship(passive_deletes="all", back_populates="user")
    tasks: Mapped[list["Task"]] = relationship(passive_deletes="all", back_populates="user")
    files: Mapped[list["File"]] = relationship(passive_deletes="all", back_populates="user")

class ProcessedWebhookEvent(Base):
    __tablename__ = "ProcessedWebhookEvent"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    payment_processor_event_id: Mapped[str] = mapped_column("paymentProcessorEventId")
    event_type: Mapped[str] = mapped_column("eventType")

    __table_args__ = (
        Index("ProcessedWebhookEvent_paymentProcessorEventId_key", "paymentProcessorEventId", unique=True),
    )

class GptResponse(Base):
    __tablename__ = "GptResponse"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="GptResponse_userId_fkey"))
    content: Mapped[str]

    user: Mapped["User"] = relationship(back_populates="gpt_responses")

class Task(Base):
    __tablename__ = "Task"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Task_userId_fkey"))
    description: Mapped[str]
    time: Mapped[str] = mapped_column(default="1", server_default=text("'1'"))
    is_done: Mapped[bool] = mapped_column("isDone", default=False, server_default=text("false"))

    user: Mapped["User"] = relationship(back_populates="tasks")

class File(Base):
    __tablename__ = "File"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="File_userId_fkey"))
    name: Mapped[str]
    type: Mapped[str]
    s3_key: Mapped[str] = mapped_column("s3Key")

    user: Mapped["User"] = relationship(back_populates="files")

class DailyStats(Base):
    __tablename__ = "DailyStats"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    date_: Mapped[datetime] = mapped_column("date", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    total_views: Mapped[int] = mapped_column("totalViews", default=0, server_default=text("0"))
    prev_day_views_change_percent: Mapped[str] = mapped_column("prevDayViewsChangePercent", default="0", server_default=text("'0'"))
    user_count: Mapped[int] = mapped_column("userCount", default=0, server_default=text("0"))
    paid_user_count: Mapped[int] = mapped_column("paidUserCount", default=0, server_default=text("0"))
    user_delta: Mapped[int] = mapped_column("userDelta", default=0, server_default=text("0"))
    paid_user_delta: Mapped[int] = mapped_column("paidUserDelta", default=0, server_default=text("0"))
    total_revenue: Mapped[float] = mapped_column("totalRevenue", default=0, server_default=text("0"))
    total_profit: Mapped[float] = mapped_column("totalProfit", default=0, server_default=text("0"))

    __table_args__ = (
        Index("DailyStats_date_key", "date", unique=True),
    )

    sources: Mapped[list["PageViewSource"]] = relationship(back_populates="daily_stats")

class PageViewSource(Base):
    __tablename__ = "PageViewSource"

    name: Mapped[str] = mapped_column(primary_key=True)
    date_: Mapped[datetime] = mapped_column("date", primary_key=True, default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    daily_stats_id: Mapped[Optional[int]] = mapped_column("dailyStatsId", ForeignKey("DailyStats.id", ondelete="SET NULL", onupdate="CASCADE", name="PageViewSource_dailyStatsId_fkey"))
    visitors: Mapped[int]

    daily_stats: Mapped[Optional["DailyStats"]] = relationship(back_populates="sources")

class Logs(Base):
    __tablename__ = "Logs"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    message: Mapped[str]
    level: Mapped[str]

class ContactFormMessage(Base):
    __tablename__ = "ContactFormMessage"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ContactFormMessage_userId_fkey"))
    content: Mapped[str]
    is_read: Mapped[bool] = mapped_column("isRead", default=False, server_default=text("false"))
    replied_at: Mapped[Optional[datetime]] = mapped_column("repliedAt")

    user: Mapped["User"] = relationship(back_populates="contact_form_messages")
