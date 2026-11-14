<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMCustomerTeamTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_CustomerTeamTable';

    protected $primaryKey = 'customer_team_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'spend',
        'alias',
        'parent_org_id',
        'budget_id',
        'budget_override_id',
        'budget_override_expires_at',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'spend' => 'float',
        'budget_override_expires_at' => 'datetime',
    ];

    public function parent_org(): BelongsTo
    {
        return $this->belongsTo(LiteLLMCustomerOrganizationTable::class, 'parent_org_id');
    }

    public function budget(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function budget_override(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_override_id');
    }
}