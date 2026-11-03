<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class PinResource extends Model
{
    use PrismaDates;

    protected $table = 'pin_resource';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'type',
        'resource_id',
        'created_time',
        'created_by',
        'order',
    ];

    protected $casts = [
        'created_time' => 'datetime',
        'order' => 'float',
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