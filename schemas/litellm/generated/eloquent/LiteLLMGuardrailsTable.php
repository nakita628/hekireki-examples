<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMGuardrailsTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_GuardrailsTable';

    protected $primaryKey = 'guardrail_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'guardrail_name',
        'litellm_params',
        'guardrail_info',
        'team_id',
        'status',
        'submitted_at',
        'reviewed_at',
    ];

    protected $attributes = [
        'status' => 'active',
    ];

    protected $casts = [
        'litellm_params' => 'array',
        'guardrail_info' => 'array',
        'submitted_at' => 'datetime',
        'reviewed_at' => 'datetime',
    ];
}