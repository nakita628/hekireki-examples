import {
  type PgTableExtraConfigValue,
  bigint,
  boolean,
  customType,
  foreignKey,
  index,
  integer,
  jsonb,
  numeric,
  pgEnum,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
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

export const identityProviderEnum = pgEnum('IdentityProvider', ['DOCUMENSO', 'GOOGLE', 'OIDC'])

export const roleEnum = pgEnum('Role', ['ADMIN', 'USER'])

export const userSecurityAuditLogTypeEnum = pgEnum('UserSecurityAuditLogType', [
  'ACCOUNT_PROFILE_UPDATE',
  'ACCOUNT_SSO_LINK',
  'ACCOUNT_SSO_UNLINK',
  'ORGANISATION_SSO_LINK',
  'ORGANISATION_SSO_UNLINK',
  'AUTH_2FA_DISABLE',
  'AUTH_2FA_ENABLE',
  'PASSKEY_CREATED',
  'PASSKEY_DELETED',
  'PASSKEY_UPDATED',
  'PASSWORD_RESET',
  'PASSWORD_UPDATE',
  'SESSION_REVOKED',
  'SIGN_OUT',
  'SIGN_IN',
  'SIGN_IN_FAIL',
  'SIGN_IN_2FA_FAIL',
  'SIGN_IN_PASSKEY_FAIL',
])

export const webhookTriggerEventsEnum = pgEnum('WebhookTriggerEvents', [
  'DOCUMENT_CREATED',
  'DOCUMENT_SENT',
  'DOCUMENT_OPENED',
  'DOCUMENT_SIGNED',
  'DOCUMENT_COMPLETED',
  'DOCUMENT_REJECTED',
  'DOCUMENT_CANCELLED',
  'RECIPIENT_EXPIRED',
  'DOCUMENT_RECIPIENT_COMPLETED',
  'DOCUMENT_REMINDER_SENT',
  'TEMPLATE_CREATED',
  'TEMPLATE_UPDATED',
  'TEMPLATE_DELETED',
  'TEMPLATE_USED',
])

export const webhookCallStatusEnum = pgEnum('WebhookCallStatus', ['SUCCESS', 'FAILED'])

export const apiTokenAlgorithmEnum = pgEnum('ApiTokenAlgorithm', ['SHA512'])

export const subscriptionStatusEnum = pgEnum('SubscriptionStatus', [
  'ACTIVE',
  'PAST_DUE',
  'INACTIVE',
])

export const documentStatusEnum = pgEnum('DocumentStatus', [
  'DRAFT',
  'PENDING',
  'COMPLETED',
  'REJECTED',
  'CANCELLED',
])

export const documentSourceEnum = pgEnum('DocumentSource', [
  'DOCUMENT',
  'TEMPLATE',
  'TEMPLATE_DIRECT_LINK',
])

export const documentVisibilityEnum = pgEnum('DocumentVisibility', [
  'EVERYONE',
  'MANAGER_AND_ABOVE',
  'ADMIN',
])

export const folderTypeEnum = pgEnum('FolderType', ['DOCUMENT', 'TEMPLATE'])

export const envelopeTypeEnum = pgEnum('EnvelopeType', ['DOCUMENT', 'TEMPLATE'])

export const documentDataTypeEnum = pgEnum('DocumentDataType', ['S3_PATH', 'BYTES', 'BYTES_64'])

export const documentSigningOrderEnum = pgEnum('DocumentSigningOrder', ['PARALLEL', 'SEQUENTIAL'])

export const documentDistributionMethodEnum = pgEnum('DocumentDistributionMethod', [
  'EMAIL',
  'NONE',
])

export const readStatusEnum = pgEnum('ReadStatus', ['NOT_OPENED', 'OPENED'])

export const sendStatusEnum = pgEnum('SendStatus', ['NOT_SENT', 'SENT'])

export const signingStatusEnum = pgEnum('SigningStatus', ['NOT_SIGNED', 'SIGNED', 'REJECTED'])

export const recipientRoleEnum = pgEnum('RecipientRole', [
  'CC',
  'SIGNER',
  'VIEWER',
  'APPROVER',
  'ASSISTANT',
])

export const fieldTypeEnum = pgEnum('FieldType', [
  'SIGNATURE',
  'FREE_SIGNATURE',
  'INITIALS',
  'NAME',
  'EMAIL',
  'DATE',
  'TEXT',
  'NUMBER',
  'RADIO',
  'CHECKBOX',
  'DROPDOWN',
])

export const organisationTypeEnum = pgEnum('OrganisationType', ['PERSONAL', 'ORGANISATION'])

export const organisationGroupTypeEnum = pgEnum('OrganisationGroupType', [
  'INTERNAL_ORGANISATION',
  'INTERNAL_TEAM',
  'CUSTOM',
])

export const organisationMemberRoleEnum = pgEnum('OrganisationMemberRole', [
  'ADMIN',
  'MANAGER',
  'MEMBER',
])

export const teamMemberRoleEnum = pgEnum('TeamMemberRole', ['ADMIN', 'MANAGER', 'MEMBER'])

export const organisationMemberInviteStatusEnum = pgEnum('OrganisationMemberInviteStatus', [
  'ACCEPTED',
  'PENDING',
  'DECLINED',
])

export const templateTypeEnum = pgEnum('TemplateType', ['PUBLIC', 'PRIVATE', 'ORGANISATION'])

export const backgroundJobStatusEnum = pgEnum('BackgroundJobStatus', [
  'PENDING',
  'PROCESSING',
  'COMPLETED',
  'FAILED',
])

export const backgroundJobTaskStatusEnum = pgEnum('BackgroundJobTaskStatus', [
  'PENDING',
  'COMPLETED',
  'FAILED',
])

export const emailDomainStatusEnum = pgEnum('EmailDomainStatus', ['PENDING', 'ACTIVE'])

export const emailTransportTypeEnum = pgEnum('EmailTransportType', [
  'SMTP_AUTH',
  'SMTP_API',
  'RESEND',
  'MAILCHANNELS',
])

export const user = pgTable(
  'User',
  {
    id: serial('id').primaryKey(),
    name: text('name'),
    email: text('email').notNull(),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
    password: text('password'),
    source: text('source'),
    signature: text('signature'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    lastSignedIn: timestamp('lastSignedIn', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    roles: roleEnum('roles').array().default(['USER']),
    identityProvider: identityProviderEnum('identityProvider').notNull().default('DOCUMENSO'),
    avatarImageId: text('avatarImageId'),
    disabled: boolean('disabled').notNull().default(false),
    twoFactorSecret: text('twoFactorSecret'),
    twoFactorEnabled: boolean('twoFactorEnabled').notNull().default(false),
    twoFactorBackupCodes: text('twoFactorBackupCodes'),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    index('User_email_idx').on(table.email),
    foreignKey({
      name: 'User_avatarImageId_fkey',
      columns: [table.avatarImageId],
      foreignColumns: [avatarImage.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const teamProfile = pgTable(
  'TeamProfile',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    enabled: boolean('enabled').notNull().default(false),
    teamId: integer('teamId').notNull(),
    bio: text('bio'),
  },
  (table) => [
    uniqueIndex('TeamProfile_teamId_key').on(table.teamId),
    foreignKey({
      name: 'TeamProfile_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userSecurityAuditLog = pgTable(
  'UserSecurityAuditLog',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    type: userSecurityAuditLogTypeEnum('type').notNull(),
    userAgent: text('userAgent'),
    ipAddress: text('ipAddress'),
  },
  (table) => [
    foreignKey({
      name: 'UserSecurityAuditLog_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const passwordResetToken = pgTable(
  'PasswordResetToken',
  {
    id: serial('id').primaryKey(),
    token: text('token').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiry: timestamp('expiry', { precision: 3 }).notNull(),
    userId: integer('userId').notNull(),
  },
  (table) => [
    uniqueIndex('PasswordResetToken_token_key').on(table.token),
    foreignKey({
      name: 'PasswordResetToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const passkey = pgTable(
  'Passkey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: integer('userId').notNull(),
    name: text('name').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    credentialId: bytea('credentialId').notNull(),
    credentialPublicKey: bytea('credentialPublicKey').notNull(),
    counter: bigint('counter', { mode: 'bigint' }).notNull(),
    credentialDeviceType: text('credentialDeviceType').notNull(),
    credentialBackedUp: boolean('credentialBackedUp').notNull(),
    transports: text('transports').array(),
  },
  (table) => [
    foreignKey({ name: 'Passkey_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const anonymousVerificationToken = pgTable(
  'AnonymousVerificationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('AnonymousVerificationToken_id_key').on(table.id),
    uniqueIndex('AnonymousVerificationToken_token_key').on(table.token),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    id: serial('id').primaryKey(),
    secondaryId: text('secondaryId')
      .notNull()
      .$defaultFn(() => cuid()),
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    completed: boolean('completed').notNull().default(false),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    metadata: jsonb('metadata'),
    userId: integer('userId').notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_secondaryId_key').on(table.secondaryId),
    uniqueIndex('VerificationToken_token_key').on(table.token),
    foreignKey({
      name: 'VerificationToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhook = pgTable(
  'Webhook',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    webhookUrl: text('webhookUrl').notNull(),
    eventTriggers: webhookTriggerEventsEnum('eventTriggers').array(),
    secret: text('secret'),
    enabled: boolean('enabled').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    userId: integer('userId').notNull(),
    teamId: integer('teamId').notNull(),
  },
  (table) => [
    foreignKey({ name: 'Webhook_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Webhook_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhookCall = pgTable(
  'WebhookCall',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    status: webhookCallStatusEnum('status').notNull(),
    url: text('url').notNull(),
    event: webhookTriggerEventsEnum('event').notNull(),
    requestBody: jsonb('requestBody').notNull(),
    responseCode: integer('responseCode').notNull(),
    responseHeaders: jsonb('responseHeaders'),
    responseBody: jsonb('responseBody'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    webhookId: text('webhookId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'WebhookCall_webhookId_fkey',
      columns: [table.webhookId],
      foreignColumns: [webhook.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiToken = pgTable(
  'ApiToken',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    token: text('token').notNull(),
    algorithm: apiTokenAlgorithmEnum('algorithm').notNull().default('SHA512'),
    expires: timestamp('expires', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    userId: integer('userId'),
    teamId: integer('teamId').notNull(),
  },
  (table) => [
    uniqueIndex('ApiToken_token_key').on(table.token),
    foreignKey({ name: 'ApiToken_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'ApiToken_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscription = pgTable(
  'Subscription',
  {
    id: serial('id').primaryKey(),
    status: subscriptionStatusEnum('status').notNull().default('INACTIVE'),
    planId: text('planId').notNull(),
    priceId: text('priceId').notNull(),
    periodEnd: timestamp('periodEnd', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    cancelAtPeriodEnd: boolean('cancelAtPeriodEnd').notNull().default(false),
    customerId: text('customerId').notNull(),
    organisationId: text('organisationId').notNull(),
  },
  (table) => [
    uniqueIndex('Subscription_planId_key').on(table.planId),
    uniqueIndex('Subscription_organisationId_key').on(table.organisationId),
    index('Subscription_organisationId_idx').on(table.organisationId),
    foreignKey({
      name: 'Subscription_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscriptionClaim = pgTable(
  'SubscriptionClaim',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    locked: boolean('locked').notNull().default(false),
    teamCount: integer('teamCount').notNull(),
    memberCount: integer('memberCount').notNull(),
    envelopeItemCount: integer('envelopeItemCount').notNull(),
    recipientCount: integer('recipientCount').notNull(),
    envelopeContentCount: integer('envelopeContentCount').notNull(),
    envelopeContentImageCount: integer('envelopeContentImageCount').notNull(),
    flags: jsonb('flags').notNull(),
    documentRateLimits: jsonb('documentRateLimits').notNull(),
    documentQuota: integer('documentQuota'),
    emailRateLimits: jsonb('emailRateLimits').notNull(),
    emailQuota: integer('emailQuota'),
    apiRateLimits: jsonb('apiRateLimits').notNull(),
    apiQuota: integer('apiQuota'),
    emailTransportId: text('emailTransportId'),
  },
  (table) => [
    foreignKey({
      name: 'SubscriptionClaim_emailTransportId_fkey',
      columns: [table.emailTransportId],
      foreignColumns: [emailTransport.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const organisationClaim = pgTable(
  'OrganisationClaim',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    originalSubscriptionClaimId: text('originalSubscriptionClaimId'),
    teamCount: integer('teamCount').notNull(),
    memberCount: integer('memberCount').notNull(),
    envelopeItemCount: integer('envelopeItemCount').notNull(),
    recipientCount: integer('recipientCount').notNull(),
    envelopeContentCount: integer('envelopeContentCount').notNull(),
    envelopeContentImageCount: integer('envelopeContentImageCount').notNull(),
    flags: jsonb('flags').notNull(),
    documentRateLimits: jsonb('documentRateLimits').notNull(),
    documentQuota: integer('documentQuota'),
    emailRateLimits: jsonb('emailRateLimits').notNull(),
    emailQuota: integer('emailQuota'),
    apiRateLimits: jsonb('apiRateLimits').notNull(),
    apiQuota: integer('apiQuota'),
    emailTransportId: text('emailTransportId'),
  },
  (table) => [
    foreignKey({
      name: 'OrganisationClaim_emailTransportId_fkey',
      columns: [table.emailTransportId],
      foreignColumns: [emailTransport.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const organisationMonthlyStat = pgTable(
  'OrganisationMonthlyStat',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organisationId: text('organisationId').notNull(),
    period: text('period').notNull(),
    documentCount: integer('documentCount').notNull().default(0),
    emailCount: integer('emailCount').notNull().default(0),
    apiCount: integer('apiCount').notNull().default(0),
    emailReports: integer('emailReports').notNull().default(0),
  },
  (table) => [
    index('OrganisationMonthlyStat_organisationId_idx').on(table.organisationId),
    uniqueIndex('OrganisationMonthlyStat_organisationId_period_key').on(
      table.organisationId,
      table.period,
    ),
    foreignKey({
      name: 'OrganisationMonthlyStat_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: integer('userId').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    created_at: integer('created_at'),
    ext_expires_in: integer('ext_expires_in'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
    password: text('password'),
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

export const session = pgTable(
  'Session',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    sessionToken: text('sessionToken').notNull(),
    userId: integer('userId').notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    isEmbed: boolean('isEmbed').notNull().default(false),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    index('Session_sessionToken_idx').on(table.sessionToken),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const authHandoff = pgTable(
  'AuthHandoff',
  {
    id: text('id').primaryKey(),
    type: text('type').notNull(),
    payload: text('payload').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  },
  (table) => [index('AuthHandoff_expiresAt_idx').on(table.expiresAt)],
)

export const folder = pgTable(
  'Folder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    userId: integer('userId').notNull(),
    teamId: integer('teamId').notNull(),
    pinned: boolean('pinned').notNull().default(false),
    parentId: text('parentId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    visibility: documentVisibilityEnum('visibility').notNull().default('EVERYONE'),
    type: folderTypeEnum('type').notNull(),
  },
  (table) => [
    index('Folder_userId_idx').on(table.userId),
    index('Folder_teamId_idx').on(table.teamId),
    index('Folder_parentId_idx').on(table.parentId),
    index('Folder_type_idx').on(table.type),
    foreignKey({ name: 'Folder_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Folder_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Folder_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const envelope = pgTable(
  'Envelope',
  {
    id: text('id').primaryKey(),
    secondaryId: text('secondaryId').notNull(),
    externalId: text('externalId'),
    type: envelopeTypeEnum('type').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    title: text('title').notNull(),
    status: documentStatusEnum('status').notNull().default('DRAFT'),
    source: documentSourceEnum('source').notNull(),
    qrToken: text('qrToken'),
    signatureLevel: text('signatureLevel').notNull(),
    internalVersion: integer('internalVersion').notNull(),
    useLegacyFieldInsertion: boolean('useLegacyFieldInsertion').notNull().default(false),
    authOptions: jsonb('authOptions'),
    formValues: jsonb('formValues'),
    visibility: documentVisibilityEnum('visibility').notNull().default('EVERYONE'),
    templateType: templateTypeEnum('templateType').notNull().default('PRIVATE'),
    publicTitle: text('publicTitle').notNull().default(''),
    publicDescription: text('publicDescription').notNull().default(''),
    templateId: integer('templateId'),
    userId: integer('userId').notNull(),
    teamId: integer('teamId').notNull(),
    folderId: text('folderId'),
    documentMetaId: text('documentMetaId').notNull(),
  },
  (table) => [
    uniqueIndex('Envelope_secondaryId_key').on(table.secondaryId),
    uniqueIndex('Envelope_documentMetaId_key').on(table.documentMetaId),
    index('Envelope_type_idx').on(table.type),
    index('Envelope_status_idx').on(table.status),
    index('Envelope_userId_idx').on(table.userId),
    index('Envelope_teamId_idx').on(table.teamId),
    index('Envelope_folderId_idx').on(table.folderId),
    index('Envelope_createdAt_idx').on(table.createdAt),
    foreignKey({ name: 'Envelope_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Envelope_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Envelope_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Envelope_documentMetaId_fkey',
      columns: [table.documentMetaId],
      foreignColumns: [documentMeta.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const envelopeItem = pgTable(
  'EnvelopeItem',
  {
    id: text('id').primaryKey(),
    title: text('title').notNull(),
    order: integer('order').notNull(),
    documentDataId: text('documentDataId').notNull(),
    envelopeId: text('envelopeId').notNull(),
  },
  (table) => [
    index('EnvelopeItem_envelopeId_idx').on(table.envelopeId),
    uniqueIndex('EnvelopeItem_documentDataId_key').on(table.documentDataId),
    foreignKey({
      name: 'EnvelopeItem_documentDataId_fkey',
      columns: [table.documentDataId],
      foreignColumns: [documentData.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EnvelopeItem_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentAuditLog = pgTable(
  'DocumentAuditLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    envelopeId: text('envelopeId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    type: text('type').notNull(),
    data: jsonb('data').notNull(),
    name: text('name'),
    email: text('email'),
    userId: integer('userId'),
    userAgent: text('userAgent'),
    ipAddress: text('ipAddress'),
  },
  (table) => [
    index('DocumentAuditLog_envelopeId_idx').on(table.envelopeId),
    foreignKey({
      name: 'DocumentAuditLog_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const documentData = pgTable('DocumentData', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  type: documentDataTypeEnum('type').notNull(),
  data: text('data').notNull(),
  initialData: text('initialData').notNull(),
})

export const documentMeta = pgTable('DocumentMeta', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  subject: text('subject'),
  message: text('message'),
  timezone: text('timezone').default('Etc/UTC'),
  dateFormat: text('dateFormat').default('yyyy-MM-dd hh:mm a'),
  redirectUrl: text('redirectUrl'),
  signingOrder: documentSigningOrderEnum('signingOrder').notNull().default('PARALLEL'),
  allowDictateNextSigner: boolean('allowDictateNextSigner').notNull().default(false),
  typedSignatureEnabled: boolean('typedSignatureEnabled').notNull().default(true),
  uploadSignatureEnabled: boolean('uploadSignatureEnabled').notNull().default(true),
  drawSignatureEnabled: boolean('drawSignatureEnabled').notNull().default(true),
  language: text('language').notNull().default('en'),
  distributionMethod: documentDistributionMethodEnum('distributionMethod')
    .notNull()
    .default('EMAIL'),
  emailSettings: jsonb('emailSettings'),
  emailReplyTo: text('emailReplyTo'),
  emailId: text('emailId'),
  envelopeExpirationPeriod: jsonb('envelopeExpirationPeriod'),
  reminderSettings: jsonb('reminderSettings'),
})

export const envelopeAttachment = pgTable(
  'EnvelopeAttachment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: text('type').notNull(),
    label: text('label').notNull(),
    data: text('data').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    envelopeId: text('envelopeId').notNull(),
  },
  (table) => [
    index('EnvelopeAttachment_envelopeId_idx').on(table.envelopeId),
    foreignKey({
      name: 'EnvelopeAttachment_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const recipient = pgTable(
  'Recipient',
  {
    id: serial('id').primaryKey(),
    envelopeId: text('envelopeId').notNull(),
    email: varchar('email', { length: 255 }).notNull(),
    name: varchar('name', { length: 255 }).notNull().default(''),
    token: text('token').notNull(),
    documentDeletedAt: timestamp('documentDeletedAt', { precision: 3 }),
    expired: timestamp('expired', { precision: 3 }),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    expirationNotifiedAt: timestamp('expirationNotifiedAt', { precision: 3 }),
    sentAt: timestamp('sentAt', { precision: 3 }),
    signedAt: timestamp('signedAt', { precision: 3 }),
    lastReminderSentAt: timestamp('lastReminderSentAt', { precision: 3 }),
    nextReminderAt: timestamp('nextReminderAt', { precision: 3 }),
    reminderCount: integer('reminderCount').notNull().default(0),
    authOptions: jsonb('authOptions'),
    signingOrder: integer('signingOrder'),
    rejectionReason: text('rejectionReason'),
    role: recipientRoleEnum('role').notNull().default('SIGNER'),
    readStatus: readStatusEnum('readStatus').notNull().default('NOT_OPENED'),
    signingStatus: signingStatusEnum('signingStatus').notNull().default('NOT_SIGNED'),
    sendStatus: sendStatusEnum('sendStatus').notNull().default('NOT_SENT'),
  },
  (table) => [
    index('Recipient_token_idx').on(table.token),
    index('Recipient_email_idx').on(table.email),
    index('Recipient_envelopeId_idx').on(table.envelopeId),
    index('Recipient_signedAt_idx').on(table.signedAt),
    index('Recipient_expiresAt_idx').on(table.expiresAt),
    index('Recipient_email_documentDeletedAt_envelopeId_idx').on(
      table.email,
      table.documentDeletedAt,
      table.envelopeId,
    ),
    index('Recipient_email_envelopeId_idx').on(table.email, table.envelopeId),
    index('Recipient_email_signingStatus_envelopeId_role_idx').on(
      table.email,
      table.signingStatus,
      table.envelopeId,
      table.role,
    ),
    index('Recipient_nextReminderAt_idx').on(table.nextReminderAt),
    index('Recipient_email_trgm_idx').on(table.email),
    index('Recipient_name_trgm_idx').on(table.name),
    foreignKey({
      name: 'Recipient_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const field = pgTable(
  'Field',
  {
    id: serial('id').primaryKey(),
    secondaryId: text('secondaryId')
      .notNull()
      .$defaultFn(() => cuid()),
    envelopeId: text('envelopeId').notNull(),
    envelopeItemId: text('envelopeItemId').notNull(),
    recipientId: integer('recipientId').notNull(),
    type: fieldTypeEnum('type').notNull(),
    page: integer('page').notNull(),
    positionX: numeric('positionX', { precision: 65, scale: 30 }).notNull().default('0'),
    positionY: numeric('positionY', { precision: 65, scale: 30 }).notNull().default('0'),
    width: numeric('width', { precision: 65, scale: 30 }).notNull().default('-1'),
    height: numeric('height', { precision: 65, scale: 30 }).notNull().default('-1'),
    customText: text('customText').notNull(),
    inserted: boolean('inserted').notNull(),
    fieldMeta: jsonb('fieldMeta'),
  },
  (table) => [
    uniqueIndex('Field_secondaryId_key').on(table.secondaryId),
    index('Field_envelopeId_idx').on(table.envelopeId),
    index('Field_envelopeItemId_idx').on(table.envelopeItemId),
    index('Field_recipientId_idx').on(table.recipientId),
    foreignKey({
      name: 'Field_envelopeItemId_fkey',
      columns: [table.envelopeItemId],
      foreignColumns: [envelopeItem.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Field_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Field_recipientId_fkey',
      columns: [table.recipientId],
      foreignColumns: [recipient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const envelopeContent = pgTable(
  'EnvelopeContent',
  {
    id: text('id').primaryKey(),
    envelopeId: text('envelopeId').notNull(),
    envelopeItemId: text('envelopeItemId').notNull(),
    contentMeta: jsonb('contentMeta').notNull(),
    dataContentId: text('dataContentId'),
  },
  (table) => [
    index('EnvelopeContent_envelopeId_idx').on(table.envelopeId),
    index('EnvelopeContent_envelopeItemId_idx').on(table.envelopeItemId),
    foreignKey({
      name: 'EnvelopeContent_envelopeItemId_fkey',
      columns: [table.envelopeItemId],
      foreignColumns: [envelopeItem.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EnvelopeContent_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EnvelopeContent_dataContentId_fkey',
      columns: [table.dataContentId],
      foreignColumns: [dataContent.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dataContent = pgTable('DataContent', {
  id: text('id').primaryKey(),
  type: documentDataTypeEnum('type').notNull(),
  data: text('data').notNull(),
  metadata: jsonb('metadata').notNull(),
})

export const signature = pgTable(
  'Signature',
  {
    id: serial('id').primaryKey(),
    created: timestamp('created', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    recipientId: integer('recipientId').notNull(),
    fieldId: integer('fieldId').notNull(),
    signatureImageAsBase64: text('signatureImageAsBase64'),
    typedSignature: text('typedSignature'),
  },
  (table) => [
    uniqueIndex('Signature_fieldId_key').on(table.fieldId),
    index('Signature_recipientId_idx').on(table.recipientId),
    foreignKey({
      name: 'Signature_recipientId_fkey',
      columns: [table.recipientId],
      foreignColumns: [recipient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Signature_fieldId_fkey',
      columns: [table.fieldId],
      foreignColumns: [field.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cscCredential = pgTable(
  'CscCredential',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    providerId: text('providerId').notNull(),
    credentialId: text('credentialId').notNull(),
    certCache: bytea('certCache'),
    signatureAlgorithm: text('signatureAlgorithm').notNull(),
    keyType: text('keyType').notNull(),
    digestAlgorithm: text('digestAlgorithm').notNull(),
    keyLenBits: integer('keyLenBits'),
    signAlgoParams: text('signAlgoParams'),
    serviceTokenCiphertext: bytea('serviceTokenCiphertext'),
    serviceTokenExpiresAt: timestamp('serviceTokenExpiresAt', { precision: 3 }),
    recipientId: integer('recipientId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('CscCredential_recipientId_key').on(table.recipientId),
    foreignKey({
      name: 'CscCredential_recipientId_fkey',
      columns: [table.recipientId],
      foreignColumns: [recipient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cscSession = pgTable(
  'CscSession',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    envelopeId: text('envelopeId').notNull(),
    signingTime: timestamp('signingTime', { precision: 3 }).notNull(),
    itemsJson: jsonb('itemsJson').notNull(),
    encryptedSad: bytea('encryptedSad'),
    sadExpiresAt: timestamp('sadExpiresAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    recipientId: integer('recipientId').notNull(),
  },
  (table) => [
    uniqueIndex('CscSession_recipientId_key').on(table.recipientId),
    foreignKey({
      name: 'CscSession_recipientId_fkey',
      columns: [table.recipientId],
      foreignColumns: [recipient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentShareLink = pgTable(
  'DocumentShareLink',
  {
    id: serial('id').primaryKey(),
    email: text('email').notNull(),
    slug: text('slug').notNull(),
    envelopeId: text('envelopeId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('DocumentShareLink_slug_key').on(table.slug),
    uniqueIndex('DocumentShareLink_envelopeId_email_key').on(table.envelopeId, table.email),
    foreignKey({
      name: 'DocumentShareLink_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisation = pgTable(
  'Organisation',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: organisationTypeEnum('type').notNull(),
    name: text('name').notNull(),
    url: text('url').notNull(),
    avatarImageId: text('avatarImageId'),
    customerId: text('customerId'),
    organisationClaimId: text('organisationClaimId').notNull(),
    ownerUserId: integer('ownerUserId').notNull(),
    organisationGlobalSettingsId: text('organisationGlobalSettingsId').notNull(),
    organisationAuthenticationPortalId: text('organisationAuthenticationPortalId').notNull(),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('Organisation_url_key').on(table.url),
    uniqueIndex('Organisation_customerId_key').on(table.customerId),
    uniqueIndex('Organisation_organisationClaimId_key').on(table.organisationClaimId),
    uniqueIndex('Organisation_organisationGlobalSettingsId_key').on(
      table.organisationGlobalSettingsId,
    ),
    uniqueIndex('Organisation_organisationAuthenticationPortalId_key').on(
      table.organisationAuthenticationPortalId,
    ),
    index('Organisation_name_idx').on(table.name),
    index('Organisation_ownerUserId_idx').on(table.ownerUserId),
    foreignKey({
      name: 'Organisation_organisationClaimId_fkey',
      columns: [table.organisationClaimId],
      foreignColumns: [organisationClaim.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Organisation_avatarImageId_fkey',
      columns: [table.avatarImageId],
      foreignColumns: [avatarImage.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Organisation_ownerUserId_fkey',
      columns: [table.ownerUserId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Organisation_organisationGlobalSettingsId_fkey',
      columns: [table.organisationGlobalSettingsId],
      foreignColumns: [organisationGlobalSettings.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Organisation_organisationAuthenticationPortalId_fkey',
      columns: [table.organisationAuthenticationPortalId],
      foreignColumns: [organisationAuthenticationPortal.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const organisationMember = pgTable(
  'OrganisationMember',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: integer('userId').notNull(),
    organisationId: text('organisationId').notNull(),
  },
  (table) => [
    index('OrganisationMember_organisationId_idx').on(table.organisationId),
    uniqueIndex('OrganisationMember_userId_organisationId_key').on(
      table.userId,
      table.organisationId,
    ),
    foreignKey({
      name: 'OrganisationMember_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganisationMember_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisationMemberInvite = pgTable(
  'OrganisationMemberInvite',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    email: text('email').notNull(),
    token: text('token').notNull(),
    status: organisationMemberInviteStatusEnum('status').notNull().default('PENDING'),
    organisationId: text('organisationId').notNull(),
    organisationRole: organisationMemberRoleEnum('organisationRole').notNull(),
  },
  (table) => [
    uniqueIndex('OrganisationMemberInvite_token_key').on(table.token),
    foreignKey({
      name: 'OrganisationMemberInvite_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisationGroup = pgTable(
  'OrganisationGroup',
  {
    id: text('id').primaryKey(),
    name: text('name'),
    type: organisationGroupTypeEnum('type').notNull(),
    organisationRole: organisationMemberRoleEnum('organisationRole').notNull(),
    organisationId: text('organisationId').notNull(),
  },
  (table) => [
    index('OrganisationGroup_organisationId_idx').on(table.organisationId),
    foreignKey({
      name: 'OrganisationGroup_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisationGroupMember = pgTable(
  'OrganisationGroupMember',
  {
    id: text('id').primaryKey(),
    groupId: text('groupId').notNull(),
    organisationMemberId: text('organisationMemberId').notNull(),
  },
  (table) => [
    index('OrganisationGroupMember_groupId_idx').on(table.groupId),
    index('OrganisationGroupMember_organisationMemberId_idx').on(table.organisationMemberId),
    uniqueIndex('OrganisationGroupMember_organisationMemberId_groupId_key').on(
      table.organisationMemberId,
      table.groupId,
    ),
    foreignKey({
      name: 'OrganisationGroupMember_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [organisationGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganisationGroupMember_organisationMemberId_fkey',
      columns: [table.organisationMemberId],
      foreignColumns: [organisationMember.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamGroup = pgTable(
  'TeamGroup',
  {
    id: text('id').primaryKey(),
    organisationGroupId: text('organisationGroupId').notNull(),
    teamRole: teamMemberRoleEnum('teamRole').notNull(),
    teamId: integer('teamId').notNull(),
  },
  (table) => [
    index('TeamGroup_teamId_idx').on(table.teamId),
    index('TeamGroup_organisationGroupId_idx').on(table.organisationGroupId),
    uniqueIndex('TeamGroup_teamId_organisationGroupId_key').on(
      table.teamId,
      table.organisationGroupId,
    ),
    foreignKey({
      name: 'TeamGroup_organisationGroupId_fkey',
      columns: [table.organisationGroupId],
      foreignColumns: [organisationGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TeamGroup_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisationGlobalSettings = pgTable(
  'OrganisationGlobalSettings',
  {
    id: text('id').primaryKey(),
    documentVisibility: documentVisibilityEnum('documentVisibility').notNull().default('EVERYONE'),
    documentLanguage: text('documentLanguage').notNull().default('en'),
    includeSenderDetails: boolean('includeSenderDetails').notNull().default(true),
    includeSigningCertificate: boolean('includeSigningCertificate').notNull().default(true),
    includeAuditLog: boolean('includeAuditLog').notNull().default(false),
    documentTimezone: text('documentTimezone'),
    documentDateFormat: text('documentDateFormat').notNull().default('yyyy-MM-dd hh:mm a'),
    delegateDocumentOwnership: boolean('delegateDocumentOwnership').notNull().default(false),
    typedSignatureEnabled: boolean('typedSignatureEnabled').notNull().default(true),
    uploadSignatureEnabled: boolean('uploadSignatureEnabled').notNull().default(true),
    drawSignatureEnabled: boolean('drawSignatureEnabled').notNull().default(true),
    defaultRecipients: jsonb('defaultRecipients'),
    emailId: text('emailId'),
    emailReplyTo: text('emailReplyTo'),
    emailDocumentSettings: jsonb('emailDocumentSettings').notNull(),
    brandingEnabled: boolean('brandingEnabled').notNull().default(false),
    brandingLogo: text('brandingLogo').notNull().default(''),
    brandingUrl: text('brandingUrl').notNull().default(''),
    brandingCompanyDetails: text('brandingCompanyDetails').notNull().default(''),
    brandingColors: jsonb('brandingColors'),
    brandingCss: text('brandingCss').notNull().default(''),
    envelopeExpirationPeriod: jsonb('envelopeExpirationPeriod'),
    reminderSettings: jsonb('reminderSettings'),
    aiFeaturesEnabled: boolean('aiFeaturesEnabled').notNull().default(false),
  },
  (table): PgTableExtraConfigValue[] => [
    foreignKey({
      name: 'OrganisationGlobalSettings_emailId_fkey',
      columns: [table.emailId],
      foreignColumns: [organisationEmail.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const teamGlobalSettings = pgTable(
  'TeamGlobalSettings',
  {
    id: text('id').primaryKey(),
    documentVisibility: documentVisibilityEnum('documentVisibility'),
    documentLanguage: text('documentLanguage'),
    documentTimezone: text('documentTimezone'),
    documentDateFormat: text('documentDateFormat'),
    delegateDocumentOwnership: boolean('delegateDocumentOwnership'),
    includeSenderDetails: boolean('includeSenderDetails'),
    includeSigningCertificate: boolean('includeSigningCertificate'),
    includeAuditLog: boolean('includeAuditLog'),
    typedSignatureEnabled: boolean('typedSignatureEnabled'),
    uploadSignatureEnabled: boolean('uploadSignatureEnabled'),
    drawSignatureEnabled: boolean('drawSignatureEnabled'),
    defaultRecipients: jsonb('defaultRecipients'),
    emailId: text('emailId'),
    emailReplyTo: text('emailReplyTo'),
    emailDocumentSettings: jsonb('emailDocumentSettings'),
    brandingEnabled: boolean('brandingEnabled'),
    brandingLogo: text('brandingLogo'),
    brandingUrl: text('brandingUrl'),
    brandingCompanyDetails: text('brandingCompanyDetails'),
    brandingColors: jsonb('brandingColors'),
    brandingCss: text('brandingCss'),
    envelopeExpirationPeriod: jsonb('envelopeExpirationPeriod'),
    reminderSettings: jsonb('reminderSettings'),
    aiFeaturesEnabled: boolean('aiFeaturesEnabled'),
  },
  (table) => [
    foreignKey({
      name: 'TeamGlobalSettings_emailId_fkey',
      columns: [table.emailId],
      foreignColumns: [organisationEmail.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const team = pgTable(
  'Team',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    url: text('url').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    avatarImageId: text('avatarImageId'),
    organisationId: text('organisationId').notNull(),
    teamGlobalSettingsId: text('teamGlobalSettingsId').notNull(),
  },
  (table) => [
    uniqueIndex('Team_url_key').on(table.url),
    uniqueIndex('Team_teamGlobalSettingsId_key').on(table.teamGlobalSettingsId),
    index('Team_name_idx').on(table.name),
    index('Team_organisationId_idx').on(table.organisationId),
    foreignKey({
      name: 'Team_avatarImageId_fkey',
      columns: [table.avatarImageId],
      foreignColumns: [avatarImage.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Team_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Team_teamGlobalSettingsId_fkey',
      columns: [table.teamGlobalSettingsId],
      foreignColumns: [teamGlobalSettings.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamEmail = pgTable(
  'TeamEmail',
  {
    teamId: integer('teamId').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    name: text('name').notNull(),
    email: text('email').notNull(),
  },
  (table) => [
    uniqueIndex('TeamEmail_teamId_key').on(table.teamId),
    uniqueIndex('TeamEmail_email_key').on(table.email),
    foreignKey({
      name: 'TeamEmail_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamEmailVerification = pgTable(
  'TeamEmailVerification',
  {
    teamId: integer('teamId').primaryKey(),
    name: text('name').notNull(),
    email: text('email').notNull(),
    token: text('token').notNull(),
    completed: boolean('completed').notNull().default(false),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('TeamEmailVerification_teamId_key').on(table.teamId),
    uniqueIndex('TeamEmailVerification_token_key').on(table.token),
    foreignKey({
      name: 'TeamEmailVerification_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const templateDirectLink = pgTable(
  'TemplateDirectLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    envelopeId: text('envelopeId').notNull(),
    token: text('token').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    enabled: boolean('enabled').notNull(),
    directTemplateRecipientId: integer('directTemplateRecipientId').notNull(),
  },
  (table) => [
    uniqueIndex('TemplateDirectLink_id_key').on(table.id),
    uniqueIndex('TemplateDirectLink_envelopeId_key').on(table.envelopeId),
    uniqueIndex('TemplateDirectLink_token_key').on(table.token),
    foreignKey({
      name: 'TemplateDirectLink_envelopeId_fkey',
      columns: [table.envelopeId],
      foreignColumns: [envelope.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const siteSettings = pgTable(
  'SiteSettings',
  {
    id: text('id').primaryKey(),
    enabled: boolean('enabled').notNull().default(false),
    data: jsonb('data').notNull(),
    lastModifiedByUserId: integer('lastModifiedByUserId'),
    lastModifiedAt: timestamp('lastModifiedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'SiteSettings_lastModifiedByUserId_fkey',
      columns: [table.lastModifiedByUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const backgroundJob = pgTable('BackgroundJob', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  status: backgroundJobStatusEnum('status').notNull().default('PENDING'),
  payload: jsonb('payload'),
  retried: integer('retried').notNull().default(0),
  maxRetries: integer('maxRetries').notNull().default(3),
  jobId: text('jobId').notNull(),
  name: text('name').notNull(),
  version: text('version').notNull(),
  submittedAt: timestamp('submittedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  completedAt: timestamp('completedAt', { precision: 3 }),
  lastRetriedAt: timestamp('lastRetriedAt', { precision: 3 }),
})

export const backgroundJobTask = pgTable(
  'BackgroundJobTask',
  {
    id: text('id').primaryKey(),
    name: text('name').notNull(),
    status: backgroundJobTaskStatusEnum('status').notNull().default('PENDING'),
    result: jsonb('result'),
    retried: integer('retried').notNull().default(0),
    maxRetries: integer('maxRetries').notNull().default(3),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    jobId: text('jobId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'BackgroundJobTask_jobId_fkey',
      columns: [table.jobId],
      foreignColumns: [backgroundJob.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const avatarImage = pgTable('AvatarImage', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  bytes: text('bytes').notNull(),
})

export const emailDomain = pgTable(
  'EmailDomain',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    status: emailDomainStatusEnum('status').notNull().default('PENDING'),
    selector: text('selector').notNull(),
    domain: text('domain').notNull(),
    publicKey: text('publicKey').notNull(),
    privateKey: text('privateKey').notNull(),
    lastVerifiedAt: timestamp('lastVerifiedAt', { precision: 3 }),
    organisationId: text('organisationId').notNull(),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('EmailDomain_selector_key').on(table.selector),
    uniqueIndex('EmailDomain_domain_key').on(table.domain),
    foreignKey({
      name: 'EmailDomain_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organisationEmail = pgTable(
  'OrganisationEmail',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    email: text('email').notNull(),
    emailName: text('emailName').notNull(),
    emailDomainId: text('emailDomainId').notNull(),
    organisationId: text('organisationId').notNull(),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('OrganisationEmail_email_key').on(table.email),
    foreignKey({
      name: 'OrganisationEmail_emailDomainId_fkey',
      columns: [table.emailDomainId],
      foreignColumns: [emailDomain.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganisationEmail_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailTransport = pgTable('EmailTransport', {
  id: text('id').primaryKey(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  name: text('name').notNull(),
  type: emailTransportTypeEnum('type').notNull(),
  fromName: text('fromName').notNull(),
  fromAddress: text('fromAddress').notNull(),
  config: text('config').notNull(),
})

export const organisationAuthenticationPortal = pgTable('OrganisationAuthenticationPortal', {
  id: text('id').primaryKey(),
  enabled: boolean('enabled').notNull().default(false),
  clientId: text('clientId').notNull().default(''),
  clientSecret: text('clientSecret').notNull().default(''),
  wellKnownUrl: text('wellKnownUrl').notNull().default(''),
  defaultOrganisationRole: organisationMemberRoleEnum('defaultOrganisationRole')
    .notNull()
    .default('MEMBER'),
  autoProvisionUsers: boolean('autoProvisionUsers').notNull().default(true),
  allowedDomains: text('allowedDomains').array().default([]),
  allowPersonalOrganisations: boolean('allowPersonalOrganisations').notNull().default(false),
})

export const counter = pgTable('Counter', {
  id: text('id').primaryKey(),
  value: integer('value').notNull(),
})

export const embedSignature = pgTable(
  'EmbedSignature',
  {
    key: text('key').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organisationId: text('organisationId').notNull(),
    recipientId: integer('recipientId').notNull(),
    signature: text('signature').notNull(),
  },
  (table) => [
    index('EmbedSignature_organisationId_idx').on(table.organisationId),
    index('EmbedSignature_recipientId_idx').on(table.recipientId),
    foreignKey({
      name: 'EmbedSignature_organisationId_fkey',
      columns: [table.organisationId],
      foreignColumns: [organisation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const rateLimit = pgTable(
  'RateLimit',
  {
    key: text('key').notNull(),
    action: text('action').notNull(),
    bucket: timestamp('bucket', { precision: 3 }).notNull(),
    count: integer('count').notNull().default(1),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'RateLimit_pkey', columns: [table.key, table.action, table.bucket] }),
    index('RateLimit_createdAt_idx').on(table.createdAt),
  ],
)

export const relations = defineRelations(
  {
    user,
    teamProfile,
    userSecurityAuditLog,
    passwordResetToken,
    passkey,
    anonymousVerificationToken,
    verificationToken,
    webhook,
    webhookCall,
    apiToken,
    subscription,
    subscriptionClaim,
    organisationClaim,
    organisationMonthlyStat,
    account,
    session,
    authHandoff,
    folder,
    envelope,
    envelopeItem,
    documentAuditLog,
    documentData,
    documentMeta,
    envelopeAttachment,
    recipient,
    field,
    envelopeContent,
    dataContent,
    signature,
    cscCredential,
    cscSession,
    documentShareLink,
    organisation,
    organisationMember,
    organisationMemberInvite,
    organisationGroup,
    organisationGroupMember,
    teamGroup,
    organisationGlobalSettings,
    teamGlobalSettings,
    team,
    teamEmail,
    teamEmailVerification,
    templateDirectLink,
    siteSettings,
    backgroundJob,
    backgroundJobTask,
    avatarImage,
    emailDomain,
    organisationEmail,
    emailTransport,
    organisationAuthenticationPortal,
    counter,
    embedSignature,
    rateLimit,
  },
  (r) => ({
    user: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      passwordResetTokens: r.many.passwordResetToken(),
      ownedOrganisations: r.many.organisation(),
      organisationMember: r.many.organisationMember(),
      folders: r.many.folder(),
      envelopes: r.many.envelope(),
      verificationTokens: r.many.verificationToken(),
      apiTokens: r.many.apiToken(),
      securityAuditLogs: r.many.userSecurityAuditLog(),
      webhooks: r.many.webhook(),
      siteSettings: r.many.siteSettings(),
      passkeys: r.many.passkey(),
      avatarImage: r.one.avatarImage({ from: r.user.avatarImageId, to: r.avatarImage.id }),
    },
    teamProfile: { team: r.one.team({ from: r.teamProfile.teamId, to: r.team.id }) },
    userSecurityAuditLog: {
      user: r.one.user({ from: r.userSecurityAuditLog.userId, to: r.user.id, optional: false }),
    },
    passwordResetToken: {
      user: r.one.user({ from: r.passwordResetToken.userId, to: r.user.id, optional: false }),
    },
    passkey: { user: r.one.user({ from: r.passkey.userId, to: r.user.id, optional: false }) },
    verificationToken: {
      user: r.one.user({ from: r.verificationToken.userId, to: r.user.id, optional: false }),
    },
    webhook: {
      user: r.one.user({ from: r.webhook.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.webhook.teamId, to: r.team.id, optional: false }),
      webhookCalls: r.many.webhookCall(),
    },
    webhookCall: {
      webhook: r.one.webhook({ from: r.webhookCall.webhookId, to: r.webhook.id, optional: false }),
    },
    apiToken: {
      user: r.one.user({ from: r.apiToken.userId, to: r.user.id }),
      team: r.one.team({ from: r.apiToken.teamId, to: r.team.id, optional: false }),
    },
    subscription: {
      organisation: r.one.organisation({
        from: r.subscription.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
    },
    subscriptionClaim: {
      emailTransport: r.one.emailTransport({
        from: r.subscriptionClaim.emailTransportId,
        to: r.emailTransport.id,
      }),
    },
    organisationClaim: {
      organisation: r.one.organisation({
        from: r.organisationClaim.id,
        to: r.organisation.organisationClaimId,
      }),
      emailTransport: r.one.emailTransport({
        from: r.organisationClaim.emailTransportId,
        to: r.emailTransport.id,
      }),
    },
    organisationMonthlyStat: {
      organisation: r.one.organisation({
        from: r.organisationMonthlyStat.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id }) },
    folder: {
      user: r.one.user({ from: r.folder.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.folder.teamId, to: r.team.id, optional: false }),
      parent: r.one.folder({ from: r.folder.parentId, to: r.folder.id, alias: 'FolderToFolder' }),
      envelopes: r.many.envelope(),
      subfolders: r.many.folder({ alias: 'FolderToFolder' }),
    },
    envelope: {
      envelopeItems: r.many.envelopeItem(),
      recipients: r.many.recipient(),
      fields: r.many.field(),
      contents: r.many.envelopeContent(),
      shareLinks: r.many.documentShareLink(),
      auditLogs: r.many.documentAuditLog(),
      directLink: r.one.templateDirectLink({
        from: r.envelope.id,
        to: r.templateDirectLink.envelopeId,
      }),
      user: r.one.user({ from: r.envelope.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.envelope.teamId, to: r.team.id, optional: false }),
      folder: r.one.folder({ from: r.envelope.folderId, to: r.folder.id }),
      documentMeta: r.one.documentMeta({
        from: r.envelope.documentMetaId,
        to: r.documentMeta.id,
        optional: false,
      }),
      envelopeAttachments: r.many.envelopeAttachment(),
    },
    envelopeItem: {
      documentData: r.one.documentData({
        from: r.envelopeItem.documentDataId,
        to: r.documentData.id,
        optional: false,
      }),
      envelope: r.one.envelope({
        from: r.envelopeItem.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
      field: r.many.field(),
      contents: r.many.envelopeContent(),
    },
    documentAuditLog: {
      envelope: r.one.envelope({ from: r.documentAuditLog.envelopeId, to: r.envelope.id }),
    },
    documentData: {
      envelopeItem: r.one.envelopeItem({
        from: r.documentData.id,
        to: r.envelopeItem.documentDataId,
      }),
    },
    documentMeta: {
      envelope: r.one.envelope({ from: r.documentMeta.id, to: r.envelope.documentMetaId }),
    },
    envelopeAttachment: {
      envelope: r.one.envelope({
        from: r.envelopeAttachment.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
    },
    recipient: {
      envelope: r.one.envelope({
        from: r.recipient.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
      fields: r.many.field(),
      signatures: r.many.signature(),
      cscCredential: r.one.cscCredential({ from: r.recipient.id, to: r.cscCredential.recipientId }),
      cscSession: r.one.cscSession({ from: r.recipient.id, to: r.cscSession.recipientId }),
    },
    field: {
      envelopeItem: r.one.envelopeItem({
        from: r.field.envelopeItemId,
        to: r.envelopeItem.id,
        optional: false,
      }),
      envelope: r.one.envelope({ from: r.field.envelopeId, to: r.envelope.id, optional: false }),
      recipient: r.one.recipient({
        from: r.field.recipientId,
        to: r.recipient.id,
        optional: false,
      }),
      signature: r.one.signature({ from: r.field.id, to: r.signature.fieldId }),
    },
    envelopeContent: {
      envelopeItem: r.one.envelopeItem({
        from: r.envelopeContent.envelopeItemId,
        to: r.envelopeItem.id,
        optional: false,
      }),
      envelope: r.one.envelope({
        from: r.envelopeContent.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
      dataContent: r.one.dataContent({
        from: r.envelopeContent.dataContentId,
        to: r.dataContent.id,
      }),
    },
    dataContent: { envelopeContents: r.many.envelopeContent() },
    signature: {
      recipient: r.one.recipient({
        from: r.signature.recipientId,
        to: r.recipient.id,
        optional: false,
      }),
      field: r.one.field({ from: r.signature.fieldId, to: r.field.id, optional: false }),
    },
    cscCredential: {
      recipient: r.one.recipient({
        from: r.cscCredential.recipientId,
        to: r.recipient.id,
        optional: false,
      }),
    },
    cscSession: {
      recipient: r.one.recipient({
        from: r.cscSession.recipientId,
        to: r.recipient.id,
        optional: false,
      }),
    },
    documentShareLink: {
      envelope: r.one.envelope({
        from: r.documentShareLink.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
    },
    organisation: {
      subscription: r.one.subscription({
        from: r.organisation.id,
        to: r.subscription.organisationId,
      }),
      organisationClaim: r.one.organisationClaim({
        from: r.organisation.organisationClaimId,
        to: r.organisationClaim.id,
        optional: false,
      }),
      members: r.many.organisationMember(),
      invites: r.many.organisationMemberInvite(),
      groups: r.many.organisationGroup(),
      teams: r.many.team(),
      emailDomains: r.many.emailDomain(),
      organisationEmails: r.many.organisationEmail(),
      monthlyStats: r.many.organisationMonthlyStat(),
      embedSignatures: r.many.embedSignature(),
      avatarImage: r.one.avatarImage({ from: r.organisation.avatarImageId, to: r.avatarImage.id }),
      owner: r.one.user({ from: r.organisation.ownerUserId, to: r.user.id, optional: false }),
      organisationGlobalSettings: r.one.organisationGlobalSettings({
        from: r.organisation.organisationGlobalSettingsId,
        to: r.organisationGlobalSettings.id,
        optional: false,
      }),
      organisationAuthenticationPortal: r.one.organisationAuthenticationPortal({
        from: r.organisation.organisationAuthenticationPortalId,
        to: r.organisationAuthenticationPortal.id,
        optional: false,
      }),
    },
    organisationMember: {
      user: r.one.user({ from: r.organisationMember.userId, to: r.user.id, optional: false }),
      organisation: r.one.organisation({
        from: r.organisationMember.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
      organisationGroupMembers: r.many.organisationGroupMember(),
    },
    organisationMemberInvite: {
      organisation: r.one.organisation({
        from: r.organisationMemberInvite.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
    },
    organisationGroup: {
      organisation: r.one.organisation({
        from: r.organisationGroup.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
      organisationGroupMembers: r.many.organisationGroupMember(),
      teamGroups: r.many.teamGroup(),
    },
    organisationGroupMember: {
      group: r.one.organisationGroup({
        from: r.organisationGroupMember.groupId,
        to: r.organisationGroup.id,
        optional: false,
      }),
      organisationMember: r.one.organisationMember({
        from: r.organisationGroupMember.organisationMemberId,
        to: r.organisationMember.id,
        optional: false,
      }),
    },
    teamGroup: {
      organisationGroup: r.one.organisationGroup({
        from: r.teamGroup.organisationGroupId,
        to: r.organisationGroup.id,
        optional: false,
      }),
      team: r.one.team({ from: r.teamGroup.teamId, to: r.team.id, optional: false }),
    },
    organisationGlobalSettings: {
      organisation: r.one.organisation({
        from: r.organisationGlobalSettings.id,
        to: r.organisation.organisationGlobalSettingsId,
      }),
      email: r.one.organisationEmail({
        from: r.organisationGlobalSettings.emailId,
        to: r.organisationEmail.id,
      }),
    },
    teamGlobalSettings: {
      team: r.one.team({ from: r.teamGlobalSettings.id, to: r.team.teamGlobalSettingsId }),
      email: r.one.organisationEmail({
        from: r.teamGlobalSettings.emailId,
        to: r.organisationEmail.id,
      }),
    },
    team: {
      teamEmail: r.one.teamEmail({ from: r.team.id, to: r.teamEmail.teamId }),
      emailVerification: r.one.teamEmailVerification({
        from: r.team.id,
        to: r.teamEmailVerification.teamId,
      }),
      avatarImage: r.one.avatarImage({ from: r.team.avatarImageId, to: r.avatarImage.id }),
      profile: r.one.teamProfile({ from: r.team.id, to: r.teamProfile.teamId }),
      envelopes: r.many.envelope(),
      folders: r.many.folder(),
      apiTokens: r.many.apiToken(),
      webhooks: r.many.webhook(),
      teamGroups: r.many.teamGroup(),
      organisation: r.one.organisation({
        from: r.team.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
      teamGlobalSettings: r.one.teamGlobalSettings({
        from: r.team.teamGlobalSettingsId,
        to: r.teamGlobalSettings.id,
        optional: false,
      }),
    },
    teamEmail: { team: r.one.team({ from: r.teamEmail.teamId, to: r.team.id, optional: false }) },
    teamEmailVerification: {
      team: r.one.team({ from: r.teamEmailVerification.teamId, to: r.team.id, optional: false }),
    },
    templateDirectLink: {
      envelope: r.one.envelope({
        from: r.templateDirectLink.envelopeId,
        to: r.envelope.id,
        optional: false,
      }),
    },
    siteSettings: {
      lastModifiedByUser: r.one.user({ from: r.siteSettings.lastModifiedByUserId, to: r.user.id }),
    },
    backgroundJob: { tasks: r.many.backgroundJobTask() },
    backgroundJobTask: {
      backgroundJob: r.one.backgroundJob({
        from: r.backgroundJobTask.jobId,
        to: r.backgroundJob.id,
        optional: false,
      }),
    },
    avatarImage: { team: r.many.team(), user: r.many.user(), organisation: r.many.organisation() },
    emailDomain: {
      organisation: r.one.organisation({
        from: r.emailDomain.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
      emails: r.many.organisationEmail(),
    },
    organisationEmail: {
      emailDomain: r.one.emailDomain({
        from: r.organisationEmail.emailDomainId,
        to: r.emailDomain.id,
        optional: false,
      }),
      organisation: r.one.organisation({
        from: r.organisationEmail.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
      organisationGlobalSettings: r.many.organisationGlobalSettings(),
      teamGlobalSettings: r.many.teamGlobalSettings(),
    },
    emailTransport: {
      subscriptionClaims: r.many.subscriptionClaim(),
      organisationClaims: r.many.organisationClaim(),
    },
    organisationAuthenticationPortal: {
      organisation: r.one.organisation({
        from: r.organisationAuthenticationPortal.id,
        to: r.organisation.organisationAuthenticationPortalId,
      }),
    },
    embedSignature: {
      organisation: r.one.organisation({
        from: r.embedSignature.organisationId,
        to: r.organisation.id,
        optional: false,
      }),
    },
  }),
)
