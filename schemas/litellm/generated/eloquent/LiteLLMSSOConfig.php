<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMSSOConfig extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_SSOConfig';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'sso_settings',
    ];

    protected $casts = [
        'sso_settings' => 'array',
    ];
}