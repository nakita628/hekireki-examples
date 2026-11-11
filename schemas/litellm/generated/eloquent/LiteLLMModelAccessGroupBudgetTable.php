<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMModelAccessGroupBudgetTable extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ModelAccessGroupBudgetTable';

    protected $primaryKey = 'access_group_name';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'access_group_name',
        'spend',
        'budget_id',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'spend' => 'float',
    ];

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }
}