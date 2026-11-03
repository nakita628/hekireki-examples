<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskScheduleInstance extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskScheduleInstance';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'taskScheduleId',
        'environmentId',
        'projectId',
        'schedulePhase',
        'active',
        'lastScheduledTimestamp',
        'nextScheduledTimestamp',
    ];

    protected $attributes = [
        'active' => true,
    ];

    protected $casts = [
        'schedulePhase' => 'integer',
        'active' => 'boolean',
        'lastScheduledTimestamp' => 'datetime',
        'nextScheduledTimestamp' => 'datetime',
    ];

    public function taskSchedule(): BelongsTo
    {
        return $this->belongsTo(TaskSchedule::class, 'taskScheduleId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}