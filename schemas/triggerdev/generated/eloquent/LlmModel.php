<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LlmModel extends Model
{
    use PrismaDates;

    protected $table = 'llm_models';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendly_id',
        'project_id',
        'model_name',
        'match_pattern',
        'start_date',
        'source',
        'provider',
        'description',
        'context_window',
        'max_output_tokens',
        'capabilities',
        'is_hidden',
        'base_model_name',
        'pricing_unit',
    ];

    protected $attributes = [
        'source' => 'default',
        'is_hidden' => false,
    ];

    protected $casts = [
        'start_date' => 'datetime',
        'context_window' => 'integer',
        'max_output_tokens' => 'integer',
        'is_hidden' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function pricingTiers(): HasMany
    {
        return $this->hasMany(LlmPricingTier::class, 'model_id');
    }

    public function prices(): HasMany
    {
        return $this->hasMany(LlmPrice::class, 'model_id');
    }
}