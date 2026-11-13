<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMBudgetTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_BudgetTable';

    protected $primaryKey = 'budget_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'max_budget',
        'soft_budget',
        'max_parallel_requests',
        'tpm_limit',
        'rpm_limit',
        'tpd_limit',
        'model_max_budget',
        'model_tpm_limit',
        'model_rpm_limit',
        'budget_duration',
        'budget_reset_at',
        'allowed_models',
        'temp_budget_increase',
        'temp_budget_expiry',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'max_budget' => 'float',
        'soft_budget' => 'float',
        'max_parallel_requests' => 'integer',
        'tpm_limit' => 'integer',
        'rpm_limit' => 'integer',
        'tpd_limit' => 'integer',
        'model_max_budget' => 'array',
        'model_tpm_limit' => 'array',
        'model_rpm_limit' => 'array',
        'budget_reset_at' => 'datetime',
        'temp_budget_increase' => 'float',
        'temp_budget_expiry' => 'datetime',
    ];

    public function customer_organization_defaults(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerOrganizationTable::class, 'default_budget_id');
    }

    public function customer_organizations(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerOrganizationTable::class, 'budget_id');
    }

    public function customer_member_templates(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerMemberTable::class, 'budget_template_id');
    }

    public function customer_teams(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerTeamTable::class, 'budget_id');
    }

    public function customer_members(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerMemberTable::class, 'budget_id');
    }

    public function organization(): HasMany
    {
        return $this->hasMany(LiteLLMOrganizationTable::class, 'budget_id');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(LiteLLMProjectTable::class, 'budget_id');
    }

    public function keys(): HasMany
    {
        return $this->hasMany(LiteLLMVerificationToken::class, 'budget_id');
    }

    public function end_users(): HasMany
    {
        return $this->hasMany(LiteLLMEndUserTable::class, 'budget_id');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(LiteLLMTagTable::class, 'budget_id');
    }

    public function model_access_groups(): HasMany
    {
        return $this->hasMany(LiteLLMModelAccessGroupBudgetTable::class, 'budget_id');
    }

    public function team_membership(): HasMany
    {
        return $this->hasMany(LiteLLMTeamMembership::class, 'budget_id');
    }

    public function organization_membership(): HasMany
    {
        return $this->hasMany(LiteLLMOrganizationMembership::class, 'budget_id');
    }
}