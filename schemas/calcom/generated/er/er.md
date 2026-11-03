```mermaid
erDiagram
    users ||--o{ Host : "(id) - (userId)"
    EventType ||--o{ Host : "(id) - (eventTypeId)"
    Schedule |o--o{ Host : "(id) - (scheduleId)"
    HostGroup |o--o{ Host : "(id) - (groupId)"
    Membership |o--o{ Host : "(id) - (memberId)"
    EventType |o--o{ HostGroup : "(id) - (eventTypeId)"
    Host ||--o| HostLocation : "(userId) - (userId)"
    Credential |o--o{ HostLocation : "(id) - (credentialId)"
    EventType ||--o| CalVideoSettings : "(id) - (eventTypeId)"
    users |o--o{ EventType : "(id) - (userId)"
    Profile |o--o{ EventType : "(id) - (profileId)"
    Team |o--o{ EventType : "(id) - (teamId)"
    EventType |o--o{ EventType : "(id) - (parentId)"
    Schedule |o--o{ EventType : "(id) - (scheduleId)"
    Schedule |o--o{ EventType : "(id) - (instantMeetingScheduleId)"
    SecondaryEmail |o--o{ EventType : "(id) - (secondaryEmailId)"
    Schedule |o--o{ EventType : "(id) - (restrictionScheduleId)"
    users |o--o{ Credential : "(id) - (userId)"
    Team |o--o{ Credential : "(id) - (teamId)"
    App |o--o{ Credential : "(slug) - (appId)"
    DelegationCredential |o--o{ Credential : "(id) - (delegationCredentialId)"
    users |o--o| DestinationCalendar : "(id) - (userId)"
    EventType |o--o| DestinationCalendar : "(id) - (eventTypeId)"
    Credential |o--o{ DestinationCalendar : "(id) - (credentialId)"
    DelegationCredential |o--o{ DestinationCalendar : "(id) - (delegationCredentialId)"
    users ||--o| UserPassword : "(id) - (userId)"
    users ||--o{ TravelSchedule : "(id) - (userId)"
    Team |o--o{ users : "(id) - (organizationId)"
    Profile |o--o| users : "(id) - (movedToProfileId)"
    users ||--o{ NotificationsSubscriptions : "(id) - (userId)"
    users ||--o{ Profile : "(id) - (userId)"
    Team ||--o{ Profile : "(id) - (organizationId)"
    Team |o--o{ Team : "(id) - (parentId)"
    PlatformOAuthClient |o--o{ Team : "(id) - (createdByOAuthClientId)"
    Team |o--o| CreditBalance : "(id) - (teamId)"
    users |o--o| CreditBalance : "(id) - (userId)"
    CreditBalance ||--o{ CreditPurchaseLog : "(id) - (creditBalanceId)"
    CreditBalance ||--o{ CreditExpenseLog : "(id) - (creditBalanceId)"
    Booking |o--o{ CreditExpenseLog : "(uid) - (bookingUid)"
    Team ||--o| OrganizationSettings : "(id) - (organizationId)"
    Role |o--o{ Membership : "(id) - (customRoleId)"
    Team ||--o{ Membership : "(id) - (teamId)"
    users ||--o{ Membership : "(id) - (userId)"
    Team |o--o{ VerificationToken : "(id) - (teamId)"
    SecondaryEmail |o--o{ VerificationToken : "(id) - (secondaryEmailId)"
    Team ||--o{ InstantMeetingToken : "(id) - (teamId)"
    Booking |o--o| InstantMeetingToken : "(id) - (bookingId)"
    Booking |o--o{ BookingReference : "(id) - (bookingId)"
    Credential |o--o{ BookingReference : "(id) - (credentialId)"
    DelegationCredential |o--o{ BookingReference : "(id) - (delegationCredentialId)"
    Booking |o--o{ Attendee : "(id) - (bookingId)"
    users |o--o{ Booking : "(id) - (userId)"
    EventType |o--o{ Booking : "(id) - (eventTypeId)"
    DestinationCalendar |o--o{ Booking : "(id) - (destinationCalendarId)"
    users |o--o{ Booking : "(id) - (reassignById)"
    Booking ||--o| Tracking : "(id) - (bookingId)"
    users ||--o{ Schedule : "(id) - (userId)"
    users |o--o{ Availability : "(id) - (userId)"
    EventType |o--o{ Availability : "(id) - (eventTypeId)"
    Schedule |o--o{ Availability : "(id) - (scheduleId)"
    users ||--o{ SelectedCalendar : "(id) - (userId)"
    Credential |o--o{ SelectedCalendar : "(id) - (credentialId)"
    DelegationCredential |o--o{ SelectedCalendar : "(id) - (delegationCredentialId)"
    EventType |o--o{ SelectedCalendar : "(id) - (eventTypeId)"
    EventType ||--o{ EventTypeCustomInput : "(id) - (eventTypeId)"
    App |o--o{ Payment : "(slug) - (appId)"
    Booking |o--o{ Payment : "(id) - (bookingId)"
    users |o--o{ Webhook : "(id) - (userId)"
    Team |o--o{ Webhook : "(id) - (teamId)"
    EventType |o--o{ Webhook : "(id) - (eventTypeId)"
    PlatformOAuthClient |o--o{ Webhook : "(id) - (platformOAuthClientId)"
    App |o--o{ Webhook : "(slug) - (appId)"
    users |o--o{ ApiKey : "(id) - (userId)"
    Team |o--o{ ApiKey : "(id) - (teamId)"
    App |o--o{ ApiKey : "(slug) - (appId)"
    ApiKey ||--o{ RateLimit : "(id) - (apiKeyId)"
    EventType ||--o{ HashedLink : "(id) - (eventTypeId)"
    users |o--o{ Account : "(id) - (userId)"
    users |o--o{ Session : "(id) - (userId)"
    users ||--o{ Feedback : "(id) - (userId)"
    Webhook |o--o{ WebhookScheduledTriggers : "(id) - (webhookId)"
    Booking |o--o{ WebhookScheduledTriggers : "(id) - (bookingId)"
    Booking ||--o{ BookingSeat : "(id) - (bookingId)"
    Attendee ||--o| BookingSeat : "(id) - (attendeeId)"
    users |o--o{ VerifiedNumber : "(id) - (userId)"
    Team |o--o{ VerifiedNumber : "(id) - (teamId)"
    users |o--o{ VerifiedEmail : "(id) - (userId)"
    Team |o--o{ VerifiedEmail : "(id) - (teamId)"
    users ||--o{ UserFeatures : "(id) - (userId)"
    Feature ||--o{ UserFeatures : "(slug) - (featureId)"
    Team ||--o{ TeamFeatures : "(id) - (teamId)"
    Feature ||--o{ TeamFeatures : "(slug) - (featureId)"
    users |o--o{ OAuthClient : "(id) - (userId)"
    OAuthClient |o--o{ AccessCode : "(clientId) - (clientId)"
    users |o--o{ AccessCode : "(id) - (userId)"
    Team |o--o{ AccessCode : "(id) - (teamId)"
    Credential |o--o{ CalendarCache : "(id) - (credentialId)"
    users ||--o{ OutOfOfficeEntry : "(id) - (userId)"
    users |o--o{ OutOfOfficeEntry : "(id) - (toUserId)"
    OutOfOfficeReason |o--o{ OutOfOfficeEntry : "(id) - (reasonId)"
    users |o--o{ OutOfOfficeReason : "(id) - (userId)"
    users ||--o| UserHolidaySettings : "(id) - (userId)"
    Team ||--o{ PlatformOAuthClient : "(id) - (organizationId)"
    users ||--o{ PlatformAuthorizationToken : "(id) - (userId)"
    PlatformOAuthClient ||--o{ PlatformAuthorizationToken : "(id) - (platformOAuthClientId)"
    users ||--o{ AccessToken : "(id) - (userId)"
    PlatformOAuthClient ||--o{ AccessToken : "(id) - (platformOAuthClientId)"
    users ||--o{ RefreshToken : "(id) - (userId)"
    PlatformOAuthClient ||--o{ RefreshToken : "(id) - (platformOAuthClientId)"
    OrganizationSettings |o--o| DSyncData : "(organizationId) - (organizationId)"
    Team ||--o{ DSyncTeamGroupMapping : "(id) - (teamId)"
    DSyncData ||--o{ DSyncTeamGroupMapping : "(directoryId) - (directoryId)"
    users ||--o{ SecondaryEmail : "(id) - (userId)"
    Team ||--o| ManagedOrganization : "(id) - (managedOrganizationId)"
    Team ||--o{ ManagedOrganization : "(id) - (managerOrganizationId)"
    PlatformBilling |o--o{ PlatformBilling : "(id) - (managerBillingId)"
    Team ||--o| PlatformBilling : "(id) - (id)"
    Attribute ||--o{ AttributeOption : "(id) - (attributeId)"
    Team ||--o{ Attribute : "(id) - (teamId)"
    Membership ||--o{ AttributeToUser : "(id) - (memberId)"
    AttributeOption ||--o{ AttributeToUser : "(id) - (attributeOptionId)"
    users |o--o{ AttributeToUser : "(id) - (createdById)"
    DSyncData |o--o{ AttributeToUser : "(directoryId) - (createdByDSyncId)"
    users |o--o{ AttributeToUser : "(id) - (updatedById)"
    DSyncData |o--o{ AttributeToUser : "(directoryId) - (updatedByDSyncId)"
    Booking ||--o{ AssignmentReason : "(id) - (bookingId)"
    WorkspacePlatform ||--o{ DelegationCredential : "(id) - (workspacePlatformId)"
    Team ||--o{ DelegationCredential : "(id) - (organizationId)"
    EventType ||--o{ EventTypeTranslation : "(id) - (eventTypeId)"
    users ||--o{ EventTypeTranslation : "(id) - (createdBy)"
    users |o--o{ EventTypeTranslation : "(id) - (updatedBy)"
    Watchlist |o--o{ WatchlistAudit : "(id) - (watchlistId)"
    Booking ||--o| BookingReport : "(uid) - (bookingUid)"
    users |o--o{ BookingReport : "(id) - (reportedById)"
    Team |o--o{ BookingReport : "(id) - (organizationId)"
    Watchlist |o--o{ BookingReport : "(id) - (watchlistId)"
    Watchlist |o--o{ BookingReport : "(id) - (globalWatchlistId)"
    Booking ||--o{ WrongAssignmentReport : "(uid) - (bookingUid)"
    users |o--o{ WrongAssignmentReport : "(id) - (reportedById)"
    Team |o--o{ WrongAssignmentReport : "(id) - (teamId)"
    users |o--o{ WrongAssignmentReport : "(id) - (reviewedById)"
    users ||--o{ OrganizationOnboarding : "(id) - (createdById)"
    Team |o--o| OrganizationOnboarding : "(id) - (organizationId)"
    Team ||--o{ InternalNotePreset : "(id) - (teamId)"
    users ||--o{ FilterSegment : "(id) - (userId)"
    Team |o--o{ FilterSegment : "(id) - (teamId)"
    users ||--o{ UserFilterSegmentPreference : "(id) - (userId)"
    FilterSegment |o--o{ UserFilterSegmentPreference : "(id) - (segmentId)"
    InternalNotePreset |o--o{ BookingInternalNote : "(id) - (notePresetId)"
    Booking ||--o{ BookingInternalNote : "(id) - (bookingId)"
    users ||--o{ BookingInternalNote : "(id) - (createdById)"
    Team |o--o{ Role : "(id) - (teamId)"
    Role ||--o{ RolePermission : "(id) - (roleId)"
    AuditActor ||--o{ BookingAudit : "(id) - (actorId)"
    users |o--o{ Agent : "(id) - (userId)"
    Team |o--o{ Agent : "(id) - (teamId)"
    users |o--o{ CalAiPhoneNumber : "(id) - (userId)"
    Team |o--o{ CalAiPhoneNumber : "(id) - (teamId)"
    Agent |o--o{ CalAiPhoneNumber : "(id) - (inboundAgentId)"
    Agent |o--o{ CalAiPhoneNumber : "(id) - (outboundAgentId)"
    Team |o--o| TeamBilling : "(id) - (teamId)"
    Team ||--o| OrganizationBilling : "(id) - (teamId)"
    Team ||--o{ SeatChangeLog : "(id) - (teamId)"
    MonthlyProration |o--o{ SeatChangeLog : "(id) - (processedInProrationId)"
    TeamBilling |o--o{ SeatChangeLog : "(id) - (teamBillingId)"
    OrganizationBilling |o--o{ SeatChangeLog : "(id) - (organizationBillingId)"
    Team ||--o{ MonthlyProration : "(id) - (teamId)"
    TeamBilling |o--o{ MonthlyProration : "(id) - (teamBillingId)"
    OrganizationBilling |o--o{ MonthlyProration : "(id) - (organizationBillingId)"
    SelectedCalendar ||--o{ CalendarCacheEvent : "(id) - (selectedCalendarId)"
    Team ||--o{ IntegrationAttributeSync : "(id) - (organizationId)"
    Credential |o--o{ IntegrationAttributeSync : "(id) - (credentialId)"
    IntegrationAttributeSync ||--o| AttributeSyncRule : "(id) - (integrationAttributeSyncId)"
    Attribute ||--o{ AttributeSyncFieldMapping : "(id) - (attributeId)"
    IntegrationAttributeSync ||--o{ AttributeSyncFieldMapping : "(id) - (integrationAttributeSyncId)"
    EventType }o--o{ users : "(users) - (eventTypes)"
    PlatformOAuthClient }o--o{ users : "(users) - (platformOAuthClients)"
    Host {
        int userId PK, FK
        int eventTypeId PK, FK
        boolean isFixed
        int priority
        int weight
        int weightAdjustment
        int scheduleId FK
        datetime createdAt
        string groupId FK
        int memberId FK
    }
    HostGroup {
        string id PK
        string name
        int eventTypeId FK
        datetime createdAt
        datetime updatedAt
    }
    HostLocation {
        string id PK
        int userId FK, UK
        int eventTypeId FK, UK
        string type
        int credentialId FK
        string link
        string address
        string phoneNumber
        datetime createdAt
        datetime updatedAt
    }
    CalVideoSettings {
        int eventTypeId PK, FK
        boolean disableRecordingForOrganizer
        boolean disableRecordingForGuests
        boolean enableAutomaticTranscription
        boolean enableAutomaticRecordingForOrganizer
        string redirectUrlOnExit
        boolean disableTranscriptionForGuests
        boolean disableTranscriptionForOrganizer
        boolean requireEmailForGuests
        datetime createdAt
        datetime updatedAt
    }
    VideoCallGuest {
        string id PK
        string bookingUid UK
        string email UK
        string name
        datetime joinedAt
        datetime createdAt
        datetime updatedAt
    }
    EventType {
        int id PK
        string title "@zod.string.min(1)"
        string slug UK "@zod.import([#quot;import { eventTypeSlug } from '../../zod-utils'#quot;]).custom.use(eventTypeSlug)"
        string description
        string interfaceLanguage
        int position
        json locations "@zod.import([#quot;import { eventTypeLocations } from '../../zod-utils'#quot;]).custom.use(eventTypeLocations)"
        int length "@zod.number.min(1)"
        int offsetStart
        boolean hidden
        int userId FK, UK
        int profileId FK
        int teamId FK, UK
        boolean useEventLevelSelectedCalendars
        string eventName
        int parentId FK, UK
        json bookingFields "@zod.import([#quot;import { eventTypeBookingFields } from '../../zod-utils'#quot;]).custom.use(eventTypeBookingFields)"
        string timeZone
        periodtype periodType
        datetime periodStartDate "@zod.import([#quot;import { coerceToDate } from '../../zod-utils'#quot;]).custom.use(coerceToDate)"
        datetime periodEndDate "@zod.import([#quot;import { coerceToDate } from '../../zod-utils'#quot;]).custom.use(coerceToDate)"
        int periodDays
        boolean periodCountCalendarDays
        boolean lockTimeZoneToggleOnBookingPage
        string lockedTimeZone
        boolean requiresConfirmation
        boolean requiresConfirmationWillBlockSlot
        boolean requiresConfirmationForFreeEmail
        boolean requiresBookerEmailVerification
        boolean canSendCalVideoTranscriptionEmails
        boolean autoTranslateDescriptionEnabled
        boolean autoTranslateInstantMeetingTitleEnabled
        json recurringEvent "@zod.import([#quot;import { recurringEventType } from '../../zod-utils'#quot;]).custom.use(recurringEventType)"
        boolean disableGuests
        boolean hideCalendarNotes
        boolean hideCalendarEventDetails
        int minimumBookingNotice "@zod.number.min(0)"
        int beforeEventBuffer
        int afterEventBuffer
        int seatsPerTimeSlot
        boolean onlyShowFirstAvailableSlot
        boolean showOptimizedSlots
        boolean disableCancelling
        boolean disableRescheduling
        int minimumRescheduleNotice "@zod.number.min(0)"
        boolean seatsShowAttendees
        boolean seatsShowAvailabilityCount
        schedulingtype schedulingType
        int scheduleId FK
        boolean allowReschedulingCancelledBookings
        int price
        string currency
        int slotInterval
        json metadata "@zod.import([#quot;import { EventTypeMetaDataSchema } from '../../zod-utils'#quot;]).custom.use(EventTypeMetaDataSchema)"
        string successRedirectUrl "@zod.import([#quot;import { successRedirectUrl } from '../../zod-utils'#quot;]).custom.use(successRedirectUrl)"
        boolean forwardParamsSuccessRedirect
        json bookingLimits "@zod.import([#quot;import { intervalLimitsType } from '../../zod-utils'#quot;]).custom.use(intervalLimitsType)"
        json durationLimits "@zod.import([#quot;import { intervalLimitsType } from '../../zod-utils'#quot;]).custom.use(intervalLimitsType)"
        boolean isInstantEvent
        int instantMeetingExpiryTimeOffsetInSeconds
        int instantMeetingScheduleId FK
        string[] instantMeetingParameters
        boolean assignAllTeamMembers
        boolean assignRRMembersUsingSegment
        json rrSegmentQueryValue "@zod.import([#quot;import { rrSegmentQueryValueSchema } from '../../zod-utils'#quot;]).custom.use(rrSegmentQueryValueSchema)"
        boolean useEventTypeDestinationCalendarEmail
        boolean isRRWeightsEnabled
        int maxLeadThreshold
        boolean includeNoShowInRRCalculation
        boolean allowReschedulingPastBookings
        boolean hideOrganizerEmail
        int maxActiveBookingsPerBooker
        boolean maxActiveBookingPerBookerOfferReschedule
        string customReplyToEmail "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        json eventTypeColor "@zod.import([#quot;import { eventTypeColor } from '../../zod-utils'#quot;]).custom.use(eventTypeColor)"
        boolean rescheduleWithSameRoundRobinHost
        int secondaryEmailId FK
        boolean useBookerTimezone
        int restrictionScheduleId FK
        boolean bookingRequiresAuthentication
        boolean rrHostSubsetEnabled
        cancellationreasonrequirement requiresCancellationReason
        boolean enablePerHostLocations
        datetime createdAt
        datetime updatedAt
    }
    Credential {
        int id PK
        string type
        json key
        string encryptedKey
        int userId FK
        int teamId FK
        string appId FK
        string subscriptionId
        string paymentStatus
        int billingCycleStart
        boolean invalid
        string delegationCredentialId FK
    }
    DestinationCalendar {
        int id PK
        string integration
        string externalId
        string primaryEmail "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        int userId FK, UK
        int eventTypeId FK, UK
        int credentialId FK
        datetime createdAt
        datetime updatedAt
        string delegationCredentialId FK
        int customCalendarReminder
    }
    UserPassword {
        string hash
        int userId FK, UK
    }
    TravelSchedule {
        int id PK
        int userId FK
        string timeZone
        datetime startDate
        datetime endDate
        string prevTimeZone
    }
    users {
        int id PK
        string uuid UK
        string username UK
        string name
        string email UK "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        datetime emailVerified
        string bio
        string avatarUrl
        string timeZone
        string weekStart
        int bufferTime
        boolean hideBranding
        string theme
        string appTheme
        datetime createdDate
        datetime trialEndsAt
        datetime lastActiveAt
        int defaultScheduleId
        boolean completedOnboarding
        string locale
        int timeFormat
        string twoFactorSecret
        boolean twoFactorEnabled
        string backupCodes
        identityprovider identityProvider
        string identityProviderId
        int invitedTo
        string brandColor
        string darkBrandColor
        boolean allowDynamicBooking
        boolean allowSEOIndexing
        boolean receiveMonthlyDigestEmail
        boolean requiresBookerEmailVerification
        json metadata "@zod.import([#quot;import { userMetadata } from '../../zod-utils'#quot;]).custom.use(userMetadata)"
        boolean verified
        userpermissionrole role
        int organizationId FK, UK
        boolean locked
        int movedToProfileId FK, UK
        boolean isPlatformManaged
        smslockstate smsLockState
        boolean smsLockReviewedByAdmin
        string referralLinkId
        creationsource creationSource
        boolean autoOptInFeatures
    }
    NotificationsSubscriptions {
        int id PK
        int userId FK
        string subscription
    }
    Profile {
        int id PK
        string uid
        int userId FK, UK
        int organizationId FK, UK
        string username UK
        datetime createdAt
        datetime updatedAt
    }
    Team {
        int id PK
        string name "@zod.string.min(1)"
        string slug UK "@zod.string.min(1)"
        string logoUrl
        string calVideoLogo
        string appLogo
        string appIconLogo
        string bio
        boolean hideBranding
        boolean hideTeamProfileLink
        boolean isPrivate
        boolean hideBookATeamMember
        datetime createdAt
        json metadata "@zod.import([#quot;import { teamMetadataSchema } from '../../zod-utils'#quot;]).custom.use(teamMetadataSchema)"
        string theme
        rrresetinterval rrResetInterval
        rrtimestampbasis rrTimestampBasis
        string brandColor
        string darkBrandColor
        string bannerUrl
        int parentId FK, UK
        int timeFormat
        string timeZone
        string weekStart
        boolean isOrganization
        boolean pendingPayment
        boolean isPlatform
        string createdByOAuthClientId FK
        smslockstate smsLockState
        boolean smsLockReviewedByAdmin
        json bookingLimits "@zod.import([#quot;import { intervalLimitsType } from '../../zod-utils'#quot;]).custom.use(intervalLimitsType)"
        boolean includeManagedEventsInLimits
        boolean autoOptInFeatures
    }
    CreditBalance {
        string id PK
        int teamId FK, UK
        int userId FK, UK
        int additionalCredits
        datetime limitReachedAt
        datetime warningSentAt
    }
    CreditPurchaseLog {
        string id PK
        string creditBalanceId FK
        int credits
        datetime createdAt
    }
    CreditExpenseLog {
        string id PK
        string creditBalanceId FK
        string bookingUid FK
        int credits
        credittype creditType
        datetime date
        string smsSid
        int smsSegments
        string phoneNumber
        string email
        int callDuration
        creditusagetype creditFor
        string externalRef UK
    }
    OrganizationSettings {
        int id PK
        int organizationId FK, UK
        boolean isOrganizationConfigured
        boolean isOrganizationVerified
        string orgAutoAcceptEmail
        boolean lockEventTypeCreationForUsers
        boolean adminGetsNoSlotsNotification
        boolean isAdminReviewed
        boolean isAdminAPIEnabled
        boolean allowSEOIndexing
        boolean orgProfileRedirectsToVerifiedDomain
        boolean disablePhoneOnlySMSNotifications
        boolean disableAutofillOnBookingPage
        boolean orgAutoJoinOnSignup
        boolean disableAttendeeConfirmationEmail
        boolean disableAttendeeCancellationEmail
        boolean disableAttendeeRescheduledEmail
        boolean disableAttendeeRequestEmail
        boolean disableAttendeeReassignedEmail
        boolean disableAttendeeAwaitingPaymentEmail
        boolean disableAttendeeRescheduleRequestEmail
        boolean disableAttendeeLocationChangeEmail
        boolean disableAttendeeNewEventEmail
    }
    Membership {
        int id PK
        int teamId FK, UK
        int userId FK, UK
        boolean accepted
        membershiprole role
        string customRoleId FK
        datetime createdAt
        datetime updatedAt
    }
    VerificationToken {
        int id PK
        string identifier UK
        string token UK
        datetime expires
        int expiresInDays
        datetime createdAt
        datetime updatedAt
        int teamId FK
        int secondaryEmailId FK
    }
    InstantMeetingToken {
        int id PK
        string token UK
        datetime expires
        int teamId FK
        int bookingId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    BookingReference {
        int id PK
        string type "@zod.string.min(1)"
        string uid "@zod.string.min(1)"
        string meetingId
        string thirdPartyRecurringEventId
        string meetingPassword
        string meetingUrl
        int bookingId FK
        string externalCalendarId
        boolean deleted
        int credentialId FK
        string delegationCredentialId FK
    }
    Attendee {
        int id PK
        string email
        string name
        string timeZone
        string phoneNumber
        string locale
        int bookingId FK
        boolean noShow
    }
    Booking {
        int id PK
        string uid UK
        string idempotencyKey UK
        int userId FK
        string userPrimaryEmail "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        int eventTypeId FK
        string title
        string description
        json customInputs
        json responses "@zod.import([#quot;import { bookingResponses } from '../../zod-utils'#quot;]).custom.use(bookingResponses)"
        datetime startTime
        datetime endTime
        string location
        datetime createdAt
        datetime updatedAt
        bookingstatus status
        boolean paid
        int destinationCalendarId FK
        string cancellationReason
        string rejectionReason
        string reassignReason
        int reassignById FK
        string dynamicEventSlugRef
        string dynamicGroupSlugRef
        boolean rescheduled
        string fromReschedule
        string recurringEventId
        string smsReminderNumber
        string[] scheduledJobs
        json metadata "@zod.import([#quot;import { bookingMetadataSchema } from '../../zod-utils'#quot;]).custom.use(bookingMetadataSchema)"
        boolean isRecorded
        string iCalUID
        int iCalSequence
        int rating
        string ratingFeedback
        boolean noShowHost
        string oneTimePassword UK
        string cancelledBy "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        string rescheduledBy "@zod.import([#quot;import { emailSchema } from '../../zod-utils'#quot;]).custom.use(emailSchema)"
        creationsource creationSource
    }
    Tracking {
        int id PK
        int bookingId FK, UK
        string utm_source
        string utm_medium
        string utm_campaign
        string utm_term
        string utm_content
    }
    Schedule {
        int id PK
        int userId FK
        string name
        string timeZone
    }
    Availability {
        int id PK
        int userId FK
        int eventTypeId FK
        int[] days
        datetime startTime
        datetime endTime
        datetime date
        int scheduleId FK
    }
    SelectedCalendar {
        string id PK
        int userId FK, UK
        string integration UK
        string externalId UK
        int credentialId FK
        datetime createdAt
        datetime updatedAt
        string googleChannelId UK
        string googleChannelKind
        string googleChannelResourceId
        string googleChannelResourceUri
        string googleChannelExpiration
        string channelId
        string channelKind
        string channelResourceId
        string channelResourceUri
        datetime channelExpiration
        datetime syncSubscribedAt
        datetime syncSubscribedErrorAt
        int syncSubscribedErrorCount
        string syncToken
        datetime syncedAt
        datetime syncErrorAt
        int syncErrorCount
        string delegationCredentialId FK
        string error
        datetime lastErrorAt
        int watchAttempts
        int unwatchAttempts
        int maxAttempts
        int eventTypeId FK, UK
    }
    EventTypeCustomInput {
        int id PK
        int eventTypeId FK
        string label
        eventtypecustominputtype type
        json options "@zod.import([#quot;import { customInputOptionSchema } from '../../zod-utils'#quot;]).custom.use(customInputOptionSchema)"
        boolean required
        string placeholder
    }
    ResetPasswordRequest {
        string id PK
        datetime createdAt
        datetime updatedAt
        string email
        datetime expires
    }
    ReminderMail {
        int id PK
        int referenceId
        remindertype reminderType
        int elapsedMinutes
        datetime createdAt
    }
    Payment {
        int id PK
        string uid UK
        string appId FK
        int bookingId FK
        int amount
        int fee
        string currency
        boolean success
        boolean refunded
        json data
        string externalId UK
        paymentoption paymentOption
    }
    Webhook {
        string id PK
        int userId FK, UK
        int teamId FK
        int eventTypeId FK
        string platformOAuthClientId FK, UK
        string subscriberUrl UK "@zod.string.url()"
        string payloadTemplate
        datetime createdAt
        boolean active
        webhooktriggerevents[] eventTriggers
        string appId FK
        string secret
        boolean platform
        int time
        timeunit timeUnit
        string version
    }
    ApiKey {
        string id PK
        int userId FK
        int teamId FK
        string note
        datetime createdAt
        datetime expiresAt
        datetime lastUsedAt
        string hashedKey UK
        string appId FK
    }
    RateLimit {
        string id PK
        string name
        string apiKeyId FK
        int ttl
        int limit
        int blockDuration
        datetime createdAt
        datetime updatedAt
    }
    HashedLink {
        int id PK
        string link UK
        int eventTypeId FK
        datetime expiresAt
        int maxUsageCount
        int usageCount
    }
    Account {
        string id PK
        int userId FK
        string type
        string provider UK
        string providerAccountId UK
        string providerEmail
        string refresh_token
        string access_token
        int expires_at
        string token_type
        string scope
        string id_token
        string session_state
    }
    Session {
        string id PK
        string sessionToken UK
        int userId FK
        datetime expires
    }
    App {
        string slug PK
        string dirName UK
        json keys
        appcategories[] categories
        datetime createdAt
        datetime updatedAt
        boolean enabled
    }
    Feedback {
        int id PK
        datetime date
        int userId FK
        string rating
        string comment
    }
    Deployment {
        int id PK "This is a single row table, so we use a fixed id"
        string logo
        json theme "@zod.import([#quot;import { DeploymentTheme } from '../../zod-utils'#quot;]).custom.use(DeploymentTheme)"
        string licenseKey
        string signatureTokenEncrypted
        datetime agreedLicenseAt
    }
    WebhookScheduledTriggers {
        int id PK
        string jobName
        string subscriberUrl
        string payload
        datetime startAfter
        int retryCount
        datetime createdAt
        string appId
        string webhookId FK
        int bookingId FK
    }
    BookingSeat {
        int id PK
        string referenceUid UK
        int bookingId FK
        int attendeeId FK, UK
        json data "@zod.import([#quot;import { bookingSeatDataSchema } from '../../zod-utils'#quot;]).custom.use(bookingSeatDataSchema)"
        json metadata
    }
    VerifiedNumber {
        int id PK
        int userId FK
        int teamId FK
        string phoneNumber
    }
    VerifiedEmail {
        int id PK
        int userId FK
        int teamId FK
        string email
    }
    Feature {
        string slug PK
        boolean enabled
        string description
        featuretype type
        boolean stale
        datetime lastUsedAt
        datetime createdAt
        datetime updatedAt
        int updatedBy
    }
    UserFeatures {
        int userId PK, FK
        string featureId PK, FK
        boolean enabled
        datetime assignedAt
        string assignedBy
        datetime updatedAt
    }
    TeamFeatures {
        int teamId PK, FK
        string featureId PK, FK
        boolean enabled
        datetime assignedAt
        string assignedBy
        datetime updatedAt
    }
    SelectedSlots {
        int id PK
        int eventTypeId
        int userId UK
        datetime slotUtcStartDate UK
        datetime slotUtcEndDate UK
        string uid UK
        datetime releaseAt
        boolean isSeat
    }
    OAuthClient {
        string clientId PK
        string redirectUri
        string clientSecret
        oauthclienttype clientType
        string name
        string purpose
        string logo
        string websiteUrl
        string rejectionReason
        boolean isTrusted
        oauthclientstatus status
        int userId FK
        datetime createdAt
    }
    AccessCode {
        int id PK
        string code
        string clientId FK
        datetime expiresAt
        accessscope[] scopes
        int userId FK
        int teamId FK
        string codeChallenge
        string codeChallengeMethod
    }
    BookingDenormalized {
        int id PK
        string uid
        int eventTypeId
        string title
        string description
        datetime startTime
        datetime endTime
        datetime createdAt
        datetime updatedAt
        string location
        boolean paid
        bookingstatus status
        boolean rescheduled
        int userId
        int teamId
        int eventLength
        int eventParentId
        string userEmail
        string userName
        string userUsername
        string ratingFeedback
        int rating
        boolean noShowHost
        boolean isTeamBooking
    }
    CalendarCache {
        string id
        string key PK, UK
        json value
        datetime expiresAt
        datetime updatedAt
        int credentialId PK, FK, UK
        int userId
    }
    TempOrgRedirect {
        int id PK
        string from UK
        int fromOrgId UK
        redirecttype type UK
        string toUrl
        boolean enabled
        datetime createdAt
        datetime updatedAt
    }
    avatars {
        int teamId UK
        int userId UK
        string data
        string objectKey UK
        boolean isBanner UK
    }
    OutOfOfficeEntry {
        int id PK
        string uuid UK
        datetime start
        datetime end
        string notes
        boolean showNotePublicly
        int userId FK
        int toUserId FK
        int reasonId FK
        datetime createdAt
        datetime updatedAt
    }
    OutOfOfficeReason {
        int id PK
        string emoji
        string reason UK
        boolean enabled
        int userId FK
    }
    UserHolidaySettings {
        int id PK
        int userId FK, UK
        string countryCode
        string[] disabledIds
        datetime createdAt
        datetime updatedAt
    }
    HolidayCache {
        string id PK
        string countryCode UK
        string calendarId
        string eventId UK
        string name
        datetime date
        int year
        datetime createdAt
        datetime updatedAt
    }
    PlatformOAuthClient {
        string id PK
        string name
        string secret
        int permissions
        string logo
        string[] redirectUris
        int organizationId FK
        string bookingRedirectUri
        string bookingCancelRedirectUri
        string bookingRescheduleRedirectUri
        boolean areEmailsEnabled
        boolean areDefaultEventTypesEnabled
        boolean areCalendarEventsEnabled
        datetime createdAt
    }
    PlatformAuthorizationToken {
        string id PK
        string platformOAuthClientId FK, UK
        int userId FK, UK
        datetime createdAt
    }
    AccessToken {
        int id PK
        string secret UK
        datetime createdAt
        datetime expiresAt
        string platformOAuthClientId FK
        int userId FK
    }
    RefreshToken {
        int id PK
        string secret UK
        datetime createdAt
        datetime expiresAt
        string platformOAuthClientId FK
        int userId FK
    }
    DSyncData {
        int id PK
        string directoryId UK
        string tenant
        int organizationId FK, UK
    }
    DSyncTeamGroupMapping {
        int id PK
        int organizationId
        int teamId FK, UK
        string directoryId FK
        string groupName UK
    }
    SecondaryEmail {
        int id PK
        int userId FK, UK
        string email UK
        datetime emailVerified
    }
    Task {
        string id PK
        datetime createdAt
        datetime updatedAt
        datetime scheduledAt
        datetime succeededAt
        string type UK
        string payload
        int attempts
        int maxAttempts
        string lastError
        datetime lastFailedAttemptAt
        string referenceUid UK
    }
    ManagedOrganization {
        int managedOrganizationId FK, UK
        int managerOrganizationId FK, UK
        datetime createdAt
    }
    PlatformBilling {
        int id PK, FK
        string customerId
        string subscriptionId
        string priceId
        string plan
        int billingCycleStart
        int billingCycleEnd
        boolean overdue
        int managerBillingId FK
    }
    AttributeOption {
        string id PK
        string attributeId FK
        string value
        string slug
        boolean isGroup
        string[] contains
    }
    Attribute {
        string id PK
        int teamId FK, UK
        attributetype type
        string name
        string slug UK
        boolean enabled
        boolean usersCanEditRelation
        datetime createdAt
        datetime updatedAt
        boolean isWeightsEnabled
        boolean isLocked
    }
    AttributeToUser {
        string id PK
        int memberId FK, UK
        string attributeOptionId FK, UK
        int weight
        datetime createdAt
        int createdById FK
        string createdByDSyncId FK
        datetime updatedAt
        int updatedById FK
        string updatedByDSyncId FK
    }
    AssignmentReason {
        int id PK
        datetime createdAt
        int bookingId FK
        assignmentreasonenum reasonEnum
        string reasonString
    }
    DelegationCredential {
        string id PK
        int workspacePlatformId FK
        json serviceAccountKey "@zod.import([#quot;import { serviceAccountKeySchema } from '../../zod-utils'#quot;]).custom.use(serviceAccountKeySchema)"
        boolean enabled
        datetime lastEnabledAt
        datetime lastDisabledAt
        int organizationId FK, UK
        string domain UK
        datetime createdAt
        datetime updatedAt
    }
    WorkspacePlatform {
        int id PK
        string slug UK "@zod.string.min(1)"
        string name "@zod.string.min(1)"
        string description
        json defaultServiceAccountKey "@zod.import([#quot;import { serviceAccountKeySchema } from '../../zod-utils'#quot;]).custom.use(serviceAccountKeySchema)"
        datetime createdAt
        datetime updatedAt
        boolean enabled
    }
    EventTypeTranslation {
        string uid PK
        int eventTypeId FK, UK
        eventtypeautotranslatedfield field UK
        string sourceLocale
        string targetLocale UK
        string translatedText
        datetime createdAt
        int createdBy FK
        datetime updatedAt
        int updatedBy FK
    }
    Watchlist {
        string id PK
        watchlisttype type UK
        string value UK
        string description
        boolean isGlobal
        int organizationId UK
        watchlistaction action
        watchlistsource source
        datetime lastUpdatedAt
    }
    WatchlistAudit {
        string id PK
        watchlisttype type
        string value
        string description
        watchlistaction action
        datetime changedAt
        int changedByUserId
        string watchlistId FK
    }
    WatchlistEventAudit {
        string id PK
        string watchlistId
        int eventTypeId
        watchlistaction actionTaken
        datetime timestamp
    }
    BookingReport {
        string id PK
        string bookingUid FK, UK
        string bookerEmail
        int reportedById FK
        int organizationId FK
        bookingreportreason reason
        string description
        boolean cancelled
        datetime createdAt
        datetime updatedAt
        bookingreportstatus status
        systemreportstatus systemStatus
        string watchlistId FK
        string globalWatchlistId FK
    }
    WrongAssignmentReport {
        string id PK
        string bookingUid FK, UK
        int reportedById FK
        string correctAssignee
        string additionalNotes
        int teamId FK
        wrongassignmentreportstatus status
        int reviewedById FK
        datetime reviewedAt
        datetime createdAt
        datetime updatedAt
    }
    OrganizationOnboarding {
        string id PK
        int createdById FK
        datetime createdAt
        string orgOwnerEmail UK
        string error
        datetime updatedAt
        int organizationId FK, UK
        billingperiod billingPeriod
        float pricePerSeat
        int seats
        boolean isPlatform
        string name
        string slug
        string logo
        string bio
        string brandColor
        string bannerUrl
        boolean isDomainConfigured
        string stripeCustomerId UK
        string stripeSubscriptionId
        string stripeSubscriptionItemId
        json invitedMembers "@zod.import([#quot;import { orgOnboardingInvitedMembersSchema } from '../../zod-utils'#quot;]).custom.use(orgOnboardingInvitedMembersSchema)"
        json teams "@zod.import([#quot;import { orgOnboardingTeamsSchema } from '../../zod-utils'#quot;]).custom.use(orgOnboardingTeamsSchema)"
        boolean isComplete
    }
    InternalNotePreset {
        int id PK
        string name UK
        string cancellationReason
        int teamId FK, UK
        datetime createdAt
    }
    FilterSegment {
        int id PK
        string name
        string tableIdentifier
        filtersegmentscope scope
        json activeFilters
        json sorting
        json columnVisibility
        json columnSizing
        int perPage
        string searchTerm
        datetime createdAt
        datetime updatedAt
        int userId FK
        int teamId FK
    }
    UserFilterSegmentPreference {
        int id PK
        int userId FK, UK
        string tableIdentifier UK
        int segmentId FK
        string systemSegmentId
        datetime createdAt
        datetime updatedAt
    }
    BookingInternalNote {
        int id PK
        int notePresetId FK, UK
        string text
        int bookingId FK, UK
        int createdById FK
        datetime createdAt
    }
    Role {
        string id PK
        string name UK
        string color
        string description
        int teamId FK, UK
        datetime createdAt
        datetime updatedAt
        roletype type
    }
    RolePermission {
        string id PK
        string roleId FK, UK
        string resource UK
        string action UK
        datetime createdAt
    }
    AuditActor {
        string id PK
        auditactortype type
        string userUuid UK
        int attendeeId UK
        int credentialId UK
        string email UK
        string phone UK
        string name
        datetime createdAt
    }
    BookingAudit {
        string id PK
        string bookingUid
        string actorId FK
        bookingaudittype type
        bookingauditaction action
        datetime timestamp
        datetime createdAt
        datetime updatedAt
        bookingauditsource source
        string operationId
        json data
        json context
    }
    Agent {
        string id PK
        string name
        int userId FK
        int teamId FK
        string providerAgentId UK
        int inboundEventTypeId
        int outboundEventTypeId
        boolean enabled
        datetime createdAt
        datetime updatedAt
    }
    CalAiPhoneNumber {
        int id PK
        int userId FK
        int teamId FK
        string phoneNumber UK
        string provider
        string providerPhoneNumberId UK
        datetime createdAt
        datetime updatedAt
        string stripeCustomerId
        string stripeSubscriptionId UK
        phonenumbersubscriptionstatus subscriptionStatus
        string inboundAgentId FK
        string outboundAgentId FK
    }
    TeamBilling {
        string id PK
        int teamId FK, UK
        string subscriptionId UK
        string subscriptionItemId
        string customerId
        string status
        string planName
        datetime subscriptionStart
        datetime subscriptionTrialEnd
        datetime subscriptionEnd
        billingperiod billingPeriod
        billingmode billingMode
        int pricePerSeat
        int paidSeats
        int highWaterMark
        datetime highWaterMarkPeriodStart
        datetime createdAt
        datetime updatedAt
    }
    OrganizationBilling {
        string id PK
        int teamId FK, UK
        string subscriptionId UK
        string subscriptionItemId
        string customerId
        string status
        string planName
        datetime subscriptionStart
        datetime subscriptionTrialEnd
        datetime subscriptionEnd
        billingperiod billingPeriod
        billingmode billingMode
        int pricePerSeat
        int paidSeats
        int highWaterMark
        datetime highWaterMarkPeriodStart
        datetime createdAt
        datetime updatedAt
    }
    SeatChangeLog {
        string id PK
        int teamId FK, UK
        seatchangetype changeType
        int seatCount
        int userId
        int triggeredBy
        datetime changeDate
        string monthKey
        string operationId UK
        string processedInProrationId FK
        json metadata
        datetime createdAt
        string teamBillingId FK
        string organizationBillingId FK
    }
    MonthlyProration {
        string id PK
        int teamId FK, UK
        string monthKey UK
        datetime periodStart
        datetime periodEnd
        int seatsAtStart
        int seatsAdded
        int seatsRemoved
        int netSeatIncrease
        int seatsAtEnd
        string subscriptionId
        string subscriptionItemId
        string customerId
        datetime subscriptionStart
        datetime subscriptionEnd
        int remainingDays
        int pricePerSeat
        int proratedAmount
        string invoiceItemId
        string invoiceId
        string invoiceUrl
        prorationstatus status
        datetime chargedAt
        datetime failedAt
        string failureReason
        int retryCount
        json metadata
        datetime createdAt
        datetime updatedAt
        string teamBillingId FK
        string organizationBillingId FK
    }
    CalendarCacheEvent {
        string id PK
        string selectedCalendarId FK, UK
        string externalId UK
        string externalEtag
        string iCalUID
        int iCalSequence
        string summary
        string description
        string location
        datetime start
        datetime end
        boolean isAllDay
        string timeZone
        calendarcacheeventstatus status
        string recurringEventId
        datetime originalStartTime
        datetime createdAt
        datetime updatedAt
        datetime externalCreatedAt
        datetime externalUpdatedAt
    }
    IntegrationAttributeSync {
        string id PK
        int organizationId FK
        string name
        string integration
        int credentialId FK
        boolean enabled
        datetime createdAt
        datetime updatedAt
    }
    AttributeSyncRule {
        string id PK
        string integrationAttributeSyncId FK, UK
        json rule
        datetime createdAt
        datetime updatedAt
    }
    AttributeSyncFieldMapping {
        string id PK
        string integrationFieldName
        string attributeId FK, UK
        boolean enabled
        string integrationAttributeSyncId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    BookingTimeStatus {
        int id UK
        string uid
        int eventTypeId
        string title
        string description
        datetime startTime
        datetime endTime
        datetime createdAt
        string location
        boolean paid
        bookingstatus status
        boolean rescheduled
        int userId
        int teamId
        int eventLength
        string timeStatus
        int eventParentId
        string userEmail
        string username
        string ratingFeedback
        int rating
        boolean noShowHost
        boolean isTeamBooking
    }
    BookingTimeStatusDenormalized {
        int id UK
        string uid
        int eventTypeId
        string title
        string description
        datetime startTime
        datetime endTime
        datetime createdAt
        datetime updatedAt
        string location
        boolean paid
        bookingstatus status
        boolean rescheduled
        int userId
        int teamId
        int eventLength
        int eventParentId
        string userEmail
        string userName
        string userUsername
        string ratingFeedback
        int rating
        boolean noShowHost
        boolean isTeamBooking
        string timeStatus
    }
```