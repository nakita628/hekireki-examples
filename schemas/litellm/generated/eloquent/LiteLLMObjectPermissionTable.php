<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMObjectPermissionTable extends Model
{
    use HasVersion4Uuids;

    protected $table = 'LiteLLM_ObjectPermissionTable';

    protected $primaryKey = 'object_permission_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'mcp_servers',
        'mcp_access_groups',
        'mcp_tool_permissions',
        'vector_stores',
        'agents',
        'agent_access_groups',
        'models',
        'blocked_tools',
        'mcp_toolsets',
        'search_tools',
        'mcp_tool_search_enabled',
        'skills',
    ];

    protected $casts = [
        'mcp_tool_permissions' => 'array',
        'mcp_tool_search_enabled' => 'boolean',
    ];

    public function teams(): HasMany
    {
        return $this->hasMany(LiteLLMTeamTable::class, 'object_permission_id');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(LiteLLMProjectTable::class, 'object_permission_id');
    }

    public function verification_tokens(): HasMany
    {
        return $this->hasMany(LiteLLMVerificationToken::class, 'object_permission_id');
    }

    public function organizations(): HasMany
    {
        return $this->hasMany(LiteLLMOrganizationTable::class, 'object_permission_id');
    }

    public function users(): HasMany
    {
        return $this->hasMany(LiteLLMUserTable::class, 'object_permission_id');
    }

    public function end_users(): HasMany
    {
        return $this->hasMany(LiteLLMEndUserTable::class, 'object_permission_id');
    }

    public function agents_table(): HasMany
    {
        return $this->hasMany(LiteLLMAgentsTable::class, 'object_permission_id');
    }
}