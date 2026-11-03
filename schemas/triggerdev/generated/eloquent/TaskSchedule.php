<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TaskSchedule extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskSchedule';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'friendlyId',
        'taskIdentifier',
        'deduplicationKey',
        'userProvidedDeduplicationKey',
        'generatorExpression',
        'generatorDescription',
        'generatorType',
        'timezone',
        'windowDurationSeconds',
        'windowPercentage',
        'defaultWindowDurationSeconds',
        'minimumWindowDurationSeconds',
        'externalId',
        'lastRunTriggeredAt',
        'projectId',
        'active',
    ];

    protected $attributes = [
        'type' => 'IMPERATIVE',
        'userProvidedDeduplicationKey' => false,
        'generatorDescription' => '',
        'generatorType' => 'CRON',
        'timezone' => 'UTC',
        'active' => true,
    ];

    protected $casts = [
        'type' => ScheduleType::class,
        'userProvidedDeduplicationKey' => 'boolean',
        'generatorType' => ScheduleGeneratorType::class,
        'windowDurationSeconds' => 'integer',
        'windowPercentage' => 'integer',
        'defaultWindowDurationSeconds' => 'integer',
        'minimumWindowDurationSeconds' => 'integer',
        'lastRunTriggeredAt' => 'datetime',
        'active' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function instances(): HasMany
    {
        return $this->hasMany(TaskScheduleInstance::class, 'taskScheduleId');
    }
}