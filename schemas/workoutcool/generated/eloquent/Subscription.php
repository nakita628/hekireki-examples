<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Subscription extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'subscriptions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'planId',
        'revenueCatUserId',
        'status',
        'startedAt',
        'currentPeriodEnd',
        'cancelledAt',
        'platform',
    ];

    protected $casts = [
        'status' => SubscriptionStatus::class,
        'startedAt' => 'datetime',
        'currentPeriodEnd' => 'datetime',
        'cancelledAt' => 'datetime',
        'platform' => Platform::class,
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function plan(): BelongsTo
    {
        return $this->belongsTo(SubscriptionPlan::class, 'planId');
    }
}