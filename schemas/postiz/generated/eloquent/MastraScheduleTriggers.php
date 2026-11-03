<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraScheduleTriggers extends Model
{
    protected $table = 'mastra_schedule_triggers';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'schedule_id',
        'run_id',
        'scheduled_fire_at',
        'actual_fire_at',
        'outcome',
        'error',
        'trigger_kind',
        'parent_trigger_id',
        'metadata',
    ];

    protected $casts = [
        'scheduled_fire_at' => 'integer',
        'actual_fire_at' => 'integer',
        'metadata' => 'array',
    ];
}