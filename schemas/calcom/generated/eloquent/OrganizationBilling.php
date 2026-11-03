<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrganizationBilling extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganizationBilling';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'teamId',
        'subscriptionId',
        'subscriptionItemId',
        'customerId',
        'status',
        'planName',
        'subscriptionStart',
        'subscriptionTrialEnd',
        'subscriptionEnd',
        'billingPeriod',
        'billingMode',
        'pricePerSeat',
        'paidSeats',
        'highWaterMark',
        'highWaterMarkPeriodStart',
    ];

    protected $attributes = [
        'billingMode' => 'SEATS',
    ];

    protected $casts = [
        'teamId' => 'integer',
        'subscriptionStart' => 'datetime',
        'subscriptionTrialEnd' => 'datetime',
        'subscriptionEnd' => 'datetime',
        'billingPeriod' => BillingPeriod::class,
        'billingMode' => BillingMode::class,
        'pricePerSeat' => 'integer',
        'paidSeats' => 'integer',
        'highWaterMark' => 'integer',
        'highWaterMarkPeriodStart' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function monthlyProrations(): HasMany
    {
        return $this->hasMany(MonthlyProration::class, 'organizationBillingId');
    }

    public function seatChangeLogs(): HasMany
    {
        return $this->hasMany(SeatChangeLog::class, 'organizationBillingId');
    }
}