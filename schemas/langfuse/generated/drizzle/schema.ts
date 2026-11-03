import {
  type PgTableExtraConfigValue,
  bigint,
  boolean,
  char,
  doublePrecision,
  foreignKey,
  index,
  integer,
  json,
  jsonb,
  numeric,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  unique,
  uniqueIndex,
  uuid,
  varchar,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
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

export const apiKeyScopeEnum = pgEnum('ApiKeyScope', ['ORGANIZATION', 'PROJECT'])

export const systemRoleEnum = pgEnum('SystemRole', [
  'OWNER',
  'ADMIN',
  'MEMBER',
  'VIEWER',
  'NONE',
  'LEGACY_PROJECT_API_KEY',
  'LEGACY_ORGANIZATION_API_KEY',
  'SCORES_INGEST',
  'INGEST',
  'AI_GATEWAY',
])

export const gatewayIngestionModeEnum = pgEnum('GatewayIngestionMode', ['none', 'usage', 'full'])

export const gatewayProviderEnum = pgEnum('GatewayProvider', ['openai', 'anthropic'])

export const gatewayConnectionStatusEnum = pgEnum('GatewayConnectionStatus', [
  'enabled',
  'disabled',
  'error',
])

export const inAppAgentConversationVisibilityScopeEnum = pgEnum(
  'InAppAgentConversationVisibilityScope',
  ['PERSONAL', 'PROJECT'],
)

export const roleEnum = pgEnum('Role', ['OWNER', 'ADMIN', 'MEMBER', 'VIEWER', 'NONE', 'AI_GATEWAY'])

export const scoreConfigDataTypeEnum = pgEnum('ScoreConfigDataType', [
  'CATEGORICAL',
  'NUMERIC',
  'BOOLEAN',
  'TEXT',
])

export const annotationQueueStatusEnum = pgEnum('AnnotationQueueStatus', ['PENDING', 'COMPLETED'])

export const annotationQueueObjectTypeEnum = pgEnum('AnnotationQueueObjectType', [
  'TRACE',
  'OBSERVATION',
  'SESSION',
])

export const datasetStatusEnum = pgEnum('DatasetStatus', ['ACTIVE', 'ARCHIVED'])

export const commentObjectTypeEnum = pgEnum('CommentObjectType', [
  'TRACE',
  'OBSERVATION',
  'SESSION',
  'PROMPT',
])

export const notificationChannelEnum = pgEnum('NotificationChannel', ['EMAIL'])

export const notificationTypeEnum = pgEnum('NotificationType', ['COMMENT_MENTION'])

export const auditLogRecordTypeEnum = pgEnum('AuditLogRecordType', ['USER', 'API_KEY'])

export const evalTemplateTypeEnum = pgEnum('EvalTemplateType', [
  'LLM_AS_JUDGE',
  'CODE',
  'DECISION_MODEL',
  'FACET',
])

export const evalTemplateSourceCodeLanguageEnum = pgEnum('EvalTemplateSourceCodeLanguage', [
  'PYTHON',
  'TYPESCRIPT',
])

export const evaluatorSourceCodeLanguageEnum = pgEnum('EvaluatorSourceCodeLanguage', [
  'PYTHON',
  'TYPESCRIPT',
])

export const jobTypeEnum = pgEnum('JobType', ['EVAL'])

export const jobConfigStateEnum = pgEnum('JobConfigState', ['ACTIVE', 'INACTIVE'])

export const evaluatorBlockReasonEnum = pgEnum('EvaluatorBlockReason', [
  'LLM_CONNECTION_AUTH_INVALID',
  'LLM_CONNECTION_BILLING_EXHAUSTED',
  'LLM_CONNECTION_ENDPOINT_UNREACHABLE',
  'LLM_CONNECTION_MISSING',
  'DEFAULT_EVAL_MODEL_MISSING',
  'EVAL_MODEL_CONFIG_INVALID',
  'EVAL_MODEL_UNAVAILABLE',
  'PROVIDER_ACCOUNT_NOT_READY',
])

export const jobExecutionStatusEnum = pgEnum('JobExecutionStatus', [
  'COMPLETED',
  'ERROR',
  'PENDING',
  'CANCELLED',
  'DELAYED',
])

export const blobStorageIntegrationFileTypeEnum = pgEnum('BlobStorageIntegrationFileType', [
  'JSON',
  'CSV',
  'JSONL',
  'PARQUET',
])

export const blobStorageIntegrationTypeEnum = pgEnum('BlobStorageIntegrationType', [
  'S3',
  'S3_COMPATIBLE',
  'AZURE_BLOB_STORAGE',
  'GOOGLE_CLOUD_STORAGE',
])

export const externalMediaStorageIntegrationTypeEnum = pgEnum(
  'ExternalMediaStorageIntegrationType',
  ['S3', 'S3_COMPATIBLE'],
)

export const blobStorageExportModeEnum = pgEnum('BlobStorageExportMode', [
  'FULL_HISTORY',
  'FROM_TODAY',
  'FROM_CUSTOM_DATE',
])

export const analyticsIntegrationExportSourceEnum = pgEnum('AnalyticsIntegrationExportSource', [
  'TRACES_OBSERVATIONS',
  'TRACES_OBSERVATIONS_EVENTS',
  'EVENTS',
])

export const mediaAssociationOriginEnum = pgEnum('MediaAssociationOrigin', [
  'UNKNOWN',
  'CLIENT_UPLOAD',
  'INGESTION_MEDIA_EXTRACTION',
  'INGESTION_FIELD_OVERFLOW',
])

export const dashboardWidgetViewsEnum = pgEnum('DashboardWidgetViews', [
  'TRACES',
  'OBSERVATIONS',
  'SCORES_NUMERIC',
  'SCORES_CATEGORICAL',
  'SCORES_BOOLEAN',
])

export const dashboardWidgetChartTypeEnum = pgEnum('DashboardWidgetChartType', [
  'LINE_TIME_SERIES',
  'AREA_TIME_SERIES',
  'BAR_TIME_SERIES',
  'HORIZONTAL_BAR',
  'VERTICAL_BAR',
  'PIE',
  'NUMBER',
  'HISTOGRAM',
  'PIVOT_TABLE',
])

export const actionTypeEnum = pgEnum('ActionType', [
  'WEBHOOK',
  'SLACK',
  'GITHUB_DISPATCH',
  'PAGERDUTY',
])

export const actionExecutionStatusEnum = pgEnum('ActionExecutionStatus', [
  'COMPLETED',
  'ERROR',
  'PENDING',
  'CANCELLED',
])

export const monitorThresholdOperatorEnum = pgEnum('MonitorThresholdOperator', [
  'GT',
  'GTE',
  'LT',
  'LTE',
  'EQ',
  'NEQ',
])

export const monitorViewEnum = pgEnum('MonitorView', [
  'OBSERVATIONS',
  'SCORES_NUMERIC',
  'SCORES_CATEGORICAL',
  'SCORES_BOOLEAN',
])

export const monitorSeverityEnum = pgEnum('MonitorSeverity', [
  'PAUSED',
  'UNKNOWN',
  'NO_DATA',
  'OK',
  'WARNING',
  'ALERT',
])

export const monitorStatusEnum = pgEnum('MonitorStatus', ['PAUSED', 'ACTIVE', 'ERROR_BAD_QUERY'])

export const surveyNameEnum = pgEnum('SurveyName', ['org_onboarding', 'user_onboarding'])

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    expires_in: integer('expires_in'),
    ext_expires_in: integer('ext_expires_in'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
    refresh_token_expires_in: integer('refresh_token_expires_in'),
    created_at: integer('created_at'),
  },
  (table) => [
    index('Account_user_id_idx').on(table.userId),
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'Account_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
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
      .$defaultFn(() => cuid()),
    sessionToken: text('session_token').notNull(),
    userId: text('user_id').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('Session_session_token_key').on(table.sessionToken),
    foreignKey({
      name: 'Session_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const users = pgTable(
  'users',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name'),
    email: text('email'),
    emailVerified: timestamp('email_verified', { precision: 3 }),
    password: text('password'),
    sessionsExpiredAt: timestamp('sessions_expired_at', { precision: 3 }),
    image: text('image'),
    admin: boolean('admin').notNull().default(false),
    v4BetaEnabled: boolean('v4_beta_enabled').notNull().default(false),
    supportAccess: jsonb('support_access'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    featureFlags: text('feature_flags').array().default([]),
  },
  (table) => [uniqueIndex('users_email_key').on(table.email)],
)

export const verificationTokens = pgTable(
  'verification_tokens',
  {
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('verification_tokens_token_key').on(table.token),
    uniqueIndex('verification_tokens_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const organizations = pgTable('organizations', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  name: text('name').notNull(),
  createdAt: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  cloudConfig: jsonb('cloud_config'),
  metadata: jsonb('metadata'),
  cloudBillingCycleAnchor: timestamp('cloud_billing_cycle_anchor', { precision: 3 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  cloudBillingCycleUpdatedAt: timestamp('cloud_billing_cycle_updated_at', { precision: 3 }),
  cloudCurrentCycleUsage: integer('cloud_current_cycle_usage'),
  cloudFreeTierUsageThresholdState: text('cloud_free_tier_usage_threshold_state'),
  aiFeaturesEnabled: boolean('ai_features_enabled').notNull().default(false),
  aiTelemetryEnabled: boolean('ai_telemetry_enabled').notNull().default(true),
  featureFlagOrgDefaults: text('feature_flag_org_defaults').array().default([]),
  sfdcOrgId: text('sfdc_org_id'),
})

export const projects = pgTable(
  'projects',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    orgId: text('org_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
    name: text('name').notNull(),
    retentionDays: integer('retention_days'),
    hasTraces: boolean('has_traces').notNull().default(false),
    metadata: jsonb('metadata'),
    homeDashboardId: text('home_dashboard_id'),
  },
  (table): PgTableExtraConfigValue[] => [
    index('projects_org_id_idx').on(table.orgId),
    foreignKey({
      name: 'projects_home_dashboard_id_fkey',
      columns: [table.homeDashboardId],
      foreignColumns: [dashboards.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'projects_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiKeys = pgTable(
  'api_keys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    note: text('note'),
    publicKey: text('public_key').notNull(),
    hashedSecretKey: text('hashed_secret_key').notNull(),
    fastHashedSecretKey: text('fast_hashed_secret_key'),
    displaySecretKey: text('display_secret_key').notNull(),
    lastUsedAt: timestamp('last_used_at', { precision: 3 }),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    isInAppAgentKey: boolean('is_in_app_agent_key').notNull().default(false),
    projectId: text('project_id'),
    orgId: text('organization_id'),
    scope: apiKeyScopeEnum('scope').notNull().default('PROJECT'),
    createdByUserId: text('created_by_user_id'),
    userId: text('user_id'),
    createdByApiKeyId: text('created_by_api_key_id'),
  },
  (table) => [
    unique('api_keys_id_key').on(table.id),
    uniqueIndex('api_keys_public_key_key').on(table.publicKey),
    uniqueIndex('api_keys_hashed_secret_key_key').on(table.hashedSecretKey),
    uniqueIndex('api_keys_fast_hashed_secret_key_key').on(table.fastHashedSecretKey),
    index('api_keys_organization_id_idx').on(table.orgId),
    index('api_keys_project_id_idx').on(table.projectId),
    index('api_keys_created_by_api_key_id_idx').on(table.createdByApiKeyId),
    index('api_keys_user_id_organization_id_idx').on(table.userId, table.orgId),
    index('api_keys_public_key_idx').on(table.publicKey),
    index('api_keys_hashed_secret_key_idx').on(table.hashedSecretKey),
    index('api_keys_fast_hashed_secret_key_idx').on(table.fastHashedSecretKey),
    foreignKey({
      name: 'api_keys_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'api_keys_organization_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'api_keys_created_by_user_id_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'api_keys_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'api_keys_created_by_api_key_id_fkey',
      columns: [table.createdByApiKeyId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const roleAssignments = pgTable(
  'role_assignments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    ownerId: text('owner_id').notNull(),
    projectId: text('project_id'),
    orgId: text('org_id').notNull(),
    principalId: text('principal_id').notNull(),
    userId: text('user_id'),
    apiKeyId: text('api_key_id'),
    roleId: text('role_id').notNull(),
    systemRole: systemRoleEnum('system_role').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('role_assignments_org_id_idx').on(table.orgId),
    index('role_assignments_project_id_idx').on(table.projectId),
    index('role_assignments_principal_id_idx').on(table.principalId),
    index('role_assignments_user_id_idx').on(table.userId),
    index('role_assignments_api_key_id_idx').on(table.apiKeyId),
    uniqueIndex('role_assignments_owner_id_principal_id_role_id_key').on(
      table.ownerId,
      table.principalId,
      table.roleId,
    ),
    foreignKey({
      name: 'role_assignments_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'role_assignments_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'role_assignments_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'role_assignments_api_key_id_fkey',
      columns: [table.apiKeyId],
      foreignColumns: [apiKeys.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayConfigs = pgTable(
  'gateway_configs',
  {
    organizationId: text('organization_id').primaryKey(),
    defaultIngestionProjectId: text('default_ingestion_project_id'),
    ingestionMode: gatewayIngestionModeEnum('ingestion_mode').notNull().default('usage'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'gateway_configs_organization_id_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'gateway_configs_default_ingestion_project_id_fkey',
      columns: [table.defaultIngestionProjectId],
      foreignColumns: [projects.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const gatewayAiConnections = pgTable(
  'gateway_ai_connections',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    organizationId: text('organization_id').notNull(),
    name: text('name').notNull(),
    provider: gatewayProviderEnum('provider').notNull(),
    encryptedCredential: text('encrypted_credentials').notNull(),
    displaySecret: text('display_secret_key').notNull(),
    createdById: text('created_by_id'),
    status: gatewayConnectionStatusEnum('status').notNull().default('enabled'),
    allowedModels: jsonb('allowed_models'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('gateway_ai_conn_org_created_at_id_idx').on(
      table.organizationId,
      table.createdAt,
      table.id,
    ),
    index('gateway_ai_conn_org_status_created_at_id_idx').on(
      table.organizationId,
      table.status,
      table.createdAt,
      table.id,
    ),
    uniqueIndex('gateway_ai_connections_organization_id_name_key').on(
      table.organizationId,
      table.name,
    ),
    foreignKey({
      name: 'gateway_ai_connections_organization_id_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gatewayApiKeyAssociations = pgTable(
  'gateway_api_key_associations',
  { apiKeyId: text('api_key_id').primaryKey(), metadata: jsonb('metadata').notNull().default({}) },
  (table) => [
    foreignKey({
      name: 'gateway_api_key_associations_api_key_id_fkey',
      columns: [table.apiKeyId],
      foreignColumns: [apiKeys.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const inAppAgentConversations = pgTable(
  'in_app_agent_conversations',
  {
    id: text('id').notNull(),
    projectId: text('project_id').notNull(),
    createdByUserId: text('created_by_user_id'),
    title: text('title'),
    renamedByUserAt: timestamp('renamed_by_user_at', { precision: 3 }),
    visibilityScope: inAppAgentConversationVisibilityScopeEnum('visibility_scope')
      .notNull()
      .default('PERSONAL'),
    providerSessionId: text('provider_session_id'),
    alwaysAllowedTools: text('always_allowed_tools').array().default([]),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'in_app_agent_conversations_pkey', columns: [table.id, table.projectId] }),
    index('in_app_agent_conversations_project_user_list_idx').on(
      table.projectId,
      table.createdByUserId,
      table.deletedAt,
      table.updatedAt,
      table.id,
    ),
    index('in_app_agent_conversations_project_updated_at_idx').on(table.projectId, table.updatedAt),
    foreignKey({
      name: 'in_app_agent_conversations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_conversations_created_by_user_id_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const inAppAgentEvents = pgTable(
  'in_app_agent_events',
  {
    projectId: text('project_id').notNull(),
    conversationId: text('conversation_id').notNull(),
    runId: text('run_id').notNull(),
    sequenceNumber: integer('sequence_number').notNull(),
    type: text('type').notNull(),
    event: jsonb('event').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'in_app_agent_events_pkey',
      columns: [table.projectId, table.conversationId, table.sequenceNumber],
    }),
    index('in_app_agent_events_project_run_idx').on(table.projectId, table.runId),
    foreignKey({
      name: 'in_app_agent_events_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_events_conversation_id_project_id_fkey',
      columns: [table.conversationId, table.projectId],
      foreignColumns: [inAppAgentConversations.id, inAppAgentConversations.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_events_run_id_project_id_fkey',
      columns: [table.runId, table.projectId],
      foreignColumns: [inAppAgentRuns.id, inAppAgentRuns.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const inAppAgentRuns = pgTable(
  'in_app_agent_runs',
  {
    id: text('id').notNull(),
    projectId: text('project_id').notNull(),
    conversationId: text('conversation_id').notNull(),
    triggeredByUserId: text('triggered_by_user_id'),
    model: text('model'),
    mcpApiKeyId: text('mcp_api_key_id'),
    errorCode: text('error_code'),
    errorMessage: text('error_message'),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    status: text('status'),
    request: jsonb('request'),
    claimedAt: timestamp('claimed_at', { precision: 3 }),
    heartbeatAt: timestamp('heartbeat_at', { precision: 3 }),
    cancelRequestedAt: timestamp('cancel_requested_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'in_app_agent_runs_pkey', columns: [table.id, table.projectId] }),
    index('in_app_agent_runs_project_conversation_created_idx').on(
      table.projectId,
      table.conversationId,
      table.createdAt,
    ),
    foreignKey({
      name: 'in_app_agent_runs_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_runs_conversation_id_project_id_fkey',
      columns: [table.conversationId, table.projectId],
      foreignColumns: [inAppAgentConversations.id, inAppAgentConversations.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_runs_triggered_by_user_id_fkey',
      columns: [table.triggeredByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const inAppAgentPendingToolApprovals = pgTable(
  'in_app_agent_pending_tool_approvals',
  {
    projectId: text('project_id').notNull(),
    conversationId: text('conversation_id').notNull(),
    toolCallId: text('tool_call_id').notNull(),
    approvalFingerprint: text('approval_fingerprint').notNull(),
    expiresAt: timestamp('expires_at', { precision: 3 }).notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'in_app_agent_pending_tool_approvals_pkey',
      columns: [table.projectId, table.conversationId, table.toolCallId],
    }),
    index('in_app_agent_pending_tool_approvals_expires_at_idx').on(table.expiresAt),
    foreignKey({
      name: 'in_app_agent_pending_tool_approvals_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'in_app_agent_pending_tool_approvals_conversation_id_projec_fkey',
      columns: [table.conversationId, table.projectId],
      foreignColumns: [inAppAgentConversations.id, inAppAgentConversations.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const backgroundMigrations = pgTable(
  'background_migrations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    script: text('script').notNull(),
    args: jsonb('args').notNull(),
    state: jsonb('state').notNull().default({}),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    failedAt: timestamp('failed_at', { precision: 3 }),
    failedReason: text('failed_reason'),
    workerId: text('worker_id'),
    lockedAt: timestamp('locked_at', { precision: 3 }),
  },
  (table) => [uniqueIndex('background_migrations_name_key').on(table.name)],
)

export const llmApiKeys = pgTable(
  'llm_api_keys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    provider: text('provider').notNull(),
    adapter: text('adapter').notNull(),
    displaySecretKey: text('display_secret_key').notNull(),
    secretKey: text('secret_key').notNull(),
    baseURL: text('base_url'),
    customModels: text('custom_models').array().default([]),
    withDefaultModels: boolean('with_default_models').notNull().default(true),
    extraHeaders: text('extra_headers'),
    extraHeaderKeys: text('extra_header_keys').array().default([]),
    config: jsonb('config'),
    projectId: text('project_id'),
    organizationId: text('organization_id'),
  },
  (table) => [
    unique('llm_api_keys_id_key').on(table.id),
    uniqueIndex('llm_api_keys_project_id_provider_key').on(table.projectId, table.provider),
    uniqueIndex('llm_api_keys_organization_id_provider_key').on(
      table.organizationId,
      table.provider,
    ),
    foreignKey({
      name: 'llm_api_keys_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'llm_api_keys_organization_id_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organizationMemberships = pgTable(
  'organization_memberships',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    orgId: text('org_id').notNull(),
    userId: text('user_id').notNull(),
    role: roleEnum('role').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('organization_memberships_user_id_idx').on(table.userId),
    uniqueIndex('organization_memberships_org_id_user_id_key').on(table.orgId, table.userId),
    foreignKey({
      name: 'organization_memberships_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'organization_memberships_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const projectMemberships = pgTable(
  'project_memberships',
  {
    orgMembershipId: text('org_membership_id').notNull(),
    projectId: text('project_id').notNull(),
    userId: text('user_id').notNull(),
    role: roleEnum('role').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'project_memberships_pkey', columns: [table.projectId, table.userId] }),
    index('project_memberships_user_id_idx').on(table.userId),
    index('project_memberships_project_id_idx').on(table.projectId),
    index('project_memberships_org_membership_id_idx').on(table.orgMembershipId),
    foreignKey({
      name: 'project_memberships_org_membership_id_fkey',
      columns: [table.orgMembershipId],
      foreignColumns: [organizationMemberships.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'project_memberships_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'project_memberships_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const membershipInvitations = pgTable(
  'membership_invitations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    email: text('email').notNull(),
    orgId: text('org_id').notNull(),
    orgRole: roleEnum('org_role').notNull(),
    projectId: text('project_id'),
    projectRole: roleEnum('project_role'),
    invitedByUserId: text('invited_by_user_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('membership_invitations_id_key').on(table.id),
    index('membership_invitations_project_id_idx').on(table.projectId),
    index('membership_invitations_org_id_idx').on(table.orgId),
    index('membership_invitations_email_idx').on(table.email),
    uniqueIndex('membership_invitations_email_org_id_key').on(table.email, table.orgId),
    foreignKey({
      name: 'membership_invitations_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'membership_invitations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'membership_invitations_invited_by_user_id_fkey',
      columns: [table.invitedByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const traceSessions = pgTable(
  'trace_sessions',
  {
    id: text('id')
      .notNull()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    bookmarked: boolean('bookmarked').notNull().default(false),
    public: boolean('public').notNull().default(false),
    environment: text('environment').notNull().default('default'),
  },
  (table) => [
    primaryKey({ name: 'trace_sessions_pkey', columns: [table.id, table.projectId] }),
    index('trace_sessions_project_id_created_at_idx').on(table.projectId, table.createdAt),
    foreignKey({
      name: 'trace_sessions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scoreConfigs = pgTable(
  'score_configs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    dataType: scoreConfigDataTypeEnum('data_type').notNull(),
    isArchived: boolean('is_archived').notNull().default(false),
    minValue: doublePrecision('min_value'),
    maxValue: doublePrecision('max_value'),
    categories: jsonb('categories'),
    description: text('description'),
  },
  (table) => [
    index('score_configs_data_type_idx').on(table.dataType),
    index('score_configs_is_archived_idx').on(table.isArchived),
    index('score_configs_project_id_idx').on(table.projectId),
    index('score_configs_categories_idx').on(table.categories),
    index('score_configs_created_at_idx').on(table.createdAt),
    index('score_configs_updated_at_idx').on(table.updatedAt),
    uniqueIndex('score_configs_id_project_id_key').on(table.id, table.projectId),
    foreignKey({
      name: 'score_configs_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const annotationQueues = pgTable(
  'annotation_queues',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    scoreConfigIds: text('score_config_ids').array().default([]),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('annotation_queues_id_project_id_idx').on(table.id, table.projectId),
    index('annotation_queues_project_id_created_at_idx').on(table.projectId, table.createdAt),
    uniqueIndex('annotation_queues_project_id_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'annotation_queues_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const annotationQueueItems = pgTable(
  'annotation_queue_items',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    queueId: text('queue_id').notNull(),
    objectId: text('object_id').notNull(),
    objectType: annotationQueueObjectTypeEnum('object_type').notNull(),
    status: annotationQueueStatusEnum('status').notNull().default('PENDING'),
    lockedAt: timestamp('locked_at', { precision: 3 }),
    lockedByUserId: text('locked_by_user_id'),
    annotatorUserId: text('annotator_user_id'),
    completedAt: timestamp('completed_at', { precision: 3 }),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('annotation_queue_items_id_project_id_idx').on(table.id, table.projectId),
    index('annotation_queue_items_project_id_queue_id_status_idx').on(
      table.projectId,
      table.queueId,
      table.status,
    ),
    index('annotation_queue_items_object_id_object_type_project_id_que_idx').on(
      table.objectId,
      table.objectType,
      table.projectId,
      table.queueId,
    ),
    index('annotation_queue_items_annotator_user_id_idx').on(table.annotatorUserId),
    index('annotation_queue_items_created_at_idx').on(table.createdAt),
    foreignKey({
      name: 'annotation_queue_items_queue_id_fkey',
      columns: [table.queueId],
      foreignColumns: [annotationQueues.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'annotation_queue_items_locked_by_user_id_fkey',
      columns: [table.lockedByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'annotation_queue_items_annotator_user_id_fkey',
      columns: [table.annotatorUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'annotation_queue_items_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const annotationQueueAssignments = pgTable(
  'annotation_queue_assignments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    userId: text('user_id').notNull(),
    queueId: text('queue_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('annotation_queue_assignments_project_id_queue_id_user_id_key').on(
      table.projectId,
      table.queueId,
      table.userId,
    ),
    foreignKey({
      name: 'annotation_queue_assignments_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'annotation_queue_assignments_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'annotation_queue_assignments_queue_id_fkey',
      columns: [table.queueId],
      foreignColumns: [annotationQueues.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cronJobs = pgTable('cron_jobs', {
  name: text('name').primaryKey(),
  lastRun: timestamp('last_run', { precision: 3 }),
  jobStartedAt: timestamp('job_started_at', { precision: 3 }),
  state: text('state'),
})

export const datasets = pgTable(
  'datasets',
  {
    id: text('id')
      .notNull()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    metadata: jsonb('metadata'),
    remoteExperimentUrl: text('remote_experiment_url'),
    remoteExperimentPayload: jsonb('remote_experiment_payload'),
    remoteExperimentEnabled: boolean('remote_experiment_enabled').notNull().default(true),
    remoteExperimentSecretKey: text('remote_experiment_secret_key'),
    remoteExperimentDisplaySecretKey: text('remote_experiment_display_secret_key'),
    remoteExperimentRequestHeaders: jsonb('remote_experiment_request_headers'),
    remoteExperimentDisplayHeaders: jsonb('remote_experiment_display_headers'),
    inputSchema: json('input_schema'),
    expectedOutputSchema: json('expected_output_schema'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'datasets_pkey', columns: [table.id, table.projectId] }),
    index('datasets_created_at_idx').on(table.createdAt),
    index('datasets_updated_at_idx').on(table.updatedAt),
    uniqueIndex('datasets_project_id_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'datasets_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const datasetItems = pgTable(
  'dataset_items',
  {
    id: text('id')
      .notNull()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    status: datasetStatusEnum('status').default('ACTIVE'),
    input: jsonb('input'),
    expectedOutput: jsonb('expected_output'),
    metadata: jsonb('metadata'),
    sourceTraceId: text('source_trace_id'),
    sourceObservationId: text('source_observation_id'),
    datasetId: text('dataset_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    validFrom: timestamp('valid_from', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    validTo: timestamp('valid_to', { precision: 3 }),
    isDeleted: boolean('is_deleted').notNull().default(false),
  },
  (table) => [
    primaryKey({
      name: 'dataset_items_pkey',
      columns: [table.id, table.projectId, table.validFrom],
    }),
    index('dataset_items_project_id_valid_to_idx').on(table.projectId, table.validTo),
    index('dataset_items_project_id_id_valid_from_idx').on(
      table.projectId,
      table.id,
      table.validFrom,
    ),
    index('dataset_items_source_trace_id_idx').on(table.sourceTraceId),
    index('dataset_items_source_observation_id_idx').on(table.sourceObservationId),
    index('dataset_items_dataset_id_idx').on(table.datasetId),
    index('dataset_items_created_at_idx').on(table.createdAt),
    index('dataset_items_updated_at_idx').on(table.updatedAt),
    foreignKey({
      name: 'dataset_items_dataset_id_project_id_fkey',
      columns: [table.datasetId, table.projectId],
      foreignColumns: [datasets.id, datasets.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const datasetRuns = pgTable(
  'dataset_runs',
  {
    id: text('id')
      .notNull()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    metadata: jsonb('metadata'),
    datasetId: text('dataset_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'dataset_runs_pkey', columns: [table.id, table.projectId] }),
    index('dataset_runs_dataset_id_idx').on(table.datasetId),
    index('dataset_runs_created_at_idx').on(table.createdAt),
    index('dataset_runs_updated_at_idx').on(table.updatedAt),
    uniqueIndex('dataset_runs_dataset_id_project_id_name_key').on(
      table.datasetId,
      table.projectId,
      table.name,
    ),
    foreignKey({
      name: 'dataset_runs_dataset_id_project_id_fkey',
      columns: [table.datasetId, table.projectId],
      foreignColumns: [datasets.id, datasets.projectId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const comments = pgTable(
  'comments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    objectType: commentObjectTypeEnum('object_type').notNull(),
    objectId: text('object_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    content: text('content').notNull(),
    authorUserId: text('author_user_id'),
    dataField: text('data_field'),
    path: text('path').array().default([]),
    rangeStart: integer('range_start').array().default([]),
    rangeEnd: integer('range_end').array().default([]),
  },
  (table) => [
    index('comments_project_id_object_type_object_id_idx').on(
      table.projectId,
      table.objectType,
      table.objectId,
    ),
    foreignKey({
      name: 'comments_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const commentReactions = pgTable(
  'comment_reactions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    commentId: text('comment_id').notNull(),
    userId: text('user_id').notNull(),
    emoji: text('emoji').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('comment_reactions_comment_id_user_id_emoji_key').on(
      table.commentId,
      table.userId,
      table.emoji,
    ),
    foreignKey({
      name: 'comment_reactions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'comment_reactions_comment_id_fkey',
      columns: [table.commentId],
      foreignColumns: [comments.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'comment_reactions_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const notificationPreferences = pgTable(
  'notification_preferences',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    projectId: text('project_id').notNull(),
    channel: notificationChannelEnum('channel').notNull(),
    type: notificationTypeEnum('type').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('notification_preferences_user_id_project_id_channel_type_key').on(
      table.userId,
      table.projectId,
      table.channel,
      table.type,
    ),
    foreignKey({
      name: 'notification_preferences_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'notification_preferences_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const prompts = pgTable(
  'prompts',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    createdBy: text('created_by').notNull(),
    prompt: jsonb('prompt').notNull(),
    name: text('name').notNull(),
    version: integer('version').notNull(),
    type: text('type').notNull().default('text'),
    isActive: boolean('is_active'),
    config: json('config').notNull().default({}),
    tags: text('tags').array().default([]),
    labels: text('labels').array().default([]),
    commitMessage: text('commit_message'),
  },
  (table) => [
    index('prompts_project_id_id_idx').on(table.projectId, table.id),
    index('prompts_created_at_idx').on(table.createdAt),
    index('prompts_updated_at_idx').on(table.updatedAt),
    index('prompts_tags_idx').on(table.tags),
    uniqueIndex('prompts_project_id_name_version_key').on(
      table.projectId,
      table.name,
      table.version,
    ),
    foreignKey({
      name: 'prompts_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const promptDependencies = pgTable(
  'prompt_dependencies',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    parentId: text('parent_id').notNull(),
    childName: text('child_name').notNull(),
    childLabel: text('child_label'),
    childVersion: integer('child_version'),
  },
  (table) => [
    index('prompt_dependencies_project_id_parent_id').on(table.projectId, table.parentId),
    index('prompt_dependencies_project_id_child_name').on(table.projectId, table.childName),
    foreignKey({
      name: 'prompt_dependencies_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'prompt_dependencies_parent_id_fkey',
      columns: [table.parentId],
      foreignColumns: [prompts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const promptProtectedLabels = pgTable(
  'prompt_protected_labels',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    label: text('label').notNull(),
  },
  (table) => [
    uniqueIndex('prompt_protected_labels_project_id_label_key').on(table.projectId, table.label),
    foreignKey({
      name: 'prompt_protected_labels_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const skills = pgTable(
  'skills',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    createdBy: text('created_by').notNull(),
    name: text('name').notNull(),
    description: text('description').notNull(),
    frontmatter: jsonb('frontmatter').notNull().default({}),
    version: integer('version').notNull(),
    tags: text('tags').array().default([]),
    labels: text('labels').array().default([]),
    commitMessage: text('commit_message'),
  },
  (table) => [
    index('skills_created_at_idx').on(table.createdAt),
    index('skills_updated_at_idx').on(table.updatedAt),
    index('skills_tags_idx').on(table.tags),
    index('skills_labels_idx').on(table.labels),
    uniqueIndex('skills_project_id_name_version_key').on(
      table.projectId,
      table.name,
      table.version,
    ),
    foreignKey({
      name: 'skills_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const skillFiles = pgTable(
  'skill_files',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    projectId: text('project_id').notNull(),
    skillId: text('skill_id').notNull(),
    blobId: text('blob_id').notNull(),
    path: text('path').notNull(),
  },
  (table) => [
    index('skill_files_project_id_blob_id_idx').on(table.projectId, table.blobId),
    uniqueIndex('skill_files_project_id_skill_id_path_key').on(
      table.projectId,
      table.skillId,
      table.path,
    ),
    foreignKey({
      name: 'skill_files_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'skill_files_skill_id_fkey',
      columns: [table.skillId],
      foreignColumns: [skills.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'skill_files_blob_id_fkey',
      columns: [table.blobId],
      foreignColumns: [skillBlobs.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const skillBlobs = pgTable(
  'skill_blobs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    projectId: text('project_id').notNull(),
    sha256Hash: char('sha_256_hash', { length: 44 }).notNull(),
    contentType: text('content_type').notNull(),
    contentLength: integer('content_length').notNull(),
    content: text('content').notNull(),
  },
  (table) => [
    uniqueIndex('skill_blobs_project_id_sha_256_hash_key').on(table.projectId, table.sha256Hash),
    foreignKey({
      name: 'skill_blobs_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const models = pgTable(
  'models',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id'),
    modelName: text('model_name').notNull(),
    matchPattern: text('match_pattern').notNull(),
    startDate: timestamp('start_date', { precision: 3 }),
    inputPrice: numeric('input_price', { precision: 65, scale: 30 }),
    outputPrice: numeric('output_price', { precision: 65, scale: 30 }),
    totalPrice: numeric('total_price', { precision: 65, scale: 30 }),
    unit: text('unit'),
    tokenizerId: text('tokenizer_id'),
    tokenizerConfig: jsonb('tokenizer_config'),
  },
  (table) => [
    index('models_model_name_idx').on(table.modelName),
    uniqueIndex('models_project_id_model_name_start_date_unit_key').on(
      table.projectId,
      table.modelName,
      table.startDate,
      table.unit,
    ),
    foreignKey({
      name: 'models_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const prices = pgTable(
  'prices',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    modelId: text('model_id').notNull(),
    projectId: text('project_id'),
    pricingTierId: text('pricing_tier_id').notNull(),
    usageType: text('usage_type').notNull(),
    price: numeric('price', { precision: 65, scale: 30 }).notNull(),
  },
  (table) => [
    index('prices_pricing_tier_id_idx').on(table.pricingTierId),
    uniqueIndex('prices_model_id_usage_type_pricing_tier_id_key').on(
      table.modelId,
      table.usageType,
      table.pricingTierId,
    ),
    foreignKey({
      name: 'prices_model_id_fkey',
      columns: [table.modelId],
      foreignColumns: [models.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'prices_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'prices_pricing_tier_id_fkey',
      columns: [table.pricingTierId],
      foreignColumns: [pricingTiers.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const pricingTiers = pgTable(
  'pricing_tiers',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    modelId: text('model_id').notNull(),
    name: text('name').notNull(),
    isDefault: boolean('is_default').notNull().default(false),
    priority: integer('priority').notNull(),
    conditions: jsonb('conditions').notNull(),
  },
  (table) => [
    uniqueIndex('pricing_tiers_model_id_priority_key').on(table.modelId, table.priority),
    uniqueIndex('pricing_tiers_model_id_name_key').on(table.modelId, table.name),
    foreignKey({
      name: 'pricing_tiers_model_id_fkey',
      columns: [table.modelId],
      foreignColumns: [models.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const auditLogs = pgTable(
  'audit_logs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    type: auditLogRecordTypeEnum('type').notNull().default('USER'),
    apiKeyId: text('api_key_id'),
    userId: text('user_id'),
    orgId: text('org_id').notNull(),
    userOrgRole: text('user_org_role'),
    projectId: text('project_id'),
    userProjectRole: text('user_project_role'),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    action: text('action').notNull(),
    before: text('before'),
    after: text('after'),
  },
  (table) => [
    index('audit_logs_project_id_idx').on(table.projectId),
    index('audit_logs_api_key_id_idx').on(table.apiKeyId),
    index('audit_logs_user_id_idx').on(table.userId),
    index('audit_logs_org_id_idx').on(table.orgId),
    index('audit_logs_created_at_idx').on(table.createdAt),
    index('audit_logs_updated_at_idx').on(table.updatedAt),
  ],
)

export const auditLogStaging = pgTable('audit_log_staging', {
  id: uuid('id').primaryKey(),
  createdAt: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  payload: jsonb('payload').notNull(),
  failedAt: timestamp('failed_at', { precision: 3 }),
  lastError: text('last_error'),
})

export const evalTemplates = pgTable(
  'eval_templates',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id'),
    name: text('name').notNull(),
    version: integer('version').notNull(),
    prompt: text('prompt'),
    type: evalTemplateTypeEnum('type').notNull().default('LLM_AS_JUDGE'),
    partner: text('partner'),
    model: text('model'),
    provider: text('provider'),
    modelParams: jsonb('model_params'),
    vars: text('vars').array().default([]),
    outputDefinition: jsonb('output_schema'),
    sourceCode: varchar('source_code', { length: 262144 }),
    sourceCodeLanguage: evalTemplateSourceCodeLanguageEnum('source_code_language'),
  },
  (table) => [
    index('eval_templates_project_id_id_idx').on(table.projectId, table.id),
    uniqueIndex('eval_templates_project_id_name_version_key').on(
      table.projectId,
      table.name,
      table.version,
    ),
    foreignKey({
      name: 'eval_templates_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const evaluators = pgTable(
  'evaluators',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    type: evalTemplateTypeEnum('type').notNull(),
    isBuiltIn: boolean('is_built_in').notNull().default(false),
    description: text('description'),
    createdByUserId: text('created_by_user_id'),
    blockedAt: timestamp('blocked_at', { precision: 3 }),
    blockReason: evaluatorBlockReasonEnum('block_reason'),
    blockMessage: text('block_message'),
  },
  (table) => [
    index('evaluators_project_id_created_at_idx').on(table.projectId, table.createdAt),
    foreignKey({
      name: 'evaluators_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'evaluators_created_by_user_id_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const evaluatorVersions = pgTable(
  'evaluator_versions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    evaluatorId: text('evaluator_id').notNull(),
    version: integer('version').notNull(),
    createdByUserId: text('created_by_user_id'),
    prompt: text('prompt'),
    promptMessages: jsonb('prompt_messages'),
    partner: text('partner'),
    model: text('model'),
    provider: text('provider'),
    modelParams: jsonb('model_params'),
    vars: text('vars').array().default([]),
    variableMapping: jsonb('variable_mapping'),
    outputDefinition: jsonb('output_definition'),
    sourceCode: varchar('source_code', { length: 262144 }),
    sourceCodeLanguage: evaluatorSourceCodeLanguageEnum('source_code_language'),
    questions: jsonb('questions'),
  },
  (table) => [
    unique('evaluator_versions_evaluator_id_version_key').on(table.evaluatorId, table.version),
    foreignKey({
      name: 'evaluator_versions_evaluator_id_fkey',
      columns: [table.evaluatorId],
      foreignColumns: [evaluators.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'evaluator_versions_created_by_user_id_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const jobConfigurations = pgTable(
  'job_configurations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    jobType: jobTypeEnum('job_type').notNull(),
    status: jobConfigStateEnum('status').notNull().default('ACTIVE'),
    blockedAt: timestamp('blocked_at', { precision: 3 }),
    blockReason: evaluatorBlockReasonEnum('block_reason'),
    blockMessage: text('block_message'),
    evalTemplateId: text('eval_template_id'),
    scoreName: text('score_name').notNull(),
    filter: jsonb('filter').notNull(),
    targetObject: text('target_object').notNull(),
    variableMapping: jsonb('variable_mapping').notNull(),
    sampling: numeric('sampling', { precision: 65, scale: 30 }).notNull(),
    delay: integer('delay').notNull(),
    timeScope: text('time_scope').array().default(['NEW']),
  },
  (table) => [
    index('job_configurations_project_id_id_idx').on(table.projectId, table.id),
    foreignKey({
      name: 'job_configurations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'job_configurations_eval_template_id_fkey',
      columns: [table.evalTemplateId],
      foreignColumns: [evalTemplates.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const evaluationRules = pgTable(
  'evaluation_rules',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    createdByUserId: text('created_by_user_id'),
    name: text('name').notNull(),
    status: jobConfigStateEnum('status').notNull().default('ACTIVE'),
    targetObject: text('target_object').notNull(),
    filter: jsonb('filter').notNull(),
    sampling: numeric('sampling', { precision: 65, scale: 30 }).notNull(),
    delay: integer('delay').notNull(),
    idleTime: integer('idle_time'),
    timeScope: text('time_scope').array().default(['NEW']),
  },
  (table) => [
    index('evaluation_rules_project_id_updated_at_idx').on(table.projectId, table.updatedAt),
    foreignKey({
      name: 'evaluation_rules_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'evaluation_rules_created_by_user_id_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const tracingReadinessSettings = pgTable(
  'tracing_readiness_settings',
  {
    projectId: text('project_id').primaryKey(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    traceIdleMs: integer('trace_idle_ms'),
  },
  (table) => [
    foreignKey({
      name: 'tracing_readiness_settings_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'tracing_readiness_settings_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'tracing_readiness_settings_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const evaluationRuleEvaluatorAssignments = pgTable(
  'evaluation_rule_evaluator_assignments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    evaluationRuleId: text('evaluation_rule_id').notNull(),
    evaluatorId: text('evaluator_id').notNull(),
    variableMapping: jsonb('variable_mapping'),
  },
  (table) => [
    index('evaluation_rule_assignments_project_id_idx').on(table.projectId),
    index('evaluation_rule_assignments_evaluator_id_idx').on(table.evaluatorId),
    uniqueIndex('evaluation_rule_assignments_rule_evaluator_key').on(
      table.evaluationRuleId,
      table.evaluatorId,
    ),
    foreignKey({
      name: 'evaluation_rule_evaluator_assignments_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'evaluation_rule_evaluator_assignments_evaluation_rule_id_fkey',
      columns: [table.evaluationRuleId],
      foreignColumns: [evaluationRules.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'evaluation_rule_evaluator_assignments_evaluator_id_fkey',
      columns: [table.evaluatorId],
      foreignColumns: [evaluators.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const jobExecutions = pgTable(
  'job_executions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    jobConfigurationId: text('job_configuration_id').notNull(),
    jobTemplateId: text('job_template_id'),
    status: jobExecutionStatusEnum('status').notNull(),
    startTime: timestamp('start_time', { precision: 3 }),
    endTime: timestamp('end_time', { precision: 3 }),
    error: text('error'),
    jobInputTraceId: text('job_input_trace_id'),
    jobInputTraceTimestamp: timestamp('job_input_trace_timestamp', { precision: 3 }),
    jobInputObservationId: text('job_input_observation_id'),
    jobInputDatasetItemId: text('job_input_dataset_item_id'),
    jobInputDatasetItemValidFrom: timestamp('job_input_dataset_item_valid_from', { precision: 3 }),
    jobOutputScoreId: text('job_output_score_id'),
    executionTraceId: text('execution_trace_id'),
  },
  (table) => [
    index('job_executions_project_id_job_configuration_id_job_input_tr_idx').on(
      table.projectId,
      table.jobConfigurationId,
      table.jobInputTraceId,
    ),
    index('job_executions_project_id_job_output_score_id_idx').on(
      table.projectId,
      table.jobOutputScoreId,
    ),
    index('job_executions_job_configuration_id_idx').on(table.jobConfigurationId),
    foreignKey({
      name: 'job_executions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const defaultLlmModels = pgTable(
  'default_llm_models',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    llmApiKeyId: text('llm_api_key_id').notNull(),
    provider: text('provider').notNull(),
    adapter: text('adapter').notNull(),
    model: text('model').notNull(),
    modelParams: jsonb('model_params'),
  },
  (table) => [
    uniqueIndex('default_llm_models_project_id_key').on(table.projectId),
    foreignKey({
      name: 'default_llm_models_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'default_llm_models_llm_api_key_id_fkey',
      columns: [table.llmApiKeyId],
      foreignColumns: [llmApiKeys.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const topicConfigs = pgTable(
  'topic_configs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    projectId: text('project_id').notNull(),
    enabled: boolean('enabled').notNull().default(false),
    blockedAt: timestamp('blocked_at', { precision: 3 }),
    blockReason: evaluatorBlockReasonEnum('block_reason'),
    blockMessage: text('block_message'),
    summaryLlmApiKeyId: text('summary_llm_api_key_id'),
    summaryModel: text('summary_model'),
    embeddingLlmApiKeyId: text('embedding_llm_api_key_id'),
    embeddingModel: text('embedding_model'),
    embeddingDimensions: integer('embedding_dimensions').notNull().default(1024),
    clusteringLlmApiKeyId: text('clustering_llm_api_key_id'),
    clusteringModel: text('clustering_model'),
  },
  (table) => [
    uniqueIndex('topic_configs_project_id_key').on(table.projectId),
    index('topic_configs_summary_llm_api_key_id_idx').on(table.summaryLlmApiKeyId),
    index('topic_configs_embedding_llm_api_key_id_idx').on(table.embeddingLlmApiKeyId),
    index('topic_configs_clustering_llm_api_key_id_idx').on(table.clusteringLlmApiKeyId),
    foreignKey({
      name: 'topic_configs_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_configs_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_configs_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_configs_summary_llm_api_key_id_fkey',
      columns: [table.summaryLlmApiKeyId],
      foreignColumns: [llmApiKeys.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_configs_embedding_llm_api_key_id_fkey',
      columns: [table.embeddingLlmApiKeyId],
      foreignColumns: [llmApiKeys.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_configs_clustering_llm_api_key_id_fkey',
      columns: [table.clusteringLlmApiKeyId],
      foreignColumns: [llmApiKeys.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const ssoConfigs = pgTable('sso_configs', {
  domain: text('domain')
    .primaryKey()
    .$defaultFn(() => cuid()),
  createdAt: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  authProvider: text('auth_provider').notNull(),
  authConfig: jsonb('auth_config'),
})

export const verifiedDomains = pgTable(
  'verified_domains',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organization_id').notNull(),
    domain: text('domain').notNull(),
    verificationToken: text('verification_token')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    verifiedAt: timestamp('verified_at', { precision: 3 }),
    createdByUserId: text('created_by_user_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('verified_domains_verification_token_key').on(table.verificationToken),
    index('verified_domains_organization_id_idx').on(table.organizationId),
    uniqueIndex('verified_domains_organization_id_domain_key').on(
      table.organizationId,
      table.domain,
    ),
    foreignKey({
      name: 'verified_domains_organization_id_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const posthogIntegrations = pgTable(
  'posthog_integrations',
  {
    projectId: text('project_id').primaryKey(),
    encryptedPosthogApiKey: text('encrypted_posthog_api_key').notNull(),
    posthogHostName: text('posthog_host_name').notNull(),
    lastSyncAt: timestamp('last_sync_at', { precision: 3 }),
    enabled: boolean('enabled').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    exportSource: analyticsIntegrationExportSourceEnum('export_source')
      .notNull()
      .default('TRACES_OBSERVATIONS'),
    backfill: boolean('backfill').notNull().default(true),
    lastError: text('last_error'),
    lastErrorAt: timestamp('last_error_at', { precision: 3 }),
  },
  (table) => [
    foreignKey({
      name: 'posthog_integrations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mixpanelIntegrations = pgTable(
  'mixpanel_integrations',
  {
    projectId: text('project_id').primaryKey(),
    encryptedMixpanelProjectToken: text('encrypted_mixpanel_project_token').notNull(),
    mixpanelRegion: text('mixpanel_region').notNull(),
    lastSyncAt: timestamp('last_sync_at', { precision: 3 }),
    enabled: boolean('enabled').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    exportSource: analyticsIntegrationExportSourceEnum('export_source')
      .notNull()
      .default('TRACES_OBSERVATIONS'),
    backfill: boolean('backfill').notNull().default(true),
  },
  (table) => [
    foreignKey({
      name: 'mixpanel_integrations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const blobStorageIntegrations = pgTable(
  'blob_storage_integrations',
  {
    projectId: text('project_id').primaryKey(),
    type: blobStorageIntegrationTypeEnum('type').notNull(),
    bucketName: text('bucket_name').notNull(),
    prefix: text('prefix').notNull(),
    accessKeyId: text('access_key_id'),
    secretAccessKey: text('secret_access_key'),
    region: text('region').notNull(),
    endpoint: text('endpoint'),
    forcePathStyle: boolean('force_path_style').notNull(),
    nextSyncAt: timestamp('next_sync_at', { precision: 3 }),
    lastSyncAt: timestamp('last_sync_at', { precision: 3 }),
    enabled: boolean('enabled').notNull(),
    exportFrequency: text('export_frequency').notNull(),
    fileType: blobStorageIntegrationFileTypeEnum('file_type').notNull().default('CSV'),
    exportMode: blobStorageExportModeEnum('export_mode').notNull().default('FULL_HISTORY'),
    exportStartDate: timestamp('export_start_date', { precision: 3 }),
    exportSource: analyticsIntegrationExportSourceEnum('export_source')
      .notNull()
      .default('TRACES_OBSERVATIONS'),
    exportFieldGroups: text('export_field_groups')
      .array()
      .default([
        'core',
        'basic',
        'time',
        'io',
        'metadata',
        'model',
        'usage',
        'prompt',
        'metrics',
        'tools',
        'trace_context',
      ]),
    compressed: boolean('compressed').notNull().default(true),
    exportTuning: jsonb('export_tuning'),
    runStartedAt: timestamp('run_started_at', { precision: 3 }),
    backfill: boolean('backfill').notNull().default(true),
    lastError: text('last_error'),
    lastErrorAt: timestamp('last_error_at', { precision: 3 }),
    lastFailureNotificationSentAt: timestamp('last_failure_notification_sent_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'blob_storage_integrations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const externalMediaStorageIntegrations = pgTable(
  'external_media_storage_integrations',
  {
    projectId: text('project_id').primaryKey(),
    type: externalMediaStorageIntegrationTypeEnum('type').notNull(),
    bucketName: text('bucket_name').notNull(),
    prefix: text('prefix'),
    accessKeyId: text('access_key_id'),
    secretAccessKey: text('secret_access_key'),
    region: text('region').notNull(),
    endpoint: text('endpoint'),
    forcePathStyle: boolean('force_path_style').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'external_media_storage_integrations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webCalloutEndpoints = pgTable(
  'web_callout_endpoints',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull().default('Default'),
    url: text('url').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    toastMessage: text('toast_message').notNull().default('Callout sent'),
    requestHeaders: text('request_headers'),
    requestHeaderKeys: text('request_header_keys').array().default([]),
  },
  (table) => [
    index('web_callout_endpoints_project_id_idx').on(table.projectId),
    index('web_callout_endpoints_project_id_enabled_idx').on(table.projectId, table.enabled),
    foreignKey({
      name: 'web_callout_endpoints_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const batchExports = pgTable(
  'batch_exports',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    userId: text('user_id').notNull(),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    name: text('name').notNull(),
    status: text('status').notNull(),
    query: jsonb('query').notNull(),
    format: text('format').notNull(),
    url: text('url'),
    log: text('log'),
  },
  (table) => [
    index('batch_exports_project_id_user_id_idx').on(table.projectId, table.userId),
    index('batch_exports_status_idx').on(table.status),
    foreignKey({
      name: 'batch_exports_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const batchActions = pgTable(
  'batch_actions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    userId: text('user_id').notNull(),
    actionType: text('action_type').notNull(),
    tableName: text('table_name').notNull(),
    status: text('status').notNull(),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    query: jsonb('query').notNull(),
    config: jsonb('config'),
    totalCount: integer('total_count'),
    processedCount: integer('processed_count'),
    failedCount: integer('failed_count'),
    log: text('log'),
  },
  (table) => [
    index('batch_actions_project_id_user_id_idx').on(table.projectId, table.userId),
    index('batch_actions_status_idx').on(table.status),
    index('batch_actions_project_id_action_type_idx').on(table.projectId, table.actionType),
    foreignKey({
      name: 'batch_actions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const media = pgTable(
  'media',
  {
    id: text('id').notNull(),
    sha256Hash: char('sha_256_hash', { length: 44 }).notNull(),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    uploadedAt: timestamp('uploaded_at', { precision: 3 }),
    uploadHttpStatus: integer('upload_http_status'),
    uploadHttpError: text('upload_http_error'),
    bucketPath: text('bucket_path').notNull(),
    bucketName: text('bucket_name').notNull(),
    contentType: text('content_type').notNull(),
    contentLength: bigint('content_length', { mode: 'bigint' }).notNull(),
  },
  (table) => [
    index('media_project_id_created_at_idx').on(table.projectId, table.createdAt),
    uniqueIndex('media_project_id_id_key').on(table.projectId, table.id),
    uniqueIndex('media_project_id_sha_256_hash_key').on(table.projectId, table.sha256Hash),
    foreignKey({
      name: 'media_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const traceMedia = pgTable(
  'trace_media',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    mediaId: text('media_id').notNull(),
    traceId: text('trace_id').notNull(),
    field: text('field').notNull(),
    origin: mediaAssociationOriginEnum('origin').notNull().default('UNKNOWN'),
  },
  (table) => [
    index('trace_media_project_id_media_id_idx').on(table.projectId, table.mediaId),
    uniqueIndex('trace_media_project_id_trace_id_media_id_field_key').on(
      table.projectId,
      table.traceId,
      table.mediaId,
      table.field,
    ),
  ],
)

export const observationMedia = pgTable(
  'observation_media',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    mediaId: text('media_id').notNull(),
    traceId: text('trace_id').notNull(),
    observationId: text('observation_id').notNull(),
    field: text('field').notNull(),
    origin: mediaAssociationOriginEnum('origin').notNull().default('UNKNOWN'),
  },
  (table) => [
    index('observation_media_project_id_media_id_idx').on(table.projectId, table.mediaId),
    uniqueIndex('observation_media_project_id_trace_id_observation_id_media__key').on(
      table.projectId,
      table.traceId,
      table.observationId,
      table.mediaId,
      table.field,
    ),
  ],
)

export const datasetItemMedia = pgTable(
  'dataset_item_media',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    mediaId: text('media_id').notNull(),
    datasetId: text('dataset_id').notNull(),
    datasetItemId: text('dataset_item_id').notNull(),
    datasetItemValidFrom: timestamp('dataset_item_valid_from', { precision: 3 }),
    field: text('field').notNull(),
    jsonPath: text('json_path'),
    referenceString: text('reference_string'),
  },
  (table) => [
    index('dataset_item_media_project_id_media_id_idx').on(table.projectId, table.mediaId),
    index('dataset_item_media_project_id_dataset_id_idx').on(table.projectId, table.datasetId),
    uniqueIndex('dataset_item_media_item_version_field_path_key').on(
      table.projectId,
      table.datasetItemId,
      table.datasetItemValidFrom,
      table.field,
      table.jsonPath,
    ),
  ],
)

export const billingMeterBackups = pgTable(
  'billing_meter_backups',
  {
    stripeCustomerId: text('stripe_customer_id').notNull(),
    meterId: text('meter_id').notNull(),
    startTime: timestamp('start_time', { precision: 3 }).notNull(),
    endTime: timestamp('end_time', { precision: 3 }).notNull(),
    aggregatedValue: integer('aggregated_value').notNull(),
    eventName: text('event_name').notNull(),
    orgId: text('org_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('billing_meter_backups_stripe_customer_id_meter_id_start_tim_idx').on(
      table.stripeCustomerId,
      table.meterId,
      table.startTime,
      table.endTime,
    ),
    uniqueIndex('billing_meter_backups_stripe_customer_id_meter_id_start_tim_key').on(
      table.stripeCustomerId,
      table.meterId,
      table.startTime,
      table.endTime,
    ),
  ],
)

export const llmSchemas = pgTable(
  'llm_schemas',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    description: text('description').notNull(),
    schema: json('schema').notNull(),
  },
  (table) => [
    uniqueIndex('llm_schemas_project_id_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'llm_schemas_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const llmTools = pgTable(
  'llm_tools',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    description: text('description').notNull(),
    parameters: json('parameters').notNull(),
  },
  (table) => [
    uniqueIndex('llm_tools_project_id_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'llm_tools_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dashboards = pgTable(
  'dashboards',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    projectId: text('project_id'),
    name: text('name').notNull(),
    description: text('description').notNull(),
    definition: jsonb('definition').notNull(),
    filters: jsonb('filters').notNull().default([]),
  },
  (table): PgTableExtraConfigValue[] => [
    foreignKey({
      name: 'dashboards_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'dashboards_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'dashboards_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dashboardWidgets = pgTable(
  'dashboard_widgets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    projectId: text('project_id'),
    name: text('name').notNull(),
    description: text('description').notNull(),
    view: dashboardWidgetViewsEnum('view').notNull(),
    dimensions: jsonb('dimensions').notNull(),
    metrics: jsonb('metrics').notNull(),
    filters: jsonb('filters').notNull(),
    chartType: dashboardWidgetChartTypeEnum('chart_type').notNull(),
    chartConfig: jsonb('chart_config').notNull(),
    minVersion: integer('min_version').notNull().default(1),
  },
  (table) => [
    foreignKey({
      name: 'dashboard_widgets_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'dashboard_widgets_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'dashboard_widgets_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tableViewPresets = pgTable(
  'table_view_presets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    name: text('name').notNull(),
    tableName: text('table_name').notNull(),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    filters: jsonb('filters').notNull(),
    columnOrder: jsonb('column_order').notNull(),
    columnVisibility: jsonb('column_visibility').notNull(),
    searchQuery: text('search_query'),
    orderBy: jsonb('order_by'),
  },
  (table) => [
    uniqueIndex('table_view_presets_project_id_table_name_name_key').on(
      table.projectId,
      table.tableName,
      table.name,
    ),
    foreignKey({
      name: 'table_view_presets_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'table_view_presets_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'table_view_presets_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const defaultViews = pgTable(
  'default_views',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    userId: text('user_id'),
    viewName: text('view_name').notNull(),
    viewId: text('view_id').notNull(),
  },
  (table) => [
    index('default_views_project_id_view_name_idx').on(table.projectId, table.viewName),
    foreignKey({
      name: 'default_views_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'default_views_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const actions = pgTable(
  'actions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    type: actionTypeEnum('type').notNull(),
    config: jsonb('config').notNull(),
  },
  (table) => [
    index('actions_project_id_idx').on(table.projectId),
    foreignKey({
      name: 'actions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const triggers = pgTable(
  'triggers',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    projectId: text('project_id').notNull(),
    eventSource: text('eventSource').notNull(),
    eventActions: text('eventActions').array(),
    filter: jsonb('filter'),
    status: jobConfigStateEnum('status').notNull().default('ACTIVE'),
  },
  (table) => [
    index('triggers_project_id_idx').on(table.projectId),
    foreignKey({
      name: 'triggers_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const automations = pgTable(
  'automations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    triggerId: text('trigger_id').notNull(),
    actionId: text('action_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    projectId: text('project_id').notNull(),
  },
  (table) => [
    index('automations_project_id_action_id_trigger_id_idx').on(
      table.projectId,
      table.actionId,
      table.triggerId,
    ),
    index('automations_project_id_name_idx').on(table.projectId, table.name),
    foreignKey({
      name: 'automations_trigger_id_fkey',
      columns: [table.triggerId],
      foreignColumns: [triggers.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'automations_action_id_fkey',
      columns: [table.actionId],
      foreignColumns: [actions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'automations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const automationExecutions = pgTable(
  'automation_executions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    sourceId: text('source_id').notNull(),
    automationId: text('automation_id').notNull(),
    triggerId: text('trigger_id').notNull(),
    actionId: text('action_id').notNull(),
    projectId: text('project_id').notNull(),
    status: actionExecutionStatusEnum('status').notNull().default('PENDING'),
    input: jsonb('input').notNull(),
    output: jsonb('output'),
    startedAt: timestamp('started_at', { precision: 3 }),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    error: text('error'),
  },
  (table) => [
    index('automation_executions_trigger_id_idx').on(table.triggerId),
    index('automation_executions_action_id_idx').on(table.actionId),
    index('automation_executions_project_id_idx').on(table.projectId),
    foreignKey({
      name: 'automation_executions_automation_id_fkey',
      columns: [table.automationId],
      foreignColumns: [automations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'automation_executions_trigger_id_fkey',
      columns: [table.triggerId],
      foreignColumns: [triggers.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'automation_executions_action_id_fkey',
      columns: [table.actionId],
      foreignColumns: [actions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'automation_executions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const monitors = pgTable(
  'monitors',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    createdBy: text('created_by'),
    updatedBy: text('updated_by'),
    projectId: text('project_id').notNull(),
    view: monitorViewEnum('view').notNull(),
    filters: jsonb('filters').notNull(),
    metric: jsonb('metric').notNull(),
    windowMs: bigint('window_ms', { mode: 'bigint' }).notNull(),
    cadenceMs: bigint('cadence_ms', { mode: 'bigint' }).notNull(),
    thresholdOperator: monitorThresholdOperatorEnum('threshold_operator').notNull(),
    alertThreshold: numeric('alert_threshold', { precision: 65, scale: 30 }).notNull(),
    warningThreshold: numeric('warning_threshold', { precision: 65, scale: 30 }),
    severity: monitorSeverityEnum('severity').notNull().default('UNKNOWN'),
    severityChangedAt: timestamp('severity_changed_at', { precision: 3 }),
    noData: jsonb('no_data').notNull(),
    renotify: jsonb('renotify').notNull(),
    status: monitorStatusEnum('status').notNull().default('ACTIVE'),
    schedulerBatchId: bigint('scheduler_batch_id', { mode: 'bigint' }).notNull(),
    nextRunAt: timestamp('next_run_at', { precision: 3 }),
    lastPublishedAt: timestamp('last_published_at', { precision: 3 }),
    lastClaimedAt: timestamp('last_claimed_at', { precision: 3 }),
    lastCompletedAt: timestamp('last_completed_at', { precision: 3 }),
    name: text('name').notNull(),
    tags: text('tags').array().default([]),
    triggerIds: text('trigger_ids').array().default([]),
    alertedAt: timestamp('alerted_at', { precision: 3 }),
  },
  (table) => [
    index('monitors_project_id_idx').on(table.projectId),
    index('monitors_scheduler_batch_id_idx').on(table.schedulerBatchId),
    index('monitors_scheduler_tick_idx').on(table.nextRunAt, table.schedulerBatchId),
    foreignKey({
      name: 'monitors_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'monitors_updated_by_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'monitors_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const slackIntegrations = pgTable(
  'slack_integrations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    teamId: text('team_id').notNull(),
    teamName: text('team_name').notNull(),
    botToken: text('bot_token').notNull(),
    botUserId: text('bot_user_id').notNull(),
    installerUserId: text('installer_user_id'),
    installerSlackUserId: text('installer_slack_user_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('slack_integrations_project_id_key').on(table.projectId),
    index('slack_integrations_team_id_idx').on(table.teamId),
    foreignKey({
      name: 'slack_integrations_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const pendingDeletions = pgTable(
  'pending_deletions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    object: text('object').notNull(),
    objectId: text('object_id').notNull(),
    isDeleted: boolean('is_deleted').notNull().default(false),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('pending_deletions_project_id_object_is_deleted_object_id_id_idx').on(
      table.projectId,
      table.object,
      table.isDeleted,
      table.objectId,
      table.id,
    ),
    index('pending_deletions_object_id_object_idx').on(table.objectId, table.object),
    foreignKey({
      name: 'pending_deletions_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveys = pgTable(
  'surveys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    surveyName: surveyNameEnum('survey_name').notNull(),
    response: jsonb('response').notNull(),
    userId: text('user_id'),
    userEmail: text('user_email'),
    orgId: text('org_id'),
  },
  (table) => [
    foreignKey({
      name: 'surveys_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'surveys_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cloudSpendAlerts = pgTable(
  'cloud_spend_alerts',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    orgId: text('org_id').notNull(),
    title: text('title').notNull(),
    threshold: numeric('threshold', { precision: 65, scale: 30 }).notNull(),
    triggeredAt: timestamp('triggered_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('cloud_spend_alerts_org_id_idx').on(table.orgId),
    foreignKey({
      name: 'cloud_spend_alerts_org_id_fkey',
      columns: [table.orgId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const topicClusteringRuns = pgTable(
  'topic_clustering_runs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectId: text('project_id').notNull(),
    facetId: text('facet_id').notNull(),
    facetVersion: integer('facet_version').notNull(),
    status: text('status').notNull().default('pending'),
    config: jsonb('config').notNull().default({}),
    error: text('error'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    startedAt: timestamp('started_at', { withTimezone: true, precision: 3 }),
    finishedAt: timestamp('finished_at', { withTimezone: true, precision: 3 }),
    topicVersionIds: text('topic_version_ids').array().default([]),
  },
  (table) => [
    index('topic_clustering_runs_serving_map_idx').on(
      table.projectId,
      table.facetId,
      table.status,
      table.facetVersion,
      table.createdAt,
      table.id,
    ),
    foreignKey({
      name: 'topic_clustering_runs_project_id_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'topic_clustering_runs_facet_id_facet_version_fkey',
      columns: [table.facetId, table.facetVersion],
      foreignColumns: [evaluatorVersions.evaluatorId, evaluatorVersions.version],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    account,
    session,
    users,
    verificationTokens,
    organizations,
    projects,
    apiKeys,
    roleAssignments,
    gatewayConfigs,
    gatewayAiConnections,
    gatewayApiKeyAssociations,
    inAppAgentConversations,
    inAppAgentEvents,
    inAppAgentRuns,
    inAppAgentPendingToolApprovals,
    backgroundMigrations,
    llmApiKeys,
    organizationMemberships,
    projectMemberships,
    membershipInvitations,
    traceSessions,
    scoreConfigs,
    annotationQueues,
    annotationQueueItems,
    annotationQueueAssignments,
    cronJobs,
    datasets,
    datasetItems,
    datasetRuns,
    comments,
    commentReactions,
    notificationPreferences,
    prompts,
    promptDependencies,
    promptProtectedLabels,
    skills,
    skillFiles,
    skillBlobs,
    models,
    prices,
    pricingTiers,
    auditLogs,
    auditLogStaging,
    evalTemplates,
    evaluators,
    evaluatorVersions,
    jobConfigurations,
    evaluationRules,
    tracingReadinessSettings,
    evaluationRuleEvaluatorAssignments,
    jobExecutions,
    defaultLlmModels,
    topicConfigs,
    ssoConfigs,
    verifiedDomains,
    posthogIntegrations,
    mixpanelIntegrations,
    blobStorageIntegrations,
    externalMediaStorageIntegrations,
    webCalloutEndpoints,
    batchExports,
    batchActions,
    media,
    traceMedia,
    observationMedia,
    datasetItemMedia,
    billingMeterBackups,
    llmSchemas,
    llmTools,
    dashboards,
    dashboardWidgets,
    tableViewPresets,
    defaultViews,
    actions,
    triggers,
    automations,
    automationExecutions,
    monitors,
    slackIntegrations,
    pendingDeletions,
    surveys,
    cloudSpendAlerts,
    topicClusteringRuns,
  },
  (r) => ({
    account: { user: r.one.users({ from: r.account.userId, to: r.users.id, optional: false }) },
    session: { user: r.one.users({ from: r.session.userId, to: r.users.id, optional: false }) },
    users: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      organizationMemberships: r.many.organizationMemberships(),
      projectMemberships: r.many.projectMemberships(),
      invitations: r.many.membershipInvitations(),
      annotatedLockedItem: r.many.annotationQueueItems({ alias: 'LockedByUser' }),
      annotatedCompletedItem: r.many.annotationQueueItems({ alias: 'AnnotatorUser' }),
      dashboardWidgetsCreated: r.many.dashboardWidgets({ alias: 'CreatedByUser' }),
      dashboardWidgetsUpdated: r.many.dashboardWidgets({ alias: 'UpdatedByUser' }),
      dashboardCreated: r.many.dashboards({ alias: 'CreatedByUser' }),
      dashboardUpdated: r.many.dashboards({ alias: 'UpdatedByUser' }),
      tableViewPresetCreated: r.many.tableViewPresets({ alias: 'CreatedByUser' }),
      tableViewPresetUpdated: r.many.tableViewPresets({ alias: 'UpdatedByUser' }),
      monitorsCreated: r.many.monitors({ alias: 'CreatedByUser' }),
      monitorsUpdated: r.many.monitors({ alias: 'UpdatedByUser' }),
      topicConfigsCreated: r.many.topicConfigs({ alias: 'CreatedByUser' }),
      topicConfigsUpdated: r.many.topicConfigs({ alias: 'UpdatedByUser' }),
      tracingReadinessCreated: r.many.tracingReadinessSettings({ alias: 'CreatedByUser' }),
      tracingReadinessUpdated: r.many.tracingReadinessSettings({ alias: 'UpdatedByUser' }),
      annotationQueueAssignment: r.many.annotationQueueAssignments(),
      surveys: r.many.surveys(),
      commentReactions: r.many.commentReactions(),
      notificationPreferences: r.many.notificationPreferences(),
      defaultViews: r.many.defaultViews(),
      inAppAgentConversations: r.many.inAppAgentConversations(),
      inAppAgentRuns: r.many.inAppAgentRuns(),
      apiKeysCreated: r.many.apiKeys({ alias: 'ApiKeyCreatedByUser' }),
      apiKeys: r.many.apiKeys({ alias: 'ApiKeyUser' }),
      roleAssignments: r.many.roleAssignments(),
      evaluatorsCreated: r.many.evaluators(),
      evaluatorVersionsCreated: r.many.evaluatorVersions(),
      evaluationRulesCreated: r.many.evaluationRules(),
    },
    organizations: {
      organizationMemberships: r.many.organizationMemberships(),
      projects: r.many.projects(),
      MembershipInvitation: r.many.membershipInvitations(),
      ApiKey: r.many.apiKeys(),
      surveys: r.many.surveys(),
      cloudSpendAlerts: r.many.cloudSpendAlerts(),
      verifiedDomains: r.many.verifiedDomains(),
      gatewayConfig: r.one.gatewayConfigs({
        from: r.organizations.id,
        to: r.gatewayConfigs.organizationId,
      }),
      gatewayAiConnections: r.many.gatewayAiConnections(),
      organizationLlmApiKeys: r.many.llmApiKeys(),
      roleAssignments: r.many.roleAssignments(),
    },
    projects: {
      homeDashboard: r.one.dashboards({
        from: r.projects.homeDashboardId,
        to: r.dashboards.id,
        alias: 'ProjectHomeDashboard',
      }),
      projectMembers: r.many.projectMemberships(),
      organization: r.one.organizations({
        from: r.projects.orgId,
        to: r.organizations.id,
        optional: false,
      }),
      apiKeys: r.many.apiKeys(),
      roleAssignments: r.many.roleAssignments(),
      dataset: r.many.datasets(),
      invitations: r.many.membershipInvitations(),
      sessions: r.many.traceSessions(),
      Prompt: r.many.prompts(),
      Skill: r.many.skills(),
      SkillFile: r.many.skillFiles(),
      SkillBlob: r.many.skillBlobs(),
      Model: r.many.models(),
      EvalTemplate: r.many.evalTemplates(),
      JobConfiguration: r.many.jobConfigurations(),
      JobExecution: r.many.jobExecutions(),
      Evaluator: r.many.evaluators(),
      EvaluationRule: r.many.evaluationRules(),
      EvaluationRuleEvaluatorAssignment: r.many.evaluationRuleEvaluatorAssignments(),
      tracingReadinessSettings: r.one.tracingReadinessSettings({
        from: r.projects.id,
        to: r.tracingReadinessSettings.projectId,
      }),
      LlmApiKeys: r.many.llmApiKeys(),
      PosthogIntegration: r.many.posthogIntegrations(),
      MixpanelIntegration: r.many.mixpanelIntegrations(),
      BlobStorageIntegration: r.many.blobStorageIntegrations(),
      ExternalMediaStorageIntegration: r.one.externalMediaStorageIntegrations({
        from: r.projects.id,
        to: r.externalMediaStorageIntegrations.projectId,
      }),
      webCalloutEndpoints: r.many.webCalloutEndpoints(),
      scoreConfig: r.many.scoreConfigs(),
      BatchExport: r.many.batchExports(),
      BatchAction: r.many.batchActions(),
      comment: r.many.comments(),
      commentReactions: r.many.commentReactions(),
      annotationQueue: r.many.annotationQueues(),
      annotationQueueItem: r.many.annotationQueueItems(),
      Media: r.many.media(),
      PromptDependency: r.many.promptDependencies(),
      LlmSchema: r.many.llmSchemas(),
      LlmTool: r.many.llmTools(),
      PromptProtectedLabels: r.many.promptProtectedLabels(),
      Dashboard: r.many.dashboards({ alias: 'DashboardToProject' }),
      DashboardWidget: r.many.dashboardWidgets(),
      TableViewPreset: r.many.tableViewPresets(),
      actions: r.many.actions(),
      triggers: r.many.triggers(),
      automationExecutions: r.many.automationExecutions(),
      Automation: r.many.automations(),
      monitors: r.many.monitors(),
      DefaultLlmModel: r.many.defaultLlmModels(),
      TopicConfig: r.one.topicConfigs({ from: r.projects.id, to: r.topicConfigs.projectId }),
      Price: r.many.prices(),
      SlackIntegration: r.one.slackIntegrations({
        from: r.projects.id,
        to: r.slackIntegrations.projectId,
      }),
      PendingDeletion: r.many.pendingDeletions(),
      AnnotationQueueAssignment: r.many.annotationQueueAssignments(),
      NotificationPreference: r.many.notificationPreferences(),
      DefaultView: r.many.defaultViews(),
      InAppAgentConversation: r.many.inAppAgentConversations(),
      InAppAgentEvent: r.many.inAppAgentEvents(),
      InAppAgentRun: r.many.inAppAgentRuns(),
      InAppAgentPendingToolApproval: r.many.inAppAgentPendingToolApprovals(),
      topicClusteringRuns: r.many.topicClusteringRuns(),
      gatewayConfigs: r.many.gatewayConfigs(),
    },
    apiKeys: {
      project: r.one.projects({ from: r.apiKeys.projectId, to: r.projects.id }),
      organization: r.one.organizations({ from: r.apiKeys.orgId, to: r.organizations.id }),
      createdByUser: r.one.users({
        from: r.apiKeys.createdByUserId,
        to: r.users.id,
        alias: 'ApiKeyCreatedByUser',
      }),
      user: r.one.users({ from: r.apiKeys.userId, to: r.users.id, alias: 'ApiKeyUser' }),
      createdByApiKey: r.one.apiKeys({
        from: r.apiKeys.createdByApiKeyId,
        to: r.apiKeys.id,
        alias: 'ApiKeyCreatedByApiKey',
      }),
      apiKeysCreated: r.many.apiKeys({ alias: 'ApiKeyCreatedByApiKey' }),
      gatewayAssociation: r.one.gatewayApiKeyAssociations({
        from: r.apiKeys.id,
        to: r.gatewayApiKeyAssociations.apiKeyId,
      }),
      roleAssignments: r.many.roleAssignments(),
    },
    roleAssignments: {
      project: r.one.projects({ from: r.roleAssignments.projectId, to: r.projects.id }),
      organization: r.one.organizations({
        from: r.roleAssignments.orgId,
        to: r.organizations.id,
        optional: false,
      }),
      user: r.one.users({ from: r.roleAssignments.userId, to: r.users.id }),
      apiKey: r.one.apiKeys({ from: r.roleAssignments.apiKeyId, to: r.apiKeys.id }),
    },
    gatewayConfigs: {
      organization: r.one.organizations({
        from: r.gatewayConfigs.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      defaultIngestionProject: r.one.projects({
        from: r.gatewayConfigs.defaultIngestionProjectId,
        to: r.projects.id,
      }),
    },
    gatewayAiConnections: {
      organization: r.one.organizations({
        from: r.gatewayAiConnections.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    gatewayApiKeyAssociations: {
      apiKey: r.one.apiKeys({
        from: r.gatewayApiKeyAssociations.apiKeyId,
        to: r.apiKeys.id,
        optional: false,
      }),
    },
    inAppAgentConversations: {
      project: r.one.projects({
        from: r.inAppAgentConversations.projectId,
        to: r.projects.id,
        optional: false,
      }),
      createdByUser: r.one.users({
        from: r.inAppAgentConversations.createdByUserId,
        to: r.users.id,
      }),
      events: r.many.inAppAgentEvents(),
      runs: r.many.inAppAgentRuns(),
      pendingToolApprovals: r.many.inAppAgentPendingToolApprovals(),
    },
    inAppAgentEvents: {
      project: r.one.projects({
        from: r.inAppAgentEvents.projectId,
        to: r.projects.id,
        optional: false,
      }),
      conversation: r.one.inAppAgentConversations({
        from: [r.inAppAgentEvents.conversationId, r.inAppAgentEvents.projectId],
        to: [r.inAppAgentConversations.id, r.inAppAgentConversations.projectId],
        optional: false,
      }),
      run: r.one.inAppAgentRuns({
        from: [r.inAppAgentEvents.runId, r.inAppAgentEvents.projectId],
        to: [r.inAppAgentRuns.id, r.inAppAgentRuns.projectId],
        optional: false,
      }),
    },
    inAppAgentRuns: {
      project: r.one.projects({
        from: r.inAppAgentRuns.projectId,
        to: r.projects.id,
        optional: false,
      }),
      conversation: r.one.inAppAgentConversations({
        from: [r.inAppAgentRuns.conversationId, r.inAppAgentRuns.projectId],
        to: [r.inAppAgentConversations.id, r.inAppAgentConversations.projectId],
        optional: false,
      }),
      triggeredByUser: r.one.users({ from: r.inAppAgentRuns.triggeredByUserId, to: r.users.id }),
      events: r.many.inAppAgentEvents(),
    },
    inAppAgentPendingToolApprovals: {
      project: r.one.projects({
        from: r.inAppAgentPendingToolApprovals.projectId,
        to: r.projects.id,
        optional: false,
      }),
      conversation: r.one.inAppAgentConversations({
        from: [
          r.inAppAgentPendingToolApprovals.conversationId,
          r.inAppAgentPendingToolApprovals.projectId,
        ],
        to: [r.inAppAgentConversations.id, r.inAppAgentConversations.projectId],
        optional: false,
      }),
    },
    llmApiKeys: {
      project: r.one.projects({ from: r.llmApiKeys.projectId, to: r.projects.id }),
      organization: r.one.organizations({
        from: r.llmApiKeys.organizationId,
        to: r.organizations.id,
      }),
      DefaultLlmModel: r.many.defaultLlmModels(),
      topicSummaryConfigs: r.many.topicConfigs({ alias: 'TopicConfigSummaryLlmApiKey' }),
      topicEmbeddingConfigs: r.many.topicConfigs({ alias: 'TopicConfigEmbeddingLlmApiKey' }),
      topicClusteringConfigs: r.many.topicConfigs({ alias: 'TopicConfigClusteringLlmApiKey' }),
    },
    organizationMemberships: {
      organization: r.one.organizations({
        from: r.organizationMemberships.orgId,
        to: r.organizations.id,
        optional: false,
      }),
      user: r.one.users({
        from: r.organizationMemberships.userId,
        to: r.users.id,
        optional: false,
      }),
      ProjectMemberships: r.many.projectMemberships(),
    },
    projectMemberships: {
      organizationMembership: r.one.organizationMemberships({
        from: r.projectMemberships.orgMembershipId,
        to: r.organizationMemberships.id,
        optional: false,
      }),
      project: r.one.projects({
        from: r.projectMemberships.projectId,
        to: r.projects.id,
        optional: false,
      }),
      user: r.one.users({ from: r.projectMemberships.userId, to: r.users.id, optional: false }),
    },
    membershipInvitations: {
      organization: r.one.organizations({
        from: r.membershipInvitations.orgId,
        to: r.organizations.id,
        optional: false,
      }),
      project: r.one.projects({ from: r.membershipInvitations.projectId, to: r.projects.id }),
      invitedByUser: r.one.users({ from: r.membershipInvitations.invitedByUserId, to: r.users.id }),
    },
    traceSessions: {
      project: r.one.projects({
        from: r.traceSessions.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    scoreConfigs: {
      project: r.one.projects({
        from: r.scoreConfigs.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    annotationQueues: {
      project: r.one.projects({
        from: r.annotationQueues.projectId,
        to: r.projects.id,
        optional: false,
      }),
      annotationQueueItem: r.many.annotationQueueItems(),
      annotationQueueAssignment: r.many.annotationQueueAssignments(),
    },
    annotationQueueItems: {
      queue: r.one.annotationQueues({
        from: r.annotationQueueItems.queueId,
        to: r.annotationQueues.id,
        optional: false,
      }),
      lockedByUser: r.one.users({
        from: r.annotationQueueItems.lockedByUserId,
        to: r.users.id,
        alias: 'LockedByUser',
      }),
      annotatorUser: r.one.users({
        from: r.annotationQueueItems.annotatorUserId,
        to: r.users.id,
        alias: 'AnnotatorUser',
      }),
      project: r.one.projects({
        from: r.annotationQueueItems.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    annotationQueueAssignments: {
      project: r.one.projects({
        from: r.annotationQueueAssignments.projectId,
        to: r.projects.id,
        optional: false,
      }),
      user: r.one.users({
        from: r.annotationQueueAssignments.userId,
        to: r.users.id,
        optional: false,
      }),
      queue: r.one.annotationQueues({
        from: r.annotationQueueAssignments.queueId,
        to: r.annotationQueues.id,
        optional: false,
      }),
    },
    datasets: {
      project: r.one.projects({ from: r.datasets.projectId, to: r.projects.id, optional: false }),
      datasetItems: r.many.datasetItems(),
      datasetRuns: r.many.datasetRuns(),
    },
    datasetItems: {
      dataset: r.one.datasets({
        from: [r.datasetItems.datasetId, r.datasetItems.projectId],
        to: [r.datasets.id, r.datasets.projectId],
        optional: false,
      }),
    },
    datasetRuns: {
      dataset: r.one.datasets({
        from: [r.datasetRuns.datasetId, r.datasetRuns.projectId],
        to: [r.datasets.id, r.datasets.projectId],
        optional: false,
      }),
    },
    comments: {
      project: r.one.projects({ from: r.comments.projectId, to: r.projects.id, optional: false }),
      reactions: r.many.commentReactions(),
    },
    commentReactions: {
      project: r.one.projects({
        from: r.commentReactions.projectId,
        to: r.projects.id,
        optional: false,
      }),
      comment: r.one.comments({
        from: r.commentReactions.commentId,
        to: r.comments.id,
        optional: false,
      }),
      user: r.one.users({ from: r.commentReactions.userId, to: r.users.id, optional: false }),
    },
    notificationPreferences: {
      user: r.one.users({
        from: r.notificationPreferences.userId,
        to: r.users.id,
        optional: false,
      }),
      project: r.one.projects({
        from: r.notificationPreferences.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    prompts: {
      project: r.one.projects({ from: r.prompts.projectId, to: r.projects.id, optional: false }),
      PromptDependency: r.many.promptDependencies(),
    },
    promptDependencies: {
      project: r.one.projects({
        from: r.promptDependencies.projectId,
        to: r.projects.id,
        optional: false,
      }),
      parent: r.one.prompts({
        from: r.promptDependencies.parentId,
        to: r.prompts.id,
        optional: false,
      }),
    },
    promptProtectedLabels: {
      project: r.one.projects({
        from: r.promptProtectedLabels.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    skills: {
      project: r.one.projects({ from: r.skills.projectId, to: r.projects.id, optional: false }),
      files: r.many.skillFiles(),
    },
    skillFiles: {
      project: r.one.projects({ from: r.skillFiles.projectId, to: r.projects.id, optional: false }),
      skill: r.one.skills({ from: r.skillFiles.skillId, to: r.skills.id, optional: false }),
      blob: r.one.skillBlobs({ from: r.skillFiles.blobId, to: r.skillBlobs.id, optional: false }),
    },
    skillBlobs: {
      project: r.one.projects({ from: r.skillBlobs.projectId, to: r.projects.id, optional: false }),
      files: r.many.skillFiles(),
    },
    models: {
      project: r.one.projects({ from: r.models.projectId, to: r.projects.id }),
      Price: r.many.prices(),
      pricingTiers: r.many.pricingTiers(),
    },
    prices: {
      Model: r.one.models({ from: r.prices.modelId, to: r.models.id, optional: false }),
      project: r.one.projects({ from: r.prices.projectId, to: r.projects.id }),
      pricingTier: r.one.pricingTiers({
        from: r.prices.pricingTierId,
        to: r.pricingTiers.id,
        optional: false,
      }),
    },
    pricingTiers: {
      model: r.one.models({ from: r.pricingTiers.modelId, to: r.models.id, optional: false }),
      prices: r.many.prices(),
    },
    evalTemplates: {
      project: r.one.projects({ from: r.evalTemplates.projectId, to: r.projects.id }),
      JobConfiguration: r.many.jobConfigurations(),
    },
    evaluators: {
      project: r.one.projects({ from: r.evaluators.projectId, to: r.projects.id, optional: false }),
      createdByUser: r.one.users({ from: r.evaluators.createdByUserId, to: r.users.id }),
      versions: r.many.evaluatorVersions(),
      assignments: r.many.evaluationRuleEvaluatorAssignments(),
    },
    evaluatorVersions: {
      evaluator: r.one.evaluators({
        from: r.evaluatorVersions.evaluatorId,
        to: r.evaluators.id,
        optional: false,
      }),
      createdByUser: r.one.users({ from: r.evaluatorVersions.createdByUserId, to: r.users.id }),
      topicClusteringRuns: r.many.topicClusteringRuns(),
    },
    jobConfigurations: {
      project: r.one.projects({
        from: r.jobConfigurations.projectId,
        to: r.projects.id,
        optional: false,
      }),
      evalTemplate: r.one.evalTemplates({
        from: r.jobConfigurations.evalTemplateId,
        to: r.evalTemplates.id,
      }),
    },
    evaluationRules: {
      project: r.one.projects({
        from: r.evaluationRules.projectId,
        to: r.projects.id,
        optional: false,
      }),
      createdByUser: r.one.users({ from: r.evaluationRules.createdByUserId, to: r.users.id }),
      assignments: r.many.evaluationRuleEvaluatorAssignments(),
    },
    tracingReadinessSettings: {
      project: r.one.projects({
        from: r.tracingReadinessSettings.projectId,
        to: r.projects.id,
        optional: false,
      }),
      createdByUser: r.one.users({
        from: r.tracingReadinessSettings.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.tracingReadinessSettings.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
    },
    evaluationRuleEvaluatorAssignments: {
      project: r.one.projects({
        from: r.evaluationRuleEvaluatorAssignments.projectId,
        to: r.projects.id,
        optional: false,
      }),
      evaluationRule: r.one.evaluationRules({
        from: r.evaluationRuleEvaluatorAssignments.evaluationRuleId,
        to: r.evaluationRules.id,
        optional: false,
      }),
      evaluator: r.one.evaluators({
        from: r.evaluationRuleEvaluatorAssignments.evaluatorId,
        to: r.evaluators.id,
        optional: false,
      }),
    },
    jobExecutions: {
      project: r.one.projects({
        from: r.jobExecutions.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    defaultLlmModels: {
      Project: r.one.projects({
        from: r.defaultLlmModels.projectId,
        to: r.projects.id,
        optional: false,
      }),
      LlmApiKey: r.one.llmApiKeys({
        from: r.defaultLlmModels.llmApiKeyId,
        to: r.llmApiKeys.id,
        optional: false,
      }),
    },
    topicConfigs: {
      createdByUser: r.one.users({
        from: r.topicConfigs.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.topicConfigs.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
      project: r.one.projects({
        from: r.topicConfigs.projectId,
        to: r.projects.id,
        optional: false,
      }),
      summaryLlmApiKey: r.one.llmApiKeys({
        from: r.topicConfigs.summaryLlmApiKeyId,
        to: r.llmApiKeys.id,
        alias: 'TopicConfigSummaryLlmApiKey',
      }),
      embeddingLlmApiKey: r.one.llmApiKeys({
        from: r.topicConfigs.embeddingLlmApiKeyId,
        to: r.llmApiKeys.id,
        alias: 'TopicConfigEmbeddingLlmApiKey',
      }),
      clusteringLlmApiKey: r.one.llmApiKeys({
        from: r.topicConfigs.clusteringLlmApiKeyId,
        to: r.llmApiKeys.id,
        alias: 'TopicConfigClusteringLlmApiKey',
      }),
    },
    verifiedDomains: {
      organization: r.one.organizations({
        from: r.verifiedDomains.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    posthogIntegrations: {
      project: r.one.projects({
        from: r.posthogIntegrations.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    mixpanelIntegrations: {
      project: r.one.projects({
        from: r.mixpanelIntegrations.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    blobStorageIntegrations: {
      project: r.one.projects({
        from: r.blobStorageIntegrations.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    externalMediaStorageIntegrations: {
      project: r.one.projects({
        from: r.externalMediaStorageIntegrations.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    webCalloutEndpoints: {
      project: r.one.projects({
        from: r.webCalloutEndpoints.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    batchExports: {
      project: r.one.projects({
        from: r.batchExports.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    batchActions: {
      project: r.one.projects({
        from: r.batchActions.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    media: {
      project: r.one.projects({ from: r.media.projectId, to: r.projects.id, optional: false }),
    },
    llmSchemas: {
      project: r.one.projects({ from: r.llmSchemas.projectId, to: r.projects.id, optional: false }),
    },
    llmTools: {
      project: r.one.projects({ from: r.llmTools.projectId, to: r.projects.id, optional: false }),
    },
    dashboards: {
      createdByUser: r.one.users({
        from: r.dashboards.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.dashboards.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
      project: r.one.projects({
        from: r.dashboards.projectId,
        to: r.projects.id,
        alias: 'DashboardToProject',
      }),
      homeDashboardProjects: r.many.projects({ alias: 'ProjectHomeDashboard' }),
    },
    dashboardWidgets: {
      createdByUser: r.one.users({
        from: r.dashboardWidgets.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.dashboardWidgets.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
      project: r.one.projects({ from: r.dashboardWidgets.projectId, to: r.projects.id }),
    },
    tableViewPresets: {
      project: r.one.projects({
        from: r.tableViewPresets.projectId,
        to: r.projects.id,
        optional: false,
      }),
      createdByUser: r.one.users({
        from: r.tableViewPresets.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.tableViewPresets.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
    },
    defaultViews: {
      project: r.one.projects({
        from: r.defaultViews.projectId,
        to: r.projects.id,
        optional: false,
      }),
      user: r.one.users({ from: r.defaultViews.userId, to: r.users.id }),
    },
    actions: {
      project: r.one.projects({ from: r.actions.projectId, to: r.projects.id, optional: false }),
      automations: r.many.automations(),
      automationExecutions: r.many.automationExecutions(),
    },
    triggers: {
      project: r.one.projects({ from: r.triggers.projectId, to: r.projects.id, optional: false }),
      automationExecutions: r.many.automationExecutions(),
      automations: r.many.automations(),
    },
    automations: {
      trigger: r.one.triggers({
        from: r.automations.triggerId,
        to: r.triggers.id,
        optional: false,
      }),
      action: r.one.actions({ from: r.automations.actionId, to: r.actions.id, optional: false }),
      project: r.one.projects({
        from: r.automations.projectId,
        to: r.projects.id,
        optional: false,
      }),
      AutomationExecution: r.many.automationExecutions(),
    },
    automationExecutions: {
      automation: r.one.automations({
        from: r.automationExecutions.automationId,
        to: r.automations.id,
        optional: false,
      }),
      trigger: r.one.triggers({
        from: r.automationExecutions.triggerId,
        to: r.triggers.id,
        optional: false,
      }),
      action: r.one.actions({
        from: r.automationExecutions.actionId,
        to: r.actions.id,
        optional: false,
      }),
      project: r.one.projects({
        from: r.automationExecutions.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    monitors: {
      createdByUser: r.one.users({
        from: r.monitors.createdBy,
        to: r.users.id,
        alias: 'CreatedByUser',
      }),
      updatedByUser: r.one.users({
        from: r.monitors.updatedBy,
        to: r.users.id,
        alias: 'UpdatedByUser',
      }),
      project: r.one.projects({ from: r.monitors.projectId, to: r.projects.id, optional: false }),
    },
    slackIntegrations: {
      project: r.one.projects({
        from: r.slackIntegrations.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    pendingDeletions: {
      project: r.one.projects({
        from: r.pendingDeletions.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    surveys: {
      org: r.one.organizations({ from: r.surveys.orgId, to: r.organizations.id }),
      user: r.one.users({ from: r.surveys.userId, to: r.users.id }),
    },
    cloudSpendAlerts: {
      org: r.one.organizations({
        from: r.cloudSpendAlerts.orgId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    topicClusteringRuns: {
      project: r.one.projects({
        from: r.topicClusteringRuns.projectId,
        to: r.projects.id,
        optional: false,
      }),
      facet: r.one.evaluatorVersions({
        from: [r.topicClusteringRuns.facetId, r.topicClusteringRuns.facetVersion],
        to: [r.evaluatorVersions.evaluatorId, r.evaluatorVersions.version],
        optional: false,
      }),
    },
  }),
)
