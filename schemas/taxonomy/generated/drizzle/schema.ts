import {
  boolean,
  datetime,
  foreignKey,
  int,
  json,
  mysqlTable,
  text,
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

export const accounts = mysqlTable(
  'accounts',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: varchar('userId', { length: 191 }).notNull(),
    type: varchar('type', { length: 191 }).notNull(),
    provider: varchar('provider', { length: 191 }).notNull(),
    providerAccountId: varchar('providerAccountId', { length: 191 }).notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: int('expires_at'),
    token_type: varchar('token_type', { length: 191 }),
    scope: varchar('scope', { length: 191 }),
    id_token: text('id_token'),
    session_state: varchar('session_state', { length: 191 }),
    createdAt: datetime('created_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updated_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('accounts_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'accounts_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sessions = mysqlTable(
  'sessions',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    sessionToken: varchar('sessionToken', { length: 191 }).notNull(),
    userId: varchar('userId', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('sessions_sessionToken_key').on(table.sessionToken),
    foreignKey({
      name: 'sessions_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const users = mysqlTable(
  'users',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: varchar('name', { length: 191 }),
    email: varchar('email', { length: 191 }),
    emailVerified: datetime('emailVerified', { fsp: 3 }),
    image: varchar('image', { length: 191 }),
    createdAt: datetime('created_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updated_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    stripeCustomerId: varchar('stripe_customer_id', { length: 191 }),
    stripeSubscriptionId: varchar('stripe_subscription_id', { length: 191 }),
    stripePriceId: varchar('stripe_price_id', { length: 191 }),
    stripeCurrentPeriodEnd: datetime('stripe_current_period_end', { fsp: 3 }),
  },
  (table) => [
    uniqueIndex('users_email_key').on(table.email),
    uniqueIndex('users_stripe_customer_id_key').on(table.stripeCustomerId),
    uniqueIndex('users_stripe_subscription_id_key').on(table.stripeSubscriptionId),
  ],
)

export const verificationTokens = mysqlTable(
  'verification_tokens',
  {
    identifier: varchar('identifier', { length: 191 }).notNull(),
    token: varchar('token', { length: 191 }).notNull(),
    expires: datetime('expires', { fsp: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('verification_tokens_token_key').on(table.token),
    uniqueIndex('verification_tokens_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const posts = mysqlTable(
  'posts',
  {
    id: varchar('id', { length: 191 })
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: varchar('title', { length: 191 }).notNull(),
    content: json('content'),
    published: boolean('published').notNull().default(false),
    createdAt: datetime('created_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    updatedAt: datetime('updated_at', { fsp: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP(3)`)
      .$defaultFn(utcNow),
    authorId: varchar('authorId', { length: 191 }).notNull(),
  },
  (table) => [
    foreignKey({
      name: 'posts_authorId_fkey',
      columns: [table.authorId],
      foreignColumns: [users.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  { accounts, sessions, users, verificationTokens, posts },
  (r) => ({
    accounts: { user: r.one.users({ from: r.accounts.userId, to: r.users.id, optional: false }) },
    sessions: { user: r.one.users({ from: r.sessions.userId, to: r.users.id, optional: false }) },
    users: { accounts: r.many.accounts(), sessions: r.many.sessions(), Post: r.many.posts() },
    posts: { author: r.one.users({ from: r.posts.authorId, to: r.users.id, optional: false }) },
  }),
)
