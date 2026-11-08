<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RevenueCatWebhookEvent extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'revenuecat_webhook_events';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'eventType',
        'eventTimestamp',
        'appUserId',
        'environment',
        'productId',
        'transactionId',
        'originalTransactionId',
        'entitlementIds',
        'processed',
        'processingError',
        'retryCount',
        'rawEventData',
    ];

    protected $attributes = [
        'processed' => false,
        'retryCount' => 0,
    ];

    protected $casts = [
        'eventTimestamp' => 'datetime',
        'processed' => 'boolean',
        'retryCount' => 'integer',
        'rawEventData' => 'array',
    ];
}