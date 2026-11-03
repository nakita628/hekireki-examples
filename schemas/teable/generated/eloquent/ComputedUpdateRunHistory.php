<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ComputedUpdateRunHistory extends Model
{
    use PrismaDates;

    protected $table = 'computed_update_run_history';

    protected $primaryKey = 'task_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'task_id',
        'base_id',
        'seed_table_id',
        'change_type',
        'run_id',
        'origin_run_ids',
        'steps',
        'edges',
        'affected_table_ids',
        'affected_field_ids',
        'source_field_ids',
        'seed_record_count',
        'stage_depth',
        'predecessor_task_id',
        'run_total_steps',
        'run_completed_steps_before',
        'sync_max_level',
        'estimated_complexity',
        'attempts',
        'outcome',
        'source_changed_at',
        'enqueued_at',
        'started_at',
        'completed_at',
        'duration_ms',
    ];

    protected $attributes = [
        'seed_record_count' => 0,
        'stage_depth' => 0,
        'run_total_steps' => 0,
        'run_completed_steps_before' => 0,
        'estimated_complexity' => 0,
        'attempts' => 0,
        'duration_ms' => 0,
    ];

    protected $casts = [
        'steps' => 'array',
        'edges' => 'array',
        'seed_record_count' => 'integer',
        'stage_depth' => 'integer',
        'run_total_steps' => 'integer',
        'run_completed_steps_before' => 'integer',
        'sync_max_level' => 'integer',
        'estimated_complexity' => 'integer',
        'attempts' => 'integer',
        'source_changed_at' => 'datetime',
        'enqueued_at' => 'datetime',
        'started_at' => 'datetime',
        'completed_at' => 'datetime',
        'duration_ms' => 'integer',
    ];
}