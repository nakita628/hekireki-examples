<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class BackgroundJob extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BackgroundJob';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'status',
        'payload',
        'retried',
        'maxRetries',
        'jobId',
        'name',
        'version',
        'submittedAt',
        'completedAt',
        'lastRetriedAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'retried' => 0,
        'maxRetries' => 3,
    ];

    protected $casts = [
        'status' => BackgroundJobStatus::class,
        'payload' => 'array',
        'retried' => 'integer',
        'maxRetries' => 'integer',
        'submittedAt' => 'datetime',
        'completedAt' => 'datetime',
        'lastRetriedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('submittedAt', $this->attributes)) {
                $this->setAttribute('submittedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(BackgroundJobTask::class, 'jobId');
    }
}