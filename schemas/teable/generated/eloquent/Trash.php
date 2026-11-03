<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Trash extends Model
{
    use PrismaDates;

    protected $table = 'trash';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'resource_type',
        'resource_id',
        'parent_id',
        'deleted_time',
        'deleted_by',
    ];

    protected $casts = [
        'deleted_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('deleted_time', $this->attributes)) {
                $this->setAttribute('deleted_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}