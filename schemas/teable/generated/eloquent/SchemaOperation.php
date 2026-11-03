<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SchemaOperation extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';
    const ZONED_DATES = ['next_run_at', 'locked_at', 'created_time', 'last_modified_time'];

    protected $table = 'schema_operation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'status',
        'phase',
        'resource_type',
        'resource_id',
        'base_id',
        'table_id',
        'idempotency_key',
        'payload',
        'result',
        'attempts',
        'max_attempts',
        'next_run_at',
        'locked_at',
        'locked_by',
        'last_error',
        'created_time',
        'created_by',
        'last_modified_by',
    ];

    protected $attributes = [
        'attempts' => 0,
        'max_attempts' => 8,
    ];

    protected $casts = [
        'payload' => 'array',
        'result' => 'array',
        'attempts' => 'integer',
        'max_attempts' => 'integer',
        'next_run_at' => 'datetime',
        'locked_at' => 'datetime',
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('next_run_at', $this->attributes)) {
                $this->setAttribute('next_run_at', $this->freshTimestamp());
            }
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}