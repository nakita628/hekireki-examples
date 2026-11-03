<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Workflow extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Workflow';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'entryLinkId',
        'teamId',
        'isActive',
    ];

    protected $attributes = [
        'isActive' => true,
    ];

    protected $casts = [
        'isActive' => 'boolean',
    ];

    public function entryLink(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'entryLinkId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function steps(): HasMany
    {
        return $this->hasMany(WorkflowStep::class, 'workflowId');
    }

    public function executions(): HasMany
    {
        return $this->hasMany(WorkflowExecution::class, 'workflowId');
    }
}