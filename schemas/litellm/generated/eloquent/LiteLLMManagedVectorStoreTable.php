<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedVectorStoreTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ManagedVectorStoreTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'unified_resource_id',
        'resource_object',
        'model_mappings',
        'flat_model_resource_ids',
        'storage_backend',
        'storage_url',
        'created_by',
        'team_id',
        'updated_by',
    ];

    protected $casts = [
        'resource_object' => 'array',
        'model_mappings' => 'array',
    ];
}