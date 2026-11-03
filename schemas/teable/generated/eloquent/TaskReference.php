<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TaskReference extends Model
{
    use PrismaDates;

    protected $table = 'task_reference';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'from_field_id',
        'to_field_id',
        'created_time',
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