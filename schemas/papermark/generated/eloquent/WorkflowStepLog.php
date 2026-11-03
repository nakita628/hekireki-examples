<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkflowStepLog extends Model
{
    use PrismaDates;

    protected $table = 'WorkflowStepLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'executionId',
        'workflowStepId',
        'conditionsMatched',
        'conditionResults',
        'actionsExecuted',
        'executedAt',
        'duration',
        'error',
    ];

    protected $casts = [
        'conditionsMatched' => 'boolean',
        'conditionResults' => 'array',
        'actionsExecuted' => 'array',
        'executedAt' => 'datetime',
        'duration' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('executedAt', $this->attributes)) {
                $this->setAttribute('executedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function execution(): BelongsTo
    {
        return $this->belongsTo(WorkflowExecution::class, 'executionId');
    }

    public function step(): BelongsTo
    {
        return $this->belongsTo(WorkflowStep::class, 'workflowStepId');
    }
}