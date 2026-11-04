<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Organization extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'organizations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'cloud_config',
        'metadata',
        'cloud_billing_cycle_anchor',
        'cloud_billing_cycle_updated_at',
        'cloud_current_cycle_usage',
        'cloud_free_tier_usage_threshold_state',
        'ai_features_enabled',
        'ai_telemetry_enabled',
        'access_audit_log_enabled',
        'feature_flag_org_defaults',
        'sfdc_org_id',
    ];

    protected $attributes = [
        'ai_features_enabled' => false,
        'ai_telemetry_enabled' => true,
        'access_audit_log_enabled' => false,
    ];

    protected $casts = [
        'cloud_config' => 'array',
        'metadata' => 'array',
        'cloud_billing_cycle_anchor' => 'datetime',
        'cloud_billing_cycle_updated_at' => 'datetime',
        'cloud_current_cycle_usage' => 'integer',
        'ai_features_enabled' => 'boolean',
        'ai_telemetry_enabled' => 'boolean',
        'access_audit_log_enabled' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('cloud_billing_cycle_anchor', $this->attributes)) {
                $this->setAttribute('cloud_billing_cycle_anchor', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function gatewayConfig(): HasOne
    {
        return $this->hasOne(GatewayConfig::class, 'organization_id');
    }

    public function organizationMemberships(): HasMany
    {
        return $this->hasMany(OrganizationMembership::class, 'org_id');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(Project::class, 'org_id');
    }

    public function MembershipInvitation(): HasMany
    {
        return $this->hasMany(MembershipInvitation::class, 'org_id');
    }

    public function ApiKey(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'organization_id');
    }

    public function surveys(): HasMany
    {
        return $this->hasMany(Survey::class, 'org_id');
    }

    public function cloudSpendAlerts(): HasMany
    {
        return $this->hasMany(CloudSpendAlert::class, 'org_id');
    }

    public function verifiedDomains(): HasMany
    {
        return $this->hasMany(VerifiedDomain::class, 'organization_id');
    }

    public function gatewayAiConnections(): HasMany
    {
        return $this->hasMany(GatewayAiConnection::class, 'organization_id');
    }

    public function organizationLlmApiKeys(): HasMany
    {
        return $this->hasMany(LlmApiKeys::class, 'organization_id');
    }

    public function roleAssignments(): HasMany
    {
        return $this->hasMany(RoleAssignment::class, 'org_id');
    }
}