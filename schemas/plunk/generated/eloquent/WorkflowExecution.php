<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkflowExecution extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'workflow_executions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'workflowId',
        'contactId',
        'status',
        'currentStepId',
        'exitReason',
        'context',
        'startedAt',
        'completedAt',
    ];

    protected $attributes = [
        'status' => 'RUNNING',
    ];

    protected $casts = [
        'status' => WorkflowExecutionStatus::class,
        'context' => 'array',
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('startedAt', $this->attributes)) {
                $this->setAttribute('startedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function workflow(): BelongsTo
    {
        return $this->belongsTo(Workflow::class, 'workflowId');
    }

    public function contact(): BelongsTo
    {
        return $this->belongsTo(Contact::class, 'contactId');
    }

    public function currentStep(): BelongsTo
    {
        return $this->belongsTo(WorkflowStep::class, 'currentStepId');
    }

    public function stepExecutions(): HasMany
    {
        return $this->hasMany(WorkflowStepExecution::class, 'executionId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(Email::class, 'workflowExecutionId');
    }
}