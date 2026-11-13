import {
  type PgTableExtraConfigValue,
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
  unique,
  uniqueIndex,
  uuid,
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

export const apiUsageResourceEnum = pgEnum('api_usage_resource', ['poll'])

export const subscriptionStatusEnum = pgEnum('subscription_status', [
  'incomplete',
  'incomplete_expired',
  'active',
  'paused',
  'trialing',
  'past_due',
  'canceled',
  'unpaid',
])

export const subscriptionIntervalEnum = pgEnum('subscription_interval', ['month', 'year'])

export const nonprofitApplicationStatusEnum = pgEnum('nonprofit_application_status', [
  'approved',
  'rejected',
  'failed',
])

export const queuedEmailStatusEnum = pgEnum('queued_email_status', [
  'pending',
  'sent',
  'skipped',
  'failed',
])

export const scheduledEventStatusEnum = pgEnum('scheduled_event_status', [
  'confirmed',
  'canceled',
  'unconfirmed',
])

export const scheduledEventInviteStatusEnum = pgEnum('scheduled_event_invite_status', [
  'pending',
  'accepted',
  'declined',
  'tentative',
])

export const credentialTypeEnum = pgEnum('CredentialType', ['OAUTH'])

export const licenseTypeEnum = pgEnum('LicenseType', ['PLUS', 'ORGANIZATION', 'ENTERPRISE'])

export const licenseStatusEnum = pgEnum('LicenseStatus', ['ACTIVE', 'REVOKED'])

export const pollStatusEnum = pgEnum('poll_status', ['open', 'closed', 'scheduled', 'canceled'])

export const pollKindEnum = pgEnum('poll_kind', ['date', 'time'])

export const pollClosedReasonEnum = pgEnum('poll_closed_reason', ['auto', 'manual'])

export const voteTypeEnum = pgEnum('vote_type', ['yes', 'no', 'ifNeedBe'])

export const spaceMemberRoleEnum = pgEnum('SpaceMemberRole', ['ADMIN', 'MEMBER'])

export const spaceTypeEnum = pgEnum('space_types', ['personal', 'work'])

export const spaceTierEnum = pgEnum('space_tiers', ['hobby', 'pro'])

export const webhookDeliveryStatusEnum = pgEnum('webhook_delivery_status', [
  'pending',
  'in_flight',
  'succeeded',
  'failed',
  'exhausted',
])

export const timeFormatEnum = pgEnum('time_format', ['hours12', 'hours24'])

export const userRoleEnum = pgEnum('user_role', ['admin', 'user'])

export const apiUsage = pgTable(
  'api_usage',
  {
    spaceId: text('space_id').notNull(),
    resource: apiUsageResourceEnum('resource').notNull(),
    period: utcDate('period').notNull(),
    count: integer('count').notNull().default(0),
    nearLimitWarnedAt: timestamp('near_limit_warned_at', { precision: 3 }),
    atLimitWarnedAt: timestamp('at_limit_warned_at', { precision: 3 }),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'api_usage_pkey', columns: [table.spaceId, table.resource, table.period] }),
    foreignKey({
      name: 'api_usage_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscriptions = pgTable(
  'subscriptions',
  {
    id: text('id').primaryKey(),
    priceId: text('price_id').notNull(),
    quantity: integer('quantity').notNull().default(1),
    subscriptionItemId: text('subscription_item_id').notNull(),
    amount: integer('amount').notNull(),
    discountPercentOff: doublePrecision('discount_percent_off'),
    discountAmountOff: integer('discount_amount_off'),
    status: subscriptionStatusEnum('status').notNull(),
    active: boolean('active').notNull(),
    currency: text('currency').notNull(),
    interval: subscriptionIntervalEnum('interval').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    periodStart: timestamp('period_start', { precision: 3 }).notNull(),
    periodEnd: timestamp('period_end', { precision: 3 }).notNull(),
    cancelAtPeriodEnd: boolean('cancel_at_period_end').notNull().default(false),
    userId: text('user_id').notNull(),
    spaceId: text('space_id').notNull(),
    apiAddonItemId: text('api_addon_item_id'),
    apiAddonPriceId: text('api_addon_price_id'),
    apiAddonAmount: integer('api_addon_amount'),
    apiPollLimit: integer('api_poll_limit'),
    apiPollRetainedLimit: integer('api_poll_retained_limit'),
    apiPollRetainedUntil: timestamp('api_poll_retained_until', { precision: 3 }),
  },
  (table) => [
    index('subscriptions_user_id_idx').on(table.userId),
    index('subscriptions_space_id_idx').on(table.spaceId),
    foreignKey({
      name: 'subscriptions_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'subscriptions_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const paymentMethods = pgTable(
  'payment_methods',
  {
    id: text('id').primaryKey(),
    userId: text('user_id').notNull(),
    type: text('type').notNull(),
    data: jsonb('data').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'payment_methods_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const nonprofitApplications = pgTable(
  'nonprofit_applications',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    spaceId: text('space_id').notNull(),
    userId: text('user_id').notNull(),
    organizationName: text('organization_name').notNull(),
    website: text('website').notNull(),
    emailDomain: text('email_domain').notNull(),
    status: nonprofitApplicationStatusEnum('status').notNull(),
    reason: text('reason'),
    model: text('model'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('nonprofit_applications_space_id_created_at_idx').on(table.spaceId, table.createdAt),
    foreignKey({
      name: 'nonprofit_applications_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'nonprofit_applications_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const queuedEmails = pgTable(
  'queued_emails',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    kind: text('kind').notNull(),
    subjectId: text('subject_id').notNull(),
    batchId: text('batch_id').notNull(),
    userId: text('user_id').notNull(),
    status: queuedEmailStatusEnum('status').notNull().default('pending'),
    sendAfter: timestamp('send_after', { precision: 3 }),
    attempts: integer('attempts').notNull().default(0),
    claimedAt: timestamp('claimed_at', { precision: 3 }),
    sentAt: timestamp('sent_at', { precision: 3 }),
    lastError: text('last_error'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('queued_emails_status_created_at_idx').on(table.status, table.createdAt),
    index('queued_emails_status_updated_at_idx').on(table.status, table.updatedAt),
    index('queued_emails_batch_id_idx').on(table.batchId),
    index('queued_emails_user_id_status_idx').on(table.userId, table.status),
    foreignKey({
      name: 'queued_emails_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const eventTypes = pgTable(
  'event_types',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    hostId: text('host_id').notNull(),
    name: text('name').notNull(),
    duration: integer('duration_minutes').notNull(),
    capacity: integer('capacity'),
    description: text('description'),
    location: jsonb('location'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    deleted: boolean('deleted').notNull().default(false),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
  },
  (table) => [
    index('event_types_space_id_idx').on(table.spaceId),
    index('event_types_host_id_idx').on(table.hostId),
    foreignKey({
      name: 'event_types_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'event_types_host_id_fkey',
      columns: [table.hostId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scheduledEvents = pgTable(
  'scheduled_events',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    spaceId: text('space_id').notNull(),
    eventTypeId: text('event_type_id'),
    sheetSlotId: text('sheet_slot_id'),
    title: text('title').notNull(),
    description: text('description'),
    location: jsonb('location'),
    conferencing: jsonb('conferencing'),
    capacity: integer('capacity'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    status: scheduledEventStatusEnum('status').notNull().default('confirmed'),
    timeZone: text('time_zone'),
    start: timestamp('start', { precision: 3 }).notNull(),
    end: timestamp('end', { precision: 3 }).notNull(),
    allDay: boolean('all_day').notNull().default(false),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
    sequence: integer('sequence').notNull().default(0),
    uid: text('uid').notNull(),
    hideAttendees: boolean('hide_attendees').notNull().default(false),
  },
  (table) => [
    uniqueIndex('scheduled_events_sheet_slot_id_key').on(table.sheetSlotId),
    uniqueIndex('scheduled_events_uid_key').on(table.uid),
    index('scheduled_events_space_id_idx').on(table.spaceId),
    index('scheduled_events_user_id_idx').on(table.userId),
    index('scheduled_events_event_type_id_idx').on(table.eventTypeId),
    index('scheduled_events_space_id_status_start_idx').on(
      table.spaceId,
      table.status,
      table.start,
    ),
    foreignKey({
      name: 'scheduled_events_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scheduled_events_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scheduled_events_event_type_id_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventTypes.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scheduled_events_sheet_slot_id_fkey',
      columns: [table.sheetSlotId],
      foreignColumns: [sheetSlots.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const rescheduledEventDates = pgTable(
  'rescheduled_event_dates',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    scheduledEventId: text('scheduled_event_id').notNull(),
    start: timestamp('start', { precision: 3 }).notNull(),
    end: timestamp('end', { precision: 3 }).notNull(),
    allDay: boolean('all_day').notNull().default(false),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('rescheduled_event_dates_scheduled_event_id_idx').on(table.scheduledEventId),
    foreignKey({
      name: 'rescheduled_event_dates_scheduled_event_id_fkey',
      columns: [table.scheduledEventId],
      foreignColumns: [scheduledEvents.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scheduledEventInvites = pgTable(
  'scheduled_event_invites',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    uid: text('uid').notNull(),
    scheduledEventId: text('scheduled_event_id').notNull(),
    inviteeName: text('invitee_name').notNull(),
    inviteeEmail: text('invitee_email').notNull(),
    inviteeId: text('invitee_id'),
    inviteeTimeZone: text('invitee_time_zone'),
    inviteeLocale: text('invitee_locale'),
    status: scheduledEventInviteStatusEnum('status').notNull().default('pending'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('scheduled_event_invites_uid_key').on(table.uid),
    index('scheduled_event_invites_invitee_id_idx').on(table.inviteeId),
    index('scheduled_event_invites_invitee_email_idx').on(table.inviteeEmail),
    uniqueIndex('scheduled_event_invites_scheduled_event_id_invitee_email_key').on(
      table.scheduledEventId,
      table.inviteeEmail,
    ),
    foreignKey({
      name: 'scheduled_event_invites_scheduled_event_id_fkey',
      columns: [table.scheduledEventId],
      foreignColumns: [scheduledEvents.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scheduled_event_invites_invitee_id_fkey',
      columns: [table.inviteeId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const instanceSettings = pgTable(
  'instance_settings',
  {
    id: integer('id').primaryKey().default(1),
    disableUserRegistration: boolean('disable_user_registration').notNull().default(false),
    instanceId: uuid('instance_id')
      .notNull()
      .default(sql`gen_random_uuid()`),
    appName: text('app_name'),
    primaryColor: text('primary_color'),
    primaryColorDark: text('primary_color_dark'),
    logo: text('logo'),
    logoDark: text('logo_dark'),
    logoIcon: text('logo_icon'),
    hideAttribution: boolean('hide_attribution'),
    footerLinks: jsonb('footer_links').notNull().default([]),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('instance_settings_instance_id_key').on(table.instanceId)],
)

export const credentials = pgTable(
  'credentials',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('provider_account_id').notNull(),
    type: credentialTypeEnum('type').notNull(),
    secret: text('secret').notNull(),
    scopes: text('scopes').array(),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table): PgTableExtraConfigValue[] => [
    index('credential_expiry_idx').on(table.expiresAt),
    uniqueIndex('credentials_user_id_provider_provider_account_id_key').on(
      table.userId,
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'credentials_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const calendarConnections = pgTable(
  'calendar_connections',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    provider: text('provider').notNull(),
    integrationId: text('integration_id').notNull(),
    providerAccountId: text('provider_account_id').notNull(),
    email: text('email').notNull(),
    displayName: text('display_name'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    credentialId: text('credential_id').notNull(),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('calendar_connections_user_id_provider_provider_account_id_key').on(
      table.userId,
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'calendar_connections_credential_id_fkey',
      columns: [table.credentialId],
      foreignColumns: [credentials.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'calendar_connections_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const providerCalendars = pgTable(
  'provider_calendars',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    calendarConnectionId: text('calendar_connection_id').notNull(),
    providerCalendarId: text('provider_calendar_id').notNull(),
    name: text('name').notNull(),
    isPrimary: boolean('primary').notNull().default(false),
    timeZone: text('time_zone'),
    isSelected: boolean('selected').notNull().default(false),
    isDeleted: boolean('deleted').notNull().default(false),
    isWritable: boolean('writable').notNull().default(false),
    providerData: jsonb('provider_data'),
    lastSyncedAt: timestamp('last_synced_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table): PgTableExtraConfigValue[] => [
    index('connection_selected_idx').on(table.calendarConnectionId, table.isSelected),
    index('primary_calendar_idx').on(table.isPrimary),
    index('sync_time_idx').on(table.lastSyncedAt),
    uniqueIndex('provider_calendars_calendar_connection_id_provider_calendar_key').on(
      table.calendarConnectionId,
      table.providerCalendarId,
    ),
    foreignKey({
      name: 'provider_calendars_calendar_connection_id_fkey',
      columns: [table.calendarConnectionId],
      foreignColumns: [calendarConnections.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const conferencingConnections = pgTable(
  'conferencing_connections',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    provider: text('provider').notNull(),
    integrationId: text('integration_id').notNull(),
    providerAccountId: text('provider_account_id').notNull(),
    email: text('email').notNull(),
    displayName: text('display_name'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    credentialId: text('credential_id').notNull(),
  },
  (table) => [
    uniqueIndex('conferencing_connections_user_id_provider_provider_account__key').on(
      table.userId,
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'conferencing_connections_credential_id_fkey',
      columns: [table.credentialId],
      foreignColumns: [credentials.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'conferencing_connections_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const licenses = pgTable(
  'licenses',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    licenseKey: text('license_key').notNull(),
    idempotencyKey: text('idempotency_key'),
    version: integer('version'),
    type: licenseTypeEnum('type').notNull(),
    seats: integer('seats'),
    issuedAt: timestamp('issued_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    licenseeEmail: text('licensee_email'),
    licenseeName: text('licensee_name'),
    whiteLabelAddon: boolean('white_label_addon').notNull().default(false),
    status: licenseStatusEnum('status').notNull().default('ACTIVE'),
  },
  (table) => [
    uniqueIndex('licenses_license_key_key').on(table.licenseKey),
    uniqueIndex('licenses_idempotency_key_key').on(table.idempotencyKey),
  ],
)

export const licenseValidations = pgTable(
  'license_validations',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    licenseId: text('license_id').notNull(),
    ipAddress: text('ip_address'),
    fingerprint: text('fingerprint'),
    validatedAt: timestamp('validated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userAgent: text('user_agent'),
  },
  (table) => [
    foreignKey({
      name: 'license_validations_license_id_fkey',
      columns: [table.licenseId],
      foreignColumns: [licenses.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const instanceLicenses = pgTable(
  'instance_licenses',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    licenseKey: text('license_key').notNull(),
    version: integer('version'),
    type: licenseTypeEnum('type').notNull(),
    seats: integer('seats'),
    issuedAt: timestamp('issued_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    licenseeEmail: text('licensee_email'),
    licenseeName: text('licensee_name'),
    whiteLabelAddon: boolean('white_label_addon').notNull().default(false),
    status: licenseStatusEnum('status').notNull().default('ACTIVE'),
  },
  (table) => [uniqueIndex('instance_licenses_license_key_key').on(table.licenseKey)],
)

export const userNotificationPreferences = pgTable(
  'user_notification_preferences',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    prefs: jsonb('prefs').notNull().default({}),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('user_notification_preferences_user_id_key').on(table.userId),
    foreignKey({
      name: 'user_notification_preferences_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const polls = pgTable(
  'polls',
  {
    id: text('id').primaryKey(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    deadline: timestamp('deadline', { precision: 3 }),
    title: text('title').notNull(),
    description: text('description'),
    location: text('location'),
    conferencing: jsonb('conferencing'),
    userId: text('user_id'),
    timeZone: text('time_zone'),
    status: pollStatusEnum('status').notNull().default('open'),
    closedReason: pollClosedReasonEnum('closed_reason'),
    kind: pollKindEnum('kind').notNull().default('date'),
    deleted: boolean('deleted').notNull().default(false),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
    eventId: text('event_id'),
    scheduledEventId: text('scheduled_event_id'),
    hideParticipants: boolean('hide_participants').notNull().default(false),
    hideScores: boolean('hide_scores').notNull().default(false),
    disableComments: boolean('disable_comments').notNull().default(true),
    requireParticipantEmail: boolean('require_participant_email').notNull().default(false),
    allowTentativeVotes: boolean('allow_tentative_votes').notNull().default(true),
    muted: boolean('muted').notNull().default(false),
    spaceId: text('space_id'),
  },
  (table) => [
    unique('polls_id_key').on(table.id),
    uniqueIndex('polls_event_id_key').on(table.eventId),
    index('polls_space_id_idx').on(table.spaceId),
    index('polls_user_id_idx').on(table.userId),
    foreignKey({ name: 'polls_user_id_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'polls_scheduled_event_id_fkey',
      columns: [table.scheduledEventId],
      foreignColumns: [scheduledEvents.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'polls_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const participants = pgTable(
  'participants',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    email: text('email'),
    note: text('note'),
    token: text('token').notNull(),
    userId: text('user_id'),
    pollId: text('poll_id').notNull(),
    locale: text('locale'),
    timeZone: text('time_zone'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('participants_token_key').on(table.token),
    index('participants_poll_id_idx').on(table.pollId),
    index('participants_user_id_idx').on(table.userId),
    foreignKey({
      name: 'participants_poll_id_fkey',
      columns: [table.pollId],
      foreignColumns: [polls.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'participants_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const pollInvites = pgTable(
  'poll_invites',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    pollId: text('poll_id').notNull(),
    email: text('email').notNull(),
    token: text('token').notNull(),
    openedAt: timestamp('opened_at', { precision: 3 }),
    remindedAt: timestamp('reminded_at', { precision: 3 }),
    revokedAt: timestamp('revoked_at', { precision: 3 }),
    participantId: text('participant_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('poll_invites_token_key').on(table.token),
    uniqueIndex('poll_invites_participant_id_key').on(table.participantId),
    uniqueIndex('poll_invites_poll_id_email_key').on(table.pollId, table.email),
    foreignKey({
      name: 'poll_invites_poll_id_fkey',
      columns: [table.pollId],
      foreignColumns: [polls.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'poll_invites_participant_id_fkey',
      columns: [table.participantId],
      foreignColumns: [participants.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const pollActivities = pgTable(
  'poll_activities',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    pollId: text('poll_id').notNull(),
    type: text('type').notNull(),
    userId: text('user_id'),
    participantId: text('participant_id'),
    inviteId: text('invite_id'),
    optionId: text('option_id'),
    payload: jsonb('payload').notNull().default({}),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('poll_activities_poll_id_created_at_idx').on(table.pollId, table.createdAt),
    index('poll_activities_poll_id_participant_id_idx').on(table.pollId, table.participantId),
    index('poll_activities_poll_id_option_id_idx').on(table.pollId, table.optionId),
    index('poll_activities_poll_id_invite_id_idx').on(table.pollId, table.inviteId),
    index('poll_activities_user_id_idx').on(table.userId),
    foreignKey({
      name: 'poll_activities_poll_id_fkey',
      columns: [table.pollId],
      foreignColumns: [polls.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const options = pgTable(
  'options',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    startTime: timestamp('start_time', { precision: 0 }).notNull(),
    duration: integer('duration_minutes').notNull().default(0),
    pollId: text('poll_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('options_poll_id_idx').on(table.pollId),
    foreignKey({
      name: 'options_poll_id_fkey',
      columns: [table.pollId],
      foreignColumns: [polls.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const votes = pgTable(
  'votes',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    participantId: text('participant_id').notNull(),
    optionId: text('option_id').notNull(),
    pollId: text('poll_id').notNull(),
    type: voteTypeEnum('type').notNull().default('yes'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('votes_poll_id_idx').on(table.pollId),
    index('votes_participant_id_idx').on(table.participantId),
    index('votes_option_id_idx').on(table.optionId),
    foreignKey({
      name: 'votes_participant_id_fkey',
      columns: [table.participantId],
      foreignColumns: [participants.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'votes_option_id_fkey',
      columns: [table.optionId],
      foreignColumns: [options.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'votes_poll_id_fkey', columns: [table.pollId], foreignColumns: [polls.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const comments = pgTable(
  'comments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    content: text('content').notNull(),
    pollId: text('poll_id').notNull(),
    authorName: text('author_name').notNull(),
    userId: text('user_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('comments_poll_id_idx').on(table.pollId),
    index('comments_user_id_idx').on(table.userId),
    foreignKey({
      name: 'comments_poll_id_fkey',
      columns: [table.pollId],
      foreignColumns: [polls.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'comments_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const registeredInstances = pgTable(
  'registered_instances',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    instanceId: text('instance_id').notNull(),
    version: text('version').notNull(),
    firstSeenAt: timestamp('first_seen_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastSeenAt: timestamp('last_seen_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('registered_instances_instance_id_key').on(table.instanceId)],
)

export const sheets = pgTable(
  'sheets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    hostId: text('host_id').notNull(),
    title: text('title').notNull(),
    description: text('description'),
    urlId: text('url_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    deleted: boolean('deleted').notNull().default(false),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('sheets_url_id_key').on(table.urlId),
    index('sheets_space_id_idx').on(table.spaceId),
    index('sheets_host_id_idx').on(table.hostId),
    foreignKey({
      name: 'sheets_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'sheets_host_id_fkey', columns: [table.hostId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sheetSlots = pgTable(
  'sheet_slots',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    sheetId: text('sheet_id').notNull(),
    eventTypeId: text('event_type_id').notNull(),
    startTime: timestamp('start_time', { precision: 3 }).notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('sheet_slots_sheet_id_start_time_idx').on(table.sheetId, table.startTime),
    index('sheet_slots_event_type_id_idx').on(table.eventTypeId),
    foreignKey({
      name: 'sheet_slots_sheet_id_fkey',
      columns: [table.sheetId],
      foreignColumns: [sheets.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'sheet_slots_event_type_id_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventTypes.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const spaces = pgTable(
  'spaces',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    image: text('image'),
    ownerId: text('owner_id').notNull(),
    tier: spaceTierEnum('tier').notNull().default('hobby'),
    spaceType: spaceTypeEnum('space_type'),
    industry: text('industry'),
    primaryColor: text('primary_color'),
    showBranding: boolean('show_branding').notNull().default(false),
    hideAttribution: boolean('hide_attribution').notNull().default(false),
    shared: boolean('shared').notNull().default(false),
    nonprofitDiscountGrantedAt: timestamp('nonprofit_discount_granted_at', { precision: 3 }),
    apiPollQuotaOverride: integer('api_poll_quota_override'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('spaces_owner_id_idx').on(table.ownerId),
    foreignKey({
      name: 'spaces_owner_id_fkey',
      columns: [table.ownerId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const spaceMembers = pgTable(
  'space_members',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    spaceId: text('space_id').notNull(),
    userId: text('user_id').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    role: spaceMemberRoleEnum('role').notNull().default('MEMBER'),
    lastSelectedAt: timestamp('last_selected_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('space_members_user_id_idx').on(table.userId),
    uniqueIndex('space_members_space_id_user_id_key').on(table.spaceId, table.userId),
    foreignKey({
      name: 'space_members_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'space_members_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const spaceMemberInvites = pgTable(
  'space_member_invites',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    spaceId: text('space_id').notNull(),
    email: text('email').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    role: spaceMemberRoleEnum('role').notNull().default('MEMBER'),
    inviterId: text('inviter_id').notNull(),
  },
  (table) => [
    uniqueIndex('space_member_invites_space_id_email_key').on(table.spaceId, table.email),
    foreignKey({
      name: 'space_member_invites_inviter_id_fkey',
      columns: [table.inviterId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'space_member_invites_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const spaceApiKeys = pgTable(
  'space_api_keys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    name: text('name').notNull(),
    prefix: text('prefix').notNull(),
    hashedKey: text('hashed_key').notNull(),
    lastUsedAt: timestamp('last_used_at', { precision: 3 }),
    expiresAt: timestamp('expires_at', { precision: 3 }),
    revokedAt: timestamp('revoked_at', { precision: 3 }),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('space_api_keys_prefix_key').on(table.prefix),
    index('space_api_key_space_idx').on(table.spaceId),
    index('space_api_key_expires_idx').on(table.expiresAt),
    index('space_api_key_revoked_idx').on(table.revokedAt),
    foreignKey({
      name: 'space_api_keys_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const spaceWebhooks = pgTable(
  'space_webhooks',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    url: text('url').notNull(),
    secret: text('secret').notNull(),
    events: text('events').array(),
    enabled: boolean('enabled').notNull().default(true),
    version: text('version').notNull(),
    cursor: timestamp('cursor', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastDeliveredAt: timestamp('last_delivered_at', { precision: 3 }),
    lastFailedAt: timestamp('last_failed_at', { precision: 3 }),
    consecutiveFailures: integer('consecutive_failures').notNull().default(0),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('space_webhooks_space_id_idx').on(table.spaceId),
    foreignKey({
      name: 'space_webhooks_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [spaces.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhookDeliveries = pgTable(
  'webhook_deliveries',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    webhookId: text('webhook_id').notNull(),
    activityId: text('activity_id').notNull(),
    eventType: text('event_type').notNull(),
    payload: jsonb('payload').notNull(),
    status: webhookDeliveryStatusEnum('status').notNull().default('pending'),
    attempts: integer('attempts').notNull().default(0),
    nextAttemptAt: timestamp('next_attempt_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastResponseStatus: integer('last_response_status'),
    lastError: text('last_error'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('webhook_deliveries_status_next_attempt_at_idx').on(table.status, table.nextAttemptAt),
    uniqueIndex('webhook_deliveries_webhook_id_activity_id_key').on(
      table.webhookId,
      table.activityId,
    ),
    foreignKey({
      name: 'webhook_deliveries_webhook_id_fkey',
      columns: [table.webhookId],
      foreignColumns: [spaceWebhooks.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accounts = pgTable(
  'accounts',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('provider_account_id').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    scope: text('scope'),
    id_token: text('id_token'),
    accessTokenExpiresAt: timestamp('access_token_expires_at', { precision: 3 }),
    refreshTokenExpiresAt: timestamp('refresh_token_expires_at', { precision: 3 }),
    password: text('password'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('accounts_user_id_idx').on(table.userId),
    uniqueIndex('accounts_provider_provider_account_id_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({
      name: 'accounts_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const users = pgTable(
  'users',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    email: text('email').notNull(),
    emailVerified: boolean('email_verified'),
    image: text('image'),
    timeZone: text('time_zone'),
    weekStart: integer('week_start'),
    timeFormat: timeFormatEnum('time_format'),
    locale: text('locale'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).$onUpdate(utcNow),
    customerId: text('customer_id'),
    banned: boolean('banned').notNull().default(false),
    bannedAt: timestamp('banned_at', { precision: 3 }),
    banReason: text('ban_reason'),
    banExpires: timestamp('ban_expires', { precision: 3 }),
    role: userRoleEnum('role').notNull().default('user'),
    lastLoginMethod: text('last_login_method'),
    isAnonymous: boolean('anonymous').notNull().default(false),
    lastSeenAt: timestamp('last_seen_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deletedAt: timestamp('deleted_at', { precision: 3 }),
    jobTitle: text('job_title'),
    reviewRequestedAt: timestamp('review_requested_at', { precision: 3 }),
    defaultDestinationCalendarId: text('default_destination_calendar_id'),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('users_email_key').on(table.email),
    index('users_deleted_at_idx').on(table.deletedAt),
    index('users_anonymous_last_seen_at_idx').on(table.isAnonymous, table.lastSeenAt),
    foreignKey({
      name: 'users_default_destination_calendar_id_fkey',
      columns: [table.defaultDestinationCalendarId],
      foreignColumns: [providerCalendars.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const sessions = pgTable(
  'sessions',
  {
    id: text('id').primaryKey(),
    expiresAt: timestamp('expires_at', { precision: 3 }).notNull(),
    token: text('token').notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    ipAddress: text('ip_address'),
    userAgent: text('user_agent'),
    userId: text('user_id').notNull(),
    impersonatedBy: text('impersonated_by'),
  },
  (table) => [
    uniqueIndex('sessions_token_key').on(table.token),
    foreignKey({
      name: 'sessions_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'sessions_impersonated_by_fkey',
      columns: [table.impersonatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const verifications = pgTable(
  'verifications',
  {
    id: text('id').primaryKey(),
    identifier: text('identifier').notNull(),
    value: text('value').notNull(),
    expiresAt: timestamp('expires_at', { precision: 3 }).notNull(),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('verifications_identifier_key').on(table.identifier),
    index('verifications_identifier_idx').on(table.identifier),
  ],
)

export const relations = defineRelations(
  {
    apiUsage,
    subscriptions,
    paymentMethods,
    nonprofitApplications,
    queuedEmails,
    eventTypes,
    scheduledEvents,
    rescheduledEventDates,
    scheduledEventInvites,
    instanceSettings,
    credentials,
    calendarConnections,
    providerCalendars,
    conferencingConnections,
    licenses,
    licenseValidations,
    instanceLicenses,
    userNotificationPreferences,
    polls,
    participants,
    pollInvites,
    pollActivities,
    options,
    votes,
    comments,
    registeredInstances,
    sheets,
    sheetSlots,
    spaces,
    spaceMembers,
    spaceMemberInvites,
    spaceApiKeys,
    spaceWebhooks,
    webhookDeliveries,
    accounts,
    users,
    sessions,
    verifications,
  },
  (r) => ({
    apiUsage: {
      space: r.one.spaces({ from: r.apiUsage.spaceId, to: r.spaces.id, optional: false }),
    },
    subscriptions: {
      user: r.one.users({ from: r.subscriptions.userId, to: r.users.id, optional: false }),
      space: r.one.spaces({ from: r.subscriptions.spaceId, to: r.spaces.id, optional: false }),
    },
    paymentMethods: {
      user: r.one.users({ from: r.paymentMethods.userId, to: r.users.id, optional: false }),
    },
    nonprofitApplications: {
      space: r.one.spaces({
        from: r.nonprofitApplications.spaceId,
        to: r.spaces.id,
        optional: false,
      }),
      user: r.one.users({ from: r.nonprofitApplications.userId, to: r.users.id, optional: false }),
    },
    queuedEmails: {
      user: r.one.users({ from: r.queuedEmails.userId, to: r.users.id, optional: false }),
    },
    eventTypes: {
      space: r.one.spaces({ from: r.eventTypes.spaceId, to: r.spaces.id, optional: false }),
      host: r.one.users({ from: r.eventTypes.hostId, to: r.users.id, optional: false }),
      slots: r.many.sheetSlots(),
      scheduledEvents: r.many.scheduledEvents(),
    },
    scheduledEvents: {
      user: r.one.users({ from: r.scheduledEvents.userId, to: r.users.id, optional: false }),
      space: r.one.spaces({ from: r.scheduledEvents.spaceId, to: r.spaces.id, optional: false }),
      eventType: r.one.eventTypes({ from: r.scheduledEvents.eventTypeId, to: r.eventTypes.id }),
      sheetSlot: r.one.sheetSlots({ from: r.scheduledEvents.sheetSlotId, to: r.sheetSlots.id }),
      rescheduledDates: r.many.rescheduledEventDates(),
      invites: r.many.scheduledEventInvites(),
      polls: r.many.polls(),
    },
    rescheduledEventDates: {
      scheduledEvent: r.one.scheduledEvents({
        from: r.rescheduledEventDates.scheduledEventId,
        to: r.scheduledEvents.id,
        optional: false,
      }),
    },
    scheduledEventInvites: {
      scheduledEvent: r.one.scheduledEvents({
        from: r.scheduledEventInvites.scheduledEventId,
        to: r.scheduledEvents.id,
        optional: false,
      }),
      user: r.one.users({ from: r.scheduledEventInvites.inviteeId, to: r.users.id }),
    },
    credentials: {
      calendarConnections: r.many.calendarConnections(),
      conferencingConnections: r.many.conferencingConnections(),
      user: r.one.users({ from: r.credentials.userId, to: r.users.id, optional: false }),
    },
    calendarConnections: {
      credential: r.one.credentials({
        from: r.calendarConnections.credentialId,
        to: r.credentials.id,
        optional: false,
      }),
      user: r.one.users({ from: r.calendarConnections.userId, to: r.users.id, optional: false }),
      providerCalendars: r.many.providerCalendars(),
    },
    providerCalendars: {
      calendarConnection: r.one.calendarConnections({
        from: r.providerCalendars.calendarConnectionId,
        to: r.calendarConnections.id,
        optional: false,
      }),
      userDefaultDestination: r.many.users(),
    },
    conferencingConnections: {
      credential: r.one.credentials({
        from: r.conferencingConnections.credentialId,
        to: r.credentials.id,
        optional: false,
      }),
      user: r.one.users({
        from: r.conferencingConnections.userId,
        to: r.users.id,
        optional: false,
      }),
    },
    licenses: { validations: r.many.licenseValidations() },
    licenseValidations: {
      license: r.one.licenses({
        from: r.licenseValidations.licenseId,
        to: r.licenses.id,
        optional: false,
      }),
    },
    userNotificationPreferences: {
      user: r.one.users({
        from: r.userNotificationPreferences.userId,
        to: r.users.id,
        optional: false,
      }),
    },
    polls: {
      user: r.one.users({ from: r.polls.userId, to: r.users.id }),
      scheduledEvent: r.one.scheduledEvents({
        from: r.polls.scheduledEventId,
        to: r.scheduledEvents.id,
      }),
      options: r.many.options(),
      participants: r.many.participants(),
      comments: r.many.comments(),
      votes: r.many.votes(),
      invites: r.many.pollInvites(),
      activities: r.many.pollActivities(),
      space: r.one.spaces({ from: r.polls.spaceId, to: r.spaces.id }),
    },
    participants: {
      votes: r.many.votes(),
      invite: r.one.pollInvites({ from: r.participants.id, to: r.pollInvites.participantId }),
      poll: r.one.polls({ from: r.participants.pollId, to: r.polls.id, optional: false }),
      user: r.one.users({ from: r.participants.userId, to: r.users.id }),
    },
    pollInvites: {
      poll: r.one.polls({ from: r.pollInvites.pollId, to: r.polls.id, optional: false }),
      participant: r.one.participants({ from: r.pollInvites.participantId, to: r.participants.id }),
    },
    pollActivities: {
      poll: r.one.polls({ from: r.pollActivities.pollId, to: r.polls.id, optional: false }),
    },
    options: {
      votes: r.many.votes(),
      poll: r.one.polls({ from: r.options.pollId, to: r.polls.id, optional: false }),
    },
    votes: {
      participant: r.one.participants({
        from: r.votes.participantId,
        to: r.participants.id,
        optional: false,
      }),
      option: r.one.options({ from: r.votes.optionId, to: r.options.id, optional: false }),
      poll: r.one.polls({ from: r.votes.pollId, to: r.polls.id, optional: false }),
    },
    comments: {
      poll: r.one.polls({ from: r.comments.pollId, to: r.polls.id, optional: false }),
      user: r.one.users({ from: r.comments.userId, to: r.users.id }),
    },
    sheets: {
      space: r.one.spaces({ from: r.sheets.spaceId, to: r.spaces.id, optional: false }),
      host: r.one.users({ from: r.sheets.hostId, to: r.users.id, optional: false }),
      slots: r.many.sheetSlots(),
    },
    sheetSlots: {
      sheet: r.one.sheets({ from: r.sheetSlots.sheetId, to: r.sheets.id, optional: false }),
      eventType: r.one.eventTypes({
        from: r.sheetSlots.eventTypeId,
        to: r.eventTypes.id,
        optional: false,
      }),
      scheduledEvent: r.one.scheduledEvents({
        from: r.sheetSlots.id,
        to: r.scheduledEvents.sheetSlotId,
      }),
    },
    spaces: {
      owner: r.one.users({ from: r.spaces.ownerId, to: r.users.id, optional: false }),
      polls: r.many.polls(),
      scheduledEvents: r.many.scheduledEvents(),
      eventTypes: r.many.eventTypes(),
      sheets: r.many.sheets(),
      subscriptions: r.many.subscriptions(),
      members: r.many.spaceMembers(),
      memberInvites: r.many.spaceMemberInvites(),
      apiKeys: r.many.spaceApiKeys(),
      nonprofitApplications: r.many.nonprofitApplications(),
      webhooks: r.many.spaceWebhooks(),
      apiUsage: r.many.apiUsage(),
    },
    spaceMembers: {
      space: r.one.spaces({ from: r.spaceMembers.spaceId, to: r.spaces.id, optional: false }),
      user: r.one.users({ from: r.spaceMembers.userId, to: r.users.id, optional: false }),
    },
    spaceMemberInvites: {
      invitedBy: r.one.users({
        from: r.spaceMemberInvites.inviterId,
        to: r.users.id,
        optional: false,
      }),
      space: r.one.spaces({ from: r.spaceMemberInvites.spaceId, to: r.spaces.id, optional: false }),
    },
    spaceApiKeys: {
      space: r.one.spaces({ from: r.spaceApiKeys.spaceId, to: r.spaces.id, optional: false }),
    },
    spaceWebhooks: {
      space: r.one.spaces({ from: r.spaceWebhooks.spaceId, to: r.spaces.id, optional: false }),
      deliveries: r.many.webhookDeliveries(),
    },
    webhookDeliveries: {
      webhook: r.one.spaceWebhooks({
        from: r.webhookDeliveries.webhookId,
        to: r.spaceWebhooks.id,
        optional: false,
      }),
    },
    accounts: { user: r.one.users({ from: r.accounts.userId, to: r.users.id, optional: false }) },
    users: {
      comments: r.many.comments(),
      polls: r.many.polls(),
      notificationPreferences: r.one.userNotificationPreferences({
        from: r.users.id,
        to: r.userNotificationPreferences.userId,
      }),
      queuedEmails: r.many.queuedEmails(),
      accounts: r.many.accounts(),
      participants: r.many.participants(),
      paymentMethods: r.many.paymentMethods(),
      spaceMemberInvites: r.many.spaceMemberInvites(),
      nonprofitApplications: r.many.nonprofitApplications(),
      subscriptions: r.many.subscriptions(),
      spaces: r.many.spaces(),
      memberOf: r.many.spaceMembers(),
      scheduledEvents: r.many.scheduledEvents(),
      scheduledEventInvites: r.many.scheduledEventInvites(),
      hostedEventTypes: r.many.eventTypes(),
      hostedSheets: r.many.sheets(),
      calendarConnections: r.many.calendarConnections(),
      conferencingConnections: r.many.conferencingConnections(),
      credentials: r.many.credentials(),
      defaultDestinationCalendar: r.one.providerCalendars({
        from: r.users.defaultDestinationCalendarId,
        to: r.providerCalendars.id,
      }),
      sessions: r.many.sessions({ alias: 'SessionToUser' }),
      impersonatedSessions: r.many.sessions({ alias: 'ImpersonatedSessions' }),
    },
    sessions: {
      user: r.one.users({
        from: r.sessions.userId,
        to: r.users.id,
        optional: false,
        alias: 'SessionToUser',
      }),
      impersonatedByUser: r.one.users({
        from: r.sessions.impersonatedBy,
        to: r.users.id,
        alias: 'ImpersonatedSessions',
      }),
    },
  }),
)
