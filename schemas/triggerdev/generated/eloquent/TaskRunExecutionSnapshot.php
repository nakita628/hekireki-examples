<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class TaskRunExecutionSnapshot extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunExecutionSnapshot';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'engine',
        'executionStatus',
        'description',
        'isValid',
        'error',
        'previousSnapshotId',
        'runId',
        'runStatus',
        'batchId',
        'attemptNumber',
        'environmentId',
        'environmentType',
        'projectId',
        'organizationId',
        'completedWaitpointOrder',
        'checkpointId',
        'workerId',
        'runnerId',
        'lastHeartbeatAt',
        'metadata',
    ];

    protected $attributes = [
        'engine' => 'V2',
        'isValid' => true,
    ];

    protected $casts = [
        'engine' => RunEngineVersion::class,
        'executionStatus' => TaskRunExecutionStatus::class,
        'isValid' => 'boolean',
        'runStatus' => TaskRunStatus::class,
        'attemptNumber' => 'integer',
        'environmentType' => RuntimeEnvironmentType::class,
        'lastHeartbeatAt' => 'datetime',
        'metadata' => 'array',
    ];

    public function run(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'runId');
    }

    public function checkpoint(): BelongsTo
    {
        return $this->belongsTo(TaskRunCheckpoint::class, 'checkpointId');
    }

    public function completedWaitpoints(): BelongsToMany
    {
        return $this->belongsToMany(Waitpoint::class, '_completedWaitpoints', 'A', 'B');
    }
}