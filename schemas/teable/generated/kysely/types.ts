import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ProvisionState = 'pending' | 'ready' | 'error' | 'deleting'

export type DataDbProvider = 'postgres'

export type DataDbConnectionStatus =
  | 'pending'
  | 'validating'
  | 'ready'
  | 'error'
  | 'migrating'
  | 'disabled'

export type DataDbConnectionHealthState = 'healthy' | 'read_only' | 'unreachable' | 'degraded'

export type SpaceDataDbBindingMode = 'default' | 'byodb'

export type SpaceDataDbBindingState =
  | 'ready'
  | 'validating'
  | 'initializing'
  | 'migrating'
  | 'error'
  | 'disabled'

export type SpaceDataDbMigrationJobState =
  | 'pending'
  | 'waiting_worker'
  | 'preflight'
  | 'freezing_writes'
  | 'copying'
  | 'validating'
  | 'switching'
  | 'succeeded'
  | 'failed'
  | 'canceled'
  | 'rolled_back'

export type BaseDataDbMoveJobState =
  | 'pending'
  | 'waiting_worker'
  | 'copying_base_schema'
  | 'copying_shared_rows'
  | 'validating'
  | 'switching'
  | 'succeeded'
  | 'failed'
  | 'cancelled'

export interface Space {
  id: string
  name: string
  avatar: string | null
  credit: number | null
  deleted_time: Timestamp | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_by: string | null
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  is_template: boolean | null
}

export interface DataDbConnection {
  id: string
  provider: Generated<DataDbProvider>
  encrypted_url: string
  url_fingerprint: string
  display_host: string | null
  display_database: string | null
  internal_schema: string
  status: Generated<DataDbConnectionStatus>
  schema_version: string | null
  capabilities: unknown
  last_validated_at: Timestamp | null
  last_error: string | null
  health_state: Generated<DataDbConnectionHealthState>
  health_reason: string | null
  health_changed_at: Timestamp | null
  last_health_check_at: Timestamp | null
  consecutive_health_failures: Generated<number>
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface SpaceDataDbBinding {
  id: string
  space_id: string
  data_db_connection_id: string | null
  mode: Generated<SpaceDataDbBindingMode>
  state: Generated<SpaceDataDbBindingState>
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_history_flushed_at: Timestamp | null
  last_removal_flushed_at: Timestamp | null
}

export interface SpaceDataDbMigrationJob {
  id: string
  space_id: string
  source_connection_id: string | null
  target_connection_id: string | null
  target_mode: Generated<string>
  switch_on_completion: Generated<boolean>
  state: Generated<SpaceDataDbMigrationJobState>
  target_url_fingerprint: string
  target_internal_schema: string
  inventory: unknown
  copy_stats: unknown
  validation_stats: unknown
  last_error: string | null
  started_at: Timestamp | null
  completed_at: Timestamp | null
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface BaseDataDbMoveJob {
  id: string
  base_id: string
  source_space_id: string
  target_space_id: string
  source_connection_id: string | null
  target_connection_id: string | null
  state: Generated<BaseDataDbMoveJobState>
  inventory: unknown
  copy_stats: unknown
  validation_stats: unknown
  last_error: string | null
  started_at: Timestamp | null
  completed_at: Timestamp | null
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface PinResource {
  id: string
  type: string
  resource_id: string
  created_time: Generated<Timestamp>
  created_by: string
  order: number
}

export interface Base {
  id: string
  space_id: string
  name: string | null
  order: number
  icon: string | null
  schema_pass: string | null
  v2_enabled: Generated<boolean>
  provision_state: Generated<ProvisionState>
  deleted_time: Timestamp | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_by: string | null
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface TableMeta {
  id: string
  base_id: string
  name: string
  description: string | null
  icon: string | null
  db_table_name: string
  db_view_name: string | null
  search_index: unknown
  search_index_policy: unknown
  query_index_policy: unknown
  provision_state: Generated<ProvisionState>
  version: number
  order: number
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_time: Timestamp | null
  created_by: string
  last_modified_by: string | null
}

export interface TableQueryObservationShard {
  space_id: string | null
  base_id: string
  table_id: string
  query_kind: string
  shape_hash: string
  window_start: Timestamp
  writer_id: string
  window_size_seconds: number
  request_count: number
  slow_count: number
  timeout_count: number
  db_error_count: number
  total_duration_ms: number
  max_duration_ms: number
  total_db_duration_ms: number | null
  max_db_duration_ms: number | null
  shape: unknown
  sql_diagnostics: unknown
  created_time: Generated<Timestamp>
  last_modified_time: Timestamp | null
}

export interface SchemaOperation {
  id: string
  type: string
  status: string
  phase: string
  resource_type: string
  resource_id: string
  base_id: string | null
  table_id: string | null
  idempotency_key: string
  payload: unknown
  result: unknown
  attempts: Generated<number>
  max_attempts: Generated<number>
  next_run_at: Generated<Timestamp>
  locked_at: Timestamp | null
  locked_by: string | null
  last_error: string | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface Field {
  id: string
  name: string
  description: string | null
  options: string | null
  meta: string | null
  ai_config: string | null
  type: string
  cell_value_type: string
  is_multiple_cell_value: boolean | null
  db_field_type: string
  db_field_name: string
  provision_state: Generated<ProvisionState>
  not_null: boolean | null
  unique: boolean | null
  is_primary: boolean | null
  is_computed: boolean | null
  is_lookup: boolean | null
  is_conditional_lookup: boolean | null
  is_pending: boolean | null
  has_error: boolean | null
  lookup_linked_field_id: string | null
  lookup_options: string | null
  table_id: string
  order: number
  version: number
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_time: Timestamp | null
  created_by: string
  last_modified_by: string | null
}

export interface ComputedUpdateOutbox {
  id: string
  base_id: string
  worker_lane: Generated<string>
  seed_table_id: string
  seed_record_ids: unknown
  change_type: string
  steps: unknown
  edges: unknown
  status: string
  attempts: Generated<number>
  max_attempts: Generated<number>
  next_run_at: Generated<Timestamp>
  locked_at: Timestamp | null
  locked_by: string | null
  last_error: string | null
  estimated_complexity: Generated<number>
  plan_hash: string
  dirty_stats: unknown
  run_id: string
  origin_run_ids: Generated<string[]>
  run_total_steps: Generated<number>
  run_completed_steps_before: Generated<number>
  affected_table_ids: Generated<string[]>
  affected_field_ids: Generated<string[]>
  sync_max_level: number | null
  source_changed_at: Timestamp | null
  stage_depth: Generated<number>
  predecessor_task_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface ComputedUpdateOutboxSeed {
  id: string
  task_id: string
  table_id: string
  record_id: string
}

export interface ComputedUpdateChangeFrontier {
  scope_id: string
  kind: string
  table_id: string
  record_id: string
  field_id: string
}

export interface ComputedUpdateStageLedger {
  scope_id: string
  kind: string
  table_id: string
  record_id: string
  seq: Generated<bigint>
}

export interface ComputedUpdateDeadLetter {
  id: string
  base_id: string
  worker_lane: Generated<string>
  seed_table_id: string
  seed_record_ids: unknown
  change_type: string
  steps: unknown
  edges: unknown
  status: string
  attempts: Generated<number>
  max_attempts: Generated<number>
  next_run_at: Timestamp
  locked_at: Timestamp | null
  locked_by: string | null
  last_error: string | null
  estimated_complexity: Generated<number>
  plan_hash: string
  dirty_stats: unknown
  run_id: string
  origin_run_ids: Generated<string[]>
  run_total_steps: Generated<number>
  run_completed_steps_before: Generated<number>
  affected_table_ids: Generated<string[]>
  affected_field_ids: Generated<string[]>
  sync_max_level: number | null
  source_changed_at: Timestamp | null
  stage_depth: Generated<number>
  predecessor_task_id: string | null
  trace_data: unknown
  failed_at: Timestamp
  created_at: Timestamp
  updated_at: Timestamp
}

export interface ComputedUpdateDeadLetterSeed {
  task_id: string
  table_id: string
  record_id: string
}

export interface ComputedUpdateRunHistory {
  task_id: string
  base_id: string
  seed_table_id: string
  change_type: string
  run_id: string
  origin_run_ids: Generated<string[]>
  steps: unknown
  edges: unknown
  affected_table_ids: Generated<string[]>
  affected_field_ids: Generated<string[]>
  source_field_ids: Generated<string[]>
  seed_record_count: Generated<number>
  stage_depth: Generated<number>
  predecessor_task_id: string | null
  run_total_steps: Generated<number>
  run_completed_steps_before: Generated<number>
  sync_max_level: number | null
  estimated_complexity: Generated<number>
  attempts: Generated<number>
  outcome: string
  source_changed_at: Timestamp | null
  enqueued_at: Timestamp
  started_at: Timestamp | null
  completed_at: Timestamp
  duration_ms: Generated<number>
}

export interface ComputedUpdatePauseScope {
  id: string
  scope_type: string
  scope_id: string
  paused_at: Generated<Timestamp>
  paused_by: string | null
  resume_at: Timestamp | null
  reason: string | null
  write_policy: Generated<string>
  updated_at: Generated<Timestamp>
  updated_by: string | null
}

export interface ComputedFieldActivity {
  field_id: string
  table_id: string
  base_id: string
  status: string
  active_task_count: Generated<number>
  processing_task_count: Generated<number>
  generation: Generated<bigint>
  estimated_complexity: Generated<bigint>
  estimated_dirty_records: Generated<bigint>
  has_all_target_records: Generated<boolean>
  queued_at: Timestamp | null
  started_at: Timestamp | null
  last_completed_at: Timestamp | null
  last_duration_ms: number | null
  last_error: unknown
  extensions: unknown
  updated_at: Generated<Timestamp>
}

export interface ComputedTableActivity {
  table_id: string
  base_id: string
  status: string
  calculating_field_count: Generated<number>
  queued_field_count: Generated<number>
  estimated_complexity: Generated<bigint>
  recent_completions: Generated<unknown>
  generation: Generated<bigint>
  updated_at: Generated<Timestamp>
}

export interface ComputedTaskFieldRef {
  task_id: string
  field_id: string
  table_id: string
  base_id: string
  was_processing: Generated<boolean>
  created_at: Generated<Timestamp>
}

export interface View {
  id: string
  name: string
  description: string | null
  table_id: string
  type: string
  sort: string | null
  filter: string | null
  group: string | null
  options: string | null
  order: number
  version: number
  column_meta: string
  is_locked: boolean | null
  enable_share: boolean | null
  share_id: string | null
  share_meta: string | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_time: Timestamp | null
  created_by: string
  last_modified_by: string | null
}

export interface Ops {
  id: string
  collection: string
  doc_id: string
  doc_type: string
  version: number
  operation: string
  created_time: Generated<Timestamp>
  created_by: string
}

export interface Reference {
  id: string
  from_field_id: string
  to_field_id: string
  created_time: Generated<Timestamp>
}

export interface User {
  id: string
  name: string
  password: string | null
  salt: string | null
  phone: string | null
  email: string
  avatar: string | null
  is_system: boolean | null
  is_admin: boolean | null
  is_trial_used: boolean | null
  lang: string | null
  notify_meta: string | null
  last_sign_time: Timestamp | null
  deactivated_time: Timestamp | null
  created_time: Generated<Timestamp>
  deleted_time: Timestamp | null
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  permanent_deleted_time: Timestamp | null
  ref_meta: string | null
}

export interface Account {
  id: string
  user_id: string
  type: string
  provider: string
  provider_id: string
  connection_id: Generated<string>
  created_time: Generated<Timestamp>
}

export interface Attachments {
  id: string
  token: string
  hash: string
  size: bigint
  mimetype: string
  path: string
  width: number | null
  height: number | null
  deleted_time: Timestamp | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_by: string | null
  thumbnail_path: string | null
}

export interface AttachmentsTable {
  id: string
  attachment_id: string
  name: string
  token: string
  table_id: string
  record_id: string
  field_id: string
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_by: string | null
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Collaborator {
  id: string
  role_name: string
  resource_type: string
  resource_id: string
  principal_id: string
  principal_type: string
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface Invitation {
  id: string
  base_id: string | null
  space_id: string | null
  type: string
  role: string
  invitation_code: string
  expired_time: Timestamp | null
  create_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
  deleted_time: Timestamp | null
}

export interface InvitationRecord {
  id: string
  invitation_id: string
  base_id: string | null
  space_id: string | null
  type: string
  inviter: string
  accepter: string
  created_time: Generated<Timestamp>
}

export interface Notification {
  id: string
  from_user_id: string
  to_user_id: string
  type: string
  message: string
  message_i18n: string | null
  severity: Generated<string>
  url_path: string | null
  is_read: Generated<boolean>
  created_time: Generated<Timestamp>
  created_by: string
}

export interface AccessToken {
  id: string
  name: string
  description: string | null
  user_id: string
  scopes: string
  space_ids: string | null
  base_ids: string | null
  sign: string
  client_id: string | null
  has_full_access: boolean | null
  expired_time: Timestamp
  last_used_time: Timestamp | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Setting {
  name: string
  content: string | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  created_by: string
  last_modified_by: string | null
}

export interface OAuthApp {
  id: string
  name: string
  logo: string | null
  homepage: string
  description: string | null
  client_id: string
  redirect_uris: string | null
  scopes: string | null
  allow_device_flow: Generated<boolean>
  dynamic_registration: Generated<boolean>
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  created_by: string
}

export interface OAuthAppAuthorized {
  id: string
  client_id: string
  user_id: string
  authorized_time: Timestamp
}

export interface OAuthAppSecret {
  id: string
  client_id: string
  secret: string
  masked_secret: string
  created_time: Generated<Timestamp>
  created_by: string
  last_used_time: Timestamp | null
}

export interface OAuthAppToken {
  id: string
  client_id: string
  app_secret_id: string | null
  refresh_token_sign: string
  expired_time: Timestamp
  created_time: Generated<Timestamp>
  created_by: string
}

export interface RecordHistory {
  id: string
  table_id: string
  record_id: string
  field_id: string
  before: string
  after: string
  created_time: Generated<Timestamp>
  created_by: string
}

export interface Trash {
  id: string
  resource_type: string
  resource_id: string
  parent_id: string | null
  deleted_time: Generated<Timestamp>
  deleted_by: string
}

export interface TableTrash {
  id: string
  table_id: string
  resource_type: string
  snapshot: string
  created_time: Generated<Timestamp>
  created_by: string
}

export interface RecordTrash {
  id: string
  table_id: string
  record_id: string
  snapshot: string
  created_time: Generated<Timestamp>
  created_by: string
  reason: Generated<string>
  record_created_time: Timestamp | null
  record_created_by: string | null
  record_last_modified_time: Timestamp | null
  record_last_modified_by: string | null
  operation_id: string | null
}

export interface RecordRemovalTombstone {
  id: string
  table_id: string
  record_id: string
  type: string
  created_time: Generated<Timestamp>
}

export interface Plugin {
  id: string
  name: string
  description: string | null
  detail_desc: string | null
  logo: string
  help_url: string | null
  status: string
  positions: string
  url: string | null
  secret: string
  masked_secret: string
  i18n: string | null
  config: string | null
  plugin_user: string | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  created_by: string
  last_modified_by: string | null
}

export interface PluginInstall {
  id: string
  plugin_id: string
  base_id: string
  name: string
  position_id: string
  position: string
  storage: string | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface Dashboard {
  id: string
  name: string
  base_id: string
  layout: string | null
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface Comment {
  id: string
  table_id: string
  record_id: string
  quote_Id: string | null
  content: string | null
  reaction: string | null
  deleted_time: Timestamp | null
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface CommentSubscription {
  id: string
  table_id: string
  record_id: string
  created_by: string
  created_time: Generated<Timestamp>
}

export interface Integration {
  id: string
  resource_id: string
  config: string
  type: string
  enable: boolean | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface PluginPanel {
  id: string
  name: string
  table_id: string
  layout: string | null
  created_by: string
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface PluginContextMenu {
  id: string
  table_id: string
  plugin_install_id: string
  order: number
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface UserLastVisit {
  id: string
  user_id: string
  resource_type: string
  resource_id: string
  parent_resource_id: string
  last_visit_time: Generated<Timestamp>
}

export interface UserBaseOrder {
  id: string
  user_id: string
  base_id: string
  order: number
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Template {
  id: string
  base_id: string | null
  cover: string | null
  name: string | null
  description: string | null
  markdown_description: string | null
  category_id: string[]
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
  is_system: boolean | null
  is_published: boolean | null
  featured: boolean | null
  snapshot: string | null
  order: number
  usage_count: Generated<number>
  publish_info: unknown
  visit_count: Generated<number>
  kind: string | null
  version: Generated<number>
  manifest: unknown
  review: unknown
  history: unknown
  parent_template_id: string | null
  slug: string | null
}

export interface TemplateCategory {
  id: string
  name: string
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
  order: number
  parent_id: string | null
  kind: Generated<string>
  key: string | null
}

export interface Task {
  id: string
  type: string
  status: string
  snapshot: string | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  created_by: string
  last_modified_by: string | null
}

export interface TaskRun {
  id: string
  task_id: string
  base_id: string
  status: string
  snapshot: string
  depends_on_run_ids: Generated<string[]>
  spent: number | null
  log: string | null
  error_msg: string | null
  started_time: Timestamp | null
  created_time: Generated<Timestamp>
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface TaskReference {
  id: string
  from_field_id: string
  to_field_id: string
  created_time: Generated<Timestamp>
}

export interface Waitlist {
  email: string
  invite: boolean | null
  invite_time: Timestamp | null
  created_time: Generated<Timestamp>
}

export interface BaseNode {
  id: string
  parent_id: string | null
  base_id: string
  resource_type: string
  resource_id: string
  order: number
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface BaseNodeFolder {
  id: string
  base_id: string
  name: string
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
  last_modified_by: string | null
}

export interface BaseShare {
  id: string
  base_id: string
  share_id: string
  password: string | null
  node_id: string | null
  allow_save: boolean | null
  allow_copy: boolean | null
  allow_edit: boolean | null
  enabled: Generated<boolean>
  created_time: Generated<Timestamp>
  created_by: string
  last_modified_time: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface ShortLink {
  id: string
  code: string
  type: string
  resource_id: string
  created_time: Generated<Timestamp>
  created_by: string
  deleted_time: Timestamp | null
}

export interface DB {
  space: Space
  data_db_connection: DataDbConnection
  space_data_db_binding: SpaceDataDbBinding
  space_data_db_migration_job: SpaceDataDbMigrationJob
  base_data_db_move_job: BaseDataDbMoveJob
  pin_resource: PinResource
  base: Base
  table_meta: TableMeta
  table_query_observation_shard: TableQueryObservationShard
  schema_operation: SchemaOperation
  field: Field
  computed_update_outbox: ComputedUpdateOutbox
  computed_update_outbox_seed: ComputedUpdateOutboxSeed
  computed_update_change_frontier: ComputedUpdateChangeFrontier
  computed_update_stage_ledger: ComputedUpdateStageLedger
  computed_update_dead_letter: ComputedUpdateDeadLetter
  computed_update_dead_letter_seed: ComputedUpdateDeadLetterSeed
  computed_update_run_history: ComputedUpdateRunHistory
  computed_update_pause_scope: ComputedUpdatePauseScope
  computed_field_activity: ComputedFieldActivity
  computed_table_activity: ComputedTableActivity
  computed_task_field_ref: ComputedTaskFieldRef
  view: View
  ops: Ops
  reference: Reference
  users: User
  account: Account
  attachments: Attachments
  attachments_table: AttachmentsTable
  collaborator: Collaborator
  invitation: Invitation
  invitation_record: InvitationRecord
  notification: Notification
  access_token: AccessToken
  setting: Setting
  oauth_app: OAuthApp
  oauth_app_authorized: OAuthAppAuthorized
  oauth_app_secret: OAuthAppSecret
  oauth_app_token: OAuthAppToken
  record_history: RecordHistory
  trash: Trash
  table_trash: TableTrash
  record_trash: RecordTrash
  record_removal_tombstone: RecordRemovalTombstone
  plugin: Plugin
  plugin_install: PluginInstall
  dashboard: Dashboard
  comment: Comment
  comment_subscription: CommentSubscription
  integration: Integration
  plugin_panel: PluginPanel
  plugin_context_menu: PluginContextMenu
  user_last_visit: UserLastVisit
  user_base_order: UserBaseOrder
  template: Template
  template_category: TemplateCategory
  task: Task
  task_run: TaskRun
  task_reference: TaskReference
  waitlist: Waitlist
  base_node: BaseNode
  base_node_folder: BaseNodeFolder
  base_share: BaseShare
  short_link: ShortLink
}
