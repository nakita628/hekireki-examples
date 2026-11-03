<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DelegationCredential extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DelegationCredential';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'workspacePlatformId',
        'serviceAccountKey',
        'enabled',
        'lastEnabledAt',
        'lastDisabledAt',
        'organizationId',
        'domain',
    ];

    protected $attributes = [
        'enabled' => false,
    ];

    protected $casts = [
        'workspacePlatformId' => 'integer',
        'serviceAccountKey' => 'array',
        'enabled' => 'boolean',
        'lastEnabledAt' => 'datetime',
        'lastDisabledAt' => 'datetime',
        'organizationId' => 'integer',
    ];

    public function workspacePlatform(): BelongsTo
    {
        return $this->belongsTo(WorkspacePlatform::class, 'workspacePlatformId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function selectedCalendars(): HasMany
    {
        return $this->hasMany(SelectedCalendar::class, 'delegationCredentialId');
    }

    public function destinationCalendar(): HasMany
    {
        return $this->hasMany(DestinationCalendar::class, 'delegationCredentialId');
    }

    public function bookingReferences(): HasMany
    {
        return $this->hasMany(BookingReference::class, 'delegationCredentialId');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(Credential::class, 'delegationCredentialId');
    }
}