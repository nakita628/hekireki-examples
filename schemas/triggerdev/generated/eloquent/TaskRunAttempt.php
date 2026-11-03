<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TaskRunAttempt extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunAttempt';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'number',
        'friendlyId',
        'taskRunId',
        'backgroundWorkerId',
        'backgroundWorkerTaskId',
        'runtimeEnvironmentId',
        'queueId',
        'status',
        'startedAt',
        'completedAt',
        'usageDurationMs',
        'error',
        'output',
        'outputType',
    ];

    protected $attributes = [
        'number' => 0,
        'status' => 'PENDING',
        'usageDurationMs' => 0,
        'outputType' => 'application/json',
    ];

    protected $casts = [
        'number' => 'integer',
        'status' => TaskRunAttemptStatus::class,
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
        'usageDurationMs' => 'integer',
        'error' => 'array',
    ];

    public function taskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'taskRunId');
    }

    public function backgroundWorker(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorker::class, 'backgroundWorkerId');
    }

    public function backgroundWorkerTask(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorkerTask::class, 'backgroundWorkerTaskId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function queue(): BelongsTo
    {
        return $this->belongsTo(TaskQueue::class, 'queueId');
    }

    public function dependencies(): HasMany
    {
        return $this->hasMany(TaskRunDependency::class, 'dependentAttemptId');
    }

    public function batchDependencies(): HasMany
    {
        return $this->hasMany(BatchTaskRun::class, 'dependentTaskAttemptId');
    }

    public function checkpoints(): HasMany
    {
        return $this->hasMany(Checkpoint::class, 'attemptId');
    }

    public function batchTaskRunItems(): HasMany
    {
        return $this->hasMany(BatchTaskRunItem::class, 'taskRunAttemptId');
    }

    public function CheckpointRestoreEvent(): HasMany
    {
        return $this->hasMany(CheckpointRestoreEvent::class, 'attemptId');
    }

    public function childRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'parentTaskRunAttemptId');
    }
}