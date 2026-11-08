import {
  bigint,
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
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

const utcDate = customType<{ data: Date; driverData: string }>({
  dataType: () => 'date',
  toDriver: (value) => value.toISOString().slice(0, 10),
  fromDriver: (value) => new Date(value),
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

export const orderStatusEnum = pgEnum('OrderStatus', [
  'PENDING',
  'ACCEPTED',
  'CANCELED',
  'COMPLETED',
])

export const fromEnum = pgEnum('From', ['BUYER', 'SELLER'])

export const stateEnum = pgEnum('State', ['QUEUE', 'PUBLISHED', 'ERROR', 'DRAFT'])

export const subscriptionTierEnum = pgEnum('SubscriptionTier', [
  'STANDARD',
  'PRO',
  'TEAM',
  'ULTIMATE',
])

export const periodEnum = pgEnum('Period', ['MONTHLY', 'YEARLY'])

export const providerEnum = pgEnum('Provider', [
  'LOCAL',
  'GITHUB',
  'GOOGLE',
  'APPLE',
  'FARCASTER',
  'WALLET',
  'GENERIC',
])

export const roleEnum = pgEnum('Role', ['SUPERADMIN', 'ADMIN', 'USER'])

export const approvedSubmitForOrderEnum = pgEnum('APPROVED_SUBMIT_FOR_ORDER', [
  'NO',
  'WAITING_CONFIRMATION',
  'YES',
])

export const creationMethodEnum = pgEnum('CreationMethod', [
  'UNKNOWN',
  'WEB',
  'MCP',
  'API',
  'AUTOPOST',
  'CLI',
])

export const shortLinkPreferenceEnum = pgEnum('ShortLinkPreference', ['ASK', 'YES', 'NO'])

export const announcementColorEnum = pgEnum('AnnouncementColor', ['INFO', 'WARNING', 'ERROR'])

export const organization = pgTable(
  'Organization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    description: text('description'),
    apiKey: text('apiKey'),
    paymentId: text('paymentId'),
    streakSince: timestamp('streakSince', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    allowTrial: boolean('allowTrial').notNull().default(false),
    isTrailing: boolean('isTrailing').notNull().default(false),
    shortlink: shortLinkPreferenceEnum('shortlink').notNull().default('ASK'),
  },
  (table) => [
    index('Organization_apiKey_idx').on(table.apiKey),
    index('Organization_streakSince_idx').on(table.streakSince),
    index('Organization_paymentId_idx').on(table.paymentId),
    index('Organization_deletedAt_idx').on(table.deletedAt),
  ],
)

export const tags = pgTable(
  'Tags',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    color: text('color').notNull(),
    orgId: text('orgId').notNull(),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Tags_orgId_idx').on(table.orgId),
    index('Tags_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Tags_orgId_fkey',
      columns: [table.orgId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const tagsPosts = pgTable(
  'TagsPosts',
  {
    postId: text('postId').notNull(),
    tagId: text('tagId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'TagsPosts_pkey', columns: [table.postId, table.tagId] }),
    uniqueIndex('TagsPosts_postId_tagId_key').on(table.postId, table.tagId),
    foreignKey({
      name: 'TagsPosts_postId_fkey',
      columns: [table.postId],
      foreignColumns: [post.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'TagsPosts_tagId_fkey', columns: [table.tagId], foreignColumns: [tags.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    email: text('email').notNull(),
    password: text('password'),
    providerName: providerEnum('providerName').notNull(),
    name: text('name'),
    lastName: text('lastName'),
    isSuperAdmin: boolean('isSuperAdmin').notNull().default(false),
    bio: text('bio'),
    audience: integer('audience').notNull().default(0),
    pictureId: text('pictureId'),
    providerId: text('providerId'),
    timezone: integer('timezone').notNull(),
    timezoneName: text('timezoneName'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    lastReadNotifications: timestamp('lastReadNotifications', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    inviteId: text('inviteId'),
    activated: boolean('activated').notNull().default(true),
    account: text('account'),
    connectedAccount: boolean('connectedAccount').notNull().default(false),
    lastOnline: timestamp('lastOnline', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    ip: text('ip'),
    agent: text('agent'),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    sendSuccessEmails: boolean('sendSuccessEmails').notNull().default(true),
    sendFailureEmails: boolean('sendFailureEmails').notNull().default(true),
    sendStreakEmails: boolean('sendStreakEmails').notNull().default(true),
  },
  (table) => [
    index('User_providerId_providerName_idx').on(table.providerId, table.providerName),
    index('User_lastReadNotifications_idx').on(table.lastReadNotifications),
    index('User_inviteId_idx').on(table.inviteId),
    index('User_account_idx').on(table.account),
    index('User_lastOnline_idx').on(table.lastOnline),
    index('User_pictureId_idx').on(table.pictureId),
    index('User_deletedAt_idx').on(table.deletedAt),
    uniqueIndex('User_email_providerName_key').on(table.email, table.providerName),
    foreignKey({
      name: 'User_pictureId_fkey',
      columns: [table.pictureId],
      foreignColumns: [media.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const usedCodes = pgTable(
  'UsedCodes',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    code: text('code').notNull(),
    orgId: text('orgId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('UsedCodes_code_idx').on(table.code),
    foreignKey({
      name: 'UsedCodes_orgId_fkey',
      columns: [table.orgId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const userOrganization = pgTable(
  'UserOrganization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: text('userId').notNull(),
    organizationId: text('organizationId').notNull(),
    disabled: boolean('disabled').notNull().default(false),
    role: roleEnum('role').notNull().default('USER'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('UserOrganization_organizationId_idx').on(table.organizationId),
    index('UserOrganization_disabled_idx').on(table.disabled),
    uniqueIndex('UserOrganization_userId_organizationId_key').on(
      table.userId,
      table.organizationId,
    ),
    foreignKey({
      name: 'UserOrganization_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserOrganization_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const gitHub = pgTable(
  'GitHub',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    login: text('login'),
    name: text('name'),
    token: text('token').notNull(),
    jobId: text('jobId'),
    organizationId: text('organizationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('GitHub_login_idx').on(table.login),
    index('GitHub_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'GitHub_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const trending = pgTable(
  'Trending',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    trendingList: text('trendingList').notNull(),
    language: text('language'),
    hash: text('hash').notNull(),
    date: timestamp('date', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Trending_language_key').on(table.language),
    index('Trending_hash_idx').on(table.hash),
  ],
)

export const trendingLog = pgTable('TrendingLog', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  language: text('language'),
  date: timestamp('date', { precision: 3 }).notNull(),
})

export const itemUser = pgTable(
  'ItemUser',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: text('userId').notNull(),
    key: text('key').notNull(),
  },
  (table) => [
    index('ItemUser_userId_idx').on(table.userId),
    index('ItemUser_key_idx').on(table.key),
    uniqueIndex('ItemUser_userId_key_key').on(table.userId, table.key),
    foreignKey({ name: 'ItemUser_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const star = pgTable(
  'Star',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    stars: integer('stars').notNull(),
    totalStars: integer('totalStars').notNull(),
    forks: integer('forks').notNull(),
    totalForks: integer('totalForks').notNull(),
    login: text('login').notNull(),
    date: utcDate('date')
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('Star_login_date_key').on(table.login, table.date)],
)

export const media = pgTable(
  'Media',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    originalName: text('originalName'),
    path: text('path').notNull(),
    organizationId: text('organizationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    fileSize: integer('fileSize').notNull().default(0),
    type: text('type').notNull().default('image'),
    thumbnail: text('thumbnail'),
    alt: text('alt'),
    thumbnailTimestamp: integer('thumbnailTimestamp'),
    status: text('status').notNull().default('ready'),
    processingError: text('processingError'),
  },
  (table) => [
    index('Media_name_idx').on(table.name),
    index('Media_organizationId_idx').on(table.organizationId),
    index('Media_type_idx').on(table.type),
    foreignKey({
      name: 'Media_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const socialMediaAgency = pgTable(
  'SocialMediaAgency',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: text('userId').notNull(),
    name: text('name').notNull(),
    logoId: text('logoId'),
    website: text('website'),
    slug: text('slug'),
    facebook: text('facebook'),
    instagram: text('instagram'),
    twitter: text('twitter'),
    linkedIn: text('linkedIn'),
    youtube: text('youtube'),
    tiktok: text('tiktok'),
    otherSocialMedia: text('otherSocialMedia'),
    shortDescription: text('shortDescription').notNull(),
    description: text('description').notNull(),
    approved: boolean('approved').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('SocialMediaAgency_userId_key').on(table.userId),
    index('SocialMediaAgency_userId_idx').on(table.userId),
    index('SocialMediaAgency_deletedAt_idx').on(table.deletedAt),
    index('SocialMediaAgency_id_idx').on(table.id),
    foreignKey({
      name: 'SocialMediaAgency_logoId_fkey',
      columns: [table.logoId],
      foreignColumns: [media.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SocialMediaAgency_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const socialMediaAgencyNiche = pgTable(
  'SocialMediaAgencyNiche',
  { agencyId: text('agencyId').notNull(), niche: text('niche').notNull() },
  (table) => [
    primaryKey({ name: 'SocialMediaAgencyNiche_pkey', columns: [table.agencyId, table.niche] }),
    foreignKey({
      name: 'SocialMediaAgencyNiche_agencyId_fkey',
      columns: [table.agencyId],
      foreignColumns: [socialMediaAgency.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const credits = pgTable(
  'Credits',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    credits: integer('credits').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: text('type').notNull().default('ai_images'),
  },
  (table) => [
    index('Credits_organizationId_idx').on(table.organizationId),
    index('Credits_createdAt_idx').on(table.createdAt),
    foreignKey({
      name: 'Credits_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const clipping = pgTable(
  'Clipping',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    url: text('url').notNull(),
    status: text('status').notNull().default('analysing'),
    error: text('error'),
    title: text('title'),
    thumbnail: text('thumbnail'),
    duration: integer('duration'),
    maxClips: integer('maxClips').notNull().default(5),
    fit: text('fit').notNull().default('blur'),
    integrations: text('integrations').notNull(),
    creditsId: text('creditsId'),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Clipping_organizationId_idx').on(table.organizationId),
    index('Clipping_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Clipping_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const clippingClip = pgTable(
  'ClippingClip',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    clippingId: text('clippingId').notNull(),
    title: text('title').notNull(),
    content: text('content').notNull(),
    start: doublePrecision('start').notNull(),
    end: doublePrecision('end').notNull(),
    trimStart: integer('trimStart'),
    status: text('status').notNull().default('pending'),
    error: text('error'),
    mediaId: text('mediaId'),
    path: text('path'),
    thumbnail: text('thumbnail'),
    draftedAt: timestamp('draftedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ClippingClip_clippingId_idx').on(table.clippingId),
    foreignKey({
      name: 'ClippingClip_clippingId_fkey',
      columns: [table.clippingId],
      foreignColumns: [clipping.id],
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
      .$defaultFn(() => cuid()),
    organizationId: text('organizationId').notNull(),
    subscriptionTier: subscriptionTierEnum('subscriptionTier').notNull(),
    provider: text('provider').notNull().default('stripe'),
    identifier: text('identifier'),
    cancelAt: timestamp('cancelAt', { precision: 3 }),
    period: periodEnum('period').notNull(),
    totalChannels: integer('totalChannels').notNull(),
    isLifetime: boolean('isLifetime').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Subscription_organizationId_key').on(table.organizationId),
    index('Subscription_organizationId_idx').on(table.organizationId),
    index('Subscription_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Subscription_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const customer = pgTable(
  'Customer',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    orgId: text('orgId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Customer_orgId_name_deletedAt_key').on(table.orgId, table.name, table.deletedAt),
    foreignKey({
      name: 'Customer_orgId_fkey',
      columns: [table.orgId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const integration = pgTable(
  'Integration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    internalId: text('internalId').notNull(),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    customName: text('customName'),
    picture: text('picture'),
    providerIdentifier: text('providerIdentifier').notNull(),
    type: text('type').notNull(),
    token: text('token').notNull(),
    disabled: boolean('disabled').notNull().default(false),
    tokenExpiration: timestamp('tokenExpiration', { precision: 3 }),
    refreshToken: text('refreshToken'),
    profile: text('profile'),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
    inBetweenSteps: boolean('inBetweenSteps').notNull().default(false),
    refreshNeeded: boolean('refreshNeeded').notNull().default(false),
    postingTimes: text('postingTimes')
      .notNull()
      .default('[{"time":120}, {"time":400}, {"time":700}]'),
    customInstanceDetails: text('customInstanceDetails'),
    customerId: text('customerId'),
    rootInternalId: text('rootInternalId'),
    additionalSettings: text('additionalSettings').default('[]'),
  },
  (table) => [
    index('Integration_rootInternalId_idx').on(table.rootInternalId),
    index('Integration_organizationId_idx').on(table.organizationId),
    index('Integration_providerIdentifier_idx').on(table.providerIdentifier),
    index('Integration_updatedAt_idx').on(table.updatedAt),
    index('Integration_createdAt_idx').on(table.createdAt),
    index('Integration_deletedAt_idx').on(table.deletedAt),
    index('Integration_customerId_idx').on(table.customerId),
    index('Integration_inBetweenSteps_idx').on(table.inBetweenSteps),
    index('Integration_refreshNeeded_idx').on(table.refreshNeeded),
    index('Integration_disabled_idx').on(table.disabled),
    uniqueIndex('Integration_organizationId_internalId_key').on(
      table.organizationId,
      table.internalId,
    ),
    foreignKey({
      name: 'Integration_customerId_fkey',
      columns: [table.customerId],
      foreignColumns: [customer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Integration_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const signatures = pgTable(
  'Signatures',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    content: text('content').notNull(),
    autoAdd: boolean('autoAdd').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('Signatures_createdAt_idx').on(table.createdAt),
    index('Signatures_organizationId_idx').on(table.organizationId),
    index('Signatures_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Signatures_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const comments = pgTable(
  'Comments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    content: text('content').notNull(),
    organizationId: text('organizationId').notNull(),
    postId: text('postId').notNull(),
    userId: text('userId'),
    displayName: text('displayName'),
    parentId: text('parentId'),
    anchorStart: integer('anchorStart'),
    anchorEnd: integer('anchorEnd'),
    anchorQuote: text('anchorQuote'),
    resolvedAt: timestamp('resolvedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('Comments_createdAt_idx').on(table.createdAt),
    index('Comments_organizationId_idx').on(table.organizationId),
    index('Comments_userId_idx').on(table.userId),
    index('Comments_postId_idx').on(table.postId),
    index('Comments_parentId_idx').on(table.parentId),
    index('Comments_resolvedAt_idx').on(table.resolvedAt),
    index('Comments_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Comments_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'Comments_postId_fkey', columns: [table.postId], foreignColumns: [post.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'Comments_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Comments_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const post = pgTable(
  'Post',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    state: stateEnum('state').notNull().default('QUEUE'),
    publishDate: timestamp('publishDate', { precision: 3 }).notNull(),
    organizationId: text('organizationId').notNull(),
    integrationId: text('integrationId').notNull(),
    content: text('content').notNull(),
    delay: integer('delay').notNull().default(0),
    group: text('group').notNull(),
    batchId: text('batchId'),
    title: text('title'),
    description: text('description'),
    parentPostId: text('parentPostId'),
    releaseId: text('releaseId'),
    releaseURL: text('releaseURL'),
    settings: text('settings'),
    image: text('image'),
    submittedForOrderId: text('submittedForOrderId'),
    submittedForOrganizationId: text('submittedForOrganizationId'),
    approvedSubmitForOrder: approvedSubmitForOrderEnum('approvedSubmitForOrder')
      .notNull()
      .default('NO'),
    creationMethod: creationMethodEnum('creationMethod').notNull().default('UNKNOWN'),
    lastMessageId: text('lastMessageId'),
    intervalInDays: integer('intervalInDays'),
    error: text('error'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('Post_group_idx').on(table.group),
    index('Post_batchId_idx').on(table.batchId),
    index('Post_deletedAt_idx').on(table.deletedAt),
    index('Post_publishDate_idx').on(table.publishDate),
    index('Post_publishDate_state_deletedAt_parentPostId_integrationId_idx').on(
      table.publishDate,
      table.state,
      table.deletedAt,
      table.parentPostId,
      table.integrationId,
      table.organizationId,
      table.creationMethod,
    ),
    index('Post_state_idx').on(table.state),
    index('Post_organizationId_idx').on(table.organizationId),
    index('Post_organizationId_publishDate_idx').on(table.organizationId, table.publishDate),
    index('Post_parentPostId_idx').on(table.parentPostId),
    index('Post_submittedForOrderId_idx').on(table.submittedForOrderId),
    index('Post_intervalInDays_idx').on(table.intervalInDays),
    index('Post_approvedSubmitForOrder_idx').on(table.approvedSubmitForOrder),
    index('Post_creationMethod_idx').on(table.creationMethod),
    index('Post_lastMessageId_idx').on(table.lastMessageId),
    index('Post_createdAt_idx').on(table.createdAt),
    index('Post_updatedAt_idx').on(table.updatedAt),
    index('Post_releaseURL_idx').on(table.releaseURL),
    index('Post_integrationId_idx').on(table.integrationId),
    foreignKey({
      name: 'Post_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Post_lastMessageId_fkey',
      columns: [table.lastMessageId],
      foreignColumns: [messages.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Post_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Post_parentPostId_fkey',
      columns: [table.parentPostId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Post_submittedForOrderId_fkey',
      columns: [table.submittedForOrderId],
      foreignColumns: [orders.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Post_submittedForOrganizationId_fkey',
      columns: [table.submittedForOrganizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const notifications = pgTable(
  'Notifications',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    content: text('content').notNull(),
    link: text('link'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('Notifications_createdAt_idx').on(table.createdAt),
    index('Notifications_organizationId_idx').on(table.organizationId),
    index('Notifications_organizationId_createdAt_deletedAt_idx').on(
      table.organizationId,
      table.createdAt,
      table.deletedAt,
    ),
    index('Notifications_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Notifications_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const messagesGroup = pgTable(
  'MessagesGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    buyerOrganizationId: text('buyerOrganizationId').notNull(),
    buyerId: text('buyerId').notNull(),
    sellerId: text('sellerId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('MessagesGroup_createdAt_idx').on(table.createdAt),
    index('MessagesGroup_updatedAt_idx').on(table.updatedAt),
    index('MessagesGroup_buyerOrganizationId_idx').on(table.buyerOrganizationId),
    uniqueIndex('MessagesGroup_buyerId_sellerId_key').on(table.buyerId, table.sellerId),
    foreignKey({
      name: 'MessagesGroup_buyerId_fkey',
      columns: [table.buyerId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'MessagesGroup_buyerOrganizationId_fkey',
      columns: [table.buyerOrganizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'MessagesGroup_sellerId_fkey',
      columns: [table.sellerId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const payoutProblems = pgTable(
  'PayoutProblems',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    status: text('status').notNull(),
    orderId: text('orderId').notNull(),
    userId: text('userId').notNull(),
    postId: text('postId'),
    amount: integer('amount').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'PayoutProblems_orderId_fkey',
      columns: [table.orderId],
      foreignColumns: [orders.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PayoutProblems_postId_fkey',
      columns: [table.postId],
      foreignColumns: [post.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PayoutProblems_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const orders = pgTable(
  'Orders',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    buyerId: text('buyerId').notNull(),
    sellerId: text('sellerId').notNull(),
    status: orderStatusEnum('status').notNull(),
    messageGroupId: text('messageGroupId').notNull(),
    captureId: text('captureId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Orders_buyerId_idx').on(table.buyerId),
    index('Orders_sellerId_idx').on(table.sellerId),
    index('Orders_updatedAt_idx').on(table.updatedAt),
    index('Orders_createdAt_idx').on(table.createdAt),
    index('Orders_messageGroupId_idx').on(table.messageGroupId),
    foreignKey({ name: 'Orders_buyerId_fkey', columns: [table.buyerId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Orders_messageGroupId_fkey',
      columns: [table.messageGroupId],
      foreignColumns: [messagesGroup.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Orders_sellerId_fkey',
      columns: [table.sellerId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const orderItems = pgTable(
  'OrderItems',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    orderId: text('orderId').notNull(),
    integrationId: text('integrationId').notNull(),
    quantity: integer('quantity').notNull(),
    price: integer('price').notNull(),
  },
  (table) => [
    index('OrderItems_orderId_idx').on(table.orderId),
    index('OrderItems_integrationId_idx').on(table.integrationId),
    foreignKey({
      name: 'OrderItems_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrderItems_orderId_fkey',
      columns: [table.orderId],
      foreignColumns: [orders.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const messages = pgTable(
  'Messages',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    from: fromEnum('from').notNull(),
    content: text('content'),
    groupId: text('groupId').notNull(),
    special: text('special'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('Messages_groupId_idx').on(table.groupId),
    index('Messages_createdAt_idx').on(table.createdAt),
    index('Messages_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Messages_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [messagesGroup.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const plugs = pgTable(
  'Plugs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    plugFunction: text('plugFunction').notNull(),
    data: text('data').notNull(),
    integrationId: text('integrationId').notNull(),
    activated: boolean('activated').notNull().default(true),
  },
  (table) => [
    index('Plugs_organizationId_idx').on(table.organizationId),
    uniqueIndex('Plugs_plugFunction_integrationId_key').on(table.plugFunction, table.integrationId),
    foreignKey({
      name: 'Plugs_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Plugs_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const exisingPlugData = pgTable(
  'ExisingPlugData',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    integrationId: text('integrationId').notNull(),
    methodName: text('methodName').notNull(),
    value: text('value').notNull(),
  },
  (table) => [
    uniqueIndex('ExisingPlugData_integrationId_methodName_value_key').on(
      table.integrationId,
      table.methodName,
      table.value,
    ),
    foreignKey({
      name: 'ExisingPlugData_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const popularPosts = pgTable('PopularPosts', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  category: text('category').notNull(),
  topic: text('topic').notNull(),
  content: text('content').notNull(),
  hook: text('hook').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const integrationsWebhooks = pgTable(
  'IntegrationsWebhooks',
  { integrationId: text('integrationId').notNull(), webhookId: text('webhookId').notNull() },
  (table) => [
    primaryKey({
      name: 'IntegrationsWebhooks_pkey',
      columns: [table.integrationId, table.webhookId],
    }),
    index('IntegrationsWebhooks_integrationId_idx').on(table.integrationId),
    index('IntegrationsWebhooks_webhookId_idx').on(table.webhookId),
    uniqueIndex('IntegrationsWebhooks_integrationId_webhookId_key').on(
      table.integrationId,
      table.webhookId,
    ),
    foreignKey({
      name: 'IntegrationsWebhooks_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'IntegrationsWebhooks_webhookId_fkey',
      columns: [table.webhookId],
      foreignColumns: [webhooks.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const webhooks = pgTable(
  'Webhooks',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    organizationId: text('organizationId').notNull(),
    url: text('url').notNull(),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Webhooks_organizationId_idx').on(table.organizationId),
    index('Webhooks_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Webhooks_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const autoPost = pgTable(
  'AutoPost',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    title: text('title').notNull(),
    content: text('content'),
    onSlot: boolean('onSlot').notNull(),
    syncLast: boolean('syncLast').notNull(),
    url: text('url').notNull(),
    lastUrl: text('lastUrl').notNull(),
    active: boolean('active').notNull(),
    addPicture: boolean('addPicture').notNull(),
    generateContent: boolean('generateContent').notNull(),
    integrations: text('integrations').notNull(),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('AutoPost_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'AutoPost_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const sets = pgTable(
  'Sets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    name: text('name').notNull(),
    content: text('content').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Sets_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'Sets_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const thirdParty = pgTable(
  'ThirdParty',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId').notNull(),
    identifier: text('identifier').notNull(),
    name: text('name').notNull(),
    internalId: text('internalId').notNull(),
    apiKey: text('apiKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    index('ThirdParty_organizationId_idx').on(table.organizationId),
    index('ThirdParty_deletedAt_idx').on(table.deletedAt),
    uniqueIndex('ThirdParty_organizationId_internalId_key').on(
      table.organizationId,
      table.internalId,
    ),
    foreignKey({
      name: 'ThirdParty_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const errors = pgTable(
  'Errors',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    message: text('message').notNull(),
    platform: text('platform').notNull(),
    organizationId: text('organizationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    postId: text('postId').notNull(),
    body: text('body').notNull().default('{}'),
  },
  (table) => [
    index('Errors_organizationId_idx').on(table.organizationId),
    index('Errors_createdAt_idx').on(table.createdAt),
    index('Errors_createdAt_platform_idx').on(table.createdAt, table.platform),
    foreignKey({
      name: 'Errors_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'Errors_postId_fkey', columns: [table.postId], foreignColumns: [post.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const mentions = pgTable(
  'Mentions',
  {
    name: text('name').notNull(),
    username: text('username').notNull(),
    platform: text('platform').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    image: text('image').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'Mentions_pkey',
      columns: [table.name, table.username, table.platform, table.image],
    }),
    index('Mentions_createdAt_idx').on(table.createdAt),
  ],
)

export const mastraMessages = pgTable(
  'mastra_messages',
  {
    id: text('id').primaryKey(),
    thread_id: text('thread_id').notNull(),
    content: text('content').notNull(),
    role: text('role').notNull(),
    type: text('type').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    resourceId: text('resourceId'),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('public_mastra_messages_thread_id_createdat_idx').on(table.thread_id, table.createdAt),
    index('mastra_messages_thread_id_createdat_idx').on(table.thread_id, table.createdAt),
  ],
)

export const mastraResources = pgTable('mastra_resources', {
  id: text('id').primaryKey(),
  workingMemory: text('workingMemory'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraScorers = pgTable(
  'mastra_scorers',
  {
    id: text('id').primaryKey(),
    scorerId: text('scorerId').notNull(),
    traceId: text('traceId'),
    runId: text('runId').notNull(),
    scorer: jsonb('scorer').notNull(),
    preprocessStepResult: jsonb('preprocessStepResult'),
    extractStepResult: jsonb('extractStepResult'),
    analyzeStepResult: jsonb('analyzeStepResult'),
    score: doublePrecision('score').notNull(),
    reason: text('reason'),
    metadata: jsonb('metadata'),
    preprocessPrompt: text('preprocessPrompt'),
    extractPrompt: text('extractPrompt'),
    generateScorePrompt: text('generateScorePrompt'),
    generateReasonPrompt: text('generateReasonPrompt'),
    analyzePrompt: text('analyzePrompt'),
    reasonPrompt: text('reasonPrompt'),
    input: jsonb('input').notNull(),
    output: jsonb('output').notNull(),
    additionalContext: jsonb('additionalContext'),
    runtimeContext: jsonb('runtimeContext'),
    entityType: text('entityType'),
    entity: jsonb('entity'),
    entityId: text('entityId'),
    source: text('source').notNull(),
    resourceId: text('resourceId'),
    threadId: text('threadId'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    spanId: text('spanId'),
    requestContext: jsonb('requestContext'),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    batchId: text('batchId'),
    datasetId: text('datasetId'),
    datasetItemId: text('datasetItemId'),
  },
  (table) => [
    index('public_mastra_scores_trace_id_span_id_created_at_idx').on(
      table.traceId,
      table.spanId,
      table.createdAt,
    ),
    index('mastra_scores_trace_id_span_id_created_at_idx').on(
      table.traceId,
      table.spanId,
      table.createdAt,
    ),
  ],
)

export const mastraThreads = pgTable(
  'mastra_threads',
  {
    id: text('id').primaryKey(),
    resourceId: text('resourceId').notNull(),
    title: text('title').notNull(),
    metadata: text('metadata'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('public_mastra_threads_resourceid_createdat_idx').on(table.resourceId, table.createdAt),
    index('mastra_threads_resourceid_createdat_idx').on(table.resourceId, table.createdAt),
  ],
)

export const mastraTraces = pgTable(
  'mastra_traces',
  {
    id: text('id').primaryKey(),
    parentSpanId: text('parentSpanId'),
    name: text('name').notNull(),
    traceId: text('traceId').notNull(),
    scope: text('scope').notNull(),
    kind: integer('kind').notNull(),
    attributes: jsonb('attributes'),
    status: jsonb('status'),
    events: jsonb('events'),
    links: jsonb('links'),
    other: text('other'),
    startTime: bigint('startTime', { mode: 'bigint' }).notNull(),
    endTime: bigint('endTime', { mode: 'bigint' }).notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('public_mastra_traces_name_starttime_idx').on(table.name, table.startTime)],
)

export const mastraWorkflowSnapshot = pgTable(
  'mastra_workflow_snapshot',
  {
    workflow_name: text('workflow_name').notNull(),
    run_id: text('run_id').notNull(),
    resourceId: text('resourceId'),
    snapshot: text('snapshot').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('mastra_workflow_snapshot_name_createdat_idx').on(table.workflow_name, table.createdAt),
    uniqueIndex('public_mastra_workflow_snapshot_workflow_name_run_id_key').on(
      table.workflow_name,
      table.run_id,
    ),
  ],
)

export const mastraAgentVersions = pgTable('mastra_agent_versions', {
  id: text('id').primaryKey(),
  agentId: text('agentId').notNull(),
  versionNumber: integer('versionNumber').notNull(),
  name: text('name').notNull(),
  description: text('description'),
  instructions: text('instructions').notNull(),
  model: jsonb('model').notNull(),
  tools: jsonb('tools'),
  defaultOptions: jsonb('defaultOptions'),
  workflows: jsonb('workflows'),
  agents: jsonb('agents'),
  integrationTools: jsonb('integrationTools'),
  inputProcessors: jsonb('inputProcessors'),
  outputProcessors: jsonb('outputProcessors'),
  memory: jsonb('memory'),
  scorers: jsonb('scorers'),
  mcpClients: jsonb('mcpClients'),
  requestContextSchema: jsonb('requestContextSchema'),
  workspace: jsonb('workspace'),
  skills: jsonb('skills'),
  skillsFormat: text('skillsFormat'),
  changedFields: jsonb('changedFields'),
  changeMessage: text('changeMessage'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  durable: jsonb('durable'),
  browser: jsonb('browser'),
  toolProviders: jsonb('toolProviders'),
})

export const mastraAgents = pgTable('mastra_agents', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  visibility: text('visibility'),
  favoriteCount: integer('favoriteCount'),
})

export const mastraBackgroundTasks = pgTable(
  'mastra_background_tasks',
  {
    id: text('id').primaryKey(),
    tool_call_id: text('tool_call_id').notNull(),
    tool_name: text('tool_name').notNull(),
    agent_id: text('agent_id').notNull(),
    run_id: text('run_id').notNull(),
    thread_id: text('thread_id'),
    resource_id: text('resource_id'),
    status: text('status').notNull(),
    args: jsonb('args').notNull(),
    result: jsonb('result'),
    error: jsonb('error'),
    suspend_payload: jsonb('suspend_payload'),
    retry_count: integer('retry_count').notNull(),
    max_retries: integer('max_retries').notNull(),
    timeout_ms: integer('timeout_ms').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    startedAt: timestamp('startedAt', { precision: 6 }),
    suspendedAt: timestamp('suspendedAt', { precision: 6 }),
    completedAt: timestamp('completedAt', { precision: 6 }),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    startedAtZ: timestamp('startedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    suspendedAtZ: timestamp('suspendedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAtZ: timestamp('completedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('mastra_bg_tasks_agent_status_idx').on(table.agent_id, table.status),
    index('mastra_bg_tasks_status_created_at_idx').on(table.status, table.createdAt),
    index('mastra_bg_tasks_thread_idx').on(table.thread_id, table.createdAt),
    index('mastra_bg_tasks_tool_call_idx').on(table.tool_call_id),
  ],
)

export const mastraChannelConfig = pgTable('mastra_channel_config', {
  platform: text('platform').primaryKey(),
  data: jsonb('data').notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraChannelInstallations = pgTable(
  'mastra_channel_installations',
  {
    id: text('id').primaryKey(),
    platform: text('platform').notNull(),
    agentId: text('agentId').notNull(),
    status: text('status').notNull(),
    webhookId: text('webhookId'),
    data: jsonb('data').notNull(),
    configHash: text('configHash'),
    error: text('error'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_channel_installations_webhook').on(table.webhookId),
    index('idx_channel_installations_platform_agent').on(table.platform, table.agentId),
  ],
)

export const mastraDatasetItems = pgTable(
  'mastra_dataset_items',
  {
    id: text('id').notNull(),
    datasetId: text('datasetId').notNull(),
    datasetVersion: integer('datasetVersion').notNull(),
    validTo: integer('validTo'),
    isDeleted: boolean('isDeleted').notNull(),
    input: jsonb('input').notNull(),
    groundTruth: jsonb('groundTruth'),
    requestContext: jsonb('requestContext'),
    metadata: jsonb('metadata'),
    source: jsonb('source'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expectedTrajectory: jsonb('expectedTrajectory'),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    toolMocks: jsonb('toolMocks'),
    unmockedToolPolicy: text('unmockedToolPolicy'),
    scorerIds: jsonb('scorerIds'),
    externalId: text('externalId'),
  },
  (table) => [
    primaryKey({ name: 'mastra_dataset_items_pkey', columns: [table.id, table.datasetVersion] }),
    index('idx_dataset_items_dataset_validto').on(table.datasetId, table.validTo),
    index('idx_dataset_items_dataset_validto_deleted').on(
      table.datasetId,
      table.validTo,
      table.isDeleted,
    ),
    index('idx_dataset_items_dataset_version').on(table.datasetId, table.datasetVersion),
    index('idx_dataset_items_external_id_history').on(
      table.datasetId,
      table.externalId,
      table.datasetVersion,
    ),
    index('idx_dataset_items_org_project').on(table.organizationId, table.projectId),
  ],
)

export const mastraDatasetVersions = pgTable(
  'mastra_dataset_versions',
  {
    id: text('id').primaryKey(),
    datasetId: text('datasetId').notNull(),
    version: integer('version').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('idx_dataset_versions_dataset_version').on(table.datasetId, table.version),
    uniqueIndex('idx_dataset_versions_dataset_version_unique').on(table.datasetId, table.version),
  ],
)

export const mastraDatasets = pgTable(
  'mastra_datasets',
  {
    id: text('id').primaryKey(),
    name: text('name').notNull(),
    description: text('description'),
    metadata: jsonb('metadata'),
    inputSchema: jsonb('inputSchema'),
    groundTruthSchema: jsonb('groundTruthSchema'),
    requestContextSchema: jsonb('requestContextSchema'),
    tags: jsonb('tags'),
    targetType: text('targetType'),
    targetIds: jsonb('targetIds'),
    scorerIds: jsonb('scorerIds'),
    version: integer('version').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    candidateKey: text('candidateKey'),
    candidateId: text('candidateId'),
  },
  (table) => [
    index('idx_datasets_candidate').on(table.candidateKey, table.candidateId),
    index('idx_datasets_org_project').on(table.organizationId, table.projectId),
  ],
)

export const mastraExperimentResults = pgTable(
  'mastra_experiment_results',
  {
    id: text('id').primaryKey(),
    experimentId: text('experimentId').notNull(),
    itemId: text('itemId').notNull(),
    itemDatasetVersion: integer('itemDatasetVersion'),
    input: jsonb('input').notNull(),
    output: jsonb('output'),
    groundTruth: jsonb('groundTruth'),
    error: jsonb('error'),
    startedAt: timestamp('startedAt', { precision: 6 }).notNull(),
    completedAt: timestamp('completedAt', { precision: 6 }).notNull(),
    retryCount: integer('retryCount').notNull(),
    traceId: text('traceId'),
    status: text('status'),
    tags: jsonb('tags'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    startedAtZ: timestamp('startedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAtZ: timestamp('completedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    comment: text('comment'),
    toolMockReport: jsonb('toolMockReport'),
    metadata: jsonb('metadata'),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    attempt: integer('attempt'),
  },
  (table) => [
    index('idx_experiment_results_experimentid').on(table.experimentId),
    index('idx_experiment_results_org_project').on(table.organizationId, table.projectId),
    index('idx_experiment_results_tags_gin').on(table.tags),
    uniqueIndex('idx_experiment_results_exp_item_attempt').on(
      table.experimentId,
      table.itemId,
      table.attempt,
    ),
  ],
)

export const mastraExperiments = pgTable(
  'mastra_experiments',
  {
    id: text('id').primaryKey(),
    name: text('name'),
    description: text('description'),
    metadata: jsonb('metadata'),
    datasetId: text('datasetId'),
    datasetVersion: integer('datasetVersion'),
    targetType: text('targetType').notNull(),
    targetId: text('targetId').notNull(),
    status: text('status').notNull(),
    totalItems: integer('totalItems').notNull(),
    succeededCount: integer('succeededCount').notNull(),
    failedCount: integer('failedCount').notNull(),
    skippedCount: integer('skippedCount').notNull(),
    startedAt: timestamp('startedAt', { precision: 6 }),
    completedAt: timestamp('completedAt', { precision: 6 }),
    agentVersion: text('agentVersion'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    startedAtZ: timestamp('startedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAtZ: timestamp('completedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    organizationId: text('organizationId'),
    projectId: text('projectId'),
    provenance: jsonb('provenance'),
    runnerAttestation: jsonb('runnerAttestation'),
    experimentSetId: text('experimentSetId'),
    comparisonId: text('comparisonId'),
    variantId: text('variantId'),
    trialIndex: integer('trialIndex'),
    scorerIds: jsonb('scorerIds'),
  },
  (table) => [
    index('idx_experiments_datasetid').on(table.datasetId),
    index('idx_experiments_grouping').on(
      table.experimentSetId,
      table.comparisonId,
      table.variantId,
      table.trialIndex,
    ),
    index('idx_experiments_org_project').on(table.organizationId, table.projectId),
  ],
)

export const mastraFavorites = pgTable(
  'mastra_favorites',
  {
    userId: text('userId').notNull(),
    entityType: text('entityType').notNull(),
    entityId: text('entityId').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'mastra_favorites_pkey',
      columns: [table.userId, table.entityType, table.entityId],
    }),
    index('idx_favorites_entity').on(table.entityType, table.entityId),
  ],
)

export const mastraKnowledgeActivity = pgTable(
  'mastra_knowledge_activity',
  {
    id: text('id').primaryKey(),
    action: text('action').notNull(),
    recordType: text('recordType').notNull(),
    recordId: text('recordId').notNull(),
    scope: jsonb('scope').notNull(),
    scopeKey: text('scopeKey').notNull(),
    sourceThreadId: text('sourceThreadId'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('idx_knowledge_activity_latest').on(table.id)],
)

export const mastraKnowledgeCursors = pgTable(
  'mastra_knowledge_cursors',
  {
    sourceThreadId: text('sourceThreadId').notNull(),
    agent: text('agent').notNull(),
    lastKnowledgeId: text('lastKnowledgeId').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'mastra_knowledge_cursors_pkey',
      columns: [table.sourceThreadId, table.agent],
    }),
  ],
)

export const mastraKnowledgeMentions = pgTable(
  'mastra_knowledge_mentions',
  {
    sourceType: text('sourceType').notNull(),
    sourceId: text('sourceId').notNull(),
    recordId: text('recordId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'mastra_knowledge_mentions_pkey',
      columns: [table.sourceType, table.sourceId, table.recordId],
    }),
    index('idx_knowledge_mentions_record').on(table.recordId, table.sourceType, table.sourceId),
  ],
)

export const mastraKnowledgeNodes = pgTable(
  'mastra_knowledge_nodes',
  {
    id: text('id').primaryKey(),
    type: text('type').notNull(),
    name: text('name').notNull(),
    canonicalName: text('canonicalName').notNull(),
    kind: text('kind'),
    content: text('content'),
    description: text('description'),
    scope: jsonb('scope').notNull(),
    scopeKey: text('scopeKey').notNull(),
    version: integer('version').notNull(),
    mergedInto: text('mergedInto'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('idx_knowledge_nodes_scope').on(table.scopeKey, table.type),
    uniqueIndex('idx_knowledge_nodes_identity').on(table.type, table.scopeKey, table.canonicalName),
  ],
)

export const mastraKnowledgeRecords = pgTable(
  'mastra_knowledge_records',
  {
    id: text('id').primaryKey(),
    node: text('node').notNull(),
    text: text('text').notNull(),
    scope: jsonb('scope').notNull(),
    scopeKey: text('scopeKey').notNull(),
    sourceThreadId: text('sourceThreadId').notNull(),
    capturedAt: timestamp('capturedAt', { precision: 6 }).notNull(),
    when: timestamp('when', { precision: 6 }),
    maxScope: text('maxScope'),
    metadata: jsonb('metadata'),
    deletedAt: timestamp('deletedAt', { precision: 6 }),
    deletedBy: text('deletedBy'),
    capturedAtZ: timestamp('capturedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    whenZ: timestamp('whenZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deletedAtZ: timestamp('deletedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('idx_knowledge_records_node_latest').on(table.node, table.id),
    index('idx_knowledge_records_thread_latest').on(table.sourceThreadId, table.id),
  ],
)

export const mastraKnowledgeSemanticOutbox = pgTable(
  'mastra_knowledge_semantic_outbox',
  {
    id: text('id').primaryKey(),
    idempotencyKey: text('idempotencyKey').notNull(),
    documentId: text('documentId').notNull(),
    documentType: text('documentType').notNull(),
    operation: text('operation').notNull(),
    scope: jsonb('scope').notNull(),
    scopeKey: text('scopeKey').notNull(),
    status: text('status').notNull(),
    attempts: integer('attempts').notNull(),
    availableAt: timestamp('availableAt', { precision: 6 }).notNull(),
    claimedAt: timestamp('claimedAt', { precision: 6 }),
    claimedBy: text('claimedBy'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    completedAt: timestamp('completedAt', { precision: 6 }),
    availableAtZ: timestamp('availableAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    claimedAtZ: timestamp('claimedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAtZ: timestamp('completedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_knowledge_outbox_idempotency').on(table.idempotencyKey),
    index('idx_knowledge_outbox_claim').on(table.status, table.availableAt, table.createdAt),
  ],
)

export const mastraMcpClientVersions = pgTable(
  'mastra_mcp_client_versions',
  {
    id: text('id').primaryKey(),
    mcpClientId: text('mcpClientId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    servers: jsonb('servers').notNull(),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_mcp_client_versions_client_version').on(
      table.mcpClientId,
      table.versionNumber,
    ),
  ],
)

export const mastraMcpClients = pgTable('mastra_mcp_clients', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraMcpServerVersions = pgTable(
  'mastra_mcp_server_versions',
  {
    id: text('id').primaryKey(),
    mcpServerId: text('mcpServerId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    version: text('version').notNull(),
    description: text('description'),
    instructions: text('instructions'),
    repository: jsonb('repository'),
    releaseDate: text('releaseDate'),
    isLatest: boolean('isLatest'),
    packageCanonical: text('packageCanonical'),
    tools: jsonb('tools'),
    agents: jsonb('agents'),
    workflows: jsonb('workflows'),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_mcp_server_versions_server_version').on(
      table.mcpServerId,
      table.versionNumber,
    ),
  ],
)

export const mastraMcpServers = pgTable('mastra_mcp_servers', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraObservationalMemory = pgTable(
  'mastra_observational_memory',
  {
    id: text('id').primaryKey(),
    lookupKey: text('lookupKey').notNull(),
    scope: text('scope').notNull(),
    resourceId: text('resourceId'),
    threadId: text('threadId'),
    activeObservations: text('activeObservations').notNull(),
    activeObservationsPendingUpdate: text('activeObservationsPendingUpdate'),
    originType: text('originType').notNull(),
    config: text('config').notNull(),
    generationCount: integer('generationCount').notNull(),
    lastObservedAt: timestamp('lastObservedAt', { precision: 6 }),
    lastReflectionAt: timestamp('lastReflectionAt', { precision: 6 }),
    pendingMessageTokens: integer('pendingMessageTokens').notNull(),
    totalTokensObserved: integer('totalTokensObserved').notNull(),
    observationTokenCount: integer('observationTokenCount').notNull(),
    isObserving: boolean('isObserving').notNull(),
    isReflecting: boolean('isReflecting').notNull(),
    observedMessageIds: jsonb('observedMessageIds'),
    observedTimezone: text('observedTimezone'),
    bufferedObservations: text('bufferedObservations'),
    bufferedObservationTokens: integer('bufferedObservationTokens'),
    bufferedMessageIds: jsonb('bufferedMessageIds'),
    bufferedReflection: text('bufferedReflection'),
    bufferedReflectionTokens: integer('bufferedReflectionTokens'),
    bufferedReflectionInputTokens: integer('bufferedReflectionInputTokens'),
    reflectedObservationLineCount: integer('reflectedObservationLineCount'),
    bufferedObservationChunks: jsonb('bufferedObservationChunks'),
    isBufferingObservation: boolean('isBufferingObservation').notNull(),
    isBufferingReflection: boolean('isBufferingReflection').notNull(),
    lastBufferedAtTokens: integer('lastBufferedAtTokens').notNull(),
    lastBufferedAtTime: timestamp('lastBufferedAtTime', { precision: 6 }),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    lastObservedAtZ: timestamp('lastObservedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastReflectionAtZ: timestamp('lastReflectionAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastBufferedAtTimeZ: timestamp('lastBufferedAtTimeZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('idx_om_lookup_key').on(table.lookupKey)],
)

export const mastraPromptBlockVersions = pgTable(
  'mastra_prompt_block_versions',
  {
    id: text('id').primaryKey(),
    blockId: text('blockId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    content: text('content').notNull(),
    rules: jsonb('rules'),
    requestContextSchema: jsonb('requestContextSchema'),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_prompt_block_versions_block_version').on(table.blockId, table.versionNumber),
  ],
)

export const mastraPromptBlocks = pgTable('mastra_prompt_blocks', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraScheduleTriggers = pgTable(
  'mastra_schedule_triggers',
  {
    id: text('id').primaryKey(),
    schedule_id: text('schedule_id').notNull(),
    run_id: text('run_id'),
    scheduled_fire_at: bigint('scheduled_fire_at', { mode: 'bigint' }).notNull(),
    actual_fire_at: bigint('actual_fire_at', { mode: 'bigint' }).notNull(),
    outcome: text('outcome').notNull(),
    error: text('error'),
    trigger_kind: text('trigger_kind').notNull(),
    parent_trigger_id: text('parent_trigger_id'),
    metadata: jsonb('metadata'),
  },
  (table) => [
    index('idx_mastra_schedule_triggers_schedule_fire').on(table.schedule_id, table.actual_fire_at),
  ],
)

export const mastraSchedules = pgTable(
  'mastra_schedules',
  {
    id: text('id').primaryKey(),
    target: jsonb('target').notNull(),
    cron: text('cron').notNull(),
    timezone: text('timezone'),
    status: text('status').notNull(),
    next_fire_at: bigint('next_fire_at', { mode: 'bigint' }).notNull(),
    last_fire_at: bigint('last_fire_at', { mode: 'bigint' }),
    last_run_id: text('last_run_id'),
    created_at: bigint('created_at', { mode: 'bigint' }).notNull(),
    updated_at: bigint('updated_at', { mode: 'bigint' }).notNull(),
    metadata: jsonb('metadata'),
    owner_type: text('owner_type'),
    owner_id: text('owner_id'),
  },
  (table) => [index('idx_mastra_schedules_status_next_fire').on(table.status, table.next_fire_at)],
)

export const mastraScorerDefinitionVersions = pgTable(
  'mastra_scorer_definition_versions',
  {
    id: text('id').primaryKey(),
    scorerDefinitionId: text('scorerDefinitionId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    type: text('type').notNull(),
    model: jsonb('model'),
    instructions: text('instructions'),
    scoreRange: jsonb('scoreRange'),
    presetConfig: jsonb('presetConfig'),
    defaultSampling: jsonb('defaultSampling'),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_scorer_definition_versions_def_version').on(
      table.scorerDefinitionId,
      table.versionNumber,
    ),
  ],
)

export const mastraScorerDefinitions = pgTable('mastra_scorer_definitions', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  organizationId: text('organizationId'),
  projectId: text('projectId'),
})

export const mastraSkillBlobs = pgTable('mastra_skill_blobs', {
  hash: text('hash').primaryKey(),
  content: text('content').notNull(),
  size: integer('size').notNull(),
  mimeType: text('mimeType'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const mastraSkillVersions = pgTable(
  'mastra_skill_versions',
  {
    id: text('id').primaryKey(),
    skillId: text('skillId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    description: text('description').notNull(),
    instructions: text('instructions').notNull(),
    license: text('license'),
    compatibility: jsonb('compatibility'),
    source: jsonb('source'),
    references: jsonb('references'),
    scripts: jsonb('scripts'),
    assets: jsonb('assets'),
    metadata: jsonb('metadata'),
    tree: jsonb('tree'),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    files: jsonb('files'),
  },
  (table) => [
    uniqueIndex('idx_skill_versions_skill_version').on(table.skillId, table.versionNumber),
  ],
)

export const mastraSkills = pgTable('mastra_skills', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  visibility: text('visibility'),
  favoriteCount: integer('favoriteCount'),
})

export const mastraThreadState = pgTable(
  'mastra_thread_state',
  {
    threadId: text('threadId').notNull(),
    type: text('type').notNull(),
    value: jsonb('value').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'mastra_thread_state_pkey', columns: [table.threadId, table.type] }),
  ],
)

export const mastraToolProviderConnections = pgTable(
  'mastra_tool_provider_connections',
  {
    authorId: text('authorId').notNull(),
    providerId: text('providerId').notNull(),
    connectionId: text('connectionId').notNull(),
    toolkit: text('toolkit').notNull(),
    label: text('label'),
    scope: text('scope').notNull(),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'mastra_tool_provider_connections_pkey',
      columns: [table.authorId, table.providerId, table.connectionId],
    }),
    index('idx_tool_provider_connections_author').on(
      table.authorId,
      table.providerId,
      table.toolkit,
    ),
  ],
)

export const mastraWorkflowDefinitions = pgTable(
  'mastra_workflow_definitions',
  {
    id: text('id').primaryKey(),
    description: text('description'),
    metadata: jsonb('metadata'),
    inputSchema: jsonb('inputSchema').notNull(),
    outputSchema: jsonb('outputSchema').notNull(),
    stateSchema: jsonb('stateSchema'),
    requestContextSchema: jsonb('requestContextSchema'),
    graph: jsonb('graph').notNull(),
    schedule: jsonb('schedule'),
    status: text('status').notNull(),
    source: text('source').notNull(),
    authorId: text('authorId'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('idx_workflow_definitions_status').on(table.status)],
)

export const mastraWorkspaceVersions = pgTable(
  'mastra_workspace_versions',
  {
    id: text('id').primaryKey(),
    workspaceId: text('workspaceId').notNull(),
    versionNumber: integer('versionNumber').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    filesystem: jsonb('filesystem'),
    sandbox: jsonb('sandbox'),
    mounts: jsonb('mounts'),
    search: jsonb('search'),
    skills: jsonb('skills'),
    tools: jsonb('tools'),
    autoSync: boolean('autoSync'),
    operationTimeout: integer('operationTimeout'),
    changedFields: jsonb('changedFields'),
    changeMessage: text('changeMessage'),
    createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
    createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('idx_workspace_versions_workspace_version').on(
      table.workspaceId,
      table.versionNumber,
    ),
  ],
)

export const mastraWorkspaces = pgTable('mastra_workspaces', {
  id: text('id').primaryKey(),
  status: text('status').notNull(),
  activeVersionId: text('activeVersionId'),
  authorId: text('authorId'),
  metadata: jsonb('metadata'),
  createdAt: timestamp('createdAt', { precision: 6 }).notNull(),
  updatedAt: timestamp('updatedAt', { precision: 6 }).notNull(),
  createdAtZ: timestamp('createdAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAtZ: timestamp('updatedAtZ', { withTimezone: true, precision: 6 })
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const oAuthApp = pgTable(
  'OAuthApp',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: text('organizationId'),
    name: text('name').notNull(),
    description: text('description'),
    pictureId: text('pictureId'),
    redirectUrl: text('redirectUrl').notNull(),
    redirectUris: text('redirectUris'),
    clientId: text('clientId').notNull(),
    clientSecret: text('clientSecret'),
    dynamic: boolean('dynamic').notNull().default(false),
    tokenEndpointAuthMethod: text('tokenEndpointAuthMethod'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('OAuthApp_clientId_key').on(table.clientId),
    index('OAuthApp_clientId_idx').on(table.clientId),
    index('OAuthApp_organizationId_idx').on(table.organizationId),
    index('OAuthApp_deletedAt_idx').on(table.deletedAt),
    index('OAuthApp_dynamic_idx').on(table.dynamic),
    uniqueIndex('OAuthApp_organizationId_deletedAt_key').on(table.organizationId, table.deletedAt),
    foreignKey({
      name: 'OAuthApp_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthApp_pictureId_fkey',
      columns: [table.pictureId],
      foreignColumns: [media.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const oAuthAuthorization = pgTable(
  'OAuthAuthorization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    oauthAppId: text('oauthAppId').notNull(),
    userId: text('userId').notNull(),
    organizationId: text('organizationId').notNull(),
    accessToken: text('accessToken'),
    authorizationCode: text('authorizationCode'),
    codeExpiresAt: timestamp('codeExpiresAt', { precision: 3 }),
    codeChallenge: text('codeChallenge'),
    codeChallengeMethod: text('codeChallengeMethod'),
    redirectUri: text('redirectUri'),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('OAuthAuthorization_accessToken_idx').on(table.accessToken),
    index('OAuthAuthorization_authorizationCode_idx').on(table.authorizationCode),
    index('OAuthAuthorization_oauthAppId_idx').on(table.oauthAppId),
    index('OAuthAuthorization_userId_idx').on(table.userId),
    index('OAuthAuthorization_organizationId_idx').on(table.organizationId),
    index('OAuthAuthorization_revokedAt_idx').on(table.revokedAt),
    uniqueIndex('OAuthAuthorization_oauthAppId_userId_organizationId_key').on(
      table.oauthAppId,
      table.userId,
      table.organizationId,
    ),
    foreignKey({
      name: 'OAuthAuthorization_oauthAppId_fkey',
      columns: [table.oauthAppId],
      foreignColumns: [oAuthApp.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthAuthorization_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthAuthorization_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const oAuthSelfHostedAuthorization = pgTable(
  'OAuthSelfHostedAuthorization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    oauthAppId: text('oauthAppId').notNull(),
    mcpUrl: text('mcpUrl').notNull(),
    apiKey: text('apiKey').notNull(),
    email: text('email'),
    accessToken: text('accessToken'),
    authorizationCode: text('authorizationCode'),
    codeExpiresAt: timestamp('codeExpiresAt', { precision: 3 }),
    codeChallenge: text('codeChallenge'),
    codeChallengeMethod: text('codeChallengeMethod'),
    redirectUri: text('redirectUri'),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('OAuthSelfHostedAuthorization_accessToken_idx').on(table.accessToken),
    index('OAuthSelfHostedAuthorization_authorizationCode_idx').on(table.authorizationCode),
    index('OAuthSelfHostedAuthorization_oauthAppId_idx').on(table.oauthAppId),
    foreignKey({
      name: 'OAuthSelfHostedAuthorization_oauthAppId_fkey',
      columns: [table.oauthAppId],
      foreignColumns: [oAuthApp.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const announcement = pgTable('Announcement', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  title: text('title').notNull(),
  description: text('description').notNull(),
  color: announcementColorEnum('color').notNull().default('INFO'),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const relations = defineRelations(
  {
    organization,
    tags,
    tagsPosts,
    user,
    usedCodes,
    userOrganization,
    gitHub,
    trending,
    trendingLog,
    itemUser,
    star,
    media,
    socialMediaAgency,
    socialMediaAgencyNiche,
    credits,
    clipping,
    clippingClip,
    subscription,
    customer,
    integration,
    signatures,
    comments,
    post,
    notifications,
    messagesGroup,
    payoutProblems,
    orders,
    orderItems,
    messages,
    plugs,
    exisingPlugData,
    popularPosts,
    integrationsWebhooks,
    webhooks,
    autoPost,
    sets,
    thirdParty,
    errors,
    mentions,
    mastraMessages,
    mastraResources,
    mastraScorers,
    mastraThreads,
    mastraTraces,
    mastraWorkflowSnapshot,
    mastraAgentVersions,
    mastraAgents,
    mastraBackgroundTasks,
    mastraChannelConfig,
    mastraChannelInstallations,
    mastraDatasetItems,
    mastraDatasetVersions,
    mastraDatasets,
    mastraExperimentResults,
    mastraExperiments,
    mastraFavorites,
    mastraKnowledgeActivity,
    mastraKnowledgeCursors,
    mastraKnowledgeMentions,
    mastraKnowledgeNodes,
    mastraKnowledgeRecords,
    mastraKnowledgeSemanticOutbox,
    mastraMcpClientVersions,
    mastraMcpClients,
    mastraMcpServerVersions,
    mastraMcpServers,
    mastraObservationalMemory,
    mastraPromptBlockVersions,
    mastraPromptBlocks,
    mastraScheduleTriggers,
    mastraSchedules,
    mastraScorerDefinitionVersions,
    mastraScorerDefinitions,
    mastraSkillBlobs,
    mastraSkillVersions,
    mastraSkills,
    mastraThreadState,
    mastraToolProviderConnections,
    mastraWorkflowDefinitions,
    mastraWorkspaceVersions,
    mastraWorkspaces,
    oAuthApp,
    oAuthAuthorization,
    oAuthSelfHostedAuthorization,
    announcement,
  },
  (r) => ({
    organization: {
      autoPost: r.many.autoPost(),
      Comments: r.many.comments(),
      credits: r.many.credits(),
      clippings: r.many.clipping(),
      customers: r.many.customer(),
      errors: r.many.errors(),
      github: r.many.gitHub(),
      Integration: r.many.integration(),
      media: r.many.media(),
      buyerOrganization: r.many.messagesGroup(),
      notifications: r.many.notifications(),
      plugs: r.many.plugs(),
      post: r.many.post({ alias: 'organization' }),
      submittedPost: r.many.post({ alias: 'submittedForOrg' }),
      sets: r.many.sets(),
      signatures: r.many.signatures(),
      subscription: r.one.subscription({
        from: r.organization.id,
        to: r.subscription.organizationId,
      }),
      tags: r.many.tags(),
      thirdParty: r.many.thirdParty(),
      usedCodes: r.many.usedCodes(),
      users: r.many.userOrganization(),
      webhooks: r.many.webhooks(),
      oauthApp: r.many.oAuthApp(),
      oauthAuthorizations: r.many.oAuthAuthorization(),
    },
    tags: {
      organization: r.one.organization({
        from: r.tags.orgId,
        to: r.organization.id,
        optional: false,
      }),
      posts: r.many.tagsPosts(),
    },
    tagsPosts: {
      post: r.one.post({ from: r.tagsPosts.postId, to: r.post.id, optional: false }),
      tag: r.one.tags({ from: r.tagsPosts.tagId, to: r.tags.id, optional: false }),
    },
    user: {
      comments: r.many.comments(),
      items: r.many.itemUser(),
      groupBuyer: r.many.messagesGroup({ alias: 'groupBuyer' }),
      groupSeller: r.many.messagesGroup({ alias: 'groupSeller' }),
      orderBuyer: r.many.orders({ alias: 'orderBuyer' }),
      orderSeller: r.many.orders({ alias: 'orderSeller' }),
      payoutProblems: r.many.payoutProblems(),
      agencies: r.one.socialMediaAgency({ from: r.user.id, to: r.socialMediaAgency.userId }),
      picture: r.one.media({ from: r.user.pictureId, to: r.media.id }),
      organizations: r.many.userOrganization(),
      oauthAuthorizations: r.many.oAuthAuthorization(),
    },
    usedCodes: {
      organization: r.one.organization({
        from: r.usedCodes.orgId,
        to: r.organization.id,
        optional: false,
      }),
    },
    userOrganization: {
      organization: r.one.organization({
        from: r.userOrganization.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      user: r.one.user({ from: r.userOrganization.userId, to: r.user.id, optional: false }),
    },
    gitHub: {
      organization: r.one.organization({
        from: r.gitHub.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    itemUser: { user: r.one.user({ from: r.itemUser.userId, to: r.user.id, optional: false }) },
    media: {
      organization: r.one.organization({
        from: r.media.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      agencies: r.many.socialMediaAgency(),
      userPicture: r.many.user(),
      oauthApps: r.many.oAuthApp(),
    },
    socialMediaAgency: {
      logo: r.one.media({ from: r.socialMediaAgency.logoId, to: r.media.id }),
      user: r.one.user({ from: r.socialMediaAgency.userId, to: r.user.id, optional: false }),
      niches: r.many.socialMediaAgencyNiche(),
    },
    socialMediaAgencyNiche: {
      agency: r.one.socialMediaAgency({
        from: r.socialMediaAgencyNiche.agencyId,
        to: r.socialMediaAgency.id,
        optional: false,
      }),
    },
    credits: {
      organization: r.one.organization({
        from: r.credits.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    clipping: {
      organization: r.one.organization({
        from: r.clipping.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      clips: r.many.clippingClip(),
    },
    clippingClip: {
      clipping: r.one.clipping({
        from: r.clippingClip.clippingId,
        to: r.clipping.id,
        optional: false,
      }),
    },
    subscription: {
      organization: r.one.organization({
        from: r.subscription.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    customer: {
      organization: r.one.organization({
        from: r.customer.orgId,
        to: r.organization.id,
        optional: false,
      }),
      integrations: r.many.integration(),
    },
    integration: {
      exisingPlugData: r.many.exisingPlugData(),
      customer: r.one.customer({ from: r.integration.customerId, to: r.customer.id }),
      organization: r.one.organization({
        from: r.integration.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      webhooks: r.many.integrationsWebhooks(),
      orderItems: r.many.orderItems(),
      plugs: r.many.plugs(),
      posts: r.many.post(),
    },
    signatures: {
      organization: r.one.organization({
        from: r.signatures.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    comments: {
      organization: r.one.organization({
        from: r.comments.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      post: r.one.post({ from: r.comments.postId, to: r.post.id, optional: false }),
      user: r.one.user({ from: r.comments.userId, to: r.user.id }),
      parent: r.one.comments({
        from: r.comments.parentId,
        to: r.comments.id,
        alias: 'CommentReplies',
      }),
      replies: r.many.comments({ alias: 'CommentReplies' }),
    },
    post: {
      comments: r.many.comments(),
      errors: r.many.errors(),
      payoutProblems: r.many.payoutProblems(),
      integration: r.one.integration({
        from: r.post.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      lastMessage: r.one.messages({ from: r.post.lastMessageId, to: r.messages.id }),
      organization: r.one.organization({
        from: r.post.organizationId,
        to: r.organization.id,
        optional: false,
        alias: 'organization',
      }),
      parentPost: r.one.post({ from: r.post.parentPostId, to: r.post.id, alias: 'parentPostId' }),
      childrenPost: r.many.post({ alias: 'parentPostId' }),
      submittedForOrder: r.one.orders({ from: r.post.submittedForOrderId, to: r.orders.id }),
      submittedForOrganization: r.one.organization({
        from: r.post.submittedForOrganizationId,
        to: r.organization.id,
        alias: 'submittedForOrg',
      }),
      tags: r.many.tagsPosts(),
    },
    notifications: {
      organization: r.one.organization({
        from: r.notifications.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    messagesGroup: {
      messages: r.many.messages(),
      buyer: r.one.user({
        from: r.messagesGroup.buyerId,
        to: r.user.id,
        optional: false,
        alias: 'groupBuyer',
      }),
      buyerOrganization: r.one.organization({
        from: r.messagesGroup.buyerOrganizationId,
        to: r.organization.id,
        optional: false,
      }),
      seller: r.one.user({
        from: r.messagesGroup.sellerId,
        to: r.user.id,
        optional: false,
        alias: 'groupSeller',
      }),
      orders: r.many.orders(),
    },
    payoutProblems: {
      order: r.one.orders({ from: r.payoutProblems.orderId, to: r.orders.id, optional: false }),
      post: r.one.post({ from: r.payoutProblems.postId, to: r.post.id }),
      user: r.one.user({ from: r.payoutProblems.userId, to: r.user.id, optional: false }),
    },
    orders: {
      ordersItems: r.many.orderItems(),
      buyer: r.one.user({
        from: r.orders.buyerId,
        to: r.user.id,
        optional: false,
        alias: 'orderBuyer',
      }),
      messageGroup: r.one.messagesGroup({
        from: r.orders.messageGroupId,
        to: r.messagesGroup.id,
        optional: false,
      }),
      seller: r.one.user({
        from: r.orders.sellerId,
        to: r.user.id,
        optional: false,
        alias: 'orderSeller',
      }),
      payoutProblems: r.many.payoutProblems(),
      posts: r.many.post(),
    },
    orderItems: {
      integration: r.one.integration({
        from: r.orderItems.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      order: r.one.orders({ from: r.orderItems.orderId, to: r.orders.id, optional: false }),
    },
    messages: {
      group: r.one.messagesGroup({
        from: r.messages.groupId,
        to: r.messagesGroup.id,
        optional: false,
      }),
      posts: r.many.post(),
    },
    plugs: {
      integration: r.one.integration({
        from: r.plugs.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      organization: r.one.organization({
        from: r.plugs.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    exisingPlugData: {
      integration: r.one.integration({
        from: r.exisingPlugData.integrationId,
        to: r.integration.id,
        optional: false,
      }),
    },
    integrationsWebhooks: {
      integration: r.one.integration({
        from: r.integrationsWebhooks.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      webhook: r.one.webhooks({
        from: r.integrationsWebhooks.webhookId,
        to: r.webhooks.id,
        optional: false,
      }),
    },
    webhooks: {
      integrations: r.many.integrationsWebhooks(),
      organization: r.one.organization({
        from: r.webhooks.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    autoPost: {
      organization: r.one.organization({
        from: r.autoPost.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    sets: {
      organization: r.one.organization({
        from: r.sets.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    thirdParty: {
      organization: r.one.organization({
        from: r.thirdParty.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    errors: {
      organization: r.one.organization({
        from: r.errors.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      post: r.one.post({ from: r.errors.postId, to: r.post.id, optional: false }),
    },
    oAuthApp: {
      organization: r.one.organization({ from: r.oAuthApp.organizationId, to: r.organization.id }),
      picture: r.one.media({ from: r.oAuthApp.pictureId, to: r.media.id }),
      authorizations: r.many.oAuthAuthorization(),
      selfHostedAuthorizations: r.many.oAuthSelfHostedAuthorization(),
    },
    oAuthAuthorization: {
      oauthApp: r.one.oAuthApp({
        from: r.oAuthAuthorization.oauthAppId,
        to: r.oAuthApp.id,
        optional: false,
      }),
      user: r.one.user({ from: r.oAuthAuthorization.userId, to: r.user.id, optional: false }),
      organization: r.one.organization({
        from: r.oAuthAuthorization.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    oAuthSelfHostedAuthorization: {
      oauthApp: r.one.oAuthApp({
        from: r.oAuthSelfHostedAuthorization.oauthAppId,
        to: r.oAuthApp.id,
        optional: false,
      }),
    },
  }),
)
