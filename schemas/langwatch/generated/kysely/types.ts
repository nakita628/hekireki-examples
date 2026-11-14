import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type SsoConnectionMigrationPhase =
  | 'SETUP'
  | 'GRACE_LEGACY'
  | 'GRACE_DIRECT'
  | 'FINALIZING'
  | 'FINALIZED'

export type TeamUserRole = 'ADMIN' | 'MEMBER' | 'VIEWER' | 'CUSTOM'

export type RoleBindingScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type GrantScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT' | 'RESOURCE' | 'PLATFORM'

export type GrantPrincipalType =
  | 'USER'
  | 'API_KEY'
  | 'GROUP'
  | 'TEAM'
  | 'ORGANIZATION'
  | 'PROJECT'
  | 'ANYONE'

export type OrganizationUserRole = 'ADMIN' | 'MEMBER' | 'EXTERNAL' | 'DEVELOPER'

export type IssuedLicenseSource = 'BACKOFFICE' | 'PURCHASE' | 'SCRIPT' | 'LEGACY_IMPORT'

export type ConnectedCreditGrantKind = 'commit' | 'added' | 'renewal'

export type ConnectedInvoiceKind = 'annual' | 'seat_change' | 'usage'

export type PlanTypes =
  | 'FREE'
  | 'PRO'
  | 'GROWTH'
  | 'GROWTH_SEAT_EUR_MONTHLY'
  | 'GROWTH_SEAT_EUR_ANNUAL'
  | 'GROWTH_SEAT_USD_MONTHLY'
  | 'GROWTH_SEAT_USD_ANNUAL'
  | 'ENTERPRISE'
  | 'LAUNCH'
  | 'ACCELERATE'
  | 'LAUNCH_ANNUAL'
  | 'ACCELERATE_ANNUAL'

export type PricingModel = 'TIERED' | 'SEAT_EVENT'

export type OrganizationIntent = 'AGENT_GOVERNANCE' | 'LLM_OPS'

export type SubscriptionStatus = 'PENDING' | 'FAILED' | 'ACTIVE' | 'CANCELLED'

export type Currency = 'USD' | 'EUR'

export type INVITE_STATUS =
  | 'PENDING'
  | 'ACCEPTED'
  | 'WAITING_APPROVAL'
  | 'PAYMENT_PENDING'
  | 'REVOKED'

export type EvaluationExecutionMode = 'ON_MESSAGE' | 'AS_GUARDRAIL' | 'MANUALLY'

export type CostType = 'TRACE_CHECK' | 'GUARDRAIL' | 'CLUSTERING' | 'BATCH_EVALUATION'

export type CostReferenceType = 'CHECK' | 'TRACE' | 'PROJECT' | 'BATCH'

export type DashboardScope = 'PRIVATE' | 'PROJECT' | 'ORGANIZATION'

export type TriggerAction =
  | 'SEND_EMAIL'
  | 'ADD_TO_DATASET'
  | 'ADD_TO_ANNOTATION_QUEUE'
  | 'SEND_SLACK_MESSAGE'
  | 'SEND_WEBHOOK'

export type TriggerKind = 'AUTOMATION' | 'ALERT' | 'REPORT'

export type AlertType = 'CRITICAL' | 'WARNING' | 'INFO'

export type SlackIntegrationScopeType = 'ORGANIZATION' | 'PROJECT'

export type SlackIntegrationKind = 'BOT' | 'INCOMING_WEBHOOK'

export type WebhookDeliveryOutcome = 'success' | 'retryable' | 'terminal' | 'pending'

export type WebhookDestinationKind = 'http' | 'sqs'

export type WebhookEndpointStatus = 'ACTIVE' | 'DISABLED'

export type WebhookDeliveryChannel = 'platform' | 'automations'

export type ExperimentType = 'DSPY' | 'BATCH_EVALUATION' | 'BATCH_EVALUATION_V2' | 'EVALUATIONS_V3'

export type ModelProviderScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type ModelDefaultScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type LangyProjectionTitleSource = 'derived' | 'auto' | 'user'

export type AnnotationScoreDataType = 'CATEGORICAL' | 'BOOLEAN' | 'LIKERT' | 'OPTION' | 'CHECKBOX'

export type ShareResourceType = 'TRACE' | 'THREAD'

export type ShareVisibility = 'PUBLIC' | 'ORGANIZATION' | 'PROJECT'

export type PinSource = 'manual' | 'share'

export type RetentionPolicyScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type DataPrivacyScopeType = 'ORGANIZATION' | 'DEPARTMENT' | 'TEAM' | 'PROJECT'

export type CustomLLMModelCostScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type PromptScope = 'ORGANIZATION' | 'PROJECT'

export type VirtualKeyStatus = 'ACTIVE' | 'DISABLED' | 'REVOKED'

export type VirtualKeyPurpose = 'USER' | 'LANGY' | 'CONNECT'

export type GatewayProviderRotationPolicy = 'MANUAL'

export type GatewayProviderHealthStatus = 'UNKNOWN' | 'HEALTHY' | 'DEGRADED' | 'CIRCUIT_OPEN'

export type GatewayBudgetScopeType =
  | 'ORGANIZATION'
  | 'TEAM'
  | 'PROJECT'
  | 'VIRTUAL_KEY'
  | 'PRINCIPAL'
  | 'GROUP'
  | 'ATTRIBUTED_USER'

export type GatewayBudgetWindow = 'MINUTE' | 'HOUR' | 'DAY' | 'WEEK' | 'MONTH' | 'TOTAL' | 'MANUAL'

export type GatewayBudgetBreachAction = 'BLOCK' | 'WARN'

export type GatewayBudgetLedgerStatus =
  | 'SUCCESS'
  | 'PROVIDER_ERROR'
  | 'BLOCKED_BY_GUARDRAIL'
  | 'CANCELLED'

export type GatewayChangeEventKind =
  | 'VK_CREATED'
  | 'VK_CONFIG_UPDATED'
  | 'VK_REVOKED'
  | 'VK_ROTATED'
  | 'VK_DISABLED'
  | 'VK_ENABLED'
  | 'BUDGET_CREATED'
  | 'BUDGET_UPDATED'
  | 'BUDGET_DELETED'
  | 'MODEL_PROVIDER_UPDATED'
  | 'CACHE_RULE_CREATED'
  | 'CACHE_RULE_UPDATED'
  | 'CACHE_RULE_DELETED'
  | 'ROUTING_POLICY_UPDATED'
  | 'ROUTING_POLICY_DELETED'

export type GatewayCacheRuleMode = 'RESPECT' | 'FORCE' | 'DISABLE'

export type VirtualKeyRoutingMode = 'NONE' | 'FALLBACK_ALL' | 'POLICY'

export type VirtualKeyScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type RoutingPolicyScopeType = 'ORGANIZATION' | 'TEAM' | 'PROJECT'

export type GatewayGuardrailDirection = 'PRE' | 'POST' | 'STREAM_CHUNK'

export type GatewayGuardrailFailureMode = 'FAIL_OPEN' | 'FAIL_CLOSED'

export type FeatureFlagExperimentSubjectType = 'USER' | 'ORGANIZATION' | 'PROJECT'

export type ProcessManagerOutboxStatus = 'pending' | 'dispatched' | 'dead' | 'discarded'

export type ProcessManagerOutboxAttemptOutcome = 'retry_scheduled' | 'dead'

export type GatewayRealtimeSessionStatus = 'OPEN' | 'CLOSED' | 'FAILED' | 'EXPIRED'

export type ConnectedSeatChangeState =
  | 'intent'
  | 'invoiced'
  | 'nothing_to_invoice'
  | 'not_onboarded'

export interface Account {
  id: string
  userId: string
  type: Generated<string>
  provider: string
  issuer: string | null
  providerAccountId: string
  refresh_token: string | null
  access_token: string | null
  expires_at: Timestamp | null
  ext_expires_in: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
  password: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface AccountCredential {
  id: string
  userId: string
  provider: string
  password: string | null
  accessToken: string | null
  refreshToken: string | null
  idToken: string | null
  accessTokenExpiresAt: Timestamp | null
  refreshTokenExpiresAt: Timestamp | null
  scope: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
  ipAddress: string | null
  userAgent: string | null
  identifierId: string | null
  amr: Generated<string[]>
  startedBySignUp: Generated<boolean>
  impersonating: unknown
  actorUserId: string | null
  subjectUserId: string | null
  impersonationReason: string | null
  impersonationExpiresAt: Timestamp | null
  lastSeenAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface UserErasure {
  id: string
  accountCreatedAt: Timestamp
  erasedAt: Generated<Timestamp>
}

export interface User {
  id: string
  name: string | null
  email: string | null
  emailVerified: Generated<boolean>
  signupConfirmationPending: Generated<boolean>
  passkeySignupClaimHash: string | null
  image: string | null
  pendingSsoSetup: Generated<boolean>
  userHashKey: string | null
  twoFactorEnabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastLoginAt: Timestamp | null
  deactivatedAt: Timestamp | null
  lastHomePath: string | null
  tracesExplorerTourDismissedAt: Timestamp | null
  langyCodeAccessPreference: string | null
  passkeyNudgeDismissedAt: Timestamp | null
  joinOfferDismissedDomains: Generated<string[]>
  notificationPreferences: Generated<unknown>
  passwordResetRequiredAt: Timestamp | null
}

export interface VerificationToken {
  id: string
  identifier: string
  token: string
  expires: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Identifier {
  id: string
  userId: string
  provider: string
  value: string | null
  domain: string | null
  identifierHash: string | null
  accountId: string | null
  providerId: string | null
  issuer: string | null
  providerAccountId: string | null
  state: string
  connectionId: string | null
  verifiedAt: Timestamp | null
  attachedAt: Timestamp
  detachedAt: Timestamp | null
  lastUsedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface IdentitySignInSwitch {
  userId: string
  onIdentifiers: boolean
  pinned: Generated<boolean>
  switchedAt: Timestamp
  lastAppliedEventId: string | null
  updatedAt: Generated<Timestamp>
}

export interface IdentifierReservation {
  normalizedValue: string
  userId: string
  identifierId: string
  commandId: string
  createdAt: Generated<Timestamp>
}

export interface IdentityProjectionCursor {
  userId: string
  lastEventId: string
  acceptedAt: Timestamp
  occurredAt: Timestamp
  projectionVersion: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TwoFactor {
  id: string
  userId: string
  secret: string
  backupCodes: string
  verified: Generated<boolean>
  failedVerificationCount: Generated<number>
  lockedUntil: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SignInAttemptLock {
  id: string
  identifierHash: string
  failedCount: Generated<number>
  lockedUntil: Timestamp | null
  consecutiveLockouts: Generated<number>
  heldForReview: Generated<boolean>
  userId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Passkey {
  id: string
  name: string | null
  publicKey: string
  userId: string
  credentialID: string
  counter: Generated<number>
  deviceType: string
  backedUp: Generated<boolean>
  transports: string | null
  aaguid: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface MfaEnrollment {
  userId: string
  enrollmentId: string | null
  method: string | null
  state: string
  enrolledAt: Timestamp | null
  confirmedAt: Timestamp | null
  expiredAt: Timestamp | null
  disabledAt: Timestamp | null
  disabledVia: string | null
  backupCodeCount: Generated<number>
  consumedBackupCodeIndexes: Generated<number[]>
  failedCount: Generated<number>
  occurredAt: Timestamp
  lastEventId: string
  acceptedAt: Timestamp
  projectionVersion: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SsoConnection {
  id: string
  organizationId: string
  type: string
  state: string
  claimedDomains: string[]
  domainClaims: Generated<unknown>
  approvedDomains: string[]
  verifiedDomains: string[]
  lapsedDomains: string[]
  domainVerifications: Generated<unknown>
  pendingVerification: unknown
  idpMetadata: unknown
  arrivalPolicy: Generated<string>
  allowsJit: Generated<boolean>
  arrivalPolicyDecidedAt: Timestamp | null
  source: string
  testLoginAccountId: string | null
  replacesConnectionId: string | null
  migrationPhase: SsoConnectionMigrationPhase | null
  graceStartedAt: Timestamp | null
  routeChangedAt: Timestamp | null
  finalizationRequestedAt: Timestamp | null
  finalizedAt: Timestamp | null
  rejection: unknown
  createdBy: string | null
  tearDownAfter: Timestamp | null
  occurredAt: Timestamp
  lastEventId: string
  acceptedAt: Timestamp
  projectionVersion: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface SsoConnectionRegistrationSlot {
  organizationId: string
  kind: string
  connectionId: string
  replacesConnectionId: string | null
  commandId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SsoConnectionReproofCursor {
  connectionId: string
  lastReproofAt: Timestamp
}

export interface SsoCredential {
  id: string
  organizationId: string
  connectionId: string
  kind: string
  ciphertext: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SsoVerifiedDomain {
  domain: string
  organizationId: string
}

export interface SsoVerifiedDomainHolder {
  domain: string
  connectionId: string
  organizationId: string
}

export interface SsoAuthenticationActivity {
  id: string
  organizationId: string
  connectionId: string
  userId: string
  authenticatedAt: Timestamp
  providerAccountId: string | null
}

export interface SsoProvider {
  id: string
  issuer: string
  oidcConfig: string | null
  samlConfig: string | null
  userId: string | null
  providerId: string
  organizationId: string | null
  domain: string
}

export interface SsoBreakGlassBinding {
  id: string
  organizationId: string
  userId: string
  grantedByUserId: string
  grantedAt: Timestamp
  expiresAt: Timestamp
  supersededAt: Timestamp | null
  renewedFromId: string | null
  warnedDays: number[]
  createdAt: Generated<Timestamp>
}

export interface SsoActivationRecoveryReservation {
  commandId: string
  organizationId: string
  connectionId: string
  createdAt: Timestamp
}

export interface JoinRequest {
  id: string
  userId: string
  organizationId: string
  domain: string
  state: string
  matchedVia: string
  origin: Generated<string>
  expiresAt: Timestamp | null
  resolvedAt: Timestamp | null
  resolvedByType: string | null
  resolvedById: string | null
  withdrawalCause: string | null
  connectionId: string | null
  occurredAt: Timestamp
  lastEventId: string
  acceptedAt: Timestamp
  projectionVersion: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface TeamUser {
  userId: string
  teamId: string
  role: TeamUserRole
  assignedRoleId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface OrganizationUser {
  userId: string
  organizationId: string
  role: OrganizationUserRole
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  departmentId: string | null
  disabledAt: Timestamp | null
  pendingSsoGrantId: string | null
  membershipStamp: Generated<string>
  fullSeatSince: Timestamp | null
  seatPendingSince: Timestamp | null
}

export interface SeatHold {
  id: string
  organizationId: string
  userId: string
  email: string | null
  name: string | null
  seatTakenAt: Timestamp
  heldUntil: Timestamp
  reason: string
  createdAt: Generated<Timestamp>
}

export interface SeatRequest {
  id: string
  organizationId: string
  email: string
  userId: string | null
  name: string | null
  status: string
  source: string
  idpRefused: Generated<boolean>
  idpKind: string | null
  salesNotifiedAt: Timestamp | null
  openedAt: Timestamp
  resolvedAt: Timestamp | null
  resolvedByUserId: string | null
  seatPendingSince: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface OrganizationSeatLease {
  organizationId: string
  holder: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface OrganizationAccessNotice {
  id: string
  organizationId: string
  kind: string
  email: string
  name: string | null
  idp: string | null
  dayKey: string
  idpRefused: Generated<boolean>
  createdAt: Generated<Timestamp>
  mailedAt: Timestamp | null
}

export interface Team {
  id: string
  name: string
  slug: string
  organizationId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  archivedAt: Timestamp | null
  isPersonal: Generated<boolean>
  ownerUserId: string | null
  departmentId: string | null
}

export interface Organization {
  id: string
  name: string
  kind: Generated<string>
  phoneNumber: string | null
  slug: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  usageSpendingMaxLimit: number | null
  datasetAttachmentMaxMb: number | null
  privateClickHouseLabel: string | null
  privateS3Label: string | null
  privateDataplaneSeenAt: Timestamp | null
  maxSessionDurationDays: Generated<number>
  mfaRequired: Generated<boolean>
  lockoutAfterFailedAttempts: Generated<number>
  lockoutMinutes: Generated<number>
  sessionIdleTimeoutMinutes: Generated<number>
  sessionMaxLifetimeMinutes: Generated<number>
  signupData: unknown
  signedDPA: Generated<boolean>
  elasticsearchNodeUrl: string | null
  elasticsearchApiKey: string | null
  useCustomElasticsearch: Generated<boolean>
  s3Endpoint: string | null
  s3AccessKeyId: string | null
  s3SecretAccessKey: string | null
  s3Bucket: string | null
  useCustomS3: Generated<boolean>
  sentPlanLimitAlert: Timestamp | null
  ssoDomain: string | null
  ssoProvider: string | null
  domainJoin: Generated<string>
  joinerRole: Generated<OrganizationUserRole>
  joinDomains: string[]
  presenceEnabled: Generated<boolean>
  traceSharingEnabled: Generated<boolean>
  supportContact: string | null
  primaryIntent: OrganizationIntent | null
  promoCode: string | null
  stripeCustomerId: string | null
  currency: Generated<Currency>
  pricingModel: Generated<PricingModel>
  license: string | null
  licenseExpiresAt: Timestamp | null
  licenseLastValidatedAt: Timestamp | null
  selfHostedCustomer: Generated<boolean>
  connectServicesDisabled: Generated<string[]>
  instantEvalsEnabledAt: Timestamp | null
  instantEvalsEnabledByUserId: string | null
  connectLastSyncAt: Timestamp | null
  connectLastSyncError: string | null
}

export interface IssuedLicense {
  id: string
  licenseId: string
  tokenHash: string
  organizationId: string | null
  organizationName: string
  email: string
  planType: string
  maxMembers: number
  maxMembersLite: number | null
  liteSeatsUnlimited: Generated<boolean>
  maxMembersDeveloper: number | null
  issuedAt: Timestamp
  expiresAt: Timestamp
  source: IssuedLicenseSource
  issuedById: string | null
  revokedAt: Timestamp | null
  revokedById: string | null
  revokedReason: string | null
  supersededAt: Timestamp | null
  replacesId: string | null
  pendingDeliveryLicense: string | null
  services: Generated<string[]>
  seatRateCents: number | null
  seatCurrency: Currency | null
  commitUsdCents: Generated<number>
  overageEnabled: Generated<boolean>
  overageMaxUsdCents: number | null
  instanceId: string | null
  instanceBoundAt: Timestamp | null
  lastSyncAt: Timestamp | null
  lastSyncVersion: string | null
  reportedMembers: number | null
  reportedMembersLite: number | null
  virtualKeyId: string | null
  seatsRaisedFrom: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationLicense {
  organizationId: string
  licenseKey: string | null
  expiresAt: Timestamp | null
  validatedAt: Timestamp | null
  updatedAt: Generated<Timestamp>
}

export interface ActivationCode {
  id: string
  codeHash: string
  codeHint: string
  organizationId: string
  organizationName: string
  email: string
  planType: string
  maxMembers: number
  maxMembersLite: Generated<number | null>
  liteSeatsUnlimited: Generated<boolean>
  maxMembersDeveloper: number | null
  licenseTermDays: number
  services: Generated<string[]>
  expiresAt: Timestamp
  reusable: Generated<boolean>
  redeemedAt: Timestamp | null
  redeemedByInstanceId: string | null
  issuedLicenseId: string | null
  redemptionCount: Generated<number>
  revokedAt: Timestamp | null
  revokedById: string | null
  createdById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ConnectedBillingAccount {
  id: string
  organizationId: string
  stripeCustomerId: string
  usageSubscriptionId: string | null
  usageSubscriptionItemId: string | null
  termStartsAt: Timestamp
  termEndsAt: Timestamp
  commitUsdCents: Generated<number>
  seatCurrency: Currency
  seatRateCents: number
  seats: number
  bankTransferType: string | null
  bankTransferCountry: string | null
  billingEmail: string
  pendingRenewal: unknown
  createdById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ConnectedCreditGrant {
  id: string
  accountId: string
  stripeCreditGrantId: string
  amountUsdCents: number
  kind: ConnectedCreditGrantKind
  termEndsAt: Timestamp
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface ConnectedInvoice {
  id: string
  accountId: string
  stripeInvoiceId: string
  kind: ConnectedInvoiceKind
  currency: Currency
  amountCents: number
  status: string
  paidOutOfBandAt: Timestamp | null
  termStartsAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ScimRequestLog {
  id: string
  organizationId: string
  connectionId: string | null
  method: string
  resource: string
  status: number
  reason: string | null
  detail: string | null
  occurredAt: Generated<Timestamp>
}

export interface ScimToken {
  id: string
  organizationId: string
  connectionId: string | null
  hashedToken: string
  hashScheme: Generated<string>
  description: string | null
  createdAt: Generated<Timestamp>
  lastUsedAt: Timestamp | null
}

export interface ScimExternalId {
  organizationId: string
  id: string
  connectionId: string
  externalId: string
  userId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ScimUserResource {
  organizationId: string
  userId: string
  userName: string
  name: string | null
  active: Generated<boolean>
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ScimDirectoryUser {
  organizationId: string
  connectionId: string
  userId: string
}

export interface ScimSyncState {
  id: string
  connectionId: string
  organizationId: string
  state: string
  lastPushedAt: Timestamp | null
  lastFailure: unknown
  deadLetters: Generated<unknown>
  revokedCause: string | null
  occurredAt: Timestamp
  lastEventId: string
  acceptedAt: Timestamp
  projectionVersion: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ScimSsoConnectionView {
  id: string
  organizationId: string
  folded: unknown
  appliedEventIds: Generated<string[]>
  projectionVersion: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface OrganizationSeatPeak {
  organizationId: string
  openedAt: number
  fullSeats: number
  liteSeats: number
  developerSeats: number
  latestChangeAt: number
  mismatches: number
}

export interface OrganizationSeatPeakHolder {
  organizationId: string
  holderKind: string
  holderId: string
  seat: string
  lastChangedAt: number
  lastChangeId: string | null
  expiresAt: number | null
  expiredByFold: Generated<boolean>
  suppressedBy: string | null
  restoredByFold: Generated<boolean>
}

export interface OrganizationSeatPeakMonth {
  organizationId: string
  month: string
  fullPeak: number
  fullPeakAt: number
  litePeak: number
  litePeakAt: number
  fullAtEnd: number
  liteAtEnd: number
}

export interface Project {
  id: string
  name: string
  slug: string
  apiKey: string
  legacyApiKeyLastUsedAt: Timestamp | null
  lwqlKey: Generated<string>
  teamId: string
  language: string
  framework: string
  kind: Generated<string>
  aggregateRule: unknown
  firstMessage: Generated<boolean>
  integrated: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userLinkTemplate: string | null
  traceSharingEnabled: Generated<boolean>
  presenceEnabled: Generated<boolean>
  s3Endpoint: string | null
  s3AccessKeyId: string | null
  s3SecretAccessKey: string | null
  s3Bucket: string | null
  archivedAt: Timestamp | null
  isPersonal: Generated<boolean>
  ownerUserId: string | null
  personalFeatures: Generated<unknown>
  departmentId: string | null
  langyEgressAllowlist: unknown
  lastCodingAgentSessionAt: Timestamp | null
  lastCodingAgentPullRequestAt: Timestamp | null
}

export interface Department {
  id: string
  organizationId: string
  name: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  archivedAt: Timestamp | null
}

export interface DepartmentMembershipHistory {
  id: string
  organizationId: string
  userId: string
  departmentId: string
  validFrom: Timestamp
  validTo: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface PlatformToolPolicy {
  id: string
  organizationId: string
  toolSlug: string
  allowVk: Generated<boolean>
  allowOtelDirect: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface OrganizationInvite {
  id: string
  email: string
  inviteCode: string
  expiration: Timestamp | null
  status: Generated<INVITE_STATUS>
  organizationId: string
  teamIds: string
  teamAssignments: unknown
  role: OrganizationUserRole
  requestedBy: string | null
  subscriptionId: string | null
  acceptedByUserId: string | null
  acceptedViaIdentifierId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Monitor {
  id: string
  projectId: string
  experimentId: string | null
  evaluatorId: string | null
  checkType: string
  name: string
  slug: string
  executionMode: Generated<EvaluationExecutionMode>
  enabled: Generated<boolean>
  preconditions: unknown
  parameters: unknown
  mappings: unknown
  sample: Generated<number>
  level: Generated<string>
  threadIdleTimeout: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  autoPausedAt: Timestamp | null
  autoPausedReason: string | null
  evaluatorLabel: string | null
  createdById: string | null
  updatedById: string | null
}

export interface Cost {
  id: string
  projectId: string
  costType: CostType
  costName: string | null
  referenceType: CostReferenceType
  referenceId: string
  amount: number
  currency: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  extraInfo: unknown
}

export interface Topic {
  id: string
  projectId: string
  name: string
  parentId: string | null
  embeddings_model: string
  centroid: unknown
  p95Distance: number
  automaticallyGenerated: Generated<boolean>
  lastEventId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Dataset {
  id: string
  projectId: string
  name: string
  slug: string
  columnTypes: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  archivedAt: Timestamp | null
  mapping: unknown
  useS3: Generated<boolean>
  s3RecordCount: number | null
  contentLayout: Generated<string>
  status: Generated<string>
  statusError: string | null
  stagingKey: string | null
  sourceStoredObjectId: string | null
  uploadFilename: string | null
  rowCount: number | null
  sizeBytes: bigint | null
  chunkCount: number | null
  chunkOffsets: unknown
}

export interface DatasetRecord {
  id: string
  datasetId: string
  projectId: string
  entry: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DatasetVersion {
  id: string
  projectId: string
  datasetId: string
  version: number
  name: string | null
  trigger: string
  contentHash: string
  columnTypes: unknown
  rowCount: number
  sizeBytes: bigint
  chunkCount: number
  restoredFromVersion: number | null
  authorId: string | null
  authorKind: string | null
  pinnedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface DatasetVersionRun {
  id: string
  projectId: string
  datasetId: string
  versionId: string
  runId: string
  experimentId: string | null
  createdAt: Generated<Timestamp>
}

export interface Dashboard {
  id: string
  projectId: string
  name: string
  order: Generated<number>
  description: string | null
  createdById: string | null
  scope: Generated<DashboardScope>
  organizationId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DashboardFavourite {
  id: string
  userId: string
  dashboardId: string | null
  templateId: string | null
  projectId: string
  position: number
  createdAt: Generated<Timestamp>
}

export interface DashboardsV2 {
  id: string
  projectId: string
  name: string
  description: string | null
  createdById: string | null
  scope: Generated<DashboardScope>
  organizationId: string | null
  excludedOrigins: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DashboardWidget {
  id: string
  projectId: string
  dashboardId: string | null
  name: string
  definition: unknown
  gridColumn: Generated<number>
  gridRow: Generated<number>
  colSpan: Generated<number>
  rowSpan: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface InsightProjection {
  id: string
  projectId: string
  title: string
  body: string
  tone: string
  topic: string | null
  validDays: number
  lwql: string | null
  sourceConversationId: string | null
  sourceMessageId: string | null
  filedByUserId: string | null
  filedAt: number
  renewedAt: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
  boardId: string | null
  boardName: string | null
  widgetId: string | null
  widgetName: string | null
  filedVia: Generated<string>
  replayStart: number | null
  replayEnd: number | null
  replayGranularitySeconds: number | null
  replayContext: unknown
  ownerUserId: string | null
  findingKey: string | null
}

export interface InsightReaderProjection {
  id: string
  projectId: string
  insightId: string
  userId: string
  seenAt: number | null
  archivedAt: number | null
  keptAt: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface InsightDailyScheduleProjection {
  id: string
  projectId: string
  userId: string
  boardKind: string
  boardId: string
  boardName: string
  state: Generated<string>
  offReason: string | null
  hour: number | null
  timezone: string | null
  maxInsights: number | null
  lastRunId: string | null
  lastRunAt: number | null
  lastRunOutcome: string | null
  lastRunReason: string | null
  lastRunFiled: number | null
  lastRunRenewed: number | null
  lastRunConversationId: string | null
  consecutiveSkips: Generated<number>
  skipStreakSlot: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface SavedView {
  id: string
  projectId: string
  userId: string | null
  name: string
  filters: Generated<unknown>
  query: string | null
  period: unknown
  order: Generated<number>
  kind: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface CustomGraph {
  id: string
  projectId: string
  name: string
  graph: unknown
  filters: unknown
  kind: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  dashboardId: string | null
  gridColumn: Generated<number>
  gridRow: Generated<number>
  colSpan: Generated<number>
  rowSpan: Generated<number>
}

export interface BatchEvaluation {
  id: string
  experimentId: string
  projectId: string
  data: Generated<unknown>
  status: string
  score: number
  label: string | null
  passed: boolean
  details: string
  cost: number
  datasetSlug: string
  datasetId: string
  evaluation: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Trigger {
  id: string
  name: string
  projectId: string
  action: TriggerAction
  triggerKind: Generated<TriggerKind>
  actionParams: unknown
  filters: unknown
  filterQuery: string | null
  lastRunAt: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  active: Generated<boolean>
  pausedReason: string | null
  pausedAt: Timestamp | null
  message: string | null
  deleted: Generated<boolean>
  alertType: AlertType | null
  slackTemplateType: string | null
  slackTemplate: string | null
  emailSubjectTemplate: string | null
  emailBodyTemplate: string | null
  notificationCadence: Generated<string>
  traceDebounceMs: Generated<number>
  customGraphId: string | null
}

export interface SlackIntegration {
  id: string
  name: string
  kind: SlackIntegrationKind
  scopeType: SlackIntegrationScopeType
  scopeId: string
  organizationId: string
  botTokenEncrypted: string | null
  webhookUrlEncrypted: string | null
  secretFingerprint: string
  secretHint: string
  slackTeamId: string | null
  slackTeamName: string | null
  createdById: string
  updatedById: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SlackConnectionClaim {
  connectionId: string
  claimantId: string
  claimantLabel: string
  organizationId: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface WebhookEndpoint {
  id: string
  organizationId: string
  destinationKind: Generated<WebhookDestinationKind>
  url: string | null
  sqsQueueUrl: string | null
  sqsRoleArn: string | null
  sqsExternalId: string | null
  sqsAccessKeyId: string | null
  sqsSecretAccessKeyEncrypted: string | null
  secretEncrypted: string
  signatureScheme: string | null
  idempotencyKey: string | null
  previousSecretEncrypted: string | null
  previousSecretExpiresAt: Timestamp | null
  enabledEvents: Generated<string[]>
  status: Generated<WebhookEndpointStatus>
  disabledReason: string | null
  disabledAt: Timestamp | null
  failingSince: Timestamp | null
  maxBatchSize: Generated<number>
  maxBatchDelayMs: Generated<number>
  maxInFlight: Generated<number>
  allowSelfSignedCertificate: Generated<boolean>
  lastSuccessAt: Timestamp | null
  lastFailureAt: Timestamp | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface WebhookEndpointDelivery {
  id: string
  channel: Generated<WebhookDeliveryChannel>
  organizationId: string | null
  endpointId: string | null
  projectId: string | null
  triggerId: string | null
  dispatchId: string
  attempt: number | null
  eventCount: number | null
  responseStatus: number | null
  latencyMs: number | null
  error: string | null
  response: unknown
  outcome: WebhookDeliveryOutcome
  firedAt: Generated<Timestamp>
  createdAt: Generated<Timestamp>
}

export interface Experiment {
  id: string
  name: string | null
  type: ExperimentType
  slug: string
  projectId: string
  workflowId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  archivedAt: Timestamp | null
  workbenchState: unknown
  workbenchVersion: Generated<number>
}

export interface ExperimentVersion {
  id: string
  experimentId: string
  projectId: string
  version: number
  counterVersion: number
  autoSaved: Generated<boolean>
  commitMessage: string | null
  authorId: string | null
  authorLabel: string
  runId: string | null
  state: unknown
  schemaVersion: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Annotation {
  id: string
  projectId: string
  traceId: string
  comment: string | null
  isThumbsUp: boolean | null
  userId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  email: string | null
  scoreOptions: unknown
  expectedOutput: string | null
  anchorKind: string | null
  anchorId: string | null
  anchorPath: string | null
}

export interface ModelProvider {
  id: string
  name: string
  provider: string
  routingHandle: string | null
  enabled: boolean
  customKeys: unknown
  extraHeaders: unknown
  customModels: unknown
  customEmbeddingsModels: unknown
  deploymentMapping: unknown
  rateLimitRpm: number | null
  rateLimitTpm: number | null
  rateLimitRpd: number | null
  rotationPolicy: Generated<GatewayProviderRotationPolicy>
  providerConfig: unknown
  fallbackPriorityGlobal: number | null
  langySkipPermissionsModels: unknown
  healthStatus: Generated<GatewayProviderHealthStatus>
  circuitOpenedAt: Timestamp | null
  lastHealthCheckAt: Timestamp | null
  disabledAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  organizationId: string
}

export interface ModelProviderScope {
  id: string
  modelProviderId: string
  scopeType: ModelProviderScopeType
  scopeId: string
  createdAt: Generated<Timestamp>
}

export interface ModelDefaultConfig {
  id: string
  config: unknown
  authorId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organizationId: string
}

export interface ModelDefaultConfigScope {
  id: string
  configId: string
  scopeType: ModelDefaultScopeType
  scopeId: string
  createdAt: Generated<Timestamp>
}

export interface GithubInstallation {
  id: string
  installationId: string
  organizationId: string
  accountLogin: string
  accountType: string
  accountId: string
  repositorySelection: string
  repositories: unknown
  suspendedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface GithubPullRequest {
  id: string
  organizationId: string
  repositoryHost: string
  repositoryFullName: string
  headBranch: string
  prNumber: number
  htmlUrl: string
  title: string
  state: string
  isDraft: Generated<boolean>
  authorLogin: string | null
  prCreatedAt: Timestamp
  prClosedAt: Timestamp | null
  prMergedAt: Timestamp | null
  prUpdatedAt: Timestamp | null
  mappedAt: Generated<Timestamp>
  lastCheckedAt: Generated<Timestamp>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface GithubBranchPullRequestCheck {
  id: string
  organizationId: string
  repositoryHost: string
  repositoryFullName: string
  headBranch: string
  lastCheckedAt: Timestamp
  prCount: Generated<number>
  notFoundAt: Timestamp | null
  recheckAfter: Timestamp | null
  attempts: Generated<number>
  lastRequestedAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface LangyConversationProjection {
  id: string
  projectId: string
  conversationId: string
  userId: string
  title: string | null
  titleSource: LangyProjectionTitleSource
  origin: Generated<string>
  status: string
  isShared: Generated<boolean>
  sharedAt: number | null
  sharedById: string | null
  messageCount: Generated<number>
  lastActivityAt: number | null
  currentTurnId: string | null
  lastError: string | null
  lastModel: string | null
  pendingHandoffToken: string | null
  pendingHandoffTurnId: string | null
  runToken: string | null
  archivedAt: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface LangyTurnRequest {
  id: string
  projectId: string
  userId: string
  requestId: string
  conversationId: string
  turnId: string
  status: string
  leaseOwner: string
  leaseExpiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface LangyActiveTurn {
  id: string
  projectId: string
  conversationId: string
  turnId: string
  requestId: string
  userId: string
  status: string
  leaseOwner: string
  leaseExpiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface LangyConversationTurnProjection {
  id: string
  projectId: string
  conversationId: string
  turnId: string
  status: string
  questionParts: unknown
  answerParts: unknown
  toolCalls: unknown
  plan: unknown
  error: string | null
  startedAt: number | null
  endedAt: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface LangyMessageProjection {
  id: string
  projectId: string
  conversationId: string
  messageId: string
  role: string
  parts: unknown
  sourceEventId: string
  occurredAt: number
  acceptedAt: number
  createdAt: number
  updatedAt: number
}

export interface ProjectSecret {
  id: string
  projectId: string
  name: string
  encryptedValue: string
  createdById: string
  updatedById: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TriggerSent {
  id: string
  triggerId: string
  traceId: string | null
  customGraphId: string | null
  projectId: string
  resolvedAt: Timestamp | null
  openIncidentKey: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TriggerLatestEvaluation {
  triggerId: string
  projectId: string
  evaluatedAt: Timestamp
  verdict: string
  observedValue: number | null
  threshold: number | null
  operator: string | null
  timePeriodMinutes: number | null
  skipCode: string | null
  updatedAt: Timestamp
}

export interface EmailSuppression {
  id: string
  projectId: string
  email: string
  triggerId: string | null
  reason: Generated<string>
  createdAt: Generated<Timestamp>
}

export interface AnnotationScore {
  id: string
  name: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  deletedAt: Timestamp | null
  description: string | null
  active: Generated<boolean>
  dataType: AnnotationScoreDataType | null
  options: unknown
  defaultValue: unknown
  global: Generated<boolean>
}

export interface AnnotationQueue {
  id: string
  name: string
  slug: string
  projectId: string
  description: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface AnnotationQueueMembers {
  annotationQueueId: string
  userId: string
}

export interface AnnotationQueueScores {
  annotationQueueId: string
  annotationScoreId: string
}

export interface AnnotationQueueItem {
  id: string
  annotationQueueId: string | null
  userId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdByUserId: string | null
  traceId: string
  projectId: string
  doneAt: Timestamp | null
  markedForDatasetAt: Timestamp | null
}

export interface ShareLink {
  id: string
  token: string
  resourceType: ShareResourceType
  resourceId: string
  threadId: string | null
  projectId: string
  userId: string | null
  visibility: Generated<ShareVisibility>
  expiresAt: Timestamp | null
  maxViews: number | null
  viewCount: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface PinnedTrace {
  id: string
  projectId: string
  traceId: string
  userId: string | null
  source: Generated<PinSource>
  reason: string | null
  createdAt: Generated<Timestamp>
}

export interface TraceEditOverlay {
  id: string
  projectId: string
  traceId: string
  patch: unknown
  createdById: string | null
  updatedById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TraceIngestSourceBilling {
  organizationId: string
  sourceType: string
  billed: boolean
  recordedAt: Timestamp
  updatedAt: Timestamp
}

export interface RetentionPolicy {
  id: string
  organizationId: string
  scopeType: RetentionPolicyScopeType
  scopeId: string
  category: string
  retentionDays: number
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DataPrivacyPolicy {
  id: string
  organizationId: string
  scopeType: DataPrivacyScopeType
  scopeId: string
  personalOnly: Generated<boolean>
  config: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface InstantEvalJudgeProject {
  projectId: string
  organizationId: string
  createdAt: Timestamp
}

export interface InstantEvalJudgeUsageBilling {
  organizationId: string
  usageBilled: boolean
  occurredAt: Timestamp
  fromCatchUp: boolean
}

export interface InstantEvalJudgeSpend {
  organizationId: string
  requestId: string
  spendNanoUsd: bigint
  occurredAt: Timestamp
}

export interface CustomLLMModelCost {
  id: string
  organizationId: string
  scopeType: Generated<CustomLLMModelCostScopeType>
  scopeId: string
  projectId: string | null
  model: string
  regex: string
  inputCostPerToken: number | null
  outputCostPerToken: number | null
  cacheReadCostPerToken: number | null
  cacheCreationCostPerToken: number | null
  cacheCreation1hCostPerToken: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Workflow {
  id: string
  projectId: string
  name: string
  icon: string
  description: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  latestVersionId: string | null
  currentVersionId: string | null
  publishedId: string | null
  publishedById: string | null
  archivedAt: Timestamp | null
  isEvaluator: Generated<boolean>
  isComponent: Generated<boolean>
  copiedFromWorkflowId: string | null
}

export interface WorkflowVersion {
  id: string
  version: string
  commitMessage: string
  authorId: string
  projectId: string
  workflowId: string
  parentId: string | null
  autoSaved: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  dsl: unknown
}

export interface AuditLog {
  id: string
  createdAt: Generated<Timestamp>
  userId: string | null
  projectId: string | null
  organizationId: string | null
  action: string
  args: unknown
  error: string | null
  ipAddress: string | null
  userAgent: string | null
  metadata: unknown
  actorUserId: string | null
  targetKind: string | null
  targetId: string | null
  before: unknown
  after: unknown
  idempotencyKey: string | null
}

export interface LlmPromptConfig {
  id: string
  handle: string | null
  name: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  deletedAt: Timestamp | null
  organizationId: string
  scope: Generated<PromptScope>
  copiedFromPromptId: string | null
}

export interface LlmPromptConfigVersion {
  id: string
  version: number
  commitMessage: string | null
  authorId: string | null
  configId: string
  config: unknown
  schemaVersion: string
  runtimeParameters: Generated<unknown>
  createdAt: Generated<Timestamp>
  projectId: string
}

export interface PromptTagAssignment {
  id: string
  configId: string
  versionId: string
  tagId: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface PromptTag {
  id: string
  organizationId: string
  name: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface Analytics {
  id: string
  projectId: string
  key: string
  value: unknown
  numericValue: number | null
  stringValue: string | null
  boolValue: boolean | null
  createdAt: Generated<Timestamp>
}

export interface CustomRole {
  id: string
  organizationId: string
  name: string
  description: string | null
  permissions: unknown
  kind: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Notification {
  id: string
  organizationId: string | null
  projectId: string | null
  metadata: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  sentAt: Timestamp
}

export interface Agent {
  id: string
  projectId: string
  name: string
  type: string
  config: unknown
  workflowId: string | null
  copiedFromAgentId: string | null
  environment: string | null
  ownerUserId: string | null
  hostLabel: string | null
  identityKey: string | null
  lastSeenAt: Timestamp | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface AgentVersion {
  id: string
  projectId: string
  agentId: string
  version: number
  name: string
  type: string
  config: unknown
  workflowId: string | null
  commitMessage: string | null
  authorId: string | null
  authorKind: string | null
  createdAt: Generated<Timestamp>
}

export interface AgentVersionLabel {
  id: string
  projectId: string
  agentId: string
  label: string
  versionId: string
  createdById: string | null
  updatedById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Evaluator {
  id: string
  projectId: string
  name: string
  slug: string | null
  type: string
  config: unknown
  workflowId: string | null
  copiedFromEvaluatorId: string | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface EvaluatorVersion {
  id: string
  projectId: string
  evaluatorId: string
  version: number
  name: string
  type: string
  config: unknown
  workflowId: string | null
  commitMessage: string | null
  authorId: string | null
  authorKind: string | null
  createdAt: Generated<Timestamp>
}

export interface EvaluatorVersionLabel {
  id: string
  projectId: string
  evaluatorId: string
  label: string
  versionId: string
  createdById: string | null
  updatedById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Scenario {
  id: string
  projectId: string
  name: string
  situation: string
  criteria: string[]
  labels: string[]
  parameters: unknown
  simulatorModel: string | null
  judgeModel: string | null
  maxTurns: number | null
  minTurns: number | null
  fields: unknown
  callerVoice: unknown
  testSuiteId: string | null
  version: Generated<number>
  lastUpdatedById: string | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ScenarioVersion {
  id: string
  scenarioId: string
  projectId: string
  version: number
  authorId: string | null
  authorLabel: string
  changeDescription: string | null
  snapshot: unknown
  schemaVersion: Generated<number>
  createdAt: Generated<Timestamp>
}

export interface SimulationSuite {
  id: string
  projectId: string
  name: string
  slug: string
  description: string | null
  scenarioIds: string[]
  targets: unknown
  repeatCount: Generated<number>
  labels: string[]
  simulatorModel: string | null
  judgeModel: string | null
  kind: Generated<string>
  scope: unknown
  fields: unknown
  evaluators: unknown
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Subscription {
  id: string
  organizationId: string
  plan: PlanTypes
  stripeSubscriptionId: string | null
  status: SubscriptionStatus
  startDate: Timestamp | null
  endDate: Timestamp | null
  lastPaymentFailedDate: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  maxMembers: number | null
  maxProjects: number | null
  maxMessagesPerMonth: number | null
  maxWorkflows: number | null
  maxRetentionDays: number | null
  maxMembersLite: number | null
  maxMembersDeveloper: number | null
  maxTeams: number | null
  maxPrompts: number | null
  maxEvaluators: number | null
  maxScenarios: number | null
  maxAgents: number | null
  maxExperiments: number | null
  maxOnlineEvaluations: number | null
  maxDatasets: number | null
  maxDashboards: number | null
  maxCustomGraphs: number | null
  maxAutomations: number | null
  seatsKept: number | null
  seatsKeptUntil: Timestamp | null
  seatLineQuantity: number | null
  seatLineQuantityAt: Timestamp | null
  seatLineWriteQuantities: Generated<number[]>
  seatLineWriteSince: Timestamp | null
  seatLineWriteAt: Timestamp | null
  seatLineWriteAttempts: number | null
  seatLineWriteFlaggedAt: Timestamp | null
  seatRepairQuantity: number | null
  seatRepairLiftedQuantity: number | null
  seatRepairSince: Timestamp | null
  seatRepairAttempts: number | null
  seatRepairFlaggedAt: Timestamp | null
  seatLineLeaseToken: string | null
  seatLineLeaseUntil: Timestamp | null
}

export interface Invoice {
  id: string
  subscriptionId: string
  totalAmount: number
  currency: string
  dueDate: Timestamp
  paid: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface InvoiceItem {
  id: string
  invoiceId: string
  description: string
  amount: number
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Group {
  id: string
  organizationId: string
  name: string
  slug: string
  externalId: string | null
  scimSource: string | null
  scimConnectionId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface GroupMembership {
  userId: string
  groupId: string
  createdAt: Generated<Timestamp>
}

export interface AuthzUserStanding {
  userId: string
  deactivatedAt: Timestamp | null
  erasedAt: Timestamp | null
  standingChangedAt: Timestamp
}

export interface AuthzEngineSwitch {
  organizationId: string
  onEngine: boolean
  pinned: Generated<boolean>
  switchedAt: Timestamp
  lastAppliedEventId: string | null
  updatedAt: Generated<Timestamp>
}

export interface AuthzOrganizationPolicy {
  organizationId: string
  braidsOrganizationAdminsOnly: Generated<boolean>
  updatedByUserId: string
  updatedAt: Timestamp
}

export interface RoleBinding {
  id: string
  organizationId: string
  userId: string | null
  groupId: string | null
  apiKeyId: string | null
  role: TeamUserRole
  customRoleId: string | null
  scopeType: RoleBindingScopeType
  scopeId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Grant {
  id: string
  organizationId: string
  principalType: GrantPrincipalType
  principalId: string | null
  roleKey: string | null
  legacyRole: string | null
  source: string
  scopeType: GrantScopeType
  scopeId: string
  token: string | null
  permission: string | null
  resourceKind: string | null
  projectId: string | null
  createdByUserId: string | null
  expiresAt: Timestamp | null
  maxViews: number | null
  condition: unknown
  replacesGrantId: string | null
  occurredAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  revokedAt: Timestamp | null
  revokedReason: string | null
}

export interface GrantUsage {
  grantId: string
  organizationId: string
  projectId: string
  viewCount: Generated<number>
  lastViewedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Role {
  id: string
  organizationId: string
  name: string
  description: string | null
  permissions: unknown
  kind: Generated<string>
  occurredAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  deletedAt: Timestamp | null
}

export interface ApiKey {
  id: string
  name: string
  description: string | null
  lookupId: string
  hashedSecret: string
  permissionMode: Generated<string>
  userId: string | null
  createdByUserId: string | null
  organizationId: string
  expiresAt: Timestamp | null
  lastUsedAt: Timestamp | null
  revokedAt: Timestamp | null
  revocationCause: string | null
  parentApiKeyId: string | null
  ingestSourceType: string | null
  ingestionTemplateId: string | null
  createdByDeviceLabel: string | null
  isSystemManaged: Generated<boolean>
  createdByBrowserSessionId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SigningKey {
  id: string
  organizationId: string
  projectId: string
  name: string
  publicId: string
  secretCiphertext: string
  createdByUserId: string
  createdAt: Generated<Timestamp>
  rotatedAt: Timestamp | null
  verifiesUntil: Timestamp | null
  revokedAt: Timestamp | null
  lastUsedAt: Timestamp | null
}

export interface ProjectActiveDay {
  id: string
  projectId: string
  day: string
  createdAt: Generated<Timestamp>
}

export interface NurturingProject {
  projectId: string
  organizationId: string
}

export interface NurturingUncopiedProject {
  projectId: string
  firstSeenAt: Generated<Timestamp>
}

export interface NurturingOrganizationMember {
  organizationId: string
  userId: string
  joinedAt: Timestamp | null
  leftAt: Timestamp | null
  returnedAt: Timestamp | null
}

export interface NurturingOrganizationCreated {
  organizationId: string
  createdAt: Timestamp
}

export interface NurturingOrganization {
  organizationId: string
  adminUserId: string | null
  seeded: Generated<boolean>
  evaluationCount: Generated<number>
  simulationRunCount: Generated<number>
  codingFirstSessionAt: Timestamp | null
  codingAgentTypes: Generated<string[]>
  codingActiveDays: Generated<number>
  codingLastActiveDay: string | null
  codingTerminalTypes: Generated<string[]>
  codingRepositoryKeys: Generated<string[]>
  codingFirstDays: Generated<number[]>
  lastTraceAt: Timestamp | null
  updatedAt: Timestamp
}

export interface NurturingProjectUsageView {
  projectId: string
  owner: string
  folded: unknown
  appliedEventIds: Generated<string[]>
  projectionVersion: string
  updatedAt: Timestamp
}

export interface NurturingProjectTraceDay {
  projectId: string
  day: string
  traceCount: Generated<number>
  errorCount: Generated<number>
  lastTraceAt: Timestamp | null
  lastFailedTraceAt: Timestamp | null
}

export interface NurturingPersonalProject {
  projectId: string
  organizationId: string
  ownerUserId: string
}

export interface NurturingTracedProject {
  projectId: string
  organizationId: string
}

export interface BillingMeterCheckpoint {
  id: string
  organizationId: string
  billingMonth: string
  meter: Generated<string>
  lastReportedTotal: Generated<number>
  pendingReportedTotal: number | null
  consecutiveFailures: Generated<number>
  lastCountedEventId: string | null
  updatedAt: Timestamp
}

export interface VirtualKey {
  id: string
  organizationId: string
  name: string
  description: string | null
  status: Generated<VirtualKeyStatus>
  purpose: Generated<VirtualKeyPurpose>
  externalId: string | null
  metadata: Generated<unknown>
  disabledAt: Timestamp | null
  disabledReason: string | null
  expiresAt: Timestamp | null
  hashedSecret: string
  displayPrefix: string
  principalUserId: string | null
  traceProjectId: string | null
  config: Generated<unknown>
  revision: Generated<bigint>
  previousHashedSecret: string | null
  previousSecretValidUntil: Timestamp | null
  revokedAt: Timestamp | null
  revokedById: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string
  lastUsedAt: Timestamp | null
  routingPolicyId: string | null
  routingMode: Generated<VirtualKeyRoutingMode>
  connectServices: Generated<string[]>
  licenseTokenHash: string | null
  licenseInstanceId: string | null
  licenseExpiresAt: Timestamp | null
  licenseId: string | null
}

export interface VirtualKeyScope {
  id: string
  virtualKeyId: string
  scopeType: VirtualKeyScopeType
  scopeId: string
  createdAt: Generated<Timestamp>
}

export interface RoutingPolicy {
  id: string
  organizationId: string
  name: string
  description: string | null
  modelProviderIds: Generated<unknown>
  modelAliases: Generated<unknown>
  defaultModel: string | null
  policyRules: Generated<unknown>
  isDefault: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface RoutingPolicyScope {
  id: string
  routingPolicyId: string
  scopeType: RoutingPolicyScopeType
  scopeId: string
  createdAt: Generated<Timestamp>
}

export interface GatewayGuardrail {
  id: string
  projectId: string
  name: string
  description: string | null
  evaluatorId: string
  direction: GatewayGuardrailDirection
  failureMode: Generated<GatewayGuardrailFailureMode>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface IngestionSource {
  id: string
  organizationId: string
  teamId: string | null
  sourceType: string
  name: string
  description: string | null
  ingestSecretHash: string
  parserConfig: Generated<unknown>
  pollerCursor: unknown
  errorCount: Generated<number>
  pullSchedule: string | null
  status: Generated<string>
  traceProjectId: string | null
  providerAccountId: string | null
  lastEventAt: Timestamp | null
  lastSuccessAt: Timestamp | null
  lastReadThroughAt: Timestamp | null
  lastRunCompleteness: string | null
  unpricedUsageSince: Timestamp | null
  unpricedUsageThrough: Timestamp | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
}

export interface AnomalyRule {
  id: string
  organizationId: string
  scope: string
  scopeId: string
  name: string
  description: string | null
  severity: string
  ruleType: string
  thresholdConfig: Generated<unknown>
  destinationConfig: Generated<unknown>
  status: Generated<string>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
}

export interface AnomalyAlert {
  id: string
  organizationId: string
  ruleId: string
  severity: string
  ruleName: string
  ruleType: string
  triggerWindowStart: Timestamp
  triggerWindowEnd: Timestamp
  triggerSpendUsd: string | null
  triggerEventCount: number | null
  detail: Generated<unknown>
  state: Generated<string>
  destinationStatus: Generated<unknown>
  detectedAt: Generated<Timestamp>
  acknowledgedAt: Timestamp | null
  resolvedAt: Timestamp | null
}

export interface AiToolEntry {
  id: string
  organizationId: string
  scope: string
  scopeId: string
  type: string
  displayName: string
  slug: string
  iconKey: string | null
  iconAsset: string | null
  order: Generated<number>
  enabled: Generated<boolean>
  config: Generated<unknown>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface AiToolEntryTeam {
  id: string
  entryId: string
  teamId: string
}

export interface AiToolEntryDepartment {
  id: string
  entryId: string
  departmentId: string
}

export interface IngestionTemplate {
  id: string
  organizationId: string | null
  slug: string
  sourceType: string
  displayName: string
  description: string | null
  iconAsset: string | null
  credentialSchema: string | null
  ottlRules: Generated<string>
  platformPublished: Generated<boolean>
  enabled: Generated<boolean>
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string | null
  updatedById: string | null
}

export interface GatewayConnectUpstream {
  organizationId: string
  baseUrl: string
  encryptedToken: string
  instanceId: string
  updatedAt: Timestamp
}

export interface GatewayTraceExportKey {
  projectId: string
  apiKeyId: string
  encryptedToken: string
  createdAt: Generated<Timestamp>
}

export interface GatewayBudget {
  id: string
  organizationId: string
  scopeType: GatewayBudgetScopeType
  scopeId: string
  providerKey: string | null
  name: string
  description: string | null
  window: GatewayBudgetWindow
  limitUsd: string
  onBreach: Generated<GatewayBudgetBreachAction>
  timezone: string | null
  externalId: string | null
  metadata: Generated<unknown>
  spentUsd: Generated<string>
  currentPeriodStartedAt: Generated<Timestamp>
  resetsAt: Timestamp
  lastResetAt: Timestamp | null
  cycleAnchorAt: Timestamp | null
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string
  managedByVirtualKeyId: string | null
}

export interface GatewayBudgetBucketBoundary {
  id: string
  organizationId: string
  budgetId: string
  bucketScopeId: string
  periodStartedAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface GatewayBudgetLedger {
  id: string
  budgetId: string
  virtualKeyId: string
  modelProviderId: string | null
  providerKey: string | null
  gatewayRequestId: string
  amountUsd: string
  tokensInput: Generated<number>
  tokensOutput: Generated<number>
  tokensCacheRead: Generated<number>
  tokensCacheWrite: Generated<number>
  model: string
  durationMs: number | null
  status: Generated<GatewayBudgetLedgerStatus>
  occurredAt: Generated<Timestamp>
}

export interface GatewayChangeEvent {
  revision: Generated<bigint>
  organizationId: string
  kind: GatewayChangeEventKind
  virtualKeyId: string | null
  budgetId: string | null
  modelProviderId: string | null
  projectId: string | null
  payload: unknown
  createdAt: Generated<Timestamp>
}

export interface GatewayCacheRule {
  id: string
  organizationId: string
  name: string
  description: string | null
  priority: Generated<number>
  enabled: Generated<boolean>
  matchers: unknown
  action: unknown
  modeEnum: GatewayCacheRuleMode
  archivedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  createdById: string
}

export interface FeatureFlag {
  key: string
  enabled: boolean
  rules: unknown
  lastEditedBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface FeatureFlagExperimentSetting {
  flagKey: string
  subjectType: FeatureFlagExperimentSubjectType
  subjectId: string
  enabled: boolean
  changedByUserId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ProcessManagerInstance {
  id: string
  processName: string
  projectId: string
  processKey: string
  tenantId: string
  userId: string | null
  state: unknown
  revision: number
  nextWakeAt: Timestamp | null
  updatedAt: Timestamp
}

export interface ProcessManagerInbox {
  id: string
  processName: string
  projectId: string
  processKey: string
  tenantId: string
  sourceEventId: string
  sourceEventKey: string | null
  consumedAt: Timestamp
}

export interface ProcessManagerOutbox {
  id: string
  processName: string
  projectId: string
  processKey: string
  tenantId: string
  userId: string | null
  messageKey: string
  intentType: string
  payload: unknown
  traceCarrier: unknown
  sourceEventId: string | null
  status: Generated<ProcessManagerOutboxStatus>
  attempts: Generated<number>
  nextAttemptAt: Timestamp
  leasedUntil: Timestamp | null
  leaseToken: string | null
  dispatchedAt: Timestamp | null
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ProcessManagerOutboxAttempt {
  id: string
  outboxId: string
  projectId: string
  attempt: number
  occurredAt: Timestamp
  outcome: ProcessManagerOutboxAttemptOutcome
  errorType: string
  errorMessage: string
  retryAfterMs: number | null
}

export interface TopicClusteringRunProjection {
  id: string
  projectId: string
  lastRequestedAt: number | null
  lastRequestTrigger: string | null
  lastRunAt: number | null
  lastRunOutcome: string | null
  lastRunMode: string | null
  lastRunSkippedReason: string | null
  lastRunError: string | null
  lastRunErrorCode: string | null
  lastRunErrorUserActionable: Generated<boolean>
  lastRunTracesProcessed: Generated<number>
  lastRunTopicsCount: Generated<number>
  lastRunSubtopicsCount: Generated<number>
  lastRunPages: Generated<number>
  inProgressRunId: string | null
  inProgressTraces: Generated<number>
  inProgressPages: Generated<number>
  inProgressStartedAt: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface TopicModelProjection {
  id: string
  projectId: string
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface TopicClusteringRunHistoryProjection {
  id: string
  projectId: string
  runs: unknown
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface IngestionPullRunProjection {
  id: string
  sourceId: string
  projectId: string
  enabled: Generated<boolean>
  cron: string | null
  cursor: string | null
  lastRunAt: number | null
  lastRunOutcome: string | null
  lastRunEventCount: Generated<number>
  lastRunError: string | null
  lastRunErrorCode: string | null
  consecutiveErrors: Generated<number>
  lastSuccessAt: number | null
  lastRunScheduledFor: number | null
  lastReadThroughAt: number | null
  lastRunCompleteness: string | null
  lastAgentsListingAt: number | null
  lastAgentsListingOutcome: string | null
  lastAgentsListingCount: number | null
  lastAgentsListingReason: string | null
  lastAgentsListingStatus: number | null
  lastPeopleListingAt: number | null
  lastPeopleListingOutcome: string | null
  lastPeopleDirectoryCount: number | null
  lastPeopleWithheldCount: number | null
  lastPeopleListingReason: string | null
  lastPeopleListingStatus: number | null
  createdAt: number
  updatedAt: number
  occurredAt: number
  acceptedAt: number
  lastEventId: string
  projectionVersion: string
}

export interface ScheduledJob {
  id: string
  projectId: string
  targetType: string
  targetId: string
  cron: string
  timezone: string
  nextRunAt: Timestamp
  lastSlot: Timestamp | null
  currentSlot: Timestamp | null
  attempts: Generated<number>
  lastError: string | null
  active: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface BugReport {
  id: string
  createdAt: Generated<Timestamp>
  source: string
  kind: string
  title: string
  summary: string | null
  sessionData: string | null
  sessionTruncated: Generated<boolean>
  agent: string | null
  contactEmail: string | null
  cliVersion: string | null
  linkedProjectId: string | null
  metadata: unknown
}

export interface IdempotencyReceipt {
  id: string
  scopeId: string
  key: string
  claimId: string
  requestFingerprint: string
  responseStatus: number | null
  responseBody: string | null
  createdAt: Generated<Timestamp>
  heartbeatAt: Timestamp
  expiresAt: Timestamp
}

export interface GatewayRealtimeSession {
  id: string
  projectId: string
  organizationId: string
  virtualKeyId: string
  modelProviderId: string
  vendor: string
  agentId: string | null
  model: string
  requestedModel: string | null
  vendorConversationId: string | null
  status: Generated<GatewayRealtimeSessionStatus>
  mintedAt: Generated<Timestamp>
  closedAt: Timestamp | null
  closeReason: string | null
  traceId: string | null
  vendorCostRaw: unknown
  kind: string | null
  metering: string | null
  credentialExpiresAt: Timestamp | null
  transcriptionModel: string | null
  endUserId: string | null
  lastReportAt: Timestamp | null
  reportedCostNanoUsd: Generated<bigint>
  reportCount: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface GatewayRealtimeSessionReport {
  id: string
  sessionId: string
  reportKey: string
  projectId: string
  model: string
  usage: unknown
  costNanoUsd: bigint
  createdAt: Generated<Timestamp>
}

export interface SystemMigrationTenantState {
  migrationName: string
  tenantId: string
  status: string
  report: unknown
  occurredAt: Generated<Timestamp>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SystemMigrationEnrollment {
  organizationId: string
  migrationName: string
  enrolledByUserId: string
  createdAt: Generated<Timestamp>
}

export interface StoredObject {
  tenantId: string
  id: string
  status: string
  purpose: string
  ownerKind: string
  ownerId: string
  filename: string
  sha256: string
  sizeBytes: bigint
  mediaType: string
  mediaTypeVerified: Generated<boolean>
  storageProvider: string | null
  storageDestinationId: string | null
  storageProviderRelativeId: string | null
  generation: Generated<number>
  audiences: unknown
  expiresAt: Timestamp | null
  availableAt: Timestamp | null
  deletedAt: Timestamp | null
  source: string
  legacyFingerprint: string | null
  createdAt: Timestamp
  updatedAt: Generated<Timestamp>
}

export interface DiscoveredPerson {
  id: string
  organizationId: string
  provider: string
  rawActorId: string
  displayText: string
  kind: string
  department: string | null
  firstSeenAt: Timestamp
  lastSeenAt: Timestamp
  erasedAt: Timestamp | null
  moneyRowsPendingAt: Timestamp | null
  moneyRebuildSince: string | null
  suspendedAt: Timestamp | null
  suspendedReason: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DiscoveredAgent {
  id: string
  organizationId: string
  provider: string
  rawAgentId: string
  displayText: string
  metadata: unknown
  firstSeenAt: Timestamp
  lastSeenAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface IdentityMatch {
  id: string
  organizationId: string
  discoveredPersonId: string
  userId: string | null
  evidenceKind: string
  validFrom: Timestamp
  validTo: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface GovernanceTenantHistory {
  id: string
  organizationId: string
  tenantId: string
  firstUsedAt: Timestamp
  lastUsedAt: Timestamp
}

export interface ErasedIdentifierSuppression {
  id: string
  organizationId: string
  provider: string
  identifierHash: string
  erasedAt: Timestamp
}

export interface IdentityMatchSuggestion {
  id: string
  organizationId: string
  discoveredPersonId: string
  userId: string
  score: number
  computedAt: Timestamp
}

export interface ConnectedSeatChange {
  id: string
  licenseId: string
  accountId: string | null
  organizationId: string | null
  changedAt: Timestamp
  addedSeats: number
  unitAmountCents: number
  amountCents: number
  currency: Currency | null
  stripeInvoiceId: string | null
  state: ConnectedSeatChangeState
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ConnectedStatement {
  id: string
  accountId: string
  month: Timestamp
  sentAt: Timestamp
}

export interface LicenseRenewalClaim {
  stripeInvoiceId: string
  stripeSubscriptionId: string
  claimToken: string
  claimedAt: Timestamp
  issuedLicense: unknown
  deliveredAt: Timestamp | null
  licenseId: string | null
}

export interface InstanceIdentity {
  id: Generated<string>
  instanceId: string
  createdAt: Generated<Timestamp>
  lastReportAt: Timestamp | null
  lastReportError: string | null
  optionalMetricsOptOut: Generated<boolean>
  hostnameOptOut: Generated<boolean>
  upgradeDiagnosticsOptOut: Generated<boolean>
}

export interface SelfHostedInstance {
  id: string
  instanceId: string
  firstSeenAt: Generated<Timestamp>
  lastSeenAt: Timestamp
  version: string | null
  installMethod: string | null
  chartVersion: string | null
  hostname: string | null
  environment: string | null
  installedAt: Timestamp | null
  reportSchemaVersion: number | null
  organizationId: string | null
  issuedLicenseId: string | null
  userEmailDomains: unknown
  userDomains: Generated<string[]>
  latestReport: unknown
  optionalMetricsReported: Generated<boolean>
  hostnameReported: Generated<boolean>
  reportCount: Generated<number>
  lastUnknownFields: Generated<number>
  raisedSignals: Generated<string[]>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SelfHostedInstanceDiagnostic {
  id: string
  instanceId: string
  stepId: string
  stepDescription: string | null
  release: string | null
  attempt: number
  trigger: string
  byteLength: number
  objectKey: string
  receivedAt: Generated<Timestamp>
  expiresAt: Timestamp
  slackAnnouncedAt: Timestamp | null
}

export interface SelfHostedNotice {
  id: string
  severity: string
  title: string
  body: string
  url: string | null
  fixedIn: string | null
  status: string
  expiresAt: Timestamp | null
  publishedAt: Timestamp | null
  publishedBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SelfHostedInstanceReport {
  id: string
  instanceId: string
  receivedAt: Generated<Timestamp>
  version: string | null
  reportSchemaVersion: number | null
  unknownFields: Generated<number>
  payload: unknown
}

export interface WebPushSubscription {
  id: string
  userId: string
  endpoint: string
  p256dh: string
  auth: string
  userAgent: string | null
  createdAt: Generated<Timestamp>
  lastSuccessAt: Timestamp | null
}

export interface WebPushVapidKey {
  id: Generated<string>
  publicKey: string
  privateKeyEncrypted: string
  createdAt: Generated<Timestamp>
}

export interface SignInProviderMount {
  id: Generated<string>
  provider: string | null
  mounted: boolean
  lastAppliedEventId: string
  updatedAt: Generated<Timestamp>
}

export interface DB {
  Account: Account
  AccountCredential: AccountCredential
  Session: Session
  UserErasure: UserErasure
  User: User
  VerificationToken: VerificationToken
  Identifier: Identifier
  IdentitySignInSwitch: IdentitySignInSwitch
  IdentifierReservation: IdentifierReservation
  IdentityProjectionCursor: IdentityProjectionCursor
  TwoFactor: TwoFactor
  SignInAttemptLock: SignInAttemptLock
  Passkey: Passkey
  MfaEnrollment: MfaEnrollment
  SsoConnection: SsoConnection
  SsoConnectionRegistrationSlot: SsoConnectionRegistrationSlot
  SsoConnectionReproofCursor: SsoConnectionReproofCursor
  SsoCredential: SsoCredential
  SsoVerifiedDomain: SsoVerifiedDomain
  SsoVerifiedDomainHolder: SsoVerifiedDomainHolder
  SsoAuthenticationActivity: SsoAuthenticationActivity
  SsoProvider: SsoProvider
  SsoBreakGlassBinding: SsoBreakGlassBinding
  SsoActivationRecoveryReservation: SsoActivationRecoveryReservation
  JoinRequest: JoinRequest
  TeamUser: TeamUser
  OrganizationUser: OrganizationUser
  SeatHold: SeatHold
  SeatRequest: SeatRequest
  OrganizationSeatLease: OrganizationSeatLease
  OrganizationAccessNotice: OrganizationAccessNotice
  Team: Team
  Organization: Organization
  IssuedLicense: IssuedLicense
  OrganizationLicense: OrganizationLicense
  ActivationCode: ActivationCode
  ConnectedBillingAccount: ConnectedBillingAccount
  ConnectedCreditGrant: ConnectedCreditGrant
  ConnectedInvoice: ConnectedInvoice
  ScimRequestLog: ScimRequestLog
  ScimToken: ScimToken
  ScimExternalId: ScimExternalId
  ScimUserResource: ScimUserResource
  ScimDirectoryUser: ScimDirectoryUser
  ScimSyncState: ScimSyncState
  ScimSsoConnectionView: ScimSsoConnectionView
  OrganizationSeatPeak: OrganizationSeatPeak
  OrganizationSeatPeakHolder: OrganizationSeatPeakHolder
  OrganizationSeatPeakMonth: OrganizationSeatPeakMonth
  Project: Project
  Department: Department
  DepartmentMembershipHistory: DepartmentMembershipHistory
  PlatformToolPolicy: PlatformToolPolicy
  OrganizationInvite: OrganizationInvite
  Monitor: Monitor
  Cost: Cost
  Topic: Topic
  Dataset: Dataset
  DatasetRecord: DatasetRecord
  DatasetVersion: DatasetVersion
  DatasetVersionRun: DatasetVersionRun
  Dashboard: Dashboard
  DashboardFavourite: DashboardFavourite
  DashboardsV2: DashboardsV2
  DashboardWidget: DashboardWidget
  InsightProjection: InsightProjection
  InsightReaderProjection: InsightReaderProjection
  InsightDailyScheduleProjection: InsightDailyScheduleProjection
  SavedView: SavedView
  CustomGraph: CustomGraph
  BatchEvaluation: BatchEvaluation
  Trigger: Trigger
  SlackIntegration: SlackIntegration
  slack_connection_claim: SlackConnectionClaim
  WebhookEndpoint: WebhookEndpoint
  WebhookEndpointDelivery: WebhookEndpointDelivery
  Experiment: Experiment
  ExperimentVersion: ExperimentVersion
  Annotation: Annotation
  ModelProvider: ModelProvider
  ModelProviderScope: ModelProviderScope
  ModelDefaultConfig: ModelDefaultConfig
  ModelDefaultConfigScope: ModelDefaultConfigScope
  GithubInstallation: GithubInstallation
  GithubPullRequest: GithubPullRequest
  GithubBranchPullRequestCheck: GithubBranchPullRequestCheck
  LangyConversationProjection: LangyConversationProjection
  LangyTurnRequest: LangyTurnRequest
  LangyActiveTurn: LangyActiveTurn
  LangyConversationTurnProjection: LangyConversationTurnProjection
  LangyMessageProjection: LangyMessageProjection
  ProjectSecret: ProjectSecret
  TriggerSent: TriggerSent
  TriggerLatestEvaluation: TriggerLatestEvaluation
  EmailSuppression: EmailSuppression
  AnnotationScore: AnnotationScore
  AnnotationQueue: AnnotationQueue
  AnnotationQueueMembers: AnnotationQueueMembers
  AnnotationQueueScores: AnnotationQueueScores
  AnnotationQueueItem: AnnotationQueueItem
  ShareLink: ShareLink
  PinnedTrace: PinnedTrace
  TraceEditOverlay: TraceEditOverlay
  TraceIngestSourceBilling: TraceIngestSourceBilling
  RetentionPolicy: RetentionPolicy
  DataPrivacyPolicy: DataPrivacyPolicy
  InstantEvalJudgeProject: InstantEvalJudgeProject
  InstantEvalJudgeUsageBilling: InstantEvalJudgeUsageBilling
  InstantEvalJudgeSpend: InstantEvalJudgeSpend
  CustomLLMModelCost: CustomLLMModelCost
  Workflow: Workflow
  WorkflowVersion: WorkflowVersion
  AuditLog: AuditLog
  LlmPromptConfig: LlmPromptConfig
  LlmPromptConfigVersion: LlmPromptConfigVersion
  PromptTagAssignment: PromptTagAssignment
  PromptTag: PromptTag
  Analytics: Analytics
  CustomRole: CustomRole
  Notification: Notification
  Agent: Agent
  AgentVersion: AgentVersion
  AgentVersionLabel: AgentVersionLabel
  Evaluator: Evaluator
  EvaluatorVersion: EvaluatorVersion
  EvaluatorVersionLabel: EvaluatorVersionLabel
  Scenario: Scenario
  ScenarioVersion: ScenarioVersion
  SimulationSuite: SimulationSuite
  Subscription: Subscription
  Invoice: Invoice
  InvoiceItem: InvoiceItem
  Group: Group
  GroupMembership: GroupMembership
  AuthzUserStanding: AuthzUserStanding
  AuthzEngineSwitch: AuthzEngineSwitch
  AuthzOrganizationPolicy: AuthzOrganizationPolicy
  RoleBinding: RoleBinding
  Grant: Grant
  GrantUsage: GrantUsage
  Role: Role
  ApiKey: ApiKey
  SigningKey: SigningKey
  ProjectActiveDay: ProjectActiveDay
  NurturingProject: NurturingProject
  NurturingUncopiedProject: NurturingUncopiedProject
  NurturingOrganizationMember: NurturingOrganizationMember
  NurturingOrganizationCreated: NurturingOrganizationCreated
  NurturingOrganization: NurturingOrganization
  NurturingProjectUsageView: NurturingProjectUsageView
  NurturingProjectTraceDay: NurturingProjectTraceDay
  NurturingPersonalProject: NurturingPersonalProject
  NurturingTracedProject: NurturingTracedProject
  BillingMeterCheckpoint: BillingMeterCheckpoint
  VirtualKey: VirtualKey
  VirtualKeyScope: VirtualKeyScope
  RoutingPolicy: RoutingPolicy
  RoutingPolicyScope: RoutingPolicyScope
  GatewayGuardrail: GatewayGuardrail
  IngestionSource: IngestionSource
  AnomalyRule: AnomalyRule
  AnomalyAlert: AnomalyAlert
  AiToolEntry: AiToolEntry
  AiToolEntryTeam: AiToolEntryTeam
  AiToolEntryDepartment: AiToolEntryDepartment
  IngestionTemplate: IngestionTemplate
  GatewayConnectUpstream: GatewayConnectUpstream
  GatewayTraceExportKey: GatewayTraceExportKey
  GatewayBudget: GatewayBudget
  GatewayBudgetBucketBoundary: GatewayBudgetBucketBoundary
  GatewayBudgetLedger: GatewayBudgetLedger
  GatewayChangeEvent: GatewayChangeEvent
  GatewayCacheRule: GatewayCacheRule
  FeatureFlag: FeatureFlag
  FeatureFlagExperimentSetting: FeatureFlagExperimentSetting
  ProcessManagerInstance: ProcessManagerInstance
  ProcessManagerInbox: ProcessManagerInbox
  ProcessManagerOutbox: ProcessManagerOutbox
  ProcessManagerOutboxAttempt: ProcessManagerOutboxAttempt
  TopicClusteringRunProjection: TopicClusteringRunProjection
  TopicModelProjection: TopicModelProjection
  TopicClusteringRunHistoryProjection: TopicClusteringRunHistoryProjection
  IngestionPullRunProjection: IngestionPullRunProjection
  ScheduledJob: ScheduledJob
  BugReport: BugReport
  IdempotencyReceipt: IdempotencyReceipt
  GatewayRealtimeSession: GatewayRealtimeSession
  GatewayRealtimeSessionReport: GatewayRealtimeSessionReport
  SystemMigrationTenantState: SystemMigrationTenantState
  SystemMigrationEnrollment: SystemMigrationEnrollment
  StoredObject: StoredObject
  DiscoveredPerson: DiscoveredPerson
  DiscoveredAgent: DiscoveredAgent
  IdentityMatch: IdentityMatch
  GovernanceTenantHistory: GovernanceTenantHistory
  ErasedIdentifierSuppression: ErasedIdentifierSuppression
  IdentityMatchSuggestion: IdentityMatchSuggestion
  ConnectedSeatChange: ConnectedSeatChange
  ConnectedStatement: ConnectedStatement
  LicenseRenewalClaim: LicenseRenewalClaim
  InstanceIdentity: InstanceIdentity
  SelfHostedInstance: SelfHostedInstance
  SelfHostedInstanceDiagnostic: SelfHostedInstanceDiagnostic
  SelfHostedNotice: SelfHostedNotice
  SelfHostedInstanceReport: SelfHostedInstanceReport
  WebPushSubscription: WebPushSubscription
  WebPushVapidKey: WebPushVapidKey
  SignInProviderMount: SignInProviderMount
}
