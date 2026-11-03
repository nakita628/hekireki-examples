<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WebhookDelivery extends Model
{
    use PrismaDates;

    protected $table = 'webhook_deliveries';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'webhook_id',
        'activity_id',
        'event_type',
        'payload',
        'status',
        'attempts',
        'next_attempt_at',
        'last_response_status',
        'last_error',
    ];

    protected $attributes = [
        'status' => 'pending',
        'attempts' => 0,
    ];

    protected $casts = [
        'payload' => 'array',
        'status' => WebhookDeliveryStatus::class,
        'attempts' => 'integer',
        'next_attempt_at' => 'datetime',
        'last_response_status' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('next_attempt_at', $this->attributes)) {
                $this->setAttribute('next_attempt_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function webhook(): BelongsTo
    {
        return $this->belongsTo(SpaceWebhook::class, 'webhook_id');
    }
}