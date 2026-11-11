<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMProjectTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ProjectTable';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_alias',
        'description',
        'team_id',
        'budget_id',
        'metadata',
        'models',
        'spend',
        'model_spend',
        'model_rpm_limit',
        'model_tpm_limit',
        'blocked',
        'object_permission_id',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'metadata' => '{}',
        'spend' => 0.0,
        'model_spend' => '{}',
        'model_rpm_limit' => '{}',
        'model_tpm_limit' => '{}',
        'blocked' => false,
    ];

    protected $casts = [
        'metadata' => 'array',
        'spend' => 'float',
        'model_spend' => 'array',
        'model_rpm_limit' => 'array',
        'model_tpm_limit' => 'array',
        'blocked' => 'boolean',
    ];

    public function litellm_team_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMTeamTable::class, 'team_id');
    }

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function keys(): HasMany
    {
        return $this->hasMany(LiteLLMVerificationToken::class, 'project_id');
    }
}