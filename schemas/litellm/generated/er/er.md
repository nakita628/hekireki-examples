```mermaid
erDiagram
    LiteLLM_BudgetTable |o--o{ LiteLLM_CustomerOrganizationTable : "(budget_id) - (budget_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_CustomerOrganizationTable : "(budget_id) - (default_budget_id)"
    LiteLLM_CustomerOrganizationTable ||--o{ LiteLLM_CustomerTeamTable : "(customer_org_id) - (parent_org_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_CustomerTeamTable : "(budget_id) - (budget_id)"
    LiteLLM_CustomerTeamTable ||--o{ LiteLLM_CustomerMemberTable : "(customer_team_id) - (parent_team_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_CustomerMemberTable : "(budget_id) - (budget_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_CustomerMemberTable : "(budget_id) - (budget_template_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_AgentsTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_AgentsTable ||--o| LiteLLM_AgentIdentity : "(agent_id) - (agent_id)"
    LiteLLM_AgentsTable |o--o{ LiteLLM_RetiredAgentIdentity : "(agent_id) - (agent_id)"
    LiteLLM_UserTable |o--o{ LiteLLM_VerifiedSubject : "(user_id) - (user_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_OrganizationTable : "(budget_id) - (budget_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_OrganizationTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_OrganizationTable |o--o{ LiteLLM_TeamTable : "(organization_id) - (organization_id)"
    LiteLLM_ModelTable |o--o| LiteLLM_TeamTable : "(id) - (model_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_TeamTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_TeamTable |o--o{ LiteLLM_ProjectTable : "(team_id) - (team_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_ProjectTable : "(budget_id) - (budget_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_ProjectTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_OrganizationTable |o--o{ LiteLLM_UserTable : "(organization_id) - (organization_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_UserTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_VerificationToken : "(budget_id) - (budget_id)"
    LiteLLM_OrganizationTable |o--o{ LiteLLM_VerificationToken : "(organization_id) - (organization_id)"
    LiteLLM_ProjectTable |o--o{ LiteLLM_VerificationToken : "(project_id) - (project_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_VerificationToken : "(object_permission_id) - (object_permission_id)"
    LiteLLM_VerificationToken ||--o{ LiteLLM_JWTKeyMapping : "(token) - (token)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_EndUserTable : "(budget_id) - (budget_id)"
    LiteLLM_ObjectPermissionTable |o--o{ LiteLLM_EndUserTable : "(object_permission_id) - (object_permission_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_ModelAccessGroupBudgetTable : "(budget_id) - (budget_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_TagTable : "(budget_id) - (budget_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_TeamMembership : "(budget_id) - (budget_id)"
    LiteLLM_UserTable ||--o{ LiteLLM_OrganizationMembership : "(user_id) - (user_id)"
    LiteLLM_OrganizationTable ||--o{ LiteLLM_OrganizationMembership : "(organization_id) - (organization_id)"
    LiteLLM_BudgetTable |o--o{ LiteLLM_OrganizationMembership : "(budget_id) - (budget_id)"
    LiteLLM_UserTable ||--o{ LiteLLM_InvitationLink : "(user_id) - (user_id)"
    LiteLLM_UserTable ||--o{ LiteLLM_InvitationLink : "(user_id) - (created_by)"
    LiteLLM_UserTable ||--o{ LiteLLM_InvitationLink : "(user_id) - (updated_by)"
    LiteLLM_ClaudeCodePluginTable ||--o{ LiteLLM_ClaudeCodePluginVersion : "(id) - (plugin_id)"
    LiteLLM_ClaudeCodePluginTable ||--o{ LiteLLM_ClaudeCodePluginDownload : "(id) - (plugin_id)"
    LiteLLM_ClaudeCodePluginTable ||--o| LiteLLM_ClaudeCodePluginUpload : "(name) - (plugin_name)"
    LiteLLM_WorkflowRun ||--o{ LiteLLM_WorkflowEvent : "(run_id) - (run_id)"
    LiteLLM_WorkflowRun ||--o{ LiteLLM_WorkflowMessage : "(run_id) - (run_id)"
    LiteLLM_Lens ||--o{ LiteLLM_LensReview : "(id) - (lens_id)"
    LiteLLM_BudgetTable {
        string budget_id PK
        float max_budget
        float soft_budget
        int max_parallel_requests
        bigint tpm_limit
        bigint rpm_limit
        bigint tpd_limit
        json model_max_budget
        json model_tpm_limit
        json model_rpm_limit
        string budget_duration
        datetime budget_reset_at
        string[] allowed_models
        float temp_budget_increase
        datetime temp_budget_expiry
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_CustomerOrganizationTable {
        float spend
        datetime budget_reset_at
        string[] budget_ids
        json budget_names
        string budget_id FK
        string customer_org_id PK
        string alias
        string default_budget_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_CustomerTeamTable {
        float spend
        datetime budget_reset_at
        string customer_team_id PK
        string alias
        string parent_org_id FK
        string budget_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_CustomerMemberTable {
        float spend
        datetime budget_reset_at
        string customer_member_id PK
        string alias
        string parent_team_id FK
        string budget_id FK
        string budget_template_id FK
        datetime budget_override_expires_at
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_CredentialsTable {
        string credential_id PK
        string credential_name UK
        string display_name
        json credential_values
        json credential_info
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_ProxyModelTable {
        string model_id PK
        string model_name
        json litellm_params
        json model_info
        boolean blocked
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_AgentsTable {
        string agent_id PK
        string agent_name UK
        json litellm_params
        json agent_card_params
        json static_headers
        string[] extra_headers
        json kill_switch
        string[] agent_access_groups
        string[] access_group_ids
        string object_permission_id FK
        float spend
        boolean identity_managed
        boolean enabled
        string execution_mode
        int tpm_limit
        int rpm_limit
        int session_tpm_limit
        int session_rpm_limit
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_AgentIdentity {
        string agent_id PK, FK
        boolean active
        string provider UK
        string issuer UK
        string tenant_id UK
        string client_id UK
        string service_principal_id UK
        string[] required_roles
        string[] required_scopes
        string revision
        datetime last_authenticated_at
    }
    LiteLLM_RetiredAgentIdentity {
        string binding_id PK
        string agent_id FK
        string provider UK
        string issuer
        string tenant_id UK
        string client_id UK
    }
    LiteLLM_RetiredAgent {
        string original_agent_id PK
        datetime retired_at
    }
    LiteLLM_VerifiedSubject {
        string subject_id PK
        string issuer UK
        string tenant_id UK
        string oid UK
        string kind
        string user_id FK
        string verified_via
        datetime verified_at
    }
    LiteLLM_OrganizationTable {
        string organization_id PK
        string organization_alias
        string budget_id FK
        json metadata
        string[] models
        float spend
        json model_spend
        string object_permission_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_ModelTable {
        int id PK
        json model_aliases
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_TeamTable {
        string team_id PK
        string team_alias
        string organization_id FK
        string object_permission_id FK
        string[] admins
        string[] members
        json members_with_roles
        json metadata
        float max_budget
        float soft_budget
        float spend
        string[] models
        int max_parallel_requests
        bigint tpm_limit
        bigint rpm_limit
        bigint tpd_limit
        string budget_duration
        datetime budget_reset_at
        boolean blocked
        datetime created_at
        datetime updated_at
        json model_spend
        json model_max_budget
        json router_settings
        string[] team_member_permissions
        string[] access_group_ids
        string[] policies
        string[] default_team_member_models
        json budget_limits
        int model_id FK, UK
        boolean allow_team_guardrail_config
    }
    LiteLLM_ProjectTable {
        string project_id PK
        string project_alias
        string description
        string team_id FK
        string budget_id FK
        json metadata
        string[] models
        float spend
        json model_spend
        json model_rpm_limit
        json model_tpm_limit
        boolean blocked
        string object_permission_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_DeletedTeamTable {
        string id PK
        string team_id
        string team_alias
        string organization_id
        string object_permission_id
        string[] admins
        string[] members
        json members_with_roles
        json metadata
        float max_budget
        float soft_budget
        float spend
        string[] models
        int max_parallel_requests
        bigint tpm_limit
        bigint rpm_limit
        bigint tpd_limit
        string budget_duration
        datetime budget_reset_at
        boolean blocked
        json model_spend
        json model_max_budget
        json router_settings
        string[] team_member_permissions
        string[] access_group_ids
        string[] policies
        int model_id
        boolean allow_team_guardrail_config
        datetime created_at
        datetime updated_at
        datetime deleted_at
        string deleted_by
        string deleted_by_api_key
        string litellm_changed_by
    }
    LiteLLM_UserTable {
        string user_id PK
        string user_alias
        string team_id
        string sso_user_id UK
        string organization_id FK
        string object_permission_id FK
        string password
        boolean password_reset_required
        datetime last_breach_check_at
        string[] teams
        string user_role
        float max_budget
        float spend
        string user_email
        string[] models
        json metadata
        int max_parallel_requests
        bigint tpm_limit
        bigint rpm_limit
        string budget_duration
        datetime budget_reset_at
        string[] allowed_cache_controls
        string[] policies
        json model_spend
        json model_max_budget
        datetime created_at
        datetime updated_at
    }
    LiteLLM_ObjectPermissionTable {
        string object_permission_id PK
        string[] mcp_servers
        string[] mcp_access_groups
        json mcp_tool_permissions
        string[] vector_stores
        string[] agents
        string[] agent_access_groups
        string[] models
        string[] blocked_tools
        string[] mcp_toolsets
        string[] search_tools
        boolean mcp_tool_search_enabled
        string[] skills
    }
    LiteLLM_MCPServerTable {
        string server_id PK
        string server_name
        string alias
        string description
        string instructions
        string url
        string spec_path
        string transport
        string auth_type
        json credentials
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
        json mcp_info
        string[] mcp_access_groups
        string[] allowed_tools
        json tool_name_to_display_name
        json tool_name_to_description
        json pinned_tools
        string[] extra_headers
        json static_headers
        json env_vars
        string status
        datetime last_health_check
        string health_check_error
        string command
        string[] args
        json env
        string issuer
        string authorization_url
        string token_url
        string registration_url
        string oauth2_flow
        string token_exchange_endpoint
        string audience
        string subject_token_type
        string token_exchange_profile
        boolean allow_all_keys
        boolean available_on_public_internet
        boolean delegate_auth_to_upstream
        boolean oauth_passthrough
        boolean dcr_bridge
        boolean per_server_oauth_discovery
        boolean is_byok
        string[] byok_description
        string byok_api_key_help_url
        string source_url
        float timeout
        int max_concurrent_requests
        int rpm
        string approval_status
        string submitted_by
        datetime submitted_at
        datetime reviewed_at
        string review_notes
    }
    LiteLLM_MCPToolsetTable {
        string toolset_id PK
        string toolset_name UK
        string description
        json tools
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_MCPUserCredentials {
        string id PK
        string user_id UK
        string server_id UK
        string credential_b64
        datetime created_at
        datetime updated_at
    }
    LiteLLM_UserProviderCredentials {
        string id PK
        string user_id UK
        string credential_name UK
        string provider
        string credential_b64
        datetime created_at
        datetime updated_at
    }
    LiteLLM_MCPUserEnvVars {
        string id PK
        string user_id UK
        string server_id UK
        string values_b64
        datetime created_at
        datetime updated_at
    }
    LiteLLM_MCPServerOAuthClient {
        string server_id PK
        json credentials
        datetime created_at
        datetime updated_at
    }
    LiteLLM_SSOIdentityAssertion {
        string user_id PK
        string assertion_b64
        datetime created_at
        datetime updated_at
    }
    LiteLLM_VerificationToken {
        string token PK
        string key_name
        string key_alias
        boolean soft_budget_cooldown
        float spend
        float total_spend
        datetime expires
        string[] models
        json aliases
        json config
        json router_settings
        string user_id
        string team_id
        string agent_id
        string project_id FK
        json permissions
        int max_parallel_requests
        json metadata
        boolean blocked
        bigint tpm_limit
        bigint rpm_limit
        bigint tpd_limit
        float max_budget
        string budget_duration
        datetime budget_reset_at
        string[] allowed_cache_controls
        string[] allowed_routes
        string key_type
        string[] policies
        string[] access_group_ids
        json model_spend
        json model_max_budget
        json budget_fallbacks
        string budget_id FK
        string organization_id FK
        string object_permission_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
        datetime settings_updated_at
        datetime last_active
        int rotation_count
        boolean auto_rotate
        string rotation_interval
        datetime last_rotation_at
        datetime key_rotation_at
        json budget_limits
    }
    LiteLLM_JWTKeyMapping {
        string id PK
        string jwt_issuer UK
        string jwt_claim_name UK
        string jwt_claim_value UK
        string token FK
        string description
        boolean is_active
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_DeprecatedVerificationToken {
        string id PK
        string token UK
        string active_token_id
        datetime revoke_at
        datetime created_at
    }
    LiteLLM_DeletedVerificationToken {
        string id PK
        string token
        string key_name
        string key_alias
        boolean soft_budget_cooldown
        float spend
        float total_spend
        datetime expires
        string[] models
        json aliases
        json config
        string user_id
        string team_id
        string agent_id
        string project_id
        json permissions
        int max_parallel_requests
        json metadata
        boolean blocked
        bigint tpm_limit
        bigint rpm_limit
        bigint tpd_limit
        float max_budget
        string budget_duration
        datetime budget_reset_at
        string[] allowed_cache_controls
        string[] allowed_routes
        string key_type
        string[] policies
        string[] access_group_ids
        json model_spend
        json model_max_budget
        json budget_fallbacks
        json router_settings
        string budget_id
        string organization_id
        string object_permission_id
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
        datetime settings_updated_at
        datetime last_active
        int rotation_count
        boolean auto_rotate
        string rotation_interval
        datetime last_rotation_at
        datetime key_rotation_at
        datetime deleted_at
        string deleted_by
        string deleted_by_api_key
        string litellm_changed_by
    }
    LiteLLM_EndUserTable {
        string user_id PK
        string alias
        float spend
        string allowed_model_region
        string default_model
        string[] models
        string budget_id FK
        string object_permission_id FK
        boolean blocked
    }
    LiteLLM_ModelAccessGroupBudgetTable {
        string access_group_name PK
        float spend
        string budget_id FK
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_TagTable {
        string tag_name PK
        string description
        string[] models
        json model_info
        float spend
        string budget_id FK
        datetime created_at
        string created_by
        datetime updated_at
    }
    LiteLLM_Config {
        string param_name PK
        json param_value
        datetime last_run_at
        bigint reload_revision
    }
    LiteLLM_SpendLogs {
        string request_id PK
        string call_type
        string api_key
        float spend
        int total_tokens
        int prompt_tokens
        int completion_tokens
        datetime startTime
        datetime endTime
        int request_duration_ms
        datetime completionStartTime
        string model
        string model_id
        string model_group
        string custom_llm_provider
        string api_base
        string user
        json metadata
        string cache_hit
        string cache_key
        json request_tags
        string team_id
        string organization_id
        string end_user
        string requester_ip_address
        json messages
        json response
        string session_id
        string status
        string mcp_namespaced_tool_name
        string billing_agent_id
        string agent_id
        json proxy_server_request
        string litellm_call_id
        datetime created_at
        datetime updated_at
    }
    LiteLLM_BudgetWindowSpend {
        string entity_type PK
        string entity_id PK
        string window_duration PK
        datetime window_start
        float spend
        datetime created_at
        datetime updated_at
    }
    LiteLLM_ErrorLogs {
        string request_id PK
        datetime startTime
        datetime endTime
        string api_base
        string model_group
        string litellm_model_name
        string model_id
        json request_kwargs
        string exception_type
        string exception_string
        string status_code
    }
    LiteLLM_UserNotifications {
        string request_id PK
        string user_id
        string[] models
        string justification
        string status
    }
    LiteLLM_TeamMembership {
        string user_id PK
        string team_id PK
        float spend
        float total_spend
        string budget_id FK
    }
    LiteLLM_OrganizationMembership {
        string user_id PK, FK, UK
        string organization_id PK, FK, UK
        string user_role
        float spend
        string budget_id FK
        datetime created_at
        datetime updated_at
    }
    LiteLLM_InvitationLink {
        string id PK
        string user_id FK
        boolean is_accepted
        datetime accepted_at
        datetime expires_at
        datetime created_at
        string created_by FK
        datetime updated_at
        string updated_by FK
    }
    LiteLLM_AuditLog {
        string id PK
        datetime updated_at
        string changed_by
        string changed_by_api_key
        string action
        string table_name
        string object_id
        json before_value
        json updated_values
    }
    LiteLLM_DailyUserSpend {
        string id PK
        string user_id UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyGlobalSpend {
        string id PK
        string date UK
        string model UK
        string model_group UK
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyOrganizationSpend {
        string id PK
        string organization_id UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyEndUserSpend {
        string id PK
        string end_user_id UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyAgentSpend {
        string id PK
        string agent_id UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyTeamSpend {
        string id PK
        string team_id UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        float ptu_flat_cost
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyTagSpend {
        string id PK
        string request_id
        string tag UK
        string date UK
        string api_key UK
        string model UK
        string model_group
        string custom_llm_provider UK
        string mcp_namespaced_tool_name UK
        string endpoint UK
        bigint prompt_tokens
        bigint completion_tokens
        bigint cache_read_input_tokens
        bigint cache_creation_input_tokens
        bigint compression_saved_tokens
        float compression_savings_spend
        float prompt_caching_savings_spend
        float gateway_injected_caching_savings_spend
        float autorouter_savings_spend
        float guardrail_spend
        float spend
        bigint api_requests
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_ProxyWorkerHeartbeat {
        string worker_id PK
        string hostname
        datetime started_at
        datetime last_heartbeat_at
    }
    LiteLLM_CronJob {
        string cronjob_id PK
        string pod_id
        jobstatus status
        datetime last_updated
        datetime ttl
    }
    LiteLLM_ScheduledJobRun {
        string id PK
        string job_name
        string lock_group
        string schedule
        datetime scheduled_for
        scheduledjobrunstatus status
        string claimed_by
        datetime claimed_at
        datetime lease_expires_at
        datetime finished_at
        datetime created_at
    }
    LiteLLM_ManagedFileTable {
        string id PK
        string unified_file_id UK
        json file_object
        json model_mappings
        string[] flat_model_file_ids
        string storage_backend
        string storage_url
        datetime created_at
        string created_by
        string team_id
        datetime updated_at
        string updated_by
    }
    LiteLLM_ManagedObjectTable {
        string id PK
        string unified_object_id UK
        string model_object_id UK
        json file_object
        string file_purpose
        string status
        boolean batch_processed
        datetime created_at
        string created_by
        string team_id
        string org_id
        string api_key
        json request_tags
        string end_user_id
        datetime updated_at
        string updated_by
    }
    LiteLLM_ManagedFileContentTable {
        string id PK
        bytes content
        datetime created_at
    }
    LiteLLM_ManagedVectorStoreTable {
        string id PK
        string unified_resource_id UK
        json resource_object
        json model_mappings
        string[] flat_model_resource_ids
        string storage_backend
        string storage_url
        datetime created_at
        string created_by
        string team_id
        datetime updated_at
        string updated_by
    }
    LiteLLM_ManagedVectorStoresTable {
        string vector_store_id PK
        string custom_llm_provider
        string vector_store_name
        string vector_store_description
        json vector_store_metadata
        datetime created_at
        datetime updated_at
        string litellm_credential_name
        json litellm_params
        string team_id
        string user_id
    }
    LiteLLM_GuardrailsTable {
        string guardrail_id PK
        string guardrail_name UK
        json litellm_params
        json guardrail_info
        string team_id
        datetime created_at
        datetime updated_at
        string status
        datetime submitted_at
        datetime reviewed_at
    }
    LiteLLM_DailyGuardrailMetrics {
        string guardrail_id PK
        string date PK
        bigint requests_evaluated
        bigint passed_count
        bigint blocked_count
        bigint flagged_count
        float avg_score
        float avg_latency_ms
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyGuardrailUsageUnits {
        string guardrail_id PK
        string date PK
        string team_id PK
        string api_key PK
        string usage_unit PK
        bigint units
        float cost
        bigint untracked_units
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyPolicyMetrics {
        string policy_id PK
        string date PK
        bigint requests_evaluated
        bigint passed_count
        bigint blocked_count
        bigint flagged_count
        float avg_score
        float avg_latency_ms
        datetime created_at
        datetime updated_at
    }
    LiteLLM_SpendLogGuardrailIndex {
        string request_id PK
        string guardrail_id PK
        string policy_id
        datetime start_time
    }
    LiteLLM_SpendLogToolIndex {
        string request_id PK
        string tool_name PK
        datetime start_time
    }
    LiteLLM_DailyToolSpend {
        string date PK
        string tool_name PK
        float spend
        bigint total_tokens
        bigint request_count
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyModelUsage {
        string date PK
        string model_group PK
        string model PK
        string custom_llm_provider PK
        string task_type PK
        float spend
        bigint prompt_tokens
        bigint completion_tokens
        bigint request_count
        bigint successful_requests
        bigint failed_requests
        bigint total_response_time_ms
        bigint timed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyGatewayRequests {
        string date PK
        string category PK
        string route PK
        bigint successful_requests
        bigint failed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyGatewayFailedRequests {
        string date PK
        string category PK
        string route PK
        int status_code PK
        bigint failed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_DailyRequestErrors {
        string date PK
        string api_key PK
        string team_id PK
        string user_id PK
        string model_group PK
        int status_code PK
        bigint failed_requests
        datetime created_at
        datetime updated_at
    }
    LiteLLM_PromptTable {
        string id PK
        string prompt_id UK
        int version UK
        string environment UK
        string created_by
        json litellm_params
        json prompt_info
        datetime created_at
        datetime updated_at
    }
    LiteLLM_HealthCheckTable {
        string health_check_id PK
        string model_name
        string model_id
        string status
        int healthy_count
        int unhealthy_count
        string error_message
        float response_time_ms
        json details
        string checked_by
        datetime checked_at
        datetime created_at
        datetime updated_at
    }
    LiteLLM_SearchToolsTable {
        string search_tool_id PK
        string search_tool_name UK
        json litellm_params
        json search_tool_info
        datetime created_at
        datetime updated_at
    }
    LiteLLM_SSOConfig {
        string id PK
        json sso_settings
        datetime created_at
        datetime updated_at
    }
    LiteLLM_ManagedVectorStoreIndexTable {
        string id PK
        string index_name UK
        json litellm_params
        json index_info
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_CacheConfig {
        string id PK
        json cache_settings
        datetime created_at
        datetime updated_at
    }
    LiteLLM_UISettings {
        string id PK
        json ui_settings
        datetime created_at
        datetime updated_at
    }
    LiteLLM_ConfigOverrides {
        string config_type PK
        json config_value
        datetime created_at
        datetime updated_at
    }
    LiteLLM_SkillsTable {
        string skill_id PK
        string display_title
        string description
        string instructions
        string source
        string latest_version
        bytes file_content
        string file_name
        string file_type
        json metadata
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_PolicyTable {
        string policy_id PK
        string policy_name UK
        int version_number UK
        string version_status
        string parent_version_id
        boolean is_latest
        datetime published_at
        datetime production_at
        string inherit
        string description
        string[] guardrails_add
        string[] guardrails_remove
        json condition
        json pipeline
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_PolicyAttachmentTable {
        string attachment_id PK
        string policy_name
        string scope
        string[] teams
        string[] keys
        string[] models
        string[] tags
        int priority
        boolean is_default
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_ToolTable {
        string tool_id PK
        string tool_name UK
        string origin
        string input_policy
        string output_policy
        int call_count
        json assignments
        string key_hash
        string team_id
        string key_alias
        string user_agent
        datetime last_used_at
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_AccessGroupTable {
        string access_group_id PK
        string access_group_name UK
        string description
        string[] access_model_names
        string[] access_mcp_server_ids
        json mcp_tool_permissions
        string[] access_agent_ids
        string[] access_skill_names
        string[] access_vector_store_ids
        string[] assigned_team_ids
        string[] assigned_key_ids
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_ClaudeCodePluginTable {
        string id PK
        string name UK
        string version
        string description
        string manifest_json
        string files_json
        boolean enabled
        datetime created_at
        datetime updated_at
        string created_by
        bigint anonymous_download_count
    }
    LiteLLM_ClaudeCodePluginVersion {
        string id PK
        string plugin_id FK, UK
        string version UK
        string description
        string manifest_json
        datetime published_at
        string published_by
        boolean archive_available
        bytes content
    }
    LiteLLM_SkillSubmission {
        string id PK
        string plugin_name
        string operation
        string status
        string proposal_json
        bytes archive_content
        string submitted_by
        datetime created_at
        string base_plugin_id
        datetime base_updated_at
        string reviewed_by
        datetime reviewed_at
        string rejection_reason
        string scan_json
    }
    LiteLLM_ClaudeCodePluginDownload {
        bigint id PK
        string plugin_id FK
        datetime downloaded_at
        string version
        string digest
        boolean authenticated
        string user_id
        string user_email
        string key_alias
        string team_id
        string team_alias
    }
    LiteLLM_ClaudeCodePluginUpload {
        string plugin_name PK, FK
        bytes content
    }
    LiteLLM_MemoryTable {
        string memory_id PK
        string key UK
        string value
        json metadata
        string user_id
        string team_id
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_AdaptiveRouterState {
        string router_name PK
        string request_type PK
        string model_name PK
        float alpha
        float beta
        int total_samples
        datetime last_updated_at
    }
    LiteLLM_AdaptiveRouterSession {
        string session_id PK
        string router_name PK
        string model_name PK
        string classified_type
        int misalignment_count
        int stagnation_count
        int disengagement_count
        int satisfaction_count
        int failure_count
        int loop_count
        int exhaustion_count
        string last_user_content
        string last_assistant_content
        json tool_call_history
        json pending_tool_calls
        int turn_count
        int last_processed_turn
        boolean clean_credit_awarded
        int terminal_status
        datetime last_activity_at
    }
    LiteLLM_AutoRouterBaselineComparison {
        string scope PK
        string api_key
        string session_id
        string router_name
        boolean initial_equivalent
        bigint revision
        bigint published_revision
        string history
        datetime attempted_at
        boolean retired
        datetime updated_at
    }
    LiteLLM_AutoRouterBaselineObservation {
        string request_id PK
        string scope
        float started_at
        bigint revision
        string data
        string publication
        boolean conflicted
    }
    LiteLLM_AutoRouterSession {
        string api_key PK
        string session_id PK
        string router_name PK
        string router_type
        datetime first_turn_at
        datetime last_turn_at
        string last_model
        json models
        int turns
        int unordered_turns
        int covered_turns
        int cache_hits
        int same_model_turns
        int same_model_hits
        int first_visit_turns
        int first_visit_hits
        int return_turns
        int return_hits
        int return_expired_misses
        int return_within_ttl_misses
        int ttl_5m_turns
        int ttl_1h_turns
        bigint total_tokens
        float spend
        float saved_spend
        int savings_estimated_turns
        float savings_estimated_actual_spend
        float savings_estimated_saved_spend
        json savings_estimated_baseline_models
        float classifier_cost
        int classifier_cost_recorded_turns
        json tier_turns
        json baseline_models
    }
    LiteLLM_AutoRouterUserSession {
        string user_id PK
        string api_key PK
        string session_id PK
        string router_name PK
        string router_type
        datetime first_turn_at
        datetime last_turn_at
        string last_model
        json models
        int turns
        int unordered_turns
        int covered_turns
        int cache_hits
        int same_model_turns
        int same_model_hits
        int first_visit_turns
        int first_visit_hits
        int return_turns
        int return_hits
        int return_expired_misses
        int return_within_ttl_misses
        int ttl_5m_turns
        int ttl_1h_turns
        bigint total_tokens
        float spend
        float saved_spend
        int savings_estimated_turns
        float savings_estimated_actual_spend
        float savings_estimated_saved_spend
        json savings_estimated_baseline_models
        float classifier_cost
        int classifier_cost_recorded_turns
        json tier_turns
        json baseline_models
    }
    LiteLLM_AutoRouterDailySpend {
        string date PK
        string api_key PK
        string user_id PK
        string router_name PK
        string router_type PK
        int turns
        bigint total_tokens
        int token_recorded_turns
        float spend
        float saved_spend
        int savings_estimated_turns
        float savings_estimated_actual_spend
        float savings_estimated_saved_spend
        float classifier_cost
        int classifier_cost_recorded_turns
    }
    LiteLLM_ShadowEvalJob {
        string id PK
        string group_id
        string target_type
        string target_id
        string router_name
        string[] router_names
        string[] models
        string direction
        string baseline_model
        string judge_model
        float shadow_percentage
        int max_turns
        float max_budget
        datetime created_at
        string created_by
        datetime ends_at
        datetime stopped_at
        string stopped_by
    }
    LiteLLM_ShadowEvalAttempt {
        string id PK
        string job_id
        string request_id
        string outcome
        string router_name
        string tier
        string real_model
        string shadow_model
        float confidence
        float judge_cost
        float shadow_cost
        float real_cost
        float real_classifier_cost
        float shadow_classifier_cost
        boolean real_cache_hit
        string error
        datetime created_at
    }
    LiteLLM_ShadowEvalFunnel {
        string job_id PK
        int not_sampled
        int unjudgeable
        int shed
        int withheld
    }
    LiteLLM_WorkflowRun {
        string run_id PK
        string session_id UK
        string workflow_type
        string status
        string created_by
        datetime created_at
        datetime updated_at
        json input
        json output
        json metadata
    }
    LiteLLM_WorkflowEvent {
        string event_id PK
        string run_id FK, UK
        string event_type
        string step_name
        int sequence_number UK
        json data
        datetime created_at
    }
    LiteLLM_WorkflowMessage {
        string message_id PK
        string run_id FK, UK
        string role
        string content
        int sequence_number UK
        string session_id
        datetime created_at
    }
    LiteLLM_BackgroundInteractionSettlement {
        string interaction_id PK
        string custom_llm_provider
        json create_context
        datetime created_at
        datetime claimed_at
        string claimed_by
        datetime settled_at
        string outcome
    }
    LiteLLM_BackgroundResponseSettlement {
        string response_id PK
        datetime settled_at
    }
    LiteLLM_Lens {
        string id PK
        int version
        json data
        datetime due_at
    }
    LiteLLM_LensRun {
        string id PK
        string lens_id
        datetime created_at
        json data
    }
    LiteLLM_LensReview {
        string lens_id PK, FK
        string criteria_key PK
        string execution_id PK
        json data
    }
    LiteLLM_LensWorker {
        string id PK
        string token_hash UK
        json data
    }
    LiteLLM_LensIngestionKey {
        string id PK
        json data
    }
    LiteLLM_LensDataset {
        string id PK
        int revision PK
        datetime created_at
        json data
    }
    LiteLLM_LensSignalConfig {
        string id PK
        json data
    }
    LiteLLM_LensTraceSignal {
        string trace_id PK
        string trace_ref PK
        string config_key
        int span_count
        datetime claimed_until
        datetime classified_at
        json data
    }
    LiteLLM_TelemetryReport {
        string id PK
        float window_start
        float window_end
        json report
        datetime created_at
    }
    LiteLLM_SpendLogDelivery {
        string delivery_id PK
        string request_id
        accountingdeliverystate state
        json accounting_payload
        boolean is_batch
        int attempts
        datetime next_attempt_at
        datetime applied_at
        string last_error
        datetime created_at
    }
    LiteLLM_SubscriptionAccountTable {
        string subscription_account_id PK
        string custom_llm_provider UK
        string account_id UK
        string label
        float monthly_fee
        string currency
        string billing_period_start
        datetime created_at
        string created_by
        datetime updated_at
        string updated_by
    }
    LiteLLM_Alert {
        bigint id PK
        string detection_key UK
        string type
        string resource_type
        string resource_id
        string status
        datetime window_start
        datetime window_end
        json evidence
        datetime created_at
        datetime updated_at
        string updated_by
    }
    LiteLLM_PerformanceAlertState {
        boolean enabled
        int alerts_used
        int id PK
        string owner
        datetime next_run_at
        string day
        string scope
        string resume_after
        boolean wrapped
        int checked
        int skipped
        string start_scope
        string start_after
        datetime completed_at
        datetime updated_at
    }
```