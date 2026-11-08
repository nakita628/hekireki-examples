<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Project extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'projects';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'public',
        'secret',
        'disabled',
        'disabledReason',
        'customer',
        'subscription',
        'cardVerification',
        'cardVerificationIntent',
        'cardVerificationAt',
        'cardVerificationSession',
        'cardVerificationPaymentMethod',
        'cardVerificationFingerprint',
        'cardVerificationAttempts',
        'billingLimitWorkflows',
        'billingLimitCampaigns',
        'billingLimitTransactional',
        'billingLimitInbound',
        'tracking',
        'language',
    ];

    protected $attributes = [
        'disabled' => false,
        'cardVerificationAttempts' => 0,
        'tracking' => 'ENABLED',
        'language' => 'en',
    ];

    protected $casts = [
        'disabled' => 'boolean',
        'disabledReason' => ProjectDisabledReason::class,
        'cardVerification' => CardVerificationStatus::class,
        'cardVerificationAt' => 'datetime',
        'cardVerificationAttempts' => 'integer',
        'billingLimitWorkflows' => 'integer',
        'billingLimitCampaigns' => 'integer',
        'billingLimitTransactional' => 'integer',
        'billingLimitInbound' => 'integer',
        'tracking' => TrackingMode::class,
    ];

    public function members(): HasMany
    {
        return $this->hasMany(Membership::class, 'projectId');
    }

    public function contacts(): HasMany
    {
        return $this->hasMany(Contact::class, 'projectId');
    }

    public function templates(): HasMany
    {
        return $this->hasMany(Template::class, 'projectId');
    }

    public function segments(): HasMany
    {
        return $this->hasMany(Segment::class, 'projectId');
    }

    public function workflows(): HasMany
    {
        return $this->hasMany(Workflow::class, 'projectId');
    }

    public function campaigns(): HasMany
    {
        return $this->hasMany(Campaign::class, 'projectId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(Email::class, 'projectId');
    }

    public function events(): HasMany
    {
        return $this->hasMany(Event::class, 'projectId');
    }

    public function domains(): HasMany
    {
        return $this->hasMany(Domain::class, 'projectId');
    }

    public function apiRequests(): HasMany
    {
        return $this->hasMany(ApiRequest::class, 'projectId');
    }

    public function idempotencyKeys(): HasMany
    {
        return $this->hasMany(IdempotencyKey::class, 'projectId');
    }

    public function throttleClaims(): HasMany
    {
        return $this->hasMany(ThrottleClaim::class, 'projectId');
    }
}