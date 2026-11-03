<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ProjectAlert extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProjectAlert';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'projectId',
        'environmentId',
        'channelId',
        'status',
        'type',
        'taskRunAttemptId',
        'taskRunId',
        'workerDeploymentId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => ProjectAlertStatus::class,
        'type' => ProjectAlertType::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function channel(): BelongsTo
    {
        return $this->belongsTo(ProjectAlertChannel::class, 'channelId');
    }

    public function workerDeployment(): BelongsTo
    {
        return $this->belongsTo(WorkerDeployment::class, 'workerDeploymentId');
    }
}