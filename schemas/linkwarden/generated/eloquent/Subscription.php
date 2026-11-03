<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Subscription extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Subscription';

    protected $fillable = [
        'active',
        'provider',
        'currentPeriodStart',
        'currentPeriodEnd',
        'quantity',
        'storeOriginalTransactionId',
        'storeProductId',
        'googlePurchaseToken',
        'storeMetadata',
        'stripeSubscriptionId',
        'userId',
    ];

    protected $attributes = [
        'quantity' => 1,
    ];

    protected $casts = [
        'active' => 'boolean',
        'provider' => SubscriptionProvider::class,
        'currentPeriodStart' => 'datetime',
        'currentPeriodEnd' => 'datetime',
        'quantity' => 'integer',
        'storeMetadata' => 'array',
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function childUsers(): HasMany
    {
        return $this->hasMany(User::class, 'parentSubscriptionId');
    }
}