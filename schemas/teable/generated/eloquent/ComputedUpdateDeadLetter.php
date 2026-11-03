<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ComputedUpdateDeadLetter extends Model
{
    use PrismaDates;

    protected $table = 'computed_update_dead_letter';

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
        'trace_data',
        'failed_at',
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
        'trace_data' => 'array',
        'failed_at' => 'datetime',
    ];
}