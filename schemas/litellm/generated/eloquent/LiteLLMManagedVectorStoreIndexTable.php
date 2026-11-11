<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedVectorStoreIndexTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ManagedVectorStoreIndexTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'index_name',
        'litellm_params',
        'index_info',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'litellm_params' => 'array',
        'index_info' => 'array',
    ];
}