<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WebhookCall extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'WebhookCall';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'status',
        'url',
        'event',
        'requestBody',
        'responseCode',
        'responseHeaders',
        'responseBody',
        'webhookId',
    ];

    protected $casts = [
        'status' => WebhookCallStatus::class,
        'event' => WebhookTriggerEvents::class,
        'requestBody' => 'array',
        'responseCode' => 'integer',
        'responseHeaders' => 'array',
        'responseBody' => 'array',
    ];

    public function webhook(): BelongsTo
    {
        return $this->belongsTo(Webhook::class, 'webhookId');
    }
}