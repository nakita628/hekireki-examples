<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedFileTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ManagedFileTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'unified_file_id',
        'file_object',
        'model_mappings',
        'flat_model_file_ids',
        'storage_backend',
        'storage_url',
        'created_by',
        'team_id',
        'updated_by',
    ];

    protected $casts = [
        'file_object' => 'array',
        'model_mappings' => 'array',
    ];
}