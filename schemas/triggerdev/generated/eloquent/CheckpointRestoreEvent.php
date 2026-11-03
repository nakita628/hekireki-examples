<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class CheckpointRestoreEvent extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CheckpointRestoreEvent';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'reason',
        'metadata',
        'checkpointId',
        'runId',
        'attemptId',
        'projectId',
        'runtimeEnvironmentId',
    ];

    protected $casts = [
        'type' => CheckpointRestoreEventType::class,
    ];

    public function checkpoint(): BelongsTo
    {
        return $this->belongsTo(Checkpoint::class, 'checkpointId');
    }

    public function run(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'runId');
    }

    public function attempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'attemptId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function taskRunDependency(): HasOne
    {
        return $this->hasOne(TaskRunDependency::class, 'checkpointEventId');
    }

    public function batchTaskRunDependency(): HasOne
    {
        return $this->hasOne(BatchTaskRun::class, 'checkpointEventId');
    }
}