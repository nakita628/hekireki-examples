<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class EventLogs extends Model
{
    use PrismaDates;

    protected $table = 'event_logs';

    public $timestamps = false;

    protected $fillable = [
        'event',
        'metadata',
        'userId',
        'occurredAt',
    ];

    protected $casts = [
        'userId' => 'integer',
        'occurredAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('occurredAt', $this->attributes)) {
                $this->setAttribute('occurredAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}