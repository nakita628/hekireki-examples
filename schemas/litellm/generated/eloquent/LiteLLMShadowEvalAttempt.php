<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMShadowEvalAttempt extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_ShadowEvalAttempt';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'job_id',
        'request_id',
        'outcome',
        'router_name',
        'tier',
        'real_model',
        'shadow_model',
        'confidence',
        'judge_cost',
        'shadow_cost',
        'real_cost',
        'real_classifier_cost',
        'shadow_classifier_cost',
        'real_cache_hit',
        'error',
    ];

    protected $attributes = [
        'judge_cost' => 0.0,
        'shadow_cost' => 0.0,
        'real_classifier_cost' => 0.0,
        'shadow_classifier_cost' => 0.0,
        'real_cache_hit' => false,
    ];

    protected $casts = [
        'confidence' => 'float',
        'judge_cost' => 'float',
        'shadow_cost' => 'float',
        'real_cost' => 'float',
        'real_classifier_cost' => 'float',
        'shadow_classifier_cost' => 'float',
        'real_cache_hit' => 'boolean',
    ];
}