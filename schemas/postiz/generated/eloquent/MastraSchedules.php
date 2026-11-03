<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraSchedules extends Model
{
    protected $table = 'mastra_schedules';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'target',
        'cron',
        'timezone',
        'status',
        'next_fire_at',
        'last_fire_at',
        'last_run_id',
        'created_at',
        'updated_at',
        'metadata',
        'owner_type',
        'owner_id',
    ];

    protected $casts = [
        'target' => 'array',
        'next_fire_at' => 'integer',
        'last_fire_at' => 'integer',
        'created_at' => 'integer',
        'updated_at' => 'integer',
        'metadata' => 'array',
    ];
}