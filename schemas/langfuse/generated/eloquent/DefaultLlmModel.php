<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DefaultLlmModel extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'default_llm_models';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'llm_api_key_id',
        'provider',
        'adapter',
        'model',
        'model_params',
    ];

    protected $casts = [
        'model_params' => 'array',
    ];

    public function Project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function LlmApiKey(): BelongsTo
    {
        return $this->belongsTo(LlmApiKeys::class, 'llm_api_key_id');
    }
}