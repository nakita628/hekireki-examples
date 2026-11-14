<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMCustomerBudgetIncreaseRequest extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_CustomerBudgetIncreaseRequest';

    protected $primaryKey = 'request_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'customer_member_id',
        'requested_max_budget',
        'expires_at',
        'reason',
        'status',
        'submitted_by',
        'submitted_at',
        'reviewed_by',
        'reviewed_at',
        'review_notes',
    ];

    protected $attributes = [
        'status' => 'pending_review',
    ];

    protected $casts = [
        'requested_max_budget' => 'float',
        'expires_at' => 'datetime',
        'submitted_at' => 'datetime',
        'reviewed_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('submitted_at', $this->attributes)) {
                $this->setAttribute('submitted_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function customer_member(): BelongsTo
    {
        return $this->belongsTo(LiteLLMCustomerMemberTable::class, 'customer_member_id');
    }
}