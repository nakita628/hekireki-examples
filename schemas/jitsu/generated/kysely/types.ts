import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type WorkspaceRole = 'owner' | 'editor' | 'analyst'

export type reverse_sync_mode = 'upsert' | 'mirror'

export type reverse_sync_extraction = 'cursor' | 'full'

export interface EmailLog {
  id: string
  time: Generated<Timestamp>
  email: unknown
  error: string | null
  previewUrl: string | null
  messageId: string | null
  status: string
}

export interface UserPreferences {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: string
  workspaceId: string | null
  scope: string | null
  preferences: unknown
}

export interface UserProfile {
  id: string
  name: string
  email: string
  admin: Generated<boolean | null>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  loginProvider: string
  externalUsername: string | null
  externalId: string
}

export interface UserPassword {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: string
  hash: string
  changeAtNextLogin: Generated<boolean>
}

export interface Workspace {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  deleted: Generated<boolean>
  slug: string | null
  featuresEnabled: Generated<string[]>
}

export interface WorkspaceOptions {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  workspaceId: string
  namespace: string
  value: unknown
}

export interface WorkspaceAccess {
  workspaceId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: string
  role: Generated<WorkspaceRole>
}

export interface WorkspaceUserProperties {
  workspaceId: string
  userId: string
  lastUsed: Generated<Timestamp>
}

export interface UserApiToken {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastUsed: Timestamp | null
  hint: string
  hash: string
  userId: string
  type: string | null
  name: string | null
  expiresAt: Timestamp | null
  oauthClientId: string | null
}

export interface OAuthClient {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  clientSecretHash: string
  name: string
  redirectUris: string[]
}

export interface KvStoreEntry {
  key: string
  value: unknown
  expires_at: Timestamp | null
}

export interface OAuthAccessToken {
  id: string
  createdAt: Generated<Timestamp>
  lastUsed: Timestamp | null
  hash: string
  expiresAt: Timestamp
  refreshTokenId: string
}

export interface InvitationToken {
  id: string
  workspaceId: string
  email: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  token: string
  usedBy: string | null
  role: Generated<WorkspaceRole>
}

export interface ConfigurationObject {
  id: string
  deleted: Generated<boolean | null>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  type: string
  workspaceId: string
  config: unknown
}

export interface ConfigurationObjectLink {
  id: string
  deleted: Generated<boolean | null>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  type: Generated<string | null>
  data: unknown
  workspaceId: string
  fromId: string
  toId: string
}

export interface ConnectorPackage {
  id: string
  packageId: string
  packageType: Generated<string>
  meta: unknown
  logoSvg: Buffer | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface AuditLog {
  id: string
  timestamp: Generated<Timestamp>
  type: string
  severity: Generated<string | null>
  userId: string | null
  workspaceId: string | null
  objectId: string | null
  changes: unknown
  authType: string | null
  tokenId: string | null
  ip: string | null
  requestHeaders: unknown
}

export interface GlobalProps {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  value: unknown
}

export interface source_spec {
  package: string
  version: string
  specs: unknown
  timestamp: Generated<Timestamp>
  error: string | null
}

export interface source_check {
  package: string
  version: string
  key: string
  status: string
  description: string | null
  timestamp: Generated<Timestamp>
}

export interface source_catalog {
  package: string
  version: string
  key: string
  catalog: unknown
  timestamp: Generated<Timestamp>
  status: string
  description: string | null
}

export interface reverse_sync_control {
  workspace_id: string
  sync_id: string
  run_id: string
  run_order: Generated<bigint>
  detached: Generated<boolean>
  revision: string
  target_hash: string
  phase: Generated<string>
  mode: reverse_sync_mode
  extraction: reverse_sync_extraction
  artifact_head: Buffer | null
  base_sequence: Generated<bigint>
  next_sequence: Generated<bigint>
  checkpoint_sequence: Generated<bigint>
  finish_sequence: bigint | null
  store: Buffer | null
  provider_state: Buffer | null
  finish_result: Buffer | null
  committed_generation: string | null
}

export interface reverse_sync_target_owner {
  target_hash: string
  workspace_id: string
  sync_id: string
}

export interface source_state {
  sync_id: string
  stream: string
  state: unknown
  timestamp: Generated<Timestamp>
}

export interface source_task {
  sync_id: string
  task_id: string
  package: string
  version: string
  started_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  started_by: unknown
  status: string
  description: string | null
  metrics: unknown
  error: string | null
}

export interface task_log {
  id: string
  level: string
  logger: string
  message: string
  sync_id: string
  task_id: string
  timestamp: Generated<Timestamp>
}

export interface ProfileBuilder {
  id: string
  name: string
  deleted: Generated<boolean | null>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  workspaceId: string
  intermediateStorageCredentials: unknown
  destinationId: string | null
  connectionOptions: unknown
  version: Generated<number>
}

export interface ProfileBuilderFunction {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  profileBuilderId: string
  functionId: string
}

export interface ProfileBuilderState {
  profileBuilderId: string
  profileBuilderVersion: number
  instanceIndex: number
  totalInstances: number
  startedAt: Timestamp
  updatedAt: Timestamp
  lastTimestamp: Timestamp | null
  processedUsers: number
  totalUsers: number
  errorUsers: number
  speed: number
}

export interface ProfileBuilderState2 {
  profileBuilderId: string
  updatedAt: Timestamp
  fullRebuildInfo: unknown
  queuesInfo: unknown
  metrics: unknown
}

export interface StatusChange {
  id: Generated<bigint>
  workspaceId: string
  actorId: string
  type: string
  tableName: string
  timestamp: Generated<Timestamp>
  status: string
  startedAt: Timestamp
  counts: Generated<number>
  description: string | null
  queueSize: Generated<number>
}

export interface NotificationState {
  workspaceId: string
  actorId: string
  type: string
  tableName: string
  channelId: string
  flappingSince: Timestamp | null
  lastNotification: Timestamp
  statusChangeId: bigint
  error: string | null
}

export interface Notification {
  id: string
  workspaceId: string
  actorId: string
  type: string
  tableName: string
  channelId: string
  statusChangeId: bigint
  timestamp: Generated<Timestamp>
  status: string
  error: string | null
}

export interface OidcProvider {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  issuer: string
  clientId: string
  clientSecret: string
  authorizationEndpoint: string | null
  tokenEndpoint: string | null
  userinfoEndpoint: string | null
  jwksUri: string | null
  introspectionEndpoint: string | null
  scopes: Generated<string[]>
  audience: string | null
  prompt: string | null
  usernameClaim: Generated<string>
  emailClaim: Generated<string>
  nameClaim: Generated<string>
  groupClaim: Generated<string>
  enabled: Generated<boolean>
  autoDiscovery: Generated<boolean>
}

export interface OidcLoginGroup {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  workspaceId: string
  oidcProviderId: string
  groupValue: string | null
  allowAllUsers: Generated<boolean>
}

export interface reprocessing_jobs {
  id: string
  config: unknown
  status: string
  created_at: Timestamp
  started_at: Timestamp | null
  completed_at: Timestamp | null
  k8s_job_name: string | null
  total_files: Generated<number | null>
  total_workers: Generated<number | null>
  error: string | null
}

export interface reprocessing_workers {
  job_id: string
  worker_index: number
  status: string
  started_at: Timestamp | null
  updated_at: Generated<Timestamp>
  completed_at: Timestamp | null
  current_file: string | null
  current_line: Generated<bigint | null>
  current_ts: string | null
  assigned_files: Generated<number | null>
  processed_files: Generated<number | null>
  total_lines: Generated<bigint | null>
  success_count: Generated<bigint | null>
  error_count: Generated<bigint | null>
  skipped_count: Generated<bigint | null>
  total_bytes: Generated<bigint | null>
  processed_bytes: Generated<bigint | null>
  error: string | null
}

export interface FunctionsServer {
  workspaceId: string
  class: string
  deploymentId: string
  connections: string[]
  emptyConnections: string[]
  profileBuilders: string[]
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp | null
}

export interface RateLimitCounter {
  bucketKey: string
  windowStart: Timestamp
  count: Generated<number>
  expiresAt: Timestamp
}

export interface DB {
  EmailLog: EmailLog
  UserPreferences: UserPreferences
  UserProfile: UserProfile
  UserPassword: UserPassword
  Workspace: Workspace
  WorkspaceOptions: WorkspaceOptions
  WorkspaceAccess: WorkspaceAccess
  WorkspaceUserProperties: WorkspaceUserProperties
  UserApiToken: UserApiToken
  OAuthClient: OAuthClient
  kv: KvStoreEntry
  OAuthAccessToken: OAuthAccessToken
  InvitationToken: InvitationToken
  ConfigurationObject: ConfigurationObject
  ConfigurationObjectLink: ConfigurationObjectLink
  ConnectorPackage: ConnectorPackage
  AuditLog: AuditLog
  GlobalProps: GlobalProps
  source_spec: source_spec
  source_check: source_check
  source_catalog: source_catalog
  reverse_sync_control: reverse_sync_control
  reverse_sync_target_owner: reverse_sync_target_owner
  source_state: source_state
  source_task: source_task
  task_log: task_log
  ProfileBuilder: ProfileBuilder
  ProfileBuilderFunction: ProfileBuilderFunction
  ProfileBuilderState: ProfileBuilderState
  ProfileBuilderState2: ProfileBuilderState2
  StatusChange: StatusChange
  NotificationState: NotificationState
  Notification: Notification
  OidcProvider: OidcProvider
  OidcLoginGroup: OidcLoginGroup
  reprocessing_jobs: reprocessing_jobs
  reprocessing_workers: reprocessing_workers
  FunctionsServer: FunctionsServer
  RateLimitCounter: RateLimitCounter
}
