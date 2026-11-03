<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraTraces extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_traces';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'parentSpanId',
        'name',
        'traceId',
        'scope',
        'kind',
        'attributes',
        'status',
        'events',
        'links',
        'other',
        'startTime',
        'endTime',
        'createdAtZ',
    ];

    protected $casts = [
        'kind' => 'integer',
        'attributes' => 'array',
        'status' => 'array',
        'events' => 'array',
        'links' => 'array',
        'startTime' => 'integer',
        'endTime' => 'integer',
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