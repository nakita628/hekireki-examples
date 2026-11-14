import {
  type PgTableExtraConfigValue,
  bigint,
  bigserial,
  boolean,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  numeric,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  unique,
  uniqueIndex,
  varchar,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import { nanoid } from 'nanoid'
import cuid from 'cuid'

const utcNow = (() => {
  let now: Date | undefined
  return () => {
    if (now === undefined) {
      now = new Date()
      queueMicrotask(() => {
        now = undefined
      })
    }
    return now
  }
})()

export const ssoConnectionMigrationPhaseEnum = pgEnum('SsoConnectionMigrationPhase', [
  'SETUP',
  'GRACE_LEGACY',
  'GRACE_DIRECT',
  'FINALIZING',
  'FINALIZED',
])

export const teamUserRoleEnum = pgEnum('TeamUserRole', ['ADMIN', 'MEMBER', 'VIEWER', 'CUSTOM'])

export const roleBindingScopeTypeEnum = pgEnum('RoleBindingScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const grantScopeTypeEnum = pgEnum('GrantScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
  'RESOURCE',
  'PLATFORM',
])

export const grantPrincipalTypeEnum = pgEnum('GrantPrincipalType', [
  'USER',
  'API_KEY',
  'GROUP',
  'TEAM',
  'ORGANIZATION',
  'PROJECT',
  'ANYONE',
])

export const organizationUserRoleEnum = pgEnum('OrganizationUserRole', [
  'ADMIN',
  'MEMBER',
  'EXTERNAL',
  'DEVELOPER',
])

export const issuedLicenseSourceEnum = pgEnum('IssuedLicenseSource', [
  'BACKOFFICE',
  'PURCHASE',
  'SCRIPT',
  'LEGACY_IMPORT',
])

export const connectedCreditGrantKindEnum = pgEnum('ConnectedCreditGrantKind', [
  'commit',
  'added',
  'renewal',
])

export const connectedInvoiceKindEnum = pgEnum('ConnectedInvoiceKind', [
  'annual',
  'seat_change',
  'usage',
])

export const planTypesEnum = pgEnum('PlanTypes', [
  'FREE',
  'PRO',
  'GROWTH',
  'GROWTH_SEAT_EUR_MONTHLY',
  'GROWTH_SEAT_EUR_ANNUAL',
  'GROWTH_SEAT_USD_MONTHLY',
  'GROWTH_SEAT_USD_ANNUAL',
  'ENTERPRISE',
  'LAUNCH',
  'ACCELERATE',
  'LAUNCH_ANNUAL',
  'ACCELERATE_ANNUAL',
])

export const pricingModelEnum = pgEnum('PricingModel', ['TIERED', 'SEAT_EVENT'])

export const organizationIntentEnum = pgEnum('OrganizationIntent', ['AGENT_GOVERNANCE', 'LLM_OPS'])

export const subscriptionStatusEnum = pgEnum('SubscriptionStatus', [
  'PENDING',
  'FAILED',
  'ACTIVE',
  'CANCELLED',
])

export const currencyEnum = pgEnum('Currency', ['USD', 'EUR'])

export const inviteStatusEnum = pgEnum('INVITE_STATUS', [
  'PENDING',
  'ACCEPTED',
  'WAITING_APPROVAL',
  'PAYMENT_PENDING',
  'REVOKED',
])

export const evaluationExecutionModeEnum = pgEnum('EvaluationExecutionMode', [
  'ON_MESSAGE',
  'AS_GUARDRAIL',
  'MANUALLY',
])

export const costTypeEnum = pgEnum('CostType', [
  'TRACE_CHECK',
  'GUARDRAIL',
  'CLUSTERING',
  'BATCH_EVALUATION',
])

export const costReferenceTypeEnum = pgEnum('CostReferenceType', [
  'CHECK',
  'TRACE',
  'PROJECT',
  'BATCH',
])

export const dashboardScopeEnum = pgEnum('DashboardScope', ['PRIVATE', 'PROJECT', 'ORGANIZATION'])

export const triggerActionEnum = pgEnum('TriggerAction', [
  'SEND_EMAIL',
  'ADD_TO_DATASET',
  'ADD_TO_ANNOTATION_QUEUE',
  'SEND_SLACK_MESSAGE',
  'SEND_WEBHOOK',
])

export const triggerKindEnum = pgEnum('TriggerKind', ['AUTOMATION', 'ALERT', 'REPORT'])

export const alertTypeEnum = pgEnum('AlertType', ['CRITICAL', 'WARNING', 'INFO'])

export const slackIntegrationScopeTypeEnum = pgEnum('SlackIntegrationScopeType', [
  'ORGANIZATION',
  'PROJECT',
])

export const slackIntegrationKindEnum = pgEnum('SlackIntegrationKind', ['BOT', 'INCOMING_WEBHOOK'])

export const webhookDeliveryOutcomeEnum = pgEnum('WebhookDeliveryOutcome', [
  'success',
  'retryable',
  'terminal',
  'pending',
])

export const webhookDestinationKindEnum = pgEnum('WebhookDestinationKind', ['http', 'sqs'])

export const webhookEndpointStatusEnum = pgEnum('WebhookEndpointStatus', ['ACTIVE', 'DISABLED'])

export const webhookDeliveryChannelEnum = pgEnum('WebhookDeliveryChannel', [
  'platform',
  'automations',
])

export const experimentTypeEnum = pgEnum('ExperimentType', [
  'DSPY',
  'BATCH_EVALUATION',
  'BATCH_EVALUATION_V2',
  'EVALUATIONS_V3',
])

export const modelProviderScopeTypeEnum = pgEnum('ModelProviderScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const modelDefaultScopeTypeEnum = pgEnum('ModelDefaultScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const langyProjectionTitleSourceEnum = pgEnum('LangyProjectionTitleSource', [
  'derived',
  'auto',
  'user',
])

export const annotationScoreDataTypeEnum = pgEnum('AnnotationScoreDataType', [
  'CATEGORICAL',
  'BOOLEAN',
  'LIKERT',
  'OPTION',
  'CHECKBOX',
])

export const shareResourceTypeEnum = pgEnum('ShareResourceType', ['TRACE', 'THREAD'])

export const shareVisibilityEnum = pgEnum('ShareVisibility', ['PUBLIC', 'ORGANIZATION', 'PROJECT'])

export const pinSourceEnum = pgEnum('PinSource', ['manual', 'share'])

export const retentionPolicyScopeTypeEnum = pgEnum('RetentionPolicyScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const dataPrivacyScopeTypeEnum = pgEnum('DataPrivacyScopeType', [
  'ORGANIZATION',
  'DEPARTMENT',
  'TEAM',
  'PROJECT',
])

export const customLlmmodelCostScopeTypeEnum = pgEnum('CustomLLMModelCostScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const promptScopeEnum = pgEnum('PromptScope', ['ORGANIZATION', 'PROJECT'])

export const virtualKeyStatusEnum = pgEnum('VirtualKeyStatus', ['ACTIVE', 'DISABLED', 'REVOKED'])

export const virtualKeyPurposeEnum = pgEnum('VirtualKeyPurpose', ['USER', 'LANGY', 'CONNECT'])

export const gatewayProviderRotationPolicyEnum = pgEnum('GatewayProviderRotationPolicy', ['MANUAL'])

export const gatewayProviderHealthStatusEnum = pgEnum('GatewayProviderHealthStatus', [
  'UNKNOWN',
  'HEALTHY',
  'DEGRADED',
  'CIRCUIT_OPEN',
])

export const gatewayBudgetScopeTypeEnum = pgEnum('GatewayBudgetScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
  'VIRTUAL_KEY',
  'PRINCIPAL',
  'GROUP',
  'ATTRIBUTED_USER',
])

export const gatewayBudgetWindowEnum = pgEnum('GatewayBudgetWindow', [
  'MINUTE',
  'HOUR',
  'DAY',
  'WEEK',
  'MONTH',
  'TOTAL',
  'MANUAL',
])

export const gatewayBudgetBreachActionEnum = pgEnum('GatewayBudgetBreachAction', ['BLOCK', 'WARN'])

export const gatewayBudgetLedgerStatusEnum = pgEnum('GatewayBudgetLedgerStatus', [
  'SUCCESS',
  'PROVIDER_ERROR',
  'BLOCKED_BY_GUARDRAIL',
  'CANCELLED',
])

export const gatewayChangeEventKindEnum = pgEnum('GatewayChangeEventKind', [
  'VK_CREATED',
  'VK_CONFIG_UPDATED',
  'VK_REVOKED',
  'VK_ROTATED',
  'VK_DISABLED',
  'VK_ENABLED',
  'BUDGET_CREATED',
  'BUDGET_UPDATED',
  'BUDGET_DELETED',
  'MODEL_PROVIDER_UPDATED',
  'CACHE_RULE_CREATED',
  'CACHE_RULE_UPDATED',
  'CACHE_RULE_DELETED',
  'ROUTING_POLICY_UPDATED',
  'ROUTING_POLICY_DELETED',
])

export const gatewayCacheRuleModeEnum = pgEnum('GatewayCacheRuleMode', [
  'RESPECT',
  'FORCE',
  'DISABLE',
])

export const virtualKeyRoutingModeEnum = pgEnum('VirtualKeyRoutingMode', [
  'NONE',
  'FALLBACK_ALL',
  'POLICY',
])

export const virtualKeyScopeTypeEnum = pgEnum('VirtualKeyScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const routingPolicyScopeTypeEnum = pgEnum('RoutingPolicyScopeType', [
  'ORGANIZATION',
  'TEAM',
  'PROJECT',
])

export const gatewayGuardrailDirectionEnum = pgEnum('GatewayGuardrailDirection', [
  'PRE',
  'POST',
  'STREAM_CHUNK',
])

export const gatewayGuardrailFailureModeEnum = pgEnum('GatewayGuardrailFailureMode', [
  'FAIL_OPEN',
  'FAIL_CLOSED',
])

export const featureFlagExperimentSubjectTypeEnum = pgEnum('FeatureFlagExperimentSubjectType', [
  'USER',
  'ORGANIZATION',
  'PROJECT',
])

export const processManagerOutboxStatusEnum = pgEnum('ProcessManagerOutboxStatus', [
  'pending',
  'dispatched',
  'dead',
  'discarded',
])

export const processManagerOutboxAttemptOutcomeEnum = pgEnum('ProcessManagerOutboxAttemptOutcome', [
  'retry_scheduled',
  'dead',
])

export const gatewayRealtimeSessionStatusEnum = pgEnum('GatewayRealtimeSessionStatus', [
  'OPEN',
  'CLOSED',
  'FAILED',
  'EXPIRED',
])

export const connectedSeatChangeStateEnum = pgEnum('ConnectedSeatChangeState', [
  'intent',
  'invoiced',
  'nothing_to_invoice',
  'not_onboarded',
])

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    userId: text('userId').notNull(),
    type: text('type').notNull().default('oauth'),
    provider: text('provider').notNull(),
    issuer: text('issuer'),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: timestamp('expires_at', { precision: 3 }),
    ext_expires_in: integer('ext_expires_in'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
    password: text('password'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Account_userId_idx').on(table.userId),
    index('Account_issuer_providerAccountId_idx').on(table.issuer, table.providerAccountId),
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accountCredential = pgTable(
  'AccountCredential',
  {
    id: text('id').primaryKey(),
    userId: text('userId').notNull(),
    provider: text('provider').notNull(),
    password: text('password'),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    idToken: text('idToken'),
    accessTokenExpiresAt: timestamp('accessTokenExpiresAt', { precision: 3 }),
    refreshTokenExpiresAt: timestamp('refreshTokenExpiresAt', { precision: 3 }),
    scope: text('scope'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('AccountCredential_userId_idx').on(table.userId),
    foreignKey({
      name: 'AccountCredential_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = pgTable(
  'Session',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    identifierId: text('identifierId'),
    amr: text('amr').array().default([]),
    startedBySignUp: boolean('startedBySignUp').notNull().default(false),
    impersonating: jsonb('impersonating'),
    actorUserId: text('actorUserId'),
    subjectUserId: text('subjectUserId'),
    impersonationReason: text('impersonationReason'),
    impersonationExpiresAt: timestamp('impersonationExpiresAt', { precision: 3 }),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    index('Session_identifierId_idx').on(table.identifierId),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userErasure = pgTable(
  'UserErasure',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    accountCreatedAt: timestamp('accountCreatedAt', { precision: 3 }).notNull(),
    erasedAt: timestamp('erasedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('UserErasure_accountCreatedAt_idx').on(table.accountCreatedAt)],
)

export const user = pgTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name'),
    email: text('email'),
    emailVerified: boolean('emailVerified').notNull().default(false),
    signupConfirmationPending: boolean('signupConfirmationPending').notNull().default(false),
    passkeySignupClaimHash: text('passkeySignupClaimHash'),
    image: text('image'),
    pendingSsoSetup: boolean('pendingSsoSetup').notNull().default(false),
    userHashKey: text('userHashKey'),
    twoFactorEnabled: boolean('twoFactorEnabled').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    lastLoginAt: timestamp('lastLoginAt', { precision: 3 }),
    deactivatedAt: timestamp('deactivatedAt', { precision: 3 }),
    lastHomePath: text('lastHomePath'),
    tracesExplorerTourDismissedAt: timestamp('tracesExplorerTourDismissedAt', { precision: 3 }),
    langyCodeAccessPreference: text('langyCodeAccessPreference'),
    passkeyNudgeDismissedAt: timestamp('passkeyNudgeDismissedAt', { precision: 3 }),
    joinOfferDismissedDomains: text('joinOfferDismissedDomains').array().default([]),
    notificationPreferences: jsonb('notificationPreferences').notNull().default({}),
    passwordResetRequiredAt: timestamp('passwordResetRequiredAt', { precision: 3 }),
  },
  (table) => [uniqueIndex('User_email_key').on(table.email)],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const identifier = pgTable(
  'Identifier',
  {
    id: text('id').primaryKey(),
    userId: text('userId').notNull(),
    provider: text('provider').notNull(),
    value: text('value'),
    domain: text('domain'),
    identifierHash: text('identifierHash'),
    accountId: text('accountId'),
    providerId: text('providerId'),
    issuer: text('issuer'),
    providerAccountId: text('providerAccountId'),
    state: text('state').notNull(),
    connectionId: text('connectionId'),
    verifiedAt: timestamp('verifiedAt', { precision: 3 }),
    attachedAt: timestamp('attachedAt', { precision: 3 }).notNull(),
    detachedAt: timestamp('detachedAt', { precision: 3 }),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Identifier_userId_idx').on(table.userId),
    index('Identifier_identifierHash_idx').on(table.identifierHash),
    index('Identifier_value_idx').on(table.value),
    index('Identifier_domain_idx').on(table.domain),
    index('Identifier_provider_providerAccountId_idx').on(table.provider, table.providerAccountId),
    index('Identifier_issuer_providerAccountId_idx').on(table.issuer, table.providerAccountId),
    index('Identifier_accountId_idx').on(table.accountId),
  ],
)

export const identitySignInSwitch = pgTable('IdentitySignInSwitch', {
  userId: text('userId').primaryKey(),
  onIdentifiers: boolean('onIdentifiers').notNull(),
  pinned: boolean('pinned').notNull().default(false),
  switchedAt: timestamp('switchedAt', { precision: 3 }).notNull(),
  lastAppliedEventId: text('lastAppliedEventId'),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const identifierReservation = pgTable(
  'IdentifierReservation',
  {
    normalizedValue: text('normalizedValue').primaryKey(),
    userId: text('userId').notNull(),
    identifierId: text('identifierId').notNull(),
    commandId: text('commandId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('IdentifierReservation_userId_idx').on(table.userId),
    index('IdentifierReservation_identifierId_idx').on(table.identifierId),
  ],
)

export const identityProjectionCursor = pgTable('IdentityProjectionCursor', {
  userId: text('userId').primaryKey(),
  lastEventId: text('lastEventId').notNull(),
  acceptedAt: timestamp('acceptedAt', { precision: 3 }).notNull(),
  occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
  projectionVersion: text('projectionVersion').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const twoFactor = pgTable(
  'TwoFactor',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    userId: text('userId').notNull(),
    secret: text('secret').notNull(),
    backupCodes: text('backupCodes').notNull(),
    verified: boolean('verified').notNull().default(true),
    failedVerificationCount: integer('failedVerificationCount').notNull().default(0),
    lockedUntil: timestamp('lockedUntil', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('TwoFactor_userId_idx').on(table.userId),
    index('TwoFactor_secret_idx').on(table.secret),
    foreignKey({
      name: 'TwoFactor_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const signInAttemptLock = pgTable(
  'SignInAttemptLock',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    identifierHash: text('identifierHash').notNull(),
    failedCount: integer('failedCount').notNull().default(0),
    lockedUntil: timestamp('lockedUntil', { precision: 3 }),
    consecutiveLockouts: integer('consecutiveLockouts').notNull().default(0),
    heldForReview: boolean('heldForReview').notNull().default(false),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('SignInAttemptLock_identifierHash_key').on(table.identifierHash),
    index('SignInAttemptLock_lockedUntil_idx').on(table.lockedUntil),
    index('SignInAttemptLock_userId_idx').on(table.userId),
  ],
)

export const passkey = pgTable(
  'Passkey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name'),
    publicKey: text('publicKey').notNull(),
    userId: text('userId').notNull(),
    credentialID: text('credentialID').notNull(),
    counter: integer('counter').notNull().default(0),
    deviceType: text('deviceType').notNull(),
    backedUp: boolean('backedUp').notNull().default(false),
    transports: text('transports'),
    aaguid: text('aaguid'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Passkey_userId_idx').on(table.userId),
    index('Passkey_credentialID_idx').on(table.credentialID),
    foreignKey({ name: 'Passkey_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mfaEnrollment = pgTable(
  'MfaEnrollment',
  {
    userId: text('userId').primaryKey(),
    enrollmentId: text('enrollmentId'),
    method: text('method'),
    state: text('state').notNull(),
    enrolledAt: timestamp('enrolledAt', { precision: 3 }),
    confirmedAt: timestamp('confirmedAt', { precision: 3 }),
    expiredAt: timestamp('expiredAt', { precision: 3 }),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    disabledVia: text('disabledVia'),
    backupCodeCount: integer('backupCodeCount').notNull().default(0),
    consumedBackupCodeIndexes: integer('consumedBackupCodeIndexes').array().default([]),
    failedCount: integer('failedCount').notNull().default(0),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    lastEventId: text('lastEventId').notNull(),
    acceptedAt: timestamp('acceptedAt', { precision: 3 }).notNull(),
    projectionVersion: text('projectionVersion').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [index('MfaEnrollment_state_idx').on(table.state)],
)

export const ssoConnection = pgTable(
  'SsoConnection',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    type: text('type').notNull(),
    state: text('state').notNull(),
    claimedDomains: text('claimedDomains').array(),
    domainClaims: jsonb('domainClaims').notNull().default([]),
    approvedDomains: text('approvedDomains').array(),
    verifiedDomains: text('verifiedDomains').array(),
    lapsedDomains: text('lapsedDomains').array(),
    domainVerifications: jsonb('domainVerifications').notNull().default([]),
    pendingVerification: jsonb('pendingVerification'),
    idpMetadata: jsonb('idpMetadata').notNull(),
    arrivalPolicy: text('arrivalPolicy').notNull().default('refuse'),
    allowsJit: boolean('allowsJit').notNull().default(false),
    arrivalPolicyDecidedAt: timestamp('arrivalPolicyDecidedAt', { precision: 3 }),
    source: text('source').notNull(),
    testLoginAccountId: text('testLoginAccountId'),
    replacesConnectionId: text('replacesConnectionId'),
    migrationPhase: ssoConnectionMigrationPhaseEnum('migrationPhase'),
    graceStartedAt: timestamp('graceStartedAt', { precision: 3 }),
    routeChangedAt: timestamp('routeChangedAt', { precision: 3 }),
    finalizationRequestedAt: timestamp('finalizationRequestedAt', { precision: 3 }),
    finalizedAt: timestamp('finalizedAt', { precision: 3 }),
    rejection: jsonb('rejection'),
    createdBy: text('createdBy'),
    tearDownAfter: timestamp('tearDownAfter', { precision: 3 }),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    lastEventId: text('lastEventId').notNull(),
    acceptedAt: timestamp('acceptedAt', { precision: 3 }).notNull(),
    projectionVersion: text('projectionVersion').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('SsoConnection_organizationId_idx').on(table.organizationId),
    index('SsoConnection_organizationId_migrationPhase_idx').on(
      table.organizationId,
      table.migrationPhase,
    ),
    index('SsoConnection_state_idx').on(table.state),
    index('SsoConnection_verifiedDomains_idx').on(table.verifiedDomains),
    index('SsoConnection_lapsedDomains_idx').on(table.lapsedDomains),
  ],
)

export const ssoConnectionRegistrationSlot = pgTable(
  'SsoConnectionRegistrationSlot',
  {
    organizationId: text('organizationId').notNull(),
    kind: text('kind').notNull(),
    connectionId: text('connectionId').notNull(),
    replacesConnectionId: text('replacesConnectionId'),
    commandId: text('commandId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'SsoConnectionRegistrationSlot_pkey',
      columns: [table.organizationId, table.kind],
    }),
    uniqueIndex('SsoConnectionRegistrationSlot_connectionId_key').on(table.connectionId),
  ],
)

export const ssoConnectionReproofCursor = pgTable(
  'SsoConnectionReproofCursor',
  {
    connectionId: text('connectionId').primaryKey(),
    lastReproofAt: timestamp('lastReproofAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('SsoConnectionReproofCursor_lastReproofAt_idx').on(table.lastReproofAt),
    foreignKey({
      name: 'SsoConnectionReproofCursor_connectionId_fkey',
      columns: [table.connectionId],
      foreignColumns: [ssoConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const ssoCredential = pgTable(
  'SsoCredential',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId').notNull(),
    kind: text('kind').notNull(),
    ciphertext: text('ciphertext').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('SsoCredential_organizationId_connectionId_idx').on(
      table.organizationId,
      table.connectionId,
    ),
  ],
)

export const ssoVerifiedDomain = pgTable(
  'SsoVerifiedDomain',
  { domain: text('domain').primaryKey(), organizationId: text('organizationId').notNull() },
  (table) => [
    index('SsoVerifiedDomain_organizationId_idx').on(table.organizationId),
    unique('SsoVerifiedDomain_domain_organizationId_key').on(table.domain, table.organizationId),
  ],
)

export const ssoVerifiedDomainHolder = pgTable(
  'SsoVerifiedDomainHolder',
  {
    domain: text('domain').notNull(),
    connectionId: text('connectionId').notNull(),
    organizationId: text('organizationId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'SsoVerifiedDomainHolder_pkey',
      columns: [table.domain, table.connectionId],
    }),
    index('SsoVerifiedDomainHolder_connectionId_idx').on(table.connectionId),
    index('SsoVerifiedDomainHolder_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'SsoVerifiedDomainHolder_domain_organizationId_fkey',
      columns: [table.domain, table.organizationId],
      foreignColumns: [ssoVerifiedDomain.domain, ssoVerifiedDomain.organizationId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const ssoAuthenticationActivity = pgTable(
  'SsoAuthenticationActivity',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId').notNull(),
    userId: text('userId').notNull(),
    authenticatedAt: timestamp('authenticatedAt', { precision: 3 }).notNull(),
    providerAccountId: text('providerAccountId'),
  },
  (table) => [
    index('SsoAuthenticationActivity_organizationId_connectionId_authe_idx').on(
      table.organizationId,
      table.connectionId,
      table.authenticatedAt,
    ),
    index('SsoAuthenticationActivity_organizationId_userId_idx').on(
      table.organizationId,
      table.userId,
    ),
  ],
)

export const ssoProvider = pgTable(
  'SsoProvider',
  {
    id: text('id').primaryKey(),
    issuer: text('issuer').notNull(),
    oidcConfig: text('oidcConfig'),
    samlConfig: text('samlConfig'),
    userId: text('userId'),
    providerId: text('providerId').notNull(),
    organizationId: text('organizationId'),
    domain: text('domain').notNull(),
  },
  (table) => [
    uniqueIndex('SsoProvider_providerId_key').on(table.providerId),
    index('SsoProvider_organizationId_idx').on(table.organizationId),
    index('SsoProvider_domain_idx').on(table.domain),
  ],
)

export const ssoBreakGlassBinding = pgTable(
  'SsoBreakGlassBinding',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    grantedByUserId: text('grantedByUserId').notNull(),
    grantedAt: timestamp('grantedAt', { precision: 3 }).notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    supersededAt: timestamp('supersededAt', { precision: 3 }),
    renewedFromId: text('renewedFromId'),
    warnedDays: integer('warnedDays').array(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('SsoBreakGlassBinding_organizationId_expiresAt_idx').on(
      table.organizationId,
      table.expiresAt,
    ),
    index('SsoBreakGlassBinding_userId_idx').on(table.userId),
  ],
)

export const ssoActivationRecoveryReservation = pgTable(
  'SsoActivationRecoveryReservation',
  {
    commandId: text('commandId').primaryKey(),
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('SsoActivationRecoveryReservation_organizationId_idx').on(table.organizationId),
    uniqueIndex('SsoActivationRecoveryReservation_organizationId_connectionI_key').on(
      table.organizationId,
      table.connectionId,
    ),
  ],
)

export const joinRequest = pgTable(
  'JoinRequest',
  {
    id: text('id').primaryKey(),
    userId: text('userId').notNull(),
    organizationId: text('organizationId').notNull(),
    domain: text('domain').notNull(),
    state: text('state').notNull(),
    matchedVia: text('matchedVia').notNull(),
    origin: text('origin').notNull().default('web'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    resolvedAt: timestamp('resolvedAt', { precision: 3 }),
    resolvedByType: text('resolvedByType'),
    resolvedById: text('resolvedById'),
    withdrawalCause: text('withdrawalCause'),
    connectionId: text('connectionId'),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    lastEventId: text('lastEventId').notNull(),
    acceptedAt: timestamp('acceptedAt', { precision: 3 }).notNull(),
    projectionVersion: text('projectionVersion').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('JoinRequest_organizationId_state_idx').on(table.organizationId, table.state),
    index('JoinRequest_userId_state_idx').on(table.userId, table.state),
    index('JoinRequest_userId_organizationId_idx').on(table.userId, table.organizationId),
    index('JoinRequest_domain_idx').on(table.domain),
  ],
)

export const teamUser = pgTable(
  'TeamUser',
  {
    userId: text('userId').notNull(),
    teamId: text('teamId').notNull(),
    role: teamUserRoleEnum('role').notNull(),
    assignedRoleId: text('assignedRoleId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'TeamUser_pkey', columns: [table.userId, table.teamId] }),
    index('TeamUser_teamId_idx').on(table.teamId),
    index('TeamUser_userId_idx').on(table.userId),
    index('TeamUser_assignedRoleId_idx').on(table.assignedRoleId),
    foreignKey({
      name: 'TeamUser_assignedRoleId_fkey',
      columns: [table.assignedRoleId],
      foreignColumns: [customRole.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'TeamUser_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'TeamUser_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const organizationUser = pgTable(
  'OrganizationUser',
  {
    userId: text('userId').notNull(),
    organizationId: text('organizationId').notNull(),
    role: organizationUserRoleEnum('role').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    departmentId: text('departmentId'),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    pendingSsoGrantId: text('pendingSsoGrantId'),
    membershipStamp: text('membershipStamp')
      .notNull()
      .default(sql`gen_random_uuid()::text`),
    fullSeatSince: timestamp('fullSeatSince', { precision: 3 }),
    seatPendingSince: timestamp('seatPendingSince', { precision: 3 }),
  },
  (table) => [
    primaryKey({ name: 'OrganizationUser_pkey', columns: [table.userId, table.organizationId] }),
    index('OrganizationUser_organizationId_idx').on(table.organizationId),
    index('OrganizationUser_userId_idx').on(table.userId),
    index('OrganizationUser_departmentId_idx').on(table.departmentId),
    foreignKey({
      name: 'OrganizationUser_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganizationUser_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const seatHold = pgTable(
  'SeatHold',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    email: text('email'),
    name: text('name'),
    seatTakenAt: timestamp('seatTakenAt', { precision: 3 }).notNull(),
    heldUntil: timestamp('heldUntil', { precision: 3 }).notNull(),
    reason: text('reason').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('SeatHold_heldUntil_idx').on(table.heldUntil),
    uniqueIndex('SeatHold_organizationId_userId_key').on(table.organizationId, table.userId),
  ],
)

export const seatRequest = pgTable(
  'SeatRequest',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    email: text('email').notNull(),
    userId: text('userId'),
    name: text('name'),
    status: text('status').notNull(),
    source: text('source').notNull(),
    idpRefused: boolean('idpRefused').notNull().default(false),
    idpKind: text('idpKind'),
    salesNotifiedAt: timestamp('salesNotifiedAt', { precision: 3 }),
    openedAt: timestamp('openedAt', { precision: 3 }).notNull(),
    resolvedAt: timestamp('resolvedAt', { precision: 3 }),
    resolvedByUserId: text('resolvedByUserId'),
    seatPendingSince: timestamp('seatPendingSince', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('SeatRequest_organizationId_status_idx').on(table.organizationId, table.status),
    index('SeatRequest_userId_idx').on(table.userId),
    uniqueIndex('SeatRequest_organizationId_email_key').on(table.organizationId, table.email),
  ],
)

export const organizationSeatLease = pgTable('OrganizationSeatLease', {
  organizationId: text('organizationId').primaryKey(),
  holder: text('holder').notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const organizationAccessNotice = pgTable(
  'OrganizationAccessNotice',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    kind: text('kind').notNull(),
    email: text('email').notNull(),
    name: text('name'),
    idp: text('idp'),
    dayKey: text('dayKey').notNull(),
    idpRefused: boolean('idpRefused').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    mailedAt: timestamp('mailedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('OrganizationAccessNotice_organizationId_kind_email_dayKey_key').on(
      table.organizationId,
      table.kind,
      table.email,
      table.dayKey,
    ),
  ],
)

export const team = pgTable(
  'Team',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    organizationId: text('organizationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    isPersonal: boolean('isPersonal').notNull().default(false),
    ownerUserId: varchar('ownerUserId', { length: 255 }),
    departmentId: text('departmentId'),
  },
  (table) => [
    uniqueIndex('Team_slug_key').on(table.slug),
    index('Team_organizationId_idx').on(table.organizationId),
    index('Team_isPersonal_idx').on(table.isPersonal),
    index('Team_departmentId_idx').on(table.departmentId),
    foreignKey({
      name: 'Team_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Team_ownerUserId_fkey',
      columns: [table.ownerUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const organization = pgTable(
  'Organization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    kind: text('kind').notNull().default('customer'),
    phoneNumber: text('phoneNumber'),
    slug: text('slug').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    usageSpendingMaxLimit: integer('usageSpendingMaxLimit'),
    datasetAttachmentMaxMb: integer('datasetAttachmentMaxMb'),
    privateClickHouseLabel: text('privateClickHouseLabel'),
    privateS3Label: text('privateS3Label'),
    privateDataplaneSeenAt: timestamp('privateDataplaneSeenAt', { precision: 3 }),
    maxSessionDurationDays: integer('maxSessionDurationDays').notNull().default(0),
    mfaRequired: boolean('mfaRequired').notNull().default(false),
    lockoutAfterFailedAttempts: integer('lockoutAfterFailedAttempts').notNull().default(0),
    lockoutMinutes: integer('lockoutMinutes').notNull().default(30),
    sessionIdleTimeoutMinutes: integer('sessionIdleTimeoutMinutes').notNull().default(1440),
    sessionMaxLifetimeMinutes: integer('sessionMaxLifetimeMinutes').notNull().default(0),
    signupData: jsonb('signupData'),
    signedDPA: boolean('signedDPA').notNull().default(false),
    elasticsearchNodeUrl: text('elasticsearchNodeUrl'),
    elasticsearchApiKey: text('elasticsearchApiKey'),
    useCustomElasticsearch: boolean('useCustomElasticsearch').notNull().default(false),
    s3Endpoint: text('s3Endpoint'),
    s3AccessKeyId: text('s3AccessKeyId'),
    s3SecretAccessKey: text('s3SecretAccessKey'),
    s3Bucket: text('s3Bucket'),
    useCustomS3: boolean('useCustomS3').notNull().default(false),
    sentPlanLimitAlert: timestamp('sentPlanLimitAlert', { precision: 3 }),
    ssoDomain: text('ssoDomain'),
    ssoProvider: text('ssoProvider'),
    domainJoin: text('domainJoin').notNull().default('request'),
    joinerRole: organizationUserRoleEnum('joinerRole').notNull().default('MEMBER'),
    joinDomains: text('joinDomains').array(),
    presenceEnabled: boolean('presenceEnabled').notNull().default(true),
    traceSharingEnabled: boolean('traceSharingEnabled').notNull().default(true),
    supportContact: text('supportContact'),
    primaryIntent: organizationIntentEnum('primaryIntent'),
    promoCode: text('promoCode'),
    stripeCustomerId: text('stripeCustomerId'),
    currency: currencyEnum('currency').notNull().default('EUR'),
    pricingModel: pricingModelEnum('pricingModel').notNull().default('TIERED'),
    license: text('license'),
    licenseExpiresAt: timestamp('licenseExpiresAt', { precision: 3 }),
    licenseLastValidatedAt: timestamp('licenseLastValidatedAt', { precision: 3 }),
    selfHostedCustomer: boolean('selfHostedCustomer').notNull().default(false),
    connectServicesDisabled: text('connectServicesDisabled').array().default([]),
    instantEvalsEnabledAt: timestamp('instantEvalsEnabledAt', { precision: 3 }),
    instantEvalsEnabledByUserId: text('instantEvalsEnabledByUserId'),
    connectLastSyncAt: timestamp('connectLastSyncAt', { precision: 3 }),
    connectLastSyncError: text('connectLastSyncError'),
  },
  (table) => [
    uniqueIndex('Organization_slug_key').on(table.slug),
    uniqueIndex('Organization_ssoDomain_key').on(table.ssoDomain),
    uniqueIndex('Organization_stripeCustomerId_key').on(table.stripeCustomerId),
    index('Organization_ssoProvider_idx').on(table.ssoProvider),
  ],
)

export const issuedLicense = pgTable(
  'IssuedLicense',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    licenseId: text('licenseId').notNull(),
    tokenHash: text('tokenHash').notNull(),
    organizationId: text('organizationId'),
    organizationName: text('organizationName').notNull(),
    email: text('email').notNull(),
    planType: text('planType').notNull(),
    maxMembers: integer('maxMembers').notNull(),
    maxMembersLite: integer('maxMembersLite'),
    liteSeatsUnlimited: boolean('liteSeatsUnlimited').notNull().default(false),
    maxMembersDeveloper: integer('maxMembersDeveloper'),
    issuedAt: timestamp('issuedAt', { precision: 3 }).notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    source: issuedLicenseSourceEnum('source').notNull(),
    issuedById: text('issuedById'),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedById: text('revokedById'),
    revokedReason: text('revokedReason'),
    supersededAt: timestamp('supersededAt', { precision: 3 }),
    replacesId: text('replacesId'),
    pendingDeliveryLicense: text('pendingDeliveryLicense'),
    services: text('services').array().default([]),
    seatRateCents: integer('seatRateCents'),
    seatCurrency: currencyEnum('seatCurrency'),
    commitUsdCents: integer('commitUsdCents').notNull().default(0),
    overageEnabled: boolean('overageEnabled').notNull().default(false),
    overageMaxUsdCents: integer('overageMaxUsdCents'),
    instanceId: text('instanceId'),
    instanceBoundAt: timestamp('instanceBoundAt', { precision: 3 }),
    lastSyncAt: timestamp('lastSyncAt', { precision: 3 }),
    lastSyncVersion: text('lastSyncVersion'),
    reportedMembers: integer('reportedMembers'),
    reportedMembersLite: integer('reportedMembersLite'),
    virtualKeyId: text('virtualKeyId'),
    seatsRaisedFrom: integer('seatsRaisedFrom'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('IssuedLicense_licenseId_key').on(table.licenseId),
    uniqueIndex('IssuedLicense_tokenHash_key').on(table.tokenHash),
    uniqueIndex('IssuedLicense_replacesId_key').on(table.replacesId),
    uniqueIndex('IssuedLicense_virtualKeyId_key').on(table.virtualKeyId),
    index('IssuedLicense_organizationId_idx').on(table.organizationId),
    index('IssuedLicense_expiresAt_idx').on(table.expiresAt),
    index('IssuedLicense_seatsRaisedFrom_idx').on(table.seatsRaisedFrom),
    foreignKey({
      name: 'IssuedLicense_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const organizationLicense = pgTable('OrganizationLicense', {
  organizationId: text('organizationId').primaryKey(),
  licenseKey: text('licenseKey'),
  expiresAt: timestamp('expiresAt', { precision: 3 }),
  validatedAt: timestamp('validatedAt', { precision: 3 }),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const activationCode = pgTable(
  'ActivationCode',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    codeHash: text('codeHash').notNull(),
    codeHint: text('codeHint').notNull(),
    organizationId: text('organizationId').notNull(),
    organizationName: text('organizationName').notNull(),
    email: text('email').notNull(),
    planType: text('planType').notNull(),
    maxMembers: integer('maxMembers').notNull(),
    maxMembersLite: integer('maxMembersLite').default(0),
    liteSeatsUnlimited: boolean('liteSeatsUnlimited').notNull().default(false),
    maxMembersDeveloper: integer('maxMembersDeveloper'),
    licenseTermDays: integer('licenseTermDays').notNull(),
    services: text('services').array().default([]),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    reusable: boolean('reusable').notNull().default(false),
    redeemedAt: timestamp('redeemedAt', { precision: 3 }),
    redeemedByInstanceId: text('redeemedByInstanceId'),
    issuedLicenseId: text('issuedLicenseId'),
    redemptionCount: integer('redemptionCount').notNull().default(0),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedById: text('revokedById'),
    createdById: text('createdById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ActivationCode_codeHash_key').on(table.codeHash),
    index('ActivationCode_organizationId_idx').on(table.organizationId),
    index('ActivationCode_expiresAt_idx').on(table.expiresAt),
  ],
)

export const connectedBillingAccount = pgTable(
  'ConnectedBillingAccount',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    stripeCustomerId: text('stripeCustomerId').notNull(),
    usageSubscriptionId: text('usageSubscriptionId'),
    usageSubscriptionItemId: text('usageSubscriptionItemId'),
    termStartsAt: timestamp('termStartsAt', { precision: 3 }).notNull(),
    termEndsAt: timestamp('termEndsAt', { precision: 3 }).notNull(),
    commitUsdCents: integer('commitUsdCents').notNull().default(0),
    seatCurrency: currencyEnum('seatCurrency').notNull(),
    seatRateCents: integer('seatRateCents').notNull(),
    seats: integer('seats').notNull(),
    bankTransferType: text('bankTransferType'),
    bankTransferCountry: text('bankTransferCountry'),
    billingEmail: text('billingEmail').notNull(),
    pendingRenewal: jsonb('pendingRenewal'),
    createdById: text('createdById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ConnectedBillingAccount_organizationId_key').on(table.organizationId),
    uniqueIndex('ConnectedBillingAccount_stripeCustomerId_key').on(table.stripeCustomerId),
    foreignKey({
      name: 'ConnectedBillingAccount_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const connectedCreditGrant = pgTable(
  'ConnectedCreditGrant',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    accountId: text('accountId').notNull(),
    stripeCreditGrantId: text('stripeCreditGrantId').notNull(),
    amountUsdCents: integer('amountUsdCents').notNull(),
    kind: connectedCreditGrantKindEnum('kind').notNull(),
    termEndsAt: timestamp('termEndsAt', { precision: 3 }).notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('ConnectedCreditGrant_stripeCreditGrantId_key').on(table.stripeCreditGrantId),
    index('ConnectedCreditGrant_accountId_idx').on(table.accountId),
  ],
)

export const connectedInvoice = pgTable(
  'ConnectedInvoice',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    accountId: text('accountId').notNull(),
    stripeInvoiceId: text('stripeInvoiceId').notNull(),
    kind: connectedInvoiceKindEnum('kind').notNull(),
    currency: currencyEnum('currency').notNull(),
    amountCents: integer('amountCents').notNull(),
    status: text('status').notNull(),
    paidOutOfBandAt: timestamp('paidOutOfBandAt', { precision: 3 }),
    termStartsAt: timestamp('termStartsAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ConnectedInvoice_stripeInvoiceId_key').on(table.stripeInvoiceId),
    index('ConnectedInvoice_accountId_idx').on(table.accountId),
  ],
)

export const scimRequestLog = pgTable(
  'ScimRequestLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId'),
    method: text('method').notNull(),
    resource: text('resource').notNull(),
    status: integer('status').notNull(),
    reason: text('reason'),
    detail: text('detail'),
    occurredAt: timestamp('occurredAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ScimRequestLog_organizationId_occurredAt_idx').on(
      table.organizationId,
      table.occurredAt,
    ),
    index('ScimRequestLog_connectionId_occurredAt_idx').on(table.connectionId, table.occurredAt),
    index('ScimRequestLog_occurredAt_idx').on(table.occurredAt),
    foreignKey({
      name: 'ScimRequestLog_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimToken = pgTable(
  'ScimToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId'),
    hashedToken: text('hashedToken').notNull(),
    hashScheme: text('hashScheme').notNull().default('sha256'),
    description: text('description'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('ScimToken_hashedToken_key').on(table.hashedToken),
    index('ScimToken_organizationId_idx').on(table.organizationId),
    index('ScimToken_connectionId_idx').on(table.connectionId),
    foreignKey({
      name: 'ScimToken_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimExternalId = pgTable(
  'ScimExternalId',
  {
    organizationId: text('organizationId').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    connectionId: text('connectionId').notNull(),
    externalId: text('externalId').notNull(),
    userId: text('userId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('ScimExternalId_organizationId_userId_idx').on(table.organizationId, table.userId),
    index('ScimExternalId_userId_idx').on(table.userId),
    index('ScimExternalId_connectionId_idx').on(table.connectionId),
    uniqueIndex('ScimExternalId_connectionId_externalId_key').on(
      table.connectionId,
      table.externalId,
    ),
    foreignKey({
      name: 'ScimExternalId_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimUserResource = pgTable(
  'ScimUserResource',
  {
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    userName: text('userName').notNull(),
    name: text('name'),
    active: boolean('active').notNull().default(true),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'ScimUserResource_pkey', columns: [table.organizationId, table.userId] }),
    index('ScimUserResource_organizationId_userName_idx').on(table.organizationId, table.userName),
    index('ScimUserResource_userId_idx').on(table.userId),
    foreignKey({
      name: 'ScimUserResource_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimDirectoryUser = pgTable(
  'ScimDirectoryUser',
  {
    organizationId: text('organizationId').notNull(),
    connectionId: text('connectionId').notNull(),
    userId: text('userId').notNull(),
  },
  (table) => [
    primaryKey({ name: 'ScimDirectoryUser_pkey', columns: [table.connectionId, table.userId] }),
    index('ScimDirectoryUser_organizationId_userId_idx').on(table.organizationId, table.userId),
  ],
)

export const scimSyncState = pgTable(
  'ScimSyncState',
  {
    id: text('id').primaryKey(),
    connectionId: text('connectionId').notNull(),
    organizationId: text('organizationId').notNull(),
    state: text('state').notNull(),
    lastPushedAt: timestamp('lastPushedAt', { precision: 3 }),
    lastFailure: jsonb('lastFailure'),
    deadLetters: jsonb('deadLetters').notNull().default([]),
    revokedCause: text('revokedCause'),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    lastEventId: text('lastEventId').notNull(),
    acceptedAt: timestamp('acceptedAt', { precision: 3 }).notNull(),
    projectionVersion: text('projectionVersion').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('ScimSyncState_organizationId_idx').on(table.organizationId),
    index('ScimSyncState_state_idx').on(table.state),
    index('ScimSyncState_connectionId_idx').on(table.connectionId),
  ],
)

export const scimSsoConnectionView = pgTable(
  'ScimSsoConnectionView',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    folded: jsonb('folded').notNull(),
    appliedEventIds: text('appliedEventIds').array().default([]),
    projectionVersion: text('projectionVersion').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('ScimSsoConnectionView_organizationId_createdAt_idx').on(
      table.organizationId,
      table.createdAt,
    ),
  ],
)

export const organizationSeatPeak = pgTable('OrganizationSeatPeak', {
  organizationId: text('organizationId').primaryKey(),
  openedAt: doublePrecision('openedAt').notNull(),
  fullSeats: integer('fullSeats').notNull(),
  liteSeats: integer('liteSeats').notNull(),
  developerSeats: integer('developerSeats').notNull(),
  latestChangeAt: doublePrecision('latestChangeAt').notNull(),
  mismatches: integer('mismatches').notNull(),
})

export const organizationSeatPeakHolder = pgTable(
  'OrganizationSeatPeakHolder',
  {
    organizationId: text('organizationId').notNull(),
    holderKind: text('holderKind').notNull(),
    holderId: text('holderId').notNull(),
    seat: text('seat').notNull(),
    lastChangedAt: doublePrecision('lastChangedAt').notNull(),
    lastChangeId: text('lastChangeId'),
    expiresAt: doublePrecision('expiresAt'),
    expiredByFold: boolean('expiredByFold').notNull().default(false),
    suppressedBy: text('suppressedBy'),
    restoredByFold: boolean('restoredByFold').notNull().default(false),
  },
  (table) => [
    primaryKey({
      name: 'OrganizationSeatPeakHolder_pkey',
      columns: [table.organizationId, table.holderKind, table.holderId],
    }),
    index('OrganizationSeatPeakHolder_organizationId_holderKind_seat_e_idx').on(
      table.organizationId,
      table.holderKind,
      table.seat,
      table.expiresAt,
    ),
    index('OrganizationSeatPeakHolder_organizationId_suppressedBy_idx').on(
      table.organizationId,
      table.suppressedBy,
    ),
  ],
)

export const organizationSeatPeakMonth = pgTable(
  'OrganizationSeatPeakMonth',
  {
    organizationId: text('organizationId').notNull(),
    month: text('month').notNull(),
    fullPeak: integer('fullPeak').notNull(),
    fullPeakAt: doublePrecision('fullPeakAt').notNull(),
    litePeak: integer('litePeak').notNull(),
    litePeakAt: doublePrecision('litePeakAt').notNull(),
    fullAtEnd: integer('fullAtEnd').notNull(),
    liteAtEnd: integer('liteAtEnd').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'OrganizationSeatPeakMonth_pkey',
      columns: [table.organizationId, table.month],
    }),
  ],
)

export const project = pgTable(
  'Project',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    apiKey: text('apiKey').notNull(),
    legacyApiKeyLastUsedAt: timestamp('legacyApiKeyLastUsedAt', { precision: 3 }),
    lwqlKey: text('lwqlKey')
      .notNull()
      .default(sql`(gen_random_uuid())::text`),
    teamId: text('teamId').notNull(),
    language: text('language').notNull(),
    framework: text('framework').notNull(),
    kind: text('kind').notNull().default('application'),
    aggregateRule: jsonb('aggregateRule'),
    firstMessage: boolean('firstMessage').notNull().default(false),
    integrated: boolean('integrated').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    userLinkTemplate: text('userLinkTemplate'),
    traceSharingEnabled: boolean('traceSharingEnabled').notNull().default(true),
    presenceEnabled: boolean('presenceEnabled').notNull().default(true),
    s3Endpoint: text('s3Endpoint'),
    s3AccessKeyId: text('s3AccessKeyId'),
    s3SecretAccessKey: text('s3SecretAccessKey'),
    s3Bucket: text('s3Bucket'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    isPersonal: boolean('isPersonal').notNull().default(false),
    ownerUserId: varchar('ownerUserId', { length: 255 }),
    personalFeatures: jsonb('personalFeatures').notNull().default({}),
    departmentId: text('departmentId'),
    langyEgressAllowlist: jsonb('langyEgressAllowlist'),
    lastCodingAgentSessionAt: timestamp('lastCodingAgentSessionAt', { precision: 3 }),
    lastCodingAgentPullRequestAt: timestamp('lastCodingAgentPullRequestAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Project_slug_key').on(table.slug),
    uniqueIndex('Project_apiKey_key').on(table.apiKey),
    uniqueIndex('Project_lwqlKey_key').on(table.lwqlKey),
    index('Project_teamId_idx').on(table.teamId),
    index('Project_isPersonal_idx').on(table.isPersonal),
    index('Project_ownerUserId_idx').on(table.ownerUserId),
    index('Project_teamId_kind_idx').on(table.teamId, table.kind),
    index('Project_departmentId_idx').on(table.departmentId),
    foreignKey({ name: 'Project_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Project_ownerUserId_fkey',
      columns: [table.ownerUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const department = pgTable(
  'Department',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
  },
  (table) => [index('Department_organizationId_idx').on(table.organizationId)],
)

export const departmentMembershipHistory = pgTable(
  'DepartmentMembershipHistory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    departmentId: text('departmentId').notNull(),
    validFrom: timestamp('validFrom', { precision: 3 }).notNull(),
    validTo: timestamp('validTo', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('DepartmentMembershipHistory_organizationId_userId_validFrom_idx').on(
      table.organizationId,
      table.userId,
      table.validFrom,
    ),
  ],
)

export const platformToolPolicy = pgTable(
  'PlatformToolPolicy',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    toolSlug: text('toolSlug').notNull(),
    allowVk: boolean('allowVk').notNull().default(true),
    allowOtelDirect: boolean('allowOtelDirect').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('PlatformToolPolicy_organizationId_idx').on(table.organizationId),
    uniqueIndex('PlatformToolPolicy_organizationId_toolSlug_key').on(
      table.organizationId,
      table.toolSlug,
    ),
  ],
)

export const organizationInvite = pgTable(
  'OrganizationInvite',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    email: text('email').notNull(),
    inviteCode: text('inviteCode').notNull(),
    expiration: timestamp('expiration', { precision: 3 }),
    status: inviteStatusEnum('status').notNull().default('PENDING'),
    organizationId: text('organizationId').notNull(),
    teamIds: text('teamIds').notNull(),
    teamAssignments: jsonb('teamAssignments'),
    role: organizationUserRoleEnum('role').notNull(),
    requestedBy: text('requestedBy'),
    subscriptionId: text('subscriptionId'),
    acceptedByUserId: text('acceptedByUserId'),
    acceptedViaIdentifierId: text('acceptedViaIdentifierId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('OrganizationInvite_inviteCode_key').on(table.inviteCode),
    index('OrganizationInvite_organizationId_idx').on(table.organizationId),
    index('OrganizationInvite_requestedBy_idx').on(table.requestedBy),
    foreignKey({
      name: 'OrganizationInvite_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganizationInvite_requestedBy_fkey',
      columns: [table.requestedBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const monitor = pgTable(
  'Monitor',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    experimentId: text('experimentId'),
    evaluatorId: text('evaluatorId'),
    checkType: text('checkType').notNull(),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    executionMode: evaluationExecutionModeEnum('executionMode').notNull().default('ON_MESSAGE'),
    enabled: boolean('enabled').notNull().default(true),
    preconditions: jsonb('preconditions').notNull(),
    parameters: jsonb('parameters').notNull(),
    mappings: jsonb('mappings'),
    sample: doublePrecision('sample').notNull().default(1),
    level: text('level').notNull().default('trace'),
    threadIdleTimeout: integer('threadIdleTimeout'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    autoPausedAt: timestamp('autoPausedAt', { precision: 3 }),
    autoPausedReason: text('autoPausedReason'),
    evaluatorLabel: text('evaluatorLabel'),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    uniqueIndex('Monitor_experimentId_key').on(table.experimentId),
    index('Monitor_projectId_idx').on(table.projectId),
    index('Monitor_evaluatorId_idx').on(table.evaluatorId),
    uniqueIndex('Monitor_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'Monitor_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Monitor_experimentId_fkey',
      columns: [table.experimentId],
      foreignColumns: [experiment.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
    foreignKey({
      name: 'Monitor_evaluatorId_fkey',
      columns: [table.evaluatorId],
      foreignColumns: [evaluator.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const cost = pgTable(
  'Cost',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    costType: costTypeEnum('costType').notNull(),
    costName: text('costName'),
    referenceType: costReferenceTypeEnum('referenceType').notNull(),
    referenceId: text('referenceId').notNull(),
    amount: doublePrecision('amount').notNull(),
    currency: text('currency').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    extraInfo: jsonb('extraInfo'),
  },
  (table) => [
    index('Cost_referenceType_referenceId_idx').on(table.referenceType, table.referenceId),
    index('Cost_costType_idx').on(table.costType),
    index('Cost_projectId_idx').on(table.projectId),
    index('Cost_projectId_createdAt_idx').on(table.projectId, table.createdAt),
    foreignKey({
      name: 'Cost_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const topic = pgTable(
  'Topic',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    parentId: text('parentId'),
    embeddings_model: text('embeddings_model').notNull(),
    centroid: jsonb('centroid').notNull(),
    p95Distance: doublePrecision('p95Distance').notNull(),
    automaticallyGenerated: boolean('automaticallyGenerated').notNull().default(true),
    lastEventId: text('lastEventId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Topic_parentId_idx').on(table.parentId),
    index('Topic_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'Topic_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Topic_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const dataset = pgTable(
  'Dataset',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    columnTypes: jsonb('columnTypes').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    mapping: jsonb('mapping'),
    useS3: boolean('useS3').notNull().default(false),
    s3RecordCount: integer('s3RecordCount'),
    contentLayout: text('contentLayout').notNull().default('postgres'),
    status: text('status').notNull().default('ready'),
    statusError: text('statusError'),
    stagingKey: text('stagingKey'),
    sourceStoredObjectId: text('sourceStoredObjectId'),
    uploadFilename: text('uploadFilename'),
    rowCount: integer('rowCount'),
    sizeBytes: bigint('sizeBytes', { mode: 'bigint' }),
    chunkCount: integer('chunkCount'),
    chunkOffsets: jsonb('chunkOffsets'),
  },
  (table) => [
    uniqueIndex('Dataset_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'Dataset_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const datasetRecord = pgTable(
  'DatasetRecord',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    datasetId: text('datasetId').notNull(),
    projectId: text('projectId').notNull(),
    entry: jsonb('entry').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DatasetRecord_datasetId_idx').on(table.datasetId),
    index('DatasetRecord_projectId_idx').on(table.projectId),
    index('DatasetRecord_createdAt_idx').on(table.createdAt),
    index('DatasetRecord_projectId_datasetId_idx').on(table.projectId, table.datasetId),
    foreignKey({
      name: 'DatasetRecord_datasetId_fkey',
      columns: [table.datasetId],
      foreignColumns: [dataset.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DatasetRecord_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const datasetVersion = pgTable(
  'DatasetVersion',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    datasetId: text('datasetId').notNull(),
    version: integer('version').notNull(),
    name: text('name'),
    trigger: text('trigger').notNull(),
    contentHash: text('contentHash').notNull(),
    columnTypes: jsonb('columnTypes').notNull(),
    rowCount: integer('rowCount').notNull(),
    sizeBytes: bigint('sizeBytes', { mode: 'bigint' }).notNull(),
    chunkCount: integer('chunkCount').notNull(),
    restoredFromVersion: integer('restoredFromVersion'),
    authorId: text('authorId'),
    authorKind: text('authorKind'),
    pinnedAt: timestamp('pinnedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('DatasetVersion_projectId_datasetId_idx').on(table.projectId, table.datasetId),
    uniqueIndex('DatasetVersion_datasetId_version_key').on(table.datasetId, table.version),
  ],
)

export const datasetVersionRun = pgTable(
  'DatasetVersionRun',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    datasetId: text('datasetId').notNull(),
    versionId: text('versionId').notNull(),
    runId: text('runId').notNull(),
    experimentId: text('experimentId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('DatasetVersionRun_versionId_idx').on(table.versionId),
    uniqueIndex('DatasetVersionRun_projectId_runId_datasetId_key').on(
      table.projectId,
      table.runId,
      table.datasetId,
    ),
  ],
)

export const dashboard = pgTable(
  'Dashboard',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    order: integer('order').notNull().default(0),
    description: text('description'),
    createdById: text('createdById'),
    scope: dashboardScopeEnum('scope').notNull().default('PROJECT'),
    organizationId: text('organizationId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Dashboard_projectId_idx').on(table.projectId),
    index('Dashboard_projectId_order_idx').on(table.projectId, table.order),
    index('Dashboard_organizationId_scope_idx').on(table.organizationId, table.scope),
    foreignKey({
      name: 'Dashboard_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const dashboardFavourite = pgTable(
  'DashboardFavourite',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    userId: text('userId').notNull(),
    dashboardId: text('dashboardId'),
    templateId: text('templateId'),
    projectId: text('projectId').notNull(),
    position: integer('position').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('DashboardFavourite_userId_projectId_position_idx').on(
      table.userId,
      table.projectId,
      table.position,
    ),
    index('DashboardFavourite_dashboardId_idx').on(table.dashboardId),
    uniqueIndex('DashboardFavourite_userId_dashboardId_key').on(table.userId, table.dashboardId),
    uniqueIndex('DashboardFavourite_userId_projectId_templateId_key').on(
      table.userId,
      table.projectId,
      table.templateId,
    ),
  ],
)

export const dashboardsV2 = pgTable(
  'DashboardsV2',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    createdById: text('createdById'),
    scope: dashboardScopeEnum('scope').notNull().default('PROJECT'),
    organizationId: text('organizationId'),
    excludedOrigins: jsonb('excludedOrigins'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DashboardsV2_projectId_idx').on(table.projectId),
    index('DashboardsV2_organizationId_scope_idx').on(table.organizationId, table.scope),
  ],
)

export const dashboardWidget = pgTable(
  'DashboardWidget',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    dashboardId: text('dashboardId'),
    name: text('name').notNull(),
    definition: jsonb('definition').notNull(),
    gridColumn: integer('gridColumn').notNull().default(0),
    gridRow: integer('gridRow').notNull().default(0),
    colSpan: integer('colSpan').notNull().default(1),
    rowSpan: integer('rowSpan').notNull().default(1),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DashboardWidget_projectId_dashboardId_idx').on(table.projectId, table.dashboardId),
  ],
)

export const insightProjection = pgTable(
  'InsightProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    title: text('title').notNull(),
    body: text('body').notNull(),
    tone: text('tone').notNull(),
    topic: text('topic'),
    validDays: integer('validDays').notNull(),
    lwql: text('lwql'),
    sourceConversationId: text('sourceConversationId'),
    sourceMessageId: text('sourceMessageId'),
    filedByUserId: text('filedByUserId'),
    filedAt: doublePrecision('filedAt').notNull(),
    renewedAt: doublePrecision('renewedAt'),
    createdAt: doublePrecision('createdAt').notNull(),
    updatedAt: doublePrecision('updatedAt').notNull(),
    occurredAt: doublePrecision('occurredAt').notNull(),
    acceptedAt: doublePrecision('acceptedAt').notNull(),
    lastEventId: text('lastEventId').notNull(),
    projectionVersion: text('projectionVersion').notNull(),
    boardId: text('boardId'),
    boardName: text('boardName'),
    widgetId: text('widgetId'),
    widgetName: text('widgetName'),
    filedVia: text('filedVia').notNull().default('chat'),
    replayStart: doublePrecision('replayStart'),
    replayEnd: doublePrecision('replayEnd'),
    replayGranularitySeconds: integer('replayGranularitySeconds'),
    replayContext: jsonb('replayContext'),
    ownerUserId: text('ownerUserId'),
    findingKey: text('findingKey'),
  },
  (table) => [
    index('InsightProjection_projectId_filedAt_idx').on(table.projectId, table.filedAt),
    index('InsightProjection_projectId_ownerUserId_filedAt_idx').on(
      table.projectId,
      table.ownerUserId,
      table.filedAt,
    ),
  ],
)

export const insightReaderProjection = pgTable(
  'InsightReaderProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    insightId: text('insightId').notNull(),
    userId: text('userId').notNull(),
    seenAt: doublePrecision('seenAt'),
    archivedAt: doublePrecision('archivedAt'),
    keptAt: doublePrecision('keptAt'),
    createdAt: doublePrecision('createdAt').notNull(),
    updatedAt: doublePrecision('updatedAt').notNull(),
    occurredAt: doublePrecision('occurredAt').notNull(),
    acceptedAt: doublePrecision('acceptedAt').notNull(),
    lastEventId: text('lastEventId').notNull(),
    projectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [
    index('InsightReaderProjection_projectId_userId_idx').on(table.projectId, table.userId),
    uniqueIndex('InsightReaderProjection_projectId_insightId_userId_key').on(
      table.projectId,
      table.insightId,
      table.userId,
    ),
  ],
)

export const insightDailyScheduleProjection = pgTable(
  'InsightDailyScheduleProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    userId: text('userId').notNull(),
    boardKind: text('boardKind').notNull(),
    boardId: text('boardId').notNull(),
    boardName: text('boardName').notNull(),
    state: text('state').notNull().default('undecided'),
    offReason: text('offReason'),
    hour: integer('hour'),
    timezone: text('timezone'),
    maxInsights: integer('maxInsights'),
    lastRunId: text('lastRunId'),
    lastRunAt: doublePrecision('lastRunAt'),
    lastRunOutcome: text('lastRunOutcome'),
    lastRunReason: text('lastRunReason'),
    lastRunFiled: integer('lastRunFiled'),
    lastRunRenewed: integer('lastRunRenewed'),
    lastRunConversationId: text('lastRunConversationId'),
    consecutiveSkips: integer('consecutiveSkips').notNull().default(0),
    skipStreakSlot: doublePrecision('skipStreakSlot'),
    createdAt: doublePrecision('createdAt').notNull(),
    updatedAt: doublePrecision('updatedAt').notNull(),
    occurredAt: doublePrecision('occurredAt').notNull(),
    acceptedAt: doublePrecision('acceptedAt').notNull(),
    lastEventId: text('lastEventId').notNull(),
    projectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [
    uniqueIndex('InsightDailySchedule_projectId_userId_board_key').on(
      table.projectId,
      table.userId,
      table.boardKind,
      table.boardId,
    ),
  ],
)

export const savedView = pgTable(
  'SavedView',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    userId: text('userId'),
    name: text('name').notNull(),
    filters: jsonb('filters').notNull().default({}),
    query: text('query'),
    period: jsonb('period'),
    order: integer('order').notNull().default(0),
    kind: text('kind').notNull().default('v1-traces-filter'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('SavedView_projectId_idx').on(table.projectId),
    index('SavedView_projectId_order_idx').on(table.projectId, table.order),
    index('SavedView_userId_idx').on(table.userId),
    index('SavedView_projectId_kind_order_idx').on(table.projectId, table.kind, table.order),
    foreignKey({
      name: 'SavedView_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SavedView_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const customGraph = pgTable(
  'CustomGraph',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    graph: jsonb('graph').notNull(),
    filters: jsonb('filters'),
    kind: text('kind').notNull().default('builder'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    dashboardId: text('dashboardId'),
    gridColumn: integer('gridColumn').notNull().default(0),
    gridRow: integer('gridRow').notNull().default(0),
    colSpan: integer('colSpan').notNull().default(1),
    rowSpan: integer('rowSpan').notNull().default(1),
  },
  (table) => [
    index('CustomGraph_projectId_idx').on(table.projectId),
    index('CustomGraph_dashboardId_idx').on(table.dashboardId),
    index('CustomGraph_projectId_kind_idx').on(table.projectId, table.kind),
    foreignKey({
      name: 'CustomGraph_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CustomGraph_dashboardId_fkey',
      columns: [table.dashboardId],
      foreignColumns: [dashboard.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const batchEvaluation = pgTable(
  'BatchEvaluation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    experimentId: text('experimentId').notNull(),
    projectId: text('projectId').notNull(),
    data: jsonb('data').notNull().default({}),
    status: text('status').notNull(),
    score: doublePrecision('score').notNull(),
    label: text('label'),
    passed: boolean('passed').notNull(),
    details: text('details').notNull(),
    cost: doublePrecision('cost').notNull(),
    datasetSlug: text('datasetSlug').notNull(),
    datasetId: text('datasetId').notNull(),
    evaluation: text('evaluation').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('BatchEvaluation_projectId_idx').on(table.projectId),
    index('BatchEvaluation_datasetId_idx').on(table.datasetId),
    index('BatchEvaluation_experimentId_idx').on(table.experimentId),
    uniqueIndex('BatchEvaluation_id_key').on(table.id),
    foreignKey({
      name: 'BatchEvaluation_experimentId_fkey',
      columns: [table.experimentId],
      foreignColumns: [experiment.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BatchEvaluation_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BatchEvaluation_datasetId_fkey',
      columns: [table.datasetId],
      foreignColumns: [dataset.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const trigger = pgTable(
  'Trigger',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    projectId: text('projectId').notNull(),
    action: triggerActionEnum('action').notNull(),
    triggerKind: triggerKindEnum('triggerKind').notNull().default('AUTOMATION'),
    actionParams: jsonb('actionParams').notNull(),
    filters: jsonb('filters').notNull(),
    filterQuery: text('filterQuery'),
    lastRunAt: doublePrecision('lastRunAt').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    active: boolean('active').notNull().default(true),
    pausedReason: text('pausedReason'),
    pausedAt: timestamp('pausedAt', { withTimezone: true, precision: 3 }),
    message: text('message'),
    deleted: boolean('deleted').notNull().default(false),
    alertType: alertTypeEnum('alertType'),
    slackTemplateType: text('slackTemplateType'),
    slackTemplate: text('slackTemplate'),
    emailSubjectTemplate: text('emailSubjectTemplate'),
    emailBodyTemplate: text('emailBodyTemplate'),
    notificationCadence: text('notificationCadence').notNull().default('immediate'),
    traceDebounceMs: integer('traceDebounceMs').notNull().default(30000),
    customGraphId: text('customGraphId'),
  },
  (table) => [
    uniqueIndex('Trigger_customGraphId_key').on(table.customGraphId),
    index('Trigger_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'Trigger_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Trigger_customGraphId_fkey',
      columns: [table.customGraphId],
      foreignColumns: [customGraph.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const slackIntegration = pgTable(
  'SlackIntegration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    kind: slackIntegrationKindEnum('kind').notNull(),
    scopeType: slackIntegrationScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    organizationId: text('organizationId').notNull(),
    botTokenEncrypted: text('botTokenEncrypted'),
    webhookUrlEncrypted: text('webhookUrlEncrypted'),
    secretFingerprint: text('secretFingerprint').notNull(),
    secretHint: text('secretHint').notNull(),
    slackTeamId: text('slackTeamId'),
    slackTeamName: text('slackTeamName'),
    createdById: text('createdById').notNull(),
    updatedById: text('updatedById').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('SlackIntegration_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    uniqueIndex('SlackIntegration_organizationId_scopeType_scopeId_secretFin_key').on(
      table.organizationId,
      table.scopeType,
      table.scopeId,
      table.secretFingerprint,
    ),
  ],
)

export const slackConnectionClaim = pgTable(
  'slack_connection_claim',
  {
    connectionId: text('connectionId').notNull(),
    claimantId: text('claimantId').notNull(),
    claimantLabel: text('claimantLabel').notNull(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'slack_connection_claim_pkey',
      columns: [table.connectionId, table.claimantId],
    }),
    index('slack_connection_claim_organizationId_idx').on(table.organizationId),
  ],
)

export const webhookEndpoint = pgTable(
  'WebhookEndpoint',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    destinationKind: webhookDestinationKindEnum('destinationKind').notNull().default('http'),
    url: text('url'),
    sqsQueueUrl: text('sqsQueueUrl'),
    sqsRoleArn: text('sqsRoleArn'),
    sqsExternalId: text('sqsExternalId'),
    sqsAccessKeyId: text('sqsAccessKeyId'),
    sqsSecretAccessKeyEncrypted: text('sqsSecretAccessKeyEncrypted'),
    secretEncrypted: text('secretEncrypted').notNull(),
    signatureScheme: text('signatureScheme'),
    idempotencyKey: text('idempotencyKey'),
    previousSecretEncrypted: text('previousSecretEncrypted'),
    previousSecretExpiresAt: timestamp('previousSecretExpiresAt', { precision: 3 }),
    enabledEvents: text('enabledEvents').array().default([]),
    status: webhookEndpointStatusEnum('status').notNull().default('ACTIVE'),
    disabledReason: text('disabledReason'),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    failingSince: timestamp('failingSince', { precision: 3 }),
    maxBatchSize: integer('maxBatchSize').notNull().default(100),
    maxBatchDelayMs: integer('maxBatchDelayMs').notNull().default(250),
    maxInFlight: integer('maxInFlight').notNull().default(4),
    allowSelfSignedCertificate: boolean('allowSelfSignedCertificate').notNull().default(false),
    lastSuccessAt: timestamp('lastSuccessAt', { precision: 3 }),
    lastFailureAt: timestamp('lastFailureAt', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('WebhookEndpoint_organizationId_status_idx').on(table.organizationId, table.status),
    uniqueIndex('WebhookEndpoint_organizationId_idempotencyKey_key').on(
      table.organizationId,
      table.idempotencyKey,
    ),
    foreignKey({
      name: 'WebhookEndpoint_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhookEndpointDelivery = pgTable(
  'WebhookEndpointDelivery',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    channel: webhookDeliveryChannelEnum('channel').notNull().default('platform'),
    organizationId: text('organizationId'),
    endpointId: text('endpointId'),
    projectId: text('projectId'),
    triggerId: text('triggerId'),
    dispatchId: text('dispatchId').notNull(),
    attempt: integer('attempt'),
    eventCount: integer('eventCount'),
    responseStatus: integer('responseStatus'),
    latencyMs: integer('latencyMs'),
    error: text('error'),
    response: jsonb('response'),
    outcome: webhookDeliveryOutcomeEnum('outcome').notNull(),
    firedAt: timestamp('firedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('WebhookEndpointDelivery_endpointId_firedAt_idx').on(table.endpointId, table.firedAt),
    index('WebhookEndpointDelivery_organizationId_firedAt_idx').on(
      table.organizationId,
      table.firedAt,
    ),
    index('WebhookEndpointDelivery_firedAt_idx').on(table.firedAt),
    index('WebhookEndpointDelivery_endpointId_dispatchId_idx').on(
      table.endpointId,
      table.dispatchId,
    ),
    index('WebhookEndpointDelivery_projectId_triggerId_firedAt_idx').on(
      table.projectId,
      table.triggerId,
      table.firedAt,
    ),
    index('WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx').on(
      table.projectId,
      table.triggerId,
      table.dispatchId,
    ),
    foreignKey({
      name: 'WebhookEndpointDelivery_endpointId_fkey',
      columns: [table.endpointId],
      foreignColumns: [webhookEndpoint.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WebhookEndpointDelivery_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WebhookEndpointDelivery_triggerId_fkey',
      columns: [table.triggerId],
      foreignColumns: [trigger.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const experiment = pgTable(
  'Experiment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name'),
    type: experimentTypeEnum('type').notNull(),
    slug: text('slug').notNull(),
    projectId: text('projectId').notNull(),
    workflowId: text('workflowId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    workbenchState: jsonb('workbenchState'),
    workbenchVersion: integer('workbenchVersion').notNull().default(0),
  },
  (table) => [
    index('Experiment_projectId_idx').on(table.projectId),
    index('Experiment_workflowId_idx').on(table.workflowId),
    index('Experiment_projectId_archivedAt_idx').on(table.projectId, table.archivedAt),
    uniqueIndex('Experiment_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'Experiment_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Experiment_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const experimentVersion = pgTable(
  'ExperimentVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    experimentId: text('experimentId').notNull(),
    projectId: text('projectId').notNull(),
    version: integer('version').notNull(),
    counterVersion: integer('counterVersion').notNull(),
    autoSaved: boolean('autoSaved').notNull().default(false),
    commitMessage: text('commitMessage'),
    authorId: text('authorId'),
    authorLabel: text('authorLabel').notNull(),
    runId: text('runId'),
    state: jsonb('state').notNull(),
    schemaVersion: text('schemaVersion').notNull().default('1'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('ExperimentVersion_projectId_experimentId_autoSaved_idx').on(
      table.projectId,
      table.experimentId,
      table.autoSaved,
    ),
    index('ExperimentVersion_projectId_experimentId_version_idx').on(
      table.projectId,
      table.experimentId,
      table.version,
    ),
    index('ExperimentVersion_projectId_experimentId_counterVersion_idx').on(
      table.projectId,
      table.experimentId,
      table.counterVersion,
    ),
    index('ExperimentVersion_createdAt_idx').on(table.createdAt),
    uniqueIndex('ExperimentVersion_experimentId_version_key').on(table.experimentId, table.version),
  ],
)

export const annotation = pgTable(
  'Annotation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    traceId: text('traceId').notNull(),
    comment: text('comment'),
    isThumbsUp: boolean('isThumbsUp'),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    email: text('email'),
    scoreOptions: jsonb('scoreOptions'),
    expectedOutput: text('expectedOutput'),
    anchorKind: text('anchorKind'),
    anchorId: text('anchorId'),
    anchorPath: text('anchorPath'),
  },
  (table) => [
    index('Annotation_projectId_idx').on(table.projectId),
    index('Annotation_traceId_idx').on(table.traceId),
    index('Annotation_userId_idx').on(table.userId),
    index('Annotation_projectId_traceId_idx').on(table.projectId, table.traceId),
    index('Annotation_projectId_traceId_anchorKind_idx').on(
      table.projectId,
      table.traceId,
      table.anchorKind,
    ),
    foreignKey({
      name: 'Annotation_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Annotation_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const modelProvider = pgTable(
  'ModelProvider',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    provider: text('provider').notNull(),
    routingHandle: text('routingHandle'),
    enabled: boolean('enabled').notNull(),
    customKeys: jsonb('customKeys'),
    extraHeaders: jsonb('extraHeaders'),
    customModels: jsonb('customModels'),
    customEmbeddingsModels: jsonb('customEmbeddingsModels'),
    deploymentMapping: jsonb('deploymentMapping'),
    rateLimitRpm: integer('rateLimitRpm'),
    rateLimitTpm: integer('rateLimitTpm'),
    rateLimitRpd: integer('rateLimitRpd'),
    rotationPolicy: gatewayProviderRotationPolicyEnum('rotationPolicy').notNull().default('MANUAL'),
    providerConfig: jsonb('providerConfig'),
    fallbackPriorityGlobal: integer('fallbackPriorityGlobal'),
    langySkipPermissionsModels: jsonb('langySkipPermissionsModels'),
    healthStatus: gatewayProviderHealthStatusEnum('healthStatus').notNull().default('UNKNOWN'),
    circuitOpenedAt: timestamp('circuitOpenedAt', { precision: 3 }),
    lastHealthCheckAt: timestamp('lastHealthCheckAt', { precision: 3 }),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    organizationId: text('organizationId').notNull(),
  },
  (table) => [
    index('ModelProvider_healthStatus_idx').on(table.healthStatus),
    index('ModelProvider_fallbackPriorityGlobal_idx').on(table.fallbackPriorityGlobal),
    index('ModelProvider_organizationId_idx').on(table.organizationId),
    uniqueIndex('ModelProvider_organizationId_routingHandle_key').on(
      table.organizationId,
      table.routingHandle,
    ),
  ],
)

export const modelProviderScope = pgTable(
  'ModelProviderScope',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    modelProviderId: text('modelProviderId').notNull(),
    scopeType: modelProviderScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ModelProviderScope_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    index('ModelProviderScope_modelProviderId_idx').on(table.modelProviderId),
    uniqueIndex('ModelProviderScope_modelProviderId_scopeType_scopeId_key').on(
      table.modelProviderId,
      table.scopeType,
      table.scopeId,
    ),
    foreignKey({
      name: 'ModelProviderScope_modelProviderId_fkey',
      columns: [table.modelProviderId],
      foreignColumns: [modelProvider.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const modelDefaultConfig = pgTable(
  'ModelDefaultConfig',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    config: jsonb('config').notNull(),
    authorId: text('authorId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationId: text('organizationId').notNull(),
  },
  (table) => [index('ModelDefaultConfig_organizationId_idx').on(table.organizationId)],
)

export const modelDefaultConfigScope = pgTable(
  'ModelDefaultConfigScope',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    configId: text('configId').notNull(),
    scopeType: modelDefaultScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ModelDefaultConfigScope_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    index('ModelDefaultConfigScope_configId_idx').on(table.configId),
    uniqueIndex('ModelDefaultConfigScope_configId_scopeType_scopeId_key').on(
      table.configId,
      table.scopeType,
      table.scopeId,
    ),
    foreignKey({
      name: 'ModelDefaultConfigScope_configId_fkey',
      columns: [table.configId],
      foreignColumns: [modelDefaultConfig.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const githubInstallation = pgTable(
  'GithubInstallation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    installationId: text('installationId').notNull(),
    organizationId: text('organizationId').notNull(),
    accountLogin: text('accountLogin').notNull(),
    accountType: text('accountType').notNull(),
    accountId: text('accountId').notNull(),
    repositorySelection: text('repositorySelection').notNull(),
    repositories: jsonb('repositories'),
    suspendedAt: timestamp('suspendedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('GithubInstallation_installationId_key').on(table.installationId),
    index('GithubInstallation_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'GithubInstallation_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const githubPullRequest = pgTable(
  'GithubPullRequest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    repositoryHost: text('repositoryHost').notNull(),
    repositoryFullName: text('repositoryFullName').notNull(),
    headBranch: text('headBranch').notNull(),
    prNumber: integer('prNumber').notNull(),
    htmlUrl: text('htmlUrl').notNull(),
    title: text('title').notNull(),
    state: text('state').notNull(),
    isDraft: boolean('isDraft').notNull().default(false),
    authorLogin: text('authorLogin'),
    prCreatedAt: timestamp('prCreatedAt', { precision: 3 }).notNull(),
    prClosedAt: timestamp('prClosedAt', { precision: 3 }),
    prMergedAt: timestamp('prMergedAt', { precision: 3 }),
    prUpdatedAt: timestamp('prUpdatedAt', { precision: 3 }),
    mappedAt: timestamp('mappedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastCheckedAt: timestamp('lastCheckedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('GithubPullRequest_organizationId_repositoryHost_repositoryF_idx').on(
      table.organizationId,
      table.repositoryHost,
      table.repositoryFullName,
      table.headBranch,
    ),
    uniqueIndex('GithubPullRequest_organizationId_repositoryHost_repositoryF_key').on(
      table.organizationId,
      table.repositoryHost,
      table.repositoryFullName,
      table.prNumber,
    ),
    foreignKey({
      name: 'GithubPullRequest_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const githubBranchPullRequestCheck = pgTable(
  'GithubBranchPullRequestCheck',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    repositoryHost: text('repositoryHost').notNull(),
    repositoryFullName: text('repositoryFullName').notNull(),
    headBranch: text('headBranch').notNull(),
    lastCheckedAt: timestamp('lastCheckedAt', { precision: 3 }).notNull(),
    prCount: integer('prCount').notNull().default(0),
    notFoundAt: timestamp('notFoundAt', { precision: 3 }),
    recheckAfter: timestamp('recheckAfter', { precision: 3 }),
    attempts: integer('attempts').notNull().default(0),
    lastRequestedAt: timestamp('lastRequestedAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_n_idx').on(
      table.recheckAfter,
      table.lastRequestedAt,
      table.notFoundAt,
    ),
    uniqueIndex('GithubBranchPullRequestCheck_organizationId_repositoryHost__key').on(
      table.organizationId,
      table.repositoryHost,
      table.repositoryFullName,
      table.headBranch,
    ),
    foreignKey({
      name: 'GithubBranchPullRequestCheck_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const langyConversationProjection = pgTable(
  'LangyConversationProjection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    ConversationId: text('conversationId').notNull(),
    UserId: text('userId').notNull(),
    Title: text('title'),
    TitleSource: langyProjectionTitleSourceEnum('titleSource').notNull(),
    Origin: text('origin').notNull().default('interactive'),
    Status: text('status').notNull(),
    IsShared: boolean('isShared').notNull().default(false),
    SharedAt: doublePrecision('sharedAt'),
    SharedById: text('sharedById'),
    MessageCount: integer('messageCount').notNull().default(0),
    LastActivityAt: doublePrecision('lastActivityAt'),
    CurrentTurnId: text('currentTurnId'),
    LastError: text('lastError'),
    LastModel: text('lastModel'),
    PendingHandoffToken: text('pendingHandoffToken'),
    PendingHandoffTurnId: text('pendingHandoffTurnId'),
    RunToken: text('runToken'),
    ArchivedAt: doublePrecision('archivedAt'),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [
    index('LangyConversationProjection_projectId_userId_conversationId_idx').on(
      table.projectId,
      table.UserId,
      table.ConversationId,
    ),
    index('LangyConversationProjection_projectId_userId_lastActivityAt_idx').on(
      table.projectId,
      table.UserId,
      table.LastActivityAt,
    ),
    uniqueIndex('LangyConversationProjection_projectId_conversationId_key').on(
      table.projectId,
      table.ConversationId,
    ),
  ],
)

export const langyTurnRequest = pgTable(
  'LangyTurnRequest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    userId: text('userId').notNull(),
    requestId: text('requestId').notNull(),
    conversationId: text('conversationId').notNull(),
    turnId: text('turnId').notNull(),
    status: text('status').notNull(),
    leaseOwner: text('leaseOwner').notNull(),
    leaseExpiresAt: timestamp('leaseExpiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('LangyTurnRequest_projectId_conversationId_idx').on(
      table.projectId,
      table.conversationId,
    ),
    uniqueIndex('LangyTurnRequest_projectId_userId_requestId_key').on(
      table.projectId,
      table.userId,
      table.requestId,
    ),
  ],
)

export const langyActiveTurn = pgTable(
  'LangyActiveTurn',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    conversationId: text('conversationId').notNull(),
    turnId: text('turnId').notNull(),
    requestId: text('requestId').notNull(),
    userId: text('userId').notNull(),
    status: text('status').notNull(),
    leaseOwner: text('leaseOwner').notNull(),
    leaseExpiresAt: timestamp('leaseExpiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('LangyActiveTurn_projectId_userId_idx').on(table.projectId, table.userId),
    uniqueIndex('LangyActiveTurn_projectId_conversationId_key').on(
      table.projectId,
      table.conversationId,
    ),
  ],
)

export const langyConversationTurnProjection = pgTable(
  'LangyConversationTurnProjection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    ConversationId: text('conversationId').notNull(),
    TurnId: text('turnId').notNull(),
    Status: text('status').notNull(),
    QuestionParts: jsonb('questionParts').notNull(),
    AnswerParts: jsonb('answerParts').notNull(),
    ToolCalls: jsonb('toolCalls').notNull(),
    Plan: jsonb('plan'),
    Error: text('error'),
    StartedAt: doublePrecision('startedAt'),
    EndedAt: doublePrecision('endedAt'),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [
    index('LangyConversationTurnProjection_projectId_conversationId_up_idx').on(
      table.projectId,
      table.ConversationId,
      table.UpdatedAt,
    ),
    uniqueIndex('LangyConversationTurnProjection_projectId_conversationId_tu_key').on(
      table.projectId,
      table.ConversationId,
      table.TurnId,
    ),
  ],
)

export const langyMessageProjection = pgTable(
  'LangyMessageProjection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    ConversationId: text('conversationId').notNull(),
    MessageId: text('messageId').notNull(),
    Role: text('role').notNull(),
    Parts: jsonb('parts').notNull(),
    SourceEventId: text('sourceEventId').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
  },
  (table) => [
    index('LangyMessageProjection_projectId_conversationId_createdAt_idx').on(
      table.projectId,
      table.ConversationId,
      table.CreatedAt,
    ),
    uniqueIndex('LangyMessageProjection_projectId_conversationId_messageId_key').on(
      table.projectId,
      table.ConversationId,
      table.MessageId,
    ),
    uniqueIndex('LangyMessageProjection_projectId_sourceEventId_key').on(
      table.projectId,
      table.SourceEventId,
    ),
  ],
)

export const projectSecret = pgTable(
  'ProjectSecret',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    encryptedValue: text('encryptedValue').notNull(),
    createdById: text('createdById').notNull(),
    updatedById: text('updatedById').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('ProjectSecret_projectId_idx').on(table.projectId),
    uniqueIndex('ProjectSecret_projectId_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'ProjectSecret_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ProjectSecret_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ProjectSecret_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const triggerSent = pgTable(
  'TriggerSent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    triggerId: text('triggerId').notNull(),
    traceId: text('traceId'),
    customGraphId: text('customGraphId'),
    projectId: text('projectId').notNull(),
    resolvedAt: timestamp('resolvedAt', { precision: 3 }),
    openIncidentKey: text('openIncidentKey'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('TriggerSent_openIncidentKey_key').on(table.openIncidentKey),
    index('TriggerSent_triggerId_idx').on(table.triggerId),
    index('TriggerSent_projectId_idx').on(table.projectId),
    index('TriggerSent_customGraphId_idx').on(table.customGraphId),
    index('TriggerSent_resolvedAt_idx').on(table.resolvedAt),
    index('TriggerSent_projectId_triggerId_createdAt_id_idx').on(
      table.projectId,
      table.triggerId,
      table.createdAt,
      table.id,
    ),
    uniqueIndex('TriggerSent_triggerId_traceId_key').on(table.triggerId, table.traceId),
    foreignKey({
      name: 'TriggerSent_customGraphId_fkey',
      columns: [table.customGraphId],
      foreignColumns: [customGraph.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TriggerSent_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TriggerSent_triggerId_fkey',
      columns: [table.triggerId],
      foreignColumns: [trigger.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const triggerLatestEvaluation = pgTable(
  'TriggerLatestEvaluation',
  {
    triggerId: text('triggerId').primaryKey(),
    projectId: text('projectId').notNull(),
    evaluatedAt: timestamp('evaluatedAt', { withTimezone: true, precision: 3 }).notNull(),
    verdict: text('verdict').notNull(),
    observedValue: doublePrecision('observedValue'),
    threshold: doublePrecision('threshold'),
    operator: text('operator'),
    timePeriodMinutes: integer('timePeriodMinutes'),
    skipCode: text('skipCode'),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('TriggerLatestEvaluation_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'TriggerLatestEvaluation_triggerId_fkey',
      columns: [table.triggerId],
      foreignColumns: [trigger.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailSuppression = pgTable(
  'EmailSuppression',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    email: text('email').notNull(),
    triggerId: text('triggerId'),
    reason: text('reason').notNull().default('unsubscribe'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('EmailSuppression_projectId_idx').on(table.projectId),
    uniqueIndex('EmailSuppression_projectId_email_triggerId_key').on(
      table.projectId,
      table.email,
      table.triggerId,
    ),
    foreignKey({
      name: 'EmailSuppression_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotationScore = pgTable(
  'AnnotationScore',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    description: text('description'),
    active: boolean('active').notNull().default(true),
    dataType: annotationScoreDataTypeEnum('dataType'),
    options: jsonb('options'),
    defaultValue: jsonb('defaultValue'),
    global: boolean('global').notNull().default(false),
  },
  (table) => [
    index('AnnotationScore_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'AnnotationScore_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotationQueue = pgTable(
  'AnnotationQueue',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    projectId: text('projectId').notNull(),
    description: text('description'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('AnnotationQueue_projectId_idx').on(table.projectId),
    uniqueIndex('AnnotationQueue_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'AnnotationQueue_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotationQueueMembers = pgTable(
  'AnnotationQueueMembers',
  { annotationQueueId: text('annotationQueueId').notNull(), userId: text('userId').notNull() },
  (table) => [
    primaryKey({
      name: 'AnnotationQueueMembers_pkey',
      columns: [table.annotationQueueId, table.userId],
    }),
    index('AnnotationQueueMembers_annotationQueueId_idx').on(table.annotationQueueId),
    index('AnnotationQueueMembers_userId_idx').on(table.userId),
    foreignKey({
      name: 'AnnotationQueueMembers_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnnotationQueueMembers_annotationQueueId_fkey',
      columns: [table.annotationQueueId],
      foreignColumns: [annotationQueue.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotationQueueScores = pgTable(
  'AnnotationQueueScores',
  {
    annotationQueueId: text('annotationQueueId').notNull(),
    annotationScoreId: text('annotationScoreId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'AnnotationQueueScores_pkey',
      columns: [table.annotationQueueId, table.annotationScoreId],
    }),
    index('AnnotationQueueScores_annotationQueueId_idx').on(table.annotationQueueId),
    index('AnnotationQueueScores_annotationScoreId_idx').on(table.annotationScoreId),
    foreignKey({
      name: 'AnnotationQueueScores_annotationScoreId_fkey',
      columns: [table.annotationScoreId],
      foreignColumns: [annotationScore.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnnotationQueueScores_annotationQueueId_fkey',
      columns: [table.annotationQueueId],
      foreignColumns: [annotationQueue.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotationQueueItem = pgTable(
  'AnnotationQueueItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    annotationQueueId: text('annotationQueueId'),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdByUserId: text('createdByUserId'),
    traceId: text('traceId').notNull(),
    projectId: text('projectId').notNull(),
    doneAt: timestamp('doneAt', { precision: 3 }),
    markedForDatasetAt: timestamp('markedForDatasetAt', { precision: 3 }),
  },
  (table) => [
    index('AnnotationQueueItem_annotationQueueId_idx').on(table.annotationQueueId),
    index('AnnotationQueueItem_userId_idx').on(table.userId),
    index('AnnotationQueueItem_projectId_idx').on(table.projectId),
    index('AnnotationQueueItem_projectId_doneAt_idx').on(table.projectId, table.doneAt),
    index('AnnotationQueueItem_projectId_userId_doneAt_idx').on(
      table.projectId,
      table.userId,
      table.doneAt,
    ),
    index('AnnotationQueueItem_annotationQueueId_doneAt_idx').on(
      table.annotationQueueId,
      table.doneAt,
    ),
    uniqueIndex('AnnotationQueueItem_traceId_userId_projectId_key').on(
      table.traceId,
      table.userId,
      table.projectId,
    ),
    uniqueIndex('AnnotationQueueItem_traceId_annotationQueueId_projectId_key').on(
      table.traceId,
      table.annotationQueueId,
      table.projectId,
    ),
    foreignKey({
      name: 'AnnotationQueueItem_annotationQueueId_fkey',
      columns: [table.annotationQueueId],
      foreignColumns: [annotationQueue.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnnotationQueueItem_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnnotationQueueItem_createdByUserId_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnnotationQueueItem_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const shareLink = pgTable(
  'ShareLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    token: text('token').notNull(),
    resourceType: shareResourceTypeEnum('resourceType').notNull(),
    resourceId: text('resourceId').notNull(),
    threadId: text('threadId'),
    projectId: text('projectId').notNull(),
    userId: text('userId'),
    visibility: shareVisibilityEnum('visibility').notNull().default('PUBLIC'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    maxViews: integer('maxViews'),
    viewCount: integer('viewCount').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ShareLink_token_key').on(table.token),
    index('ShareLink_projectId_resourceType_resourceId_idx').on(
      table.projectId,
      table.resourceType,
      table.resourceId,
    ),
    index('ShareLink_userId_idx').on(table.userId),
    foreignKey({
      name: 'ShareLink_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ShareLink_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const pinnedTrace = pgTable(
  'PinnedTrace',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    traceId: text('traceId').notNull(),
    userId: text('userId'),
    source: pinSourceEnum('source').notNull().default('manual'),
    reason: text('reason'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('PinnedTrace_projectId_idx').on(table.projectId),
    uniqueIndex('PinnedTrace_projectId_traceId_key').on(table.projectId, table.traceId),
    foreignKey({
      name: 'PinnedTrace_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PinnedTrace_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const traceEditOverlay = pgTable(
  'TraceEditOverlay',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    traceId: text('traceId').notNull(),
    patch: jsonb('patch').notNull(),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('TraceEditOverlay_projectId_idx').on(table.projectId),
    uniqueIndex('TraceEditOverlay_projectId_traceId_key').on(table.projectId, table.traceId),
    foreignKey({
      name: 'TraceEditOverlay_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TraceEditOverlay_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TraceEditOverlay_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const traceIngestSourceBilling = pgTable(
  'TraceIngestSourceBilling',
  {
    organizationId: text('organizationId').notNull(),
    sourceType: text('sourceType').notNull(),
    billed: boolean('billed').notNull(),
    recordedAt: timestamp('recordedAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'TraceIngestSourceBilling_pkey',
      columns: [table.organizationId, table.sourceType],
    }),
  ],
)

export const retentionPolicy = pgTable(
  'RetentionPolicy',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scopeType: retentionPolicyScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    category: text('category').notNull(),
    retentionDays: integer('retentionDays').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('RetentionPolicy_organizationId_idx').on(table.organizationId),
    index('RetentionPolicy_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    uniqueIndex('RetentionPolicy_scopeType_scopeId_category_key').on(
      table.scopeType,
      table.scopeId,
      table.category,
    ),
  ],
)

export const dataPrivacyPolicy = pgTable(
  'DataPrivacyPolicy',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scopeType: dataPrivacyScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    personalOnly: boolean('personalOnly').notNull().default(false),
    config: jsonb('config').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DataPrivacyPolicy_organizationId_idx').on(table.organizationId),
    index('DataPrivacyPolicy_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    uniqueIndex('DataPrivacyPolicy_scopeType_scopeId_personalOnly_key').on(
      table.scopeType,
      table.scopeId,
      table.personalOnly,
    ),
  ],
)

export const instantEvalJudgeProject = pgTable('InstantEvalJudgeProject', {
  projectId: text('projectId').primaryKey(),
  organizationId: text('organizationId').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
})

export const instantEvalJudgeUsageBilling = pgTable('InstantEvalJudgeUsageBilling', {
  organizationId: text('organizationId').primaryKey(),
  usageBilled: boolean('usageBilled').notNull(),
  occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
  fromCatchUp: boolean('fromCatchUp').notNull(),
})

export const instantEvalJudgeSpend = pgTable(
  'InstantEvalJudgeSpend',
  {
    organizationId: text('organizationId').notNull(),
    requestId: text('requestId').notNull(),
    spendNanoUsd: bigint('spendNanoUsd', { mode: 'bigint' }).notNull(),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
  },
  (table) => [
    primaryKey({
      name: 'InstantEvalJudgeSpend_pkey',
      columns: [table.organizationId, table.requestId],
    }),
  ],
)

export const customLLMModelCost = pgTable(
  'CustomLLMModelCost',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scopeType: customLlmmodelCostScopeTypeEnum('scopeType').notNull().default('PROJECT'),
    scopeId: text('scopeId').notNull(),
    projectId: text('projectId'),
    model: text('model').notNull(),
    regex: text('regex').notNull(),
    inputCostPerToken: doublePrecision('inputCostPerToken'),
    outputCostPerToken: doublePrecision('outputCostPerToken'),
    cacheReadCostPerToken: doublePrecision('cacheReadCostPerToken'),
    cacheCreationCostPerToken: doublePrecision('cacheCreationCostPerToken'),
    cacheCreation1hCostPerToken: doublePrecision('cacheCreation1hCostPerToken'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('CustomLLMModelCost_organizationId_idx').on(table.organizationId),
    index('CustomLLMModelCost_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    index('CustomLLMModelCost_projectId_idx').on(table.projectId),
  ],
)

export const workflow = pgTable(
  'Workflow',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    icon: text('icon').notNull(),
    description: text('description').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    latestVersionId: text('latestVersionId'),
    currentVersionId: text('currentVersionId'),
    publishedId: text('publishedId'),
    publishedById: text('publishedById'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    isEvaluator: boolean('isEvaluator').notNull().default(false),
    isComponent: boolean('isComponent').notNull().default(false),
    copiedFromWorkflowId: text('copiedFromWorkflowId'),
  },
  (table): PgTableExtraConfigValue[] => [
    index('Workflow_projectId_idx').on(table.projectId),
    index('Workflow_publishedById_idx').on(table.publishedById),
    index('Workflow_latestVersionId_idx').on(table.latestVersionId),
    index('Workflow_currentVersionId_idx').on(table.currentVersionId),
    index('Workflow_copiedFromWorkflowId_idx').on(table.copiedFromWorkflowId),
    foreignKey({
      name: 'Workflow_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Workflow_latestVersionId_fkey',
      columns: [table.latestVersionId],
      foreignColumns: [workflowVersion.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
    foreignKey({
      name: 'Workflow_currentVersionId_fkey',
      columns: [table.currentVersionId],
      foreignColumns: [workflowVersion.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
    foreignKey({
      name: 'Workflow_publishedById_fkey',
      columns: [table.publishedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Workflow_copiedFromWorkflowId_fkey',
      columns: [table.copiedFromWorkflowId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const workflowVersion = pgTable(
  'WorkflowVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    version: text('version').notNull(),
    commitMessage: text('commitMessage').notNull(),
    authorId: text('authorId').notNull(),
    projectId: text('projectId').notNull(),
    workflowId: text('workflowId').notNull(),
    parentId: text('parentId'),
    autoSaved: boolean('autoSaved').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    dsl: jsonb('dsl').notNull(),
  },
  (table): PgTableExtraConfigValue[] => [
    index('WorkflowVersion_workflowId_idx').on(table.workflowId),
    index('WorkflowVersion_projectId_idx').on(table.projectId),
    index('WorkflowVersion_parentId_idx').on(table.parentId),
    index('WorkflowVersion_authorId_idx').on(table.authorId),
    index('WorkflowVersion_createdAt_idx').on(table.createdAt),
    uniqueIndex('WorkflowVersion_workflowId_version_key').on(table.workflowId, table.version),
    foreignKey({
      name: 'WorkflowVersion_authorId_fkey',
      columns: [table.authorId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowVersion_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowVersion_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowVersion_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const auditLog = pgTable(
  'AuditLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: text('userId'),
    projectId: text('projectId'),
    organizationId: text('organizationId'),
    action: text('action').notNull(),
    args: jsonb('args'),
    error: text('error'),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    metadata: jsonb('metadata'),
    actorUserId: text('actorUserId'),
    targetKind: text('targetKind'),
    targetId: text('targetId'),
    before: jsonb('before'),
    after: jsonb('after'),
    idempotencyKey: text('idempotencyKey'),
  },
  (table) => [
    uniqueIndex('AuditLog_idempotencyKey_key').on(table.idempotencyKey),
    index('AuditLog_createdAt_idx').on(table.createdAt),
    index('AuditLog_userId_idx').on(table.userId),
    index('AuditLog_actorUserId_idx').on(table.actorUserId),
    index('AuditLog_projectId_idx').on(table.projectId),
    index('AuditLog_action_idx').on(table.action),
    index('AuditLog_ipAddress_idx').on(table.ipAddress),
    index('AuditLog_organizationId_createdAt_idx').on(table.organizationId, table.createdAt),
    index('AuditLog_targetKind_targetId_idx').on(table.targetKind, table.targetId),
  ],
)

export const llmPromptConfig = pgTable(
  'LlmPromptConfig',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    handle: text('handle'),
    name: text('name').notNull(),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    organizationId: text('organizationId').notNull(),
    scope: promptScopeEnum('scope').notNull().default('PROJECT'),
    copiedFromPromptId: text('copiedFromPromptId'),
  },
  (table) => [
    index('LlmPromptConfig_projectId_idx').on(table.projectId),
    index('LlmPromptConfig_organizationId_scope_idx').on(table.organizationId, table.scope),
    index('LlmPromptConfig_copiedFromPromptId_idx').on(table.copiedFromPromptId),
    uniqueIndex('LlmPromptConfig_handle_key').on(table.handle),
    foreignKey({
      name: 'LlmPromptConfig_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LlmPromptConfig_copiedFromPromptId_fkey',
      columns: [table.copiedFromPromptId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const llmPromptConfigVersion = pgTable(
  'LlmPromptConfigVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    version: integer('version').notNull(),
    commitMessage: text('commitMessage'),
    authorId: text('authorId'),
    configId: text('configId').notNull(),
    configData: jsonb('config').notNull(),
    schemaVersion: text('schemaVersion').notNull(),
    runtimeParameters: jsonb('runtimeParameters').notNull().default({}),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    projectId: text('projectId').notNull(),
  },
  (table) => [
    index('LlmPromptConfigVersion_configId_idx').on(table.configId),
    index('LlmPromptConfigVersion_authorId_idx').on(table.authorId),
    index('LlmPromptConfigVersion_createdAt_idx').on(table.createdAt),
    uniqueIndex('LlmPromptConfigVersion_configId_version_key').on(table.configId, table.version),
    foreignKey({
      name: 'LlmPromptConfigVersion_authorId_fkey',
      columns: [table.authorId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LlmPromptConfigVersion_configId_fkey',
      columns: [table.configId],
      foreignColumns: [llmPromptConfig.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const promptTagAssignment = pgTable(
  'PromptTagAssignment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    configId: text('configId').notNull(),
    versionId: text('versionId').notNull(),
    tagId: text('tagId').notNull(),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('PromptTagAssignment_configId_idx').on(table.configId),
    index('PromptTagAssignment_versionId_idx').on(table.versionId),
    index('PromptTagAssignment_projectId_idx').on(table.projectId),
    index('PromptTagAssignment_tagId_idx').on(table.tagId),
    uniqueIndex('PromptTagAssignment_configId_tagId_key').on(table.configId, table.tagId),
    foreignKey({
      name: 'PromptTagAssignment_configId_fkey',
      columns: [table.configId],
      foreignColumns: [llmPromptConfig.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTagAssignment_versionId_fkey',
      columns: [table.versionId],
      foreignColumns: [llmPromptConfigVersion.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTagAssignment_tagId_fkey',
      columns: [table.tagId],
      foreignColumns: [promptTag.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTagAssignment_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTagAssignment_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const promptTag = pgTable(
  'PromptTag',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('PromptTag_organizationId_idx').on(table.organizationId),
    uniqueIndex('PromptTag_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'PromptTag_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTag_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PromptTag_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const analytics = pgTable(
  'Analytics',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    key: text('key').notNull(),
    value: jsonb('value'),
    numericValue: doublePrecision('numericValue'),
    stringValue: text('stringValue'),
    boolValue: boolean('boolValue'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('Analytics_createdAt_key_projectId_numericValue_idx').on(
      table.createdAt,
      table.key,
      table.projectId,
      table.numericValue,
    ),
    index('Analytics_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'Analytics_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const customRole = pgTable(
  'CustomRole',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    permissions: jsonb('permissions').notNull(),
    kind: text('kind').notNull().default('custom'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('CustomRole_organizationId_idx').on(table.organizationId),
    uniqueIndex('CustomRole_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'CustomRole_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const notification = pgTable(
  'Notification',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    metadata: jsonb('metadata').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    sentAt: timestamp('sentAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('Notification_organizationId_idx').on(table.organizationId),
    index('Notification_projectId_idx').on(table.projectId),
    index('Notification_createdAt_idx').on(table.createdAt),
    index('Notification_sentAt_idx').on(table.sentAt),
    foreignKey({
      name: 'Notification_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Notification_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const agent = pgTable(
  'Agent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    config: jsonb('config').notNull(),
    workflowId: text('workflowId'),
    copiedFromAgentId: text('copiedFromAgentId'),
    environment: text('environment'),
    ownerUserId: text('ownerUserId'),
    hostLabel: text('hostLabel'),
    identityKey: text('identityKey'),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('Agent_projectId_idx').on(table.projectId),
    index('Agent_workflowId_idx').on(table.workflowId),
    index('Agent_copiedFromAgentId_idx').on(table.copiedFromAgentId),
    index('Agent_projectId_lastSeenAt_idx').on(table.projectId, table.lastSeenAt),
    uniqueIndex('Agent_projectId_identityKey_key').on(table.projectId, table.identityKey),
    foreignKey({
      name: 'Agent_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Agent_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Agent_copiedFromAgentId_fkey',
      columns: [table.copiedFromAgentId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const agentVersion = pgTable(
  'AgentVersion',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    agentId: text('agentId').notNull(),
    version: integer('version').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    config: jsonb('config').notNull(),
    workflowId: text('workflowId'),
    commitMessage: text('commitMessage'),
    authorId: text('authorId'),
    authorKind: text('authorKind'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('AgentVersion_projectId_idx').on(table.projectId),
    uniqueIndex('AgentVersion_agentId_version_key').on(table.agentId, table.version),
  ],
)

export const agentVersionLabel = pgTable(
  'AgentVersionLabel',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    agentId: text('agentId').notNull(),
    label: text('label').notNull(),
    versionId: text('versionId').notNull(),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('AgentVersionLabel_projectId_idx').on(table.projectId),
    index('AgentVersionLabel_versionId_idx').on(table.versionId),
    uniqueIndex('AgentVersionLabel_agentId_label_key').on(table.agentId, table.label),
  ],
)

export const evaluator = pgTable(
  'Evaluator',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    slug: text('slug'),
    type: text('type').notNull(),
    config: jsonb('config').notNull(),
    workflowId: text('workflowId'),
    copiedFromEvaluatorId: text('copiedFromEvaluatorId'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('Evaluator_projectId_idx').on(table.projectId),
    index('Evaluator_workflowId_idx').on(table.workflowId),
    index('Evaluator_copiedFromEvaluatorId_idx').on(table.copiedFromEvaluatorId),
    uniqueIndex('Evaluator_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'Evaluator_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Evaluator_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Evaluator_copiedFromEvaluatorId_fkey',
      columns: [table.copiedFromEvaluatorId],
      foreignColumns: [table.id],
    })
      .onDelete('restrict')
      .onUpdate('restrict'),
  ],
)

export const evaluatorVersion = pgTable(
  'EvaluatorVersion',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    evaluatorId: text('evaluatorId').notNull(),
    version: integer('version').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    config: jsonb('config').notNull(),
    workflowId: text('workflowId'),
    commitMessage: text('commitMessage'),
    authorId: text('authorId'),
    authorKind: text('authorKind'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('EvaluatorVersion_projectId_idx').on(table.projectId),
    uniqueIndex('EvaluatorVersion_evaluatorId_version_key').on(table.evaluatorId, table.version),
  ],
)

export const evaluatorVersionLabel = pgTable(
  'EvaluatorVersionLabel',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    evaluatorId: text('evaluatorId').notNull(),
    label: text('label').notNull(),
    versionId: text('versionId').notNull(),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('EvaluatorVersionLabel_projectId_idx').on(table.projectId),
    index('EvaluatorVersionLabel_versionId_idx').on(table.versionId),
    uniqueIndex('EvaluatorVersionLabel_evaluatorId_label_key').on(table.evaluatorId, table.label),
  ],
)

export const scenario = pgTable(
  'Scenario',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    situation: text('situation').notNull(),
    criteria: text('criteria').array(),
    labels: text('labels').array(),
    parameters: jsonb('parameters'),
    simulatorModel: text('simulatorModel'),
    judgeModel: text('judgeModel'),
    maxTurns: integer('maxTurns'),
    minTurns: integer('minTurns'),
    fields: jsonb('fields'),
    callerVoice: jsonb('callerVoice'),
    testSuiteId: text('testSuiteId'),
    version: integer('version').notNull().default(1),
    lastUpdatedById: text('lastUpdatedById'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Scenario_projectId_idx').on(table.projectId),
    index('Scenario_projectId_testSuiteId_idx').on(table.projectId, table.testSuiteId),
    foreignKey({
      name: 'Scenario_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Scenario_testSuiteId_fkey',
      columns: [table.testSuiteId],
      foreignColumns: [simulationSuite.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Scenario_lastUpdatedById_fkey',
      columns: [table.lastUpdatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const scenarioVersion = pgTable(
  'ScenarioVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    scenarioId: text('scenarioId').notNull(),
    projectId: text('projectId').notNull(),
    version: integer('version').notNull(),
    authorId: text('authorId'),
    authorLabel: text('authorLabel').notNull(),
    changeDescription: text('changeDescription'),
    snapshot: jsonb('snapshot').notNull(),
    schemaVersion: integer('schemaVersion').notNull().default(1),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ScenarioVersion_projectId_scenarioId_version_idx').on(
      table.projectId,
      table.scenarioId,
      table.version,
    ),
    index('ScenarioVersion_createdAt_idx').on(table.createdAt),
    uniqueIndex('ScenarioVersion_scenarioId_version_key').on(table.scenarioId, table.version),
    foreignKey({
      name: 'ScenarioVersion_scenarioId_fkey',
      columns: [table.scenarioId],
      foreignColumns: [scenario.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const simulationSuite = pgTable(
  'SimulationSuite',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    description: text('description'),
    scenarioIds: text('scenarioIds').array(),
    targets: jsonb('targets').notNull(),
    repeatCount: integer('repeatCount').notNull().default(1),
    labels: text('labels').array(),
    simulatorModel: text('simulatorModel'),
    judgeModel: text('judgeModel'),
    kind: text('kind').notNull().default('run_plan'),
    scope: jsonb('scope'),
    fields: jsonb('fields'),
    evaluators: jsonb('evaluators'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('SimulationSuite_projectId_idx').on(table.projectId),
    index('SimulationSuite_projectId_kind_idx').on(table.projectId, table.kind),
    uniqueIndex('SimulationSuite_projectId_slug_key').on(table.projectId, table.slug),
    foreignKey({
      name: 'SimulationSuite_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const subscription = pgTable(
  'Subscription',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    plan: planTypesEnum('plan').notNull(),
    stripeSubscriptionId: text('stripeSubscriptionId'),
    status: subscriptionStatusEnum('status').notNull(),
    startDate: timestamp('startDate', { precision: 3 }),
    endDate: timestamp('endDate', { precision: 3 }),
    lastPaymentFailedDate: timestamp('lastPaymentFailedDate', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    maxMembers: integer('maxMembers'),
    maxProjects: integer('maxProjects'),
    maxMessagesPerMonth: integer('maxMessagesPerMonth'),
    maxWorkflows: integer('maxWorkflows'),
    maxRetentionDays: integer('maxRetentionDays'),
    maxMembersLite: integer('maxMembersLite'),
    maxMembersDeveloper: integer('maxMembersDeveloper'),
    maxTeams: integer('maxTeams'),
    maxPrompts: integer('maxPrompts'),
    maxEvaluators: integer('maxEvaluators'),
    maxScenarios: integer('maxScenarios'),
    maxAgents: integer('maxAgents'),
    maxExperiments: integer('maxExperiments'),
    maxOnlineEvaluations: integer('maxOnlineEvaluations'),
    maxDatasets: integer('maxDatasets'),
    maxDashboards: integer('maxDashboards'),
    maxCustomGraphs: integer('maxCustomGraphs'),
    maxAutomations: integer('maxAutomations'),
    seatsKept: integer('seatsKept'),
    seatsKeptUntil: timestamp('seatsKeptUntil', { precision: 3 }),
    seatLineQuantity: integer('seatLineQuantity'),
    seatLineQuantityAt: timestamp('seatLineQuantityAt', { precision: 3 }),
    seatLineWriteQuantities: integer('seatLineWriteQuantities').array().default([]),
    seatLineWriteSince: timestamp('seatLineWriteSince', { precision: 3 }),
    seatLineWriteAt: timestamp('seatLineWriteAt', { precision: 3 }),
    seatLineWriteAttempts: integer('seatLineWriteAttempts'),
    seatLineWriteFlaggedAt: timestamp('seatLineWriteFlaggedAt', { precision: 3 }),
    seatRepairQuantity: integer('seatRepairQuantity'),
    seatRepairLiftedQuantity: integer('seatRepairLiftedQuantity'),
    seatRepairSince: timestamp('seatRepairSince', { precision: 3 }),
    seatRepairAttempts: integer('seatRepairAttempts'),
    seatRepairFlaggedAt: timestamp('seatRepairFlaggedAt', { precision: 3 }),
    seatLineLeaseToken: text('seatLineLeaseToken'),
    seatLineLeaseUntil: timestamp('seatLineLeaseUntil', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Subscription_stripeSubscriptionId_key').on(table.stripeSubscriptionId),
    index('Subscription_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'Subscription_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const invoice = pgTable(
  'Invoice',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    subscriptionId: text('subscriptionId').notNull(),
    totalAmount: doublePrecision('totalAmount').notNull(),
    currency: text('currency').notNull(),
    dueDate: timestamp('dueDate', { precision: 3 }).notNull(),
    paid: boolean('paid').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Invoice_subscriptionId_idx').on(table.subscriptionId),
    foreignKey({
      name: 'Invoice_subscriptionId_fkey',
      columns: [table.subscriptionId],
      foreignColumns: [subscription.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const invoiceItem = pgTable(
  'InvoiceItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    invoiceId: text('invoiceId').notNull(),
    description: text('description').notNull(),
    amount: doublePrecision('amount').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('InvoiceItem_invoiceId_idx').on(table.invoiceId),
    foreignKey({
      name: 'InvoiceItem_invoiceId_fkey',
      columns: [table.invoiceId],
      foreignColumns: [invoice.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const group = pgTable(
  'Group',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    externalId: text('externalId'),
    scimSource: text('scimSource'),
    scimConnectionId: text('scimConnectionId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('Group_organizationId_idx').on(table.organizationId),
    index('Group_scimConnectionId_idx').on(table.scimConnectionId),
    uniqueIndex('Group_organizationId_slug_key').on(table.organizationId, table.slug),
    uniqueIndex('Group_scimConnectionId_externalId_key').on(
      table.scimConnectionId,
      table.externalId,
    ),
    foreignKey({
      name: 'Group_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const groupMembership = pgTable(
  'GroupMembership',
  {
    userId: text('userId').notNull(),
    groupId: text('groupId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'GroupMembership_pkey', columns: [table.userId, table.groupId] }),
    index('GroupMembership_userId_idx').on(table.userId),
    index('GroupMembership_groupId_idx').on(table.groupId),
    foreignKey({
      name: 'GroupMembership_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GroupMembership_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [group.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const authzUserStanding = pgTable('AuthzUserStanding', {
  userId: text('userId').primaryKey(),
  deactivatedAt: timestamp('deactivatedAt', { precision: 3 }),
  erasedAt: timestamp('erasedAt', { precision: 3 }),
  standingChangedAt: timestamp('standingChangedAt', { precision: 3 }).notNull(),
})

export const authzEngineSwitch = pgTable('AuthzEngineSwitch', {
  organizationId: text('organizationId').primaryKey(),
  onEngine: boolean('onEngine').notNull(),
  pinned: boolean('pinned').notNull().default(false),
  switchedAt: timestamp('switchedAt', { precision: 3 }).notNull(),
  lastAppliedEventId: text('lastAppliedEventId'),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const authzOrganizationPolicy = pgTable('AuthzOrganizationPolicy', {
  organizationId: text('organizationId').primaryKey(),
  braidsOrganizationAdminsOnly: boolean('braidsOrganizationAdminsOnly').notNull().default(false),
  updatedByUserId: text('updatedByUserId').notNull(),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const roleBinding = pgTable(
  'RoleBinding',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    userId: text('userId'),
    groupId: text('groupId'),
    apiKeyId: text('apiKeyId'),
    role: teamUserRoleEnum('role').notNull(),
    customRoleId: text('customRoleId'),
    scopeType: roleBindingScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('RoleBinding_organizationId_idx').on(table.organizationId),
    index('RoleBinding_userId_idx').on(table.userId),
    index('RoleBinding_groupId_idx').on(table.groupId),
    index('RoleBinding_apiKeyId_idx').on(table.apiKeyId),
    index('RoleBinding_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    foreignKey({
      name: 'RoleBinding_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoleBinding_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoleBinding_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [group.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoleBinding_apiKeyId_fkey',
      columns: [table.apiKeyId],
      foreignColumns: [apiKey.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoleBinding_customRoleId_fkey',
      columns: [table.customRoleId],
      foreignColumns: [customRole.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const grant = pgTable(
  'Grant',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    principalType: grantPrincipalTypeEnum('principalType').notNull(),
    principalId: text('principalId'),
    roleKey: text('roleKey'),
    legacyRole: text('legacyRole'),
    source: text('source').notNull(),
    scopeType: grantScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    token: text('token'),
    permission: text('permission'),
    resourceKind: text('resourceKind'),
    projectId: text('projectId'),
    createdByUserId: text('createdByUserId'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    maxViews: integer('maxViews'),
    condition: jsonb('condition'),
    replacesGrantId: text('replacesGrantId'),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedReason: text('revokedReason'),
  },
  (table) => [
    uniqueIndex('Grant_token_key').on(table.token),
    index('Grant_organizationId_scopeType_scopeId_idx').on(
      table.organizationId,
      table.scopeType,
      table.scopeId,
    ),
    index('Grant_projectId_resourceKind_scopeId_idx').on(
      table.projectId,
      table.resourceKind,
      table.scopeId,
    ),
  ],
)

export const grantUsage = pgTable(
  'GrantUsage',
  {
    grantId: text('grantId').primaryKey(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId').notNull(),
    viewCount: integer('viewCount').notNull().default(0),
    lastViewedAt: timestamp('lastViewedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('GrantUsage_organizationId_idx').on(table.organizationId),
    index('GrantUsage_projectId_idx').on(table.projectId),
  ],
)

export const role = pgTable(
  'Role',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    permissions: jsonb('permissions').notNull(),
    kind: text('kind').notNull().default('custom'),
    occurredAt: timestamp('occurredAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [index('Role_organizationId_idx').on(table.organizationId)],
)

export const apiKey = pgTable(
  'ApiKey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    name: text('name').notNull(),
    description: text('description'),
    lookupId: text('lookupId').notNull(),
    hashedSecret: text('hashedSecret').notNull(),
    permissionMode: text('permissionMode').notNull().default('all'),
    userId: text('userId'),
    createdByUserId: text('createdByUserId'),
    organizationId: text('organizationId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revocationCause: text('revocationCause'),
    parentApiKeyId: text('parentApiKeyId'),
    ingestSourceType: text('ingestSourceType'),
    ingestionTemplateId: text('ingestionTemplateId'),
    createdByDeviceLabel: text('createdByDeviceLabel'),
    isSystemManaged: boolean('isSystemManaged').notNull().default(false),
    createdByBrowserSessionId: text('createdByBrowserSessionId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ApiKey_lookupId_key').on(table.lookupId),
    index('ApiKey_userId_idx').on(table.userId),
    index('ApiKey_organizationId_idx').on(table.organizationId),
    index('ApiKey_lookupId_idx').on(table.lookupId),
    index('ApiKey_ingestSourceType_idx').on(table.ingestSourceType),
    index('ApiKey_ingestionTemplateId_idx').on(table.ingestionTemplateId),
    foreignKey({ name: 'ApiKey_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ApiKey_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const signingKey = pgTable(
  'SigningKey',
  {
    id: text('id').primaryKey(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    publicId: text('publicId').notNull(),
    secretCiphertext: text('secretCiphertext').notNull(),
    createdByUserId: text('createdByUserId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    rotatedAt: timestamp('rotatedAt', { precision: 3 }),
    verifiesUntil: timestamp('verifiesUntil', { precision: 3 }),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('SigningKey_publicId_key').on(table.publicId),
    index('SigningKey_projectId_idx').on(table.projectId),
    index('SigningKey_organizationId_idx').on(table.organizationId),
  ],
)

export const projectActiveDay = pgTable(
  'ProjectActiveDay',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('projectId').notNull(),
    day: text('day').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('ProjectActiveDay_projectId_day_key').on(table.projectId, table.day)],
)

export const nurturingProject = pgTable(
  'NurturingProject',
  { projectId: text('projectId').primaryKey(), organizationId: text('organizationId').notNull() },
  (table) => [index('NurturingProject_organizationId_idx').on(table.organizationId)],
)

export const nurturingUncopiedProject = pgTable('NurturingUncopiedProject', {
  projectId: text('projectId').primaryKey(),
  firstSeenAt: timestamp('firstSeenAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const nurturingOrganizationMember = pgTable(
  'NurturingOrganizationMember',
  {
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    joinedAt: timestamp('joinedAt', { precision: 3 }),
    leftAt: timestamp('leftAt', { precision: 3 }),
    returnedAt: timestamp('returnedAt', { precision: 3 }),
  },
  (table) => [
    primaryKey({
      name: 'NurturingOrganizationMember_pkey',
      columns: [table.organizationId, table.userId],
    }),
    index('NurturingOrganizationMember_userId_idx').on(table.userId),
  ],
)

export const nurturingOrganizationCreated = pgTable(
  'NurturingOrganizationCreated',
  {
    organizationId: text('organizationId').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  },
  (table) => [index('NurturingOrganizationCreated_createdAt_idx').on(table.createdAt)],
)

export const nurturingOrganization = pgTable('NurturingOrganization', {
  organizationId: text('organizationId').primaryKey(),
  adminUserId: text('adminUserId'),
  seeded: boolean('seeded').notNull().default(false),
  evaluationCount: integer('evaluationCount').notNull().default(0),
  simulationRunCount: integer('simulationRunCount').notNull().default(0),
  codingFirstSessionAt: timestamp('codingFirstSessionAt', { precision: 3 }),
  codingAgentTypes: text('codingAgentTypes').array().default([]),
  codingActiveDays: integer('codingActiveDays').notNull().default(0),
  codingLastActiveDay: text('codingLastActiveDay'),
  codingTerminalTypes: text('codingTerminalTypes').array().default([]),
  codingRepositoryKeys: text('codingRepositoryKeys').array().default([]),
  codingFirstDays: integer('codingFirstDays').array().default([]),
  lastTraceAt: timestamp('lastTraceAt', { precision: 3 }),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const nurturingProjectUsageView = pgTable(
  'NurturingProjectUsageView',
  {
    projectId: text('projectId').notNull(),
    owner: text('owner').notNull(),
    folded: jsonb('folded').notNull(),
    appliedEventIds: text('appliedEventIds').array().default([]),
    projectionVersion: text('projectionVersion').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    primaryKey({ name: 'NurturingProjectUsageView_pkey', columns: [table.projectId, table.owner] }),
  ],
)

export const nurturingProjectTraceDay = pgTable(
  'NurturingProjectTraceDay',
  {
    projectId: text('projectId').notNull(),
    day: text('day').notNull(),
    traceCount: integer('traceCount').notNull().default(0),
    errorCount: integer('errorCount').notNull().default(0),
    lastTraceAt: timestamp('lastTraceAt', { precision: 3 }),
    lastFailedTraceAt: timestamp('lastFailedTraceAt', { precision: 3 }),
  },
  (table) => [
    primaryKey({ name: 'NurturingProjectTraceDay_pkey', columns: [table.projectId, table.day] }),
  ],
)

export const nurturingPersonalProject = pgTable('NurturingPersonalProject', {
  projectId: text('projectId').primaryKey(),
  organizationId: text('organizationId').notNull(),
  ownerUserId: text('ownerUserId').notNull(),
})

export const nurturingTracedProject = pgTable(
  'NurturingTracedProject',
  { projectId: text('projectId').primaryKey(), organizationId: text('organizationId').notNull() },
  (table) => [index('NurturingTracedProject_organizationId_idx').on(table.organizationId)],
)

export const billingMeterCheckpoint = pgTable(
  'BillingMeterCheckpoint',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    organizationId: text('organizationId').notNull(),
    billingMonth: text('billingMonth').notNull(),
    meter: text('meter').notNull().default('langwatch_billable_events'),
    lastReportedTotal: integer('lastReportedTotal').notNull().default(0),
    pendingReportedTotal: integer('pendingReportedTotal'),
    consecutiveFailures: integer('consecutiveFailures').notNull().default(0),
    lastCountedEventId: text('lastCountedEventId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('BillingMeterCheckpoint_organizationId_billingMonth_meter_key').on(
      table.organizationId,
      table.billingMonth,
      table.meter,
    ),
  ],
)

export const virtualKey = pgTable(
  'VirtualKey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    status: virtualKeyStatusEnum('status').notNull().default('ACTIVE'),
    purpose: virtualKeyPurposeEnum('purpose').notNull().default('USER'),
    externalId: text('externalId'),
    metadata: jsonb('metadata').notNull().default({}),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    disabledReason: text('disabledReason'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    hashedSecret: text('hashedSecret').notNull(),
    displayPrefix: text('displayPrefix').notNull(),
    principalUserId: text('principalUserId'),
    traceProjectId: text('traceProjectId'),
    config: jsonb('config').notNull().default({}),
    revision: bigint('revision', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    previousHashedSecret: text('previousHashedSecret'),
    previousSecretValidUntil: timestamp('previousSecretValidUntil', { precision: 3 }),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedById: text('revokedById'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById').notNull(),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    routingPolicyId: text('routingPolicyId'),
    routingMode: virtualKeyRoutingModeEnum('routingMode').notNull().default('NONE'),
    connectServices: text('connectServices').array().default([]),
    licenseTokenHash: text('licenseTokenHash'),
    licenseInstanceId: text('licenseInstanceId'),
    licenseExpiresAt: timestamp('licenseExpiresAt', { precision: 3 }),
    licenseId: text('licenseId'),
  },
  (table) => [
    uniqueIndex('VirtualKey_licenseTokenHash_key').on(table.licenseTokenHash),
    index('VirtualKey_organizationId_idx').on(table.organizationId),
    index('VirtualKey_displayPrefix_idx').on(table.displayPrefix),
    index('VirtualKey_status_idx').on(table.status),
    index('VirtualKey_principalUserId_idx').on(table.principalUserId),
    index('VirtualKey_revision_idx').on(table.revision),
    index('VirtualKey_routingPolicyId_idx').on(table.routingPolicyId),
    index('VirtualKey_organizationId_purpose_idx').on(table.organizationId, table.purpose),
    uniqueIndex('VirtualKey_hashedSecret_key').on(table.hashedSecret),
    uniqueIndex('VirtualKey_organizationId_externalId_key').on(
      table.organizationId,
      table.externalId,
    ),
    foreignKey({
      name: 'VirtualKey_principalUserId_fkey',
      columns: [table.principalUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'VirtualKey_routingPolicyId_fkey',
      columns: [table.routingPolicyId],
      foreignColumns: [routingPolicy.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const virtualKeyScope = pgTable(
  'VirtualKeyScope',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    virtualKeyId: text('virtualKeyId').notNull(),
    scopeType: virtualKeyScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('VirtualKeyScope_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    index('VirtualKeyScope_virtualKeyId_idx').on(table.virtualKeyId),
    uniqueIndex('VirtualKeyScope_virtualKeyId_scopeType_scopeId_key').on(
      table.virtualKeyId,
      table.scopeType,
      table.scopeId,
    ),
    foreignKey({
      name: 'VirtualKeyScope_virtualKeyId_fkey',
      columns: [table.virtualKeyId],
      foreignColumns: [virtualKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const routingPolicy = pgTable(
  'RoutingPolicy',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    modelProviderIds: jsonb('modelProviderIds').notNull().default([]),
    modelAliases: jsonb('modelAliases').notNull().default({}),
    defaultModel: text('defaultModel'),
    policyRules: jsonb('policyRules').notNull().default({}),
    isDefault: boolean('isDefault').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('RoutingPolicy_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'RoutingPolicy_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoutingPolicy_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RoutingPolicy_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const routingPolicyScope = pgTable(
  'RoutingPolicyScope',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    routingPolicyId: text('routingPolicyId').notNull(),
    scopeType: routingPolicyScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('RoutingPolicyScope_scopeType_scopeId_idx').on(table.scopeType, table.scopeId),
    index('RoutingPolicyScope_routingPolicyId_idx').on(table.routingPolicyId),
    uniqueIndex('RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key').on(
      table.routingPolicyId,
      table.scopeType,
      table.scopeId,
    ),
    foreignKey({
      name: 'RoutingPolicyScope_routingPolicyId_fkey',
      columns: [table.routingPolicyId],
      foreignColumns: [routingPolicy.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayGuardrail = pgTable(
  'GatewayGuardrail',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    evaluatorId: text('evaluatorId').notNull(),
    direction: gatewayGuardrailDirectionEnum('direction').notNull(),
    failureMode: gatewayGuardrailFailureModeEnum('failureMode').notNull().default('FAIL_CLOSED'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('GatewayGuardrail_projectId_idx').on(table.projectId),
    index('GatewayGuardrail_evaluatorId_idx').on(table.evaluatorId),
    index('GatewayGuardrail_projectId_archivedAt_idx').on(table.projectId, table.archivedAt),
    foreignKey({
      name: 'GatewayGuardrail_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GatewayGuardrail_evaluatorId_fkey',
      columns: [table.evaluatorId],
      foreignColumns: [evaluator.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GatewayGuardrail_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GatewayGuardrail_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const ingestionSource = pgTable(
  'IngestionSource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    teamId: text('teamId'),
    sourceType: text('sourceType').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    ingestSecretHash: text('ingestSecretHash').notNull(),
    parserConfig: jsonb('parserConfig').notNull().default({}),
    pollerCursor: jsonb('pollerCursor'),
    errorCount: integer('errorCount').notNull().default(0),
    pullSchedule: text('pullSchedule'),
    status: text('status').notNull().default('awaiting_first_event'),
    traceProjectId: text('traceProjectId'),
    providerAccountId: text('providerAccountId'),
    lastEventAt: timestamp('lastEventAt', { precision: 3 }),
    lastSuccessAt: timestamp('lastSuccessAt', { precision: 3 }),
    lastReadThroughAt: timestamp('lastReadThroughAt', { precision: 3 }),
    lastRunCompleteness: text('lastRunCompleteness'),
    unpricedUsageSince: timestamp('unpricedUsageSince', { precision: 3 }),
    unpricedUsageThrough: timestamp('unpricedUsageThrough', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
  },
  (table) => [
    index('IngestionSource_organizationId_idx').on(table.organizationId),
    index('IngestionSource_teamId_idx').on(table.teamId),
    index('IngestionSource_sourceType_idx').on(table.sourceType),
    uniqueIndex('IngestionSource_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'IngestionSource_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'IngestionSource_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'IngestionSource_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const anomalyRule = pgTable(
  'AnomalyRule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scope: text('scope').notNull(),
    scopeId: text('scopeId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    severity: text('severity').notNull(),
    ruleType: text('ruleType').notNull(),
    thresholdConfig: jsonb('thresholdConfig').notNull().default({}),
    destinationConfig: jsonb('destinationConfig').notNull().default({}),
    status: text('status').notNull().default('active'),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
  },
  (table) => [
    index('AnomalyRule_organizationId_idx').on(table.organizationId),
    index('AnomalyRule_scope_scopeId_idx').on(table.scope, table.scopeId),
    index('AnomalyRule_ruleType_idx').on(table.ruleType),
    index('AnomalyRule_status_idx').on(table.status),
    uniqueIndex('AnomalyRule_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'AnomalyRule_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnomalyRule_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const anomalyAlert = pgTable(
  'AnomalyAlert',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    ruleId: text('ruleId').notNull(),
    severity: text('severity').notNull(),
    ruleName: text('ruleName').notNull(),
    ruleType: text('ruleType').notNull(),
    triggerWindowStart: timestamp('triggerWindowStart', { precision: 3 }).notNull(),
    triggerWindowEnd: timestamp('triggerWindowEnd', { precision: 3 }).notNull(),
    triggerSpendUsd: numeric('triggerSpendUsd', { precision: 18, scale: 6 }),
    triggerEventCount: integer('triggerEventCount'),
    detail: jsonb('detail').notNull().default({}),
    state: text('state').notNull().default('open'),
    destinationStatus: jsonb('destinationStatus').notNull().default({}),
    detectedAt: timestamp('detectedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    acknowledgedAt: timestamp('acknowledgedAt', { precision: 3 }),
    resolvedAt: timestamp('resolvedAt', { precision: 3 }),
  },
  (table) => [
    index('AnomalyAlert_organizationId_detectedAt_idx').on(table.organizationId, table.detectedAt),
    index('AnomalyAlert_ruleId_idx').on(table.ruleId),
    index('AnomalyAlert_state_idx').on(table.state),
    uniqueIndex('AnomalyAlert_ruleId_triggerWindowStart_key').on(
      table.ruleId,
      table.triggerWindowStart,
    ),
    foreignKey({
      name: 'AnomalyAlert_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AnomalyAlert_ruleId_fkey',
      columns: [table.ruleId],
      foreignColumns: [anomalyRule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const aiToolEntry = pgTable(
  'AiToolEntry',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scope: text('scope').notNull(),
    scopeId: text('scopeId').notNull(),
    type: text('type').notNull(),
    displayName: text('displayName').notNull(),
    slug: text('slug').notNull(),
    iconKey: text('iconKey'),
    iconAsset: text('iconAsset'),
    order: integer('order').notNull().default(0),
    enabled: boolean('enabled').notNull().default(true),
    config: jsonb('config').notNull().default({}),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('AiToolEntry_organizationId_scope_scopeId_idx').on(
      table.organizationId,
      table.scope,
      table.scopeId,
    ),
    index('AiToolEntry_organizationId_enabled_archivedAt_idx').on(
      table.organizationId,
      table.enabled,
      table.archivedAt,
    ),
    index('AiToolEntry_organizationId_type_idx').on(table.organizationId, table.type),
    foreignKey({
      name: 'AiToolEntry_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const aiToolEntryTeam = pgTable(
  'AiToolEntryTeam',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    entryId: text('entryId').notNull(),
    teamId: text('teamId').notNull(),
  },
  (table) => [
    index('AiToolEntryTeam_teamId_idx').on(table.teamId),
    uniqueIndex('AiToolEntryTeam_entryId_teamId_key').on(table.entryId, table.teamId),
    foreignKey({
      name: 'AiToolEntryTeam_entryId_fkey',
      columns: [table.entryId],
      foreignColumns: [aiToolEntry.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AiToolEntryTeam_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const aiToolEntryDepartment = pgTable(
  'AiToolEntryDepartment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    entryId: text('entryId').notNull(),
    departmentId: text('departmentId').notNull(),
  },
  (table) => [
    index('AiToolEntryDepartment_departmentId_idx').on(table.departmentId),
    uniqueIndex('AiToolEntryDepartment_entryId_departmentId_key').on(
      table.entryId,
      table.departmentId,
    ),
    foreignKey({
      name: 'AiToolEntryDepartment_entryId_fkey',
      columns: [table.entryId],
      foreignColumns: [aiToolEntry.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const ingestionTemplate = pgTable(
  'IngestionTemplate',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId'),
    slug: text('slug').notNull(),
    sourceType: text('sourceType').notNull(),
    displayName: text('displayName').notNull(),
    description: text('description'),
    iconAsset: text('iconAsset'),
    credentialSchema: text('credentialSchema'),
    ottlRules: text('ottlRules').notNull().default(''),
    platformPublished: boolean('platformPublished').notNull().default(false),
    enabled: boolean('enabled').notNull().default(true),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById'),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('IngestionTemplate_organizationId_archivedAt_idx').on(
      table.organizationId,
      table.archivedAt,
    ),
    index('IngestionTemplate_sourceType_idx').on(table.sourceType),
    uniqueIndex('IngestionTemplate_organizationId_slug_key').on(table.organizationId, table.slug),
    foreignKey({
      name: 'IngestionTemplate_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayConnectUpstream = pgTable('GatewayConnectUpstream', {
  organizationId: text('organizationId').primaryKey(),
  baseUrl: text('baseUrl').notNull(),
  encryptedToken: text('encryptedToken').notNull(),
  instanceId: text('instanceId').notNull(),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const gatewayTraceExportKey = pgTable('GatewayTraceExportKey', {
  projectId: text('projectId').primaryKey(),
  apiKeyId: text('apiKeyId').notNull(),
  encryptedToken: text('encryptedToken').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const gatewayBudget = pgTable(
  'GatewayBudget',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    scopeType: gatewayBudgetScopeTypeEnum('scopeType').notNull(),
    scopeId: text('scopeId').notNull(),
    providerKey: text('providerKey'),
    name: text('name').notNull(),
    description: text('description'),
    window: gatewayBudgetWindowEnum('window').notNull(),
    limitUsd: numeric('limitUsd', { precision: 18, scale: 6 }).notNull(),
    onBreach: gatewayBudgetBreachActionEnum('onBreach').notNull().default('BLOCK'),
    timezone: text('timezone'),
    externalId: text('externalId'),
    metadata: jsonb('metadata').notNull().default({}),
    spentUsd: numeric('spentUsd', { precision: 18, scale: 6 }).notNull().default('0'),
    currentPeriodStartedAt: timestamp('currentPeriodStartedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    resetsAt: timestamp('resetsAt', { precision: 3 }).notNull(),
    lastResetAt: timestamp('lastResetAt', { precision: 3 }),
    cycleAnchorAt: timestamp('cycleAnchorAt', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById').notNull(),
    managedByVirtualKeyId: text('managedByVirtualKeyId'),
  },
  (table) => [
    index('GatewayBudget_organizationId_idx').on(table.organizationId),
    index('GatewayBudget_scopeType_scopeId_providerKey_idx').on(
      table.scopeType,
      table.scopeId,
      table.providerKey,
    ),
    index('GatewayBudget_managedByVirtualKeyId_idx').on(table.managedByVirtualKeyId),
    index('GatewayBudget_resetsAt_idx').on(table.resetsAt),
    uniqueIndex('GatewayBudget_organizationId_externalId_key').on(
      table.organizationId,
      table.externalId,
    ),
    foreignKey({
      name: 'GatewayBudget_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const gatewayBudgetBucketBoundary = pgTable(
  'GatewayBudgetBucketBoundary',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    budgetId: text('budgetId').notNull(),
    bucketScopeId: text('bucketScopeId').notNull(),
    periodStartedAt: timestamp('periodStartedAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('GatewayBudgetBucketBoundary_organizationId_idx').on(table.organizationId),
    uniqueIndex('GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key').on(
      table.budgetId,
      table.bucketScopeId,
    ),
    foreignKey({
      name: 'GatewayBudgetBucketBoundary_budgetId_fkey',
      columns: [table.budgetId],
      foreignColumns: [gatewayBudget.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayBudgetLedger = pgTable(
  'GatewayBudgetLedger',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    budgetId: text('budgetId').notNull(),
    virtualKeyId: text('virtualKeyId').notNull(),
    modelProviderId: text('modelProviderId'),
    providerKey: text('providerKey'),
    gatewayRequestId: text('gatewayRequestId').notNull(),
    amountUsd: numeric('amountUsd', { precision: 18, scale: 6 }).notNull(),
    tokensInput: integer('tokensInput').notNull().default(0),
    tokensOutput: integer('tokensOutput').notNull().default(0),
    tokensCacheRead: integer('tokensCacheRead').notNull().default(0),
    tokensCacheWrite: integer('tokensCacheWrite').notNull().default(0),
    model: text('model').notNull(),
    durationMs: integer('durationMs'),
    status: gatewayBudgetLedgerStatusEnum('status').notNull().default('SUCCESS'),
    occurredAt: timestamp('occurredAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('GatewayBudgetLedger_virtualKeyId_idx').on(table.virtualKeyId),
    index('GatewayBudgetLedger_gatewayRequestId_idx').on(table.gatewayRequestId),
    index('GatewayBudgetLedger_occurredAt_idx').on(table.occurredAt),
    uniqueIndex('GatewayBudgetLedger_budgetId_gatewayRequestId_key').on(
      table.budgetId,
      table.gatewayRequestId,
    ),
    foreignKey({
      name: 'GatewayBudgetLedger_budgetId_fkey',
      columns: [table.budgetId],
      foreignColumns: [gatewayBudget.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GatewayBudgetLedger_virtualKeyId_fkey',
      columns: [table.virtualKeyId],
      foreignColumns: [virtualKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayChangeEvent = pgTable(
  'GatewayChangeEvent',
  {
    revision: bigserial('revision', { mode: 'bigint' }).primaryKey(),
    organizationId: text('organizationId').notNull(),
    kind: gatewayChangeEventKindEnum('kind').notNull(),
    virtualKeyId: text('virtualKeyId'),
    budgetId: text('budgetId'),
    modelProviderId: text('modelProviderId'),
    projectId: text('projectId'),
    payload: jsonb('payload'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('GatewayChangeEvent_organizationId_revision_idx').on(
      table.organizationId,
      table.revision,
    ),
    index('GatewayChangeEvent_projectId_revision_idx').on(table.projectId, table.revision),
    index('GatewayChangeEvent_virtualKeyId_idx').on(table.virtualKeyId),
    index('GatewayChangeEvent_budgetId_idx').on(table.budgetId),
    foreignKey({
      name: 'GatewayChangeEvent_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'GatewayChangeEvent_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const gatewayCacheRule = pgTable(
  'GatewayCacheRule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    priority: integer('priority').notNull().default(100),
    enabled: boolean('enabled').notNull().default(true),
    matchers: jsonb('matchers').notNull(),
    action: jsonb('action').notNull(),
    modeEnum: gatewayCacheRuleModeEnum('modeEnum').notNull(),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdById: text('createdById').notNull(),
  },
  (table) => [
    index('GatewayCacheRule_organizationId_archivedAt_priority_idx').on(
      table.organizationId,
      table.archivedAt,
      table.priority,
    ),
    index('GatewayCacheRule_organizationId_enabled_priority_idx').on(
      table.organizationId,
      table.enabled,
      table.priority,
    ),
  ],
)

export const featureFlag = pgTable('FeatureFlag', {
  key: text('key').primaryKey(),
  enabled: boolean('enabled').notNull(),
  rules: jsonb('rules'),
  lastEditedBy: text('lastEditedBy'),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const featureFlagExperimentSetting = pgTable(
  'FeatureFlagExperimentSetting',
  {
    flagKey: text('flagKey').notNull(),
    subjectType: featureFlagExperimentSubjectTypeEnum('subjectType').notNull(),
    subjectId: text('subjectId').notNull(),
    enabled: boolean('enabled').notNull(),
    changedByUserId: text('changedByUserId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'FeatureFlagExperimentSetting_pkey',
      columns: [table.flagKey, table.subjectType, table.subjectId],
    }),
    index('FeatureFlagExperimentSetting_subjectType_subjectId_idx').on(
      table.subjectType,
      table.subjectId,
    ),
  ],
)

export const processManagerInstance = pgTable(
  'ProcessManagerInstance',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    processName: text('processName').notNull(),
    projectId: text('projectId').notNull(),
    processKey: text('processKey').notNull(),
    tenantId: text('tenantId').notNull(),
    userId: text('userId'),
    state: jsonb('state').notNull(),
    revision: integer('revision').notNull(),
    nextWakeAt: timestamp('nextWakeAt', { withTimezone: true, precision: 3 }),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 3 }).notNull(),
  },
  (table) => [
    index('ProcessManagerInstance_nextWakeAt_idx').on(table.nextWakeAt),
    uniqueIndex('ProcessManagerInstance_processName_projectId_processKey_key').on(
      table.processName,
      table.projectId,
      table.processKey,
    ),
  ],
)

export const processManagerInbox = pgTable(
  'ProcessManagerInbox',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    processName: text('processName').notNull(),
    projectId: text('projectId').notNull(),
    processKey: text('processKey').notNull(),
    tenantId: text('tenantId').notNull(),
    sourceEventId: text('sourceEventId').notNull(),
    sourceEventKey: text('sourceEventKey'),
    consumedAt: timestamp('consumedAt', { withTimezone: true, precision: 3 }).notNull(),
  },
  (table) => [
    index('ProcessManagerInbox_processName_projectId_processKey_consum_idx').on(
      table.processName,
      table.projectId,
      table.processKey,
      table.consumedAt,
    ),
    index('ProcessManagerInbox_consumedAt_idx').on(table.consumedAt),
    uniqueIndex('ProcessManagerInbox_processName_projectId_sourceEventKey_key').on(
      table.processName,
      table.projectId,
      table.sourceEventKey,
    ),
  ],
)

export const processManagerOutbox = pgTable(
  'ProcessManagerOutbox',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    processName: text('processName').notNull(),
    projectId: text('projectId').notNull(),
    processKey: text('processKey').notNull(),
    tenantId: text('tenantId').notNull(),
    userId: text('userId'),
    messageKey: text('messageKey').notNull(),
    intentType: text('intentType').notNull(),
    payload: jsonb('payload').notNull(),
    traceCarrier: jsonb('traceCarrier').notNull(),
    sourceEventId: text('sourceEventId'),
    status: processManagerOutboxStatusEnum('status').notNull().default('pending'),
    attempts: integer('attempts').notNull().default(0),
    nextAttemptAt: timestamp('nextAttemptAt', { withTimezone: true, precision: 3 }).notNull(),
    leasedUntil: timestamp('leasedUntil', { withTimezone: true, precision: 3 }),
    leaseToken: text('leaseToken'),
    dispatchedAt: timestamp('dispatchedAt', { withTimezone: true, precision: 3 }),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('ProcessManagerOutbox_leaseToken_key').on(table.leaseToken),
    index('ProcessManagerOutbox_processName_projectId_processKey_creat_idx').on(
      table.processName,
      table.projectId,
      table.processKey,
      table.createdAt,
    ),
    index('ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx').on(
      table.status,
      table.nextAttemptAt,
      table.leasedUntil,
    ),
    index('ProcessManagerOutbox_status_processName_nextAttemptAt_idx').on(
      table.status,
      table.processName,
      table.nextAttemptAt,
    ),
    uniqueIndex('ProcessManagerOutbox_processName_projectId_messageKey_key').on(
      table.processName,
      table.projectId,
      table.messageKey,
    ),
  ],
)

export const processManagerOutboxAttempt = pgTable(
  'ProcessManagerOutboxAttempt',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    outboxId: text('outboxId').notNull(),
    projectId: text('projectId').notNull(),
    attempt: integer('attempt').notNull(),
    occurredAt: timestamp('occurredAt', { withTimezone: true, precision: 3 }).notNull(),
    outcome: processManagerOutboxAttemptOutcomeEnum('outcome').notNull(),
    errorType: text('errorType').notNull(),
    errorMessage: text('errorMessage').notNull(),
    retryAfterMs: integer('retryAfterMs'),
  },
  (table) => [
    index('ProcessManagerOutboxAttempt_outboxId_attempt_idx').on(table.outboxId, table.attempt),
    foreignKey({
      name: 'ProcessManagerOutboxAttempt_outboxId_fkey',
      columns: [table.outboxId],
      foreignColumns: [processManagerOutbox.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const topicClusteringRunProjection = pgTable(
  'TopicClusteringRunProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    LastRequestedAt: doublePrecision('lastRequestedAt'),
    LastRequestTrigger: text('lastRequestTrigger'),
    LastRunAt: doublePrecision('lastRunAt'),
    LastRunOutcome: text('lastRunOutcome'),
    LastRunMode: text('lastRunMode'),
    LastRunSkippedReason: text('lastRunSkippedReason'),
    LastRunError: text('lastRunError'),
    LastRunErrorCode: text('lastRunErrorCode'),
    LastRunErrorUserActionable: boolean('lastRunErrorUserActionable').notNull().default(false),
    LastRunTracesProcessed: integer('lastRunTracesProcessed').notNull().default(0),
    LastRunTopicsCount: integer('lastRunTopicsCount').notNull().default(0),
    LastRunSubtopicsCount: integer('lastRunSubtopicsCount').notNull().default(0),
    LastRunPages: integer('lastRunPages').notNull().default(0),
    InProgressRunId: text('inProgressRunId'),
    InProgressTraces: integer('inProgressTraces').notNull().default(0),
    InProgressPages: integer('inProgressPages').notNull().default(0),
    InProgressStartedAt: doublePrecision('inProgressStartedAt'),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [uniqueIndex('TopicClusteringRunProjection_projectId_key').on(table.projectId)],
)

export const topicModelProjection = pgTable(
  'TopicModelProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [uniqueIndex('TopicModelProjection_projectId_key').on(table.projectId)],
)

export const topicClusteringRunHistoryProjection = pgTable(
  'TopicClusteringRunHistoryProjection',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    Runs: jsonb('runs').notNull(),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [uniqueIndex('TopicClusteringRunHistoryProjection_projectId_key').on(table.projectId)],
)

export const ingestionPullRunProjection = pgTable(
  'IngestionPullRunProjection',
  {
    id: text('id').primaryKey(),
    sourceId: text('sourceId').notNull(),
    projectId: text('projectId').notNull(),
    Enabled: boolean('enabled').notNull().default(false),
    Cron: text('cron'),
    Cursor: text('cursor'),
    LastRunAt: doublePrecision('lastRunAt'),
    LastRunOutcome: text('lastRunOutcome'),
    LastRunEventCount: integer('lastRunEventCount').notNull().default(0),
    LastRunError: text('lastRunError'),
    LastRunErrorCode: text('lastRunErrorCode'),
    ConsecutiveErrors: integer('consecutiveErrors').notNull().default(0),
    LastSuccessAt: doublePrecision('lastSuccessAt'),
    LastRunScheduledFor: doublePrecision('lastRunScheduledFor'),
    LastReadThroughAt: doublePrecision('lastReadThroughAt'),
    LastRunCompleteness: text('lastRunCompleteness'),
    LastAgentsListingAt: doublePrecision('lastAgentsListingAt'),
    LastAgentsListingOutcome: text('lastAgentsListingOutcome'),
    LastAgentsListingCount: integer('lastAgentsListingCount'),
    LastAgentsListingReason: text('lastAgentsListingReason'),
    LastAgentsListingStatus: integer('lastAgentsListingStatus'),
    LastPeopleListingAt: doublePrecision('lastPeopleListingAt'),
    LastPeopleListingOutcome: text('lastPeopleListingOutcome'),
    LastPeopleDirectoryCount: integer('lastPeopleDirectoryCount'),
    LastPeopleWithheldCount: integer('lastPeopleWithheldCount'),
    LastPeopleListingReason: text('lastPeopleListingReason'),
    LastPeopleListingStatus: integer('lastPeopleListingStatus'),
    CreatedAt: doublePrecision('createdAt').notNull(),
    UpdatedAt: doublePrecision('updatedAt').notNull(),
    OccurredAt: doublePrecision('occurredAt').notNull(),
    AcceptedAt: doublePrecision('acceptedAt').notNull(),
    LastEventId: text('lastEventId').notNull(),
    ProjectionVersion: text('projectionVersion').notNull(),
  },
  (table) => [
    uniqueIndex('IngestionPullRunProjection_sourceId_key').on(table.sourceId),
    index('IngestionPullRunProjection_projectId_idx').on(table.projectId),
  ],
)

export const scheduledJob = pgTable(
  'ScheduledJob',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    projectId: text('projectId').notNull(),
    targetType: text('targetType').notNull(),
    targetId: text('targetId').notNull(),
    cron: text('cron').notNull(),
    timezone: text('timezone').notNull(),
    nextRunAt: timestamp('nextRunAt', { precision: 3 }).notNull(),
    lastSlot: timestamp('lastSlot', { precision: 3 }),
    currentSlot: timestamp('currentSlot', { precision: 3 }),
    attempts: integer('attempts').notNull().default(0),
    lastError: text('lastError'),
    active: boolean('active').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('ScheduledJob_active_nextRunAt_idx').on(table.active, table.nextRunAt),
    index('ScheduledJob_projectId_idx').on(table.projectId),
    uniqueIndex('ScheduledJob_targetType_targetId_key').on(table.targetType, table.targetId),
  ],
)

export const bugReport = pgTable(
  'BugReport',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    source: text('source').notNull(),
    kind: text('kind').notNull(),
    title: text('title').notNull(),
    summary: text('summary'),
    sessionData: text('sessionData'),
    sessionTruncated: boolean('sessionTruncated').notNull().default(false),
    agent: text('agent'),
    contactEmail: text('contactEmail'),
    cliVersion: text('cliVersion'),
    linkedProjectId: text('linkedProjectId'),
    metadata: jsonb('metadata'),
  },
  (table) => [index('BugReport_createdAt_idx').on(table.createdAt)],
)

export const idempotencyReceipt = pgTable(
  'IdempotencyReceipt',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    scopeId: text('scopeId').notNull(),
    key: text('key').notNull(),
    claimId: text('claimId').notNull(),
    requestFingerprint: text('requestFingerprint').notNull(),
    responseStatus: integer('responseStatus'),
    responseBody: text('responseBody'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    heartbeatAt: timestamp('heartbeatAt', { precision: 3 }).notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('IdempotencyReceipt_expiresAt_idx').on(table.expiresAt),
    uniqueIndex('IdempotencyReceipt_scopeId_key_key').on(table.scopeId, table.key),
  ],
)

export const gatewayRealtimeSession = pgTable(
  'GatewayRealtimeSession',
  {
    id: text('id').primaryKey(),
    projectId: text('projectId').notNull(),
    organizationId: text('organizationId').notNull(),
    virtualKeyId: text('virtualKeyId').notNull(),
    modelProviderId: text('modelProviderId').notNull(),
    vendor: text('vendor').notNull(),
    agentId: text('agentId'),
    model: text('model').notNull(),
    requestedModel: text('requestedModel'),
    vendorConversationId: text('vendorConversationId'),
    status: gatewayRealtimeSessionStatusEnum('status').notNull().default('OPEN'),
    mintedAt: timestamp('mintedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    closedAt: timestamp('closedAt', { precision: 3 }),
    closeReason: text('closeReason'),
    traceId: text('traceId'),
    vendorCostRaw: jsonb('vendorCostRaw'),
    kind: text('kind'),
    metering: text('metering'),
    credentialExpiresAt: timestamp('credentialExpiresAt', { precision: 3 }),
    transcriptionModel: text('transcriptionModel'),
    endUserId: text('endUserId'),
    lastReportAt: timestamp('lastReportAt', { precision: 3 }),
    reportedCostNanoUsd: bigint('reportedCostNanoUsd', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    reportCount: integer('reportCount').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx').on(
      table.virtualKeyId,
      table.status,
      table.mintedAt,
    ),
    index('GatewayRealtimeSession_projectId_status_mintedAt_idx').on(
      table.projectId,
      table.status,
      table.mintedAt,
    ),
    index('GatewayRealtimeSession_status_metering_mintedAt_idx').on(
      table.status,
      table.metering,
      table.mintedAt,
    ),
    uniqueIndex('GatewayRealtimeSession_vendor_vendorConversationId_key').on(
      table.vendor,
      table.vendorConversationId,
    ),
  ],
)

export const gatewayRealtimeSessionReport = pgTable(
  'GatewayRealtimeSessionReport',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    sessionId: text('sessionId').notNull(),
    reportKey: text('reportKey').notNull(),
    projectId: text('projectId').notNull(),
    model: text('model').notNull(),
    usage: jsonb('usage').notNull(),
    costNanoUsd: bigint('costNanoUsd', { mode: 'bigint' }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('GatewayRealtimeSessionReport_projectId_idx').on(table.projectId),
    uniqueIndex('GatewayRealtimeSessionReport_sessionId_reportKey_key').on(
      table.sessionId,
      table.reportKey,
    ),
  ],
)

export const systemMigrationTenantState = pgTable(
  'SystemMigrationTenantState',
  {
    migrationName: text('migrationName').notNull(),
    tenantId: text('tenantId').notNull(),
    status: text('status').notNull(),
    report: jsonb('report'),
    occurredAt: timestamp('occurredAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'SystemMigrationTenantState_pkey',
      columns: [table.migrationName, table.tenantId],
    }),
    index('SystemMigrationTenantState_migrationName_status_idx').on(
      table.migrationName,
      table.status,
    ),
  ],
)

export const systemMigrationEnrollment = pgTable(
  'SystemMigrationEnrollment',
  {
    organizationId: text('organizationId').notNull(),
    migrationName: text('migrationName').notNull(),
    enrolledByUserId: text('enrolledByUserId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'SystemMigrationEnrollment_pkey',
      columns: [table.organizationId, table.migrationName],
    }),
    index('SystemMigrationEnrollment_migrationName_idx').on(table.migrationName),
  ],
)

export const storedObject = pgTable(
  'StoredObject',
  {
    tenantId: text('tenantId').notNull(),
    id: text('id').notNull(),
    status: text('status').notNull(),
    purpose: text('purpose').notNull(),
    ownerKind: text('ownerKind').notNull(),
    ownerId: text('ownerId').notNull(),
    filename: text('filename').notNull(),
    sha256: text('sha256').notNull(),
    sizeBytes: bigint('sizeBytes', { mode: 'bigint' }).notNull(),
    mediaType: text('mediaType').notNull(),
    mediaTypeVerified: boolean('mediaTypeVerified').notNull().default(false),
    storageProvider: text('storageProvider'),
    storageDestinationId: text('storageDestinationId'),
    storageProviderRelativeId: text('storageProviderRelativeId'),
    generation: integer('generation').notNull().default(0),
    audiences: jsonb('audiences').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    availableAt: timestamp('availableAt', { precision: 3 }),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    source: text('source').notNull(),
    legacyFingerprint: text('legacyFingerprint'),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'StoredObject_pkey', columns: [table.tenantId, table.id] }),
    index('StoredObject_tenantId_status_id_idx').on(table.tenantId, table.status, table.id),
    index('StoredObject_ownerKind_ownerId_status_idx').on(
      table.ownerKind,
      table.ownerId,
      table.status,
    ),
    index('StoredObject_status_expiresAt_idx').on(table.status, table.expiresAt),
  ],
)

export const discoveredPerson = pgTable(
  'DiscoveredPerson',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    provider: text('provider').notNull(),
    rawActorId: text('rawActorId').notNull(),
    displayText: text('displayText').notNull(),
    kind: text('kind').notNull(),
    department: text('department'),
    firstSeenAt: timestamp('firstSeenAt', { precision: 3 }).notNull(),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 }).notNull(),
    erasedAt: timestamp('erasedAt', { precision: 3 }),
    moneyRowsPendingAt: timestamp('moneyRowsPendingAt', { precision: 3 }),
    moneyRebuildSince: text('moneyRebuildSince'),
    suspendedAt: timestamp('suspendedAt', { precision: 3 }),
    suspendedReason: text('suspendedReason'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DiscoveredPerson_organizationId_lastSeenAt_idx').on(
      table.organizationId,
      table.lastSeenAt,
    ),
    uniqueIndex('DiscoveredPerson_organizationId_provider_rawActorId_key').on(
      table.organizationId,
      table.provider,
      table.rawActorId,
    ),
  ],
)

export const discoveredAgent = pgTable(
  'DiscoveredAgent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    provider: text('provider').notNull(),
    rawAgentId: text('rawAgentId').notNull(),
    displayText: text('displayText').notNull(),
    metadata: jsonb('metadata').notNull(),
    firstSeenAt: timestamp('firstSeenAt', { precision: 3 }).notNull(),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('DiscoveredAgent_organizationId_lastSeenAt_idx').on(
      table.organizationId,
      table.lastSeenAt,
    ),
    uniqueIndex('DiscoveredAgent_organizationId_provider_rawAgentId_key').on(
      table.organizationId,
      table.provider,
      table.rawAgentId,
    ),
  ],
)

export const identityMatch = pgTable(
  'IdentityMatch',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    discoveredPersonId: text('discoveredPersonId').notNull(),
    userId: text('userId'),
    evidenceKind: text('evidenceKind').notNull(),
    validFrom: timestamp('validFrom', { precision: 3 }).notNull(),
    validTo: timestamp('validTo', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('IdentityMatch_organizationId_discoveredPersonId_idx').on(
      table.organizationId,
      table.discoveredPersonId,
    ),
    index('IdentityMatch_organizationId_userId_idx').on(table.organizationId, table.userId),
  ],
)

export const governanceTenantHistory = pgTable(
  'GovernanceTenantHistory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    tenantId: text('tenantId').notNull(),
    firstUsedAt: timestamp('firstUsedAt', { precision: 3 }).notNull(),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('GovernanceTenantHistory_organizationId_idx').on(table.organizationId),
    uniqueIndex('GovernanceTenantHistory_organizationId_tenantId_key').on(
      table.organizationId,
      table.tenantId,
    ),
  ],
)

export const erasedIdentifierSuppression = pgTable(
  'ErasedIdentifierSuppression',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    provider: text('provider').notNull(),
    identifierHash: text('identifierHash').notNull(),
    erasedAt: timestamp('erasedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('ErasedIdentifierSuppression_organizationId_idx').on(table.organizationId),
    uniqueIndex('ErasedIdentifierSuppression_organizationId_provider_identif_key').on(
      table.organizationId,
      table.provider,
      table.identifierHash,
    ),
  ],
)

export const identityMatchSuggestion = pgTable(
  'IdentityMatchSuggestion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    organizationId: text('organizationId').notNull(),
    discoveredPersonId: text('discoveredPersonId').notNull(),
    userId: text('userId').notNull(),
    score: doublePrecision('score').notNull(),
    computedAt: timestamp('computedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('IdentityMatchSuggestion_organizationId_score_idx').on(table.organizationId, table.score),
    uniqueIndex('IdentityMatchSuggestion_organizationId_discoveredPersonId_u_key').on(
      table.organizationId,
      table.discoveredPersonId,
      table.userId,
    ),
  ],
)

export const connectedSeatChange = pgTable(
  'ConnectedSeatChange',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    licenseId: text('licenseId').notNull(),
    accountId: text('accountId'),
    organizationId: text('organizationId'),
    changedAt: timestamp('changedAt', { precision: 3 }).notNull(),
    addedSeats: integer('addedSeats').notNull(),
    unitAmountCents: integer('unitAmountCents').notNull(),
    amountCents: integer('amountCents').notNull(),
    currency: currencyEnum('currency'),
    stripeInvoiceId: text('stripeInvoiceId'),
    state: connectedSeatChangeStateEnum('state').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('ConnectedSeatChange_licenseId_key').on(table.licenseId),
    index('ConnectedSeatChange_accountId_idx').on(table.accountId),
    index('ConnectedSeatChange_organizationId_idx').on(table.organizationId),
    index('ConnectedSeatChange_state_idx').on(table.state),
  ],
)

export const connectedStatement = pgTable(
  'ConnectedStatement',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    accountId: text('accountId').notNull(),
    month: timestamp('month', { precision: 3 }).notNull(),
    sentAt: timestamp('sentAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('ConnectedStatement_accountId_idx').on(table.accountId),
    uniqueIndex('ConnectedStatement_accountId_month_key').on(table.accountId, table.month),
  ],
)

export const licenseRenewalClaim = pgTable('LicenseRenewalClaim', {
  stripeInvoiceId: text('stripeInvoiceId').primaryKey(),
  stripeSubscriptionId: text('stripeSubscriptionId').notNull(),
  claimToken: text('claimToken').notNull(),
  claimedAt: timestamp('claimedAt', { precision: 3 }).notNull(),
  issuedLicense: jsonb('issuedLicense'),
  deliveredAt: timestamp('deliveredAt', { precision: 3 }),
  licenseId: text('licenseId'),
})

export const instanceIdentity = pgTable(
  'InstanceIdentity',
  {
    id: text('id').primaryKey().default('self'),
    instanceId: text('instanceId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastReportAt: timestamp('lastReportAt', { precision: 3 }),
    lastReportError: text('lastReportError'),
    optionalMetricsOptOut: boolean('optionalMetricsOptOut').notNull().default(false),
    hostnameOptOut: boolean('hostnameOptOut').notNull().default(false),
    upgradeDiagnosticsOptOut: boolean('upgradeDiagnosticsOptOut').notNull().default(false),
  },
  (table) => [uniqueIndex('InstanceIdentity_instanceId_key').on(table.instanceId)],
)

export const selfHostedInstance = pgTable(
  'SelfHostedInstance',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    instanceId: text('instanceId').notNull(),
    firstSeenAt: timestamp('firstSeenAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 }).notNull(),
    version: text('version'),
    installMethod: text('installMethod'),
    chartVersion: text('chartVersion'),
    hostname: text('hostname'),
    environment: text('environment'),
    installedAt: timestamp('installedAt', { precision: 3 }),
    reportSchemaVersion: integer('reportSchemaVersion'),
    organizationId: text('organizationId'),
    issuedLicenseId: text('issuedLicenseId'),
    userEmailDomains: jsonb('userEmailDomains'),
    userDomains: text('userDomains').array().default([]),
    latestReport: jsonb('latestReport'),
    optionalMetricsReported: boolean('optionalMetricsReported').notNull().default(true),
    hostnameReported: boolean('hostnameReported').notNull().default(true),
    reportCount: integer('reportCount').notNull().default(0),
    lastUnknownFields: integer('lastUnknownFields').notNull().default(0),
    raisedSignals: text('raisedSignals').array().default([]),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('SelfHostedInstance_instanceId_key').on(table.instanceId),
    index('SelfHostedInstance_lastSeenAt_idx').on(table.lastSeenAt),
    index('SelfHostedInstance_organizationId_idx').on(table.organizationId),
    index('SelfHostedInstance_issuedLicenseId_idx').on(table.issuedLicenseId),
  ],
)

export const selfHostedInstanceDiagnostic = pgTable(
  'SelfHostedInstanceDiagnostic',
  {
    id: text('id').primaryKey(),
    instanceId: text('instanceId').notNull(),
    stepId: text('stepId').notNull(),
    stepDescription: text('stepDescription'),
    release: text('release'),
    attempt: integer('attempt').notNull(),
    trigger: text('trigger').notNull(),
    byteLength: integer('byteLength').notNull(),
    objectKey: text('objectKey').notNull(),
    receivedAt: timestamp('receivedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    slackAnnouncedAt: timestamp('slackAnnouncedAt', { precision: 3 }),
  },
  (table) => [
    index('SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx').on(
      table.instanceId,
      table.receivedAt,
    ),
  ],
)

export const selfHostedNotice = pgTable('SelfHostedNotice', {
  id: text('id').primaryKey(),
  severity: text('severity').notNull(),
  title: text('title').notNull(),
  body: text('body').notNull(),
  url: text('url'),
  fixedIn: text('fixedIn'),
  status: text('status').notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }),
  publishedAt: timestamp('publishedAt', { precision: 3 }),
  publishedBy: text('publishedBy'),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const selfHostedInstanceReport = pgTable(
  'SelfHostedInstanceReport',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    instanceId: text('instanceId').notNull(),
    receivedAt: timestamp('receivedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    version: text('version'),
    reportSchemaVersion: integer('reportSchemaVersion'),
    unknownFields: integer('unknownFields').notNull().default(0),
    payload: jsonb('payload').notNull(),
  },
  (table) => [
    index('SelfHostedInstanceReport_instanceId_receivedAt_idx').on(
      table.instanceId,
      table.receivedAt,
    ),
    index('SelfHostedInstanceReport_receivedAt_idx').on(table.receivedAt),
  ],
)

export const webPushSubscription = pgTable(
  'WebPushSubscription',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => nanoid()),
    userId: text('userId').notNull(),
    endpoint: text('endpoint').notNull(),
    p256dh: text('p256dh').notNull(),
    auth: text('auth').notNull(),
    userAgent: text('userAgent'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastSuccessAt: timestamp('lastSuccessAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('WebPushSubscription_endpoint_key').on(table.endpoint),
    index('WebPushSubscription_userId_idx').on(table.userId),
  ],
)

export const webPushVapidKey = pgTable('WebPushVapidKey', {
  id: text('id').primaryKey().default('self'),
  publicKey: text('publicKey').notNull(),
  privateKeyEncrypted: text('privateKeyEncrypted').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const signInProviderMount = pgTable('SignInProviderMount', {
  id: text('id').primaryKey().default('self'),
  provider: text('provider'),
  mounted: boolean('mounted').notNull(),
  lastAppliedEventId: text('lastAppliedEventId').notNull(),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const relations = defineRelations(
  {
    account,
    accountCredential,
    session,
    userErasure,
    user,
    verificationToken,
    identifier,
    identitySignInSwitch,
    identifierReservation,
    identityProjectionCursor,
    twoFactor,
    signInAttemptLock,
    passkey,
    mfaEnrollment,
    ssoConnection,
    ssoConnectionRegistrationSlot,
    ssoConnectionReproofCursor,
    ssoCredential,
    ssoVerifiedDomain,
    ssoVerifiedDomainHolder,
    ssoAuthenticationActivity,
    ssoProvider,
    ssoBreakGlassBinding,
    ssoActivationRecoveryReservation,
    joinRequest,
    teamUser,
    organizationUser,
    seatHold,
    seatRequest,
    organizationSeatLease,
    organizationAccessNotice,
    team,
    organization,
    issuedLicense,
    organizationLicense,
    activationCode,
    connectedBillingAccount,
    connectedCreditGrant,
    connectedInvoice,
    scimRequestLog,
    scimToken,
    scimExternalId,
    scimUserResource,
    scimDirectoryUser,
    scimSyncState,
    scimSsoConnectionView,
    organizationSeatPeak,
    organizationSeatPeakHolder,
    organizationSeatPeakMonth,
    project,
    department,
    departmentMembershipHistory,
    platformToolPolicy,
    organizationInvite,
    monitor,
    cost,
    topic,
    dataset,
    datasetRecord,
    datasetVersion,
    datasetVersionRun,
    dashboard,
    dashboardFavourite,
    dashboardsV2,
    dashboardWidget,
    insightProjection,
    insightReaderProjection,
    insightDailyScheduleProjection,
    savedView,
    customGraph,
    batchEvaluation,
    trigger,
    slackIntegration,
    slackConnectionClaim,
    webhookEndpoint,
    webhookEndpointDelivery,
    experiment,
    experimentVersion,
    annotation,
    modelProvider,
    modelProviderScope,
    modelDefaultConfig,
    modelDefaultConfigScope,
    githubInstallation,
    githubPullRequest,
    githubBranchPullRequestCheck,
    langyConversationProjection,
    langyTurnRequest,
    langyActiveTurn,
    langyConversationTurnProjection,
    langyMessageProjection,
    projectSecret,
    triggerSent,
    triggerLatestEvaluation,
    emailSuppression,
    annotationScore,
    annotationQueue,
    annotationQueueMembers,
    annotationQueueScores,
    annotationQueueItem,
    shareLink,
    pinnedTrace,
    traceEditOverlay,
    traceIngestSourceBilling,
    retentionPolicy,
    dataPrivacyPolicy,
    instantEvalJudgeProject,
    instantEvalJudgeUsageBilling,
    instantEvalJudgeSpend,
    customLLMModelCost,
    workflow,
    workflowVersion,
    auditLog,
    llmPromptConfig,
    llmPromptConfigVersion,
    promptTagAssignment,
    promptTag,
    analytics,
    customRole,
    notification,
    agent,
    agentVersion,
    agentVersionLabel,
    evaluator,
    evaluatorVersion,
    evaluatorVersionLabel,
    scenario,
    scenarioVersion,
    simulationSuite,
    subscription,
    invoice,
    invoiceItem,
    group,
    groupMembership,
    authzUserStanding,
    authzEngineSwitch,
    authzOrganizationPolicy,
    roleBinding,
    grant,
    grantUsage,
    role,
    apiKey,
    signingKey,
    projectActiveDay,
    nurturingProject,
    nurturingUncopiedProject,
    nurturingOrganizationMember,
    nurturingOrganizationCreated,
    nurturingOrganization,
    nurturingProjectUsageView,
    nurturingProjectTraceDay,
    nurturingPersonalProject,
    nurturingTracedProject,
    billingMeterCheckpoint,
    virtualKey,
    virtualKeyScope,
    routingPolicy,
    routingPolicyScope,
    gatewayGuardrail,
    ingestionSource,
    anomalyRule,
    anomalyAlert,
    aiToolEntry,
    aiToolEntryTeam,
    aiToolEntryDepartment,
    ingestionTemplate,
    gatewayConnectUpstream,
    gatewayTraceExportKey,
    gatewayBudget,
    gatewayBudgetBucketBoundary,
    gatewayBudgetLedger,
    gatewayChangeEvent,
    gatewayCacheRule,
    featureFlag,
    featureFlagExperimentSetting,
    processManagerInstance,
    processManagerInbox,
    processManagerOutbox,
    processManagerOutboxAttempt,
    topicClusteringRunProjection,
    topicModelProjection,
    topicClusteringRunHistoryProjection,
    ingestionPullRunProjection,
    scheduledJob,
    bugReport,
    idempotencyReceipt,
    gatewayRealtimeSession,
    gatewayRealtimeSessionReport,
    systemMigrationTenantState,
    systemMigrationEnrollment,
    storedObject,
    discoveredPerson,
    discoveredAgent,
    identityMatch,
    governanceTenantHistory,
    erasedIdentifierSuppression,
    identityMatchSuggestion,
    connectedSeatChange,
    connectedStatement,
    licenseRenewalClaim,
    instanceIdentity,
    selfHostedInstance,
    selfHostedInstanceDiagnostic,
    selfHostedNotice,
    selfHostedInstanceReport,
    webPushSubscription,
    webPushVapidKey,
    signInProviderMount,
  },
  (r) => ({
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    accountCredential: {
      user: r.one.user({ from: r.accountCredential.userId, to: r.user.id, optional: false }),
    },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    user: {
      accounts: r.many.account(),
      accountCredentials: r.many.accountCredential(),
      sessions: r.many.session(),
      twoFactors: r.many.twoFactor(),
      passkeys: r.many.passkey(),
      teamMemberships: r.many.teamUser(),
      orgMemberships: r.many.organizationUser(),
      scimUserResources: r.many.scimUserResource(),
      Annotation: r.many.annotation(),
      shareLinks: r.many.shareLink(),
      Workflow: r.many.workflow(),
      WorkflowVersion: r.many.workflowVersion(),
      annotationQueues: r.many.annotationQueueMembers(),
      assignedQueueItems: r.many.annotationQueueItem({ alias: 'AssignedTo' }),
      createdQueueItems: r.many.annotationQueueItem({ alias: 'CreatedBy' }),
      llmPromptConfigVersions: r.many.llmPromptConfigVersion(),
      scenarios: r.many.scenario(),
      inviteRequests: r.many.organizationInvite(),
      secretsCreated: r.many.projectSecret({ alias: 'secretCreatedBy' }),
      secretsUpdated: r.many.projectSecret({ alias: 'secretUpdatedBy' }),
      savedViews: r.many.savedView(),
      versionTagsCreated: r.many.promptTagAssignment({ alias: 'versionTagCreatedBy' }),
      versionTagsUpdated: r.many.promptTagAssignment({ alias: 'versionTagUpdatedBy' }),
      promptTagsCreated: r.many.promptTag({ alias: 'promptTagCreatedBy' }),
      promptTagsUpdated: r.many.promptTag({ alias: 'promptTagUpdatedBy' }),
      groupMemberships: r.many.groupMembership(),
      roleBindings: r.many.roleBinding(),
      scimExternalIds: r.many.scimExternalId(),
      apiKeys: r.many.apiKey(),
      gatewayBudgetsCreated: r.many.gatewayBudget(),
      principalVirtualKeys: r.many.virtualKey(),
      personalTeams: r.many.team(),
      personalProjects: r.many.project(),
      routingPoliciesCreated: r.many.routingPolicy({ alias: 'RoutingPolicyCreatedBy' }),
      routingPoliciesUpdated: r.many.routingPolicy({ alias: 'RoutingPolicyUpdatedBy' }),
      gatewayGuardrailsCreated: r.many.gatewayGuardrail({ alias: 'GatewayGuardrailCreatedBy' }),
      gatewayGuardrailsUpdated: r.many.gatewayGuardrail({ alias: 'GatewayGuardrailUpdatedBy' }),
      ingestionSourcesCreated: r.many.ingestionSource(),
      anomalyRulesCreated: r.many.anomalyRule(),
      pinnedTraces: r.many.pinnedTrace(),
      traceEditOverlaysCreated: r.many.traceEditOverlay({ alias: 'TraceEditOverlayCreatedBy' }),
      traceEditOverlaysUpdated: r.many.traceEditOverlay({ alias: 'TraceEditOverlayUpdatedBy' }),
    },
    twoFactor: { user: r.one.user({ from: r.twoFactor.userId, to: r.user.id, optional: false }) },
    passkey: { user: r.one.user({ from: r.passkey.userId, to: r.user.id, optional: false }) },
    ssoConnection: {
      reproofCursor: r.one.ssoConnectionReproofCursor({
        from: r.ssoConnection.id,
        to: r.ssoConnectionReproofCursor.connectionId,
      }),
    },
    ssoConnectionReproofCursor: {
      connection: r.one.ssoConnection({
        from: r.ssoConnectionReproofCursor.connectionId,
        to: r.ssoConnection.id,
        optional: false,
      }),
    },
    ssoVerifiedDomain: { holders: r.many.ssoVerifiedDomainHolder() },
    ssoVerifiedDomainHolder: {
      ownership: r.one.ssoVerifiedDomain({
        from: [r.ssoVerifiedDomainHolder.domain, r.ssoVerifiedDomainHolder.organizationId],
        to: [r.ssoVerifiedDomain.domain, r.ssoVerifiedDomain.organizationId],
        optional: false,
      }),
    },
    teamUser: {
      assignedRole: r.one.customRole({ from: r.teamUser.assignedRoleId, to: r.customRole.id }),
      user: r.one.user({ from: r.teamUser.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.teamUser.teamId, to: r.team.id, optional: false }),
    },
    organizationUser: {
      user: r.one.user({ from: r.organizationUser.userId, to: r.user.id, optional: false }),
      organization: r.one.organization({
        from: r.organizationUser.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    team: {
      members: r.many.teamUser(),
      organization: r.one.organization({
        from: r.team.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      projects: r.many.project(),
      ownerUser: r.one.user({ from: r.team.ownerUserId, to: r.user.id }),
      ingestionSources: r.many.ingestionSource(),
      aiToolEntryTeams: r.many.aiToolEntryTeam(),
    },
    organization: {
      members: r.many.organizationUser(),
      teams: r.many.team(),
      webhookEndpoints: r.many.webhookEndpoint(),
      OrganizationInvite: r.many.organizationInvite(),
      CustomRoles: r.many.customRole(),
      notifications: r.many.notification(),
      subscriptions: r.many.subscription(),
      scimTokens: r.many.scimToken(),
      scimRequestLog: r.many.scimRequestLog(),
      promptTags: r.many.promptTag(),
      groups: r.many.group(),
      roleBindings: r.many.roleBinding(),
      apiKeys: r.many.apiKey(),
      issuedLicenses: r.many.issuedLicense(),
      connectedBilling: r.one.connectedBillingAccount({
        from: r.organization.id,
        to: r.connectedBillingAccount.organizationId,
      }),
      gatewayChangeEvents: r.many.gatewayChangeEvent(),
      routingPolicies: r.many.routingPolicy(),
      ingestionSources: r.many.ingestionSource(),
      anomalyRules: r.many.anomalyRule(),
      anomalyAlerts: r.many.anomalyAlert(),
      aiToolEntries: r.many.aiToolEntry(),
      ingestionTemplates: r.many.ingestionTemplate(),
      githubInstallations: r.many.githubInstallation(),
      githubPullRequests: r.many.githubPullRequest(),
      githubBranchPrChecks: r.many.githubBranchPullRequestCheck(),
    },
    issuedLicense: {
      organization: r.one.organization({
        from: r.issuedLicense.organizationId,
        to: r.organization.id,
      }),
    },
    connectedBillingAccount: {
      organization: r.one.organization({
        from: r.connectedBillingAccount.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    scimRequestLog: {
      organization: r.one.organization({
        from: r.scimRequestLog.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    scimToken: {
      organization: r.one.organization({
        from: r.scimToken.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    scimExternalId: {
      user: r.one.user({ from: r.scimExternalId.userId, to: r.user.id, optional: false }),
    },
    scimUserResource: {
      user: r.one.user({ from: r.scimUserResource.userId, to: r.user.id, optional: false }),
    },
    project: {
      team: r.one.team({ from: r.project.teamId, to: r.team.id, optional: false }),
      checks: r.many.monitor(),
      costs: r.many.cost(),
      topics: r.many.topic(),
      datasets: r.many.dataset(),
      datasetRecords: r.many.datasetRecord(),
      customGraphs: r.many.customGraph(),
      dashboards: r.many.dashboard(),
      batchEvaluations: r.many.batchEvaluation(),
      triggers: r.many.trigger(),
      experiments: r.many.experiment(),
      annotations: r.many.annotation(),
      projectSecrets: r.many.projectSecret(),
      TriggerSent: r.many.triggerSent(),
      webhookDeliveries: r.many.webhookEndpointDelivery(),
      annotationScores: r.many.annotationScore(),
      shareLinks: r.many.shareLink(),
      workflows: r.many.workflow(),
      WorkflowVersion: r.many.workflowVersion(),
      AnnotationQueue: r.many.annotationQueue(),
      AnnotationQueueItem: r.many.annotationQueueItem(),
      llmPromptConfigs: r.many.llmPromptConfig(),
      analytics: r.many.analytics(),
      notifications: r.many.notification(),
      agents: r.many.agent(),
      evaluators: r.many.evaluator(),
      scenarios: r.many.scenario(),
      suiteConfigurations: r.many.simulationSuite(),
      savedViews: r.many.savedView(),
      gatewayGuardrails: r.many.gatewayGuardrail(),
      gatewayChangeEvents: r.many.gatewayChangeEvent(),
      pinnedTraces: r.many.pinnedTrace(),
      traceEditOverlays: r.many.traceEditOverlay(),
      emailSuppressions: r.many.emailSuppression(),
      ownerUser: r.one.user({ from: r.project.ownerUserId, to: r.user.id }),
    },
    organizationInvite: {
      organization: r.one.organization({
        from: r.organizationInvite.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      requestedByUser: r.one.user({ from: r.organizationInvite.requestedBy, to: r.user.id }),
    },
    monitor: {
      project: r.one.project({ from: r.monitor.projectId, to: r.project.id, optional: false }),
      experiment: r.one.experiment({ from: r.monitor.experimentId, to: r.experiment.id }),
      evaluator: r.one.evaluator({ from: r.monitor.evaluatorId, to: r.evaluator.id }),
    },
    cost: { project: r.one.project({ from: r.cost.projectId, to: r.project.id, optional: false }) },
    topic: {
      project: r.one.project({ from: r.topic.projectId, to: r.project.id, optional: false }),
      parent: r.one.topic({ from: r.topic.parentId, to: r.topic.id, alias: 'Subtopics' }),
      subtopics: r.many.topic({ alias: 'Subtopics' }),
    },
    dataset: {
      project: r.one.project({ from: r.dataset.projectId, to: r.project.id, optional: false }),
      datasetRecords: r.many.datasetRecord(),
      batchEvaluations: r.many.batchEvaluation(),
    },
    datasetRecord: {
      dataset: r.one.dataset({
        from: r.datasetRecord.datasetId,
        to: r.dataset.id,
        optional: false,
      }),
      project: r.one.project({
        from: r.datasetRecord.projectId,
        to: r.project.id,
        optional: false,
      }),
    },
    dashboard: {
      project: r.one.project({ from: r.dashboard.projectId, to: r.project.id, optional: false }),
      graphs: r.many.customGraph(),
    },
    savedView: {
      project: r.one.project({ from: r.savedView.projectId, to: r.project.id, optional: false }),
      user: r.one.user({ from: r.savedView.userId, to: r.user.id }),
    },
    customGraph: {
      project: r.one.project({ from: r.customGraph.projectId, to: r.project.id, optional: false }),
      dashboard: r.one.dashboard({ from: r.customGraph.dashboardId, to: r.dashboard.id }),
      trigger: r.one.trigger({ from: r.customGraph.id, to: r.trigger.customGraphId }),
      TriggerSent: r.many.triggerSent(),
    },
    batchEvaluation: {
      experiment: r.one.experiment({
        from: r.batchEvaluation.experimentId,
        to: r.experiment.id,
        optional: false,
      }),
      project: r.one.project({
        from: r.batchEvaluation.projectId,
        to: r.project.id,
        optional: false,
      }),
      dataset: r.one.dataset({
        from: r.batchEvaluation.datasetId,
        to: r.dataset.id,
        optional: false,
      }),
    },
    trigger: {
      project: r.one.project({ from: r.trigger.projectId, to: r.project.id, optional: false }),
      TriggerSent: r.many.triggerSent(),
      webhookDeliveries: r.many.webhookEndpointDelivery(),
      latestEvaluation: r.one.triggerLatestEvaluation({
        from: r.trigger.id,
        to: r.triggerLatestEvaluation.triggerId,
      }),
      customGraph: r.one.customGraph({ from: r.trigger.customGraphId, to: r.customGraph.id }),
    },
    webhookEndpoint: {
      organization: r.one.organization({
        from: r.webhookEndpoint.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      deliveries: r.many.webhookEndpointDelivery(),
    },
    webhookEndpointDelivery: {
      endpoint: r.one.webhookEndpoint({
        from: r.webhookEndpointDelivery.endpointId,
        to: r.webhookEndpoint.id,
      }),
      project: r.one.project({ from: r.webhookEndpointDelivery.projectId, to: r.project.id }),
      trigger: r.one.trigger({ from: r.webhookEndpointDelivery.triggerId, to: r.trigger.id }),
    },
    experiment: {
      project: r.one.project({ from: r.experiment.projectId, to: r.project.id, optional: false }),
      workflow: r.one.workflow({ from: r.experiment.workflowId, to: r.workflow.id }),
      batchEvaluations: r.many.batchEvaluation(),
      monitor: r.one.monitor({ from: r.experiment.id, to: r.monitor.experimentId }),
    },
    annotation: {
      project: r.one.project({ from: r.annotation.projectId, to: r.project.id, optional: false }),
      user: r.one.user({ from: r.annotation.userId, to: r.user.id }),
    },
    modelProvider: { scopes: r.many.modelProviderScope() },
    modelProviderScope: {
      modelProvider: r.one.modelProvider({
        from: r.modelProviderScope.modelProviderId,
        to: r.modelProvider.id,
        optional: false,
      }),
    },
    modelDefaultConfig: { scopes: r.many.modelDefaultConfigScope() },
    modelDefaultConfigScope: {
      config: r.one.modelDefaultConfig({
        from: r.modelDefaultConfigScope.configId,
        to: r.modelDefaultConfig.id,
        optional: false,
      }),
    },
    githubInstallation: {
      organization: r.one.organization({
        from: r.githubInstallation.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    githubPullRequest: {
      organization: r.one.organization({
        from: r.githubPullRequest.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    githubBranchPullRequestCheck: {
      organization: r.one.organization({
        from: r.githubBranchPullRequestCheck.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    projectSecret: {
      project: r.one.project({
        from: r.projectSecret.projectId,
        to: r.project.id,
        optional: false,
      }),
      createdBy: r.one.user({
        from: r.projectSecret.createdById,
        to: r.user.id,
        optional: false,
        alias: 'secretCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.projectSecret.updatedById,
        to: r.user.id,
        optional: false,
        alias: 'secretUpdatedBy',
      }),
    },
    triggerSent: {
      customGraph: r.one.customGraph({ from: r.triggerSent.customGraphId, to: r.customGraph.id }),
      project: r.one.project({ from: r.triggerSent.projectId, to: r.project.id, optional: false }),
      trigger: r.one.trigger({ from: r.triggerSent.triggerId, to: r.trigger.id, optional: false }),
    },
    triggerLatestEvaluation: {
      trigger: r.one.trigger({
        from: r.triggerLatestEvaluation.triggerId,
        to: r.trigger.id,
        optional: false,
      }),
    },
    emailSuppression: {
      project: r.one.project({
        from: r.emailSuppression.projectId,
        to: r.project.id,
        optional: false,
      }),
    },
    annotationScore: {
      project: r.one.project({
        from: r.annotationScore.projectId,
        to: r.project.id,
        optional: false,
      }),
      AnnotationQueueScores: r.many.annotationQueueScores(),
    },
    annotationQueue: {
      project: r.one.project({
        from: r.annotationQueue.projectId,
        to: r.project.id,
        optional: false,
      }),
      members: r.many.annotationQueueMembers(),
      AnnotationQueueScores: r.many.annotationQueueScores(),
      AnnotationQueueItems: r.many.annotationQueueItem(),
    },
    annotationQueueMembers: {
      user: r.one.user({ from: r.annotationQueueMembers.userId, to: r.user.id, optional: false }),
      annotationQueue: r.one.annotationQueue({
        from: r.annotationQueueMembers.annotationQueueId,
        to: r.annotationQueue.id,
        optional: false,
      }),
    },
    annotationQueueScores: {
      annotationScore: r.one.annotationScore({
        from: r.annotationQueueScores.annotationScoreId,
        to: r.annotationScore.id,
        optional: false,
      }),
      annotationQueue: r.one.annotationQueue({
        from: r.annotationQueueScores.annotationQueueId,
        to: r.annotationQueue.id,
        optional: false,
      }),
    },
    annotationQueueItem: {
      annotationQueue: r.one.annotationQueue({
        from: r.annotationQueueItem.annotationQueueId,
        to: r.annotationQueue.id,
      }),
      user: r.one.user({ from: r.annotationQueueItem.userId, to: r.user.id, alias: 'AssignedTo' }),
      createdByUser: r.one.user({
        from: r.annotationQueueItem.createdByUserId,
        to: r.user.id,
        alias: 'CreatedBy',
      }),
      project: r.one.project({
        from: r.annotationQueueItem.projectId,
        to: r.project.id,
        optional: false,
      }),
    },
    shareLink: {
      project: r.one.project({ from: r.shareLink.projectId, to: r.project.id, optional: false }),
      user: r.one.user({ from: r.shareLink.userId, to: r.user.id }),
    },
    pinnedTrace: {
      project: r.one.project({ from: r.pinnedTrace.projectId, to: r.project.id, optional: false }),
      user: r.one.user({ from: r.pinnedTrace.userId, to: r.user.id }),
    },
    traceEditOverlay: {
      project: r.one.project({
        from: r.traceEditOverlay.projectId,
        to: r.project.id,
        optional: false,
      }),
      createdBy: r.one.user({
        from: r.traceEditOverlay.createdById,
        to: r.user.id,
        alias: 'TraceEditOverlayCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.traceEditOverlay.updatedById,
        to: r.user.id,
        alias: 'TraceEditOverlayUpdatedBy',
      }),
    },
    workflow: {
      project: r.one.project({ from: r.workflow.projectId, to: r.project.id, optional: false }),
      latestVersion: r.one.workflowVersion({
        from: r.workflow.latestVersionId,
        to: r.workflowVersion.id,
        alias: 'LatestVersion',
      }),
      currentVersion: r.one.workflowVersion({
        from: r.workflow.currentVersionId,
        to: r.workflowVersion.id,
        alias: 'CurrentVersion',
      }),
      versions: r.many.workflowVersion({ alias: 'Versions' }),
      publishedBy: r.one.user({ from: r.workflow.publishedById, to: r.user.id }),
      experiments: r.many.experiment(),
      copiedFrom: r.one.workflow({
        from: r.workflow.copiedFromWorkflowId,
        to: r.workflow.id,
        alias: 'WorkflowCopies',
      }),
      copiedWorkflows: r.many.workflow({ alias: 'WorkflowCopies' }),
      agents: r.many.agent(),
      evaluators: r.many.evaluator(),
    },
    workflowVersion: {
      author: r.one.user({ from: r.workflowVersion.authorId, to: r.user.id, optional: false }),
      project: r.one.project({
        from: r.workflowVersion.projectId,
        to: r.project.id,
        optional: false,
      }),
      workflow: r.one.workflow({
        from: r.workflowVersion.workflowId,
        to: r.workflow.id,
        optional: false,
        alias: 'Versions',
      }),
      parent: r.one.workflowVersion({
        from: r.workflowVersion.parentId,
        to: r.workflowVersion.id,
        alias: 'WorkflowVersionHistory',
      }),
      children: r.many.workflowVersion({ alias: 'WorkflowVersionHistory' }),
      WorkflowAsLatestVersion: r.many.workflow({ alias: 'LatestVersion' }),
      WorkflowAsCurrentVersion: r.many.workflow({ alias: 'CurrentVersion' }),
    },
    llmPromptConfig: {
      project: r.one.project({
        from: r.llmPromptConfig.projectId,
        to: r.project.id,
        optional: false,
      }),
      copiedFrom: r.one.llmPromptConfig({
        from: r.llmPromptConfig.copiedFromPromptId,
        to: r.llmPromptConfig.id,
        alias: 'PromptCopies',
      }),
      copiedPrompts: r.many.llmPromptConfig({ alias: 'PromptCopies' }),
      versions: r.many.llmPromptConfigVersion(),
      versionTags: r.many.promptTagAssignment(),
    },
    llmPromptConfigVersion: {
      author: r.one.user({ from: r.llmPromptConfigVersion.authorId, to: r.user.id }),
      config: r.one.llmPromptConfig({
        from: r.llmPromptConfigVersion.configId,
        to: r.llmPromptConfig.id,
        optional: false,
      }),
      versionTags: r.many.promptTagAssignment(),
    },
    promptTagAssignment: {
      config: r.one.llmPromptConfig({
        from: r.promptTagAssignment.configId,
        to: r.llmPromptConfig.id,
        optional: false,
      }),
      version: r.one.llmPromptConfigVersion({
        from: r.promptTagAssignment.versionId,
        to: r.llmPromptConfigVersion.id,
        optional: false,
      }),
      promptTag: r.one.promptTag({
        from: r.promptTagAssignment.tagId,
        to: r.promptTag.id,
        optional: false,
      }),
      createdBy: r.one.user({
        from: r.promptTagAssignment.createdById,
        to: r.user.id,
        alias: 'versionTagCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.promptTagAssignment.updatedById,
        to: r.user.id,
        alias: 'versionTagUpdatedBy',
      }),
    },
    promptTag: {
      organization: r.one.organization({
        from: r.promptTag.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      assignments: r.many.promptTagAssignment(),
      createdBy: r.one.user({
        from: r.promptTag.createdById,
        to: r.user.id,
        alias: 'promptTagCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.promptTag.updatedById,
        to: r.user.id,
        alias: 'promptTagUpdatedBy',
      }),
    },
    analytics: {
      project: r.one.project({ from: r.analytics.projectId, to: r.project.id, optional: false }),
    },
    customRole: {
      organization: r.one.organization({
        from: r.customRole.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      assignedUsers: r.many.teamUser(),
      roleBindings: r.many.roleBinding(),
    },
    notification: {
      organization: r.one.organization({
        from: r.notification.organizationId,
        to: r.organization.id,
      }),
      project: r.one.project({ from: r.notification.projectId, to: r.project.id }),
    },
    agent: {
      project: r.one.project({ from: r.agent.projectId, to: r.project.id, optional: false }),
      workflow: r.one.workflow({ from: r.agent.workflowId, to: r.workflow.id }),
      copiedFrom: r.one.agent({
        from: r.agent.copiedFromAgentId,
        to: r.agent.id,
        alias: 'AgentCopies',
      }),
      copiedAgents: r.many.agent({ alias: 'AgentCopies' }),
    },
    evaluator: {
      project: r.one.project({ from: r.evaluator.projectId, to: r.project.id, optional: false }),
      workflow: r.one.workflow({ from: r.evaluator.workflowId, to: r.workflow.id }),
      copiedFrom: r.one.evaluator({
        from: r.evaluator.copiedFromEvaluatorId,
        to: r.evaluator.id,
        alias: 'EvaluatorCopies',
      }),
      copiedEvaluators: r.many.evaluator({ alias: 'EvaluatorCopies' }),
      monitors: r.many.monitor(),
      gatewayGuardrails: r.many.gatewayGuardrail(),
    },
    scenario: {
      project: r.one.project({ from: r.scenario.projectId, to: r.project.id, optional: false }),
      testSuite: r.one.simulationSuite({ from: r.scenario.testSuiteId, to: r.simulationSuite.id }),
      versions: r.many.scenarioVersion(),
      lastUpdatedBy: r.one.user({ from: r.scenario.lastUpdatedById, to: r.user.id }),
    },
    scenarioVersion: {
      scenario: r.one.scenario({
        from: r.scenarioVersion.scenarioId,
        to: r.scenario.id,
        optional: false,
      }),
    },
    simulationSuite: {
      project: r.one.project({
        from: r.simulationSuite.projectId,
        to: r.project.id,
        optional: false,
      }),
      testSuiteScenarios: r.many.scenario(),
    },
    subscription: {
      organization: r.one.organization({
        from: r.subscription.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      invoices: r.many.invoice(),
    },
    invoice: {
      subscription: r.one.subscription({
        from: r.invoice.subscriptionId,
        to: r.subscription.id,
        optional: false,
      }),
      lineItems: r.many.invoiceItem(),
    },
    invoiceItem: {
      invoice: r.one.invoice({ from: r.invoiceItem.invoiceId, to: r.invoice.id, optional: false }),
    },
    group: {
      organization: r.one.organization({
        from: r.group.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      members: r.many.groupMembership(),
      roleBindings: r.many.roleBinding(),
    },
    groupMembership: {
      user: r.one.user({ from: r.groupMembership.userId, to: r.user.id, optional: false }),
      group: r.one.group({ from: r.groupMembership.groupId, to: r.group.id, optional: false }),
    },
    roleBinding: {
      organization: r.one.organization({
        from: r.roleBinding.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      user: r.one.user({ from: r.roleBinding.userId, to: r.user.id }),
      group: r.one.group({ from: r.roleBinding.groupId, to: r.group.id }),
      apiKey: r.one.apiKey({ from: r.roleBinding.apiKeyId, to: r.apiKey.id }),
      customRole: r.one.customRole({ from: r.roleBinding.customRoleId, to: r.customRole.id }),
    },
    apiKey: {
      user: r.one.user({ from: r.apiKey.userId, to: r.user.id }),
      organization: r.one.organization({
        from: r.apiKey.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      roleBindings: r.many.roleBinding(),
    },
    virtualKey: {
      principalUser: r.one.user({ from: r.virtualKey.principalUserId, to: r.user.id }),
      scopes: r.many.virtualKeyScope(),
      ledgerEntries: r.many.gatewayBudgetLedger(),
      routingPolicy: r.one.routingPolicy({
        from: r.virtualKey.routingPolicyId,
        to: r.routingPolicy.id,
      }),
    },
    virtualKeyScope: {
      virtualKey: r.one.virtualKey({
        from: r.virtualKeyScope.virtualKeyId,
        to: r.virtualKey.id,
        optional: false,
      }),
    },
    routingPolicy: {
      organization: r.one.organization({
        from: r.routingPolicy.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      createdBy: r.one.user({
        from: r.routingPolicy.createdById,
        to: r.user.id,
        alias: 'RoutingPolicyCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.routingPolicy.updatedById,
        to: r.user.id,
        alias: 'RoutingPolicyUpdatedBy',
      }),
      virtualKeys: r.many.virtualKey(),
      scopes: r.many.routingPolicyScope(),
    },
    routingPolicyScope: {
      routingPolicy: r.one.routingPolicy({
        from: r.routingPolicyScope.routingPolicyId,
        to: r.routingPolicy.id,
        optional: false,
      }),
    },
    gatewayGuardrail: {
      project: r.one.project({
        from: r.gatewayGuardrail.projectId,
        to: r.project.id,
        optional: false,
      }),
      evaluator: r.one.evaluator({
        from: r.gatewayGuardrail.evaluatorId,
        to: r.evaluator.id,
        optional: false,
      }),
      createdBy: r.one.user({
        from: r.gatewayGuardrail.createdById,
        to: r.user.id,
        alias: 'GatewayGuardrailCreatedBy',
      }),
      updatedBy: r.one.user({
        from: r.gatewayGuardrail.updatedById,
        to: r.user.id,
        alias: 'GatewayGuardrailUpdatedBy',
      }),
    },
    ingestionSource: {
      organization: r.one.organization({
        from: r.ingestionSource.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      team: r.one.team({ from: r.ingestionSource.teamId, to: r.team.id }),
      createdBy: r.one.user({ from: r.ingestionSource.createdById, to: r.user.id }),
    },
    anomalyRule: {
      organization: r.one.organization({
        from: r.anomalyRule.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      createdBy: r.one.user({ from: r.anomalyRule.createdById, to: r.user.id }),
      alerts: r.many.anomalyAlert(),
    },
    anomalyAlert: {
      organization: r.one.organization({
        from: r.anomalyAlert.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      rule: r.one.anomalyRule({
        from: r.anomalyAlert.ruleId,
        to: r.anomalyRule.id,
        optional: false,
      }),
    },
    aiToolEntry: {
      organization: r.one.organization({
        from: r.aiToolEntry.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      teams: r.many.aiToolEntryTeam(),
      departments: r.many.aiToolEntryDepartment(),
    },
    aiToolEntryTeam: {
      entry: r.one.aiToolEntry({
        from: r.aiToolEntryTeam.entryId,
        to: r.aiToolEntry.id,
        optional: false,
      }),
      team: r.one.team({ from: r.aiToolEntryTeam.teamId, to: r.team.id, optional: false }),
    },
    aiToolEntryDepartment: {
      entry: r.one.aiToolEntry({
        from: r.aiToolEntryDepartment.entryId,
        to: r.aiToolEntry.id,
        optional: false,
      }),
    },
    ingestionTemplate: {
      organization: r.one.organization({
        from: r.ingestionTemplate.organizationId,
        to: r.organization.id,
      }),
    },
    gatewayBudget: {
      createdBy: r.one.user({ from: r.gatewayBudget.createdById, to: r.user.id, optional: false }),
      ledgerEntries: r.many.gatewayBudgetLedger(),
      bucketBoundaries: r.many.gatewayBudgetBucketBoundary(),
    },
    gatewayBudgetBucketBoundary: {
      budget: r.one.gatewayBudget({
        from: r.gatewayBudgetBucketBoundary.budgetId,
        to: r.gatewayBudget.id,
        optional: false,
      }),
    },
    gatewayBudgetLedger: {
      budget: r.one.gatewayBudget({
        from: r.gatewayBudgetLedger.budgetId,
        to: r.gatewayBudget.id,
        optional: false,
      }),
      virtualKey: r.one.virtualKey({
        from: r.gatewayBudgetLedger.virtualKeyId,
        to: r.virtualKey.id,
        optional: false,
      }),
    },
    gatewayChangeEvent: {
      organization: r.one.organization({
        from: r.gatewayChangeEvent.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      project: r.one.project({ from: r.gatewayChangeEvent.projectId, to: r.project.id }),
    },
    processManagerOutbox: { attemptLog: r.many.processManagerOutboxAttempt() },
    processManagerOutboxAttempt: {
      outbox: r.one.processManagerOutbox({
        from: r.processManagerOutboxAttempt.outboxId,
        to: r.processManagerOutbox.id,
        optional: false,
      }),
    },
  }),
)
