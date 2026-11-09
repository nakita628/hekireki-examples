import {
  bigint,
  bigserial,
  boolean,
  customType,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  uniqueIndex,
  uuid,
  varchar,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

const bytea = customType<{ data: Uint8Array }>({
  dataType: () => 'bytea',
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

export const workspaceRoleEnum = pgEnum('WorkspaceRole', ['owner', 'editor', 'analyst'])

export const reverseSyncModeEnum = pgEnum('reverse_sync_mode', ['upsert', 'mirror'])

export const reverseSyncExtractionEnum = pgEnum('reverse_sync_extraction', ['cursor', 'full'])

export const emailLog = pgTable('EmailLog', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  time: timestamp('time', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  email: jsonb('email').notNull(),
  error: text('error'),
  previewUrl: text('previewUrl'),
  messageId: text('messageId'),
  status: text('status').notNull(),
})

export const userPreferences = pgTable(
  'UserPreferences',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    userId: text('userId').notNull(),
    workspaceId: text('workspaceId'),
    scope: text('scope'),
    preferences: jsonb('preferences').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'UserPreferences_userId_fkey',
      columns: [table.userId],
      foreignColumns: [userProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserPreferences_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const userProfile = pgTable(
  'UserProfile',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    email: text('email').notNull(),
    admin: boolean('admin').default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    loginProvider: text('loginProvider').notNull(),
    externalUsername: text('externalUsername'),
    externalId: text('externalId').notNull(),
  },
  (table) => [
    index('UserProfile_externalId_idx').on(table.externalId),
    uniqueIndex('UserProfile_loginProvider_externalId_key').on(
      table.loginProvider,
      table.externalId,
    ),
  ],
)

export const userPassword = pgTable(
  'UserPassword',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    userId: text('userId').notNull(),
    hash: text('hash').notNull(),
    changeAtNextLogin: boolean('changeAtNextLogin').notNull().default(false),
  },
  (table) => [
    uniqueIndex('UserPassword_userId_key').on(table.userId),
    foreignKey({
      name: 'UserPassword_userId_fkey',
      columns: [table.userId],
      foreignColumns: [userProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workspace = pgTable(
  'Workspace',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    name: text('name').notNull(),
    deleted: boolean('deleted').notNull().default(false),
    slug: text('slug'),
    featuresEnabled: text('featuresEnabled').array().default([]),
  },
  (table) => [
    index('Workspace_slug_idx').on(table.slug),
    index('Workspace_updatedAt_idx').on(table.updatedAt),
    uniqueIndex('Workspace_slug_key').on(table.slug),
  ],
)

export const workspaceOptions = pgTable(
  'WorkspaceOptions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    workspaceId: text('workspaceId').notNull(),
    namespace: text('namespace').notNull(),
    value: jsonb('value').notNull(),
  },
  (table) => [
    index('WorkspaceOptions_workspaceId_namespace_idx').on(table.workspaceId, table.namespace),
    foreignKey({
      name: 'WorkspaceOptions_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workspaceAccess = pgTable(
  'WorkspaceAccess',
  {
    workspaceId: text('workspaceId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    userId: text('userId').notNull(),
    role: workspaceRoleEnum('role').notNull().default('owner'),
  },
  (table) => [
    primaryKey({ name: 'WorkspaceAccess_pkey', columns: [table.userId, table.workspaceId] }),
    foreignKey({
      name: 'WorkspaceAccess_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkspaceAccess_userId_fkey',
      columns: [table.userId],
      foreignColumns: [userProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workspaceUserProperties = pgTable(
  'WorkspaceUserProperties',
  {
    workspaceId: text('workspaceId').notNull(),
    userId: text('userId').notNull(),
    lastUsed: timestamp('lastUsed', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'WorkspaceUserProperties_pkey',
      columns: [table.workspaceId, table.userId],
    }),
    foreignKey({
      name: 'WorkspaceUserProperties_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkspaceUserProperties_userId_fkey',
      columns: [table.userId],
      foreignColumns: [userProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const userApiToken = pgTable(
  'UserApiToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    lastUsed: timestamp('lastUsed', { precision: 3 }),
    hint: text('hint').notNull(),
    hash: text('hash').notNull(),
    userId: text('userId').notNull(),
    type: text('type'),
    name: text('name'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    oauthClientId: text('oauthClientId'),
  },
  (table) => [
    foreignKey({
      name: 'UserApiToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [userProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserApiToken_oauthClientId_fkey',
      columns: [table.oauthClientId],
      foreignColumns: [oAuthClient.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const oAuthClient = pgTable('OAuthClient', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  clientSecretHash: text('clientSecretHash').notNull(),
  name: text('name').notNull(),
  redirectUris: text('redirectUris').array(),
})

export const kv = pgTable(
  'kv',
  {
    key: text('key').primaryKey(),
    value: jsonb('value').notNull(),
    expiresAt: timestamp('expires_at', { precision: 3 }),
  },
  (table) => [
    index('kv_expires_at_idx').on(table.expiresAt),
    index('kv_key_pattern_idx').on(table.key),
  ],
)

export const oAuthAccessToken = pgTable(
  'OAuthAccessToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUsed: timestamp('lastUsed', { precision: 3 }),
    hash: text('hash').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    refreshTokenId: text('refreshTokenId').notNull(),
  },
  (table) => [
    index('OAuthAccessToken_expiresAt_idx').on(table.expiresAt),
    index('OAuthAccessToken_refreshTokenId_idx').on(table.refreshTokenId),
    foreignKey({
      name: 'OAuthAccessToken_refreshTokenId_fkey',
      columns: [table.refreshTokenId],
      foreignColumns: [userApiToken.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const invitationToken = pgTable('InvitationToken', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  workspaceId: text('workspaceId').notNull(),
  email: text('email').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  token: text('token').notNull(),
  usedBy: text('usedBy'),
  role: workspaceRoleEnum('role').notNull().default('owner'),
})

export const configurationObject = pgTable(
  'ConfigurationObject',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    deleted: boolean('deleted').default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    type: text('type').notNull(),
    workspaceId: text('workspaceId').notNull(),
    config: jsonb('config'),
  },
  (table) => [
    index('ConfigurationObject_updatedAt_idx').on(table.updatedAt),
    foreignKey({
      name: 'ConfigurationObject_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const configurationObjectLink = pgTable(
  'ConfigurationObjectLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    deleted: boolean('deleted').default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    type: text('type').default('push'),
    data: jsonb('data'),
    workspaceId: text('workspaceId').notNull(),
    fromId: text('fromId').notNull(),
    toId: text('toId').notNull(),
  },
  (table) => [
    index('ConfigurationObjectLink_updatedAt_idx').on(table.updatedAt),
    index('ConfigurationObjectLink_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'ConfigurationObjectLink_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConfigurationObjectLink_fromId_fkey',
      columns: [table.fromId],
      foreignColumns: [configurationObject.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConfigurationObjectLink_toId_fkey',
      columns: [table.toId],
      foreignColumns: [configurationObject.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const connectorPackage = pgTable('ConnectorPackage', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  packageId: text('packageId').notNull(),
  packageType: text('packageType').notNull().default('airbyte'),
  meta: jsonb('meta'),
  logoSvg: bytea('logoSvg'),
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

export const auditLog = pgTable(
  'AuditLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    timestamp: timestamp('timestamp', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    type: text('type').notNull(),
    severity: text('severity').default('info'),
    userId: text('userId'),
    workspaceId: text('workspaceId'),
    objectId: text('objectId'),
    changes: jsonb('changes'),
    authType: text('authType'),
    tokenId: text('tokenId'),
    ip: text('ip'),
    requestHeaders: jsonb('requestHeaders'),
  },
  (table) => [
    index('AuditLog_timestamp_idx').on(table.timestamp),
    index('AuditLog_type_idx').on(table.type),
    index('AuditLog_workspaceId_timestamp_idx').on(table.workspaceId, table.timestamp),
  ],
)

export const globalProps = pgTable('GlobalProps', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  name: text('name').notNull(),
  value: jsonb('value'),
})

export const sourceSpec = pgTable(
  'source_spec',
  {
    package: text('package').notNull(),
    version: text('version').notNull(),
    specs: jsonb('specs'),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    error: text('error'),
  },
  (table) => [primaryKey({ name: 'source_spec_pkey', columns: [table.package, table.version] })],
)

export const sourceCheck = pgTable('source_check', {
  package: text('package').notNull(),
  version: text('version').notNull(),
  key: text('key').primaryKey(),
  status: text('status').notNull(),
  description: text('description'),
  timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const sourceCatalog = pgTable(
  'source_catalog',
  {
    package: text('package').notNull(),
    version: text('version').notNull(),
    key: text('key').notNull(),
    catalog: jsonb('catalog'),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    status: text('status').notNull(),
    description: text('description'),
  },
  (table) => [
    primaryKey({ name: 'source_catalog_pkey', columns: [table.package, table.version, table.key] }),
  ],
)

export const reverseSyncControl = pgTable(
  'reverse_sync_control',
  {
    workspace_id: text('workspace_id').notNull(),
    sync_id: text('sync_id').notNull(),
    run_id: text('run_id').notNull(),
    run_order: bigint('run_order', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    detached: boolean('detached').notNull().default(false),
    revision: text('revision').notNull(),
    target_hash: text('target_hash').notNull(),
    phase: text('phase').notNull().default('new'),
    mode: reverseSyncModeEnum('mode').notNull(),
    extraction: reverseSyncExtractionEnum('extraction').notNull(),
    artifact_head: bytea('artifact_head'),
    base_sequence: bigint('base_sequence', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    next_sequence: bigint('next_sequence', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    checkpoint_sequence: bigint('checkpoint_sequence', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    finish_sequence: bigint('finish_sequence', { mode: 'bigint' }),
    store: bytea('store'),
    provider_state: bytea('provider_state'),
    finish_result: bytea('finish_result'),
    committed_generation: text('committed_generation'),
  },
  (table) => [
    primaryKey({
      name: 'reverse_sync_control_pkey',
      columns: [table.workspace_id, table.sync_id, table.run_id],
    }),
    index('reverse_sync_control_sync_id_idx').on(table.sync_id),
    uniqueIndex('reverse_sync_control_workspace_id_sync_id_run_order_key').on(
      table.workspace_id,
      table.sync_id,
      table.run_order,
    ),
  ],
)

export const reverseSyncTargetOwner = pgTable(
  'reverse_sync_target_owner',
  {
    target_hash: text('target_hash').primaryKey(),
    workspace_id: text('workspace_id').notNull(),
    sync_id: text('sync_id').notNull(),
  },
  (table) => [
    uniqueIndex('reverse_sync_target_owner_workspace_id_sync_id_key').on(
      table.workspace_id,
      table.sync_id,
    ),
  ],
)

export const sourceState = pgTable(
  'source_state',
  {
    sync_id: text('sync_id').notNull(),
    stream: text('stream').notNull(),
    state: jsonb('state').notNull(),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [primaryKey({ name: 'source_state_pkey', columns: [table.sync_id, table.stream] })],
)

export const sourceTask = pgTable(
  'source_task',
  {
    sync_id: text('sync_id').notNull(),
    task_id: text('task_id').primaryKey(),
    package: text('package').notNull(),
    version: text('version').notNull(),
    started_at: timestamp('started_at', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    started_by: jsonb('started_by'),
    status: text('status').notNull(),
    description: text('description'),
    metrics: jsonb('metrics'),
    error: text('error'),
  },
  (table) => [
    index('source_task_sync_id_idx').on(table.sync_id),
    index('source_task_started_at_idx').on(table.started_at),
    index('source_task_sync_id_started_at_idx').on(table.sync_id, table.started_at),
  ],
)

export const taskLog = pgTable(
  'task_log',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    level: text('level').notNull(),
    logger: text('logger').notNull(),
    message: text('message').notNull(),
    sync_id: text('sync_id').notNull(),
    task_id: text('task_id').notNull(),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('task_log_sync_id_idx').on(table.sync_id),
    index('task_log_task_id_idx').on(table.task_id),
    index('task_log_timestamp_idx').on(table.timestamp),
  ],
)

export const profileBuilder = pgTable(
  'ProfileBuilder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    deleted: boolean('deleted').default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    workspaceId: text('workspaceId').notNull(),
    intermediateStorageCredentials: jsonb('intermediateStorageCredentials').notNull(),
    destinationId: text('destinationId'),
    connectionOptions: jsonb('connectionOptions'),
    version: integer('version').notNull().default(0),
  },
  (table) => [
    foreignKey({
      name: 'ProfileBuilder_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ProfileBuilder_destinationId_fkey',
      columns: [table.destinationId],
      foreignColumns: [configurationObject.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const profileBuilderFunction = pgTable(
  'ProfileBuilderFunction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    profileBuilderId: text('profileBuilderId').notNull(),
    functionId: text('functionId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'ProfileBuilderFunction_profileBuilderId_fkey',
      columns: [table.profileBuilderId],
      foreignColumns: [profileBuilder.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ProfileBuilderFunction_functionId_fkey',
      columns: [table.functionId],
      foreignColumns: [configurationObject.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const profileBuilderState = pgTable(
  'ProfileBuilderState',
  {
    profileBuilderId: text('profileBuilderId').notNull(),
    profileBuilderVersion: integer('profileBuilderVersion').notNull(),
    instanceIndex: integer('instanceIndex').notNull(),
    totalInstances: integer('totalInstances').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    lastTimestamp: timestamp('lastTimestamp', { precision: 3 }),
    processedUsers: integer('processedUsers').notNull(),
    totalUsers: integer('totalUsers').notNull(),
    errorUsers: integer('errorUsers').notNull(),
    speed: doublePrecision('speed').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'ProfileBuilderState_pkey',
      columns: [
        table.profileBuilderId,
        table.profileBuilderVersion,
        table.totalInstances,
        table.instanceIndex,
      ],
    }),
  ],
)

export const profileBuilderState2 = pgTable(
  'ProfileBuilderState2',
  {
    profileBuilderId: text('profileBuilderId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    fullRebuildInfo: jsonb('fullRebuildInfo'),
    queuesInfo: jsonb('queuesInfo'),
    metrics: jsonb('metrics'),
  },
  (table) => [primaryKey({ name: 'ProfileBuilderState2_pkey', columns: [table.profileBuilderId] })],
)

export const statusChange = pgTable(
  'StatusChange',
  {
    id: bigserial('id', { mode: 'bigint' }).primaryKey(),
    workspaceId: text('workspaceId').notNull(),
    actorId: text('actorId').notNull(),
    type: text('type').notNull(),
    tableName: text('tableName').notNull(),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    status: text('status').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 }).notNull(),
    counts: integer('counts').notNull().default(0),
    description: text('description'),
    queueSize: integer('queueSize').notNull().default(0),
  },
  (table) => [
    index('StatusChange_actorId_type_tableName_idx').on(table.actorId, table.type, table.tableName),
    index('StatusChange_timestamp_idx').on(table.timestamp),
    index('StatusChange_startedAt_idx').on(table.startedAt),
  ],
)

export const notificationState = pgTable(
  'NotificationState',
  {
    workspaceId: text('workspaceId').notNull(),
    actorId: text('actorId').notNull(),
    type: text('type').notNull(),
    tableName: text('tableName').notNull(),
    channelId: text('channelId').notNull(),
    flappingSince: timestamp('flappingSince', { withTimezone: true, precision: 3 }),
    lastNotification: timestamp('lastNotification', { withTimezone: true, precision: 3 }).notNull(),
    statusChangeId: bigint('statusChangeId', { mode: 'bigint' }).notNull(),
    error: text('error'),
  },
  (table) => [
    primaryKey({
      name: 'NotificationState_pkey',
      columns: [table.channelId, table.actorId, table.type, table.tableName],
    }),
  ],
)

export const notification = pgTable(
  'Notification',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    workspaceId: text('workspaceId').notNull(),
    actorId: text('actorId').notNull(),
    type: text('type').notNull(),
    tableName: text('tableName').notNull(),
    channelId: text('channelId').notNull(),
    statusChangeId: bigint('statusChangeId', { mode: 'bigint' }).notNull(),
    timestamp: timestamp('timestamp', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    status: text('status').notNull(),
    error: text('error'),
  },
  (table) => [
    index('Notification_actorId_type_tableName_idx').on(table.actorId, table.type, table.tableName),
    index('Notification_timestamp_idx').on(table.timestamp),
  ],
)

export const oidcProvider = pgTable(
  'OidcProvider',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    name: text('name').notNull(),
    issuer: text('issuer').notNull(),
    clientId: text('clientId').notNull(),
    clientSecret: text('clientSecret').notNull(),
    authorizationEndpoint: text('authorizationEndpoint'),
    tokenEndpoint: text('tokenEndpoint'),
    userinfoEndpoint: text('userinfoEndpoint'),
    jwksUri: text('jwksUri'),
    introspectionEndpoint: text('introspectionEndpoint'),
    scopes: text('scopes').array().default(['openid', 'profile', 'email', 'offline_access']),
    audience: text('audience'),
    prompt: text('prompt'),
    usernameClaim: text('usernameClaim').notNull().default('email'),
    emailClaim: text('emailClaim').notNull().default('email'),
    nameClaim: text('nameClaim').notNull().default('name'),
    groupClaim: text('groupClaim').notNull().default('groups'),
    enabled: boolean('enabled').notNull().default(true),
    autoDiscovery: boolean('autoDiscovery').notNull().default(true),
  },
  (table) => [uniqueIndex('OidcProvider_issuer_clientId_key').on(table.issuer, table.clientId)],
)

export const oidcLoginGroup = pgTable(
  'OidcLoginGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    workspaceId: text('workspaceId').notNull(),
    oidcProviderId: text('oidcProviderId').notNull(),
    groupValue: text('groupValue'),
    allowAllUsers: boolean('allowAllUsers').notNull().default(false),
  },
  (table) => [
    index('OidcLoginGroup_workspaceId_idx').on(table.workspaceId),
    index('OidcLoginGroup_oidcProviderId_idx').on(table.oidcProviderId),
    uniqueIndex('OidcLoginGroup_workspaceId_oidcProviderId_key').on(
      table.workspaceId,
      table.oidcProviderId,
    ),
    foreignKey({
      name: 'OidcLoginGroup_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OidcLoginGroup_oidcProviderId_fkey',
      columns: [table.oidcProviderId],
      foreignColumns: [oidcProvider.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const reprocessingJobs = pgTable(
  'reprocessing_jobs',
  {
    id: varchar('id', { length: 64 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    config: jsonb('config').notNull(),
    status: varchar('status', { length: 32 }).notNull(),
    created_at: timestamp('created_at', { withTimezone: true, precision: 6 }).notNull(),
    started_at: timestamp('started_at', { withTimezone: true, precision: 6 }),
    completed_at: timestamp('completed_at', { withTimezone: true, precision: 6 }),
    k8s_job_name: varchar('k8s_job_name', { length: 255 }),
    total_files: integer('total_files').default(0),
    total_workers: integer('total_workers').default(0),
    error: text('error'),
  },
  (table) => [index('reprocessing_jobs_created_at_idx').on(table.created_at)],
)

export const reprocessingWorkers = pgTable(
  'reprocessing_workers',
  {
    job_id: varchar('job_id', { length: 64 }).notNull(),
    worker_index: integer('worker_index').notNull(),
    status: varchar('status', { length: 32 }).notNull(),
    started_at: timestamp('started_at', { withTimezone: true, precision: 6 }),
    updated_at: timestamp('updated_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completed_at: timestamp('completed_at', { withTimezone: true, precision: 6 }),
    current_file: text('current_file'),
    current_line: bigint('current_line', { mode: 'bigint' }).default(sql`0`),
    current_ts: text('current_ts'),
    assigned_files: integer('assigned_files').default(0),
    processed_files: integer('processed_files').default(0),
    total_lines: bigint('total_lines', { mode: 'bigint' }).default(sql`0`),
    success_count: bigint('success_count', { mode: 'bigint' }).default(sql`0`),
    error_count: bigint('error_count', { mode: 'bigint' }).default(sql`0`),
    skipped_count: bigint('skipped_count', { mode: 'bigint' }).default(sql`0`),
    total_bytes: bigint('total_bytes', { mode: 'bigint' }).default(sql`0`),
    processed_bytes: bigint('processed_bytes', { mode: 'bigint' }).default(sql`0`),
    error: text('error'),
  },
  (table) => [
    primaryKey({ name: 'reprocessing_workers_pkey', columns: [table.job_id, table.worker_index] }),
    index('reprocessing_workers_job_id_idx').on(table.job_id),
    index('reprocessing_workers_status_idx').on(table.job_id, table.status),
    foreignKey({
      name: 'reprocessing_workers_job_id_fkey',
      columns: [table.job_id],
      foreignColumns: [reprocessingJobs.id],
    })
      .onDelete('cascade')
      .onUpdate('no action'),
  ],
)

export const functionsServer = pgTable(
  'FunctionsServer',
  {
    workspaceId: text('workspaceId').notNull(),
    class: text('class').notNull(),
    deploymentId: text('deploymentId').notNull(),
    connections: text('connections').array(),
    emptyConnections: text('emptyConnections').array(),
    profileBuilders: text('profileBuilders').array(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 3 }),
  },
  (table) => [
    primaryKey({ name: 'FunctionsServer_pkey', columns: [table.workspaceId, table.class] }),
  ],
)

export const rateLimitCounter = pgTable(
  'RateLimitCounter',
  {
    bucketKey: text('bucketKey').notNull(),
    windowStart: timestamp('windowStart', { withTimezone: true, precision: 3 }).notNull(),
    count: integer('count').notNull().default(0),
    expiresAt: timestamp('expiresAt', { withTimezone: true, precision: 3 }).notNull(),
  },
  (table) => [
    primaryKey({ name: 'RateLimitCounter_pkey', columns: [table.bucketKey, table.windowStart] }),
    index('RateLimitCounter_expiresAt_idx').on(table.expiresAt),
  ],
)

export const relations = defineRelations(
  {
    emailLog,
    userPreferences,
    userProfile,
    userPassword,
    workspace,
    workspaceOptions,
    workspaceAccess,
    workspaceUserProperties,
    userApiToken,
    oAuthClient,
    kv,
    oAuthAccessToken,
    invitationToken,
    configurationObject,
    configurationObjectLink,
    connectorPackage,
    auditLog,
    globalProps,
    sourceSpec,
    sourceCheck,
    sourceCatalog,
    reverseSyncControl,
    reverseSyncTargetOwner,
    sourceState,
    sourceTask,
    taskLog,
    profileBuilder,
    profileBuilderFunction,
    profileBuilderState,
    profileBuilderState2,
    statusChange,
    notificationState,
    notification,
    oidcProvider,
    oidcLoginGroup,
    reprocessingJobs,
    reprocessingWorkers,
    functionsServer,
    rateLimitCounter,
  },
  (r) => ({
    userPreferences: {
      user: r.one.userProfile({
        from: r.userPreferences.userId,
        to: r.userProfile.id,
        optional: false,
      }),
      workspace: r.one.workspace({ from: r.userPreferences.workspaceId, to: r.workspace.id }),
    },
    userProfile: {
      tokens: r.many.userApiToken(),
      workspaceAccess: r.many.workspaceAccess(),
      userPreferences: r.many.userPreferences(),
      workspaceUserProperties: r.many.workspaceUserProperties(),
      password: r.one.userPassword({ from: r.userProfile.id, to: r.userPassword.userId }),
    },
    userPassword: {
      user: r.one.userProfile({
        from: r.userPassword.userId,
        to: r.userProfile.id,
        optional: false,
      }),
    },
    workspace: {
      workspaceAccess: r.many.workspaceAccess(),
      workspaceOptions: r.many.workspaceOptions(),
      configurationObject: r.many.configurationObject(),
      configurationObjectLink: r.many.configurationObjectLink(),
      preferences: r.many.userPreferences(),
      workspaceUserProperties: r.many.workspaceUserProperties(),
      profileBuilders: r.many.profileBuilder(),
      oidcLoginGroups: r.many.oidcLoginGroup(),
    },
    workspaceOptions: {
      workspace: r.one.workspace({
        from: r.workspaceOptions.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    workspaceAccess: {
      workspace: r.one.workspace({
        from: r.workspaceAccess.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      user: r.one.userProfile({
        from: r.workspaceAccess.userId,
        to: r.userProfile.id,
        optional: false,
      }),
    },
    workspaceUserProperties: {
      workspace: r.one.workspace({
        from: r.workspaceUserProperties.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      user: r.one.userProfile({
        from: r.workspaceUserProperties.userId,
        to: r.userProfile.id,
        optional: false,
      }),
    },
    userApiToken: {
      user: r.one.userProfile({
        from: r.userApiToken.userId,
        to: r.userProfile.id,
        optional: false,
      }),
      oauthClient: r.one.oAuthClient({ from: r.userApiToken.oauthClientId, to: r.oAuthClient.id }),
      accessTokens: r.many.oAuthAccessToken(),
    },
    oAuthClient: { tokens: r.many.userApiToken() },
    oAuthAccessToken: {
      refreshToken: r.one.userApiToken({
        from: r.oAuthAccessToken.refreshTokenId,
        to: r.userApiToken.id,
        optional: false,
      }),
    },
    configurationObject: {
      workspace: r.one.workspace({
        from: r.configurationObject.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      fromLinks: r.many.configurationObjectLink({ alias: 'from' }),
      toLinks: r.many.configurationObjectLink({ alias: 'to' }),
      ProfileBuilder: r.many.profileBuilder(),
      ProfileBuilderFunction: r.many.profileBuilderFunction(),
    },
    configurationObjectLink: {
      workspace: r.one.workspace({
        from: r.configurationObjectLink.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      from: r.one.configurationObject({
        from: r.configurationObjectLink.fromId,
        to: r.configurationObject.id,
        optional: false,
        alias: 'to',
      }),
      to: r.one.configurationObject({
        from: r.configurationObjectLink.toId,
        to: r.configurationObject.id,
        optional: false,
        alias: 'from',
      }),
    },
    profileBuilder: {
      workspace: r.one.workspace({
        from: r.profileBuilder.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      destination: r.one.configurationObject({
        from: r.profileBuilder.destinationId,
        to: r.configurationObject.id,
      }),
      functions: r.many.profileBuilderFunction(),
    },
    profileBuilderFunction: {
      profileBuilder: r.one.profileBuilder({
        from: r.profileBuilderFunction.profileBuilderId,
        to: r.profileBuilder.id,
        optional: false,
      }),
      function: r.one.configurationObject({
        from: r.profileBuilderFunction.functionId,
        to: r.configurationObject.id,
        optional: false,
      }),
    },
    oidcProvider: { oidcLoginGroups: r.many.oidcLoginGroup() },
    oidcLoginGroup: {
      workspace: r.one.workspace({
        from: r.oidcLoginGroup.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      oidcProvider: r.one.oidcProvider({
        from: r.oidcLoginGroup.oidcProviderId,
        to: r.oidcProvider.id,
        optional: false,
      }),
    },
    reprocessingJobs: { reprocessing_workers: r.many.reprocessingWorkers() },
    reprocessingWorkers: {
      job: r.one.reprocessingJobs({
        from: r.reprocessingWorkers.job_id,
        to: r.reprocessingJobs.id,
        optional: false,
      }),
    },
  }),
)
