<?php

namespace App\Models;


class AuditLog extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'audit_logs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'api_key_id',
        'user_id',
        'org_id',
        'user_org_role',
        'project_id',
        'user_project_role',
        'resource_type',
        'resource_id',
        'action',
        'before',
        'after',
    ];

    protected $attributes = [
        'type' => 'USER',
    ];

    protected $casts = [
        'type' => AuditLogRecordType::class,
    ];
}