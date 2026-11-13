import {
  bigint,
  boolean,
  customType,
  foreignKey,
  index,
  integer,
  jsonb,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
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

export const users = pgTable(
  'users',
  {
    id: serial('id').primaryKey(),
    email: text('email').notNull(),
    passwordHash: text('password_hash'),
    isVerified: integer('is_verified').notNull().default(0),
    verificationToken: text('verification_token'),
    verificationTokenExpiresAt: bigint('verification_token_expires_at', { mode: 'bigint' }),
    verificationResendCount: integer('verification_resend_count').notNull().default(0),
    resetPasswordToken: text('reset_password_token'),
    resetPasswordTokenExpiresAt: bigint('reset_password_token_expires_at', { mode: 'bigint' }),
    passkeyRecoveryToken: text('passkey_recovery_token'),
    passkeyRecoveryTokenExpiresAt: bigint('passkey_recovery_token_expires_at', { mode: 'bigint' }),
    loginToken: text('login_token'),
    loginTokenExpiresAt: bigint('login_token_expires_at', { mode: 'bigint' }),
    failedLoginAttempts: integer('failed_login_attempts').notNull().default(0),
    lockedUntil: bigint('locked_until', { mode: 'bigint' }),
    tokenVersion: integer('token_version').notNull().default(0),
    termsAcceptedAt: bigint('terms_accepted_at', { mode: 'bigint' }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    storageQuotaBytes: bigint('storage_quota_bytes', { mode: 'bigint' })
      .notNull()
      .default(sql`104857600`),
    storageUsedBytes: bigint('storage_used_bytes', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
  },
  (table) => [
    uniqueIndex('users_email_key').on(table.email),
    index('users_verification_token_idx').on(table.verificationToken),
    index('users_reset_password_token_idx').on(table.resetPasswordToken),
    index('users_passkey_recovery_token_idx').on(table.passkeyRecoveryToken),
    index('users_login_token_idx').on(table.loginToken),
  ],
)

export const passkeys = pgTable(
  'passkeys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    credentialId: bytea('credential_id').notNull(),
    publicKey: bytea('public_key').notNull(),
    counter: bigint('counter', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    transports: text('transports'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUsedAt: timestamp('last_used_at', { precision: 3 }),
    userId: integer('user_id').notNull(),
  },
  (table) => [
    uniqueIndex('passkeys_credential_id_key').on(table.credentialId),
    index('passkeys_user_id_idx').on(table.userId),
    foreignKey({
      name: 'passkeys_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const pendingPasskeyRegistrations = pgTable(
  'pending_passkey_registrations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    verificationToken: text('verification_token').notNull(),
    verificationTokenExpiresAt: bigint('verification_token_expires_at', {
      mode: 'bigint',
    }).notNull(),
    credentialId: bytea('credential_id').notNull(),
    publicKey: bytea('public_key').notNull(),
    counter: bigint('counter', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    transports: text('transports'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: integer('user_id').notNull(),
  },
  (table) => [
    uniqueIndex('pending_passkey_registrations_verification_token_key').on(table.verificationToken),
    index('pending_passkey_registrations_user_id_idx').on(table.userId),
    foreignKey({
      name: 'pending_passkey_registrations_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const operations = pgTable(
  'operations',
  {
    id: text('id').primaryKey(),
    userId: integer('user_id').notNull(),
    clientId: text('client_id').notNull(),
    serverSeq: integer('server_seq').notNull(),
    actionType: text('action_type').notNull(),
    opType: text('op_type').notNull(),
    entityType: text('entity_type').notNull(),
    entityId: text('entity_id'),
    entityIds: text('entity_ids').array().default([]),
    payload: jsonb('payload').notNull(),
    payloadBytes: bigint('payload_bytes', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    vectorClock: jsonb('vector_clock').notNull(),
    schemaVersion: integer('schema_version').notNull(),
    clientTimestamp: bigint('client_timestamp', { mode: 'bigint' }).notNull(),
    receivedAt: bigint('received_at', { mode: 'bigint' }).notNull(),
    isPayloadEncrypted: boolean('is_payload_encrypted').notNull().default(false),
    syncImportReason: text('sync_import_reason'),
    repairBaseServerSeq: integer('repair_base_server_seq'),
  },
  (table) => [
    index('operations_user_id_entity_type_entity_id_server_seq_idx').on(
      table.userId,
      table.entityType,
      table.entityId,
      table.serverSeq,
    ),
    index('operations_user_id_received_at_server_seq_idx').on(
      table.userId,
      table.receivedAt,
      table.serverSeq,
    ),
    index('operations_entity_ids_gin').on(table.entityIds),
    uniqueIndex('operations_user_id_server_seq_key').on(table.userId, table.serverSeq),
    foreignKey({
      name: 'operations_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userSyncState = pgTable(
  'user_sync_state',
  {
    userId: integer('user_id').primaryKey(),
    lastSeq: integer('last_seq').notNull().default(0),
    lastSnapshotSeq: integer('last_snapshot_seq'),
    snapshotData: bytea('snapshot_data'),
    snapshotAt: bigint('snapshot_at', { mode: 'bigint' }),
    snapshotSchemaVersion: integer('snapshot_schema_version').default(1),
    latestFullStateSeq: integer('latest_full_state_seq'),
    latestFullStateVectorClock: jsonb('latest_full_state_vector_clock'),
    latestStateReplacementSeq: integer('latest_state_replacement_seq'),
  },
  (table) => [
    foreignKey({
      name: 'user_sync_state_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const syncDevices = pgTable(
  'sync_devices',
  {
    clientId: text('client_id').notNull(),
    userId: integer('user_id').notNull(),
    deviceName: text('device_name'),
    userAgent: text('user_agent'),
    appVersion: text('app_version'),
    lastSeenAt: bigint('last_seen_at', { mode: 'bigint' }).notNull(),
    lastAckedSeq: integer('last_acked_seq').notNull().default(0),
    createdAt: bigint('created_at', { mode: 'bigint' }).notNull(),
  },
  (table) => [
    primaryKey({ name: 'sync_devices_pkey', columns: [table.userId, table.clientId] }),
    foreignKey({
      name: 'sync_devices_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const captureKeys = pgTable(
  'capture_keys',
  {
    userId: integer('user_id').primaryKey(),
    publicKey: text('public_key').notNull(),
    fingerprint: text('fingerprint').notNull(),
    createdAt: bigint('created_at', { mode: 'bigint' }).notNull(),
    disabledAt: bigint('disabled_at', { mode: 'bigint' }),
  },
  (table) => [
    foreignKey({
      name: 'capture_keys_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const captureGrants = pgTable(
  'capture_grants',
  {
    id: text('id').primaryKey(),
    userId: integer('user_id').notNull(),
    kind: text('kind').notNull(),
    clientName: text('client_name').notNull(),
    secretHash: text('secret_hash').notNull(),
    createdAt: bigint('created_at', { mode: 'bigint' }).notNull(),
    expiresAt: bigint('expires_at', { mode: 'bigint' }).notNull(),
    lastUsedAt: bigint('last_used_at', { mode: 'bigint' }),
    revokedAt: bigint('revoked_at', { mode: 'bigint' }),
    clientId: text('client_id'),
    resource: text('resource'),
    refreshHash: text('refresh_hash'),
    prevRefreshHash: text('prev_refresh_hash'),
    rotatedAt: bigint('rotated_at', { mode: 'bigint' }),
    refreshExpiresAt: bigint('refresh_expires_at', { mode: 'bigint' }),
  },
  (table) => [
    index('capture_grants_user_id_idx').on(table.userId),
    foreignKey({
      name: 'capture_grants_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const captureItems = pgTable(
  'capture_items',
  {
    id: text('id').primaryKey(),
    userId: integer('user_id').notNull(),
    grantId: text('grant_id').notNull(),
    ciphertext: text('ciphertext').notNull(),
    taskId: text('task_id').notNull(),
    createdAt: bigint('created_at', { mode: 'bigint' }).notNull(),
    leaseDeviceId: text('lease_device_id'),
    leaseUntil: bigint('lease_until', { mode: 'bigint' }),
  },
  (table) => [
    index('capture_items_user_id_created_at_idx').on(table.userId, table.createdAt),
    index('capture_items_grant_id_idx').on(table.grantId),
    foreignKey({
      name: 'capture_items_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const captureConnectCodes = pgTable(
  'capture_connect_codes',
  {
    userId: integer('user_id').primaryKey(),
    codeHash: text('code_hash').notNull(),
    tokenVersion: integer('token_version').notNull(),
    expiresAt: bigint('expires_at', { mode: 'bigint' }).notNull(),
  },
  (table) => [
    uniqueIndex('capture_connect_codes_code_hash_key').on(table.codeHash),
    foreignKey({
      name: 'capture_connect_codes_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const captureAuth = pgTable(
  'capture_auth',
  {
    id: text('id').primaryKey(),
    userId: integer('user_id'),
    clientId: text('client_id').notNull(),
    clientName: text('client_name').notNull(),
    redirectUri: text('redirect_uri').notNull(),
    codeChallenge: text('code_challenge').notNull(),
    resource: text('resource').notNull(),
    state: text('state').notNull(),
    scope: text('scope').notNull(),
    codeHash: text('code_hash'),
    tokenVersion: integer('token_version'),
    expiresAt: bigint('expires_at', { mode: 'bigint' }).notNull(),
  },
  (table) => [
    uniqueIndex('capture_auth_code_hash_key').on(table.codeHash),
    index('capture_auth_user_id_idx').on(table.userId),
    index('capture_auth_expires_at_idx').on(table.expiresAt),
    foreignKey({
      name: 'capture_auth_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const connectRequests = pgTable(
  'connect_requests',
  {
    id: text('id').primaryKey(),
    pollSecretHash: text('poll_secret_hash').notNull(),
    matchCode: text('match_code').notNull(),
    deviceName: text('device_name').notNull(),
    platform: text('platform').notNull(),
    status: text('status').notNull().default('pending'),
    userId: integer('user_id'),
    tokenVersion: integer('token_version'),
    expiresAt: bigint('expires_at', { mode: 'bigint' }).notNull(),
  },
  (table) => [
    index('connect_requests_user_id_idx').on(table.userId),
    index('connect_requests_expires_at_idx').on(table.expiresAt),
    foreignKey({
      name: 'connect_requests_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    users,
    passkeys,
    pendingPasskeyRegistrations,
    operations,
    userSyncState,
    syncDevices,
    captureKeys,
    captureGrants,
    captureItems,
    captureConnectCodes,
    captureAuth,
    connectRequests,
  },
  (r) => ({
    users: {
      operations: r.many.operations(),
      syncState: r.one.userSyncState({ from: r.users.id, to: r.userSyncState.userId }),
      devices: r.many.syncDevices(),
      passkeys: r.many.passkeys(),
      pendingPasskeyRegistrations: r.many.pendingPasskeyRegistrations(),
      captureKey: r.one.captureKeys({ from: r.users.id, to: r.captureKeys.userId }),
      captureGrants: r.many.captureGrants(),
      captureItems: r.many.captureItems(),
      captureConnectCode: r.one.captureConnectCodes({
        from: r.users.id,
        to: r.captureConnectCodes.userId,
      }),
      captureAuthorizations: r.many.captureAuth(),
      connectRequests: r.many.connectRequests(),
    },
    passkeys: { user: r.one.users({ from: r.passkeys.userId, to: r.users.id, optional: false }) },
    pendingPasskeyRegistrations: {
      user: r.one.users({
        from: r.pendingPasskeyRegistrations.userId,
        to: r.users.id,
        optional: false,
      }),
    },
    operations: {
      user: r.one.users({ from: r.operations.userId, to: r.users.id, optional: false }),
    },
    userSyncState: {
      user: r.one.users({ from: r.userSyncState.userId, to: r.users.id, optional: false }),
    },
    syncDevices: {
      user: r.one.users({ from: r.syncDevices.userId, to: r.users.id, optional: false }),
    },
    captureKeys: {
      user: r.one.users({ from: r.captureKeys.userId, to: r.users.id, optional: false }),
    },
    captureGrants: {
      user: r.one.users({ from: r.captureGrants.userId, to: r.users.id, optional: false }),
    },
    captureItems: {
      user: r.one.users({ from: r.captureItems.userId, to: r.users.id, optional: false }),
    },
    captureConnectCodes: {
      user: r.one.users({ from: r.captureConnectCodes.userId, to: r.users.id, optional: false }),
    },
    captureAuth: { user: r.one.users({ from: r.captureAuth.userId, to: r.users.id }) },
    connectRequests: { user: r.one.users({ from: r.connectRequests.userId, to: r.users.id }) },
  }),
)
