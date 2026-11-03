<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class MonthlyProration extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'MonthlyProration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'teamId',
        'monthKey',
        'periodStart',
        'periodEnd',
        'seatsAtStart',
        'seatsAdded',
        'seatsRemoved',
        'netSeatIncrease',
        'seatsAtEnd',
        'subscriptionId',
        'subscriptionItemId',
        'customerId',
        'subscriptionStart',
        'subscriptionEnd',
        'remainingDays',
        'pricePerSeat',
        'proratedAmount',
        'invoiceItemId',
        'invoiceId',
        'invoiceUrl',
        'status',
        'chargedAt',
        'failedAt',
        'failureReason',
        'retryCount',
        'metadata',
        'teamBillingId',
        'organizationBillingId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'retryCount' => 0,
    ];

    protected $casts = [
        'teamId' => 'integer',
        'periodStart' => 'datetime',
        'periodEnd' => 'datetime',
        'seatsAtStart' => 'integer',
        'seatsAdded' => 'integer',
        'seatsRemoved' => 'integer',
        'netSeatIncrease' => 'integer',
        'seatsAtEnd' => 'integer',
        'subscriptionStart' => 'datetime',
        'subscriptionEnd' => 'datetime',
        'remainingDays' => 'integer',
        'pricePerSeat' => 'integer',
        'proratedAmount' => 'integer',
        'status' => ProrationStatus::class,
        'chargedAt' => 'datetime',
        'failedAt' => 'datetime',
        'retryCount' => 'integer',
        'metadata' => 'array',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function teamBilling(): BelongsTo
    {
        return $this->belongsTo(TeamBilling::class, 'teamBillingId');
    }

    public function organizationBilling(): BelongsTo
    {
        return $this->belongsTo(OrganizationBilling::class, 'organizationBillingId');
    }

    public function seatChanges(): HasMany
    {
        return $this->hasMany(SeatChangeLog::class, 'processedInProrationId');
    }
}