import {
  boolean,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  serial,
  text,
  timestamp,
  uniqueIndex,
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

export const chatProviderEnum = pgEnum('ChatProvider', ['WHATSAPP', 'DIALOG360'])

export const workspaceRoleEnum = pgEnum('WorkspaceRole', ['ADMIN', 'MEMBER', 'GUEST'])

export const graphNavigationEnum = pgEnum('GraphNavigation', ['MOUSE', 'TRACKPAD'])

export const planEnum = pgEnum('Plan', [
  'FREE',
  'STARTER',
  'PRO',
  'LIFETIME',
  'OFFERED',
  'CUSTOM',
  'UNLIMITED',
  'ENTERPRISE',
])

export const collaborationTypeEnum = pgEnum('CollaborationType', ['READ', 'WRITE', 'FULL_ACCESS'])

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
    oauth_token_secret: text('oauth_token_secret'),
    oauth_token: text('oauth_token'),
    refresh_token_expires_in: integer('refresh_token_expires_in'),
  },
  (table) => [
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
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
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
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
    lastActivityAt: timestamp('lastActivityAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    name: text('name'),
    email: text('email'),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
    image: text('image'),
    company: text('company'),
    onboardingCategories: jsonb('onboardingCategories').notNull(),
    referral: text('referral'),
    graphNavigation: graphNavigationEnum('graphNavigation'),
    preferredAppAppearance: text('preferredAppAppearance'),
    displayedInAppNotifications: jsonb('displayedInAppNotifications'),
    groupTitlesAutoGeneration: jsonb('groupTitlesAutoGeneration'),
    preferredLanguage: text('preferredLanguage'),
    termsAcceptedAt: timestamp('termsAcceptedAt', { precision: 3 }),
  },
  (table) => [uniqueIndex('User_email_key').on(table.email)],
)

export const apiToken = pgTable(
  'ApiToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    token: text('token').notNull(),
    name: text('name').notNull(),
    ownerId: text('ownerId').notNull(),
  },
  (table) => [
    uniqueIndex('ApiToken_token_key').on(table.token),
    foreignKey({
      name: 'ApiToken_ownerId_fkey',
      columns: [table.ownerId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
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
    icon: text('icon'),
    plan: planEnum('plan').notNull().default('FREE'),
    stripeId: text('stripeId'),
    additionalChatsIndex: integer('additionalChatsIndex').notNull().default(0),
    additionalStorageIndex: integer('additionalStorageIndex').notNull().default(0),
    chatsLimitFirstEmailSentAt: timestamp('chatsLimitFirstEmailSentAt', { precision: 3 }),
    storageLimitFirstEmailSentAt: timestamp('storageLimitFirstEmailSentAt', { precision: 3 }),
    chatsLimitSecondEmailSentAt: timestamp('chatsLimitSecondEmailSentAt', { precision: 3 }),
    storageLimitSecondEmailSentAt: timestamp('storageLimitSecondEmailSentAt', { precision: 3 }),
    customChatsLimit: integer('customChatsLimit'),
    customStorageLimit: integer('customStorageLimit'),
    customSeatsLimit: integer('customSeatsLimit'),
    isQuarantined: boolean('isQuarantined').notNull().default(false),
    isSuspended: boolean('isSuspended').notNull().default(false),
    isPastDue: boolean('isPastDue').notNull().default(false),
    isVerified: boolean('isVerified'),
    chatsHardLimit: integer('chatsHardLimit'),
    settings: jsonb('settings'),
    lastActivityAt: timestamp('lastActivityAt', { precision: 3 }),
  },
  (table) => [uniqueIndex('Workspace_stripeId_key').on(table.stripeId)],
)

export const space = pgTable(
  'Space',
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
    icon: text('icon'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    uniqueIndex('Space_id_workspaceId_key').on(table.id, table.workspaceId),
    foreignKey({
      name: 'Space_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const memberInWorkspace = pgTable(
  'MemberInWorkspace',
  {
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
    workspaceId: text('workspaceId').notNull(),
    role: workspaceRoleEnum('role').notNull(),
  },
  (table) => [
    uniqueIndex('MemberInWorkspace_userId_workspaceId_key').on(table.userId, table.workspaceId),
    foreignKey({
      name: 'MemberInWorkspace_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'MemberInWorkspace_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceInvitation = pgTable(
  'WorkspaceInvitation',
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
    email: text('email').notNull(),
    workspaceId: text('workspaceId').notNull(),
    type: workspaceRoleEnum('type').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'WorkspaceInvitation_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const customDomain = pgTable(
  'CustomDomain',
  {
    name: text('name').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'CustomDomain_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const credentials = pgTable(
  'Credentials',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    workspaceId: text('workspaceId').notNull(),
    data: text('data').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    iv: text('iv').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'Credentials_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userCredentials = pgTable(
  'UserCredentials',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: text('userId').notNull(),
    data: text('data').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    iv: text('iv').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'UserCredentials_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    value: text('value'),
    failedAttempts: integer('failedAttempts').notNull().default(0),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const dashboardFolder = pgTable(
  'DashboardFolder',
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
    parentFolderId: text('parentFolderId'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'DashboardFolder_parentFolderId_fkey',
      columns: [table.parentFolderId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DashboardFolder_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const typebot = pgTable(
  'Typebot',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    version: text('version'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    icon: text('icon'),
    name: text('name').notNull(),
    folderId: text('folderId'),
    groups: jsonb('groups').notNull(),
    events: jsonb('events'),
    variables: jsonb('variables').notNull(),
    edges: jsonb('edges').notNull(),
    theme: jsonb('theme').notNull(),
    selectedThemeTemplateId: text('selectedThemeTemplateId'),
    settings: jsonb('settings').notNull(),
    publicId: text('publicId'),
    customDomain: text('customDomain'),
    workspaceId: text('workspaceId').notNull(),
    resultsTablePreferences: jsonb('resultsTablePreferences'),
    isArchived: boolean('isArchived').notNull().default(false),
    isClosed: boolean('isClosed').notNull().default(false),
    whatsAppCredentialsId: text('whatsAppCredentialsId'),
    riskLevel: integer('riskLevel'),
    spaceId: text('spaceId'),
  },
  (table) => [
    uniqueIndex('Typebot_publicId_key').on(table.publicId),
    uniqueIndex('Typebot_customDomain_key').on(table.customDomain),
    index('Typebot_workspaceId_idx').on(table.workspaceId),
    index('Typebot_isArchived_createdAt_idx').on(table.isArchived, table.createdAt),
    foreignKey({
      name: 'Typebot_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [dashboardFolder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Typebot_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Typebot_spaceId_fkey',
      columns: [table.spaceId],
      foreignColumns: [space.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const invitation = pgTable(
  'Invitation',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    email: text('email').notNull(),
    typebotId: text('typebotId').notNull(),
    type: collaborationTypeEnum('type').notNull(),
  },
  (table) => [
    uniqueIndex('Invitation_email_typebotId_key').on(table.email, table.typebotId),
    foreignKey({
      name: 'Invitation_typebotId_fkey',
      columns: [table.typebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const collaboratorsOnTypebots = pgTable(
  'CollaboratorsOnTypebots',
  {
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
    typebotId: text('typebotId').notNull(),
    type: collaborationTypeEnum('type').notNull(),
  },
  (table) => [
    uniqueIndex('CollaboratorsOnTypebots_userId_typebotId_key').on(table.userId, table.typebotId),
    foreignKey({
      name: 'CollaboratorsOnTypebots_typebotId_fkey',
      columns: [table.typebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CollaboratorsOnTypebots_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const publicTypebot = pgTable(
  'PublicTypebot',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    version: text('version'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastActivityAt: timestamp('lastActivityAt', { precision: 3 }),
    typebotId: text('typebotId').notNull(),
    groups: jsonb('groups').notNull(),
    events: jsonb('events'),
    variables: jsonb('variables').notNull(),
    edges: jsonb('edges').notNull(),
    theme: jsonb('theme').notNull(),
    settings: jsonb('settings').notNull(),
  },
  (table) => [
    uniqueIndex('PublicTypebot_typebotId_key').on(table.typebotId),
    index('PublicTypebot_lastActivityAt_idx').on(table.lastActivityAt),
    foreignKey({
      name: 'PublicTypebot_typebotId_fkey',
      columns: [table.typebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const runtimeMediaIdCache = pgTable(
  'RuntimeMediaIdCache',
  {
    provider: chatProviderEnum('provider').notNull(),
    url: text('url').notNull(),
    mediaId: text('mediaId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    publicTypebotId: text('publicTypebotId').notNull(),
  },
  (table) => [
    index('RuntimeMediaIdCache_expiresAt_idx').on(table.expiresAt),
    uniqueIndex('RuntimeMediaIdCache_publicTypebotId_provider_url_key').on(
      table.publicTypebotId,
      table.provider,
      table.url,
    ),
    foreignKey({
      name: 'RuntimeMediaIdCache_publicTypebotId_fkey',
      columns: [table.publicTypebotId],
      foreignColumns: [publicTypebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const result = pgTable(
  'Result',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    typebotId: text('typebotId').notNull(),
    variables: jsonb('variables').notNull(),
    isCompleted: boolean('isCompleted').notNull(),
    hasStarted: boolean('hasStarted'),
    isArchived: boolean('isArchived').default(false),
    lastChatSessionId: text('lastChatSessionId'),
  },
  (table) => [
    index('Result_typebotId_hasStarted_createdAt_idx').on(
      table.typebotId,
      table.hasStarted,
      table.createdAt,
    ),
    index('Result_typebotId_isCompleted_idx').on(table.typebotId, table.isCompleted),
    foreignKey({
      name: 'Result_typebotId_fkey',
      columns: [table.typebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const setVariableHistoryItem = pgTable(
  'SetVariableHistoryItem',
  {
    resultId: text('resultId').notNull(),
    index: integer('index').notNull(),
    variableId: text('variableId').notNull(),
    blockId: text('blockId').notNull(),
    blockIndex: integer('blockIndex'),
    value: jsonb('value').notNull(),
  },
  (table) => [
    uniqueIndex('SetVariableHistoryItem_resultId_index_key').on(table.resultId, table.index),
    foreignKey({
      name: 'SetVariableHistoryItem_resultId_fkey',
      columns: [table.resultId],
      foreignColumns: [result.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const visitedEdge = pgTable(
  'VisitedEdge',
  {
    resultId: text('resultId').notNull(),
    edgeId: text('edgeId').notNull(),
    index: integer('index').notNull(),
  },
  (table) => [
    uniqueIndex('VisitedEdge_resultId_index_key').on(table.resultId, table.index),
    foreignKey({
      name: 'VisitedEdge_resultId_fkey',
      columns: [table.resultId],
      foreignColumns: [result.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const log = pgTable(
  'Log',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    resultId: text('resultId').notNull(),
    status: text('status').notNull(),
    context: text('context'),
    description: text('description').notNull(),
    details: text('details'),
  },
  (table) => [
    index('Log_resultId_idx').on(table.resultId),
    foreignKey({
      name: 'Log_resultId_fkey',
      columns: [table.resultId],
      foreignColumns: [result.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const answer = pgTable(
  'Answer',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    resultId: text('resultId').notNull(),
    blockId: text('blockId').notNull(),
    groupId: text('groupId').notNull(),
    variableId: text('variableId'),
    content: text('content').notNull(),
  },
  (table) => [
    uniqueIndex('Answer_resultId_blockId_groupId_key').on(
      table.resultId,
      table.blockId,
      table.groupId,
    ),
    foreignKey({
      name: 'Answer_resultId_fkey',
      columns: [table.resultId],
      foreignColumns: [result.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const answerV2 = pgTable(
  'AnswerV2',
  {
    id: serial('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    blockId: text('blockId').notNull(),
    content: text('content').notNull(),
    attachedFileUrls: jsonb('attachedFileUrls'),
    resultId: text('resultId').notNull(),
  },
  (table) => [
    index('AnswerV2_blockId_idx').on(table.blockId),
    foreignKey({
      name: 'AnswerV2_resultId_fkey',
      columns: [table.resultId],
      foreignColumns: [result.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const coupon = pgTable(
  'Coupon',
  {
    userPropertiesToUpdate: jsonb('userPropertiesToUpdate').notNull(),
    code: text('code').primaryKey(),
    dateRedeemed: timestamp('dateRedeemed', { precision: 3 }),
  },
  (table) => [uniqueIndex('Coupon_code_key').on(table.code)],
)

export const webhook = pgTable(
  'Webhook',
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
    url: text('url'),
    method: text('method').notNull(),
    queryParams: jsonb('queryParams').notNull(),
    headers: jsonb('headers').notNull(),
    body: text('body'),
    typebotId: text('typebotId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'Webhook_typebotId_fkey',
      columns: [table.typebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const claimableCustomPlan = pgTable(
  'ClaimableCustomPlan',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    claimedAt: timestamp('claimedAt', { precision: 3 }),
    name: text('name').notNull(),
    description: text('description'),
    price: integer('price').notNull(),
    currency: text('currency').notNull(),
    workspaceId: text('workspaceId').notNull(),
    chatsLimit: integer('chatsLimit').notNull(),
    storageLimit: integer('storageLimit').notNull(),
    seatsLimit: integer('seatsLimit').notNull(),
    isYearly: boolean('isYearly').notNull().default(false),
    companyName: text('companyName'),
    vatType: text('vatType'),
    vatValue: text('vatValue'),
  },
  (table) => [
    uniqueIndex('ClaimableCustomPlan_workspaceId_key').on(table.workspaceId),
    foreignKey({
      name: 'ClaimableCustomPlan_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatSession = pgTable('ChatSession', {
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
  state: jsonb('state').notNull(),
  isReplying: boolean('isReplying'),
})

export const themeTemplate = pgTable(
  'ThemeTemplate',
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
    theme: jsonb('theme').notNull(),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'ThemeTemplate_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bannedIp = pgTable(
  'BannedIp',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    ip: text('ip').notNull(),
    responsibleTypebotId: text('responsibleTypebotId').notNull(),
    userId: text('userId').notNull(),
  },
  (table) => [
    uniqueIndex('BannedIp_ip_key').on(table.ip),
    foreignKey({
      name: 'BannedIp_responsibleTypebotId_fkey',
      columns: [table.responsibleTypebotId],
      foreignColumns: [typebot.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'BannedIp_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const suppressedEmail = pgTable(
  'SuppressedEmail',
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
    email: text('email').notNull(),
    transientGeneralBounceCount: integer('transientGeneralBounceCount').notNull().default(0),
    suppressedAt: timestamp('suppressedAt', { precision: 3 }),
    lastWebhookId: text('lastWebhookId'),
  },
  (table) => [uniqueIndex('SuppressedEmail_email_key').on(table.email)],
)

export const relations = defineRelations(
  {
    account,
    session,
    user,
    apiToken,
    workspace,
    space,
    memberInWorkspace,
    workspaceInvitation,
    customDomain,
    credentials,
    userCredentials,
    verificationToken,
    dashboardFolder,
    typebot,
    invitation,
    collaboratorsOnTypebots,
    publicTypebot,
    runtimeMediaIdCache,
    result,
    setVariableHistoryItem,
    visitedEdge,
    log,
    answer,
    answerV2,
    coupon,
    webhook,
    claimableCustomPlan,
    chatSession,
    themeTemplate,
    bannedIp,
    suppressedEmail,
  },
  (r) => ({
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    user: {
      accounts: r.many.account(),
      apiTokens: r.many.apiToken(),
      CollaboratorsOnTypebots: r.many.collaboratorsOnTypebots(),
      workspaces: r.many.memberInWorkspace(),
      sessions: r.many.session(),
      bannedIps: r.many.bannedIp(),
      credentials: r.many.userCredentials(),
    },
    apiToken: { owner: r.one.user({ from: r.apiToken.ownerId, to: r.user.id, optional: false }) },
    workspace: {
      credentials: r.many.credentials(),
      customDomains: r.many.customDomain(),
      folders: r.many.dashboardFolder(),
      members: r.many.memberInWorkspace(),
      typebots: r.many.typebot(),
      invitations: r.many.workspaceInvitation(),
      claimableCustomPlan: r.one.claimableCustomPlan({
        from: r.workspace.id,
        to: r.claimableCustomPlan.workspaceId,
      }),
      themeTemplates: r.many.themeTemplate(),
      spaces: r.many.space(),
    },
    space: {
      workspace: r.one.workspace({
        from: r.space.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      typebots: r.many.typebot(),
    },
    memberInWorkspace: {
      user: r.one.user({ from: r.memberInWorkspace.userId, to: r.user.id, optional: false }),
      workspace: r.one.workspace({
        from: r.memberInWorkspace.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    workspaceInvitation: {
      workspace: r.one.workspace({
        from: r.workspaceInvitation.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    customDomain: {
      workspace: r.one.workspace({
        from: r.customDomain.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    credentials: {
      workspace: r.one.workspace({
        from: r.credentials.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    userCredentials: {
      user: r.one.user({ from: r.userCredentials.userId, to: r.user.id, optional: false }),
    },
    dashboardFolder: {
      parentFolder: r.one.dashboardFolder({
        from: r.dashboardFolder.parentFolderId,
        to: r.dashboardFolder.id,
        alias: 'ParentChild',
      }),
      workspace: r.one.workspace({
        from: r.dashboardFolder.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      childrenFolder: r.many.dashboardFolder({ alias: 'ParentChild' }),
      typebots: r.many.typebot(),
    },
    typebot: {
      folder: r.one.dashboardFolder({ from: r.typebot.folderId, to: r.dashboardFolder.id }),
      workspace: r.one.workspace({
        from: r.typebot.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      collaborators: r.many.collaboratorsOnTypebots(),
      invitations: r.many.invitation(),
      publishedTypebot: r.one.publicTypebot({ from: r.typebot.id, to: r.publicTypebot.typebotId }),
      results: r.many.result(),
      webhooks: r.many.webhook(),
      bannedIps: r.many.bannedIp(),
      space: r.one.space({ from: r.typebot.spaceId, to: r.space.id }),
    },
    invitation: {
      typebot: r.one.typebot({ from: r.invitation.typebotId, to: r.typebot.id, optional: false }),
    },
    collaboratorsOnTypebots: {
      typebot: r.one.typebot({
        from: r.collaboratorsOnTypebots.typebotId,
        to: r.typebot.id,
        optional: false,
      }),
      user: r.one.user({ from: r.collaboratorsOnTypebots.userId, to: r.user.id, optional: false }),
    },
    publicTypebot: {
      typebot: r.one.typebot({
        from: r.publicTypebot.typebotId,
        to: r.typebot.id,
        optional: false,
      }),
      mediaIdsCache: r.many.runtimeMediaIdCache(),
    },
    runtimeMediaIdCache: {
      publicTypebot: r.one.publicTypebot({
        from: r.runtimeMediaIdCache.publicTypebotId,
        to: r.publicTypebot.id,
      }),
    },
    result: {
      typebot: r.one.typebot({ from: r.result.typebotId, to: r.typebot.id, optional: false }),
      answers: r.many.answer(),
      answersV2: r.many.answerV2(),
      logs: r.many.log(),
      edges: r.many.visitedEdge(),
      setVariableHistory: r.many.setVariableHistoryItem(),
    },
    setVariableHistoryItem: {
      result: r.one.result({
        from: r.setVariableHistoryItem.resultId,
        to: r.result.id,
        optional: false,
      }),
    },
    visitedEdge: {
      result: r.one.result({ from: r.visitedEdge.resultId, to: r.result.id, optional: false }),
    },
    log: { result: r.one.result({ from: r.log.resultId, to: r.result.id, optional: false }) },
    answer: { result: r.one.result({ from: r.answer.resultId, to: r.result.id, optional: false }) },
    answerV2: {
      result: r.one.result({ from: r.answerV2.resultId, to: r.result.id, optional: false }),
    },
    webhook: {
      typebot: r.one.typebot({ from: r.webhook.typebotId, to: r.typebot.id, optional: false }),
    },
    claimableCustomPlan: {
      workspace: r.one.workspace({
        from: r.claimableCustomPlan.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    themeTemplate: {
      workspace: r.one.workspace({
        from: r.themeTemplate.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    bannedIp: {
      responsibleTypebot: r.one.typebot({
        from: r.bannedIp.responsibleTypebotId,
        to: r.typebot.id,
        optional: false,
      }),
      user: r.one.user({ from: r.bannedIp.userId, to: r.user.id, optional: false }),
    },
  }),
)
