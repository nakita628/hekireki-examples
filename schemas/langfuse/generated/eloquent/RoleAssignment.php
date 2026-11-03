<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RoleAssignment extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'role_assignments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'owner_id',
        'project_id',
        'org_id',
        'principal_id',
        'user_id',
        'api_key_id',
        'role_id',
        'system_role',
    ];

    protected $casts = [
        'system_role' => SystemRole::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function apiKey(): BelongsTo
    {
        return $this->belongsTo(ApiKey::class, 'api_key_id');
    }
}