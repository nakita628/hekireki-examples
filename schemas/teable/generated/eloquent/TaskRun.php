<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskRun extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'task_run';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'task_id',
        'base_id',
        'status',
        'snapshot',
        'depends_on_run_ids',
        'spent',
        'log',
        'error_msg',
        'started_time',
        'created_time',
    ];

    protected $casts = [
        'spent' => 'integer',
        'started_time' => 'datetime',
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

    public function task(): BelongsTo
    {
        return $this->belongsTo(Task::class, 'task_id');
    }
}