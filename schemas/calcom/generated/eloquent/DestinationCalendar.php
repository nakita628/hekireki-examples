<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DestinationCalendar extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DestinationCalendar';

    protected $fillable = [
        'integration',
        'externalId',
        'primaryEmail',
        'userId',
        'eventTypeId',
        'credentialId',
        'delegationCredentialId',
        'customCalendarReminder',
    ];

    protected $casts = [
        'userId' => 'integer',
        'eventTypeId' => 'integer',
        'credentialId' => 'integer',
        'customCalendarReminder' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function credential(): BelongsTo
    {
        return $this->belongsTo(Credential::class, 'credentialId');
    }

    public function delegationCredential(): BelongsTo
    {
        return $this->belongsTo(DelegationCredential::class, 'delegationCredentialId');
    }

    public function booking(): HasMany
    {
        return $this->hasMany(Booking::class, 'destinationCalendarId');
    }
}