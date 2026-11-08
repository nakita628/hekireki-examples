<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Workflow extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'workflows';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'description',
        'enabled',
        'triggerType',
        'triggerConfig',
        'allowReentry',
        'throttleWindowSeconds',
        'throttleKey',
        'projectId',
    ];

    protected $attributes = [
        'enabled' => false,
        'allowReentry' => false,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'triggerType' => WorkflowTriggerType::class,
        'triggerConfig' => 'array',
        'allowReentry' => 'boolean',
        'throttleWindowSeconds' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function steps(): HasMany
    {
        return $this->hasMany(WorkflowStep::class, 'workflowId');
    }

    public function executions(): HasMany
    {
        return $this->hasMany(WorkflowExecution::class, 'workflowId');
    }

    public function throttleClaims(): HasMany
    {
        return $this->hasMany(ThrottleClaim::class, 'workflowId');
    }
}