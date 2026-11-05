<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'firstName',
        'lastName',
        'name',
        'emailVerified',
        'image',
        'twoFactorEnabled',
        'deletedAt',
    ];

    protected $attributes = [
        'emailVerified' => false,
        'twoFactorEnabled' => false,
    ];

    protected $casts = [
        'emailVerified' => 'boolean',
        'twoFactorEnabled' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function twoFactor(): HasOne
    {
        return $this->hasOne(TwoFactor::class, 'userId');
    }

    public function totp(): HasOne
    {
        return $this->hasOne(UserTotp::class, 'userId');
    }

    public function createdOrganizations(): HasMany
    {
        return $this->hasMany(Organization::class, 'createdByUserId');
    }

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Organization::class, 'subscriptionCreatedByUserId');
    }

    public function membership(): HasMany
    {
        return $this->hasMany(Member::class, 'userId');
    }

    public function sentInvites(): HasMany
    {
        return $this->hasMany(Member::class, 'invitedById');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(Invite::class, 'createdById');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'userId');
    }

    public function twoFactorChallenges(): HasMany
    {
        return $this->hasMany(TwoFactorChallenge::class, 'userId');
    }
}