<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class StatusChange extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['timestamp'];

    protected $table = 'StatusChange';

    public $timestamps = false;

    protected $fillable = [
        'workspaceId',
        'actorId',
        'type',
        'tableName',
        'timestamp',
        'status',
        'startedAt',
        'counts',
        'description',
        'queueSize',
    ];

    protected $attributes = [
        'counts' => 0,
        'queueSize' => 0,
    ];

    protected $casts = [
        'timestamp' => 'datetime',
        'startedAt' => 'datetime',
        'counts' => 'integer',
        'queueSize' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('timestamp', $this->attributes)) {
                $this->setAttribute('timestamp', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}