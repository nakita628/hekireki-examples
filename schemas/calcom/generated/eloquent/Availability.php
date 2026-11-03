<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Availability extends Model
{
    use PrismaDates;

    protected $table = 'Availability';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'eventTypeId',
        'days',
        'startTime',
        'endTime',
        'date',
        'scheduleId',
    ];

    protected $casts = [
        'userId' => 'integer',
        'eventTypeId' => 'integer',
        'startTime' => AsPrismaTime::class,
        'endTime' => AsPrismaTime::class,
        'date' => AsPrismaDate::class,
        'scheduleId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function Schedule(): BelongsTo
    {
        return $this->belongsTo(Schedule::class, 'scheduleId');
    }
}