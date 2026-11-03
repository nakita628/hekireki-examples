<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrganizationOnboarding extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganizationOnboarding';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'createdById',
        'orgOwnerEmail',
        'error',
        'organizationId',
        'billingPeriod',
        'pricePerSeat',
        'seats',
        'isPlatform',
        'name',
        'slug',
        'logo',
        'bio',
        'brandColor',
        'bannerUrl',
        'isDomainConfigured',
        'stripeCustomerId',
        'stripeSubscriptionId',
        'stripeSubscriptionItemId',
        'invitedMembers',
        'teams',
        'isComplete',
    ];

    protected $attributes = [
        'isPlatform' => false,
        'isDomainConfigured' => false,
        'invitedMembers' => '[]',
        'teams' => '[]',
        'isComplete' => false,
    ];

    protected $casts = [
        'createdById' => 'integer',
        'organizationId' => 'integer',
        'billingPeriod' => BillingPeriod::class,
        'pricePerSeat' => 'float',
        'seats' => 'integer',
        'isPlatform' => 'boolean',
        'isDomainConfigured' => 'boolean',
        'invitedMembers' => 'array',
        'teams' => 'array',
        'isComplete' => 'boolean',
    ];

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }
}