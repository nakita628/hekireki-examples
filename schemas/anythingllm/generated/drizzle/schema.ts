import {
  customType,
  foreignKey,
  index,
  integer,
  real,
  sqliteTable,
  text,
  uniqueIndex,
} from 'drizzle-orm/sqlite-core'
import { defineRelations } from 'drizzle-orm'

const utcDateTime = customType<{ data: Date; driverData: string | number }>({
  dataType: () => 'datetime',
  toDriver: (value) => value.toISOString().replace('Z', '+00:00'),
  fromDriver: (value) => {
    if (typeof value === 'number' || /^-?\d+$/u.test(value)) return new Date(Number(value))
    const iso = value.replace(' ', 'T').replace(/ (?=[+-]\d\d:?\d\d$)/u, '')
    return new Date(/T[\d:.]+$/u.test(iso) ? `${iso}Z` : iso)
  },
})

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

export const apiKeys = sqliteTable(
  'api_keys',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    name: text('name'),
    secret: text('secret'),
    createdBy: integer('createdBy'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('api_keys_secret_key').on(table.secret)],
)

export const workspaceDocuments = sqliteTable(
  'workspace_documents',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    docId: text('docId').notNull(),
    filename: text('filename').notNull(),
    docpath: text('docpath').notNull(),
    workspaceId: integer('workspaceId').notNull(),
    metadata: text('metadata'),
    pinned: integer('pinned', { mode: 'boolean' }).default(false),
    watched: integer('watched', { mode: 'boolean' }).default(false),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('workspace_documents_docId_key').on(table.docId),
    foreignKey({
      name: 'workspace_documents_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspaces.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const invites = sqliteTable(
  'invites',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    code: text('code').notNull(),
    status: text('status').notNull().default('pending'),
    claimedBy: integer('claimedBy'),
    workspaceIds: text('workspaceIds'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    createdBy: integer('createdBy').notNull(),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('invites_code_key').on(table.code)],
)

export const systemSettings = sqliteTable(
  'system_settings',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    label: text('label').notNull(),
    value: text('value'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('system_settings_label_key').on(table.label)],
)

export const users = sqliteTable(
  'users',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    username: text('username'),
    password: text('password').notNull(),
    pfpFilename: text('pfpFilename'),
    role: text('role').notNull().default('default'),
    suspended: integer('suspended').notNull().default(0),
    seen_recovery_codes: integer('seen_recovery_codes', { mode: 'boolean' }).default(false),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
    dailyMessageLimit: integer('dailyMessageLimit'),
    bio: text('bio').default(''),
    web_push_subscription_config: text('web_push_subscription_config'),
  },
  (table) => [uniqueIndex('users_username_key').on(table.username)],
)

export const recoveryCodes = sqliteTable(
  'recovery_codes',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    user_id: integer('user_id').notNull(),
    code_hash: text('code_hash').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    index('recovery_codes_user_id_idx').on(table.user_id),
    foreignKey({
      name: 'recovery_codes_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const passwordResetTokens = sqliteTable(
  'password_reset_tokens',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    user_id: integer('user_id').notNull(),
    token: text('token').notNull(),
    expiresAt: utcDateTime('expiresAt').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('password_reset_tokens_token_key').on(table.token),
    index('password_reset_tokens_user_id_idx').on(table.user_id),
    foreignKey({
      name: 'password_reset_tokens_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentVectors = sqliteTable('document_vectors', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  docId: text('docId').notNull(),
  vectorId: text('vectorId').notNull(),
  createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
})

export const workspaces = sqliteTable(
  'workspaces',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    vectorTag: text('vectorTag'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    openAiTemp: real('openAiTemp'),
    openAiHistory: integer('openAiHistory').notNull().default(20),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
    openAiPrompt: text('openAiPrompt'),
    similarityThreshold: real('similarityThreshold').default(0.25),
    chatProvider: text('chatProvider'),
    chatModel: text('chatModel'),
    topN: integer('topN').default(4),
    chatMode: text('chatMode').default('chat'),
    pfpFilename: text('pfpFilename'),
    agentProvider: text('agentProvider'),
    agentModel: text('agentModel'),
    queryRefusalResponse: text('queryRefusalResponse'),
    vectorSearchMode: text('vectorSearchMode').default('default'),
    router_id: integer('router_id'),
  },
  (table) => [uniqueIndex('workspaces_slug_key').on(table.slug)],
)

export const workspaceThreads = sqliteTable(
  'workspace_threads',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    workspace_id: integer('workspace_id').notNull(),
    user_id: integer('user_id'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('workspace_threads_slug_key').on(table.slug),
    index('workspace_threads_workspace_id_idx').on(table.workspace_id),
    index('workspace_threads_user_id_idx').on(table.user_id),
    foreignKey({
      name: 'workspace_threads_workspace_id_fkey',
      columns: [table.workspace_id],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workspace_threads_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceSuggestedMessages = sqliteTable(
  'workspace_suggested_messages',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    workspaceId: integer('workspaceId').notNull(),
    heading: text('heading').notNull(),
    message: text('message').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    index('workspace_suggested_messages_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'workspace_suggested_messages_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceChats = sqliteTable(
  'workspace_chats',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    workspaceId: integer('workspaceId').notNull(),
    prompt: text('prompt').notNull(),
    response: text('response').notNull(),
    include: integer('include', { mode: 'boolean' }).notNull().default(true),
    user_id: integer('user_id'),
    thread_id: integer('thread_id'),
    api_session_id: text('api_session_id'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
    feedbackScore: integer('feedbackScore', { mode: 'boolean' }),
    memoryProcessed: integer('memory_processed', { mode: 'boolean' }),
  },
  (table) => [
    foreignKey({
      name: 'workspace_chats_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceAgentInvocations = sqliteTable(
  'workspace_agent_invocations',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    prompt: text('prompt').notNull(),
    closed: integer('closed', { mode: 'boolean' }).notNull().default(false),
    user_id: integer('user_id'),
    thread_id: integer('thread_id'),
    workspace_id: integer('workspace_id').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('workspace_agent_invocations_uuid_key').on(table.uuid),
    index('workspace_agent_invocations_uuid_idx').on(table.uuid),
    foreignKey({
      name: 'workspace_agent_invocations_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workspace_agent_invocations_workspace_id_fkey',
      columns: [table.workspace_id],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceUsers = sqliteTable(
  'workspace_users',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    user_id: integer('user_id').notNull(),
    workspace_id: integer('workspace_id').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'workspace_users_workspace_id_fkey',
      columns: [table.workspace_id],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workspace_users_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cacheData = sqliteTable('cache_data', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  name: text('name').notNull(),
  data: text('data').notNull(),
  belongsTo: text('belongsTo'),
  byId: integer('byId'),
  expiresAt: utcDateTime('expiresAt'),
  createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
})

export const embedConfigs = sqliteTable(
  'embed_configs',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    enabled: integer('enabled', { mode: 'boolean' }).notNull().default(false),
    chat_mode: text('chat_mode').notNull().default('query'),
    allowlist_domains: text('allowlist_domains'),
    allow_model_override: integer('allow_model_override', { mode: 'boolean' })
      .notNull()
      .default(false),
    allow_temperature_override: integer('allow_temperature_override', { mode: 'boolean' })
      .notNull()
      .default(false),
    allow_prompt_override: integer('allow_prompt_override', { mode: 'boolean' })
      .notNull()
      .default(false),
    max_chats_per_day: integer('max_chats_per_day'),
    max_chats_per_session: integer('max_chats_per_session'),
    message_limit: integer('message_limit').default(20),
    workspace_id: integer('workspace_id').notNull(),
    createdBy: integer('createdBy'),
    usersId: integer('usersId'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('embed_configs_uuid_key').on(table.uuid),
    foreignKey({
      name: 'embed_configs_workspace_id_fkey',
      columns: [table.workspace_id],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'embed_configs_usersId_fkey',
      columns: [table.usersId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const embedChats = sqliteTable(
  'embed_chats',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    prompt: text('prompt').notNull(),
    response: text('response').notNull(),
    session_id: text('session_id').notNull(),
    include: integer('include', { mode: 'boolean' }).notNull().default(true),
    connection_information: text('connection_information'),
    embed_id: integer('embed_id').notNull(),
    usersId: integer('usersId'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'embed_chats_embed_id_fkey',
      columns: [table.embed_id],
      foreignColumns: [embedConfigs.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'embed_chats_usersId_fkey',
      columns: [table.usersId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const eventLogs = sqliteTable(
  'event_logs',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    event: text('event').notNull(),
    metadata: text('metadata'),
    userId: integer('userId'),
    occurredAt: utcDateTime('occurredAt').notNull().$defaultFn(utcNow),
  },
  (table) => [index('event_logs_event_idx').on(table.event)],
)

export const slashCommandPresets = sqliteTable(
  'slash_command_presets',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    command: text('command').notNull(),
    prompt: text('prompt').notNull(),
    description: text('description').notNull(),
    uid: integer('uid').notNull().default(0),
    userId: integer('userId'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('slash_command_presets_uid_command_key').on(table.uid, table.command),
    foreignKey({
      name: 'slash_command_presets_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentSyncQueues = sqliteTable(
  'document_sync_queues',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    staleAfterMs: integer('staleAfterMs').notNull().default(604800000),
    nextSyncAt: utcDateTime('nextSyncAt').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastSyncedAt: utcDateTime('lastSyncedAt').notNull().$defaultFn(utcNow),
    workspaceDocId: integer('workspaceDocId').notNull(),
  },
  (table) => [
    uniqueIndex('document_sync_queues_workspaceDocId_key').on(table.workspaceDocId),
    foreignKey({
      name: 'document_sync_queues_workspaceDocId_fkey',
      columns: [table.workspaceDocId],
      foreignColumns: [workspaceDocuments.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentSyncExecutions = sqliteTable(
  'document_sync_executions',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    queueId: integer('queueId').notNull(),
    status: text('status').notNull().default('unknown'),
    result: text('result'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'document_sync_executions_queueId_fkey',
      columns: [table.queueId],
      foreignColumns: [documentSyncQueues.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const browserExtensionApiKeys = sqliteTable(
  'browser_extension_api_keys',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    key: text('key').notNull(),
    user_id: integer('user_id'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('browser_extension_api_keys_key_key').on(table.key),
    index('browser_extension_api_keys_user_id_idx').on(table.user_id),
    foreignKey({
      name: 'browser_extension_api_keys_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const temporaryAuthTokens = sqliteTable(
  'temporary_auth_tokens',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    token: text('token').notNull(),
    userId: integer('userId').notNull(),
    expiresAt: utcDateTime('expiresAt').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('temporary_auth_tokens_token_key').on(table.token),
    index('temporary_auth_tokens_token_idx').on(table.token),
    index('temporary_auth_tokens_userId_idx').on(table.userId),
    foreignKey({
      name: 'temporary_auth_tokens_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const systemPromptVariables = sqliteTable(
  'system_prompt_variables',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    key: text('key').notNull(),
    value: text('value'),
    description: text('description'),
    type: text('type').notNull().default('system'),
    userId: integer('userId'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    updatedAt: utcDateTime('updatedAt').notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('system_prompt_variables_key_key').on(table.key),
    index('system_prompt_variables_userId_idx').on(table.userId),
    foreignKey({
      name: 'system_prompt_variables_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const promptHistory = sqliteTable(
  'prompt_history',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    workspaceId: integer('workspaceId').notNull(),
    prompt: text('prompt').notNull(),
    modifiedBy: integer('modifiedBy'),
    modifiedAt: utcDateTime('modifiedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    index('prompt_history_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'prompt_history_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'prompt_history_modifiedBy_fkey',
      columns: [table.modifiedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const desktopMobileDevices = sqliteTable(
  'desktop_mobile_devices',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    deviceOs: text('deviceOs').notNull(),
    deviceName: text('deviceName').notNull(),
    token: text('token').notNull(),
    approved: integer('approved', { mode: 'boolean' }).notNull().default(false),
    userId: integer('userId'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('desktop_mobile_devices_token_key').on(table.token),
    index('desktop_mobile_devices_userId_idx').on(table.userId),
    foreignKey({
      name: 'desktop_mobile_devices_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceParsedFiles = sqliteTable(
  'workspace_parsed_files',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    filename: text('filename').notNull(),
    workspaceId: integer('workspaceId').notNull(),
    userId: integer('userId'),
    threadId: integer('threadId'),
    metadata: text('metadata'),
    tokenCountEstimate: integer('tokenCountEstimate').default(0),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('workspace_parsed_files_filename_key').on(table.filename),
    index('workspace_parsed_files_workspaceId_idx').on(table.workspaceId),
    index('workspace_parsed_files_userId_idx').on(table.userId),
    foreignKey({
      name: 'workspace_parsed_files_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workspace_parsed_files_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workspace_parsed_files_threadId_fkey',
      columns: [table.threadId],
      foreignColumns: [workspaceThreads.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const externalCommunicationConnectors = sqliteTable(
  'external_communication_connectors',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    type: text('type').notNull(),
    config: text('config').notNull().default('{}'),
    active: integer('active', { mode: 'boolean' }).notNull().default(false),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('external_communication_connectors_type_key').on(table.type)],
)

export const scheduledJobs = sqliteTable('scheduled_jobs', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  name: text('name').notNull(),
  prompt: text('prompt').notNull(),
  tools: text('tools'),
  schedule: text('schedule').notNull(),
  enabled: integer('enabled', { mode: 'boolean' }).notNull().default(true),
  lastRunAt: utcDateTime('lastRunAt'),
  nextRunAt: utcDateTime('nextRunAt'),
  createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  updatedAt: utcDateTime('updatedAt').notNull().$defaultFn(utcNow),
})

export const scheduledJobRuns = sqliteTable(
  'scheduled_job_runs',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    jobId: integer('jobId').notNull(),
    status: text('status').notNull().default('queued'),
    result: text('result'),
    error: text('error'),
    startedAt: utcDateTime('startedAt').notNull().$defaultFn(utcNow),
    completedAt: utcDateTime('completedAt'),
    readAt: utcDateTime('readAt'),
  },
  (table) => [
    index('scheduled_job_runs_jobId_idx').on(table.jobId),
    foreignKey({
      name: 'scheduled_job_runs_jobId_fkey',
      columns: [table.jobId],
      foreignColumns: [scheduledJobs.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const memories = sqliteTable(
  'memories',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    userId: integer('user_id'),
    workspaceId: integer('workspace_id'),
    scope: text('scope').notNull().default('workspace'),
    content: text('content').notNull(),
    lastUsedAt: utcDateTime('last_used_at'),
    createdAt: utcDateTime('created_at').notNull().$defaultFn(utcNow),
    updatedAt: utcDateTime('updated_at').notNull().$defaultFn(utcNow),
  },
  (table) => [
    index('memories_user_id_workspace_id_idx').on(table.userId, table.workspaceId),
    index('memories_user_id_scope_idx').on(table.userId, table.scope),
    foreignKey({
      name: 'memories_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'memories_workspace_id_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const modelRouters = sqliteTable(
  'model_routers',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    name: text('name').notNull(),
    description: text('description'),
    fallback_provider: text('fallback_provider').notNull(),
    fallback_model: text('fallback_model').notNull(),
    cooldown_seconds: integer('cooldown_seconds').notNull().default(30),
    created_by: integer('created_by'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('model_routers_name_key').on(table.name)],
)

export const modelRouterRules = sqliteTable(
  'model_router_rules',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    router_id: integer('router_id').notNull(),
    enabled: integer('enabled', { mode: 'boolean' }).notNull().default(true),
    priority: integer('priority').notNull(),
    type: text('type').notNull().default('calculated'),
    title: text('title').notNull(),
    description: text('description'),
    condition_logic: text('condition_logic'),
    conditions: text('conditions'),
    route_provider: text('route_provider').notNull(),
    route_model: text('route_model').notNull(),
    created_by: integer('created_by'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    lastUpdatedAt: utcDateTime('lastUpdatedAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    index('model_router_rules_router_id_idx').on(table.router_id),
    index('model_router_rules_router_id_enabled_priority_idx').on(
      table.router_id,
      table.enabled,
      table.priority,
    ),
    uniqueIndex('model_router_rules_router_id_title_key').on(table.router_id, table.title),
    foreignKey({
      name: 'model_router_rules_router_id_fkey',
      columns: [table.router_id],
      foreignColumns: [modelRouters.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    apiKeys,
    workspaceDocuments,
    invites,
    systemSettings,
    users,
    recoveryCodes,
    passwordResetTokens,
    documentVectors,
    workspaces,
    workspaceThreads,
    workspaceSuggestedMessages,
    workspaceChats,
    workspaceAgentInvocations,
    workspaceUsers,
    cacheData,
    embedConfigs,
    embedChats,
    eventLogs,
    slashCommandPresets,
    documentSyncQueues,
    documentSyncExecutions,
    browserExtensionApiKeys,
    temporaryAuthTokens,
    systemPromptVariables,
    promptHistory,
    desktopMobileDevices,
    workspaceParsedFiles,
    externalCommunicationConnectors,
    scheduledJobs,
    scheduledJobRuns,
    memories,
    modelRouters,
    modelRouterRules,
  },
  (r) => ({
    workspaceDocuments: {
      workspace: r.one.workspaces({
        from: r.workspaceDocuments.workspaceId,
        to: r.workspaces.id,
        optional: false,
      }),
      document_sync_queues: r.one.documentSyncQueues({
        from: r.workspaceDocuments.id,
        to: r.documentSyncQueues.workspaceDocId,
      }),
    },
    users: {
      workspace_chats: r.many.workspaceChats(),
      workspace_users: r.many.workspaceUsers(),
      embed_configs: r.many.embedConfigs(),
      embed_chats: r.many.embedChats(),
      threads: r.many.workspaceThreads(),
      recovery_codes: r.many.recoveryCodes(),
      password_reset_tokens: r.many.passwordResetTokens(),
      workspace_agent_invocations: r.many.workspaceAgentInvocations(),
      slash_command_presets: r.many.slashCommandPresets(),
      browser_extension_api_keys: r.many.browserExtensionApiKeys(),
      temporary_auth_tokens: r.many.temporaryAuthTokens(),
      system_prompt_variables: r.many.systemPromptVariables(),
      prompt_history: r.many.promptHistory(),
      desktop_mobile_devices: r.many.desktopMobileDevices(),
      workspace_parsed_files: r.many.workspaceParsedFiles(),
      memories: r.many.memories(),
    },
    recoveryCodes: {
      user: r.one.users({ from: r.recoveryCodes.user_id, to: r.users.id, optional: false }),
    },
    passwordResetTokens: {
      user: r.one.users({ from: r.passwordResetTokens.user_id, to: r.users.id, optional: false }),
    },
    workspaces: {
      workspace_users: r.many.workspaceUsers(),
      documents: r.many.workspaceDocuments(),
      workspace_suggested_messages: r.many.workspaceSuggestedMessages(),
      embed_configs: r.many.embedConfigs(),
      threads: r.many.workspaceThreads(),
      workspace_agent_invocations: r.many.workspaceAgentInvocations(),
      prompt_history: r.many.promptHistory(),
      workspace_parsed_files: r.many.workspaceParsedFiles(),
      memories: r.many.memories(),
    },
    workspaceThreads: {
      workspace: r.one.workspaces({
        from: r.workspaceThreads.workspace_id,
        to: r.workspaces.id,
        optional: false,
      }),
      user: r.one.users({ from: r.workspaceThreads.user_id, to: r.users.id }),
      workspace_parsed_files: r.many.workspaceParsedFiles(),
    },
    workspaceSuggestedMessages: {
      workspace: r.one.workspaces({
        from: r.workspaceSuggestedMessages.workspaceId,
        to: r.workspaces.id,
        optional: false,
      }),
    },
    workspaceChats: { users: r.one.users({ from: r.workspaceChats.user_id, to: r.users.id }) },
    workspaceAgentInvocations: {
      user: r.one.users({ from: r.workspaceAgentInvocations.user_id, to: r.users.id }),
      workspace: r.one.workspaces({
        from: r.workspaceAgentInvocations.workspace_id,
        to: r.workspaces.id,
        optional: false,
      }),
    },
    workspaceUsers: {
      workspaces: r.one.workspaces({
        from: r.workspaceUsers.workspace_id,
        to: r.workspaces.id,
        optional: false,
      }),
      users: r.one.users({ from: r.workspaceUsers.user_id, to: r.users.id, optional: false }),
    },
    embedConfigs: {
      workspace: r.one.workspaces({
        from: r.embedConfigs.workspace_id,
        to: r.workspaces.id,
        optional: false,
      }),
      embed_chats: r.many.embedChats(),
      users: r.one.users({ from: r.embedConfigs.usersId, to: r.users.id }),
    },
    embedChats: {
      embed_config: r.one.embedConfigs({
        from: r.embedChats.embed_id,
        to: r.embedConfigs.id,
        optional: false,
      }),
      users: r.one.users({ from: r.embedChats.usersId, to: r.users.id }),
    },
    slashCommandPresets: {
      user: r.one.users({ from: r.slashCommandPresets.userId, to: r.users.id }),
    },
    documentSyncQueues: {
      workspaceDoc: r.one.workspaceDocuments({
        from: r.documentSyncQueues.workspaceDocId,
        to: r.workspaceDocuments.id,
      }),
      runs: r.many.documentSyncExecutions(),
    },
    documentSyncExecutions: {
      queue: r.one.documentSyncQueues({
        from: r.documentSyncExecutions.queueId,
        to: r.documentSyncQueues.id,
        optional: false,
      }),
    },
    browserExtensionApiKeys: {
      user: r.one.users({ from: r.browserExtensionApiKeys.user_id, to: r.users.id }),
    },
    temporaryAuthTokens: {
      user: r.one.users({ from: r.temporaryAuthTokens.userId, to: r.users.id, optional: false }),
    },
    systemPromptVariables: {
      user: r.one.users({ from: r.systemPromptVariables.userId, to: r.users.id }),
    },
    promptHistory: {
      workspace: r.one.workspaces({
        from: r.promptHistory.workspaceId,
        to: r.workspaces.id,
        optional: false,
      }),
      user: r.one.users({ from: r.promptHistory.modifiedBy, to: r.users.id }),
    },
    desktopMobileDevices: {
      user: r.one.users({ from: r.desktopMobileDevices.userId, to: r.users.id }),
    },
    workspaceParsedFiles: {
      workspace: r.one.workspaces({
        from: r.workspaceParsedFiles.workspaceId,
        to: r.workspaces.id,
        optional: false,
      }),
      user: r.one.users({ from: r.workspaceParsedFiles.userId, to: r.users.id }),
      thread: r.one.workspaceThreads({
        from: r.workspaceParsedFiles.threadId,
        to: r.workspaceThreads.id,
      }),
    },
    scheduledJobs: { runs: r.many.scheduledJobRuns() },
    scheduledJobRuns: {
      job: r.one.scheduledJobs({
        from: r.scheduledJobRuns.jobId,
        to: r.scheduledJobs.id,
        optional: false,
      }),
    },
    memories: {
      user: r.one.users({ from: r.memories.userId, to: r.users.id }),
      workspace: r.one.workspaces({ from: r.memories.workspaceId, to: r.workspaces.id }),
    },
    modelRouters: { rules: r.many.modelRouterRules() },
    modelRouterRules: {
      router: r.one.modelRouters({
        from: r.modelRouterRules.router_id,
        to: r.modelRouters.id,
        optional: false,
      }),
    },
  }),
)
