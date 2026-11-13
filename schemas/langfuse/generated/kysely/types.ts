import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ApiKeyScope = 'ORGANIZATION' | 'PROJECT' | 'USER'

export type SystemRole =
  | 'OWNER'
  | 'ADMIN'
  | 'MEMBER'
  | 'VIEWER'
  | 'NONE'
  | 'LEGACY_PROJECT_API_KEY'
  | 'LEGACY_ORGANIZATION_API_KEY'
  | 'SCORES_INGEST'
  | 'INGEST'
  | 'AI_GATEWAY'

export type GatewayIngestionMode = 'none' | 'usage' | 'full'

export type GatewayProvider =
  | 'openai'
  | 'anthropic'
  | 'bedrock'
  | 'openrouter'
  | 'xai'
  | 'vercel'
  | 'azure'
  | 'google_ai_studio'

export type GatewayConnectionStatus = 'enabled' | 'disabled' | 'error'

export type InAppAgentConversationVisibilityScope = 'PERSONAL' | 'PROJECT'

export type Role = 'OWNER' | 'ADMIN' | 'MEMBER' | 'VIEWER' | 'NONE' | 'AI_GATEWAY'

export type ScoreConfigDataType = 'CATEGORICAL' | 'NUMERIC' | 'BOOLEAN' | 'TEXT'

export type AnnotationQueueStatus = 'PENDING' | 'COMPLETED'

export type AnnotationQueueObjectType = 'TRACE' | 'OBSERVATION' | 'SESSION'

export type DatasetStatus = 'ACTIVE' | 'ARCHIVED'

export type CommentObjectType = 'TRACE' | 'OBSERVATION' | 'SESSION' | 'PROMPT'

export type NotificationChannel = 'EMAIL'

export type NotificationType = 'COMMENT_MENTION'

export type AuditLogRecordType = 'USER' | 'API_KEY'

export type EvalTemplateType = 'LLM_AS_JUDGE' | 'CODE' | 'DECISION_MODEL' | 'FACET'

export type EvalTemplateSourceCodeLanguage = 'PYTHON' | 'TYPESCRIPT'

export type EvaluatorSourceCodeLanguage = 'PYTHON' | 'TYPESCRIPT'

export type JobType = 'EVAL'

export type JobConfigState = 'ACTIVE' | 'INACTIVE'

export type EvaluatorBlockReason =
  | 'LLM_CONNECTION_AUTH_INVALID'
  | 'LLM_CONNECTION_BILLING_EXHAUSTED'
  | 'LLM_CONNECTION_ENDPOINT_UNREACHABLE'
  | 'LLM_CONNECTION_MISSING'
  | 'DEFAULT_EVAL_MODEL_MISSING'
  | 'EVAL_MODEL_CONFIG_INVALID'
  | 'EVAL_MODEL_UNAVAILABLE'
  | 'PROVIDER_ACCOUNT_NOT_READY'

export type JobExecutionStatus = 'COMPLETED' | 'ERROR' | 'PENDING' | 'CANCELLED' | 'DELAYED'

export type BlobStorageIntegrationFileType = 'JSON' | 'CSV' | 'JSONL' | 'PARQUET'

export type BlobStorageIntegrationType =
  | 'S3'
  | 'S3_COMPATIBLE'
  | 'AZURE_BLOB_STORAGE'
  | 'GOOGLE_CLOUD_STORAGE'

export type ExternalMediaStorageIntegrationType = 'S3' | 'S3_COMPATIBLE'

export type StorageIntegrationAuthType = 'ACCESS_KEYS' | 'ROLE_ARN'

export type BlobStorageExportMode = 'FULL_HISTORY' | 'FROM_TODAY' | 'FROM_CUSTOM_DATE'

export type AnalyticsIntegrationExportSource =
  | 'TRACES_OBSERVATIONS'
  | 'TRACES_OBSERVATIONS_EVENTS'
  | 'EVENTS'

export type MediaAssociationOrigin =
  | 'UNKNOWN'
  | 'CLIENT_UPLOAD'
  | 'INGESTION_MEDIA_EXTRACTION'
  | 'INGESTION_FIELD_OVERFLOW'

export type DashboardWidgetViews =
  | 'TRACES'
  | 'OBSERVATIONS'
  | 'SCORES_NUMERIC'
  | 'SCORES_CATEGORICAL'
  | 'SCORES_BOOLEAN'

export type DashboardWidgetChartType =
  | 'LINE_TIME_SERIES'
  | 'AREA_TIME_SERIES'
  | 'BAR_TIME_SERIES'
  | 'HORIZONTAL_BAR'
  | 'VERTICAL_BAR'
  | 'PIE'
  | 'NUMBER'
  | 'HISTOGRAM'
  | 'PIVOT_TABLE'

export type ActionType = 'WEBHOOK' | 'SLACK' | 'GITHUB_DISPATCH' | 'PAGERDUTY'

export type ActionExecutionStatus = 'COMPLETED' | 'ERROR' | 'PENDING' | 'CANCELLED'

export type MonitorThresholdOperator = 'GT' | 'GTE' | 'LT' | 'LTE' | 'EQ' | 'NEQ'

export type MonitorView =
  | 'OBSERVATIONS'
  | 'SCORES_NUMERIC'
  | 'SCORES_CATEGORICAL'
  | 'SCORES_BOOLEAN'

export type MonitorSeverity = 'PAUSED' | 'UNKNOWN' | 'NO_DATA' | 'OK' | 'WARNING' | 'ALERT'

export type MonitorStatus = 'PAUSED' | 'ACTIVE' | 'ERROR_BAD_QUERY'

export type SurveyName = 'org_onboarding' | 'user_onboarding'

export interface Account {
  id: string
  user_id: string
  type: string
  provider: string
  providerAccountId: string
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  expires_in: number | null
  ext_expires_in: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
  refresh_token_expires_in: number | null
  created_at: number | null
}

export interface Session {
  id: string
  session_token: string
  user_id: string
  expires: Timestamp
}

export interface User {
  id: string
  name: string | null
  email: string | null
  email_verified: Timestamp | null
  password: string | null
  sessions_expired_at: Timestamp | null
  image: string | null
  admin: Generated<boolean>
  v4_beta_enabled: Generated<boolean>
  support_access: unknown
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  feature_flags: Generated<string[]>
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface Organization {
  id: string
  name: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  cloud_config: unknown
  metadata: unknown
  cloud_billing_cycle_anchor: Generated<Timestamp | null>
  cloud_billing_cycle_updated_at: Timestamp | null
  cloud_current_cycle_usage: number | null
  cloud_free_tier_usage_threshold_state: string | null
  ai_features_enabled: Generated<boolean>
  ai_telemetry_enabled: Generated<boolean>
  access_audit_log_enabled: Generated<boolean>
  feature_flag_org_defaults: Generated<string[]>
  sfdc_org_id: string | null
}

export interface Project {
  id: string
  org_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  deleted_at: Timestamp | null
  name: string
  retention_days: number | null
  has_traces: Generated<boolean>
  metadata: unknown
  home_dashboard_id: string | null
}

export interface ApiKey {
  id: string
  created_at: Generated<Timestamp>
  note: string | null
  public_key: string
  hashed_secret_key: string
  fast_hashed_secret_key: string | null
  display_secret_key: string
  last_used_at: Timestamp | null
  expires_at: Timestamp | null
  is_in_app_agent_key: Generated<boolean>
  project_id: string | null
  organization_id: string | null
  scope: Generated<ApiKeyScope>
  created_by_user_id: string | null
  user_id: string | null
  created_by_api_key_id: string | null
}

export interface RoleAssignment {
  id: string
  owner_id: string
  project_id: string | null
  org_id: string
  principal_id: string
  user_id: string | null
  api_key_id: string | null
  role_id: string
  system_role: SystemRole
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface GatewayConfig {
  organization_id: string
  default_ingestion_project_id: string | null
  ingestion_mode: Generated<GatewayIngestionMode>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface GatewayAiConnection {
  id: string
  organization_id: string
  name: string
  provider: GatewayProvider
  encrypted_credentials: string
  display_secret_key: string
  created_by_id: string | null
  status: Generated<GatewayConnectionStatus>
  allowed_models: unknown
  custom_models: Generated<unknown>
  config: unknown
  base_url: string | null
  extra_headers: string | null
  extra_header_keys: Generated<string[]>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface GatewayApiKeyAssociation {
  api_key_id: string
  metadata: Generated<unknown>
}

export interface InAppAgentConversation {
  id: string
  project_id: string
  created_by_user_id: string | null
  title: string | null
  renamed_by_user_at: Timestamp | null
  visibility_scope: Generated<InAppAgentConversationVisibilityScope>
  provider_session_id: string | null
  always_allowed_tools: Generated<string[]>
  deleted_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface InAppAgentEvent {
  project_id: string
  conversation_id: string
  run_id: string
  sequence_number: number
  type: string
  event: unknown
  created_at: Generated<Timestamp>
}

export interface InAppAgentRun {
  id: string
  project_id: string
  conversation_id: string
  triggered_by_user_id: string | null
  model: string | null
  mcp_api_key_id: string | null
  error_code: string | null
  error_message: string | null
  finished_at: Timestamp | null
  status: string | null
  request: unknown
  claimed_at: Timestamp | null
  heartbeat_at: Timestamp | null
  cancel_requested_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface InAppAgentPendingToolApproval {
  project_id: string
  conversation_id: string
  tool_call_id: string
  approval_fingerprint: string
  expires_at: Timestamp
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface BackgroundMigration {
  id: string
  name: string
  script: string
  args: unknown
  state: Generated<unknown>
  finished_at: Timestamp | null
  failed_at: Timestamp | null
  failed_reason: string | null
  worker_id: string | null
  locked_at: Timestamp | null
}

export interface LlmApiKeys {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  provider: string
  adapter: string
  display_secret_key: string
  secret_key: string
  base_url: string | null
  custom_models: Generated<string[]>
  with_default_models: Generated<boolean>
  extra_headers: string | null
  extra_header_keys: Generated<string[]>
  config: unknown
  project_id: string | null
  organization_id: string | null
}

export interface OrganizationMembership {
  id: string
  org_id: string
  user_id: string
  role: Role
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface ProjectMembership {
  org_membership_id: string
  project_id: string
  user_id: string
  role: Role
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface MembershipInvitation {
  id: string
  email: string
  org_id: string
  org_role: Role
  project_id: string | null
  project_role: Role | null
  invited_by_user_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface TraceSession {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  bookmarked: Generated<boolean>
  public: Generated<boolean>
  environment: Generated<string>
}

export interface ScoreConfig {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: string
  data_type: ScoreConfigDataType
  is_archived: Generated<boolean>
  min_value: number | null
  max_value: number | null
  categories: unknown
  description: string | null
}

export interface AnnotationQueue {
  id: string
  name: string
  description: string | null
  score_config_ids: Generated<string[]>
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface AnnotationQueueItem {
  id: string
  queue_id: string
  object_id: string
  object_type: AnnotationQueueObjectType
  status: Generated<AnnotationQueueStatus>
  locked_at: Timestamp | null
  locked_by_user_id: string | null
  annotator_user_id: string | null
  completed_at: Timestamp | null
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface AnnotationQueueAssignment {
  id: string
  project_id: string
  user_id: string
  queue_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface CronJobs {
  name: string
  last_run: Timestamp | null
  job_started_at: Timestamp | null
  state: string | null
}

export interface Dataset {
  id: string
  project_id: string
  name: string
  description: string | null
  metadata: unknown
  remote_experiment_url: string | null
  remote_experiment_payload: unknown
  remote_experiment_enabled: Generated<boolean>
  remote_experiment_secret_key: string | null
  remote_experiment_display_secret_key: string | null
  remote_experiment_request_headers: unknown
  remote_experiment_display_headers: unknown
  input_schema: unknown
  expected_output_schema: unknown
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface DatasetItem {
  id: string
  project_id: string
  status: Generated<DatasetStatus | null>
  input: unknown
  expected_output: unknown
  metadata: unknown
  source_trace_id: string | null
  source_observation_id: string | null
  dataset_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  valid_from: Generated<Timestamp>
  valid_to: Timestamp | null
  is_deleted: Generated<boolean>
}

export interface DatasetRuns {
  id: string
  project_id: string
  name: string
  description: string | null
  metadata: unknown
  dataset_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface Comment {
  id: string
  project_id: string
  object_type: CommentObjectType
  object_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  content: string
  author_user_id: string | null
  data_field: string | null
  path: Generated<string[]>
  range_start: Generated<number[]>
  range_end: Generated<number[]>
}

export interface CommentReaction {
  id: string
  project_id: string
  comment_id: string
  user_id: string
  emoji: string
  created_at: Generated<Timestamp>
}

export interface NotificationPreference {
  id: string
  user_id: string
  project_id: string
  channel: NotificationChannel
  type: NotificationType
  enabled: Generated<boolean>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface Prompt {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  created_by: string
  prompt: unknown
  name: string
  version: number
  type: Generated<string>
  is_active: boolean | null
  config: Generated<unknown>
  tags: Generated<string[]>
  labels: Generated<string[]>
  commit_message: string | null
}

export interface PromptDependency {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  parent_id: string
  child_name: string
  child_label: string | null
  child_version: number | null
}

export interface PromptProtectedLabels {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  label: string
}

export interface Skill {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  created_by: string
  name: string
  description: string
  frontmatter: Generated<unknown>
  version: number
  tags: Generated<string[]>
  labels: Generated<string[]>
  commit_message: string | null
}

export interface SkillFile {
  id: string
  created_at: Generated<Timestamp>
  project_id: string
  skill_id: string
  blob_id: string
  path: string
}

export interface SkillBlob {
  id: string
  created_at: Generated<Timestamp>
  project_id: string
  sha_256_hash: string
  content_type: string
  content_length: number
  content: string
}

export interface Model {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string | null
  model_name: string
  match_pattern: string
  start_date: Timestamp | null
  input_price: string | null
  output_price: string | null
  total_price: string | null
  unit: string | null
  tokenizer_id: string | null
  tokenizer_config: unknown
}

export interface Price {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  model_id: string
  project_id: string | null
  pricing_tier_id: string
  usage_type: string
  price: string
}

export interface PricingTier {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  model_id: string
  name: string
  is_default: Generated<boolean>
  priority: number
  conditions: unknown
}

export interface AuditLog {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  type: Generated<AuditLogRecordType>
  api_key_id: string | null
  user_id: string | null
  org_id: string
  user_org_role: string | null
  project_id: string | null
  user_project_role: string | null
  resource_type: string
  resource_id: string
  action: string
  before: string | null
  after: string | null
}

export interface AuditLogStaging {
  id: string
  created_at: Generated<Timestamp>
  payload: unknown
  failed_at: Timestamp | null
  last_error: string | null
}

export interface EvalTemplate {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string | null
  name: string
  version: number
  prompt: string | null
  type: Generated<EvalTemplateType>
  partner: string | null
  model: string | null
  provider: string | null
  model_params: unknown
  vars: Generated<string[]>
  output_schema: unknown
  source_code: string | null
  source_code_language: EvalTemplateSourceCodeLanguage | null
}

export interface Evaluator {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: string
  type: EvalTemplateType
  is_built_in: Generated<boolean>
  description: string | null
  created_by_user_id: string | null
  blocked_at: Timestamp | null
  block_reason: EvaluatorBlockReason | null
  block_message: string | null
}

export interface EvaluatorVersion {
  id: string
  created_at: Generated<Timestamp>
  evaluator_id: string
  version: number
  created_by_user_id: string | null
  prompt: string | null
  prompt_messages: unknown
  partner: string | null
  model: string | null
  provider: string | null
  model_params: unknown
  vars: Generated<string[]>
  variable_mapping: unknown
  output_definition: unknown
  source_code: string | null
  source_code_language: EvaluatorSourceCodeLanguage | null
  questions: unknown
}

export interface JobConfiguration {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  job_type: JobType
  status: Generated<JobConfigState>
  blocked_at: Timestamp | null
  block_reason: EvaluatorBlockReason | null
  block_message: string | null
  eval_template_id: string | null
  score_name: string
  filter: unknown
  target_object: string
  variable_mapping: unknown
  sampling: string
  delay: number
  time_scope: Generated<string[]>
}

export interface EvaluationRule {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  created_by_user_id: string | null
  name: string
  status: Generated<JobConfigState>
  target_object: string
  filter: unknown
  sampling: string
  delay: number
  idle_time: number | null
  time_scope: Generated<string[]>
}

export interface TracingReadinessSettings {
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  created_by: string | null
  updated_by: string | null
  trace_idle_ms: number | null
}

export interface EvaluationRuleEvaluatorAssignment {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  evaluation_rule_id: string
  evaluator_id: string
  variable_mapping: unknown
}

export interface JobExecution {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  job_configuration_id: string
  job_template_id: string | null
  status: JobExecutionStatus
  start_time: Timestamp | null
  end_time: Timestamp | null
  error: string | null
  job_input_trace_id: string | null
  job_input_trace_timestamp: Timestamp | null
  job_input_observation_id: string | null
  job_input_dataset_item_id: string | null
  job_input_dataset_item_valid_from: Timestamp | null
  job_output_score_id: string | null
  execution_trace_id: string | null
}

export interface DefaultLlmModel {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  llm_api_key_id: string
  provider: string
  adapter: string
  model: string
  model_params: unknown
}

export interface TopicConfig {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  created_by: string | null
  updated_by: string | null
  project_id: string
  enabled: Generated<boolean>
  blocked_at: Timestamp | null
  block_reason: EvaluatorBlockReason | null
  block_message: string | null
  summary_llm_api_key_id: string | null
  summary_model: string | null
  embedding_llm_api_key_id: string | null
  embedding_model: string | null
  embedding_dimensions: Generated<number>
  clustering_llm_api_key_id: string | null
  clustering_model: string | null
}

export interface SsoConfig {
  domain: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  auth_provider: string
  auth_config: unknown
}

export interface VerifiedDomain {
  id: string
  organization_id: string
  domain: string
  verification_token: string
  verified_at: Timestamp | null
  created_by_user_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface PosthogIntegration {
  project_id: string
  encrypted_posthog_api_key: string
  posthog_host_name: string
  last_sync_at: Timestamp | null
  enabled: boolean
  created_at: Generated<Timestamp>
  export_source: Generated<AnalyticsIntegrationExportSource>
  backfill: Generated<boolean>
  last_error: string | null
  last_error_at: Timestamp | null
}

export interface MixpanelIntegration {
  project_id: string
  encrypted_mixpanel_project_token: string
  mixpanel_region: string
  last_sync_at: Timestamp | null
  enabled: boolean
  created_at: Generated<Timestamp>
  export_source: Generated<AnalyticsIntegrationExportSource>
  backfill: Generated<boolean>
}

export interface BlobStorageIntegration {
  project_id: string
  type: BlobStorageIntegrationType
  bucket_name: string
  prefix: string
  auth_type: Generated<StorageIntegrationAuthType>
  access_key_id: string | null
  secret_access_key: string | null
  role_arn: string | null
  external_id: string | null
  region: string
  endpoint: string | null
  force_path_style: boolean
  next_sync_at: Timestamp | null
  last_sync_at: Timestamp | null
  enabled: boolean
  export_frequency: string
  file_type: Generated<BlobStorageIntegrationFileType>
  export_mode: Generated<BlobStorageExportMode>
  export_start_date: Timestamp | null
  export_source: Generated<AnalyticsIntegrationExportSource>
  export_field_groups: Generated<string[]>
  compressed: Generated<boolean>
  export_tuning: unknown
  run_started_at: Timestamp | null
  backfill: Generated<boolean>
  last_error: string | null
  last_error_at: Timestamp | null
  last_failure_notification_sent_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface ExternalMediaStorageIntegration {
  project_id: string
  type: ExternalMediaStorageIntegrationType
  bucket_name: string
  prefix: string | null
  auth_type: Generated<StorageIntegrationAuthType>
  access_key_id: string | null
  secret_access_key: string | null
  role_arn: string | null
  external_id: string | null
  region: string
  endpoint: string | null
  force_path_style: boolean
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface WebCalloutEndpoint {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: Generated<string>
  url: string
  enabled: Generated<boolean>
  toast_message: Generated<string>
  request_headers: string | null
  request_header_keys: Generated<string[]>
}

export interface BatchExport {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  user_id: string
  finished_at: Timestamp | null
  expires_at: Timestamp | null
  name: string
  status: string
  query: unknown
  format: string
  url: string | null
  log: string | null
}

export interface BatchAction {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  user_id: string
  action_type: string
  table_name: string
  status: string
  finished_at: Timestamp | null
  query: unknown
  config: unknown
  total_count: number | null
  processed_count: number | null
  failed_count: number | null
  log: string | null
}

export interface Media {
  id: string
  sha_256_hash: string
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  uploaded_at: Timestamp | null
  upload_http_status: number | null
  upload_http_error: string | null
  bucket_path: string
  bucket_name: string
  content_type: string
  content_length: bigint
}

export interface TraceMedia {
  id: string
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  media_id: string
  trace_id: string
  field: string
  origin: Generated<MediaAssociationOrigin>
}

export interface ObservationMedia {
  id: string
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  media_id: string
  trace_id: string
  observation_id: string
  field: string
  origin: Generated<MediaAssociationOrigin>
}

export interface DatasetItemMedia {
  id: string
  project_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  media_id: string
  dataset_id: string
  dataset_item_id: string
  dataset_item_valid_from: Timestamp | null
  field: string
  json_path: string | null
  reference_string: string | null
}

export interface BillingMeterBackup {
  stripe_customer_id: string
  meter_id: string
  start_time: Timestamp
  end_time: Timestamp
  aggregated_value: number
  event_name: string
  org_id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface LlmSchema {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: string
  description: string
  schema: unknown
}

export interface LlmTool {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: string
  description: string
  parameters: unknown
}

export interface Dashboard {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  created_by: string | null
  updated_by: string | null
  project_id: string | null
  name: string
  description: string
  definition: unknown
  filters: Generated<unknown>
}

export interface DashboardWidget {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  created_by: string | null
  updated_by: string | null
  project_id: string | null
  name: string
  description: string
  view: DashboardWidgetViews
  dimensions: unknown
  metrics: unknown
  filters: unknown
  chart_type: DashboardWidgetChartType
  chart_config: unknown
  min_version: Generated<number>
}

export interface TableViewPreset {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  name: string
  table_name: string
  created_by: string | null
  updated_by: string | null
  filters: unknown
  column_order: unknown
  column_visibility: unknown
  search_query: string | null
  order_by: unknown
}

export interface DefaultView {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  user_id: string | null
  view_name: string
  view_id: string
}

export interface Action {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  type: ActionType
  config: unknown
}

export interface Trigger {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  project_id: string
  eventSource: string
  eventActions: string[]
  filter: unknown
  status: Generated<JobConfigState>
}

export interface Automation {
  id: string
  name: string
  trigger_id: string
  action_id: string
  created_at: Generated<Timestamp>
  project_id: string
}

export interface AutomationExecution {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  source_id: string
  automation_id: string
  trigger_id: string
  action_id: string
  project_id: string
  status: Generated<ActionExecutionStatus>
  input: unknown
  output: unknown
  started_at: Timestamp | null
  finished_at: Timestamp | null
  error: string | null
}

export interface Monitor {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  created_by: string | null
  updated_by: string | null
  project_id: string
  view: MonitorView
  filters: unknown
  metric: unknown
  window_ms: bigint
  cadence_ms: bigint
  threshold_operator: MonitorThresholdOperator
  alert_threshold: string
  warning_threshold: string | null
  severity: Generated<MonitorSeverity>
  severity_changed_at: Timestamp | null
  no_data: unknown
  renotify: unknown
  status: Generated<MonitorStatus>
  scheduler_batch_id: bigint
  next_run_at: Timestamp | null
  last_published_at: Timestamp | null
  last_claimed_at: Timestamp | null
  last_completed_at: Timestamp | null
  name: string
  tags: Generated<string[]>
  trigger_ids: Generated<string[]>
  alerted_at: Timestamp | null
}

export interface SlackIntegration {
  id: string
  project_id: string
  team_id: string
  team_name: string
  bot_token: string
  bot_user_id: string
  installer_user_id: string | null
  installer_slack_user_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface PendingDeletion {
  id: string
  project_id: string
  object: string
  object_id: string
  is_deleted: Generated<boolean>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface Survey {
  id: string
  created_at: Generated<Timestamp>
  survey_name: SurveyName
  response: unknown
  user_id: string | null
  user_email: string | null
  org_id: string | null
}

export interface CloudSpendAlert {
  id: string
  org_id: string
  title: string
  threshold: string
  triggered_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface TopicClusteringRun {
  id: string
  project_id: string
  facet_id: string
  facet_version: number
  status: Generated<string>
  config: Generated<unknown>
  error: string | null
  created_at: Generated<Timestamp>
  started_at: Timestamp | null
  finished_at: Timestamp | null
  topic_version_ids: Generated<string[]>
  rule_id: string | null
  batch_action_id: string | null
}

export interface DB {
  Account: Account
  Session: Session
  users: User
  verification_tokens: VerificationToken
  organizations: Organization
  projects: Project
  api_keys: ApiKey
  role_assignments: RoleAssignment
  gateway_configs: GatewayConfig
  gateway_ai_connections: GatewayAiConnection
  gateway_api_key_associations: GatewayApiKeyAssociation
  in_app_agent_conversations: InAppAgentConversation
  in_app_agent_events: InAppAgentEvent
  in_app_agent_runs: InAppAgentRun
  in_app_agent_pending_tool_approvals: InAppAgentPendingToolApproval
  background_migrations: BackgroundMigration
  llm_api_keys: LlmApiKeys
  organization_memberships: OrganizationMembership
  project_memberships: ProjectMembership
  membership_invitations: MembershipInvitation
  trace_sessions: TraceSession
  score_configs: ScoreConfig
  annotation_queues: AnnotationQueue
  annotation_queue_items: AnnotationQueueItem
  annotation_queue_assignments: AnnotationQueueAssignment
  cron_jobs: CronJobs
  datasets: Dataset
  dataset_items: DatasetItem
  dataset_runs: DatasetRuns
  comments: Comment
  comment_reactions: CommentReaction
  notification_preferences: NotificationPreference
  prompts: Prompt
  prompt_dependencies: PromptDependency
  prompt_protected_labels: PromptProtectedLabels
  skills: Skill
  skill_files: SkillFile
  skill_blobs: SkillBlob
  models: Model
  prices: Price
  pricing_tiers: PricingTier
  audit_logs: AuditLog
  audit_log_staging: AuditLogStaging
  eval_templates: EvalTemplate
  evaluators: Evaluator
  evaluator_versions: EvaluatorVersion
  job_configurations: JobConfiguration
  evaluation_rules: EvaluationRule
  tracing_readiness_settings: TracingReadinessSettings
  evaluation_rule_evaluator_assignments: EvaluationRuleEvaluatorAssignment
  job_executions: JobExecution
  default_llm_models: DefaultLlmModel
  topic_configs: TopicConfig
  sso_configs: SsoConfig
  verified_domains: VerifiedDomain
  posthog_integrations: PosthogIntegration
  mixpanel_integrations: MixpanelIntegration
  blob_storage_integrations: BlobStorageIntegration
  external_media_storage_integrations: ExternalMediaStorageIntegration
  web_callout_endpoints: WebCalloutEndpoint
  batch_exports: BatchExport
  batch_actions: BatchAction
  media: Media
  trace_media: TraceMedia
  observation_media: ObservationMedia
  dataset_item_media: DatasetItemMedia
  billing_meter_backups: BillingMeterBackup
  llm_schemas: LlmSchema
  llm_tools: LlmTool
  dashboards: Dashboard
  dashboard_widgets: DashboardWidget
  table_view_presets: TableViewPreset
  default_views: DefaultView
  actions: Action
  triggers: Trigger
  automations: Automation
  automation_executions: AutomationExecution
  monitors: Monitor
  slack_integrations: SlackIntegration
  pending_deletions: PendingDeletion
  surveys: Survey
  cloud_spend_alerts: CloudSpendAlert
  topic_clustering_runs: TopicClusteringRun
}
