<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class Notification extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const ZONED_DATES = ['timestamp'];

    protected $table = 'Notification';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'workspaceId',
        'actorId',
        'type',
        'tableName',
        'channelId',
        'statusChangeId',
        'timestamp',
        'status',
        'error',
    ];

    protected $casts = [
        'statusChangeId' => 'integer',
        'timestamp' => 'datetime',
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