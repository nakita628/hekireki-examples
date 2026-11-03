<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraDatasets extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_datasets';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'metadata',
        'inputSchema',
        'groundTruthSchema',
        'requestContextSchema',
        'tags',
        'targetType',
        'targetIds',
        'scorerIds',
        'version',
        'createdAtZ',
        'updatedAtZ',
        'organizationId',
        'projectId',
        'candidateKey',
        'candidateId',
    ];

    protected $casts = [
        'metadata' => 'array',
        'inputSchema' => 'array',
        'groundTruthSchema' => 'array',
        'requestContextSchema' => 'array',
        'tags' => 'array',
        'targetIds' => 'array',
        'scorerIds' => 'array',
        'version' => 'integer',
        'createdAtZ' => 'datetime',
        'updatedAtZ' => 'datetime',
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