<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class RestrictedToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RestrictedToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'hashedKey',
        'partialKey',
        'scopes',
        'expires',
        'lastUsed',
        'userId',
        'projectId',
        'installationId',
    ];

    protected $casts = [
        'expires' => 'datetime',
        'lastUsed' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function installedIntegration(): BelongsTo
    {
        return $this->belongsTo(InstalledIntegration::class, 'installationId');
    }

    public function refreshTokens(): HasMany
    {
        return $this->hasMany(OAuthRefreshToken::class, 'accessTokenId');
    }
}