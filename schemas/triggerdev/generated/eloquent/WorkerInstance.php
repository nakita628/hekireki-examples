<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkerInstance extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkerInstance';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'resourceIdentifier',
        'metadata',
        'workerGroupId',
        'organizationId',
        'projectId',
        'environmentId',
        'deploymentId',
        'lastDequeueAt',
        'lastHeartbeatAt',
    ];

    protected $casts = [
        'metadata' => 'array',
        'lastDequeueAt' => 'datetime',
        'lastHeartbeatAt' => 'datetime',
    ];

    public function workerGroup(): BelongsTo
    {
        return $this->belongsTo(WorkerInstanceGroup::class, 'workerGroupId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function deployment(): BelongsTo
    {
        return $this->belongsTo(WorkerDeployment::class, 'deploymentId');
    }
}