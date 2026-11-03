<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Webhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Webhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'teamId',
        'eventTypeId',
        'platformOAuthClientId',
        'subscriberUrl',
        'payloadTemplate',
        'active',
        'eventTriggers',
        'appId',
        'secret',
        'platform',
        'time',
        'timeUnit',
        'version',
    ];

    protected $attributes = [
        'active' => true,
        'platform' => false,
        'version' => '2021-10-20',
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
        'eventTypeId' => 'integer',
        'active' => 'boolean',
        'platform' => 'boolean',
        'time' => 'integer',
        'timeUnit' => TimeUnit::class,
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function platformOAuthClient(): BelongsTo
    {
        return $this->belongsTo(PlatformOAuthClient::class, 'platformOAuthClientId');
    }

    public function app(): BelongsTo
    {
        return $this->belongsTo(App::class, 'appId');
    }

    public function scheduledTriggers(): HasMany
    {
        return $this->hasMany(WebhookScheduledTriggers::class, 'webhookId');
    }
}