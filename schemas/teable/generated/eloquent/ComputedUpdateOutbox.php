<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ComputedUpdateOutbox extends Model
{
    use PrismaDates;

    protected $table = 'computed_update_outbox';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'base_id',
        'seed_table_id',
        'seed_record_ids',
        'change_type',
        'steps',
        'edges',
        'status',
        'attempts',
        'max_attempts',
        'next_run_at',
        'locked_at',
        'locked_by',
        'last_error',
        'estimated_complexity',
        'plan_hash',
        'dirty_stats',
        'run_id',
        'origin_run_ids',
        'run_total_steps',
        'run_completed_steps_before',
        'affected_table_ids',
        'affected_field_ids',
        'sync_max_level',
        'source_changed_at',
        'stage_depth',
        'predecessor_task_id',
    ];

    protected $attributes = [
        'attempts' => 0,
        'max_attempts' => 8,
        'estimated_complexity' => 0,
        'run_total_steps' => 0,
        'run_completed_steps_before' => 0,
        'stage_depth' => 0,
    ];

    protected $casts = [
        'seed_record_ids' => 'array',
        'steps' => 'array',
        'edges' => 'array',
        'attempts' => 'integer',
        'max_attempts' => 'integer',
        'next_run_at' => 'datetime',
        'locked_at' => 'datetime',
        'estimated_complexity' => 'integer',
        'dirty_stats' => 'array',
        'run_total_steps' => 'integer',
        'run_completed_steps_before' => 'integer',
        'sync_max_level' => 'integer',
        'source_changed_at' => 'datetime',
        'stage_depth' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('next_run_at', $this->attributes)) {
                $this->setAttribute('next_run_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function seeds(): HasMany
    {
        return $this->hasMany(ComputedUpdateOutboxSeed::class, 'task_id');
    }
}