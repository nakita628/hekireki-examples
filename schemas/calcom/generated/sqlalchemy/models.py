from sqlalchemy import ARRAY, Column, Date, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, Integer, MetaData, PrimaryKeyConstraint, Table, Text, Time, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, date, time as time_type, timezone
import uuid as uuid_mod
import uuid6


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

user_eventtype = Table(
    "_user_eventtype",
    Base.metadata,
    Column("A", Integer, ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="_user_eventtype_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="_user_eventtype_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_user_eventtype_AB_pkey"),
    Index("_user_eventtype_B_index", "B"),
)

platform_oauth_client_to_user = Table(
    "_PlatformOAuthClientToUser",
    Base.metadata,
    Column("A", Text, ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="_PlatformOAuthClientToUser_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="_PlatformOAuthClientToUser_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_PlatformOAuthClientToUser_AB_pkey"),
    Index("_PlatformOAuthClientToUser_B_index", "B"),
)


class Host(Base):
    __tablename__ = "Host"

    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Host_userId_fkey"), primary_key=True)
    event_type_id: Mapped[int] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="Host_eventTypeId_fkey"), primary_key=True)
    is_fixed: Mapped[bool] = mapped_column("isFixed", default=False, server_default=text("false"))
    priority: Mapped[Optional[int]]
    weight: Mapped[Optional[int]]
    weight_adjustment: Mapped[Optional[int]] = mapped_column("weightAdjustment")
    schedule_id: Mapped[Optional[int]] = mapped_column("scheduleId", ForeignKey("Schedule.id", ondelete="SET NULL", onupdate="CASCADE", name="Host_scheduleId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("HostGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="Host_groupId_fkey"))
    member_id: Mapped[Optional[int]] = mapped_column("memberId", ForeignKey("Membership.id", ondelete="CASCADE", onupdate="CASCADE", name="Host_memberId_fkey"))

    __table_args__ = (
        Index("Host_memberId_idx", "memberId"),
        Index("Host_userId_idx", "userId"),
        Index("Host_eventTypeId_idx", "eventTypeId"),
        Index("Host_scheduleId_idx", "scheduleId"),
    )

    user: Mapped["User"] = relationship(back_populates="hosts")
    event_type: Mapped["EventType"] = relationship(back_populates="hosts")
    schedule: Mapped[Optional["Schedule"]] = relationship(back_populates="host")
    group: Mapped[Optional["HostGroup"]] = relationship(back_populates="hosts")
    member: Mapped[Optional["Membership"]] = relationship(back_populates="host")
    location: Mapped[Optional["HostLocation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="host")

class HostGroup(Base):
    __tablename__ = "HostGroup"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="HostGroup_eventTypeId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("HostGroup_name_idx", "name"),
        Index("HostGroup_eventTypeId_idx", "eventTypeId"),
    )

    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="host_groups")
    hosts: Mapped[list["Host"]] = relationship(back_populates="group")

class HostLocation(Base):
    __tablename__ = "HostLocation"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[int] = mapped_column("userId")
    event_type_id: Mapped[int] = mapped_column("eventTypeId")
    type: Mapped[str]
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="SET NULL", onupdate="CASCADE", name="HostLocation_credentialId_fkey"))
    link: Mapped[Optional[str]]
    address: Mapped[Optional[str]]
    phone_number: Mapped[Optional[str]] = mapped_column("phoneNumber")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("HostLocation_credentialId_idx", "credentialId"),
        Index("HostLocation_eventTypeId_idx", "eventTypeId"),
        Index("HostLocation_userId_eventTypeId_key", "userId", "eventTypeId", unique=True),
        ForeignKeyConstraint(["userId", "eventTypeId"], ["Host.userId", "Host.eventTypeId"], ondelete="CASCADE", onupdate="CASCADE", name="HostLocation_userId_eventTypeId_fkey"),
    )

    host: Mapped["Host"] = relationship(back_populates="location")
    credential: Mapped[Optional["Credential"]] = relationship(back_populates="host_locations")

class CalVideoSettings(Base):
    __tablename__ = "CalVideoSettings"

    event_type_id: Mapped[int] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="CalVideoSettings_eventTypeId_fkey"), primary_key=True, autoincrement=False)
    disable_recording_for_organizer: Mapped[bool] = mapped_column("disableRecordingForOrganizer", default=False, server_default=text("false"))
    disable_recording_for_guests: Mapped[bool] = mapped_column("disableRecordingForGuests", default=False, server_default=text("false"))
    enable_automatic_transcription: Mapped[bool] = mapped_column("enableAutomaticTranscription", default=False, server_default=text("false"))
    enable_automatic_recording_for_organizer: Mapped[bool] = mapped_column("enableAutomaticRecordingForOrganizer", default=False, server_default=text("false"))
    redirect_url_on_exit: Mapped[Optional[str]] = mapped_column("redirectUrlOnExit")
    disable_transcription_for_guests: Mapped[bool] = mapped_column("disableTranscriptionForGuests", default=False, server_default=text("false"))
    disable_transcription_for_organizer: Mapped[bool] = mapped_column("disableTranscriptionForOrganizer", default=False, server_default=text("false"))
    require_email_for_guests: Mapped[bool] = mapped_column("requireEmailForGuests", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    event_type: Mapped["EventType"] = relationship(back_populates="cal_video_settings")

class VideoCallGuest(Base):
    __tablename__ = "VideoCallGuest"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    booking_uid: Mapped[str] = mapped_column("bookingUid")
    email: Mapped[str]
    name: Mapped[str]
    joined_at: Mapped[Optional[datetime]] = mapped_column("joinedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("VideoCallGuest_bookingUid_idx", "bookingUid"),
        Index("VideoCallGuest_email_idx", "email"),
        Index("VideoCallGuest_bookingUid_email_key", "bookingUid", "email", unique=True),
    )

class EventType(Base):
    __tablename__ = "EventType"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    title: Mapped[str]
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    interface_language: Mapped[Optional[str]] = mapped_column("interfaceLanguage")
    position: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    locations: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    length: Mapped[int]
    offset_start: Mapped[int] = mapped_column("offsetStart", default=0, server_default=text("0"))
    hidden: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="EventType_userId_fkey"))
    profile_id: Mapped[Optional[int]] = mapped_column("profileId", ForeignKey("Profile.id", ondelete="SET NULL", onupdate="CASCADE", name="EventType_profileId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="EventType_teamId_fkey"))
    use_event_level_selected_calendars: Mapped[bool] = mapped_column("useEventLevelSelectedCalendars", default=False, server_default=text("false"))
    event_name: Mapped[Optional[str]] = mapped_column("eventName")
    parent_id: Mapped[Optional[int]] = mapped_column("parentId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="EventType_parentId_fkey"))
    booking_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("bookingFields", JSONB(none_as_null=True))
    time_zone: Mapped[Optional[str]] = mapped_column("timeZone")
    period_type: Mapped[str] = mapped_column("periodType", Enum("unlimited", "rolling", "rolling_window", "range", name="PeriodType"), default="unlimited", server_default=text("'unlimited'"))
    period_start_date: Mapped[Optional[datetime]] = mapped_column("periodStartDate")
    period_end_date: Mapped[Optional[datetime]] = mapped_column("periodEndDate")
    period_days: Mapped[Optional[int]] = mapped_column("periodDays")
    period_count_calendar_days: Mapped[Optional[bool]] = mapped_column("periodCountCalendarDays")
    lock_time_zone_toggle_on_booking_page: Mapped[bool] = mapped_column("lockTimeZoneToggleOnBookingPage", default=False, server_default=text("false"))
    locked_time_zone: Mapped[Optional[str]] = mapped_column("lockedTimeZone")
    requires_confirmation: Mapped[bool] = mapped_column("requiresConfirmation", default=False, server_default=text("false"))
    requires_confirmation_will_block_slot: Mapped[bool] = mapped_column("requiresConfirmationWillBlockSlot", default=False, server_default=text("false"))
    requires_confirmation_for_free_email: Mapped[bool] = mapped_column("requiresConfirmationForFreeEmail", default=False, server_default=text("false"))
    requires_booker_email_verification: Mapped[bool] = mapped_column("requiresBookerEmailVerification", default=False, server_default=text("false"))
    can_send_cal_video_transcription_emails: Mapped[bool] = mapped_column("canSendCalVideoTranscriptionEmails", default=True, server_default=text("true"))
    auto_translate_description_enabled: Mapped[bool] = mapped_column("autoTranslateDescriptionEnabled", default=False, server_default=text("false"))
    auto_translate_instant_meeting_title_enabled: Mapped[bool] = mapped_column("autoTranslateInstantMeetingTitleEnabled", default=True, server_default=text("true"))
    recurring_event: Mapped[Optional[dict[str, Any]]] = mapped_column("recurringEvent", JSONB(none_as_null=True))
    disable_guests: Mapped[bool] = mapped_column("disableGuests", default=False, server_default=text("false"))
    hide_calendar_notes: Mapped[bool] = mapped_column("hideCalendarNotes", default=False, server_default=text("false"))
    hide_calendar_event_details: Mapped[bool] = mapped_column("hideCalendarEventDetails", default=False, server_default=text("false"))
    minimum_booking_notice: Mapped[int] = mapped_column("minimumBookingNotice", default=120, server_default=text("120"))
    before_event_buffer: Mapped[int] = mapped_column("beforeEventBuffer", default=0, server_default=text("0"))
    after_event_buffer: Mapped[int] = mapped_column("afterEventBuffer", default=0, server_default=text("0"))
    seats_per_time_slot: Mapped[Optional[int]] = mapped_column("seatsPerTimeSlot")
    only_show_first_available_slot: Mapped[bool] = mapped_column("onlyShowFirstAvailableSlot", default=False, server_default=text("false"))
    show_optimized_slots: Mapped[Optional[bool]] = mapped_column("showOptimizedSlots", default=False, server_default=text("false"))
    disable_cancelling: Mapped[Optional[bool]] = mapped_column("disableCancelling", default=False, server_default=text("false"))
    disable_rescheduling: Mapped[Optional[bool]] = mapped_column("disableRescheduling", default=False, server_default=text("false"))
    minimum_reschedule_notice: Mapped[Optional[int]] = mapped_column("minimumRescheduleNotice")
    seats_show_attendees: Mapped[Optional[bool]] = mapped_column("seatsShowAttendees", default=False, server_default=text("false"))
    seats_show_availability_count: Mapped[Optional[bool]] = mapped_column("seatsShowAvailabilityCount", default=True, server_default=text("true"))
    scheduling_type: Mapped[Optional[str]] = mapped_column("schedulingType", Enum("roundRobin", "collective", "managed", name="SchedulingType"))
    schedule_id: Mapped[Optional[int]] = mapped_column("scheduleId", ForeignKey("Schedule.id", ondelete="SET NULL", onupdate="CASCADE", name="EventType_scheduleId_fkey"))
    allow_rescheduling_cancelled_bookings: Mapped[Optional[bool]] = mapped_column("allowReschedulingCancelledBookings", default=False, server_default=text("false"))
    price: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    currency: Mapped[str] = mapped_column(default="usd", server_default=text("'usd'"))
    slot_interval: Mapped[Optional[int]] = mapped_column("slotInterval")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    success_redirect_url: Mapped[Optional[str]] = mapped_column("successRedirectUrl")
    forward_params_success_redirect: Mapped[Optional[bool]] = mapped_column("forwardParamsSuccessRedirect", default=True, server_default=text("true"))
    booking_limits: Mapped[Optional[dict[str, Any]]] = mapped_column("bookingLimits", JSONB(none_as_null=True))
    duration_limits: Mapped[Optional[dict[str, Any]]] = mapped_column("durationLimits", JSONB(none_as_null=True))
    is_instant_event: Mapped[bool] = mapped_column("isInstantEvent", default=False, server_default=text("false"))
    instant_meeting_expiry_time_offset_in_seconds: Mapped[int] = mapped_column("instantMeetingExpiryTimeOffsetInSeconds", default=90, server_default=text("90"))
    instant_meeting_schedule_id: Mapped[Optional[int]] = mapped_column("instantMeetingScheduleId", ForeignKey("Schedule.id", ondelete="SET NULL", onupdate="CASCADE", name="EventType_instantMeetingScheduleId_fkey"))
    instant_meeting_parameters: Mapped[list[str]] = mapped_column("instantMeetingParameters", ARRAY(Text), nullable=True)
    assign_all_team_members: Mapped[bool] = mapped_column("assignAllTeamMembers", default=False, server_default=text("false"))
    assign_rrmembers_using_segment: Mapped[bool] = mapped_column("assignRRMembersUsingSegment", default=False, server_default=text("false"))
    rr_segment_query_value: Mapped[Optional[dict[str, Any]]] = mapped_column("rrSegmentQueryValue", JSONB(none_as_null=True))
    use_event_type_destination_calendar_email: Mapped[bool] = mapped_column("useEventTypeDestinationCalendarEmail", default=False, server_default=text("false"))
    is_rrweights_enabled: Mapped[bool] = mapped_column("isRRWeightsEnabled", default=False, server_default=text("false"))
    max_lead_threshold: Mapped[Optional[int]] = mapped_column("maxLeadThreshold")
    include_no_show_in_rrcalculation: Mapped[bool] = mapped_column("includeNoShowInRRCalculation", default=False, server_default=text("false"))
    allow_rescheduling_past_bookings: Mapped[bool] = mapped_column("allowReschedulingPastBookings", default=False, server_default=text("false"))
    hide_organizer_email: Mapped[bool] = mapped_column("hideOrganizerEmail", default=False, server_default=text("false"))
    max_active_bookings_per_booker: Mapped[Optional[int]] = mapped_column("maxActiveBookingsPerBooker")
    max_active_booking_per_booker_offer_reschedule: Mapped[bool] = mapped_column("maxActiveBookingPerBookerOfferReschedule", default=False, server_default=text("false"))
    custom_reply_to_email: Mapped[Optional[str]] = mapped_column("customReplyToEmail")
    event_type_color: Mapped[Optional[dict[str, Any]]] = mapped_column("eventTypeColor", JSONB(none_as_null=True))
    reschedule_with_same_round_robin_host: Mapped[bool] = mapped_column("rescheduleWithSameRoundRobinHost", default=False, server_default=text("false"))
    secondary_email_id: Mapped[Optional[int]] = mapped_column("secondaryEmailId", ForeignKey("SecondaryEmail.id", ondelete="CASCADE", onupdate="CASCADE", name="EventType_secondaryEmailId_fkey"))
    use_booker_timezone: Mapped[bool] = mapped_column("useBookerTimezone", default=False, server_default=text("false"))
    restriction_schedule_id: Mapped[Optional[int]] = mapped_column("restrictionScheduleId", ForeignKey("Schedule.id", ondelete="SET NULL", onupdate="CASCADE", name="EventType_restrictionScheduleId_fkey"))
    booking_requires_authentication: Mapped[bool] = mapped_column("bookingRequiresAuthentication", default=False, server_default=text("false"))
    rr_host_subset_enabled: Mapped[bool] = mapped_column("rrHostSubsetEnabled", default=False, server_default=text("false"))
    requires_cancellation_reason: Mapped[Optional[str]] = mapped_column("requiresCancellationReason", Enum("MANDATORY_BOTH", "MANDATORY_HOST_ONLY", "MANDATORY_ATTENDEE_ONLY", "OPTIONAL_BOTH", name="CancellationReasonRequirement"), default="MANDATORY_HOST_ONLY", server_default=text("'MANDATORY_HOST_ONLY'"))
    enable_per_host_locations: Mapped[bool] = mapped_column("enablePerHostLocations", default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("EventType_userId_idx", "userId"),
        Index("EventType_teamId_idx", "teamId"),
        Index("EventType_profileId_idx", "profileId"),
        Index("EventType_scheduleId_idx", "scheduleId"),
        Index("EventType_secondaryEmailId_idx", "secondaryEmailId"),
        Index("EventType_parentId_idx", "parentId"),
        Index("EventType_parentId_teamId_idx", "parentId", "teamId"),
        Index("EventType_restrictionScheduleId_idx", "restrictionScheduleId"),
        Index("EventType_userId_slug_key", "userId", "slug", unique=True),
        Index("EventType_teamId_slug_key", "teamId", "slug", unique=True),
        Index("EventType_userId_parentId_key", "userId", "parentId", unique=True),
    )

    owner: Mapped[Optional["User"]] = relationship(back_populates="owned_event_types")
    profile: Mapped[Optional["Profile"]] = relationship(back_populates="event_types")
    team: Mapped[Optional["Team"]] = relationship(back_populates="event_types")
    parent: Mapped[Optional["EventType"]] = relationship(remote_side=[id], back_populates="children")
    schedule: Mapped[Optional["Schedule"]] = relationship(foreign_keys=[schedule_id], back_populates="event_type")
    instant_meeting_schedule: Mapped[Optional["Schedule"]] = relationship(foreign_keys=[instant_meeting_schedule_id], back_populates="instant_meeting_events")
    secondary_email: Mapped[Optional["SecondaryEmail"]] = relationship(back_populates="event_types")
    restriction_schedule: Mapped[Optional["Schedule"]] = relationship(foreign_keys=[restriction_schedule_id], back_populates="restriction_schedule")
    hosts: Mapped[list["Host"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    hashed_link: Mapped[list["HashedLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    bookings: Mapped[list["Booking"]] = relationship(back_populates="event_type")
    availability: Mapped[list["Availability"]] = relationship(back_populates="event_type")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    custom_inputs: Mapped[list["EventTypeCustomInput"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    children: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")
    field_translations: Mapped[list["EventTypeTranslation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    selected_calendars: Mapped[list["SelectedCalendar"]] = relationship(back_populates="event_type")
    host_groups: Mapped[list["HostGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    destination_calendar: Mapped[Optional["DestinationCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    cal_video_settings: Mapped[Optional["CalVideoSettings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="event_type")
    users: Mapped[list["User"]] = relationship(secondary=user_eventtype, back_populates="event_types")

class Credential(Base):
    __tablename__ = "Credential"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    type: Mapped[str]
    key: Mapped[dict[str, Any]] = mapped_column(JSONB)
    encrypted_key: Mapped[Optional[str]] = mapped_column("encryptedKey")
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Credential_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Credential_teamId_fkey"))
    app_id: Mapped[Optional[str]] = mapped_column("appId", ForeignKey("App.slug", ondelete="CASCADE", onupdate="CASCADE", name="Credential_appId_fkey"))
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    payment_status: Mapped[Optional[str]] = mapped_column("paymentStatus")
    billing_cycle_start: Mapped[Optional[int]] = mapped_column("billingCycleStart")
    invalid: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    delegation_credential_id: Mapped[Optional[str]] = mapped_column("delegationCredentialId", ForeignKey("DelegationCredential.id", ondelete="CASCADE", onupdate="CASCADE", name="Credential_delegationCredentialId_fkey"))

    __table_args__ = (
        Index("Credential_appId_idx", "appId"),
        Index("Credential_subscriptionId_idx", "subscriptionId"),
        Index("Credential_invalid_idx", "invalid"),
        Index("Credential_userId_delegationCredentialId_idx", "userId", "delegationCredentialId"),
        Index("Credential_teamId_idx", "teamId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="credentials")
    team: Mapped[Optional["Team"]] = relationship(back_populates="credentials")
    app: Mapped[Optional["App"]] = relationship(back_populates="credentials")
    delegation_credential: Mapped[Optional["DelegationCredential"]] = relationship(back_populates="credentials")
    destination_calendars: Mapped[list["DestinationCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")
    selected_calendars: Mapped[list["SelectedCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")
    calendar_cache: Mapped[list["CalendarCache"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")
    references: Mapped[list["BookingReference"]] = relationship(back_populates="credential")
    integration_attribute_syncs: Mapped[list["IntegrationAttributeSync"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credential")
    host_locations: Mapped[list["HostLocation"]] = relationship(back_populates="credential")

class DestinationCalendar(Base):
    __tablename__ = "DestinationCalendar"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    integration: Mapped[str]
    external_id: Mapped[str] = mapped_column("externalId")
    primary_email: Mapped[Optional[str]] = mapped_column("primaryEmail")
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="DestinationCalendar_userId_fkey"))
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="DestinationCalendar_eventTypeId_fkey"))
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="CASCADE", onupdate="CASCADE", name="DestinationCalendar_credentialId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    delegation_credential_id: Mapped[Optional[str]] = mapped_column("delegationCredentialId", ForeignKey("DelegationCredential.id", ondelete="CASCADE", onupdate="CASCADE", name="DestinationCalendar_delegationCredentialId_fkey"))
    custom_calendar_reminder: Mapped[Optional[int]] = mapped_column("customCalendarReminder")

    __table_args__ = (
        Index("DestinationCalendar_userId_key", "userId", unique=True),
        Index("DestinationCalendar_eventTypeId_key", "eventTypeId", unique=True),
        Index("DestinationCalendar_userId_idx", "userId"),
        Index("DestinationCalendar_eventTypeId_idx", "eventTypeId"),
        Index("DestinationCalendar_credentialId_idx", "credentialId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="destination_calendar")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="destination_calendar")
    credential: Mapped[Optional["Credential"]] = relationship(back_populates="destination_calendars")
    delegation_credential: Mapped[Optional["DelegationCredential"]] = relationship(back_populates="destination_calendar")
    booking: Mapped[list["Booking"]] = relationship(back_populates="destination_calendar")

class TravelSchedule(Base):
    __tablename__ = "TravelSchedule"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="TravelSchedule_userId_fkey"))
    time_zone: Mapped[str] = mapped_column("timeZone")
    start_date: Mapped[datetime] = mapped_column("startDate")
    end_date: Mapped[Optional[datetime]] = mapped_column("endDate")
    prev_time_zone: Mapped[Optional[str]] = mapped_column("prevTimeZone")

    __table_args__ = (
        Index("TravelSchedule_startDate_idx", "startDate"),
        Index("TravelSchedule_endDate_idx", "endDate"),
    )

    user: Mapped["User"] = relationship(back_populates="travel_schedules")

class User(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[uuid_mod.UUID] = mapped_column(Uuid, default=uuid_mod.uuid4)
    username: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    email: Mapped[str]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    bio: Mapped[Optional[str]]
    avatar_url: Mapped[Optional[str]] = mapped_column("avatarUrl")
    time_zone: Mapped[str] = mapped_column("timeZone", default="Europe/London", server_default=text("'Europe/London'"))
    week_start: Mapped[str] = mapped_column("weekStart", default="Sunday", server_default=text("'Sunday'"))
    buffer_time: Mapped[int] = mapped_column("bufferTime", default=0, server_default=text("0"))
    hide_branding: Mapped[bool] = mapped_column("hideBranding", default=False, server_default=text("false"))
    theme: Mapped[Optional[str]]
    app_theme: Mapped[Optional[str]] = mapped_column("appTheme")
    created_date: Mapped[datetime] = mapped_column("created", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    trial_ends_at: Mapped[Optional[datetime]] = mapped_column("trialEndsAt")
    last_active_at: Mapped[Optional[datetime]] = mapped_column("lastActiveAt")
    default_schedule_id: Mapped[Optional[int]] = mapped_column("defaultScheduleId")
    completed_onboarding: Mapped[bool] = mapped_column("completedOnboarding", default=False, server_default=text("false"))
    locale: Mapped[Optional[str]]
    time_format: Mapped[Optional[int]] = mapped_column("timeFormat", default=12, server_default=text("12"))
    two_factor_secret: Mapped[Optional[str]] = mapped_column("twoFactorSecret")
    two_factor_enabled: Mapped[bool] = mapped_column("twoFactorEnabled", default=False, server_default=text("false"))
    backup_codes: Mapped[Optional[str]] = mapped_column("backupCodes")
    identity_provider: Mapped[str] = mapped_column("identityProvider", Enum("CAL", "GOOGLE", "SAML", "AZUREAD", name="IdentityProvider"), default="CAL", server_default=text("'CAL'"))
    identity_provider_id: Mapped[Optional[str]] = mapped_column("identityProviderId")
    invited_to: Mapped[Optional[int]] = mapped_column("invitedTo")
    brand_color: Mapped[Optional[str]] = mapped_column("brandColor")
    dark_brand_color: Mapped[Optional[str]] = mapped_column("darkBrandColor")
    allow_dynamic_booking: Mapped[Optional[bool]] = mapped_column("allowDynamicBooking", default=True, server_default=text("true"))
    allow_seoindexing: Mapped[Optional[bool]] = mapped_column("allowSEOIndexing", default=True, server_default=text("true"))
    receive_monthly_digest_email: Mapped[Optional[bool]] = mapped_column("receiveMonthlyDigestEmail", default=True, server_default=text("true"))
    requires_booker_email_verification: Mapped[Optional[bool]] = mapped_column("requiresBookerEmailVerification", default=False, server_default=text("false"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    verified: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    role: Mapped[str] = mapped_column(Enum("USER", "ADMIN", name="UserPermissionRole"), default="USER", server_default=text("'USER'"))
    organization_id: Mapped[Optional[int]] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="SET NULL", onupdate="CASCADE", name="users_organizationId_fkey"))
    locked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    moved_to_profile_id: Mapped[Optional[int]] = mapped_column("movedToProfileId", ForeignKey("Profile.id", ondelete="SET NULL", onupdate="CASCADE", name="users_movedToProfileId_fkey"))
    is_platform_managed: Mapped[bool] = mapped_column("isPlatformManaged", default=False, server_default=text("false"))
    sms_lock_state: Mapped[str] = mapped_column("smsLockState", Enum("LOCKED", "UNLOCKED", "REVIEW_NEEDED", name="SMSLockState"), default="UNLOCKED", server_default=text("'UNLOCKED'"))
    sms_lock_reviewed_by_admin: Mapped[bool] = mapped_column("smsLockReviewedByAdmin", default=False, server_default=text("false"))
    referral_link_id: Mapped[Optional[str]] = mapped_column("referralLinkId")
    creation_source: Mapped[Optional[str]] = mapped_column("creationSource", Enum("api_v1", "api_v2", "webapp", name="CreationSource"))
    auto_opt_in_features: Mapped[bool] = mapped_column("autoOptInFeatures", default=False, server_default=text("false"))

    __table_args__ = (
        Index("users_uuid_key", "uuid", unique=True),
        Index("users_username_idx", "username"),
        Index("users_emailVerified_idx", "emailVerified"),
        Index("users_identityProvider_idx", "identityProvider"),
        Index("users_identityProviderId_idx", "identityProviderId"),
        Index("users_email_key", "email", unique=True),
        Index("users_email_username_key", "email", "username", unique=True),
        Index("users_username_organizationId_key", "username", "organizationId", unique=True),
        Index("users_movedToProfileId_key", "movedToProfileId", unique=True),
    )

    organization: Mapped[Optional["Team"]] = relationship(back_populates="org_users")
    moved_to_profile: Mapped[Optional["Profile"]] = relationship(back_populates="moved_from_user")
    travel_schedules: Mapped[list["TravelSchedule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    credentials: Mapped[list["Credential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    teams: Mapped[list["Membership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    bookings: Mapped[list["Booking"]] = relationship(foreign_keys="Booking.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    schedules: Mapped[list["Schedule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    selected_calendars: Mapped[list["SelectedCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    availability: Mapped[list["Availability"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    feedback: Mapped[list["Feedback"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    owned_event_types: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    verified_numbers: Mapped[list["VerifiedNumber"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    verified_emails: Mapped[list["VerifiedEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    hosts: Mapped[list["Host"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    access_codes: Mapped[list["AccessCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    booking_redirects: Mapped[list["OutOfOfficeEntry"]] = relationship(foreign_keys="OutOfOfficeEntry.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    booking_redirects_to: Mapped[list["OutOfOfficeEntry"]] = relationship(foreign_keys="OutOfOfficeEntry.to_user_id", cascade="all, delete", passive_deletes=True, back_populates="to_user")
    access_token: Mapped[list["AccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    refresh_token: Mapped[list["RefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    platform_authorization_token: Mapped[list["PlatformAuthorizationToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    profiles: Mapped[list["Profile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    secondary_emails: Mapped[list["SecondaryEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    out_of_office_reasons: Mapped[list["OutOfOfficeReason"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    notifications_subscriptions: Mapped[list["NotificationsSubscriptions"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    features: Mapped[list["UserFeatures"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    reassigned_bookings: Mapped[list["Booking"]] = relationship(foreign_keys="Booking.reassign_by_id", back_populates="reassign_by")
    created_attribute_to_users: Mapped[list["AttributeToUser"]] = relationship(foreign_keys="AttributeToUser.created_by_id", back_populates="created_by")
    updated_attribute_to_users: Mapped[list["AttributeToUser"]] = relationship(foreign_keys="AttributeToUser.updated_by_id", back_populates="updated_by")
    created_translations: Mapped[list["EventTypeTranslation"]] = relationship(foreign_keys="EventTypeTranslation.created_by", passive_deletes="all", back_populates="creator")
    updated_translations: Mapped[list["EventTypeTranslation"]] = relationship(foreign_keys="EventTypeTranslation.updated_by", back_populates="updater")
    reported_bookings: Mapped[list["BookingReport"]] = relationship(back_populates="reported_by")
    wrong_assignment_reports: Mapped[list["WrongAssignmentReport"]] = relationship(foreign_keys="WrongAssignmentReport.reported_by_id", back_populates="reported_by")
    reviewed_wrong_assignment_reports: Mapped[list["WrongAssignmentReport"]] = relationship(foreign_keys="WrongAssignmentReport.reviewed_by_id", back_populates="reviewed_by")
    booking_internal_note: Mapped[list["BookingInternalNote"]] = relationship(passive_deletes="all", back_populates="created_by")
    created_organization_onboardings: Mapped[list["OrganizationOnboarding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="created_by")
    filter_segments: Mapped[list["FilterSegment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    filter_segment_preferences: Mapped[list["UserFilterSegmentPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    cal_ai_phone_numbers: Mapped[list["CalAiPhoneNumber"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    agents: Mapped[list["Agent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    o_auth_clients: Mapped[list["OAuthClient"]] = relationship(back_populates="user")
    password: Mapped[Optional["UserPassword"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    destination_calendar: Mapped[Optional["DestinationCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    credit_balance: Mapped[Optional["CreditBalance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    holiday_settings: Mapped[Optional["UserHolidaySettings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    event_types: Mapped[list["EventType"]] = relationship(secondary=user_eventtype, back_populates="users")
    platform_oauth_clients: Mapped[list["PlatformOAuthClient"]] = relationship(secondary=platform_oauth_client_to_user, back_populates="users")

class NotificationsSubscriptions(Base):
    __tablename__ = "NotificationsSubscriptions"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="NotificationsSubscriptions_userId_fkey"))
    subscription: Mapped[str]

    __table_args__ = (
        Index("NotificationsSubscriptions_userId_subscription_idx", "userId", "subscription"),
    )

    user: Mapped["User"] = relationship(back_populates="notifications_subscriptions")

class Profile(Base):
    __tablename__ = "Profile"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uid: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Profile_userId_fkey"))
    organization_id: Mapped[int] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Profile_organizationId_fkey"))
    username: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Profile_uid_idx", "uid"),
        Index("Profile_userId_idx", "userId"),
        Index("Profile_organizationId_idx", "organizationId"),
        Index("Profile_userId_organizationId_key", "userId", "organizationId", unique=True),
        Index("Profile_username_organizationId_key", "username", "organizationId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="profiles")
    organization: Mapped["Team"] = relationship(back_populates="org_profiles")
    event_types: Mapped[list["EventType"]] = relationship(back_populates="profile")
    moved_from_user: Mapped[Optional["User"]] = relationship(back_populates="moved_to_profile")

class Team(Base):
    __tablename__ = "Team"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    slug: Mapped[Optional[str]]
    logo_url: Mapped[Optional[str]] = mapped_column("logoUrl")
    cal_video_logo: Mapped[Optional[str]] = mapped_column("calVideoLogo")
    app_logo: Mapped[Optional[str]] = mapped_column("appLogo")
    app_icon_logo: Mapped[Optional[str]] = mapped_column("appIconLogo")
    bio: Mapped[Optional[str]]
    hide_branding: Mapped[bool] = mapped_column("hideBranding", default=False, server_default=text("false"))
    hide_team_profile_link: Mapped[bool] = mapped_column("hideTeamProfileLink", default=False, server_default=text("false"))
    is_private: Mapped[bool] = mapped_column("isPrivate", default=False, server_default=text("false"))
    hide_book_ateam_member: Mapped[bool] = mapped_column("hideBookATeamMember", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    theme: Mapped[Optional[str]]
    rr_reset_interval: Mapped[Optional[str]] = mapped_column("rrResetInterval", Enum("MONTH", "DAY", name="RRResetInterval"), default="MONTH", server_default=text("'MONTH'"))
    rr_timestamp_basis: Mapped[str] = mapped_column("rrTimestampBasis", Enum("CREATED_AT", "START_TIME", name="RRTimestampBasis"), default="CREATED_AT", server_default=text("'CREATED_AT'"))
    brand_color: Mapped[Optional[str]] = mapped_column("brandColor")
    dark_brand_color: Mapped[Optional[str]] = mapped_column("darkBrandColor")
    banner_url: Mapped[Optional[str]] = mapped_column("bannerUrl")
    parent_id: Mapped[Optional[int]] = mapped_column("parentId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Team_parentId_fkey"))
    time_format: Mapped[Optional[int]] = mapped_column("timeFormat")
    time_zone: Mapped[str] = mapped_column("timeZone", default="Europe/London", server_default=text("'Europe/London'"))
    week_start: Mapped[str] = mapped_column("weekStart", default="Sunday", server_default=text("'Sunday'"))
    is_organization: Mapped[bool] = mapped_column("isOrganization", default=False, server_default=text("false"))
    pending_payment: Mapped[bool] = mapped_column("pendingPayment", default=False, server_default=text("false"))
    is_platform: Mapped[bool] = mapped_column("isPlatform", default=False, server_default=text("false"))
    created_by_oauth_client_id: Mapped[Optional[str]] = mapped_column("createdByOAuthClientId", ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="Team_createdByOAuthClientId_fkey"))
    sms_lock_state: Mapped[str] = mapped_column("smsLockState", Enum("LOCKED", "UNLOCKED", "REVIEW_NEEDED", name="SMSLockState"), default="UNLOCKED", server_default=text("'UNLOCKED'"))
    sms_lock_reviewed_by_admin: Mapped[bool] = mapped_column("smsLockReviewedByAdmin", default=False, server_default=text("false"))
    booking_limits: Mapped[Optional[dict[str, Any]]] = mapped_column("bookingLimits", JSONB(none_as_null=True))
    include_managed_events_in_limits: Mapped[bool] = mapped_column("includeManagedEventsInLimits", default=False, server_default=text("false"))
    auto_opt_in_features: Mapped[bool] = mapped_column("autoOptInFeatures", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Team_parentId_idx", "parentId"),
        Index("Team_slug_parentId_key", "slug", "parentId", unique=True),
    )

    parent: Mapped[Optional["Team"]] = relationship(remote_side=[id], back_populates="children")
    created_by_oauth_client: Mapped[Optional["PlatformOAuthClient"]] = relationship(back_populates="teams")
    members: Mapped[list["Membership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    event_types: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    verified_numbers: Mapped[list["VerifiedNumber"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    verified_emails: Mapped[list["VerifiedEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    children: Mapped[list["Team"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")
    org_users: Mapped[list["User"]] = relationship(back_populates="organization")
    invite_tokens: Mapped[list["VerificationToken"]] = relationship(back_populates="team")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    credentials: Mapped[list["Credential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    access_codes: Mapped[list["AccessCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    instant_meeting_tokens: Mapped[list["InstantMeetingToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    org_profiles: Mapped[list["Profile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    dsync_team_group_mapping: Mapped[list["DSyncTeamGroupMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    platform_oauth_client: Mapped[list["PlatformOAuthClient"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    attributes: Mapped[list["Attribute"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    delegation_credentials: Mapped[list["DelegationCredential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    roles: Mapped[list["Role"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    cal_ai_phone_numbers: Mapped[list["CalAiPhoneNumber"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    agents: Mapped[list["Agent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    booking_reports: Mapped[list["BookingReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    wrong_assignment_reports: Mapped[list["WrongAssignmentReport"]] = relationship(back_populates="team")
    features: Mapped[list["TeamFeatures"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    internal_note_presets: Mapped[list["InternalNotePreset"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    managed_organizations: Mapped[list["ManagedOrganization"]] = relationship(foreign_keys="ManagedOrganization.manager_organization_id", cascade="all, delete", passive_deletes=True, back_populates="manager_organization")
    filter_segments: Mapped[list["FilterSegment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    integration_attribute_syncs: Mapped[list["IntegrationAttributeSync"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    seat_change_logs: Mapped[list["SeatChangeLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    monthly_prorations: Mapped[list["MonthlyProration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    organization_settings: Mapped[Optional["OrganizationSettings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    platform_billing: Mapped[Optional["PlatformBilling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    credit_balance: Mapped[Optional["CreditBalance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    organization_onboarding: Mapped[Optional["OrganizationOnboarding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    managed_organization: Mapped[Optional["ManagedOrganization"]] = relationship(foreign_keys="ManagedOrganization.managed_organization_id", cascade="all, delete", passive_deletes=True, back_populates="managed_organization")
    team_billing: Mapped[Optional["TeamBilling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    organization_billing: Mapped[Optional["OrganizationBilling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class CreditBalance(Base):
    __tablename__ = "CreditBalance"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="CreditBalance_teamId_fkey"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="CreditBalance_userId_fkey"))
    additional_credits: Mapped[int] = mapped_column("additionalCredits", default=0, server_default=text("0"))
    limit_reached_at: Mapped[Optional[datetime]] = mapped_column("limitReachedAt")
    warning_sent_at: Mapped[Optional[datetime]] = mapped_column("warningSentAt")

    __table_args__ = (
        Index("CreditBalance_teamId_key", "teamId", unique=True),
        Index("CreditBalance_userId_key", "userId", unique=True),
    )

    team: Mapped[Optional["Team"]] = relationship(back_populates="credit_balance")
    user: Mapped[Optional["User"]] = relationship(back_populates="credit_balance")
    expense_logs: Mapped[list["CreditExpenseLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credit_balance")
    purchase_logs: Mapped[list["CreditPurchaseLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="credit_balance")

class CreditPurchaseLog(Base):
    __tablename__ = "CreditPurchaseLog"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    credit_balance_id: Mapped[str] = mapped_column("creditBalanceId", ForeignKey("CreditBalance.id", ondelete="CASCADE", onupdate="CASCADE", name="CreditPurchaseLog_creditBalanceId_fkey"))
    credits: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    credit_balance: Mapped["CreditBalance"] = relationship(back_populates="purchase_logs")

class CreditExpenseLog(Base):
    __tablename__ = "CreditExpenseLog"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    credit_balance_id: Mapped[str] = mapped_column("creditBalanceId", ForeignKey("CreditBalance.id", ondelete="CASCADE", onupdate="CASCADE", name="CreditExpenseLog_creditBalanceId_fkey"))
    booking_uid: Mapped[Optional[str]] = mapped_column("bookingUid", ForeignKey("Booking.uid", ondelete="CASCADE", onupdate="CASCADE", name="CreditExpenseLog_bookingUid_fkey"))
    credits: Mapped[Optional[int]]
    credit_type: Mapped[str] = mapped_column("creditType", Enum("MONTHLY", "ADDITIONAL", name="CreditType"))
    date: Mapped[datetime]
    sms_sid: Mapped[Optional[str]] = mapped_column("smsSid")
    sms_segments: Mapped[Optional[int]] = mapped_column("smsSegments")
    phone_number: Mapped[Optional[str]] = mapped_column("phoneNumber")
    email: Mapped[Optional[str]]
    call_duration: Mapped[Optional[int]] = mapped_column("callDuration")
    credit_for: Mapped[Optional[str]] = mapped_column("creditFor", Enum("SMS", "CAL_AI_PHONE_CALL", name="CreditUsageType"))
    external_ref: Mapped[Optional[str]] = mapped_column("externalRef")

    __table_args__ = (
        Index("CreditExpenseLog_externalRef_key", "externalRef", unique=True),
    )

    credit_balance: Mapped["CreditBalance"] = relationship(back_populates="expense_logs")
    booking: Mapped[Optional["Booking"]] = relationship(back_populates="expense_logs")

class OrganizationSettings(Base):
    __tablename__ = "OrganizationSettings"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    organization_id: Mapped[int] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationSettings_organizationId_fkey"))
    is_organization_configured: Mapped[bool] = mapped_column("isOrganizationConfigured", default=False, server_default=text("false"))
    is_organization_verified: Mapped[bool] = mapped_column("isOrganizationVerified", default=False, server_default=text("false"))
    org_auto_accept_email: Mapped[str] = mapped_column("orgAutoAcceptEmail")
    lock_event_type_creation_for_users: Mapped[bool] = mapped_column("lockEventTypeCreationForUsers", default=False, server_default=text("false"))
    admin_gets_no_slots_notification: Mapped[bool] = mapped_column("adminGetsNoSlotsNotification", default=False, server_default=text("false"))
    is_admin_reviewed: Mapped[bool] = mapped_column("isAdminReviewed", default=False, server_default=text("false"))
    is_admin_apienabled: Mapped[bool] = mapped_column("isAdminAPIEnabled", default=False, server_default=text("false"))
    allow_seoindexing: Mapped[bool] = mapped_column("allowSEOIndexing", default=False, server_default=text("false"))
    org_profile_redirects_to_verified_domain: Mapped[bool] = mapped_column("orgProfileRedirectsToVerifiedDomain", default=False, server_default=text("false"))
    disable_phone_only_smsnotifications: Mapped[bool] = mapped_column("disablePhoneOnlySMSNotifications", default=False, server_default=text("false"))
    disable_autofill_on_booking_page: Mapped[bool] = mapped_column("disableAutofillOnBookingPage", default=False, server_default=text("false"))
    org_auto_join_on_signup: Mapped[bool] = mapped_column("orgAutoJoinOnSignup", default=True, server_default=text("true"))
    disable_attendee_confirmation_email: Mapped[bool] = mapped_column("disableAttendeeConfirmationEmail", default=False, server_default=text("false"))
    disable_attendee_cancellation_email: Mapped[bool] = mapped_column("disableAttendeeCancellationEmail", default=False, server_default=text("false"))
    disable_attendee_rescheduled_email: Mapped[bool] = mapped_column("disableAttendeeRescheduledEmail", default=False, server_default=text("false"))
    disable_attendee_request_email: Mapped[bool] = mapped_column("disableAttendeeRequestEmail", default=False, server_default=text("false"))
    disable_attendee_reassigned_email: Mapped[bool] = mapped_column("disableAttendeeReassignedEmail", default=False, server_default=text("false"))
    disable_attendee_awaiting_payment_email: Mapped[bool] = mapped_column("disableAttendeeAwaitingPaymentEmail", default=False, server_default=text("false"))
    disable_attendee_reschedule_request_email: Mapped[bool] = mapped_column("disableAttendeeRescheduleRequestEmail", default=False, server_default=text("false"))
    disable_attendee_location_change_email: Mapped[bool] = mapped_column("disableAttendeeLocationChangeEmail", default=False, server_default=text("false"))
    disable_attendee_new_event_email: Mapped[bool] = mapped_column("disableAttendeeNewEventEmail", default=False, server_default=text("false"))

    __table_args__ = (
        Index("OrganizationSettings_organizationId_key", "organizationId", unique=True),
    )

    organization: Mapped["Team"] = relationship(back_populates="organization_settings")
    d_sync_data: Mapped[Optional["DSyncData"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="org")

class Membership(Base):
    __tablename__ = "Membership"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Membership_teamId_fkey"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Membership_userId_fkey"))
    accepted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    role: Mapped[str] = mapped_column(Enum("MEMBER", "ADMIN", "OWNER", name="MembershipRole"))
    custom_role_id: Mapped[Optional[str]] = mapped_column("customRoleId", ForeignKey("Role.id", ondelete="SET NULL", onupdate="CASCADE", name="Membership_customRoleId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Membership_teamId_idx", "teamId"),
        Index("Membership_userId_idx", "userId"),
        Index("Membership_accepted_idx", "accepted"),
        Index("Membership_role_idx", "role"),
        Index("Membership_customRoleId_idx", "customRoleId"),
        Index("Membership_userId_teamId_key", "userId", "teamId", unique=True),
    )

    custom_role: Mapped[Optional["Role"]] = relationship(back_populates="memberships")
    team: Mapped["Team"] = relationship(back_populates="members")
    user: Mapped["User"] = relationship(back_populates="teams")
    attribute_to_user: Mapped[list["AttributeToUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="member")
    host: Mapped[list["Host"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="member")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    identifier: Mapped[str]
    token: Mapped[str]
    expires: Mapped[datetime]
    expires_in_days: Mapped[Optional[int]] = mapped_column("expiresInDays")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="SET NULL", onupdate="CASCADE", name="VerificationToken_teamId_fkey"))
    secondary_email_id: Mapped[Optional[int]] = mapped_column("secondaryEmailId", ForeignKey("SecondaryEmail.id", ondelete="SET NULL", onupdate="CASCADE", name="VerificationToken_secondaryEmailId_fkey"))

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
        Index("VerificationToken_token_idx", "token"),
        Index("VerificationToken_teamId_idx", "teamId"),
        Index("VerificationToken_secondaryEmailId_idx", "secondaryEmailId"),
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
    )

    team: Mapped[Optional["Team"]] = relationship(back_populates="invite_tokens")
    secondary_email: Mapped[Optional["SecondaryEmail"]] = relationship(back_populates="verification_tokens")

class InstantMeetingToken(Base):
    __tablename__ = "InstantMeetingToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    token: Mapped[str]
    expires: Mapped[datetime]
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="InstantMeetingToken_teamId_fkey"))
    booking_id: Mapped[Optional[int]] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="InstantMeetingToken_bookingId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("InstantMeetingToken_token_key", "token", unique=True),
        Index("InstantMeetingToken_bookingId_key", "bookingId", unique=True),
        Index("InstantMeetingToken_token_idx", "token"),
    )

    team: Mapped["Team"] = relationship(back_populates="instant_meeting_tokens")
    booking: Mapped[Optional["Booking"]] = relationship(back_populates="instant_meeting_token")

class BookingReference(Base):
    __tablename__ = "BookingReference"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    type: Mapped[str]
    uid: Mapped[str]
    meeting_id: Mapped[Optional[str]] = mapped_column("meetingId")
    third_party_recurring_event_id: Mapped[Optional[str]] = mapped_column("thirdPartyRecurringEventId")
    meeting_password: Mapped[Optional[str]] = mapped_column("meetingPassword")
    meeting_url: Mapped[Optional[str]] = mapped_column("meetingUrl")
    booking_id: Mapped[Optional[int]] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingReference_bookingId_fkey"))
    external_calendar_id: Mapped[Optional[str]] = mapped_column("externalCalendarId")
    deleted: Mapped[Optional[bool]]
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingReference_credentialId_fkey"))
    delegation_credential_id: Mapped[Optional[str]] = mapped_column("delegationCredentialId", ForeignKey("DelegationCredential.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingReference_delegationCredentialId_fkey"))

    __table_args__ = (
        Index("BookingReference_bookingId_idx", "bookingId"),
        Index("BookingReference_type_idx", "type"),
        Index("BookingReference_uid_idx", "uid"),
    )

    booking: Mapped[Optional["Booking"]] = relationship(back_populates="references")
    credential: Mapped[Optional["Credential"]] = relationship(back_populates="references")
    delegation_credential: Mapped[Optional["DelegationCredential"]] = relationship(back_populates="booking_references")

class Attendee(Base):
    __tablename__ = "Attendee"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    email: Mapped[str]
    name: Mapped[str]
    time_zone: Mapped[str] = mapped_column("timeZone")
    phone_number: Mapped[Optional[str]] = mapped_column("phoneNumber")
    locale: Mapped[Optional[str]] = mapped_column(default="en", server_default=text("'en'"))
    booking_id: Mapped[Optional[int]] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="Attendee_bookingId_fkey"))
    no_show: Mapped[Optional[bool]] = mapped_column("noShow", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Attendee_email_idx", "email"),
        Index("Attendee_bookingId_idx", "bookingId"),
        Index("Attendee_email_bookingId_idx", "email", "bookingId"),
    )

    booking: Mapped[Optional["Booking"]] = relationship(back_populates="attendees")
    booking_seat: Mapped[Optional["BookingSeat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attendee")

class Booking(Base):
    __tablename__ = "Booking"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uid: Mapped[str]
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Booking_userId_fkey"))
    user_primary_email: Mapped[Optional[str]] = mapped_column("userPrimaryEmail")
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="SET NULL", onupdate="CASCADE", name="Booking_eventTypeId_fkey"))
    title: Mapped[str]
    description: Mapped[Optional[str]]
    custom_inputs: Mapped[Optional[dict[str, Any]]] = mapped_column("customInputs", JSONB(none_as_null=True))
    responses: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    location: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(Enum("cancelled", "accepted", "rejected", "pending", "awaiting_host", name="BookingStatus"), default="accepted", server_default=text("'accepted'"))
    paid: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    destination_calendar_id: Mapped[Optional[int]] = mapped_column("destinationCalendarId", ForeignKey("DestinationCalendar.id", ondelete="SET NULL", onupdate="CASCADE", name="Booking_destinationCalendarId_fkey"))
    cancellation_reason: Mapped[Optional[str]] = mapped_column("cancellationReason")
    rejection_reason: Mapped[Optional[str]] = mapped_column("rejectionReason")
    reassign_reason: Mapped[Optional[str]] = mapped_column("reassignReason")
    reassign_by_id: Mapped[Optional[int]] = mapped_column("reassignById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="Booking_reassignById_fkey"))
    dynamic_event_slug_ref: Mapped[Optional[str]] = mapped_column("dynamicEventSlugRef")
    dynamic_group_slug_ref: Mapped[Optional[str]] = mapped_column("dynamicGroupSlugRef")
    rescheduled: Mapped[Optional[bool]]
    from_reschedule: Mapped[Optional[str]] = mapped_column("fromReschedule")
    recurring_event_id: Mapped[Optional[str]] = mapped_column("recurringEventId")
    sms_reminder_number: Mapped[Optional[str]] = mapped_column("smsReminderNumber")
    scheduled_jobs: Mapped[list[str]] = mapped_column("scheduledJobs", ARRAY(Text), nullable=True)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    is_recorded: Mapped[bool] = mapped_column("isRecorded", default=False, server_default=text("false"))
    i_cal_uid: Mapped[Optional[str]] = mapped_column("iCalUID", default="", server_default=text("''"))
    i_cal_sequence: Mapped[int] = mapped_column("iCalSequence", default=0, server_default=text("0"))
    rating: Mapped[Optional[int]]
    rating_feedback: Mapped[Optional[str]] = mapped_column("ratingFeedback")
    no_show_host: Mapped[Optional[bool]] = mapped_column("noShowHost", default=False, server_default=text("false"))
    one_time_password: Mapped[Optional[str]] = mapped_column("oneTimePassword", default=lambda: str(uuid_mod.uuid4()))
    cancelled_by: Mapped[Optional[str]] = mapped_column("cancelledBy")
    rescheduled_by: Mapped[Optional[str]] = mapped_column("rescheduledBy")
    creation_source: Mapped[Optional[str]] = mapped_column("creationSource", Enum("api_v1", "api_v2", "webapp", name="CreationSource"))

    __table_args__ = (
        Index("Booking_uid_key", "uid", unique=True),
        Index("Booking_idempotencyKey_key", "idempotencyKey", unique=True),
        Index("Booking_oneTimePassword_key", "oneTimePassword", unique=True),
        Index("Booking_eventTypeId_idx", "eventTypeId"),
        Index("Booking_userId_idx", "userId"),
        Index("Booking_destinationCalendarId_idx", "destinationCalendarId"),
        Index("Booking_recurringEventId_idx", "recurringEventId"),
        Index("Booking_uid_idx", "uid"),
        Index("Booking_status_idx", "status"),
        Index("Booking_startTime_endTime_status_idx", "startTime", "endTime", "status"),
        Index("Booking_fromReschedule_idx", "fromReschedule"),
        Index("Booking_userId_endTime_idx", "userId", "endTime"),
        Index("Booking_userId_status_startTime_idx", "userId", "status", "startTime"),
        Index("Booking_eventTypeId_status_idx", "eventTypeId", "status"),
        Index("Booking_userId_createdAt_idx", "userId", "createdAt"),
    )

    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="bookings")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="bookings")
    destination_calendar: Mapped[Optional["DestinationCalendar"]] = relationship(back_populates="booking")
    reassign_by: Mapped[Optional["User"]] = relationship(foreign_keys=[reassign_by_id], back_populates="reassigned_bookings")
    references: Mapped[list["BookingReference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    attendees: Mapped[list["Attendee"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    payment: Mapped[list["Payment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    seats_references: Mapped[list["BookingSeat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    scheduled_triggers: Mapped[list["WebhookScheduledTriggers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    assignment_reason: Mapped[list["AssignmentReason"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    internal_note: Mapped[list["BookingInternalNote"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    expense_logs: Mapped[list["CreditExpenseLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    wrong_assignment_reports: Mapped[list["WrongAssignmentReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    instant_meeting_token: Mapped[Optional["InstantMeetingToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    tracking: Mapped[Optional["Tracking"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")
    report: Mapped[Optional["BookingReport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking")

class Tracking(Base):
    __tablename__ = "Tracking"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    booking_id: Mapped[int] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="Tracking_bookingId_fkey"))
    utm_source: Mapped[Optional[str]]
    utm_medium: Mapped[Optional[str]]
    utm_campaign: Mapped[Optional[str]]
    utm_term: Mapped[Optional[str]]
    utm_content: Mapped[Optional[str]]

    __table_args__ = (
        Index("Tracking_bookingId_key", "bookingId", unique=True),
    )

    booking: Mapped["Booking"] = relationship(back_populates="tracking")

class Schedule(Base):
    __tablename__ = "Schedule"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Schedule_userId_fkey"))
    name: Mapped[str]
    time_zone: Mapped[Optional[str]] = mapped_column("timeZone")

    __table_args__ = (
        Index("Schedule_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="schedules")
    event_type: Mapped[list["EventType"]] = relationship(foreign_keys="EventType.schedule_id", back_populates="schedule")
    instant_meeting_events: Mapped[list["EventType"]] = relationship(foreign_keys="EventType.instant_meeting_schedule_id", back_populates="instant_meeting_schedule")
    restriction_schedule: Mapped[list["EventType"]] = relationship(foreign_keys="EventType.restriction_schedule_id", back_populates="restriction_schedule")
    availability: Mapped[list["Availability"]] = relationship(back_populates="schedule")
    host: Mapped[list["Host"]] = relationship(back_populates="schedule")

class Availability(Base):
    __tablename__ = "Availability"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Availability_userId_fkey"))
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="SET NULL", onupdate="CASCADE", name="Availability_eventTypeId_fkey"))
    days: Mapped[list[int]] = mapped_column(ARRAY(Integer), nullable=True)
    start_time: Mapped[time_type] = mapped_column("startTime", Time)
    end_time: Mapped[time_type] = mapped_column("endTime", Time)
    date: Mapped[Optional[date]] = mapped_column(Date)
    schedule_id: Mapped[Optional[int]] = mapped_column("scheduleId", ForeignKey("Schedule.id", ondelete="SET NULL", onupdate="CASCADE", name="Availability_scheduleId_fkey"))

    __table_args__ = (
        Index("Availability_userId_idx", "userId"),
        Index("Availability_eventTypeId_idx", "eventTypeId"),
        Index("Availability_scheduleId_idx", "scheduleId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="availability")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="availability")
    schedule: Mapped[Optional["Schedule"]] = relationship(back_populates="availability")

class SelectedCalendar(Base):
    __tablename__ = "SelectedCalendar"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="SelectedCalendar_userId_fkey"))
    integration: Mapped[str]
    external_id: Mapped[str] = mapped_column("externalId")
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="CASCADE", onupdate="CASCADE", name="SelectedCalendar_credentialId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    google_channel_id: Mapped[Optional[str]] = mapped_column("googleChannelId")
    google_channel_kind: Mapped[Optional[str]] = mapped_column("googleChannelKind")
    google_channel_resource_id: Mapped[Optional[str]] = mapped_column("googleChannelResourceId")
    google_channel_resource_uri: Mapped[Optional[str]] = mapped_column("googleChannelResourceUri")
    google_channel_expiration: Mapped[Optional[str]] = mapped_column("googleChannelExpiration")
    channel_id: Mapped[Optional[str]] = mapped_column("channelId")
    channel_kind: Mapped[Optional[str]] = mapped_column("channelKind")
    channel_resource_id: Mapped[Optional[str]] = mapped_column("channelResourceId")
    channel_resource_uri: Mapped[Optional[str]] = mapped_column("channelResourceUri")
    channel_expiration: Mapped[Optional[datetime]] = mapped_column("channelExpiration")
    sync_subscribed_at: Mapped[Optional[datetime]] = mapped_column("syncSubscribedAt")
    sync_subscribed_error_at: Mapped[Optional[datetime]] = mapped_column("syncSubscribedErrorAt")
    sync_subscribed_error_count: Mapped[int] = mapped_column("syncSubscribedErrorCount", default=0, server_default=text("0"))
    sync_token: Mapped[Optional[str]] = mapped_column("syncToken")
    synced_at: Mapped[Optional[datetime]] = mapped_column("syncedAt")
    sync_error_at: Mapped[Optional[datetime]] = mapped_column("syncErrorAt")
    sync_error_count: Mapped[Optional[int]] = mapped_column("syncErrorCount", default=0, server_default=text("0"))
    delegation_credential_id: Mapped[Optional[str]] = mapped_column("delegationCredentialId", ForeignKey("DelegationCredential.id", ondelete="CASCADE", onupdate="CASCADE", name="SelectedCalendar_delegationCredentialId_fkey"))
    error: Mapped[Optional[str]]
    last_error_at: Mapped[Optional[datetime]] = mapped_column("lastErrorAt")
    watch_attempts: Mapped[int] = mapped_column("watchAttempts", default=0, server_default=text("0"))
    unwatch_attempts: Mapped[int] = mapped_column("unwatchAttempts", default=0, server_default=text("0"))
    max_attempts: Mapped[int] = mapped_column("maxAttempts", default=3, server_default=text("3"))
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="SET NULL", onupdate="CASCADE", name="SelectedCalendar_eventTypeId_fkey"))

    __table_args__ = (
        Index("SelectedCalendar_userId_idx", "userId"),
        Index("SelectedCalendar_externalId_idx", "externalId"),
        Index("SelectedCalendar_eventTypeId_idx", "eventTypeId"),
        Index("SelectedCalendar_credentialId_idx", "credentialId"),
        Index("SelectedCalendar_channelId_idx", "channelId"),
        Index("SelectedCalendar_watch_idx", "integration", "googleChannelExpiration", "error", "watchAttempts", "maxAttempts"),
        Index("SelectedCalendar_unwatch_idx", "integration", "googleChannelExpiration", "error", "unwatchAttempts", "maxAttempts"),
        Index("SelectedCalendar_userId_integration_externalId_eventTypeId_key", "userId", "integration", "externalId", "eventTypeId", unique=True),
        Index("SelectedCalendar_googleChannelId_eventTypeId_key", "googleChannelId", "eventTypeId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="selected_calendars")
    credential: Mapped[Optional["Credential"]] = relationship(back_populates="selected_calendars")
    delegation_credential: Mapped[Optional["DelegationCredential"]] = relationship(back_populates="selected_calendars")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="selected_calendars")
    calendar_cache_events: Mapped[list["CalendarCacheEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="selected_calendar")

class EventTypeCustomInput(Base):
    __tablename__ = "EventTypeCustomInput"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    event_type_id: Mapped[int] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="EventTypeCustomInput_eventTypeId_fkey"))
    label: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("text", "textLong", "number", "bool", "radio", "phone", name="EventTypeCustomInputType"))
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    required: Mapped[bool]
    placeholder: Mapped[str] = mapped_column(default="", server_default=text("''"))

    __table_args__ = (
        Index("EventTypeCustomInput_eventTypeId_idx", "eventTypeId"),
    )

    event_type: Mapped["EventType"] = relationship(back_populates="custom_inputs")

class ResetPasswordRequest(Base):
    __tablename__ = "ResetPasswordRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email: Mapped[str]
    expires: Mapped[datetime]

class ReminderMail(Base):
    __tablename__ = "ReminderMail"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    reference_id: Mapped[int] = mapped_column("referenceId")
    reminder_type: Mapped[str] = mapped_column("reminderType", Enum("PENDING_BOOKING_CONFIRMATION", name="ReminderType"))
    elapsed_minutes: Mapped[int] = mapped_column("elapsedMinutes")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ReminderMail_referenceId_idx", "referenceId"),
        Index("ReminderMail_reminderType_idx", "reminderType"),
    )

class Payment(Base):
    __tablename__ = "Payment"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uid: Mapped[str]
    app_id: Mapped[Optional[str]] = mapped_column("appId", ForeignKey("App.slug", ondelete="CASCADE", onupdate="CASCADE", name="Payment_appId_fkey"))
    booking_id: Mapped[int] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="Payment_bookingId_fkey"))
    amount: Mapped[int]
    fee: Mapped[int]
    currency: Mapped[str]
    success: Mapped[bool]
    refunded: Mapped[bool]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    external_id: Mapped[str] = mapped_column("externalId")
    payment_option: Mapped[Optional[str]] = mapped_column("paymentOption", Enum("ON_BOOKING", "HOLD", name="PaymentOption"), default="ON_BOOKING", server_default=text("'ON_BOOKING'"))

    __table_args__ = (
        Index("Payment_uid_key", "uid", unique=True),
        Index("Payment_externalId_key", "externalId", unique=True),
        Index("Payment_bookingId_idx", "bookingId"),
        Index("Payment_externalId_idx", "externalId"),
    )

    app: Mapped[Optional["App"]] = relationship(back_populates="payments")
    booking: Mapped[Optional["Booking"]] = relationship(back_populates="payment")

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_teamId_fkey"))
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_eventTypeId_fkey"))
    platform_oauth_client_id: Mapped[Optional[str]] = mapped_column("platformOAuthClientId", ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_platformOAuthClientId_fkey"))
    subscriber_url: Mapped[str] = mapped_column("subscriberUrl")
    payload_template: Mapped[Optional[str]] = mapped_column("payloadTemplate")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    event_triggers: Mapped[list[str]] = mapped_column("eventTriggers", ARRAY(Enum("BOOKING_CREATED", "BOOKING_PAYMENT_INITIATED", "BOOKING_PAID", "BOOKING_RESCHEDULED", "BOOKING_REQUESTED", "BOOKING_CANCELLED", "BOOKING_REJECTED", "BOOKING_NO_SHOW_UPDATED", "FORM_SUBMITTED", "MEETING_ENDED", "MEETING_STARTED", "RECORDING_READY", "RECORDING_TRANSCRIPTION_GENERATED", "OOO_CREATED", "AFTER_HOSTS_CAL_VIDEO_NO_SHOW", "AFTER_GUESTS_CAL_VIDEO_NO_SHOW", "FORM_SUBMITTED_NO_EVENT", "DELEGATION_CREDENTIAL_ERROR", "WRONG_ASSIGNMENT_REPORT", name="WebhookTriggerEvents")), nullable=True)
    app_id: Mapped[Optional[str]] = mapped_column("appId", ForeignKey("App.slug", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_appId_fkey"))
    secret: Mapped[Optional[str]]
    platform: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    time: Mapped[Optional[int]]
    time_unit: Mapped[Optional[str]] = mapped_column("timeUnit", Enum("day", "hour", "minute", name="TimeUnit"))
    version: Mapped[str] = mapped_column(default="2021-10-20", server_default=text("'2021-10-20'"))

    __table_args__ = (
        Index("Webhook_id_key", "id", unique=True),
        Index("Webhook_active_idx", "active"),
        Index("Webhook_userId_subscriberUrl_key", "userId", "subscriberUrl", unique=True),
        Index("Webhook_platformOAuthClientId_subscriberUrl_key", "platformOAuthClientId", "subscriberUrl", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="webhooks")
    team: Mapped[Optional["Team"]] = relationship(back_populates="webhooks")
    event_type: Mapped[Optional["EventType"]] = relationship(back_populates="webhooks")
    platform_oauth_client: Mapped[Optional["PlatformOAuthClient"]] = relationship(back_populates="webhook")
    app: Mapped[Optional["App"]] = relationship(back_populates="webhook")
    scheduled_triggers: Mapped[list["WebhookScheduledTriggers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="webhook")

class ApiKey(Base):
    __tablename__ = "ApiKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_teamId_fkey"))
    note: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    app_id: Mapped[Optional[str]] = mapped_column("appId", ForeignKey("App.slug", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_appId_fkey"))

    __table_args__ = (
        Index("ApiKey_id_key", "id", unique=True),
        Index("ApiKey_hashedKey_key", "hashedKey", unique=True),
        Index("ApiKey_userId_idx", "userId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="api_keys")
    team: Mapped[Optional["Team"]] = relationship(back_populates="api_keys")
    app: Mapped[Optional["App"]] = relationship(back_populates="api_key")
    rate_limits: Mapped[list["RateLimit"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="api_key")

class RateLimit(Base):
    __tablename__ = "RateLimit"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    api_key_id: Mapped[str] = mapped_column("apiKeyId", ForeignKey("ApiKey.id", ondelete="CASCADE", onupdate="CASCADE", name="RateLimit_apiKeyId_fkey"))
    ttl: Mapped[int]
    limit: Mapped[int]
    block_duration: Mapped[int] = mapped_column("blockDuration")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("RateLimit_apiKeyId_idx", "apiKeyId"),
    )

    api_key: Mapped["ApiKey"] = relationship(back_populates="rate_limits")

class HashedLink(Base):
    __tablename__ = "HashedLink"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    link: Mapped[str]
    event_type_id: Mapped[int] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="HashedLink_eventTypeId_fkey"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    max_usage_count: Mapped[int] = mapped_column("maxUsageCount", default=1, server_default=text("1"))
    usage_count: Mapped[int] = mapped_column("usageCount", default=0, server_default=text("0"))

    __table_args__ = (
        Index("HashedLink_link_key", "link", unique=True),
        Index("HashedLink_eventTypeId_idx", "eventTypeId"),
    )

    event_type: Mapped["EventType"] = relationship(back_populates="hashed_link")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    provider_email: Mapped[Optional[str]] = mapped_column("providerEmail")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_type_idx", "type"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="sessions")

class App(Base):
    __tablename__ = "App"

    slug: Mapped[str] = mapped_column(primary_key=True)
    dir_name: Mapped[str] = mapped_column("dirName")
    keys: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    categories: Mapped[list[str]] = mapped_column(ARRAY(Enum("calendar", "messaging", "other", "payment", "video", "web3", "automation", "analytics", "conferencing", "crm", name="AppCategories")), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("App_slug_key", "slug", unique=True),
        Index("App_dirName_key", "dirName", unique=True),
        Index("App_enabled_idx", "enabled"),
    )

    credentials: Mapped[list["Credential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="app")
    payments: Mapped[list["Payment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="app")
    webhook: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="app")
    api_key: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="app")

class Feedback(Base):
    __tablename__ = "Feedback"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    date: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Feedback_userId_fkey"))
    rating: Mapped[str]
    comment: Mapped[Optional[str]]

    __table_args__ = (
        Index("Feedback_userId_idx", "userId"),
        Index("Feedback_rating_idx", "rating"),
    )

    user: Mapped["User"] = relationship(back_populates="feedback")

class Deployment(Base):
    __tablename__ = "Deployment"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=False)
    logo: Mapped[Optional[str]]
    theme: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    license_key: Mapped[Optional[str]] = mapped_column("licenseKey")
    signature_token_encrypted: Mapped[Optional[str]] = mapped_column("signatureTokenEncrypted")
    agreed_license_at: Mapped[Optional[datetime]] = mapped_column("agreedLicenseAt")

class WebhookScheduledTriggers(Base):
    __tablename__ = "WebhookScheduledTriggers"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    job_name: Mapped[Optional[str]] = mapped_column("jobName")
    subscriber_url: Mapped[str] = mapped_column("subscriberUrl")
    payload: Mapped[str]
    start_after: Mapped[datetime] = mapped_column("startAfter")
    retry_count: Mapped[int] = mapped_column("retryCount", default=0, server_default=text("0"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    app_id: Mapped[Optional[str]] = mapped_column("appId")
    webhook_id: Mapped[Optional[str]] = mapped_column("webhookId", ForeignKey("Webhook.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookScheduledTriggers_webhookId_fkey"))
    booking_id: Mapped[Optional[int]] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="WebhookScheduledTriggers_bookingId_fkey"))

    webhook: Mapped[Optional["Webhook"]] = relationship(back_populates="scheduled_triggers")
    booking: Mapped[Optional["Booking"]] = relationship(back_populates="scheduled_triggers")

class BookingSeat(Base):
    __tablename__ = "BookingSeat"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    reference_uid: Mapped[str] = mapped_column("referenceUid")
    booking_id: Mapped[int] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingSeat_bookingId_fkey"))
    attendee_id: Mapped[int] = mapped_column("attendeeId", ForeignKey("Attendee.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingSeat_attendeeId_fkey"))
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("BookingSeat_referenceUid_key", "referenceUid", unique=True),
        Index("BookingSeat_attendeeId_key", "attendeeId", unique=True),
        Index("BookingSeat_bookingId_idx", "bookingId"),
        Index("BookingSeat_attendeeId_idx", "attendeeId"),
    )

    booking: Mapped["Booking"] = relationship(back_populates="seats_references")
    attendee: Mapped["Attendee"] = relationship(back_populates="booking_seat")

class VerifiedNumber(Base):
    __tablename__ = "VerifiedNumber"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="VerifiedNumber_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="VerifiedNumber_teamId_fkey"))
    phone_number: Mapped[str] = mapped_column("phoneNumber")

    __table_args__ = (
        Index("VerifiedNumber_userId_idx", "userId"),
        Index("VerifiedNumber_teamId_idx", "teamId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="verified_numbers")
    team: Mapped[Optional["Team"]] = relationship(back_populates="verified_numbers")

class VerifiedEmail(Base):
    __tablename__ = "VerifiedEmail"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="VerifiedEmail_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="VerifiedEmail_teamId_fkey"))
    email: Mapped[str]

    __table_args__ = (
        Index("VerifiedEmail_userId_idx", "userId"),
        Index("VerifiedEmail_teamId_idx", "teamId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="verified_emails")
    team: Mapped[Optional["Team"]] = relationship(back_populates="verified_emails")

class Feature(Base):
    __tablename__ = "Feature"

    slug: Mapped[str] = mapped_column(primary_key=True)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    description: Mapped[Optional[str]]
    type: Mapped[Optional[str]] = mapped_column(Enum("RELEASE", "EXPERIMENT", "OPERATIONAL", "KILL_SWITCH", "PERMISSION", name="FeatureType"), default="RELEASE", server_default=text("'RELEASE'"))
    stale: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[int]] = mapped_column("updatedBy")

    __table_args__ = (
        Index("Feature_slug_key", "slug", unique=True),
        Index("Feature_enabled_idx", "enabled"),
        Index("Feature_stale_idx", "stale"),
    )

    users: Mapped[list["UserFeatures"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="feature")
    teams: Mapped[list["TeamFeatures"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="feature")

class UserFeatures(Base):
    __tablename__ = "UserFeatures"

    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="UserFeatures_userId_fkey"), primary_key=True)
    feature_id: Mapped[str] = mapped_column("featureId", ForeignKey("Feature.slug", ondelete="CASCADE", onupdate="CASCADE", name="UserFeatures_featureId_fkey"), primary_key=True)
    enabled: Mapped[bool]
    assigned_at: Mapped[datetime] = mapped_column("assignedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    assigned_by: Mapped[str] = mapped_column("assignedBy")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserFeatures_userId_featureId_idx", "userId", "featureId"),
    )

    user: Mapped["User"] = relationship(back_populates="features")
    feature: Mapped["Feature"] = relationship(back_populates="users")

class TeamFeatures(Base):
    __tablename__ = "TeamFeatures"

    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamFeatures_teamId_fkey"), primary_key=True)
    feature_id: Mapped[str] = mapped_column("featureId", ForeignKey("Feature.slug", ondelete="CASCADE", onupdate="CASCADE", name="TeamFeatures_featureId_fkey"), primary_key=True)
    enabled: Mapped[bool]
    assigned_at: Mapped[datetime] = mapped_column("assignedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    assigned_by: Mapped[str] = mapped_column("assignedBy")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TeamFeatures_teamId_featureId_idx", "teamId", "featureId"),
    )

    team: Mapped["Team"] = relationship(back_populates="features")
    feature: Mapped["Feature"] = relationship(back_populates="teams")

class SelectedSlots(Base):
    __tablename__ = "SelectedSlots"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    event_type_id: Mapped[int] = mapped_column("eventTypeId")
    user_id: Mapped[int] = mapped_column("userId")
    slot_utc_start_date: Mapped[datetime] = mapped_column("slotUtcStartDate")
    slot_utc_end_date: Mapped[datetime] = mapped_column("slotUtcEndDate")
    uid: Mapped[str]
    release_at: Mapped[datetime] = mapped_column("releaseAt")
    is_seat: Mapped[bool] = mapped_column("isSeat", default=False, server_default=text("false"))

    __table_args__ = (
        Index("SelectedSlots_userId_slotUtcStartDate_slotUtcEndDate_uid_key", "userId", "slotUtcStartDate", "slotUtcEndDate", "uid", unique=True),
    )

class OAuthClient(Base):
    __tablename__ = "OAuthClient"

    client_id: Mapped[str] = mapped_column("clientId", primary_key=True)
    redirect_uri: Mapped[str] = mapped_column("redirectUri")
    client_secret: Mapped[Optional[str]] = mapped_column("clientSecret")
    client_type: Mapped[str] = mapped_column("clientType", Enum("confidential", "public", name="OAuthClientType"), default="confidential", server_default=text("'confidential'"))
    name: Mapped[str]
    purpose: Mapped[Optional[str]]
    logo: Mapped[Optional[str]]
    website_url: Mapped[Optional[str]] = mapped_column("websiteUrl")
    rejection_reason: Mapped[Optional[str]] = mapped_column("rejectionReason")
    is_trusted: Mapped[bool] = mapped_column("isTrusted", default=False, server_default=text("false"))
    status: Mapped[str] = mapped_column(Enum("pending", "approved", "rejected", name="OAuthClientStatus"), default="approved", server_default=text("'approved'"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="OAuthClient_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("OAuthClient_clientId_key", "clientId", unique=True),
        Index("OAuthClient_userId_idx", "userId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="o_auth_clients")
    access_codes: Mapped[list["AccessCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")

class AccessCode(Base):
    __tablename__ = "AccessCode"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    code: Mapped[str]
    client_id: Mapped[Optional[str]] = mapped_column("clientId", ForeignKey("OAuthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="AccessCode_clientId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Enum("READ_BOOKING", "READ_PROFILE", name="AccessScope")), nullable=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="AccessCode_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="AccessCode_teamId_fkey"))
    code_challenge: Mapped[Optional[str]] = mapped_column("codeChallenge")
    code_challenge_method: Mapped[Optional[str]] = mapped_column("codeChallengeMethod", default="S256", server_default=text("'S256'"))

    client: Mapped[Optional["OAuthClient"]] = relationship(back_populates="access_codes")
    user: Mapped[Optional["User"]] = relationship(back_populates="access_codes")
    team: Mapped[Optional["Team"]] = relationship(back_populates="access_codes")

class BookingDenormalized(Base):
    __tablename__ = "BookingDenormalized"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=False)
    uid: Mapped[str]
    event_type_id: Mapped[Optional[int]] = mapped_column("eventTypeId")
    title: Mapped[str]
    description: Mapped[Optional[str]]
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")
    location: Mapped[Optional[str]]
    paid: Mapped[bool]
    status: Mapped[str] = mapped_column(Enum("cancelled", "accepted", "rejected", "pending", "awaiting_host", name="BookingStatus"))
    rescheduled: Mapped[Optional[bool]]
    user_id: Mapped[Optional[int]] = mapped_column("userId")
    team_id: Mapped[Optional[int]] = mapped_column("teamId")
    event_length: Mapped[Optional[int]] = mapped_column("eventLength")
    event_parent_id: Mapped[Optional[int]] = mapped_column("eventParentId")
    user_email: Mapped[Optional[str]] = mapped_column("userEmail")
    user_name: Mapped[Optional[str]] = mapped_column("userName")
    user_username: Mapped[Optional[str]] = mapped_column("userUsername")
    rating_feedback: Mapped[Optional[str]] = mapped_column("ratingFeedback")
    rating: Mapped[Optional[int]]
    no_show_host: Mapped[Optional[bool]] = mapped_column("noShowHost")
    is_team_booking: Mapped[bool] = mapped_column("isTeamBooking")

    __table_args__ = (
        Index("BookingDenormalized_id_key", "id", unique=True),
        Index("BookingDenormalized_userId_idx", "userId"),
        Index("BookingDenormalized_createdAt_idx", "createdAt"),
        Index("BookingDenormalized_eventTypeId_idx", "eventTypeId"),
        Index("BookingDenormalized_eventParentId_idx", "eventParentId"),
        Index("BookingDenormalized_teamId_idx", "teamId"),
        Index("BookingDenormalized_startTime_idx", "startTime"),
        Index("BookingDenormalized_endTime_idx", "endTime"),
        Index("BookingDenormalized_status_idx", "status"),
        Index("BookingDenormalized_teamId_isTeamBooking_idx", "teamId", "isTeamBooking"),
        Index("BookingDenormalized_userId_isTeamBooking_idx", "userId", "isTeamBooking"),
        Index("BookingDenormalized_startTime_endTime_idx", "startTime", "endTime"),
    )

class CalendarCache(Base):
    __tablename__ = "CalendarCache"

    id: Mapped[Optional[str]] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    key: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[dict[str, Any]] = mapped_column(JSONB)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    credential_id: Mapped[int] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="CASCADE", onupdate="CASCADE", name="CalendarCache_credentialId_fkey"), primary_key=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId")

    __table_args__ = (
        Index("CalendarCache_userId_key_idx", "userId", "key"),
        Index("CalendarCache_credentialId_key_key", "credentialId", "key", unique=True),
    )

    credential: Mapped[Optional["Credential"]] = relationship(back_populates="calendar_cache")

class TempOrgRedirect(Base):
    __tablename__ = "TempOrgRedirect"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    from_: Mapped[str] = mapped_column("from")
    from_org_id: Mapped[int] = mapped_column("fromOrgId")
    type: Mapped[str] = mapped_column(Enum("user-event-type", "team-event-type", "user", "team", name="RedirectType"))
    to_url: Mapped[str] = mapped_column("toUrl")
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TempOrgRedirect_from_type_fromOrgId_key", "from", "type", "fromOrgId", unique=True),
    )

class OutOfOfficeEntry(Base):
    __tablename__ = "OutOfOfficeEntry"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    start: Mapped[datetime]
    end: Mapped[datetime]
    notes: Mapped[Optional[str]]
    show_note_publicly: Mapped[bool] = mapped_column("showNotePublicly", default=False, server_default=text("false"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="OutOfOfficeEntry_userId_fkey"))
    to_user_id: Mapped[Optional[int]] = mapped_column("toUserId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="OutOfOfficeEntry_toUserId_fkey"))
    reason_id: Mapped[Optional[int]] = mapped_column("reasonId", ForeignKey("OutOfOfficeReason.id", ondelete="SET NULL", onupdate="CASCADE", name="OutOfOfficeEntry_reasonId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OutOfOfficeEntry_uuid_key", "uuid", unique=True),
        Index("OutOfOfficeEntry_uuid_idx", "uuid"),
        Index("OutOfOfficeEntry_userId_idx", "userId"),
        Index("OutOfOfficeEntry_toUserId_idx", "toUserId"),
        Index("OutOfOfficeEntry_start_end_idx", "start", "end"),
    )

    user: Mapped["User"] = relationship(foreign_keys=[user_id], back_populates="booking_redirects")
    to_user: Mapped[Optional["User"]] = relationship(foreign_keys=[to_user_id], back_populates="booking_redirects_to")
    reason: Mapped[Optional["OutOfOfficeReason"]] = relationship(back_populates="entries")

class OutOfOfficeReason(Base):
    __tablename__ = "OutOfOfficeReason"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    emoji: Mapped[str]
    reason: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="OutOfOfficeReason_userId_fkey"))

    __table_args__ = (
        Index("OutOfOfficeReason_reason_key", "reason", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="out_of_office_reasons")
    entries: Mapped[list["OutOfOfficeEntry"]] = relationship(back_populates="reason")

class UserHolidaySettings(Base):
    __tablename__ = "UserHolidaySettings"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="UserHolidaySettings_userId_fkey"))
    country_code: Mapped[Optional[str]] = mapped_column("countryCode")
    disabled_ids: Mapped[list[str]] = mapped_column("disabledIds", ARRAY(Text), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserHolidaySettings_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="holiday_settings")

class HolidayCache(Base):
    __tablename__ = "HolidayCache"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    country_code: Mapped[str] = mapped_column("countryCode")
    calendar_id: Mapped[str] = mapped_column("calendarId")
    event_id: Mapped[str] = mapped_column("eventId")
    name: Mapped[str]
    date: Mapped[datetime]
    year: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("HolidayCache_countryCode_year_idx", "countryCode", "year"),
        Index("HolidayCache_countryCode_date_idx", "countryCode", "date"),
        Index("HolidayCache_countryCode_eventId_key", "countryCode", "eventId", unique=True),
    )

class PlatformOAuthClient(Base):
    __tablename__ = "PlatformOAuthClient"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    secret: Mapped[str]
    permissions: Mapped[int]
    logo: Mapped[Optional[str]]
    redirect_uris: Mapped[list[str]] = mapped_column("redirectUris", ARRAY(Text), nullable=True)
    organization_id: Mapped[int] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformOAuthClient_organizationId_fkey"))
    booking_redirect_uri: Mapped[Optional[str]] = mapped_column("bookingRedirectUri")
    booking_cancel_redirect_uri: Mapped[Optional[str]] = mapped_column("bookingCancelRedirectUri")
    booking_reschedule_redirect_uri: Mapped[Optional[str]] = mapped_column("bookingRescheduleRedirectUri")
    are_emails_enabled: Mapped[bool] = mapped_column("areEmailsEnabled", default=False, server_default=text("false"))
    are_default_event_types_enabled: Mapped[bool] = mapped_column("areDefaultEventTypesEnabled", default=True, server_default=text("true"))
    are_calendar_events_enabled: Mapped[bool] = mapped_column("areCalendarEventsEnabled", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    organization: Mapped["Team"] = relationship(back_populates="platform_oauth_client")
    teams: Mapped[list["Team"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="created_by_oauth_client")
    access_tokens: Mapped[list["AccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    refresh_token: Mapped[list["RefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    authorization_tokens: Mapped[list["PlatformAuthorizationToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    webhook: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="platform_oauth_client")
    users: Mapped[list["User"]] = relationship(secondary=platform_oauth_client_to_user, back_populates="platform_oauth_clients")

class PlatformAuthorizationToken(Base):
    __tablename__ = "PlatformAuthorizationToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    platform_oauth_client_id: Mapped[str] = mapped_column("platformOAuthClientId", ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformAuthorizationToken_platformOAuthClientId_fkey"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformAuthorizationToken_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("PlatformAuthorizationToken_userId_platformOAuthClientId_key", "userId", "platformOAuthClientId", unique=True),
    )

    owner: Mapped["User"] = relationship(back_populates="platform_authorization_token")
    client: Mapped["PlatformOAuthClient"] = relationship(back_populates="authorization_tokens")

class AccessToken(Base):
    __tablename__ = "AccessToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    secret: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    platform_oauth_client_id: Mapped[str] = mapped_column("platformOAuthClientId", ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="AccessToken_platformOAuthClientId_fkey"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="AccessToken_userId_fkey"))

    __table_args__ = (
        Index("AccessToken_secret_key", "secret", unique=True),
    )

    owner: Mapped["User"] = relationship(back_populates="access_token")
    client: Mapped["PlatformOAuthClient"] = relationship(back_populates="access_tokens")

class RefreshToken(Base):
    __tablename__ = "RefreshToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    secret: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    platform_oauth_client_id: Mapped[str] = mapped_column("platformOAuthClientId", ForeignKey("PlatformOAuthClient.id", ondelete="CASCADE", onupdate="CASCADE", name="RefreshToken_platformOAuthClientId_fkey"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="RefreshToken_userId_fkey"))

    __table_args__ = (
        Index("RefreshToken_secret_key", "secret", unique=True),
    )

    owner: Mapped["User"] = relationship(back_populates="refresh_token")
    client: Mapped["PlatformOAuthClient"] = relationship(back_populates="refresh_token")

class DSyncData(Base):
    __tablename__ = "DSyncData"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    directory_id: Mapped[str] = mapped_column("directoryId")
    tenant: Mapped[str]
    organization_id: Mapped[Optional[int]] = mapped_column("organizationId", ForeignKey("OrganizationSettings.organizationId", ondelete="CASCADE", onupdate="CASCADE", name="DSyncData_organizationId_fkey"))

    __table_args__ = (
        Index("DSyncData_directoryId_key", "directoryId", unique=True),
        Index("DSyncData_organizationId_key", "organizationId", unique=True),
    )

    org: Mapped[Optional["OrganizationSettings"]] = relationship(back_populates="d_sync_data")
    team_group_mapping: Mapped[list["DSyncTeamGroupMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="directory")
    created_attribute_to_users: Mapped[list["AttributeToUser"]] = relationship(foreign_keys="AttributeToUser.created_by_dsync_id", back_populates="created_by_dsync")
    updated_attribute_to_users: Mapped[list["AttributeToUser"]] = relationship(foreign_keys="AttributeToUser.updated_by_dsync_id", back_populates="updated_by_dsync")

class DSyncTeamGroupMapping(Base):
    __tablename__ = "DSyncTeamGroupMapping"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    organization_id: Mapped[int] = mapped_column("organizationId")
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DSyncTeamGroupMapping_teamId_fkey"))
    directory_id: Mapped[str] = mapped_column("directoryId", ForeignKey("DSyncData.directoryId", ondelete="CASCADE", onupdate="CASCADE", name="DSyncTeamGroupMapping_directoryId_fkey"))
    group_name: Mapped[str] = mapped_column("groupName")

    __table_args__ = (
        Index("DSyncTeamGroupMapping_teamId_groupName_key", "teamId", "groupName", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="dsync_team_group_mapping")
    directory: Mapped["DSyncData"] = relationship(back_populates="team_group_mapping")

class SecondaryEmail(Base):
    __tablename__ = "SecondaryEmail"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="SecondaryEmail_userId_fkey"))
    email: Mapped[str]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")

    __table_args__ = (
        Index("SecondaryEmail_userId_idx", "userId"),
        Index("SecondaryEmail_email_emailVerified_idx", "email", "emailVerified"),
        Index("SecondaryEmail_email_key", "email", unique=True),
        Index("SecondaryEmail_userId_email_key", "userId", "email", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="secondary_emails")
    verification_tokens: Mapped[list["VerificationToken"]] = relationship(back_populates="secondary_email")
    event_types: Mapped[list["EventType"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="secondary_email")

class Task(Base):
    __tablename__ = "Task"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    scheduled_at: Mapped[datetime] = mapped_column("scheduledAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    succeeded_at: Mapped[Optional[datetime]] = mapped_column("succeededAt")
    type: Mapped[str]
    payload: Mapped[str]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_attempts: Mapped[int] = mapped_column("maxAttempts", default=3, server_default=text("3"))
    last_error: Mapped[Optional[str]] = mapped_column("lastError")
    last_failed_attempt_at: Mapped[Optional[datetime]] = mapped_column("lastFailedAttemptAt")
    reference_uid: Mapped[Optional[str]] = mapped_column("referenceUid")

    __table_args__ = (
        Index("Task_id_key", "id", unique=True),
        Index("Task_succeededAt_idx", "succeededAt"),
        Index("Task_scheduledAt_succeededAt_idx", "scheduledAt", "succeededAt"),
        Index("Task_referenceUid_type_key", "referenceUid", "type", unique=True),
    )

class PlatformBilling(Base):
    __tablename__ = "PlatformBilling"

    id: Mapped[int] = mapped_column(ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="PlatformBilling_id_fkey"), primary_key=True, autoincrement=False)
    customer_id: Mapped[str] = mapped_column("customerId")
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    price_id: Mapped[Optional[str]] = mapped_column("priceId")
    plan: Mapped[str] = mapped_column(default="none", server_default=text("'none'"))
    billing_cycle_start: Mapped[Optional[int]] = mapped_column("billingCycleStart")
    billing_cycle_end: Mapped[Optional[int]] = mapped_column("billingCycleEnd")
    overdue: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    manager_billing_id: Mapped[Optional[int]] = mapped_column("managerBillingId", ForeignKey("PlatformBilling.id", ondelete="SET NULL", onupdate="CASCADE", name="PlatformBilling_managerBillingId_fkey"))

    __table_args__ = (
        Index("PlatformBilling_id_key", "id", unique=True),
    )

    manager_billing: Mapped[Optional["PlatformBilling"]] = relationship(remote_side=[id], back_populates="managed_billings")
    team: Mapped["Team"] = relationship(back_populates="platform_billing")
    managed_billings: Mapped[list["PlatformBilling"]] = relationship(back_populates="manager_billing")

class AttributeOption(Base):
    __tablename__ = "AttributeOption"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    attribute_id: Mapped[str] = mapped_column("attributeId", ForeignKey("Attribute.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeOption_attributeId_fkey"))
    value: Mapped[str]
    slug: Mapped[str]
    is_group: Mapped[bool] = mapped_column("isGroup", default=False, server_default=text("false"))
    contains: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)

    attribute: Mapped["Attribute"] = relationship(back_populates="options")
    assigned_users: Mapped[list["AttributeToUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attribute_option")

class Attribute(Base):
    __tablename__ = "Attribute"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Attribute_teamId_fkey"))
    type: Mapped[str] = mapped_column(Enum("TEXT", "NUMBER", "SINGLE_SELECT", "MULTI_SELECT", name="AttributeType"))
    name: Mapped[str]
    slug: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    users_can_edit_relation: Mapped[bool] = mapped_column("usersCanEditRelation", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    is_weights_enabled: Mapped[bool] = mapped_column("isWeightsEnabled", default=False, server_default=text("false"))
    is_locked: Mapped[bool] = mapped_column("isLocked", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Attribute_teamId_idx", "teamId"),
        Index("Attribute_teamId_slug_key", "teamId", "slug", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="attributes")
    options: Mapped[list["AttributeOption"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attribute")
    attribute_sync_field_mappings: Mapped[list["AttributeSyncFieldMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attribute")

class AttributeToUser(Base):
    __tablename__ = "AttributeToUser"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    member_id: Mapped[int] = mapped_column("memberId", ForeignKey("Membership.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeToUser_memberId_fkey"))
    attribute_option_id: Mapped[str] = mapped_column("attributeOptionId", ForeignKey("AttributeOption.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeToUser_attributeOptionId_fkey"))
    weight: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by_id: Mapped[Optional[int]] = mapped_column("createdById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="AttributeToUser_createdById_fkey"))
    created_by_dsync_id: Mapped[Optional[str]] = mapped_column("createdByDSyncId", ForeignKey("DSyncData.directoryId", ondelete="SET NULL", onupdate="CASCADE", name="AttributeToUser_createdByDSyncId_fkey"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    updated_by_id: Mapped[Optional[int]] = mapped_column("updatedById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="AttributeToUser_updatedById_fkey"))
    updated_by_dsync_id: Mapped[Optional[str]] = mapped_column("updatedByDSyncId", ForeignKey("DSyncData.directoryId", ondelete="SET NULL", onupdate="CASCADE", name="AttributeToUser_updatedByDSyncId_fkey"))

    __table_args__ = (
        Index("AttributeToUser_memberId_attributeOptionId_key", "memberId", "attributeOptionId", unique=True),
    )

    member: Mapped["Membership"] = relationship(back_populates="attribute_to_user")
    attribute_option: Mapped["AttributeOption"] = relationship(back_populates="assigned_users")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="created_attribute_to_users")
    created_by_dsync: Mapped[Optional["DSyncData"]] = relationship(foreign_keys=[created_by_dsync_id], back_populates="created_attribute_to_users")
    updated_by: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by_id], back_populates="updated_attribute_to_users")
    updated_by_dsync: Mapped[Optional["DSyncData"]] = relationship(foreign_keys=[updated_by_dsync_id], back_populates="updated_attribute_to_users")

class AssignmentReason(Base):
    __tablename__ = "AssignmentReason"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    booking_id: Mapped[int] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="AssignmentReason_bookingId_fkey"))
    reason_enum: Mapped[str] = mapped_column("reasonEnum", Enum("REASSIGNED", "RR_REASSIGNED", "REROUTED", "SALESFORCE_ASSIGNMENT", name="AssignmentReasonEnum"))
    reason_string: Mapped[str] = mapped_column("reasonString")

    __table_args__ = (
        Index("AssignmentReason_id_key", "id", unique=True),
        Index("AssignmentReason_bookingId_idx", "bookingId"),
    )

    booking: Mapped["Booking"] = relationship(back_populates="assignment_reason")

class DelegationCredential(Base):
    __tablename__ = "DelegationCredential"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    workspace_platform_id: Mapped[int] = mapped_column("workspacePlatformId", ForeignKey("WorkspacePlatform.id", ondelete="CASCADE", onupdate="CASCADE", name="DelegationCredential_workspacePlatformId_fkey"))
    service_account_key: Mapped[dict[str, Any]] = mapped_column("serviceAccountKey", JSONB)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    last_enabled_at: Mapped[Optional[datetime]] = mapped_column("lastEnabledAt")
    last_disabled_at: Mapped[Optional[datetime]] = mapped_column("lastDisabledAt")
    organization_id: Mapped[int] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DelegationCredential_organizationId_fkey"))
    domain: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DelegationCredential_enabled_idx", "enabled"),
        Index("DelegationCredential_organizationId_domain_key", "organizationId", "domain", unique=True),
    )

    workspace_platform: Mapped["WorkspacePlatform"] = relationship(back_populates="delegation_credentials")
    organization: Mapped["Team"] = relationship(back_populates="delegation_credentials")
    selected_calendars: Mapped[list["SelectedCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="delegation_credential")
    destination_calendar: Mapped[list["DestinationCalendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="delegation_credential")
    booking_references: Mapped[list["BookingReference"]] = relationship(back_populates="delegation_credential")
    credentials: Mapped[list["Credential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="delegation_credential")

class WorkspacePlatform(Base):
    __tablename__ = "WorkspacePlatform"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    slug: Mapped[str]
    name: Mapped[str]
    description: Mapped[str]
    default_service_account_key: Mapped[dict[str, Any]] = mapped_column("defaultServiceAccountKey", JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("WorkspacePlatform_slug_key", "slug", unique=True),
    )

    delegation_credentials: Mapped[list["DelegationCredential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace_platform")

class EventTypeTranslation(Base):
    __tablename__ = "EventTypeTranslation"

    uid: Mapped[str] = mapped_column(primary_key=True)
    event_type_id: Mapped[int] = mapped_column("eventTypeId", ForeignKey("EventType.id", ondelete="CASCADE", onupdate="CASCADE", name="EventTypeTranslation_eventTypeId_fkey"))
    field: Mapped[str] = mapped_column(Enum("DESCRIPTION", "TITLE", name="EventTypeAutoTranslatedField"))
    source_locale: Mapped[str] = mapped_column("sourceLocale")
    target_locale: Mapped[str] = mapped_column("targetLocale")
    translated_text: Mapped[str] = mapped_column("translatedText")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[int] = mapped_column("createdBy", ForeignKey("users.id", ondelete="RESTRICT", onupdate="CASCADE", name="EventTypeTranslation_createdBy_fkey"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    updated_by: Mapped[Optional[int]] = mapped_column("updatedBy", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="EventTypeTranslation_updatedBy_fkey"))

    __table_args__ = (
        Index("EventTypeTranslation_eventTypeId_field_targetLocale_idx", "eventTypeId", "field", "targetLocale"),
        Index("EventTypeTranslation_eventTypeId_field_targetLocale_key", "eventTypeId", "field", "targetLocale", unique=True),
    )

    event_type: Mapped["EventType"] = relationship(back_populates="field_translations")
    creator: Mapped["User"] = relationship(foreign_keys=[created_by], back_populates="created_translations")
    updater: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="updated_translations")

class Watchlist(Base):
    __tablename__ = "Watchlist"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid_mod.uuid4)
    type: Mapped[str] = mapped_column(Enum("EMAIL", "DOMAIN", "USERNAME", name="WatchlistType"))
    value: Mapped[str]
    description: Mapped[Optional[str]]
    is_global: Mapped[bool] = mapped_column("isGlobal", default=False, server_default=text("false"))
    organization_id: Mapped[Optional[int]] = mapped_column("organizationId")
    action: Mapped[str] = mapped_column(Enum("REPORT", "BLOCK", "ALERT", name="WatchlistAction"), default="REPORT", server_default=text("'REPORT'"))
    source: Mapped[str] = mapped_column(Enum("MANUAL", "FREE_DOMAIN_POLICY", "SIGNUP", name="WatchlistSource"), default="MANUAL", server_default=text("'MANUAL'"))
    last_updated_at: Mapped[datetime] = mapped_column("lastUpdatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("Watchlist_isGlobal_action_organizationId_type_value_idx", "isGlobal", "action", "organizationId", "type", "value"),
        Index("Watchlist_type_value_organizationId_key", "type", "value", "organizationId", unique=True),
    )

    booking_reports: Mapped[list["BookingReport"]] = relationship(foreign_keys="BookingReport.watchlist_id", back_populates="watchlist")
    global_booking_reports: Mapped[list["BookingReport"]] = relationship(foreign_keys="BookingReport.global_watchlist_id", back_populates="global_watchlist")
    audits: Mapped[list["WatchlistAudit"]] = relationship(back_populates="watchlist")

class WatchlistAudit(Base):
    __tablename__ = "WatchlistAudit"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid6.uuid7)
    type: Mapped[str] = mapped_column(Enum("EMAIL", "DOMAIN", "USERNAME", name="WatchlistType"))
    value: Mapped[str]
    description: Mapped[Optional[str]]
    action: Mapped[str] = mapped_column(Enum("REPORT", "BLOCK", "ALERT", name="WatchlistAction"), default="REPORT", server_default=text("'REPORT'"))
    changed_at: Mapped[datetime] = mapped_column("changedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    changed_by_user_id: Mapped[Optional[int]] = mapped_column("changedByUserId")
    watchlist_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column("watchlistId", Uuid, ForeignKey("Watchlist.id", ondelete="SET NULL", onupdate="CASCADE", name="WatchlistAudit_watchlistId_fkey"))

    __table_args__ = (
        Index("WatchlistAudit_watchlistId_changedAt_idx", "watchlistId", "changedAt"),
    )

    watchlist: Mapped[Optional["Watchlist"]] = relationship(back_populates="audits")

class WatchlistEventAudit(Base):
    __tablename__ = "WatchlistEventAudit"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid6.uuid7)
    watchlist_id: Mapped[uuid_mod.UUID] = mapped_column("watchlistId", Uuid)
    event_type_id: Mapped[int] = mapped_column("eventTypeId")
    action_taken: Mapped[str] = mapped_column("actionTaken", Enum("REPORT", "BLOCK", "ALERT", name="WatchlistAction"))
    timestamp: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class BookingReport(Base):
    __tablename__ = "BookingReport"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid_mod.uuid4)
    booking_uid: Mapped[str] = mapped_column("bookingUid", ForeignKey("Booking.uid", ondelete="CASCADE", onupdate="CASCADE", name="BookingReport_bookingUid_fkey"))
    booker_email: Mapped[str] = mapped_column("bookerEmail")
    reported_by_id: Mapped[Optional[int]] = mapped_column("reportedById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingReport_reportedById_fkey"))
    organization_id: Mapped[Optional[int]] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingReport_organizationId_fkey"))
    reason: Mapped[str] = mapped_column(Enum("SPAM", "dont_know_person", "OTHER", name="BookingReportReason"))
    description: Mapped[Optional[str]]
    cancelled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(Enum("PENDING", "DISMISSED", "BLOCKED", name="BookingReportStatus"), default="PENDING", server_default=text("'PENDING'"))
    system_status: Mapped[str] = mapped_column("systemStatus", Enum("PENDING", "BLOCKED", "DISMISSED", name="SystemReportStatus"), default="PENDING", server_default=text("'PENDING'"))
    watchlist_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column("watchlistId", Uuid, ForeignKey("Watchlist.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingReport_watchlistId_fkey"))
    global_watchlist_id: Mapped[Optional[uuid_mod.UUID]] = mapped_column("globalWatchlistId", Uuid, ForeignKey("Watchlist.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingReport_globalWatchlistId_fkey"))

    __table_args__ = (
        Index("BookingReport_bookingUid_key", "bookingUid", unique=True),
        Index("BookingReport_bookerEmail_idx", "bookerEmail"),
        Index("BookingReport_reportedById_idx", "reportedById"),
        Index("BookingReport_organizationId_idx", "organizationId"),
        Index("BookingReport_watchlistId_idx", "watchlistId"),
        Index("BookingReport_globalWatchlistId_idx", "globalWatchlistId"),
        Index("BookingReport_systemStatus_idx", "systemStatus"),
        Index("BookingReport_createdAt_idx", "createdAt"),
    )

    booking: Mapped["Booking"] = relationship(back_populates="report")
    reported_by: Mapped[Optional["User"]] = relationship(back_populates="reported_bookings")
    organization: Mapped[Optional["Team"]] = relationship(back_populates="booking_reports")
    watchlist: Mapped[Optional["Watchlist"]] = relationship(foreign_keys=[watchlist_id], back_populates="booking_reports")
    global_watchlist: Mapped[Optional["Watchlist"]] = relationship(foreign_keys=[global_watchlist_id], back_populates="global_booking_reports")

class WrongAssignmentReport(Base):
    __tablename__ = "WrongAssignmentReport"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid_mod.uuid4)
    booking_uid: Mapped[str] = mapped_column("bookingUid", ForeignKey("Booking.uid", ondelete="CASCADE", onupdate="CASCADE", name="WrongAssignmentReport_bookingUid_fkey"))
    reported_by_id: Mapped[Optional[int]] = mapped_column("reportedById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="WrongAssignmentReport_reportedById_fkey"))
    correct_assignee: Mapped[Optional[str]] = mapped_column("correctAssignee")
    additional_notes: Mapped[str] = mapped_column("additionalNotes")
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="SET NULL", onupdate="CASCADE", name="WrongAssignmentReport_teamId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "REVIEWED", "RESOLVED", "DISMISSED", name="WrongAssignmentReportStatus"), default="PENDING", server_default=text("'PENDING'"))
    reviewed_by_id: Mapped[Optional[int]] = mapped_column("reviewedById", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="WrongAssignmentReport_reviewedById_fkey"))
    reviewed_at: Mapped[Optional[datetime]] = mapped_column("reviewedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WrongAssignmentReport_bookingUid_key", "bookingUid", unique=True),
        Index("WrongAssignmentReport_reportedById_idx", "reportedById"),
        Index("WrongAssignmentReport_teamId_idx", "teamId"),
        Index("WrongAssignmentReport_status_idx", "status"),
        Index("WrongAssignmentReport_reviewedById_idx", "reviewedById"),
        Index("WrongAssignmentReport_createdAt_idx", "createdAt"),
        Index("WrongAssignmentReport_teamId_status_createdAt_idx", "teamId", "status", "createdAt"),
    )

    booking: Mapped["Booking"] = relationship(back_populates="wrong_assignment_reports")
    reported_by: Mapped[Optional["User"]] = relationship(foreign_keys=[reported_by_id], back_populates="wrong_assignment_reports")
    team: Mapped[Optional["Team"]] = relationship(back_populates="wrong_assignment_reports")
    reviewed_by: Mapped[Optional["User"]] = relationship(foreign_keys=[reviewed_by_id], back_populates="reviewed_wrong_assignment_reports")

class OrganizationOnboarding(Base):
    __tablename__ = "OrganizationOnboarding"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_by_id: Mapped[int] = mapped_column("createdById", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationOnboarding_createdById_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    org_owner_email: Mapped[str] = mapped_column("orgOwnerEmail")
    error: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organization_id: Mapped[Optional[int]] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationOnboarding_organizationId_fkey"))
    billing_period: Mapped[str] = mapped_column("billingPeriod", Enum("MONTHLY", "ANNUALLY", name="BillingPeriod"))
    price_per_seat: Mapped[float] = mapped_column("pricePerSeat")
    seats: Mapped[int]
    is_platform: Mapped[bool] = mapped_column("isPlatform", default=False, server_default=text("false"))
    name: Mapped[str]
    slug: Mapped[str]
    logo: Mapped[Optional[str]]
    bio: Mapped[Optional[str]]
    brand_color: Mapped[Optional[str]] = mapped_column("brandColor")
    banner_url: Mapped[Optional[str]] = mapped_column("bannerUrl")
    is_domain_configured: Mapped[bool] = mapped_column("isDomainConfigured", default=False, server_default=text("false"))
    stripe_customer_id: Mapped[Optional[str]] = mapped_column("stripeCustomerId")
    stripe_subscription_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionId")
    stripe_subscription_item_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionItemId")
    invited_members: Mapped[dict[str, Any]] = mapped_column("invitedMembers", JSONB, default=lambda: [], server_default=text("'[]'"))
    teams: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    is_complete: Mapped[bool] = mapped_column("isComplete", default=False, server_default=text("false"))

    __table_args__ = (
        Index("OrganizationOnboarding_orgOwnerEmail_key", "orgOwnerEmail", unique=True),
        Index("OrganizationOnboarding_organizationId_key", "organizationId", unique=True),
        Index("OrganizationOnboarding_stripeCustomerId_key", "stripeCustomerId", unique=True),
        Index("OrganizationOnboarding_orgOwnerEmail_idx", "orgOwnerEmail"),
        Index("OrganizationOnboarding_stripeCustomerId_idx", "stripeCustomerId"),
    )

    created_by: Mapped["User"] = relationship(back_populates="created_organization_onboardings")
    organization: Mapped[Optional["Team"]] = relationship(back_populates="organization_onboarding")

class InternalNotePreset(Base):
    __tablename__ = "InternalNotePreset"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    cancellation_reason: Mapped[Optional[str]] = mapped_column("cancellationReason")
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="InternalNotePreset_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("InternalNotePreset_teamId_idx", "teamId"),
        Index("InternalNotePreset_teamId_name_key", "teamId", "name", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="internal_note_presets")
    booking_internal_note: Mapped[list["BookingInternalNote"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="note_preset")

class FilterSegment(Base):
    __tablename__ = "FilterSegment"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    table_identifier: Mapped[str] = mapped_column("tableIdentifier")
    scope: Mapped[str] = mapped_column(Enum("USER", "TEAM", name="FilterSegmentScope"))
    active_filters: Mapped[Optional[dict[str, Any]]] = mapped_column("activeFilters", JSONB(none_as_null=True))
    sorting: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    column_visibility: Mapped[Optional[dict[str, Any]]] = mapped_column("columnVisibility", JSONB(none_as_null=True))
    column_sizing: Mapped[Optional[dict[str, Any]]] = mapped_column("columnSizing", JSONB(none_as_null=True))
    per_page: Mapped[int] = mapped_column("perPage")
    search_term: Mapped[Optional[str]] = mapped_column("searchTerm")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="FilterSegment_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="FilterSegment_teamId_fkey"))

    __table_args__ = (
        Index("FilterSegment_scope_userId_tableIdentifier_idx", "scope", "userId", "tableIdentifier"),
        Index("FilterSegment_scope_teamId_tableIdentifier_idx", "scope", "teamId", "tableIdentifier"),
    )

    user: Mapped["User"] = relationship(back_populates="filter_segments")
    team: Mapped[Optional["Team"]] = relationship(back_populates="filter_segments")
    user_preferences: Mapped[list["UserFilterSegmentPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="segment")

class UserFilterSegmentPreference(Base):
    __tablename__ = "UserFilterSegmentPreference"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="UserFilterSegmentPreference_userId_fkey"))
    table_identifier: Mapped[str] = mapped_column("tableIdentifier")
    segment_id: Mapped[Optional[int]] = mapped_column("segmentId", ForeignKey("FilterSegment.id", ondelete="CASCADE", onupdate="CASCADE", name="UserFilterSegmentPreference_segmentId_fkey"))
    system_segment_id: Mapped[Optional[str]] = mapped_column("systemSegmentId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserFilterSegmentPreference_userId_idx", "userId"),
        Index("UserFilterSegmentPreference_segmentId_idx", "segmentId"),
        Index("UserFilterSegmentPreference_userId_tableIdentifier_key", "userId", "tableIdentifier", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="filter_segment_preferences")
    segment: Mapped[Optional["FilterSegment"]] = relationship(back_populates="user_preferences")

class BookingInternalNote(Base):
    __tablename__ = "BookingInternalNote"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    note_preset_id: Mapped[Optional[int]] = mapped_column("notePresetId", ForeignKey("InternalNotePreset.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingInternalNote_notePresetId_fkey"))
    text: Mapped[Optional[str]]
    booking_id: Mapped[int] = mapped_column("bookingId", ForeignKey("Booking.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingInternalNote_bookingId_fkey"))
    created_by_id: Mapped[int] = mapped_column("createdById", ForeignKey("users.id", ondelete="RESTRICT", onupdate="CASCADE", name="BookingInternalNote_createdById_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("BookingInternalNote_bookingId_idx", "bookingId"),
        Index("BookingInternalNote_bookingId_notePresetId_key", "bookingId", "notePresetId", unique=True),
    )

    note_preset: Mapped[Optional["InternalNotePreset"]] = relationship(back_populates="booking_internal_note")
    booking: Mapped["Booking"] = relationship(back_populates="internal_note")
    created_by: Mapped["User"] = relationship(back_populates="booking_internal_note")

class Role(Base):
    __tablename__ = "Role"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    color: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Role_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("SYSTEM", "CUSTOM", name="RoleType"), default="CUSTOM", server_default=text("'CUSTOM'"))

    __table_args__ = (
        Index("Role_teamId_idx", "teamId"),
        Index("Role_name_teamId_key", "name", "teamId", unique=True),
    )

    team: Mapped[Optional["Team"]] = relationship(back_populates="roles")
    permissions: Mapped[list["RolePermission"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="role")
    memberships: Mapped[list["Membership"]] = relationship(back_populates="custom_role")

class RolePermission(Base):
    __tablename__ = "RolePermission"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    role_id: Mapped[str] = mapped_column("roleId", ForeignKey("Role.id", ondelete="CASCADE", onupdate="CASCADE", name="RolePermission_roleId_fkey"))
    resource: Mapped[str]
    action: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("RolePermission_roleId_idx", "roleId"),
        Index("RolePermission_action_idx", "action"),
        Index("RolePermission_roleId_resource_action_key", "roleId", "resource", "action", unique=True),
    )

    role: Mapped["Role"] = relationship(back_populates="permissions")

class AuditActor(Base):
    __tablename__ = "AuditActor"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid6.uuid7)
    type: Mapped[str] = mapped_column(Enum("user", "guest", "attendee", "system", "app", name="AuditActorType"))
    user_uuid: Mapped[Optional[uuid_mod.UUID]] = mapped_column("userUuid", Uuid)
    attendee_id: Mapped[Optional[int]] = mapped_column("attendeeId")
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId")
    email: Mapped[Optional[str]]
    phone: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AuditActor_email_idx", "email"),
        Index("AuditActor_userUuid_idx", "userUuid"),
        Index("AuditActor_attendeeId_idx", "attendeeId"),
        Index("AuditActor_credentialId_idx", "credentialId"),
        Index("AuditActor_userUuid_key", "userUuid", unique=True),
        Index("AuditActor_attendeeId_key", "attendeeId", unique=True),
        Index("AuditActor_credentialId_key", "credentialId", unique=True),
        Index("AuditActor_email_key", "email", unique=True),
        Index("AuditActor_phone_key", "phone", unique=True),
    )

    booking_audits: Mapped[list["BookingAudit"]] = relationship(passive_deletes="all", back_populates="actor")

class BookingAudit(Base):
    __tablename__ = "BookingAudit"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True, default=uuid6.uuid7)
    booking_uid: Mapped[str] = mapped_column("bookingUid")
    actor_id: Mapped[uuid_mod.UUID] = mapped_column("actorId", Uuid, ForeignKey("AuditActor.id", ondelete="RESTRICT", onupdate="CASCADE", name="BookingAudit_actorId_fkey"))
    type: Mapped[str] = mapped_column(Enum("record_created", "record_updated", "record_deleted", name="BookingAuditType"))
    action: Mapped[str] = mapped_column(Enum("created", "cancelled", "accepted", "rejected", "pending", "awaiting_host", "rescheduled", "attendee_added", "attendee_removed", "reassignment", "location_changed", "no_show_updated", "reschedule_requested", "seat_booked", "seat_rescheduled", name="BookingAuditAction"))
    timestamp: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    source: Mapped[str] = mapped_column(Enum("api_v1", "api_v2", "webapp", "webhook", "system", "magic_link", "unknown", name="BookingAuditSource"))
    operation_id: Mapped[str] = mapped_column("operationId")
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    context: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("BookingAudit_actorId_idx", "actorId"),
        Index("BookingAudit_bookingUid_idx", "bookingUid"),
        Index("BookingAudit_timestamp_idx", "timestamp"),
        Index("BookingAudit_operationId_idx", "operationId"),
    )

    actor: Mapped["AuditActor"] = relationship(back_populates="booking_audits")

class Agent(Base):
    __tablename__ = "Agent"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Agent_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Agent_teamId_fkey"))
    provider_agent_id: Mapped[str] = mapped_column("providerAgentId")
    inbound_event_type_id: Mapped[Optional[int]] = mapped_column("inboundEventTypeId")
    outbound_event_type_id: Mapped[Optional[int]] = mapped_column("outboundEventTypeId")
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Agent_providerAgentId_key", "providerAgentId", unique=True),
        Index("Agent_userId_idx", "userId"),
        Index("Agent_teamId_idx", "teamId"),
        Index("Agent_inboundEventTypeId_idx", "inboundEventTypeId"),
        Index("Agent_outboundEventTypeId_idx", "outboundEventTypeId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="agents")
    team: Mapped[Optional["Team"]] = relationship(back_populates="agents")
    inbound_phone_numbers: Mapped[list["CalAiPhoneNumber"]] = relationship(foreign_keys="CalAiPhoneNumber.inbound_agent_id", back_populates="inbound_agent")
    outbound_phone_numbers: Mapped[list["CalAiPhoneNumber"]] = relationship(foreign_keys="CalAiPhoneNumber.outbound_agent_id", back_populates="outbound_agent")

class CalAiPhoneNumber(Base):
    __tablename__ = "CalAiPhoneNumber"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="CalAiPhoneNumber_userId_fkey"))
    team_id: Mapped[Optional[int]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="CalAiPhoneNumber_teamId_fkey"))
    phone_number: Mapped[str] = mapped_column("phoneNumber")
    provider: Mapped[str]
    provider_phone_number_id: Mapped[Optional[str]] = mapped_column("providerPhoneNumberId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    stripe_customer_id: Mapped[Optional[str]] = mapped_column("stripeCustomerId")
    stripe_subscription_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionId")
    subscription_status: Mapped[Optional[str]] = mapped_column("subscriptionStatus", Enum("ACTIVE", "PAST_DUE", "CANCELLED", "INCOMPLETE", "INCOMPLETE_EXPIRED", "TRIALING", "UNPAID", name="PhoneNumberSubscriptionStatus"))
    inbound_agent_id: Mapped[Optional[str]] = mapped_column("inboundAgentId", ForeignKey("Agent.id", ondelete="SET NULL", onupdate="CASCADE", name="CalAiPhoneNumber_inboundAgentId_fkey"))
    outbound_agent_id: Mapped[Optional[str]] = mapped_column("outboundAgentId", ForeignKey("Agent.id", ondelete="SET NULL", onupdate="CASCADE", name="CalAiPhoneNumber_outboundAgentId_fkey"))

    __table_args__ = (
        Index("CalAiPhoneNumber_phoneNumber_key", "phoneNumber", unique=True),
        Index("CalAiPhoneNumber_providerPhoneNumberId_key", "providerPhoneNumberId", unique=True),
        Index("CalAiPhoneNumber_stripeSubscriptionId_key", "stripeSubscriptionId", unique=True),
        Index("CalAiPhoneNumber_userId_idx", "userId"),
        Index("CalAiPhoneNumber_teamId_idx", "teamId"),
        Index("CalAiPhoneNumber_inboundAgentId_idx", "inboundAgentId"),
        Index("CalAiPhoneNumber_outboundAgentId_idx", "outboundAgentId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="cal_ai_phone_numbers")
    team: Mapped[Optional["Team"]] = relationship(back_populates="cal_ai_phone_numbers")
    inbound_agent: Mapped[Optional["Agent"]] = relationship(foreign_keys=[inbound_agent_id], back_populates="inbound_phone_numbers")
    outbound_agent: Mapped[Optional["Agent"]] = relationship(foreign_keys=[outbound_agent_id], back_populates="outbound_phone_numbers")

class TeamBilling(Base):
    __tablename__ = "TeamBilling"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamBilling_teamId_fkey"))
    subscription_id: Mapped[str] = mapped_column("subscriptionId")
    subscription_item_id: Mapped[str] = mapped_column("subscriptionItemId")
    customer_id: Mapped[str] = mapped_column("customerId")
    status: Mapped[str]
    plan_name: Mapped[str] = mapped_column("planName")
    subscription_start: Mapped[Optional[datetime]] = mapped_column("subscriptionStart")
    subscription_trial_end: Mapped[Optional[datetime]] = mapped_column("subscriptionTrialEnd")
    subscription_end: Mapped[Optional[datetime]] = mapped_column("subscriptionEnd")
    billing_period: Mapped[Optional[str]] = mapped_column("billingPeriod", Enum("MONTHLY", "ANNUALLY", name="BillingPeriod"))
    billing_mode: Mapped[str] = mapped_column("billingMode", Enum("SEATS", "ACTIVE_USERS", name="BillingMode"), default="SEATS", server_default=text("'SEATS'"))
    price_per_seat: Mapped[Optional[int]] = mapped_column("pricePerSeat")
    paid_seats: Mapped[Optional[int]] = mapped_column("paidSeats")
    high_water_mark: Mapped[Optional[int]] = mapped_column("highWaterMark")
    high_water_mark_period_start: Mapped[Optional[datetime]] = mapped_column("highWaterMarkPeriodStart")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TeamBilling_teamId_key", "teamId", unique=True),
        Index("TeamBilling_subscriptionId_key", "subscriptionId", unique=True),
    )

    team: Mapped[Optional["Team"]] = relationship(back_populates="team_billing")
    monthly_prorations: Mapped[list["MonthlyProration"]] = relationship(back_populates="team_billing")
    seat_change_logs: Mapped[list["SeatChangeLog"]] = relationship(back_populates="team_billing")

class OrganizationBilling(Base):
    __tablename__ = "OrganizationBilling"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationBilling_teamId_fkey"))
    subscription_id: Mapped[str] = mapped_column("subscriptionId")
    subscription_item_id: Mapped[str] = mapped_column("subscriptionItemId")
    customer_id: Mapped[str] = mapped_column("customerId")
    status: Mapped[str]
    plan_name: Mapped[str] = mapped_column("planName")
    subscription_start: Mapped[Optional[datetime]] = mapped_column("subscriptionStart")
    subscription_trial_end: Mapped[Optional[datetime]] = mapped_column("subscriptionTrialEnd")
    subscription_end: Mapped[Optional[datetime]] = mapped_column("subscriptionEnd")
    billing_period: Mapped[Optional[str]] = mapped_column("billingPeriod", Enum("MONTHLY", "ANNUALLY", name="BillingPeriod"))
    billing_mode: Mapped[str] = mapped_column("billingMode", Enum("SEATS", "ACTIVE_USERS", name="BillingMode"), default="SEATS", server_default=text("'SEATS'"))
    price_per_seat: Mapped[Optional[int]] = mapped_column("pricePerSeat")
    paid_seats: Mapped[Optional[int]] = mapped_column("paidSeats")
    high_water_mark: Mapped[Optional[int]] = mapped_column("highWaterMark")
    high_water_mark_period_start: Mapped[Optional[datetime]] = mapped_column("highWaterMarkPeriodStart")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrganizationBilling_teamId_key", "teamId", unique=True),
        Index("OrganizationBilling_subscriptionId_key", "subscriptionId", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="organization_billing")
    monthly_prorations: Mapped[list["MonthlyProration"]] = relationship(back_populates="organization_billing")
    seat_change_logs: Mapped[list["SeatChangeLog"]] = relationship(back_populates="organization_billing")

class SeatChangeLog(Base):
    __tablename__ = "SeatChangeLog"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="SeatChangeLog_teamId_fkey"))
    change_type: Mapped[str] = mapped_column("changeType", Enum("ADDITION", "REMOVAL", name="SeatChangeType"))
    seat_count: Mapped[int] = mapped_column("seatCount")
    user_id: Mapped[Optional[int]] = mapped_column("userId")
    triggered_by: Mapped[Optional[int]] = mapped_column("triggeredBy")
    change_date: Mapped[datetime] = mapped_column("changeDate", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    month_key: Mapped[str] = mapped_column("monthKey")
    operation_id: Mapped[Optional[str]] = mapped_column("operationId")
    processed_in_proration_id: Mapped[Optional[str]] = mapped_column("processedInProrationId", ForeignKey("MonthlyProration.id", ondelete="SET NULL", onupdate="CASCADE", name="SeatChangeLog_processedInProrationId_fkey"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    team_billing_id: Mapped[Optional[str]] = mapped_column("teamBillingId", ForeignKey("TeamBilling.id", ondelete="SET NULL", onupdate="CASCADE", name="SeatChangeLog_teamBillingId_fkey"))
    organization_billing_id: Mapped[Optional[str]] = mapped_column("organizationBillingId", ForeignKey("OrganizationBilling.id", ondelete="SET NULL", onupdate="CASCADE", name="SeatChangeLog_organizationBillingId_fkey"))

    __table_args__ = (
        Index("SeatChangeLog_teamId_monthKey_idx", "teamId", "monthKey"),
        Index("SeatChangeLog_teamId_processedInProrationId_idx", "teamId", "processedInProrationId"),
        Index("SeatChangeLog_monthKey_idx", "monthKey"),
        Index("SeatChangeLog_teamId_operationId_key", "teamId", "operationId", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="seat_change_logs")
    proration: Mapped[Optional["MonthlyProration"]] = relationship(back_populates="seat_changes")
    team_billing: Mapped[Optional["TeamBilling"]] = relationship(back_populates="seat_change_logs")
    organization_billing: Mapped[Optional["OrganizationBilling"]] = relationship(back_populates="seat_change_logs")

class MonthlyProration(Base):
    __tablename__ = "MonthlyProration"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[int] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="MonthlyProration_teamId_fkey"))
    month_key: Mapped[str] = mapped_column("monthKey")
    period_start: Mapped[datetime] = mapped_column("periodStart")
    period_end: Mapped[datetime] = mapped_column("periodEnd")
    seats_at_start: Mapped[int] = mapped_column("seatsAtStart")
    seats_added: Mapped[int] = mapped_column("seatsAdded")
    seats_removed: Mapped[int] = mapped_column("seatsRemoved")
    net_seat_increase: Mapped[int] = mapped_column("netSeatIncrease")
    seats_at_end: Mapped[int] = mapped_column("seatsAtEnd")
    subscription_id: Mapped[str] = mapped_column("subscriptionId")
    subscription_item_id: Mapped[str] = mapped_column("subscriptionItemId")
    customer_id: Mapped[str] = mapped_column("customerId")
    subscription_start: Mapped[datetime] = mapped_column("subscriptionStart")
    subscription_end: Mapped[datetime] = mapped_column("subscriptionEnd")
    remaining_days: Mapped[int] = mapped_column("remainingDays")
    price_per_seat: Mapped[int] = mapped_column("pricePerSeat")
    prorated_amount: Mapped[int] = mapped_column("proratedAmount")
    invoice_item_id: Mapped[Optional[str]] = mapped_column("invoiceItemId")
    invoice_id: Mapped[Optional[str]] = mapped_column("invoiceId")
    invoice_url: Mapped[Optional[str]] = mapped_column("invoiceUrl")
    status: Mapped[str] = mapped_column(Enum("PENDING", "INVOICE_CREATED", "CHARGED", "FAILED", "CANCELLED", name="ProrationStatus"), default="PENDING", server_default=text("'PENDING'"))
    charged_at: Mapped[Optional[datetime]] = mapped_column("chargedAt")
    failed_at: Mapped[Optional[datetime]] = mapped_column("failedAt")
    failure_reason: Mapped[Optional[str]] = mapped_column("failureReason")
    retry_count: Mapped[int] = mapped_column("retryCount", default=0, server_default=text("0"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    team_billing_id: Mapped[Optional[str]] = mapped_column("teamBillingId", ForeignKey("TeamBilling.id", ondelete="SET NULL", onupdate="CASCADE", name="MonthlyProration_teamBillingId_fkey"))
    organization_billing_id: Mapped[Optional[str]] = mapped_column("organizationBillingId", ForeignKey("OrganizationBilling.id", ondelete="SET NULL", onupdate="CASCADE", name="MonthlyProration_organizationBillingId_fkey"))

    __table_args__ = (
        Index("MonthlyProration_monthKey_status_idx", "monthKey", "status"),
        Index("MonthlyProration_status_idx", "status"),
        Index("MonthlyProration_teamId_idx", "teamId"),
        Index("MonthlyProration_teamId_monthKey_key", "teamId", "monthKey", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="monthly_prorations")
    team_billing: Mapped[Optional["TeamBilling"]] = relationship(back_populates="monthly_prorations")
    organization_billing: Mapped[Optional["OrganizationBilling"]] = relationship(back_populates="monthly_prorations")
    seat_changes: Mapped[list["SeatChangeLog"]] = relationship(back_populates="proration")

class CalendarCacheEvent(Base):
    __tablename__ = "CalendarCacheEvent"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    selected_calendar_id: Mapped[str] = mapped_column("selectedCalendarId", ForeignKey("SelectedCalendar.id", ondelete="CASCADE", onupdate="CASCADE", name="CalendarCacheEvent_selectedCalendarId_fkey"))
    external_id: Mapped[str] = mapped_column("externalId")
    external_etag: Mapped[str] = mapped_column("externalEtag")
    i_cal_uid: Mapped[Optional[str]] = mapped_column("iCalUID")
    i_cal_sequence: Mapped[int] = mapped_column("iCalSequence", default=0, server_default=text("0"))
    summary: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    location: Mapped[Optional[str]]
    start: Mapped[datetime]
    end: Mapped[datetime]
    is_all_day: Mapped[bool] = mapped_column("isAllDay", default=False, server_default=text("false"))
    time_zone: Mapped[Optional[str]] = mapped_column("timeZone")
    status: Mapped[str] = mapped_column(Enum("confirmed", "tentative", "cancelled", name="CalendarCacheEventStatus"), default="confirmed", server_default=text("'confirmed'"))
    recurring_event_id: Mapped[Optional[str]] = mapped_column("recurringEventId")
    original_start_time: Mapped[Optional[datetime]] = mapped_column("originalStartTime")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    external_created_at: Mapped[Optional[datetime]] = mapped_column("externalCreatedAt")
    external_updated_at: Mapped[Optional[datetime]] = mapped_column("externalUpdatedAt")

    __table_args__ = (
        Index("CalendarCacheEvent_start_end_status_idx", "start", "end", "status"),
        Index("CalendarCacheEvent_selectedCalendarId_iCalUID_idx", "selectedCalendarId", "iCalUID"),
        Index("CalendarCacheEvent_selectedCalendarId_externalId_key", "selectedCalendarId", "externalId", unique=True),
    )

    selected_calendar: Mapped["SelectedCalendar"] = relationship(back_populates="calendar_cache_events")

class IntegrationAttributeSync(Base):
    __tablename__ = "IntegrationAttributeSync"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[int] = mapped_column("organizationId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="IntegrationAttributeSync_organizationId_fkey"))
    name: Mapped[str]
    integration: Mapped[str]
    credential_id: Mapped[Optional[int]] = mapped_column("credentialId", ForeignKey("Credential.id", ondelete="CASCADE", onupdate="CASCADE", name="IntegrationAttributeSync_credentialId_fkey"))
    enabled: Mapped[bool]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("IntegrationAttributeSync_organizationId_idx", "organizationId"),
        Index("IntegrationAttributeSync_credentialId_idx", "credentialId"),
    )

    organization: Mapped["Team"] = relationship(back_populates="integration_attribute_syncs")
    credential: Mapped[Optional["Credential"]] = relationship(back_populates="integration_attribute_syncs")
    sync_field_mappings: Mapped[list["AttributeSyncFieldMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration_attribute_sync")
    attribute_sync_rule: Mapped[Optional["AttributeSyncRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration_attribute_sync")

class AttributeSyncRule(Base):
    __tablename__ = "AttributeSyncRule"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    integration_attribute_sync_id: Mapped[str] = mapped_column("integrationAttributeSyncId", ForeignKey("IntegrationAttributeSync.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeSyncRule_integrationAttributeSyncId_fkey"))
    rule: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AttributeSyncRule_integrationAttributeSyncId_key", "integrationAttributeSyncId", unique=True),
    )

    integration_attribute_sync: Mapped["IntegrationAttributeSync"] = relationship(back_populates="attribute_sync_rule")

class AttributeSyncFieldMapping(Base):
    __tablename__ = "AttributeSyncFieldMapping"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    integration_field_name: Mapped[str] = mapped_column("integrationFieldName")
    attribute_id: Mapped[str] = mapped_column("attributeId", ForeignKey("Attribute.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeSyncFieldMapping_attributeId_fkey"))
    enabled: Mapped[bool]
    integration_attribute_sync_id: Mapped[str] = mapped_column("integrationAttributeSyncId", ForeignKey("IntegrationAttributeSync.id", ondelete="CASCADE", onupdate="CASCADE", name="AttributeSyncFieldMapping_integrationAttributeSyncId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AttributeSyncFieldMapping_integrationAttributeSyncId_idx", "integrationAttributeSyncId"),
        Index("AttributeSyncFieldMapping_integrationAttributeSyncId_attrib_key", "integrationAttributeSyncId", "attributeId", unique=True),
    )

    attribute: Mapped["Attribute"] = relationship(back_populates="attribute_sync_field_mappings")
    integration_attribute_sync: Mapped["IntegrationAttributeSync"] = relationship(back_populates="sync_field_mappings")
