<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthAuthorization extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OAuthAuthorization';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'oauthAppId',
        'userId',
        'organizationId',
        'accessToken',
        'authorizationCode',
        'codeExpiresAt',
        'codeChallenge',
        'codeChallengeMethod',
        'redirectUri',
        'revokedAt',
    ];

    protected $casts = [
        'codeExpiresAt' => 'datetime',
        'revokedAt' => 'datetime',
    ];

    public function oauthApp(): BelongsTo
    {
        return $this->belongsTo(OAuthApp::class, 'oauthAppId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}