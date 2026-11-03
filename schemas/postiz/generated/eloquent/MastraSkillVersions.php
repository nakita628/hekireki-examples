<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraSkillVersions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_skill_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'skillId',
        'versionNumber',
        'name',
        'description',
        'instructions',
        'license',
        'compatibility',
        'source',
        'references',
        'scripts',
        'assets',
        'metadata',
        'tree',
        'changedFields',
        'changeMessage',
        'createdAtZ',
        'files',
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'compatibility' => 'array',
        'source' => 'array',
        'references' => 'array',
        'scripts' => 'array',
        'assets' => 'array',
        'metadata' => 'array',
        'tree' => 'array',
        'changedFields' => 'array',
        'createdAtZ' => 'datetime',
        'files' => 'array',
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