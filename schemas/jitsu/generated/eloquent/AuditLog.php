<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AuditLog extends Model
{
    use PrismaDates;

    protected $table = 'AuditLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'timestamp',
        'type',
        'severity',
        'userId',
        'workspaceId',
        'objectId',
        'changes',
        'authType',
        'tokenId',
        'ip',
        'requestHeaders',
    ];

    protected $attributes = [
        'severity' => 'info',
    ];

    protected $casts = [
        'timestamp' => 'datetime',
        'changes' => 'array',
        'requestHeaders' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('timestamp', $this->attributes)) {
                $this->setAttribute('timestamp', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}