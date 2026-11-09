<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ReprocessingJobs extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at', 'started_at', 'completed_at'];

    protected $table = 'reprocessing_jobs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'config',
        'status',
        'started_at',
        'completed_at',
        'k8s_job_name',
        'total_files',
        'total_workers',
        'error',
    ];

    protected $attributes = [
        'total_files' => 0,
        'total_workers' => 0,
    ];

    protected $casts = [
        'config' => 'array',
        'started_at' => 'datetime',
        'completed_at' => 'datetime',
        'total_files' => 'integer',
        'total_workers' => 'integer',
    ];

    public function reprocessing_workers(): HasMany
    {
        return $this->hasMany(ReprocessingWorkers::class, 'job_id');
    }
}