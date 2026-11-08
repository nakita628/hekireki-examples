import {
  boolean,
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

export const signupIntentEnum = pgEnum('SignupIntent', ['AI_ASSISTANT', 'MAIL_CLIENT', 'BOTH'])

export const apiKeyScopeEnum = pgEnum('ApiKeyScope', [
  'STATS_READ',
  'RULES_READ',
  'RULES_WRITE',
  'SETTINGS_READ',
  'SETTINGS_WRITE',
  'ASSISTANT_CHAT',
  'SENDERS_UNSUBSCRIBE',
])

export const scheduledEmailStatusEnum = pgEnum('ScheduledEmailStatus', [
  'PENDING',
  'PROCESSING',
  'SENT',
  'BLOCKED_AUTH',
  'UNCERTAIN',
  'FAILED',
  'CANCELLED',
])

export const emailReminderStatusEnum = pgEnum('EmailReminderStatus', [
  'NONE',
  'PENDING',
  'PROCESSING',
  'COMPLETED',
  'CANCELLED',
])

export const meetingJoinRuleEnum = pgEnum('MeetingJoinRule', [
  'ALL',
  'EXTERNAL_ONLY',
  'HOST_ONLY',
  'OFF',
])

export const meetingRecordingStatusEnum = pgEnum('MeetingRecordingStatus', [
  'PENDING',
  'SCHEDULED',
  'CANCELLING',
  'JOINING',
  'IN_WAITING_ROOM',
  'IN_CALL',
  'RECORDING',
  'CALL_ENDED',
  'DONE',
  'FAILED',
  'CANCELLED',
])

export const meetingProcessingStatusEnum = pgEnum('MeetingProcessingStatus', [
  'PENDING',
  'PROCESSING',
  'COMPLETED',
  'FAILED',
])

export const mcpAuthTypeEnum = pgEnum('McpAuthType', ['OAUTH', 'API_TOKEN', 'NONE'])

export const mobilePushPlatformEnum = pgEnum('MobilePushPlatform', ['android', 'ios'])

export const mobilePushTokenTypeEnum = pgEnum('MobilePushTokenType', ['expo', 'apns', 'fcm'])

export const actionTypeEnum = pgEnum('ActionType', [
  'ARCHIVE',
  'LABEL',
  'REPLY',
  'SEND_EMAIL',
  'FORWARD',
  'DRAFT_EMAIL',
  'DRAFT_MESSAGING_CHANNEL',
  'NOTIFY_MESSAGING_CHANNEL',
  'MARK_SPAM',
  'CALL_WEBHOOK',
  'MARK_READ',
  'STAR',
  'DELETE',
  'DIGEST',
  'MOVE_FOLDER',
  'NOTIFY_SENDER',
  'INTEGRATION',
])

export const messagingMessageStatusEnum = pgEnum('MessagingMessageStatus', [
  'SENT',
  'DRAFT_SENT',
  'DRAFT_EDITED',
  'DISMISSED',
  'EXPIRED',
  'FAILED',
])

export const frequencyEnum = pgEnum('Frequency', ['NEVER', 'DAILY', 'WEEKLY'])

export const mailLayoutEnum = pgEnum('MailLayout', ['LIST', 'SPLIT'])

export const mailSplitFilterKindEnum = pgEnum('MailSplitFilterKind', [
  'UNREAD',
  'STARRED',
  'LABEL',
  'CATEGORY',
  'FROM',
  'OLDER_THAN',
])

export const draftReplyConfidenceEnum = pgEnum('DraftReplyConfidence', [
  'ALL_EMAILS',
  'STANDARD',
  'HIGH_CONFIDENCE',
])

export const draftEmailStatusEnum = pgEnum('DraftEmailStatus', [
  'PENDING',
  'LIKELY_SENT',
  'REPLIED_WITHOUT_DRAFT',
  'CLEANED_UP_UNUSED',
  'MISSING_FROM_PROVIDER',
  'USER_EDITED',
  'CLEANUP_SKIPPED',
])

export const replyMemoryKindEnum = pgEnum('ReplyMemoryKind', ['FACT', 'PREFERENCE', 'PROCEDURE'])

export const replyMemoryScopeTypeEnum = pgEnum('ReplyMemoryScopeType', [
  'GLOBAL',
  'SENDER',
  'DOMAIN',
  'TOPIC',
])

export const newsletterStatusEnum = pgEnum('NewsletterStatus', [
  'APPROVED',
  'UNSUBSCRIBED',
  'AUTO_ARCHIVED',
])

export const coldEmailStatusEnum = pgEnum('ColdEmailStatus', [
  'AI_LABELED_COLD',
  'USER_REJECTED_COLD',
])

export const documentFilingStatusEnum = pgEnum('DocumentFilingStatus', [
  'PROCESSING',
  'PENDING',
  'FILED',
  'REJECTED',
  'ERROR',
  'PREVIEW',
])

export const attachmentSourceTypeEnum = pgEnum('AttachmentSourceType', ['FILE', 'FOLDER'])

export const coldEmailSettingEnum = pgEnum('ColdEmailSetting', [
  'DISABLED',
  'LIST',
  'LABEL',
  'ARCHIVE_AND_LABEL',
  'ARCHIVE_AND_READ_AND_LABEL',
])

export const premiumTierEnum = pgEnum('PremiumTier', [
  'BASIC_MONTHLY',
  'BASIC_ANNUALLY',
  'PRO_MONTHLY',
  'PRO_ANNUALLY',
  'BUSINESS_MONTHLY',
  'BUSINESS_ANNUALLY',
  'PLUS_MONTHLY',
  'PLUS_ANNUALLY',
  'BUSINESS_PLUS_MONTHLY',
  'BUSINESS_PLUS_ANNUALLY',
  'COPILOT_MONTHLY',
  'LIFETIME',
])

export const executedRuleStatusEnum = pgEnum('ExecutedRuleStatus', [
  'APPLIED',
  'APPLYING',
  'REJECTED',
  'PENDING',
  'SKIPPED',
  'ERROR',
])

export const executedActionStatusEnum = pgEnum('ExecutedActionStatus', [
  'SUCCEEDED',
  'FAILED',
  'SKIPPED',
])

export const groupItemTypeEnum = pgEnum('GroupItemType', ['FROM', 'SUBJECT', 'BODY'])

export const categoryFilterTypeEnum = pgEnum('CategoryFilterType', ['INCLUDE', 'EXCLUDE'])

export const logicalOperatorEnum = pgEnum('LogicalOperator', ['AND', 'OR'])

export const threadTrackerTypeEnum = pgEnum('ThreadTrackerType', [
  'AWAITING',
  'NEEDS_REPLY',
  'NEEDS_ACTION',
])

export const processorTypeEnum = pgEnum('ProcessorType', ['LEMON_SQUEEZY', 'STRIPE'])

export const cleanActionEnum = pgEnum('CleanAction', ['ARCHIVE', 'MARK_READ'])

export const systemTypeEnum = pgEnum('SystemType', [
  'TO_REPLY',
  'FYI',
  'AWAITING_REPLY',
  'ACTIONED',
  'COLD_EMAIL',
  'NEWSLETTER',
  'MARKETING',
  'CALENDAR',
  'RECEIPT',
  'NOTIFICATION',
  'OTP',
])

export const referralStatusEnum = pgEnum('ReferralStatus', ['PENDING', 'COMPLETED'])

export const digestStatusEnum = pgEnum('DigestStatus', ['PENDING', 'PROCESSING', 'SENT', 'FAILED'])

export const scheduledActionStatusEnum = pgEnum('ScheduledActionStatus', [
  'PENDING',
  'EXECUTING',
  'COMPLETED',
  'FAILED',
  'CANCELLED',
])

export const snoozedThreadStatusEnum = pgEnum('SnoozedThreadStatus', [
  'PREPARING',
  'PENDING',
  'EXECUTING',
  'COMPLETED',
  'CANCELLED',
])

export const emailSendOperationStatusEnum = pgEnum('EmailSendOperationStatus', [
  'PROCESSING',
  'SENT',
  'UNCERTAIN',
])

export const schedulingStatusEnum = pgEnum('SchedulingStatus', ['PENDING', 'SCHEDULED', 'FAILED'])

export const meetingBriefingStatusEnum = pgEnum('MeetingBriefingStatus', [
  'PENDING',
  'SENT',
  'FAILED',
  'SKIPPED',
])

export const bookingLinkLocationTypeEnum = pgEnum('BookingLinkLocationType', [
  'IN_PERSON',
  'GOOGLE_MEET',
  'MICROSOFT_TEAMS',
  'PHONE',
  'CUSTOM',
])

export const bookingStatusEnum = pgEnum('BookingStatus', [
  'PENDING_PROVIDER_EVENT',
  'CONFIRMED',
  'CANCELED',
  'FAILED',
])

export const groupItemSourceEnum = pgEnum('GroupItemSource', [
  'AI',
  'USER',
  'LABEL_REMOVED',
  'LABEL_ADDED',
])

export const classificationFeedbackEventTypeEnum = pgEnum('ClassificationFeedbackEventType', [
  'LABEL_ADDED',
  'LABEL_REMOVED',
])

export const messagingProviderEnum = pgEnum('MessagingProvider', ['SLACK', 'TEAMS', 'TELEGRAM'])

export const messagingRoutePurposeEnum = pgEnum('MessagingRoutePurpose', [
  'RULE_NOTIFICATIONS',
  'SCHEDULED_CHECK_INS',
  'MEETING_BRIEFS',
  'DOCUMENT_FILINGS',
  'DIGESTS',
  'FOLLOW_UPS',
])

export const messagingRouteTargetTypeEnum = pgEnum('MessagingRouteTargetType', [
  'CHANNEL',
  'DIRECT_MESSAGE',
])

export const automationJobRunStatusEnum = pgEnum('AutomationJobRunStatus', [
  'PENDING',
  'RUNNING',
  'SENT',
  'SKIPPED',
  'FAILED',
])

export const conversationStatusEnum = pgEnum('ConversationStatus', ['ACTIVE', 'STOPPED'])

export const conversationActivityKindEnum = pgEnum('ConversationActivityKind', [
  'INVITED',
  'COMMENT',
  'MENTION',
])

export const account = pgTable(
  'Account',
  {
    password: text('password'),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
    provider: text('provider').notNull(),
    type: text('type').notNull().default('oidc'),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    refreshTokenExpiresAt: timestamp('refreshTokenExpiresAt', { precision: 3 }),
    access_token: text('access_token'),
    expires_at: timestamp('expires_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    disconnectedAt: timestamp('disconnectedAt', { precision: 3 }),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
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

export const session = pgTable(
  'Session',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    emailOtp: boolean('emailOtp').notNull().default(false),
    emailOtpVersion: integer('emailOtpVersion').notNull().default(0),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    activeOrganizationId: text('activeOrganizationId'),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    index('Session_activeOrganizationId_idx').on(table.activeOrganizationId),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Session_activeOrganizationId_fkey',
      columns: [table.activeOrganizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name'),
    email: text('email').notNull(),
    emailVerified: boolean('emailVerified').default(false),
    image: text('image'),
    completedOnboardingAt: timestamp('completedOnboardingAt', { precision: 3 }),
    signupIntent: signupIntentEnum('signupIntent'),
    onboardingAnswers: jsonb('onboardingAnswers'),
    lastLogin: timestamp('lastLogin', { precision: 3 }),
    utms: jsonb('utms'),
    errorMessages: jsonb('errorMessages'),
    announcementDismissedAt: timestamp('announcementDismissedAt', { precision: 3 }),
    dismissedHints: text('dismissedHints').array().default([]),
    surveyFeatures: text('surveyFeatures').array(),
    surveyRole: text('surveyRole'),
    surveyGoal: text('surveyGoal'),
    surveyCompanySize: integer('surveyCompanySize'),
    surveySource: text('surveySource'),
    surveyImprovements: text('surveyImprovements'),
    mcpServerEnabled: boolean('mcpServerEnabled').notNull().default(false),
    mcpTokenVersion: integer('mcpTokenVersion').notNull().default(0),
    emailOtpEnabled: boolean('emailOtpEnabled').notNull().default(false),
    emailOtpVersion: integer('emailOtpVersion').notNull().default(0),
    aiProvider: text('aiProvider'),
    aiModel: text('aiModel'),
    aiApiKey: text('aiApiKey'),
    decisionModelEnabled: boolean('decisionModelEnabled'),
    webhookSecret: text('webhookSecret'),
    referralCode: text('referralCode'),
    premiumId: text('premiumId'),
    premiumAdminId: text('premiumAdminId'),
    scimAccessDisabled: boolean('scimAccessDisabled').notNull().default(false),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    uniqueIndex('User_referralCode_key').on(table.referralCode),
    foreignKey({
      name: 'User_premiumId_fkey',
      columns: [table.premiumId],
      foreignColumns: [premium.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'User_premiumAdminId_fkey',
      columns: [table.premiumAdminId],
      foreignColumns: [premium.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const emailAccount = pgTable(
  'EmailAccount',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    email: text('email').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name'),
    image: text('image'),
    sortOrder: integer('sortOrder'),
    about: text('about'),
    writingStyle: text('writingStyle'),
    signature: text('signature'),
    sentSignatureScan: jsonb('sentSignatureScan'),
    sentSignatureScannedAt: timestamp('sentSignatureScannedAt', { precision: 3 }),
    includeReferralSignature: boolean('includeReferralSignature').notNull().default(false),
    includeSentWithSignature: boolean('includeSentWithSignature').notNull().default(true),
    sentMessageOpenTrackingEnabled: boolean('sentMessageOpenTrackingEnabled')
      .notNull()
      .default(true),
    watchEmailsExpirationDate: timestamp('watchEmailsExpirationDate', { precision: 3 }),
    watchEmailsSubscriptionId: text('watchEmailsSubscriptionId'),
    watchEmailsSubscriptionHistory: jsonb('watchEmailsSubscriptionHistory'),
    lastSyncedHistoryId: text('lastSyncedHistoryId'),
    behaviorProfile: jsonb('behaviorProfile'),
    personaAnalysis: jsonb('personaAnalysis'),
    role: text('role'),
    timezone: text('timezone'),
    calendarBookingLink: text('calendarBookingLink'),
    draftDocsUrls: text('draftDocsUrls').array().default([]),
    statsEmailFrequency: frequencyEnum('statsEmailFrequency').notNull().default('WEEKLY'),
    summaryEmailFrequency: frequencyEnum('summaryEmailFrequency').notNull().default('WEEKLY'),
    lastSummaryEmailAt: timestamp('lastSummaryEmailAt', { precision: 3 }),
    lastInboxHealthEmailAt: timestamp('lastInboxHealthEmailAt', { precision: 3 }),
    coldEmailBlocker: coldEmailSettingEnum('coldEmailBlocker'),
    coldEmailDigest: boolean('coldEmailDigest').notNull().default(false),
    coldEmailPrompt: text('coldEmailPrompt'),
    rulesPrompt: text('rulesPrompt'),
    autoCategorizeSenders: boolean('autoCategorizeSenders').notNull().default(false),
    multiRuleSelectionEnabled: boolean('multiRuleSelectionEnabled').notNull().default(false),
    sensitiveDataPolicy: text('sensitiveDataPolicy'),
    draftReplyConfidence: draftReplyConfidenceEnum('draftReplyConfidence')
      .notNull()
      .default('ALL_EMAILS'),
    draftCleanupDays: integer('draftCleanupDays').default(14),
    allowHiddenAiDraftLinks: boolean('allowHiddenAiDraftLinks').notNull().default(false),
    rulesRevision: integer('rulesRevision').notNull().default(0),
    mailLayout: mailLayoutEnum('mailLayout').notNull().default('LIST'),
    mailExpandedPreview: boolean('mailExpandedPreview').notNull().default(false),
    mailBundleInbox: boolean('mailBundleInbox'),
    includeInAllAccounts: boolean('includeInAllAccounts').notNull().default(true),
    meetingBriefingsEnabled: boolean('meetingBriefingsEnabled').notNull().default(false),
    meetingBriefingsMinutesBefore: integer('meetingBriefingsMinutesBefore').notNull().default(240),
    meetingBriefsSendEmail: boolean('meetingBriefsSendEmail').notNull().default(true),
    meetingRecorderEnabled: boolean('meetingRecorderEnabled').notNull().default(false),
    meetingRecorderJoinRule: meetingJoinRuleEnum('meetingRecorderJoinRule')
      .notNull()
      .default('ALL'),
    meetingRecorderRecapEmailEnabled: boolean('meetingRecorderRecapEmailEnabled')
      .notNull()
      .default(true),
    meetingRecorderFollowUpDraftEnabled: boolean('meetingRecorderFollowUpDraftEnabled')
      .notNull()
      .default(true),
    filingEnabled: boolean('filingEnabled').notNull().default(false),
    filingPrompt: text('filingPrompt'),
    filingConfirmationSendEmail: boolean('filingConfirmationSendEmail').notNull().default(true),
    followUpAwaitingReplyDays: doublePrecision('followUpAwaitingReplyDays'),
    followUpNeedsReplyDays: doublePrecision('followUpNeedsReplyDays'),
    followUpAutoDraftEnabled: boolean('followUpAutoDraftEnabled').notNull().default(true),
    digestSendEmail: boolean('digestSendEmail').notNull().default(true),
    learnedWritingStyle: text('learnedWritingStyle'),
    userId: text('userId').notNull(),
    accountId: text('accountId').notNull(),
  },
  (table) => [
    uniqueIndex('EmailAccount_email_key').on(table.email),
    uniqueIndex('EmailAccount_accountId_key').on(table.accountId),
    index('EmailAccount_userId_idx').on(table.userId),
    index('EmailAccount_lastSummaryEmailAt_idx').on(table.lastSummaryEmailAt),
    index('EmailAccount_lastInboxHealthEmailAt_idx').on(table.lastInboxHealthEmailAt),
    index('EmailAccount_watchEmailsSubscriptionId_idx').on(table.watchEmailsSubscriptionId),
    index('EmailAccount_watchEmailsSubscriptionHistory_idx').on(
      table.watchEmailsSubscriptionHistory,
    ),
    foreignKey({
      name: 'EmailAccount_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EmailAccount_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [account.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mailSplit = pgTable(
  'MailSplit',
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
    order: integer('order').notNull(),
    matchAll: boolean('matchAll').notNull().default(true),
    bundleInInbox: boolean('bundleInInbox').notNull().default(false),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('MailSplit_emailAccountId_order_idx').on(table.emailAccountId, table.order),
    uniqueIndex('MailSplit_emailAccountId_name_key').on(table.emailAccountId, table.name),
    foreignKey({
      name: 'MailSplit_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mailSplitFilter = pgTable(
  'MailSplitFilter',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    kind: mailSplitFilterKindEnum('kind').notNull(),
    value: text('value'),
    order: integer('order').notNull(),
    mailSplitId: text('mailSplitId').notNull(),
  },
  (table) => [
    index('MailSplitFilter_mailSplitId_idx').on(table.mailSplitId),
    index('MailSplitFilter_kind_value_idx').on(table.kind, table.value),
    uniqueIndex('MailSplitFilter_mailSplitId_order_key').on(table.mailSplitId, table.order),
    foreignKey({
      name: 'MailSplitFilter_mailSplitId_fkey',
      columns: [table.mailSplitId],
      foreignColumns: [mailSplit.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organization = pgTable(
  'Organization',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    logo: text('logo'),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('Organization_slug_key').on(table.slug)],
)

export const organizationRule = pgTable(
  'OrganizationRule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationId: text('organizationId').notNull(),
    enabled: boolean('enabled').notNull().default(true),
    name: text('name').notNull(),
    instructions: text('instructions'),
    runOnThreads: boolean('runOnThreads').notNull().default(false),
    conditionalOperator: logicalOperatorEnum('conditionalOperator').notNull().default('AND'),
    from: text('from'),
    to: text('to'),
    subject: text('subject'),
    body: text('body'),
  },
  (table) => [
    index('OrganizationRule_organizationId_enabled_idx').on(table.organizationId, table.enabled),
    uniqueIndex('OrganizationRule_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'OrganizationRule_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organizationRuleAction = pgTable(
  'OrganizationRuleAction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationRuleId: text('organizationRuleId').notNull(),
    type: actionTypeEnum('type').notNull(),
    label: text('label'),
    subject: text('subject'),
    content: text('content'),
    to: text('to'),
    cc: text('cc'),
    bcc: text('bcc'),
    url: text('url'),
    folderName: text('folderName'),
    delayInMinutes: integer('delayInMinutes'),
    staticAttachments: jsonb('staticAttachments'),
  },
  (table) => [
    index('OrganizationRuleAction_organizationRuleId_idx').on(table.organizationRuleId),
    foreignKey({
      name: 'OrganizationRuleAction_organizationRuleId_fkey',
      columns: [table.organizationRuleId],
      foreignColumns: [organizationRule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const member = pgTable(
  'Member',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    organizationId: text('organizationId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
    role: text('role').notNull().default('member'),
    allowOrgAdminAnalytics: boolean('allowOrgAdminAnalytics').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Member_organizationId_emailAccountId_key').on(
      table.organizationId,
      table.emailAccountId,
    ),
    uniqueIndex('Member_emailAccountId_key').on(table.emailAccountId),
    foreignKey({
      name: 'Member_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Member_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const conversation = pgTable(
  'Conversation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationId: text('organizationId').notNull(),
    publisherId: text('publisherId').notNull(),
    threadId: text('threadId').notNull(),
    status: conversationStatusEnum('status').notNull().default('ACTIVE'),
    revision: integer('revision').notNull().default(0),
    generation: integer('generation').notNull().default(1),
  },
  (table) => [
    index('Conversation_organizationId_idx').on(table.organizationId),
    uniqueIndex('Conversation_publisherId_threadId_key').on(table.publisherId, table.threadId),
    foreignKey({
      name: 'Conversation_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_publisherId_fkey',
      columns: [table.publisherId],
      foreignColumns: [member.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const conversationParticipant = pgTable(
  'ConversationParticipant',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    conversationId: text('conversationId').notNull(),
    memberId: text('memberId').notNull(),
    generation: integer('generation').notNull(),
    active: boolean('active').notNull().default(true),
    readRevision: integer('readRevision').notNull().default(0),
    muted: boolean('muted').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ConversationParticipant_memberId_idx').on(table.memberId),
    uniqueIndex('ConversationParticipant_conversationId_memberId_generation_key').on(
      table.conversationId,
      table.memberId,
      table.generation,
    ),
    foreignKey({
      name: 'ConversationParticipant_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationParticipant_memberId_fkey',
      columns: [table.memberId],
      foreignColumns: [member.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const conversationComment = pgTable(
  'ConversationComment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    conversationId: text('conversationId').notNull(),
    authorMemberId: text('authorMemberId'),
    clientMutationId: text('clientMutationId').notNull(),
    body: text('body').notNull(),
    revision: integer('revision').notNull(),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('ConversationComment_conversationId_revision_key').on(
      table.conversationId,
      table.revision,
    ),
    uniqueIndex('ConversationComment_conversationId_authorMemberId_clientMut_key').on(
      table.conversationId,
      table.authorMemberId,
      table.clientMutationId,
    ),
    foreignKey({
      name: 'ConversationComment_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationComment_authorMemberId_fkey',
      columns: [table.authorMemberId],
      foreignColumns: [member.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const conversationActivity = pgTable(
  'ConversationActivity',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    participantId: text('participantId').notNull(),
    commentId: text('commentId'),
    kind: conversationActivityKindEnum('kind').notNull(),
    revision: integer('revision').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ConversationActivity_participantId_createdAt_id_idx').on(
      table.participantId,
      table.createdAt,
      table.id,
    ),
    uniqueIndex('ConversationActivity_participantId_kind_revision_key').on(
      table.participantId,
      table.kind,
      table.revision,
    ),
    foreignKey({
      name: 'ConversationActivity_participantId_fkey',
      columns: [table.participantId],
      foreignColumns: [conversationParticipant.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationActivity_commentId_fkey',
      columns: [table.commentId],
      foreignColumns: [conversationComment.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const invitation = pgTable(
  'invitation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    organizationId: text('organizationId').notNull(),
    email: text('email').notNull(),
    role: text('role'),
    status: text('status').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    inviterId: text('inviterId').notNull(),
  },
  (table) => [
    index('invitation_organizationId_idx').on(table.organizationId),
    index('invitation_inviterId_idx').on(table.inviterId),
    foreignKey({
      name: 'invitation_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'invitation_inviterId_fkey',
      columns: [table.inviterId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verification = pgTable('verification', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  identifier: text('identifier').notNull(),
  value: text('value').notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const ssoProvider = pgTable(
  'ssoProvider',
  {
    userId: text('userId'),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    issuer: text('issuer').notNull(),
    oidcConfig: text('oidcConfig'),
    samlConfig: text('samlConfig'),
    emailAccountId: text('emailAccountId'),
    providerId: text('providerId').notNull(),
    organizationId: text('organizationId'),
    domain: text('domain').notNull(),
  },
  (table) => [
    uniqueIndex('ssoProvider_providerId_key').on(table.providerId),
    uniqueIndex('ssoProvider_organizationId_key').on(table.organizationId),
    index('ssoProvider_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'ssoProvider_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ssoProvider_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ssoProvider_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const digest = pgTable(
  'Digest',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    emailAccountId: text('emailAccountId').notNull(),
    sentAt: timestamp('sentAt', { precision: 3 }),
    status: digestStatusEnum('status').notNull().default('PENDING'),
  },
  (table) => [
    index('Digest_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'Digest_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const digestItem = pgTable(
  'DigestItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    messageId: text('messageId').notNull(),
    threadId: text('threadId').notNull(),
    content: text('content').notNull(),
    digestId: text('digestId').notNull(),
    actionId: text('actionId'),
    coldEmailId: text('coldEmailId'),
  },
  (table) => [
    index('DigestItem_actionId_idx').on(table.actionId),
    index('DigestItem_coldEmailId_idx').on(table.coldEmailId),
    uniqueIndex('DigestItem_digestId_threadId_messageId_key').on(
      table.digestId,
      table.threadId,
      table.messageId,
    ),
    foreignKey({
      name: 'DigestItem_digestId_fkey',
      columns: [table.digestId],
      foreignColumns: [digest.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DigestItem_actionId_fkey',
      columns: [table.actionId],
      foreignColumns: [executedAction.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DigestItem_coldEmailId_fkey',
      columns: [table.coldEmailId],
      foreignColumns: [coldEmail.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const schedule = pgTable(
  'Schedule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    intervalDays: integer('intervalDays'),
    occurrences: integer('occurrences'),
    daysOfWeek: integer('daysOfWeek'),
    timeOfDay: timestamp('timeOfDay', { precision: 3 }),
    emailAccountId: text('emailAccountId').notNull(),
    lastOccurrenceAt: timestamp('lastOccurrenceAt', { precision: 3 }),
    nextOccurrenceAt: timestamp('nextOccurrenceAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Schedule_emailAccountId_key').on(table.emailAccountId),
    foreignKey({
      name: 'Schedule_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const premium = pgTable(
  'Premium',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    pendingInvites: text('pendingInvites').array(),
    lemonSqueezyRenewsAt: timestamp('lemonSqueezyRenewsAt', { precision: 3 }),
    lemonSqueezyCustomerId: integer('lemonSqueezyCustomerId'),
    lemonSqueezySubscriptionId: integer('lemonSqueezySubscriptionId'),
    lemonSqueezySubscriptionItemId: integer('lemonSqueezySubscriptionItemId'),
    lemonSqueezyOrderId: integer('lemonSqueezyOrderId'),
    lemonSqueezyProductId: integer('lemonSqueezyProductId'),
    lemonSqueezyVariantId: integer('lemonSqueezyVariantId'),
    lemonLicenseKey: text('lemonLicenseKey'),
    lemonLicenseInstanceId: text('lemonLicenseInstanceId'),
    lemonSubscriptionStatus: text('lemonSubscriptionStatus'),
    stripeCustomerId: text('stripeCustomerId'),
    stripeSubscriptionId: text('stripeSubscriptionId'),
    stripeSubscriptionItemId: text('stripeSubscriptionItemId'),
    stripePriceId: text('stripePriceId'),
    stripeProductId: text('stripeProductId'),
    stripeSubscriptionStatus: text('stripeSubscriptionStatus'),
    stripeCancelAt: timestamp('stripeCancelAt', { precision: 3 }),
    stripeCancelAtPeriodEnd: boolean('stripeCancelAtPeriodEnd'),
    stripeRenewsAt: timestamp('stripeRenewsAt', { precision: 3 }),
    stripeTrialEnd: timestamp('stripeTrialEnd', { precision: 3 }),
    stripeTrialConversionInvoiceId: text('stripeTrialConversionInvoiceId'),
    stripeTrialConvertedAt: timestamp('stripeTrialConvertedAt', { precision: 3 }),
    stripeCancellationInitiatedAt: timestamp('stripeCancellationInitiatedAt', { precision: 3 }),
    stripeCanceledAt: timestamp('stripeCanceledAt', { precision: 3 }),
    stripeEndedAt: timestamp('stripeEndedAt', { precision: 3 }),
    stripeCancellationFeedback: text('stripeCancellationFeedback'),
    stripeCancellationComment: text('stripeCancellationComment'),
    stripeAiOverageLastInvoiceId: text('stripeAiOverageLastInvoiceId'),
    stripeAiOverageLastPeriodEnd: timestamp('stripeAiOverageLastPeriodEnd', { precision: 3 }),
    stripeAiOverageLastUnits: integer('stripeAiOverageLastUnits'),
    stripeInvoiceEmailsEnabled: boolean('stripeInvoiceEmailsEnabled').notNull().default(false),
    appleOriginalTransactionId: text('appleOriginalTransactionId'),
    appleLatestTransactionId: text('appleLatestTransactionId'),
    appleProductId: text('appleProductId'),
    appleSubscriptionGroupIdentifier: text('appleSubscriptionGroupIdentifier'),
    appleAppAccountToken: text('appleAppAccountToken'),
    appleSubscriptionStatus: text('appleSubscriptionStatus'),
    appleEnvironment: text('appleEnvironment'),
    applePurchaseDate: timestamp('applePurchaseDate', { precision: 3 }),
    appleExpiresAt: timestamp('appleExpiresAt', { precision: 3 }),
    appleRevokedAt: timestamp('appleRevokedAt', { precision: 3 }),
    appleOfferDiscountType: text('appleOfferDiscountType'),
    adminGrantExpiresAt: timestamp('adminGrantExpiresAt', { precision: 3 }),
    adminGrantTier: premiumTierEnum('adminGrantTier'),
    tier: premiumTierEnum('tier'),
    emailAccountsAccess: integer('emailAccountsAccess'),
    unsubscribeMonth: integer('unsubscribeMonth'),
    unsubscribeCredits: integer('unsubscribeCredits'),
    aiMonth: integer('aiMonth'),
    aiCredits: integer('aiCredits'),
  },
  (table) => [
    uniqueIndex('Premium_stripeCustomerId_key').on(table.stripeCustomerId),
    uniqueIndex('Premium_stripeSubscriptionId_key').on(table.stripeSubscriptionId),
    uniqueIndex('Premium_stripeSubscriptionItemId_key').on(table.stripeSubscriptionItemId),
    uniqueIndex('Premium_appleOriginalTransactionId_key').on(table.appleOriginalTransactionId),
    uniqueIndex('Premium_appleLatestTransactionId_key').on(table.appleLatestTransactionId),
    index('Premium_pendingInvites_idx').on(table.pendingInvites),
  ],
)

export const pendingPremiumGrant = pgTable(
  'PendingPremiumGrant',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    email: text('email').notNull(),
    tier: premiumTierEnum('tier').notNull(),
    count: integer('count').notNull().default(1),
    emailAccountsAccess: integer('emailAccountsAccess'),
  },
  (table) => [uniqueIndex('PendingPremiumGrant_email_key').on(table.email)],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const label = pgTable(
  'Label',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    gmailLabelId: text('gmailLabelId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    enabled: boolean('enabled').notNull().default(true),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Label_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('Label_gmailLabelId_emailAccountId_key').on(
      table.gmailLabelId,
      table.emailAccountId,
    ),
    uniqueIndex('Label_name_emailAccountId_key').on(table.name, table.emailAccountId),
    foreignKey({
      name: 'Label_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const rule = pgTable(
  'Rule',
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
    enabled: boolean('enabled').notNull().default(true),
    automate: boolean('automate').notNull().default(true),
    runOnThreads: boolean('runOnThreads').notNull().default(false),
    emailAccountId: text('emailAccountId').notNull(),
    organizationRuleId: text('organizationRuleId'),
    organizationRuleMemberEnabled: boolean('organizationRuleMemberEnabled'),
    conditionalOperator: logicalOperatorEnum('conditionalOperator').notNull().default('AND'),
    instructions: text('instructions'),
    groupId: text('groupId'),
    from: text('from'),
    to: text('to'),
    subject: text('subject'),
    body: text('body'),
    categoryFilterType: categoryFilterTypeEnum('categoryFilterType'),
    systemType: systemTypeEnum('systemType'),
    promptText: text('promptText'),
  },
  (table) => [
    uniqueIndex('Rule_groupId_key').on(table.groupId),
    index('Rule_emailAccountId_enabled_idx').on(table.emailAccountId, table.enabled),
    index('Rule_organizationRuleId_idx').on(table.organizationRuleId),
    unique('Rule_id_emailAccountId_key').on(table.id, table.emailAccountId),
    uniqueIndex('Rule_name_emailAccountId_key').on(table.name, table.emailAccountId),
    uniqueIndex('Rule_emailAccountId_systemType_key').on(table.emailAccountId, table.systemType),
    uniqueIndex('Rule_emailAccountId_organizationRuleId_key').on(
      table.emailAccountId,
      table.organizationRuleId,
    ),
    foreignKey({
      name: 'Rule_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Rule_organizationRuleId_fkey',
      columns: [table.organizationRuleId],
      foreignColumns: [organizationRule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Rule_groupId_fkey', columns: [table.groupId], foreignColumns: [group.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const action = pgTable(
  'Action',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: actionTypeEnum('type').notNull(),
    ruleId: text('ruleId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
    messagingChannelId: text('messagingChannelId'),
    messagingChannelEmailAccountId: text('messagingChannelEmailAccountId'),
    label: text('label'),
    labelId: text('labelId'),
    subject: text('subject'),
    content: text('content'),
    to: text('to'),
    cc: text('cc'),
    bcc: text('bcc'),
    url: text('url'),
    folderName: text('folderName'),
    folderId: text('folderId'),
    delayInMinutes: integer('delayInMinutes'),
    staticAttachments: jsonb('staticAttachments'),
    integrationName: text('integrationName'),
    integrationToolName: text('integrationToolName'),
    integrationArgs: jsonb('integrationArgs'),
  },
  (table) => [
    index('Action_ruleId_idx').on(table.ruleId),
    index('Action_emailAccountId_idx').on(table.emailAccountId),
    index('Action_messagingChannelId_messagingChannelEmailAccountId_idx').on(
      table.messagingChannelId,
      table.messagingChannelEmailAccountId,
    ),
    foreignKey({
      name: 'Action_ruleId_emailAccountId_fkey',
      columns: [table.ruleId, table.emailAccountId],
      foreignColumns: [rule.id, rule.emailAccountId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Action_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Action_messagingChannelId_messagingChannelEmailAccountId_fkey',
      columns: [table.messagingChannelId, table.messagingChannelEmailAccountId],
      foreignColumns: [messagingChannel.id, messagingChannel.emailAccountId],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const ruleHistory = pgTable(
  'RuleHistory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    ruleId: text('ruleId').notNull(),
    version: integer('version').notNull(),
    triggerType: text('triggerType').notNull(),
    promptText: text('promptText'),
    name: text('name').notNull(),
    instructions: text('instructions'),
    enabled: boolean('enabled').notNull(),
    automate: boolean('automate').notNull(),
    runOnThreads: boolean('runOnThreads').notNull(),
    conditionalOperator: text('conditionalOperator').notNull(),
    from: text('from'),
    to: text('to'),
    subject: text('subject'),
    body: text('body'),
    categoryFilterType: text('categoryFilterType'),
    systemType: text('systemType'),
    actions: jsonb('actions').notNull(),
    categoryFilters: jsonb('categoryFilters'),
  },
  (table) => [
    index('RuleHistory_ruleId_createdAt_idx').on(table.ruleId, table.createdAt),
    uniqueIndex('RuleHistory_ruleId_version_key').on(table.ruleId, table.version),
    foreignKey({
      name: 'RuleHistory_ruleId_fkey',
      columns: [table.ruleId],
      foreignColumns: [rule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const executedRule = pgTable(
  'ExecutedRule',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    threadId: text('threadId').notNull(),
    messageId: text('messageId').notNull(),
    status: executedRuleStatusEnum('status').notNull(),
    automated: boolean('automated').notNull(),
    reason: text('reason'),
    matchMetadata: jsonb('matchMetadata'),
    ruleId: text('ruleId'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx').on(
      table.emailAccountId,
      table.threadId,
      table.messageId,
      table.ruleId,
    ),
    index('ExecutedRule_emailAccountId_messageId_idx').on(table.emailAccountId, table.messageId),
    index('ExecutedRule_emailAccountId_status_createdAt_idx').on(
      table.emailAccountId,
      table.status,
      table.createdAt,
    ),
    index('ExecutedRule_emailAccountId_createdAt_idx').on(table.emailAccountId, table.createdAt),
    index('ExecutedRule_emailAccountId_id_idx').on(table.emailAccountId, table.id),
    index('ExecutedRule_emailAccountId_updatedAt_id_idx').on(
      table.emailAccountId,
      table.updatedAt,
      table.id,
    ),
    index('ExecutedRule_ruleId_idx').on(table.ruleId),
    foreignKey({
      name: 'ExecutedRule_ruleId_fkey',
      columns: [table.ruleId],
      foreignColumns: [rule.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ExecutedRule_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const executedAction = pgTable(
  'ExecutedAction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: actionTypeEnum('type').notNull(),
    executionStatus: executedActionStatusEnum('executionStatus'),
    executionStartedAt: timestamp('executionStartedAt', { precision: 3 }),
    executedAt: timestamp('executedAt', { precision: 3 }),
    executionError: jsonb('executionError'),
    sentMessageIds: text('sentMessageIds').array().default([]),
    executedRuleId: text('executedRuleId').notNull(),
    messagingChannelId: text('messagingChannelId'),
    messagingMessageId: text('messagingMessageId'),
    messagingMessageSentAt: timestamp('messagingMessageSentAt', { precision: 3 }),
    messagingMessageStatus: messagingMessageStatusEnum('messagingMessageStatus'),
    label: text('label'),
    labelId: text('labelId'),
    subject: text('subject'),
    content: text('content'),
    to: text('to'),
    cc: text('cc'),
    bcc: text('bcc'),
    url: text('url'),
    folderName: text('folderName'),
    folderId: text('folderId'),
    staticAttachments: jsonb('staticAttachments'),
    selectedAttachments: jsonb('selectedAttachments'),
    integrationName: text('integrationName'),
    integrationToolName: text('integrationToolName'),
    integrationArgs: jsonb('integrationArgs'),
    draftId: text('draftId'),
    draftStatus: draftEmailStatusEnum('draftStatus'),
    wasDraftSent: boolean('wasDraftSent'),
    draftModelProvider: text('draftModelProvider'),
    draftModelName: text('draftModelName'),
    draftPipelineVersion: integer('draftPipelineVersion'),
    draftContextMetadata: jsonb('draftContextMetadata'),
  },
  (table) => [
    index('ExecutedAction_executedRuleId_idx').on(table.executedRuleId),
    index('ExecutedAction_executionStatus_createdAt_idx').on(
      table.executionStatus,
      table.createdAt,
    ),
    index('ExecutedAction_sentMessageIds_idx').on(table.sentMessageIds),
    index('ExecutedAction_messagingChannelId_createdAt_idx').on(
      table.messagingChannelId,
      table.createdAt,
    ),
    index('ExecutedAction_draftStatus_createdAt_draft_idx').on(table.draftStatus, table.createdAt),
    index('ExecutedAction_executedRuleId_draft_idx').on(table.executedRuleId),
    foreignKey({
      name: 'ExecutedAction_executedRuleId_fkey',
      columns: [table.executedRuleId],
      foreignColumns: [executedRule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ExecutedAction_messagingChannelId_fkey',
      columns: [table.messagingChannelId],
      foreignColumns: [messagingChannel.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const scheduledAction = pgTable(
  'ScheduledAction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    executedRuleId: text('executedRuleId').notNull(),
    actionType: actionTypeEnum('actionType').notNull(),
    messageId: text('messageId').notNull(),
    threadId: text('threadId').notNull(),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }).notNull(),
    emailAccountId: text('emailAccountId').notNull(),
    status: scheduledActionStatusEnum('status').notNull().default('PENDING'),
    schedulingStatus: schedulingStatusEnum('schedulingStatus').notNull().default('PENDING'),
    label: text('label'),
    labelId: text('labelId'),
    subject: text('subject'),
    content: text('content'),
    to: text('to'),
    cc: text('cc'),
    bcc: text('bcc'),
    url: text('url'),
    folderName: text('folderName'),
    folderId: text('folderId'),
    scheduledId: text('scheduledId'),
    staticAttachments: jsonb('staticAttachments'),
    selectedAttachments: jsonb('selectedAttachments'),
    executedAt: timestamp('executedAt', { precision: 3 }),
    executedActionId: text('executedActionId'),
  },
  (table) => [
    uniqueIndex('ScheduledAction_executedActionId_key').on(table.executedActionId),
    index('ScheduledAction_executedRuleId_idx').on(table.executedRuleId),
    index('ScheduledAction_status_scheduledFor_idx').on(table.status, table.scheduledFor),
    index('ScheduledAction_emailAccountId_messageId_idx').on(table.emailAccountId, table.messageId),
    foreignKey({
      name: 'ScheduledAction_executedRuleId_fkey',
      columns: [table.executedRuleId],
      foreignColumns: [executedRule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ScheduledAction_executedActionId_fkey',
      columns: [table.executedActionId],
      foreignColumns: [executedAction.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ScheduledAction_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const snoozedThread = pgTable(
  'SnoozedThread',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    threadId: text('threadId').notNull(),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }).notNull(),
    status: snoozedThreadStatusEnum('status').notNull().default('PENDING'),
    clientMutationId: text('clientMutationId'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('SnoozedThread_status_scheduledFor_idx').on(table.status, table.scheduledFor),
    index('SnoozedThread_emailAccountId_threadId_status_idx').on(
      table.emailAccountId,
      table.threadId,
      table.status,
    ),
    uniqueIndex('SnoozedThread_emailAccountId_clientMutationId_key').on(
      table.emailAccountId,
      table.clientMutationId,
    ),
    foreignKey({
      name: 'SnoozedThread_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scheduledEmail = pgTable(
  'ScheduledEmail',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    emailAccountId: text('emailAccountId').notNull(),
    clientMutationId: text('clientMutationId').notNull(),
    payloadHash: text('payloadHash').notNull(),
    payload: jsonb('payload').notNull(),
    threadId: text('threadId'),
    sendAt: timestamp('sendAt', { precision: 3 }).notNull(),
    status: scheduledEmailStatusEnum('status').notNull().default('PENDING'),
    processingStartedAt: timestamp('processingStartedAt', { precision: 3 }),
    executionQueuedAt: timestamp('executionQueuedAt', { precision: 3 }),
    sentAt: timestamp('sentAt', { precision: 3 }),
    error: text('error'),
    remindAt: timestamp('remindAt', { precision: 3 }),
    reminderStatus: emailReminderStatusEnum('reminderStatus').notNull().default('NONE'),
    reminderStartedAt: timestamp('reminderStartedAt', { precision: 3 }),
    heldForUndo: boolean('heldForUndo').notNull().default(false),
    draftMessageIds: text('draftMessageIds').array().default([]),
  },
  (table) => [
    index('ScheduledEmail_status_sendAt_idx').on(table.status, table.sendAt),
    index('ScheduledEmail_status_processingStartedAt_idx').on(
      table.status,
      table.processingStartedAt,
    ),
    index('ScheduledEmail_reminderStatus_remindAt_idx').on(table.reminderStatus, table.remindAt),
    index('ScheduledEmail_reminderStatus_reminderStartedAt_idx').on(
      table.reminderStatus,
      table.reminderStartedAt,
    ),
    index('ScheduledEmail_emailAccountId_threadId_idx').on(table.emailAccountId, table.threadId),
    uniqueIndex('ScheduledEmail_emailAccountId_clientMutationId_key').on(
      table.emailAccountId,
      table.clientMutationId,
    ),
    foreignKey({
      name: 'ScheduledEmail_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailSendOperation = pgTable(
  'EmailSendOperation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    clientMutationId: text('clientMutationId').notNull(),
    payloadHash: text('payloadHash').notNull(),
    status: emailSendOperationStatusEnum('status').notNull().default('PROCESSING'),
    processingStartedAt: timestamp('processingStartedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    result: jsonb('result'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('EmailSendOperation_status_updatedAt_idx').on(table.status, table.updatedAt),
    uniqueIndex('EmailSendOperation_emailAccountId_clientMutationId_key').on(
      table.emailAccountId,
      table.clientMutationId,
    ),
    foreignKey({
      name: 'EmailSendOperation_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sentMessageOpen = pgTable(
  'SentMessageOpen',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    token: text('token').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
    threadId: text('threadId'),
    messageId: text('messageId'),
    firstOpenedAt: timestamp('firstOpenedAt', { precision: 3 }),
    lastOpenedAt: timestamp('lastOpenedAt', { precision: 3 }),
    openCount: integer('openCount').notNull().default(0),
  },
  (table) => [
    uniqueIndex('SentMessageOpen_token_key').on(table.token),
    index('SentMessageOpen_emailAccountId_threadId_idx').on(table.emailAccountId, table.threadId),
    uniqueIndex('SentMessageOpen_emailAccountId_messageId_key').on(
      table.emailAccountId,
      table.messageId,
    ),
    foreignKey({
      name: 'SentMessageOpen_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const group = pgTable(
  'Group',
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
    prompt: text('prompt'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Group_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('Group_name_emailAccountId_key').on(table.name, table.emailAccountId),
    foreignKey({
      name: 'Group_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const groupItem = pgTable(
  'GroupItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    groupId: text('groupId'),
    type: groupItemTypeEnum('type').notNull(),
    value: text('value').notNull(),
    exclude: boolean('exclude').notNull().default(false),
    reason: text('reason'),
    threadId: text('threadId'),
    messageId: text('messageId'),
    source: groupItemSourceEnum('source'),
  },
  (table) => [
    uniqueIndex('GroupItem_groupId_type_value_key').on(table.groupId, table.type, table.value),
    foreignKey({
      name: 'GroupItem_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [group.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const classificationFeedback = pgTable(
  'ClassificationFeedback',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    sender: text('sender').notNull(),
    eventType: classificationFeedbackEventTypeEnum('eventType').notNull(),
    ruleId: text('ruleId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
    threadId: text('threadId').notNull(),
    messageId: text('messageId').notNull(),
  },
  (table) => [
    index('ClassificationFeedback_ruleId_idx').on(table.ruleId),
    uniqueIndex('ClassificationFeedback_emailAccountId_sender_ruleId_message_key').on(
      table.emailAccountId,
      table.sender,
      table.ruleId,
      table.messageId,
      table.eventType,
    ),
    foreignKey({
      name: 'ClassificationFeedback_ruleId_fkey',
      columns: [table.ruleId],
      foreignColumns: [rule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ClassificationFeedback_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const category = pgTable(
  'Category',
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
    description: text('description'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Category_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('Category_name_emailAccountId_key').on(table.name, table.emailAccountId),
    foreignKey({
      name: 'Category_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const newsletter = pgTable(
  'Newsletter',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    email: text('email').notNull(),
    name: text('name'),
    status: newsletterStatusEnum('status'),
    patternAnalyzed: boolean('patternAnalyzed').notNull().default(false),
    lastAnalyzedAt: timestamp('lastAnalyzedAt', { precision: 3 }),
    emailAccountId: text('emailAccountId').notNull(),
    categoryId: text('categoryId'),
  },
  (table) => [
    index('Newsletter_emailAccountId_status_idx').on(table.emailAccountId, table.status),
    index('Newsletter_categoryId_idx').on(table.categoryId),
    uniqueIndex('Newsletter_email_emailAccountId_key').on(table.email, table.emailAccountId),
    foreignKey({
      name: 'Newsletter_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Newsletter_categoryId_fkey',
      columns: [table.categoryId],
      foreignColumns: [category.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const coldEmail = pgTable(
  'ColdEmail',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    fromEmail: text('fromEmail').notNull(),
    messageId: text('messageId'),
    threadId: text('threadId'),
    status: coldEmailStatusEnum('status'),
    reason: text('reason'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ColdEmail_emailAccountId_status_idx').on(table.emailAccountId, table.status),
    index('ColdEmail_emailAccountId_createdAt_idx').on(table.emailAccountId, table.createdAt),
    uniqueIndex('ColdEmail_emailAccountId_fromEmail_key').on(table.emailAccountId, table.fromEmail),
    foreignKey({
      name: 'ColdEmail_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailMessage = pgTable(
  'EmailMessage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    threadId: text('threadId').notNull(),
    messageId: text('messageId').notNull(),
    date: timestamp('date', { precision: 3 }).notNull(),
    from: text('from').notNull(),
    fromName: text('fromName'),
    fromDomain: text('fromDomain').notNull(),
    to: text('to').notNull(),
    unsubscribeLink: text('unsubscribeLink'),
    read: boolean('read').notNull(),
    sent: boolean('sent').notNull(),
    draft: boolean('draft').notNull(),
    inbox: boolean('inbox').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('EmailMessage_emailAccountId_threadId_idx').on(table.emailAccountId, table.threadId),
    index('EmailMessage_emailAccountId_date_idx').on(table.emailAccountId, table.date),
    index('EmailMessage_emailAccountId_from_idx').on(table.emailAccountId, table.from),
    index('EmailMessage_emailAccountId_fromName_idx').on(table.emailAccountId, table.fromName),
    uniqueIndex('EmailMessage_emailAccountId_threadId_messageId_key').on(
      table.emailAccountId,
      table.threadId,
      table.messageId,
    ),
    foreignKey({
      name: 'EmailMessage_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const responseTime = pgTable(
  'ResponseTime',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    threadId: text('threadId').notNull(),
    sentMessageId: text('sentMessageId').notNull(),
    receivedMessageId: text('receivedMessageId').notNull(),
    responseTimeMins: integer('responseTimeMins').notNull(),
    receivedAt: timestamp('receivedAt', { precision: 3 }).notNull(),
    sentAt: timestamp('sentAt', { precision: 3 }).notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ResponseTime_emailAccountId_sentAt_idx').on(table.emailAccountId, table.sentAt),
    uniqueIndex('ResponseTime_emailAccountId_sentMessageId_key').on(
      table.emailAccountId,
      table.sentMessageId,
    ),
    foreignKey({
      name: 'ResponseTime_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const threadTracker = pgTable(
  'ThreadTracker',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    sentAt: timestamp('sentAt', { precision: 3 }).notNull(),
    threadId: text('threadId').notNull(),
    messageId: text('messageId').notNull(),
    resolved: boolean('resolved').notNull().default(false),
    type: threadTrackerTypeEnum('type').notNull(),
    followUpAppliedAt: timestamp('followUpAppliedAt', { precision: 3 }),
    followUpDraftId: text('followUpDraftId'),
    followUpDraftContent: text('followUpDraftContent'),
    followUpDraftCreatedAt: timestamp('followUpDraftCreatedAt', { precision: 3 }),
    followUpNotifications: jsonb('followUpNotifications'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ThreadTracker_emailAccountId_resolved_idx').on(table.emailAccountId, table.resolved),
    index('ThreadTracker_emailAccountId_resolved_sentAt_type_idx').on(
      table.emailAccountId,
      table.resolved,
      table.sentAt,
      table.type,
    ),
    index('ThreadTracker_emailAccountId_type_resolved_sentAt_idx').on(
      table.emailAccountId,
      table.type,
      table.resolved,
      table.sentAt,
    ),
    index('ThreadTracker_emailAccountId_type_resolved_followUpAppliedA_idx').on(
      table.emailAccountId,
      table.type,
      table.resolved,
      table.followUpAppliedAt,
      table.sentAt,
    ),
    index('ThreadTracker_followUpDraft_cleanup_idx').on(
      table.emailAccountId,
      table.followUpDraftCreatedAt,
    ),
    uniqueIndex('ThreadTracker_emailAccountId_threadId_messageId_key').on(
      table.emailAccountId,
      table.threadId,
      table.messageId,
    ),
    foreignKey({
      name: 'ThreadTracker_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cleanupJob = pgTable(
  'CleanupJob',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    action: cleanActionEnum('action').notNull().default('ARCHIVE'),
    daysOld: integer('daysOld').notNull().default(7),
    instructions: text('instructions'),
    skipReply: boolean('skipReply'),
    skipStarred: boolean('skipStarred'),
    skipCalendar: boolean('skipCalendar'),
    skipReceipt: boolean('skipReceipt'),
    skipAttachment: boolean('skipAttachment'),
    skipConversation: boolean('skipConversation'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('CleanupJob_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'CleanupJob_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const cleanupThread = pgTable(
  'CleanupThread',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    threadId: text('threadId').notNull(),
    archived: boolean('archived').notNull(),
    jobId: text('jobId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('CleanupThread_jobId_idx').on(table.jobId),
    index('CleanupThread_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'CleanupThread_jobId_fkey',
      columns: [table.jobId],
      foreignColumns: [cleanupJob.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CleanupThread_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const knowledge = pgTable(
  'Knowledge',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    title: text('title').notNull(),
    content: text('content').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    uniqueIndex('Knowledge_emailAccountId_title_key').on(table.emailAccountId, table.title),
    foreignKey({
      name: 'Knowledge_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const snippet = pgTable(
  'Snippet',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    shortcut: text('shortcut').notNull(),
    content: text('content').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Snippet_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('Snippet_emailAccountId_shortcut_key').on(table.emailAccountId, table.shortcut),
    foreignKey({
      name: 'Snippet_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailSignature = pgTable(
  'EmailSignature',
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
    html: text('html').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    uniqueIndex('EmailSignature_emailAccountId_name_key').on(table.emailAccountId, table.name),
    foreignKey({
      name: 'EmailSignature_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const replyMemory = pgTable(
  'ReplyMemory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    content: text('content').notNull(),
    kind: replyMemoryKindEnum('kind').notNull(),
    scopeType: replyMemoryScopeTypeEnum('scopeType').notNull(),
    scopeValue: text('scopeValue').notNull(),
    isLearnedStyleEvidence: boolean('isLearnedStyleEvidence').notNull().default(false),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ReplyMemory_emailAccountId_updatedAt_idx').on(table.emailAccountId, table.updatedAt),
    index('ReplyMemory_emailAccountId_scopeType_scopeValue_idx').on(
      table.emailAccountId,
      table.scopeType,
      table.scopeValue,
    ),
    uniqueIndex('ReplyMemory_emailAccountId_kind_scopeType_scopeValue_conten_key').on(
      table.emailAccountId,
      table.kind,
      table.scopeType,
      table.scopeValue,
      table.content,
    ),
    foreignKey({
      name: 'ReplyMemory_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const replyMemorySource = pgTable(
  'ReplyMemorySource',
  {
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    learnedWritingStyleAnalyzedAt: timestamp('learnedWritingStyleAnalyzedAt', { precision: 3 }),
    replyMemoryId: text('replyMemoryId').notNull(),
    draftSendLogId: text('draftSendLogId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'ReplyMemorySource_pkey',
      columns: [table.replyMemoryId, table.draftSendLogId],
    }),
    index('ReplyMemorySource_draftSendLogId_idx').on(table.draftSendLogId),
    index('ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzed_idx').on(
      table.replyMemoryId,
      table.learnedWritingStyleAnalyzedAt,
      table.createdAt,
    ),
    foreignKey({
      name: 'ReplyMemorySource_replyMemoryId_fkey',
      columns: [table.replyMemoryId],
      foreignColumns: [replyMemory.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ReplyMemorySource_draftSendLogId_fkey',
      columns: [table.draftSendLogId],
      foreignColumns: [draftSendLog.id],
    })
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
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name'),
    hashedKey: text('hashedKey').notNull(),
    isActive: boolean('isActive').notNull().default(true),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    scopes: apiKeyScopeEnum('scopes').array().default([]),
    userId: text('userId').notNull(),
    emailAccountId: text('emailAccountId'),
  },
  (table) => [
    uniqueIndex('ApiKey_hashedKey_key').on(table.hashedKey),
    index('ApiKey_userId_isActive_idx').on(table.userId, table.isActive),
    index('ApiKey_emailAccountId_isActive_idx').on(table.emailAccountId, table.isActive),
    foreignKey({ name: 'ApiKey_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ApiKey_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emailToken = pgTable(
  'EmailToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    token: text('token').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    uniqueIndex('EmailToken_token_key').on(table.token),
    index('EmailToken_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'EmailToken_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const payment = pgTable(
  'Payment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    premiumId: text('premiumId'),
    processorType: processorTypeEnum('processorType').notNull().default('LEMON_SQUEEZY'),
    processorId: text('processorId'),
    processorSubscriptionId: text('processorSubscriptionId'),
    processorCustomerId: text('processorCustomerId'),
    amount: integer('amount').notNull(),
    currency: text('currency').notNull(),
    status: text('status').notNull(),
    tax: integer('tax').notNull(),
    taxInclusive: boolean('taxInclusive').notNull(),
    refunded: boolean('refunded').notNull().default(false),
    refundedAt: timestamp('refundedAt', { precision: 3 }),
    refundedAmount: integer('refundedAmount'),
    billingReason: text('billingReason'),
    invoiceEmailSentAt: timestamp('invoiceEmailSentAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('Payment_processorId_key').on(table.processorId),
    index('Payment_premiumId_createdAt_idx').on(table.premiumId, table.createdAt),
    foreignKey({
      name: 'Payment_premiumId_fkey',
      columns: [table.premiumId],
      foreignColumns: [premium.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const draftSendLog = pgTable(
  'DraftSendLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    executedActionId: text('executedActionId').notNull(),
    sentMessageId: text('sentMessageId').notNull(),
    similarityScore: doublePrecision('similarityScore').notNull(),
    bodySimilarityScore: doublePrecision('bodySimilarityScore'),
    bodySimilarityStatus: text('bodySimilarityStatus'),
    similarityMetadata: jsonb('similarityMetadata'),
    sentText: text('sentText'),
    replyMemorySentText: text('replyMemorySentText'),
    replyMemoryAttemptCount: integer('replyMemoryAttemptCount').notNull().default(0),
    replyMemoryProcessedAt: timestamp('replyMemoryProcessedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('DraftSendLog_executedActionId_key').on(table.executedActionId),
    index('DraftSendLog_executedActionId_idx').on(table.executedActionId),
    index('DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_idx').on(
      table.replyMemoryProcessedAt,
      table.replyMemoryAttemptCount,
      table.createdAt,
    ),
    foreignKey({
      name: 'DraftSendLog_executedActionId_fkey',
      columns: [table.executedActionId],
      foreignColumns: [executedAction.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chat = pgTable(
  'Chat',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    name: text('name'),
    compactionCount: integer('compactionCount').notNull().default(0),
    lastSeenRulesRevision: integer('lastSeenRulesRevision'),
    activeStreamId: text('activeStreamId'),
    activeStreamStartedAt: timestamp('activeStreamStartedAt', { precision: 3 }),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Chat_emailAccountId_idx').on(table.emailAccountId),
    index('Chat_deletedAt_idx').on(table.deletedAt),
    foreignKey({
      name: 'Chat_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatMessage = pgTable(
  'ChatMessage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    role: text('role').notNull(),
    parts: jsonb('parts').notNull(),
    metadata: jsonb('metadata'),
    chatId: text('chatId').notNull(),
  },
  (table) => [
    index('ChatMessage_chatId_idx').on(table.chatId),
    foreignKey({
      name: 'ChatMessage_chatId_fkey',
      columns: [table.chatId],
      foreignColumns: [chat.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatCompaction = pgTable(
  'ChatCompaction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    summary: text('summary').notNull(),
    messageCount: integer('messageCount').notNull(),
    compactedBeforeCreatedAt: timestamp('compactedBeforeCreatedAt', { precision: 3 }).notNull(),
    chatId: text('chatId').notNull(),
  },
  (table) => [
    index('ChatCompaction_chatId_idx').on(table.chatId),
    foreignKey({
      name: 'ChatCompaction_chatId_fkey',
      columns: [table.chatId],
      foreignColumns: [chat.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatMemory = pgTable(
  'ChatMemory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    content: text('content').notNull(),
    chatId: text('chatId'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('ChatMemory_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'ChatMemory_chatId_fkey',
      columns: [table.chatId],
      foreignColumns: [chat.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ChatMemory_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const calendarConnection = pgTable(
  'CalendarConnection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    provider: text('provider').notNull(),
    email: text('email').notNull(),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    isConnected: boolean('isConnected').notNull().default(true),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    uniqueIndex('CalendarConnection_emailAccountId_provider_email_key').on(
      table.emailAccountId,
      table.provider,
      table.email,
    ),
    foreignKey({
      name: 'CalendarConnection_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const calendar = pgTable(
  'Calendar',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    calendarId: text('calendarId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    primary: boolean('primary').notNull().default(false),
    isEnabled: boolean('isEnabled').notNull().default(true),
    timezone: text('timezone'),
    connectionId: text('connectionId').notNull(),
  },
  (table) => [
    uniqueIndex('Calendar_connectionId_calendarId_key').on(table.connectionId, table.calendarId),
    foreignKey({
      name: 'Calendar_connectionId_fkey',
      columns: [table.connectionId],
      foreignColumns: [calendarConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const bookingLink = pgTable(
  'BookingLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    slug: text('slug').notNull(),
    title: text('title').notNull(),
    description: text('description'),
    isActive: boolean('isActive').notNull().default(true),
    durationMinutes: integer('durationMinutes').notNull(),
    slotIntervalMinutes: integer('slotIntervalMinutes').notNull(),
    locationType: bookingLinkLocationTypeEnum('locationType').notNull().default('CUSTOM'),
    locationValue: text('locationValue'),
    minimumNoticeMinutes: integer('minimumNoticeMinutes').notNull().default(120),
    maxDaysAhead: integer('maxDaysAhead').notNull().default(90),
    emailAccountId: text('emailAccountId').notNull(),
    destinationCalendarId: text('destinationCalendarId'),
    availabilityScheduleId: text('availabilityScheduleId').notNull(),
  },
  (table) => [
    uniqueIndex('BookingLink_slug_key').on(table.slug),
    uniqueIndex('BookingLink_emailAccountId_key').on(table.emailAccountId),
    index('BookingLink_destinationCalendarId_idx').on(table.destinationCalendarId),
    index('BookingLink_availabilityScheduleId_idx').on(table.availabilityScheduleId),
    foreignKey({
      name: 'BookingLink_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingLink_destinationCalendarId_fkey',
      columns: [table.destinationCalendarId],
      foreignColumns: [calendar.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'BookingLink_availabilityScheduleId_emailAccountId_fkey',
      columns: [table.availabilityScheduleId, table.emailAccountId],
      foreignColumns: [availabilitySchedule.id, availabilitySchedule.emailAccountId],
    })
      .onDelete('no action')
      .onUpdate('cascade'),
  ],
)

export const availabilitySchedule = pgTable(
  'AvailabilitySchedule',
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
    isDefault: boolean('isDefault').notNull().default(false),
    timezone: text('timezone').notNull().default('UTC'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('AvailabilitySchedule_emailAccountId_idx').on(table.emailAccountId),
    index('AvailabilitySchedule_emailAccountId_isDefault_idx').on(
      table.emailAccountId,
      table.isDefault,
    ),
    unique('AvailabilitySchedule_id_emailAccountId_key').on(table.id, table.emailAccountId),
    foreignKey({
      name: 'AvailabilitySchedule_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const availabilityWindow = pgTable(
  'AvailabilityWindow',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    weekday: integer('weekday').notNull(),
    startMinutes: integer('startMinutes').notNull(),
    endMinutes: integer('endMinutes').notNull(),
    availabilityScheduleId: text('availabilityScheduleId').notNull(),
  },
  (table) => [
    index('AvailabilityWindow_availabilityScheduleId_idx').on(table.availabilityScheduleId),
    foreignKey({
      name: 'AvailabilityWindow_availabilityScheduleId_fkey',
      columns: [table.availabilityScheduleId],
      foreignColumns: [availabilitySchedule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const booking = pgTable(
  'Booking',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    guestName: text('guestName').notNull(),
    guestEmail: text('guestEmail').notNull(),
    guestNote: text('guestNote'),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    status: bookingStatusEnum('status').notNull().default('PENDING_PROVIDER_EVENT'),
    provider: text('provider'),
    providerConnectionId: text('providerConnectionId'),
    providerCalendarId: text('providerCalendarId'),
    providerEventId: text('providerEventId'),
    videoConferenceLink: text('videoConferenceLink'),
    cancelTokenHash: text('cancelTokenHash').notNull(),
    cancellationReason: text('cancellationReason'),
    idempotencyToken: text('idempotencyToken'),
    bookingLinkId: text('bookingLinkId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Booking_bookingLinkId_startTime_endTime_idx').on(
      table.bookingLinkId,
      table.startTime,
      table.endTime,
    ),
    index('Booking_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('Booking_bookingLinkId_idempotencyToken_key').on(
      table.bookingLinkId,
      table.idempotencyToken,
    ),
    foreignKey({
      name: 'Booking_bookingLinkId_fkey',
      columns: [table.bookingLinkId],
      foreignColumns: [bookingLink.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Booking_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const meetingBriefing = pgTable(
  'MeetingBriefing',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    calendarEventId: text('calendarEventId').notNull(),
    eventTitle: text('eventTitle').notNull(),
    eventStartTime: timestamp('eventStartTime', { precision: 3 }).notNull(),
    guestCount: integer('guestCount').notNull(),
    status: meetingBriefingStatusEnum('status').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('MeetingBriefing_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('MeetingBriefing_emailAccountId_calendarEventId_key').on(
      table.emailAccountId,
      table.calendarEventId,
    ),
    foreignKey({
      name: 'MeetingBriefing_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const contactResearch = pgTable(
  'ContactResearch',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    email: text('email').notNull(),
    researchStartedAt: timestamp('researchStartedAt', { precision: 3 }).notNull(),
    found: boolean('found').notNull(),
    role: text('role'),
    confidence: text('confidence'),
    company: jsonb('company'),
    sources: text('sources').array().default([]),
  },
  (table) => [
    index('ContactResearch_latest_idx').on(
      table.email,
      table.researchStartedAt,
      table.createdAt,
      table.id,
    ),
  ],
)

export const meetingRecording = pgTable(
  'MeetingRecording',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    botProvider: text('botProvider').notNull().default('recall'),
    externalBotId: text('externalBotId'),
    botName: text('botName'),
    externalRecordingId: text('externalRecordingId'),
    transcriptRequestedAt: timestamp('transcriptRequestedAt', { precision: 3 }),
    externalTranscriptId: text('externalTranscriptId'),
    meetingUrl: text('meetingUrl').notNull(),
    normalizedMeetingUrl: text('normalizedMeetingUrl').notNull(),
    activeKey: text('activeKey'),
    meetingStartTime: timestamp('meetingStartTime', { precision: 3 }).notNull(),
    status: meetingRecordingStatusEnum('status').notNull().default('PENDING'),
    failureReason: text('failureReason'),
    transcript: jsonb('transcript'),
    transcriptFetchedAt: timestamp('transcriptFetchedAt', { precision: 3 }),
    mediaDeletedAt: timestamp('mediaDeletedAt', { precision: 3 }),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx').on(
      table.normalizedMeetingUrl,
      table.meetingStartTime,
    ),
    index('MeetingRecording_status_meetingStartTime_idx').on(table.status, table.meetingStartTime),
    index('MeetingRecording_status_mediaDeletedAt_idx').on(table.status, table.mediaDeletedAt),
    index('MeetingRecording_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('MeetingRecording_botProvider_externalBotId_key').on(
      table.botProvider,
      table.externalBotId,
    ),
    uniqueIndex('MeetingRecording_activeKey_meetingStartTime_key').on(
      table.activeKey,
      table.meetingStartTime,
    ),
    foreignKey({
      name: 'MeetingRecording_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const meeting = pgTable(
  'Meeting',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    calendarEventId: text('calendarEventId').notNull(),
    eventTitle: text('eventTitle').notNull(),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    attendees: jsonb('attendees').notNull(),
    organizerEmail: text('organizerEmail'),
    joinOverride: boolean('joinOverride'),
    processingStatus: meetingProcessingStatusEnum('processingStatus').notNull().default('PENDING'),
    processingAttempts: integer('processingAttempts').notNull().default(0),
    summary: jsonb('summary'),
    followUpDraftStartedAt: timestamp('followUpDraftStartedAt', { precision: 3 }),
    followUpDraftId: text('followUpDraftId'),
    recapSentAt: timestamp('recapSentAt', { precision: 3 }),
    recordingId: text('recordingId'),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('Meeting_emailAccountId_startTime_idx').on(table.emailAccountId, table.startTime),
    index('Meeting_recordingId_idx').on(table.recordingId),
    index('Meeting_processingStatus_updatedAt_idx').on(table.processingStatus, table.updatedAt),
    uniqueIndex('Meeting_emailAccountId_calendarEventId_key').on(
      table.emailAccountId,
      table.calendarEventId,
    ),
    uniqueIndex('Meeting_emailAccountId_recordingId_key').on(
      table.emailAccountId,
      table.recordingId,
    ),
    foreignKey({
      name: 'Meeting_recordingId_fkey',
      columns: [table.recordingId],
      foreignColumns: [meetingRecording.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Meeting_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const messagingChannel = pgTable(
  'MessagingChannel',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    provider: messagingProviderEnum('provider').notNull(),
    isConnected: boolean('isConnected').notNull().default(true),
    teamId: text('teamId').notNull(),
    teamName: text('teamName'),
    providerUserId: text('providerUserId'),
    botUserId: text('botUserId'),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('MessagingChannel_emailAccountId_idx').on(table.emailAccountId),
    index('MessagingChannel_provider_teamId_idx').on(table.provider, table.teamId),
    uniqueIndex('MessagingChannel_emailAccountId_provider_teamId_key').on(
      table.emailAccountId,
      table.provider,
      table.teamId,
    ),
    unique('MessagingChannel_id_emailAccountId_key').on(table.id, table.emailAccountId),
    foreignKey({
      name: 'MessagingChannel_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const messagingRoute = pgTable(
  'MessagingRoute',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    purpose: messagingRoutePurposeEnum('purpose').notNull(),
    targetType: messagingRouteTargetTypeEnum('targetType').notNull(),
    targetId: text('targetId').notNull(),
    messagingChannelId: text('messagingChannelId').notNull(),
  },
  (table) => [
    uniqueIndex('MessagingRoute_messagingChannelId_purpose_key').on(
      table.messagingChannelId,
      table.purpose,
    ),
    foreignKey({
      name: 'MessagingRoute_messagingChannelId_fkey',
      columns: [table.messagingChannelId],
      foreignColumns: [messagingChannel.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const automationJob = pgTable(
  'AutomationJob',
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
    enabled: boolean('enabled').notNull().default(true),
    prompt: text('prompt'),
    cronExpression: text('cronExpression').notNull(),
    nextRunAt: timestamp('nextRunAt', { precision: 3 }).notNull(),
    messagingChannelId: text('messagingChannelId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('AutomationJob_emailAccountId_enabled_nextRunAt_idx').on(
      table.emailAccountId,
      table.enabled,
      table.nextRunAt,
    ),
    index('AutomationJob_enabled_nextRunAt_idx').on(table.enabled, table.nextRunAt),
    index('AutomationJob_messagingChannelId_emailAccountId_idx').on(
      table.messagingChannelId,
      table.emailAccountId,
    ),
    uniqueIndex('AutomationJob_emailAccountId_key').on(table.emailAccountId),
    foreignKey({
      name: 'AutomationJob_messagingChannelId_emailAccountId_fkey',
      columns: [table.messagingChannelId, table.emailAccountId],
      foreignColumns: [messagingChannel.id, messagingChannel.emailAccountId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const automationJobRun = pgTable(
  'AutomationJobRun',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    status: automationJobRunStatusEnum('status').notNull().default('PENDING'),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }).notNull(),
    processedAt: timestamp('processedAt', { precision: 3 }),
    outboundMessage: text('outboundMessage'),
    providerMessageId: text('providerMessageId'),
    error: text('error'),
    automationJobId: text('automationJobId').notNull(),
  },
  (table) => [
    index('AutomationJobRun_automationJobId_createdAt_idx').on(
      table.automationJobId,
      table.createdAt,
    ),
    index('AutomationJobRun_status_createdAt_idx').on(table.status, table.createdAt),
    uniqueIndex('AutomationJobRun_automationJobId_scheduledFor_key').on(
      table.automationJobId,
      table.scheduledFor,
    ),
    foreignKey({
      name: 'AutomationJobRun_automationJobId_fkey',
      columns: [table.automationJobId],
      foreignColumns: [automationJob.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const driveConnection = pgTable(
  'DriveConnection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    provider: text('provider').notNull(),
    email: text('email').notNull(),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    isConnected: boolean('isConnected').notNull().default(true),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('DriveConnection_emailAccountId_idx').on(table.emailAccountId),
    uniqueIndex('DriveConnection_emailAccountId_provider_key').on(
      table.emailAccountId,
      table.provider,
    ),
    foreignKey({
      name: 'DriveConnection_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const filingFolder = pgTable(
  'FilingFolder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    folderId: text('folderId').notNull(),
    folderName: text('folderName').notNull(),
    folderPath: text('folderPath').notNull(),
    driveConnectionId: text('driveConnectionId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    index('FilingFolder_driveConnectionId_idx').on(table.driveConnectionId),
    uniqueIndex('FilingFolder_emailAccountId_folderId_key').on(
      table.emailAccountId,
      table.folderId,
    ),
    foreignKey({
      name: 'FilingFolder_driveConnectionId_fkey',
      columns: [table.driveConnectionId],
      foreignColumns: [driveConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FilingFolder_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentFiling = pgTable(
  'DocumentFiling',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    messageId: text('messageId').notNull(),
    attachmentId: text('attachmentId').notNull(),
    filename: text('filename').notNull(),
    folderId: text('folderId'),
    folderPath: text('folderPath').notNull(),
    fileId: text('fileId'),
    reasoning: text('reasoning'),
    confidence: doublePrecision('confidence'),
    status: documentFilingStatusEnum('status').notNull().default('FILED'),
    wasAsked: boolean('wasAsked').notNull().default(false),
    wasCorrected: boolean('wasCorrected').notNull().default(false),
    originalPath: text('originalPath'),
    correctedAt: timestamp('correctedAt', { precision: 3 }),
    feedbackPositive: boolean('feedbackPositive'),
    feedbackAt: timestamp('feedbackAt', { precision: 3 }),
    notificationMessageId: text('notificationMessageId'),
    notificationSentAt: timestamp('notificationSentAt', { precision: 3 }),
    notificationBatchId: text('notificationBatchId'),
    driveConnectionId: text('driveConnectionId').notNull(),
    emailAccountId: text('emailAccountId').notNull(),
  },
  (table) => [
    uniqueIndex('DocumentFiling_notificationMessageId_key').on(table.notificationMessageId),
    index('DocumentFiling_emailAccountId_status_idx').on(table.emailAccountId, table.status),
    index('DocumentFiling_emailAccountId_notificationBatchId_idx').on(
      table.emailAccountId,
      table.notificationBatchId,
    ),
    index('DocumentFiling_driveConnectionId_idx').on(table.driveConnectionId),
    index('DocumentFiling_messageId_idx').on(table.messageId),
    uniqueIndex('DocumentFiling_emailAccountId_messageId_attachmentId_key').on(
      table.emailAccountId,
      table.messageId,
      table.attachmentId,
    ),
    foreignKey({
      name: 'DocumentFiling_driveConnectionId_fkey',
      columns: [table.driveConnectionId],
      foreignColumns: [driveConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentFiling_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attachmentSource = pgTable(
  'AttachmentSource',
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
    type: attachmentSourceTypeEnum('type').notNull(),
    sourceId: text('sourceId').notNull(),
    sourcePath: text('sourcePath'),
    ruleId: text('ruleId').notNull(),
    driveConnectionId: text('driveConnectionId').notNull(),
  },
  (table) => [
    index('AttachmentSource_ruleId_idx').on(table.ruleId),
    index('AttachmentSource_driveConnectionId_idx').on(table.driveConnectionId),
    uniqueIndex('AttachmentSource_ruleId_driveConnectionId_type_sourceId_key').on(
      table.ruleId,
      table.driveConnectionId,
      table.type,
      table.sourceId,
    ),
    foreignKey({
      name: 'AttachmentSource_ruleId_fkey',
      columns: [table.ruleId],
      foreignColumns: [rule.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AttachmentSource_driveConnectionId_fkey',
      columns: [table.driveConnectionId],
      foreignColumns: [driveConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attachmentDocument = pgTable(
  'AttachmentDocument',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    attachmentSourceId: text('attachmentSourceId').notNull(),
    fileId: text('fileId').notNull(),
    name: text('name').notNull(),
    mimeType: text('mimeType').notNull(),
    modifiedAt: timestamp('modifiedAt', { precision: 3 }),
    summary: text('summary'),
    content: text('content'),
    metadata: jsonb('metadata'),
    indexedAt: timestamp('indexedAt', { precision: 3 }),
    error: text('error'),
  },
  (table) => [
    index('AttachmentDocument_attachmentSourceId_modifiedAt_idx').on(
      table.attachmentSourceId,
      table.modifiedAt,
    ),
    uniqueIndex('AttachmentDocument_attachmentSourceId_fileId_key').on(
      table.attachmentSourceId,
      table.fileId,
    ),
    foreignKey({
      name: 'AttachmentDocument_attachmentSourceId_fkey',
      columns: [table.attachmentSourceId],
      foreignColumns: [attachmentSource.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const referral = pgTable(
  'Referral',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    referrerUserId: text('referrerUserId').notNull(),
    referredUserId: text('referredUserId').notNull(),
    referralCodeUsed: text('referralCodeUsed').notNull(),
    status: referralStatusEnum('status').notNull().default('PENDING'),
    rewardGrantedAt: timestamp('rewardGrantedAt', { precision: 3 }),
    stripeBalanceTransactionId: text('stripeBalanceTransactionId'),
    rewardAmount: integer('rewardAmount'),
  },
  (table) => [
    uniqueIndex('Referral_referredUserId_key').on(table.referredUserId),
    index('Referral_referrerUserId_idx').on(table.referrerUserId),
    index('Referral_status_idx').on(table.status),
    foreignKey({
      name: 'Referral_referrerUserId_fkey',
      columns: [table.referrerUserId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Referral_referredUserId_fkey',
      columns: [table.referredUserId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mcpIntegration = pgTable(
  'McpIntegration',
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
    registeredServerUrl: text('registeredServerUrl'),
    registeredAuthorizationUrl: text('registeredAuthorizationUrl'),
    registeredTokenUrl: text('registeredTokenUrl'),
    oauthClientId: text('oauthClientId'),
    oauthClientSecret: text('oauthClientSecret'),
    displayName: text('displayName'),
    serverUrl: text('serverUrl'),
    authType: mcpAuthTypeEnum('authType'),
    emailAccountId: text('emailAccountId'),
  },
  (table) => [
    uniqueIndex('McpIntegration_name_key').on(table.name),
    index('McpIntegration_emailAccountId_idx').on(table.emailAccountId),
    foreignKey({
      name: 'McpIntegration_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mcpConnection = pgTable(
  'McpConnection',
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
    isActive: boolean('isActive').notNull().default(true),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    apiKey: text('apiKey'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    integrationId: text('integrationId').notNull(),
    emailAccountId: text('emailAccountId'),
  },
  (table) => [
    uniqueIndex('McpConnection_emailAccountId_integrationId_key').on(
      table.emailAccountId,
      table.integrationId,
    ),
    foreignKey({
      name: 'McpConnection_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [mcpIntegration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'McpConnection_emailAccountId_fkey',
      columns: [table.emailAccountId],
      foreignColumns: [emailAccount.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mcpTool = pgTable(
  'McpTool',
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
    description: text('description'),
    schema: jsonb('schema'),
    isEnabled: boolean('isEnabled').notNull().default(true),
    isWrite: boolean('isWrite').notNull().default(false),
    connectionId: text('connectionId').notNull(),
  },
  (table) => [
    uniqueIndex('McpTool_connectionId_name_key').on(table.connectionId, table.name),
    foreignKey({
      name: 'McpTool_connectionId_fkey',
      columns: [table.connectionId],
      foreignColumns: [mcpConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mobilePushToken = pgTable(
  'MobilePushToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    token: text('token').notNull(),
    platform: mobilePushPlatformEnum('platform').notNull(),
    tokenType: mobilePushTokenTypeEnum('tokenType').notNull().default('expo'),
    appVersion: text('appVersion'),
    userId: text('userId').notNull(),
  },
  (table) => [
    uniqueIndex('MobilePushToken_token_key').on(table.token),
    index('MobilePushToken_userId_idx').on(table.userId),
    foreignKey({
      name: 'MobilePushToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const mobilePushDelivery = pgTable(
  'MobilePushDelivery',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deduplicationKey: text('deduplicationKey').notNull(),
    mobilePushTokenId: text('mobilePushTokenId').notNull(),
  },
  (table) => [
    index('MobilePushDelivery_createdAt_idx').on(table.createdAt),
    index('MobilePushDelivery_mobilePushTokenId_idx').on(table.mobilePushTokenId),
    uniqueIndex('MobilePushDelivery_key_token_key').on(
      table.deduplicationKey,
      table.mobilePushTokenId,
    ),
    foreignKey({
      name: 'MobilePushDelivery_mobilePushTokenId_fkey',
      columns: [table.mobilePushTokenId],
      foreignColumns: [mobilePushToken.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthClient = pgTable(
  'OauthClient',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    clientSecret: text('clientSecret'),
    clientDiscoveryId: text('clientDiscoveryId'),
    disabled: boolean('disabled').default(false),
    skipConsent: boolean('skipConsent'),
    enableEndSession: boolean('enableEndSession'),
    subjectType: text('subjectType'),
    scopes: text('scopes').array(),
    clientCredentialsScopes: text('clientCredentialsScopes').array().default([]),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 }),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    name: text('name'),
    uri: text('uri'),
    icon: text('icon'),
    contacts: text('contacts').array(),
    tos: text('tos'),
    policy: text('policy'),
    softwareId: text('softwareId'),
    softwareVersion: text('softwareVersion'),
    softwareStatement: text('softwareStatement'),
    redirectUris: text('redirectUris').array(),
    postLogoutRedirectUris: text('postLogoutRedirectUris').array(),
    backchannelLogoutUri: text('backchannelLogoutUri'),
    backchannelLogoutSessionRequired: boolean('backchannelLogoutSessionRequired'),
    tokenEndpointAuthMethod: text('tokenEndpointAuthMethod'),
    applicationType: text('applicationType'),
    jwks: text('jwks'),
    jwksUri: text('jwksUri'),
    grantTypes: text('grantTypes').array(),
    responseTypes: text('responseTypes').array(),
    requirePKCE: boolean('requirePKCE'),
    dpopBoundAccessTokens: boolean('dpopBoundAccessTokens').default(false),
    referenceId: text('referenceId'),
    metadata: jsonb('metadata'),
  },
  (table) => [
    index('OauthClient_userId_idx').on(table.userId),
    unique('OauthClient_clientId_key').on(table.clientId),
    foreignKey({
      name: 'OauthClient_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthRefreshToken = pgTable(
  'OauthRefreshToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    clientId: text('clientId').notNull(),
    sessionId: text('sessionId'),
    userId: text('userId').notNull(),
    referenceId: text('referenceId'),
    authorizationCodeId: text('authorizationCodeId'),
    resources: text('resources').array(),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    revoked: timestamp('revoked', { precision: 3 }),
    rotatedAt: timestamp('rotatedAt', { precision: 3 }),
    rotationReplayResponse: text('rotationReplayResponse'),
    rotationReplayExpiresAt: timestamp('rotationReplayExpiresAt', { precision: 3 }),
    authTime: timestamp('authTime', { precision: 3 }),
    confirmation: jsonb('confirmation'),
    scopes: text('scopes').array(),
  },
  (table) => [
    index('OauthRefreshToken_clientId_idx').on(table.clientId),
    index('OauthRefreshToken_sessionId_idx').on(table.sessionId),
    index('OauthRefreshToken_userId_idx').on(table.userId),
    index('OauthRefreshToken_authorizationCodeId_idx').on(table.authorizationCodeId),
    uniqueIndex('OauthRefreshToken_token_key').on(table.token),
    foreignKey({
      name: 'OauthRefreshToken_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthRefreshToken_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthRefreshToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthAccessToken = pgTable(
  'OauthAccessToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    clientId: text('clientId').notNull(),
    sessionId: text('sessionId'),
    userId: text('userId'),
    referenceId: text('referenceId'),
    authorizationCodeId: text('authorizationCodeId'),
    resources: text('resources').array(),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array(),
    refreshId: text('refreshId'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    revoked: timestamp('revoked', { precision: 3 }),
    confirmation: jsonb('confirmation'),
    scopes: text('scopes').array(),
  },
  (table) => [
    index('OauthAccessToken_clientId_idx').on(table.clientId),
    index('OauthAccessToken_sessionId_idx').on(table.sessionId),
    index('OauthAccessToken_userId_idx').on(table.userId),
    index('OauthAccessToken_authorizationCodeId_idx').on(table.authorizationCodeId),
    index('OauthAccessToken_refreshId_idx').on(table.refreshId),
    uniqueIndex('OauthAccessToken_token_key').on(table.token),
    foreignKey({
      name: 'OauthAccessToken_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthAccessToken_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthAccessToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthAccessToken_refreshId_fkey',
      columns: [table.refreshId],
      foreignColumns: [oauthRefreshToken.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthConsent = pgTable(
  'OauthConsent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    userId: text('userId'),
    referenceId: text('referenceId'),
    resources: text('resources').array(),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array(),
    scopes: text('scopes').array(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('OauthConsent_userId_idx').on(table.userId),
    uniqueIndex('OauthConsent_clientId_userId_key').on(table.clientId, table.userId),
    foreignKey({
      name: 'OauthConsent_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthConsent_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const jwks = pgTable('Jwks', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  publicKey: text('publicKey').notNull(),
  privateKey: text('privateKey').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }),
  alg: text('alg'),
  crv: text('crv'),
})

export const oauthResource = pgTable(
  'OauthResource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    identifier: text('identifier').notNull(),
    name: text('name').notNull(),
    accessTokenTtl: integer('accessTokenTtl'),
    refreshTokenTtl: integer('refreshTokenTtl'),
    signingAlgorithm: text('signingAlgorithm'),
    signingKeyId: text('signingKeyId'),
    allowedScopes: text('allowedScopes').array(),
    customClaims: jsonb('customClaims'),
    dpopBoundAccessTokensRequired: boolean('dpopBoundAccessTokensRequired').default(false),
    disabled: boolean('disabled').default(false),
    createdAt: timestamp('createdAt', { precision: 3 }),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    policyVersion: integer('policyVersion').default(1),
    metadata: jsonb('metadata'),
  },
  (table) => [unique('OauthResource_identifier_key').on(table.identifier)],
)

export const oauthClientResource = pgTable(
  'OauthClientResource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    resourceId: text('resourceId').notNull(),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 }),
  },
  (table) => [
    index('OauthClientResource_clientId_idx').on(table.clientId),
    index('OauthClientResource_resourceId_idx').on(table.resourceId),
    uniqueIndex('oauthClientResource_clientId_resourceId_uidx').on(
      table.clientId,
      table.resourceId,
    ),
    foreignKey({
      name: 'OauthClientResource_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'OauthClientResource_resourceId_fkey',
      columns: [table.resourceId],
      foreignColumns: [oauthResource.identifier],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthClientAssertion = pgTable('OauthClientAssertion', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
})

export const scimManagedConnection = pgTable(
  'scimManagedConnection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    creationRequestId: text('creationRequestId').notNull(),
    connectionId: text('connectionId').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    status: text('status').notNull(),
    revision: integer('revision').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    createdBy: text('createdBy').notNull(),
    decommissionStartedAt: timestamp('decommissionStartedAt', { precision: 3 }),
    decommissionStartedBy: text('decommissionStartedBy'),
    decommissionedAt: timestamp('decommissionedAt', { precision: 3 }),
    decommissionedBy: text('decommissionedBy'),
  },
  (table) => [
    index('scimManagedConnection_provisioningDomainId_idx').on(table.provisioningDomainId),
    uniqueIndex('scimManagedConnection_creationRequestId_key').on(table.creationRequestId),
    uniqueIndex('scimManagedConnection_connectionId_key').on(table.connectionId),
  ],
)

export const scimManagedCredential = pgTable(
  'scimManagedCredential',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionRecordId: text('connectionRecordId').notNull(),
    credentialId: text('credentialId').notNull(),
    tokenDigest: text('tokenDigest').notNull(),
    hashVersion: text('hashVersion').notNull(),
    activeSlotKey: text('activeSlotKey').notNull(),
    status: text('status').notNull(),
    serializedScopes: text('serializedScopes').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    createdBy: text('createdBy').notNull(),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedBy: text('revokedBy'),
    decommissionedAt: timestamp('decommissionedAt', { precision: 3 }),
  },
  (table) => [
    index('scimManagedCredential_connectionRecordId_idx').on(table.connectionRecordId),
    uniqueIndex('scimManagedCredential_credentialId_key').on(table.credentialId),
    uniqueIndex('scimManagedCredential_activeSlotKey_key').on(table.activeSlotKey),
    foreignKey({
      name: 'scimManagedCredential_connectionRecordId_fkey',
      columns: [table.connectionRecordId],
      foreignColumns: [scimManagedConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimManagedConnectionEvent = pgTable(
  'scimManagedConnectionEvent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionRecordId: text('connectionRecordId').notNull(),
    eventKey: text('eventKey').notNull(),
    sequence: integer('sequence').notNull(),
    type: text('type').notNull(),
    actorId: text('actorId').notNull(),
    credentialId: text('credentialId'),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimManagedConnectionEvent_connectionRecordId_idx').on(table.connectionRecordId),
    uniqueIndex('scimManagedConnectionEvent_eventKey_key').on(table.eventKey),
    foreignKey({
      name: 'scimManagedConnectionEvent_connectionRecordId_fkey',
      columns: [table.connectionRecordId],
      foreignColumns: [scimManagedConnection.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimConnectionBinding = pgTable(
  'scimConnectionBinding',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    connectionKey: text('connectionKey').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    decommissionedAt: timestamp('decommissionedAt', { precision: 3 }),
    decommissionStatus: text('decommissionStatus').notNull().default('active'),
    decommissionCursorUserId: text('decommissionCursorUserId'),
    decommissionReconciledUserCount: integer('decommissionReconciledUserCount')
      .notNull()
      .default(0),
    decommissionBatchCount: integer('decommissionBatchCount').notNull().default(0),
    decommissionRevision: integer('decommissionRevision').notNull().default(0),
    decommissionCompletedAt: timestamp('decommissionCompletedAt', { precision: 3 }),
    decommissionLeaseId: text('decommissionLeaseId'),
    decommissionLeaseExpiresAt: timestamp('decommissionLeaseExpiresAt', { precision: 3 }),
  },
  (table) => [
    index('scimConnectionBinding_connectionId_idx').on(table.connectionId),
    uniqueIndex('scimConnectionBinding_connectionKey_key').on(table.connectionKey),
  ],
)

export const scimIdentityTombstone = pgTable(
  'scimIdentityTombstone',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    externalId: text('externalId').notNull(),
    externalIdKey: text('externalIdKey').notNull(),
    userId: text('userId').notNull(),
    profile: text('profile').notNull(),
    deletedAt: timestamp('deletedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimIdentityTombstone_connectionId_idx').on(table.connectionId),
    index('scimIdentityTombstone_provisioningDomainId_idx').on(table.provisioningDomainId),
    index('scimIdentityTombstone_userId_idx').on(table.userId),
    uniqueIndex('scimIdentityTombstone_externalIdKey_key').on(table.externalIdKey),
    foreignKey({
      name: 'scimIdentityTombstone_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimSubject = pgTable(
  'scimSubject',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    profileSourceId: text('profileSourceId'),
    revision: integer('revision').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimSubject_profileSourceId_idx').on(table.profileSourceId),
    uniqueIndex('scimSubject_userId_key').on(table.userId),
    foreignKey({
      name: 'scimSubject_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimUser = pgTable(
  'scimUser',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    userId: text('userId').notNull(),
    connectionUserKey: text('connectionUserKey').notNull(),
    userName: text('userName').notNull(),
    userNameKey: text('userNameKey').notNull(),
    primaryEmail: text('primaryEmail').notNull(),
    workEmailValueIndex: text('workEmailValueIndex').notNull(),
    emailValueIndex: text('emailValueIndex').notNull(),
    displayName: text('displayName').notNull(),
    formattedName: text('formattedName').notNull(),
    givenName: text('givenName'),
    familyName: text('familyName'),
    serializedEmails: text('serializedEmails').notNull(),
    serializedAttributes: text('serializedAttributes'),
    externalId: text('externalId'),
    externalIdKey: text('externalIdKey'),
    active: boolean('active').notNull(),
    orderKey: text('orderKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimUser_connectionId_idx').on(table.connectionId),
    index('scimUser_provisioningDomainId_idx').on(table.provisioningDomainId),
    index('scimUser_userId_idx').on(table.userId),
    uniqueIndex('scimUser_connectionUserKey_key').on(table.connectionUserKey),
    uniqueIndex('scimUser_userNameKey_key').on(table.userNameKey),
    uniqueIndex('scimUser_externalIdKey_key').on(table.externalIdKey),
    uniqueIndex('scimUser_orderKey_key').on(table.orderKey),
    foreignKey({ name: 'scimUser_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimProjectionGrant = pgTable(
  'scimProjectionGrant',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    scimUserId: text('scimUserId').notNull(),
    userId: text('userId').notNull(),
    sourceKind: text('sourceKind').notNull(),
    sourceId: text('sourceId').notNull(),
    sourceValue: text('sourceValue'),
    role: text('role').notNull(),
    grantKey: text('grantKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimProjectionGrant_connectionId_idx').on(table.connectionId),
    index('scimProjectionGrant_provisioningDomainId_idx').on(table.provisioningDomainId),
    index('scimProjectionGrant_scimUserId_idx').on(table.scimUserId),
    index('scimProjectionGrant_userId_idx').on(table.userId),
    uniqueIndex('scimProjectionGrant_grantKey_key').on(table.grantKey),
    foreignKey({
      name: 'scimProjectionGrant_scimUserId_fkey',
      columns: [table.scimUserId],
      foreignColumns: [scimUser.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scimProjectionGrant_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimGroup = pgTable(
  'scimGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    provisioningDomainId: text('provisioningDomainId').notNull(),
    revision: integer('revision').notNull().default(0),
    displayName: text('displayName').notNull(),
    displayNameKey: text('displayNameKey').notNull(),
    externalId: text('externalId'),
    externalIdKey: text('externalIdKey'),
    orderKey: text('orderKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimGroup_connectionId_idx').on(table.connectionId),
    index('scimGroup_provisioningDomainId_idx').on(table.provisioningDomainId),
    uniqueIndex('scimGroup_displayNameKey_key').on(table.displayNameKey),
    uniqueIndex('scimGroup_externalIdKey_key').on(table.externalIdKey),
    uniqueIndex('scimGroup_orderKey_key').on(table.orderKey),
  ],
)

export const scimGroupMember = pgTable(
  'scimGroupMember',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    groupId: text('groupId').notNull(),
    scimUserId: text('scimUserId').notNull(),
    membershipKey: text('membershipKey').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('scimGroupMember_connectionId_idx').on(table.connectionId),
    index('scimGroupMember_groupId_idx').on(table.groupId),
    index('scimGroupMember_scimUserId_idx').on(table.scimUserId),
    uniqueIndex('scimGroupMember_membershipKey_key').on(table.membershipKey),
    foreignKey({
      name: 'scimGroupMember_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [scimGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'scimGroupMember_scimUserId_fkey',
      columns: [table.scimUserId],
      foreignColumns: [scimUser.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const scimIdentityLink = pgTable(
  'ScimIdentityLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    connectionId: text('connectionId').notNull(),
    externalId: text('externalId').notNull(),
    userId: text('userId').notNull(),
  },
  (table) => [
    index('ScimIdentityLink_userId_idx').on(table.userId),
    uniqueIndex('ScimIdentityLink_connectionId_externalId_key').on(
      table.connectionId,
      table.externalId,
    ),
    foreignKey({
      name: 'ScimIdentityLink_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const loopsEmailSource = pgTable(
  'LoopsEmailSource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    emailMessageId: text('emailMessageId'),
    loopId: text('loopId'),
    loopName: text('loopName'),
    campaignId: text('campaignId'),
    campaignName: text('campaignName'),
    sourceType: text('sourceType'),
    eventTime: timestamp('eventTime', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('LoopsEmailSource_emailMessageId_key').on(table.emailMessageId),
    index('LoopsEmailSource_loopId_idx').on(table.loopId),
    index('LoopsEmailSource_campaignId_idx').on(table.campaignId),
    uniqueIndex('LoopsEmailSource_loopId_unversioned_key').on(table.loopId),
    uniqueIndex('LoopsEmailSource_campaignId_unversioned_key').on(table.campaignId),
  ],
)

export const categoryToRule = pgTable(
  '_CategoryToRule',
  { A: text('A').notNull(), B: text('B').notNull() },
  (table) => [
    primaryKey({ name: '_CategoryToRule_AB_pkey', columns: [table.A, table.B] }),
    index('_CategoryToRule_B_index').on(table.B),
    foreignKey({
      name: '_CategoryToRule_A_fkey',
      columns: [table.A],
      foreignColumns: [category.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_CategoryToRule_B_fkey', columns: [table.B], foreignColumns: [rule.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    account,
    session,
    user,
    emailAccount,
    mailSplit,
    mailSplitFilter,
    organization,
    organizationRule,
    organizationRuleAction,
    member,
    conversation,
    conversationParticipant,
    conversationComment,
    conversationActivity,
    invitation,
    verification,
    ssoProvider,
    digest,
    digestItem,
    schedule,
    premium,
    pendingPremiumGrant,
    verificationToken,
    label,
    rule,
    action,
    ruleHistory,
    executedRule,
    executedAction,
    scheduledAction,
    snoozedThread,
    scheduledEmail,
    emailSendOperation,
    sentMessageOpen,
    group,
    groupItem,
    classificationFeedback,
    category,
    newsletter,
    coldEmail,
    emailMessage,
    responseTime,
    threadTracker,
    cleanupJob,
    cleanupThread,
    knowledge,
    snippet,
    emailSignature,
    replyMemory,
    replyMemorySource,
    apiKey,
    emailToken,
    payment,
    draftSendLog,
    chat,
    chatMessage,
    chatCompaction,
    chatMemory,
    calendarConnection,
    calendar,
    bookingLink,
    availabilitySchedule,
    availabilityWindow,
    booking,
    meetingBriefing,
    contactResearch,
    meetingRecording,
    meeting,
    messagingChannel,
    messagingRoute,
    automationJob,
    automationJobRun,
    driveConnection,
    filingFolder,
    documentFiling,
    attachmentSource,
    attachmentDocument,
    referral,
    mcpIntegration,
    mcpConnection,
    mcpTool,
    mobilePushToken,
    mobilePushDelivery,
    oauthClient,
    oauthRefreshToken,
    oauthAccessToken,
    oauthConsent,
    jwks,
    oauthResource,
    oauthClientResource,
    oauthClientAssertion,
    scimManagedConnection,
    scimManagedCredential,
    scimManagedConnectionEvent,
    scimConnectionBinding,
    scimIdentityTombstone,
    scimSubject,
    scimUser,
    scimProjectionGrant,
    scimGroup,
    scimGroupMember,
    scimIdentityLink,
    loopsEmailSource,
    categoryToRule,
  },
  (r) => ({
    account: {
      user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }),
      emailAccount: r.one.emailAccount({ from: r.account.id, to: r.emailAccount.accountId }),
    },
    session: {
      user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }),
      activeOrganization: r.one.organization({
        from: r.session.activeOrganizationId,
        to: r.organization.id,
      }),
      oauthRefreshTokenRecords: r.many.oauthRefreshToken(),
      oauthAccessTokenRecords: r.many.oauthAccessToken(),
    },
    user: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      premium: r.one.premium({ from: r.user.premiumId, to: r.premium.id, alias: 'userPremium' }),
      premiumAdmin: r.one.premium({
        from: r.user.premiumAdminId,
        to: r.premium.id,
        alias: 'PremiumToUser',
      }),
      apiKeys: r.many.apiKey(),
      emailAccounts: r.many.emailAccount(),
      referralsMade: r.many.referral({ alias: 'ReferrerUser' }),
      referralReceived: r.one.referral({
        from: r.user.id,
        to: r.referral.referredUserId,
        alias: 'ReferredUser',
      }),
      mobilePushTokens: r.many.mobilePushToken(),
      oauthClientRecords: r.many.oauthClient(),
      oauthRefreshTokenRecords: r.many.oauthRefreshToken(),
      oauthAccessTokenRecords: r.many.oauthAccessToken(),
      oauthConsentRecords: r.many.oauthConsent(),
      ssoProviders: r.many.ssoProvider(),
      scimIdentityTombstones: r.many.scimIdentityTombstone(),
      scimSubject: r.one.scimSubject({ from: r.user.id, to: r.scimSubject.userId }),
      scimUsers: r.many.scimUser(),
      scimProjectionGrants: r.many.scimProjectionGrant(),
      scimIdentityLinks: r.many.scimIdentityLink(),
    },
    emailAccount: {
      digestSchedule: r.one.schedule({ from: r.emailAccount.id, to: r.schedule.emailAccountId }),
      user: r.one.user({ from: r.emailAccount.userId, to: r.user.id, optional: false }),
      account: r.one.account({ from: r.emailAccount.accountId, to: r.account.id, optional: false }),
      labels: r.many.label(),
      rules: r.many.rule(),
      executedRules: r.many.executedRule(),
      newsletters: r.many.newsletter(),
      coldEmails: r.many.coldEmail(),
      groups: r.many.group(),
      categories: r.many.category(),
      threadTrackers: r.many.threadTracker(),
      cleanupJobs: r.many.cleanupJob(),
      cleanupThreads: r.many.cleanupThread(),
      emailMessages: r.many.emailMessage(),
      emailTokens: r.many.emailToken(),
      knowledge: r.many.knowledge(),
      snippets: r.many.snippet(),
      emailSignatures: r.many.emailSignature(),
      replyMemories: r.many.replyMemory(),
      chats: r.many.chat(),
      chatMemories: r.many.chatMemory(),
      digests: r.many.digest(),
      scheduledActions: r.many.scheduledAction(),
      snoozedThreads: r.many.snoozedThread(),
      emailSendOperations: r.many.emailSendOperation(),
      sentMessageOpens: r.many.sentMessageOpen(),
      scheduledEmails: r.many.scheduledEmail(),
      responseTimes: r.many.responseTime(),
      actions: r.many.action(),
      members: r.many.member(),
      invitations: r.many.invitation(),
      ssoproviders: r.many.ssoProvider(),
      calendarConnections: r.many.calendarConnection(),
      availabilitySchedules: r.many.availabilitySchedule(),
      bookingLinks: r.many.bookingLink(),
      bookings: r.many.booking(),
      mcpConnections: r.many.mcpConnection(),
      mcpIntegrations: r.many.mcpIntegration(),
      meetingBriefings: r.many.meetingBriefing(),
      meetings: r.many.meeting(),
      meetingRecordings: r.many.meetingRecording(),
      driveConnections: r.many.driveConnection(),
      messagingChannels: r.many.messagingChannel(),
      documentFilings: r.many.documentFiling(),
      filingFolders: r.many.filingFolder(),
      apiKeys: r.many.apiKey(),
      classificationFeedback: r.many.classificationFeedback(),
      mailSplits: r.many.mailSplit(),
    },
    mailSplit: {
      emailAccount: r.one.emailAccount({
        from: r.mailSplit.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      filters: r.many.mailSplitFilter(),
    },
    mailSplitFilter: {
      mailSplit: r.one.mailSplit({
        from: r.mailSplitFilter.mailSplitId,
        to: r.mailSplit.id,
        optional: false,
      }),
    },
    organization: {
      members: r.many.member(),
      SsoProvider: r.one.ssoProvider({ from: r.organization.id, to: r.ssoProvider.organizationId }),
      sessions: r.many.session(),
      invitations: r.many.invitation(),
      rules: r.many.organizationRule(),
      conversations: r.many.conversation(),
    },
    organizationRule: {
      organization: r.one.organization({
        from: r.organizationRule.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      actions: r.many.organizationRuleAction(),
      memberRules: r.many.rule(),
    },
    organizationRuleAction: {
      organizationRule: r.one.organizationRule({
        from: r.organizationRuleAction.organizationRuleId,
        to: r.organizationRule.id,
        optional: false,
      }),
    },
    member: {
      organization: r.one.organization({
        from: r.member.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.member.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      conversations: r.many.conversation(),
      conversationParticipants: r.many.conversationParticipant(),
      conversationComments: r.many.conversationComment(),
    },
    conversation: {
      organization: r.one.organization({
        from: r.conversation.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      publisher: r.one.member({
        from: r.conversation.publisherId,
        to: r.member.id,
        optional: false,
      }),
      participants: r.many.conversationParticipant(),
      comments: r.many.conversationComment(),
    },
    conversationParticipant: {
      conversation: r.one.conversation({
        from: r.conversationParticipant.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
      member: r.one.member({
        from: r.conversationParticipant.memberId,
        to: r.member.id,
        optional: false,
      }),
      activity: r.many.conversationActivity(),
    },
    conversationComment: {
      conversation: r.one.conversation({
        from: r.conversationComment.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
      author: r.one.member({ from: r.conversationComment.authorMemberId, to: r.member.id }),
      activity: r.many.conversationActivity(),
    },
    conversationActivity: {
      participant: r.one.conversationParticipant({
        from: r.conversationActivity.participantId,
        to: r.conversationParticipant.id,
        optional: false,
      }),
      comment: r.one.conversationComment({
        from: r.conversationActivity.commentId,
        to: r.conversationComment.id,
      }),
    },
    invitation: {
      organization: r.one.organization({
        from: r.invitation.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      inviter: r.one.emailAccount({
        from: r.invitation.inviterId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    ssoProvider: {
      user: r.one.user({ from: r.ssoProvider.userId, to: r.user.id }),
      emailAccount: r.one.emailAccount({
        from: r.ssoProvider.emailAccountId,
        to: r.emailAccount.id,
      }),
      organization: r.one.organization({
        from: r.ssoProvider.organizationId,
        to: r.organization.id,
      }),
    },
    digest: {
      emailAccount: r.one.emailAccount({
        from: r.digest.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      items: r.many.digestItem(),
    },
    digestItem: {
      digest: r.one.digest({ from: r.digestItem.digestId, to: r.digest.id, optional: false }),
      action: r.one.executedAction({ from: r.digestItem.actionId, to: r.executedAction.id }),
      coldEmail: r.one.coldEmail({ from: r.digestItem.coldEmailId, to: r.coldEmail.id }),
    },
    schedule: {
      emailAccount: r.one.emailAccount({
        from: r.schedule.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    premium: {
      users: r.many.user({ alias: 'userPremium' }),
      admins: r.many.user({ alias: 'PremiumToUser' }),
      payments: r.many.payment(),
    },
    label: {
      emailAccount: r.one.emailAccount({
        from: r.label.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    rule: {
      actions: r.many.action(),
      attachmentSources: r.many.attachmentSource(),
      emailAccount: r.one.emailAccount({
        from: r.rule.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      organizationRule: r.one.organizationRule({
        from: r.rule.organizationRuleId,
        to: r.organizationRule.id,
      }),
      executedRules: r.many.executedRule(),
      classificationFeedback: r.many.classificationFeedback(),
      group: r.one.group({ from: r.rule.groupId, to: r.group.id }),
      categoryFilters: r.many.category({
        from: r.rule.id.through(r.categoryToRule.B),
        to: r.category.id.through(r.categoryToRule.A),
      }),
      history: r.many.ruleHistory(),
    },
    action: {
      rule: r.one.rule({
        from: [r.action.ruleId, r.action.emailAccountId],
        to: [r.rule.id, r.rule.emailAccountId],
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.action.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      messagingChannel: r.one.messagingChannel({
        from: [r.action.messagingChannelId, r.action.messagingChannelEmailAccountId],
        to: [r.messagingChannel.id, r.messagingChannel.emailAccountId],
      }),
    },
    ruleHistory: {
      rule: r.one.rule({ from: r.ruleHistory.ruleId, to: r.rule.id, optional: false }),
    },
    executedRule: {
      rule: r.one.rule({ from: r.executedRule.ruleId, to: r.rule.id }),
      emailAccount: r.one.emailAccount({
        from: r.executedRule.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      actionItems: r.many.executedAction(),
      scheduledActions: r.many.scheduledAction(),
    },
    executedAction: {
      executedRule: r.one.executedRule({
        from: r.executedAction.executedRuleId,
        to: r.executedRule.id,
        optional: false,
      }),
      messagingChannel: r.one.messagingChannel({
        from: r.executedAction.messagingChannelId,
        to: r.messagingChannel.id,
      }),
      draftSendLog: r.one.draftSendLog({
        from: r.executedAction.id,
        to: r.draftSendLog.executedActionId,
      }),
      digestItems: r.many.digestItem(),
      scheduledAction: r.one.scheduledAction({
        from: r.executedAction.id,
        to: r.scheduledAction.executedActionId,
      }),
    },
    scheduledAction: {
      executedRule: r.one.executedRule({
        from: r.scheduledAction.executedRuleId,
        to: r.executedRule.id,
        optional: false,
      }),
      executedAction: r.one.executedAction({
        from: r.scheduledAction.executedActionId,
        to: r.executedAction.id,
      }),
      emailAccount: r.one.emailAccount({
        from: r.scheduledAction.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    snoozedThread: {
      emailAccount: r.one.emailAccount({
        from: r.snoozedThread.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    scheduledEmail: {
      emailAccount: r.one.emailAccount({
        from: r.scheduledEmail.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    emailSendOperation: {
      emailAccount: r.one.emailAccount({
        from: r.emailSendOperation.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    sentMessageOpen: {
      emailAccount: r.one.emailAccount({
        from: r.sentMessageOpen.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    group: {
      items: r.many.groupItem(),
      rule: r.one.rule({ from: r.group.id, to: r.rule.groupId }),
      emailAccount: r.one.emailAccount({
        from: r.group.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    groupItem: { group: r.one.group({ from: r.groupItem.groupId, to: r.group.id }) },
    classificationFeedback: {
      rule: r.one.rule({ from: r.classificationFeedback.ruleId, to: r.rule.id, optional: false }),
      emailAccount: r.one.emailAccount({
        from: r.classificationFeedback.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    category: {
      emailAccount: r.one.emailAccount({
        from: r.category.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      emailSenders: r.many.newsletter(),
      rules: r.many.rule({
        from: r.category.id.through(r.categoryToRule.A),
        to: r.rule.id.through(r.categoryToRule.B),
      }),
    },
    newsletter: {
      emailAccount: r.one.emailAccount({
        from: r.newsletter.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      category: r.one.category({ from: r.newsletter.categoryId, to: r.category.id }),
    },
    coldEmail: {
      emailAccount: r.one.emailAccount({
        from: r.coldEmail.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      digestItems: r.many.digestItem(),
    },
    emailMessage: {
      emailAccount: r.one.emailAccount({
        from: r.emailMessage.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    responseTime: {
      emailAccount: r.one.emailAccount({
        from: r.responseTime.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    threadTracker: {
      emailAccount: r.one.emailAccount({
        from: r.threadTracker.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    cleanupJob: {
      emailAccount: r.one.emailAccount({
        from: r.cleanupJob.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      threads: r.many.cleanupThread(),
    },
    cleanupThread: {
      job: r.one.cleanupJob({ from: r.cleanupThread.jobId, to: r.cleanupJob.id, optional: false }),
      emailAccount: r.one.emailAccount({
        from: r.cleanupThread.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    knowledge: {
      emailAccount: r.one.emailAccount({
        from: r.knowledge.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    snippet: {
      emailAccount: r.one.emailAccount({
        from: r.snippet.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    emailSignature: {
      emailAccount: r.one.emailAccount({
        from: r.emailSignature.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    replyMemory: {
      emailAccount: r.one.emailAccount({
        from: r.replyMemory.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      sources: r.many.replyMemorySource(),
    },
    replyMemorySource: {
      replyMemory: r.one.replyMemory({
        from: r.replyMemorySource.replyMemoryId,
        to: r.replyMemory.id,
        optional: false,
      }),
      draftSendLog: r.one.draftSendLog({
        from: r.replyMemorySource.draftSendLogId,
        to: r.draftSendLog.id,
        optional: false,
      }),
    },
    apiKey: {
      user: r.one.user({ from: r.apiKey.userId, to: r.user.id, optional: false }),
      emailAccount: r.one.emailAccount({ from: r.apiKey.emailAccountId, to: r.emailAccount.id }),
    },
    emailToken: {
      emailAccount: r.one.emailAccount({
        from: r.emailToken.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    payment: { premium: r.one.premium({ from: r.payment.premiumId, to: r.premium.id }) },
    draftSendLog: {
      executedAction: r.one.executedAction({
        from: r.draftSendLog.executedActionId,
        to: r.executedAction.id,
        optional: false,
      }),
      replyMemorySources: r.many.replyMemorySource(),
    },
    chat: {
      messages: r.many.chatMessage(),
      compactions: r.many.chatCompaction(),
      memories: r.many.chatMemory(),
      emailAccount: r.one.emailAccount({
        from: r.chat.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    chatMessage: {
      chat: r.one.chat({ from: r.chatMessage.chatId, to: r.chat.id, optional: false }),
    },
    chatCompaction: {
      chat: r.one.chat({ from: r.chatCompaction.chatId, to: r.chat.id, optional: false }),
    },
    chatMemory: {
      chat: r.one.chat({ from: r.chatMemory.chatId, to: r.chat.id }),
      emailAccount: r.one.emailAccount({
        from: r.chatMemory.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    calendarConnection: {
      emailAccount: r.one.emailAccount({
        from: r.calendarConnection.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      calendars: r.many.calendar(),
    },
    calendar: {
      connection: r.one.calendarConnection({
        from: r.calendar.connectionId,
        to: r.calendarConnection.id,
        optional: false,
      }),
      bookingLinks: r.many.bookingLink(),
    },
    bookingLink: {
      emailAccount: r.one.emailAccount({
        from: r.bookingLink.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      destinationCalendar: r.one.calendar({
        from: r.bookingLink.destinationCalendarId,
        to: r.calendar.id,
      }),
      availabilitySchedule: r.one.availabilitySchedule({
        from: [r.bookingLink.availabilityScheduleId, r.bookingLink.emailAccountId],
        to: [r.availabilitySchedule.id, r.availabilitySchedule.emailAccountId],
        optional: false,
      }),
      bookings: r.many.booking(),
    },
    availabilitySchedule: {
      emailAccount: r.one.emailAccount({
        from: r.availabilitySchedule.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      windows: r.many.availabilityWindow(),
      bookingLinks: r.many.bookingLink(),
    },
    availabilityWindow: {
      availabilitySchedule: r.one.availabilitySchedule({
        from: r.availabilityWindow.availabilityScheduleId,
        to: r.availabilitySchedule.id,
        optional: false,
      }),
    },
    booking: {
      bookingLink: r.one.bookingLink({
        from: r.booking.bookingLinkId,
        to: r.bookingLink.id,
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.booking.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    meetingBriefing: {
      emailAccount: r.one.emailAccount({
        from: r.meetingBriefing.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    meetingRecording: {
      emailAccount: r.one.emailAccount({
        from: r.meetingRecording.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      meetings: r.many.meeting(),
    },
    meeting: {
      recording: r.one.meetingRecording({ from: r.meeting.recordingId, to: r.meetingRecording.id }),
      emailAccount: r.one.emailAccount({
        from: r.meeting.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    messagingChannel: {
      emailAccount: r.one.emailAccount({
        from: r.messagingChannel.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      routes: r.many.messagingRoute(),
      automationJobs: r.many.automationJob(),
      actions: r.many.action(),
      executedActions: r.many.executedAction(),
    },
    messagingRoute: {
      messagingChannel: r.one.messagingChannel({
        from: r.messagingRoute.messagingChannelId,
        to: r.messagingChannel.id,
        optional: false,
      }),
    },
    automationJob: {
      messagingChannel: r.one.messagingChannel({
        from: [r.automationJob.messagingChannelId, r.automationJob.emailAccountId],
        to: [r.messagingChannel.id, r.messagingChannel.emailAccountId],
        optional: false,
      }),
      runs: r.many.automationJobRun(),
    },
    automationJobRun: {
      automationJob: r.one.automationJob({
        from: r.automationJobRun.automationJobId,
        to: r.automationJob.id,
        optional: false,
      }),
    },
    driveConnection: {
      emailAccount: r.one.emailAccount({
        from: r.driveConnection.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
      documentFilings: r.many.documentFiling(),
      filingFolders: r.many.filingFolder(),
      attachmentSources: r.many.attachmentSource(),
    },
    filingFolder: {
      driveConnection: r.one.driveConnection({
        from: r.filingFolder.driveConnectionId,
        to: r.driveConnection.id,
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.filingFolder.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    documentFiling: {
      driveConnection: r.one.driveConnection({
        from: r.documentFiling.driveConnectionId,
        to: r.driveConnection.id,
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.documentFiling.emailAccountId,
        to: r.emailAccount.id,
        optional: false,
      }),
    },
    attachmentSource: {
      rule: r.one.rule({ from: r.attachmentSource.ruleId, to: r.rule.id, optional: false }),
      driveConnection: r.one.driveConnection({
        from: r.attachmentSource.driveConnectionId,
        to: r.driveConnection.id,
        optional: false,
      }),
      documents: r.many.attachmentDocument(),
    },
    attachmentDocument: {
      attachmentSource: r.one.attachmentSource({
        from: r.attachmentDocument.attachmentSourceId,
        to: r.attachmentSource.id,
        optional: false,
      }),
    },
    referral: {
      referrerUser: r.one.user({
        from: r.referral.referrerUserId,
        to: r.user.id,
        optional: false,
        alias: 'ReferrerUser',
      }),
      referredUser: r.one.user({
        from: r.referral.referredUserId,
        to: r.user.id,
        optional: false,
        alias: 'ReferredUser',
      }),
    },
    mcpIntegration: {
      emailAccount: r.one.emailAccount({
        from: r.mcpIntegration.emailAccountId,
        to: r.emailAccount.id,
      }),
      connections: r.many.mcpConnection(),
    },
    mcpConnection: {
      integration: r.one.mcpIntegration({
        from: r.mcpConnection.integrationId,
        to: r.mcpIntegration.id,
        optional: false,
      }),
      emailAccount: r.one.emailAccount({
        from: r.mcpConnection.emailAccountId,
        to: r.emailAccount.id,
      }),
      tools: r.many.mcpTool(),
    },
    mcpTool: {
      connection: r.one.mcpConnection({
        from: r.mcpTool.connectionId,
        to: r.mcpConnection.id,
        optional: false,
      }),
    },
    mobilePushToken: {
      user: r.one.user({ from: r.mobilePushToken.userId, to: r.user.id, optional: false }),
      mobilePushDeliveries: r.many.mobilePushDelivery(),
    },
    mobilePushDelivery: {
      mobilePushToken: r.one.mobilePushToken({
        from: r.mobilePushDelivery.mobilePushTokenId,
        to: r.mobilePushToken.id,
        optional: false,
      }),
    },
    oauthClient: {
      user: r.one.user({ from: r.oauthClient.userId, to: r.user.id }),
      oauthClientResources: r.many.oauthClientResource(),
      oauthrefreshtokens: r.many.oauthRefreshToken(),
      oauthaccesstokens: r.many.oauthAccessToken(),
      oauthconsents: r.many.oauthConsent(),
    },
    oauthRefreshToken: {
      client: r.one.oauthClient({
        from: r.oauthRefreshToken.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      session: r.one.session({ from: r.oauthRefreshToken.sessionId, to: r.session.id }),
      user: r.one.user({ from: r.oauthRefreshToken.userId, to: r.user.id, optional: false }),
      oauthaccesstokens: r.many.oauthAccessToken(),
    },
    oauthAccessToken: {
      client: r.one.oauthClient({
        from: r.oauthAccessToken.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      session: r.one.session({ from: r.oauthAccessToken.sessionId, to: r.session.id }),
      user: r.one.user({ from: r.oauthAccessToken.userId, to: r.user.id }),
      refresh: r.one.oauthRefreshToken({
        from: r.oauthAccessToken.refreshId,
        to: r.oauthRefreshToken.id,
      }),
    },
    oauthConsent: {
      client: r.one.oauthClient({
        from: r.oauthConsent.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      user: r.one.user({ from: r.oauthConsent.userId, to: r.user.id }),
    },
    oauthResource: { oauthClientResources: r.many.oauthClientResource() },
    oauthClientResource: {
      client: r.one.oauthClient({
        from: r.oauthClientResource.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      oauthResource: r.one.oauthResource({
        from: r.oauthClientResource.resourceId,
        to: r.oauthResource.identifier,
        optional: false,
      }),
    },
    scimManagedConnection: {
      scimmanagedcredentials: r.many.scimManagedCredential(),
      scimmanagedconnectionevents: r.many.scimManagedConnectionEvent(),
    },
    scimManagedCredential: {
      scimmanagedconnection: r.one.scimManagedConnection({
        from: r.scimManagedCredential.connectionRecordId,
        to: r.scimManagedConnection.id,
        optional: false,
      }),
    },
    scimManagedConnectionEvent: {
      scimmanagedconnection: r.one.scimManagedConnection({
        from: r.scimManagedConnectionEvent.connectionRecordId,
        to: r.scimManagedConnection.id,
        optional: false,
      }),
    },
    scimIdentityTombstone: {
      user: r.one.user({ from: r.scimIdentityTombstone.userId, to: r.user.id, optional: false }),
    },
    scimSubject: {
      user: r.one.user({ from: r.scimSubject.userId, to: r.user.id, optional: false }),
    },
    scimUser: {
      user: r.one.user({ from: r.scimUser.userId, to: r.user.id, optional: false }),
      scimProjectionGrants: r.many.scimProjectionGrant(),
      scimGroupMembers: r.many.scimGroupMember(),
    },
    scimProjectionGrant: {
      scimUser: r.one.scimUser({
        from: r.scimProjectionGrant.scimUserId,
        to: r.scimUser.id,
        optional: false,
      }),
      user: r.one.user({ from: r.scimProjectionGrant.userId, to: r.user.id, optional: false }),
    },
    scimGroup: { scimGroupMembers: r.many.scimGroupMember() },
    scimGroupMember: {
      scimGroup: r.one.scimGroup({
        from: r.scimGroupMember.groupId,
        to: r.scimGroup.id,
        optional: false,
      }),
      scimUser: r.one.scimUser({
        from: r.scimGroupMember.scimUserId,
        to: r.scimUser.id,
        optional: false,
      }),
    },
    scimIdentityLink: {
      user: r.one.user({ from: r.scimIdentityLink.userId, to: r.user.id, optional: false }),
    },
    categoryToRule: {
      category: r.one.category({ from: r.categoryToRule.A, to: r.category.id, optional: false }),
      rule: r.one.rule({ from: r.categoryToRule.B, to: r.rule.id, optional: false }),
    },
  }),
)
