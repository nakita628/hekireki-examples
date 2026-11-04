<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ScheduledJobRuns extends Model
{
    use PrismaDates;

    protected $table = 'scheduled_job_runs';

    public $timestamps = false;

    protected $fillable = [
        'jobId',
        'status',
        'result',
        'error',
        'startedAt',
        'completedAt',
        'readAt',
    ];

    protected $attributes = [
        'status' => 'queued',
    ];

    protected $casts = [
        'jobId' => 'integer',
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
        'readAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('startedAt', $this->attributes)) {
                $this->setAttribute('startedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function job(): BelongsTo
    {
        return $this->belongsTo(ScheduledJobs::class, 'jobId');
    }
}