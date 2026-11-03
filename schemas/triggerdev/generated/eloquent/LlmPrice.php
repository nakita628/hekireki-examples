<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LlmPrice extends Model
{
    protected $table = 'llm_prices';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'model_id',
        'pricing_tier_id',
        'usage_type',
        'price',
    ];

    protected $casts = [
        'price' => AsDecimal::class,
    ];

    public function model(): BelongsTo
    {
        return $this->belongsTo(LlmModel::class, 'model_id');
    }

    public function pricingTier(): BelongsTo
    {
        return $this->belongsTo(LlmPricingTier::class, 'pricing_tier_id');
    }
}