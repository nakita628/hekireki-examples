<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class EventType extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EventType';

    protected $fillable = [
        'title',
        'slug',
        'description',
        'interfaceLanguage',
        'position',
        'locations',
        'length',
        'offsetStart',
        'hidden',
        'userId',
        'profileId',
        'teamId',
        'useEventLevelSelectedCalendars',
        'eventName',
        'parentId',
        'bookingFields',
        'timeZone',
        'periodType',
        'periodStartDate',
        'periodEndDate',
        'periodDays',
        'periodCountCalendarDays',
        'lockTimeZoneToggleOnBookingPage',
        'lockedTimeZone',
        'requiresConfirmation',
        'requiresConfirmationWillBlockSlot',
        'requiresConfirmationForFreeEmail',
        'requiresBookerEmailVerification',
        'canSendCalVideoTranscriptionEmails',
        'autoTranslateDescriptionEnabled',
        'autoTranslateInstantMeetingTitleEnabled',
        'recurringEvent',
        'disableGuests',
        'hideCalendarNotes',
        'hideCalendarEventDetails',
        'minimumBookingNotice',
        'beforeEventBuffer',
        'afterEventBuffer',
        'seatsPerTimeSlot',
        'onlyShowFirstAvailableSlot',
        'showOptimizedSlots',
        'disableCancelling',
        'disableRescheduling',
        'minimumRescheduleNotice',
        'seatsShowAttendees',
        'seatsShowAvailabilityCount',
        'schedulingType',
        'scheduleId',
        'allowReschedulingCancelledBookings',
        'price',
        'currency',
        'slotInterval',
        'metadata',
        'successRedirectUrl',
        'forwardParamsSuccessRedirect',
        'bookingLimits',
        'durationLimits',
        'isInstantEvent',
        'instantMeetingExpiryTimeOffsetInSeconds',
        'instantMeetingScheduleId',
        'instantMeetingParameters',
        'assignAllTeamMembers',
        'assignRRMembersUsingSegment',
        'rrSegmentQueryValue',
        'useEventTypeDestinationCalendarEmail',
        'isRRWeightsEnabled',
        'maxLeadThreshold',
        'includeNoShowInRRCalculation',
        'allowReschedulingPastBookings',
        'hideOrganizerEmail',
        'maxActiveBookingsPerBooker',
        'maxActiveBookingPerBookerOfferReschedule',
        'customReplyToEmail',
        'eventTypeColor',
        'rescheduleWithSameRoundRobinHost',
        'secondaryEmailId',
        'useBookerTimezone',
        'restrictionScheduleId',
        'bookingRequiresAuthentication',
        'rrHostSubsetEnabled',
        'requiresCancellationReason',
        'enablePerHostLocations',
    ];

    protected $attributes = [
        'position' => 0,
        'offsetStart' => 0,
        'hidden' => false,
        'useEventLevelSelectedCalendars' => false,
        'periodType' => 'unlimited',
        'lockTimeZoneToggleOnBookingPage' => false,
        'requiresConfirmation' => false,
        'requiresConfirmationWillBlockSlot' => false,
        'requiresConfirmationForFreeEmail' => false,
        'requiresBookerEmailVerification' => false,
        'canSendCalVideoTranscriptionEmails' => true,
        'autoTranslateDescriptionEnabled' => false,
        'autoTranslateInstantMeetingTitleEnabled' => true,
        'disableGuests' => false,
        'hideCalendarNotes' => false,
        'hideCalendarEventDetails' => false,
        'minimumBookingNotice' => 120,
        'beforeEventBuffer' => 0,
        'afterEventBuffer' => 0,
        'onlyShowFirstAvailableSlot' => false,
        'showOptimizedSlots' => false,
        'disableCancelling' => false,
        'disableRescheduling' => false,
        'seatsShowAttendees' => false,
        'seatsShowAvailabilityCount' => true,
        'allowReschedulingCancelledBookings' => false,
        'price' => 0,
        'currency' => 'usd',
        'forwardParamsSuccessRedirect' => true,
        'isInstantEvent' => false,
        'instantMeetingExpiryTimeOffsetInSeconds' => 90,
        'assignAllTeamMembers' => false,
        'assignRRMembersUsingSegment' => false,
        'useEventTypeDestinationCalendarEmail' => false,
        'isRRWeightsEnabled' => false,
        'includeNoShowInRRCalculation' => false,
        'allowReschedulingPastBookings' => false,
        'hideOrganizerEmail' => false,
        'maxActiveBookingPerBookerOfferReschedule' => false,
        'rescheduleWithSameRoundRobinHost' => false,
        'useBookerTimezone' => false,
        'bookingRequiresAuthentication' => false,
        'rrHostSubsetEnabled' => false,
        'requiresCancellationReason' => 'MANDATORY_HOST_ONLY',
        'enablePerHostLocations' => false,
    ];

    protected $casts = [
        'position' => 'integer',
        'locations' => 'array',
        'length' => 'integer',
        'offsetStart' => 'integer',
        'hidden' => 'boolean',
        'userId' => 'integer',
        'profileId' => 'integer',
        'teamId' => 'integer',
        'useEventLevelSelectedCalendars' => 'boolean',
        'parentId' => 'integer',
        'bookingFields' => 'array',
        'periodType' => PeriodType::class,
        'periodStartDate' => 'datetime',
        'periodEndDate' => 'datetime',
        'periodDays' => 'integer',
        'periodCountCalendarDays' => 'boolean',
        'lockTimeZoneToggleOnBookingPage' => 'boolean',
        'requiresConfirmation' => 'boolean',
        'requiresConfirmationWillBlockSlot' => 'boolean',
        'requiresConfirmationForFreeEmail' => 'boolean',
        'requiresBookerEmailVerification' => 'boolean',
        'canSendCalVideoTranscriptionEmails' => 'boolean',
        'autoTranslateDescriptionEnabled' => 'boolean',
        'autoTranslateInstantMeetingTitleEnabled' => 'boolean',
        'recurringEvent' => 'array',
        'disableGuests' => 'boolean',
        'hideCalendarNotes' => 'boolean',
        'hideCalendarEventDetails' => 'boolean',
        'minimumBookingNotice' => 'integer',
        'beforeEventBuffer' => 'integer',
        'afterEventBuffer' => 'integer',
        'seatsPerTimeSlot' => 'integer',
        'onlyShowFirstAvailableSlot' => 'boolean',
        'showOptimizedSlots' => 'boolean',
        'disableCancelling' => 'boolean',
        'disableRescheduling' => 'boolean',
        'minimumRescheduleNotice' => 'integer',
        'seatsShowAttendees' => 'boolean',
        'seatsShowAvailabilityCount' => 'boolean',
        'schedulingType' => SchedulingType::class,
        'scheduleId' => 'integer',
        'allowReschedulingCancelledBookings' => 'boolean',
        'price' => 'integer',
        'slotInterval' => 'integer',
        'metadata' => 'array',
        'forwardParamsSuccessRedirect' => 'boolean',
        'bookingLimits' => 'array',
        'durationLimits' => 'array',
        'isInstantEvent' => 'boolean',
        'instantMeetingExpiryTimeOffsetInSeconds' => 'integer',
        'instantMeetingScheduleId' => 'integer',
        'assignAllTeamMembers' => 'boolean',
        'assignRRMembersUsingSegment' => 'boolean',
        'rrSegmentQueryValue' => 'array',
        'useEventTypeDestinationCalendarEmail' => 'boolean',
        'isRRWeightsEnabled' => 'boolean',
        'maxLeadThreshold' => 'integer',
        'includeNoShowInRRCalculation' => 'boolean',
        'allowReschedulingPastBookings' => 'boolean',
        'hideOrganizerEmail' => 'boolean',
        'maxActiveBookingsPerBooker' => 'integer',
        'maxActiveBookingPerBookerOfferReschedule' => 'boolean',
        'eventTypeColor' => 'array',
        'rescheduleWithSameRoundRobinHost' => 'boolean',
        'secondaryEmailId' => 'integer',
        'useBookerTimezone' => 'boolean',
        'restrictionScheduleId' => 'integer',
        'bookingRequiresAuthentication' => 'boolean',
        'rrHostSubsetEnabled' => 'boolean',
        'requiresCancellationReason' => CancellationReasonRequirement::class,
        'enablePerHostLocations' => 'boolean',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function profile(): BelongsTo
    {
        return $this->belongsTo(Profile::class, 'profileId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function parent(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'parentId');
    }

    public function schedule(): BelongsTo
    {
        return $this->belongsTo(Schedule::class, 'scheduleId');
    }

    public function instantMeetingSchedule(): BelongsTo
    {
        return $this->belongsTo(Schedule::class, 'instantMeetingScheduleId');
    }

    public function secondaryEmail(): BelongsTo
    {
        return $this->belongsTo(SecondaryEmail::class, 'secondaryEmailId');
    }

    public function restrictionSchedule(): BelongsTo
    {
        return $this->belongsTo(Schedule::class, 'restrictionScheduleId');
    }

    public function destinationCalendar(): HasOne
    {
        return $this->hasOne(DestinationCalendar::class, 'eventTypeId');
    }

    public function calVideoSettings(): HasOne
    {
        return $this->hasOne(CalVideoSettings::class, 'eventTypeId');
    }

    public function hosts(): HasMany
    {
        return $this->hasMany(Host::class, 'eventTypeId');
    }

    public function hashedLink(): HasMany
    {
        return $this->hasMany(HashedLink::class, 'eventTypeId');
    }

    public function bookings(): HasMany
    {
        return $this->hasMany(Booking::class, 'eventTypeId');
    }

    public function availability(): HasMany
    {
        return $this->hasMany(Availability::class, 'eventTypeId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'eventTypeId');
    }

    public function customInputs(): HasMany
    {
        return $this->hasMany(EventTypeCustomInput::class, 'eventTypeId');
    }

    public function children(): HasMany
    {
        return $this->hasMany(EventType::class, 'parentId');
    }

    public function fieldTranslations(): HasMany
    {
        return $this->hasMany(EventTypeTranslation::class, 'eventTypeId');
    }

    public function selectedCalendars(): HasMany
    {
        return $this->hasMany(SelectedCalendar::class, 'eventTypeId');
    }

    public function hostGroups(): HasMany
    {
        return $this->hasMany(HostGroup::class, 'eventTypeId');
    }

    public function users(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_user_eventtype', 'A', 'B');
    }
}