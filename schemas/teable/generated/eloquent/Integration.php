<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Integration extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'integration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'resource_id',
        'config',
        'type',
        'enable',
        'created_time',
    ];

    protected $casts = [
        'enable' => 'boolean',
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}