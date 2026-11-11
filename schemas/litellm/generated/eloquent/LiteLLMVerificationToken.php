<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMVerificationToken extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_VerificationToken';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'token',
        'key_name',
        'key_alias',
        'soft_budget_cooldown',
        'spend',
        'total_spend',
        'expires',
        'models',
        'aliases',
        'config',
        'router_settings',
        'user_id',
        'team_id',
        'agent_id',
        'project_id',
        'permissions',
        'max_parallel_requests',
        'metadata',
        'blocked',
        'tpm_limit',
        'rpm_limit',
        'tpd_limit',
        'max_budget',
        'budget_duration',
        'budget_reset_at',
        'allowed_cache_controls',
        'allowed_routes',
        'key_type',
        'policies',
        'access_group_ids',
        'model_spend',
        'model_max_budget',
        'budget_fallbacks',
        'budget_id',
        'organization_id',
        'object_permission_id',
        'created_by',
        'updated_by',
        'settings_updated_at',
        'last_active',
        'rotation_count',
        'auto_rotate',
        'rotation_interval',
        'last_rotation_at',
        'key_rotation_at',
        'budget_limits',
    ];

    protected $attributes = [
        'soft_budget_cooldown' => false,
        'spend' => 0.0,
        'total_spend' => 0.0,
        'aliases' => '{}',
        'config' => '{}',
        'router_settings' => '{}',
        'permissions' => '{}',
        'metadata' => '{}',
        'model_spend' => '{}',
        'model_max_budget' => '{}',
        'budget_fallbacks' => '{}',
        'rotation_count' => 0,
        'auto_rotate' => false,
    ];

    protected $casts = [
        'soft_budget_cooldown' => 'boolean',
        'spend' => 'float',
        'total_spend' => 'float',
        'expires' => 'datetime',
        'aliases' => 'array',
        'config' => 'array',
        'router_settings' => 'array',
        'permissions' => 'array',
        'max_parallel_requests' => 'integer',
        'metadata' => 'array',
        'blocked' => 'boolean',
        'tpm_limit' => 'integer',
        'rpm_limit' => 'integer',
        'tpd_limit' => 'integer',
        'max_budget' => 'float',
        'budget_reset_at' => 'datetime',
        'model_spend' => 'array',
        'model_max_budget' => 'array',
        'budget_fallbacks' => 'array',
        'settings_updated_at' => 'datetime',
        'last_active' => 'datetime',
        'rotation_count' => 'integer',
        'auto_rotate' => 'boolean',
        'last_rotation_at' => 'datetime',
        'key_rotation_at' => 'datetime',
        'budget_limits' => 'array',
    ];

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function litellm_organization_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMOrganizationTable::class, 'organization_id');
    }

    public function litellm_project_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMProjectTable::class, 'project_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function jwt_key_mappings(): HasMany
    {
        return $this->hasMany(LiteLLMJWTKeyMapping::class, 'token');
    }
}