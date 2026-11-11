<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMProxyModelTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ProxyModelTable';

    protected $primaryKey = 'model_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'model_name',
        'litellm_params',
        'model_info',
        'blocked',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'blocked' => false,
    ];

    protected $casts = [
        'litellm_params' => 'array',
        'model_info' => 'array',
        'blocked' => 'boolean',
    ];
}