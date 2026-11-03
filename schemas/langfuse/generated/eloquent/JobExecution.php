<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class JobExecution extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'job_executions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'job_configuration_id',
        'job_template_id',
        'status',
        'start_time',
        'end_time',
        'error',
        'job_input_trace_id',
        'job_input_trace_timestamp',
        'job_input_observation_id',
        'job_input_dataset_item_id',
        'job_input_dataset_item_valid_from',
        'job_output_score_id',
        'execution_trace_id',
    ];

    protected $casts = [
        'status' => JobExecutionStatus::class,
        'start_time' => 'datetime',
        'end_time' => 'datetime',
        'job_input_trace_timestamp' => 'datetime',
        'job_input_dataset_item_valid_from' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}