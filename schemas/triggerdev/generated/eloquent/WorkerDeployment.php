<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkerDeployment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkerDeployment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'contentHash',
        'friendlyId',
        'shortCode',
        'version',
        'runtime',
        'runtimeVersion',
        'cliVersion',
        'imageReference',
        'imagePlatform',
        'templateKey',
        'indexObjectKey',
        'indexSha256',
        'indexFormatVersion',
        'externalBuildData',
        'buildServerMetadata',
        'buildEnvVars',
        'status',
        'type',
        'projectId',
        'environmentId',
        'workerId',
        'triggeredById',
        'triggeredVia',
        'commitSHA',
        'externalId',
        'startedAt',
        'installedAt',
        'builtAt',
        'deployedAt',
        'failedAt',
        'errorData',
        'canceledAt',
        'canceledReason',
        'git',
    ];

    protected $attributes = [
        'imagePlatform' => 'linux/amd64',
        'status' => 'PENDING',
        'type' => 'V1',
    ];

    protected $casts = [
        'indexFormatVersion' => 'integer',
        'externalBuildData' => 'array',
        'buildServerMetadata' => 'array',
        'buildEnvVars' => 'array',
        'status' => WorkerDeploymentStatus::class,
        'type' => WorkerDeploymentType::class,
        'startedAt' => 'datetime',
        'installedAt' => 'datetime',
        'builtAt' => 'datetime',
        'deployedAt' => 'datetime',
        'failedAt' => 'datetime',
        'errorData' => 'array',
        'canceledAt' => 'datetime',
        'git' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function worker(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorker::class, 'workerId');
    }

    public function triggeredBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'triggeredById');
    }

    public function promotions(): HasMany
    {
        return $this->hasMany(WorkerDeploymentPromotion::class, 'deploymentId');
    }

    public function alerts(): HasMany
    {
        return $this->hasMany(ProjectAlert::class, 'workerDeploymentId');
    }

    public function workerInstance(): HasMany
    {
        return $this->hasMany(WorkerInstance::class, 'deploymentId');
    }

    public function integrationDeployments(): HasMany
    {
        return $this->hasMany(IntegrationDeployment::class, 'deploymentId');
    }
}