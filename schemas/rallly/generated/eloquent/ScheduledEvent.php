<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ScheduledEvent extends Model
{
    use PrismaDates;

    protected $table = 'scheduled_events';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'space_id',
        'event_type_id',
        'sheet_slot_id',
        'title',
        'description',
        'location',
        'conferencing',
        'capacity',
        'status',
        'time_zone',
        'start',
        'end',
        'all_day',
        'deleted_at',
        'sequence',
        'uid',
        'hide_attendees',
    ];

    protected $attributes = [
        'status' => 'confirmed',
        'all_day' => false,
        'sequence' => 0,
        'hide_attendees' => false,
    ];

    protected $casts = [
        'location' => 'array',
        'conferencing' => 'array',
        'capacity' => 'integer',
        'status' => ScheduledEventStatus::class,
        'start' => 'datetime',
        'end' => 'datetime',
        'all_day' => 'boolean',
        'deleted_at' => 'datetime',
        'sequence' => 'integer',
        'hide_attendees' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'event_type_id');
    }

    public function sheetSlot(): BelongsTo
    {
        return $this->belongsTo(SheetSlot::class, 'sheet_slot_id');
    }

    public function rescheduledDates(): HasMany
    {
        return $this->hasMany(RescheduledEventDate::class, 'scheduled_event_id');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(ScheduledEventInvite::class, 'scheduled_event_id');
    }

    public function polls(): HasMany
    {
        return $this->hasMany(Poll::class, 'scheduled_event_id');
    }
}