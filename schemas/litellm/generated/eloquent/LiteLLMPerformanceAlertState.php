<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMPerformanceAlertState extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const ZONED_DATES = ['next_run_at'];

    protected $table = 'LiteLLM_PerformanceAlertState';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'enabled',
        'alerts_used',
        'owner',
        'next_run_at',
        'day',
        'scope',
        'resume_after',
        'wrapped',
        'checked',
        'skipped',
        'start_scope',
        'start_after',
        'completed_at',
    ];

    protected $attributes = [
        'enabled' => true,
        'resume_after' => '',
        'wrapped' => false,
        'checked' => 0,
        'skipped' => 0,
        'start_after' => '',
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'alerts_used' => 'integer',
        'next_run_at' => 'datetime',
        'wrapped' => 'boolean',
        'checked' => 'integer',
        'skipped' => 'integer',
        'completed_at' => 'datetime',
    ];
}