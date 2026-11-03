<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Project extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Project';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'name',
        'externalRef',
        'organizationId',
        'deletedAt',
        'initializedAt',
        'defaultRuntime',
        'version',
        'engine',
        'builderProjectId',
        'defaultWorkerGroupId',
        'allowedMasterQueues',
        'buildSettings',
        'onboardingData',
    ];

    protected $attributes = [
        'version' => 'V2',
        'engine' => 'V1',
    ];

    protected $casts = [
        'deletedAt' => 'datetime',
        'initializedAt' => 'datetime',
        'version' => ProjectVersion::class,
        'engine' => RunEngineVersion::class,
        'buildSettings' => 'array',
        'onboardingData' => 'array',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function defaultWorkerGroup(): BelongsTo
    {
        return $this->belongsTo(WorkerInstanceGroup::class, 'defaultWorkerGroupId');
    }

    public function connectedGithubRepository(): HasOne
    {
        return $this->hasOne(ConnectedGithubRepository::class, 'projectId');
    }

    public function workerGroups(): HasMany
    {
        return $this->hasMany(WorkerInstanceGroup::class, 'projectId');
    }

    public function workers(): HasMany
    {
        return $this->hasMany(WorkerInstance::class, 'projectId');
    }

    public function environments(): HasMany
    {
        return $this->hasMany(RuntimeEnvironment::class, 'projectId');
    }

    public function backgroundWorkers(): HasMany
    {
        return $this->hasMany(BackgroundWorker::class, 'projectId');
    }

    public function backgroundWorkerTasks(): HasMany
    {
        return $this->hasMany(BackgroundWorkerTask::class, 'projectId');
    }

    public function taskRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'projectId');
    }

    public function runTags(): HasMany
    {
        return $this->hasMany(TaskRunTag::class, 'projectId');
    }

    public function taskQueues(): HasMany
    {
        return $this->hasMany(TaskQueue::class, 'projectId');
    }

    public function environmentVariables(): HasMany
    {
        return $this->hasMany(EnvironmentVariable::class, 'projectId');
    }

    public function checkpoints(): HasMany
    {
        return $this->hasMany(Checkpoint::class, 'projectId');
    }

    public function WorkerDeployment(): HasMany
    {
        return $this->hasMany(WorkerDeployment::class, 'projectId');
    }

    public function CheckpointRestoreEvent(): HasMany
    {
        return $this->hasMany(CheckpointRestoreEvent::class, 'projectId');
    }

    public function taskSchedules(): HasMany
    {
        return $this->hasMany(TaskSchedule::class, 'projectId');
    }

    public function alertChannels(): HasMany
    {
        return $this->hasMany(ProjectAlertChannel::class, 'projectId');
    }

    public function alerts(): HasMany
    {
        return $this->hasMany(ProjectAlert::class, 'projectId');
    }

    public function alertStorages(): HasMany
    {
        return $this->hasMany(ProjectAlertStorage::class, 'projectId');
    }

    public function bulkActionGroups(): HasMany
    {
        return $this->hasMany(BulkActionGroup::class, 'projectId');
    }

    public function BackgroundWorkerFile(): HasMany
    {
        return $this->hasMany(BackgroundWorkerFile::class, 'projectId');
    }

    public function waitpoints(): HasMany
    {
        return $this->hasMany(Waitpoint::class, 'projectId');
    }

    public function taskRunWaitpoints(): HasMany
    {
        return $this->hasMany(TaskRunWaitpoint::class, 'projectId');
    }

    public function taskRunCheckpoints(): HasMany
    {
        return $this->hasMany(TaskRunCheckpoint::class, 'projectId');
    }

    public function waitpointTags(): HasMany
    {
        return $this->hasMany(WaitpointTag::class, 'projectId');
    }

    public function organizationProjectIntegration(): HasMany
    {
        return $this->hasMany(OrganizationProjectIntegration::class, 'projectId');
    }

    public function customerQueries(): HasMany
    {
        return $this->hasMany(CustomerQuery::class, 'projectId');
    }

    public function playgroundConversations(): HasMany
    {
        return $this->hasMany(PlaygroundConversation::class, 'projectId');
    }

    public function taskScheduleInstances(): HasMany
    {
        return $this->hasMany(TaskScheduleInstance::class, 'projectId');
    }

    public function metricsDashboards(): HasMany
    {
        return $this->hasMany(MetricsDashboard::class, 'projectId');
    }

    public function llmModels(): HasMany
    {
        return $this->hasMany(LlmModel::class, 'project_id');
    }

    public function prompts(): HasMany
    {
        return $this->hasMany(Prompt::class, 'projectId');
    }

    public function platformNotifications(): HasMany
    {
        return $this->hasMany(PlatformNotification::class, 'projectId');
    }

    public function errorGroupStates(): HasMany
    {
        return $this->hasMany(ErrorGroupState::class, 'projectId');
    }

    public function taskIdentifiers(): HasMany
    {
        return $this->hasMany(TaskIdentifier::class, 'projectId');
    }
}