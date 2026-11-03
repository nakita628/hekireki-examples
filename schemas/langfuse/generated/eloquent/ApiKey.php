<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class ApiKey extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'api_keys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'note',
        'public_key',
        'hashed_secret_key',
        'fast_hashed_secret_key',
        'display_secret_key',
        'last_used_at',
        'expires_at',
        'is_in_app_agent_key',
        'project_id',
        'organization_id',
        'scope',
        'created_by_user_id',
        'user_id',
        'created_by_api_key_id',
    ];

    protected $attributes = [
        'is_in_app_agent_key' => false,
        'scope' => 'PROJECT',
    ];

    protected $casts = [
        'last_used_at' => 'datetime',
        'expires_at' => 'datetime',
        'is_in_app_agent_key' => 'boolean',
        'scope' => ApiKeyScope::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organization_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by_user_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function createdByApiKey(): BelongsTo
    {
        return $this->belongsTo(ApiKey::class, 'created_by_api_key_id');
    }

    public function gatewayAssociation(): HasOne
    {
        return $this->hasOne(GatewayApiKeyAssociation::class, 'api_key_id');
    }

    public function apiKeysCreated(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'created_by_api_key_id');
    }

    public function roleAssignments(): HasMany
    {
        return $this->hasMany(RoleAssignment::class, 'api_key_id');
    }
}