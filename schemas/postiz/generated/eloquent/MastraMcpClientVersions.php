<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraMcpClientVersions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_mcp_client_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'mcpClientId',
        'versionNumber',
        'name',
        'description',
        'servers',
        'changedFields',
        'changeMessage',
        'createdAtZ',
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'servers' => 'array',
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