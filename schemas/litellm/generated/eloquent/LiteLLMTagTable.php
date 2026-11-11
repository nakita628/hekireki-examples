<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMTagTable extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_TagTable';

    protected $primaryKey = 'tag_name';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'tag_name',
        'description',
        'models',
        'model_info',
        'spend',
        'budget_id',
        'created_by',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'model_info' => 'array',
        'spend' => 'float',
    ];

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }
}