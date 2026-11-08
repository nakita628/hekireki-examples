<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkflowStepExecution extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'workflow_step_executions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'executionId',
        'stepId',
        'status',
        'scheduledFor',
        'executeAfter',
        'output',
        'error',
        'startedAt',
        'completedAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => StepExecutionStatus::class,
        'scheduledFor' => 'datetime',
        'executeAfter' => 'datetime',
        'output' => 'array',
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
    ];

    public function execution(): BelongsTo
    {
        return $this->belongsTo(WorkflowExecution::class, 'executionId');
    }

    public function step(): BelongsTo
    {
        return $this->belongsTo(WorkflowStep::class, 'stepId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(Email::class, 'workflowStepExecutionId');
    }
}