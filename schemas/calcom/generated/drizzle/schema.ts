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
  serial,
  text,
  timestamp,
  unique,
  uniqueIndex,
  uuid,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'
import { v7 as uuidv7 } from 'uuid'

const utcDate = customType<{ data: Date; driverData: string }>({
  dataType: () => 'date',
  toDriver: (value) => value.toISOString().slice(0, 10),
  fromDriver: (value) => new Date(value),
})

const utcTime = customType<{
  data: Date
  driverData: string
  config: { precision?: number }
}>({
  dataType: (config) => `time${config?.precision === undefined ? '' : `(${config.precision})`}`,
  toDriver: (value) => value.toISOString().slice(11, 23),
  fromDriver: (value) => new Date(`1970-01-01T${value}Z`),
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

export const schedulingTypeEnum = pgEnum('SchedulingType', ['roundRobin', 'collective', 'managed'])

export const periodTypeEnum = pgEnum('PeriodType', [
  'unlimited',
  'rolling',
  'rolling_window',
  'range',
])

export const creationSourceEnum = pgEnum('CreationSource', ['api_v1', 'api_v2', 'webapp'])

export const cancellationReasonRequirementEnum = pgEnum('CancellationReasonRequirement', [
  'MANDATORY_BOTH',
  'MANDATORY_HOST_ONLY',
  'MANDATORY_ATTENDEE_ONLY',
  'OPTIONAL_BOTH',
])

export const identityProviderEnum = pgEnum('IdentityProvider', ['CAL', 'GOOGLE', 'SAML', 'AZUREAD'])

export const userPermissionRoleEnum = pgEnum('UserPermissionRole', ['USER', 'ADMIN'])

export const creditUsageTypeEnum = pgEnum('CreditUsageType', ['SMS', 'CAL_AI_PHONE_CALL'])

export const creditTypeEnum = pgEnum('CreditType', ['MONTHLY', 'ADDITIONAL'])

export const membershipRoleEnum = pgEnum('MembershipRole', ['MEMBER', 'ADMIN', 'OWNER'])

export const bookingStatusEnum = pgEnum('BookingStatus', [
  'cancelled',
  'accepted',
  'rejected',
  'pending',
  'awaiting_host',
])

export const eventTypeCustomInputTypeEnum = pgEnum('EventTypeCustomInputType', [
  'text',
  'textLong',
  'number',
  'bool',
  'radio',
  'phone',
])

export const reminderTypeEnum = pgEnum('ReminderType', ['PENDING_BOOKING_CONFIRMATION'])

export const paymentOptionEnum = pgEnum('PaymentOption', ['ON_BOOKING', 'HOLD'])

export const webhookTriggerEventsEnum = pgEnum('WebhookTriggerEvents', [
  'BOOKING_CREATED',
  'BOOKING_PAYMENT_INITIATED',
  'BOOKING_PAID',
  'BOOKING_RESCHEDULED',
  'BOOKING_REQUESTED',
  'BOOKING_CANCELLED',
  'BOOKING_REJECTED',
  'BOOKING_NO_SHOW_UPDATED',
  'FORM_SUBMITTED',
  'MEETING_ENDED',
  'MEETING_STARTED',
  'RECORDING_READY',
  'RECORDING_TRANSCRIPTION_GENERATED',
  'OOO_CREATED',
  'AFTER_HOSTS_CAL_VIDEO_NO_SHOW',
  'AFTER_GUESTS_CAL_VIDEO_NO_SHOW',
  'FORM_SUBMITTED_NO_EVENT',
  'DELEGATION_CREDENTIAL_ERROR',
  'WRONG_ASSIGNMENT_REPORT',
])

export const appCategoriesEnum = pgEnum('AppCategories', [
  'calendar',
  'messaging',
  'other',
  'payment',
  'video',
  'web3',
  'automation',
  'analytics',
  'conferencing',
  'crm',
])

export const timeUnitEnum = pgEnum('TimeUnit', ['day', 'hour', 'minute'])

export const featureTypeEnum = pgEnum('FeatureType', [
  'RELEASE',
  'EXPERIMENT',
  'OPERATIONAL',
  'KILL_SWITCH',
  'PERMISSION',
])

export const rrresetIntervalEnum = pgEnum('RRResetInterval', ['MONTH', 'DAY'])

export const rrtimestampBasisEnum = pgEnum('RRTimestampBasis', ['CREATED_AT', 'START_TIME'])

export const oauthClientTypeEnum = pgEnum('OAuthClientType', ['confidential', 'public'])

export const oauthClientStatusEnum = pgEnum('OAuthClientStatus', [
  'pending',
  'approved',
  'rejected',
])

export const accessScopeEnum = pgEnum('AccessScope', ['READ_BOOKING', 'READ_PROFILE'])

export const redirectTypeEnum = pgEnum('RedirectType', [
  'user-event-type',
  'team-event-type',
  'user',
  'team',
])

export const smslockStateEnum = pgEnum('SMSLockState', ['LOCKED', 'UNLOCKED', 'REVIEW_NEEDED'])

export const attributeTypeEnum = pgEnum('AttributeType', [
  'TEXT',
  'NUMBER',
  'SINGLE_SELECT',
  'MULTI_SELECT',
])

export const assignmentReasonEnumEnum = pgEnum('AssignmentReasonEnum', [
  'REASSIGNED',
  'RR_REASSIGNED',
  'REROUTED',
  'SALESFORCE_ASSIGNMENT',
])

export const eventTypeAutoTranslatedFieldEnum = pgEnum('EventTypeAutoTranslatedField', [
  'DESCRIPTION',
  'TITLE',
])

export const watchlistTypeEnum = pgEnum('WatchlistType', ['EMAIL', 'DOMAIN', 'USERNAME'])

export const watchlistActionEnum = pgEnum('WatchlistAction', ['REPORT', 'BLOCK', 'ALERT'])

export const watchlistSourceEnum = pgEnum('WatchlistSource', [
  'MANUAL',
  'FREE_DOMAIN_POLICY',
  'SIGNUP',
])

export const bookingReportReasonEnum = pgEnum('BookingReportReason', [
  'SPAM',
  'dont_know_person',
  'OTHER',
])

export const bookingReportStatusEnum = pgEnum('BookingReportStatus', [
  'PENDING',
  'DISMISSED',
  'BLOCKED',
])

export const systemReportStatusEnum = pgEnum('SystemReportStatus', [
  'PENDING',
  'BLOCKED',
  'DISMISSED',
])

export const wrongAssignmentReportStatusEnum = pgEnum('WrongAssignmentReportStatus', [
  'PENDING',
  'REVIEWED',
  'RESOLVED',
  'DISMISSED',
])

export const billingPeriodEnum = pgEnum('BillingPeriod', ['MONTHLY', 'ANNUALLY'])

export const billingModeEnum = pgEnum('BillingMode', ['SEATS', 'ACTIVE_USERS'])

export const filterSegmentScopeEnum = pgEnum('FilterSegmentScope', ['USER', 'TEAM'])

export const roleTypeEnum = pgEnum('RoleType', ['SYSTEM', 'CUSTOM'])

export const bookingAuditTypeEnum = pgEnum('BookingAuditType', [
  'record_created',
  'record_updated',
  'record_deleted',
])

export const bookingAuditActionEnum = pgEnum('BookingAuditAction', [
  'created',
  'cancelled',
  'accepted',
  'rejected',
  'pending',
  'awaiting_host',
  'rescheduled',
  'attendee_added',
  'attendee_removed',
  'reassignment',
  'location_changed',
  'no_show_updated',
  'reschedule_requested',
  'seat_booked',
  'seat_rescheduled',
])

export const bookingAuditSourceEnum = pgEnum('BookingAuditSource', [
  'api_v1',
  'api_v2',
  'webapp',
  'webhook',
  'system',
  'magic_link',
  'unknown',
])

export const auditActorTypeEnum = pgEnum('AuditActorType', [
  'user',
  'guest',
  'attendee',
  'system',
  'app',
])

export const phoneNumberSubscriptionStatusEnum = pgEnum('PhoneNumberSubscriptionStatus', [
  'ACTIVE',
  'PAST_DUE',
  'CANCELLED',
  'INCOMPLETE',
  'INCOMPLETE_EXPIRED',
  'TRIALING',
  'UNPAID',
])

export const seatChangeTypeEnum = pgEnum('SeatChangeType', ['ADDITION', 'REMOVAL'])

export const prorationStatusEnum = pgEnum('ProrationStatus', [
  'PENDING',
  'INVOICE_CREATED',
  'CHARGED',
  'FAILED',
  'CANCELLED',
])

export const calendarCacheEventStatusEnum = pgEnum('CalendarCacheEventStatus', [
  'confirmed',
  'tentative',
  'cancelled',
])

export const host = pgTable(
  'Host',
  {
    userId: integer('userId').notNull(),
    eventTypeId: integer('eventTypeId').notNull(),
    isFixed: boolean('isFixed').notNull().default(false),
    priority: integer('priority'),
    weight: integer('weight'),
    weightAdjustment: integer('weightAdjustment'),
    scheduleId: integer('scheduleId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    groupId: text('groupId'),
    memberId: integer('memberId'),
  },
  (table) => [
    primaryKey({ name: 'Host_pkey', columns: [table.userId, table.eventTypeId] }),
    index('Host_memberId_idx').on(table.memberId),
    index('Host_userId_idx').on(table.userId),
    index('Host_eventTypeId_idx').on(table.eventTypeId),
    index('Host_scheduleId_idx').on(table.scheduleId),
    foreignKey({ name: 'Host_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Host_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Host_scheduleId_fkey',
      columns: [table.scheduleId],
      foreignColumns: [schedule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Host_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [hostGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Host_memberId_fkey',
      columns: [table.memberId],
      foreignColumns: [membership.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const hostGroup = pgTable(
  'HostGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    eventTypeId: integer('eventTypeId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('HostGroup_name_idx').on(table.name),
    index('HostGroup_eventTypeId_idx').on(table.eventTypeId),
    foreignKey({
      name: 'HostGroup_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const hostLocation = pgTable(
  'HostLocation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: integer('userId').notNull(),
    eventTypeId: integer('eventTypeId').notNull(),
    type: text('type').notNull(),
    credentialId: integer('credentialId'),
    link: text('link'),
    address: text('address'),
    phoneNumber: text('phoneNumber'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('HostLocation_credentialId_idx').on(table.credentialId),
    index('HostLocation_eventTypeId_idx').on(table.eventTypeId),
    uniqueIndex('HostLocation_userId_eventTypeId_key').on(table.userId, table.eventTypeId),
    foreignKey({
      name: 'HostLocation_userId_eventTypeId_fkey',
      columns: [table.userId, table.eventTypeId],
      foreignColumns: [host.userId, host.eventTypeId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'HostLocation_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const calVideoSettings = pgTable(
  'CalVideoSettings',
  {
    eventTypeId: integer('eventTypeId').primaryKey(),
    disableRecordingForOrganizer: boolean('disableRecordingForOrganizer').notNull().default(false),
    disableRecordingForGuests: boolean('disableRecordingForGuests').notNull().default(false),
    enableAutomaticTranscription: boolean('enableAutomaticTranscription').notNull().default(false),
    enableAutomaticRecordingForOrganizer: boolean('enableAutomaticRecordingForOrganizer')
      .notNull()
      .default(false),
    redirectUrlOnExit: text('redirectUrlOnExit'),
    disableTranscriptionForGuests: boolean('disableTranscriptionForGuests')
      .notNull()
      .default(false),
    disableTranscriptionForOrganizer: boolean('disableTranscriptionForOrganizer')
      .notNull()
      .default(false),
    requireEmailForGuests: boolean('requireEmailForGuests').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'CalVideoSettings_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const videoCallGuest = pgTable(
  'VideoCallGuest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    bookingUid: text('bookingUid').notNull(),
    email: text('email').notNull(),
    name: text('name').notNull(),
    joinedAt: timestamp('joinedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('VideoCallGuest_bookingUid_idx').on(table.bookingUid),
    index('VideoCallGuest_email_idx').on(table.email),
    uniqueIndex('VideoCallGuest_bookingUid_email_key').on(table.bookingUid, table.email),
  ],
)

export const eventType = pgTable(
  'EventType',
  {
    id: serial('id').primaryKey(),
    title: text('title').notNull(),
    slug: text('slug').notNull(),
    description: text('description'),
    interfaceLanguage: text('interfaceLanguage'),
    position: integer('position').notNull().default(0),
    locations: jsonb('locations'),
    length: integer('length').notNull(),
    offsetStart: integer('offsetStart').notNull().default(0),
    hidden: boolean('hidden').notNull().default(false),
    userId: integer('userId'),
    profileId: integer('profileId'),
    teamId: integer('teamId'),
    useEventLevelSelectedCalendars: boolean('useEventLevelSelectedCalendars')
      .notNull()
      .default(false),
    eventName: text('eventName'),
    parentId: integer('parentId'),
    bookingFields: jsonb('bookingFields'),
    timeZone: text('timeZone'),
    periodType: periodTypeEnum('periodType').notNull().default('unlimited'),
    periodStartDate: timestamp('periodStartDate', { precision: 3 }),
    periodEndDate: timestamp('periodEndDate', { precision: 3 }),
    periodDays: integer('periodDays'),
    periodCountCalendarDays: boolean('periodCountCalendarDays'),
    lockTimeZoneToggleOnBookingPage: boolean('lockTimeZoneToggleOnBookingPage')
      .notNull()
      .default(false),
    lockedTimeZone: text('lockedTimeZone'),
    requiresConfirmation: boolean('requiresConfirmation').notNull().default(false),
    requiresConfirmationWillBlockSlot: boolean('requiresConfirmationWillBlockSlot')
      .notNull()
      .default(false),
    requiresConfirmationForFreeEmail: boolean('requiresConfirmationForFreeEmail')
      .notNull()
      .default(false),
    requiresBookerEmailVerification: boolean('requiresBookerEmailVerification')
      .notNull()
      .default(false),
    canSendCalVideoTranscriptionEmails: boolean('canSendCalVideoTranscriptionEmails')
      .notNull()
      .default(true),
    autoTranslateDescriptionEnabled: boolean('autoTranslateDescriptionEnabled')
      .notNull()
      .default(false),
    autoTranslateInstantMeetingTitleEnabled: boolean('autoTranslateInstantMeetingTitleEnabled')
      .notNull()
      .default(true),
    recurringEvent: jsonb('recurringEvent'),
    disableGuests: boolean('disableGuests').notNull().default(false),
    hideCalendarNotes: boolean('hideCalendarNotes').notNull().default(false),
    hideCalendarEventDetails: boolean('hideCalendarEventDetails').notNull().default(false),
    minimumBookingNotice: integer('minimumBookingNotice').notNull().default(120),
    beforeEventBuffer: integer('beforeEventBuffer').notNull().default(0),
    afterEventBuffer: integer('afterEventBuffer').notNull().default(0),
    seatsPerTimeSlot: integer('seatsPerTimeSlot'),
    onlyShowFirstAvailableSlot: boolean('onlyShowFirstAvailableSlot').notNull().default(false),
    showOptimizedSlots: boolean('showOptimizedSlots').default(false),
    disableCancelling: boolean('disableCancelling').default(false),
    disableRescheduling: boolean('disableRescheduling').default(false),
    minimumRescheduleNotice: integer('minimumRescheduleNotice'),
    seatsShowAttendees: boolean('seatsShowAttendees').default(false),
    seatsShowAvailabilityCount: boolean('seatsShowAvailabilityCount').default(true),
    schedulingType: schedulingTypeEnum('schedulingType'),
    scheduleId: integer('scheduleId'),
    allowReschedulingCancelledBookings: boolean('allowReschedulingCancelledBookings').default(
      false,
    ),
    price: integer('price').notNull().default(0),
    currency: text('currency').notNull().default('usd'),
    slotInterval: integer('slotInterval'),
    metadata: jsonb('metadata'),
    successRedirectUrl: text('successRedirectUrl'),
    forwardParamsSuccessRedirect: boolean('forwardParamsSuccessRedirect').default(true),
    bookingLimits: jsonb('bookingLimits'),
    durationLimits: jsonb('durationLimits'),
    isInstantEvent: boolean('isInstantEvent').notNull().default(false),
    instantMeetingExpiryTimeOffsetInSeconds: integer('instantMeetingExpiryTimeOffsetInSeconds')
      .notNull()
      .default(90),
    instantMeetingScheduleId: integer('instantMeetingScheduleId'),
    instantMeetingParameters: text('instantMeetingParameters').array(),
    assignAllTeamMembers: boolean('assignAllTeamMembers').notNull().default(false),
    assignRRMembersUsingSegment: boolean('assignRRMembersUsingSegment').notNull().default(false),
    rrSegmentQueryValue: jsonb('rrSegmentQueryValue'),
    useEventTypeDestinationCalendarEmail: boolean('useEventTypeDestinationCalendarEmail')
      .notNull()
      .default(false),
    isRRWeightsEnabled: boolean('isRRWeightsEnabled').notNull().default(false),
    maxLeadThreshold: integer('maxLeadThreshold'),
    includeNoShowInRRCalculation: boolean('includeNoShowInRRCalculation').notNull().default(false),
    allowReschedulingPastBookings: boolean('allowReschedulingPastBookings')
      .notNull()
      .default(false),
    hideOrganizerEmail: boolean('hideOrganizerEmail').notNull().default(false),
    maxActiveBookingsPerBooker: integer('maxActiveBookingsPerBooker'),
    maxActiveBookingPerBookerOfferReschedule: boolean('maxActiveBookingPerBookerOfferReschedule')
      .notNull()
      .default(false),
    customReplyToEmail: text('customReplyToEmail'),
    eventTypeColor: jsonb('eventTypeColor'),
    rescheduleWithSameRoundRobinHost: boolean('rescheduleWithSameRoundRobinHost')
      .notNull()
      .default(false),
    secondaryEmailId: integer('secondaryEmailId'),
    useBookerTimezone: boolean('useBookerTimezone').notNull().default(false),
    restrictionScheduleId: integer('restrictionScheduleId'),
    bookingRequiresAuthentication: boolean('bookingRequiresAuthentication')
      .notNull()
      .default(false),
    rrHostSubsetEnabled: boolean('rrHostSubsetEnabled').notNull().default(false),
    requiresCancellationReason: cancellationReasonRequirementEnum(
      'requiresCancellationReason',
    ).default('MANDATORY_HOST_ONLY'),
    enablePerHostLocations: boolean('enablePerHostLocations').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('EventType_userId_idx').on(table.userId),
    index('EventType_teamId_idx').on(table.teamId),
    index('EventType_profileId_idx').on(table.profileId),
    index('EventType_scheduleId_idx').on(table.scheduleId),
    index('EventType_secondaryEmailId_idx').on(table.secondaryEmailId),
    index('EventType_parentId_idx').on(table.parentId),
    index('EventType_parentId_teamId_idx').on(table.parentId, table.teamId),
    index('EventType_restrictionScheduleId_idx').on(table.restrictionScheduleId),
    uniqueIndex('EventType_userId_slug_key').on(table.userId, table.slug),
    uniqueIndex('EventType_teamId_slug_key').on(table.teamId, table.slug),
    uniqueIndex('EventType_userId_parentId_key').on(table.userId, table.parentId),
    foreignKey({
      name: 'EventType_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_profileId_fkey',
      columns: [table.profileId],
      foreignColumns: [profile.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_scheduleId_fkey',
      columns: [table.scheduleId],
      foreignColumns: [schedule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_instantMeetingScheduleId_fkey',
      columns: [table.instantMeetingScheduleId],
      foreignColumns: [schedule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_secondaryEmailId_fkey',
      columns: [table.secondaryEmailId],
      foreignColumns: [secondaryEmail.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventType_restrictionScheduleId_fkey',
      columns: [table.restrictionScheduleId],
      foreignColumns: [schedule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const credential = pgTable(
  'Credential',
  {
    id: serial('id').primaryKey(),
    type: text('type').notNull(),
    key: jsonb('key').notNull(),
    encryptedKey: text('encryptedKey'),
    userId: integer('userId'),
    teamId: integer('teamId'),
    appId: text('appId'),
    subscriptionId: text('subscriptionId'),
    paymentStatus: text('paymentStatus'),
    billingCycleStart: integer('billingCycleStart'),
    invalid: boolean('invalid').default(false),
    delegationCredentialId: text('delegationCredentialId'),
  },
  (table) => [
    index('Credential_appId_idx').on(table.appId),
    index('Credential_subscriptionId_idx').on(table.subscriptionId),
    index('Credential_invalid_idx').on(table.invalid),
    index('Credential_userId_delegationCredentialId_idx').on(
      table.userId,
      table.delegationCredentialId,
    ),
    index('Credential_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'Credential_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Credential_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Credential_appId_fkey',
      columns: [table.appId],
      foreignColumns: [app.slug],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Credential_delegationCredentialId_fkey',
      columns: [table.delegationCredentialId],
      foreignColumns: [delegationCredential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const destinationCalendar = pgTable(
  'DestinationCalendar',
  {
    id: serial('id').primaryKey(),
    integration: text('integration').notNull(),
    externalId: text('externalId').notNull(),
    primaryEmail: text('primaryEmail'),
    userId: integer('userId'),
    eventTypeId: integer('eventTypeId'),
    credentialId: integer('credentialId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
    delegationCredentialId: text('delegationCredentialId'),
    customCalendarReminder: integer('customCalendarReminder'),
  },
  (table) => [
    uniqueIndex('DestinationCalendar_userId_key').on(table.userId),
    uniqueIndex('DestinationCalendar_eventTypeId_key').on(table.eventTypeId),
    index('DestinationCalendar_userId_idx').on(table.userId),
    index('DestinationCalendar_eventTypeId_idx').on(table.eventTypeId),
    index('DestinationCalendar_credentialId_idx').on(table.credentialId),
    foreignKey({
      name: 'DestinationCalendar_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DestinationCalendar_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DestinationCalendar_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DestinationCalendar_delegationCredentialId_fkey',
      columns: [table.delegationCredentialId],
      foreignColumns: [delegationCredential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userPassword = pgTable(
  'UserPassword',
  { hash: text('hash').notNull(), userId: integer('userId').notNull() },
  (table) => [
    uniqueIndex('UserPassword_userId_key').on(table.userId),
    foreignKey({
      name: 'UserPassword_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const travelSchedule = pgTable(
  'TravelSchedule',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    timeZone: text('timeZone').notNull(),
    startDate: timestamp('startDate', { precision: 3 }).notNull(),
    endDate: timestamp('endDate', { precision: 3 }),
    prevTimeZone: text('prevTimeZone'),
  },
  (table) => [
    index('TravelSchedule_startDate_idx').on(table.startDate),
    index('TravelSchedule_endDate_idx').on(table.endDate),
    foreignKey({
      name: 'TravelSchedule_userId_fkey',
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
    id: serial('id').primaryKey(),
    uuid: uuid('uuid')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    username: text('username'),
    name: text('name'),
    email: text('email').notNull(),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
    bio: text('bio'),
    avatarUrl: text('avatarUrl'),
    timeZone: text('timeZone').notNull().default('Europe/London'),
    weekStart: text('weekStart').notNull().default('Sunday'),
    bufferTime: integer('bufferTime').notNull().default(0),
    hideBranding: boolean('hideBranding').notNull().default(false),
    theme: text('theme'),
    appTheme: text('appTheme'),
    createdDate: timestamp('created', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    trialEndsAt: timestamp('trialEndsAt', { precision: 3 }),
    lastActiveAt: timestamp('lastActiveAt', { precision: 3 }),
    defaultScheduleId: integer('defaultScheduleId'),
    completedOnboarding: boolean('completedOnboarding').notNull().default(false),
    locale: text('locale'),
    timeFormat: integer('timeFormat').default(12),
    twoFactorSecret: text('twoFactorSecret'),
    twoFactorEnabled: boolean('twoFactorEnabled').notNull().default(false),
    backupCodes: text('backupCodes'),
    identityProvider: identityProviderEnum('identityProvider').notNull().default('CAL'),
    identityProviderId: text('identityProviderId'),
    invitedTo: integer('invitedTo'),
    brandColor: text('brandColor'),
    darkBrandColor: text('darkBrandColor'),
    allowDynamicBooking: boolean('allowDynamicBooking').default(true),
    allowSEOIndexing: boolean('allowSEOIndexing').default(true),
    receiveMonthlyDigestEmail: boolean('receiveMonthlyDigestEmail').default(true),
    requiresBookerEmailVerification: boolean('requiresBookerEmailVerification').default(false),
    metadata: jsonb('metadata'),
    verified: boolean('verified').default(false),
    role: userPermissionRoleEnum('role').notNull().default('USER'),
    organizationId: integer('organizationId'),
    locked: boolean('locked').notNull().default(false),
    movedToProfileId: integer('movedToProfileId'),
    isPlatformManaged: boolean('isPlatformManaged').notNull().default(false),
    smsLockState: smslockStateEnum('smsLockState').notNull().default('UNLOCKED'),
    smsLockReviewedByAdmin: boolean('smsLockReviewedByAdmin').notNull().default(false),
    referralLinkId: text('referralLinkId'),
    creationSource: creationSourceEnum('creationSource'),
    autoOptInFeatures: boolean('autoOptInFeatures').notNull().default(false),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('users_uuid_key').on(table.uuid),
    index('users_username_idx').on(table.username),
    index('users_emailVerified_idx').on(table.emailVerified),
    index('users_identityProvider_idx').on(table.identityProvider),
    index('users_identityProviderId_idx').on(table.identityProviderId),
    uniqueIndex('users_email_key').on(table.email),
    uniqueIndex('users_email_username_key').on(table.email, table.username),
    uniqueIndex('users_username_organizationId_key').on(table.username, table.organizationId),
    uniqueIndex('users_movedToProfileId_key').on(table.movedToProfileId),
    foreignKey({
      name: 'users_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'users_movedToProfileId_fkey',
      columns: [table.movedToProfileId],
      foreignColumns: [profile.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const notificationsSubscriptions = pgTable(
  'NotificationsSubscriptions',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    subscription: text('subscription').notNull(),
  },
  (table) => [
    index('NotificationsSubscriptions_userId_subscription_idx').on(
      table.userId,
      table.subscription,
    ),
    foreignKey({
      name: 'NotificationsSubscriptions_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const profile = pgTable(
  'Profile',
  {
    id: serial('id').primaryKey(),
    uid: text('uid').notNull(),
    userId: integer('userId').notNull(),
    organizationId: integer('organizationId').notNull(),
    username: text('username').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table): PgTableExtraConfigValue[] => [
    index('Profile_uid_idx').on(table.uid),
    index('Profile_userId_idx').on(table.userId),
    index('Profile_organizationId_idx').on(table.organizationId),
    uniqueIndex('Profile_userId_organizationId_key').on(table.userId, table.organizationId),
    uniqueIndex('Profile_username_organizationId_key').on(table.username, table.organizationId),
    foreignKey({ name: 'Profile_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Profile_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const team = pgTable(
  'Team',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    slug: text('slug'),
    logoUrl: text('logoUrl'),
    calVideoLogo: text('calVideoLogo'),
    appLogo: text('appLogo'),
    appIconLogo: text('appIconLogo'),
    bio: text('bio'),
    hideBranding: boolean('hideBranding').notNull().default(false),
    hideTeamProfileLink: boolean('hideTeamProfileLink').notNull().default(false),
    isPrivate: boolean('isPrivate').notNull().default(false),
    hideBookATeamMember: boolean('hideBookATeamMember').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    metadata: jsonb('metadata'),
    theme: text('theme'),
    rrResetInterval: rrresetIntervalEnum('rrResetInterval').default('MONTH'),
    rrTimestampBasis: rrtimestampBasisEnum('rrTimestampBasis').notNull().default('CREATED_AT'),
    brandColor: text('brandColor'),
    darkBrandColor: text('darkBrandColor'),
    bannerUrl: text('bannerUrl'),
    parentId: integer('parentId'),
    timeFormat: integer('timeFormat'),
    timeZone: text('timeZone').notNull().default('Europe/London'),
    weekStart: text('weekStart').notNull().default('Sunday'),
    isOrganization: boolean('isOrganization').notNull().default(false),
    pendingPayment: boolean('pendingPayment').notNull().default(false),
    isPlatform: boolean('isPlatform').notNull().default(false),
    createdByOAuthClientId: text('createdByOAuthClientId'),
    smsLockState: smslockStateEnum('smsLockState').notNull().default('UNLOCKED'),
    smsLockReviewedByAdmin: boolean('smsLockReviewedByAdmin').notNull().default(false),
    bookingLimits: jsonb('bookingLimits'),
    includeManagedEventsInLimits: boolean('includeManagedEventsInLimits').notNull().default(false),
    autoOptInFeatures: boolean('autoOptInFeatures').notNull().default(false),
  },
  (table): PgTableExtraConfigValue[] => [
    index('Team_parentId_idx').on(table.parentId),
    uniqueIndex('Team_slug_parentId_key').on(table.slug, table.parentId),
    foreignKey({
      name: 'Team_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Team_createdByOAuthClientId_fkey',
      columns: [table.createdByOAuthClientId],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creditBalance = pgTable(
  'CreditBalance',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId'),
    userId: integer('userId'),
    additionalCredits: integer('additionalCredits').notNull().default(0),
    limitReachedAt: timestamp('limitReachedAt', { precision: 3 }),
    warningSentAt: timestamp('warningSentAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('CreditBalance_teamId_key').on(table.teamId),
    uniqueIndex('CreditBalance_userId_key').on(table.userId),
    foreignKey({
      name: 'CreditBalance_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CreditBalance_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creditPurchaseLog = pgTable(
  'CreditPurchaseLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    creditBalanceId: text('creditBalanceId').notNull(),
    credits: integer('credits').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'CreditPurchaseLog_creditBalanceId_fkey',
      columns: [table.creditBalanceId],
      foreignColumns: [creditBalance.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creditExpenseLog = pgTable(
  'CreditExpenseLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    creditBalanceId: text('creditBalanceId').notNull(),
    bookingUid: text('bookingUid'),
    credits: integer('credits'),
    creditType: creditTypeEnum('creditType').notNull(),
    date: timestamp('date', { precision: 3 }).notNull(),
    smsSid: text('smsSid'),
    smsSegments: integer('smsSegments'),
    phoneNumber: text('phoneNumber'),
    email: text('email'),
    callDuration: integer('callDuration'),
    creditFor: creditUsageTypeEnum('creditFor'),
    externalRef: text('externalRef'),
  },
  (table) => [
    uniqueIndex('CreditExpenseLog_externalRef_key').on(table.externalRef),
    foreignKey({
      name: 'CreditExpenseLog_creditBalanceId_fkey',
      columns: [table.creditBalanceId],
      foreignColumns: [creditBalance.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CreditExpenseLog_bookingUid_fkey',
      columns: [table.bookingUid],
      foreignColumns: [booking.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organizationSettings = pgTable(
  'OrganizationSettings',
  {
    id: serial('id').primaryKey(),
    organizationId: integer('organizationId').notNull(),
    isOrganizationConfigured: boolean('isOrganizationConfigured').notNull().default(false),
    isOrganizationVerified: boolean('isOrganizationVerified').notNull().default(false),
    orgAutoAcceptEmail: text('orgAutoAcceptEmail').notNull(),
    lockEventTypeCreationForUsers: boolean('lockEventTypeCreationForUsers')
      .notNull()
      .default(false),
    adminGetsNoSlotsNotification: boolean('adminGetsNoSlotsNotification').notNull().default(false),
    isAdminReviewed: boolean('isAdminReviewed').notNull().default(false),
    isAdminAPIEnabled: boolean('isAdminAPIEnabled').notNull().default(false),
    allowSEOIndexing: boolean('allowSEOIndexing').notNull().default(false),
    orgProfileRedirectsToVerifiedDomain: boolean('orgProfileRedirectsToVerifiedDomain')
      .notNull()
      .default(false),
    disablePhoneOnlySMSNotifications: boolean('disablePhoneOnlySMSNotifications')
      .notNull()
      .default(false),
    disableAutofillOnBookingPage: boolean('disableAutofillOnBookingPage').notNull().default(false),
    orgAutoJoinOnSignup: boolean('orgAutoJoinOnSignup').notNull().default(true),
    disableAttendeeConfirmationEmail: boolean('disableAttendeeConfirmationEmail')
      .notNull()
      .default(false),
    disableAttendeeCancellationEmail: boolean('disableAttendeeCancellationEmail')
      .notNull()
      .default(false),
    disableAttendeeRescheduledEmail: boolean('disableAttendeeRescheduledEmail')
      .notNull()
      .default(false),
    disableAttendeeRequestEmail: boolean('disableAttendeeRequestEmail').notNull().default(false),
    disableAttendeeReassignedEmail: boolean('disableAttendeeReassignedEmail')
      .notNull()
      .default(false),
    disableAttendeeAwaitingPaymentEmail: boolean('disableAttendeeAwaitingPaymentEmail')
      .notNull()
      .default(false),
    disableAttendeeRescheduleRequestEmail: boolean('disableAttendeeRescheduleRequestEmail')
      .notNull()
      .default(false),
    disableAttendeeLocationChangeEmail: boolean('disableAttendeeLocationChangeEmail')
      .notNull()
      .default(false),
    disableAttendeeNewEventEmail: boolean('disableAttendeeNewEventEmail').notNull().default(false),
  },
  (table) => [
    unique('OrganizationSettings_organizationId_key').on(table.organizationId),
    foreignKey({
      name: 'OrganizationSettings_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const membership = pgTable(
  'Membership',
  {
    id: serial('id').primaryKey(),
    teamId: integer('teamId').notNull(),
    userId: integer('userId').notNull(),
    accepted: boolean('accepted').notNull().default(false),
    role: membershipRoleEnum('role').notNull(),
    customRoleId: text('customRoleId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('Membership_teamId_idx').on(table.teamId),
    index('Membership_userId_idx').on(table.userId),
    index('Membership_accepted_idx').on(table.accepted),
    index('Membership_role_idx').on(table.role),
    index('Membership_customRoleId_idx').on(table.customRoleId),
    uniqueIndex('Membership_userId_teamId_key').on(table.userId, table.teamId),
    foreignKey({
      name: 'Membership_customRoleId_fkey',
      columns: [table.customRoleId],
      foreignColumns: [role.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Membership_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Membership_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    id: serial('id').primaryKey(),
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    expiresInDays: integer('expiresInDays'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    teamId: integer('teamId'),
    secondaryEmailId: integer('secondaryEmailId'),
  },
  (table) => [
    uniqueIndex('VerificationToken_token_key').on(table.token),
    index('VerificationToken_token_idx').on(table.token),
    index('VerificationToken_teamId_idx').on(table.teamId),
    index('VerificationToken_secondaryEmailId_idx').on(table.secondaryEmailId),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
    foreignKey({
      name: 'VerificationToken_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'VerificationToken_secondaryEmailId_fkey',
      columns: [table.secondaryEmailId],
      foreignColumns: [secondaryEmail.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const instantMeetingToken = pgTable(
  'InstantMeetingToken',
  {
    id: serial('id').primaryKey(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    teamId: integer('teamId').notNull(),
    bookingId: integer('bookingId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('InstantMeetingToken_token_key').on(table.token),
    uniqueIndex('InstantMeetingToken_bookingId_key').on(table.bookingId),
    index('InstantMeetingToken_token_idx').on(table.token),
    foreignKey({
      name: 'InstantMeetingToken_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'InstantMeetingToken_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingReference = pgTable(
  'BookingReference',
  {
    id: serial('id').primaryKey(),
    type: text('type').notNull(),
    uid: text('uid').notNull(),
    meetingId: text('meetingId'),
    thirdPartyRecurringEventId: text('thirdPartyRecurringEventId'),
    meetingPassword: text('meetingPassword'),
    meetingUrl: text('meetingUrl'),
    bookingId: integer('bookingId'),
    externalCalendarId: text('externalCalendarId'),
    deleted: boolean('deleted'),
    credentialId: integer('credentialId'),
    delegationCredentialId: text('delegationCredentialId'),
  },
  (table) => [
    index('BookingReference_bookingId_idx').on(table.bookingId),
    index('BookingReference_type_idx').on(table.type),
    index('BookingReference_uid_idx').on(table.uid),
    foreignKey({
      name: 'BookingReference_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReference_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReference_delegationCredentialId_fkey',
      columns: [table.delegationCredentialId],
      foreignColumns: [delegationCredential.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const attendee = pgTable(
  'Attendee',
  {
    id: serial('id').primaryKey(),
    email: text('email').notNull(),
    name: text('name').notNull(),
    timeZone: text('timeZone').notNull(),
    phoneNumber: text('phoneNumber'),
    locale: text('locale').default('en'),
    bookingId: integer('bookingId'),
    noShow: boolean('noShow').default(false),
  },
  (table) => [
    index('Attendee_email_idx').on(table.email),
    index('Attendee_bookingId_idx').on(table.bookingId),
    index('Attendee_email_bookingId_idx').on(table.email, table.bookingId),
    foreignKey({
      name: 'Attendee_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const booking = pgTable(
  'Booking',
  {
    id: serial('id').primaryKey(),
    uid: text('uid').notNull(),
    idempotencyKey: text('idempotencyKey'),
    userId: integer('userId'),
    userPrimaryEmail: text('userPrimaryEmail'),
    eventTypeId: integer('eventTypeId'),
    title: text('title').notNull(),
    description: text('description'),
    customInputs: jsonb('customInputs'),
    responses: jsonb('responses'),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    location: text('location'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
    status: bookingStatusEnum('status').notNull().default('accepted'),
    paid: boolean('paid').notNull().default(false),
    destinationCalendarId: integer('destinationCalendarId'),
    cancellationReason: text('cancellationReason'),
    rejectionReason: text('rejectionReason'),
    reassignReason: text('reassignReason'),
    reassignById: integer('reassignById'),
    dynamicEventSlugRef: text('dynamicEventSlugRef'),
    dynamicGroupSlugRef: text('dynamicGroupSlugRef'),
    rescheduled: boolean('rescheduled'),
    fromReschedule: text('fromReschedule'),
    recurringEventId: text('recurringEventId'),
    smsReminderNumber: text('smsReminderNumber'),
    scheduledJobs: text('scheduledJobs').array(),
    metadata: jsonb('metadata'),
    isRecorded: boolean('isRecorded').notNull().default(false),
    iCalUID: text('iCalUID').default(''),
    iCalSequence: integer('iCalSequence').notNull().default(0),
    rating: integer('rating'),
    ratingFeedback: text('ratingFeedback'),
    noShowHost: boolean('noShowHost').default(false),
    oneTimePassword: text('oneTimePassword').$defaultFn(() => crypto.randomUUID()),
    cancelledBy: text('cancelledBy'),
    rescheduledBy: text('rescheduledBy'),
    creationSource: creationSourceEnum('creationSource'),
  },
  (table) => [
    unique('Booking_uid_key').on(table.uid),
    uniqueIndex('Booking_idempotencyKey_key').on(table.idempotencyKey),
    uniqueIndex('Booking_oneTimePassword_key').on(table.oneTimePassword),
    index('Booking_eventTypeId_idx').on(table.eventTypeId),
    index('Booking_userId_idx').on(table.userId),
    index('Booking_destinationCalendarId_idx').on(table.destinationCalendarId),
    index('Booking_recurringEventId_idx').on(table.recurringEventId),
    index('Booking_uid_idx').on(table.uid),
    index('Booking_status_idx').on(table.status),
    index('Booking_startTime_endTime_status_idx').on(table.startTime, table.endTime, table.status),
    index('Booking_fromReschedule_idx').on(table.fromReschedule),
    index('Booking_userId_endTime_idx').on(table.userId, table.endTime),
    index('Booking_userId_status_startTime_idx').on(table.userId, table.status, table.startTime),
    index('Booking_eventTypeId_status_idx').on(table.eventTypeId, table.status),
    index('Booking_userId_createdAt_idx').on(table.userId, table.createdAt),
    foreignKey({ name: 'Booking_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Booking_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Booking_destinationCalendarId_fkey',
      columns: [table.destinationCalendarId],
      foreignColumns: [destinationCalendar.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Booking_reassignById_fkey',
      columns: [table.reassignById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const tracking = pgTable(
  'Tracking',
  {
    id: serial('id').primaryKey(),
    bookingId: integer('bookingId').notNull(),
    utm_source: text('utm_source'),
    utm_medium: text('utm_medium'),
    utm_campaign: text('utm_campaign'),
    utm_term: text('utm_term'),
    utm_content: text('utm_content'),
  },
  (table) => [
    uniqueIndex('Tracking_bookingId_key').on(table.bookingId),
    foreignKey({
      name: 'Tracking_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const schedule = pgTable(
  'Schedule',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    name: text('name').notNull(),
    timeZone: text('timeZone'),
  },
  (table) => [
    index('Schedule_userId_idx').on(table.userId),
    foreignKey({
      name: 'Schedule_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const availability = pgTable(
  'Availability',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId'),
    eventTypeId: integer('eventTypeId'),
    days: integer('days').array(),
    startTime: utcTime('startTime').notNull(),
    endTime: utcTime('endTime').notNull(),
    date: utcDate('date'),
    scheduleId: integer('scheduleId'),
  },
  (table) => [
    index('Availability_userId_idx').on(table.userId),
    index('Availability_eventTypeId_idx').on(table.eventTypeId),
    index('Availability_scheduleId_idx').on(table.scheduleId),
    foreignKey({
      name: 'Availability_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Availability_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Availability_scheduleId_fkey',
      columns: [table.scheduleId],
      foreignColumns: [schedule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const selectedCalendar = pgTable(
  'SelectedCalendar',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: integer('userId').notNull(),
    integration: text('integration').notNull(),
    externalId: text('externalId').notNull(),
    credentialId: integer('credentialId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
    googleChannelId: text('googleChannelId'),
    googleChannelKind: text('googleChannelKind'),
    googleChannelResourceId: text('googleChannelResourceId'),
    googleChannelResourceUri: text('googleChannelResourceUri'),
    googleChannelExpiration: text('googleChannelExpiration'),
    channelId: text('channelId'),
    channelKind: text('channelKind'),
    channelResourceId: text('channelResourceId'),
    channelResourceUri: text('channelResourceUri'),
    channelExpiration: timestamp('channelExpiration', { precision: 3 }),
    syncSubscribedAt: timestamp('syncSubscribedAt', { precision: 3 }),
    syncSubscribedErrorAt: timestamp('syncSubscribedErrorAt', { precision: 3 }),
    syncSubscribedErrorCount: integer('syncSubscribedErrorCount').notNull().default(0),
    syncToken: text('syncToken'),
    syncedAt: timestamp('syncedAt', { precision: 3 }),
    syncErrorAt: timestamp('syncErrorAt', { precision: 3 }),
    syncErrorCount: integer('syncErrorCount').default(0),
    delegationCredentialId: text('delegationCredentialId'),
    error: text('error'),
    lastErrorAt: timestamp('lastErrorAt', { precision: 3 }),
    watchAttempts: integer('watchAttempts').notNull().default(0),
    unwatchAttempts: integer('unwatchAttempts').notNull().default(0),
    maxAttempts: integer('maxAttempts').notNull().default(3),
    eventTypeId: integer('eventTypeId'),
  },
  (table) => [
    index('SelectedCalendar_userId_idx').on(table.userId),
    index('SelectedCalendar_externalId_idx').on(table.externalId),
    index('SelectedCalendar_eventTypeId_idx').on(table.eventTypeId),
    index('SelectedCalendar_credentialId_idx').on(table.credentialId),
    index('SelectedCalendar_channelId_idx').on(table.channelId),
    index('SelectedCalendar_watch_idx').on(
      table.integration,
      table.googleChannelExpiration,
      table.error,
      table.watchAttempts,
      table.maxAttempts,
    ),
    index('SelectedCalendar_unwatch_idx').on(
      table.integration,
      table.googleChannelExpiration,
      table.error,
      table.unwatchAttempts,
      table.maxAttempts,
    ),
    uniqueIndex('SelectedCalendar_userId_integration_externalId_eventTypeId_key').on(
      table.userId,
      table.integration,
      table.externalId,
      table.eventTypeId,
    ),
    uniqueIndex('SelectedCalendar_googleChannelId_eventTypeId_key').on(
      table.googleChannelId,
      table.eventTypeId,
    ),
    foreignKey({
      name: 'SelectedCalendar_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SelectedCalendar_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SelectedCalendar_delegationCredentialId_fkey',
      columns: [table.delegationCredentialId],
      foreignColumns: [delegationCredential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SelectedCalendar_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const eventTypeCustomInput = pgTable(
  'EventTypeCustomInput',
  {
    id: serial('id').primaryKey(),
    eventTypeId: integer('eventTypeId').notNull(),
    label: text('label').notNull(),
    type: eventTypeCustomInputTypeEnum('type').notNull(),
    options: jsonb('options'),
    required: boolean('required').notNull(),
    placeholder: text('placeholder').notNull().default(''),
  },
  (table) => [
    index('EventTypeCustomInput_eventTypeId_idx').on(table.eventTypeId),
    foreignKey({
      name: 'EventTypeCustomInput_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const resetPasswordRequest = pgTable('ResetPasswordRequest', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  email: text('email').notNull(),
  expires: timestamp('expires', { precision: 3 }).notNull(),
})

export const reminderMail = pgTable(
  'ReminderMail',
  {
    id: serial('id').primaryKey(),
    referenceId: integer('referenceId').notNull(),
    reminderType: reminderTypeEnum('reminderType').notNull(),
    elapsedMinutes: integer('elapsedMinutes').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ReminderMail_referenceId_idx').on(table.referenceId),
    index('ReminderMail_reminderType_idx').on(table.reminderType),
  ],
)

export const payment = pgTable(
  'Payment',
  {
    id: serial('id').primaryKey(),
    uid: text('uid').notNull(),
    appId: text('appId'),
    bookingId: integer('bookingId').notNull(),
    amount: integer('amount').notNull(),
    fee: integer('fee').notNull(),
    currency: text('currency').notNull(),
    success: boolean('success').notNull(),
    refunded: boolean('refunded').notNull(),
    data: jsonb('data').notNull(),
    externalId: text('externalId').notNull(),
    paymentOption: paymentOptionEnum('paymentOption').default('ON_BOOKING'),
  },
  (table) => [
    uniqueIndex('Payment_uid_key').on(table.uid),
    uniqueIndex('Payment_externalId_key').on(table.externalId),
    index('Payment_bookingId_idx').on(table.bookingId),
    index('Payment_externalId_idx').on(table.externalId),
    foreignKey({ name: 'Payment_appId_fkey', columns: [table.appId], foreignColumns: [app.slug] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Payment_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const webhook = pgTable(
  'Webhook',
  {
    id: text('id').primaryKey(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    eventTypeId: integer('eventTypeId'),
    platformOAuthClientId: text('platformOAuthClientId'),
    subscriberUrl: text('subscriberUrl').notNull(),
    payloadTemplate: text('payloadTemplate'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    active: boolean('active').notNull().default(true),
    eventTriggers: webhookTriggerEventsEnum('eventTriggers').array(),
    appId: text('appId'),
    secret: text('secret'),
    platform: boolean('platform').notNull().default(false),
    time: integer('time'),
    timeUnit: timeUnitEnum('timeUnit'),
    version: text('version').notNull().default('2021-10-20'),
  },
  (table) => [
    unique('Webhook_id_key').on(table.id),
    index('Webhook_active_idx').on(table.active),
    uniqueIndex('Webhook_userId_subscriberUrl_key').on(table.userId, table.subscriberUrl),
    uniqueIndex('Webhook_platformOAuthClientId_subscriberUrl_key').on(
      table.platformOAuthClientId,
      table.subscriberUrl,
    ),
    foreignKey({ name: 'Webhook_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Webhook_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Webhook_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Webhook_platformOAuthClientId_fkey',
      columns: [table.platformOAuthClientId],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Webhook_appId_fkey', columns: [table.appId], foreignColumns: [app.slug] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiKey = pgTable(
  'ApiKey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: integer('userId').notNull(),
    teamId: integer('teamId'),
    note: text('note'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    hashedKey: text('hashedKey').notNull(),
    appId: text('appId'),
  },
  (table) => [
    unique('ApiKey_id_key').on(table.id),
    uniqueIndex('ApiKey_hashedKey_key').on(table.hashedKey),
    index('ApiKey_userId_idx').on(table.userId),
    foreignKey({ name: 'ApiKey_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'ApiKey_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'ApiKey_appId_fkey', columns: [table.appId], foreignColumns: [app.slug] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const rateLimit = pgTable(
  'RateLimit',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    apiKeyId: text('apiKeyId').notNull(),
    ttl: integer('ttl').notNull(),
    limit: integer('limit').notNull(),
    blockDuration: integer('blockDuration').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('RateLimit_apiKeyId_idx').on(table.apiKeyId),
    foreignKey({
      name: 'RateLimit_apiKeyId_fkey',
      columns: [table.apiKeyId],
      foreignColumns: [apiKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const hashedLink = pgTable(
  'HashedLink',
  {
    id: serial('id').primaryKey(),
    link: text('link').notNull(),
    eventTypeId: integer('eventTypeId').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    maxUsageCount: integer('maxUsageCount').notNull().default(1),
    usageCount: integer('usageCount').notNull().default(0),
  },
  (table) => [
    uniqueIndex('HashedLink_link_key').on(table.link),
    index('HashedLink_eventTypeId_idx').on(table.eventTypeId),
    foreignKey({
      name: 'HashedLink_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
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
    userId: integer('userId').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    providerEmail: text('providerEmail'),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
  },
  (table) => [
    index('Account_userId_idx').on(table.userId),
    index('Account_type_idx').on(table.type),
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
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
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const app = pgTable(
  'App',
  {
    slug: text('slug').primaryKey(),
    dirName: text('dirName').notNull(),
    keys: jsonb('keys'),
    categories: appCategoriesEnum('categories').array(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    enabled: boolean('enabled').notNull().default(false),
  },
  (table) => [
    unique('App_slug_key').on(table.slug),
    uniqueIndex('App_dirName_key').on(table.dirName),
    index('App_enabled_idx').on(table.enabled),
  ],
)

export const feedback = pgTable(
  'Feedback',
  {
    id: serial('id').primaryKey(),
    date: timestamp('date', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: integer('userId').notNull(),
    rating: text('rating').notNull(),
    comment: text('comment'),
  },
  (table) => [
    index('Feedback_userId_idx').on(table.userId),
    index('Feedback_rating_idx').on(table.rating),
    foreignKey({
      name: 'Feedback_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const deployment = pgTable('Deployment', {
  id: integer('id').primaryKey().default(1),
  logo: text('logo'),
  theme: jsonb('theme'),
  licenseKey: text('licenseKey'),
  signatureTokenEncrypted: text('signatureTokenEncrypted'),
  agreedLicenseAt: timestamp('agreedLicenseAt', { precision: 3 }),
})

export const webhookScheduledTriggers = pgTable(
  'WebhookScheduledTriggers',
  {
    id: serial('id').primaryKey(),
    jobName: text('jobName'),
    subscriberUrl: text('subscriberUrl').notNull(),
    payload: text('payload').notNull(),
    startAfter: timestamp('startAfter', { precision: 3 }).notNull(),
    retryCount: integer('retryCount').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    appId: text('appId'),
    webhookId: text('webhookId'),
    bookingId: integer('bookingId'),
  },
  (table) => [
    foreignKey({
      name: 'WebhookScheduledTriggers_webhookId_fkey',
      columns: [table.webhookId],
      foreignColumns: [webhook.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WebhookScheduledTriggers_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingSeat = pgTable(
  'BookingSeat',
  {
    id: serial('id').primaryKey(),
    referenceUid: text('referenceUid').notNull(),
    bookingId: integer('bookingId').notNull(),
    attendeeId: integer('attendeeId').notNull(),
    data: jsonb('data'),
    metadata: jsonb('metadata'),
  },
  (table) => [
    uniqueIndex('BookingSeat_referenceUid_key').on(table.referenceUid),
    uniqueIndex('BookingSeat_attendeeId_key').on(table.attendeeId),
    index('BookingSeat_bookingId_idx').on(table.bookingId),
    index('BookingSeat_attendeeId_idx').on(table.attendeeId),
    foreignKey({
      name: 'BookingSeat_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingSeat_attendeeId_fkey',
      columns: [table.attendeeId],
      foreignColumns: [attendee.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verifiedNumber = pgTable(
  'VerifiedNumber',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    phoneNumber: text('phoneNumber').notNull(),
  },
  (table) => [
    index('VerifiedNumber_userId_idx').on(table.userId),
    index('VerifiedNumber_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'VerifiedNumber_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'VerifiedNumber_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verifiedEmail = pgTable(
  'VerifiedEmail',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    email: text('email').notNull(),
  },
  (table) => [
    index('VerifiedEmail_userId_idx').on(table.userId),
    index('VerifiedEmail_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'VerifiedEmail_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'VerifiedEmail_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feature = pgTable(
  'Feature',
  {
    slug: text('slug').primaryKey(),
    enabled: boolean('enabled').notNull().default(false),
    description: text('description'),
    type: featureTypeEnum('type').default('RELEASE'),
    stale: boolean('stale').default(false),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updatedBy: integer('updatedBy'),
  },
  (table) => [
    unique('Feature_slug_key').on(table.slug),
    index('Feature_enabled_idx').on(table.enabled),
    index('Feature_stale_idx').on(table.stale),
  ],
)

export const userFeatures = pgTable(
  'UserFeatures',
  {
    userId: integer('userId').notNull(),
    featureId: text('featureId').notNull(),
    enabled: boolean('enabled').notNull(),
    assignedAt: timestamp('assignedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    assignedBy: text('assignedBy').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'UserFeatures_pkey', columns: [table.userId, table.featureId] }),
    index('UserFeatures_userId_featureId_idx').on(table.userId, table.featureId),
    foreignKey({
      name: 'UserFeatures_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserFeatures_featureId_fkey',
      columns: [table.featureId],
      foreignColumns: [feature.slug],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamFeatures = pgTable(
  'TeamFeatures',
  {
    teamId: integer('teamId').notNull(),
    featureId: text('featureId').notNull(),
    enabled: boolean('enabled').notNull(),
    assignedAt: timestamp('assignedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    assignedBy: text('assignedBy').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'TeamFeatures_pkey', columns: [table.teamId, table.featureId] }),
    index('TeamFeatures_teamId_featureId_idx').on(table.teamId, table.featureId),
    foreignKey({
      name: 'TeamFeatures_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TeamFeatures_featureId_fkey',
      columns: [table.featureId],
      foreignColumns: [feature.slug],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const selectedSlots = pgTable(
  'SelectedSlots',
  {
    id: serial('id').primaryKey(),
    eventTypeId: integer('eventTypeId').notNull(),
    userId: integer('userId').notNull(),
    slotUtcStartDate: timestamp('slotUtcStartDate', { precision: 3 }).notNull(),
    slotUtcEndDate: timestamp('slotUtcEndDate', { precision: 3 }).notNull(),
    uid: text('uid').notNull(),
    releaseAt: timestamp('releaseAt', { precision: 3 }).notNull(),
    isSeat: boolean('isSeat').notNull().default(false),
  },
  (table) => [
    uniqueIndex('SelectedSlots_userId_slotUtcStartDate_slotUtcEndDate_uid_key').on(
      table.userId,
      table.slotUtcStartDate,
      table.slotUtcEndDate,
      table.uid,
    ),
  ],
)

export const oAuthClient = pgTable(
  'OAuthClient',
  {
    clientId: text('clientId').primaryKey(),
    redirectUri: text('redirectUri').notNull(),
    clientSecret: text('clientSecret'),
    clientType: oauthClientTypeEnum('clientType').notNull().default('confidential'),
    name: text('name').notNull(),
    purpose: text('purpose'),
    logo: text('logo'),
    websiteUrl: text('websiteUrl'),
    rejectionReason: text('rejectionReason'),
    isTrusted: boolean('isTrusted').notNull().default(false),
    status: oauthClientStatusEnum('status').notNull().default('approved'),
    userId: integer('userId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    unique('OAuthClient_clientId_key').on(table.clientId),
    index('OAuthClient_userId_idx').on(table.userId),
    foreignKey({
      name: 'OAuthClient_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const accessCode = pgTable(
  'AccessCode',
  {
    id: serial('id').primaryKey(),
    code: text('code').notNull(),
    clientId: text('clientId'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    scopes: accessScopeEnum('scopes').array(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    codeChallenge: text('codeChallenge'),
    codeChallengeMethod: text('codeChallengeMethod').default('S256'),
  },
  (table) => [
    foreignKey({
      name: 'AccessCode_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oAuthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AccessCode_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AccessCode_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingDenormalized = pgTable(
  'BookingDenormalized',
  {
    id: integer('id').primaryKey(),
    uid: text('uid').notNull(),
    eventTypeId: integer('eventTypeId'),
    title: text('title').notNull(),
    description: text('description'),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    location: text('location'),
    paid: boolean('paid').notNull(),
    status: bookingStatusEnum('status').notNull(),
    rescheduled: boolean('rescheduled'),
    userId: integer('userId'),
    teamId: integer('teamId'),
    eventLength: integer('eventLength'),
    eventParentId: integer('eventParentId'),
    userEmail: text('userEmail'),
    userName: text('userName'),
    userUsername: text('userUsername'),
    ratingFeedback: text('ratingFeedback'),
    rating: integer('rating'),
    noShowHost: boolean('noShowHost'),
    isTeamBooking: boolean('isTeamBooking').notNull(),
  },
  (table) => [
    uniqueIndex('BookingDenormalized_id_key').on(table.id),
    index('BookingDenormalized_userId_idx').on(table.userId),
    index('BookingDenormalized_createdAt_idx').on(table.createdAt),
    index('BookingDenormalized_eventTypeId_idx').on(table.eventTypeId),
    index('BookingDenormalized_eventParentId_idx').on(table.eventParentId),
    index('BookingDenormalized_teamId_idx').on(table.teamId),
    index('BookingDenormalized_startTime_idx').on(table.startTime),
    index('BookingDenormalized_endTime_idx').on(table.endTime),
    index('BookingDenormalized_status_idx').on(table.status),
    index('BookingDenormalized_teamId_isTeamBooking_idx').on(table.teamId, table.isTeamBooking),
    index('BookingDenormalized_userId_isTeamBooking_idx').on(table.userId, table.isTeamBooking),
    index('BookingDenormalized_startTime_endTime_idx').on(table.startTime, table.endTime),
  ],
)

export const calendarCache = pgTable(
  'CalendarCache',
  {
    id: text('id').$defaultFn(() => crypto.randomUUID()),
    key: text('key').notNull(),
    value: jsonb('value').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    credentialId: integer('credentialId').notNull(),
    userId: integer('userId'),
  },
  (table) => [
    primaryKey({ name: 'CalendarCache_pkey', columns: [table.credentialId, table.key] }),
    index('CalendarCache_userId_key_idx').on(table.userId, table.key),
    uniqueIndex('CalendarCache_credentialId_key_key').on(table.credentialId, table.key),
    foreignKey({
      name: 'CalendarCache_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tempOrgRedirect = pgTable(
  'TempOrgRedirect',
  {
    id: serial('id').primaryKey(),
    from: text('from').notNull(),
    fromOrgId: integer('fromOrgId').notNull(),
    type: redirectTypeEnum('type').notNull(),
    toUrl: text('toUrl').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('TempOrgRedirect_from_type_fromOrgId_key').on(
      table.from,
      table.type,
      table.fromOrgId,
    ),
  ],
)

export const avatars = pgTable(
  'avatars',
  {
    teamId: integer('teamId').notNull().default(0),
    userId: integer('userId').notNull().default(0),
    data: text('data').notNull(),
    objectKey: text('objectKey').notNull(),
    isBanner: boolean('isBanner').notNull().default(false),
  },
  (table) => [
    uniqueIndex('avatars_objectKey_key').on(table.objectKey),
    uniqueIndex('avatars_teamId_userId_isBanner_key').on(
      table.teamId,
      table.userId,
      table.isBanner,
    ),
  ],
)

export const outOfOfficeEntry = pgTable(
  'OutOfOfficeEntry',
  {
    id: serial('id').primaryKey(),
    uuid: text('uuid').notNull(),
    start: timestamp('start', { precision: 3 }).notNull(),
    end: timestamp('end', { precision: 3 }).notNull(),
    notes: text('notes'),
    showNotePublicly: boolean('showNotePublicly').notNull().default(false),
    userId: integer('userId').notNull(),
    toUserId: integer('toUserId'),
    reasonId: integer('reasonId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('OutOfOfficeEntry_uuid_key').on(table.uuid),
    index('OutOfOfficeEntry_uuid_idx').on(table.uuid),
    index('OutOfOfficeEntry_userId_idx').on(table.userId),
    index('OutOfOfficeEntry_toUserId_idx').on(table.toUserId),
    index('OutOfOfficeEntry_start_end_idx').on(table.start, table.end),
    foreignKey({
      name: 'OutOfOfficeEntry_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OutOfOfficeEntry_toUserId_fkey',
      columns: [table.toUserId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OutOfOfficeEntry_reasonId_fkey',
      columns: [table.reasonId],
      foreignColumns: [outOfOfficeReason.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const outOfOfficeReason = pgTable(
  'OutOfOfficeReason',
  {
    id: serial('id').primaryKey(),
    emoji: text('emoji').notNull(),
    reason: text('reason').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    userId: integer('userId'),
  },
  (table) => [
    uniqueIndex('OutOfOfficeReason_reason_key').on(table.reason),
    foreignKey({
      name: 'OutOfOfficeReason_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userHolidaySettings = pgTable(
  'UserHolidaySettings',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    countryCode: text('countryCode'),
    disabledIds: text('disabledIds').array(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('UserHolidaySettings_userId_key').on(table.userId),
    foreignKey({
      name: 'UserHolidaySettings_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const holidayCache = pgTable(
  'HolidayCache',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    countryCode: text('countryCode').notNull(),
    calendarId: text('calendarId').notNull(),
    eventId: text('eventId').notNull(),
    name: text('name').notNull(),
    date: timestamp('date', { precision: 3 }).notNull(),
    year: integer('year').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('HolidayCache_countryCode_year_idx').on(table.countryCode, table.year),
    index('HolidayCache_countryCode_date_idx').on(table.countryCode, table.date),
    uniqueIndex('HolidayCache_countryCode_eventId_key').on(table.countryCode, table.eventId),
  ],
)

export const platformOAuthClient = pgTable(
  'PlatformOAuthClient',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    secret: text('secret').notNull(),
    permissions: integer('permissions').notNull(),
    logo: text('logo'),
    redirectUris: text('redirectUris').array(),
    organizationId: integer('organizationId').notNull(),
    bookingRedirectUri: text('bookingRedirectUri'),
    bookingCancelRedirectUri: text('bookingCancelRedirectUri'),
    bookingRescheduleRedirectUri: text('bookingRescheduleRedirectUri'),
    areEmailsEnabled: boolean('areEmailsEnabled').notNull().default(false),
    areDefaultEventTypesEnabled: boolean('areDefaultEventTypesEnabled').notNull().default(true),
    areCalendarEventsEnabled: boolean('areCalendarEventsEnabled').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table): PgTableExtraConfigValue[] => [
    foreignKey({
      name: 'PlatformOAuthClient_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const platformAuthorizationToken = pgTable(
  'PlatformAuthorizationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    platformOAuthClientId: text('platformOAuthClientId').notNull(),
    userId: integer('userId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('PlatformAuthorizationToken_userId_platformOAuthClientId_key').on(
      table.userId,
      table.platformOAuthClientId,
    ),
    foreignKey({
      name: 'PlatformAuthorizationToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PlatformAuthorizationToken_platformOAuthClientId_fkey',
      columns: [table.platformOAuthClientId],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const accessToken = pgTable(
  'AccessToken',
  {
    id: serial('id').primaryKey(),
    secret: text('secret').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    platformOAuthClientId: text('platformOAuthClientId').notNull(),
    userId: integer('userId').notNull(),
  },
  (table) => [
    uniqueIndex('AccessToken_secret_key').on(table.secret),
    foreignKey({
      name: 'AccessToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AccessToken_platformOAuthClientId_fkey',
      columns: [table.platformOAuthClientId],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const refreshToken = pgTable(
  'RefreshToken',
  {
    id: serial('id').primaryKey(),
    secret: text('secret').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    platformOAuthClientId: text('platformOAuthClientId').notNull(),
    userId: integer('userId').notNull(),
  },
  (table) => [
    uniqueIndex('RefreshToken_secret_key').on(table.secret),
    foreignKey({
      name: 'RefreshToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RefreshToken_platformOAuthClientId_fkey',
      columns: [table.platformOAuthClientId],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dSyncData = pgTable(
  'DSyncData',
  {
    id: serial('id').primaryKey(),
    directoryId: text('directoryId').notNull(),
    tenant: text('tenant').notNull(),
    organizationId: integer('organizationId'),
  },
  (table) => [
    unique('DSyncData_directoryId_key').on(table.directoryId),
    uniqueIndex('DSyncData_organizationId_key').on(table.organizationId),
    foreignKey({
      name: 'DSyncData_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organizationSettings.organizationId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dSyncTeamGroupMapping = pgTable(
  'DSyncTeamGroupMapping',
  {
    id: serial('id').primaryKey(),
    organizationId: integer('organizationId').notNull(),
    teamId: integer('teamId').notNull(),
    directoryId: text('directoryId').notNull(),
    groupName: text('groupName').notNull(),
  },
  (table) => [
    uniqueIndex('DSyncTeamGroupMapping_teamId_groupName_key').on(table.teamId, table.groupName),
    foreignKey({
      name: 'DSyncTeamGroupMapping_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DSyncTeamGroupMapping_directoryId_fkey',
      columns: [table.directoryId],
      foreignColumns: [dSyncData.directoryId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const secondaryEmail = pgTable(
  'SecondaryEmail',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    email: text('email').notNull(),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
  },
  (table) => [
    index('SecondaryEmail_userId_idx').on(table.userId),
    index('SecondaryEmail_email_emailVerified_idx').on(table.email, table.emailVerified),
    uniqueIndex('SecondaryEmail_email_key').on(table.email),
    uniqueIndex('SecondaryEmail_userId_email_key').on(table.userId, table.email),
    foreignKey({
      name: 'SecondaryEmail_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const task = pgTable(
  'Task',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    scheduledAt: timestamp('scheduledAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    succeededAt: timestamp('succeededAt', { precision: 3 }),
    type: text('type').notNull(),
    payload: text('payload').notNull(),
    attempts: integer('attempts').notNull().default(0),
    maxAttempts: integer('maxAttempts').notNull().default(3),
    lastError: text('lastError'),
    lastFailedAttemptAt: timestamp('lastFailedAttemptAt', { precision: 3 }),
    referenceUid: text('referenceUid'),
  },
  (table) => [
    uniqueIndex('Task_id_key').on(table.id),
    index('Task_succeededAt_idx').on(table.succeededAt),
    index('Task_scheduledAt_succeededAt_idx').on(table.scheduledAt, table.succeededAt),
    uniqueIndex('Task_referenceUid_type_key').on(table.referenceUid, table.type),
  ],
)

export const managedOrganization = pgTable(
  'ManagedOrganization',
  {
    managedOrganizationId: integer('managedOrganizationId').notNull(),
    managerOrganizationId: integer('managerOrganizationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('ManagedOrganization_managedOrganizationId_key').on(table.managedOrganizationId),
    index('ManagedOrganization_managerOrganizationId_idx').on(table.managerOrganizationId),
    uniqueIndex('ManagedOrganization_managerOrganizationId_managedOrganizati_key').on(
      table.managerOrganizationId,
      table.managedOrganizationId,
    ),
    foreignKey({
      name: 'ManagedOrganization_managedOrganizationId_fkey',
      columns: [table.managedOrganizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ManagedOrganization_managerOrganizationId_fkey',
      columns: [table.managerOrganizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const platformBilling = pgTable(
  'PlatformBilling',
  {
    id: integer('id').primaryKey(),
    customerId: text('customerId').notNull(),
    subscriptionId: text('subscriptionId'),
    priceId: text('priceId'),
    plan: text('plan').notNull().default('none'),
    billingCycleStart: integer('billingCycleStart'),
    billingCycleEnd: integer('billingCycleEnd'),
    overdue: boolean('overdue').default(false),
    managerBillingId: integer('managerBillingId'),
  },
  (table) => [
    unique('PlatformBilling_id_key').on(table.id),
    foreignKey({
      name: 'PlatformBilling_managerBillingId_fkey',
      columns: [table.managerBillingId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'PlatformBilling_id_fkey', columns: [table.id], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attributeOption = pgTable(
  'AttributeOption',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    attributeId: text('attributeId').notNull(),
    value: text('value').notNull(),
    slug: text('slug').notNull(),
    isGroup: boolean('isGroup').notNull().default(false),
    contains: text('contains').array(),
  },
  (table) => [
    foreignKey({
      name: 'AttributeOption_attributeId_fkey',
      columns: [table.attributeId],
      foreignColumns: [attribute.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attribute = pgTable(
  'Attribute',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId').notNull(),
    type: attributeTypeEnum('type').notNull(),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    usersCanEditRelation: boolean('usersCanEditRelation').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    isWeightsEnabled: boolean('isWeightsEnabled').notNull().default(false),
    isLocked: boolean('isLocked').notNull().default(false),
  },
  (table) => [
    index('Attribute_teamId_idx').on(table.teamId),
    uniqueIndex('Attribute_teamId_slug_key').on(table.teamId, table.slug),
    foreignKey({
      name: 'Attribute_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attributeToUser = pgTable(
  'AttributeToUser',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    memberId: integer('memberId').notNull(),
    attributeOptionId: text('attributeOptionId').notNull(),
    weight: integer('weight'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdById: integer('createdById'),
    createdByDSyncId: text('createdByDSyncId'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).$onUpdate(utcNow),
    updatedById: integer('updatedById'),
    updatedByDSyncId: text('updatedByDSyncId'),
  },
  (table) => [
    uniqueIndex('AttributeToUser_memberId_attributeOptionId_key').on(
      table.memberId,
      table.attributeOptionId,
    ),
    foreignKey({
      name: 'AttributeToUser_memberId_fkey',
      columns: [table.memberId],
      foreignColumns: [membership.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeToUser_attributeOptionId_fkey',
      columns: [table.attributeOptionId],
      foreignColumns: [attributeOption.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeToUser_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeToUser_createdByDSyncId_fkey',
      columns: [table.createdByDSyncId],
      foreignColumns: [dSyncData.directoryId],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeToUser_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeToUser_updatedByDSyncId_fkey',
      columns: [table.updatedByDSyncId],
      foreignColumns: [dSyncData.directoryId],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const assignmentReason = pgTable(
  'AssignmentReason',
  {
    id: serial('id').primaryKey(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    bookingId: integer('bookingId').notNull(),
    reasonEnum: assignmentReasonEnumEnum('reasonEnum').notNull(),
    reasonString: text('reasonString').notNull(),
  },
  (table) => [
    uniqueIndex('AssignmentReason_id_key').on(table.id),
    index('AssignmentReason_bookingId_idx').on(table.bookingId),
    foreignKey({
      name: 'AssignmentReason_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const delegationCredential = pgTable(
  'DelegationCredential',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    workspacePlatformId: integer('workspacePlatformId').notNull(),
    serviceAccountKey: jsonb('serviceAccountKey').notNull(),
    enabled: boolean('enabled').notNull().default(false),
    lastEnabledAt: timestamp('lastEnabledAt', { precision: 3 }),
    lastDisabledAt: timestamp('lastDisabledAt', { precision: 3 }),
    organizationId: integer('organizationId').notNull(),
    domain: text('domain').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DelegationCredential_enabled_idx').on(table.enabled),
    uniqueIndex('DelegationCredential_organizationId_domain_key').on(
      table.organizationId,
      table.domain,
    ),
    foreignKey({
      name: 'DelegationCredential_workspacePlatformId_fkey',
      columns: [table.workspacePlatformId],
      foreignColumns: [workspacePlatform.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DelegationCredential_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspacePlatform = pgTable(
  'WorkspacePlatform',
  {
    id: serial('id').primaryKey(),
    slug: text('slug').notNull(),
    name: text('name').notNull(),
    description: text('description').notNull(),
    defaultServiceAccountKey: jsonb('defaultServiceAccountKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    enabled: boolean('enabled').notNull().default(false),
  },
  (table) => [uniqueIndex('WorkspacePlatform_slug_key').on(table.slug)],
)

export const eventTypeTranslation = pgTable(
  'EventTypeTranslation',
  {
    uid: text('uid')
      .primaryKey()
      .$defaultFn(() => cuid()),
    eventTypeId: integer('eventTypeId').notNull(),
    field: eventTypeAutoTranslatedFieldEnum('field').notNull(),
    sourceLocale: text('sourceLocale').notNull(),
    targetLocale: text('targetLocale').notNull(),
    translatedText: text('translatedText').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: integer('createdBy').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    updatedBy: integer('updatedBy'),
  },
  (table) => [
    index('EventTypeTranslation_eventTypeId_field_targetLocale_idx').on(
      table.eventTypeId,
      table.field,
      table.targetLocale,
    ),
    uniqueIndex('EventTypeTranslation_eventTypeId_field_targetLocale_key').on(
      table.eventTypeId,
      table.field,
      table.targetLocale,
    ),
    foreignKey({
      name: 'EventTypeTranslation_eventTypeId_fkey',
      columns: [table.eventTypeId],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventTypeTranslation_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [users.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EventTypeTranslation_updatedBy_fkey',
      columns: [table.updatedBy],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const watchlist = pgTable(
  'Watchlist',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    type: watchlistTypeEnum('type').notNull(),
    value: text('value').notNull(),
    description: text('description'),
    isGlobal: boolean('isGlobal').notNull().default(false),
    organizationId: integer('organizationId'),
    action: watchlistActionEnum('action').notNull().default('REPORT'),
    source: watchlistSourceEnum('source').notNull().default('MANUAL'),
    lastUpdatedAt: timestamp('lastUpdatedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('Watchlist_isGlobal_action_organizationId_type_value_idx').on(
      table.isGlobal,
      table.action,
      table.organizationId,
      table.type,
      table.value,
    ),
    uniqueIndex('Watchlist_type_value_organizationId_key').on(
      table.type,
      table.value,
      table.organizationId,
    ),
  ],
)

export const watchlistAudit = pgTable(
  'WatchlistAudit',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => uuidv7()),
    type: watchlistTypeEnum('type').notNull(),
    value: text('value').notNull(),
    description: text('description'),
    action: watchlistActionEnum('action').notNull().default('REPORT'),
    changedAt: timestamp('changedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    changedByUserId: integer('changedByUserId'),
    watchlistId: uuid('watchlistId'),
  },
  (table) => [
    index('WatchlistAudit_watchlistId_changedAt_idx').on(table.watchlistId, table.changedAt),
    foreignKey({
      name: 'WatchlistAudit_watchlistId_fkey',
      columns: [table.watchlistId],
      foreignColumns: [watchlist.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const watchlistEventAudit = pgTable('WatchlistEventAudit', {
  id: uuid('id')
    .primaryKey()
    .$defaultFn(() => uuidv7()),
  watchlistId: uuid('watchlistId').notNull(),
  eventTypeId: integer('eventTypeId').notNull(),
  actionTaken: watchlistActionEnum('actionTaken').notNull(),
  timestamp: timestamp('timestamp', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const bookingReport = pgTable(
  'BookingReport',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    bookingUid: text('bookingUid').notNull(),
    bookerEmail: text('bookerEmail').notNull(),
    reportedById: integer('reportedById'),
    organizationId: integer('organizationId'),
    reason: bookingReportReasonEnum('reason').notNull(),
    description: text('description'),
    cancelled: boolean('cancelled').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    status: bookingReportStatusEnum('status').notNull().default('PENDING'),
    systemStatus: systemReportStatusEnum('systemStatus').notNull().default('PENDING'),
    watchlistId: uuid('watchlistId'),
    globalWatchlistId: uuid('globalWatchlistId'),
  },
  (table) => [
    uniqueIndex('BookingReport_bookingUid_key').on(table.bookingUid),
    index('BookingReport_bookerEmail_idx').on(table.bookerEmail),
    index('BookingReport_reportedById_idx').on(table.reportedById),
    index('BookingReport_organizationId_idx').on(table.organizationId),
    index('BookingReport_watchlistId_idx').on(table.watchlistId),
    index('BookingReport_globalWatchlistId_idx').on(table.globalWatchlistId),
    index('BookingReport_systemStatus_idx').on(table.systemStatus),
    index('BookingReport_createdAt_idx').on(table.createdAt),
    foreignKey({
      name: 'BookingReport_bookingUid_fkey',
      columns: [table.bookingUid],
      foreignColumns: [booking.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReport_reportedById_fkey',
      columns: [table.reportedById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReport_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReport_watchlistId_fkey',
      columns: [table.watchlistId],
      foreignColumns: [watchlist.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingReport_globalWatchlistId_fkey',
      columns: [table.globalWatchlistId],
      foreignColumns: [watchlist.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const wrongAssignmentReport = pgTable(
  'WrongAssignmentReport',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    bookingUid: text('bookingUid').notNull(),
    reportedById: integer('reportedById'),
    correctAssignee: text('correctAssignee'),
    additionalNotes: text('additionalNotes').notNull(),
    teamId: integer('teamId'),
    status: wrongAssignmentReportStatusEnum('status').notNull().default('PENDING'),
    reviewedById: integer('reviewedById'),
    reviewedAt: timestamp('reviewedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('WrongAssignmentReport_bookingUid_key').on(table.bookingUid),
    index('WrongAssignmentReport_reportedById_idx').on(table.reportedById),
    index('WrongAssignmentReport_teamId_idx').on(table.teamId),
    index('WrongAssignmentReport_status_idx').on(table.status),
    index('WrongAssignmentReport_reviewedById_idx').on(table.reviewedById),
    index('WrongAssignmentReport_createdAt_idx').on(table.createdAt),
    index('WrongAssignmentReport_teamId_status_createdAt_idx').on(
      table.teamId,
      table.status,
      table.createdAt,
    ),
    foreignKey({
      name: 'WrongAssignmentReport_bookingUid_fkey',
      columns: [table.bookingUid],
      foreignColumns: [booking.uid],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WrongAssignmentReport_reportedById_fkey',
      columns: [table.reportedById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WrongAssignmentReport_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WrongAssignmentReport_reviewedById_fkey',
      columns: [table.reviewedById],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const organizationOnboarding = pgTable(
  'OrganizationOnboarding',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdById: integer('createdById').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    orgOwnerEmail: text('orgOwnerEmail').notNull(),
    error: text('error'),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationId: integer('organizationId'),
    billingPeriod: billingPeriodEnum('billingPeriod').notNull(),
    pricePerSeat: doublePrecision('pricePerSeat').notNull(),
    seats: integer('seats').notNull(),
    isPlatform: boolean('isPlatform').notNull().default(false),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    logo: text('logo'),
    bio: text('bio'),
    brandColor: text('brandColor'),
    bannerUrl: text('bannerUrl'),
    isDomainConfigured: boolean('isDomainConfigured').notNull().default(false),
    stripeCustomerId: text('stripeCustomerId'),
    stripeSubscriptionId: text('stripeSubscriptionId'),
    stripeSubscriptionItemId: text('stripeSubscriptionItemId'),
    invitedMembers: jsonb('invitedMembers').notNull().default([]),
    teams: jsonb('teams').notNull().default([]),
    isComplete: boolean('isComplete').notNull().default(false),
  },
  (table) => [
    uniqueIndex('OrganizationOnboarding_orgOwnerEmail_key').on(table.orgOwnerEmail),
    uniqueIndex('OrganizationOnboarding_organizationId_key').on(table.organizationId),
    uniqueIndex('OrganizationOnboarding_stripeCustomerId_key').on(table.stripeCustomerId),
    index('OrganizationOnboarding_orgOwnerEmail_idx').on(table.orgOwnerEmail),
    index('OrganizationOnboarding_stripeCustomerId_idx').on(table.stripeCustomerId),
    foreignKey({
      name: 'OrganizationOnboarding_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OrganizationOnboarding_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const internalNotePreset = pgTable(
  'InternalNotePreset',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    cancellationReason: text('cancellationReason'),
    teamId: integer('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('InternalNotePreset_teamId_idx').on(table.teamId),
    uniqueIndex('InternalNotePreset_teamId_name_key').on(table.teamId, table.name),
    foreignKey({
      name: 'InternalNotePreset_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const filterSegment = pgTable(
  'FilterSegment',
  {
    id: serial('id').primaryKey(),
    name: text('name').notNull(),
    tableIdentifier: text('tableIdentifier').notNull(),
    scope: filterSegmentScopeEnum('scope').notNull(),
    activeFilters: jsonb('activeFilters'),
    sorting: jsonb('sorting'),
    columnVisibility: jsonb('columnVisibility'),
    columnSizing: jsonb('columnSizing'),
    perPage: integer('perPage').notNull(),
    searchTerm: text('searchTerm'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: integer('userId').notNull(),
    teamId: integer('teamId'),
  },
  (table) => [
    index('FilterSegment_scope_userId_tableIdentifier_idx').on(
      table.scope,
      table.userId,
      table.tableIdentifier,
    ),
    index('FilterSegment_scope_teamId_tableIdentifier_idx').on(
      table.scope,
      table.teamId,
      table.tableIdentifier,
    ),
    foreignKey({
      name: 'FilterSegment_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FilterSegment_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userFilterSegmentPreference = pgTable(
  'UserFilterSegmentPreference',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId').notNull(),
    tableIdentifier: text('tableIdentifier').notNull(),
    segmentId: integer('segmentId'),
    systemSegmentId: text('systemSegmentId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('UserFilterSegmentPreference_userId_idx').on(table.userId),
    index('UserFilterSegmentPreference_segmentId_idx').on(table.segmentId),
    uniqueIndex('UserFilterSegmentPreference_userId_tableIdentifier_key').on(
      table.userId,
      table.tableIdentifier,
    ),
    foreignKey({
      name: 'UserFilterSegmentPreference_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserFilterSegmentPreference_segmentId_fkey',
      columns: [table.segmentId],
      foreignColumns: [filterSegment.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingInternalNote = pgTable(
  'BookingInternalNote',
  {
    id: serial('id').primaryKey(),
    notePresetId: integer('notePresetId'),
    text: text('text'),
    bookingId: integer('bookingId').notNull(),
    createdById: integer('createdById').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('BookingInternalNote_bookingId_idx').on(table.bookingId),
    uniqueIndex('BookingInternalNote_bookingId_notePresetId_key').on(
      table.bookingId,
      table.notePresetId,
    ),
    foreignKey({
      name: 'BookingInternalNote_notePresetId_fkey',
      columns: [table.notePresetId],
      foreignColumns: [internalNotePreset.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingInternalNote_bookingId_fkey',
      columns: [table.bookingId],
      foreignColumns: [booking.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingInternalNote_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [users.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const role = pgTable(
  'Role',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    color: text('color'),
    description: text('description'),
    teamId: integer('teamId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: roleTypeEnum('type').notNull().default('CUSTOM'),
  },
  (table) => [
    index('Role_teamId_idx').on(table.teamId),
    uniqueIndex('Role_name_teamId_key').on(table.name, table.teamId),
    foreignKey({ name: 'Role_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const rolePermission = pgTable(
  'RolePermission',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    roleId: text('roleId').notNull(),
    resource: text('resource').notNull(),
    action: text('action').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('RolePermission_roleId_idx').on(table.roleId),
    index('RolePermission_action_idx').on(table.action),
    uniqueIndex('RolePermission_roleId_resource_action_key').on(
      table.roleId,
      table.resource,
      table.action,
    ),
    foreignKey({
      name: 'RolePermission_roleId_fkey',
      columns: [table.roleId],
      foreignColumns: [role.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const auditActor = pgTable(
  'AuditActor',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => uuidv7()),
    type: auditActorTypeEnum('type').notNull(),
    userUuid: uuid('userUuid'),
    attendeeId: integer('attendeeId'),
    credentialId: integer('credentialId'),
    email: text('email'),
    phone: text('phone'),
    name: text('name'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('AuditActor_email_idx').on(table.email),
    index('AuditActor_userUuid_idx').on(table.userUuid),
    index('AuditActor_attendeeId_idx').on(table.attendeeId),
    index('AuditActor_credentialId_idx').on(table.credentialId),
    uniqueIndex('AuditActor_userUuid_key').on(table.userUuid),
    uniqueIndex('AuditActor_attendeeId_key').on(table.attendeeId),
    uniqueIndex('AuditActor_credentialId_key').on(table.credentialId),
    uniqueIndex('AuditActor_email_key').on(table.email),
    uniqueIndex('AuditActor_phone_key').on(table.phone),
  ],
)

export const bookingAudit = pgTable(
  'BookingAudit',
  {
    id: uuid('id')
      .primaryKey()
      .$defaultFn(() => uuidv7()),
    bookingUid: text('bookingUid').notNull(),
    actorId: uuid('actorId').notNull(),
    type: bookingAuditTypeEnum('type').notNull(),
    action: bookingAuditActionEnum('action').notNull(),
    timestamp: timestamp('timestamp', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    source: bookingAuditSourceEnum('source').notNull(),
    operationId: text('operationId').notNull(),
    data: jsonb('data'),
    context: jsonb('context'),
  },
  (table) => [
    index('BookingAudit_actorId_idx').on(table.actorId),
    index('BookingAudit_bookingUid_idx').on(table.bookingUid),
    index('BookingAudit_timestamp_idx').on(table.timestamp),
    index('BookingAudit_operationId_idx').on(table.operationId),
    foreignKey({
      name: 'BookingAudit_actorId_fkey',
      columns: [table.actorId],
      foreignColumns: [auditActor.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const agent = pgTable(
  'Agent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    providerAgentId: text('providerAgentId').notNull(),
    inboundEventTypeId: integer('inboundEventTypeId'),
    outboundEventTypeId: integer('outboundEventTypeId'),
    enabled: boolean('enabled').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Agent_providerAgentId_key').on(table.providerAgentId),
    index('Agent_userId_idx').on(table.userId),
    index('Agent_teamId_idx').on(table.teamId),
    index('Agent_inboundEventTypeId_idx').on(table.inboundEventTypeId),
    index('Agent_outboundEventTypeId_idx').on(table.outboundEventTypeId),
    foreignKey({ name: 'Agent_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Agent_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const calAiPhoneNumber = pgTable(
  'CalAiPhoneNumber',
  {
    id: serial('id').primaryKey(),
    userId: integer('userId'),
    teamId: integer('teamId'),
    phoneNumber: text('phoneNumber').notNull(),
    provider: text('provider').notNull(),
    providerPhoneNumberId: text('providerPhoneNumberId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    stripeCustomerId: text('stripeCustomerId'),
    stripeSubscriptionId: text('stripeSubscriptionId'),
    subscriptionStatus: phoneNumberSubscriptionStatusEnum('subscriptionStatus'),
    inboundAgentId: text('inboundAgentId'),
    outboundAgentId: text('outboundAgentId'),
  },
  (table) => [
    uniqueIndex('CalAiPhoneNumber_phoneNumber_key').on(table.phoneNumber),
    uniqueIndex('CalAiPhoneNumber_providerPhoneNumberId_key').on(table.providerPhoneNumberId),
    uniqueIndex('CalAiPhoneNumber_stripeSubscriptionId_key').on(table.stripeSubscriptionId),
    index('CalAiPhoneNumber_userId_idx').on(table.userId),
    index('CalAiPhoneNumber_teamId_idx').on(table.teamId),
    index('CalAiPhoneNumber_inboundAgentId_idx').on(table.inboundAgentId),
    index('CalAiPhoneNumber_outboundAgentId_idx').on(table.outboundAgentId),
    foreignKey({
      name: 'CalAiPhoneNumber_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CalAiPhoneNumber_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CalAiPhoneNumber_inboundAgentId_fkey',
      columns: [table.inboundAgentId],
      foreignColumns: [agent.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CalAiPhoneNumber_outboundAgentId_fkey',
      columns: [table.outboundAgentId],
      foreignColumns: [agent.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const teamBilling = pgTable(
  'TeamBilling',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId').notNull(),
    subscriptionId: text('subscriptionId').notNull(),
    subscriptionItemId: text('subscriptionItemId').notNull(),
    customerId: text('customerId').notNull(),
    status: text('status').notNull(),
    planName: text('planName').notNull(),
    subscriptionStart: timestamp('subscriptionStart', { precision: 3 }),
    subscriptionTrialEnd: timestamp('subscriptionTrialEnd', { precision: 3 }),
    subscriptionEnd: timestamp('subscriptionEnd', { precision: 3 }),
    billingPeriod: billingPeriodEnum('billingPeriod'),
    billingMode: billingModeEnum('billingMode').notNull().default('SEATS'),
    pricePerSeat: integer('pricePerSeat'),
    paidSeats: integer('paidSeats'),
    highWaterMark: integer('highWaterMark'),
    highWaterMarkPeriodStart: timestamp('highWaterMarkPeriodStart', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('TeamBilling_teamId_key').on(table.teamId),
    uniqueIndex('TeamBilling_subscriptionId_key').on(table.subscriptionId),
    foreignKey({
      name: 'TeamBilling_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organizationBilling = pgTable(
  'OrganizationBilling',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId').notNull(),
    subscriptionId: text('subscriptionId').notNull(),
    subscriptionItemId: text('subscriptionItemId').notNull(),
    customerId: text('customerId').notNull(),
    status: text('status').notNull(),
    planName: text('planName').notNull(),
    subscriptionStart: timestamp('subscriptionStart', { precision: 3 }),
    subscriptionTrialEnd: timestamp('subscriptionTrialEnd', { precision: 3 }),
    subscriptionEnd: timestamp('subscriptionEnd', { precision: 3 }),
    billingPeriod: billingPeriodEnum('billingPeriod'),
    billingMode: billingModeEnum('billingMode').notNull().default('SEATS'),
    pricePerSeat: integer('pricePerSeat'),
    paidSeats: integer('paidSeats'),
    highWaterMark: integer('highWaterMark'),
    highWaterMarkPeriodStart: timestamp('highWaterMarkPeriodStart', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('OrganizationBilling_teamId_key').on(table.teamId),
    uniqueIndex('OrganizationBilling_subscriptionId_key').on(table.subscriptionId),
    foreignKey({
      name: 'OrganizationBilling_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const seatChangeLog = pgTable(
  'SeatChangeLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId').notNull(),
    changeType: seatChangeTypeEnum('changeType').notNull(),
    seatCount: integer('seatCount').notNull(),
    userId: integer('userId'),
    triggeredBy: integer('triggeredBy'),
    changeDate: timestamp('changeDate', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    monthKey: text('monthKey').notNull(),
    operationId: text('operationId'),
    processedInProrationId: text('processedInProrationId'),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    teamBillingId: text('teamBillingId'),
    organizationBillingId: text('organizationBillingId'),
  },
  (table) => [
    index('SeatChangeLog_teamId_monthKey_idx').on(table.teamId, table.monthKey),
    index('SeatChangeLog_teamId_processedInProrationId_idx').on(
      table.teamId,
      table.processedInProrationId,
    ),
    index('SeatChangeLog_monthKey_idx').on(table.monthKey),
    uniqueIndex('SeatChangeLog_teamId_operationId_key').on(table.teamId, table.operationId),
    foreignKey({
      name: 'SeatChangeLog_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SeatChangeLog_processedInProrationId_fkey',
      columns: [table.processedInProrationId],
      foreignColumns: [monthlyProration.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SeatChangeLog_teamBillingId_fkey',
      columns: [table.teamBillingId],
      foreignColumns: [teamBilling.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SeatChangeLog_organizationBillingId_fkey',
      columns: [table.organizationBillingId],
      foreignColumns: [organizationBilling.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const monthlyProration = pgTable(
  'MonthlyProration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    teamId: integer('teamId').notNull(),
    monthKey: text('monthKey').notNull(),
    periodStart: timestamp('periodStart', { precision: 3 }).notNull(),
    periodEnd: timestamp('periodEnd', { precision: 3 }).notNull(),
    seatsAtStart: integer('seatsAtStart').notNull(),
    seatsAdded: integer('seatsAdded').notNull(),
    seatsRemoved: integer('seatsRemoved').notNull(),
    netSeatIncrease: integer('netSeatIncrease').notNull(),
    seatsAtEnd: integer('seatsAtEnd').notNull(),
    subscriptionId: text('subscriptionId').notNull(),
    subscriptionItemId: text('subscriptionItemId').notNull(),
    customerId: text('customerId').notNull(),
    subscriptionStart: timestamp('subscriptionStart', { precision: 3 }).notNull(),
    subscriptionEnd: timestamp('subscriptionEnd', { precision: 3 }).notNull(),
    remainingDays: integer('remainingDays').notNull(),
    pricePerSeat: integer('pricePerSeat').notNull(),
    proratedAmount: integer('proratedAmount').notNull(),
    invoiceItemId: text('invoiceItemId'),
    invoiceId: text('invoiceId'),
    invoiceUrl: text('invoiceUrl'),
    status: prorationStatusEnum('status').notNull().default('PENDING'),
    chargedAt: timestamp('chargedAt', { precision: 3 }),
    failedAt: timestamp('failedAt', { precision: 3 }),
    failureReason: text('failureReason'),
    retryCount: integer('retryCount').notNull().default(0),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    teamBillingId: text('teamBillingId'),
    organizationBillingId: text('organizationBillingId'),
  },
  (table) => [
    index('MonthlyProration_monthKey_status_idx').on(table.monthKey, table.status),
    index('MonthlyProration_status_idx').on(table.status),
    index('MonthlyProration_teamId_idx').on(table.teamId),
    uniqueIndex('MonthlyProration_teamId_monthKey_key').on(table.teamId, table.monthKey),
    foreignKey({
      name: 'MonthlyProration_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'MonthlyProration_teamBillingId_fkey',
      columns: [table.teamBillingId],
      foreignColumns: [teamBilling.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'MonthlyProration_organizationBillingId_fkey',
      columns: [table.organizationBillingId],
      foreignColumns: [organizationBilling.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const calendarCacheEvent = pgTable(
  'CalendarCacheEvent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    selectedCalendarId: text('selectedCalendarId').notNull(),
    externalId: text('externalId').notNull(),
    externalEtag: text('externalEtag').notNull(),
    iCalUID: text('iCalUID'),
    iCalSequence: integer('iCalSequence').notNull().default(0),
    summary: text('summary'),
    description: text('description'),
    location: text('location'),
    start: timestamp('start', { precision: 3 }).notNull(),
    end: timestamp('end', { precision: 3 }).notNull(),
    isAllDay: boolean('isAllDay').notNull().default(false),
    timeZone: text('timeZone'),
    status: calendarCacheEventStatusEnum('status').notNull().default('confirmed'),
    recurringEventId: text('recurringEventId'),
    originalStartTime: timestamp('originalStartTime', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    externalCreatedAt: timestamp('externalCreatedAt', { precision: 3 }),
    externalUpdatedAt: timestamp('externalUpdatedAt', { precision: 3 }),
  },
  (table) => [
    index('CalendarCacheEvent_start_end_status_idx').on(table.start, table.end, table.status),
    index('CalendarCacheEvent_selectedCalendarId_iCalUID_idx').on(
      table.selectedCalendarId,
      table.iCalUID,
    ),
    uniqueIndex('CalendarCacheEvent_selectedCalendarId_externalId_key').on(
      table.selectedCalendarId,
      table.externalId,
    ),
    foreignKey({
      name: 'CalendarCacheEvent_selectedCalendarId_fkey',
      columns: [table.selectedCalendarId],
      foreignColumns: [selectedCalendar.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const integrationAttributeSync = pgTable(
  'IntegrationAttributeSync',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organizationId: integer('organizationId').notNull(),
    name: text('name').notNull(),
    integration: text('integration').notNull(),
    credentialId: integer('credentialId'),
    enabled: boolean('enabled').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('IntegrationAttributeSync_organizationId_idx').on(table.organizationId),
    index('IntegrationAttributeSync_credentialId_idx').on(table.credentialId),
    foreignKey({
      name: 'IntegrationAttributeSync_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'IntegrationAttributeSync_credentialId_fkey',
      columns: [table.credentialId],
      foreignColumns: [credential.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attributeSyncRule = pgTable(
  'AttributeSyncRule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    integrationAttributeSyncId: text('integrationAttributeSyncId').notNull(),
    rule: jsonb('rule').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('AttributeSyncRule_integrationAttributeSyncId_key').on(
      table.integrationAttributeSyncId,
    ),
    foreignKey({
      name: 'AttributeSyncRule_integrationAttributeSyncId_fkey',
      columns: [table.integrationAttributeSyncId],
      foreignColumns: [integrationAttributeSync.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attributeSyncFieldMapping = pgTable(
  'AttributeSyncFieldMapping',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    integrationFieldName: text('integrationFieldName').notNull(),
    attributeId: text('attributeId').notNull(),
    enabled: boolean('enabled').notNull(),
    integrationAttributeSyncId: text('integrationAttributeSyncId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('AttributeSyncFieldMapping_integrationAttributeSyncId_idx').on(
      table.integrationAttributeSyncId,
    ),
    uniqueIndex('AttributeSyncFieldMapping_integrationAttributeSyncId_attrib_key').on(
      table.integrationAttributeSyncId,
      table.attributeId,
    ),
    foreignKey({
      name: 'AttributeSyncFieldMapping_attributeId_fkey',
      columns: [table.attributeId],
      foreignColumns: [attribute.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttributeSyncFieldMapping_integrationAttributeSyncId_fkey',
      columns: [table.integrationAttributeSyncId],
      foreignColumns: [integrationAttributeSync.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingTimeStatus = pgTable(
  'BookingTimeStatus',
  {
    id: integer('id').notNull(),
    uid: text('uid'),
    eventTypeId: integer('eventTypeId'),
    title: text('title'),
    description: text('description'),
    startTime: timestamp('startTime', { precision: 3 }),
    endTime: timestamp('endTime', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 }),
    location: text('location'),
    paid: boolean('paid'),
    status: bookingStatusEnum('status'),
    rescheduled: boolean('rescheduled'),
    userId: integer('userId'),
    teamId: integer('teamId'),
    eventLength: integer('eventLength'),
    timeStatus: text('timeStatus'),
    eventParentId: integer('eventParentId'),
    userEmail: text('userEmail'),
    username: text('username'),
    ratingFeedback: text('ratingFeedback'),
    rating: integer('rating'),
    noShowHost: boolean('noShowHost'),
    isTeamBooking: boolean('isTeamBooking').notNull(),
  },
  (table) => [uniqueIndex('BookingTimeStatus_id_key').on(table.id)],
)

export const bookingTimeStatusDenormalized = pgTable(
  'BookingTimeStatusDenormalized',
  {
    id: integer('id').notNull(),
    uid: text('uid').notNull(),
    eventTypeId: integer('eventTypeId'),
    title: text('title').notNull(),
    description: text('description'),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    location: text('location'),
    paid: boolean('paid').notNull(),
    status: bookingStatusEnum('status').notNull(),
    rescheduled: boolean('rescheduled'),
    userId: integer('userId'),
    teamId: integer('teamId'),
    eventLength: integer('eventLength'),
    eventParentId: integer('eventParentId'),
    userEmail: text('userEmail'),
    userName: text('userName'),
    userUsername: text('userUsername'),
    ratingFeedback: text('ratingFeedback'),
    rating: integer('rating'),
    noShowHost: boolean('noShowHost'),
    isTeamBooking: boolean('isTeamBooking').notNull(),
    timeStatus: text('timeStatus'),
  },
  (table) => [uniqueIndex('BookingTimeStatusDenormalized_id_key').on(table.id)],
)

export const userEventtype = pgTable(
  '_user_eventtype',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    primaryKey({ name: '_user_eventtype_AB_pkey', columns: [table.A, table.B] }),
    index('_user_eventtype_B_index').on(table.B),
    foreignKey({
      name: '_user_eventtype_A_fkey',
      columns: [table.A],
      foreignColumns: [eventType.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_user_eventtype_B_fkey', columns: [table.B], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const platformOauthClientToUser = pgTable(
  '_PlatformOAuthClientToUser',
  { A: text('A').notNull(), B: integer('B').notNull() },
  (table) => [
    primaryKey({ name: '_PlatformOAuthClientToUser_AB_pkey', columns: [table.A, table.B] }),
    index('_PlatformOAuthClientToUser_B_index').on(table.B),
    foreignKey({
      name: '_PlatformOAuthClientToUser_A_fkey',
      columns: [table.A],
      foreignColumns: [platformOAuthClient.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: '_PlatformOAuthClientToUser_B_fkey',
      columns: [table.B],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    host,
    hostGroup,
    hostLocation,
    calVideoSettings,
    videoCallGuest,
    eventType,
    credential,
    destinationCalendar,
    userPassword,
    travelSchedule,
    users,
    notificationsSubscriptions,
    profile,
    team,
    creditBalance,
    creditPurchaseLog,
    creditExpenseLog,
    organizationSettings,
    membership,
    verificationToken,
    instantMeetingToken,
    bookingReference,
    attendee,
    booking,
    tracking,
    schedule,
    availability,
    selectedCalendar,
    eventTypeCustomInput,
    resetPasswordRequest,
    reminderMail,
    payment,
    webhook,
    apiKey,
    rateLimit,
    hashedLink,
    account,
    session,
    app,
    feedback,
    deployment,
    webhookScheduledTriggers,
    bookingSeat,
    verifiedNumber,
    verifiedEmail,
    feature,
    userFeatures,
    teamFeatures,
    selectedSlots,
    oAuthClient,
    accessCode,
    bookingDenormalized,
    calendarCache,
    tempOrgRedirect,
    avatars,
    outOfOfficeEntry,
    outOfOfficeReason,
    userHolidaySettings,
    holidayCache,
    platformOAuthClient,
    platformAuthorizationToken,
    accessToken,
    refreshToken,
    dSyncData,
    dSyncTeamGroupMapping,
    secondaryEmail,
    task,
    managedOrganization,
    platformBilling,
    attributeOption,
    attribute,
    attributeToUser,
    assignmentReason,
    delegationCredential,
    workspacePlatform,
    eventTypeTranslation,
    watchlist,
    watchlistAudit,
    watchlistEventAudit,
    bookingReport,
    wrongAssignmentReport,
    organizationOnboarding,
    internalNotePreset,
    filterSegment,
    userFilterSegmentPreference,
    bookingInternalNote,
    role,
    rolePermission,
    auditActor,
    bookingAudit,
    agent,
    calAiPhoneNumber,
    teamBilling,
    organizationBilling,
    seatChangeLog,
    monthlyProration,
    calendarCacheEvent,
    integrationAttributeSync,
    attributeSyncRule,
    attributeSyncFieldMapping,
    bookingTimeStatus,
    bookingTimeStatusDenormalized,
    userEventtype,
    platformOauthClientToUser,
  },
  (r) => ({
    host: {
      user: r.one.users({ from: r.host.userId, to: r.users.id, optional: false }),
      eventType: r.one.eventType({ from: r.host.eventTypeId, to: r.eventType.id, optional: false }),
      schedule: r.one.schedule({ from: r.host.scheduleId, to: r.schedule.id }),
      group: r.one.hostGroup({ from: r.host.groupId, to: r.hostGroup.id }),
      member: r.one.membership({ from: r.host.memberId, to: r.membership.id }),
      location: r.one.hostLocation({
        from: [r.host.userId, r.host.eventTypeId],
        to: [r.hostLocation.userId, r.hostLocation.eventTypeId],
      }),
    },
    hostGroup: {
      hosts: r.many.host(),
      eventType: r.one.eventType({ from: r.hostGroup.eventTypeId, to: r.eventType.id }),
    },
    hostLocation: {
      host: r.one.host({
        from: [r.hostLocation.userId, r.hostLocation.eventTypeId],
        to: [r.host.userId, r.host.eventTypeId],
        optional: false,
      }),
      credential: r.one.credential({ from: r.hostLocation.credentialId, to: r.credential.id }),
    },
    calVideoSettings: {
      eventType: r.one.eventType({
        from: r.calVideoSettings.eventTypeId,
        to: r.eventType.id,
        optional: false,
      }),
    },
    eventType: {
      hosts: r.many.host(),
      users: r.many.users({
        from: r.eventType.id.through(r.userEventtype.A),
        to: r.users.id.through(r.userEventtype.B),
      }),
      owner: r.one.users({ from: r.eventType.userId, to: r.users.id, alias: 'owner' }),
      profile: r.one.profile({ from: r.eventType.profileId, to: r.profile.id }),
      team: r.one.team({ from: r.eventType.teamId, to: r.team.id }),
      hashedLink: r.many.hashedLink(),
      bookings: r.many.booking(),
      availability: r.many.availability(),
      webhooks: r.many.webhook(),
      destinationCalendar: r.one.destinationCalendar({
        from: r.eventType.id,
        to: r.destinationCalendar.eventTypeId,
      }),
      customInputs: r.many.eventTypeCustomInput(),
      parent: r.one.eventType({
        from: r.eventType.parentId,
        to: r.eventType.id,
        alias: 'managed_eventtype',
      }),
      children: r.many.eventType({ alias: 'managed_eventtype' }),
      schedule: r.one.schedule({
        from: r.eventType.scheduleId,
        to: r.schedule.id,
        alias: 'EventTypeToSchedule',
      }),
      instantMeetingSchedule: r.one.schedule({
        from: r.eventType.instantMeetingScheduleId,
        to: r.schedule.id,
        alias: 'InstantMeetingSchedule',
      }),
      fieldTranslations: r.many.eventTypeTranslation(),
      selectedCalendars: r.many.selectedCalendar(),
      calVideoSettings: r.one.calVideoSettings({
        from: r.eventType.id,
        to: r.calVideoSettings.eventTypeId,
      }),
      secondaryEmail: r.one.secondaryEmail({
        from: r.eventType.secondaryEmailId,
        to: r.secondaryEmail.id,
      }),
      restrictionSchedule: r.one.schedule({
        from: r.eventType.restrictionScheduleId,
        to: r.schedule.id,
        alias: 'restrictionSchedule',
      }),
      hostGroups: r.many.hostGroup(),
    },
    credential: {
      user: r.one.users({ from: r.credential.userId, to: r.users.id }),
      team: r.one.team({ from: r.credential.teamId, to: r.team.id }),
      app: r.one.app({ from: r.credential.appId, to: r.app.slug }),
      destinationCalendars: r.many.destinationCalendar(),
      selectedCalendars: r.many.selectedCalendar(),
      CalendarCache: r.many.calendarCache(),
      references: r.many.bookingReference(),
      delegationCredential: r.one.delegationCredential({
        from: r.credential.delegationCredentialId,
        to: r.delegationCredential.id,
      }),
      integrationAttributeSyncs: r.many.integrationAttributeSync(),
      hostLocations: r.many.hostLocation(),
    },
    destinationCalendar: {
      user: r.one.users({ from: r.destinationCalendar.userId, to: r.users.id }),
      booking: r.many.booking(),
      eventType: r.one.eventType({ from: r.destinationCalendar.eventTypeId, to: r.eventType.id }),
      credential: r.one.credential({
        from: r.destinationCalendar.credentialId,
        to: r.credential.id,
      }),
      delegationCredential: r.one.delegationCredential({
        from: r.destinationCalendar.delegationCredentialId,
        to: r.delegationCredential.id,
      }),
    },
    userPassword: {
      user: r.one.users({ from: r.userPassword.userId, to: r.users.id, optional: false }),
    },
    travelSchedule: {
      user: r.one.users({ from: r.travelSchedule.userId, to: r.users.id, optional: false }),
    },
    users: {
      password: r.one.userPassword({ from: r.users.id, to: r.userPassword.userId }),
      travelSchedules: r.many.travelSchedule(),
      eventTypes: r.many.eventType({
        from: r.users.id.through(r.userEventtype.B),
        to: r.eventType.id.through(r.userEventtype.A),
      }),
      credentials: r.many.credential(),
      teams: r.many.membership(),
      bookings: r.many.booking({ alias: 'BookingToUser' }),
      schedules: r.many.schedule(),
      selectedCalendars: r.many.selectedCalendar(),
      availability: r.many.availability(),
      webhooks: r.many.webhook(),
      destinationCalendar: r.one.destinationCalendar({
        from: r.users.id,
        to: r.destinationCalendar.userId,
      }),
      apiKeys: r.many.apiKey(),
      accounts: r.many.account(),
      sessions: r.many.session(),
      Feedback: r.many.feedback(),
      ownedEventTypes: r.many.eventType({ alias: 'owner' }),
      verifiedNumbers: r.many.verifiedNumber(),
      verifiedEmails: r.many.verifiedEmail(),
      hosts: r.many.host(),
      organization: r.one.team({ from: r.users.organizationId, to: r.team.id }),
      accessCodes: r.many.accessCode(),
      bookingRedirects: r.many.outOfOfficeEntry({ alias: 'OutOfOfficeEntryToUser' }),
      bookingRedirectsTo: r.many.outOfOfficeEntry({ alias: 'toUser' }),
      platformOAuthClients: r.many.platformOAuthClient({
        from: r.users.id.through(r.platformOauthClientToUser.B),
        to: r.platformOAuthClient.id.through(r.platformOauthClientToUser.A),
      }),
      AccessToken: r.many.accessToken(),
      RefreshToken: r.many.refreshToken(),
      PlatformAuthorizationToken: r.many.platformAuthorizationToken(),
      profiles: r.many.profile({ alias: 'ProfileToUser' }),
      movedToProfile: r.one.profile({
        from: r.users.movedToProfileId,
        to: r.profile.id,
        alias: 'moved_to_profile',
      }),
      secondaryEmails: r.many.secondaryEmail(),
      OutOfOfficeReasons: r.many.outOfOfficeReason(),
      NotificationsSubscriptions: r.many.notificationsSubscriptions(),
      features: r.many.userFeatures(),
      reassignedBookings: r.many.booking({ alias: 'reassignByUser' }),
      createdAttributeToUsers: r.many.attributeToUser({ alias: 'createdBy' }),
      updatedAttributeToUsers: r.many.attributeToUser({ alias: 'updatedBy' }),
      createdTranslations: r.many.eventTypeTranslation({ alias: 'CreatedEventTypeTranslations' }),
      updatedTranslations: r.many.eventTypeTranslation({ alias: 'UpdatedEventTypeTranslations' }),
      reportedBookings: r.many.bookingReport(),
      wrongAssignmentReports: r.many.wrongAssignmentReport({ alias: 'WrongAssignmentReportedBy' }),
      reviewedWrongAssignmentReports: r.many.wrongAssignmentReport({
        alias: 'WrongAssignmentReviewedBy',
      }),
      BookingInternalNote: r.many.bookingInternalNote(),
      createdOrganizationOnboardings: r.many.organizationOnboarding(),
      filterSegments: r.many.filterSegment(),
      filterSegmentPreferences: r.many.userFilterSegmentPreference(),
      creditBalance: r.one.creditBalance({ from: r.users.id, to: r.creditBalance.userId }),
      calAiPhoneNumbers: r.many.calAiPhoneNumber(),
      agents: r.many.agent(),
      oAuthClients: r.many.oAuthClient(),
      holidaySettings: r.one.userHolidaySettings({
        from: r.users.id,
        to: r.userHolidaySettings.userId,
      }),
    },
    notificationsSubscriptions: {
      user: r.one.users({
        from: r.notificationsSubscriptions.userId,
        to: r.users.id,
        optional: false,
      }),
    },
    profile: {
      user: r.one.users({
        from: r.profile.userId,
        to: r.users.id,
        optional: false,
        alias: 'ProfileToUser',
      }),
      organization: r.one.team({ from: r.profile.organizationId, to: r.team.id, optional: false }),
      eventTypes: r.many.eventType(),
      movedFromUser: r.one.users({
        from: r.profile.id,
        to: r.users.movedToProfileId,
        alias: 'moved_to_profile',
      }),
    },
    team: {
      members: r.many.membership(),
      eventTypes: r.many.eventType(),
      verifiedNumbers: r.many.verifiedNumber(),
      verifiedEmails: r.many.verifiedEmail(),
      parent: r.one.team({ from: r.team.parentId, to: r.team.id, alias: 'organization' }),
      children: r.many.team({ alias: 'organization' }),
      orgUsers: r.many.users(),
      inviteTokens: r.many.verificationToken(),
      webhooks: r.many.webhook(),
      apiKeys: r.many.apiKey(),
      credentials: r.many.credential(),
      accessCodes: r.many.accessCode(),
      organizationSettings: r.one.organizationSettings({
        from: r.team.id,
        to: r.organizationSettings.organizationId,
      }),
      instantMeetingTokens: r.many.instantMeetingToken(),
      orgProfiles: r.many.profile(),
      dsyncTeamGroupMapping: r.many.dSyncTeamGroupMapping(),
      platformOAuthClient: r.many.platformOAuthClient({ alias: 'PlatformOAuthClientToTeam' }),
      createdByOAuthClient: r.one.platformOAuthClient({
        from: r.team.createdByOAuthClientId,
        to: r.platformOAuthClient.id,
        alias: 'CreatedByOAuthClient',
      }),
      platformBilling: r.one.platformBilling({ from: r.team.id, to: r.platformBilling.id }),
      attributes: r.many.attribute(),
      delegationCredentials: r.many.delegationCredential(),
      roles: r.many.role(),
      calAiPhoneNumbers: r.many.calAiPhoneNumber(),
      agents: r.many.agent(),
      bookingReports: r.many.bookingReport(),
      wrongAssignmentReports: r.many.wrongAssignmentReport(),
      features: r.many.teamFeatures(),
      internalNotePresets: r.many.internalNotePreset(),
      creditBalance: r.one.creditBalance({ from: r.team.id, to: r.creditBalance.teamId }),
      organizationOnboarding: r.one.organizationOnboarding({
        from: r.team.id,
        to: r.organizationOnboarding.organizationId,
      }),
      managedOrganization: r.one.managedOrganization({
        from: r.team.id,
        to: r.managedOrganization.managedOrganizationId,
        alias: 'ManagedOrganization',
      }),
      managedOrganizations: r.many.managedOrganization({ alias: 'ManagerOrganization' }),
      filterSegments: r.many.filterSegment(),
      teamBilling: r.one.teamBilling({ from: r.team.id, to: r.teamBilling.teamId }),
      organizationBilling: r.one.organizationBilling({
        from: r.team.id,
        to: r.organizationBilling.teamId,
      }),
      integrationAttributeSyncs: r.many.integrationAttributeSync(),
      seatChangeLogs: r.many.seatChangeLog(),
      monthlyProrations: r.many.monthlyProration(),
    },
    creditBalance: {
      team: r.one.team({ from: r.creditBalance.teamId, to: r.team.id }),
      user: r.one.users({ from: r.creditBalance.userId, to: r.users.id }),
      expenseLogs: r.many.creditExpenseLog(),
      purchaseLogs: r.many.creditPurchaseLog(),
    },
    creditPurchaseLog: {
      creditBalance: r.one.creditBalance({
        from: r.creditPurchaseLog.creditBalanceId,
        to: r.creditBalance.id,
        optional: false,
      }),
    },
    creditExpenseLog: {
      creditBalance: r.one.creditBalance({
        from: r.creditExpenseLog.creditBalanceId,
        to: r.creditBalance.id,
        optional: false,
      }),
      booking: r.one.booking({ from: r.creditExpenseLog.bookingUid, to: r.booking.uid }),
    },
    organizationSettings: {
      organization: r.one.team({
        from: r.organizationSettings.organizationId,
        to: r.team.id,
        optional: false,
      }),
      dSyncData: r.one.dSyncData({
        from: r.organizationSettings.organizationId,
        to: r.dSyncData.organizationId,
      }),
    },
    membership: {
      customRole: r.one.role({ from: r.membership.customRoleId, to: r.role.id }),
      team: r.one.team({ from: r.membership.teamId, to: r.team.id, optional: false }),
      user: r.one.users({ from: r.membership.userId, to: r.users.id, optional: false }),
      AttributeToUser: r.many.attributeToUser(),
      Host: r.many.host(),
    },
    verificationToken: {
      team: r.one.team({ from: r.verificationToken.teamId, to: r.team.id }),
      secondaryEmail: r.one.secondaryEmail({
        from: r.verificationToken.secondaryEmailId,
        to: r.secondaryEmail.id,
      }),
    },
    instantMeetingToken: {
      team: r.one.team({ from: r.instantMeetingToken.teamId, to: r.team.id, optional: false }),
      booking: r.one.booking({ from: r.instantMeetingToken.bookingId, to: r.booking.id }),
    },
    bookingReference: {
      booking: r.one.booking({ from: r.bookingReference.bookingId, to: r.booking.id }),
      credential: r.one.credential({ from: r.bookingReference.credentialId, to: r.credential.id }),
      delegationCredential: r.one.delegationCredential({
        from: r.bookingReference.delegationCredentialId,
        to: r.delegationCredential.id,
      }),
    },
    attendee: {
      booking: r.one.booking({ from: r.attendee.bookingId, to: r.booking.id }),
      bookingSeat: r.one.bookingSeat({ from: r.attendee.id, to: r.bookingSeat.attendeeId }),
    },
    booking: {
      user: r.one.users({ from: r.booking.userId, to: r.users.id, alias: 'BookingToUser' }),
      references: r.many.bookingReference(),
      eventType: r.one.eventType({ from: r.booking.eventTypeId, to: r.eventType.id }),
      attendees: r.many.attendee(),
      payment: r.many.payment(),
      destinationCalendar: r.one.destinationCalendar({
        from: r.booking.destinationCalendarId,
        to: r.destinationCalendar.id,
      }),
      reassignBy: r.one.users({
        from: r.booking.reassignById,
        to: r.users.id,
        alias: 'reassignByUser',
      }),
      seatsReferences: r.many.bookingSeat(),
      instantMeetingToken: r.one.instantMeetingToken({
        from: r.booking.id,
        to: r.instantMeetingToken.bookingId,
      }),
      scheduledTriggers: r.many.webhookScheduledTriggers(),
      assignmentReason: r.many.assignmentReason(),
      internalNote: r.many.bookingInternalNote(),
      tracking: r.one.tracking({ from: r.booking.id, to: r.tracking.bookingId }),
      expenseLogs: r.many.creditExpenseLog(),
      report: r.one.bookingReport({ from: r.booking.uid, to: r.bookingReport.bookingUid }),
      wrongAssignmentReports: r.many.wrongAssignmentReport(),
    },
    tracking: {
      booking: r.one.booking({ from: r.tracking.bookingId, to: r.booking.id, optional: false }),
    },
    schedule: {
      user: r.one.users({ from: r.schedule.userId, to: r.users.id, optional: false }),
      eventType: r.many.eventType({ alias: 'EventTypeToSchedule' }),
      instantMeetingEvents: r.many.eventType({ alias: 'InstantMeetingSchedule' }),
      restrictionSchedule: r.many.eventType({ alias: 'restrictionSchedule' }),
      availability: r.many.availability(),
      Host: r.many.host(),
    },
    availability: {
      user: r.one.users({ from: r.availability.userId, to: r.users.id }),
      eventType: r.one.eventType({ from: r.availability.eventTypeId, to: r.eventType.id }),
      Schedule: r.one.schedule({ from: r.availability.scheduleId, to: r.schedule.id }),
    },
    selectedCalendar: {
      user: r.one.users({ from: r.selectedCalendar.userId, to: r.users.id, optional: false }),
      credential: r.one.credential({ from: r.selectedCalendar.credentialId, to: r.credential.id }),
      delegationCredential: r.one.delegationCredential({
        from: r.selectedCalendar.delegationCredentialId,
        to: r.delegationCredential.id,
      }),
      eventType: r.one.eventType({ from: r.selectedCalendar.eventTypeId, to: r.eventType.id }),
      calendarCacheEvents: r.many.calendarCacheEvent(),
    },
    eventTypeCustomInput: {
      eventType: r.one.eventType({
        from: r.eventTypeCustomInput.eventTypeId,
        to: r.eventType.id,
        optional: false,
      }),
    },
    payment: {
      app: r.one.app({ from: r.payment.appId, to: r.app.slug }),
      booking: r.one.booking({ from: r.payment.bookingId, to: r.booking.id }),
    },
    webhook: {
      user: r.one.users({ from: r.webhook.userId, to: r.users.id }),
      team: r.one.team({ from: r.webhook.teamId, to: r.team.id }),
      eventType: r.one.eventType({ from: r.webhook.eventTypeId, to: r.eventType.id }),
      platformOAuthClient: r.one.platformOAuthClient({
        from: r.webhook.platformOAuthClientId,
        to: r.platformOAuthClient.id,
      }),
      app: r.one.app({ from: r.webhook.appId, to: r.app.slug }),
      scheduledTriggers: r.many.webhookScheduledTriggers(),
    },
    apiKey: {
      user: r.one.users({ from: r.apiKey.userId, to: r.users.id }),
      team: r.one.team({ from: r.apiKey.teamId, to: r.team.id }),
      app: r.one.app({ from: r.apiKey.appId, to: r.app.slug }),
      rateLimits: r.many.rateLimit(),
    },
    rateLimit: {
      apiKey: r.one.apiKey({ from: r.rateLimit.apiKeyId, to: r.apiKey.id, optional: false }),
    },
    hashedLink: {
      eventType: r.one.eventType({
        from: r.hashedLink.eventTypeId,
        to: r.eventType.id,
        optional: false,
      }),
    },
    account: { user: r.one.users({ from: r.account.userId, to: r.users.id }) },
    session: { user: r.one.users({ from: r.session.userId, to: r.users.id }) },
    app: {
      credentials: r.many.credential(),
      payments: r.many.payment(),
      Webhook: r.many.webhook(),
      ApiKey: r.many.apiKey(),
    },
    feedback: { user: r.one.users({ from: r.feedback.userId, to: r.users.id, optional: false }) },
    webhookScheduledTriggers: {
      webhook: r.one.webhook({ from: r.webhookScheduledTriggers.webhookId, to: r.webhook.id }),
      booking: r.one.booking({ from: r.webhookScheduledTriggers.bookingId, to: r.booking.id }),
    },
    bookingSeat: {
      booking: r.one.booking({ from: r.bookingSeat.bookingId, to: r.booking.id, optional: false }),
      attendee: r.one.attendee({
        from: r.bookingSeat.attendeeId,
        to: r.attendee.id,
        optional: false,
      }),
    },
    verifiedNumber: {
      user: r.one.users({ from: r.verifiedNumber.userId, to: r.users.id }),
      team: r.one.team({ from: r.verifiedNumber.teamId, to: r.team.id }),
    },
    verifiedEmail: {
      user: r.one.users({ from: r.verifiedEmail.userId, to: r.users.id }),
      team: r.one.team({ from: r.verifiedEmail.teamId, to: r.team.id }),
    },
    feature: { users: r.many.userFeatures(), teams: r.many.teamFeatures() },
    userFeatures: {
      user: r.one.users({ from: r.userFeatures.userId, to: r.users.id, optional: false }),
      feature: r.one.feature({
        from: r.userFeatures.featureId,
        to: r.feature.slug,
        optional: false,
      }),
    },
    teamFeatures: {
      team: r.one.team({ from: r.teamFeatures.teamId, to: r.team.id, optional: false }),
      feature: r.one.feature({
        from: r.teamFeatures.featureId,
        to: r.feature.slug,
        optional: false,
      }),
    },
    oAuthClient: {
      accessCodes: r.many.accessCode(),
      user: r.one.users({ from: r.oAuthClient.userId, to: r.users.id }),
    },
    accessCode: {
      client: r.one.oAuthClient({ from: r.accessCode.clientId, to: r.oAuthClient.clientId }),
      user: r.one.users({ from: r.accessCode.userId, to: r.users.id }),
      team: r.one.team({ from: r.accessCode.teamId, to: r.team.id }),
    },
    calendarCache: {
      credential: r.one.credential({ from: r.calendarCache.credentialId, to: r.credential.id }),
    },
    outOfOfficeEntry: {
      user: r.one.users({
        from: r.outOfOfficeEntry.userId,
        to: r.users.id,
        optional: false,
        alias: 'OutOfOfficeEntryToUser',
      }),
      toUser: r.one.users({ from: r.outOfOfficeEntry.toUserId, to: r.users.id, alias: 'toUser' }),
      reason: r.one.outOfOfficeReason({
        from: r.outOfOfficeEntry.reasonId,
        to: r.outOfOfficeReason.id,
      }),
    },
    outOfOfficeReason: {
      user: r.one.users({ from: r.outOfOfficeReason.userId, to: r.users.id }),
      entries: r.many.outOfOfficeEntry(),
    },
    userHolidaySettings: {
      user: r.one.users({ from: r.userHolidaySettings.userId, to: r.users.id, optional: false }),
    },
    platformOAuthClient: {
      users: r.many.users({
        from: r.platformOAuthClient.id.through(r.platformOauthClientToUser.A),
        to: r.users.id.through(r.platformOauthClientToUser.B),
      }),
      organization: r.one.team({
        from: r.platformOAuthClient.organizationId,
        to: r.team.id,
        optional: false,
        alias: 'PlatformOAuthClientToTeam',
      }),
      teams: r.many.team({ alias: 'CreatedByOAuthClient' }),
      accessTokens: r.many.accessToken(),
      refreshToken: r.many.refreshToken(),
      authorizationTokens: r.many.platformAuthorizationToken(),
      webhook: r.many.webhook(),
    },
    platformAuthorizationToken: {
      owner: r.one.users({
        from: r.platformAuthorizationToken.userId,
        to: r.users.id,
        optional: false,
      }),
      client: r.one.platformOAuthClient({
        from: r.platformAuthorizationToken.platformOAuthClientId,
        to: r.platformOAuthClient.id,
        optional: false,
      }),
    },
    accessToken: {
      owner: r.one.users({ from: r.accessToken.userId, to: r.users.id, optional: false }),
      client: r.one.platformOAuthClient({
        from: r.accessToken.platformOAuthClientId,
        to: r.platformOAuthClient.id,
        optional: false,
      }),
    },
    refreshToken: {
      owner: r.one.users({ from: r.refreshToken.userId, to: r.users.id, optional: false }),
      client: r.one.platformOAuthClient({
        from: r.refreshToken.platformOAuthClientId,
        to: r.platformOAuthClient.id,
        optional: false,
      }),
    },
    dSyncData: {
      org: r.one.organizationSettings({
        from: r.dSyncData.organizationId,
        to: r.organizationSettings.organizationId,
      }),
      teamGroupMapping: r.many.dSyncTeamGroupMapping(),
      createdAttributeToUsers: r.many.attributeToUser({ alias: 'createdByDSync' }),
      updatedAttributeToUsers: r.many.attributeToUser({ alias: 'updatedByDSync' }),
    },
    dSyncTeamGroupMapping: {
      team: r.one.team({ from: r.dSyncTeamGroupMapping.teamId, to: r.team.id, optional: false }),
      directory: r.one.dSyncData({
        from: r.dSyncTeamGroupMapping.directoryId,
        to: r.dSyncData.directoryId,
        optional: false,
      }),
    },
    secondaryEmail: {
      user: r.one.users({ from: r.secondaryEmail.userId, to: r.users.id, optional: false }),
      verificationTokens: r.many.verificationToken(),
      eventTypes: r.many.eventType(),
    },
    managedOrganization: {
      managedOrganization: r.one.team({
        from: r.managedOrganization.managedOrganizationId,
        to: r.team.id,
        optional: false,
        alias: 'ManagedOrganization',
      }),
      managerOrganization: r.one.team({
        from: r.managedOrganization.managerOrganizationId,
        to: r.team.id,
        optional: false,
        alias: 'ManagerOrganization',
      }),
    },
    platformBilling: {
      managerBilling: r.one.platformBilling({
        from: r.platformBilling.managerBillingId,
        to: r.platformBilling.id,
        alias: 'PlatformManagedBilling',
      }),
      managedBillings: r.many.platformBilling({ alias: 'PlatformManagedBilling' }),
      team: r.one.team({ from: r.platformBilling.id, to: r.team.id, optional: false }),
    },
    attributeOption: {
      attribute: r.one.attribute({
        from: r.attributeOption.attributeId,
        to: r.attribute.id,
        optional: false,
      }),
      assignedUsers: r.many.attributeToUser(),
    },
    attribute: {
      team: r.one.team({ from: r.attribute.teamId, to: r.team.id, optional: false }),
      options: r.many.attributeOption(),
      attributeSyncFieldMappings: r.many.attributeSyncFieldMapping(),
    },
    attributeToUser: {
      member: r.one.membership({
        from: r.attributeToUser.memberId,
        to: r.membership.id,
        optional: false,
      }),
      attributeOption: r.one.attributeOption({
        from: r.attributeToUser.attributeOptionId,
        to: r.attributeOption.id,
        optional: false,
      }),
      createdBy: r.one.users({
        from: r.attributeToUser.createdById,
        to: r.users.id,
        alias: 'createdBy',
      }),
      createdByDSync: r.one.dSyncData({
        from: r.attributeToUser.createdByDSyncId,
        to: r.dSyncData.directoryId,
        alias: 'createdByDSync',
      }),
      updatedBy: r.one.users({
        from: r.attributeToUser.updatedById,
        to: r.users.id,
        alias: 'updatedBy',
      }),
      updatedByDSync: r.one.dSyncData({
        from: r.attributeToUser.updatedByDSyncId,
        to: r.dSyncData.directoryId,
        alias: 'updatedByDSync',
      }),
    },
    assignmentReason: {
      booking: r.one.booking({
        from: r.assignmentReason.bookingId,
        to: r.booking.id,
        optional: false,
      }),
    },
    delegationCredential: {
      workspacePlatform: r.one.workspacePlatform({
        from: r.delegationCredential.workspacePlatformId,
        to: r.workspacePlatform.id,
        optional: false,
      }),
      organization: r.one.team({
        from: r.delegationCredential.organizationId,
        to: r.team.id,
        optional: false,
      }),
      selectedCalendars: r.many.selectedCalendar(),
      destinationCalendar: r.many.destinationCalendar(),
      bookingReferences: r.many.bookingReference(),
      credentials: r.many.credential(),
    },
    workspacePlatform: { delegationCredentials: r.many.delegationCredential() },
    eventTypeTranslation: {
      eventType: r.one.eventType({
        from: r.eventTypeTranslation.eventTypeId,
        to: r.eventType.id,
        optional: false,
      }),
      creator: r.one.users({
        from: r.eventTypeTranslation.createdBy,
        to: r.users.id,
        optional: false,
        alias: 'CreatedEventTypeTranslations',
      }),
      updater: r.one.users({
        from: r.eventTypeTranslation.updatedBy,
        to: r.users.id,
        alias: 'UpdatedEventTypeTranslations',
      }),
    },
    watchlist: {
      bookingReports: r.many.bookingReport({ alias: 'BookingReportToWatchlist' }),
      globalBookingReports: r.many.bookingReport({ alias: 'GlobalBlocklist' }),
      audits: r.many.watchlistAudit(),
    },
    watchlistAudit: {
      watchlist: r.one.watchlist({ from: r.watchlistAudit.watchlistId, to: r.watchlist.id }),
    },
    bookingReport: {
      booking: r.one.booking({
        from: r.bookingReport.bookingUid,
        to: r.booking.uid,
        optional: false,
      }),
      reportedBy: r.one.users({ from: r.bookingReport.reportedById, to: r.users.id }),
      organization: r.one.team({ from: r.bookingReport.organizationId, to: r.team.id }),
      watchlist: r.one.watchlist({
        from: r.bookingReport.watchlistId,
        to: r.watchlist.id,
        alias: 'BookingReportToWatchlist',
      }),
      globalWatchlist: r.one.watchlist({
        from: r.bookingReport.globalWatchlistId,
        to: r.watchlist.id,
        alias: 'GlobalBlocklist',
      }),
    },
    wrongAssignmentReport: {
      booking: r.one.booking({
        from: r.wrongAssignmentReport.bookingUid,
        to: r.booking.uid,
        optional: false,
      }),
      reportedBy: r.one.users({
        from: r.wrongAssignmentReport.reportedById,
        to: r.users.id,
        alias: 'WrongAssignmentReportedBy',
      }),
      team: r.one.team({ from: r.wrongAssignmentReport.teamId, to: r.team.id }),
      reviewedBy: r.one.users({
        from: r.wrongAssignmentReport.reviewedById,
        to: r.users.id,
        alias: 'WrongAssignmentReviewedBy',
      }),
    },
    organizationOnboarding: {
      createdBy: r.one.users({
        from: r.organizationOnboarding.createdById,
        to: r.users.id,
        optional: false,
      }),
      organization: r.one.team({ from: r.organizationOnboarding.organizationId, to: r.team.id }),
    },
    internalNotePreset: {
      team: r.one.team({ from: r.internalNotePreset.teamId, to: r.team.id, optional: false }),
      BookingInternalNote: r.many.bookingInternalNote(),
    },
    filterSegment: {
      user: r.one.users({ from: r.filterSegment.userId, to: r.users.id, optional: false }),
      team: r.one.team({ from: r.filterSegment.teamId, to: r.team.id }),
      userPreferences: r.many.userFilterSegmentPreference(),
    },
    userFilterSegmentPreference: {
      user: r.one.users({
        from: r.userFilterSegmentPreference.userId,
        to: r.users.id,
        optional: false,
      }),
      segment: r.one.filterSegment({
        from: r.userFilterSegmentPreference.segmentId,
        to: r.filterSegment.id,
      }),
    },
    bookingInternalNote: {
      notePreset: r.one.internalNotePreset({
        from: r.bookingInternalNote.notePresetId,
        to: r.internalNotePreset.id,
      }),
      booking: r.one.booking({
        from: r.bookingInternalNote.bookingId,
        to: r.booking.id,
        optional: false,
      }),
      createdBy: r.one.users({
        from: r.bookingInternalNote.createdById,
        to: r.users.id,
        optional: false,
      }),
    },
    role: {
      team: r.one.team({ from: r.role.teamId, to: r.team.id }),
      permissions: r.many.rolePermission(),
      memberships: r.many.membership(),
    },
    rolePermission: {
      role: r.one.role({ from: r.rolePermission.roleId, to: r.role.id, optional: false }),
    },
    auditActor: { bookingAudits: r.many.bookingAudit() },
    bookingAudit: {
      actor: r.one.auditActor({
        from: r.bookingAudit.actorId,
        to: r.auditActor.id,
        optional: false,
      }),
    },
    agent: {
      user: r.one.users({ from: r.agent.userId, to: r.users.id }),
      team: r.one.team({ from: r.agent.teamId, to: r.team.id }),
      inboundPhoneNumbers: r.many.calAiPhoneNumber({ alias: 'InboundAgent' }),
      outboundPhoneNumbers: r.many.calAiPhoneNumber({ alias: 'OutboundAgent' }),
    },
    calAiPhoneNumber: {
      user: r.one.users({ from: r.calAiPhoneNumber.userId, to: r.users.id }),
      team: r.one.team({ from: r.calAiPhoneNumber.teamId, to: r.team.id }),
      inboundAgent: r.one.agent({
        from: r.calAiPhoneNumber.inboundAgentId,
        to: r.agent.id,
        alias: 'InboundAgent',
      }),
      outboundAgent: r.one.agent({
        from: r.calAiPhoneNumber.outboundAgentId,
        to: r.agent.id,
        alias: 'OutboundAgent',
      }),
    },
    teamBilling: {
      team: r.one.team({ from: r.teamBilling.teamId, to: r.team.id }),
      monthlyProrations: r.many.monthlyProration(),
      seatChangeLogs: r.many.seatChangeLog(),
    },
    organizationBilling: {
      team: r.one.team({ from: r.organizationBilling.teamId, to: r.team.id, optional: false }),
      monthlyProrations: r.many.monthlyProration(),
      seatChangeLogs: r.many.seatChangeLog(),
    },
    seatChangeLog: {
      team: r.one.team({ from: r.seatChangeLog.teamId, to: r.team.id, optional: false }),
      proration: r.one.monthlyProration({
        from: r.seatChangeLog.processedInProrationId,
        to: r.monthlyProration.id,
      }),
      teamBilling: r.one.teamBilling({ from: r.seatChangeLog.teamBillingId, to: r.teamBilling.id }),
      organizationBilling: r.one.organizationBilling({
        from: r.seatChangeLog.organizationBillingId,
        to: r.organizationBilling.id,
      }),
    },
    monthlyProration: {
      team: r.one.team({ from: r.monthlyProration.teamId, to: r.team.id, optional: false }),
      seatChanges: r.many.seatChangeLog(),
      teamBilling: r.one.teamBilling({
        from: r.monthlyProration.teamBillingId,
        to: r.teamBilling.id,
      }),
      organizationBilling: r.one.organizationBilling({
        from: r.monthlyProration.organizationBillingId,
        to: r.organizationBilling.id,
      }),
    },
    calendarCacheEvent: {
      selectedCalendar: r.one.selectedCalendar({
        from: r.calendarCacheEvent.selectedCalendarId,
        to: r.selectedCalendar.id,
        optional: false,
      }),
    },
    integrationAttributeSync: {
      organization: r.one.team({
        from: r.integrationAttributeSync.organizationId,
        to: r.team.id,
        optional: false,
      }),
      credential: r.one.credential({
        from: r.integrationAttributeSync.credentialId,
        to: r.credential.id,
      }),
      attributeSyncRule: r.one.attributeSyncRule({
        from: r.integrationAttributeSync.id,
        to: r.attributeSyncRule.integrationAttributeSyncId,
      }),
      syncFieldMappings: r.many.attributeSyncFieldMapping(),
    },
    attributeSyncRule: {
      integrationAttributeSync: r.one.integrationAttributeSync({
        from: r.attributeSyncRule.integrationAttributeSyncId,
        to: r.integrationAttributeSync.id,
        optional: false,
      }),
    },
    attributeSyncFieldMapping: {
      attribute: r.one.attribute({
        from: r.attributeSyncFieldMapping.attributeId,
        to: r.attribute.id,
        optional: false,
      }),
      integrationAttributeSync: r.one.integrationAttributeSync({
        from: r.attributeSyncFieldMapping.integrationAttributeSyncId,
        to: r.integrationAttributeSync.id,
        optional: false,
      }),
    },
    userEventtype: {
      eventType: r.one.eventType({ from: r.userEventtype.A, to: r.eventType.id, optional: false }),
      user: r.one.users({ from: r.userEventtype.B, to: r.users.id, optional: false }),
    },
    platformOauthClientToUser: {
      platformOauthClient: r.one.platformOAuthClient({
        from: r.platformOauthClientToUser.A,
        to: r.platformOAuthClient.id,
        optional: false,
      }),
      user: r.one.users({ from: r.platformOauthClientToUser.B, to: r.users.id, optional: false }),
    },
  }),
)
