<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMMCPServerOAuthClient extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_MCPServerOAuthClient';

    protected $primaryKey = 'server_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'server_id',
        'credentials',
    ];

    protected $casts = [
        'credentials' => 'array',
    ];
}