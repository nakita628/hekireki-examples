<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Str;

class InfraToken extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['expiresOn', 'createdOn', 'updatedOn'];

    protected $table = 'InfraToken';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'creatorUid',
        'label',
        'token',
        'expiresOn',
        'createdOn',
        'updatedOn',
    ];

    protected $casts = [
        'expiresOn' => 'datetime',
        'createdOn' => 'datetime',
        'updatedOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('token', $this->attributes)) {
                $this->setAttribute('token', (string) Str::orderedUuid());
            }
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
            if (! array_key_exists('updatedOn', $this->attributes)) {
                $this->setAttribute('updatedOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}