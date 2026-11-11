<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMMCPServerTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_MCPServerTable';

    protected $primaryKey = 'server_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'server_name',
        'alias',
        'description',
        'instructions',
        'url',
        'spec_path',
        'transport',
        'auth_type',
        'credentials',
        'created_by',
        'updated_by',
        'mcp_info',
        'mcp_access_groups',
        'allowed_tools',
        'tool_name_to_display_name',
        'tool_name_to_description',
        'pinned_tools',
        'extra_headers',
        'static_headers',
        'env_vars',
        'status',
        'last_health_check',
        'health_check_error',
        'command',
        'args',
        'env',
        'issuer',
        'authorization_url',
        'token_url',
        'registration_url',
        'oauth2_flow',
        'token_exchange_endpoint',
        'audience',
        'subject_token_type',
        'token_exchange_profile',
        'allow_all_keys',
        'available_on_public_internet',
        'delegate_auth_to_upstream',
        'oauth_passthrough',
        'dcr_bridge',
        'per_server_oauth_discovery',
        'is_byok',
        'byok_description',
        'byok_api_key_help_url',
        'source_url',
        'timeout',
        'max_concurrent_requests',
        'rpm',
        'approval_status',
        'submitted_by',
        'submitted_at',
        'reviewed_at',
        'review_notes',
    ];

    protected $attributes = [
        'transport' => 'sse',
        'credentials' => '{}',
        'mcp_info' => '{}',
        'tool_name_to_display_name' => '{}',
        'tool_name_to_description' => '{}',
        'pinned_tools' => '{}',
        'static_headers' => '{}',
        'env_vars' => '[]',
        'status' => 'unknown',
        'env' => '{}',
        'allow_all_keys' => false,
        'available_on_public_internet' => true,
        'delegate_auth_to_upstream' => false,
        'oauth_passthrough' => false,
        'per_server_oauth_discovery' => false,
        'is_byok' => false,
        'approval_status' => 'active',
    ];

    protected $casts = [
        'credentials' => 'array',
        'mcp_info' => 'array',
        'tool_name_to_display_name' => 'array',
        'tool_name_to_description' => 'array',
        'pinned_tools' => 'array',
        'static_headers' => 'array',
        'env_vars' => 'array',
        'last_health_check' => 'datetime',
        'env' => 'array',
        'allow_all_keys' => 'boolean',
        'available_on_public_internet' => 'boolean',
        'delegate_auth_to_upstream' => 'boolean',
        'oauth_passthrough' => 'boolean',
        'dcr_bridge' => 'boolean',
        'per_server_oauth_discovery' => 'boolean',
        'is_byok' => 'boolean',
        'timeout' => 'float',
        'max_concurrent_requests' => 'integer',
        'rpm' => 'integer',
        'submitted_at' => 'datetime',
        'reviewed_at' => 'datetime',
    ];
}