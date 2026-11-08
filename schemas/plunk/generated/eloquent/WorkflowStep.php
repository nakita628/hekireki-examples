<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkflowStep extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'workflow_steps';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'type',
        'name',
        'position',
        'config',
        'workflowId',
        'templateId',
    ];

    protected $casts = [
        'type' => WorkflowStepType::class,
        'position' => 'array',
        'config' => 'array',
    ];

    public function workflow(): BelongsTo
    {
        return $this->belongsTo(Workflow::class, 'workflowId');
    }

    public function template(): BelongsTo
    {
        return $this->belongsTo(Template::class, 'templateId');
    }

    public function outgoingTransitions(): HasMany
    {
        return $this->hasMany(WorkflowTransition::class, 'fromStepId');
    }

    public function incomingTransitions(): HasMany
    {
        return $this->hasMany(WorkflowTransition::class, 'toStepId');
    }

    public function executions(): HasMany
    {
        return $this->hasMany(WorkflowStepExecution::class, 'stepId');
    }

    public function currentWorkflowExecutions(): HasMany
    {
        return $this->hasMany(WorkflowExecution::class, 'currentStepId');
    }
}