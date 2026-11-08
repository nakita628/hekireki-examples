<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkflowTransition extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'workflow_transitions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'fromStepId',
        'toStepId',
        'condition',
        'priority',
    ];

    protected $attributes = [
        'priority' => 0,
    ];

    protected $casts = [
        'condition' => 'array',
        'priority' => 'integer',
    ];

    public function fromStep(): BelongsTo
    {
        return $this->belongsTo(WorkflowStep::class, 'fromStepId');
    }

    public function toStep(): BelongsTo
    {
        return $this->belongsTo(WorkflowStep::class, 'toStepId');
    }
}