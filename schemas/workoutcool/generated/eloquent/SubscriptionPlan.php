<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SubscriptionPlan extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'subscription_plans';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'priceMonthly',
        'priceYearly',
        'currency',
        'interval',
        'isActive',
        'availableRegions',
    ];

    protected $attributes = [
        'currency' => 'EUR',
        'interval' => 'month',
        'isActive' => true,
    ];

    protected $casts = [
        'priceMonthly' => AsDecimal::class,
        'priceYearly' => AsDecimal::class,
        'isActive' => 'boolean',
    ];

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Subscription::class, 'planId');
    }

    public function providerMappings(): HasMany
    {
        return $this->hasMany(PlanProviderMapping::class, 'planId');
    }
}