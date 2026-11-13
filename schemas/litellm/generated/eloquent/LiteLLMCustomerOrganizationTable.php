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
        'budget_reset_at',
        'budget_ids',
        'budget_names',
        'budget_id',
        'alias',
        'default_budget_id',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
        'budget_names' => '{}',
    ];

    protected $casts = [
        'spend' => 'float',
        'budget_reset_at' => 'datetime',
        'budget_names' => 'array',
    ];

    public function budget(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function default_budget(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'default_budget_id');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(LiteLLMCustomerTeamTable::class, 'parent_org_id');
    }
}