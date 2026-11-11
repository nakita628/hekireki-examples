<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMSpendLogDelivery extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_SpendLogDelivery';

    protected $primaryKey = 'delivery_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'delivery_id',
        'request_id',
        'state',
        'accounting_payload',
        'is_batch',
        'attempts',
        'next_attempt_at',
        'applied_at',
        'last_error',
    ];

    protected $attributes = [
        'state' => 'pending',
        'attempts' => 0,
    ];

    protected $casts = [
        'state' => AccountingDeliveryState::class,
        'accounting_payload' => 'array',
        'is_batch' => 'boolean',
        'attempts' => 'integer',
        'next_attempt_at' => 'datetime',
        'applied_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('next_attempt_at', $this->attributes)) {
                $this->setAttribute('next_attempt_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}