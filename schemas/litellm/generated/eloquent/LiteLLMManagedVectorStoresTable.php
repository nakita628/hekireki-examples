<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedVectorStoresTable extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ManagedVectorStoresTable';

    protected $primaryKey = 'vector_store_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'vector_store_id',
        'custom_llm_provider',
        'vector_store_name',
        'vector_store_description',
        'vector_store_metadata',
        'litellm_credential_name',
        'litellm_params',
        'team_id',
        'user_id',
    ];

    protected $casts = [
        'vector_store_metadata' => 'array',
        'litellm_params' => 'array',
    ];
}