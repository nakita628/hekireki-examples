<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraWorkflowDefinitions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_workflow_definitions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'description',
        'metadata',
        'inputSchema',
        'outputSchema',
        'stateSchema',
        'requestContextSchema',
        'graph',
        'schedule',
        'status',
        'source',
        'authorId',
        'createdAtZ',
        'updatedAtZ',
    ];

    protected $casts = [
        'metadata' => 'array',
        'inputSchema' => 'array',
        'outputSchema' => 'array',
        'stateSchema' => 'array',
        'requestContextSchema' => 'array',
        'graph' => 'array',
        'schedule' => 'array',
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