import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type SignupIntent = 'AI_ASSISTANT' | 'MAIL_CLIENT' | 'BOTH'

export type ApiKeyScope =
  | 'STATS_READ'
  | 'RULES_READ'
  | 'RULES_WRITE'
  | 'SETTINGS_READ'
  | 'SETTINGS_WRITE'
  | 'ASSISTANT_CHAT'
  | 'SENDERS_UNSUBSCRIBE'

export type ScheduledEmailStatus =
  | 'PENDING'
  | 'PROCESSING'
  | 'SENT'
  | 'BLOCKED_AUTH'
  | 'UNCERTAIN'
  | 'FAILED'
  | 'CANCELLED'

export type EmailReminderStatus = 'NONE' | 'PENDING' | 'PROCESSING' | 'COMPLETED' | 'CANCELLED'

export type MeetingJoinRule = 'ALL' | 'EXTERNAL_ONLY' | 'HOST_ONLY' | 'OFF'

export type MeetingRecordingStatus =
  | 'PENDING'
  | 'SCHEDULED'
  | 'CANCELLING'
  | 'JOINING'
  | 'IN_WAITING_ROOM'
  | 'IN_CALL'
  | 'RECORDING'
  | 'CALL_ENDED'
  | 'DONE'
  | 'FAILED'
  | 'CANCELLED'

export type MeetingProcessingStatus = 'PENDING' | 'PROCESSING' | 'COMPLETED' | 'FAILED'

export type McpAuthType = 'OAUTH' | 'API_TOKEN' | 'NONE'

export type MobilePushPlatform = 'android' | 'ios'

export type MobilePushTokenType = 'expo' | 'apns' | 'fcm'

export type ActionType =
  | 'ARCHIVE'
  | 'LABEL'
  | 'REPLY'
  | 'SEND_EMAIL'
  | 'FORWARD'
  | 'DRAFT_EMAIL'
  | 'DRAFT_MESSAGING_CHANNEL'
  | 'NOTIFY_MESSAGING_CHANNEL'
  | 'MARK_SPAM'
  | 'CALL_WEBHOOK'
  | 'MARK_READ'
  | 'STAR'
  | 'DELETE'
  | 'DIGEST'
  | 'MOVE_FOLDER'
  | 'NOTIFY_SENDER'
  | 'INTEGRATION'

export type MessagingMessageStatus =
  | 'SENT'
  | 'DRAFT_SENT'
  | 'DRAFT_EDITED'
  | 'DISMISSED'
  | 'EXPIRED'
  | 'FAILED'

export type Frequency = 'NEVER' | 'DAILY' | 'WEEKLY'

export type MailLayout = 'LIST' | 'SPLIT'

export type MailSplitFilterKind =
  | 'UNREAD'
  | 'STARRED'
  | 'LABEL'
  | 'CATEGORY'
  | 'FROM'
  | 'OLDER_THAN'

export type DraftReplyConfidence = 'ALL_EMAILS' | 'STANDARD' | 'HIGH_CONFIDENCE'

export type DraftEmailStatus =
  | 'PENDING'
  | 'LIKELY_SENT'
  | 'REPLIED_WITHOUT_DRAFT'
  | 'CLEANED_UP_UNUSED'
  | 'MISSING_FROM_PROVIDER'
  | 'USER_EDITED'
  | 'CLEANUP_SKIPPED'

export type ReplyMemoryKind = 'FACT' | 'PREFERENCE' | 'PROCEDURE'

export type ReplyMemoryScopeType = 'GLOBAL' | 'SENDER' | 'DOMAIN' | 'TOPIC'

export type NewsletterStatus = 'APPROVED' | 'UNSUBSCRIBED' | 'AUTO_ARCHIVED'

export type ColdEmailStatus = 'AI_LABELED_COLD' | 'USER_REJECTED_COLD'

export type DocumentFilingStatus =
  | 'PROCESSING'
  | 'PENDING'
  | 'FILED'
  | 'REJECTED'
  | 'ERROR'
  | 'PREVIEW'

export type AttachmentSourceType = 'FILE' | 'FOLDER'

export type ColdEmailSetting =
  | 'DISABLED'
  | 'LIST'
  | 'LABEL'
  | 'ARCHIVE_AND_LABEL'
  | 'ARCHIVE_AND_READ_AND_LABEL'

export type PremiumTier =
  | 'BASIC_MONTHLY'
  | 'BASIC_ANNUALLY'
  | 'PRO_MONTHLY'
  | 'PRO_ANNUALLY'
  | 'BUSINESS_MONTHLY'
  | 'BUSINESS_ANNUALLY'
  | 'PLUS_MONTHLY'
  | 'PLUS_ANNUALLY'
  | 'BUSINESS_PLUS_MONTHLY'
  | 'BUSINESS_PLUS_ANNUALLY'
  | 'COPILOT_MONTHLY'
  | 'LIFETIME'

export type ExecutedRuleStatus =
  | 'APPLIED'
  | 'APPLYING'
  | 'REJECTED'
  | 'PENDING'
  | 'SKIPPED'
  | 'ERROR'

export type ExecutedActionStatus = 'SUCCEEDED' | 'FAILED' | 'SKIPPED'

export type GroupItemType = 'FROM' | 'SUBJECT' | 'BODY'

export type CategoryFilterType = 'INCLUDE' | 'EXCLUDE'

export type LogicalOperator = 'AND' | 'OR'

export type ThreadTrackerType = 'AWAITING' | 'NEEDS_REPLY' | 'NEEDS_ACTION'

export type ProcessorType = 'LEMON_SQUEEZY' | 'STRIPE'

export type CleanAction = 'ARCHIVE' | 'MARK_READ'

export type SystemType =
  | 'TO_REPLY'
  | 'FYI'
  | 'AWAITING_REPLY'
  | 'ACTIONED'
  | 'COLD_EMAIL'
  | 'NEWSLETTER'
  | 'MARKETING'
  | 'CALENDAR'
  | 'RECEIPT'
  | 'NOTIFICATION'
  | 'OTP'

export type ReferralStatus = 'PENDING' | 'COMPLETED'

export type DigestStatus = 'PENDING' | 'PROCESSING' | 'SENT' | 'FAILED'

export type ScheduledActionStatus = 'PENDING' | 'EXECUTING' | 'COMPLETED' | 'FAILED' | 'CANCELLED'

export type SnoozedThreadStatus = 'PREPARING' | 'PENDING' | 'EXECUTING' | 'COMPLETED' | 'CANCELLED'

export type EmailSendOperationStatus = 'PROCESSING' | 'SENT' | 'UNCERTAIN'

export type SchedulingStatus = 'PENDING' | 'SCHEDULED' | 'FAILED'

export type MeetingBriefingStatus = 'PENDING' | 'SENT' | 'FAILED' | 'SKIPPED'

export type BookingLinkLocationType =
  | 'IN_PERSON'
  | 'GOOGLE_MEET'
  | 'MICROSOFT_TEAMS'
  | 'PHONE'
  | 'CUSTOM'

export type BookingStatus = 'PENDING_PROVIDER_EVENT' | 'CONFIRMED' | 'CANCELED' | 'FAILED'

export type GroupItemSource = 'AI' | 'USER' | 'LABEL_REMOVED' | 'LABEL_ADDED'

export type ClassificationFeedbackEventType = 'LABEL_ADDED' | 'LABEL_REMOVED'

export type MessagingProvider = 'SLACK' | 'TEAMS' | 'TELEGRAM'

export type MessagingRoutePurpose =
  | 'RULE_NOTIFICATIONS'
  | 'SCHEDULED_CHECK_INS'
  | 'MEETING_BRIEFS'
  | 'DOCUMENT_FILINGS'
  | 'DIGESTS'
  | 'FOLLOW_UPS'

export type MessagingRouteTargetType = 'CHANNEL' | 'DIRECT_MESSAGE'

export type AutomationJobRunStatus = 'PENDING' | 'RUNNING' | 'SENT' | 'SKIPPED' | 'FAILED'

export type ConversationStatus = 'ACTIVE' | 'STOPPED'

export type ConversationActivityKind = 'INVITED' | 'COMMENT' | 'MENTION'

export interface Account {
  password: string | null
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: string
  provider: string
  type: Generated<string>
  providerAccountId: string
  refresh_token: string | null
  refreshTokenExpiresAt: Timestamp | null
  access_token: string | null
  expires_at: Generated<Timestamp | null>
  disconnectedAt: Timestamp | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  emailOtp: Generated<boolean>
  emailOtpVersion: Generated<number>
  ipAddress: string | null
  userAgent: string | null
  activeOrganizationId: string | null
}

export interface User {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string | null
  email: string
  emailVerified: Generated<boolean | null>
  image: string | null
  completedOnboardingAt: Timestamp | null
  signupIntent: SignupIntent | null
  onboardingAnswers: unknown
  lastLogin: Timestamp | null
  utms: unknown
  errorMessages: unknown
  announcementDismissedAt: Timestamp | null
  dismissedHints: Generated<string[]>
  surveyFeatures: string[]
  surveyRole: string | null
  surveyGoal: string | null
  surveyCompanySize: number | null
  surveySource: string | null
  surveyImprovements: string | null
  mcpServerEnabled: Generated<boolean>
  mcpTokenVersion: Generated<number>
  emailOtpEnabled: Generated<boolean>
  emailOtpVersion: Generated<number>
  aiProvider: string | null
  aiModel: string | null
  aiApiKey: string | null
  decisionModelEnabled: boolean | null
  webhookSecret: string | null
  referralCode: string | null
  premiumId: string | null
  premiumAdminId: string | null
  scimAccessDisabled: Generated<boolean>
}

export interface EmailAccount {
  id: string
  email: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string | null
  image: string | null
  sortOrder: number | null
  about: string | null
  writingStyle: string | null
  signature: string | null
  sentSignatureScan: unknown
  sentSignatureScannedAt: Timestamp | null
  includeReferralSignature: Generated<boolean>
  includeSentWithSignature: Generated<boolean>
  sentMessageOpenTrackingEnabled: Generated<boolean>
  watchEmailsExpirationDate: Timestamp | null
  watchEmailsSubscriptionId: string | null
  watchEmailsSubscriptionHistory: unknown
  lastSyncedHistoryId: string | null
  behaviorProfile: unknown
  personaAnalysis: unknown
  role: string | null
  timezone: string | null
  calendarBookingLink: string | null
  draftDocsUrls: Generated<string[]>
  statsEmailFrequency: Generated<Frequency>
  summaryEmailFrequency: Generated<Frequency>
  lastSummaryEmailAt: Timestamp | null
  lastInboxHealthEmailAt: Timestamp | null
  coldEmailBlocker: ColdEmailSetting | null
  coldEmailDigest: Generated<boolean>
  coldEmailPrompt: string | null
  rulesPrompt: string | null
  autoCategorizeSenders: Generated<boolean>
  multiRuleSelectionEnabled: Generated<boolean>
  sensitiveDataPolicy: string | null
  draftReplyConfidence: Generated<DraftReplyConfidence>
  draftCleanupDays: Generated<number | null>
  allowHiddenAiDraftLinks: Generated<boolean>
  rulesRevision: Generated<number>
  mailLayout: Generated<MailLayout>
  mailExpandedPreview: Generated<boolean>
  mailBundleInbox: boolean | null
  includeInAllAccounts: Generated<boolean>
  meetingBriefingsEnabled: Generated<boolean>
  meetingBriefingsMinutesBefore: Generated<number>
  meetingBriefsSendEmail: Generated<boolean>
  meetingRecorderEnabled: Generated<boolean>
  meetingRecorderJoinRule: Generated<MeetingJoinRule>
  meetingRecorderRecapEmailEnabled: Generated<boolean>
  meetingRecorderFollowUpDraftEnabled: Generated<boolean>
  filingEnabled: Generated<boolean>
  filingPrompt: string | null
  filingConfirmationSendEmail: Generated<boolean>
  followUpAwaitingReplyDays: number | null
  followUpNeedsReplyDays: number | null
  followUpAutoDraftEnabled: Generated<boolean>
  digestSendEmail: Generated<boolean>
  learnedWritingStyle: string | null
  userId: string
  accountId: string
}

export interface MailSplit {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  order: number
  matchAll: Generated<boolean>
  bundleInInbox: Generated<boolean>
  emailAccountId: string
}

export interface MailSplitFilter {
  id: string
  kind: MailSplitFilterKind
  value: string | null
  order: number
  mailSplitId: string
}

export interface Organization {
  id: string
  name: string
  slug: string
  logo: string | null
  metadata: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationRule {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organizationId: string
  enabled: Generated<boolean>
  name: string
  instructions: string | null
  runOnThreads: Generated<boolean>
  conditionalOperator: Generated<LogicalOperator>
  from: string | null
  to: string | null
  subject: string | null
  body: string | null
}

export interface OrganizationRuleAction {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organizationRuleId: string
  type: ActionType
  label: string | null
  subject: string | null
  content: string | null
  to: string | null
  cc: string | null
  bcc: string | null
  url: string | null
  folderName: string | null
  delayInMinutes: number | null
  staticAttachments: unknown
}

export interface Member {
  id: string
  organizationId: string
  emailAccountId: string
  role: Generated<string>
  allowOrgAdminAnalytics: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Conversation {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organizationId: string
  publisherId: string
  threadId: string
  status: Generated<ConversationStatus>
  revision: Generated<number>
  generation: Generated<number>
}

export interface ConversationParticipant {
  id: string
  conversationId: string
  memberId: string
  generation: number
  active: Generated<boolean>
  readRevision: Generated<number>
  muted: Generated<boolean>
  createdAt: Generated<Timestamp>
}

export interface ConversationComment {
  id: string
  conversationId: string
  authorMemberId: string | null
  clientMutationId: string
  body: string
  revision: number
  deletedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface ConversationActivity {
  id: string
  participantId: string
  commentId: string | null
  kind: ConversationActivityKind
  revision: number
  createdAt: Generated<Timestamp>
}

export interface Invitation {
  id: string
  organizationId: string
  email: string
  role: string | null
  status: string
  expiresAt: Timestamp
  inviterId: string
}

export interface Verification {
  id: string
  identifier: string
  value: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface SsoProvider {
  userId: string | null
  id: string
  issuer: string
  oidcConfig: string | null
  samlConfig: string | null
  emailAccountId: string | null
  providerId: string
  organizationId: string | null
  domain: string
}

export interface Digest {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  emailAccountId: string
  sentAt: Timestamp | null
  status: Generated<DigestStatus>
}

export interface DigestItem {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  messageId: string
  threadId: string
  content: string
  digestId: string
  actionId: string | null
  coldEmailId: string | null
}

export interface Schedule {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  intervalDays: number | null
  occurrences: number | null
  daysOfWeek: number | null
  timeOfDay: Timestamp | null
  emailAccountId: string
  lastOccurrenceAt: Timestamp | null
  nextOccurrenceAt: Timestamp | null
}

export interface Premium {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  pendingInvites: string[]
  lemonSqueezyRenewsAt: Timestamp | null
  lemonSqueezyCustomerId: number | null
  lemonSqueezySubscriptionId: number | null
  lemonSqueezySubscriptionItemId: number | null
  lemonSqueezyOrderId: number | null
  lemonSqueezyProductId: number | null
  lemonSqueezyVariantId: number | null
  lemonLicenseKey: string | null
  lemonLicenseInstanceId: string | null
  lemonSubscriptionStatus: string | null
  stripeCustomerId: string | null
  stripeSubscriptionId: string | null
  stripeSubscriptionItemId: string | null
  stripePriceId: string | null
  stripeProductId: string | null
  stripeSubscriptionStatus: string | null
  stripeCancelAt: Timestamp | null
  stripeCancelAtPeriodEnd: boolean | null
  stripeRenewsAt: Timestamp | null
  stripeTrialEnd: Timestamp | null
  stripeTrialConversionInvoiceId: string | null
  stripeTrialConvertedAt: Timestamp | null
  stripeCancellationInitiatedAt: Timestamp | null
  stripeCanceledAt: Timestamp | null
  stripeEndedAt: Timestamp | null
  stripeCancellationFeedback: string | null
  stripeCancellationComment: string | null
  stripeAiOverageLastInvoiceId: string | null
  stripeAiOverageLastPeriodEnd: Timestamp | null
  stripeAiOverageLastUnits: number | null
  stripeInvoiceEmailsEnabled: Generated<boolean>
  appleOriginalTransactionId: string | null
  appleLatestTransactionId: string | null
  appleProductId: string | null
  appleSubscriptionGroupIdentifier: string | null
  appleAppAccountToken: string | null
  appleSubscriptionStatus: string | null
  appleEnvironment: string | null
  applePurchaseDate: Timestamp | null
  appleExpiresAt: Timestamp | null
  appleRevokedAt: Timestamp | null
  appleOfferDiscountType: string | null
  adminGrantExpiresAt: Timestamp | null
  adminGrantTier: PremiumTier | null
  tier: PremiumTier | null
  emailAccountsAccess: number | null
  unsubscribeMonth: number | null
  unsubscribeCredits: number | null
  aiMonth: number | null
  aiCredits: number | null
}

export interface PendingPremiumGrant {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  email: string
  tier: PremiumTier
  count: Generated<number>
  emailAccountsAccess: number | null
}

export interface VerificationToken {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  identifier: string
  token: string
  expires: Timestamp
}

export interface Label {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  gmailLabelId: string
  name: string
  description: string | null
  enabled: Generated<boolean>
  emailAccountId: string
}

export interface Rule {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  enabled: Generated<boolean>
  automate: Generated<boolean>
  runOnThreads: Generated<boolean>
  emailAccountId: string
  organizationRuleId: string | null
  organizationRuleMemberEnabled: boolean | null
  conditionalOperator: Generated<LogicalOperator>
  instructions: string | null
  groupId: string | null
  from: string | null
  to: string | null
  subject: string | null
  body: string | null
  categoryFilterType: CategoryFilterType | null
  systemType: SystemType | null
  promptText: string | null
}

export interface Action {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: ActionType
  ruleId: string
  emailAccountId: string
  messagingChannelId: string | null
  messagingChannelEmailAccountId: string | null
  label: string | null
  labelId: string | null
  subject: string | null
  content: string | null
  to: string | null
  cc: string | null
  bcc: string | null
  url: string | null
  folderName: string | null
  folderId: string | null
  delayInMinutes: number | null
  staticAttachments: unknown
  integrationName: string | null
  integrationToolName: string | null
  integrationArgs: unknown
}

export interface RuleHistory {
  id: string
  createdAt: Generated<Timestamp>
  ruleId: string
  version: number
  triggerType: string
  promptText: string | null
  name: string
  instructions: string | null
  enabled: boolean
  automate: boolean
  runOnThreads: boolean
  conditionalOperator: string
  from: string | null
  to: string | null
  subject: string | null
  body: string | null
  categoryFilterType: string | null
  systemType: string | null
  actions: unknown
  categoryFilters: unknown
}

export interface ExecutedRule {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  threadId: string
  messageId: string
  status: ExecutedRuleStatus
  automated: boolean
  reason: string | null
  matchMetadata: unknown
  ruleId: string | null
  emailAccountId: string
}

export interface ExecutedAction {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: ActionType
  executionStatus: ExecutedActionStatus | null
  executionStartedAt: Timestamp | null
  executedAt: Timestamp | null
  executionError: unknown
  sentMessageIds: Generated<string[]>
  executedRuleId: string
  messagingChannelId: string | null
  messagingMessageId: string | null
  messagingMessageSentAt: Timestamp | null
  messagingMessageStatus: MessagingMessageStatus | null
  label: string | null
  labelId: string | null
  subject: string | null
  content: string | null
  to: string | null
  cc: string | null
  bcc: string | null
  url: string | null
  folderName: string | null
  folderId: string | null
  staticAttachments: unknown
  selectedAttachments: unknown
  integrationName: string | null
  integrationToolName: string | null
  integrationArgs: unknown
  draftId: string | null
  draftStatus: DraftEmailStatus | null
  wasDraftSent: boolean | null
  draftModelProvider: string | null
  draftModelName: string | null
  draftPipelineVersion: number | null
  draftContextMetadata: unknown
}

export interface ScheduledAction {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  executedRuleId: string
  actionType: ActionType
  messageId: string
  threadId: string
  scheduledFor: Timestamp
  emailAccountId: string
  status: Generated<ScheduledActionStatus>
  schedulingStatus: Generated<SchedulingStatus>
  label: string | null
  labelId: string | null
  subject: string | null
  content: string | null
  to: string | null
  cc: string | null
  bcc: string | null
  url: string | null
  folderName: string | null
  folderId: string | null
  scheduledId: string | null
  staticAttachments: unknown
  selectedAttachments: unknown
  executedAt: Timestamp | null
  executedActionId: string | null
}

export interface SnoozedThread {
  id: string
  updatedAt: Timestamp
  threadId: string
  scheduledFor: Timestamp
  status: Generated<SnoozedThreadStatus>
  clientMutationId: string | null
  emailAccountId: string
}

export interface ScheduledEmail {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  emailAccountId: string
  clientMutationId: string
  payloadHash: string
  payload: unknown
  threadId: string | null
  sendAt: Timestamp
  status: Generated<ScheduledEmailStatus>
  processingStartedAt: Timestamp | null
  executionQueuedAt: Timestamp | null
  sentAt: Timestamp | null
  error: string | null
  remindAt: Timestamp | null
  reminderStatus: Generated<EmailReminderStatus>
  reminderStartedAt: Timestamp | null
  heldForUndo: Generated<boolean>
  draftMessageIds: Generated<string[]>
}

export interface EmailSendOperation {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  clientMutationId: string
  payloadHash: string
  status: Generated<EmailSendOperationStatus>
  processingStartedAt: Generated<Timestamp>
  result: unknown
  emailAccountId: string
}

export interface SentMessageOpen {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  token: string
  emailAccountId: string
  threadId: string | null
  messageId: string | null
  firstOpenedAt: Timestamp | null
  lastOpenedAt: Timestamp | null
  openCount: Generated<number>
}

export interface Group {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  prompt: string | null
  emailAccountId: string
}

export interface GroupItem {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  groupId: string | null
  type: GroupItemType
  value: string
  exclude: Generated<boolean>
  reason: string | null
  threadId: string | null
  messageId: string | null
  source: GroupItemSource | null
}

export interface ClassificationFeedback {
  id: string
  createdAt: Generated<Timestamp>
  sender: string
  eventType: ClassificationFeedbackEventType
  ruleId: string
  emailAccountId: string
  threadId: string
  messageId: string
}

export interface Category {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  description: string | null
  emailAccountId: string
}

export interface Newsletter {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  email: string
  name: string | null
  status: NewsletterStatus | null
  patternAnalyzed: Generated<boolean>
  lastAnalyzedAt: Timestamp | null
  emailAccountId: string
  categoryId: string | null
}

export interface ColdEmail {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  fromEmail: string
  messageId: string | null
  threadId: string | null
  status: ColdEmailStatus | null
  reason: string | null
  emailAccountId: string
}

export interface EmailMessage {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  threadId: string
  messageId: string
  date: Timestamp
  from: string
  fromName: string | null
  fromDomain: string
  to: string
  unsubscribeLink: string | null
  read: boolean
  sent: boolean
  draft: boolean
  inbox: boolean
  emailAccountId: string
}

export interface ResponseTime {
  id: string
  createdAt: Generated<Timestamp>
  threadId: string
  sentMessageId: string
  receivedMessageId: string
  responseTimeMins: number
  receivedAt: Timestamp
  sentAt: Timestamp
  emailAccountId: string
}

export interface ThreadTracker {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  sentAt: Timestamp
  threadId: string
  messageId: string
  resolved: Generated<boolean>
  type: ThreadTrackerType
  followUpAppliedAt: Timestamp | null
  followUpDraftId: string | null
  followUpDraftContent: string | null
  followUpDraftCreatedAt: Timestamp | null
  followUpNotifications: unknown
  emailAccountId: string
}

export interface CleanupJob {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  action: Generated<CleanAction>
  daysOld: Generated<number>
  instructions: string | null
  skipReply: boolean | null
  skipStarred: boolean | null
  skipCalendar: boolean | null
  skipReceipt: boolean | null
  skipAttachment: boolean | null
  skipConversation: boolean | null
  emailAccountId: string
}

export interface CleanupThread {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  threadId: string
  archived: boolean
  jobId: string
  emailAccountId: string
}

export interface Knowledge {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  title: string
  content: string
  emailAccountId: string
}

export interface Snippet {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  shortcut: string
  content: string
  emailAccountId: string
}

export interface EmailSignature {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  html: string
  emailAccountId: string
}

export interface ReplyMemory {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  content: string
  kind: ReplyMemoryKind
  scopeType: ReplyMemoryScopeType
  scopeValue: string
  isLearnedStyleEvidence: Generated<boolean>
  emailAccountId: string
}

export interface ReplyMemorySource {
  createdAt: Generated<Timestamp>
  learnedWritingStyleAnalyzedAt: Timestamp | null
  replyMemoryId: string
  draftSendLogId: string
}

export interface ApiKey {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string | null
  hashedKey: string
  isActive: Generated<boolean>
  expiresAt: Timestamp | null
  lastUsedAt: Timestamp | null
  scopes: Generated<ApiKeyScope[]>
  userId: string
  emailAccountId: string | null
}

export interface EmailToken {
  id: string
  createdAt: Generated<Timestamp>
  token: string
  expiresAt: Timestamp
  emailAccountId: string
}

export interface Payment {
  id: string
  createdAt: Timestamp
  updatedAt: Timestamp
  premiumId: string | null
  processorType: Generated<ProcessorType>
  processorId: string | null
  processorSubscriptionId: string | null
  processorCustomerId: string | null
  amount: number
  currency: string
  status: string
  tax: number
  taxInclusive: boolean
  refunded: Generated<boolean>
  refundedAt: Timestamp | null
  refundedAmount: number | null
  billingReason: string | null
  invoiceEmailSentAt: Timestamp | null
}

export interface DraftSendLog {
  id: string
  createdAt: Generated<Timestamp>
  executedActionId: string
  sentMessageId: string
  similarityScore: number
  bodySimilarityScore: number | null
  bodySimilarityStatus: string | null
  similarityMetadata: unknown
  sentText: string | null
  replyMemorySentText: string | null
  replyMemoryAttemptCount: Generated<number>
  replyMemoryProcessedAt: Timestamp | null
}

export interface Chat {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  name: string | null
  compactionCount: Generated<number>
  lastSeenRulesRevision: number | null
  activeStreamId: string | null
  activeStreamStartedAt: Timestamp | null
  emailAccountId: string
}

export interface ChatMessage {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  role: string
  parts: unknown
  metadata: unknown
  chatId: string
}

export interface ChatCompaction {
  id: string
  createdAt: Generated<Timestamp>
  summary: string
  messageCount: number
  compactedBeforeCreatedAt: Timestamp
  chatId: string
}

export interface ChatMemory {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  content: string
  chatId: string | null
  emailAccountId: string
}

export interface CalendarConnection {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  provider: string
  email: string
  accessToken: string | null
  refreshToken: string | null
  expiresAt: Timestamp | null
  isConnected: Generated<boolean>
  emailAccountId: string
}

export interface Calendar {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  calendarId: string
  name: string
  description: string | null
  primary: Generated<boolean>
  isEnabled: Generated<boolean>
  timezone: string | null
  connectionId: string
}

export interface BookingLink {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  slug: string
  title: string
  description: string | null
  isActive: Generated<boolean>
  durationMinutes: number
  slotIntervalMinutes: number
  locationType: Generated<BookingLinkLocationType>
  locationValue: string | null
  minimumNoticeMinutes: Generated<number>
  maxDaysAhead: Generated<number>
  emailAccountId: string
  destinationCalendarId: string | null
  availabilityScheduleId: string
}

export interface AvailabilitySchedule {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  isDefault: Generated<boolean>
  timezone: Generated<string>
  emailAccountId: string
}

export interface AvailabilityWindow {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  weekday: number
  startMinutes: number
  endMinutes: number
  availabilityScheduleId: string
}

export interface Booking {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  guestName: string
  guestEmail: string
  guestNote: string | null
  startTime: Timestamp
  endTime: Timestamp
  status: Generated<BookingStatus>
  provider: string | null
  providerConnectionId: string | null
  providerCalendarId: string | null
  providerEventId: string | null
  videoConferenceLink: string | null
  cancelTokenHash: string
  cancellationReason: string | null
  idempotencyToken: string | null
  bookingLinkId: string
  emailAccountId: string
}

export interface MeetingBriefing {
  id: string
  createdAt: Generated<Timestamp>
  calendarEventId: string
  eventTitle: string
  eventStartTime: Timestamp
  guestCount: number
  status: MeetingBriefingStatus
  emailAccountId: string
}

export interface ContactResearch {
  id: string
  createdAt: Generated<Timestamp>
  email: string
  researchStartedAt: Timestamp
  found: boolean
  role: string | null
  confidence: string | null
  company: unknown
  sources: Generated<string[]>
}

export interface MeetingRecording {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  botProvider: Generated<string>
  externalBotId: string | null
  botName: string | null
  externalRecordingId: string | null
  transcriptRequestedAt: Timestamp | null
  externalTranscriptId: string | null
  meetingUrl: string
  normalizedMeetingUrl: string
  activeKey: string | null
  meetingStartTime: Timestamp
  status: Generated<MeetingRecordingStatus>
  failureReason: string | null
  transcript: unknown
  transcriptFetchedAt: Timestamp | null
  mediaDeletedAt: Timestamp | null
  emailAccountId: string
}

export interface Meeting {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  calendarEventId: string
  eventTitle: string
  startTime: Timestamp
  endTime: Timestamp
  attendees: unknown
  organizerEmail: string | null
  joinOverride: boolean | null
  processingStatus: Generated<MeetingProcessingStatus>
  processingAttempts: Generated<number>
  summary: unknown
  followUpDraftStartedAt: Timestamp | null
  followUpDraftId: string | null
  recapSentAt: Timestamp | null
  recordingId: string | null
  emailAccountId: string
}

export interface MessagingChannel {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  provider: MessagingProvider
  isConnected: Generated<boolean>
  teamId: string
  teamName: string | null
  providerUserId: string | null
  botUserId: string | null
  accessToken: string | null
  refreshToken: string | null
  expiresAt: Timestamp | null
  emailAccountId: string
}

export interface MessagingRoute {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  purpose: MessagingRoutePurpose
  targetType: MessagingRouteTargetType
  targetId: string
  messagingChannelId: string
}

export interface AutomationJob {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  enabled: Generated<boolean>
  prompt: string | null
  cronExpression: string
  nextRunAt: Timestamp
  messagingChannelId: string
  emailAccountId: string
}

export interface AutomationJobRun {
  id: string
  createdAt: Generated<Timestamp>
  status: Generated<AutomationJobRunStatus>
  scheduledFor: Timestamp
  processedAt: Timestamp | null
  outboundMessage: string | null
  providerMessageId: string | null
  error: string | null
  automationJobId: string
}

export interface DriveConnection {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  provider: string
  email: string
  accessToken: string | null
  refreshToken: string | null
  expiresAt: Timestamp | null
  isConnected: Generated<boolean>
  emailAccountId: string
}

export interface FilingFolder {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  folderId: string
  folderName: string
  folderPath: string
  driveConnectionId: string
  emailAccountId: string
}

export interface DocumentFiling {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  messageId: string
  attachmentId: string
  filename: string
  folderId: string | null
  folderPath: string
  fileId: string | null
  reasoning: string | null
  confidence: number | null
  status: Generated<DocumentFilingStatus>
  wasAsked: Generated<boolean>
  wasCorrected: Generated<boolean>
  originalPath: string | null
  correctedAt: Timestamp | null
  feedbackPositive: boolean | null
  feedbackAt: Timestamp | null
  notificationMessageId: string | null
  notificationSentAt: Timestamp | null
  notificationBatchId: string | null
  driveConnectionId: string
  emailAccountId: string
}

export interface AttachmentSource {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  type: AttachmentSourceType
  sourceId: string
  sourcePath: string | null
  ruleId: string
  driveConnectionId: string
}

export interface AttachmentDocument {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  attachmentSourceId: string
  fileId: string
  name: string
  mimeType: string
  modifiedAt: Timestamp | null
  summary: string | null
  content: string | null
  metadata: unknown
  indexedAt: Timestamp | null
  error: string | null
}

export interface Referral {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  referrerUserId: string
  referredUserId: string
  referralCodeUsed: string
  status: Generated<ReferralStatus>
  rewardGrantedAt: Timestamp | null
  stripeBalanceTransactionId: string | null
  rewardAmount: number | null
}

export interface McpIntegration {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  registeredServerUrl: string | null
  registeredAuthorizationUrl: string | null
  registeredTokenUrl: string | null
  oauthClientId: string | null
  oauthClientSecret: string | null
  displayName: string | null
  serverUrl: string | null
  authType: McpAuthType | null
  emailAccountId: string | null
}

export interface McpConnection {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  isActive: Generated<boolean>
  accessToken: string | null
  refreshToken: string | null
  apiKey: string | null
  expiresAt: Timestamp | null
  integrationId: string
  emailAccountId: string | null
}

export interface McpTool {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  description: string | null
  schema: unknown
  isEnabled: Generated<boolean>
  isWrite: Generated<boolean>
  connectionId: string
}

export interface MobilePushToken {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  token: string
  platform: MobilePushPlatform
  tokenType: Generated<MobilePushTokenType>
  appVersion: string | null
  userId: string
}

export interface MobilePushDelivery {
  id: string
  createdAt: Generated<Timestamp>
  deduplicationKey: string
  mobilePushTokenId: string
}

export interface OauthClient {
  id: string
  clientId: string
  clientSecret: string | null
  clientDiscoveryId: string | null
  disabled: Generated<boolean | null>
  skipConsent: boolean | null
  enableEndSession: boolean | null
  subjectType: string | null
  scopes: string[]
  clientCredentialsScopes: Generated<string[]>
  userId: string | null
  createdAt: Timestamp | null
  updatedAt: Timestamp | null
  name: string | null
  uri: string | null
  icon: string | null
  contacts: string[]
  tos: string | null
  policy: string | null
  softwareId: string | null
  softwareVersion: string | null
  softwareStatement: string | null
  redirectUris: string[]
  postLogoutRedirectUris: string[]
  backchannelLogoutUri: string | null
  backchannelLogoutSessionRequired: boolean | null
  tokenEndpointAuthMethod: string | null
  applicationType: string | null
  jwks: string | null
  jwksUri: string | null
  grantTypes: string[]
  responseTypes: string[]
  requirePKCE: boolean | null
  dpopBoundAccessTokens: Generated<boolean | null>
  referenceId: string | null
  metadata: unknown
}

export interface OauthRefreshToken {
  id: string
  token: string
  clientId: string
  sessionId: string | null
  userId: string
  referenceId: string | null
  authorizationCodeId: string | null
  resources: string[]
  requestedUserInfoClaims: string[]
  expiresAt: Timestamp
  createdAt: Timestamp
  revoked: Timestamp | null
  rotatedAt: Timestamp | null
  rotationReplayResponse: string | null
  rotationReplayExpiresAt: Timestamp | null
  authTime: Timestamp | null
  confirmation: unknown
  scopes: string[]
}

export interface OauthAccessToken {
  id: string
  token: string
  clientId: string
  sessionId: string | null
  userId: string | null
  referenceId: string | null
  authorizationCodeId: string | null
  resources: string[]
  requestedUserInfoClaims: string[]
  refreshId: string | null
  expiresAt: Timestamp
  createdAt: Timestamp
  revoked: Timestamp | null
  confirmation: unknown
  scopes: string[]
}

export interface OauthConsent {
  id: string
  clientId: string
  userId: string | null
  referenceId: string | null
  resources: string[]
  requestedUserInfoClaims: string[]
  scopes: string[]
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface Jwks {
  id: string
  publicKey: string
  privateKey: string
  createdAt: Timestamp
  expiresAt: Timestamp | null
  alg: string | null
  crv: string | null
}

export interface OauthResource {
  id: string
  identifier: string
  name: string
  accessTokenTtl: number | null
  refreshTokenTtl: number | null
  signingAlgorithm: string | null
  signingKeyId: string | null
  allowedScopes: string[]
  customClaims: unknown
  dpopBoundAccessTokensRequired: Generated<boolean | null>
  disabled: Generated<boolean | null>
  createdAt: Timestamp | null
  updatedAt: Timestamp | null
  policyVersion: Generated<number | null>
  metadata: unknown
}

export interface OauthClientResource {
  id: string
  clientId: string
  resourceId: string
  metadata: unknown
  createdAt: Timestamp | null
}

export interface OauthClientAssertion {
  id: string
  expiresAt: Timestamp
}

export interface ScimManagedConnection {
  id: string
  creationRequestId: string
  connectionId: string
  provisioningDomainId: string
  status: string
  revision: number
  createdAt: Timestamp
  createdBy: string
  decommissionStartedAt: Timestamp | null
  decommissionStartedBy: string | null
  decommissionedAt: Timestamp | null
  decommissionedBy: string | null
}

export interface ScimManagedCredential {
  id: string
  connectionRecordId: string
  credentialId: string
  tokenDigest: string
  hashVersion: string
  activeSlotKey: string
  status: string
  serializedScopes: string
  expiresAt: Timestamp
  createdAt: Timestamp
  createdBy: string
  lastUsedAt: Timestamp | null
  revokedAt: Timestamp | null
  revokedBy: string | null
  decommissionedAt: Timestamp | null
}

export interface ScimManagedConnectionEvent {
  id: string
  connectionRecordId: string
  eventKey: string
  sequence: number
  type: string
  actorId: string
  credentialId: string | null
  createdAt: Timestamp
}

export interface ScimConnectionBinding {
  id: string
  connectionId: string
  connectionKey: string
  provisioningDomainId: string
  createdAt: Timestamp
  decommissionedAt: Timestamp | null
  decommissionStatus: Generated<string>
  decommissionCursorUserId: string | null
  decommissionReconciledUserCount: Generated<number>
  decommissionBatchCount: Generated<number>
  decommissionRevision: Generated<number>
  decommissionCompletedAt: Timestamp | null
  decommissionLeaseId: string | null
  decommissionLeaseExpiresAt: Timestamp | null
}

export interface ScimIdentityTombstone {
  id: string
  connectionId: string
  provisioningDomainId: string
  externalId: string
  externalIdKey: string
  userId: string
  profile: string
  deletedAt: Timestamp
}

export interface ScimSubject {
  id: string
  userId: string
  profileSourceId: string | null
  revision: number
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ScimUser {
  id: string
  connectionId: string
  provisioningDomainId: string
  userId: string
  connectionUserKey: string
  userName: string
  userNameKey: string
  primaryEmail: string
  workEmailValueIndex: string
  emailValueIndex: string
  displayName: string
  formattedName: string
  givenName: string | null
  familyName: string | null
  serializedEmails: string
  serializedAttributes: string | null
  externalId: string | null
  externalIdKey: string | null
  active: boolean
  orderKey: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ScimProjectionGrant {
  id: string
  connectionId: string
  provisioningDomainId: string
  scimUserId: string
  userId: string
  sourceKind: string
  sourceId: string
  sourceValue: string | null
  role: string
  grantKey: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ScimGroup {
  id: string
  connectionId: string
  provisioningDomainId: string
  revision: Generated<number>
  displayName: string
  displayNameKey: string
  externalId: string | null
  externalIdKey: string | null
  orderKey: string
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface ScimGroupMember {
  id: string
  connectionId: string
  groupId: string
  scimUserId: string
  membershipKey: string
  createdAt: Timestamp
}

export interface ScimIdentityLink {
  id: string
  connectionId: string
  externalId: string
  userId: string
}

export interface LoopsEmailSource {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  emailMessageId: string | null
  loopId: string | null
  loopName: string | null
  campaignId: string | null
  campaignName: string | null
  sourceType: string | null
  eventTime: Timestamp | null
}

export interface CategoryToRule {
  A: string
  B: string
}

export interface DB {
  Account: Account
  Session: Session
  User: User
  EmailAccount: EmailAccount
  MailSplit: MailSplit
  MailSplitFilter: MailSplitFilter
  Organization: Organization
  OrganizationRule: OrganizationRule
  OrganizationRuleAction: OrganizationRuleAction
  Member: Member
  Conversation: Conversation
  ConversationParticipant: ConversationParticipant
  ConversationComment: ConversationComment
  ConversationActivity: ConversationActivity
  invitation: Invitation
  verification: Verification
  ssoProvider: SsoProvider
  Digest: Digest
  DigestItem: DigestItem
  Schedule: Schedule
  Premium: Premium
  PendingPremiumGrant: PendingPremiumGrant
  VerificationToken: VerificationToken
  Label: Label
  Rule: Rule
  Action: Action
  RuleHistory: RuleHistory
  ExecutedRule: ExecutedRule
  ExecutedAction: ExecutedAction
  ScheduledAction: ScheduledAction
  SnoozedThread: SnoozedThread
  ScheduledEmail: ScheduledEmail
  EmailSendOperation: EmailSendOperation
  SentMessageOpen: SentMessageOpen
  Group: Group
  GroupItem: GroupItem
  ClassificationFeedback: ClassificationFeedback
  Category: Category
  Newsletter: Newsletter
  ColdEmail: ColdEmail
  EmailMessage: EmailMessage
  ResponseTime: ResponseTime
  ThreadTracker: ThreadTracker
  CleanupJob: CleanupJob
  CleanupThread: CleanupThread
  Knowledge: Knowledge
  Snippet: Snippet
  EmailSignature: EmailSignature
  ReplyMemory: ReplyMemory
  ReplyMemorySource: ReplyMemorySource
  ApiKey: ApiKey
  EmailToken: EmailToken
  Payment: Payment
  DraftSendLog: DraftSendLog
  Chat: Chat
  ChatMessage: ChatMessage
  ChatCompaction: ChatCompaction
  ChatMemory: ChatMemory
  CalendarConnection: CalendarConnection
  Calendar: Calendar
  BookingLink: BookingLink
  AvailabilitySchedule: AvailabilitySchedule
  AvailabilityWindow: AvailabilityWindow
  Booking: Booking
  MeetingBriefing: MeetingBriefing
  ContactResearch: ContactResearch
  MeetingRecording: MeetingRecording
  Meeting: Meeting
  MessagingChannel: MessagingChannel
  MessagingRoute: MessagingRoute
  AutomationJob: AutomationJob
  AutomationJobRun: AutomationJobRun
  DriveConnection: DriveConnection
  FilingFolder: FilingFolder
  DocumentFiling: DocumentFiling
  AttachmentSource: AttachmentSource
  AttachmentDocument: AttachmentDocument
  Referral: Referral
  McpIntegration: McpIntegration
  McpConnection: McpConnection
  McpTool: McpTool
  MobilePushToken: MobilePushToken
  MobilePushDelivery: MobilePushDelivery
  OauthClient: OauthClient
  OauthRefreshToken: OauthRefreshToken
  OauthAccessToken: OauthAccessToken
  OauthConsent: OauthConsent
  Jwks: Jwks
  OauthResource: OauthResource
  OauthClientResource: OauthClientResource
  OauthClientAssertion: OauthClientAssertion
  scimManagedConnection: ScimManagedConnection
  scimManagedCredential: ScimManagedCredential
  scimManagedConnectionEvent: ScimManagedConnectionEvent
  scimConnectionBinding: ScimConnectionBinding
  scimIdentityTombstone: ScimIdentityTombstone
  scimSubject: ScimSubject
  scimUser: ScimUser
  scimProjectionGrant: ScimProjectionGrant
  scimGroup: ScimGroup
  scimGroupMember: ScimGroupMember
  ScimIdentityLink: ScimIdentityLink
  LoopsEmailSource: LoopsEmailSource
  _CategoryToRule: CategoryToRule
}
