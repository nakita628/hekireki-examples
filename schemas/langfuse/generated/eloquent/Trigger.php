<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Trigger extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'triggers';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'eventSource',
        'eventActions',
        'filter',
        'status',
    ];

    protected $attributes = [
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'filter' => 'array',
        'status' => JobConfigState::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function automationExecutions(): HasMany
    {
        return $this->hasMany(AutomationExecution::class, 'trigger_id');
    }

    public function automations(): HasMany
    {
        return $this->hasMany(Automation::class, 'trigger_id');
    }
}