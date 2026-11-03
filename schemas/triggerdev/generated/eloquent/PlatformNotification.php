<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PlatformNotification extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PlatformNotification';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'title',
        'payload',
        'surface',
        'scope',
        'userId',
        'organizationId',
        'projectId',
        'startsAt',
        'endsAt',
        'cliMaxDaysAfterFirstSeen',
        'cliMaxShowCount',
        'cliShowEvery',
        'priority',
        'isDraft',
        'archivedAt',
    ];

    protected $attributes = [
        'priority' => 0,
        'isDraft' => false,
    ];

    protected $casts = [
        'payload' => 'array',
        'surface' => PlatformNotificationSurface::class,
        'scope' => PlatformNotificationScope::class,
        'startsAt' => 'datetime',
        'endsAt' => 'datetime',
        'cliMaxDaysAfterFirstSeen' => 'integer',
        'cliMaxShowCount' => 'integer',
        'cliShowEvery' => 'integer',
        'priority' => 'integer',
        'isDraft' => 'boolean',
        'archivedAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function interactions(): HasMany
    {
        return $this->hasMany(PlatformNotificationInteraction::class, 'notificationId');
    }
}