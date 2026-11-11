<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMCronJob extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_CronJob';

    protected $primaryKey = 'cronjob_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'cronjob_id',
        'pod_id',
        'status',
        'last_updated',
        'ttl',
    ];

    protected $attributes = [
        'status' => 'INACTIVE',
    ];

    protected $casts = [
        'status' => JobStatus::class,
        'last_updated' => 'datetime',
        'ttl' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('last_updated', $this->attributes)) {
                $this->setAttribute('last_updated', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}