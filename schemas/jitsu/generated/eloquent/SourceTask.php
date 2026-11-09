<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SourceTask extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const ZONED_DATES = ['started_at', 'updated_at'];

    protected $table = 'source_task';

    protected $primaryKey = 'task_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'task_id',
        'sync_id',
        'package',
        'version',
        'started_at',
        'started_by',
        'status',
        'description',
        'metrics',
        'error',
    ];

    protected $casts = [
        'started_at' => 'datetime',
        'started_by' => 'array',
        'metrics' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('started_at', $this->attributes)) {
                $this->setAttribute('started_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}