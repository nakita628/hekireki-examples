<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Feature extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Feature';

    protected $primaryKey = 'slug';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'slug',
        'enabled',
        'description',
        'type',
        'stale',
        'lastUsedAt',
        'updatedBy',
    ];

    protected $attributes = [
        'enabled' => false,
        'type' => 'RELEASE',
        'stale' => false,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'type' => FeatureType::class,
        'stale' => 'boolean',
        'lastUsedAt' => 'datetime',
        'updatedBy' => 'integer',
    ];

    public function users(): HasMany
    {
        return $this->hasMany(UserFeatures::class, 'featureId');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(TeamFeatures::class, 'featureId');
    }
}