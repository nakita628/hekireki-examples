<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PlanProviderMapping extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'plan_provider_mappings';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'planId',
        'provider',
        'externalId',
        'region',
        'metadata',
        'isActive',
    ];

    protected $attributes = [
        'isActive' => true,
    ];

    protected $casts = [
        'provider' => PaymentProcessor::class,
        'metadata' => 'array',
        'isActive' => 'boolean',
    ];

    public function plan(): BelongsTo
    {
        return $this->belongsTo(SubscriptionPlan::class, 'planId');
    }
}