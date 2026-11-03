<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Subscription extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Subscription';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organizationId',
        'subscriptionTier',
        'provider',
        'identifier',
        'cancelAt',
        'period',
        'totalChannels',
        'isLifetime',
        'deletedAt',
    ];

    protected $attributes = [
        'provider' => 'stripe',
        'isLifetime' => false,
    ];

    protected $casts = [
        'subscriptionTier' => SubscriptionTier::class,
        'cancelAt' => 'datetime',
        'period' => Period::class,
        'totalChannels' => 'integer',
        'isLifetime' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}