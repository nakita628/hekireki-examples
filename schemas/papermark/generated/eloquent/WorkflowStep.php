<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkflowStep extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkflowStep';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workflowId',
        'name',
        'stepOrder',
        'stepType',
        'conditions',
        'actions',
    ];

    protected $attributes = [
        'stepType' => 'ROUTER',
    ];

    protected $casts = [
        'stepOrder' => 'integer',
        'stepType' => WorkflowStepType::class,
        'conditions' => 'array',
        'actions' => 'array',
    ];

    public function workflow(): BelongsTo
    {
        return $this->belongsTo(Workflow::class, 'workflowId');
    }

    public function executionLogs(): HasMany
    {
        return $this->hasMany(WorkflowStepLog::class, 'workflowStepId');
    }
}