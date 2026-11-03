<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraExperiments extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['startedAtZ', 'completedAtZ', 'createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_experiments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'metadata',
        'datasetId',
        'datasetVersion',
        'targetType',
        'targetId',
        'status',
        'totalItems',
        'succeededCount',
        'failedCount',
        'skippedCount',
        'startedAt',
        'completedAt',
        'agentVersion',
        'startedAtZ',
        'completedAtZ',
        'createdAtZ',
        'updatedAtZ',
        'organizationId',
        'projectId',
        'provenance',
        'runnerAttestation',
        'experimentSetId',
        'comparisonId',
        'variantId',
        'trialIndex',
        'scorerIds',
    ];

    protected $casts = [
        'metadata' => 'array',
        'datasetVersion' => 'integer',
        'totalItems' => 'integer',
        'succeededCount' => 'integer',
        'failedCount' => 'integer',
        'skippedCount' => 'integer',
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
        'startedAtZ' => 'datetime',
        'completedAtZ' => 'datetime',
        'createdAtZ' => 'datetime',
        'updatedAtZ' => 'datetime',
        'provenance' => 'array',
        'runnerAttestation' => 'array',
        'trialIndex' => 'integer',
        'scorerIds' => 'array',
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
            if (! array_key_exists('updatedAtZ', $this->attributes)) {
                $this->setAttribute('updatedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}