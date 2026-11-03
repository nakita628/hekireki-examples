<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RegisteredInstance extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_seen_at';

    protected $table = 'registered_instances';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'instance_id',
        'version',
        'first_seen_at',
    ];

    protected $casts = [
        'first_seen_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('first_seen_at', $this->attributes)) {
                $this->setAttribute('first_seen_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}