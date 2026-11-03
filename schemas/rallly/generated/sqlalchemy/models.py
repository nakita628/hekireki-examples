from sqlalchemy import ARRAY, Dialect, Enum, ForeignKey, Index, MetaData, Text, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import CITEXT, JSONB, TIMESTAMP
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


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class Subscription(Base):
    __tablename__ = "subscriptions"

    id: Mapped[str] = mapped_column(primary_key=True)
    price_id: Mapped[str]
    quantity: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    subscription_item_id: Mapped[str]
    amount: Mapped[int]
    discount_percent_off: Mapped[Optional[float]]
    discount_amount_off: Mapped[Optional[int]]
    status: Mapped[str] = mapped_column(Enum("incomplete", "incomplete_expired", "active", "paused", "trialing", "past_due", "canceled", "unpaid", name="subscription_status"))
    active: Mapped[bool]
    currency: Mapped[str]
    interval: Mapped[str] = mapped_column(Enum("month", "year", name="subscription_interval"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    period_start: Mapped[datetime]
    period_end: Mapped[datetime]
    cancel_at_period_end: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="subscriptions_user_id_fkey"))
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="subscriptions_space_id_fkey"))

    __table_args__ = (
        Index("subscriptions_user_id_idx", "user_id"),
        Index("subscriptions_space_id_idx", "space_id"),
    )

    user: Mapped["User"] = relationship(back_populates="subscriptions")
    space: Mapped["Space"] = relationship(back_populates="subscriptions")

class PaymentMethod(Base):
    __tablename__ = "payment_methods"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="payment_methods_user_id_fkey"))
    type: Mapped[str]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    user: Mapped["User"] = relationship(back_populates="payment_methods")

class NonprofitApplication(Base):
    __tablename__ = "nonprofit_applications"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="nonprofit_applications_space_id_fkey"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="nonprofit_applications_user_id_fkey"))
    organization_name: Mapped[str]
    website: Mapped[str]
    email_domain: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("approved", "rejected", "failed", name="nonprofit_application_status"))
    reason: Mapped[Optional[str]]
    model: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("nonprofit_applications_space_id_created_at_idx", "space_id", "created_at"),
    )

    space: Mapped["Space"] = relationship(back_populates="nonprofit_applications")
    user: Mapped["User"] = relationship(back_populates="nonprofit_applications")

class QueuedEmail(Base):
    __tablename__ = "queued_emails"

    id: Mapped[str] = mapped_column(primary_key=True)
    kind: Mapped[str]
    subject_id: Mapped[str]
    batch_id: Mapped[str]
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="queued_emails_user_id_fkey"))
    status: Mapped[str] = mapped_column(Enum("pending", "sent", "skipped", "failed", name="queued_email_status"), default="pending", server_default=text("'pending'"))
    send_after: Mapped[Optional[datetime]]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    claimed_at: Mapped[Optional[datetime]]
    sent_at: Mapped[Optional[datetime]]
    last_error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("queued_emails_status_created_at_idx", "status", "created_at"),
        Index("queued_emails_status_updated_at_idx", "status", "updated_at"),
        Index("queued_emails_batch_id_idx", "batch_id"),
        Index("queued_emails_user_id_status_idx", "user_id", "status"),
    )

    user: Mapped["User"] = relationship(back_populates="queued_emails")

class EventType(Base):
    __tablename__ = "event_types"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="event_types_space_id_fkey"))
    host_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="event_types_host_id_fkey"))
    name: Mapped[str]
    duration: Mapped[int] = mapped_column("duration_minutes")
    capacity: Mapped[Optional[int]]
    description: Mapped[Optional[str]]
    location: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    deleted_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("event_types_space_id_idx", "space_id"),
        Index("event_types_host_id_idx", "host_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="event_types")
    host: Mapped["User"] = relationship(back_populates="hosted_event_types")
    slots: Mapped[list["SheetSlot"]] = relationship(passive_deletes="all", back_populates="event_type")
    scheduled_events: Mapped[list["ScheduledEvent"]] = relationship(back_populates="event_type")

class ScheduledEvent(Base):
    __tablename__ = "scheduled_events"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="scheduled_events_user_id_fkey"))
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="scheduled_events_space_id_fkey"))
    event_type_id: Mapped[Optional[str]] = mapped_column(ForeignKey("event_types.id", ondelete="SET NULL", onupdate="CASCADE", name="scheduled_events_event_type_id_fkey"))
    sheet_slot_id: Mapped[Optional[str]] = mapped_column(ForeignKey("sheet_slots.id", ondelete="SET NULL", onupdate="CASCADE", name="scheduled_events_sheet_slot_id_fkey"))
    title: Mapped[str]
    description: Mapped[Optional[str]]
    location: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    conferencing: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    capacity: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(Enum("confirmed", "canceled", "unconfirmed", name="scheduled_event_status"), default="confirmed", server_default=text("'confirmed'"))
    time_zone: Mapped[Optional[str]]
    start: Mapped[datetime]
    end: Mapped[datetime]
    all_day: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    deleted_at: Mapped[Optional[datetime]]
    sequence: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    uid: Mapped[str]
    hide_attendees: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("scheduled_events_sheet_slot_id_key", "sheet_slot_id", unique=True),
        Index("scheduled_events_uid_key", "uid", unique=True),
        Index("scheduled_events_space_id_idx", "space_id"),
        Index("scheduled_events_user_id_idx", "user_id"),
        Index("scheduled_events_event_type_id_idx", "event_type_id"),
        Index("scheduled_events_space_id_status_start_idx", "space_id", "status", "start"),
    )

    user: Mapped["User"] = relationship(back_populates="scheduled_events")
    space: Mapped["Space"] = relationship(back_populates="scheduled_events")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="scheduled_events")
    sheet_slot: Mapped[Optional["SheetSlot"]] = relationship(back_populates="scheduled_event")
    rescheduled_dates: Mapped[list["RescheduledEventDate"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scheduled_event")
    invites: Mapped[list["ScheduledEventInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scheduled_event")
    polls: Mapped[list["Poll"]] = relationship(back_populates="scheduled_event")

class RescheduledEventDate(Base):
    __tablename__ = "rescheduled_event_dates"

    id: Mapped[str] = mapped_column(primary_key=True)
    scheduled_event_id: Mapped[str] = mapped_column(ForeignKey("scheduled_events.id", ondelete="CASCADE", onupdate="CASCADE", name="rescheduled_event_dates_scheduled_event_id_fkey"))
    start: Mapped[datetime]
    end: Mapped[datetime]
    all_day: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("rescheduled_event_dates_scheduled_event_id_idx", "scheduled_event_id"),
    )

    scheduled_event: Mapped["ScheduledEvent"] = relationship(back_populates="rescheduled_dates")

class ScheduledEventInvite(Base):
    __tablename__ = "scheduled_event_invites"

    id: Mapped[str] = mapped_column(primary_key=True)
    uid: Mapped[str]
    scheduled_event_id: Mapped[str] = mapped_column(ForeignKey("scheduled_events.id", ondelete="CASCADE", onupdate="CASCADE", name="scheduled_event_invites_scheduled_event_id_fkey"))
    invitee_name: Mapped[str]
    invitee_email: Mapped[str] = mapped_column(CITEXT)
    invitee_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="scheduled_event_invites_invitee_id_fkey"))
    invitee_time_zone: Mapped[Optional[str]]
    invitee_locale: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("pending", "accepted", "declined", "tentative", name="scheduled_event_invite_status"), default="pending", server_default=text("'pending'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("scheduled_event_invites_uid_key", "uid", unique=True),
        Index("scheduled_event_invites_invitee_id_idx", "invitee_id"),
        Index("scheduled_event_invites_invitee_email_idx", "invitee_email"),
        Index("scheduled_event_invites_scheduled_event_id_invitee_email_key", "scheduled_event_id", "invitee_email", unique=True),
    )

    scheduled_event: Mapped["ScheduledEvent"] = relationship(back_populates="invites")
    user: Mapped[Optional["User"]] = relationship(back_populates="scheduled_event_invites")

class InstanceSettings(Base):
    __tablename__ = "instance_settings"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=False)
    disable_user_registration: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    instance_id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, server_default=text("gen_random_uuid()"))
    app_name: Mapped[Optional[str]]
    primary_color: Mapped[Optional[str]]
    primary_color_dark: Mapped[Optional[str]]
    logo: Mapped[Optional[str]]
    logo_dark: Mapped[Optional[str]]
    logo_icon: Mapped[Optional[str]]
    hide_attribution: Mapped[Optional[bool]]
    footer_links: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("instance_settings_instance_id_key", "instance_id", unique=True),
    )

class Credential(Base):
    __tablename__ = "credentials"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="credentials_user_id_fkey"))
    provider: Mapped[str]
    provider_account_id: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("OAUTH", name="CredentialType"))
    secret: Mapped[str]
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    expires_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("credential_expiry_idx", "expires_at"),
        Index("credentials_user_id_provider_provider_account_id_key", "user_id", "provider", "provider_account_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="credentials")
    calendar_connections: Mapped[list["CalendarConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")
    conferencing_connections: Mapped[list["ConferencingConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")

class CalendarConnection(Base):
    __tablename__ = "calendar_connections"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="calendar_connections_user_id_fkey"))
    provider: Mapped[str]
    integration_id: Mapped[str]
    provider_account_id: Mapped[str]
    email: Mapped[str]
    display_name: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    credential_id: Mapped[str] = mapped_column(ForeignKey("credentials.id", ondelete="CASCADE", onupdate="CASCADE", name="calendar_connections_credential_id_fkey"))

    __table_args__ = (
        Index("calendar_connections_user_id_provider_provider_account_id_key", "user_id", "provider", "provider_account_id", unique=True),
    )

    credential: Mapped["Credential"] = relationship(back_populates="calendar_connections")
    user: Mapped["User"] = relationship(back_populates="calendar_connections")
    provider_calendars: Mapped[list["ProviderCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="calendar_connection")

class ProviderCalendar(Base):
    __tablename__ = "provider_calendars"

    id: Mapped[str] = mapped_column(primary_key=True)
    calendar_connection_id: Mapped[str] = mapped_column(ForeignKey("calendar_connections.id", ondelete="CASCADE", onupdate="CASCADE", name="provider_calendars_calendar_connection_id_fkey"))
    provider_calendar_id: Mapped[str]
    name: Mapped[str]
    is_primary: Mapped[bool] = mapped_column("primary", default=False, server_default=text("false"))
    time_zone: Mapped[Optional[str]]
    is_selected: Mapped[bool] = mapped_column("selected", default=False, server_default=text("false"))
    is_deleted: Mapped[bool] = mapped_column("deleted", default=False, server_default=text("false"))
    is_writable: Mapped[bool] = mapped_column("writable", default=False, server_default=text("false"))
    provider_data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_synced_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("connection_selected_idx", "calendar_connection_id", "selected"),
        Index("primary_calendar_idx", "primary"),
        Index("sync_time_idx", "last_synced_at"),
        Index("provider_calendars_calendar_connection_id_provider_calendar_key", "calendar_connection_id", "provider_calendar_id", unique=True),
    )

    calendar_connection: Mapped["CalendarConnection"] = relationship(back_populates="provider_calendars")
    user_default_destination: Mapped[list["User"]] = relationship(back_populates="default_destination_calendar")

class ConferencingConnection(Base):
    __tablename__ = "conferencing_connections"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="conferencing_connections_user_id_fkey"))
    provider: Mapped[str]
    integration_id: Mapped[str]
    provider_account_id: Mapped[str]
    email: Mapped[str]
    display_name: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    credential_id: Mapped[str] = mapped_column(ForeignKey("credentials.id", ondelete="CASCADE", onupdate="CASCADE", name="conferencing_connections_credential_id_fkey"))

    __table_args__ = (
        Index("conferencing_connections_user_id_provider_provider_account__key", "user_id", "provider", "provider_account_id", unique=True),
    )

    credential: Mapped["Credential"] = relationship(back_populates="conferencing_connections")
    user: Mapped["User"] = relationship(back_populates="conferencing_connections")

class License(Base):
    __tablename__ = "licenses"

    id: Mapped[str] = mapped_column(primary_key=True)
    license_key: Mapped[str]
    idempotency_key: Mapped[Optional[str]]
    version: Mapped[Optional[int]]
    type: Mapped[str] = mapped_column(Enum("PLUS", "ORGANIZATION", "ENTERPRISE", name="LicenseType"))
    seats: Mapped[Optional[int]]
    issued_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[Optional[datetime]]
    licensee_email: Mapped[Optional[str]]
    licensee_name: Mapped[Optional[str]]
    white_label_addon: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "REVOKED", name="LicenseStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))

    __table_args__ = (
        Index("licenses_license_key_key", "license_key", unique=True),
        Index("licenses_idempotency_key_key", "idempotency_key", unique=True),
    )

    validations: Mapped[list["LicenseValidation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="license")

class LicenseValidation(Base):
    __tablename__ = "license_validations"

    id: Mapped[str] = mapped_column(primary_key=True)
    license_id: Mapped[str] = mapped_column(ForeignKey("licenses.id", ondelete="CASCADE", onupdate="CASCADE", name="license_validations_license_id_fkey"))
    ip_address: Mapped[Optional[str]]
    fingerprint: Mapped[Optional[str]]
    validated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_agent: Mapped[Optional[str]]

    license: Mapped["License"] = relationship(back_populates="validations")

class InstanceLicense(Base):
    __tablename__ = "instance_licenses"

    id: Mapped[str] = mapped_column(primary_key=True)
    license_key: Mapped[str]
    version: Mapped[Optional[int]]
    type: Mapped[str] = mapped_column(Enum("PLUS", "ORGANIZATION", "ENTERPRISE", name="LicenseType"))
    seats: Mapped[Optional[int]]
    issued_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[Optional[datetime]]
    licensee_email: Mapped[Optional[str]]
    licensee_name: Mapped[Optional[str]]
    white_label_addon: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "REVOKED", name="LicenseStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))

    __table_args__ = (
        Index("instance_licenses_license_key_key", "license_key", unique=True),
    )

class UserNotificationPreferences(Base):
    __tablename__ = "user_notification_preferences"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="user_notification_preferences_user_id_fkey"))
    prefs: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("user_notification_preferences_user_id_key", "user_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="notification_preferences")

class Poll(Base):
    __tablename__ = "polls"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    deadline: Mapped[Optional[datetime]]
    title: Mapped[str]
    description: Mapped[Optional[str]]
    location: Mapped[Optional[str]]
    conferencing: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="polls_user_id_fkey"))
    time_zone: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("open", "closed", "scheduled", "canceled", name="poll_status"), default="open", server_default=text("'open'"))
    closed_reason: Mapped[Optional[str]] = mapped_column(Enum("auto", "manual", name="poll_closed_reason"))
    kind: Mapped[str] = mapped_column(Enum("date", "time", name="poll_kind"), default="date", server_default=text("'date'"))
    deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    deleted_at: Mapped[Optional[datetime]]
    event_id: Mapped[Optional[str]]
    scheduled_event_id: Mapped[Optional[str]] = mapped_column(ForeignKey("scheduled_events.id", ondelete="SET NULL", onupdate="CASCADE", name="polls_scheduled_event_id_fkey"))
    hide_participants: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    hide_scores: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    disable_comments: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    require_participant_email: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    allow_tentative_votes: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    muted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    space_id: Mapped[Optional[str]] = mapped_column(ForeignKey("spaces.id", ondelete="SET NULL", onupdate="CASCADE", name="polls_space_id_fkey"))

    __table_args__ = (
        Index("polls_id_key", "id", unique=True),
        Index("polls_event_id_key", "event_id", unique=True),
        Index("polls_space_id_idx", "space_id"),
        Index("polls_user_id_idx", "user_id"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="polls")
    scheduled_event: Mapped[Optional["ScheduledEvent"]] = relationship(back_populates="polls")
    space: Mapped[Optional["Space"]] = relationship(back_populates="polls")
    options: Mapped[list["Option"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")
    participants: Mapped[list["Participant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")
    comments: Mapped[list["Comment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")
    votes: Mapped[list["Vote"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")
    invites: Mapped[list["PollInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")
    activities: Mapped[list["PollActivity"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="poll")

class Participant(Base):
    __tablename__ = "participants"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    email: Mapped[Optional[str]]
    note: Mapped[Optional[str]]
    token: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="participants_user_id_fkey"))
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="participants_poll_id_fkey"))
    locale: Mapped[Optional[str]]
    time_zone: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("participants_token_key", "token", unique=True),
        Index("participants_poll_id_idx", "poll_id"),
        Index("participants_user_id_idx", "user_id"),
    )

    poll: Mapped["Poll"] = relationship(back_populates="participants")
    user: Mapped[Optional["User"]] = relationship(back_populates="participants")
    votes: Mapped[list["Vote"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="participant")
    invite: Mapped[Optional["PollInvite"]] = relationship(back_populates="participant")

class PollInvite(Base):
    __tablename__ = "poll_invites"

    id: Mapped[str] = mapped_column(primary_key=True)
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="poll_invites_poll_id_fkey"))
    email: Mapped[str] = mapped_column(CITEXT)
    token: Mapped[str]
    opened_at: Mapped[Optional[datetime]]
    reminded_at: Mapped[Optional[datetime]]
    revoked_at: Mapped[Optional[datetime]]
    participant_id: Mapped[Optional[str]] = mapped_column(ForeignKey("participants.id", ondelete="SET NULL", onupdate="CASCADE", name="poll_invites_participant_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("poll_invites_token_key", "token", unique=True),
        Index("poll_invites_participant_id_key", "participant_id", unique=True),
        Index("poll_invites_poll_id_email_key", "poll_id", "email", unique=True),
    )

    poll: Mapped["Poll"] = relationship(back_populates="invites")
    participant: Mapped[Optional["Participant"]] = relationship(back_populates="invite")

class PollActivity(Base):
    __tablename__ = "poll_activities"

    id: Mapped[str] = mapped_column(primary_key=True)
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="poll_activities_poll_id_fkey"))
    type: Mapped[str]
    user_id: Mapped[Optional[str]]
    participant_id: Mapped[Optional[str]]
    invite_id: Mapped[Optional[str]]
    option_id: Mapped[Optional[str]]
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("poll_activities_poll_id_created_at_idx", "poll_id", "created_at"),
        Index("poll_activities_poll_id_participant_id_idx", "poll_id", "participant_id"),
        Index("poll_activities_poll_id_option_id_idx", "poll_id", "option_id"),
        Index("poll_activities_poll_id_invite_id_idx", "poll_id", "invite_id"),
        Index("poll_activities_user_id_idx", "user_id"),
    )

    poll: Mapped["Poll"] = relationship(back_populates="activities")

class Option(Base):
    __tablename__ = "options"

    id: Mapped[str] = mapped_column(primary_key=True)
    start_time: Mapped[datetime] = mapped_column(UtcDateTime(precision=0))
    duration: Mapped[int] = mapped_column("duration_minutes", default=0, server_default=text("0"))
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="options_poll_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("options_poll_id_idx", "poll_id"),
    )

    poll: Mapped["Poll"] = relationship(back_populates="options")
    votes: Mapped[list["Vote"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="option")

class Vote(Base):
    __tablename__ = "votes"

    id: Mapped[str] = mapped_column(primary_key=True)
    participant_id: Mapped[str] = mapped_column(ForeignKey("participants.id", ondelete="CASCADE", onupdate="CASCADE", name="votes_participant_id_fkey"))
    option_id: Mapped[str] = mapped_column(ForeignKey("options.id", ondelete="CASCADE", onupdate="CASCADE", name="votes_option_id_fkey"))
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="votes_poll_id_fkey"))
    type: Mapped[str] = mapped_column(Enum("yes", "no", "ifNeedBe", name="vote_type"), default="yes", server_default=text("'yes'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("votes_poll_id_idx", "poll_id"),
        Index("votes_participant_id_idx", "participant_id"),
        Index("votes_option_id_idx", "option_id"),
    )

    participant: Mapped["Participant"] = relationship(back_populates="votes")
    option: Mapped["Option"] = relationship(back_populates="votes")
    poll: Mapped["Poll"] = relationship(back_populates="votes")

class Comment(Base):
    __tablename__ = "comments"

    id: Mapped[str] = mapped_column(primary_key=True)
    content: Mapped[str]
    poll_id: Mapped[str] = mapped_column(ForeignKey("polls.id", ondelete="CASCADE", onupdate="CASCADE", name="comments_poll_id_fkey"))
    author_name: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="comments_user_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("comments_poll_id_idx", "poll_id"),
        Index("comments_user_id_idx", "user_id"),
    )

    poll: Mapped["Poll"] = relationship(back_populates="comments")
    user: Mapped[Optional["User"]] = relationship(back_populates="comments")

class RegisteredInstance(Base):
    __tablename__ = "registered_instances"

    id: Mapped[str] = mapped_column(primary_key=True)
    instance_id: Mapped[str]
    version: Mapped[str]
    first_seen_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_seen_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("registered_instances_instance_id_key", "instance_id", unique=True),
    )

class Sheet(Base):
    __tablename__ = "sheets"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="sheets_space_id_fkey"))
    host_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="sheets_host_id_fkey"))
    title: Mapped[str]
    description: Mapped[Optional[str]]
    url_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    deleted_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("sheets_url_id_key", "url_id", unique=True),
        Index("sheets_space_id_idx", "space_id"),
        Index("sheets_host_id_idx", "host_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="sheets")
    host: Mapped["User"] = relationship(back_populates="hosted_sheets")
    slots: Mapped[list["SheetSlot"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="sheet")

class SheetSlot(Base):
    __tablename__ = "sheet_slots"

    id: Mapped[str] = mapped_column(primary_key=True)
    sheet_id: Mapped[str] = mapped_column(ForeignKey("sheets.id", ondelete="CASCADE", onupdate="CASCADE", name="sheet_slots_sheet_id_fkey"))
    event_type_id: Mapped[str] = mapped_column(ForeignKey("event_types.id", ondelete="RESTRICT", onupdate="CASCADE", name="sheet_slots_event_type_id_fkey"))
    start_time: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("sheet_slots_sheet_id_start_time_idx", "sheet_id", "start_time"),
        Index("sheet_slots_event_type_id_idx", "event_type_id"),
    )

    sheet: Mapped["Sheet"] = relationship(back_populates="slots")
    event_type: Mapped["EventType"] = relationship(back_populates="slots")
    scheduled_event: Mapped[Optional["ScheduledEvent"]] = relationship(back_populates="sheet_slot")

class Space(Base):
    __tablename__ = "spaces"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    image: Mapped[Optional[str]]
    owner_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="spaces_owner_id_fkey"))
    tier: Mapped[str] = mapped_column(Enum("hobby", "pro", name="space_tiers"), default="hobby", server_default=text("'hobby'"))
    space_type: Mapped[Optional[str]] = mapped_column(Enum("personal", "work", name="space_types"))
    industry: Mapped[Optional[str]]
    primary_color: Mapped[Optional[str]]
    show_branding: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    hide_attribution: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    shared: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    nonprofit_discount_granted_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("spaces_owner_id_idx", "owner_id"),
    )

    owner: Mapped["User"] = relationship(back_populates="spaces")
    polls: Mapped[list["Poll"]] = relationship(back_populates="space")
    scheduled_events: Mapped[list["ScheduledEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    event_types: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    sheets: Mapped[list["Sheet"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    subscriptions: Mapped[list["Subscription"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    members: Mapped[list["SpaceMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    member_invites: Mapped[list["SpaceMemberInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    api_keys: Mapped[list["SpaceApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    nonprofit_applications: Mapped[list["NonprofitApplication"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    webhooks: Mapped[list["SpaceWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")

class SpaceMember(Base):
    __tablename__ = "space_members"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="space_members_space_id_fkey"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="space_members_user_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", name="SpaceMemberRole"), default="MEMBER", server_default=text("'MEMBER'"))
    last_selected_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("space_members_user_id_idx", "user_id"),
        Index("space_members_space_id_user_id_key", "space_id", "user_id", unique=True),
    )

    space: Mapped["Space"] = relationship(back_populates="members")
    user: Mapped["User"] = relationship(back_populates="member_of")

class SpaceMemberInvite(Base):
    __tablename__ = "space_member_invites"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="space_member_invites_space_id_fkey"))
    email: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    role: Mapped[str] = mapped_column(Enum("ADMIN", "MEMBER", name="SpaceMemberRole"), default="MEMBER", server_default=text("'MEMBER'"))
    inviter_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="space_member_invites_inviter_id_fkey"))

    __table_args__ = (
        Index("space_member_invites_space_id_email_key", "space_id", "email", unique=True),
    )

    invited_by: Mapped["User"] = relationship(back_populates="space_member_invites")
    space: Mapped["Space"] = relationship(back_populates="member_invites")

class SpaceApiKey(Base):
    __tablename__ = "space_api_keys"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="space_api_keys_space_id_fkey"))
    name: Mapped[str]
    prefix: Mapped[str]
    hashed_key: Mapped[str]
    last_used_at: Mapped[Optional[datetime]]
    expires_at: Mapped[Optional[datetime]]
    revoked_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("space_api_keys_prefix_key", "prefix", unique=True),
        Index("space_api_key_space_idx", "space_id"),
        Index("space_api_key_expires_idx", "expires_at"),
        Index("space_api_key_revoked_idx", "revoked_at"),
    )

    space: Mapped["Space"] = relationship(back_populates="api_keys")

class SpaceWebhook(Base):
    __tablename__ = "space_webhooks"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("spaces.id", ondelete="CASCADE", onupdate="CASCADE", name="space_webhooks_space_id_fkey"))
    url: Mapped[str]
    secret: Mapped[str]
    events: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    version: Mapped[str]
    cursor: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_delivered_at: Mapped[Optional[datetime]]
    last_failed_at: Mapped[Optional[datetime]]
    consecutive_failures: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("space_webhooks_space_id_idx", "space_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="webhooks")
    deliveries: Mapped[list["WebhookDelivery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="webhook")

class WebhookDelivery(Base):
    __tablename__ = "webhook_deliveries"

    id: Mapped[str] = mapped_column(primary_key=True)
    webhook_id: Mapped[str] = mapped_column(ForeignKey("space_webhooks.id", ondelete="CASCADE", onupdate="CASCADE", name="webhook_deliveries_webhook_id_fkey"))
    activity_id: Mapped[str]
    event_type: Mapped[str]
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    status: Mapped[str] = mapped_column(Enum("pending", "in_flight", "succeeded", "failed", "exhausted", name="webhook_delivery_status"), default="pending", server_default=text("'pending'"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    next_attempt_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_response_status: Mapped[Optional[int]]
    last_error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("webhook_deliveries_status_next_attempt_at_idx", "status", "next_attempt_at"),
        Index("webhook_deliveries_webhook_id_activity_id_key", "webhook_id", "activity_id", unique=True),
    )

    webhook: Mapped["SpaceWebhook"] = relationship(back_populates="deliveries")

class Account(Base):
    __tablename__ = "accounts"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="accounts_user_id_fkey"))
    provider: Mapped[str]
    provider_account_id: Mapped[str]
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    access_token_expires_at: Mapped[Optional[datetime]]
    refresh_token_expires_at: Mapped[Optional[datetime]]
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("accounts_user_id_idx", "user_id"),
        Index("accounts_provider_provider_account_id_key", "provider", "provider_account_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    email: Mapped[str] = mapped_column(CITEXT)
    email_verified: Mapped[Optional[bool]]
    image: Mapped[Optional[str]]
    time_zone: Mapped[Optional[str]]
    week_start: Mapped[Optional[int]]
    time_format: Mapped[Optional[str]] = mapped_column(Enum("hours12", "hours24", name="time_format"))
    locale: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    customer_id: Mapped[Optional[str]]
    banned: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    banned_at: Mapped[Optional[datetime]]
    ban_reason: Mapped[Optional[str]]
    ban_expires: Mapped[Optional[datetime]]
    role: Mapped[str] = mapped_column(Enum("admin", "user", name="user_role"), default="user", server_default=text("'user'"))
    last_login_method: Mapped[Optional[str]]
    is_anonymous: Mapped[bool] = mapped_column("anonymous", default=False, server_default=text("false"))
    last_seen_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_at: Mapped[Optional[datetime]]
    job_title: Mapped[Optional[str]]
    review_requested_at: Mapped[Optional[datetime]]
    default_destination_calendar_id: Mapped[Optional[str]] = mapped_column(ForeignKey("provider_calendars.id", ondelete="SET NULL", onupdate="CASCADE", name="users_default_destination_calendar_id_fkey"))

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
        Index("users_deleted_at_idx", "deleted_at"),
        Index("users_anonymous_last_seen_at_idx", "anonymous", "last_seen_at"),
    )

    default_destination_calendar: Mapped[Optional["ProviderCalendar"]] = relationship(back_populates="user_default_destination")
    comments: Mapped[list["Comment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    polls: Mapped[list["Poll"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    queued_emails: Mapped[list["QueuedEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    participants: Mapped[list["Participant"]] = relationship(back_populates="user")
    payment_methods: Mapped[list["PaymentMethod"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    space_member_invites: Mapped[list["SpaceMemberInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="invited_by")
    nonprofit_applications: Mapped[list["NonprofitApplication"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    subscriptions: Mapped[list["Subscription"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    spaces: Mapped[list["Space"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    member_of: Mapped[list["SpaceMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scheduled_events: Mapped[list["ScheduledEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scheduled_event_invites: Mapped[list["ScheduledEventInvite"]] = relationship(back_populates="user")
    hosted_event_types: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="host")
    hosted_sheets: Mapped[list["Sheet"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="host")
    calendar_connections: Mapped[list["CalendarConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    conferencing_connections: Mapped[list["ConferencingConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    credentials: Mapped[list["Credential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(foreign_keys="Session.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    impersonated_sessions: Mapped[list["Session"]] = relationship(foreign_keys="Session.impersonated_by", back_populates="impersonated_by_user")
    notification_preferences: Mapped[Optional["UserNotificationPreferences"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Session(Base):
    __tablename__ = "sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    expires_at: Mapped[datetime]
    token: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    ip_address: Mapped[Optional[str]]
    user_agent: Mapped[Optional[str]]
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="sessions_user_id_fkey"))
    impersonated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="sessions_impersonated_by_fkey"))

    __table_args__ = (
        Index("sessions_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(foreign_keys=[user_id], back_populates="sessions")
    impersonated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[impersonated_by], back_populates="impersonated_sessions")

class Verification(Base):
    __tablename__ = "verifications"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    value: Mapped[str]
    expires_at: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("verifications_identifier_key", "identifier", unique=True),
        Index("verifications_identifier_idx", "identifier"),
    )
