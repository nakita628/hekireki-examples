<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMProxyWorkerHeartbeat extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ProxyWorkerHeartbeat';

    protected $primaryKey = 'worker_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'worker_id',
        'hostname',
        'started_at',
        'last_heartbeat_at',
    ];

    protected $casts = [
        'started_at' => 'datetime',
        'last_heartbeat_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('started_at', $this->attributes)) {
                $this->setAttribute('started_at', $this->freshTimestamp());
            }
            if (! array_key_exists('last_heartbeat_at', $this->attributes)) {
                $this->setAttribute('last_heartbeat_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}