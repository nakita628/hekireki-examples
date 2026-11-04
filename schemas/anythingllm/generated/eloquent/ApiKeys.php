<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ApiKeys extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'api_keys';

    protected $fillable = [
        'name',
        'secret',
        'createdBy',
        'lastUpdatedAt',
    ];

    protected $casts = [
        'createdBy' => 'integer',
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