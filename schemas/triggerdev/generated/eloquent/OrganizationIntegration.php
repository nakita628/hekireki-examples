<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrganizationIntegration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganizationIntegration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'service',
        'externalOrganizationId',
        'integrationData',
        'tokenReferenceId',
        'organizationId',
        'deletedAt',
    ];

    protected $casts = [
        'service' => IntegrationService::class,
        'integrationData' => 'array',
        'deletedAt' => 'datetime',
    ];

    public function tokenReference(): BelongsTo
    {
        return $this->belongsTo(SecretReference::class, 'tokenReferenceId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function alertChannels(): HasMany
    {
        return $this->hasMany(ProjectAlertChannel::class, 'integrationId');
    }

    public function organizationProjectIntegration(): HasMany
    {
        return $this->hasMany(OrganizationProjectIntegration::class, 'organizationIntegrationId');
    }
}