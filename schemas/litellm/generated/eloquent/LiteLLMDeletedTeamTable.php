<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMDeletedTeamTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_DeletedTeamTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'team_id',
        'team_alias',
        'organization_id',
        'object_permission_id',
        'admins',
        'members',
        'members_with_roles',
        'metadata',
        'max_budget',
        'soft_budget',
        'spend',
        'models',
        'max_parallel_requests',
        'tpm_limit',
        'rpm_limit',
        'tpd_limit',
        'budget_duration',
        'budget_reset_at',
        'blocked',
        'model_spend',
        'model_max_budget',
        'router_settings',
        'team_member_permissions',
        'access_group_ids',
        'policies',
        'model_id',
        'allow_team_guardrail_config',
        'deleted_at',
        'deleted_by',
        'deleted_by_api_key',
        'litellm_changed_by',
    ];

    protected $attributes = [
        'members_with_roles' => '{}',
        'metadata' => '{}',
        'spend' => 0.0,
        'blocked' => false,
        'model_spend' => '{}',
        'model_max_budget' => '{}',
        'router_settings' => '{}',
        'allow_team_guardrail_config' => false,
    ];

    protected $casts = [
        'members_with_roles' => 'array',
        'metadata' => 'array',
        'max_budget' => 'float',
        'soft_budget' => 'float',
        'spend' => 'float',
        'max_parallel_requests' => 'integer',
        'tpm_limit' => 'integer',
        'rpm_limit' => 'integer',
        'tpd_limit' => 'integer',
        'budget_reset_at' => 'datetime',
        'blocked' => 'boolean',
        'model_spend' => 'array',
        'model_max_budget' => 'array',
        'router_settings' => 'array',
        'model_id' => 'integer',
        'allow_team_guardrail_config' => 'boolean',
        'deleted_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('deleted_at', $this->attributes)) {
                $this->setAttribute('deleted_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}