<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TableTrash extends Model
{
    use PrismaDates;

    protected $table = 'table_trash';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'table_id',
        'resource_type',
        'snapshot',
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