<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class TaskLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const ZONED_DATES = ['timestamp'];

    protected $table = 'task_log';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'level',
        'logger',
        'message',
        'sync_id',
        'task_id',
        'timestamp',
    ];

    protected $casts = [
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