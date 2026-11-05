import {
  boolean,
  doublePrecision,
  foreignKey,
  index,
  integer,
  json,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  uniqueIndex,
  uuid,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'

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

export const projectTypeEnum = pgEnum('ProjectType', ['website', 'app', 'backend'])

export const projectScopeEnum = pgEnum('ProjectScope', ['all', 'selected'])

export const accessLevelEnum = pgEnum('AccessLevel', ['read', 'write', 'admin'])

export const clientTypeEnum = pgEnum('ClientType', ['read', 'write', 'root'])

export const intervalEnum = pgEnum('Interval', ['hour', 'day', 'month', 'minute', 'week'])

export const chartTypeEnum = pgEnum('ChartType', [
  'linear',
  'bar',
  'histogram',
  'pie',
  'metric',
  'area',
  'map',
  'funnel',
  'retention',
  'conversion',
  'sankey',
])

export const metricEnum = pgEnum('Metric', ['sum', 'average', 'min', 'max', 'count'])

export const importStatusEnum = pgEnum('ImportStatus', [
  'pending',
  'processing',
  'completed',
  'failed',
])

export const insightStateEnum = pgEnum('InsightState', ['active', 'suppressed', 'closed'])

export const codeMigrations = pgTable(
  '__code_migrations',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
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
  },
  (table) => [uniqueIndex('__code_migrations_name_key').on(table.name)],
)

export const conversations = pgTable(
  'conversations',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    title: text('title'),
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
    index('conversations_projectId_updatedAt_idx').on(table.projectId, table.updatedAt),
    index('conversations_userId_updatedAt_idx').on(table.userId, table.updatedAt),
    foreignKey({
      name: 'conversations_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'conversations_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'conversations_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatMessages = pgTable(
  'chat_messages',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    conversationId: text('conversationId').notNull(),
    role: text('role').notNull(),
    parts: json('parts').notNull(),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('chat_messages_conversationId_createdAt_idx').on(table.conversationId, table.createdAt),
    foreignKey({
      name: 'chat_messages_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organizations = pgTable(
  'organizations',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    slug: text('slug'),
    createdByUserId: text('createdByUserId'),
    timezone: text('timezone'),
    onboarding: text('onboarding').default('completed'),
    attribution: jsonb('attribution'),
    subscriptionId: text('subscriptionId'),
    subscriptionCustomerId: text('subscriptionCustomerId'),
    subscriptionPriceId: text('subscriptionPriceId'),
    subscriptionProductId: text('subscriptionProductId'),
    subscriptionStatus: text('subscriptionStatus'),
    subscriptionStartsAt: timestamp('subscriptionStartsAt', { precision: 3 }),
    subscriptionEndsAt: timestamp('subscriptionEndsAt', { precision: 3 }),
    subscriptionCanceledAt: timestamp('subscriptionCanceledAt', { precision: 3 }),
    subscriptionCreatedByUserId: text('subscriptionCreatedByUserId'),
    subscriptionPeriodEventsCount: integer('subscriptionPeriodEventsCount').notNull().default(0),
    subscriptionPeriodEventsCountExceededAt: timestamp('subscriptionPeriodEventsCountExceededAt', {
      precision: 3,
    }),
    subscriptionPeriodEventsLimit: integer('subscriptionPeriodEventsLimit').notNull().default(0),
    subscriptionInterval: text('subscriptionInterval'),
    subscriptionCancelReason: text('subscriptionCancelReason'),
    subscriptionCancelComment: text('subscriptionCancelComment'),
    subscriptionSaveDiscountAppliedAt: timestamp('subscriptionSaveDiscountAppliedAt', {
      precision: 3,
    }),
    subscriptionPauseAtPeriodEnd: boolean('subscriptionPauseAtPeriodEnd').notNull().default(false),
    subscriptionResumesAt: timestamp('subscriptionResumesAt', { precision: 3 }),
    subscriptionFirstStartedAt: timestamp('subscriptionFirstStartedAt', { precision: 3 }),
    usageWarningSentAt: timestamp('usageWarningSentAt', { precision: 3 }),
    usageExceededSentAt: timestamp('usageExceededSentAt', { precision: 3 }),
    subscriptionDiscount: jsonb('subscriptionDiscount'),
    windDownStartedAt: timestamp('windDownStartedAt', { precision: 3 }),
    windDownStep: text('windDownStep'),
    deleteAt: timestamp('deleteAt', { precision: 3 }),
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
    uniqueIndex('organizations_slug_key').on(table.slug),
    index('organizations_windDownStartedAt_idx').on(table.windDownStartedAt),
    foreignKey({
      name: 'organizations_createdByUserId_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'organizations_subscriptionCreatedByUserId_fkey',
      columns: [table.subscriptionCreatedByUserId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const users = pgTable(
  'users',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    email: text('email').notNull(),
    firstName: text('firstName'),
    lastName: text('lastName'),
    name: text('name'),
    emailVerified: boolean('emailVerified').notNull().default(false),
    image: text('image'),
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
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [uniqueIndex('users_email_key').on(table.email)],
)

export const userTotp = pgTable(
  'user_totp',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    userId: text('userId').notNull(),
    secret: text('secret').notNull(),
    recoveryCodes: text('recoveryCodes').array(),
    enabledAt: timestamp('enabledAt', { precision: 3 }),
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
    uniqueIndex('user_totp_userId_key').on(table.userId),
    foreignKey({
      name: 'user_totp_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const twoFactor = pgTable(
  'two_factor',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
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
    uniqueIndex('two_factor_userId_key').on(table.userId),
    foreignKey({
      name: 'two_factor_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const twoFactorChallenges = pgTable(
  'two_factor_challenges',
  {
    id: text('id').primaryKey(),
    userId: text('userId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('two_factor_challenges_userId_idx').on(table.userId),
    foreignKey({
      name: 'two_factor_challenges_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accounts = pgTable(
  'accounts',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    userId: text('userId').notNull(),
    email: text('email'),
    provider: text('provider').notNull(),
    providerId: text('providerId'),
    accountId: text('accountId'),
    idToken: text('idToken'),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    accessTokenExpiresAt: timestamp('accessTokenExpiresAt', { precision: 3 }),
    refreshTokenExpiresAt: timestamp('refreshTokenExpiresAt', { precision: 3 }),
    scope: text('scope'),
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
    foreignKey({
      name: 'accounts_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sessions = pgTable(
  'sessions',
  {
    id: text('id').primaryKey(),
    token: text('token'),
    userId: text('userId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
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
    uniqueIndex('sessions_token_key').on(table.token),
    foreignKey({
      name: 'sessions_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verifications = pgTable(
  'verifications',
  {
    id: text('id').primaryKey(),
    identifier: text('identifier').notNull(),
    value: text('value').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
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
  (table) => [index('verifications_identifier_idx').on(table.identifier)],
)

export const members = pgTable(
  'members',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    role: text('role').notNull(),
    email: text('email').notNull(),
    userId: text('userId'),
    invitedById: text('invitedById'),
    organizationId: text('organizationId').notNull(),
    projectScope: projectScopeEnum('projectScope').notNull().default('all'),
    meta: jsonb('meta'),
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
    uniqueIndex('members_organizationId_userId_key').on(table.organizationId, table.userId),
    foreignKey({ name: 'members_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'members_invitedById_fkey',
      columns: [table.invitedById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'members_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const memberProjects = pgTable(
  'member_projects',
  {
    memberId: uuid('memberId').notNull(),
    projectId: text('projectId').notNull(),
    level: accessLevelEnum('level'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'member_projects_pkey', columns: [table.memberId, table.projectId] }),
    index('member_projects_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'member_projects_memberId_fkey',
      columns: [table.memberId],
      foreignColumns: [members.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'member_projects_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const invites = pgTable(
  'invites',
  {
    id: text('id').primaryKey(),
    email: text('email').notNull(),
    createdById: text('createdById').notNull(),
    organizationId: text('organizationId').notNull(),
    projectAccess: jsonb('projectAccess').notNull().default([]),
    projectScope: projectScopeEnum('projectScope').notNull().default('all'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    role: text('role').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'invites_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'invites_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const projects = pgTable(
  'projects',
  {
    id: text('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    organizationId: text('organizationId').notNull(),
    eventsCount: integer('eventsCount').notNull().default(0),
    types: projectTypeEnum('types').array().default([]),
    domain: text('domain'),
    cors: text('cors').array().default([]),
    crossDomain: boolean('crossDomain').notNull().default(false),
    allowUnsafeRevenueTracking: boolean('allowUnsafeRevenueTracking').notNull().default(false),
    aiStoreContent: boolean('aiStoreContent').notNull().default(true),
    filters: jsonb('filters').notNull().default([]),
    firstEventAt: timestamp('firstEventAt', { precision: 3 }),
    noDataNotifiedAt: timestamp('noDataNotifiedAt', { precision: 3 }),
    dataStoppedNotifiedAt: timestamp('dataStoppedNotifiedAt', { precision: 3 }),
    deleteAt: timestamp('deleteAt', { precision: 3 }),
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
    foreignKey({
      name: 'projects_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cohorts = pgTable(
  'cohorts',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    description: text('description'),
    projectId: text('projectId').notNull(),
    definition: jsonb('definition').notNull().default({}),
    isStatic: boolean('isStatic').notNull().default(false),
    profileCount: integer('profileCount').notNull().default(0),
    lastComputedAt: timestamp('lastComputedAt', { precision: 3 }),
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
    index('cohorts_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'cohorts_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const salts = pgTable('salts', {
  salt: text('salt').primaryKey(),
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

export const clients = pgTable(
  'clients',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    secret: text('secret'),
    type: clientTypeEnum('type').notNull().default('write'),
    projectId: text('projectId'),
    organizationId: text('organizationId').notNull(),
    ignoreCorsAndSecret: boolean('ignoreCorsAndSecret').notNull().default(false),
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
    foreignKey({
      name: 'clients_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'clients_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
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
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
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
    foreignKey({
      name: 'dashboards_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'dashboards_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const reports = pgTable(
  'reports',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    interval: intervalEnum('interval').notNull(),
    range: text('range').notNull().default('30d'),
    chartType: chartTypeEnum('chartType').notNull(),
    lineType: text('lineType').notNull().default('monotone'),
    breakdowns: jsonb('breakdowns').notNull(),
    events: jsonb('events').notNull(),
    globalFilters: jsonb('globalFilters').notNull().default([]),
    formula: text('formula'),
    unit: text('unit'),
    metric: metricEnum('metric').notNull().default('sum'),
    projectId: text('projectId').notNull(),
    previous: boolean('previous').notNull().default(false),
    criteria: text('criteria'),
    funnelGroup: text('funnelGroup'),
    funnelWindow: doublePrecision('funnelWindow'),
    options: jsonb('options'),
    display: jsonb('display'),
    visibleSeries: text('visibleSeries').array(),
    startDate: text('startDate'),
    endDate: text('endDate'),
    dashboardId: text('dashboardId').notNull(),
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
    foreignKey({
      name: 'reports_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'reports_dashboardId_fkey',
      columns: [table.dashboardId],
      foreignColumns: [dashboards.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const reportLayouts = pgTable(
  'report_layouts',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    reportId: uuid('reportId').notNull(),
    gridColumns: integer('gridColumns').notNull().default(12),
    x: integer('x').notNull().default(0),
    y: integer('y').notNull().default(0),
    w: integer('w').notNull().default(8),
    h: integer('h').notNull().default(6),
    minW: integer('minW').default(4),
    minH: integer('minH').default(4),
    maxW: integer('maxW'),
    maxH: integer('maxH'),
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
    uniqueIndex('report_layouts_reportId_key').on(table.reportId),
    foreignKey({
      name: 'report_layouts_reportId_fkey',
      columns: [table.reportId],
      foreignColumns: [reports.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const shares = pgTable(
  'shares',
  {
    id: text('id').notNull(),
    projectId: text('projectId').notNull(),
    organizationId: text('organizationId').notNull(),
    public: boolean('public').notNull().default(false),
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
    uniqueIndex('shares_id_key').on(table.id),
    uniqueIndex('shares_projectId_key').on(table.projectId),
    foreignKey({
      name: 'shares_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'shares_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const shareDashboards = pgTable(
  'share_dashboards',
  {
    id: text('id').notNull(),
    dashboardId: text('dashboardId').notNull(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId').notNull(),
    public: boolean('public').notNull().default(false),
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
    uniqueIndex('share_dashboards_id_key').on(table.id),
    uniqueIndex('share_dashboards_dashboardId_key').on(table.dashboardId),
    foreignKey({
      name: 'share_dashboards_dashboardId_fkey',
      columns: [table.dashboardId],
      foreignColumns: [dashboards.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'share_dashboards_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'share_dashboards_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const shareReports = pgTable(
  'share_reports',
  {
    id: text('id').notNull(),
    reportId: uuid('reportId').notNull(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId').notNull(),
    public: boolean('public').notNull().default(false),
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
    uniqueIndex('share_reports_id_key').on(table.id),
    uniqueIndex('share_reports_reportId_key').on(table.reportId),
    foreignKey({
      name: 'share_reports_reportId_fkey',
      columns: [table.reportId],
      foreignColumns: [reports.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'share_reports_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'share_reports_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const shareWidgets = pgTable(
  'share_widgets',
  {
    id: text('id').notNull(),
    projectId: text('projectId').notNull(),
    organizationId: text('organizationId').notNull(),
    public: boolean('public').notNull().default(true),
    options: jsonb('options').notNull(),
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
    uniqueIndex('share_widgets_id_key').on(table.id),
    foreignKey({
      name: 'share_widgets_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'share_widgets_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const eventMeta = pgTable(
  'event_meta',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    conversion: boolean('conversion'),
    color: text('color'),
    icon: text('icon'),
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
    uniqueIndex('event_meta_name_projectId_key').on(table.name, table.projectId),
    foreignKey({
      name: 'event_meta_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const customEvents = pgTable(
  'custom_events',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    items: jsonb('items').notNull(),
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
    uniqueIndex('custom_events_projectId_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'custom_events_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const references = pgTable(
  'references',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    title: text('title').notNull(),
    description: text('description'),
    date: timestamp('date', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
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
    foreignKey({
      name: 'references_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const notificationRules = pgTable(
  'notification_rules',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    projectId: text('projectId').notNull(),
    sendToApp: boolean('sendToApp').notNull().default(false),
    sendToEmail: boolean('sendToEmail').notNull().default(false),
    config: jsonb('config').notNull(),
    template: text('template'),
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
    foreignKey({
      name: 'notification_rules_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const notifications = pgTable(
  'notifications',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    title: text('title').notNull(),
    message: text('message').notNull(),
    isReadAt: timestamp('isReadAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    sendToApp: boolean('sendToApp').notNull().default(false),
    sendToEmail: boolean('sendToEmail').notNull().default(false),
    integrationId: uuid('integrationId'),
    notificationRuleId: uuid('notificationRuleId'),
    payload: jsonb('payload'),
  },
  (table) => [
    foreignKey({
      name: 'notifications_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'notifications_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integrations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'notifications_notificationRuleId_fkey',
      columns: [table.notificationRuleId],
      foreignColumns: [notificationRules.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const integrations = pgTable(
  'integrations',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    name: text('name').notNull(),
    config: jsonb('config').notNull(),
    organizationId: text('organizationId').notNull(),
    projectId: text('projectId'),
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
    index('integrations_organizationId_idx').on(table.organizationId),
    index('integrations_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'integrations_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'integrations_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const exportWatermarks = pgTable(
  'export_watermarks',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    integrationId: uuid('integrationId').notNull(),
    lastInsertedAt: timestamp('lastInsertedAt', { precision: 3 }).notNull(),
    lastEventId: text('lastEventId').notNull().default(''),
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
    index('export_watermarks_integrationId_idx').on(table.integrationId),
    uniqueIndex('export_watermarks_projectId_integrationId_key').on(
      table.projectId,
      table.integrationId,
    ),
    foreignKey({
      name: 'export_watermarks_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'export_watermarks_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integrations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const resetPassword = pgTable(
  'reset_password',
  {
    id: text('id').primaryKey(),
    accountId: text('accountId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
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
    foreignKey({
      name: 'reset_password_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const imports = pgTable(
  'imports',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    jobId: text('jobId'),
    status: importStatusEnum('status').notNull(),
    statusMessage: text('statusMessage'),
    errorMessage: text('errorMessage'),
    config: jsonb('config').notNull(),
    totalEvents: integer('totalEvents').notNull().default(0),
    processedEvents: integer('processedEvents').notNull().default(0),
    currentStep: text('currentStep'),
    currentBatch: text('currentBatch'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'imports_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const projectInsights = pgTable(
  'project_insights',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    moduleKey: text('moduleKey').notNull(),
    dimensionKey: text('dimensionKey').notNull(),
    windowKind: text('windowKind').notNull(),
    state: insightStateEnum('state').notNull().default('active'),
    title: text('title').notNull(),
    summary: text('summary'),
    displayName: text('displayName').notNull().default(''),
    payload: jsonb('payload').notNull().default({}),
    direction: text('direction'),
    impactScore: doublePrecision('impactScore').notNull().default(0),
    severityBand: text('severityBand'),
    relevanceScore: doublePrecision('relevanceScore'),
    aiSummary: text('aiSummary'),
    aiCategory: text('aiCategory'),
    emailWorthy: boolean('emailWorthy').notNull().default(false),
    referenceWorthy: boolean('referenceWorthy').notNull().default(false),
    enrichedAt: timestamp('enrichedAt', { precision: 3 }),
    enrichVersion: integer('enrichVersion'),
    version: integer('version').notNull().default(1),
    threadId: uuid('threadId')
      .notNull()
      .default(sql`gen_random_uuid()`),
    windowStart: timestamp('windowStart', { precision: 3 }),
    windowEnd: timestamp('windowEnd', { precision: 3 }),
    firstDetectedAt: timestamp('firstDetectedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUpdatedAt: timestamp('lastUpdatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    lastSeenAt: timestamp('lastSeenAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('project_insights_projectId_impactScore_idx').on(table.projectId, table.impactScore),
    index('project_insights_projectId_moduleKey_windowKind_state_idx').on(
      table.projectId,
      table.moduleKey,
      table.windowKind,
      table.state,
    ),
    index('project_insights_projectId_state_relevanceScore_idx').on(
      table.projectId,
      table.state,
      table.relevanceScore,
    ),
    uniqueIndex('project_insights_projectId_moduleKey_dimensionKey_windowKin_key').on(
      table.projectId,
      table.moduleKey,
      table.dimensionKey,
      table.windowKind,
      table.state,
    ),
  ],
)

export const insightEvents = pgTable(
  'insight_events',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    insightId: uuid('insightId').notNull(),
    eventKind: text('eventKind').notNull(),
    changeFrom: jsonb('changeFrom'),
    changeTo: jsonb('changeTo'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('insight_events_insightId_createdAt_idx').on(table.insightId, table.createdAt),
    foreignKey({
      name: 'insight_events_insightId_fkey',
      columns: [table.insightId],
      foreignColumns: [projectInsights.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const gscConnections = pgTable(
  'gsc_connections',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    projectId: text('projectId').notNull(),
    siteUrl: text('siteUrl').notNull().default(''),
    accessToken: text('accessToken').notNull(),
    refreshToken: text('refreshToken').notNull(),
    accessTokenExpiresAt: timestamp('accessTokenExpiresAt', { precision: 3 }),
    lastSyncedAt: timestamp('lastSyncedAt', { precision: 3 }),
    lastSyncStatus: text('lastSyncStatus'),
    lastSyncError: text('lastSyncError'),
    backfillStatus: text('backfillStatus'),
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
    uniqueIndex('gsc_connections_projectId_key').on(table.projectId),
    foreignKey({
      name: 'gsc_connections_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailUnsubscribes = pgTable(
  'email_unsubscribes',
  {
    id: uuid('id')
      .primaryKey()
      .default(sql`gen_random_uuid()`),
    email: text('email').notNull(),
    category: text('category').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('email_unsubscribes_email_category_key').on(table.email, table.category)],
)

export const integrationToNotificationRule = pgTable(
  '_IntegrationToNotificationRule',
  { A: uuid('A').notNull(), B: uuid('B').notNull() },
  (table) => [
    primaryKey({ name: '_IntegrationToNotificationRule_AB_pkey', columns: [table.A, table.B] }),
    index('_IntegrationToNotificationRule_B_index').on(table.B),
    foreignKey({
      name: '_IntegrationToNotificationRule_A_fkey',
      columns: [table.A],
      foreignColumns: [integrations.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: '_IntegrationToNotificationRule_B_fkey',
      columns: [table.B],
      foreignColumns: [notificationRules.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    codeMigrations,
    conversations,
    chatMessages,
    organizations,
    users,
    userTotp,
    twoFactor,
    twoFactorChallenges,
    accounts,
    sessions,
    verifications,
    members,
    memberProjects,
    invites,
    projects,
    cohorts,
    salts,
    clients,
    dashboards,
    reports,
    reportLayouts,
    shares,
    shareDashboards,
    shareReports,
    shareWidgets,
    eventMeta,
    customEvents,
    references,
    notificationRules,
    notifications,
    integrations,
    exportWatermarks,
    resetPassword,
    imports,
    projectInsights,
    insightEvents,
    gscConnections,
    emailUnsubscribes,
    integrationToNotificationRule,
  },
  (r) => ({
    conversations: {
      project: r.one.projects({
        from: r.conversations.projectId,
        to: r.projects.id,
        optional: false,
      }),
      organization: r.one.organizations({
        from: r.conversations.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      user: r.one.users({ from: r.conversations.userId, to: r.users.id, optional: false }),
      messages: r.many.chatMessages(),
    },
    chatMessages: {
      conversation: r.one.conversations({
        from: r.chatMessages.conversationId,
        to: r.conversations.id,
        optional: false,
      }),
    },
    organizations: {
      projects: r.many.projects(),
      members: r.many.members(),
      createdBy: r.one.users({
        from: r.organizations.createdByUserId,
        to: r.users.id,
        alias: 'organizationCreatedBy',
      }),
      Client: r.many.clients(),
      Dashboard: r.many.dashboards(),
      ShareOverview: r.many.shares(),
      ShareDashboard: r.many.shareDashboards(),
      ShareReport: r.many.shareReports(),
      ShareWidget: r.many.shareWidgets(),
      integrations: r.many.integrations(),
      invites: r.many.invites(),
      conversations: r.many.conversations(),
      subscriptionCreatedBy: r.one.users({
        from: r.organizations.subscriptionCreatedByUserId,
        to: r.users.id,
        alias: 'subscriptionCreatedBy',
      }),
    },
    users: {
      twoFactor: r.one.twoFactor({ from: r.users.id, to: r.twoFactor.userId }),
      createdOrganizations: r.many.organizations({ alias: 'organizationCreatedBy' }),
      subscriptions: r.many.organizations({ alias: 'subscriptionCreatedBy' }),
      membership: r.many.members({ alias: 'MemberToUser' }),
      sentInvites: r.many.members({ alias: 'invitedBy' }),
      sessions: r.many.sessions(),
      accounts: r.many.accounts(),
      invites: r.many.invites(),
      conversations: r.many.conversations(),
      totp: r.one.userTotp({ from: r.users.id, to: r.userTotp.userId }),
      twoFactorChallenges: r.many.twoFactorChallenges(),
    },
    userTotp: { user: r.one.users({ from: r.userTotp.userId, to: r.users.id, optional: false }) },
    twoFactor: { user: r.one.users({ from: r.twoFactor.userId, to: r.users.id, optional: false }) },
    twoFactorChallenges: {
      user: r.one.users({ from: r.twoFactorChallenges.userId, to: r.users.id, optional: false }),
    },
    accounts: {
      user: r.one.users({ from: r.accounts.userId, to: r.users.id, optional: false }),
      resetPasswords: r.many.resetPassword(),
    },
    sessions: { user: r.one.users({ from: r.sessions.userId, to: r.users.id, optional: false }) },
    members: {
      user: r.one.users({ from: r.members.userId, to: r.users.id, alias: 'MemberToUser' }),
      invitedBy: r.one.users({ from: r.members.invitedById, to: r.users.id, alias: 'invitedBy' }),
      organization: r.one.organizations({
        from: r.members.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      projects: r.many.memberProjects(),
    },
    memberProjects: {
      member: r.one.members({ from: r.memberProjects.memberId, to: r.members.id, optional: false }),
      project: r.one.projects({
        from: r.memberProjects.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    invites: {
      createdBy: r.one.users({ from: r.invites.createdById, to: r.users.id, optional: false }),
      organization: r.one.organizations({
        from: r.invites.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    projects: {
      organization: r.one.organizations({
        from: r.projects.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      clients: r.many.clients(),
      reports: r.many.reports(),
      dashboards: r.many.dashboards(),
      share: r.one.shares({ from: r.projects.id, to: r.shares.projectId }),
      shareDashboards: r.many.shareDashboards(),
      shareReports: r.many.shareReports(),
      shareWidgets: r.many.shareWidgets(),
      meta: r.many.eventMeta(),
      customEvents: r.many.customEvents(),
      references: r.many.references(),
      memberProjects: r.many.memberProjects(),
      notificationRules: r.many.notificationRules(),
      notifications: r.many.notifications(),
      imports: r.many.imports(),
      gscConnection: r.one.gscConnections({ from: r.projects.id, to: r.gscConnections.projectId }),
      cohorts: r.many.cohorts(),
      exportWatermarks: r.many.exportWatermarks(),
      integrations: r.many.integrations(),
      conversations: r.many.conversations(),
    },
    cohorts: {
      project: r.one.projects({ from: r.cohorts.projectId, to: r.projects.id, optional: false }),
    },
    clients: {
      project: r.one.projects({ from: r.clients.projectId, to: r.projects.id }),
      organization: r.one.organizations({
        from: r.clients.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    dashboards: {
      organization: r.one.organizations({
        from: r.dashboards.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      project: r.one.projects({ from: r.dashboards.projectId, to: r.projects.id, optional: false }),
      reports: r.many.reports(),
      share: r.one.shareDashboards({ from: r.dashboards.id, to: r.shareDashboards.dashboardId }),
    },
    reports: {
      project: r.one.projects({ from: r.reports.projectId, to: r.projects.id, optional: false }),
      dashboard: r.one.dashboards({
        from: r.reports.dashboardId,
        to: r.dashboards.id,
        optional: false,
      }),
      layout: r.one.reportLayouts({ from: r.reports.id, to: r.reportLayouts.reportId }),
      share: r.one.shareReports({ from: r.reports.id, to: r.shareReports.reportId }),
    },
    reportLayouts: {
      report: r.one.reports({ from: r.reportLayouts.reportId, to: r.reports.id, optional: false }),
    },
    shares: {
      project: r.one.projects({ from: r.shares.projectId, to: r.projects.id, optional: false }),
      organization: r.one.organizations({
        from: r.shares.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    shareDashboards: {
      dashboard: r.one.dashboards({
        from: r.shareDashboards.dashboardId,
        to: r.dashboards.id,
        optional: false,
      }),
      organization: r.one.organizations({
        from: r.shareDashboards.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      project: r.one.projects({
        from: r.shareDashboards.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    shareReports: {
      report: r.one.reports({ from: r.shareReports.reportId, to: r.reports.id, optional: false }),
      organization: r.one.organizations({
        from: r.shareReports.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      project: r.one.projects({
        from: r.shareReports.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    shareWidgets: {
      project: r.one.projects({
        from: r.shareWidgets.projectId,
        to: r.projects.id,
        optional: false,
      }),
      organization: r.one.organizations({
        from: r.shareWidgets.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
    },
    eventMeta: {
      project: r.one.projects({ from: r.eventMeta.projectId, to: r.projects.id, optional: false }),
    },
    customEvents: {
      project: r.one.projects({
        from: r.customEvents.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    references: {
      project: r.one.projects({ from: r.references.projectId, to: r.projects.id, optional: false }),
    },
    notificationRules: {
      project: r.one.projects({
        from: r.notificationRules.projectId,
        to: r.projects.id,
        optional: false,
      }),
      integrations: r.many.integrations({
        from: r.notificationRules.id.through(r.integrationToNotificationRule.B),
        to: r.integrations.id.through(r.integrationToNotificationRule.A),
      }),
      notifications: r.many.notifications(),
    },
    notifications: {
      project: r.one.projects({
        from: r.notifications.projectId,
        to: r.projects.id,
        optional: false,
      }),
      integration: r.one.integrations({
        from: r.notifications.integrationId,
        to: r.integrations.id,
      }),
      notificationRule: r.one.notificationRules({
        from: r.notifications.notificationRuleId,
        to: r.notificationRules.id,
      }),
    },
    integrations: {
      organization: r.one.organizations({
        from: r.integrations.organizationId,
        to: r.organizations.id,
        optional: false,
      }),
      project: r.one.projects({ from: r.integrations.projectId, to: r.projects.id }),
      notificationRules: r.many.notificationRules({
        from: r.integrations.id.through(r.integrationToNotificationRule.A),
        to: r.notificationRules.id.through(r.integrationToNotificationRule.B),
      }),
      notifications: r.many.notifications(),
      exportWatermarks: r.many.exportWatermarks(),
    },
    exportWatermarks: {
      project: r.one.projects({
        from: r.exportWatermarks.projectId,
        to: r.projects.id,
        optional: false,
      }),
      integration: r.one.integrations({
        from: r.exportWatermarks.integrationId,
        to: r.integrations.id,
        optional: false,
      }),
    },
    resetPassword: {
      account: r.one.accounts({
        from: r.resetPassword.accountId,
        to: r.accounts.id,
        optional: false,
      }),
    },
    imports: {
      project: r.one.projects({ from: r.imports.projectId, to: r.projects.id, optional: false }),
    },
    projectInsights: { events: r.many.insightEvents() },
    insightEvents: {
      insight: r.one.projectInsights({
        from: r.insightEvents.insightId,
        to: r.projectInsights.id,
        optional: false,
      }),
    },
    gscConnections: {
      project: r.one.projects({
        from: r.gscConnections.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    integrationToNotificationRule: {
      integration: r.one.integrations({
        from: r.integrationToNotificationRule.A,
        to: r.integrations.id,
        optional: false,
      }),
      notificationRule: r.one.notificationRules({
        from: r.integrationToNotificationRule.B,
        to: r.notificationRules.id,
        optional: false,
      }),
    },
  }),
)
