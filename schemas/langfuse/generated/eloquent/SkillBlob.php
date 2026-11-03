<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SkillBlob extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'skill_blobs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'sha_256_hash',
        'content_type',
        'content_length',
        'content',
    ];

    protected $casts = [
        'content_length' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function files(): HasMany
    {
        return $this->hasMany(SkillFile::class, 'blob_id');
    }
}