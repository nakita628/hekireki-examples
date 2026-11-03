<?php

namespace App\Models;


class AuditLogStaging extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'audit_log_staging';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'payload',
        'failed_at',
        'last_error',
    ];

    protected $casts = [
        'payload' => 'array',
        'failed_at' => 'datetime',
    ];
}