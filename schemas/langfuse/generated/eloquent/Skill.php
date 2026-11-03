<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Skill extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'skills';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'created_by',
        'name',
        'description',
        'frontmatter',
        'version',
        'tags',
        'labels',
        'commit_message',
    ];

    protected $attributes = [
        'frontmatter' => '{}',
    ];

    protected $casts = [
        'frontmatter' => 'array',
        'version' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function files(): HasMany
    {
        return $this->hasMany(SkillFile::class, 'skill_id');
    }
}