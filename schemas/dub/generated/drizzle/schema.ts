import {
  bigint,
  boolean,
  datetime,
  foreignKey,
  index,
  int,
  json,
  longtext,
  mysqlEnum,
  mysqlTable,
  text,
  timestamp,
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

export const customer = mysqlTable(
  'Customer',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }),
    email: varchar('email', { length: 191 }),
    normalizedEmail: varchar('normalizedEmail', { length: 191 }),
    avatar: text('avatar'),
    externalId: varchar('externalId', { length: 191 }),
    stripeCustomerId: varchar('stripeCustomerId', { length: 191 }),
    linkId: varchar('linkId', { length: 191 }),
    clickId: varchar('clickId', { length: 191 }),
    clickedAt: datetime('clickedAt', { fsp: 3 }),
    country: varchar('country', { length: 191 }),
    sales: int('sales').notNull().default(0),
    saleAmount: bigint('saleAmount', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    firstSaleAt: datetime('firstSaleAt', { fsp: 3 }),
    subscriptionCanceledAt: datetime('subscriptionCanceledAt', { fsp: 3 }),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    projectConnectId: varchar('projectConnectId', { length: 191 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Customer_stripeCustomerId_key').on(table.stripeCustomerId),
    index('Customer_projectId_email_idx').on(table.projectId, table.email),
    index('Customer_projectId_normalizedEmail_idx').on(table.projectId, table.normalizedEmail),
    index('Customer_projectId_createdAt_idx').on(table.projectId, table.createdAt),
    index('Customer_projectId_saleAmount_idx').on(table.projectId, table.saleAmount),
    index('Customer_projectId_firstSaleAt_idx').on(table.projectId, table.firstSaleAt),
    index('Customer_projectId_subscriptionCanceledAt_idx').on(
      table.projectId,
      table.subscriptionCanceledAt,
    ),
    index('Customer_linkId_idx').on(table.linkId),
    index('Customer_country_idx').on(table.country),
    uniqueIndex('Customer_projectId_externalId_key').on(table.projectId, table.externalId),
    index('Customer_email_name_idx').on(table.email, table.name),
    foreignKey({
      name: 'Customer_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'Customer_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dashboard = mysqlTable(
  'Dashboard',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    linkId: varchar('linkId', { length: 191 }),
    folderId: varchar('folderId', { length: 191 }),
    projectId: varchar('projectId', { length: 191 }),
    userId: varchar('userId', { length: 191 }),
    doIndex: boolean('doIndex').notNull().default(false),
    password: varchar('password', { length: 191 }),
    showConversions: boolean('showConversions').notNull().default(false),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Dashboard_linkId_key').on(table.linkId),
    uniqueIndex('Dashboard_folderId_key').on(table.folderId),
    index('Dashboard_projectId_idx').on(table.projectId),
    index('Dashboard_userId_idx').on(table.userId),
    foreignKey({
      name: 'Dashboard_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dashboard_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dashboard_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dashboard_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const domain = mysqlTable(
  'Domain',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    slug: varchar('slug', { length: 191 }).notNull(),
    verified: boolean('verified').notNull().default(false),
    placeholder: varchar('placeholder', { length: 191 }),
    expiredUrl: longtext('expiredUrl'),
    notFoundUrl: longtext('notFoundUrl'),
    primary: boolean('primary').notNull().default(false),
    archived: boolean('archived').notNull().default(false),
    lastChecked: datetime('lastChecked', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    logo: varchar('logo', { length: 191 }),
    appleAppSiteAssociation: json('appleAppSiteAssociation'),
    assetLinks: json('assetLinks'),
    deepviewData: json('deepviewData').default({}),
    linkRetentionDays: int('linkRetentionDays'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    projectId: varchar('projectId', { length: 191 }),
  },
  (table) => [
    unique('Domain_slug_key').on(table.slug),
    index('Domain_projectId_idx').on(table.projectId),
    index('Domain_lastChecked_idx').on(table.lastChecked),
    foreignKey({
      name: 'Domain_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const registeredDomain = mysqlTable(
  'RegisteredDomain',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    slug: varchar('slug', { length: 191 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    domainId: varchar('domainId', { length: 191 }),
    autoRenewalDisabledAt: datetime('autoRenewalDisabledAt', { fsp: 3 }),
    renewalFee: int('renewalFee').notNull().default(1200),
    expiresAt: datetime('expiresAt', { fsp: 3 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('RegisteredDomain_domainId_key').on(table.domainId),
    index('RegisteredDomain_projectId_idx').on(table.projectId),
    index('RegisteredDomain_expiresAt_idx').on(table.expiresAt),
    foreignKey({
      name: 'RegisteredDomain_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RegisteredDomain_domainId_fkey',
      columns: [table.domainId],
      foreignColumns: [domain.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const defaultDomains = mysqlTable(
  'DefaultDomains',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    dubsh: boolean('dubsh').notNull().default(true),
    chatgpt: boolean('chatgpt').notNull().default(true),
    sptifi: boolean('sptifi').notNull().default(true),
    gitnew: boolean('gitnew').notNull().default(true),
    callink: boolean('callink').notNull().default(true),
    amznid: boolean('amznid').notNull().default(true),
    ggllink: boolean('ggllink').notNull().default(true),
    figpage: boolean('figpage').notNull().default(true),
    loooooooong: boolean('loooooooong').notNull().default(false),
    projectId: varchar('projectId', { length: 191 }).notNull(),
  },
  (table) => [
    uniqueIndex('DefaultDomains_projectId_key').on(table.projectId),
    foreignKey({
      name: 'DefaultDomains_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const folder = mysqlTable(
  'Folder',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }).notNull(),
    description: text('description'),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    type: mysqlEnum('type', ['default', 'mega']).notNull().default('default'),
    accessLevel: mysqlEnum('accessLevel', ['read', 'write']),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Folder_projectId_idx').on(table.projectId),
    uniqueIndex('Folder_name_projectId_key').on(table.name, table.projectId),
    foreignKey({
      name: 'Folder_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const folderUser = mysqlTable(
  'FolderUser',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    folderId: varchar('folderId', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    role: mysqlEnum('role', ['owner', 'editor', 'viewer']),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('FolderUser_userId_idx').on(table.userId),
    uniqueIndex('FolderUser_folderId_userId_key').on(table.folderId, table.userId),
    foreignKey({
      name: 'FolderUser_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FolderUser_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const folderAccessRequest = mysqlTable(
  'FolderAccessRequest',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    folderId: varchar('folderId', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('FolderAccessRequest_userId_idx').on(table.userId),
    uniqueIndex('FolderAccessRequest_folderId_userId_key').on(table.folderId, table.userId),
    foreignKey({
      name: 'FolderAccessRequest_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FolderAccessRequest_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const integration = mysqlTable(
  'Integration',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: varchar('userId', { length: 191 }),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    name: varchar('name', { length: 191 }).notNull(),
    slug: varchar('slug', { length: 191 }).notNull(),
    description: varchar('description', { length: 191 }),
    readme: longtext('readme'),
    developer: varchar('developer', { length: 191 }).notNull(),
    website: varchar('website', { length: 191 }).notNull(),
    logo: varchar('logo', { length: 191 }),
    screenshots: json('screenshots'),
    verified: boolean('verified').notNull().default(false),
    installUrl: text('installUrl'),
    guideUrl: text('guideUrl'),
    category: varchar('category', { length: 191 }),
    comingSoon: boolean('comingSoon').notNull().default(false),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Integration_slug_key').on(table.slug),
    index('Integration_projectId_idx').on(table.projectId),
    index('Integration_userId_idx').on(table.userId),
    foreignKey({
      name: 'Integration_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Integration_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const installedIntegration = mysqlTable(
  'InstalledIntegration',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: varchar('userId', { length: 191 }).notNull(),
    integrationId: varchar('integrationId', { length: 191 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    credentials: json('credentials'),
    settings: json('settings'),
  },
  (table) => [
    index('InstalledIntegration_projectId_idx').on(table.projectId),
    index('InstalledIntegration_integrationId_idx').on(table.integrationId),
    uniqueIndex('InstalledIntegration_userId_integrationId_projectId_key').on(
      table.userId,
      table.integrationId,
      table.projectId,
    ),
    foreignKey({
      name: 'InstalledIntegration_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'InstalledIntegration_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'InstalledIntegration_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const invoice = mysqlTable(
  'Invoice',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    workspaceId: varchar('workspaceId', { length: 191 }).notNull(),
    number: varchar('number', { length: 191 }),
    status: mysqlEnum('status', ['processing', 'completed', 'failed'])
      .notNull()
      .default('processing'),
    type: mysqlEnum('type', ['domainRenewal']).notNull().default('domainRenewal'),
    paymentMethod: mysqlEnum('paymentMethod', ['card', 'ach', 'ach_fast', 'sepa', 'acss']),
    amount: int('amount').notNull().default(0),
    fee: int('fee').notNull().default(0),
    refundedAmount: int('refundedAmount').notNull().default(0),
    total: int('total').notNull().default(0),
    receiptUrl: text('receiptUrl'),
    refundReason: text('refundReason'),
    failedReason: text('failedReason'),
    registeredDomains: json('registeredDomains'),
    stripeChargeMetadata: json('stripeChargeMetadata'),
    failedAttempts: int('failedAttempts').notNull().default(0),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    paidAt: datetime('paidAt', { fsp: 3 }),
  },
  (table) => [
    uniqueIndex('Invoice_number_key').on(table.number),
    index('Invoice_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'Invoice_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [project.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const jacksonIndex = mysqlTable(
  'jackson_index',
  {
    id: int('id').primaryKey().autoincrement(),
    key: varchar('key', { length: 250 }).notNull(),
    storeKey: varchar('storeKey', { length: 250 }).notNull(),
  },
  (table) => [index('_jackson_index_key_store').on(table.key, table.storeKey)],
)

export const jacksonStore = mysqlTable(
  'jackson_store',
  {
    key: varchar('key', { length: 250 }).primaryKey(),
    value: text('value').notNull(),
    iv: varchar('iv', { length: 64 }),
    tag: varchar('tag', { length: 64 }),
    namespace: varchar('namespace', { length: 64 }),
    createdAt: timestamp('createdAt', { fsp: 0 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    modifiedAt: timestamp('modifiedAt', { fsp: 0 }),
  },
  (table) => [index('_jackson_store_namespace').on(table.namespace)],
)

export const jacksonTtl = mysqlTable(
  'jackson_ttl',
  {
    key: varchar('key', { length: 250 }).primaryKey(),
    expiresAt: bigint('expiresAt', { mode: 'bigint' }).notNull(),
  },
  (table) => [index('_jackson_ttl_expires_at').on(table.expiresAt)],
)

export const job = mysqlTable(
  'Job',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }).notNull(),
    payload: json('payload').notNull(),
    options: json('options'),
    scheduledAt: datetime('scheduledAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    attempts: int('attempts').notNull().default(0),
    lastError: text('lastError'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [index('Job_scheduledAt_attempts_idx').on(table.scheduledAt, table.attempts)],
)

export const link = mysqlTable(
  'Link',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    domain: varchar('domain', { length: 191 }).notNull(),
    key: varchar('key', { length: 191 }).notNull(),
    url: longtext('url').notNull(),
    shortLink: varchar('shortLink', { length: 400 }).notNull(),
    archived: boolean('archived').notNull().default(false),
    expiresAt: datetime('expiresAt', { fsp: 3 }),
    expiredUrl: text('expiredUrl'),
    disabledAt: datetime('disabledAt', { fsp: 3 }),
    password: varchar('password', { length: 191 }),
    trackConversion: boolean('trackConversion').notNull().default(false),
    proxy: boolean('proxy').notNull().default(false),
    title: varchar('title', { length: 191 }),
    description: varchar('description', { length: 280 }),
    image: longtext('image'),
    video: text('video'),
    utm_source: varchar('utm_source', { length: 255 }),
    utm_medium: varchar('utm_medium', { length: 255 }),
    utm_campaign: varchar('utm_campaign', { length: 255 }),
    utm_term: varchar('utm_term', { length: 255 }),
    utm_content: varchar('utm_content', { length: 255 }),
    rewrite: boolean('rewrite').notNull().default(false),
    linkRetentionCleanupDisabledAt: datetime('linkRetentionCleanupDisabledAt', { fsp: 3 }),
    doIndex: boolean('doIndex').notNull().default(false),
    ios: text('ios'),
    android: text('android'),
    geo: json('geo'),
    testVariants: json('testVariants'),
    testStartedAt: datetime('testStartedAt', { fsp: 3 }),
    testCompletedAt: datetime('testCompletedAt', { fsp: 3 }),
    userId: varchar('userId', { length: 191 }),
    projectId: varchar('projectId', { length: 191 }),
    folderId: varchar('folderId', { length: 191 }),
    externalId: varchar('externalId', { length: 191 }),
    tenantId: varchar('tenantId', { length: 191 }),
    publicStats: boolean('publicStats').notNull().default(false),
    clicks: int('clicks').notNull().default(0),
    leads: int('leads').notNull().default(0),
    conversions: int('conversions').notNull().default(0),
    sales: int('sales').notNull().default(0),
    saleAmount: bigint('saleAmount', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    lastClicked: datetime('lastClicked', { fsp: 3 }),
    lastLeadAt: datetime('lastLeadAt', { fsp: 3 }),
    lastConversionAt: datetime('lastConversionAt', { fsp: 3 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    comments: text('comments'),
  },
  (table) => [
    uniqueIndex('Link_shortLink_key').on(table.shortLink),
    index('Link_projectId_tenantId_idx').on(table.projectId, table.tenantId),
    index('Link_projectId_url_idx').on(table.projectId, sql`${table.url}(500)`),
    index('Link_projectId_folderId_archived_createdAt_idx').on(
      table.projectId,
      table.folderId,
      table.archived,
      table.createdAt,
    ),
    index('Link_domain_createdAt_idx').on(table.domain, table.createdAt),
    index('Link_folderId_idx').on(table.folderId),
    index('Link_userId_idx').on(table.userId),
    uniqueIndex('Link_domain_key_key').on(table.domain, table.key),
    uniqueIndex('Link_projectId_externalId_key').on(table.projectId, table.externalId),
    foreignKey({ name: 'Link_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Link_domain_fkey', columns: [table.domain], foreignColumns: [domain.slug] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const yearInReview = mysqlTable(
  'YearInReview',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    year: int('year').notNull(),
    totalLinks: int('totalLinks').notNull(),
    totalClicks: int('totalClicks').notNull(),
    topLinks: json('topLinks').notNull(),
    topCountries: json('topCountries').notNull(),
    workspaceId: varchar('workspaceId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    sentAt: datetime('sentAt', { fsp: 3 }),
  },
  (table) => [
    index('YearInReview_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'YearInReview_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userNotificationPreferences = mysqlTable(
  'UserNotificationPreferences',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: varchar('userId', { length: 191 }).notNull(),
    dubLinks: boolean('dubLinks').notNull().default(true),
  },
  (table) => [
    uniqueIndex('UserNotificationPreferences_userId_key').on(table.userId),
    foreignKey({
      name: 'UserNotificationPreferences_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const notificationPreference = mysqlTable(
  'NotificationPreference',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    projectUserId: varchar('projectUserId', { length: 191 }).notNull(),
    linkUsageSummary: boolean('linkUsageSummary').notNull().default(true),
    domainConfigurationUpdates: boolean('domainConfigurationUpdates').notNull().default(true),
  },
  (table) => [
    uniqueIndex('NotificationPreference_projectUserId_key').on(table.projectUserId),
    foreignKey({
      name: 'NotificationPreference_projectUserId_fkey',
      columns: [table.projectUserId],
      foreignColumns: [projectUsers.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oAuthApp = mysqlTable(
  'OAuthApp',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    integrationId: varchar('integrationId', { length: 191 }).notNull(),
    clientId: varchar('clientId', { length: 191 }).notNull(),
    hashedClientSecret: varchar('hashedClientSecret', { length: 191 }).notNull(),
    partialClientSecret: varchar('partialClientSecret', { length: 191 }).notNull(),
    redirectUris: json('redirectUris').notNull(),
    pkce: boolean('pkce').notNull().default(false),
  },
  (table) => [
    uniqueIndex('OAuthApp_integrationId_key').on(table.integrationId),
    unique('OAuthApp_clientId_key').on(table.clientId),
    foreignKey({
      name: 'OAuthApp_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oAuthCode = mysqlTable(
  'OAuthCode',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: varchar('clientId', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    code: varchar('code', { length: 191 }).notNull(),
    scopes: text('scopes'),
    redirectUri: varchar('redirectUri', { length: 191 }).notNull(),
    codeChallenge: varchar('codeChallenge', { length: 191 }),
    codeChallengeMethod: varchar('codeChallengeMethod', { length: 191 }),
    expiresAt: datetime('expiresAt', { fsp: 3 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('OAuthCode_code_key').on(table.code),
    index('OAuthCode_clientId_idx').on(table.clientId),
    index('OAuthCode_userId_idx').on(table.userId),
    index('OAuthCode_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'OAuthCode_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oAuthApp.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthCode_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthCode_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oAuthRefreshToken = mysqlTable(
  'OAuthRefreshToken',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    installationId: varchar('installationId', { length: 191 }).notNull(),
    accessTokenId: varchar('accessTokenId', { length: 191 }).notNull(),
    hashedRefreshToken: varchar('hashedRefreshToken', { length: 191 }).notNull(),
    expiresAt: datetime('expiresAt', { fsp: 3 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('OAuthRefreshToken_hashedRefreshToken_key').on(table.hashedRefreshToken),
    index('OAuthRefreshToken_installationId_idx').on(table.installationId),
    index('OAuthRefreshToken_accessTokenId_idx').on(table.accessTokenId),
    foreignKey({
      name: 'OAuthRefreshToken_accessTokenId_fkey',
      columns: [table.accessTokenId],
      foreignColumns: [restrictedToken.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OAuthRefreshToken_installationId_fkey',
      columns: [table.installationId],
      foreignColumns: [installedIntegration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const user = mysqlTable(
  'User',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: varchar('name', { length: 191 }),
    email: varchar('email', { length: 191 }),
    emailVerified: datetime('emailVerified', { fsp: 3 }),
    image: varchar('image', { length: 191 }),
    isMachine: boolean('isMachine').notNull().default(false),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    passwordHash: varchar('passwordHash', { length: 191 }),
    invalidLoginAttempts: int('invalidLoginAttempts').notNull().default(0),
    lockedAt: datetime('lockedAt', { fsp: 3 }),
    defaultWorkspace: varchar('defaultWorkspace', { length: 191 }),
    source: varchar('source', { length: 191 }),
    sentMail: boolean('sentMail').notNull().default(false),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    index('User_createdAt_idx').on(table.createdAt),
    index('User_defaultWorkspace_idx').on(table.defaultWorkspace),
    index('User_source_idx').on(table.source),
    index('User_sentMail_idx').on(table.sentMail),
  ],
)

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
    refresh_token: text('refresh_token'),
    refresh_token_expires_in: int('refresh_token_expires_in'),
    access_token: text('access_token'),
    expires_at: int('expires_at'),
    token_type: varchar('token_type', { length: 191 }),
    scope: varchar('scope', { length: 191 }),
    id_token: text('id_token'),
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

export const tag = mysqlTable(
  'Tag',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }).notNull(),
    color: varchar('color', { length: 191 }).notNull().default('blue'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    projectId: varchar('projectId', { length: 191 }),
  },
  (table) => [
    index('Tag_projectId_idx').on(table.projectId),
    uniqueIndex('Tag_name_projectId_key').on(table.name, table.projectId),
    foreignKey({
      name: 'Tag_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const linkTag = mysqlTable(
  'LinkTag',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    linkId: varchar('linkId', { length: 191 }).notNull(),
    tagId: varchar('tagId', { length: 191 }).notNull(),
  },
  (table) => [
    index('LinkTag_tagId_idx').on(table.tagId),
    uniqueIndex('LinkTag_linkId_tagId_key').on(table.linkId, table.tagId),
    foreignKey({ name: 'LinkTag_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'LinkTag_tagId_fkey', columns: [table.tagId], foreignColumns: [tag.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const token = mysqlTable(
  'Token',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: varchar('name', { length: 191 }).notNull(),
    hashedKey: varchar('hashedKey', { length: 191 }).notNull(),
    partialKey: varchar('partialKey', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }),
    lastUsed: datetime('lastUsed', { fsp: 3 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    userId: varchar('userId', { length: 191 }).notNull(),
  },
  (table) => [
    uniqueIndex('Token_hashedKey_key').on(table.hashedKey),
    index('Token_userId_idx').on(table.userId),
    foreignKey({ name: 'Token_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const restrictedToken = mysqlTable(
  'RestrictedToken',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: varchar('name', { length: 191 }).notNull(),
    hashedKey: varchar('hashedKey', { length: 191 }).notNull(),
    partialKey: varchar('partialKey', { length: 191 }).notNull(),
    scopes: text('scopes'),
    expires: datetime('expires', { fsp: 3 }),
    lastUsed: datetime('lastUsed', { fsp: 3 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    userId: varchar('userId', { length: 191 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    installationId: varchar('installationId', { length: 191 }),
  },
  (table) => [
    uniqueIndex('RestrictedToken_hashedKey_key').on(table.hashedKey),
    index('RestrictedToken_userId_idx').on(table.userId),
    index('RestrictedToken_projectId_idx').on(table.projectId),
    index('RestrictedToken_installationId_idx').on(table.installationId),
    foreignKey({
      name: 'RestrictedToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RestrictedToken_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RestrictedToken_installationId_fkey',
      columns: [table.installationId],
      foreignColumns: [installedIntegration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
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

export const emailVerificationToken = mysqlTable(
  'EmailVerificationToken',
  {
    identifier: varchar('identifier', { length: 191 }).notNull(),
    token: varchar('token', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('EmailVerificationToken_token_key').on(table.token),
    uniqueIndex('EmailVerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const passwordResetToken = mysqlTable(
  'PasswordResetToken',
  {
    identifier: varchar('identifier', { length: 191 }).notNull(),
    token: varchar('token', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('PasswordResetToken_token_key').on(table.token),
    uniqueIndex('PasswordResetToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const utmTemplate = mysqlTable(
  'UtmTemplate',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }).notNull(),
    utm_source: varchar('utm_source', { length: 255 }),
    utm_medium: varchar('utm_medium', { length: 255 }),
    utm_campaign: varchar('utm_campaign', { length: 255 }),
    utm_term: varchar('utm_term', { length: 255 }),
    utm_content: varchar('utm_content', { length: 255 }),
    ref: varchar('ref', { length: 255 }),
    userId: varchar('userId', { length: 191 }),
    projectId: varchar('projectId', { length: 191 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('UtmTemplate_userId_idx').on(table.userId),
    uniqueIndex('UtmTemplate_projectId_name_key').on(table.projectId, table.name),
    foreignKey({
      name: 'UtmTemplate_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UtmTemplate_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhook = mysqlTable(
  'Webhook',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    installationId: varchar('installationId', { length: 191 }),
    receiver: mysqlEnum('receiver', ['user', 'zapier', 'make', 'slack', 'segment'])
      .notNull()
      .default('user'),
    name: varchar('name', { length: 191 }).notNull(),
    url: longtext('url').notNull(),
    secret: varchar('secret', { length: 191 }).notNull(),
    triggers: json('triggers').notNull(),
    linkScope: mysqlEnum('linkScope', ['workspace', 'folders', 'links']),
    consecutiveFailures: int('consecutiveFailures').notNull().default(0),
    lastFailedAt: datetime('lastFailedAt', { fsp: 3 }),
    disabledAt: datetime('disabledAt', { fsp: 3 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Webhook_projectId_idx').on(table.projectId),
    index('Webhook_installationId_idx').on(table.installationId),
    foreignKey({
      name: 'Webhook_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Webhook_installationId_fkey',
      columns: [table.installationId],
      foreignColumns: [installedIntegration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const linkWebhook = mysqlTable(
  'LinkWebhook',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    linkId: varchar('linkId', { length: 191 }).notNull(),
    webhookId: varchar('webhookId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LinkWebhook_webhookId_idx').on(table.webhookId),
    uniqueIndex('LinkWebhook_linkId_webhookId_key').on(table.linkId, table.webhookId),
    foreignKey({
      name: 'LinkWebhook_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LinkWebhook_webhookId_fkey',
      columns: [table.webhookId],
      foreignColumns: [webhook.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const folderWebhook = mysqlTable(
  'FolderWebhook',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    folderId: varchar('folderId', { length: 191 }).notNull(),
    webhookId: varchar('webhookId', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('FolderWebhook_webhookId_idx').on(table.webhookId),
    uniqueIndex('FolderWebhook_folderId_webhookId_key').on(table.folderId, table.webhookId),
    foreignKey({
      name: 'FolderWebhook_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FolderWebhook_webhookId_fkey',
      columns: [table.webhookId],
      foreignColumns: [webhook.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const project = mysqlTable(
  'Project',
  {
    id: varchar('id', { length: 191 }).primaryKey(),
    name: varchar('name', { length: 191 }).notNull(),
    slug: varchar('slug', { length: 191 }).notNull(),
    logo: varchar('logo', { length: 191 }),
    inviteCode: varchar('inviteCode', { length: 191 }),
    plan: varchar('plan', { length: 191 }).notNull().default('free'),
    planTier: int('planTier').notNull().default(1),
    planPeriod: mysqlEnum('planPeriod', ['monthly', 'yearly', 'quarterly', 'biweekly']),
    stripeId: varchar('stripeId', { length: 191 }),
    billingCycleStart: int('billingCycleStart').notNull(),
    billingCycleEndsAt: datetime('billingCycleEndsAt', { fsp: 3 }),
    trialEndsAt: datetime('trialEndsAt', { fsp: 3 }),
    subscriptionCanceledAt: datetime('subscriptionCanceledAt', { fsp: 3 }),
    paymentFailedAt: datetime('paymentFailedAt', { fsp: 3 }),
    invoicePrefix: varchar('invoicePrefix', { length: 191 }),
    stripeConnectId: varchar('stripeConnectId', { length: 191 }),
    stripeConnectMode: mysqlEnum('stripeConnectMode', ['live', 'test', 'sandbox']),
    shopifyStoreId: varchar('shopifyStoreId', { length: 191 }),
    totalLinks: int('totalLinks').notNull().default(0),
    totalClicks: int('totalClicks').notNull().default(0),
    usage: int('usage').notNull().default(0),
    usageLimit: int('usageLimit').notNull().default(1000),
    linksUsage: int('linksUsage').notNull().default(0),
    linksLimit: int('linksLimit').notNull().default(25),
    domainsLimit: int('domainsLimit').notNull().default(3),
    tagsLimit: int('tagsLimit').notNull().default(5),
    foldersUsage: int('foldersUsage').notNull().default(0),
    foldersLimit: int('foldersLimit').notNull().default(0),
    usersLimit: int('usersLimit').notNull().default(1),
    aiUsage: int('aiUsage').notNull().default(0),
    aiLimit: int('aiLimit').notNull().default(10),
    store: json('store'),
    siteVisitTrackingSettings: json('siteVisitTrackingSettings'),
    allowedHostnames: json('allowedHostnames'),
    publishableKey: varchar('publishableKey', { length: 191 }),
    conversionEnabled: boolean('conversionEnabled').notNull().default(false),
    webhookEnabled: boolean('webhookEnabled').notNull().default(false),
    dotLinkClaimed: boolean('dotLinkClaimed').notNull().default(false),
    ssoEmailDomain: varchar('ssoEmailDomain', { length: 191 }),
    ssoEnforcedAt: datetime('ssoEnforcedAt', { fsp: 3 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
    usageLastChecked: datetime('usageLastChecked', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    disabledAt: datetime('disabledAt', { fsp: 3 }),
  },
  (table) => [
    uniqueIndex('Project_slug_key').on(table.slug),
    uniqueIndex('Project_inviteCode_key').on(table.inviteCode),
    uniqueIndex('Project_stripeId_key').on(table.stripeId),
    uniqueIndex('Project_invoicePrefix_key').on(table.invoicePrefix),
    uniqueIndex('Project_shopifyStoreId_key').on(table.shopifyStoreId),
    uniqueIndex('Project_publishableKey_key').on(table.publishableKey),
    uniqueIndex('Project_ssoEmailDomain_key').on(table.ssoEmailDomain),
    index('Project_usageLastChecked_idx').on(table.usageLastChecked),
    index('Project_trialEndsAt_idx').on(table.trialEndsAt),
    uniqueIndex('Project_stripeConnectId_stripeConnectMode_key').on(
      table.stripeConnectId,
      table.stripeConnectMode,
    ),
  ],
)

export const projectInvite = mysqlTable(
  'ProjectInvite',
  {
    email: varchar('email', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    role: mysqlEnum('role', ['owner', 'member', 'viewer', 'billing']).notNull().default('member'),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ProjectInvite_projectId_idx').on(table.projectId),
    uniqueIndex('ProjectInvite_email_projectId_key').on(table.email, table.projectId),
    foreignKey({
      name: 'ProjectInvite_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const projectUsers = mysqlTable(
  'ProjectUsers',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    role: mysqlEnum('role', ['owner', 'member', 'viewer', 'billing']).notNull().default('member'),
    userId: varchar('userId', { length: 191 }).notNull(),
    projectId: varchar('projectId', { length: 191 }).notNull(),
    workspacePreferences: json('workspacePreferences'),
    defaultFolderId: varchar('defaultFolderId', { length: 191 }),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updatedAt', { fsp: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ProjectUsers_projectId_idx').on(table.projectId),
    uniqueIndex('ProjectUsers_userId_projectId_key').on(table.userId, table.projectId),
    foreignKey({
      name: 'ProjectUsers_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ProjectUsers_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sentEmail = mysqlTable(
  'SentEmail',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: varchar('type', { length: 191 }).notNull(),
    createdAt: datetime('createdAt', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    projectId: varchar('projectId', { length: 191 }),
  },
  (table) => [
    index('SentEmail_projectId_idx').on(table.projectId),
    foreignKey({
      name: 'SentEmail_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [project.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    customer,
    dashboard,
    domain,
    registeredDomain,
    defaultDomains,
    folder,
    folderUser,
    folderAccessRequest,
    integration,
    installedIntegration,
    invoice,
    jacksonIndex,
    jacksonStore,
    jacksonTtl,
    job,
    link,
    yearInReview,
    userNotificationPreferences,
    notificationPreference,
    oAuthApp,
    oAuthCode,
    oAuthRefreshToken,
    user,
    account,
    session,
    tag,
    linkTag,
    token,
    restrictedToken,
    verificationToken,
    emailVerificationToken,
    passwordResetToken,
    utmTemplate,
    webhook,
    linkWebhook,
    folderWebhook,
    project,
    projectInvite,
    projectUsers,
    sentEmail,
  },
  (r) => ({
    customer: {
      project: r.one.project({ from: r.customer.projectId, to: r.project.id, optional: false }),
      link: r.one.link({ from: r.customer.linkId, to: r.link.id }),
    },
    dashboard: {
      link: r.one.link({ from: r.dashboard.linkId, to: r.link.id }),
      folder: r.one.folder({ from: r.dashboard.folderId, to: r.folder.id }),
      project: r.one.project({ from: r.dashboard.projectId, to: r.project.id }),
      user: r.one.user({ from: r.dashboard.userId, to: r.user.id }),
    },
    domain: {
      project: r.one.project({ from: r.domain.projectId, to: r.project.id }),
      links: r.many.link(),
      registeredDomain: r.one.registeredDomain({
        from: r.domain.id,
        to: r.registeredDomain.domainId,
      }),
    },
    registeredDomain: {
      project: r.one.project({
        from: r.registeredDomain.projectId,
        to: r.project.id,
        optional: false,
      }),
      domain: r.one.domain({ from: r.registeredDomain.domainId, to: r.domain.id }),
    },
    defaultDomains: {
      project: r.one.project({
        from: r.defaultDomains.projectId,
        to: r.project.id,
        optional: false,
      }),
    },
    folder: {
      project: r.one.project({ from: r.folder.projectId, to: r.project.id, optional: false }),
      links: r.many.link(),
      users: r.many.folderUser(),
      accessRequests: r.many.folderAccessRequest(),
      webhooks: r.many.folderWebhook(),
      dashboard: r.one.dashboard({ from: r.folder.id, to: r.dashboard.folderId }),
    },
    folderUser: {
      folder: r.one.folder({ from: r.folderUser.folderId, to: r.folder.id, optional: false }),
      user: r.one.user({ from: r.folderUser.userId, to: r.user.id, optional: false }),
    },
    folderAccessRequest: {
      folder: r.one.folder({
        from: r.folderAccessRequest.folderId,
        to: r.folder.id,
        optional: false,
      }),
      user: r.one.user({ from: r.folderAccessRequest.userId, to: r.user.id, optional: false }),
    },
    integration: {
      user: r.one.user({ from: r.integration.userId, to: r.user.id }),
      project: r.one.project({ from: r.integration.projectId, to: r.project.id, optional: false }),
      oAuthApp: r.one.oAuthApp({ from: r.integration.id, to: r.oAuthApp.integrationId }),
      installations: r.many.installedIntegration(),
    },
    installedIntegration: {
      user: r.one.user({ from: r.installedIntegration.userId, to: r.user.id, optional: false }),
      integration: r.one.integration({
        from: r.installedIntegration.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      project: r.one.project({
        from: r.installedIntegration.projectId,
        to: r.project.id,
        optional: false,
      }),
      refreshTokens: r.many.oAuthRefreshToken(),
      accessTokens: r.many.restrictedToken(),
      webhooks: r.many.webhook(),
    },
    invoice: {
      workspace: r.one.project({ from: r.invoice.workspaceId, to: r.project.id, optional: false }),
    },
    link: {
      user: r.one.user({ from: r.link.userId, to: r.user.id }),
      project: r.one.project({ from: r.link.projectId, to: r.project.id }),
      folder: r.one.folder({ from: r.link.folderId, to: r.folder.id }),
      shortDomain: r.one.domain({ from: r.link.domain, to: r.domain.slug, optional: false }),
      tags: r.many.linkTag(),
      webhooks: r.many.linkWebhook(),
      dashboard: r.one.dashboard({ from: r.link.id, to: r.dashboard.linkId }),
      customers: r.many.customer(),
    },
    yearInReview: {
      workspace: r.one.project({
        from: r.yearInReview.workspaceId,
        to: r.project.id,
        optional: false,
      }),
    },
    userNotificationPreferences: {
      user: r.one.user({
        from: r.userNotificationPreferences.userId,
        to: r.user.id,
        optional: false,
      }),
    },
    notificationPreference: {
      projectUser: r.one.projectUsers({
        from: r.notificationPreference.projectUserId,
        to: r.projectUsers.id,
        optional: false,
      }),
    },
    oAuthApp: {
      oAuthCodes: r.many.oAuthCode(),
      integration: r.one.integration({ from: r.oAuthApp.integrationId, to: r.integration.id }),
    },
    oAuthCode: {
      oAuthApp: r.one.oAuthApp({
        from: r.oAuthCode.clientId,
        to: r.oAuthApp.clientId,
        optional: false,
      }),
      user: r.one.user({ from: r.oAuthCode.userId, to: r.user.id, optional: false }),
      project: r.one.project({ from: r.oAuthCode.projectId, to: r.project.id, optional: false }),
    },
    oAuthRefreshToken: {
      accessToken: r.one.restrictedToken({
        from: r.oAuthRefreshToken.accessTokenId,
        to: r.restrictedToken.id,
        optional: false,
      }),
      installedIntegration: r.one.installedIntegration({
        from: r.oAuthRefreshToken.installationId,
        to: r.installedIntegration.id,
        optional: false,
      }),
    },
    user: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      notificationPreferences: r.one.userNotificationPreferences({
        from: r.user.id,
        to: r.userNotificationPreferences.userId,
      }),
      projects: r.many.projectUsers(),
      links: r.many.link(),
      dashboards: r.many.dashboard(),
      tokens: r.many.token(),
      restrictedTokens: r.many.restrictedToken(),
      oAuthCodes: r.many.oAuthCode(),
      integrations: r.many.integration(),
      installedIntegrations: r.many.installedIntegration(),
      folders: r.many.folderUser(),
      folderAccessRequests: r.many.folderAccessRequest(),
      utmTemplates: r.many.utmTemplate(),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    tag: {
      project: r.one.project({ from: r.tag.projectId, to: r.project.id }),
      links: r.many.linkTag(),
    },
    linkTag: {
      link: r.one.link({ from: r.linkTag.linkId, to: r.link.id, optional: false }),
      tag: r.one.tag({ from: r.linkTag.tagId, to: r.tag.id, optional: false }),
    },
    token: { user: r.one.user({ from: r.token.userId, to: r.user.id, optional: false }) },
    restrictedToken: {
      user: r.one.user({ from: r.restrictedToken.userId, to: r.user.id, optional: false }),
      project: r.one.project({
        from: r.restrictedToken.projectId,
        to: r.project.id,
        optional: false,
      }),
      refreshTokens: r.many.oAuthRefreshToken(),
      installedIntegration: r.one.installedIntegration({
        from: r.restrictedToken.installationId,
        to: r.installedIntegration.id,
      }),
    },
    utmTemplate: {
      user: r.one.user({ from: r.utmTemplate.userId, to: r.user.id }),
      project: r.one.project({ from: r.utmTemplate.projectId, to: r.project.id }),
    },
    webhook: {
      project: r.one.project({ from: r.webhook.projectId, to: r.project.id, optional: false }),
      installedIntegration: r.one.installedIntegration({
        from: r.webhook.installationId,
        to: r.installedIntegration.id,
      }),
      links: r.many.linkWebhook(),
      folders: r.many.folderWebhook(),
    },
    linkWebhook: {
      link: r.one.link({ from: r.linkWebhook.linkId, to: r.link.id, optional: false }),
      webhook: r.one.webhook({ from: r.linkWebhook.webhookId, to: r.webhook.id, optional: false }),
    },
    folderWebhook: {
      folder: r.one.folder({ from: r.folderWebhook.folderId, to: r.folder.id, optional: false }),
      webhook: r.one.webhook({
        from: r.folderWebhook.webhookId,
        to: r.webhook.id,
        optional: false,
      }),
    },
    project: {
      users: r.many.projectUsers(),
      invites: r.many.projectInvite(),
      sentEmails: r.many.sentEmail(),
      links: r.many.link(),
      domains: r.many.domain(),
      tags: r.many.tag(),
      invoices: r.many.invoice(),
      customers: r.many.customer(),
      defaultDomains: r.many.defaultDomains(),
      restrictedTokens: r.many.restrictedToken(),
      oAuthCodes: r.many.oAuthCode(),
      integrations: r.many.integration(),
      installedIntegrations: r.many.installedIntegration(),
      webhooks: r.many.webhook(),
      folders: r.many.folder(),
      registeredDomains: r.many.registeredDomain(),
      dashboards: r.many.dashboard(),
      utmTemplates: r.many.utmTemplate(),
      yearInReviews: r.many.yearInReview(),
    },
    projectInvite: {
      project: r.one.project({
        from: r.projectInvite.projectId,
        to: r.project.id,
        optional: false,
      }),
    },
    projectUsers: {
      notificationPreference: r.one.notificationPreference({
        from: r.projectUsers.id,
        to: r.notificationPreference.projectUserId,
      }),
      user: r.one.user({ from: r.projectUsers.userId, to: r.user.id, optional: false }),
      project: r.one.project({ from: r.projectUsers.projectId, to: r.project.id, optional: false }),
    },
    sentEmail: { project: r.one.project({ from: r.sentEmail.projectId, to: r.project.id }) },
  }),
)
