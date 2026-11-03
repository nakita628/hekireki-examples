<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraChannelInstallations extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_channel_installations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'platform',
        'agentId',
        'status',
        'webhookId',
        'data',
        'configHash',
        'error',
        'createdAtZ',
        'updatedAtZ',
    ];

    protected $casts = [
        'data' => 'array',
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