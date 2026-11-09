<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthAccessToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthAccessToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'lastUsed',
        'hash',
        'expiresAt',
        'refreshTokenId',
    ];

    protected $casts = [
        'lastUsed' => 'datetime',
        'expiresAt' => 'datetime',
    ];

    public function refreshToken(): BelongsTo
    {
        return $this->belongsTo(UserApiToken::class, 'refreshTokenId');
    }
}