<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMSpendLogs extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_SpendLogs';

    protected $primaryKey = 'request_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'request_id',
        'call_type',
        'api_key',
        'spend',
        'total_tokens',
        'prompt_tokens',
        'completion_tokens',
        'startTime',
        'endTime',
        'request_duration_ms',
        'completionStartTime',
        'model',
        'model_id',
        'model_group',
        'custom_llm_provider',
        'api_base',
        'user',
        'metadata',
        'cache_hit',
        'cache_key',
        'request_tags',
        'team_id',
        'organization_id',
        'end_user',
        'requester_ip_address',
        'messages',
        'response',
        'session_id',
        'status',
        'mcp_namespaced_tool_name',
        'billing_agent_id',
        'agent_id',
        'proxy_server_request',
        'litellm_call_id',
    ];

    protected $attributes = [
        'api_key' => '',
        'spend' => 0.0,
        'total_tokens' => 0,
        'prompt_tokens' => 0,
        'completion_tokens' => 0,
        'model' => '',
        'model_id' => '',
        'model_group' => '',
        'custom_llm_provider' => '',
        'api_base' => '',
        'user' => '',
        'metadata' => '{}',
        'cache_hit' => '',
        'cache_key' => '',
        'request_tags' => '[]',
        'messages' => '{}',
        'response' => '{}',
        'proxy_server_request' => '{}',
    ];

    protected $casts = [
        'spend' => 'float',
        'total_tokens' => 'integer',
        'prompt_tokens' => 'integer',
        'completion_tokens' => 'integer',
        'startTime' => 'datetime',
        'endTime' => 'datetime',
        'request_duration_ms' => 'integer',
        'completionStartTime' => 'datetime',
        'metadata' => 'array',
        'request_tags' => 'array',
        'messages' => 'array',
        'response' => 'array',
        'proxy_server_request' => 'array',
    ];
}