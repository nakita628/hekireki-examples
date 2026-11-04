<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ExternalCommunicationConnectors extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'external_communication_connectors';

    protected $fillable = [
        'type',
        'config',
        'active',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'config' => '{}',
        'active' => false,
    ];

    protected $casts = [
        'active' => 'boolean',
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