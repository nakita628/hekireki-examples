<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RecordHistory extends Model
{
    use PrismaDates;

    protected $table = 'record_history';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'table_id',
        'record_id',
        'field_id',
        'before',
        'after',
        'created_time',
        'created_by',
    ];

    protected $casts = [
        'created_time' => 'datetime',
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