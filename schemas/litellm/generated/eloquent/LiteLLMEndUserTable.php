<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMEndUserTable extends Model
{
    protected $table = 'LiteLLM_EndUserTable';

    protected $primaryKey = 'user_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'alias',
        'spend',
        'allowed_model_region',
        'default_model',
        'models',
        'budget_id',
        'object_permission_id',
        'blocked',
    ];

    protected $attributes = [
        'spend' => 0.0,
        'blocked' => false,
    ];

    protected $casts = [
        'spend' => 'float',
        'blocked' => 'boolean',
    ];

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }
}