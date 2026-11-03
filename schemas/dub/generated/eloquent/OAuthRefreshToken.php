<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthRefreshToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthRefreshToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'installationId',
        'accessTokenId',
        'hashedRefreshToken',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];

    public function accessToken(): BelongsTo
    {
        return $this->belongsTo(RestrictedToken::class, 'accessTokenId');
    }

    public function installedIntegration(): BelongsTo
    {
        return $this->belongsTo(InstalledIntegration::class, 'installationId');
    }
}