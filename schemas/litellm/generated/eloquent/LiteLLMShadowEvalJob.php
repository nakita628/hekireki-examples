<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMShadowEvalJob extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_ShadowEvalJob';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'group_id',
        'target_type',
        'target_id',
        'router_name',
        'router_names',
        'models',
        'direction',
        'baseline_model',
        'judge_model',
        'shadow_percentage',
        'max_turns',
        'max_budget',
        'created_by',
        'ends_at',
        'stopped_at',
        'stopped_by',
    ];

    protected $attributes = [
        'target_type' => 'key',
        'direction' => 'forward',
    ];

    protected $casts = [
        'shadow_percentage' => 'float',
        'max_turns' => 'integer',
        'max_budget' => 'float',
        'ends_at' => 'datetime',
        'stopped_at' => 'datetime',
    ];
}