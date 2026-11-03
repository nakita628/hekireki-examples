<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskRunDependency extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunDependency';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'taskRunId',
        'checkpointEventId',
        'dependentAttemptId',
        'dependentBatchRunId',
        'resumedAt',
    ];

    protected $casts = [
        'resumedAt' => 'datetime',
    ];

    public function taskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'taskRunId');
    }

    public function checkpointEvent(): BelongsTo
    {
        return $this->belongsTo(CheckpointRestoreEvent::class, 'checkpointEventId');
    }

    public function dependentAttempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'dependentAttemptId');
    }

    public function dependentBatchRun(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'dependentBatchRunId');
    }
}