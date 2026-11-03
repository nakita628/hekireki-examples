<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Session extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Session';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'externalId',
        'type',
        'projectId',
        'runtimeEnvironmentId',
        'environmentType',
        'organizationId',
        'taskIdentifier',
        'triggerConfig',
        'isTest',
        'tags',
        'metadata',
        'currentRunId',
        'currentRunVersion',
        'closedAt',
        'closedReason',
        'expiresAt',
        'streamBasinName',
        'chatSnapshotStoragePath',
    ];

    protected $attributes = [
        'isTest' => false,
        'currentRunVersion' => 0,
    ];

    protected $casts = [
        'environmentType' => RuntimeEnvironmentType::class,
        'triggerConfig' => 'array',
        'isTest' => 'boolean',
        'metadata' => 'array',
        'currentRunVersion' => 'integer',
        'closedAt' => 'datetime',
        'expiresAt' => 'datetime',
    ];

    public function runs(): HasMany
    {
        return $this->hasMany(SessionRun::class, 'sessionId');
    }
}