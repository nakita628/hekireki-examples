<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Schedule extends Model
{
    protected $table = 'Schedule';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'name',
        'timeZone',
    ];

    protected $casts = [
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function eventType(): HasMany
    {
        return $this->hasMany(EventType::class, 'scheduleId');
    }

    public function instantMeetingEvents(): HasMany
    {
        return $this->hasMany(EventType::class, 'instantMeetingScheduleId');
    }

    public function restrictionSchedule(): HasMany
    {
        return $this->hasMany(EventType::class, 'restrictionScheduleId');
    }

    public function availability(): HasMany
    {
        return $this->hasMany(Availability::class, 'scheduleId');
    }

    public function Host(): HasMany
    {
        return $this->hasMany(Host::class, 'scheduleId');
    }
}