<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class Task extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Task';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'scheduledAt',
        'succeededAt',
        'type',
        'payload',
        'attempts',
        'maxAttempts',
        'lastError',
        'lastFailedAttemptAt',
        'referenceUid',
    ];

    protected $attributes = [
        'attempts' => 0,
        'maxAttempts' => 3,
    ];

    protected $casts = [
        'scheduledAt' => 'datetime',
        'succeededAt' => 'datetime',
        'attempts' => 'integer',
        'maxAttempts' => 'integer',
        'lastFailedAttemptAt' => 'datetime',
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