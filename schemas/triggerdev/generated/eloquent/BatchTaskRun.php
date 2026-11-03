<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class BatchTaskRun extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BatchTaskRun';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'idempotencyKey',
        'idempotencyKeyExpiresAt',
        'status',
        'runtimeEnvironmentId',
        'runIds',
        'runCount',
        'payload',
        'payloadType',
        'options',
        'batchVersion',
        'sealed',
        'sealedAt',
        'expectedCount',
        'completedCount',
        'completedAt',
        'resumedAt',
        'processingJobsCount',
        'processingJobsExpectedCount',
        'oneTimeUseToken',
        'processingStartedAt',
        'processingCompletedAt',
        'successfulRunCount',
        'failedRunCount',
        'taskIdentifier',
        'checkpointEventId',
        'dependentTaskAttemptId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'runCount' => 0,
        'payloadType' => 'application/json',
        'batchVersion' => 'v1',
        'sealed' => false,
        'expectedCount' => 0,
        'completedCount' => 0,
        'processingJobsCount' => 0,
        'processingJobsExpectedCount' => 0,
    ];

    protected $casts = [
        'idempotencyKeyExpiresAt' => 'datetime',
        'status' => BatchTaskRunStatus::class,
        'runCount' => 'integer',
        'options' => 'array',
        'sealed' => 'boolean',
        'sealedAt' => 'datetime',
        'expectedCount' => 'integer',
        'completedCount' => 'integer',
        'completedAt' => 'datetime',
        'resumedAt' => 'datetime',
        'processingJobsCount' => 'integer',
        'processingJobsExpectedCount' => 'integer',
        'processingStartedAt' => 'datetime',
        'processingCompletedAt' => 'datetime',
        'successfulRunCount' => 'integer',
        'failedRunCount' => 'integer',
    ];

    public function checkpointEvent(): BelongsTo
    {
        return $this->belongsTo(CheckpointRestoreEvent::class, 'checkpointEventId');
    }

    public function dependentTaskAttempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'dependentTaskAttemptId');
    }

    public function runs(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'batchId');
    }

    public function runsBlocked(): HasMany
    {
        return $this->hasMany(TaskRunWaitpoint::class, 'batchId');
    }

    public function waitpoints(): HasMany
    {
        return $this->hasMany(Waitpoint::class, 'completedByBatchId');
    }

    public function errors(): HasMany
    {
        return $this->hasMany(BatchTaskRunError::class, 'batchTaskRunId');
    }

    public function items(): HasMany
    {
        return $this->hasMany(BatchTaskRunItem::class, 'batchTaskRunId');
    }

    public function runDependencies(): HasMany
    {
        return $this->hasMany(TaskRunDependency::class, 'dependentBatchRunId');
    }
}