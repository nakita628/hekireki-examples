<?php

namespace App\Models;


class BackgroundMigration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'background_migrations';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'name',
        'script',
        'args',
        'state',
        'finished_at',
        'failed_at',
        'failed_reason',
        'worker_id',
        'locked_at',
    ];

    protected $attributes = [
        'state' => '{}',
    ];

    protected $casts = [
        'args' => 'array',
        'state' => 'array',
        'finished_at' => 'datetime',
        'failed_at' => 'datetime',
        'locked_at' => 'datetime',
    ];
}