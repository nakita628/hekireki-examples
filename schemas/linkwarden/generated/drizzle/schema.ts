import {
  type PgTableExtraConfigValue,
  boolean,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
  uuid,
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

export const themeEnum = pgEnum('Theme', ['dark', 'light', 'auto'])

export const aiTaggingMethodEnum = pgEnum('AiTaggingMethod', [
  'DISABLED',
  'GENERATE',
  'EXISTING',
  'PREDEFINED',
])

export const linksRouteToEnum = pgEnum('LinksRouteTo', [
  'ORIGINAL',
  'PDF',
  'READABLE',
  'MONOLITH',
  'SCREENSHOT',
  'DETAILS',
])

export const subscriptionProviderEnum = pgEnum('SubscriptionProvider', [
  'STRIPE',
  'APPLE',
  'GOOGLE',
])

export const dashboardSectionTypeEnum = pgEnum('DashboardSectionType', [
  'STATS',
  'RECENT_LINKS',
  'PINNED_LINKS',
  'COLLECTION',
])

export const appMigrationStatusEnum = pgEnum('AppMigrationStatus', ['APPLIED', 'PENDING', 'FAILED'])

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: integer('userId').notNull(),
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

export const user = pgTable(
  'User',
  {
    id: serial('id').primaryKey(),
    uuid: uuid('uuid')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name'),
    username: text('username'),
    email: text('email'),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
    unverifiedNewEmail: text('unverifiedNewEmail'),
    image: text('image'),
    password: text('password'),
    locale: text('locale').notNull().default('en'),
    parentSubscriptionId: integer('parentSubscriptionId'),
    collectionOrder: integer('collectionOrder').array().default([]),
    linksRouteTo: linksRouteToEnum('linksRouteTo').notNull().default('ORIGINAL'),
    aiTaggingMethod: aiTaggingMethodEnum('aiTaggingMethod').notNull().default('DISABLED'),
    aiPredefinedTags: text('aiPredefinedTags').array().default([]),
    aiTagExistingLinks: boolean('aiTagExistingLinks').notNull().default(false),
    theme: themeEnum('theme').notNull().default('dark'),
    dismissedAnnouncementId: text('dismissedAnnouncementId'),
    readableFontFamily: text('readableFontFamily').default('sans-serif'),
    readableFontSize: text('readableFontSize').default('20px'),
    readableLineHeight: text('readableLineHeight').default('1.8'),
    readableLineWidth: text('readableLineWidth').default('normal'),
    preventDuplicateLinks: boolean('preventDuplicateLinks').notNull().default(false),
    archiveAsScreenshot: boolean('archiveAsScreenshot').notNull().default(true),
    archiveAsMonolith: boolean('archiveAsMonolith').notNull().default(true),
    archiveAsPDF: boolean('archiveAsPDF').notNull().default(true),
    archiveAsReadable: boolean('archiveAsReadable').notNull().default(true),
    archiveAsWaybackMachine: boolean('archiveAsWaybackMachine').notNull().default(false),
    referredBy: text('referredBy'),
    lastPickedAt: timestamp('lastPickedAt', { precision: 3 }),
    acceptPromotionalEmails: boolean('acceptPromotionalEmails').notNull().default(false),
    trialEndEmailSent: boolean('trialEndEmailSent').notNull().default(false),
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
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('User_uuid_key').on(table.uuid),
    uniqueIndex('User_username_key').on(table.username),
    uniqueIndex('User_email_key').on(table.email),
    foreignKey({
      name: 'User_parentSubscriptionId_fkey',
      columns: [table.parentSubscriptionId],
      foreignColumns: [subscription.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
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
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const passwordResetToken = pgTable(
  'PasswordResetToken',
  {
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
  (table) => [uniqueIndex('PasswordResetToken_token_key').on(table.token)],
)

export const collection = pgTable(
  'Collection',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    description: text('description').notNull().default(''),
    icon: text('icon'),
    iconWeight: text('iconWeight'),
    color: text('color').notNull().default('#0ea5e9'),
    parentId: integer('parentId'),
    isPublic: boolean('isPublic').notNull().default(false),
    ownerId: integer('ownerId').notNull(),
    createdById: integer('createdById'),
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
    index('Collection_ownerId_idx').on(table.ownerId),
    foreignKey({
      name: 'Collection_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Collection_ownerId_fkey',
      columns: [table.ownerId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Collection_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const usersAndCollections = pgTable(
  'UsersAndCollections',
  {
    userId: integer('userId').notNull(),
    collectionId: integer('collectionId').notNull(),
    canCreate: boolean('canCreate').notNull(),
    canUpdate: boolean('canUpdate').notNull(),
    canDelete: boolean('canDelete').notNull(),
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
    primaryKey({ name: 'UsersAndCollections_pkey', columns: [table.userId, table.collectionId] }),
    index('UsersAndCollections_userId_idx').on(table.userId),
    foreignKey({
      name: 'UsersAndCollections_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UsersAndCollections_collectionId_fkey',
      columns: [table.collectionId],
      foreignColumns: [collection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const link = pgTable(
  'Link',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull().default(''),
    type: text('type').notNull().default('url'),
    description: text('description').notNull().default(''),
    createdById: integer('createdById'),
    collectionId: integer('collectionId').notNull(),
    icon: text('icon'),
    iconWeight: text('iconWeight'),
    color: text('color'),
    url: text('url'),
    textContent: text('textContent'),
    preview: text('preview'),
    image: text('image'),
    pdf: text('pdf'),
    readable: text('readable'),
    monolith: text('monolith'),
    clientSide: boolean('clientSide').notNull().default(false),
    aiTagged: boolean('aiTagged').notNull().default(false),
    metaDescription: text('metaDescription'),
    indexVersion: integer('indexVersion'),
    lastPreserved: timestamp('lastPreserved', { precision: 3 }),
    importDate: timestamp('importDate', { precision: 3 }),
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
    index('Link_collectionId_idx').on(table.collectionId),
    foreignKey({
      name: 'Link_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_collectionId_fkey',
      columns: [table.collectionId],
      foreignColumns: [collection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tag = pgTable(
  'Tag',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    ownerId: integer('ownerId').notNull(),
    archiveAsScreenshot: boolean('archiveAsScreenshot'),
    archiveAsMonolith: boolean('archiveAsMonolith'),
    archiveAsPDF: boolean('archiveAsPDF'),
    archiveAsReadable: boolean('archiveAsReadable'),
    archiveAsWaybackMachine: boolean('archiveAsWaybackMachine'),
    aiTag: boolean('aiTag'),
    aiGenerated: boolean('aiGenerated').notNull().default(false),
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
    index('Tag_ownerId_idx').on(table.ownerId),
    uniqueIndex('Tag_name_ownerId_key').on(table.name, table.ownerId),
    foreignKey({ name: 'Tag_ownerId_fkey', columns: [table.ownerId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscription = pgTable(
  'Subscription',
  {
    id: serial('id').primaryKey(),
    active: boolean('active').notNull(),
    provider: subscriptionProviderEnum('provider').notNull(),
    currentPeriodStart: timestamp('currentPeriodStart', { precision: 3 }).notNull(),
    currentPeriodEnd: timestamp('currentPeriodEnd', { precision: 3 }).notNull(),
    quantity: integer('quantity').notNull().default(1),
    storeOriginalTransactionId: text('storeOriginalTransactionId'),
    storeProductId: text('storeProductId'),
    googlePurchaseToken: text('googlePurchaseToken'),
    storeMetadata: jsonb('storeMetadata'),
    stripeSubscriptionId: text('stripeSubscriptionId'),
    userId: integer('userId').notNull(),
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
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('Subscription_storeOriginalTransactionId_key').on(table.storeOriginalTransactionId),
    uniqueIndex('Subscription_googlePurchaseToken_key').on(table.googlePurchaseToken),
    uniqueIndex('Subscription_stripeSubscriptionId_key').on(table.stripeSubscriptionId),
    uniqueIndex('Subscription_userId_key').on(table.userId),
    foreignKey({
      name: 'Subscription_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accessToken = pgTable(
  'AccessToken',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    userId: integer('userId').notNull(),
    token: text('token').notNull(),
    revoked: boolean('revoked').notNull().default(false),
    isSession: boolean('isSession').notNull().default(false),
    expires: timestamp('expires', { precision: 3 }).notNull(),
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
    uniqueIndex('AccessToken_token_key').on(table.token),
    foreignKey({
      name: 'AccessToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const rssSubscription = pgTable(
  'RssSubscription',
  {
    id: serial('id').primaryKey(),
    url: text('url').notNull(),
    name: text('name').notNull(),
    lastBuildDate: timestamp('lastBuildDate', { precision: 3 }),
    collectionId: integer('collectionId').notNull(),
    ownerId: integer('ownerId').notNull(),
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
      name: 'RssSubscription_collectionId_fkey',
      columns: [table.collectionId],
      foreignColumns: [collection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const highlight = pgTable(
  'Highlight',
  {
    id: serial('id').primaryKey(),
    color: text('color').notNull(),
    comment: text('comment'),
    linkId: integer('linkId').notNull(),
    userId: integer('userId').notNull(),
    startOffset: integer('startOffset').notNull(),
    endOffset: integer('endOffset').notNull(),
    text: text('text').notNull(),
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
      name: 'Highlight_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Highlight_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dashboardSection = pgTable(
  'DashboardSection',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    collectionId: integer('collectionId'),
    type: dashboardSectionTypeEnum('type').notNull(),
    order: integer('order').notNull(),
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
    uniqueIndex('DashboardSection_userId_collectionId_key').on(table.userId, table.collectionId),
    foreignKey({
      name: 'DashboardSection_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DashboardSection_collectionId_fkey',
      columns: [table.collectionId],
      foreignColumns: [collection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const appMigration = pgTable(
  'AppMigration',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    status: appMigrationStatusEnum('status').notNull(),
    finishedAt: timestamp('finishedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('AppMigration_name_key').on(table.name)],
)

export const pinnedLinks = pgTable(
  '_PinnedLinks',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    primaryKey({ name: '_PinnedLinks_AB_pkey', columns: [table.A, table.B] }),
    index('_PinnedLinks_B_index').on(table.B),
    foreignKey({ name: '_PinnedLinks_A_fkey', columns: [table.A], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_PinnedLinks_B_fkey', columns: [table.B], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const linkToTag = pgTable(
  '_LinkToTag',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    primaryKey({ name: '_LinkToTag_AB_pkey', columns: [table.A, table.B] }),
    index('_LinkToTag_B_index').on(table.B),
    foreignKey({ name: '_LinkToTag_A_fkey', columns: [table.A], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_LinkToTag_B_fkey', columns: [table.B], foreignColumns: [tag.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    account,
    user,
    verificationToken,
    passwordResetToken,
    collection,
    usersAndCollections,
    link,
    tag,
    subscription,
    accessToken,
    rssSubscription,
    highlight,
    dashboardSection,
    appMigration,
    pinnedLinks,
    linkToTag,
  },
  (r) => ({
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    user: {
      parentSubscription: r.one.subscription({
        from: r.user.parentSubscriptionId,
        to: r.subscription.id,
        alias: 'ChildUsers',
      }),
      accounts: r.many.account(),
      collections: r.many.collection({ alias: 'CollectionToUser' }),
      tags: r.many.tag(),
      pinnedLinks: r.many.link({
        from: r.user.id.through(r.pinnedLinks.B),
        to: r.link.id.through(r.pinnedLinks.A),
      }),
      createdLinks: r.many.link({ alias: 'CreatedLinks' }),
      createdCollections: r.many.collection({ alias: 'CreatedCollections' }),
      highlights: r.many.highlight(),
      collectionsJoined: r.many.usersAndCollections(),
      accessTokens: r.many.accessToken(),
      subscriptions: r.one.subscription({
        from: r.user.id,
        to: r.subscription.userId,
        alias: 'SubscriptionToUser',
      }),
      dashboardSections: r.many.dashboardSection(),
    },
    collection: {
      parent: r.one.collection({
        from: r.collection.parentId,
        to: r.collection.id,
        alias: 'SubCollections',
      }),
      subCollections: r.many.collection({ alias: 'SubCollections' }),
      owner: r.one.user({
        from: r.collection.ownerId,
        to: r.user.id,
        optional: false,
        alias: 'CollectionToUser',
      }),
      members: r.many.usersAndCollections(),
      createdBy: r.one.user({
        from: r.collection.createdById,
        to: r.user.id,
        alias: 'CreatedCollections',
      }),
      links: r.many.link(),
      rssSubscriptions: r.many.rssSubscription(),
      DashboardSection: r.many.dashboardSection(),
    },
    usersAndCollections: {
      user: r.one.user({ from: r.usersAndCollections.userId, to: r.user.id, optional: false }),
      collection: r.one.collection({
        from: r.usersAndCollections.collectionId,
        to: r.collection.id,
        optional: false,
      }),
    },
    link: {
      highlight: r.many.highlight(),
      pinnedBy: r.many.user({
        from: r.link.id.through(r.pinnedLinks.A),
        to: r.user.id.through(r.pinnedLinks.B),
      }),
      createdBy: r.one.user({ from: r.link.createdById, to: r.user.id, alias: 'CreatedLinks' }),
      collection: r.one.collection({
        from: r.link.collectionId,
        to: r.collection.id,
        optional: false,
      }),
      tags: r.many.tag({
        from: r.link.id.through(r.linkToTag.A),
        to: r.tag.id.through(r.linkToTag.B),
      }),
    },
    tag: {
      links: r.many.link({
        from: r.tag.id.through(r.linkToTag.B),
        to: r.link.id.through(r.linkToTag.A),
      }),
      owner: r.one.user({ from: r.tag.ownerId, to: r.user.id, optional: false }),
    },
    subscription: {
      user: r.one.user({
        from: r.subscription.userId,
        to: r.user.id,
        optional: false,
        alias: 'SubscriptionToUser',
      }),
      childUsers: r.many.user({ alias: 'ChildUsers' }),
    },
    accessToken: {
      user: r.one.user({ from: r.accessToken.userId, to: r.user.id, optional: false }),
    },
    rssSubscription: {
      collection: r.one.collection({
        from: r.rssSubscription.collectionId,
        to: r.collection.id,
        optional: false,
      }),
    },
    highlight: {
      link: r.one.link({ from: r.highlight.linkId, to: r.link.id, optional: false }),
      user: r.one.user({ from: r.highlight.userId, to: r.user.id, optional: false }),
    },
    dashboardSection: {
      user: r.one.user({ from: r.dashboardSection.userId, to: r.user.id, optional: false }),
      collection: r.one.collection({ from: r.dashboardSection.collectionId, to: r.collection.id }),
    },
    pinnedLinks: {
      link: r.one.link({ from: r.pinnedLinks.A, to: r.link.id, optional: false }),
      user: r.one.user({ from: r.pinnedLinks.B, to: r.user.id, optional: false }),
    },
    linkToTag: {
      link: r.one.link({ from: r.linkToTag.A, to: r.link.id, optional: false }),
      tag: r.one.tag({ from: r.linkToTag.B, to: r.tag.id, optional: false }),
    },
  }),
)
