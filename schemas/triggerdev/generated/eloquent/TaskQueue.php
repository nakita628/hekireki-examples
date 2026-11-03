<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TaskQueue extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskQueue';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'name',
        'type',
        'version',
        'orderableName',
        'concurrencyVersion',
        'role',
        'projectId',
        'runtimeEnvironmentId',
        'concurrencyLimit',
        'concurrencyLimitOverriddenAt',
        'concurrencyLimitOverriddenBy',
        'concurrencyLimitBase',
        'concurrencyLimitOverridePercent',
        'totalConcurrencyLimit',
        'totalConcurrencyLimitOverriddenAt',
        'totalConcurrencyLimitOverriddenBy',
        'totalConcurrencyLimitBase',
        'rateLimit',
        'paused',
        'archivedAt',
    ];

    protected $attributes = [
        'type' => 'VIRTUAL',
        'version' => 'V1',
        'concurrencyVersion' => 'V1',
        'role' => 'QUEUE',
        'paused' => false,
    ];

    protected $casts = [
        'type' => TaskQueueType::class,
        'version' => TaskQueueVersion::class,
        'concurrencyVersion' => TaskQueueConcurrencyVersion::class,
        'role' => TaskQueueRole::class,
        'concurrencyLimit' => 'integer',
        'concurrencyLimitOverriddenAt' => 'datetime',
        'concurrencyLimitBase' => 'integer',
        'concurrencyLimitOverridePercent' => AsDecimal::class,
        'totalConcurrencyLimit' => 'integer',
        'totalConcurrencyLimitOverriddenAt' => 'datetime',
        'totalConcurrencyLimitBase' => 'integer',
        'rateLimit' => 'array',
        'paused' => 'boolean',
        'archivedAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function attempts(): HasMany
    {
        return $this->hasMany(TaskRunAttempt::class, 'queueId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(BackgroundWorkerTask::class, 'queueId');
    }

    public function workers(): BelongsToMany
    {
        return $this->belongsToMany(BackgroundWorker::class, '_BackgroundWorkerToTaskQueue', 'B', 'A');
    }
}