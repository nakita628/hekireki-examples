<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;
use Illuminate\Support\Str;

class Booking extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Booking';

    protected $fillable = [
        'uid',
        'idempotencyKey',
        'userId',
        'userPrimaryEmail',
        'eventTypeId',
        'title',
        'description',
        'customInputs',
        'responses',
        'startTime',
        'endTime',
        'location',
        'status',
        'paid',
        'destinationCalendarId',
        'cancellationReason',
        'rejectionReason',
        'reassignReason',
        'reassignById',
        'dynamicEventSlugRef',
        'dynamicGroupSlugRef',
        'rescheduled',
        'fromReschedule',
        'recurringEventId',
        'smsReminderNumber',
        'scheduledJobs',
        'metadata',
        'isRecorded',
        'iCalUID',
        'iCalSequence',
        'rating',
        'ratingFeedback',
        'noShowHost',
        'oneTimePassword',
        'cancelledBy',
        'rescheduledBy',
        'creationSource',
    ];

    protected $attributes = [
        'status' => 'accepted',
        'paid' => false,
        'isRecorded' => false,
        'iCalUID' => '',
        'iCalSequence' => 0,
        'noShowHost' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'eventTypeId' => 'integer',
        'customInputs' => 'array',
        'responses' => 'array',
        'startTime' => 'datetime',
        'endTime' => 'datetime',
        'status' => BookingStatus::class,
        'paid' => 'boolean',
        'destinationCalendarId' => 'integer',
        'reassignById' => 'integer',
        'rescheduled' => 'boolean',
        'metadata' => 'array',
        'isRecorded' => 'boolean',
        'iCalSequence' => 'integer',
        'rating' => 'integer',
        'noShowHost' => 'boolean',
        'creationSource' => CreationSource::class,
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('oneTimePassword', $this->attributes)) {
                $this->setAttribute('oneTimePassword', (string) Str::orderedUuid());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function destinationCalendar(): BelongsTo
    {
        return $this->belongsTo(DestinationCalendar::class, 'destinationCalendarId');
    }

    public function reassignBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'reassignById');
    }

    public function instantMeetingToken(): HasOne
    {
        return $this->hasOne(InstantMeetingToken::class, 'bookingId');
    }

    public function tracking(): HasOne
    {
        return $this->hasOne(Tracking::class, 'bookingId');
    }

    public function report(): HasOne
    {
        return $this->hasOne(BookingReport::class, 'bookingUid', 'uid');
    }

    public function references(): HasMany
    {
        return $this->hasMany(BookingReference::class, 'bookingId');
    }

    public function attendees(): HasMany
    {
        return $this->hasMany(Attendee::class, 'bookingId');
    }

    public function payment(): HasMany
    {
        return $this->hasMany(Payment::class, 'bookingId');
    }

    public function seatsReferences(): HasMany
    {
        return $this->hasMany(BookingSeat::class, 'bookingId');
    }

    public function scheduledTriggers(): HasMany
    {
        return $this->hasMany(WebhookScheduledTriggers::class, 'bookingId');
    }

    public function assignmentReason(): HasMany
    {
        return $this->hasMany(AssignmentReason::class, 'bookingId');
    }

    public function internalNote(): HasMany
    {
        return $this->hasMany(BookingInternalNote::class, 'bookingId');
    }

    public function expenseLogs(): HasMany
    {
        return $this->hasMany(CreditExpenseLog::class, 'bookingUid', 'uid');
    }

    public function wrongAssignmentReports(): HasMany
    {
        return $this->hasMany(WrongAssignmentReport::class, 'bookingUid', 'uid');
    }
}