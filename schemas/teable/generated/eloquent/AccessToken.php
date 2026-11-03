<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AccessToken extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'access_token';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'user_id',
        'scopes',
        'space_ids',
        'base_ids',
        'sign',
        'client_id',
        'has_full_access',
        'expired_time',
        'last_used_time',
        'created_time',
    ];

    protected $casts = [
        'has_full_access' => 'boolean',
        'expired_time' => 'datetime',
        'last_used_time' => 'datetime',
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