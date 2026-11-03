import {
  customType,
  foreignKey,
  integer,
  primaryKey,
  sqliteTable,
  text,
  uniqueIndex,
} from 'drizzle-orm/sqlite-core'
import { defineRelations } from 'drizzle-orm'
import cuid from 'cuid'

const utcDateTime = customType<{ data: Date; driverData: string | number }>({
  dataType: () => 'datetime',
  toDriver: (value) => value.toISOString().replace('Z', '+00:00'),
  fromDriver: (value) => {
    if (typeof value === 'number' || /^-?\d+$/u.test(value)) return new Date(Number(value))
    const iso = value.replace(' ', 'T').replace(/ (?=[+-]\d\d:?\d\d$)/u, '')
    return new Date(/T[\d:.]+$/u.test(iso) ? `${iso}Z` : iso)
  },
})

export const user = sqliteTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name'),
    email: text('email').notNull(),
    emailVerified: utcDateTime('emailVerified'),
    image: text('image'),
  },
  (table) => [uniqueIndex('User_email_key').on(table.email)],
)

export const account = sqliteTable(
  'Account',
  {
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
  },
  (table) => [
    primaryKey({ name: 'Account_pkey', columns: [table.provider, table.providerAccountId] }),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = sqliteTable(
  'Session',
  {
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: utcDateTime('expires').notNull(),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = sqliteTable(
  'VerificationToken',
  {
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: utcDateTime('expires').notNull(),
  },
  (table) => [
    primaryKey({ name: 'VerificationToken_pkey', columns: [table.identifier, table.token] }),
    uniqueIndex('VerificationToken_token_key').on(table.token),
  ],
)

export const authenticator = sqliteTable(
  'Authenticator',
  {
    credentialID: text('credentialID').notNull(),
    userId: text('userId').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    credentialPublicKey: text('credentialPublicKey').notNull(),
    counter: integer('counter').notNull(),
    credentialDeviceType: text('credentialDeviceType').notNull(),
    credentialBackedUp: integer('credentialBackedUp', { mode: 'boolean' }).notNull(),
    transports: text('transports'),
  },
  (table) => [
    primaryKey({ name: 'Authenticator_pkey', columns: [table.userId, table.credentialID] }),
    uniqueIndex('Authenticator_credentialID_key').on(table.credentialID),
    foreignKey({
      name: 'Authenticator_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  { user, account, session, verificationToken, authenticator },
  (r) => ({
    user: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      Authenticator: r.many.authenticator(),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    authenticator: {
      user: r.one.user({ from: r.authenticator.userId, to: r.user.id, optional: false }),
    },
  }),
)
