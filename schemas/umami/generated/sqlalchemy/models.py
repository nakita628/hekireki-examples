from sqlalchemy import CHAR, Dialect, ForeignKey, Index, MetaData, Numeric, String, Text, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from decimal import Decimal as DecimalType
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


class User(Base):
    __tablename__ = "user"

    id: Mapped[uuid_mod.UUID] = mapped_column("user_id", Uuid, primary_key=True)
    username: Mapped[str] = mapped_column(String(255))
    password: Mapped[str] = mapped_column(String(60))
    role: Mapped[str] = mapped_column(String(50))
    logo_url: Mapped[Optional[str]] = mapped_column(String(2183))
    display_name: Mapped[Optional[str]] = mapped_column(String(255))
    two_factor_required: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("user_username_key", "username", unique=True),
    )

    websites: Mapped[list["Website"]] = relationship(foreign_keys="Website.user_id", back_populates="user")
    created_by: Mapped[list["Website"]] = relationship(foreign_keys="Website.created_by", back_populates="create_user")
    links: Mapped[list["Link"]] = relationship(back_populates="user")
    pixels: Mapped[list["Pixel"]] = relationship(back_populates="user")
    teams: Mapped[list["TeamUser"]] = relationship(passive_deletes="all", back_populates="user")
    reports: Mapped[list["Report"]] = relationship(passive_deletes="all", back_populates="user")
    boards: Mapped[list["Board"]] = relationship(back_populates="user")
    two_factor_backup_codes: Mapped[list["TwoFactorBackupCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factor_otp_useds: Mapped[list["TwoFactorOtpUsed"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factor_auth: Mapped[Optional["TwoFactorAuth"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    two_factor_rate_limit: Mapped[Optional["TwoFactorRateLimit"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class ApiKey(Base):
    __tablename__ = "api_key"

    id: Mapped[uuid_mod.UUID] = mapped_column("api_key_id", Uuid, primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="CASCADE", onupdate="CASCADE", name="api_key_user_id_fkey"))
    name: Mapped[str] = mapped_column(String(255))
    key_hash: Mapped[str] = mapped_column(String(128))
    key_prefix: Mapped[str] = mapped_column(String(20))
    last_used_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("api_key_key_hash_key", "key_hash", unique=True),
        Index("api_key_user_id_idx", "user_id"),
    )

    user: Mapped["User"] = relationship(back_populates="api_keys")

class Session(Base):
    __tablename__ = "session"

    id: Mapped[uuid_mod.UUID] = mapped_column("session_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    browser: Mapped[Optional[str]] = mapped_column(String(20))
    os: Mapped[Optional[str]] = mapped_column(String(20))
    device: Mapped[Optional[str]] = mapped_column(String(20))
    screen: Mapped[Optional[str]] = mapped_column(String(11))
    language: Mapped[Optional[str]] = mapped_column(String(35))
    country: Mapped[Optional[str]] = mapped_column(CHAR(2))
    region: Mapped[Optional[str]] = mapped_column(String(20))
    city: Mapped[Optional[str]] = mapped_column(String(50))
    distinct_id: Mapped[Optional[str]] = mapped_column(String(50))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("session_created_at_idx", "created_at"),
        Index("session_website_id_idx", "website_id"),
        Index("session_website_id_created_at_idx", "website_id", "created_at"),
        Index("session_website_id_created_at_browser_idx", "website_id", "created_at", "browser"),
        Index("session_website_id_created_at_os_idx", "website_id", "created_at", "os"),
        Index("session_website_id_created_at_device_idx", "website_id", "created_at", "device"),
        Index("session_website_id_created_at_screen_idx", "website_id", "created_at", "screen"),
        Index("session_website_id_created_at_language_idx", "website_id", "created_at", "language"),
        Index("session_website_id_created_at_country_idx", "website_id", "created_at", "country"),
        Index("session_website_id_created_at_region_idx", "website_id", "created_at", "region"),
        Index("session_website_id_created_at_city_idx", "website_id", "created_at", "city"),
    )

    website_events: Mapped[list["WebsiteEvent"]] = relationship(passive_deletes="all", back_populates="session")
    session_data: Mapped[list["SessionData"]] = relationship(passive_deletes="all", back_populates="session")
    revenue: Mapped[list["Revenue"]] = relationship(passive_deletes="all", back_populates="session")

class SessionLink(Base):
    __tablename__ = "session_link"

    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True)
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True)
    distinct_id: Mapped[str] = mapped_column(String(50), primary_key=True)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("session_link_website_id_session_id_idx", "website_id", "session_id"),
    )

class Website(Base):
    __tablename__ = "website"

    id: Mapped[uuid_mod.UUID] = mapped_column("website_id", Uuid, primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
    domain: Mapped[Optional[str]] = mapped_column(String(500))
    reset_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    user_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="SET NULL", onupdate="CASCADE", name="website_user_id_fkey"))
    team_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("team.team_id", ondelete="SET NULL", onupdate="CASCADE", name="website_team_id_fkey"))
    created_by: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="SET NULL", onupdate="CASCADE", name="website_created_by_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    recorder_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    replay_config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("website_user_id_idx", "user_id"),
        Index("website_team_id_idx", "team_id"),
        Index("website_created_at_idx", "created_at"),
        Index("website_created_by_idx", "created_by"),
    )

    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="websites")
    create_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="created_by")
    team: Mapped[Optional["Team"]] = relationship(back_populates="websites")
    annotations: Mapped[list["Annotation"]] = relationship(passive_deletes="all", back_populates="website")
    event_data: Mapped[list["EventData"]] = relationship(passive_deletes="all", back_populates="website")
    reports: Mapped[list["Report"]] = relationship(passive_deletes="all", back_populates="website")
    revenue: Mapped[list["Revenue"]] = relationship(passive_deletes="all", back_populates="website")
    segments: Mapped[list["Segment"]] = relationship(passive_deletes="all", back_populates="website")
    session_data: Mapped[list["SessionData"]] = relationship(passive_deletes="all", back_populates="website")
    session_replays: Mapped[list["SessionReplay"]] = relationship(passive_deletes="all", back_populates="website")
    session_replays_saved: Mapped[list["SessionReplaySaved"]] = relationship(passive_deletes="all", back_populates="website")
    heatmap_events: Mapped[list["HeatmapEvent"]] = relationship(passive_deletes="all", back_populates="website")

class WebsiteEvent(Base):
    __tablename__ = "website_event"

    id: Mapped[uuid_mod.UUID] = mapped_column("event_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("session.session_id", ondelete="RESTRICT", onupdate="CASCADE", name="website_event_session_id_fkey"))
    visit_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    url_path: Mapped[str] = mapped_column(String(500))
    url_query: Mapped[Optional[str]] = mapped_column(String(500))
    utm_source: Mapped[Optional[str]] = mapped_column(String(255))
    utm_medium: Mapped[Optional[str]] = mapped_column(String(255))
    utm_campaign: Mapped[Optional[str]] = mapped_column(String(255))
    utm_content: Mapped[Optional[str]] = mapped_column(String(255))
    utm_term: Mapped[Optional[str]] = mapped_column(String(255))
    referrer_path: Mapped[Optional[str]] = mapped_column(String(500))
    referrer_query: Mapped[Optional[str]] = mapped_column(String(500))
    referrer_domain: Mapped[Optional[str]] = mapped_column(String(500))
    page_title: Mapped[Optional[str]] = mapped_column(String(500))
    gclid: Mapped[Optional[str]] = mapped_column(String(255))
    fbclid: Mapped[Optional[str]] = mapped_column(String(255))
    msclkid: Mapped[Optional[str]] = mapped_column(String(255))
    ttclid: Mapped[Optional[str]] = mapped_column(String(255))
    lifatid: Mapped[Optional[str]] = mapped_column("li_fat_id", String(255))
    twclid: Mapped[Optional[str]] = mapped_column(String(255))
    event_type: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    event_name: Mapped[Optional[str]] = mapped_column(String(50))
    tag: Mapped[Optional[str]] = mapped_column(String(50))
    hostname: Mapped[Optional[str]] = mapped_column(String(100))
    lcp: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=10, scale=1))
    inp: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=10, scale=1))
    cls_: Mapped[Optional[DecimalType]] = mapped_column("cls", Numeric(precision=10, scale=4))
    fcp: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=10, scale=1))
    ttfb: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=10, scale=1))

    __table_args__ = (
        Index("website_event_created_at_idx", "created_at"),
        Index("website_event_session_id_idx", "session_id"),
        Index("website_event_visit_id_idx", "visit_id"),
        Index("website_event_website_id_idx", "website_id"),
        Index("website_event_website_id_created_at_idx", "website_id", "created_at"),
        Index("website_event_website_id_created_at_url_path_idx", "website_id", "created_at", "url_path"),
        Index("website_event_website_id_created_at_url_query_idx", "website_id", "created_at", "url_query"),
        Index("website_event_website_id_created_at_referrer_domain_idx", "website_id", "created_at", "referrer_domain"),
        Index("website_event_website_id_created_at_page_title_idx", "website_id", "created_at", "page_title"),
        Index("website_event_website_id_created_at_event_name_idx", "website_id", "created_at", "event_name"),
        Index("website_event_website_id_created_at_tag_idx", "website_id", "created_at", "tag"),
        Index("website_event_website_id_session_id_created_at_idx", "website_id", "session_id", "created_at"),
        Index("website_event_website_id_visit_id_created_at_idx", "website_id", "visit_id", "created_at"),
        Index("website_event_website_id_created_at_hostname_idx", "website_id", "created_at", "hostname"),
    )

    session: Mapped["Session"] = relationship(back_populates="website_events")
    event_data: Mapped[list["EventData"]] = relationship(passive_deletes="all", back_populates="website_event")

class EventData(Base):
    __tablename__ = "event_data"

    id: Mapped[uuid_mod.UUID] = mapped_column("event_data_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="event_data_website_id_fkey"))
    website_event_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website_event.event_id", ondelete="RESTRICT", onupdate="CASCADE", name="event_data_website_event_id_fkey"))
    data_key: Mapped[str] = mapped_column(String(500))
    string_value: Mapped[Optional[str]] = mapped_column(String(500))
    number_value: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=19, scale=4))
    date_value: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    data_type: Mapped[int]
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("event_data_created_at_idx", "created_at"),
        Index("event_data_website_id_idx", "website_id"),
        Index("event_data_website_event_id_idx", "website_event_id"),
        Index("event_data_website_id_created_at_idx", "website_id", "created_at"),
        Index("event_data_website_id_created_at_data_key_idx", "website_id", "created_at", "data_key"),
    )

    website: Mapped["Website"] = relationship(back_populates="event_data")
    website_event: Mapped["WebsiteEvent"] = relationship(back_populates="event_data")

class SessionData(Base):
    __tablename__ = "session_data"

    id: Mapped[uuid_mod.UUID] = mapped_column("session_data_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="session_data_website_id_fkey"))
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("session.session_id", ondelete="RESTRICT", onupdate="CASCADE", name="session_data_session_id_fkey"))
    data_key: Mapped[str] = mapped_column(String(500))
    string_value: Mapped[Optional[str]] = mapped_column(String(500))
    number_value: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=19, scale=4))
    date_value: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    data_type: Mapped[int]
    distinct_id: Mapped[Optional[str]] = mapped_column(String(50))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("session_data_created_at_idx", "created_at"),
        Index("session_data_website_id_idx", "website_id"),
        Index("session_data_session_id_idx", "session_id"),
        Index("session_data_session_id_created_at_idx", "session_id", "created_at"),
        Index("session_data_website_id_created_at_data_key_idx", "website_id", "created_at", "data_key"),
        Index("session_data_session_id_data_key_key", "session_id", "data_key", unique=True),
    )

    website: Mapped["Website"] = relationship(back_populates="session_data")
    session: Mapped["Session"] = relationship(back_populates="session_data")

class Team(Base):
    __tablename__ = "team"

    id: Mapped[uuid_mod.UUID] = mapped_column("team_id", Uuid, primary_key=True)
    name: Mapped[str] = mapped_column(String(50))
    access_code: Mapped[Optional[str]] = mapped_column(String(50))
    logo_url: Mapped[Optional[str]] = mapped_column(String(2183))
    two_factor_required: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("team_access_code_key", "access_code", unique=True),
        Index("team_access_code_idx", "access_code"),
    )

    websites: Mapped[list["Website"]] = relationship(back_populates="team")
    members: Mapped[list["TeamUser"]] = relationship(passive_deletes="all", back_populates="team")
    links: Mapped[list["Link"]] = relationship(back_populates="team")
    pixels: Mapped[list["Pixel"]] = relationship(back_populates="team")
    boards: Mapped[list["Board"]] = relationship(back_populates="team")

class TeamUser(Base):
    __tablename__ = "team_user"

    id: Mapped[uuid_mod.UUID] = mapped_column("team_user_id", Uuid, primary_key=True)
    team_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("team.team_id", ondelete="RESTRICT", onupdate="CASCADE", name="team_user_team_id_fkey"))
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="team_user_user_id_fkey"))
    role: Mapped[str] = mapped_column(String(50))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("team_user_team_id_idx", "team_id"),
        Index("team_user_user_id_idx", "user_id"),
    )

    team: Mapped["Team"] = relationship(back_populates="members")
    user: Mapped["User"] = relationship(back_populates="teams")

class Report(Base):
    __tablename__ = "report"

    id: Mapped[uuid_mod.UUID] = mapped_column("report_id", Uuid, primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="report_user_id_fkey"))
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="report_website_id_fkey"))
    type: Mapped[str] = mapped_column(String(50))
    name: Mapped[str] = mapped_column(String(200))
    description: Mapped[str] = mapped_column(String(500))
    parameters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("report_user_id_idx", "user_id"),
        Index("report_website_id_idx", "website_id"),
        Index("report_type_idx", "type"),
        Index("report_name_idx", "name"),
    )

    user: Mapped["User"] = relationship(back_populates="reports")
    website: Mapped["Website"] = relationship(back_populates="reports")

class Annotation(Base):
    __tablename__ = "annotation"

    id: Mapped[uuid_mod.UUID] = mapped_column("annotation_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="annotation_website_id_fkey"))
    user_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid)
    date_: Mapped[datetime] = mapped_column("date", UtcDateTimeTz(precision=6))
    all_day: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    note: Mapped[str] = mapped_column(String(500))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("annotation_website_id_date_idx", "website_id", "date"),
    )

    website: Mapped["Website"] = relationship(back_populates="annotations")

class Segment(Base):
    __tablename__ = "segment"

    id: Mapped[uuid_mod.UUID] = mapped_column("segment_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="segment_website_id_fkey"))
    type: Mapped[str] = mapped_column(String(50))
    name: Mapped[str] = mapped_column(String(200))
    parameters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("segment_website_id_idx", "website_id"),
    )

    website: Mapped["Website"] = relationship(back_populates="segments")

class Revenue(Base):
    __tablename__ = "revenue"

    id: Mapped[uuid_mod.UUID] = mapped_column("revenue_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="revenue_website_id_fkey"))
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("session.session_id", ondelete="RESTRICT", onupdate="CASCADE", name="revenue_session_id_fkey"))
    event_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    event_name: Mapped[str] = mapped_column(String(50))
    currency: Mapped[str] = mapped_column(String(10))
    revenue: Mapped[Optional[DecimalType]] = mapped_column(Numeric(precision=19, scale=4))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("revenue_website_id_idx", "website_id"),
        Index("revenue_session_id_idx", "session_id"),
        Index("revenue_website_id_created_at_idx", "website_id", "created_at"),
        Index("revenue_website_id_session_id_created_at_idx", "website_id", "session_id", "created_at"),
    )

    website: Mapped["Website"] = relationship(back_populates="revenue")
    session: Mapped["Session"] = relationship(back_populates="revenue")

class Link(Base):
    __tablename__ = "link"

    id: Mapped[uuid_mod.UUID] = mapped_column("link_id", Uuid, primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
    url: Mapped[str] = mapped_column(String(500))
    slug: Mapped[str] = mapped_column(String(100))
    user_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="SET NULL", onupdate="CASCADE", name="link_user_id_fkey"))
    team_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("team.team_id", ondelete="SET NULL", onupdate="CASCADE", name="link_team_id_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("link_slug_key", "slug", unique=True),
        Index("link_slug_idx", "slug"),
        Index("link_user_id_idx", "user_id"),
        Index("link_team_id_idx", "team_id"),
        Index("link_created_at_idx", "created_at"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="links")
    team: Mapped[Optional["Team"]] = relationship(back_populates="links")

class Pixel(Base):
    __tablename__ = "pixel"

    id: Mapped[uuid_mod.UUID] = mapped_column("pixel_id", Uuid, primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
    slug: Mapped[str] = mapped_column(String(100))
    user_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="SET NULL", onupdate="CASCADE", name="pixel_user_id_fkey"))
    team_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("team.team_id", ondelete="SET NULL", onupdate="CASCADE", name="pixel_team_id_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("pixel_slug_key", "slug", unique=True),
        Index("pixel_slug_idx", "slug"),
        Index("pixel_user_id_idx", "user_id"),
        Index("pixel_team_id_idx", "team_id"),
        Index("pixel_created_at_idx", "created_at"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="pixels")
    team: Mapped[Optional["Team"]] = relationship(back_populates="pixels")

class Board(Base):
    __tablename__ = "board"

    id: Mapped[uuid_mod.UUID] = mapped_column("board_id", Uuid, primary_key=True)
    type: Mapped[str] = mapped_column(String(50))
    name: Mapped[str] = mapped_column(String(200))
    description: Mapped[str] = mapped_column(String(500))
    parameters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    user_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="SET NULL", onupdate="CASCADE", name="board_user_id_fkey"))
    team_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column(Uuid, ForeignKey("team.team_id", ondelete="SET NULL", onupdate="CASCADE", name="board_team_id_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("board_user_id_idx", "user_id"),
        Index("board_team_id_idx", "team_id"),
        Index("board_created_at_idx", "created_at"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="boards")
    team: Mapped[Optional["Team"]] = relationship(back_populates="boards")

class Share(Base):
    __tablename__ = "share"

    id: Mapped[uuid_mod.UUID] = mapped_column("share_id", Uuid, primary_key=True)
    entity_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    name: Mapped[str] = mapped_column(String(200))
    share_type: Mapped[int]
    slug: Mapped[str] = mapped_column(String(100))
    parameters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("share_slug_key", "slug", unique=True),
        Index("share_entity_id_idx", "entity_id"),
    )

class SessionReplay(Base):
    __tablename__ = "session_replay"

    id: Mapped[uuid_mod.UUID] = mapped_column("replay_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="session_replay_website_id_fkey"))
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    visit_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    chunk_index: Mapped[int]
    events: Mapped[bytes]
    event_count: Mapped[int]
    started_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6))
    ended_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6))
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("session_replay_website_id_idx", "website_id"),
        Index("session_replay_session_id_idx", "session_id"),
        Index("session_replay_visit_id_idx", "visit_id"),
        Index("session_replay_website_id_session_id_idx", "website_id", "session_id"),
        Index("session_replay_website_id_visit_id_idx", "website_id", "visit_id"),
        Index("session_replay_website_id_created_at_idx", "website_id", "created_at"),
        Index("session_replay_session_id_chunk_index_idx", "session_id", "chunk_index"),
    )

    website: Mapped["Website"] = relationship(back_populates="session_replays")

class SessionReplaySaved(Base):
    __tablename__ = "session_replay_saved"

    id: Mapped[uuid_mod.UUID] = mapped_column("saved_replay_id", Uuid, primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="session_replay_saved_website_id_fkey"))
    visit_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    created_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("session_replay_saved_website_id_idx", "website_id"),
        Index("session_replay_saved_visit_id_idx", "visit_id"),
        Index("session_replay_saved_website_id_created_at_idx", "website_id", "created_at"),
        Index("session_replay_saved_website_id_visit_id_key", "website_id", "visit_id", unique=True),
    )

    website: Mapped["Website"] = relationship(back_populates="session_replays_saved")

class HeatmapEvent(Base):
    __tablename__ = "heatmap_event"

    id: Mapped[uuid_mod.UUID] = mapped_column("heatmap_event_id", Uuid, primary_key=True)
    website_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("website.website_id", ondelete="RESTRICT", onupdate="CASCADE", name="heatmap_event_website_id_fkey"))
    session_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    visit_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid)
    url_path: Mapped[str] = mapped_column(String(500))
    event_type: Mapped[int]
    x: Mapped[Optional[int]]
    y: Mapped[Optional[int]]
    page_x: Mapped[Optional[int]]
    page_y: Mapped[Optional[int]]
    page_w: Mapped[Optional[int]]
    viewport_w: Mapped[Optional[int]]
    viewport_h: Mapped[Optional[int]]
    page_h: Mapped[Optional[int]]
    scroll_pct: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("heatmap_event_website_id_idx", "website_id"),
        Index("heatmap_event_visit_id_idx", "visit_id"),
        Index("heatmap_event_website_id_created_at_idx", "website_id", "created_at"),
        Index("heatmap_event_website_id_url_path_event_type_created_at_idx", "website_id", "url_path", "event_type", "created_at"),
    )

    website: Mapped["Website"] = relationship(back_populates="heatmap_events")

class TwoFactorAuth(Base):
    __tablename__ = "two_factor_auth"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_auth_user_id_fkey"))
    secret: Mapped[str]
    is_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("two_factor_auth_user_id_key", "user_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor_auth")

class TwoFactorBackupCode(Base):
    __tablename__ = "two_factor_backup_code"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_backup_code_user_id_fkey"))
    code_hash: Mapped[str]
    used: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("two_factor_backup_code_user_id_idx", "user_id"),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor_backup_codes")

class TwoFactorOtpUsed(Base):
    __tablename__ = "two_factor_otp_used"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_otp_used_user_id_fkey"))
    otp: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("two_factor_otp_used_user_id_otp_key", "user_id", "otp", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor_otp_useds")

class TwoFactorRateLimit(Base):
    __tablename__ = "two_factor_rate_limit"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, ForeignKey("user.user_id", ondelete="CASCADE", onupdate="CASCADE", name="two_factor_rate_limit_user_id_fkey"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    locked_until: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    updated_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("two_factor_rate_limit_user_id_key", "user_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor_rate_limit")

class AppSetting(Base):
    __tablename__ = "app_setting"

    key: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[str]
