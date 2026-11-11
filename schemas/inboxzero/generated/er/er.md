```mermaid
erDiagram
    User ||--o{ Account : "(id) - (userId)"
    User ||--o{ Session : "(id) - (userId)"
    Organization |o--o{ Session : "(id) - (activeOrganizationId)"
    Premium |o--o{ User : "(id) - (premiumId)"
    Premium |o--o{ User : "(id) - (premiumAdminId)"
    User ||--o{ EmailAccount : "(id) - (userId)"
    Account ||--o| EmailAccount : "(id) - (accountId)"
    EmailAccount ||--o{ MailSplit : "(id) - (emailAccountId)"
    MailSplit ||--o{ MailSplitFilter : "(id) - (mailSplitId)"
    Organization ||--o{ OrganizationRule : "(id) - (organizationId)"
    OrganizationRule ||--o{ OrganizationRuleAction : "(id) - (organizationRuleId)"
    Organization ||--o{ Member : "(id) - (organizationId)"
    EmailAccount ||--o{ Member : "(id) - (emailAccountId)"
    Organization ||--o{ Conversation : "(id) - (organizationId)"
    Member ||--o{ Conversation : "(id) - (publisherId)"
    Conversation ||--o{ ConversationParticipant : "(id) - (conversationId)"
    Member ||--o{ ConversationParticipant : "(id) - (memberId)"
    Conversation ||--o{ ConversationComment : "(id) - (conversationId)"
    Member |o--o{ ConversationComment : "(id) - (authorMemberId)"
    ConversationParticipant ||--o{ ConversationActivity : "(id) - (participantId)"
    ConversationComment |o--o{ ConversationActivity : "(id) - (commentId)"
    Organization ||--o{ invitation : "(id) - (organizationId)"
    EmailAccount ||--o{ invitation : "(id) - (inviterId)"
    User |o--o{ ssoProvider : "(id) - (userId)"
    EmailAccount |o--o{ ssoProvider : "(id) - (emailAccountId)"
    Organization |o--o| ssoProvider : "(id) - (organizationId)"
    EmailAccount ||--o{ Digest : "(id) - (emailAccountId)"
    Digest ||--o{ DigestItem : "(id) - (digestId)"
    ExecutedAction |o--o{ DigestItem : "(id) - (actionId)"
    ColdEmail |o--o{ DigestItem : "(id) - (coldEmailId)"
    EmailAccount ||--o| Schedule : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Label : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Rule : "(id) - (emailAccountId)"
    OrganizationRule |o--o{ Rule : "(id) - (organizationRuleId)"
    Group |o--o| Rule : "(id) - (groupId)"
    Rule ||--o{ Action : "(id) - (ruleId)"
    EmailAccount ||--o{ Action : "(id) - (emailAccountId)"
    MessagingChannel |o--o{ Action : "(id) - (messagingChannelId)"
    Rule ||--o{ RuleHistory : "(id) - (ruleId)"
    Rule |o--o{ ExecutedRule : "(id) - (ruleId)"
    EmailAccount ||--o{ ExecutedRule : "(id) - (emailAccountId)"
    ExecutedRule ||--o{ ExecutedAction : "(id) - (executedRuleId)"
    MessagingChannel |o--o{ ExecutedAction : "(id) - (messagingChannelId)"
    ExecutedRule ||--o{ ScheduledAction : "(id) - (executedRuleId)"
    ExecutedAction |o--o| ScheduledAction : "(id) - (executedActionId)"
    EmailAccount ||--o{ ScheduledAction : "(id) - (emailAccountId)"
    EmailAccount ||--o{ SnoozedThread : "(id) - (emailAccountId)"
    EmailAccount ||--o{ ScheduledEmail : "(id) - (emailAccountId)"
    EmailAccount ||--o{ EmailSendOperation : "(id) - (emailAccountId)"
    EmailAccount ||--o{ SentMessageOpen : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Group : "(id) - (emailAccountId)"
    Group |o--o{ GroupItem : "(id) - (groupId)"
    Rule ||--o{ ClassificationFeedback : "(id) - (ruleId)"
    EmailAccount ||--o{ ClassificationFeedback : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Category : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Newsletter : "(id) - (emailAccountId)"
    Category |o--o{ Newsletter : "(id) - (categoryId)"
    EmailAccount ||--o{ ColdEmail : "(id) - (emailAccountId)"
    EmailAccount ||--o{ EmailMessage : "(id) - (emailAccountId)"
    EmailAccount ||--o{ ResponseTime : "(id) - (emailAccountId)"
    EmailAccount ||--o{ ThreadTracker : "(id) - (emailAccountId)"
    EmailAccount ||--o{ CleanupJob : "(id) - (emailAccountId)"
    CleanupJob ||--o{ CleanupThread : "(id) - (jobId)"
    EmailAccount ||--o{ CleanupThread : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Knowledge : "(id) - (emailAccountId)"
    EmailAccount ||--o{ Snippet : "(id) - (emailAccountId)"
    EmailAccount ||--o{ EmailSignature : "(id) - (emailAccountId)"
    EmailAccount ||--o{ ReplyMemory : "(id) - (emailAccountId)"
    ReplyMemory ||--o{ ReplyMemorySource : "(id) - (replyMemoryId)"
    DraftSendLog ||--o{ ReplyMemorySource : "(id) - (draftSendLogId)"
    User ||--o{ ApiKey : "(id) - (userId)"
    EmailAccount |o--o{ ApiKey : "(id) - (emailAccountId)"
    EmailAccount ||--o{ EmailToken : "(id) - (emailAccountId)"
    Premium |o--o{ Payment : "(id) - (premiumId)"
    ExecutedAction ||--o| DraftSendLog : "(id) - (executedActionId)"
    EmailAccount ||--o{ Chat : "(id) - (emailAccountId)"
    Chat ||--o{ ChatMessage : "(id) - (chatId)"
    Chat ||--o{ ChatCompaction : "(id) - (chatId)"
    Chat ||--o{ ChatActionTotal : "(id) - (chatId)"
    Chat |o--o{ ChatMemory : "(id) - (chatId)"
    EmailAccount ||--o{ ChatMemory : "(id) - (emailAccountId)"
    EmailAccount ||--o{ CalendarConnection : "(id) - (emailAccountId)"
    CalendarConnection ||--o{ Calendar : "(id) - (connectionId)"
    EmailAccount ||--o{ BookingLink : "(id) - (emailAccountId)"
    Calendar |o--o{ BookingLink : "(id) - (destinationCalendarId)"
    AvailabilitySchedule ||--o{ BookingLink : "(id) - (availabilityScheduleId)"
    EmailAccount ||--o{ AvailabilitySchedule : "(id) - (emailAccountId)"
    AvailabilitySchedule ||--o{ AvailabilityWindow : "(id) - (availabilityScheduleId)"
    BookingLink ||--o{ Booking : "(id) - (bookingLinkId)"
    EmailAccount ||--o{ Booking : "(id) - (emailAccountId)"
    EmailAccount ||--o{ MeetingBriefing : "(id) - (emailAccountId)"
    EmailAccount ||--o{ MeetingRecording : "(id) - (emailAccountId)"
    MeetingRecording |o--o{ Meeting : "(id) - (recordingId)"
    EmailAccount ||--o{ Meeting : "(id) - (emailAccountId)"
    EmailAccount ||--o{ MessagingChannel : "(id) - (emailAccountId)"
    MessagingChannel ||--o{ MessagingRoute : "(id) - (messagingChannelId)"
    MessagingChannel ||--o{ AutomationJob : "(id) - (messagingChannelId)"
    AutomationJob ||--o{ AutomationJobRun : "(id) - (automationJobId)"
    EmailAccount ||--o{ DriveConnection : "(id) - (emailAccountId)"
    DriveConnection ||--o{ FilingFolder : "(id) - (driveConnectionId)"
    EmailAccount ||--o{ FilingFolder : "(id) - (emailAccountId)"
    DriveConnection ||--o{ DocumentFiling : "(id) - (driveConnectionId)"
    EmailAccount ||--o{ DocumentFiling : "(id) - (emailAccountId)"
    Rule ||--o{ AttachmentSource : "(id) - (ruleId)"
    DriveConnection ||--o{ AttachmentSource : "(id) - (driveConnectionId)"
    AttachmentSource ||--o{ AttachmentDocument : "(id) - (attachmentSourceId)"
    User ||--o{ Referral : "(id) - (referrerUserId)"
    User ||--o| Referral : "(id) - (referredUserId)"
    EmailAccount |o--o{ McpIntegration : "(id) - (emailAccountId)"
    McpIntegration ||--o{ McpConnection : "(id) - (integrationId)"
    EmailAccount |o--o{ McpConnection : "(id) - (emailAccountId)"
    McpConnection ||--o{ McpTool : "(id) - (connectionId)"
    User ||--o{ MobilePushToken : "(id) - (userId)"
    MobilePushToken ||--o{ MobilePushDelivery : "(id) - (mobilePushTokenId)"
    User |o--o{ OauthClient : "(id) - (userId)"
    OauthClient ||--o{ OauthRefreshToken : "(clientId) - (clientId)"
    Session |o--o{ OauthRefreshToken : "(id) - (sessionId)"
    User ||--o{ OauthRefreshToken : "(id) - (userId)"
    OauthClient ||--o{ OauthAccessToken : "(clientId) - (clientId)"
    Session |o--o{ OauthAccessToken : "(id) - (sessionId)"
    User |o--o{ OauthAccessToken : "(id) - (userId)"
    OauthRefreshToken |o--o{ OauthAccessToken : "(id) - (refreshId)"
    OauthClient ||--o{ OauthConsent : "(clientId) - (clientId)"
    User |o--o{ OauthConsent : "(id) - (userId)"
    OauthClient ||--o{ OauthClientResource : "(clientId) - (clientId)"
    OauthResource ||--o{ OauthClientResource : "(identifier) - (resourceId)"
    scimManagedConnection ||--o{ scimManagedCredential : "(id) - (connectionRecordId)"
    scimManagedConnection ||--o{ scimManagedConnectionEvent : "(id) - (connectionRecordId)"
    User ||--o{ scimIdentityTombstone : "(id) - (userId)"
    User ||--o| scimSubject : "(id) - (userId)"
    User ||--o{ scimUser : "(id) - (userId)"
    scimUser ||--o{ scimProjectionGrant : "(id) - (scimUserId)"
    User ||--o{ scimProjectionGrant : "(id) - (userId)"
    scimGroup ||--o{ scimGroupMember : "(id) - (groupId)"
    scimUser ||--o{ scimGroupMember : "(id) - (scimUserId)"
    User ||--o{ ScimIdentityLink : "(id) - (userId)"
    Category }o--o{ Rule : "(rules) - (categoryFilters)"
    Account {
        string password
        string id PK
        datetime createdAt
        datetime updatedAt
        string userId FK
        string provider UK
        string type
        string providerAccountId UK
        string refresh_token
        datetime refreshTokenExpiresAt
        string access_token
        datetime expires_at
        datetime disconnectedAt
        string token_type
        string scope
        string id_token
        string session_state
    }
    Session {
        string id PK
        string sessionToken UK
        string userId FK
        datetime expires
        datetime createdAt
        datetime updatedAt
        boolean emailOtp
        int emailOtpVersion
        string ipAddress
        string userAgent
        string activeOrganizationId FK
    }
    User {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        string email UK
        boolean emailVerified
        string image
        datetime completedOnboardingAt
        signupintent signupIntent
        json onboardingAnswers
        datetime lastLogin
        json utms
        json errorMessages
        datetime announcementDismissedAt
        string[] dismissedHints
        string[] surveyFeatures
        string surveyRole
        string surveyGoal
        int surveyCompanySize
        string surveySource
        string surveyImprovements
        boolean mcpServerEnabled
        int mcpTokenVersion
        boolean emailOtpEnabled
        int emailOtpVersion
        string aiProvider
        string aiModel
        string aiApiKey
        boolean decisionModelEnabled
        string webhookSecret
        string referralCode UK
        string premiumId FK
        string premiumAdminId FK
        boolean scimAccessDisabled
    }
    EmailAccount {
        string id PK
        string email UK
        datetime createdAt
        datetime updatedAt
        string name
        string image
        int sortOrder
        string about
        string writingStyle
        string signature
        json sentSignatureScan
        datetime sentSignatureScannedAt
        boolean includeReferralSignature
        boolean includeSentWithSignature
        boolean sentMessageOpenTrackingEnabled
        datetime watchEmailsExpirationDate
        string watchEmailsSubscriptionId
        json watchEmailsSubscriptionHistory
        string lastSyncedHistoryId
        json behaviorProfile
        json personaAnalysis
        string role
        string timezone
        string calendarBookingLink
        string[] draftDocsUrls
        frequency statsEmailFrequency
        frequency summaryEmailFrequency
        datetime lastSummaryEmailAt
        datetime lastInboxHealthEmailAt
        coldemailsetting coldEmailBlocker
        boolean coldEmailDigest
        string coldEmailPrompt
        string rulesPrompt
        boolean autoCategorizeSenders
        boolean multiRuleSelectionEnabled
        string sensitiveDataPolicy
        draftreplyconfidence draftReplyConfidence
        int draftCleanupDays
        boolean allowHiddenAiDraftLinks
        int rulesRevision
        maillayout mailLayout
        boolean mailExpandedPreview
        boolean mailBundleInbox "Collapses threads from bundled splits into one row each in the inbox. Null until first turned on, which is when default bundles are chosen."
        string mailDefaultSplitId
        mailsetupstate mailSetup
        boolean includeInAllAccounts
        boolean meetingBriefingsEnabled
        int meetingBriefingsMinutesBefore
        boolean meetingBriefsSendEmail
        boolean meetingRecorderEnabled
        meetingjoinrule meetingRecorderJoinRule
        boolean meetingRecorderRecapEmailEnabled
        boolean meetingRecorderFollowUpDraftEnabled
        boolean filingEnabled
        string filingPrompt
        boolean filingConfirmationSendEmail
        float followUpAwaitingReplyDays
        float followUpNeedsReplyDays
        boolean followUpAutoDraftEnabled
        boolean digestSendEmail
        string learnedWritingStyle
        string userId FK
        string accountId FK, UK
    }
    MailSplit {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        int order
        boolean matchAll "Whether every filter must match, or any one of them (#quot;Show mail matching all/any of these conditions#quot;). A split with no filters is the plain inbox."
        boolean bundleInInbox "Shown as one collapsed row in the inbox when the account bundles its inbox."
        boolean hideFromAll
        string emailAccountId FK, UK
    }
    MailSplitFilter {
        string id PK
        mailsplitfilterkind kind
        string value
        int order UK
        string mailSplitId FK, UK
    }
    Organization {
        string id PK
        string name
        string slug UK
        string logo
        json metadata
        datetime createdAt
        datetime updatedAt
    }
    OrganizationRule {
        string id PK
        datetime createdAt
        datetime updatedAt
        string organizationId FK, UK
        boolean enabled
        string name UK
        string instructions
        boolean runOnThreads
        logicaloperator conditionalOperator
        string from
        string to
        string subject
        string body
    }
    OrganizationRuleAction {
        string id PK
        datetime createdAt
        datetime updatedAt
        string organizationRuleId FK
        actiontype type
        string label
        string subject
        string content
        string to
        string cc
        string bcc
        string url
        string folderName
        int delayInMinutes
        json staticAttachments
    }
    Member {
        string id PK
        string organizationId FK, UK
        string emailAccountId FK, UK
        string role
        boolean allowOrgAdminAnalytics
        datetime createdAt
        datetime updatedAt
    }
    Conversation {
        string id PK
        datetime createdAt
        datetime updatedAt
        string organizationId FK
        string publisherId FK, UK
        string threadId UK
        conversationstatus status
        int revision
        int generation
    }
    ConversationParticipant {
        string id PK
        string conversationId FK, UK
        string memberId FK, UK
        int generation UK
        boolean active
        int readRevision
        boolean muted
        datetime createdAt
    }
    ConversationComment {
        string id PK
        string conversationId FK, UK
        string authorMemberId FK, UK
        string clientMutationId UK
        string body
        int revision UK
        datetime deletedAt
        datetime createdAt
    }
    ConversationActivity {
        string id PK
        string participantId FK, UK
        string commentId FK
        conversationactivitykind kind UK
        int revision UK
        datetime createdAt
    }
    invitation {
        string id PK
        string organizationId FK
        string email
        string role
        string status
        datetime expiresAt
        string inviterId FK
    }
    verification {
        string id PK
        string identifier
        string value
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
    }
    ssoProvider {
        string userId FK
        string id PK
        string issuer
        string oidcConfig
        string samlConfig
        string emailAccountId FK
        string providerId UK
        string organizationId FK, UK
        string domain
    }
    Digest {
        string id PK
        datetime createdAt
        datetime updatedAt
        string emailAccountId FK
        datetime sentAt
        digeststatus status
    }
    DigestItem {
        string id PK
        datetime createdAt
        datetime updatedAt
        string messageId UK
        string threadId UK
        string content
        string digestId FK, UK
        string actionId FK
        string coldEmailId FK
    }
    Schedule {
        string id PK
        datetime createdAt
        datetime updatedAt
        int intervalDays
        int occurrences
        int daysOfWeek
        datetime timeOfDay
        string emailAccountId FK, UK
        datetime lastOccurrenceAt
        datetime nextOccurrenceAt
    }
    Premium {
        string id PK
        datetime createdAt
        datetime updatedAt
        string[] pendingInvites
        datetime lemonSqueezyRenewsAt
        int lemonSqueezyCustomerId
        int lemonSqueezySubscriptionId
        int lemonSqueezySubscriptionItemId
        int lemonSqueezyOrderId
        int lemonSqueezyProductId
        int lemonSqueezyVariantId
        string lemonLicenseKey
        string lemonLicenseInstanceId
        string lemonSubscriptionStatus
        string stripeCustomerId UK
        string stripeSubscriptionId UK
        string stripeSubscriptionItemId UK
        string stripePriceId
        string stripeProductId
        string stripeSubscriptionStatus
        datetime stripeCancelAt
        boolean stripeCancelAtPeriodEnd
        datetime stripeRenewsAt
        datetime stripeTrialEnd
        string stripeTrialConversionInvoiceId
        datetime stripeTrialConvertedAt
        datetime stripeCancellationInitiatedAt
        datetime stripeCanceledAt
        datetime stripeEndedAt
        string stripeCancellationFeedback
        string stripeCancellationComment
        string stripeAiOverageLastInvoiceId
        datetime stripeAiOverageLastPeriodEnd
        int stripeAiOverageLastUnits
        boolean stripeInvoiceEmailsEnabled
        string appleOriginalTransactionId UK
        string appleLatestTransactionId UK
        string appleProductId
        string appleSubscriptionGroupIdentifier
        string appleAppAccountToken
        string appleSubscriptionStatus
        string appleEnvironment
        datetime applePurchaseDate
        datetime appleExpiresAt
        datetime appleRevokedAt
        string appleOfferDiscountType
        datetime adminGrantExpiresAt
        premiumtier adminGrantTier
        premiumtier tier
        int emailAccountsAccess
        int unsubscribeMonth
        int unsubscribeCredits
        int aiMonth
        int aiCredits
    }
    PendingPremiumGrant {
        string id PK
        datetime createdAt
        datetime updatedAt
        string email UK
        premiumtier tier
        int count
        int emailAccountsAccess
    }
    VerificationToken {
        string id PK
        datetime createdAt
        datetime updatedAt
        string identifier UK
        string token UK
        datetime expires
    }
    Label {
        string id PK
        datetime createdAt
        datetime updatedAt
        string gmailLabelId UK
        string name UK
        string description
        boolean enabled
        string emailAccountId FK, UK
    }
    Rule {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string name UK
        boolean enabled
        boolean automate
        boolean runOnThreads
        string emailAccountId FK, UK
        string organizationRuleId FK, UK
        boolean organizationRuleMemberEnabled
        logicaloperator conditionalOperator
        string instructions
        string groupId FK, UK
        string from
        string to
        string subject
        string body
        categoryfiltertype categoryFilterType
        systemtype systemType UK
        string promptText
    }
    Action {
        string id PK
        datetime createdAt
        datetime updatedAt
        actiontype type
        string ruleId FK
        string emailAccountId FK
        string messagingChannelId FK
        string messagingChannelEmailAccountId FK
        string label
        string labelId
        string subject
        string content
        string to
        string cc
        string bcc
        string url
        string folderName
        string folderId
        int delayInMinutes
        json staticAttachments
        string integrationName
        string integrationToolName
        json integrationArgs
    }
    RuleHistory {
        string id PK
        datetime createdAt
        string ruleId FK, UK
        int version UK
        string triggerType
        string promptText
        string name
        string instructions
        boolean enabled
        boolean automate
        boolean runOnThreads
        string conditionalOperator
        string from
        string to
        string subject
        string body
        string categoryFilterType
        string systemType
        json actions
        json categoryFilters
    }
    ExecutedRule {
        string id PK
        datetime createdAt
        datetime updatedAt
        string threadId
        string messageId
        executedrulestatus status
        boolean automated
        string reason
        json matchMetadata
        string ruleId FK
        string emailAccountId FK
    }
    ExecutedAction {
        string id PK
        datetime createdAt
        datetime updatedAt
        actiontype type
        executedactionstatus executionStatus
        datetime executionStartedAt
        datetime executedAt
        json executionError
        string[] sentMessageIds
        string executedRuleId FK
        string messagingChannelId FK
        string messagingMessageId
        datetime messagingMessageSentAt
        messagingmessagestatus messagingMessageStatus
        string label
        string labelId
        string subject
        string content
        string to
        string cc
        string bcc
        string url
        string folderName
        string folderId
        json staticAttachments
        json selectedAttachments
        string integrationName
        string integrationToolName
        json integrationArgs
        string draftId
        draftemailstatus draftStatus
        boolean wasDraftSent
        string draftModelProvider
        string draftModelName
        int draftPipelineVersion
        json draftContextMetadata
    }
    ScheduledAction {
        string id PK
        datetime createdAt
        datetime updatedAt
        string executedRuleId FK
        actiontype actionType
        string messageId
        string threadId
        datetime scheduledFor
        string emailAccountId FK
        scheduledactionstatus status
        schedulingstatus schedulingStatus
        string label
        string labelId
        string subject
        string content
        string to
        string cc
        string bcc
        string url
        string folderName
        string folderId
        string scheduledId
        json staticAttachments
        json selectedAttachments
        datetime executedAt
        string executedActionId FK, UK
    }
    SnoozedThread {
        string id PK
        datetime updatedAt
        string threadId
        datetime scheduledFor
        snoozedthreadstatus status
        string clientMutationId UK
        string emailAccountId FK, UK
    }
    ScheduledEmail {
        string id PK
        datetime createdAt
        datetime updatedAt
        string emailAccountId FK, UK
        string clientMutationId UK
        string payloadHash
        json payload
        string threadId
        datetime sendAt
        scheduledemailstatus status
        datetime processingStartedAt
        datetime executionQueuedAt
        datetime sentAt
        string error
        datetime remindAt
        emailreminderstatus reminderStatus
        datetime reminderStartedAt
        boolean heldForUndo
        string[] draftMessageIds
    }
    EmailSendOperation {
        string id PK
        datetime createdAt
        datetime updatedAt
        string clientMutationId UK
        string payloadHash
        emailsendoperationstatus status
        datetime processingStartedAt
        json result
        string emailAccountId FK, UK
    }
    SentMessageOpen {
        string id PK
        datetime createdAt
        datetime updatedAt
        string token UK
        string emailAccountId FK, UK
        string threadId
        string messageId UK
        datetime firstOpenedAt
        datetime lastOpenedAt
        int openCount
    }
    Group {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string prompt
        string emailAccountId FK, UK
    }
    GroupItem {
        string id PK
        datetime createdAt
        datetime updatedAt
        string groupId FK, UK
        groupitemtype type UK
        string value UK
        boolean exclude
        string reason
        string threadId
        string messageId
        groupitemsource source
    }
    ClassificationFeedback {
        string id PK
        datetime createdAt
        string sender UK
        classificationfeedbackeventtype eventType UK
        string ruleId FK, UK
        string emailAccountId FK, UK
        string threadId
        string messageId UK
    }
    Category {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string description
        string emailAccountId FK, UK
    }
    Newsletter {
        string id PK
        datetime createdAt
        datetime updatedAt
        string email UK
        string name
        newsletterstatus status
        boolean patternAnalyzed
        datetime lastAnalyzedAt
        string emailAccountId FK, UK
        string categoryId FK
    }
    ColdEmail {
        string id PK
        datetime createdAt
        datetime updatedAt
        string fromEmail UK
        string messageId
        string threadId
        coldemailstatus status
        string reason
        string emailAccountId FK, UK
    }
    EmailMessage {
        string id PK
        datetime createdAt
        datetime updatedAt
        string threadId UK
        string messageId UK
        datetime date
        string from
        string fromName
        string fromDomain
        string to
        string unsubscribeLink
        boolean read
        boolean sent
        boolean draft
        boolean inbox
        string emailAccountId FK, UK
    }
    ResponseTime {
        string id PK
        datetime createdAt
        string threadId
        string sentMessageId UK
        string receivedMessageId
        int responseTimeMins
        datetime receivedAt
        datetime sentAt
        string emailAccountId FK, UK
    }
    ThreadTracker {
        string id PK
        datetime createdAt
        datetime updatedAt
        datetime sentAt
        string threadId UK
        string messageId UK
        boolean resolved
        threadtrackertype type
        datetime followUpAppliedAt
        string followUpDraftId
        string followUpDraftContent
        datetime followUpDraftCreatedAt
        json followUpNotifications
        string emailAccountId FK, UK
    }
    CleanupJob {
        string id PK
        datetime createdAt
        datetime updatedAt
        cleanaction action
        int daysOld
        string instructions
        boolean skipReply
        boolean skipStarred
        boolean skipCalendar
        boolean skipReceipt
        boolean skipAttachment
        boolean skipConversation
        string emailAccountId FK
    }
    CleanupThread {
        string id PK
        datetime createdAt
        datetime updatedAt
        string threadId
        boolean archived
        string jobId FK
        string emailAccountId FK
    }
    Knowledge {
        string id PK
        datetime createdAt
        datetime updatedAt
        string title UK
        string content
        string emailAccountId FK, UK
    }
    Snippet {
        string id PK
        datetime createdAt
        datetime updatedAt
        string shortcut UK
        string content
        string emailAccountId FK, UK
    }
    EmailSignature {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string html
        string emailAccountId FK, UK
    }
    ReplyMemory {
        string id PK
        datetime createdAt
        datetime updatedAt
        string content UK
        replymemorykind kind UK
        replymemoryscopetype scopeType UK
        string scopeValue UK
        boolean isLearnedStyleEvidence
        string emailAccountId FK, UK
    }
    ReplyMemorySource {
        datetime createdAt
        datetime learnedWritingStyleAnalyzedAt
        string replyMemoryId PK, FK
        string draftSendLogId PK, FK
    }
    ApiKey {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        string hashedKey UK
        boolean isActive
        datetime expiresAt
        datetime lastUsedAt
        apikeyscope[] scopes
        string userId FK
        string emailAccountId FK
    }
    EmailToken {
        string id PK
        datetime createdAt
        string token UK
        datetime expiresAt
        string emailAccountId FK
    }
    Payment {
        string id PK
        datetime createdAt
        datetime updatedAt
        string premiumId FK
        processortype processorType
        string processorId UK
        string processorSubscriptionId
        string processorCustomerId
        int amount
        string currency
        string status
        int tax
        boolean taxInclusive
        boolean refunded
        datetime refundedAt
        int refundedAmount
        string billingReason
        datetime invoiceEmailSentAt
    }
    DraftSendLog {
        string id PK
        datetime createdAt
        string executedActionId FK, UK
        string sentMessageId
        float similarityScore
        float bodySimilarityScore
        string bodySimilarityStatus
        json similarityMetadata
        string sentText
        string replyMemorySentText
        int replyMemoryAttemptCount
        datetime replyMemoryProcessedAt
    }
    Chat {
        string id PK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string name
        int compactionCount
        int lastSeenRulesRevision
        string activeStreamId
        datetime activeStreamStartedAt
        string emailAccountId FK
    }
    ChatMessage {
        string id PK
        datetime createdAt
        datetime updatedAt
        string role
        json parts
        json metadata
        string chatId FK
    }
    ChatCompaction {
        string id PK
        datetime createdAt
        string summary
        int messageCount
        datetime compactedBeforeCreatedAt
        string chatId FK
    }
    ChatActionTotal {
        string id PK
        datetime createdAt
        datetime updatedAt
        string action UK
        int count
        string chatId FK, UK
    }
    ChatMemory {
        string id PK
        datetime createdAt
        datetime updatedAt
        string content
        string chatId FK
        string emailAccountId FK
    }
    CalendarConnection {
        string id PK
        datetime createdAt
        datetime updatedAt
        string provider UK
        string email UK
        string accessToken
        string refreshToken
        datetime expiresAt
        boolean isConnected
        string emailAccountId FK, UK
    }
    Calendar {
        string id PK
        datetime createdAt
        datetime updatedAt
        string calendarId UK
        string name
        string description
        boolean primary
        boolean isEnabled
        string timezone
        string connectionId FK, UK
    }
    BookingLink {
        string id PK
        datetime createdAt
        datetime updatedAt
        string slug UK
        string title
        string description
        boolean isActive
        int durationMinutes
        int slotIntervalMinutes
        bookinglinklocationtype locationType
        string locationValue
        int minimumNoticeMinutes
        int maxDaysAhead
        string emailAccountId FK, UK
        string destinationCalendarId FK
        string availabilityScheduleId FK
    }
    AvailabilitySchedule {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string name
        boolean isDefault
        string timezone
        string emailAccountId FK, UK
    }
    AvailabilityWindow {
        string id PK
        datetime createdAt
        datetime updatedAt
        int weekday
        int startMinutes
        int endMinutes
        string availabilityScheduleId FK
    }
    Booking {
        string id PK
        datetime createdAt
        datetime updatedAt
        string guestName
        string guestEmail
        string guestNote
        datetime startTime
        datetime endTime
        bookingstatus status
        string provider
        string providerConnectionId
        string providerCalendarId
        string providerEventId
        string videoConferenceLink
        string cancelTokenHash
        string cancellationReason
        string idempotencyToken UK
        string bookingLinkId FK, UK
        string emailAccountId FK
    }
    MeetingBriefing {
        string id PK
        datetime createdAt
        string calendarEventId UK
        string eventTitle
        datetime eventStartTime
        int guestCount
        meetingbriefingstatus status
        string emailAccountId FK, UK
    }
    ContactResearch {
        string id PK
        datetime createdAt
        string email
        datetime researchStartedAt
        boolean found
        string role
        string confidence
        json company
        string[] sources
    }
    MeetingRecording {
        string id PK
        datetime createdAt
        datetime updatedAt
        string botProvider UK
        string externalBotId UK
        string botName
        string externalRecordingId
        datetime transcriptRequestedAt
        string externalTranscriptId
        string meetingUrl
        string normalizedMeetingUrl
        string activeKey UK
        datetime meetingStartTime UK
        meetingrecordingstatus status
        string failureReason
        json transcript
        datetime transcriptFetchedAt
        datetime mediaDeletedAt
        string emailAccountId FK
    }
    Meeting {
        string id PK
        datetime createdAt
        datetime updatedAt
        string calendarEventId UK
        string eventTitle
        datetime startTime
        datetime endTime
        json attendees
        string organizerEmail
        boolean joinOverride
        meetingprocessingstatus processingStatus
        int processingAttempts
        json summary
        datetime followUpDraftStartedAt
        string followUpDraftId
        datetime recapSentAt
        string recordingId FK, UK
        string emailAccountId FK, UK
    }
    MessagingChannel {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        messagingprovider provider UK
        boolean isConnected
        string teamId UK
        string teamName
        string providerUserId
        string botUserId
        string accessToken
        string refreshToken
        datetime expiresAt
        string emailAccountId FK, UK
    }
    MessagingRoute {
        string id PK
        datetime createdAt
        datetime updatedAt
        messagingroutepurpose purpose UK
        messagingroutetargettype targetType
        string targetId
        string messagingChannelId FK, UK
    }
    AutomationJob {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        boolean enabled
        string prompt
        string cronExpression
        datetime nextRunAt
        string messagingChannelId FK
        string emailAccountId FK, UK
    }
    AutomationJobRun {
        string id PK
        datetime createdAt
        automationjobrunstatus status
        datetime scheduledFor UK
        datetime processedAt
        string outboundMessage
        string providerMessageId
        string error
        string automationJobId FK, UK
    }
    DriveConnection {
        string id PK
        datetime createdAt
        datetime updatedAt
        string provider UK
        string email
        string accessToken
        string refreshToken
        datetime expiresAt
        boolean isConnected
        string emailAccountId FK, UK
    }
    FilingFolder {
        string id PK
        datetime createdAt
        datetime updatedAt
        string folderId UK
        string folderName
        string folderPath
        string driveConnectionId FK
        string emailAccountId FK, UK
    }
    DocumentFiling {
        string id PK
        datetime createdAt
        datetime updatedAt
        string messageId UK
        string attachmentId UK
        string filename
        string folderId
        string folderPath
        string fileId
        string reasoning
        float confidence
        documentfilingstatus status
        boolean wasAsked
        boolean wasCorrected
        string originalPath
        datetime correctedAt
        boolean feedbackPositive
        datetime feedbackAt
        string notificationMessageId UK
        datetime notificationSentAt
        string notificationBatchId
        string driveConnectionId FK
        string emailAccountId FK, UK
    }
    AttachmentSource {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        attachmentsourcetype type UK
        string sourceId UK
        string sourcePath
        string ruleId FK, UK
        string driveConnectionId FK, UK
    }
    AttachmentDocument {
        string id PK
        datetime createdAt
        datetime updatedAt
        string attachmentSourceId FK, UK
        string fileId UK
        string name
        string mimeType
        datetime modifiedAt
        string summary
        string content
        json metadata
        datetime indexedAt
        string error
    }
    Referral {
        string id PK
        datetime createdAt
        datetime updatedAt
        string referrerUserId FK
        string referredUserId FK, UK
        string referralCodeUsed
        referralstatus status
        datetime rewardGrantedAt
        string stripeBalanceTransactionId
        int rewardAmount
    }
    McpIntegration {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string registeredServerUrl
        string registeredAuthorizationUrl
        string registeredTokenUrl
        string oauthClientId
        string oauthClientSecret
        string displayName
        string serverUrl
        mcpauthtype authType
        string emailAccountId FK
    }
    McpConnection {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        boolean isActive
        string accessToken
        string refreshToken
        string apiKey
        datetime expiresAt
        string integrationId FK, UK
        string emailAccountId FK, UK
    }
    McpTool {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string description
        json schema
        boolean isEnabled
        boolean isWrite
        string connectionId FK, UK
    }
    MobilePushToken {
        string id PK
        datetime createdAt
        datetime updatedAt
        string token UK
        mobilepushplatform platform
        mobilepushtokentype tokenType
        string appVersion
        string userId FK
    }
    MobilePushDelivery {
        string id PK
        datetime createdAt
        string deduplicationKey UK
        string mobilePushTokenId FK, UK
    }
    OauthClient {
        string id PK
        string clientId UK
        string clientSecret
        string clientDiscoveryId
        boolean disabled
        boolean skipConsent
        boolean enableEndSession
        string subjectType
        string[] scopes
        string[] clientCredentialsScopes
        string userId FK
        datetime createdAt
        datetime updatedAt
        string name
        string uri
        string icon
        string[] contacts
        string tos
        string policy
        string softwareId
        string softwareVersion
        string softwareStatement
        string[] redirectUris
        string[] postLogoutRedirectUris
        string backchannelLogoutUri
        boolean backchannelLogoutSessionRequired
        string tokenEndpointAuthMethod
        string applicationType
        string jwks
        string jwksUri
        string[] grantTypes
        string[] responseTypes
        boolean requirePKCE
        boolean dpopBoundAccessTokens
        string referenceId
        json metadata
    }
    OauthRefreshToken {
        string id PK
        string token UK
        string clientId FK
        string sessionId FK
        string userId FK
        string referenceId
        string authorizationCodeId
        string[] resources
        string[] requestedUserInfoClaims
        datetime expiresAt
        datetime createdAt
        datetime revoked
        datetime rotatedAt
        string rotationReplayResponse
        datetime rotationReplayExpiresAt
        datetime authTime
        json confirmation
        string[] scopes
    }
    OauthAccessToken {
        string id PK
        string token UK
        string clientId FK
        string sessionId FK
        string userId FK
        string referenceId
        string authorizationCodeId
        string[] resources
        string[] requestedUserInfoClaims
        string refreshId FK
        datetime expiresAt
        datetime createdAt
        datetime revoked
        json confirmation
        string[] scopes
    }
    OauthConsent {
        string id PK
        string clientId FK, UK
        string userId FK, UK
        string referenceId
        string[] resources
        string[] requestedUserInfoClaims
        string[] scopes
        datetime createdAt
        datetime updatedAt
    }
    Jwks {
        string id PK
        string publicKey
        string privateKey
        datetime createdAt
        datetime expiresAt
        string alg
        string crv
    }
    OauthResource {
        string id PK
        string identifier UK
        string name
        int accessTokenTtl
        int refreshTokenTtl
        string signingAlgorithm
        string signingKeyId
        string[] allowedScopes
        json customClaims
        boolean dpopBoundAccessTokensRequired
        boolean disabled
        datetime createdAt
        datetime updatedAt
        int policyVersion
        json metadata
    }
    OauthClientResource {
        string id PK
        string clientId FK, UK
        string resourceId FK, UK
        json metadata
        datetime createdAt
    }
    OauthClientAssertion {
        string id PK
        datetime expiresAt
    }
    scimManagedConnection {
        string id PK
        string creationRequestId UK
        string connectionId UK
        string provisioningDomainId
        string status
        int revision
        datetime createdAt
        string createdBy
        datetime decommissionStartedAt
        string decommissionStartedBy
        datetime decommissionedAt
        string decommissionedBy
    }
    scimManagedCredential {
        string id PK
        string connectionRecordId FK
        string credentialId UK
        string tokenDigest
        string hashVersion
        string activeSlotKey UK
        string status
        string serializedScopes
        datetime expiresAt
        datetime createdAt
        string createdBy
        datetime lastUsedAt
        datetime revokedAt
        string revokedBy
        datetime decommissionedAt
    }
    scimManagedConnectionEvent {
        string id PK
        string connectionRecordId FK
        string eventKey UK
        int sequence
        string type
        string actorId
        string credentialId
        datetime createdAt
    }
    scimConnectionBinding {
        string id PK
        string connectionId
        string connectionKey UK
        string provisioningDomainId
        datetime createdAt
        datetime decommissionedAt
        string decommissionStatus
        string decommissionCursorUserId
        int decommissionReconciledUserCount
        int decommissionBatchCount
        int decommissionRevision
        datetime decommissionCompletedAt
        string decommissionLeaseId
        datetime decommissionLeaseExpiresAt
    }
    scimIdentityTombstone {
        string id PK
        string connectionId
        string provisioningDomainId
        string externalId
        string externalIdKey UK
        string userId FK
        string profile
        datetime deletedAt
    }
    scimSubject {
        string id PK
        string userId FK, UK
        string profileSourceId
        int revision
        datetime createdAt
        datetime updatedAt
    }
    scimUser {
        string id PK
        string connectionId
        string provisioningDomainId
        string userId FK
        string connectionUserKey UK
        string userName
        string userNameKey UK
        string primaryEmail
        string workEmailValueIndex
        string emailValueIndex
        string displayName
        string formattedName
        string givenName
        string familyName
        string serializedEmails
        string serializedAttributes
        string externalId
        string externalIdKey UK
        boolean active
        string orderKey UK
        datetime createdAt
        datetime updatedAt
    }
    scimProjectionGrant {
        string id PK
        string connectionId
        string provisioningDomainId
        string scimUserId FK
        string userId FK
        string sourceKind
        string sourceId
        string sourceValue
        string role
        string grantKey UK
        datetime createdAt
        datetime updatedAt
    }
    scimGroup {
        string id PK
        string connectionId
        string provisioningDomainId
        int revision
        string displayName
        string displayNameKey UK
        string externalId
        string externalIdKey UK
        string orderKey UK
        datetime createdAt
        datetime updatedAt
    }
    scimGroupMember {
        string id PK
        string connectionId
        string groupId FK
        string scimUserId FK
        string membershipKey UK
        datetime createdAt
    }
    ScimIdentityLink {
        string id PK
        string connectionId UK
        string externalId UK
        string userId FK
    }
    LoopsEmailSource {
        string id PK
        datetime createdAt
        datetime updatedAt
        string emailMessageId UK
        string loopId
        string loopName
        string campaignId
        string campaignName
        string sourceType
        datetime eventTime
    }
```