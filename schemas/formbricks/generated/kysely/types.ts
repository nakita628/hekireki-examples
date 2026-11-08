import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type CreativeType = 'feedbackButton' | 'banner'

export type CreativeStatus = 'draft' | 'inProgress' | 'paused' | 'completed'

export type CreativeShowOn = 'pageView' | 'action'

export type CreativeDeviceType = 'phone' | 'tablet' | 'desktop'

export type PipelineTriggers = 'responseCreated' | 'responseUpdated' | 'responseFinished'

export type WebhookSource = 'user' | 'zapier' | 'make' | 'n8n' | 'activepieces'

export type ContactAttributeType = 'default' | 'custom'

export type ContactAttributeDataType = 'string' | 'number' | 'date'

export type ResponseOutcome = 'lowQuality'

export type SurveyStatus = 'draft' | 'inProgress' | 'paused' | 'completed'

export type SurveyAttributeFilterCondition = 'equals' | 'notEquals'

export type SurveyQuotaAction = 'endSurvey' | 'continueSurvey'

export type ResponseQuotaLinkStatus = 'screenedIn' | 'screenedOut'

export type SurveyType = 'link' | 'app'

export type displayOptions = 'displayOnce' | 'displayMultiple' | 'displaySome' | 'respondMultiple'

export type SurveyVisibility = 'restricted' | 'workspace'

export type SurveyScriptMode = 'add' | 'replace'

export type ActionType = 'code' | 'noCode'

export type IntegrationType = 'googleSheets' | 'notion' | 'airtable' | 'slack'

export type DataMigrationStatus = 'pending' | 'applied' | 'failed'

export type WidgetPlacement = 'bottomLeft' | 'bottomRight' | 'topLeft' | 'topRight' | 'center'

export type SurveyOverlay = 'none' | 'light' | 'dark'

export type BootstrapAdminMarker = 'bootstrapAdmin'

export type OrganizationRole = 'owner' | 'manager' | 'member' | 'billing' | 'dashboard_viewer'

export type ApiKeyPermission = 'read' | 'write' | 'manage'

export type IdentityProvider = 'email' | 'github' | 'google' | 'azuread' | 'openid' | 'saml'

export type EmbeddedDataSource = 'computed' | 'ingested' | 'reserved'

export type EmbeddedDataType = 'string' | 'number' | 'boolean' | 'date'

export type TeamUserRole = 'admin' | 'contributor'

export type WorkspaceTeamPermission = 'read' | 'readWrite' | 'manage'

export type ChartType = 'area' | 'bar' | 'pie' | 'big_number' | 'matrix'

export type FeedbackSourceType = 'formbricks_survey' | 'csv'

export type FeedbackSourceStatus = 'active' | 'paused' | 'error'

export type FeedbackSourceImportMode = 'completedOnly' | 'all'

export type FeedbackSourceElementScope = 'all' | 'specific'

export type HubFieldType =
  | 'text'
  | 'categorical'
  | 'nps'
  | 'csat'
  | 'ces'
  | 'rating'
  | 'number'
  | 'boolean'
  | 'date'

export type DeletionCleanupKind =
  | 'hubSurvey'
  | 'hubResponses'
  | 'storageFiles'
  | 'storageSurveyFolder'
  | 'hubResponseOutcome'

export type AttachmentExportStatus = 'queued' | 'running' | 'completed' | 'failed' | 'expired'

export type RetentionEntity = 'responses' | 'surveys' | 'members'

export type RetentionSurveyCondition = 'noResponse' | 'noChange' | 'createdBefore'

export type RetentionRunKind = 'sweep' | 'purge'

export type RetentionExemptionSource = 'manual' | 'restore'

export type RetentionTargetType = 'survey' | 'user'

export type RetentionRunItemAction = 'notified' | 'archived' | 'deactivated' | 'deleted' | 'skipped'

export type RetentionSkipReason = 'exempt' | 'lastOwner' | 'otherOrganization' | 'noRecipient'

export type WorkflowStatus = 'draft' | 'enabled' | 'disabled' | 'archived'

export type WorkflowRunStatus = 'queued' | 'running' | 'completed' | 'failed' | 'canceled'

export type WorkflowRunLogStatus = 'pending' | 'running' | 'succeeded' | 'failed' | 'skipped'

export interface Creative {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  type: CreativeType
  status: Generated<CreativeStatus>
  publishedAt: Timestamp | null
  archivedAt: Timestamp | null
  workspaceId: string
  createdBy: string | null
  content: unknown
  styling: unknown
  surveyId: string | null
  linkUrl: string | null
  showOn: Generated<CreativeShowOn>
  showConditions: unknown
  displayPercentage: number | null
  startAt: Timestamp | null
  endAt: Timestamp | null
  autoSelectLanguage: boolean | null
}

export interface CreativeDailyStat {
  creativeId: string
  date: Timestamp
  deviceType: CreativeDeviceType
  impressions: Generated<number>
  clicks: Generated<number>
}

export interface CreativeDailyPageStat {
  creativeId: string
  date: Timestamp
  page: string
  impressions: Generated<number>
  clicks: Generated<number>
}

export interface CreativeTrigger {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  creativeId: string
  actionClassId: string
}

export interface CreativeLanguage {
  languageId: string
  creativeId: string
  default: Generated<boolean>
  enabled: Generated<boolean>
}

export interface OutboundEmailAudit {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  expiresAt: Timestamp
  contentExpiresAt: Timestamp
  environment: string
  emailType: string
  trigger: string
  actorKind: string
  actorUserId: string | null
  configuredByUserId: string | null
  actorUserHash: string | null
  configuredByUserHash: string | null
  organizationId: string | null
  surveyId: string | null
  responseId: string | null
  followUpId: string | null
  workflowId: string | null
  workflowRunId: string | null
  inviteId: string | null
  recipientMac: string | null
  sesMessageHash: string | null
  snapshotKey: string | null
  snapshotSha256: string | null
  status: Generated<string>
  statusRecordedAt: Timestamp | null
  auditGap: Generated<boolean>
  erasureRequestedAt: Timestamp | null
  erasedAt: Timestamp | null
  contentDeleteRetryAt: Timestamp | null
  contentDeleteFailures: Generated<number>
}

export interface OutboundEmailAuditErasureIntent {
  id: string
  userId: string | null
  userHash: string
  recipientMac: string
  createdAt: Generated<Timestamp>
  activatedAt: Timestamp | null
  checkedAt: Timestamp | null
  expiresAt: Timestamp
}

export interface Webhook {
  id: string
  name: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  url: string
  source: Generated<WebhookSource>
  workspaceId: string
  triggers: PipelineTriggers[]
  surveyIds: string[]
  secret: string | null
}

export interface ContactAttribute {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  attributeKeyId: string
  contactId: string
  value: string
  valueNumber: number | null
  valueDate: Timestamp | null
}

export interface ContactAttributeKey {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  isUnique: Generated<boolean>
  key: string
  name: string | null
  description: string | null
  type: Generated<ContactAttributeType>
  dataType: Generated<ContactAttributeDataType>
  workspaceId: string
}

export interface Contact {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  workspaceId: string
}

export interface Response {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  finished: Generated<boolean>
  surveyId: string
  contactId: string | null
  endingId: string | null
  data: Generated<unknown>
  variables: Generated<unknown>
  ttc: Generated<unknown>
  meta: Generated<unknown>
  ingest_flags: unknown
  outcome: ResponseOutcome | null
  not_shown: unknown
  contactAttributes: unknown
  singleUseId: string | null
  language: string | null
  displayId: string | null
}

export interface Tag {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  workspaceId: string
}

export interface TagsOnResponses {
  responseId: string
  tagId: string
}

export interface Display {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  surveyId: string
  contactId: string | null
  creativeId: string | null
}

export interface SurveyTrigger {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  surveyId: string
  actionClassId: string
}

export interface SurveyAttributeFilter {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  attributeKeyId: string
  surveyId: string
  condition: SurveyAttributeFilterCondition
  value: string
}

export interface Survey {
  hiddenFields: Generated<unknown>
  variables: Generated<unknown>
  ownershipInitialized: Generated<boolean>
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  redirectUrl: string | null
  type: Generated<SurveyType>
  workspaceId: string
  createdBy: string | null
  visibility: Generated<SurveyVisibility>
  ownerId: string | null
  visibilityVersion: Generated<number>
  visibilityProjectedVersion: Generated<number>
  visibilityPending: Generated<boolean>
  visibilityChangedAt: Timestamp | null
  visibilityChangedById: string | null
  status: Generated<SurveyStatus>
  welcomeCard: Generated<unknown>
  questions: Generated<unknown>
  blocks: Generated<unknown[]>
  endings: Generated<unknown[]>
  displayOption: Generated<displayOptions>
  recontactDays: number | null
  displayLimit: number | null
  inlineTriggers: unknown
  autoClose: number | null
  keepEndingCardOpen: Generated<boolean>
  autoComplete: number | null
  delay: Generated<number>
  publishOn: Timestamp | null
  closeOn: Timestamp | null
  archivedAt: Timestamp | null
  surveyClosedMessage: unknown
  segmentId: string | null
  workspaceOverwrites: unknown
  styling: unknown
  singleUse: Generated<unknown>
  isVerifyEmailEnabled: Generated<boolean>
  isBackButtonHidden: Generated<boolean>
  isAutoProgressingEnabled: Generated<boolean>
  isCaptureIpEnabled: Generated<boolean>
  is_anonymize_responses_enabled: Generated<boolean>
  pin: string | null
  displayPercentage: string | null
  showLanguageSwitch: boolean | null
  autoSelectLanguage: boolean | null
  recaptcha: Generated<unknown>
  metadata: Generated<unknown>
  slug: string | null
  customHeadScripts: string | null
  customHeadScriptsMode: Generated<SurveyScriptMode | null>
  customCss: unknown
  showConditions: unknown
  startLogic: unknown
}

export interface SurveyQuota {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  surveyId: string
  name: string
  limit: number
  logic: Generated<unknown>
  action: SurveyQuotaAction
  endingCardId: string | null
  countPartialSubmissions: Generated<boolean>
}

export interface ResponseQuotaLink {
  responseId: string
  quotaId: string
  status: ResponseQuotaLinkStatus
}

export interface SurveyFollowUp {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  surveyId: string
  name: string
  trigger: unknown
  action: unknown
}

export interface ActionClass {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  description: string | null
  type: ActionType
  key: string | null
  noCodeConfig: unknown
  workspaceId: string
}

export interface Integration {
  id: string
  type: IntegrationType
  config: unknown
  workspaceId: string
}

export interface DataMigration {
  id: string
  started_at: Generated<Timestamp>
  finished_at: Timestamp | null
  name: string
  status: DataMigrationStatus
}

export interface Workspace {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  legacyEnvironmentId: string | null
  organizationId: string
  styling: Generated<unknown>
  config: Generated<unknown>
  recontactDays: Generated<number>
  linkSurveyBranding: Generated<boolean>
  inAppSurveyBranding: Generated<boolean>
  placement: Generated<WidgetPlacement>
  clickOutsideClose: Generated<boolean>
  overlay: Generated<SurveyOverlay>
  overlayColor: string | null
  overlayOpacity: number | null
  logo: unknown
  appSetupCompleted: Generated<boolean>
  customHeadScripts: string | null
  customCss: unknown
  customCssPrevious: unknown
}

export interface Organization {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  whitelabel: Generated<unknown>
  isAISmartToolsEnabled: Generated<boolean>
  displayTimeZone: string | null
}

export interface OrganizationBilling {
  organization_id: string
  stripe_customer_id: string | null
  limits: unknown
  usage_cycle_anchor: Timestamp | null
  stripe: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface Membership {
  organizationId: string
  userId: string
  accepted: Generated<boolean>
  role: Generated<OrganizationRole>
}

export interface Invite {
  id: string
  email: string
  name: string | null
  organizationId: string
  creatorId: string
  acceptorId: string | null
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
  role: Generated<OrganizationRole>
  teamIds: Generated<string[]>
}

export interface ApiKey {
  id: string
  createdAt: Generated<Timestamp>
  createdBy: string | null
  lastUsedAt: Timestamp | null
  label: string
  hashedKey: string
  lookupHash: string | null
  organizationId: string
  organizationAccess: Generated<unknown>
}

export interface ApiKeyWorkspace {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  apiKeyId: string
  workspaceId: string
  permission: ApiKeyPermission
}

export interface Account {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  userId: string
  type: string | null
  provider: string
  providerAccountId: string
  access_token: string | null
  refresh_token: string | null
  expires_at: number | null
  ext_expires_in: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
  password: string | null
  accessTokenExpiresAt: Timestamp | null
  refreshTokenExpiresAt: Timestamp | null
  issuer: string | null
}

export interface Session {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  sessionToken: string
  userId: string
  expires: Timestamp
  ipAddress: string | null
  userAgent: string | null
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface TwoFactor {
  id: string
  secret: string
  backupCodes: string
  userId: string
  verified: Generated<boolean>
  failedVerificationCount: Generated<number>
  lockedUntil: Timestamp | null
}

export interface jwks {
  id: string
  publicKey: string
  privateKey: string
  createdAt: Timestamp
  expiresAt: Timestamp | null
  alg: string | null
  crv: string | null
}

export interface oauthClient {
  id: string
  clientId: string
  clientSecret: string | null
  disabled: Generated<boolean | null>
  skipConsent: boolean | null
  enableEndSession: boolean | null
  subjectType: string | null
  scopes: Generated<string[]>
  userId: string | null
  createdAt: Timestamp | null
  updatedAt: Timestamp | null
  name: string | null
  uri: string | null
  icon: string | null
  contacts: Generated<string[]>
  tos: string | null
  policy: string | null
  softwareId: string | null
  softwareVersion: string | null
  softwareStatement: string | null
  redirectUris: string[]
  postLogoutRedirectUris: Generated<string[]>
  tokenEndpointAuthMethod: string | null
  grantTypes: Generated<string[]>
  responseTypes: Generated<string[]>
  public: boolean | null
  type: string | null
  requirePKCE: boolean | null
  referenceId: string | null
  metadata: unknown
  clientDiscoveryId: string | null
  clientCredentialsScopes: Generated<string[]>
  backchannelLogoutUri: string | null
  backchannelLogoutSessionRequired: boolean | null
  applicationType: string | null
  jwks: string | null
  jwksUri: string | null
  dpopBoundAccessTokens: Generated<boolean | null>
}

export interface oauthAccessToken {
  id: string
  token: string
  clientId: string
  sessionId: string | null
  userId: string | null
  referenceId: string | null
  refreshId: string | null
  expiresAt: Timestamp
  createdAt: Timestamp
  scopes: string[]
  authorizationCodeId: string | null
  resources: Generated<string[]>
  requestedUserInfoClaims: Generated<string[]>
  revoked: Timestamp | null
  confirmation: unknown
}

export interface oauthRefreshToken {
  id: string
  token: string
  clientId: string
  sessionId: string | null
  userId: string
  referenceId: string | null
  expiresAt: Timestamp
  createdAt: Timestamp
  revoked: Timestamp | null
  authTime: Timestamp | null
  scopes: string[]
  authorizationCodeId: string | null
  resources: Generated<string[]>
  requestedUserInfoClaims: Generated<string[]>
  rotatedAt: Timestamp | null
  rotationReplayResponse: string | null
  rotationReplayExpiresAt: Timestamp | null
  confirmation: unknown
}

export interface oauthConsent {
  id: string
  clientId: string
  userId: string | null
  referenceId: string | null
  scopes: string[]
  createdAt: Timestamp
  updatedAt: Timestamp
  resources: Generated<string[]>
  requestedUserInfoClaims: Generated<string[]>
}

export interface oauthResource {
  id: string
  identifier: string
  name: string
  accessTokenTtl: number | null
  refreshTokenTtl: number | null
  signingAlgorithm: string | null
  signingKeyId: string | null
  allowedScopes: Generated<string[]>
  customClaims: unknown
  dpopBoundAccessTokensRequired: Generated<boolean | null>
  disabled: Generated<boolean | null>
  createdAt: Timestamp | null
  updatedAt: Timestamp | null
  policyVersion: Generated<number | null>
  metadata: unknown
}

export interface oauthClientResource {
  id: string
  clientId: string
  resourceId: string
  metadata: unknown
  createdAt: Timestamp | null
}

export interface oauthClientAssertion {
  id: string
  expiresAt: Timestamp
}

export interface PasswordResetToken {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  token_hash: string
  expires_at: Timestamp
  userId: string
}

export interface User {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  email: string
  email_verified: Generated<boolean>
  twoFactorSecret: string | null
  twoFactorEnabled: Generated<boolean>
  backupCodes: string | null
  password: string | null
  identityProvider: Generated<IdentityProvider>
  identityProviderAccountId: string | null
  groupId: string | null
  notificationSettings: Generated<unknown>
  locale: Generated<string>
  lastLoginAt: Timestamp | null
  isActive: Generated<boolean>
  reactivatedAt: Timestamp | null
  lastActiveAt: Timestamp | null
  isBootstrapAdmin: BootstrapAdminMarker | null
}

export interface Segment {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  title: string
  description: string | null
  isPrivate: Generated<boolean>
  filters: Generated<unknown>
  workspaceId: string
}

export interface Language {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  code: string
  alias: string | null
  workspaceId: string
}

export interface SurveyLanguage {
  languageId: string
  surveyId: string
  default: Generated<boolean>
  enabled: Generated<boolean>
}

export interface EmbeddedData {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  key: string | null
  name: string
  description: string | null
  source: EmbeddedDataSource
  dataType: Generated<EmbeddedDataType>
  defaultValue: unknown
  locked: Generated<boolean>
  surveyId: string | null
  workspaceId: string
}

export interface SurveyEmbeddedData {
  id: string
  workspaceId: string
  surveyId: string
  embeddedDataId: string
  storageKey: string
  order: number
}

export interface Team {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  organizationId: string
}

export interface TeamUser {
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  teamId: string
  userId: string
  role: TeamUserRole
}

export interface WorkspaceTeam {
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  workspaceId: string
  teamId: string
  permission: Generated<WorkspaceTeamPermission>
}

export interface Chart {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  type: ChartType
  workspaceId: string
  query: Generated<unknown>
  config: Generated<unknown>
  createdBy: string | null
  feedbackDirectoryId: string
}

export interface Dashboard {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  workspaceId: string
  createdBy: string | null
  filters: Generated<unknown>
}

export interface DashboardWidget {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  dashboardId: string
  chartId: string
  layout: Generated<unknown>
  order: Generated<number>
}

export interface DeletionCleanup {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  kind: DeletionCleanupKind
  organizationId: string
  workspaceId: string
  surveyId: string
  tenantIds: Generated<string[]>
  responseIds: Generated<string[]>
  fileKeys: Generated<string[]>
  attempts: Generated<number>
  nextAttemptAt: Generated<Timestamp>
  lastError: string | null
}

export interface AttachmentExport {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  surveyId: string
  workspaceId: string
  requestedById: string | null
  status: Generated<AttachmentExportStatus>
  filterCriteria: unknown
  timeZone: string
  dispatchCount: Generated<number>
  fileCount: number
  responseCount: number
  includedFileCount: number | null
  totalBytes: bigint | null
  truncated: Generated<boolean>
  storageKey: string | null
  errorCode: string | null
  startedAt: Timestamp | null
  finishedAt: Timestamp | null
  expiresAt: Timestamp | null
}

export interface FeedbackSource {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  type: FeedbackSourceType
  status: Generated<FeedbackSourceStatus>
  import_mode: Generated<FeedbackSourceImportMode>
  elementScope: Generated<FeedbackSourceElementScope>
  workspaceId: string
  feedbackDirectoryId: string
  last_sync_at: Timestamp | null
  created_by: string | null
}

export interface FeedbackSourceFormbricksMapping {
  id: string
  created_at: Generated<Timestamp>
  feedback_source_id: string
  workspaceId: string
  surveyId: string
  elementId: string
  hubFieldType: HubFieldType
  custom_field_label: string | null
}

export interface FeedbackSourceFieldMapping {
  id: string
  created_at: Generated<Timestamp>
  feedback_source_id: string
  workspaceId: string
  source_field_id: string
  target_field_id: string
  static_value: string | null
}

export interface FeedbackDirectory {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  isArchived: Generated<boolean>
  organizationId: string
}

export interface FeedbackDirectoryWorkspace {
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  feedbackDirectoryId: string
  workspaceId: string
}

export interface AuthzedProjectionScopeState {
  scope: string
  readyAt: Timestamp | null
  readyBy: string | null
  updatedAt: Timestamp
}

export interface AuthzedProjectionOutbox {
  id: string
  targetType: string
  primaryId: string
  secondaryId: string | null
  isRevocation: Generated<boolean>
  attempts: Generated<number>
  permanentFailures: Generated<number>
  availableAt: Generated<Timestamp>
  leasedAt: Timestamp | null
  leaseExpiresAt: Timestamp | null
  leaseOwner: string | null
  processedAt: Timestamp | null
  deadLetteredAt: Timestamp | null
  lastAttemptAt: Timestamp | null
  lastErrorCode: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OnlineUpgradeState {
  release: string
  targetImage: string
  phase: string
  completedPhases: Generated<string[]>
  surveyCursor: string | null
  surveyCount: Generated<bigint>
  baselineCompletedAt: Timestamp | null
  forwardOnlyAt: Timestamp | null
  queuePausedAt: Timestamp | null
  queuePauseDeadlineAt: Timestamp | null
  queueResumedAt: Timestamp | null
  activatedAt: Timestamp | null
  updatedAt: Generated<Timestamp>
}

export interface RetentionPolicy {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  organizationId: string
  entity: RetentionEntity
  enabled: Generated<boolean>
  enabledAt: Timestamp | null
  warnDays: Generated<number>
  periodDays: number
  conditions: Generated<RetentionSurveyCondition[]>
  deferredAt: Timestamp | null
  updatedById: string | null
}

export interface RetentionExemption {
  id: string
  created_at: Generated<Timestamp>
  organizationId: string
  entity: RetentionEntity
  surveyId: string | null
  until: Timestamp
  source: Generated<RetentionExemptionSource>
  reason: string | null
  createdById: string | null
  revokedAt: Timestamp | null
  revokedById: string | null
}

export interface RetentionRun {
  id: string
  organizationId: string
  entity: RetentionEntity
  kind: Generated<RetentionRunKind>
  startedAt: Generated<Timestamp>
  finishedAt: Timestamp | null
  notifiedCount: Generated<number>
  archivedCount: Generated<number>
  deletedCount: Generated<number>
  skippedCount: Generated<number>
  hasChanges: Generated<boolean>
  scanCursor: string | null
}

export interface RetentionRunItem {
  id: string
  runId: string
  targetType: RetentionTargetType
  targetId: string
  targetName: string | null
  action: RetentionRunItemAction
  count: Generated<number>
  recipient: string | null
  skipReason: RetentionSkipReason | null
}

export interface RetentionNotice {
  id: string
  organizationId: string
  entity: RetentionEntity
  surveyId: string | null
  userId: string | null
  sentAt: Generated<Timestamp>
  deliveredAt: Timestamp | null
  emailSent: Generated<boolean>
  claimToken: string | null
  clockAt: Timestamp | null
}

export interface RetentionSurveyRestore {
  surveyId: string
  restoredAt: Timestamp
}

export interface Workflow {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  name: string
  description: string | null
  status: Generated<WorkflowStatus>
  workspaceId: string
  createdBy: string | null
  definition: unknown
}

export interface WorkflowVersion {
  id: string
  workflowId: string
  workspaceId: string
  version: number
  definition: unknown
  publishedAt: Generated<Timestamp>
  publishedBy: string | null
}

export interface WorkflowRun {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  workflowId: string
  workspaceId: string
  workflowVersionId: string | null
  responseId: string | null
  status: Generated<WorkflowRunStatus>
  triggerType: string
  surveyId: string | null
  isDryRun: Generated<boolean>
  idempotencyKey: string | null
  attempt: Generated<number>
  nextAttemptAt: Timestamp | null
  lastErrorAt: Timestamp | null
  triggerPayload: unknown
  data: Generated<unknown>
  error: string | null
  dispatchedAt: Timestamp | null
  startedAt: Timestamp | null
  finishedAt: Timestamp | null
}

export interface WorkflowRunLog {
  id: string
  runId: string
  sequence: number
  stepId: string
  stepType: string
  status: Generated<WorkflowRunLogStatus>
  input: Generated<unknown>
  output: Generated<unknown>
  error: string | null
  startedAt: Timestamp | null
  finishedAt: Timestamp | null
}

export interface DB {
  Creative: Creative
  CreativeDailyStat: CreativeDailyStat
  CreativeDailyPageStat: CreativeDailyPageStat
  CreativeTrigger: CreativeTrigger
  CreativeLanguage: CreativeLanguage
  OutboundEmailAudit: OutboundEmailAudit
  OutboundEmailAuditErasureIntent: OutboundEmailAuditErasureIntent
  Webhook: Webhook
  ContactAttribute: ContactAttribute
  ContactAttributeKey: ContactAttributeKey
  Contact: Contact
  Response: Response
  Tag: Tag
  TagsOnResponses: TagsOnResponses
  Display: Display
  SurveyTrigger: SurveyTrigger
  SurveyAttributeFilter: SurveyAttributeFilter
  Survey: Survey
  SurveyQuota: SurveyQuota
  ResponseQuotaLink: ResponseQuotaLink
  SurveyFollowUp: SurveyFollowUp
  ActionClass: ActionClass
  Integration: Integration
  DataMigration: DataMigration
  Workspace: Workspace
  Organization: Organization
  OrganizationBilling: OrganizationBilling
  Membership: Membership
  Invite: Invite
  ApiKey: ApiKey
  ApiKeyWorkspace: ApiKeyWorkspace
  Account: Account
  Session: Session
  VerificationToken: VerificationToken
  TwoFactor: TwoFactor
  jwks: jwks
  oauthClient: oauthClient
  oauthAccessToken: oauthAccessToken
  oauthRefreshToken: oauthRefreshToken
  oauthConsent: oauthConsent
  oauthResource: oauthResource
  oauthClientResource: oauthClientResource
  oauthClientAssertion: oauthClientAssertion
  PasswordResetToken: PasswordResetToken
  User: User
  Segment: Segment
  Language: Language
  SurveyLanguage: SurveyLanguage
  EmbeddedData: EmbeddedData
  SurveyEmbeddedData: SurveyEmbeddedData
  Team: Team
  TeamUser: TeamUser
  WorkspaceTeam: WorkspaceTeam
  Chart: Chart
  Dashboard: Dashboard
  DashboardWidget: DashboardWidget
  DeletionCleanup: DeletionCleanup
  AttachmentExport: AttachmentExport
  FeedbackSource: FeedbackSource
  FeedbackSourceFormbricksMapping: FeedbackSourceFormbricksMapping
  FeedbackSourceFieldMapping: FeedbackSourceFieldMapping
  FeedbackDirectory: FeedbackDirectory
  FeedbackDirectoryWorkspace: FeedbackDirectoryWorkspace
  AuthzedProjectionScopeState: AuthzedProjectionScopeState
  AuthzedProjectionOutbox: AuthzedProjectionOutbox
  OnlineUpgradeState: OnlineUpgradeState
  RetentionPolicy: RetentionPolicy
  RetentionExemption: RetentionExemption
  RetentionRun: RetentionRun
  RetentionRunItem: RetentionRunItem
  RetentionNotice: RetentionNotice
  RetentionSurveyRestore: RetentionSurveyRestore
  Workflow: Workflow
  WorkflowVersion: WorkflowVersion
  WorkflowRun: WorkflowRun
  WorkflowRunLog: WorkflowRunLog
}
