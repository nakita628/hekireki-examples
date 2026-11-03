<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Credential extends Model
{
    protected $table = 'Credential';

    public $timestamps = false;

    protected $fillable = [
        'type',
        'key',
        'encryptedKey',
        'userId',
        'teamId',
        'appId',
        'subscriptionId',
        'paymentStatus',
        'billingCycleStart',
        'invalid',
        'delegationCredentialId',
    ];

    protected $attributes = [
        'invalid' => false,
    ];

    protected $casts = [
        'key' => 'array',
        'userId' => 'integer',
        'teamId' => 'integer',
        'billingCycleStart' => 'integer',
        'invalid' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function app(): BelongsTo
    {
        return $this->belongsTo(App::class, 'appId');
    }

    public function delegationCredential(): BelongsTo
    {
        return $this->belongsTo(DelegationCredential::class, 'delegationCredentialId');
    }

    public function destinationCalendars(): HasMany
    {
        return $this->hasMany(DestinationCalendar::class, 'credentialId');
    }

    public function selectedCalendars(): HasMany
    {
        return $this->hasMany(SelectedCalendar::class, 'credentialId');
    }

    public function CalendarCache(): HasMany
    {
        return $this->hasMany(CalendarCache::class, 'credentialId');
    }

    public function references(): HasMany
    {
        return $this->hasMany(BookingReference::class, 'credentialId');
    }

    public function integrationAttributeSyncs(): HasMany
    {
        return $this->hasMany(IntegrationAttributeSync::class, 'credentialId');
    }

    public function hostLocations(): HasMany
    {
        return $this->hasMany(HostLocation::class, 'credentialId');
    }
}