<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Payment extends Model
{
    protected $table = 'Payment';

    public $timestamps = false;

    protected $fillable = [
        'uid',
        'appId',
        'bookingId',
        'amount',
        'fee',
        'currency',
        'success',
        'refunded',
        'data',
        'externalId',
        'paymentOption',
    ];

    protected $attributes = [
        'paymentOption' => 'ON_BOOKING',
    ];

    protected $casts = [
        'bookingId' => 'integer',
        'amount' => 'integer',
        'fee' => 'integer',
        'success' => 'boolean',
        'refunded' => 'boolean',
        'data' => 'array',
        'paymentOption' => PaymentOption::class,
    ];

    public function app(): BelongsTo
    {
        return $this->belongsTo(App::class, 'appId');
    }

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }
}