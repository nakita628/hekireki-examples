import {
  boolean,
  char,
  customType,
  foreignKey,
  index,
  integer,
  jsonb,
  numeric,
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

export const user = pgTable(
  'user',
  {
    id: uuid('user_id').primaryKey(),
    username: varchar('username', { length: 255 }).notNull(),
    password: varchar('password', { length: 60 }).notNull(),
    role: varchar('role', { length: 50 }).notNull(),
    logoUrl: varchar('logo_url', { length: 2183 }),
    displayName: varchar('display_name', { length: 255 }),
    twoFactorRequired: boolean('two_factor_required').notNull().default(false),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { withTimezone: true, precision: 6 }),
  },
  (table) => [uniqueIndex('user_username_key').on(table.username)],
)

export const apiKey = pgTable(
  'api_key',
  {
    id: uuid('api_key_id').primaryKey(),
    userId: uuid('user_id').notNull(),
    name: varchar('name', { length: 255 }).notNull(),
    keyHash: varchar('key_hash', { length: 128 }).notNull(),
    keyPrefix: varchar('key_prefix', { length: 20 }).notNull(),
    lastUsedAt: timestamp('last_used_at', { withTimezone: true, precision: 6 }),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('api_key_key_hash_key').on(table.keyHash),
    index('api_key_user_id_idx').on(table.userId),
    foreignKey({ name: 'api_key_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = pgTable(
  'session',
  {
    id: uuid('session_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    browser: varchar('browser', { length: 20 }),
    os: varchar('os', { length: 20 }),
    device: varchar('device', { length: 20 }),
    screen: varchar('screen', { length: 11 }),
    language: varchar('language', { length: 35 }),
    country: char('country', { length: 2 }),
    region: varchar('region', { length: 20 }),
    city: varchar('city', { length: 50 }),
    distinctId: varchar('distinct_id', { length: 50 }),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('session_created_at_idx').on(table.createdAt),
    index('session_website_id_idx').on(table.websiteId),
    index('session_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('session_website_id_created_at_browser_idx').on(
      table.websiteId,
      table.createdAt,
      table.browser,
    ),
    index('session_website_id_created_at_os_idx').on(table.websiteId, table.createdAt, table.os),
    index('session_website_id_created_at_device_idx').on(
      table.websiteId,
      table.createdAt,
      table.device,
    ),
    index('session_website_id_created_at_screen_idx').on(
      table.websiteId,
      table.createdAt,
      table.screen,
    ),
    index('session_website_id_created_at_language_idx').on(
      table.websiteId,
      table.createdAt,
      table.language,
    ),
    index('session_website_id_created_at_country_idx').on(
      table.websiteId,
      table.createdAt,
      table.country,
    ),
    index('session_website_id_created_at_region_idx').on(
      table.websiteId,
      table.createdAt,
      table.region,
    ),
    index('session_website_id_created_at_city_idx').on(
      table.websiteId,
      table.createdAt,
      table.city,
    ),
  ],
)

export const sessionLink = pgTable(
  'session_link',
  {
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    distinctId: varchar('distinct_id', { length: 50 }).notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'session_link_pkey',
      columns: [table.websiteId, table.distinctId, table.sessionId],
    }),
    index('session_link_website_id_session_id_idx').on(table.websiteId, table.sessionId),
  ],
)

export const website = pgTable(
  'website',
  {
    id: uuid('website_id').primaryKey(),
    name: varchar('name', { length: 100 }).notNull(),
    domain: varchar('domain', { length: 500 }),
    resetAt: timestamp('reset_at', { withTimezone: true, precision: 6 }),
    userId: uuid('user_id'),
    teamId: uuid('team_id'),
    createdBy: uuid('created_by'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { withTimezone: true, precision: 6 }),
    recorderEnabled: boolean('recorder_enabled').notNull().default(false),
    replayConfig: jsonb('replay_config'),
  },
  (table) => [
    index('website_user_id_idx').on(table.userId),
    index('website_team_id_idx').on(table.teamId),
    index('website_created_at_idx').on(table.createdAt),
    index('website_created_by_idx').on(table.createdBy),
    foreignKey({ name: 'website_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'website_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'website_team_id_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const websiteEvent = pgTable(
  'website_event',
  {
    id: uuid('event_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    visitId: uuid('visit_id').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    urlPath: varchar('url_path', { length: 500 }).notNull(),
    urlQuery: varchar('url_query', { length: 500 }),
    utmSource: varchar('utm_source', { length: 255 }),
    utmMedium: varchar('utm_medium', { length: 255 }),
    utmCampaign: varchar('utm_campaign', { length: 255 }),
    utmContent: varchar('utm_content', { length: 255 }),
    utmTerm: varchar('utm_term', { length: 255 }),
    referrerPath: varchar('referrer_path', { length: 500 }),
    referrerQuery: varchar('referrer_query', { length: 500 }),
    referrerDomain: varchar('referrer_domain', { length: 500 }),
    pageTitle: varchar('page_title', { length: 500 }),
    gclid: varchar('gclid', { length: 255 }),
    fbclid: varchar('fbclid', { length: 255 }),
    msclkid: varchar('msclkid', { length: 255 }),
    ttclid: varchar('ttclid', { length: 255 }),
    lifatid: varchar('li_fat_id', { length: 255 }),
    twclid: varchar('twclid', { length: 255 }),
    eventType: integer('event_type').notNull().default(1),
    eventName: varchar('event_name', { length: 50 }),
    tag: varchar('tag', { length: 50 }),
    hostname: varchar('hostname', { length: 100 }),
    lcp: numeric('lcp', { precision: 10, scale: 1 }),
    inp: numeric('inp', { precision: 10, scale: 1 }),
    cls: numeric('cls', { precision: 10, scale: 4 }),
    fcp: numeric('fcp', { precision: 10, scale: 1 }),
    ttfb: numeric('ttfb', { precision: 10, scale: 1 }),
  },
  (table) => [
    index('website_event_created_at_idx').on(table.createdAt),
    index('website_event_session_id_idx').on(table.sessionId),
    index('website_event_visit_id_idx').on(table.visitId),
    index('website_event_website_id_idx').on(table.websiteId),
    index('website_event_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('website_event_website_id_created_at_url_path_idx').on(
      table.websiteId,
      table.createdAt,
      table.urlPath,
    ),
    index('website_event_website_id_created_at_url_query_idx').on(
      table.websiteId,
      table.createdAt,
      table.urlQuery,
    ),
    index('website_event_website_id_created_at_referrer_domain_idx').on(
      table.websiteId,
      table.createdAt,
      table.referrerDomain,
    ),
    index('website_event_website_id_created_at_page_title_idx').on(
      table.websiteId,
      table.createdAt,
      table.pageTitle,
    ),
    index('website_event_website_id_created_at_event_name_idx').on(
      table.websiteId,
      table.createdAt,
      table.eventName,
    ),
    index('website_event_website_id_created_at_tag_idx').on(
      table.websiteId,
      table.createdAt,
      table.tag,
    ),
    index('website_event_website_id_session_id_created_at_idx').on(
      table.websiteId,
      table.sessionId,
      table.createdAt,
    ),
    index('website_event_website_id_visit_id_created_at_idx').on(
      table.websiteId,
      table.visitId,
      table.createdAt,
    ),
    index('website_event_website_id_created_at_hostname_idx').on(
      table.websiteId,
      table.createdAt,
      table.hostname,
    ),
    foreignKey({
      name: 'website_event_session_id_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const eventData = pgTable(
  'event_data',
  {
    id: uuid('event_data_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    websiteEventId: uuid('website_event_id').notNull(),
    dataKey: varchar('data_key', { length: 500 }).notNull(),
    stringValue: varchar('string_value', { length: 500 }),
    numberValue: numeric('number_value', { precision: 19, scale: 4 }),
    dateValue: timestamp('date_value', { withTimezone: true, precision: 6 }),
    dataType: integer('data_type').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('event_data_created_at_idx').on(table.createdAt),
    index('event_data_website_id_idx').on(table.websiteId),
    index('event_data_website_event_id_idx').on(table.websiteEventId),
    index('event_data_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('event_data_website_id_created_at_data_key_idx').on(
      table.websiteId,
      table.createdAt,
      table.dataKey,
    ),
    foreignKey({
      name: 'event_data_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'event_data_website_event_id_fkey',
      columns: [table.websiteEventId],
      foreignColumns: [websiteEvent.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const sessionData = pgTable(
  'session_data',
  {
    id: uuid('session_data_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    dataKey: varchar('data_key', { length: 500 }).notNull(),
    stringValue: varchar('string_value', { length: 500 }),
    numberValue: numeric('number_value', { precision: 19, scale: 4 }),
    dateValue: timestamp('date_value', { withTimezone: true, precision: 6 }),
    dataType: integer('data_type').notNull(),
    distinctId: varchar('distinct_id', { length: 50 }),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('session_data_created_at_idx').on(table.createdAt),
    index('session_data_website_id_idx').on(table.websiteId),
    index('session_data_session_id_idx').on(table.sessionId),
    index('session_data_session_id_created_at_idx').on(table.sessionId, table.createdAt),
    index('session_data_website_id_created_at_data_key_idx').on(
      table.websiteId,
      table.createdAt,
      table.dataKey,
    ),
    uniqueIndex('session_data_session_id_data_key_key').on(table.sessionId, table.dataKey),
    foreignKey({
      name: 'session_data_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'session_data_session_id_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const team = pgTable(
  'team',
  {
    id: uuid('team_id').primaryKey(),
    name: varchar('name', { length: 50 }).notNull(),
    accessCode: varchar('access_code', { length: 50 }),
    logoUrl: varchar('logo_url', { length: 2183 }),
    twoFactorRequired: boolean('two_factor_required').notNull().default(false),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { withTimezone: true, precision: 6 }),
  },
  (table) => [
    uniqueIndex('team_access_code_key').on(table.accessCode),
    index('team_access_code_idx').on(table.accessCode),
  ],
)

export const teamUser = pgTable(
  'team_user',
  {
    id: uuid('team_user_id').primaryKey(),
    teamId: uuid('team_id').notNull(),
    userId: uuid('user_id').notNull(),
    role: varchar('role', { length: 50 }).notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('team_user_team_id_idx').on(table.teamId),
    index('team_user_user_id_idx').on(table.userId),
    foreignKey({
      name: 'team_user_team_id_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'team_user_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const report = pgTable(
  'report',
  {
    id: uuid('report_id').primaryKey(),
    userId: uuid('user_id').notNull(),
    websiteId: uuid('website_id').notNull(),
    type: varchar('type', { length: 50 }).notNull(),
    name: varchar('name', { length: 200 }).notNull(),
    description: varchar('description', { length: 500 }).notNull(),
    parameters: jsonb('parameters').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('report_user_id_idx').on(table.userId),
    index('report_website_id_idx').on(table.websiteId),
    index('report_type_idx').on(table.type),
    index('report_name_idx').on(table.name),
    foreignKey({ name: 'report_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'report_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const annotation = pgTable(
  'annotation',
  {
    id: uuid('annotation_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    userId: uuid('user_id'),
    date: timestamp('date', { withTimezone: true, precision: 6 }).notNull(),
    allDay: boolean('all_day').notNull().default(true),
    note: varchar('note', { length: 500 }).notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('annotation_website_id_date_idx').on(table.websiteId, table.date),
    foreignKey({
      name: 'annotation_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const segment = pgTable(
  'segment',
  {
    id: uuid('segment_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    type: varchar('type', { length: 50 }).notNull(),
    name: varchar('name', { length: 200 }).notNull(),
    parameters: jsonb('parameters').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('segment_website_id_idx').on(table.websiteId),
    foreignKey({
      name: 'segment_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const revenue = pgTable(
  'revenue',
  {
    id: uuid('revenue_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    eventId: uuid('event_id').notNull(),
    eventName: varchar('event_name', { length: 50 }).notNull(),
    currency: varchar('currency', { length: 10 }).notNull(),
    revenue: numeric('revenue', { precision: 19, scale: 4 }),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('revenue_website_id_idx').on(table.websiteId),
    index('revenue_session_id_idx').on(table.sessionId),
    index('revenue_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('revenue_website_id_session_id_created_at_idx').on(
      table.websiteId,
      table.sessionId,
      table.createdAt,
    ),
    foreignKey({
      name: 'revenue_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'revenue_session_id_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const link = pgTable(
  'link',
  {
    id: uuid('link_id').primaryKey(),
    name: varchar('name', { length: 100 }).notNull(),
    url: varchar('url', { length: 500 }).notNull(),
    slug: varchar('slug', { length: 100 }).notNull(),
    userId: uuid('user_id'),
    teamId: uuid('team_id'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { withTimezone: true, precision: 6 }),
  },
  (table) => [
    uniqueIndex('link_slug_key').on(table.slug),
    index('link_slug_idx').on(table.slug),
    index('link_user_id_idx').on(table.userId),
    index('link_team_id_idx').on(table.teamId),
    index('link_created_at_idx').on(table.createdAt),
    foreignKey({ name: 'link_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'link_team_id_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const pixel = pgTable(
  'pixel',
  {
    id: uuid('pixel_id').primaryKey(),
    name: varchar('name', { length: 100 }).notNull(),
    slug: varchar('slug', { length: 100 }).notNull(),
    userId: uuid('user_id'),
    teamId: uuid('team_id'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
    deletedAt: timestamp('deleted_at', { withTimezone: true, precision: 6 }),
  },
  (table) => [
    uniqueIndex('pixel_slug_key').on(table.slug),
    index('pixel_slug_idx').on(table.slug),
    index('pixel_user_id_idx').on(table.userId),
    index('pixel_team_id_idx').on(table.teamId),
    index('pixel_created_at_idx').on(table.createdAt),
    foreignKey({ name: 'pixel_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'pixel_team_id_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const board = pgTable(
  'board',
  {
    id: uuid('board_id').primaryKey(),
    type: varchar('type', { length: 50 }).notNull(),
    name: varchar('name', { length: 200 }).notNull(),
    description: varchar('description', { length: 500 }).notNull(),
    parameters: jsonb('parameters').notNull(),
    userId: uuid('user_id'),
    teamId: uuid('team_id'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('board_user_id_idx').on(table.userId),
    index('board_team_id_idx').on(table.teamId),
    index('board_created_at_idx').on(table.createdAt),
    foreignKey({ name: 'board_user_id_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'board_team_id_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const share = pgTable(
  'share',
  {
    id: uuid('share_id').primaryKey(),
    entityId: uuid('entity_id').notNull(),
    name: varchar('name', { length: 200 }).notNull(),
    shareType: integer('share_type').notNull(),
    slug: varchar('slug', { length: 100 }).notNull(),
    parameters: jsonb('parameters').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('share_slug_key').on(table.slug),
    index('share_entity_id_idx').on(table.entityId),
  ],
)

export const sessionReplay = pgTable(
  'session_replay',
  {
    id: uuid('replay_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    visitId: uuid('visit_id').notNull(),
    chunkIndex: integer('chunk_index').notNull(),
    events: bytea('events').notNull(),
    eventCount: integer('event_count').notNull(),
    startedAt: timestamp('started_at', { withTimezone: true, precision: 6 }).notNull(),
    endedAt: timestamp('ended_at', { withTimezone: true, precision: 6 }).notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('session_replay_website_id_idx').on(table.websiteId),
    index('session_replay_session_id_idx').on(table.sessionId),
    index('session_replay_visit_id_idx').on(table.visitId),
    index('session_replay_website_id_session_id_idx').on(table.websiteId, table.sessionId),
    index('session_replay_website_id_visit_id_idx').on(table.websiteId, table.visitId),
    index('session_replay_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('session_replay_session_id_chunk_index_idx').on(table.sessionId, table.chunkIndex),
    foreignKey({
      name: 'session_replay_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const sessionReplaySaved = pgTable(
  'session_replay_saved',
  {
    id: uuid('saved_replay_id').primaryKey(),
    name: varchar('name', { length: 100 }).notNull(),
    websiteId: uuid('website_id').notNull(),
    visitId: uuid('visit_id').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 }).$onUpdate(utcNow),
  },
  (table) => [
    index('session_replay_saved_website_id_idx').on(table.websiteId),
    index('session_replay_saved_visit_id_idx').on(table.visitId),
    index('session_replay_saved_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    uniqueIndex('session_replay_saved_website_id_visit_id_key').on(table.websiteId, table.visitId),
    foreignKey({
      name: 'session_replay_saved_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const heatmapEvent = pgTable(
  'heatmap_event',
  {
    id: uuid('heatmap_event_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    visitId: uuid('visit_id').notNull(),
    urlPath: varchar('url_path', { length: 500 }).notNull(),
    eventType: integer('event_type').notNull(),
    x: integer('x'),
    y: integer('y'),
    pageX: integer('page_x'),
    pageY: integer('page_y'),
    pageW: integer('page_w'),
    viewportW: integer('viewport_w'),
    viewportH: integer('viewport_h'),
    pageH: integer('page_h'),
    scrollPct: integer('scroll_pct'),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('heatmap_event_website_id_idx').on(table.websiteId),
    index('heatmap_event_visit_id_idx').on(table.visitId),
    index('heatmap_event_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('heatmap_event_website_id_url_path_event_type_created_at_idx').on(
      table.websiteId,
      table.urlPath,
      table.eventType,
      table.createdAt,
    ),
    foreignKey({
      name: 'heatmap_event_website_id_fkey',
      columns: [table.websiteId],
      foreignColumns: [website.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const twoFactorAuth = pgTable(
  'two_factor_auth',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: uuid('user_id').notNull(),
    secret: text('secret').notNull(),
    isEnabled: boolean('is_enabled').notNull().default(false),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('two_factor_auth_user_id_key').on(table.userId),
    foreignKey({
      name: 'two_factor_auth_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const twoFactorBackupCode = pgTable(
  'two_factor_backup_code',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: uuid('user_id').notNull(),
    codeHash: text('code_hash').notNull(),
    used: boolean('used').notNull().default(false),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('two_factor_backup_code_user_id_idx').on(table.userId),
    foreignKey({
      name: 'two_factor_backup_code_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const twoFactorOtpUsed = pgTable(
  'two_factor_otp_used',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: uuid('user_id').notNull(),
    otp: text('otp').notNull(),
    expiresAt: timestamp('expires_at', { withTimezone: true, precision: 6 }).notNull(),
  },
  (table) => [
    uniqueIndex('two_factor_otp_used_user_id_otp_key').on(table.userId, table.otp),
    foreignKey({
      name: 'two_factor_otp_used_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const twoFactorRateLimit = pgTable(
  'two_factor_rate_limit',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: uuid('user_id').notNull(),
    attempts: integer('attempts').notNull().default(0),
    lockedUntil: timestamp('locked_until', { withTimezone: true, precision: 6 }),
    updatedAt: timestamp('updated_at', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('two_factor_rate_limit_user_id_key').on(table.userId),
    foreignKey({
      name: 'two_factor_rate_limit_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const appSetting = pgTable('app_setting', {
  key: text('key').primaryKey(),
  value: text('value').notNull(),
})

export const errorIssue = pgTable(
  'error_issue',
  {
    id: uuid('issue_id').primaryKey(),
    websiteId: uuid('website_id').notNull(),
    fingerprint: varchar('fingerprint', { length: 64 }).notNull(),
    groupingVersion: integer('grouping_version').notNull().default(1),
    title: varchar('title', { length: 2200 }).notNull(),
    status: varchar('status', { length: 20 }).notNull().default('unresolved'),
    resolvedAt: timestamp('resolved_at', { withTimezone: true, precision: 6 }),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('error_issue_website_id_status_idx').on(table.websiteId, table.status),
    uniqueIndex('error_issue_website_id_grouping_version_fingerprint_key').on(
      table.websiteId,
      table.groupingVersion,
      table.fingerprint,
    ),
  ],
)

export const errorEvent = pgTable(
  'error_event',
  {
    websiteId: uuid('website_id').notNull(),
    id: uuid('event_id').notNull(),
    issueId: uuid('issue_id').notNull(),
    sessionId: uuid('session_id').notNull(),
    visitId: uuid('visit_id').notNull(),
    createdAt: timestamp('created_at', { withTimezone: true, precision: 6 }).notNull(),
    receivedAt: timestamp('received_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    name: varchar('name', { length: 200 }).notNull(),
    message: varchar('message', { length: 2000 }).notNull(),
    stack: text('stack').notNull(),
    frames: jsonb('frames').notNull(),
    tags: jsonb('tags').notNull(),
    handled: boolean('handled').notNull(),
    urlPath: varchar('url_path', { length: 500 }).notNull(),
    release: varchar('release', { length: 100 }).notNull(),
    environment: varchar('environment', { length: 50 }).notNull(),
    browser: varchar('browser', { length: 50 }).notNull(),
    os: varchar('os', { length: 50 }).notNull(),
    device: varchar('device', { length: 50 }).notNull(),
  },
  (table) => [
    primaryKey({ name: 'error_event_pkey', columns: [table.websiteId, table.id] }),
    index('error_event_website_id_created_at_idx').on(table.websiteId, table.createdAt),
    index('error_event_website_id_issue_id_created_at_idx').on(
      table.websiteId,
      table.issueId,
      table.createdAt,
    ),
  ],
)

export const errorRateLimit = pgTable('error_rate_limit', {
  websiteId: uuid('website_id').primaryKey(),
  bucket: timestamp('bucket', { withTimezone: true, precision: 6 }).notNull(),
  count: integer('count').notNull(),
})

export const relations = defineRelations(
  {
    user,
    apiKey,
    session,
    sessionLink,
    website,
    websiteEvent,
    eventData,
    sessionData,
    team,
    teamUser,
    report,
    annotation,
    segment,
    revenue,
    link,
    pixel,
    board,
    share,
    sessionReplay,
    sessionReplaySaved,
    heatmapEvent,
    twoFactorAuth,
    twoFactorBackupCode,
    twoFactorOtpUsed,
    twoFactorRateLimit,
    appSetting,
    errorIssue,
    errorEvent,
    errorRateLimit,
  },
  (r) => ({
    user: {
      websites: r.many.website({ alias: 'user' }),
      createdBy: r.many.website({ alias: 'createUser' }),
      links: r.many.link(),
      pixels: r.many.pixel(),
      teams: r.many.teamUser(),
      reports: r.many.report(),
      boards: r.many.board(),
      twoFactorAuth: r.one.twoFactorAuth({ from: r.user.id, to: r.twoFactorAuth.userId }),
      twoFactorBackupCodes: r.many.twoFactorBackupCode(),
      twoFactorOtpUseds: r.many.twoFactorOtpUsed(),
      twoFactorRateLimit: r.one.twoFactorRateLimit({
        from: r.user.id,
        to: r.twoFactorRateLimit.userId,
      }),
      apiKeys: r.many.apiKey(),
    },
    apiKey: { user: r.one.user({ from: r.apiKey.userId, to: r.user.id, optional: false }) },
    session: {
      websiteEvents: r.many.websiteEvent(),
      sessionData: r.many.sessionData(),
      revenue: r.many.revenue(),
    },
    website: {
      user: r.one.user({ from: r.website.userId, to: r.user.id, alias: 'user' }),
      createUser: r.one.user({ from: r.website.createdBy, to: r.user.id, alias: 'createUser' }),
      team: r.one.team({ from: r.website.teamId, to: r.team.id }),
      annotations: r.many.annotation(),
      eventData: r.many.eventData(),
      reports: r.many.report(),
      revenue: r.many.revenue(),
      segments: r.many.segment(),
      sessionData: r.many.sessionData(),
      sessionReplays: r.many.sessionReplay(),
      sessionReplaysSaved: r.many.sessionReplaySaved(),
      heatmapEvents: r.many.heatmapEvent(),
    },
    websiteEvent: {
      eventData: r.many.eventData(),
      session: r.one.session({ from: r.websiteEvent.sessionId, to: r.session.id, optional: false }),
    },
    eventData: {
      website: r.one.website({ from: r.eventData.websiteId, to: r.website.id, optional: false }),
      websiteEvent: r.one.websiteEvent({
        from: r.eventData.websiteEventId,
        to: r.websiteEvent.id,
        optional: false,
      }),
    },
    sessionData: {
      website: r.one.website({ from: r.sessionData.websiteId, to: r.website.id, optional: false }),
      session: r.one.session({ from: r.sessionData.sessionId, to: r.session.id, optional: false }),
    },
    team: {
      websites: r.many.website(),
      members: r.many.teamUser(),
      links: r.many.link(),
      pixels: r.many.pixel(),
      boards: r.many.board(),
    },
    teamUser: {
      team: r.one.team({ from: r.teamUser.teamId, to: r.team.id, optional: false }),
      user: r.one.user({ from: r.teamUser.userId, to: r.user.id, optional: false }),
    },
    report: {
      user: r.one.user({ from: r.report.userId, to: r.user.id, optional: false }),
      website: r.one.website({ from: r.report.websiteId, to: r.website.id, optional: false }),
    },
    annotation: {
      website: r.one.website({ from: r.annotation.websiteId, to: r.website.id, optional: false }),
    },
    segment: {
      website: r.one.website({ from: r.segment.websiteId, to: r.website.id, optional: false }),
    },
    revenue: {
      website: r.one.website({ from: r.revenue.websiteId, to: r.website.id, optional: false }),
      session: r.one.session({ from: r.revenue.sessionId, to: r.session.id, optional: false }),
    },
    link: {
      user: r.one.user({ from: r.link.userId, to: r.user.id }),
      team: r.one.team({ from: r.link.teamId, to: r.team.id }),
    },
    pixel: {
      user: r.one.user({ from: r.pixel.userId, to: r.user.id }),
      team: r.one.team({ from: r.pixel.teamId, to: r.team.id }),
    },
    board: {
      user: r.one.user({ from: r.board.userId, to: r.user.id }),
      team: r.one.team({ from: r.board.teamId, to: r.team.id }),
    },
    sessionReplay: {
      website: r.one.website({
        from: r.sessionReplay.websiteId,
        to: r.website.id,
        optional: false,
      }),
    },
    sessionReplaySaved: {
      website: r.one.website({
        from: r.sessionReplaySaved.websiteId,
        to: r.website.id,
        optional: false,
      }),
    },
    heatmapEvent: {
      website: r.one.website({ from: r.heatmapEvent.websiteId, to: r.website.id, optional: false }),
    },
    twoFactorAuth: {
      user: r.one.user({ from: r.twoFactorAuth.userId, to: r.user.id, optional: false }),
    },
    twoFactorBackupCode: {
      user: r.one.user({ from: r.twoFactorBackupCode.userId, to: r.user.id, optional: false }),
    },
    twoFactorOtpUsed: {
      user: r.one.user({ from: r.twoFactorOtpUsed.userId, to: r.user.id, optional: false }),
    },
    twoFactorRateLimit: {
      user: r.one.user({ from: r.twoFactorRateLimit.userId, to: r.user.id, optional: false }),
    },
  }),
)
