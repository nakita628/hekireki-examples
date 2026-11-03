<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SelectedCalendar extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SelectedCalendar';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'integration',
        'externalId',
        'credentialId',
        'googleChannelId',
        'googleChannelKind',
        'googleChannelResourceId',
        'googleChannelResourceUri',
        'googleChannelExpiration',
        'channelId',
        'channelKind',
        'channelResourceId',
        'channelResourceUri',
        'channelExpiration',
        'syncSubscribedAt',
        'syncSubscribedErrorAt',
        'syncSubscribedErrorCount',
        'syncToken',
        'syncedAt',
        'syncErrorAt',
        'syncErrorCount',
        'delegationCredentialId',
        'error',
        'lastErrorAt',
        'watchAttempts',
        'unwatchAttempts',
        'maxAttempts',
        'eventTypeId',
    ];

    protected $attributes = [
        'syncSubscribedErrorCount' => 0,
        'syncErrorCount' => 0,
        'watchAttempts' => 0,
        'unwatchAttempts' => 0,
        'maxAttempts' => 3,
    ];

    protected $casts = [
        'userId' => 'integer',
        'credentialId' => 'integer',
        'channelExpiration' => 'datetime',
        'syncSubscribedAt' => 'datetime',
        'syncSubscribedErrorAt' => 'datetime',
        'syncSubscribedErrorCount' => 'integer',
        'syncedAt' => 'datetime',
        'syncErrorAt' => 'datetime',
        'syncErrorCount' => 'integer',
        'lastErrorAt' => 'datetime',
        'watchAttempts' => 'integer',
        'unwatchAttempts' => 'integer',
        'maxAttempts' => 'integer',
        'eventTypeId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function credential(): BelongsTo
    {
        return $this->belongsTo(Credential::class, 'credentialId');
    }

    public function delegationCredential(): BelongsTo
    {
        return $this->belongsTo(DelegationCredential::class, 'delegationCredentialId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function calendarCacheEvents(): HasMany
    {
        return $this->hasMany(CalendarCacheEvent::class, 'selectedCalendarId');
    }
}