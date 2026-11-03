<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraWorkspaceVersions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_workspace_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workspaceId',
        'versionNumber',
        'name',
        'description',
        'filesystem',
        'sandbox',
        'mounts',
        'search',
        'skills',
        'tools',
        'autoSync',
        'operationTimeout',
        'changedFields',
        'changeMessage',
        'createdAtZ',
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'filesystem' => 'array',
        'sandbox' => 'array',
        'mounts' => 'array',
        'search' => 'array',
        'skills' => 'array',
        'tools' => 'array',
        'autoSync' => 'boolean',
        'operationTimeout' => 'integer',
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