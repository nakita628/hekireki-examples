<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PlatformOAuthClient extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'PlatformOAuthClient';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'secret',
        'permissions',
        'logo',
        'redirectUris',
        'organizationId',
        'bookingRedirectUri',
        'bookingCancelRedirectUri',
        'bookingRescheduleRedirectUri',
        'areEmailsEnabled',
        'areDefaultEventTypesEnabled',
        'areCalendarEventsEnabled',
    ];

    protected $attributes = [
        'areEmailsEnabled' => false,
        'areDefaultEventTypesEnabled' => true,
        'areCalendarEventsEnabled' => true,
    ];

    protected $casts = [
        'permissions' => 'integer',
        'organizationId' => 'integer',
        'areEmailsEnabled' => 'boolean',
        'areDefaultEventTypesEnabled' => 'boolean',
        'areCalendarEventsEnabled' => 'boolean',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(Team::class, 'createdByOAuthClientId');
    }

    public function accessTokens(): HasMany
    {
        return $this->hasMany(AccessToken::class, 'platformOAuthClientId');
    }

    public function refreshToken(): HasMany
    {
        return $this->hasMany(RefreshToken::class, 'platformOAuthClientId');
    }

    public function authorizationTokens(): HasMany
    {
        return $this->hasMany(PlatformAuthorizationToken::class, 'platformOAuthClientId');
    }

    public function webhook(): HasMany
    {
        return $this->hasMany(Webhook::class, 'platformOAuthClientId');
    }

    public function users(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_PlatformOAuthClientToUser', 'A', 'B');
    }
}