<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BatchTaskRunItem extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BatchTaskRunItem';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'status',
        'batchTaskRunId',
        'taskRunId',
        'taskRunAttemptId',
        'completedAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => BatchTaskRunItemStatus::class,
        'completedAt' => 'datetime',
    ];

    public function batchTaskRun(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'batchTaskRunId');
    }

    public function taskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'taskRunId');
    }

    public function taskRunAttempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'taskRunAttemptId');
    }
}