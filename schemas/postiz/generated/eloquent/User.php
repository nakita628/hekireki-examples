<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'email',
        'password',
        'providerName',
        'name',
        'lastName',
        'isSuperAdmin',
        'bio',
        'audience',
        'pictureId',
        'providerId',
        'timezone',
        'timezoneName',
        'lastReadNotifications',
        'inviteId',
        'activated',
        'account',
        'connectedAccount',
        'lastOnline',
        'ip',
        'agent',
        'deletedAt',
        'sendSuccessEmails',
        'sendFailureEmails',
        'sendStreakEmails',
    ];

    protected $attributes = [
        'isSuperAdmin' => false,
        'audience' => 0,
        'activated' => true,
        'connectedAccount' => false,
        'sendSuccessEmails' => true,
        'sendFailureEmails' => true,
        'sendStreakEmails' => true,
    ];

    protected $casts = [
        'providerName' => Provider::class,
        'isSuperAdmin' => 'boolean',
        'audience' => 'integer',
        'timezone' => 'integer',
        'lastReadNotifications' => 'datetime',
        'activated' => 'boolean',
        'connectedAccount' => 'boolean',
        'lastOnline' => 'datetime',
        'deletedAt' => 'datetime',
        'sendSuccessEmails' => 'boolean',
        'sendFailureEmails' => 'boolean',
        'sendStreakEmails' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastReadNotifications', $this->attributes)) {
                $this->setAttribute('lastReadNotifications', $this->freshTimestamp());
            }
            if (! array_key_exists('lastOnline', $this->attributes)) {
                $this->setAttribute('lastOnline', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function picture(): BelongsTo
    {
        return $this->belongsTo(Media::class, 'pictureId');
    }

    public function agencies(): HasOne
    {
        return $this->hasOne(SocialMediaAgency::class, 'userId');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comments::class, 'userId');
    }

    public function items(): HasMany
    {
        return $this->hasMany(ItemUser::class, 'userId');
    }

    public function groupBuyer(): HasMany
    {
        return $this->hasMany(MessagesGroup::class, 'buyerId');
    }

    public function groupSeller(): HasMany
    {
        return $this->hasMany(MessagesGroup::class, 'sellerId');
    }

    public function orderBuyer(): HasMany
    {
        return $this->hasMany(Orders::class, 'buyerId');
    }

    public function orderSeller(): HasMany
    {
        return $this->hasMany(Orders::class, 'sellerId');
    }

    public function payoutProblems(): HasMany
    {
        return $this->hasMany(PayoutProblems::class, 'userId');
    }

    public function organizations(): HasMany
    {
        return $this->hasMany(UserOrganization::class, 'userId');
    }

    public function oauthAuthorizations(): HasMany
    {
        return $this->hasMany(OAuthAuthorization::class, 'userId');
    }
}