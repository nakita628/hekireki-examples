<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PromptVersion extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'prompt_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'version',
        'promptId',
        'textContent',
        'chatContent',
        'model',
        'config',
        'source',
        'commitMessage',
        'contentHash',
        'labels',
        'workerId',
        'createdBy',
    ];

    protected $casts = [
        'version' => 'integer',
        'chatContent' => 'array',
        'config' => 'array',
    ];

    public function prompt(): BelongsTo
    {
        return $this->belongsTo(Prompt::class, 'promptId');
    }

    public function worker(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorker::class, 'workerId');
    }
}