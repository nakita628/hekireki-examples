import {
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

export const accessTypeEnum = pgEnum('AccessType', ['MCP', 'PRIVATE', 'PUBLIC'])

export const assetClassEnum = pgEnum('AssetClass', [
  'ALTERNATIVE_INVESTMENT',
  'COMMODITY',
  'EQUITY',
  'FIXED_INCOME',
  'LIQUIDITY',
  'REAL_ESTATE',
])

export const assetSubClassEnum = pgEnum('AssetSubClass', [
  'BOND',
  'CASH',
  'COLLECTIBLE',
  'COMMODITY',
  'CRYPTOCURRENCY',
  'ETF',
  'LOAN',
  'MUTUALFUND',
  'PRECIOUS_METAL',
  'PRIVATE_EQUITY',
  'STOCK',
])

export const dataGatheringFrequencyEnum = pgEnum('DataGatheringFrequency', ['DAILY', 'HOURLY'])

export const dataSourceEnum = pgEnum('DataSource', [
  'ALPHA_VANTAGE',
  'COINGECKO',
  'EOD_HISTORICAL_DATA',
  'FINANCIAL_MODELING_PREP',
  'GHOSTFOLIO',
  'GOOGLE_SHEETS',
  'MANUAL',
  'RAPID_API',
  'YAHOO',
])

export const marketDataStateEnum = pgEnum('MarketDataState', ['CLOSE', 'INTRADAY'])

export const providerEnum = pgEnum('Provider', ['ANONYMOUS', 'GOOGLE', 'INTERNET_IDENTITY', 'OIDC'])

export const requestClientEnum = pgEnum('RequestClient', ['API', 'MCP', 'SELF_HOSTING', 'WEB'])

export const roleEnum = pgEnum('Role', ['ADMIN', 'DEMO', 'INACTIVE', 'USER'])

export const typeEnum = pgEnum('Type', ['BUY', 'DIVIDEND', 'FEE', 'INTEREST', 'LIABILITY', 'SELL'])

export const access = pgTable(
  'Access',
  {
    alias: text('alias'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    granteeUserId: text('granteeUserId'),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    scopes: text('scopes').array().default([]),
    settings: jsonb('settings').notNull().default({}),
    type: accessTypeEnum('type').notNull().default('PRIVATE'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    index('Access_alias_idx').on(table.alias),
    index('Access_granteeUserId_idx').on(table.granteeUserId),
    index('Access_userId_idx').on(table.userId),
    foreignKey({
      name: 'Access_granteeUserId_fkey',
      columns: [table.granteeUserId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Access_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const account = pgTable(
  'Account',
  {
    comment: text('comment'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    currency: text('currency'),
    id: text('id')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name'),
    platformId: text('platformId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    primaryKey({ name: 'Account_pkey', columns: [table.id, table.userId] }),
    index('Account_currency_idx').on(table.currency),
    index('Account_id_idx').on(table.id),
    index('Account_name_idx').on(table.name),
    index('Account_userId_idx').on(table.userId),
    foreignKey({
      name: 'Account_platformId_fkey',
      columns: [table.platformId],
      foreignColumns: [platform.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accountBalance = pgTable(
  'AccountBalance',
  {
    accountId: text('accountId').notNull(),
    comment: text('comment'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    date: timestamp('date', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
    value: doublePrecision('value').notNull(),
  },
  (table) => [
    index('AccountBalance_accountId_idx').on(table.accountId),
    index('AccountBalance_date_idx').on(table.date),
    uniqueIndex('AccountBalance_accountId_userId_date_key').on(
      table.accountId,
      table.userId,
      table.date,
    ),
    foreignKey({
      name: 'AccountBalance_accountId_userId_fkey',
      columns: [table.accountId, table.userId],
      foreignColumns: [account.id, account.userId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const analytics = pgTable(
  'Analytics',
  {
    activityCount: integer('activityCount').notNull().default(0),
    country: text('country'),
    dataProviderGhostfolioDailyRequests: integer('dataProviderGhostfolioDailyRequests')
      .notNull()
      .default(0),
    lastRequestAt: timestamp('lastRequestAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastRequestClient: requestClientEnum('lastRequestClient'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').primaryKey(),
  },
  (table) => [
    index('Analytics_lastRequestAt_idx').on(table.lastRequestAt),
    index('Analytics_updatedAt_idx').on(table.updatedAt),
    foreignKey({
      name: 'Analytics_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiKey = pgTable(
  'ApiKey',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    hashedKey: text('hashedKey').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    uniqueIndex('ApiKey_hashedKey_key').on(table.hashedKey),
    index('ApiKey_userId_idx').on(table.userId),
    foreignKey({ name: 'ApiKey_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const symbolProfileOverrides = pgTable(
  'SymbolProfileOverrides',
  {
    assetClass: assetClassEnum('assetClass'),
    assetSubClass: assetSubClassEnum('assetSubClass'),
    countries: jsonb('countries').default([]),
    holdings: jsonb('holdings').default([]),
    name: text('name'),
    sectors: jsonb('sectors').default([]),
    symbolProfileId: text('symbolProfileId').primaryKey(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    url: text('url'),
  },
  (table) => [
    foreignKey({
      name: 'SymbolProfileOverrides_symbolProfileId_fkey',
      columns: [table.symbolProfileId],
      foreignColumns: [symbolProfile.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const assetProfileResolution = pgTable(
  'AssetProfileResolution',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    currency: text('currency').notNull(),
    dataSourceOrigin: dataSourceEnum('dataSourceOrigin').notNull(),
    dataSourceTarget: dataSourceEnum('dataSourceTarget').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    requestCount: integer('requestCount').notNull().default(1),
    symbolOrigin: text('symbolOrigin').notNull(),
    symbolTarget: text('symbolTarget').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('AssetProfileResolution_dataSourceOrigin_symbolOrigin_key').on(
      table.dataSourceOrigin,
      table.symbolOrigin,
    ),
  ],
)

export const assetProfileSplit = pgTable(
  'AssetProfileSplit',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    date: timestamp('date', { precision: 3 }).notNull(),
    denominator: integer('denominator').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    numerator: integer('numerator').notNull(),
    symbolProfileId: text('symbolProfileId').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('AssetProfileSplit_date_idx').on(table.date),
    uniqueIndex('AssetProfileSplit_symbolProfileId_date_key').on(table.symbolProfileId, table.date),
    foreignKey({
      name: 'AssetProfileSplit_symbolProfileId_fkey',
      columns: [table.symbolProfileId],
      foreignColumns: [symbolProfile.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const authDevice = pgTable(
  'AuthDevice',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    credentialId: bytea('credentialId').notNull(),
    credentialPublicKey: bytea('credentialPublicKey').notNull(),
    counter: integer('counter').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    index('AuthDevice_userId_idx').on(table.userId),
    foreignKey({
      name: 'AuthDevice_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const marketData = pgTable(
  'MarketData',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    dataSource: dataSourceEnum('dataSource').notNull(),
    date: timestamp('date', { precision: 3 }).notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    isCarriedForward: boolean('isCarriedForward').notNull().default(false),
    marketPrice: doublePrecision('marketPrice').notNull(),
    state: marketDataStateEnum('state').notNull().default('CLOSE'),
    symbol: text('symbol').notNull(),
  },
  (table) => [
    index('MarketData_date_idx').on(table.date),
    index('MarketData_state_idx').on(table.state),
    index('MarketData_symbol_idx').on(table.symbol),
    uniqueIndex('MarketData_dataSource_symbol_date_key').on(
      table.dataSource,
      table.symbol,
      table.date,
    ),
  ],
)

export const order = pgTable(
  'Order',
  {
    accountId: text('accountId'),
    accountUserId: text('accountUserId'),
    comment: text('comment'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    currency: text('currency'),
    date: timestamp('date', { precision: 3 }).notNull(),
    fee: doublePrecision('fee').notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    quantity: doublePrecision('quantity').notNull(),
    symbolProfileId: text('symbolProfileId').notNull(),
    type: typeEnum('type').notNull(),
    unitPrice: doublePrecision('unitPrice').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    index('Order_accountId_idx').on(table.accountId),
    index('Order_date_idx').on(table.date),
    index('Order_symbolProfileId_date_idx').on(table.symbolProfileId, table.date),
    index('Order_userId_date_idx').on(table.userId, table.date),
    index('Order_userId_type_date_idx').on(table.userId, table.type, table.date),
    foreignKey({
      name: 'Order_accountId_accountUserId_fkey',
      columns: [table.accountId, table.accountUserId],
      foreignColumns: [account.id, account.userId],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Order_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Order_symbolProfileId_fkey',
      columns: [table.symbolProfileId],
      foreignColumns: [symbolProfile.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const platform = pgTable(
  'Platform',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name'),
    url: text('url').notNull(),
  },
  (table) => [
    uniqueIndex('Platform_url_key').on(table.url),
    index('Platform_name_idx').on(table.name),
  ],
)

export const property = pgTable('Property', {
  key: text('key').primaryKey(),
  value: text('value').notNull(),
})

export const settings = pgTable(
  'Settings',
  {
    settings: jsonb('settings'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').primaryKey(),
  },
  (table) => [
    foreignKey({ name: 'Settings_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const symbolProfile = pgTable(
  'SymbolProfile',
  {
    assetClass: assetClassEnum('assetClass'),
    assetSubClass: assetSubClassEnum('assetSubClass'),
    comment: text('comment'),
    countries: jsonb('countries'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    currency: text('currency').notNull(),
    cusip: text('cusip'),
    dataGatheringFrequency: dataGatheringFrequencyEnum('dataGatheringFrequency')
      .notNull()
      .default('DAILY'),
    dataSource: dataSourceEnum('dataSource').notNull(),
    figi: text('figi'),
    figiComposite: text('figiComposite'),
    figiShareClass: text('figiShareClass'),
    holdings: jsonb('holdings').default([]),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    isActive: boolean('isActive').notNull().default(true),
    isin: text('isin'),
    name: text('name'),
    scraperConfiguration: jsonb('scraperConfiguration'),
    sectors: jsonb('sectors'),
    symbol: text('symbol').notNull(),
    symbolMapping: jsonb('symbolMapping'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    url: text('url'),
    userId: text('userId'),
  },
  (table) => [
    index('SymbolProfile_assetClass_idx').on(table.assetClass),
    index('SymbolProfile_currency_idx').on(table.currency),
    index('SymbolProfile_cusip_idx').on(table.cusip),
    index('SymbolProfile_dataGatheringFrequency_idx').on(table.dataGatheringFrequency),
    index('SymbolProfile_dataSource_idx').on(table.dataSource),
    index('SymbolProfile_isActive_idx').on(table.isActive),
    index('SymbolProfile_isin_idx').on(table.isin),
    index('SymbolProfile_name_idx').on(table.name),
    index('SymbolProfile_symbol_idx').on(table.symbol),
    uniqueIndex('SymbolProfile_dataSource_symbol_key').on(table.dataSource, table.symbol),
    foreignKey({
      name: 'SymbolProfile_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscription = pgTable(
  'Subscription',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    price: doublePrecision('price'),
    stripeCheckoutSessionId: text('stripeCheckoutSessionId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    uniqueIndex('Subscription_stripeCheckoutSessionId_key').on(table.stripeCheckoutSessionId),
    index('Subscription_userId_idx').on(table.userId),
    foreignKey({
      name: 'Subscription_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tag = pgTable(
  'Tag',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    userId: text('userId'),
  },
  (table) => [
    index('Tag_name_idx').on(table.name),
    uniqueIndex('Tag_name_userId_key').on(table.name, table.userId),
    foreignKey({ name: 'Tag_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tagsOnAccounts = pgTable(
  'TagsOnAccounts',
  {
    accountId: text('accountId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    tagId: text('tagId').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'TagsOnAccounts_pkey',
      columns: [table.accountId, table.tagId, table.userId],
    }),
    index('TagsOnAccounts_tagId_idx').on(table.tagId),
    foreignKey({
      name: 'TagsOnAccounts_accountId_userId_fkey',
      columns: [table.accountId, table.userId],
      foreignColumns: [account.id, account.userId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TagsOnAccounts_tagId_fkey',
      columns: [table.tagId],
      foreignColumns: [tag.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
  {
    accessToken: text('accessToken'),
    authChallenge: text('authChallenge'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    provider: providerEnum('provider').notNull().default('ANONYMOUS'),
    role: roleEnum('role').notNull().default('USER'),
    thirdPartyId: text('thirdPartyId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('User_accessToken_idx').on(table.accessToken),
    index('User_createdAt_idx').on(table.createdAt),
    index('User_provider_idx').on(table.provider),
    index('User_role_idx').on(table.role),
    index('User_thirdPartyId_idx').on(table.thirdPartyId),
  ],
)

export const orderToTag = pgTable(
  '_OrderToTag',
  { A: text('A').notNull(), B: text('B').notNull() },
  (table) => [
    primaryKey({ name: '_OrderToTag_AB_pkey', columns: [table.A, table.B] }),
    index('_OrderToTag_B_index').on(table.B),
    foreignKey({ name: '_OrderToTag_A_fkey', columns: [table.A], foreignColumns: [order.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_OrderToTag_B_fkey', columns: [table.B], foreignColumns: [tag.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userWatchlist = pgTable(
  '_UserWatchlist',
  { A: text('A').notNull(), B: text('B').notNull() },
  (table) => [
    primaryKey({ name: '_UserWatchlist_AB_pkey', columns: [table.A, table.B] }),
    index('_UserWatchlist_B_index').on(table.B),
    foreignKey({
      name: '_UserWatchlist_A_fkey',
      columns: [table.A],
      foreignColumns: [symbolProfile.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_UserWatchlist_B_fkey', columns: [table.B], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    access,
    account,
    accountBalance,
    analytics,
    apiKey,
    symbolProfileOverrides,
    assetProfileResolution,
    assetProfileSplit,
    authDevice,
    marketData,
    order,
    platform,
    property,
    settings,
    symbolProfile,
    subscription,
    tag,
    tagsOnAccounts,
    user,
    orderToTag,
    userWatchlist,
  },
  (r) => ({
    access: {
      granteeUser: r.one.user({ from: r.access.granteeUserId, to: r.user.id, alias: 'accessGet' }),
      user: r.one.user({
        from: r.access.userId,
        to: r.user.id,
        optional: false,
        alias: 'accessGive',
      }),
    },
    account: {
      activities: r.many.order(),
      balances: r.many.accountBalance(),
      platform: r.one.platform({ from: r.account.platformId, to: r.platform.id }),
      tags: r.many.tagsOnAccounts(),
      user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }),
    },
    accountBalance: {
      account: r.one.account({
        from: [r.accountBalance.accountId, r.accountBalance.userId],
        to: [r.account.id, r.account.userId],
        optional: false,
      }),
    },
    analytics: { user: r.one.user({ from: r.analytics.userId, to: r.user.id, optional: false }) },
    apiKey: { user: r.one.user({ from: r.apiKey.userId, to: r.user.id, optional: false }) },
    symbolProfileOverrides: {
      symbolProfile: r.one.symbolProfile({
        from: r.symbolProfileOverrides.symbolProfileId,
        to: r.symbolProfile.id,
        optional: false,
      }),
    },
    assetProfileSplit: {
      symbolProfile: r.one.symbolProfile({
        from: r.assetProfileSplit.symbolProfileId,
        to: r.symbolProfile.id,
        optional: false,
      }),
    },
    authDevice: { user: r.one.user({ from: r.authDevice.userId, to: r.user.id, optional: false }) },
    order: {
      account: r.one.account({
        from: [r.order.accountId, r.order.accountUserId],
        to: [r.account.id, r.account.userId],
      }),
      tags: r.many.tag({
        from: r.order.id.through(r.orderToTag.A),
        to: r.tag.id.through(r.orderToTag.B),
      }),
      user: r.one.user({ from: r.order.userId, to: r.user.id, optional: false }),
      SymbolProfile: r.one.symbolProfile({
        from: r.order.symbolProfileId,
        to: r.symbolProfile.id,
        optional: false,
      }),
    },
    platform: { accounts: r.many.account() },
    settings: { user: r.one.user({ from: r.settings.userId, to: r.user.id, optional: false }) },
    symbolProfile: {
      activities: r.many.order(),
      assetProfileOverrides: r.one.symbolProfileOverrides({
        from: r.symbolProfile.id,
        to: r.symbolProfileOverrides.symbolProfileId,
      }),
      assetProfileSplits: r.many.assetProfileSplit(),
      user: r.one.user({
        from: r.symbolProfile.userId,
        to: r.user.id,
        alias: 'SymbolProfileToUser',
      }),
      watchedBy: r.many.user({
        from: r.symbolProfile.id.through(r.userWatchlist.A),
        to: r.user.id.through(r.userWatchlist.B),
      }),
    },
    subscription: {
      user: r.one.user({ from: r.subscription.userId, to: r.user.id, optional: false }),
    },
    tag: {
      accounts: r.many.tagsOnAccounts(),
      activities: r.many.order({
        from: r.tag.id.through(r.orderToTag.B),
        to: r.order.id.through(r.orderToTag.A),
      }),
      user: r.one.user({ from: r.tag.userId, to: r.user.id }),
    },
    tagsOnAccounts: {
      account: r.one.account({
        from: [r.tagsOnAccounts.accountId, r.tagsOnAccounts.userId],
        to: [r.account.id, r.account.userId],
        optional: false,
      }),
      tag: r.one.tag({ from: r.tagsOnAccounts.tagId, to: r.tag.id, optional: false }),
    },
    user: {
      accessesGet: r.many.access({ alias: 'accessGet' }),
      accessesGive: r.many.access({ alias: 'accessGive' }),
      accounts: r.many.account(),
      activities: r.many.order(),
      analytics: r.one.analytics({ from: r.user.id, to: r.analytics.userId }),
      apiKeys: r.many.apiKey(),
      authDevices: r.many.authDevice(),
      settings: r.one.settings({ from: r.user.id, to: r.settings.userId }),
      subscriptions: r.many.subscription(),
      tags: r.many.tag(),
      watchlist: r.many.symbolProfile({
        from: r.user.id.through(r.userWatchlist.B),
        to: r.symbolProfile.id.through(r.userWatchlist.A),
      }),
      SymbolProfile: r.many.symbolProfile({ alias: 'SymbolProfileToUser' }),
    },
    orderToTag: {
      order: r.one.order({ from: r.orderToTag.A, to: r.order.id, optional: false }),
      tag: r.one.tag({ from: r.orderToTag.B, to: r.tag.id, optional: false }),
    },
    userWatchlist: {
      symbolProfile: r.one.symbolProfile({
        from: r.userWatchlist.A,
        to: r.symbolProfile.id,
        optional: false,
      }),
      user: r.one.user({ from: r.userWatchlist.B, to: r.user.id, optional: false }),
    },
  }),
)
