<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ModelRouters extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'model_routers';

    protected $fillable = [
        'name',
        'description',
        'fallback_provider',
        'fallback_model',
        'cooldown_seconds',
        'created_by',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'cooldown_seconds' => 30,
    ];

    protected $casts = [
        'cooldown_seconds' => 'integer',
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

    public function rules(): HasMany
    {
        return $this->hasMany(ModelRouterRules::class, 'router_id');
    }
}