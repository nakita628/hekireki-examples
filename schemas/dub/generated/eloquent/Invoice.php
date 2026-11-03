<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Invoice extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Invoice';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workspaceId',
        'number',
        'status',
        'type',
        'paymentMethod',
        'amount',
        'fee',
        'refundedAmount',
        'total',
        'receiptUrl',
        'refundReason',
        'failedReason',
        'registeredDomains',
        'stripeChargeMetadata',
        'failedAttempts',
        'paidAt',
    ];

    protected $attributes = [
        'status' => 'processing',
        'type' => 'domainRenewal',
        'amount' => 0,
        'fee' => 0,
        'refundedAmount' => 0,
        'total' => 0,
        'failedAttempts' => 0,
    ];

    protected $casts = [
        'status' => InvoiceStatus::class,
        'type' => InvoiceType::class,
        'paymentMethod' => PaymentMethod::class,
        'amount' => 'integer',
        'fee' => 'integer',
        'refundedAmount' => 'integer',
        'total' => 'integer',
        'registeredDomains' => 'array',
        'stripeChargeMetadata' => 'array',
        'failedAttempts' => 'integer',
        'paidAt' => 'datetime',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'workspaceId');
    }
}