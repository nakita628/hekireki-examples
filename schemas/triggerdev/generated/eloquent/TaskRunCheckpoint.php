<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TaskRunCheckpoint extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunCheckpoint';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'type',
        'location',
        'imageRef',
        'reason',
        'metadata',
        'projectId',
        'runtimeEnvironmentId',
    ];

    protected $casts = [
        'type' => TaskRunCheckpointType::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function executionSnapshot(): HasMany
    {
        return $this->hasMany(TaskRunExecutionSnapshot::class, 'checkpointId');
    }
}