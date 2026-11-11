<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMCacheConfig extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_CacheConfig';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'cache_settings',
    ];

    protected $casts = [
        'cache_settings' => 'array',
    ];
}