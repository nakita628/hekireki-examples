<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMBackgroundInteractionSettlement extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_BackgroundInteractionSettlement';

    protected $primaryKey = 'interaction_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'interaction_id',
        'custom_llm_provider',
        'create_context',
        'claimed_at',
        'claimed_by',
        'settled_at',
        'outcome',
    ];

    protected $casts = [
        'create_context' => 'array',
        'claimed_at' => 'datetime',
        'settled_at' => 'datetime',
    ];
}