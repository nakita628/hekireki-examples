<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Invites extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'invites';

    protected $fillable = [
        'code',
        'status',
        'claimedBy',
        'workspaceIds',
        'createdBy',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'status' => 'pending',
    ];

    protected $casts = [
        'claimedBy' => 'integer',
        'createdBy' => 'integer',
        'lastUpdatedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}