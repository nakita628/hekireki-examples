<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Space extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'spaces';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'image',
        'owner_id',
        'tier',
        'space_type',
        'industry',
        'primary_color',
        'show_branding',
        'hide_attribution',
        'shared',
        'nonprofit_discount_granted_at',
        'api_poll_quota_override',
    ];

    protected $attributes = [
        'tier' => 'hobby',
        'show_branding' => false,
        'hide_attribution' => false,
        'shared' => false,
    ];

    protected $casts = [
        'tier' => SpaceTier::class,
        'space_type' => SpaceType::class,
        'show_branding' => 'boolean',
        'hide_attribution' => 'boolean',
        'shared' => 'boolean',
        'nonprofit_discount_granted_at' => 'datetime',
        'api_poll_quota_override' => 'integer',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'owner_id');
    }

    public function polls(): HasMany
    {
        return $this->hasMany(Poll::class, 'space_id');
    }

    public function scheduledEvents(): HasMany
    {
        return $this->hasMany(ScheduledEvent::class, 'space_id');
    }

    public function eventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'space_id');
    }

    public function sheets(): HasMany
    {
        return $this->hasMany(Sheet::class, 'space_id');
    }

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Subscription::class, 'space_id');
    }

    public function members(): HasMany
    {
        return $this->hasMany(SpaceMember::class, 'space_id');
    }

    public function memberInvites(): HasMany
    {
        return $this->hasMany(SpaceMemberInvite::class, 'space_id');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(SpaceApiKey::class, 'space_id');
    }

    public function nonprofitApplications(): HasMany
    {
        return $this->hasMany(NonprofitApplication::class, 'space_id');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(SpaceWebhook::class, 'space_id');
    }

    public function apiUsage(): HasMany
    {
        return $this->hasMany(ApiUsage::class, 'space_id');
    }
}