<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraChannelConfig extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['updatedAtZ'];

    protected $table = 'mastra_channel_config';

    protected $primaryKey = 'platform';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'platform',
        'data',
        'updatedAtZ',
    ];

    protected $casts = [
        'data' => 'array',
        'updatedAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('updatedAtZ', $this->attributes)) {
                $this->setAttribute('updatedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}