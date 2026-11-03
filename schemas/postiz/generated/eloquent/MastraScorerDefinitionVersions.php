<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraScorerDefinitionVersions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_scorer_definition_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'scorerDefinitionId',
        'versionNumber',
        'name',
        'description',
        'type',
        'model',
        'instructions',
        'scoreRange',
        'presetConfig',
        'defaultSampling',
        'changedFields',
        'changeMessage',
        'createdAtZ',
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'model' => 'array',
        'scoreRange' => 'array',
        'presetConfig' => 'array',
        'defaultSampling' => 'array',
        'changedFields' => 'array',
        'createdAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}