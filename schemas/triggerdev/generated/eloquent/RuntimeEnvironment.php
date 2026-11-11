<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class RuntimeEnvironment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RuntimeEnvironment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'apiKey',
        'rootApiKeyHiddenAt',
        'pkApiKey',
        'type',
        'isBranchableEnvironment',
        'branchName',
        'parentEnvironmentId',
        'previewAutoArchiveAfterDays',
        'previewAutoArchiveExcludedBranches',
        'previewAutoArchiveNextCheckAt',
        'previewAutoArchiveCursorCreatedAt',
        'previewAutoArchiveCursorId',
        'git',
        'archivedAt',
        'shortcode',
        'maximumConcurrencyLimit',
        'concurrencyLimitBurstFactor',
        'paused',
        'pauseSource',
        'autoEnableInternalSources',
        'organizationId',
        'projectId',
        'orgMemberId',
        'builtInEnvironmentVariableOverrides',
        'tunnelId',
        'currentSessionId',
    ];

    protected $attributes = [
        'type' => 'DEVELOPMENT',
        'isBranchableEnvironment' => false,
        'maximumConcurrencyLimit' => 5,
        'concurrencyLimitBurstFactor' => '2',
        'paused' => false,
        'autoEnableInternalSources' => true,
    ];

    protected $casts = [
        'rootApiKeyHiddenAt' => 'datetime',
        'type' => RuntimeEnvironmentType::class,
        'isBranchableEnvironment' => 'boolean',
        'previewAutoArchiveAfterDays' => 'integer',
        'previewAutoArchiveNextCheckAt' => 'datetime',
        'previewAutoArchiveCursorCreatedAt' => 'datetime',
        'git' => 'array',
        'archivedAt' => 'datetime',
        'maximumConcurrencyLimit' => 'integer',
        'concurrencyLimitBurstFactor' => AsDecimal::class,
        'paused' => 'boolean',
        'pauseSource' => EnvironmentPauseSource::class,
        'autoEnableInternalSources' => 'boolean',
        'builtInEnvironmentVariableOverrides' => 'array',
    ];

    public function parentEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'parentEnvironmentId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function orgMember(): BelongsTo
    {
        return $this->belongsTo(OrgMember::class, 'orgMemberId');
    }

    public function currentSession(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironmentSession::class, 'currentSessionId');
    }

    public function trustedDeployGrant(): HasOne
    {
        return $this->hasOne(TrustedDeployEnvironment::class, 'runtimeEnvironmentId');
    }

    public function childEnvironments(): HasMany
    {
        return $this->hasMany(RuntimeEnvironment::class, 'parentEnvironmentId');
    }

    public function backgroundWorkers(): HasMany
    {
        return $this->hasMany(BackgroundWorker::class, 'runtimeEnvironmentId');
    }

    public function backgroundWorkerTasks(): HasMany
    {
        return $this->hasMany(BackgroundWorkerTask::class, 'runtimeEnvironmentId');
    }

    public function taskRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'runtimeEnvironmentId');
    }

    public function taskQueues(): HasMany
    {
        return $this->hasMany(TaskQueue::class, 'runtimeEnvironmentId');
    }

    public function environmentVariableValues(): HasMany
    {
        return $this->hasMany(EnvironmentVariableValue::class, 'environmentId');
    }

    public function checkpoints(): HasMany
    {
        return $this->hasMany(Checkpoint::class, 'runtimeEnvironmentId');
    }

    public function workerDeployments(): HasMany
    {
        return $this->hasMany(WorkerDeployment::class, 'environmentId');
    }

    public function workerDeploymentPromotions(): HasMany
    {
        return $this->hasMany(WorkerDeploymentPromotion::class, 'environmentId');
    }

    public function taskRunAttempts(): HasMany
    {
        return $this->hasMany(TaskRunAttempt::class, 'runtimeEnvironmentId');
    }

    public function CheckpointRestoreEvent(): HasMany
    {
        return $this->hasMany(CheckpointRestoreEvent::class, 'runtimeEnvironmentId');
    }

    public function taskScheduleInstances(): HasMany
    {
        return $this->hasMany(TaskScheduleInstance::class, 'environmentId');
    }

    public function alerts(): HasMany
    {
        return $this->hasMany(ProjectAlert::class, 'environmentId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(RuntimeEnvironmentSession::class, 'environmentId');
    }

    public function taskRunNumberCounter(): HasMany
    {
        return $this->hasMany(TaskRunNumberCounter::class, 'environmentId');
    }

    public function taskRunCheckpoints(): HasMany
    {
        return $this->hasMany(TaskRunCheckpoint::class, 'runtimeEnvironmentId');
    }

    public function waitpoints(): HasMany
    {
        return $this->hasMany(Waitpoint::class, 'environmentId');
    }

    public function workerInstances(): HasMany
    {
        return $this->hasMany(WorkerInstance::class, 'environmentId');
    }

    public function waitpointTags(): HasMany
    {
        return $this->hasMany(WaitpointTag::class, 'environmentId');
    }

    public function BulkActionGroup(): HasMany
    {
        return $this->hasMany(BulkActionGroup::class, 'environmentId');
    }

    public function customerQueries(): HasMany
    {
        return $this->hasMany(CustomerQuery::class, 'environmentId');
    }

    public function prompts(): HasMany
    {
        return $this->hasMany(Prompt::class, 'runtimeEnvironmentId');
    }

    public function playgroundConversations(): HasMany
    {
        return $this->hasMany(PlaygroundConversation::class, 'runtimeEnvironmentId');
    }

    public function errorGroupStates(): HasMany
    {
        return $this->hasMany(ErrorGroupState::class, 'environmentId');
    }

    public function taskIdentifiers(): HasMany
    {
        return $this->hasMany(TaskIdentifier::class, 'runtimeEnvironmentId');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'runtime_environment_id');
    }

    public function revokedApiKeys(): HasMany
    {
        return $this->hasMany(RevokedApiKey::class, 'runtimeEnvironmentId');
    }
}