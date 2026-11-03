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

    protected $fillable = [
        'status',
        'planId',
        'priceId',
        'periodEnd',
        'cancelAtPeriodEnd',
        'customerId',
        'organisationId',
    ];

    protected $attributes = [
        'status' => 'INACTIVE',
        'cancelAtPeriodEnd' => false,
    ];

    protected $casts = [
        'status' => SubscriptionStatus::class,
        'periodEnd' => 'datetime',
        'cancelAtPeriodEnd' => 'boolean',
    ];

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }
}