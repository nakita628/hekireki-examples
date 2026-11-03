<?php

namespace App\Models;


class CronJobs extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'cron_jobs';

    protected $primaryKey = 'name';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'name',
        'last_run',
        'job_started_at',
        'state',
    ];

    protected $casts = [
        'last_run' => 'datetime',
        'job_started_at' => 'datetime',
    ];
}