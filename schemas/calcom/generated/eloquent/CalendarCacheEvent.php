<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CalendarCacheEvent extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CalendarCacheEvent';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'selectedCalendarId',
        'externalId',
        'externalEtag',
        'iCalUID',
        'iCalSequence',
        'summary',
        'description',
        'location',
        'start',
        'end',
        'isAllDay',
        'timeZone',
        'status',
        'recurringEventId',
        'originalStartTime',
        'externalCreatedAt',
        'externalUpdatedAt',
    ];

    protected $attributes = [
        'iCalSequence' => 0,
        'isAllDay' => false,
        'status' => 'confirmed',
    ];

    protected $casts = [
        'iCalSequence' => 'integer',
        'start' => 'datetime',
        'end' => 'datetime',
        'isAllDay' => 'boolean',
        'status' => CalendarCacheEventStatus::class,
        'originalStartTime' => 'datetime',
        'externalCreatedAt' => 'datetime',
        'externalUpdatedAt' => 'datetime',
    ];

    public function selectedCalendar(): BelongsTo
    {
        return $this->belongsTo(SelectedCalendar::class, 'selectedCalendarId');
    }
}