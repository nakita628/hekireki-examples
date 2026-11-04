import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

import uuid6
from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.utils import timezone


class UtcDateTimeField(models.DateTimeField):  # type: ignore[type-arg]
    def get_db_prep_value(self, value: Any, connection: BaseDatabaseWrapper, prepared: bool = False) -> Any:
        if not prepared:
            value = self.get_prep_value(value)
        if not isinstance(value, datetime):
            return super().get_db_prep_value(value, connection, prepared=True)
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_default_timezone())
        value = value.astimezone(dt_timezone.utc)
        if connection.vendor == "sqlite":
            return value.isoformat(timespec="milliseconds")
        return connection.ops.adapt_datetimefield_value(value.replace(tzinfo=None))

    def from_db_value(self, value: datetime | None, expression: Any, connection: BaseDatabaseWrapper) -> datetime | None:
        if value is None:
            return None
        if timezone.is_naive(value):
            value = value.replace(tzinfo=dt_timezone.utc)
        return value if settings.USE_TZ else timezone.make_naive(value, timezone.get_default_timezone())


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def uuid4_str() -> str:
    return str(uuid.uuid4())


class SchedulingType(models.TextChoices):
    ROUND_ROBIN = "roundRobin"
    COLLECTIVE = "collective"
    MANAGED = "managed"


class PeriodType(models.TextChoices):
    UNLIMITED = "unlimited"
    ROLLING = "rolling"
    ROLLING_WINDOW = "rolling_window"
    RANGE = "range"


class CreationSource(models.TextChoices):
    API_V1 = "api_v1"
    API_V2 = "api_v2"
    WEBAPP = "webapp"


class CancellationReasonRequirement(models.TextChoices):
    MANDATORY_BOTH = "MANDATORY_BOTH"
    MANDATORY_HOST_ONLY = "MANDATORY_HOST_ONLY"
    MANDATORY_ATTENDEE_ONLY = "MANDATORY_ATTENDEE_ONLY"
    OPTIONAL_BOTH = "OPTIONAL_BOTH"


class IdentityProvider(models.TextChoices):
    CAL = "CAL"
    GOOGLE = "GOOGLE"
    SAML = "SAML"
    AZUREAD = "AZUREAD"


class UserPermissionRole(models.TextChoices):
    USER = "USER"
    ADMIN = "ADMIN"


class CreditUsageType(models.TextChoices):
    SMS = "SMS"
    CAL_AI_PHONE_CALL = "CAL_AI_PHONE_CALL"


class CreditType(models.TextChoices):
    MONTHLY = "MONTHLY"
    ADDITIONAL = "ADDITIONAL"


class MembershipRole(models.TextChoices):
    MEMBER = "MEMBER"
    ADMIN = "ADMIN"
    OWNER = "OWNER"


class BookingStatus(models.TextChoices):
    CANCELLED = "cancelled"
    ACCEPTED = "accepted"
    REJECTED = "rejected"
    PENDING = "pending"
    AWAITING_HOST = "awaiting_host"


class EventTypeCustomInputType(models.TextChoices):
    TEXT = "text"
    TEXTLONG = "textLong"
    NUMBER = "number"
    BOOL = "bool"
    RADIO = "radio"
    PHONE = "phone"


class ReminderType(models.TextChoices):
    PENDING_BOOKING_CONFIRMATION = "PENDING_BOOKING_CONFIRMATION"


class PaymentOption(models.TextChoices):
    ON_BOOKING = "ON_BOOKING"
    HOLD = "HOLD"


class WebhookTriggerEvents(models.TextChoices):
    BOOKING_CREATED = "BOOKING_CREATED"
    BOOKING_PAYMENT_INITIATED = "BOOKING_PAYMENT_INITIATED"
    BOOKING_PAID = "BOOKING_PAID"
    BOOKING_RESCHEDULED = "BOOKING_RESCHEDULED"
    BOOKING_REQUESTED = "BOOKING_REQUESTED"
    BOOKING_CANCELLED = "BOOKING_CANCELLED"
    BOOKING_REJECTED = "BOOKING_REJECTED"
    BOOKING_NO_SHOW_UPDATED = "BOOKING_NO_SHOW_UPDATED"
    FORM_SUBMITTED = "FORM_SUBMITTED"
    MEETING_ENDED = "MEETING_ENDED"
    MEETING_STARTED = "MEETING_STARTED"
    RECORDING_READY = "RECORDING_READY"
    RECORDING_TRANSCRIPTION_GENERATED = "RECORDING_TRANSCRIPTION_GENERATED"
    OOO_CREATED = "OOO_CREATED"
    AFTER_HOSTS_CAL_VIDEO_NO_SHOW = "AFTER_HOSTS_CAL_VIDEO_NO_SHOW"
    AFTER_GUESTS_CAL_VIDEO_NO_SHOW = "AFTER_GUESTS_CAL_VIDEO_NO_SHOW"
    FORM_SUBMITTED_NO_EVENT = "FORM_SUBMITTED_NO_EVENT"
    DELEGATION_CREDENTIAL_ERROR = "DELEGATION_CREDENTIAL_ERROR"
    WRONG_ASSIGNMENT_REPORT = "WRONG_ASSIGNMENT_REPORT"


class AppCategories(models.TextChoices):
    calendar = "calendar"
    messaging = "messaging"
    other = "other"
    payment = "payment"
    video = "video"
    web3 = "web3"
    automation = "automation"
    analytics = "analytics"
    conferencing = "conferencing"
    crm = "crm"


class TimeUnit(models.TextChoices):
    DAY = "day"
    HOUR = "hour"
    MINUTE = "minute"


class FeatureType(models.TextChoices):
    RELEASE = "RELEASE"
    EXPERIMENT = "EXPERIMENT"
    OPERATIONAL = "OPERATIONAL"
    KILL_SWITCH = "KILL_SWITCH"
    PERMISSION = "PERMISSION"


class RRResetInterval(models.TextChoices):
    MONTH = "MONTH"
    DAY = "DAY"


class RRTimestampBasis(models.TextChoices):
    CREATED_AT = "CREATED_AT"
    START_TIME = "START_TIME"


class OAuthClientType(models.TextChoices):
    CONFIDENTIAL = "confidential"
    PUBLIC = "public"


class OAuthClientStatus(models.TextChoices):
    PENDING = "pending"
    APPROVED = "approved"
    REJECTED = "rejected"


class AccessScope(models.TextChoices):
    READ_BOOKING = "READ_BOOKING"
    READ_PROFILE = "READ_PROFILE"


class RedirectType(models.TextChoices):
    UserEventType = "user-event-type"
    TeamEventType = "team-event-type"
    User = "user"
    Team = "team"


class SMSLockState(models.TextChoices):
    LOCKED = "LOCKED"
    UNLOCKED = "UNLOCKED"
    REVIEW_NEEDED = "REVIEW_NEEDED"


class AttributeType(models.TextChoices):
    TEXT = "TEXT"
    NUMBER = "NUMBER"
    SINGLE_SELECT = "SINGLE_SELECT"
    MULTI_SELECT = "MULTI_SELECT"


class AssignmentReasonEnum(models.TextChoices):
    REASSIGNED = "REASSIGNED"
    RR_REASSIGNED = "RR_REASSIGNED"
    REROUTED = "REROUTED"
    SALESFORCE_ASSIGNMENT = "SALESFORCE_ASSIGNMENT"


class EventTypeAutoTranslatedField(models.TextChoices):
    DESCRIPTION = "DESCRIPTION"
    TITLE = "TITLE"


class WatchlistType(models.TextChoices):
    EMAIL = "EMAIL"
    DOMAIN = "DOMAIN"
    USERNAME = "USERNAME"


class WatchlistAction(models.TextChoices):
    REPORT = "REPORT"
    BLOCK = "BLOCK"
    ALERT = "ALERT"


class WatchlistSource(models.TextChoices):
    MANUAL = "MANUAL"
    FREE_DOMAIN_POLICY = "FREE_DOMAIN_POLICY"
    SIGNUP = "SIGNUP"


class BookingReportReason(models.TextChoices):
    SPAM = "SPAM"
    DONT_KNOW_PERSON = "dont_know_person"
    OTHER = "OTHER"


class BookingReportStatus(models.TextChoices):
    PENDING = "PENDING"
    DISMISSED = "DISMISSED"
    BLOCKED = "BLOCKED"


class SystemReportStatus(models.TextChoices):
    PENDING = "PENDING"
    BLOCKED = "BLOCKED"
    DISMISSED = "DISMISSED"


class WrongAssignmentReportStatus(models.TextChoices):
    PENDING = "PENDING"
    REVIEWED = "REVIEWED"
    RESOLVED = "RESOLVED"
    DISMISSED = "DISMISSED"


class BillingPeriod(models.TextChoices):
    MONTHLY = "MONTHLY"
    ANNUALLY = "ANNUALLY"


class BillingMode(models.TextChoices):
    SEATS = "SEATS"
    ACTIVE_USERS = "ACTIVE_USERS"


class FilterSegmentScope(models.TextChoices):
    USER = "USER"
    TEAM = "TEAM"


class RoleType(models.TextChoices):
    SYSTEM = "SYSTEM"
    CUSTOM = "CUSTOM"


class BookingAuditType(models.TextChoices):
    RECORD_CREATED = "record_created"
    RECORD_UPDATED = "record_updated"
    RECORD_DELETED = "record_deleted"


class BookingAuditAction(models.TextChoices):
    CREATED = "created"
    CANCELLED = "cancelled"
    ACCEPTED = "accepted"
    REJECTED = "rejected"
    PENDING = "pending"
    AWAITING_HOST = "awaiting_host"
    RESCHEDULED = "rescheduled"
    ATTENDEE_ADDED = "attendee_added"
    ATTENDEE_REMOVED = "attendee_removed"
    REASSIGNMENT = "reassignment"
    LOCATION_CHANGED = "location_changed"
    NO_SHOW_UPDATED = "no_show_updated"
    RESCHEDULE_REQUESTED = "reschedule_requested"
    SEAT_BOOKED = "seat_booked"
    SEAT_RESCHEDULED = "seat_rescheduled"


class BookingAuditSource(models.TextChoices):
    API_V1 = "api_v1"
    API_V2 = "api_v2"
    WEBAPP = "webapp"
    WEBHOOK = "webhook"
    SYSTEM = "system"
    MAGIC_LINK = "magic_link"
    UNKNOWN = "unknown"


class AuditActorType(models.TextChoices):
    USER = "user"
    GUEST = "guest"
    ATTENDEE = "attendee"
    SYSTEM = "system"
    APP = "app"


class PhoneNumberSubscriptionStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    PAST_DUE = "PAST_DUE"
    CANCELLED = "CANCELLED"
    INCOMPLETE = "INCOMPLETE"
    INCOMPLETE_EXPIRED = "INCOMPLETE_EXPIRED"
    TRIALING = "TRIALING"
    UNPAID = "UNPAID"


class SeatChangeType(models.TextChoices):
    ADDITION = "ADDITION"
    REMOVAL = "REMOVAL"


class ProrationStatus(models.TextChoices):
    PENDING = "PENDING"
    INVOICE_CREATED = "INVOICE_CREATED"
    CHARGED = "CHARGED"
    FAILED = "FAILED"
    CANCELLED = "CANCELLED"


class CalendarCacheEventStatus(models.TextChoices):
    confirmed = "confirmed"
    tentative = "tentative"
    cancelled = "cancelled"


class Host(models.Model):
    pk = models.CompositePrimaryKey("user_id", "event_type_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="hosts", db_column="userId", db_index=False)
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="hosts", db_column="eventTypeId", db_index=False)
    is_fixed = models.BooleanField(db_column="isFixed", default=False)
    priority = models.IntegerField(null=True)
    weight = models.IntegerField(null=True)
    weight_adjustment = models.IntegerField(null=True, db_column="weightAdjustment")
    schedule = models.ForeignKey("Schedule", on_delete=models.SET_NULL, related_name="host", db_column="scheduleId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    group = models.ForeignKey("HostGroup", on_delete=models.SET_NULL, related_name="hosts", db_column="groupId", null=True, db_index=False)
    member = models.ForeignKey("Membership", on_delete=models.CASCADE, related_name="host", db_column="memberId", null=True, db_index=False)

    class Meta:
        db_table = "Host"
        indexes = [
            models.Index(fields=["member"]),
            models.Index(fields=["user"]),
            models.Index(fields=["event_type"]),
            models.Index(fields=["schedule"]),
        ]


class HostGroup(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="host_groups", db_column="eventTypeId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "HostGroup"
        indexes = [
            models.Index(fields=["name"]),
            models.Index(fields=["event_type"]),
        ]


class HostLocation(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.IntegerField(db_column="userId")
    event_type_id = models.IntegerField(db_column="eventTypeId")
    type = models.TextField()
    credential = models.ForeignKey("Credential", on_delete=models.SET_NULL, related_name="host_locations", db_column="credentialId", null=True, db_index=False)
    link = models.TextField(null=True)
    address = models.TextField(null=True)
    phone_number = models.TextField(null=True, db_column="phoneNumber")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "HostLocation"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "event_type_id"], name="HostLocation_userId_eventTypeId_key"),
        ]
        indexes = [
            models.Index(fields=["credential"]),
            models.Index(fields=["event_type_id"]),
        ]


class CalVideoSettings(models.Model):
    event_type = models.OneToOneField("EventType", on_delete=models.CASCADE, related_name="cal_video_settings", primary_key=True, db_column="eventTypeId")
    disable_recording_for_organizer = models.BooleanField(db_column="disableRecordingForOrganizer", default=False)
    disable_recording_for_guests = models.BooleanField(db_column="disableRecordingForGuests", default=False)
    enable_automatic_transcription = models.BooleanField(db_column="enableAutomaticTranscription", default=False)
    enable_automatic_recording_for_organizer = models.BooleanField(db_column="enableAutomaticRecordingForOrganizer", default=False)
    redirect_url_on_exit = models.TextField(null=True, db_column="redirectUrlOnExit")
    disable_transcription_for_guests = models.BooleanField(db_column="disableTranscriptionForGuests", default=False)
    disable_transcription_for_organizer = models.BooleanField(db_column="disableTranscriptionForOrganizer", default=False)
    require_email_for_guests = models.BooleanField(db_column="requireEmailForGuests", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CalVideoSettings"


class VideoCallGuest(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    booking_uid = models.TextField(db_column="bookingUid")
    email = models.TextField()
    name = models.TextField()
    joined_at = UtcDateTimeField(null=True, db_column="joinedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VideoCallGuest"
        constraints = [
            models.UniqueConstraint(fields=["booking_uid", "email"], name="VideoCallGuest_bookingUid_email_key"),
        ]
        indexes = [
            models.Index(fields=["booking_uid"]),
            models.Index(fields=["email"]),
        ]


class EventType(models.Model):
    id = models.AutoField(primary_key=True)
    title = models.TextField()
    slug = models.TextField()
    description = models.TextField(null=True)
    interface_language = models.TextField(null=True, db_column="interfaceLanguage")
    position = models.IntegerField(default=0)
    locations = models.JSONField(null=True)
    length = models.IntegerField()
    offset_start = models.IntegerField(db_column="offsetStart", default=0)
    hidden = models.BooleanField(default=False)
    users: "models.ManyToManyField[User, UserEventtype]" = models.ManyToManyField("User", through="UserEventtype", related_name="event_types")
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="owned_event_types", db_column="userId", null=True, db_index=False)
    profile = models.ForeignKey("Profile", on_delete=models.SET_NULL, related_name="event_types", db_column="profileId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="event_types", db_column="teamId", null=True, db_index=False)
    use_event_level_selected_calendars = models.BooleanField(db_column="useEventLevelSelectedCalendars", default=False)
    event_name = models.TextField(null=True, db_column="eventName")
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="children", db_column="parentId", null=True, db_index=False)
    booking_fields = models.JSONField(null=True, db_column="bookingFields")
    time_zone = models.TextField(null=True, db_column="timeZone")
    period_type = models.TextField(choices=PeriodType.choices, db_column="periodType", default=PeriodType.UNLIMITED)
    period_start_date = UtcDateTimeField(null=True, db_column="periodStartDate")
    period_end_date = UtcDateTimeField(null=True, db_column="periodEndDate")
    period_days = models.IntegerField(null=True, db_column="periodDays")
    period_count_calendar_days = models.BooleanField(null=True, db_column="periodCountCalendarDays")
    lock_time_zone_toggle_on_booking_page = models.BooleanField(db_column="lockTimeZoneToggleOnBookingPage", default=False)
    locked_time_zone = models.TextField(null=True, db_column="lockedTimeZone")
    requires_confirmation = models.BooleanField(db_column="requiresConfirmation", default=False)
    requires_confirmation_will_block_slot = models.BooleanField(db_column="requiresConfirmationWillBlockSlot", default=False)
    requires_confirmation_for_free_email = models.BooleanField(db_column="requiresConfirmationForFreeEmail", default=False)
    requires_booker_email_verification = models.BooleanField(db_column="requiresBookerEmailVerification", default=False)
    can_send_cal_video_transcription_emails = models.BooleanField(db_column="canSendCalVideoTranscriptionEmails", default=True)
    auto_translate_description_enabled = models.BooleanField(db_column="autoTranslateDescriptionEnabled", default=False)
    auto_translate_instant_meeting_title_enabled = models.BooleanField(db_column="autoTranslateInstantMeetingTitleEnabled", default=True)
    recurring_event = models.JSONField(null=True, db_column="recurringEvent")
    disable_guests = models.BooleanField(db_column="disableGuests", default=False)
    hide_calendar_notes = models.BooleanField(db_column="hideCalendarNotes", default=False)
    hide_calendar_event_details = models.BooleanField(db_column="hideCalendarEventDetails", default=False)
    minimum_booking_notice = models.IntegerField(db_column="minimumBookingNotice", default=120)
    before_event_buffer = models.IntegerField(db_column="beforeEventBuffer", default=0)
    after_event_buffer = models.IntegerField(db_column="afterEventBuffer", default=0)
    seats_per_time_slot = models.IntegerField(null=True, db_column="seatsPerTimeSlot")
    only_show_first_available_slot = models.BooleanField(db_column="onlyShowFirstAvailableSlot", default=False)
    show_optimized_slots = models.BooleanField(null=True, db_column="showOptimizedSlots", default=False)
    disable_cancelling = models.BooleanField(null=True, db_column="disableCancelling", default=False)
    disable_rescheduling = models.BooleanField(null=True, db_column="disableRescheduling", default=False)
    minimum_reschedule_notice = models.IntegerField(null=True, db_column="minimumRescheduleNotice")
    seats_show_attendees = models.BooleanField(null=True, db_column="seatsShowAttendees", default=False)
    seats_show_availability_count = models.BooleanField(null=True, db_column="seatsShowAvailabilityCount", default=True)
    scheduling_type = models.TextField(choices=SchedulingType.choices, null=True, db_column="schedulingType")
    schedule = models.ForeignKey("Schedule", on_delete=models.SET_NULL, related_name="event_type", db_column="scheduleId", null=True, db_index=False)
    allow_rescheduling_cancelled_bookings = models.BooleanField(null=True, db_column="allowReschedulingCancelledBookings", default=False)
    price = models.IntegerField(default=0)
    currency = models.TextField(default="usd")
    slot_interval = models.IntegerField(null=True, db_column="slotInterval")
    metadata = models.JSONField(null=True)
    success_redirect_url = models.TextField(null=True, db_column="successRedirectUrl")
    forward_params_success_redirect = models.BooleanField(null=True, db_column="forwardParamsSuccessRedirect", default=True)
    booking_limits = models.JSONField(null=True, db_column="bookingLimits")
    duration_limits = models.JSONField(null=True, db_column="durationLimits")
    is_instant_event = models.BooleanField(db_column="isInstantEvent", default=False)
    instant_meeting_expiry_time_offset_in_seconds = models.IntegerField(db_column="instantMeetingExpiryTimeOffsetInSeconds", default=90)
    instant_meeting_schedule = models.ForeignKey("Schedule", on_delete=models.SET_NULL, related_name="instant_meeting_events", db_column="instantMeetingScheduleId", null=True, db_index=False)
    instant_meeting_parameters = ArrayField(models.TextField(), db_column="instantMeetingParameters")
    assign_all_team_members = models.BooleanField(db_column="assignAllTeamMembers", default=False)
    assign_rrmembers_using_segment = models.BooleanField(db_column="assignRRMembersUsingSegment", default=False)
    rr_segment_query_value = models.JSONField(null=True, db_column="rrSegmentQueryValue")
    use_event_type_destination_calendar_email = models.BooleanField(db_column="useEventTypeDestinationCalendarEmail", default=False)
    is_rrweights_enabled = models.BooleanField(db_column="isRRWeightsEnabled", default=False)
    max_lead_threshold = models.IntegerField(null=True, db_column="maxLeadThreshold")
    include_no_show_in_rrcalculation = models.BooleanField(db_column="includeNoShowInRRCalculation", default=False)
    allow_rescheduling_past_bookings = models.BooleanField(db_column="allowReschedulingPastBookings", default=False)
    hide_organizer_email = models.BooleanField(db_column="hideOrganizerEmail", default=False)
    max_active_bookings_per_booker = models.IntegerField(null=True, db_column="maxActiveBookingsPerBooker")
    max_active_booking_per_booker_offer_reschedule = models.BooleanField(db_column="maxActiveBookingPerBookerOfferReschedule", default=False)
    custom_reply_to_email = models.TextField(null=True, db_column="customReplyToEmail")
    event_type_color = models.JSONField(null=True, db_column="eventTypeColor")
    reschedule_with_same_round_robin_host = models.BooleanField(db_column="rescheduleWithSameRoundRobinHost", default=False)
    secondary_email = models.ForeignKey("SecondaryEmail", on_delete=models.CASCADE, related_name="event_types", db_column="secondaryEmailId", null=True, db_index=False)
    use_booker_timezone = models.BooleanField(db_column="useBookerTimezone", default=False)
    restriction_schedule = models.ForeignKey("Schedule", on_delete=models.SET_NULL, related_name="restriction_schedule", db_column="restrictionScheduleId", null=True, db_index=False)
    booking_requires_authentication = models.BooleanField(db_column="bookingRequiresAuthentication", default=False)
    rr_host_subset_enabled = models.BooleanField(db_column="rrHostSubsetEnabled", default=False)
    requires_cancellation_reason = models.TextField(choices=CancellationReasonRequirement.choices, null=True, db_column="requiresCancellationReason", default=CancellationReasonRequirement.MANDATORY_HOST_ONLY)
    enable_per_host_locations = models.BooleanField(db_column="enablePerHostLocations", default=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EventType"
        constraints = [
            models.UniqueConstraint(fields=["owner", "slug"], name="EventType_userId_slug_key"),
            models.UniqueConstraint(fields=["team", "slug"], name="EventType_teamId_slug_key"),
            models.UniqueConstraint(fields=["owner", "parent"], name="EventType_userId_parentId_key"),
        ]
        indexes = [
            models.Index(fields=["owner"]),
            models.Index(fields=["team"]),
            models.Index(fields=["profile"]),
            models.Index(fields=["schedule"]),
            models.Index(fields=["secondary_email"]),
            models.Index(fields=["parent"]),
            models.Index(fields=["parent", "team"]),
            models.Index(fields=["restriction_schedule"]),
        ]


class Credential(models.Model):
    id = models.AutoField(primary_key=True)
    type = models.TextField()
    key = models.JSONField()
    encrypted_key = models.TextField(null=True, db_column="encryptedKey")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="credentials", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="credentials", db_column="teamId", null=True, db_index=False)
    app = models.ForeignKey("App", on_delete=models.CASCADE, related_name="credentials", db_column="appId", null=True, db_index=False)
    subscription_id = models.TextField(null=True, db_column="subscriptionId")
    payment_status = models.TextField(null=True, db_column="paymentStatus")
    billing_cycle_start = models.IntegerField(null=True, db_column="billingCycleStart")
    invalid = models.BooleanField(null=True, default=False)
    delegation_credential = models.ForeignKey("DelegationCredential", on_delete=models.CASCADE, related_name="credentials", db_column="delegationCredentialId", null=True, db_index=False)

    class Meta:
        db_table = "Credential"
        indexes = [
            models.Index(fields=["app"]),
            models.Index(fields=["subscription_id"]),
            models.Index(fields=["invalid"]),
            models.Index(fields=["user", "delegation_credential"]),
            models.Index(fields=["team"]),
        ]


class DestinationCalendar(models.Model):
    id = models.AutoField(primary_key=True)
    integration = models.TextField()
    external_id = models.TextField(db_column="externalId")
    primary_email = models.TextField(null=True, db_column="primaryEmail")
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="destination_calendar", db_column="userId", null=True)
    event_type = models.OneToOneField("EventType", on_delete=models.CASCADE, related_name="destination_calendar", db_column="eventTypeId", null=True)
    credential = models.ForeignKey("Credential", on_delete=models.CASCADE, related_name="destination_calendars", db_column="credentialId", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    delegation_credential = models.ForeignKey("DelegationCredential", on_delete=models.CASCADE, related_name="destination_calendar", db_column="delegationCredentialId", null=True, db_index=False)
    custom_calendar_reminder = models.IntegerField(null=True, db_column="customCalendarReminder")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DestinationCalendar"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["event_type"]),
            models.Index(fields=["credential"]),
        ]


class UserPassword(models.Model):
    hash = models.TextField()
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="password", primary_key=True, db_column="userId")

    class Meta:
        db_table = "UserPassword"


class TravelSchedule(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="travel_schedules", db_column="userId", db_index=False)
    time_zone = models.TextField(db_column="timeZone")
    start_date = UtcDateTimeField(db_column="startDate")
    end_date = UtcDateTimeField(null=True, db_column="endDate")
    prev_time_zone = models.TextField(null=True, db_column="prevTimeZone")

    class Meta:
        db_table = "TravelSchedule"
        indexes = [
            models.Index(fields=["start_date"]),
            models.Index(fields=["end_date"]),
        ]


class User(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.UUIDField(unique=True, default=uuid.uuid4)
    username = models.TextField(null=True)
    name = models.TextField(null=True)
    email = models.TextField(unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    bio = models.TextField(null=True)
    avatar_url = models.TextField(null=True, db_column="avatarUrl")
    time_zone = models.TextField(db_column="timeZone", default="Europe/London")
    week_start = models.TextField(db_column="weekStart", default="Sunday")
    buffer_time = models.IntegerField(db_column="bufferTime", default=0)
    hide_branding = models.BooleanField(db_column="hideBranding", default=False)
    theme = models.TextField(null=True)
    app_theme = models.TextField(null=True, db_column="appTheme")
    created_date = UtcDateTimeField(db_column="created", default=timezone.now)
    trial_ends_at = UtcDateTimeField(null=True, db_column="trialEndsAt")
    last_active_at = UtcDateTimeField(null=True, db_column="lastActiveAt")
    default_schedule_id = models.IntegerField(null=True, db_column="defaultScheduleId")
    completed_onboarding = models.BooleanField(db_column="completedOnboarding", default=False)
    locale = models.TextField(null=True)
    time_format = models.IntegerField(null=True, db_column="timeFormat", default=12)
    two_factor_secret = models.TextField(null=True, db_column="twoFactorSecret")
    two_factor_enabled = models.BooleanField(db_column="twoFactorEnabled", default=False)
    backup_codes = models.TextField(null=True, db_column="backupCodes")
    identity_provider = models.TextField(choices=IdentityProvider.choices, db_column="identityProvider", default=IdentityProvider.CAL)
    identity_provider_id = models.TextField(null=True, db_column="identityProviderId")
    invited_to = models.IntegerField(null=True, db_column="invitedTo")
    brand_color = models.TextField(null=True, db_column="brandColor")
    dark_brand_color = models.TextField(null=True, db_column="darkBrandColor")
    allow_dynamic_booking = models.BooleanField(null=True, db_column="allowDynamicBooking", default=True)
    allow_seoindexing = models.BooleanField(null=True, db_column="allowSEOIndexing", default=True)
    receive_monthly_digest_email = models.BooleanField(null=True, db_column="receiveMonthlyDigestEmail", default=True)
    requires_booker_email_verification = models.BooleanField(null=True, db_column="requiresBookerEmailVerification", default=False)
    metadata = models.JSONField(null=True)
    verified = models.BooleanField(null=True, default=False)
    role = models.TextField(choices=UserPermissionRole.choices, default=UserPermissionRole.USER)
    organization = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="org_users", db_column="organizationId", null=True, db_index=False)
    locked = models.BooleanField(default=False)
    moved_to_profile = models.OneToOneField("Profile", on_delete=models.SET_NULL, related_name="moved_from_user", db_column="movedToProfileId", null=True)
    is_platform_managed = models.BooleanField(db_column="isPlatformManaged", default=False)
    sms_lock_state = models.TextField(choices=SMSLockState.choices, db_column="smsLockState", default=SMSLockState.UNLOCKED)
    sms_lock_reviewed_by_admin = models.BooleanField(db_column="smsLockReviewedByAdmin", default=False)
    referral_link_id = models.TextField(null=True, db_column="referralLinkId")
    creation_source = models.TextField(choices=CreationSource.choices, null=True, db_column="creationSource")
    auto_opt_in_features = models.BooleanField(db_column="autoOptInFeatures", default=False)

    class Meta:
        db_table = "users"
        constraints = [
            models.UniqueConstraint(fields=["email"], name="users_email_key"),
            models.UniqueConstraint(fields=["email", "username"], name="users_email_username_key"),
            models.UniqueConstraint(fields=["username", "organization"], name="users_username_organizationId_key"),
            models.UniqueConstraint(fields=["moved_to_profile"], name="users_movedToProfileId_key"),
        ]
        indexes = [
            models.Index(fields=["username"]),
            models.Index(fields=["email_verified"]),
            models.Index(fields=["identity_provider"]),
            models.Index(fields=["identity_provider_id"]),
        ]


class NotificationsSubscriptions(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="notifications_subscriptions", db_column="userId", db_index=False)
    subscription = models.TextField()

    class Meta:
        db_table = "NotificationsSubscriptions"
        indexes = [
            models.Index(fields=["user", "subscription"]),
        ]


class Profile(models.Model):
    id = models.AutoField(primary_key=True)
    uid = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="profiles", db_column="userId", db_index=False)
    organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="org_profiles", db_column="organizationId", db_index=False)
    username = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Profile"
        constraints = [
            models.UniqueConstraint(fields=["user", "organization"], name="Profile_userId_organizationId_key"),
            models.UniqueConstraint(fields=["username", "organization"], name="Profile_username_organizationId_key"),
        ]
        indexes = [
            models.Index(fields=["uid"]),
            models.Index(fields=["user"]),
            models.Index(fields=["organization"]),
        ]


class Team(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    slug = models.TextField(null=True)
    logo_url = models.TextField(null=True, db_column="logoUrl")
    cal_video_logo = models.TextField(null=True, db_column="calVideoLogo")
    app_logo = models.TextField(null=True, db_column="appLogo")
    app_icon_logo = models.TextField(null=True, db_column="appIconLogo")
    bio = models.TextField(null=True)
    hide_branding = models.BooleanField(db_column="hideBranding", default=False)
    hide_team_profile_link = models.BooleanField(db_column="hideTeamProfileLink", default=False)
    is_private = models.BooleanField(db_column="isPrivate", default=False)
    hide_book_ateam_member = models.BooleanField(db_column="hideBookATeamMember", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    metadata = models.JSONField(null=True)
    theme = models.TextField(null=True)
    rr_reset_interval = models.TextField(choices=RRResetInterval.choices, null=True, db_column="rrResetInterval", default=RRResetInterval.MONTH)
    rr_timestamp_basis = models.TextField(choices=RRTimestampBasis.choices, db_column="rrTimestampBasis", default=RRTimestampBasis.CREATED_AT)
    brand_color = models.TextField(null=True, db_column="brandColor")
    dark_brand_color = models.TextField(null=True, db_column="darkBrandColor")
    banner_url = models.TextField(null=True, db_column="bannerUrl")
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="children", db_column="parentId", null=True, db_index=False)
    time_format = models.IntegerField(null=True, db_column="timeFormat")
    time_zone = models.TextField(db_column="timeZone", default="Europe/London")
    week_start = models.TextField(db_column="weekStart", default="Sunday")
    is_organization = models.BooleanField(db_column="isOrganization", default=False)
    pending_payment = models.BooleanField(db_column="pendingPayment", default=False)
    is_platform = models.BooleanField(db_column="isPlatform", default=False)
    created_by_oauth_client = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="teams", db_column="createdByOAuthClientId", null=True, db_index=False)
    sms_lock_state = models.TextField(choices=SMSLockState.choices, db_column="smsLockState", default=SMSLockState.UNLOCKED)
    sms_lock_reviewed_by_admin = models.BooleanField(db_column="smsLockReviewedByAdmin", default=False)
    booking_limits = models.JSONField(null=True, db_column="bookingLimits")
    include_managed_events_in_limits = models.BooleanField(db_column="includeManagedEventsInLimits", default=False)
    auto_opt_in_features = models.BooleanField(db_column="autoOptInFeatures", default=False)

    class Meta:
        db_table = "Team"
        constraints = [
            models.UniqueConstraint(fields=["slug", "parent"], name="Team_slug_parentId_key"),
        ]
        indexes = [
            models.Index(fields=["parent"]),
        ]


class CreditBalance(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="credit_balance", db_column="teamId", null=True)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="credit_balance", db_column="userId", null=True)
    additional_credits = models.IntegerField(db_column="additionalCredits", default=0)
    limit_reached_at = UtcDateTimeField(null=True, db_column="limitReachedAt")
    warning_sent_at = UtcDateTimeField(null=True, db_column="warningSentAt")

    class Meta:
        db_table = "CreditBalance"


class CreditPurchaseLog(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    credit_balance = models.ForeignKey("CreditBalance", on_delete=models.CASCADE, related_name="purchase_logs", db_column="creditBalanceId", db_index=False)
    credits = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "CreditPurchaseLog"


class CreditExpenseLog(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    credit_balance = models.ForeignKey("CreditBalance", on_delete=models.CASCADE, related_name="expense_logs", db_column="creditBalanceId", db_index=False)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="expense_logs", to_field="uid", db_column="bookingUid", null=True, db_index=False)
    credits = models.IntegerField(null=True)
    credit_type = models.TextField(choices=CreditType.choices, db_column="creditType")
    date = UtcDateTimeField()
    sms_sid = models.TextField(null=True, db_column="smsSid")
    sms_segments = models.IntegerField(null=True, db_column="smsSegments")
    phone_number = models.TextField(null=True, db_column="phoneNumber")
    email = models.TextField(null=True)
    call_duration = models.IntegerField(null=True, db_column="callDuration")
    credit_for = models.TextField(choices=CreditUsageType.choices, null=True, db_column="creditFor")
    external_ref = models.TextField(null=True, unique=True, db_column="externalRef")

    class Meta:
        db_table = "CreditExpenseLog"


class OrganizationSettings(models.Model):
    id = models.AutoField(primary_key=True)
    organization = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="organization_settings", db_column="organizationId")
    is_organization_configured = models.BooleanField(db_column="isOrganizationConfigured", default=False)
    is_organization_verified = models.BooleanField(db_column="isOrganizationVerified", default=False)
    org_auto_accept_email = models.TextField(db_column="orgAutoAcceptEmail")
    lock_event_type_creation_for_users = models.BooleanField(db_column="lockEventTypeCreationForUsers", default=False)
    admin_gets_no_slots_notification = models.BooleanField(db_column="adminGetsNoSlotsNotification", default=False)
    is_admin_reviewed = models.BooleanField(db_column="isAdminReviewed", default=False)
    is_admin_apienabled = models.BooleanField(db_column="isAdminAPIEnabled", default=False)
    allow_seoindexing = models.BooleanField(db_column="allowSEOIndexing", default=False)
    org_profile_redirects_to_verified_domain = models.BooleanField(db_column="orgProfileRedirectsToVerifiedDomain", default=False)
    disable_phone_only_smsnotifications = models.BooleanField(db_column="disablePhoneOnlySMSNotifications", default=False)
    disable_autofill_on_booking_page = models.BooleanField(db_column="disableAutofillOnBookingPage", default=False)
    org_auto_join_on_signup = models.BooleanField(db_column="orgAutoJoinOnSignup", default=True)
    disable_attendee_confirmation_email = models.BooleanField(db_column="disableAttendeeConfirmationEmail", default=False)
    disable_attendee_cancellation_email = models.BooleanField(db_column="disableAttendeeCancellationEmail", default=False)
    disable_attendee_rescheduled_email = models.BooleanField(db_column="disableAttendeeRescheduledEmail", default=False)
    disable_attendee_request_email = models.BooleanField(db_column="disableAttendeeRequestEmail", default=False)
    disable_attendee_reassigned_email = models.BooleanField(db_column="disableAttendeeReassignedEmail", default=False)
    disable_attendee_awaiting_payment_email = models.BooleanField(db_column="disableAttendeeAwaitingPaymentEmail", default=False)
    disable_attendee_reschedule_request_email = models.BooleanField(db_column="disableAttendeeRescheduleRequestEmail", default=False)
    disable_attendee_location_change_email = models.BooleanField(db_column="disableAttendeeLocationChangeEmail", default=False)
    disable_attendee_new_event_email = models.BooleanField(db_column="disableAttendeeNewEventEmail", default=False)

    class Meta:
        db_table = "OrganizationSettings"


class Membership(models.Model):
    id = models.AutoField(primary_key=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="members", db_column="teamId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="teams", db_column="userId", db_index=False)
    accepted = models.BooleanField(default=False)
    role = models.TextField(choices=MembershipRole.choices)
    custom_role = models.ForeignKey("Role", on_delete=models.SET_NULL, related_name="memberships", db_column="customRoleId", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Membership"
        constraints = [
            models.UniqueConstraint(fields=["user", "team"], name="Membership_userId_teamId_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["user"]),
            models.Index(fields=["accepted"]),
            models.Index(fields=["role"]),
            models.Index(fields=["custom_role"]),
        ]


class VerificationToken(models.Model):
    id = models.AutoField(primary_key=True)
    identifier = models.TextField()
    token = models.TextField(unique=True)
    expires = UtcDateTimeField()
    expires_in_days = models.IntegerField(null=True, db_column="expiresInDays")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="invite_tokens", db_column="teamId", null=True, db_index=False)
    secondary_email = models.ForeignKey("SecondaryEmail", on_delete=models.SET_NULL, related_name="verification_tokens", db_column="secondaryEmailId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]
        indexes = [
            models.Index(fields=["token"]),
            models.Index(fields=["team"]),
            models.Index(fields=["secondary_email"]),
        ]


class InstantMeetingToken(models.Model):
    id = models.AutoField(primary_key=True)
    token = models.TextField(unique=True)
    expires = UtcDateTimeField()
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="instant_meeting_tokens", db_column="teamId", db_index=False)
    booking = models.OneToOneField("Booking", on_delete=models.CASCADE, related_name="instant_meeting_token", db_column="bookingId", null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InstantMeetingToken"
        indexes = [
            models.Index(fields=["token"]),
        ]


class BookingReference(models.Model):
    id = models.AutoField(primary_key=True)
    type = models.TextField()
    uid = models.TextField()
    meeting_id = models.TextField(null=True, db_column="meetingId")
    third_party_recurring_event_id = models.TextField(null=True, db_column="thirdPartyRecurringEventId")
    meeting_password = models.TextField(null=True, db_column="meetingPassword")
    meeting_url = models.TextField(null=True, db_column="meetingUrl")
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="references", db_column="bookingId", null=True, db_index=False)
    external_calendar_id = models.TextField(null=True, db_column="externalCalendarId")
    deleted = models.BooleanField(null=True)
    credential = models.ForeignKey("Credential", on_delete=models.SET_NULL, related_name="references", db_column="credentialId", null=True, db_index=False)
    delegation_credential = models.ForeignKey("DelegationCredential", on_delete=models.SET_NULL, related_name="booking_references", db_column="delegationCredentialId", null=True, db_index=False)

    class Meta:
        db_table = "BookingReference"
        indexes = [
            models.Index(fields=["booking"]),
            models.Index(fields=["type"]),
            models.Index(fields=["uid"]),
        ]


class Attendee(models.Model):
    id = models.AutoField(primary_key=True)
    email = models.TextField()
    name = models.TextField()
    time_zone = models.TextField(db_column="timeZone")
    phone_number = models.TextField(null=True, db_column="phoneNumber")
    locale = models.TextField(null=True, default="en")
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="attendees", db_column="bookingId", null=True, db_index=False)
    no_show = models.BooleanField(null=True, db_column="noShow", default=False)

    class Meta:
        db_table = "Attendee"
        indexes = [
            models.Index(fields=["email"]),
            models.Index(fields=["booking"]),
            models.Index(fields=["email", "booking"]),
        ]


class Booking(models.Model):
    id = models.AutoField(primary_key=True)
    uid = models.TextField(unique=True)
    idempotency_key = models.TextField(null=True, unique=True, db_column="idempotencyKey")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="bookings", db_column="userId", null=True, db_index=False)
    user_primary_email = models.TextField(null=True, db_column="userPrimaryEmail")
    event_type = models.ForeignKey("EventType", on_delete=models.SET_NULL, related_name="bookings", db_column="eventTypeId", null=True, db_index=False)
    title = models.TextField()
    description = models.TextField(null=True)
    custom_inputs = models.JSONField(null=True, db_column="customInputs")
    responses = models.JSONField(null=True)
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    location = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    status = models.TextField(choices=BookingStatus.choices, default=BookingStatus.ACCEPTED)
    paid = models.BooleanField(default=False)
    destination_calendar = models.ForeignKey("DestinationCalendar", on_delete=models.SET_NULL, related_name="booking", db_column="destinationCalendarId", null=True, db_index=False)
    cancellation_reason = models.TextField(null=True, db_column="cancellationReason")
    rejection_reason = models.TextField(null=True, db_column="rejectionReason")
    reassign_reason = models.TextField(null=True, db_column="reassignReason")
    reassign_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="reassigned_bookings", db_column="reassignById", null=True, db_index=False)
    dynamic_event_slug_ref = models.TextField(null=True, db_column="dynamicEventSlugRef")
    dynamic_group_slug_ref = models.TextField(null=True, db_column="dynamicGroupSlugRef")
    rescheduled = models.BooleanField(null=True)
    from_reschedule = models.TextField(null=True, db_column="fromReschedule")
    recurring_event_id = models.TextField(null=True, db_column="recurringEventId")
    sms_reminder_number = models.TextField(null=True, db_column="smsReminderNumber")
    scheduled_jobs = ArrayField(models.TextField(), db_column="scheduledJobs")
    metadata = models.JSONField(null=True)
    is_recorded = models.BooleanField(db_column="isRecorded", default=False)
    i_cal_uid = models.TextField(null=True, db_column="iCalUID", default="")
    i_cal_sequence = models.IntegerField(db_column="iCalSequence", default=0)
    rating = models.IntegerField(null=True)
    rating_feedback = models.TextField(null=True, db_column="ratingFeedback")
    no_show_host = models.BooleanField(null=True, db_column="noShowHost", default=False)
    one_time_password = models.TextField(null=True, unique=True, db_column="oneTimePassword", default=uuid4_str)
    cancelled_by = models.TextField(null=True, db_column="cancelledBy")
    rescheduled_by = models.TextField(null=True, db_column="rescheduledBy")
    creation_source = models.TextField(choices=CreationSource.choices, null=True, db_column="creationSource")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Booking"
        indexes = [
            models.Index(fields=["event_type"]),
            models.Index(fields=["user"]),
            models.Index(fields=["destination_calendar"]),
            models.Index(fields=["recurring_event_id"]),
            models.Index(fields=["uid"]),
            models.Index(fields=["status"]),
            models.Index(fields=["start_time", "end_time", "status"]),
            models.Index(fields=["from_reschedule"]),
            models.Index(fields=["user", "end_time"]),
            models.Index(fields=["user", "status", "start_time"]),
            models.Index(fields=["event_type", "status"]),
            models.Index(fields=["user", "created_at"]),
        ]


class Tracking(models.Model):
    id = models.AutoField(primary_key=True)
    booking = models.OneToOneField("Booking", on_delete=models.CASCADE, related_name="tracking", db_column="bookingId")
    utm_source = models.TextField(null=True)
    utm_medium = models.TextField(null=True)
    utm_campaign = models.TextField(null=True)
    utm_term = models.TextField(null=True)
    utm_content = models.TextField(null=True)

    class Meta:
        db_table = "Tracking"
        constraints = [
            models.UniqueConstraint(fields=["booking"], name="Tracking_bookingId_key"),
        ]


class Schedule(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="schedules", db_column="userId", db_index=False)
    name = models.TextField()
    time_zone = models.TextField(null=True, db_column="timeZone")

    class Meta:
        db_table = "Schedule"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Availability(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="availability", db_column="userId", null=True, db_index=False)
    event_type = models.ForeignKey("EventType", on_delete=models.SET_NULL, related_name="availability", db_column="eventTypeId", null=True, db_index=False)
    days = ArrayField(models.IntegerField())
    start_time = models.TimeField(db_column="startTime")
    end_time = models.TimeField(db_column="endTime")
    date = models.DateField(null=True)
    schedule = models.ForeignKey("Schedule", on_delete=models.SET_NULL, related_name="availability", db_column="scheduleId", null=True, db_index=False)

    class Meta:
        db_table = "Availability"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["event_type"]),
            models.Index(fields=["schedule"]),
        ]


class SelectedCalendar(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="selected_calendars", db_column="userId", db_index=False)
    integration = models.TextField()
    external_id = models.TextField(db_column="externalId")
    credential = models.ForeignKey("Credential", on_delete=models.CASCADE, related_name="selected_calendars", db_column="credentialId", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    google_channel_id = models.TextField(null=True, db_column="googleChannelId")
    google_channel_kind = models.TextField(null=True, db_column="googleChannelKind")
    google_channel_resource_id = models.TextField(null=True, db_column="googleChannelResourceId")
    google_channel_resource_uri = models.TextField(null=True, db_column="googleChannelResourceUri")
    google_channel_expiration = models.TextField(null=True, db_column="googleChannelExpiration")
    channel_id = models.TextField(null=True, db_column="channelId")
    channel_kind = models.TextField(null=True, db_column="channelKind")
    channel_resource_id = models.TextField(null=True, db_column="channelResourceId")
    channel_resource_uri = models.TextField(null=True, db_column="channelResourceUri")
    channel_expiration = UtcDateTimeField(null=True, db_column="channelExpiration")
    sync_subscribed_at = UtcDateTimeField(null=True, db_column="syncSubscribedAt")
    sync_subscribed_error_at = UtcDateTimeField(null=True, db_column="syncSubscribedErrorAt")
    sync_subscribed_error_count = models.IntegerField(db_column="syncSubscribedErrorCount", default=0)
    sync_token = models.TextField(null=True, db_column="syncToken")
    synced_at = UtcDateTimeField(null=True, db_column="syncedAt")
    sync_error_at = UtcDateTimeField(null=True, db_column="syncErrorAt")
    sync_error_count = models.IntegerField(null=True, db_column="syncErrorCount", default=0)
    delegation_credential = models.ForeignKey("DelegationCredential", on_delete=models.CASCADE, related_name="selected_calendars", db_column="delegationCredentialId", null=True, db_index=False)
    error = models.TextField(null=True)
    last_error_at = UtcDateTimeField(null=True, db_column="lastErrorAt")
    watch_attempts = models.IntegerField(db_column="watchAttempts", default=0)
    unwatch_attempts = models.IntegerField(db_column="unwatchAttempts", default=0)
    max_attempts = models.IntegerField(db_column="maxAttempts", default=3)
    event_type = models.ForeignKey("EventType", on_delete=models.SET_NULL, related_name="selected_calendars", db_column="eventTypeId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SelectedCalendar"
        constraints = [
            models.UniqueConstraint(fields=["user", "integration", "external_id", "event_type"], name="SelectedCalendar_userId_integration_externalId_eventTypeId_key"),
            models.UniqueConstraint(fields=["google_channel_id", "event_type"], name="SelectedCalendar_googleChannelId_eventTypeId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["external_id"]),
            models.Index(fields=["event_type"]),
            models.Index(fields=["credential"]),
            models.Index(fields=["channel_id"]),
            models.Index(fields=["integration", "google_channel_expiration", "error", "watch_attempts", "max_attempts"], name="SelectedCalendar_watch_idx"),
            models.Index(fields=["integration", "google_channel_expiration", "error", "unwatch_attempts", "max_attempts"], name="SelectedCalendar_unwatch_idx"),
        ]


class EventTypeCustomInput(models.Model):
    id = models.AutoField(primary_key=True)
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="custom_inputs", db_column="eventTypeId", db_index=False)
    label = models.TextField()
    type = models.TextField(choices=EventTypeCustomInputType.choices)
    options = models.JSONField(null=True)
    required = models.BooleanField()
    placeholder = models.TextField(default="")

    class Meta:
        db_table = "EventTypeCustomInput"
        indexes = [
            models.Index(fields=["event_type"]),
        ]


class ResetPasswordRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField()
    expires = UtcDateTimeField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ResetPasswordRequest"


class ReminderMail(models.Model):
    id = models.AutoField(primary_key=True)
    reference_id = models.IntegerField(db_column="referenceId")
    reminder_type = models.TextField(choices=ReminderType.choices, db_column="reminderType")
    elapsed_minutes = models.IntegerField(db_column="elapsedMinutes")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ReminderMail"
        indexes = [
            models.Index(fields=["reference_id"]),
            models.Index(fields=["reminder_type"]),
        ]


class Payment(models.Model):
    id = models.AutoField(primary_key=True)
    uid = models.TextField(unique=True)
    app = models.ForeignKey("App", on_delete=models.CASCADE, related_name="payments", db_column="appId", null=True, db_index=False)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="payment", db_column="bookingId", db_index=False)
    amount = models.IntegerField()
    fee = models.IntegerField()
    currency = models.TextField()
    success = models.BooleanField()
    refunded = models.BooleanField()
    data = models.JSONField()
    external_id = models.TextField(unique=True, db_column="externalId")
    payment_option = models.TextField(choices=PaymentOption.choices, null=True, db_column="paymentOption", default=PaymentOption.ON_BOOKING)

    class Meta:
        db_table = "Payment"
        indexes = [
            models.Index(fields=["booking"]),
            models.Index(fields=["external_id"]),
        ]


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="webhooks", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="webhooks", db_column="teamId", null=True, db_index=False)
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="webhooks", db_column="eventTypeId", null=True, db_index=False)
    platform_oauth_client = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="webhook", db_column="platformOAuthClientId", null=True, db_index=False)
    subscriber_url = models.TextField(db_column="subscriberUrl")
    payload_template = models.TextField(null=True, db_column="payloadTemplate")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    active = models.BooleanField(default=True)
    event_triggers = ArrayField(models.TextField(choices=WebhookTriggerEvents.choices), db_column="eventTriggers")
    app = models.ForeignKey("App", on_delete=models.CASCADE, related_name="webhook", db_column="appId", null=True, db_index=False)
    secret = models.TextField(null=True)
    platform = models.BooleanField(default=False)
    time = models.IntegerField(null=True)
    time_unit = models.TextField(choices=TimeUnit.choices, null=True, db_column="timeUnit")
    version = models.TextField(default="2021-10-20")

    class Meta:
        db_table = "Webhook"
        constraints = [
            models.UniqueConstraint(fields=["user", "subscriber_url"], name="Webhook_userId_subscriberUrl_key"),
            models.UniqueConstraint(fields=["platform_oauth_client", "subscriber_url"], name="Webhook_platformOAuthClientId_subscriberUrl_key"),
        ]
        indexes = [
            models.Index(fields=["active"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_keys", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="api_keys", db_column="teamId", null=True, db_index=False)
    note = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    app = models.ForeignKey("App", on_delete=models.CASCADE, related_name="api_key", db_column="appId", null=True, db_index=False)

    class Meta:
        db_table = "ApiKey"
        indexes = [
            models.Index(fields=["user"]),
        ]


class RateLimit(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    api_key = models.ForeignKey("ApiKey", on_delete=models.CASCADE, related_name="rate_limits", db_column="apiKeyId", db_index=False)
    ttl = models.IntegerField()
    limit = models.IntegerField()
    block_duration = models.IntegerField(db_column="blockDuration")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RateLimit"
        indexes = [
            models.Index(fields=["api_key"]),
        ]


class HashedLink(models.Model):
    id = models.AutoField(primary_key=True)
    link = models.TextField(unique=True)
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="hashed_link", db_column="eventTypeId", db_index=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    max_usage_count = models.IntegerField(db_column="maxUsageCount", default=1)
    usage_count = models.IntegerField(db_column="usageCount", default=0)

    class Meta:
        db_table = "HashedLink"
        indexes = [
            models.Index(fields=["event_type"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    provider_email = models.TextField(null=True, db_column="providerEmail")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["type"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
        ]


class App(models.Model):
    slug = models.TextField(primary_key=True, default=None)
    dir_name = models.TextField(unique=True, db_column="dirName")
    keys = models.JSONField(null=True)
    categories = ArrayField(models.TextField(choices=AppCategories.choices))
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    enabled = models.BooleanField(default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "App"
        indexes = [
            models.Index(fields=["enabled"]),
        ]


class Feedback(models.Model):
    id = models.AutoField(primary_key=True)
    date = UtcDateTimeField(default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="feedback", db_column="userId", db_index=False)
    rating = models.TextField()
    comment = models.TextField(null=True)

    class Meta:
        db_table = "Feedback"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["rating"]),
        ]


class Deployment(models.Model):
    id = models.IntegerField(primary_key=True)
    logo = models.TextField(null=True)
    theme = models.JSONField(null=True)
    license_key = models.TextField(null=True, db_column="licenseKey")
    signature_token_encrypted = models.TextField(null=True, db_column="signatureTokenEncrypted")
    agreed_license_at = UtcDateTimeField(null=True, db_column="agreedLicenseAt")

    class Meta:
        db_table = "Deployment"


class WebhookScheduledTriggers(models.Model):
    id = models.AutoField(primary_key=True)
    job_name = models.TextField(null=True, db_column="jobName")
    subscriber_url = models.TextField(db_column="subscriberUrl")
    payload = models.TextField()
    start_after = UtcDateTimeField(db_column="startAfter")
    retry_count = models.IntegerField(db_column="retryCount", default=0)
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    app_id = models.TextField(null=True, db_column="appId")
    webhook = models.ForeignKey("Webhook", on_delete=models.CASCADE, related_name="scheduled_triggers", db_column="webhookId", null=True, db_index=False)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="scheduled_triggers", db_column="bookingId", null=True, db_index=False)

    class Meta:
        db_table = "WebhookScheduledTriggers"


class BookingSeat(models.Model):
    id = models.AutoField(primary_key=True)
    reference_uid = models.TextField(unique=True, db_column="referenceUid")
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="seats_references", db_column="bookingId", db_index=False)
    attendee = models.OneToOneField("Attendee", on_delete=models.CASCADE, related_name="booking_seat", db_column="attendeeId")
    data = models.JSONField(null=True)
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "BookingSeat"
        indexes = [
            models.Index(fields=["booking"]),
            models.Index(fields=["attendee"]),
        ]


class VerifiedNumber(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="verified_numbers", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="verified_numbers", db_column="teamId", null=True, db_index=False)
    phone_number = models.TextField(db_column="phoneNumber")

    class Meta:
        db_table = "VerifiedNumber"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
        ]


class VerifiedEmail(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="verified_emails", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="verified_emails", db_column="teamId", null=True, db_index=False)
    email = models.TextField()

    class Meta:
        db_table = "VerifiedEmail"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
        ]


class Feature(models.Model):
    slug = models.TextField(primary_key=True, default=None)
    enabled = models.BooleanField(default=False)
    description = models.TextField(null=True)
    type = models.TextField(choices=FeatureType.choices, null=True, default=FeatureType.RELEASE)
    stale = models.BooleanField(null=True, default=False)
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    created_at = UtcDateTimeField(null=True, db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    updated_by = models.IntegerField(null=True, db_column="updatedBy")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Feature"
        indexes = [
            models.Index(fields=["enabled"]),
            models.Index(fields=["stale"]),
        ]


class UserFeatures(models.Model):
    pk = models.CompositePrimaryKey("user_id", "feature_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="features", db_column="userId", db_index=False)
    feature = models.ForeignKey("Feature", on_delete=models.CASCADE, related_name="users", db_column="featureId", db_index=False)
    enabled = models.BooleanField()
    assigned_at = UtcDateTimeField(db_column="assignedAt", default=timezone.now)
    assigned_by = models.TextField(db_column="assignedBy")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserFeatures"
        indexes = [
            models.Index(fields=["user", "feature"]),
        ]


class TeamFeatures(models.Model):
    pk = models.CompositePrimaryKey("team_id", "feature_id")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="features", db_column="teamId", db_index=False)
    feature = models.ForeignKey("Feature", on_delete=models.CASCADE, related_name="teams", db_column="featureId", db_index=False)
    enabled = models.BooleanField()
    assigned_at = UtcDateTimeField(db_column="assignedAt", default=timezone.now)
    assigned_by = models.TextField(db_column="assignedBy")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamFeatures"
        indexes = [
            models.Index(fields=["team", "feature"]),
        ]


class SelectedSlots(models.Model):
    id = models.AutoField(primary_key=True)
    event_type_id = models.IntegerField(db_column="eventTypeId")
    user_id = models.IntegerField(db_column="userId")
    slot_utc_start_date = UtcDateTimeField(db_column="slotUtcStartDate")
    slot_utc_end_date = UtcDateTimeField(db_column="slotUtcEndDate")
    uid = models.TextField()
    release_at = UtcDateTimeField(db_column="releaseAt")
    is_seat = models.BooleanField(db_column="isSeat", default=False)

    class Meta:
        db_table = "SelectedSlots"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "slot_utc_start_date", "slot_utc_end_date", "uid"], name="SelectedSlots_userId_slotUtcStartDate_slotUtcEndDate_uid_key"),
        ]


class OAuthClient(models.Model):
    client_id = models.TextField(primary_key=True, db_column="clientId", default=None)
    redirect_uri = models.TextField(db_column="redirectUri")
    client_secret = models.TextField(null=True, db_column="clientSecret")
    client_type = models.TextField(choices=OAuthClientType.choices, db_column="clientType", default=OAuthClientType.CONFIDENTIAL)
    name = models.TextField()
    purpose = models.TextField(null=True)
    logo = models.TextField(null=True)
    website_url = models.TextField(null=True, db_column="websiteUrl")
    rejection_reason = models.TextField(null=True, db_column="rejectionReason")
    is_trusted = models.BooleanField(db_column="isTrusted", default=False)
    status = models.TextField(choices=OAuthClientStatus.choices, default=OAuthClientStatus.APPROVED)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="o_auth_clients", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OAuthClient"
        indexes = [
            models.Index(fields=["user"]),
        ]


class AccessCode(models.Model):
    id = models.AutoField(primary_key=True)
    code = models.TextField()
    client = models.ForeignKey("OAuthClient", on_delete=models.CASCADE, related_name="access_codes", db_column="clientId", null=True, db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    scopes = ArrayField(models.TextField(choices=AccessScope.choices))
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="access_codes", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="access_codes", db_column="teamId", null=True, db_index=False)
    code_challenge = models.TextField(null=True, db_column="codeChallenge")
    code_challenge_method = models.TextField(null=True, db_column="codeChallengeMethod", default="S256")

    class Meta:
        db_table = "AccessCode"


class BookingDenormalized(models.Model):
    id = models.IntegerField(primary_key=True)
    uid = models.TextField()
    event_type_id = models.IntegerField(null=True, db_column="eventTypeId")
    title = models.TextField()
    description = models.TextField(null=True)
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    location = models.TextField(null=True)
    paid = models.BooleanField()
    status = models.TextField(choices=BookingStatus.choices)
    rescheduled = models.BooleanField(null=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    team_id = models.IntegerField(null=True, db_column="teamId")
    event_length = models.IntegerField(null=True, db_column="eventLength")
    event_parent_id = models.IntegerField(null=True, db_column="eventParentId")
    user_email = models.TextField(null=True, db_column="userEmail")
    user_name = models.TextField(null=True, db_column="userName")
    user_username = models.TextField(null=True, db_column="userUsername")
    rating_feedback = models.TextField(null=True, db_column="ratingFeedback")
    rating = models.IntegerField(null=True)
    no_show_host = models.BooleanField(null=True, db_column="noShowHost")
    is_team_booking = models.BooleanField(db_column="isTeamBooking")

    class Meta:
        db_table = "BookingDenormalized"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["event_type_id"]),
            models.Index(fields=["event_parent_id"]),
            models.Index(fields=["team_id"]),
            models.Index(fields=["start_time"]),
            models.Index(fields=["end_time"]),
            models.Index(fields=["status"]),
            models.Index(fields=["team_id", "is_team_booking"]),
            models.Index(fields=["user_id", "is_team_booking"]),
            models.Index(fields=["start_time", "end_time"]),
        ]


class CalendarCache(models.Model):
    pk = models.CompositePrimaryKey("credential_id", "key")
    id = models.TextField(null=True, default=uuid4_str)
    key = models.TextField()
    value = models.JSONField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    credential = models.ForeignKey("Credential", on_delete=models.CASCADE, related_name="calendar_cache", db_column="credentialId", db_index=False)
    user_id = models.IntegerField(null=True, db_column="userId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CalendarCache"
        constraints = [
            models.UniqueConstraint(fields=["credential", "key"], name="CalendarCache_credentialId_key_key"),
        ]
        indexes = [
            models.Index(fields=["user_id", "key"]),
        ]


class TempOrgRedirect(models.Model):
    id = models.AutoField(primary_key=True)
    from_field = models.TextField(db_column="from")
    from_org_id = models.IntegerField(db_column="fromOrgId")
    type = models.TextField(choices=RedirectType.choices)
    to_url = models.TextField(db_column="toUrl")
    enabled = models.BooleanField(default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TempOrgRedirect"
        constraints = [
            models.UniqueConstraint(fields=["from_field", "type", "from_org_id"], name="TempOrgRedirect_from_type_fromOrgId_key"),
        ]


class Avatar(models.Model):
    team_id = models.IntegerField(db_column="teamId", default=0)
    user_id = models.IntegerField(db_column="userId", default=0)
    data = models.TextField()
    object_key = models.TextField(primary_key=True, db_column="objectKey", default=None)
    is_banner = models.BooleanField(db_column="isBanner", default=False)

    class Meta:
        db_table = "avatars"
        constraints = [
            models.UniqueConstraint(fields=["team_id", "user_id", "is_banner"], name="avatars_teamId_userId_isBanner_key"),
        ]


class OutOfOfficeEntry(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    start = UtcDateTimeField()
    end = UtcDateTimeField()
    notes = models.TextField(null=True)
    show_note_publicly = models.BooleanField(db_column="showNotePublicly", default=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="booking_redirects", db_column="userId", db_index=False)
    to_user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="booking_redirects_to", db_column="toUserId", null=True, db_index=False)
    reason = models.ForeignKey("OutOfOfficeReason", on_delete=models.SET_NULL, related_name="entries", db_column="reasonId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OutOfOfficeEntry"
        indexes = [
            models.Index(fields=["uuid"]),
            models.Index(fields=["user"]),
            models.Index(fields=["to_user"]),
            models.Index(fields=["start", "end"]),
        ]


class OutOfOfficeReason(models.Model):
    id = models.AutoField(primary_key=True)
    emoji = models.TextField()
    reason = models.TextField(unique=True)
    enabled = models.BooleanField(default=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="out_of_office_reasons", db_column="userId", null=True, db_index=False)

    class Meta:
        db_table = "OutOfOfficeReason"


class UserHolidaySettings(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="holiday_settings", db_column="userId")
    country_code = models.TextField(null=True, db_column="countryCode")
    disabled_ids = ArrayField(models.TextField(), db_column="disabledIds")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserHolidaySettings"


class HolidayCache(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    country_code = models.TextField(db_column="countryCode")
    calendar_id = models.TextField(db_column="calendarId")
    event_id = models.TextField(db_column="eventId")
    name = models.TextField()
    date = UtcDateTimeField()
    year = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "HolidayCache"
        constraints = [
            models.UniqueConstraint(fields=["country_code", "event_id"], name="HolidayCache_countryCode_eventId_key"),
        ]
        indexes = [
            models.Index(fields=["country_code", "year"]),
            models.Index(fields=["country_code", "date"]),
        ]


class PlatformOAuthClient(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    secret = models.TextField()
    permissions = models.IntegerField()
    users: "models.ManyToManyField[User, PlatformOAuthClientToUser]" = models.ManyToManyField("User", through="PlatformOAuthClientToUser", related_name="platform_oauth_clients")
    logo = models.TextField(null=True)
    redirect_uris = ArrayField(models.TextField(), db_column="redirectUris")
    organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="platform_oauth_client", db_column="organizationId", db_index=False)
    booking_redirect_uri = models.TextField(null=True, db_column="bookingRedirectUri")
    booking_cancel_redirect_uri = models.TextField(null=True, db_column="bookingCancelRedirectUri")
    booking_reschedule_redirect_uri = models.TextField(null=True, db_column="bookingRescheduleRedirectUri")
    are_emails_enabled = models.BooleanField(db_column="areEmailsEnabled", default=False)
    are_default_event_types_enabled = models.BooleanField(db_column="areDefaultEventTypesEnabled", default=True)
    are_calendar_events_enabled = models.BooleanField(db_column="areCalendarEventsEnabled", default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "PlatformOAuthClient"


class PlatformAuthorizationToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="authorization_tokens", db_column="platformOAuthClientId", db_index=False)
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="platform_authorization_token", db_column="userId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "PlatformAuthorizationToken"
        constraints = [
            models.UniqueConstraint(fields=["owner", "client"], name="PlatformAuthorizationToken_userId_platformOAuthClientId_key"),
        ]


class AccessToken(models.Model):
    id = models.AutoField(primary_key=True)
    secret = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    client = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="access_tokens", db_column="platformOAuthClientId", db_index=False)
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="access_token", db_column="userId", db_index=False)

    class Meta:
        db_table = "AccessToken"


class RefreshToken(models.Model):
    id = models.AutoField(primary_key=True)
    secret = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    client = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="refresh_token", db_column="platformOAuthClientId", db_index=False)
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="refresh_token", db_column="userId", db_index=False)

    class Meta:
        db_table = "RefreshToken"


class DSyncData(models.Model):
    id = models.AutoField(primary_key=True)
    directory_id = models.TextField(unique=True, db_column="directoryId")
    tenant = models.TextField()
    org = models.OneToOneField("OrganizationSettings", on_delete=models.CASCADE, related_name="d_sync_data", to_field="organization", db_column="organizationId", null=True)

    class Meta:
        db_table = "DSyncData"


class DSyncTeamGroupMapping(models.Model):
    id = models.AutoField(primary_key=True)
    organization_id = models.IntegerField(db_column="organizationId")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="dsync_team_group_mapping", db_column="teamId", db_index=False)
    directory = models.ForeignKey("DSyncData", on_delete=models.CASCADE, related_name="team_group_mapping", to_field="directory_id", db_column="directoryId", db_index=False)
    group_name = models.TextField(db_column="groupName")

    class Meta:
        db_table = "DSyncTeamGroupMapping"
        constraints = [
            models.UniqueConstraint(fields=["team", "group_name"], name="DSyncTeamGroupMapping_teamId_groupName_key"),
        ]


class SecondaryEmail(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="secondary_emails", db_column="userId", db_index=False)
    email = models.TextField(unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")

    class Meta:
        db_table = "SecondaryEmail"
        constraints = [
            models.UniqueConstraint(fields=["email"], name="SecondaryEmail_email_key"),
            models.UniqueConstraint(fields=["user", "email"], name="SecondaryEmail_userId_email_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["email", "email_verified"]),
        ]


class Task(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    scheduled_at = UtcDateTimeField(db_column="scheduledAt", default=timezone.now)
    succeeded_at = UtcDateTimeField(null=True, db_column="succeededAt")
    type = models.TextField()
    payload = models.TextField()
    attempts = models.IntegerField(default=0)
    max_attempts = models.IntegerField(db_column="maxAttempts", default=3)
    last_error = models.TextField(null=True, db_column="lastError")
    last_failed_attempt_at = UtcDateTimeField(null=True, db_column="lastFailedAttemptAt")
    reference_uid = models.TextField(null=True, db_column="referenceUid")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Task"
        constraints = [
            models.UniqueConstraint(fields=["reference_uid", "type"], name="Task_referenceUid_type_key"),
        ]
        indexes = [
            models.Index(fields=["succeeded_at"]),
            models.Index(fields=["scheduled_at", "succeeded_at"]),
        ]


class ManagedOrganization(models.Model):
    managed_organization = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="managed_organization", primary_key=True, db_column="managedOrganizationId")
    manager_organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="managed_organizations", db_column="managerOrganizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ManagedOrganization"
        constraints = [
            models.UniqueConstraint(fields=["manager_organization", "managed_organization"], name="ManagedOrganization_managerOrganizationId_managedOrganizationId_key"),
        ]
        indexes = [
            models.Index(fields=["manager_organization"]),
        ]


class PlatformBilling(models.Model):
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="platform_billing", primary_key=True, db_column="id")
    customer_id = models.TextField(db_column="customerId")
    subscription_id = models.TextField(null=True, db_column="subscriptionId")
    price_id = models.TextField(null=True, db_column="priceId")
    plan = models.TextField(default="none")
    billing_cycle_start = models.IntegerField(null=True, db_column="billingCycleStart")
    billing_cycle_end = models.IntegerField(null=True, db_column="billingCycleEnd")
    overdue = models.BooleanField(null=True, default=False)
    manager_billing = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="managed_billings", db_column="managerBillingId", null=True, db_index=False)

    class Meta:
        db_table = "PlatformBilling"


class AttributeOption(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    attribute = models.ForeignKey("Attribute", on_delete=models.CASCADE, related_name="options", db_column="attributeId", db_index=False)
    value = models.TextField()
    slug = models.TextField()
    is_group = models.BooleanField(db_column="isGroup", default=False)
    contains = ArrayField(models.TextField())

    class Meta:
        db_table = "AttributeOption"


class Attribute(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="attributes", db_column="teamId", db_index=False)
    type = models.TextField(choices=AttributeType.choices)
    name = models.TextField()
    slug = models.TextField()
    enabled = models.BooleanField(default=True)
    users_can_edit_relation = models.BooleanField(db_column="usersCanEditRelation", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    is_weights_enabled = models.BooleanField(db_column="isWeightsEnabled", default=False)
    is_locked = models.BooleanField(db_column="isLocked", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Attribute"
        constraints = [
            models.UniqueConstraint(fields=["team", "slug"], name="Attribute_teamId_slug_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class AttributeToUser(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    member = models.ForeignKey("Membership", on_delete=models.CASCADE, related_name="attribute_to_user", db_column="memberId", db_index=False)
    attribute_option = models.ForeignKey("AttributeOption", on_delete=models.CASCADE, related_name="assigned_users", db_column="attributeOptionId", db_index=False)
    weight = models.IntegerField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_attribute_to_users", db_column="createdById", null=True, db_index=False)
    created_by_dsync = models.ForeignKey("DSyncData", on_delete=models.SET_NULL, related_name="created_attribute_to_users", to_field="directory_id", db_column="createdByDSyncId", null=True, db_index=False)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="updated_attribute_to_users", db_column="updatedById", null=True, db_index=False)
    updated_by_dsync = models.ForeignKey("DSyncData", on_delete=models.SET_NULL, related_name="updated_attribute_to_users", to_field="directory_id", db_column="updatedByDSyncId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttributeToUser"
        constraints = [
            models.UniqueConstraint(fields=["member", "attribute_option"], name="AttributeToUser_memberId_attributeOptionId_key"),
        ]


class AssignmentReason(models.Model):
    id = models.AutoField(primary_key=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="assignment_reason", db_column="bookingId", db_index=False)
    reason_enum = models.TextField(choices=AssignmentReasonEnum.choices, db_column="reasonEnum")
    reason_string = models.TextField(db_column="reasonString")

    class Meta:
        db_table = "AssignmentReason"
        indexes = [
            models.Index(fields=["booking"]),
        ]


class DelegationCredential(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    workspace_platform = models.ForeignKey("WorkspacePlatform", on_delete=models.CASCADE, related_name="delegation_credentials", db_column="workspacePlatformId", db_index=False)
    service_account_key = models.JSONField(db_column="serviceAccountKey")
    enabled = models.BooleanField(default=False)
    last_enabled_at = UtcDateTimeField(null=True, db_column="lastEnabledAt")
    last_disabled_at = UtcDateTimeField(null=True, db_column="lastDisabledAt")
    organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="delegation_credentials", db_column="organizationId", db_index=False)
    domain = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DelegationCredential"
        constraints = [
            models.UniqueConstraint(fields=["organization", "domain"], name="DelegationCredential_organizationId_domain_key"),
        ]
        indexes = [
            models.Index(fields=["enabled"]),
        ]


class WorkspacePlatform(models.Model):
    id = models.AutoField(primary_key=True)
    slug = models.TextField(unique=True)
    name = models.TextField()
    description = models.TextField()
    default_service_account_key = models.JSONField(db_column="defaultServiceAccountKey")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    enabled = models.BooleanField(default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkspacePlatform"
        constraints = [
            models.UniqueConstraint(fields=["slug"], name="WorkspacePlatform_slug_key"),
        ]


class EventTypeTranslation(models.Model):
    uid = models.TextField(primary_key=True, default=None)
    event_type = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="field_translations", db_column="eventTypeId", db_index=False)
    field = models.TextField(choices=EventTypeAutoTranslatedField.choices)
    source_locale = models.TextField(db_column="sourceLocale")
    target_locale = models.TextField(db_column="targetLocale")
    translated_text = models.TextField(db_column="translatedText")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    creator = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="created_translations", db_column="createdBy", db_index=False)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    updater = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="updated_translations", db_column="updatedBy", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EventTypeTranslation"
        constraints = [
            models.UniqueConstraint(fields=["event_type", "field", "target_locale"], name="EventTypeTranslation_eventTypeId_field_targetLocale_key"),
        ]
        indexes = [
            models.Index(fields=["event_type", "field", "target_locale"]),
        ]


class Watchlist(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    type = models.TextField(choices=WatchlistType.choices)
    value = models.TextField()
    description = models.TextField(null=True)
    is_global = models.BooleanField(db_column="isGlobal", default=False)
    organization_id = models.IntegerField(null=True, db_column="organizationId")
    action = models.TextField(choices=WatchlistAction.choices, default=WatchlistAction.REPORT)
    source = models.TextField(choices=WatchlistSource.choices, default=WatchlistSource.MANUAL)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", default=timezone.now)

    class Meta:
        db_table = "Watchlist"
        constraints = [
            models.UniqueConstraint(fields=["type", "value", "organization_id"], name="Watchlist_type_value_organizationId_key"),
        ]
        indexes = [
            models.Index(fields=["is_global", "action", "organization_id", "type", "value"]),
        ]


class WatchlistAudit(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid6.uuid7)
    type = models.TextField(choices=WatchlistType.choices)
    value = models.TextField()
    description = models.TextField(null=True)
    action = models.TextField(choices=WatchlistAction.choices, default=WatchlistAction.REPORT)
    changed_at = UtcDateTimeField(db_column="changedAt", default=timezone.now)
    changed_by_user_id = models.IntegerField(null=True, db_column="changedByUserId")
    watchlist = models.ForeignKey("Watchlist", on_delete=models.SET_NULL, related_name="audits", db_column="watchlistId", null=True, db_index=False)

    class Meta:
        db_table = "WatchlistAudit"
        indexes = [
            models.Index(fields=["watchlist", "changed_at"]),
        ]


class WatchlistEventAudit(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid6.uuid7)
    watchlist_id = models.UUIDField(db_column="watchlistId")
    event_type_id = models.IntegerField(db_column="eventTypeId")
    action_taken = models.TextField(choices=WatchlistAction.choices, db_column="actionTaken")
    timestamp = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "WatchlistEventAudit"


class BookingReport(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    booking = models.OneToOneField("Booking", on_delete=models.CASCADE, related_name="report", to_field="uid", db_column="bookingUid")
    booker_email = models.TextField(db_column="bookerEmail")
    reported_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="reported_bookings", db_column="reportedById", null=True, db_index=False)
    organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="booking_reports", db_column="organizationId", null=True, db_index=False)
    reason = models.TextField(choices=BookingReportReason.choices)
    description = models.TextField(null=True)
    cancelled = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    status = models.TextField(choices=BookingReportStatus.choices, default=BookingReportStatus.PENDING)
    system_status = models.TextField(choices=SystemReportStatus.choices, db_column="systemStatus", default=SystemReportStatus.PENDING)
    watchlist = models.ForeignKey("Watchlist", on_delete=models.SET_NULL, related_name="booking_reports", db_column="watchlistId", null=True, db_index=False)
    global_watchlist = models.ForeignKey("Watchlist", on_delete=models.SET_NULL, related_name="global_booking_reports", db_column="globalWatchlistId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BookingReport"
        indexes = [
            models.Index(fields=["booker_email"]),
            models.Index(fields=["reported_by"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["watchlist"]),
            models.Index(fields=["global_watchlist"]),
            models.Index(fields=["system_status"]),
            models.Index(fields=["created_at"]),
        ]


class WrongAssignmentReport(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="wrong_assignment_reports", to_field="uid", db_column="bookingUid", db_index=False)
    reported_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="wrong_assignment_reports", db_column="reportedById", null=True, db_index=False)
    correct_assignee = models.TextField(null=True, db_column="correctAssignee")
    additional_notes = models.TextField(db_column="additionalNotes")
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="wrong_assignment_reports", db_column="teamId", null=True, db_index=False)
    status = models.TextField(choices=WrongAssignmentReportStatus.choices, default=WrongAssignmentReportStatus.PENDING)
    reviewed_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="reviewed_wrong_assignment_reports", db_column="reviewedById", null=True, db_index=False)
    reviewed_at = UtcDateTimeField(null=True, db_column="reviewedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WrongAssignmentReport"
        indexes = [
            models.Index(fields=["reported_by"]),
            models.Index(fields=["team"]),
            models.Index(fields=["status"]),
            models.Index(fields=["reviewed_by"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["team", "status", "created_at"]),
        ]


class OrganizationOnboarding(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_by = models.ForeignKey("User", on_delete=models.CASCADE, related_name="created_organization_onboardings", db_column="createdById", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    org_owner_email = models.TextField(unique=True, db_column="orgOwnerEmail")
    error = models.TextField(null=True)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="organization_onboarding", db_column="organizationId", null=True)
    billing_period = models.TextField(choices=BillingPeriod.choices, db_column="billingPeriod")
    price_per_seat = models.FloatField(db_column="pricePerSeat")
    seats = models.IntegerField()
    is_platform = models.BooleanField(db_column="isPlatform", default=False)
    name = models.TextField()
    slug = models.TextField()
    logo = models.TextField(null=True)
    bio = models.TextField(null=True)
    brand_color = models.TextField(null=True, db_column="brandColor")
    banner_url = models.TextField(null=True, db_column="bannerUrl")
    is_domain_configured = models.BooleanField(db_column="isDomainConfigured", default=False)
    stripe_customer_id = models.TextField(null=True, unique=True, db_column="stripeCustomerId")
    stripe_subscription_id = models.TextField(null=True, db_column="stripeSubscriptionId")
    stripe_subscription_item_id = models.TextField(null=True, db_column="stripeSubscriptionItemId")
    invited_members = models.JSONField(db_column="invitedMembers", default=list)
    teams = models.JSONField(default=list)
    is_complete = models.BooleanField(db_column="isComplete", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationOnboarding"
        indexes = [
            models.Index(fields=["org_owner_email"]),
            models.Index(fields=["stripe_customer_id"]),
        ]


class InternalNotePreset(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    cancellation_reason = models.TextField(null=True, db_column="cancellationReason")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="internal_note_presets", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "InternalNotePreset"
        constraints = [
            models.UniqueConstraint(fields=["team", "name"], name="InternalNotePreset_teamId_name_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class FilterSegment(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    table_identifier = models.TextField(db_column="tableIdentifier")
    scope = models.TextField(choices=FilterSegmentScope.choices)
    active_filters = models.JSONField(null=True, db_column="activeFilters")
    sorting = models.JSONField(null=True)
    column_visibility = models.JSONField(null=True, db_column="columnVisibility")
    column_sizing = models.JSONField(null=True, db_column="columnSizing")
    per_page = models.IntegerField(db_column="perPage")
    search_term = models.TextField(null=True, db_column="searchTerm")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="filter_segments", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="filter_segments", db_column="teamId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FilterSegment"
        indexes = [
            models.Index(fields=["scope", "user", "table_identifier"]),
            models.Index(fields=["scope", "team", "table_identifier"]),
        ]


class UserFilterSegmentPreference(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="filter_segment_preferences", db_column="userId", db_index=False)
    table_identifier = models.TextField(db_column="tableIdentifier")
    segment = models.ForeignKey("FilterSegment", on_delete=models.CASCADE, related_name="user_preferences", db_column="segmentId", null=True, db_index=False)
    system_segment_id = models.TextField(null=True, db_column="systemSegmentId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserFilterSegmentPreference"
        constraints = [
            models.UniqueConstraint(fields=["user", "table_identifier"], name="UserFilterSegmentPreference_userId_tableIdentifier_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["segment"]),
        ]


class BookingInternalNote(models.Model):
    id = models.AutoField(primary_key=True)
    note_preset = models.ForeignKey("InternalNotePreset", on_delete=models.CASCADE, related_name="booking_internal_note", db_column="notePresetId", null=True, db_index=False)
    text = models.TextField(null=True)
    booking = models.ForeignKey("Booking", on_delete=models.CASCADE, related_name="internal_note", db_column="bookingId", db_index=False)
    created_by = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="booking_internal_note", db_column="createdById", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "BookingInternalNote"
        constraints = [
            models.UniqueConstraint(fields=["booking", "note_preset"], name="BookingInternalNote_bookingId_notePresetId_key"),
        ]
        indexes = [
            models.Index(fields=["booking"]),
        ]


class Role(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    color = models.TextField(null=True)
    description = models.TextField(null=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="roles", db_column="teamId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(choices=RoleType.choices, default=RoleType.CUSTOM)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Role"
        constraints = [
            models.UniqueConstraint(fields=["name", "team"], name="Role_name_teamId_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class RolePermission(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    role = models.ForeignKey("Role", on_delete=models.CASCADE, related_name="permissions", db_column="roleId", db_index=False)
    resource = models.TextField()
    action = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "RolePermission"
        constraints = [
            models.UniqueConstraint(fields=["role", "resource", "action"], name="RolePermission_roleId_resource_action_key"),
        ]
        indexes = [
            models.Index(fields=["role"]),
            models.Index(fields=["action"]),
        ]


class AuditActor(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid6.uuid7)
    type = models.TextField(choices=AuditActorType.choices)
    user_uuid = models.UUIDField(null=True, unique=True, db_column="userUuid")
    attendee_id = models.IntegerField(null=True, unique=True, db_column="attendeeId")
    credential_id = models.IntegerField(null=True, unique=True, db_column="credentialId")
    email = models.TextField(null=True, unique=True)
    phone = models.TextField(null=True, unique=True)
    name = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AuditActor"
        constraints = [
            models.UniqueConstraint(fields=["user_uuid"], name="AuditActor_userUuid_key"),
            models.UniqueConstraint(fields=["attendee_id"], name="AuditActor_attendeeId_key"),
            models.UniqueConstraint(fields=["credential_id"], name="AuditActor_credentialId_key"),
            models.UniqueConstraint(fields=["email"], name="AuditActor_email_key"),
            models.UniqueConstraint(fields=["phone"], name="AuditActor_phone_key"),
        ]
        indexes = [
            models.Index(fields=["email"]),
            models.Index(fields=["user_uuid"]),
            models.Index(fields=["attendee_id"]),
            models.Index(fields=["credential_id"]),
        ]


class BookingAudit(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid6.uuid7)
    booking_uid = models.TextField(db_column="bookingUid")
    actor = models.ForeignKey("AuditActor", on_delete=models.RESTRICT, related_name="booking_audits", db_column="actorId", db_index=False)
    type = models.TextField(choices=BookingAuditType.choices)
    action = models.TextField(choices=BookingAuditAction.choices)
    timestamp = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    source = models.TextField(choices=BookingAuditSource.choices)
    operation_id = models.TextField(db_column="operationId")
    data = models.JSONField(null=True)
    context = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BookingAudit"
        indexes = [
            models.Index(fields=["actor"]),
            models.Index(fields=["booking_uid"]),
            models.Index(fields=["timestamp"]),
            models.Index(fields=["operation_id"]),
        ]


class Agent(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="agents", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="agents", db_column="teamId", null=True, db_index=False)
    provider_agent_id = models.TextField(unique=True, db_column="providerAgentId")
    inbound_event_type_id = models.IntegerField(null=True, db_column="inboundEventTypeId")
    outbound_event_type_id = models.IntegerField(null=True, db_column="outboundEventTypeId")
    enabled = models.BooleanField(default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Agent"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["inbound_event_type_id"]),
            models.Index(fields=["outbound_event_type_id"]),
        ]


class CalAiPhoneNumber(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="cal_ai_phone_numbers", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="cal_ai_phone_numbers", db_column="teamId", null=True, db_index=False)
    phone_number = models.TextField(unique=True, db_column="phoneNumber")
    provider = models.TextField()
    provider_phone_number_id = models.TextField(null=True, unique=True, db_column="providerPhoneNumberId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    stripe_customer_id = models.TextField(null=True, db_column="stripeCustomerId")
    stripe_subscription_id = models.TextField(null=True, unique=True, db_column="stripeSubscriptionId")
    subscription_status = models.TextField(choices=PhoneNumberSubscriptionStatus.choices, null=True, db_column="subscriptionStatus")
    inbound_agent = models.ForeignKey("Agent", on_delete=models.SET_NULL, related_name="inbound_phone_numbers", db_column="inboundAgentId", null=True, db_index=False)
    outbound_agent = models.ForeignKey("Agent", on_delete=models.SET_NULL, related_name="outbound_phone_numbers", db_column="outboundAgentId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CalAiPhoneNumber"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["inbound_agent"]),
            models.Index(fields=["outbound_agent"]),
        ]


class TeamBilling(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="team_billing", db_column="teamId")
    subscription_id = models.TextField(unique=True, db_column="subscriptionId")
    subscription_item_id = models.TextField(db_column="subscriptionItemId")
    customer_id = models.TextField(db_column="customerId")
    status = models.TextField()
    plan_name = models.TextField(db_column="planName")
    subscription_start = UtcDateTimeField(null=True, db_column="subscriptionStart")
    subscription_trial_end = UtcDateTimeField(null=True, db_column="subscriptionTrialEnd")
    subscription_end = UtcDateTimeField(null=True, db_column="subscriptionEnd")
    billing_period = models.TextField(choices=BillingPeriod.choices, null=True, db_column="billingPeriod")
    billing_mode = models.TextField(choices=BillingMode.choices, db_column="billingMode", default=BillingMode.SEATS)
    price_per_seat = models.IntegerField(null=True, db_column="pricePerSeat")
    paid_seats = models.IntegerField(null=True, db_column="paidSeats")
    high_water_mark = models.IntegerField(null=True, db_column="highWaterMark")
    high_water_mark_period_start = UtcDateTimeField(null=True, db_column="highWaterMarkPeriodStart")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamBilling"


class OrganizationBilling(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.OneToOneField("Team", on_delete=models.CASCADE, related_name="organization_billing", db_column="teamId")
    subscription_id = models.TextField(unique=True, db_column="subscriptionId")
    subscription_item_id = models.TextField(db_column="subscriptionItemId")
    customer_id = models.TextField(db_column="customerId")
    status = models.TextField()
    plan_name = models.TextField(db_column="planName")
    subscription_start = UtcDateTimeField(null=True, db_column="subscriptionStart")
    subscription_trial_end = UtcDateTimeField(null=True, db_column="subscriptionTrialEnd")
    subscription_end = UtcDateTimeField(null=True, db_column="subscriptionEnd")
    billing_period = models.TextField(choices=BillingPeriod.choices, null=True, db_column="billingPeriod")
    billing_mode = models.TextField(choices=BillingMode.choices, db_column="billingMode", default=BillingMode.SEATS)
    price_per_seat = models.IntegerField(null=True, db_column="pricePerSeat")
    paid_seats = models.IntegerField(null=True, db_column="paidSeats")
    high_water_mark = models.IntegerField(null=True, db_column="highWaterMark")
    high_water_mark_period_start = UtcDateTimeField(null=True, db_column="highWaterMarkPeriodStart")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationBilling"


class SeatChangeLog(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="seat_change_logs", db_column="teamId", db_index=False)
    change_type = models.TextField(choices=SeatChangeType.choices, db_column="changeType")
    seat_count = models.IntegerField(db_column="seatCount")
    user_id = models.IntegerField(null=True, db_column="userId")
    triggered_by = models.IntegerField(null=True, db_column="triggeredBy")
    change_date = UtcDateTimeField(db_column="changeDate", default=timezone.now)
    month_key = models.TextField(db_column="monthKey")
    operation_id = models.TextField(null=True, db_column="operationId")
    proration = models.ForeignKey("MonthlyProration", on_delete=models.SET_NULL, related_name="seat_changes", db_column="processedInProrationId", null=True, db_index=False)
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    team_billing = models.ForeignKey("TeamBilling", on_delete=models.SET_NULL, related_name="seat_change_logs", db_column="teamBillingId", null=True, db_index=False)
    organization_billing = models.ForeignKey("OrganizationBilling", on_delete=models.SET_NULL, related_name="seat_change_logs", db_column="organizationBillingId", null=True, db_index=False)

    class Meta:
        db_table = "SeatChangeLog"
        constraints = [
            models.UniqueConstraint(fields=["team", "operation_id"], name="SeatChangeLog_teamId_operationId_key"),
        ]
        indexes = [
            models.Index(fields=["team", "month_key"]),
            models.Index(fields=["team", "proration"]),
            models.Index(fields=["month_key"]),
        ]


class MonthlyProration(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="monthly_prorations", db_column="teamId", db_index=False)
    month_key = models.TextField(db_column="monthKey")
    period_start = UtcDateTimeField(db_column="periodStart")
    period_end = UtcDateTimeField(db_column="periodEnd")
    seats_at_start = models.IntegerField(db_column="seatsAtStart")
    seats_added = models.IntegerField(db_column="seatsAdded")
    seats_removed = models.IntegerField(db_column="seatsRemoved")
    net_seat_increase = models.IntegerField(db_column="netSeatIncrease")
    seats_at_end = models.IntegerField(db_column="seatsAtEnd")
    subscription_id = models.TextField(db_column="subscriptionId")
    subscription_item_id = models.TextField(db_column="subscriptionItemId")
    customer_id = models.TextField(db_column="customerId")
    subscription_start = UtcDateTimeField(db_column="subscriptionStart")
    subscription_end = UtcDateTimeField(db_column="subscriptionEnd")
    remaining_days = models.IntegerField(db_column="remainingDays")
    price_per_seat = models.IntegerField(db_column="pricePerSeat")
    prorated_amount = models.IntegerField(db_column="proratedAmount")
    invoice_item_id = models.TextField(null=True, db_column="invoiceItemId")
    invoice_id = models.TextField(null=True, db_column="invoiceId")
    invoice_url = models.TextField(null=True, db_column="invoiceUrl")
    status = models.TextField(choices=ProrationStatus.choices, default=ProrationStatus.PENDING)
    charged_at = UtcDateTimeField(null=True, db_column="chargedAt")
    failed_at = UtcDateTimeField(null=True, db_column="failedAt")
    failure_reason = models.TextField(null=True, db_column="failureReason")
    retry_count = models.IntegerField(db_column="retryCount", default=0)
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    team_billing = models.ForeignKey("TeamBilling", on_delete=models.SET_NULL, related_name="monthly_prorations", db_column="teamBillingId", null=True, db_index=False)
    organization_billing = models.ForeignKey("OrganizationBilling", on_delete=models.SET_NULL, related_name="monthly_prorations", db_column="organizationBillingId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MonthlyProration"
        constraints = [
            models.UniqueConstraint(fields=["team", "month_key"], name="MonthlyProration_teamId_monthKey_key"),
        ]
        indexes = [
            models.Index(fields=["month_key", "status"]),
            models.Index(fields=["status"]),
            models.Index(fields=["team"]),
        ]


class CalendarCacheEvent(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    selected_calendar = models.ForeignKey("SelectedCalendar", on_delete=models.CASCADE, related_name="calendar_cache_events", db_column="selectedCalendarId", db_index=False)
    external_id = models.TextField(db_column="externalId")
    external_etag = models.TextField(db_column="externalEtag")
    i_cal_uid = models.TextField(null=True, db_column="iCalUID")
    i_cal_sequence = models.IntegerField(db_column="iCalSequence", default=0)
    summary = models.TextField(null=True)
    description = models.TextField(null=True)
    location = models.TextField(null=True)
    start = UtcDateTimeField()
    end = UtcDateTimeField()
    is_all_day = models.BooleanField(db_column="isAllDay", default=False)
    time_zone = models.TextField(null=True, db_column="timeZone")
    status = models.TextField(choices=CalendarCacheEventStatus.choices, default=CalendarCacheEventStatus.confirmed)
    recurring_event_id = models.TextField(null=True, db_column="recurringEventId")
    original_start_time = UtcDateTimeField(null=True, db_column="originalStartTime")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    external_created_at = UtcDateTimeField(null=True, db_column="externalCreatedAt")
    external_updated_at = UtcDateTimeField(null=True, db_column="externalUpdatedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CalendarCacheEvent"
        constraints = [
            models.UniqueConstraint(fields=["selected_calendar", "external_id"], name="CalendarCacheEvent_selectedCalendarId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["start", "end", "status"]),
            models.Index(fields=["selected_calendar", "i_cal_uid"]),
        ]


class IntegrationAttributeSync(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="integration_attribute_syncs", db_column="organizationId", db_index=False)
    name = models.TextField()
    integration = models.TextField()
    credential = models.ForeignKey("Credential", on_delete=models.CASCADE, related_name="integration_attribute_syncs", db_column="credentialId", null=True, db_index=False)
    enabled = models.BooleanField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IntegrationAttributeSync"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["credential"]),
        ]


class AttributeSyncRule(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    integration_attribute_sync = models.OneToOneField("IntegrationAttributeSync", on_delete=models.CASCADE, related_name="attribute_sync_rule", db_column="integrationAttributeSyncId")
    rule = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttributeSyncRule"


class AttributeSyncFieldMapping(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    integration_field_name = models.TextField(db_column="integrationFieldName")
    attribute = models.ForeignKey("Attribute", on_delete=models.CASCADE, related_name="attribute_sync_field_mappings", db_column="attributeId", db_index=False)
    enabled = models.BooleanField()
    integration_attribute_sync = models.ForeignKey("IntegrationAttributeSync", on_delete=models.CASCADE, related_name="sync_field_mappings", db_column="integrationAttributeSyncId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttributeSyncFieldMapping"
        constraints = [
            models.UniqueConstraint(fields=["integration_attribute_sync", "attribute"], name="AttributeSyncFieldMapping_integrationAttributeSyncId_attributeId_key"),
        ]
        indexes = [
            models.Index(fields=["integration_attribute_sync"]),
        ]


class BookingTimeStatus(models.Model):
    id = models.IntegerField(primary_key=True)
    uid = models.TextField(null=True)
    event_type_id = models.IntegerField(null=True, db_column="eventTypeId")
    title = models.TextField(null=True)
    description = models.TextField(null=True)
    start_time = UtcDateTimeField(null=True, db_column="startTime")
    end_time = UtcDateTimeField(null=True, db_column="endTime")
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    location = models.TextField(null=True)
    paid = models.BooleanField(null=True)
    status = models.TextField(choices=BookingStatus.choices, null=True)
    rescheduled = models.BooleanField(null=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    team_id = models.IntegerField(null=True, db_column="teamId")
    event_length = models.IntegerField(null=True, db_column="eventLength")
    time_status = models.TextField(null=True, db_column="timeStatus")
    event_parent_id = models.IntegerField(null=True, db_column="eventParentId")
    user_email = models.TextField(null=True, db_column="userEmail")
    username = models.TextField(null=True)
    rating_feedback = models.TextField(null=True, db_column="ratingFeedback")
    rating = models.IntegerField(null=True)
    no_show_host = models.BooleanField(null=True, db_column="noShowHost")
    is_team_booking = models.BooleanField(db_column="isTeamBooking")

    class Meta:
        db_table = "BookingTimeStatus"


class BookingTimeStatusDenormalized(models.Model):
    id = models.IntegerField(primary_key=True)
    uid = models.TextField()
    event_type_id = models.IntegerField(null=True, db_column="eventTypeId")
    title = models.TextField()
    description = models.TextField(null=True)
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    location = models.TextField(null=True)
    paid = models.BooleanField()
    status = models.TextField(choices=BookingStatus.choices)
    rescheduled = models.BooleanField(null=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    team_id = models.IntegerField(null=True, db_column="teamId")
    event_length = models.IntegerField(null=True, db_column="eventLength")
    event_parent_id = models.IntegerField(null=True, db_column="eventParentId")
    user_email = models.TextField(null=True, db_column="userEmail")
    user_name = models.TextField(null=True, db_column="userName")
    user_username = models.TextField(null=True, db_column="userUsername")
    rating_feedback = models.TextField(null=True, db_column="ratingFeedback")
    rating = models.IntegerField(null=True)
    no_show_host = models.BooleanField(null=True, db_column="noShowHost")
    is_team_booking = models.BooleanField(db_column="isTeamBooking")
    time_status = models.TextField(null=True, db_column="timeStatus")

    class Meta:
        db_table = "BookingTimeStatusDenormalized"


class UserEventtype(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("EventType", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("User", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_user_eventtype"
        indexes = [
            models.Index(fields=["b"]),
        ]


class PlatformOAuthClientToUser(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("PlatformOAuthClient", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("User", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_PlatformOAuthClientToUser"
        indexes = [
            models.Index(fields=["b"]),
        ]
