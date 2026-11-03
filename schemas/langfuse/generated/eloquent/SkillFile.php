<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SkillFile extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'skill_files';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'skill_id',
        'blob_id',
        'path',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function skill(): BelongsTo
    {
        return $this->belongsTo(Skill::class, 'skill_id');
    }

    public function blob(): BelongsTo
    {
        return $this->belongsTo(SkillBlob::class, 'blob_id');
    }
}