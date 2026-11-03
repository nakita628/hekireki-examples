<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkerInstanceGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkerInstanceGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'name',
        'masterQueue',
        'description',
        'hidden',
        'tokenId',
        'organizationId',
        'projectId',
        'cloudProvider',
        'location',
        'staticIPs',
        'workloadType',
        'region',
        'enableFastPath',
    ];

    protected $attributes = [
        'hidden' => false,
        'workloadType' => 'CONTAINER',
        'enableFastPath' => false,
    ];

    protected $casts = [
        'type' => WorkerInstanceGroupType::class,
        'hidden' => 'boolean',
        'workloadType' => WorkloadType::class,
        'enableFastPath' => 'boolean',
    ];

    public function token(): BelongsTo
    {
        return $this->belongsTo(WorkerGroupToken::class, 'tokenId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function workers(): HasMany
    {
        return $this->hasMany(WorkerInstance::class, 'workerGroupId');
    }

    public function backgroundWorkers(): HasMany
    {
        return $this->hasMany(BackgroundWorker::class, 'workerGroupId');
    }

    public function defaultForProjects(): HasMany
    {
        return $this->hasMany(Project::class, 'defaultWorkerGroupId');
    }
}