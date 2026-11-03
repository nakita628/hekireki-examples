<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraMessages extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_messages';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'thread_id',
        'content',
        'role',
        'type',
        'resourceId',
        'createdAtZ',
    ];

    protected $casts = [
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