<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['createdOn', 'lastLoggedOn', 'lastActiveOn'];

    protected $table = 'User';

    protected $primaryKey = 'uid';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'uid',
        'displayName',
        'email',
        'photoURL',
        'isAdmin',
        'refreshToken',
        'currentRESTSession',
        'currentGQLSession',
        'createdOn',
        'lastLoggedOn',
        'lastActiveOn',
    ];

    protected $attributes = [
        'isAdmin' => false,
    ];

    protected $casts = [
        'isAdmin' => 'boolean',
        'currentRESTSession' => 'array',
        'currentGQLSession' => 'array',
        'createdOn' => 'datetime',
        'lastLoggedOn' => 'datetime',
        'lastActiveOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function settings(): HasOne
    {
        return $this->hasOne(UserSettings::class, 'userUid');
    }

    public function providerAccounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function invitedUsers(): HasMany
    {
        return $this->hasMany(InvitedUsers::class, 'adminUid');
    }

    public function mockServers(): HasMany
    {
        return $this->hasMany(MockServer::class, 'creatorUid');
    }

    public function personalAccessTokens(): HasMany
    {
        return $this->hasMany(PersonalAccessToken::class, 'userUid');
    }

    public function shortcodes(): HasMany
    {
        return $this->hasMany(Shortcode::class, 'creatorUid');
    }

    public function userCollections(): HasMany
    {
        return $this->hasMany(UserCollection::class, 'userUid');
    }

    public function environments(): HasMany
    {
        return $this->hasMany(UserEnvironment::class, 'userUid');
    }

    public function history(): HasMany
    {
        return $this->hasMany(UserHistory::class, 'userUid');
    }

    public function userRequests(): HasMany
    {
        return $this->hasMany(UserRequest::class, 'userUid');
    }

    public function verificationTokens(): HasMany
    {
        return $this->hasMany(VerificationToken::class, 'userUid');
    }
}