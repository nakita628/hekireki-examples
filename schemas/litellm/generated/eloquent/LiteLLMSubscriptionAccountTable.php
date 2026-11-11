<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMSubscriptionAccountTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_SubscriptionAccountTable';

    protected $primaryKey = 'subscription_account_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'custom_llm_provider',
        'account_id',
        'label',
        'monthly_fee',
        'currency',
        'billing_period_start',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'monthly_fee' => 'float',
    ];
}