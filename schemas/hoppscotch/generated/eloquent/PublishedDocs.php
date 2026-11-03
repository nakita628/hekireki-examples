<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class PublishedDocs extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'PublishedDocs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'title',
        'collectionID',
        'creatorUid',
        'version',
        'autoSync',
        'documentTree',
        'workspaceType',
        'workspaceID',
        'environmentID',
        'environmentName',
        'environmentVariables',
        'metadata',
        'createdOn',
    ];

    protected $casts = [
        'autoSync' => 'boolean',
        'documentTree' => 'array',
        'workspaceType' => WorkspaceType::class,
        'environmentVariables' => 'array',
        'metadata' => 'array',
        'createdOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}