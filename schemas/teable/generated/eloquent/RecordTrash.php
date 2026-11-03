<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RecordTrash extends Model
{
    use PrismaDates;

    protected $table = 'record_trash';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'table_id',
        'record_id',
        'snapshot',
        'created_time',
        'created_by',
        'reason',
        'record_created_time',
        'record_created_by',
        'record_last_modified_time',
        'record_last_modified_by',
        'operation_id',
    ];

    protected $attributes = [
        'reason' => 'deleted',
    ];

    protected $casts = [
        'created_time' => 'datetime',
        'record_created_time' => 'datetime',
        'record_last_modified_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}