<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraScorers extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_scorers';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'scorerId',
        'traceId',
        'runId',
        'scorer',
        'preprocessStepResult',
        'extractStepResult',
        'analyzeStepResult',
        'score',
        'reason',
        'metadata',
        'preprocessPrompt',
        'extractPrompt',
        'generateScorePrompt',
        'generateReasonPrompt',
        'analyzePrompt',
        'reasonPrompt',
        'input',
        'output',
        'additionalContext',
        'runtimeContext',
        'entityType',
        'entity',
        'entityId',
        'source',
        'resourceId',
        'threadId',
        'createdAtZ',
        'updatedAtZ',
        'spanId',
        'requestContext',
        'organizationId',
        'projectId',
        'batchId',
        'datasetId',
        'datasetItemId',
    ];

    protected $casts = [
        'scorer' => 'array',
        'preprocessStepResult' => 'array',
        'extractStepResult' => 'array',
        'analyzeStepResult' => 'array',
        'score' => 'float',
        'metadata' => 'array',
        'input' => 'array',
        'output' => 'array',
        'additionalContext' => 'array',
        'runtimeContext' => 'array',
        'entity' => 'array',
        'createdAtZ' => 'datetime',
        'updatedAtZ' => 'datetime',
        'requestContext' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
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