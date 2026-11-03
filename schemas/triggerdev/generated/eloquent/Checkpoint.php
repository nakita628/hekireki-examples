<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Checkpoint extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Checkpoint';

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
        'runId',
        'attemptId',
        'attemptNumber',
        'projectId',
        'runtimeEnvironmentId',
    ];

    protected $casts = [
        'type' => CheckpointType::class,
        'attemptNumber' => 'integer',
    ];

    public function run(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'runId');
    }

    public function attempt(): BelongsTo
    {
        return $this->belongsTo(TaskRunAttempt::class, 'attemptId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function events(): HasMany
    {
        return $this->hasMany(CheckpointRestoreEvent::class, 'checkpointId');
    }
}