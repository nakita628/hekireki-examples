<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class TaskRun extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRun';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'number',
        'friendlyId',
        'engine',
        'status',
        'statusReason',
        'idempotencyKey',
        'idempotencyKeyExpiresAt',
        'idempotencyKeyOptions',
        'debounce',
        'taskIdentifier',
        'isTest',
        'payload',
        'payloadType',
        'context',
        'traceContext',
        'traceId',
        'spanId',
        'runtimeEnvironmentId',
        'environmentType',
        'projectId',
        'organizationId',
        'queue',
        'lockedQueueId',
        'masterQueue',
        'region',
        'secondaryMasterQueue',
        'attemptNumber',
        'runTags',
        'taskVersion',
        'sdkVersion',
        'cliVersion',
        'startedAt',
        'executedAt',
        'completedAt',
        'machinePreset',
        'usageDurationMs',
        'costInCents',
        'baseCostInCents',
        'lockedAt',
        'lockedById',
        'lockedToVersionId',
        'priorityMs',
        'concurrencyKey',
        'gates',
        'delayUntil',
        'queuedAt',
        'ttl',
        'expiredAt',
        'maxAttempts',
        'lockedRetryConfig',
        'oneTimeUseToken',
        'taskEventStore',
        'queueTimestamp',
        'scheduleInstanceId',
        'scheduleId',
        'bulkActionGroupIds',
        'logsDeletedAt',
        'replayedFromTaskRunFriendlyId',
        'rootTaskRunId',
        'parentTaskRunId',
        'parentTaskRunAttemptId',
        'batchId',
        'resumeParentOnCompletion',
        'depth',
        'parentSpanId',
        'runChainState',
        'seedMetadata',
        'seedMetadataType',
        'metadata',
        'metadataType',
        'metadataVersion',
        'annotations',
        'isWarmStart',
        'output',
        'outputType',
        'error',
        'planType',
        'maxDurationInSeconds',
        'realtimeStreamsVersion',
        'realtimeStreams',
        'streamBasinName',
    ];

    protected $attributes = [
        'number' => 0,
        'engine' => 'V1',
        'status' => 'PENDING',
        'isTest' => false,
        'payloadType' => 'application/json',
        'masterQueue' => 'main',
        'usageDurationMs' => 0,
        'costInCents' => 0.0,
        'baseCostInCents' => 0.0,
        'priorityMs' => 0,
        'taskEventStore' => 'taskEvent',
        'resumeParentOnCompletion' => false,
        'depth' => 0,
        'seedMetadataType' => 'application/json',
        'metadataType' => 'application/json',
        'metadataVersion' => 1,
        'outputType' => 'application/json',
        'realtimeStreamsVersion' => 'v1',
    ];

    protected $casts = [
        'number' => 'integer',
        'engine' => RunEngineVersion::class,
        'status' => TaskRunStatus::class,
        'idempotencyKeyExpiresAt' => 'datetime',
        'idempotencyKeyOptions' => 'array',
        'debounce' => 'array',
        'isTest' => 'boolean',
        'context' => 'array',
        'traceContext' => 'array',
        'environmentType' => RuntimeEnvironmentType::class,
        'attemptNumber' => 'integer',
        'startedAt' => 'datetime',
        'executedAt' => 'datetime',
        'completedAt' => 'datetime',
        'usageDurationMs' => 'integer',
        'costInCents' => 'float',
        'baseCostInCents' => 'float',
        'lockedAt' => 'datetime',
        'priorityMs' => 'integer',
        'gates' => 'array',
        'delayUntil' => 'datetime',
        'queuedAt' => 'datetime',
        'expiredAt' => 'datetime',
        'maxAttempts' => 'integer',
        'lockedRetryConfig' => 'array',
        'queueTimestamp' => 'datetime',
        'logsDeletedAt' => 'datetime',
        'resumeParentOnCompletion' => 'boolean',
        'depth' => 'integer',
        'runChainState' => 'array',
        'metadataVersion' => 'integer',
        'annotations' => 'array',
        'isWarmStart' => 'boolean',
        'error' => 'array',
        'maxDurationInSeconds' => 'integer',
    ];

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function lockedBy(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorkerTask::class, 'lockedById');
    }

    public function lockedToVersion(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorker::class, 'lockedToVersionId');
    }

    public function rootTaskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'rootTaskRunId');
    }

    public function parentTaskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'parentTaskRunId');
    }

    public function parentTaskRunAttempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'parentTaskRunAttemptId');
    }

    public function batch(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'batchId');
    }

    public function associatedWaitpoint(): HasOne
    {
        return $this->hasOne(Waitpoint::class, 'completedByTaskRunId');
    }

    public function dependency(): HasOne
    {
        return $this->hasOne(TaskRunDependency::class, 'taskRunId');
    }

    public function attempts(): HasMany
    {
        return $this->hasMany(TaskRunAttempt::class, 'taskRunId');
    }

    public function checkpoints(): HasMany
    {
        return $this->hasMany(Checkpoint::class, 'runId');
    }

    public function blockedByWaitpoints(): HasMany
    {
        return $this->hasMany(TaskRunWaitpoint::class, 'taskRunId');
    }

    public function batchItems(): HasMany
    {
        return $this->hasMany(BatchTaskRunItem::class, 'taskRunId');
    }

    public function CheckpointRestoreEvent(): HasMany
    {
        return $this->hasMany(CheckpointRestoreEvent::class, 'runId');
    }

    public function executionSnapshots(): HasMany
    {
        return $this->hasMany(TaskRunExecutionSnapshot::class, 'runId');
    }

    public function sourceBulkActionItems(): HasMany
    {
        return $this->hasMany(BulkActionItem::class, 'sourceRunId');
    }

    public function destinationBulkActionItems(): HasMany
    {
        return $this->hasMany(BulkActionItem::class, 'destinationRunId');
    }

    public function descendantRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'rootTaskRunId');
    }

    public function childRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'parentTaskRunId');
    }

    public function playgroundConversations(): HasMany
    {
        return $this->hasMany(PlaygroundConversation::class, 'runId');
    }

    public function connectedWaitpoints(): BelongsToMany
    {
        return $this->belongsToMany(Waitpoint::class, '_WaitpointRunConnections', 'A', 'B');
    }
}