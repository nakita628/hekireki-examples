<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMUserTable extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_UserTable';

    protected $primaryKey = 'user_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'user_id',
        'user_alias',
        'team_id',
        'sso_user_id',
        'organization_id',
        'object_permission_id',
        'password',
        'password_reset_required',
        'last_breach_check_at',
        'teams',
        'user_role',
        'max_budget',
        'spend',
        'user_email',
        'models',
        'metadata',
        'max_parallel_requests',
        'tpm_limit',
        'rpm_limit',
        'budget_duration',
        'budget_reset_at',
        'allowed_cache_controls',
        'policies',
        'model_spend',
        'model_max_budget',
    ];

    protected $attributes = [
        'spend' => 0.0,
        'metadata' => '{}',
        'model_spend' => '{}',
        'model_max_budget' => '{}',
    ];

    protected $casts = [
        'password_reset_required' => 'boolean',
        'last_breach_check_at' => 'datetime',
        'max_budget' => 'float',
        'spend' => 'float',
        'metadata' => 'array',
        'max_parallel_requests' => 'integer',
        'tpm_limit' => 'integer',
        'rpm_limit' => 'integer',
        'budget_reset_at' => 'datetime',
        'model_spend' => 'array',
        'model_max_budget' => 'array',
    ];

    public function litellm_organization_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMOrganizationTable::class, 'organization_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function verified_subjects(): HasMany
    {
        return $this->hasMany(LiteLLMVerifiedSubject::class, 'user_id');
    }

    public function organization_memberships(): HasMany
    {
        return $this->hasMany(LiteLLMOrganizationMembership::class, 'user_id');
    }

    public function invitations_created(): HasMany
    {
        return $this->hasMany(LiteLLMInvitationLink::class, 'created_by');
    }

    public function invitations_updated(): HasMany
    {
        return $this->hasMany(LiteLLMInvitationLink::class, 'updated_by');
    }

    public function invitations_user(): HasMany
    {
        return $this->hasMany(LiteLLMInvitationLink::class, 'user_id');
    }
}