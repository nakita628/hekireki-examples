<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PromptDependency extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'prompt_dependencies';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'parent_id',
        'child_name',
        'child_label',
        'child_version',
    ];

    protected $casts = [
        'child_version' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function parent(): BelongsTo
    {
        return $this->belongsTo(Prompt::class, 'parent_id');
    }
}