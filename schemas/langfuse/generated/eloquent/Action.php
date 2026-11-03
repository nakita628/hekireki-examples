<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Action extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'actions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'type',
        'config',
    ];

    protected $casts = [
        'type' => ActionType::class,
        'config' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function automations(): HasMany
    {
        return $this->hasMany(Automation::class, 'action_id');
    }

    public function automationExecutions(): HasMany
    {
        return $this->hasMany(AutomationExecution::class, 'action_id');
    }
}