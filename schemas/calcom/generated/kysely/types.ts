import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type SchedulingType = 'roundRobin' | 'collective' | 'managed'

export type PeriodType = 'unlimited' | 'rolling' | 'rolling_window' | 'range'

export type CreationSource = 'api_v1' | 'api_v2' | 'webapp'

export type CancellationReasonRequirement =
  | 'MANDATORY_BOTH'
  | 'MANDATORY_HOST_ONLY'
  | 'MANDATORY_ATTENDEE_ONLY'
  | 'OPTIONAL_BOTH'

export type IdentityProvider = 'CAL' | 'GOOGLE' | 'SAML' | 'AZUREAD'

export type UserPermissionRole = 'USER' | 'ADMIN'

export type CreditUsageType = 'SMS' | 'CAL_AI_PHONE_CALL'

export type CreditType = 'MONTHLY' | 'ADDITIONAL'

export type MembershipRole = 'MEMBER' | 'ADMIN' | 'OWNER'

export type BookingStatus = 'cancelled' | 'accepted' | 'rejected' | 'pending' | 'awaiting_host'

export type EventTypeCustomInputType = 'text' | 'textLong' | 'number' | 'bool' | 'radio' | 'phone'

export type ReminderType = 'PENDING_BOOKING_CONFIRMATION'

export type PaymentOption = 'ON_BOOKING' | 'HOLD'

export type WebhookTriggerEvents =
  | 'BOOKING_CREATED'
  | 'BOOKING_PAYMENT_INITIATED'
  | 'BOOKING_PAID'
  | 'BOOKING_RESCHEDULED'
  | 'BOOKING_REQUESTED'
  | 'BOOKING_CANCELLED'
  | 'BOOKING_REJECTED'
  | 'BOOKING_NO_SHOW_UPDATED'
  | 'FORM_SUBMITTED'
  | 'MEETING_ENDED'
  | 'MEETING_STARTED'
  | 'RECORDING_READY'
  | 'RECORDING_TRANSCRIPTION_GENERATED'
  | 'OOO_CREATED'
  | 'AFTER_HOSTS_CAL_VIDEO_NO_SHOW'
  | 'AFTER_GUESTS_CAL_VIDEO_NO_SHOW'
  | 'FORM_SUBMITTED_NO_EVENT'
  | 'DELEGATION_CREDENTIAL_ERROR'
  | 'WRONG_ASSIGNMENT_REPORT'

export type AppCategories =
  | 'calendar'
  | 'messaging'
  | 'other'
  | 'payment'
  | 'video'
  | 'web3'
  | 'automation'
  | 'analytics'
  | 'conferencing'
  | 'crm'

export type TimeUnit = 'day' | 'hour' | 'minute'

export type FeatureType = 'RELEASE' | 'EXPERIMENT' | 'OPERATIONAL' | 'KILL_SWITCH' | 'PERMISSION'

export type RRResetInterval = 'MONTH' | 'DAY'

export type RRTimestampBasis = 'CREATED_AT' | 'START_TIME'

export type OAuthClientType = 'confidential' | 'public'

export type OAuthClientStatus = 'pending' | 'approved' | 'rejected'

export type AccessScope = 'READ_BOOKING' | 'READ_PROFILE'

export type RedirectType = 'user-event-type' | 'team-event-type' | 'user' | 'team'

export type SMSLockState = 'LOCKED' | 'UNLOCKED' | 'REVIEW_NEEDED'

export type AttributeType = 'TEXT' | 'NUMBER' | 'SINGLE_SELECT' | 'MULTI_SELECT'

export type AssignmentReasonEnum =
  | 'REASSIGNED'
  | 'RR_REASSIGNED'
  | 'REROUTED'
  | 'SALESFORCE_ASSIGNMENT'

export type EventTypeAutoTranslatedField = 'DESCRIPTION' | 'TITLE'

export type WatchlistType = 'EMAIL' | 'DOMAIN' | 'USERNAME'

export type WatchlistAction = 'REPORT' | 'BLOCK' | 'ALERT'

export type WatchlistSource = 'MANUAL' | 'FREE_DOMAIN_POLICY' | 'SIGNUP'

export type BookingReportReason = 'SPAM' | 'dont_know_person' | 'OTHER'

export type BookingReportStatus = 'PENDING' | 'DISMISSED' | 'BLOCKED'

export type SystemReportStatus = 'PENDING' | 'BLOCKED' | 'DISMISSED'

export type WrongAssignmentReportStatus = 'PENDING' | 'REVIEWED' | 'RESOLVED' | 'DISMISSED'

export type BillingPeriod = 'MONTHLY' | 'ANNUALLY'

export type BillingMode = 'SEATS' | 'ACTIVE_USERS'

export type FilterSegmentScope = 'USER' | 'TEAM'

export type RoleType = 'SYSTEM' | 'CUSTOM'

export type BookingAuditType = 'record_created' | 'record_updated' | 'record_deleted'

export type BookingAuditAction =
  | 'created'
  | 'cancelled'
  | 'accepted'
  | 'rejected'
  | 'pending'
  | 'awaiting_host'
  | 'rescheduled'
  | 'attendee_added'
  | 'attendee_removed'
  | 'reassignment'
  | 'location_changed'
  | 'no_show_updated'
  | 'reschedule_requested'
  | 'seat_booked'
  | 'seat_rescheduled'

export type BookingAuditSource =
  | 'api_v1'
  | 'api_v2'
  | 'webapp'
  | 'webhook'
  | 'system'
  | 'magic_link'
  | 'unknown'

export type AuditActorType = 'user' | 'guest' | 'attendee' | 'system' | 'app'

export type PhoneNumberSubscriptionStatus =
  | 'ACTIVE'
  | 'PAST_DUE'
  | 'CANCELLED'
  | 'INCOMPLETE'
  | 'INCOMPLETE_EXPIRED'
  | 'TRIALING'
  | 'UNPAID'

export type SeatChangeType = 'ADDITION' | 'REMOVAL'

export type ProrationStatus = 'PENDING' | 'INVOICE_CREATED' | 'CHARGED' | 'FAILED' | 'CANCELLED'

export type CalendarCacheEventStatus = 'confirmed' | 'tentative' | 'cancelled'

export interface Host {
  userId: number
  eventTypeId: number
  isFixed: Generated<boolean>
  priority: number | null
  weight: number | null
  weightAdjustment: number | null
  scheduleId: number | null
  createdAt: Generated<Timestamp>
  groupId: string | null
  memberId: number | null
}

export interface HostGroup {
  id: string
  name: string
  eventTypeId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface HostLocation {
  id: string
  userId: number
  eventTypeId: number
  type: string
  credentialId: number | null
  link: string | null
  address: string | null
  phoneNumber: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface CalVideoSettings {
  eventTypeId: number
  disableRecordingForOrganizer: Generated<boolean>
  disableRecordingForGuests: Generated<boolean>
  enableAutomaticTranscription: Generated<boolean>
  enableAutomaticRecordingForOrganizer: Generated<boolean>
  redirectUrlOnExit: string | null
  disableTranscriptionForGuests: Generated<boolean>
  disableTranscriptionForOrganizer: Generated<boolean>
  requireEmailForGuests: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface VideoCallGuest {
  id: string
  bookingUid: string
  email: string
  name: string
  joinedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface EventType {
  id: Generated<number>
  title: string
  slug: string
  description: string | null
  interfaceLanguage: string | null
  position: Generated<number>
  locations: unknown
  length: number
  offsetStart: Generated<number>
  hidden: Generated<boolean>
  userId: number | null
  profileId: number | null
  teamId: number | null
  useEventLevelSelectedCalendars: Generated<boolean>
  eventName: string | null
  parentId: number | null
  bookingFields: unknown
  timeZone: string | null
  periodType: Generated<PeriodType>
  periodStartDate: Timestamp | null
  periodEndDate: Timestamp | null
  periodDays: number | null
  periodCountCalendarDays: boolean | null
  lockTimeZoneToggleOnBookingPage: Generated<boolean>
  lockedTimeZone: string | null
  requiresConfirmation: Generated<boolean>
  requiresConfirmationWillBlockSlot: Generated<boolean>
  requiresConfirmationForFreeEmail: Generated<boolean>
  requiresBookerEmailVerification: Generated<boolean>
  canSendCalVideoTranscriptionEmails: Generated<boolean>
  autoTranslateDescriptionEnabled: Generated<boolean>
  autoTranslateInstantMeetingTitleEnabled: Generated<boolean>
  recurringEvent: unknown
  disableGuests: Generated<boolean>
  hideCalendarNotes: Generated<boolean>
  hideCalendarEventDetails: Generated<boolean>
  minimumBookingNotice: Generated<number>
  beforeEventBuffer: Generated<number>
  afterEventBuffer: Generated<number>
  seatsPerTimeSlot: number | null
  onlyShowFirstAvailableSlot: Generated<boolean>
  showOptimizedSlots: Generated<boolean | null>
  disableCancelling: Generated<boolean | null>
  disableRescheduling: Generated<boolean | null>
  minimumRescheduleNotice: number | null
  seatsShowAttendees: Generated<boolean | null>
  seatsShowAvailabilityCount: Generated<boolean | null>
  schedulingType: SchedulingType | null
  scheduleId: number | null
  allowReschedulingCancelledBookings: Generated<boolean | null>
  price: Generated<number>
  currency: Generated<string>
  slotInterval: number | null
  metadata: unknown
  successRedirectUrl: string | null
  forwardParamsSuccessRedirect: Generated<boolean | null>
  bookingLimits: unknown
  durationLimits: unknown
  isInstantEvent: Generated<boolean>
  instantMeetingExpiryTimeOffsetInSeconds: Generated<number>
  instantMeetingScheduleId: number | null
  instantMeetingParameters: string[]
  assignAllTeamMembers: Generated<boolean>
  assignRRMembersUsingSegment: Generated<boolean>
  rrSegmentQueryValue: unknown
  useEventTypeDestinationCalendarEmail: Generated<boolean>
  isRRWeightsEnabled: Generated<boolean>
  maxLeadThreshold: number | null
  includeNoShowInRRCalculation: Generated<boolean>
  allowReschedulingPastBookings: Generated<boolean>
  hideOrganizerEmail: Generated<boolean>
  maxActiveBookingsPerBooker: number | null
  maxActiveBookingPerBookerOfferReschedule: Generated<boolean>
  customReplyToEmail: string | null
  eventTypeColor: unknown
  rescheduleWithSameRoundRobinHost: Generated<boolean>
  secondaryEmailId: number | null
  useBookerTimezone: Generated<boolean>
  restrictionScheduleId: number | null
  bookingRequiresAuthentication: Generated<boolean>
  rrHostSubsetEnabled: Generated<boolean>
  requiresCancellationReason: Generated<CancellationReasonRequirement | null>
  enablePerHostLocations: Generated<boolean>
  createdAt: Generated<Timestamp | null>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Credential {
  id: Generated<number>
  type: string
  key: unknown
  encryptedKey: string | null
  userId: number | null
  teamId: number | null
  appId: string | null
  subscriptionId: string | null
  paymentStatus: string | null
  billingCycleStart: number | null
  invalid: Generated<boolean | null>
  delegationCredentialId: string | null
}

export interface DestinationCalendar {
  id: Generated<number>
  integration: string
  externalId: string
  primaryEmail: string | null
  userId: number | null
  eventTypeId: number | null
  credentialId: number | null
  createdAt: Generated<Timestamp | null>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
  delegationCredentialId: string | null
  customCalendarReminder: number | null
}

export interface UserPassword {
  hash: string
  userId: number
}

export interface TravelSchedule {
  id: Generated<number>
  userId: number
  timeZone: string
  startDate: Timestamp
  endDate: Timestamp | null
  prevTimeZone: string | null
}

export interface User {
  id: Generated<number>
  uuid: string
  username: string | null
  name: string | null
  email: string
  emailVerified: Timestamp | null
  bio: string | null
  avatarUrl: string | null
  timeZone: Generated<string>
  weekStart: Generated<string>
  bufferTime: Generated<number>
  hideBranding: Generated<boolean>
  theme: string | null
  appTheme: string | null
  created: Generated<Timestamp>
  trialEndsAt: Timestamp | null
  lastActiveAt: Timestamp | null
  defaultScheduleId: number | null
  completedOnboarding: Generated<boolean>
  locale: string | null
  timeFormat: Generated<number | null>
  twoFactorSecret: string | null
  twoFactorEnabled: Generated<boolean>
  backupCodes: string | null
  identityProvider: Generated<IdentityProvider>
  identityProviderId: string | null
  invitedTo: number | null
  brandColor: string | null
  darkBrandColor: string | null
  allowDynamicBooking: Generated<boolean | null>
  allowSEOIndexing: Generated<boolean | null>
  receiveMonthlyDigestEmail: Generated<boolean | null>
  requiresBookerEmailVerification: Generated<boolean | null>
  metadata: unknown
  verified: Generated<boolean | null>
  role: Generated<UserPermissionRole>
  organizationId: number | null
  locked: Generated<boolean>
  movedToProfileId: number | null
  isPlatformManaged: Generated<boolean>
  smsLockState: Generated<SMSLockState>
  smsLockReviewedByAdmin: Generated<boolean>
  referralLinkId: string | null
  creationSource: CreationSource | null
  autoOptInFeatures: Generated<boolean>
}

export interface NotificationsSubscriptions {
  id: Generated<number>
  userId: number
  subscription: string
}

export interface Profile {
  id: Generated<number>
  uid: string
  userId: number
  organizationId: number
  username: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Team {
  id: Generated<number>
  name: string
  slug: string | null
  logoUrl: string | null
  calVideoLogo: string | null
  appLogo: string | null
  appIconLogo: string | null
  bio: string | null
  hideBranding: Generated<boolean>
  hideTeamProfileLink: Generated<boolean>
  isPrivate: Generated<boolean>
  hideBookATeamMember: Generated<boolean>
  createdAt: Generated<Timestamp>
  metadata: unknown
  theme: string | null
  rrResetInterval: Generated<RRResetInterval | null>
  rrTimestampBasis: Generated<RRTimestampBasis>
  brandColor: string | null
  darkBrandColor: string | null
  bannerUrl: string | null
  parentId: number | null
  timeFormat: number | null
  timeZone: Generated<string>
  weekStart: Generated<string>
  isOrganization: Generated<boolean>
  pendingPayment: Generated<boolean>
  isPlatform: Generated<boolean>
  createdByOAuthClientId: string | null
  smsLockState: Generated<SMSLockState>
  smsLockReviewedByAdmin: Generated<boolean>
  bookingLimits: unknown
  includeManagedEventsInLimits: Generated<boolean>
  autoOptInFeatures: Generated<boolean>
}

export interface CreditBalance {
  id: string
  teamId: number | null
  userId: number | null
  additionalCredits: Generated<number>
  limitReachedAt: Timestamp | null
  warningSentAt: Timestamp | null
}

export interface CreditPurchaseLog {
  id: string
  creditBalanceId: string
  credits: number
  createdAt: Generated<Timestamp>
}

export interface CreditExpenseLog {
  id: string
  creditBalanceId: string
  bookingUid: string | null
  credits: number | null
  creditType: CreditType
  date: Timestamp
  smsSid: string | null
  smsSegments: number | null
  phoneNumber: string | null
  email: string | null
  callDuration: number | null
  creditFor: CreditUsageType | null
  externalRef: string | null
}

export interface OrganizationSettings {
  id: Generated<number>
  organizationId: number
  isOrganizationConfigured: Generated<boolean>
  isOrganizationVerified: Generated<boolean>
  orgAutoAcceptEmail: string
  lockEventTypeCreationForUsers: Generated<boolean>
  adminGetsNoSlotsNotification: Generated<boolean>
  isAdminReviewed: Generated<boolean>
  isAdminAPIEnabled: Generated<boolean>
  allowSEOIndexing: Generated<boolean>
  orgProfileRedirectsToVerifiedDomain: Generated<boolean>
  disablePhoneOnlySMSNotifications: Generated<boolean>
  disableAutofillOnBookingPage: Generated<boolean>
  orgAutoJoinOnSignup: Generated<boolean>
  disableAttendeeConfirmationEmail: Generated<boolean>
  disableAttendeeCancellationEmail: Generated<boolean>
  disableAttendeeRescheduledEmail: Generated<boolean>
  disableAttendeeRequestEmail: Generated<boolean>
  disableAttendeeReassignedEmail: Generated<boolean>
  disableAttendeeAwaitingPaymentEmail: Generated<boolean>
  disableAttendeeRescheduleRequestEmail: Generated<boolean>
  disableAttendeeLocationChangeEmail: Generated<boolean>
  disableAttendeeNewEventEmail: Generated<boolean>
}

export interface Membership {
  id: Generated<number>
  teamId: number
  userId: number
  accepted: Generated<boolean>
  role: MembershipRole
  customRoleId: string | null
  createdAt: Generated<Timestamp | null>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface VerificationToken {
  id: Generated<number>
  identifier: string
  token: string
  expires: Timestamp
  expiresInDays: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  teamId: number | null
  secondaryEmailId: number | null
}

export interface InstantMeetingToken {
  id: Generated<number>
  token: string
  expires: Timestamp
  teamId: number
  bookingId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BookingReference {
  id: Generated<number>
  type: string
  uid: string
  meetingId: string | null
  thirdPartyRecurringEventId: string | null
  meetingPassword: string | null
  meetingUrl: string | null
  bookingId: number | null
  externalCalendarId: string | null
  deleted: boolean | null
  credentialId: number | null
  delegationCredentialId: string | null
}

export interface Attendee {
  id: Generated<number>
  email: string
  name: string
  timeZone: string
  phoneNumber: string | null
  locale: Generated<string | null>
  bookingId: number | null
  noShow: Generated<boolean | null>
}

export interface Booking {
  id: Generated<number>
  uid: string
  idempotencyKey: string | null
  userId: number | null
  userPrimaryEmail: string | null
  eventTypeId: number | null
  title: string
  description: string | null
  customInputs: unknown
  responses: unknown
  startTime: Timestamp
  endTime: Timestamp
  location: string | null
  createdAt: Generated<Timestamp>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
  status: Generated<BookingStatus>
  paid: Generated<boolean>
  destinationCalendarId: number | null
  cancellationReason: string | null
  rejectionReason: string | null
  reassignReason: string | null
  reassignById: number | null
  dynamicEventSlugRef: string | null
  dynamicGroupSlugRef: string | null
  rescheduled: boolean | null
  fromReschedule: string | null
  recurringEventId: string | null
  smsReminderNumber: string | null
  scheduledJobs: string[]
  metadata: unknown
  isRecorded: Generated<boolean>
  iCalUID: Generated<string | null>
  iCalSequence: Generated<number>
  rating: number | null
  ratingFeedback: string | null
  noShowHost: Generated<boolean | null>
  oneTimePassword: string | null
  cancelledBy: string | null
  rescheduledBy: string | null
  creationSource: CreationSource | null
}

export interface Tracking {
  id: Generated<number>
  bookingId: number
  utm_source: string | null
  utm_medium: string | null
  utm_campaign: string | null
  utm_term: string | null
  utm_content: string | null
}

export interface Schedule {
  id: Generated<number>
  userId: number
  name: string
  timeZone: string | null
}

export interface Availability {
  id: Generated<number>
  userId: number | null
  eventTypeId: number | null
  days: number[]
  startTime: string
  endTime: string
  date: Timestamp | null
  scheduleId: number | null
}

export interface SelectedCalendar {
  id: string
  userId: number
  integration: string
  externalId: string
  credentialId: number | null
  createdAt: Generated<Timestamp | null>
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
  googleChannelId: string | null
  googleChannelKind: string | null
  googleChannelResourceId: string | null
  googleChannelResourceUri: string | null
  googleChannelExpiration: string | null
  channelId: string | null
  channelKind: string | null
  channelResourceId: string | null
  channelResourceUri: string | null
  channelExpiration: Timestamp | null
  syncSubscribedAt: Timestamp | null
  syncSubscribedErrorAt: Timestamp | null
  syncSubscribedErrorCount: Generated<number>
  syncToken: string | null
  syncedAt: Timestamp | null
  syncErrorAt: Timestamp | null
  syncErrorCount: Generated<number | null>
  delegationCredentialId: string | null
  error: string | null
  lastErrorAt: Timestamp | null
  watchAttempts: Generated<number>
  unwatchAttempts: Generated<number>
  maxAttempts: Generated<number>
  eventTypeId: number | null
}

export interface EventTypeCustomInput {
  id: Generated<number>
  eventTypeId: number
  label: string
  type: EventTypeCustomInputType
  options: unknown
  required: boolean
  placeholder: Generated<string>
}

export interface ResetPasswordRequest {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  email: string
  expires: Timestamp
}

export interface ReminderMail {
  id: Generated<number>
  referenceId: number
  reminderType: ReminderType
  elapsedMinutes: number
  createdAt: Generated<Timestamp>
}

export interface Payment {
  id: Generated<number>
  uid: string
  appId: string | null
  bookingId: number
  amount: number
  fee: number
  currency: string
  success: boolean
  refunded: boolean
  data: unknown
  externalId: string
  paymentOption: Generated<PaymentOption | null>
}

export interface Webhook {
  id: string
  userId: number | null
  teamId: number | null
  eventTypeId: number | null
  platformOAuthClientId: string | null
  subscriberUrl: string
  payloadTemplate: string | null
  createdAt: Generated<Timestamp>
  active: Generated<boolean>
  eventTriggers: WebhookTriggerEvents[]
  appId: string | null
  secret: string | null
  platform: Generated<boolean>
  time: number | null
  timeUnit: TimeUnit | null
  version: Generated<string>
}

export interface ApiKey {
  id: string
  userId: number
  teamId: number | null
  note: string | null
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp | null
  lastUsedAt: Timestamp | null
  hashedKey: string
  appId: string | null
}

export interface RateLimit {
  id: string
  name: string
  apiKeyId: string
  ttl: number
  limit: number
  blockDuration: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface HashedLink {
  id: Generated<number>
  link: string
  eventTypeId: number
  expiresAt: Timestamp | null
  maxUsageCount: Generated<number>
  usageCount: Generated<number>
}

export interface Account {
  id: string
  userId: number
  type: string
  provider: string
  providerAccountId: string
  providerEmail: string | null
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: number
  expires: Timestamp
}

export interface App {
  slug: string
  dirName: string
  keys: unknown
  categories: AppCategories[]
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  enabled: Generated<boolean>
}

export interface Feedback {
  id: Generated<number>
  date: Generated<Timestamp>
  userId: number
  rating: string
  comment: string | null
}

export interface Deployment {
  id: Generated<number>
  logo: string | null
  theme: unknown
  licenseKey: string | null
  signatureTokenEncrypted: string | null
  agreedLicenseAt: Timestamp | null
}

export interface WebhookScheduledTriggers {
  id: Generated<number>
  jobName: string | null
  subscriberUrl: string
  payload: string
  startAfter: Timestamp
  retryCount: Generated<number>
  createdAt: Generated<Timestamp | null>
  appId: string | null
  webhookId: string | null
  bookingId: number | null
}

export interface BookingSeat {
  id: Generated<number>
  referenceUid: string
  bookingId: number
  attendeeId: number
  data: unknown
  metadata: unknown
}

export interface VerifiedNumber {
  id: Generated<number>
  userId: number | null
  teamId: number | null
  phoneNumber: string
}

export interface VerifiedEmail {
  id: Generated<number>
  userId: number | null
  teamId: number | null
  email: string
}

export interface Feature {
  slug: string
  enabled: Generated<boolean>
  description: string | null
  type: Generated<FeatureType | null>
  stale: Generated<boolean | null>
  lastUsedAt: Timestamp | null
  createdAt: Generated<Timestamp | null>
  updatedAt: Generated<ColumnType<Date | null, Date | string, Date | string | null>>
  updatedBy: number | null
}

export interface UserFeatures {
  userId: number
  featureId: string
  enabled: boolean
  assignedAt: Generated<Timestamp>
  assignedBy: string
  updatedAt: Timestamp
}

export interface TeamFeatures {
  teamId: number
  featureId: string
  enabled: boolean
  assignedAt: Generated<Timestamp>
  assignedBy: string
  updatedAt: Timestamp
}

export interface SelectedSlots {
  id: Generated<number>
  eventTypeId: number
  userId: number
  slotUtcStartDate: Timestamp
  slotUtcEndDate: Timestamp
  uid: string
  releaseAt: Timestamp
  isSeat: Generated<boolean>
}

export interface OAuthClient {
  clientId: string
  redirectUri: string
  clientSecret: string | null
  clientType: Generated<OAuthClientType>
  name: string
  purpose: string | null
  logo: string | null
  websiteUrl: string | null
  rejectionReason: string | null
  isTrusted: Generated<boolean>
  status: Generated<OAuthClientStatus>
  userId: number | null
  createdAt: Generated<Timestamp>
}

export interface AccessCode {
  id: Generated<number>
  code: string
  clientId: string | null
  expiresAt: Timestamp
  scopes: AccessScope[]
  userId: number | null
  teamId: number | null
  codeChallenge: string | null
  codeChallengeMethod: Generated<string | null>
}

export interface BookingDenormalized {
  id: number
  uid: string
  eventTypeId: number | null
  title: string
  description: string | null
  startTime: Timestamp
  endTime: Timestamp
  createdAt: Timestamp
  updatedAt: Timestamp | null
  location: string | null
  paid: boolean
  status: BookingStatus
  rescheduled: boolean | null
  userId: number | null
  teamId: number | null
  eventLength: number | null
  eventParentId: number | null
  userEmail: string | null
  userName: string | null
  userUsername: string | null
  ratingFeedback: string | null
  rating: number | null
  noShowHost: boolean | null
  isTeamBooking: boolean
}

export interface CalendarCache {
  id: string | null
  key: string
  value: unknown
  expiresAt: Timestamp
  updatedAt: Generated<Timestamp>
  credentialId: number
  userId: number | null
}

export interface TempOrgRedirect {
  id: Generated<number>
  from: string
  fromOrgId: number
  type: RedirectType
  toUrl: string
  enabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Avatar {
  teamId: Generated<number>
  userId: Generated<number>
  data: string
  objectKey: string
  isBanner: Generated<boolean>
}

export interface OutOfOfficeEntry {
  id: Generated<number>
  uuid: string
  start: Timestamp
  end: Timestamp
  notes: string | null
  showNotePublicly: Generated<boolean>
  userId: number
  toUserId: number | null
  reasonId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OutOfOfficeReason {
  id: Generated<number>
  emoji: string
  reason: string
  enabled: Generated<boolean>
  userId: number | null
}

export interface UserHolidaySettings {
  id: Generated<number>
  userId: number
  countryCode: string | null
  disabledIds: string[]
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface HolidayCache {
  id: string
  countryCode: string
  calendarId: string
  eventId: string
  name: string
  date: Timestamp
  year: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PlatformOAuthClient {
  id: string
  name: string
  secret: string
  permissions: number
  logo: string | null
  redirectUris: string[]
  organizationId: number
  bookingRedirectUri: string | null
  bookingCancelRedirectUri: string | null
  bookingRescheduleRedirectUri: string | null
  areEmailsEnabled: Generated<boolean>
  areDefaultEventTypesEnabled: Generated<boolean>
  areCalendarEventsEnabled: Generated<boolean>
  createdAt: Generated<Timestamp>
}

export interface PlatformAuthorizationToken {
  id: string
  platformOAuthClientId: string
  userId: number
  createdAt: Generated<Timestamp>
}

export interface AccessToken {
  id: Generated<number>
  secret: string
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
  platformOAuthClientId: string
  userId: number
}

export interface RefreshToken {
  id: Generated<number>
  secret: string
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
  platformOAuthClientId: string
  userId: number
}

export interface DSyncData {
  id: Generated<number>
  directoryId: string
  tenant: string
  organizationId: number | null
}

export interface DSyncTeamGroupMapping {
  id: Generated<number>
  organizationId: number
  teamId: number
  directoryId: string
  groupName: string
}

export interface SecondaryEmail {
  id: Generated<number>
  userId: number
  email: string
  emailVerified: Timestamp | null
}

export interface Task {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  scheduledAt: Generated<Timestamp>
  succeededAt: Timestamp | null
  type: string
  payload: string
  attempts: Generated<number>
  maxAttempts: Generated<number>
  lastError: string | null
  lastFailedAttemptAt: Timestamp | null
  referenceUid: string | null
}

export interface ManagedOrganization {
  managedOrganizationId: number
  managerOrganizationId: number
  createdAt: Generated<Timestamp>
}

export interface PlatformBilling {
  id: number
  customerId: string
  subscriptionId: string | null
  priceId: string | null
  plan: Generated<string>
  billingCycleStart: number | null
  billingCycleEnd: number | null
  overdue: Generated<boolean | null>
  managerBillingId: number | null
}

export interface AttributeOption {
  id: string
  attributeId: string
  value: string
  slug: string
  isGroup: Generated<boolean>
  contains: string[]
}

export interface Attribute {
  id: string
  teamId: number
  type: AttributeType
  name: string
  slug: string
  enabled: Generated<boolean>
  usersCanEditRelation: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  isWeightsEnabled: Generated<boolean>
  isLocked: Generated<boolean>
}

export interface AttributeToUser {
  id: string
  memberId: number
  attributeOptionId: string
  weight: number | null
  createdAt: Generated<Timestamp>
  createdById: number | null
  createdByDSyncId: string | null
  updatedAt: ColumnType<Date | null, Date | string, Date | string | null>
  updatedById: number | null
  updatedByDSyncId: string | null
}

export interface AssignmentReason {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  bookingId: number
  reasonEnum: AssignmentReasonEnum
  reasonString: string
}

export interface DelegationCredential {
  id: string
  workspacePlatformId: number
  serviceAccountKey: unknown
  enabled: Generated<boolean>
  lastEnabledAt: Timestamp | null
  lastDisabledAt: Timestamp | null
  organizationId: number
  domain: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkspacePlatform {
  id: Generated<number>
  slug: string
  name: string
  description: string
  defaultServiceAccountKey: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  enabled: Generated<boolean>
}

export interface EventTypeTranslation {
  uid: string
  eventTypeId: number
  field: EventTypeAutoTranslatedField
  sourceLocale: string
  targetLocale: string
  translatedText: string
  createdAt: Generated<Timestamp>
  createdBy: number
  updatedAt: Timestamp
  updatedBy: number | null
}

export interface Watchlist {
  id: string
  type: WatchlistType
  value: string
  description: string | null
  isGlobal: Generated<boolean>
  organizationId: number | null
  action: Generated<WatchlistAction>
  source: Generated<WatchlistSource>
  lastUpdatedAt: Generated<Timestamp>
}

export interface WatchlistAudit {
  id: string
  type: WatchlistType
  value: string
  description: string | null
  action: Generated<WatchlistAction>
  changedAt: Generated<Timestamp>
  changedByUserId: number | null
  watchlistId: string | null
}

export interface WatchlistEventAudit {
  id: string
  watchlistId: string
  eventTypeId: number
  actionTaken: WatchlistAction
  timestamp: Generated<Timestamp>
}

export interface BookingReport {
  id: string
  bookingUid: string
  bookerEmail: string
  reportedById: number | null
  organizationId: number | null
  reason: BookingReportReason
  description: string | null
  cancelled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  status: Generated<BookingReportStatus>
  systemStatus: Generated<SystemReportStatus>
  watchlistId: string | null
  globalWatchlistId: string | null
}

export interface WrongAssignmentReport {
  id: string
  bookingUid: string
  reportedById: number | null
  correctAssignee: string | null
  additionalNotes: string
  teamId: number | null
  status: Generated<WrongAssignmentReportStatus>
  reviewedById: number | null
  reviewedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationOnboarding {
  id: string
  createdById: number
  createdAt: Generated<Timestamp>
  orgOwnerEmail: string
  error: string | null
  updatedAt: Timestamp
  organizationId: number | null
  billingPeriod: BillingPeriod
  pricePerSeat: number
  seats: number
  isPlatform: Generated<boolean>
  name: string
  slug: string
  logo: string | null
  bio: string | null
  brandColor: string | null
  bannerUrl: string | null
  isDomainConfigured: Generated<boolean>
  stripeCustomerId: string | null
  stripeSubscriptionId: string | null
  stripeSubscriptionItemId: string | null
  invitedMembers: Generated<unknown>
  teams: Generated<unknown>
  isComplete: Generated<boolean>
}

export interface InternalNotePreset {
  id: Generated<number>
  name: string
  cancellationReason: string | null
  teamId: number
  createdAt: Generated<Timestamp>
}

export interface FilterSegment {
  id: Generated<number>
  name: string
  tableIdentifier: string
  scope: FilterSegmentScope
  activeFilters: unknown
  sorting: unknown
  columnVisibility: unknown
  columnSizing: unknown
  perPage: number
  searchTerm: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: number
  teamId: number | null
}

export interface UserFilterSegmentPreference {
  id: Generated<number>
  userId: number
  tableIdentifier: string
  segmentId: number | null
  systemSegmentId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BookingInternalNote {
  id: Generated<number>
  notePresetId: number | null
  text: string | null
  bookingId: number
  createdById: number
  createdAt: Generated<Timestamp>
}

export interface Role {
  id: string
  name: string
  color: string | null
  description: string | null
  teamId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: Generated<RoleType>
}

export interface RolePermission {
  id: string
  roleId: string
  resource: string
  action: string
  createdAt: Generated<Timestamp>
}

export interface AuditActor {
  id: string
  type: AuditActorType
  userUuid: string | null
  attendeeId: number | null
  credentialId: number | null
  email: string | null
  phone: string | null
  name: string | null
  createdAt: Generated<Timestamp>
}

export interface BookingAudit {
  id: string
  bookingUid: string
  actorId: string
  type: BookingAuditType
  action: BookingAuditAction
  timestamp: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  source: BookingAuditSource
  operationId: string
  data: unknown
  context: unknown
}

export interface Agent {
  id: string
  name: string
  userId: number | null
  teamId: number | null
  providerAgentId: string
  inboundEventTypeId: number | null
  outboundEventTypeId: number | null
  enabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface CalAiPhoneNumber {
  id: Generated<number>
  userId: number | null
  teamId: number | null
  phoneNumber: string
  provider: string
  providerPhoneNumberId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  stripeCustomerId: string | null
  stripeSubscriptionId: string | null
  subscriptionStatus: PhoneNumberSubscriptionStatus | null
  inboundAgentId: string | null
  outboundAgentId: string | null
}

export interface TeamBilling {
  id: string
  teamId: number
  subscriptionId: string
  subscriptionItemId: string
  customerId: string
  status: string
  planName: string
  subscriptionStart: Timestamp | null
  subscriptionTrialEnd: Timestamp | null
  subscriptionEnd: Timestamp | null
  billingPeriod: BillingPeriod | null
  billingMode: Generated<BillingMode>
  pricePerSeat: number | null
  paidSeats: number | null
  highWaterMark: number | null
  highWaterMarkPeriodStart: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OrganizationBilling {
  id: string
  teamId: number
  subscriptionId: string
  subscriptionItemId: string
  customerId: string
  status: string
  planName: string
  subscriptionStart: Timestamp | null
  subscriptionTrialEnd: Timestamp | null
  subscriptionEnd: Timestamp | null
  billingPeriod: BillingPeriod | null
  billingMode: Generated<BillingMode>
  pricePerSeat: number | null
  paidSeats: number | null
  highWaterMark: number | null
  highWaterMarkPeriodStart: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SeatChangeLog {
  id: string
  teamId: number
  changeType: SeatChangeType
  seatCount: number
  userId: number | null
  triggeredBy: number | null
  changeDate: Generated<Timestamp>
  monthKey: string
  operationId: string | null
  processedInProrationId: string | null
  metadata: unknown
  createdAt: Generated<Timestamp>
  teamBillingId: string | null
  organizationBillingId: string | null
}

export interface MonthlyProration {
  id: string
  teamId: number
  monthKey: string
  periodStart: Timestamp
  periodEnd: Timestamp
  seatsAtStart: number
  seatsAdded: number
  seatsRemoved: number
  netSeatIncrease: number
  seatsAtEnd: number
  subscriptionId: string
  subscriptionItemId: string
  customerId: string
  subscriptionStart: Timestamp
  subscriptionEnd: Timestamp
  remainingDays: number
  pricePerSeat: number
  proratedAmount: number
  invoiceItemId: string | null
  invoiceId: string | null
  invoiceUrl: string | null
  status: Generated<ProrationStatus>
  chargedAt: Timestamp | null
  failedAt: Timestamp | null
  failureReason: string | null
  retryCount: Generated<number>
  metadata: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  teamBillingId: string | null
  organizationBillingId: string | null
}

export interface CalendarCacheEvent {
  id: string
  selectedCalendarId: string
  externalId: string
  externalEtag: string
  iCalUID: string | null
  iCalSequence: Generated<number>
  summary: string | null
  description: string | null
  location: string | null
  start: Timestamp
  end: Timestamp
  isAllDay: Generated<boolean>
  timeZone: string | null
  status: Generated<CalendarCacheEventStatus>
  recurringEventId: string | null
  originalStartTime: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  externalCreatedAt: Timestamp | null
  externalUpdatedAt: Timestamp | null
}

export interface IntegrationAttributeSync {
  id: string
  organizationId: number
  name: string
  integration: string
  credentialId: number | null
  enabled: boolean
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface AttributeSyncRule {
  id: string
  integrationAttributeSyncId: string
  rule: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface AttributeSyncFieldMapping {
  id: string
  integrationFieldName: string
  attributeId: string
  enabled: boolean
  integrationAttributeSyncId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface BookingTimeStatus {
  id: number
  uid: string | null
  eventTypeId: number | null
  title: string | null
  description: string | null
  startTime: Timestamp | null
  endTime: Timestamp | null
  createdAt: Timestamp | null
  location: string | null
  paid: boolean | null
  status: BookingStatus | null
  rescheduled: boolean | null
  userId: number | null
  teamId: number | null
  eventLength: number | null
  timeStatus: string | null
  eventParentId: number | null
  userEmail: string | null
  username: string | null
  ratingFeedback: string | null
  rating: number | null
  noShowHost: boolean | null
  isTeamBooking: boolean
}

export interface BookingTimeStatusDenormalized {
  id: number
  uid: string
  eventTypeId: number | null
  title: string
  description: string | null
  startTime: Timestamp
  endTime: Timestamp
  createdAt: Timestamp
  updatedAt: Timestamp | null
  location: string | null
  paid: boolean
  status: BookingStatus
  rescheduled: boolean | null
  userId: number | null
  teamId: number | null
  eventLength: number | null
  eventParentId: number | null
  userEmail: string | null
  userName: string | null
  userUsername: string | null
  ratingFeedback: string | null
  rating: number | null
  noShowHost: boolean | null
  isTeamBooking: boolean
  timeStatus: string | null
}

export interface UserEventtype {
  A: number
  B: number
}

export interface PlatformOauthClientToUser {
  A: string
  B: number
}

export interface DB {
  Host: Host
  HostGroup: HostGroup
  HostLocation: HostLocation
  CalVideoSettings: CalVideoSettings
  VideoCallGuest: VideoCallGuest
  EventType: EventType
  Credential: Credential
  DestinationCalendar: DestinationCalendar
  UserPassword: UserPassword
  TravelSchedule: TravelSchedule
  users: User
  NotificationsSubscriptions: NotificationsSubscriptions
  Profile: Profile
  Team: Team
  CreditBalance: CreditBalance
  CreditPurchaseLog: CreditPurchaseLog
  CreditExpenseLog: CreditExpenseLog
  OrganizationSettings: OrganizationSettings
  Membership: Membership
  VerificationToken: VerificationToken
  InstantMeetingToken: InstantMeetingToken
  BookingReference: BookingReference
  Attendee: Attendee
  Booking: Booking
  Tracking: Tracking
  Schedule: Schedule
  Availability: Availability
  SelectedCalendar: SelectedCalendar
  EventTypeCustomInput: EventTypeCustomInput
  ResetPasswordRequest: ResetPasswordRequest
  ReminderMail: ReminderMail
  Payment: Payment
  Webhook: Webhook
  ApiKey: ApiKey
  RateLimit: RateLimit
  HashedLink: HashedLink
  Account: Account
  Session: Session
  App: App
  Feedback: Feedback
  Deployment: Deployment
  WebhookScheduledTriggers: WebhookScheduledTriggers
  BookingSeat: BookingSeat
  VerifiedNumber: VerifiedNumber
  VerifiedEmail: VerifiedEmail
  Feature: Feature
  UserFeatures: UserFeatures
  TeamFeatures: TeamFeatures
  SelectedSlots: SelectedSlots
  OAuthClient: OAuthClient
  AccessCode: AccessCode
  BookingDenormalized: BookingDenormalized
  CalendarCache: CalendarCache
  TempOrgRedirect: TempOrgRedirect
  avatars: Avatar
  OutOfOfficeEntry: OutOfOfficeEntry
  OutOfOfficeReason: OutOfOfficeReason
  UserHolidaySettings: UserHolidaySettings
  HolidayCache: HolidayCache
  PlatformOAuthClient: PlatformOAuthClient
  PlatformAuthorizationToken: PlatformAuthorizationToken
  AccessToken: AccessToken
  RefreshToken: RefreshToken
  DSyncData: DSyncData
  DSyncTeamGroupMapping: DSyncTeamGroupMapping
  SecondaryEmail: SecondaryEmail
  Task: Task
  ManagedOrganization: ManagedOrganization
  PlatformBilling: PlatformBilling
  AttributeOption: AttributeOption
  Attribute: Attribute
  AttributeToUser: AttributeToUser
  AssignmentReason: AssignmentReason
  DelegationCredential: DelegationCredential
  WorkspacePlatform: WorkspacePlatform
  EventTypeTranslation: EventTypeTranslation
  Watchlist: Watchlist
  WatchlistAudit: WatchlistAudit
  WatchlistEventAudit: WatchlistEventAudit
  BookingReport: BookingReport
  WrongAssignmentReport: WrongAssignmentReport
  OrganizationOnboarding: OrganizationOnboarding
  InternalNotePreset: InternalNotePreset
  FilterSegment: FilterSegment
  UserFilterSegmentPreference: UserFilterSegmentPreference
  BookingInternalNote: BookingInternalNote
  Role: Role
  RolePermission: RolePermission
  AuditActor: AuditActor
  BookingAudit: BookingAudit
  Agent: Agent
  CalAiPhoneNumber: CalAiPhoneNumber
  TeamBilling: TeamBilling
  OrganizationBilling: OrganizationBilling
  SeatChangeLog: SeatChangeLog
  MonthlyProration: MonthlyProration
  CalendarCacheEvent: CalendarCacheEvent
  IntegrationAttributeSync: IntegrationAttributeSync
  AttributeSyncRule: AttributeSyncRule
  AttributeSyncFieldMapping: AttributeSyncFieldMapping
  BookingTimeStatus: BookingTimeStatus
  BookingTimeStatusDenormalized: BookingTimeStatusDenormalized
  _user_eventtype: UserEventtype
  _PlatformOAuthClientToUser: PlatformOauthClientToUser
}
