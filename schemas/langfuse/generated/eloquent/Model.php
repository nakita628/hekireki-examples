<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Model extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'models';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'model_name',
        'match_pattern',
        'start_date',
        'input_price',
        'output_price',
        'total_price',
        'unit',
        'tokenizer_id',
        'tokenizer_config',
    ];

    protected $casts = [
        'start_date' => 'datetime',
        'input_price' => AsDecimal::class,
        'output_price' => AsDecimal::class,
        'total_price' => AsDecimal::class,
        'tokenizer_config' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function Price(): HasMany
    {
        return $this->hasMany(Price::class, 'model_id');
    }

    public function pricingTiers(): HasMany
    {
        return $this->hasMany(PricingTier::class, 'model_id');
    }
}