<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Organization extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'organizations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'slug',
        'createdByUserId',
        'timezone',
        'onboarding',
        'attribution',
        'subscriptionId',
        'subscriptionCustomerId',
        'subscriptionPriceId',
        'subscriptionProductId',
        'subscriptionStatus',
        'subscriptionStartsAt',
        'subscriptionEndsAt',
        'subscriptionCanceledAt',
        'subscriptionCreatedByUserId',
        'subscriptionPeriodEventsCount',
        'subscriptionPeriodEventsCountExceededAt',
        'subscriptionPeriodEventsLimit',
        'subscriptionInterval',
        'subscriptionCancelReason',
        'subscriptionCancelComment',
        'subscriptionSaveDiscountAppliedAt',
        'subscriptionPauseAtPeriodEnd',
        'subscriptionResumesAt',
        'subscriptionFirstStartedAt',
        'usageWarningSentAt',
        'usageExceededSentAt',
        'subscriptionDiscount',
        'windDownStartedAt',
        'windDownStep',
        'deleteAt',
    ];

    protected $attributes = [
        'onboarding' => 'completed',
        'subscriptionPeriodEventsCount' => 0,
        'subscriptionPeriodEventsLimit' => 0,
        'subscriptionPauseAtPeriodEnd' => false,
    ];

    protected $casts = [
        'attribution' => 'array',
        'subscriptionStartsAt' => 'datetime',
        'subscriptionEndsAt' => 'datetime',
        'subscriptionCanceledAt' => 'datetime',
        'subscriptionPeriodEventsCount' => 'integer',
        'subscriptionPeriodEventsCountExceededAt' => 'datetime',
        'subscriptionPeriodEventsLimit' => 'integer',
        'subscriptionSaveDiscountAppliedAt' => 'datetime',
        'subscriptionPauseAtPeriodEnd' => 'boolean',
        'subscriptionResumesAt' => 'datetime',
        'subscriptionFirstStartedAt' => 'datetime',
        'usageWarningSentAt' => 'datetime',
        'usageExceededSentAt' => 'datetime',
        'subscriptionDiscount' => 'array',
        'windDownStartedAt' => 'datetime',
        'deleteAt' => 'datetime',
    ];

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdByUserId');
    }

    public function subscriptionCreatedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'subscriptionCreatedByUserId');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(Project::class, 'organizationId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(Member::class, 'organizationId');
    }

    public function Client(): HasMany
    {
        return $this->hasMany(Client::class, 'organizationId');
    }

    public function Dashboard(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'organizationId');
    }

    public function ShareOverview(): HasMany
    {
        return $this->hasMany(ShareOverview::class, 'organizationId');
    }

    public function ShareDashboard(): HasMany
    {
        return $this->hasMany(ShareDashboard::class, 'organizationId');
    }

    public function ShareReport(): HasMany
    {
        return $this->hasMany(ShareReport::class, 'organizationId');
    }

    public function ShareWidget(): HasMany
    {
        return $this->hasMany(ShareWidget::class, 'organizationId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(Integration::class, 'organizationId');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(Invite::class, 'organizationId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'organizationId');
    }
}