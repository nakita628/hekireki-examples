<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PlatformBilling extends Model
{
    protected $table = 'PlatformBilling';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'customerId',
        'subscriptionId',
        'priceId',
        'plan',
        'billingCycleStart',
        'billingCycleEnd',
        'overdue',
        'managerBillingId',
    ];

    protected $attributes = [
        'plan' => 'none',
        'overdue' => false,
    ];

    protected $casts = [
        'billingCycleStart' => 'integer',
        'billingCycleEnd' => 'integer',
        'overdue' => 'boolean',
        'managerBillingId' => 'integer',
    ];

    public function managerBilling(): BelongsTo
    {
        return $this->belongsTo(PlatformBilling::class, 'managerBillingId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'id');
    }

    public function managedBillings(): HasMany
    {
        return $this->hasMany(PlatformBilling::class, 'managerBillingId');
    }
}