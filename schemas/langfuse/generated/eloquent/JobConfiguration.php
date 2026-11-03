<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class JobConfiguration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'job_configurations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'job_type',
        'status',
        'blocked_at',
        'block_reason',
        'block_message',
        'eval_template_id',
        'score_name',
        'filter',
        'target_object',
        'variable_mapping',
        'sampling',
        'delay',
        'time_scope',
    ];

    protected $attributes = [
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'job_type' => JobType::class,
        'status' => JobConfigState::class,
        'blocked_at' => 'datetime',
        'block_reason' => EvaluatorBlockReason::class,
        'filter' => 'array',
        'variable_mapping' => 'array',
        'sampling' => AsDecimal::class,
        'delay' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function evalTemplate(): BelongsTo
    {
        return $this->belongsTo(EvalTemplate::class, 'eval_template_id');
    }
}