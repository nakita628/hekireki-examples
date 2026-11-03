<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Prompt extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'prompts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'created_by',
        'prompt',
        'name',
        'version',
        'type',
        'is_active',
        'config',
        'tags',
        'labels',
        'commit_message',
    ];

    protected $attributes = [
        'type' => 'text',
        'config' => '{}',
    ];

    protected $casts = [
        'prompt' => 'array',
        'version' => 'integer',
        'is_active' => 'boolean',
        'config' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function PromptDependency(): HasMany
    {
        return $this->hasMany(PromptDependency::class, 'parent_id');
    }
}