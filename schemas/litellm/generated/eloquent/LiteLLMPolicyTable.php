<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMPolicyTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_PolicyTable';

    protected $primaryKey = 'policy_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'policy_name',
        'version_number',
        'version_status',
        'parent_version_id',
        'is_latest',
        'published_at',
        'production_at',
        'inherit',
        'description',
        'guardrails_add',
        'guardrails_remove',
        'condition',
        'pipeline',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'version_number' => 1,
        'version_status' => 'production',
        'is_latest' => true,
        'condition' => '{}',
    ];

    protected $casts = [
        'version_number' => 'integer',
        'is_latest' => 'boolean',
        'published_at' => 'datetime',
        'production_at' => 'datetime',
        'condition' => 'array',
        'pipeline' => 'array',
    ];
}