import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type JobStatus = 'ACTIVE' | 'INACTIVE'

export type ScheduledJobRunStatus = 'PENDING' | 'RUNNING' | 'SUCCEEDED' | 'FAILED' | 'CANCELLED'

export type AccountingDeliveryState = 'pending' | 'applied' | 'parked'

export interface LiteLLM_BudgetTable {
  budget_id: string
  name: string | null
  max_budget: number | null
  soft_budget: number | null
  max_parallel_requests: number | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  tpd_limit: bigint | null
  model_max_budget: unknown
  model_tpm_limit: unknown
  model_rpm_limit: unknown
  budget_duration: string | null
  budget_limits: unknown
  cap_policy: string | null
  budget_reset_at: Timestamp | null
  allowed_models: Generated<string[]>
  temp_budget_increase: number | null
  temp_budget_expiry: Timestamp | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_CustomerOrganizationTable {
  spend: Generated<number>
  budget_template_ids: Generated<string[]>
  budget_id: string | null
  customer_org_id: string
  alias: string | null
  default_budget_template_id: string | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_CustomerTeamTable {
  spend: Generated<number>
  customer_team_id: string
  alias: string | null
  parent_org_id: string
  budget_id: string | null
  budget_override_id: string | null
  budget_override_expires_at: Timestamp | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_CustomerMemberTable {
  spend: Generated<number>
  customer_member_id: string
  alias: string | null
  parent_org_id: string
  parent_team_ids: Generated<string[]>
  budget_id: string | null
  budget_override_id: string | null
  budget_override_expires_at: Timestamp | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_CustomerBudgetIncreaseRequest {
  request_id: string
  customer_member_id: string
  requested_max_budget: number
  expires_at: Timestamp | null
  reason: string | null
  status: Generated<string>
  submitted_by: string
  submitted_at: Generated<Timestamp>
  reviewed_by: string | null
  reviewed_at: Timestamp | null
  review_notes: string | null
}

export interface LiteLLM_CredentialsTable {
  credential_id: string
  credential_name: string
  display_name: string | null
  credential_values: unknown
  credential_info: unknown
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_ProxyModelTable {
  model_id: string
  model_name: string
  litellm_params: unknown
  model_info: unknown
  blocked: Generated<boolean>
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_AgentsTable {
  agent_id: string
  agent_name: string
  litellm_params: unknown
  agent_card_params: unknown
  static_headers: Generated<unknown>
  extra_headers: Generated<string[]>
  kill_switch: unknown
  agent_access_groups: Generated<string[]>
  access_group_ids: Generated<string[]>
  object_permission_id: string | null
  spend: Generated<number>
  identity_managed: Generated<boolean>
  enabled: Generated<boolean>
  execution_mode: Generated<string>
  tpm_limit: number | null
  rpm_limit: number | null
  session_tpm_limit: number | null
  session_rpm_limit: number | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_AgentIdentity {
  agent_id: string
  active: Generated<boolean>
  provider: string
  issuer: string
  tenant_id: string
  client_id: string
  service_principal_id: string | null
  required_roles: Generated<string[]>
  required_scopes: Generated<string[]>
  revision: string
  last_authenticated_at: Timestamp | null
}

export interface LiteLLM_RetiredAgentIdentity {
  binding_id: string
  agent_id: string | null
  provider: string
  issuer: string
  tenant_id: string
  client_id: string
}

export interface LiteLLM_RetiredAgent {
  original_agent_id: string
  retired_at: Generated<Timestamp>
}

export interface LiteLLM_VerifiedSubject {
  subject_id: string
  issuer: string
  tenant_id: string
  oid: string
  kind: Generated<string>
  user_id: string | null
  verified_via: Generated<string>
  verified_at: Generated<Timestamp>
}

export interface LiteLLM_OrganizationTable {
  organization_id: string
  organization_alias: string
  budget_id: string
  metadata: Generated<unknown>
  models: string[]
  spend: Generated<number>
  model_spend: Generated<unknown>
  object_permission_id: string | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_ModelTable {
  id: Generated<number>
  aliases: unknown
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_TeamTable {
  team_id: string
  team_alias: string | null
  organization_id: string | null
  object_permission_id: string | null
  admins: string[]
  members: string[]
  members_with_roles: Generated<unknown>
  metadata: Generated<unknown>
  max_budget: number | null
  soft_budget: number | null
  spend: Generated<number>
  models: string[]
  max_parallel_requests: number | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  tpd_limit: bigint | null
  budget_duration: string | null
  budget_reset_at: Timestamp | null
  blocked: Generated<boolean>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  model_spend: Generated<unknown>
  model_max_budget: Generated<unknown>
  router_settings: Generated<unknown>
  team_member_permissions: Generated<string[]>
  access_group_ids: Generated<string[]>
  policies: Generated<string[]>
  default_team_member_models: Generated<string[]>
  budget_limits: unknown
  model_id: number | null
  allow_team_guardrail_config: Generated<boolean>
}

export interface LiteLLM_ProjectTable {
  project_id: string
  project_alias: string | null
  description: string | null
  team_id: string | null
  budget_id: string | null
  metadata: Generated<unknown>
  models: string[]
  spend: Generated<number>
  model_spend: Generated<unknown>
  model_rpm_limit: Generated<unknown>
  model_tpm_limit: Generated<unknown>
  blocked: Generated<boolean>
  object_permission_id: string | null
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_DeletedTeamTable {
  id: string
  team_id: string
  team_alias: string | null
  organization_id: string | null
  object_permission_id: string | null
  admins: string[]
  members: string[]
  members_with_roles: Generated<unknown>
  metadata: Generated<unknown>
  max_budget: number | null
  soft_budget: number | null
  spend: Generated<number>
  models: string[]
  max_parallel_requests: number | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  tpd_limit: bigint | null
  budget_duration: string | null
  budget_reset_at: Timestamp | null
  blocked: Generated<boolean>
  model_spend: Generated<unknown>
  model_max_budget: Generated<unknown>
  router_settings: Generated<unknown>
  team_member_permissions: Generated<string[]>
  access_group_ids: Generated<string[]>
  policies: Generated<string[]>
  model_id: number | null
  allow_team_guardrail_config: Generated<boolean>
  created_at: Timestamp | null
  updated_at: Timestamp | null
  deleted_at: Generated<Timestamp>
  deleted_by: string | null
  deleted_by_api_key: string | null
  litellm_changed_by: string | null
}

export interface LiteLLM_UserTable {
  user_id: string
  user_alias: string | null
  team_id: string | null
  sso_user_id: string | null
  organization_id: string | null
  object_permission_id: string | null
  password: string | null
  password_reset_required: boolean | null
  last_breach_check_at: Timestamp | null
  teams: Generated<string[]>
  user_role: string | null
  max_budget: number | null
  spend: Generated<number>
  user_email: string | null
  models: string[]
  metadata: Generated<unknown>
  max_parallel_requests: number | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  budget_duration: string | null
  budget_reset_at: Timestamp | null
  allowed_cache_controls: Generated<string[]>
  policies: Generated<string[]>
  model_spend: Generated<unknown>
  model_max_budget: Generated<unknown>
  created_at: Generated<Timestamp | null>
  updated_at: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
}

export interface LiteLLM_ObjectPermissionTable {
  object_permission_id: string
  mcp_servers: Generated<string[]>
  mcp_access_groups: Generated<string[]>
  mcp_tool_permissions: unknown
  vector_stores: Generated<string[]>
  agents: Generated<string[]>
  agent_access_groups: Generated<string[]>
  models: Generated<string[]>
  blocked_tools: Generated<string[]>
  mcp_toolsets: Generated<string[]>
  search_tools: Generated<string[]>
  mcp_tool_search_enabled: boolean | null
  skills: Generated<string[]>
}

export interface LiteLLM_MCPServerTable {
  server_id: string
  server_name: string | null
  alias: string | null
  description: string | null
  instructions: string | null
  url: string | null
  spec_path: string | null
  transport: Generated<string>
  auth_type: string | null
  credentials: Generated<unknown>
  created_at: Generated<Timestamp | null>
  created_by: string | null
  updated_at: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
  updated_by: string | null
  mcp_info: Generated<unknown>
  mcp_access_groups: string[]
  allowed_tools: Generated<string[]>
  tool_name_to_display_name: Generated<unknown>
  tool_name_to_description: Generated<unknown>
  pinned_tools: Generated<unknown>
  extra_headers: Generated<string[]>
  static_headers: Generated<unknown>
  env_vars: Generated<unknown>
  status: Generated<string | null>
  last_health_check: Timestamp | null
  health_check_error: string | null
  command: string | null
  args: Generated<string[]>
  env: Generated<unknown>
  issuer: string | null
  authorization_url: string | null
  token_url: string | null
  registration_url: string | null
  oauth2_flow: string | null
  token_exchange_endpoint: string | null
  audience: string | null
  subject_token_type: string | null
  token_exchange_profile: string | null
  allow_all_keys: Generated<boolean>
  available_on_public_internet: Generated<boolean>
  delegate_auth_to_upstream: Generated<boolean>
  oauth_passthrough: Generated<boolean>
  dcr_bridge: boolean | null
  per_server_oauth_discovery: Generated<boolean>
  is_byok: Generated<boolean>
  byok_description: Generated<string[]>
  byok_api_key_help_url: string | null
  source_url: string | null
  timeout: number | null
  max_concurrent_requests: number | null
  rpm: number | null
  approval_status: Generated<string | null>
  submitted_by: string | null
  submitted_at: Timestamp | null
  reviewed_at: Timestamp | null
  review_notes: string | null
}

export interface LiteLLM_MCPToolsetTable {
  toolset_id: string
  toolset_name: string
  description: string | null
  tools: Generated<unknown>
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_MCPUserCredentials {
  id: string
  user_id: string
  server_id: string
  credential_b64: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_UserProviderCredentials {
  id: string
  user_id: string
  credential_name: string
  provider: string
  credential_b64: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_MCPUserEnvVars {
  id: string
  user_id: string
  server_id: string
  values_b64: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_MCPServerOAuthClient {
  server_id: string
  credentials: unknown
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_SSOIdentityAssertion {
  user_id: string
  assertion_b64: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_VerificationToken {
  token: string
  key_name: string | null
  key_alias: string | null
  soft_budget_cooldown: Generated<boolean>
  spend: Generated<number>
  total_spend: Generated<number>
  expires: Timestamp | null
  models: string[]
  aliases: Generated<unknown>
  config: Generated<unknown>
  router_settings: Generated<unknown>
  user_id: string | null
  team_id: string | null
  agent_id: string | null
  project_id: string | null
  permissions: Generated<unknown>
  max_parallel_requests: number | null
  metadata: Generated<unknown>
  blocked: boolean | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  tpd_limit: bigint | null
  max_budget: number | null
  budget_duration: string | null
  budget_reset_at: Timestamp | null
  allowed_cache_controls: Generated<string[]>
  allowed_routes: Generated<string[]>
  key_type: string | null
  policies: Generated<string[]>
  access_group_ids: Generated<string[]>
  model_spend: Generated<unknown>
  model_max_budget: Generated<unknown>
  budget_fallbacks: Generated<unknown>
  budget_id: string | null
  organization_id: string | null
  object_permission_id: string | null
  created_at: Generated<Timestamp | null>
  created_by: string | null
  updated_at: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
  updated_by: string | null
  settings_updated_at: Timestamp | null
  last_active: Timestamp | null
  rotation_count: Generated<number | null>
  auto_rotate: Generated<boolean | null>
  rotation_interval: string | null
  last_rotation_at: Timestamp | null
  key_rotation_at: Timestamp | null
  budget_limits: unknown
}

export interface LiteLLM_JWTKeyMapping {
  id: string
  jwt_issuer: Generated<string>
  jwt_claim_name: string
  jwt_claim_value: string
  token: string
  description: string | null
  is_active: Generated<boolean>
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_DeprecatedVerificationToken {
  id: string
  token: string
  active_token_id: string
  revoke_at: Timestamp
  created_at: Generated<Timestamp>
}

export interface LiteLLM_DeletedVerificationToken {
  id: string
  token: string
  key_name: string | null
  key_alias: string | null
  soft_budget_cooldown: Generated<boolean>
  spend: Generated<number>
  total_spend: Generated<number>
  expires: Timestamp | null
  models: string[]
  aliases: Generated<unknown>
  config: Generated<unknown>
  user_id: string | null
  team_id: string | null
  agent_id: string | null
  project_id: string | null
  permissions: Generated<unknown>
  max_parallel_requests: number | null
  metadata: Generated<unknown>
  blocked: boolean | null
  tpm_limit: bigint | null
  rpm_limit: bigint | null
  tpd_limit: bigint | null
  max_budget: number | null
  budget_duration: string | null
  budget_reset_at: Timestamp | null
  allowed_cache_controls: Generated<string[]>
  allowed_routes: Generated<string[]>
  key_type: string | null
  policies: Generated<string[]>
  access_group_ids: Generated<string[]>
  model_spend: Generated<unknown>
  model_max_budget: Generated<unknown>
  budget_fallbacks: Generated<unknown>
  router_settings: Generated<unknown>
  budget_id: string | null
  organization_id: string | null
  object_permission_id: string | null
  created_at: Timestamp | null
  created_by: string | null
  updated_at: Timestamp | null
  updated_by: string | null
  settings_updated_at: Timestamp | null
  last_active: Timestamp | null
  rotation_count: Generated<number | null>
  auto_rotate: Generated<boolean | null>
  rotation_interval: string | null
  last_rotation_at: Timestamp | null
  key_rotation_at: Timestamp | null
  deleted_at: Generated<Timestamp>
  deleted_by: string | null
  deleted_by_api_key: string | null
  litellm_changed_by: string | null
}

export interface LiteLLM_EndUserTable {
  user_id: string
  alias: string | null
  spend: Generated<number>
  allowed_model_region: string | null
  default_model: string | null
  models: Generated<string[]>
  budget_id: string | null
  object_permission_id: string | null
  blocked: Generated<boolean>
}

export interface LiteLLM_ModelAccessGroupBudgetTable {
  access_group_name: string
  spend: Generated<number>
  budget_id: string | null
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_TagTable {
  tag_name: string
  description: string | null
  models: string[]
  model_info: unknown
  spend: Generated<number>
  budget_id: string | null
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_Config {
  param_name: string
  param_value: unknown
  last_run_at: Timestamp | null
  reload_revision: Generated<bigint>
}

export interface LiteLLM_SpendLogs {
  request_id: string
  call_type: string
  api_key: Generated<string>
  spend: Generated<number>
  total_tokens: Generated<number>
  prompt_tokens: Generated<number>
  completion_tokens: Generated<number>
  startTime: Timestamp
  endTime: Timestamp
  request_duration_ms: number | null
  completionStartTime: Timestamp | null
  model: Generated<string>
  model_id: Generated<string | null>
  model_group: Generated<string | null>
  custom_llm_provider: Generated<string | null>
  api_base: Generated<string | null>
  user: Generated<string | null>
  metadata: Generated<unknown>
  cache_hit: Generated<string | null>
  cache_key: Generated<string | null>
  request_tags: Generated<unknown>
  team_id: string | null
  organization_id: string | null
  end_user: string | null
  requester_ip_address: string | null
  messages: Generated<unknown>
  response: Generated<unknown>
  session_id: string | null
  status: string | null
  mcp_namespaced_tool_name: string | null
  billing_agent_id: string | null
  agent_id: string | null
  proxy_server_request: Generated<unknown>
  litellm_call_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_BudgetWindowSpend {
  entity_type: string
  entity_id: string
  window_duration: string
  window_start: Timestamp
  spend: Generated<number>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_ErrorLogs {
  request_id: string
  startTime: Timestamp
  endTime: Timestamp
  api_base: Generated<string>
  model_group: Generated<string>
  litellm_model_name: Generated<string>
  model_id: Generated<string>
  request_kwargs: Generated<unknown>
  exception_type: Generated<string>
  exception_string: Generated<string>
  status_code: Generated<string>
}

export interface LiteLLM_UserNotifications {
  request_id: string
  user_id: string
  models: string[]
  justification: string
  status: string
}

export interface LiteLLM_TeamMembership {
  user_id: string
  team_id: string
  spend: Generated<number>
  total_spend: Generated<number>
  budget_id: string | null
}

export interface LiteLLM_OrganizationMembership {
  user_id: string
  organization_id: string
  user_role: string | null
  spend: Generated<number | null>
  budget_id: string | null
  created_at: Generated<Timestamp | null>
  updated_at: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
}

export interface LiteLLM_InvitationLink {
  id: string
  user_id: string
  is_accepted: Generated<boolean>
  accepted_at: Timestamp | null
  expires_at: Timestamp
  created_at: Timestamp
  created_by: string
  updated_at: Timestamp
  updated_by: string
}

export interface LiteLLM_AuditLog {
  id: string
  updated_at: Generated<Timestamp>
  changed_by: Generated<string>
  changed_by_api_key: Generated<string>
  action: string
  table_name: string
  object_id: string
  before_value: unknown
  updated_values: unknown
}

export interface LiteLLM_DailyUserSpend {
  id: string
  user_id: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyGlobalSpend {
  id: string
  date: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyOrganizationSpend {
  id: string
  organization_id: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyCustomerMemberSpend {
  id: string
  date: string
  customer_org_id: string
  customer_team_id: Generated<string>
  customer_member_id: Generated<string>
  model: Generated<string>
  model_group: Generated<string>
  custom_llm_provider: Generated<string>
  mcp_namespaced_tool_name: Generated<string>
  endpoint: Generated<string>
  api_key: Generated<string>
  spend: Generated<number>
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyEndUserSpend {
  id: string
  end_user_id: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyAgentSpend {
  id: string
  agent_id: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyTeamSpend {
  id: string
  team_id: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  ptu_flat_cost: Generated<number>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyTagSpend {
  id: string
  request_id: string | null
  tag: string | null
  date: string
  api_key: string
  model: string | null
  model_group: string | null
  custom_llm_provider: string | null
  mcp_namespaced_tool_name: string | null
  endpoint: string | null
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  cache_read_input_tokens: Generated<bigint>
  cache_creation_input_tokens: Generated<bigint>
  compression_saved_tokens: Generated<bigint>
  compression_savings_spend: Generated<number>
  prompt_caching_savings_spend: Generated<number>
  gateway_injected_caching_savings_spend: Generated<number>
  autorouter_savings_spend: Generated<number>
  guardrail_spend: Generated<number>
  spend: Generated<number>
  api_requests: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_ProxyWorkerHeartbeat {
  worker_id: string
  hostname: string
  started_at: Generated<Timestamp>
  last_heartbeat_at: Generated<Timestamp>
}

export interface LiteLLM_CronJob {
  cronjob_id: string
  pod_id: string
  status: Generated<JobStatus>
  last_updated: Generated<Timestamp>
  ttl: Timestamp
}

export interface LiteLLM_ScheduledJobRun {
  id: string
  job_name: string
  lock_group: string
  schedule: string
  scheduled_for: Timestamp
  status: Generated<ScheduledJobRunStatus>
  claimed_by: string | null
  claimed_at: Timestamp | null
  lease_expires_at: Timestamp | null
  finished_at: Timestamp | null
  created_at: Generated<Timestamp>
}

export interface LiteLLM_ManagedFileTable {
  id: string
  unified_file_id: string
  file_object: unknown
  model_mappings: unknown
  flat_model_file_ids: Generated<string[]>
  storage_backend: string | null
  storage_url: string | null
  created_at: Generated<Timestamp>
  created_by: string | null
  team_id: string | null
  updated_at: Timestamp
  updated_by: string | null
}

export interface LiteLLM_ManagedObjectTable {
  id: string
  unified_object_id: string
  model_object_id: string
  file_object: unknown
  file_purpose: string
  status: string | null
  batch_processed: Generated<boolean>
  created_at: Generated<Timestamp>
  created_by: string | null
  team_id: string | null
  org_id: string | null
  api_key: string | null
  request_tags: Generated<unknown>
  end_user_id: string | null
  updated_at: Timestamp
  updated_by: string | null
}

export interface LiteLLM_ManagedFileContentTable {
  id: string
  content: Buffer
  created_at: Generated<Timestamp>
}

export interface LiteLLM_ManagedVectorStoreTable {
  id: string
  unified_resource_id: string
  resource_object: unknown
  model_mappings: unknown
  flat_model_resource_ids: Generated<string[]>
  storage_backend: string | null
  storage_url: string | null
  created_at: Generated<Timestamp>
  created_by: string | null
  team_id: string | null
  updated_at: Timestamp
  updated_by: string | null
}

export interface LiteLLM_ManagedVectorStoresTable {
  vector_store_id: string
  custom_llm_provider: string
  vector_store_name: string | null
  vector_store_description: string | null
  vector_store_metadata: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  litellm_credential_name: string | null
  litellm_params: unknown
  team_id: string | null
  user_id: string | null
}

export interface LiteLLM_GuardrailsTable {
  guardrail_id: string
  guardrail_name: string
  litellm_params: unknown
  guardrail_info: unknown
  team_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  status: Generated<string>
  submitted_at: Timestamp | null
  reviewed_at: Timestamp | null
}

export interface LiteLLM_DailyGuardrailMetrics {
  guardrail_id: string
  date: string
  requests_evaluated: Generated<bigint>
  passed_count: Generated<bigint>
  blocked_count: Generated<bigint>
  flagged_count: Generated<bigint>
  avg_score: number | null
  avg_latency_ms: number | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyGuardrailUsageUnits {
  guardrail_id: string
  date: string
  team_id: string
  api_key: string
  usage_unit: string
  units: Generated<bigint>
  cost: number | null
  untracked_units: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyPolicyMetrics {
  policy_id: string
  date: string
  requests_evaluated: Generated<bigint>
  passed_count: Generated<bigint>
  blocked_count: Generated<bigint>
  flagged_count: Generated<bigint>
  avg_score: number | null
  avg_latency_ms: number | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_SpendLogGuardrailIndex {
  request_id: string
  guardrail_id: string
  policy_id: string | null
  start_time: Timestamp
}

export interface LiteLLM_SpendLogToolIndex {
  request_id: string
  tool_name: string
  start_time: Timestamp
}

export interface LiteLLM_DailyToolSpend {
  date: string
  tool_name: string
  spend: Generated<number>
  total_tokens: Generated<bigint>
  request_count: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyModelUsage {
  date: string
  model_group: string
  model: string
  custom_llm_provider: string
  task_type: string
  spend: Generated<number>
  prompt_tokens: Generated<bigint>
  completion_tokens: Generated<bigint>
  request_count: Generated<bigint>
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  total_response_time_ms: Generated<bigint>
  timed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyGatewayRequests {
  date: string
  category: string
  route: string
  successful_requests: Generated<bigint>
  failed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyGatewayFailedRequests {
  date: string
  category: string
  route: string
  status_code: number
  failed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_DailyRequestErrors {
  date: string
  api_key: string
  team_id: Generated<string>
  user_id: Generated<string>
  model_group: Generated<string>
  status_code: number
  failed_requests: Generated<bigint>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_PromptTable {
  id: string
  prompt_id: string
  version: Generated<number>
  environment: Generated<string>
  created_by: string | null
  litellm_params: unknown
  prompt_info: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_HealthCheckTable {
  health_check_id: string
  model_name: string
  model_id: string | null
  status: string
  healthy_count: Generated<number>
  unhealthy_count: Generated<number>
  error_message: string | null
  response_time_ms: number | null
  details: unknown
  checked_by: string | null
  checked_at: Generated<Timestamp>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_SearchToolsTable {
  search_tool_id: string
  search_tool_name: string
  litellm_params: unknown
  search_tool_info: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_SSOConfig {
  id: Generated<string>
  sso_settings: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_ManagedVectorStoreIndexTable {
  id: string
  index_name: string
  litellm_params: unknown
  index_info: unknown
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Timestamp
  updated_by: string | null
}

export interface LiteLLM_CacheConfig {
  id: Generated<string>
  cache_settings: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_UISettings {
  id: Generated<string>
  ui_settings: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_ConfigOverrides {
  config_type: string
  config_value: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface LiteLLM_SkillsTable {
  skill_id: string
  display_title: string | null
  description: string | null
  instructions: string | null
  source: Generated<string>
  latest_version: string | null
  file_content: Buffer | null
  file_name: string | null
  file_type: string | null
  metadata: Generated<unknown>
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_PolicyTable {
  policy_id: string
  policy_name: string
  version_number: Generated<number>
  version_status: Generated<string>
  parent_version_id: string | null
  is_latest: Generated<boolean>
  published_at: Timestamp | null
  production_at: Timestamp | null
  inherit: string | null
  description: string | null
  guardrails_add: Generated<string[]>
  guardrails_remove: Generated<string[]>
  condition: Generated<unknown>
  pipeline: unknown
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_PolicyAttachmentTable {
  attachment_id: string
  policy_name: string
  scope: string | null
  teams: Generated<string[]>
  keys: Generated<string[]>
  models: Generated<string[]>
  tags: Generated<string[]>
  priority: number | null
  is_default: Generated<boolean>
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_ToolTable {
  tool_id: string
  tool_name: string
  origin: string | null
  input_policy: Generated<string>
  output_policy: Generated<string>
  call_count: Generated<number>
  assignments: Generated<unknown>
  key_hash: string | null
  team_id: string | null
  key_alias: string | null
  user_agent: string | null
  last_used_at: Timestamp | null
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_AccessGroupTable {
  access_group_id: string
  access_group_name: string
  description: string | null
  access_model_names: Generated<string[]>
  access_mcp_server_ids: Generated<string[]>
  mcp_tool_permissions: unknown
  access_agent_ids: Generated<string[]>
  access_skill_names: Generated<string[]>
  access_vector_store_ids: Generated<string[]>
  assigned_team_ids: Generated<string[]>
  assigned_key_ids: Generated<string[]>
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_ClaudeCodePluginTable {
  id: string
  name: string
  version: string | null
  description: string | null
  manifest_json: string | null
  files_json: Generated<string | null>
  enabled: Generated<boolean>
  created_at: Generated<Timestamp | null>
  updated_at: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
  created_by: string | null
  anonymous_download_count: Generated<bigint>
}

export interface LiteLLM_ClaudeCodePluginVersion {
  id: string
  plugin_id: string
  version: string | null
  description: string | null
  manifest_json: string | null
  published_at: Timestamp | null
  published_by: string | null
  archive_available: Generated<boolean>
  content: Buffer | null
}

export interface LiteLLM_SkillSubmission {
  id: string
  plugin_name: string
  operation: string
  status: Generated<string>
  proposal_json: string
  archive_content: Buffer | null
  submitted_by: string
  created_at: Generated<Timestamp>
  base_plugin_id: string | null
  base_updated_at: Timestamp | null
  reviewed_by: string | null
  reviewed_at: Timestamp | null
  rejection_reason: string | null
  scan_json: string | null
}

export interface LiteLLM_ClaudeCodePluginDownload {
  id: Generated<bigint>
  plugin_id: string
  downloaded_at: Generated<Timestamp>
  version: string | null
  digest: string
  authenticated: boolean
  user_id: string | null
  user_email: string | null
  key_alias: string | null
  team_id: string | null
  team_alias: string | null
}

export interface LiteLLM_ClaudeCodePluginUpload {
  plugin_name: string
  content: Buffer
}

export interface LiteLLM_MemoryTable {
  memory_id: string
  key: string
  value: string
  metadata: unknown
  user_id: string | null
  team_id: string | null
  created_at: Generated<Timestamp>
  created_by: string | null
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_AdaptiveRouterState {
  router_name: string
  request_type: string
  model_name: string
  alpha: number
  beta: number
  total_samples: Generated<number>
  last_updated_at: Generated<Timestamp>
}

export interface LiteLLM_AdaptiveRouterSession {
  session_id: string
  router_name: string
  model_name: string
  classified_type: string
  misalignment_count: Generated<number>
  stagnation_count: Generated<number>
  disengagement_count: Generated<number>
  satisfaction_count: Generated<number>
  failure_count: Generated<number>
  loop_count: Generated<number>
  exhaustion_count: Generated<number>
  last_user_content: string | null
  last_assistant_content: string | null
  tool_call_history: Generated<unknown>
  pending_tool_calls: Generated<unknown>
  turn_count: Generated<number>
  last_processed_turn: Generated<number>
  clean_credit_awarded: Generated<boolean>
  terminal_status: number | null
  last_activity_at: Generated<Timestamp>
}

export interface LiteLLM_AutoRouterBaselineComparison {
  scope: string
  api_key: string
  session_id: string
  router_name: string
  initial_equivalent: boolean
  revision: Generated<bigint>
  published_revision: Generated<bigint>
  history: string | null
  attempted_at: Timestamp | null
  retired: Generated<boolean>
  updated_at: Generated<Timestamp>
}

export interface LiteLLM_AutoRouterBaselineObservation {
  request_id: string
  scope: string
  started_at: number
  revision: bigint
  data: string
  publication: string | null
  conflicted: Generated<boolean>
}

export interface LiteLLM_AutoRouterSession {
  api_key: string
  session_id: string
  router_name: string
  router_type: string
  first_turn_at: Timestamp
  last_turn_at: Timestamp
  last_model: string
  models: Generated<unknown>
  turns: Generated<number>
  unordered_turns: Generated<number>
  covered_turns: Generated<number>
  cache_hits: Generated<number>
  same_model_turns: Generated<number>
  same_model_hits: Generated<number>
  first_visit_turns: Generated<number>
  first_visit_hits: Generated<number>
  return_turns: Generated<number>
  return_hits: Generated<number>
  return_expired_misses: Generated<number>
  return_within_ttl_misses: Generated<number>
  ttl_5m_turns: Generated<number>
  ttl_1h_turns: Generated<number>
  total_tokens: Generated<bigint>
  spend: Generated<number>
  saved_spend: Generated<number>
  savings_estimated_turns: Generated<number>
  savings_estimated_actual_spend: Generated<number>
  savings_estimated_saved_spend: Generated<number>
  savings_estimated_baseline_models: Generated<unknown>
  classifier_cost: Generated<number>
  classifier_cost_recorded_turns: Generated<number>
  tier_turns: Generated<unknown>
  baseline_models: Generated<unknown>
}

export interface LiteLLM_AutoRouterUserSession {
  user_id: string
  api_key: string
  session_id: string
  router_name: string
  router_type: string
  first_turn_at: Timestamp
  last_turn_at: Timestamp
  last_model: string
  models: Generated<unknown>
  turns: Generated<number>
  unordered_turns: Generated<number>
  covered_turns: Generated<number>
  cache_hits: Generated<number>
  same_model_turns: Generated<number>
  same_model_hits: Generated<number>
  first_visit_turns: Generated<number>
  first_visit_hits: Generated<number>
  return_turns: Generated<number>
  return_hits: Generated<number>
  return_expired_misses: Generated<number>
  return_within_ttl_misses: Generated<number>
  ttl_5m_turns: Generated<number>
  ttl_1h_turns: Generated<number>
  total_tokens: Generated<bigint>
  spend: Generated<number>
  saved_spend: Generated<number>
  savings_estimated_turns: Generated<number>
  savings_estimated_actual_spend: Generated<number>
  savings_estimated_saved_spend: Generated<number>
  savings_estimated_baseline_models: Generated<unknown>
  classifier_cost: Generated<number>
  classifier_cost_recorded_turns: Generated<number>
  tier_turns: Generated<unknown>
  baseline_models: Generated<unknown>
}

export interface LiteLLM_AutoRouterDailySpend {
  date: string
  api_key: string
  user_id: string
  router_name: string
  router_type: string
  turns: Generated<number>
  total_tokens: Generated<bigint>
  token_recorded_turns: Generated<number>
  spend: Generated<number>
  saved_spend: Generated<number>
  savings_estimated_turns: Generated<number>
  savings_estimated_actual_spend: Generated<number>
  savings_estimated_saved_spend: Generated<number>
  classifier_cost: Generated<number>
  classifier_cost_recorded_turns: Generated<number>
}

export interface LiteLLM_ShadowEvalJob {
  id: string
  group_id: string
  target_type: Generated<string>
  target_id: string
  router_name: string
  router_names: Generated<string[]>
  models: Generated<string[]>
  direction: Generated<string>
  baseline_model: string | null
  judge_model: string
  shadow_percentage: number
  max_turns: number
  max_budget: number | null
  created_at: Generated<Timestamp>
  created_by: string | null
  ends_at: Timestamp
  stopped_at: Timestamp | null
  stopped_by: string | null
}

export interface LiteLLM_ShadowEvalAttempt {
  id: string
  job_id: string
  request_id: string
  outcome: string
  router_name: string | null
  tier: string | null
  real_model: string | null
  shadow_model: string | null
  confidence: number | null
  judge_cost: Generated<number>
  shadow_cost: Generated<number>
  real_cost: number | null
  real_classifier_cost: Generated<number>
  shadow_classifier_cost: Generated<number>
  real_cache_hit: Generated<boolean>
  error: string | null
  created_at: Generated<Timestamp>
}

export interface LiteLLM_ShadowEvalFunnel {
  job_id: string
  not_sampled: Generated<number>
  unjudgeable: Generated<number>
  shed: Generated<number>
  withheld: Generated<number>
}

export interface LiteLLM_WorkflowRun {
  run_id: string
  session_id: string
  workflow_type: string
  status: Generated<string>
  created_by: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  input: unknown
  output: unknown
  metadata: unknown
}

export interface LiteLLM_WorkflowEvent {
  event_id: string
  run_id: string
  event_type: string
  step_name: string
  sequence_number: number
  data: unknown
  created_at: Generated<Timestamp>
}

export interface LiteLLM_WorkflowMessage {
  message_id: string
  run_id: string
  role: string
  content: string
  sequence_number: number
  session_id: string | null
  created_at: Generated<Timestamp>
}

export interface LiteLLM_BackgroundInteractionSettlement {
  interaction_id: string
  custom_llm_provider: string
  create_context: unknown
  created_at: Generated<Timestamp>
  claimed_at: Timestamp | null
  claimed_by: string | null
  settled_at: Timestamp | null
  outcome: string | null
}

export interface LiteLLM_BackgroundResponseSettlement {
  response_id: string
  settled_at: Generated<Timestamp>
}

export interface LiteLLM_Lens {
  id: string
  version: Generated<number>
  data: unknown
  due_at: Generated<Timestamp | null>
}

export interface LiteLLM_LensRun {
  id: string
  lens_id: string
  created_at: Timestamp
  data: unknown
}

export interface LiteLLM_LensReview {
  lens_id: string
  criteria_key: string
  execution_id: string
  data: unknown
}

export interface LiteLLM_LensWorker {
  id: string
  token_hash: string
  data: unknown
}

export interface LiteLLM_LensIngestionKey {
  id: string
  data: unknown
}

export interface LiteLLM_LensDataset {
  id: string
  revision: number
  created_at: Timestamp
  data: unknown
}

export interface LiteLLM_LensSignalConfig {
  id: string
  data: unknown
}

export interface LiteLLM_LensTraceSignal {
  trace_id: string
  trace_ref: Generated<string>
  config_key: string
  span_count: number
  claimed_until: Timestamp | null
  classified_at: Timestamp | null
  data: unknown
}

export interface LiteLLM_TelemetryReport {
  id: string
  window_start: number
  window_end: number
  report: unknown
  created_at: Generated<Timestamp>
}

export interface LiteLLM_SpendLogDelivery {
  delivery_id: Generated<string>
  request_id: string
  state: Generated<AccountingDeliveryState>
  accounting_payload: unknown
  is_batch: boolean
  attempts: Generated<number>
  next_attempt_at: Generated<Timestamp>
  applied_at: Timestamp | null
  last_error: string | null
  created_at: Generated<Timestamp>
}

export interface LiteLLM_SubscriptionAccountTable {
  subscription_account_id: string
  custom_llm_provider: string
  account_id: string
  label: string | null
  monthly_fee: number
  currency: string
  billing_period_start: string
  created_at: Generated<Timestamp>
  created_by: string
  updated_at: Generated<Timestamp>
  updated_by: string
}

export interface LiteLLM_Alert {
  id: Generated<bigint>
  detection_key: string
  type: string
  resource_type: string
  resource_id: string
  status: Generated<string>
  window_start: Timestamp
  window_end: Timestamp
  evidence: unknown
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface LiteLLM_PerformanceAlertState {
  enabled: Generated<boolean>
  alerts_used: number | null
  id: number
  owner: string
  next_run_at: Timestamp
  day: string | null
  scope: string | null
  resume_after: Generated<string>
  wrapped: Generated<boolean>
  checked: Generated<number>
  skipped: Generated<number>
  start_scope: string | null
  start_after: Generated<string>
  completed_at: Timestamp | null
  updated_at: Generated<Timestamp>
}

export interface DB {
  LiteLLM_BudgetTable: LiteLLM_BudgetTable
  LiteLLM_CustomerOrganizationTable: LiteLLM_CustomerOrganizationTable
  LiteLLM_CustomerTeamTable: LiteLLM_CustomerTeamTable
  LiteLLM_CustomerMemberTable: LiteLLM_CustomerMemberTable
  LiteLLM_CustomerBudgetIncreaseRequest: LiteLLM_CustomerBudgetIncreaseRequest
  LiteLLM_CredentialsTable: LiteLLM_CredentialsTable
  LiteLLM_ProxyModelTable: LiteLLM_ProxyModelTable
  LiteLLM_AgentsTable: LiteLLM_AgentsTable
  LiteLLM_AgentIdentity: LiteLLM_AgentIdentity
  LiteLLM_RetiredAgentIdentity: LiteLLM_RetiredAgentIdentity
  LiteLLM_RetiredAgent: LiteLLM_RetiredAgent
  LiteLLM_VerifiedSubject: LiteLLM_VerifiedSubject
  LiteLLM_OrganizationTable: LiteLLM_OrganizationTable
  LiteLLM_ModelTable: LiteLLM_ModelTable
  LiteLLM_TeamTable: LiteLLM_TeamTable
  LiteLLM_ProjectTable: LiteLLM_ProjectTable
  LiteLLM_DeletedTeamTable: LiteLLM_DeletedTeamTable
  LiteLLM_UserTable: LiteLLM_UserTable
  LiteLLM_ObjectPermissionTable: LiteLLM_ObjectPermissionTable
  LiteLLM_MCPServerTable: LiteLLM_MCPServerTable
  LiteLLM_MCPToolsetTable: LiteLLM_MCPToolsetTable
  LiteLLM_MCPUserCredentials: LiteLLM_MCPUserCredentials
  LiteLLM_UserProviderCredentials: LiteLLM_UserProviderCredentials
  LiteLLM_MCPUserEnvVars: LiteLLM_MCPUserEnvVars
  LiteLLM_MCPServerOAuthClient: LiteLLM_MCPServerOAuthClient
  LiteLLM_SSOIdentityAssertion: LiteLLM_SSOIdentityAssertion
  LiteLLM_VerificationToken: LiteLLM_VerificationToken
  LiteLLM_JWTKeyMapping: LiteLLM_JWTKeyMapping
  LiteLLM_DeprecatedVerificationToken: LiteLLM_DeprecatedVerificationToken
  LiteLLM_DeletedVerificationToken: LiteLLM_DeletedVerificationToken
  LiteLLM_EndUserTable: LiteLLM_EndUserTable
  LiteLLM_ModelAccessGroupBudgetTable: LiteLLM_ModelAccessGroupBudgetTable
  LiteLLM_TagTable: LiteLLM_TagTable
  LiteLLM_Config: LiteLLM_Config
  LiteLLM_SpendLogs: LiteLLM_SpendLogs
  LiteLLM_BudgetWindowSpend: LiteLLM_BudgetWindowSpend
  LiteLLM_ErrorLogs: LiteLLM_ErrorLogs
  LiteLLM_UserNotifications: LiteLLM_UserNotifications
  LiteLLM_TeamMembership: LiteLLM_TeamMembership
  LiteLLM_OrganizationMembership: LiteLLM_OrganizationMembership
  LiteLLM_InvitationLink: LiteLLM_InvitationLink
  LiteLLM_AuditLog: LiteLLM_AuditLog
  LiteLLM_DailyUserSpend: LiteLLM_DailyUserSpend
  LiteLLM_DailyGlobalSpend: LiteLLM_DailyGlobalSpend
  LiteLLM_DailyOrganizationSpend: LiteLLM_DailyOrganizationSpend
  LiteLLM_DailyCustomerMemberSpend: LiteLLM_DailyCustomerMemberSpend
  LiteLLM_DailyEndUserSpend: LiteLLM_DailyEndUserSpend
  LiteLLM_DailyAgentSpend: LiteLLM_DailyAgentSpend
  LiteLLM_DailyTeamSpend: LiteLLM_DailyTeamSpend
  LiteLLM_DailyTagSpend: LiteLLM_DailyTagSpend
  LiteLLM_ProxyWorkerHeartbeat: LiteLLM_ProxyWorkerHeartbeat
  LiteLLM_CronJob: LiteLLM_CronJob
  LiteLLM_ScheduledJobRun: LiteLLM_ScheduledJobRun
  LiteLLM_ManagedFileTable: LiteLLM_ManagedFileTable
  LiteLLM_ManagedObjectTable: LiteLLM_ManagedObjectTable
  LiteLLM_ManagedFileContentTable: LiteLLM_ManagedFileContentTable
  LiteLLM_ManagedVectorStoreTable: LiteLLM_ManagedVectorStoreTable
  LiteLLM_ManagedVectorStoresTable: LiteLLM_ManagedVectorStoresTable
  LiteLLM_GuardrailsTable: LiteLLM_GuardrailsTable
  LiteLLM_DailyGuardrailMetrics: LiteLLM_DailyGuardrailMetrics
  LiteLLM_DailyGuardrailUsageUnits: LiteLLM_DailyGuardrailUsageUnits
  LiteLLM_DailyPolicyMetrics: LiteLLM_DailyPolicyMetrics
  LiteLLM_SpendLogGuardrailIndex: LiteLLM_SpendLogGuardrailIndex
  LiteLLM_SpendLogToolIndex: LiteLLM_SpendLogToolIndex
  LiteLLM_DailyToolSpend: LiteLLM_DailyToolSpend
  LiteLLM_DailyModelUsage: LiteLLM_DailyModelUsage
  LiteLLM_DailyGatewayRequests: LiteLLM_DailyGatewayRequests
  LiteLLM_DailyGatewayFailedRequests: LiteLLM_DailyGatewayFailedRequests
  LiteLLM_DailyRequestErrors: LiteLLM_DailyRequestErrors
  LiteLLM_PromptTable: LiteLLM_PromptTable
  LiteLLM_HealthCheckTable: LiteLLM_HealthCheckTable
  LiteLLM_SearchToolsTable: LiteLLM_SearchToolsTable
  LiteLLM_SSOConfig: LiteLLM_SSOConfig
  LiteLLM_ManagedVectorStoreIndexTable: LiteLLM_ManagedVectorStoreIndexTable
  LiteLLM_CacheConfig: LiteLLM_CacheConfig
  LiteLLM_UISettings: LiteLLM_UISettings
  LiteLLM_ConfigOverrides: LiteLLM_ConfigOverrides
  LiteLLM_SkillsTable: LiteLLM_SkillsTable
  LiteLLM_PolicyTable: LiteLLM_PolicyTable
  LiteLLM_PolicyAttachmentTable: LiteLLM_PolicyAttachmentTable
  LiteLLM_ToolTable: LiteLLM_ToolTable
  LiteLLM_AccessGroupTable: LiteLLM_AccessGroupTable
  LiteLLM_ClaudeCodePluginTable: LiteLLM_ClaudeCodePluginTable
  LiteLLM_ClaudeCodePluginVersion: LiteLLM_ClaudeCodePluginVersion
  LiteLLM_SkillSubmission: LiteLLM_SkillSubmission
  LiteLLM_ClaudeCodePluginDownload: LiteLLM_ClaudeCodePluginDownload
  LiteLLM_ClaudeCodePluginUpload: LiteLLM_ClaudeCodePluginUpload
  LiteLLM_MemoryTable: LiteLLM_MemoryTable
  LiteLLM_AdaptiveRouterState: LiteLLM_AdaptiveRouterState
  LiteLLM_AdaptiveRouterSession: LiteLLM_AdaptiveRouterSession
  LiteLLM_AutoRouterBaselineComparison: LiteLLM_AutoRouterBaselineComparison
  LiteLLM_AutoRouterBaselineObservation: LiteLLM_AutoRouterBaselineObservation
  LiteLLM_AutoRouterSession: LiteLLM_AutoRouterSession
  LiteLLM_AutoRouterUserSession: LiteLLM_AutoRouterUserSession
  LiteLLM_AutoRouterDailySpend: LiteLLM_AutoRouterDailySpend
  LiteLLM_ShadowEvalJob: LiteLLM_ShadowEvalJob
  LiteLLM_ShadowEvalAttempt: LiteLLM_ShadowEvalAttempt
  LiteLLM_ShadowEvalFunnel: LiteLLM_ShadowEvalFunnel
  LiteLLM_WorkflowRun: LiteLLM_WorkflowRun
  LiteLLM_WorkflowEvent: LiteLLM_WorkflowEvent
  LiteLLM_WorkflowMessage: LiteLLM_WorkflowMessage
  LiteLLM_BackgroundInteractionSettlement: LiteLLM_BackgroundInteractionSettlement
  LiteLLM_BackgroundResponseSettlement: LiteLLM_BackgroundResponseSettlement
  LiteLLM_Lens: LiteLLM_Lens
  LiteLLM_LensRun: LiteLLM_LensRun
  LiteLLM_LensReview: LiteLLM_LensReview
  LiteLLM_LensWorker: LiteLLM_LensWorker
  LiteLLM_LensIngestionKey: LiteLLM_LensIngestionKey
  LiteLLM_LensDataset: LiteLLM_LensDataset
  LiteLLM_LensSignalConfig: LiteLLM_LensSignalConfig
  LiteLLM_LensTraceSignal: LiteLLM_LensTraceSignal
  LiteLLM_TelemetryReport: LiteLLM_TelemetryReport
  LiteLLM_SpendLogDelivery: LiteLLM_SpendLogDelivery
  LiteLLM_SubscriptionAccountTable: LiteLLM_SubscriptionAccountTable
  LiteLLM_Alert: LiteLLM_Alert
  LiteLLM_PerformanceAlertState: LiteLLM_PerformanceAlertState
}
