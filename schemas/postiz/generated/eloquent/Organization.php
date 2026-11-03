<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Organization extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Organization';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'description',
        'apiKey',
        'paymentId',
        'streakSince',
        'deletedAt',
        'allowTrial',
        'isTrailing',
        'shortlink',
    ];

    protected $attributes = [
        'allowTrial' => false,
        'isTrailing' => false,
        'shortlink' => 'ASK',
    ];

    protected $casts = [
        'streakSince' => 'datetime',
        'deletedAt' => 'datetime',
        'allowTrial' => 'boolean',
        'isTrailing' => 'boolean',
        'shortlink' => ShortLinkPreference::class,
    ];

    public function subscription(): HasOne
    {
        return $this->hasOne(Subscription::class, 'organizationId');
    }

    public function autoPost(): HasMany
    {
        return $this->hasMany(AutoPost::class, 'organizationId');
    }

    public function Comments(): HasMany
    {
        return $this->hasMany(Comments::class, 'organizationId');
    }

    public function credits(): HasMany
    {
        return $this->hasMany(Credits::class, 'organizationId');
    }

    public function clippings(): HasMany
    {
        return $this->hasMany(Clipping::class, 'organizationId');
    }

    public function customers(): HasMany
    {
        return $this->hasMany(Customer::class, 'orgId');
    }

    public function errors(): HasMany
    {
        return $this->hasMany(Errors::class, 'organizationId');
    }

    public function github(): HasMany
    {
        return $this->hasMany(GitHub::class, 'organizationId');
    }

    public function Integration(): HasMany
    {
        return $this->hasMany(Integration::class, 'organizationId');
    }

    public function media(): HasMany
    {
        return $this->hasMany(Media::class, 'organizationId');
    }

    public function buyerOrganization(): HasMany
    {
        return $this->hasMany(MessagesGroup::class, 'buyerOrganizationId');
    }

    public function notifications(): HasMany
    {
        return $this->hasMany(Notifications::class, 'organizationId');
    }

    public function plugs(): HasMany
    {
        return $this->hasMany(Plugs::class, 'organizationId');
    }

    public function post(): HasMany
    {
        return $this->hasMany(Post::class, 'organizationId');
    }

    public function submittedPost(): HasMany
    {
        return $this->hasMany(Post::class, 'submittedForOrganizationId');
    }

    public function sets(): HasMany
    {
        return $this->hasMany(Sets::class, 'organizationId');
    }

    public function signatures(): HasMany
    {
        return $this->hasMany(Signatures::class, 'organizationId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(Tags::class, 'orgId');
    }

    public function thirdParty(): HasMany
    {
        return $this->hasMany(ThirdParty::class, 'organizationId');
    }

    public function usedCodes(): HasMany
    {
        return $this->hasMany(UsedCodes::class, 'orgId');
    }

    public function users(): HasMany
    {
        return $this->hasMany(UserOrganization::class, 'organizationId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhooks::class, 'organizationId');
    }

    public function oauthApp(): HasMany
    {
        return $this->hasMany(OAuthApp::class, 'organizationId');
    }

    public function oauthAuthorizations(): HasMany
    {
        return $this->hasMany(OAuthAuthorization::class, 'organizationId');
    }
}