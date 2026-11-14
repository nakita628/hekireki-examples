<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMDailyCustomerMemberSpend extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_DailyCustomerMemberSpend';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'date',
        'customer_org_id',
        'customer_team_id',
        'customer_member_id',
        'model',
        'model_group',
        'custom_llm_provider',
        'mcp_namespaced_tool_name',
        'endpoint',
        'api_key',
        'spend',
        'prompt_tokens',
        'completion_tokens',
        'cache_read_input_tokens',
        'cache_creation_input_tokens',
        'compression_saved_tokens',
        'compression_savings_spend',
        'prompt_caching_savings_spend',
        'gateway_injected_caching_savings_spend',
        'autorouter_savings_spend',
        'guardrail_spend',
        'api_requests',
        'successful_requests',
        'failed_requests',
        'total_response_time_ms',
        'timed_requests',
    ];

    protected $attributes = [
        'customer_team_id' => '',
        'customer_member_id' => '',
        'model' => '',
        'model_group' => '',
        'custom_llm_provider' => '',
        'mcp_namespaced_tool_name' => '',
        'endpoint' => '',
        'api_key' => '',
        'spend' => 0.0,
        'prompt_tokens' => 0,
        'completion_tokens' => 0,
        'cache_read_input_tokens' => 0,
        'cache_creation_input_tokens' => 0,
        'compression_saved_tokens' => 0,
        'compression_savings_spend' => 0.0,
        'prompt_caching_savings_spend' => 0.0,
        'gateway_injected_caching_savings_spend' => 0.0,
        'autorouter_savings_spend' => 0.0,
        'guardrail_spend' => 0.0,
        'api_requests' => 0,
        'successful_requests' => 0,
        'failed_requests' => 0,
        'total_response_time_ms' => 0,
        'timed_requests' => 0,
    ];

    protected $casts = [
        'spend' => 'float',
        'prompt_tokens' => 'integer',
        'completion_tokens' => 'integer',
        'cache_read_input_tokens' => 'integer',
        'cache_creation_input_tokens' => 'integer',
        'compression_saved_tokens' => 'integer',
        'compression_savings_spend' => 'float',
        'prompt_caching_savings_spend' => 'float',
        'gateway_injected_caching_savings_spend' => 'float',
        'autorouter_savings_spend' => 'float',
        'guardrail_spend' => 'float',
        'api_requests' => 'integer',
        'successful_requests' => 'integer',
        'failed_requests' => 'integer',
        'total_response_time_ms' => 'integer',
        'timed_requests' => 'integer',
    ];
}