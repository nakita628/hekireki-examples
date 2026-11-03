import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type OrderStatus = 'PENDING' | 'ACCEPTED' | 'CANCELED' | 'COMPLETED'

export type From = 'BUYER' | 'SELLER'

export type State = 'QUEUE' | 'PUBLISHED' | 'ERROR' | 'DRAFT'

export type SubscriptionTier = 'STANDARD' | 'PRO' | 'TEAM' | 'ULTIMATE'

export type Period = 'MONTHLY' | 'YEARLY'

export type Provider = 'LOCAL' | 'GITHUB' | 'GOOGLE' | 'APPLE' | 'FARCASTER' | 'WALLET' | 'GENERIC'

export type Role = 'SUPERADMIN' | 'ADMIN' | 'USER'

export type APPROVED_SUBMIT_FOR_ORDER = 'NO' | 'WAITING_CONFIRMATION' | 'YES'

export type CreationMethod = 'UNKNOWN' | 'WEB' | 'MCP' | 'API' | 'AUTOPOST' | 'CLI'

export type ShortLinkPreference = 'ASK' | 'YES' | 'NO'

export type AnnouncementColor = 'INFO' | 'WARNING' | 'ERROR'

export interface Organization {
  id: string
  name: string
  description: string | null
  apiKey: string | null
  paymentId: string | null
  streakSince: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  allowTrial: Generated<boolean>
  isTrailing: Generated<boolean>
  shortlink: Generated<ShortLinkPreference>
}

export interface Tags {
  id: string
  name: string
  color: string
  orgId: string
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TagsPosts {
  postId: string
  tagId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface User {
  id: string
  email: string
  password: string | null
  providerName: Provider
  name: string | null
  lastName: string | null
  isSuperAdmin: Generated<boolean>
  bio: string | null
  audience: Generated<number>
  pictureId: string | null
  providerId: string | null
  timezone: number
  timezoneName: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  lastReadNotifications: Generated<Timestamp>
  inviteId: string | null
  activated: Generated<boolean>
  account: string | null
  connectedAccount: Generated<boolean>
  lastOnline: Generated<Timestamp>
  ip: string | null
  agent: string | null
  deletedAt: Timestamp | null
  sendSuccessEmails: Generated<boolean>
  sendFailureEmails: Generated<boolean>
  sendStreakEmails: Generated<boolean>
}

export interface UsedCodes {
  id: string
  code: string
  orgId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface UserOrganization {
  id: string
  userId: string
  organizationId: string
  disabled: Generated<boolean>
  role: Generated<Role>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface GitHub {
  id: string
  login: string | null
  name: string | null
  token: string
  jobId: string | null
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Trending {
  id: string
  trendingList: string
  language: string | null
  hash: string
  date: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TrendingLog {
  id: string
  language: string | null
  date: Timestamp
}

export interface ItemUser {
  id: string
  userId: string
  key: string
}

export interface Star {
  id: string
  stars: number
  totalStars: number
  forks: number
  totalForks: number
  login: string
  date: Generated<Timestamp>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Media {
  id: string
  name: string
  originalName: string | null
  path: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  fileSize: Generated<number>
  type: Generated<string>
  thumbnail: string | null
  alt: string | null
  thumbnailTimestamp: number | null
  status: Generated<string>
  processingError: string | null
}

export interface SocialMediaAgency {
  id: string
  userId: string
  name: string
  logoId: string | null
  website: string | null
  slug: string | null
  facebook: string | null
  instagram: string | null
  twitter: string | null
  linkedIn: string | null
  youtube: string | null
  tiktok: string | null
  otherSocialMedia: string | null
  shortDescription: string
  description: string
  approved: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface SocialMediaAgencyNiche {
  agencyId: string
  niche: string
}

export interface Credits {
  id: string
  organizationId: string
  credits: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: Generated<string>
}

export interface Clipping {
  id: string
  organizationId: string
  url: string
  status: Generated<string>
  error: string | null
  title: string | null
  thumbnail: string | null
  duration: number | null
  maxClips: Generated<number>
  fit: Generated<string>
  integrations: string
  creditsId: string | null
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ClippingClip {
  id: string
  clippingId: string
  title: string
  content: string
  start: number
  end: number
  trimStart: number | null
  status: Generated<string>
  error: string | null
  mediaId: string | null
  path: string | null
  thumbnail: string | null
  draftedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Subscription {
  id: string
  organizationId: string
  subscriptionTier: SubscriptionTier
  provider: Generated<string>
  identifier: string | null
  cancelAt: Timestamp | null
  period: Period
  totalChannels: number
  isLifetime: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Customer {
  id: string
  name: string
  orgId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Integration {
  id: string
  internalId: string
  organizationId: string
  name: string
  customName: string | null
  picture: string | null
  providerIdentifier: string
  type: string
  token: string
  disabled: Generated<boolean>
  tokenExpiration: Timestamp | null
  refreshToken: string | null
  profile: string | null
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
  inBetweenSteps: Generated<boolean>
  refreshNeeded: Generated<boolean>
  postingTimes: Generated<string>
  customInstanceDetails: string | null
  customerId: string | null
  rootInternalId: string | null
  additionalSettings: Generated<string | null>
}

export interface Signatures {
  id: string
  organizationId: string
  content: string
  autoAdd: boolean
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Comments {
  id: string
  content: string
  organizationId: string
  postId: string
  userId: string | null
  displayName: string | null
  parentId: string | null
  anchorStart: number | null
  anchorEnd: number | null
  anchorQuote: string | null
  resolvedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Post {
  id: string
  state: Generated<State>
  publishDate: Timestamp
  organizationId: string
  integrationId: string
  content: string
  delay: Generated<number>
  group: string
  batchId: string | null
  title: string | null
  description: string | null
  parentPostId: string | null
  releaseId: string | null
  releaseURL: string | null
  settings: string | null
  image: string | null
  submittedForOrderId: string | null
  submittedForOrganizationId: string | null
  approvedSubmitForOrder: Generated<APPROVED_SUBMIT_FOR_ORDER>
  creationMethod: Generated<CreationMethod>
  lastMessageId: string | null
  intervalInDays: number | null
  error: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Notifications {
  id: string
  organizationId: string
  content: string
  link: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface MessagesGroup {
  id: string
  buyerOrganizationId: string
  buyerId: string
  sellerId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PayoutProblems {
  id: string
  status: string
  orderId: string
  userId: string
  postId: string | null
  amount: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Orders {
  id: string
  buyerId: string
  sellerId: string
  status: OrderStatus
  messageGroupId: string
  captureId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrderItems {
  id: string
  orderId: string
  integrationId: string
  quantity: number
  price: number
}

export interface Messages {
  id: string
  from: From
  content: string | null
  groupId: string
  special: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Plugs {
  id: string
  organizationId: string
  plugFunction: string
  data: string
  integrationId: string
  activated: Generated<boolean>
}

export interface ExisingPlugData {
  id: string
  integrationId: string
  methodName: string
  value: string
}

export interface PopularPosts {
  id: string
  category: string
  topic: string
  content: string
  hook: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface IntegrationsWebhooks {
  integrationId: string
  webhookId: string
}

export interface Webhooks {
  id: string
  name: string
  organizationId: string
  url: string
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface AutoPost {
  id: string
  organizationId: string
  title: string
  content: string | null
  onSlot: boolean
  syncLast: boolean
  url: string
  lastUrl: string
  active: boolean
  addPicture: boolean
  generateContent: boolean
  integrations: string
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Sets {
  id: string
  organizationId: string
  name: string
  content: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ThirdParty {
  id: string
  organizationId: string
  identifier: string
  name: string
  internalId: string
  apiKey: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface Errors {
  id: string
  message: string
  platform: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  postId: string
  body: Generated<string>
}

export interface Mentions {
  name: string
  username: string
  platform: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  image: string
}

export interface mastra_messages {
  id: string
  thread_id: string
  content: string
  role: string
  type: string
  createdAt: Timestamp
  resourceId: string | null
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_resources {
  id: string
  workingMemory: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_scorers {
  id: string
  scorerId: string
  traceId: string | null
  runId: string
  scorer: unknown
  preprocessStepResult: unknown
  extractStepResult: unknown
  analyzeStepResult: unknown
  score: number
  reason: string | null
  metadata: unknown
  preprocessPrompt: string | null
  extractPrompt: string | null
  generateScorePrompt: string | null
  generateReasonPrompt: string | null
  analyzePrompt: string | null
  reasonPrompt: string | null
  input: unknown
  output: unknown
  additionalContext: unknown
  runtimeContext: unknown
  entityType: string | null
  entity: unknown
  entityId: string | null
  source: string
  resourceId: string | null
  threadId: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  spanId: string | null
  requestContext: unknown
  organizationId: string | null
  projectId: string | null
  batchId: string | null
  datasetId: string | null
  datasetItemId: string | null
}

export interface mastra_threads {
  id: string
  resourceId: string
  title: string
  metadata: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_traces {
  id: string
  parentSpanId: string | null
  name: string
  traceId: string
  scope: string
  kind: number
  attributes: unknown
  status: unknown
  events: unknown
  links: unknown
  other: string | null
  startTime: bigint
  endTime: bigint
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_workflow_snapshot {
  workflow_name: string
  run_id: string
  resourceId: string | null
  snapshot: string
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_agent_versions {
  id: string
  agentId: string
  versionNumber: number
  name: string
  description: string | null
  instructions: string
  model: unknown
  tools: unknown
  defaultOptions: unknown
  workflows: unknown
  agents: unknown
  integrationTools: unknown
  inputProcessors: unknown
  outputProcessors: unknown
  memory: unknown
  scorers: unknown
  mcpClients: unknown
  requestContextSchema: unknown
  workspace: unknown
  skills: unknown
  skillsFormat: string | null
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  durable: unknown
  browser: unknown
  toolProviders: unknown
}

export interface mastra_agents {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  visibility: string | null
  favoriteCount: number | null
}

export interface mastra_background_tasks {
  id: string
  tool_call_id: string
  tool_name: string
  agent_id: string
  run_id: string
  thread_id: string | null
  resource_id: string | null
  status: string
  args: unknown
  result: unknown
  error: unknown
  suspend_payload: unknown
  retry_count: number
  max_retries: number
  timeout_ms: number
  createdAt: Timestamp
  startedAt: Timestamp | null
  suspendedAt: Timestamp | null
  completedAt: Timestamp | null
  createdAtZ: Generated<Timestamp | null>
  startedAtZ: Generated<Timestamp | null>
  suspendedAtZ: Generated<Timestamp | null>
  completedAtZ: Generated<Timestamp | null>
}

export interface mastra_channel_config {
  platform: string
  data: unknown
  updatedAt: Timestamp
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_channel_installations {
  id: string
  platform: string
  agentId: string
  status: string
  webhookId: string | null
  data: unknown
  configHash: string | null
  error: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_dataset_items {
  id: string
  datasetId: string
  datasetVersion: number
  validTo: number | null
  isDeleted: boolean
  input: unknown
  groundTruth: unknown
  requestContext: unknown
  metadata: unknown
  source: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  expectedTrajectory: unknown
  organizationId: string | null
  projectId: string | null
  toolMocks: unknown
  unmockedToolPolicy: string | null
  scorerIds: unknown
  externalId: string | null
}

export interface mastra_dataset_versions {
  id: string
  datasetId: string
  version: number
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_datasets {
  id: string
  name: string
  description: string | null
  metadata: unknown
  inputSchema: unknown
  groundTruthSchema: unknown
  requestContextSchema: unknown
  tags: unknown
  targetType: string | null
  targetIds: unknown
  scorerIds: unknown
  version: number
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  organizationId: string | null
  projectId: string | null
  candidateKey: string | null
  candidateId: string | null
}

export interface mastra_experiment_results {
  id: string
  experimentId: string
  itemId: string
  itemDatasetVersion: number | null
  input: unknown
  output: unknown
  groundTruth: unknown
  error: unknown
  startedAt: Timestamp
  completedAt: Timestamp
  retryCount: number
  traceId: string | null
  status: string | null
  tags: unknown
  createdAt: Timestamp
  startedAtZ: Generated<Timestamp | null>
  completedAtZ: Generated<Timestamp | null>
  createdAtZ: Generated<Timestamp | null>
  comment: string | null
  toolMockReport: unknown
  metadata: unknown
  organizationId: string | null
  projectId: string | null
  attempt: number | null
}

export interface mastra_experiments {
  id: string
  name: string | null
  description: string | null
  metadata: unknown
  datasetId: string | null
  datasetVersion: number | null
  targetType: string
  targetId: string
  status: string
  totalItems: number
  succeededCount: number
  failedCount: number
  skippedCount: number
  startedAt: Timestamp | null
  completedAt: Timestamp | null
  agentVersion: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  startedAtZ: Generated<Timestamp | null>
  completedAtZ: Generated<Timestamp | null>
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  organizationId: string | null
  projectId: string | null
  provenance: unknown
  runnerAttestation: unknown
  experimentSetId: string | null
  comparisonId: string | null
  variantId: string | null
  trialIndex: number | null
  scorerIds: unknown
}

export interface mastra_favorites {
  userId: string
  entityType: string
  entityId: string
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_knowledge_activity {
  id: string
  action: string
  recordType: string
  recordId: string
  scope: unknown
  scopeKey: string
  sourceThreadId: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_knowledge_cursors {
  sourceThreadId: string
  agent: string
  lastKnowledgeId: string
  updatedAt: Timestamp
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_knowledge_mentions {
  sourceType: string
  sourceId: string
  recordId: string
}

export interface mastra_knowledge_nodes {
  id: string
  type: string
  name: string
  canonicalName: string
  kind: string | null
  content: string | null
  description: string | null
  scope: unknown
  scopeKey: string
  version: number
  mergedInto: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_knowledge_records {
  id: string
  node: string
  text: string
  scope: unknown
  scopeKey: string
  sourceThreadId: string
  capturedAt: Timestamp
  when: Timestamp | null
  maxScope: string | null
  metadata: unknown
  deletedAt: Timestamp | null
  deletedBy: string | null
  capturedAtZ: Generated<Timestamp | null>
  whenZ: Generated<Timestamp | null>
  deletedAtZ: Generated<Timestamp | null>
}

export interface mastra_knowledge_semantic_outbox {
  id: string
  idempotencyKey: string
  documentId: string
  documentType: string
  operation: string
  scope: unknown
  scopeKey: string
  status: string
  attempts: number
  availableAt: Timestamp
  claimedAt: Timestamp | null
  claimedBy: string | null
  createdAt: Timestamp
  completedAt: Timestamp | null
  availableAtZ: Generated<Timestamp | null>
  claimedAtZ: Generated<Timestamp | null>
  createdAtZ: Generated<Timestamp | null>
  completedAtZ: Generated<Timestamp | null>
}

export interface mastra_mcp_client_versions {
  id: string
  mcpClientId: string
  versionNumber: number
  name: string
  description: string | null
  servers: unknown
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_mcp_clients {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_mcp_server_versions {
  id: string
  mcpServerId: string
  versionNumber: number
  name: string
  version: string
  description: string | null
  instructions: string | null
  repository: unknown
  releaseDate: string | null
  isLatest: boolean | null
  packageCanonical: string | null
  tools: unknown
  agents: unknown
  workflows: unknown
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_mcp_servers {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_observational_memory {
  id: string
  lookupKey: string
  scope: string
  resourceId: string | null
  threadId: string | null
  activeObservations: string
  activeObservationsPendingUpdate: string | null
  originType: string
  config: string
  generationCount: number
  lastObservedAt: Timestamp | null
  lastReflectionAt: Timestamp | null
  pendingMessageTokens: number
  totalTokensObserved: number
  observationTokenCount: number
  isObserving: boolean
  isReflecting: boolean
  observedMessageIds: unknown
  observedTimezone: string | null
  bufferedObservations: string | null
  bufferedObservationTokens: number | null
  bufferedMessageIds: unknown
  bufferedReflection: string | null
  bufferedReflectionTokens: number | null
  bufferedReflectionInputTokens: number | null
  reflectedObservationLineCount: number | null
  bufferedObservationChunks: unknown
  isBufferingObservation: boolean
  isBufferingReflection: boolean
  lastBufferedAtTokens: number
  lastBufferedAtTime: Timestamp | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  lastObservedAtZ: Generated<Timestamp | null>
  lastReflectionAtZ: Generated<Timestamp | null>
  lastBufferedAtTimeZ: Generated<Timestamp | null>
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_prompt_block_versions {
  id: string
  blockId: string
  versionNumber: number
  name: string
  description: string | null
  content: string
  rules: unknown
  requestContextSchema: unknown
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_prompt_blocks {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_schedule_triggers {
  id: string
  schedule_id: string
  run_id: string | null
  scheduled_fire_at: bigint
  actual_fire_at: bigint
  outcome: string
  error: string | null
  trigger_kind: string
  parent_trigger_id: string | null
  metadata: unknown
}

export interface mastra_schedules {
  id: string
  target: unknown
  cron: string
  timezone: string | null
  status: string
  next_fire_at: bigint
  last_fire_at: bigint | null
  last_run_id: string | null
  created_at: bigint
  updated_at: bigint
  metadata: unknown
  owner_type: string | null
  owner_id: string | null
}

export interface mastra_scorer_definition_versions {
  id: string
  scorerDefinitionId: string
  versionNumber: number
  name: string
  description: string | null
  type: string
  model: unknown
  instructions: string | null
  scoreRange: unknown
  presetConfig: unknown
  defaultSampling: unknown
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_scorer_definitions {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  organizationId: string | null
  projectId: string | null
}

export interface mastra_skill_blobs {
  hash: string
  content: string
  size: number
  mimeType: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_skill_versions {
  id: string
  skillId: string
  versionNumber: number
  name: string
  description: string
  instructions: string
  license: string | null
  compatibility: unknown
  source: unknown
  references: unknown
  scripts: unknown
  assets: unknown
  metadata: unknown
  tree: unknown
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  files: unknown
}

export interface mastra_skills {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
  visibility: string | null
  favoriteCount: number | null
}

export interface mastra_thread_state {
  threadId: string
  type: string
  value: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_tool_provider_connections {
  authorId: string
  providerId: string
  connectionId: string
  toolkit: string
  label: string | null
  scope: string
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_workflow_definitions {
  id: string
  description: string | null
  metadata: unknown
  inputSchema: unknown
  outputSchema: unknown
  stateSchema: unknown
  requestContextSchema: unknown
  graph: unknown
  schedule: unknown
  status: string
  source: string
  authorId: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface mastra_workspace_versions {
  id: string
  workspaceId: string
  versionNumber: number
  name: string
  description: string | null
  filesystem: unknown
  sandbox: unknown
  mounts: unknown
  search: unknown
  skills: unknown
  tools: unknown
  autoSync: boolean | null
  operationTimeout: number | null
  changedFields: unknown
  changeMessage: string | null
  createdAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
}

export interface mastra_workspaces {
  id: string
  status: string
  activeVersionId: string | null
  authorId: string | null
  metadata: unknown
  createdAt: Timestamp
  updatedAt: Timestamp
  createdAtZ: Generated<Timestamp | null>
  updatedAtZ: Generated<Timestamp | null>
}

export interface OAuthApp {
  id: string
  organizationId: string | null
  name: string
  description: string | null
  pictureId: string | null
  redirectUrl: string
  redirectUris: string | null
  clientId: string
  clientSecret: string | null
  dynamic: Generated<boolean>
  tokenEndpointAuthMethod: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
}

export interface OAuthAuthorization {
  id: string
  oauthAppId: string
  userId: string
  organizationId: string
  accessToken: string | null
  authorizationCode: string | null
  codeExpiresAt: Timestamp | null
  codeChallenge: string | null
  codeChallengeMethod: string | null
  redirectUri: string | null
  revokedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OAuthSelfHostedAuthorization {
  id: string
  oauthAppId: string
  mcpUrl: string
  apiKey: string
  email: string | null
  accessToken: string | null
  authorizationCode: string | null
  codeExpiresAt: Timestamp | null
  codeChallenge: string | null
  codeChallengeMethod: string | null
  redirectUri: string | null
  revokedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Announcement {
  id: string
  title: string
  description: string
  color: Generated<AnnouncementColor>
  createdAt: Generated<Timestamp>
}

export interface DB {
  Organization: Organization
  Tags: Tags
  TagsPosts: TagsPosts
  User: User
  UsedCodes: UsedCodes
  UserOrganization: UserOrganization
  GitHub: GitHub
  Trending: Trending
  TrendingLog: TrendingLog
  ItemUser: ItemUser
  Star: Star
  Media: Media
  SocialMediaAgency: SocialMediaAgency
  SocialMediaAgencyNiche: SocialMediaAgencyNiche
  Credits: Credits
  Clipping: Clipping
  ClippingClip: ClippingClip
  Subscription: Subscription
  Customer: Customer
  Integration: Integration
  Signatures: Signatures
  Comments: Comments
  Post: Post
  Notifications: Notifications
  MessagesGroup: MessagesGroup
  PayoutProblems: PayoutProblems
  Orders: Orders
  OrderItems: OrderItems
  Messages: Messages
  Plugs: Plugs
  ExisingPlugData: ExisingPlugData
  PopularPosts: PopularPosts
  IntegrationsWebhooks: IntegrationsWebhooks
  Webhooks: Webhooks
  AutoPost: AutoPost
  Sets: Sets
  ThirdParty: ThirdParty
  Errors: Errors
  Mentions: Mentions
  mastra_messages: mastra_messages
  mastra_resources: mastra_resources
  mastra_scorers: mastra_scorers
  mastra_threads: mastra_threads
  mastra_traces: mastra_traces
  mastra_workflow_snapshot: mastra_workflow_snapshot
  mastra_agent_versions: mastra_agent_versions
  mastra_agents: mastra_agents
  mastra_background_tasks: mastra_background_tasks
  mastra_channel_config: mastra_channel_config
  mastra_channel_installations: mastra_channel_installations
  mastra_dataset_items: mastra_dataset_items
  mastra_dataset_versions: mastra_dataset_versions
  mastra_datasets: mastra_datasets
  mastra_experiment_results: mastra_experiment_results
  mastra_experiments: mastra_experiments
  mastra_favorites: mastra_favorites
  mastra_knowledge_activity: mastra_knowledge_activity
  mastra_knowledge_cursors: mastra_knowledge_cursors
  mastra_knowledge_mentions: mastra_knowledge_mentions
  mastra_knowledge_nodes: mastra_knowledge_nodes
  mastra_knowledge_records: mastra_knowledge_records
  mastra_knowledge_semantic_outbox: mastra_knowledge_semantic_outbox
  mastra_mcp_client_versions: mastra_mcp_client_versions
  mastra_mcp_clients: mastra_mcp_clients
  mastra_mcp_server_versions: mastra_mcp_server_versions
  mastra_mcp_servers: mastra_mcp_servers
  mastra_observational_memory: mastra_observational_memory
  mastra_prompt_block_versions: mastra_prompt_block_versions
  mastra_prompt_blocks: mastra_prompt_blocks
  mastra_schedule_triggers: mastra_schedule_triggers
  mastra_schedules: mastra_schedules
  mastra_scorer_definition_versions: mastra_scorer_definition_versions
  mastra_scorer_definitions: mastra_scorer_definitions
  mastra_skill_blobs: mastra_skill_blobs
  mastra_skill_versions: mastra_skill_versions
  mastra_skills: mastra_skills
  mastra_thread_state: mastra_thread_state
  mastra_tool_provider_connections: mastra_tool_provider_connections
  mastra_workflow_definitions: mastra_workflow_definitions
  mastra_workspace_versions: mastra_workspace_versions
  mastra_workspaces: mastra_workspaces
  OAuthApp: OAuthApp
  OAuthAuthorization: OAuthAuthorization
  OAuthSelfHostedAuthorization: OAuthSelfHostedAuthorization
  Announcement: Announcement
}
