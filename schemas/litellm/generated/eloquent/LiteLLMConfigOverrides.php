<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMConfigOverrides extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ConfigOverrides';

    protected $primaryKey = 'config_type';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'config_type',
        'config_value',
    ];

    protected $casts = [
        'config_value' => 'array',
    ];
}