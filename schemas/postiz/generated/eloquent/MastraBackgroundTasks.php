<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraBackgroundTasks extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ', 'startedAtZ', 'suspendedAtZ', 'completedAtZ'];

    protected $table = 'mastra_background_tasks';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'tool_call_id',
        'tool_name',
        'agent_id',
        'run_id',
        'thread_id',
        'resource_id',
        'status',
        'args',
        'result',
        'error',
        'suspend_payload',
        'retry_count',
        'max_retries',
        'timeout_ms',
        'startedAt',
        'suspendedAt',
        'completedAt',
        'createdAtZ',
        'startedAtZ',
        'suspendedAtZ',
        'completedAtZ',
    ];

    protected $casts = [
        'args' => 'array',
        'result' => 'array',
        'error' => 'array',
        'suspend_payload' => 'array',
        'retry_count' => 'integer',
        'max_retries' => 'integer',
        'timeout_ms' => 'integer',
        'startedAt' => 'datetime',
        'suspendedAt' => 'datetime',
        'completedAt' => 'datetime',
        'createdAtZ' => 'datetime',
        'startedAtZ' => 'datetime',
        'suspendedAtZ' => 'datetime',
        'completedAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('startedAtZ', $this->attributes)) {
                $this->setAttribute('startedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('suspendedAtZ', $this->attributes)) {
                $this->setAttribute('suspendedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('completedAtZ', $this->attributes)) {
                $this->setAttribute('completedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}