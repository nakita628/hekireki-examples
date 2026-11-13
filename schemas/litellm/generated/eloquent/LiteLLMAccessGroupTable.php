<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMAccessGroupTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_AccessGroupTable';

    protected $primaryKey = 'access_group_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'access_group_name',
        'description',
        'access_model_names',
        'access_mcp_server_ids',
        'mcp_tool_permissions',
        'access_agent_ids',
        'access_skill_names',
        'access_vector_store_ids',
        'assigned_team_ids',
        'assigned_key_ids',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'mcp_tool_permissions' => 'array',
    ];
}