<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Automation extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'automations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'trigger_id',
        'action_id',
        'project_id',
    ];

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

    public function AutomationExecution(): HasMany
    {
        return $this->hasMany(AutomationExecution::class, 'automation_id');
    }
}