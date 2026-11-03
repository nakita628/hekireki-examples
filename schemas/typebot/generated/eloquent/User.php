<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'lastActivityAt',
        'name',
        'email',
        'emailVerified',
        'image',
        'company',
        'onboardingCategories',
        'referral',
        'graphNavigation',
        'preferredAppAppearance',
        'displayedInAppNotifications',
        'groupTitlesAutoGeneration',
        'preferredLanguage',
        'termsAcceptedAt',
    ];

    protected $casts = [
        'lastActivityAt' => 'datetime',
        'emailVerified' => 'datetime',
        'onboardingCategories' => 'array',
        'graphNavigation' => GraphNavigation::class,
        'displayedInAppNotifications' => 'array',
        'groupTitlesAutoGeneration' => 'array',
        'termsAcceptedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastActivityAt', $this->attributes)) {
                $this->setAttribute('lastActivityAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function apiTokens(): HasMany
    {
        return $this->hasMany(ApiToken::class, 'ownerId');
    }

    public function CollaboratorsOnTypebots(): HasMany
    {
        return $this->hasMany(CollaboratorsOnTypebots::class, 'userId');
    }

    public function workspaces(): HasMany
    {
        return $this->hasMany(MemberInWorkspace::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function bannedIps(): HasMany
    {
        return $this->hasMany(BannedIp::class, 'userId');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(UserCredentials::class, 'userId');
    }
}