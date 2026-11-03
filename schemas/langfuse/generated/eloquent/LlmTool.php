<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LlmTool extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'llm_tools';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'description',
        'parameters',
    ];

    protected $casts = [
        'parameters' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}