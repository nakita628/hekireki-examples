<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Job extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Job';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'payload',
        'options',
        'scheduledAt',
        'attempts',
        'lastError',
    ];

    protected $attributes = [
        'attempts' => 0,
    ];

    protected $casts = [
        'payload' => 'array',
        'options' => 'array',
        'scheduledAt' => 'datetime',
        'attempts' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('scheduledAt', $this->attributes)) {
                $this->setAttribute('scheduledAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}