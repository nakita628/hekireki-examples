<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AutomationExecution extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'automation_executions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'source_id',
        'automation_id',
        'trigger_id',
        'action_id',
        'project_id',
        'status',
        'input',
        'output',
        'started_at',
        'finished_at',
        'error',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => ActionExecutionStatus::class,
        'input' => 'array',
        'output' => 'array',
        'started_at' => 'datetime',
        'finished_at' => 'datetime',
    ];

    public function automation(): BelongsTo
    {
        return $this->belongsTo(Automation::class, 'automation_id');
    }

    public function trigger(): BelongsTo
    {
        return $this->belongsTo(Trigger::class, 'trigger_id');
    }

    public function action(): BelongsTo
    {
        return $this->belongsTo(Action::class, 'action_id');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}