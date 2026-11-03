<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WebhookScheduledTriggers extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'WebhookScheduledTriggers';

    protected $fillable = [
        'jobName',
        'subscriberUrl',
        'payload',
        'startAfter',
        'retryCount',
        'appId',
        'webhookId',
        'bookingId',
    ];

    protected $attributes = [
        'retryCount' => 0,
    ];

    protected $casts = [
        'startAfter' => 'datetime',
        'retryCount' => 'integer',
        'bookingId' => 'integer',
    ];

    public function webhook(): BelongsTo
    {
        return $this->belongsTo(Webhook::class, 'webhookId');
    }

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }
}