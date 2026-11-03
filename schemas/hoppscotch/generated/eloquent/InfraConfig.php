<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class InfraConfig extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'InfraConfig';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'value',
        'isEncrypted',
        'lastSyncedEnvFileValue',
        'createdOn',
    ];

    protected $attributes = [
        'isEncrypted' => false,
    ];

    protected $casts = [
        'isEncrypted' => 'boolean',
        'createdOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}