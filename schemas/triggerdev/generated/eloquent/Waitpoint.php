<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Waitpoint extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Waitpoint';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'type',
        'status',
        'completedAt',
        'idempotencyKey',
        'userProvidedIdempotencyKey',
        'idempotencyKeyExpiresAt',
        'inactiveIdempotencyKey',
        'completedByTaskRunId',
        'completedAfter',
        'completedByBatchId',
        'output',
        'outputType',
        'outputIsError',
        'projectId',
        'environmentId',
        'tags',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'outputType' => 'application/json',
        'outputIsError' => false,
    ];

    protected $casts = [
        'type' => WaitpointType::class,
        'status' => WaitpointStatus::class,
        'completedAt' => 'datetime',
        'userProvidedIdempotencyKey' => 'boolean',
        'idempotencyKeyExpiresAt' => 'datetime',
        'completedAfter' => 'datetime',
        'outputIsError' => 'boolean',
    ];

    public function completedByTaskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'completedByTaskRunId');
    }

    public function completedByBatch(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'completedByBatchId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function blockingTaskRuns(): HasMany
    {
        return $this->hasMany(TaskRunWaitpoint::class, 'waitpointId');
    }

    public function connectedRuns(): BelongsToMany
    {
        return $this->belongsToMany(TaskRun::class, '_WaitpointRunConnections', 'B', 'A');
    }

    public function completedExecutionSnapshots(): BelongsToMany
    {
        return $this->belongsToMany(TaskRunExecutionSnapshot::class, '_completedWaitpoints', 'B', 'A');
    }
}