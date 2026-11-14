<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMCustomerOrganizationTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_CustomerOrganizationTable';

    protected $primaryKey = 'customer_org_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'spend',
        'budget_template_ids',
        'budget_id',
        'alias',
        'default_budget_template_id',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'spend' => 'float',
    ];

    public function budget(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function default_budget_template(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'default_budget_template_id');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerTeamTable::class, 'parent_org_id');
    }

    public function members(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerMemberTable::class, 'parent_org_id');
    }
}