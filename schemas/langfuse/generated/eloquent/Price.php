<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Price extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'prices';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'model_id',
        'project_id',
        'pricing_tier_id',
        'usage_type',
        'price',
    ];

    protected $casts = [
        'price' => AsDecimal::class,
    ];

    public function Model(): BelongsTo
    {
        return $this->belongsTo(Model::class, 'model_id');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function pricingTier(): BelongsTo
    {
        return $this->belongsTo(PricingTier::class, 'pricing_tier_id');
    }
}