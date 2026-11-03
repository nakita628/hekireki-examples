<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LlmSchema extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'llm_schemas';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'description',
        'schema',
    ];

    protected $casts = [
        'schema' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}