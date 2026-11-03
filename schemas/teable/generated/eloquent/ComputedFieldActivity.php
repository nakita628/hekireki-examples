<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ComputedFieldActivity extends Model
{
    use PrismaDates;

    const CREATED_AT = null;

    protected $table = 'computed_field_activity';

    protected $primaryKey = 'field_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'field_id',
        'table_id',
        'base_id',
        'status',
        'active_task_count',
        'processing_task_count',
        'generation',
        'estimated_complexity',
        'estimated_dirty_records',
        'has_all_target_records',
        'queued_at',
        'started_at',
        'last_completed_at',
        'last_duration_ms',
        'last_error',
        'extensions',
    ];

    protected $attributes = [
        'active_task_count' => 0,
        'processing_task_count' => 0,
        'generation' => 0,
        'estimated_complexity' => 0,
        'estimated_dirty_records' => 0,
        'has_all_target_records' => false,
    ];

    protected $casts = [
        'active_task_count' => 'integer',
        'processing_task_count' => 'integer',
        'generation' => 'integer',
        'estimated_complexity' => 'integer',
        'estimated_dirty_records' => 'integer',
        'has_all_target_records' => 'boolean',
        'queued_at' => 'datetime',
        'started_at' => 'datetime',
        'last_completed_at' => 'datetime',
        'last_duration_ms' => 'integer',
        'last_error' => 'array',
        'extensions' => 'array',
    ];
}