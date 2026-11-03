<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
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
        'accessToken',
        'authChallenge',
        'provider',
        'role',
        'thirdPartyId',
    ];

    protected $attributes = [
        'provider' => 'ANONYMOUS',
        'role' => 'USER',
    ];

    protected $casts = [
        'provider' => Provider::class,
        'role' => Role::class,
    ];

    public function analytics(): HasOne
    {
        return $this->hasOne(Analytics::class, 'userId');
    }

    public function settings(): HasOne
    {
        return $this->hasOne(Settings::class, 'userId');
    }

    public function accessesGet(): HasMany
    {
        return $this->hasMany(Access::class, 'granteeUserId');
    }

    public function accessesGive(): HasMany
    {
        return $this->hasMany(Access::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function activities(): HasMany
    {
        return $this->hasMany(Order::class, 'userId');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'userId');
    }

    public function authDevices(): HasMany
    {
        return $this->hasMany(AuthDevice::class, 'userId');
    }

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Subscription::class, 'userId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(Tag::class, 'userId');
    }

    public function SymbolProfile(): HasMany
    {
        return $this->hasMany(SymbolProfile::class, 'userId');
    }

    public function watchlist(): BelongsToMany
    {
        return $this->belongsToMany(SymbolProfile::class, '_UserWatchlist', 'B', 'A');
    }
}