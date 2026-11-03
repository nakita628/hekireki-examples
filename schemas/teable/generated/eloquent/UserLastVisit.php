<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class UserLastVisit extends Model
{
    use PrismaDates;

    protected $table = 'user_last_visit';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'resource_type',
        'resource_id',
        'parent_resource_id',
        'last_visit_time',
    ];

    protected $casts = [
        'last_visit_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('last_visit_time', $this->attributes)) {
                $this->setAttribute('last_visit_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}