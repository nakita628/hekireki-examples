<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PricingTier extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'pricing_tiers';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'model_id',
        'name',
        'is_default',
        'priority',
        'conditions',
    ];

    protected $attributes = [
        'is_default' => false,
    ];

    protected $casts = [
        'is_default' => 'boolean',
        'priority' => 'integer',
        'conditions' => 'array',
    ];

    public function model(): BelongsTo
    {
        return $this->belongsTo(Model::class, 'model_id');
    }

    public function prices(): HasMany
    {
        return $this->hasMany(Price::class, 'pricing_tier_id');
    }
}