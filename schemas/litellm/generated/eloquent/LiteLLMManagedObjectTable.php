<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedObjectTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ManagedObjectTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'unified_object_id',
        'model_object_id',
        'file_object',
        'file_purpose',
        'status',
        'batch_processed',
        'created_by',
        'team_id',
        'org_id',
        'api_key',
        'request_tags',
        'end_user_id',
        'updated_by',
    ];

    protected $attributes = [
        'batch_processed' => false,
        'request_tags' => '[]',
    ];

    protected $casts = [
        'file_object' => 'array',
        'batch_processed' => 'boolean',
        'request_tags' => 'array',
    ];
}