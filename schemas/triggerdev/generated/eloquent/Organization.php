<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Organization extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Organization';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'title',
        'maximumExecutionTimePerRunInMs',
        'maximumConcurrencyLimit',
        'maximumSchedulesLimit',
        'maximumDevQueueSize',
        'maximumDeployedQueueSize',
        'deletedAt',
        'companySize',
        'onboardingData',
        'avatar',
        'runsEnabled',
        'v3Enabled',
        'v2Enabled',
        'v2MarqsEnabled',
        'hasRequestedV3',
        'apiRateLimiterConfig',
        'realtimeRateLimiterConfig',
        'batchRateLimitConfig',
        'batchQueueConcurrencyConfig',
        'webhookLimitsConfig',
        'featureFlags',
        'maximumProjectCount',
        'maxSessionDuration',
        'streamBasinName',
        'supportAccessMode',
        'supportAccessModeUpdatedAt',
        'supportAccessModeUpdatedById',
    ];

    protected $attributes = [
        'maximumExecutionTimePerRunInMs' => 900000,
        'maximumConcurrencyLimit' => 10,
        'maximumSchedulesLimit' => 5,
        'runsEnabled' => true,
        'v3Enabled' => false,
        'v2Enabled' => false,
        'v2MarqsEnabled' => false,
        'hasRequestedV3' => false,
        'maximumProjectCount' => 25,
        'supportAccessMode' => 'ALLOW',
    ];

    protected $casts = [
        'maximumExecutionTimePerRunInMs' => 'integer',
        'maximumConcurrencyLimit' => 'integer',
        'maximumSchedulesLimit' => 'integer',
        'maximumDevQueueSize' => 'integer',
        'maximumDeployedQueueSize' => 'integer',
        'deletedAt' => 'datetime',
        'onboardingData' => 'array',
        'avatar' => 'array',
        'runsEnabled' => 'boolean',
        'v3Enabled' => 'boolean',
        'v2Enabled' => 'boolean',
        'v2MarqsEnabled' => 'boolean',
        'hasRequestedV3' => 'boolean',
        'apiRateLimiterConfig' => 'array',
        'realtimeRateLimiterConfig' => 'array',
        'batchRateLimitConfig' => 'array',
        'batchQueueConcurrencyConfig' => 'array',
        'webhookLimitsConfig' => 'array',
        'featureFlags' => 'array',
        'maximumProjectCount' => 'integer',
        'maxSessionDuration' => 'integer',
        'supportAccessMode' => SupportAccessMode::class,
        'supportAccessModeUpdatedAt' => 'datetime',
    ];

    public function environments(): HasMany
    {
        return $this->hasMany(RuntimeEnvironment::class, 'organizationId');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(Project::class, 'organizationId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(OrgMember::class, 'organizationId');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(OrgMemberInvite::class, 'organizationId');
    }

    public function organizationIntegrations(): HasMany
    {
        return $this->hasMany(OrganizationIntegration::class, 'organizationId');
    }

    public function organizationAccessTokens(): HasMany
    {
        return $this->hasMany(OrganizationAccessToken::class, 'organizationId');
    }

    public function workerGroups(): HasMany
    {
        return $this->hasMany(WorkerInstanceGroup::class, 'organizationId');
    }

    public function workerInstances(): HasMany
    {
        return $this->hasMany(WorkerInstance::class, 'organizationId');
    }

    public function githubAppInstallations(): HasMany
    {
        return $this->hasMany(GithubAppInstallation::class, 'organizationId');
    }

    public function customerQueries(): HasMany
    {
        return $this->hasMany(CustomerQuery::class, 'organizationId');
    }

    public function metricsDashboards(): HasMany
    {
        return $this->hasMany(MetricsDashboard::class, 'organizationId');
    }

    public function prompts(): HasMany
    {
        return $this->hasMany(Prompt::class, 'organizationId');
    }

    public function platformNotifications(): HasMany
    {
        return $this->hasMany(PlatformNotification::class, 'organizationId');
    }

    public function errorGroupStates(): HasMany
    {
        return $this->hasMany(ErrorGroupState::class, 'organizationId');
    }

    public function supportAccessRequests(): HasMany
    {
        return $this->hasMany(SupportAccessRequest::class, 'organizationId');
    }
}