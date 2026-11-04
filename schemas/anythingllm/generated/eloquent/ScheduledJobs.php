<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ScheduledJobs extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'scheduled_jobs';

    protected $fillable = [
        'name',
        'prompt',
        'tools',
        'schedule',
        'enabled',
        'lastRunAt',
        'nextRunAt',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'lastRunAt' => 'datetime',
        'nextRunAt' => 'datetime',
    ];

    public function runs(): HasMany
    {
        return $this->hasMany(ScheduledJobRuns::class, 'jobId');
    }
}