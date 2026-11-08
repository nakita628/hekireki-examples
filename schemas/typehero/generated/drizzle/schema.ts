import {
  boolean,
  datetime,
  foreignKey,
  index,
  int,
  json,
  mediumtext,
  mysqlEnum,
  mysqlTable,
  unique,
  uniqueIndex,
  varchar,
} from 'drizzle-orm/mysql-core'
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

export const account = mysqlTable(
  'Account',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: varchar('userId', { length: 191 }).notNull(),
    type: varchar('type', { length: 191 }).notNull(),
    provider: varchar('provider', { length: 191 }).notNull(),
    providerAccountId: varchar('providerAccountId', { length: 191 }).notNull(),
    refresh_token: varchar('refresh_token', { length: 191 }),
    access_token: varchar('access_token', { length: 191 }),
    expires_at: int('expires_at'),
    token_type: varchar('token_type', { length: 191 }),
    scope: varchar('scope', { length: 191 }),
    id_token: varchar('id_token', { length: 191 }),
    session_state: varchar('session_state', { length: 191 }),
  },
  (table) => [
    index('Account_userId_idx').on(table.userId),
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = mysqlTable(
  'Session',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    sessionToken: varchar('sessionToken', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const role = mysqlTable(
  'Role',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    role: mysqlEnum('role', [
      'USER',
      'ADMIN',
      'MODERATOR',
      'CREATOR',
      'SUPPORTER',
      'CONTRIBUTOR',
    ]).notNull(),
  },
  (table) => [uniqueIndex('Role_role_key').on(table.role)],
)

export const userLink = mysqlTable('UserLink', {
  id: varchar('id', { length: 191 })
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  url: varchar('url', { length: 256 }).notNull(),
  createdAt: datetime('createdAt', { fsp: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP(3)`)
    .$defaultFn(utcNow),
  updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
})

export const user = mysqlTable(
  'User',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: varchar('name', { length: 191 }).notNull(),
    email: varchar('email', { length: 191 }).notNull(),
    emailVerified: datetime('emailVerified', { fsp: 3 }),
    image: varchar('image', { length: 191 }),
    bio: varchar('bio', { length: 1000 }).notNull().default(''),
    status: mysqlEnum('status', ['BANNED', 'ACTIVE']).notNull().default('ACTIVE'),
    banReason: mediumtext('banReason'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('User_email_key').on(table.email)],
)

export const verificationToken = mysqlTable(
  'VerificationToken',
  {
    identifier: varchar('identifier', { length: 191 }).notNull(),
    token: varchar('token', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const tag = mysqlTable(
  'Tag',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    tag: mysqlEnum('tag', ['POPULAR', 'NEWEST']).notNull(),
  },
  (table) => [uniqueIndex('Tag_tag_key').on(table.tag)],
)

export const challenge = mysqlTable(
  'Challenge',
  {
    id: int('id').primaryKey().autoincrement(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    difficulty: mysqlEnum('difficulty', [
      'BEGINNER',
      'EASY',
      'MEDIUM',
      'HARD',
      'EXTREME',
      'EVENT',
    ]).notNull(),
    name: varchar('name', { length: 191 }).notNull(),
    slug: varchar('slug', { length: 191 }).notNull(),
    description: mediumtext('description').notNull(),
    shortDescription: varchar('shortDescription', { length: 191 }).notNull(),
    code: mediumtext('code').notNull(),
    tests: mediumtext('tests').notNull(),
    status: mysqlEnum('status', ['DECLINED', 'BANNED', 'PENDING', 'ACTIVE'])
      .notNull()
      .default('PENDING'),
    tsconfig: json('tsconfig'),
    userId: varchar('userId', { length: 191 }).notNull(),
  },
  (table) => [
    uniqueIndex('Challenge_name_key').on(table.name),
    uniqueIndex('Challenge_slug_key').on(table.slug),
    index('Challenge_userId_idx').on(table.userId),
    foreignKey({
      name: 'Challenge_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const vote = mysqlTable(
  'Vote',
  {
    id: int('id').primaryKey().autoincrement(),
    userId: varchar('userId', { length: 191 }).notNull(),
    rootType: mysqlEnum('rootType', [
      'CHALLENGE',
      'COMMENT',
      'SUBMISSION',
      'SHAREDSOLUTION',
    ]).notNull(),
    challengeId: int('challengeId'),
    commentId: int('commentId'),
    submissionId: int('submissionId'),
    sharedSolutionId: int('sharedSolutionId'),
  },
  (table) => [
    index('Vote_userId_idx').on(table.userId),
    index('Vote_challengeId_idx').on(table.challengeId),
    index('Vote_commentId_idx').on(table.commentId),
    index('Vote_submissionId_idx').on(table.submissionId),
    index('Vote_sharedSolutionId_idx').on(table.sharedSolutionId),
    foreignKey({ name: 'Vote_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Vote_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Vote_commentId_fkey',
      columns: [table.commentId],
      foreignColumns: [comment.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Vote_submissionId_fkey',
      columns: [table.submissionId],
      foreignColumns: [submission.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Vote_sharedSolutionId_fkey',
      columns: [table.sharedSolutionId],
      foreignColumns: [sharedSolution.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const sharedSolution = mysqlTable(
  'SharedSolution',
  {
    id: int('id').primaryKey().autoincrement(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    isPinned: boolean('isPinned').notNull().default(false),
    title: varchar('title', { length: 191 }).notNull(),
    description: mediumtext('description').notNull(),
    userId: varchar('userId', { length: 191 }),
    challengeId: int('challengeId'),
  },
  (table) => [
    index('SharedSolution_userId_idx').on(table.userId),
    index('SharedSolution_challengeId_idx').on(table.challengeId),
    foreignKey({
      name: 'SharedSolution_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SharedSolution_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const submission = mysqlTable(
  'Submission',
  {
    id: int('id').primaryKey().autoincrement(),
    code: mediumtext('code').notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    isSuccessful: boolean('isSuccessful').notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    challengeId: int('challengeId').notNull(),
  },
  (table) => [
    index('Submission_userId_idx').on(table.userId),
    index('Submission_challengeId_idx').on(table.challengeId),
    foreignKey({
      name: 'Submission_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Submission_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const bookmark = mysqlTable(
  'Bookmark',
  {
    id: int('id').primaryKey().autoincrement(),
    userId: varchar('userId', { length: 191 }).notNull(),
    challengeId: int('challengeId'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('Bookmark_userId_idx').on(table.userId),
    index('Bookmark_challengeId_idx').on(table.challengeId),
    uniqueIndex('Bookmark_userId_challengeId_key').on(table.userId, table.challengeId),
    foreignKey({ name: 'Bookmark_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Bookmark_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const reportIssue = mysqlTable(
  'ReportIssue',
  {
    id: int('id').primaryKey().autoincrement(),
    reportId: int('reportId').notNull(),
    type: mysqlEnum('type', [
      'DEROGATORY',
      'OTHER',
      'UNCLEAR',
      'BULLYING',
      'SPAM',
      'HATESPEECH',
      'THREAT',
    ]).notNull(),
  },
  (table) => [
    index('ReportIssue_reportId_idx').on(table.reportId),
    uniqueIndex('ReportIssue_id_key').on(table.id),
    foreignKey({
      name: 'ReportIssue_reportId_fkey',
      columns: [table.reportId],
      foreignColumns: [report.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const report = mysqlTable(
  'Report',
  {
    id: int('id').primaryKey().autoincrement(),
    text: mediumtext('text').notNull(),
    type: mysqlEnum('type', ['CHALLENGE', 'USER', 'COMMENT', 'SOLUTION']).notNull(),
    status: mysqlEnum('status', ['PENDING', 'CLEARED', 'DISMISSED']).notNull(),
    reporterId: varchar('reporterId', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }),
    challengeId: int('challengeId'),
    commentId: int('commentId'),
    solutionId: int('solutionId'),
    moderatorId: varchar('moderatorId', { length: 191 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Report_commentId_idx').on(table.commentId),
    index('Report_reporterId_idx').on(table.reporterId),
    index('Report_userId_idx').on(table.userId),
    index('Report_challengeId_idx').on(table.challengeId),
    index('Report_solutionId_idx').on(table.solutionId),
    index('Report_moderatorId_idx').on(table.moderatorId),
    unique('Report_id_key').on(table.id),
    foreignKey({
      name: 'Report_reporterId_fkey',
      columns: [table.reporterId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'Report_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Report_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Report_commentId_fkey',
      columns: [table.commentId],
      foreignColumns: [comment.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Report_solutionId_fkey',
      columns: [table.solutionId],
      foreignColumns: [sharedSolution.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Report_moderatorId_fkey',
      columns: [table.moderatorId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const comment = mysqlTable(
  'Comment',
  {
    id: int('id').primaryKey().autoincrement(),
    text: mediumtext('text').notNull(),
    visible: boolean('visible').notNull().default(true),
    parentId: int('parentId'),
    userId: varchar('userId', { length: 191 }).notNull(),
    rootType: mysqlEnum('rootType', ['SOLUTION', 'CHALLENGE']).notNull().default('CHALLENGE'),
    rootChallengeId: int('rootChallengeId'),
    rootSolutionId: int('rootSolutionId'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Comment_rootSolutionId_idx').on(table.rootSolutionId),
    index('Comment_rootChallengeId_idx').on(table.rootChallengeId),
    index('Comment_parentId_idx').on(table.parentId),
    index('Comment_userId_idx').on(table.userId),
    foreignKey({
      name: 'Comment_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('no action')
      .onUpdate('no action'),
    foreignKey({ name: 'Comment_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Comment_rootChallengeId_fkey',
      columns: [table.rootChallengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Comment_rootSolutionId_fkey',
      columns: [table.rootSolutionId],
      foreignColumns: [sharedSolution.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const imageUpload = mysqlTable(
  'ImageUpload',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: varchar('userId', { length: 191 }).notNull(),
    url: varchar('url', { length: 256 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ImageUpload_userId_idx').on(table.userId),
    foreignKey({
      name: 'ImageUpload_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const trackChallenge = mysqlTable(
  'TrackChallenge',
  {
    id: int('id').primaryKey().autoincrement(),
    orderId: int('orderId').notNull(),
    challengeId: int('challengeId').notNull(),
    trackId: int('trackId'),
  },
  (table) => [
    index('TrackChallenge_challengeId_idx').on(table.challengeId),
    index('TrackChallenge_trackId_idx').on(table.trackId),
    foreignKey({
      name: 'TrackChallenge_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TrackChallenge_trackId_fkey',
      columns: [table.trackId],
      foreignColumns: [track.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const track = mysqlTable(
  'Track',
  {
    id: int('id').primaryKey().autoincrement(),
    name: varchar('name', { length: 191 }).notNull(),
    slug: varchar('slug', { length: 191 }).notNull(),
    description: mediumtext('description').notNull(),
    visible: boolean('visible').notNull().default(true),
    isComingSoon: boolean('isComingSoon').notNull().default(false),
  },
  (table) => [
    uniqueIndex('Track_name_key').on(table.name),
    uniqueIndex('Track_slug_key').on(table.slug),
  ],
)

export const betaTokens = mysqlTable(
  'BetaTokens',
  {
    id: int('id').primaryKey().autoincrement(),
    token: varchar('token', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }),
  },
  (table) => [
    uniqueIndex('BetaTokens_token_key').on(table.token),
    index('BetaTokens_userId_idx').on(table.userId),
    foreignKey({
      name: 'BetaTokens_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const donation = mysqlTable(
  'Donation',
  {
    id: int('id').primaryKey().autoincrement(),
    userId: varchar('userId', { length: 191 }).notNull(),
    amount: int('amount').notNull(),
    stripeCheckoutSessionId: varchar('stripeCheckoutSessionId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('Donation_stripeCheckoutSessionId_key').on(table.stripeCheckoutSessionId),
    index('Donation_userId_idx').on(table.userId),
    foreignKey({ name: 'Donation_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const notification = mysqlTable(
  'Notification',
  {
    id: int('id').primaryKey().autoincrement(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    isRead: boolean('isRead').notNull().default(false),
    type: mysqlEnum('type', ['LIKE', 'REPLY', 'MENTION', 'MISC', 'SYSTEM']).notNull(),
    toUserId: varchar('toUserId', { length: 191 }).notNull(),
    fromUserId: varchar('fromUserId', { length: 191 }).notNull(),
    commentId: int('commentId'),
    content: varchar('content', { length: 191 }),
    blurb: varchar('blurb', { length: 191 }),
    url: varchar('url', { length: 191 }).notNull(),
    challengeId: int('challengeId'),
    sharedSolutionId: int('sharedSolutionId'),
  },
  (table) => [
    index('Notification_toUserId_idx').on(table.toUserId),
    index('Notification_fromUserId_idx').on(table.fromUserId),
    index('Notification_commentId_idx').on(table.commentId),
    index('Notification_challengeId_idx').on(table.challengeId),
    index('Notification_sharedSolutionId_idx').on(table.sharedSolutionId),
    foreignKey({
      name: 'Notification_toUserId_fkey',
      columns: [table.toUserId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Notification_fromUserId_fkey',
      columns: [table.fromUserId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Notification_commentId_fkey',
      columns: [table.commentId],
      foreignColumns: [comment.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Notification_challengeId_fkey',
      columns: [table.challengeId],
      foreignColumns: [challenge.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Notification_sharedSolutionId_fkey',
      columns: [table.sharedSolutionId],
      foreignColumns: [sharedSolution.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const shortURL = mysqlTable(
  'ShortURL',
  {
    id: int('id').primaryKey().autoincrement(),
    originalUrl: mediumtext('originalUrl').notNull(),
    shortUrlSlug: varchar('shortUrlSlug', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    expiresAt: datetime('expiresAt', { fsp: 3 }),
    clicks: int('clicks').notNull().default(0),
    userId: varchar('userId', { length: 191 }),
  },
  (table) => [
    uniqueIndex('ShortURL_shortUrlSlug_key').on(table.shortUrlSlug),
    index('ShortURL_userId_idx').on(table.userId),
    index('ShortURL_shortUrlSlug_idx').on(table.shortUrlSlug),
    foreignKey({ name: 'ShortURL_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const roleToUser = mysqlTable(
  '_RoleToUser',
  { A: varchar('A', { length: 191 }).notNull(), B: varchar('B', { length: 191 }).notNull() },
  (table) => [
    uniqueIndex('_RoleToUser_AB_unique').on(table.A, table.B),
    index('_RoleToUser_B_index').on(table.B),
    foreignKey({ name: '_RoleToUser_A_fkey', columns: [table.A], foreignColumns: [role.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_RoleToUser_B_fkey', columns: [table.B], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userToUserLink = mysqlTable(
  '_UserToUserLink',
  { A: varchar('A', { length: 191 }).notNull(), B: varchar('B', { length: 191 }).notNull() },
  (table) => [
    uniqueIndex('_UserToUserLink_AB_unique').on(table.A, table.B),
    index('_UserToUserLink_B_index').on(table.B),
    foreignKey({ name: '_UserToUserLink_A_fkey', columns: [table.A], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: '_UserToUserLink_B_fkey',
      columns: [table.B],
      foreignColumns: [userLink.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const trackToUser = mysqlTable(
  '_TrackToUser',
  { A: int('A').notNull(), B: varchar('B', { length: 191 }).notNull() },
  (table) => [
    uniqueIndex('_TrackToUser_AB_unique').on(table.A, table.B),
    index('_TrackToUser_B_index').on(table.B),
    foreignKey({ name: '_TrackToUser_A_fkey', columns: [table.A], foreignColumns: [track.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_TrackToUser_B_fkey', columns: [table.B], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const challengeToTag = mysqlTable(
  '_ChallengeToTag',
  { A: int('A').notNull(), B: varchar('B', { length: 191 }).notNull() },
  (table) => [
    uniqueIndex('_ChallengeToTag_AB_unique').on(table.A, table.B),
    index('_ChallengeToTag_B_index').on(table.B),
    foreignKey({
      name: '_ChallengeToTag_A_fkey',
      columns: [table.A],
      foreignColumns: [challenge.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_ChallengeToTag_B_fkey', columns: [table.B], foreignColumns: [tag.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    account,
    session,
    role,
    userLink,
    user,
    verificationToken,
    tag,
    challenge,
    vote,
    sharedSolution,
    submission,
    bookmark,
    reportIssue,
    report,
    comment,
    imageUpload,
    trackChallenge,
    track,
    betaTokens,
    donation,
    notification,
    shortURL,
    roleToUser,
    userToUserLink,
    trackToUser,
    challengeToTag,
  },
  (r) => ({
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    role: {
      user: r.many.user({
        from: r.role.id.through(r.roleToUser.A),
        to: r.user.id.through(r.roleToUser.B),
      }),
    },
    userLink: {
      user: r.many.user({
        from: r.userLink.id.through(r.userToUserLink.B),
        to: r.user.id.through(r.userToUserLink.A),
      }),
    },
    user: {
      roles: r.many.role({
        from: r.user.id.through(r.roleToUser.B),
        to: r.role.id.through(r.roleToUser.A),
      }),
      userLinks: r.many.userLink({
        from: r.user.id.through(r.userToUserLink.A),
        to: r.userLink.id.through(r.userToUserLink.B),
      }),
      accounts: r.many.account(),
      sessions: r.many.session(),
      challenge: r.many.challenge(),
      vote: r.many.vote(),
      submission: r.many.submission(),
      bookmark: r.many.bookmark(),
      sharedSolution: r.many.sharedSolution(),
      comment: r.many.comment(),
      reports: r.many.report({ alias: 'reporter' }),
      reportsAbout: r.many.report({ alias: 'reported_user' }),
      moderated: r.many.report({ alias: 'report_moderator' }),
      imageUpload: r.many.imageUpload(),
      tracks: r.many.track({
        from: r.user.id.through(r.trackToUser.B),
        to: r.track.id.through(r.trackToUser.A),
      }),
      BetaTokens: r.many.betaTokens(),
      donations: r.many.donation(),
      shortUrls: r.many.shortURL(),
      toUserNotification: r.many.notification({ alias: 'notificationToUser' }),
      fromUserNotification: r.many.notification({ alias: 'notificationFromUser' }),
    },
    tag: {
      challenges: r.many.challenge({
        from: r.tag.id.through(r.challengeToTag.B),
        to: r.challenge.id.through(r.challengeToTag.A),
      }),
    },
    challenge: {
      user: r.one.user({ from: r.challenge.userId, to: r.user.id, optional: false }),
      tags: r.many.tag({
        from: r.challenge.id.through(r.challengeToTag.A),
        to: r.tag.id.through(r.challengeToTag.B),
      }),
      vote: r.many.vote(),
      submission: r.many.submission(),
      comment: r.many.comment(),
      bookmark: r.many.bookmark(),
      sharedSolution: r.many.sharedSolution(),
      Report: r.many.report(),
      TrackChallenge: r.many.trackChallenge(),
      Notification: r.many.notification(),
    },
    vote: {
      user: r.one.user({ from: r.vote.userId, to: r.user.id, optional: false }),
      challenge: r.one.challenge({ from: r.vote.challengeId, to: r.challenge.id }),
      comment: r.one.comment({ from: r.vote.commentId, to: r.comment.id }),
      submission: r.one.submission({ from: r.vote.submissionId, to: r.submission.id }),
      sharedSolution: r.one.sharedSolution({
        from: r.vote.sharedSolutionId,
        to: r.sharedSolution.id,
      }),
    },
    sharedSolution: {
      vote: r.many.vote(),
      user: r.one.user({ from: r.sharedSolution.userId, to: r.user.id }),
      challenge: r.one.challenge({ from: r.sharedSolution.challengeId, to: r.challenge.id }),
      solutionComment: r.many.comment(),
      Report: r.many.report(),
      Notification: r.many.notification(),
    },
    submission: {
      user: r.one.user({ from: r.submission.userId, to: r.user.id, optional: false }),
      challenge: r.one.challenge({
        from: r.submission.challengeId,
        to: r.challenge.id,
        optional: false,
      }),
      vote: r.many.vote(),
    },
    bookmark: {
      user: r.one.user({ from: r.bookmark.userId, to: r.user.id, optional: false }),
      challenge: r.one.challenge({ from: r.bookmark.challengeId, to: r.challenge.id }),
    },
    reportIssue: {
      report: r.one.report({ from: r.reportIssue.reportId, to: r.report.id, optional: false }),
    },
    report: {
      issues: r.many.reportIssue(),
      reporter: r.one.user({
        from: r.report.reporterId,
        to: r.user.id,
        optional: false,
        alias: 'reporter',
      }),
      user: r.one.user({ from: r.report.userId, to: r.user.id, alias: 'reported_user' }),
      challenge: r.one.challenge({ from: r.report.challengeId, to: r.challenge.id }),
      comment: r.one.comment({ from: r.report.commentId, to: r.comment.id }),
      solution: r.one.sharedSolution({ from: r.report.solutionId, to: r.sharedSolution.id }),
      moderator: r.one.user({
        from: r.report.moderatorId,
        to: r.user.id,
        alias: 'report_moderator',
      }),
    },
    comment: {
      parentComment: r.one.comment({
        from: r.comment.parentId,
        to: r.comment.id,
        alias: 'reply_comments',
      }),
      replies: r.many.comment({ alias: 'reply_comments' }),
      user: r.one.user({ from: r.comment.userId, to: r.user.id, optional: false }),
      rootChallenge: r.one.challenge({ from: r.comment.rootChallengeId, to: r.challenge.id }),
      rootSolution: r.one.sharedSolution({
        from: r.comment.rootSolutionId,
        to: r.sharedSolution.id,
      }),
      Report: r.many.report(),
      vote: r.many.vote(),
      Notification: r.many.notification(),
    },
    imageUpload: {
      user: r.one.user({ from: r.imageUpload.userId, to: r.user.id, optional: false }),
    },
    trackChallenge: {
      challenge: r.one.challenge({
        from: r.trackChallenge.challengeId,
        to: r.challenge.id,
        optional: false,
      }),
      track: r.one.track({ from: r.trackChallenge.trackId, to: r.track.id }),
    },
    track: {
      trackChallenges: r.many.trackChallenge(),
      enrolledUsers: r.many.user({
        from: r.track.id.through(r.trackToUser.A),
        to: r.user.id.through(r.trackToUser.B),
      }),
    },
    betaTokens: { user: r.one.user({ from: r.betaTokens.userId, to: r.user.id }) },
    donation: { user: r.one.user({ from: r.donation.userId, to: r.user.id, optional: false }) },
    notification: {
      toUser: r.one.user({
        from: r.notification.toUserId,
        to: r.user.id,
        optional: false,
        alias: 'notificationToUser',
      }),
      fromUser: r.one.user({
        from: r.notification.fromUserId,
        to: r.user.id,
        optional: false,
        alias: 'notificationFromUser',
      }),
      comment: r.one.comment({ from: r.notification.commentId, to: r.comment.id }),
      Challenge: r.one.challenge({ from: r.notification.challengeId, to: r.challenge.id }),
      SharedSolution: r.one.sharedSolution({
        from: r.notification.sharedSolutionId,
        to: r.sharedSolution.id,
      }),
    },
    shortURL: { user: r.one.user({ from: r.shortURL.userId, to: r.user.id }) },
    roleToUser: {
      role: r.one.role({ from: r.roleToUser.A, to: r.role.id, optional: false }),
      user: r.one.user({ from: r.roleToUser.B, to: r.user.id, optional: false }),
    },
    userToUserLink: {
      user: r.one.user({ from: r.userToUserLink.A, to: r.user.id, optional: false }),
      userLink: r.one.userLink({ from: r.userToUserLink.B, to: r.userLink.id, optional: false }),
    },
    trackToUser: {
      track: r.one.track({ from: r.trackToUser.A, to: r.track.id, optional: false }),
      user: r.one.user({ from: r.trackToUser.B, to: r.user.id, optional: false }),
    },
    challengeToTag: {
      challenge: r.one.challenge({ from: r.challengeToTag.A, to: r.challenge.id, optional: false }),
      tag: r.one.tag({ from: r.challengeToTag.B, to: r.tag.id, optional: false }),
    },
  }),
)
