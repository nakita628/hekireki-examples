<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraExperimentResults extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['startedAtZ', 'completedAtZ', 'createdAtZ'];

    protected $table = 'mastra_experiment_results';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'experimentId',
        'itemId',
        'itemDatasetVersion',
        'input',
        'output',
        'groundTruth',
        'error',
        'startedAt',
        'completedAt',
        'retryCount',
        'traceId',
        'status',
        'tags',
        'startedAtZ',
        'completedAtZ',
        'createdAtZ',
        'comment',
        'toolMockReport',
        'metadata',
        'organizationId',
        'projectId',
        'attempt',
    ];

    protected $casts = [
        'itemDatasetVersion' => 'integer',
        'input' => 'array',
        'output' => 'array',
        'groundTruth' => 'array',
        'error' => 'array',
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
        'retryCount' => 'integer',
        'tags' => 'array',
        'startedAtZ' => 'datetime',
        'completedAtZ' => 'datetime',
        'createdAtZ' => 'datetime',
        'toolMockReport' => 'array',
        'metadata' => 'array',
        'attempt' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('startedAtZ', $this->attributes)) {
                $this->setAttribute('startedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('completedAtZ', $this->attributes)) {
                $this->setAttribute('completedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}