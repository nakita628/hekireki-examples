<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class WebhookEndpoint extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WebhookEndpoint';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'opaqueId',
        'organizationId',
        'projectId',
        'runtimeEnvironmentId',
        'environmentType',
        'endpointTenantId',
        'endpointExternalRef',
        'source',
        'declaredId',
        'routingTargets',
        'verifierArtifact',
        'metadata',
        'secretProvisioning',
        'setupPrompt',
        'signingSecretKey',
        'status',
        'manuallyDeactivatedAt',
    ];

    protected $attributes = [
        'endpointTenantId' => '',
        'endpointExternalRef' => '',
        'routingTargets' => '[]',
        'metadata' => '{}',
        'secretProvisioning' => 'either',
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'environmentType' => RuntimeEnvironmentType::class,
        'routingTargets' => 'array',
        'verifierArtifact' => 'array',
        'metadata' => 'array',
        'status' => WebhookEndpointStatus::class,
        'manuallyDeactivatedAt' => 'datetime',
    ];
}