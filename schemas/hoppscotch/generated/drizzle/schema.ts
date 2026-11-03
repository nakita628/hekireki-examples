import {
  boolean,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
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

export const workspaceTypeEnum = pgEnum('WorkspaceType', ['USER', 'TEAM'])

export const reqTypeEnum = pgEnum('ReqType', ['REST', 'GQL'])

export const teamAccessRoleEnum = pgEnum('TeamAccessRole', ['OWNER', 'VIEWER', 'EDITOR'])

export const mockServerActionEnum = pgEnum('MockServerAction', [
  'CREATED',
  'DELETED',
  'ACTIVATED',
  'DEACTIVATED',
])

export const team = pgTable('Team', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  name: text('name').notNull(),
})

export const teamMember = pgTable(
  'TeamMember',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    role: teamAccessRoleEnum('role').notNull(),
    userUid: text('userUid').notNull(),
    teamID: text('teamID').notNull(),
  },
  (table) => [
    uniqueIndex('TeamMember_teamID_userUid_key').on(table.teamID, table.userUid),
    foreignKey({
      name: 'TeamMember_teamID_fkey',
      columns: [table.teamID],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamInvitation = pgTable(
  'TeamInvitation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    teamID: text('teamID').notNull(),
    creatorUid: text('creatorUid').notNull(),
    inviteeEmail: text('inviteeEmail').notNull(),
    inviteeRole: teamAccessRoleEnum('inviteeRole').notNull(),
  },
  (table) => [
    index('TeamInvitation_teamID_idx').on(table.teamID),
    uniqueIndex('TeamInvitation_teamID_inviteeEmail_key').on(table.teamID, table.inviteeEmail),
    foreignKey({
      name: 'TeamInvitation_teamID_fkey',
      columns: [table.teamID],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamCollection = pgTable(
  'TeamCollection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    parentID: text('parentID'),
    teamID: text('teamID').notNull(),
    title: text('title').notNull(),
    orderIndex: integer('orderIndex').notNull(),
    data: jsonb('data'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('TeamCollection_teamID_parentID_orderIndex_key').on(
      table.teamID,
      table.parentID,
      table.orderIndex,
    ),
    foreignKey({
      name: 'TeamCollection_parentID_fkey',
      columns: [table.parentID],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TeamCollection_teamID_fkey',
      columns: [table.teamID],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamRequest = pgTable(
  'TeamRequest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    collectionID: text('collectionID').notNull(),
    teamID: text('teamID').notNull(),
    title: text('title').notNull(),
    request: jsonb('request').notNull(),
    mockExamples: jsonb('mockExamples'),
    orderIndex: integer('orderIndex').notNull(),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('TeamRequest_teamID_collectionID_orderIndex_key').on(
      table.teamID,
      table.collectionID,
      table.orderIndex,
    ),
    foreignKey({
      name: 'TeamRequest_collectionID_fkey',
      columns: [table.collectionID],
      foreignColumns: [teamCollection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TeamRequest_teamID_fkey',
      columns: [table.teamID],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamEnvironment = pgTable(
  'TeamEnvironment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    teamID: text('teamID').notNull(),
    name: text('name').notNull(),
    variables: jsonb('variables').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'TeamEnvironment_teamID_fkey',
      columns: [table.teamID],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
  {
    uid: text('uid')
      .primaryKey()
      .$defaultFn(() => cuid()),
    displayName: text('displayName'),
    email: text('email'),
    photoURL: text('photoURL'),
    isAdmin: boolean('isAdmin').notNull().default(false),
    refreshToken: text('refreshToken'),
    currentRESTSession: jsonb('currentRESTSession'),
    currentGQLSession: jsonb('currentGQLSession'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastLoggedOn: timestamp('lastLoggedOn', { withTimezone: true, precision: 3 }),
    lastActiveOn: timestamp('lastActiveOn', { withTimezone: true, precision: 3 }),
  },
  (table) => [uniqueIndex('User_email_key').on(table.email)],
)

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    providerRefreshToken: text('providerRefreshToken'),
    providerAccessToken: text('providerAccessToken'),
    providerScope: text('providerScope'),
    loggedIn: timestamp('loggedIn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.uid] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    deviceIdentifier: text('deviceIdentifier').notNull(),
    token: text('token')
      .notNull()
      .$defaultFn(() => cuid()),
    userUid: text('userUid').notNull(),
    expiresOn: timestamp('expiresOn', { withTimezone: true, precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_deviceIdentifier_token_key').on(
      table.deviceIdentifier,
      table.token,
    ),
    foreignKey({
      name: 'VerificationToken_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userSettings = pgTable(
  'UserSettings',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userUid: text('userUid').notNull(),
    properties: jsonb('properties').notNull(),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('UserSettings_userUid_key').on(table.userUid),
    foreignKey({
      name: 'UserSettings_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userHistory = pgTable(
  'UserHistory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userUid: text('userUid').notNull(),
    reqType: reqTypeEnum('reqType').notNull(),
    request: jsonb('request').notNull(),
    responseMetadata: jsonb('responseMetadata').notNull(),
    isStarred: boolean('isStarred').notNull(),
    executedOn: timestamp('executedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'UserHistory_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userEnvironment = pgTable(
  'UserEnvironment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userUid: text('userUid').notNull(),
    name: text('name'),
    variables: jsonb('variables').notNull(),
    isGlobal: boolean('isGlobal').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'UserEnvironment_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const invitedUsers = pgTable(
  'InvitedUsers',
  {
    adminUid: text('adminUid').notNull(),
    adminEmail: text('adminEmail').notNull(),
    inviteeEmail: text('inviteeEmail').notNull(),
    invitedOn: timestamp('invitedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('InvitedUsers_inviteeEmail_key').on(table.inviteeEmail),
    foreignKey({
      name: 'InvitedUsers_adminUid_fkey',
      columns: [table.adminUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userRequest = pgTable(
  'UserRequest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    collectionID: text('collectionID').notNull(),
    userUid: text('userUid').notNull(),
    title: text('title').notNull(),
    request: jsonb('request').notNull(),
    type: reqTypeEnum('type').notNull(),
    mockExamples: jsonb('mockExamples'),
    orderIndex: integer('orderIndex').notNull(),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('UserRequest_userUid_collectionID_orderIndex_key').on(
      table.userUid,
      table.collectionID,
      table.orderIndex,
    ),
    foreignKey({
      name: 'UserRequest_collectionID_fkey',
      columns: [table.collectionID],
      foreignColumns: [userCollection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserRequest_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userCollection = pgTable(
  'UserCollection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    parentID: text('parentID'),
    userUid: text('userUid').notNull(),
    title: text('title').notNull(),
    type: reqTypeEnum('type').notNull(),
    orderIndex: integer('orderIndex').notNull(),
    data: jsonb('data'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('UserCollection_userUid_parentID_orderIndex_key').on(
      table.userUid,
      table.parentID,
      table.orderIndex,
    ),
    foreignKey({
      name: 'UserCollection_parentID_fkey',
      columns: [table.parentID],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserCollection_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mockServer = pgTable(
  'MockServer',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    subdomain: text('subdomain').notNull(),
    creatorUid: text('creatorUid'),
    collectionID: text('collectionID').notNull(),
    workspaceType: workspaceTypeEnum('workspaceType').notNull(),
    workspaceID: text('workspaceID').notNull(),
    delayInMs: integer('delayInMs').notNull().default(0),
    isPublic: boolean('isPublic').notNull().default(true),
    isActive: boolean('isActive').notNull().default(true),
    hitCount: integer('hitCount').notNull().default(0),
    lastHitAt: timestamp('lastHitAt', { withTimezone: true, precision: 3 }),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { withTimezone: true, precision: 3 }),
  },
  (table) => [
    uniqueIndex('MockServer_subdomain_key').on(table.subdomain),
    foreignKey({
      name: 'MockServer_creatorUid_fkey',
      columns: [table.creatorUid],
      foreignColumns: [user.uid],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const mockServerLog = pgTable(
  'MockServerLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    mockServerID: text('mockServerID').notNull(),
    requestMethod: text('requestMethod').notNull(),
    requestPath: text('requestPath').notNull(),
    requestHeaders: jsonb('requestHeaders').notNull(),
    requestBody: jsonb('requestBody'),
    requestQuery: jsonb('requestQuery'),
    responseStatus: integer('responseStatus').notNull(),
    responseHeaders: jsonb('responseHeaders').notNull(),
    responseBody: jsonb('responseBody'),
    responseTime: integer('responseTime').notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    executedAt: timestamp('executedAt', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('MockServerLog_mockServerID_idx').on(table.mockServerID),
    index('MockServerLog_mockServerID_executedAt_idx').on(table.mockServerID, table.executedAt),
    foreignKey({
      name: 'MockServerLog_mockServerID_fkey',
      columns: [table.mockServerID],
      foreignColumns: [mockServer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mockServerActivity = pgTable(
  'MockServerActivity',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    mockServerID: text('mockServerID').notNull(),
    action: mockServerActionEnum('action').notNull(),
    performedBy: text('performedBy'),
    performedAt: timestamp('performedAt', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('MockServerActivity_mockServerID_idx').on(table.mockServerID),
    foreignKey({
      name: 'MockServerActivity_mockServerID_fkey',
      columns: [table.mockServerID],
      foreignColumns: [mockServer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const shortcode = pgTable(
  'Shortcode',
  {
    id: text('id').primaryKey(),
    request: jsonb('request').notNull(),
    creatorUid: text('creatorUid'),
    embedProperties: jsonb('embedProperties'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Shortcode_id_key').on(table.id),
    uniqueIndex('Shortcode_id_creatorUid_key').on(table.id, table.creatorUid),
    foreignKey({
      name: 'Shortcode_creatorUid_fkey',
      columns: [table.creatorUid],
      foreignColumns: [user.uid],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const infraConfig = pgTable(
  'InfraConfig',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    value: text('value'),
    isEncrypted: boolean('isEncrypted').notNull().default(false),
    lastSyncedEnvFileValue: text('lastSyncedEnvFileValue'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('InfraConfig_name_key').on(table.name)],
)

export const personalAccessToken = pgTable(
  'PersonalAccessToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userUid: text('userUid').notNull(),
    label: text('label').notNull(),
    token: text('token')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    expiresOn: timestamp('expiresOn', { withTimezone: true, precision: 3 }),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('PersonalAccessToken_token_key').on(table.token),
    foreignKey({
      name: 'PersonalAccessToken_userUid_fkey',
      columns: [table.userUid],
      foreignColumns: [user.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const infraToken = pgTable(
  'InfraToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    creatorUid: text('creatorUid').notNull(),
    label: text('label').notNull(),
    token: text('token')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    expiresOn: timestamp('expiresOn', { withTimezone: true, precision: 3 }),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('InfraToken_token_key').on(table.token)],
)

export const publishedDocs = pgTable(
  'PublishedDocs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    slug: text('slug').notNull(),
    title: text('title').notNull(),
    collectionID: text('collectionID').notNull(),
    creatorUid: text('creatorUid').notNull(),
    version: text('version').notNull(),
    autoSync: boolean('autoSync').notNull(),
    documentTree: jsonb('documentTree'),
    workspaceType: workspaceTypeEnum('workspaceType').notNull(),
    workspaceID: text('workspaceID').notNull(),
    environmentID: text('environmentID'),
    environmentName: text('environmentName'),
    environmentVariables: jsonb('environmentVariables'),
    metadata: jsonb('metadata'),
    createdOn: timestamp('createdOn', { withTimezone: true, precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedOn: timestamp('updatedOn', { withTimezone: true, precision: 3 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('PublishedDocs_collectionID_idx').on(table.collectionID),
    uniqueIndex('PublishedDocs_slug_version_key').on(table.slug, table.version),
  ],
)

export const relations = defineRelations(
  {
    team,
    teamMember,
    teamInvitation,
    teamCollection,
    teamRequest,
    teamEnvironment,
    user,
    account,
    verificationToken,
    userSettings,
    userHistory,
    userEnvironment,
    invitedUsers,
    userRequest,
    userCollection,
    mockServer,
    mockServerLog,
    mockServerActivity,
    shortcode,
    infraConfig,
    personalAccessToken,
    infraToken,
    publishedDocs,
  },
  (r) => ({
    team: {
      collections: r.many.teamCollection(),
      environments: r.many.teamEnvironment(),
      invitations: r.many.teamInvitation(),
      members: r.many.teamMember(),
      requests: r.many.teamRequest(),
    },
    teamMember: { team: r.one.team({ from: r.teamMember.teamID, to: r.team.id, optional: false }) },
    teamInvitation: {
      team: r.one.team({ from: r.teamInvitation.teamID, to: r.team.id, optional: false }),
    },
    teamCollection: {
      parent: r.one.teamCollection({
        from: r.teamCollection.parentID,
        to: r.teamCollection.id,
        alias: 'TeamCollectionChildParent',
      }),
      children: r.many.teamCollection({ alias: 'TeamCollectionChildParent' }),
      team: r.one.team({ from: r.teamCollection.teamID, to: r.team.id, optional: false }),
      requests: r.many.teamRequest(),
    },
    teamRequest: {
      collection: r.one.teamCollection({
        from: r.teamRequest.collectionID,
        to: r.teamCollection.id,
        optional: false,
      }),
      team: r.one.team({ from: r.teamRequest.teamID, to: r.team.id, optional: false }),
    },
    teamEnvironment: {
      team: r.one.team({ from: r.teamEnvironment.teamID, to: r.team.id, optional: false }),
    },
    user: {
      providerAccounts: r.many.account(),
      invitedUsers: r.many.invitedUsers(),
      mockServers: r.many.mockServer(),
      personalAccessTokens: r.many.personalAccessToken(),
      shortcodes: r.many.shortcode(),
      userCollections: r.many.userCollection(),
      environments: r.many.userEnvironment(),
      history: r.many.userHistory(),
      userRequests: r.many.userRequest(),
      settings: r.one.userSettings({ from: r.user.uid, to: r.userSettings.userUid }),
      verificationTokens: r.many.verificationToken(),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.uid, optional: false }) },
    verificationToken: {
      user: r.one.user({ from: r.verificationToken.userUid, to: r.user.uid, optional: false }),
    },
    userSettings: {
      user: r.one.user({ from: r.userSettings.userUid, to: r.user.uid, optional: false }),
    },
    userHistory: {
      user: r.one.user({ from: r.userHistory.userUid, to: r.user.uid, optional: false }),
    },
    userEnvironment: {
      user: r.one.user({ from: r.userEnvironment.userUid, to: r.user.uid, optional: false }),
    },
    invitedUsers: {
      user: r.one.user({ from: r.invitedUsers.adminUid, to: r.user.uid, optional: false }),
    },
    userRequest: {
      userCollection: r.one.userCollection({
        from: r.userRequest.collectionID,
        to: r.userCollection.id,
        optional: false,
      }),
      user: r.one.user({ from: r.userRequest.userUid, to: r.user.uid, optional: false }),
    },
    userCollection: {
      parent: r.one.userCollection({
        from: r.userCollection.parentID,
        to: r.userCollection.id,
        alias: 'ParentUserCollection',
      }),
      children: r.many.userCollection({ alias: 'ParentUserCollection' }),
      user: r.one.user({ from: r.userCollection.userUid, to: r.user.uid, optional: false }),
      requests: r.many.userRequest(),
    },
    mockServer: {
      user: r.one.user({ from: r.mockServer.creatorUid, to: r.user.uid }),
      requestLogs: r.many.mockServerLog(),
      activityHistory: r.many.mockServerActivity(),
    },
    mockServerLog: {
      mockServer: r.one.mockServer({
        from: r.mockServerLog.mockServerID,
        to: r.mockServer.id,
        optional: false,
      }),
    },
    mockServerActivity: {
      mockServer: r.one.mockServer({
        from: r.mockServerActivity.mockServerID,
        to: r.mockServer.id,
        optional: false,
      }),
    },
    shortcode: { user: r.one.user({ from: r.shortcode.creatorUid, to: r.user.uid }) },
    personalAccessToken: {
      user: r.one.user({ from: r.personalAccessToken.userUid, to: r.user.uid, optional: false }),
    },
  }),
)
