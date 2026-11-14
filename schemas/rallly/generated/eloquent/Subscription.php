<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Subscription extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'subscriptions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'price_id',
        'quantity',
        'subscription_item_id',
        'amount',
        'discount_percent_off',
        'discount_amount_off',
        'status',
        'active',
        'currency',
        'interval',
        'period_start',
        'period_end',
        'cancel_at_period_end',
        'user_id',
        'space_id',
        'api_addon_item_id',
        'api_addon_price_id',
        'api_addon_amount',
        'api_addon_tier',
        'api_poll_retained_tier',
        'api_poll_retained_until',
    ];

    protected $attributes = [
        'quantity' => 1,
        'cancel_at_period_end' => false,
    ];

    protected $casts = [
        'quantity' => 'integer',
        'amount' => 'integer',
        'discount_percent_off' => 'float',
        'discount_amount_off' => 'integer',
        'status' => SubscriptionStatus::class,
        'active' => 'boolean',
        'interval' => SubscriptionInterval::class,
        'period_start' => 'datetime',
        'period_end' => 'datetime',
        'cancel_at_period_end' => 'boolean',
        'api_addon_amount' => 'integer',
        'api_poll_retained_until' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }
}