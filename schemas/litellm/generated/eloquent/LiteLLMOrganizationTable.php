<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMOrganizationTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_OrganizationTable';

    protected $primaryKey = 'organization_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organization_alias',
        'budget_id',
        'metadata',
        'models',
        'spend',
        'model_spend',
        'object_permission_id',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'metadata' => '{}',
        'spend' => 0.0,
        'model_spend' => '{}',
    ];

    protected $casts = [
        'metadata' => 'array',
        'spend' => 'float',
        'model_spend' => 'array',
    ];

    public function litellm_budget_table(): BelongsTo
    {
        return $this->belongsTo(LiteLLMBudgetTable::class, 'budget_id');
    }

    public function object_permission(): BelongsTo
    {
        return $this->belongsTo(LiteLLMObjectPermissionTable::class, 'object_permission_id');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(LiteLLMTeamTable::class, 'organization_id');
    }

    public function users(): HasMany
    {
        return $this->hasMany(LiteLLMUserTable::class, 'organization_id');
    }

    public function keys(): HasMany
    {
        return $this->hasMany(LiteLLMVerificationToken::class, 'organization_id');
    }

    public function members(): HasMany
    {
        return $this->hasMany(LiteLLMOrganizationMembership::class, 'organization_id');
    }
}