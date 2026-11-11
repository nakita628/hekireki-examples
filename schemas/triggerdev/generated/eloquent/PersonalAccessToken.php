<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PersonalAccessToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PersonalAccessToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'encryptedToken',
        'obfuscatedToken',
        'hashedToken',
        'userId',
        'revokedAt',
        'lastAccessedAt',
        'expiresAt',
        'idleTimeoutSeconds',
        'expiryReminderSentAt',
        'expiredNoticeSentAt',
    ];

    protected $casts = [
        'encryptedToken' => 'array',
        'revokedAt' => 'datetime',
        'lastAccessedAt' => 'datetime',
        'expiresAt' => 'datetime',
        'idleTimeoutSeconds' => 'integer',
        'expiryReminderSentAt' => 'datetime',
        'expiredNoticeSentAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function authorizationCodes(): HasMany
    {
        return $this->hasMany(AuthorizationCode::class, 'personalAccessTokenId');
    }
}