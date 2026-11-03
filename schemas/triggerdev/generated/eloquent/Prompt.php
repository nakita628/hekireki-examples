<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Prompt extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'prompts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendly_id',
        'slug',
        'description',
        'type',
        'organizationId',
        'projectId',
        'runtimeEnvironmentId',
        'filePath',
        'exportName',
        'variableSchema',
        'defaultModel',
        'defaultConfig',
        'tags',
        'archivedAt',
    ];

    protected $attributes = [
        'type' => 'text',
    ];

    protected $casts = [
        'variableSchema' => 'array',
        'defaultConfig' => 'array',
        'archivedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function versions(): HasMany
    {
        return $this->hasMany(PromptVersion::class, 'promptId');
    }
}