<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ModelRouterRules extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'model_router_rules';

    protected $fillable = [
        'router_id',
        'enabled',
        'priority',
        'type',
        'title',
        'description',
        'condition_logic',
        'conditions',
        'route_provider',
        'route_model',
        'created_by',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'enabled' => true,
        'type' => 'calculated',
    ];

    protected $casts = [
        'router_id' => 'integer',
        'enabled' => 'boolean',
        'priority' => 'integer',
        'created_by' => 'integer',
        'lastUpdatedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function router(): BelongsTo
    {
        return $this->belongsTo(ModelRouters::class, 'router_id');
    }
}