<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthCode extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthCode';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'clientId',
        'userId',
        'projectId',
        'code',
        'scopes',
        'redirectUri',
        'codeChallenge',
        'codeChallengeMethod',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];

    public function oAuthApp(): BelongsTo
    {
        return $this->belongsTo(OAuthApp::class, 'clientId', 'clientId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}