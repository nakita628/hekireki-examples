<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMHealthCheckTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_HealthCheckTable';

    protected $primaryKey = 'health_check_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'model_name',
        'model_id',
        'status',
        'healthy_count',
        'unhealthy_count',
        'error_message',
        'response_time_ms',
        'details',
        'checked_by',
        'checked_at',
    ];

    protected $attributes = [
        'healthy_count' => 0,
        'unhealthy_count' => 0,
    ];

    protected $casts = [
        'healthy_count' => 'integer',
        'unhealthy_count' => 'integer',
        'response_time_ms' => 'float',
        'details' => 'array',
        'checked_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('checked_at', $this->attributes)) {
                $this->setAttribute('checked_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}