import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type AuthenticationMethod = 'GITHUB' | 'MAGIC_LINK' | 'GOOGLE' | 'SSO'

export type OrganizationAccessTokenType = 'USER' | 'SYSTEM'

export type OrgMemberRole = 'ADMIN' | 'MEMBER'

export type ApiKeyIssuer = 'USER' | 'SYSTEM'

export type RuntimeEnvironmentType = 'PRODUCTION' | 'STAGING' | 'DEVELOPMENT' | 'PREVIEW'

export type EnvironmentPauseSource = 'BILLING_LIMIT' | 'ADMIN_BAN'

export type ProjectVersion = 'V2' | 'V3'

export type EnvironmentKeyWrappingProvider = 'PLATFORM_KEYRING' | 'AWS_KMS'

export type SecretStoreProvider = 'DATABASE' | 'AWS_PARAM_STORE'

export type OidcSigningKeyStatus = 'STAGED' | 'ACTIVE' | 'RETIRED'

export type OidcSigningKeyPurpose = 'EXTERNAL_OIDC' | 'INTERNAL_RUN'

export type TaskTriggerSource = 'STANDARD' | 'SCHEDULED' | 'AGENT' | 'WEBHOOK'

export type WebhookEndpointStatus = 'ACTIVE' | 'INACTIVE' | 'DELETING'

export type WebhookDeliveryStatus =
  | 'PENDING'
  | 'PROCESSING'
  | 'SUCCEEDED'
  | 'FAILED'
  | 'FILTERED'
  | 'UNMATCHED'

export type TaskRunStatus =
  | 'DELAYED'
  | 'PENDING'
  | 'PENDING_VERSION'
  | 'WAITING_FOR_DEPLOY'
  | 'DEQUEUED'
  | 'EXECUTING'
  | 'WAITING_TO_RESUME'
  | 'RETRYING_AFTER_FAILURE'
  | 'PAUSED'
  | 'CANCELED'
  | 'INTERRUPTED'
  | 'COMPLETED_SUCCESSFULLY'
  | 'COMPLETED_WITH_ERRORS'
  | 'SYSTEM_FAILURE'
  | 'CRASHED'
  | 'EXPIRED'
  | 'TIMED_OUT'

export type RunEngineVersion = 'V1' | 'V2'

export type TaskRunExecutionStatus =
  | 'RUN_CREATED'
  | 'DELAYED'
  | 'QUEUED'
  | 'QUEUED_EXECUTING'
  | 'PENDING_EXECUTING'
  | 'EXECUTING'
  | 'EXECUTING_WITH_WAITPOINTS'
  | 'SUSPENDED'
  | 'PENDING_CANCEL'
  | 'FINISHED'

export type TaskRunCheckpointType = 'DOCKER' | 'KUBERNETES' | 'COMPUTE'

export type WaitpointType = 'RUN' | 'DATETIME' | 'MANUAL' | 'BATCH'

export type WaitpointStatus = 'PENDING' | 'COMPLETED'

export type WorkerInstanceGroupType = 'MANAGED' | 'UNMANAGED'

export type WorkloadType = 'CONTAINER' | 'MICROVM'

export type TaskRunAttemptStatus =
  | 'PENDING'
  | 'EXECUTING'
  | 'PAUSED'
  | 'FAILED'
  | 'CANCELED'
  | 'COMPLETED'

export type TaskEventLevel = 'TRACE' | 'DEBUG' | 'LOG' | 'INFO' | 'WARN' | 'ERROR'

export type TaskEventKind =
  | 'UNSPECIFIED'
  | 'INTERNAL'
  | 'SERVER'
  | 'CLIENT'
  | 'PRODUCER'
  | 'CONSUMER'
  | 'UNRECOGNIZED'
  | 'LOG'

export type TaskEventStatus = 'UNSET' | 'OK' | 'ERROR' | 'UNRECOGNIZED'

export type TaskQueueType = 'VIRTUAL' | 'NAMED'

export type TaskQueueVersion = 'V1' | 'V2'

export type TaskQueueConcurrencyVersion = 'V1' | 'V2'

export type TaskQueueRole = 'QUEUE' | 'LIMIT'

export type BatchTaskRunStatus =
  | 'PENDING'
  | 'PROCESSING'
  | 'COMPLETED'
  | 'PARTIAL_FAILED'
  | 'ABORTED'

export type BatchTaskRunItemStatus = 'PENDING' | 'FAILED' | 'CANCELED' | 'COMPLETED'

export type CheckpointType = 'DOCKER' | 'KUBERNETES'

export type CheckpointRestoreEventType = 'CHECKPOINT' | 'RESTORE'

export type WorkerDeploymentType = 'MANAGED' | 'UNMANAGED' | 'V1'

export type WorkerDeploymentStatus =
  | 'PENDING'
  | 'INSTALLING'
  | 'BUILDING'
  | 'DEPLOYING'
  | 'DEPLOYED'
  | 'FAILED'
  | 'CANCELED'
  | 'TIMED_OUT'

export type ScheduleType = 'DECLARATIVE' | 'IMPERATIVE'

export type ScheduleGeneratorType = 'CRON'

export type ProjectAlertChannelType = 'EMAIL' | 'SLACK' | 'WEBHOOK'

export type ProjectAlertType =
  | 'TASK_RUN'
  | 'TASK_RUN_ATTEMPT'
  | 'DEPLOYMENT_FAILURE'
  | 'DEPLOYMENT_SUCCESS'
  | 'ERROR_GROUP'
  | 'DASHBOARD_AGENT_WATCH'

export type ProjectAlertStatus = 'PENDING' | 'SENT' | 'FAILED'

export type IntegrationService = 'SLACK' | 'VERCEL'

export type BulkActionType = 'CANCEL' | 'REPLAY'

export type BulkActionStatus = 'PENDING' | 'COMPLETED' | 'ABORTED'

export type BulkActionNotificationType = 'NONE' | 'EMAIL'

export type BulkActionItemStatus = 'PENDING' | 'COMPLETED' | 'FAILED'

export type GithubRepositorySelection = 'ALL' | 'SELECTED'

export type TrustedDeployScope = 'ANY' | 'BRANCH' | 'TAG' | 'ENVIRONMENT'

export type ImpersonationAuditLogAction = 'START' | 'STOP'

export type SupportAccessMode = 'ALLOW' | 'REQUIRES_REQUEST'

export type SupportAccessRequestStatus = 'PENDING' | 'APPROVED' | 'CANCELLED'

export type CustomerQuerySource = 'DASHBOARD' | 'API'

export type CustomerQueryScope = 'ORGANIZATION' | 'PROJECT' | 'ENVIRONMENT'

export type PlatformNotificationSurface = 'WEBAPP' | 'CLI'

export type PlatformNotificationScope = 'USER' | 'PROJECT' | 'ORGANIZATION' | 'GLOBAL'

export type ErrorGroupStatus = 'UNRESOLVED' | 'RESOLVED' | 'IGNORED'

export type DataStoreKind = 'CLICKHOUSE'

export interface User {
  id: string
  email: string
  pendingEmail: string | null
  authenticationMethod: AuthenticationMethod
  authenticationProfile: unknown
  authenticationExtraParams: unknown
  authIdentifier: string | null
  displayName: string | null
  name: string | null
  avatarUrl: string | null
  admin: Generated<boolean>
  dashboardPreferences: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  isOnCloudWaitlist: Generated<boolean>
  featureCloud: Generated<boolean>
  isOnHostedRepoWaitlist: Generated<boolean>
  marketingEmails: Generated<boolean>
  confirmedBasicDetails: Generated<boolean>
  referralSource: string | null
  onboardingData: unknown
  mfaEnabledAt: Timestamp | null
  mfaSecretReferenceId: string | null
  mfaLastUsedCode: string | null
  sessionDuration: Generated<number>
  nextSessionEnd: Timestamp | null
  invitationCodeId: string | null
}

export interface MfaBackupCode {
  id: string
  code: string
  userId: string
  usedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface InvitationCode {
  id: string
  code: string
  createdAt: Generated<Timestamp>
}

export interface AuthorizationCode {
  id: string
  code: string
  personalAccessTokenId: string | null
  requesterIp: string | null
  codeChallenge: string | null
  callbackPort: number | null
  approvalCodeHash: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PersonalAccessToken {
  id: string
  name: string
  encryptedToken: unknown
  obfuscatedToken: string
  hashedToken: string
  userId: string
  revokedAt: Timestamp | null
  lastAccessedAt: Timestamp | null
  expiresAt: Timestamp | null
  idleTimeoutSeconds: number | null
  expiryReminderSentAt: Timestamp | null
  expiredNoticeSentAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationAccessToken {
  id: string
  name: string
  type: Generated<OrganizationAccessTokenType>
  hashedToken: string
  organizationId: string
  expiresAt: Timestamp | null
  revokedAt: Timestamp | null
  lastAccessedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Organization {
  id: string
  slug: string
  title: string
  maximumExecutionTimePerRunInMs: Generated<number>
  maximumConcurrencyLimit: Generated<number>
  maximumSchedulesLimit: Generated<number>
  maximumDevQueueSize: number | null
  maximumDeployedQueueSize: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  companySize: string | null
  onboardingData: unknown
  avatar: unknown
  runsEnabled: Generated<boolean>
  v3Enabled: Generated<boolean>
  v2Enabled: Generated<boolean>
  v2MarqsEnabled: Generated<boolean>
  hasRequestedV3: Generated<boolean>
  apiRateLimiterConfig: unknown
  realtimeRateLimiterConfig: unknown
  batchRateLimitConfig: unknown
  batchQueueConcurrencyConfig: unknown
  webhookLimitsConfig: unknown
  featureFlags: unknown
  maximumProjectCount: Generated<number>
  maxSessionDuration: number | null
  streamBasinName: string | null
  supportAccessMode: Generated<SupportAccessMode>
  supportAccessModeUpdatedAt: Timestamp | null
  supportAccessModeUpdatedById: string | null
}

export interface OrgMember {
  id: string
  organizationId: string
  userId: string
  role: Generated<OrgMemberRole>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrgMemberInvite {
  id: string
  token: string
  email: string
  role: Generated<OrgMemberRole>
  rbacRoleId: string | null
  organizationId: string
  inviterId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface RuntimeEnvironment {
  id: string
  slug: string
  apiKey: string
  rootApiKeyHiddenAt: Timestamp | null
  pkApiKey: string
  type: Generated<RuntimeEnvironmentType>
  isBranchableEnvironment: Generated<boolean>
  branchName: string | null
  parentEnvironmentId: string | null
  previewAutoArchiveAfterDays: number | null
  previewAutoArchiveExcludedBranches: Generated<string[]>
  previewAutoArchiveNextCheckAt: Timestamp | null
  previewAutoArchiveCursorCreatedAt: Timestamp | null
  previewAutoArchiveCursorId: string | null
  git: unknown
  archivedAt: Timestamp | null
  shortcode: string
  maximumConcurrencyLimit: Generated<number>
  concurrencyLimitBurstFactor: Generated<string>
  paused: Generated<boolean>
  pauseSource: EnvironmentPauseSource | null
  autoEnableInternalSources: Generated<boolean>
  organizationId: string
  projectId: string
  orgMemberId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  builtInEnvironmentVariableOverrides: unknown
  tunnelId: string | null
  currentSessionId: string | null
}

export interface ApiKey {
  id: string
  name: string
  key_hash: string
  last_four: string
  description: string | null
  runtime_environment_id: string
  created_by_user_id: string | null
  issued_by: Generated<ApiKeyIssuer>
  preset_id: string | null
  scopes: string[]
  allowed_ip_ranges: Generated<string[]>
  last_used_at: Timestamp | null
  last_used_ip: string | null
  revoked_at: Timestamp | null
  expires_at: Timestamp | null
  updated_at: Timestamp
  created_at: Generated<Timestamp>
}

export interface RevokedApiKey {
  id: string
  apiKey: string
  runtimeEnvironmentId: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface Project {
  id: string
  slug: string
  name: string
  externalRef: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  initializedAt: Timestamp | null
  defaultRuntime: string | null
  version: Generated<ProjectVersion>
  engine: Generated<RunEngineVersion>
  builderProjectId: string | null
  defaultWorkerGroupId: string | null
  allowedMasterQueues: Generated<string[]>
  buildSettings: unknown
  onboardingData: unknown
}

export interface EnvironmentEncryptionPolicy {
  environmentId: string
  revision: Generated<number>
  wrappingProvider: EnvironmentKeyWrappingProvider | null
  wrappingKeyReference: string | null
  activeKeyId: string | null
}

export interface EnvironmentEncryptionKey {
  id: string
  environmentId: string
  wrappedKey: unknown
  wrappingProvider: EnvironmentKeyWrappingProvider
  wrappingKeyReference: string
  revision: Generated<number>
  createdAt: Generated<Timestamp>
}

export interface SecretReference {
  id: string
  key: string
  provider: Generated<SecretStoreProvider>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SecretStore {
  key: string
  value: unknown
  version: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OidcSigningKey {
  id: string
  kid: string
  purpose: Generated<OidcSigningKeyPurpose>
  status: OidcSigningKeyStatus
  publicJwk: unknown
  readyAt: Timestamp
  publicUntil: Timestamp | null
  generatedByUserId: string
  activatedAt: Timestamp | null
  activatedByUserId: string | null
  retiredAt: Timestamp | null
  retiredByUserId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DataMigration {
  id: string
  name: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  completedAt: Timestamp | null
}

export interface BackgroundWorker {
  id: string
  friendlyId: string
  engine: Generated<RunEngineVersion>
  contentHash: string
  sdkVersion: Generated<string>
  cliVersion: Generated<string>
  projectId: string
  runtimeEnvironmentId: string
  version: string
  metadata: unknown
  runtime: string | null
  runtimeVersion: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  workerGroupId: string | null
  supportsLazyAttempts: Generated<boolean>
}

export interface BackgroundWorkerFile {
  id: string
  friendlyId: string
  filePath: string
  contentHash: string
  contents: Buffer
  projectId: string
  createdAt: Generated<Timestamp>
}

export interface Prompt {
  id: string
  friendly_id: string
  slug: string
  description: string | null
  type: Generated<string>
  organizationId: string
  projectId: string
  runtimeEnvironmentId: string
  filePath: string | null
  exportName: string | null
  variableSchema: unknown
  defaultModel: string | null
  defaultConfig: unknown
  tags: Generated<string[]>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PromptVersion {
  id: string
  version: number
  promptId: string
  textContent: string | null
  chatContent: unknown
  model: string | null
  config: unknown
  source: string
  commitMessage: string | null
  contentHash: string
  labels: Generated<string[]>
  workerId: string | null
  createdBy: string | null
  createdAt: Generated<Timestamp>
}

export interface BackgroundWorkerTask {
  id: string
  slug: string
  description: string | null
  friendlyId: string
  filePath: string
  exportName: string | null
  workerId: string
  projectId: string
  fileId: string | null
  runtimeEnvironmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  queueConfig: unknown
  retryConfig: unknown
  machineConfig: unknown
  gates: unknown
  queueId: string | null
  maxDurationInSeconds: number | null
  ttl: string | null
  triggerSource: Generated<TaskTriggerSource>
  config: unknown
  payloadSchema: unknown
}

export interface WebhookEndpoint {
  id: string
  friendlyId: string
  opaqueId: string
  organizationId: string
  projectId: string
  runtimeEnvironmentId: string
  environmentType: RuntimeEnvironmentType
  endpointTenantId: Generated<string>
  endpointExternalRef: Generated<string>
  source: string
  declaredId: string
  routingTargets: Generated<unknown>
  verifierArtifact: unknown
  metadata: Generated<unknown>
  secretProvisioning: Generated<string>
  setupPrompt: string | null
  signingSecretKey: string | null
  status: Generated<WebhookEndpointStatus>
  manuallyDeactivatedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WebhookDelivery {
  id: string
  friendlyId: string
  webhookEndpointId: string
  organizationId: string
  projectId: string
  runtimeEnvironmentId: string
  environmentType: RuntimeEnvironmentType
  externalDeliveryId: string
  idempotencyKey: string
  runId: string | null
  status: Generated<WebhookDeliveryStatus>
  isTest: Generated<boolean>
  parsedEvent: unknown
  headers: unknown
  rawBodyHash: string | null
  errorMessage: string | null
  filterReason: string | null
  targets: Generated<unknown>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  processedAt: Timestamp | null
  retentionDays: Generated<number>
}

export interface Alert {
  id: string
  organizationId: string
  projectId: string
  environmentId: string
  capabilityKey: string
  params: unknown
  condition: unknown
  query: string | null
  expiresAt: Timestamp | null
  lifecycle: Generated<string>
  attention: Generated<string>
  observedCondition: Generated<string>
  observedConditionChangedAt: Timestamp | null
  pendingMetSince: Timestamp | null
  evaluatorState: unknown
  evaluationLeaseId: string | null
  evaluationLeaseUntil: Timestamp | null
  lastEvaluatedAt: Timestamp | null
  nextEvaluationAt: Timestamp | null
  notifiers: Generated<unknown>
  name: string | null
  createdBy: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  resolvedAt: Timestamp | null
  resolvedBy: string | null
  stoppedAt: Timestamp | null
  stoppedBy: string | null
  lastOccurrenceAt: Timestamp | null
  occurrencesSinceRaise: Generated<number>
  occurrencesTotal: Generated<bigint>
  groupId: string | null
}

export interface AlertEvent {
  id: string
  alertId: string
  type: string
  at: Generated<Timestamp>
  facts: Generated<unknown>
  actorId: string | null
  sourceEventId: string | null
}

export interface AlertOccurrenceSeen {
  alertId: string
  sourceEventId: string
  rowTime: Timestamp
  seenAt: Generated<Timestamp>
  expiresAt: Timestamp
}

export interface AlertDelivery {
  id: string
  alertEventId: string
  eventAt: Timestamp
  notifierId: string
  notifier: unknown
  status: Generated<string>
  claimId: string | null
  claimedAt: Timestamp | null
  deliveredAt: Timestamp | null
  failureReason: string | null
  providerMetadata: unknown
  attempts: Generated<number>
  firstAttemptAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface PlaygroundConversation {
  id: string
  chatId: string
  title: Generated<string>
  agentSlug: string
  runId: string | null
  clientData: unknown
  messages: unknown
  lastEventId: string | null
  projectId: string
  runtimeEnvironmentId: string
  userId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Session {
  id: string
  friendlyId: string
  externalId: string | null
  type: string
  projectId: string
  runtimeEnvironmentId: string
  environmentType: RuntimeEnvironmentType
  organizationId: string
  taskIdentifier: string
  triggerConfig: unknown
  isTest: Generated<boolean>
  tags: Generated<string[]>
  metadata: unknown
  currentRunId: string | null
  currentRunVersion: Generated<number>
  closedAt: Timestamp | null
  closedReason: string | null
  expiresAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  streamBasinName: string | null
  chatSnapshotStoragePath: string | null
}

export interface SessionRun {
  id: string
  sessionId: string
  runId: string
  reason: string
  triggeredAt: Generated<Timestamp>
}

export interface TaskRun {
  id: string
  number: Generated<number>
  friendlyId: string
  engine: Generated<RunEngineVersion>
  status: Generated<TaskRunStatus>
  statusReason: string | null
  idempotencyKey: string | null
  idempotencyKeyExpiresAt: Timestamp | null
  idempotencyKeyOptions: unknown
  debounce: unknown
  taskIdentifier: string
  isTest: Generated<boolean>
  payload: string
  payloadType: Generated<string>
  context: unknown
  traceContext: unknown
  traceId: string
  spanId: string
  runtimeEnvironmentId: string
  environmentType: RuntimeEnvironmentType | null
  projectId: string
  organizationId: string | null
  queue: string
  lockedQueueId: string | null
  masterQueue: Generated<string>
  region: string | null
  secondaryMasterQueue: string | null
  attemptNumber: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  runTags: string[]
  taskVersion: string | null
  sdkVersion: string | null
  cliVersion: string | null
  startedAt: Timestamp | null
  executedAt: Timestamp | null
  completedAt: Timestamp | null
  machinePreset: string | null
  usageDurationMs: Generated<number>
  costInCents: Generated<number>
  baseCostInCents: Generated<number>
  lockedAt: Timestamp | null
  lockedById: string | null
  lockedToVersionId: string | null
  priorityMs: Generated<number>
  concurrencyKey: string | null
  gates: unknown
  delayUntil: Timestamp | null
  queuedAt: Timestamp | null
  ttl: string | null
  expiredAt: Timestamp | null
  maxAttempts: number | null
  lockedRetryConfig: unknown
  oneTimeUseToken: string | null
  taskEventStore: Generated<string>
  queueTimestamp: Timestamp | null
  scheduleInstanceId: string | null
  scheduleId: string | null
  bulkActionGroupIds: Generated<string[]>
  logsDeletedAt: Timestamp | null
  replayedFromTaskRunFriendlyId: string | null
  rootTaskRunId: string | null
  parentTaskRunId: string | null
  parentTaskRunAttemptId: string | null
  batchId: string | null
  resumeParentOnCompletion: Generated<boolean>
  depth: Generated<number>
  parentSpanId: string | null
  runChainState: unknown
  seedMetadata: string | null
  seedMetadataType: Generated<string>
  metadata: string | null
  metadataType: Generated<string>
  metadataVersion: Generated<number>
  annotations: unknown
  isWarmStart: boolean | null
  output: string | null
  outputType: Generated<string>
  error: unknown
  planType: string | null
  maxDurationInSeconds: number | null
  realtimeStreamsVersion: Generated<string>
  realtimeStreams: Generated<string[]>
  streamBasinName: string | null
}

export interface TaskRunTemplate {
  id: string
  taskSlug: string
  triggerSource: TaskTriggerSource
  label: string
  payload: string | null
  payloadType: Generated<string>
  metadata: string | null
  metadataType: Generated<string>
  queue: string | null
  concurrencyKey: string | null
  ttlSeconds: number | null
  maxAttempts: number | null
  maxDurationSeconds: number | null
  tags: string[]
  machinePreset: string | null
  projectId: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskRunExecutionSnapshot {
  id: string
  engine: Generated<RunEngineVersion>
  executionStatus: TaskRunExecutionStatus
  description: string
  isValid: Generated<boolean>
  error: string | null
  previousSnapshotId: string | null
  runId: string
  runStatus: TaskRunStatus
  batchId: string | null
  attemptNumber: number | null
  environmentId: string
  environmentType: RuntimeEnvironmentType
  projectId: string
  organizationId: string
  completedWaitpointOrder: string[]
  checkpointId: string | null
  workerId: string | null
  runnerId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  lastHeartbeatAt: Timestamp | null
  metadata: unknown
}

export interface TaskRunCheckpoint {
  id: string
  friendlyId: string
  type: TaskRunCheckpointType
  location: string
  imageRef: string | null
  reason: string | null
  metadata: string | null
  projectId: string
  runtimeEnvironmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Waitpoint {
  id: string
  friendlyId: string
  type: WaitpointType
  status: Generated<WaitpointStatus>
  completedAt: Timestamp | null
  idempotencyKey: string
  userProvidedIdempotencyKey: boolean
  idempotencyKeyExpiresAt: Timestamp | null
  inactiveIdempotencyKey: string | null
  completedByTaskRunId: string | null
  completedAfter: Timestamp | null
  completedByBatchId: string | null
  output: string | null
  outputType: Generated<string>
  outputIsError: Generated<boolean>
  projectId: string
  environmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  tags: string[]
}

export interface TaskRunWaitpoint {
  id: string
  taskRunId: string
  waitpointId: string
  projectId: string
  spanIdToComplete: string | null
  batchId: string | null
  batchIndex: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WaitpointTag {
  id: string
  name: string
  environmentId: string
  projectId: string
  createdAt: Generated<Timestamp>
}

export interface FeatureFlag {
  id: string
  key: string
  value: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkerInstance {
  id: string
  name: string
  resourceIdentifier: string
  metadata: unknown
  workerGroupId: string
  organizationId: string | null
  projectId: string | null
  environmentId: string | null
  deploymentId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  lastDequeueAt: Timestamp | null
  lastHeartbeatAt: Timestamp | null
}

export interface WorkerInstanceGroup {
  id: string
  type: WorkerInstanceGroupType
  name: string
  masterQueue: string
  description: string | null
  hidden: Generated<boolean>
  tokenId: string
  organizationId: string | null
  projectId: string | null
  cloudProvider: string | null
  location: string | null
  staticIPs: string | null
  workloadType: Generated<WorkloadType>
  region: string | null
  enableFastPath: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkerGroupToken {
  id: string
  tokenHash: string
  previousTokenHash: string | null
  previousTokenValidUntil: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskRunTag {
  id: string
  name: string
  friendlyId: string
  projectId: string
  createdAt: Generated<Timestamp>
}

export interface TaskRunDependency {
  id: string
  taskRunId: string
  checkpointEventId: string | null
  dependentAttemptId: string | null
  dependentBatchRunId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  resumedAt: Timestamp | null
}

export interface TaskRunCounter {
  taskIdentifier: string
  lastNumber: Generated<number>
}

export interface TaskRunNumberCounter {
  id: string
  taskIdentifier: string
  environmentId: string
  lastNumber: Generated<number>
}

export interface TaskRunAttempt {
  id: string
  number: Generated<number>
  friendlyId: string
  taskRunId: string
  backgroundWorkerId: string
  backgroundWorkerTaskId: string
  runtimeEnvironmentId: string
  queueId: string
  status: Generated<TaskRunAttemptStatus>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  startedAt: Timestamp | null
  completedAt: Timestamp | null
  usageDurationMs: Generated<number>
  error: unknown
  output: string | null
  outputType: Generated<string>
}

export interface TaskEvent {
  id: string
  message: string
  traceId: string
  spanId: string
  parentId: string | null
  tracestate: string | null
  isError: Generated<boolean>
  isPartial: Generated<boolean>
  isCancelled: Generated<boolean>
  isDebug: Generated<boolean>
  serviceName: string
  serviceNamespace: string
  level: Generated<TaskEventLevel>
  kind: Generated<TaskEventKind>
  status: Generated<TaskEventStatus>
  links: unknown
  events: unknown
  startTime: bigint
  duration: Generated<bigint>
  attemptId: string | null
  attemptNumber: number | null
  environmentId: string
  environmentType: RuntimeEnvironmentType
  organizationId: string
  projectId: string
  projectRef: string
  runId: string
  runIsTest: Generated<boolean>
  idempotencyKey: string | null
  taskSlug: string
  taskPath: string | null
  taskExportName: string | null
  workerId: string | null
  workerVersion: string | null
  queueId: string | null
  queueName: string | null
  batchId: string | null
  properties: unknown
  metadata: unknown
  style: unknown
  output: unknown
  outputType: string | null
  payload: unknown
  payloadType: string | null
  createdAt: Generated<Timestamp>
  usageDurationMs: Generated<number>
  usageCostInCents: Generated<number>
  machinePreset: string | null
  machinePresetCpu: number | null
  machinePresetMemory: number | null
  machinePresetCentsPerMs: number | null
}

export interface TaskQueue {
  id: string
  friendlyId: string
  name: string
  type: Generated<TaskQueueType>
  version: Generated<TaskQueueVersion>
  orderableName: string | null
  concurrencyVersion: Generated<TaskQueueConcurrencyVersion>
  role: Generated<TaskQueueRole>
  projectId: string
  runtimeEnvironmentId: string
  concurrencyLimit: number | null
  concurrencyLimitOverriddenAt: Timestamp | null
  concurrencyLimitOverriddenBy: string | null
  concurrencyLimitBase: number | null
  concurrencyLimitOverridePercent: string | null
  totalConcurrencyLimit: number | null
  totalConcurrencyLimitOverriddenAt: Timestamp | null
  totalConcurrencyLimitOverriddenBy: string | null
  totalConcurrencyLimitBase: number | null
  rateLimit: unknown
  paused: Generated<boolean>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BatchTaskRun {
  id: string
  friendlyId: string
  idempotencyKey: string | null
  idempotencyKeyExpiresAt: Timestamp | null
  status: Generated<BatchTaskRunStatus>
  runtimeEnvironmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  runIds: Generated<string[]>
  runCount: Generated<number>
  payload: string | null
  payloadType: Generated<string>
  options: unknown
  batchVersion: Generated<string>
  sealed: Generated<boolean>
  sealedAt: Timestamp | null
  expectedCount: Generated<number>
  completedCount: Generated<number>
  completedAt: Timestamp | null
  resumedAt: Timestamp | null
  processingJobsCount: Generated<number>
  processingJobsExpectedCount: Generated<number>
  oneTimeUseToken: string | null
  processingStartedAt: Timestamp | null
  processingCompletedAt: Timestamp | null
  successfulRunCount: number | null
  failedRunCount: number | null
  taskIdentifier: string | null
  checkpointEventId: string | null
  dependentTaskAttemptId: string | null
}

export interface BatchTaskRunItem {
  id: string
  status: Generated<BatchTaskRunItemStatus>
  batchTaskRunId: string
  taskRunId: string
  taskRunAttemptId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  completedAt: Timestamp | null
}

export interface BatchTaskRunError {
  id: string
  batchTaskRunId: string
  index: number
  taskIdentifier: string
  payload: string | null
  options: unknown
  error: string
  errorCode: string | null
  createdAt: Generated<Timestamp>
}

export interface EnvironmentVariable {
  id: string
  friendlyId: string
  key: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface EnvironmentVariableValue {
  id: string
  valueReferenceId: string | null
  variableId: string
  environmentId: string
  isSecret: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  version: Generated<number>
  lastUpdatedBy: unknown
}

export interface Checkpoint {
  id: string
  friendlyId: string
  type: CheckpointType
  location: string
  imageRef: string
  reason: string | null
  metadata: string | null
  runId: string
  attemptId: string
  attemptNumber: number | null
  projectId: string
  runtimeEnvironmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface CheckpointRestoreEvent {
  id: string
  type: CheckpointRestoreEventType
  reason: string | null
  metadata: string | null
  checkpointId: string
  runId: string
  attemptId: string
  projectId: string
  runtimeEnvironmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkerDeployment {
  id: string
  contentHash: string
  friendlyId: string
  shortCode: string
  version: string
  runtime: string | null
  runtimeVersion: string | null
  cliVersion: string | null
  imageReference: string | null
  imagePlatform: Generated<string>
  templateKey: string | null
  indexObjectKey: string | null
  indexSha256: string | null
  indexFormatVersion: number | null
  externalBuildData: unknown
  buildServerMetadata: unknown
  buildEnvVars: unknown
  status: Generated<WorkerDeploymentStatus>
  type: Generated<WorkerDeploymentType>
  projectId: string
  environmentId: string
  workerId: string | null
  triggeredById: string | null
  triggeredVia: string | null
  commitSHA: string | null
  externalId: string | null
  startedAt: Timestamp | null
  installedAt: Timestamp | null
  builtAt: Timestamp | null
  deployedAt: Timestamp | null
  failedAt: Timestamp | null
  errorData: unknown
  canceledAt: Timestamp | null
  canceledReason: string | null
  git: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkerDeploymentPromotion {
  id: string
  label: string
  deploymentId: string
  environmentId: string
}

export interface TaskSchedule {
  id: string
  type: Generated<ScheduleType>
  friendlyId: string
  taskIdentifier: string
  deduplicationKey: string
  userProvidedDeduplicationKey: Generated<boolean>
  generatorExpression: string
  generatorDescription: Generated<string>
  generatorType: Generated<ScheduleGeneratorType>
  timezone: Generated<string>
  windowDurationSeconds: number | null
  windowPercentage: number | null
  defaultWindowDurationSeconds: number | null
  minimumWindowDurationSeconds: number | null
  externalId: string | null
  lastRunTriggeredAt: Timestamp | null
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  active: Generated<boolean>
}

export interface TaskScheduleInstance {
  id: string
  taskScheduleId: string
  environmentId: string
  projectId: string
  schedulePhase: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  active: Generated<boolean>
  lastScheduledTimestamp: Timestamp | null
  nextScheduledTimestamp: Timestamp | null
}

export interface RuntimeEnvironmentSession {
  id: string
  ipAddress: string
  environmentId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  disconnectedAt: Timestamp | null
}

export interface ProjectAlertChannel {
  id: string
  friendlyId: string
  deduplicationKey: string
  userProvidedDeduplicationKey: Generated<boolean>
  integrationId: string | null
  enabled: Generated<boolean>
  type: ProjectAlertChannelType
  name: string
  properties: unknown
  alertTypes: ProjectAlertType[]
  environmentTypes: Generated<RuntimeEnvironmentType[]>
  errorAlertConfig: unknown
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ProjectAlert {
  id: string
  friendlyId: string
  projectId: string
  environmentId: string
  channelId: string
  status: Generated<ProjectAlertStatus>
  type: ProjectAlertType
  taskRunAttemptId: string | null
  taskRunId: string | null
  workerDeploymentId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ProjectAlertStorage {
  id: string
  projectId: string
  alertChannelId: string
  alertType: ProjectAlertType
  storageId: string
  storageData: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationIntegration {
  id: string
  friendlyId: string
  service: IntegrationService
  externalOrganizationId: string | null
  integrationData: unknown
  tokenReferenceId: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface OrganizationProjectIntegration {
  id: string
  organizationIntegrationId: string
  projectId: string
  externalEntityId: string
  integrationData: unknown
  installedBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface BulkActionGroup {
  id: string
  friendlyId: string
  projectId: string
  environmentId: string | null
  type: BulkActionType
  status: Generated<BulkActionStatus>
  queryName: string | null
  params: unknown
  dedupeKey: string | null
  cursor: unknown
  successCount: Generated<number>
  failureCount: Generated<number>
  totalCount: Generated<number>
  completionNotification: Generated<BulkActionNotificationType>
  userId: string | null
  name: string | null
  completedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BulkActionItem {
  id: string
  friendlyId: string | null
  groupId: string
  type: BulkActionType
  status: Generated<BulkActionItemStatus>
  sourceRunId: string
  destinationRunId: string | null
  error: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface RealtimeStreamChunk {
  id: string
  key: string
  value: string
  sequence: number
  runId: string
  createdAt: Generated<Timestamp>
}

export interface TaskEventPartitioned {
  id: string
  message: string
  traceId: string
  spanId: string
  parentId: string | null
  tracestate: string | null
  isError: Generated<boolean>
  isPartial: Generated<boolean>
  isCancelled: Generated<boolean>
  serviceName: string
  serviceNamespace: string
  level: Generated<TaskEventLevel>
  kind: Generated<TaskEventKind>
  status: Generated<TaskEventStatus>
  links: unknown
  events: unknown
  startTime: bigint
  duration: Generated<bigint>
  attemptId: string | null
  attemptNumber: number | null
  environmentId: string
  environmentType: RuntimeEnvironmentType
  organizationId: string
  projectId: string
  projectRef: string
  runId: string
  runIsTest: Generated<boolean>
  idempotencyKey: string | null
  taskSlug: string
  taskPath: string | null
  taskExportName: string | null
  workerId: string | null
  workerVersion: string | null
  queueId: string | null
  queueName: string | null
  batchId: string | null
  properties: unknown
  metadata: unknown
  style: unknown
  output: unknown
  outputType: string | null
  payload: unknown
  payloadType: string | null
  createdAt: Generated<Timestamp>
  usageDurationMs: Generated<number>
  usageCostInCents: Generated<number>
  machinePreset: string | null
  machinePresetCpu: number | null
  machinePresetMemory: number | null
  machinePresetCentsPerMs: number | null
}

export interface GithubAppInstallation {
  id: string
  appInstallationId: bigint
  targetId: bigint
  targetType: string
  accountHandle: string
  permissions: unknown
  repositorySelection: GithubRepositorySelection
  installedBy: string | null
  installedByUserId: string | null
  organizationId: string
  deletedAt: Timestamp | null
  suspendedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface GithubRepository {
  id: string
  githubId: bigint
  name: string
  fullName: string
  htmlUrl: string
  private: boolean
  defaultBranch: string
  installationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TrustedDeployRepository {
  id: string
  projectId: string
  issuer: Generated<string>
  repositoryOwner: string
  repositoryName: string
  repositoryId: string | null
  repositoryOwnerId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TrustedDeployEnvironment {
  id: string
  trustedDeployRepositoryId: string
  runtimeEnvironmentId: string
  enabled: Generated<boolean>
  scope: TrustedDeployScope
  scopeValue: string | null
  additionalClaims: Generated<unknown>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ConnectedGithubRepository {
  id: string
  projectId: string
  repositoryId: string
  branchTracking: unknown
  previewDeploymentsEnabled: Generated<boolean>
  buildFilters: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ImpersonationAuditLog {
  id: string
  action: ImpersonationAuditLogAction
  adminId: string
  targetId: string
  ipAddress: string | null
  createdAt: Generated<Timestamp>
}

export interface SupportAccessRequest {
  id: string
  organizationId: string
  requestedById: string | null
  reason: string
  status: Generated<SupportAccessRequestStatus>
  approvedById: string | null
  approvedAt: Timestamp | null
  expiresAt: Timestamp | null
  cancelledById: string | null
  cancelledAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface CustomerQuery {
  id: string
  query: string
  scope: CustomerQueryScope
  stats: unknown
  title: string | null
  source: Generated<CustomerQuerySource>
  organizationId: string
  projectId: string | null
  environmentId: string | null
  userId: string | null
  filterPeriod: string | null
  filterFrom: Timestamp | null
  filterTo: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface IntegrationDeployment {
  id: string
  integrationName: string
  integrationDeploymentId: string
  commitSHA: string
  deploymentId: string | null
  status: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface MetricsDashboard {
  id: string
  friendlyId: string
  title: string
  description: Generated<string>
  organizationId: string
  projectId: string | null
  ownerId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  layout: string
}

export interface LlmModel {
  id: string
  friendly_id: string
  project_id: string | null
  model_name: string
  match_pattern: string
  start_date: Timestamp | null
  source: Generated<string>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  provider: string | null
  description: string | null
  context_window: number | null
  max_output_tokens: number | null
  capabilities: Generated<string[]>
  is_hidden: Generated<boolean>
  base_model_name: string | null
  pricing_unit: string | null
}

export interface LlmPricingTier {
  id: string
  model_id: string
  name: string
  is_default: Generated<boolean>
  priority: Generated<number>
  conditions: Generated<unknown>
}

export interface LlmPrice {
  id: string
  model_id: string
  pricing_tier_id: string
  usage_type: string
  price: string
}

export interface PlatformNotification {
  id: string
  friendlyId: string
  title: string
  payload: unknown
  surface: PlatformNotificationSurface
  scope: PlatformNotificationScope
  userId: string | null
  organizationId: string | null
  projectId: string | null
  startsAt: Timestamp
  endsAt: Timestamp
  cliMaxDaysAfterFirstSeen: number | null
  cliMaxShowCount: number | null
  cliShowEvery: number | null
  priority: Generated<number>
  isDraft: Generated<boolean>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PlatformNotificationInteraction {
  id: string
  notificationId: string
  userId: string
  firstSeenAt: Generated<Timestamp>
  showCount: Generated<number>
  webappDismissedAt: Timestamp | null
  webappClickedAt: Timestamp | null
  cliDismissedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ErrorGroupState {
  id: string
  organizationId: string
  projectId: string
  environmentId: string | null
  taskIdentifier: string
  errorFingerprint: string
  status: Generated<ErrorGroupStatus>
  ignoredUntil: Timestamp | null
  ignoredUntilOccurrenceRate: number | null
  ignoredUntilTotalOccurrences: number | null
  ignoredAtOccurrenceCount: bigint | null
  ignoredAt: Timestamp | null
  ignoredReason: string | null
  ignoredByUserId: string | null
  resolvedAt: Timestamp | null
  resolvedInVersion: string | null
  resolvedBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskIdentifier {
  id: string
  runtimeEnvironmentId: string
  projectId: string
  slug: string
  currentTriggerSource: Generated<TaskTriggerSource>
  currentWorkerId: string | null
  firstSeenAt: Generated<Timestamp>
  lastSeenAt: Generated<Timestamp>
  isInLatestDeployment: Generated<boolean>
}

export interface OrganizationDataStore {
  id: string
  key: string
  organizationIds: string[]
  kind: DataStoreKind
  config: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BackgroundWorkerToBackgroundWorkerFile {
  A: string
  B: string
}

export interface BackgroundWorkerToTaskQueue {
  A: string
  B: string
}

export interface WaitpointRunConnections {
  A: string
  B: string
}

export interface CompletedWaitpoints {
  A: string
  B: string
}

export interface DB {
  User: User
  MfaBackupCode: MfaBackupCode
  InvitationCode: InvitationCode
  AuthorizationCode: AuthorizationCode
  PersonalAccessToken: PersonalAccessToken
  OrganizationAccessToken: OrganizationAccessToken
  Organization: Organization
  OrgMember: OrgMember
  OrgMemberInvite: OrgMemberInvite
  RuntimeEnvironment: RuntimeEnvironment
  api_keys: ApiKey
  RevokedApiKey: RevokedApiKey
  Project: Project
  EnvironmentEncryptionPolicy: EnvironmentEncryptionPolicy
  EnvironmentEncryptionKey: EnvironmentEncryptionKey
  SecretReference: SecretReference
  SecretStore: SecretStore
  OidcSigningKey: OidcSigningKey
  DataMigration: DataMigration
  BackgroundWorker: BackgroundWorker
  BackgroundWorkerFile: BackgroundWorkerFile
  prompts: Prompt
  prompt_versions: PromptVersion
  BackgroundWorkerTask: BackgroundWorkerTask
  WebhookEndpoint: WebhookEndpoint
  WebhookDelivery: WebhookDelivery
  Alert: Alert
  AlertEvent: AlertEvent
  AlertOccurrenceSeen: AlertOccurrenceSeen
  AlertDelivery: AlertDelivery
  PlaygroundConversation: PlaygroundConversation
  Session: Session
  SessionRun: SessionRun
  TaskRun: TaskRun
  TaskRunTemplate: TaskRunTemplate
  TaskRunExecutionSnapshot: TaskRunExecutionSnapshot
  TaskRunCheckpoint: TaskRunCheckpoint
  Waitpoint: Waitpoint
  TaskRunWaitpoint: TaskRunWaitpoint
  WaitpointTag: WaitpointTag
  FeatureFlag: FeatureFlag
  WorkerInstance: WorkerInstance
  WorkerInstanceGroup: WorkerInstanceGroup
  WorkerGroupToken: WorkerGroupToken
  TaskRunTag: TaskRunTag
  TaskRunDependency: TaskRunDependency
  TaskRunCounter: TaskRunCounter
  TaskRunNumberCounter: TaskRunNumberCounter
  TaskRunAttempt: TaskRunAttempt
  TaskEvent: TaskEvent
  TaskQueue: TaskQueue
  BatchTaskRun: BatchTaskRun
  BatchTaskRunItem: BatchTaskRunItem
  BatchTaskRunError: BatchTaskRunError
  EnvironmentVariable: EnvironmentVariable
  EnvironmentVariableValue: EnvironmentVariableValue
  Checkpoint: Checkpoint
  CheckpointRestoreEvent: CheckpointRestoreEvent
  WorkerDeployment: WorkerDeployment
  WorkerDeploymentPromotion: WorkerDeploymentPromotion
  TaskSchedule: TaskSchedule
  TaskScheduleInstance: TaskScheduleInstance
  RuntimeEnvironmentSession: RuntimeEnvironmentSession
  ProjectAlertChannel: ProjectAlertChannel
  ProjectAlert: ProjectAlert
  ProjectAlertStorage: ProjectAlertStorage
  OrganizationIntegration: OrganizationIntegration
  OrganizationProjectIntegration: OrganizationProjectIntegration
  BulkActionGroup: BulkActionGroup
  BulkActionItem: BulkActionItem
  RealtimeStreamChunk: RealtimeStreamChunk
  TaskEventPartitioned: TaskEventPartitioned
  GithubAppInstallation: GithubAppInstallation
  GithubRepository: GithubRepository
  TrustedDeployRepository: TrustedDeployRepository
  TrustedDeployEnvironment: TrustedDeployEnvironment
  ConnectedGithubRepository: ConnectedGithubRepository
  ImpersonationAuditLog: ImpersonationAuditLog
  SupportAccessRequest: SupportAccessRequest
  CustomerQuery: CustomerQuery
  IntegrationDeployment: IntegrationDeployment
  MetricsDashboard: MetricsDashboard
  llm_models: LlmModel
  llm_pricing_tiers: LlmPricingTier
  llm_prices: LlmPrice
  PlatformNotification: PlatformNotification
  PlatformNotificationInteraction: PlatformNotificationInteraction
  ErrorGroupState: ErrorGroupState
  TaskIdentifier: TaskIdentifier
  OrganizationDataStore: OrganizationDataStore
  _BackgroundWorkerToBackgroundWorkerFile: BackgroundWorkerToBackgroundWorkerFile
  _BackgroundWorkerToTaskQueue: BackgroundWorkerToTaskQueue
  _WaitpointRunConnections: WaitpointRunConnections
  _completedWaitpoints: CompletedWaitpoints
}
