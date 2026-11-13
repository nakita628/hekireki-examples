<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMCustomerMemberTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_CustomerMemberTable';

    protected $primaryKey = 'customer_member_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'spend',
        'budget_reset_at',
        'alias',
        'parent_team_id',
        'budget_id',
        'budget_template_id',
        'budget_override_expires_at',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'spend' => 'float',
        'budget_reset_at' => 'datetime',
        'budget_override_expires_at' => 'datetime',
    ];

    public function parent_team(): BelongsTo
    {
        return $this->belongsTo(LiteLLMCustomerTeamTable::class, 'parent_team_id');
    }

    public function budget(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function budget_template(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_template_id');
    }
}