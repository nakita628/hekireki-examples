<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class CacheData extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'cache_data';

    protected $fillable = [
        'name',
        'data',
        'belongsTo',
        'byId',
        'expiresAt',
        'lastUpdatedAt',
    ];

    protected $casts = [
        'byId' => 'integer',
        'expiresAt' => 'datetime',
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
}