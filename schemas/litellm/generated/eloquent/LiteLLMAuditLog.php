<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMAuditLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = null;

    protected $table = 'LiteLLM_AuditLog';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'changed_by',
        'changed_by_api_key',
        'action',
        'table_name',
        'object_id',
        'before_value',
        'updated_values',
    ];

    protected $attributes = [
        'changed_by' => '',
        'changed_by_api_key' => '',
    ];

    protected $casts = [
        'before_value' => 'array',
        'updated_values' => 'array',
    ];
}