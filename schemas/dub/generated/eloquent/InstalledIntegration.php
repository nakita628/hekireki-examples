<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class InstalledIntegration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'InstalledIntegration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'integrationId',
        'projectId',
        'credentials',
        'settings',
    ];

    protected $casts = [
        'credentials' => 'array',
        'settings' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function refreshTokens(): HasMany
    {
        return $this->hasMany(OAuthRefreshToken::class, 'installationId');
    }

    public function accessTokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'installationId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'installationId');
    }
}