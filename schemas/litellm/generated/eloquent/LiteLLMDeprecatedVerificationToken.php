<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMDeprecatedVerificationToken extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_DeprecatedVerificationToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'token',
        'active_token_id',
        'revoke_at',
    ];

    protected $casts = [
        'revoke_at' => 'datetime',
    ];
}