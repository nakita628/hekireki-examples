<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SpaceWebhook extends Model
{
    use PrismaDates;

    protected $table = 'space_webhooks';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'url',
        'secret',
        'events',
        'enabled',
        'version',
        'cursor',
        'last_delivered_at',
        'last_failed_at',
        'consecutive_failures',
    ];

    protected $attributes = [
        'enabled' => true,
        'consecutive_failures' => 0,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'cursor' => 'datetime',
        'last_delivered_at' => 'datetime',
        'last_failed_at' => 'datetime',
        'consecutive_failures' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('cursor', $this->attributes)) {
                $this->setAttribute('cursor', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function deliveries(): HasMany
    {
        return $this->hasMany(WebhookDelivery::class, 'webhook_id');
    }
}