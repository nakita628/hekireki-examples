<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Monitor extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'monitors';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'created_by',
        'updated_by',
        'project_id',
        'view',
        'filters',
        'metric',
        'window_ms',
        'cadence_ms',
        'threshold_operator',
        'alert_threshold',
        'warning_threshold',
        'severity',
        'severity_changed_at',
        'no_data',
        'renotify',
        'status',
        'scheduler_batch_id',
        'next_run_at',
        'last_published_at',
        'last_claimed_at',
        'last_completed_at',
        'name',
        'tags',
        'trigger_ids',
        'alerted_at',
    ];

    protected $attributes = [
        'severity' => 'UNKNOWN',
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'view' => MonitorView::class,
        'filters' => 'array',
        'metric' => 'array',
        'window_ms' => 'integer',
        'cadence_ms' => 'integer',
        'threshold_operator' => MonitorThresholdOperator::class,
        'alert_threshold' => AsDecimal::class,
        'warning_threshold' => AsDecimal::class,
        'severity' => MonitorSeverity::class,
        'severity_changed_at' => 'datetime',
        'no_data' => 'array',
        'renotify' => 'array',
        'status' => MonitorStatus::class,
        'scheduler_batch_id' => 'integer',
        'next_run_at' => 'datetime',
        'last_published_at' => 'datetime',
        'last_claimed_at' => 'datetime',
        'last_completed_at' => 'datetime',
        'alerted_at' => 'datetime',
    ];

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function updatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updated_by');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}