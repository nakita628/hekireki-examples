#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class EventType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? InterfaceLanguage { get; set; }

    public int Position { get; set; } = 0;

    public string? Locations { get; set; }

    public int Length { get; set; }

    public int OffsetStart { get; set; } = 0;

    public bool Hidden { get; set; } = false;

    public virtual ICollection<Host> Hosts { get; set; } = new List<Host>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual User? Owner { get; set; }

    public int? UserId { get; set; }

    public int? ProfileId { get; set; }

    public virtual Profile? Profile { get; set; }

    public virtual Team? Team { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<HashedLink> HashedLink { get; set; } = new List<HashedLink>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Availability> Availability { get; set; } = new List<Availability>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual DestinationCalendar? DestinationCalendar { get; set; }

    public bool UseEventLevelSelectedCalendars { get; set; } = false;

    public string? EventName { get; set; }

    public virtual ICollection<EventTypeCustomInput> CustomInputs { get; set; } = new List<EventTypeCustomInput>();

    public int? ParentId { get; set; }

    public virtual EventType? Parent { get; set; }

    public virtual ICollection<EventType> Children { get; set; } = new List<EventType>();

    public string? BookingFields { get; set; }

    public string? TimeZone { get; set; }

    public PeriodType PeriodType { get; set; } = PeriodType.Unlimited;

    public DateTime? PeriodStartDate { get; set; }

    public DateTime? PeriodEndDate { get; set; }

    public int? PeriodDays { get; set; }

    public bool? PeriodCountCalendarDays { get; set; }

    public bool LockTimeZoneToggleOnBookingPage { get; set; } = false;

    public string? LockedTimeZone { get; set; }

    public bool RequiresConfirmation { get; set; } = false;

    public bool RequiresConfirmationWillBlockSlot { get; set; } = false;

    public bool RequiresConfirmationForFreeEmail { get; set; } = false;

    public bool RequiresBookerEmailVerification { get; set; } = false;

    public bool CanSendCalVideoTranscriptionEmails { get; set; } = true;

    public bool AutoTranslateDescriptionEnabled { get; set; } = false;

    public bool AutoTranslateInstantMeetingTitleEnabled { get; set; } = true;

    public string? RecurringEvent { get; set; }

    public bool DisableGuests { get; set; } = false;

    public bool HideCalendarNotes { get; set; } = false;

    public bool HideCalendarEventDetails { get; set; } = false;

    public int MinimumBookingNotice { get; set; } = 120;

    public int BeforeEventBuffer { get; set; } = 0;

    public int AfterEventBuffer { get; set; } = 0;

    public int? SeatsPerTimeSlot { get; set; }

    public bool OnlyShowFirstAvailableSlot { get; set; } = false;

    public bool? ShowOptimizedSlots { get; set; } = false;

    public bool? DisableCancelling { get; set; } = false;

    public bool? DisableRescheduling { get; set; } = false;

    public int? MinimumRescheduleNotice { get; set; }

    public bool? SeatsShowAttendees { get; set; } = false;

    public bool? SeatsShowAvailabilityCount { get; set; } = true;

    public SchedulingType? SchedulingType { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public int? ScheduleId { get; set; }

    public bool? AllowReschedulingCancelledBookings { get; set; } = false;

    public int Price { get; set; } = 0;

    public string Currency { get; set; } = "usd";

    public int? SlotInterval { get; set; }

    public string? Metadata { get; set; }

    public string? SuccessRedirectUrl { get; set; }

    public bool? ForwardParamsSuccessRedirect { get; set; } = true;

    public string? BookingLimits { get; set; }

    public string? DurationLimits { get; set; }

    public bool IsInstantEvent { get; set; } = false;

    public int InstantMeetingExpiryTimeOffsetInSeconds { get; set; } = 90;

    public int? InstantMeetingScheduleId { get; set; }

    public virtual Schedule? InstantMeetingSchedule { get; set; }

    public List<string>? InstantMeetingParameters { get; set; }

    public bool AssignAllTeamMembers { get; set; } = false;

    public bool AssignRRMembersUsingSegment { get; set; } = false;

    public string? RrSegmentQueryValue { get; set; }

    public bool UseEventTypeDestinationCalendarEmail { get; set; } = false;

    public bool IsRRWeightsEnabled { get; set; } = false;

    public virtual ICollection<EventTypeTranslation> FieldTranslations { get; set; } = new List<EventTypeTranslation>();

    public int? MaxLeadThreshold { get; set; }

    public bool IncludeNoShowInRRCalculation { get; set; } = false;

    public virtual ICollection<SelectedCalendar> SelectedCalendars { get; set; } = new List<SelectedCalendar>();

    public bool AllowReschedulingPastBookings { get; set; } = false;

    public bool HideOrganizerEmail { get; set; } = false;

    public int? MaxActiveBookingsPerBooker { get; set; }

    public bool MaxActiveBookingPerBookerOfferReschedule { get; set; } = false;

    public string? CustomReplyToEmail { get; set; }

    public virtual CalVideoSettings? CalVideoSettings { get; set; }

    public string? EventTypeColor { get; set; }

    public bool RescheduleWithSameRoundRobinHost { get; set; } = false;

    public int? SecondaryEmailId { get; set; }

    public virtual SecondaryEmail? SecondaryEmail { get; set; }

    public bool UseBookerTimezone { get; set; } = false;

    public int? RestrictionScheduleId { get; set; }

    public virtual Schedule? RestrictionSchedule { get; set; }

    public virtual ICollection<HostGroup> HostGroups { get; set; } = new List<HostGroup>();

    public bool BookingRequiresAuthentication { get; set; } = false;

    public bool RrHostSubsetEnabled { get; set; } = false;

    public CancellationReasonRequirement? RequiresCancellationReason { get; set; } = CancellationReasonRequirement.MandatoryHostOnly;

    public bool EnablePerHostLocations { get; set; } = false;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
