<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskIdentifier extends Model
{
    use PrismaDates;

    protected $table = 'TaskIdentifier';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'runtimeEnvironmentId',
        'projectId',
        'slug',
        'currentTriggerSource',
        'currentWorkerId',
        'firstSeenAt',
        'lastSeenAt',
        'isInLatestDeployment',
    ];

    protected $attributes = [
        'currentTriggerSource' => 'STANDARD',
        'isInLatestDeployment' => true,
    ];

    protected $casts = [
        'currentTriggerSource' => TaskTriggerSource::class,
        'firstSeenAt' => 'datetime',
        'lastSeenAt' => 'datetime',
        'isInLatestDeployment' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('firstSeenAt', $this->attributes)) {
                $this->setAttribute('firstSeenAt', $this->freshTimestamp());
            }
            if (! array_key_exists('lastSeenAt', $this->attributes)) {
                $this->setAttribute('lastSeenAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function currentWorker(): BelongsTo
    {
        return $this->belongsTo(BackgroundWorker::class, 'currentWorkerId');
    }
}