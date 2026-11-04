<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SystemSettings extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'system_settings';

    protected $fillable = [
        'label',
        'value',
        'lastUpdatedAt',
    ];

    protected $casts = [
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