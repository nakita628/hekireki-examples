<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Team extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Team';

    protected $fillable = [
        'name',
        'url',
        'avatarImageId',
        'organisationId',
        'teamGlobalSettingsId',
    ];

    public function avatarImage(): BelongsTo
    {
        return $this->belongsTo(AvatarImage::class, 'avatarImageId');
    }

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }

    public function teamGlobalSettings(): BelongsTo
    {
        return $this->belongsTo(TeamGlobalSettings::class, 'teamGlobalSettingsId');
    }

    public function teamEmail(): HasOne
    {
        return $this->hasOne(TeamEmail::class, 'teamId');
    }

    public function emailVerification(): HasOne
    {
        return $this->hasOne(TeamEmailVerification::class, 'teamId');
    }

    public function profile(): HasOne
    {
        return $this->hasOne(TeamProfile::class, 'teamId');
    }

    public function envelopes(): HasMany
    {
        return $this->hasMany(Envelope::class, 'teamId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(Folder::class, 'teamId');
    }

    public function apiTokens(): HasMany
    {
        return $this->hasMany(ApiToken::class, 'teamId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'teamId');
    }

    public function teamGroups(): HasMany
    {
        return $this->hasMany(TeamGroup::class, 'teamId');
    }
}