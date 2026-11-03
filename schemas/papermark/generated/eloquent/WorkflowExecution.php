<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkflowExecution extends Model
{
    use PrismaDates;

    protected $table = 'WorkflowExecution';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'workflowId',
        'visitorEmail',
        'visitorIp',
        'status',
        'startedAt',
        'completedAt',
        'result',
        'metadata',
    ];

    protected $casts = [
        'status' => ExecutionStatus::class,
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
        'result' => 'array',
        'metadata' => 'array',
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

    public function stepLogs(): HasMany
    {
        return $this->hasMany(WorkflowStepLog::class, 'executionId');
    }
}