<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMScheduledJobRun extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['scheduled_for', 'claimed_at', 'lease_expires_at', 'finished_at', 'created_at'];

    protected $table = 'LiteLLM_ScheduledJobRun';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'job_name',
        'lock_group',
        'schedule',
        'scheduled_for',
        'status',
        'claimed_by',
        'claimed_at',
        'lease_expires_at',
        'finished_at',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'scheduled_for' => 'datetime',
        'status' => ScheduledJobRunStatus::class,
        'claimed_at' => 'datetime',
        'lease_expires_at' => 'datetime',
        'finished_at' => 'datetime',
    ];
}