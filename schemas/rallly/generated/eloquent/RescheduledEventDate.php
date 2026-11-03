<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RescheduledEventDate extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'rescheduled_event_dates';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'scheduled_event_id',
        'start',
        'end',
        'all_day',
    ];

    protected $attributes = [
        'all_day' => false,
    ];

    protected $casts = [
        'start' => 'datetime',
        'end' => 'datetime',
        'all_day' => 'boolean',
    ];

    public function scheduledEvent(): BelongsTo
    {
        return $this->belongsTo(ScheduledEvent::class, 'scheduled_event_id');
    }
}