<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMTeamTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_TeamTable';

    protected $primaryKey = 'team_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
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
        'default_team_member_models',
        'budget_limits',
        'model_id',
        'allow_team_guardrail_config',
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
        'budget_limits' => 'array',
        'model_id' => 'integer',
        'allow_team_guardrail_config' => 'boolean',
    ];

    public function litellm_organization_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMOrganizationTable::class, 'organization_id');
    }

    public function litellm_model_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMModelTable::class, 'model_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(LiteLLMProjectTable::class, 'team_id');
    }
}