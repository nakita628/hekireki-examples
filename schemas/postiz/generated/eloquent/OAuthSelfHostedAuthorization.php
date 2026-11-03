<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthSelfHostedAuthorization extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OAuthSelfHostedAuthorization';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'oauthAppId',
        'mcpUrl',
        'apiKey',
        'email',
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
}