<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProjectAlertChannel extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProjectAlertChannel';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'deduplicationKey',
        'userProvidedDeduplicationKey',
        'integrationId',
        'enabled',
        'type',
        'name',
        'properties',
        'alertTypes',
        'environmentTypes',
        'errorAlertConfig',
        'projectId',
    ];

    protected $attributes = [
        'userProvidedDeduplicationKey' => false,
        'enabled' => true,
    ];

    protected $casts = [
        'userProvidedDeduplicationKey' => 'boolean',
        'enabled' => 'boolean',
        'type' => ProjectAlertChannelType::class,
        'properties' => 'array',
        'errorAlertConfig' => 'array',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(OrganizationIntegration::class, 'integrationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function alerts(): HasMany
    {
        return $this->hasMany(ProjectAlert::class, 'channelId');
    }

    public function alertStorages(): HasMany
    {
        return $this->hasMany(ProjectAlertStorage::class, 'alertChannelId');
    }
}