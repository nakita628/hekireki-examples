import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export interface api_keys {
  id: Generated<number>
  name: string | null
  secret: string | null
  createdBy: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface workspace_documents {
  id: Generated<number>
  docId: string
  filename: string
  docpath: string
  workspaceId: number
  metadata: string | null
  pinned: Generated<number | null>
  watched: Generated<number | null>
  createdAt: string
  lastUpdatedAt: string
}

export interface invites {
  id: Generated<number>
  code: string
  status: Generated<string>
  claimedBy: number | null
  workspaceIds: string | null
  createdAt: string
  createdBy: number
  lastUpdatedAt: string
}

export interface system_settings {
  id: Generated<number>
  label: string
  value: string | null
  createdAt: string
  lastUpdatedAt: string
}

export interface users {
  id: Generated<number>
  username: string | null
  password: string
  pfpFilename: string | null
  role: Generated<string>
  suspended: Generated<number>
  seen_recovery_codes: Generated<number | null>
  createdAt: string
  lastUpdatedAt: string
  dailyMessageLimit: number | null
  bio: Generated<string | null>
  web_push_subscription_config: string | null
}

export interface recovery_codes {
  id: Generated<number>
  user_id: number
  code_hash: string
  createdAt: string
}

export interface password_reset_tokens {
  id: Generated<number>
  user_id: number
  token: string
  expiresAt: string
  createdAt: string
}

export interface document_vectors {
  id: Generated<number>
  docId: string
  vectorId: string
  createdAt: string
  lastUpdatedAt: string
}

export interface workspaces {
  id: Generated<number>
  name: string
  slug: string
  vectorTag: string | null
  createdAt: string
  openAiTemp: number | null
  openAiHistory: Generated<number>
  lastUpdatedAt: string
  openAiPrompt: string | null
  similarityThreshold: Generated<number | null>
  chatProvider: string | null
  chatModel: string | null
  topN: Generated<number | null>
  chatMode: Generated<string | null>
  pfpFilename: string | null
  agentProvider: string | null
  agentModel: string | null
  queryRefusalResponse: string | null
  vectorSearchMode: Generated<string | null>
  router_id: number | null
}

export interface workspace_threads {
  id: Generated<number>
  name: string
  slug: string
  workspace_id: number
  user_id: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface workspace_suggested_messages {
  id: Generated<number>
  workspaceId: number
  heading: string
  message: string
  createdAt: string
  lastUpdatedAt: string
}

export interface workspace_chats {
  id: Generated<number>
  workspaceId: number
  prompt: string
  response: string
  include: Generated<number>
  user_id: number | null
  thread_id: number | null
  api_session_id: string | null
  createdAt: string
  lastUpdatedAt: string
  feedbackScore: number | null
  memory_processed: number | null
}

export interface workspace_agent_invocations {
  id: Generated<number>
  uuid: string
  prompt: string
  closed: Generated<number>
  user_id: number | null
  thread_id: number | null
  workspace_id: number
  createdAt: string
  lastUpdatedAt: string
}

export interface workspace_users {
  id: Generated<number>
  user_id: number
  workspace_id: number
  createdAt: string
  lastUpdatedAt: string
}

export interface cache_data {
  id: Generated<number>
  name: string
  data: string
  belongsTo: string | null
  byId: number | null
  expiresAt: string | null
  createdAt: string
  lastUpdatedAt: string
}

export interface embed_configs {
  id: Generated<number>
  uuid: string
  enabled: Generated<number>
  chat_mode: Generated<string>
  allowlist_domains: string | null
  allow_model_override: Generated<number>
  allow_temperature_override: Generated<number>
  allow_prompt_override: Generated<number>
  max_chats_per_day: number | null
  max_chats_per_session: number | null
  message_limit: Generated<number | null>
  workspace_id: number
  createdBy: number | null
  usersId: number | null
  createdAt: string
}

export interface embed_chats {
  id: Generated<number>
  prompt: string
  response: string
  session_id: string
  include: Generated<number>
  connection_information: string | null
  embed_id: number
  usersId: number | null
  createdAt: string
}

export interface event_logs {
  id: Generated<number>
  event: string
  metadata: string | null
  userId: number | null
  occurredAt: string
}

export interface slash_command_presets {
  id: Generated<number>
  command: string
  prompt: string
  description: string
  uid: Generated<number>
  userId: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface document_sync_queues {
  id: Generated<number>
  staleAfterMs: Generated<number>
  nextSyncAt: string
  createdAt: string
  lastSyncedAt: string
  workspaceDocId: number
}

export interface document_sync_executions {
  id: Generated<number>
  queueId: number
  status: Generated<string>
  result: string | null
  createdAt: string
}

export interface browser_extension_api_keys {
  id: Generated<number>
  key: string
  user_id: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface temporary_auth_tokens {
  id: Generated<number>
  token: string
  userId: number
  expiresAt: string
  createdAt: string
}

export interface system_prompt_variables {
  id: Generated<number>
  key: string
  value: string | null
  description: string | null
  type: Generated<string>
  userId: number | null
  createdAt: string
  updatedAt: string
}

export interface prompt_history {
  id: Generated<number>
  workspaceId: number
  prompt: string
  modifiedBy: number | null
  modifiedAt: string
}

export interface desktop_mobile_devices {
  id: Generated<number>
  deviceOs: string
  deviceName: string
  token: string
  approved: Generated<number>
  userId: number | null
  createdAt: string
}

export interface workspace_parsed_files {
  id: Generated<number>
  filename: string
  workspaceId: number
  userId: number | null
  threadId: number | null
  metadata: string | null
  tokenCountEstimate: Generated<number | null>
  createdAt: string
}

export interface external_communication_connectors {
  id: Generated<number>
  type: string
  config: Generated<string>
  active: Generated<number>
  createdAt: string
  lastUpdatedAt: string
}

export interface scheduled_jobs {
  id: Generated<number>
  name: string
  prompt: string
  tools: string | null
  schedule: string
  enabled: Generated<number>
  lastRunAt: string | null
  nextRunAt: string | null
  createdAt: string
  updatedAt: string
}

export interface scheduled_job_runs {
  id: Generated<number>
  jobId: number
  status: Generated<string>
  result: string | null
  error: string | null
  startedAt: string
  completedAt: string | null
  readAt: string | null
}

export interface memories {
  id: Generated<number>
  user_id: number | null
  workspace_id: number | null
  scope: Generated<string>
  content: string
  last_used_at: string | null
  created_at: string
  updated_at: string
}

export interface model_routers {
  id: Generated<number>
  name: string
  description: string | null
  fallback_provider: string
  fallback_model: string
  cooldown_seconds: Generated<number>
  created_by: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface model_router_rules {
  id: Generated<number>
  router_id: number
  enabled: Generated<number>
  priority: number
  type: Generated<string>
  title: string
  description: string | null
  condition_logic: string | null
  conditions: string | null
  route_provider: string
  route_model: string
  created_by: number | null
  createdAt: string
  lastUpdatedAt: string
}

export interface DB {
  api_keys: api_keys
  workspace_documents: workspace_documents
  invites: invites
  system_settings: system_settings
  users: users
  recovery_codes: recovery_codes
  password_reset_tokens: password_reset_tokens
  document_vectors: document_vectors
  workspaces: workspaces
  workspace_threads: workspace_threads
  workspace_suggested_messages: workspace_suggested_messages
  workspace_chats: workspace_chats
  workspace_agent_invocations: workspace_agent_invocations
  workspace_users: workspace_users
  cache_data: cache_data
  embed_configs: embed_configs
  embed_chats: embed_chats
  event_logs: event_logs
  slash_command_presets: slash_command_presets
  document_sync_queues: document_sync_queues
  document_sync_executions: document_sync_executions
  browser_extension_api_keys: browser_extension_api_keys
  temporary_auth_tokens: temporary_auth_tokens
  system_prompt_variables: system_prompt_variables
  prompt_history: prompt_history
  desktop_mobile_devices: desktop_mobile_devices
  workspace_parsed_files: workspace_parsed_files
  external_communication_connectors: external_communication_connectors
  scheduled_jobs: scheduled_jobs
  scheduled_job_runs: scheduled_job_runs
  memories: memories
  model_routers: model_routers
  model_router_rules: model_router_rules
}
