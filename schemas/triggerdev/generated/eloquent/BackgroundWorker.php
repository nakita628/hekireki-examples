<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class BackgroundWorker extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BackgroundWorker';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'engine',
        'contentHash',
        'sdkVersion',
        'cliVersion',
        'projectId',
        'runtimeEnvironmentId',
        'version',
        'metadata',
        'runtime',
        'runtimeVersion',
        'workerGroupId',
        'supportsLazyAttempts',
    ];

    protected $attributes = [
        'engine' => 'V1',
        'sdkVersion' => 'unknown',
        'cliVersion' => 'unknown',
        'supportsLazyAttempts' => false,
    ];

    protected $casts = [
        'engine' => RunEngineVersion::class,
        'metadata' => 'array',
        'supportsLazyAttempts' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function workerGroup(): BelongsTo
    {
        return $this->belongsTo(WorkerInstanceGroup::class, 'workerGroupId');
    }

    public function deployment(): HasOne
    {
        return $this->hasOne(WorkerDeployment::class, 'workerId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(BackgroundWorkerTask::class, 'workerId');
    }

    public function attempts(): HasMany
    {
        return $this->hasMany(TaskRunAttempt::class, 'backgroundWorkerId');
    }

    public function lockedRuns(): HasMany
    {
        return $this->hasMany(TaskRun::class, 'lockedToVersionId');
    }

    public function promptVersions(): HasMany
    {
        return $this->hasMany(PromptVersion::class, 'workerId');
    }

    public function taskIdentifiers(): HasMany
    {
        return $this->hasMany(TaskIdentifier::class, 'currentWorkerId');
    }

    public function files(): BelongsToMany
    {
        return $this->belongsToMany(BackgroundWorkerFile::class, '_BackgroundWorkerToBackgroundWorkerFile', 'A', 'B');
    }

    public function queues(): BelongsToMany
    {
        return $this->belongsToMany(TaskQueue::class, '_BackgroundWorkerToTaskQueue', 'A', 'B');
    }
}