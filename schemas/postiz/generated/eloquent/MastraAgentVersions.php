<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraAgentVersions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_agent_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'agentId',
        'versionNumber',
        'name',
        'description',
        'instructions',
        'model',
        'tools',
        'defaultOptions',
        'workflows',
        'agents',
        'integrationTools',
        'inputProcessors',
        'outputProcessors',
        'memory',
        'scorers',
        'mcpClients',
        'requestContextSchema',
        'workspace',
        'skills',
        'skillsFormat',
        'changedFields',
        'changeMessage',
        'createdAtZ',
        'durable',
        'browser',
        'toolProviders',
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'model' => 'array',
        'tools' => 'array',
        'defaultOptions' => 'array',
        'workflows' => 'array',
        'agents' => 'array',
        'integrationTools' => 'array',
        'inputProcessors' => 'array',
        'outputProcessors' => 'array',
        'memory' => 'array',
        'scorers' => 'array',
        'mcpClients' => 'array',
        'requestContextSchema' => 'array',
        'workspace' => 'array',
        'skills' => 'array',
        'changedFields' => 'array',
        'createdAtZ' => 'datetime',
        'durable' => 'array',
        'browser' => 'array',
        'toolProviders' => 'array',
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