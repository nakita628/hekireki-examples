<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PromptProtectedLabels extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'prompt_protected_labels';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'label',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}