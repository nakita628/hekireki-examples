<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LlmPricingTier extends Model
{
    protected $table = 'llm_pricing_tiers';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'model_id',
        'name',
        'is_default',
        'priority',
        'conditions',
    ];

    protected $attributes = [
        'is_default' => true,
        'priority' => 0,
        'conditions' => '[]',
    ];

    protected $casts = [
        'is_default' => 'boolean',
        'priority' => 'integer',
        'conditions' => 'array',
    ];

    public function model(): BelongsTo
    {
        return $this->belongsTo(LlmModel::class, 'model_id');
    }

    public function prices(): HasMany
    {
        return $this->hasMany(LlmPrice::class, 'pricing_tier_id');
    }
}