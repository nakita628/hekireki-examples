<?php

namespace App\Models;


class SsoConfig extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'sso_configs';

    protected $primaryKey = 'domain';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'domain',
        'auth_provider',
        'auth_config',
    ];

    protected $casts = [
        'auth_config' => 'array',
    ];
}