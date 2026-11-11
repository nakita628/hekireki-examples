<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class LiteLLMAgentsTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_AgentsTable';

    protected $primaryKey = 'agent_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'agent_name',
        'litellm_params',
        'agent_card_params',
        'static_headers',
        'extra_headers',
        'kill_switch',
        'agent_access_groups',
        'access_group_ids',
        'object_permission_id',
        'spend',
        'identity_managed',
        'enabled',
        'execution_mode',
        'tpm_limit',
        'rpm_limit',
        'session_tpm_limit',
        'session_rpm_limit',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'static_headers' => '{}',
        'spend' => 0.0,
        'identity_managed' => false,
        'enabled' => true,
        'execution_mode' => 'autonomous',
    ];

    protected $casts = [
        'litellm_params' => 'array',
        'agent_card_params' => 'array',
        'static_headers' => 'array',
        'kill_switch' => 'array',
        'spend' => 'float',
        'identity_managed' => 'boolean',
        'enabled' => 'boolean',
        'tpm_limit' => 'integer',
        'rpm_limit' => 'integer',
        'session_tpm_limit' => 'integer',
        'session_rpm_limit' => 'integer',
    ];

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function identity(): HasOne
    {
        return $this->hasOne(LiteLLMAgentIdentity::class, 'agent_id');
    }

    public function retired_identities(): HasMany
    {
        return $this->hasMany(LiteLLMRetiredAgentIdentity::class, 'agent_id');
    }
}