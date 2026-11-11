<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMDeletedVerificationToken extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_DeletedVerificationToken';

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
        'router_settings',
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
        'deleted_at',
        'deleted_by',
        'deleted_by_api_key',
        'litellm_changed_by',
    ];

    protected $attributes = [
        'soft_budget_cooldown' => false,
        'spend' => 0.0,
        'total_spend' => 0.0,
        'aliases' => '{}',
        'config' => '{}',
        'permissions' => '{}',
        'metadata' => '{}',
        'model_spend' => '{}',
        'model_max_budget' => '{}',
        'budget_fallbacks' => '{}',
        'router_settings' => '{}',
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
        'router_settings' => 'array',
        'settings_updated_at' => 'datetime',
        'last_active' => 'datetime',
        'rotation_count' => 'integer',
        'auto_rotate' => 'boolean',
        'last_rotation_at' => 'datetime',
        'key_rotation_at' => 'datetime',
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